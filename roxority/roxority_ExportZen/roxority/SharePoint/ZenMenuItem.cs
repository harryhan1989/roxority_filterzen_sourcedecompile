namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebControls;
    using Microsoft.SharePoint.WebPartPages;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Xml;

    public class ZenMenuItem : WebControl
    {
        internal string actionPropPrefix = string.Empty;
        internal string baseSequence = "100";
        private static PropertyInfo camlAndProp = null;
        private SPFolder folder;
        private static PropertyInfo fpKeyProp = null;
        private static PropertyInfo fpOpProp = null;
        private static PropertyInfo fpValProp = null;
        private static MethodInfo getConnPartMethod = null;
        private static string iconUrl = null;
        private static string iconUrlSmall = null;
        private bool? isDisp = null;
        private SPListItem item;
        private static PropertyInfo kvpValProp = null;
        [ThreadStatic]
        private static ProductPage.LicInfo l = null;
        internal SPList list;
        private LiteralControl literal = new LiteralControl();
        private ToolBarMenuButton menuButton;
        private MenuTemplate menuTemplate;
        private static PropertyInfo partFiltersProp = null;
        private static PropertyInfo partJsonProp = null;
        public readonly Dictionary<string, string> PhVals = new Dictionary<string, string>();
        private string pn;
        private static readonly Reflector refl = new Reflector(typeof(FormButton).Assembly);
        private List<RibbonItem> ribbons = new List<RibbonItem>();
        private static string schemaName = null;
        private SPContext spCtx;
        private string tri;
        private SPView view;
        internal System.Web.UI.WebControls.WebParts.WebPart webPart;

        internal void AddFormButton(string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo)
        {
            FormButton child = new FormButton();
            if (cmdNo > 1)
            {
                this.Controls.Add(new LiteralControl("</span></td><td class=\"ms-separator\">&nbsp;</td><td class=\"ms-toolbar\" noWrap=\"nowrap\"><span>"));
            }
            child.ID = id;
            child.ToolTip = desc;
            if (string.IsNullOrEmpty(clickScript))
            {
                child.NavigateUrl = clickUrl;
            }
            else
            {
                child.OnClientClick = clickScript + "return(event && (event.returnValue=!(event.cancelBubble=true)));";
            }
            child.ImageUrl = img;
            child.CssClass = "roxlistformbutton";
            child.Text = caption;
            child.ControlMode = SPControlMode.Display;
            child.PermissionContext = this.PermContext;
            child.Permissions = this.Perms;
            this.Controls.Add(child);
            if (ProductPage.Is14)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(ClientScriptManager), ProductPage.AssemblyName + "_Script_" + cmdNo, " " + this.Prod + "Buttons['" + id + "'] = '" + child.ClientID + "'; ", true);
                this.AddRibbonButton(id, caption, img, desc, clickUrl, clickScript, cmdNo, child);
            }
        }

        internal void AddItem(IDictionary inst, string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo)
        {
            foreach (KeyValuePair<string, string> pair in this.PhVals)
            {
                string oldValue = this.PhName(pair.Key);
                caption = caption.Replace(oldValue, pair.Value);
                string introduced3 = this.PhName(pair.Key);
                desc = desc.Replace(introduced3, pair.Value);
            }
            if (!this.DispForm && !this.IsDispFormOnly(inst))
            {
                this.AddMenuItem(inst, id, caption, img, desc, clickUrl, clickScript, cmdNo);
            }
            else if (this.DispForm && this.IsDispFormSupported(inst))
            {
                this.AddFormButton(id, caption, img, desc, clickUrl, clickScript, cmdNo);
            }
        }

        internal void AddMenuItem(IDictionary inst, string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo)
        {
            MenuItemTemplate child = new MenuItemTemplate(caption, img) {
                ID = id,
                Description = desc,
                PermissionContext = this.PermContext,
                Permissions = this.Perms
            };
            if (string.IsNullOrEmpty(clickScript))
            {
                child.ClientOnClickNavigateUrl = SPEncode.ScriptEncode(clickUrl);
            }
            else
            {
                child.ClientOnClickScript = clickScript;
            }
            this.Controls.Add(child);
            if (ProductPage.Is14)
            {
                this.ribbons.Add(new RibbonItem(child, inst, this.ClientID + id, caption, img, desc, clickUrl, clickScript, cmdNo));
                this.WebPartIDs.ToString();
            }
        }

        internal void AddRibbonButton(string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo, FormButton btn)
        {
            object obj2 = this.Page.Items[refl.GetType("Microsoft.SharePoint.WebControls.SPRibbon")];
            XmlDocument document = new XmlDocument();
            if (obj2 != null)
            {
                document.LoadXml(string.Concat(new object[] { 
                    "<Button Id=\"Ribbon.", this.RibbonPath, ".", this.RibbonGroup, ".Controls.", this.ProdName, "Action_", id, "\" Sequence=\"100", this.baseSequence, cmdNo, "\" Command=\"", this.ProdName, "Action\" Image16by16=\"", this.SmallIconUrl, "\" Image32by32=\"", 
                    this.IconUrl, "\" LabelText=\"", HttpUtility.HtmlEncode(caption), "\" Description=\"", HttpUtility.HtmlEncode(desc), "\" ToolTipDescription=\"", HttpUtility.HtmlEncode(desc), "\" ToolTipTitle=\"", HttpUtility.HtmlEncode(caption), "\" Alt=\"\" TemplateAlias=\"o", (cmdNo == 1) ? 1 : 2, "\" />"
                 }));
                refl.Call(obj2, "RegisterDataExtension", null, new object[] { document.DocumentElement, "Ribbon." + this.RibbonPath + "." + this.RibbonGroup + ".Controls._children" });
            }
        }

        internal void AddRibbonButton(IDictionary inst, string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo, MenuItemTemplate btn)
        {
            object obj2 = this.Page.Items[refl.GetType("Microsoft.SharePoint.WebControls.SPRibbon")];
            XmlDocument document = new XmlDocument();
            if (obj2 != null)
            {
                document.LoadXml(string.Concat(new object[] { 
                    "<Button Id=\"Ribbon.", this.RibbonPath, ".", this.RibbonGroup, ".Controls.", this.ProdName, "Action_", id, "\" Sequence=\"", this.baseSequence, cmdNo, "\" Command=\"", this.ProdName, "Action\" Image16by16=\"", this.SmallIconUrl, "\" Image32by32=\"", 
                    this.IconUrl, "\" LabelText=\"", HttpUtility.HtmlEncode(caption), "\" Description=\"", HttpUtility.HtmlEncode(desc), "\" ToolTipDescription=\"", HttpUtility.HtmlEncode(desc), "\" ToolTipTitle=\"", HttpUtility.HtmlEncode(caption + "\r\n[" + this.PhVals["Context_Title"] + "]"), "\" Alt=\"\" TemplateAlias=\"o2\" DisplayMode=\"Small\" />"
                 }));
                refl.Call(obj2, "RegisterDataExtension", null, new object[] { document.DocumentElement, "Ribbon." + this.RibbonPath + "." + this.RibbonGroup + ".Controls._children" });
            }
        }

        protected override void CreateChildControls()
        {
            string fj = string.Empty;
            string str2 = string.Empty;
            string str7 = string.Empty;
            bool includeFilters = false;
            bool useView = false;
            int cmdNo = 0;
            new Hashtable();
            List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters = null;
            SPContext current = SPContext.Current;
            SPSecurity.CatchAccessDeniedException = false;
            current.Site.CatchAccessDeniedException = false;
            if ((((this.Page != null) && (this.Page.Header != null)) && ((this.List != null) && (this.Context != null))) && (((this.Context.Request != null) && (this.Context.Request.Url != null)) && !this.Context.Request.Url.AbsolutePath.ToLowerInvariant().Contains("/_layouts/roxority_")))
            {
                using (ProductPage page = new ProductPage())
                {
                    using (SPSite site = new SPSite(current.Site.ID))
                    {
                        site.CatchAccessDeniedException = false;
                        using (SPWeb web = site.OpenWeb(current.Web.ID))
                        {
                            IEnumerable<IDictionary> enumerable = JsonSchemaManager.GetInstances(page, null, this.SchemaName, web, this.List, this.DispForm ? null : this.View, true, true, false);
                            if (enumerable != null)
                            {
                                string siteUrl = web.ServerRelativeUrl.TrimEnd(new char[] { '/' });
                                this.PhVals["View_Title"] = string.Empty;
                                if (this.WebPart == null)
                                {
                                    this.PhVals["WebPart_Title"] = string.Empty;
                                }
                                this.PhVals["TitleBar_Title"] = this.Page.Title;
                                if (this.LoadScript)
                                {
                                    if (!ProductPage.Config<bool>(null, "_nojquery") && !this.Page.Items.Contains("jquery"))
                                    {
                                        this.Page.Items["jquery"] = new object();
                                        this.Page.ClientScript.RegisterClientScriptInclude("jquery", string.Concat(new object[] { siteUrl, "/_layouts/", ProductPage.AssemblyName, "/jQuery.js?v=", ProductPage.Version }));
                                    }
                                    this.Page.ClientScript.RegisterClientScriptInclude(ProductPage.AssemblyName, string.Concat(new object[] { siteUrl, "/_layouts/", ProductPage.AssemblyName, "/", ProductPage.AssemblyName, ".js?v=", ProductPage.Version }));
                                }
                                if (ProductPage.Is14)
                                {
                                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(ClientScriptManager), ProductPage.AssemblyName + "_Script", ProductPage.GetResource("__RibbonScript", new object[] { "{", "}", this.Prod, this.ProdName }), true);
                                }
                                foreach (IDictionary dictionary in enumerable)
                                {
                                    string resource;
                                    if (!this.IsActionSupported(dictionary))
                                    {
                                        continue;
                                    }
                                    foreach (DictionaryEntry entry in dictionary)
                                    {
                                        string introduced40 = this.actionPropPrefix + entry.Key;
                                        this.PhVals[introduced40] = entry.Value + string.Empty;
                                    }
                                    this.PhVals["QueryString_a"] = dictionary["id"] + string.Empty;
                                    string clickScript = string.Empty;
                                    if (!ProductPage.isEnabled)
                                    {
                                        using (SPSite site2 = ProductPage.GetAdminSite())
                                        {
                                            string str4;
                                            clickScript = "if(confirm('" + SPEncode.ScriptEncode(ProductPage.GetResource("NotEnabledPlain", new object[] { str4 = ProductPage.MergeUrlPaths(site2.Url, string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=enable&r=", page.Rnd.Next() })), ProductPage.GetTitle() })) + @"\n\n" + SPEncode.ScriptEncode(ProductPage.GetResource("NotEnabledPrompt", new object[0])) + "'))location.href='" + str4 + "';";
                                            goto Label_0737;
                                        }
                                    }
                                    if (!this.Page.Items.Contains(this.Prod + "zenlistids"))
                                    {
                                        this.Page.Items[this.Prod + "zenlistids"] = str2 = "," + this.List.ID + ",";
                                    }
                                    else if (!(str2 = this.Page.Items[this.Prod + "zenlistids"] + string.Empty).Contains("," + this.List.ID + ","))
                                    {
                                        this.Page.Items[this.Prod + "zenlistids"] = str2 = string.Concat(new object[] { str2, ",", this.List.ID, "," });
                                    }
                                    if (!this.Page.Items.Contains(this.Prod + "zencmdcount"))
                                    {
                                        this.Page.Items[this.Prod + "zencmdcount"] = cmdNo = 1;
                                    }
                                    else
                                    {
                                        this.Page.Items[this.Prod + "zencmdcount"] = cmdNo = ((int) this.Page.Items[this.Prod + "zencmdcount"]) + 1;
                                    }
                                    this.ValidateInstance(dictionary, ref clickScript);
                                    useView = false;
                                    if (dictionary["view"] != null)
                                    {
                                        try
                                        {
                                            useView = (bool) dictionary["view"];
                                        }
                                        catch
                                        {
                                            useView = false;
                                        }
                                    }
                                    if (useView && !IsLic(this.Vl))
                                    {
                                        useView = false;
                                        clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource("PC_" + this.SchemaName + "_view", new object[0]), (this.Vl == 2) ? "Basic" : ((this.Vl == 0) ? "Lite" : "Ultimate") })) + "');";
                                    }
                                    GetFilterInfo(dictionary, this.SchemaName, ref clickScript, this.WebPart, this.Page, ref includeFilters, ref fj, ref effectiveFilters);
                                Label_0737:
                                    if (Lic.expired)
                                    {
                                        resource = ProductPage.GetResource("LicExpiry", new object[0]);
                                    }
                                    else
                                    {
                                        resource = JsonSchemaManager.GetDisplayName(dictionary, this.SchemaName, false);
                                    }
                                    string desc = Lic.expired ? ProductPage.GetResource("LicStudio", new object[] { ProductPage.GetTitle() }) : (IsLic(2) ? (dictionary["desc"]).ToString() : ("SharePoint-Tools.net/" + this.ProdName));
                                    //if (string.IsNullOrEmpty(clickScript) && string.IsNullOrEmpty(clickScript = this.GetClickScript(siteUrl, clickScript, dictionary, web, useView, includeFilters, effectiveFilters, fj, this.GetFlag(dictionary))))
                                    //{
                                    //    this.AddItem(dictionary, dictionary["id"] + string.Empty, resource, siteUrl + this.IconUrl, desc, siteUrl + (Lic.expired ? string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=lic&r=", page.Rnd.Next() }) : this.GetActionUrl(dictionary, web, useView, includeFilters, effectiveFilters, fj, this.GetFlag(dictionary))), string.Empty, cmdNo);
                                    //}
                                    //else
                                    //{
                                    var actionUrl = this.GetActionUrl(dictionary, web, useView, includeFilters, effectiveFilters, fj, this.GetFlag(dictionary));
                                    var script = "post_to_url('" + actionUrl.Split('?')[0] + "','" + actionUrl.Split('?')[1] + "')";
                                    this.AddItem(dictionary, dictionary["id"] + string.Empty, resource, siteUrl + this.IconUrl, desc, Lic.expired ? string.Concat(new object[] { siteUrl, "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=lic&r=", page.Rnd.Next() }) : string.Empty, Lic.expired ? string.Empty : script, cmdNo);
                                    //}
                                    if (string.IsNullOrEmpty(str7))
                                    {
                                        try
                                        {
                                            str7 = string.Concat(new object[] { siteUrl, "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=tools&tool=Tool_", this.SchemaName, "&r=", page.Rnd.Next() });
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if ((cmdNo >= 1) && !IsLic(2))
                                    {
                                        break;
                                    }
                                }
                                if (this.ribbons.Count > 0)
                                {
                                    foreach (RibbonItem item in this.ribbons)
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(typeof(ClientScriptManager), ProductPage.AssemblyName + "_Script_" + item.CmdNo, " " + this.Prod + (string.IsNullOrEmpty(item.ClickScript) ? "Urls" : "Commands") + "['" + item.ID + "'] = '" + SPEncode.ScriptEncode(string.IsNullOrEmpty(item.ClickScript) ? item.ClickUrl : item.ClickScript) + "'; ", true);
                                        this.AddRibbonButton(item.Inst, item.ID, item.Caption, item.Img, item.Desc, item.ClickUrl, item.ClickScript, item.CmdNo, item.Item);
                                    }
                                }
                                this.OnActionsCreated(cmdNo);
                            }
                        }
                    }
                }
            }
            base.CreateChildControls();
        }

        internal static T FindParent<T>(Control parent) where T: class
        {
            if (parent is T)
            {
                return (parent as T);
            }
            if (parent != null)
            {
                return FindParent<T>(parent.Parent);
            }
            return default(T);
        }

        protected virtual string GetActionUrl(IDictionary inst, SPWeb thisWeb, bool useView, bool includeFilters, List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters, string fj, bool flag)
        {
            return ("/_layouts/" + ProductPage.AssemblyName + "/default.aspx");
        }

        protected virtual string GetClickScript(string siteUrl, string clickScript, IDictionary inst, SPWeb thisWeb, bool useView, bool includeFilters, List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters, string fj, bool flag)
        {
            return clickScript;
        }

        public static void GetFilterInfo(IDictionary inst, string schemaName, ref string clickScript, System.Web.UI.WebControls.WebParts.WebPart webPart, Page page, ref bool includeFilters, ref string fj, ref List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters)
        {
            SPWebPartManager currentWebPartManager = null;
            System.Web.UI.WebControls.WebParts.WebPart part = null;
            IList list = null;
            List<string> list2 = new List<string>();
            List<object[]> list3 = new List<object[]>();
            includeFilters = false;
            string filterCaml = "";
            if (inst["filter"] != null)
            {
                try
                {
                    includeFilters = (bool) inst["filter"];
                }
                catch
                {
                    includeFilters = false;
                }
            }
            if (includeFilters && !IsLic(2))
            {
                includeFilters = false;
                clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource("PC_" + schemaName + "_filter", new object[0]), "Basic" })) + "');";
            }
            if (includeFilters && (effectiveFilters == null))
            {
                effectiveFilters = new List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>>();
                try
                {
                    currentWebPartManager = WebPartManager.GetCurrentWebPartManager(page) as SPWebPartManager;
                }
                catch
                {
                }
                if ((webPart != null) && (currentWebPartManager != null))
                {
                    foreach (System.Web.UI.WebControls.WebParts.WebPart part2 in ProductPage.TryEach<System.Web.UI.WebControls.WebParts.WebPart>(currentWebPartManager.WebParts))
                    {
                        if (part2.GetType().AssemblyQualifiedName == "roxority_FilterZen.roxority_FilterWebPart, roxority_FilterZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1")
                        {
                            foreach (System.Web.UI.WebControls.WebParts.WebPart part3 in ((getConnPartMethod == null) ? (getConnPartMethod = part2.GetType().GetMethod("GetConnectedParts", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, System.Type.EmptyTypes, null)) : getConnPartMethod).Invoke(part2, null) as IEnumerable<System.Web.UI.WebControls.WebParts.WebPart>)
                            {
                                if (part3.ID.Equals(webPart.ID))
                                {
                                    part = part2;
                                    break;
                                }
                            }
                        }
                        if (part != null)
                        {
                            break;
                        }
                    }
                }
                if (part != null)
                {
                    try
                    {
                        if (!((bool) part.GetType().GetMethod("LicEd", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(part, new object[] { 2 })))
                        {
                            clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetProductResource("Old_NoFilterZenEnt", new object[0])) + "');";
                            part = null;
                        }
                    }
                    catch
                    {
                        clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetProductResource("Old_NoFilterZenEnt", new object[0])) + "');";
                        part = null;
                    }
                }
                fj = string.Empty;
                if (part != null)
                {
                    try
                    {
                        filterCaml = ((roxority_FilterZen.roxority_FilterWebPart)(part)).GeneratedQuery;
                        list = ((partFiltersProp == null) ? (partFiltersProp = part.GetType().GetProperty("PartFilters", BindingFlags.Public | BindingFlags.Instance)) : partFiltersProp).GetValue(part, null) as IList;
                        fj = ((partJsonProp == null) ? (partJsonProp = part.GetType().GetProperty("JsonFilters", BindingFlags.Public | BindingFlags.Instance)) : partJsonProp).GetValue(part, null) as string;
                        list2.AddRange(((string) ((camlAndProp == null) ? (camlAndProp = part.GetType().GetProperty("CamlFiltersAndCombined", BindingFlags.Public | BindingFlags.Instance)) : camlAndProp).GetValue(part, null)).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    catch
                    {
                    }
                    if (list != null)
                    {
                        foreach (object obj3 in list)
                        {
                            if (kvpValProp == null)
                            {
                                kvpValProp = obj3.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                            }
                            object obj2 = kvpValProp.GetValue(obj3, null);
                            if (obj2 != null)
                            {
                                if (fpKeyProp == null)
                                {
                                    fpKeyProp = obj2.GetType().GetProperty("Key", BindingFlags.Public | BindingFlags.Instance);
                                }
                                if (fpValProp == null)
                                {
                                    fpValProp = obj2.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                                }
                                if (fpOpProp == null)
                                {
                                    fpOpProp = obj2.GetType().GetProperty("CamlOperator", BindingFlags.Public | BindingFlags.Instance);
                                }
                                list3.Add(new object[] { fpKeyProp.GetValue(obj2, null), fpValProp.GetValue(obj2, null), (CamlOperator) Enum.Parse(typeof(CamlOperator), fpOpProp.GetValue(obj2, null).ToString(), true) });
                            }
                        }
                    }
                    foreach (object[] objArray in list3)
                    {
                        int num2;
                        KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> item = new KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>(objArray[0] as string, new KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>(new List<KeyValuePair<string, CamlOperator>>(), list2.Contains(objArray[0] as string)));
                        int num = num2 = -1;
                        foreach (KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> pair2 in effectiveFilters)
                        {
                            num2++;
                            if (pair2.Key == item.Key)
                            {
                                num = num2;
                                item = pair2;
                                break;
                            }
                        }
                        item.Value.Key.Add(new KeyValuePair<string, CamlOperator>(objArray[1] as string, (CamlOperator) objArray[2]));
                        if (num >= 0)
                        {
                            effectiveFilters[num] = item;
                        }
                        else
                        {
                            effectiveFilters.Add(item);
                        }
                    }
                }
            }
            fj += "(((((("+filterCaml+"))))))";
        }

        protected virtual bool GetFlag(IDictionary inst)
        {
            return false;
        }

        protected virtual bool IsActionSupported(IDictionary inst)
        {
            return true;
        }

        protected virtual bool IsDispFormOnly(IDictionary inst)
        {
            return false;
        }

        protected virtual bool IsDispFormSupported(IDictionary inst)
        {
            return this.DispFormSupported;
        }

        internal static bool IsLic(int e)
        {
            return ProductPage.LicEdition(ProductPage.GetContext(), Lic, e);
        }

        protected virtual void OnActionsCreated(int cmdCount)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            this.EnsureChildControls();
            base.OnLoad(e);
        }

        internal string PhName(string name)
        {
            return ("{$" + this.ProdName + "_" + name + "$}");
        }

        protected virtual void PrepareChildControls(IDictionary propBag)
        {
        }

        protected virtual void ValidateInstance(IDictionary inst, ref string clickScript)
        {
        }

        public bool DispForm
        {
            get
            {
                return ((this.DispFormSupported && (this.MenuButton == null)) && (this.ListItem != null));
            }
        }

        public virtual bool DispFormSupported
        {
            get
            {
                return false;
            }
        }

        public virtual string IconUrl
        {
            get
            {
                if (string.IsNullOrEmpty(iconUrl))
                {
                    iconUrl = ProductPage.GetProductResource("__MenuActionIcon", new object[0]);
                }
                return iconUrl;
            }
        }

        public bool IsCalendar
        {
            get
            {
                return ((this.List != null) && (this.list.BaseTemplate == SPListTemplateType.Events));
            }
        }

        public bool IsDispForm
        {
            get
            {
                if (!this.isDisp.HasValue || !this.isDisp.HasValue)
                {
                    this.isDisp = new bool?((this.WebPart == null) ? (!string.IsNullOrEmpty(this.Context.Request.QueryString["ID"]) && (this.Context.Request.RawUrl.ToLowerInvariant().Contains("/dispform.aspx?") || this.Context.Request.RawUrl.ToLowerInvariant().Contains("/formdisp.aspx?"))) : (this.WebPart is ListFormWebPart));
                }
                return this.isDisp.Value;
            }
        }

        internal string this[string resKey, object[] args]
        {
            get
            {
                return ProductPage.GetProductResource(resKey, args);
            }
        }

        internal static ProductPage.LicInfo Lic
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

        public virtual SPList List
        {
            get
            {
                if (this.list == null)
                {
                    if (this.MenuButton != null)
                    {
                        this.list = this.menuButton.List;
                    }
                    if ((this.list == null) && (this.ListItem != null))
                    {
                        this.list = this.item.ParentList;
                    }
                    if (this.list != null)
                    {
                        this.PhVals["List_Title"] = this.list.Title;
                        this.PhVals["QueryString_l"] = ProductPage.GuidBracedUpper(this.list.ID);
                        if (((this.ListItem == null) && (this.WebPart == null)) && (this.View == null))
                        {
                            this.PhVals["Context_Title"] = this.list.Title;
                        }
                    }
                }
                return this.list;
            }
        }

        public virtual SPFolder ListFolder
        {
            get
            {
                if (this.folder == null)
                {
                    ToolBarMenuButton menuButton = this.MenuButton;
                }
                return this.folder;
            }
        }

        public SPListItem ListItem
        {
            get
            {
                if ((this.DispFormSupported && this.IsDispForm) && ((this.item == null) && ((this.item = this.SpContext.ListItem) != null)))
                {
                    string str;
                    if (string.IsNullOrEmpty(str = this.item.Title))
                    {
                        try
                        {
                            str = this.item["Name"] + string.Empty;
                        }
                        catch
                        {
                        }
                    }
                    this.PhVals["Context_Title"] = this.PhVals["Item_Title"] = string.IsNullOrEmpty(str) ? ("#" + this.item.ID) : str;
                    this.PhVals["QueryString_View"] = this.item.ID.ToString();
                }
                return this.item;
            }
        }

        public virtual bool LoadScript
        {
            get
            {
                return ProductPage.Is14;
            }
        }

        public ToolBarMenuButton MenuButton
        {
            get
            {
                if (this.menuButton == null)
                {
                    this.menuButton = FindParent<ToolBarMenuButton>(this.Parent);
                }
                return this.menuButton;
            }
        }

        public MenuTemplate MenuTemplateControl
        {
            get
            {
                if (this.menuTemplate == null)
                {
                    this.menuTemplate = FindParent<MenuTemplate>(this.Parent);
                }
                return this.menuTemplate;
            }
        }

        public virtual PermissionContext PermContext
        {
            get
            {
                if (!this.DispForm)
                {
                    return PermissionContext.CurrentFolder;
                }
                return PermissionContext.CurrentItem;
            }
        }

        public virtual SPBasePermissions Perms
        {
            get
            {
                string[] strArray;
                string str = ProductPage.Config(this.SpContext, "MenuPerms");
                SPBasePermissions permissions = SPBasePermissions.EmptyMask | SPBasePermissions.ViewListItems;
                if ((!string.IsNullOrEmpty(str) && ((strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) != null)) && (strArray.Length > 0))
                {
                    foreach (string str2 in strArray)
                    {
                        permissions |= (SPBasePermissions) Enum.Parse(typeof(SPBasePermissions), str2, true);
                    }
                }
                return permissions;
            }
        }

        public string Prod
        {
            get
            {
                if (this.tri == null)
                {
                    this.tri = "rox" + this.ProdName.Substring(0, 3);
                }
                return this.tri;
            }
        }

        public string ProdName
        {
            get
            {
                if (this.pn == null)
                {
                    this.pn = ProductPage.AssemblyName.Substring(ProductPage.AssemblyName.IndexOf('_') + 1);
                }
                return this.pn;
            }
        }

        public virtual string RibbonGroup
        {
            get
            {
                return "Actions";
            }
        }

        public virtual string RibbonPath
        {
            get
            {
                if (this.DispForm)
                {
                    return "ListForm.Display";
                }
                if (this.List is SPDocumentLibrary)
                {
                    return "Library";
                }
                if (!this.IsCalendar)
                {
                    return "List";
                }
                return "Calendar.Calendar";
            }
        }

        public virtual string RootFolderUrl
        {
            get
            {
                string str;
                string str2;
                if (ProductPage.Is14 && !string.IsNullOrEmpty(str2 = Reflector.Current.Get(this.SpContext, "RootFolderUrl", new object[0]) + string.Empty))
                {
                    return str2;
                }
                Guid guid = (this.View == null) ? Guid.Empty : this.View.ID;
                Guid guid2 = ProductPage.GetGuid(this.Context.Request.QueryString["View"], true);
                if (((guid != Guid.Empty) && (guid == guid2)) && (!string.IsNullOrEmpty(str = this.Context.Request.QueryString["RootFolder"]) && (str != "*")))
                {
                    return str;
                }
                return this.List.RootFolder.ServerRelativeUrl;
            }
        }

        public string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(schemaName))
                {
                    schemaName = ProductPage.GetProductResource("__MenuActionDefaultSchema", new object[0]);
                }
                return schemaName;
            }
        }

        public virtual string SmallIconUrl
        {
            get
            {
                if (string.IsNullOrEmpty(iconUrlSmall))
                {
                    iconUrlSmall = ProductPage.GetProductResource("__MenuActionIconSmall", new object[0]);
                }
                return iconUrlSmall;
            }
        }

        public SPContext SpContext
        {
            get
            {
                if (this.MenuButton == null)
                {
                    return SPContext.Current;
                }
                return this.MenuButton.RenderContext;
            }
        }

        public virtual SPView View
        {
            get
            {
                if (this.view == null)
                {
                    if (this.MenuButton != null)
                    {
                        this.view = this.menuButton.View;
                    }
                    if (this.view != null)
                    {
                        this.PhVals["QueryString_View"] = ProductPage.GuidBracedUpper(this.view.ID);
                        if ((this.List != null) && (this.ListItem == null))
                        {
                            this.PhVals["Context_Title"] = this.list.Title + ((string.IsNullOrEmpty(this.list.Title) || string.IsNullOrEmpty(this.view.Title)) ? string.Empty : " - ") + this.view.Title;
                        }
                        this.PhVals["View_Title"] = this.view.Title;
                    }
                }
                return this.view;
            }
        }

        public virtual Guid ViewID
        {
            get
            {
                if ((this.MenuButton != null) && !Guid.Empty.Equals(this.menuButton.ViewId))
                {
                    this.PhVals["QueryString_View"] = ProductPage.GuidBracedUpper(this.menuButton.ViewId);
                    return this.menuButton.ViewId;
                }
                if (this.View != null)
                {
                    return this.view.ID;
                }
                return Guid.Empty;
            }
        }

        internal virtual int Vl
        {
            get
            {
                return 4;
            }
        }

        public virtual System.Web.UI.WebControls.WebParts.WebPart WebPart
        {
            get
            {
                if ((this.webPart == null) && ((this.webPart = FindParent<System.Web.UI.WebControls.WebParts.WebPart>(this.Parent)) != null))
                {
                    this.PhVals["WebPart_Title"] = this.webPart.DisplayTitle;
                    if ((this.ListItem == null) && (this.View == null))
                    {
                        this.PhVals["Context_Title"] = this.webPart.DisplayTitle;
                    }
                }
                return this.webPart;
            }
        }

        public List<string> WebPartIDs
        {
            get
            {
                string str = ProductPage.AssemblyName + "_ribbonwpids";
                List<string> list = this.Context.Items[str] as List<string>;
                if (list == null)
                {
                    this.Context.Items[str] = list = new List<string>();
                }
                if ((this.WebPart != null) && !list.Contains(this.WebPart.ID))
                {
                    list.Add(this.WebPart.ID);
                }
                return list;
            }
        }

        private class RibbonItem
        {
            public readonly string Caption;
            public readonly string ClickScript;
            public readonly string ClickUrl;
            public readonly int CmdNo;
            public readonly string Desc;
            public readonly string ID;
            public readonly string Img;
            public readonly IDictionary Inst;
            public readonly MenuItemTemplate Item;

            public RibbonItem(MenuItemTemplate item, IDictionary inst, string id, string caption, string img, string desc, string clickUrl, string clickScript, int cmdNo)
            {
                this.Item = item;
                this.Inst = inst;
                this.ID = id;
                this.Caption = caption;
                this.Desc = desc;
                this.Img = img;
                this.ClickScript = clickScript;
                this.ClickUrl = clickUrl;
                this.CmdNo = cmdNo;
            }
        }
    }
}

