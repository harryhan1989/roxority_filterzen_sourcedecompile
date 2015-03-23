namespace roxority.Shared.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    internal class BooleanTypeEditor : UITypeEditor
    {
        internal static Image FalseImage;
        internal static Image TrueImage;

        public BooleanTypeEditor()
        {
            new object();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (!(e.Value is bool))
            {
                base.PaintValue(e);
            }
            else if ((TrueImage != null) && (FalseImage != null))
            {
                e.Graphics.DrawImage(((bool) e.Value) ? TrueImage : FalseImage, e.Bounds);
            }
            else
            {
                Size size;
                Size size2 = size = CheckBoxRenderer.GetGlyphSize(e.Graphics, ((bool) e.Value) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
                if (size2.Height <= e.Bounds.Height)
                {
                    if ((bool) e.Value)
                    {
                        CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.X + ((e.Bounds.Width - size.Width) / 2), e.Bounds.Y + ((e.Bounds.Height - size.Height) / 2)), Rectangle.Empty, string.Empty, null, false, CheckBoxState.CheckedNormal);
                    }
                    else
                    {
                        CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.X + ((e.Bounds.Width - size.Width) / 2), e.Bounds.Y + ((e.Bounds.Height - size.Height) / 2)), Rectangle.Empty, string.Empty, null, false, CheckBoxState.UncheckedNormal);
                    }
                }
            }
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return false;
            }
        }
    }
}

