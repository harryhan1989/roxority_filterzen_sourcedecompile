namespace roxority.Shared.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ProvideProperty("FontStyle", typeof(Control))]
    internal class FontStyleProvider : Component, IExtenderProvider
    {
        private readonly Dictionary<Control, FontStyle> controls = new Dictionary<Control, FontStyle>();

        internal static Font GetFont(Control control)
        {
            if (control.Font != null)
            {
                return control.Font;
            }
            if (control.Parent != null)
            {
                return GetFont(control.Parent);
            }
            return SystemFonts.DefaultFont;
        }

        [ExtenderProvidedProperty, DefaultValue(0), Localizable(false)]
        internal FontStyle GetFontStyle(Control control)
        {
            FontStyle style;
            if (!this.controls.TryGetValue(control, out style))
            {
                return FontStyle.Regular;
            }
            return style;
        }

        internal void RefreshControlFonts()
        {
            foreach (KeyValuePair<Control, FontStyle> pair in this.controls)
            {
                if ((pair.Key.Font == null) || (pair.Key.Font.Style != ((FontStyle) pair.Value)))
                {
                    Font font = GetFont(pair.Key);
                    pair.Key.Font = new Font(font, pair.Value);
                }
            }
        }

        [ExtenderProvidedProperty, Localizable(false), DefaultValue(0)]
        internal void SetFontStyle(Control control, FontStyle fontStyle)
        {
            if (fontStyle == FontStyle.Regular)
            {
                this.controls.Remove(control);
            }
            else
            {
                this.controls[control] = fontStyle;
            }
        }

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return (extendee is Control);
        }
    }
}

