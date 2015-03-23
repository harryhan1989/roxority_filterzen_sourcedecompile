namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;
    using Microsoft.SharePoint.Utilities;
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class JsonSchemaManager : IDisposable
    {
        public readonly Dictionary<string, Schema> AllSchemas = new Dictionary<string, Schema>();
        internal string assemblyName = ProductPage.AssemblyName;
        private SPSite dispSite;
        public readonly bool Elevated = ProductPage.Elevated;
        private SPFarm farm;
        [ThreadStatic]
        private static ProductPage.LicInfo li = null;
        [ThreadStatic]
        private static bool? li2 = null;
        [ThreadStatic]
        private static bool? li4 = null;
        [ThreadStatic]
        internal static bool noSave = false;
        public readonly ProductPage ProdPage;
        public readonly IDictionary RawSchema;
        private SPSite site;
        private bool siteScope;
        public readonly string Source;
        private Hashtable store;
        private List<SPListTemplate> templates;
        private SPWeb web;
        private SPWebApplication webApp;

        public JsonSchemaManager(ProductPage prodPage, string filePath, bool siteScope, string asmName)
        {
            if (!string.IsNullOrEmpty(asmName))
            {
                this.assemblyName = asmName;
            }
            this.ProdPage = prodPage;
            this.siteScope = siteScope;
            if ((string.IsNullOrEmpty(filePath) && (prodPage != null)) && ((prodPage.Server != null) && !File.Exists(filePath = prodPage.Server.MapPath("/_layouts/" + ProductPage.AssemblyName + "/schemas.json"))))
            {
                filePath = prodPage.Server.MapPath("/_layouts/" + ProductPage.AssemblyName + "/schemas.tl.json");
            }
            using (StreamReader reader = File.OpenText(filePath))
            {
                reader.ReadLine();
                this.RawSchema = JSON.JsonDecode(this.Source = reader.ReadToEnd(), typeof(OrderedDictionary)) as IDictionary;
                foreach (DictionaryEntry entry in this.RawSchema)
                {
                    string name = entry.Key + string.Empty;
                    this.AllSchemas[entry.Key + string.Empty] = new Schema(this, name, entry.Value as IDictionary);
                }
            }
        }

        internal static bool Bool(object val, bool defVal)
        {
            if (val is bool)
            {
                return (bool) val;
            }
            return defVal;
        }

        internal static IDictionary CloneDictionary(Schema schema, IDictionary dic, int level)
        {
            IDictionary dictionary = new OrderedDictionary();
            Converter<KeyValuePair<IDictionary, Property>, bool> shouldSerialize = schema.ShouldSerialize;
            foreach (DictionaryEntry entry in dic)
            {
                IDictionary dictionary2 = entry.Value as IDictionary;
                if (dictionary2 != null)
                {
                    dictionary[entry.Key] = CloneDictionary(schema, dictionary2, level + 1);
                }
                else
                {
                    Property property;
                    if ((level == 1) && ((property = schema[entry.Key + string.Empty]) != null))
                    {
                        if ((shouldSerialize == null) || shouldSerialize(new KeyValuePair<IDictionary, Property>(dic, property)))
                        {
                            string str;
                            if (Property.Type.String.IsPassword(property.RawSchema) && !string.IsNullOrEmpty(str = entry.Value + string.Empty))
                            {
                                dictionary[entry.Key] = Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(str), ProductPage.Assembly.GetName().GetPublicKeyToken(), DataProtectionScope.LocalMachine), Base64FormattingOptions.None);
                            }
                            else
                            {
                                dictionary[entry.Key] = entry.Value;
                            }
                        }
                    }
                    else
                    {
                        dictionary[entry.Key] = entry.Value;
                    }
                }
            }
            return dictionary;
        }

        public static string[] DiscoverSchemaFiles(HttpContext context)
        {
            string[] strArray = null;
            try
            {
                strArray = Directory.GetFiles(context.Server.MapPath("/_layouts/" + ProductPage.AssemblyName + "/"), "*.json", SearchOption.TopDirectoryOnly);
            }
            catch
            {
            }
            if (strArray != null)
            {
                return strArray;
            }
            return new string[0];
        }

        public void Dispose()
        {
            if (this.dispSite != null)
            {
                this.dispSite.Dispose();
                this.dispSite = null;
            }
        }

        ~JsonSchemaManager()
        {
            this.Dispose();
        }

        public static string GetDisplayName(IDictionary inst, string schemaName, bool mergeNameAndTitle)
        {
            string str = inst["name"] + string.Empty;
            string str2 = inst["title"] + string.Empty;
            string resource = ProductPage.GetResource("Tool_ItemEditor_Untitled", new object[] { ProductPage.GetProductResource("Tool_" + schemaName + "_TitleSingular", new object[0]), inst["id"] });
            if ((mergeNameAndTitle && !string.IsNullOrEmpty(str)) && !string.IsNullOrEmpty(str2))
            {
                resource = str + " [\"" + str2 + "\"]";
            }
            else if (!string.IsNullOrEmpty(str2))
            {
                resource = str2;
            }
            else if (!string.IsNullOrEmpty(str))
            {
                resource = str;
            }
            if (!resource.Contains("$Resources:"))
            {
                return resource;
            }
            return (GetDisplayValue(resource) + string.Empty);
        }

        public static object GetDisplayValue(object value)
        {
            string str = value as string;
            string str3 = "$Resources:";
            if (!string.IsNullOrEmpty(str))
            {
                int num;
                while ((num = str.IndexOf(str3, StringComparison.InvariantCultureIgnoreCase)) >= 0)
                {
                    string str2;
                    int index = str.IndexOf(' ', num + str3.Length);
                    if (index < 0)
                    {
                        str2 = str.Substring(num + str3.Length);
                    }
                    else
                    {
                        str2 = str.Substring(num, (index - str3.Length) - num);
                    }
                    str = str.Replace(str3 + str2, ProductPage.GetProductResource(str2, new object[0]));
                }
                value = str;
            }
            return value;
        }

        public static ICollection<IDictionary> GetInstances(string fpath, string schemaName)
        {
            return GetInstances(fpath, schemaName, null);
        }

        public static ICollection<IDictionary> GetInstances(string fpath, string schemaName, string asmName)
        {
            SPSecurity.CodeToRunElevated code = null;
            Dictionary<object, IDictionary> actions = new Dictionary<object, IDictionary>();
            try
            {
                if (code == null)
                {
                    code = delegate {
                        using (ProductPage page = new ProductPage())
                        {
                            IEnumerable<IDictionary> enumerable = GetInstances(page, fpath, schemaName, null, null, null, true, true, true, asmName);
                            if (enumerable != null)
                            {
                                foreach (IDictionary dictionary in enumerable)
                                {
                                    actions[dictionary["id"]] = dictionary;
                                }
                            }
                        }
                    };
                }
                ProductPage.Elevate(code, true, true);
            }
            catch
            {
            }
            return actions.Values;
        }

        public static IEnumerable<IDictionary> GetInstances(ProductPage prodPage, string filePath, string schemaName, SPWeb web, SPList list, SPView view, bool farmScoped, bool siteScoped, bool reThrow)
        {
            return GetInstances(prodPage, filePath, schemaName, web, list, view, farmScoped, siteScoped, reThrow, null);
        }

        public static IEnumerable<IDictionary> GetInstances(ProductPage prodPage, string filePath, string schemaName, SPWeb web, SPList list, SPView view, bool farmScoped, bool siteScoped, bool reThrow, string asmName)
        {
            List<IDictionary> results = new List<IDictionary>();
            if (farmScoped || siteScoped)
            {
                ProductPage.Elevate(delegate {
                    bool elevated = ProductPage.Elevated;
                    JsonSchemaManager manager = null;
                    Schema schema = null;
                    results.Clear();
                    try
                    {
                        try
                        {
                            manager = new JsonSchemaManager(prodPage, filePath, !farmScoped, asmName);
                            if (!manager.AllSchemas.TryGetValue(schemaName, out schema))
                            {
                                manager = null;
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            if (!ProductPage.Elevated)
                            {
                                throw;
                            }
                        }
                        catch
                        {
                            if (reThrow)
                            {
                                throw;
                            }
                        }
                        if (schema != null)
                        {
                            IEnumerable<IDictionary> enumerable = schema.GetInstances(web, list, view);
                            if (enumerable != null)
                            {
                                foreach (IDictionary dictionary in enumerable)
                                {
                                    results.Add(dictionary);
                                }
                            }
                            if (farmScoped && siteScoped)
                            {
                                manager.SiteScope = true;
                                enumerable = schema.GetInstances(web, list, view);
                                if (enumerable != null)
                                {
                                    foreach (IDictionary dictionary2 in enumerable)
                                    {
                                        results.Add(dictionary2);
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (manager != null)
                        {
                            manager.Dispose();
                        }
                    }
                }, true);
            }
            return results;
        }

        internal SPListTemplate GetListTemplate(string name)
        {
            foreach (SPListTemplate template in this.ListTemplates)
            {
                if (template.InternalName == name)
                {
                    return template;
                }
            }
            return null;
        }

        public static JsonSchemaManager TryGet(ProductPage prodPage, string filePath, bool siteScope, string asmName)
        {
            KeyValuePair<JsonSchemaManager, JsonSchemaManager> pair = TryGet(prodPage, filePath, !siteScope, siteScope, asmName);
            if (!siteScope)
            {
                return pair.Key;
            }
            return pair.Value;
        }

        public static KeyValuePair<JsonSchemaManager, JsonSchemaManager> TryGet(ProductPage prodPage, string filePath, bool farmScope, bool siteScope, string asmName)
        {
            JsonSchemaManager key = null;
            JsonSchemaManager manager2 = null;
            if (farmScope)
            {
                try
                {
                    key = new JsonSchemaManager(prodPage, filePath, false, asmName);
                }
                catch
                {
                }
            }
            if (siteScope)
            {
                try
                {
                    manager2 = new JsonSchemaManager(prodPage, filePath, true, asmName);
                }
                catch
                {
                }
            }
            return new KeyValuePair<JsonSchemaManager, JsonSchemaManager>(key, manager2);
        }

        internal void Update(Schema schema, string key, IDictionary val)
        {
            new OrderedDictionary();
            try
            {
                this.Web.AllowUnsafeUpdates = this.Site.AllowUnsafeUpdates = true;
            }
            catch
            {
            }
            this.Storage[key] = JSON.JsonEncode(CloneDictionary(schema, val, 0));
            if (this.SiteScope)
            {
                this.web.Update();
            }
            else
            {
                this.WebApp.Update(true);
            }
        }

        public SPFarm Farm
        {
            get
            {
                if (this.farm == null)
                {
                    this.farm = ProductPage.GetFarm(ProductPage.GetContext());
                }
                return this.farm;
            }
        }

        internal static ProductPage.LicInfo Li
        {
            get
            {
                if (li == null)
                {
                    li = ProductPage.LicInfo.Get(null);
                }
                return li;
            }
        }

        public List<SPListTemplate> ListTemplates
        {
            get
            {
                if (this.templates == null)
                {
                    this.templates = new List<SPListTemplate>();
                    foreach (SPListTemplate template in ProductPage.TryEach<SPListTemplate>(this.Web.ListTemplates))
                    {
                        this.templates.Add(template);
                    }
                    foreach (SPListTemplate template2 in ProductPage.TryEach<SPListTemplate>(this.Site.GetCustomListTemplates(this.Web)))
                    {
                        this.templates.Add(template2);
                    }
                }
                return this.templates;
            }
        }

        public SPSite Site
        {
            get
            {
                if (this.site == null)
                {
                    this.site = this.SiteScope ? (this.Elevated ? (this.dispSite = ProductPage.OpenSite(ProductPage.GetContext())) : ProductPage.GetSite(ProductPage.GetContext())) : this.ProdPage.AdminSite;
                }
                if (this.site != null)
                {
                    this.site.CatchAccessDeniedException = false;
                }
                return this.site;
            }
        }

        public bool SiteScope
        {
            get
            {
                return this.siteScope;
            }
            set
            {
                if (value != this.siteScope)
                {
                    this.siteScope = value;
                    this.web = null;
                    this.webApp = null;
                    this.farm = null;
                    this.store = null;
                    this.site = null;
                    foreach (KeyValuePair<string, Schema> pair in this.AllSchemas)
                    {
                        pair.Value.instDict = null;
                    }
                }
            }
        }

        public Hashtable Storage
        {
            get
            {
                SPSecurity.CodeToRunElevated code = null;
                Guid webID;
                Guid siteID;
                if (this.store == null)
                {
                    try
                    {
                        this.store = this.SiteScope ? this.Web.AllProperties : this.Site.WebApplication.Properties;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        if (!ProductPage.Elevated)
                        {
                            webID = this.Web.ID;
                            siteID = this.Site.ID;
                            if (code == null)
                            {
                                code = delegate {
                                    using (SPSite site = new SPSite(siteID))
                                    {
                                        using (SPWeb web = site.OpenWeb(webID))
                                        {
                                            this.store = this.SiteScope ? web.AllProperties : site.WebApplication.Properties;
                                        }
                                    }
                                };
                            }
                            ProductPage.Elevate(code, false, false);
                        }
                    }
                }
                return this.store;
            }
        }

        public SPWeb Web
        {
            get
            {
                if (this.web == null)
                {
                    this.web = this.Site.RootWeb;
                }
                return this.web;
            }
        }

        public SPWebApplication WebApp
        {
            get
            {
                if (this.webApp == null)
                {
                    this.webApp = this.Site.WebApplication;
                }
                return this.webApp;
            }
        }

        public interface ISchemaExtender
        {
            void InitSchema(JsonSchemaManager.Schema owner);
        }

        public class Property
        {
            private bool? isLe = null;
            internal readonly int le;
            public readonly string Name;
            public readonly JsonSchemaManager.Schema Owner;
            public readonly IDictionary RawSchema;

            public Property(JsonSchemaManager.Schema schema, string name, IDictionary rawSchema)
            {
                this.Name = name;
                this.Owner = schema;
                this.RawSchema = rawSchema;
                if (Array.IndexOf<string>(schema.lp0, name) >= 0)
                {
                    this.le = 0;
                }
                else if (Array.IndexOf<string>(schema.lp4, name) >= 0)
                {
                    this.le = 4;
                }
                else
                {
                    this.le = 2;
                }
            }

            internal bool IsLe(int l)
            {
                return ProductPage.LicEdition(ProductPage.GetContext(), JsonSchemaManager.Li, this.le);
            }

            public override string ToString()
            {
                string str = this.RawSchema["res_title"] + string.Empty;
                string productResource = ProductPage.GetProductResource(string.IsNullOrEmpty(str) ? ("PC_" + this.Owner.Name + "_" + this.Name) : str, new object[0]);
                if (string.IsNullOrEmpty(productResource))
                {
                    productResource = ProductPage.GetResource("PC_ItemEditor_" + this.Name, new object[] { ProductPage.GetProductResource("Tool_" + this.Owner.Name + "_TitleSingular", new object[0]) });
                }
                if (!string.IsNullOrEmpty(productResource))
                {
                    return productResource;
                }
                return this.Name;
            }

            public bool AlwaysShowHelp
            {
                get
                {
                    return ((this.RawSchema["always_show_help"] is bool) && ((bool) this.RawSchema["always_show_help"]));
                }
            }

            public object DefaultValue
            {
                get
                {
                    return this.PropertyType.GetDefaultValue(this.RawSchema);
                }
            }

            public string Description
            {
                get
                {
                    string str = this.RawSchema["res_desc"] + string.Empty;
                    string productResource = ProductPage.GetProductResource(string.IsNullOrEmpty(str) ? (str = "PD_" + this.Owner.Name + "_" + this.Name) : str, new object[0]);
                    string resource = ProductPage.GetResource(str.Substring(0, str.Length - 1) + "*", new object[0]);
                    if (string.IsNullOrEmpty(productResource))
                    {
                        productResource = ProductPage.GetResource("PD_ItemEditor_" + this.Name, new object[] { ProductPage.GetProductResource("Tool_" + this.Owner.Name + "_TitleSingular", new object[0]) });
                    }
                    if (productResource.StartsWith("*"))
                    {
                        return productResource.Substring(1);
                    }
                    if (!string.IsNullOrEmpty(resource))
                    {
                        return string.Format(resource, productResource);
                    }
                    return productResource;
                }
            }

            public bool Disabled
            {
                get
                {
                    return ((this.RawSchema["disabled"] is bool) && ((bool) this.RawSchema["disabled"]));
                }
            }

            public bool Editable
            {
                get
                {
                    return (((!this.Disabled && !this.ReadOnly) && this.Owner.Owner.ProdPage.IsApplicableAdmin) && this.Le);
                }
            }

            public string EditHint
            {
                get
                {
                    if (this.ReadOnly)
                    {
                        return ProductPage.GetResource("Tool_ItemEditor_ReadOnly", new object[0]);
                    }
                    if (!this.Disabled)
                    {
                        return ProductPage.GetResource("NopeEd", new object[] { this.ToString(), (this.le == 4) ? "Ultimate" : ((this.le == 2) ? "Basic" : "Lite") });
                    }
                    return ProductPage.GetResource("Tool_ItemEditor_Disabled", new object[] { ProductPage.GetTitle() });
                }
            }

            internal bool Le
            {
                get
                {
                    if (!this.isLe.HasValue || !this.isLe.HasValue)
                    {
                        this.isLe = new bool?(this.IsLe(this.le));
                    }
                    return this.isLe.Value;
                }
            }

            public Type PropertyType
            {
                get
                {
                    return Type.FromSchema(this.RawSchema["type"]);
                }
            }

            public bool ReadOnly
            {
                get
                {
                    return ((this.RawSchema["readonly"] is bool) && ((bool) this.RawSchema["readonly"]));
                }
            }

            public bool ShowInSummary
            {
                get
                {
                    object obj2 = this.RawSchema["show_in_summary"];
                    if (Type.String.IsPassword(this.RawSchema) || !this.PropertyType.ShowInSummary)
                    {
                        return false;
                    }
                    if (obj2 is bool)
                    {
                        return (bool) obj2;
                    }
                    return true;
                }
            }

            public string Tab
            {
                get
                {
                    return (this.RawSchema["tab"] + string.Empty);
                }
            }

            public abstract class Type
            {
                internal const string DISABLED = " disabled=\"disabled\" ";
                internal const string READONLY = " readonly=\"readonly\" ";

                protected Type()
                {
                }

                public virtual object FromPostedValue(JsonSchemaManager.Property prop, string value, JsonSchemaManager.Schema owner)
                {
                    return value;
                }

                public static JsonSchemaManager.Property.Type FromSchema(object typeSpec)
                {
                    if (typeSpec is string)
                    {
                        System.Type type = ProductPage.Assembly.GetType(typeof(JsonSchemaManager.Property.Type).FullName + "+" + typeSpec, false, true);
                        if (type == null)
                        {
                            type = ProductPage.Assembly.GetType("roxority.SharePoint.JsonSchemaPropertyTypes." + typeSpec, true, true);
                        }
                        return (type.GetConstructor(new System.Type[0]).Invoke(null) as JsonSchemaManager.Property.Type);
                    }
                    if (typeSpec is IEnumerable)
                    {
                        return new Choice(typeSpec as IEnumerable);
                    }
                    return null;
                }

                public virtual object GetDefaultValue(IDictionary rawSchema)
                {
                    return rawSchema["default"];
                }

                public string GetFormKey(IDictionary inst, JsonSchemaManager.Property prop)
                {
                    return this.GetFormKey(inst["id"] + string.Empty, prop);
                }

                public virtual string GetFormKey(string id, JsonSchemaManager.Property prop)
                {
                    return (id + "_" + prop.Name);
                }

                public virtual string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                {
                    return (val + string.Empty);
                }

                public virtual string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                {
                    return string.Empty;
                }

                public override string ToString()
                {
                    return base.GetType().Name;
                }

                public virtual string ToString(JsonSchemaManager.Property prop, object val)
                {
                    string str = this.RenderValueForDisplay(prop, val);
                    string str2 = prop.ToString();
                    int index = str2.IndexOf('(');
                    if (index > 0)
                    {
                        str2 = str2.Substring(0, index);
                    }
                    if (!string.IsNullOrEmpty(str.Trim()))
                    {
                        return (str2 + ": <b>" + str + "</b>");
                    }
                    return string.Empty;
                }

                public void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context)
                {
                    this.Update(inst, prop, context, this.GetFormKey(inst, prop));
                }

                public virtual void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context, string formKey)
                {
                    inst[prop.Name] = prop.Le ? this.FromPostedValue(prop, context.Request.Form[formKey], prop.Owner) : prop.DefaultValue;
                }

                public virtual string CssClass
                {
                    get
                    {
                        return ("rox-iteminst-edit-control rox-iteminst-edit-" + base.GetType().Name);
                    }
                }

                public virtual bool IsBool
                {
                    get
                    {
                        return false;
                    }
                }

                public string this[string name, object[] args]
                {
                    get
                    {
                        return ProductPage.GetResource(name, args);
                    }
                }

                public virtual bool ShowInSummary
                {
                    get
                    {
                        return true;
                    }
                }

                public class Boolean : JsonSchemaManager.Property.Type
                {
                    public override object FromPostedValue(JsonSchemaManager.Property prop, string value, JsonSchemaManager.Schema owner)
                    {
                        return !string.IsNullOrEmpty(value);
                    }

                    public override string GetFormKey(string id, JsonSchemaManager.Property prop)
                    {
                        return ("roxiteminstchk_" + base.GetFormKey(id, prop));
                    }

                    public override string ToString(JsonSchemaManager.Property prop, object val)
                    {
                        return string.Empty;
                    }

                    public override bool IsBool
                    {
                        get
                        {
                            return true;
                        }
                    }
                }

                public class Choice : JsonSchemaManager.Property.Type
                {
                    private readonly IEnumerable choices;

                    public Choice(IEnumerable choices)
                    {
                        this.choices = choices;
                    }

                    public virtual string GetChoiceDesc(JsonSchemaManager.Property prop, object choice)
                    {
                        return this.GetChoiceString(prop.Owner.Name, prop.Name, choice, "PD_");
                    }

                    public virtual IEnumerable GetChoices(IDictionary rawSchema)
                    {
                        return this.choices;
                    }

                    protected internal string GetChoiceString(string ownerName, string propName, object choice, string prefix)
                    {
                        int num;
                        string productResource = ProductPage.GetProductResource(string.Concat(new object[] { prefix, ownerName, "_", propName, "_", ("PD_".Equals(prefix) && "UserDatabase".Equals(choice)) ? "Ado" : choice }), new object[0]);
                        if (string.IsNullOrEmpty(productResource) && ((num = propName.IndexOf('_')) > 0))
                        {
                            return this.GetChoiceString(ownerName, propName.Substring(num + 1), choice, prefix);
                        }
                        return productResource;
                    }

                    public virtual string GetChoiceTitle(JsonSchemaManager.Property prop, object choice)
                    {
                        return this.GetChoiceString(prop.Owner.Name, prop.Name, choice, "PC_");
                    }

                    public override object GetDefaultValue(IDictionary rawSchema)
                    {
                        object defaultValue = base.GetDefaultValue(rawSchema);
                        if (defaultValue == null)
                        {
                            IEnumerator enumerator = this.GetChoices(rawSchema).GetEnumerator();
                            {
                                while (enumerator.MoveNext())
                                {
                                    return enumerator.Current;
                                }
                            }
                        }
                        return defaultValue;
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        return this.GetChoiceTitle(prop, val);
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        string str = "";
                        foreach (object obj2 in this.GetChoices(prop.RawSchema))
                        {
                            string str2;
                            str = str + string.Format("<option value=\"{0}\"{2}>{1}</option>", obj2, this.GetChoiceTitle(prop, obj2) + (string.IsNullOrEmpty(str2 = this.GetChoiceDesc(prop, obj2)) ? string.Empty : (" &mdash; " + str2)), (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + (obj2.Equals(instance[prop.Name]) ? " selected=\"selected\"" : string.Empty));
                        }
                        return string.Format("<select onchange=\"roxHasChanged();\" class=\"{2}\" id=\"{0}\" name=\"{0}\"" + (readOnly ? " disabled=\"disabled\" " : string.Empty) + ">{1}</select>", instance["id"] + "_" + prop.Name, str, this.CssClass);
                    }
                }

                public class ConfigChoice : JsonSchemaManager.Property.Type.DictChoice
                {
                    public ConfigChoice() : base(new OrderedDictionary())
                    {
                    }

                    public override object GetDefaultValue(IDictionary rawSchema)
                    {
                        this.ResetChoices(rawSchema);
                        return base.GetDefaultValue(rawSchema);
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        this.ResetChoices(prop.RawSchema);
                        if (!string.IsNullOrEmpty(val + string.Empty))
                        {
                            return base.RenderValueForDisplay(prop, val);
                        }
                        return string.Empty;
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        this.ResetChoices(prop.RawSchema);
                        return base.RenderValueForEdit(prop, instance, disabled, readOnly);
                    }

                    internal void ResetChoices(IDictionary rawSchema)
                    {
                        string str;
                        string str2;
                        string[] strArray;
                        if (((base.Choices.Count == 0) && !string.IsNullOrEmpty(str = rawSchema["config"] + string.Empty)) && (!string.IsNullOrEmpty(str2 = ProductPage.Config(ProductPage.GetContext(), str)) && ((strArray = str2.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length > 0)))
                        {
                            foreach (string str3 in strArray)
                            {
                                int index = str3.IndexOf(':');
                                if (index > 0)
                                {
                                    base.Choices[str3.Substring(index + 1).Trim()] = str3.Substring(0, index).Trim();
                                }
                                else
                                {
                                    base.Choices[str3] = str3;
                                }
                            }
                        }
                    }
                }

                public class DictChoice : JsonSchemaManager.Property.Type
                {
                    public readonly IDictionary Choices;
                    private string formKey;

                    public DictChoice(IDictionary choices)
                    {
                        this.Choices = choices;
                    }

                    public override object FromPostedValue(JsonSchemaManager.Property prop, string value, JsonSchemaManager.Schema owner)
                    {
                        string[] values = null;
                        if (this.formKey != null)
                        {
                            try
                            {
                                values = HttpContext.Current.Request.Form.GetValues(this.formKey);
                            }
                            catch
                            {
                            }
                            if (values != null)
                            {
                                return string.Join("\r\n", values);
                            }
                        }
                        return base.FromPostedValue(prop, value, owner);
                    }

                    public override object GetDefaultValue(IDictionary rawSchema)
                    {
                        object obj2 = null;
                        if (!this.IsMulti(rawSchema) && ((obj2 = base.GetDefaultValue(rawSchema)) == null))
                        {
                            foreach (DictionaryEntry entry in this.Choices)
                            {
                                return entry.Key;
                            }
                        }
                        return obj2;
                    }

                    protected internal bool IsMulti(IDictionary rawSchema)
                    {
                        return JsonSchemaManager.Bool(rawSchema["multi"], false);
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        string[] array = (val + string.Empty).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> list = new List<string>();
                        foreach (DictionaryEntry entry in this.Choices)
                        {
                            if (Array.IndexOf<string>(array, entry.Key + string.Empty) >= 0)
                            {
                                list.Add(entry.Value + string.Empty);
                            }
                        }
                        if (list.Count != 0)
                        {
                            return string.Join(" \x00b7 ", list.ToArray());
                        }
                        return (val + string.Empty);
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        string str = "";
                        string[] array = (instance[prop.Name] + string.Empty).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (DictionaryEntry entry in this.Choices)
                        {
                            str = str + string.Format("<option value=\"{0}\"{2}>{1}</option>", entry.Key, HttpUtility.HtmlEncode(entry.Value + string.Empty) + ((this is JsonSchemaManager.Property.Type.ConfigChoice) ? (" &mdash; [ " + HttpUtility.HtmlEncode(entry.Key + string.Empty) + " ]") : string.Empty), (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + ((Array.IndexOf<string>(array, entry.Key + string.Empty) >= 0) ? " selected=\"selected\"" : string.Empty));
                        }
                        return string.Format("<select " + (this.IsMulti(prop.RawSchema) ? ("multiple=\"multiple\" size=\"" + this.Choices.Count + "\" ") : string.Empty) + "onchange=\"roxHasChanged();\" class=\"{2}\" id=\"{0}\" name=\"{0}\">{1}</select>", instance["id"] + "_" + prop.Name, str, this.CssClass);
                    }

                    public override void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context, string formKey)
                    {
                        this.formKey = formKey;
                        base.Update(inst, prop, context, formKey);
                    }

                    public override string CssClass
                    {
                        get
                        {
                            return ("rox-iteminst-edit-control rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.Choice).Name);
                        }
                    }
                }

                public class EncodingChoice : JsonSchemaManager.Property.Type.DictChoice
                {
                    public EncodingChoice() : base(GetChoices())
                    {
                    }

                    internal static IDictionary GetChoices()
                    {
                        KeyValuePair<string, string> pair;
                        int num = -1;
                        IDictionary dictionary = new OrderedDictionary();
                        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                        foreach (EncodingInfo info in Encoding.GetEncodings())
                        {
                            list.Add(new KeyValuePair<string, string>(info.CodePage.ToString(), info.DisplayName));
                        }
                        list.Sort(delegate (KeyValuePair<string, string> one, KeyValuePair<string, string> two) {
                            int num111 = one.Value.CompareTo(two.Value);
                            if (num111 != 0)
                            {
                                return num111;
                            }
                            return one.Key.CompareTo(two.Key);
                        });
                        for (int i = 0; i < list.Count; i++)
                        {
                            KeyValuePair<string, string> pair3 = list[i];
                            if (!pair3.Key.StartsWith("unicode"))
                            {
                                KeyValuePair<string, string> pair4 = list[i];
                                if (!pair4.Key.StartsWith("utf-"))
                                {
                                    KeyValuePair<string, string> pair5 = list[i];
                                    if (!pair5.Key.EndsWith("ascii"))
                                    {
                                        continue;
                                    }
                                }
                            }
                            KeyValuePair<string, string> pair6 = list[i];
                            KeyValuePair<string, string> pair7 = list[i];
                            pair = new KeyValuePair<string, string>(pair6.Key, pair7.Value);
                            list.RemoveAt(i);
                            list.Insert(++num, pair);
                        }
                        for (int j = 0; j < list.Count; j++)
                        {
                            KeyValuePair<string, string> pair8 = list[j];
                            if ("utf-8".Equals(pair8.Key))
                            {
                                KeyValuePair<string, string> pair9 = list[j];
                                KeyValuePair<string, string> pair10 = list[j];
                                pair = new KeyValuePair<string, string>(pair9.Key, pair10.Value);
                                list.RemoveAt(j);
                                list.Insert(0, pair);
                                break;
                            }
                        }
                        dictionary.Add(string.Empty, ProductPage.GetProductResource("DefaultEncoding", new object[] { Encoding.Default.EncodingName }));
                        foreach (KeyValuePair<string, string> pair2 in list)
                        {
                            if (dictionary.Contains(pair2.Key))
                            {
                                dictionary[pair2.Key] = pair2.Value;
                            }
                            else
                            {
                                dictionary.Add(pair2.Key, pair2.Value);
                            }
                        }
                        return dictionary;
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        if (!string.IsNullOrEmpty(val + string.Empty))
                        {
                            return base.RenderValueForDisplay(prop, val);
                        }
                        return string.Empty;
                    }
                }

                public class EnumChoice : JsonSchemaManager.Property.Type.Choice
                {
                    private System.Type enumType;
                    private string enumTypeName;
                    private string[] exclude;

                    public EnumChoice() : base(new ArrayList())
                    {
                    }

                    public override string GetChoiceDesc(JsonSchemaManager.Property prop, object choice)
                    {
                        this.Init(prop.RawSchema);
                        return ProductPage.GetResource(string.Concat(new object[] { "PD_", this.EnumType.Name, "_", choice }), new object[0]);
                    }

                    public override IEnumerable GetChoices(IDictionary rawSchema)
                    {
                        Array iteratorVariable2;
                        this.Init(rawSchema);
                        if (((iteratorVariable2 = Enum.GetValues(this.EnumType)) != null) && (iteratorVariable2.Length > 0))
                        {
                            List<string> iteratorVariable1 = new List<string>(iteratorVariable2.Length);
                            foreach (object obj2 in iteratorVariable2)
                            {
                                string iteratorVariable0;
                                if (!iteratorVariable1.Contains(iteratorVariable0 = Enum.GetName(this.EnumType, obj2)) && (Array.IndexOf<string>(this.exclude, iteratorVariable0) < 0))
                                {
                                    iteratorVariable1.Insert(iteratorVariable0.Equals(rawSchema["default"]) ? 0 : iteratorVariable1.Count, iteratorVariable0);
                                }
                            }
                            foreach (string iteratorVariable3 in iteratorVariable1)
                            {
                                yield return iteratorVariable3;
                            }
                        }
                    }

                    public override string GetChoiceTitle(JsonSchemaManager.Property prop, object choice)
                    {
                        string str;
                        this.Init(prop.RawSchema);
                        if (!string.IsNullOrEmpty(str = ProductPage.GetResource(string.Concat(new object[] { "PC_", this.EnumType.Name, "_", choice }), new object[0])))
                        {
                            return str;
                        }
                        return choice.ToString();
                    }

                    internal void Init(IDictionary rawSchema)
                    {
                        if (this.enumTypeName == null)
                        {
                            this.enumTypeName = rawSchema["enumtype"] + string.Empty;
                        }
                        if (this.exclude == null)
                        {
                            this.exclude = (rawSchema["exclude"] + string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                    }

                    public override string CssClass
                    {
                        get
                        {
                            return ("rox-iteminst-edit-control rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.Choice).Name);
                        }
                    }

                    public System.Type EnumType
                    {
                        get
                        {
                            if (this.enumType == null)
                            {
                                this.enumType = System.Type.GetType(this.enumTypeName, false, true);
                            }
                            return this.enumType;
                        }
                    }

                }

                public class LibSet : JsonSchemaManager.Property.Type.ListSet
                {
                    public static readonly SPListTemplateType[] SupportedTemplateTypes = new SPListTemplateType[] { SPListTemplateType.DataConnectionLibrary, SPListTemplateType.DataSources, SPListTemplateType.DocumentLibrary, SPListTemplateType.HomePageLibrary, SPListTemplateType.NoCodeWorkflows, SPListTemplateType.PictureLibrary, SPListTemplateType.WebPageLibrary, SPListTemplateType.XMLForm, SPListTemplateType.ListTemplateCatalog, SPListTemplateType.WebTemplateCatalog, SPListTemplateType.MasterPageCatalog };
                    public static readonly int[] SupportedTemplateTypes2 = new int[] { 0x353, 0x2776, 0x834, 0x7a, 0x75, 0x1b1 };

                    internal override bool IsSupported(SPBaseType type)
                    {
                        return (type == SPBaseType.DocumentLibrary);
                    }

                    internal override bool IsSupported(SPList list)
                    {
                        return (list is SPDocumentLibrary);
                    }

                    internal override bool IsSupported(SPListTemplateType type)
                    {
                        if (Array.IndexOf<SPListTemplateType>(SupportedTemplateTypes, type) < 0)
                        {
                            return (Array.IndexOf<int>(SupportedTemplateTypes2, (int) type) >= 0);
                        }
                        return true;
                    }

                    public override string CssClass
                    {
                        get
                        {
                            return ("rox-iteminst-edit-control rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.ListSet).Name);
                        }
                    }

                    public override bool IgnoreBaseType
                    {
                        get
                        {
                            return true;
                        }
                    }
                }

                public class ListSet : JsonSchemaManager.Property.Type
                {
                    internal virtual bool IsSupported(SPBaseType type)
                    {
                        int num = (int) type;
                        return ((num != 2) && (num >= 0));
                    }

                    internal virtual bool IsSupported(SPList list)
                    {
                        return true;
                    }

                    internal virtual bool IsSupported(SPListTemplate lt)
                    {
                        return ((lt != null) && this.IsSupported(lt.BaseType));
                    }

                    internal virtual bool IsSupported(SPListTemplateType type)
                    {
                        return (type > SPListTemplateType.NoListTemplate);
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        string str = string.Empty;
                        string formKey = base.GetFormKey(instance, prop);
                        Config config = new Config(instance[prop.Name]);
                        object obj2 = str;
                        string str3 = string.Concat(new object[] { 
                            obj2, "<div id=\"roxlistsetouter_", formKey, "\" class=\"", this.CssClass, "\"><select onchange=\"roxHasChanged();\" id=\"", formKey, "_l_n\" name=\"", formKey, "_l_n\"><option value=\"0\"", !config.ListNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_ListSet_All", new object[] { base["Tool_ItemEditor_" + base.GetType().Name + "_Sel", new object[0]] }], "</option><option value=\"1\"", 
                            config.ListNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_ListSet_None", new object[] { base["Tool_ItemEditor_" + base.GetType().Name + "_Sel", new object[0]] }], "</option></select> <a href=\"#noop\" onclick=\"roxToggleListSel('", instance["id"], "', '", prop.Name, "');\">", base["Tool_ItemEditor_ListSet_Except", new object[] { (config.ListSet.Count > 1) ? (config.ListSet.Count - 1) : 0 }], "</a>&nbsp;&nbsp;&nbsp;&mdash;&nbsp;&nbsp;&nbsp;<select onchange=\"roxHasChanged();\" id=\"", formKey, "_v_n\" name=\"", formKey, "_v_n\"><option value=\"0\"", 
                            !config.ViewNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_ListSet_All", new object[] { base["Tool_ItemEditor_ListSet_Views", new object[0]] }], "</option><option value=\"1\"", config.ViewNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_ListSet_None", new object[] { base["Tool_ItemEditor_ListSet_Views", new object[0]] }], "</option></select> <a href=\"#noop\" onclick=\"roxToggleListSel('", instance["id"], "', '", prop.Name, "');\">", 
                            base["Tool_ItemEditor_ListSet_Except", new object[] { (config.ViewSet.Count > 1) ? (config.ViewSet.Count - 1) : 0 }], "</a></div>"
                         });
                        string str4 = (str3 + "<div id=\"roxlistsetinner_" + formKey + "\" style=\"display: none;\" class=\"" + this.CssClass + "-inner\">") + "<div class=\"" + this.CssClass + "-exbox\" style=\"float: left; border-right: #909090 1px dotted;\">";
                        str = str4 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptBaseType", new object[0]] + "</div><select onchange=\"roxHasChanged();\" size=\"" + (this.IgnoreBaseType ? "2\" disabled=\"disabled" : "4") + "\" id=\"" + formKey + "_l_b\" name=\"" + formKey + "_l_b\" multiple=\"multiple\">";
                        foreach (SPBaseType type in Enum.GetValues(typeof(SPBaseType)))
                        {
                            if (this.IsSupported(type))
                            {
                                object obj3 = str;
                                str = string.Concat(new object[] { obj3, "<option value=\"", type, "\"", config.IsChecked(type) ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", type, "</option>" });
                            }
                        }
                        string str5 = str + "</select>";
                        str = str5 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptTemplateType", new object[0]] + "</div><select onchange=\"roxHasChanged();\" size=\"4\" multiple=\"multiple\" id=\"" + formKey + "_l_t\" name=\"" + formKey + "_l_t\" >";
                        foreach (SPListTemplateType type2 in Enum.GetValues(typeof(SPListTemplateType)))
                        {
                            if (this.IsSupported(type2))
                            {
                                object obj4 = str;
                                str = string.Concat(new object[] { obj4, "<option value=\"", type2, "\"", config.IsChecked(type2) ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", (int) type2, " &mdash; ", type2, "</option>" });
                            }
                        }
                        string str6 = str + "</select>";
                        str = str6 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptBaseTemplate", new object[0]] + "</div><select onchange=\"roxHasChanged();\" size=\"4\" multiple=\"multiple\" id=\"" + formKey + "_l_bt\" name=\"" + formKey + "_l_bt\">";
                        foreach (SPListTemplate template in prop.Owner.Owner.ListTemplates)
                        {
                            if (this.IsSupported(template))
                            {
                                string str7 = str;
                                str = str7 + "<option value=\"" + template.InternalName + "\"" + (config.IsChecked(template) ? " selected=\"selected\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + ">" + HttpUtility.HtmlEncode(template.Name) + "</option>";
                            }
                        }
                        string str8 = str + "</select>";
                        string str9 = (str8 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptListName", new object[0]] + "</div><textarea onchange=\"roxHasChanged();\" rows=\"2\" class=\"rox-iteminst-edit-String" + ((disabled || readOnly) ? " rox-iteminst-edit-String-readonly" : string.Empty) + "\" id=\"" + formKey + "_l_i\" name=\"" + formKey + "_l_i\"" + ((disabled || readOnly) ? " readonly=\"readonly\" " : string.Empty) + ">" + string.Join("\n", config.ListNames) + "</textarea>") + "</div><div class=\"" + this.CssClass + "-exbox\" style=\"float: right;\">";
                        string str10 = str9 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptView", new object[0]] + "<br/><input onclick=\"roxHasChanged();\" id=\"" + formKey + "_v_d\" name=\"" + formKey + "_v_d\" type=\"checkbox\"" + (config.ViewDefault ? " checked=\"checked\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + " /> <label for=\"" + formKey + "_v_d\">" + base["Tool_ItemEditor_ListSet_ExceptDefaultView", new object[0]] + "</label></div>";
                        string str11 = str10 + "<div><input onclick=\"roxHasChanged();\" id=\"" + formKey + "_v_h\" name=\"" + formKey + "_v_h\" type=\"checkbox\"" + (config.ViewHidden ? " checked=\"checked\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + " /> <label for=\"" + formKey + "_v_h\">" + base["Tool_ItemEditor_ListSet_ExceptViewHidden", new object[0]] + "</label></div>";
                        string str12 = str11 + "<div><input onclick=\"roxHasChanged();\" id=\"" + formKey + "_v_p\" name=\"" + formKey + "_v_p\" type=\"checkbox\"" + (config.ViewPersonal ? " checked=\"checked\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + " /> <label for=\"" + formKey + "_v_p\">" + base["Tool_ItemEditor_ListSet_ExceptViewPersonal", new object[0]] + "</label></div>";
                        object obj5 = str12 + "<div><input onclick=\"roxHasChanged();\" id=\"" + formKey + "_v_w\" name=\"" + formKey + "_v_w\" type=\"checkbox\"" + (config.ViewNoTitle ? " checked=\"checked\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + " /> <label for=\"" + formKey + "_v_w\">" + base["Tool_ItemEditor_ListSet_ExceptViewWebPart", new object[0]] + "</label></div>";
                        return (string.Concat(new object[] { obj5, "<div>", base["Tool_ItemEditor_ListSet_ExceptViewName", new object[0]], "<a class=\"rox-iteminst-edit-Pick\" style=\"display: none;\" href=\"#\"/><img src=\"/_layouts/images/blank.gif\" width=\"16\" height=\"16\"/></a></div><textarea onchange=\"roxHasChanged();\" rows=\"", this.IgnoreBaseType ? 9 : 11, "\" class=\"rox-iteminst-edit-String", (disabled || readOnly) ? " rox-iteminst-edit-String-readonly" : string.Empty, "\" id=\"", formKey, "_v_i\" name=\"", formKey, "_v_i\"", (disabled || readOnly) ? " readonly=\"readonly\" " : string.Empty, ">", string.Join("\n", config.ViewNames), "</textarea>" }) + "</div>" + "<br style=\"clear: both;\"/>&nbsp;</div>");
                    }

                    public override string ToString(JsonSchemaManager.Property prop, object val)
                    {
                        return string.Empty;
                    }

                    public override void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context, string id)
                    {
                        Config config = new Config(inst[prop.Name]);
                        Converter<string, string> converter = k => context.Request.Form[id + k] + string.Empty;
                        string str = converter("_v_n");
                        string str2 = converter("_v_d");
                        string str3 = converter("_v_h");
                        string str4 = converter("_v_p");
                        string str5 = converter("_v_w");
                        string str6 = converter("_v_i");
                        string str7 = converter("_l_n");
                        string str8 = converter("_l_i");
                        string str9 = converter("_l_b");
                        string str10 = converter("_l_t");
                        string str11 = converter("_l_bt");
                        config.ViewNone = "1".Equals(str);
                        config.ViewDefault = "on".Equals(str2, StringComparison.InvariantCultureIgnoreCase);
                        config.ViewHidden = "on".Equals(str3, StringComparison.InvariantCultureIgnoreCase);
                        config.ViewPersonal = "on".Equals(str4, StringComparison.InvariantCultureIgnoreCase);
                        config.ViewNoTitle = "on".Equals(str5, StringComparison.InvariantCultureIgnoreCase);
                        config.ViewNames = str6.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        config.ListNone = "1".Equals(str7);
                        config.ListNames = str8.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        config.ListBaseTemplates = str11.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        config.ListBaseTypes = str9.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        config.ListTemplateTypes = str10.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        inst[prop.Name] = config.List;
                    }

                    public virtual bool IgnoreBaseType
                    {
                        get
                        {
                            return false;
                        }
                    }

                    public class Config
                    {
                        public readonly ArrayList List;
                        public readonly IDictionary ListSet;
                        public readonly IDictionary ViewSet;

                        public Config(object val)
                        {
                            this.List = val as ArrayList;
                            if (this.List == null)
                            {
                                this.List = new ArrayList();
                            }
                            if ((this.List.Count < 1) || ((this.ListSet = this.List[0] as IDictionary) == null))
                            {
                                this.List.Insert(0, this.ListSet = new OrderedDictionary());
                            }
                            if ((this.List.Count < 2) || ((this.ViewSet = this.List[1] as IDictionary) == null))
                            {
                                this.List.Insert(1, this.ViewSet = new OrderedDictionary());
                            }
                        }

                        public bool IsChecked(SPBaseType val)
                        {
                            return (Array.IndexOf<string>(this.ListBaseTypes, val.ToString()) >= 0);
                        }

                        public bool IsChecked(SPListTemplate val)
                        {
                            return (Array.IndexOf<string>(this.ListBaseTemplates, val.InternalName) >= 0);
                        }

                        public bool IsChecked(SPListTemplateType val)
                        {
                            return (Array.IndexOf<string>(this.ListTemplateTypes, val.ToString()) >= 0);
                        }

                        public bool IsMatch(SPView view)
                        {
                            string[] strArray;
                            bool flag = false;
                            if (view == null)
                            {
                                return true;
                            }
                            if ((!flag && this.ViewDefault) && view.DefaultView)
                            {
                                flag = true;
                            }
                            if ((!flag && this.ViewHidden) && view.Hidden)
                            {
                                flag = true;
                            }
                            if ((!flag && this.ViewPersonal) && view.PersonalView)
                            {
                                flag = true;
                            }
                            if ((!flag && this.ViewNoTitle) && string.IsNullOrEmpty(view.Title))
                            {
                                flag = true;
                            }
                            if ((!flag && ((strArray = this.ViewNames) != null)) && (strArray.Length > 0))
                            {
                                foreach (string str in strArray)
                                {
                                    if (flag = ((view.Url.IndexOf(str.Trim(), StringComparison.InvariantCultureIgnoreCase) >= 0) || str.Trim().Equals(view.Title.Trim(), StringComparison.InvariantCultureIgnoreCase)) || view.ID.Equals(ProductPage.GetGuid(str.Trim(), true)))
                                    {
                                        break;
                                    }
                                }
                            }
                            return ((this.ViewNone && flag) || (!this.ViewNone && !flag));
                        }

                        public bool IsMatch(SPList list, JsonSchemaManager schemaManager)
                        {
                            string[] strArray;
                            string[] strArray2;
                            string[] strArray3;
                            string[] strArray4;
                            bool flag = false;
                            if (list == null)
                            {
                                return true;
                            }
                            if ((!flag && ((strArray = this.ListNames) != null)) && (strArray.Length > 0))
                            {
                                foreach (string str in strArray)
                                {
                                    if (flag = ((list.DefaultViewUrl.IndexOf(str.Trim() + "/", StringComparison.InvariantCultureIgnoreCase) >= 0) || str.Trim().Equals(list.Title.Trim(), StringComparison.InvariantCultureIgnoreCase)) || list.ID.Equals(ProductPage.GetGuid(str.Trim(), true)))
                                    {
                                        break;
                                    }
                                }
                            }
                            if ((!flag && ((strArray3 = this.ListBaseTypes) != null)) && (strArray3.Length > 0))
                            {
                                foreach (string str2 in strArray3)
                                {
                                    if (flag = str2.Equals(list.BaseType.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        break;
                                    }
                                }
                            }
                            if ((!flag && ((strArray4 = this.ListTemplateTypes) != null)) && (strArray4.Length > 0))
                            {
                                foreach (string str3 in strArray4)
                                {
                                    if (flag = str3.Equals(list.BaseTemplate.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        break;
                                    }
                                }
                            }
                            if ((!flag && ((strArray2 = this.ListBaseTemplates) != null)) && (strArray2.Length > 0))
                            {
                                foreach (string str4 in strArray2)
                                {
                                    SPListTemplate template;
                                    if (flag = ((template = schemaManager.GetListTemplate(str4)) != null) && list.TemplateFeatureId.Equals(template.FeatureId))
                                    {
                                        break;
                                    }
                                }
                            }
                            return ((this.ListNone && flag) || (!this.ListNone && !flag));
                        }

                        public string[] ListBaseTemplates
                        {
                            get
                            {
                                ArrayList list = this.ListSet["bt"] as ArrayList;
                                if (list == null)
                                {
                                    list = new ArrayList();
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                ArrayList list = new ArrayList((value == null) ? 0 : value.Length);
                                foreach (string str in value)
                                {
                                    list.Add(str);
                                }
                                if (list.Count == 0)
                                {
                                    this.ListSet.Remove("bt");
                                }
                                else
                                {
                                    this.ListSet["bt"] = list;
                                }
                            }
                        }

                        public string[] ListBaseTypes
                        {
                            get
                            {
                                ArrayList list = this.ListSet["b"] as ArrayList;
                                if (list == null)
                                {
                                    list = new ArrayList();
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                ArrayList list = new ArrayList((value == null) ? 0 : value.Length);
                                foreach (string str in value)
                                {
                                    list.Add(str);
                                }
                                if (list.Count == 0)
                                {
                                    this.ListSet.Remove("b");
                                }
                                else
                                {
                                    this.ListSet["b"] = list;
                                }
                            }
                        }

                        public string[] ListNames
                        {
                            get
                            {
                                ArrayList list = this.ListSet["i"] as ArrayList;
                                if (list == null)
                                {
                                    return new string[0];
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                List<string> list = ((value == null) ? new List<string>() : new List<string>(value)).ConvertAll<string>(s => s.Trim());
                                ProductPage.RemoveDuplicates<string>(list);
                                if (list.Count == 0)
                                {
                                    this.ListSet.Remove("i");
                                }
                                else
                                {
                                    this.ListSet["i"] = new ArrayList(list);
                                }
                            }
                        }

                        public bool ListNone
                        {
                            get
                            {
                                return "1".Equals(this.ListSet["n"]);
                            }
                            set
                            {
                                this.ListSet["n"] = value ? "1" : "0";
                            }
                        }

                        public string[] ListTemplateTypes
                        {
                            get
                            {
                                ArrayList list = this.ListSet["t"] as ArrayList;
                                if (list == null)
                                {
                                    list = new ArrayList();
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                ArrayList list = new ArrayList((value == null) ? 0 : value.Length);
                                foreach (string str in value)
                                {
                                    list.Add(str);
                                }
                                if (list.Count == 0)
                                {
                                    this.ListSet.Remove("t");
                                }
                                else
                                {
                                    this.ListSet["t"] = list;
                                }
                            }
                        }

                        public bool ViewDefault
                        {
                            get
                            {
                                return "1".Equals(this.ViewSet["d"]);
                            }
                            set
                            {
                                if (value)
                                {
                                    this.ViewSet["d"] = "1";
                                }
                                else
                                {
                                    this.ViewSet.Remove("d");
                                }
                            }
                        }

                        public bool ViewHidden
                        {
                            get
                            {
                                return "1".Equals(this.ViewSet["h"]);
                            }
                            set
                            {
                                if (value)
                                {
                                    this.ViewSet["h"] = "1";
                                }
                                else
                                {
                                    this.ViewSet.Remove("h");
                                }
                            }
                        }

                        public string[] ViewNames
                        {
                            get
                            {
                                ArrayList list = this.ViewSet["i"] as ArrayList;
                                if (list == null)
                                {
                                    return new string[0];
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                List<string> list = ((value == null) ? new List<string>() : new List<string>(value)).ConvertAll<string>(s => s.Trim());
                                ProductPage.RemoveDuplicates<string>(list);
                                if (list.Count == 0)
                                {
                                    this.ViewSet.Remove("i");
                                }
                                else
                                {
                                    this.ViewSet["i"] = new ArrayList(list);
                                }
                            }
                        }

                        public bool ViewNone
                        {
                            get
                            {
                                return "1".Equals(this.ViewSet["n"]);
                            }
                            set
                            {
                                this.ViewSet["n"] = value ? "1" : "0";
                            }
                        }

                        public bool ViewNoTitle
                        {
                            get
                            {
                                return "1".Equals(this.ViewSet["w"]);
                            }
                            set
                            {
                                if (value)
                                {
                                    this.ViewSet["w"] = "1";
                                }
                                else
                                {
                                    this.ViewSet.Remove("w");
                                }
                            }
                        }

                        public bool ViewPersonal
                        {
                            get
                            {
                                return "1".Equals(this.ViewSet["p"]);
                            }
                            set
                            {
                                if (value)
                                {
                                    this.ViewSet["p"] = "1";
                                }
                                else
                                {
                                    this.ViewSet.Remove("p");
                                }
                            }
                        }
                    }
                }

                public class LocaleChoice : JsonSchemaManager.Property.Type.DictChoice
                {
                    internal static readonly Dictionary<string, CultureInfo> cachedCultures = new Dictionary<string, CultureInfo>();
                    internal static readonly List<KeyValuePair<string, string>> staticLocales = new List<KeyValuePair<string, string>>();

                    public LocaleChoice() : base(GetChoices())
                    {
                    }

                    public static IDictionary GetChoices()
                    {
                        IDictionary dictionary = new OrderedDictionary();
                        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                        foreach (CultureInfo info in ProductPage.AllSpecificCultures)
                        {
                            list.Add(new KeyValuePair<string, string>(info.Name, ToString(info)));
                        }
                        list.Sort(delegate (KeyValuePair<string, string> one, KeyValuePair<string, string> two) {
                            int num = one.Value.CompareTo(two.Value);
                            if (num != 0)
                            {
                                return num;
                            }
                            return one.Key.CompareTo(two.Key);
                        });
                        list.InsertRange(0, StaticLocales);
                        foreach (KeyValuePair<string, string> pair in list)
                        {
                            dictionary.Add(pair.Key, string.Format(pair.Value, ToString(GetCulture(pair.Key))));
                        }
                        return dictionary;
                    }

                    public static CultureInfo GetCulture(string staticName)
                    {
                        SPSecurity.CodeToRunElevated code = null;
                        SPSecurity.CodeToRunElevated elevated2 = null;
                        CultureInfo culture = null;
                        if (cachedCultures.TryGetValue(staticName, out culture))
                        {
                            goto Label_0128;
                        }
                        if (staticName == "System")
                        {
                            culture = CultureInfo.InstalledUICulture;
                        }
                        else if (staticName == "Web")
                        {
                            try
                            {
                                if (code == null)
                                {
                                    code = delegate {
                                        culture = new CultureInfo((int) SPContext.Current.Web.RegionalSettings.LocaleId);
                                    };
                                }
                                ProductPage.Elevate(code, true);
                            }
                            catch
                            {
                            }
                        }
                        else if (staticName == "User")
                        {
                            try
                            {
                                if (elevated2 == null)
                                {
                                    elevated2 = delegate {
                                        if (SPContext.Current.Web.CurrentUser.RegionalSettings != null)
                                        {
                                            culture = new CultureInfo((int) SPContext.Current.Web.CurrentUser.RegionalSettings.LocaleId);
                                        }
                                        else
                                        {
                                            culture = new CultureInfo((int) SPContext.Current.Web.RegionalSettings.LocaleId);
                                        }
                                    };
                                }
                                ProductPage.Elevate(elevated2, true);
                            }
                            catch
                            {
                            }
                        }
                        else if (staticName == "Browser")
                        {
                            try
                            {
                                foreach (string str in HttpContext.Current.Request.UserLanguages)
                                {
                                    try
                                    {
                                        culture = new CultureInfo(str);
                                    }
                                    catch
                                    {
                                    }
                                    if (culture != null)
                                    {
                                        goto Label_0117;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        else if ((staticName != "Current") && !string.IsNullOrEmpty(staticName))
                        {
                            try
                            {
                                culture = new CultureInfo(staticName);
                            }
                            catch
                            {
                            }
                        }
                    Label_0117:
                        cachedCultures[staticName] = culture;
                    Label_0128:
                        if ((culture != null) && (culture.LCID != CultureInfo.InvariantCulture.LCID))
                        {
                            return culture;
                        }
                        return CultureInfo.CurrentUICulture;
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        if (!"Current".Equals(val))
                        {
                            return base.RenderValueForDisplay(prop, val);
                        }
                        return string.Empty;
                    }

                    public static string ToString(CultureInfo culture)
                    {
                        return (culture.DisplayName + (culture.DisplayName.Equals(culture.NativeName) ? string.Empty : (" / " + culture.NativeName)));
                    }

                    public static List<KeyValuePair<string, string>> StaticLocales
                    {
                        get
                        {
                            lock (staticLocales)
                            {
                                if (staticLocales.Count == 0)
                                {
                                    foreach (string str in new string[] { "Current", "Web", "User", "System", "Browser" })
                                    {
                                        staticLocales.Add(new KeyValuePair<string, string>(str, HttpUtility.HtmlDecode("&rarr; ") + ProductPage.GetProductResource("Locale" + str, new object[0])));
                                    }
                                }
                            }
                            return staticLocales;
                        }
                    }
                }

                public class String : JsonSchemaManager.Property.Type
                {
                    public override object FromPostedValue(JsonSchemaManager.Property prop, string value, JsonSchemaManager.Schema owner)
                    {
                        return base.FromPostedValue(prop, ProductPage.GetResource("Tool_ItemEditor_NewDesc", new object[] { ProductPage.GetProductResource("Tool_" + owner.Name + "_TitleSingular", new object[0]) }).Equals(value) ? string.Empty : value, owner);
                    }

                    public static bool IsPassword(IDictionary rawSchema)
                    {
                        return ((rawSchema["is_password"] is bool) && ((bool) rawSchema["is_password"]));
                    }

                    public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
                    {
                        int num;
                        bool flag = IsPassword(prop.RawSchema);
                        string str2 = flag ? "**(password set)**" : base.RenderValueForDisplay(prop, val);
                        if ((!flag && int.TryParse(prop.RawSchema["lines"] + string.Empty, out num)) && (num > 1))
                        {
                            string str;
                            return string.Join(string.IsNullOrEmpty(str = prop.RawSchema["multiline_summary_delimiter"] + string.Empty) ? ", " : str, str2.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                        }
                        return str2;
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        int num;
                        object obj2 = prop.RawSchema["lines"];
                        string str = prop.RawSchema["validator"] + string.Empty;
                        string s = string.Empty;
                        string str3 = instance["id"] + "_" + prop.Name;
                        if (!string.IsNullOrEmpty(str))
                        {
                            s = string.Concat(new object[] { 
                                "var oval=jQuery('#", SPEncode.ScriptEncode(str3), "').val(),val=", str, "('", SPEncode.ScriptEncode(instance["id"] + string.Empty), "', '", SPEncode.ScriptEncode(prop.Name), "', '", SPEncode.ScriptEncode(prop.DefaultValue + string.Empty), "');if(val!=oval)jQuery('#roxiteminstdesc_", instance["id"], "_", prop.Name, "').show();if(val===null)setTimeout('jQuery(\\\"#", SPEncode.ScriptEncode(str3), 
                                "\\\").focus();',250);else jQuery('#", SPEncode.ScriptEncode(str3), "').val(val);"
                             });
                        }
                        s = s + "roxHasChanged();";
                        if (int.TryParse(obj2 + string.Empty, out num) && (num > 1))
                        {
                            return string.Format("<textarea onchange=\"" + HttpUtility.HtmlAttributeEncode(s) + "\" class=\"{2}\" rows=\"{3}\" id=\"{0}\" name=\"{0}\"{4}>{1}</textarea>", new object[] { str3, HttpUtility.HtmlEncode(instance[prop.Name] + string.Empty), this.CssClass + ((disabled || readOnly) ? " rox-iteminst-edit-String-readonly" : string.Empty), num, (disabled || readOnly) ? " readonly=\"readonly\" " : string.Empty });
                        }
                        return string.Format("<input onchange=\"" + HttpUtility.HtmlAttributeEncode(s) + "\" class=\"{2}\" type=\"" + (IsPassword(prop.RawSchema) ? "password" : "text") + "\" id=\"{0}\" name=\"{0}\" value=\"{1}\"{3}/>", new object[] { str3, HttpUtility.HtmlEncode(instance[prop.Name] + string.Empty), this.CssClass + ((disabled || readOnly) ? " rox-iteminst-edit-String-readonly" : string.Empty), (disabled || readOnly) ? " readonly=\"readonly\" " : string.Empty });
                    }
                }

                public class WebSet : JsonSchemaManager.Property.Type
                {
                    internal virtual bool IsSupported(WebTemplateType template)
                    {
                        return true;
                    }

                    public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
                    {
                        string str = string.Empty;
                        string formKey = base.GetFormKey(instance, prop);
                        Config config = new Config(instance[prop.Name]);
                        object obj2 = str;
                        string str4 = string.Concat(new object[] { 
                            obj2, "<div id=\"roxlistsetouter_", formKey, "\" class=\"", this.CssClass, "\"><select onchange=\"roxHasChanged();\" id=\"", formKey, "_w_n\" name=\"", formKey, "_w_n\"><option value=\"0\"", !config.WebNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_WebSet_All", new object[] { prop.Owner.Owner.SiteScope ? ("&quot;" + HttpUtility.HtmlEncode(prop.Owner.Owner.Web.Title) + "&quot;") : string.Empty }], "</option><option value=\"1\"", 
                            config.WebNone ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", base["Tool_ItemEditor_WebSet_None", new object[] { prop.Owner.Owner.SiteScope ? ("&quot;" + HttpUtility.HtmlEncode(prop.Owner.Owner.Web.Title) + "&quot;") : string.Empty }], "</option></select> <a href=\"#noop\" onclick=\"roxToggleListSel('", instance["id"], "', '", prop.Name, "');\">", base["Tool_ItemEditor_ListSet_Except", new object[] { (config.WebSet.Count > 1) ? (config.WebSet.Count - 1) : 0 }], "</a></div>"
                         });
                        string str5 = (str4 + "<div id=\"roxlistsetinner_" + formKey + "\" style=\"display: none;\" class=\"" + this.CssClass + "-inner\">") + "<div class=\"" + this.CssClass + "-exbox\" style=\"float: left; width: 54%;\">";
                        string str6 = str5 + "<div>" + base["Tool_ItemEditor_ListSet_ExceptView", new object[0]] + "<br/><input onclick=\"roxHasChanged();\" id=\"" + formKey + "_w_d\" name=\"" + formKey + "_w_d\" type=\"checkbox\"" + (config.WebRoot ? " checked=\"checked\"" : string.Empty) + (disabled ? " disabled=\"disabled\" " : string.Empty) + (readOnly ? " readonly=\"readonly\" " : string.Empty) + " /> <label for=\"" + formKey + "_w_d\">" + base["Tool_ItemEditor_WebSet_ExceptRootWeb", new object[0]] + "</label></div>";
                        str = str6 + "<div>" + base["Tool_ItemEditor_WebSet_ExceptTemplate", new object[0]] + "</div><select onchange=\"roxHasChanged();\" size=\"10\" id=\"" + formKey + "_w_t\" name=\"" + formKey + "_w_t\" multiple=\"multiple\">";
                        foreach (WebTemplateType type in Enum.GetValues(typeof(WebTemplateType)))
                        {
                            if (this.IsSupported(type))
                            {
                                string str3;
                                object obj3 = str;
                                str = string.Concat(new object[] { obj3, "<option value=\"", type, "\"", config.IsChecked(type) ? " selected=\"selected\"" : string.Empty, disabled ? " disabled=\"disabled\" " : string.Empty, readOnly ? " readonly=\"readonly\" " : string.Empty, ">", (type >= WebTemplateType.GLOBAL) ? (((int) type) + " &mdash; ") : string.Empty, type, string.IsNullOrEmpty(str3 = base["Tool_ItemEditor_WebSet_Temp_" + type, new object[0]]) ? str3 : (" (" + str3 + ")"), "</option>" });
                            }
                        }
                        string str7 = (str + "</select>") + "</div><div class=\"" + this.CssClass + "-exbox\" style=\"float: right; width: 40%;\">";
                        return ((str7 + "<div><br/>" + base["Tool_ItemEditor_WebSet_ExceptWebName", new object[0]] + "<a class=\"rox-iteminst-edit-Pick\" style=\"display: none;\" href=\"#\"/><img src=\"/_layouts/images/blank.gif\" width=\"16\" height=\"16\"/></a></div><textarea onchange=\"roxHasChanged();\" rows=\"10\" class=\"rox-iteminst-edit-String" + ((disabled || readOnly) ? " rox-iteminst-edit-String-readonly" : string.Empty) + "\" id=\"" + formKey + "_w_i\" name=\"" + formKey + "_w_i\"" + ((disabled || readOnly) ? " readonly=\"readonly\" " : string.Empty) + ">" + string.Join("\n", config.WebNames) + "</textarea>") + "</div>" + "&nbsp;<br style=\"clear: both;\"/></div>");
                    }

                    public override string ToString(JsonSchemaManager.Property prop, object val)
                    {
                        return string.Empty;
                    }

                    public override void Update(IDictionary inst, JsonSchemaManager.Property prop, HttpContext context, string id)
                    {
                        Config config = new Config(inst[prop.Name]);
                        Converter<string, string> converter = k => context.Request.Form[id + k] + string.Empty;
                        string str = converter("_w_n");
                        string str2 = converter("_w_d");
                        string str3 = converter("_w_i");
                        string str4 = converter("_w_t");
                        config.WebNone = "1".Equals(str);
                        config.WebRoot = "on".Equals(str2, StringComparison.InvariantCultureIgnoreCase);
                        config.WebNames = str3.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        config.WebTemplates = str4.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        inst[prop.Name] = config.WebSet;
                    }

                    public override string CssClass
                    {
                        get
                        {
                            return ("rox-iteminst-edit-control rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.ListSet).Name);
                        }
                    }

                    public class Config
                    {
                        public readonly IDictionary WebSet;

                        public Config(object val)
                        {
                            this.WebSet = val as IDictionary;
                            if (this.WebSet == null)
                            {
                                this.WebSet = new OrderedDictionary();
                            }
                        }

                        public bool IsChecked(roxority.SharePoint.JsonSchemaManager.Property.Type.WebSet.WebTemplateType val)
                        {
                            return (Array.IndexOf<string>(this.WebTemplates, val.ToString()) >= 0);
                        }

                        public bool IsMatch(SPWeb web)
                        {
                            string[] strArray;
                            string[] strArray2;
                            bool flag = false;
                            if (web == null)
                            {
                                return true;
                            }
                            if ((!flag && this.WebRoot) && web.IsRootWeb)
                            {
                                flag = true;
                            }
                            if ((!flag && ((strArray = this.WebNames) != null)) && (strArray.Length > 0))
                            {
                                foreach (string str in strArray)
                                {
                                    if (flag = ((web.Url.IndexOf(str.Trim(), StringComparison.InvariantCultureIgnoreCase) >= 0) || str.Trim().Equals(web.Title.Trim(), StringComparison.InvariantCultureIgnoreCase)) || web.ID.Equals(ProductPage.GetGuid(str.Trim(), true)))
                                    {
                                        break;
                                    }
                                }
                            }
                            if ((!flag && ((strArray2 = this.WebTemplates) != null)) && (strArray2.Length > 0))
                            {
                                foreach (string str2 in strArray2)
                                {
                                    if (flag = str2.Equals(web.WebTemplate, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        break;
                                    }
                                }
                            }
                            return ((this.WebNone && flag) || (!this.WebNone && !flag));
                        }

                        public string[] WebNames
                        {
                            get
                            {
                                ArrayList list = this.WebSet["i"] as ArrayList;
                                if (list == null)
                                {
                                    return new string[0];
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                List<string> list = ((value == null) ? new List<string>() : new List<string>(value)).ConvertAll<string>(s => s.Trim());
                                ProductPage.RemoveDuplicates<string>(list);
                                if (list.Count == 0)
                                {
                                    this.WebSet.Remove("i");
                                }
                                else
                                {
                                    this.WebSet["i"] = new ArrayList(list);
                                }
                            }
                        }

                        public bool WebNone
                        {
                            get
                            {
                                return "1".Equals(this.WebSet["n"]);
                            }
                            set
                            {
                                this.WebSet["n"] = value ? "1" : "0";
                            }
                        }

                        public bool WebRoot
                        {
                            get
                            {
                                return "1".Equals(this.WebSet["d"]);
                            }
                            set
                            {
                                if (value)
                                {
                                    this.WebSet["d"] = "1";
                                }
                                else
                                {
                                    this.WebSet.Remove("d");
                                }
                            }
                        }

                        public string[] WebTemplates
                        {
                            get
                            {
                                ArrayList list = this.WebSet["t"] as ArrayList;
                                if (list == null)
                                {
                                    list = new ArrayList();
                                }
                                return (list.ToArray(typeof(string)) as string[]);
                            }
                            set
                            {
                                ArrayList list = new ArrayList((value == null) ? 0 : value.Length);
                                foreach (string str in value)
                                {
                                    list.Add(str);
                                }
                                if (list.Count == 0)
                                {
                                    this.WebSet.Remove("t");
                                }
                                else
                                {
                                    this.WebSet["t"] = list;
                                }
                            }
                        }
                    }

                    public enum WebTemplateType
                    {
                        AccSvr = -1,
                        BDR = 7,
                        BICenterSite = -2,
                        BLANKINTERNET = 0x35,
                        BLANKINTERNETCONTAINER = 0x34,
                        BLOG = 9,
                        CENTRALADMIN = 3,
                        CMSPUBLISHING = 0x27,
                        ENTERWIKI = -3,
                        GLOBAL = 0,
                        MPS = 2,
                        OFFILE = 0x3893,
                        OSRV = 40,
                        PPSMASite = -4,
                        PROFILES = 0x33,
                        PUBLISHING = -5,
                        PWA = 0x184d,
                        PWS = 0x1847,
                        SGS = -6,
                        SPSMSITE = 0x16,
                        SPSMSITEHOST = 0x36,
                        SPSNHOME = 0x21,
                        SPSPERS = 0x15,
                        SPSPORTAL = 0x2f,
                        SPSREPORTCENTER = 0x26,
                        SPSSITES = 0x22,
                        SRCHCEN = 50,
                        SRCHCENTERFAST = -7,
                        SRCHCENTERLITE = 90,
                        STS = 1,
                        TenantAdmin = -8,
                        VISPR = -9,
                        VISPRUS = -10,
                        WebManifest = -11,
                        WIKI = 4
                    }
                }
            }
        }

        public class Schema
        {
            private JsonSchemaManager.Property descProp;
            internal IDictionary instDict;
            internal readonly string[] lp0;
            internal readonly string[] lp2;
            internal readonly string[] lp4;
            public readonly string Name;
            public readonly JsonSchemaManager Owner;
            public readonly List<JsonSchemaManager.Property> Properties;
            public readonly SortedDictionary<string, string> PropTabs;
            public readonly IDictionary RawSchema;
            public readonly bool Saved;
            public bool SavedSilent;
            public readonly Exception SaveError;
            internal Converter<KeyValuePair<IDictionary, JsonSchemaManager.Property>, bool> ShouldSerialize;

            public Schema(JsonSchemaManager schemaManager, string name, IDictionary rawSchema)
            {
                string str3;
                this.Properties = new List<JsonSchemaManager.Property>();
                this.PropTabs = new SortedDictionary<string, string>();
                string tab = string.Empty;
                HttpContext current = null;
                IDictionary dictionary = new OrderedDictionary();
                JsonSchemaManager.ISchemaExtender extender = null;
                this.Name = name;
                this.RawSchema = rawSchema;
                this.Owner = schemaManager;
                if (string.IsNullOrEmpty(str3 = ProductPage.GetProductResource("PL_" + name + "_0", new object[0])))
                {
                    this.lp0 = new string[0];
                }
                else
                {
                    this.lp0 = str3.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (string.IsNullOrEmpty(str3 = ProductPage.GetProductResource("PL_" + name + "_2", new object[0])))
                {
                    this.lp2 = new string[0];
                }
                else
                {
                    this.lp2 = str3.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (string.IsNullOrEmpty(str3 = ProductPage.GetProductResource("PL_" + name + "_4", new object[0])))
                {
                    this.lp4 = new string[0];
                }
                else
                {
                    this.lp4 = str3.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                foreach (DictionaryEntry entry in this.RawSchema)
                {
                    string str2;
                    if (!(str2 = entry.Key + string.Empty).StartsWith("_"))
                    {
                        JsonSchemaManager.Property property;
                        this.Properties.Add(property = new JsonSchemaManager.Property(this, str2, entry.Value as IDictionary));
                        if (string.IsNullOrEmpty(tab))
                        {
                            tab = property.Tab;
                        }
                        if (!this.PropTabs.ContainsKey(property.Tab))
                        {
                            int num;
                            this.PropTabs[property.Tab] = ((num = property.Tab.IndexOf("__")) > 0) ? ProductPage.GetResource("PC_" + this.Name + "_" + property.Tab.Substring(0, num) + "_" + property.Tab.Substring(num + 2), new object[0]) : ProductPage.GetProductResource("PT_" + this.Name + "_" + property.Tab, new object[0]);
                        }
                    }
                    else if ("_dyn".Equals(str2) && !string.IsNullOrEmpty(str3 = entry.Value + string.Empty))
                    {
                        try
                        {
                            extender = null;
                            extender = Reflector.Current.New(str3, new object[0]) as JsonSchemaManager.ISchemaExtender;
                        }
                        catch
                        {
                        }
                        if (extender != null)
                        {
                            extender.InitSchema(this);
                        }
                    }
                }
                dictionary["type"] = "String";
                dictionary["tab"] = tab;
                this.Properties.Insert(0, new JsonSchemaManager.Property(this, "name", dictionary));
                dictionary = new OrderedDictionary();
                dictionary["type"] = "WebSet";
                dictionary["tab"] = tab;
                if (this.HasWebs)
                {
                    this.Properties.Insert(1, new JsonSchemaManager.Property(this, "webs", dictionary));
                }
                try
                {
                    current = HttpContext.Current;
                }
                catch
                {
                }
                if (((!JsonSchemaManager.noSave && (current != null)) && ((current.Request != null) && "POST".Equals(current.Request.HttpMethod, StringComparison.InvariantCultureIgnoreCase))) && ((!string.IsNullOrEmpty(current.Request.Form["roxitemsave"]) && this.Name.Equals(current.Request.Form["roxitemsaveschema"])) && !current.Items.Contains(current.Request.Form["roxitemsave"])))
                {
                    try
                    {
                        current.Items[current.Request.Form["roxitemsave"]] = new object();
                        this.SaveChanges(current);
                        this.Saved = true;
                    }
                    catch (Exception exception)
                    {
                        this.SaveError = exception;
                    }
                }
            }

            public IDictionary CreateDefaultInstance()
            {
                IDictionary dictionary = new OrderedDictionary();
                dictionary["id"] = "default";
                foreach (JsonSchemaManager.Property property in this.Properties)
                {
                    dictionary[property.Name] = JsonSchemaManager.GetDisplayValue(property.DefaultValue);
                }
                dictionary["name"] = ProductPage.GetResource("Tool_ItemEditor_DefaultName", new object[] { ProductPage.GetProductResource("Tool_" + this.Name + "_TitleSingular", new object[0]) });
                if ((this.Name != "DataSources") && (this.Name != "DataFieldFormats"))
                {
                    dictionary["desc"] = ProductPage.GetResource("Tool_ItemEditor_DefaultDesc", new object[] { ProductPage.GetProductResource("Tool_" + this.Name + "_TitleSingular", new object[0]), this.Owner.ProdPage.ProductName, ProductPage.GetProductResource("Tool_" + this.Name + "_Title", new object[0]), ProductPage.GetSiteTitle(ProductPage.GetContext()) });
                }
                return dictionary;
            }

            public string GetInstanceDescription(IDictionary inst)
            {
                if (this.descProp == null)
                {
                    foreach (JsonSchemaManager.Property property in this.Properties)
                    {
                        if ((property.RawSchema["is_desc"] is bool) && ((bool) property.RawSchema["is_desc"]))
                        {
                            this.descProp = property;
                            break;
                        }
                        if ((property.Name == "desc") && (this.descProp == null))
                        {
                            this.descProp = property;
                        }
                    }
                }
                if (this.descProp != null)
                {
                    return this.descProp.PropertyType.RenderValueForDisplay(this.descProp, inst[this.descProp.Name]);
                }
                return string.Empty;
            }

            public IEnumerable<IDictionary> GetInstances(SPWeb web, SPList list, SPView view)
            {
                JsonSchemaManager.Property iteratorVariable0 = null;
                IEnumerable<IDictionary> instances = this.Instances;
                if (instances != null)
                {
                    if ((list != null) || (view != null))
                    {
                        foreach (JsonSchemaManager.Property property in this.Properties)
                        {
                            if (property.PropertyType is JsonSchemaManager.Property.Type.ListSet)
                            {
                                iteratorVariable0 = property;
                                break;
                            }
                        }
                    }
                    foreach (IDictionary iteratorVariable3 in instances)
                    {
                        JsonSchemaManager.Property.Type.ListSet.Config iteratorVariable2;
                        if (this.HasWebs && (!new JsonSchemaManager.Property.Type.WebSet.Config(iteratorVariable3["webs"]).IsMatch(web) || ((iteratorVariable0 != null) && (!(iteratorVariable2 = new JsonSchemaManager.Property.Type.ListSet.Config(iteratorVariable3[iteratorVariable0.Name])).IsMatch(list, this.Owner) || !iteratorVariable2.IsMatch(view)))))
                        {
                            continue;
                        }
                        yield return iteratorVariable3;
                    }
                }
            }

            internal List<JsonSchemaManager.Property> GetPropertiesNoDuplicates()
            {
                string tab = null;
                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                List<JsonSchemaManager.Property> list3 = new List<JsonSchemaManager.Property>();
                List<JsonSchemaManager.Property> list4 = new List<JsonSchemaManager.Property>(this.Properties);
                list4.Sort(delegate (JsonSchemaManager.Property one, JsonSchemaManager.Property two) {
                    int num = (one.Tab.Contains("_") && two.Tab.Contains("_")) ? 0 : one.Tab.CompareTo(two.Tab);
                    if (num != 0)
                    {
                        return num;
                    }
                    return this.Properties.IndexOf(one).CompareTo(this.Properties.IndexOf(two));
                });
                foreach (JsonSchemaManager.Property property in list4)
                {
                    int num;
                    if (property.Tab != tab)
                    {
                        if (!string.IsNullOrEmpty(tab) && !list.Contains(tab))
                        {
                            list.Add(tab);
                        }
                        tab = property.Tab;
                    }
                    if (!list.Contains(property.Tab) && (((num = property.Name.IndexOf('_')) < 0) || !list2.Contains(property.Tab + property.Name.Substring(num))))
                    {
                        list3.Add(property);
                        if (num > 0)
                        {
                            list2.Add(property.Tab + property.Name.Substring(num));
                        }
                    }
                }
                return list3;
            }

            public void Import(string json)
            {
                IDictionary dictionary = JSON.JsonDecode(json) as IDictionary;
                IDictionary instDict = this.InstDict;
                if (dictionary.Count > 0)
                {
                    foreach (DictionaryEntry entry in dictionary)
                    {
                        instDict[entry.Key] = entry.Value;
                    }
                }
                this.SaveChanges(null);
            }

            public void SaveChanges(HttpContext context)
            {
                object obj2 = null;
                OrderedDictionary instDict = this.InstDict as OrderedDictionary;
                if (context != null)
                {
                    foreach (DictionaryEntry entry in instDict)
                    {
                        foreach (JsonSchemaManager.Property property in this.Properties)
                        {
                            property.PropertyType.Update(entry.Value as IDictionary, property, context);
                        }
                    }
                    if ("1".Equals(context.Request.Form["roxitemhasnew"]) && !"new".Equals(context.Request.Form["roxitemdelete"]))
                    {
                        if (!this.Li2)
                        {
                            throw new Exception(ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetResource("Tool_ItemEditor_Add", new object[] { ProductPage.GetProductResource("Tool_" + this.Name + "_TitleSingular", new object[0]) }), "Basic" }));
                        }
                        OrderedDictionary inst = new OrderedDictionary();
                        inst["id"] = ProductPage.GuidLower(Guid.NewGuid(), false);
                        foreach (JsonSchemaManager.Property property2 in this.Properties)
                        {
                            property2.PropertyType.Update(inst, property2, context, property2.PropertyType.GetFormKey("new", property2));
                        }
                        instDict.Insert(0, inst["id"], inst);
                    }
                    else
                    {
                        string str;
                        if (!string.IsNullOrEmpty(str = context.Request.Form["roxitemdelete"]))
                        {
                            instDict.Remove(str);
                            this.SavedSilent = true;
                        }
                        else
                        {
                            OrderedDictionary dictionary;
                            if (!string.IsNullOrEmpty(str = context.Request.Form["roxitemmoveup"]))
                            {
                                dictionary = new OrderedDictionary(instDict.Count);
                                foreach (DictionaryEntry entry2 in instDict)
                                {
                                    if (str.Equals(entry2.Key) && (dictionary.Count > 0))
                                    {
                                        dictionary.Insert(dictionary.Count - 1, entry2.Key, entry2.Value);
                                    }
                                    else
                                    {
                                        dictionary[entry2.Key] = entry2.Value;
                                    }
                                }
                                instDict = dictionary;
                                this.SavedSilent = true;
                            }
                            else if (!string.IsNullOrEmpty(str = context.Request.Form["roxitemmovedn"]))
                            {
                                dictionary = new OrderedDictionary(instDict.Count);
                                foreach (DictionaryEntry entry3 in instDict)
                                {
                                    if (str.Equals(entry3.Key))
                                    {
                                        obj2 = entry3.Value;
                                    }
                                    else
                                    {
                                        dictionary[entry3.Key] = entry3.Value;
                                        if (obj2 != null)
                                        {
                                            dictionary[str] = obj2;
                                            obj2 = null;
                                        }
                                    }
                                }
                                if ((obj2 != null) && (dictionary.Count < instDict.Count))
                                {
                                    dictionary[str] = obj2;
                                }
                                instDict = dictionary;
                                this.SavedSilent = true;
                            }
                        }
                    }
                }
                this.Owner.Update(this, this.Key, this.instDict = instDict);
            }

            public bool CanAddNew
            {
                get
                {
                    return (this.Li2 && this.Owner.ProdPage.IsApplicableAdmin);
                }
            }

            public bool HasWebs
            {
                get
                {
                    return this["has_webs", true];
                }
            }

            public int InstanceCount
            {
                get
                {
                    return this.InstDict.Count;
                }
            }

            public IEnumerable<IDictionary> Instances
            {
                get
                {
                    IDictionaryEnumerator enumerator = this.InstDict.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                        yield return (current.Value as IDictionary);
                    }
                }
            }

            internal IDictionary InstDict
            {
                get
                {
                    bool flag = false;
                    bool flag2 = false;
                    bool flag3 = ProductPage.Config<bool>(ProductPage.GetContext(), "NoDefaultInst");
                    OrderedDictionary instDict = null;
                    if (this.instDict == null)
                    {
                        this.instDict = JSON.JsonDecode(this.Owner.Storage[this.Key] + string.Empty, typeof(OrderedDictionary)) as IDictionary;
                        if (this.instDict == null)
                        {
                            this.instDict = new OrderedDictionary();
                        }
                        else
                        {
                            flag2 = true;
                        }
                    }
                    if (this.Owner.SiteScope && flag3)
                    {
                        this.instDict.Remove("default");
                    }
                    foreach (DictionaryEntry entry in this.instDict)
                    {
                        if ("default".Equals(entry.Key))
                        {
                            flag = true;
                        }
                        IDictionary dictionary2 = entry.Value as IDictionary;
                        if (dictionary2 != null)
                        {
                            foreach (JsonSchemaManager.Property property in this.Properties)
                            {
                                string str;
                                if ((string.IsNullOrEmpty(str = dictionary2[property.Name] + string.Empty) && (property.RawSchema["default_if_empty"] is bool)) && ((bool) property.RawSchema["default_if_empty"]))
                                {
                                    object obj2;
                                    dictionary2[property.Name] = obj2 = property.DefaultValue;
                                    str = obj2 + string.Empty;
                                }
                                if ((!this.Li4 && !property.Le) || property.ReadOnly)
                                {
                                    dictionary2[property.Name] = property.DefaultValue;
                                }
                                else if ((flag2 && JsonSchemaManager.Property.Type.String.IsPassword(property.RawSchema)) && !string.IsNullOrEmpty(str))
                                {
                                    try
                                    {
                                        dictionary2[property.Name] = Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(str), ProductPage.Assembly.GetName().GetPublicKeyToken(), DataProtectionScope.LocalMachine));
                                    }
                                    catch
                                    {
                                        dictionary2[property.Name] = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                    if ((!flag && this["has_default", false]) && (this.Owner.SiteScope && !flag3))
                    {
                        IDictionary dictionary = this.CreateDefaultInstance();
                        instDict = this.instDict as OrderedDictionary;
                        if (instDict != null)
                        {
                            instDict.Insert(0, "default", dictionary);
                        }
                        else
                        {
                            this.instDict["default"] = dictionary;
                        }
                    }
                    if (!this.Li2 && (this.instDict.Count > 1))
                    {
                        if (!flag && ((instDict != null) || ((instDict = this.instDict as OrderedDictionary) != null)))
                        {
                            for (int i = 1; i < instDict.Count; i++)
                            {
                                instDict.RemoveAt(i);
                            }
                        }
                        else
                        {
                            instDict = new OrderedDictionary();
                            if (flag)
                            {
                                instDict["default"] = this.instDict["default"];
                            }
                            else
                            {
                                foreach (DictionaryEntry entry2 in this.instDict)
                                {
                                    instDict[entry2.Key] = entry2.Value;
                                    break;
                                }
                            }
                            this.instDict = instDict;
                        }
                    }
                    return this.instDict;
                }
            }

            public JsonSchemaManager.Property this[string propName]
            {
                get
                {
                    return this.Properties.Find(prop => prop.Name == propName);
                }
            }

            public bool this[string configKey, bool defVal]
            {
                get
                {
                    object obj2 = null;
                    IDictionary dictionary = this.RawSchema["_config"] as IDictionary;
                    if ((dictionary != null) && dictionary.Contains(configKey))
                    {
                        obj2 = dictionary[configKey];
                    }
                    if (obj2 is bool)
                    {
                        return (bool) obj2;
                    }
                    return defVal;
                }
            }

            public string this[string configKey, string defVal]
            {
                get
                {
                    object obj2 = null;
                    IDictionary dictionary = this.RawSchema["_config"] as IDictionary;
                    if ((dictionary != null) && dictionary.Contains(configKey))
                    {
                        obj2 = dictionary[configKey];
                    }
                    if (obj2 != null)
                    {
                        return obj2.ToString();
                    }
                    return defVal;
                }
            }

            public string Key
            {
                get
                {
                    return (this.Owner.assemblyName + "_" + this.Name);
                }
            }

            private bool Li2
            {
                get
                {
                    if (!JsonSchemaManager.li2.HasValue || !JsonSchemaManager.li2.HasValue)
                    {
                        JsonSchemaManager.li2 = new bool?(ProductPage.LicEdition(ProductPage.GetContext(), JsonSchemaManager.Li, 2));
                    }
                    return JsonSchemaManager.li2.Value;
                }
            }

            private bool Li4
            {
                get
                {
                    if (!JsonSchemaManager.li4.HasValue || !JsonSchemaManager.li4.HasValue)
                    {
                        JsonSchemaManager.li4 = new bool?(ProductPage.LicEdition(ProductPage.GetContext(), JsonSchemaManager.Li, 4));
                    }
                    return JsonSchemaManager.li4.Value;
                }
            }


        }
    }
}

