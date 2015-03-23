namespace roxority.Shared.Drawing
{
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    internal static class DrawingUtil
    {
        private static readonly Dictionary<Duo<Color, Image>, Bitmap> disabledImages = new Dictionary<Duo<Color, Image>, Bitmap>();
        private const int DTT_COMPOSITED = 0x2000;
        private const int DTT_GLOWSIZE = 0x800;
        private const int DTT_TEXTCOLOR = 1;
        private static ImageCodecInfo jpegCodec = null;
        private static readonly Hashtable knownColors;
        private static readonly Dictionary<int, LinearGradientBrush> lgBrushes = new Dictionary<int, LinearGradientBrush>();
        private static readonly Dictionary<int, Pen> pens = new Dictionary<int, Pen>();
        private static readonly Dictionary<int, SolidBrush> sBrushes = new Dictionary<int, SolidBrush>();

        static DrawingUtil()
        {
            string[] names = Enum.GetNames(typeof(KnownColor));
            knownColors = new Hashtable(names.Length);
            foreach (string str in names)
            {
                knownColors[str.ToLower()] = Color.Empty;
            }
        }

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        internal static void ClearAll()
        {
            foreach (KeyValuePair<int, LinearGradientBrush> pair in lgBrushes)
            {
                pair.Value.Dispose();
            }
            lgBrushes.Clear();
            foreach (KeyValuePair<int, SolidBrush> pair2 in sBrushes)
            {
                pair2.Value.Dispose();
            }
            sBrushes.Clear();
            foreach (KeyValuePair<int, Pen> pair3 in pens)
            {
                pair3.Value.Dispose();
            }
            pens.Clear();
            disabledImages.Clear();
        }

        internal static int Compare(Size one, Size two)
        {
            if (one.IsEmpty && two.IsEmpty)
            {
                return 0;
            }
            int num = one.Width + one.Height;
            return num.CompareTo((int) (two.Width + two.Height));
        }

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, Win32BitmapInfo pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);
        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        private static extern bool DeleteObject(IntPtr hObject);
        internal static void DrawGlowingText(Graphics graphics, string text, Font font, Rectangle bounds, Color color, TextFormatFlags flags)
        {
            Win32BitmapInfo info=new Win32BitmapInfo();
            IntPtr hdc = graphics.GetHdc();
            IntPtr hDC = CreateCompatibleDC(hdc);
            info = new Win32BitmapInfo {
                biSize = Marshal.SizeOf(info),
                biWidth = bounds.Width,
                biHeight = -bounds.Height,
                biPlanes = 1,
                biBitCount = 0x20,
                biCompression = 0
            };
            IntPtr hObject = CreateDIBSection(hdc, info, 0, 0, IntPtr.Zero, 0);
            SelectObject(hDC, hObject);
            IntPtr ptr4 = font.ToHfont();
            SelectObject(hDC, ptr4);
            VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
            Win32DrawThemeOptions pOptions = new Win32DrawThemeOptions {
                dwSize = Marshal.SizeOf(typeof(Win32DrawThemeOptions)),
                dwFlags = 0x2801,
                crText = ColorTranslator.ToWin32(color),
                iGlowSize = 5
            };
            Win32Rect pRect = new Win32Rect(0, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
            DrawThemeTextEx(renderer.Handle, hDC, 0, 0, text, -1, (int) flags, ref pRect, ref pOptions);
            BitBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, hDC, 0, 0, 0xcc0020);
            DeleteObject(ptr4);
            DeleteObject(hObject);
            DeleteDC(hDC);
            graphics.ReleaseHdc(hdc);
        }

        [DllImport("UxTheme.dll", CharSet=CharSet.Unicode)]
        private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref Win32Rect pRect, ref Win32DrawThemeOptions pOptions);
        [DllImport("dwmapi.dll")]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Win32Margins pMargins);
        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref bool pfEnabled);
        internal static bool Equals(Bitmap one, Bitmap two)
        {
            if ((one != null) || (two != null))
            {
                if ((one == null) || (two == null))
                {
                    return false;
                }
                if ((one.Width != two.Width) || (one.Height != two.Height))
                {
                    return false;
                }
                for (int i = 0; i < one.Width; i++)
                {
                    for (int j = 0; j < two.Height; j++)
                    {
                        if (one.GetPixel(i, j).ToArgb() != two.GetPixel(i, j).ToArgb())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        internal static Color GetColor(string color)
        {
            Color color2;
            if (SharedUtil.IsEmpty(color))
            {
                return Color.Transparent;
            }
            while (color.StartsWith("#"))
            {
                color = color.Substring(1);
            }
            if (!knownColors.ContainsKey(color = color.ToLower()))
            {
                return Color.FromArgb(int.Parse((color.Length == 6) ? ("FF" + color) : color, NumberStyles.HexNumber));
            }
            if ((color2 = (Color) knownColors[color]) == Color.Empty)
            {
                knownColors[color] = color2 = Color.FromName(color);
            }
            return color2;
        }

        internal static Image GetDisabledImage(Color backColor, Image image)
        {
            Duo<Color, Image> key = new Duo<Color, Image>(backColor, image);
            Bitmap bitmap = null;
            if ((image != null) && !disabledImages.TryGetValue(key, out bitmap))
            {
                disabledImages[key] = bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(backColor);
                    ControlPaint.DrawImageDisabled(graphics, image, 0, 0, backColor);
                }
            }
            return bitmap;
        }

        internal static Icon GetFileIcon(string filePath, bool large)
        {
            try
            {
                SHGFI shgfi;
                Win32FileInfo structure = new Win32FileInfo(true);
                int num = Marshal.SizeOf(structure);
                if (large)
                {
                    shgfi = SHGFI.Icon | SHGFI.UseFileAttributes;
                }
                else
                {
                    shgfi = SHGFI.Icon | SHGFI.UseFileAttributes | SHGFI.SmallIcon;
                }
                Win32FileInfo.SHGetFileInfo(filePath, 0x100, out structure, (uint) num, shgfi);
                return Icon.FromHandle(structure.hIcon);
            }
            catch
            {
                return null;
            }
        }

        internal static Image GetFileImage(string filePath, bool large)
        {
            return GetFileImage(filePath, large, Color.Transparent);
        }

        internal static Image GetFileImage(string filePath, bool large, Color backColor)
        {
            Icon fileIcon = GetFileIcon(filePath, large);
            Bitmap image = null;
            if (fileIcon != null)
            {
                image = new Bitmap(fileIcon.Width, fileIcon.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(backColor);
                    graphics.DrawIcon(fileIcon, new Rectangle(0, 0, fileIcon.Width, fileIcon.Height));
                }
            }
            return image;
        }

        internal static LinearGradientBrush GetLinearGradientBrush(Rectangle rect, Color one, Color two, LinearGradientMode mode)
        {
            LinearGradientBrush brush;
            int hashCode = SharedUtil.GetHashCode(new object[] { rect, one.ToArgb(), two.ToArgb(), mode });
            if (!lgBrushes.TryGetValue(hashCode, out brush))
            {
                lgBrushes[hashCode] = brush = new LinearGradientBrush(rect, one, two, mode);
            }
            return brush;
        }

        internal static Pen GetPen(Brush brush, float width)
        {
            Pen pen;
            int hashCode = SharedUtil.GetHashCode(new object[] { brush, width });
            if (!pens.TryGetValue(hashCode, out pen))
            {
                pens[hashCode] = pen = new Pen(brush, width);
            }
            return pen;
        }

        internal static Pen GetPen(Color color, float width)
        {
            Pen pen;
            int hashCode = SharedUtil.GetHashCode(new object[] { color.ToArgb(), width });
            if (!pens.TryGetValue(hashCode, out pen))
            {
                pens[hashCode] = pen = new Pen(color, width);
            }
            return pen;
        }

        internal static Point[] GetPoints(params int[] xy)
        {
            List<Point> list = new List<Point>(xy.Length / 2);
            for (int i = 1; i < xy.Length; i += 2)
            {
                list.Add(new Point(xy[i - 1], xy[i]));
            }
            return list.ToArray();
        }

        internal static RectangleF GetRectangle(Rectangle rect)
        {
            return new RectangleF((float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height);
        }

        internal static Rectangle GetRectangle(RectangleF rect)
        {
            return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
        }

        internal static SizeF GetSize(Size size)
        {
            return new SizeF((float) size.Width, (float) size.Height);
        }

        internal static Size GetSize(SizeF size)
        {
            return new Size((int) size.Width, (int) size.Height);
        }

        internal static SolidBrush GetSolidBrush(Color color)
        {
            SolidBrush brush;
            int key = color.ToArgb();
            if (!sBrushes.TryGetValue(key, out brush))
            {
                sBrushes[key] = brush = new SolidBrush(color);
            }
            return brush;
        }

        internal static void SaveImage(Image image, Stream stream, ImageFormat imageFormat, int jpegQuality)
        {
            SharedUtil.ThrowIfEmpty(image, "image");
            SharedUtil.ThrowIfEmpty(stream, "stream");
            SharedUtil.ThrowIfEmpty(imageFormat, "imageFormat");
            if (imageFormat == ImageFormat.Jpeg)
            {
                using (EncoderParameters parameters = new EncoderParameters(1))
                {
                    using (EncoderParameter parameter = new EncoderParameter(Encoder.Quality, (long) jpegQuality))
                    {
                        parameters.Param[0] = parameter;
                        image.Save(stream, JpegCodec, parameters);
                        stream.Flush();
                    }
                    return;
                }
            }
            using (MemoryStream stream2 = new MemoryStream())
            {
                image.Save(stream2, imageFormat);
                stream2.Seek(0L, SeekOrigin.Begin);
                stream2.WriteTo(stream);
                stream.Flush();
                stream2.Flush();
            }
        }

        internal static SizeF ScaleToHeight(SizeF size, float maxHeight)
        {
            float num = size.Height / maxHeight;
            if (num > 1f)
            {
                size = new SizeF(size.Width / num, maxHeight);
            }
            return size;
        }

        internal static SizeF ScaleToWidth(SizeF size, float maxWidth)
        {
            float num = size.Width / maxWidth;
            if (num > 1f)
            {
                size = new SizeF(maxWidth, size.Height / num);
            }
            return size;
        }

        [DllImport("gdi32.dll", ExactSpelling=true)]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        internal static ImageCodecInfo JpegCodec
        {
            get
            {
                ImageCodecInfo[] infoArray;
                if ((jpegCodec == null) && !SharedUtil.IsEmpty((ICollection) (infoArray = ImageCodecInfo.GetImageEncoders())))
                {
                    foreach (ImageCodecInfo info in infoArray)
                    {
                        if (info.MimeType == "image/jpeg")
                        {
                            jpegCodec = info;
                            break;
                        }
                    }
                }
                return jpegCodec;
            }
        }
    }
}

