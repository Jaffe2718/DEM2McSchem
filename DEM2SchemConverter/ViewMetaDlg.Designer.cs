namespace DEM2SchemConverter
{
    partial class ViewMetaDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMetaDlg));
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDrop = new System.Windows.Forms.Button();
            this.listViewMeta = new System.Windows.Forms.ListView();
            this.cItem = new System.Windows.Forms.ColumnHeader();
            this.cProperties = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(683, 421);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(105, 39);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonDrop
            // 
            this.buttonDrop.ForeColor = System.Drawing.Color.Maroon;
            this.buttonDrop.Location = new System.Drawing.Point(542, 421);
            this.buttonDrop.Name = "buttonDrop";
            this.buttonDrop.Size = new System.Drawing.Size(126, 39);
            this.buttonDrop.TabIndex = 2;
            this.buttonDrop.Text = "Drop Layer";
            this.buttonDrop.UseVisualStyleBackColor = true;
            this.buttonDrop.Click += new System.EventHandler(this.buttonDrop_Click);
            // 
            // listViewMeta
            // 
            this.listViewMeta.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.cItem, this.cProperties });
            this.listViewMeta.GridLines = true;
            this.listViewMeta.HideSelection = false;
            this.listViewMeta.Location = new System.Drawing.Point(12, 12);
            this.listViewMeta.Name = "listViewMeta";
            this.listViewMeta.Size = new System.Drawing.Size(776, 403);
            this.listViewMeta.TabIndex = 3;
            this.listViewMeta.UseCompatibleStateImageBehavior = false;
            this.listViewMeta.View = System.Windows.Forms.View.Details;
            // 
            // cItem
            // 
            this.cItem.Text = "Item";
            this.cItem.Width = 175;
            // 
            // cProperties
            // 
            this.cProperties.Text = "Properties";
            this.cProperties.Width = 596;
            // 
            // ViewMetaDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 463);
            this.Controls.Add(this.listViewMeta);
            this.Controls.Add(this.buttonDrop);
            this.Controls.Add(this.buttonExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewMetaDlg";
            this.Text = "Layer Properties";
            this.Load += new System.EventHandler(this.ViewMetaDlg_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ColumnHeader cItem;
        private System.Windows.Forms.ColumnHeader cProperties;

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDrop;

        #endregion

        private System.Windows.Forms.ListView listViewMeta;
    }
}