namespace roxority.Shared
{
    using System;
    using System.Runtime.CompilerServices;

    internal delegate void ActionRef<T, U>(ref T t, ref U u);
}

