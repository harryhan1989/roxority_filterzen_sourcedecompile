namespace roxority.Shared.Design
{
    using roxority.Shared;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Resources;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    internal sealed class DisplayNameAttribute : Attribute
    {
        private string displayName;
        private readonly string memberName;
        private const string RESOURCENAME_SUFFIX = "DisplayName";
        private readonly Type type;

        internal DisplayNameAttribute(string displayName)
        {
            this.displayName = SharedUtil.Trim(displayName);
            this.memberName = string.Empty;
            this.type = null;
        }

        internal DisplayNameAttribute(Type type, string memberName)
        {
            this.type = type;
            if (this.type == null)
            {
                throw new ArgumentNullException("type");
            }
            SharedUtil.ThrowIfEmpty(this.memberName = memberName, "memberName");
        }

        internal static string GetDisplayName(PropertyDescriptor propDesc)
        {
            roxority.Shared.Design.DisplayNameAttribute displayNameAttribute = GetDisplayNameAttribute(propDesc);
            if (displayNameAttribute != null)
            {
                return displayNameAttribute.DisplayName;
            }
            return propDesc.DisplayName;
        }

        internal static string GetDisplayName(MemberInfo memberInfo)
        {
            roxority.Shared.Design.DisplayNameAttribute attribute = null;
            foreach (Attribute attribute2 in memberInfo.GetCustomAttributes(true))
            {
                attribute = attribute2 as roxority.Shared.Design.DisplayNameAttribute;
                if (attribute != null)
                {
                    break;
                }
            }
            if (attribute != null)
            {
                return attribute.DisplayName;
            }
            return memberInfo.Name;
        }

        internal static roxority.Shared.Design.DisplayNameAttribute GetDisplayNameAttribute(PropertyDescriptor propDesc)
        {
            roxority.Shared.Design.DisplayNameAttribute attribute = null;
            foreach (Attribute attribute2 in propDesc.Attributes)
            {
                attribute = attribute2 as roxority.Shared.Design.DisplayNameAttribute;
                if (attribute != null)
                {
                    return attribute;
                }
            }
            return attribute;
        }

        private string[] GetResourceNames()
        {
            return new string[] { (this.memberName + '.' + "DisplayName"), string.Concat(new object[] { this.type.Name, '.', this.memberName, '.', "DisplayName" }), string.Concat(new object[] { this.type.FullName, '.', this.memberName, '.', "DisplayName" }), string.Concat(new object[] { this.type.FullName.Replace('+', '.'), '.', this.memberName, '.', "DisplayName" }) };
        }

        internal string DisplayName
        {
            get
            {
                if (((this.displayName == null) && (this.type != null)) && !SharedUtil.IsEmpty(this.memberName))
                {
                    try
                    {
                        ResourceManager resourceManager = SharedUtil.GetResourceManager(this.type.Assembly);
                        string[] resourceNames = this.GetResourceNames();
                        for (int i = 0; i < resourceNames.Length; i++)
                        {
                            if (!SharedUtil.IsEmpty(this.displayName = SharedUtil.Trim(SharedUtil.GetString(resourceManager, resourceNames[i], new object[0]))))
                            {
                                break;
                            }
                        }
                        if (SharedUtil.IsEmpty(this.displayName))
                        {
                            if ((this.type.BaseType == null) || (this.type.BaseType == typeof(object)))
                            {
                                this.displayName = this.memberName;
                            }
                            else
                            {
                                this.displayName = new roxority.Shared.Design.DisplayNameAttribute(this.type.BaseType, this.memberName).DisplayName;
                            }
                        }
                    }
                    catch
                    {
                        this.displayName = this.memberName;
                    }
                }
                return this.displayName;
            }
        }
    }
}

