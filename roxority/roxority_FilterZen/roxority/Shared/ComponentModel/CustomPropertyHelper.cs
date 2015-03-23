namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;

    internal class CustomPropertyHelper : IPropertyHelper
    {
        internal readonly string Category;
        internal readonly string Description;
        internal readonly string DisplayName;

        internal CustomPropertyHelper(string category, string description, string displayName)
        {
            this.Category = SharedUtil.IsEmpty(category) ? string.Empty : category;
            this.Description = SharedUtil.IsEmpty(description) ? string.Empty : description;
            this.DisplayName = SharedUtil.IsEmpty(displayName) ? string.Empty : displayName;
        }

        string IPropertyHelper.GetCategory(CustomPropertyDescriptor property, object owner)
        {
            return this.Category;
        }

        string IPropertyHelper.GetDescription(CustomPropertyDescriptor property, object owner)
        {
            return this.Description;
        }

        string IPropertyHelper.GetDisplayName(CustomPropertyDescriptor property, object owner)
        {
            return this.DisplayName;
        }
    }
}

