using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using roxority.Shared;
using roxority.SharePoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace roxority_FilterZen
{
    [Serializable]
    public abstract class FilterBase : ISerializable
    {
        [Serializable]
        public abstract class Interactive : FilterBase
        {
            public const string CHOICE_EMPTY = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            protected internal const string PLACEHOLDER_LISTID = "%%PLACEHOLDER_LISTID%%";
            protected internal const string SCRIPT_CHECK_DEFAULT = "setTimeout('var roxtmp = document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%2\\'); if (document.getElementById(\\'filter_DefaultIfEmpty\\').disabled = ((document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%\\').selectedIndex == 0) || (roxtmp && (roxtmp.selectedIndex == 0)))) { document.getElementById(\\'label_filter_DefaultIfEmpty\\').style.textDecoration = \\'none\\'; document.getElementById(\\'filter_DefaultIfEmpty\\').checked = true; }', 150);";
            protected internal static readonly string[] baseViewFields = new string[]
			{
				"ID",
				"Title",
				"LinkTitle",
				"LinkTitleNoMenu",
				"FileLeafRef",
				"LinkFilenameNoMenu",
				"LinkFilename",
				"BaseName"
			};
            protected internal int reqEd;
            protected internal bool defaultIfEmpty;
            protected internal bool doPostFilterNow;
            protected internal bool interactive;
            protected internal bool pickerSemantics;
            protected internal bool postFiltered;
            protected internal bool supportAllowMultiEnter;
            protected internal bool supportAutoSuggest;
            protected internal bool suppressInteractive;
            internal SPList postFilterList;
            internal SPView postFilterView;
            private int pickerLimit = -1;
            private bool allowMultiEnter;
            private bool autoSuggest;
            private bool postFilter;
            private bool checkStyle;
            private bool sendAllAsMultiValuesIfEmpty;
            internal string beginGroup = string.Empty;
            internal string label = string.Empty;
            internal string label2 = string.Empty;
            internal string postFilterListViewUrl = string.Empty;
            internal string postFilterFieldName = string.Empty;
            protected internal virtual IEnumerable<string> AllPickableValues
            {
                get
                {
                    return null;
                }
            }
            protected internal string HtmlOnChangeAttr
            {
                get
                {
                    if (this.parentWebPart == null || !this.parentWebPart.AutoRepost)
                    {
                        return string.Empty;
                    }
                    return " onchange=\"roxRefreshFilters('" + this.parentWebPart.ID + "');\"";
                }
            }
            protected internal string HtmlOnChangeMultiAttr
            {
                get
                {
                    return " onchange=\"roxMultiSelect(this);" + ((this.parentWebPart != null && this.parentWebPart.AutoRepost) ? ("roxRefreshFilters('" + this.parentWebPart.ID + "');") : string.Empty) + "\"";
                }
            }
            public bool AllowMultiEnter
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && this.allowMultiEnter && !base.IsRange;
                }
                set
                {
                    this.allowMultiEnter = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && value && !base.IsRange);
                }
            }
            public bool AutoSuggest
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(4)) && this.autoSuggest;
                }
                set
                {
                    this.autoSuggest = ((this.parentWebPart == null || this.parentWebPart.LicEd(4)) && value);
                }
            }
            public string BeginGroup
            {
                get
                {
                    return this.beginGroup;
                }
                set
                {
                    this.beginGroup = (value + string.Empty).Trim();
                }
            }
            public virtual bool Cascade
            {
                get
                {
                    return this.pickerSemantics && this.parentWebPart != null && this.parentWebPart.Cascaded && this.parentWebPart.LicEd(4) && this.parentWebPart.CamlFilters && this.parentWebPart.connectedList != null;
                }
            }
            public bool CheckStyle
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && this.checkStyle;
                }
                set
                {
                    this.checkStyle = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && value);
                }
            }
            public virtual bool DefaultIfEmpty
            {
                get
                {
                    return this.defaultIfEmpty;
                }
                set
                {
                    this.defaultIfEmpty = value;
                }
            }
            public virtual bool IsInteractive
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && this.interactive && !this.suppressInteractive;
                }
                set
                {
                    this.interactive = (!this.suppressInteractive && (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && value);
                }
            }
            public bool IsNumeric
            {
                get
                {
                    return !string.IsNullOrEmpty(this.Get<string>("NumberFormat").Trim());
                }
            }
            public bool IsSet
            {
                get
                {
                    List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, string.Empty);
                    return filterValues != null && filterValues.Count > 0 && !string.IsNullOrEmpty(filterValues[0]) && !"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(filterValues[0], StringComparison.InvariantCultureIgnoreCase) && (!"0".Equals(filterValues[0]) || !(this is FilterBase.Lookup));
                }
            }
            public virtual string Label
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.label;
                }
                set
                {
                    this.label = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public virtual string Label2
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.label2;
                }
                set
                {
                    this.label2 = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public int PickerLimit
            {
                get
                {
                    if (this.pickerLimit < 0 && !int.TryParse(ProductPage.Config(null, "PickerLimit"), out this.pickerLimit))
                    {
                        this.pickerLimit = 150;
                    }
                    return this.pickerLimit;
                }
            }
            public bool PostFilter
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && this.postFilter;
                }
                set
                {
                    this.postFilter = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && value);
                }
            }
            public string PostFilterListViewUrl
            {
                get
                {
                    return this.postFilterListViewUrl;
                }
                set
                {
                    this.postFilterListViewUrl = ProductPage.Trim(value, new char[0]);
                }
            }
            public string PostFilterFieldName
            {
                get
                {
                    return this.postFilterFieldName;
                }
                set
                {
                    this.postFilterFieldName = ProductPage.Trim(value, new char[0]);
                }
            }
            public bool SendAllAsMultiValuesIfEmpty
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && this.sendAllAsMultiValuesIfEmpty && !base.IsRange;
                }
                set
                {
                    this.sendAllAsMultiValuesIfEmpty = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) && value && !base.IsRange);
                }
            }
            public Interactive()
            {
            }
            public Interactive(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                try
                {
                    this.DefaultIfEmpty = info.GetBoolean("DefaultIfEmpty");
                    this.IsInteractive = info.GetBoolean("IsInteractive");
                    this.Label = info.GetString("Label");
                    this.PostFilter = info.GetBoolean("PostFilter");
                    this.PostFilterFieldName = info.GetString("PostFilterFieldName");
                    this.PostFilterListViewUrl = info.GetString("PostFilterListViewUrl");
                    this.SendAllAsMultiValuesIfEmpty = info.GetBoolean("SendAllAsMultiValuesIfEmpty");
                    this.AllowMultiEnter = info.GetBoolean("AllowMultiEnter");
                    this.CheckStyle = info.GetBoolean("CheckStyle");
                    if (this is FilterBase.Text || this is FilterBase.Multi)
                    {
                        this.AutoSuggest = info.GetBoolean("AutoSuggest");
                    }
                    this.Label2 = info.GetString("Label2");
                    this.BeginGroup = info.GetString("BeginGroup");
                }
                catch
                {
                }
            }
            internal string CreateCamlViewFields(params string[] viewFields)
            {
                string text = "";
                for (int i = 0; i < viewFields.Length; i++)
                {
                    string arg = viewFields[i];
                    text += string.Format("<FieldRef Name=\"{0}\"/>", arg);
                }
                return text;
            }
            internal SPQuery CreateQuery(SPView view, string queryViewFields, string filterCaml)
            {
                SPQuery sPQuery = new SPQuery(view);
                SPQuery arg_66_0 = sPQuery;
                SPQuery arg_5F_0 = sPQuery;
                SPQuery arg_55_0 = sPQuery;
                SPQuery arg_4B_0 = sPQuery;
                SPQuery arg_41_0 = sPQuery;
                SPQuery arg_37_0 = sPQuery;
                SPQuery arg_2D_0 = sPQuery;
                SPQuery arg_24_0 = sPQuery;
                SPQuery arg_1C_0 = sPQuery;
                bool flag;
                sPQuery.RecurrenceOrderBy=(flag = false);
                bool flag2;
                arg_1C_0.IncludeMandatoryColumns=(flag2 = flag);
                bool flag3;
                arg_24_0.IncludeAttachmentVersion=(flag3 = flag2);
                bool flag4;
                arg_2D_0.IncludeAttachmentUrls=(flag4 = flag3);
                bool flag5;
                arg_37_0.IncludeAllUserPermissions=(flag5 = flag4);
                bool flag6;
                arg_41_0.IncludePermissions=(flag6 = flag5);
                bool flag7;
                arg_4B_0.ExpandUserField=(flag7 = flag6);
                bool flag8;
                arg_55_0.IndividualProperties=(flag8 = flag7);
                bool autoHyperlink;
                arg_5F_0.ExpandRecurrence=(autoHyperlink = flag8);
                arg_66_0.AutoHyperlink=(autoHyperlink);
                sPQuery.RowLimit=(0u);
                if (!string.IsNullOrEmpty(queryViewFields))
                {
                    sPQuery.ViewFields=(queryViewFields);
                }
                if (!string.IsNullOrEmpty(filterCaml))
                {
                    sPQuery.Query=(filterCaml);
                }
                return sPQuery;
            }
            internal SPWrap<SPList> GetList(string listUrlParam, bool parentListOrNull)
            {
                bool isParent = parentListOrNull || string.IsNullOrEmpty(this.Get<string>(listUrlParam));
                string listUrl = isParent ? base.Context.Request.Url.ToString() : this.Get<string>(listUrlParam);
                return SPWrap<SPList>.Create(listUrl, delegate(SPWeb web)
                {
                    List<SPList> list = new List<SPList>();
                    string value = this.Context.Request.Url.ToString().Substring(0, this.Context.Request.Url.ToString().LastIndexOf('/'));
                    if (!isParent)
                    {
                        if (!listUrl.ToLowerInvariant().Contains("/_catalogs/users/"))
                        {
                            return ProductPage.GetList(web, listUrl);
                        }
                        return web.Site.GetCatalog((SPListTemplateType)112);
                    }
                    else
                    {
                        foreach (SPList current in ProductPage.TryEach<SPList>(web.Lists))
                        {
                            if (ProductPage.MergeUrlPaths(web.Url, current.DefaultViewUrl).StartsWith(value, StringComparison.InvariantCultureIgnoreCase))
                            {
                                list.Add(current);
                            }
                        }
                        if (list.Count != 1)
                        {
                            return null;
                        }
                        return list[0];
                    }
                });
            }
            internal virtual int GetPageID(string listUrlParam)
            {
                try
                {
                    using (SPWrap<SPList> list = this.GetList(listUrlParam, true))
                    {
                        SPField field;
                        if (list.Value != null && (field = ProductPage.GetField(list.Value, "ServerUrl")) != null)
                        {
                            foreach (SPListItem current in ProductPage.TryEach<SPListItem>(list.Value.Items))
                            {
                                string text;
                                if (!string.IsNullOrEmpty(text = ProductPage.Trim(current[field.Id], new char[0])) && base.Context.Request.Url.ToString().ToLowerInvariant().Contains(text.ToLowerInvariant()))
                                {
                                    return current.ID;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    base.Report(ex);
                }
                return -1;
            }
            internal SPView GetView(SPWrap<SPList> wrap, string listUrl)
            {
                SPView result = wrap.Value.DefaultView;
                foreach (SPView current in ProductPage.TryEach<SPView>(wrap.Value.Views))
                {
                    if (!string.IsNullOrEmpty(current.Url) && ProductPage.MergeUrlPaths(wrap.Web.Url, current.Url).Equals(ProductPage.MergeUrlPaths(wrap.Web.Url, listUrl.Replace("%20", " ")), StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(current.Query))
                    {
                        result = current;
                        break;
                    }
                }
                return result;
            }
            internal KeyValuePair<string[], string[]> PostFilterChoices(string[] choices)
            {
                this.doPostFilterNow = false;
                return this.PostFilterChoices(this.postFilterList, this.postFilterView, this.Get<string>("PostFilterFieldName"), choices, false);
            }
            internal KeyValuePair<string[], string[]> PostFilterChoices(SPList postFilterList, SPView postFilterView, string postFilterFieldName, string[] choices, bool cascade)
            {
                string text = string.Empty;
                string text2 = "<And>{0}{1}</And>";
                string text3 = "<Or>{0}{1}</Or>";
                bool flag = postFilterView == null && postFilterList == null;
                List<string> list = new List<string>();
                SPView sPView = null;
                SPField sPField = null;
                Dictionary<string, SPField> dictionary = new Dictionary<string, SPField>();
                List<string> list2 = new List<string>();
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                SPWrap<SPList> sPWrap = null;
                KeyValuePair<string[], string[]> result;
                try
                {
                    if (choices != null)
                    {
                        List<string> list3 = new List<string>(choices);
                        try
                        {
                            sPWrap = (flag ? this.GetList("PostFilterListViewUrl", false) : new SPWrap<SPList>(postFilterList.ParentWeb.Site, postFilterList.ParentWeb, postFilterList, false));
                            if (sPWrap.Value == null || (sPField = ProductPage.GetField(sPWrap.Value, postFilterFieldName = (string.IsNullOrEmpty(postFilterFieldName) ? "Title" : postFilterFieldName))) == null || (sPView = ((postFilterView == null) ? this.GetView(sPWrap, this.Get<string>("PostFilterListViewUrl")) : postFilterView)) == null)
                            {
                                throw new Exception(base["PostFilterFailed", new object[]
								{
									(sPWrap.Value == null) ? this.Get<string>("PostFilterListViewUrl") : sPWrap.Value.ToString(),
									(sPView == null) ? this.Get<string>("PostFilterListViewUrl") : sPView.ToString(),
									(sPField == null) ? this.Get<string>("PostFilterFieldName") : sPField.ToString()
								}]);
                            }
                            list2.Add(sPField.InternalName);
                            if (cascade)
                            {
                                foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> current in this.parentWebPart.PartFilters)
                                {
                                    if (current.Value.Key == base.Name && this.parentWebPart.cascadeLtr)
                                    {
                                        break;
                                    }
                                    SPField field;
                                    if (current.Value.Key != base.Name && !string.IsNullOrEmpty(current.Value.Value) && (field = ProductPage.GetField(sPWrap.Value, current.Value.Key)) != null)
                                    {
                                        if (!dictionary.ContainsKey(field.InternalName))
                                        {
                                            dictionary.Add(field.InternalName, field);
                                        }
                                        string text4 = ProductPage.CreateSimpleCamlNode(string.Empty, "Eq", field.InternalName, field.TypeAsString, current.Value.Value);
                                        string arg;
                                        if (dictionary2.TryGetValue(current.Value.Key, out arg))
                                        {
                                            dictionary2[current.Value.Key] = string.Format(this.parentWebPart.EffectiveAndFilters.Contains(current.Value.Key) ? text2 : text3, arg, text4);
                                        }
                                        else
                                        {
                                            dictionary2[current.Value.Key] = text4;
                                        }
                                    }
                                }
                                foreach (KeyValuePair<string, string> current2 in dictionary2)
                                {
                                    if (string.IsNullOrEmpty(text))
                                    {
                                        text = current2.Value;
                                    }
                                    else
                                    {
                                        text = string.Format(text2, text, current2.Value);
                                    }
                                }
                            }
                            for (int i = 0; i < list3.Count; i++)
                            {
                                string text4 = ProductPage.CreateSimpleCamlNode(string.Empty, "Eq", sPField.InternalName, sPField.TypeAsString, list3[i]);
                                if (!string.IsNullOrEmpty(text))
                                {
                                    text4 = string.Format(text2, text, text4);
                                }
                                SPListItemCollection items;
                                if ((items = sPWrap.Value.GetItems(this.CreateQuery(sPView, this.CreateCamlViewFields(list2.ToArray()), "<Where>" + text4 + "</Where>"))) == null || items.Count == 0)
                                {
                                    list.Add(list3[i]);
                                    list3.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!cascade || !this.parentWebPart.LicEd(4))
                            {
                                base.Report(ex);
                            }
                        }
                        choices = list3.ToArray();
                    }
                    this.doPostFilterNow = false;
                    result = new KeyValuePair<string[], string[]>(choices, list.ToArray());
                }
                finally
                {
                    if (flag && sPWrap != null)
                    {
                        ((IDisposable)sPWrap).Dispose();
                    }
                }
                return result;
            }
            protected string GetDisplayValue(string value, string valSep, string nameSep, bool isNumeric)
            {
                string arg_05_0 = string.Empty;
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(valSep))
                {
                    if (string.IsNullOrEmpty(nameSep))
                    {
                        return string.Join(base["CamlOp_Or", new object[0]], value.Split(new string[]
						{
							valSep
						}, StringSplitOptions.RemoveEmptyEntries));
                    }
                    string[] array = value.Split(new string[]
					{
						nameSep
					}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i];
                        int num;
                        if ((num = text.IndexOf(valSep)) > 0)
                        {
                            list.Add(text.Substring(0, num) + ":'" + text.Substring(num + valSep.Length) + "'");
                        }
                        else
                        {
                            list.Add(base.Name + ":'" + text.Substring((num == 0) ? valSep.Length : 0) + "'");
                        }
                    }
                    return string.Join(base["CamlOp_And", new object[0]], list.ToArray());
                }
                else
                {
                    if (!isNumeric)
                    {
                        return value;
                    }
                    return this.GetNumeric(value);
                }
            }
            protected string GetDisplayValue(string value)
            {
                return this.GetDisplayValue(value, this.Get<string>("MultiValueSeparator"), this.Get<string>("MultiFilterSeparator"), this.IsNumeric);
            }
            protected internal string GetFilterValue(string formKey, string defaultValue)
            {
                List<string> filterValues = this.GetFilterValues(formKey, defaultValue);
                if (filterValues.Count != 0)
                {
                    return filterValues[0];
                }
                return defaultValue;
            }
            protected internal virtual List<string> GetFilterValues(string formKey, string defaultValue)
            {
                SPContext context = ProductPage.GetContext();
                bool flag = string.IsNullOrEmpty(ProductPage.Config(context, "RememberStorage")) || ProductPage.Config(context, "RememberStorage") == "both" || ProductPage.Config(context, "RememberStorage") == "cookies";
                bool flag2 = string.IsNullOrEmpty(ProductPage.Config(context, "RememberStorage")) || ProductPage.Config(context, "RememberStorage") == "both" || ProductPage.Config(context, "RememberStorage") == "spweb";
                HttpContext context2 = base.Context;
                string text = "roxfz_" + (ProductPage.Config<bool>(context, "RememberByNameOnly") ? base.Name : (base.ID + "_" + ((this.parentWebPart == null) ? string.Empty : (this.parentWebPart.ID + "_"))));
                string key = text + ((context.Web.CurrentUser == null) ? null : context.Web.CurrentUser.LoginName.ToLowerInvariant());
                List<string> list = new List<string>();
                Guid siteID = context.Site.ID;
                Guid webID = context.Web.ID;
                if (Array.IndexOf<string>(base.Context.Request.Form.AllKeys, formKey) < 0)
                {
                    if (!(this is FilterBase.Boolean) || context2 == null || context2.Request == null || !"POST".Equals(context2.Request.HttpMethod, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (this.parentWebPart != null && string.IsNullOrEmpty(context2.Request.Form["roxact_" + this.parentWebPart.ID]) && this.parentWebPart.RememberFilterValues && (flag || flag2))
                        {
                            string json = null;
                            if (context2 != null && flag && Array.IndexOf<string>(context2.Request.Cookies.AllKeys, text) >= 0)
                            {
                                json = HttpUtility.UrlDecode(context2.Request.Cookies[text].Value);
                            }
                            else
                            {
                                if (flag2 && context.Web.AllProperties.ContainsKey(key))
                                {
                                    json = context.Web.AllProperties[key] + string.Empty;
                                }
                            }
                            object obj;
                            if (!string.IsNullOrEmpty(json) && (obj = JSON.JsonDecode(json)) != null)
                            {
                                list.Clear();
                                if (obj is ArrayList)
                                {
                                    IEnumerator enumerator = ((ArrayList)obj).GetEnumerator();
                                    try
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            object current = enumerator.Current;
                                            list.Add(current + string.Empty);
                                        }
                                        goto IL_484;
                                    }
                                    finally
                                    {
                                        IDisposable disposable = enumerator as IDisposable;
                                        if (disposable != null)
                                        {
                                            disposable.Dispose();
                                        }
                                    }
                                }
                                list.Add(obj + string.Empty);
                            }
                        }
                    IL_484:
                        if (list.Count == 0)
                        {
                            if (this is FilterBase.Choice && this.AllowMultiEnter && !string.IsNullOrEmpty(defaultValue))
                            {
                                list.AddRange(defaultValue.Split(new char[]
								{
									','
								}, StringSplitOptions.RemoveEmptyEntries));
                            }
                            else
                            {
                                list.Add(defaultValue);
                            }
                        }
                        return list;
                    }
                }
                try
                {
                    string[] values;
                    if ((values = base.Context.Request.Form.GetValues(formKey)) != null)
                    {
                        list.AddRange(values);
                    }
                    if (list.Count == 0)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    list.AddRange((base.Context.Request.Form[formKey] + string.Empty).Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries));
                }
                if (list.Count == 1 && ((this.DefaultIfEmpty && !this.pickerSemantics) ? string.IsNullOrEmpty(list[0]) : (list[0] == null)))
                {
                    list.Clear();
                }
                if (list.Count == 0 && (this.DefaultIfEmpty || this.pickerSemantics))
                {
                    list.Add(defaultValue);
                }
                if (list.Count > 0 && this.parentWebPart != null && this.parentWebPart.RememberFilterValues && !string.IsNullOrEmpty(key))
                {
                    if (!flag2)
                    {
                        if (!flag)
                        {
                            return list;
                        }
                    }
                    try
                    {
                        string json = JSON.JsonEncode(list);
                        if (flag && context2 != null)
                        {
                            context2.Response.SetCookie(new HttpCookie(text, HttpUtility.UrlEncode(json)));
                        }
                        if (flag2)
                        {
                            try
                            {
                                ProductPage.Elevate(delegate
                                {
                                    using (SPSite sPSite = new SPSite(siteID))
                                    {
                                        using (SPWeb sPWeb = sPSite.OpenWeb(webID))
                                        {
                                            SPSite arg_24_0 = sPSite;
                                            bool allowUnsafeUpdates;
                                            sPWeb.AllowUnsafeUpdates=(allowUnsafeUpdates = true);
                                            arg_24_0.AllowUnsafeUpdates=(allowUnsafeUpdates);
                                            sPWeb.AllProperties[key] = json;
                                            sPWeb.Update();
                                        }
                                    }
                                }, true);
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                return list;
            }
            public string GetNumeric(string value)
            {
                string format = this.Get<string>("NumberFormat");
                bool flag = base.EffectiveNumberCulture == null;
                try
                {
                    long num;
                    if (flag ? long.TryParse(value, out num) : long.TryParse(value, NumberStyles.Any, base.EffectiveNumberCulture, out num))
                    {
                        string result = flag ? num.ToString(format) : num.ToString(format, base.EffectiveNumberCulture);
                        return result;
                    }
                    decimal num2;
                    if (flag ? decimal.TryParse(value, out num2) : decimal.TryParse(value, NumberStyles.Any, base.EffectiveNumberCulture, out num2))
                    {
                        string result = flag ? num2.ToString(format) : num2.ToString(format, base.EffectiveNumberCulture);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    if (this.parentWebPart != null)
                    {
                        this.parentWebPart.additionalWarningsErrors.Add(ex.Message);
                    }
                }
                return value;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("DefaultIfEmpty", this.DefaultIfEmpty);
                info.AddValue("IsInteractive", this.IsInteractive);
                info.AddValue("Label", this.Label);
                info.AddValue("Label2", this.Label2);
                info.AddValue("PostFilter", this.PostFilter);
                info.AddValue("CheckStyle", this.CheckStyle);
                info.AddValue("PostFilterFieldName", this.PostFilterFieldName);
                info.AddValue("PostFilterListViewUrl", this.PostFilterListViewUrl);
                info.AddValue("SendAllAsMultiValuesIfEmpty", this.SendAllAsMultiValuesIfEmpty);
                info.AddValue("AllowMultiEnter", this.AllowMultiEnter);
                if (this.supportAutoSuggest)
                {
                    info.AddValue("AutoSuggest", this.AutoSuggest);
                }
                info.AddValue("BeginGroup", this.BeginGroup);
                base.GetObjectData(info, context);
            }
            public virtual void Render(HtmlTextWriter output, bool isUpperBound)
            {
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
                string text2 = "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
                if (this.suppressInteractive)
                {
                    this.hiddenProperties.Add("Label");
                }
                else
                {
                    this.hiddenProperties.Remove("Label");
                }
                if (base.IsRange && !this.suppressInteractive)
                {
                    this.hiddenProperties.Remove("Label2");
                }
                else
                {
                    this.hiddenProperties.Add("Label2");
                }
                if (this.parentWebPart != null && this.pickerSemantics && !(this is FilterBase.CamlViewSwitch))
                {
                    if (this.suppressInteractive)
                    {
                        this.hiddenProperties.Add("AllowMultiEnter");
                    }
                    else
                    {
                        this.hiddenProperties.Remove("AllowMultiEnter");
                    }
                    this.hiddenProperties.Remove("SendAllAsMultiValuesIfEmpty");
                }
                else
                {
                    if (this.suppressInteractive)
                    {
                        this.hiddenProperties.Remove("AllowMultiEnter");
                    }
                    else
                    {
                        if (!(this is FilterBase.Multi))
                        {
                            this.hiddenProperties.Add("AllowMultiEnter");
                        }
                    }
                    this.hiddenProperties.Add("SendAllAsMultiValuesIfEmpty");
                }
                if (this is FilterBase.Boolean || this is FilterBase.Date)
                {
                    this.hiddenProperties.AddRange(new string[]
					{
						"NumberCulture",
						"NumberFormat"
					});
                }
                if (this.parentWebPart != null)
                {
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(this.reqEd) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, this.suppressInteractive || this.hiddenProperties.Contains("IsInteractive"), new object[]
					{
						"filter_IsInteractive",
						base["Prop_IsInteractive" + (this.pickerSemantics ? "Picker" : ""), new object[0]],
						base.GetChecked(this.Get<bool>("IsInteractive")),
						"jQuery('#roxboxia').css({'display':(this.checked?'block':'none')});" + (this.pickerSemantics ? "jQuery('#div_filter_PostFilter').css({'display':(this.checked?'block':'none')});jQuery('#roxboxpostfilter').css({'display':(this.checked&&document.getElementById('filter_PostFilter').checked?'block':'none')});" : string.Empty)
					}));
                    panel.Controls.Add(new LiteralControl("<fieldset id=\"roxboxia\" style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\">"));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(this.reqEd) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text, "Label", new object[]
					{
						this.Get<string>("Label")
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(this.reqEd) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text, "Label2", new object[]
					{
						this.Get<string>("Label2")
					}));
                    if (this.supportAutoSuggest)
                    {
                        panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, "AutoSuggest", new object[]
						{
							base.GetChecked(this.Get<bool>("AutoSuggest"))
						}));
                    }
                    panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", this.suppressInteractive || this.hiddenProperties.Contains("DefaultIfEmpty"), new object[]
					{
						"filter_DefaultIfEmpty",
						base["Prop_" + (this.pickerSemantics ? "AllowEmpty" : "DefaultIfEmpty"), new object[0]],
						base.GetChecked(this.Get<bool>("DefaultIfEmpty"))
					}));
                    panel.Controls.Add(base.CreateControl((this.parentWebPart.LicEd(this.reqEd) && !base.IsRange) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, "AllowMultiEnter", new object[]
					{
						base.GetChecked(this.Get<bool>("AllowMultiEnter") && !base.IsRange),
						string.Empty
					}));
                    if (this.pickerSemantics && !this.suppressInteractive)
                    {
                        panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(this.reqEd) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, "PostFilter", new object[]
						{
							base.GetChecked(this.Get<bool>("PostFilter")),
							"document.getElementById('roxboxpostfilter').style.display=(this.checked?'block':'none');"
						}));
                        panel.Controls.Add(new LiteralControl("<fieldset id=\"roxboxpostfilter\" style=\"padding: 4px; background-color: ButtonFace; color: ButtonText;\">"));
                        panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "PostFilterListViewUrl", new object[]
						{
							this.Get<string>("PostFilterListViewUrl")
						}));
                        panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "PostFilterFieldName", new object[]
						{
							this.Get<string>("PostFilterFieldName")
						}));
                        panel.Controls.Add(new LiteralControl("</fieldset>"));
                        panel.Controls.Add(base.CreateControl((this.parentWebPart.LicEd(this.reqEd) && !base.IsRange) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, "CheckStyle", new object[]
						{
							base.GetChecked(this.Get<bool>("CheckStyle") && !base.IsRange),
							string.Empty
						}));
                    }
                    panel.Controls.Add(base.CreateControl((this.parentWebPart.LicEd(this.reqEd) && !base.IsRange) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text2, "SendAllAsMultiValuesIfEmpty", new object[]
					{
						base.GetChecked(this.Get<bool>("SendAllAsMultiValuesIfEmpty") && !base.IsRange),
						string.Empty
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(this.reqEd) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text, "BeginGroup", new object[]
					{
						this.Get<string>("BeginGroup")
					}));
                    panel.Controls.Add(new LiteralControl("</fieldset>"));
                    if (!this.Get<bool>("IsInteractive"))
                    {
                        panel.Controls.Add(base.CreateScript((this.pickerSemantics ? "document.getElementById('roxboxpostfilter').style.display = " : string.Empty) + "document.getElementById('roxboxia').style.display = 'none';"));
                    }
                    if (this.pickerSemantics && !this.Get<bool>("PostFilter"))
                    {
                        panel.Controls.Add(base.CreateScript("document.getElementById('roxboxpostfilter').style.display = 'none';"));
                    }
                }
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.DefaultIfEmpty = this.Get<bool>("DefaultIfEmpty");
                this.IsInteractive = this.Get<bool>("IsInteractive");
                this.Label = this.Get<string>("Label");
                this.Label2 = this.Get<string>("Label2");
                this.CheckStyle = this.Get<bool>("CheckStyle");
                this.PostFilter = this.Get<bool>("PostFilter");
                this.PostFilterListViewUrl = this.Get<string>("PostFilterListViewUrl");
                this.PostFilterFieldName = this.Get<string>("PostFilterFieldName");
                this.SendAllAsMultiValuesIfEmpty = this.Get<bool>("SendAllAsMultiValuesIfEmpty");
                this.AllowMultiEnter = this.Get<bool>("AllowMultiEnter");
                if (this.supportAutoSuggest)
                {
                    this.AutoSuggest = this.Get<bool>("AutoSuggest");
                }
                this.BeginGroup = this.Get<string>("BeginGroup");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        public class Boolean : FilterBase.Interactive
        {
            private string boolValue = "";
            private string falseValue = "";
            private string trueValue = "1";
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    yield return new KeyValuePair<string, string>(base.Name, this.WebPartValue);
                    yield break;
                }
            }
            public string BoolValue
            {
                get
                {
                    return this.boolValue;
                }
                set
                {
                    this.boolValue = value;
                }
            }
            public string FalseValue
            {
                get
                {
                    return this.falseValue;
                }
                set
                {
                    this.falseValue = ProductPage.Trim(value, new char[0]);
                }
            }
            public string TrueValue
            {
                get
                {
                    return this.trueValue;
                }
                set
                {
                    this.trueValue = ProductPage.Trim(value, new char[0]);
                }
            }
            public override string WebPartValue
            {
                get
                {
                    string filterValue = base.GetFilterValue("filterval_" + base.ID, "POST".Equals(HttpContext.Current.Request.HttpMethod, StringComparison.InvariantCultureIgnoreCase) ? string.Empty : this.Get<string>("BoolValue"));
                    if ("on".Equals(filterValue))
                    {
                        return this.Get<string>("TrueValue");
                    }
                    if (!string.IsNullOrEmpty(filterValue) && !filterValue.Equals(this.Get<string>("FalseValue"), StringComparison.InvariantCultureIgnoreCase))
                    {
                        return this.Get<string>("TrueValue");
                    }
                    return this.Get<string>("FalseValue");
                }
            }
            public Boolean()
            {
                this.interactive = true;
                this.supportAllowMultiEnter = false;
                base.AllowMultiEnter = false;
                this.sendEmpty = true;
            }
            public Boolean(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.supportAllowMultiEnter = false;
                try
                {
                    this.BoolValue = info.GetString("BoolValue");
                    this.FalseValue = info.GetString("FalseValue");
                    this.TrueValue = info.GetString("TrueValue");
                }
                catch
                {
                }
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("BoolValue", this.BoolValue);
                info.AddValue("FalseValue", this.FalseValue);
                info.AddValue("TrueValue", this.TrueValue);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                bool flag = this.Get<string>("TrueValue").Equals(this.Get<string>("BoolValue"));
                output.Write("<input class=\"" + (flag ? "rox-check-default" : "rox-check-value") + "\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{1}{2}/><label for=\"{0}\">{3}</label>", new object[]
				{
					"filterval_" + base.ID,
					object.Equals(this.WebPartValue, this.Get<string>("TrueValue")) ? " checked=\"checked\"" : string.Empty,
					base.HtmlOnChangeAttr.Replace("onchange", "onclick"),
					this.Label
				});
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                if (!this.hiddenProperties.Contains("DefaultIfEmpty"))
                {
                    this.hiddenProperties.Add("DefaultIfEmpty");
                }
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "FalseValue", new object[]
				{
					this.Get<string>("FalseValue")
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "TrueValue", new object[]
				{
					this.Get<string>("TrueValue")
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "BoolValue", new object[]
				{
					this.Get<string>("BoolValue")
				}));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.BoolValue = this.Get<string>("BoolValue");
                this.FalseValue = this.Get<string>("FalseValue");
                this.TrueValue = this.Get<string>("TrueValue");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class CamlDistinct : FilterBase
        {
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    return null;
                }
            }
            public CamlDistinct()
            {
            }
            public CamlDistinct(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
            public override void UpdatePanel(Panel panel)
            {
                this.hiddenProperties.Add("SendEmpty");
                panel.Controls.Add(new LiteralControl("<div>" + base["CamlDistinct", new object[0]] + "</div>"));
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText; display: none; visibility: hidden;\" id=\"roxfilterspecial\">"));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
                panel.Controls.Add(new LiteralControl("<style type=\"text/css\"> fieldset#roxfilterspecial, fieldset#roxfilteradvanced, div.roxsectionlink { display: none; } </style>"));
            }
        }
        [Serializable]
        internal class Text : FilterBase.Interactive
        {
            internal bool isCamlSource;
            private string defaultValue = string.Empty;
            private string defaultValue2 = string.Empty;
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    yield return new KeyValuePair<string, string>(base.Name, this.WebPartValue);
                    if (base.IsRange)
                    {
                        yield return new KeyValuePair<string, string>(base.Name, this.WebPartValue2);
                    }
                    yield break;
                }
            }
            public string DefaultValue
            {
                get
                {
                    return this.defaultValue;
                }
                set
                {
                    this.defaultValue = ProductPage.Trim(value, new char[0]);
                }
            }
            public string DefaultValue2
            {
                get
                {
                    return this.defaultValue2;
                }
                set
                {
                    this.defaultValue2 = ProductPage.Trim(value, new char[0]);
                }
            }
            public override string WebPartValue
            {
                get
                {
                    return base.GetFilterValue("filterval_" + base.ID, this.Get<string>("DefaultValue"));
                }
            }
            public string WebPartValue2
            {
                get
                {
                    return base.GetFilterValue("filterval_" + base.ID + "2", this.Get<string>("DefaultValue2"));
                }
            }
            public Text()
            {
                base.AutoSuggest = (this.supportAutoSuggest = (this.supportRange = true));
            }
            public Text(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                try
                {
                    this.supportAutoSuggest = true;
                    this.supportRange = true;
                    this.DefaultValue = info.GetString("Value");
                    this.DefaultValue2 = info.GetString("Value2");
                }
                catch
                {
                }
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Value", this.DefaultValue);
                info.AddValue("Value2", this.DefaultValue2);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                string text = isUpperBound ? this.WebPartValue2 : this.WebPartValue;
                bool flag = base.AutoSuggest && this.parentWebPart.connectedList != null;
                int num = 40;
                if (!int.TryParse(ProductPage.Config(ProductPage.GetContext(), "AutoLimit"), out num) || num <= 1)
                {
                    num = 40;
                }
                if (base.IsNumeric)
                {
                    text = base.GetNumeric(text);
                }
                string text2;
                output.Write(string.Concat(new string[]
				{
					"<input class=\"ms-input\" type=\"text\" name=\"{0}\" ",
					flag ? string.Empty : ("onkeyup=\"roxOnKey(event, '" + this.parentWebPart.ID + "');\""),
					" id=\"{0}\" value=\"{1}\"{2} ",
					flag ? " autocomplete=\"off\" " : string.Empty,
					" />"
				}), text2 = "filterval_" + base.ID + (isUpperBound ? "2" : string.Empty), HttpUtility.HtmlEncode(text), base.HtmlOnChangeAttr);
                if (flag)
                {
                    output.WriteLine(string.Concat(new object[]
					{
						"<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { jQuery('#",
						text2,
						"').autocomplete('",
						this.parentWebPart.connectedList.ParentWeb.Url.TrimEnd(new char[]
						{
							'/'
						}),
						"/_layouts/roxority_FilterZen/jqas.aspx?f=",
						HttpUtility.UrlEncode(base.Name),
						"&fs=",
						string.Empty,
						base.MultiValueSeparator,
						"&v=",
						(this.parentWebPart.connectedView == null) ? string.Empty : ProductPage.GuidLower(this.parentWebPart.connectedView.ID, true),
						"&b=",
						(base.CamlOperator == 6) ? 1 : 0,
						"&l=",
						ProductPage.GuidLower(this.parentWebPart.connectedList.ID, true),
						"&sf=",
						HttpUtility.UrlEncode(this.parentWebPart.AcSecFields),
						"', { \"max\": ",
						num,
						", \"delay\": 100, \"minChars\": 1, \"selectFirst\": false, \"multiple\": ",
						string.IsNullOrEmpty(base.MultiValueSeparator) ? "false" : "true",
						", \"multipleSeparator\": \"",
						base.MultiValueSeparator,
						"\", \"matchContains\": ",
						(base.CamlOperator != 0) ? "true" : "false",
						" }); }); </script>"
					}));
                }
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                if (base.IsRange)
                {
                    this.hiddenProperties.Remove("DefaultValue2");
                }
                else
                {
                    this.hiddenProperties.Add("DefaultValue2");
                }
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "DefaultValue", new object[]
				{
					this.Get<string>("DefaultValue")
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "DefaultValue2", new object[]
				{
					this.Get<string>("DefaultValue2")
				}));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.DefaultValue = this.Get<string>("DefaultValue");
                this.DefaultValue2 = this.Get<string>("DefaultValue2");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class CamlSource : FilterBase.Text
        {
            public CamlSource()
            {
                this.isCamlSource = true;
            }
            public CamlSource(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.isCamlSource = true;
            }
        }
        [Serializable]
        internal class Choice : FilterBase.Interactive
        {
            private const string SCRIPT_REPOPULATE = "repopulateList('filter_DefaultChoice', 'filter_Choices', '{0}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');repopulateList('filter_DefaultChoice2', 'filter_Choices', '{1}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');";
            private static readonly string scriptCheckDefault = "setTimeout('var roxtmp = document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%2\\'); if (document.getElementById(\\'filter_DefaultIfEmpty\\').disabled = ((document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%\\').selectedIndex == 0) || (roxtmp && (roxtmp.selectedIndex == 0)))) { document.getElementById(\\'label_filter_DefaultIfEmpty\\').style.textDecoration = \\'none\\'; document.getElementById(\\'filter_DefaultIfEmpty\\').checked = true; }', 150);".Replace("%%PLACEHOLDER_LISTID%%", "DefaultChoice");
            internal string[] choices = new string[0];
            internal string[] postChoices;
            internal bool choicesDisabled;
            private string defaultChoice = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            private string defaultChoice2 = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            protected internal override IEnumerable<string> AllPickableValues
            {
                get
                {
                    string text = base.Context.Request["filter_Choices"];
                    List<string> autoChoices = this.AutoChoices;
                    return new List<string>(string.IsNullOrEmpty(text) ? ((autoChoices == null) ? this.Choices : autoChoices.ToArray()) : text.Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            protected internal virtual List<string> AutoChoices
            {
                get
                {
                    return null;
                }
            }
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    string[] array = (this.postChoices == null) ? ((List<string>)this.AllPickableValues).ToArray() : this.postChoices;
                    List<string> autoChoices = this.AutoChoices;
                    if (autoChoices != null)
                    {
                        array = autoChoices.ToArray();
                    }
                    foreach (string current in this.GetFilterValues("filterval_" + base.ID, this.Get<string>("DefaultChoice")))
                    {
                        if (string.IsNullOrEmpty(current) || current.Equals("0478f8f9-fbdc-42f5-99ea-f6e8ec702606") || Array.IndexOf<string>(array, current) >= 0)
                        {
                            int length;
                            yield return new KeyValuePair<string, string>(base.Name, "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(current) ? string.Empty : (((length = current.IndexOf(";#")) > 0) ? current.Substring(0, length) : current));
                            if (base.IsRange)
                            {
                                break;
                            }
                        }
                    }
                    if (base.IsRange)
                    {
                        foreach (string current2 in this.GetFilterValues("filterval_" + base.ID + 2, this.Get<string>("DefaultChoice2")))
                        {
                            if (string.IsNullOrEmpty(current2) || current2.Equals("0478f8f9-fbdc-42f5-99ea-f6e8ec702606") || Array.IndexOf<string>(array, current2) >= 0)
                            {
                                int length;
                                yield return new KeyValuePair<string, string>(base.Name, "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(current2) ? string.Empty : (((length = current2.IndexOf(";#")) > 0) ? current2.Substring(0, length) : current2));
                                if (base.IsRange)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    yield break;
                }
            }
            public string[] Choices
            {
                get
                {
                    List<string> autoChoices = this.AutoChoices;
                    if (autoChoices != null)
                    {
                        return autoChoices.ToArray();
                    }
                    return this.choices;
                }
                set
                {
                    List<string> list = new List<string>((value == null) ? new string[0] : value);
                    if (this.AutoChoices == null)
                    {
                        list = list.ConvertAll<string>((string val) => ProductPage.Trim(val, new char[0]));
                        while (list.IndexOf(string.Empty) >= 0)
                        {
                            list.Remove(string.Empty);
                        }
                        ProductPage.RemoveDuplicates<string>(list);
                        this.choices = list.ToArray();
                    }
                }
            }
            public string DefaultChoice
            {
                get
                {
                    return this.defaultChoice;
                }
                set
                {
                    this.defaultChoice = value;
                }
            }
            public string DefaultChoice2
            {
                get
                {
                    return this.defaultChoice2;
                }
                set
                {
                    this.defaultChoice2 = value;
                }
            }
            public override bool DefaultIfEmpty
            {
                get
                {
                    string value = this.Get<string>("DefaultChoice");
                    return base.DefaultIfEmpty || string.IsNullOrEmpty(value) || "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(value);
                }
                set
                {
                    string value2 = this.Get<string>("DefaultChoice");
                    base.DefaultIfEmpty = (value || string.IsNullOrEmpty(value2) || "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(value2));
                }
            }
            public override string WebPartValue
            {
                get
                {
                    string text = this.Get<string>("DefaultChoice");
                    string text2 = base.Context.Request["filter_Choices"];
                    if (Array.IndexOf<string>(string.IsNullOrEmpty(text2) ? this.Choices : text2.Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries), text) >= 0)
                    {
                        if (!"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(text))
                        {
                            return text;
                        }
                        return string.Empty;
                    }
                    else
                    {
                        if (!"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(base.WebPartValue))
                        {
                            return base.WebPartValue;
                        }
                        return string.Empty;
                    }
                }
            }
            public string WebPartValue2
            {
                get
                {
                    string text = this.Get<string>("DefaultChoice2");
                    string text2 = base.Context.Request["filter_Choices"];
                    if (Array.IndexOf<string>(string.IsNullOrEmpty(text2) ? this.Choices : text2.Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries), text) >= 0)
                    {
                        if (!"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(text))
                        {
                            return text;
                        }
                        return string.Empty;
                    }
                    else
                    {
                        if (!"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(base.WebPartValue))
                        {
                            return base.WebPartValue;
                        }
                        return string.Empty;
                    }
                }
            }
            public Choice()
            {
                this.pickerSemantics = (this.defaultIfEmpty = (this.supportRange = true));
            }
            public Choice(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.supportRange = (this.pickerSemantics = true);
                try
                {
                    this.DefaultChoice = info.GetString("DefaultChoice");
                    this.DefaultIfEmpty = info.GetBoolean("DefaultIfEmpty");
                    if (!(this is FilterBase.CamlViewSwitch))
                    {
                        this.Choices = (info.GetValue("Choices", typeof(string[])) as string[]);
                    }
                    this.DefaultChoice2 = info.GetString("DefaultChoice2");
                }
                catch
                {
                }
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (this.AutoChoices == null)
                {
                    info.AddValue("Choices", this.choices, typeof(string[]));
                }
                info.AddValue("DefaultChoice", this.DefaultChoice);
                info.AddValue("DefaultChoice2", this.DefaultChoice2);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                string text = string.Empty;
                string text2 = "filterval_" + base.ID + (isUpperBound ? "2" : string.Empty);
                string name = isUpperBound ? "DefaultChoice2" : "DefaultChoice";
                string[] array = (this.postChoices == null) ? ((List<string>)this.AllPickableValues).ToArray() : this.postChoices;
                int num = 0;
                bool flag = this.Get<bool>("CheckStyle");
                bool flag2 = false;
                bool flag3 = false;
                List<string> filterValues = this.GetFilterValues(text2, this.Get<string>(name).ToString());
                List<string> autoChoices = this.AutoChoices;
                if (autoChoices != null)
                {
                    array = autoChoices.ToArray();
                }
                if (filterValues.Count > 0 && (string.IsNullOrEmpty(filterValues[0]) || filterValues[0].Equals("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")))
                {
                    filterValues.Clear();
                }
                if (this.postChoices == null)
                {
                    if (this.Get<bool>("PostFilter"))
                    {
                        array = (this.postChoices = base.PostFilterChoices(array).Key);
                    }
                    if (this.Cascade)
                    {
                        array = (this.postChoices = base.PostFilterChoices(this.parentWebPart.connectedList, (this.parentWebPart.connectedView == null) ? this.parentWebPart.connectedList.DefaultView : this.parentWebPart.connectedView, base.Name.StartsWith("@") ? base.Name.Substring(1) : base.Name, array, true).Key);
                    }
                }
                if (this.Get<bool>("DefaultIfEmpty"))
                {
                    output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxMultiMins['filterval_" + base.ID + "'] = '0478f8f9-fbdc-42f5-99ea-f6e8ec702606'; </script>");
                    if (!flag)
                    {
                        text += string.Format("<option value=\"{0}\"{2}>{1}</option>", "0478f8f9-fbdc-42f5-99ea-f6e8ec702606", base["Empty" + ((this is FilterBase.CamlViewSwitch) ? "Cur" : (this.Get<bool>("SendEmpty") ? "None" : "All")), new object[0]], (filterValues.Count == 0 || (filterValues.Count == 1 && (filterValues[0] == "0478f8f9-fbdc-42f5-99ea-f6e8ec702606" || filterValues[0] == string.Empty))) ? " selected=\"selected\"" : string.Empty);
                    }
                    else
                    {
                        text += string.Format(string.Concat(new string[]
						{
							"<span><input class=\"rox-check-default\" name=\"",
							text2,
							"\" type=\"",
							base.AllowMultiEnter ? "checkbox" : "radio",
							"\" id=\"empty_",
							text2,
							"\" value=\"{1}\" {3}",
							string.IsNullOrEmpty(base.HtmlOnChangeAttr) ? (" onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);"),
							"/><label for=\"empty_",
							text2,
							"\">{2}</label></span>"
						}), new object[]
						{
							ProductPage.GuidLower(Guid.NewGuid()),
							"0478f8f9-fbdc-42f5-99ea-f6e8ec702606",
							base["Empty" + ((this is FilterBase.CamlViewSwitch) ? "Cur" : (this.Get<bool>("SendEmpty") ? "None" : "All")), new object[0]],
							(filterValues.Count == 0 || (filterValues.Count == 1 && (filterValues[0] == "0478f8f9-fbdc-42f5-99ea-f6e8ec702606" || filterValues[0] == string.Empty))) ? " checked=\"checked\"" : string.Empty
						});
                    }
                }
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text3 = array2[i];
                    num++;
                    int num2;
                    string item;
                    string text4;
                    if ((num2 = text3.IndexOf(";#")) > 0)
                    {
                        item = text3.Substring(0, num2);
                        text4 = text3.Substring(num2 + 2);
                    }
                    else
                    {
                        item = (text4 = text3);
                    }
                    flag3 = false;
                    if (flag2 = ((num2 = text4.IndexOf("[[")) > 0 && text4.IndexOf("]]") > num2 + 4 && text4.IndexOf(':', num2) > num2 + 2))
                    {
                        try
                        {
                            flag3 = text4.Substring(text4.IndexOf(':', num2) + 1, text4.IndexOf("]]") - text4.IndexOf(':', num2) - 1).Equals(base.ResolveValue(text4.Substring(num2 + 2, text4.IndexOf(':', num2) - (num2 + 2))));
                            text4 = text4.Substring(0, num2);
                        }
                        catch
                        {
                        }
                    }
                    if (!flag2 || flag3)
                    {
                        if (flag)
                        {
                            text += string.Format(string.Concat(new string[]
							{
								"<span><input class=\"chk-",
								base.ID,
								" rox-check-value\" name=\"",
								text2,
								"\" type=\"",
								base.AllowMultiEnter ? "checkbox" : "radio",
								"\" id=\"x{0}\" value=\"{1}\" {3}",
								(string.IsNullOrEmpty(base.HtmlOnChangeAttr) && this.Get<bool>("DefaultIfEmpty")) ? (" onclick=\"document.getElementById('empty_" + text2 + "').checked=false;\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"" + (this.Get<bool>("DefaultIfEmpty") ? ("document.getElementById('empty_" + text2 + "').checked=false;") : string.Empty)),
								"/><label for=\"x{0}\">{2}</label></span>"
							}), new object[]
							{
								ProductPage.GuidLower(Guid.NewGuid()),
								text3,
								base.GetDisplayValue(text4),
								filterValues.Contains(item) ? " checked=\"checked\"" : string.Empty
							});
                        }
                        else
                        {
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", text3, base.GetDisplayValue(text4), filterValues.Contains(text3) ? " selected=\"selected\"" : string.Empty);
                        }
                        if (base.PickerLimit != 0 && num >= base.PickerLimit)
                        {
                            break;
                        }
                    }
                }
                if (flag)
                {
                    output.Write("<div>" + text + "</div>");
                }
                else
                {
                    output.Write(string.Concat(new string[]
					{
						"<select",
						base.AllowMultiEnter ? " size=\"1\" multiple=\"multiple\" class=\"rox-multiselect ms-input\"" : " class=\"ms-input\"",
						" name=\"{0}\" id=\"{0}\"{1}>",
						text,
						"</select>"
					}), text2, base.AllowMultiEnter ? base.HtmlOnChangeMultiAttr : base.HtmlOnChangeAttr);
                }
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = base.Context.Request["filter_Choices"];
                string[] array = string.IsNullOrEmpty(text) ? this.Choices : text.Split(new char[]
				{
					'\r',
					'\n'
				}, StringSplitOptions.RemoveEmptyEntries);
                List<string> autoChoices = this.AutoChoices;
                if (autoChoices != null)
                {
                    array = autoChoices.ToArray();
                }
                if (base.IsRange)
                {
                    this.hiddenProperties.Remove("DefaultChoice2");
                }
                else
                {
                    this.hiddenProperties.Add("DefaultChoice2");
                }
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                panel.Controls.Add(base.CreateControl((autoChoices != null) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ") : "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>", "Choices", new object[]
				{
					(array.Length == 0) ? string.Empty : string.Join("\n", array),
					(array.Length < 4) ? 4 : array.Length,
					string.Format("repopulateList('filter_DefaultChoice', 'filter_Choices', '{0}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');repopulateList('filter_DefaultChoice2', 'filter_Choices', '{1}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');", "0478f8f9-fbdc-42f5-99ea-f6e8ec702606", "0478f8f9-fbdc-42f5-99ea-f6e8ec702606")
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>", "DefaultChoice", new object[]
				{
					" onchange=\"" + FilterBase.Choice.scriptCheckDefault + "\"" + (this.Get<bool>("AllowMultiEnter") ? (" multiple=\"multiple\" size=\"" + array.Length + "\"") : string.Empty)
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>", "DefaultChoice2", new object[]
				{
					" onchange=\"" + FilterBase.Choice.scriptCheckDefault + "\"" + (this.Get<bool>("AllowMultiEnter") ? (" multiple=\"multiple\" size=\"" + array.Length + "\"") : string.Empty)
				}));
                panel.Controls.Add(base.CreateScript(string.Format("repopulateList('filter_DefaultChoice', 'filter_Choices', '{0}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');repopulateList('filter_DefaultChoice2', 'filter_Choices', '{1}', '0478f8f9-fbdc-42f5-99ea-f6e8ec702606');", this.Get<string>("DefaultChoice"), this.Get<string>("DefaultChoice2")) + FilterBase.Choice.scriptCheckDefault));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                if (this.AutoChoices == null)
                {
                    this.Choices = this.Get<string>("Choices").Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries);
                }
                this.DefaultChoice = this.Get<string>("DefaultChoice");
                this.DefaultChoice2 = this.Get<string>("DefaultChoice2");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class CamlViewSwitch : FilterBase.Choice
        {
            private List<string> empty = new List<string>();
            private List<string> views;
            private string[] excludeViews;
            protected internal override List<string> AutoChoices
            {
                get
                {
                    if (this.views == null && this.parentWebPart != null && this.parentWebPart.connectedList != null)
                    {
                        this.views = new List<string>();
                        foreach (SPView view in ProductPage.TryEach<SPView>(this.parentWebPart.connectedList.Views))
                        {
                            if (!string.IsNullOrEmpty(view.Title) && !view.PersonalView && !"assetLibTemp".Equals(view.Title) && !Array.Exists<string>(this.ExcludeViews, (string urlPart) => view.Url.IndexOf(urlPart, StringComparison.InvariantCultureIgnoreCase) > 0))
                            {
                                this.views.Add(view.Url + ";#" + view.Title);
                            }
                        }
                    }
                    if (this.views != null)
                    {
                        return this.views;
                    }
                    return this.empty;
                }
            }
            public override bool Cascade
            {
                get
                {
                    return false;
                }
            }
            public string[] ExcludeViews
            {
                get
                {
                    if (this.excludeViews == null)
                    {
                        this.excludeViews = ProductPage.Config(ProductPage.GetContext(), "ExcludeViews").Split(new char[]
						{
							'\r',
							'\n'
						}, StringSplitOptions.RemoveEmptyEntries);
                    }
                    return this.excludeViews;
                }
            }
            protected internal override bool SupportsMultipleValues
            {
                get
                {
                    return false;
                }
            }
            public CamlViewSwitch()
            {
                this.reqEd = 4;
                this.name = "_roxListView";
                this.label = base["CamlViewSwitch", new object[0]];
                this.interactive = true;
                this.supportAllowMultiEnter = false;
            }
            public CamlViewSwitch(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.reqEd = 4;
                this.supportAllowMultiEnter = false;
            }
            public override void UpdatePanel(Panel panel)
            {
                this.hiddenProperties.AddRange(new string[]
				{
					"AllowMultiEnter",
					"Groups",
					"CamlOperator",
					"SuppressMode",
					"MultiFilterSeparator",
					"MultiValueSeparator",
					"SuppressMultiValues",
					"FallbackValue",
					"NumberFormat",
					"NumberCulture",
					"SendEmpty",
					"SendAllAsMultiValuesIfEmpty",
					"PostFilter"
				});
                panel.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> roxEmpty = '" + SPEncode.ScriptEncode(base["EmptyCur", new object[0]]) + "'; </script>"));
                base.UpdatePanel(panel);
            }
        }
        [Serializable]
        internal sealed class Date : FilterBase.Interactive
        {
            private CultureInfo dtCulture;
            private long absoluteDefaultValue;
            private long absoluteDefaultValue2;
            private string dateCulture = string.Empty;
            private string dateFormat = string.Empty;
            private string dateFilter = string.Empty;
            private string dateFilter2 = string.Empty;
            private int relativeOffset;
            private bool relativeOffsetForDefaultOnly = true;
            internal DateTime AbsoluteDate
            {
                get
                {
                    long num = this.Get<long>("AbsoluteDefaultValue");
                    if (num >= 0L)
                    {
                        return new DateTime(num);
                    }
                    return ProductPage.ConvertStringToDate(base.ResolveValue(this.DefaultValue.Substring(2, this.DefaultValue.Length - 4)), this.EffectiveDateCulture);
                }
                set
                {
                    this.AbsoluteDefaultValue = value.Ticks;
                }
            }
            internal DateTime AbsoluteDate2
            {
                get
                {
                    long num = this.Get<long>("AbsoluteDefaultValue2");
                    if (num >= 0L)
                    {
                        return new DateTime(num);
                    }
                    return ProductPage.ConvertStringToDate(base.ResolveValue(this.DefaultValue2.Substring(2, this.DefaultValue2.Length - 4)), this.EffectiveDateCulture);
                }
                set
                {
                    this.AbsoluteDefaultValue2 = value.Ticks;
                }
            }
            internal string AbsoluteDateValue
            {
                get
                {
                    return ProductPage.ConvertDateToString(this.AbsoluteDate, this.DateFormat, this.EffectiveDateCulture);
                }
                set
                {
                    this.AbsoluteDefaultValue = this.GetDateSetValue(value);
                }
            }
            internal string AbsoluteDateValue2
            {
                get
                {
                    return ProductPage.ConvertDateToString(this.AbsoluteDate2, this.DateFormat, this.EffectiveDateCulture);
                }
                set
                {
                    this.AbsoluteDefaultValue2 = this.GetDateSetValue(value);
                }
            }
            internal string DefaultValue
            {
                get
                {
                    return this.GetDateGetValue(false);
                }
            }
            internal string DefaultValue2
            {
                get
                {
                    return this.GetDateGetValue(true);
                }
            }
            internal CultureInfo EffectiveDateCulture
            {
                get
                {
                    string text = this.Get<string>("DateCulture");
                    if (this.dtCulture == null && !string.IsNullOrEmpty(text))
                    {
                        try
                        {
                            int culture;
                            if (int.TryParse(text, out culture))
                            {
                                this.dtCulture = new CultureInfo(culture);
                            }
                            else
                            {
                                this.dtCulture = new CultureInfo(text);
                            }
                        }
                        catch
                        {
                            this.dtCulture = null;
                        }
                    }
                    return this.dtCulture;
                }
            }
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    bool flag = false;
                    string text = this.GetFilterValue(out flag, false);
                    if (text.StartsWith("{$") && text.EndsWith("$}") && (text = base.ResolveValue(text.Substring(2, text.Length - 4))) == null)
                    {
                        text = string.Empty;
                    }
                    DateTime dateTime = ProductPage.ConvertStringToDate(text, this.EffectiveDateCulture);
                    yield return new KeyValuePair<string, string>(base.Name, string.IsNullOrEmpty(text) ? text : ProductPage.ConvertDateToString(dateTime.AddDays((double)((!this.Get<bool>("RelativeOffsetForDefaultOnly") || flag) ? this.Get<int>("RelativeOffset") : 0)), this.DateFormat, this.EffectiveDateCulture));
                    if (base.IsRange)
                    {
                        if ((text = this.GetFilterValue(out flag, true)).StartsWith("{$") && text.EndsWith("$}") && (text = base.ResolveValue(text.Substring(2, text.Length - 4))) == null)
                        {
                            text = string.Empty;
                        }
                        dateTime = ProductPage.ConvertStringToDate(text, this.EffectiveDateCulture);
                        yield return new KeyValuePair<string, string>(base.Name, string.IsNullOrEmpty(text) ? text : ProductPage.ConvertDateToString(dateTime.AddDays((double)((!this.Get<bool>("RelativeOffsetForDefaultOnly") || flag) ? this.Get<int>("RelativeOffset") : 0)), this.DateFormat, this.EffectiveDateCulture));
                    }
                    yield break;
                }
            }
            public long AbsoluteDefaultValue
            {
                get
                {
                    return this.absoluteDefaultValue;
                }
                set
                {
                    this.absoluteDefaultValue = value;
                }
            }
            public long AbsoluteDefaultValue2
            {
                get
                {
                    return this.absoluteDefaultValue2;
                }
                set
                {
                    this.absoluteDefaultValue2 = value;
                }
            }
            public string DateFormat
            {
                get
                {
                    return this.dateFormat;
                }
                set
                {
                    this.dateFormat = value;
                }
            }
            public string DateCulture
            {
                get
                {
                    return this.dateCulture;
                }
                set
                {
                    this.dtCulture = null;
                    value = ProductPage.Trim(value, new char[0]);
                    try
                    {
                        int culture;
                        if (int.TryParse(value, out culture))
                        {
                            new CultureInfo(culture);
                        }
                        else
                        {
                            new CultureInfo(value);
                        }
                        this.dateCulture = value;
                    }
                    catch
                    {
                        this.dateCulture = string.Empty;
                    }
                }
            }
            public int RelativeOffset
            {
                get
                {
                    return this.relativeOffset;
                }
                set
                {
                    this.relativeOffset = value;
                }
            }
            public bool RelativeOffsetForDefaultOnly
            {
                get
                {
                    return this.relativeOffsetForDefaultOnly;
                }
                set
                {
                    this.relativeOffsetForDefaultOnly = value;
                }
            }
            public Date()
            {
                this.supportRange = true;
            }
            public Date(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.supportRange = true;
                try
                {
                    this.AbsoluteDefaultValue = info.GetInt64("AbsoluteDefaultValue");
                    this.RelativeOffset = info.GetInt32("RelativeOffset");
                    this.DateCulture = info.GetString("DateCulture");
                    this.DateFormat = info.GetString("DateFormat");
                    this.RelativeOffsetForDefaultOnly = info.GetBoolean("RelativeOffsetForDefaultOnly");
                    this.dateFilter = info.GetString("dateFilter");
                    this.dateFilter2 = info.GetString("dateFilter2");
                    this.AbsoluteDefaultValue2 = info.GetInt64("AbsoluteDefaultValue2");
                }
                catch
                {
                }
            }
            internal string GetDateGetString(long absDefVal)
            {
                long num = DateTime.MaxValue.Ticks - absDefVal;
                long num2 = num;
                if (num2 <= 6L && num2 >= 1L)
                {
                    switch ((int)(num2 - 1L))
                    {
                        case 0:
                            return "lastmonth_firstday";
                        case 1:
                            return "lastmonth_lastday";
                        case 2:
                            return "thismonth_firstday";
                        case 3:
                            return "thismonth_lastday";
                        case 4:
                            return "nextmonth_firstday";
                        case 5:
                            return "nextmonth_lastday";
                    }
                }
                return "today";
            }
            internal string GetDateGetValue(bool is2)
            {
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(0, false, false), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 1L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(-1, true, false), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 2L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(-1, false, true), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 3L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(0, true, false), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 4L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(0, false, true), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 5L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(1, true, false), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == DateTime.MaxValue.Ticks - 6L)
                {
                    return ProductPage.ConvertDateToString(this.GetDateSpecialValue(1, false, true), this.DateFormat, this.EffectiveDateCulture);
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == 0L)
                {
                    return string.Empty;
                }
                if (this.Get<long>("AbsoluteDefaultValue" + (is2 ? "2" : string.Empty)) == -1L)
                {
                    return this.dateFilter;
                }
                if (!is2)
                {
                    return this.AbsoluteDateValue;
                }
                return this.AbsoluteDateValue2;
            }
            internal long GetDateSetValue(string value)
            {
                value = ProductPage.Trim(value, new char[0]);
                if (string.IsNullOrEmpty(value))
                {
                    return 0L;
                }
                if (value.ToLowerInvariant().Contains("today"))
                {
                    return DateTime.MaxValue.Ticks;
                }
                if (value.ToLowerInvariant().Contains("lastmonth_firstday"))
                {
                    return DateTime.MaxValue.Ticks - 1L;
                }
                if (value.ToLowerInvariant().Contains("lastmonth_lastday"))
                {
                    return DateTime.MaxValue.Ticks - 2L;
                }
                if (value.ToLowerInvariant().Contains("thismonth_firstday"))
                {
                    return DateTime.MaxValue.Ticks - 3L;
                }
                if (value.ToLowerInvariant().Contains("thismonth_lastday"))
                {
                    return DateTime.MaxValue.Ticks - 4L;
                }
                if (value.ToLowerInvariant().Contains("nextmonth_firstday"))
                {
                    return DateTime.MaxValue.Ticks - 5L;
                }
                if (value.ToLowerInvariant().Contains("nextmonth_lastday"))
                {
                    return DateTime.MaxValue.Ticks - 6L;
                }
                if (value.StartsWith("{$") && value.EndsWith("$}"))
                {
                    this.dateFilter = value;
                    return -1L;
                }
                DateTime dateTime2;
                DateTime dateTime = dateTime2 = ProductPage.ConvertStringToDate(value, this.EffectiveDateCulture);
                if (dateTime2.Equals(DateTime.MaxValue))
                {
                    return 0L;
                }
                return dateTime.Ticks;
            }
            internal DateTime GetDateSpecialValue(int addMonths, bool first, bool last)
            {
                DateTime today = DateTime.Today;
                if (first)
                {
                    DateTime dateTime2;
                    DateTime dateTime = dateTime2 = today.AddMonths(addMonths);
                    return new DateTime(dateTime2.Year, dateTime.Month, 1);
                }
                if (last)
                {
                    DateTime dateTime3;
                    DateTime dateTime = dateTime3 = today.AddMonths(addMonths + 1);
                    return new DateTime(dateTime3.Year, dateTime.Month, 1).AddDays(-1.0);
                }
                return today;
            }
            internal string GetFilterValue(out bool isDefaultValue, bool isUpperBound)
            {
                string text = isUpperBound ? this.DefaultValue2 : this.DefaultValue;
                List<string> filterValues = this.GetFilterValues(string.Concat(new string[]
				{
					"$datePicker",
					base.ID,
					isUpperBound ? "2" : string.Empty,
					"$datePicker",
					base.ID,
					isUpperBound ? "2" : string.Empty,
					"Date"
				}), string.Empty);
                isDefaultValue = false;
                string[] allKeys = base.Context.Request.Form.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    string text2 = allKeys[i];
                    if (text2.EndsWith(string.Concat(new string[]
					{
						"$datePicker",
						base.ID,
						isUpperBound ? "2" : string.Empty,
						"$datePicker",
						base.ID,
						isUpperBound ? "2" : string.Empty,
						"Date"
					})))
                    {
                        string result = base.GetFilterValue(text2, text);
                        return result;
                    }
                }
                if (filterValues != null)
                {
                    foreach (string current in filterValues)
                    {
                        if (!string.IsNullOrEmpty(current))
                        {
                            string result = current;
                            return result;
                        }
                    }
                }
                isDefaultValue = true;
                return text;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("AbsoluteDefaultValue", this.AbsoluteDefaultValue);
                info.AddValue("AbsoluteDefaultValue2", this.AbsoluteDefaultValue2);
                info.AddValue("RelativeOffset", this.RelativeOffset);
                info.AddValue("DateFormat", this.DateFormat);
                info.AddValue("DateCulture", this.DateCulture);
                info.AddValue("RelativeOffsetForDefaultOnly", this.RelativeOffsetForDefaultOnly);
                info.AddValue("dateFilter", this.dateFilter);
                info.AddValue("dateFilter2", this.dateFilter2);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                int num = 0;
                DateTimeControl dateTimeControl = new DateTimeControl();
                ProductPage.InitializeDateTimePicker(dateTimeControl);
                dateTimeControl.AutoPostBack=(this.parentWebPart != null && this.parentWebPart.AutoRepost);
                dateTimeControl.ID = "datePicker" + base.ID + (isUpperBound ? "2" : string.Empty);
                foreach (KeyValuePair<string, string> current in this.FilterPairs)
                {
                    if (!string.IsNullOrEmpty(current.Value) && (!isUpperBound || num == 1))
                    {
                        dateTimeControl.SelectedDate=(ProductPage.ConvertStringToDate(current.Value, this.EffectiveDateCulture));
                        break;
                    }
                    num++;
                }
                output.Write("<span class=\"rox-ifilter-datetime\">");
                if (this.parentWebPart != null)
                {
                    this.parentWebPart.Controls.Add(dateTimeControl);
                }
                dateTimeControl.RenderControl(output);
                if (this.parentWebPart != null)
                {
                    this.parentWebPart.Controls.Remove(dateTimeControl);
                }
                output.Write("</span>");
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                DateTimeControl dateTimeControl = new DateTimeControl();
                DateTimeControl dateTimeControl2 = new DateTimeControl();
                string text = null;
                string text2 = null;
                ProductPage.InitializeDateTimePicker(dateTimeControl);
                ProductPage.InitializeDateTimePicker(dateTimeControl2);
                string[] allKeys = base.Context.Request.Form.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    string text3 = allKeys[i];
                    if (text3.EndsWith(((this.parentWebPart == null) ? string.Empty : this.parentWebPart.ID) + "$datePicker$datePickerDate"))
                    {
                        text = base.Context.Request[text3];
                    }
                    else
                    {
                        if (text3.EndsWith(((this.parentWebPart == null) ? string.Empty : this.parentWebPart.ID) + "$datePicker2$datePicker2Date"))
                        {
                            text2 = base.Context.Request[text3];
                        }
                    }
                }
                dateTimeControl.ID = "datePicker";
                dateTimeControl2.ID = "datePicker2";
                if (text == null && this.AbsoluteDefaultValue > 0L && this.AbsoluteDefaultValue != DateTime.MaxValue.Ticks)
                {
                    dateTimeControl.SelectedDate=(this.AbsoluteDate);
                }
                if (text2 == null && this.AbsoluteDefaultValue2 > 0L && this.AbsoluteDefaultValue2 != DateTime.MaxValue.Ticks)
                {
                    dateTimeControl2.SelectedDate=(this.AbsoluteDate2);
                }
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "AbsoluteDateTime", new object[0]));
                panel.Controls.Add(dateTimeControl);
                if (base.IsRange)
                {
                    panel.Controls.Add(dateTimeControl2);
                }
                panel.Controls.Add(new LiteralControl("</div>"));
                panel.Controls.Add(new LiteralControl(string.Format("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div></div>", "filter_RelativeOffset", base["Prop_RelativeOffset", new object[]
				{
					string.Format("<input style=\"width: 32px; text-align: center;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\"/>", "filter_RelativeOffset", this.Get<int>("RelativeOffset"))
				}])));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "RelativeOffsetForDefaultOnly", new object[]
				{
					base.GetChecked(this.Get<bool>("RelativeOffsetForDefaultOnly"))
				}));
                if (text != null || this.AbsoluteDefaultValue >= DateTime.MaxValue.Ticks - 6L || this.AbsoluteDefaultValue < 0L)
                {
                    panel.Controls.Add(base.CreateScript(string.Concat(new string[]
					{
						"jQuery('#",
						dateTimeControl.ClientID,
						"_datePickerDate').val('",
						(text != null) ? text : ((this.AbsoluteDefaultValue < 0L) ? this.dateFilter : this.GetDateGetString(this.AbsoluteDefaultValue)),
						"');"
					})));
                }
                if (text2 != null || this.AbsoluteDefaultValue2 >= DateTime.MaxValue.Ticks - 6L || this.AbsoluteDefaultValue2 < 0L)
                {
                    panel.Controls.Add(base.CreateScript(string.Concat(new string[]
					{
						"jQuery('#",
						dateTimeControl2.ClientID,
						"_datePicker2Date').val('",
						(text2 != null) ? text2 : ((this.AbsoluteDefaultValue2 < 0L) ? this.dateFilter2 : this.GetDateGetString(this.AbsoluteDefaultValue2)),
						"');"
					})));
                }
                panel.Controls.Add(new LiteralControl("<br/>"));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "DateCulture", new object[]
				{
					this.Get<string>("DateCulture")
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "DateFormat", new object[]
				{
					this.Get<string>("DateFormat")
				}));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.RelativeOffset = this.Get<int>("RelativeOffset");
                this.RelativeOffsetForDefaultOnly = this.Get<bool>("RelativeOffsetForDefaultOnly");
                string[] allKeys = base.Context.Request.Form.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    string text = allKeys[i];
                    if (text.EndsWith(((this.parentWebPart == null) ? string.Empty : this.parentWebPart.ID) + "$datePicker$datePickerDate"))
                    {
                        this.AbsoluteDateValue = base.Context.Request[text];
                    }
                    else
                    {
                        if (text.EndsWith(((this.parentWebPart == null) ? string.Empty : this.parentWebPart.ID) + "$datePicker2$datePicker2Date"))
                        {
                            this.AbsoluteDateValue2 = base.Context.Request[text];
                        }
                    }
                }
                this.DateCulture = this.Get<string>("DateCulture");
                this.DateFormat = this.Get<string>("DateFormat");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        public class Favorites : FilterBase.Interactive
        {
            protected internal override bool SupportsMultipleValues
            {
                get
                {
                    return false;
                }
            }
            public override bool IsInteractive
            {
                get
                {
                    return true;
                }
                set
                {
                    base.IsInteractive = true;
                }
            }
            public Favorites()
            {
                this.Init();
            }
            public Favorites(SerializationInfo info, StreamingContext ctx)
                : base(info, ctx)
            {
                this.Init();
            }
            internal void Init()
            {
                this.supportAutoSuggest = (this.supportRange = (this.supportAllowMultiEnter = false));
                this.interactive = true;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                output.Write("<select></select>");
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                this.hiddenProperties.AddRange(new string[]
				{
					"IsInteractive",
					"SendEmpty",
					"DefaultIfEmpty"
				});
                base.UpdatePanel(panel);
                panel.Controls.Add(new LiteralControl("<style type=\"text/css\"> fieldset#roxfilteradvanced, div.roxsectionlink { display: none; } </style>"));
            }
            public override void UpdateProperties(Panel panel)
            {
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class Lookup : FilterBase.Interactive
        {
            public enum ItemSort
            {
                None,
                TitleAlphaAsc,
                TitleAlphaDesc,
                TitleCountAsc,
                TitleCountDesc,
                ValueAlphaAsc,
                ValueAlphaDesc,
                ValueCountAsc,
                ValueCountDesc
            }
            private static readonly string scriptCheckDefault = "setTimeout('var roxtmp = document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%2\\'); if (document.getElementById(\\'filter_DefaultIfEmpty\\').disabled = ((document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%\\').selectedIndex == 0) || (roxtmp && (roxtmp.selectedIndex == 0)))) { document.getElementById(\\'label_filter_DefaultIfEmpty\\').style.textDecoration = \\'none\\'; document.getElementById(\\'filter_DefaultIfEmpty\\').checked = true; }', 150);".Replace("%%PLACEHOLDER_LISTID%%", "ItemID");
            protected internal bool fallbackTitles;
            protected internal bool stripID = true;
            protected internal int itemID;
            private string displayFieldName = string.Empty;
            private string filterCaml = string.Empty;
            private string valueFieldName = string.Empty;
            private string listUrl = string.Empty;
            private int itemSorting;
            private bool removeDuplicateTitles;
            private bool removeDuplicateValues = true;
            private List<KeyValuePair<string, string[]>> items;
            internal IEnumerable<KeyValuePair<string, string>> Items
            {
                get
                {
                    bool flag = false;
                    bool flag2 = false;
                    string text = null;
                    string text2 = string.Empty;
                    string text3 = string.IsNullOrEmpty(this.Get<string>("ListUrl")) ? base.Context.Request.Url.ToString() : this.Get<string>("ListUrl");
                    bool flag3 = this.Get<bool>("StripID");
                    bool flag4 = this.Get<bool>("RemoveDuplicateTitles");
                    bool flag5 = this.Get<bool>("RemoveDuplicateValues");
                    bool flag6 = false;
                    bool flag7 = false;
                    SPList sPList = null;
                    SPView sPView = null;
                    SPQuery sPQuery = null;
                    SPUser sPUser = null;
                    SPField sPField = null;
                    SPField sPField2 = null;
                    Dictionary<string, int>[] countDicts = new Dictionary<string, int>[]
					{
						new Dictionary<string, int>(),
						new Dictionary<string, int>()
					};
                    FilterBase.Lookup.ItemSort itemSort = (FilterBase.Lookup.ItemSort)this.Get<int>("ItemSorting");
                    List<string> list = new List<string>(FilterBase.Interactive.baseViewFields);
                    List<string> list2 = new List<string>();
                    List<string> list3 = flag5 ? new List<string>() : null;
                    List<string> list4 = flag4 ? new List<string>() : null;
                    SPWrap<SPList> sPWrap = null;
                    SPWrap<SPList> sPWrap2 = null;
                    if (base.Name == "Company")
                    {
                        new object();
                    }
                    IEnumerable<KeyValuePair<string, string>> result;
                    try
                    {
                        if (this.items == null)
                        {
                            this.items = new List<KeyValuePair<string, string[]>>();
                            if (this.parentWebPart.CamlFilters && base.CamlOperator == 7)
                            {
                                text = this.Get<string>("MultiValueSeparator");
                                text2 = ProductPage.Config(ProductPage.GetContext(), "IndentHint");
                            }
                            try
                            {
                                sPWrap = base.GetList("ListUrl", false);
                                if ((sPList = sPWrap.Value) != null)
                                {
                                    sPView = base.GetView(sPWrap, text3);
                                    string text4;
                                    int num;
                                    if (string.IsNullOrEmpty(text4 = this.Get<string>("ValueFieldName")))
                                    {
                                        text4 = "Title";
                                    }
                                    else
                                    {
                                        if ((num = text4.IndexOf("::", StringComparison.InvariantCultureIgnoreCase)) > 0)
                                        {
                                            text = text4.Substring(num + 2);
                                            text4 = text4.Substring(0, num);
                                        }
                                    }
                                    string text5;
                                    if (!string.IsNullOrEmpty(text5 = this.Get<string>("DisplayFieldName")) && (num = text5.IndexOf("::", StringComparison.InvariantCultureIgnoreCase)) > 0)
                                    {
                                        text5.Substring(num + 2);
                                        text5 = text5.Substring(0, num);
                                    }
                                    if ((sPField2 = ProductPage.GetField(sPList, text4)) != null)
                                    {
                                        if (!list.Contains(sPField2.InternalName))
                                        {
                                            list.Add(sPField2.InternalName);
                                        }
                                        if (flag2 = (sPField2.TypeAsString == "TaxonomyFieldType" || sPField2.TypeAsString == "TaxonomyFieldTypeMulti"))
                                        {
                                            text = ";";
                                        }
                                    }
                                    if ((sPField = (string.IsNullOrEmpty(text5) ? sPField2 : ProductPage.GetField(sPList, text5))) != null)
                                    {
                                        if (!list.Contains(sPField.InternalName))
                                        {
                                            list.Add(sPField.InternalName);
                                        }
                                        flag = (sPField.TypeAsString == "TaxonomyFieldType" || sPField.TypeAsString == "TaxonomyFieldTypeMulti");
                                    }
                                    string queryViewFields = base.CreateCamlViewFields(list.ToArray());
                                    sPQuery = base.CreateQuery(sPView, queryViewFields, this.Get<string>("FilterCaml"));
                                    sPQuery.ViewAttributes=("Scope=\"RecursiveAll\"");
                                }
                            }
                            catch (Exception ex)
                            {
                                base.Report(ex);
                            }
                            if (sPList != null && sPField != null && sPField2 != null && sPQuery != null)
                            {
                                bool flag8 = (int)sPField2.Type == 4 || (sPField2.FieldTypeDefinition != null && sPField2.FieldTypeDefinition.BaseRenderingTypeName == "DateTime");
                                SPFieldCalculated sPFieldCalculated;
                                if (((sPFieldCalculated = (sPField2 as SPFieldCalculated)) != null || (sPFieldCalculated = (sPField as SPFieldCalculated)) != null) && (int)sPFieldCalculated.OutputType == 4)
                                {
                                    flag8 = true;
                                }
                                flag6 = ((int)sPField2.Type == 7 || (sPField2.FieldTypeDefinition != null && sPField2.FieldTypeDefinition.BaseRenderingTypeName == "Lookup") || (int)sPField2.Type == 20 || (sPField2.FieldTypeDefinition != null && sPField2.FieldTypeDefinition.BaseRenderingTypeName == "User"));
                                flag7 = (sPField2.InternalName != "GroupLink" && ((int)sPField2.Type == 12 || (int)sPField2.Type == 17));
                                if (base.PickerLimit > 0 && !this.Cascade && !this.doPostFilterNow)
                                {
                                    sPQuery.RowLimit=((uint)base.PickerLimit);
                                }
                                IEnumerator enumerator = sPList.GetItems(sPQuery).GetEnumerator();
                                try
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        SPListItem sPListItem = (SPListItem)enumerator.Current;
                                        object fieldVal = ProductPage.GetFieldVal(sPListItem, sPField, false);
                                        string text6;
                                        if (string.IsNullOrEmpty(text6 = ProductPage.Trim(fieldVal, new char[0])) && this.fallbackTitles && string.IsNullOrEmpty(text6 = sPListItem.Title) && string.IsNullOrEmpty(text6 = sPListItem.Name))
                                        {
                                            text6 = "[" + sPListItem.ID + "]";
                                        }
                                        string text7;
                                        if (this is FilterBase.User && sPField2.InternalName == "GroupLink")
                                        {
                                            text7 = (text6 = string.Empty);
                                            sPUser = null;
                                            try
                                            {
                                                sPUser = sPList.ParentWeb.Users[sPListItem["Name"] + string.Empty];
                                            }
                                            catch
                                            {
                                                sPUser = sPList.ParentWeb.SiteUsers[sPListItem["Name"] + string.Empty];
                                            }
                                            if (sPUser != null)
                                            {
                                                foreach (SPGroup sPGroup in sPUser.Groups)
                                                {
                                                    object obj = text7;
                                                    text7 = string.Concat(new object[]
													{
														obj,
														";#",
														sPGroup.ID,
														";#",
														sPGroup.Name
													});
                                                    text6 = text6 + ", " + sPGroup.Name;
                                                }
                                            }
                                            if (text7.StartsWith(";#"))
                                            {
                                                text7 = text7.Substring(2);
                                            }
                                            if (text6.StartsWith(", "))
                                            {
                                                text6 = text6.Substring(2);
                                            }
                                        }
                                        else
                                        {
                                            text7 = ProductPage.GetFieldVal(sPListItem, sPField2, false) + string.Empty;
                                        }
                                        if (!string.IsNullOrEmpty(text7))
                                        {
                                            list2.Clear();
                                            int num;
                                            if ((num = text7.IndexOf(";#", StringComparison.InvariantCultureIgnoreCase)) > 0 && flag7)
                                            {
                                                text7 = text7.Substring(num + 2);
                                            }
                                            if (text7.IndexOf(";#") >= 0)
                                            {
                                                if (text7.IndexOf(";#") != text7.LastIndexOf(";#"))
                                                {
                                                    string[] array = text7.Split(new string[]
													{
														";#"
													}, StringSplitOptions.None);
                                                    for (int i = (flag6 || sPUser != null) ? 1 : 0; i < array.Length; i += ((flag6 || sPUser != null) ? 2 : 1))
                                                    {
                                                        list2.Add((flag6 || sPUser != null) ? (array[i - 1] + ";#" + array[i]) : array[i]);
                                                    }
                                                }
                                                else
                                                {
                                                    if (flag6 || sPUser != null || flag7)
                                                    {
                                                        list2.Add(text7);
                                                    }
                                                    else
                                                    {
                                                        list2.AddRange(text7.Split(new string[]
														{
															";#"
														}, StringSplitOptions.RemoveEmptyEntries));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(text) && text7.IndexOf(text) >= 0)
                                                {
                                                    string[] array2 = text7.Split(new string[]
													{
														text
													}, StringSplitOptions.RemoveEmptyEntries);
                                                    for (int j = 0; j < array2.Length; j++)
                                                    {
                                                        string text8 = array2[j];
                                                        if (!list2.Contains(text8.Trim()))
                                                        {
                                                            list2.Add(text8.Trim());
                                                        }
                                                    }
                                                }
                                            }
                                            if (list2.Count > 1)
                                            {
                                                text6 = null;
                                            }
                                            while (list2.Contains(string.Empty))
                                            {
                                                list2.Remove(string.Empty);
                                            }
                                            if (list2.Count == 0)
                                            {
                                                list2.Add(text7);
                                            }
                                            if (flag2)
                                            {
                                                for (int k = 0; k < list2.Count; k++)
                                                {
                                                    if ((num = list2[k].IndexOf('|')) > 0)
                                                    {
                                                        list2[k] = list2[k].Substring(0, num);
                                                    }
                                                }
                                            }
                                            foreach (string current in list2)
                                            {
                                                if (sPField is SPFieldCalculated && (num = text6.IndexOf(";#", StringComparison.InvariantCultureIgnoreCase)) > 0)
                                                {
                                                    text6 = text6.Substring(num + 2);
                                                }
                                                if (flag && text6 != null && (num = text6.IndexOf('|')) > 0)
                                                {
                                                    text6 = text6.Substring(0, num);
                                                }
                                                string[] array;
                                                if (!flag3 && text6 == current && current.Contains(";#") && (array = current.Split(new string[]
												{
													";#"
												}, StringSplitOptions.RemoveEmptyEntries)) != null && array.Length >= 1)
                                                {
                                                    string[] array2 = array;
                                                    for (int j = 0; j < array2.Length; j++)
                                                    {
                                                        string val = array2[j];
                                                        string text9 = ProductPage.ConvertDateNoTimeIf(val, false, flag8);
                                                        decimal d;
                                                        if (sPField is SPFieldNumber && ((SPFieldNumber)sPField).ShowAsPercentage && decimal.TryParse(text9, out d) && d <= 1m)
                                                        {
                                                            text9 = d * 100m + "%";
                                                        }
                                                        string text10 = ProductPage.ConvertDateNoTimeIf(val, flag8, flag8);
                                                        if ((list4 == null || !list4.Contains(text9)) && (list3 == null || !list3.Contains(text10)))
                                                        {
                                                            if (list4 != null && !list4.Contains(text9))
                                                            {
                                                                list4.Add(text9);
                                                            }
                                                            if (list3 != null && !list3.Contains(text10))
                                                            {
                                                                list3.Add(text10);
                                                            }
                                                            this.items.Add(new KeyValuePair<string, string[]>(sPListItem.ID + ";#" + text10, new string[]
															{
																text9,
																text10
															}));
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string text9 = ProductPage.ConvertDateNoTimeIf(ProductPage.StripID((text6 == null) ? current : text6), flag8, flag8);
                                                    decimal d;
                                                    if (sPField is SPFieldNumber && ((SPFieldNumber)sPField).ShowAsPercentage && decimal.TryParse(text9, out d) && d <= 1m)
                                                    {
                                                        text9 = (int)(d * 100m) + "%";
                                                    }
                                                    string text10 = ProductPage.ConvertDateNoTimeIf(flag3 ? ProductPage.StripID(current) : current, flag8, flag8);
                                                    if ((list4 == null || !list4.Contains(text9)) && (list3 == null || !list3.Contains(text10)))
                                                    {
                                                        if (list4 != null && !list4.Contains(text9))
                                                        {
                                                            list4.Add(text9);
                                                        }
                                                        if (list3 != null && !list3.Contains(text10))
                                                        {
                                                            list3.Add(text10);
                                                        }
                                                        this.items.Add(new KeyValuePair<string, string[]>(sPListItem.ID + ";#" + text10, new string[]
														{
															text9,
															text10
														}));
                                                    }
                                                }
                                            }
                                            if (base.PickerLimit > 0 && itemSort == FilterBase.Lookup.ItemSort.None && this.items.Count >= base.PickerLimit && !flag4 && !flag5)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    goto IL_C0E;
                                }
                                finally
                                {
                                    IDisposable disposable = enumerator as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                                goto IL_B83;
                            IL_C0E:
                                if (!this.Get<bool>("DefaultIfEmpty"))
                                {
                                    this.items.RemoveAll((KeyValuePair<string, string[]> kvp) => string.IsNullOrEmpty(kvp.Value[1]));
                                }
                                if (itemSort != FilterBase.Lookup.ItemSort.None)
                                {
                                    string text11 = itemSort.ToString();
                                    int sortIndex = text11.StartsWith("Value") ? 1 : 0;
                                    bool sortDesc = text11.EndsWith("Desc");
                                    bool sortCount = text11.Contains("Count");
                                    this.items.Sort(delegate(KeyValuePair<string, string[]> one, KeyValuePair<string, string[]> two)
                                    {
                                        int num3 = 0;
                                        KeyValuePair<string, string[]> keyValuePair = one;
                                        if (sortDesc)
                                        {
                                            one = two;
                                            two = keyValuePair;
                                        }
                                        if (sortCount)
                                        {
                                            num3 = countDicts[sortIndex][one.Value[sortIndex]].CompareTo(countDicts[sortIndex][two.Value[sortIndex]]);
                                        }
                                        if (num3 == 0)
                                        {
                                            num3 = one.Value[sortIndex].CompareTo(two.Value[sortIndex]);
                                        }
                                        if (num3 != 0)
                                        {
                                            return num3;
                                        }
                                        return one.Key.CompareTo(two.Key);
                                    });
                                }
                                if (!string.IsNullOrEmpty(text2))
                                {
                                    if (this.items.TrueForAll((KeyValuePair<string, string[]> kvp) => kvp.Value.Length > 0 && (kvp.Value.Length < 2 || (string.Empty + kvp.Value[0]).Equals(string.Empty + kvp.Value[1]))))
                                    {
                                        int count;
                                        int num2 = count = this.items.Count;
                                        string[] tmpArr;
                                        string tmpKey;
                                        for (int l = 0; l < num2; l++)
                                        {
                                            int length;
                                            string str = (((length = this.items[l].Key.IndexOf(";#")) > 0) ? this.items[l].Key.Substring(0, length) : "0") + ";#";
                                            if ((tmpArr = this.items[l].Value[0].Split(new string[]
											{
												text2
											}, StringSplitOptions.RemoveEmptyEntries)) != null)
                                            {
                                                for (int m = 0; m < tmpArr.Length; m++)
                                                {
                                                    tmpKey = string.Join(text2, tmpArr, 0, m + 1);
                                                    if (this.items.FindIndex(num2, delegate(KeyValuePair<string, string[]> kvp)
                                                    {
                                                        string[] arg_0B_0 = tmpArr;
                                                        string text12 = kvp.Key;
                                                        int num3 = text12.IndexOf(";#");
                                                        if (num3 > 0)
                                                        {
                                                            text12 = text12.Substring(num3 + 2);
                                                        }
                                                        return text12.Trim().Equals(tmpKey.Trim());
                                                    }) < num2)
                                                    {
                                                        this.items.Insert(count++, new KeyValuePair<string, string[]>(str + tmpKey, new string[]
														{
															new string(' ', m * 3) + tmpArr[m].Trim(),
															tmpKey
														}));
                                                    }
                                                }
                                            }
                                        }
                                        for (int n = 0; n < num2; n++)
                                        {
                                            this.items.RemoveAt(0);
                                        }
                                    }
                                }
                                if (this.Get<bool>("PostFilter"))
                                {
                                    KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(this.items.ConvertAll<string>((KeyValuePair<string, string[]> kvp) => kvp.Value[1]).ToArray());
                                    this.items.RemoveAll((KeyValuePair<string, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                                }
                                if (this.Cascade && this.doPostFilterNow)
                                {
                                    KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(sPList, sPView, base.Name.StartsWith("@") ? base.Name.Substring(1) : base.Name, this.items.ConvertAll<string>((KeyValuePair<string, string[]> kvp) => kvp.Value[1]).ToArray(), true);
                                    this.items.RemoveAll((KeyValuePair<string, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                                    goto IL_106C;
                                }
                                goto IL_106C;
                            }
                        IL_B83:
                            throw new Exception(base["LookupFailed", new object[]
							{
								(sPList == null) ? this.Get<string>("ListUrl") : sPList.ToString(),
								(sPView == null) ? this.Get<string>("ListUrl") : sPView.ToString(),
								(sPField2 == null) ? this.Get<string>("ValueFieldName") : sPField2.ToString(),
								(sPField == null) ? this.Get<string>("DisplayFieldName") : sPField.ToString()
							}]);
                        }
                        if (this.doPostFilterNow)
                        {
                            if (sPList == null || sPView == null)
                            {
                                sPWrap2 = base.GetList("ListUrl", false);
                            }
                            if ((sPList = sPWrap2.Value) != null)
                            {
                                sPView = base.GetView(sPWrap2, text3);
                            }
                            if (sPList != null && sPView != null)
                            {
                                KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(sPList, sPView, base.Name.StartsWith("@") ? base.Name.Substring(1) : base.Name, this.items.ConvertAll<string>((KeyValuePair<string, string[]> kvp) => kvp.Value[1]).ToArray(), true);
                                this.items.RemoveAll((KeyValuePair<string, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                            }
                        }
                    IL_106C:
                        result = this.items.ConvertAll<KeyValuePair<string, string>>((KeyValuePair<string, string[]> kvp) => new KeyValuePair<string, string>(kvp.Key, kvp.Value[0]));
                    }
                    finally
                    {
                        if (sPWrap != null)
                        {
                            ((IDisposable)sPWrap).Dispose();
                        }
                        if (sPWrap2 != null)
                        {
                            ((IDisposable)sPWrap2).Dispose();
                        }
                    }
                    return result;
                }
            }
            protected internal override IEnumerable<string> AllPickableValues
            {
                get
                {
                    if (this.items == null)
                    {
                        this.Items.ToString();
                    }
                    foreach (KeyValuePair<string, string[]> current in this.items)
                    {
                        KeyValuePair<string, string[]> keyValuePair = current;
                        yield return keyValuePair.Value[1];
                    }
                    yield break;
                }
            }
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    int result = this.Get<int>("ItemID");
                    bool iteratorVariable1 = false;
                    SPUser currentUser = null;
                    List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, result.ToString());
                    string name = string.Empty;
                    Converter<string, string> converter = delegate(string id)
                    {
                        KeyValuePair<string, string[]> keyValuePair = this.items.Find(delegate(KeyValuePair<string, string[]> value)
                        {
                            int index = value.Key.IndexOf(";#");
                            return (id == value.Key) || ((index > 0) && id.Equals(value.Key.Substring(0, index)));
                        });
                        if (keyValuePair.Value == null || keyValuePair.Value.Length < 2)
                        {
                            return string.Empty;
                        }
                        return keyValuePair.Value[1];
                    };
                    if (!base.Le(2, true))
                    {
                        throw new Exception(ProductPage.GetResource("NopeEd", new object[]
						{
							FilterBase.GetFilterTypeTitle(base.GetType()),
							"Basic"
						}));
                    }
                    try
                    {
                        if (this.items == null)
                        {
                            this.Items.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        base.Report(ex);
                    }
                    foreach (string current in filterValues)
                    {
                        if (!int.TryParse(current, out result))
                        {
                            if ((result = current.IndexOf(";#")) <= 0 || !int.TryParse(current.Substring(0, result), out result))
                            {
                                continue;
                            }
                        }
                        try
                        {
                            if (result > 0 || (result == -1 && !(this is FilterBase.User) && (result = this.GetPageID("ListUrl")) > 0))
                            {
                                name = converter(current);
                            }
                        }
                        catch (Exception ex2)
                        {
                            base.Report(ex2);
                        }
                        if (this.items != null && this.items.Count > 0 && this is FilterBase.User && result == -1)
                        {
                            try
                            {
                                currentUser = SPContext.Current.Web.CurrentUser;
                            }
                            catch
                            {
                            }
                        }
                        if (currentUser != null)
                        {
                            if (iteratorVariable1 = (this.valueFieldName == "GroupLink"))
                            {
                                foreach (SPGroup sPGroup in SPContext.Current.Web.CurrentUser.Groups)
                                {
                                    yield return new KeyValuePair<string, string>(base.Name, this.Get<bool>("StripID") ? sPGroup.Name : (sPGroup.ID + ";#" + sPGroup.Name));
                                }
                            }
                            else
                            {
                                name = currentUser.Name;
                            }
                        }
                        if (result == -2)
                        {
                            name = this.items[0].Value[1];
                        }
                        else
                        {
                            if (result == -3)
                            {
                                name = this.items[this.items.Count - 1].Value[1];
                            }
                            else
                            {
                                if (result == 0 && (result = current.IndexOf(";#")) > 0)
                                {
                                    name = current.Substring(result + 2);
                                }
                            }
                        }
                        if (!iteratorVariable1)
                        {
                            yield return new KeyValuePair<string, string>(base.Name, name);
                        }
                    }
                    yield break;
                }
            }

            public override bool Cascade
            {
                get
                {
                    return this.pickerSemantics && this.parentWebPart != null && this.parentWebPart.Cascaded && this.parentWebPart.LicEd(4) && this.parentWebPart.CamlFilters;
                }
            }
            public override bool DefaultIfEmpty
            {
                get
                {
                    return base.DefaultIfEmpty || this.Get<int>("ItemID") == 0;
                }
                set
                {
                    base.DefaultIfEmpty = (value || this.Get<int>("ItemID") == 0);
                }
            }
            public string DisplayFieldName
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return string.Empty;
                    }
                    return this.displayFieldName;
                }
                set
                {
                    this.displayFieldName = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string FilterCaml
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return string.Empty;
                    }
                    return this.filterCaml;
                }
                set
                {
                    this.filterCaml = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public int ItemID
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return 0;
                    }
                    return this.itemID;
                }
                set
                {
                    this.itemID = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ((value < -3) ? 0 : value) : 0);
                }
            }
            public int ItemSorting
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return 0;
                    }
                    return this.itemSorting;
                }
                set
                {
                    this.itemSorting = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? value : 0);
                }
            }
            public virtual string ListUrl
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return string.Empty;
                    }
                    return this.listUrl;
                }
                set
                {
                    this.listUrl = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public bool RemoveDuplicateTitles
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(2)) && this.removeDuplicateTitles;
                }
                set
                {
                    this.removeDuplicateTitles = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) && value);
                }
            }
            public bool RemoveDuplicateValues
            {
                get
                {
                    return (this.parentWebPart != null && !this.parentWebPart.LicEd(2)) || this.removeDuplicateValues;
                }
                set
                {
                    this.removeDuplicateValues = ((this.parentWebPart != null && !this.parentWebPart.LicEd(2)) || value);
                }
            }
            public bool StripID
            {
                get
                {
                    return (this.parentWebPart == null || this.parentWebPart.LicEd(2)) && this.stripID;
                }
                set
                {
                    this.stripID = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) && value);
                }
            }
            public string ValueFieldName
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                    {
                        return string.Empty;
                    }
                    return this.valueFieldName;
                }
                set
                {
                    this.valueFieldName = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public Lookup()
            {
                this.pickerSemantics = true;
                this.defaultIfEmpty = true;
            }
            public Lookup(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.pickerSemantics = true;
                try
                {
                    this.DisplayFieldName = info.GetString("DisplayFieldName");
                    this.FilterCaml = info.GetString("FilterCaml");
                    this.ItemID = info.GetInt32("ItemID");
                    this.ItemSorting = info.GetInt32("ItemSorting");
                    this.ListUrl = info.GetString("ListUrl");
                    this.RemoveDuplicateTitles = info.GetBoolean("RemoveDuplicateTitles");
                    this.RemoveDuplicateValues = info.GetBoolean("RemoveDuplicateValues");
                    this.StripID = info.GetBoolean("StripID");
                    this.ValueFieldName = info.GetString("ValueFieldName");
                    this.DefaultIfEmpty = info.GetBoolean("DefaultIfEmpty");
                }
                catch
                {
                }
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("DisplayFieldName", this.DisplayFieldName);
                info.AddValue("FilterCaml", this.FilterCaml);
                info.AddValue("ItemID", this.ItemID);
                info.AddValue("ItemSorting", this.ItemSorting);
                info.AddValue("ListUrl", this.ListUrl);
                info.AddValue("RemoveDuplicateTitles", this.RemoveDuplicateTitles);
                info.AddValue("RemoveDuplicateValues", this.RemoveDuplicateValues);
                info.AddValue("StripID", this.StripID);
                info.AddValue("ValueFieldName", this.ValueFieldName);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, this.Get<int>("ItemID").ToString());
                string value = string.Empty;
                string text = "";
                string nameSep = this.Get<string>("MultiFilterSeparator");
                string valSep = this.Get<string>("MultiValueSeparator");
                int num = 0;
                bool flag = false;
                bool isNumeric = base.IsNumeric;
                bool flag2 = this.Get<bool>("CheckStyle");
                if (filterValues.Contains("0"))
                {
                    filterValues.Clear();
                }
                if (!base.Le(2, true))
                {
                    output.WriteLine(ProductPage.GetResource("NopeEd", new object[]
					{
						FilterBase.GetFilterTypeTitle(base.GetType()),
						"Basic"
					}));
                    base.Render(output, isUpperBound);
                    return;
                }
                if (this is FilterBase.User && filterValues.Count == 1 && filterValues.Contains("-1"))
                {
                    value = this.GetPageID(string.Empty) + ";#";
                }
                try
                {
                    if (this.items == null)
                    {
                        if (!this.postFiltered)
                        {
                            this.doPostFilterNow = (this.postFiltered = true);
                        }
                        this.Items.ToString();
                    }
                    else
                    {
                        if (this.Cascade && !this.postFiltered)
                        {
                            this.postFiltered = (this.doPostFilterNow = true);
                            this.Items.ToString();
                        }
                    }
                    foreach (KeyValuePair<string, string[]> current in this.items)
                    {
                        string text2 = base.GetDisplayValue(current.Value[0], valSep, nameSep, isNumeric).Replace(" ", "&nbsp;");
                        bool flag3;
                        if (flag2)
                        {
                            int length;
                            text += string.Format(string.Concat(new string[]
							{
								"<span><input class=\"chk-",
								base.ID,
								" rox-check-value\" name=\"filterval_",
								base.ID,
								"\" type=\"",
								base.AllowMultiEnter ? "checkbox" : "radio",
								"\" id=\"x{0}\" value=\"{1}\" {3}",
								(string.IsNullOrEmpty(base.HtmlOnChangeAttr) && this.Get<bool>("DefaultIfEmpty")) ? (" onclick=\"document.getElementById('empty_filterval_" + base.ID + "').checked=false;\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"" + (this.Get<bool>("DefaultIfEmpty") ? ("document.getElementById('empty_filterval_" + base.ID + "').checked=false;") : string.Empty)),
								"/><label for=\"x{0}\">{2}</label></span>"
							}), new object[]
							{
								ProductPage.GuidLower(Guid.NewGuid()),
								current.Key,
								text2,
								(flag3 = (filterValues.Contains(current.Key) || ((length = current.Key.IndexOf(";#")) > 0 && filterValues.Contains(current.Key.Substring(0, length))) || (filterValues.Contains("-2") && num == 0) || (filterValues.Contains("-3") && num == this.items.Count - 1))) ? " checked=\"checked\"" : string.Empty
							});
                        }
                        else
                        {
                            int length;
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", HttpUtility.HtmlAttributeEncode(current.Key), text2, (flag3 = ((!string.IsNullOrEmpty(value) && current.Key.StartsWith(value, StringComparison.InvariantCultureIgnoreCase)) || filterValues.Contains(current.Key) || ((length = current.Key.IndexOf(";#")) > 0 && filterValues.Contains(current.Key.Substring(0, length))) || (filterValues.Contains("-2") && num == 0) || (filterValues.Contains("-3") && num == this.items.Count - 1))) ? " selected=\"selected\"" : string.Empty);
                        }
                        flag |= flag3;
                        if (base.PickerLimit != 0 && num >= base.PickerLimit)
                        {
                            break;
                        }
                        num++;
                    }
                    if (this.Get<bool>("DefaultIfEmpty"))
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxMultiMins['filterval_" + base.ID + "'] = '0'; </script>");
                        if (!flag2)
                        {
                            text = string.Format("<option value=\"{0}\"{2}>{1}</option>", "0", base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]], (filterValues.Count == 0 || filterValues.Contains("0")) ? " selected=\"selected\"" : string.Empty) + text;
                        }
                        else
                        {
                            text = string.Format(string.Concat(new string[]
							{
								"<span><input class=\"rox-check-default\" name=\"filterval_",
								base.ID,
								"\" type=\"",
								base.AllowMultiEnter ? "checkbox" : "radio",
								"\" id=\"empty_filterval_",
								base.ID,
								"\" value=\"{1}\" {3}",
								string.IsNullOrEmpty(base.HtmlOnChangeAttr) ? (" onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);"),
								"/><label for=\"empty_filterval_",
								base.ID,
								"\">{2}</label></span>"
							}), new object[]
							{
								ProductPage.GuidLower(Guid.NewGuid()),
								0,
								base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]],
								(filterValues.Count == 0 || filterValues.Contains("0")) ? " checked=\"checked\"" : string.Empty
							}) + text;
                        }
                    }
                    if (text.Length > 0)
                    {
                        if (flag2)
                        {
                            output.Write("<div>" + text + "</div>");
                        }
                        else
                        {
                            output.Write(string.Concat(new string[]
							{
								"<select",
								base.AllowMultiEnter ? " size=\"1\" multiple=\"multiple\" class=\"rox-multiselect ms-input\"" : " class=\"ms-input\"",
								" name=\"{0}\" id=\"{0}\"{1}>",
								text,
								"</select>"
							}), "filterval_" + base.ID, base.AllowMultiEnter ? base.HtmlOnChangeMultiAttr : base.HtmlOnChangeAttr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    base.Report(ex);
                }
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                int num = this.Get<int>("ItemID");
                string text = string.Format("<option value=\"{0}\"{2}>{1}</option>", "0", base["Empty", new object[0]], (num == 0) ? " selected=\"selected\"" : string.Empty) + string.Format("<option value=\"{0}\"{2}>{1}</option>", "-2", base["ItemIDFirst", new object[0]], (num == -2) ? " selected=\"selected\"" : string.Empty);
                string text2 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                string text3 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
                string text4 = "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                string text5 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
                if (this.parentWebPart != null)
                {
                    try
                    {
                        using (SPWrap<SPList> list = base.GetList("ListUrl", true))
                        {
                            if (list.Value != null && (string.IsNullOrEmpty(this.Get<string>("ListUrl")) || this.Get<string>("ListUrl").Equals(list.Value.DefaultViewUrl, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                text += string.Format("<option value=\"{0}\"{2}>{1}</option>", "-1", base["ItemID", new object[0]], (num == -1) ? " selected=\"selected\"" : string.Empty);
                            }
                            else
                            {
                                using (SPWrap<SPList> list2 = base.GetList("ListUrl", false))
                                {
                                    if (list2.Value != null && list2.Value.DefaultViewUrl.ToLowerInvariant().Contains("_catalogs/users/"))
                                    {
                                        text += string.Format("<option value=\"{0}\"{2}>{1}</option>", "-1", base["ItemUser", new object[0]], (num == -1) ? " selected=\"selected\"" : string.Empty);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        base.Report(ex);
                    }
                    LiteralControl literalControl;
                    (literalControl = base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "ListUrl", new object[]
					{
						this.Get<string>("ListUrl")
					})).Text = literalControl.Text.Replace("100%", "294px");
                    panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
					{
						FilterBase.GetFilterTypeTitle(base.GetType())
					}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                    panel.Controls.Add(literalControl);
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "ValueFieldName", new object[]
					{
						this.Get<string>("ValueFieldName")
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "DisplayFieldName", new object[]
					{
						this.Get<string>("DisplayFieldName")
					}));
                    try
                    {
                        foreach (KeyValuePair<string, string> current in this.Items)
                        {
                            int num2;
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", current.Key, current.Value, ((int.TryParse(current.Key, out num2) || (current.Key.Contains(";#") && int.TryParse(current.Key.Substring(0, current.Key.IndexOf(";#")), out num2))) && num == num2) ? " selected=\"selected\"" : string.Empty);
                        }
                    }
                    catch (Exception ex2)
                    {
                        base.Report(ex2);
                    }
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "ItemID", new object[]
					{
						" onchange=\"" + FilterBase.Lookup.scriptCheckDefault + "\"",
						text + string.Format("<option value=\"{0}\"{2}>{1}</option>", "-3", base["ItemIDLast", new object[0]], (num == -3) ? " selected=\"selected\"" : string.Empty)
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text4, "StripID", new object[]
					{
						base.GetChecked(this.Get<bool>("StripID"))
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text4, "RemoveDuplicateValues", new object[]
					{
						base.GetChecked(this.Get<bool>("RemoveDuplicateValues"))
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text4, "RemoveDuplicateTitles", new object[]
					{
						base.GetChecked(this.Get<bool>("RemoveDuplicateTitles"))
					}));
                    text = "";
                    for (int i = 0; i < 9; i++)
                    {
                        text += string.Format("<option value=\"{0}\"{2}>{1}</option>", i, base["ItemSort_" + i, new object[0]], (i == this.Get<int>("ItemSorting")) ? " selected=\"selected\"" : string.Empty);
                    }
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "ItemSorting", new object[]
					{
						"",
						text
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>" : text5, "FilterCaml", new object[]
					{
						this.Get<string>("FilterCaml")
					}));
                    panel.Controls.Add(base.CreateScript(FilterBase.Lookup.scriptCheckDefault));
                    panel.Controls.Add(new LiteralControl("</fieldset>"));
                }
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.FilterCaml = this.Get<string>("FilterCaml");
                this.ListUrl = this.Get<string>("ListUrl");
                this.ValueFieldName = this.Get<string>("ValueFieldName");
                this.DisplayFieldName = this.Get<string>("DisplayFieldName");
                this.ItemID = this.Get<int>("ItemID");
                this.RemoveDuplicateTitles = this.Get<bool>("RemoveDuplicateTitles");
                this.RemoveDuplicateValues = this.Get<bool>("RemoveDuplicateValues");
                this.ItemSorting = this.Get<int>("ItemSorting");
                this.StripID = this.Get<bool>("StripID");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class Multi : FilterBase.Interactive
        {
            internal enum FieldCat
            {
                None,
                Custom = 16,
                ListInherited = 4,
                ListOther = 2,
                ListSystem = 8,
                ListView = 1,
                All = 31
            }
            internal class FieldDesc
            {
                public string Name = string.Empty;
                public string Title = string.Empty;
                public SPField SPField;
                public FilterBase.Multi.FieldCat Cat = FilterBase.Multi.FieldCat.Custom;
                public static int Compare(FilterBase.Multi.FieldDesc one, FilterBase.Multi.FieldDesc two)
                {
                    if (one.Cat != two.Cat)
                    {
                        int cat = (int)one.Cat;
                        return cat.CompareTo((int)two.Cat);
                    }
                    if (one.Title == "Title" && two.Title != "Title")
                    {
                        return "a".CompareTo("b");
                    }
                    if (two.Title == "Title" && one.Title != "Title")
                    {
                        return "b".CompareTo("a");
                    }
                    if (!one.Title.Equals(two.Title))
                    {
                        return one.Title.CompareTo(two.Title);
                    }
                    return one.Name.CompareTo(two.Name);
                }
            }
            internal static readonly string[] blockedFields = new string[]
			{
				"Attachments",
				"ContentTypeId",
				"Edit",
				"SelectTitle",
				"PermMask",
				"UniqueId",
				"ProgId",
				"ScopeId",
				"_EditMenuTableStart",
				"_EditMenuTableEnd",
				"LinkFilenameNoMenu",
				"LinkFilename",
				"ServerUrl",
				"EncodedAbsUrl",
				"BaseName",
				"FolderChildCount",
				"ItemChildCount",
				"Last_x0020_Modified",
				"Created_x0020_Date",
				"_UIVersionString",
				"WorkflowInstanceID",
				"WorkflowVersion",
				"_UIVersion",
				"SortBehavior",
				"MetaInfo",
				"owshiddenversion",
				"Order",
				"_Level",
				"FSObjType",
				"_IsCurrentVersion",
				"InstanceID",
				"HTML_x0020_File_x0020_Type",
				"_HasCopyDestinations",
				"GUID",
				"File_x0020_Type",
				"_CopySource",
				"SyncClientId",
				"ContentType",
				"_ModerationStatus",
				"DocIcon"
			};
            internal static readonly string[] titleFields = new string[]
			{
				"Title",
				"LinkTitle",
				"LinkTitleNoMenu",
				"LinkTitle2",
				"LinkTitleNoMenu2"
			};
            internal static readonly string[] boolOpTypes = new string[]
			{
				"AllDayEvent",
				"Boolean"
			};
            internal static readonly string[] numOpTypes = new string[]
			{
				"Counter",
				"Currency",
				"DateTime",
				"Integer",
				"MaxItems",
				"Number",
				"ThreadIndex"
			};
            internal static readonly string[] strOpTypes = new string[]
			{
				"Computed",
				"Note",
				"Text",
				"Choice",
				"Lookup",
				"URL",
				"MultiChoice",
				"GridChoice",
				"ModStat"
			};
            internal static readonly string[] userOpTypes = new string[]
			{
				"User"
			};
            internal static readonly CamlOperator[] allOperators = new CamlOperator[]
			{
				roxority.SharePoint.CamlOperator.Eq,
				roxority.SharePoint.CamlOperator.Neq,
				roxority.SharePoint.CamlOperator.BeginsWith,
				roxority.SharePoint.CamlOperator.Contains,
				roxority.SharePoint.CamlOperator.Gt,
				roxority.SharePoint.CamlOperator.Geq,
				roxority.SharePoint.CamlOperator.Lt,
				roxority.SharePoint.CamlOperator.Leq
			};
            internal static readonly CamlOperator[] anyOperators = new CamlOperator[]
			{
				roxority.SharePoint.CamlOperator.Eq,
				roxority.SharePoint.CamlOperator.Neq
			};
            internal static readonly CamlOperator[] numberOperators = new CamlOperator[]
			{
				roxority.SharePoint.CamlOperator.Eq,
				roxority.SharePoint.CamlOperator.Neq,
				roxority.SharePoint.CamlOperator.Gt,
				roxority.SharePoint.CamlOperator.Geq,
				roxority.SharePoint.CamlOperator.Lt,
				roxority.SharePoint.CamlOperator.Leq
			};
            internal static readonly CamlOperator[] stringOperators = new CamlOperator[]
			{
				roxority.SharePoint.CamlOperator.Eq,
				roxority.SharePoint.CamlOperator.Neq,
				roxority.SharePoint.CamlOperator.BeginsWith,
				roxority.SharePoint.CamlOperator.Contains
			};
            internal static readonly CamlOperator[] userOperators = new CamlOperator[]
			{
				roxority.SharePoint.CamlOperator.Me,
				roxority.SharePoint.CamlOperator.NotMe,
				roxority.SharePoint.CamlOperator.Eq,
				roxority.SharePoint.CamlOperator.Neq
			};
            private string[] customFieldNames = new string[0];
            private string[] listFieldNames = new string[0];
            private bool allowAllInheritedFields = true;
            private bool allowAllListFields = true;
            private bool allowAllOtherFields;
            private bool allowAllViewFields = true;
            private bool allowAnyField;
            private bool allowAnyAllOps;
            private bool anyIsAll;
            private bool groupFields = true;
            private bool indent = true;
            private List<FilterBase.Multi.FieldDesc> fields;
            private List<roxority_FilterWebPart.FilterPair> fps;
            private bool? hasPeop = null;
            //modified by lhan
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    if (this.fps == null && this.parentWebPart != null && !string.IsNullOrEmpty(this.parentWebPart.MultiTextBox.Text))
                    {
                        try
                        {
                            ArrayList arrayList;
                            if ((arrayList = (JSON.JsonDecode(this.parentWebPart.MultiTextBox.Text) as ArrayList)) == null)
                            {
                                throw new Exception(base["JsonSyntax", new object[0]] + " -- " + this.parentWebPart.MultiTextBox.Text);
                            }
                            this.fps = new List<roxority_FilterWebPart.FilterPair>();
                            IEnumerator enumerator = arrayList.GetEnumerator();
                            try
                            {
                                string tmp;
                                Hashtable ht;
                                while (enumerator.MoveNext())
                                {
                                    ht = (Hashtable)enumerator.Current;
                                    if ("*".Equals(tmp = ht["field"] + string.Empty, StringComparison.InvariantCultureIgnoreCase) || string.IsNullOrEmpty(tmp))
                                    {
                                        using (IEnumerator<FilterBase.Multi.FieldDesc> enumerator2 = this.GetFields(this.AnyIsAll ? FilterBase.Multi.FieldCat.All : this.FieldMask).GetEnumerator())
                                        {
                                            FilterBase.Multi.FieldDesc fd;
                                            while (enumerator2.MoveNext())
                                            {
                                                fd = enumerator2.Current;
                                                if (fd.SPField == null || (int)fd.SPField.Type != 8)
                                                {
                                                    //if (!this.fps.Exists((roxority_FilterWebPart.FilterPair test) => test.Key == fd.Name))
                                                    //{
                                                        this.fps.Add(new roxority_FilterWebPart.FilterPair(fd.Name, ht["val"] + string.Empty, (CamlOperator)Enum.Parse(typeof(CamlOperator), ht["op"] + string.Empty, true))
                                                        {
                                                            nextAnd = false
                                                        });
                                                    //}
                                                }
                                            }
                                            continue;
                                        }
                                    }
                                    //if (!this.fps.Exists(
                                    //    (roxority_FilterWebPart.FilterPair test) => test.Key == tmp
                                    //        && (ht["val"] + string.Empty).Equals(test.Value)
                                    //        && test.CamlOperator.Equals((CamlOperator)Enum.Parse(typeof(CamlOperator), ht["op"] + string.Empty, true))
                                    //        && test.nextAnd.Equals(!"or".Equals(ht["lop"] + string.Empty, StringComparison.InvariantCultureIgnoreCase))))
                                    //{
                                        this.fps.Add(new roxority_FilterWebPart.FilterPair(tmp, ht["val"] + string.Empty, (CamlOperator)Enum.Parse(typeof(CamlOperator), ht["op"] + string.Empty, true))
                                        {
                                            nextAnd = !"or".Equals(ht["lop"] + string.Empty, StringComparison.InvariantCultureIgnoreCase)
                                        });
                                    //}
                                }
                            }
                            finally
                            {
                                IDisposable disposable = enumerator as IDisposable;
                                if (disposable != null)
                                {
                                    disposable.Dispose();
                                }
                            }
                        }
                        catch (Exception value)
                        {
                            this.parentWebPart.warningsErrors.Add(new KeyValuePair<FilterBase, Exception>(this, value));
                        }
                    }
                    if (this.fps != null)
                    {
                        for (int i = 0; i < this.fps.Count; i++)
                        {
                            if (this.fps[i].CamlOperator == roxority.SharePoint.CamlOperator.Me || this.fps[i].CamlOperator == roxority.SharePoint.CamlOperator.NotMe)
                            {
                                this.fps[i].Value = SPContext.Current.Web.CurrentUser.Name;
                                this.fps[i].CamlOperator = ((this.fps[i].CamlOperator == roxority.SharePoint.CamlOperator.NotMe) ? roxority.SharePoint.CamlOperator.Neq : roxority.SharePoint.CamlOperator.Eq);
                            }
                        }
                    }
                    if (this.fps != null && this.fps.Count != 0)
                    {
                        return this.fps.ConvertAll<KeyValuePair<string, string>>((roxority_FilterWebPart.FilterPair fp) => new KeyValuePair<string, string>(fp.Key, fp.Value));
                    }
                    return null;
                }
            }

            public List<MultiTextCollection> FilterConditions
            {
                get{
                    if (this.parentWebPart != null && !string.IsNullOrEmpty(this.parentWebPart.MultiTextBox.Text))
                    {
                        try
                        {
                            ArrayList arrayList;
                            List<MultiTextCollection> filterConditions = new List<MultiTextCollection>();
                            if ((arrayList = (JSON.JsonDecode(this.parentWebPart.MultiTextBox.Text) as ArrayList)) == null)
                            {
                                throw new Exception(base["JsonSyntax", new object[0]] + " -- " + this.parentWebPart.MultiTextBox.Text);
                            }
                            
                            IEnumerator enumerator = arrayList.GetEnumerator();
                            try
                            {
                                string tmp;
                                Hashtable ht;
                                while (enumerator.MoveNext())
                                {
                                    ht = (Hashtable)enumerator.Current;

                                    using (IEnumerator<FilterBase.Multi.FieldDesc> enumerator2 = this.GetFields(this.AnyIsAll ? FilterBase.Multi.FieldCat.All : this.FieldMask).GetEnumerator())
                                    {
                                        filterConditions.Add(new MultiTextCollection
                                        {
                                            Deep = ConvertHelper.ConvertString(ht["deep"]),
                                            Field = ConvertHelper.ConvertString(ht["field"]),
                                            Lop = ConvertHelper.ConvertString(ht["lop"]),
                                            Num = ConvertHelper.ConvertString(ht["num"]),
                                            Op = ConvertHelper.ConvertString(ht["op"]),
                                            ParentNum = ConvertHelper.ConvertString(ht["parentNum"]),
                                            Sort = ConvertHelper.ConvertString(ht["sort"]),
                                            Val = ConvertHelper.ConvertString(ht["val"])
                                        });
                                    }
                                }
                            }
                            finally
                            {
                                IDisposable disposable = enumerator as IDisposable;
                                if (disposable != null)
                                {
                                    disposable.Dispose();
                                }
                            }
                           return filterConditions;
                        }
                        catch (Exception value)
                        {
                            this.parentWebPart.warningsErrors.Add(new KeyValuePair<FilterBase, Exception>(this, value));
                        }
                    }
                return null;
                }
            }

            public class MultiTextCollection
            {
                public string Field { get; set; }
                public string Op { get; set; }
                public string Lop { get; set; }
                public string Val { get; set; }
                public string Num { get; set; }
                public string ParentNum { get; set; }
                public string Deep { get; set; }
                public string Sort { get; set; }
            }

            public bool AllowAllInheritedFields
            {
                get
                {
                    return this.allowAllInheritedFields;
                }
                set
                {
                    this.allowAllInheritedFields = value;
                }
            }
            public bool AllowAllListFields
            {
                get
                {
                    return this.allowAllListFields;
                }
                set
                {
                    this.allowAllListFields = value;
                }
            }
            public bool AllowAllOtherFields
            {
                get
                {
                    return this.allowAllOtherFields;
                }
                set
                {
                    this.allowAllOtherFields = value;
                }
            }
            public bool AllowAllViewFields
            {
                get
                {
                    return this.allowAllViewFields;
                }
                set
                {
                    this.allowAllViewFields = value;
                }
            }
            public bool AllowAnyAllOps
            {
                get
                {
                    return this.allowAnyAllOps;
                }
                set
                {
                    this.allowAnyAllOps = value;
                }
            }
            public bool AllowAnyField
            {
                get
                {
                    return this.allowAnyField;
                }
                set
                {
                    this.allowAnyField = value;
                }
            }
            public bool AnyIsAll
            {
                get
                {
                    return this.anyIsAll;
                }
                set
                {
                    this.anyIsAll = value;
                }
            }
            public string[] CustomFieldNames
            {
                get
                {
                    return this.customFieldNames;
                }
                set
                {
                    this.customFieldNames = value;
                }
            }
            public FilterBase.Multi.FieldCat FieldMask
            {
                get
                {
                    FilterBase.Multi.FieldCat fieldCat = this.AllowAllViewFields ? FilterBase.Multi.FieldCat.ListView : FilterBase.Multi.FieldCat.None;
                    if (this.AllowAllListFields)
                    {
                        fieldCat |= FilterBase.Multi.FieldCat.ListOther;
                    }
                    if (this.AllowAllInheritedFields)
                    {
                        fieldCat |= FilterBase.Multi.FieldCat.ListInherited;
                    }
                    if (this.AllowAllOtherFields)
                    {
                        fieldCat |= FilterBase.Multi.FieldCat.ListSystem;
                    }
                    if (this.ListFieldNames.Length > 0 || this.CustomFieldNames.Length > 0)
                    {
                        fieldCat |= FilterBase.Multi.FieldCat.Custom;
                    }
                    return fieldCat;
                }
            }
            public bool GroupFields
            {
                get
                {
                    return this.groupFields;
                }
                set
                {
                    this.groupFields = value;
                }
            }
            public bool Indent
            {
                get
                {
                    return this.indent;
                }
                set
                {
                    this.indent = value;
                }
            }
            public override bool IsInteractive
            {
                get
                {
                    return true;
                }
                set
                {
                }
            }
            public override string Label
            {
                get
                {
                    return string.Empty;
                }
                set
                {
                }
            }
            public string[] ListFieldNames
            {
                get
                {
                    return this.listFieldNames;
                }
                set
                {
                    this.listFieldNames = value;
                }
            }
            public Multi()
            {
                this.supportAllowMultiEnter = (this.supportAutoSuggest = (this.requirePostLoadRendering = true));
                base.AllowMultiEnter = (base.AutoSuggest = true);
            }
            public Multi(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                try
                {
                    this.customFieldNames = (info.GetValue("CustomFieldNames", typeof(string[])) as string[]);
                    this.listFieldNames = (info.GetValue("ListFieldNames", typeof(string[])) as string[]);
                    this.AllowAllInheritedFields = info.GetBoolean("AllowAllInheritedFields");
                    this.AllowAllListFields = info.GetBoolean("AllowAllListFields");
                    this.AllowAllOtherFields = info.GetBoolean("AllowAllOtherFields");
                    this.AllowAllViewFields = info.GetBoolean("AllowAllViewFields");
                    this.AllowAnyField = info.GetBoolean("AllowAnyField");
                    this.GroupFields = info.GetBoolean("GroupFields");
                    this.AllowAnyAllOps = info.GetBoolean("AllowAnyAllOps");
                    this.Indent = info.GetBoolean("Indent");
                    this.AnyIsAll = info.GetBoolean("AnyIsAll");
                }
                catch
                {
                }
                this.supportAllowMultiEnter = (this.supportAutoSuggest = (this.requirePostLoadRendering = true));
            }
            internal CamlOperator GetDefaultOperator(string fieldType, bool safe, CamlOperator defOp)
            {
                CamlOperator[] operators = this.GetOperators(fieldType, safe);
                CamlOperator camlOperator = (Array.IndexOf<CamlOperator>(operators, defOp) >= 0) ? defOp : operators[0];
                if (camlOperator != roxority.SharePoint.CamlOperator.Me && camlOperator != roxority.SharePoint.CamlOperator.NotMe)
                {
                    return camlOperator;
                }
                return defOp;
            }
            internal IEnumerable<FilterBase.Multi.FieldDesc> GetFields(FilterBase.Multi.FieldCat match)
            {
                bool flag = false;
                bool flag2 = this.HasPeop();
                if (this.fields == null && this.parentWebPart != null)
                {
                    this.fields = new List<FilterBase.Multi.FieldDesc>();
                    List<string> list = new List<string>();
                    if (this.parentWebPart.connectedList != null)
                    {
                        if (this.parentWebPart.connectedView != null)
                        {
                            foreach (string text in this.parentWebPart.connectedView.ViewFields)
                            {
                                bool flag3;
                                if ((!(flag3 = this.IsTitle(text)) || !flag) && !list.Contains(text.ToLowerInvariant()) && this.IsAllowed(text))
                                {
                                    FilterBase.Multi.FieldDesc fieldDesc = new FilterBase.Multi.FieldDesc
                                    {
                                        Name = text,
                                        Title = text,
                                        Cat = FilterBase.Multi.FieldCat.ListView
                                    };
                                    SPField sPField;
                                    if ((sPField = ProductPage.GetField(this.parentWebPart.connectedList, text)) != null)
                                    {
                                        fieldDesc.SPField = sPField;
                                        fieldDesc.Title = sPField.Title;
                                    }
                                    if (sPField == null || (int)sPField.Type != 12)
                                    {
                                        if (flag3)
                                        {
                                            flag = true;
                                        }
                                        list.Add(text.ToLowerInvariant());
                                        this.fields.Add(fieldDesc);
                                    }
                                }
                            }
                        }
                        foreach (SPField current in ProductPage.TryEach<SPField>(this.parentWebPart.connectedList.Fields))
                        {
                            bool flag3;
                            if ((!(flag3 = this.IsTitle(current.InternalName)) || !flag) && !list.Contains(current.InternalName.ToLowerInvariant()) && this.IsAllowed(current.InternalName) && (int)current.Type != 12)
                            {
                                if (flag3)
                                {
                                    flag = true;
                                }
                                list.Add(current.InternalName.ToLowerInvariant());
                                this.fields.Add(new FilterBase.Multi.FieldDesc
                                {
                                    Cat = current.FromBaseType ? FilterBase.Multi.FieldCat.ListSystem : ((current.UsedInWebContentTypes || current.SourceId == "http://schemas.microsoft.com/sharepoint/v3") ? FilterBase.Multi.FieldCat.ListInherited : FilterBase.Multi.FieldCat.ListOther),
                                    SPField = current,
                                    Name = current.InternalName,
                                    Title = current.Title
                                });
                            }
                        }
                    }
                    foreach (KeyValuePair<string, string> current2 in this.parentWebPart.validFilterNames)
                    {
                        bool flag3;
                        SPField sPField;
                        if ((!flag2 || current2.Key != "Title") && (!(flag3 = this.IsTitle(current2.Key)) || !flag) && !list.Contains(current2.Key.ToLowerInvariant()) && this.IsAllowed(current2.Key) && ((sPField = ((this.parentWebPart == null || this.parentWebPart.connectedList == null) ? null : ProductPage.GetField(this.parentWebPart.connectedList, current2.Key))) == null || (int)sPField.Type != 12))
                        {
                            if (flag3)
                            {
                                flag = true;
                            }
                            list.Add(current2.Key.ToLowerInvariant());
                            this.fields.Add(new FilterBase.Multi.FieldDesc
                            {
                                Cat = FilterBase.Multi.FieldCat.ListOther,
                                Name = current2.Key,
                                Title = string.IsNullOrEmpty(current2.Value) ? current2.Key : current2.Value
                            });
                        }
                    }
                    string[] array = this.CustomFieldNames;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text2 = array[i];
                        bool flag3;
                        SPField sPField;
                        string[] array2;
                        if (!string.IsNullOrEmpty(text2) && (array2 = text2.Split(new string[]
						{
							";#"
						}, StringSplitOptions.RemoveEmptyEntries)) != null && array2.Length > 0 && (!(flag3 = this.IsTitle(array2[0])) || !flag) && !list.Contains(array2[0].ToLowerInvariant()) && this.IsAllowed(array2[0]) && ((sPField = ((this.parentWebPart == null || this.parentWebPart.connectedList == null) ? null : ProductPage.GetField(this.parentWebPart.connectedList, array2[0]))) == null || (int)sPField.Type != 12))
                        {
                            if (flag3)
                            {
                                flag = true;
                            }
                            list.Add(array2[0].ToLowerInvariant());
                            this.fields.Add(new FilterBase.Multi.FieldDesc
                            {
                                Cat = FilterBase.Multi.FieldCat.Custom,
                                Name = array2[0],
                                Title = array2[(array2.Length > 1) ? 1 : 0]
                            });
                        }
                    }
                    if (this.fields.Count == 0)
                    {
                        this.fields = null;
                    }
                    else
                    {
                        this.fields.Sort(new Comparison<FilterBase.Multi.FieldDesc>(FilterBase.Multi.FieldDesc.Compare));
                    }
                }
                if (this.fields != null && match != FilterBase.Multi.FieldCat.All)
                {
                    return this.fields.FindAll((FilterBase.Multi.FieldDesc fd) => Array.IndexOf<string>(this.CustomFieldNames, fd.Name) >= 0 || Array.IndexOf<string>(this.ListFieldNames, fd.Name) >= 0 || (match != FilterBase.Multi.FieldCat.None && fd.Cat != FilterBase.Multi.FieldCat.None && (fd.Cat & match) == fd.Cat) || Array.Exists<string>(this.CustomFieldNames, (string cfn) => cfn.StartsWith(fd.Name + ";#")));
                }
                return this.fields;
            }
            internal string GetJson()
            {
                int num = 0;
                StringBuilder stringBuilder = new StringBuilder();
                IEnumerable<KeyValuePair<string, string>> filterPairs;
                if (this.fps == null && (filterPairs = this.FilterPairs) != null)
                {
                    using (IEnumerator<KeyValuePair<string, string>> enumerator = filterPairs.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            KeyValuePair<string, string> arg_2B_0 = enumerator.Current;
                        }
                    }
                }
                int count;
                if (this.fps != null && (count = this.fps.Count) > 0)
                {
                    if (count > 1)
                    {
                        num++;
                        stringBuilder.Append("{\"" + (this.fps[0].nextAnd ? "AND" : "OR") + "\":[");
                    }
                    for (int i = 0; i < count; i++)
                    {
                        stringBuilder.Append(" \"" + this.fps[i].Key + "\" ");
                        if (i < count - 1)
                        {
                            stringBuilder.Append(",");
                        }
                        if (i < count - 2)
                        {
                            num++;
                            stringBuilder.Append("{\"" + (this.fps[i + 1].nextAnd ? "AND" : "OR") + "\":[");
                        }
                    }
                    for (int j = 0; j < num; j++)
                    {
                        stringBuilder.Append("]}");
                    }
                }
                return stringBuilder.ToString();
            }
            internal CamlOperator GetOperator(string name,string value,int position, CamlOperator defVal) //modified by lhan
            {
                IEnumerable<FilterBase.Multi.FieldDesc> enumerable = this.GetFields(FilterBase.Multi.FieldCat.All);
                FilterBase.Multi.FieldDesc fieldDesc = null;
                if (enumerable != null)
                {
                    foreach (FilterBase.Multi.FieldDesc current in enumerable)
                    {
                        if (current.Name == name && (fieldDesc == null || fieldDesc.SPField == null))
                        {
                            fieldDesc = current;
                        }
                    }
                }
                IEnumerable<KeyValuePair<string, string>> filterPairs;
                if (this.fps == null && (filterPairs = this.FilterPairs) != null)
                {
                    using (IEnumerator<KeyValuePair<string, string>> enumerator2 = filterPairs.GetEnumerator())
                    {
                        if (enumerator2.MoveNext())
                        {
                            KeyValuePair<string, string> arg_75_0 = enumerator2.Current;
                        }
                    }
                }
                if (this.fps != null)
                {

                    //foreach (roxority_FilterWebPart.FilterPair current2 in this.fps)
                    //{
                        //if (current2.Key == name)
                        //{
                        //    return (fieldDesc == null || fieldDesc.SPField == null) ? current2.CamlOperator : this.GetDefaultOperator(fieldDesc.SPField.TypeAsString, !this.AllowAnyAllOps, current2.CamlOperator);
                        //}
                    //}

                    if (this.fps.Count > position && this.fps[position].Key == name && this.fps[position].Value == value)  //modified by:lhan
                    {
                        return (fieldDesc == null || fieldDesc.SPField == null) ? this.fps[position].CamlOperator : this.GetDefaultOperator(fieldDesc.SPField.TypeAsString, !this.AllowAnyAllOps, this.fps[position].CamlOperator);
                    }
                    else
                    {
                        foreach (roxority_FilterWebPart.FilterPair current2 in this.fps)
                        {
                            if (current2.Key == name && current2.Value == value) //modified by lhan
                            {
                                return (fieldDesc == null || fieldDesc.SPField == null) ? current2.CamlOperator : this.GetDefaultOperator(fieldDesc.SPField.TypeAsString, !this.AllowAnyAllOps, current2.CamlOperator);
                            }
                        }
                    }

                    return defVal;
                }
                return defVal;
            }
            internal string GetOperatorOptions(CamlOperator[] ops)
            {
                string text = string.Empty;
                for (int i = 0; i < ops.Length; i++)
                {
                    CamlOperator camlOperator = ops[i];
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						"<option value=\"",
						camlOperator,
						"\">",
						HttpUtility.HtmlEncode(base["CamlOp_" + camlOperator, new object[0]]),
						"</option>"
					});
                }
                return text;
            }
            internal string GetOperators(string fieldType)
            {
                CamlOperator[] operators = this.GetOperators(fieldType, !this.AllowAnyAllOps);
                if (operators == FilterBase.Multi.numberOperators)
                {
                    return "roxMultiOpsNum";
                }
                if (operators == FilterBase.Multi.stringOperators)
                {
                    return "roxMultiOpsStr";
                }
                if (operators == FilterBase.Multi.userOperators)
                {
                    return "roxMultiOpsUser";
                }
                if (operators == FilterBase.Multi.allOperators)
                {
                    return "roxMultiOpsAll";
                }
                if (operators == FilterBase.Multi.anyOperators)
                {
                    return "roxMultiOpsAny";
                }
                return "roxMultiOpsNone";
            }
            internal CamlOperator[] GetOperators(string fieldType, bool safe)
            {
                if (Array.IndexOf<string>(FilterBase.Multi.numOpTypes, fieldType) >= 0)
                {
                    return FilterBase.Multi.numberOperators;
                }
                if (Array.IndexOf<string>(FilterBase.Multi.strOpTypes, fieldType) >= 0)
                {
                    return FilterBase.Multi.stringOperators;
                }
                if (Array.IndexOf<string>(FilterBase.Multi.userOpTypes, fieldType) >= 0)
                {
                    return FilterBase.Multi.userOperators;
                }
                if (!safe && Array.IndexOf<string>(FilterBase.Multi.boolOpTypes, fieldType) < 0)
                {
                    return FilterBase.Multi.allOperators;
                }
                return FilterBase.Multi.anyOperators;
            }
            internal string GetFieldOptions(FilterBase.Multi.FieldCat match, bool optGroups, bool showFieldNames, bool showCustom, string[] selected, out int titlePos)
            {
                int num = 0;
                string text = string.Empty;
                bool hasPeop = this.HasPeop();
                FilterBase.Multi.FieldCat fieldCat = FilterBase.Multi.FieldCat.None;
                titlePos = -1;
                IEnumerable<FilterBase.Multi.FieldDesc> enumerable;
                if ((enumerable = this.GetFields(match)) != null)
                {
                    if (!optGroups)
                    {
                        List<FilterBase.Multi.FieldDesc> list = new List<FilterBase.Multi.FieldDesc>(enumerable);
                        list.Sort(delegate(FilterBase.Multi.FieldDesc one, FilterBase.Multi.FieldDesc two)
                        {
                            if (!hasPeop && one.Title == "Title" && two.Title != "Title")
                            {
                                return "a".CompareTo("b");
                            }
                            if (!hasPeop && two.Title == "Title" && one.Title != "Title")
                            {
                                return "b".CompareTo("a");
                            }
                            return one.Title.CompareTo(two.Title);
                        });
                        enumerable = list;
                    }
                    foreach (FilterBase.Multi.FieldDesc current in enumerable)
                    {
                        if (current.Cat != FilterBase.Multi.FieldCat.Custom || showCustom)
                        {
                            if (optGroups && current.Cat != fieldCat)
                            {
                                if (optGroups && fieldCat != FilterBase.Multi.FieldCat.None)
                                {
                                    text += "</optgroup>";
                                }
                                text = text + "<optgroup label=\"" + HttpUtility.HtmlEncode(base["FieldCat_" + (fieldCat = current.Cat), new object[0]]) + "\">";
                                num++;
                            }
                            if (!hasPeop && current.Name == "Title")
                            {
                                titlePos = num;
                            }
                            num++;
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", HttpUtility.HtmlEncode(current.Name), HttpUtility.HtmlEncode(current.Title) + ((current.Name.Equals(current.Title) || !showFieldNames) ? string.Empty : string.Format(" &mdash; [ " + HttpUtility.HtmlEncode(current.Name) + " ]", new object[0])), (Array.IndexOf<string>(selected, current.Name) >= 0) ? " selected=\"selected\"" : string.Empty);
                        }
                    }
                }
                if (optGroups && !string.IsNullOrEmpty(text))
                {
                    text += "</optgroup>";
                }
                return text;
            }
            internal bool HasPeop()
            {
                if ((!this.hasPeop.HasValue || !this.hasPeop.HasValue) && this.parentWebPart != null && this.parentWebPart.connectedParts != null)
                {
                    this.hasPeop = new bool?(false);
                    foreach (WebPart current in this.parentWebPart.connectedParts)
                    {
                        if (current.GetType().Name == "roxority_UserListWebPart")
                        {
                            this.hasPeop = new bool?(true);
                            break;
                        }
                    }
                }
                return this.hasPeop.HasValue && this.hasPeop.Value;
            }
            internal bool IsAnd(string name,string value,int position, bool defVal) //modified by:lhan
            {
                IEnumerable<KeyValuePair<string, string>> filterPairs;
                if (this.fps == null && (filterPairs = this.FilterPairs) != null)
                {
                    using (IEnumerator<KeyValuePair<string, string>> enumerator = filterPairs.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            KeyValuePair<string, string> arg_21_0 = enumerator.Current;
                        }
                    }
                }
                if (this.fps != null)
                {
                    //foreach (roxority_FilterWebPart.FilterPair current in this.fps)
                    //{
                    //    if (current.Key == name && current.Value == value)
                    //    {
                    //        return current.nextAnd;
                    //    }
                    //}

                    if (this.fps.Count > position && this.fps[position].Key == name && this.fps[position].Value == value) //modified by:lhan
                    {
                        return this.fps[position].nextAnd;
                    }
                    else
                    {
                        foreach (roxority_FilterWebPart.FilterPair current in this.fps)
                        {
                            if (current.Key == name && current.Value == value)
                            {
                                return current.nextAnd;
                            }
                        }
                    }
                    return defVal;
                }
                return defVal;
            }
            internal bool IsAllowed(string name)
            {
                return this.parentWebPart == null || this.parentWebPart.connectedList == null || (ProductPage.GetField(this.parentWebPart.connectedList, name) != null && Array.IndexOf<string>(FilterBase.Multi.blockedFields, name) < 0 && (!name.EndsWith("2", StringComparison.InvariantCultureIgnoreCase) || Array.IndexOf<string>(FilterBase.Multi.blockedFields, name.Substring(0, name.Length - 1)) < 0));
            }
            internal bool IsTitle(string name)
            {
                return !this.HasPeop() && Array.IndexOf<string>(FilterBase.Multi.titleFields, name) >= 0;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("ListFieldNames", this.ListFieldNames, typeof(string[]));
                info.AddValue("CustomFieldNames", this.CustomFieldNames, typeof(string[]));
                info.AddValue("AllowAllInheritedFields", this.AllowAllInheritedFields);
                info.AddValue("AllowAllListFields", this.AllowAllListFields);
                info.AddValue("AllowAllOtherFields", this.AllowAllOtherFields);
                info.AddValue("AllowAllViewFields", this.AllowAllViewFields);
                info.AddValue("AllowAnyField", this.AllowAnyField);
                info.AddValue("AllowAnyAllOps", this.AllowAnyAllOps);
                info.AddValue("GroupFields", this.GroupFields);
                info.AddValue("Indent", this.Indent);
                info.AddValue("AnyIsAll", this.AnyIsAll);
                base.GetObjectData(info, context);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                string arg_05_0 = string.Empty;
                string text = this.AllowAnyField ? "*" : string.Empty;
                string[] array = new string[]
				{
					"CamlOp_And",
					"CamlOp_Or",
					"MultiChecked"
				};
                bool flag = base.AutoSuggest && this.parentWebPart.connectedList != null;
                int num = 40;
                IEnumerable<FilterBase.Multi.FieldDesc> enumerable = this.GetFields(this.FieldMask);
                List<FilterBase.Multi.FieldDesc> list = enumerable as List<FilterBase.Multi.FieldDesc>;
                if (!int.TryParse(ProductPage.Config(ProductPage.GetContext(), "AutoLimit"), out num) || num <= 1)
                {
                    num = 40;
                }
                if (string.IsNullOrEmpty(text) && enumerable != null)
                {
                    using (IEnumerator<FilterBase.Multi.FieldDesc> enumerator = enumerable.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            FilterBase.Multi.FieldDesc current = enumerator.Current;
                            text = current.Name;
                        }
                    }
                }
                string arg_434_1 = "\r\n<script type=\"text/javascript\" language=\"JavaScript\">\r\nroxMultiOpsAll = '{12}';\r\nroxMultiOpsAny = '{13}';\r\nroxMultiOpsNone = '{14}';\r\nroxMultiOpsNum = '{15}';\r\nroxMultiOpsStr = '{16}';\r\nroxMultiOpsUser = '{22}';\r\nroxMultis['{2}'] = {0}\r\n\tfieldOpts: '{11}',\r\n\tctlID: '{3}',\r\n\tindent: {10},\r\n\tallowOps: {5},\r\n\tallowAnyAllOps: {6},\r\n\tdefField: '{8}',\r\n\tdefOp: '{4}',\r\n\tdefLop: '{7}',\r\n\tisCaml : {9},\r\n\tcfg: null,\r\n\tfields: {17},\r\n\tfieldTypes: {18},\r\n\tallowMulti: {23},\r\n\tautoComplete: {0}\r\n\t\tactive: {19},\r\n\t\turl: '{20}',\r\n\t\topts: {0}\r\n\t\t\tmax: {21},\r\n\t\t\tdelay: 100,\r\n\t\t\tminChars: 1,\r\n\t\t\tselectFirst: false,\r\n\t\t\tmultiple: false,\r\n\t\t\tcacheLength: 0,\r\n\t\t\tmatchContains: true\r\n\t\t{1}\r\n\t{1}\r\n{1};\r\nroxMultiDateCounts['{2}'] = 0;\r\nroxMultiUserCounts['{2}'] = 0;\r\nroxMultiInit('{2}');\r\n";
                object[] array2 = new object[24];
                array2[0] = "{";
                array2[1] = "}";
                array2[2] = base.ID;
                array2[3] = this.parentWebPart.MultiTextBox.ClientID;
                array2[4] = (CamlOperator)base.CamlOperator;
                array2[5] = this.parentWebPart.CamlFilters.ToString().ToLowerInvariant();
                array2[6] = this.AllowAnyAllOps.ToString().ToLowerInvariant();
                array2[7] = (this.parentWebPart.DefaultToOr ? "Or" : "And");
                array2[8] = SPEncode.ScriptEncode(text);
                array2[9] = this.parentWebPart.CamlFilters.ToString().ToLowerInvariant();
                array2[10] = this.Indent.ToString().ToLowerInvariant();
                int num2;
                array2[11] = SPEncode.ScriptEncode((this.AllowAnyField ? ("<option value=\"*\">" + base["FieldCat_Any", new object[0]] + "</option>") : string.Empty) + this.GetFieldOptions(this.FieldMask, this.GroupFields, false, true, new string[0], out num2));
                array2[12] = SPEncode.ScriptEncode(this.GetOperatorOptions(FilterBase.Multi.allOperators));
                array2[13] = SPEncode.ScriptEncode(this.GetOperatorOptions(FilterBase.Multi.anyOperators));
                object[] arg_259_0 = array2;
                int arg_259_1 = 14;
                CamlOperator[] ops = new CamlOperator[1];
                arg_259_0[arg_259_1] = SPEncode.ScriptEncode(this.GetOperatorOptions(ops));
                array2[15] = SPEncode.ScriptEncode(this.GetOperatorOptions(FilterBase.Multi.numberOperators));
                array2[16] = SPEncode.ScriptEncode(this.GetOperatorOptions(FilterBase.Multi.stringOperators));
                array2[17] = ((list == null) ? "null" : ("{" + string.Join(",", list.ConvertAll<string>((FilterBase.Multi.FieldDesc fd) => string.Format("\"{0}\": {1}", fd.Name, this.GetOperators((fd.SPField == null) ? string.Empty : fd.SPField.TypeAsString))).ToArray()) + "}"));
                object[] arg_316_0 = array2;
                int arg_316_1 = 18;
                string arg_316_2;
                if (list != null)
                {
                    arg_316_2 = "{" + string.Join(",", list.ConvertAll<string>((FilterBase.Multi.FieldDesc fd) => string.Format("\"{0}\": \"{1}\"", fd.Name, (fd.SPField == null) ? string.Empty : fd.SPField.TypeAsString)).ToArray()) + "}";
                }
                else
                {
                    arg_316_2 = "null";
                }
                arg_316_0[arg_316_1] = arg_316_2;
                array2[19] = flag.ToString().ToLowerInvariant();
                array2[20] = (flag ? SPEncode.ScriptEncode(string.Concat(new string[]
				{
					this.parentWebPart.connectedList.ParentWeb.Url.TrimEnd(new char[]
					{
						'/'
					}),
					"/_layouts/roxority_FilterZen/jqas.aspx?v=",
					(this.parentWebPart.connectedView == null) ? string.Empty : ProductPage.GuidLower(this.parentWebPart.connectedView.ID, true),
					"&l=",
					ProductPage.GuidLower(this.parentWebPart.connectedList.ID, true),
					"&sf=",
					HttpUtility.UrlEncode(this.parentWebPart.AcSecFields)
				})) : string.Empty);
                array2[21] = num;
                array2[22] = SPEncode.ScriptEncode(this.GetOperatorOptions(FilterBase.Multi.userOperators));
                array2[23] = this.Get<bool>("AllowMultiEnter").ToString().ToLowerInvariant();
                output.Write(arg_434_1, array2);
                string[] array3 = array;
                for (int i = 0; i < array3.Length; i++)
                {
                    string text2 = array3[i];
                    output.Write(string.Concat(new string[]
					{
						"roxLox['",
						text2,
						"'] = '",
						SPEncode.ScriptEncode(base[text2, new object[0]]),
						"';"
					}));
                }
                output.Write(string.Concat(new string[]
				{
					"</script><div class=\"rox-multibox rox-multibox-",
					base.ID,
					" rox-multibox-",
					this.parentWebPart.ClientID,
					"\">"
				}));
                output.Write("</div>");
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = string.Empty;
                string text2 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
                string text3 = "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                string text4 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
                this.hiddenProperties.AddRange(new string[]
				{
					"Label",
					"DefaultIfEmpty",
					"IsInteractive",
					"SendEmpty",
					"SuppressMultiValues",
					"MultiFilterSeparator"
				});
                this.fields = null;
                panel.Controls.Add(new LiteralControl("<div>" + base["FilterDesc_Multi", new object[0]] + "</div>"));
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + FilterBase.GetFilterTypeTitle(base.GetType()) + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\">"));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAllViewFields", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAllViewFields"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAllListFields", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAllListFields"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAllInheritedFields", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAllInheritedFields"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAllOtherFields", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAllOtherFields"))
				}));
                int num;
                text = this.GetFieldOptions(FilterBase.Multi.FieldCat.All, true, true, false, this.ListFieldNames, out num);
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text2, "ListFieldNames", new object[]
				{
					" size=\"" + ((num > 10) ? (num + 2) : 12) + "\" multiple=\"multiple\"",
					text
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>" : text4, "CustomFieldNames", new object[]
				{
					this.Get<string>("CustomFieldNames")
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAnyField", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAnyField"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AnyIsAll", new object[]
				{
					base.GetChecked(this.Get<bool>("AnyIsAll"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "AllowAnyAllOps", new object[]
				{
					base.GetChecked(this.Get<bool>("AllowAnyAllOps"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "GroupFields", new object[]
				{
					base.GetChecked(this.Get<bool>("GroupFields"))
				}));
                panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : text3, "Indent", new object[]
				{
					base.GetChecked(this.Get<bool>("Indent"))
				}));
                panel.Controls.Add(new LiteralControl("</fieldset><style type=\"text/css\"> div#div_filter_Label, div#div_filter_IsInteractive { display: none !important; visibility: hidden !important; } </style>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                HttpContext context = base.Context;
                string text = (context == null) ? this.Get<string>("CustomFieldNames") : context.Request["filter_CustomFieldNames"];
                string text2 = (context == null) ? this.Get<string>("ListFieldNames") : context.Request["filter_ListFieldNames"];
                if (text == null)
                {
                    text = string.Empty;
                }
                if (text2 == null)
                {
                    text2 = string.Empty;
                }
                this.CustomFieldNames = text.Split(new char[]
				{
					'\r',
					'\n'
				}, StringSplitOptions.RemoveEmptyEntries);
                this.ListFieldNames = text2.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
                this.AllowAllInheritedFields = this.Get<bool>("AllowAllInheritedFields");
                this.AllowAllListFields = this.Get<bool>("AllowAllListFields");
                this.AllowAllOtherFields = this.Get<bool>("AllowAllOtherFields");
                this.AllowAllViewFields = this.Get<bool>("AllowAllViewFields");
                this.AllowAnyField = this.Get<bool>("AllowAnyField");
                this.AllowAnyAllOps = this.Get<bool>("AllowAnyAllOps");
                this.GroupFields = this.Get<bool>("GroupFields");
                this.Indent = this.Get<bool>("Indent");
                this.AnyIsAll = this.Get<bool>("AnyIsAll");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal sealed class PageField : FilterBase.Lookup
        {
            public PageField()
            {
                this.Init();
                this.defaultIfEmpty = false;
            }
            public PageField(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.Init();
            }
            private void Init()
            {
                this.suppressInteractive = true;
                this.hiddenProperties.AddRange(new string[]
				{
					"ListUrl",
					"DisplayFieldName",
					"ItemID",
					"RemoveDuplicateValues",
					"RemoveDuplicateTitles",
					"ItemSorting",
					"FilterCaml"
				});
                this.itemID = -1;
            }
            public override void UpdatePanel(Panel panel)
            {
                using (SPWrap<SPList> list = base.GetList("ListUrl", true))
                {
                    if (list.Value == null)
                    {
                        base.Report(new Exception(base["NoPageLibrary", new object[0]]));
                    }
                }
                base.UpdatePanel(panel);
            }
        }
        [Serializable]
        internal sealed class RequestParameter : FilterBase
        {
            private static List<string> blockedParams;
            private bool catchAll;
            private bool sendNull;
            private int requestMode;
            private string parameterName = string.Empty;
            private string subParameterName = string.Empty;
            internal static List<string> BlockedParams
            {
                get
                {
                    if (FilterBase.RequestParameter.blockedParams == null)
                    {
                        FilterBase.RequestParameter.blockedParams = new List<string>(ProductPage.Config(SPContext.Current, "BlockedParams").Split(new char[]
						{
							'\r',
							'\n'
						}, StringSplitOptions.RemoveEmptyEntries)).ConvertAll<string>((string value) => value.ToLowerInvariant());
                    }
                    return FilterBase.RequestParameter.blockedParams;
                }
            }
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    List<NameObjectCollectionBase> list = new List<NameObjectCollectionBase>();
                    bool flag = this.Get<bool>("CatchAll");
                    string text = this.GetParameterName();
                    string text2 = this.Get<string>("SubParameterName");
                    List<string> list2 = (flag && base.Name == "?") ? new List<string>() : this.GetParameterValues(flag ? base.Name : text, flag ? string.Empty : text2);
                    if (flag)
                    {
                        if (base.Name == "?")
                        {
                            if (this.RequestMode != 1)
                            {
                                list.Add(base.Context.Request.QueryString);
                            }
                            if (this.RequestMode != 0)
                            {
                                list.Add(base.Context.Request.Form);
                            }
                            if (this.RequestMode != 0 && this.RequestMode != 1)
                            {
                                list.Add(base.Context.Request.Cookies);
                                list.Add(base.Context.Request.ServerVariables);
                            }
                            foreach (NameObjectCollectionBase current in list)
                            {
                                NameValueCollection nameValueCollection = current as NameValueCollection;
                                foreach (string text3 in current.Keys)
                                {
                                    if (!string.IsNullOrEmpty(text3))
                                    {
                                        if (FilterBase.RequestParameter.BlockedParams.Contains(text3.ToLowerInvariant()))
                                        {
                                            if (this.parentWebPart != null)
                                            {
                                                this.parentWebPart.filtersNotSent.Add(new KeyValuePair<string, string>(text3, "Blocked"));
                                            }
                                        }
                                        else
                                        {
                                            string[] values;
                                            if (nameValueCollection == null || (values = nameValueCollection.GetValues(text3)) == null || values.Length == 0)
                                            {
                                                yield return new KeyValuePair<string, string>(text3, (nameValueCollection != null) ? nameValueCollection[text3] : base.Context.Request.Cookies[text3].Value);
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    string[] array = values;
                                                    for (int i = 0; i < array.Length; i++)
                                                    {
                                                        string value = array[i];
                                                        yield return new KeyValuePair<string, string>(text3, value);
                                                    }
                                                }
                                                finally
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (list2.Count == 0 && this.Get<bool>("SendNull"))
                            {
                                yield return new KeyValuePair<string, string>(text, string.Empty);
                            }
                            foreach (string current2 in list2)
                            {
                                try
                                {
                                    string[] array2 = current2.Split(new string[]
									{
										"\" "
									}, StringSplitOptions.RemoveEmptyEntries);
                                    for (int j = 0; j < array2.Length; j++)
                                    {
                                        string text4 = array2[j];
                                        string text5;
                                        yield return new KeyValuePair<string, string>(text4.Substring(0, text4.IndexOf(':')), (text5 = text4.Substring(text4.IndexOf(":\"") + 2)).Substring(0, text5.Length - (text5.EndsWith("\"") ? 1 : 0)));
                                    }
                                }
                                finally
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        if (list2.Count == 0 && this.Get<bool>("SendNull"))
                        {
                            yield return new KeyValuePair<string, string>(base.Name, string.Empty);
                        }
                        foreach (string current3 in list2)
                        {
                            string text5;
                            if (!string.IsNullOrEmpty(text5 = ProductPage.Trim(current3, new char[0])) || this.Get<bool>("SendNull"))
                            {
                                yield return new KeyValuePair<string, string>(base.Name, text5);
                            }
                        }
                    }
                    yield break;
                }
            }
            public bool CatchAll
            {
                get
                {
                    return this.catchAll;
                }
                set
                {
                    this.catchAll = value;
                }
            }
            public string ParameterName
            {
                get
                {
                    return this.parameterName;
                }
                set
                {
                    this.parameterName = ProductPage.Trim(value, new char[0]);
                }
            }
            public int RequestMode
            {
                get
                {
                    return this.requestMode;
                }
                set
                {
                    this.requestMode = ((value >= 1 && value <= 3) ? value : 0);
                }
            }
            public bool SendNull
            {
                get
                {
                    return this.sendNull;
                }
                set
                {
                    this.sendNull = value;
                }
            }
            public string SubParameterName
            {
                get
                {
                    return this.subParameterName;
                }
                set
                {
                    this.subParameterName = ProductPage.Trim(value, new char[0]);
                }
            }
            public RequestParameter()
            {
            }
            public RequestParameter(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                try
                {
                    this.CatchAll = info.GetBoolean("CatchAll");
                    this.ParameterName = info.GetString("ParameterName");
                    this.RequestMode = info.GetInt32("RequestMode");
                    this.SendNull = info.GetBoolean("SendNull");
                    this.SubParameterName = info.GetString("SubParameterName");
                }
                catch
                {
                }
            }
            internal string GetParameterName()
            {
                string text = this.Get<string>("ParameterName");
                if (string.IsNullOrEmpty(text))
                {
                    text = base.Name;
                }
                List<string> parameterValues;
                if (text.StartsWith("{{") && text.EndsWith("}}") && text.Length > 4 && (parameterValues = this.GetParameterValues(text.Substring(2, text.Length - 4), string.Empty)) != null && parameterValues.Count > 0)
                {
                    text = parameterValues[0];
                }
                return text;
            }
            internal List<string> GetParameterValues(string paramName, string subParamName)
            {
                HttpCookie httpCookie = null;
                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                NameValueCollection nameValueCollection = (this.RequestMode == 0) ? base.Context.Request.QueryString : ((this.RequestMode == 1) ? base.Context.Request.Form : null);
                if (this.RequestMode == 3)
                {
                    list.AddRange(base.Context.Request.Url.ToString().Split(new char[]
					{
						'/'
					}));
                    int num;
                    if (!string.IsNullOrEmpty(paramName = ProductPage.Trim(paramName, new char[0])) && (num = list.IndexOf(paramName)) >= 0 && num < list.Count - 1)
                    {
                        list2.Add(list[num + 1]);
                    }
                }
                else
                {
                    if (this.RequestMode == 2)
                    {
                        NameValueCollection[] array = new NameValueCollection[]
						{
							base.Context.Request.QueryString,
							base.Context.Request.Form,
							base.Context.Request.ServerVariables,
							(Array.IndexOf<string>(base.Context.Request.Cookies.AllKeys, paramName) >= 0 && (httpCookie = base.Context.Request.Cookies[paramName]) != null) ? httpCookie.Values : null
						};
                        for (int i = 0; i < array.Length; i++)
                        {
                            NameValueCollection nameValueCollection2 = array[i];
                            string[] values;
                            if (nameValueCollection2 != null && (values = nameValueCollection2.GetValues(paramName)) != null && values.Length > 0)
                            {
                                list2.AddRange(values);
                            }
                            else
                            {
                                if (nameValueCollection2 != null && httpCookie != null && nameValueCollection2 == httpCookie.Values)
                                {
                                    for (int j = 0; j < nameValueCollection2.Count; j++)
                                    {
                                        list2.Add(nameValueCollection2[j]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] values;
                        if ((values = nameValueCollection.GetValues(paramName)) != null && values.Length > 0)
                        {
                            list2.AddRange(values);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(subParamName))
                {
                    int count = list2.Count;
                    foreach (string current in list2)
                    {
                        int num;
                        string text;
                        string text2;
                        if ((num = (text = current).IndexOf(text2 = subParamName + ":\"")) >= 0)
                        {
                            list2.Add(((num = (text = text.Substring(num + text2.Length)).IndexOf('"')) >= 0) ? text.Substring(0, num) : text);
                        }
                    }
                    list2.RemoveRange(0, count);
                }
                return list2;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("CatchAll", this.CatchAll);
                info.AddValue("ParameterName", this.ParameterName);
                info.AddValue("RequestMode", this.RequestMode);
                info.AddValue("SendNull", this.SendNull);
                info.AddValue("SubParameterName", this.SubParameterName);
                base.GetObjectData(info, context);
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = "";
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "ParameterName", new object[]
				{
					this.GetParameterName(),
					" onchange=\"roxUpdatePreview();\" onkeyup=\"roxUpdatePreview();\""
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>", "SubParameterName", new object[]
				{
					this.Get<string>("SubParameterName"),
					" onchange=\"roxUpdatePreview();\" onkeyup=\"roxUpdatePreview();\""
				}));
                for (int i = 0; i <= 3; i++)
                {
                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", i, base["RequestMode" + i, new object[0]], (i == this.Get<int>("RequestMode")) ? " selected=\"selected\"" : string.Empty);
                }
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>", "RequestMode", new object[]
				{
					" onchange=\"roxUpdatePreview();\"",
					text
				}));
                panel.Controls.Add(new LiteralControl(string.Format("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><span id=\"span_Preview\"></span></div>", "Preview", base["Prop_Preview", new object[0]])));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "SendNull", new object[]
				{
					base.GetChecked(this.Get<bool>("SendNull"))
				}));
                panel.Controls.Add(base.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "CatchAll", new object[]
				{
					string.Empty,
					"document.getElementById('div_filter_ParameterName').style.display=document.getElementById('div_filter_SubParameterName').style.display=document.getElementById('div_filter_SendNull').style.display=((this.checked)?'none':'block');roxUpdatePreview();"
				}));
                panel.Controls.Add(base.CreateScript(string.Concat(new string[]
				{
					"roxUpdatePreview = function() { if (document.getElementById('filter_RequestMode').selectedIndex == 3) jQuery('#span_Preview').html('<span style=\"color: GrayText;\">http://server/xyz/</span><b>' + jQuery.trim(jQuery('#filter_ParameterName').val()) + '</b>/<i>",
					base["JsValue", new object[0]],
					"</i><span style=\"color: GrayText;\">/xyz/</span>'); else jQuery('#span_Preview').html('<span style=\"color: GrayText;\">page.aspx?' + ((document.getElementById('filter_CatchAll').checked & (jQuery('#' + nameTextBoxID).val() == '?')) ? '</span><b>",
					base["JsFilter", new object[0]],
					"</b>=<i>",
					base["JsValue", new object[0]],
					"</i>&<b>",
					base["JsName", new object[0]],
					"</b>=<i>",
					base["JsValue", new object[0]],
					"</i>&<b>",
					base["JsColumn", new object[0]],
					"</b>=<i>",
					base["JsValue", new object[0]],
					"</i>' : ('x=y&</span><b>' + (((document.getElementById('filter_CatchAll').checked) || (jQuery.trim(jQuery('#filter_ParameterName').val()).length == 0)) ? jQuery('#' + nameTextBoxID).val() : jQuery('#filter_ParameterName').val()) + '</b>=' + (((!document.getElementById('filter_CatchAll').checked) && (jQuery.trim(jQuery('#filter_SubParameterName').val()).length == 0)) ? '<i>",
					base["JsFilterValue", new object[0]],
					"</i>' : (document.getElementById('filter_CatchAll').checked ? ('",
					base["JsFilter", new object[0]],
					"1:\"<i>",
					base["JsValue", new object[0]],
					"1</i>\" ",
					base["JsColumn", new object[0]],
					"2:\"<i>",
					base["JsValue", new object[0]],
					"2</i>\" ",
					base["JsName", new object[0]],
					"3:\"<i>",
					base["JsValue", new object[0]],
					"3</i>\"') : ('a:\"b\" <b>' + jQuery('#filter_SubParameterName').val() + '</b>:\"<i>",
					base["JsFilterValue", new object[0]],
					"</i>\" c:\"d\"'))) + '<span style=\"color: GrayText;\">&x=y')) + '</span>'); jQuery('#div_Preview').css({ 'display': ((document.getElementById('filter_RequestMode').selectedIndex == 1) ? 'none' : 'block') }); }; document.getElementById('div_filter_ParameterName').style.display=document.getElementById('div_filter_SubParameterName').style.display=document.getElementById('div_filter_SendNull').style.display=((document.getElementById('filter_CatchAll').checked=",
					this.Get<bool>("CatchAll") ? "true" : "false",
					")?'none':'block'); jQuery(document).ready(function() { roxUpdatePreview(); });"
				})));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.CatchAll = this.Get<bool>("CatchAll");
                this.ParameterName = this.Get<string>("ParameterName");
                this.RequestMode = this.Get<int>("RequestMode");
                this.SendNull = this.Get<bool>("SendNull");
                this.SubParameterName = this.Get<string>("SubParameterName");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal class SqlData : FilterBase.Interactive
        {
            public const string CHOICE_FIRST = "4aea04a6-787a-4135-81a0-195e5946db1f";
            public const string CHOICE_LAST = "c3bd9af3-8df1-49bd-835b-36f5d64b060c";
            private static readonly string[] choices = new string[]
			{
				"Empty",
				"ResultFirst",
				"ResultLast"
			};
            private static readonly string scriptCheckDefault = "setTimeout('var roxtmp = document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%2\\'); if (document.getElementById(\\'filter_DefaultIfEmpty\\').disabled = ((document.getElementById(\\'filter_%%PLACEHOLDER_LISTID%%\\').selectedIndex == 0) || (roxtmp && (roxtmp.selectedIndex == 0)))) { document.getElementById(\\'label_filter_DefaultIfEmpty\\').style.textDecoration = \\'none\\'; document.getElementById(\\'filter_DefaultIfEmpty\\').checked = true; }', 150);".Replace("%%PLACEHOLDER_LISTID%%", "SqlChoice");
            private string adoConnectionString = string.Empty;
            private string adoDataProvider = string.Empty;
            private string displayColumnName = string.Empty;
            private string query = string.Empty;
            private string valueColumnName = string.Empty;
            private string choice = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            private List<KeyValuePair<int, string[]>> items;
            internal IEnumerable<KeyValuePair<int, string>> Items
            {
                get
                {
                    bool cascade = this.Cascade;
                    bool flag = this.Get<bool>("PostFilter");
                    bool flag2 = this.Get<bool>("DefaultIfEmpty");
                    if (this.items == null)
                    {
                        this.items = new List<KeyValuePair<int, string[]>>();
                        using (IDbConnection dbConnection = this.CreateConnection())
                        {
                            using (IDbCommand dbCommand = dbConnection.CreateCommand())
                            {
                                dbCommand.CommandText = this.Get<string>("Query");
                                dbConnection.Open();
                                using (IDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    while (dataReader.Read())
                                    {
                                        object obj = dataReader[this.Get<string>("ValueColumnName")];
                                        object obj2 = dataReader[string.IsNullOrEmpty(this.Get<string>("DisplayColumnName")) ? this.Get<string>("ValueColumnName") : this.Get<string>("DisplayColumnName")];
                                        this.items.Add(new KeyValuePair<int, string[]>(this.items.Count + 1, new string[]
										{
											(obj2 == null) ? string.Empty : obj2.ToString(),
											(obj == null) ? string.Empty : obj.ToString()
										}));
                                        if (flag2 && !flag && !cascade && base.PickerLimit != 0 && this.items.Count >= base.PickerLimit)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (!flag2)
                        {
                            this.items.RemoveAll((KeyValuePair<int, string[]> kvp) => string.IsNullOrEmpty(kvp.Value[1]));
                        }
                        if (flag)
                        {
                            KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(this.items.ConvertAll<string>((KeyValuePair<int, string[]> kvp) => kvp.Value[1]).ToArray());
                            this.items.RemoveAll((KeyValuePair<int, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                        }
                        if (cascade && this.doPostFilterNow)
                        {
                            KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(this.parentWebPart.connectedList, (this.parentWebPart.connectedView == null) ? this.parentWebPart.connectedList.DefaultView : this.parentWebPart.connectedView, base.Name.StartsWith("@") ? base.Name.Substring(1) : base.Name, this.items.ConvertAll<string>((KeyValuePair<int, string[]> kvp) => kvp.Value[1]).ToArray(), true);
                            this.items.RemoveAll((KeyValuePair<int, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                        }
                    }
                    else
                    {
                        if (this.doPostFilterNow)
                        {
                            KeyValuePair<string[], string[]> postFiltered = base.PostFilterChoices(this.parentWebPart.connectedList, (this.parentWebPart.connectedView == null) ? this.parentWebPart.connectedList.DefaultView : this.parentWebPart.connectedView, base.Name.StartsWith("@") ? base.Name.Substring(1) : base.Name, this.items.ConvertAll<string>((KeyValuePair<int, string[]> kvp) => kvp.Value[1]).ToArray(), true);
                            this.items.RemoveAll((KeyValuePair<int, string[]> kvp) => Array.IndexOf<string>(postFiltered.Value, kvp.Value[1]) >= 0);
                        }
                    }
                    return this.items.ConvertAll<KeyValuePair<int, string>>((KeyValuePair<int, string[]> kvp) => new KeyValuePair<int, string>(kvp.Key, kvp.Value[0]));
                }
            }
            protected internal override IEnumerable<string> AllPickableValues
            {
                get
                {
                    if (this.items == null)
                    {
                        this.Items.ToString();
                    }
                    foreach (KeyValuePair<int, string[]> current in this.items)
                    {
                        KeyValuePair<int, string[]> keyValuePair = current;
                        yield return keyValuePair.Value[1];
                    }
                    yield break;
                }
            }
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    string text = this.Get<string>("SqlChoice");
                    List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, text.ToString());
                    string text2 = string.Empty;
                    Converter<string, string> converter = delegate(string id)
                    {
                        KeyValuePair<int, string[]> keyValuePair = default(KeyValuePair<int, string[]>);
                        if ("0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(id) || string.IsNullOrEmpty(id))
                        {
                            return string.Empty;
                        }
                        if (this.items.Count > 0)
                        {
                            keyValuePair = ((id == "4aea04a6-787a-4135-81a0-195e5946db1f") ? this.items[0] : ((id == "c3bd9af3-8df1-49bd-835b-36f5d64b060c") ? this.items[this.items.Count - 1] : this.items.Find((KeyValuePair<int, string[]> value) => value.Value[1] == id)));
                        }
                        if (keyValuePair.Value == null || keyValuePair.Value.Length < 2)
                        {
                            return string.Empty;
                        }
                        return keyValuePair.Value[1];
                    };
                    if (!base.Le(4, true))
                    {
                        throw new Exception(ProductPage.GetResource("NopeEd", new object[]
						{
							FilterBase.GetFilterTypeTitle(base.GetType()),
							"Ultimate"
						}));
                    }
                    try
                    {
                        if (this.items == null)
                        {
                            this.Items.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        base.Report(ex);
                    }
                    foreach (string current in filterValues)
                    {
                        try
                        {
                            if ("0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(current))
                            {
                                text2 = string.Empty;
                            }
                            else
                            {
                                if ("4aea04a6-787a-4135-81a0-195e5946db1f".Equals(current) || "c3bd9af3-8df1-49bd-835b-36f5d64b060c".Equals(current))
                                {
                                    text2 = converter(text);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(current))
                                    {
                                        text2 = current;
                                    }
                                    else
                                    {
                                        if (text != "0478f8f9-fbdc-42f5-99ea-f6e8ec702606")
                                        {
                                            text2 = converter(text);
                                        }
                                        else
                                        {
                                            text2 = string.Empty;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex2)
                        {
                            base.Report(ex2);
                        }
                        yield return new KeyValuePair<string, string>(base.Name, "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(text2) ? string.Empty : text2);
                    }
                    yield break;
                }
            }
            public string AdoConnectionString
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.adoConnectionString;
                }
                set
                {
                    this.adoConnectionString = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string AdoDataProvider
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.adoDataProvider;
                }
                set
                {
                    this.adoDataProvider = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string SqlChoice
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
                    }
                    return this.choice;
                }
                set
                {
                    this.choice = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? value : "0478f8f9-fbdc-42f5-99ea-f6e8ec702606");
                }
            }
            public override bool DefaultIfEmpty
            {
                get
                {
                    string value = this.Get<string>("SqlChoice");
                    return base.DefaultIfEmpty || string.IsNullOrEmpty(value) || "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(value);
                }
                set
                {
                    string value2 = this.Get<string>("SqlChoice");
                    base.DefaultIfEmpty = (value || string.IsNullOrEmpty(value2) || "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(value2));
                }
            }
            public string DisplayColumnName
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.displayColumnName;
                }
                set
                {
                    this.displayColumnName = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string Query
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.query;
                }
                set
                {
                    this.query = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string ValueColumnName
            {
                get
                {
                    if (this.parentWebPart != null && !this.parentWebPart.LicEd(this.reqEd))
                    {
                        return string.Empty;
                    }
                    return this.valueColumnName;
                }
                set
                {
                    this.valueColumnName = ((this.parentWebPart == null || this.parentWebPart.LicEd(this.reqEd)) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public SqlData()
            {
                this.pickerSemantics = true;
                this.defaultIfEmpty = true;
                this.reqEd = 4;
            }
            public SqlData(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.reqEd = 4;
                this.pickerSemantics = true;
                try
                {
                    this.AdoConnectionString = info.GetString("AdoConnectionString");
                    this.AdoDataProvider = info.GetString("AdoDataProvider");
                    this.DisplayColumnName = info.GetString("DisplayColumnName");
                    this.Query = info.GetString("Query");
                    this.ValueColumnName = info.GetString("ValueColumnName");
                    this.SqlChoice = info.GetString("SqlChoice");
                    this.DefaultIfEmpty = info.GetBoolean("DefaultIfEmpty");
                }
                catch
                {
                }
            }
            internal IDbConnection CreateConnection()
            {
                string text = this.Get<string>("AdoDataProvider");
                string name = text.Substring(0, text.IndexOf(','));
                string text2 = text.Substring(text.IndexOf(',') + 1).Trim();
                Type type;
                if (text2.Equals("System.Data", StringComparison.InvariantCultureIgnoreCase))
                {
                    type = typeof(DataTable).Assembly.GetType(name, true, true);
                }
                else
                {
                    if (text2.Equals("System.Data.OracleClient", StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = typeof(OracleConnection).Assembly.GetType(name, true, true);
                    }
                    else
                    {
                        type = Type.GetType(text, true, true);
                    }
                }
                return type.GetConstructor(new Type[]
				{
					typeof(string)
				}).Invoke(new object[]
				{
					this.Get<string>("AdoConnectionString")
				}) as IDbConnection;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                info.AddValue("AdoConnectionString", this.AdoConnectionString);
                info.AddValue("AdoDataProvider", this.AdoDataProvider);
                info.AddValue("SqlChoice", this.SqlChoice);
                info.AddValue("DisplayColumnName", this.DisplayColumnName);
                info.AddValue("Query", this.Query);
                info.AddValue("ValueColumnName", this.ValueColumnName);
            }
            public override void Render(HtmlTextWriter output, bool isUpperBound)
            {
                string text = "";
                int num = -1;
                bool flag = this.Get<bool>("CheckStyle");
                List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, this.Get<string>("SqlChoice").ToString());
                if (filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606"))
                {
                    filterValues.Clear();
                }
                if (filterValues.Count == 1 && (string.IsNullOrEmpty(filterValues[0]) || filterValues[0].Equals("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")))
                {
                    filterValues.Clear();
                }
                if (!base.Le(4, true))
                {
                    output.WriteLine(ProductPage.GetResource("NopeEd", new object[]
					{
						FilterBase.GetFilterTypeTitle(base.GetType()),
						"Ultimate"
					}));
                    base.Render(output, isUpperBound);
                    return;
                }
                try
                {
                    if (this.items == null)
                    {
                        if (!this.postFiltered)
                        {
                            this.doPostFilterNow = (this.postFiltered = true);
                        }
                        this.Items.ToString();
                    }
                    else
                    {
                        if (this.Cascade && !this.postFiltered)
                        {
                            this.postFiltered = (this.doPostFilterNow = true);
                            this.Items.ToString();
                        }
                    }
                    if (this.Get<bool>("DefaultIfEmpty"))
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxMultiMins['filterval_" + base.ID + "'] = '0478f8f9-fbdc-42f5-99ea-f6e8ec702606'; </script>");
                        if (!flag)
                        {
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", "0478f8f9-fbdc-42f5-99ea-f6e8ec702606", base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]], (filterValues.Count == 0 || filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")) ? " selected=\"selected\"" : string.Empty);
                        }
                        else
                        {
                            text += string.Format(string.Concat(new string[]
							{
								"<span><input class=\"rox-check-default\" name=\"filterval_",
								base.ID,
								"\" type=\"",
								base.AllowMultiEnter ? "checkbox" : "radio",
								"\" id=\"empty_filterval_",
								base.ID,
								"\" value=\"{1}\" {3}",
								string.IsNullOrEmpty(base.HtmlOnChangeAttr) ? (" onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);"),
								"/><label for=\"empty_filterval_",
								base.ID,
								"\">{2}</label></span>"
							}), new object[]
							{
								ProductPage.GuidLower(Guid.NewGuid()),
								"0478f8f9-fbdc-42f5-99ea-f6e8ec702606",
								base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]],
								(filterValues.Count == 0 || filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")) ? " checked=\"checked\"" : string.Empty
							});
                        }
                    }
                    foreach (KeyValuePair<int, string[]> current in this.items)
                    {
                        num++;
                        if (flag)
                        {
                            text += string.Format(string.Concat(new string[]
							{
								"<span><input class=\"chk-",
								base.ID,
								" rox-check-value\" name=\"filterval_",
								base.ID,
								"\" type=\"",
								base.AllowMultiEnter ? "checkbox" : "radio",
								"\" id=\"x{0}\" value=\"{1}\" {3}",
								(string.IsNullOrEmpty(base.HtmlOnChangeAttr) && this.Get<bool>("DefaultIfEmpty")) ? (" onclick=\"document.getElementById('empty_filterval_" + base.ID + "').checked=false;\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"" + (this.Get<bool>("DefaultIfEmpty") ? ("document.getElementById('empty_filterval_" + base.ID + "').checked=false;") : string.Empty)),
								"/><label for=\"x{0}\">{2}</label></span>"
							}), new object[]
							{
								ProductPage.GuidLower(Guid.NewGuid()),
								current.Value[1],
								base.GetDisplayValue(current.Value[0]),
								(filterValues.Contains(current.Value[1]) || (filterValues.Contains("4aea04a6-787a-4135-81a0-195e5946db1f") && num == 0) || (filterValues.Contains("c3bd9af3-8df1-49bd-835b-36f5d64b060c") && num == this.items.Count - 1)) ? " checked=\"checked\"" : string.Empty
							});
                        }
                        else
                        {
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", current.Value[1], base.GetDisplayValue(current.Value[0]), (filterValues.Contains(current.Value[1]) || (filterValues.Contains("4aea04a6-787a-4135-81a0-195e5946db1f") && num == 0) || (filterValues.Contains("c3bd9af3-8df1-49bd-835b-36f5d64b060c") && num == this.items.Count - 1)) ? " selected=\"selected\"" : string.Empty);
                        }
                        if (base.PickerLimit != 0 && num + 1 >= base.PickerLimit)
                        {
                            break;
                        }
                    }
                    if (text.Length > 0)
                    {
                        if (flag)
                        {
                            output.Write("<div>" + text + "</div>");
                        }
                        else
                        {
                            output.Write(string.Concat(new string[]
							{
								"<select",
								base.AllowMultiEnter ? " size=\"1\" multiple=\"multiple\" class=\"rox-multiselect ms-input\"" : " class=\"ms-input\"",
								" name=\"{0}\" id=\"{0}\"{1}>",
								text,
								"</select>"
							}), "filterval_" + base.ID, base.AllowMultiEnter ? base.HtmlOnChangeMultiAttr : base.HtmlOnChangeAttr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    base.Report(ex);
                }
                base.Render(output, isUpperBound);
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = "";
                this.Get<string>("SqlChoice");
                string text2 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                string text3 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
                "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
                string text4 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
                if (this.parentWebPart != null)
                {
                    string[] array = ProductPage.Config(SPContext.Current, "DataProviders").Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text5 = array[i];
                        int num;
                        if ((num = text5.IndexOf(':')) <= 0 || text5.LastIndexOf(',') < num)
                        {
                            base.Report(new Exception(base["InvalidAdoProviderFormat", new object[]
							{
								text5
							}]));
                        }
                        else
                        {
                            string a;
                            text += string.Format("<option value=\"{0}\"{2}>{1}</option>", a = text5.Substring(num + 1).Trim(), text5.Substring(0, num), (a == this.Get<string>("AdoDataProvider")) ? " selected=\"selected\"" : string.Empty);
                        }
                    }
                    panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
					{
						FilterBase.GetFilterTypeTitle(base.GetType())
					}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "AdoDataProvider", new object[]
					{
						"",
						text
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>" : text4, "AdoConnectionString", new object[]
					{
						this.Get<string>("AdoConnectionString")
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "ValueColumnName", new object[]
					{
						this.Get<string>("ValueColumnName")
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "DisplayColumnName", new object[]
					{
						this.Get<string>("DisplayColumnName")
					}));
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>" : text4, "Query", new object[]
					{
						this.Get<string>("Query")
					}));
                    text = "";
                    for (int j = 0; j < FilterBase.SqlData.choices.Length; j++)
                    {
                        string a2;
                        text += string.Format("<option value=\"{0}\"{2}>{1}</option>", a2 = ((j != 0) ? ((j == 1) ? "4aea04a6-787a-4135-81a0-195e5946db1f" : "c3bd9af3-8df1-49bd-835b-36f5d64b060c") : "0478f8f9-fbdc-42f5-99ea-f6e8ec702606"), base[FilterBase.SqlData.choices[j], new object[0]], (a2 == this.Get<string>("SqlChoice")) ? " selected=\"selected\"" : string.Empty);
                    }
                    panel.Controls.Add(base.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "SqlChoice", new object[]
					{
						" onchange=\"" + FilterBase.SqlData.scriptCheckDefault + "\"",
						text
					}));
                    panel.Controls.Add(new LiteralControl("</fieldset>"));
                }
                base.UpdatePanel(panel);
                if (this.parentWebPart != null)
                {
                    panel.Controls.Add(base.CreateScript(FilterBase.SqlData.scriptCheckDefault));
                }
            }
            public override void UpdateProperties(Panel panel)
            {
                this.AdoConnectionString = this.Get<string>("AdoConnectionString");
                this.AdoDataProvider = this.Get<string>("AdoDataProvider");
                this.SqlChoice = this.Get<string>("SqlChoice");
                this.DisplayColumnName = this.Get<string>("DisplayColumnName");
                this.Query = this.Get<string>("Query");
                this.ValueColumnName = this.Get<string>("ValueColumnName");
                base.UpdateProperties(panel);
            }
        }
        [Serializable]
        internal sealed class User : FilterBase.Lookup
        {
            private SPList userList;
            private string userListUrl;
            internal SPList UserList
            {
                get
                {
                    if (this.userList == null)
                    {
                        this.userList = SPContext.Current.Site.GetCatalog((SPListTemplateType)112);
                    }
                    return this.userList;
                }
            }
            internal string UserListUrl
            {
                get
                {
                    if (this.userListUrl == null)
                    {
                        this.userListUrl = ProductPage.MergeUrlPaths(SPContext.Current.Site.Url, this.UserList.DefaultViewUrl);
                    }
                    return this.userListUrl;
                }
            }
            public override string ListUrl
            {
                get
                {
                    return this.UserListUrl;
                }
                set
                {
                    base.ListUrl = this.UserListUrl;
                }
            }
            public User()
            {
                this.Init();
                this.defaultIfEmpty = true;
                this.itemID = -1;
                this.stripID = true;
            }
            public User(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                this.Init();
            }
            private void Init()
            {
                this.hiddenProperties.AddRange(new string[]
				{
					"ListUrl"
				});
                base.ListUrl = this.UserListUrl;
            }
            internal override int GetPageID(string listUrlParam)
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    return SPContext.Current.Web.CurrentUser.ID;
                }
                return 0;
            }
            protected internal override object GetValue(string name)
            {
                if (!(name == "ListUrl"))
                {
                    return base.GetValue(name);
                }
                return this.UserListUrl;
            }
            public override void UpdatePanel(Panel panel)
            {
                base.UpdatePanel(panel);
                if (string.IsNullOrEmpty(this.Get<string>("ValueFieldName")) && string.IsNullOrEmpty(this.Get<string>("DisplayFieldName")))
                {
                    panel.Controls.Add(base.CreateScript("function roxUserColumns(disp) { document.getElementById('div_filter_ValueFieldName').style.display = document.getElementById('div_filter_DisplayFieldName').style.display = disp; if (disp == 'block') { (function(){jQuery('#roxfilterspecial').slideDown();})(); document.getElementById('div_filter_ValueFieldName').style.backgroundColor = document.getElementById('div_filter_DisplayFieldName').style.backgroundColor = 'ButtonFace'; location.replace('#roxtooltop'); jQuery('#roxusercols').hide(); } } roxUserColumns('none'); "));
                    panel.Controls.Add(new LiteralControl("<div id=\"roxusercols\" class=\"rox-prop\"><a onclick=\"roxUserColumns('block');\" href=\"#noop\">" + base["UserFilterColumnsLink", new object[0]] + "</a></div>"));
                }
            }
        }
        [Serializable]
        internal sealed class WssContext : FilterBase
        {
            private static readonly string[] contextObjects = new string[]
			{
				"Site.WebApplication",
				"Site",
				"Web",
				"List",
				"ViewContext.View",
				"ListItem",
				"File"
			};
            private string contextObject = "Web";
            private string contextProperty = "ID";
            protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
            {
                get
                {
                    object obj = this.GetContextObject(this.Get<string>("ContextObject"));
                    object obj2 = (obj == null) ? new Exception() : this.GetContextPropertyValue(obj, this.Get<string>("ContextProperty"));
                    if (!base.Le(2, true))
                    {
                        throw new Exception(ProductPage.GetResource("NopeEd", new object[]
						{
							FilterBase.GetFilterTypeTitle(base.GetType()),
							"Basic"
						}));
                    }
                    if (!(obj2 is Exception))
                    {
                        yield return new KeyValuePair<string, string>(base.Name, obj2 + string.Empty);
                    }
                    yield break;
                }
            }
            public string ContextObject
            {
                get
                {
                    if (!base.Le(2, true))
                    {
                        return string.Empty;
                    }
                    return this.contextObject;
                }
                set
                {
                    this.contextObject = (base.Le(2, true) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public string ContextProperty
            {
                get
                {
                    if (!base.Le(2, true))
                    {
                        return string.Empty;
                    }
                    return this.contextProperty;
                }
                set
                {
                    this.contextProperty = (base.Le(2, true) ? ProductPage.Trim(value, new char[0]) : string.Empty);
                }
            }
            public WssContext()
            {
            }
            public WssContext(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                try
                {
                    this.ContextObject = info.GetString("ContextObject");
                    this.ContextProperty = info.GetString("ContextProperty");
                }
                catch
                {
                }
            }
            internal object GetContextObject(string opath)
            {
                object obj = SPContext.Current;
                object obj2 = null;
                string[] array = opath.Split(new char[]
				{
					'.'
				}, StringSplitOptions.RemoveEmptyEntries);
                PropertyInfo propertyInfo = null;
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string name = array2[i];
                    try
                    {
                        propertyInfo = obj.GetType().GetProperty(name);
                    }
                    catch
                    {
                        propertyInfo = null;
                    }
                    if (propertyInfo != null)
                    {
                        try
                        {
                            obj2 = propertyInfo.GetValue(obj, null);
                        }
                        catch
                        {
                            obj2 = null;
                        }
                    }
                    if (obj2 == null)
                    {
                        obj = null;
                        break;
                    }
                    obj = obj2;
                }
                if (obj != SPContext.Current)
                {
                    return obj;
                }
                return null;
            }
            internal Type GetContextObjectType(string opath)
            {
                Type type = typeof(SPContext);
                string[] array = opath.Split(new char[]
				{
					'.'
				}, StringSplitOptions.RemoveEmptyEntries);
                PropertyInfo propertyInfo = null;
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string name = array2[i];
                    try
                    {
                        propertyInfo = type.GetProperty(name);
                    }
                    catch
                    {
                        propertyInfo = null;
                    }
                    if (propertyInfo == null)
                    {
                        break;
                    }
                    type = propertyInfo.PropertyType;
                }
                if (type != typeof(SPContext))
                {
                    return type;
                }
                return null;
            }
            internal object GetContextPropertyValue(object obj, string prop)
            {
                SPItem sPItem = obj as SPItem;
                object result;
                try
                {
                    result = ((prop.StartsWith("roxfld__") && sPItem != null) ? this.GetContextPropertyValue(sPItem, ProductPage.GetField(sPItem.Fields, prop.Substring("roxfld__".Length))) : this.GetContextPropertyValue(obj, obj.GetType().GetProperty(prop)));
                }
                catch (Exception ex)
                {
                    result = ex;
                }
                return result;
            }
            internal object GetContextPropertyValue(SPItem obj, SPField prop)
            {
                object result = null;
                Exception ex = null;
                if (prop != null && obj != null)
                {
                    try
                    {
                        result = obj[prop.InternalName];
                    }
                    catch (Exception ex2)
                    {
                        ex = ex2;
                    }
                }
                if (ex != null)
                {
                    return ex;
                }
                return result;
            }
            internal object GetContextPropertyValue(object obj, PropertyInfo prop)
            {
                object obj2 = null;
                Exception ex = null;
                if (prop != null && obj != null)
                {
                    try
                    {
                        obj2 = prop.GetValue(obj, null);
                    }
                    catch (Exception ex2)
                    {
                        ex = ex2;
                    }
                }
                if (obj2 != null)
                {
                    if (obj2 is Guid)
                    {
                        obj2 = obj2.ToString();
                    }
                    else
                    {
                        if (obj2.GetType().IsGenericParameter || obj2.GetType().IsGenericType || obj2.GetType().IsGenericTypeDefinition)
                        {
                            obj2 = new Exception();
                        }
                        else
                        {
                            if (obj2.GetType().IsClass)
                            {
                                obj2 = (obj2.ToString().Equals(obj2.GetType().FullName) ?Convert.ToString(new Exception()) : obj2.ToString());
                            }
                        }
                    }
                }
                else
                {
                    if (prop.PropertyType != typeof(string))
                    {
                        obj2 = new Exception();
                    }
                    else
                    {
                        obj2 = string.Empty;
                    }
                }
                if (ex != null)
                {
                    return ex;
                }
                return obj2;
            }
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("ContextObject", this.ContextObject);
                info.AddValue("ContextProperty", this.ContextProperty);
                base.GetObjectData(info, context);
            }
            public List<PropertyInfo> GetProperties(Type ctype)
            {
                ParameterInfo[] array = null;
                List<PropertyInfo> list = new List<PropertyInfo>();
                PropertyInfo[] properties = ctype.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo propertyInfo = properties[i];
                    try
                    {
                        array = propertyInfo.GetIndexParameters();
                    }
                    catch
                    {
                    }
                    if ((array == null || array.Length == 0) && !propertyInfo.PropertyType.IsGenericParameter && !propertyInfo.PropertyType.IsGenericType && !propertyInfo.PropertyType.IsGenericTypeDefinition)
                    {
                        list.Add(propertyInfo);
                    }
                }
                list.Sort((PropertyInfo one, PropertyInfo two) => one.Name.CompareTo(two.Name));
                return list;
            }
            public override void UpdatePanel(Panel panel)
            {
                string text = "";
                string text2 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
                int num = 0;
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[]
				{
					FilterBase.GetFilterTypeTitle(base.GetType())
				}] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText; display: none;\" id=\"roxfilterspecial\">"));
                for (int i = 0; i < FilterBase.WssContext.contextObjects.Length; i++)
                {
                    bool flag;
                    if (flag = (FilterBase.WssContext.contextObjects[i] == this.ContextObject))
                    {
                        num = i;
                    }
                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", FilterBase.WssContext.contextObjects[i], base["ContextObject" + i, new object[0]], flag ? " selected=\"selected\"" : string.Empty);
                }
                panel.Controls.Add(base.CreateControl(base.Le(2, true) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text2, "ContextObject", new object[]
				{
					" onchange=\"jQuery('.rox-spctxprops').hide(); jQuery('#roxspctxprops_' + this.selectedIndex).show();\"",
					text
				}));
                for (int j = 0; j < FilterBase.WssContext.contextObjects.Length; j++)
                {
                    text = string.Empty;
                    panel.Controls.Add(new LiteralControl(string.Concat(new object[]
					{
						"<div class=\"rox-spctxprops\" id=\"roxspctxprops_",
						j,
						"\" style=\"display: ",
						(j == num) ? "block" : "none",
						"\">"
					})));
                    Type contextObjectType;
                    if ((contextObjectType = this.GetContextObjectType(FilterBase.WssContext.contextObjects[j])) != null)
                    {
                        object obj = this.GetContextObject(FilterBase.WssContext.contextObjects[j]);
                        foreach (PropertyInfo current in this.GetProperties(contextObjectType))
                        {
                            object contextPropertyValue;
                            if (!((contextPropertyValue = this.GetContextPropertyValue(obj, current)) is Exception))
                            {
                                text += string.Format("<option value=\"{0}\"{2}>{1}</option>", current.Name, current.Name + ((obj == null) ? string.Empty : (" [" + base.Context.Server.HtmlEncode(JSON.JsonEncode(contextPropertyValue)) + "]")), (current.Name == this.ContextProperty) ? " selected=\"selected\"" : string.Empty);
                            }
                        }
                        SPItem sPItem;
                        if ((sPItem = (obj as SPItem)) != null && sPItem.Fields != null)
                        {
                            foreach (SPField current2 in ProductPage.TryEach<SPField>(sPItem.Fields))
                            {
                                object contextPropertyValue;
                                if (!((contextPropertyValue = this.GetContextPropertyValue(sPItem, current2)) is Exception))
                                {
                                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", "roxfld__" + current2.InternalName, current2.Title + ((obj == null) ? string.Empty : (" [" + base.Context.Server.HtmlEncode(JSON.JsonEncode(contextPropertyValue)) + "]")), ("roxfld__" + current2.InternalName == this.ContextProperty) ? " selected=\"selected\"" : string.Empty);
                                }
                            }
                        }
                    }
                    panel.Controls.Add(base.CreateControl(base.Le(2, true) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text2, "ContextProperty___" + j, new object[]
					{
						string.Empty,
						text
					}));
                    panel.Controls.Add(new LiteralControl("</div>"));
                }
                panel.Controls.Add(new LiteralControl("</fieldset>"));
                base.UpdatePanel(panel);
            }
            public override void UpdateProperties(Panel panel)
            {
                this.ContextObject = this.Get<string>("ContextObject");
                this.ContextProperty = string.Empty;
                for (int i = 0; i < FilterBase.WssContext.contextObjects.Length; i++)
                {
                    if (FilterBase.WssContext.contextObjects[i] == this.ContextObject)
                    {
                        this.ContextProperty = this.Get<string>("ContextProperty___" + i);
                    }
                }
                base.UpdateProperties(panel);
            }
        }
        protected internal const string FORMAT_CHECKBOX = "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>";
        protected internal const string FORMAT_GENERIC_PREFIX = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>";
        protected internal const string FORMAT_GENERIC_SUFFIX = "</div>";
        protected internal const string FORMAT_LIST = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>";
        protected internal const string FORMAT_LISTOPTION = "<option value=\"{0}\"{2}>{1}</option>";
        protected internal const string FORMAT_SCRIPT = "<script type=\"text/javascript\" language=\"JavaScript\">\n{0}\n</script>";
        protected internal const string FORMAT_TEXTAREA = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>";
        protected internal const string FORMAT_TEXTBOX = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>";
        protected internal const string PREFIX_FIELDNAME = "filterval_";
        internal static readonly List<string> invalidTypes;
        internal static readonly string[] rangeRelevantProperties;
        private static readonly List<Type> filterTypes;
        private static readonly Dictionary<string, Type> externalFilterTypes;
        protected internal roxority_FilterWebPart parentWebPart;
        internal readonly List<string> groups = new List<string>();
        internal readonly List<string> hiddenProperties = new List<string>();
        internal bool isEditMode;
        internal bool requirePostLoadRendering;
        internal bool resolve = true;
        internal bool supportRange;
        internal string id = string.Empty;
        internal string[] suppressValues = new string[0];
        private Dictionary<string, int> resolveLevels = new Dictionary<string, int>();
        private bool enabled = true;
        private bool sendEmpty;
        private bool suppressIfInactive;
        private int camlOperator;
        private int suppressMode;
        private string fallbackValue = string.Empty;
        private string multiFilterSeparator = string.Empty;
        private string multiValueSeparator = string.Empty;
        private string name = string.Empty;
        private string numberCulture = string.Empty;
        private string numberFormat = string.Empty;
        private string suppressIfParam = string.Empty;
        private string suppressMultiValues = string.Empty;
        private CultureInfo culture;
        public static IEnumerable<Type> FilterTypes
        {
            get
            {
                List<string> list = null;
                bool flag = true;
                try
                {
                    list = new List<string>(ProductPage.Config(SPContext.Current, "FilterTypes").Split(new char[]
					{
						'\r',
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries));
                }
                catch
                {
                }
                foreach (Type current in FilterBase.filterTypes)
                {
                    yield return current;
                }
                if (roxority_FilterWebPart.BdcClientUtilType != null && roxority_FilterWebPart.BdcFilterType != null)
                {
                    yield return roxority_FilterWebPart.BdcFilterType;
                }
                if (list != null)
                {
                    using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            string current2 = enumerator2.Current;
                            if (!FilterBase.externalFilterTypes.ContainsKey(current2))
                            {
                                Type type;
                                if ((type = Type.GetType(current2, false, true)) != null && !typeof(FilterBase).IsAssignableFrom(type))
                                {
                                    type = null;
                                }
                                FilterBase.externalFilterTypes.Add(current2, type);
                            }
                        }
                        goto IL_20F;
                    }
                IL_198:
                    flag = false;
                    foreach (KeyValuePair<string, Type> current3 in FilterBase.externalFilterTypes)
                    {
                        if (!list.Contains(current3.Key))
                        {
                            FilterBase.externalFilterTypes.Remove(current3.Key);
                            FilterBase.invalidTypes.Remove(current3.Key);
                            flag = true;
                            break;
                        }
                    }
                IL_20F:
                    if (flag)
                    {
                        goto IL_198;
                    }
                }
                foreach (KeyValuePair<string, Type> current4 in FilterBase.externalFilterTypes)
                {
                    KeyValuePair<string, Type> keyValuePair = current4;
                    if (keyValuePair.Value != null)
                    {
                        KeyValuePair<string, Type> keyValuePair2 = current4;
                        yield return keyValuePair2.Value;
                    }
                    else
                    {
                        List<string> arg_28F_0 = FilterBase.invalidTypes;
                        KeyValuePair<string, Type> keyValuePair3 = current4;
                        if (!arg_28F_0.Contains(keyValuePair3.Key))
                        {
                            List<string> arg_2AA_0 = FilterBase.invalidTypes;
                            KeyValuePair<string, Type> keyValuePair4 = current4;
                            arg_2AA_0.Add(keyValuePair4.Key);
                        }
                    }
                }
                yield break;
            }
        }
        internal HttpContext Context
        {
            get
            {
                try
                {
                    return HttpContext.Current;
                }
                catch
                {
                }
                return null;
            }
        }
        internal CultureInfo EffectiveNumberCulture
        {
            get
            {
                string s = this.Get<string>("NumberCulture");
                if (this.culture == null)
                {
                    try
                    {
                        int num;
                        if (int.TryParse(s, out num))
                        {
                            this.culture = new CultureInfo(num);
                        }
                        else
                        {
                            this.culture = new CultureInfo(s);
                        }
                    }
                    catch
                    {
                        this.culture = null;
                    }
                }
                return this.culture;
            }
        }
        protected internal virtual IEnumerable<KeyValuePair<string, string>> FilterPairs
        {
            get
            {
                return null;
            }
        }
        protected internal string this[string resKey, params object[] args]
        {
            get
            {
                return ProductPage.GetProductResource(resKey, args);
            }
        }
        protected internal virtual bool SupportsMultipleValues
        {
            get
            {
                return true;
            }
        }
        public int CamlOperator
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(4))
                {
                    return 0;
                }
                return this.camlOperator;
            }
            set
            {
                this.camlOperator = ((this.parentWebPart == null || this.parentWebPart.LicEd(4)) ? value : 0);
            }
        }
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }
        public string FallbackValue
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                {
                    return string.Empty;
                }
                return this.fallbackValue;
            }
            set
            {
                this.fallbackValue = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ((value == null) ? string.Empty : value) : string.Empty);
            }
        }
        public string Groups
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(4))
                {
                    return string.Empty;
                }
                return string.Join("\r\n", this.groups.ConvertAll<string>((string v) => v.Replace(roxority_FilterWebPart.sep, ",")).ToArray());
            }
            set
            {
                string[] array = ProductPage.Trim(value, new char[0]).Split(new char[]
				{
					'\r',
					'\n'
				}, StringSplitOptions.RemoveEmptyEntries);
                this.groups.Clear();
                if (this.parentWebPart == null || this.parentWebPart.LicEd(4))
                {
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string text = array2[i];
                        string item;
                        if (!string.IsNullOrEmpty(item = text.Trim().Replace(",", roxority_FilterWebPart.sep)) && !this.groups.Contains(item))
                        {
                            this.groups.Add(item);
                        }
                    }
                }
            }
        }
        public string ID
        {
            get
            {
                return this.id.ToString();
            }
        }
        public bool IsMultiValueFilter
        {
            get
            {
                return this.parentWebPart != null && this.ID.Equals(this.parentWebPart.MultiValueFilterID);
            }
        }
        public bool IsRange
        {
            get
            {
                return this.Get<int>("CamlOperator") > 8;
            }
        }
        public string MultiFilterSeparator
        {
            get
            {
                if ((this.parentWebPart != null && !this.parentWebPart.LicEd(4)) || this.IsRange)
                {
                    return string.Empty;
                }
                return this.multiFilterSeparator;
            }
            set
            {
                this.multiFilterSeparator = (((this.parentWebPart == null || this.parentWebPart.LicEd(4)) && !this.IsRange) ? ((value == null) ? string.Empty : value) : string.Empty);
            }
        }
        public string MultiValueSeparator
        {
            get
            {
                if ((this.parentWebPart != null && !this.parentWebPart.LicEd(4)) || this.IsRange)
                {
                    return string.Empty;
                }
                return this.multiValueSeparator;
            }
            set
            {
                this.multiValueSeparator = (((this.parentWebPart == null || this.parentWebPart.LicEd(4)) && !this.IsRange) ? ((value == null) ? string.Empty : value) : string.Empty);
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = ProductPage.Trim(value, new char[0]);
            }
        }
        public string NumberFormat
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                {
                    return string.Empty;
                }
                return this.numberFormat;
            }
            set
            {
                this.numberFormat = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? (value + string.Empty).Trim() : string.Empty);
            }
        }
        public string NumberCulture
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                {
                    return string.Empty;
                }
                return this.numberCulture;
            }
            set
            {
                this.numberCulture = null;
                value = ProductPage.Trim(value, new char[0]);
                if (this.parentWebPart != null)
                {
                    if (!this.parentWebPart.LicEd(2))
                    {
                        goto IL_5C;
                    }
                }
                try
                {
                    int num;
                    if (int.TryParse(value, out num))
                    {
                        new CultureInfo(num);
                    }
                    else
                    {
                        new CultureInfo(value);
                    }
                    this.numberCulture = value;
                    return;
                }
                catch
                {
                    this.numberCulture = string.Empty;
                    return;
                }
            IL_5C:
                this.numberCulture = string.Empty;
            }
        }
        public bool SendEmpty
        {
            get
            {
                return this.sendEmpty;
            }
            set
            {
                this.sendEmpty = value;
            }
        }
        public bool SuppressIfInactive
        {
            get
            {
                return this.suppressIfInactive;
            }
            set
            {
                this.suppressIfInactive = value;
            }
        }
        public string SuppressIfParam
        {
            get
            {
                return this.suppressIfParam;
            }
            set
            {
                this.suppressIfParam = (value + string.Empty).Trim();
            }
        }
        public int SuppressMode
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(2))
                {
                    return 0;
                }
                return this.suppressMode;
            }
            set
            {
                this.suppressMode = ((this.parentWebPart == null || this.parentWebPart.LicEd(2)) ? ((value < 0 || value > 4) ? 0 : value) : 0);
            }
        }
        public string SuppressMultiValues
        {
            get
            {
                if (this.parentWebPart != null && !this.parentWebPart.LicEd(4))
                {
                    return string.Empty;
                }
                return this.suppressMultiValues;
            }
            set
            {
                this.suppressMultiValues = ((this.parentWebPart == null || this.parentWebPart.LicEd(4)) ? ((value == null) ? string.Empty : value) : string.Empty);
            }
        }
        public string SuppressValues
        {
            get
            {
                return string.Join("\n", this.suppressValues);
            }
            set
            {
                List<string> list = new List<string>(string.IsNullOrEmpty(value = ProductPage.Trim(value, new char[0])) ? new string[0] : value.Split(new char[]
				{
					'\r',
					'\n'
				}, StringSplitOptions.RemoveEmptyEntries));
                list = list.ConvertAll<string>((string val) => ProductPage.Trim(val, new char[0]));
                while (list.IndexOf(string.Empty) >= 0)
                {
                    list.Remove(string.Empty);
                }
                ProductPage.RemoveDuplicates<string>(list);
                this.suppressValues = list.ToArray();
            }
        }
        public string UrlPropertyPrefix
        {
            get
            {
                if (this.parentWebPart != null)
                {
                    return this.parentWebPart.urlPropertyPrefix;
                }
                return "filter_";
            }
        }
        public virtual string WebPartValue
        {
            get
            {
                return string.Empty;
            }
        }
        static FilterBase()
        {
            FilterBase.invalidTypes = new List<string>();
            FilterBase.rangeRelevantProperties = new string[]
			{
				"DefaultValue",
				"DefaultValue2",
				"DefaultChoice",
				"DefaultChoice2"
			};
            FilterBase.filterTypes = new List<Type>();
            FilterBase.externalFilterTypes = new Dictionary<string, Type>();
            FilterBase.filterTypes.Add(typeof(FilterBase.Multi));
            FilterBase.filterTypes.Add(typeof(FilterBase.Choice));
            FilterBase.filterTypes.Add(typeof(FilterBase.Date));
            FilterBase.filterTypes.Add(typeof(FilterBase.Lookup));
            FilterBase.filterTypes.Add(typeof(FilterBase.PageField));
            FilterBase.filterTypes.Add(typeof(FilterBase.RequestParameter));
            FilterBase.filterTypes.Add(typeof(FilterBase.WssContext));
            FilterBase.filterTypes.Add(typeof(FilterBase.Text));
            FilterBase.filterTypes.Add(typeof(FilterBase.User));
            FilterBase.filterTypes.Add(typeof(FilterBase.SqlData));
            FilterBase.filterTypes.Add(typeof(FilterBase.Boolean));
            FilterBase.filterTypes.Add(typeof(FilterBase.CamlSource));
            FilterBase.filterTypes.Add(typeof(FilterBase.CamlDistinct));
            FilterBase.filterTypes.Add(typeof(FilterBase.CamlViewSwitch));
        }
        public static FilterBase Create(string typeName)
        {
            return Type.GetType(typeName).GetConstructor(Type.EmptyTypes).Invoke(null) as FilterBase;
        }
        public static List<FilterBase> Deserialize(roxority_FilterWebPart webPart, string value)
        {
            IEnumerable<FilterBase> enumerable = ProductPage.Deserialize<FilterBase>(value, delegate(FilterBase fb)
            {
                fb.parentWebPart = webPart;
            });
            if (enumerable != null)
            {
                return new List<FilterBase>(enumerable);
            }
            return new List<FilterBase>();
        }
        public static string GetFilterTypeDesc(Type type)
        {
            string text = ProductPage.GetProductResource("FilterDesc_" + type.Name, new object[0]);
            PropertyInfo property;
            if (string.IsNullOrEmpty(text) && (property = type.GetProperty("FilterTypeDescription", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)) != null)
            {
                try
                {
                    text = (property.GetValue(null, null) as string);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            return type.AssemblyQualifiedName;
        }
        public static string GetFilterTypeTitle(Type type)
        {
            string text = ProductPage.GetProductResource("FilterType_" + type.Name, new object[0]);
            PropertyInfo property;
            if (string.IsNullOrEmpty(text) && (property = type.GetProperty("FilterTypeTitle", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)) != null)
            {
                try
                {
                    text = (property.GetValue(null, null) as string);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            return type.Name;
        }
        public static string Serialize(List<FilterBase> values)
        {
            return ProductPage.Serialize<FilterBase>(values.ToArray());
        }
        public FilterBase()
        {
            this.id = Guid.NewGuid().ToString();
        }
        public FilterBase(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.id = info.GetString("ID");
                this.Name = info.GetString("Name");
                this.Enabled = info.GetBoolean("Enabled");
                this.SendEmpty = info.GetBoolean("SendEmpty");
                this.FallbackValue = info.GetString("FallbackValue");
                this.SuppressMode = info.GetInt32("SuppressMode");
                this.suppressValues = (string[])info.GetValue("SuppressValues", typeof(string[]));
                this.MultiFilterSeparator = info.GetString("MultiFilterSeparator");
                this.MultiValueSeparator = info.GetString("MultiValueSeparator");
                this.CamlOperator = info.GetInt32("CamlOperator");
                this.SuppressMultiValues = info.GetString("SuppressMultiValues");
                this.Groups = info.GetString("Groups");
                this.NumberFormat = info.GetString("NumberFormat");
                this.NumberCulture = info.GetString("NumberCulture");
                this.SuppressIfInactive = info.GetBoolean("SuppressIfInactive");
                this.SuppressIfParam = info.GetString("SuppressIfParam");
            }
            catch
            {
            }
        }
        protected internal LiteralControl CreateControl(string format, bool hidden, params object[] args)
        {
            int num = 0;
            List<object> list = new List<object>(args);
            if ("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Equals(format))
            {
                format = format.Replace("\" for=\"", "\" for=\"nope_");
            }
            int num2 = 0;
            while (num2 < 10 && format.IndexOf("{" + num2 + "}") >= 0)
            {
                num++;
                num2++;
            }
            if (list.Count < num)
            {
                for (int i = list.Count; i < num; i++)
                {
                    list.Add(string.Empty);
                }
            }
            if (hidden)
            {
                format = "<span style=\"display: none;\">" + format + "</span>";
            }
            return new LiteralControl(string.Format(format, list.ToArray()));
        }
        protected internal LiteralControl CreateControl(string format, string name, params object[] args)
        {
            List<object> list = new List<object>(args);
            FilterBase.Interactive interactive = this as FilterBase.Interactive;
            bool flag = interactive != null && interactive.IsRange && Array.IndexOf<string>(FilterBase.rangeRelevantProperties, name) >= 0;
            string text = (flag && name.EndsWith("2")) ? name.Substring(0, name.Length - 1) : name;
            list.Insert(0, "filter_" + name);
            if (flag)
            {
                list.Insert(1, (name == text) ? this["Prop_" + name + "Lower", new object[0]] : this["Prop_" + text + "Upper", new object[0]]);
            }
            else
            {
                list.Insert(1, this["Prop_" + ((this is FilterBase.Multi && "AllowMultiEnter".Equals(name, StringComparison.InvariantCultureIgnoreCase)) ? "AllowMultiCombo" : name), new object[0]]);
            }
            return this.CreateControl(format, this.hiddenProperties.Contains(name), list.ToArray());
        }
        protected internal LiteralControl CreateScript(string script)
        {
            return this.CreateControl("<script type=\"text/javascript\" language=\"JavaScript\">\n{0}\n</script>", false, new object[]
			{
				script
			});
        }
        protected internal string GetChecked(bool value)
        {
            if (!value)
            {
                return string.Empty;
            }
            return " checked=\"checked\"";
        }
        protected internal virtual object GetValue(string name)
        {
            int num = name.IndexOf("___");
            HttpContext context = this.Context;
            object value = base.GetType().GetProperty((num > 0) ? name.Substring(0, num) : name).GetValue(this, null);
            bool flag = this.isEditMode || (context != null && ("add".Equals(context.Request["roxfilteraction"]) || "edit".Equals(context.Request["roxfilteraction"])));
            bool flag2 = context != null && Array.IndexOf<string>(context.Request.Form.AllKeys, this.UrlPropertyPrefix + name) >= 0;
            bool flag3 = false;
            bool flag4 = this.parentWebPart != null && this.parentWebPart.UrlSettings && !flag && context != null && ((flag3 = (Array.IndexOf<string>(context.Request.QueryString.AllKeys, this.UrlPropertyPrefix + this.Name + "_" + name) >= 0)) || Array.IndexOf<string>(context.Request.QueryString.AllKeys, this.UrlPropertyPrefix + name) >= 0);
            string text = (context == null) ? string.Empty : context.Request[(flag3 && Array.IndexOf<string>(context.Request.Form.AllKeys, this.UrlPropertyPrefix + name) < 0) ? (this.UrlPropertyPrefix + this.Name + "_" + name) : (this.UrlPropertyPrefix + name)];
            string text2 = null;
            Converter<string, string> converter = delegate(string v)
            {
                int num6;
                if (!this.resolveLevels.TryGetValue(v, out num6))
                {
                    num6 = (this.resolveLevels[v] = 0);
                }
                if (num6 < 5)
                {
                    this.resolveLevels[v] = num6 + 1;
                    return this.ResolveValue(v);
                }
                return null;
            };
            if (flag4 || flag3)
            {
                new object();
            }
            if (!flag4 && (!this.resolve || !(value is string)) && (!this.isEditMode || (context != null && ("add".Equals(context.Request["roxfilteraction"]) || "edit".Equals(context.Request["roxfilteraction"])))))
            {
                return value;
            }
            if (value is bool)
            {
                if (!flag4)
                {
                    return !string.IsNullOrEmpty(text);
                }
                if (text == "1")
                {
                    return true;
                }
                if (!(text == "0"))
                {
                    return value;
                }
                return false;
            }
            else
            {
                if ((flag2 || flag4) && value is int)
                {
                    int num2;
                    if (!int.TryParse(text, out num2) && ((num = text.IndexOf(";#", StringComparison.InvariantCultureIgnoreCase)) <= 0 || !int.TryParse(text.Substring(0, num), out num2)))
                    {
                        return value;
                    }
                    return num2;
                }
                else
                {
                    if ((flag2 || flag4) && value is long)
                    {
                        long num3;
                        if (!long.TryParse(text, out num3))
                        {
                            return value;
                        }
                        return num3;
                    }
                    else
                    {
                        if (this.resolve && this.parentWebPart != null)
                        {
                            if ((!flag2 && !flag4) || string.IsNullOrEmpty(text2 = text))
                            {
                                if (flag4 || flag2 || string.IsNullOrEmpty(text2 = (value as string)))
                                {
                                    goto IL_3C2;
                                }
                            }
                            int num4;
                            int num5;
                            while ((num4 = text2.IndexOf("{$")) >= 0 && (num5 = text2.IndexOf("$}", num4 + 2)) > num4 + 2)
                            {
                                string text3;
                                string text4;
                                text2 = text2.Replace("{$" + (text3 = text2.Substring(num4 + 2, num5 - 2 - num4)) + "$}", (string.IsNullOrEmpty(text3) || (this.Name.Equals(text3) && !this.parentWebPart.consumedRow.ContainsKey(text3))) ? string.Empty : (string.IsNullOrEmpty(text4 = converter(text3)) ? "0478f8f9-fbdc-42f5-99ea-f6e8ec702606" : text4));
                            }
                        }
                    IL_3C2:
                        if (!string.IsNullOrEmpty(text2))
                        {
                            return text2.Replace("0478f8f9-fbdc-42f5-99ea-f6e8ec702606", string.Empty);
                        }
                        if (!flag2 && !flag4)
                        {
                            return value;
                        }
                        return text;
                    }
                }
            }
        }
        protected internal virtual T Get<T>(string name)
        {
            object value = this.GetValue(name);
            if (!(value is string[]) || typeof(T) != typeof(string))
            {
                return (T)((object)this.GetValue(name));
            }
            return (T)((object)string.Join("\r\n", (string[])value));
        }
        protected internal bool Le(int le, bool ifNull)
        {
            if (this.parentWebPart != null)
            {
                return this.parentWebPart.LicEd(le);
            }
            return ifNull;
        }
        protected internal void Report(Exception ex)
        {
            if (this.parentWebPart != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                string text = ex.Message.Contains(" ") ? ex.Message : string.Format(this["ColumnFailed", new object[]
				{
					ex.Message
				}], new object[0]);
                foreach (KeyValuePair<FilterBase, Exception> current in this.parentWebPart.warningsErrors)
                {
                    if (current.Key.ID == this.ID && current.Value.Message == text)
                    {
                        return;
                    }
                }
                this.parentWebPart.warningsErrors.Add(new KeyValuePair<FilterBase, Exception>(this, (ex.Message == text) ? ex : new Exception(text, ex)));
            }
        }
        protected internal string ResolveValue(string name)
        {
            List<string> list = new List<string>();
            FilterBase filterBase = null;
            if (this.parentWebPart.LicEd(4))
            {
                if (list.Count == 0 && this.parentWebPart.consumedRow.ContainsKey(name))
                {
                    list.Add(this.parentWebPart.consumedRow[name]);
                }
                if (list.Count == 0)
                {
                    List<FilterBase> list2 = this.parentWebPart.GetFilters(true, true).FindAll((FilterBase fb) => fb.Name.Equals(name)) ?? new List<FilterBase>();
                    if (list2.Count == 0)
                    {
                        list2 = this.parentWebPart.GetFilters(true, false).FindAll((FilterBase fb) => fb.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    }
                    if (list2 != null)
                    {
                        foreach (FilterBase current in list2)
                        {
                            IEnumerable<KeyValuePair<string, string>> filterPairs;
                            if ((filterPairs = current.FilterPairs) != null)
                            {
                                filterBase = current;
                                using (IEnumerator<KeyValuePair<string, string>> enumerator2 = filterPairs.GetEnumerator())
                                {
                                    while (enumerator2.MoveNext())
                                    {
                                        KeyValuePair<string, string> current2 = enumerator2.Current;
                                        list.Add(current2.Value);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (list.Count == 1)
                {
                    return list[0];
                }
                if (list.Count > 1)
                {
                    string text;
                    int num;
                    if ((text = filterBase.Get<string>("SuppressMultiValues")).StartsWith("[") && text.EndsWith("]") && int.TryParse(text.Substring(1, text.Length - 2), out num))
                    {
                        return list[(num < 1) ? (list.Count - 1) : (num - 1)];
                    }
                    return string.Join(text, list.ToArray());
                }
            }
            return WebConfigurationManager.AppSettings[name];
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CamlOperator", this.CamlOperator);
            info.AddValue("FallbackValue", this.FallbackValue);
            info.AddValue("ID", this.ID);
            info.AddValue("Name", this.Name);
            info.AddValue("Enabled", this.Enabled);
            info.AddValue("SendEmpty", this.SendEmpty);
            info.AddValue("SuppressMode", this.SuppressMode);
            info.AddValue("SuppressValues", this.suppressValues, typeof(string[]));
            info.AddValue("MultiFilterSeparator", this.MultiFilterSeparator);
            info.AddValue("MultiValueSeparator", this.MultiValueSeparator);
            info.AddValue("SuppressMultiValues", this.SuppressMultiValues);
            info.AddValue("Groups", this.Groups);
            info.AddValue("NumberFormat", this.NumberFormat);
            info.AddValue("NumberCulture", this.NumberCulture);
            info.AddValue("SuppressIfInactive", this.SuppressIfInactive);
            info.AddValue("SuppressIfParam", this.SuppressIfParam);
        }
        public void Set(string name, object value)
        {
            base.GetType().GetProperty(name).SetValue(this, value, null);
        }
        public virtual void UpdatePanel(Panel panel)
        {
            List<string> list = new List<string>();
            string text = "";
            "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
            string text2 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>".Replace("<input ", "<input disabled=\"disabled\" ");
            string text3 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
            if (this.parentWebPart != null && this.parentWebPart.toolPart != null)
            {
                panel.Controls.Add(this.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "SendEmpty", new object[]
				{
					this.GetChecked(this.Get<bool>("SendEmpty"))
				}));
                panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilteradvanced').slideToggle();\" href=\"#noop\">" + this["FilterAdvancedProps", new object[0]] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText; display: none;\" id=\"roxfilteradvanced\">"));
                text = "";
                for (int i = 0; i <= (this.supportRange ? 12 : 8); i++)
                {
                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", i, this["CamlOp_" + ((CamlOperator)i).ToString(), new object[0]], (i == this.Get<int>("CamlOperator")) ? " selected=\"selected\"" : string.Empty);
                }
                panel.Controls.Add(this.CreateControl((this.parentWebPart.LicEd(4) && this.parentWebPart.toolPart.camlYesRadioButton.Checked) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "CamlOperator", new object[]
				{
					string.Concat(new string[]
					{
						" onchange=\"if((",
						this.IsRange ? "true" : "false",
						"&&(this.selectedIndex<=8))||(",
						this.IsRange ? "false" : "true",
						"&&(this.selectedIndex>8)))roxRefreshFilters();\""
					}),
					text
				}));
                panel.Controls.Add(this.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "FallbackValue", new object[]
				{
					this.Get<string>("FallbackValue")
				}));
                panel.Controls.Add(this.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "NumberFormat", new object[]
				{
					this.Get<string>("NumberFormat")
				}));
                panel.Controls.Add(this.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "NumberCulture", new object[]
				{
					this.Get<string>("NumberCulture")
				}));
                panel.Controls.Add(this.CreateControl(this.parentWebPart.LicEd(4) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "SuppressMultiValues", new object[]
				{
					this.Get<string>("SuppressMultiValues")
				}));
                panel.Controls.Add(this.CreateControl((this.parentWebPart.LicEd(4) && !this.IsRange) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "MultiValueSeparator", new object[]
				{
					this.Get<string>("MultiValueSeparator")
				}));
                panel.Controls.Add(this.CreateControl((this.parentWebPart.LicEd(4) && !this.IsRange) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>" : text2, "MultiFilterSeparator", new object[]
				{
					this.Get<string>("MultiFilterSeparator")
				}));
                text = "";
                for (int j = 0; j <= 4; j++)
                {
                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", j, this["SuppressMode" + j, new object[0]], (j == this.Get<int>("SuppressMode")) ? " selected=\"selected\"" : string.Empty);
                }
                panel.Controls.Add(this.CreateControl(this.parentWebPart.LicEd(2) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "SuppressMode", new object[]
				{
					" onchange=\"document.getElementById('div_filter_SuppressValues').style.display=((this.selectedIndex==0)?'none':'block');\"",
					text
				}));
                panel.Controls.Add(this.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>", "SuppressValues", new object[]
				{
					this.Get<string>("SuppressValues"),
					4
				}));
                if (this.Get<int>("SuppressMode") == 0)
                {
                    panel.Controls.Add(this.CreateScript("document.getElementById('div_filter_SuppressValues').style.display = 'none';"));
                }
                text = "";
                list = this.parentWebPart.GetGroups();
                foreach (string current in list)
                {
                    text += string.Format("<option value=\"{0}\"{2}>{1}</option>", current, current.Replace(roxority_FilterWebPart.sep, ","), this.groups.Contains(current) ? " selected=\"selected\"" : string.Empty);
                }
                panel.Controls.Add(this.CreateControl((this.parentWebPart.LicEd(4) && list.Count > 1) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : text3, "Groups", new object[]
				{
					" multiple=\"multiple\" size=\"" + (list.Count + 1) + "\"",
					text
				}));
                panel.Controls.Add(this.CreateControl("<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>", "SuppressIfInactive", new object[]
				{
					this.GetChecked(this.Get<bool>("SuppressIfInactive"))
				}));
                panel.Controls.Add(this.CreateControl("<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>", "SuppressIfParam", new object[]
				{
					this.Get<string>("SuppressIfParam")
				}));
                panel.Controls.Add(new LiteralControl("</fieldset>"));
            }
        }
        public virtual void UpdateProperties(Panel panel)
        {
            this.CamlOperator = this.Get<int>("CamlOperator");
            this.FallbackValue = this.Get<string>("FallbackValue");
            this.SendEmpty = this.Get<bool>("SendEmpty");
            this.SuppressMode = this.Get<int>("SuppressMode");
            this.SuppressValues = this.Get<string>("SuppressValues");
            this.MultiFilterSeparator = this.Get<string>("MultiFilterSeparator");
            this.MultiValueSeparator = this.Get<string>("MultiValueSeparator");
            this.SuppressMultiValues = this.Get<string>("SuppressMultiValues");
            this.Groups = string.Join("\r\n", this.Get<string>("Groups").Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries));
            this.NumberCulture = this.Get<string>("NumberCulture");
            this.NumberFormat = this.Get<string>("NumberFormat");
            this.SuppressIfInactive = this.Get<bool>("SuppressIfInactive");
            this.SuppressIfParam = this.Get<string>("SuppressIfParam");
        }
        public override string ToString()
        {
            FilterBase.Interactive interactive = this as FilterBase.Interactive;
            string text = (interactive == null || string.IsNullOrEmpty(interactive.Label)) ? string.Empty : (interactive.Label.EndsWith(":") ? interactive.Label.Substring(0, interactive.Label.Length - 1) : interactive.Label);
            return string.Concat(new string[]
			{
				this.Enabled ? string.Empty : this["Disabled", new object[0]],
				this["FilterType_" + base.GetType().Name, new object[0]],
				": ",
				string.IsNullOrEmpty(this.Name) ? this["Edit_NoName", new object[0]] : this.Name,
				(!string.IsNullOrEmpty(text) && text != this.Name) ? string.Format(" (\"{0}\")", text) : string.Empty
			});
        }
    }
}