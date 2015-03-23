namespace roxority.Shared.ComponentModel
{
    using System;

    public interface IPropertyHelper
    {
        string GetCategory(CustomPropertyDescriptor property, object owner);
        string GetDescription(CustomPropertyDescriptor property, object owner);
        string GetDisplayName(CustomPropertyDescriptor property, object owner);
    }
}

