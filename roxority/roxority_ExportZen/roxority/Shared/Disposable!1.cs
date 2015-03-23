namespace roxority.Shared
{
    using System;

    internal class Disposable<T> : IDisposable
    {
        internal readonly Action<T> DisposeAction;
        internal readonly T Value;

        internal Disposable(T value, Action<T> disposeAction)
        {
            this.DisposeAction = disposeAction;
            this.Value = value;
        }

        public void Dispose()
        {
            if (this.DisposeAction != null)
            {
                this.DisposeAction(this.Value);
            }
            else if (this.Value is IDisposable)
            {
                ((IDisposable) this.Value).Dispose();
            }
        }
    }
}

