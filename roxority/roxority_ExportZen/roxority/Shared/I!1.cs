namespace roxority.Shared
{
    using System;
    using System.Runtime.InteropServices;

    internal interface I<T>
    {
        void Get(out T value);
        void Set(T value);
    }
}

