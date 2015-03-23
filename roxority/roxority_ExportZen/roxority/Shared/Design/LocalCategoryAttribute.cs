namespace roxority.Shared.Design
{
    using roxority.Shared;
    using System;
    using System.ComponentModel;
    using System.Resources;

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class LocalCategoryAttribute : CategoryAttribute
    {
        private string category;
        private bool loaded;
        private const string RESOURCENAME_SUFFIX = "Category";
        private readonly Type type;

        internal LocalCategoryAttribute(Type type, string category) : base(category)
        {
            this.category = string.Empty;
            this.category = SharedUtil.Trim(category);
            SharedUtil.ThrowIfNull(this.type = type, "type");
        }

        protected override string GetLocalizedString(string value)
        {
            string category = this.category;
            if (!this.loaded)
            {
                try
                {
                    ResourceManager resourceManager = SharedUtil.GetResourceManager(this.type.Assembly);
                    string[] resourceNames = this.GetResourceNames();
                    for (int i = 0; i < resourceNames.Length; i++)
                    {
                        if (!SharedUtil.IsEmpty(category = SharedUtil.Trim(SharedUtil.GetString(resourceManager, resourceNames[i], new object[0]))))
                        {
                            break;
                        }
                    }
                    if ((SharedUtil.IsEmpty(category) && (this.type.BaseType != null)) && (this.type.BaseType != typeof(object)))
                    {
                        category = new LocalCategoryAttribute(this.type.BaseType, this.category).Category;
                    }
                }
                catch
                {
                }
                finally
                {
                    if (SharedUtil.IsEmpty(category))
                    {
                        category = this.category;
                    }
                    else
                    {
                        this.category = category;
                    }
                    this.loaded = true;
                }
            }
            return category;
        }

        private string[] GetResourceNames()
        {
            return new string[] { (this.category + '.' + "Category"), ("Category" + '.' + this.category) };
        }
    }
}

