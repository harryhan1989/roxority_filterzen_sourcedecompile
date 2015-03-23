namespace roxority.Shared.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class Win32BitmapInfo
    {
        internal int biSize;
        internal int biWidth;
        internal int biHeight;
        internal short biPlanes;
        internal short biBitCount;
        internal int biCompression;
        internal int biSizeImage;
        internal int biXPelsPerMeter;
        internal int biYPelsPerMeter;
        internal int biClrUsed;
        internal int biClrImportant;
        internal byte bmiColors_rgbBlue;
        internal byte bmiColors_rgbGreen;
        internal byte bmiColors_rgbRed;
        internal byte bmiColors_rgbReserved;
    }
}

