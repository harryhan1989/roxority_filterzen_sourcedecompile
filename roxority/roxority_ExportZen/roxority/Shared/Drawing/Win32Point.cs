namespace roxority.Shared.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        internal int x;
        internal int y;
        internal Win32Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

