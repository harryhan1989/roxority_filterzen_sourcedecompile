namespace roxority.Shared
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate TReturn Operation<TParam, TReturn>(TParam value);
}

