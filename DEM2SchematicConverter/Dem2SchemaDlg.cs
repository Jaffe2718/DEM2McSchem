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
            while ((layer = layers.Next()) != null)
            {
                comboBoxDEMLayer.Items.Add(layer.Name);
            }
            // set default value of combo box
            if (comboBoxDEMLayer.Items.Count > 0)
            {
                comboBoxDEMLayer.SelectedIndex = 0;
            }
            String tempDir = System.IO.Path.GetTempPath();
            textBoxExportPath.Text = System.IO.Path.Combine(tempDir, "DEM2Schema.schem");
            
            // set default value of combo box
            comboBoxInterpolation.SelectedIndex = 1;  // bi-linear
            comboBoxMcVersion.SelectedIndex = 0;      // JE_1_19_2
            
            // set default layout of list view
            listViewStratumStruct.Columns[0].Width = listViewStratumStruct.Width / 2;
            listViewStratumStruct.Columns[1].Width = listViewStratumStruct.Width / 2;
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
                return layers;
            }
            if (m_sceneHookHelper != null)
            {
                layers = m_sceneHookHelper.Scene.get_Layers(uid, true);
                return layers;
            }
            MessageBox.Show("No hook helper found!");
            throw new Exception("No hook helper found!");
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
        /// Second, get the source DEM file path of the selected DEM layer
        /// Third, generate the a python list format string param named stratum_struct by reading the listview
        /// Fourth, call the python script exe to convert DEM to Schematic, which is at the same directory of this dll
        /// Fifth, show a message box to tell the user the process is done
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            // check the input parameters
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
/////////////////////////////////////////////////////////////////////////// debug
            // get the source DEM data file of the selected DEM layer, comboBoxDEMLayer.SelectedItem is the name of the layer, it is a string
            // get the layer object of the selected DEM layer
            UID uid = new UIDClass();
            uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";  // the uid of the layer
            IEnumLayer enumLayer;
            if (m_hookHelper != null)
            { 
                enumLayer = m_hookHelper.FocusMap.Layers[uid, true];
            }
            else
            {
                enumLayer = m_sceneHookHelper.Scene.Layers[uid, true];
            }
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                if (layer.Name == comboBoxDEMLayer.SelectedItem.ToString())
                {
                    break;
                }
                layer = enumLayer.Next();
            }
            // get the source DEM data file of the selected DEM layer
            String demPath = ((IRasterLayer) layer).FilePath;
/////////////////////////////////////////////////////////////////////////// debug
            // generate a python list format string param named stratum_struct by reading the listview
            // [ ("minecraft:grass_block", 1), ("minecraft:dirt", 1), ("minecraft:stone", 1), ("minecraft:bedrock", 1) ]
            String stratumStruct = "[";
            foreach (ListViewItem item in listViewStratumStruct.Items)
            {
                stratumStruct += "('" + item.SubItems[0].Text + "', " + item.SubItems[1].Text + "), ";
            }
            stratumStruct = stratumStruct.Substring(0, stratumStruct.Length - 2) + "]";
            // call the python script exe to convert DEM to Schematic, which is at the same directory of this dll
            // get the path of the python script exe, which is at the same directory of this dll
            String exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\dem2schema.exe";
            // execute the python script exe
            // dem2schema.exe <dem_path:str> <schema_path:str> [<interpolation=INTER_NEAREST>] [<scale=1.0>] [<pavement_elevation=0.0>] [<version=JE_1_19_2>] [<stratum_struct=[("minecraft:grass_block",1)]>]
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = "\"" + demPath + "\" \"" + textBoxExportPath.Text + "\" " +
                                          comboBoxInterpolation.SelectedItem.ToString() + " " +
                                          textBoxScale.Text + " " +
                                          textBoxPavementEle.Text + " " +
                                          comboBoxMcVersion.SelectedItem.ToString() + " " +
                                          "\"" + stratumStruct + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = false;
            process.Start();
            process.WaitForExit();
            // show a message box to tell the user the process is done
            MessageBox.Show("Done!");
        }

        /// open https://github.com/Jaffe2718/DEM2McSchem/ in the default browser
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Jaffe2718/DEM2McSchem/");
        }
    }
}
