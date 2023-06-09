# DEM to Minecraft Schematic Converter Documentation

by [Jaffe2718](https://github.com/Jaffe2718)

![Stars](https://img.shields.io/github/stars/Jaffe2718/DEM2McSchem?style=flat-square)
![Forks](https://img.shields.io/github/forks/Jaffe2718/DEM2McSchem?style=flat-square)
![Issues](https://img.shields.io/github/issues/Jaffe2718/DEM2McSchem?style=flat-square)
![License](https://img.shields.io/github/license/Jaffe2718/DEM2McSchem?style=flat-square)

## Preface
作者是湖南师范大学地理科学学院2021级地理信息科学专业的在读本科生，邮箱是[202130163019@hunnu.edu.cn](mailto:202130163019@hunnu.edu.cn)。
这其实是组件式地理信息系统开发课程的项目作业，但是我觉得这个项目还是挺有意思的，所以就把它放到了GitHub上。
The author is a 2021 undergraduate student majoring in Geographic Information Science at the School of Geography, Hunan Normal University, the email is [202130163019@hunnu.edu.cn](mailto:202130163019@hunnu.edu.cn).
This is the project assignment of the course "Component-based GIS Development", but I think this project is interesting, so I put it on GitHub.

## User Documentation

### Product Description
There are two kinds of products in this repository:
a command line tool and an ArcGIS plugin. 
The command line tool `dem2schema.exe` is is an executable file packaged and compiled by Python scripts through `Pyinstaller`.

The ArcGIS plugin is a C# Windows Forms plugin which can be used in ArcMap and ArcScene.
The plugin's core unit is the same as the command line tool, which is `dem2schema.exe`.

But the executable file `dem2schema.exe` is different command line tool and ArcGIS plugin.
For the command line tool, it is a command line tool which can be used in Windows command line.
But for the ArcGIS plugin, it is headless, which means it can run without any console window message.

### Command Line Tool

#### Installation
It does not need to be installed. Just download the executable file `dem2schema.exe` and put it in a folder.
You can add it's path into the system environment variable `PATH` to make it available in any folder.

#### Usage
It can convert a DEM file to a Minecraft schematic file. But Esri File Geo-database DEM is not supported.
If you add the path of `dem2schema.exe` into the system environment variable `PATH`, you can use it in any folder.

For help, you can use the following command:
```bash
dem2schema.exe -h
```

```bash
dem2schema.exe --help
```

If you want to get the supported Minecraft version, you can use the following command:
```bash
dem2schema.exe -v
```

```bash
dem2schema.exe --mc-versions
```

Then if you want to convert a DEM file to a Minecraft schematic file, you can use the following command:
```bash
dem2schema.exe <dem_path:str> <schema_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>]
```
```plaintext
Convert a DEM file to a Minecraft schematic file
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
        :param pavement_elevation: elevation of pavement, default is 0.0
        :param version: version of the schematic, default is JE_1_19_2
        :param stratum_struct: python list of stratum structure, default is [("minecraft:grass_block",1)],
                    all the elements are tuple, the first element is the block name, the second element is the thickness weight.
                    For example, [("minecraft:grass_block",1),("minecraft:dirt",3),("minecraft:stone",5)], which means
                    the first layer is grass block, the second layer is dirt, the third layer is stone, and the thickness
                    of these blocks are block:dirt:stone = 1:3:5
```
There are is an example:
```bash
dem2schema.exe "E:\Coding\python\dem2schema\dem\circle.bil" "E:\Coding\python\dem2schema\schem\YueluMountain2.schem" INTER_LINEAR 0.1 0.0 JE_1_19_2 "[('minecraft:grass_block', 1), ('minecraft:dirt', 3), ('minecraft:stone', 6)]"
```
You should replace the DEM file path and the output schematic file path with your own path, and adjsut the scale, pavement elevation, version and stratum structure if you want.

### ArcGIS Plugin

#### Installation
1. You can download the plugin from the [release page](https://github.com/Jaffe2718/DEM2McSchem/releases) and ArcGIS 10.8 at first.
2. Unzip all the files in the zip file, and put them in a folder.
3. Launch ArcMap or ArcScene.
4. Right click on the toolbar, and then click `Customize` in the context menu.
![Customize](doc/Customize_ctxmenu.png)
5. Click `Commands` in the `Customize` dialog.
6. Click `Add from File` in the dialog.
![Add from File](doc/addCmdDlg.png)
7. Select the `DEM2SchematicConverter.tbx` file in the folder you unziped in step 2, and click `Open`.
![Select tbx](doc/addFromFile.png)
8. Click `OK` in the `Add Commands` dialog.
9. Then find the command `DEM to Minecraft Schematic Converter` in the category `Minecraft Tools`,
and drag it to the toolbar.
![Add Commands](doc/addTool.png)
10. Click `Close` in the `Customize` dialog.

#### Usage

1. Create a Minecraft schematic file from a DEM file in ArcMap or ArcScene.
![use](doc/use.png)
The user can use the plugin in ArcMap and ArcScene.
Warning: The raster data must have a spatial reference system,
and DEM data in Esri File Geo-database is not supported. And the DEM data must be a single band raster, nodata value mast less than -1e5

2. you can load the Minecraft schematic file into Minecraft by using the [WorldEdit](https://www.curseforge.com/minecraft/mc-mods/worldedit) mod.
![load](doc/mc.png)


### For 1.1.0

#### What's New
1. Now command line tool can automatically handle nodata value.
You can use the following command to convert a DEM file to a Minecraft schematic file:
```bash
dem2schema.exe <dem_path:str> <schema_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>] [<noise=[]>]
```
```plaintext
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
```
2. In the ArcGIS plugin, the users can use random noise to generate blocks on the Minecraft surface.
The format for noise configuration is python dick like this:
```plaintext
{'block': 'minecraft:<block_id>', 'density': <float: between 0 and 1>}
```
The higher the priority of the configuration item, the higher the priority,
That is to say, if two random noise generated blocks overlap at a certain location on the Minecraft surface, the higher priority block will cover the lower priority block
![noise](doc/noise.png)
![noice-view](doc/noise_view.png)

### For 1.2.x

#### Command Line Tool
##### Update Command Syntax
Now, random noise distribution supports block and [NBT File](https://minecraft.fandom.com/wiki/Structure_Block_file_format) structures
```plaintext
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
```
#### ArcGIS Plugin
##### What's New
1. DEM Raster Dataset in Geo-Database is supported.
2. Noise Feature syntax change:
```plaintext
{'type': 'block', 'block': 'minecraft:<block_id>', 'density': <float: between 0 and 1>}
{'type': 'nbt', 'path': '<nbt_file_path>', 'density': <float: between 0 and 1>, 'offset': (dx, dy, dz), 'overwrite': <bool>, 'ignore_air': <bool>}
```
So Minecraft structures of NBT File are supported.
## Developer Documentation

### Command Line Tool

1. Use Conda virtual environment to develop the command line tool.
The recommended way to develop the command line tool is to use conda virtual environment.
You should just install the dependencies for the command line tool in the virtual environment.
If you do not use conda virtual environment, the executable file `dem2schema.exe` created by Pyinstaller will be very large.
```bash
conda create -n dem2schema python=3.9
conda activate dem2schema
conda install -c conda-forge gdal
conda install -c conda-forge pyinstaller
conda install numpy
conda install opencv-python
conda install mcschematic
```

2. Use Pyinstaller to create the executable file `dem2schema.exe`.
```bash
# create the executable file as a command line tool
pyinstaller -F -i "path/to/your/icon.ico" dem2schematic.py
```
### ArcGIS Plugin
1. You need to install `ArcGIS 10.8`, `Visual Studio 2017`, `.NET Framework 4.7.2`, and `ArcGIS SDK for .NET 10.8`.
2. Clone the repository.
```bash
git clone https://github.com/Jaffe2718/DEM2McSchem.git
```
3. Open the solution file `DEM2McSchem.sln` in Visual Studio 2017 and start to develop.
4. If you want to release the plugin, you should do the following steps:
> 1. Open the solution file `DEM2McSchem.sln` in Visual Studio 2017 with Administrator privilege.
> 2. Build the solution in `Release` mode.
> 3. Create Python script core tool for this plugin:
> ```bash
> pyinstaller -F -w -i "path/to/your/icon.ico" dem2schematic.py
> ```
> Then copy the `dem2schema.exe` file built by Pyinstaller to the `bin/Release` folder of the plugin.
> 4. Zip the entire folder `bin/Release` of the plugin, you can rename the zip file to any name you want.
> 5. Then you can publish your zip file.

## Source Repository
The source repository is [here](https://github.com/Jaffe2718/DEM2McSchem).

## Contact
[Issues](https://github.com/Jaffe2718/DEM2McSchem/issues)

[Bilibili](https://space.bilibili.com/1671742926)

[Email](mailto:202130163019@hunnu.edu.cn)
