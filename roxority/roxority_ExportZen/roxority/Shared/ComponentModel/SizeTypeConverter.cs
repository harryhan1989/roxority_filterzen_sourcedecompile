namespace roxority.Shared.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;

    internal class SizeTypeConverter : SizeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Size size;
            if (!(value is Size) || (destinationType != typeof(string)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Size size2 = size = (Size) value;
            return string.Format("{0} \x00d7 {1}", size2.Width, size.Height);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}

