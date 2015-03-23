namespace roxority.Data
{
    using Microsoft.SharePoint;
    using roxority.Data.Providers;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web;

    public abstract class DataSource : IEnumerable, IEnumerator, IDisposable
    {
        private Guid? contextID = null;
        private System.Web.HttpContext httpContext;
        internal IDictionary inst;
        protected internal bool noDispose = true;
        protected internal static readonly Random rnd = new Random();
        private JsonSchemaManager.Schema schema;
        public const string SCHEMAPROP_ASMNAME = "roxority_Shared";
        public const string SCHEMAPROP_DEFAULTFIELDS = "pd";
        public const string SCHEMAPROP_PICTUREFIELD = "pp";
        public const string SCHEMAPROP_PREFIX = "rox___";
        public const string SCHEMAPROP_TITLEFIELD = "pt";
        public const string SCHEMAPROP_URLFIELD = "pu";
        private static readonly Dictionary<System.Type, DataSource> statics = new Dictionary<System.Type, DataSource>();

        protected DataSource()
        {
        }

        protected IDictionary AddSchemaProp(JsonSchemaManager.Schema owner, string pname, object ptype, IDictionary pmore)
        {
            return this.AddSchemaProp(owner, pname, ptype, null, false, pmore);
        }

        protected IDictionary AddSchemaProp(JsonSchemaManager.Schema owner, string pname, object ptype, string tab, bool sharedRes, IDictionary pmore)
        {
            IDictionary dictionary = new OrderedDictionary();
            if (string.IsNullOrEmpty(tab))
            {
                tab = "t__" + base.GetType().Name;
            }
            dictionary["type"] = ptype;
            dictionary["tab"] = tab;
            dictionary["show_in_summary"] = false;
            IDictionary dictionary2 = pmore["show_if"] as IDictionary;
            if (dictionary2 == null)
            {
                dictionary2 = new OrderedDictionary();
            }
            dictionary2["t"] = base.GetType().Name;
            dictionary["show_if"] = dictionary2;
            if (sharedRes)
            {
                dictionary["res_title"] = "PC_DataSources_" + pname;
                dictionary["res_desc"] = "PD_DataSources_" + pname;
            }
            if (pmore != null)
            {
                foreach (DictionaryEntry entry in pmore)
                {
                    dictionary[entry.Key] = entry.Value;
                }
            }
            owner.RawSchema[this.SchemaPropNamePrefix + "_" + pname] = dictionary;
            return dictionary;
        }

        public virtual bool AllowDateParsing(RecordProperty prop)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }

        public void DoDispose()
        {
            this.noDispose = false;
            this.Dispose();
        }

        public virtual string FixupTitle(string name)
        {
            if ((!DataSourceConsumer.L.expired && !DataSourceConsumer.L.broken) && !DataSourceConsumer.L.userBroken)
            {
                return name;
            }
            return ProductPage.GetResource("LicExpiry", new object[0]);
        }

        public static DataSource FromID(string id, bool farmScope, bool siteScope, string typeName)
        {
            bool flag = false;
            IDictionary inst = new OrderedDictionary();
            if (string.IsNullOrEmpty(id))
            {
                id = "default";
            }
            inst["id"] = id;
            using (ProductPage page = new ProductPage())
            {
                KeyValuePair<JsonSchemaManager, JsonSchemaManager> pair = JsonSchemaManager.TryGet(page, null, farmScope, siteScope, "roxority_Shared");
                foreach (JsonSchemaManager manager in new JsonSchemaManager[] { pair.Value, pair.Key })
                {
                    if (manager != null)
                    {
                        using (manager)
                        {
                            foreach (JsonSchemaManager.Schema schema in manager.AllSchemas.Values)
                            {
                                if ((schema == null) || !(schema.Name == "DataSources"))
                                {
                                    continue;
                                }
                                if ("new".Equals(id, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    return Load(schema, inst, typeName);
                                }
                                foreach (IDictionary dictionary2 in schema.GetInstances(SPContext.Current.Web, null, null))
                                {
                                    if (flag = (dictionary2 != null) && id.Equals(dictionary2["id"]))
                                    {
                                        foreach (DictionaryEntry entry in dictionary2)
                                        {
                                            inst[entry.Key] = entry.Value;
                                        }
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    return Load(schema, inst, typeName);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public virtual string GetFieldInfoUrl(string webUrl, Guid contextID)
        {
            return ProductPage.MergeUrlPaths(webUrl, string.Concat(new object[] { "_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=tools&tool=Tool_DataSources&r=", rnd.Next() }));
        }

        public virtual string GetKnownPropName(KnownProperty kp)
        {
            return string.Empty;
        }

        public virtual string GetPropertyDisplayName(RecordProperty prop)
        {
            string resource = ProductPage.GetResource("Disp_" + prop.Name, new object[0]);
            if (!string.IsNullOrEmpty(resource))
            {
                return resource;
            }
            return prop.Name;
        }

        public string GetPropertyDisplayName(string name)
        {
            string resource = ProductPage.GetResource("Disp_" + name, new object[0]);
            RecordProperty propertyByName = this.Properties.GetPropertyByName(name);
            if (propertyByName != null)
            {
                return this.GetPropertyDisplayName(propertyByName);
            }
            if (!string.IsNullOrEmpty(resource))
            {
                return resource;
            }
            return name;
        }

        public virtual RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            return new RecordPropertyValueCollection(this, rec, prop, null, null, () => 0, () => null);
        }

        public virtual Record GetRecord(long recID)
        {
            return null;
        }

        public virtual string GetRecordUri(Record rec)
        {
            return rec["rox___pu", string.Empty];
        }

        public static DataSource GetStatic(System.Type type, string typeName, ref Exception error)
        {
            DataSource source = null;
            if (type == null)
            {
                try
                {
                    type = ProductPage.Assembly.GetType("roxority.Data.Providers." + typeName, false, true);
                }
                catch (Exception exception)
                {
                    error = exception;
                }
            }
            if (((type != null) && !statics.TryGetValue(type, out source)) && ((source = New(type, ref error)) != null))
            {
                statics[type] = source;
            }
            return source;
        }

        public virtual void InitSchema(JsonSchemaManager.Schema owner)
        {
        }

        public virtual void InitSchemaForCaching(JsonSchemaManager.Schema owner, bool defVal, bool allowChange)
        {
            IDictionary pmore = new OrderedDictionary();
            IDictionary dictionary2 = new OrderedDictionary();
            dictionary2[this.SchemaPropNamePrefix + "_cc"] = "c";
            pmore["default"] = defVal;
            pmore["always_show_help"] = true;
            if (!allowChange)
            {
                pmore["readonly"] = true;
            }
            this.AddSchemaProp(owner, "cc", new ArrayList(new string[] { defVal ? "c" : "n", defVal ? "n" : "c" }), "u", true, pmore);
            pmore.Clear();
            pmore["show_if"] = dictionary2;
            pmore["validator"] = "roxValidateNumeric";
            pmore["default"] = "10";
            pmore["always_show_help"] = true;
            this.AddSchemaProp(owner, "cr", "String", "u", true, pmore);
            pmore.Clear();
            pmore["show_if"] = dictionary2;
            pmore["always_show_help"] = true;
            this.AddSchemaProp(owner, "cn", "Boolean", "u", true, pmore);
            pmore.Clear();
            pmore["show_if"] = dictionary2;
            pmore["always_show_help"] = true;
            this.AddSchemaProp(owner, "rc", "Boolean", "u", true, pmore);
            pmore.Clear();
            pmore["show_if"] = dictionary2;
            pmore["always_show_help"] = true;
            this.AddSchemaProp(owner, "cl", "ClearCache", "u", true, pmore);
        }

        public static DataSource Load(JsonSchemaManager.Schema schema, IDictionary inst, string typeName)
        {
            System.Type type = ProductPage.Assembly.GetType("roxority.Data.Providers." + (string.IsNullOrEmpty(typeName) ? inst["t"] : typeName), false, true);
            Exception error = null;
            DataSource source = (type == null) ? null : New(type, ref error);
            if (source != null)
            {
                source.schema = schema;
                source.schema.ShouldSerialize = new Converter<KeyValuePair<IDictionary, JsonSchemaManager.Property>, bool>(DataSource.ShouldSerialize);
                source.inst = inst;
                return source;
            }
            if (error != null)
            {
                throw error;
            }
            return source;
        }

        public virtual bool MoveNext()
        {
            return false;
        }

        public static DataSource New(System.Type type, ref Exception error)
        {
            try
            {
                return (Reflector.Current.New(type.FullName, new object[0]) as DataSource);
            }
            catch (Exception exception)
            {
                error = exception;
                return null;
            }
        }

        public virtual void Reset()
        {
        }

        public virtual string RewritePropertyName(string name)
        {
            foreach (string str2 in Enum.GetNames(typeof(KnownProperty)))
            {
                string str;
                if (str2.Equals(name, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(str = this.GetKnownPropName((KnownProperty) Enum.Parse(typeof(KnownProperty), name, true))))
                {
                    return str;
                }
            }
            return name;
        }

        public static bool ShouldSerialize(KeyValuePair<IDictionary, JsonSchemaManager.Property> kvp)
        {
            return true;
        }

        public int CacheRate
        {
            get
            {
                int num;
                if (int.TryParse(this["cr", string.Empty], out num) && (num >= 0))
                {
                    return num;
                }
                return 10;
            }
        }

        public bool CacheRefresh
        {
            get
            {
                return this["cn", false];
            }
        }

        public virtual Guid ContextID
        {
            get
            {
                if ((!this.contextID.HasValue || !this.contextID.HasValue) && (this.JsonInstance != null))
                {
                    this.contextID = new Guid?(ProductPage.GetGuid(this.JsonInstance["id"] + string.Empty, true));
                }
                return this.contextID.Value;
            }
        }

        public virtual long Count
        {
            get
            {
                return (this.RequireCaching ? ((long) (-1)) : ((long) 0));
            }
        }

        public virtual object Current
        {
            get
            {
                return null;
            }
        }

        public System.Web.HttpContext HttpContext
        {
            get
            {
                if (this.httpContext == null)
                {
                    this.httpContext = System.Web.HttpContext.Current;
                }
                return this.httpContext;
            }
        }

        public bool IsReal
        {
            get
            {
                return ((this.inst != null) && (this.schema != null));
            }
        }

        public bool this[string name, bool defVal]
        {
            get
            {
                object obj2 = this[name];
                if (obj2 is bool)
                {
                    return (bool) obj2;
                }
                return defVal;
            }
        }

        public object this[string name]
        {
            get
            {
                object defaultValue = null;
                JsonSchemaManager.Property property;
                string str = this.SchemaPropNamePrefix + "_" + name;
                if (this.inst != null)
                {
                    defaultValue = this.inst[str];
                }
                if (((defaultValue == null) && (this.schema != null)) && ((property = this.schema[str]) != null))
                {
                    defaultValue = property.DefaultValue;
                }
                return defaultValue;
            }
        }

        public string this[string name, string defVal]
        {
            get
            {
                object obj2 = this[name];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return defVal;
            }
        }

        public IDictionary JsonInstance
        {
            get
            {
                return this.inst;
            }
        }

        public JsonSchemaManager.Schema JsonSchema
        {
            get
            {
                return this.schema;
            }
        }

        public static IEnumerable<System.Type> KnownProviderTypes
        {
            get
            {
                yield return typeof(Dummy);
                yield return typeof(ListLocal);
                yield return typeof(ListRemote);
                yield return typeof(UserAccounts);
                yield return typeof(Directory);
                yield return typeof(Ado);
                yield return typeof(Hybrid);
            }
        }

        public virtual RecordPropertyCollection Properties
        {
            get
            {
                return new RecordPropertyCollection(this);
            }
        }

        public bool Recache
        {
            get
            {
                return this["rc", false];
            }
        }

        public virtual bool RequireCaching
        {
            get
            {
                return !"n".Equals(this["cc", "n"]);
            }
        }

        public virtual string SchemaPropNamePrefix
        {
            get
            {
                System.Type type = base.GetType();
                if (type != typeof(DataSource))
                {
                    return type.Name;
                }
                return "dsp";
            }
        }

        public static IEnumerable<string> SchemaProps
        {
            get
            {
                yield return "pp";
                yield return "pt";
                yield return "pu";
                yield return "pl";
                yield return "pm";
            }
        }



        public enum KnownProperty
        {
            FriendlyName,
            Picture
        }
    }
}

