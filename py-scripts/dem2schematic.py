import ast

import numpy as np
import cv2
from osgeo import gdal
from ast import literal_eval  # parse string to dict
import mcschematic as mcs
import nbtlib
import os
import sys

__version__ = '1.2.1'

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


class SchematicFactory:
    '''
Usage:
    Help:
        dem2schematic.exe | dem2schematic.exe -h | dem2schematic.exe --help

    Show Supported Minecraft Versions:
        dem2schematic.exe -v | dem2schematic.exe --mc-versions

    Convert DEM data to minecraft schematic (*.schem):
        dem2schematic.exe <dem_path:str> <export_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>] [<noise=[]>]

        :param dem_path: DEM file path
        :param export_path: output schematic file path (include file name)
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
                    there are two types of noise structure, one is single block, the other is *.nbt file means mc-structure.
                    {'type': 'block', 'block': 'minecraft:<block_id>', 'density': <float: between 0 and 1>}
                    {'type': 'nbt', 'path': '<nbt_file_path>', 'density': <float: between 0 and 1>, 'offset': (dx, dy, dz), 'overwrite': <bool>, 'ignore_air': <bool>}
                    then add these two types of noise structure to the python list, like this:
                    [{...}, {...} ...]
                    The nbt type noice has higher priority.
                    The density of the noise item is the probability of the noise item appearing.
    '''

    schematic: mcs.MCSchematic
    dem: gdal.Dataset
    export_path: str
    version: mcs.Version
    mc_matrix: np.ndarray

    def __init__(self, dem_path: str,
                 export_path: str,
                 interpolation='INTER_LINEAR',
                 scale='1.0',
                 pavement_elevation='0.0',
                 version='JE_1_19_2',
                 stratum_struct='[("minecraft:grass_block",1)]',
                 noise='[]'):
        self.dem = gdal.Open(dem_path)
        self.export_path = export_path
        self.schematic = mcs.MCSchematic()
        self.version = mcs.Version[version]
        self.mc_matrix = self._convent_mc_matrix(self.dem, interpolation, float(scale), float(pavement_elevation))
        self._build_terrain(self.mc_matrix, stratum_struct)
        self._build_noise(noise)

    def _convent_mc_matrix(self, dem: gdal.Dataset, interpolation: str, scale: float, pavement_elevation: float):
        # get min value, max value, nodata value, spatial_resolution from the DEM raster
        dem_min, dem_max, dem_nodata = dem.GetRasterBand(1).GetMinimum(), \
            dem.GetRasterBand(1).GetMaximum(), dem.GetRasterBand(1).GetNoDataValue()
        dem_resolution = dem.GetGeoTransform()[1]
        # read DEM data as a matrix, if the DEM raster is multi-band, only the first band will be read
        dem_matrix = dem.ReadAsArray()
        # ignore the nodata value by setting it to the pavement elevation
        dem_matrix[dem_matrix == dem_nodata] = float(pavement_elevation)
        # re-calculate elevation, if the elevation is less than the pavement elevation, set it to the 0,
        # else set it as (elevation - pavement_elevation)
        dem_matrix = dem_matrix - float(pavement_elevation)  # subtract the pavement elevation
        dem_matrix[dem_matrix < 0] = 0  # set the negative value to 0
        # get the size of the DEM matrix
        dem_rows, dem_cols = dem_matrix.shape
        mc_rows = round(dem_rows * float(scale) * dem_resolution)  # number of rows in the schematic, y direction
        mc_cols = round(dem_cols * float(scale) * dem_resolution)  # number of columns in the schematic, x direction
        # scale the DEM matrix to the size of the schematic, what's more, the elevation will be scaled, all elevation are integer
        mc_matrix = np.rint(
            cv2.resize(dem_matrix, (mc_cols, mc_rows),
                       interpolation=dict_interpolation[interpolation])
            * float(scale)).astype(int)
        return mc_matrix

    def _build_terrain(self, mc_matrix: np.ndarray, stratum_struct: str):
        # parse stratum_struct list as a list of tuple
        struct = literal_eval(stratum_struct)
        weight_list = list(item[1] for item in struct)  # get the thickness weight list
        sum_weight = sum(weight_list)  # get the sum of the thickness weight
        cumu_weight = [sum(weight_list[:i + 1]) / sum_weight for i in
                       range(len(weight_list))]  # get the cumulative sum of the thickness weight
        block_list = list(item[0] for item in struct)  # get the block name list
        mc_rows, mc_cols = mc_matrix.shape  # get the size of the schematic matrix
        for row in range(mc_rows - 1, -1, -1):  # build from south to north
            for col in range(mc_cols):  # build from west to east
                for elev in range(mc_matrix[row, col], -1, -1):  # build from top to bottom by weight
                    try:
                        build_process = (mc_matrix[row, col] - elev + 1) / mc_matrix[row, col]
                    except ZeroDivisionError:
                        continue
                    for p in range(len(cumu_weight)):  # find the block by weight, p: index of block
                        if build_process <= cumu_weight[p]:
                            # in minecraft, x is the east-west direction, y is the up-down direction, z is the north-south direction
                            # in DEM, row is the north-south direction, col is the east-west direction, elev is the up-down direction
                            self.schematic.setBlock((col, elev, row), block_list[p])
                            break
                        elif p == len(cumu_weight) - 1:  # build the bottom layer
                            self.schematic.setBlock((col, elev, row), block_list[p])
                            break

    def _build_noise(self, noise: str):
        noise_layers = ast.literal_eval(noise.replace('\\', '/'))
        # divide the schematic into nbt and block noise layers
        nbt_noise_layers = list(filter(lambda x: x['type'] == 'nbt', noise_layers))
        block_noise_layers = list(filter(lambda x: x['type'] == 'block', noise_layers))
        # build nbt noise layers
        # step 1: build noise matrix, restore the nbt id
        nbt_id_matrix = np.zeros(shape=self.mc_matrix.shape, dtype=int)    # matrix to store the nbt id, 0 means no nbt
        id = len(nbt_noise_layers)
        for layer in nbt_noise_layers[::-1]:
            noise_matrix = np.random.rand(*self.mc_matrix.shape)
            noise_matrix[noise_matrix > layer['density']] = 0
            noise_matrix[noise_matrix != 0] = -1
            for row in range(self.mc_matrix.shape[0]):
                for col in range(self.mc_matrix.shape[1]):
                    if noise_matrix[row, col] == -1:
                        nbt_id_matrix[row, col] = id
            id -= 1
        # step 2: build nbt noise layers
        for row in range(nbt_id_matrix.shape[0]):
            for col in range(nbt_id_matrix.shape[1]):
                if nbt_id_matrix[row, col] != 0:
                    # nbt_dict : {'type': 'nbt', 'path': '<nbt_file_path>', 'density': <float: between 0 and 1>, 'offset': (dx, dy, dz), 'overwrite': <bool>, 'ignore_air': <bool>}
                    nbt_dict = nbt_noise_layers[nbt_id_matrix[row, col] - 1]  # get the nbt dict by id in nbt_id_matrix
                    nbt_file = nbtlib.nbt.load(nbt_dict.get('path'))  # load the nbt file
                    # set blocks, relative origin is (col, mc_matrix[row, col] + 1, row), offset is (dx, dy, dz),
                    # absolute origin is (col + dx, mc_matrix[row, col] + 1 + dy, row + dz), set all blocks in the nbt file
                    # check overwrite, ignore_air
                    palette = nbt_file['palette']
                    for block in nbt_file['blocks']:      # set each block in the nbt file  // testing
                        # build block data
                        block_palette = palette[block['state']]
                        block_data = block_palette['Name']
                        if 'Properties' in block_palette:
                            block_data += "["
                            for key, value in block_palette['Properties'].items():
                                block_data += f"{key}={value},"
                            block_data = block_data[:-1] + "]"
                        # set block
                        if nbt_dict.get('overwrite'):
                            if nbt_dict.get('ignore_air') and block['state'] == 0:  # ignore air block in nbt file
                                continue
                            else:  # force overwrite, set new block with block state(str)
                                self.schematic.setBlock((col + nbt_dict.get('offset')[0] + block['pos'][0],
                                                         self.mc_matrix[row, col] + 1 + nbt_dict.get('offset')[1] + block['pos'][1],
                                                         row + nbt_dict.get('offset')[2] + block['pos'][2]),
                                                        block_data)
                        else:             # not overwrite, set new block with block state(str)
                            if self.schematic.getBlockDataAt((col + nbt_dict.get('offset')[0] + block['pos'][0],
                                                              self.mc_matrix[row, col] + 1 + nbt_dict.get('offset')[1] + block['pos'][1],
                                                              row + nbt_dict.get('offset')[2] + block['pos'][2])) == 'minecraft:air':
                                self.schematic.setBlock((col + nbt_dict.get('offset')[0] + block['pos'][0],
                                                         self.mc_matrix[row, col] + 1 + nbt_dict.get('offset')[1] + block['pos'][1],
                                                         row + nbt_dict.get('offset')[2] + block['pos'][2]),
                                                        block_data)
        # build block noise layers
        for layer in block_noise_layers:
            block_id = layer['block']
            noise_matrix = np.random.rand(*self.mc_matrix.shape)
            noise_matrix[noise_matrix > layer['density']] = 0
            noise_matrix[noise_matrix != 0] = 1
            for row in range(self.mc_matrix.shape[0]):
                for col in range(self.mc_matrix.shape[1]):
                    if (noise_matrix[row, col] == 1 and
                            self.schematic.getBlockDataAt((col, self.mc_matrix[row, col] + 1, row)) == 'minecraft:air'):
                        self.schematic.setBlock((col, self.mc_matrix[row, col] + 1, row), block_id)

    def export(self):
        '''
        Export the schematic file to the given path
        '''
        dir_path = os.path.dirname(self.export_path)
        if not os.path.exists(dir_path):
            os.makedirs(dir_path)
        # get the schematic file name without extension
        schem_name = os.path.splitext(os.path.basename(self.export_path))[0]
        # save the schematic file
        self.schematic.save(dir_path, schem_name, self.version)

    @classmethod
    def supported_versions(cls):
        '''
        Show Supported Minecraft Versions:
        '''
        for version in mcs.Version.__members__.keys():
            print(version)


if __name__ == '__main__':
    # print(sys.argv)
    if len(sys.argv) == 1:
        print(SchematicFactory.__doc__)
    elif len(sys.argv) == 2:
        if sys.argv[1] == '-v' or sys.argv[1] == '--mc-versions':
            SchematicFactory.supported_versions()
        else:
            print(SchematicFactory.__doc__)
    else:
        factory = SchematicFactory(*sys.argv[1:])
        factory.export()