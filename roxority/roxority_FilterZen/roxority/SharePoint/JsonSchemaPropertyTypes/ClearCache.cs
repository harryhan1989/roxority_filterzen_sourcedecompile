namespace roxority.SharePoint.JsonSchemaPropertyTypes
{
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Web;

    public class ClearCache : JsonSchemaManager.Property.Type.Boolean
    {
        public override void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context, string formKey)
        {
            DataSourceCache cache;
            DataSource source;
            string key = inst["id"] + string.Empty;
            if ((!string.IsNullOrEmpty(context.Request[formKey]) && (DataSourceConsumer.dsCaches != null)) && (DataSourceConsumer.dsCaches.TryGetValue(key, out cache) || (((source = DataSource.FromID(key, true, true, null)) != null) && DataSourceConsumer.dsCaches.TryGetValue(ProductPage.GuidLower(source.ContextID, false), out cache))))
            {
                cache.Clear();
            }
        }
    }
}

