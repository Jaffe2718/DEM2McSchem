from ast import literal_eval                   # parse string to dict
from cv2 import resize                         # resize matrix
from osgeo.gdal import Open                    # read DEM data
from numpy import random, rint                 # round to nearest integer
from mcschematic import MCSchematic, Version   # create schematic
from os.path import dirname, basename          # handle path
from sys import argv                           # get arguments from command line

__version__ = '1.1.0'

dict_interpolation = {
    "INTER_NEAREST": 0,
    "INTER_LINEAR": 1,
    "INTER_AREA": 3,
    "INTER_CUBIC": 2,
    "INTER_LANCZOS4": 4,
    "INTER_LINEAR_EXACT": 5,
    "INTER_MAX": 7,
    "WARP_FILL_OUTLIERS": 8,
    "WARP_INVERSE_MAP": 16
}

def convert(dem_path: str,
            schema_path: str,
            interpolation='INTER_LINEAR',
            scale='1.0',
            pavement_elevation='0.0',
            version='JE_1_19_2',
            stratum_struct='[("minecraft:grass_block": 1)]',
            noise='[]'):
    '''
A tool to convert DEM data to Minecraft Schematic File (*.schem)

Usage:
    Help:
        dem2schema.exe | dem2schema.exe -h | dem2schema.exe --help

    Show Supported Minecraft Versions:
        dem2schema.exe -v | dem2schema.exe --mc-versions

    Convert DEM data to minecraft schematic (*.schem):
        dem2schema.exe <dem_path:str> <schema_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>] [<noise=[]>]

        :param dem_path: DEM file path
        :param schema_path: output schematic file path (include file name)
        :param interpolation: interpolation method, default is INTER_LINEAR, can be:
                    INTER_NEAREST - a nearest-neighbor interpolation
                    INTER_LINEAR - a bi-linear interpolation (used by default)
                    INTER_AREA - resampling using pixel area relation. It may be a preferred method for image decimation,
                        as it gives moiré'-free results. But when the image is zoomed, it is similar to the INTER_NEAREST method.
                    INTER_CUBIC - a bicubic interpolation over 4x4 pixel neighborhood
                    INTER_LANCZOS4 - a Lanczos interpolation over 8x8 pixel neighborhood
                    INTER_LINEAR_EXACT - a bi-linear interpolation for exact downscaling
                    INTER_MAX - flag, gives maximum interpolation algorithm
                    WARP_FILL_OUTLIERS - flag, inverse mapping filling all the destination image pixels
                    WARP_INVERSE_MAP - flag, sets the mapping to be from destination image to source image
        :param scale: scale the linear size of the schematic from the real world data, default is 1.0
                    In minecraft, 1 block = 1 meter, the real world distance is depended on the spatial reference system
        :param pavement_elevation: elevation of pavement, default is 0.0, only to build the positive elevation finally in minecraft
        :param version: version of the schematic, default is JE_1_19_2
        :param stratum_struct: python list of stratum structure, default is [("minecraft:grass_block",1)],
                    all the elements are tuple, the first element is the block name, the second element is the thickness weight.
                    For example, [("minecraft:grass_block",1),("minecraft:dirt",3),("minecraft:stone",5)], which means
                    the first layer is grass block, the second layer is dirt, the third layer is stone, and the thickness
                    of these blocks are block:dirt:stone = 1:3:5
        :param noise: python list of noice structure in string format, default is [], all the elements are dict,
                    used to randomly generate blocks in game. The item front will cover the back.
                    format: [ {'block': 'minecraft:<block_id>', 'density': <float: between 0 and 1>}, ... ]
                    example: "[{'block': 'minecraft:oak_sapling', 'density': 0.005}, {'block': 'minecraft:dark_oak_sapling', 'density': 0.002}, {'block': 'minecraft:dandelion', 'density': 0.002}]"
    '''
    # read DEM data as a single band raster
    dem = Open(dem_path)
    # get min value, max value, nodata value, spatial_resolution from the DEM raster
    dem_min, dem_max, dem_nodata = dem.GetRasterBand(1).GetMinimum(), dem.GetRasterBand(1).GetMaximum(), dem.GetRasterBand(1).GetNoDataValue()
    dem_resolution = dem.GetGeoTransform()[1]
    # read DEM data as a matrix, if the DEM raster is multi-band, only the first band will be read
    dem_matrix = dem.ReadAsArray()
    # ignore the nodata value by setting it to the pavement elevation
    dem_matrix[dem_matrix == dem_nodata] = float(pavement_elevation)
    # re-calculate elevation, if the elevation is less than the pavement elevation, set it to the 0,
    # else set it as (elevation - pavement_elevation)
    dem_matrix = dem_matrix - float(pavement_elevation)    # subtract the pavement elevation
    dem_matrix[dem_matrix < 0] = 0                         # set the negative value to 0
    # get the size of the DEM matrix
    dem_rows, dem_cols = dem_matrix.shape
    mc_rows = round(dem_rows * float(scale) * dem_resolution)  # number of rows in the schematic, y direction
    mc_cols = round(dem_cols * float(scale) * dem_resolution)  # number of columns in the schematic, x direction
    # scale the DEM matrix to the size of the schematic, what's more, the elevation will be scaled
    mc_data = resize(dem_matrix, (mc_cols, mc_rows), interpolation=dict_interpolation[interpolation]) * float(scale)
    # rounding the mc_data matrix to integer
    mc_data = rint(mc_data)

    # parse stratum_struct list as a list of tuple
    struct = literal_eval(stratum_struct)
    weight_list = list(item[1] for item in struct)  # get the thickness weight list
    sum_weight = sum(weight_list)  # get the sum of the thickness weight
    cumu_weight = [sum(weight_list[:i + 1]) / sum_weight for i in
                   range(len(weight_list))]  # get the cumulative sum of the thickness weight
    block_list = list(item[0] for item in struct)  # get the block name list

    # build terrain structure
    schema = MCSchematic()
    for row in range(mc_rows-1, -1, -1):                         # build from south to north
        for col in range(mc_cols):                               # build from west to east
            for elev in range(round(mc_data[row, col]), -1, -1):  # build from top to bottom by weight
                try:
                    build_process = (round(mc_data[row, col]) - elev + 1) / round(mc_data[row, col])
                except ZeroDivisionError:
                    continue
                for p in range(len(cumu_weight)):                # find the block by weight, p: index of block
                    if build_process <= cumu_weight[p]:
                        # in minecraft, x is the east-west direction, y is the up-down direction, z is the north-south direction
                        # in DEM, row is the north-south direction, col is the east-west direction, elev is the up-down direction
                        schema.setBlock((col, elev, row), block_list[p])
                        break
                    elif p == len(cumu_weight) - 1:   # build the bottom layer
                        schema.setBlock((col, elev, row), block_list[p])
                        break

    # parse noice list as a list of dict
    noise_json = literal_eval(noise)
    noise_matrices = []
    for noise in noise_json:            # noice is like {"block": "minecraft:sapling", "density": 0.005}
        # generate a random matrix, shape is the same as mc_data
        noise_matrix = random.rand(*mc_data.shape)
        noise_matrix[noise_matrix > noise['density']] = 0  # set the value greater than the density to 0
        noise_matrix[noise_matrix > 0] = 1                 # set the value not equal to 0 to 1
        noise_matrices.insert(0, (noise['block'], noise_matrix))  # insert the noice matrix to the front of the list

    # build noice structure at the top surface of the terrain
    # the noice structure before will cover the noice structure after from the parameter list
    for noise_item in noise_matrices:                          # build from the front to the back
        for row in range(mc_rows-1, -1, -1):                         # build from south to north (bottom to top)
            for col in range(mc_cols):                               # build from west to east (left to right)
                if noise_item[1][row, col] == 1:                      # if the noice matrix is 1, build the noice
                    schema.setBlock((col, round(mc_data[row, col]) + 1, row), noise_item[0])
                    break

    # save the schematic
    schema.save(dirname(schema_path), basename(schema_path).removesuffix('.schem'), Version[version])



def support_version():
    '''
    Get the supported version of the schematic
    '''
    for v in Version.__members__.keys():
        print(v)


def dem2schema_help():
    '''
    Get help information
    '''
    print(convert.__doc__)


if __name__ == '__main__':
    print(f'dem2schema v{__version__}')
    if len(argv) == 1:
        dem2schema_help()
    elif len(argv) == 2:
        if argv[1] == '--mc-versions' or argv[1] == '-v':
            support_version()
        elif argv[1] == '--help' or argv[1] == '-h':
            dem2schema_help()
        else:
            print('Error: invalid argument: %s' % argv[1])
            dem2schema_help()
    else:
        convert(*argv[1:])