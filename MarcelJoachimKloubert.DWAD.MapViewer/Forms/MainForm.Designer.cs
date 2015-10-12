namespace MarcelJoachimKloubert.DWAD.MapViewer.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBox_File = new System.Windows.Forms.GroupBox();
            this.SplitContainer_File = new System.Windows.Forms.SplitContainer();
            this.TextBox_WADFile = new System.Windows.Forms.TextBox();
            this.Button_SelectWADFile = new System.Windows.Forms.Button();
            this.GroupBox_Map = new System.Windows.Forms.GroupBox();
            this.SplitContainer_Map = new System.Windows.Forms.SplitContainer();
            this.PictureBox_Map = new System.Windows.Forms.PictureBox();
            this.NumericUpDown_OffsetY = new System.Windows.Forms.NumericUpDown();
            this.Label_MapOffsetY = new System.Windows.Forms.Label();
            this.NumericUpDown_MapScale = new System.Windows.Forms.NumericUpDown();
            this.Label_MapScale = new System.Windows.Forms.Label();
            this.NumericUpDown_OffsetX = new System.Windows.Forms.NumericUpDown();
            this.Label_MapOffsetX = new System.Windows.Forms.Label();
            this.GroupBox_File.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_File)).BeginInit();
            this.SplitContainer_File.Panel1.SuspendLayout();
            this.SplitContainer_File.Panel2.SuspendLayout();
            this.SplitContainer_File.SuspendLayout();
            this.GroupBox_Map.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Map)).BeginInit();
            this.SplitContainer_Map.Panel1.SuspendLayout();
            this.SplitContainer_Map.Panel2.SuspendLayout();
            this.SplitContainer_Map.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Map)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_OffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MapScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_OffsetX)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox_File
            // 
            this.GroupBox_File.Controls.Add(this.SplitContainer_File);
            this.GroupBox_File.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBox_File.Location = new System.Drawing.Point(0, 0);
            this.GroupBox_File.Name = "GroupBox_File";
            this.GroupBox_File.Size = new System.Drawing.Size(1022, 41);
            this.GroupBox_File.TabIndex = 0;
            this.GroupBox_File.TabStop = false;
            this.GroupBox_File.Text = "File";
            // 
            // SplitContainer_File
            // 
            this.SplitContainer_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_File.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer_File.Location = new System.Drawing.Point(3, 16);
            this.SplitContainer_File.Name = "SplitContainer_File";
            // 
            // SplitContainer_File.Panel1
            // 
            this.SplitContainer_File.Panel1.Controls.Add(this.TextBox_WADFile);
            // 
            // SplitContainer_File.Panel2
            // 
            this.SplitContainer_File.Panel2.Controls.Add(this.Button_SelectWADFile);
            this.SplitContainer_File.Size = new System.Drawing.Size(1016, 22);
            this.SplitContainer_File.SplitterDistance = 901;
            this.SplitContainer_File.TabIndex = 0;
            // 
            // TextBox_WADFile
            // 
            this.TextBox_WADFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_WADFile.Location = new System.Drawing.Point(0, 0);
            this.TextBox_WADFile.Name = "TextBox_WADFile";
            this.TextBox_WADFile.ReadOnly = true;
            this.TextBox_WADFile.Size = new System.Drawing.Size(901, 20);
            this.TextBox_WADFile.TabIndex = 0;
            this.TextBox_WADFile.TextChanged += new System.EventHandler(this.TextBox_WADFile_TextChanged);
            // 
            // Button_SelectWADFile
            // 
            this.Button_SelectWADFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_SelectWADFile.Location = new System.Drawing.Point(0, 0);
            this.Button_SelectWADFile.Name = "Button_SelectWADFile";
            this.Button_SelectWADFile.Size = new System.Drawing.Size(111, 22);
            this.Button_SelectWADFile.TabIndex = 0;
            this.Button_SelectWADFile.Text = "...";
            this.Button_SelectWADFile.UseVisualStyleBackColor = true;
            this.Button_SelectWADFile.Click += new System.EventHandler(this.Button_SelectWADFile_Click);
            // 
            // GroupBox_Map
            // 
            this.GroupBox_Map.Controls.Add(this.SplitContainer_Map);
            this.GroupBox_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Map.Location = new System.Drawing.Point(0, 41);
            this.GroupBox_Map.Name = "GroupBox_Map";
            this.GroupBox_Map.Size = new System.Drawing.Size(1022, 622);
            this.GroupBox_Map.TabIndex = 1;
            this.GroupBox_Map.TabStop = false;
            // 
            // SplitContainer_Map
            // 
            this.SplitContainer_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_Map.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer_Map.Location = new System.Drawing.Point(3, 16);
            this.SplitContainer_Map.Name = "SplitContainer_Map";
            // 
            // SplitContainer_Map.Panel1
            // 
            this.SplitContainer_Map.Panel1.Controls.Add(this.PictureBox_Map);
            // 
            // SplitContainer_Map.Panel2
            // 
            this.SplitContainer_Map.Panel2.Controls.Add(this.NumericUpDown_OffsetY);
            this.SplitContainer_Map.Panel2.Controls.Add(this.Label_MapOffsetY);
            this.SplitContainer_Map.Panel2.Controls.Add(this.NumericUpDown_MapScale);
            this.SplitContainer_Map.Panel2.Controls.Add(this.Label_MapScale);
            this.SplitContainer_Map.Panel2.Controls.Add(this.NumericUpDown_OffsetX);
            this.SplitContainer_Map.Panel2.Controls.Add(this.Label_MapOffsetX);
            this.SplitContainer_Map.Size = new System.Drawing.Size(1016, 603);
            this.SplitContainer_Map.SplitterDistance = 839;
            this.SplitContainer_Map.TabIndex = 0;
            // 
            // PictureBox_Map
            // 
            this.PictureBox_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox_Map.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Map.Name = "PictureBox_Map";
            this.PictureBox_Map.Size = new System.Drawing.Size(839, 603);
            this.PictureBox_Map.TabIndex = 1;
            this.PictureBox_Map.TabStop = false;
            // 
            // NumericUpDown_OffsetY
            // 
            this.NumericUpDown_OffsetY.Location = new System.Drawing.Point(15, 67);
            this.NumericUpDown_OffsetY.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NumericUpDown_OffsetY.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.NumericUpDown_OffsetY.Name = "NumericUpDown_OffsetY";
            this.NumericUpDown_OffsetY.Size = new System.Drawing.Size(149, 20);
            this.NumericUpDown_OffsetY.TabIndex = 5;
            this.NumericUpDown_OffsetY.ValueChanged += new System.EventHandler(this.NumericUpDown_OffsetY_ValueChanged);
            // 
            // Label_MapOffsetY
            // 
            this.Label_MapOffsetY.AutoSize = true;
            this.Label_MapOffsetY.Location = new System.Drawing.Point(12, 51);
            this.Label_MapOffsetY.Name = "Label_MapOffsetY";
            this.Label_MapOffsetY.Size = new System.Drawing.Size(54, 13);
            this.Label_MapOffsetY.TabIndex = 4;
            this.Label_MapOffsetY.Text = "Offset (Y):";
            // 
            // NumericUpDown_MapScale
            // 
            this.NumericUpDown_MapScale.Location = new System.Drawing.Point(15, 106);
            this.NumericUpDown_MapScale.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumericUpDown_MapScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_MapScale.Name = "NumericUpDown_MapScale";
            this.NumericUpDown_MapScale.Size = new System.Drawing.Size(149, 20);
            this.NumericUpDown_MapScale.TabIndex = 3;
            this.NumericUpDown_MapScale.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumericUpDown_MapScale.ValueChanged += new System.EventHandler(this.NumericUpDown_MapScale_ValueChanged);
            // 
            // Label_MapScale
            // 
            this.Label_MapScale.AutoSize = true;
            this.Label_MapScale.Location = new System.Drawing.Point(12, 90);
            this.Label_MapScale.Name = "Label_MapScale";
            this.Label_MapScale.Size = new System.Drawing.Size(60, 13);
            this.Label_MapScale.TabIndex = 2;
            this.Label_MapScale.Text = "Scale (1:x):";
            // 
            // NumericUpDown_OffsetX
            // 
            this.NumericUpDown_OffsetX.Location = new System.Drawing.Point(15, 28);
            this.NumericUpDown_OffsetX.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NumericUpDown_OffsetX.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.NumericUpDown_OffsetX.Name = "NumericUpDown_OffsetX";
            this.NumericUpDown_OffsetX.Size = new System.Drawing.Size(149, 20);
            this.NumericUpDown_OffsetX.TabIndex = 1;
            this.NumericUpDown_OffsetX.ValueChanged += new System.EventHandler(this.NumericUpDown_OffsetX_ValueChanged);
            // 
            // Label_MapOffsetX
            // 
            this.Label_MapOffsetX.AutoSize = true;
            this.Label_MapOffsetX.Location = new System.Drawing.Point(12, 12);
            this.Label_MapOffsetX.Name = "Label_MapOffsetX";
            this.Label_MapOffsetX.Size = new System.Drawing.Size(54, 13);
            this.Label_MapOffsetX.TabIndex = 0;
            this.Label_MapOffsetX.Text = "Offset (X):";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 663);
            this.Controls.Add(this.GroupBox_Map);
            this.Controls.Add(this.GroupBox_File);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dwad MapViewer";
            this.GroupBox_File.ResumeLayout(false);
            this.SplitContainer_File.Panel1.ResumeLayout(false);
            this.SplitContainer_File.Panel1.PerformLayout();
            this.SplitContainer_File.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_File)).EndInit();
            this.SplitContainer_File.ResumeLayout(false);
            this.GroupBox_Map.ResumeLayout(false);
            this.SplitContainer_Map.Panel1.ResumeLayout(false);
            this.SplitContainer_Map.Panel2.ResumeLayout(false);
            this.SplitContainer_Map.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Map)).EndInit();
            this.SplitContainer_Map.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Map)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_OffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_MapScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_OffsetX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_File;
        private System.Windows.Forms.GroupBox GroupBox_Map;
        private System.Windows.Forms.SplitContainer SplitContainer_File;
        private System.Windows.Forms.TextBox TextBox_WADFile;
        private System.Windows.Forms.Button Button_SelectWADFile;
        private System.Windows.Forms.SplitContainer SplitContainer_Map;
        private System.Windows.Forms.PictureBox PictureBox_Map;
        private System.Windows.Forms.Label Label_MapOffsetX;
        private System.Windows.Forms.NumericUpDown NumericUpDown_OffsetX;
        private System.Windows.Forms.NumericUpDown NumericUpDown_MapScale;
        private System.Windows.Forms.Label Label_MapScale;
        private System.Windows.Forms.NumericUpDown NumericUpDown_OffsetY;
        private System.Windows.Forms.Label Label_MapOffsetY;

    }
}

