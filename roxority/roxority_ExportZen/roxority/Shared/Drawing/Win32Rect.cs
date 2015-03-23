namespace roxority.Shared.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Rect
    {
        internal int Left;
        internal int Top;
        internal int Right;
        internal int Bottom;
        internal Win32Rect(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        internal Win32Rect(Rectangle rectangle)
        {
            this.Left = rectangle.X;
            this.Top = rectangle.Y;
            this.Right = rectangle.Right;
            this.Bottom = rectangle.Bottom;
        }

        internal Rectangle ToRectangle()
        {
            return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
        }
    }
}

