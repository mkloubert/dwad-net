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
using System.Text;

namespace MarcelJoachimKloubert.DWAD.WADs
{
    /// <summary>
    /// A basic WAD file.
    /// </summary>
    public abstract partial class WADFileBase : DisposableBase, IWADFile
    {
        #region Fields (4)

        /// <summary>
        /// The size of the file ID string (&quot;IWAD&quot; or &quot;PWAD&quot;).
        /// </summary>
        public const int FILE_ID_SIZE = 4;

        private readonly WADFormat _FORMAT;

        /// <summary>
        /// Stores if that object owns <see cref="WADFileBase._STREAM" /> or not.
        /// </summary>
        protected readonly bool _OWNS_STREAM;

        private readonly Stream _STREAM;

        #endregion Fields (4)

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="WADFileBase" /> class.
        /// </summary>
        /// <param name="format">
        /// The value for the <see cref="WADFileBase.Format" /> property.
        /// </param>
        /// <param name="stream">
        /// The value for the <see cref="WADFileBase.Stream" /> property.
        /// </param>
        /// <param name="ownsStream">
        /// The value for the <see cref="WADFileBase._OWNS_STREAM" /> field.
        /// </param>
        /// <param name="sync">
        /// The custom value for the <see cref="WADObject._SYNC" /> field.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream" /> is not readable / seekable.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        protected WADFileBase(WADFormat format, Stream stream, bool ownsStream = true, object sync = null)
            : base(sync: sync)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (stream.CanRead == false)
            {
                throw new ArgumentException("Not readable!", "stream");
            }

            if (stream.CanSeek == false)
            {
                throw new ArgumentException("Not seekable!", "stream");
            }

            this._FORMAT = format;
            this._STREAM = stream;
            this._OWNS_STREAM = ownsStream;
        }

        #endregion Constructors (1)

        #region Properties (3)

        /// <summary>
        /// <see cref="IWADFile.Format" />
        /// </summary>
        public WADFormat Format
        {
            get { return this._FORMAT; }
        }

        /// <summary>
        /// Gets the base stream.
        /// </summary>
        public Stream Stream
        {
            get { return this._STREAM; }
        }

        /// <summary>
        /// <see cref="IWADFile.Type" />
        /// </summary>
        public abstract WADType Type
        {
            get;
        }

        #endregion Properties (3)

        #region Methods (9)

        /// <summary>
        /// <see cref="IWADFile.EnumerateLumps()" />.
        /// </summary>
        public IEnumerable<ILump> EnumerateLumps()
        {
            return this.InvokeForStream(func: EnumerateLumps);
        }

        /// <summary>
        /// The logic for the <see cref="WADFileBase.EnumerateLumps()" /> method.
        /// </summary>
        /// <param name="file">The underlying file instance.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The list of lumps.</returns>
        protected static IEnumerable<ILump> EnumerateLumps(WADFileBase file, Stream stream)
        {
            stream.Position = 0;

            byte[] buffer;

            buffer = new byte[4];
            if ((stream.Read(buffer, 0, buffer.Length)) != buffer.Length)
            {
                yield break;
            }

            var lumpCount = ToInt32(buffer).Value;

            buffer = new byte[4];
            if ((stream.Read(buffer, 0, buffer.Length)) != buffer.Length)
            {
                yield break;
            }

            var lumpDirOffset = ToInt32(buffer).Value;

            stream.Position = lumpDirOffset - FILE_ID_SIZE;
            for (var i = 0; i < lumpCount; i++)
            {
                buffer = new byte[4];
                if ((stream.Read(buffer, 0, buffer.Length)) != buffer.Length)
                {
                    yield break;
                }

                var lumpPos = ToInt32(buffer).Value - FILE_ID_SIZE;

                buffer = new byte[4];
                if ((stream.Read(buffer, 0, buffer.Length)) != buffer.Length)
                {
                    yield break;
                }

                var lumpSize = ToInt32(buffer).Value;

                buffer = new byte[8];
                if ((stream.Read(buffer, 0, buffer.Length)) != buffer.Length)
                {
                    yield break;
                }

                var lumpName = Encoding.ASCII.GetString(buffer);
                while (lumpName.EndsWith("\0"))
                {
                    lumpName = lumpName.Substring(0, lumpName.Length - 1);
                }

                var lumpType = typeof(UnknownLump);

                switch (lumpName.ToUpper().Trim())
                {
                    case "THINGS":
                        lumpType = typeof(ThingsLump);
                        break;
                }

                if (lumpType != null)
                {
                    var result = (UnknownLump)Activator.CreateInstance(lumpType);
                    result.File = file;
                    result.Name = lumpName;
                    result.Position = lumpPos + FILE_ID_SIZE;
                    result.Size = lumpSize;

                    yield return result;
                }
            }
        }

        /// <summary>
        /// <see cref="DisposableBase.OnDispose(bool, ref bool)" />
        /// </summary>
        protected override void OnDispose(bool disposing, ref bool isDisposed)
        {
            if (disposing)
            {
                if (this._OWNS_STREAM)
                {
                    this.Stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Invokes an action for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForStream(Action<WADFileBase, Stream> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForStream(action: (obj, stream, state) => state.Action(obj, stream),
                                 actionState: new
                                 {
                                     Action = action,
                                 });
        }

        /// <summary>
        /// Invokes an action for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <typeparam name="TState">The type of the second argument of <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">The second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForStream<TState>(Action<WADFileBase, Stream, TState> action, TState actionState)
        {
            this.InvokeForStream<TState>(action: action,
                                         actionStateFactory: (obj, stream) => actionState);
        }

        /// <summary>
        /// Invokes an action for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <typeparam name="TState">The type of the second argument of <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">The factory that produces the second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> and/or <paramref name="actionStateFactory" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForStream<TState>(Action<WADFileBase, Stream, TState> action, Func<WADFileBase, Stream, TState> actionStateFactory)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            this.InvokeForStream(
                func: (obj, stream, state) =>
                    {
                        state.Action(obj, stream,
                                     state.StateFactory(obj, stream));

                        return (object)null;
                    },
                funcState: new
                    {
                        Action = action,
                        StateFactory = actionStateFactory,
                    });
        }

        /// <summary>
        /// Invokes a function for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForStream<TResult>(Func<WADFileBase, Stream, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeForStream(func: (obj, stream, state) => state.Function(obj, stream),
                                        funcState: new
                                        {
                                            Function = func,
                                        });
        }

        /// <summary>
        /// Invokes a function for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <typeparam name="TState">The tpe of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcState">The second argument of <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForStream<TState, TResult>(Func<WADFileBase, Stream, TState, TResult> func, TState funcState)
        {
            return this.InvokeForStream<TState, TResult>(func: func,
                                                         funcStateFactory: (obj, stream) => funcState);
        }

        /// <summary>
        /// Invokes a function for the underlying stream of that object thread safe
        /// and restores the original position (if possible).
        /// </summary>
        /// <typeparam name="TState">The tpe of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcStateFactory">The factory that produces the second argument of <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> and/or <paramref name="funcStateFactory" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForStream<TState, TResult>(Func<WADFileBase, Stream, TState, TResult> func, Func<WADFileBase, Stream, TState> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            return this.InvokeForDisposable(
                func: (obj, state) =>
                    {
                        long? oldPosition = null;

                        try
                        {
                            if (state.Object.Stream.CanSeek)
                            {
                                oldPosition = state.Object.Stream.Position;
                            }

                            return state.Function(state.Object, state.Object.Stream,
                                                  state.StateFactory(state.Object, state.Object.Stream));
                        }
                        finally
                        {
                            try
                            {
                                if (oldPosition.HasValue)
                                {
                                    state.Object.Stream.Position = oldPosition.Value;
                                }
                            }
                            catch (Exception ex)
                            {
                                state.Object.RaiseError(ex, true);
                            }
                        }
                    },
                funcState: new
                    {
                        Function = func,
                        Object = this,
                        StateFactory = funcStateFactory,
                    });
        }

        #endregion Methods (9)
    }
}