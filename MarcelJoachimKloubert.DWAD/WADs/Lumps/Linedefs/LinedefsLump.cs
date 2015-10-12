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
using MarcelJoachimKloubert.DWAD.WADs.Lumps.Vertexes;
using System.Collections.Generic;
using System.Linq;

namespace MarcelJoachimKloubert.DWAD.WADs
{
    partial class WADFileBase
    {
        internal class LinedefsLump : UnknownLump, ILinedefsLump
        {
            #region Methods (1)

            public IEnumerable<ILinedef> EnumerateLinedefs()
            {
                var allVertextes = this.File
                                       .EnumerateLumps()
                                       .OfType<IVertexesLump>()
                                       .SelectMany(x => x.EnumerateVertexes())
                                       .ToArray();

                using (var stream = this.GetStream())
                {
                    bool hasNext;

                    do
                    {
                        hasNext = false;

                        byte[] buffer;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // start vertex
                        var startVertexIndex = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // end vertex
                        var endVertexIndex = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // flags
                        var flags = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // special type
                        var specialType = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // sector tag
                        var sectorTag = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // sidedef (right)
                        var sideDefRight = ToInt16(buffer).Value;

                        buffer = new byte[2];
                        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                        {
                            continue;
                        }

                        // sidedef (left)
                        var sideDefLeft = ToInt16(buffer).Value;

                        var startVertex = allVertextes.Skip(startVertexIndex).FirstOrDefault();
                        if (startVertex == null)
                        {
                            continue;
                        }

                        var endVertex = allVertextes.Skip(endVertexIndex).FirstOrDefault();
                        if (endVertex == null)
                        {
                            continue;
                        }

                        hasNext = true;

                        yield return new Linedef()
                        {
                            End = endVertex,
                            Lump = this,
                            Start = startVertex,
                        };
                    }
                    while (hasNext);
                }
            }

            #endregion Methods (1)
        }
    }
}