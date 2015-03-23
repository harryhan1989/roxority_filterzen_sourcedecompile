namespace roxority.Shared
{
    using System;
    using System.Runtime.CompilerServices;

    internal delegate TReturn Operation<TParam1, TParam2, TReturn>(TParam1 param1, TParam2 param2);
}

