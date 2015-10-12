namespace MarcelJoachimKloubert.DWAD.LumpViewer.Forms
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
            this.SplitContainer_LumpDetails = new System.Windows.Forms.SplitContainer();
            this.ListView_Lumps = new System.Windows.Forms.ListView();
            this.ColumnHeader_Lumps_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RichTextBox_LumpDetails = new System.Windows.Forms.RichTextBox();
            this.GroupBox_File.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_File)).BeginInit();
            this.SplitContainer_File.Panel1.SuspendLayout();
            this.SplitContainer_File.Panel2.SuspendLayout();
            this.SplitContainer_File.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_LumpDetails)).BeginInit();
            this.SplitContainer_LumpDetails.Panel1.SuspendLayout();
            this.SplitContainer_LumpDetails.Panel2.SuspendLayout();
            this.SplitContainer_LumpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_File
            // 
            this.GroupBox_File.Controls.Add(this.SplitContainer_File);
            this.GroupBox_File.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBox_File.Location = new System.Drawing.Point(0, 0);
            this.GroupBox_File.Name = "GroupBox_File";
            this.GroupBox_File.Size = new System.Drawing.Size(949, 41);
            this.GroupBox_File.TabIndex = 1;
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
            this.SplitContainer_File.Size = new System.Drawing.Size(943, 22);
            this.SplitContainer_File.SplitterDistance = 828;
            this.SplitContainer_File.TabIndex = 0;
            // 
            // TextBox_WADFile
            // 
            this.TextBox_WADFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_WADFile.Location = new System.Drawing.Point(0, 0);
            this.TextBox_WADFile.Name = "TextBox_WADFile";
            this.TextBox_WADFile.ReadOnly = true;
            this.TextBox_WADFile.Size = new System.Drawing.Size(828, 20);
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
            // SplitContainer_LumpDetails
            // 
            this.SplitContainer_LumpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_LumpDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer_LumpDetails.Location = new System.Drawing.Point(0, 41);
            this.SplitContainer_LumpDetails.Name = "SplitContainer_LumpDetails";
            // 
            // SplitContainer_LumpDetails.Panel1
            // 
            this.SplitContainer_LumpDetails.Panel1.Controls.Add(this.ListView_Lumps);
            // 
            // SplitContainer_LumpDetails.Panel2
            // 
            this.SplitContainer_LumpDetails.Panel2.Controls.Add(this.RichTextBox_LumpDetails);
            this.SplitContainer_LumpDetails.Size = new System.Drawing.Size(949, 437);
            this.SplitContainer_LumpDetails.SplitterDistance = 244;
            this.SplitContainer_LumpDetails.TabIndex = 2;
            this.SplitContainer_LumpDetails.Visible = false;
            // 
            // ListView_Lumps
            // 
            this.ListView_Lumps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_Lumps_Name});
            this.ListView_Lumps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Lumps.FullRowSelect = true;
            this.ListView_Lumps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Lumps.Location = new System.Drawing.Point(0, 0);
            this.ListView_Lumps.Name = "ListView_Lumps";
            this.ListView_Lumps.Size = new System.Drawing.Size(244, 437);
            this.ListView_Lumps.TabIndex = 0;
            this.ListView_Lumps.UseCompatibleStateImageBehavior = false;
            this.ListView_Lumps.View = System.Windows.Forms.View.Details;
            this.ListView_Lumps.SelectedIndexChanged += new System.EventHandler(this.ListView_Lumps_SelectedIndexChanged);
            // 
            // ColumnHeader_Lumps_Name
            // 
            this.ColumnHeader_Lumps_Name.Text = "Name";
            this.ColumnHeader_Lumps_Name.Width = 240;
            // 
            // RichTextBox_LumpDetails
            // 
            this.RichTextBox_LumpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_LumpDetails.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBox_LumpDetails.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox_LumpDetails.Name = "RichTextBox_LumpDetails";
            this.RichTextBox_LumpDetails.ReadOnly = true;
            this.RichTextBox_LumpDetails.Size = new System.Drawing.Size(701, 437);
            this.RichTextBox_LumpDetails.TabIndex = 0;
            this.RichTextBox_LumpDetails.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 478);
            this.Controls.Add(this.SplitContainer_LumpDetails);
            this.Controls.Add(this.GroupBox_File);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dwad LumpViewer";
            this.GroupBox_File.ResumeLayout(false);
            this.SplitContainer_File.Panel1.ResumeLayout(false);
            this.SplitContainer_File.Panel1.PerformLayout();
            this.SplitContainer_File.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_File)).EndInit();
            this.SplitContainer_File.ResumeLayout(false);
            this.SplitContainer_LumpDetails.Panel1.ResumeLayout(false);
            this.SplitContainer_LumpDetails.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_LumpDetails)).EndInit();
            this.SplitContainer_LumpDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_File;
        private System.Windows.Forms.SplitContainer SplitContainer_File;
        private System.Windows.Forms.TextBox TextBox_WADFile;
        private System.Windows.Forms.Button Button_SelectWADFile;
        private System.Windows.Forms.SplitContainer SplitContainer_LumpDetails;
        private System.Windows.Forms.ListView ListView_Lumps;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Lumps_Name;
        private System.Windows.Forms.RichTextBox RichTextBox_LumpDetails;
    }
}

