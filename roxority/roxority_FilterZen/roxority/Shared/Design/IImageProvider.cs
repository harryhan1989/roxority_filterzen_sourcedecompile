namespace roxority.Shared.Design
{
    using System;
    using System.Drawing;

    internal interface IImageProvider
    {
        event EventHandler ImageChanged;

        System.Drawing.Image Image { get; }
    }
}

