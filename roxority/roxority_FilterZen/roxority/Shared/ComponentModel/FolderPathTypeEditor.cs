namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms;

    internal class FolderPathTypeEditor : UITypeEditor
    {
        internal static Image FolderImage;
        internal static Image NoFolderImage;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = context.PropertyDescriptor.Description;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.SelectedPath = value as string;
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
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
            if ((flag = ((FolderImage != null) && !SharedUtil.IsEmpty(str)) && Directory.Exists(str)) || (NoFolderImage != null))
            {
                e.Graphics.DrawImage(flag ? FolderImage : NoFolderImage, new Rectangle(1, 2, e.Bounds.Width - 2, e.Bounds.Height - 2));
            }
            base.PaintValue(e);
        }
    }
}

