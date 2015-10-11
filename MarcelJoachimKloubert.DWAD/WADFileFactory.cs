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

using MarcelJoachimKloubert.DWAD.WADs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.DWAD
{
    /// <summary>
    /// WAD file factory.
    /// </summary>
    public static class WADFileFactory
    {
        #region Methods (2)

        /// <summary>
        /// Creates instances from a stream in <see cref="WADFormat.Default" /> format.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bufferSize">The custom buffer size to use to read from <paramref name="stream" />.</param>
        /// <returns>The list of lazy loaded instances.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream" /> is not readable.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is less than 1.
        /// </exception>
        public static IEnumerable<IWADFile> FromStream(Stream stream, int? bufferSize = null)
        {
            return FromStream(stream, WADFormat.Default, bufferSize);
        }

        /// <summary>
        /// Creates instances from a stream.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="bufferSize">The custom buffer size to use to read from <paramref name="stream" />.</param>
        /// <returns>The list of lazy loaded instances.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream" /> is not readable.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize" /> is less than 1.
        /// </exception>
        public static IEnumerable<IWADFile> FromStream(Stream stream, WADFormat format, int? bufferSize = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (stream.CanRead == false)
            {
                throw new ArgumentException("stream");
            }

            if (bufferSize < 1)
            {
                throw new ArgumentOutOfRangeException("bufferSize", bufferSize,
                                                      "Is less than 1!");
            }

            bool hasNext;

            do
            {
                hasNext = false;

                byte[] buffer;

                buffer = new byte[4];
                if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                {
                    continue;
                }

                var header = Encoding.ASCII.GetString(buffer).ToUpper().Trim();

                IWADFile result = null;

                var wadStream = new MemoryStream();
                try
                {
                    Action copyToWadStream = () =>
                        {
                            if (!bufferSize.HasValue)
                            {
                                stream.CopyTo(wadStream);
                            }
                            else
                            {
                                stream.CopyTo(wadStream, bufferSize.Value);
                            }

                            wadStream.Position = 0;
                        };

                    switch (header)
                    {
                        case "IWAD":
                            copyToWadStream();

                            result = new IWAD(format, wadStream, true);
                            break;

                        case "PWAD":
                            copyToWadStream();

                            result = new PWAD(format, wadStream, true);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    wadStream.Dispose();

                    throw ex;
                }

                if (result != null)
                {
                    hasNext = true;

                    yield return result;
                }
            }
            while (hasNext);
        }

        #endregion Methods (2)
    }
}