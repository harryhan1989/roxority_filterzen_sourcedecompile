namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Threading;
    using System.Reflection;
    using System.Resources;

    internal class EnumTypeConverter : EnumConverter
    {
        private readonly Dictionary<string, string> translations;

        internal static  event EventHandler<EventArgs<Assembly, ResourceManager>> ResolveResourceManager;

        internal EnumTypeConverter(Type enumType) : base(enumType)
        {
            this.translations = new Dictionary<string, string>();
            EventArgs<Assembly, ResourceManager> e = new EventArgs<Assembly, ResourceManager>(enumType.Assembly, null);
            if (ResolveResourceManager != null)
            {
                ResolveResourceManager(this, e);
            }
            foreach (string str in Enum.GetNames(base.EnumType))
            {
                this.translations[str] = (e.Value2 == null) ? str : e.Value2.GetString(string.Format("E_{0}_{1}", enumType.Name, str));
            }
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
            if (value is string)
            {
                foreach (KeyValuePair<string, string> pair in this.translations)
                {
                    if (pair.Value.Equals((string) value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return Enum.Parse(base.EnumType, pair.Key, true);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is string)
                {
                    foreach (KeyValuePair<string, string> pair in this.translations)
                    {
                        if (pair.Value.Equals((string) value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return pair.Key;
                        }
                    }
                    return base.ConvertTo(context, culture, value, destinationType);
                }
                if (value != null)
                {
                    return this.translations[value.ToString()];
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(this.translations.Values);
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
            return (((value is string) && (this.translations.ContainsKey((string) value) || this.translations.ContainsValue((string) value))) || base.IsValid(context, value));
        }
    }
}

