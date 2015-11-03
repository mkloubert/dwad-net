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

namespace MarcelJoachimKloubert.DWAD
{
    /// <summary>
    /// Builds a WAD file.
    /// </summary>
    public class WADFileBuilder : DisposableBase
    {
        #region Fields (2)

        /// <summary>
        /// List of lump files.
        /// </summary>
        protected readonly IList<ILump> _LUMPS = new List<ILump>();

        /// <summary>
        /// Stores if lumps of <see cref="WADFileBuilder._LUMPS" /> should also be disposed or not..
        /// </summary>
        protected readonly bool _OWNS_LUMPS;

        #endregion Fields (2)

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="WADFileBuilder" /> class.
        /// </summary>
        /// <param name="ownsLumps">The value for the <see cref="WADFileBuilder._OWNS_LUMPS" /> field.</param>
        public WADFileBuilder(bool ownsLumps = true)
        {
            this._OWNS_LUMPS = ownsLumps;
        }

        #endregion Constructors (1)

        #region Methods (4)

        /// <summary>
        /// Adds a lump.
        /// </summary>
        /// <param name="lump">The lump to add.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lump" /> is <see langword="null" />.
        /// </exception>
        public WADFileBuilder Add(ILump lump)
        {
            if (lump == null)
            {
                throw new ArgumentNullException("lump");
            }

            return this.InvokeForDisposable(
                func: (obj, state) =>
                    {
                        var builder = (WADFileBuilder)obj;
                        builder._LUMPS.Add(state.Lump);

                        return builder;
                    },
                funcState: new
                    {
                        Lump = lump,
                    });
        }

        /// <summary>
        /// Adds a list of lumps.
        /// </summary>
        /// <param name="lumps">The lumps to add.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lumps" /> is <see langword="null" />.
        /// </exception>
        public WADFileBuilder AddRange(IEnumerable<ILump> lumps)
        {
            if (lumps == null)
            {
                throw new ArgumentNullException("lumps");
            }

            foreach (var l in lumps.OfType<ILump>())
            {
                this.Add(l);
            }

            return this;
        }

        /// <summary>
        /// Builds a file.
        /// </summary>
        /// <param name="initialLumpName">Name of the initial lump.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>The created file.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initialLumpName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initialLumpName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Length of <paramref name="initialLumpName" /> is greater than 8.
        /// </exception>
        public IWADFile Build(string initialLumpName, WADFormat format = WADFormat.Default)
        {
            if (initialLumpName == null)
            {
                throw new ArgumentNullException("initialLumpName");
            }

            initialLumpName = initialLumpName.ToUpper().Trim();
            if (initialLumpName == string.Empty)
            {
                throw new ArgumentException("initialLumpName");
            }

            var initialLumpNameLength = Encoding.ASCII.GetBytes(initialLumpName).Length;
            if (initialLumpNameLength > 8)
            {
                throw new ArgumentOutOfRangeException("initialLumpName.Length", initialLumpNameLength,
                                                      "Cannot be larger than 8 chars!");
            }

            return this.InvokeForDisposable(
                func: (obj, state) =>
                    {
                        var builder = (WADFileBuilder)obj;

                        using (var temp = new MemoryStream())
                        {
                            // header
                            temp.Write(Encoding.ASCII.GetBytes("PWAD"), 0, 4);

                            // these are overwritten later
                            temp.Write(new byte[8], 0, 8);

                            var newLumpList = new List<Lump>();

                            var firstLump = new Lump();
                            firstLump.Name = state.FirstLumpName;
                            firstLump.Position = 12;
                            firstLump.Size = 0;
                            newLumpList.Add(firstLump);

                            // write lump data
                            foreach (var lump in builder._LUMPS)
                            {
                                using (var lumpStream = lump.GetStream())
                                {
                                    var newLump = new Lump();
                                    newLump.Name = lump.Name;
                                    newLump.Position = (int)temp.Position;
                                    newLump.Size = (int)lumpStream.Length;

                                    lumpStream.CopyTo(temp);

                                    newLumpList.Add(newLump);
                                }
                            }

                            var secondLump = newLumpList.Skip(1).FirstOrDefault();
                            if (secondLump != null)
                            {
                                firstLump.Position = secondLump.Position;
                            }

                            var numberOfLumps = newLumpList.Count;
                            var posOfEntries = (int)temp.Position;

                            // now write entries
                            foreach (var nl in newLumpList)
                            {
                                var lumpNameChars = new List<byte>(Encoding.ASCII.GetBytes(nl.Name ?? string.Empty));
                                while (lumpNameChars.Count < 8)
                                {
                                    lumpNameChars.Add(0);  // fill with zero chars
                                }

                                if (lumpNameChars.Count > 8)
                                {
                                    throw new ArgumentOutOfRangeException("lumpNameChars.Count", lumpNameChars.Count,
                                                                          "Cannot be larger than 8 chars!");
                                }

                                var lumpPos = GetBytes(nl.Position);
                                var lumpSize = GetBytes(nl.Size);
                                var lumpName = lumpNameChars.ToArray();

                                temp.Write(lumpPos, 0, lumpPos.Length);
                                temp.Write(lumpSize, 0, lumpSize.Length);
                                temp.Write(lumpName, 0, lumpName.Length);
                            }

                            // update header
                            temp.Position = 4;
                            temp.Write(GetBytes(numberOfLumps), 0, 4);
                            temp.Write(GetBytes(posOfEntries), 0, 4);

                            temp.Position = 0;
                            return WADFileFactory.FromStream(temp, state.Format).Single();
                        }
                    },
                funcState: new
                    {
                        FirstLumpName = initialLumpName,
                        Format = format,
                    });
        }

        /// <summary>
        /// <see cref="DisposableBase.OnDispose(bool, ref bool)" />
        /// </summary>
        protected override void OnDispose(bool disposing, ref bool isDisposed)
        {
            if (disposing)
            {
                if (this._OWNS_LUMPS)
                {
                    var exceptions = new List<Exception>();

                    foreach (var lump in this._LUMPS)
                    {
                        try
                        {
                            lump.Dispose();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add(ex);
                        }
                    }

                    if (exceptions.Count > 0)
                    {
                        throw new AggregateException(exceptions);
                    }
                }
            }
        }

        #endregion Methods (4)
    }
}