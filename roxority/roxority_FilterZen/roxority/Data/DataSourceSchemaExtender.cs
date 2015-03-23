namespace roxority.Data
{
    using roxority.SharePoint;
    using System;

    public class DataSourceSchemaExtender : JsonSchemaManager.ISchemaExtender
    {
        public void InitSchema(JsonSchemaManager.Schema owner)
        {
            Exception error = null;
            foreach (System.Type type in DataSource.KnownProviderTypes)
            {
                DataSource source = DataSource.GetStatic(type, null, ref error);
                if (source != null)
                {
                    source.InitSchema(owner);
                }
            }
        }
    }
}

