namespace roxority_FilterZen
{
    using Microsoft.SharePoint;
    using roxority.SharePoint;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using PanGu;
    using System.IO;
    using System.Xml;
    using System.Collections;
    using DsLib;

    public class AutoSuggestPage : Page
    {
        private static readonly string[] nonTextFieldTypes = new string[] { "AllDayEvent", "Attachments", "BusinessData", "Currency", "DateTime", "Guid", "Integer", "Number", "Recurrence", "ThreadIndex", "Threading" };
        private static readonly string[] unsupportedFieldTypes = new string[] { "Boolean", "ContentTypeId", "Counter", "Error", "MaxItems", "PageSeparator" };

        protected override void Render(HtmlTextWriter __w)
        {
            int num2 = 40;

            Guid guid;
            SPQuery query;
            bool flag = "1".Equals(base.Request["b"]);
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            int num = 40;
            int num3 = 0;
            int num4 = -1;
            int num5 = 0;
            string str = base.Request["fs"];
            string str6 = (base.Request["q"] + string.Empty).Trim();
            string str7 = (flag = flag ? true : ((str6.Length != 1) ? false : ProductPage.Config<bool>(ProductPage.GetContext(), "Auto1Begins"))) ? "BeginsWith" : "Contains";
            SPList list = null;
            SPView defaultView = null;
            SPListItemCollection collection = null;
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<SPField> list4 = new List<SPField>();


            if (!int.TryParse(ProductPage.Config(ProductPage.GetContext(), "AutoLimit"), out num2) || (num2 <= 1))
            {
                num2 = num;
            }
            //if (!int.TryParse(base.Request["limit"], out num2) || (num2 <= 1))
            //{
            //    num2 = num;
            //}
            try
            {
                list = SPContext.Current.Web.Lists[ProductPage.GetGuid(base.Request["l"])];
            }
            catch
            {
            }
            if (list == null)
            {
                __w.Write(ProductPage.GetProductResource("AutoNoList", new object[] { ProductPage.GetGuid(base.Request["l"]) }));
                goto Label_0876;
            }
            SPField field = ProductPage.GetField(list, base.Request["f"]);
            if (field == null)
            {
                __w.Write(ProductPage.GetProductResource("AutoNoField", new object[] { base.Request["f"] }));
                goto Label_0876;
            }
            if (string.IsNullOrEmpty(str6))
            {
                goto Label_0876;
            }
            foreach (string str8 in (base.Request["sf"] + string.Empty).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                SPField field2 = ProductPage.GetField(list, str8);
                if (field2 != null)
                {
                    list4.Add(field2);
                }
            }
            SPFieldCalculated calculated = field as SPFieldCalculated;
            SPFieldUrl url = field as SPFieldUrl;
            SPFieldLookup lookup = field as SPFieldLookup;
            if (!string.IsNullOrEmpty(base.Request["v"]) && !Guid.Empty.Equals(guid = ProductPage.GetGuid(base.Request["v"])))
            {
                try
                {
                    defaultView = list.Views[guid];
                }
                catch
                {
                }
            }
            if (defaultView == null)
            {
                defaultView = list.DefaultView;
            }
        Label_02A7:
            for (int queryNum = 0; queryNum < 2; queryNum++)
            {
                if (list2.Count >= num2)
                {
                    break;
                }

                query = new SPQuery(defaultView);
                query.Folder = list.RootFolder;
                query.IncludeAllUserPermissions = query.IncludeMandatoryColumns = query.IncludePermissions = query.IndividualProperties = query.AutoHyperlink = query.ExpandRecurrence = query.ExpandUserField = query.IncludeAttachmentVersion = query.IncludeAttachmentUrls = query.ItemIdQuery = false;
                query.ViewAttributes = "FailIfEmpty=\"FALSE\" RequiresClientIntegration=\"FALSE\" Threaded=\"FALSE\" Scope=\"Recursive\"";
                query.ViewFields = "<FieldRef Name=\"" + field.InternalName + "\"/>";
                foreach (SPField field3 in list4)
                {
                    query.ViewFields = query.ViewFields + "<FieldRef Name=\"" + field3.InternalName + "\"/>";
                }
                string str5 = (calculated != null) ? calculated.OutputType.ToString() : field.TypeAsString;
                flag2 = Array.IndexOf<string>(nonTextFieldTypes, field.TypeAsString) >= 0;
                Array.IndexOf<string>(unsupportedFieldTypes, field.TypeAsString);
                ArrayList alist = new ArrayList();
                if (queryNum == 0)
                {
                    alist.Add(str6);
                    query.Query = (flag4 ? string.Empty : ("<Where><" + str7 + "><FieldRef Name=\"" + field.InternalName + "\" /><Value Type=\"" + (flag3 ? "Text" : str5) + "\">" + str6 + "</Value></" + str7 + "></Where>")) + "<OrderBy><FieldRef Name=\"" + field.InternalName + "\"/></OrderBy>";
                }
                else
                {
                    try
                    {
                        Segment segment = new Segment();
                        Segment.Init(Server.MapPath("/_layouts/PanGuSegment/PanGu.xml"));
                        ICollection<WordInfo> words = segment.DoSegment(str6);
                        string queryCondition = "";
                        Dictionary<string, string> listCamls = new Dictionary<string, string>();
                        string testDrawCamlTree = "";
                        int i = 1;
                        foreach (WordInfo wordInfo in words)
                        {
                            if (wordInfo != null && wordInfo.Word.Replace(" ", "") != "" && wordInfo.Word.Length == 1)
                            {
                                alist.Add(wordInfo.Word);
                                testDrawCamlTree += "0-And:" + i + ";";
                                listCamls.Add(i.ToString(), "<" + str7 + "><FieldRef Name=\"" + field.InternalName + "\" /><Value Type=\"" + (flag3 ? "Text" : str5) + "\">" + wordInfo.Word + "</Value></" + str7 + ">");
                                i++;
                            }
                        }

                        if (listCamls.Count > 0)
                        {
                            query.Query = (flag4 ? string.Empty : "<Where>" + BSTUtils.DrawCamlTree(testDrawCamlTree.TrimEnd(';'), listCamls) + "</Where>") + "<OrderBy><FieldRef Name=\"" + field.InternalName + "\"/></OrderBy>";
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                
                query.RowLimit = (uint)100000;
                collection = list.GetItems(query);
                if (collection != null)
                {
                    num4 = 0;
                    foreach (SPListItem item in ProductPage.TryEach<SPListItem>(collection))
                    {
                        string lookupValue;
                        string str3 = string.Empty;
                        string str4 = string.Empty;
                        foreach (SPField field4 in list4)
                        {
                            if ((field4 != null) && !Guid.Empty.Equals(field4.Id))
                            {
                                try
                                {
                                    str4 = item[field4.Id] + string.Empty;
                                }
                                catch
                                {
                                    str4 = string.Empty;
                                }
                            }
                            if (!string.IsNullOrEmpty(str4))
                            {
                                string str11 = str3;
                                str3 = str11 + "<div class=\"rox-acsecfield\"><i>" + base.Server.HtmlEncode(field4.Title) + ":</i> " + str4.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ") + "</div>";
                            }
                        }
                        num4++;
                        string[] strArray = null;
                        object obj2 = ProductPage.GetFieldVal(item, field, true);
                        if (!string.IsNullOrEmpty(lookupValue = (obj2 == null) ? string.Empty : field.GetFieldValueAsText(obj2)))
                        {
                            if (!IsContains(lookupValue.Trim(),alist))
                            {
                                lookupValue = string.Empty;
                            }
                            else if (lookup != null)
                            {
                                SPFieldUserValueCollection values2 = obj2 as SPFieldUserValueCollection;
                                if (values2 != null)
                                {
                                    lookupValue = string.Empty;
                                    strArray = values2.ConvertAll<string>(uv => uv.LookupValue).ToArray();
                                }
                                else
                                {
                                    SPFieldLookupValueCollection values = obj2 as SPFieldLookupValueCollection;
                                    if (values != null)
                                    {
                                        lookupValue = string.Empty;
                                        strArray = values.ConvertAll<string>(lv => lv.LookupValue).ToArray();
                                    }
                                    else
                                    {
                                        SPFieldLookupValue value2 = obj2 as SPFieldLookupValue;
                                        if (value2 != null)
                                        {
                                            lookupValue = value2.LookupValue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SPFieldUrlValue value3;
                                if ((url != null) && ((value3 = obj2 as SPFieldUrlValue) != null))
                                {
                                    lookupValue = value3.Url;
                                }
                                else if (field.Type == SPFieldType.MultiChoice)
                                {
                                    strArray = (obj2 + string.Empty).Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                }
                                else if (!string.IsNullOrEmpty(str))
                                {
                                    strArray = (obj2 + string.Empty).Split(new string[] { str }, StringSplitOptions.RemoveEmptyEntries);
                                }
                            }
                        }
                        if (!IsContains(lookupValue.Trim(),alist))
                        {
                            lookupValue = string.Empty;
                        }
                        if (strArray != null)
                        {
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                if (!IsContains(strArray[i].Trim(),alist))
                                {
                                    strArray[i] = string.Empty;
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(lookupValue))
                        {
                            strArray = new string[] { lookupValue };
                        }
                        else
                        {
                            strArray = new string[0];
                        }
                        if (strArray.Length > 0)
                        {
                            num3++;
                            foreach (string str9 in strArray)
                            {
                                if ((!string.IsNullOrEmpty(str9) && (IsContains(str9.Trim(),alist))) && !list2.Contains(str9.Trim()))
                                {
                                    list3.Add(str3);
                                    list2.Add(str9.Trim());
                                }
                            }
                        }
                        if (list2.Count >= num2)
                        {
                            break;
                        }
                    }
                }
            }
            if (num4 < (flag2 ? 1 : 0))
            {
                if (flag2 && !flag3)
                {
                    flag3 = true;
                    goto Label_02A7;
                }
                if (!flag4)
                {
                    flag4 = true;
                    goto Label_02A7;
                }
            }
        Label_0876:
            //list2.Sort();
            foreach (string str10 in list2)
            {
                __w.Write(str10.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ") + list3[num5++] + "\r\n");
            }
        }

        protected XmlNode GroupQueryConditions(List<string> listCamls)
        {
            DictionaryEntry dice = new DictionaryEntry();
            string[] strArray = new string[] { string.Empty, string.Empty };
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("And");
            return node;
        }

        public bool IsContains(string body, ArrayList alist)
        {
            if (body == null || string.IsNullOrEmpty(body) || alist.Count == 0)
                return false;
            bool isContains = true;
            foreach (string str in alist)
            {
                if (!body.ToLower().Contains(str.ToLower()))
                {
                    isContains = false;
                    break;
                }
            }
            return isContains;
        }
    }
}