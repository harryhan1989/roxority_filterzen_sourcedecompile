namespace roxority.Shared.Design
{
    using roxority.Shared;
    using System;
    using System.ComponentModel;
    using System.Resources;

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class LocalDescriptionAttribute : DescriptionAttribute
    {
        private string description;
        private readonly string memberName;
        private const string RESOURCENAME_SUFFIX = "Description";
        private readonly Type type;

        internal LocalDescriptionAttribute(string description) : base(description)
        {
            this.description = string.Empty;
            this.description = SharedUtil.Trim(description);
            this.memberName = string.Empty;
            this.type = null;
        }

        internal LocalDescriptionAttribute(Type type, string memberName)
        {
            this.description = string.Empty;
            this.type = type;
            if (this.type == null)
            {
                throw new ArgumentNullException("type");
            }
            this.memberName = SharedUtil.Trim(memberName);
        }

        internal static string GetLocalDescription(Enum member)
        {
            string description = new LocalDescriptionAttribute(member.GetType(), member.ToString()).Description;
            if (!SharedUtil.IsEmpty(ref description))
            {
                return description;
            }
            return member.ToString();
        }

        private string[] GetResourceNames()
        {
            return new string[] { string.Concat(new object[] { this.type.FullName.Replace('+', '.'), '.', this.memberName, '.', "Description" }), string.Concat(new object[] { this.type.Name, '.', this.memberName, '.', "Description" }), (this.memberName + '.' + "Description"), (this.type.FullName.Replace('+', '.') + '.' + this.memberName), (this.type.Name + '.' + this.memberName), this.memberName };
        }

        public override string Description
        {
            get
            {
                if ((SharedUtil.IsEmpty(this.description) && (this.type != null)) && !SharedUtil.IsEmpty(this.memberName))
                {
                    try
                    {
                        ResourceManager resourceManager = SharedUtil.GetResourceManager(this.type.Assembly);
                        string[] resourceNames = this.GetResourceNames();
                        for (int i = 0; i < resourceNames.Length; i++)
                        {
                            if (!SharedUtil.IsEmpty(this.description = SharedUtil.Trim(SharedUtil.GetString(resourceManager, resourceNames[i], new object[0]))))
                            {
                                break;
                            }
                        }
                        if (SharedUtil.IsEmpty(this.description))
                        {
                            if ((this.type.BaseType == null) || (this.type.BaseType == typeof(object)))
                            {
                                this.description = this.memberName;
                            }
                            else
                            {
                                this.description = new LocalDescriptionAttribute(this.type.BaseType, this.memberName).Description;
                            }
                        }
                    }
                    catch
                    {
                        this.description = this.memberName;
                    }
                }
                return this.description;
            }
        }
    }
}

