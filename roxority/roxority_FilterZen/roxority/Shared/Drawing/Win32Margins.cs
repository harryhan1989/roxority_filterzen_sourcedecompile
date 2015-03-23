namespace roxority.Shared.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Margins
    {
        internal int Left;
        internal int Right;
        internal int Top;
        internal int Bottom;
    }
}

