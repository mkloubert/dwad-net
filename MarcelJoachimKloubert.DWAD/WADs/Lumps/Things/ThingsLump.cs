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

using MarcelJoachimKloubert.DWAD.WADs.Lumps.Things;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.DWAD.WADs
{
    partial class WADFileBase
    {
        internal class ThingsLump : UnknownLump, IThingsLump
        {
            #region Methods (1)

            public IEnumerable<IThing> EnumerateThings()
            {
                using (var stream = this.GetStream())
                {
                    switch (this.File.Format)
                    {
                        case WADFormat.Default:
                            {
                                bool hasNext;

                                var index = -1;
                                do
                                {
                                    hasNext = false;

                                    byte[] buffer;

                                    buffer = new byte[2];
                                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                                    {
                                        continue;
                                    }

                                    // X coordinate
                                    var x = ToInt16(buffer).Value;

                                    buffer = new byte[2];
                                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                                    {
                                        continue;
                                    }

                                    // Y coordinate
                                    var y = ToInt16(buffer).Value;

                                    buffer = new byte[2];
                                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                                    {
                                        continue;
                                    }

                                    // angle facing
                                    var angle = ToInt16(buffer).Value;

                                    buffer = new byte[2];
                                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                                    {
                                        continue;
                                    }

                                    // typeID
                                    var type = ToInt16(buffer).Value;

                                    buffer = new byte[2];
                                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                                    {
                                        continue;
                                    }

                                    // flags
                                    var flags = ToInt16(buffer).Value;

                                    var thingType = typeof(UnknownDOOMThing);

                                    if (thingType != null)
                                    {
                                        hasNext = true;

                                        var thing = (UnknownDOOMThing)Activator.CreateInstance(thingType);

                                        thing.Angle = angle;
                                        thing.Flags = flags;
                                        thing.Index = ++index;
                                        thing.Lump = this;
                                        thing.Type = type;
                                        thing.X = x;
                                        thing.Y = y;

                                        yield return thing;
                                    }
                                }
                                while (hasNext);
                            }
                            break;

                        default:
                            throw new NotImplementedException(string.Format("Format '{0}' is currently NOT supported!",
                                                                            this.File.Format));
                    }
                }
            }

            #endregion Methods (1)
        }
    }
}