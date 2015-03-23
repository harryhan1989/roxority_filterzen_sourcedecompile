namespace roxority.Shared
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly)]
    internal sealed class ResourceBaseNameAttribute : Attribute
    {
        internal readonly string ResourceBaseName;

        internal ResourceBaseNameAttribute(string resourceBaseName)
        {
            this.ResourceBaseName = SharedUtil.IsEmpty(resourceBaseName) ? string.Empty : resourceBaseName.Trim();
        }
    }
}

