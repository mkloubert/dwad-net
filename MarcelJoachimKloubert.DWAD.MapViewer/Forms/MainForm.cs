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

using MarcelJoachimKloubert.DWAD.WADs.Lumps.Linedefs;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.DWAD.MapViewer.Forms
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

                        this.UpdateMapView(value);
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
            }
        }

        #endregion Properties (1)

        #region Method (8)

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

        private void NumericUpDown_MapScale_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateMapView();
        }

        private void NumericUpDown_OffsetX_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateMapView();
        }

        private void NumericUpDown_OffsetY_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateMapView();
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

        private void UpdateMapView()
        {
            this.UpdateMapView(this.CurrentFile);
        }

        private void UpdateMapView(IWADFile file)
        {
            try
            {
                using (var oldMap = this.PictureBox_Map.Image)
                {
                    if (file == null)
                    {
                        return;
                    }

                    var offsetX = (int)this.NumericUpDown_OffsetX.Value;
                    var offsetY = (int)this.NumericUpDown_OffsetY.Value;
                    var scale = (int)this.NumericUpDown_MapScale.Value;

                    var newImage = new Bitmap(short.MaxValue / 10, short.MaxValue / 10, PixelFormat.Format32bppArgb);
                    try
                    {
                        var whitePen = Pens.White;

                        using (var g = Graphics.FromImage(newImage))
                        {
                            g.FillRectangle(Brushes.Black,
                                            new Rectangle(0, 0, newImage.Width, newImage.Height));

                            foreach (var lump in file.EnumerateLumps().OfType<ILinedefsLump>())
                            {
                                foreach (var linedef in lump.EnumerateLinedefs())
                                {
                                    var p1 = new Point((linedef.Start.X) / scale + offsetX,
                                                       (linedef.Start.Y) / scale + offsetY);

                                    var p2 = new Point((linedef.End.X) / scale + offsetX,
                                                       (linedef.End.Y) / scale + offsetY);

                                    g.DrawLine(whitePen,
                                               p1, p2);
                                }
                            }

                            g.Flush();
                            g.Save();
                        }

                        this.PictureBox_Map.Image = newImage;
                    }
                    catch (Exception ex)
                    {
                        newImage.Dispose();

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        #endregion Method (8)
    }
}