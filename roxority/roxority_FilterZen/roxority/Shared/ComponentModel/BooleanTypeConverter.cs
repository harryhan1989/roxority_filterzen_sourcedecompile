namespace roxority.Shared.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class BooleanTypeConverter : BooleanConverter
    {
        internal static string FalseString = "No";
        internal static string TrueString = "Yes";

        public BooleanTypeConverter()
        {
            new object();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType != typeof(string))
            {
                return base.CanConvertFrom(context, sourceType);
            }
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.CanConvertTo(context, destinationType);
            }
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return false;
            }
            if ((value is string) && TrueString.Equals((string) value, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if ((value is string) && FalseString.Equals((string) value, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(value is bool) || (destinationType != typeof(string)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (!((bool) value))
            {
                return FalseString;
            }
            return TrueString;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(new string[] { TrueString, FalseString });
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            if (value != null)
            {
                bool flag = true;
                if (!flag.Equals(value))
                {
                    bool flag2 = false;
                    if (!flag2.Equals(value))
                    {
                        if (!(value is string))
                        {
                            return false;
                        }
                        if (!TrueString.Equals((string) value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return FalseString.Equals((string) value, StringComparison.InvariantCultureIgnoreCase);
                        }
                        return true;
                    }
                }
            }
            return true;
        }
    }
}

