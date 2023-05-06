using System;
using System.Windows.Forms;
using DEM2SchematicConverter;
using DEM2SchemConverter;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Path = System.IO.Path;

namespace DEM2SchemExplorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            axSceneControl1.Navigate = true;
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            axSceneControl1.Scene.ClearLayers();
        }

        /// <summary>
        /// add a DEM raster to the AxSceneControl
        /// The DEM raster may be a file or a dataset
        /// call the add data tool to add the DEM raster to the scene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)     // event handler for the button "Add"
        {
            // create a new instance of the AddDataDialog
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;
            pOpenFileDialog.Title = "Open Raster File";
            pOpenFileDialog.Filter = "Raster files|*.tif;*.img;*.dat;*.bil;*.bsq;*.bip" +
                                     "|All files (*.*)|*.*";
            pOpenFileDialog.ShowDialog();
            // check if the file is existing
            if (!pOpenFileDialog.CheckFileExists)
            {
                MessageBox.Show("Error: File not existing!");
                return;
            }
            try    // check if the file is a DEM raster (single band)
            {
                IRasterDataset rasterDEM = new RasterDatasetClass();
                rasterDEM.OpenFromFile(pOpenFileDialog.FileName);
                IRasterBandCollection rasterBc = (IRasterBandCollection)rasterDEM;
                if (rasterBc.Count > 1)
                {
                    MessageBox.Show("Error: The raster is not a DEM raster!");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: The file is not a raster file!");
                return;
            }
            // add the DEM raster to the scene
            ILayer layer = new RasterLayerClass();
            ((IRasterLayer) layer).CreateFromFilePath(pOpenFileDialog.FileName);
            layer.Name = Path.GetFileNameWithoutExtension(pOpenFileDialog.FileName);
            AddDem3DLayer(layer);
        }

        private void buttonAddDB_Click(object sender, EventArgs e)
        {
            // create a new instance of the AddDataDialog
            IGxDialog gxDialog = new GxDialogClass();
            gxDialog.Title = "Select a Geodatabase";
            gxDialog.ButtonCaption = "Select";
            gxDialog.AllowMultiSelect = false;
            gxDialog.RememberLocation = true;
            IGxObjectFilterCollection gxFilterCollection = (IGxObjectFilterCollection)gxDialog;
            gxFilterCollection.RemoveAllFilters();
            IGxObjectFilter gxFilterRasterDs = new GxFilterRasterDatasetsClass();
            gxFilterCollection.AddFilter(gxFilterRasterDs, true);
            IEnumGxObject gxEnum;
            bool bOK = gxDialog.DoModalOpen(0, out gxEnum);
            if (bOK)
            {
                IGxObject gxObject = gxEnum.Next();
                if (gxObject != null)
                {
                    IRasterDataset rasterDEM = new RasterDatasetClass();
                    rasterDEM = (IRasterDataset)gxObject.InternalObjectName.Open();
                    IRasterBandCollection rasterBc = (IRasterBandCollection)rasterDEM;
                    if (rasterBc.Count > 1)
                    {
                        MessageBox.Show("Error: The raster is not a DEM raster!");
                        return;
                    }
                    ILayer layer = new RasterLayerClass();
                    ((IRasterLayer)layer).CreateFromDataset(rasterDEM);
                    layer.Name = gxObject.Name;
                    AddDem3DLayer(layer);
                }
            }
        }
        
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            new Dem2SchemaDlg(axSceneControl1).Show();
        }

        private void AddDem3DLayer(ILayer demLayer)              // tool function to add a DEM raster to the scene
        {
            axSceneControl1.SceneGraph.Scene.AddLayer(demLayer);
            I3DProperties prop3D = new Raster3DPropertiesClass();            // Create a new Raster3DProperties object
            ILayerExtensions layerEx = demLayer as ILayerExtensions;
            for (int i = layerEx.ExtensionCount-1; i>=0; i--)
            {
                if (layerEx.Extension[i] != null)
                {
                    prop3D = layerEx.Extension[i] as I3DProperties;
                    break;
                }
            }
            prop3D.BaseOption = esriBaseOption.esriBaseSurface;             // Set the BaseOption property to esriBaseSurface
            IRasterSurface rSurface = new RasterSurface();                   // Create a new RasterSurface object
            rSurface.PutRaster(((IRasterLayer) demLayer).Raster, 0);            // Set the Raster property to the RasterLayer's Raster
            prop3D.BaseSurface = rSurface as IFunctionalSurface;            // Set the BaseSurface property to the RasterSurface
            prop3D.ZFactor = 1;
            prop3D.ExtrusionType = esriExtrusionType.esriExtrusionBase;
            prop3D.Apply3DProperties(demLayer);
            axSceneControl1.SceneGraph.RefreshViewers();
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            if (e.button == 1)
            {
                // get clicked layer
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
                // ensure the item gets selected
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    axTOCControl1.SelectItem(layer, null);
                    new ViewMetaDlg(axSceneControl1, layer).Show();
                }
            }
        }
    }
}