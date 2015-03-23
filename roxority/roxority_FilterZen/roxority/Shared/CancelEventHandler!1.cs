namespace roxority.Shared
{
    using System;
    using System.Runtime.CompilerServices;

    internal delegate void CancelEventHandler<T>(object sender, CancelEventArgs<T> e);
}

