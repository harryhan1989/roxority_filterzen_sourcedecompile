namespace roxority.Data.Providers
{
    using Microsoft.SharePoint;
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class UserAccounts : UserDataSource
    {
        private ArrayList combined;
        private Record curRec;
        private IEnumerator enumerator;
        private readonly Dictionary<string, SPField> fields;
        private SPGroupCollection groupCol;
        private readonly string[] hiddenFields;
        private readonly string[] includeFields;
        private static readonly Dictionary<DataSource.KnownProperty, string> knownMap = new Dictionary<DataSource.KnownProperty, string>();
        private RecordPropertyCollection propCol;
        public static readonly Dictionary<string, string> PropMappings = new Dictionary<string, string>();
        public readonly SPWeb Site;
        public readonly SPSite SpSite;
        private SPUserCollection userCol;
        public readonly SPList Users;

        static UserAccounts()
        {
            PropMappings["AccountName"] = "Name";
            PropMappings["WorkEmail"] = "EMail";
            PropMappings["PreferredName"] = "Title";
            PropMappings["PictureURL"] = "Picture";
            PropMappings["SPS-SipAddress"] = "SipAddress";
            PropMappings["CellPhone"] = "MobilePhone";
            PropMappings["Title"] = "JobTitle";
            PropMappings["SPS-Location"] = "Office";
            PropMappings["SPS-Responsibility"] = "SPSResponsibility";
            PropMappings["AboutMe"] = "Notes";
            knownMap[DataSource.KnownProperty.FriendlyName] = "Title";
            knownMap[DataSource.KnownProperty.Picture] = "Picture";
        }

        public UserAccounts() : this(SPContext.Current.Web)
        {
        }

        public UserAccounts(SPWeb site)
        {
            this.hiddenFields = new string[] { "UserSelection", "NameWithPictureAndDetails", "NameWithPicture", "EditUser", "ContentTypeDisp" };
            this.includeFields = new string[] { "Title", "Name", "EMail", "MobilePhone", "Notes", "SipAddress", "Picture", "Department", "JobTitle" };
            this.fields = new Dictionary<string, SPField>();
            this.SpSite = (site == null) ? null : new SPSite(site.Site.ID);
            this.Site = (this.SpSite == null) ? null : this.SpSite.OpenWeb(site.ID);
            if ((this.SpSite != null) && (this.Site != null))
            {
                SPSecurity.CatchAccessDeniedException = false;
                this.SpSite.CatchAccessDeniedException = false;
                this.Users = GetUserList(this.Site);
                try
                {
                    site.Site.CatchAccessDeniedException = false;
                }
                catch
                {
                }
            }
        }

        public override bool AllowDateParsing(RecordProperty prop)
        {
            SPFieldCalculated property = prop.Property as SPFieldCalculated;
            if (property != null)
            {
                return (property.OutputType == SPFieldType.DateTime);
            }
            return true;
        }

        public override void Dispose()
        {
            if (!base.noDispose)
            {
                this.Site.Dispose();
                this.SpSite.Dispose();
                base.Dispose();
            }
        }

        internal string Get(SPListItem item, string fieldName)
        {
            SPField field;
            if (!this.fields.TryGetValue(fieldName, out field))
            {
                this.fields[fieldName] = field = ProductPage.GetField(this.Users, fieldName);
            }
            if (field == null)
            {
                return null;
            }
            return (string.Empty + item[field.InternalName]);
        }

        internal bool Get(SPListItem item, string fieldName, bool defIfNull)
        {
            SPField field;
            if (!this.fields.TryGetValue(fieldName, out field))
            {
                this.fields[fieldName] = field = ProductPage.GetField(this.Users, fieldName);
            }
            if (field == null)
            {
                return defIfNull;
            }
            return (bool) item[field.InternalName];
        }

        public override string GetFieldInfoUrl(string webUrl, Guid contextID)
        {
            return ProductPage.MergeUrlPaths(webUrl, "_layouts/listedit.aspx?List=" + contextID);
        }

        public override string GetKnownPropName(DataSource.KnownProperty kp)
        {
            if (!knownMap.ContainsKey(kp))
            {
                return base.GetKnownPropName(kp);
            }
            return knownMap[kp];
        }

        public override string GetPropertyDisplayName(RecordProperty prop)
        {
            SPField property = prop.Property as SPField;
            if (property == null)
            {
                return base.GetPropertyDisplayName(prop);
            }
            return property.Title;
        }

        public override RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            SPFieldUrl url;
            SPListItem relItem = rec.RelItem as SPListItem;
            SPField property = prop.Property as SPField;
            SPUser mainItem = rec.MainItem as SPUser;
            if (prop.Name == "roxVcardExport")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { UserDataSource.GetVcardExport(rec) }, null, null, null);
            }
            if ((prop.Name == "roxSiteGroups") && (mainItem != null))
            {
                List<object> list = new List<object>();
                foreach (SPGroup group in ProductPage.TryEach<SPGroup>(mainItem.Groups))
                {
                    try
                    {
                        list.Add(group.Name);
                    }
                    catch
                    {
                    }
                }
                return new RecordPropertyValueCollection(this, rec, prop, list.ToArray(), null, null, null);
            }
            if (prop.Name == "roxUserPersonalUrl")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { base.GetPersonalUrl(rec) }, null, null, null);
            }
            if (prop.Name == "roxUserPublicUrl")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { base.GetPublicUrl(rec) }, null, null, null);
            }
            if ((relItem == null) || (property == null))
            {
                return null;
            }
            object obj2 = relItem[property.Id];
            if (obj2 is DateTime)
            {
                obj2 = ((DateTime) obj2).ToUniversalTime();
            }
            if (((url = property as SPFieldUrl) != null) && (obj2 is string))
            {
                if (url.DisplayFormat == SPUrlFieldFormatType.Image)
                {
                    obj2 = new SPFieldUrlValue(obj2 as string).Url;
                }
                else
                {
                    obj2 = new SPFieldUrlValue(obj2 as string);
                }
            }
            return new RecordPropertyValueCollection(this, rec, prop, (obj2 is object[]) ? (obj2 as object[]) : ((obj2 == null) ? new object[0] : new object[] { ((url != null) ? obj2 : property.GetFieldValueAsHtml(obj2)) }), null, null, null);
        }

        public override Record GetRecord(long recID)
        {
            return this.GetRecord(recID, null);
        }

        public Record GetRecord(long recID, object obj)
        {
            SPListItem relItem = obj as SPListItem;
            SPUser item = obj as SPUser;
            SPGroup byID = obj as SPGroup;
            if (recID <= 0L)
            {
                if (relItem != null)
                {
                    recID = relItem.ID;
                }
                else if (item != null)
                {
                    recID = item.ID;
                }
                else if (byID != null)
                {
                    recID = byID.ID;
                }
            }
            if ((recID > 0L) && (relItem == null))
            {
                try
                {
                    relItem = this.Users.GetItemById((int) recID);
                }
                catch
                {
                }
            }
            if (((recID > 0L) && (item == null)) && (this.UserCollection != null))
            {
                try
                {
                    item = this.UserCollection.GetByID((int) recID);
                }
                catch
                {
                }
            }
            if (((recID > 0L) && (item == null)) && ((byID == null) && (this.GroupCollection != null)))
            {
                try
                {
                    byID = this.GroupCollection.GetByID((int) recID);
                }
                catch
                {
                }
            }
            if (item != null)
            {
                return new Record(this, item, relItem, (relItem != null) ? relItem.UniqueId : Guid.Empty, recID);
            }
            if (byID != null)
            {
                return new Record(this, byID, relItem, (relItem != null) ? relItem.UniqueId : Guid.Empty, recID);
            }
            return null;
        }

        public static SPList GetUserList(SPWeb web)
        {
            SPList catalog = null;
            try
            {
                catalog = web.GetCatalog(SPListTemplateType.UserInformation);
            }
            catch
            {
            }
            if (catalog == null)
            {
                catalog = web.Site.GetCatalog(SPListTemplateType.UserInformation);
            }
            return catalog;
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            string[] c = new string[] { "a", "s", "u", "n" };
            string[] strArray2 = new string[] { "n", "g", "s" };
            base.InitSchema(owner);
            IDictionary pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "u", new ArrayList(c), pmore);
            base.AddSchemaProp(owner, "g", new ArrayList(strArray2), pmore);
            this.InitSchemaForCaching(owner, false, true);
        }

        public override bool MoveNext()
        {
            this.curRec = null;
            return this.Enumerator.MoveNext();
        }

        public override void Reset()
        {
            this.curRec = null;
            this.Enumerator.Reset();
        }

        public override string RewritePropertyName(string name)
        {
            if (!PropMappings.ContainsKey(name))
            {
                return base.RewritePropertyName(name);
            }
            return PropMappings[name];
        }

        public ArrayList CombinedCollection
        {
            get
            {
                if (this.combined == null)
                {
                    this.combined = new ArrayList();
                    if (this.UserCollection != null)
                    {
                        foreach (SPUser user in ProductPage.TryEach<SPUser>(this.UserCollection))
                        {
                            this.combined.Add(user);
                        }
                    }
                    if (this.GroupCollection != null)
                    {
                        foreach (SPGroup group in ProductPage.TryEach<SPGroup>(this.GroupCollection))
                        {
                            this.combined.Add(group);
                        }
                    }
                }
                return this.combined;
            }
        }

        public override Guid ContextID
        {
            get
            {
                return this.Users.ID;
            }
        }

        public override long Count
        {
            get
            {
                return ((this.RequireCaching || (this.Users == null)) ? ((long) (-1)) : ((long) this.Users.ItemCount));
            }
        }

        public override object Current
        {
            get
            {
                object current = this.Enumerator.Current;
                if ((this.curRec == null) && (current != null))
                {
                    this.curRec = this.GetRecord(0L, current);
                }
                return this.curRec;
            }
        }

        public bool EnumerateAllUsers
        {
            get
            {
                if ((base.inst != null) && base.inst.Contains(this.SchemaPropNamePrefix + "_u"))
                {
                    return "a".Equals(base["u"]);
                }
                return true;
            }
        }

        public bool EnumerateSiteGroups
        {
            get
            {
                return "s".Equals(base["g"]);
            }
        }

        public bool EnumerateSiteUsers
        {
            get
            {
                return "s".Equals(base["u"]);
            }
        }

        public bool EnumerateWebGroups
        {
            get
            {
                return "g".Equals(base["g"]);
            }
        }

        public bool EnumerateWebUsers
        {
            get
            {
                return "u".Equals(base["u"]);
            }
        }

        public IEnumerator Enumerator
        {
            get
            {
                if (this.enumerator == null)
                {
                    this.enumerator = this.CombinedCollection.GetEnumerator();
                }
                return this.enumerator;
            }
        }

        public SPGroupCollection GroupCollection
        {
            get
            {
                if (this.groupCol == null)
                {
                    if (this.EnumerateSiteGroups)
                    {
                        this.groupCol = this.Site.SiteGroups;
                    }
                    if (this.EnumerateWebGroups)
                    {
                        this.groupCol = this.Site.Groups;
                    }
                }
                return this.groupCol;
            }
        }

        public override RecordPropertyCollection Properties
        {
            get
            {
                if (this.propCol == null)
                {
                    this.propCol = new RecordPropertyCollection(this);
                    foreach (SPField field in ProductPage.TryEach<SPField>(this.Users.Fields))
                    {
                        if (((!field.Hidden && !field.FromBaseType) && (Array.IndexOf<string>(this.hiddenFields, field.InternalName) < 0)) || (Array.IndexOf<string>(this.includeFields, field.InternalName) >= 0))
                        {
                            this.propCol.Props.Add(new RecordProperty(this, field.InternalName, field));
                        }
                    }
                    base.EnsureUserFields(this.propCol);
                    this.propCol.Props.Add(new RecordProperty(this, "roxUserPersonalUrl", null));
                    this.propCol.Props.Add(new RecordProperty(this, "roxUserPublicUrl", null));
                }
                return this.propCol;
            }
        }

        public override string SchemaPropNamePrefix
        {
            get
            {
                return "ua";
            }
        }

        public SPUserCollection UserCollection
        {
            get
            {
                if (this.userCol == null)
                {
                    if (this.EnumerateAllUsers)
                    {
                        this.userCol = this.Site.AllUsers;
                    }
                    if (this.EnumerateSiteUsers)
                    {
                        this.userCol = this.Site.SiteUsers;
                    }
                    if (this.EnumerateWebUsers)
                    {
                        this.userCol = this.Site.Users;
                    }
                }
                return this.userCol;
            }
        }
    }
}

