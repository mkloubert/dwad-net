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

using System;
using System.Collections.Generic;
using System.Linq;

namespace MarcelJoachimKloubert.DWAD
{
    /// <summary>
    /// A basic WAD object.
    /// </summary>
    public class WADObject : MarshalByRefObject
    {
        #region Fields (1)

        /// <summary>
        /// Stores the value for <see cref="WADObject.SyncRoot" /> property.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields (1)

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="WADObject" /> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="WADObject._SYNC" /> field.</param>
        public WADObject(object sync = null)
        {
            this._SYNC = sync ?? new object();
        }

        #endregion Constructors (1)

        #region Events (1)

        /// <summary>
        /// Is raised on an error.
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        #endregion Events (1)

        #region Properties (1)

        /// <summary>
        /// Gets an object for thread safe operations.
        /// </summary>
        public object SyncRoot
        {
            get { return this._SYNC; }
        }

        #endregion Properties (1)

        #region Methods (14)

        /// <summary>
        /// Returns a sequence as array.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>
        /// <paramref name="seq" /> as array.
        /// If <paramref name="seq" /> is already an array, it is simply casted.
        /// If <paramref name="seq" /> is <see langword="null" />, a <see langword="null" /> reference will be returned.
        /// </returns>
        public static T[] AsArray<T>(IEnumerable<T> seq)
        {
            if (seq is T[])
            {
                return (T[])seq;
            }

            if (seq == null)
            {
                return null;
            }

            if (seq is List<T>)
            {
                // ToArray() of list object
                return ((List<T>)seq).ToArray();
            }

            // LINQ style
            return seq.ToArray();
        }

        /// <summary>
        /// Returns the binary data for a <see cref="int" /> value.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <returns>The output value.</returns>
        public static byte[] GetBytes(int? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return AsArray(UpdateByteOrder(BitConverter.GetBytes(value.Value)));
        }

        /// <summary>
        /// Invokes an action for that object thread safe.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeThreadSafe(Action<WADObject> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeThreadSafe(action: (obj, state) => state.Action(obj),
                                  actionState: new
                                  {
                                      Action = action,
                                  });
        }

        /// <summary>
        /// Invokes an action for that object thread safe.
        /// </summary>
        /// <typeparam name="TState">The type of the second argument of <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">The second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeThreadSafe<TState>(Action<WADObject, TState> action, TState actionState)
        {
            this.InvokeThreadSafe<TState>(action: action,
                                          actionStateFactory: (obj) => actionState);
        }

        /// <summary>
        /// Invokes an action for that object thread safe.
        /// </summary>
        /// <typeparam name="TState">The type of the second argument of <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">The factory that produces the second argument for <paramref name="action" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> and/or <paramref name="actionStateFactory" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeThreadSafe<TState>(Action<WADObject, TState> action, Func<WADObject, TState> actionStateFactory)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            this.InvokeThreadSafe(
                func: (obj, state) =>
                    {
                        state.Action(obj,
                                     state.StateFactory(obj));

                        return (object)null;
                    },
                funcState: new
                    {
                        Action = action,
                        StateFactory = actionStateFactory,
                    });
        }

        /// <summary>
        /// Invokes a function for that object thread safe.
        /// </summary>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeThreadSafe<TResult>(Func<WADObject, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeThreadSafe(func: (obj, state) => state.Function(obj),
                                         funcState: new
                                         {
                                             Function = func,
                                         });
        }

        /// <summary>
        /// Invokes a function for that object thread safe.
        /// </summary>
        /// <typeparam name="TState">The tpe of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcState">The second argument of <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeThreadSafe<TState, TResult>(Func<WADObject, TState, TResult> func, TState funcState)
        {
            return this.InvokeThreadSafe<TState, TResult>(func: func,
                                                          funcStateFactory: (obj) => funcState);
        }

        /// <summary>
        /// Invokes a function for that object thread safe.
        /// </summary>
        /// <typeparam name="TState">The tpe of the second argument of <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Type of the result of <paramref name="func" />.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcStateFactory">The factory that produces the second argument of <paramref name="func" />.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> and/or <paramref name="funcStateFactory" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeThreadSafe<TState, TResult>(Func<WADObject, TState, TResult> func, Func<WADObject, TState> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            TResult result = default(TResult);

            lock (this._SYNC)
            {
                try
                {
                    result = func(this,
                                  funcStateFactory(this));
                }
                catch (Exception ex)
                {
                    this.RaiseError(ex, true);
                }
            }

            return result;
        }

        /// <summary>
        /// Raises the <see cref="WADObject.Error" /> event.
        /// </summary>
        /// <param name="ex">The underlying exception.</param>
        /// <param name="rethrow">
        /// Rethrow exception or not.
        /// <see langword="null" /> indicates that exception only should be rethrown if no event handler was raised.
        /// </param>
        /// <returns>
        /// Handler was raised (<see langword="true" />) or not (<see langword="false" />).
        /// <see langword="null" /> indicates that <paramref name="ex" /> is <see langword="null" />.
        /// </returns>
        /// <exception cref="Exception">The exception of <paramref name="ex" />.</exception>
        protected bool? RaiseError(Exception ex, bool? rethrow = null)
        {
            if (ex == null)
            {
                return null;
            }

            var e = new ErrorEventArgs(ex);
            var result = this.RaiseEventHandler(this.Error, e);

            if (rethrow ?? !result)
            {
                throw e.Exception;
            }

            return result;
        }

        /// <summary>
        /// Raises an event handler.
        /// </summary>
        /// <param name="handler">The handler to raise.</param>
        /// <returns>Handler was raised (<see langword="true" />); otherwise <paramref name="handler" /> is <see langword="null" />.</returns>
        protected bool RaiseEventHandler(EventHandler handler)
        {
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Raises an event handler.
        /// </summary>
        /// <typeparam name="TArgs">Type of the event arguments.</typeparam>
        /// <param name="handler">The handler to raise.</param>
        /// <param name="e">The arguments for the event.</param>
        /// <returns>Handler was raised (<see langword="true" />); otherwise <paramref name="handler" /> is <see langword="null" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="e" /> is <see langword="null" />.
        /// </exception>
        protected bool RaiseEventHandler<TArgs>(EventHandler<TArgs> handler, TArgs e)
            where TArgs : global::System.EventArgs
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (handler != null)
            {
                handler(this, e);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts binary data to a <see cref="short" /> value.
        /// </summary>
        /// <param name="bytes">The input data.</param>
        /// <returns>The output value.</returns>
        public static short? ToInt16(IEnumerable<byte> bytes)
        {
            bytes = UpdateByteOrder(bytes);

            if (bytes != null)
            {
                return BitConverter.ToInt16(AsArray(bytes.Take(2)), 0);
            }

            return null;
        }

        /// <summary>
        /// Converts binary data to a <see cref="int" /> value.
        /// </summary>
        /// <param name="bytes">The input data.</param>
        /// <returns>The output value.</returns>
        public static int? ToInt32(IEnumerable<byte> bytes)
        {
            bytes = UpdateByteOrder(bytes);

            if (bytes != null)
            {
                return BitConverter.ToInt32(AsArray(bytes.Take(4)), 0);
            }

            return null;
        }

        /// <summary>
        /// Updates the order binary data depending on the system settings in <see cref="BitConverter.IsLittleEndian" />.
        /// </summary>
        /// <param name="bytes">The input data.</param>
        /// <returns>The output data.</returns>
        public static IEnumerable<byte> UpdateByteOrder(IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            if (!BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse();
            }

            return bytes;
        }

        #endregion Methods (14)
    }
}