namespace roxority.Data.Providers
{
    using Microsoft.SharePoint;
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.DirectoryServices;

    public class Directory : UserDataSource
    {
        internal static Dictionary<Guid, Dictionary<long, string>> cacheHelpers = new Dictionary<Guid, Dictionary<long, string>>();
        private bool clearRecs;
        private Guid? contextID = null;
        private AuthenticationTypes? dirAuth = null;
        private IEnumerator dirEnum;
        private DirectoryEntry dirRoot;
        private SearchScope? dirScope = null;
        private DirectorySearcher dirSearch;
        private SearchResultCollection dirSearchResults;
        private List<IDisposable> disposables = new List<IDisposable>();
        private static readonly Dictionary<DataSource.KnownProperty, string> knownMap = new Dictionary<DataSource.KnownProperty, string>();
        public static readonly Dictionary<string, string> PropMappings = new Dictionary<string, string>();
        private RecordPropertyCollection props;
        private IEnumerator recEnum;
        private List<Record> records = new List<Record>();

        static Directory()
        {
            knownMap[DataSource.KnownProperty.FriendlyName] = "cn";
            knownMap[DataSource.KnownProperty.Picture] = string.Empty;
            PropMappings["PreferredName"] = knownMap[DataSource.KnownProperty.FriendlyName];
            PropMappings["PictureURL"] = string.Empty;
            PropMappings["CellPhone"] = "telephoneNumber";
            PropMappings["Department"] = "department";
            PropMappings["Title"] = "title";
            PropMappings["SPS-Location"] = "physicalDeliveryOfficeName";
            PropMappings["AboutMe"] = "info";
            PropMappings["UserName"] = "sAMAccountName";
        }

        public override void Dispose()
        {
            if (!base.noDispose)
            {
                foreach (IDisposable disposable in this.disposables)
                {
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                this.disposables.Clear();
                this.dirEnum = null;
                this.recEnum = null;
                if (this.dirSearchResults != null)
                {
                    this.dirSearchResults.Dispose();
                    this.dirSearchResults = null;
                }
                if (this.dirSearch != null)
                {
                    this.dirSearch.Dispose();
                    this.dirSearch = null;
                }
                if (this.dirRoot != null)
                {
                    this.dirRoot.Dispose();
                    this.dirRoot = null;
                }
                base.Dispose();
            }
        }

        public override string GetKnownPropName(DataSource.KnownProperty kp)
        {
            if (!knownMap.ContainsKey(kp))
            {
                return base.GetKnownPropName(kp);
            }
            return knownMap[kp];
        }

        public override RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            Converter<string, string> getSourceValue = null;
            DirectoryEntry mainItem = rec.MainItem as DirectoryEntry;
            SearchResult relItem = rec.RelItem as SearchResult;
            ResultPropertyValueCollection rvals = (relItem == null) ? null : relItem.Properties[prop.Name];
            PropertyValueCollection pvals = (mainItem == null) ? null : mainItem.Properties[prop.Name];
            object obj1 = rec.MainItem;
            SPUser user = null;
            if (prop.Name == "roxVcardExport")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { UserDataSource.GetVcardExport(rec) }, null, null, null);
            }
            if (prop.Name == "roxSiteGroups")
            {
                string str;
                List<object> list = new List<object>();
                if (getSourceValue == null)
                {
                    getSourceValue = pn => rec[pn, string.Empty];
                }
                if (!string.IsNullOrEmpty(str = Record.GetSpecialFieldValue(this, "rox___pl", getSourceValue)))
                {
                    try
                    {
                        user = SPContext.Current.Web.AllUsers[str];
                    }
                    catch
                    {
                    }
                    if (user != null)
                    {
                        foreach (SPGroup group in ProductPage.TryEach<SPGroup>(user.Groups))
                        {
                            try
                            {
                                list.Add(group.Name);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                return new RecordPropertyValueCollection(this, rec, prop, list.ToArray(), null, null, null);
            }
            if (rvals != null)
            {
                return new RecordPropertyValueCollection(this, rec, prop, null, rvals.GetEnumerator(), () => rvals.Count, delegate {
                    IEnumerator enumerator = rvals.GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            return enumerator.Current;
                        }
                    }
                    return null;
                });
            }
            if (pvals != null)
            {
                return new RecordPropertyValueCollection(this, rec, prop, null, pvals.GetEnumerator(), () => pvals.Count, () => pvals.Value);
            }
            return null;
        }

        public override Record GetRecord(long recID)
        {
            if (this.records.Count == 0)
            {
                string str;
                Dictionary<long, string> dictionary;
                if (((this.RequireCaching && cacheHelpers.TryGetValue(this.ContextID, out dictionary)) && ((dictionary != null) && dictionary.TryGetValue(recID, out str))) && !string.IsNullOrEmpty(str))
                {
                    return this.MakeRecord(this.MakeEntry(str), false, recID);
                }
                foreach (Record record in this)
                {
                    if (record.RecordID == recID)
                    {
                        return record;
                    }
                }
            }
            if ((recID > 0L) && (recID < this.records.Count))
            {
                return this.records[(int) recID];
            }
            return null;
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            IDictionary pmore = new OrderedDictionary();
            IDictionary dictionary2 = new OrderedDictionary();
            base.InitSchema(owner);
            dictionary2[this.SchemaPropNamePrefix + "_au"] = new string[] { "Secure", "None", "" };
            pmore = new OrderedDictionary();
            pmore["default"] = "LDAP://OU=Departments,DC=global,DC=local";
            base.AddSchemaProp(owner, "cs", "String", pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "sq", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = "Subtree";
            pmore["enumtype"] = typeof(SearchScope).AssemblyQualifiedName;
            base.AddSchemaProp(owner, "ss", "EnumChoice", pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = "None";
            pmore["exclude"] = "Signing,Sealing,ServerBind,FastBind,ReadonlyServer";
            pmore["enumtype"] = typeof(AuthenticationTypes).AssemblyQualifiedName;
            base.AddSchemaProp(owner, "au", "EnumChoice", pmore);
            pmore = new OrderedDictionary();
            pmore["show_if"] = dictionary2;
            base.AddSchemaProp(owner, "us", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["is_password"] = true;
            pmore["show_if"] = dictionary2;
            base.AddSchemaProp(owner, "pw", "String", pmore);
            this.InitSchemaForCaching(owner, false, true);
        }

        internal DirectoryEntry MakeEntry(string path)
        {
            if (string.IsNullOrEmpty(this.DirAuthUser) || string.IsNullOrEmpty(this.DirAuthPass))
            {
                return new DirectoryEntry(path, null, null, this.DirAuth);
            }
            return new DirectoryEntry(path, this.DirAuthUser, this.DirAuthPass, this.DirAuth);
        }

        internal Record MakeRecord(object obj, bool addCacheHelper, long recID)
        {
            Dictionary<long, string> dictionary = null;
            DirectoryEntry item = obj as DirectoryEntry;
            SearchResult relItem = obj as SearchResult;
            if (obj == null)
            {
                return null;
            }
            if (this.props == null)
            {
                this.props = new RecordPropertyCollection(this);
            }
            if (relItem != null)
            {
                item = relItem.GetDirectoryEntry();
                foreach (string str in relItem.Properties.PropertyNames)
                {
                    if (this.props.GetPropertyByName(str) == null)
                    {
                        this.props.Props.Add(new RecordProperty(this, str, null));
                    }
                }
            }
            if (item != null)
            {
                if (addCacheHelper)
                {
                    if ((dictionary == null) && (!cacheHelpers.TryGetValue(this.ContextID, out dictionary) || (dictionary == null)))
                    {
                        cacheHelpers[this.ContextID] = dictionary = new Dictionary<long, string>();
                    }
                    dictionary[recID] = item.Path;
                }
                this.disposables.Add(item);
                foreach (string str2 in item.Properties.PropertyNames)
                {
                    if (this.props.GetPropertyByName(str2) == null)
                    {
                        this.props.Props.Add(new RecordProperty(this, str2, null));
                    }
                }
            }
            return new Record(this, item, relItem, Guid.Empty, recID);
        }

        public override bool MoveNext()
        {
            if (this.recEnum != null)
            {
                return this.recEnum.MoveNext();
            }
            this.clearRecs = true;
            return ((this.DirEnum != null) && this.DirEnum.MoveNext());
        }

        public override void Reset()
        {
            if ((this.recEnum == null) && (this.records.Count > 0))
            {
                this.recEnum = this.records.GetEnumerator();
                this.recEnum.Reset();
            }
        }

        public override string RewritePropertyName(string name)
        {
            if (!PropMappings.ContainsKey(name))
            {
                return base.RewritePropertyName(name);
            }
            return PropMappings[name];
        }

        public override Guid ContextID
        {
            get
            {
                if ((!this.contextID.HasValue || !this.contextID.HasValue) && ((base.JsonInstance != null) && Guid.Empty.Equals(this.contextID = new Guid?(ProductPage.GetGuid(base.JsonInstance["id"] + string.Empty, true)))))
                {
                    this.contextID = new Guid("ef0bd2b1-2c73-597f-b86c-1783eb216994");
                }
                if (!this.contextID.HasValue)
                {
                    return Guid.Empty;
                }
                return this.contextID.Value;
            }
        }

        public override long Count
        {
            get
            {
                if ((this.recEnum != null) || (this.dirSearchResults == null))
                {
                    return ((this.records.Count < 1) ? ((long) (-1)) : ((long) this.records.Count));
                }
                if (this.dirSearchResults.Count < 1)
                {
                    return -1L;
                }
                return (long) this.dirSearchResults.Count;
            }
        }

        public override object Current
        {
            get
            {
                Record record;
                if (this.recEnum != null)
                {
                    return this.recEnum.Current;
                }
                this.records.Add(record = this.MakeRecord(this.DirEnum.Current, this.RequireCaching, (long) (this.records.Count + 1)));
                return record;
            }
        }

        public AuthenticationTypes DirAuth
        {
            get
            {
                if (!this.dirAuth.HasValue || !this.dirAuth.HasValue)
                {
                    try
                    {
                        this.dirAuth = new AuthenticationTypes?((AuthenticationTypes) Enum.Parse(typeof(AuthenticationTypes), base["au", "None"]));
                    }
                    catch
                    {
                        this.dirAuth = 0;
                    }
                }
                return this.dirAuth.Value;
            }
        }

        public string DirAuthPass
        {
            get
            {
                return base["pw", string.Empty];
            }
        }

        public string DirAuthUser
        {
            get
            {
                return base["us", string.Empty];
            }
        }

        public string DirConn
        {
            get
            {
                return base["cs", string.Empty];
            }
        }

        public IEnumerator DirEnum
        {
            get
            {
                if ((this.dirEnum == null) && (this.DirRoot != null))
                {
                    if (this.clearRecs)
                    {
                        this.records.Clear();
                        this.clearRecs = false;
                    }
                    if ((this.DirSearch != null) && ((this.dirSearchResults != null) || ((this.dirSearchResults = this.DirSearch.FindAll()) != null)))
                    {
                        this.dirEnum = this.dirSearchResults.GetEnumerator();
                    }
                    else
                    {
                        this.dirEnum = this.DirRoot.Children.GetEnumerator();
                    }
                }
                return this.dirEnum;
            }
        }

        public string DirQuery
        {
            get
            {
                return base["sq", string.Empty];
            }
        }

        public DirectoryEntry DirRoot
        {
            get
            {
                if (this.dirRoot == null)
                {
                    this.dirRoot = this.MakeEntry(this.DirConn);
                }
                return this.dirRoot;
            }
        }

        public SearchScope DirScope
        {
            get
            {
                if (!this.dirScope.HasValue || !this.dirScope.HasValue)
                {
                    try
                    {
                        this.dirScope = new SearchScope?((SearchScope) Enum.Parse(typeof(SearchScope), base["ss", "Subtree"]));
                    }
                    catch
                    {
                        this.dirScope = (SearchScope)2;
                    }
                }
                return this.dirScope.Value;
            }
        }

        public DirectorySearcher DirSearch
        {
            get
            {
                if (((this.dirSearch == null) && !string.IsNullOrEmpty(this.DirQuery)) && (this.DirRoot != null))
                {
                    this.dirSearch = new DirectorySearcher(this.DirRoot, this.DirQuery, null, this.DirScope);
                }
                return this.dirSearch;
            }
        }

        public override RecordPropertyCollection Properties
        {
            get
            {
                if (this.props == null)
                {
                    this.props = new RecordPropertyCollection(this);
                    IEnumerator enumerator = base.GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            object current = enumerator.Current;
                            goto Label_0041;
                        }
                    }
                }
            Label_0041:
                base.EnsureUserFields(this.props);
                return this.props;
            }
        }

        public override string SchemaPropNamePrefix
        {
            get
            {
                return "ds";
            }
        }
    }
}

