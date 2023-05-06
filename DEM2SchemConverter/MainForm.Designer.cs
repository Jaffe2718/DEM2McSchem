namespace DEM2SchemExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonAddDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(56, 83);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(397, 582);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCControl1_OnDoubleClick);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(56, 28);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(397, 30);
            this.axToolbarControl1.TabIndex = 1;
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Location = new System.Drawing.Point(478, 28);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(858, 787);
            this.axSceneControl1.TabIndex = 2;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConvert.ForeColor = System.Drawing.Color.Blue;
            this.buttonConvert.Location = new System.Drawing.Point(56, 749);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(397, 66);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "Convert DEM to Minecraft Schematic (*.schem)";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(663, 537);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAdd.Location = new System.Drawing.Point(56, 677);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(186, 66);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add DEM File";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonAddDB
            // 
            this.buttonAddDB.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddDB.Location = new System.Drawing.Point(267, 677);
            this.buttonAddDB.Name = "buttonAddDB";
            this.buttonAddDB.Size = new System.Drawing.Size(186, 66);
            this.buttonAddDB.TabIndex = 6;
            this.buttonAddDB.Text = "Add DEM From Geodatabase";
            this.buttonAddDB.UseVisualStyleBackColor = true;
            this.buttonAddDB.Click += new System.EventHandler(this.buttonAddDB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1394, 861);
            this.Controls.Add(this.buttonAddDB);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.axSceneControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DEM to Minecraft Schematic Conventer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private System.Windows.Forms.Button buttonConvert;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        public ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonAddDB;
    }
}

