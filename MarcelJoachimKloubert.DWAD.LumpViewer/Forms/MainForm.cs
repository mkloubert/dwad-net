/**********************************************************************************************************************
 * dwad-net (https://github.com/mkloubert/dwad-net)                                                                   *
 *                                                                                                                    *
 * Copyright (c) 2015, Marcel Joachim Kloubert <marcel.kloubert@gmx.net>                                              *
 * All rights reserved.                                                                                               *
 *                                                                                                                    *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the   *
 * following conditions are met:                                                                                      *
 *                                                                                                                    *
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the          *
 *    following disclaimer.                                                                                           *
 *                                                                                                                    *
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the       *
 *    following disclaimer in the documentation and/or other materials provided with the distribution.                *
 *                                                                                                                    *
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote    *
 *    products derived from this software without specific prior written permission.                                  *
 *                                                                                                                    *
 *                                                                                                                    *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, *
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE  *
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, *
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR    *
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,  *
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE   *
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.                                           *
 *                                                                                                                    *
 **********************************************************************************************************************/

using MarcelJoachimKloubert.DWAD.WADs.Lumps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.DWAD.LumpViewer.Forms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields (1)

        private IWADFile _currentFile;

        #endregion Fields (1)

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion Constructors (1)

        #region Properties (1)

        public IWADFile CurrentFile
        {
            get { return this._currentFile; }

            private set
            {
                try
                {
                    using (var oldFile = this._currentFile)
                    {
                        this._currentFile = value;

                        this.UpdateView(value);
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
            }
        }

        #endregion Properties (1)

        #region Methods (3)

        private void Button_SelectWADFile_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.InitialDirectory = Environment.CurrentDirectory;
                dialog.Filter = "WAD files (*.wad)|*.wad|All files (*.*)|*.*";
                dialog.Title = "Select WAD file...";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;

                if (DialogResult.OK != dialog.ShowDialog())
                {
                    return;
                }

                this.TextBox_WADFile.Text = dialog.FileName;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void ListView_Lumps_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var li = (ListView)sender;

                this.SplitContainer_LumpDetails.Panel2.Visible = false;

                if (li.SelectedIndices.Count < 1)
                {
                    return;
                }

                var selectedIndex = li.SelectedIndices[0];
                var selectedItem = li.Items[selectedIndex];

                var lump = (ILump)selectedItem.Tag;
                this.UpdateLumpDetails(lump);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        protected bool? ShowError(Exception ex)
        {
            if (ex == null)
            {
                return null;
            }

            try
            {
                ex = ex.GetBaseException() ?? ex;

                MessageBox.Show(this,
                                ex.GetBaseException().Message, "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return true;
            }
            catch
            {
                // ignore
                return false;
            }
        }

        private void TextBox_WADFile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var tb = (TextBox)sender;

                var filePath = tb.Text;
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return;
                }

                var file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    return;
                }

                using (var fs = file.OpenRead())
                {
                    this.CurrentFile = WADFileFactory.FromStream(fs).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void UpdateLumpDetails(ILump lump)
        {
            try
            {
                this.SplitContainer_LumpDetails.Panel2.Tag = lump;
                this.SplitContainer_LumpDetails.Panel2.Visible = false;

                if (lump == null)
                {
                    return;
                }

                this.RichTextBox_LumpDetails.Text = "";

                using (var stream = lump.GetStream())
                {
                    const int LINE_SIZE = 8;

                    var lines = new List<IList<byte>>();

                    IList<byte> currentLine = null;
                    for (var i = 0L; i < stream.Length; i++)
                    {
                        var @byte = (byte)stream.ReadByte();

                        if (i % LINE_SIZE == 0)
                        {
                            currentLine = new List<byte>();
                            lines.Add(currentLine);
                        }

                        currentLine.Add(@byte);
                    }

                    var sb = new StringBuilder();

                    using (var writer = new StringWriter(sb))
                    {
                        writer.WriteLine(string.Format("Name: {0}", lump.Name));
                        writer.WriteLine(string.Format("Position: {0}", lump.Position));
                        writer.WriteLine(string.Format("Size: {0}", lump.Size));

                        writer.WriteLine();
                        writer.WriteLine();

                        foreach (var l in lines)
                        {
                            foreach (var @byte in l)
                            {
                                writer.Write(string.Format("{0:X2} ", @byte));
                            }

                            for (var i = 0; i < (LINE_SIZE - l.Count); i++)
                            {
                                writer.Write("   ");
                            }

                            writer.Write("      ");

                            var str = Encoding.ASCII.GetString(l.ToArray());
                            foreach (var @char in str)
                            {
                                var charToWrite = @char.ToString();
                                if (char.IsLetterOrDigit(@char))
                                {
                                    charToWrite = @char + " ";
                                }
                                else
                                {
                                    charToWrite = "  ";
                                }

                                writer.Write(string.Format("{0} ",
                                                           charToWrite.PadLeft(2, ' ')));
                            }

                            writer.WriteLine();
                        }

                        writer.Flush();
                        writer.Close();
                    }

                    this.RichTextBox_LumpDetails.Text = sb.ToString();
                }

                this.SplitContainer_LumpDetails.Panel2.Visible = true;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void UpdateView(IWADFile file)
        {
            try
            {
                try
                {
                    // remove old lumps
                    foreach (var lvi in this.ListView_Lumps.Items
                                                     .Cast<ListViewItem>())
                    {
                        using (var lump = (ILump)lvi.Tag)
                        {
                            lvi.Remove();
                        }
                    }

                    this.SplitContainer_LumpDetails.Visible = false;

                    if (file == null)
                    {
                        return;
                    }

                    foreach (var lump in file.EnumerateLumps())
                    {
                        var lvi = new ListViewItem();
                        lvi.Tag = lump;
                        lvi.Text = lump.Name;

                        this.ListView_Lumps.Items.Add(lvi);
                    }

                    this.SplitContainer_LumpDetails.Visible = true;
                }
                finally
                {
                    this.ListView_Lumps_SelectedIndexChanged(this.ListView_Lumps, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        #endregion Methods (3)
    }
}