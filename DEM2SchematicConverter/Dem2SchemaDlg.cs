using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using Message = System.Windows.Forms.Message;

namespace DEM2SchematicConverter
{
    public partial class Dem2SchemaDlg : Form
    {
        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        private IHookHelper m_hookHelper;
        private ISceneHookHelper m_sceneHookHelper;

        private const uint WM_VSCROLL = 0x115;
        private const uint SB_BOTTOM = 7;

        private bool mc_block = true;

        public Dem2SchemaDlg(IHookHelper hookHelper)
        {
            InitializeComponent();
            m_hookHelper = hookHelper;
        }
        
        public Dem2SchemaDlg(ISceneHookHelper hookHelper)
        {
            InitializeComponent();
            m_sceneHookHelper = hookHelper;
        }

        private void Dem2SchemaDlg_Load(object sender, EventArgs e)
        {
            // get single band raster by GetDEMLayers()
            IEnumLayer layers = GetDemLayers();
            layers.Reset();
            ILayer layer;
            // add layer name to combo box
            while ((layer = layers.Next()) != null)
            {   // only add raster
                if (layer is IRasterLayer)
                {
                    comboBoxDEMLayer.Items.Add(layer.Name);
                }
            }
            // set default value of combo box
            if (comboBoxDEMLayer.Items.Count > 0)
            {
                comboBoxDEMLayer.SelectedIndex = 0;
            }
            String tempDir = Path.GetTempPath();
            textBoxExportPath.Text = System.IO.Path.Combine(tempDir, "DEM2Schema.schem");
            
            // set default value of combo box
            comboBoxInterpolation.SelectedIndex = 1;  // bi-linear
            comboBoxMcVersion.SelectedIndex = 0;      // JE_1_19_2
            
            // set default layout of list view
            listViewStratumStruct.Columns[0].Width = listViewStratumStruct.Width / 2;
            listViewStratumStruct.Columns[1].Width = listViewStratumStruct.Width / 2;
            // MessageBox.Show(Size.Width.ToString() + " " + Size.Height.ToString());
            // MessageBox.Show(ClientSize.ToString());
        }
        
        private IEnumLayer GetDemLayers()
        {
            UID uid = new UIDClass();
            // get single band raster
            uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";
            IEnumLayer layers;
            if (m_hookHelper != null)
            {
                layers = m_hookHelper.FocusMap.Layers[uid];
            }
            else
            {
                layers = m_sceneHookHelper.Scene.Layers[uid, true];
            }
            
            return layers;
        }

        private string GetStratumStruct()   // get the struct of stratum: python list of bi-tuple [ ("block", weight), ("block", weight), ... ]
        {
            string stratumStruct = "\"[";
            foreach (ListViewItem item in listViewStratumStruct.Items)
            {
                stratumStruct += "('" + item.Text + "'," + item.SubItems[1].Text + "), ";
            }
            stratumStruct = stratumStruct.Substring(0, stratumStruct.Length - 2) + "]\"";
            return stratumStruct;
        }
        
        private string GetNoise()   // get the noise of stratum: python list of dict [ {...}, {...}, ... ], item.Text is dict
        {
            string noise = "\"[";
            foreach (ListViewItem item in listViewNoise.Items)
            {
                noise += item.Text + ", ";
            }
            noise = noise.Substring(0, noise.Length - 2) + "]\"";
            return noise;
        }

        /// <summary>
        /// create a dialog to save new schematic file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportPath_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Schematic File (*.schem)|*.schem";
            fileDialog.Title = "Save As New Schematic File";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxExportPath.Text = fileDialog.FileName;
            }
        }

        /// <summary>
        /// add a new row of struct to the listview
        /// format = | block | weight |
        /// add default value to the new row like | minecraft:stone | 1 |
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddStruct_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(new []{"minecraft:stone", "1"});
            listViewStratumStruct.Items.Add(item);
            
            // scroll to the bottom of the listview
            PostMessage(listViewStratumStruct.Handle, WM_VSCROLL, (int)SB_BOTTOM, 0);
        }

        /// <summary>
        /// delete the selected struct from the listview, if no struct is selected, delete the last one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonDropStruct_Click(object sender, EventArgs e)
        {
            if (listViewStratumStruct.SelectedItems.Count > 0)
            {
                listViewStratumStruct.SelectedItems[0].Remove();
            }
            else if (listViewStratumStruct.Items.Count > 0)
            {
                listViewStratumStruct.Items[listViewStratumStruct.Items.Count - 1].Remove();
            }
        }
        
        /// <summary>
        /// Edit the block name of the selected struct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewStratumStruct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // edit the the block name selected struct
            if (listViewStratumStruct.SelectedItems.Count > 0 && e.Button == MouseButtons.Left)
            {
                listViewStratumStruct.SelectedItems[0].BeginEdit();
            }
        }


        private void buttonWeightIncr_Click(object sender, EventArgs e)
        {
            // increment the weight of the selected struct
            if (listViewStratumStruct.SelectedItems.Count > 0)
            {
                int weight = int.Parse(listViewStratumStruct.SelectedItems[0].SubItems[1].Text);
                listViewStratumStruct.SelectedItems[0].SubItems[1].Text = (weight + 1).ToString();
            }
        }

        private void buttonWeightDecr_Click(object sender, EventArgs e)
        {
            // decrement the weight of the selected struct
            if (listViewStratumStruct.SelectedItems.Count > 0)
            {
                int weight = int.Parse(listViewStratumStruct.SelectedItems[0].SubItems[1].Text);
                listViewStratumStruct.SelectedItems[0].SubItems[1].Text = (weight - 1 > 1 ? weight-1 : 1).ToString();
            }
        }

        /// <summary>
        /// To Convert DEM to Schematic
        /// First, check the input parameters
        /// Second, create a new folder named "__rCache__" in the dst folder, means "Raster Cache"
        /// Third, copy the DEM raster to the "__rCache__" folder, use "工具箱\系统工具箱\Data Management Tools.tbx\栅格\栅格数据集\复制栅格" tool, named as the same as the schema file, extension is ".bil"
        /// Fourth, call python script to convert copied DEM to Schematic
        /// Fifth, delete the "rasterCache" folder
        /// Finally, show a message box to tell the user the process is done
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            // 1. check the input parameters
            if (comboBoxDEMLayer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a DEM layer!");
                return;
            }
            if (textBoxExportPath.Text == "")
            {
                MessageBox.Show("Please select a path to save the new schematic file!");
                return;
            }
            if (listViewStratumStruct.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one struct!");
                return;
            }
            if (comboBoxInterpolation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a interpolation method!");
                return;
            }
            if (comboBoxMcVersion.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Minecraft version!");
                return;
            }
            // 2. create a new folder named "__rCache__" in the dst folder
            string dstFolder = Path.GetDirectoryName(textBoxExportPath.Text);   // get the folder of the dst schematic file
            if (dstFolder != null)
            {
                string rasterCacheFolder = Path.Combine(dstFolder, "__rCache__");      // create a new folder named "__rCache__" in the dst folder
                if (Directory.Exists(rasterCacheFolder))
                {
                    Directory.Delete(rasterCacheFolder, true);
                }
                Directory.CreateDirectory(rasterCacheFolder);                       // create the folder
                // 3. copy the DEM raster to the "__rCache__" folder
                ILayer srcLayer;                                                   // the layer of the DEM raster
                IEnumLayer enumLayer = GetDemLayers();                             // get all the DEM layers
                enumLayer.Reset();                                                 // reset the enumLayer
                while ((srcLayer = enumLayer.Next()) != null)
                {   // only add raster
                    if (srcLayer is IRasterLayer && srcLayer.Name == comboBoxDEMLayer.SelectedItem.ToString())
                    {
                        break;
                    }
                }
                string dstRasterPath = Path.Combine(rasterCacheFolder, Path.GetFileNameWithoutExtension(textBoxExportPath.Text) + ".bil"); // located the new raster file named as the same as the schema file, extension is ".bil"
                Geoprocessor gp = new Geoprocessor();                              // create a new Geoprocessor
                gp.OverwriteOutput = true;                                         // set the overwrite option to true
                gp.AddOutputsToMap = false;                                        // don't add the output to the map
                ESRI.ArcGIS.DataManagementTools.CopyRaster copyRaster = new ESRI.ArcGIS.DataManagementTools.CopyRaster(); // create a new "CopyRaster" tool
                copyRaster.in_raster = srcLayer;                                   // set the input raster
                copyRaster.out_rasterdataset = dstRasterPath;                      // set the output DEM raster
                gp.Execute(copyRaster, null);                            // execute the tool
                // 4. call python script to convert copied DEM to Schematic
                Process pyProcess = new Process();                                 // create a new process
                // 1.2.x named dem2schematic.exe, before 1.2.0 named dem2schema.exe
                // dem2schematic.exe <dem_path:str> <schema_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>] [<noise=[]>]
                String exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\dem2schematic.exe";         // get the path of the executable file, at the same folder of the dll
                pyProcess.StartInfo.FileName = exePath;                            // set the executable file
                pyProcess.StartInfo.Arguments = "\"" + dstRasterPath + "\" " +
                                                "\"" + textBoxExportPath.Text + "\" " +
                                                comboBoxInterpolation.SelectedItem + " " +
                                                textBoxScale.Text + " " +
                                                textBoxPavementEle.Text + " " +
                                                comboBoxMcVersion.SelectedItem + " " +
                                                GetStratumStruct() + " " +
                                                GetNoise();                                     // set the arguments
                pyProcess.StartInfo.UseShellExecute = false;                      // don't use the shell execute
                pyProcess.StartInfo.RedirectStandardOutput = true;                // redirect the standard output
                pyProcess.StartInfo.RedirectStandardError = true;                 // redirect the standard error
                pyProcess.StartInfo.CreateNoWindow = true;                        // don't create a new window
                pyProcess.Start();                                                // start the process
                pyProcess.WaitForExit();                                          // wait for the process to exit
                // 5. delete the "rasterCache" folder and all the files in it
                Directory.Delete(rasterCacheFolder, true);                        // delete the "rasterCache" folder and all the files in it
            }

            // 6. show a message box to tell the user the process is done
            MessageBox.Show("Export \"" + textBoxExportPath.Text + "\" successfully!");
        }

        /// open https://github.com/Jaffe2718/DEM2McSchem/ in the default browser
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Jaffe2718/DEM2McSchem#for-120");
        }

        private void buttonAddNoise_Click(object sender, EventArgs e)
        {
            // add a new item to the listview noise, and start edit it
            if (mc_block)
            {
                listViewNoise.Items.Add(new ListViewItem("{'type': 'block', 'block': 'minecraft:<block_id>', 'density': <float: between 0 and 1>}"));
                mc_block = false;
            }
            else
            {
                listViewNoise.Items.Add(new ListViewItem("{'type': 'nbt', 'path': '<nbt_file_path>', 'density': <float: between 0 and 1>, 'offset': (dx, dy, dz), 'overwrite': <bool>, 'ignore_air': <bool>}"));
                mc_block = true;
            }
            listViewNoise.Items[listViewNoise.Items.Count - 1].BeginEdit();
        }

        private void buttonDropNoise_Click(object sender, EventArgs e)
        {
            // remove the selected item from the listview noise, if nothing is selected, remove the last item
            if (listViewNoise.SelectedItems.Count > 0)
            {
                listViewNoise.Items.Remove(listViewNoise.SelectedItems[0]);       // remove the selected item
            }
            else if (listViewNoise.Items.Count > 0)
            {
                listViewNoise.Items.RemoveAt(listViewNoise.Items.Count - 1);  // remove the last item
            }
        }

        private void listViewNoise_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // start edit the selected item
            if (listViewNoise.SelectedItems.Count > 0)
            {
                listViewNoise.SelectedItems[0].BeginEdit();
            }
        }
    }
}
