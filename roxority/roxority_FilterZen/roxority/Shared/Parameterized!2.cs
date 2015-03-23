namespace roxority.Shared
{
    using System;
    using System.Runtime.CompilerServices;

    internal delegate void Parameterized<T, U>(T t, params U[] u);
}

