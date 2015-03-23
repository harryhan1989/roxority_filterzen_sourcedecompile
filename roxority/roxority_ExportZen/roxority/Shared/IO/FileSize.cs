namespace roxority.Shared.IO
{
    using System;

    internal enum FileSize : long
    {
        B = 0L,
        GB = 0x40000000L,
        KB = 0x400L,
        MB = 0x100000L,
        TB = 0x10000000000L
    }
}

