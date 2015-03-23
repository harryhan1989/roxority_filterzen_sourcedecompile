namespace roxority.Data.Providers
{
    using roxority.Data;
    using System;

    public class ListLocal : DataSource
    {
        public override string SchemaPropNamePrefix
        {
            get
            {
                return "ll";
            }
        }
    }
}

