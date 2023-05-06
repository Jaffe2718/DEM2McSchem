namespace DEM2SchematicConverter
{
    partial class Dem2SchemaDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dem2SchemaDlg));
            this.comboBoxDEMLayer = new System.Windows.Forms.ComboBox();
            this.textBoxExportPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExportPath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxInterpolation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMcVersion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPavementEle = new System.Windows.Forms.TextBox();
            this.listViewStratumStruct = new System.Windows.Forms.ListView();
            this.columnHeaderBlock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonAddStruct = new System.Windows.Forms.Button();
            this.buttonDropStruct = new System.Windows.Forms.Button();
            this.buttonWeightIncr = new System.Windows.Forms.Button();
            this.buttonWeightDecr = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.listViewNoise = new System.Windows.Forms.ListView();
            this.columnHeaderNoise = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddNoise = new System.Windows.Forms.Button();
            this.buttonDropNoise = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxDEMLayer
            // 
            this.comboBoxDEMLayer.FormattingEnabled = true;
            this.comboBoxDEMLayer.Location = new System.Drawing.Point(209, 23);
            this.comboBoxDEMLayer.Name = "comboBoxDEMLayer";
            this.comboBoxDEMLayer.Size = new System.Drawing.Size(552, 26);
            this.comboBoxDEMLayer.TabIndex = 0;
            // 
            // textBoxExportPath
            // 
            this.textBoxExportPath.Location = new System.Drawing.Point(252, 78);
            this.textBoxExportPath.Name = "textBoxExportPath";
            this.textBoxExportPath.Size = new System.Drawing.Size(432, 28);
            this.textBoxExportPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select DEM Layer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Export Schematic Path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonExportPath
            // 
            this.buttonExportPath.Location = new System.Drawing.Point(701, 76);
            this.buttonExportPath.Name = "buttonExportPath";
            this.buttonExportPath.Size = new System.Drawing.Size(60, 28);
            this.buttonExportPath.TabIndex = 6;
            this.buttonExportPath.Text = "...";
            this.buttonExportPath.UseVisualStyleBackColor = true;
            this.buttonExportPath.Click += new System.EventHandler(this.buttonExportPath_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Interpolation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxInterpolation
            // 
            this.comboBoxInterpolation.FormattingEnabled = true;
            this.comboBoxInterpolation.Items.AddRange(new object[] {
            "INTER_NEAREST",
            "INTER_LINEAR",
            "INTER_AREA",
            "INTER_CUBIC",
            "INTER_LANCZOS4",
            "INTER_LINEAR_EXACT"});
            this.comboBoxInterpolation.Location = new System.Drawing.Point(209, 139);
            this.comboBoxInterpolation.Name = "comboBoxInterpolation";
            this.comboBoxInterpolation.Size = new System.Drawing.Size(193, 26);
            this.comboBoxInterpolation.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(428, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Scale";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(625, 139);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(136, 28);
            this.textBoxScale.TabIndex = 10;
            this.textBoxScale.Text = "1.0";
            this.textBoxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 32);
            this.label5.TabIndex = 11;
            this.label5.Text = "Minecraft Version";
            // 
            // comboBoxMcVersion
            // 
            this.comboBoxMcVersion.FormattingEnabled = true;
            this.comboBoxMcVersion.Items.AddRange(new object[] {
            "JE_1_19_2",
            "JE_1_19_2_RELEASE_CANDIDATE_2",
            "JE_1_19_2_RELEASE_CANDIDATE_1",
            "JE_1_19_1",
            "JE_1_19_1_RELEASE_CANDIDATE_3",
            "JE_1_19_1_RELEASE_CANDIDATE_2",
            "JE_1_19_1_PRE_RELEASE_6",
            "JE_1_19_1_PRE_RELEASE_5",
            "JE_1_19_1_PRE_RELEASE_4",
            "JE_1_19_1_PRE_RELEASE_3",
            "JE_1_19_1_PRE_RELEASE_2",
            "JE_1_19_1_RELEASE_CANDIDATE_1",
            "JE_1_19_1_PRE_RELEASE_1",
            "JE_22W24A",
            "JE_1_19",
            "JE_1_19_RELEASE_CANDIDATE_2",
            "JE_1_19_RELEASE_CANDIDATE_1",
            "JE_1_19_PRE_RELEASE_5",
            "JE_1_19_PRE_RELEASE_4",
            "JE_1_19_PRE_RELEASE_3",
            "JE_1_19_PRE_RELEASE_2",
            "JE_1_19_PRE_RELEASE_1",
            "JE_22W19A",
            "JE_22W18A",
            "JE_22W17A",
            "JE_22W16B",
            "JE_22W16A",
            "JE_22W15A",
            "JE_22W14A",
            "JE_22W13A",
            "JE_22W12A",
            "JE_22W11A",
            "JE_DEEP_DARK_EXPERIMENTAL_SNAPSHOT_1",
            "JE_1_18_2",
            "JE_1_18_2_RELEASE_CANDIDATE_1",
            "JE_1_18_2_PRE_RELEASE_3",
            "JE_1_18_2_PRE_RELEASE_2",
            "JE_1_18_2_PRE_RELEASE_1",
            "JE_22W07A",
            "JE_22W06A",
            "JE_22W05A",
            "JE_22W03A",
            "JE_1_18_1",
            "JE_1_18_1_RELEASE_CANDIDATE_3",
            "JE_1_18_1_RELEASE_CANDIDATE_2",
            "JE_1_18_1_RELEASE_CANDIDATE_1",
            "JE_1_18_1_PRE_RELEASE_1",
            "JE_1_18",
            "JE_1_18_RELEASE_CANDIDATE_4",
            "JE_1_18_RELEASE_CANDIDATE_3",
            "JE_1_18_RELEASE_CANDIDATE_2",
            "JE_1_18_RELEASE_CANDIDATE_1",
            "JE_1_18_PRE_RELEASE_8",
            "JE_1_18_PRE_RELEASE_7",
            "JE_1_18_PRE_RELEASE_6",
            "JE_1_18_PRE_RELEASE_5",
            "JE_1_18_PRE_RELEASE_4",
            "JE_1_18_PRE_RELEASE_3",
            "JE_1_18_PRE_RELEASE_2",
            "JE_1_18_PRE_RELEASE_1",
            "JE_21W44A",
            "JE_21W43A",
            "JE_21W42A",
            "JE_21W41A",
            "JE_21W40A",
            "JE_21W39A",
            "JE_21W38A",
            "JE_21W37A",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_7",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_6",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_5",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_4",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_3",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_2",
            "JE_1_18_EXPERIMENTAL_SNAPSHOT_1",
            "JE_1_17_1",
            "JE_1_17_1_RELEASE_CANDIDATE_2",
            "JE_1_17_1_RELEASE_CANDIDATE_1",
            "JE_1_17_1_PRE_RELEASE_3",
            "JE_1_17_1_PRE_RELEASE_2",
            "JE_1_17_1_PRE_RELEASE_1",
            "JE_1_17",
            "JE_1_17_RELEASE_CANDIDATE_2",
            "JE_1_17_RELEASE_CANDIDATE_1",
            "JE_1_17_PRE_RELEASE_5",
            "JE_1_17_PRE_RELEASE_4",
            "JE_1_17_PRE_RELEASE_3",
            "JE_1_17_PRE_RELEASE_2",
            "JE_1_17_PRE_RELEASE_1",
            "JE_21W20A",
            "JE_21W19A",
            "JE_21W18A",
            "JE_21W17A",
            "JE_21W16A",
            "JE_21W15A",
            "JE_21W14A",
            "JE_21W13A",
            "JE_21W11A",
            "JE_21W10A",
            "JE_21W08B",
            "JE_21W08A",
            "JE_21W07A",
            "JE_21W06A",
            "JE_21W05B",
            "JE_21W05A",
            "JE_21W03A",
            "JE_20W51A",
            "JE_20W49A",
            "JE_20W48A",
            "JE_20W46A",
            "JE_20W45A",
            "JE_COMBAT_TEST_8C",
            "JE_COMBAT_TEST_8B",
            "JE_COMBAT_TEST_8",
            "JE_COMBAT_TEST_7C",
            "JE_COMBAT_TEST_7B",
            "JE_COMBAT_TEST_7",
            "JE_COMBAT_TEST_6",
            "JE_1_16_5",
            "JE_1_16_5_RELEASE_CANDIDATE_1",
            "JE_1_16_4",
            "JE_1_16_4_RELEASE_CANDIDATE_1",
            "JE_1_16_4_PRE_RELEASE_2",
            "JE_1_16_4_PRE_RELEASE_1",
            "JE_1_16_3",
            "JE_1_16_3_RELEASE_CANDIDATE_1",
            "JE_1_16_2",
            "JE_1_16_2_RELEASE_CANDIDATE_2",
            "JE_1_16_2_RELEASE_CANDIDATE_1",
            "JE_1_16_2_PRE_RELEASE_3",
            "JE_1_16_2_PRE_RELEASE_2",
            "JE_1_16_2_PRE_RELEASE_1",
            "JE_20W30A",
            "JE_20W29A",
            "JE_20W28A",
            "JE_20W27A",
            "JE_1_16_1",
            "JE_1_16",
            "JE_1_16_RELEASE_CANDIDATE_1",
            "JE_1_16_PRE_RELEASE_8",
            "JE_1_16_PRE_RELEASE_7",
            "JE_1_16_PRE_RELEASE_6",
            "JE_1_16_PRE_RELEASE_5",
            "JE_1_16_PRE_RELEASE_4",
            "JE_1_16_PRE_RELEASE_3",
            "JE_1_16_PRE_RELEASE_2",
            "JE_1_16_PRE_RELEASE_1",
            "JE_20W22A",
            "JE_20W21A",
            "JE_20W20B",
            "JE_20W20A",
            "JE_20W19A",
            "JE_20W18A",
            "JE_20W17A",
            "JE_20W16A",
            "JE_20W15A",
            "JE_20W14A",
            "JE_20W13B",
            "JE_20W13A",
            "JE_20W12A",
            "JE_20W11A",
            "JE_20W10A",
            "JE_20W09A",
            "JE_20W08A",
            "JE_20W07A",
            "JE_SNAPSHOT_20W06A",
            "JE_COMBAT_TEST_5",
            "JE_COMBAT_TEST_4",
            "JE_1_15_2",
            "JE_1_15_2_PRE_RELEASE_2",
            "JE_1_15_2_PRE_RELEASE_1",
            "JE_1_15_1",
            "JE_1_15_1_PRE_RELEASE_1",
            "JE_1_15",
            "JE_1_15_PRE_RELEASE_7",
            "JE_1_15_PRE_RELEASE_6",
            "JE_1_15_PRE_RELEASE_5",
            "JE_1_15_PRE_RELEASE_4",
            "JE_1_15_PRE_RELEASE_3",
            "JE_1_15_PRE_RELEASE_2",
            "JE_1_15_PRE_RELEASE_1",
            "JE_19W46B",
            "JE_19W46A",
            "JE_19W45B",
            "JE_19W45A",
            "JE_19W44A",
            "JE_19W42A",
            "JE_19W41A",
            "JE_19W40A",
            "JE_19W39A",
            "JE_19W38B",
            "JE_19W38A",
            "JE_19W37A",
            "JE_19W36A",
            "JE_19W35A",
            "JE_19W34A",
            "JE_COMBAT_TEST_3",
            "JE_COMBAT_TEST_2",
            "JE_1_14_3___COMBAT_TEST",
            "JE_1_14_4",
            "JE_1_14_4_PRE_RELEASE_7",
            "JE_1_14_4_PRE_RELEASE_6",
            "JE_1_14_4_PRE_RELEASE_5",
            "JE_1_14_4_PRE_RELEASE_4",
            "JE_1_14_4_PRE_RELEASE_3",
            "JE_1_14_4_PRE_RELEASE_2",
            "JE_1_14_4_PRE_RELEASE_1",
            "JE_1_14_3",
            "JE_1_14_3_PRE_RELEASE_4",
            "JE_1_14_3_PRE_RELEASE_3",
            "JE_1_14_3_PRE_RELEASE_2",
            "JE_1_14_3_PRE_RELEASE_1",
            "JE_1_14_2",
            "JE_1_14_2_PRE_RELEASE_4",
            "JE_1_14_2_PRE_RELEASE_3",
            "JE_1_14_2_PRE_RELEASE_2",
            "JE_1_14_2_PRE_RELEASE_1",
            "JE_1_14_1",
            "JE_1_14_1_PRE_RELEASE_2",
            "JE_1_14_1_PRE_RELEASE_1",
            "JE_1_14",
            "JE_1_14_PRE_RELEASE_5",
            "JE_1_14_PRE_RELEASE_4",
            "JE_1_14_PRE_RELEASE_3",
            "JE_1_14_PRE_RELEASE_2",
            "JE_1_14_PRE_RELEASE_1",
            "JE_19W14B",
            "JE_19W14A",
            "JE_19W13B",
            "JE_19W13A",
            "JE_19W12B",
            "JE_19W12A",
            "JE_19W11B",
            "JE_19W11A",
            "JE_19W09A",
            "JE_19W08B",
            "JE_19W08A",
            "JE_19W07A",
            "JE_19W06A",
            "JE_19W05A",
            "JE_19W04B",
            "JE_19W04A",
            "JE_19W03C",
            "JE_19W03B",
            "JE_19W03A",
            "JE_19W02A",
            "JE_18W50A",
            "JE_18W49A",
            "JE_18W48B",
            "JE_18W48A",
            "JE_18W47B",
            "JE_18W47A",
            "JE_18W46A",
            "JE_18W45A",
            "JE_18W44A",
            "JE_18W43C",
            "JE_18W43B",
            "JE_18W43A",
            "JE_1_13_2",
            "JE_1_13_2_PRE2",
            "JE_1_13_2_PRE1",
            "JE_1_13_1",
            "JE_1_13_1_PRE2",
            "JE_1_13_1_PRE1",
            "JE_18W33A",
            "JE_18W32A",
            "JE_18W31A",
            "JE_18W30B",
            "JE_18W30A",
            "JE_1_13",
            "JE_1_13_PRE10",
            "JE_1_13_PRE9",
            "JE_1_13_PRE8",
            "JE_1_13_PRE7",
            "JE_1_13_PRE6",
            "JE_1_13_PRE5",
            "JE_1_13_PRE4",
            "JE_1_13_PRE3",
            "JE_1_13_PRE2",
            "JE_1_13_PRE1",
            "JE_18W22C",
            "JE_18W22B",
            "JE_18W22A",
            "JE_18W21B",
            "JE_18W21A",
            "JE_18W20C",
            "JE_18W20B",
            "JE_18W20A",
            "JE_18W19B",
            "JE_18W19A",
            "JE_18W16A",
            "JE_18W15A",
            "JE_18W14B",
            "JE_18W14A",
            "JE_18W11A",
            "JE_18W10D",
            "JE_18W10C",
            "JE_18W10B",
            "JE_18W10A",
            "JE_18W09A",
            "JE_18W08B",
            "JE_18W08A",
            "JE_18W07C",
            "JE_18W07B",
            "JE_18W07A",
            "JE_18W06A",
            "JE_18W05A",
            "JE_18W03B",
            "JE_18W03A",
            "JE_18W02A",
            "JE_18W01A",
            "JE_17W50A",
            "JE_17W49B",
            "JE_17W49A",
            "JE_17W48A",
            "JE_17W47B",
            "JE_17W47A",
            "JE_17W46A",
            "JE_17W45B",
            "JE_17W45A",
            "JE_17W43B",
            "JE_17W43A",
            "JE_1_12_2",
            "JE_1_12_2_PRE2",
            "JE_1_12_2_PRE1",
            "JE_1_12_1",
            "JE_1_12_1_PRE1",
            "JE_17W31A",
            "JE_1_12",
            "JE_1_12_PRE7",
            "JE_1_12_PRE6",
            "JE_1_12_PRE5",
            "JE_1_12_PRE4",
            "JE_1_12_PRE3",
            "JE_1_12_PRE2",
            "JE_1_12_PRE1",
            "JE_17W18B",
            "JE_17W18A",
            "JE_17W17B",
            "JE_17W17A",
            "JE_17W16B",
            "JE_17W16A",
            "JE_17W15A",
            "JE_17W14A",
            "JE_17W13B",
            "JE_17W13A",
            "JE_17W06A",
            "JE_1_11_2",
            "JE_1_11_1",
            "JE_16W50A",
            "JE_1_11",
            "JE_1_11_PRE1",
            "JE_16W44A",
            "JE_16W43A",
            "JE_16W42A",
            "JE_16W41A",
            "JE_16W40A",
            "JE_16W39C",
            "JE_16W39B",
            "JE_16W39A",
            "JE_16W38A",
            "JE_16W36A",
            "JE_16W35A",
            "JE_16W33A",
            "JE_16W32B",
            "JE_16W32A",
            "JE_1_10_2",
            "JE_1_10_1",
            "JE_1_10",
            "JE_1_10_PRE2",
            "JE_1_10_PRE1",
            "JE_16W21B",
            "JE_16W21A",
            "JE_16W20A",
            "JE_1_9_4",
            "JE_1_9_3",
            "JE_1_9_3_PRE3",
            "JE_1_9_3_PRE2",
            "JE_1_9_3_PRE1",
            "JE_16W15B",
            "JE_16W15A",
            "JE_16W14A",
            "JE_1_9_2",
            "JE_1_9_1",
            "JE_1_9_1_PRE3",
            "JE_1_9_1_PRE2",
            "JE_1_9_1_PRE1",
            "JE_1_9",
            "JE_1_9_PRE4",
            "JE_1_9_PRE3",
            "JE_1_9_PRE2",
            "JE_1_9_PRE1",
            "JE_16W07B",
            "JE_16W07A",
            "JE_16W06A",
            "JE_16W05B",
            "JE_16W05A",
            "JE_16W04A",
            "JE_16W03A",
            "JE_16W02A",
            "JE_15W51B",
            "JE_15W51A",
            "JE_15W50A",
            "JE_15W49B",
            "JE_15W49A",
            "JE_15W47C",
            "JE_15W47B",
            "JE_15W47A",
            "JE_15W46A",
            "JE_15W45A",
            "JE_15W44B",
            "JE_15W44A",
            "JE_15W43C",
            "JE_15W43B",
            "JE_15W43A",
            "JE_15W42A",
            "JE_15W41B",
            "JE_15W41A",
            "JE_15W40B",
            "JE_15W40A",
            "JE_15W39C",
            "JE_15W39B",
            "JE_15W39A",
            "JE_15W38B",
            "JE_15W38A",
            "JE_15W37A",
            "JE_15W36D",
            "JE_15W36C",
            "JE_15W36B",
            "JE_15W36A",
            "JE_15W35E",
            "JE_15W35D",
            "JE_15W35C",
            "JE_15W35B",
            "JE_15W35A",
            "JE_15W34D",
            "JE_15W34C",
            "JE_15W34B",
            "JE_15W34A",
            "JE_15W33C",
            "JE_15W33B",
            "JE_15W33A",
            "JE_15W32C",
            "JE_15W32B",
            "JE_15W32A"});
            this.comboBoxMcVersion.Location = new System.Drawing.Point(209, 196);
            this.comboBoxMcVersion.Name = "comboBoxMcVersion";
            this.comboBoxMcVersion.Size = new System.Drawing.Size(193, 26);
            this.comboBoxMcVersion.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(428, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 30);
            this.label6.TabIndex = 13;
            this.label6.Text = "Pavement Elevation";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPavementEle
            // 
            this.textBoxPavementEle.Location = new System.Drawing.Point(625, 193);
            this.textBoxPavementEle.Name = "textBoxPavementEle";
            this.textBoxPavementEle.Size = new System.Drawing.Size(136, 28);
            this.textBoxPavementEle.TabIndex = 14;
            this.textBoxPavementEle.Text = "0.0";
            this.textBoxPavementEle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // listViewStratumStruct
            // 
            this.listViewStratumStruct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBlock,
            this.columnHeaderWeight});
            this.listViewStratumStruct.GridLines = true;
            this.listViewStratumStruct.HideSelection = false;
            this.listViewStratumStruct.LabelEdit = true;
            this.listViewStratumStruct.LabelWrap = false;
            this.listViewStratumStruct.Location = new System.Drawing.Point(24, 292);
            this.listViewStratumStruct.Name = "listViewStratumStruct";
            this.listViewStratumStruct.Size = new System.Drawing.Size(696, 163);
            this.listViewStratumStruct.TabIndex = 15;
            this.listViewStratumStruct.UseCompatibleStateImageBehavior = false;
            this.listViewStratumStruct.View = System.Windows.Forms.View.Details;
            this.listViewStratumStruct.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewStratumStruct_MouseDoubleClick);
            // 
            // columnHeaderBlock
            // 
            this.columnHeaderBlock.Text = "Block";
            this.columnHeaderBlock.Width = 322;
            // 
            // columnHeaderWeight
            // 
            this.columnHeaderWeight.Text = "Weight";
            this.columnHeaderWeight.Width = 362;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(328, 38);
            this.label7.TabIndex = 16;
            this.label7.Text = "Minecraft Stratum Structure Design";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.buttonExecute.ForeColor = System.Drawing.Color.Navy;
            this.buttonExecute.Location = new System.Drawing.Point(631, 695);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(130, 34);
            this.buttonExecute.TabIndex = 17;
            this.buttonExecute.Text = "Convert";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonHelp.Location = new System.Drawing.Point(24, 695);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(130, 34);
            this.buttonHelp.TabIndex = 18;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonAddStruct
            // 
            this.buttonAddStruct.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddStruct.Location = new System.Drawing.Point(564, 251);
            this.buttonAddStruct.Name = "buttonAddStruct";
            this.buttonAddStruct.Size = new System.Drawing.Size(38, 38);
            this.buttonAddStruct.TabIndex = 19;
            this.buttonAddStruct.Text = "+";
            this.buttonAddStruct.UseVisualStyleBackColor = true;
            this.buttonAddStruct.Click += new System.EventHandler(this.buttonAddStruct_Click);
            // 
            // buttonDropStruct
            // 
            this.buttonDropStruct.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDropStruct.Location = new System.Drawing.Point(646, 251);
            this.buttonDropStruct.Name = "buttonDropStruct";
            this.buttonDropStruct.Size = new System.Drawing.Size(38, 38);
            this.buttonDropStruct.TabIndex = 20;
            this.buttonDropStruct.Text = "-";
            this.buttonDropStruct.UseVisualStyleBackColor = true;
            this.buttonDropStruct.Click += new System.EventHandler(this.buttonDropStruct_Click);
            // 
            // buttonWeightIncr
            // 
            this.buttonWeightIncr.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonWeightIncr.Location = new System.Drawing.Point(723, 292);
            this.buttonWeightIncr.Name = "buttonWeightIncr";
            this.buttonWeightIncr.Size = new System.Drawing.Size(38, 38);
            this.buttonWeightIncr.TabIndex = 21;
            this.buttonWeightIncr.Text = "⋀⋁";
            this.buttonWeightIncr.UseVisualStyleBackColor = true;
            this.buttonWeightIncr.Click += new System.EventHandler(this.buttonWeightIncr_Click);
            // 
            // buttonWeightDecr
            // 
            this.buttonWeightDecr.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonWeightDecr.Location = new System.Drawing.Point(723, 417);
            this.buttonWeightDecr.Name = "buttonWeightDecr";
            this.buttonWeightDecr.Size = new System.Drawing.Size(38, 38);
            this.buttonWeightDecr.TabIndex = 22;
            this.buttonWeightDecr.Text = "⋁";
            this.buttonWeightDecr.UseVisualStyleBackColor = true;
            this.buttonWeightDecr.Click += new System.EventHandler(this.buttonWeightDecr_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 476);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(428, 38);
            this.label8.TabIndex = 23;
            this.label8.Text = "Minecraft Surface Random Noise Block Design";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewNoise
            // 
            this.listViewNoise.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNoise});
            this.listViewNoise.GridLines = true;
            this.listViewNoise.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewNoise.HideSelection = false;
            this.listViewNoise.LabelEdit = true;
            this.listViewNoise.LabelWrap = false;
            this.listViewNoise.Location = new System.Drawing.Point(24, 517);
            this.listViewNoise.Name = "listViewNoise";
            this.listViewNoise.Size = new System.Drawing.Size(696, 161);
            this.listViewNoise.TabIndex = 24;
            this.listViewNoise.UseCompatibleStateImageBehavior = false;
            this.listViewNoise.View = System.Windows.Forms.View.Details;
            this.listViewNoise.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewNoise_MouseDoubleClick);
            // 
            // columnHeaderNoise
            // 
            this.columnHeaderNoise.Text = "Noise Block & Density ({\'block\': \'minecraft:<id>\', \'density\': <float>})";
            this.columnHeaderNoise.Width = 689;
            // 
            // buttonAddNoise
            // 
            this.buttonAddNoise.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddNoise.Location = new System.Drawing.Point(564, 476);
            this.buttonAddNoise.Name = "buttonAddNoise";
            this.buttonAddNoise.Size = new System.Drawing.Size(38, 38);
            this.buttonAddNoise.TabIndex = 25;
            this.buttonAddNoise.Text = "+";
            this.buttonAddNoise.UseVisualStyleBackColor = true;
            this.buttonAddNoise.Click += new System.EventHandler(this.buttonAddNoise_Click);
            // 
            // buttonDropNoise
            // 
            this.buttonDropNoise.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDropNoise.Location = new System.Drawing.Point(646, 476);
            this.buttonDropNoise.Name = "buttonDropNoise";
            this.buttonDropNoise.Size = new System.Drawing.Size(38, 38);
            this.buttonDropNoise.TabIndex = 26;
            this.buttonDropNoise.Text = "-";
            this.buttonDropNoise.UseVisualStyleBackColor = true;
            this.buttonDropNoise.Click += new System.EventHandler(this.buttonDropNoise_Click);
            // 
            // Dem2SchemaDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(788, 748);
            this.Controls.Add(this.buttonDropNoise);
            this.Controls.Add(this.buttonAddNoise);
            this.Controls.Add(this.listViewNoise);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonWeightDecr);
            this.Controls.Add(this.buttonWeightIncr);
            this.Controls.Add(this.buttonDropStruct);
            this.Controls.Add(this.buttonAddStruct);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listViewStratumStruct);
            this.Controls.Add(this.textBoxPavementEle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxMcVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxScale);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxInterpolation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonExportPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxExportPath);
            this.Controls.Add(this.comboBoxDEMLayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Dem2SchemaDlg";
            this.Text = "DEM to Minecraft Schematic Converter";
            this.Load += new System.EventHandler(this.Dem2SchemaDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ColumnHeader columnHeaderNoise;

        private System.Windows.Forms.Button buttonAddNoise;
        private System.Windows.Forms.Button buttonDropNoise;

        private System.Windows.Forms.ListView listViewNoise;

        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.ColumnHeader columnHeaderBlock;
        private System.Windows.Forms.ColumnHeader columnHeaderWeight;

        private System.Windows.Forms.Button buttonWeightIncr;
        private System.Windows.Forms.Button buttonWeightDecr;

        private System.Windows.Forms.Button buttonAddStruct;
        private System.Windows.Forms.Button buttonDropStruct;

        private System.Windows.Forms.Button buttonHelp;

        private System.Windows.Forms.Button buttonExecute;

        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.ListView listViewStratumStruct;

        private System.Windows.Forms.TextBox textBoxPavementEle;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMcVersion;

        private System.Windows.Forms.TextBox textBoxScale;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxInterpolation;

        private System.Windows.Forms.Button buttonExportPath;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox textBoxExportPath;

        private System.Windows.Forms.ComboBox comboBoxDEMLayer;

        #endregion
    }
}