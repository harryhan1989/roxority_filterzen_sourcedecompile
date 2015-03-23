namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;
    using System.ComponentModel;
    using System.Resources;

    internal class ParameterAttribute : Attribute
    {
        private string name;
        internal const string PREFIX_RES = "res::";
        private string value;

        internal ParameterAttribute(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        internal static string GetValue(PropertyDescriptor propertyDescriptor, string name, string defaultValue)
        {
            ResourceManager manager;
            string str = defaultValue;
            if (propertyDescriptor.Attributes != null)
            {
                foreach (Attribute attribute in propertyDescriptor.Attributes)
                {
                    ParameterAttribute attribute2;
                    if (((attribute2 = attribute as ParameterAttribute) != null) && (attribute2.Name == name))
                    {
                        str = attribute2.Value;
                        break;
                    }
                }
            }
            if (((str != null) && str.StartsWith("res::")) && ((manager = SharedUtil.GetResourceManager(propertyDescriptor.ComponentType.Assembly)) != null))
            {
                str = manager.GetString(str.Substring("res::".Length));
            }
            return str;
        }

        internal string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        internal string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}

