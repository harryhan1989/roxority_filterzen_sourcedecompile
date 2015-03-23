namespace roxority.Data.Providers
{
    using roxority.Data;
    using System;

    public class ListRemote : DataSource
    {
        public override string SchemaPropNamePrefix
        {
            get
            {
                return "lr";
            }
        }
    }
}

