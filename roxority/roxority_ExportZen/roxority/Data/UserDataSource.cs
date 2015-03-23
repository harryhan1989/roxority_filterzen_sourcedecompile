namespace roxority.Data
{
    using Microsoft.SharePoint;
    using roxority.Data.Providers;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    public abstract class UserDataSource : DataSource
    {
        private string[] exclPatterns;
        public const string FIELDNAME_SITEGROUPS = "roxSiteGroups";
        public const string FIELDNAME_VCARDEXPORT = "roxVcardExport";
        public const string SCHEMAPROP_LOGINFIELD = "pl";
        public const string SCHEMAPROP_MAILFIELD = "pm";

        protected UserDataSource()
        {
        }

        protected void EnsureUserFields(RecordPropertyCollection props)
        {
            string str;
            string[] strArray;
            if ((props.GetPropertyByName("roxSiteGroups") == null) && !string.IsNullOrEmpty(base["pl", string.Empty]))
            {
                props.Props.Add(new RecordProperty(this, "roxSiteGroups", null));
            }
            if ((!string.IsNullOrEmpty(str = base["mf", string.Empty]) && ((strArray = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)) != null)) && (strArray.Length > 0))
            {
                foreach (string str2 in strArray)
                {
                    if (props.GetPropertyByName(str2) == null)
                    {
                        props.Props.Add(new RecordProperty(this, str2, null));
                    }
                }
            }
        }

        public override string FixupTitle(string name)
        {
            int num;
            if (this.ExcludeDomain && ((num = name.IndexOf('\\')) > 0))
            {
                name = name.Substring(num + 1);
            }
            return base.FixupTitle(name);
        }

        protected virtual string GetDefVal(string schemaProp)
        {
            string str = string.Empty;
            if (schemaProp == "pt")
            {
                return (((!(this is UserAccounts) || ProductPage.Is14) ? ("[" + this.RewritePropertyName("FirstName") + "] [" + this.RewritePropertyName("LastName") + "]\r\n") : string.Empty) + "[" + this.RewritePropertyName("PreferredName") + "]\r\n[" + this.RewritePropertyName("UserName") + "]\r\n[" + this.RewritePropertyName("AccountName") + "]");
            }
            if (schemaProp == "pu")
            {
                if (this is Directory)
                {
                    return string.Empty;
                }
                return "[roxUserPublicUrl]\r\n[roxUserPersonalUrl]";
            }
            if (schemaProp == "pd")
            {
                return (this.RewritePropertyName("Department") + ":" + ProductPage.GetResource("Department", new object[0]) + "\r\n" + this.RewritePropertyName("WorkEmail") + ":" + ProductPage.GetResource("WorkEmail", new object[0]));
            }
            if (schemaProp == "pm")
            {
                return ("[" + this.RewritePropertyName("WorkEmail") + "]");
            }
            if (schemaProp == "pl")
            {
                return ("[" + this.RewritePropertyName("AccountName") + "]");
            }
            if (schemaProp == "vc")
            {
                ProductPage.LicInfo info;
                return (((str + "N:" + ((!(this is UserAccounts) || ProductPage.Is14) ? ("[" + this.RewritePropertyName("LastName") + "];[" + this.RewritePropertyName("FirstName") + "]") : ("[" + this.RewritePropertyName("PreferredName") + "]")) + "\r\n") + "FN:[rox___pt]\r\nORG:" + ((((info = ProductPage.LicInfo.Get(null)) == null) || string.IsNullOrEmpty(info.name)) ? "ROXORITY Ltd." : info.name.Replace('[', '(').Replace(']', ')')) + "\r\nEMAIL:[rox___pm]\r\nPHOTO;VALUE=URL:[rox___pp]") + "\r\nTEL:[" + this.RewritePropertyName("CellPhone") + "]");
            }
            if (schemaProp == "pp")
            {
                return (((this is Directory) ? string.Empty : ("[" + this.RewritePropertyName("PictureURL") + "]\r\n")) + "/_layouts/images/" + ProductPage.AssemblyName + "/" + (ProductPage.Is14 ? "person" : "no_pic") + ".gif");
            }
            return string.Empty;
        }

        public Uri GetPersonalUrl(Record user)
        {
            SPUser mainItem = user.MainItem as SPUser;
            SPGroup group = user.MainItem as SPGroup;
            Uri uri = user.Tags["prvUri"] as Uri;
            if (uri == null)
            {
                if (mainItem != null)
                {
                    user.Tags["prvUri "] = uri = new Uri(string.Concat(new object[] { ProductPage.MergeUrlPaths(mainItem.ParentWeb.Url.Replace("http:", ((base.HttpContext != null) && base.HttpContext.Request.IsSecureConnection) ? "https:" : "http:"), "_layouts/userdisp.aspx"), "?Force=1&ID=", mainItem.ID, "&Source=", (base.HttpContext == null) ? string.Empty : HttpUtility.UrlEncode((base.HttpContext.Request.Url.ToString().ToLowerInvariant().Contains("_layouts/" + ProductPage.AssemblyName.ToLowerInvariant() + "/mash.tl.aspx?") ? base.HttpContext.Request.UrlReferrer : base.HttpContext.Request.Url).ToString()) }));
                    return uri;
                }
                if (group != null)
                {
                    user.Tags["prvUri "] = uri = new Uri(string.Concat(new object[] { ProductPage.MergeUrlPaths(group.ParentWeb.Url.Replace("http:", ((base.HttpContext != null) && base.HttpContext.Request.IsSecureConnection) ? "https:" : "http:"), "_layouts/userdisp.aspx"), "?Force=1&ID=", group.ID, "&Source=", (base.HttpContext == null) ? string.Empty : HttpUtility.UrlEncode((base.HttpContext.Request.Url.ToString().ToLowerInvariant().Contains("_layouts/" + ProductPage.AssemblyName.ToLowerInvariant() + "/mash.tl.aspx?") ? base.HttpContext.Request.UrlReferrer : base.HttpContext.Request.Url).ToString()) }));
                }
            }
            return uri;
        }

        public Uri GetPublicUrl(Record user)
        {
            SPUser mainItem = user.MainItem as SPUser;
            SPGroup group = user.MainItem as SPGroup;
            Uri uri = user.Tags["pubUri"] as Uri;
            if (uri == null)
            {
                if (mainItem != null)
                {
                    user.Tags["pubUri"] = uri = new Uri(string.Concat(new object[] { ProductPage.MergeUrlPaths(mainItem.ParentWeb.Url.Replace("http:", ((base.HttpContext != null) && base.HttpContext.Request.IsSecureConnection) ? "https:" : "http:"), "_layouts/userdisp.aspx"), "?ID=", mainItem.ID, "&Source=", (base.HttpContext == null) ? string.Empty : HttpUtility.UrlEncode((base.HttpContext.Request.Url.ToString().ToLowerInvariant().Contains("_layouts/" + ProductPage.AssemblyName.ToLowerInvariant() + "/mash.tl.aspx?") ? base.HttpContext.Request.UrlReferrer : base.HttpContext.Request.Url).ToString()) }));
                    return uri;
                }
                if (group != null)
                {
                    user.Tags["pubUri"] = uri = new Uri(string.Concat(new object[] { ProductPage.MergeUrlPaths(group.ParentWeb.Url.Replace("http:", ((base.HttpContext != null) && base.HttpContext.Request.IsSecureConnection) ? "https:" : "http:"), "_layouts/userdisp.aspx"), "?ID=", group.ID, "&Source=", (base.HttpContext == null) ? string.Empty : HttpUtility.UrlEncode((base.HttpContext.Request.Url.ToString().ToLowerInvariant().Contains("_layouts/" + ProductPage.AssemblyName.ToLowerInvariant() + "/mash.tl.aspx?") ? base.HttpContext.Request.UrlReferrer : base.HttpContext.Request.Url).ToString()) }));
                }
            }
            return uri;
        }

        public virtual string GetRecordVal(string prop, string val, string getProp)
        {
            DataSourceCache cache;
            CachedRecord record = null;
            if (DataSourceConsumer.dsCaches.TryGetValue(ProductPage.GuidLower(this.ContextID, false), out cache))
            {
                record = cache.recordCache.Find(cachedRec => (cachedRec != null) && val.Equals(cachedRec[prop, string.Empty, this], StringComparison.InvariantCultureIgnoreCase));
            }
            if (record != null)
            {
                return record[getProp, string.Empty, this];
            }
            return string.Empty;
        }

        public static string GetVcardExport(Record user)
        {
            return ProductPage.MergeUrlPaths(SPContext.Current.Web.Url, string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/mash.tl.aspx?op=vc&dsid=", user.DataSource.JsonInstance["id"], "&r=", user.RecordID, "&i=", ProductPage.GuidLower(user.ID, false), "&rr=", DataSource.rnd.Next(), "&fn=", HttpUtility.UrlEncode(user["rox___pl", ProductPage.AssemblyName].ToLowerInvariant() + "_" + user.RecordID), ".vcf" }));
        }

        public static string GetVcardExport(string dsid, int recID, Guid guid)
        {
            string str = "BEGIN:VCARD\r\nVERSION:3.0\r\n";
            List<string> list = new List<string>();
            Record record = null;
            using (DataSource source = DataSource.FromID(dsid, true, true, null))
            {
                string str2;
                if (recID > 0)
                {
                    record = source.GetRecord((long) recID);
                }
                if (!Guid.Empty.Equals(guid) && ((record == null) || (record.ID != guid)))
                {
                    foreach (Record record2 in source)
                    {
                        if (record2.ID == guid)
                        {
                            record = record2;
                            break;
                        }
                    }
                }
                if ((record != null) && !string.IsNullOrEmpty(str2 = source["vc", string.Empty].Trim(new char[] { '\r', '\n' })))
                {
                    foreach (string str5 in str2.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int num2;
                        int num = -1;
                        string str3 = str5;
                        list.Clear();
                        while ((num < (num = str5.IndexOf('[', num + 1))) && ((num2 = str5.IndexOf(']', num + 1)) > num))
                        {
                            list.Add(str5.Substring(num + 1, (num2 - num) - 1));
                        }
                        foreach (string str6 in list)
                        {
                            string message;
                            try
                            {
                                message = record[str6, string.Empty];
                            }
                            catch (Exception exception)
                            {
                                message = exception.Message;
                            }
                            if (((str6 == "rox___pp") && !message.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase)) && !message.StartsWith("https:", StringComparison.InvariantCultureIgnoreCase))
                            {
                                message = ProductPage.MergeUrlPaths(SPContext.Current.Web.Url, message);
                            }
                            str3 = str3.Replace("[" + str6 + "]", message);
                        }
                        str = str + str3 + "\r\n";
                    }
                }
            }
            return (str + "REV:" + DateTime.Now.ToString("yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture) + "\r\nEND:VCARD");
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            IDictionary pmore = new OrderedDictionary();
            base.InitSchema(owner);
            pmore["default"] = this.GetDefVal("pd");
            base.AddSchemaProp(owner, "pd", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("pt");
            base.AddSchemaProp(owner, "pt", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("pp");
            base.AddSchemaProp(owner, "pp", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("pu");
            base.AddSchemaProp(owner, "pu", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("pm");
            base.AddSchemaProp(owner, "pm", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("pl");
            base.AddSchemaProp(owner, "pl", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = this.GetDefVal("vc");
            pmore["default_if_empty"] = true;
            pmore["lines"] = 6;
            base.AddSchemaProp(owner, "vc", "String", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["lines"] = 6;
            base.AddSchemaProp(owner, "mf", "String", "w", true, pmore);
        }

        public bool ExcludeDomain
        {
            get
            {
                return base["ed", false];
            }
        }

        public string[] ExcludePatterns
        {
            get
            {
                if (this.exclPatterns == null)
                {
                    this.exclPatterns = (base["ex"] + string.Empty).Split(new char[] { '\r', '\n' });
                }
                if (this.exclPatterns.Length != 0)
                {
                    return this.exclPatterns;
                }
                return null;
            }
        }
    }
}

