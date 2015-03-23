namespace roxority.SharePoint.JsonSchemaPropertyTypes
{
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DataProvider : JsonSchemaManager.Property.Type.Choice
    {
        public DataProvider() : base(null)
        {
        }

        public override IEnumerable GetChoices(IDictionary rawSchema)
        {
            foreach (System.Type iteratorVariable0 in DataSource.KnownProviderTypes)
            {
                yield return iteratorVariable0.Name;
            }
        }

        public override string CssClass
        {
            get
            {
                return ("rox-iteminst-edit-control rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.Choice).Name);
            }
        }

    }
}

