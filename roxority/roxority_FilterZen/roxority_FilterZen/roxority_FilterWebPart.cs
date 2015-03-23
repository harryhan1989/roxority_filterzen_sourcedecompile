namespace roxority_FilterZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebControls;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using roxority.Shared;
    using roxority.Shared.ComponentModel;
    using roxority.Shared.Xml;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Xml;
    using System.Xml.Serialization;

    [Guid("cdbec42f-2b0c-4c1c-8984-1a38f425e8f5")]
    public class roxority_FilterWebPart : WebPartBase, ICellProvider, ICellConsumer, IRowConsumer, IRowProvider, IFilterProvider, IWebPartParameters
    {
        internal bool _cellConnected;
        internal bool _connected;
        internal bool _rowConnected;
        private string[] acSecFields;
        public readonly List<string> additionalWarningsErrors = new List<string>();
        internal bool ajax14focus = true;
        internal bool ajax14hide;
        internal int ajax14Interval;
        internal readonly List<System.Web.UI.WebControls.WebParts.WebPart> appliedParts = new List<System.Web.UI.WebControls.WebParts.WebPart>();
        internal bool applyToolbarStylings = !ProductPage.Is14;
        internal bool autoConnect;
        internal bool autoRepost;
        private static System.Type bdcClientUtilType = null;
        private static MethodInfo bdcFilterApplyMethod = null;
        private static MethodInfo bdcFilterCanApplyMethod = null;
        private static System.Type bdcFilterType = null;
        internal bool camlFiltered;
        internal bool camlFilters;
        private string camlFiltersAndCombined = string.Empty;
        internal bool cascaded;
        internal bool cascadeLtr;
        private string[] cell;
        private CellReadyEventArgs cellArgs;
        private string cellFieldName = "FilterGroup";
        private FilterBase cellFilter;
        private string[] cellNames = new string[0];
        private List<DataFormWebPart> connectedDataParts = new List<DataFormWebPart>();
        internal SPList connectedList;
        internal readonly List<System.Web.UI.WebControls.WebParts.WebPart> connectedParts = new List<System.Web.UI.WebControls.WebParts.WebPart>();
        internal SPView connectedView;
        protected internal readonly Dictionary<string, string> consumedRow = new Dictionary<string, string>();
        private string[] dataPartIDs = new string[0];
        public DateTimeControl[] DatePickers = new DateTimeControl[6];
        private List<KeyValuePair<string, string>> debugFilters = new List<KeyValuePair<string, string>>();
        internal bool debugMode;
        internal bool defaultToOr;
        internal readonly Dictionary<System.Web.UI.WebControls.WebParts.WebPart, Action<System.Web.UI.WebControls.WebParts.WebPart>> deferredActions = new Dictionary<System.Web.UI.WebControls.WebParts.WebPart, Action<System.Web.UI.WebControls.WebParts.WebPart>>();
        private roxority.Shared.Action deferredFilterAction1;
        private roxority.Shared.Action deferredFilterAction2;
        internal bool disableFilters = true;
        internal bool disableFiltersSome = true;
        private List<IDisposable> disps = new List<IDisposable>();
        private List<FilterBase> dynamicFilters;
        internal bool embedFilters;
        internal bool errorMode = true;
        internal readonly List<string> eventOrderLog = new List<string>();
        internal bool extraHide;
        private List<FilterBase> filters;
        internal readonly List<KeyValuePair<string, string>> filtersNotSent = new List<KeyValuePair<string, string>>();
        private string finalJson;
        private bool firstSkipped = true;
        private string folderScope = string.Empty;
        internal bool forceReload;
        private string generatedQuery = string.Empty;
        private string group = string.Empty;
        internal readonly List<string> groups = new List<string>();
        private bool? hasDate;
        private bool? hasMulti;
        private bool? hasPeople;
        internal bool highlight;
        private string htmlEmbed;
        private int htmlMode = 2;
        private bool initChecksPerformed;
        private bool isRowConsumer;
        private bool? isViewPage;
        private string jsonFilters = string.Empty;
        private List<Guid> listViews = new List<Guid>();
        private string listViewUrl;
        private int maxFiltersPerRow;
        public Panel MultiPanel = new Panel();
        public TextBox MultiTextBox = new TextBox();
        private string multiValueFilterID = string.Empty;
        private int multiWidth = 240;
        private int nullParts;
        private List<KeyValuePair<string, FilterPair>> partFilters;
        public PeopleEditor[] PeoplePickers = new PeopleEditor[6];
        internal bool recollapseGroups;
        private Exception regError;
        internal bool rememberFilterValues;
        internal bool respectFilters = true;
        private RowProviderInitEventArgs rowArgs;
        private DataTable rowTable;
        internal bool searchBehaviour;
        internal static readonly string sep = "{DB02F8DE-30FE-47c3-BFE8-8E5BD525989B}";
        private string serializedFilters = string.Empty;
        internal bool showClearButtons;
        internal bool suppressSpacing;
        internal bool suppressUnknownFilters;
        internal FilterToolPart toolPart;
        internal Transform transform;
        internal bool urlParams;
        internal readonly List<KeyValuePair<string, string>> validFilterNames = new List<KeyValuePair<string, string>>();
        private System.Web.UI.WebControls.WebParts.WebPart viewPart;
        protected internal readonly List<KeyValuePair<FilterBase, Exception>> warningsErrors = new List<KeyValuePair<FilterBase, Exception>>();
        internal static readonly string xsltTypeName = "Microsoft.SharePoint.WebPartPages.XsltListViewWebPart";

        public event CellConsumerInitEventHandler CellConsumerInit;

        public event CellProviderInitEventHandler CellProviderInit;

        public event CellReadyEventHandler CellReady;

        public event ClearFilterEventHandler ClearFilter;

        public event NoFilterEventHandler NoFilter;

        public event RowProviderInitEventHandler RowProviderInit;

        public event RowReadyEventHandler RowReady;

        public event SetFilterEventHandler SetFilter;

        public roxority_FilterWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
            this.ChromeType = PartChromeType.None;
            base.urlPropertyPrefix = "filter_";
        }


        internal void AddViewFields(XmlDocument doc, SPList list, string[] viewFields)
        {
            XmlNode node = doc.DocumentElement.SelectSingleNode("/View/ViewFields");
            if (node != null)
            {
                foreach (string str in viewFields)
                {
                    if (node.SelectSingleNode("FieldRef[@Name='" + str + "']") == null)
                    {
                        node.AppendChild(doc.CreateElement("FieldRef")).Attributes.SetNamedItem(doc.CreateAttribute("Name")).Value = str;
                    }
                }
            }
        }

        internal void AddViewFields(XmlDocument doc, SPList list, Hashtable ht)
        {
            ht["_list"] = list;
            foreach (XmlNode node in doc.DocumentElement.SelectNodes("/View/ViewFields/FieldRef"))
            {
                string str;
                SPField field = ProductPage.GetField(list, str = XmlUtil.GetAttributeValue(node, "Name", string.Empty));
                if (field != null)
                {
                    ht[str] = field.Title;
                }
            }
        }

        internal void Apply<T>(IEnumerable<T> webParts, bool isDfwp14)
        {
            if (webParts != null)
            {
                foreach (T local in webParts)
                {
                    ListViewWebPart webPart = local as ListViewWebPart;
                    if (webPart != null)
                    {
                        if (this.Apply(webPart, new Hashtable()))
                        {
                            this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { roxCollapseGroups('" + webPart.ID + "'); }); </script>"));
                        }
                    }
                    else
                    {
                        DataFormWebPart part = local as DataFormWebPart;
                        if (part != null)
                        {
                            this.Apply(part, isDfwp14, new Hashtable());
                        }
                    }
                }
            }
        }

        internal bool Apply(ListViewWebPart webPart, Hashtable listAndViewFields)
        {
            string str;
            SPWeb web;
            bool expandGroups = this.CamlFilters && this.RecollapseGroups;
            SPList list = null;
            XmlDocument doc = new XmlDocument();
            new Hashtable();
            Hashtable moreFilters = new Hashtable();
            HttpContext context = this.Context;
            Guid guid = ProductPage.GetGuid(webPart.ViewGuid);
            SPView nuView = null;
            Reflector reflector = new Reflector(typeof(ListViewWebPart).Assembly);
            ViewType none = ViewType.None;
            if ((this.RespectFilters && (context != null)) && ((guid != Guid.Empty) && (guid == ProductPage.GetGuid(context.Request["View"]))))
            {
                for (int i = 1; i <= 0x7fffffff; i++)
                {
                    if (string.IsNullOrEmpty(context.Request["FilterField" + i]))
                    {
                        break;
                    }
                    moreFilters[context.Request["FilterField" + i]] = context.Request["FilterValue" + i] + string.Empty;
                }
            }
            this.disps.Add(web = SPContext.Current.Site.OpenWeb(Guid.Empty.Equals(webPart.WebId) ? SPContext.Current.Web.ID : webPart.WebId));
            try
            {
                list = web.Lists[new Guid(webPart.ListName)];
            }
            catch
            {
                try
                {
                    list = web.Lists[webPart.ListName];
                }
                catch (Exception exception)
                {
                    this.additionalWarningsErrors.Add(exception.ToString());
                }
            }
            doc.LoadXml(str = this.Apply(list, webPart.ListViewXml, this.FolderScope, ref expandGroups, moreFilters, webPart.ViewGuid, null, ref nuView));
            this.AddViewFields(doc, list, listAndViewFields);
            if (nuView != null)
            {
                webPart.ViewGuid = ProductPage.GuidBracedUpper(nuView.ID);
                try
                {
                    none = (ViewType)Enum.Parse(typeof(ViewType), nuView.Type, true);
                }
                catch
                {
                }
                if (none != ViewType.None)
                {
                    webPart.ViewType = none;
                }
                reflector.Set(webPart, "view", nuView, new object[0]);
            }
            try
            {
                webPart.ListViewXml = str;
            }
            catch (XmlException exception2)
            {
                if (!exception2.Message.Contains("EntityName"))
                {
                    throw;
                }
                foreach (XmlNode node in doc.SelectNodes("*"))
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        attribute.Value = attribute.Value.Replace("&", "&amp;");
                    }
                }
                webPart.ListViewXml = doc.DocumentElement.OuterXml;
            }
            object[] objArray = new object[] { webPart.GetType().Name + ": &quot;" + webPart.EffectiveTitle + "&quot;", this.GeneratedQuery, (string.IsNullOrEmpty(this.GeneratedQuery) && string.IsNullOrEmpty(this.finalJson)) ? "none" : "inline", this.finalJson };
            this.eventOrderLog.Add(base["LogCaml", objArray]);
            return expandGroups;
        }

        internal void Apply(DataFormWebPart webPart, bool is14, Hashtable listAndViewFields)
        {
            string str2 = string.Empty;
            string str3 = string.Empty;
            object obj3 = Reflector.Current.Get(webPart, "ChildControlsCreated", new object[0]);
            bool recollapseGroups = this.RecollapseGroups;
            if (obj3 is bool)
            {
                new bool?((bool)obj3);
            }
            SPDataSource dataSource = is14 ? null : (webPart.DataSource as SPDataSource);
            XmlDocument document = new XmlDocument();
            Hashtable hashtable = new Hashtable();
            HttpContext context = this.Context;
            SPView nuView = null;
            if (this.DisableFilters && !this.DisableFiltersSome)
            {
                Reflector.Current.Set(webPart, "_disableColumnFiltering", true, new object[0]);
            }
            if (is14)
            {
                try
                {
                    str3 = (string)webPart.GetType().GetProperty("XmlDefinition").GetValue(webPart, null);
                }
                catch
                {
                }
                if (string.IsNullOrEmpty(str3))
                {
                    is14 = false;
                    dataSource = webPart.DataSource as SPDataSource;
                }
            }
            if ((dataSource == null) && !is14)
            {
                if (((BdcFilterCanApplyMethod != null) && (BdcFilterApplyMethod != null)) && ((bool)BdcFilterCanApplyMethod.Invoke(null, new object[] { this, webPart })))
                {
                    BdcFilterApplyMethod.Invoke(null, new object[] { this, webPart });
                    object[] objArray3 = new object[] { webPart.GetType().Name + ": &quot;" + webPart.EffectiveTitle + "&quot;", this.GeneratedQuery, (string.IsNullOrEmpty(this.GeneratedQuery) && string.IsNullOrEmpty(this.finalJson)) ? "none" : "inline", this.finalJson };
                    this.eventOrderLog.Add(base["LogCaml", objArray3]);
                }
                else if (webPart.GetType().GetProperty("CustomQuery", BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    Reflector.Current.Set(webPart, "CustomQuery", this.Apply(null, Reflector.Current.Get(webPart, "QueryOverride", new object[0]) + string.Empty, this.FolderScope, ref recollapseGroups, (((webPart.FilterValues == null) || (webPart.FilterValues.Collection == null)) || (webPart.FilterValues.Collection.Count == 0)) ? hashtable : webPart.FilterValues.Collection, null, null, ref nuView), new object[0]);
                }
                else
                {
                    object[] objArray4 = new object[] { webPart.DisplayTitle };
                    this.additionalWarningsErrors.Add(base["DataFormNoCaml", objArray4]);
                }
            }
            else
            {
                string str;
                object obj2;
                if (((this.RespectFilters && (context != null)) && !string.IsNullOrEmpty(context.Request["View"])) && ((!string.IsNullOrEmpty(str3) || (dataSource != null)) && !string.IsNullOrEmpty(context.Request["FilterField1"])))
                {
                    document.LoadXml(string.IsNullOrEmpty(str3) ? dataSource.SelectCommand : str3);
                    if (ProductPage.GetGuid(XmlUtil.GetAttributeValue(document.DocumentElement, "Name", string.Empty)).Equals(ProductPage.GetGuid(context.Request["View"])))
                    {
                        for (int i = 1; i <= 0x7fffffff; i++)
                        {
                            if (string.IsNullOrEmpty(context.Request["FilterField" + i]))
                            {
                                break;
                            }
                            hashtable[context.Request["FilterField" + i]] = context.Request["FilterValue" + i] + string.Empty;
                        }
                    }
                }
                document.LoadXml(str2 = this.Apply(is14 ? (Reflector.Current.Get(webPart, "SPList", new object[0]) as SPList) : dataSource.List, is14 ? str3 : dataSource.SelectCommand, this.FolderScope, ref recollapseGroups, (((webPart.FilterValues == null) || (webPart.FilterValues.Collection == null)) || (webPart.FilterValues.Collection.Count == 0)) ? hashtable : webPart.FilterValues.Collection, Reflector.Current.Get(webPart, "ViewID", new object[0]), null, ref nuView));
                object[] objArray5 = new object[] { webPart.GetType().Name + ": &quot;" + webPart.EffectiveTitle + "&quot;", this.GeneratedQuery, (string.IsNullOrEmpty(this.GeneratedQuery) && string.IsNullOrEmpty(this.finalJson)) ? "none" : "inline", this.finalJson };
                this.eventOrderLog.Add(base["LogCaml", objArray5]);
                if ((((obj2 = this.Context.Application[str = webPart.ID]) == null) || ((is14 ? str3 : dataSource.SelectCommand) != obj2.ToString())) || (is14 ? str3 : dataSource.SelectCommand).Equals(this.Context.Application["orig_" + str]))
                {
                    this.Context.Application["orig_" + str] = is14 ? str3 : dataSource.SelectCommand;
                    this.Context.Application[str] = str2;
                    if (!is14)
                    {
                        dataSource.SelectCommand = str2;
                    }
                }
                else if ((is14 ? str3 : dataSource.SelectCommand) != str2)
                {
                    this.Context.Application[str] = str2;
                    if (!is14)
                    {
                        dataSource.SelectCommand = str2;
                    }
                }
                if (!is14)
                {
                    dataSource.DataBind();
                }
                else
                {
                    try
                    {
                        foreach (Control control in webPart.Controls)
                        {
                            if (control is Literal)
                            {
                                ((Literal)control).Text = "";
                            }
                        }
                        Reflector.Current.Set(webPart, "_viewXml", null, new object[0]);
                        Reflector.Current.Set(webPart, "XmlDefinition", str2, new object[0]);
                        Reflector.Current.Set(webPart, "_singleDataSource", null, new object[0]);
                        Reflector.Current.Set(webPart, "_dataSource", null, new object[0]);
                        Reflector.Current.Set(webPart, "_schemaXml", str2, new object[0]);
                        if (!ProductPage.Is14)
                        {
                            Reflector.Current.Call(webPart, "ForceTransformRerun", null, null);
                            webPart.DataBind();
                            foreach (Control control2 in webPart.Controls)
                            {
                                if (control2 is Literal)
                                {
                                    try
                                    {
                                        ((Literal)control2).Text.ToString();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                if (((!is14 && (webPart.GetType().FullName != xsltTypeName)) && webPart.FireInitialRow) && (base.DesignMode || base.WebPartManager.DisplayMode.AllowPageDesign))
                {
                    object[] objArray6 = new object[] { webPart.EffectiveTitle };
                    this.additionalWarningsErrors.Add(base["CamlDataFormFail", objArray6]);
                }
                if (!this.appliedParts.Contains(webPart))
                {
                    this.appliedParts.Add(webPart);
                }
                if (this.deferredActions.ContainsKey(webPart))
                {
                    this.deferredActions[webPart](webPart);
                    this.extraHide = true;
                }
            }
        }

        internal string Apply(SPList list, string viewXml, string folderScope, ref bool expandGroups, Hashtable moreFilters, object altViewID, string[] addViewFields, ref SPView nuView)
        {
            string str4;
            ArrayList list3;
            Hashtable hashtable;
            Hashtable hashtable2;
            Hashtable hashtable3;
            KeyValuePair<string, List<FilterPair>> pair;
            Guid guid;
            XmlNode node;
            Comparison<int> comparison = null;
            Dictionary<int, Dictionary<string, string>> cdIDMaps;
            string json = string.Empty;
            int num = 1;
            ArrayList flist = new ArrayList();
            Hashtable ht = null;
            Hashtable custJson = null;
            List<KeyValuePair<string, List<FilterPair>>> list4 = new List<KeyValuePair<string, List<FilterPair>>>();
            List<string> effectiveAndFilters = this.EffectiveAndFilters;
            List<string> list6 = new List<string>();
            XmlDocument doc = new XmlDocument();
            XmlDocument document2 = new XmlDocument();
            List<FilterBase.CamlDistinct> list7 = new List<FilterBase.CamlDistinct>();
            List<string> messages = new List<string>();
            roxority_FilterZen.FilterBase.Lookup.Multi multi = null;
            this.connectedList = list;
            if (list != null)
            {
                using (IEnumerator<SPField> enumerator = ProductPage.TryEach<SPField>(list.Fields).GetEnumerator())
                {
                    SPField fld;
                    while (enumerator.MoveNext())
                    {
                        fld = enumerator.Current;
                        if (!this.validFilterNames.Exists(kvp => kvp.Key == fld.InternalName))
                        {
                            this.validFilterNames.Add(new KeyValuePair<string, string>(fld.InternalName, fld.Title));
                        }
                    }
                }
            }
            List<roxority_FilterZen.FilterBase.Multi.MultiTextCollection> filterConditions = null;
            foreach (FilterBase base2 in this.GetFilters(false, false, true))
            {
                if (base2.Enabled)
                {
                    if (base2 is FilterBase.CamlDistinct)
                    {
                        list7.Add(base2 as FilterBase.CamlDistinct);
                    }
                    else if (base2 is roxority_FilterZen.FilterBase.Lookup.Multi)
                    {
                        json = (multi = base2 as roxority_FilterZen.FilterBase.Lookup.Multi).GetJson();
                    }
                    else
                    {
                        list6.Add(base2.Name);
                    }
                }
            }
            if (multi != null)
            {
                filterConditions = multi.FilterConditions;
            }

            if (list7.Count > 0)
            {
                SPQuery query=new SPQuery();
                Dictionary<string, Dictionary<string, int>> dictionary2 = new Dictionary<string, Dictionary<string, int>>();
                Dictionary<string, Dictionary<string, List<int>>> dictionary = new Dictionary<string, Dictionary<string, List<int>>>();
                cdIDMaps = new Dictionary<int, Dictionary<string, string>>();
                List<int> list8 = new List<int>();
                List<int> list9 = new List<int>();
                query = new SPQuery
                {
                    AutoHyperlink = query.ExpandRecurrence = query.ExpandUserField = query.IncludeAllUserPermissions = query.IncludeAttachmentUrls = query.IncludeAttachmentVersion = query.IncludeMandatoryColumns = query.IncludePermissions = query.IndividualProperties = query.ItemIdQuery = false,
                    ViewAttributes = "FailIfEmpty=\"FALSE\" RequiresClientIntegration=\"FALSE\" Threaded=\"FALSE\" Scope=\"Recursive\"",
                    ViewFields = "<FieldRef Name=\"ID\"/>"
                };
                foreach (FilterBase.CamlDistinct distinct in list7)
                {
                    if (!dictionary.ContainsKey(distinct.Name))
                    {
                        dictionary2[distinct.Name] = new Dictionary<string, int>();
                        dictionary[distinct.Name] = new Dictionary<string, List<int>>();
                        query.ViewFields = query.ViewFields + "<FieldRef Name=\"" + distinct.Name + "\"/>";
                    }
                }
                foreach (SPListItem item in ProductPage.TryEach<SPListItem>(list.GetItems(query)))
                {
                    list8.Add(item.ID);
                    cdIDMaps[item.ID] = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, Dictionary<string, List<int>>> pair2 in dictionary)
                    {
                        string str2;
                        try
                        {
                            str2 = item[item.Fields.GetFieldByInternalName(pair2.Key).Id] + string.Empty;
                        }
                        catch
                        {
                            str2 = string.Empty;
                        }
                        cdIDMaps[item.ID][pair2.Key] = str2;
                        if (!pair2.Value.ContainsKey(str2))
                        {
                            pair2.Value[str2] = new List<int>();
                        }
                        pair2.Value[str2].Add(item.ID);
                    }
                }
                if (comparison == null)
                {
                    comparison = delegate(int id1, int id2)
                    {
                        int num111 = cdIDMaps[id2].Count.CompareTo(cdIDMaps[id1].Count);
                        if (num111 != 0)
                        {
                            return num111;
                        }
                        return id1.CompareTo(id2);
                    };
                }
                list8.Sort(comparison);
                foreach (int num2 in list8)
                {
                    foreach (KeyValuePair<string, string> pair3 in cdIDMaps[num2])
                    {
                        if (!dictionary2[pair3.Key].ContainsKey(pair3.Value))
                        {
                            if (!list9.Contains(num2))
                            {
                                list9.Add(num2);
                            }
                            dictionary2[pair3.Key][pair3.Value] = num2;
                        }
                    }
                }
                if (list9.Count > 0)
                {
                    flist.Add(hashtable = new Hashtable());
                    hashtable["k"] = "ID";
                    hashtable["v"] = hashtable2 = new Hashtable();
                    hashtable2["v"] = false;
                    hashtable2["k"] = list3 = new ArrayList();
                    foreach (int num3 in list9)
                    {
                        list3.Add(hashtable3 = new Hashtable());
                        hashtable3["k"] = num3.ToString();
                        hashtable3["v"] = "Eq";
                    }
                }
            }
            if (moreFilters != null)
            {
                IDictionaryEnumerator enumerator8 = moreFilters.GetEnumerator();
                {
                    Predicate<KeyValuePair<string, List<FilterPair>>> match = null;
                    DictionaryEntry entry;
                    while (enumerator8.MoveNext())
                    {
                        entry = (DictionaryEntry)enumerator8.Current;
                        pair = new KeyValuePair<string, List<FilterPair>>(entry.Key.ToString(), new List<FilterPair>());
                        if (list4.Count > 0)
                        {
                            if (match == null)
                            {
                                match = test => test.Key.Equals(entry.Key.ToString());
                            }
                            pair = list4.Find(match);
                        }
                        if (string.IsNullOrEmpty(pair.Key) || (pair.Value == null))
                        {
                            pair = new KeyValuePair<string, List<FilterPair>>(entry.Key.ToString(), new List<FilterPair>());
                        }
                        list4.Remove(pair);
                        pair.Value.Add(new FilterPair(entry.Key.ToString(), entry.Value + string.Empty, CamlOperator.Eq));
                        list4.Add(pair);
                    }
                }
            }
            using (List<KeyValuePair<string, FilterPair>>.Enumerator enumerator9 = this.PartFilters.GetEnumerator())
            {
                Predicate<KeyValuePair<string, List<FilterPair>>> predicate2 = null;
                KeyValuePair<string, FilterPair> kvp;
                while (enumerator9.MoveNext())
                {
                    kvp = enumerator9.Current;
                    pair = new KeyValuePair<string, List<FilterPair>>(kvp.Key, new List<FilterPair>());
                    if (list4.Count > 0)
                    {
                        if (predicate2 == null)
                        {
                            predicate2 = test => test.Key.Equals(kvp.Key);
                        }
                        pair = list4.Find(predicate2);
                    }
                    //if (string.IsNullOrEmpty(pair.Key) || (kvp.Value == null))  //modify by:lhan
                    //{
                        pair = new KeyValuePair<string, List<FilterPair>>(kvp.Key, new List<FilterPair>());
                    //}
                    list4.Remove(pair);
                    pair.Value.Add(kvp.Value);
                    list4.Add(pair);
                }
            }

            int position = 0;
            foreach (KeyValuePair<string, List<FilterPair>> pair4 in list4)
            {
                flist.Add(hashtable = new Hashtable());
                hashtable["k"] = pair4.Key;
                hashtable["v"] = hashtable2 = new Hashtable();
                hashtable2["v"] = effectiveAndFilters.Contains(pair4.Key);
                hashtable2["k"] = list3 = new ArrayList();
                foreach (FilterPair pair5 in pair4.Value)
                {
                    list3.Add(hashtable3 = new Hashtable());
                    hashtable3["k"] = pair5.Value;
                    hashtable3["v"] = this.GetOperator(pair5, ref position).ToString();
                    position++;
                }
            }

            //if (list4 != null) //modified by:lhan
            //{
            //    int position = 0;
            //    for (int i = 0; i < list4.Count; i++)
            //    {
            //        KeyValuePair<string, List<FilterPair>> pair4 = list4[i];
            //        flist.Add(hashtable = new Hashtable());
            //        hashtable["k"] = pair4.Key;
            //        hashtable["v"] = hashtable2 = new Hashtable();
            //        hashtable2["v"] = effectiveAndFilters.Contains(pair4.Key);
            //        hashtable2["k"] = list3 = new ArrayList();
            //        if (pair4.Value != null)
            //        {
            //            for (int j = 0; j < pair4.Value.Count; j++)
            //            {
            //                position++;
            //                FilterPair pair5 = pair4.Value[j];
            //                list3.Add(hashtable3 = new Hashtable());
            //                hashtable3["k"] = pair5.Value;
            //                hashtable3["v"] = this.GetOperator(pair5, ref position).ToString();
            //            }
            //        }
            //    }
            //}


            if (!string.IsNullOrEmpty(this.ListViewUrl))
            {
                foreach (SPView view in ProductPage.TryEach<SPView>(list.Views))
                {
                    if (this.ListViewUrl.Equals(view.Url, StringComparison.InvariantCultureIgnoreCase))
                    {
                        nuView = view;
                        altViewID = guid = view.ID;
                        this.connectedView = view;
                        if (string.IsNullOrEmpty(viewXml))
                        {
                            viewXml = view.SchemaXml;
                        }
                        else
                        {
                            doc.LoadXml(view.SchemaXml);
                            document2.LoadXml(viewXml);
                            document2.DocumentElement.InnerXml = doc.DocumentElement.InnerXml;
                            foreach (XmlAttribute attribute2 in doc.DocumentElement.Attributes)
                            {
                                XmlAttribute namedItem = document2.DocumentElement.Attributes.GetNamedItem(attribute2.LocalName, attribute2.NamespaceURI) as XmlAttribute;
                                if (namedItem != null)
                                {
                                    namedItem.Value = attribute2.Value;
                                }
                                else if ("Scope".Equals(attribute2.LocalName, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    document2.DocumentElement.Attributes.Append(document2.CreateAttribute(attribute2.Prefix, attribute2.LocalName, attribute2.NamespaceURI)).Value = attribute2.Value;
                                }
                            }
                            viewXml = document2.DocumentElement.OuterXml;
                        }
                        break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(viewXml))
            {
                doc.LoadXml(viewXml);
                if (!string.IsNullOrEmpty(folderScope) && ((flist.Count > 0) || !ProductPage.Config<bool>(ProductPage.GetContext(), "NoFoldersAlt")))
                {
                    doc.DocumentElement.SetAttribute("Scope", folderScope);
                    viewXml = doc.DocumentElement.OuterXml;
                }
                if ((this.connectedView == null) && !Guid.Empty.Equals(guid = ProductPage.GetGuid((XmlUtil.Attribute(doc.DocumentElement, "Name", altViewID + string.Empty) == null) ? Guid.Empty.ToString() : XmlUtil.Attribute(doc.DocumentElement, "Name", altViewID + string.Empty))))
                {
                    foreach (SPView view2 in list.Views)
                    {
                        if (view2.ID.Equals(guid))
                        {
                            this.connectedView = view2;
                            break;
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(str4 = this.JsonFilters))
            {
                if (list6.Count == 0)
                {
                    str4 = json;
                }
                else if (multi != null)
                {
                    str4 = "{\"" + (this.DefaultToOr ? "OR" : "AND") + "\":[";
                    for (int i = 0; i < list6.Count; i++)
                    {
                        str4 = str4 + "\"" + list6[i] + "\",";
                        if (i < (list6.Count - 1))
                        {
                            num++;
                            str4 = str4 + "{\"" + (this.DefaultToOr ? "OR" : "AND") + "\":[";
                        }
                    }
                    str4 = str4 + "\"" + multi.Name + "\"";
                    for (int j = 0; j < num; j++)
                    {
                        str4 = str4 + "]}";
                    }
                }
            }
            if (!string.IsNullOrEmpty(str4) && ((str4 != json) || str4.StartsWith("{")))
            {
                if ((str4 != json) && !string.IsNullOrEmpty(json))
                {
                    try
                    {
                        object obj2 = JSON.JsonDecode(json);
                        if (obj2 is string)
                        {
                            str4 = str4.Replace("\"" + multi.Name + "\"", "\"" + obj2 + "\"");
                        }
                        else
                        {
                            custJson = obj2 as Hashtable;
                        }
                    }
                    catch (Exception exception)
                    {
                        messages.Add(base["JsonError", new object[0]] + exception.Message);
                    }
                }
                try
                {
                    ht = JSON.JsonDecode(str4) as Hashtable;
                    if (ht == null)
                    {
                        throw new Exception(base["JsonSyntax", new object[0]]);
                    }
                }
                catch (Exception exception2)
                {
                    messages.Add(base["JsonError", new object[0]] + exception2.Message);
                }
                if (custJson != null)
                {
                    if (ht == null)
                    {
                        ht = custJson;
                    }
                    else
                    {
                        try
                        {
                            this.MergeJson(ht, multi.Name, custJson);
                        }
                        catch (Exception exception3)
                        {
                            messages.Add(exception3.Message);
                        }
                    }
                }
                if (ht != null)
                {
                    this.ValidateJsonFilters(ht, flist, messages);
                }
                if (messages.Count > 0)
                {
                    this.additionalWarningsErrors.Add(base["JsonIssues", new object[0]] + "<ul><li>" + string.Join("</li><li>", messages.ConvertAll<string>(v => HttpUtility.HtmlEncode(v)).ToArray()) + "</li></ul>");
                }
            }
            this.finalJson = JSON.JsonEncode(ht);
            string str = ProductPage.ApplyCore(list, viewXml, doc, flist, ref expandGroups, this.DefaultToOr, (messages.Count == 0) ? ht : null, this.CamlSourceFilters, filterConditions);
            if (this.DisableFilters && !this.DisableFiltersSome)
            {
                str = str.Replace("]]></HTML><GetVar Name=\"FilterDisable\" HTMLEncode=\"TRUE\" /><HTML><![CDATA[", "TRUE");
            }
            if (!string.IsNullOrEmpty(str))
            {
                doc.LoadXml(str);
            }
            else if (list == null)
            {
                doc.RemoveAll();
            }
            if ((string.IsNullOrEmpty(this.ListViewUrl) && (addViewFields != null)) && (addViewFields.Length > 0))
            {
                this.AddViewFields(doc, list, addViewFields);
            }
            str = (doc.DocumentElement == null) ? string.Empty : doc.DocumentElement.OuterXml;
            if ((doc.DocumentElement != null) && ((node = doc.DocumentElement.SelectSingleNode("Query/Where")) != null))
            {
                this.GeneratedQuery = HttpUtility.HtmlEncode(node.OuterXml);
            }
            foreach (KeyValuePair<string, List<FilterPair>> pair6 in list4)
            {
                if (pair6.Value == null)
                {
                    this.debugFilters.Add(new KeyValuePair<string, string>(pair6.Key, null));
                }
                else
                {
                    foreach (FilterPair pair7 in pair6.Value)
                    {
                        this.debugFilters.Add(new KeyValuePair<string, string>(pair7.Key, pair7.Value));
                    }
                }
            }
            return str;
        }

        public override ConnectionRunAt CanRunAt()
        {
            if (!this.CanRun)
            {
                return ConnectionRunAt.None;
            }
            return ConnectionRunAt.Server;
        }

        protected override void CreateChildControls()
        {
            SPContext context = ProductPage.GetContext();
            this.PerformInitChecks();
            base.CreateChildControls();
            this.MultiPanel.ID = "MultiPanel";
            this.MultiPanel.Style[HtmlTextWriterStyle.Display] = "none";
            if (!this.Controls.Contains(this.MultiPanel))
            {
                this.Controls.Add(this.MultiPanel);
            }
            this.MultiTextBox.ID = "MultiTextBox";
            this.MultiTextBox.TextMode = TextBoxMode.MultiLine;
            this.MultiTextBox.Rows = 8;
            this.MultiTextBox.Width = new Unit(90.0, UnitType.Percentage);
            if (this.MultiPanel.Controls.Count == 0)
            {
                this.MultiPanel.Controls.Add(this.MultiTextBox);
                for (int i = 0; i < this.DatePickers.Length; i++)
                {
                    DateTimeControl control2;
                    DateTimeControl control = new DateTimeControl
                    {
                        ID = "DatePicker" + i,
                        Visible = this.HasMulti
                    };
                    this.DatePickers[i] = control2 = control;
                    this.MultiPanel.Controls.Add(ProductPage.InitializeDateTimePicker(control2));
                }
                for (int j = 0; j < this.PeoplePickers.Length; j++)
                {
                    PeopleEditor editor;
                    PeopleEditor editor2 = new PeopleEditor
                    {
                        ID = "PeoplePicker" + j,
                        AllUrlZones = true,
                        PlaceButtonsUnderEntityEditor = false,
                        CssClass = "ms-input",
                        Rows = 1,
                        ValidateResolvedEntity = true,
                        ValidatorEnabled = true,
                        MaximumEntities = 1,
                        MultiSelect = false,
                        Width = new Unit((double)this.MultiWidth, UnitType.Pixel),
                        Visible = this.HasMulti
                    };
                    this.PeoplePickers[j] = editor = editor2;
                    if (context != null)
                    {
                        editor.WebApplicationId = context.Site.WebApplication.Id;
                    }
                    this.MultiPanel.Controls.Add(editor);
                }
            }
        }

        internal FilterBase.Interactive CreateDynamicInteractiveFilter(KeyValuePair<string, string> kvp)
        {
            return this.CreateDynamicInteractiveFilter(kvp, true, false);
        }

        internal FilterBase.Interactive CreateDynamicInteractiveFilter(string name, bool isLookupFilter)
        {
            foreach (KeyValuePair<string, string> pair in this.validFilterNames)
            {
                if (pair.Key == name)
                {
                    return this.CreateDynamicInteractiveFilter(pair, false, isLookupFilter);
                }
            }
            return null;
        }

        internal FilterBase.Interactive CreateDynamicInteractiveFilter(KeyValuePair<string, string> kvp, bool dynID, bool isLookupFilter)
        {
            Converter<SPWeb, SPList> converter = null;
            Converter<SPWeb, SPList> converter2 = null;
            roxority.Shared.Action action3 = null;
            roxority.Shared.Action action4 = null;
            bool doTry2;
            ListViewWebPart listPart;
            SPDataSource dataSource;
            FilterBase.Interactive dynFilter = null;
            FilterBase.Choice choice = null;
            FilterBase.Lookup lookupFilter = null;
            roxority_FilterZen.FilterBase.Lookup.Multi.SqlData.User user = null;
            ListViewWebPart part = null;
            SPField field = null;
            SPFieldMultiChoice choice2 = null;
            SPFieldLookup lookupField = null;
            SPList connectedList = null;
            SPView view = null;
            Guid empty = Guid.Empty;
            SPWrap<SPList> wrap = null;
            foreach (System.Web.UI.WebControls.WebParts.WebPart part3 in this.connectedParts)
            {
                DataFormWebPart part2;
                if (((part2 = part3 as DataFormWebPart) != null) && ((dataSource = part2.DataSource as SPDataSource) != null))
                {
                    try
                    {
                        if (converter == null)
                        {
                            converter = web => dataSource.List;
                        }
                        wrap = new SPWrap<SPList>(SPContext.Current.Site, SPContext.Current.Web, converter);
                        connectedList = wrap.Value;
                        if (connectedList != null)
                        {
                            field = ProductPage.GetField(wrap.Value, kvp.Key);
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    listPart = part3 as ListViewWebPart;
                    if (listPart != null)
                    {
                        try
                        {
                            if (converter2 == null)
                            {
                                converter2 = web => web.Lists[new Guid(listPart.ListName)];
                            }
                            wrap = new SPWrap<SPList>(SPContext.Current.Site, SPContext.Current.Web, converter2);
                            connectedList = wrap.Value;
                            if (connectedList != null)
                            {
                                field = ProductPage.GetField(wrap.Value, kvp.Key);
                                part = listPart;
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if ((this.connectedList == null) && (connectedList != null))
            {
                this.connectedList = connectedList;
            }
            else if ((connectedList == null) && (this.connectedList != null))
            {
                connectedList = this.connectedList;
            }
            if (((connectedList == null) && (this.connectedList == null)) && isLookupFilter)
            {
                return null;
            }
            if (isLookupFilter)
            {
                dynFilter = lookupFilter = new FilterBase.Lookup();
            }
            else if (field is SPFieldBoolean)
            {
                dynFilter = new FilterBase.Boolean();
            }
            else
            {
                choice2 = field as SPFieldMultiChoice;
                if (choice2 != null)
                {
                    dynFilter = choice = new FilterBase.Choice();
                }
                else if (field is SPFieldDateTime)
                {
                    dynFilter = new FilterBase.Date();
                }
                else if (field is SPFieldUser)
                {
                    dynFilter = user = new roxority_FilterZen.FilterBase.Lookup.Multi.SqlData.User();
                }
                else
                {
                    lookupField = field as SPFieldLookup;
                    if (lookupField != null)
                    {
                        dynFilter = lookupFilter = new FilterBase.Lookup();
                    }
                }
            }
            if (dynFilter == null)
            {
                dynFilter = new roxority_FilterZen.FilterBase.Lookup.Multi.SqlData.Text();
            }
            if (dynID)
            {
                dynFilter.id = this.ID + kvp.Key;
            }
            dynFilter.parentWebPart = this;
            dynFilter.Name = kvp.Key;
            dynFilter.DefaultIfEmpty = true;
            dynFilter.Enabled = true;
            dynFilter.IsInteractive = true;
            dynFilter.Label = kvp.Value + ":";
            dynFilter.SendEmpty = false;
            dynFilter.suppressInteractive = false;
            if (user != null)
            {
                user.ItemID = 0;
            }
            if (choice != null)
            {
                choice.choices = new string[choice2.Choices.Count];
                choice2.Choices.CopyTo(choice.choices, 0);
            }
            if (lookupFilter != null)
            {
                if (isLookupFilter)
                {
                    lookupFilter.ListUrl = ProductPage.MergeUrlPaths(this.connectedList.ParentWeb.Url, this.connectedList.DefaultViewUrl);
                    if (lookupFilter.ListUrl.ToLowerInvariant().StartsWith(SPContext.Current.Web.Url.ToLowerInvariant().TrimEnd(new char[] { '/' }) + '/'))
                    {
                        lookupFilter.ListUrl = lookupFilter.ListUrl.Substring(SPContext.Current.Web.Url.ToLowerInvariant().TrimEnd(new char[] { '/' }).Length + 1);
                    }
                    lookupFilter.ItemID = 0;
                    lookupFilter.ItemSorting = 1;
                    lookupFilter.ValueFieldName = kvp.Key;
                    lookupFilter.stripID = (field is SPFieldLookup) || (field is SPFieldUser);
                    lookupFilter.AllowMultiEnter = this.CamlFilters;
                }
                else
                {
                    doTry2 = false;
                    if (action3 == null)
                    {
                        action3 = delegate
                        {
                            object obj2;
                            PropertyInfo property = lookupField.GetType().GetProperty("ViewUrl");
                            if ((property == null) || ((obj2 = property.GetValue(lookupField, null)) == null))
                            {
                                doTry2 = true;
                            }
                            else
                            {
                                lookupFilter.ListUrl = obj2.ToString();
                            }
                        };
                    }
                    roxority.Shared.Action action = action3;
                    if (action4 == null)
                    {
                        action4 = delegate
                        {
                            Guid guid = ProductPage.GetGuid(lookupField.LookupList);
                            if (Guid.Empty.Equals(guid))
                            {
                                dynFilter = (FilterBase.Interactive)(lookupFilter = null);
                            }
                            else
                            {
                                using (SPWeb web = SPContext.Current.Site.OpenWeb(lookupField.LookupWebId))
                                {
                                    lookupFilter.ListUrl = ProductPage.MergeUrlPaths(web.Url, web.Lists[guid].DefaultViewUrl);
                                }
                            }
                        };
                    }
                    roxority.Shared.Action action2 = action4;
                    try
                    {
                        action();
                        if (doTry2)
                        {
                            action2();
                        }
                    }
                    catch
                    {
                        if (doTry2)
                        {
                            dynFilter = (FilterBase.Interactive)(lookupFilter = null);
                        }
                        else
                        {
                            try
                            {
                                action2();
                            }
                            catch
                            {
                                dynFilter = (FilterBase.Interactive)(lookupFilter = null);
                            }
                        }
                    }
                    finally
                    {
                        if (lookupFilter != null)
                        {
                            lookupFilter.ItemID = 0;
                            lookupFilter.ValueFieldName = lookupField.LookupField;
                        }
                    }
                }
            }
            try
            {
                if ((((dynID && (dynFilter != null)) && (dynFilter.pickerSemantics && (part != null))) && (((connectedList != null) && !string.IsNullOrEmpty(part.ViewGuid)) && (!Guid.Empty.Equals(empty = new Guid(part.ViewGuid)) && ((view = connectedList.Views[empty]) != null)))) && !string.IsNullOrEmpty(view.Url))
                {
                    dynFilter.PostFilter = true;
                    dynFilter.PostFilterListViewUrl = ProductPage.MergeUrlPaths(connectedList.ParentWeb.Url, view.Url);
                    dynFilter.PostFilterFieldName = kvp.Key;
                    dynFilter.postFilterList = connectedList;
                    dynFilter.postFilterView = view;
                }
            }
            catch
            {
                dynFilter.PostFilter = false;
            }
            return dynFilter;
        }

        public override void Dispose()
        {
            if (this.rowTable != null)
            {
                this.rowTable.Dispose();
                this.rowTable = null;
            }
            foreach (FilterBase base2 in this.GetFilters(true, false))
            {
                IDisposable disposable = base2 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            foreach (IDisposable disposable2 in this.disps)
            {
                disposable2.Dispose();
            }
            base.Dispose();
        }

        internal void EnsureChildControls2()
        {
            this.EnsureChildControls();
        }

        public override void EnsureInterfaces()
        {
            bool flag = false;
            try
            {
                Guid guid;
                base.RegisterInterface("roxorityConsumeCell", "ICellConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, base["GetGroupFrom", new object[0]], base["GetGroupFrom", new object[0]], true);
                base.RegisterInterface("roxorityConsumeRow", "IRowConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, base["GetValuesFromRow", new object[0]], base["GetValuesFromRow", new object[0]], true);
                base.RegisterInterface("roxorityFilterProviderInterface", "IFilterProvider", -1, ConnectionRunAt.Server, this, "", base["SendUrlFilterTo", new object[0]], base["SendsDesc", new object[0]], true);
                base.RegisterInterface("roxorityProvideRow", "IRowProvider", -1, ConnectionRunAt.Server, this, "", base[this.HasPeople ? "SendRowToAlt" : "SendRowTo", new object[0]], base["SendsRowDesc", new object[0]], true);
                if (ProductPage.Config<bool>(SPContext.Current, "AllowCells"))
                {
                    base.RegisterInterface("roxorityProvideCell", "ICellProvider", -1, ConnectionRunAt.Server, this, "", base["SendCellTo", new object[0]], base["SendsCellDesc", new object[0]], true);
                }
                if (((this.Context.Request["ShowInGrid"] == "True") && !Guid.Empty.Equals(base.ConnectionID)) && !Guid.Empty.Equals(guid = ProductPage.GetGuid(this.Context.Request["View"], false)))
                {
                    foreach (System.Web.UI.WebControls.WebParts.WebPart part2 in base.WebPartManager.WebParts)
                    {
                        SPList list;
                        ListViewWebPart part;
                        if (((((part = part2 as ListViewWebPart) != null) && !Guid.Empty.Equals(part.ConnectionID)) && (part.Connections.Contains(ProductPage.GuidLower(part.ConnectionID, true) + "," + ProductPage.GuidLower(base.ConnectionID, true) + ",") && ProductPage.GetGuid(part.ViewGuid, false).Equals(guid))) && ((list = ProductPage.GetList(SPContext.Current.Web, ProductPage.GetGuid(part.ListName))) != null))
                        {
                            if ("redirect".Equals(ProductPage.Config(ProductPage.GetContext(), "Datasheet")))
                            {
                                foreach (SPView view in list.Views)
                                {
                                    if (((view.Type == "GRID") && !view.Hidden) && !view.PersonalView)
                                    {
                                        this.Context.Response.Redirect(ProductPage.MergeUrlPaths(SPContext.Current.Web.Url, view.Url), true);
                                        flag = true;
                                    }
                                }
                            }
                            if (!flag)
                            {
                                this.additionalWarningsErrors.Add(base["Datasheet", new object[] { SPContext.Current.Web.Url.TrimEnd(new char[] { '/' }), part.ListName }]);
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.regError = exception;
            }
        }

        internal IEnumerable<T> GetConnectedParts<T>() where T : System.Web.UI.WebControls.WebParts.WebPart
        {
            T iteratorVariable3;
            SPWebPartManager webPartManager = this.WebPartManager as SPWebPartManager;
            List<string> iteratorVariable1 = new List<string>();
            if ((webPartManager != null) && (webPartManager.SPWebPartConnections != null))
            {
                IEnumerator enumerator = webPartManager.SPWebPartConnections.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    SPWebPartConnection current = (SPWebPartConnection)enumerator.Current;
                    if (((current.Provider == null) && string.IsNullOrEmpty(current.ProviderID)) || (current.Consumer == null))
                    {
                        this.additionalWarningsErrors.Add(this["BrokenConnection", new object[] { current.ID }]);
                    }
                    else if (this.ID.Equals(current.ProviderID))
                    {
                        DataFormWebPart iteratorVariable2;
                        if (((iteratorVariable2 = current.Consumer as DataFormWebPart) != null) && !this.connectedDataParts.Contains(iteratorVariable2))
                        {
                            this.connectedDataParts.Add(iteratorVariable2);
                            this.nullParts--;
                        }
                        if (!this.connectedParts.Contains(current.Consumer))
                        {
                            this.connectedParts.Add(current.Consumer);
                        }
                        if (((iteratorVariable3 = current.Consumer as T) != null) && !iteratorVariable1.Contains(iteratorVariable3.ID))
                        {
                            iteratorVariable1.Add(iteratorVariable3.ID);
                            yield return iteratorVariable3;
                        }
                    }
                }
            }
            if (typeof(T).IsAssignableFrom(typeof(DataFormWebPart)))
            {
                foreach (DataFormWebPart iteratorVariable5 in this.connectedDataParts)
                {
                    if (iteratorVariable1.Contains(iteratorVariable5.ID))
                    {
                        continue;
                    }
                    iteratorVariable1.Add(iteratorVariable5.ID);
                    yield return (iteratorVariable5 as T);
                }
            }
            foreach (System.Web.UI.WebControls.WebParts.WebPart iteratorVariable6 in this.connectedParts)
            {
                if (((iteratorVariable3 = iteratorVariable6 as T) == null) || iteratorVariable1.Contains(iteratorVariable3.ID))
                {
                    continue;
                }
                iteratorVariable1.Add(iteratorVariable3.ID);
                yield return iteratorVariable3;
            }
            if (iteratorVariable1.Count > 0)
            {
                this._connected = true;
            }
        }

        public IEnumerable<System.Web.UI.WebControls.WebParts.WebPart> GetConnectedParts()
        {
            return this.GetConnectedParts<System.Web.UI.WebControls.WebParts.WebPart>();
        }

        [ConnectionProvider("Parameters", "roxorityParametersProviderInterface", typeof(Transform.Provider), AllowsMultipleConnections = true)]
        public IWebPartParameters GetConnectionInterface()
        {
            return this;
        }

        public List<FilterBase> GetFilters()
        {
            return this.FiltersList;
        }

        internal List<FilterBase> GetFilters(bool includeDynamicFilters, bool useGroups)
        {
            return this.GetFilters(includeDynamicFilters, false, useGroups);
        }

        internal List<FilterBase> GetFilters(bool includeDynamicFilters, bool suggestLookupFilters, bool useGroups)
        {
            List<FilterBase> list = new List<FilterBase>();
            List<FilterBase> filtersList = this.FiltersList;
            string selGroup = string.Empty;
            if (useGroups && (this.GetGroups().Count > 1))
            {
                includeDynamicFilters = false;
                selGroup = this.SelectedGroup;
            }
            if ((includeDynamicFilters && (this.dynamicFilters == null)) && ((this.DynamicInteractiveFilters > 0) && (this.validFilterNames.Count > 0)))
            {
                this.dynamicFilters = new List<FilterBase>();
                using (List<KeyValuePair<string, string>>.Enumerator enumerator = this.validFilterNames.GetEnumerator())
                {
                    Predicate<FilterBase> match = null;
                    KeyValuePair<string, string> kvp;
                    while (enumerator.MoveNext())
                    {
                        kvp = enumerator.Current;
                        if (match == null)
                        {
                            match = filter => filter.Name.Equals(kvp.Key);
                        }
                        if (!this.filters.Exists(match))
                        {
                            FilterBase.Interactive item = this.CreateDynamicInteractiveFilter(kvp);
                            if (item != null)
                            {
                                this.dynamicFilters.Add(item);
                            }
                            item = this.CreateDynamicInteractiveFilter(kvp, true, true);
                            if (item != null)
                            {
                                this.dynamicFilters.Add(item);
                            }
                        }
                    }
                }
            }
            if ((includeDynamicFilters && (this.DynamicInteractiveFilters == 1)) && (this.dynamicFilters != null))
            {
                list.AddRange(this.dynamicFilters);
            }
            list.AddRange(this.filters);
            if ((includeDynamicFilters && (this.DynamicInteractiveFilters == 2)) && (this.dynamicFilters != null))
            {
                list.AddRange(this.dynamicFilters);
            }
            if (!string.IsNullOrEmpty(selGroup))
            {
                list.RemoveAll(fb => !fb.groups.Contains(selGroup));
            }
            return list;
        }

        internal string GetFilterValue(string val)
        {
            if (!this.Exed)
            {
                return val;
            }
            return base.ExpiredMessage;
        }

        internal List<string> GetGroups()
        {
            List<string> list = new List<string>();
            string str2 = (this.toolPart != null) ? this.toolPart.groupsTextBox.Text : base.GetProp<string>("Groups", this.Groups);
            if (base.LicEd(4))
            {
                if (str2 == this.Groups)
                {
                    return this.groups;
                }
                foreach (string str3 in ProductPage.Trim(str2, new char[0]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string str;
                    if (!string.IsNullOrEmpty(str = str3.Trim().Replace(",", sep)) && !list.Contains(str))
                    {
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        public override InitEventArgs GetInitEventArgs(string interfaceName)
        {
            CellConsumerInitEventArgs args = (interfaceName == "roxorityConsumeCell") ? new CellConsumerInitEventArgs() : null;
            if (args != null)
            {
                args.FieldDisplayName = args.FieldName = this.EffectiveCellFieldName;
            }
            if (!(interfaceName == "roxorityProvideRow"))
            {
                return args;
            }
            return this.RowArgs;
        }

        internal bool GetLop(FilterPair fp,int position) //modified by:lhan
        {
            bool nextAnd = fp.nextAnd;
            //foreach (FilterBase base2 in this.GetFilters(false, false, true))
            //{
            //    roxority_FilterZen.FilterBase.Lookup.Multi multi = base2 as roxority_FilterZen.FilterBase.Lookup.Multi;
            //    if (multi != null)
            //    {
            //        return multi.IsAnd(fp.Key,fp.Value, nextAnd);
            //    }
            //}

            List<FilterBase> filterBases = this.GetFilters(false, false, true);
            for (int i = 0; i < filterBases.Count;i++ )
            {
                roxority_FilterZen.FilterBase.Lookup.Multi multi = filterBases[i] as roxority_FilterZen.FilterBase.Lookup.Multi;
                if (multi != null)
                {
                    return multi.IsAnd(fp.Key, fp.Value, position+i, nextAnd);
                }
            }

            return nextAnd;
        }

        internal CamlOperator GetOperator(FilterPair fp,ref int position) //modified by:lhan
        {
            CamlOperator camlOperator = fp.CamlOperator;
            //foreach (FilterBase base2 in this.GetFilters(false, false, true))
            //{
            //    roxority_FilterZen.FilterBase.Lookup.Multi multi = base2 as roxority_FilterZen.FilterBase.Lookup.Multi;
            //    if (multi != null)
            //    {
            //        return multi.GetOperator(fp.Key,fp.Value, camlOperator);
            //    }
            //}

            List<FilterBase> filterBases = this.GetFilters(false, false, true); //modified by:lhan
            if (filterBases != null)
            {
                foreach (FilterBase base2 in this.GetFilters(false, false, true))
                {
                    roxority_FilterZen.FilterBase.Lookup.Multi multi = base2 as roxority_FilterZen.FilterBase.Lookup.Multi;
                    if (multi != null)
                    {
                        return multi.GetOperator(fp.Key, fp.Value, position, camlOperator);
                    }
                    position++;
                }
            }
            return camlOperator;
        }

        public void GetParametersData(ParametersCallback callback)
        {
            Hashtable parametersData = new Hashtable();
            if (callback != null)
            {
                this._connected = true;
                foreach (KeyValuePair<string, FilterPair> pair in this.PartFilters)
                {
                    parametersData[pair.Value.Key] = pair.Value.Value;
                    this.eventOrderLog.Add(base["LogSent", new object[] { base["Parameters", new object[] { pair.Value.Key }] }]);
                }
                callback(parametersData);
            }
        }

        public override ToolPart[] GetToolParts()
        {
            List<ToolPart> list = new List<ToolPart>(base.GetToolParts());
            if (this.CanRun)
            {
                list.Insert(0, this.toolPart = new FilterToolPart(base["FilterWebPart_Title", new object[0]]));
            }
            return list.ToArray();
        }

        internal void MergeJson(Hashtable ht, string name, Hashtable custJson)
        {
            foreach (DictionaryEntry entry in ht)
            {
                ArrayList list = entry.Value as ArrayList;
                if (name.Equals(list[1]))
                {
                    list[1] = custJson;
                }
                else if (name.Equals(list[0]))
                {
                    if (!(list[1] is string))
                    {
                        throw new Exception(Convert.ToString(base["JsonCraze", new object[] { FilterBase.GetFilterTypeTitle(typeof(roxority_FilterZen.FilterBase.Lookup.Multi)), name }]));
                    }
                    list[0] = list[1];
                    list[1] = custJson;
                }
                else if (list[1] is Hashtable)
                {
                    this.MergeJson(list[1] as Hashtable, name, custJson);
                }
            }
        }

        void ICellConsumer.CellProviderInit(object sender, CellProviderInitEventArgs cellProviderInitArgs)
        {
            this.cell = new string[] { cellProviderInitArgs.FieldName, cellProviderInitArgs.FieldDisplayName };
        }

        void ICellConsumer.CellReady(object sender, CellReadyEventArgs cellReadyArgs)
        {
            object[] objArray = new object[] { this.EffectiveCellFieldName, this.SelectedGroup = (cellReadyArgs.Cell == null) ? string.Empty : cellReadyArgs.Cell.ToString() };
            this.eventOrderLog.Add(base["LogReceived", objArray]);
            this.partFilters = null;
        }

        void ICellProvider.CellConsumerInit(object sender, CellConsumerInitEventArgs cellConsumerInitEventArgs)
        {
            Predicate<KeyValuePair<string, string>> match = null;
            if ((cellConsumerInitEventArgs != null) && !string.IsNullOrEmpty(cellConsumerInitEventArgs.FieldName))
            {
                this.cellNames = new string[] { cellConsumerInitEventArgs.FieldName, cellConsumerInitEventArgs.FieldDisplayName };
                if (match == null)
                {
                    match = value => value.Key.Equals(cellConsumerInitEventArgs.FieldName);
                }
                if (!this.validFilterNames.Exists(match))
                {
                    this.validFilterNames.Add(new KeyValuePair<string, string>(cellConsumerInitEventArgs.FieldName, string.IsNullOrEmpty(cellConsumerInitEventArgs.FieldDisplayName) ? cellConsumerInitEventArgs.FieldName : cellConsumerInitEventArgs.FieldDisplayName));
                }
            }
        }

        void IFilterProvider.FilterConsumerInit(object sender, FilterConsumerInitEventArgs filterConsumerInitEventArgs)
        {
            string[] fieldList = null;
            string[] fieldDisplayList = null;
            if (filterConsumerInitEventArgs != null)
            {
                fieldList = filterConsumerInitEventArgs.FieldList;
                fieldDisplayList = filterConsumerInitEventArgs.FieldDisplayList;
            }
            if ((fieldList != null) && (fieldList.Length > 0))
            {
                for (int i = 0; i < fieldList.Length; i++)
                {
                    this.validFilterNames.Add(new KeyValuePair<string, string>(fieldList[i], ((fieldDisplayList != null) && (fieldDisplayList.Length > i)) ? fieldDisplayList[i] : string.Empty));
                }
                ProductPage.RemoveDuplicates<KeyValuePair<string, string>>(this.validFilterNames);
            }
        }

        void IRowConsumer.RowProviderInit(object sender, RowProviderInitEventArgs e)
        {
            this.consumedRow.Clear();
        }

        void IRowConsumer.RowReady(object sender, RowReadyEventArgs e)
        {
            if ((e != null) && (e.Rows != null))
            {
                DataRow[] rows = e.Rows;
                int index = 0;
                while (index < rows.Length)
                {
                    DataRow row = rows[index];
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        if (!this.consumedRow.ContainsKey(column.ColumnName))
                        {
                            string str = base.LicEd(4) ? (row[column.ColumnName] + string.Empty) : ProductPage.GetResource("NopeEd", new object[] { string.Concat(new object[] { "{", row[column.ColumnName], string.Empty, "} via ", base["GetValuesFrom", new object[0]] }), "Ultimate" });
                            this.eventOrderLog.Add(base["LogReceived", new object[] { column.ColumnName, str }]);
                            this.consumedRow[column.ColumnName] = str;
                        }
                    }
                    break;
                }
            }
            if (this.deferredFilterAction1 != null)
            {
                if (this.CamlFilters && !this.camlFiltered)
                {
                    this.camlFiltered = true;
                    if (!this.Exed)
                    {
                        this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), true);
                        this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), true);
                    }
                }
                this.deferredFilterAction1();
                this.deferredFilterAction1 = null;
                if (this.deferredFilterAction2 != null)
                {
                    this.deferredFilterAction2();
                    this.deferredFilterAction2 = null;
                }
            }
        }

        protected virtual void OnCellConsumerInit(CellConsumerInitEventArgs e)
        {
            if (this.CellConsumerInit != null)
            {
                this.CellConsumerInit(this, e);
            }
        }

        protected virtual void OnCellProviderInit(CellProviderInitEventArgs e)
        {
            if (this.CellProviderInit != null)
            {
                this.CellProviderInit(this, e);
            }
        }

        protected virtual void OnCellReady(CellReadyEventArgs e)
        {
            if (this.CellReady != null)
            {
                if (e.Cell != null)
                {
                    this.eventOrderLog.Add(base["LogSent", new object[] { base["SendCellTo", new object[0]] }]);
                }
                this.CellReady(this, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            MenuItemTemplate gridButton;
            base.OnLoad(e);
            base.OnLoad(e);
            Action<System.Web.UI.WebControls.WebParts.WebPart> action = delegate(System.Web.UI.WebControls.WebParts.WebPart wp)
            {
                gridButton = ProductPage.FindControl(wp.Controls, "EditInGridButton") as MenuItemTemplate;
                if (gridButton != null)
                {
                    gridButton.Visible = false;
                    gridButton.Attributes["class"] = "rox-hidehidehide";
                    gridButton.Attributes["style"] = "display: none !important;";
                }
            };
            if ("xxx_disable".Equals(ProductPage.Config(ProductPage.GetContext(), "Datasheet")))
            {
                foreach (System.Web.UI.WebControls.WebParts.WebPart part in this.GetConnectedParts())
                {
                    if ((!ProductPage.Is14 || !(part is DataFormWebPart)) || (!this.CamlFilters || this.appliedParts.Contains(part)))
                    {
                        action(part);
                    }
                    else
                    {
                        this.deferredActions[part] = action;
                    }
                }
            }
        }

        protected virtual void OnRowProviderInit(RowProviderInitEventArgs e)
        {
            if (this.RowProviderInit != null)
            {
                this.RowProviderInit(this, e);
            }
        }

        protected virtual void OnRowReady(RowReadyEventArgs e)
        {
            e.SelectionStatus = "Standard";
            if (this.RowReady != null)
            {
                if ((e.Rows != null) && (e.Rows.Length > 0))
                {
                    this.eventOrderLog.Add(base["LogSent", new object[] { base["SendRowTo", new object[0]] }]);
                }
                try
                {
                    this.RowReady(this, e);
                }
                catch
                {
                }
            }
        }

        public override void PartCommunicationConnect(string interfaceName, Microsoft.SharePoint.WebPartPages.WebPart connectedPart, string connectedInterfaceName, ConnectionRunAt runAt)
        {
            Guid empty = Guid.Empty;
            this.PerformInitChecks();
            if (interfaceName == "roxorityConsumeCell")
            {
                this._cellConnected = true;
            }
            if (interfaceName == "roxorityConsumeRow")
            {
                this.isRowConsumer = true;
            }
            if (((interfaceName == "roxorityFilterProviderInterface") || (interfaceName == "roxorityProvideCell")) || (interfaceName == "roxorityProvideRow"))
            {
                this._connected = true;
                if (interfaceName == "roxorityProvideRow")
                {
                    this._rowConnected = true;
                }
                if (connectedPart == null)
                {
                    this.nullParts++;
                }
                else
                {
                    if (!this.connectedParts.Contains(connectedPart))
                    {
                        this.connectedParts.Add(connectedPart);
                        if (this.forceReload && (connectedPart.GetType().FullName == "roxority_PeopleZen.roxority_UserListWebPart"))
                        {
                            try
                            {
                                connectedPart.GetType().GetField("forceReload", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(connectedPart, true);
                            }
                            catch
                            {
                            }
                        }
                    }
                    ListViewWebPart part2 = connectedPart as ListViewWebPart;
                    if (part2 != null)
                    {
                        if (this.connectedList == null)
                        {
                            try
                            {
                                Guid guid2;
                                if (Guid.Empty.Equals(guid2 = ProductPage.GetGuid(part2.ListName)))
                                {
                                    this.connectedList = SPContext.Current.Web.Lists[part2.ListName];
                                }
                                else
                                {
                                    this.connectedList = SPContext.Current.Web.Lists[guid2];
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (!string.IsNullOrEmpty(part2.ViewGuid))
                        {
                            empty = new Guid(part2.ViewGuid);
                            if (!Guid.Empty.Equals(empty) && !this.listViews.Contains(empty))
                            {
                                this.listViews.Add(empty);
                            }
                        }
                        if (((this.connectedList != null) && (this.connectedView == null)) && !Guid.Empty.Equals(empty))
                        {
                            try
                            {
                                this.connectedView = this.connectedList.Views[empty];
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        DataFormWebPart part;
                        if (((part = connectedPart as DataFormWebPart) != null) && !this.connectedDataParts.Contains(part))
                        {
                            this.connectedDataParts.Add(part);
                        }
                    }
                }
            }
        }

        public override void PartCommunicationInit()
        {
            Predicate<FilterBase> match = null;
            CellProviderInitEventArgs e = new CellProviderInitEventArgs();
            CellConsumerInitEventArgs args2 = new CellConsumerInitEventArgs();
            SPWebPartManager webPartManager = base.WebPartManager as SPWebPartManager;
            SPContext current = null;
            if ((webPartManager != null) && (webPartManager.SPWebPartConnections != null))
            {
                foreach (SPWebPartConnection connection in ((SPWebPartManager)base.WebPartManager).SPWebPartConnections)
                {
                    if (connection.Provider == this)
                    {
                        Guid empty;
                        if (current == null)
                        {
                            try
                            {
                                current = SPContext.Current;
                            }
                            catch
                            {
                            }
                        }
                        if (!this.connectedParts.Contains(connection.Consumer))
                        {
                            this.connectedParts.Add(connection.Consumer);
                        }
                        ListViewWebPart consumer = connection.Consumer as ListViewWebPart;
                        if (consumer != null)
                        {
                            if (!Guid.Empty.Equals(empty = ProductPage.GetGuid(consumer.ViewGuid)) && !this.listViews.Contains(empty))
                            {
                                this.listViews.Add(empty);
                            }
                            if (current == null)
                            {
                                continue;
                            }
                            try
                            {
                                if (this.connectedList == null)
                                {
                                    Guid guid2;
                                    if (Guid.Empty.Equals(guid2 = ProductPage.GetGuid(consumer.ListName)))
                                    {
                                        this.connectedList = current.Web.Lists[consumer.ListName];
                                    }
                                    else
                                    {
                                        this.connectedList = current.Web.Lists[guid2];
                                    }
                                }
                            }
                            catch
                            {
                            }
                            if (((this.connectedList == null) || (this.connectedView != null)) || Guid.Empty.Equals(empty))
                            {
                                continue;
                            }
                            try
                            {
                                this.connectedView = this.connectedList.Views[empty];
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        DataFormWebPart item = connection.Consumer as DataFormWebPart;
                        if (item != null)
                        {
                            if (!this.connectedDataParts.Contains(item))
                            {
                                this.connectedDataParts.Add(item);
                            }
                            if (this.connectedList == null)
                            {
                                try
                                {
                                    this.connectedList = Reflector.Current.Get(item, "SPList", new object[0]) as SPList;
                                }
                                catch
                                {
                                }
                            }
                            empty = Guid.Empty;
                            try
                            {
                                empty = (Guid)Reflector.Current.Get(item, "ViewID", new object[0]);
                            }
                            catch
                            {
                            }
                            if (!Guid.Empty.Equals(empty))
                            {
                                if (!this.listViews.Contains(empty))
                                {
                                    this.listViews.Add(empty);
                                }
                                if ((this.connectedList != null) && (this.connectedView == null))
                                {
                                    try
                                    {
                                        this.connectedView = this.connectedList.Views[empty];
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (this._cellConnected)
            {
                args2.FieldDisplayName = args2.FieldName = this.EffectiveCellFieldName;
                this.OnCellConsumerInit(args2);
            }
            else if (this.IsConnected)
            {
                if (this._rowConnected)
                {
                    this.OnRowProviderInit(this.RowArgs);
                }
                if ((this.cellNames != null) && (this.cellNames.Length > 0))
                {
                    if (match == null)
                    {
                        match = filter => filter.Enabled && filter.Name.Equals(this.cellNames[0]);
                    }
                    if (this.GetFilters(true, false).Exists(match))
                    {
                        e.FieldDisplayName = this.cellNames[(this.cellNames.Length > 1) ? 1 : 0];
                        e.FieldName = this.cellNames[0];
                        this.OnCellProviderInit(e);
                    }
                }
            }
            if ((this.CamlFilters && !this.camlFiltered) && !this.isRowConsumer)
            {
                this.camlFiltered = true;
                if (!this.Exed)
                {
                    this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), false);
                    this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), ProductPage.Is14 && ((!string.IsNullOrEmpty(this.FolderScope) || this.RecollapseGroups) || !string.IsNullOrEmpty(this.ListViewUrl)));
                }
            }
        }

        internal void PerformInitChecks()
        {
            Hashtable listAndViewFields = new Hashtable();
            if (!this.initChecksPerformed)
            {
                this.initChecksPerformed = true;
                bool camlFilters = this.camlFilters;
                bool flag2 = false;
                if ((this.IsConnected && (this.CamlFilters || (flag2 = ((this.AutoConnect && this.IsViewPage) && (this.viewPart != null)) && base.LicEd(4)))) && !this.camlFiltered)
                {
                    try
                    {
                        this.camlFilters = true;
                        this.camlFiltered = true;
                        if (this.viewPart == null)
                        {
                            foreach (System.Web.UI.WebControls.WebParts.WebPart part3 in this.connectedParts)
                            {
                                if ((part3 is DataFormWebPart) || (part3 is ListViewWebPart))
                                {
                                    this.viewPart = part3;
                                    break;
                                }
                            }
                        }
                        ListViewWebPart viewPart = this.viewPart as ListViewWebPart;
                        if (viewPart != null)
                        {
                            this.Apply(viewPart, listAndViewFields);
                        }
                        else
                        {
                            DataFormWebPart part2;
                            if (ProductPage.Is14 && ((part2 = this.viewPart as DataFormWebPart) != null))
                            {
                                this.Apply(part2, true, listAndViewFields);
                            }
                        }
                        foreach (DictionaryEntry entry in listAndViewFields)
                        {
                            if (entry.Value is string)
                            {
                                string key = entry.Key.ToString();
                                this.validFilterNames.Add(new KeyValuePair<string, string>(key, entry.Value.ToString()));
                            }
                        }
                    }
                    finally
                    {
                        if (flag2)
                        {
                            this.camlFilters = camlFilters;
                        }
                    }
                }
            }
        }

        internal void RenderScripts(HtmlTextWriter output, string webUrl)
        {
            if (this.CanRun)
            {
                if (base.EffectiveJquery && !this.Page.Items.Contains("jquery"))
                {
                    this.Page.Items["jquery"] = new object();
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/jQuery.js?v=", ProductPage.Version, "\"></script>" }));
                }

                if (!this.Page.Items.Contains("roxority"))
                {
                    this.Page.Items["roxority"] = new object();
                    output.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", webUrl, "/_layouts/roxority_FilterZen/roxority.tl.css?v=", ProductPage.Version, "\"/>" }));
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/roxority.tl.js?v=", ProductPage.Version, "\"></script>" }));
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/json2.tl.js?v=", ProductPage.Version, "\"></script>" }));
                }
                if (!this.Context.Items.Contains("roxfzscripts"))
                {
                    string str;
                    if (string.IsNullOrEmpty(str = ProductPage.Config(ProductPage.GetContext(), "Anim").Trim()))
                    {
                        str = webUrl + "/_layouts/images/gears_an" + (ProductPage.Is14 ? "v4" : string.Empty) + ".gif";
                    }
                    this.Context.Items["roxfzscripts"] = new object();
                    if (this.HasMulti || this.HasDate)
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + webUrl + "/_layouts/datepicker.js\"></script>");
                    }
                   
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/jqms/jquery.multiSelect.js?v=", ProductPage.Version, "\"></script>" }));
                    output.Write("<script type=\"text/javascript\" language=\"JavaScript\">var roxEmpty = '" + base["Empty", new object[0]] + "', roxEmptyAll = '" + base["EmptyAll", new object[0]] + "', roxJqMs = jQuery().multiSelect, roxEmptyNone = '" + base["EmptyNone", new object[0]] + "'; </script>");
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/roxority_FilterZen.js?v=", ProductPage.Version, "\"></script>" }));
                    output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxAnim = '" + SPEncode.ScriptEncode(str) + "'; </script>");
                    if (!string.IsNullOrEmpty(str = ProductPage.Config(ProductPage.GetContext(), "AutoInfo").Trim()))
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxAcHint = '" + SPEncode.ScriptEncode(HttpUtility.HtmlEncode(str)) + "'; </script>");
                    }
                    output.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", webUrl, "/_layouts/roxority_FilterZen/jqms/jquery.multiSelect.css?v=", ProductPage.Version, "\"/>" }));
                    output.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", webUrl, "/_layouts/roxority_FilterZen/roxority_FilterZen.css?v=", ProductPage.Version, "\"/>" }));
                }
                output.WriteLine("<style type=\"text/css\">");
                if (this.SuppressSpacing)
                {
                    output.WriteLine(" div.ms-PartSpacingVertical { display: none !important; } ");
                }
                output.WriteLine(" .rox-ifilter-all-" + this.ID + " { border: " + ((this.ApplyToolbarStylings && this.SuppressSpacing) ? "0px none" : "1px solid") + " " + (this.ApplyToolbarStylings ? "#83b0ec" : "transparent") + "; } ");
                if (this.ApplyToolbarStylings)
                {
                    output.Write(" .rox-ifilter-all-" + this.ID + " { background: #d6e8ff url('" + webUrl + "/_layouts/images/toolgrad.gif') repeat-x; color: #003399; } ");
                }
                output.Write(string.Concat(new object[] { " div.rox-multibox-", this.ClientID, " input.rox-multi-text-input { width: ", this.MultiWidth - 6, "px; } " }));
                output.Write(string.Concat(new object[] { " div.rox-multibox-", this.ClientID, " span.rox-multi-check-input { width: ", this.MultiWidth, "px; } " }));
                output.Write(string.Concat(new object[] { " div.rox-multibox-", this.ClientID, " table.rox-dtpickertable input.ms-input { width: ", this.MultiWidth - 0x1a, "px; } " }));
                if (ProductPage.Is14)
                {
                    output.Write(" span.rox-multi-datepicker, span.rox-multi-userpicker { overflow: hidden; } ");
                }
                output.WriteLine("</style>");
                output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { jQuery('div#" + this.MultiPanel.ClientID + " > table').addClass('rox-dtpickertable').attr('id', function() { var tmpID; return (tmpID = jQuery(this).find('input.ms-input').attr('id')).substr(0, tmpID.lastIndexOf('_')); }); }); </script>");
                if (this.extraHide)
                {
                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxHideDatasheetRibbon = true; </script>");
                }
                if (this.ajax14hide)
                {
                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxAjax14Hide = true; </script>");
                }
                if (this.ajax14focus)
                {
                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxAjax14Focus = true; </script>");
                }
                if (this.ajax14Interval > 0)
                {
                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxAjax14Interval = " + this.ajax14Interval + "; </script>");
                }
                if (!this.Page.Items.Contains("tooltipster"))
                {
                    this.Page.Items["tooltipster"] = new object();
                    output.Write(string.Concat(new object[] { "<link rel=\"stylesheet\" type=\"text/css\" href=\"", webUrl, "/_layouts/roxority_FilterZen/jqtp/tooltipster.css\"/>" }));
                    output.Write(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"JavaScript\" src=\"", webUrl, "/_layouts/roxority_FilterZen/jqtp/jquery.tooltipster.min.js\"></script>" }));
                }
            }
        }

        protected override void RenderWebPart(HtmlTextWriter output)
        {
            Converter<KeyValuePair<string, string>, string> converter = null;
            Converter<KeyValuePair<string, string>, string> converter2 = null;
            Converter<KeyValuePair<string, FilterPair>, string> converter3 = null;
            Converter<KeyValuePair<string, string>, string> converter4 = null;
            Guid viewID;
            string format = "<span class=\"rox-ifilter-button\" style=\"white-space: nowrap !important;\">{0}</span>";
            string str2 = "?";
            string webUrl = string.Empty;
            int urlFilterCount = 0;
            int num = 0;
            int num2 = 0;
            bool flag = true;
            bool hasMultiValues = false;
            bool flag2 = false;
            bool flag3 = true;
            bool flag4 = false;
            string[] strArray = ProductPage.Config(ProductPage.GetContext(), "RemoveParams").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            List<FilterBase.Interactive> list2 = new List<FilterBase.Interactive>();
            List<int> removeUrlFields = new List<int>();
            List<string> removeUrlParams = new List<string>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, bool> transWritten = new Dictionary<string, bool>();
            Dictionary<string, int> counts = new Dictionary<string, int>();
            StringBuilder sb = new StringBuilder();
            IEnumerable<System.Web.UI.WebControls.WebParts.WebPart> connectedParts = this.GetConnectedParts();
            try
            {
                webUrl = SPContext.Current.Web.Url.TrimEnd(new char[] { '/' });
            }
            catch
            {
            }
            try
            {
                flag4 = base.DesignMode || base.WebPartManager.DisplayMode.AllowPageDesign;
            }
            catch
            {
            }
            Action<Guid> action = delegate(Guid vid)
            {
                if (((viewID = string.IsNullOrEmpty(this.Context.Request.QueryString["View"]) ? Guid.Empty : new Guid(this.Context.Request.QueryString["View"])) != Guid.Empty) && viewID.Equals(vid))
                {
                    if (!this.CamlFilters || this.DisableFilters)
                    {
                        for (int j = 1; j < 0x7fffffff; j++)
                        {
                            if (Array.IndexOf<string>(this.Context.Request.QueryString.AllKeys, "FilterField" + j) < 0)
                            {
                                break;
                            }
                            urlFilterCount = j;
                            if ((this.ActiveFilters.IndexOf(this.Context.Request.QueryString["FilterField" + j]) >= 0) && !removeUrlFields.Contains(j))
                            {
                                removeUrlFields.Add(j);
                            }
                        }
                    }
                    if ("TRUE".Equals(this.Context.Request.QueryString["Paged"], StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(this.Context.Request.Form["roxact_" + this.ID]))
                    {
                        removeUrlParams.Add("paged");
                        removeUrlParams.Add("pagedprev");
                        removeUrlParams.Add("pagefirstrow");
                        foreach (string str in this.Context.Request.QueryString.AllKeys)
                        {
                            if (!string.IsNullOrEmpty(str) && str.StartsWith("p_", StringComparison.InvariantCultureIgnoreCase))
                            {
                                removeUrlParams.Add(str.ToLowerInvariant());
                            }
                        }
                    }
                }
            };
            foreach (string str4 in strArray)
            {
                removeUrlParams.Add((str4 + string.Empty).Trim().ToLowerInvariant());
            }
            output.Write("<input type=\"hidden\" name=\"roxact_" + this.ID + "\" id=\"roxact_" + this.ID + "\" value=\"\"/><input type=\"hidden\" name=\"roxact2_" + this.ID + "\" id=\"roxact2_" + this.ID + "\" value=\"" + this.Context.Request.Form["roxact2_" + this.ID] + "\"/><span class=\"roxfilterouter\"><span class=\"roxfilterinner" + (ProductPage.Is14 ? " roxfilterinner14" : string.Empty) + "\" id=\"roxfilterinner_" + this.ID + "\">");
            this.RenderScripts(output, webUrl);
            if (!ProductPage.isEnabled)
            {
                using (SPSite site = ProductPage.GetAdminSite())
                {
                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", ProductPage.GetResource("NotEnabled", new object[] { ProductPage.MergeUrlPaths(site.Url, "/_layouts/roxority_FilterZen/default.aspx?cfg=enable"), "FilterZen" }), "servicenotinstalled.gif", "noid");
                }
                base.RenderWebPart(output);
            }
            else if (this.Exed)
            {
                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", "<a href=\"" + ProductPage.MergeUrlPaths(SPContext.Current.Web.Url, string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, ".aspx?cfg=lic&r=", new Random().Next() })) + "\">" + ProductPage.GetResource("LicExpiry", new object[0]) + "</a>", "servicenotinstalled.gif", "noid");
                base.RenderWebPart(output);
            }
            else
            {
                if (!this.CanRun)
                {
                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["NoCanRun", new object[0]], "blank.gif", "noid");
                    base.RenderWebPart(output);
                }
                else
                {
                    try
                    {
                        output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { jQuery('#WebPart" + base.Qualifier + "').css('overflow', 'visible'); }); ");
                        foreach (FilterBase base2 in this.GetFilters(true, false))
                        {
                            if ((base2 is roxority_FilterZen.FilterBase.Lookup.Multi) && base2.Enabled)
                            {
                                num2++;
                            }
                            output.Write("roxFilterNames['" + SPEncode.ScriptEncode(this.ID.ToLowerInvariant()) + "_" + SPEncode.ScriptEncode(base2.ID.ToLowerInvariant()) + "'] = '" + SPEncode.ScriptEncode(base2.Name) + "'; ");
                            output.Write("roxFilterCamlOps['" + SPEncode.ScriptEncode(this.ID.ToLowerInvariant()) + "_" + SPEncode.ScriptEncode(base2.ID.ToLowerInvariant()) + "'] = '" + SPEncode.ScriptEncode(((CamlOperator)base2.CamlOperator).ToString()) + "'; ");
                            if (base2.SendEmpty)
                            {
                                output.Write("roxFilterEmpties.push('" + SPEncode.ScriptEncode(this.ID.ToLowerInvariant()) + "_" + SPEncode.ScriptEncode(base2.ID.ToLowerInvariant()) + "'); ");
                            }
                            if (!string.IsNullOrEmpty(base2.MultiValueSeparator))
                            {
                                output.Write("roxSeps.push('" + SPEncode.ScriptEncode(base2.MultiValueSeparator) + "');");
                            }
                        }
                        output.Write(" </script>");
                        if (((this.listViews.Count > 0) || ((this.connectedView != null) && (this.connectedList != null))) || this.UrlParams)
                        {
                            bool flag5;
                            output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\">");
                            foreach (System.Web.UI.WebControls.WebParts.WebPart part2 in this.connectedParts)
                            {
                                ListViewWebPart part = part2 as ListViewWebPart;
                                if (part != null)
                                {
                                    string[] strArray8 = new string[15];
                                    strArray8[0] = "roxListViews[roxListViews.length] = { wpID: '";
                                    strArray8[1] = this.ID;
                                    strArray8[2] = "', highlight: ";
                                    strArray8[3] = this.Highlight.ToString().ToLowerInvariant();
                                    strArray8[4] = ", disableFilters: ";
                                    flag5 = !this.CamlFilters || (this.DisableFilters && this.DisableFiltersSome);
                                    strArray8[5] = flag5.ToString().ToLowerInvariant();
                                    strArray8[6] = ", embedFilters: ";
                                    strArray8[7] = this.EmbedFilters.ToString().ToLowerInvariant();
                                    strArray8[8] = ", listID: '";
                                    strArray8[9] = ProductPage.GuidBracedUpper(new Guid(part.ListName));
                                    strArray8[10] = "', viewID: '";
                                    strArray8[11] = ProductPage.GuidBracedUpper(new Guid(part.ViewGuid));
                                    strArray8[12] = "', filters: ";
                                    strArray8[13] = (this.ActiveFilters.Count == 0) ? "[]" : ("['" + string.Join("', '", this.ActiveFilters.ToArray()) + "']");
                                    strArray8[14] = " };";
                                    output.WriteLine(string.Concat(strArray8));
                                    action(ProductPage.GetGuid(part.ViewGuid));
                                }
                            }
                            if ((this.connectedView != null) && (this.connectedList != null))
                            {
                                string[] strArray3 = new string[15];
                                strArray3[0] = "roxListViews[roxListViews.length] = { wpID: '";
                                strArray3[1] = this.ID;
                                strArray3[2] = "', highlight: ";
                                strArray3[3] = this.Highlight.ToString().ToLowerInvariant();
                                strArray3[4] = ", disableFilters: ";
                                flag5 = !this.CamlFilters || (this.DisableFilters && this.DisableFiltersSome);
                                strArray3[5] = flag5.ToString().ToLowerInvariant();
                                strArray3[6] = ", embedFilters: ";
                                strArray3[7] = this.EmbedFilters.ToString().ToLowerInvariant();
                                strArray3[8] = ", listID: '";
                                strArray3[9] = ProductPage.GuidBracedUpper(this.connectedList.ID);
                                strArray3[10] = "', viewID: '";
                                strArray3[11] = ProductPage.GuidBracedUpper(this.connectedView.ID);
                                strArray3[12] = "', filters: ";
                                strArray3[13] = (this.ActiveFilters.Count == 0) ? "[]" : ("['" + string.Join("', '", this.ActiveFilters.ToArray()) + "']");
                                strArray3[14] = " };";
                                output.WriteLine(string.Concat(strArray3));
                                action(this.connectedView.ID);
                            }
                            if (((removeUrlFields.Count > 0) || (removeUrlParams.Count > 0)) || this.UrlParams)
                            {
                                foreach (string str5 in this.Context.Request.QueryString.AllKeys)
                                {
                                    if (((!string.IsNullOrEmpty(str5) && !str5.StartsWith("FilterField")) && (!str5.StartsWith("fz-") && !str5.StartsWith("FilterValue"))) && !removeUrlParams.Contains(str5.ToLowerInvariant()))
                                    {
                                        dictionary[str5] = this.Context.Request.QueryString[str5];
                                    }
                                }
                                for (int i = 1; i <= urlFilterCount; i++)
                                {
                                    if (removeUrlFields.Contains(i))
                                    {
                                        num++;
                                    }
                                    else
                                    {
                                        dictionary["FilterField" + (i - num)] = this.Context.Request.QueryString["FilterField" + i];
                                        dictionary["FilterValue" + (i - num)] = this.Context.Request.QueryString["FilterValue" + i];
                                    }
                                }
                            }
                            output.WriteLine("</script>");
                        }
                        if (flag4)
                        {
                            if (!base.EffectiveJquery)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["JqueryNone", new object[0]], "ServiceNotInstalled.gif", "noid");
                            }
                            if (this.GetGroups().Count > 1)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["SelectedGroup", new object[] { this.SelectedGroup }] + base["SelectedGroup" + (this._cellConnected ? "Conn" : "NoConn"), new object[] { string.Empty, this.ID }], "cat.gif", "noid");
                            }
                            if (this.CamlFilters)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["FilterMode", new object[0]], "ServiceInstalled.gif", "noid");
                            }
                            if (this.toolPart != null)
                            {
                                if (this.transform != null)
                                {
                                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["MultiLabel", new object[] { this.transform.Filter.Name, this.ClientID, '{', '}' }], "checkall.gif", "noid");
                                }
                                if (this._rowConnected)
                                {
                                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["RowLabel", new object[] { string.Empty, this.ClientID, '{', '}' }], "checkall.gif", "noid");
                                }
                            }
                            if (((this.nullParts > 0) || (this.connectedParts.Count > 0)) || (this.transform != null))
                            {
                                foreach (System.Web.UI.WebControls.WebParts.WebPart part3 in this.connectedParts)
                                {
                                    list.Add(new KeyValuePair<string, string>(part3.GetType().Name, part3.DisplayTitle));
                                    flag = false;
                                }
                                if (connectedParts != null)
                                {
                                    using (IEnumerator<System.Web.UI.WebControls.WebParts.WebPart> enumerator3 = connectedParts.GetEnumerator())
                                    {
                                        System.Web.UI.WebControls.WebParts.WebPart wp;
                                        while (enumerator3.MoveNext())
                                        {
                                            wp = enumerator3.Current;
                                            if (!list.Exists(test => test.Value.Equals(wp.DisplayTitle)))
                                            {
                                                flag = false;
                                                list.Add(new KeyValuePair<string, string>(wp.GetType().Name, wp.DisplayTitle));
                                            }
                                        }
                                    }
                                }
                                if (flag && ((this.nullParts > 0) || ((this.transform != null) && !this.CamlFilters)))
                                {
                                    list.Add(new KeyValuePair<string, string>(string.Empty, string.Empty));
                                }
                                ProductPage.RemoveDuplicates<string, string>(list);
                                if (converter == null)
                                {
                                    converter = delegate(KeyValuePair<string, string> pair)
                                    {
                                        if (string.IsNullOrEmpty(pair.Key) && string.IsNullOrEmpty(pair.Value))
                                        {
                                            return base["WebPartConnectedUnknown", new object[0]];
                                        }
                                        return string.Format("<i>{0}</i>: <b>{1}</b>", this.Context.Server.HtmlEncode(pair.Key), this.Context.Server.HtmlEncode(pair.Value));
                                    };
                                }
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", string.Format(base["WebPartConnected", new object[0]], ((this.transform != null) && (list.Count == 1)) ? 1 : this.ActiveFilters.Count, string.Join("</li><li>", list.ConvertAll<string>(converter).ToArray()), ((this.viewPart == null) || !ProductPage.Is14) ? string.Empty : base["AutoConnected", new object[0]]), "webpart.gif", "noid");
                            }
                            if (this.eventOrderLog.Count > 0)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["LogHtml", new object[] { string.Join("", this.eventOrderLog.ToArray()), this.ID }], "log16.gif", "noid");
                            }
                            if (this.toolPart != null)
                            {
                                if (this.validFilterNames.Count > 0)
                                {
                                    if (converter2 == null)
                                    {
                                        converter2 = delegate(KeyValuePair<string, string> pair)
                                        {
                                            IEnumerable<DataFormWebPart> enumerable;
                                            string s = pair.Key;
                                            if (s.StartsWith("@") && ((enumerable = this.GetConnectedParts<DataFormWebPart>()) != null))
                                            {
                                                foreach (DataFormWebPart part in enumerable)
                                                {
                                                    SPDataSource source;
                                                    if (((source = part.DataSource as SPDataSource) != null) && (source.List != null))
                                                    {
                                                        s = s.Substring(1);
                                                        break;
                                                    }
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(pair.Value))
                                            {
                                                return base["ValidNamesFormat", new object[] { this.Context.Server.HtmlEncode(pair.Value), this.Context.Server.HtmlEncode(s) }];
                                            }
                                            return "<b>" + s + "</b>";
                                        };
                                    }
                                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", string.Format(base["ValidNames", new object[0]], string.Join("</li><li>", this.validFilterNames.ConvertAll<string>(converter2).ToArray()), '{', '}'), "checkall.gif", "noid");
                                }
                                else
                                {
                                    output.Write("<div id=\"roxvalidnames\" style=\"display: none; background-image: none;\" class=\"rox-infobox\">" + base["ValidNamesNone", new object[0]] + "</div>");
                                }
                            }
                            if ((this.connectedDataParts.Count > 0) && ((this.connectedDataParts.Count != this.connectedDataParts.FindAll(dfwp => dfwp.GetType().FullName == xsltTypeName).Count) && !this.CamlFilters))
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["DataFormConnected", new object[0]], "itdatash.gif", "noid");
                            }
                            if (((this.nullParts > 0) && (this.nullParts != this.connectedParts.Count)) && this.CamlFilters)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["CamlDataFormConnected", new object[0]], "itdatash.gif", "noid");
                            }
                        }
                        if (this.DebugMode || flag4)
                        {
                            if ((this.debugFilters.Count > 0) || ((this.transform != null) && (this.PartFilters.Count > 0)))
                            {
                                if (converter3 == null)
                                {
                                    converter3 = delegate(KeyValuePair<string, FilterPair> pair)
                                    {
                                        int num111 = 0;
                                        counts.TryGetValue(pair.Value.Key, out num111);
                                        counts[pair.Value.Key] = ++num111;
                                        if (num111 > 1)
                                        {
                                            hasMultiValues = true;
                                        }
                                        if (((this.transform == null) || !pair.Key.Equals(this.transform.ParameterName)) && (!this.CamlFilters && !this.debugFilters.Exists(ouf => ouf.Key.Equals(pair.Key))))
                                        {
                                            return null;
                                        }
                                        if (!this.CamlFilters && ((this.transform == null) || !pair.Key.Equals(this.transform.ParameterName)))
                                        {
                                            return string.Format("{0}: <b>{1}</b>", this.Context.Server.HtmlEncode(pair.Key), this.Context.Server.HtmlEncode(pair.Value.Value));
                                        }
                                        if (transWritten.ContainsKey(pair.Key))
                                        {
                                            return null;
                                        }
                                        string str = "<b>" + this.Context.Server.HtmlEncode(pair.Key) + "</b>: <ul>";
                                        foreach (KeyValuePair<string, FilterPair> pair2 in this.PartFilters)
                                        {
                                            if (pair2.Key.Equals(pair.Key))
                                            {
                                                str = str + "<li>" + this.Context.Server.HtmlEncode(pair2.Value.Value) + "</li>";
                                            }
                                        }
                                        transWritten[pair.Key] = true;
                                        return str + "</ul>";
                                    };
                                }
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", string.Format(base["DebugOutput", new object[0]], string.Join("</li><li>", new List<string>(ProductPage.TryEach<string>(this.PartFilters.ConvertAll<string>(converter3), false, null, false)).ToArray())), "wpfilter.gif", "noid");
                            }
                            if (hasMultiValues)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["MultiFilterHint", new object[0]], "iccat.gif", "noid");
                            }
                            ProductPage.RemoveDuplicates<KeyValuePair<string, string>>(this.filtersNotSent);
                            if (this.filtersNotSent.Count > 0)
                            {
                                if (converter4 == null)
                                {
                                    converter4 = pair => string.Format("<b>{0}</b> &mdash; {2}: <i>{1}</i>", this.Context.Server.HtmlEncode(pair.Key), string.IsNullOrEmpty(base["Reason" + pair.Value, new object[0]]) ? pair.Value : this.Context.Server.HtmlEncode(base["Reason" + pair.Value, new object[0]]), base["Reason", new object[0]]);
                                }
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", string.Format(base["FiltersNotSent", new object[0]], string.Join("</li><li>", this.filtersNotSent.ConvertAll<string>(converter4).ToArray()), '{', '}'), "filteroffdisabled.gif", "noid");
                            }
                        }
                        if (this.ErrorMode || flag4)
                        {
                            if (this.regError != null)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", this.regError.Message, "exclaim.gif", "noid");
                            }
                            if (num2 > 1)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["MultiCount", new object[] { FilterBase.GetFilterTypeTitle(typeof(roxority_FilterZen.FilterBase.Lookup.Multi)) }], "exclaim.gif", "noid");
                            }
                            if (Guid.Empty.Equals(base.ConnectionID) && !this.IsConnected)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["NotConnected", new object[0]], "exclaim.gif", "noid");
                            }
                            foreach (KeyValuePair<FilterBase, Exception> pair in this.warningsErrors)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", "[" + pair.Key.ToString() + "] &mdash; " + pair.Value.Message, "servicenotinstalled.gif", "noid");
                            }
                            foreach (string str6 in this.additionalWarningsErrors)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", str6, "servicenotinstalled.gif", "noid");
                            }
                            foreach (string str7 in FilterBase.invalidTypes)
                            {
                                if (!base["CfgSettingDef_FilterTypes", new object[0]].Equals(str7, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["InvalidFilterTypeName", new object[] { str7 }], "exclaim.gif", "noid");
                                }
                            }
                            this.warningsErrors.Clear();
                            this.additionalWarningsErrors.Clear();
                        }
                        foreach (FilterBase base3 in this.GetFilters(true, true))
                        {
                            FilterBase.Interactive interactive;
                            if ((((interactive = base3 as FilterBase.Interactive) != null) && interactive.Enabled) && interactive.Get<bool>("IsInteractive"))
                            {
                                list2.Add(interactive);
                            }
                            if (base3.isEditMode)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", base["InfoFilterPreview", new object[0]], "filter.gif", "noid");
                            }
                        }
                        int num4 = 0;
                        if (list2.Count > 0)
                        {
                            output.Write(" <span class=\"rox-ifilter-all rox-ifilter-all-" + this.ID + "\"> ");
                            if ((this.HtmlMode == 1) && !string.IsNullOrEmpty(this.HtmlEmbed))
                            {
                                output.Write(string.Format(format, this.HtmlEmbed.Replace("{0}", this.ID)));
                            }
                            output.Write(" <span class=\"rox-ifilter-controls rox-ifilter-" + this.ID + "\"> ");
                            foreach (FilterBase.Interactive interactive2 in list2)
                            {
                                IEnumerable<KeyValuePair<string, string>> enumerable2;
                                if (interactive2.AutoSuggest && !this.Context.Items.Contains("roxjqas"))
                                {
                                    this.Context.Items["roxjqas"] = new object();
                                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + webUrl + "/_layouts/roxority_FilterZen/jqas/jquery.ajaxQueue.js\"></script>");
                                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + webUrl + "/_layouts/roxority_FilterZen/jqas/jquery.bgiframe.min.js\"></script>");
                                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + webUrl + "/_layouts/roxority_FilterZen/jqas/jquery.autocomplete.js\"></script>");
                                    output.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + webUrl + "/_layouts/roxority_FilterZen/jqas/jquery.autocomplete.css\"/>");
                                }
                                if (this.AutoRepost)
                                {
                                    output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxAutoReposts[roxAutoReposts.length] = 'rox_ifilter_" + interactive2.ID + "'; roxAutoReposts[roxAutoReposts.length] = 'rox_ifilter_control_" + interactive2.ID + "'; </script>");
                                }
                                sb.Remove(0, sb.Length);
                                using (StringWriter writer = new StringWriter(sb))
                                {
                                    using (HtmlTextWriter writer2 = new HtmlTextWriter(writer))
                                    {
                                        interactive2.Render(writer2, false);
                                        if (interactive2.IsRange)
                                        {
                                            writer2.Write("&nbsp;</span><span id=\"roxifiltercontrol2_" + interactive2.ID + "\">");
                                            interactive2.Render(writer2, true);
                                        }
                                        if (this.ShowClearButtons && !(interactive2 is roxority_FilterZen.FilterBase.Lookup.Multi))
                                        {
                                            writer2.Write("<a class=\"rox-filter-icon-clear\" href=\"#noop\" onclick=\"roxClearFilters('" + this.ID + "', '" + interactive2.ID + "');\"></a>");
                                        }
                                    }
                                }
                                if (sb.Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(interactive2.BeginGroup))
                                    {
                                        output.Write("<div class=\"rox-ifilter-header\">" + HttpUtility.HtmlEncode(interactive2.BeginGroup) + "</div>");
                                    }
                                    output.Write(" <span class=\"rox-ifilter" + (interactive2.IsSet ? " rox-ifilter-active" : string.Empty) + "\" id=\"rox_ifilter_" + interactive2.ID + "\" style=\"" + ((interactive2 is roxority_FilterZen.FilterBase.Lookup.Multi) ? "display: block; clear: both;" : string.Empty) + "\"> <span class=\"rox-ifilter-label rox-ifilter-label-" + this.ID + ((interactive2 is FilterBase.Date) ? " rox-ifilter-label-datetime" : string.Empty) + "\" id=\"rox_ifilter_label_" + interactive2.ID + "\" style=\"white-space: nowrap;\"> " + ((interactive2 is FilterBase.Boolean) ? string.Empty : interactive2.Get<string>("Label")) + " </span>" + (interactive2.IsRange ? ("<span class=\"rox-ifilter-label rox-ifilter-label2 rox-ifilter-label-" + this.ID + ((interactive2 is FilterBase.Date) ? " rox-ifilter-label-datetime" : string.Empty) + "\" id=\"rox_ifilter_label2_" + interactive2.ID + "\" style=\"white-space: nowrap;\"> " + ((interactive2 is FilterBase.Boolean) ? string.Empty : interactive2.Get<string>("Label2")) + " </span>") : string.Empty) + (interactive2.CheckStyle ? string.Empty : "<br/>") + "<span class=\"rox-ifilter-control " + (interactive2.IsNumeric ? "rox-ifilter-control-numeric " : string.Empty) + "rox-ifilter-control-" + this.ID + "\" id=\"rox_ifilter_control_" + interactive2.ID + "\"> <span id=\"roxifiltercontrol1_" + interactive2.ID + "\"> ");
                                    output.Write(sb.ToString());
                                    output.Write(" </span> </span> </span> ");
                                    if ((this.maxFiltersPerRow == 1) || ((this.maxFiltersPerRow > 0) && ((++num4 % this.maxFiltersPerRow) == 0)))
                                    {
                                        output.Write("<br style=\"clear: both;\"/>");
                                    }
                                }
                                flag2 |= interactive2.AllowMultiEnter;
                                if (this.UrlParams && ((enumerable2 = interactive2.FilterPairs) != null))
                                {
                                    foreach (KeyValuePair<string, string> pair2 in enumerable2)
                                    {
                                        if (!string.IsNullOrEmpty(pair2.Value) || interactive2.SendEmpty)
                                        {
                                            string introduced71 = "fz-" + pair2.Key;
                                            dictionary[introduced71] = pair2.Value;
                                        }
                                    }
                                }
                            }
                            output.Write(" </span> ");
                            if ((this.HtmlMode == 2) && !string.IsNullOrEmpty(this.HtmlEmbed))
                            {
                                output.Write(string.Format(format, this.HtmlEmbed.Replace("{0}", this.ID)));
                            }
                            output.Write(" </span> ");
                            if (flag2)
                            {
                                output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { if (roxJqMs && !jQuery().multiSelect) jQuery.extend(jQuery.fn, { multiSelect: roxJqMs }); try { jQuery('select.rox-multiselect').multiSelect({ selectAll: false, noneSelected: '', oneOrMoreSelected: '*', selectAllText: '' }, roxMultiSelect); } catch(err) { jQuery('span#roxfilterinner_" + this.ID + "').prepend('<div class=\"rox-infobox\" style=\"background-image: url(" + webUrl + "/_layouts/images/servicenotinstalled.gif);\">" + base["JqueryWarning", new object[0]].Replace("'", @"\'") + "</div> '); } }); </script>");
                            }
                        }
                        if (flag4)
                        {
                            foreach (KeyValuePair<FilterBase, Exception> pair3 in this.warningsErrors)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", "[" + pair3.Key.ToString() + "] &mdash; " + pair3.Value.Message, "exclaim.gif", "noid");
                            }
                            foreach (string str8 in this.additionalWarningsErrors)
                            {
                                output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", str8, "servicenotinstalled.gif", "noid");
                            }
                        }
                        this.warningsErrors.Clear();
                        this.additionalWarningsErrors.Clear();
                        if ("1".Equals(this.Context.Request.QueryString["FilterClear"], StringComparison.InvariantCultureIgnoreCase))
                        {
                            foreach (string str9 in this.Context.Request.QueryString.AllKeys)
                            {
                                if ((!string.IsNullOrEmpty(str9) && !"FilterClear".Equals(str9, StringComparison.InvariantCultureIgnoreCase)) && !dictionary.ContainsKey(str9))
                                {
                                    dictionary[str9] = this.Context.Request.QueryString[str9];
                                }
                            }
                        }
                        foreach (KeyValuePair<string, string> pair4 in dictionary)
                        {
                            str2 = str2 + string.Format("{0}={1}&", this.Context.Server.UrlEncode(pair4.Key), this.Context.Server.UrlEncode(pair4.Value));
                        }
                        if (flag3 = dictionary.Count == this.Context.Request.QueryString.Count)
                        {
                            foreach (KeyValuePair<string, string> pair5 in dictionary)
                            {
                                if (!(flag3 = pair5.Value.Equals(this.Context.Request.QueryString[pair5.Key])))
                                {
                                    break;
                                }
                            }
                        }
                        if (!flag3)
                        {
                            output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\"> roxNewQueryString = '{0}'; </script>", str2.Substring(0, str2.Length - 1));
                        }
                        base.RenderWebPart(output);
                    }
                    catch (Exception exception)
                    {
                        output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", exception.Message, "exclaim.gif", "noid");
                    }
                }
                output.Write("</span></span>");
            }
        }

        //public override void PartCommunicationMain()
        //{
        //    // ISSUE: object of a compiler-generated type is created
        //    // ISSUE: variable of a compiler-generated type
        //    roxority_FilterWebPart.DisplayClass5e cDisplayClass5e1 = new roxority_FilterWebPart.DisplayClass5e();

        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.ethis = this;
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.eIndex = 0;
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.filterExpression = string.Empty;
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.val = string.Empty;
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.revertToClear = false;
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.rowSent = false;
        //    XmlDocument xmlDocument = new XmlDocument();
        //    // ISSUE: reference to a compiler-generated field
        //    cDisplayClass5e1.setFilterEventArgs = new SetFilterEventArgs();
        //    RowReadyEventArgs e = new RowReadyEventArgs();
        //    if (this._cellConnected && this.IsConnected && !this.firstSkipped)
        //    {
        //        this.firstSkipped = true;
        //    }
        //    else
        //    {
        //        this.debugFilters.Clear();
        //        if (this.IsConnected)
        //        {
        //            if (this.CamlFilters && this.connectedList == null)
        //            {
        //                using (IEnumerator<ListViewWebPart> enumerator = this.GetConnectedParts<ListViewWebPart>().GetEnumerator())
        //                {
        //                    while (((IEnumerator)enumerator).MoveNext())
        //                    {
        //                        ListViewWebPart current = enumerator.Current;
        //                        try
        //                        {
        //                            List<IDisposable> list = this.disps;
        //                            SPSite site = SPContext.Current.Site;
        //                            Guid guid = Guid.Empty.Equals(current.WebId) ? SPContext.Current.Web.ID : current.WebId;
        //                            SPWeb spWeb1;
        //                            SPWeb spWeb2 = spWeb1 = site.OpenWeb(guid);
        //                            list.Add((IDisposable)spWeb1);
        //                            try
        //                            {
        //                                this.connectedList = spWeb2.Lists[current.ListId];
                                        
        //                            }
        //                            catch
        //                            {
        //                                try
        //                                {
        //                                    this.connectedList = spWeb2.Lists[current.ListName];
        //                                }
        //                                catch
        //                                {
        //                                }
        //                            }
        //                            if (this.connectedList != null)
        //                            {
        //                                xmlDocument.LoadXml(current.ListViewXml );
        //                                break;
        //                            }
        //                        }
        //                        catch
        //                        {
        //                        }
        //                    }
        //                }
        //                if (this.connectedList == null)
        //                {
        //                    using (IEnumerator<DataFormWebPart> enumerator = this.GetConnectedParts<DataFormWebPart>().GetEnumerator())
        //                    {
        //                        while (((IEnumerator)enumerator).MoveNext())
        //                        {
        //                            DataFormWebPart current = enumerator.Current;
        //                            SPDataSource spDataSource;
        //                            if ((spDataSource = current.DataSource as SPDataSource) != null && spDataSource.List != null)
        //                            {
        //                                this.connectedList = spDataSource.List;
        //                                xmlDocument.LoadXml(string.IsNullOrEmpty((this).Context.Application["orig_" + (current).ID] as string) ? spDataSource.SelectCommand : (this).Context.Application["orig_" + (current).ID] as string);
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                Guid guid1;
        //                if (this.connectedList != null && xmlDocument.DocumentElement != null && xmlDocument.DocumentElement.Attributes.GetNamedItem("Name") != null && !Guid.Empty.Equals(guid1 = ProductPage.GetGuid(xmlDocument.DocumentElement.Attributes.GetNamedItem("Name").Value)))
        //                {
        //                    foreach (SPView spView in (IEnumerable)this.connectedList.Views)
        //                    {
        //                        if (spView.ID.Equals(guid1))
        //                        {
        //                            this.connectedView = spView;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            if (this.cellNames != null && this.cellNames.Length > 0)
        //            {
        //                roxority_FilterWebPart roxorityFilterWebPart = this;
        //                List<FilterBase> filters = this.GetFilters(true, true);
        //                Predicate<FilterBase> match = (Predicate<FilterBase>)(filter =>
        //                {
        //                    if (filter.Enabled)
        //                        return filter.Name.Equals(this.cellNames[0]);
        //                    else
        //                        return false;
        //                });
        //                FilterBase filterBase1;
        //                FilterBase filterBase2 = filterBase1 = filters.Find(match);
        //                roxorityFilterWebPart.cellFilter = filterBase1;
        //                if (filterBase2 != null)
        //                    this.cellArgs = new CellReadyEventArgs();
        //            }
        //            if (this.rowTable != null)
        //                this.rowTable.Dispose();
        //            this.rowTable = new DataTable();
        //            if (!this.CamlFilters && !this.isRowConsumer)
        //            {
        //                foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> keyValuePair in this.PartFilters)
        //                {
        //                    this.debugFilters.Add(new KeyValuePair<string, string>(keyValuePair.Key, keyValuePair.Value.Value));
        //                    // ISSUE: reference to a compiler-generated field
        //                    if ((cDisplayClass5e1.val = this.GetFilterValue(keyValuePair.Value.Value)).IndexOf('&') >= 0 && (this.connectedList != null || this.connectedDataParts.Count > 0))
        //                    {
        //                        // ISSUE: reference to a compiler-generated field
        //                        this.additionalWarningsErrors.Add(this["Ampersand", new object[2]
        //        {
        //          (object) keyValuePair.Value.Key,
        //          (object) cDisplayClass5e1.val
        //        }]);
        //                        if (!ProductPage.Is14)
        //                        {
        //                            // ISSUE: reference to a compiler-generated field
        //                            // ISSUE: reference to a compiler-generated field
        //                            cDisplayClass5e1.val = cDisplayClass5e1.val.Replace("&", string.Empty);
        //                            this.additionalWarningsErrors.Add(this["Ampersand12", new object[0]]);
        //                        }
        //                    }
        //                    // ISSUE: variable of a compiler-generated type
        //                    roxority_FilterWebPart.DisplayClass5e cDisplayClass5e2 = cDisplayClass5e1;
        //                    // ISSUE: reference to a compiler-generated field
        //                    // ISSUE: reference to a compiler-generated field
        //                    // ISSUE: reference to a compiler-generated field
        //                    string str = cDisplayClass5e2.filterExpression + string.Format("FilterField{2}={0}&FilterValue{2}={1}&", (object)keyValuePair.Key, (object)cDisplayClass5e1.val, (object)++cDisplayClass5e1.eIndex);
        //                    // ISSUE: reference to a compiler-generated field
        //                    cDisplayClass5e2.filterExpression = str;
        //                }
        //            }
        //            if (this._rowConnected && this.RowArgs.FieldList.Length > 0)
        //            {
        //                foreach (string str in this.RowArgs.FieldList)
        //                {
        //                    if (!this.rowTable.Columns.Contains(str))
        //                        this.rowTable.Columns.Add(str);
        //                }
        //                RowReadyEventArgs rowReadyEventArgs = e;
        //                DataRow[] dataRowArray;
        //                if (!this.Exed)
        //                    dataRowArray = new DataRow[1]
        //      {
        //        this.rowTable.Rows.Add(new List<string>((IEnumerable<string>) this.RowArgs.FieldList).ConvertAll<object>((Converter<string, object>) (fieldName =>
        //        {
        //          KeyValuePair<string, roxority_FilterWebPart.FilterPair> local_0 = this.PartFilters.Find((Predicate<KeyValuePair<string, roxority_FilterWebPart.FilterPair>>) (keyValuePair =>
        //          {
        //            if (fieldName.Equals(keyValuePair.Key))
        //              return true;
        //            if (fieldName.StartsWith("@"))
        //              return fieldName.Substring(1).Equals(keyValuePair.Key);
        //            else
        //              return false;
        //          }));
        //          if (local_0.Value != null)
        //            return (object) local_0.Value.Value;
        //          else
        //            return (object) string.Empty;
        //        })).ToArray())
        //      };
        //                else
        //                    dataRowArray = new DataRow[0];
        //                rowReadyEventArgs.Rows=dataRowArray;
        //                this.OnRowReady(e);
        //                // ISSUE: reference to a compiler-generated field
        //                cDisplayClass5e1.rowSent = true;
        //            }
        //            // ISSUE: reference to a compiler-generated method
        //            this.deferredFilterAction1 = new roxority.Shared.Action(cDisplayClass5e1.PartCommunicationMain_58);
        //            if (!this.isRowConsumer)
        //            {
        //                this.deferredFilterAction1();
        //                this.deferredFilterAction1 = (roxority.Shared.Action)null;
        //            }
        //        }
        //        if (!this.CamlFilters)
        //        {
        //            // ISSUE: reference to a compiler-generated method
        //            this.deferredFilterAction2 = new roxority.Shared.Action(cDisplayClass5e1.PartCommunicationMain_59);
        //            if (this.isRowConsumer)
        //                return;
        //            this.deferredFilterAction2();
        //            this.deferredFilterAction2 = (roxority.Shared.Action)null;
        //        }
        //        else if (!this.camlFiltered && !this.isRowConsumer)
        //        {
        //            this.camlFiltered = true;
        //            if (this.Exed)
        //                return;
        //            this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), false);
        //            this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), false);
        //        }
        //        else
        //        {
        //            if (!this.CamlFilters || this.camlFiltered || this.consumedRow.Count <= 0)
        //                return;
        //            this.camlFiltered = true;
        //            if (!this.Exed)
        //            {
        //                this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), true);
        //                this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), true);
        //            }
        //            if (this.deferredFilterAction1 != null)
        //            {
        //                this.deferredFilterAction1();
        //                this.deferredFilterAction1 = (roxority.Shared.Action)null;
        //            }
        //            if (this.deferredFilterAction2 == null)
        //                return;
        //            this.deferredFilterAction2();
        //            this.deferredFilterAction2 = (roxority.Shared.Action)null;
        //        }
        //    }
        //}

        public override void PartCommunicationMain()
        {
            roxority_FilterWebPart.DisplayClass5e displayClass5e = new roxority_FilterWebPart.DisplayClass5e();
            displayClass5e.ethis = this;
            displayClass5e.eIndex = 0;
            displayClass5e.filterExpression = string.Empty;
            displayClass5e.val = string.Empty;
            displayClass5e.revertToClear = false;
            displayClass5e.rowSent = false;
            XmlDocument xmlDocument = new XmlDocument();
            displayClass5e.setFilterEventArgs = new SetFilterEventArgs();
            RowReadyEventArgs rowReadyEventArgs = new RowReadyEventArgs();
            if (this._cellConnected && this.IsConnected && !this.firstSkipped)
            {
                this.firstSkipped = true;
                return;
            }
            this.debugFilters.Clear();
            if (this.IsConnected)
            {
                if (this.CamlFilters && this.connectedList == null)
                {
                    foreach (ListViewWebPart current in this.GetConnectedParts<ListViewWebPart>())
                    {
                        try
                        {
                            SPWeb sPWeb;
                            this.disps.Add(sPWeb = SPContext.Current.Site.OpenWeb(Guid.Empty.Equals(current.WebId) ? SPContext.Current.Web.ID : current.WebId));
                            try
                            {
                                this.connectedList = sPWeb.Lists[new Guid(current.ListName)];
                            }
                            catch
                            {
                                try
                                {
                                    this.connectedList = sPWeb.Lists[current.ListName];
                                }
                                catch
                                {
                                }
                            }
                            if (this.connectedList != null)
                            {
                                xmlDocument.LoadXml(current.ListViewXml);
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (this.connectedList == null)
                    {
                        foreach (DataFormWebPart current2 in this.GetConnectedParts<DataFormWebPart>())
                        {
                            SPDataSource sPDataSource;
                            if ((sPDataSource = (current2.DataSource as SPDataSource)) != null && sPDataSource.List != null)
                            {
                                this.connectedList = sPDataSource.List;
                                xmlDocument.LoadXml(string.IsNullOrEmpty(this.Context.Application["orig_" + current2.ID] as string) ? sPDataSource.SelectCommand : (this.Context.Application["orig_" + current2.ID] as string));
                                break;
                            }
                        }
                    }
                    Guid guid;
                    if (this.connectedList != null && xmlDocument.DocumentElement != null && xmlDocument.DocumentElement.Attributes.GetNamedItem("Name") != null && !Guid.Empty.Equals(guid = ProductPage.GetGuid(xmlDocument.DocumentElement.Attributes.GetNamedItem("Name").Value)))
                    {
                        foreach (SPView sPView in this.connectedList.Views)
                        {
                            if (sPView.ID.Equals(guid))
                            {
                                this.connectedView = sPView;
                                break;
                            }
                        }
                    }
                }
                if (this.cellNames != null && this.cellNames.Length > 0)
                {
                    if ((this.cellFilter = this.GetFilters(true, true).Find((FilterBase filter) => filter.Enabled && filter.Name.Equals(this.cellNames[0]))) != null)
                    {
                        this.cellArgs = new CellReadyEventArgs();
                    }
                }
                if (this.rowTable != null)
                {
                    this.rowTable.Dispose();
                }
                this.rowTable = new DataTable();
                if (!this.CamlFilters && !this.isRowConsumer)
                {
                    foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> current3 in this.PartFilters)
                    {
                        this.debugFilters.Add(new KeyValuePair<string, string>(current3.Key, current3.Value.Value));
                        if ((displayClass5e.val = this.GetFilterValue(current3.Value.Value)).IndexOf('&') >= 0 && (this.connectedList != null || this.connectedDataParts.Count > 0))
                        {
                            this.additionalWarningsErrors.Add(Convert.ToString(base["Ampersand", new object[]
							{
								current3.Value.Key,
								displayClass5e.val
							}]));
                            if (!ProductPage.Is14)
                            {
                                displayClass5e.val = displayClass5e.val.Replace("&", string.Empty);
                                this.additionalWarningsErrors.Add(base["Ampersand12", new object[0]]);
                            }
                        }
                        roxority_FilterWebPart.DisplayClass5e expr_490 = displayClass5e;
                        expr_490.filterExpression += string.Format("FilterField{2}={0}&FilterValue{2}={1}&", current3.Key, displayClass5e.val, ++displayClass5e.eIndex);
                    }
                }
                if (this._rowConnected && this.RowArgs.FieldList.Length > 0)
                {
                    string[] fieldList = this.RowArgs.FieldList;
                    for (int i = 0; i < fieldList.Length; i++)
                    {
                        string text = fieldList[i];
                        if (!this.rowTable.Columns.Contains(text))
                        {
                            this.rowTable.Columns.Add(text);
                        }
                    }
                    RowReadyEventArgs arg_5B6_0 = rowReadyEventArgs;
                    DataRow[] arg_5B6_1;
                    if (!this.Exed)
                    {
                        DataRow[] array = new DataRow[1];
                        array[0] = this.rowTable.Rows.Add(new List<string>(this.RowArgs.FieldList).ConvertAll<object>(delegate(string fieldName)
                        {
                            KeyValuePair<string, roxority_FilterWebPart.FilterPair> keyValuePair2 = this.PartFilters.Find((KeyValuePair<string, roxority_FilterWebPart.FilterPair> keyValuePair) => fieldName.Equals(keyValuePair.Key) || (fieldName.StartsWith("@") && fieldName.Substring(1).Equals(keyValuePair.Key)));
                            if (keyValuePair2.Value != null)
                            {
                                return keyValuePair2.Value.Value;
                            }
                            return string.Empty;
                        }).ToArray());
                        arg_5B6_1 = array;
                    }
                    else
                    {
                        arg_5B6_1 = new DataRow[0];
                    }
                    arg_5B6_0.Rows = (arg_5B6_1);
                    this.OnRowReady(rowReadyEventArgs);
                    displayClass5e.rowSent = true;
                }
                this.deferredFilterAction1 = delegate
                {
                    if (displayClass5e.ethis.isRowConsumer)
                    {
                        foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> current4 in displayClass5e.ethis.PartFilters)
                        {
                            displayClass5e.ethis.debugFilters.Add(new KeyValuePair<string, string>(current4.Key, current4.Value.Value));
                            if ((displayClass5e.val = displayClass5e.ethis.GetFilterValue(current4.Value.Value)).IndexOf('&') >= 0 && (displayClass5e.ethis.connectedList != null || displayClass5e.ethis.connectedDataParts.Count > 0))
                            {
                                displayClass5e.ethis.additionalWarningsErrors.Add(displayClass5e.ethis["Ampersand", new object[]
								{
									current4.Value.Key,
									displayClass5e.val
								}]);
                                if (!ProductPage.Is14)
                                {
                                    displayClass5e.val = displayClass5e.val.Replace("&", string.Empty);
                                    displayClass5e.ethis.additionalWarningsErrors.Add(displayClass5e.ethis["Ampersand12", new object[0]]);
                                }
                            }
                            displayClass5e.filterExpression += string.Format("FilterField{2}={0}&FilterValue{2}={1}&", current4.Key, displayClass5e.val, ++displayClass5e.eIndex);
                        }
                    }
                    if (displayClass5e.filterExpression.Length != 0)
                    {
                        displayClass5e.filterExpression = displayClass5e.filterExpression.Substring(0, displayClass5e.filterExpression.Length - 1);
                    }
                    else
                    {
                        displayClass5e.revertToClear = true;
                    }
                    if (displayClass5e.ethis.SetFilter != null)
                    {
                        displayClass5e.setFilterEventArgs.FilterExpression = ((displayClass5e.ethis.CamlFilters || displayClass5e.ethis.Exed) ? "" : displayClass5e.filterExpression);
                        try
                        {
                            displayClass5e.ethis.SetFilter.Invoke(displayClass5e.ethis, displayClass5e.setFilterEventArgs);
                            if (!displayClass5e.ethis.CamlFilters)
                            {
                                displayClass5e.ethis.eventOrderLog.Add(displayClass5e.ethis["LogSent", new object[]
								{
									displayClass5e.ethis["SendUrlFilterTo", new object[0]]
								}]);
                            }
                            return;
                        }
                        catch
                        {
                            return;
                        }
                    }
                    displayClass5e.revertToClear = true;
                };
                if (!this.isRowConsumer)
                {
                    this.deferredFilterAction1();
                    this.deferredFilterAction1 = null;
                }
            }
            if (!this.CamlFilters)
            {
                this.deferredFilterAction2 = delegate
                {
                    if (!displayClass5e.ethis.IsConnected || displayClass5e.revertToClear)
                    {
                        if (displayClass5e.ethis.IsConnected && displayClass5e.revertToClear && displayClass5e.ethis.debugFilters.Count > 0 && !displayClass5e.ethis._rowConnected && displayClass5e.rowSent)
                        {
                            displayClass5e.ethis.debugFilters.Clear();
                        }
                        if (displayClass5e.ethis.ClearFilter != null)
                        {
                            displayClass5e.ethis.ClearFilter.Invoke(displayClass5e.ethis, EventArgs.Empty);
                        }
                        else
                        {
                            if (displayClass5e.ethis.NoFilter != null)
                            {
                                displayClass5e.ethis.NoFilter.Invoke(displayClass5e.ethis, EventArgs.Empty);
                            }
                        }
                        using (List<DataFormWebPart>.Enumerator enumerator5 = displayClass5e.ethis.connectedDataParts.GetEnumerator())
                        {
                            while (enumerator5.MoveNext())
                            {
                                DataFormWebPart current4 = enumerator5.Current;
                                if (!roxority_FilterWebPart.xsltTypeName.Equals(current4.GetType().FullName))
                                {
                                    current4.FilterValues.Collection.Clear();
                                    current4.DataBind();
                                }
                            }
                            return;
                        }
                    }
                    if (!displayClass5e.ethis.Exed)
                    {
                        foreach (DataFormWebPart current5 in displayClass5e.ethis.connectedDataParts)
                        {
                            if (!roxority_FilterWebPart.xsltTypeName.Equals(current5.GetType().FullName))
                            {
                                foreach (KeyValuePair<string, string> current6 in displayClass5e.ethis.debugFilters)
                                {
                                    current5.FilterValues.Set(current6.Key, displayClass5e.ethis.GetFilterValue(current6.Value));
                                }
                                current5.DataBind();
                            }
                        }
                    }
                };
                if (!this.isRowConsumer)
                {
                    this.deferredFilterAction2();
                    this.deferredFilterAction2 = null;
                    return;
                }
            }
            else
            {
                if (!this.camlFiltered && !this.isRowConsumer)
                {
                    this.camlFiltered = true;
                    if (!this.Exed)
                    {
                        this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), false);
                        this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), false);
                        return;
                    }
                }
                else
                {
                    if (this.CamlFilters && !this.camlFiltered && this.consumedRow.Count > 0)
                    {
                        this.camlFiltered = true;
                        if (!this.Exed)
                        {
                            this.Apply<ListViewWebPart>(this.GetConnectedParts<ListViewWebPart>(), true);
                            this.Apply<DataFormWebPart>(this.GetConnectedParts<DataFormWebPart>(), true);
                        }
                        if (this.deferredFilterAction1 != null)
                        {
                            this.deferredFilterAction1();
                            this.deferredFilterAction1 = null;
                        }
                        if (this.deferredFilterAction2 != null)
                        {
                            this.deferredFilterAction2();
                            this.deferredFilterAction2 = null;
                        }
                    }
                }
            }
        }

        [ConnectionProvider("Transformed", "roxorityTransformedFilterProviderInterface", typeof(Transform.Provider), AllowsMultipleConnections = true)]
        public ITransformableFilterValues SetConnectionInterface()
        {
            if (this.transform == null)
            {
                foreach (FilterBase base2 in this.GetFilters(true, false))
                {
                    if (((base2.Enabled && base2.SupportsMultipleValues) && !(base2 is roxority_FilterZen.FilterBase.Lookup.Multi)) && (string.IsNullOrEmpty(this.MultiValueFilterID) || base2.ID.Equals(this.MultiValueFilterID)))
                    {
                        return (this.transform = new Transform(this, base2));
                    }
                }
            }
            return this.transform;
        }

        public void SetConsumerSchema(PropertyDescriptorCollection schema)
        {
            if ((schema != null) && (schema.Count == 0))
            {
                foreach (PropertyDescriptor descriptor in this.Schema)
                {
                    schema.Add(descriptor);
                }
            }
        }

        internal bool ValidateJsonFilters(Hashtable ht, ArrayList flist, List<string> messages)
        {
            Predicate<FilterBase> match = null;
            string val;
            if (ht.Count > 1)
            {
                messages.Add(base["JsonOne", new object[0]]);
            }
            else
            {
                foreach (DictionaryEntry entry in ht)
                {
                    string str;
                    ArrayList list;
                    if (((str = entry.Key as string) == null) || (!"OR".Equals(str) && !"AND".Equals(str)))
                    {
                        messages.Add(base["JsonOperator", new object[] { entry.Key }]);
                    }
                    if (((list = entry.Value as ArrayList) == null) || (list.Count != 2))
                    {
                        messages.Add(base["JsonOperands", new object[] { str }]);
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Hashtable hashtable;
                            if (((hashtable = list[i] as Hashtable) == null) & ((val = list[i] as string) == null))
                            {
                                messages.Add(base["JsonOperandTypes", new object[] { str }]);
                                continue;
                            }
                            if (hashtable != null)
                            {
                                this.ValidateJsonFilters(hashtable, flist, messages);
                                continue;
                            }
                            if ((val != null) && (val != "*"))
                            {
                                bool flag;
                                if (match == null)
                                {
                                    match = test => val.Equals(test.Name);
                                }
                                if (!this.FiltersList.Exists(match) && !(flag = (this.connectedList != null) && (ProductPage.GetField(this.connectedList, val) != null)))
                                {
                                    foreach (Hashtable hashtable2 in flist)
                                    {
                                        if (val.Equals(hashtable2["k"] + string.Empty, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        messages.Add(base["JsonUnknown", new object[0]] + val);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (messages.Count == 0);
        }

        [Personalizable]
        public string AcSecFields
        {
            get
            {
                if (this.acSecFields != null)
                {
                    return string.Join("\r\n", this.acSecFields).Trim();
                }
                return string.Empty;
            }
            set
            {
                this.acSecFields = (value + string.Empty).Trim().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public bool Activated
        {
            get
            {
                return !string.IsNullOrEmpty(this.Context.Request.Form["roxact2_" + this.ID]);
            }
        }

        internal List<string> ActiveFilters
        {
            get
            {
                List<string> list = new List<string>();
                foreach (FilterBase base2 in this.GetFilters(true, true))
                {
                    if (base2.Enabled)
                    {
                        list.Add(base2.Name);
                    }
                }
                return list;
            }
        }

        [Personalizable]
        public bool Ajax14focus
        {
            get
            {
                return this.ajax14focus;
            }
            set
            {
                this.ajax14focus = value;
            }
        }

        [Personalizable]
        public bool Ajax14hide
        {
            get
            {
                return this.ajax14hide;
            }
            set
            {
                this.ajax14hide = value;
            }
        }

        [Personalizable]
        public int Ajax14Interval
        {
            get
            {
                return this.ajax14Interval;
            }
            set
            {
                this.ajax14Interval = value;
            }
        }

        [Personalizable]
        public bool ApplyToolbarStylings
        {
            get
            {
                return base.GetProp<bool>("ApplyToolbarStylings", this.applyToolbarStylings);
            }
            set
            {
                this.applyToolbarStylings = value;
            }
        }

        [Personalizable]
        public bool AutoConnect
        {
            get
            {
                return this.autoConnect;
            }
            set
            {
                this.autoConnect = value;
            }
        }

        [Personalizable]
        public bool AutoRepost
        {
            get
            {
                if (!base.GetProp<bool>("AutoRepost", this.autoRepost))
                {
                    return this.EmbedFilters;
                }
                return true;
            }
            set
            {
                this.autoRepost = value;
            }
        }

        public static System.Type BdcClientUtilType
        {
            get
            {
                if (bdcClientUtilType == null)
                {
                    try
                    {
                        bdcClientUtilType = System.Type.GetType((ProductPage.Is14 ? "Microsoft.SharePoint.BdcClientUtil, Microsoft.SharePoint, Version=14.0.0.0" : "Microsoft.SharePoint.Portal.WebControls.BdcClientUtil, Microsoft.SharePoint.Portal, Version=12.0.0.0") + ", Culture=neutral, PublicKeyToken=71e9bce111e9429c", true, true);
                    }
                    catch
                    {
                    }
                }
                return bdcClientUtilType;
            }
        }

        public static MethodInfo BdcFilterApplyMethod
        {
            get
            {
                if ((bdcFilterApplyMethod == null) && (BdcFilterType != null))
                {
                    bdcFilterApplyMethod = bdcFilterType.GetMethod("Apply", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly, null, new System.Type[] { typeof(roxority_FilterWebPart), typeof(DataFormWebPart) }, null);
                }
                return bdcFilterApplyMethod;
            }
        }

        public static MethodInfo BdcFilterCanApplyMethod
        {
            get
            {
                if ((bdcFilterCanApplyMethod == null) && (BdcFilterType != null))
                {
                    bdcFilterCanApplyMethod = bdcFilterType.GetMethod("CanApply", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly, null, new System.Type[] { typeof(roxority_FilterWebPart), typeof(DataFormWebPart) }, null);
                }
                return bdcFilterCanApplyMethod;
            }
        }

        public static System.Type BdcFilterType
        {
            get
            {
                if (bdcFilterType == null)
                {
                    try
                    {
                        bdcFilterType = System.Type.GetType("roxority_FilterZen.ServerExtensions.BdcFilter, roxority_FilterZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=68349fdcd3484f01", false, true);
                    }
                    catch
                    {
                    }
                }
                return bdcFilterType;
            }
        }

        [Personalizable]
        public bool CamlFilters
        {
            get
            {
                return (base.LicEd(4) && this.camlFilters);
            }
            set
            {
                this.camlFilters = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public string CamlFiltersAndCombined
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return string.Empty;
                }
                return this.camlFiltersAndCombined;
            }
            set
            {
                this.camlFiltersAndCombined = ProductPage.Trim(value, new char[0]);
            }
        }

        public string[] CamlSourceFilters
        {
            get
            {
                List<string> list = new List<string>();
                foreach (FilterBase base2 in this.GetFilters(false, false, true))
                {
                    if (base2.Enabled && (base2 is FilterBase.CamlSource))
                    {
                        list.Add(base2.Name);
                    }
                }
                return list.ToArray();
            }
        }

        protected internal override bool CanRun
        {
            get
            {
                return ((base.CanRun && !base.IsFrontPage) && !base.IsPreview);
            }
        }

        [Personalizable]
        public bool Cascaded
        {
            get
            {
                if (!base.LicEd(4) || !base.GetProp<bool>("Cascade", this.cascaded))
                {
                    return false;
                }
                if (!this.Activated)
                {
                    return !this.SearchBehaviour;
                }
                return true;
            }
            set
            {
                this.cascaded = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public string CellFieldName
        {
            get
            {
                return this.cellFieldName;
            }
            set
            {
                this.cellFieldName = string.IsNullOrEmpty(value = ProductPage.Trim(value, new char[0])) ? "FilterGroup" : value;
            }
        }

        [Personalizable]
        public string DataPartIDsString
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        [Personalizable]
        public bool DebugMode
        {
            get
            {
                return base.GetProp<bool>("DebugMode", this.debugMode);
            }
            set
            {
                this.debugMode = value;
            }
        }

        [Personalizable]
        public bool DefaultToOr
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("DefaultToOr", this.defaultToOr);
                }
                return false;
            }
            set
            {
                this.defaultToOr = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public bool DisableFilters
        {
            get
            {
                return base.GetProp<bool>("DisableFilters", this.disableFilters);
            }
            set
            {
                this.disableFilters = value;
            }
        }

        [Personalizable]
        public bool DisableFiltersSome
        {
            get
            {
                return base.GetProp<bool>("DisableFiltersSome", this.disableFiltersSome);
            }
            set
            {
                this.disableFiltersSome = value;
            }
        }

        [Personalizable]
        public int DynamicInteractiveFilters
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public List<string> EffectiveAndFilters
        {
            get
            {
                List<string> list = new List<string>();
                list.AddRange(this.CamlFiltersAndCombined.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                foreach (FilterBase base2 in this.GetFilters(false, false, false))
                {
                    FilterBase.Interactive interactive;
                    if ((((interactive = base2 as FilterBase.Interactive) != null) && interactive.IsRange) && !list.Contains(interactive.Name))
                    {
                        list.Add(interactive.Name);
                    }
                }
                return list;
            }
        }

        public string EffectiveCellFieldName
        {
            get
            {
                if (((this.cell != null) && (this.cell.Length != 0)) && !string.IsNullOrEmpty(this.cell[0]))
                {
                    return this.cell[0];
                }
                return this.CellFieldName;
            }
        }

        [Personalizable]
        public bool EmbedFilters
        {
            get
            {
                return base.GetProp<bool>("EmbedFilters", this.embedFilters);
            }
            set
            {
                this.embedFilters = value;
            }
        }

        [Personalizable]
        public bool ErrorMode
        {
            get
            {
                return base.GetProp<bool>("ErrorMode", this.errorMode);
            }
            set
            {
                this.errorMode = value;
            }
        }

        [XmlIgnore, Personalizable(false)]
        protected internal List<FilterBase> FiltersList
        {
            get
            {
                if (this.filters == null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(this.serializedFilters))
                        {
                            this.filters = FilterBase.Deserialize(this, this.serializedFilters);
                        }
                    }
                    catch
                    {
                    }
                    if (this.filters == null)
                    {
                        this.serializedFilters = FilterBase.Serialize(this.filters = new List<FilterBase>());
                    }
                }
                return this.filters;
            }
            set
            {
                this.filters = value;
                if (this.filters == null)
                {
                    this.filters = new List<FilterBase>();
                }
                this.serializedFilters = FilterBase.Serialize(this.filters);
            }
        }

        [Personalizable]
        public string FolderScope
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return string.Empty;
                }
                return this.folderScope;
            }
            set
            {
                this.folderScope = base.LicEd(4) ? value : string.Empty;
            }
        }

        public string GeneratedQuery
        {
            get
            {
                return this.generatedQuery;
            }
            set
            {
                string str = HttpUtility.HtmlDecode(this.generatedQuery = value + string.Empty);
                if ((this.Context != null) && !string.IsNullOrEmpty(str))
                {
                    this.Context.Items["roxFiltCaml"] = str;
                }
                if ((this.Page != null) && !string.IsNullOrEmpty(str))
                {
                    this.Page.Items["roxFiltCaml"] = str;
                }
            }
        }

        [Personalizable]
        public string Groups
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return string.Empty;
                }
                return string.Join("\r\n", this.groups.ConvertAll<string>(v => v.Replace(sep, ",")).ToArray());
            }
            set
            {
                string[] strArray = base.LicEd(4) ? ProductPage.Trim(value, new char[0]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
                this.groups.Clear();
                foreach (string str2 in strArray)
                {
                    string str;
                    if (!string.IsNullOrEmpty(str = str2.Trim().Replace(",", sep)) && !this.groups.Contains(str))
                    {
                        this.groups.Add(str);
                    }
                }
            }
        }

        public bool HasDate
        {
            get
            {
                List<FilterBase> list;
                bool flag = false;
                if ((!this.hasDate.HasValue || !this.hasDate.HasValue) && ((list = this.GetFilters(false, false, true)) != null))
                {
                    foreach (FilterBase base2 in list)
                    {
                        if (flag = (base2 is FilterBase.Date) && base2.Enabled)
                        {
                            break;
                        }
                    }
                    this.hasDate = new bool?(flag);
                }
                if (this.hasDate.HasValue && this.hasDate.HasValue)
                {
                    return this.hasDate.Value;
                }
                return false;
            }
        }

        public bool HasHiddenFilter
        {
            get
            {
                foreach (FilterBase base2 in this.GetFilters(false, false, true))
                {
                    if (base2.Enabled && (((base2 is roxority_FilterZen.FilterBase.Lookup.Multi) || !(base2 is FilterBase.Interactive)) || !((FilterBase.Interactive)base2).IsInteractive))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasMulti
        {
            get
            {
                List<FilterBase> list;
                bool flag = false;
                if ((!this.hasMulti.HasValue || !this.hasMulti.HasValue) && ((list = this.GetFilters(false, false, true)) != null))
                {
                    foreach (FilterBase base2 in list)
                    {
                        if (flag = (base2 is roxority_FilterZen.FilterBase.Lookup.Multi) && base2.Enabled)
                        {
                            break;
                        }
                    }
                    this.hasMulti = new bool?(flag);
                }
                if (this.hasMulti.HasValue && this.hasMulti.HasValue)
                {
                    return this.hasMulti.Value;
                }
                return false;
            }
        }

        public bool HasPeople
        {
            get
            {
                bool flag = false;
                WebPartCollection collection = null;
                if (!this.hasPeople.HasValue || !this.hasPeople.HasValue)
                {
                    try
                    {
                        if (base.WebPartManager != null)
                        {
                            collection = base.WebPartManager.WebParts;
                        }
                        if ((collection != null) && (collection.Count == 0))
                        {
                            collection = null;
                        }
                    }
                    catch
                    {
                    }
                    if (collection != null)
                    {
                        foreach (System.Web.UI.WebControls.WebParts.WebPart part in ProductPage.TryEach<System.Web.UI.WebControls.WebParts.WebPart>(collection))
                        {
                            if ((part != null) && (flag = part.GetType().FullName == "roxority_PeopleZen.roxority_UserListWebPart"))
                            {
                                break;
                            }
                        }
                        this.hasPeople = new bool?(flag);
                    }
                }
                if (this.hasPeople.HasValue && this.hasPeople.HasValue)
                {
                    return this.hasPeople.Value;
                }
                return false;
            }
        }

        [Personalizable]
        public bool Highlight
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("Highlight", this.highlight);
                }
                return false;
            }
            set
            {
                this.highlight = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public string HtmlEmbed
        {
            get
            {
                if (this.htmlEmbed == null)
                {
                    this.htmlEmbed = base["HtmlTemp1", new object[0]];
                }
                return this.htmlEmbed;
            }
            set
            {
                this.htmlEmbed = (value == null) ? string.Empty : value.Trim();
            }
        }

        [Personalizable]
        public int HtmlMode
        {
            get
            {
                return base.GetProp<int>("HtmlMode", this.htmlMode);
            }
            set
            {
                this.htmlMode = value;
            }
        }

        public override bool IsConnected
        {
            get
            {
                if (((!this._connected && (this.connectedParts.Count == 0)) && (this.AutoConnect && this.IsViewPage)) && (this.viewPart != null))
                {
                    this.connectedParts.Add(this.viewPart);
                    this._connected = true;
                }
                if ((!this._connected && !this._rowConnected) && (this.transform == null))
                {
                    return base.IsConnected;
                }
                return true;
            }
        }

        public override bool IsViewPage
        {
            get
            {
                int num = 0;
                int num2 = 0;
                System.Web.UI.WebControls.WebParts.WebPart part = null;
                if ((!this.isViewPage.HasValue || !this.isViewPage.HasValue) && (base.WebPartManager != null))
                {
                    foreach (System.Web.UI.WebControls.WebParts.WebPart part2 in base.WebPartManager.WebParts)
                    {
                        num++;
                        if (((part2 is ListViewWebPart) || xsltTypeName.Equals(part2.GetType().FullName)) || "roxority_FilterZen.XsltListViewWebPart".Equals(part2.GetType().FullName))
                        {
                            part = part2;
                            num2++;
                        }
                    }
                    bool? nullable2 = this.isViewPage = new bool?((num2 == 1) && (part != null));
                    if (nullable2.Value)
                    {
                        this.viewPart = part;
                    }
                }
                if (this.isViewPage.HasValue && this.isViewPage.HasValue)
                {
                    return this.isViewPage.Value;
                }
                return false;
            }
        }

        public bool IsViewPageCore
        {
            get
            {
                return base.IsViewPage;
            }
        }

        [Personalizable]
        public string JsonFilters
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return string.Empty;
                }
                return base.GetProp<string>("JsonFilters", this.jsonFilters);
            }
            set
            {
                this.jsonFilters = base.LicEd(4) ? value : string.Empty;
            }
        }

        internal string ListViewUrl
        {
            get
            {
                if (this.listViewUrl == null)
                {
                    foreach (KeyValuePair<string, FilterPair> pair in this.PartFilters)
                    {
                        pair.GetType();
                    }
                }
                return this.listViewUrl;
            }
        }

        [Personalizable]
        public int MaxFiltersPerRow
        {
            get
            {
                return this.maxFiltersPerRow;
            }
            set
            {
                this.maxFiltersPerRow = (value < 0) ? 0 : value;
            }
        }

        [Personalizable]
        public string MultiValueFilterID
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return string.Empty;
                }
                return this.multiValueFilterID;
            }
            set
            {
                this.multiValueFilterID = base.LicEd(4) ? ProductPage.Trim(value, new char[0]) : string.Empty;
            }
        }

        public int MultiWidth
        {
            get
            {
                return this.multiWidth;
            }
            set
            {
                this.multiWidth = (value < 60) ? 240 : value;
            }
        }

        [Personalizable]
        public bool NoListFolders
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public List<KeyValuePair<string, FilterPair>> PartFilters
        {
            get
            {
                Predicate<string> match = null;
                Converter<KeyValuePair<string, string>, KeyValuePair<string, string>> converter = null;
                Converter<KeyValuePair<string, string>, KeyValuePair<string, string>> converter2 = null;
                Predicate<KeyValuePair<string, string>> predicate2 = null;
                KeyValuePair<string, string> pair;
                FilterBase.Interactive iaFilter;
                FilterBase.Date dtFilter;
                int num = 0;
                int result = -1;
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                if (this.partFilters == null)
                {
                    this.listViewUrl = string.Empty;
                    this.filtersNotSent.Clear();
                    this.partFilters = new List<KeyValuePair<string, FilterPair>>();
                    List<FilterBase> filters = this.GetFilters(true, false);
                    if (filters != null)
                    {
                        foreach (FilterBase base2 in filters)
                        {
                            if (flag3)
                            {
                                break;
                            }
                            if (base2 != null)
                            {
                                List<KeyValuePair<string, string>> list2;
                                IEnumerable<KeyValuePair<string, string>> filterPairs;
                                if (string.IsNullOrEmpty(base2.Name))
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.ToString(), "NoName"));
                                    continue;
                                }
                                if (!base2.Enabled)
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, "Disabled"));
                                    continue;
                                }
                                if (base2.SuppressIfInactive && !this.Activated)
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, "NonActive"));
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(base2.SuppressIfParam))
                                {
                                    if (match == null)
                                    {
                                        match = pn => this.Context.Request.QueryString[pn] != null;
                                    }
                                    if (new List<string>(base2.SuppressIfParam.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Exists(match))
                                    {
                                        this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, "HasParam"));
                                        continue;
                                    }
                                }
                                if ("_roxListView".Equals(base2.Name))
                                {
                                    filterPairs = base2.FilterPairs;
                                    if (filterPairs != null)
                                    {
                                        foreach (KeyValuePair<string, string> pair2 in filterPairs)
                                        {
                                            this.listViewUrl = pair2.Value;
                                            break;
                                        }
                                    }
                                    continue;
                                }
                                if (((this.GetGroups().Count > 1) && !string.IsNullOrEmpty(this.SelectedGroup)) && !base2.groups.Contains(this.SelectedGroup))
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, "Group"));
                                    continue;
                                }
                                if ((this.partFilters.Count > 0) && !base.LicEd(2))
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, ProductPage.GetResource("NopeEd", new object[] { base["MultiFilters", new object[0]], "Basic" })));
                                    continue;
                                }
                                iaFilter = base2 as FilterBase.Interactive;
                                num = 0;
                                filterPairs = base2.FilterPairs;
                                if (filterPairs == null)
                                {
                                    list2 = new List<KeyValuePair<string, string>>();
                                }
                                else
                                {
                                    try
                                    {
                                        list2 = new List<KeyValuePair<string, string>>(filterPairs);
                                    }
                                    catch (Exception exception)
                                    {
                                        this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, exception.Message));
                                        list2 = new List<KeyValuePair<string, string>>();
                                    }
                                }
                                if (((dtFilter = base2 as FilterBase.Date) != null) && ((dtFilter.EffectiveDateCulture != null) || !string.IsNullOrEmpty(dtFilter.Get<string>("DateFormat"))))
                                {
                                    if (converter == null)
                                    {
                                        converter = delegate(KeyValuePair<string, string> kv)
                                        {
                                            if (!string.IsNullOrEmpty(kv.Value))
                                            {
                                                return new KeyValuePair<string, string>(kv.Key, ProductPage.ConvertDateToString(ProductPage.ConvertStringToDate(kv.Value), dtFilter.Get<string>("DateFormat"), dtFilter.EffectiveDateCulture));
                                            }
                                            return kv;
                                        };
                                    }
                                    list2 = list2.ConvertAll<KeyValuePair<string, string>>(converter);
                                }
                                if (list2 != null)
                                {
                                    string str;
                                    if ((iaFilter != null) && iaFilter.IsNumeric)
                                    {
                                        if (converter2 == null)
                                        {
                                            converter2 = thePair => new KeyValuePair<string, string>(thePair.Key, iaFilter.GetNumeric(thePair.Value));
                                        }
                                        list2 = list2.ConvertAll<KeyValuePair<string, string>>(converter2);
                                    }
                                    if (((iaFilter != null) && iaFilter.pickerSemantics) && (iaFilter.SendAllAsMultiValuesIfEmpty && (list2.Count == 1)))
                                    {
                                        KeyValuePair<string, string> pair5 = list2[0];
                                        if (string.IsNullOrEmpty(pair5.Value))
                                        {
                                            try
                                            {
                                                foreach (string str2 in iaFilter.AllPickableValues)
                                                {
                                                    list2.Add(new KeyValuePair<string, string>(iaFilter.Name, str2));
                                                }
                                                if (list2.Count > 1)
                                                {
                                                    list2.RemoveAt(0);
                                                }
                                            }
                                            catch (Exception exception2)
                                            {
                                                for (int i = 1; i < list2.Count; i++)
                                                {
                                                    list2.RemoveAt(i);
                                                }
                                                iaFilter.Report(exception2);
                                            }
                                        }
                                    }
                                    //ProductPage.RemoveDuplicates<string, string>(list2);  //modified by:lhan
                                    if ((list2.Count > 0) && !string.IsNullOrEmpty(str = base2.Get<string>("SuppressMultiValues")))
                                    {
                                        if (str == "[-1]")
                                        {
                                            result = list2.Count - 1;
                                        }
                                        else if ((str.StartsWith("[") && str.EndsWith("]")) && int.TryParse(str, out result))
                                        {
                                            result--;
                                        }
                                        if ((result >= 0) && (result < list2.Count))
                                        {
                                            KeyValuePair<string, string>[] collection = new KeyValuePair<string, string>[] { list2[result] };
                                            list2 = new List<KeyValuePair<string, string>>(collection);
                                        }
                                        else
                                        {
                                            KeyValuePair<string, string>[] pairArray2 = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(base2.Name, string.Join(str, list2.ConvertAll<string>(tuple => tuple.Value).ToArray())) };
                                            list2 = new List<KeyValuePair<string, string>>(pairArray2);
                                        }
                                    }
                                    if (((list2.Count == 0) && this.SearchBehaviour) && !this.Activated)
                                    {
                                        list2.Add(new KeyValuePair<string, string>(base2.Name, string.Empty));
                                    }
                                    foreach (KeyValuePair<string, string> pair3 in list2)
                                    {
                                        num++;
                                        pair = pair3;
                                        flag2 = flag = false;
                                        if (this.SearchBehaviour && !this.Activated)
                                        {
                                            flag3 = true;
                                            pair = new KeyValuePair<string, string>(string.IsNullOrEmpty(pair.Key) ? "Title" : pair.Key, string.Concat(new object[] { "Emulating search behavior [", pair.Key, "] -- ", Guid.NewGuid() }));
                                        }
                                        if ((((base2.Get<int>("SuppressMode") == 1) || (base2.Get<int>("SuppressMode") == 2)) && (Array.IndexOf<string>(base2.suppressValues, pair.Value) >= 0)) || (((base2.Get<int>("SuppressMode") == 3) || (base2.Get<int>("SuppressMode") == 4)) && (Array.IndexOf<string>(base2.suppressValues, pair.Value) < 0)))
                                        {
                                            if ((base2.Get<int>("SuppressMode") == 1) || (base2.Get<int>("SuppressMode") == 3))
                                            {
                                                pair = new KeyValuePair<string, string>(pair.Key, string.Empty);
                                            }
                                            else
                                            {
                                                flag = true;
                                                this.filtersNotSent.Add(new KeyValuePair<string, string>(pair.Key, "Locked"));
                                            }
                                        }
                                        if (!flag)
                                        {
                                            if (this.SuppressUnknownFilters)
                                            {
                                                if (predicate2 == null)
                                                {
                                                    predicate2 = val => pair.Key.Equals(val.Key);
                                                }
                                                if (!this.validFilterNames.Exists(predicate2))
                                                {
                                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(pair.Key, "Suppressed"));
                                                    goto Label_087A;
                                                }
                                            }
                                            if ((!string.IsNullOrEmpty(pair.Value) || base2.Get<bool>("SendEmpty")) || !string.IsNullOrEmpty(base2.Get<string>("FallbackValue")))
                                            {
                                                flag2 = true;
                                                this.partFilters.Add((string.IsNullOrEmpty(pair.Value) && !string.IsNullOrEmpty(base2.Get<string>("FallbackValue"))) ? new KeyValuePair<string, FilterPair>(pair.Key, new FilterPair(pair.Key, base2.Get<string>("FallbackValue"), base2.Get<int>("CamlOperator"))) : new KeyValuePair<string, FilterPair>(pair.Key, new FilterPair(pair.Key, pair.Value, base2.Get<int>("CamlOperator"))));
                                            }
                                            else
                                            {
                                                this.filtersNotSent.Add(new KeyValuePair<string, string>(pair.Key, "Empty"));
                                            }
                                        }
                                    Label_087A:
                                        if (flag2 && !string.IsNullOrEmpty(base2.Get<string>("MultiValueSeparator")))
                                        {
                                            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                                            KeyValuePair<string, FilterPair> pair111 = this.partFilters[this.partFilters.Count - 1];
                                            this.partFilters.RemoveAt(this.partFilters.Count - 1);
                                            if (string.IsNullOrEmpty(base2.Get<string>("MultiFilterSeparator")))
                                            {
                                                foreach (string str3 in pair111.Value.Value.Split(new string[] { base2.Get<string>("MultiValueSeparator") }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    list.Add(new KeyValuePair<string, string>(pair111.Key, str3));
                                                }
                                            }
                                            else
                                            {
                                                foreach (string str4 in pair111.Value.Value.Split(new string[] { base2.Get<string>("MultiFilterSeparator") }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    string[] strArray;
                                                    if (((strArray = str4.Split(new string[] { base2.Get<string>("MultiValueSeparator") }, StringSplitOptions.RemoveEmptyEntries)) != null) && (strArray.Length > 0))
                                                    {
                                                        list.Add(new KeyValuePair<string, string>((strArray.Length == 1) ? pair111.Key : strArray[0], strArray[(strArray.Length == 1) ? 0 : 1]));
                                                    }
                                                }
                                            }
                                            foreach (KeyValuePair<string, string> pair4 in list)
                                            {
                                                if ((((base2.Get<int>("SuppressMode") != 1) && (base2.Get<int>("SuppressMode") != 2)) || (Array.IndexOf<string>(base2.suppressValues, pair4.Value) < 0)) && (((base2.Get<int>("SuppressMode") != 3) && (base2.Get<int>("SuppressMode") != 4)) || (Array.IndexOf<string>(base2.suppressValues, pair4.Value) >= 0)))
                                                {
                                                    this.partFilters.Add(new KeyValuePair<string, FilterPair>(pair4.Key, new FilterPair(pair4.Key, pair4.Value, base2.Get<int>("CamlOperator"))));
                                                }
                                                else if ((base2.Get<int>("SuppressMode") == 1) || (base2.Get<int>("SuppressMode") == 3))
                                                {
                                                    this.partFilters.Add(new KeyValuePair<string, FilterPair>(pair4.Key, new FilterPair(pair4.Key, pair4.Value, base2.Get<int>("CamlOperator"))));
                                                }
                                                else
                                                {
                                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(pair4.Key + " = " + pair4.Value, "Locked"));
                                                }
                                            }
                                        }
                                        if ((flag2 && (this.cellArgs != null)) && ((this.cellFilter != null) && base2.ID.Equals(this.cellFilter.ID)))
                                        {
                                            this.cellArgs.Cell = pair.Value;
                                            this.OnCellReady(this.cellArgs);
                                            this.cellArgs = null;
                                        }
                                        if (flag3)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if ((num == 0) && !(base2 is FilterBase.CamlDistinct))
                                {
                                    this.filtersNotSent.Add(new KeyValuePair<string, string>(base2.Name, "Null"));
                                }
                            }
                        }
                    }
                    //ProductPage.RemoveDuplicates<string, FilterPair>(this.partFilters); //modified by:lhan
                }
                return this.partFilters;
            }
        }

        [Personalizable]
        public bool RecollapseGroups
        {
            get
            {
                return base.GetProp<bool>("RecollapseGroups", this.recollapseGroups);
            }
            set
            {
                this.recollapseGroups = value;
            }
        }

        [Personalizable]
        public bool RememberFilterValues
        {
            get
            {
                if (base.LicEd(2))
                {
                    return base.GetProp<bool>("RememberFilterValues", this.rememberFilterValues);
                }
                return false;
            }
            set
            {
                this.rememberFilterValues = base.LicEd(2) && value;
            }
        }

        [Personalizable]
        public bool RespectFilters
        {
            get
            {
                return base.GetProp<bool>("RespectFilters", this.respectFilters);
            }
            set
            {
                this.respectFilters = value;
            }
        }

        protected internal RowProviderInitEventArgs RowArgs
        {
            get
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (this.rowArgs == null)
                {
                    this.rowArgs = new RowProviderInitEventArgs();
                    using (List<FilterBase>.Enumerator enumerator = this.GetFilters(false, false, false).GetEnumerator())
                    {
                        FilterBase fb;
                        while (enumerator.MoveNext())
                        {
                            fb = enumerator.Current;
                            if (fb.Enabled && !list.Exists(test => fb.Name.Equals(test.Key)))
                            {
                                FilterBase.Interactive interactive;
                                list.Add(new KeyValuePair<string, string>(fb.Name, (((interactive = fb as FilterBase.Interactive) == null) || string.IsNullOrEmpty(interactive.Get<string>("Label"))) ? fb.Name : (interactive.Get<string>("Label").EndsWith(":") ? interactive.Get<string>("Label").Substring(0, interactive.Get<string>("Label").Length - 1) : interactive.Get<string>("Label"))));
                            }
                        }
                    }
                    this.rowArgs.FieldList = list.ConvertAll<string>(value => value.Key).ToArray();
                    this.rowArgs.FieldDisplayList = list.ConvertAll<string>(value => value.Value).ToArray();
                }
                return this.rowArgs;
            }
        }

        public PropertyDescriptorCollection Schema
        {
            get
            {
                List<CustomPropertyDescriptor> list = new List<CustomPropertyDescriptor>();
                foreach (KeyValuePair<string, FilterPair> pair in this.PartFilters)
                {
                    string str;
                    list.Add(new CustomPropertyDescriptor(str = pair.Value.Key, null, new CustomPropertyHelper("FilterZen", str, str), null));
                }
                return new PropertyDescriptorCollection(list.ToArray());
            }
        }

        [Personalizable]
        public bool SearchBehaviour
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("SearchBehaviour", this.searchBehaviour);
                }
                return false;
            }
            set
            {
                this.searchBehaviour = base.LicEd(4) && value;
            }
        }

        [Personalizable(false)]
        public string SelectedGroup
        {
            get
            {
                this.SelectedGroup = this.group;
                return this.group;
            }
            set
            {
                List<string> groups = this.GetGroups();
                this.group = (groups.Count < 2) ? string.Empty : groups[0];
                foreach (string str in this.GetGroups())
                {
                    if (str.Equals(value))
                    {
                        this.group = value;
                        break;
                    }
                }
            }
        }

        [Personalizable]
        public string SerializedFilters
        {
            get
            {
                return this.serializedFilters;
            }
            set
            {
                this.serializedFilters = value;
            }
        }

        [Personalizable]
        public bool ShowClearButtons
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("ShowClearButtons", this.showClearButtons);
                }
                return false;
            }
            set
            {
                this.showClearButtons = base.LicEd(4) && value;
            }
        }

        internal StateBag State
        {
            get
            {
                return this.ViewState;
            }
        }

        [Personalizable]
        public bool SuppressSpacing
        {
            get
            {
                return base.GetProp<bool>("SuppressSpacing", this.suppressSpacing);
            }
            set
            {
                this.suppressSpacing = value;
            }
        }

        [Personalizable]
        public bool SuppressUnknownFilters
        {
            get
            {
                if (base.LicEd(4))
                {
                    return base.GetProp<bool>("SuppressUnknownFilters", this.suppressUnknownFilters);
                }
                return false;
            }
            set
            {
                this.suppressUnknownFilters = base.LicEd(4) && value;
            }
        }

        [Personalizable]
        public bool UrlParams
        {
            get
            {
                return (base.LicEd(4) && this.urlParams);
            }
            set
            {
                this.urlParams = base.LicEd(4) && value;
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

        [CompilerGenerated]
        public sealed class DisplayClass5e
        {
            public roxority_FilterWebPart ethis;
            public int eIndex;
            public string filterExpression;
            public bool revertToClear;
            public bool rowSent;
            public SetFilterEventArgs setFilterEventArgs;
            public string val;

            public void PartCommunicationMain_58()
            {
                if (this.ethis.isRowConsumer)
                {
                    foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> pair in this.ethis.PartFilters)
                    {
                        this.ethis.debugFilters.Add(new KeyValuePair<string, string>(pair.Key, pair.Value.Value));
                        if (((this.val = this.ethis.GetFilterValue(pair.Value.Value)).IndexOf('&') >= 0) && ((this.ethis.connectedList != null) || (this.ethis.connectedDataParts.Count > 0)))
                        {
                            this.ethis.additionalWarningsErrors.Add(this.ethis["Ampersand", new object[] { pair.Value.Key, this.val }]);
                            if (!ProductPage.Is14)
                            {
                                this.val = this.val.Replace("&", string.Empty);
                                this.ethis.additionalWarningsErrors.Add(this.ethis["Ampersand12", new object[0]]);
                            }
                        }
                        this.filterExpression = this.filterExpression + string.Format("FilterField{2}={0}&FilterValue{2}={1}&", pair.Key, this.val, ++this.eIndex);
                    }
                }
                if (this.filterExpression.Length != 0)
                {
                    this.filterExpression = this.filterExpression.Substring(0, this.filterExpression.Length - 1);
                }
                else
                {
                    this.revertToClear = true;
                }
                if (this.ethis.SetFilter != null)
                {
                    this.setFilterEventArgs.FilterExpression = (this.ethis.CamlFilters || this.ethis.Exed) ? "" : this.filterExpression;
                    try
                    {
                        this.ethis.SetFilter(this.ethis, this.setFilterEventArgs);
                        if (!this.ethis.CamlFilters)
                        {
                            this.ethis.eventOrderLog.Add(this.ethis["LogSent", new object[] { this.ethis["SendUrlFilterTo", new object[0]] }]);
                        }
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                this.revertToClear = true;
            }

            public void PartCommunicationMain_59()
            {
                if (!this.ethis.IsConnected || this.revertToClear)
                {
                    if (((this.ethis.IsConnected && this.revertToClear) && ((this.ethis.debugFilters.Count > 0) && !this.ethis._rowConnected)) && this.rowSent)
                    {
                        this.ethis.debugFilters.Clear();
                    }
                    if (this.ethis.ClearFilter != null)
                    {
                        this.ethis.ClearFilter(this.ethis, EventArgs.Empty);
                    }
                    else if (this.ethis.NoFilter != null)
                    {
                        this.ethis.NoFilter(this.ethis, EventArgs.Empty);
                    }
                    foreach (DataFormWebPart part in this.ethis.connectedDataParts)
                    {
                        if (!roxority_FilterWebPart.xsltTypeName.Equals(part.GetType().FullName))
                        {
                            part.FilterValues.Collection.Clear();
                            part.DataBind();
                        }
                    }
                }
                else if (!this.ethis.Exed)
                {
                    foreach (DataFormWebPart part2 in this.ethis.connectedDataParts)
                    {
                        if (!roxority_FilterWebPart.xsltTypeName.Equals(part2.GetType().FullName))
                        {
                            foreach (KeyValuePair<string, string> pair in this.ethis.debugFilters)
                            {
                                part2.FilterValues.Set(pair.Key, this.ethis.GetFilterValue(pair.Value));
                            }
                            part2.DataBind();
                        }
                    }
                }
            }
        }


        public class FilterPair
        {
            private roxority.SharePoint.CamlOperator camlOperator;
            private string key;
            internal bool nextAnd;
            private string value;

            public FilterPair(string key, string value, roxority.SharePoint.CamlOperator camlOperator)
            {
                this.nextAnd = true;
                this.Key = key;
                this.Value = value;
                this.CamlOperator = camlOperator;
            }

            public FilterPair(string key, string value, int camlOperator)
                : this(key, value, (roxority.SharePoint.CamlOperator)camlOperator)
            {
            }

            public override bool Equals(object obj)
            {
                roxority_FilterWebPart.FilterPair pair = obj as roxority_FilterWebPart.FilterPair;
                return ((((pair != null) && pair.CamlOperator.Equals(this.CamlOperator)) && pair.Key.Equals(this.Key)) && pair.Value.Equals(this.Value));
            }

            public override int GetHashCode()
            {
                return (((this.CamlOperator.GetHashCode() ^ this.Key.GetHashCode()) ^ this.Value.GetHashCode()) ^ this.nextAnd.GetHashCode());
            }

            public roxority.SharePoint.CamlOperator CamlOperator
            {
                get
                {
                    return this.camlOperator;
                }
                set
                {
                    this.camlOperator = value;
                }
            }

            public string Key
            {
                get
                {
                    return this.key;
                }
                set
                {
                    this.key = (value == null) ? string.Empty : value;
                }
            }

            public string Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = (value == null) ? string.Empty : value;
                }
            }
        }

        public class Transform : ITransformableFilterValues
        {
            public readonly FilterBase Filter;
            public readonly roxority_FilterWebPart WebPart;

            public Transform(roxority_FilterWebPart webPart, FilterBase filter)
            {
                this.WebPart = webPart;
                this.Filter = filter;
            }

            public bool AllowAllValue
            {
                get
                {
                    return !this.Filter.Get<bool>("SendEmpty");
                }
            }

            public bool AllowEmptyValue
            {
                get
                {
                    return this.Filter.Get<bool>("SendEmpty");
                }
            }

            public bool AllowMultipleValues
            {
                get
                {
                    return (this.Filter.SupportsMultipleValues && string.IsNullOrEmpty(this.Filter.SuppressMultiValues));
                }
            }

            public string ParameterName
            {
                get
                {
                    return this.Filter.Name;
                }
            }

            public ReadOnlyCollection<string> ParameterValues
            {
                get
                {
                    Predicate<FilterBase> match = null;
                    List<string> list = new List<string>();
                    if (!this.WebPart.CamlFilters)
                    {
                        foreach (KeyValuePair<string, roxority_FilterWebPart.FilterPair> pair in this.WebPart.PartFilters)
                        {
                            if (pair.Key == this.ParameterName)
                            {
                                list.Add(pair.Value.Value);
                            }
                        }
                        if (list.Count > 0)
                        {
                            ProductPage.RemoveDuplicates<string>(list);
                            object[] objArray = new object[1];
                            object[] objArray2 = new object[1];
                            if (match == null)
                            {
                                match = delegate(FilterBase fb)
                                {
                                    if (!fb.Enabled || !fb.SupportsMultipleValues)
                                    {
                                        return false;
                                    }
                                    if (!string.IsNullOrEmpty(this.WebPart.MultiValueFilterID))
                                    {
                                        return this.WebPart.MultiValueFilterID.Equals(fb.ID);
                                    }
                                    return true;
                                };
                            }
                            objArray2[0] = this.WebPart.FiltersList.Find(match).Name;
                            objArray[0] = this.WebPart["Transformed", objArray2];
                            this.WebPart.eventOrderLog.Add(this.WebPart["LogSent", objArray]);
                        }
                    }
                    if (list.Count != 0)
                    {
                        return new ReadOnlyCollection<string>(list);
                    }
                    return null;
                }
            }

            public class Provider : ProviderConnectionPoint
            {
                private readonly string cleanDisplayName;
                private readonly string originalDisplayName;

                public Provider(MethodInfo callbackMethod, System.Type interfaceType, System.Type controlType, string displayName, string id, bool allowsMultipleConnections)
                    : base(callbackMethod, interfaceType, controlType, ProductPage.GetProductResource(displayName, new object[0]).Replace("'{0}'-", "").Replace("'{0}'", ""), id, allowsMultipleConnections)
                {
                    this.cleanDisplayName = base.DisplayName;
                    this.originalDisplayName = displayName;
                }

                public override bool GetEnabled(Control control)
                {
                    roxority_FilterWebPart part = control as roxority_FilterWebPart;
                    FilterBase base2 = null;
                    bool flag = (part != null) && ((base2 = part.GetFilters(false, false).Find(delegate(FilterBase value)
                    {
                        if (!value.Enabled || !value.SupportsMultipleValues)
                        {
                            return false;
                        }
                        if (!string.IsNullOrEmpty(part.MultiValueFilterID))
                        {
                            return value.ID.Equals(part.MultiValueFilterID);
                        }
                        return true;
                    })) != null);
                    try
                    {
                        typeof(ConnectionPoint).GetField("_displayName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).SetValue(this, flag ? ProductPage.GetProductResource(part.HasPeople ? (this.originalDisplayName + "Alt") : this.originalDisplayName, new object[] { base2.Name }) : this.cleanDisplayName);
                    }
                    catch
                    {
                    }
                    if (flag)
                    {
                        part._connected = true;
                    }
                    return flag;
                }

                public string Description
                {
                    get
                    {
                        return ProductPage.GetProductResource("TransformDesc", new object[0]);
                    }
                }
            }
        }
    }
}