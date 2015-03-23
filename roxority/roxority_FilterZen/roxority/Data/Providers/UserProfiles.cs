namespace roxority.Data.Providers
{
    using Microsoft.Office.Server;
    using Microsoft.Office.Server.UserProfiles;
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Reflection;

    public class UserProfiles : UserDataSource
    {
        internal static Guid appID = Guid.Empty;
        private IDbConnection conn;
        private Record curProf;
        public readonly IEnumerator Enum;
        private static readonly Dictionary<DataSource.KnownProperty, string> knownMap = new Dictionary<DataSource.KnownProperty, string>();
        public readonly UserProfileManager Manager;
        private RecordPropertyCollection propCol;
        public readonly ServerContext SC;

        static UserProfiles()
        {
            knownMap[DataSource.KnownProperty.FriendlyName] = "PreferredName";
            knownMap[DataSource.KnownProperty.Picture] = "PictureURL";
        }

        public UserProfiles() : this(GetServerContext(null))
        {
        }

        public UserProfiles(ServerContext sc)
        {
            this.SC = sc;
            if (this.SC != null)
            {
                try
                {
                    this.Manager = new UserProfileManager(sc, true, true);
                }
                catch (UnauthorizedAccessException)
                {
                    this.Manager = new UserProfileManager(sc, false, true);
                }
                try
                {
                    this.Enum = this.Manager.GetEnumerator();
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            if (!ProductPage.Is14)
            {
                if (Guid.Empty.Equals(appID))
                {
                    try
                    {
                        appID = (Guid) Reflector.Current.Get(Reflector.Current.Get(sc, "SharedResourceProvider", new object[0]), "ApplicationId", new object[0]);
                    }
                    catch
                    {
                        appID = new Guid("df0bd2b0-1c72-497e-a86b-0783eb216993");
                    }
                }
                try
                {
                    typeof(UserProfileManager).GetField("m_bIsSiteAdminInit", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this.Manager, true);
                    typeof(UserProfileManager).GetField("m_bIsSiteAdmin", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this.Manager, true);
                }
                catch
                {
                }
            }
            else if (Guid.Empty.Equals(appID))
            {
                try
                {
                    object obj2;
                    appID = (Guid) (obj2 = typeof(UserProfileManager).BaseType.GetField("m_userProfileApplicationProxy", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this.Manager)).GetType().GetProperty("AppID", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2, null);
                }
                catch
                {
                }
            }
        }

        public UserProfiles(SPWeb site) : this(GetServerContext(site))
        {
        }

        public override bool AllowDateParsing(RecordProperty prop)
        {
            Property property = prop.Property as Property;
            Hashtable hashtable = prop.Property as Hashtable;
            if (((property == null) || (!"date".Equals(property.Type, StringComparison.InvariantCultureIgnoreCase) && !"datenoyear".Equals(property.Type, StringComparison.InvariantCultureIgnoreCase))) && ((hashtable == null) || (!"12".Equals(hashtable["DataTypeID"] + string.Empty) && !"14".Equals(hashtable["DataTypeID"] + string.Empty))))
            {
                return base.AllowDateParsing(prop);
            }
            return true;
        }

        public override void Dispose()
        {
            if (!base.noDispose)
            {
                if (this.conn != null)
                {
                    this.conn.Dispose();
                    this.conn = null;
                }
                base.Dispose();
            }
        }

        protected override string GetDefVal(string schemaProp)
        {
            string defVal = base.GetDefVal(schemaProp);
            if (schemaProp == "vc")
            {
                defVal = defVal + "\r\nTITLE:[" + this.RewritePropertyName("Title") + "]";
            }
            return defVal;
        }

        public override string GetFieldInfoUrl(string webUrl, Guid contextID)
        {
            return ProductPage.MergeUrlPaths(webUrl, "_layouts/MgrProperty.aspx?ProfileType=User&ApplicationID=" + contextID);
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
            string str = null;
            SPWeb web = null;
            Property property = prop.Property as Property;
            Hashtable hashtable = prop.Property as Hashtable;
            if (hashtable != null)
            {
                CultureInfo info;
                SPContext context = ProductPage.GetContext();
                if (context != null)
                {
                    web = context.Web;
                }
                if (((web != null) && (web.CurrentUser != null)) && (web.CurrentUser.RegionalSettings != null))
                {
                    str = hashtable["c_" + web.CurrentUser.RegionalSettings.LocaleId] + string.Empty;
                }
                if ((string.IsNullOrEmpty(str) && (web != null)) && (web.RegionalSettings != null))
                {
                    str = hashtable["c_" + web.RegionalSettings.LocaleId] + string.Empty;
                }
                if ((string.IsNullOrEmpty(str) && (web != null)) && (web.Locale != null))
                {
                    str = hashtable["c_" + web.Locale.LCID] + string.Empty;
                }
                if (string.IsNullOrEmpty(str))
                {
                    foreach (int num in ProductPage.TryEach<object>(ProductPage.WssInstalledCultures))
                    {
                        if (!string.IsNullOrEmpty(str = hashtable["c_" + num] + string.Empty))
                        {
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(str) && ((info = ProductPage.GetFarmCulture(context)) != null))
                {
                    str = hashtable["c_" + info.LCID] + string.Empty;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                foreach (string str2 in hashtable.Keys)
                {
                    if (str2.StartsWith("c_"))
                    {
                        return (hashtable[str2] + string.Empty);
                    }
                }
            }
            if (property == null)
            {
                return base.GetPropertyDisplayName(prop);
            }
            return property.DisplayName;
        }

        public override RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            roxority.Shared.Func<int> getCount = null;
            roxority.Shared.Func<object> getValue = null;
            bool isHtml = false;
            UserProfile mainItem = rec.MainItem as UserProfile;
            List<object> list = null;
            SPUser user = null;
            Property property2 = prop.Property as Property;
            if (property2 != null)
            {
                isHtml = property2.Type == "HTML";
            }
            if (prop.Name == "roxVcardExport")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { UserDataSource.GetVcardExport(rec) }, null, null, null);
            }
            if (prop.Name == "roxSiteGroups")
            {
                string str;
                RecordProperty property;
                RecordPropertyValueCollection values;
                list = new List<object>();
                if ((((property = rec.DataSource.Properties.GetPropertyByName("AccountName")) != null) && ((values = this.GetPropertyValues(rec, property)) != null)) && !string.IsNullOrEmpty(str = values.Value + string.Empty))
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
            if (prop.Name == "roxUserPersonalUrl")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { base.GetPersonalUrl(rec) }, null, null, null);
            }
            if (prop.Name == "roxUserPublicUrl")
            {
                return new RecordPropertyValueCollection(this, rec, prop, new object[] { base.GetPublicUrl(rec) }, null, null, null);
            }
            UserProfileValueCollection vals = mainItem[prop.Name];
            if (getCount == null)
            {
                getCount = () => vals.Count;
            }
            if (getValue == null)
            {
                getValue = delegate {
                    if (isHtml)
                    {
                        return "<roxhtml/>" + vals.Value;
                    }
                    return vals.Value;
                };
            }
            return new RecordPropertyValueCollection(this, rec, prop, null, isHtml ? Array.ConvertAll<object, string>(new ArrayList(vals).ToArray(), o => "<roxhtml/>" + o).GetEnumerator() : vals.GetEnumerator(), getCount, getValue);
        }

        public override Record GetRecord(long recID)
        {
            UserProfile userProfile = this.Manager.GetUserProfile(recID);
            if (userProfile != null)
            {
                return new Record(this, userProfile, null, userProfile.ID, recID);
            }
            return null;
        }

        public static ServerContext GetServerContext(SPWeb site)
        {
            string str;
            ServerContext context = null;
            if (site == null)
            {
                site = SPContext.Current.Web;
            }
            SPSecurity.CatchAccessDeniedException = false;
            site.Site.CatchAccessDeniedException = false;
            if (!ProductPage.Is14 && !string.IsNullOrEmpty(str = ProductPage.Config(null, "SspName")))
            {
                context =  ServerContext.GetContext(new SPSite(str));
            }
            if (((context == null) && ((context = ServerContext.GetContext(site.Site)) == null)) && ((context = ServerContext.Current) == null))
            {
                throw new Exception(ProductPage.GetResource("NoServerContext", new object[0]));
            }
            if (context.Status != SPObjectStatus.Online)
            {
                throw new Exception(ProductPage.GetResource("NoServerContextStatus", new object[] { context.Status }));
            }
            return context;
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            base.InitSchema(owner);
            this.InitSchemaForCaching(owner, true, false);
        }

        public override bool MoveNext()
        {
            this.curProf = null;
            return this.Enum.MoveNext();
        }

        public override void Reset()
        {
            this.curProf = null;
            this.Enum.Reset();
        }

        public IDbConnection AdoConnection
        {
            get
            {
                if (this.conn == null)
                {
                    this.conn = new SqlConnection(@"Data Source=ROXORITY\SHAREPOINT; Initial Catalog=User Profile Service Application_ProfileDB_8f84974dfdac4fd1bcab512fe4dd11ea; Integrated Security=SSPI");
                    this.conn.Open();
                }
                return this.conn;
            }
        }

        public bool AdoMode
        {
            get
            {
                return "s".Equals(base["m", string.Empty]);
            }
        }

        public override Guid ContextID
        {
            get
            {
                return appID;
            }
        }

        public override long Count
        {
            get
            {
                return this.Manager.Count;
            }
        }

        public override object Current
        {
            get
            {
                UserProfile current = this.Enum.Current as UserProfile;
                if ((this.curProf == null) && (current != null))
                {
                    this.curProf = new Record(this, current, null, current.ID, current.RecordId);
                }
                return this.curProf;
            }
        }

        public override RecordPropertyCollection Properties
        {
            get
            {
                if (this.propCol == null)
                {
                    this.propCol = new RecordPropertyCollection(this);
                    if (this.AdoMode)
                    {
                        Dictionary<string, Hashtable> dictionary = new Dictionary<string, Hashtable>();
                        using (IDbCommand command = this.AdoConnection.CreateCommand())
                        {
                            Hashtable hashtable;
                            command.CommandText = "SELECT PropertyID, PropertyName, DataTypeID, IsMultiValue FROM PropertyList";
                            using (IDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dictionary[reader["PropertyID"] + string.Empty] = hashtable = new Hashtable(reader.FieldCount + 2);
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        hashtable[reader.GetName(i)] = reader[i] + string.Empty;
                                    }
                                }
                            }
                            command.CommandText = "SELECT PropertyId, PropertyField, Lcid, Text FROM PropertyListLoc";
                            using (IDataReader reader2 = command.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    bool flag;
                                    string str;
                                    if (dictionary.TryGetValue(reader2["PropertyId"] + string.Empty, out hashtable) && ((flag = "2".Equals(str = reader2["PropertyField"] + string.Empty)) || "1".Equals(str)))
                                    {
                                        hashtable[(flag ? "d_" : "c_") + reader2["Lcid"]] = reader2["Text"] + string.Empty;
                                    }
                                }
                            }
                        }
                        foreach (KeyValuePair<string, Hashtable> pair in dictionary)
                        {
                            string name = pair.Value["PropertyName"] + string.Empty;
                            this.propCol.Props.Add(new RecordProperty(this, name, pair.Value));
                        }
                    }
                    else
                    {
                        foreach (Property property in this.Manager.Properties)
                        {
                            this.propCol.Props.Add(new RecordProperty(this, property.Name, property));
                        }
                    }
                    base.EnsureUserFields(this.propCol);
                    this.propCol.Props.Add(new RecordProperty(this, "roxUserPersonalUrl", null));
                    this.propCol.Props.Add(new RecordProperty(this, "roxUserPublicUrl", null));
                }
                return this.propCol;
            }
        }

        public override bool RequireCaching
        {
            get
            {
                return !this.AdoMode;
            }
        }

        public override string SchemaPropNamePrefix
        {
            get
            {
                return "up";
            }
        }
    }
}

