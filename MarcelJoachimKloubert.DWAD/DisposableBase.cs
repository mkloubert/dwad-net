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

namespace MarcelJoachimKloubert.DWAD
{
    /// <summary>
    /// A basic disposable object (thread safe).
    /// </summary>
    public abstract class DisposableBase : WADObject, IDisposable
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableBase" /> class.
        /// </summary>
        /// <param name="sync">
        /// The custom value for the <see cref="WADObject._SYNC" /> field.
        /// </param>
        protected DisposableBase(object sync = null)
            : base(sync: sync)
        {
        }

        /// <summary>
        /// Frees the <see cref="DisposableBase" /> object.
        /// </summary>
        ~DisposableBase()
        {
            try
            {
                this.Dispose(false);
            }
            catch
            {
                // ignore
            }
        }

        #endregion Constructors (2)

        #region Events (2)

        /// <summary>
        /// Is raised when object begins disposing itself.
        /// </summary>
        public event EventHandler Disposing;

        /// <summary>
        /// Is raised when the object has been disposed.
        /// </summary>
        public event EventHandler Disposed;

        #endregion Events (2)

        #region Properties (1)

        /// <summary>
        /// Gets if the object has been disposed (<see langword="true" />) or not (<see langword="false" />).
        /// </summary>
        public bool IsDisposed
        {
            get;
            private set;
        }

        #endregion Properties (1)

        #region Methods (10)

        /// <summary>
        /// <see cref="IDisposable.Dispose()" />
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                this.RaiseError(ex);
            }
        }

        private void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                if (disposing && this.IsDisposed)
                {
                    return;
                }

                try
                {
                    if (disposing)
                    {
                        this.RaiseEventHandler(this.Disposing);
                    }

                    var isDisposed = disposing ? true : this.IsDisposed;
                    this.OnDispose(disposing, ref isDisposed);

                    this.IsDisposed = isDisposed;
                    if (this.IsDisposed)
                    {
                        this.RaiseEventHandler(this.Disposed);
                    }
                }
                catch (Exception ex)
                {
                    if (disposing)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Invokes an action for that object thread safe.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForDisposable(Action<DisposableBase> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForDisposable(action: (obj, state) => state.Action(obj),
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
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForDisposable<TState>(Action<DisposableBase, TState> action, TState actionState)
        {
            this.InvokeForDisposable<TState>(action: action,
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
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void InvokeForDisposable<TState>(Action<DisposableBase, TState> action, Func<DisposableBase, TState> actionStateFactory)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            this.InvokeForDisposable(
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
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForDisposable<TResult>(Func<DisposableBase, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeForDisposable(func: (obj, state) => state.Function(obj),
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
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForDisposable<TState, TResult>(Func<DisposableBase, TState, TResult> func, TState funcState)
        {
            return this.InvokeForDisposable<TState, TResult>(func: func,
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
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected TResult InvokeForDisposable<TState, TResult>(Func<DisposableBase, TState, TResult> func, Func<DisposableBase, TState> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            return this.InvokeThreadSafe(
                func: (obj, state) =>
                    {
                        state.Object.ThrowIfDisposed();

                        return state.Function(state.Object,
                                              state.StateFactory(state.Object));
                    },
                funcState: new
                    {
                        Function = func,
                        Object = this,
                        StateFactory = funcStateFactory,
                    });
        }

        /// <summary>
        /// The logic for the <see cref="DisposableBase.Dispose()" /> method or the destructor.
        /// </summary>
        /// <param name="disposing">
        /// <see cref="DisposableBase.Dispose()" /> method was invoked (<see langword="true" />)
        /// or the destructor (<see langword="false" />).
        /// </param>
        /// <param name="isDisposed">
        /// The new value for <see cref="DisposableBase.IsDisposed" /> property.
        /// </param>
        protected abstract void OnDispose(bool disposing, ref bool isDisposed);

        /// <summary>
        /// Throws an exception if that object has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Object has been disposed.</exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        #endregion Methods (10)
    }
}