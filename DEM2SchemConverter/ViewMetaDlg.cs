using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace DEM2SchemConverter
{
    public partial class ViewMetaDlg : Form
    {
        private AxSceneControl targetSceneControl;
        private IRasterLayer targetLayer;
        
        public ViewMetaDlg(AxSceneControl targetSc, ILayer targetLy)
        {
            InitializeComponent();
            targetSceneControl = targetSc;
            targetLayer = (IRasterLayer)targetLy;
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
            // show a message box to confirm
            DialogResult result = MessageBox.Show("Are you sure to drop the selected layer?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // remove the layer from the scene
                targetSceneControl.Scene.DeleteLayer(targetLayer);
                // close the dialog
                Close();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewMetaDlg_Load(object sender, EventArgs e)
        {
            // always show the layer name
            Text += " - " + targetLayer.Name;
            // add the layer information to the list view
            ListViewItem item = new ListViewItem("Name");
            item.SubItems.Add(targetLayer.Name);
            listViewMeta.Items.Add(item);
            // type
            item = new ListViewItem("Type");
            item.SubItems.Add(targetLayer.GetType().ToString());
            listViewMeta.Items.Add(item);
            // source
            item = new ListViewItem("Source");
            item.SubItems.Add(targetLayer.FilePath);
            listViewMeta.Items.Add(item);
            if (targetLayer is IGeoDataset geoDataset)
            {
                // coordinate system
                item = new ListViewItem("Coordinate System");
                item.SubItems.Add(geoDataset.SpatialReference.Name);
                listViewMeta.Items.Add(item);
            }
            var rasterProps = (IRasterProps)targetLayer.Raster;
            // unit
            if (rasterProps.SpatialReference is IGeographicCoordinateSystem)
            {
                item = new ListViewItem("Unit");
                item.SubItems.Add("Degree");
                listViewMeta.Items.Add(item);
            }
            else if (rasterProps.SpatialReference is IProjectedCoordinateSystem)
            {
                item = new ListViewItem("Unit");
                IProjectedCoordinateSystem projectedCS = rasterProps.SpatialReference as IProjectedCoordinateSystem;
                ILinearUnit linearUnit = projectedCS.CoordinateUnit;
                item.SubItems.Add(linearUnit.Name);
                listViewMeta.Items.Add(item);
            }
            else
            {
                item = new ListViewItem("Unit");
                item.SubItems.Add("Unknown");
                listViewMeta.Items.Add(item);
            }
            // spatial resolution
            item = new ListViewItem("Spatial Resolution");
            item.SubItems.Add($"X: {rasterProps.MeanCellSize().X}, Y: {rasterProps.MeanCellSize().Y}");
            listViewMeta.Items.Add(item);
            // pixel type
            item = new ListViewItem("Pixel Type");
            item.SubItems.Add(rasterProps.PixelType.ToString());
            listViewMeta.Items.Add(item);
            // row count
            item = new ListViewItem("Row Count");
            item.SubItems.Add(targetLayer.RowCount.ToString());
            listViewMeta.Items.Add(item);
            // column count
            item = new ListViewItem("Column Count");
            item.SubItems.Add(targetLayer.ColumnCount.ToString());
            listViewMeta.Items.Add(item);
            // band count
            item = new ListViewItem("Band Count");
            item.SubItems.Add(targetLayer.BandCount.ToString());
            listViewMeta.Items.Add(item);
            // spatial range
            item = new ListViewItem("Spatial Range");
            item.SubItems.Add($"XMin: {rasterProps.Extent.XMin}, XMax: {rasterProps.Extent.XMax}, YMin: {rasterProps.Extent.YMin}, YMax: {rasterProps.Extent.YMax}");
            listViewMeta.Items.Add(item);
            // always show on top
            TopMost = true;
        }
    }
}
