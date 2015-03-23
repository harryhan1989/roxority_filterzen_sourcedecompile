namespace roxority.Data.Providers
{
    using roxority.Data;
    using System;

    public class Bdc : DataSource
    {
        public override string SchemaPropNamePrefix
        {
            get
            {
                return "bd";
            }
        }
    }
}

