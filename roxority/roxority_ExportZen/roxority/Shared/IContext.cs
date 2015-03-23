namespace roxority.Shared
{
    using System;

    internal interface IContext : IDisposable
    {
        object ContextObject { get; }
    }
}

