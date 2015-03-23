namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using roxority.Shared.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    internal class FilePathTypeEditor<T> : UITypeEditor where T: FileDialog, new()
    {
        internal static Image NoFileImage;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (T local = Activator.CreateInstance<T>())
            {
                local.FileName = value as string;
                local.Filter = ParameterAttribute.GetValue(context.PropertyDescriptor, "Filter", local.Filter);
                if (local.ShowDialog() == DialogResult.OK)
                {
                    return local.FileName;
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            bool flag;
            string str = e.Value as string;
            if ((flag = !SharedUtil.IsEmpty(str)) || (FilePathTypeEditor<T>.NoFileImage != null))
            {
                e.Graphics.DrawImage(flag ? DrawingUtil.GetFileImage(str, false) : FilePathTypeEditor<T>.NoFileImage, new Rectangle(1, 2, e.Bounds.Width - 2, e.Bounds.Height - 2));
            }
            base.PaintValue(e);
        }
    }
}

