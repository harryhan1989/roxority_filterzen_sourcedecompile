namespace roxority_RollupZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using roxority.Data;
    using roxority.Data.Providers;
    using roxority.Shared;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net.Mail;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    [Guid("6B7BE95D-059D-4c35-91EB-BF8823C1040C")]
    public class RollupWebPart : WebPartBase, IFilterConsumer
    {
        internal bool allowSort;
        internal bool allowView;
        internal List<string> andFilters = new List<string>();
        internal System.Web.UI.WebControls.WebParts.WebPart connectedWebPart;
        internal bool curUser;
        internal IDictionary dataInst;
        private roxority.Data.DataSource dataSource;
        internal string dataSourceID = "default";
        internal bool dateAgo = true;
        internal bool dateForth;
        internal bool dateIgnoreDay;
        internal double dateInterval = 56.0;
        internal string dateProp = string.Empty;
        internal bool dateThisYear;
        internal const string DEFAULT_PICTUREURL = "/_layouts/images/blank.gif";
        private string dsPath;
        internal IDictionary expInst;
        internal string exportAction = string.Empty;
        internal WebPartVerb exportVerb;
        private string ezPath;
        internal bool filterLive = true;
        internal List<object[]> filters = new List<object[]>();
        internal bool forceReload;
        internal static PropertyInfo fzHasHiddenProp = null;
        internal bool groupByCounts;
        internal bool groupDesc;
        internal bool groupInteractive;
        internal bool groupInteractiveDir;
        internal string groupProp = string.Empty;
        internal bool groupShowCounts;
        internal int imageHeight;
        internal bool isConnected;
        public static readonly bool IsPeopleZen = false;
        public static readonly bool IsRollupZen = false;
        [ThreadStatic]
        private static ProductPage.LicInfo l = null;
        internal bool listStyle;
        internal string loaderAnim = "k";
        internal string message = string.Empty;
        internal int nameMode = 1;
        private bool? noAjax = null;
        internal Dictionary<string, string> oobFilterPairs = new Dictionary<string, string>();
        internal int pageMode = 1;
        internal int pageSize = 6;
        internal int pageSkipMode;
        internal int pageStepMode = 1;
        internal int pictMode = (IsPeopleZen ? 1 : 0);
        internal bool presence;
        internal string printAction = string.Empty;
        internal IDictionary printInst;
        internal WebPartVerb printVerb;
        internal string props;
        private string pzPath;
        internal static readonly Random rnd = new Random();
        internal int rowSize = 2;
        internal bool sortDesc;
        internal List<Exception> sortErrors;
        internal string sortProp = string.Empty;
        private static string sspWebUrl = null;
        internal bool tabInteractive;
        internal string tabProp = string.Empty;
        public TextBox textArea = new TextBox();
        internal string tileWidth = "180px";
        internal bool vcard;
        private string webUrl;

        public event FilterConsumerInitEventHandler FilterConsumerInit;

        public RollupWebPart()
        {
            base.urlPropertyPrefix = "rollup_";
            this.ExportMode = WebPartExportMode.All;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.textArea.ID = "textArea";
            this.textArea.Wrap = false;
            this.textArea.Rows = 8;
            this.textArea.Style["display"] = "none";
            this.textArea.Style["width"] = "98%";
            this.textArea.TextMode = TextBoxMode.MultiLine;
            this.Controls.Add(this.textArea);
        }

        public override void EnsureInterfaces()
        {
            try
            {
                base.EnsureInterfaces();
                base.RegisterInterface("roxorityFilterConsumerInterface", "IFilterConsumer", 1, ConnectionRunAt.Server, this, string.Empty, base["Old_GetFiltersFrom", new object[0]], base["Old_GetFiltersFrom", new object[0]], false);
            }
            catch
            {
            }
        }

        public override InitEventArgs GetInitEventArgs(string InterfaceName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<string> list = new List<string>();
            FilterConsumerInitEventArgs args = new FilterConsumerInitEventArgs();
            if (this.DataSource != null)
            {
                foreach (RecordProperty property in this.dataSource.Properties)
                {
                    if (!list.Contains(property.Name.ToLowerInvariant()))
                    {
                        dictionary[property.Name] = property.DisplayName;
                        list.Add(property.Name.ToLowerInvariant());
                    }
                }
            }
            args.FieldList = new string[dictionary.Count];
            args.FieldDisplayList = new string[dictionary.Count];
            dictionary.Keys.CopyTo(args.FieldList, 0);
            dictionary.Values.CopyTo(args.FieldDisplayList, 0);
            return args;
        }

        internal static string GetPictureUrl(HttpContext context, roxority.Data.DataSource ds, CachedRecord cachedRecord, string siteUrl)
        {
            string str = ((L.expired || L.broken) || L.userBroken) ? (siteUrl + "/_layouts/images/" + ProductPage.AssemblyName + "/" + ProductPage.AssemblyName + ".png") : cachedRecord["rox___pp", siteUrl + "/_layouts/images/blank.gif", ds];
            if ((context.Request.IsSecureConnection && str.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase)) && str.StartsWith(siteUrl.TrimEnd(new char[] { '/' }) + '/', StringComparison.InvariantCultureIgnoreCase))
            {
                str = "https" + str.Substring(str.IndexOf(':'));
            }
            return str;
        }

        public WebPartVerb GetPrintVerb(IDictionary printAction)
        {
            object obj2;
            string str;
            Reflector reflector = null;
            WebPartVerb verb = null;
            try
            {
                reflector = new Reflector(Assembly.Load("roxority_PrintZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1"));
            }
            catch
            {
            }
            if (((reflector != null) && ((obj2 = reflector.New("roxority_PrintZen.PrintZenMenuItem", new object[0])) != null)) && !string.IsNullOrEmpty(str = reflector.Call(obj2, "GetRollupClickScript", new System.Type[] { typeof(IDictionary), typeof(string), typeof(Microsoft.SharePoint.WebPartPages.WebPart), typeof(List<object[]>), typeof(List<string>), typeof(Dictionary<string, string>) }, new object[] { printAction, this.Context.Request.RawUrl, this, this.filters, this.andFilters, this.oobFilterPairs }) as string))
            {
                int num = int.Parse(str.Substring(0, 1));
                verb = new WebPartVerb(this.ID + "_PrintVerb", str.Substring(1)) {
                    Description = (num == 0) ? "SharePoint-Tools.net/PrintZen" : this.Replace(printAction["desc"] + string.Empty, printAction, "PrintZen_PrintAction_", "PrintZen_QueryString_"),
                    Enabled = verb.Visible = true,
                    ImageUrl = this.WebUrl + "/_layouts/images/roxority_PrintZen/printer16.png",
                    Text = this.Replace(JsonSchemaManager.GetDisplayName(printAction, "PrintActions", false), printAction, "PrintZen_PrintAction_", "PrintZen_QueryString_")
                };
            }
            return verb;
        }

        internal static string GetProfileUrl(CachedRecord profile)
        {
            if ((!L.expired && !L.broken) && !L.userBroken)
            {
                return profile.Url;
            }
            return string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=lic&r=", rnd.Next() });
        }

        internal static string GetReloadScript(string opName, string textAreaID, string id, int pageSize, int pageStart, int pageMode, int pageStepMode, int pageSkipMode, bool dateThisYear, bool dateIgnoreDay, bool filterLive, string properties, bool listStyle, bool allowView, bool allowSort, string sortPropName, bool sortPropDesc, string tabPropName, string tabPropOrig, string tabValue, string groupPropName, bool groupPropDesc, bool groupByCounts, bool groupShowCounts, bool groupInt, bool groupIntDir, int rowSize, string tileWidth, int nameMode, int pictMode, bool presence, bool vcard, int imageHeight, string loaderAnim, bool tabInt, Hashtable fht, string dsid, object dynInst)
        {
            HttpContext current = HttpContext.Current;
            List<object[]> json = null;
            List<string> list2 = null;
            if (fht != null)
            {
                json = fht["f"] as List<object[]>;
                list2 = fht["fa"] as List<string>;
            }
            return string.Concat(new object[] { 
                opName, "('", textAreaID, "', '", id, "', { \"ps\": ", pageSize, ", \"p\": ", pageStart, ", \"la\": \"", loaderAnim, "\", \"pmo\": ", pageMode, ", \"pst\": ", pageStepMode, ", \"psk\": ", 
                pageSkipMode, ", \"dty\": \"", dateThisYear ? 1 : 0, "\", \"did\": \"", dateIgnoreDay ? 1 : 0, "\", \"dsid\": \"", SPEncode.ScriptEncode(dsid), "\", \"fl\": \"", filterLive ? 1 : 0, "\", \"pr\": \"", SPEncode.ScriptEncode(current.Server.UrlEncode(properties)), "\", \"ls\": \"", listStyle ? 1 : 0, "\", \"v\": \"", allowView ? 1 : 0, "\", \"s\": \"", 
                allowSort ? 1 : 0, "\", \"spn\": \"", SPEncode.ScriptEncode(sortPropName), "\", \"sd\": \"", sortPropDesc ? 1 : 0, "\", \"tpn\": \"", tabPropName, "\", \"tpo\": \"", tabPropOrig, "\", ", (tabValue == null) ? string.Empty : ("\"tv\": \"" + SPEncode.ScriptEncode(current.Server.UrlEncode(tabValue)) + "\", "), "\"gpn\": \"", groupPropName, "\", \"gd\": \"", groupPropDesc ? 1 : 0, "\", \"gb\": \"", 
                groupByCounts ? 1 : 0, "\", \"gs\": \"", groupShowCounts ? 1 : 0, "\", \"gi\": \"", groupInt ? 1 : 0, "\", \"ti\": \"", tabInt ? 1 : 0, "\", \"gid\": \"", groupIntDir ? 1 : 0, "\", \"rs\": ", rowSize, ", \"t\": \"", SPEncode.ScriptEncode(current.Server.UrlEncode(tileWidth)), "\", \"nm\": ", nameMode, ", \"pm\": ", 
                pictMode, ", \"on\": \"", presence ? 1 : 0, "\", \"vc\": \"", vcard ? 1 : 0, "\", \"ih\": ", imageHeight, ", \"f\": \"", SPEncode.ScriptEncode(current.Server.UrlEncode(JSON.JsonEncode(json))), "\", \"webUrl\": \"", SPEncode.ScriptEncode(SPContext.Current.Web.Url.TrimEnd(new char[] { '/' })), "\", \"fa\": \"", SPEncode.ScriptEncode(current.Server.UrlEncode(JSON.JsonEncode(list2))), "\", \"dyn\": ", (dynInst == null) ? "null" : ("\"" + SPEncode.ScriptEncode(current.Server.UrlEncode((dynInst is string) ? dynInst.ToString() : JSON.JsonEncode(dynInst))) + "\""), " });"
             });
        }

        public override ToolPart[] GetToolParts()
        {
            List<ToolPart> list = new List<ToolPart>(base.GetToolParts());
            if (!base.IsFrontPage)
            {
                list.Insert(0, new RollupToolPart());
            }
            return list.ToArray();
        }

        void IFilterConsumer.ClearFilter(object sender, EventArgs e)
        {
            this.oobFilterPairs.Clear();
        }

        void IFilterConsumer.NoFilter(object sender, EventArgs e)
        {
        }

        void IFilterConsumer.SetFilter(object sender, SetFilterEventArgs setFilterEventArgs)
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            if ((setFilterEventArgs != null) && !string.IsNullOrEmpty(setFilterEventArgs.FilterExpression))
            {
                foreach (string str in setFilterEventArgs.FilterExpression.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int index = str.IndexOf('=');
                    if (index > 0)
                    {
                        (str.Substring(0, index).StartsWith("FilterField") ? list : list2).Add(str.Substring(index + 1));
                    }
                }
            }
            if (list.Count <= list2.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.oobFilterPairs[list[i]] = list2[i];
                }
            }
        }

        protected virtual void OnFilterConsumerInit(FilterConsumerInitEventArgs e)
        {
            if (this.FilterConsumerInit != null)
            {
                this.FilterConsumerInit(this, e);
            }
        }

        public override void PartCommunicationConnect(string interfaceName, Microsoft.SharePoint.WebPartPages.WebPart connectedPart, string connectedInterfaceName, ConnectionRunAt runAt)
        {
            if (connectedPart != null)
            {
                this.connectedWebPart = connectedPart;
            }
            this.isConnected = true;
        }

        public override void PartCommunicationInit()
        {
            this.OnFilterConsumerInit((FilterConsumerInitEventArgs) this.GetInitEventArgs("roxorityFilterConsumerInterface"));
        }

        public override void PartCommunicationMain()
        {
            bool flag = false;
            IList list = null;
            PropertyInfo info = null;
            PropertyInfo property = null;
            PropertyInfo info3 = null;
            PropertyInfo info4 = null;
            PropertyInfo info5 = null;
            this.filters.Clear();
            this.andFilters.Clear();
            try
            {
                flag = (bool) this.ConnectedWebPart.GetType().GetMethod("LicEd", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this.ConnectedWebPart, new object[] { 2 });
            }
            catch
            {
                flag = false;
            }
            if (base.LicEd(2) && flag)
            {
                try
                {
                    list = this.ConnectedWebPart.GetType().GetProperty("PartFilters", BindingFlags.Public | BindingFlags.Instance).GetValue(this.ConnectedWebPart, null) as IList;
                    this.andFilters.AddRange(((string) this.ConnectedWebPart.GetType().GetProperty("CamlFiltersAndCombined", BindingFlags.Public | BindingFlags.Instance).GetValue(this.ConnectedWebPart, null)).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                }
                catch
                {
                }
            }
            if (((list == null) && (this.oobFilterPairs.Count > 0)) && (this.IsFilterOobConnection && base.LicEd(4)))
            {
                list = new List<KeyValuePair<string, string>>();
                foreach (KeyValuePair<string, string> pair in this.oobFilterPairs)
                {
                    list.Add(pair);
                }
            }
            if (list != null)
            {
                foreach (object obj3 in list)
                {
                    if (property == null)
                    {
                        property = obj3.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                    }
                    if (info == null)
                    {
                        info = obj3.GetType().GetProperty("Key", BindingFlags.Public | BindingFlags.Instance);
                    }
                    object obj2 = property.GetValue(obj3, null);
                    if (obj2 != null)
                    {
                        if (info3 == null)
                        {
                            info3 = obj2.GetType().GetProperty("Key", BindingFlags.Public | BindingFlags.Instance);
                        }
                        if (info4 == null)
                        {
                            info4 = obj2.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                        }
                        if (info5 == null)
                        {
                            info5 = obj2.GetType().GetProperty("CamlOperator", BindingFlags.Public | BindingFlags.Instance);
                        }
                        if (((info3 != null) && (info4 != null)) && (info5 != null))
                        {
                            this.filters.Add(new object[] { info3.GetValue(obj2, null), info4.GetValue(obj2, null), (CamlOperator) Enum.Parse(typeof(CamlOperator), info5.GetValue(obj2, null).ToString(), true) });
                        }
                        else if (info != null)
                        {
                            this.filters.Add(new object[] { info.GetValue(obj3, null), obj2, CamlOperator.Eq });
                        }
                    }
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string webUrl;
            int pageStart = 0;
            Hashtable fht = new Hashtable();
            NameValueCollection values = null;
            List<int> list = new List<int>(new int[] { 0x409 });
            SPContext context = ProductPage.GetContext();
            Guid empty = Guid.Empty;
            fht["f"] = this.Filters;
            fht["fa"] = this.CurUser ? new List<string>() : this.andFilters;
            this.EnsureChildControls();
            if (base.EffectiveJquery && !this.Page.Items.Contains("jquery"))
            {
                this.Page.Items["jquery"] = new object();
                writer.Write(string.Concat(new object[] { "<script language=\"JavaScript\" type=\"text/javascript\" src=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/jQuery.js?v=", ProductPage.Version, "\"></script>" }));
            }
            if (!this.Page.Items.Contains("roxority"))
            {
                this.Page.Items["roxority"] = new object();
                writer.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/roxority.tl.css?v=", ProductPage.Version, "\"/>" }));
                writer.Write(string.Concat(new object[] { "<script language=\"JavaScript\" type=\"text/javascript\" src=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/json2.tl.js?v=", ProductPage.Version, "\"></script>" }));
                writer.Write(string.Concat(new object[] { "<script language=\"JavaScript\" type=\"text/javascript\" src=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/roxority.tl.js?v=", ProductPage.Version, "\"></script>" }));
            }
            if (!this.Page.Items.Contains("roxrollupcss"))
            {
                this.Page.Items["roxrollupcss"] = new object();
                writer.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/RollupZen.tl.css?v=", ProductPage.Version, "\"/>" }));
                writer.Write(string.Concat(new object[] { "<script language=\"JavaScript\" type=\"text/javascript\" src=\"", this.WebUrl, "/_layouts/", ProductPage.AssemblyName, "/RollupZen.tl.js?v=", ProductPage.Version, "\"></script>" }));
                if (this.NoAjax)
                {
                    writer.Write("<script language=\"JavaScript\" type=\"text/javascript\"> roxRollNoAjax = true; </script>");
                }
                writer.Write("<script language=\"JavaScript\" type=\"text/javascript\"> roxEmbedMode = '" + ProductPage.Config(context, "EmbedFilters") + "' || 'merge'; " + (base.IsDesign ? "roxEditMode = true;" : string.Empty) + " </script>");
                if (ProductPage.Is14)
                {
                    writer.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + this.WebUrl + "/_layouts/1033/styles/Themable/layouts.css\"/>");
                    foreach (int num2 in ProductPage.WssInstalledCultures)
                    {
                        if (!list.Contains(num2))
                        {
                            list.Add(num2);
                            writer.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", this.WebUrl, "/_layouts/", num2, "/styles/Themable/layouts.css\"/>" }));
                        }
                    }
                    try
                    {
                        if ((context.Web.RegionalSettings != null) && !list.Contains((int) context.Web.RegionalSettings.LocaleId))
                        {
                            list.Add((int) context.Web.RegionalSettings.LocaleId);
                            writer.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", this.WebUrl, "/_layouts/", context.Web.RegionalSettings.LocaleId, "/styles/Themable/layouts.css\"/>" }));
                        }
                        else if ((context.Web.Locale != null) && !list.Contains(context.Web.Locale.LCID))
                        {
                            list.Add(context.Web.Locale.LCID);
                            writer.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", this.WebUrl, "/_layouts/", context.Web.Locale.LCID, "/styles/Themable/layouts.css\"/>" }));
                        }
                    }
                    catch
                    {
                    }
                }
            }
            writer.Write("<script language=\"JavaScript\" type=\"text/javascript\"> jQuery(document).ready(function() { roxRewriteFilterFunc(); " + (((this.ConnectedWebPart != null) && this.EffectiveFilterLive) ? ("roxRollupConns['" + this.ID + "'] = '" + this.ConnectedWebPart.ID + "'; ") : string.Empty) + GetReloadScript("roxRefreshRollup", this.textArea.ClientID, this.ID, this.PageSize, pageStart, this.PageMode, this.PageStepMode, this.PageSkipMode, this.DateThisYear, this.DateIgnoreDay, this.EffectiveFilterLive, this.Properties, this.ListStyle, this.AllowView, this.AllowSort, this.SortProp, this.SortDesc, this.TabProp, this.TabProp, null, this.GroupProp, this.GroupDesc, this.GroupByCounts, this.GroupShowCounts, this.GroupInteractive, this.GroupInteractiveDir, this.RowSize, this.TileWidth, this.NameMode, this.PictMode, this.Presence, this.Vcard, this.ImageHeight, this.LoaderAnim, this.TabInteractive, fht, this.DataSourceID, null) + " }); </script>");
            if (this.isConnected)
            {
                if ((this.ConnectedWebPart == null) && !base.LicEd(4))
                {
                    writer.Write("<div class=\"rox-error\">" + base["Old_NoFilterZen", new object[0]] + "</div>");
                }
                else if (this.ConnectedWebPart != null)
                {
                    if (!this.IsFilterOobConnection && !this.IsFilterZenConnection)
                    {
                        writer.Write("<div class=\"rox-error\">" + ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource(this.IsFilterZenConnection ? "Old_GetFiltersFrom_FilterZen" : "Old_GetFiltersFrom_Other", new object[0]), this.IsFilterZenConnection ? "Basic" : "Ultimate" }) + "</div>");
                    }
                    else if (!base.LicEd(this.IsFilterZenConnection ? 2 : 4))
                    {
                        writer.Write("<div class=\"rox-error\">" + ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource(this.IsFilterZenConnection ? "Old_GetFiltersFrom_FilterZen" : "Old_GetFiltersFrom_Other", new object[0]), this.IsFilterZenConnection ? "Basic" : "Ultimate" }) + "</div>");
                    }
                    else if (!base.LicEd(4) && !this.IsFilterZenConnection)
                    {
                        writer.Write("<div class=\"rox-error\">" + base["Old_NoFilterZen", new object[0]] + "</div>");
                    }
                    else if (this.IsFilterZenConnection && !((bool) this.ConnectedWebPart.GetType().GetMethod("LicEd", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this.ConnectedWebPart, new object[] { 2 })))
                    {
                        writer.Write("<div class=\"rox-error\">" + base["Old_NoFilterZenEnt", new object[0]] + "</div>");
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.message))
            {
                writer.Write("<div class=\"rox-error\">" + this.message + "</div>");
            }
            if (base.IsDesign && !base.EffectiveJquery)
            {
                writer.Write("<div class=\"rox-error\">" + base["JqueryNone", new object[0]] + "</div>");
            }
            writer.Write("<div class=\"rox-loader\" id=\"rox_loader_" + this.ID + "\" style=\"background-image: url('" + this.WebUrl + "/_layouts/images/" + ProductPage.AssemblyName + "/" + this.LoaderAnim + ".gif');\">&nbsp;</div>");
            writer.Write("<div class=\"rox-rollup\" id=\"rox_rollup_" + this.ID + "\"></div>");
            if (string.IsNullOrEmpty(this.Context.Request.QueryString["rpzopt"]) && (((string.IsNullOrEmpty(this.textArea.Text) || this.forceReload) && !ProductPage.Config<bool>(context, "AjaxFirst")) || ((string.IsNullOrEmpty(this.textArea.Text) || !this.textArea.Text.StartsWith("roxrollnoajax::")) && ((this.ConnectedWebPart != null) && !this.EffectiveFilterLive))))
            {
                using (StringWriter writer2 = new StringWriter())
                {
                    Render(this, writer2, this.textArea.ClientID, this.ID, this.PageSize, pageStart, this.PageMode, this.PageStepMode, this.PageSkipMode, this.DateThisYear, this.DateIgnoreDay, this.EffectiveFilterLive, this.Properties, this.ListStyle, this.AllowView, this.AllowSort, this.SortProp, this.SortDesc, this.TabProp, this.TabProp, null, this.GroupProp, this.GroupDesc, this.GroupByCounts, this.GroupShowCounts, this.GroupInteractive, this.GroupInteractiveDir, this.RowSize, this.TileWidth, this.NameMode, this.PictMode, this.Presence, this.Vcard, this.ImageHeight, this.LoaderAnim, this.TabInteractive, fht, base.Lic, this.DataSourceID, null);
                    writer2.Flush();
                    this.textArea.Text = writer2.GetStringBuilder().ToString();
                    goto Label_0FB2;
                }
            }
            if (this.textArea.Text.StartsWith("roxrollnoajax::") || !string.IsNullOrEmpty(this.Context.Request.QueryString["rpzopt"]))
            {
                if (this.textArea.Text.StartsWith("roxrollnoajax::"))
                {
                    values = HttpUtility.ParseQueryString(this.textArea.Text.Substring(this.textArea.Text.IndexOf('?') + 1));
                }
                else
                {
                    Hashtable hashtable2;
                    if ((!string.IsNullOrEmpty(this.Context.Request.QueryString["rpzopt"]) && ((hashtable2 = JSON.JsonDecode(this.Context.Request.QueryString["rpzopt"]) as Hashtable) != null)) && (hashtable2.Count > 0))
                    {
                        values = new NameValueCollection(hashtable2.Count);
                        foreach (DictionaryEntry entry in hashtable2)
                        {
                            string introduced37 = entry.Key + string.Empty;
                            values[introduced37] = entry.Value + string.Empty;
                        }
                    }
                }
                if (values != null)
                {
                    bool flag = Array.IndexOf<string>(values.AllKeys, "tv") >= 0;
                    fht["f"] = JSON.JsonDecode(values["f"]);
                    fht["fa"] = JSON.JsonDecode(values["fa"]);
                    IDictionary dynInst = JSON.JsonDecode(values["dyn"]) as IDictionary;
                    using (StringWriter writer3 = new StringWriter())
                    {
                        Render(this, writer3, string.IsNullOrEmpty(values["tid"]) ? this.textArea.ClientID : values["tid"], string.IsNullOrEmpty(values["id"]) ? this.ID : values["id"], int.Parse(values["ps"]), int.Parse(values["p"]), int.Parse(values["pmo"]), int.Parse(values["pst"]), int.Parse(values["psk"]), "1".Equals(values["dty"]), "1".Equals(values["did"]), "1".Equals(values["fl"]), values["pr"], "1".Equals(values["ls"]), "1".Equals(values["v"]), "1".Equals(values["s"]), values["spn"], "1".Equals(values["sd"]), values["tpn"], values["tpo"], flag ? values["tv"] : null, values["gpn"], "1".Equals(values["gd"]), "1".Equals(values["gb"]), "1".Equals(values["gs"]), "1".Equals(values["gi"]), "1".Equals(values["gid"]), int.Parse(values["rs"]), values["t"], int.Parse(values["nm"]), int.Parse(values["pm"]), "1".Equals(values["on"]), "1".Equals(values["vc"]), int.Parse(values["ih"]), values["la"], "1".Equals(values["ti"]), fht, null, values["dsid"], dynInst);
                        writer3.Flush();
                        this.textArea.Text = writer3.GetStringBuilder().ToString();
                    }
                }
            }
        Label_0FB2:
            if (string.IsNullOrEmpty(SspWebUrl) || !this.ServerContext)
            {
                webUrl = this.WebUrl;
            }
            else
            {
                webUrl = sspWebUrl;
            }
            if (this.DataSource != null)
            {
                empty = this.DataSource.ContextID;
            }
            else
            {
                try
                {
                    empty = UserAccounts.GetUserList(context.Web).ID;
                }
                catch
                {
                }
            }
            if (this.DataSource != null)
            {
                writer.Write("<script type=\"text/javascript\" language=\"JavaScript\">jQuery(document).ready(function() { jQuery('#roxpzproplink" + this.ID + "').attr({ \"href\": \"" + this.DataSource.GetFieldInfoUrl(webUrl, empty) + "\", \"target\": \"_blank\" }); });</script>");
            }
            if ((this.sortErrors != null) && (this.sortErrors.Count > 0))
            {
                foreach (Exception exception in this.sortErrors)
                {
                    writer.Write("<div class=\"rox-error\">Comparison error details: " + this.Context.Server.HtmlEncode(exception.ToString()).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") + "</div>");
                }
            }
            base.Render(writer);
        }

        public static void Render(roxority_RollupZen.RollupWebPart wp, TextWriter tw, string textAreaID, string id, int pageSize, int pageStart, int pageMode, int pageStepMode, int pageSkipMode, bool dateThisYear, bool dateIgnoreDay, bool filterLive, string properties, bool listStyle, bool allowView, bool allowSort, string sortPropName, bool sortDesc, string tabPropName, string tabPropOrig, string tabValue, string groupPropName, bool groupDesc, bool groupByCounts, bool groupShowCounts, bool groupInt, bool groupIntDir, int rowSize, string tileWidth, int nameMode, int pictMode, bool presence, bool vcard, int imageHeight, string loaderAnim, bool tabInt, Hashtable fht, object l, string dsid, IDictionary dynInst)
        {
            SPSecurity.CodeToRunElevated elevated2 = null;
            int thisTab;
            bool isEmpty;
            bool isDate;
            char theChar;
            string[] pair;
            string propName;
            string picUrl;
            string profUrl;
            string picProfUrl;
            string temp;
            DateTime dtVal;
            SPContext current = SPContext.Current;
            Guid siteID = current.Site.ID;
            Guid webID = current.Web.ID;
            int pc = 0;
            int lastTab = -1;
            bool isPaging = false;
            string nameCaption = ProductPage.Config(current, "AltNameCaption");
            string linkTarget = ProductPage.Config(current, "LinkTarget");
            string trClass = "ms-alternating";
            string str = null;
            string domain = string.Empty;
            string str3 = null;
            string str4 = null;
            string webUrl = string.Empty;
            StringBuilder buffer = new StringBuilder();
            StringBuilder navBuffer = new StringBuilder();
            roxority.Data.DataSource ds = (wp == null) ? null : wp.DataSource;
            Converter<int, string> getPagingScript = null;
            Converter<string, string> getRegroupScript = null;
            Converter<string, string> getRetabScript = null;
            Converter<string, string> getSortScript = null;
            Converter<string, string> getTabScript = null;
            Converter<bool, string> getGroupScript = null;
            Converter<bool, string> getViewScript = null;
            List<string> friendlyProperties = new List<string>();
            HttpContext context = HttpContext.Current;
            SortedDictionary<string, string> knownProps = null;
            List<Exception> sortErrors = new List<Exception>();
            try
            {
                webUrl = current.Web.Url.TrimEnd(new char[] { '/' });
            }
            catch
            {
            }
            if (!ProductPage.isEnabled)
            {
                using (SPSite site = ProductPage.GetAdminSite())
                {
                    tw.Write("<div class=\"rox-error\">" + ProductPage.GetResource("NotEnabled", new object[] { ProductPage.MergeUrlPaths(site.Url, string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=enable&r=", rnd.Next() })), ProductPage.GetTitle() }) + "</div>");
                }
            }
            else
            {
                if (!(l is ProductPage.LicInfo))
                {
                    l = L;
                }
                if (fht != null)
                {
                    if (fht["f"] is ArrayList)
                    {
                        List<object[]> list = new List<object[]>();
                        foreach (object obj2 in (ArrayList) fht["f"])
                        {
                            list.Add((obj2 is ArrayList) ? ((ArrayList) obj2).ToArray() : ((object[]) obj2));
                        }
                        fht["f"] = list;
                    }
                    if (fht["fa"] is ArrayList)
                    {
                        fht["fa"] = new List<string>(((ArrayList) fht["fa"]).ToArray(typeof(string)) as string[]);
                    }
                }
                if (!ProductPage.LicEdition(current, L, 2))
                {
                    presence = listStyle = false;
                    sortPropName = groupPropName = tabValue = tabPropOrig = tabPropName = string.Empty;
                }
                if (!ProductPage.LicEdition(current, L, 4))
                {
                    vcard = groupByCounts = groupShowCounts = groupInt = groupIntDir = tabInt = allowView = false;
                }
                try
                {
                    int num;
                    if (ds == null)
                    {
                        ds = roxority.Data.DataSource.FromID(dsid, true, true, (dynInst == null) ? null : (dynInst["t"] as string));
                    }
                    if ((wp != null) && (wp.dataSource == null))
                    {
                        wp.dataSource = ds;
                    }
                    if (elevated2 == null)
                    {
                        elevated2 = delegate {
                            int num2 = 0;
                            int num3 = 0;
                            string str11 = null;
                            string str31 = string.Empty;
                            string str41 = null;
                            string str5 = string.Empty;
                            SPContext context1 = ProductPage.GetContext();
                            buffer.Remove(0, buffer.Length);
                            if (allowSort && string.IsNullOrEmpty(sortPropName))
                            {
                                sortPropName = null;
                            }
                            if (string.IsNullOrEmpty(groupPropName))
                            {
                                groupPropName = null;
                            }
                            if (string.IsNullOrEmpty(tabPropName))
                            {
                                tabPropName = string.IsNullOrEmpty(tabPropOrig) ? null : tabPropOrig;
                            }
                            getRegroupScript = groupProp => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, pageStart, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, sortPropName, sortDesc, tabPropName, tabPropOrig, tabValue, groupProp, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getRetabScript = tabProp => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, 0, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, sortPropName, sortDesc, tabProp, tabPropOrig, null, groupPropName, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getGroupScript = desc => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, pageStart, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, sortPropName, sortDesc, tabPropName, tabPropOrig, tabValue, groupPropName, desc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getPagingScript = ps => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, ps, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, sortPropName, sortDesc, tabPropName, tabPropOrig, tabValue, groupPropName, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getSortScript = spn => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, pageStart, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, spn.Substring(1), spn[0] == '-', tabPropName, tabPropOrig, tabValue, groupPropName, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getTabScript = tv => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, 0, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, listStyle, allowView, allowSort, sortPropName, sortDesc, tabPropName, tabPropOrig, tv, groupPropName, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            getViewScript = ls => GetReloadScript("roxReloadRollup", textAreaID, id, pageSize, pageStart, pageMode, pageStepMode, pageSkipMode, dateThisYear, dateIgnoreDay, filterLive, properties, ls, allowView, allowSort, sortPropName, sortDesc, tabPropName, tabPropOrig, tabValue, groupPropName, groupDesc, groupByCounts, groupShowCounts, groupInt, groupIntDir, rowSize, tileWidth, nameMode, pictMode, presence, vcard, imageHeight, loaderAnim, tabInt, fht, dsid, dynInst);
                            using (StringWriter writer = new StringWriter(buffer))
                            {
                                using (SPSite site = new SPSite(siteID))
                                {
                                    using (SPWeb web = site.OpenWeb(webID))
                                    {
                                        using (DataSourceConsumer consumer = new DataSourceConsumer(pageSize, pageStart, dateThisYear, dateIgnoreDay, properties, sortPropName, string.IsNullOrEmpty(sortPropName) ? null : ((object) sortDesc), tabPropName, tabValue, groupPropName, string.IsNullOrEmpty(groupPropName) ? null : ((object) groupDesc), groupByCounts, groupShowCounts, web, dsid, dynInst, fht, l, sortErrors))
                                        {
                                            if (ds == null)
                                            {
                                                ds = consumer.DataSource;
                                            }
                                            if (wp != null)
                                            {
                                                wp.dataSource = consumer.DataSource;
                                            }
                                            knownProps = new SortedDictionary<string, string>();
                                            foreach (RecordProperty property in consumer.DataSource.Properties)
                                            {
                                                knownProps[property.Name] = property.DisplayName;
                                            }
                                            foreach (KeyValuePair<string, string> pair1 in knownProps)
                                            {
                                                if ((((pair1.Key == tabPropName) || (pair1.Key == tabPropOrig)) || (pair1.Key == groupPropName)) && !friendlyProperties.Contains(pair1.Key))
                                                {
                                                    friendlyProperties.Add(pair1.Key);
                                                }
                                                if (groupInt)
                                                {
                                                    string str14 = str5;
                                                    str5 = str14 + "<option value=\"" + context.Server.HtmlEncode(pair1.Key) + "\"" + (pair1.Key.Equals(groupPropName) ? " selected=\"selected\"" : string.Empty) + ">" + context.Server.HtmlEncode(pair1.Value) + "</option>";
                                                }
                                            }
                                            foreach (string str7 in properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                                            {
                                                if ((((pair = str7.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (pair.Length >= 1)) && (!friendlyProperties.Contains(pair[0]) && !friendlyProperties.Contains(consumer.DataSource.RewritePropertyName(pair[0]))))
                                                {
                                                    friendlyProperties.Add(pair[0]);
                                                }
                                            }
                                            writer.Write("<div class=\"rox-rollupitems\">");
                                            if (consumer.tabs != null)
                                            {
                                                writer.Write("<div class=\"rox-tabstrip\">");
                                                if (tabInt && !string.IsNullOrEmpty(tabPropName))
                                                {
                                                    writer.Write("<div class=\"rox-tabprop\"><select onchange=\"" + HttpUtility.HtmlAttributeEncode(getRetabScript("\" + this.options[this.selectedIndex].value + \"")) + "\">");
                                                    foreach (string str8 in friendlyProperties)
                                                    {
                                                        writer.Write("<option value=\"" + context.Server.HtmlEncode(str8) + "\"" + (str8.Equals(tabPropName) ? " selected=\"selected\"" : string.Empty) + ">" + context.Server.HtmlEncode((knownProps.ContainsKey(str8) ? knownProps[str8] : str8) + (str8.Equals(tabPropName) ? ":" : string.Empty)) + "</option>");
                                                    }
                                                    writer.Write("</select></div>");
                                                }
                                                if (ProductPage.Config<bool>(null, "FilterTabShowAll"))
                                                {
                                                    writer.Write("<div class=\"ms-templatepicker" + ((consumer.tabValue == null) ? string.Empty : "un") + "selected\"><div><a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getTabScript(null)) + "\">" + Res("All", new object[0]) + "</a></div></div>");
                                                }
                                                foreach (string str9 in consumer.tabs)
                                                {
                                                    if (!string.IsNullOrEmpty(str9))
                                                    {
                                                        if (consumer.tabsReduced)
                                                        {
                                                            if (lastTab < 0)
                                                            {
                                                                lastTab = (int.TryParse(str9, out lastTab) ? 0x30 : 0x41) - 1;
                                                            }
                                                            if ((((thisTab = str9[0]) - lastTab) > 1) && (lastTab >= 0))
                                                            {
                                                                for (int n = lastTab + 1; n < thisTab; n++)
                                                                {
                                                                    if (char.IsLetterOrDigit(theChar = (char) n))
                                                                    {
                                                                        writer.Write("<div class=\"ms-templatepickerunselected ms-templatepickerdisabled\"><div><span>" + theChar + "</span></div></div>");
                                                                    }
                                                                }
                                                            }
                                                            lastTab = thisTab;
                                                        }
                                                        writer.Write("<div class=\"ms-templatepicker" + (((((consumer.tabValue == null) && (consumer.tabs.IndexOf(str9) == 0)) && !ProductPage.Config<bool>(null, "FilterTabShowAll")) || (consumer.tabValue == str9)) ? string.Empty : "un") + "selected\"><div><a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getTabScript(str9)) + "\">" + (string.IsNullOrEmpty(str9) ? "&mdash;" : context.Server.HtmlEncode(str9)) + "</a></div></div>");
                                                    }
                                                }
                                                if (lastTab > 0)
                                                {
                                                    char ch1 = (char) lastTab;
                                                    if (int.TryParse(ch1.ToString(), out thisTab))
                                                    {
                                                        if (thisTab < 9)
                                                        {
                                                            for (int i = thisTab + 1; i <= 9; i++)
                                                            {
                                                                writer.Write("<div class=\"ms-templatepickerunselected ms-templatepickerdisabled\"><div><span>" + i + "</span></div></div>");
                                                            }
                                                        }
                                                    }
                                                    else if (lastTab < 90)
                                                    {
                                                        for (int j = lastTab + 1; j <= 90; j++)
                                                        {
                                                            if (char.IsLetterOrDigit(theChar = (char) j))
                                                            {
                                                                writer.Write("<div class=\"ms-templatepickerunselected ms-templatepickerdisabled\"><div><span>" + theChar + "</span></div></div>");
                                                            }
                                                        }
                                                    }
                                                }
                                                if ((consumer.tabs.Count > 0) && string.Empty.Equals(consumer.tabs[consumer.tabs.Count - 1]))
                                                {
                                                    writer.Write("<div class=\"ms-templatepicker" + (string.Empty.Equals(consumer.tabValue) ? string.Empty : "un") + "selected\"><div><a href=\"" + Noop + "\" class=\"rollajaxlnk\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getTabScript(string.Empty)) + "\">&mdash;</a></div></div>");
                                                }
                                                writer.Write("<div class=\"rox-tabstrip-fill\">&nbsp;</div></div>");
                                            }
                                            if (((isPaging = (((pageSize > 0) && (consumer.List.Count > 0)) && ((pageStart > 0) || ((pageStart + pageSize) < consumer.recCount))) && (((pageMode > 0) || (pageStepMode > 0)) || (pageSkipMode > 0))) || (consumer.List.Count == 0)) || allowView)
                                            {
                                                using (StringWriter writer2 = new StringWriter(navBuffer))
                                                {
                                                    writer2.Write("<div id=\"rox_pager_" + id + "\" class=\"rox-rollup-paging" + (allowView ? " rox-rollup-switcher" : string.Empty) + ((consumer.tabs == null) ? string.Empty : " rox-rollup-hastabs") + "\">");
                                                    if (allowView)
                                                    {
                                                        writer2.Write(listStyle ? ("<span style=\"float: right;\">" + Res("ShowAs", new object[0]) + " <a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getViewScript(false)) + "\">" + Res("StyleClassicCaption", new object[0]) + "</a>&nbsp;") : ("<span style=\"float: right;\">" + Res("ShowAs", new object[0]) + "<b>" + Res("StyleClassicCaption", new object[0]) + "</b>&nbsp;"));
                                                        writer2.Write(listStyle ? ("&nbsp;<b>" + Res("StyleListCaption", new object[0]) + "</b></span>") : ("&nbsp;<a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getViewScript(true)) + "\">" + Res("StyleListCaption", new object[0]) + "</a></span>"));
                                                    }
                                                    if (consumer.List.Count == 0)
                                                    {
                                                        writer2.Write("<span class=\"rox-pz-zero rox-rz-zero\">" + Res("ZeroRecs" + (IsRollupZen ? "R" : string.Empty), new object[0]) + "</span>");
                                                    }
                                                    else if (isPaging)
                                                    {
                                                        int num12 = (consumer.recCount / pageSize) + (((consumer.recCount % pageSize) == 0) ? 0 : 1);
                                                        if (pageStart > 0)
                                                        {
                                                            if (pageSkipMode == 1)
                                                            {
                                                                writer2.Write("<a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\">" + Res("First", new object[0]) + "</a>&nbsp;&nbsp;" + ((pageStepMode == 0) ? "|&nbsp;" : "&nbsp;"), HttpUtility.HtmlAttributeEncode(getPagingScript(0)));
                                                            }
                                                            else if (pageSkipMode == 2)
                                                            {
                                                                writer2.Write("<a class=\"rox-pagestep rox-pageskip rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\" style=\"background-image: url('" + webUrl + "/_layouts/images/marrrtl.gif'); padding-left: 1px;\">|&nbsp;&nbsp;</a>&nbsp;", HttpUtility.HtmlAttributeEncode(getPagingScript(0)));
                                                            }
                                                            if (pageStepMode == 1)
                                                            {
                                                                writer2.Write("<a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\">" + Res("Prev", new object[0]) + "</a>&nbsp;&nbsp;|&nbsp;", HttpUtility.HtmlAttributeEncode(getPagingScript(pageStart - pageSize)));
                                                            }
                                                            else if (pageStepMode == 2)
                                                            {
                                                                writer2.Write("<a class=\"rox-pagestep rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\" style=\"background-image: url('" + webUrl + "/_layouts/images/marrrtl.gif');\">&nbsp;</a>&nbsp;", HttpUtility.HtmlAttributeEncode(getPagingScript(pageStart - pageSize)));
                                                            }
                                                        }
                                                        if (pageMode == 1)
                                                        {
                                                            writer2.Write("&nbsp;" + Res("xtoyofz", new object[] { pageStart + 1, ((pageStart + pageSize) > consumer.recCount) ? consumer.recCount : (pageStart + pageSize), consumer.recCount }) + "&nbsp;");
                                                        }
                                                        else if (pageMode == 2)
                                                        {
                                                            writer2.Write("&nbsp;" + Res("xofy", new object[] { (pageStart / pageSize) + 1, num12 }) + "&nbsp;");
                                                        }
                                                        else if (pageMode == 3)
                                                        {
                                                            for (int k = 0; k < num12; k++)
                                                            {
                                                                if (pageStart == (k * pageSize))
                                                                {
                                                                    if (num2 > 0)
                                                                    {
                                                                        writer2.Write("&hellip;");
                                                                    }
                                                                    num2 = 0;
                                                                    writer2.Write("<b class=\"rox-pageno\">" + (k + 1) + "</b>");
                                                                }
                                                                else if ((((k == 0) || (k == 1)) || ((k == (num12 - 1)) || (k == (num12 - 2)))) || ((k == ((pageStart / pageSize) - 1)) || (k == ((pageStart / pageSize) + 1))))
                                                                {
                                                                    if (num2 > 0)
                                                                    {
                                                                        writer2.Write("&hellip;");
                                                                    }
                                                                    num2 = 0;
                                                                    writer2.Write(string.Concat(new object[] { "<a class=\"rox-pageno rollajaxlnk\" href=\"", Noop, "\" onclick=\"{0}\">", k + 1, "</a>" }), HttpUtility.HtmlAttributeEncode(getPagingScript(k * pageSize)));
                                                                }
                                                                else
                                                                {
                                                                    num2++;
                                                                }
                                                            }
                                                        }
                                                        if ((pageStart + pageSize) < consumer.recCount)
                                                        {
                                                            if (pageStepMode == 1)
                                                            {
                                                                writer2.Write("&nbsp;|&nbsp;&nbsp;<a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\">" + Res("Next", new object[0]) + "</a>", HttpUtility.HtmlAttributeEncode(getPagingScript(pageStart + pageSize)));
                                                            }
                                                            else if (pageStepMode == 2)
                                                            {
                                                                writer2.Write("&nbsp;<a class=\"rox-pagestep rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\" style=\"background-image: url('" + webUrl + "/_layouts/images/marr.gif');\">&nbsp;</a>", HttpUtility.HtmlAttributeEncode(getPagingScript(pageStart + pageSize)));
                                                            }
                                                            if (pageSkipMode == 1)
                                                            {
                                                                writer2.Write(((pageStepMode == 0) ? "&nbsp;|&nbsp;" : "&nbsp;&nbsp;") + "&nbsp;<a class=\"rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\">" + Res("Last", new object[0]) + "</a>", HttpUtility.HtmlAttributeEncode(getPagingScript((num12 - 1) * pageSize)));
                                                            }
                                                            else if (pageSkipMode == 2)
                                                            {
                                                                writer2.Write("&nbsp;<a class=\"rox-pagestep rox-pageskip rollajaxlnk\" href=\"" + Noop + "\" onclick=\"{0}\" style=\"background-image: url('" + webUrl + "/_layouts/images/marr.gif');\">&nbsp;&nbsp;|</a>", HttpUtility.HtmlAttributeEncode(getPagingScript((num12 - 1) * pageSize)));
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        writer2.Write("&nbsp;");
                                                    }
                                                    writer2.Write("</div>");
                                                }
                                            }
                                            if (ShowNavTop)
                                            {
                                                writer.Write("<div class=\"rox-rollup-topnav\">" + navBuffer.ToString(0, navBuffer.Length) + "</div>");
                                            }
                                            if (!string.IsNullOrEmpty(groupPropName))
                                            {
                                                foreach (string str10 in properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    if ((((pair = str10.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (pair.Length >= 1)) && pair[0].Trim().Equals(groupPropName, StringComparison.InvariantCultureIgnoreCase))
                                                    {
                                                        if (((pair.Length < 2) || "___".Equals(pair[1].Trim(), StringComparison.InvariantCultureIgnoreCase)) || string.IsNullOrEmpty(pair[1].Trim()))
                                                        {
                                                            str41 = string.Empty;
                                                        }
                                                        else
                                                        {
                                                            str41 = pair[1].Trim();
                                                        }
                                                    }
                                                }
                                                if ((str41 == null) && knownProps.ContainsKey(groupPropName))
                                                {
                                                    str41 = knownProps[groupPropName];
                                                }
                                            }
                                            if (consumer.List.Count > 0)
                                            {
                                                string str2;
                                                string str6;
                                                MailAddress address;
                                                if (listStyle)
                                                {
                                                    writer.Write("<table class=\"rox-rollupitems\" width=\"99%\" border=\"0\" cellSpacing=\"0\" cellPadding=\"0\"><tr><td><table width=\"100%\" class=\"ms-summarystandardbody\" border=\"0\" cellSpacing=\"0\" cellPadding=\"1\" summary=\"Tasks\">");
                                                    writer.Write("<tr class=\"ms-viewheadertr\" vAlign=\"top\">");
                                                    num3 = 0;
                                                    foreach (string str111 in (((pictMode > 0) ? "_rox_Picture:___\r\n" : string.Empty) + ((nameMode == 0) ? string.Empty : ("_rox_Name:" + nameCaption + "\r\n")) + properties).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                                                    {
                                                        if ((((pair = str111.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (pair.Length >= 1)) && ((pair.Length < 2) || !string.IsNullOrEmpty(pair[1].Trim())))
                                                        {
                                                            if (pair.Length < 2)
                                                            {
                                                                pair = new string[] { pair[0], consumer.DataSource.GetPropertyDisplayName(pair[0]) };
                                                            }
                                                            propName = pair[0].Trim();
                                                            writer.Write("<th class=\"ms-vh2\" noWrap=\"nowrap\"><div style=\"position: relative; width: 100%; top: 0px; left: 0px;\"><table height=\"100%\" class=\"ms-unselectedtitle\" style=\"width: 100%;\" cellSpacing=\"1\" cellPadding=\"0\"><tr><td width=\"100%\" class=\"ms-vb\" noWrap=\"nowrap\">" + ("___".Equals(pair[1].Trim()) ? string.Empty : (allowSort ? ("<a title=\"" + context.Server.HtmlEncode(Res("SortBy", new object[] { pair[1] })) + "\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getSortScript(((((sortPropName == null) && propName.Equals("_rox_Name")) || propName.Equals(sortPropName)) && !sortDesc) ? ('-' + propName) : ('+' + propName))) + "\">" + context.Server.HtmlEncode(pair[1].Trim()) + "</a><img src=\"" + webUrl + "/_layouts/images/" + (propName.Equals(sortPropName) ? (sortDesc ? "rsort" : "sort") : "blank") + ".gif\" border=\"0\" />") : context.Server.HtmlEncode(pair[1].Trim()))) + "</td><td style=\"position: absolute;\"><img width=\"13\" style=\"visibility: hidden;\" src=\"" + webUrl + "/_layouts/images/blank.gif\" /></td></tr></table></div></th>");
                                                        }
                                                        num3++;
                                                        if (!ProductPage.LicEdition(context1, l as ProductPage.LicInfo, 2) && (num3 >= ((2 + ((pictMode > 0) ? 1 : 0)) + ((nameMode > 0) ? 1 : 0))))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    writer.Write("</tr>");
                                                    foreach (CachedRecord record in consumer.List)
                                                    {
                                                        string[] strArray3;
                                                        profUrl = (nameMode == 1) ? GetProfileUrl(record) : string.Empty;
                                                        picProfUrl = (pictMode == 1) ? GetProfileUrl(record) : string.Empty;
                                                        if (!string.IsNullOrEmpty(groupPropName) && ((str2 = record[groupPropName, null, consumer.DataSource]) != str11))
                                                        {
                                                            str11 = str2;
                                                            writer.Write("<tr><td id=\"" + (str31 = "g" + ProductPage.GuidLower(Guid.NewGuid()).Replace('-', '_')) + "\" class=\"ms-gb\" nowrap=\"nowrap\" colspan=\"100\" onmouseover=\"jQuery(this).addClass('ms-gbhover" + (groupInt ? string.Empty : "x") + "');jQuery('a.rox-rollgroupdirlink').hide();jQuery('select.rox-rollgroupprefix').hide();jQuery('span.rox-rollgroupprefix').show();roxNoMouseOut=false;jQuery('#dir_" + str31 + "').show();jQuery('#grp_" + str31 + "').show();jQuery('#pref_" + str31 + "').hide();\" onmouseout=\"if(!roxNoMouseOut){jQuery(this).removeClass('ms-gbhover');jQuery('a.rox-rollgroupdirlink').hide();jQuery('select.rox-rollgroupprefix').hide();jQuery('span.rox-rollgroupprefix').show();}\">" + (!string.IsNullOrEmpty(str41) ? ("<span id=\"pref_" + (groupInt ? str31 : string.Empty) + "\" class=\"rox-rollgroupprefix\">" + context.Server.HtmlEncode(str41) + ":</span> ") : string.Empty) + (groupInt ? ("<select onchange=\"" + HttpUtility.HtmlAttributeEncode(getRegroupScript("\" + this.options[this.selectedIndex].value + \"")) + "\" onfocus=\"roxNoMouseOut=true;\" onblur=\"roxNoMouseOut=false;\" onchange=\"\" class=\"rox-rollgroupprefix\" id=\"grp_" + str31 + "\" style=\"display: none;\">" + str5 + "</select> ") : string.Empty) + "<span class=\"rox-rollgroup\">" + (string.IsNullOrEmpty(str2) ? "&mdash;" : context.Server.HtmlEncode(str2)) + "</span>" + (groupShowCounts ? (" <span class=\"rox-rollgroupcount\">(" + consumer.groupCounts[str2 + string.Empty] + ")</span>") : string.Empty) + (groupIntDir ? (" <a class=\"rox-rollgroupdirlink rollajaxlnk\" id=\"dir_" + str31 + "\" style=\"display: none;\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getGroupScript(!groupDesc)) + "\"><img src=\"" + webUrl + "/_layouts/images/" + (groupDesc ? "rsort" : "sort") + ".gif\" border=\"0\" align=\"baseline\" /></a>") : string.Empty) + "</td></tr>");
                                                        }
                                                        writer.Write("<tr class=\"" + (trClass = string.IsNullOrEmpty(trClass) ? "ms-alternating" : string.Empty) + "\">");
                                                        if (pictMode > 0)
                                                        {
                                                            writer.Write("<td width=\"1%\" class=\"ms-vb2\"><div class=\"rox-rollupitem-picture\"><" + (string.IsNullOrEmpty(picProfUrl) ? "span" : "a") + (((string.IsNullOrEmpty(picUrl = GetPictureUrl(context, consumer.DataSource, record, site.Url)) || picUrl.ToLowerInvariant().Contains("/_layouts/images/blank.gif".ToLowerInvariant())) || (picUrl.ToLowerInvariant().Contains("person.gif") || picUrl.ToLowerInvariant().Contains("no_pic"))) ? " style=\"background: none !important; border: 0px none transparent !important; padding: 3px !important;\" " : string.Empty) + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? string.Concat(strArray3 = new string[5]) : string.Empty) + " href=\"" + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? Noop : picProfUrl) + "\" target=\"" + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? "_self" : linkTarget) + "\"\"><img border=\"0\" onerror=\"roxImageError(this,'" + webUrl + "/_layouts/images/blank.gif');\" src=\"" + picUrl + "\" title=\"" + HttpUtility.HtmlAttributeEncode(DataSourceConsumer.GetTitle(consumer, record)) + "\" " + ((imageHeight == 0) ? string.Empty : ("style=\"height: " + imageHeight + "px;\" ")) + "/></" + (string.IsNullOrEmpty(picProfUrl) ? "span" : "a") + "></div></td>");
                                                        }
                                                        if (nameMode > 0)
                                                        {
                                                            writer.Write("<td nowrap=\"nowrap\" class=\"ms-vb2 rox-rollupitem-fullname\"><" + (string.IsNullOrEmpty(profUrl) ? "b" : "a") + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? string.Concat(strArray3 = new string[5]) : string.Empty) + " href=\"" + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? Noop : profUrl) + "\" target=\"" + (((linkTarget == "_modal") || (linkTarget == "_popup")) ? "_self" : linkTarget) + "\">" + DataSourceConsumer.GetTitle(consumer, record) + "</" + (string.IsNullOrEmpty(profUrl) ? "b" : "a") + ">");
                                                            if (presence && !string.IsNullOrEmpty(str6 = record["rox___pm", string.Empty, consumer.DataSource]))
                                                            {
                                                                writer.Write("<span style=\"padding: 0px 5px 0px 5px;\"><img border=\"0\" height=\"12\" width=\"12\" src=\"" + webUrl + "/_layouts/images/imnunk.png\" onload=\"IMNRC('" + SPEncode.ScriptEncode(str6) + "')\" name=\"imnmark\" id=\"IMID" + ProductPage.GuidLower(Guid.NewGuid()) + "\" ShowOfflinePawn=\"1\"/></span>");
                                                            }
                                                            if (vcard && !string.IsNullOrEmpty(str6 = record["roxVcardExport", string.Empty, consumer.DataSource]))
                                                            {
                                                                writer.Write(str6);
                                                            }
                                                            writer.Write("</td>");
                                                        }
                                                        num3 = 0;
                                                        foreach (string str12 in properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                                                        {
                                                            if ((((pair = str12.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (pair.Length >= 1)) && ((pair.Length < 2) || !string.IsNullOrEmpty(pair[1].Trim())))
                                                            {
                                                                writer.Write("<td class=\"ms-vb2\">");
                                                                writer.Write((temp = ((isDate = DateTime.TryParse(record[propName = pair[0].Trim(), string.Empty, consumer.DataSource], out dtVal)) && propName.Equals("SPS-Birthday", StringComparison.InvariantCultureIgnoreCase)) ? dtVal.ToString("m", ProductPage.GetFarmCulture(context1)) : record[propName, string.Empty, consumer.DataSource]).ToLowerInvariant().Contains("<div>") ? temp : (isDate ? ("<nobr>" + context.Server.HtmlEncode(temp) + "</nobr>") : (((address = ProductPage.GetEmailAddress(temp)) != null) ? string.Format(context.Server.HtmlDecode(ProductPage.Config(context1, "MailPropFormat")), context.Server.HtmlEncode(address.Address), '{', '}') : (("roxVcardExport".Equals(propName) || temp.StartsWith("<roxhtml/>")) ? temp : context.Server.HtmlEncode(temp)))));
                                                                writer.Write("</td>");
                                                            }
                                                            num3++;
                                                            if (!ProductPage.LicEdition(context1, l as ProductPage.LicInfo, 2) && (num3 >= 2))
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        writer.Write("</tr>");
                                                    }
                                                    writer.Write("</table></td></tr></table>");
                                                }
                                                else
                                                {
                                                    writer.Write("<div class=\"rox-rollupitems-all\">");
                                                    if (rowSize > 0)
                                                    {
                                                        writer.WriteLine("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                                                    }
                                                    foreach (CachedRecord record2 in consumer.List)
                                                    {
                                                        string[] strArray;
                                                        profUrl = (nameMode == 1) ? GetProfileUrl(record2) : string.Empty;
                                                        picProfUrl = (pictMode == 1) ? GetProfileUrl(record2) : string.Empty;
                                                        if (((rowSize > 0) && !string.IsNullOrEmpty(groupPropName)) && ((str2 = record2[groupPropName, null, consumer.DataSource]) != str11))
                                                        {
                                                            str11 = str2;
                                                            writer.Write("<tr><td id=\"" + (str31 = "g" + ProductPage.GuidLower(Guid.NewGuid()).Replace('-', '_')) + "\" class=\"ms-gb\" nowrap=\"nowrap\" colspan=\"100\" onmouseover=\"jQuery(this).addClass('ms-gbhover" + (groupInt ? string.Empty : "x") + "');jQuery('a.rox-rollgroupdirlink').hide();jQuery('select.rox-rollgroupprefix').hide();jQuery('span.rox-rollgroupprefix').show();roxNoMouseOut=false;jQuery('#dir_" + str31 + "').show();jQuery('#grp_" + str31 + "').show();jQuery('#pref_" + str31 + "').hide();\" onmouseout=\"if(!roxNoMouseOut){jQuery(this).removeClass('ms-gbhover');jQuery('a.rox-rollgroupdirlink').hide();jQuery('select.rox-rollgroupprefix').hide();jQuery('span.rox-rollgroupprefix').show();}\">" + (!string.IsNullOrEmpty(str41) ? ("<span id=\"pref_" + (groupInt ? str31 : string.Empty) + "\" class=\"rox-rollgroupprefix\">" + context.Server.HtmlEncode(str41) + ":</span> ") : string.Empty) + (groupInt ? ("<select onchange=\"" + HttpUtility.HtmlAttributeEncode(getRegroupScript("\" + this.options[this.selectedIndex].value + \"")) + "\" onfocus=\"roxNoMouseOut=true;\" onblur=\"roxNoMouseOut=false;\" onchange=\"\" class=\"rox-rollgroupprefix\" id=\"grp_" + str31 + "\" style=\"display: none;\">" + str5 + "</select> ") : string.Empty) + "<span class=\"rox-rollgroup\">" + (string.IsNullOrEmpty(str2) ? "&mdash;" : context.Server.HtmlEncode(str2)) + "</span>" + (groupShowCounts ? (" <span class=\"rox-rollgroupcount\">(" + consumer.groupCounts[str2 + string.Empty] + ")</span>") : string.Empty) + (groupIntDir ? (" <a class=\"rox-rollgroupdirlink rollajaxlnk\" id=\"dir_" + str31 + "\" style=\"display: none;\" href=\"" + Noop + "\" onclick=\"" + HttpUtility.HtmlAttributeEncode(getGroupScript(!groupDesc)) + "\"><img src=\"" + webUrl + "/_layouts/images/" + (groupDesc ? "rsort" : "sort") + ".gif\" border=\"0\" align=\"baseline\" /></a>") : string.Empty) + "</td></tr>");
                                                        }
                                                        if ((rowSize > 0) && ((pc == 0) || ((pc % rowSize) == 0)))
                                                        {
                                                            writer.WriteLine("<tr>");
                                                        }
                                                        if (rowSize > 0)
                                                        {
                                                            writer.WriteLine("<td valign=\"top\">");
                                                        }
                                                        writer.WriteLine("\r\n<div class=\"rox-rollupitem\" style=\"width: " + tileWidth + ";\">" + ((pictMode > 0) ? ("\r\n\t<div class=\"rox-rollupitem-picture\">\r\n\t\t<{10} style=\"{3}\" href=\"{6}\" target=\"{4}\"{8}><img alt=\"{1}\" border=\"0\" onerror=\"roxImageError(this,'" + webUrl + "/_layouts/images/blank.gif');\" src=\"{0}\" title=\"{1}\" " + ((imageHeight == 0) ? string.Empty : ("style=\"height: " + imageHeight + "px;\" ")) + "/></{10}>\r\n\t</div>\r\n") : string.Empty) + ((nameMode <= 0) ? string.Empty : "\r\n\t<div class=\"rox-rollupitem-fullname\">\r\n\t\t{9}\r\n\t\t<{5} href=\"{2}\" target=\"{4}\" class=\"rox-rollup-item\"{7}>{1}</{5}>\r\n\t\t{11}\r\n\t</div>"), new object[] { picUrl = GetPictureUrl(context, consumer.DataSource, record2, site.Url), HttpUtility.HtmlEncode(DataSourceConsumer.GetTitle(consumer, record2)), ((linkTarget == "_modal") || (linkTarget == "_popup")) ? Noop : profUrl, ((string.IsNullOrEmpty(picUrl) || picUrl.ToLowerInvariant().Contains("/_layouts/images/blank.gif".ToLowerInvariant())) || (picUrl.ToLowerInvariant().Contains("person.gif") || picUrl.ToLowerInvariant().Contains("no_pic"))) ? "background: none !important; border: 0px none transparent !important; padding: 3px !important;" : string.Empty, ((linkTarget == "_modal") || (linkTarget == "_popup")) ? "_self" : linkTarget, string.IsNullOrEmpty(profUrl) ? "span" : "a", ((linkTarget == "_modal") || (linkTarget == "_popup")) ? Noop : picProfUrl, ((linkTarget == "_modal") || (linkTarget == "_popup")) ? string.Concat(strArray = new string[5]) : string.Empty, ((linkTarget == "_modal") || (linkTarget == "_popup")) ? string.Concat(strArray = new string[5]) : string.Empty, (presence && !string.IsNullOrEmpty(str6 = record2["rox___pm", string.Empty, consumer.DataSource])) ? ("<span style=\"padding: 0px 5px 0px 5px;\"><img border=\"0\" height=\"12\" width=\"12\" src=\"" + webUrl + "/_layouts/images/imnunk.png\" onload=\"IMNRC('" + SPEncode.ScriptEncode(str6) + "');\" name=\"imnmark\" id=\"IMID" + ProductPage.GuidLower(Guid.NewGuid()) + "\" ShowOfflinePawn=\"1\"/></span>") : string.Empty, string.IsNullOrEmpty(picProfUrl) ? "span" : "a", vcard ? record2["roxVcardExport", string.Empty, consumer.DataSource] : string.Empty });
                                                        num3 = 0;
                                                        foreach (string str13 in properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                                                        {
                                                            if ((((((pair = str13.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (pair.Length >= 1)) && !string.IsNullOrEmpty(propName = pair[0].Trim())) && ((pair.Length < 2) || !string.IsNullOrEmpty(pair[1].Trim()))) && (!(isEmpty = string.IsNullOrEmpty(record2[propName, string.Empty, consumer.DataSource])) || !ProductPage.Config<bool>(context1, "SkipUnknownProps")))
                                                            {
                                                                try
                                                                {
                                                                    if (pair.Length < 2)
                                                                    {
                                                                        pair = new string[] { pair[0], consumer.DataSource.GetPropertyDisplayName(pair[0]) };
                                                                    }
                                                                    writer.WriteLine("<div class=\"rox-rollupitem-{0} rox-rollup-item\">{1}</div>", propName.ToLowerInvariant(), string.Format(context.Server.HtmlDecode(ProductPage.Config(context1, (isEmpty ? "Unknown" : "Known") + "PropFormat")), new object[] { context.Server.HtmlEncode(pair[1].Trim()), isEmpty ? Res("Unknown", new object[0]) : ((propName.Equals("SPS-Birthday", StringComparison.InvariantCultureIgnoreCase) && DateTime.TryParse(record2[propName, string.Empty, consumer.DataSource], out dtVal)) ? dtVal.ToString("m", ProductPage.GetFarmCulture(context1)) : (((address = ProductPage.GetEmailAddress(record2[propName, string.Empty, consumer.DataSource])) != null) ? string.Format(context.Server.HtmlDecode(ProductPage.Config(context1, "MailPropFormat")), context.Server.HtmlEncode(address.Address), '{', '}') : (((temp = record2[propName, string.Empty, consumer.DataSource]).StartsWith("<roxhtml/>") || "roxVcardExport".Equals(propName)) ? temp : context.Server.HtmlEncode(temp)))), propName, '{', '}' }).Replace("___: ", string.Empty).Replace("___ ", string.Empty));
                                                                }
                                                                catch (Exception exception)
                                                                {
                                                                    writer.WriteLine("<div style=\"background-color: #ff9999;\" class=\"rox-rollupitem-{0} rox-rollup-item\">{1}</div>", propName.ToLowerInvariant(), context.Server.HtmlEncode(exception.Message));
                                                                }
                                                            }
                                                            num3++;
                                                            if (!ProductPage.LicEdition(context1, l as ProductPage.LicInfo, 2) && (num3 >= 2))
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        writer.Write("</div>");
                                                        if (rowSize > 0)
                                                        {
                                                            writer.WriteLine("</td>");
                                                        }
                                                        pc++;
                                                        if ((rowSize > 0) && ((pc % rowSize) == 0))
                                                        {
                                                            writer.WriteLine("</tr>");
                                                        }
                                                    }
                                                    if (rowSize > 0)
                                                    {
                                                        writer.WriteLine("</table>");
                                                    }
                                                    writer.Write("</div>");
                                                }
                                            }
                                            if (ShowNavBottom && (navBuffer.Length > 0))
                                            {
                                                writer.Write("<div class=\"rox-rollup-bottomnav\">");
                                                writer.Write(navBuffer.ToString());
                                                writer.Write("</div>");
                                            }
                                            writer.Write("<div style=\"clear: both;\"></div></div>");
                                            writer.Flush();
                                        }
                                    }
                                }
                            }
                        };
                    }
                    SPSecurity.CodeToRunElevated code = elevated2;
                    if ((ds != null) && (ds.inst != null))
                    {
                        str = ds.inst["s"] as string;
                    }
                    if ((dynInst != null) && dynInst.Contains("s"))
                    {
                        str = dynInst["s"] + string.Empty;
                    }
                    if ((ds != null) && (ds.inst != null))
                    {
                        str3 = ds.inst["su"] as string;
                    }
                    if ((dynInst != null) && dynInst.Contains("su"))
                    {
                        str3 = dynInst["su"] + string.Empty;
                    }
                    if ((ds != null) && (ds.inst != null))
                    {
                        str4 = ds.inst["sp"] as string;
                    }
                    if ((dynInst != null) && dynInst.Contains("sp"))
                    {
                        str4 = dynInst["sp"] + string.Empty;
                    }
                    if (!string.IsNullOrEmpty(str3) && ((num = str3.IndexOf('\\')) > 0))
                    {
                        domain = str3.Substring(0, num);
                        str3 = str3.Substring(num + 1);
                    }
                    if ((string.IsNullOrEmpty(str) || (str == "b")) || (str == "e"))
                    {
                        ProductPage.Elevate(code, str != "e");
                    }
                    else
                    {
                        if (((str == "i") && !string.IsNullOrEmpty(str3)) && !string.IsNullOrEmpty(str4))
                        {
                            using (new SPElevator(domain, str3, str4))
                            {
                                code();
                                goto Label_07A4;
                            }
                        }
                        code();
                    }
                Label_07A4:
                    tw.Write(buffer.ToString());
                }
                catch (Exception innerException)
                {
                    if ((innerException is TargetInvocationException) && (innerException.InnerException != null))
                    {
                        innerException = innerException.InnerException;
                    }
                    tw.Write("<div class=\"rox-error\">" + HttpUtility.HtmlEncode(innerException.ToString()).Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;") + "</div>");
                }
                finally
                {
                    if ((wp != null) && (wp.sortErrors == null))
                    {
                        wp.sortErrors = sortErrors;
                    }
                }
            }
        }

        internal string Replace(string v, IDictionary inst, string pa, string pq)
        {
            int num;
            int num2;
            while (((num = v.IndexOf("{$", StringComparison.InvariantCultureIgnoreCase)) >= 0) && ((num2 = v.IndexOf("$}", num + 2, StringComparison.InvariantCultureIgnoreCase)) > num))
            {
                string effectiveTitle;
                string oldValue = v.Substring(num, (num2 - num) + 2);
                string str2 = oldValue.Substring(2, oldValue.Length - 4);
                if (str2.StartsWith(pa, StringComparison.InvariantCultureIgnoreCase))
                {
                    effectiveTitle = inst[str2.Substring(pa.Length)] + string.Empty;
                }
                else if (str2.StartsWith(pq, StringComparison.InvariantCultureIgnoreCase))
                {
                    effectiveTitle = this.Context.Request.QueryString[str2.Substring(pa.Length)] + string.Empty;
                }
                else
                {
                    effectiveTitle = base.EffectiveTitle;
                }
                v = v.Replace(oldValue, effectiveTitle);
            }
            return v;
        }

        internal static string Res(string name, params object[] args)
        {
            return ProductPage.GetProductResource(name, args);
        }

        [Personalizable]
        public bool AllowSort
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("AllowSort", this.allowSort);
                }
                return false;
            }
            set
            {
                this.allowSort = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public bool AllowView
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("AllowView", this.allowView);
                }
                return false;
            }
            set
            {
                this.allowView = base.LicEd(4) && value;
            }
        }

        public System.Web.UI.WebControls.WebParts.WebPart ConnectedWebPart
        {
            get
            {
                SPWebPartManager webPartManager = base.WebPartManager as SPWebPartManager;
                if (((this.connectedWebPart == null) && (webPartManager != null)) && ((webPartManager.SPWebPartConnections != null) && (webPartManager.SPWebPartConnections.Count > 0)))
                {
                    foreach (SPWebPartConnection connection in webPartManager.SPWebPartConnections)
                    {
                        if ((connection.Consumer == this) && ((this.connectedWebPart = connection.Provider) != null))
                        {
                            break;
                        }
                    }
                }
                return this.connectedWebPart;
            }
        }

        [Personalizable]
        public bool CurUser
        {
            get
            {
                return base.GetProp<bool>("CurUser", this.curUser);
            }
            set
            {
                this.curUser = value;
            }
        }

        public roxority.Data.DataSource DataSource
        {
            get
            {
                if ((this.dataSource == null) && !string.IsNullOrEmpty(this.DataSourceID))
                {
                    try
                    {
                        this.dataSource = roxority.Data.DataSource.FromID(this.DataSourceID, true, true, null);
                    }
                    catch
                    {
                    }
                }
                return this.dataSource;
            }
        }

        [Personalizable]
        public string DataSourceID
        {
            get
            {
                return this.dataSourceID;
            }
            set
            {
                this.dataSourceID = value;
            }
        }

        internal IDictionary DataSourceInst
        {
            get
            {
                if (((this.dataInst == null) && !string.IsNullOrEmpty(this.DataSourceID)) && !string.IsNullOrEmpty(this.DataSourcePath))
                {
                    foreach (IDictionary dictionary in JsonSchemaManager.GetInstances(this.DataSourcePath, "DataSource"))
                    {
                        if (this.DataSourceID.Equals(dictionary["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.dataInst = dictionary;
                            break;
                        }
                    }
                }
                return this.dataInst;
            }
        }

        internal string DataSourcePath
        {
            get
            {
                if (this.dsPath == null)
                {
                    this.dsPath = string.Empty;
                    try
                    {
                        this.dsPath = this.Context.Server.MapPath("/_layouts/" + ProductPage.AssemblyName + "/schemas.tl.json");
                    }
                    catch
                    {
                    }
                    if (!string.IsNullOrEmpty(this.dsPath) && !File.Exists(this.dsPath))
                    {
                        this.dsPath = string.Empty;
                    }
                }
                return this.dsPath;
            }
        }

        [Personalizable]
        public bool DateIgnoreDay
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("DateIgnoreDay", this.dateIgnoreDay);
                }
                return false;
            }
            set
            {
                this.dateIgnoreDay = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public bool DateThisYear
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("DateThisYear", this.dateThisYear);
                }
                return false;
            }
            set
            {
                this.dateThisYear = base.LicEd(2) && value;
            }
        }

        public bool EffectiveFilterLive
        {
            get
            {
                if (!this.NoAjax)
                {
                    if (!base.LicEd(2) || !this.IsFilterZenConnection)
                    {
                        return false;
                    }
                    if (((this.ConnectedWebPart != null) && ((fzHasHiddenProp != null) || ((fzHasHiddenProp = this.ConnectedWebPart.GetType().GetProperty("HasHiddenFilter", BindingFlags.Public | BindingFlags.Instance)) != null))) && ((bool) fzHasHiddenProp.GetValue(this.ConnectedWebPart, null)))
                    {
                        return false;
                    }
                    if (!this.Context.Request.UserAgent.Contains("Gecko/") && !this.Context.Request.UserAgent.Contains("Firefox"))
                    {
                        return base.GetProp<bool>("FilterLive", this.filterLive);
                    }
                }
                return false;
            }
        }

        internal IDictionary ExpInst
        {
            get
            {
                if (((this.expInst == null) && !string.IsNullOrEmpty(this.ExportAction)) && !string.IsNullOrEmpty(this.EzPath))
                {
                    foreach (IDictionary dictionary in JsonSchemaManager.GetInstances(this.EzPath, "ExportActions", "roxority_ExportZen"))
                    {
                        if (this.ExportAction.Equals(dictionary["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.expInst = dictionary;
                            break;
                        }
                    }
                }
                return this.expInst;
            }
        }

        [Personalizable]
        public string ExportAction
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return string.Empty;
                }
                return this.exportAction;
            }
            set
            {
                this.exportAction = base.LicEd(2) ? value : string.Empty;
            }
        }

        public WebPartVerb ExportVerb
        {
            get
            {
                Reflector reflector = null;
                if ((this.exportVerb == null) && (this.ExpInst != null))
                {
                    object obj2;
                    string str;
                    try
                    {
                        reflector = new Reflector(Assembly.Load("roxority_ExportZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1"));
                    }
                    catch
                    {
                    }
                    if (((reflector != null) && ((obj2 = reflector.New("roxority_ExportZen.ExportZenMenuItem", new object[0])) != null)) && !string.IsNullOrEmpty(str = reflector.Call(obj2, "GetRollupClickScript", new System.Type[] { typeof(IDictionary), typeof(string), typeof(Microsoft.SharePoint.WebPartPages.WebPart), typeof(List<object[]>), typeof(List<string>), typeof(Dictionary<string, string>) }, new object[] { this.expInst, this.Context.Request.RawUrl, this, this.filters, this.andFilters, this.oobFilterPairs }) as string))
                    {
                        int num = int.Parse(str.Substring(0, 1));
                        this.exportVerb = new WebPartVerb(this.ID + "_ExportVerb", str.Substring(1));
                        this.exportVerb.Description = (num == 0) ? "SharePoint-Tools.net/ExportZen" : this.Replace(this.expInst["desc"] + string.Empty, this.expInst, "ExportZen_ExportAction_", "ExportZen_QueryString_");
                        this.exportVerb.Enabled = this.exportVerb.Visible = true;
                        this.exportVerb.ImageUrl = this.WebUrl + "/_layouts/images/roxority_ExportZen/icon16.png";
                        this.exportVerb.Text = this.Replace(JsonSchemaManager.GetDisplayName(this.expInst, "ExportActions", false), this.expInst, "ExportZen_ExportAction_", "ExportZen_QueryString_");
                    }
                }
                return this.exportVerb;
            }
        }

        internal string EzPath
        {
            get
            {
                if (this.ezPath == null)
                {
                    this.ezPath = string.Empty;
                    try
                    {
                        this.ezPath = this.Context.Server.MapPath("/_layouts/roxority_ExportZen/schemas.json");
                    }
                    catch
                    {
                    }
                    if (!string.IsNullOrEmpty(this.ezPath) && !File.Exists(this.ezPath))
                    {
                        this.ezPath = string.Empty;
                    }
                }
                return this.ezPath;
            }
        }

        [Personalizable]
        public bool FilterLive
        {
            get
            {
                return (base.LicEd(2) && this.filterLive);
            }
            set
            {
                this.filterLive = base.LicEd(2) && value;
            }
        }

        public List<object[]> Filters
        {
            get
            {
                return this.filters;
            }
        }

        [Personalizable]
        public bool GroupByCounts
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("GroupByCounts", this.groupByCounts);
                }
                return false;
            }
            set
            {
                this.groupByCounts = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public bool GroupDesc
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("GroupDesc", this.groupDesc);
                }
                return false;
            }
            set
            {
                this.groupDesc = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public bool GroupInteractive
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("GroupInteractive", this.groupInteractive);
                }
                return false;
            }
            set
            {
                this.groupInteractive = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public bool GroupInteractiveDir
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("GroupInteractiveDir", this.groupInteractiveDir);
                }
                return false;
            }
            set
            {
                this.groupInteractiveDir = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public string GroupProp
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return string.Empty;
                }
                return base.GetProp<string>("GroupProp", this.groupProp);
            }
            set
            {
                this.groupProp = base.LicEd(2) ? value : string.Empty;
            }
        }

        [Personalizable]
        public bool GroupShowCounts
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("GroupShowCounts", this.groupShowCounts);
                }
                return false;
            }
            set
            {
                this.groupShowCounts = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public int ImageHeight
        {
            get
            {
                return base.GetProp<int>("ImageHeight", this.imageHeight);
            }
            set
            {
                this.imageHeight = value;
            }
        }

        public bool IsB
        {
            get
            {
                return ProductPage.LicEdition(ProductPage.GetContext(), L, 2);
            }
        }

        public bool IsFilterOobConnection
        {
            get
            {
                object connectedWebPart = this.ConnectedWebPart;
                return (((connectedWebPart != null) && connectedWebPart.GetType().FullName.StartsWith("Microsoft.SharePoint.")) && connectedWebPart.GetType().Assembly.FullName.StartsWith("Microsoft."));
            }
        }

        public bool IsFilterZenConnection
        {
            get
            {
                object connectedWebPart = this.ConnectedWebPart;
                return ((connectedWebPart != null) && (connectedWebPart.GetType().AssemblyQualifiedName == "roxority_FilterZen.roxority_FilterWebPart, roxority_FilterZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1"));
            }
        }

        private static ProductPage.LicInfo L
        {
            get
            {
                if (l == null)
                {
                    l = ProductPage.LicInfo.Get(null);
                }
                return l;
            }
        }

        [Personalizable]
        public bool ListStyle
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("ListStyle", this.listStyle);
                }
                return false;
            }
            set
            {
                this.listStyle = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public string LoaderAnim
        {
            get
            {
                return base.GetProp<string>("LoaderAnim", this.loaderAnim);
            }
            set
            {
                value = (value + string.Empty).ToLowerInvariant().Trim();
                this.loaderAnim = ((value == "b") || (value == "l")) ? value : "k";
            }
        }

        [Personalizable]
        public int NameMode
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return 1;
                }
                return base.GetProp<int>("NameMode", this.nameMode);
            }
            set
            {
                this.nameMode = base.LicEd(2) ? value : 1;
            }
        }

        public bool NoAjax
        {
            get
            {
                if (!this.noAjax.HasValue || !this.noAjax.HasValue)
                {
                    this.noAjax = new bool?(ProductPage.Config<bool>(ProductPage.GetContext(), "NoAjax"));
                }
                return this.noAjax.Value;
            }
        }

        internal static string Noop
        {
            get
            {
                return ("#noop" + rnd.Next(0, 0x3b9ac9ff));
            }
        }

        [Personalizable]
        public int PageMode
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return 1;
                }
                return base.GetProp<int>("PageMode", this.pageMode);
            }
            set
            {
                this.pageMode = base.LicEd(4) ? value : 1;
            }
        }

        [Personalizable]
        public int PageSize
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return 4;
                }
                return base.GetProp<int>("PageSize", this.pageSize);
            }
            set
            {
                this.pageSize = base.LicEd(2) ? ((value < 1) ? 0 : value) : 4;
            }
        }

        [Personalizable]
        public int PageSkipMode
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return 0;
                }
                return base.GetProp<int>("PageSkipMode", this.pageSkipMode);
            }
            set
            {
                this.pageSkipMode = base.LicEd(4) ? value : 0;
            }
        }

        [Personalizable]
        public int PageStepMode
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return 1;
                }
                return base.GetProp<int>("PageStepMode", this.pageStepMode);
            }
            set
            {
                this.pageStepMode = base.LicEd(4) ? value : 1;
            }
        }

        [Personalizable]
        public int PictMode
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return 1;
                }
                return base.GetProp<int>("PictMode", this.pictMode);
            }
            set
            {
                this.pictMode = base.LicEd(2) ? value : 1;
            }
        }

        [Personalizable]
        public bool Presence
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("Presence", this.presence);
                }
                return false;
            }
            set
            {
                this.presence = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public string PrintAction
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return string.Empty;
                }
                return this.printAction;
            }
            set
            {
                this.printAction = base.LicEd(2) ? value : string.Empty;
            }
        }

        internal IDictionary PrintInst
        {
            get
            {
                if (((this.printInst == null) && !string.IsNullOrEmpty(this.PrintAction)) && !string.IsNullOrEmpty(this.PzPath))
                {
                    foreach (IDictionary dictionary in JsonSchemaManager.GetInstances(this.PzPath, "PrintActions", "roxority_PrintZen"))
                    {
                        if (this.PrintAction.Equals(dictionary["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.printInst = dictionary;
                            break;
                        }
                    }
                }
                return this.printInst;
            }
        }

        public WebPartVerb PrintVerb
        {
            get
            {
                if (((this.printVerb == null) && (this.PrintInst != null)) && !"n".Equals(this.printInst["mpz"]))
                {
                    this.printVerb = this.GetPrintVerb(this.PrintInst);
                }
                return this.printVerb;
            }
        }

        [Personalizable]
        public string Properties
        {
            get
            {
                if ((this.props == null) && (this.DataSource != null))
                {
                    this.props = this.dataSource["pd", string.Empty];
                }
                if (this.props == null)
                {
                    this.props = string.Empty;
                }
                return this.props;
            }
            set
            {
                this.props = (value + string.Empty).Trim().Trim(new char[] { '\r', '\n' });
            }
        }

        internal string PzPath
        {
            get
            {
                if (this.pzPath == null)
                {
                    this.pzPath = string.Empty;
                    try
                    {
                        this.pzPath = this.Context.Server.MapPath("/_layouts/roxority_PrintZen/schemas.json");
                    }
                    catch
                    {
                    }
                    if (!string.IsNullOrEmpty(this.pzPath) && !File.Exists(this.pzPath))
                    {
                        this.pzPath = string.Empty;
                    }
                }
                return this.pzPath;
            }
        }

        [Personalizable]
        public int RowSize
        {
            get
            {
                return base.GetProp<int>("RowSize", this.rowSize);
            }
            set
            {
                this.rowSize = (value < 1) ? 0 : value;
            }
        }

        public bool ServerContext
        {
            get
            {
                return false;
            }
        }

        public static bool ShowNavBottom
        {
            get
            {
                string str = ProductPage.Config(ProductPage.GetContext(), "PagingAlign");
                if (!"bottom".Equals(str, StringComparison.InvariantCultureIgnoreCase))
                {
                    return "both".Equals(str, StringComparison.InvariantCultureIgnoreCase);
                }
                return true;
            }
        }

        public static bool ShowNavTop
        {
            get
            {
                return !"bottom".Equals(ProductPage.Config(ProductPage.GetContext(), "PagingAlign"), StringComparison.InvariantCultureIgnoreCase);
            }
        }

        [Personalizable]
        public bool SortDesc
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("SortDesc", this.sortDesc);
                }
                return false;
            }
            set
            {
                this.sortDesc = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public string SortProp
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return string.Empty;
                }
                return base.GetProp<string>("SortProp", this.sortProp);
            }
            set
            {
                this.sortProp = base.LicEd(2) ? value : string.Empty;
            }
        }

        internal static string SspWebUrl
        {
            get
            {
                if (sspWebUrl == null)
                {
                    sspWebUrl = ProductPage.GetSrpUrl();
                }
                return sspWebUrl;
            }
        }

        [Personalizable]
        public bool TabInteractive
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("TabInteractive", this.tabInteractive);
                }
                return false;
            }
            set
            {
                this.tabInteractive = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public string TabProp
        {
            get
            {
                if (!base.LicEd(2))
                {
                    return string.Empty;
                }
                return base.GetProp<string>("TabProp", this.tabProp);
            }
            set
            {
                this.tabProp = base.LicEd(2) ? value : string.Empty;
            }
        }

        [Personalizable]
        public string TileWidth
        {
            get
            {
                return base.GetProp<string>("TileWidth", this.tileWidth);
            }
            set
            {
                this.tileWidth = value;
            }
        }

        [Personalizable]
        public override bool UrlSettings
        {
            get
            {
                return (base.LicEd(4) && base.UrlSettings);
            }
            set
            {
                base.UrlSettings = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public bool Vcard
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("Vcard", this.vcard);
                }
                return false;
            }
            set
            {
                this.vcard = base.LicEd(4) && value;
            }
        }

        public override WebPartVerbCollection Verbs
        {
            get
            {
                WebPartVerbCollection verbs = base.Verbs;
                WebPartVerb exportVerb = this.ExportVerb;
                WebPartVerb printVerb = this.PrintVerb;
                List<WebPartVerb> list = new List<WebPartVerb>();
                if (verbs != null)
                {
                    foreach (WebPartVerb verb3 in verbs)
                    {
                        list.Add(verb3);
                    }
                }
                if (printVerb != null)
                {
                    list.Add(printVerb);
                }
                if (exportVerb != null)
                {
                    list.Add(exportVerb);
                }
                return new WebPartVerbCollection(list);
            }
        }

        public string WebUrl
        {
            get
            {
                if (this.webUrl == null)
                {
                    try
                    {
                        this.webUrl = SPContext.Current.Web.Url.TrimEnd(new char[] { '/' });
                    }
                    catch
                    {
                        this.webUrl = string.Empty;
                    }
                }
                return this.webUrl;
            }
        }
    }
}

