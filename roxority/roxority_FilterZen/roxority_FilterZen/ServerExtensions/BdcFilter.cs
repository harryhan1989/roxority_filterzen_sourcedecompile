namespace roxority_FilterZen.ServerExtensions
{
    using Microsoft.Office.Server.ApplicationRegistry.MetadataModel;
    using Microsoft.Office.Server.ApplicationRegistry.Runtime;
    using Microsoft.SharePoint.Portal.WebControls;
    using Microsoft.SharePoint.WebPartPages;
    using roxority.SharePoint;
    using roxority_FilterZen;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Serializable]
    public class BdcFilter : roxority_FilterZen.FilterBase.Interactive, IDisposable
    {
        private string bdcDisplayField;
        private string bdcEntity;
        private string bdcInstanceID;
        private string bdcValueField;
        private Field dispField;
        private Microsoft.Office.Server.ApplicationRegistry.MetadataModel.Entity entity;
        private LobSystemInstance lobInstance;
        private static readonly string scriptCheckDefault = @"setTimeout('var roxtmp = document.getElementById(\'filter_%%PLACEHOLDER_LISTID%%2\'); if (document.getElementById(\'filter_DefaultIfEmpty\').disabled = ((document.getElementById(\'filter_%%PLACEHOLDER_LISTID%%\').selectedIndex == 0) || (roxtmp && (roxtmp.selectedIndex == 0)))) { document.getElementById(\'label_filter_DefaultIfEmpty\').style.textDecoration = \'none\'; document.getElementById(\'filter_DefaultIfEmpty\').checked = true; }', 150);".Replace("%%PLACEHOLDER_LISTID%%", "BdcInstanceID");
        private bool sendNull;
        private const string SEPARATOR = "{C453BF77-8CC4-4e1a-A50E-8A60B293CE94}";
        private Field valueField;
        private Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View view;

        public BdcFilter()
        {
            this.bdcDisplayField = string.Empty;
            this.bdcEntity = string.Empty;
            this.bdcInstanceID = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            this.bdcValueField = string.Empty;
            base.pickerSemantics = true;
            base.defaultIfEmpty = true;
            base.reqEd = 4;
        }

        public BdcFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.bdcDisplayField = string.Empty;
            this.bdcEntity = string.Empty;
            this.bdcInstanceID = "0478f8f9-fbdc-42f5-99ea-f6e8ec702606";
            this.bdcValueField = string.Empty;
            base.reqEd = 4;
            base.pickerSemantics = true;
            try
            {
                this.BdcDisplayField = info.GetString("BdcDisplayField");
                this.BdcEntity = info.GetString("BdcEntity");
                this.BdcInstanceID = info.GetString("BdcInstanceID");
                this.BdcValueField = info.GetString("BdcValueField");
                this.SendNull = info.GetBoolean("SendNull");
                this.DefaultIfEmpty = info.GetBoolean("DefaultIfEmpty");
            }
            catch
            {
            }
        }

        public static void Apply(roxority_FilterWebPart filterWebPart, DataFormWebPart bdcListWebPart)
        {
            BdcDataSource dataSource = bdcListWebPart.DataSource as BdcDataSource;
            List<roxority_FilterZen.FilterBase> filters = filterWebPart.GetFilters();
            List<KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>>> list2 = new List<KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>>>();
            Dictionary<string, roxority_FilterZen.FilterBase> dictionary = new Dictionary<string, roxority_FilterZen.FilterBase>();
            foreach (roxority_FilterZen.FilterBase base3 in filters)
            {
                dictionary[base3.Name] = base3;
            }
            using (List<KeyValuePair<string, roxority_FilterWebPart.FilterPair>>.Enumerator enumerator2 = filterWebPart.PartFilters.GetEnumerator())
            {
                Predicate<KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>>> match = null;
                KeyValuePair<string, roxority_FilterWebPart.FilterPair> kvp;
                while (enumerator2.MoveNext())
                {
                    kvp = enumerator2.Current;
                    KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>> item = new KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>>(kvp.Key, new List<roxority_FilterWebPart.FilterPair>());
                    if (list2.Count > 0)
                    {
                        if (match == null)
                        {
                            match = test => test.Key.Equals(kvp.Key);
                        }
                        item = list2.Find(match);
                    }
                    if (string.IsNullOrEmpty(item.Key) || (kvp.Value == null))
                    {
                        item = new KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>>(kvp.Key, new List<roxority_FilterWebPart.FilterPair>());
                    }
                    list2.Remove(item);
                    item.Value.Add(kvp.Value);
                    list2.Add(item);
                }
            }
            foreach (KeyValuePair<string, List<roxority_FilterWebPart.FilterPair>> pair2 in list2)
            {
                foreach (roxority_FilterWebPart.FilterPair pair3 in pair2.Value)
                {
                    roxority_FilterZen.FilterBase base2;
                    Microsoft.SharePoint.WebControls.BusinessDataParameter parameter = new Microsoft.SharePoint.WebControls.BusinessDataParameter();
                    if (dictionary.TryGetValue(pair3.Key, out base2))
                    {
                        parameter.ConvertEmptyStringToNull = !base2.SendEmpty;
                    }
                    parameter.Name = pair3.Key;
                    parameter.DefaultValue = pair3.Value;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Operator=ConvertCamlOperator(pair3.CamlOperator);
                    dataSource.FilterParameters.Add(parameter);
                }
            }
        }

        public static bool CanApply(roxority_FilterWebPart filterWebPart, DataFormWebPart bdcListWebPart)
        {
            return (bdcListWebPart.DataSource is BdcDataSource);
        }

        public static Microsoft.SharePoint.WebControls.FilterOperator ConvertCamlOperator(CamlOperator op)
        {
            if (op == roxority.SharePoint.CamlOperator.Contains)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)2;
            }
            if (op == roxority.SharePoint.CamlOperator.BeginsWith)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)1;
            }
            if (op == roxority.SharePoint.CamlOperator.Neq)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)4;
            }
            if (op == roxority.SharePoint.CamlOperator.Gt)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)7;
            }
            if (op == roxority.SharePoint.CamlOperator.Lt)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)5;
            }
            if (op == roxority.SharePoint.CamlOperator.Geq)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)8;
            }
            if (op == roxority.SharePoint.CamlOperator.Leq)
            {
                return (Microsoft.SharePoint.WebControls.FilterOperator)6;
            }
            return 0;
        }

        public void Dispose()
        {
            try
            {
                if ((this.LobInstance != null) && (this.LobInstance.CurrentConnection != null))
                {
                    this.LobInstance.CloseConnection();
                }
            }
            catch
            {
            }
        }

        internal Dictionary<string, Microsoft.Office.Server.ApplicationRegistry.MetadataModel.Entity> GetEntities(LobSystemInstance inst)
        {
            return inst.GetEntities();
        }

        internal IEnumerable<object> GetIdentifierValues(IEntityInstance inst)
        {
            return inst.GetIdentifierValues();
        }

        internal LobSystemInstance GetLobSystemInstanceByName(string name)
        {
            return ApplicationRegistry.GetLobSystemInstanceByName(name);
        }

        internal Dictionary<string, LobSystemInstance> GetLobSystemInstances()
        {
            return ApplicationRegistry.GetLobSystemInstances();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("BdcDisplayField", this.BdcDisplayField);
            info.AddValue("BdcEntity", this.BdcEntity);
            info.AddValue("BdcInstanceID", this.BdcInstanceID);
            info.AddValue("BdcValueField", this.BdcValueField);
            info.AddValue("SendNull", this.SendNull);
            base.GetObjectData(info, context);
        }

        internal bool HasSpecificFinder(Microsoft.Office.Server.ApplicationRegistry.MetadataModel.Entity ent)
        {
            return ent.HasSpecificFinder();
        }

        public override void Render(HtmlTextWriter output, bool isUpperBound)
        {
            string str = string.Empty;
            int num = 0;
            bool flag = this.Get<bool>("CheckStyle");
            List<string> filterValues = this.GetFilterValues("filterval_" + base.ID, this.Get<string>("BdcInstanceID"));
            if (filterValues.Contains(string.Empty) || filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606"))
            {
                filterValues.Clear();
            }
            if (!base.Le(4, true))
            {
                output.WriteLine(ProductPage.GetResource("NopeEd", new object[] { roxority_FilterZen.FilterBase.GetFilterTypeTitle(base.GetType()), "Ultimate" }));
                base.Render(output, isUpperBound);
            }
            else
            {
                try
                {
                    if (this.Get<bool>("DefaultIfEmpty"))
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> roxMultiMins['filterval_" + base.ID + "'] = '0478f8f9-fbdc-42f5-99ea-f6e8ec702606'; </script>");
                        if (!flag)
                        {
                            str = string.Format("<option value=\"{0}\"{2}>{1}</option>", "0478f8f9-fbdc-42f5-99ea-f6e8ec702606", base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]], (((filterValues.Count == 0) || filterValues.Contains(string.Empty)) || filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")) ? " selected=\"selected\"" : string.Empty);
                        }
                        else
                        {
                            str = string.Format("<span><input class=\"rox-check-default\" name=\"filterval_" + base.ID + "\" type=\"" + (base.AllowMultiEnter ? "checkbox" : "radio") + "\" id=\"empty_filterval_" + base.ID + "\" value=\"{1}\" {3}" + (string.IsNullOrEmpty(base.HtmlOnChangeAttr) ? (" onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"jQuery('.chk-" + base.ID + "').attr('checked', false);")) + "/><label for=\"empty_filterval_" + base.ID + "\">{2}</label></span>", new object[] { ProductPage.GuidLower(Guid.NewGuid()), "0478f8f9-fbdc-42f5-99ea-f6e8ec702606", base["Empty" + (this.Get<bool>("SendEmpty") ? "None" : "All"), new object[0]], (((filterValues.Count == 0) || filterValues.Contains(string.Empty)) || filterValues.Contains("0478f8f9-fbdc-42f5-99ea-f6e8ec702606")) ? " checked=\"checked\"" : string.Empty });
                        }
                    }
                    if ((((this.LobInstance != null) && (this.Entity != null)) && ((this.View != null) && (this.ValueField != null))) && (this.DisplayField != null))
                    {
                        using (IEntityInstanceEnumerator enumerator = this.Entity.FindFiltered(new FilterCollection(), this.LobInstance))
                        {
                            while (enumerator.MoveNext())
                            {
                                string str2;
                                if ((enumerator.Current != null) && !string.IsNullOrEmpty(str2 = ProductPage.Serialize<object>(this.GetIdentifierValues(enumerator.Current))))
                                {
                                    num++;
                                    if (flag)
                                    {
                                        str = str + string.Format("<span><input class=\"chk-" + base.ID + " rox-check-value\" name=\"filterval_" + base.ID + "\" type=\"" + (base.AllowMultiEnter ? "checkbox" : "radio") + "\" id=\"x{0}\" value=\"{1}\" {3}" + ((string.IsNullOrEmpty(base.HtmlOnChangeAttr) && this.Get<bool>("DefaultIfEmpty")) ? (" onclick=\"document.getElementById('empty_filterval_" + base.ID + "').checked=false;\"") : base.HtmlOnChangeAttr.Replace("onchange=\"", "onclick=\"" + (this.Get<bool>("DefaultIfEmpty") ? ("document.getElementById('empty_filterval_" + base.ID + "').checked=false;") : string.Empty))) + "/><label for=\"x{0}\">{2}</label></span>", new object[] { ProductPage.GuidLower(Guid.NewGuid()), str2, base.GetDisplayValue(ProductPage.Trim(enumerator.Current[this.DisplayField.Name], new char[0])), filterValues.Contains(str2.ToString()) ? " checked=\"checked\"" : string.Empty });
                                    }
                                    else
                                    {
                                        str = str + string.Format("<option value=\"{0}\"{2}>{1}</option>", str2, base.GetDisplayValue(ProductPage.Trim(enumerator.Current[this.DisplayField.Name], new char[0])), filterValues.Contains(str2.ToString()) ? " selected=\"selected\"" : string.Empty);
                                    }
                                }
                                if ((base.PickerLimit != 0) && (num >= base.PickerLimit))
                                {
                                    goto Label_04FB;
                                }
                            }
                        }
                    }
                Label_04FB:
                    if (str.Length > 0)
                    {
                        if (flag)
                        {
                            output.Write("<div>" + str + "</div>");
                        }
                        else
                        {
                            output.Write("<select" + (base.AllowMultiEnter ? " size=\"1\" multiple=\"multiple\" class=\"rox-multiselect ms-input\"" : " class=\"ms-input\"") + " name=\"{0}\" id=\"{0}\"{1}>" + str + "</select>", "filterval_" + base.ID, base.AllowMultiEnter ? base.HtmlOnChangeMultiAttr : base.HtmlOnChangeAttr);
                        }
                    }
                }
                catch (Exception exception)
                {
                    base.Report(exception);
                }
                base.Render(output, isUpperBound);
            }
        }

        public override void UpdatePanel(Panel panel)
        {
            string str = string.Format("<option value=\"{0}\"{2}>{1}</option>", string.Empty, string.Empty, string.Empty);
            string str2 = str;
            bool flag = false;
            "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><input style=\"width: 98%;\" class=\"ms-input\" type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{2}\"{3}/></div>".Replace("<input ", "<input disabled=\"disabled\" ");
            string str5 = "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>".Replace("<select ", "<select disabled=\"disabled\" ");
            string str6 = "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>".Replace("<input ", "<input disabled=\"disabled\" ");
            "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><textarea rows=\"{3}\" onchange=\"{4}\" style=\"width: 96%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\">{2}</textarea></div>".Replace("<textarea ", "<textarea disabled=\"disabled\" ");
            panel.Controls.Add(new LiteralControl("<div class=\"roxsectionlink\"><a onclick=\"jQuery('#roxfilterspecial').slideToggle();\" href=\"#noop\">" + base["FilterProps", new object[] { roxority_FilterZen.FilterBase.GetFilterTypeTitle(base.GetType()) }] + "</a></div><fieldset style=\"padding: 4px; background-color: InfoBackground; color: InfoText;\" id=\"roxfilterspecial\" style=\"display: none;\">"));
            if (base.parentWebPart != null)
            {
                try
                {
                    try
                    {
                        foreach (KeyValuePair<string, LobSystemInstance> pair in this.GetLobSystemInstances())
                        {
                            try
                            {
                                foreach (KeyValuePair<string, Microsoft.Office.Server.ApplicationRegistry.MetadataModel.Entity> pair2 in this.GetEntities(pair.Value))
                                {
                                    try
                                    {
                                        if ((pair2.Value.GetIdentifierCount() == 1) && this.HasSpecificFinder(pair2.Value))
                                        {
                                            string str3;
                                            str2 = str2 + string.Format("<option value=\"{0}\"{2}>{1}</option>", str3 = pair.Key + "{C453BF77-8CC4-4e1a-A50E-8A60B293CE94}" + pair2.Key, (pair.Value.ContainsLocalizedDisplayName() ? pair.Value.GetLocalizedDisplayName() : pair.Value.GetDefaultDisplayName()) + ": " + (pair2.Value.ContainsLocalizedDisplayName() ? pair2.Value.GetLocalizedDisplayName() : pair2.Value.GetDefaultDisplayName()), (flag = str3.Equals(this.Get<string>("BdcEntity"))) ? " selected=\"selected\"" : string.Empty);
                                            if (flag)
                                            {
                                                this.lobInstance = pair.Value;
                                                this.entity = pair2.Value;
                                                this.view = roxority_BusinessDataItemBuilderWebPart.GetView(this.entity);
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        base.Report(exception);
                                    }
                                }
                            }
                            catch (Exception exception2)
                            {
                                base.Report(exception2);
                            }
                        }
                    }
                    catch (Exception exception3)
                    {
                        base.Report(exception3);
                    }
                    panel.Controls.Add(base.CreateControl(base.Le(4, false) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : str5, "BdcEntity", new object[] { " onchange=\"roxRefreshFilters();\"", str2 }));
                    str2 = str;
                    try
                    {
                        if (this.view != null)
                        {
                            foreach (Field field in this.view.Fields)
                            {
                                try
                                {
                                    str2 = str2 + string.Format("<option value=\"{0}\"{2}>{1}</option>", field.Name, field.ContainsLocalizedDisplayName ? field.LocalizedDisplayName : field.DefaultDisplayName, (flag = field.Name.Equals(this.Get<string>("BdcValueField"))) ? " selected=\"selected\"" : string.Empty);
                                    if (flag)
                                    {
                                        this.valueField = field;
                                    }
                                }
                                catch (Exception exception4)
                                {
                                    base.Report(exception4);
                                }
                            }
                        }
                    }
                    catch (Exception exception5)
                    {
                        base.Report(exception5);
                    }
                    panel.Controls.Add(base.CreateControl(base.Le(4, false) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : str5, "BdcValueField", new object[] { " onchange=\"roxRefreshFilters();\"", str2 }));
                    str2 = str;
                    try
                    {
                        if (this.view != null)
                        {
                            foreach (Field field2 in this.view.Fields)
                            {
                                try
                                {
                                    str2 = str2 + string.Format("<option value=\"{0}\"{2}>{1}</option>", field2.Name, field2.ContainsLocalizedDisplayName ? field2.LocalizedDisplayName : field2.DefaultDisplayName, (flag = field2.Name.Equals(this.Get<string>("BdcDisplayField"))) ? " selected=\"selected\"" : string.Empty);
                                    if (flag)
                                    {
                                        this.dispField = field2;
                                    }
                                }
                                catch (Exception exception6)
                                {
                                    base.Report(exception6);
                                }
                            }
                        }
                    }
                    catch (Exception exception7)
                    {
                        base.Report(exception7);
                    }
                    panel.Controls.Add(base.CreateControl(base.Le(4, false) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : str5, "BdcDisplayField", new object[] { " onchange=\"roxRefreshFilters();\"", str2 }));
                    str2 = string.Format("<option value=\"{0}\"{2}>{1}</option>", string.Empty, base["Empty", new object[0]], string.Empty);
                    if ((this.entity != null) && (this.view != null))
                    {
                        if (this.valueField == null)
                        {
                            this.valueField = this.view.Fields[0];
                        }
                        if (this.valueField != null)
                        {
                            if (this.dispField == null)
                            {
                                this.dispField = this.valueField;
                            }
                            using (IEntityInstanceEnumerator enumerator = this.entity.FindFiltered(new FilterCollection(), this.lobInstance))
                            {
                                while (enumerator.MoveNext())
                                {
                                    string str4;
                                    if ((enumerator.Current != null) && !string.IsNullOrEmpty(str4 = ProductPage.Serialize<object>(this.GetIdentifierValues(enumerator.Current))))
                                    {
                                        str2 = str2 + string.Format("<option value=\"{0}\"{2}>{1}</option>", str4, enumerator.Current[this.dispField.Name], str4.ToString().Equals(this.Get<string>("BdcInstanceID")) ? " selected=\"selected\"" : string.Empty);
                                    }
                                }
                            }
                        }
                    }
                    panel.Controls.Add(base.CreateControl(base.Le(4, false) ? "<div id=\"div_{0}\"><div class=\"rox-prop\"><label id=\"label_{0}\" for=\"{0}\">{1}</label></div><select style=\"width: 98%;\" class=\"ms-input\" name=\"{0}\" id=\"{0}\"{2}>{3}</select></div>" : str5, "BdcInstanceID", new object[] { " onchange=\"" + scriptCheckDefault + "\"", str2 }));
                }
                catch (Exception exception8)
                {
                    base.Report(exception8);
                }
                finally
                {
                    try
                    {
                        if ((this.lobInstance != null) && (this.lobInstance.CurrentConnection != null))
                        {
                            this.lobInstance.CloseConnection();
                        }
                    }
                    catch (Exception exception9)
                    {
                        base.Report(exception9);
                    }
                }
            }
            panel.Controls.Add(new LiteralControl("</fieldset>"));
            try
            {
                panel.Controls.Add(base.CreateControl(base.Le(4, false) ? "<div id=\"div_{0}\" class=\"rox-prop\"><input onclick=\"{3}\" type=\"checkbox\" name=\"{0}\" id=\"{0}\"{2}/><label id=\"label_{0}\" for=\"{0}\">{1}</label></div>" : str6, "SendNull", new object[] { base.GetChecked(this.Get<bool>("SendNull")) }));
                base.UpdatePanel(panel);
                panel.Controls.Add(base.CreateScript(scriptCheckDefault));
            }
            catch (Exception exception10)
            {
                base.Report(exception10);
            }
        }

        public override void UpdateProperties(Panel panel)
        {
            this.BdcDisplayField = this.Get<string>("BdcDisplayField");
            this.BdcEntity = this.Get<string>("BdcEntity");
            this.BdcInstanceID = this.Get<string>("BdcInstanceID");
            this.BdcValueField = this.Get<string>("BdcValueField");
            this.SendNull = this.Get<bool>("SendNull");
            base.UpdateProperties(panel);
        }

        protected internal override IEnumerable<string> AllPickableValues
        {
            get
            {
                if (((this.LobInstance != null) && (this.Entity != null)) && ((this.View != null) && (this.ValueField != null)))
                {
                    using (IEntityInstanceEnumerator iteratorVariable1 = this.Entity.FindFiltered(new FilterCollection(), this.LobInstance))
                    {
                        while (iteratorVariable1.MoveNext())
                        {
                            object iteratorVariable0;
                            if ((iteratorVariable1.Current != null) && ((iteratorVariable0 = iteratorVariable1.Current[this.ValueField.Name]) != null))
                            {
                                yield return iteratorVariable0.ToString();
                            }
                        }
                    }
                }
            }
        }

        public string BdcDisplayField
        {
            get
            {
                if (!base.Le(4, true))
                {
                    return string.Empty;
                }
                return this.bdcDisplayField;
            }
            set
            {
                this.bdcDisplayField = base.Le(4, true) ? ProductPage.Trim(value, new char[0]) : string.Empty;
            }
        }

        public string BdcEntity
        {
            get
            {
                if (!base.Le(4, true))
                {
                    return string.Empty;
                }
                return this.bdcEntity;
            }
            set
            {
                this.bdcEntity = base.Le(4, true) ? ProductPage.Trim(value, new char[0]) : string.Empty;
            }
        }

        public string BdcInstanceID
        {
            get
            {
                if (!base.Le(4, true))
                {
                    return string.Empty;
                }
                return this.bdcInstanceID;
            }
            set
            {
                this.bdcInstanceID = base.Le(4, true) ? ProductPage.Trim(value, new char[0]) : string.Empty;
            }
        }

        public string BdcValueField
        {
            get
            {
                if (!base.Le(4, true))
                {
                    return string.Empty;
                }
                return this.bdcValueField;
            }
            set
            {
                this.bdcValueField = base.Le(4, true) ? ProductPage.Trim(value, new char[0]) : string.Empty;
            }
        }

        public override bool DefaultIfEmpty
        {
            get
            {
                string str = this.Get<string>("BdcInstanceID");
                if (!base.DefaultIfEmpty && !string.IsNullOrEmpty(str))
                {
                    return "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(str);
                }
                return true;
            }
            set
            {
                string str = this.Get<string>("BdcInstanceID");
                base.DefaultIfEmpty = (value || string.IsNullOrEmpty(str)) || "0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(str);
            }
        }

        public Field DisplayField
        {
            get
            {
                string str = this.Get<string>("BdcDisplayField");
                if ((this.dispField == null) && (this.View != null))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            this.dispField = this.ValueField;
                        }
                        else
                        {
                            foreach (Field field in this.View.Fields)
                            {
                                if (str.Equals(field.Name))
                                {
                                    this.dispField = field;
                                    break;
                                }
                            }
                        }
                        if (this.dispField == null)
                        {
                            throw new Exception(base["BdcFailed", new object[] { str }]);
                        }
                    }
                    catch
                    {
                        throw new Exception(base["BdcFailed", new object[] { str }]);
                    }
                }
                return this.dispField;
            }
        }

        public Microsoft.Office.Server.ApplicationRegistry.MetadataModel.Entity Entity
        {
            get
            {
                string str = this.Get<string>("BdcEntity");
                int num = (str == null) ? -1 : str.IndexOf("{C453BF77-8CC4-4e1a-A50E-8A60B293CE94}");
                if (((this.entity == null) && (this.LobInstance != null)) && (num > 0))
                {
                    try
                    {
                        this.entity = this.lobInstance.GetEntities()[str.Substring(num + "{C453BF77-8CC4-4e1a-A50E-8A60B293CE94}".Length)];
                        if (this.entity == null)
                        {
                            throw new Exception(base["BdcFailed", new object[] { str }]);
                        }
                    }
                    catch
                    {
                        throw new Exception(base["BdcFailed", new object[] { str }]);
                    }
                }
                return this.entity;
            }
        }

        public IEnumerable<IEntityInstance> EntityInstances
        {
            get
            {
                List<string> filterValues = this.GetFilterValues("filterval_" + this.ID, this.Get<string>("BdcInstanceID"));
                foreach (string iteratorVariable3 in filterValues)
                {
                    IEnumerable<object> iteratorVariable1;
                    if ((!string.IsNullOrEmpty(iteratorVariable3) && !"0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(iteratorVariable3)) && ((iteratorVariable1 = ProductPage.Deserialize<object>(iteratorVariable3, null)) != null))
                    {
                        foreach (object iteratorVariable4 in iteratorVariable1)
                        {
                            IEntityInstance iteratorVariable2 = null;
                            try
                            {
                                iteratorVariable2 = this.Entity.FindSpecific(iteratorVariable4, this.LobInstance);
                            }
                            catch (Exception exception)
                            {
                                this.Report(exception);
                            }
                            if (iteratorVariable2 != null)
                            {
                                yield return iteratorVariable2;
                            }
                        }
                    }
                }
            }
        }

        protected internal override IEnumerable<KeyValuePair<string, string>> FilterPairs
        {
            get
            {
                IEnumerable<IEntityInstance> entityInstances = null;
                Field valueField = null;
                object iteratorVariable2 = null;
                if (!ProductPage.LicEdition(ProductPage.GetContext(), (ProductPage.LicInfo) null, 4))
                {
                    throw new Exception(ProductPage.GetResource("NopeEd", new object[] { roxority_FilterZen.FilterBase.GetFilterTypeTitle(this.GetType()), "Ultimate" }));
                }
                try
                {
                    valueField = this.ValueField;
                    entityInstances = this.EntityInstances;
                }
                catch (Exception exception)
                {
                    this.Report(exception);
                }
                if (entityInstances != null)
                {
                    foreach (IEntityInstance iteratorVariable4 in entityInstances)
                    {
                        KeyValuePair<string, string> iteratorVariable3 = new KeyValuePair<string, string>(string.Empty, string.Empty);
                        try
                        {
                            if ((iteratorVariable4 == null) && this.SendNull)
                            {
                                iteratorVariable3 = new KeyValuePair<string, string>(this.Name, string.Empty);
                            }
                            else if ((iteratorVariable4 != null) && (valueField != null))
                            {
                                iteratorVariable3 = new KeyValuePair<string, string>(this.Name, ((iteratorVariable2 = iteratorVariable4[valueField]) == null) ? string.Empty : iteratorVariable2.ToString());
                            }
                        }
                        catch (Exception exception2)
                        {
                            this.Report(exception2);
                        }
                        if (!string.IsNullOrEmpty(iteratorVariable3.Key))
                        {
                            yield return ("0478f8f9-fbdc-42f5-99ea-f6e8ec702606".Equals(iteratorVariable3.Value) ? new KeyValuePair<string, string>(iteratorVariable3.Key, string.Empty) : iteratorVariable3);
                        }
                    }
                }
            }
        }

        public LobSystemInstance LobInstance
        {
            get
            {
                string str = this.Get<string>("BdcEntity");
                int length = (str == null) ? -1 : str.IndexOf("{C453BF77-8CC4-4e1a-A50E-8A60B293CE94}");
                if ((this.lobInstance == null) && (length > 0))
                {
                    try
                    {
                        this.lobInstance = this.GetLobSystemInstanceByName(str.Substring(0, length));
                        if (this.lobInstance == null)
                        {
                            throw new Exception(base["BdcFailed", new object[] { str }]);
                        }
                    }
                    catch
                    {
                        throw new Exception(base["BdcFailed", new object[] { str }]);
                    }
                }
                return this.lobInstance;
            }
        }

        public bool SendNull
        {
            get
            {
                return (base.Le(4, true) && this.sendNull);
            }
            set
            {
                this.sendNull = base.Le(4, true) && value;
            }
        }

        public Field ValueField
        {
            get
            {
                string str = this.Get<string>("BdcValueField");
                if ((this.valueField == null) && (this.View != null))
                {
                    try
                    {
                        foreach (Field field in this.View.Fields)
                        {
                            if (string.IsNullOrEmpty(str) || str.Equals(field.Name))
                            {
                                this.valueField = field;
                                break;
                            }
                        }
                        if (this.valueField == null)
                        {
                            throw new Exception(base["BdcFailed", new object[] { str }]);
                        }
                    }
                    catch
                    {
                        throw new Exception(base["BdcFailed", new object[] { str }]);
                    }
                }
                return this.valueField;
            }
        }

        public Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View View
        {
            get
            {
                if ((this.view == null) && (this.Entity != null))
                {
                    this.view = roxority_BusinessDataItemBuilderWebPart.GetView(this.Entity);
                }
                return this.view;
            }
        }



    }
}

