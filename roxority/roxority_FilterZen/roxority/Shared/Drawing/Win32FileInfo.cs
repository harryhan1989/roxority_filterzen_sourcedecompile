namespace roxority.Shared.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32FileInfo
    {
        internal const int MAX_PATH = 260;
        internal const int MAX_TYPE = 80;
        internal IntPtr hIcon;
        internal int iIcon;
        internal uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
        internal string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
        internal string szTypeName;
        [DllImport("shell32.dll", CharSet=CharSet.Auto)]
        internal static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, out Win32FileInfo psfi, uint cbfileInfo, SHGFI uFlags);
        internal Win32FileInfo(bool b)
        {
            this.hIcon = IntPtr.Zero;
            this.iIcon = 0;
            this.dwAttributes = 0;
            this.szDisplayName = "";
            this.szTypeName = "";
        }
    }
}

