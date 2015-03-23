namespace roxority.Shared
{
    using System;
    using System.ComponentModel;

    internal class CancelEventArgs<T> : CancelEventArgs
    {
        internal readonly T Value;

        internal CancelEventArgs(T value, bool cancel) : base(cancel)
        {
            this.Value = value;
        }
    }
}

