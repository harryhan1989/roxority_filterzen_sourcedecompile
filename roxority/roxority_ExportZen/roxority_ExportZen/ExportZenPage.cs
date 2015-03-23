namespace roxority_ExportZen
{
    using Microsoft.SharePoint;
    using roxority.Shared;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Xml;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using OfficeOpenXml;

    public class ExportZenPage : Page
    {
        internal static readonly Regex htmlRegex = new Regex("(?></?\\w+)(?>(?:[^>'\"]+|'[^']*'|\"[^\"]*\")*)>", RegexOptions.Singleline);
        internal static Reflector refl = null;
        internal const string WEIRD_CHAR = "​";

       
        internal static string CsvEscape(string value, string sep, bool unix)
        {
            while (value.StartsWith(sep, StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.Substring(1);
            }
            while (value.EndsWith(sep, StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.Substring(0, value.Length - 1);
            }
            if (unix)
            {
                value = value.Replace(@"\", @"\\").Replace(sep, @"\" + sep).Replace("\r", @"\r").Replace("\n", @"\n");
            }
            else
            {
                value = value.Replace("\"", "\"\"");
                if (((value.IndexOf(sep) > 0) || (value.IndexOf('\r') > 0)) || (value.IndexOf('\n') > 0))
                {
                    value = "\"" + value + "\"";
                }
            }
            return value.Trim();
        }

        public static void Export(TextWriter writer, ICollection<IDictionary> allActions, string webUrl, string exportListID, string ruleID, string listViewID, string separator, string unixFlag, string fs, int min, string fj)
        {
            ExcelPackage excelPackage;
            HSSFWorkbook hssfworkbook;
            var fileEx=new ExportZenPage().IsExportFromTemp(webUrl, exportListID, out excelPackage, out hssfworkbook);

            if (excelPackage != null)
            {
                ExportFromTemp(excelPackage, fileEx, writer, allActions, webUrl, exportListID, ruleID, listViewID, separator, unixFlag, fs, min, fj);
                return;
            }
            else if (hssfworkbook != null)
            {
                ExportFromTemp(hssfworkbook, writer, allActions, webUrl, exportListID, ruleID, listViewID, separator, unixFlag, fs, min, fj);
                return;
            }
                
            bool flag4;
            roxority.Shared.Action<string, string> action2 = null;
            string locale = string.Empty;
            string sep = ",";
            string json = null;
            string mulSep = ProductPage.Config(ProductPage.GetContext(), "MultiSep");
            int index = -1;
            bool expandGroups = false;
            bool flag2 = false;
            bool flag3 = false;
            HttpContext current = null;
            Encoding encoding = Encoding.Default;
            SPList list = null;
            SPListItemCollection collection = null;
            SPView view = null;
            ProductPage.LicInfo licInfo = ProductPage.LicInfo.Get(null);
            ArrayList flist = null;
            Hashtable filterHierarchy = null;
            Hashtable opt = null;
            XmlDocument doc = new XmlDocument();
            roxority.Shared.Action<string, string> csvWrite = null;
            IDictionary inst = null;
            bool is2 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 2);
            bool flag5 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 4);
            bool flag6 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, min);

            string filteredCaml = "";

            if (fj.Contains("((((((") && fj.Contains("))))))"))
            {
                filteredCaml = fj.Substring(fj.IndexOf("((((((") + 6, fj.IndexOf("))))))") - fj.IndexOf("((((((") - 6);
                fj = fj.Substring(0, fj.IndexOf("(((((("));
            }

            try
            {
                current = HttpContext.Current;
            }
            catch
            {
            }
            if (flag4 = (((current != null) && (current.Request != null))) && !string.IsNullOrEmpty(json = current.Request.Form["rpzopt"]))
            {
                opt = JSON.JsonDecode(json) as Hashtable;
            }
            if (licInfo.expired)
            {
                throw new SPException(ProductPage.GetResource("LicExpiry", new object[0]));
            }
            if (!flag6)
            {
                throw new SPException(ProductPage.GetResource("NopeEd", new object[] { "ExportZen.exe", "Ultimate" }));
            }
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    string str3;
                    string str10;
                    string[] strArray;
                    int num;
                    object obj2;
                    SPField field;
                    if (!flag4 && (string.IsNullOrEmpty(exportListID) || ((list = web.Lists[new Guid(exportListID)]) == null)))
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoExportList", new object[0]));
                    }
                    foreach (IDictionary dictionary3 in allActions)
                    {
                        Guid guid;
                        Guid guid2;
                        if ((ruleID.Equals(dictionary3["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase) || ruleID.Equals(dictionary3["name"] + string.Empty, StringComparison.InvariantCultureIgnoreCase)) || ((!Guid.Empty.Equals(guid2 = ProductPage.GetGuid(dictionary3["id"] + string.Empty)) && !Guid.Empty.Equals(guid = ProductPage.GetGuid(ruleID))) && guid.Equals(guid2)))
                        {
                            inst = dictionary3;
                            break;
                        }
                    }
                    if (inst == null)
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoRuleItem", new object[] { ruleID, allActions.Count, web.CurrentUser.LoginName, ProductPage.Config(ProductPage.GetContext(), "_lang") }));
                    }
                    if ((flag5 && (list != null)) && !string.IsNullOrEmpty(listViewID))
                    {
                        try
                        {
                            view = list.Views[new Guid(listViewID)];
                        }
                        catch
                        {
                        }
                    }
                    if (((strArray = (inst["cols"] + string.Empty).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)) == null) || (strArray.Length == 0))
                    {
                        if (flag4)
                        {
                            strArray = HttpUtility.UrlDecode(((((int) opt["nm"]) != 0) ? ("rox___pt:" + ProductPage.GetResource("Name", new object[0]) + "\r\n") : string.Empty) + opt["pr"]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else if (list != null)
                        {
                            SPView defaultView = view;
                            if (defaultView == null)
                            {
                                defaultView = list.DefaultView;
                            }
                            List<string> list4 = new List<string>(defaultView.ViewFields.Count);
                            foreach (string str14 in defaultView.ViewFields)
                            {
                                if ((Array.IndexOf<string>(ProductPage.SPOddFieldNames, str14) < 0) || str14.StartsWith("Link"))
                                {
                                    list4.Add(str14 + (((field = ProductPage.GetField(list, str14)) == null) ? string.Empty : (":" + field.Title)));
                                }
                            }
                            strArray = list4.ToArray();
                        }
                    }
                    if ((strArray.Length > 3) && !is2)
                    {
                        strArray = new string[] { strArray[0], strArray[1], strArray[2] };
                    }
                    flag2 = ((obj2 = inst["vhrows"]) is bool) && ((bool) obj2);
                    if (!string.IsNullOrEmpty(str3 = (inst["vhcol"] + string.Empty).Trim()))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if ("roxVersion".Equals((string.Empty + strArray[i]).Trim(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index < 0)
                        {
                            Array.Resize<string>(ref strArray, strArray.Length + 1);
                            strArray[index = strArray.Length - 1] = str3;
                        }
                        else
                        {
                            strArray[index] = str3;
                        }
                    }
                    try
                    {
                        locale = inst["loc"] + string.Empty;
                    }
                    catch
                    {
                    }
                    try
                    {
                        string str9;
                        if (!string.IsNullOrEmpty(str9 = inst["enc"] + string.Empty))
                        {
                            int num3;
                            if (int.TryParse(str9, out num3))
                            {
                                encoding = Encoding.GetEncoding(num3);
                            }
                            else
                            {
                                encoding = Encoding.GetEncoding(str9);
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (current != null)
                    {
                        current.Response.ContentEncoding = encoding;
                        current.Response.ContentType = "text/csv; charset=" + encoding.WebName;
                        current.Response.AddHeader("Content-Disposition", string.Concat(new object[] { "attachment;filename=\"", System.Web.HttpUtility.UrlEncode(SafeName(((list == null) ? current.Request.Form["t"] : list.Title) + " " + JsonSchemaManager.GetDisplayName(inst, "ExportActions", false)), System.Text.Encoding.UTF8), "_", DateTime.Now.Ticks, ".csv\"" }));
                    }
                    try
                    {
                        if ((inst["bom"] is bool) && ((bool) inst["bom"]))
                        {
                            writer.Write("﻿");
                        }
                    }
                    catch
                    {
                    }
                    if (is2)
                    {
                        string str13 = inst["sep"] + string.Empty;
                        if (!string.IsNullOrEmpty(separator))
                        {
                            sep = separator;
                        }
                        else if (((inst["excel"] is bool) && ((bool) inst["excel"])) && (string.IsNullOrEmpty(str13) || (str13 == "s")))
                        {
                            sep = ";";
                        }
                        else if (str13 == "t")
                        {
                            sep = "\t";
                        }
                    }
                    if (!JsonSchemaManager.Bool(inst["nf"], false))
                    {
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            str10 = (((num = strArray[j].IndexOf(':')) <= 0) ? strArray[j] : strArray[j].Substring(num + 1)).Trim();
                            if ((str10 == "ID") && (j == 0))
                            {
                                str10 = "\"ID\"";
                            }
                            writer.Write(str10 + ((j == (strArray.Length - 1)) ? "\r\n" : sep));
                        }
                    }
                    if (is2 && !string.IsNullOrEmpty(fs))
                    {
                        flist = JSON.JsonDecode(fs) as ArrayList;
                    }
                    if (is2 && !string.IsNullOrEmpty(fj))
                    {
                        filterHierarchy = JSON.JsonDecode(fj) as Hashtable;
                    }
                    if ((flist != null) && (flist.Count == 0))
                    {
                        flist = null;
                    }
                    if (list != null)
                    {
                        if ((view == null) && (flist == null))
                        {
                            collection = list.Items;
                        }
                        else
                        {
                            string schemaXml;
                            SPQuery query = (view != null) ? new SPQuery(view) : new SPQuery();
                            if (view == null)
                            {
                                query.Folder = list.RootFolder;
                                schemaXml = "<View><Query/></View>";
                            }
                            else
                            {
                                schemaXml = view.SchemaXml;
                            }
                            query.AutoHyperlink = query.ExpandUserField = query.ItemIdQuery = false;
                            query.ExpandRecurrence = query.IndividualProperties = query.IncludePermissions = query.IncludeMandatoryColumns = query.IncludeAttachmentVersion = query.IncludeAttachmentUrls = query.IncludeAllUserPermissions = query.RecurrenceOrderBy = true;
                            query.RowLimit = 0;
                            query.ViewFields = string.Empty;
                            foreach (SPField field2 in ProductPage.TryEach<SPField>(list.Fields))
                            {
                                query.ViewFields = query.ViewFields + string.Format("<FieldRef Name=\"{0}\"/>", field2.InternalName);
                            }
                            if (flist != null)
                            {
                                doc.LoadXml(schemaXml);
                                if (!string.IsNullOrEmpty(schemaXml = ProductPage.ApplyCore(list, schemaXml, doc, flist, ref expandGroups, false, filterHierarchy, null)))
                                {
                                    doc.LoadXml(schemaXml);
                                    XmlNode node = doc.DocumentElement.SelectSingleNode("Query");
                                    if (node != null)
                                    {
                                        if (filteredCaml!="")
                                        {
                                            string innerText=node.InnerXml.ToLower();
                                            string queryed = innerText.Substring(innerText.IndexOf("<where>") + 7, innerText.Length - innerText.IndexOf("<where>") - 7);
                                            query.Query = HttpUtility.HtmlDecode(queryed.Replace("</where>", filteredCaml));
                                        }
                                        //query.Query = node.InnerXml;
                                    }
                                }
                            }
                            collection = list.GetItems(query);
                        }
                    }
                    if (action2 == null)
                    {
                        action2 = (csvVal, suffix) => writer.Write(CsvEscape(csvVal, sep, is2 && "1".Equals(unixFlag)) + suffix);
                    }
                    csvWrite = action2;
                    if (flag4)
                    {
                        ExportRollup(writer, inst, opt, web, filterHierarchy, strArray, locale, csvWrite, sep);
                    }
                    else if (collection != null)
                    {
                        foreach (SPListItem item in ProductPage.TryEach<SPListItem>(collection))
                        {
                            for (int k = 0; k < strArray.Length; k++)
                            {
                                string str4;
                                flag3 = false;
                                if (!string.IsNullOrEmpty(str3) && (k == index))
                                {
                                    List<SPListItemVersion> list2 = new List<SPListItemVersion>();
                                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                    str4 = string.Empty;
                                    foreach (SPListItemVersion version in item.Versions)
                                    {
                                        list2.Add(version);
                                    }
                                    list2.Sort((Comparison<SPListItemVersion>) ((one, two) => one.VersionLabel.CompareTo(two.VersionLabel)));
                                    if (flag2 && (list2.Count > 1))
                                    {
                                        flag3 = true;
                                        csvWrite(string.Empty, "\r\n");
                                        for (int m = list2.Count - 1; m >= 0; m--)
                                        {
                                            for (int n = 0; n < strArray.Length; n++)
                                            {
                                                if (n == index)
                                                {
                                                    csvWrite(list2[m].VersionLabel, (n == (strArray.Length - 1)) ? "\r\n" : sep);
                                                }
                                                else
                                                {
                                                    str4 = string.Empty;
                                                    if (!"ID".Equals(str10 = ((num = strArray[n].IndexOf(':')) <= 0) ? strArray[n] : strArray[n].Substring(0, num)))
                                                    {
                                                        field = ProductPage.GetField(item, str10);
                                                        obj2 = string.Empty;
                                                        try
                                                        {
                                                            obj2 = list2[m][str10];
                                                        }
                                                        catch
                                                        {
                                                        }
                                                        str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                                    }
                                                    csvWrite(str4, (n == (strArray.Length - 1)) ? "\r\n" : sep);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int num9 = 0; num9 < list2.Count; num9++)
                                        {
                                            string str5 = string.Concat(new object[] { list2[num9].VersionLabel, " ", list2[num9].Created.ToLocalTime(), "\r\n", list2[num9].CreatedBy.LookupValue });
                                            for (int num10 = 0; num10 < strArray.Length; num10++)
                                            {
                                                if (num10 != index)
                                                {
                                                    try
                                                    {
                                                        string str7;
                                                        string fieldName = ((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(0, num).Trim();
                                                        field = ProductPage.GetField(item, fieldName);
                                                        if (field != null)
                                                        {
                                                            fieldName = field.InternalName;
                                                        }
                                                        try
                                                        {
                                                            obj2 = list2[num9][fieldName];
                                                        }
                                                        catch
                                                        {
                                                            obj2 = null;
                                                        }
                                                        string str8 = GetExportValue(web, obj2, field, locale, mulSep);
                                                        if (!dictionary.TryGetValue(fieldName, out str7) || (str7 != str8))
                                                        {
                                                            dictionary[fieldName] = str8;
                                                            string str15 = str5;
                                                            str5 = str15 + "\r\n" + (((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(num + 1).Trim()) + ": " + str8;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                            str4 = str5 + "\r\n\r\n" + str4;
                                        }
                                    }
                                }
                                else
                                {
                                    field = ProductPage.GetField(item, str10 = ((num = strArray[k].IndexOf(':')) <= 0) ? strArray[k] : strArray[k].Substring(0, num));
                                    if (field != null)
                                    {
                                        obj2 = item[field.Id];
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj2 = item[str10];
                                        }
                                        catch
                                        {
                                            obj2 = null;
                                        }
                                    }
                                    str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                }
                                if (!flag3)
                                {
                                    csvWrite(str4, (k == (strArray.Length - 1)) ? "\r\n" : sep);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Export From Template file(xlsm/xlsx)
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="allActions"></param>
        /// <param name="webUrl"></param>
        /// <param name="exportListID"></param>
        /// <param name="ruleID"></param>
        /// <param name="listViewID"></param>
        /// <param name="separator"></param>
        /// <param name="unixFlag"></param>
        /// <param name="fs"></param>
        /// <param name="min"></param>
        /// <param name="fj"></param>
        internal static void ExportFromTemp(ExcelPackage excelPackage, string fileEx, TextWriter writer, ICollection<IDictionary> allActions, string webUrl, string exportListID, string ruleID, string listViewID, string separator, string unixFlag, string fs, int min, string fj)
        {
            bool flag4;
            roxority.Shared.Action<string, string> action2 = null;
            string locale = string.Empty;
            string sep = ",";
            string json = null;
            string mulSep = ProductPage.Config(ProductPage.GetContext(), "MultiSep");
            int index = -1;
            bool expandGroups = false;
            bool flag2 = false;
            bool flag3 = false;
            HttpContext current = null;
            Encoding encoding = Encoding.Default;
            SPList list = null;
            SPListItemCollection collection = null;
            SPView view = null;
            ProductPage.LicInfo licInfo = ProductPage.LicInfo.Get(null);
            ArrayList flist = null;
            Hashtable filterHierarchy = null;
            Hashtable opt = null;
            XmlDocument doc = new XmlDocument();
            roxority.Shared.Action<string, string> csvWrite = null;
            IDictionary inst = null;
            bool is2 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 2);
            bool flag5 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 4);
            bool flag6 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, min);

            ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];

            int currentRow = 2;

            string filteredCaml = "";

            if (fj.Contains("((((((") && fj.Contains("))))))"))
            {
                filteredCaml = fj.Substring(fj.IndexOf("((((((") + 6, fj.IndexOf("))))))") - fj.IndexOf("((((((") - 6);
                fj = fj.Substring(0, fj.IndexOf("(((((("));
            }

            try
            {
                current = HttpContext.Current;
            }
            catch
            {
            }
            if (flag4 = (((current != null) && (current.Request != null))) && !string.IsNullOrEmpty(json = current.Request.Form["rpzopt"]))
            {
                opt = JSON.JsonDecode(json) as Hashtable;
            }
            if (licInfo.expired)
            {
                throw new SPException(ProductPage.GetResource("LicExpiry", new object[0]));
            }
            if (!flag6)
            {
                throw new SPException(ProductPage.GetResource("NopeEd", new object[] { "ExportZen.exe", "Ultimate" }));
            }
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    string str3;
                    string str10;
                    string[] strArray;
                    int num;
                    object obj2;
                    SPField field;
                    if (!flag4 && (string.IsNullOrEmpty(exportListID) || ((list = web.Lists[new Guid(exportListID)]) == null)))
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoExportList", new object[0]));
                    }
                    foreach (IDictionary dictionary3 in allActions)
                    {
                        Guid guid;
                        Guid guid2;
                        if ((ruleID.Equals(dictionary3["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase) || ruleID.Equals(dictionary3["name"] + string.Empty, StringComparison.InvariantCultureIgnoreCase)) || ((!Guid.Empty.Equals(guid2 = ProductPage.GetGuid(dictionary3["id"] + string.Empty)) && !Guid.Empty.Equals(guid = ProductPage.GetGuid(ruleID))) && guid.Equals(guid2)))
                        {
                            inst = dictionary3;
                            break;
                        }
                    }
                    if (inst == null)
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoRuleItem", new object[] { ruleID, allActions.Count, web.CurrentUser.LoginName, ProductPage.Config(ProductPage.GetContext(), "_lang") }));
                    }
                    if ((flag5 && (list != null)) && !string.IsNullOrEmpty(listViewID))
                    {
                        try
                        {
                            view = list.Views[new Guid(listViewID)];
                        }
                        catch
                        {
                        }
                    }
                    if (((strArray = (inst["cols"] + string.Empty).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)) == null) || (strArray.Length == 0))
                    {
                        if (flag4)
                        {
                            strArray = HttpUtility.UrlDecode(((((int) opt["nm"]) != 0) ? ("rox___pt:" + ProductPage.GetResource("Name", new object[0]) + "\r\n") : string.Empty) + opt["pr"]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else if (list != null)
                        {
                            SPView defaultView = view;
                            if (defaultView == null)
                            {
                                defaultView = list.DefaultView;
                            }
                            List<string> list4 = new List<string>(defaultView.ViewFields.Count);
                            foreach (string str14 in defaultView.ViewFields)
                            {
                                if ((Array.IndexOf<string>(ProductPage.SPOddFieldNames, str14) < 0) || str14.StartsWith("Link"))
                                {
                                    list4.Add(str14 + (((field = ProductPage.GetField(list, str14)) == null) ? string.Empty : (":" + field.Title)));
                                }
                            }
                            strArray = list4.ToArray();
                        }
                    }
                    if ((strArray.Length > 3) && !is2)
                    {
                        strArray = new string[] { strArray[0], strArray[1], strArray[2] };
                    }
                    flag2 = ((obj2 = inst["vhrows"]) is bool) && ((bool) obj2);
                    if (!string.IsNullOrEmpty(str3 = (inst["vhcol"] + string.Empty).Trim()))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if ("roxVersion".Equals((string.Empty + strArray[i]).Trim(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index < 0)
                        {
                            Array.Resize<string>(ref strArray, strArray.Length + 1);
                            strArray[index = strArray.Length - 1] = str3;
                        }
                        else
                        {
                            strArray[index] = str3;
                        }
                    }
                    try
                    {
                        locale = inst["loc"] + string.Empty;
                    }
                    catch
                    {
                    }
                    try
                    {
                        string str9;
                        if (!string.IsNullOrEmpty(str9 = inst["enc"] + string.Empty))
                        {
                            int num3;
                            if (int.TryParse(str9, out num3))
                            {
                                encoding = Encoding.GetEncoding(num3);
                            }
                            else
                            {
                                encoding = Encoding.GetEncoding(str9);
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (current != null)
                    {
                        current.Response.ContentEncoding = encoding;
                        current.Response.ContentType = "application/vnd.openxmlformats; charset=" + encoding.WebName;
                        current.Response.AddHeader("Content-Disposition", string.Concat(new object[] { "attachment;filename=\"", System.Web.HttpUtility.UrlEncode(SafeName(((list == null) ? current.Request.Form["t"] : list.Title) + " " + JsonSchemaManager.GetDisplayName(inst, "ExportActions", false)), System.Text.Encoding.UTF8), "_", DateTime.Now.Ticks, fileEx+"\"" }));
                    }
                    try
                    {
                        if ((inst["bom"] is bool) && ((bool) inst["bom"]))
                        {
                            writer.Write("﻿");
                        }
                    }
                    catch
                    {
                    }
                    if (is2)
                    {
                        string str13 = inst["sep"] + string.Empty;
                        if (!string.IsNullOrEmpty(separator))
                        {
                            sep = separator;
                        }
                        else if (((inst["excel"] is bool) && ((bool) inst["excel"])) && (string.IsNullOrEmpty(str13) || (str13 == "s")))
                        {
                            sep = ";";
                        }
                        else if (str13 == "t")
                        {
                            sep = "\t";
                        }
                    }
                    if (!JsonSchemaManager.Bool(inst["nf"], false))
                    {
                        //for (int j = 0; j < strArray.Length; j++)
                        //{
                        //    str10 = (((num = strArray[j].IndexOf(':')) <= 0) ? strArray[j] : strArray[j].Substring(num + 1)).Trim();
                        //    if ((str10 == "ID") && (j == 0))
                        //    {
                        //        str10 = "\"ID\"";
                        //    }
                        //    writer.Write(str10 + ((j == (strArray.Length - 1)) ? "\r\n" : sep));
                        //}
                    }
                    if (is2 && !string.IsNullOrEmpty(fs))
                    {
                        flist = JSON.JsonDecode(fs) as ArrayList;
                    }
                    if (is2 && !string.IsNullOrEmpty(fj))
                    {
                        filterHierarchy = JSON.JsonDecode(fj) as Hashtable;
                    }
                    if ((flist != null) && (flist.Count == 0))
                    {
                        flist = null;
                    }
                    if (list != null)
                    {
                        if ((view == null) && (flist == null))
                        {
                            collection = list.Items;
                        }
                        else
                        {
                            string schemaXml;
                            SPQuery query = (view != null) ? new SPQuery(view) : new SPQuery();
                            if (view == null)
                            {
                                query.Folder = list.RootFolder;
                                schemaXml = "<View><Query/></View>";
                            }
                            else
                            {
                                schemaXml = view.SchemaXml;
                            }
                            query.AutoHyperlink = query.ExpandUserField = query.ItemIdQuery = false;
                            query.ExpandRecurrence = query.IndividualProperties = query.IncludePermissions = query.IncludeMandatoryColumns = query.IncludeAttachmentVersion = query.IncludeAttachmentUrls = query.IncludeAllUserPermissions = query.RecurrenceOrderBy = true;
                            query.RowLimit = 0;
                            query.ViewFields = string.Empty;
                            foreach (SPField field2 in ProductPage.TryEach<SPField>(list.Fields))
                            {
                                query.ViewFields = query.ViewFields + string.Format("<FieldRef Name=\"{0}\"/>", field2.InternalName);
                            }
                            if (flist != null)
                            {
                                doc.LoadXml(schemaXml);
                                if (!string.IsNullOrEmpty(schemaXml = ProductPage.ApplyCore(list, schemaXml, doc, flist, ref expandGroups, false, filterHierarchy, null)))
                                {
                                    doc.LoadXml(schemaXml);
                                    XmlNode node = doc.DocumentElement.SelectSingleNode("Query");
                                    if (node != null)
                                    {
                                        if (filteredCaml!="")
                                        {
                                            string innerText=node.InnerXml.ToLower();
                                            string queryed = innerText.Substring(innerText.IndexOf("<where>") + 7, innerText.Length - innerText.IndexOf("<where>") - 7);
                                            query.Query = HttpUtility.HtmlDecode(queryed.Replace("</where>", filteredCaml));
                                        }
                                        //query.Query = node.InnerXml;
                                    }
                                }
                            }
                            collection = list.GetItems(query);
                        }
                    }
                    if (action2 == null)
                    {
                        action2 = (csvVal, suffix) => writer.Write(CsvEscape(csvVal, sep, is2 && "1".Equals(unixFlag)) + suffix);
                    }
                    csvWrite = action2;
                    if (flag4)
                    {
                        ExportRollup(writer, inst, opt, web, filterHierarchy, strArray, locale, csvWrite, sep);
                    }
                    else if (collection != null)
                    {
                        foreach (SPListItem item in ProductPage.TryEach<SPListItem>(collection))
                        {
                            for (int k = 0; k < strArray.Length; k++)
                            {
                                string str4;
                                flag3 = false;
                                if (!string.IsNullOrEmpty(str3) && (k == index))
                                {
                                    List<SPListItemVersion> list2 = new List<SPListItemVersion>();
                                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                    str4 = string.Empty;
                                    foreach (SPListItemVersion version in item.Versions)
                                    {
                                        list2.Add(version);
                                    }
                                    list2.Sort((Comparison<SPListItemVersion>) ((one, two) => one.VersionLabel.CompareTo(two.VersionLabel)));
                                    if (flag2 && (list2.Count > 1))
                                    {
                                        flag3 = true;
                                        //csvWrite(string.Empty, "\r\n");
                                        for (int m = list2.Count - 1; m >= 0; m--)
                                        {
                                            for (int n = 0; n < strArray.Length; n++)
                                            {
                                                if (n == index)
                                                {
                                                    sheet.Cells[currentRow, k+1].Value = list2[m].VersionLabel;
                                                }
                                                else
                                                {
                                                    str4 = string.Empty;
                                                    if (!"ID".Equals(str10 = ((num = strArray[n].IndexOf(':')) <= 0) ? strArray[n] : strArray[n].Substring(0, num)))
                                                    {
                                                        field = ProductPage.GetField(item, str10);
                                                        obj2 = string.Empty;
                                                        try
                                                        {
                                                            obj2 = list2[m][str10];
                                                        }
                                                        catch
                                                        {
                                                        }
                                                        str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                                    }
                                                    sheet.Cells[currentRow, k+1].Value = str4;                                              
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int num9 = 0; num9 < list2.Count; num9++)
                                        {
                                            string str5 = string.Concat(new object[] { list2[num9].VersionLabel, " ", list2[num9].Created.ToLocalTime(), "\r\n", list2[num9].CreatedBy.LookupValue });
                                            for (int num10 = 0; num10 < strArray.Length; num10++)
                                            {
                                                if (num10 != index)
                                                {
                                                    try
                                                    {
                                                        string str7;
                                                        string fieldName = ((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(0, num).Trim();
                                                        field = ProductPage.GetField(item, fieldName);
                                                        if (field != null)
                                                        {
                                                            fieldName = field.InternalName;
                                                        }
                                                        try
                                                        {
                                                            obj2 = list2[num9][fieldName];
                                                        }
                                                        catch
                                                        {
                                                            obj2 = null;
                                                        }
                                                        string str8 = GetExportValue(web, obj2, field, locale, mulSep);
                                                        if (!dictionary.TryGetValue(fieldName, out str7) || (str7 != str8))
                                                        {
                                                            dictionary[fieldName] = str8;
                                                            string str15 = str5;
                                                            str5 = str15 + "\r\n" + (((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(num + 1).Trim()) + ": " + str8;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                            str4 = str5 + "\r\n\r\n" + str4;
                                        }
                                    }
                                }
                                else
                                {
                                    field = ProductPage.GetField(item, str10 = ((num = strArray[k].IndexOf(':')) <= 0) ? strArray[k] : strArray[k].Substring(0, num));
                                    if (field != null)
                                    {
                                        obj2 = item[field.Id];
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj2 = item[str10];
                                        }
                                        catch
                                        {
                                            obj2 = null;
                                        }
                                    }
                                    str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                }
                                if (!flag3)
                                {
                                    sheet.Cells[currentRow, k+1].Value = str4;
                                }
                            }
                            currentRow++;
                        }
                    }
                }
            }
            current.Response.Clear();
            MemoryStream file = new MemoryStream();
            excelPackage.SaveAs(file);
            //(excelPackage.Stream as MemoryStream).WriteTo(current.Response.OutputStream);
            file.WriteTo(current.Response.OutputStream);
            current.Response.End();
        }

        /// <summary>
        /// Export From Template file(xls)
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="allActions"></param>
        /// <param name="webUrl"></param>
        /// <param name="exportListID"></param>
        /// <param name="ruleID"></param>
        /// <param name="listViewID"></param>
        /// <param name="separator"></param>
        /// <param name="unixFlag"></param>
        /// <param name="fs"></param>
        /// <param name="min"></param>
        /// <param name="fj"></param>
        internal static void ExportFromTemp(HSSFWorkbook hssfworkbook, TextWriter writer, ICollection<IDictionary> allActions, string webUrl, string exportListID, string ruleID, string listViewID, string separator, string unixFlag, string fs, int min, string fj)
        {
            bool flag4;
            roxority.Shared.Action<string, string> action2 = null;
            string locale = string.Empty;
            string sep = ",";
            string json = null;
            string mulSep = ProductPage.Config(ProductPage.GetContext(), "MultiSep");
            int index = -1;
            bool expandGroups = false;
            bool flag2 = false;
            bool flag3 = false;
            HttpContext current = null;
            Encoding encoding = Encoding.Default;
            SPList list = null;
            SPListItemCollection collection = null;
            SPView view = null;
            ProductPage.LicInfo licInfo = ProductPage.LicInfo.Get(null);
            ArrayList flist = null;
            Hashtable filterHierarchy = null;
            Hashtable opt = null;
            XmlDocument doc = new XmlDocument();
            roxority.Shared.Action<string, string> csvWrite = null;
            IDictionary inst = null;
            bool is2 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 2);
            bool flag5 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, 4);
            bool flag6 = ProductPage.LicEdition(ProductPage.GetContext(), licInfo, min);

            ISheet sheet = hssfworkbook.GetSheetAt(0);

            int currentRow = 1;
            int currentCol = 0;

            string filteredCaml = "";

            if (fj.Contains("((((((") && fj.Contains("))))))"))
            {
                filteredCaml = fj.Substring(fj.IndexOf("((((((") + 6, fj.IndexOf("))))))") - fj.IndexOf("((((((") - 6);
                fj = fj.Substring(0, fj.IndexOf("(((((("));
            }

            try
            {
                current = HttpContext.Current;
            }
            catch
            {
            }
            if (flag4 = (((current != null) && (current.Request != null))) && !string.IsNullOrEmpty(json = current.Request.Form["rpzopt"]))
            {
                opt = JSON.JsonDecode(json) as Hashtable;
            }
            if (licInfo.expired)
            {
                throw new SPException(ProductPage.GetResource("LicExpiry", new object[0]));
            }
            if (!flag6)
            {
                throw new SPException(ProductPage.GetResource("NopeEd", new object[] { "ExportZen.exe", "Ultimate" }));
            }
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    string str3;
                    string str10;
                    string[] strArray;
                    int num;
                    object obj2;
                    SPField field;
                    if (!flag4 && (string.IsNullOrEmpty(exportListID) || ((list = web.Lists[new Guid(exportListID)]) == null)))
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoExportList", new object[0]));
                    }
                    foreach (IDictionary dictionary3 in allActions)
                    {
                        Guid guid;
                        Guid guid2;
                        if ((ruleID.Equals(dictionary3["id"] + string.Empty, StringComparison.InvariantCultureIgnoreCase) || ruleID.Equals(dictionary3["name"] + string.Empty, StringComparison.InvariantCultureIgnoreCase)) || ((!Guid.Empty.Equals(guid2 = ProductPage.GetGuid(dictionary3["id"] + string.Empty)) && !Guid.Empty.Equals(guid = ProductPage.GetGuid(ruleID))) && guid.Equals(guid2)))
                        {
                            inst = dictionary3;
                            break;
                        }
                    }
                    if (inst == null)
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoRuleItem", new object[] { ruleID, allActions.Count, web.CurrentUser.LoginName, ProductPage.Config(ProductPage.GetContext(), "_lang") }));
                    }
                    if ((flag5 && (list != null)) && !string.IsNullOrEmpty(listViewID))
                    {
                        try
                        {
                            view = list.Views[new Guid(listViewID)];
                        }
                        catch
                        {
                        }
                    }
                    if (((strArray = (inst["cols"] + string.Empty).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)) == null) || (strArray.Length == 0))
                    {
                        if (flag4)
                        {
                            strArray = HttpUtility.UrlDecode(((((int)opt["nm"]) != 0) ? ("rox___pt:" + ProductPage.GetResource("Name", new object[0]) + "\r\n") : string.Empty) + opt["pr"]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else if (list != null)
                        {
                            SPView defaultView = view;
                            if (defaultView == null)
                            {
                                defaultView = list.DefaultView;
                            }
                            List<string> list4 = new List<string>(defaultView.ViewFields.Count);
                            foreach (string str14 in defaultView.ViewFields)
                            {
                                if ((Array.IndexOf<string>(ProductPage.SPOddFieldNames, str14) < 0) || str14.StartsWith("Link"))
                                {
                                    list4.Add(str14 + (((field = ProductPage.GetField(list, str14)) == null) ? string.Empty : (":" + field.Title)));
                                }
                            }
                            strArray = list4.ToArray();
                        }
                    }
                    if ((strArray.Length > 3) && !is2)
                    {
                        strArray = new string[] { strArray[0], strArray[1], strArray[2] };
                    }
                    flag2 = ((obj2 = inst["vhrows"]) is bool) && ((bool)obj2);
                    if (!string.IsNullOrEmpty(str3 = (inst["vhcol"] + string.Empty).Trim()))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if ("roxVersion".Equals((string.Empty + strArray[i]).Trim(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index < 0)
                        {
                            Array.Resize<string>(ref strArray, strArray.Length + 1);
                            strArray[index = strArray.Length - 1] = str3;
                        }
                        else
                        {
                            strArray[index] = str3;
                        }
                    }
                    try
                    {
                        locale = inst["loc"] + string.Empty;
                    }
                    catch
                    {
                    }
                    try
                    {
                        string str9;
                        if (!string.IsNullOrEmpty(str9 = inst["enc"] + string.Empty))
                        {
                            int num3;
                            if (int.TryParse(str9, out num3))
                            {
                                encoding = Encoding.GetEncoding(num3);
                            }
                            else
                            {
                                encoding = Encoding.GetEncoding(str9);
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (current != null)
                    {
                        current.Response.ContentEncoding = encoding;
                        current.Response.ContentType = "application/vnd.ms-excel; charset=" + encoding.WebName;
                        current.Response.AddHeader("Content-Disposition", string.Concat(new object[] { "attachment;filename=\"", System.Web.HttpUtility.UrlEncode(SafeName(((list == null) ? current.Request.Form["t"] : list.Title) + " " + JsonSchemaManager.GetDisplayName(inst, "ExportActions", false)), System.Text.Encoding.UTF8), "_", DateTime.Now.Ticks, ".xls\"" }));
                    }
                    try
                    {
                        if ((inst["bom"] is bool) && ((bool)inst["bom"]))
                        {
                            writer.Write("﻿");
                        }
                    }
                    catch
                    {
                    }
                    if (is2)
                    {
                        string str13 = inst["sep"] + string.Empty;
                        if (!string.IsNullOrEmpty(separator))
                        {
                            sep = separator;
                        }
                        else if (((inst["excel"] is bool) && ((bool)inst["excel"])) && (string.IsNullOrEmpty(str13) || (str13 == "s")))
                        {
                            sep = ";";
                        }
                        else if (str13 == "t")
                        {
                            sep = "\t";
                        }
                    }
                    if (!JsonSchemaManager.Bool(inst["nf"], false))
                    {
                        //for (int j = 0; j < strArray.Length; j++)
                        //{
                        //    str10 = (((num = strArray[j].IndexOf(':')) <= 0) ? strArray[j] : strArray[j].Substring(num + 1)).Trim();
                        //    if ((str10 == "ID") && (j == 0))
                        //    {
                        //        str10 = "\"ID\"";
                        //    }
                        //    writer.Write(str10 + ((j == (strArray.Length - 1)) ? "\r\n" : sep));
                        //}
                    }
                    if (is2 && !string.IsNullOrEmpty(fs))
                    {
                        flist = JSON.JsonDecode(fs) as ArrayList;
                    }
                    if (is2 && !string.IsNullOrEmpty(fj))
                    {
                        filterHierarchy = JSON.JsonDecode(fj) as Hashtable;
                    }
                    if ((flist != null) && (flist.Count == 0))
                    {
                        flist = null;
                    }
                    if (list != null)
                    {
                        if ((view == null) && (flist == null))
                        {
                            collection = list.Items;
                        }
                        else
                        {
                            string schemaXml;
                            SPQuery query = (view != null) ? new SPQuery(view) : new SPQuery();
                            if (view == null)
                            {
                                query.Folder = list.RootFolder;
                                schemaXml = "<View><Query/></View>";
                            }
                            else
                            {
                                schemaXml = view.SchemaXml;
                            }
                            query.AutoHyperlink = query.ExpandUserField = query.ItemIdQuery = false;
                            query.ExpandRecurrence = query.IndividualProperties = query.IncludePermissions = query.IncludeMandatoryColumns = query.IncludeAttachmentVersion = query.IncludeAttachmentUrls = query.IncludeAllUserPermissions = query.RecurrenceOrderBy = true;
                            query.RowLimit = 0;
                            query.ViewFields = string.Empty;
                            foreach (SPField field2 in ProductPage.TryEach<SPField>(list.Fields))
                            {
                                query.ViewFields = query.ViewFields + string.Format("<FieldRef Name=\"{0}\"/>", field2.InternalName);
                            }
                            if (flist != null)
                            {
                                doc.LoadXml(schemaXml);
                                if (!string.IsNullOrEmpty(schemaXml = ProductPage.ApplyCore(list, schemaXml, doc, flist, ref expandGroups, false, filterHierarchy, null)))
                                {
                                    doc.LoadXml(schemaXml);
                                    XmlNode node = doc.DocumentElement.SelectSingleNode("Query");
                                    if (node != null)
                                    {
                                        if (filteredCaml != "")
                                        {
                                            string innerText = node.InnerXml.ToLower();
                                            string queryed = innerText.Substring(innerText.IndexOf("<where>") + 7, innerText.Length - innerText.IndexOf("<where>") - 7);
                                            query.Query = HttpUtility.HtmlDecode(queryed.Replace("</where>", filteredCaml));
                                        }
                                        //query.Query = node.InnerXml;
                                    }
                                }
                            }
                            collection = list.GetItems(query);
                        }
                    }
                    if (action2 == null)
                    {
                        action2 = (csvVal, suffix) => writer.Write(CsvEscape(csvVal, sep, is2 && "1".Equals(unixFlag)) + suffix);
                    }
                    csvWrite = action2;
                    if (flag4)
                    {
                        ExportRollup(writer, inst, opt, web, filterHierarchy, strArray, locale, csvWrite, sep);
                    }
                    else if (collection != null)
                    {
                        foreach (SPListItem item in ProductPage.TryEach<SPListItem>(collection))
                        {
                            sheet.CreateRow(currentRow);
                            for (int k = 0; k < strArray.Length; k++)
                            {
                                string str4;
                                flag3 = false;
                                if (!string.IsNullOrEmpty(str3) && (k == index))
                                {
                                    List<SPListItemVersion> list2 = new List<SPListItemVersion>();
                                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                    str4 = string.Empty;
                                    foreach (SPListItemVersion version in item.Versions)
                                    {
                                        list2.Add(version);
                                    }
                                    list2.Sort((Comparison<SPListItemVersion>)((one, two) => one.VersionLabel.CompareTo(two.VersionLabel)));
                                    if (flag2 && (list2.Count > 1))
                                    {
                                        flag3 = true;
                                        //csvWrite(string.Empty, "\r\n");
                                        for (int m = list2.Count - 1; m >= 0; m--)
                                        {
                                            for (int n = 0; n < strArray.Length; n++)
                                            {
                                                if (n == index)
                                                {
                                                    csvWrite(list2[m].VersionLabel, (n == (strArray.Length - 1)) ? "\r\n" : sep);
                                                    var cell = sheet.GetRow(currentRow).CreateCell(k);
                                                    cell.SetCellValue(list2[m].VersionLabel);
                                                }
                                                else
                                                {
                                                    str4 = string.Empty;
                                                    if (!"ID".Equals(str10 = ((num = strArray[n].IndexOf(':')) <= 0) ? strArray[n] : strArray[n].Substring(0, num)))
                                                    {
                                                        field = ProductPage.GetField(item, str10);
                                                        obj2 = string.Empty;
                                                        try
                                                        {
                                                            obj2 = list2[m][str10];
                                                        }
                                                        catch
                                                        {
                                                        }
                                                        str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                                    }
                                                    csvWrite(str4, (n == (strArray.Length - 1)) ? "\r\n" : sep);
                                                    var cell = sheet.GetRow(currentRow).CreateCell(k);
                                                    cell.SetCellValue(str4);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int num9 = 0; num9 < list2.Count; num9++)
                                        {
                                            string str5 = string.Concat(new object[] { list2[num9].VersionLabel, " ", list2[num9].Created.ToLocalTime(), "\r\n", list2[num9].CreatedBy.LookupValue });
                                            for (int num10 = 0; num10 < strArray.Length; num10++)
                                            {
                                                if (num10 != index)
                                                {
                                                    try
                                                    {
                                                        string str7;
                                                        string fieldName = ((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(0, num).Trim();
                                                        field = ProductPage.GetField(item, fieldName);
                                                        if (field != null)
                                                        {
                                                            fieldName = field.InternalName;
                                                        }
                                                        try
                                                        {
                                                            obj2 = list2[num9][fieldName];
                                                        }
                                                        catch
                                                        {
                                                            obj2 = null;
                                                        }
                                                        string str8 = GetExportValue(web, obj2, field, locale, mulSep);
                                                        if (!dictionary.TryGetValue(fieldName, out str7) || (str7 != str8))
                                                        {
                                                            dictionary[fieldName] = str8;
                                                            string str15 = str5;
                                                            str5 = str15 + "\r\n" + (((num = strArray[num10].IndexOf(':')) <= 0) ? strArray[num10] : strArray[num10].Substring(num + 1).Trim()) + ": " + str8;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                            str4 = str5 + "\r\n\r\n" + str4;
                                        }
                                    }
                                }
                                else
                                {
                                    field = ProductPage.GetField(item, str10 = ((num = strArray[k].IndexOf(':')) <= 0) ? strArray[k] : strArray[k].Substring(0, num));
                                    if (field != null)
                                    {
                                        obj2 = item[field.Id];
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj2 = item[str10];
                                        }
                                        catch
                                        {
                                            obj2 = null;
                                        }
                                    }
                                    str4 = GetExportValue(web, obj2, field, locale, mulSep);
                                }
                                if (!flag3)
                                {
                                    csvWrite(str4, (k == (strArray.Length - 1)) ? "\r\n" : sep);
                                    var cell = sheet.GetRow(currentRow).CreateCell(k);
                                    var cellType = cell.CellType;
                                    cell.SetCellValue(str4);
                                }
                            }
                            currentRow++;
                        }
                    }
                }
            }
            current.Response.Clear();
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            file.WriteTo(current.Response.OutputStream);
            current.Response.End();
        }

        /// <summary>
        /// Check is export from template
        /// </summary>
        /// <param name="webUrl"></param>
        /// <param name="exportListID"></param>
        /// <returns></returns>
        internal string IsExportFromTemp(string webUrl, string exportListID, out ExcelPackage excelPackage, out HSSFWorkbook hssfworkbook)
        {
            var tempPath = Server.MapPath("/_layouts/roxority_ExportZen/Template");
            var exportListName = "";
            excelPackage = null;
            hssfworkbook = null;
            var fileEx = "";
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list;
                    if ((string.IsNullOrEmpty(exportListID) || ((list = web.Lists[new Guid(exportListID)]) == null)))
                    {
                        throw new SPException(ProductPage.GetProductResource("Old_NoExportList", new object[0]));
                    }
                    else
                    {
                        exportListName = list.RootFolder.Name;
                    }
                }
            }
            if (exportListName != "")
            {
                var filePath = tempPath + "\\" + exportListName;
                if (File.Exists(filePath = filePath + ".xlsm"))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        excelPackage = new ExcelPackage();
                        excelPackage.Load(file);
                        fileEx = ".xlsm";
                    }
                }
                else if (File.Exists(filePath = filePath + ".xlsx"))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        excelPackage = new ExcelPackage();
                        excelPackage.Load(file);
                        fileEx = ".xlsx";
                    }
                }
                else if (File.Exists(filePath = filePath + ".xls"))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = new HSSFWorkbook(file);
                        fileEx = ".xls";
                    }
                }
            }
            return fileEx;
        }

        internal static void ExportRollup(TextWriter writer, IDictionary action, Hashtable opt, SPWeb web, Hashtable fht, string[] listColumns, string locale, roxority.Shared.Action<string, string> csvWrite, string sep)
        {
            string str;
            string mulSep = ProductPage.Config(ProductPage.GetContext(), "MultiSep");
            bool flag = (action["filter"] is bool) && ((bool) action["filter"]);
            if (refl == null)
            {
                refl = new Reflector(Assembly.Load("roxority_PeopleZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1"));
            }
            foreach (string str5 in new ArrayList(opt.Keys))
            {
                if (opt[str5] is string)
                {
                    opt[str5] = HttpUtility.UrlDecode(opt[str5] + string.Empty);
                }
            }
            if ((flag && (fht == null)) && opt.ContainsKey("f"))
            {
                fht = new Hashtable();
                fht["f"] = JSON.JsonDecode(HttpUtility.UrlDecode(opt["f"] + string.Empty));
                if (opt.ContainsKey("fa"))
                {
                    fht["fa"] = JSON.JsonDecode(HttpUtility.UrlDecode(opt["fa"] + string.Empty));
                }
            }
            object[] args = new object[0x13];
            args[0] = JsonSchemaManager.Bool(action["nopg"], false) ? 0 : ((int) opt["ps"]);
            args[1] = (int) opt["p"];
            args[2] = "1".Equals(opt["dty"]);
            args[3] = "1".Equals(opt["did"]);
            args[4] = opt["pr"] + string.Empty;
            args[5] = str = opt["spn"] + string.Empty;
            args[6] = string.IsNullOrEmpty(str) ? null : ((object) "1".Equals(opt["sd"]));
            args[7] = JsonSchemaManager.Bool(action["notb"], false) ? string.Empty : (opt["tpn"] + string.Empty);
            args[8] = JsonSchemaManager.Bool(action["notb"], false) ? string.Empty : opt["tv"];
            args[9] = str = opt["gpn"] + string.Empty;
            args[10] = string.IsNullOrEmpty(str) ? null : ((object) "1".Equals(opt["gd"]));
            args[11] = "1".Equals(opt["gb"]);
            args[12] = "1".Equals(opt["gs"]);
            args[13] = web;
            args[14] = opt["dsid"] + string.Empty;
            args[0x10] = flag ? fht : null;
            args[0x12] = new List<Exception>();
            using (IDisposable disposable = refl.New("roxority.Data.DataSourceConsumer", args) as IDisposable)
            {
                object obj3 = refl.Get(disposable, "DataSource", new object[0]);
                foreach (object obj4 in refl.Get(disposable, "List", new object[0]) as IEnumerable)
                {
                    for (int i = 0; i < listColumns.Length; i++)
                    {
                        int num;
                        string str2 = ((num = listColumns[i].IndexOf(':')) <= 0) ? listColumns[i] : listColumns[i].Substring(0, num);
                        object val = refl.Call(obj4, "Get", new System.Type[] { typeof(string), typeof(string), obj3.GetType().BaseType }, new object[] { str2, string.Empty, obj3 });
                        string t = GetExportValue(web, val, null, locale, (str2.ToLowerInvariant().Contains("birthday") || str2.ToLowerInvariant().Contains("xxxhiredate")) && !string.IsNullOrEmpty(val + string.Empty), mulSep);
                        csvWrite(t, (i == (listColumns.Length - 1)) ? "\r\n" : sep);
                    }
                }
            }
        }

        public static ICollection<IDictionary> GetActions(string fpath)
        {
            return JsonSchemaManager.GetInstances(fpath, "ExportActions");
        }

        internal static string GetExportValue(SPWeb exportWeb, object val, SPField field, string locale, string mulSep)
        {
            return GetExportValue(exportWeb, val, field, locale, false, mulSep);
        }

        internal static string GetExportValue(SPWeb exportWeb, object val, SPField field, string locale, bool forceDate, string mulSep)
        {
            string url;
            DateTime time;
            SPFieldDateTime time2 = field as SPFieldDateTime;
            SPFieldLookup lookup = field as SPFieldLookup;
            SPFieldMultiLineText text = field as SPFieldMultiLineText;
            if (field is SPFieldCalculated)
            {
                val = ((SPFieldCalculated) field).GetFieldValueAsText(val);
            }
            if (field is SPFieldUrl)
            {
                val = ((SPFieldUrl) field).GetFieldValue(val + string.Empty);
            }
            if (forceDate && (DateTime.TryParse(val + string.Empty, out time) || DateTime.TryParse(val + string.Empty, JsonSchemaManager.Property.Type.LocaleChoice.GetCulture(locale), DateTimeStyles.AllowWhiteSpaces, out time)))
            {
                val = time;
            }
            if ((val is DateTime) && ((time2 != null) || forceDate))
            {
                url = ((DateTime) val).ToString((time2 == null) ? "m" : ((time2.DisplayFormat == SPDateTimeFieldFormatType.DateOnly) ? "d" : "g"), JsonSchemaManager.Property.Type.LocaleChoice.GetCulture(locale));
            }
            else if (val is SPFieldUrlValue)
            {
                url = ((SPFieldUrlValue) val).Url;
            }
            else if (val is SPFieldLookupValueCollection)
            {
                url = string.Join(mulSep, ((SPFieldLookupValueCollection) val).ConvertAll<string>(lv => lv.LookupValue).ToArray());
            }
            else if (val is SPFieldLookupValue)
            {
                url = ((SPFieldLookupValue) val).LookupValue;
            }
            else
            {
                SPFieldMultiChoiceValue value2 = val as SPFieldMultiChoiceValue;
                if (value2 != null)
                {
                    url = string.Empty;
                    for (int i = 0; i < value2.Count; i++)
                    {
                        url = url + mulSep + value2[i];
                    }
                    if (url.Length > mulSep.Length)
                    {
                        url = url.Substring(mulSep.Length);
                    }
                }
                else if (((lookup = field as SPFieldLookup) != null) || (((text = field as SPFieldMultiLineText) != null) && text.RichText))
                {
                    url = val + string.Empty;
                    if (lookup != null)
                    {
                        int index = url.IndexOf(";#", StringComparison.InvariantCultureIgnoreCase);
                        if (index >= 0)
                        {
                            url = url.Substring(index + 2);
                        }
                        index = url.IndexOf(";#");
                        if (index > 0)
                        {
                            url = url.Substring(0, index);
                        }
                    }
                    url = htmlRegex.Replace(url.Replace("​", string.Empty).Replace("<br>", "\r\n").Replace("<BR>", "\r\n").Replace("<br/>", "\r\n").Replace("<BR/>", "\r\n").Replace("<br />", "\r\n").Replace("<BR />", "\r\n"), string.Empty);
                }
                else
                {
                    url = val + string.Empty;
                }
            }
            if (!url.StartsWith("<roxhtml/>"))
            {
                return url;
            }
            url = htmlRegex.Replace(url.Substring("<roxhtml/>".Length).Replace("​", string.Empty).Replace("<br>", "\r\n").Replace("<BR>", "\r\n").Replace("<br/>", "\r\n").Replace("<BR/>", "\r\n").Replace("<br />", "\r\n").Replace("<BR />", "\r\n"), string.Empty);
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(url);
                url = document.DocumentElement.InnerText;
            }
            catch
            {
            }
            return url.Replace('\r', ' ').Replace('\n', ' ').Trim();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string url = string.Empty;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                url = SPContext.Current.Web.Url;
            }
            catch
            {
                try
                {
                    url = HttpContext.Current.Request.RawUrl;
                }
                catch (Exception exception)
                {
                    throw new Exception("Could not determine current SPWeb URL: " + exception.Message);
                }
            }
            foreach (string str2 in new string[] { "exportlist", "rule", "lv", "sep", "unix", "f", "fj" })
            {
                try
                {
                    dictionary[str2] = base.Request.Form[str2] + string.Empty;
                }
                catch (Exception exception2)
                {
                    throw new Exception("Could not obtain GET argument " + str2 + ": " + exception2.Message);
                }
            }
            Export(writer, GetActions(base.Server.MapPath("/_layouts/roxority_ExportZen/schemas.json")), url.TrimEnd(new char[] { '/' }), dictionary["exportlist"], dictionary["rule"], dictionary["lv"], dictionary["sep"], dictionary["unix"], dictionary["f"], 0, dictionary["fj"]);
            base.Render(writer);
        }

        internal static string SafeName(string name)
        {
            int num;
            int num2;
            while (((num = name.IndexOf("{$", StringComparison.InvariantCultureIgnoreCase)) > 0) && ((num2 = name.IndexOf("$}", num + 2, StringComparison.InvariantCultureIgnoreCase)) > num))
            {
                name = name.Substring(0, num) + name.Substring(num2 + 2);
            }
            for (int i = 0; i < name.Length; i++)
            {
                if (!char.IsLetterOrDigit(name, i))
                {
                    name = name.Replace(name[i], '_');
                }
            }
            return name;
        }

        public void WriteLog()
        {
        }
    }
}