namespace roxority.Shared.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32DrawThemeOptions
    {
        internal int dwSize;
        internal int dwFlags;
        internal int crText;
        internal int crBorder;
        internal int crShadow;
        internal int iTextShadowType;
        internal Win32Point ptShadowOffset;
        internal int iBorderSize;
        internal int iFontPropId;
        internal int iColorPropId;
        internal int iStateId;
        internal bool fApplyOverlay;
        internal int iGlowSize;
        internal int pfnDrawTextCallback;
        internal IntPtr lParam;
    }
}

