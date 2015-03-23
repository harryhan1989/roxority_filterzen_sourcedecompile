namespace roxority.Shared
{
    using System;
    using System.Drawing;

    internal interface ISelfDescriptor
    {
        string Description { get; }

        System.Drawing.Image Image { get; }

        string Title { get; }
    }
}

