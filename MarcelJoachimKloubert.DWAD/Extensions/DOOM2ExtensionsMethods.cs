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

namespace MarcelJoachimKloubert.DWAD.Extensions
{
    /// <summary>
    /// Extensions methods for DOOM 2.
    /// </summary>
    static partial class WADExtensionsMethods
    {
        #region Methods (1)

        /// <summary>
        /// Enumerates over the DOOM 2 maps.
        /// </summary>
        /// <param name="wadFile">The IWAD file.</param>
        /// <returns>The list of maps.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="wadFile" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="wadFile" /> has an invalid value in <see cref="IWADFile.Format" />.
        /// </exception>
        public static IEnumerable<IWADFile> EnumerateDOOM2Maps(this IWADFile wadFile)
        {
            if (wadFile == null)
            {
                throw new ArgumentNullException("wadFile");
            }

            if (wadFile.Format != WADFormat.Default)
            {
                throw new FormatException("wadFile");
            }

            using (var stream = wadFile.GetStream())
            {
                ILump mapLump = null;
                IList<ILump> lumpsOfMap = null;
                foreach (var lump in wadFile.EnumerateLumps())
                {
                    if ((lump.Name ?? string.Empty).ToUpper().Trim().StartsWith("MAP"))
                    {
                        if (lumpsOfMap != null)
                        {
                            using (var builder = new WADFileBuilder(true))
                            {
                                builder.AddRange(lumpsOfMap);

                                yield return builder.Build(mapLump.Name, WADFormat.Default);
                            }
                        }

                        mapLump = lump;
                        lumpsOfMap = new List<ILump>();

                        continue;
                    }

                    if (mapLump == null)
                    {
                        continue;
                    }

                    lumpsOfMap.Add(lump);
                }
            }
        }

        #endregion Methods (1)
    }
}