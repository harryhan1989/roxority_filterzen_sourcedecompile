namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        public static bool AllowBoolEditor = true;
        public bool CatchSetValue;
        public Type DefaultComponentType;
        public Operation<object, object> DefaultGetValue;
        public bool DefaultIsReadOnly;
        public Type DefaultPropertyType;
        public object DefaultValue;
        public readonly PropertyDescriptor Descriptor;
        public readonly IPropertyHelper Helper;
        public bool? ReadOnly;
        private readonly ICustomTypeDescriptor typeDesc;

        public CustomPropertyDescriptor(PropertyDescriptor propertyDescriptor, IPropertyHelper helper, ICustomTypeDescriptor typeDesc) : base(propertyDescriptor)
        {
            this.CatchSetValue = true;
            this.ReadOnly = null;
            this.Helper = helper;
            this.typeDesc = typeDesc;
            this.Descriptor = propertyDescriptor;
            if (AllowBoolEditor && (this.PropertyType == typeof(bool)))
            {
                this.AttributeArray = new List<Attribute>(this.AttributeArray) { new TypeConverterAttribute(typeof(BooleanTypeConverter)) }.ToArray();
            }
        }

        public CustomPropertyDescriptor(string name, Attribute[] attributes, IPropertyHelper helper, ICustomTypeDescriptor typeDesc) : base(name, attributes)
        {
            this.CatchSetValue = true;
            this.ReadOnly = null;
            this.Helper = helper;
            this.typeDesc = typeDesc;
            this.Descriptor = null;
        }

        public override bool CanResetValue(object component)
        {
            return ((this.Descriptor != null) && this.Descriptor.CanResetValue(component));
        }

        public static string GetDescription(Enum value)
        {
            DescriptionAttribute attribute;
            string name = value.ToString();
            object[] customAttributes = value.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), true);
            if ((!SharedUtil.IsEmpty((ICollection) customAttributes) && ((attribute = customAttributes[0] as DescriptionAttribute) != null)) && !SharedUtil.IsEmpty(attribute.Description))
            {
                return attribute.Description;
            }
            return name;
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (AllowBoolEditor && (this.PropertyType == typeof(bool)))
            {
                return new BooleanTypeEditor();
            }
            return base.GetEditor(editorBaseType);
        }

        public override object GetValue(object component)
        {
            if (this.Descriptor != null)
            {
                return this.Descriptor.GetValue(component);
            }
            if (this.DefaultGetValue != null)
            {
                return this.DefaultGetValue(component);
            }
            return this.DefaultValue;
        }

        public override void ResetValue(object component)
        {
            if (this.Descriptor != null)
            {
                this.Descriptor.ResetValue(component);
            }
        }

        public override void SetValue(object component, object value)
        {
            string str = value + string.Empty;
            bool flag = false;
            if (this.Descriptor != null)
            {
                if (((this.Descriptor.PropertyType == typeof(bool)) && !string.IsNullOrEmpty(str)) && (BooleanTypeConverter.FalseString.Equals(str, StringComparison.InvariantCultureIgnoreCase) || (flag = BooleanTypeConverter.TrueString.Equals(str, StringComparison.InvariantCultureIgnoreCase))))
                {
                    value = flag;
                }
                if (!this.CatchSetValue)
                {
                    this.Descriptor.SetValue(component, value);
                }
                else
                {
                    try
                    {
                        this.Descriptor.SetValue(component, value);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            if (this.Descriptor == null)
            {
                return false;
            }
            return this.Descriptor.ShouldSerializeValue(component);
        }

        public override string Category
        {
            get
            {
                if ((this.Helper != null) && (this.typeDesc != null))
                {
                    return this.Helper.GetCategory(this, this.typeDesc.GetPropertyOwner(this));
                }
                if (this.Descriptor != null)
                {
                    return this.Descriptor.Category;
                }
                return base.Category;
            }
        }

        public override Type ComponentType
        {
            get
            {
                if (this.Descriptor != null)
                {
                    return this.Descriptor.ComponentType;
                }
                return this.DefaultComponentType;
            }
        }

        public override string Description
        {
            get
            {
                if ((this.Helper != null) && (this.typeDesc != null))
                {
                    return this.Helper.GetDescription(this, this.typeDesc.GetPropertyOwner(this));
                }
                if (this.Descriptor != null)
                {
                    return this.Descriptor.Description;
                }
                return base.Description;
            }
        }

        public override string DisplayName
        {
            get
            {
                if ((this.Helper != null) && (this.typeDesc != null))
                {
                    return this.Helper.GetDisplayName(this, this.typeDesc.GetPropertyOwner(this));
                }
                if (this.Descriptor != null)
                {
                    return this.Descriptor.DisplayName;
                }
                return base.DisplayName;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                if (this.ReadOnly.HasValue && this.ReadOnly.HasValue)
                {
                    return this.ReadOnly.Value;
                }
                if (this.Descriptor != null)
                {
                    return this.Descriptor.IsReadOnly;
                }
                return this.DefaultIsReadOnly;
            }
        }

        public override Type PropertyType
        {
            get
            {
                if (this.Descriptor != null)
                {
                    return this.Descriptor.PropertyType;
                }
                return this.DefaultPropertyType;
            }
        }
    }
}

