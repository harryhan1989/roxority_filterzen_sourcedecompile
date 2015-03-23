namespace roxority.Data.Providers
{
    using roxority.Data;
    using System;

    public class Hybrid : DataSource
    {
        public override string SchemaPropNamePrefix
        {
            get
            {
                return "hy";
            }
        }
    }
}

