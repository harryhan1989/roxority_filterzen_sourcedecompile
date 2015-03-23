namespace roxority_ExportZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebPartPages;
    using roxority.Shared;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;

    public class ExportZenMenuItem : ZenMenuItem
    {
        private Dictionary<object, string> seps = new Dictionary<object, string>();
        private Dictionary<object, bool> unixes = new Dictionary<object, bool>();

        public ExportZenMenuItem()
        {
            base.actionPropPrefix = "ExportAction_";
            base.baseSequence = "200";
        }

        protected override string GetActionUrl(IDictionary inst, SPWeb thisWeb, bool useView, bool includeFilters, List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters, string fj, bool flag)
        {
            return string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/expo.aspx?sep=", this.Context.Server.UrlEncode(this.seps[inst["id"]]), "&unix=", this.unixes[inst["id"]] ? 1 : 0, "&rule=", inst["id"], "&exportlist=", this.List.ID, useView ? ("&lv=" + this.Context.Server.UrlEncode(base.MenuButton.ViewId + string.Empty)) : string.Empty, (!includeFilters || (effectiveFilters.Count == 0)) ? string.Empty : ("&f=" + this.Context.Server.UrlEncode(JSON.JsonEncode(effectiveFilters)) + "&fj=" + this.Context.Server.UrlEncode(fj)), "&dt=", DateTime.Now.Ticks });
        }

        public string GetPeopleClickScript(IDictionary inst, string webPageUrl, WebPart webPart, List<object[]> filters, List<string> andFilters, Dictionary<string, string> oobFilterPairs)
        {
            return this.GetRollupClickScript(inst, webPageUrl, webPart, filters, andFilters, oobFilterPairs);
        }

        public string GetRollupClickScript(IDictionary inst, string webPageUrl, WebPart webPart, List<object[]> filters, List<string> andFilters, Dictionary<string, string> oobFilterPairs)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = "&rpzopt=' + encodeURI(JSON.stringify(roxLastOps['" + webPart.ID + "'][1]))";
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            dictionary["rule"] = inst["id"] + string.Empty;
            dictionary["exportlist"] = webPart.ID;
            dictionary["View"] = HttpUtility.UrlEncode(webPageUrl);
            dictionary["t"] = HttpUtility.UrlEncode(webPart.Title);
            dictionary["r"] = DateTime.Now.Ticks.ToString();
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                string str4 = str2;
                str2 = str4 + (string.IsNullOrEmpty(str2) ? "?" : "&") + pair.Key + "=" + pair.Value;
            }
            str = SPContext.Current.Web.Url.TrimEnd(new char[] { '/' }) + "/_layouts/" + ProductPage.AssemblyName + "/expo.aspx" + str2;
            return string.Concat(new object[] { ZenMenuItem.IsLic(2) ? 2 : 0, "location.href='", str, str3, ";" });
        }

        protected override void OnActionsCreated(int cmdCount)
        {
            Control control;
            if (((ProductPage.Config(null, "HideAction") == "always") || ((ProductPage.Config(null, "HideAction") == "auto") && (cmdCount > 0))) && ((control = base.MenuTemplateControl.FindControl("ExportToSpreadsheet")) != null))
            {
                control.Visible = false;
            }
        }

        protected override void ValidateInstance(IDictionary inst, ref string clickScript)
        {
            string str;
            bool flag = false;
            string str2 = ",";
            if (inst["unix"] != null)
            {
                try
                {
                    flag = (bool) inst["unix"];
                }
                catch
                {
                    flag = false;
                }
            }
            if (flag && !ZenMenuItem.IsLic(2))
            {
                flag = false;
                clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource("PC_" + base.SchemaName + "_unix", new object[0]), "Basic" })) + "');";
            }
            if (!string.IsNullOrEmpty(str = inst["sep"] + string.Empty))
            {
                if (str == "s")
                {
                    str2 = ";";
                }
                else if (str == "t")
                {
                    str2 = "\t";
                }
            }
            else if (inst["excel"] != null)
            {
                try
                {
                    if ((bool) inst["excel"])
                    {
                        str2 = ";";
                    }
                }
                catch
                {
                }
            }
            if ((str2 != ",") && !ZenMenuItem.IsLic(2))
            {
                str2 = ",";
                clickScript = "alert('" + SPEncode.ScriptEncode(ProductPage.GetResource("NopeEd", new object[] { ProductPage.GetProductResource("PC_" + base.SchemaName + "_sep", new object[0]), "Basic" })) + "');";
            }
            this.seps[inst["id"]] = str2;
            this.unixes[inst["id"]] = flag;
        }
    }
}

