namespace roxority.Shared.Drawing
{
    using System;

    [Flags]
    internal enum SHGFI
    {
        AddOverlays = 0x20,
        Attr_Specified = 0x20000,
        Attributes = 0x800,
        DisplayName = 0x200,
        ExeType = 0x2000,
        Icon = 0x100,
        IconLocation = 0x1000,
        LargeIcon = 0,
        LinkOverlay = 0x8000,
        OpenIcon = 2,
        OverlayIndex = 0x40,
        PIDL = 8,
        Selected = 0x10000,
        ShellIconize = 4,
        SmallIcon = 1,
        SysIconIndex = 0x4000,
        TypeName = 0x400,
        UseFileAttributes = 0x10
    }
}

