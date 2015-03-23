namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebControls;
    using Microsoft.Win32;
    using roxority.Shared;
    using roxority.SharePoint.JsonSchemaPropertyTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.UI;
    using System.Xml;
    using System.Runtime.Serialization;
    using DsLib;
    using System.Linq;

    public class ProductPage : Page
    {
        private SPWebApplication adminApp;
        public static readonly string[] AdminHelpTopicIDs = new string[0];
        private SPSite adminSite;
        protected internal string alertMessage = string.Empty;
        private static List<CultureInfo> allCultures = null;
        private static List<CultureInfo> allSpecificCultures = null;
        public static readonly System.Reflection.Assembly Assembly = typeof(ProductPage).Assembly;
        public static readonly string AssemblyName = (Assembly.ToString().Contains(",") ? Assembly.ToString().Substring(0, Assembly.ToString().IndexOf(',')) : Assembly.ToString());
        internal static Dictionary<string, string> cfgGroups = null;
        [ThreadStatic]
        public static SPSite currentSite = null;
        public static readonly string DefaultTopicID = "intro";
        private static readonly List<KeyValuePair<int, string>> editions = new List<KeyValuePair<int, string>>();
        [ThreadStatic]
        public static bool Elevated = false;
        [ThreadStatic]
        protected internal static string errorMessage = string.Empty;
        private SPFarm farm;
        private static MethodInfo farmMethod = null;
        private static bool farmMethodTried = false;
        internal const string FILTERZEN_TYPENAME = "roxority_FilterZen.roxority_FilterWebPart, roxority_FilterZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1";
        public const string FORMAT_CAML_VIEWFIELD = "<FieldRef Name=\"{0}\"/>";
        private static readonly BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Remoting));
        internal static readonly BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Persistence));
        public static string GoogSrc = null;
        public static readonly string[] HelpTopicIDs = new string[] { "intro", "eula" };
        private static bool? is14 = null;
        private bool? isAnyAdmin = null;
        private bool? isAppAdmin = null;
        public static bool isEnabled = true;
        private bool? isFarmAdmin = null;
        public bool IsFarmError;
        private bool? isSiteAdmin = null;
        protected internal static readonly int l1 = 0xafc8;
        protected internal static readonly int l2 = 0x2b;
        protected internal static readonly int l3 = 60;
        protected internal static readonly int l4 = 8;
        private static string mapkey = null;
        private static string name = null;
        protected internal static byte[] os = new List<string>(GetProductResource("_ProductID", new object[0]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ConvertAll<byte>(value => byte.Parse(value.Trim())).ToArray();
        public string pageTitle = string.Empty;
        private static string pname = null;
        public Exception postEx;
        public static readonly ResourceManager ProductResources = new ResourceManager(AssemblyName + ".Properties.Resources", Assembly);
        private static readonly Regex regexGuidPattern = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
        internal static readonly Dictionary<string, ResourceManager> resMans = new Dictionary<string, ResourceManager>();
        public static readonly ResourceManager Resources = new ResourceManager(AssemblyName + ".Properties.roxority_Shared", Assembly);
        public readonly Random Rnd = new Random();
        public const string SEPARATOR = ";#";
        public static readonly string[] SPOddFieldNames = new string[] { 
            "LinkDiscussionTitle", "LinkDiscussionTitleNoMenu", "LinkTitle", "LinkTitleNoMenu", "Attachments", "BaseName", "EncodedAbsUrl", "Edit", "SelectTitle", "PermMask", "UniqueId", "ScopeId", "_EditMenuTableStart", "_EditMenuTableEnd", "LinkFilenameNoMenu", "LinkFilename", 
            "ServerUrl", "FileLeafRef", "ParentLeafName", "DocIcon"
         };
        protected internal static readonly string tk = "t";
        private static readonly Dictionary<string, object> usersDic = new Dictionary<string, object>();
        private static string wlabel = null;

        static ProductPage()
        {
            HelpTopicIDs = GetProductResource("_HelpTopics", new object[0]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            AdminHelpTopicIDs = GetProductResource("_AdminHelpTopics", new object[0]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            DefaultTopicID = (HelpTopicIDs.Length > 0) ? HelpTopicIDs[0] : string.Empty;
            formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
        }

        /// <summary>
        /// To filter special value, such as (number with ',') 
        /// </summary>
        /// <param name="flist"></param>
        /// <returns></returns>
        public static ArrayList FilterSpecialValue(SPList list,ArrayList flist) //modified by:lhan
        {
           // ArrayList list = flist.;
            foreach (Hashtable hashtable in flist)
            {
                string fieldName = hashtable["k"] + string.Empty;
                foreach (Hashtable hashtable2 in (ArrayList)((Hashtable)hashtable["v"])["k"])
                {
                    string fieldCamlOperator = hashtable2["v"] + string.Empty;
                    string fieldValue = hashtable2["k"] + string.Empty;
                    if (!string.IsNullOrEmpty(fieldName) && fieldName != "" && !string.IsNullOrEmpty(fieldValue) && fieldValue != "")
                    {
                        SPField field = GetField(list, fieldName);
                        if ((field as SPFieldNumber)!=null ||(field as SPFieldCurrency)!=null)
                        {
                            hashtable2["k"]=FilterSpecialValueForNumber(fieldValue);
                        }                        
                    }
                }
            }

            return flist;
        }

        /// <summary>
        /// To filter special value for number field
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FilterSpecialValueForNumber(string value) //modified by:lhan
        {
            if (ConvertHelper.ConvertInt(value) == 0)
            {
                value = System.Text.RegularExpressions.Regex.Replace(value, @"[^0-9.]", "");
            }
            return value;
        }

        /// <summary>
        /// To filter special value for lookup field
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FilterSpecialValueForLookup(string value) //modified by:lhan
        {
            if (ConvertHelper.ConvertString(value) != "")
            {
                value = value.Replace("\"", "\\\"").Replace("\'","\\\'");
            }
            return value;
        }

        internal static string ApplyCore(SPList list, string viewXml, XmlDocument doc, ArrayList flist, ref bool expandGroups, bool outerOr, Hashtable filterHierarchy, string[] camlSourceFilters, List<roxority_FilterZen.FilterBase.Multi.MultiTextCollection> filterConditions)
        {
            XmlAttribute namedItem;
            string str2 = string.Empty;
            int result = -1;
            XmlDocument document = new XmlDocument();
            Dictionary<string, string> subCamls = new Dictionary<string, string>();
            XmlNode newChild = null;
            XmlNode node4 = null;
            XmlNode node5 = null;
            Converter<DictionaryEntry, XmlNode> createFromHierarchy = null;

            flist=FilterSpecialValue(list, flist); //modified by:lhan

            int position = 0;

            createFromHierarchy = delegate(DictionaryEntry entry)
            {
                string[] strArray = new string[] { string.Empty, string.Empty };
                ArrayList list111 = (ArrayList) entry.Value;
                XmlNode node = doc.CreateElement("OR".Equals(entry.Key) ? "Or" : "And");
                for (int j = 0; j < 2; j++)
                {
                    string str;

                    //if (!string.IsNullOrEmpty(str = list111[j] as string) && subCamls.ContainsKey(str))
                    //{
                    //    strArray[j] = subCamls[list111[j] as string];
                    //}
                    if (!string.IsNullOrEmpty(str = list111[j] as string) && subCamls.Count>j)  //modify by:lhan
                    {
                        int i = 0;
                        foreach (string val in subCamls.Values)
                        {
                            if (i == position)
                            {
                                strArray[j] = val;
                                position++;
                                break;
                            }
                            i++;
                        }
                        
                    }
                    else
                    {
                        Hashtable hashtable;
                        if (((hashtable = list111[j] as Hashtable) != null) && (hashtable.Count == 1))
                        {
                            foreach (DictionaryEntry entry2 in hashtable)
                            {
                                XmlNode node2;
                                if (((node2 = createFromHierarchy(entry2)) != null) && node2.HasChildNodes)
                                {
                                    strArray[j] = ((node2.ChildNodes.Count == 1) ? (node2.FirstChild) : (node2)).OuterXml;
                                }
                            }
                        }
                    }
                }
                node.InnerXml = strArray[0] + strArray[1];
                if (!node.HasChildNodes)
                {
                    return null;
                }
                if (node.ChildNodes.Count != 1)
                {
                    return node;
                }
                return node.FirstChild;
            };
            if (doc.DocumentElement == null)
            {
                doc.LoadXml("<View/>");
            }
            XmlNode node111 = doc.DocumentElement.SelectSingleNode("Query");
            if (node111 == null)
            {
                node111 = doc.DocumentElement.AppendChild(doc.CreateElement("Query"));
            }
            XmlNode oldChild = node111.SelectSingleNode("Where");
            if (oldChild == null)
            {
                oldChild = node111.AppendChild(doc.CreateElement("Where"));
            }
            newChild = oldChild.FirstChild;

            foreach (Hashtable hashtable in flist)
            {
                XmlNode node6;
                SPField field;
                node5 = null;
                int num = 0;
                string fieldName = hashtable["k"] + string.Empty;
                string fieldCamlOperator = "";
                string fieldValue = "";
                Dictionary<string, CamlOperator> dictionary2 = new Dictionary<string, CamlOperator>();
                Dictionary<string, CamlOperator> dictionary = new Dictionary<string, CamlOperator>();
                bool flag = (bool) ((Hashtable) hashtable["v"])["v"];
                foreach (Hashtable hashtable2 in (ArrayList) ((Hashtable) hashtable["v"])["k"])
                {
                    fieldCamlOperator = hashtable2["v"] + string.Empty;
                    fieldValue = hashtable2["k"] + string.Empty;
                    if (dictionary.ContainsKey(hashtable2["k"] + string.Empty))
                    {
                        dictionary[hashtable2["k"] + string.Empty] = CamlOperator.Eq;
                    }
                    else
                    {
                        dictionary[hashtable2["k"] + string.Empty] = (CamlOperator) System.Enum.Parse(typeof(CamlOperator), hashtable2["v"] + string.Empty, true);
                    }
                }
                foreach (KeyValuePair<string, CamlOperator> pair in dictionary)
                {
                    if (((CamlOperator) pair.Value) > CamlOperator.Contains)
                    {
                        if (((CamlOperator) pair.Value) == CamlOperator.RangeGeLe)
                        {
                            dictionary2[pair.Key] = (dictionary.Count == 1) ? CamlOperator.Eq : ((num == 0) ? CamlOperator.Geq : CamlOperator.Leq);
                        }
                        else if (((CamlOperator) pair.Value) == CamlOperator.RangeGeLt)
                        {
                            dictionary2[pair.Key] = (dictionary.Count == 1) ? CamlOperator.Eq : ((num == 0) ? CamlOperator.Geq : CamlOperator.Lt);
                        }
                        else if (((CamlOperator) pair.Value) == CamlOperator.RangeGtLe)
                        {
                            dictionary2[pair.Key] = (dictionary.Count == 1) ? CamlOperator.Eq : ((num == 0) ? CamlOperator.Gt : CamlOperator.Leq);
                        }
                        else if (((CamlOperator) pair.Value) == CamlOperator.RangeGtLt)
                        {
                            dictionary2[pair.Key] = (dictionary.Count == 1) ? CamlOperator.Eq : ((num == 0) ? CamlOperator.Gt : CamlOperator.Lt);
                        }
                    }
                    num++;
                }
                foreach (KeyValuePair<string, CamlOperator> pair2 in dictionary2)
                {
                    dictionary[pair2.Key] = pair2.Value;
                }
                try
                {
                    field = GetField(list, fieldName);
                    if (field != null)
                    {
                        fieldName = field.InternalName;
                    }
                }
                catch
                {
                    field = null;
                }
                foreach (KeyValuePair<string, CamlOperator> pair3 in dictionary)
                {
                    string str3 = null;
                    bool flag2 = false;
                    if ((camlSourceFilters != null) && (Array.IndexOf<string>(camlSourceFilters, fieldName) >= 0))
                    {
                        document.LoadXml(pair3.Key);
                        node4 = doc.ImportNode(document.DocumentElement, true);
                    }
                    else
                    {
                        node4 = doc.CreateElement((((CamlOperator) pair3.Value) == CamlOperator.Member) ? "Membership" : pair3.Value.ToString());
                        if (((CamlOperator) pair3.Value) == CamlOperator.Member)
                        {
                            string str6;
                            node4.Attributes.Append(node4.OwnerDocument.CreateAttribute("Type")).Value = "SPGroup";
                            if (!int.TryParse(str6 = pair3.Key, out result))
                            {
                                SPGroupCollection[] groupsArray = new SPGroupCollection[2];
                                try
                                {
                                    groupsArray[0] = SPContext.Current.Web.Groups;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    groupsArray[1] = SPContext.Current.Web.SiteGroups;
                                }
                                catch
                                {
                                }
                                foreach (SPGroupCollection groups in groupsArray)
                                {
                                    if (groups != null)
                                    {
                                        foreach (SPGroup group in TryEach<SPGroup>(groups))
                                        {
                                            if (group.Name == str6)
                                            {
                                                result = group.ID;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            node4.Attributes.Append(node4.OwnerDocument.CreateAttribute("ID")).Value = result.ToString();
                        }
                        node4.AppendChild(doc.CreateElement("FieldRef")).Attributes.SetNamedItem(doc.CreateAttribute("Name")).Value = fieldName;
                        if (((CamlOperator) pair3.Value) != CamlOperator.Member)
                        {
                            node4.AppendChild(doc.CreateElement("Value")).Attributes.SetNamedItem(doc.CreateAttribute("Type")).Value = (field == null) ? "Text" : field.TypeAsString;
                        }
                        if (field != null)
                        {
                            string outerXml;
                            bool flag3;
                            SPFieldNumber number;
                            SPFieldCalculated calculated = field as SPFieldCalculated;
                            decimal num3 = -1M;
                            if (flag3 = ((((number = field as SPFieldNumber) != null) && number.ShowAsPercentage) || (((calculated != null) && (calculated.OutputType == SPFieldType.Number)) && calculated.ShowAsPercentage)) && (decimal.TryParse(pair3.Key, out num3) || (pair3.Key.EndsWith("%") && decimal.TryParse(pair3.Key, out num3))))
                            {
                                if (num3 > 1M)
                                {
                                    str3 = (num3 / 100M).ToString();
                                }
                                else
                                {
                                    str3 = ConvertNumberForCaml(num3.ToString());
                                }
                            }
                            if (((field.Type == SPFieldType.Invalid) && (field.FieldTypeDefinition != null)) && !string.IsNullOrEmpty(field.FieldTypeDefinition.BaseRenderingTypeName))
                            {
                                node4.LastChild.Attributes.GetNamedItem("Type").Value = str2 = field.FieldTypeDefinition.BaseRenderingTypeName;
                            }
                            if ((field.Type == SPFieldType.Boolean) && string.IsNullOrEmpty(pair3.Key))
                            {
                                outerXml = node4.FirstChild.OuterXml;
                                string str5 = node4.OuterXml;
                                node4 = node4.OwnerDocument.CreateElement("Or");
                                node4.AppendChild(node4.OwnerDocument.CreateElement("IsNull"));
                                node4.FirstChild.InnerXml = outerXml;
                                node4.InnerXml = node4.InnerXml + str5;
                            }
                            if (flag2 = ((str2 == "DateTime") || (field.Type == SPFieldType.DateTime)) || ((calculated != null) && (calculated.OutputType == SPFieldType.DateTime)))
                            {
                                if (string.IsNullOrEmpty(pair3.Key))
                                {
                                    outerXml = node4.FirstChild.OuterXml;
                                    node4 = node4.OwnerDocument.CreateElement("IsNull");
                                    node4.InnerXml = outerXml;
                                }
                                else
                                {
                                    str3 = SPUtility.CreateISO8601DateTimeFromSystemDateTime(ConvertStringToDate(pair3.Key));
                                    namedItem = node4.LastChild.Attributes.GetNamedItem("IncludeTimeValue") as XmlAttribute;
                                    if (namedItem == null)
                                    {
                                        namedItem = node4.LastChild.Attributes.Append(doc.CreateAttribute("IncludeTimeValue"));
                                    }
                                    namedItem.Value = "FALSE";
                                }
                                if (calculated != null)
                                {
                                    node4.LastChild.Attributes.GetNamedItem("Type").Value = calculated.OutputType.ToString();
                                }
                            }
                            if (((!flag2 && !flag3) && ((calculated != null) || (str2 == "Calculated"))) && ((((CamlOperator) pair3.Value) != CamlOperator.Eq) && (((CamlOperator) pair3.Value) != CamlOperator.Neq)))
                            {
                                if ((((CamlOperator) pair3.Value) == CamlOperator.BeginsWith) || (((CamlOperator) pair3.Value) == CamlOperator.Contains))
                                {
                                    node4.LastChild.Attributes.GetNamedItem("Type").Value = "Text";
                                }
                                else
                                {
                                    node4.LastChild.Attributes.GetNamedItem("Type").Value = (calculated == null) ? "Number" : calculated.OutputType.ToString();
                                }
                            }
                        }
                        else
                        {
                            node4.LastChild.InnerText = pair3.Key;
                        }
                        if (((((CamlOperator) pair3.Value) != CamlOperator.Member) && (field != null)) && ((field.Type != SPFieldType.Boolean) || !string.IsNullOrEmpty(pair3.Key)))
                        {
                            node4.LastChild.InnerText = string.IsNullOrEmpty(str3) ? pair3.Key : str3;
                        }
                        if (((namedItem = node4.FirstChild.Attributes.GetNamedItem("Name") as XmlAttribute) != null) && (Array.IndexOf<string>(new string[] { "LinkFilename", "LinkFilenameNoMenu" }, namedItem.Value) >= 0))
                        {
                            namedItem.Value = "FileLeafRef";
                        }
                    }
                    if (node5 == null)
                    {
                        node5 = node4;
                    }
                    else
                    {
                        node6 = node5;
                        node5 = doc.CreateElement(flag ? "And" : "Or");
                        node5.AppendChild(node6);
                        node5.AppendChild(node4);
                    }
                }
                if (node5 != null)
                {
                    //if (fieldCamlOperator != "" && !string.IsNullOrEmpty(fieldCamlOperator) && fieldValue != "" && !string.IsNullOrEmpty(fieldValue))
                    //{
                    //    subCamls[fieldName + fieldCamlOperator + fieldValue+Guid.NewGuid()] = node5.OuterXml;
                    //}
                    //else
                    //{
                    //    subCamls[fieldName] = node5.OuterXml;
                    //}
                    try
                    {
                        var subCamlsCount = subCamls.Count;
                        if (filterConditions[subCamlsCount].Field == fieldName && filterConditions[subCamlsCount].Val == fieldValue && filterConditions[subCamlsCount].Op == fieldCamlOperator)
                        {
                            subCamls[(Convert.ToInt32(filterConditions[subCamlsCount].Num)+1).ToString()] = node5.OuterXml; //modified by:lhan
                        }
                        else
                        {
                            subCamls[Guid.NewGuid().ToString()] = node5.OuterXml; //modified by:lhan
                        }
                    }
                    catch (Exception e)
                    {
                        subCamls[Guid.NewGuid().ToString()] = node5.OuterXml; //modified by:lhan
                    }
                    
                    if (newChild == null)
                    {
                        newChild = node5;
                    }
                    else
                    {
                        node6 = newChild;
                        newChild = doc.CreateElement(outerOr ? "Or" : "And");
                        newChild.AppendChild(node6);
                        newChild.AppendChild(node5);
                    }
                }
            }
            if ((filterHierarchy != null) && (filterHierarchy.Count == 1))
            {
                foreach (DictionaryEntry entry in filterHierarchy)
                {
                    newChild = createFromHierarchy(entry);
                }   
            }

            if (filterConditions != null && filterConditions.Count >= 2)
            {
                try
                {
                    string reArrageFilters = "";
                    foreach (var filterCondition in filterConditions)
                    {
                        var lop = from fc in filterConditions where fc.ParentNum == filterCondition.ParentNum select new { fc.Lop };
                        reArrageFilters += (Convert.ToInt32(filterCondition.ParentNum) + 1) + "-" + lop.First().Lop + ":" + (Convert.ToInt32(filterCondition.Num) + 1) + ";";
                    }
                    string reArrageCamls = BSTUtils.DrawCamlTree(reArrageFilters.TrimEnd(';'), subCamls).Trim();
                    XmlElement reArrageCamlsXml;
                    if (reArrageCamls.IndexOf("<And>") == 0)
                    {
                        reArrageCamlsXml = doc.CreateElement("And");
                        reArrageCamls = reArrageCamls.Substring(5);
                        reArrageCamls = reArrageCamls.Substring(0, reArrageCamls.Length - 6);
                        reArrageCamlsXml.InnerXml = reArrageCamls;
                    }
                    else
                    {
                        reArrageCamlsXml = doc.CreateElement("Or");

                        reArrageCamls = reArrageCamls.Substring(4);
                        reArrageCamls = reArrageCamls.Substring(0, reArrageCamls.Length - 5);
                        reArrageCamlsXml.InnerXml = reArrageCamls;
                    }
                    newChild = reArrageCamlsXml;
                }
                catch (Exception e)
                { 
                
                }
            }

            if (newChild != null)
            {
                oldChild.RemoveAll();
                oldChild.AppendChild(newChild);
            }
            else if (oldChild != null)
            {
                node111.RemoveChild(oldChild);
            }
            if (expandGroups)
            {
                expandGroups = false;
                if ((((newChild = doc.DocumentElement.SelectSingleNode("Query/GroupBy")) != null) && ((namedItem = newChild.Attributes.GetNamedItem("Collapse") as XmlAttribute) != null)) && namedItem.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    namedItem.Value = "FALSE";
                    expandGroups = true;
                }
            }
            if (list == null)
            {
                newChild = doc.SelectSingleNode("//Where");
                if (newChild == null)
                {
                    return string.Empty;
                }

                doc.LoadXml(newChild.OuterXml);
            }
            return doc.DocumentElement.OuterXml;
        }

        internal static void Check(string cap, bool noTrial)
        {
            LicInfo info = LicInfo.Get(null);
            if (noTrial && string.IsNullOrEmpty(info.name))
            {
                throw new Exception(GetResource("NopeTrial", new object[] { GetProductResource("Cap_" + cap, new object[0]) }));
            }
            if (info.expired)
            {
                throw new Exception(GetResource("NopeExpired", new object[] { GetProductResource("Cap_" + cap, new object[0]) }));
            }
        }

        public static string CheckVersion(SPContext context)
        {
            return null;
        }

        public static T Config<T>(SPContext context, string key) where T: struct
        {
            string scope = ConfigScope(ref key);
            string k = AssemblyName + "_" + key;
            bool isLower = false;
            T val = default(T);
            if (context == null)
            {
                context = GetContext();
            }
            if ((key[0] == '_') || LicEdition(context, (LicInfo) null, 2))
            {
                Elevate(delegate {
                    object obj2 = null;
                    string str = null;
                    SPFarm farm = GetFarm(context);
                    SPSite site = Elevated ? OpenSite(context) : GetSite(context);
                    SPWeb web = null;
                    SPSecurity.CatchAccessDeniedException = false;
                    if (site != null)
                    {
                        try
                        {
                            site.CatchAccessDeniedException = false;
                            if (scope != "farm")
                            {
                                web = site.OpenWeb(site.RootWeb.ID);
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            if (!Elevated)
                            {
                                throw;
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (web != null)
                    {
                        using (web)
                        {
                            if ((web.AllProperties != null) && ((web.AllProperties.ContainsKey(k) && !string.IsNullOrEmpty(str = web.AllProperties[k] + string.Empty)) || (isLower = web.AllProperties.ContainsKey(k.ToLowerInvariant()) && !string.IsNullOrEmpty(str = web.AllProperties[k.ToLowerInvariant()] + string.Empty))))
                            {
                                try
                                {
                                    val = (T) Convert.ChangeType(str, typeof(T));
                                }
                                catch
                                {
                                }
                            }
                            else if (((farm != null) && (farm.Properties != null)) && ((farm.Properties.ContainsKey(k) && ((obj2 = farm.Properties[k]) != null)) || (isLower = farm.Properties.ContainsKey(k.ToLowerInvariant()) && ((obj2 = farm.Properties[k.ToLowerInvariant()]) != null))))
                            {
                                try
                                {
                                    val = (obj2 is T) ? ((T) obj2) : ((T) Convert.ChangeType(obj2, typeof(T)));
                                }
                                catch
                                {
                                }
                            }
                            goto Label_02BE;
                        }
                    }
                    if (((farm != null) && (farm.Properties != null)) && ((farm.Properties.ContainsKey(k) && ((obj2 = farm.Properties[k]) != null)) || (isLower = farm.Properties.ContainsKey(k.ToLowerInvariant()) && ((obj2 = farm.Properties[k.ToLowerInvariant()]) != null))))
                    {
                        try
                        {
                            val = (obj2 is T) ? ((T) obj2) : ((T) Convert.ChangeType(obj2, typeof(T)));
                        }
                        catch
                        {
                        }
                    }
                Label_02BE:
                    if (Elevated && (site != null))
                    {
                        site.Dispose();
                    }
                }, true);
            }
            return val;
        }

        public static string Config(SPContext context, string key)
        {
            string scope = ConfigScope(ref key);
            string k = AssemblyName + "_" + key;
            string val = (key == "_lang") ? string.Empty : GetProductResource("CfgSettingDef_" + key, new object[0]);
            bool isLower = false;
            if (context == null)
            {
                context = GetContext();
            }
            if ((key[0] == '_') || LicEdition(context, (LicInfo) null, 2))
            {
                Elevate(delegate {
                    SPFarm farm = GetFarm(context);
                    SPWeb web = null;
                    SPSite site = Elevated ? OpenSite(context) : GetSite(context);
                    SPSecurity.CatchAccessDeniedException = false;
                    if (site != null)
                    {
                        try
                        {
                            site.CatchAccessDeniedException = false;
                            if (scope != "farm")
                            {
                                web = site.OpenWeb(site.RootWeb.ID);
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            if (!Elevated)
                            {
                                throw;
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (web != null)
                    {
                        using (web)
                        {
                            if ((web.AllProperties != null) && (web.AllProperties.ContainsKey(k) || (isLower = web.AllProperties.ContainsKey(k.ToLowerInvariant()))))
                            {
                                val = web.AllProperties[isLower ? k.ToLowerInvariant() : k] + string.Empty;
                            }
                            else if ((((scope != "site") && (farm != null)) && (farm.Properties != null)) && (farm.Properties.ContainsKey(k) || (isLower = farm.Properties.ContainsKey(k.ToLowerInvariant()))))
                            {
                                val = farm.Properties[isLower ? k.ToLowerInvariant() : k] + string.Empty;
                            }
                            goto Label_0221;
                        }
                    }
                    if ((((scope != "site") && (farm != null)) && (farm.Properties != null)) && (farm.Properties.ContainsKey(k) || (isLower = farm.Properties.ContainsKey(k.ToLowerInvariant()))))
                    {
                        val = farm.Properties[isLower ? k.ToLowerInvariant() : k] + string.Empty;
                    }
                Label_0221:
                    if (Elevated && (site != null))
                    {
                        site.Dispose();
                    }
                }, true);
            }
            if (val != null)
            {
                return val;
            }
            return string.Empty;
        }

        public static void Config<T>(SPContext context, string key, T value, bool siteUpdate)
        {
            string k = ((AssemblyName == "Deploy") ? "Yukka_GreenBox" : AssemblyName) + "_" + key;
            if (key == "_lang")
            {
                farmCulture = null;
                siteCulture = null;
            }
            Elevate(delegate {
                SPWeb web;
                if (context == null)
                {
                    context = GetContext();
                }
                SPSite site = Elevated ? OpenSite(context) : GetSite(context);
                SPFarm farm = GetFarm(context);
                SPSecurity.CatchAccessDeniedException = false;
                if (site != null)
                {
                    site.AllowUnsafeUpdates = true;
                    site.CatchAccessDeniedException = false;
                }
                if (context != null)
                {
                    try
                    {
                        context.Web.AllowUnsafeUpdates = true;
                    }
                    catch
                    {
                    }
                }
                if ((!siteUpdate && (farm != null)) && (farm.Properties != null))
                {
                    try
                    {
                        farm.Properties[k] = value;
                        farm.Update(true);
                        goto Label_010C;
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    catch (SecurityException)
                    {
                        throw;
                    }
                    catch
                    {
                        goto Label_010C;
                    }
                }
                if ((siteUpdate && (site != null)) && (((web = site.RootWeb) != null) && (web.AllProperties != null)))
                {
                    try
                    {
                        web.AllowUnsafeUpdates = true;
                        web.AllProperties[k] = value.ToString();
                        web.Update();
                    }
                    catch
                    {
                    }
                }
            Label_010C:
                if ((site != null) && Elevated)
                {
                    site.Dispose();
                }
            }, true);
        }

        public static bool ConfigHasSiteValue(SPContext context, SPSite site, string key)
        {
            bool flag3;
            string str = AssemblyName + "_" + key;
            bool flag = false;
            bool flag2 = site == null;
            if (flag2)
            {
                site = Elevated ? OpenSite(context) : GetSite(context);
            }
            if ((site != null) && (site.RootWeb == null))
            {
                site = null;
            }
            if (site == null)
            {
                return false;
            }
            SPFarm farm = GetFarm(context);
            try
            {
                using (SPWeb web = site.OpenWeb(site.RootWeb.ID))
                {
                    if ((web.AllProperties == null) || (!web.AllProperties.ContainsKey(str) && !(flag = web.AllProperties.ContainsKey(str.ToLowerInvariant()))))
                    {
                        return false;
                    }
                    flag3 = !web.AllProperties[flag ? str.ToLowerInvariant() : str].Equals(farm.Properties[farm.Properties.ContainsKey(str.ToLowerInvariant()) ? str.ToLowerInvariant() : str]);
                }
            }
            finally
            {
                if ((flag2 && Elevated) && (site != null))
                {
                    site.Dispose();
                }
            }
            return flag3;
        }

        public static void ConfigReset(SPContext context, string key, bool siteReset)
        {
            string k = AssemblyName + "_" + key;
            if (key == "_lang")
            {
                farmCulture = null;
                siteCulture = null;
            }
            Elevate(delegate {
                SPFarm farm = GetFarm(context);
                SPSite site = Elevated ? OpenSite(context) : GetSite(context);
                site.AllowUnsafeUpdates = true;
                site.CatchAccessDeniedException = false;
                if (context != null)
                {
                    context.Web.AllowUnsafeUpdates = true;
                }
                if (!siteReset)
                {
                    if (farm.Properties.ContainsKey(k.ToLowerInvariant()))
                    {
                        farm.Properties.Remove(k.ToLowerInvariant());
                    }
                    if (farm.Properties.ContainsKey(k))
                    {
                        farm.Properties.Remove(k);
                    }
                    farm.Update(true);
                }
                else
                {
                    using (SPWeb web = site.OpenWeb(site.RootWeb.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        if (web.AllProperties.ContainsKey(k))
                        {
                            web.AllProperties.Remove(k);
                        }
                        if (web.AllProperties.ContainsKey(k.ToLowerInvariant()))
                        {
                            web.AllProperties.Remove(k.ToLowerInvariant());
                        }
                        web.Update();
                    }
                }
                if (Elevated && (site != null))
                {
                    site.Dispose();
                }
            }, true);
        }

        public static string ConfigScope(ref string key)
        {
            int index = key.IndexOf(':');
            string str = (index <= 0) ? string.Empty : key.Substring(0, index);
            if (index >= 0)
            {
                key = key.Substring(index + 1);
            }
            return str;
        }

        public static object ConvertCalcFieldValue(object val, SPFieldType typeHint, bool dateNoYear)
        {
            string s = string.Empty + val;
            string str2 = string.Empty;
            if ((typeHint != SPFieldType.Boolean) || !(val is bool))
            {
                double num2;
                DateTime time;
                if ((typeHint == SPFieldType.Currency) && ((((val is decimal) || (val is int)) || ((val is long) || (val is float))) || (val is double)))
                {
                    return val;
                }
                if ((typeHint == SPFieldType.DateTime) && (val is DateTime))
                {
                    return val;
                }
                if ((typeHint == SPFieldType.Number) && (((val is decimal) || (val is int)) || (((val is long) || (val is float)) || (val is double))))
                {
                    return val;
                }
                int index = s.IndexOf(";#", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                {
                    str2 = s.Substring(0, index);
                    s = s.Substring(index + 2);
                }
                if (((str2 == "float") && (typeHint == SPFieldType.DateTime)) && double.TryParse(s, out num2))
                {
                    return DateTime.FromOADate(num2);
                }
                if ((str2 == "datetime") && DateTime.TryParse(s, out time))
                {
                    return time;
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    return s;
                }
            }
            return val;
        }

        internal static string ConvertDateIf(string val, bool convert)
        {
            if (!convert)
            {
                return val;
            }
            return ConvertDateToString(ConvertStringToDate(val));
        }

        internal static string ConvertDateNoTimeIf(string val, bool convertDate, bool stripTime)
        {
            int num;
            if (((num = (val = convertDate ? ConvertDateToString(ConvertStringToDate(val)) : val).IndexOf(' ')) > 0) && stripTime)
            {
                return val.Substring(0, num);
            }
            return val;
        }

        internal static string ConvertDateToString(DateTime value)
        {
            return ConvertDateToString(value, string.Empty, null);
        }

        internal static string ConvertDateToString(DateTime value, string format, CultureInfo culture)
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            if (string.IsNullOrEmpty(format))
            {
                format = culture.DateTimeFormat.ShortDatePattern;
            }
            if (value.TimeOfDay.Ticks != 0L)
            {
                format = format + @" HH\:mm\:ss";
            }
            return value.ToString(format, culture);
        }

        internal static string ConvertNumberForCaml(string value)
        {
            string str = string.Empty;
            foreach (char ch in value)
            {
                if (char.IsNumber(ch))
                {
                    str = str + ch;
                }
                else if (CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator.Equals(ch.ToString()))
                {
                    str = str + CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;
                }
                else if (CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.Equals(ch.ToString()))
                {
                    str = str + CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
                }
            }
            return str;
        }

        internal static DateTime ConvertStringToDate(string value)
        {
            return ConvertStringToDate(value, null);
        }

        internal static DateTime ConvertStringToDate(string value, CultureInfo culture)
        {
            DateTime maxValue = DateTime.MaxValue;
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            if (value != null)
            {
                DateTime.TryParse(value, culture, DateTimeStyles.AllowWhiteSpaces, out maxValue);
            }
            return maxValue;
        }

        internal static void CreateLicControls(ControlCollection controls, string prefixControl, string suffixControl)
        {
            controls.Add(new LiteralControl(string.Format("<span id=\"roxlicsection\">" + prefixControl, string.Empty, "<div class=\"rox-wplicdiv\"><a target=\"_blank\" href=\"http://SharePoint-Tools.net\"><img border=\"0\" align=\"right\" src=\"/_layouts/" + AssemblyName + "/help/res/roxority.tlhr.png\" width=\"96\"/></a>" + GetResource("HelpText", new object[0]) + " <a target=\"_blank\" href=\"" + SPContext.Current.Site.Url + "/_layouts/" + GetProductResource("_WhiteLabel", new object[0]) + GetTitle() + ".aspx\">" + GetResource("HelpTitle", new object[] { GetTitle() }) + "</a>.</div>" + GetLicHtml(), "")));
            controls.Add(new LiteralControl(suffixControl + "<a name=\"roxtooltop\"></a></span>"));
        }

        internal static string CreateSimpleCamlNode(string outerNodeName, string innerNodeName, string fieldRefName, string fieldRefType, string value)
        {
            XmlNode node;
            XmlDocument document = new XmlDocument();
            if (((fieldRefType == "BusinessData") || (fieldRefType == "Computed")) || (fieldRefType == "Calculated"))
            {
                fieldRefType = "Text";
            }
            document.LoadXml(string.Format(string.IsNullOrEmpty(outerNodeName) ? "<{1}><FieldRef Name=\"\"/><Value Type=\"{2}\"></Value></{1}>" : "<{0}><{1}><FieldRef Name=\"\"/><Value Type=\"{2}\"></Value></{1}></{0}>", outerNodeName, innerNodeName, fieldRefType));
            (node = (string.IsNullOrEmpty(outerNodeName) != null) ? document.DocumentElement : document.DocumentElement.FirstChild).FirstChild.Attributes[0].Value = fieldRefName;
            if (fieldRefType == "Number")
            {
                value = ConvertNumberForCaml(value);
            }
            else if (fieldRefType == "DateTime")
            {
                try
                {
                    value = SPUtility.CreateISO8601DateTimeFromSystemDateTime(ConvertStringToDate(value));
                    XmlAttribute namedItem = node.LastChild.Attributes.GetNamedItem("IncludeTimeValue") as XmlAttribute;
                    if (namedItem == null)
                    {
                        namedItem = node.LastChild.Attributes.Append(document.CreateAttribute("IncludeTimeValue"));
                    }
                    namedItem.Value = "FALSE";
                }
                catch
                {
                }
            }
            node.LastChild.InnerText = value;
            return document.DocumentElement.OuterXml;
        }

        private static bool Deep(object one, object two)
        {
            ICollection is2 = one as ICollection;
            ICollection is3 = two as ICollection;
            IDictionary dictionary = one as IDictionary;
            IDictionary dictionary2 = two as IDictionary;
            if (((one != null) || (two != null)) && !object.ReferenceEquals(one, two))
            {
                if (((one == null) || (two == null)) || (one.GetType() != two.GetType()))
                {
                    return false;
                }
                if (dictionary != null)
                {
                    if (dictionary.Count != dictionary2.Count)
                    {
                        return false;
                    }
                    foreach (string str in dictionary.Keys)
                    {
                        if (!dictionary2.Contains(str))
                        {
                            return false;
                        }
                        if (!Deep(dictionary[str], dictionary2[str]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                if (is2 == null)
                {
                    return one.Equals(two);
                }
                if (is2.Count != is3.Count)
                {
                    return false;
                }
                object[] array = new object[is2.Count];
                object[] objArray2 = new object[is3.Count];
                is2.CopyTo(array, 0);
                is3.CopyTo(objArray2, 0);
                for (int i = 0; i < is2.Count; i++)
                {
                    if (!Deep(array[i], objArray2[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static IEnumerable<T> Deserialize<T>(string value, Action<T> action) where T: class
        {
            T iteratorVariable1;
            object iteratorVariable0;
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(value)))
            {
                iteratorVariable0 = Formatter.Deserialize(stream);
            }
            if ((typeof(T) != typeof(object)) && ((iteratorVariable1 = iteratorVariable0 as T) != null))
            {
                if (action != null)
                {
                    action(iteratorVariable1);
                }
                yield return iteratorVariable1;
            }
            else
            {
                IEnumerable<T> iteratorVariable2 = iteratorVariable0 as IEnumerable<T>;
                if (iteratorVariable2 != null)
                {
                    foreach (T iteratorVariable3 in iteratorVariable2)
                    {
                        if (action != null)
                        {
                            action(iteratorVariable3);
                        }
                        yield return iteratorVariable3;
                    }
                }
            }
        }

        public override void Dispose()
        {
            if (this.adminSite != null)
            {
                this.adminSite.Dispose();
                this.adminSite = null;
            }
            base.Dispose();
        }

        public static void Elevate(SPSecurity.CodeToRunElevated code, bool tryUnelevatedFirst)
        {
            Elevate(code, tryUnelevatedFirst, false);
        }

        public static void Elevate(SPSecurity.CodeToRunElevated code, bool tryUnelevatedFirst, bool forceBoth)
        {
            bool catchAccessDeniedException = SPSecurity.CatchAccessDeniedException;
            SPSecurity.CatchAccessDeniedException = false;
            try
            {
                if (SPContext.Current != null)
                {
                    SPContext.Current.Site.CatchAccessDeniedException = false;
                }
            }
            catch
            {
            }
            if (tryUnelevatedFirst && !forceBoth)
            {
                try
                {
                    try
                    {
                        code();
                    }
                    catch
                    {
                        Elevated = true;
                        SPSecurity.RunWithElevatedPrivileges(code);
                    }
                    return;
                }
                finally
                {
                    SPSecurity.CatchAccessDeniedException = catchAccessDeniedException;
                    Elevated = false;
                }
            }
            if (forceBoth)
            {
                try
                {
                    code();
                }
                catch
                {
                }
            }
            try
            {
                Elevated = true;
                SPSecurity.RunWithElevatedPrivileges(code);
            }
            finally
            {
                SPSecurity.CatchAccessDeniedException = catchAccessDeniedException;
                Elevated = false;
            }
        }

        private static IEnumerable<SPPersistedObject> EnumeratePersisteds(SPContext context)
        {
            return EnumeratePersisteds(context, false);
        }

        private static IEnumerable<SPPersistedObject> EnumeratePersisteds(SPContext context, bool contentAppsOnly)
        {
            if (!contentAppsOnly)
            {
                yield return GetFarm(context);
            }
            foreach (SPWebService iteratorVariable0 in new SPWebService[] { SPWebService.AdministrationService, SPWebService.ContentService })
            {
                if (!contentAppsOnly)
                {
                    yield return iteratorVariable0;
                }
                if ((iteratorVariable0 == SPWebService.ContentService) || !contentAppsOnly)
                {
                    foreach (SPWebApplication iteratorVariable1 in TryEach<SPWebApplication>(iteratorVariable0.WebApplications))
                    {
                        yield return iteratorVariable1;
                    }
                }
            }
        }

        internal static Control FindControl(ControlCollection controls, string id)
        {
            foreach (Control control2 in controls)
            {
                if (id.Equals(control2.ID))
                {
                    return control2;
                }
                Control control = FindControl(control2.Controls, id);
                if (control != null)
                {
                    return control;
                }
            }
            return null;
        }

        public static string FindControlClientID(ControlCollection controls, string id)
        {
            Control control = FindControl(controls, id);
            if (control != null)
            {
                return control.ClientID;
            }
            return null;
        }

        public static SPWebApplication GetAdminApplication()
        {
            foreach (SPWebApplication application in TryEach<SPWebApplication>(SPWebService.AdministrationService.WebApplications))
            {
                if (application.IsAdministrationWebApplication)
                {
                    return application;
                }
            }
            return null;
        }

        public static SPSite GetAdminSite()
        {
            SPWebApplication adminApplication = GetAdminApplication();
            SPSite site = null;
            if (((adminApplication != null) && (adminApplication.Sites.Count > 0)) && ((site = adminApplication.Sites["/"]) == null))
            {
                site = adminApplication.Sites[0];
            }
            site.CatchAccessDeniedException = false;
            return site;
        }

        public static SPContext GetContext()
        {
            try
            {
                return SPContext.Current;
            }
            catch
            {
                return null;
            }
        }

        public static SPListItem GetDocument(SPList list, string fileName)
        {
            int num = fileName.LastIndexOf('/');
            SPQuery query = new SPQuery();
            if (num >= 0)
            {
                fileName = fileName.Substring(num + 1);
            }
            try
            {
                query.Folder = list.RootFolder;
                query.AutoHyperlink = query.ExpandRecurrence = query.ExpandUserField = query.IncludeAttachmentVersion = query.IncludeAttachmentUrls = query.ItemIdQuery = false;
                query.IncludeAllUserPermissions = query.IncludeMandatoryColumns = query.IncludePermissions = query.IndividualProperties = true;
                query.ViewAttributes = "FailIfEmpty=\"FALSE\" RequiresClientIntegration=\"FALSE\" Threaded=\"FALSE\" Scope=\"Recursive\"";
                query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"FileLeafRef\"/><FieldRef Name=\"LinkFilename\"/><FieldRef Name=\"LinkFilenameNoMenu\"/><FieldRef Name=\"LinkFilename\"/><FieldRef Name=\"DocIcon\"/><FieldRef Name=\"ParentLeafName\"/>";
                query.Query = "<Where><Eq><FieldRef Name=\"FileLeafRef\" /><Value Type=\"File\">" + fileName + "</Value></Eq></Where>";
                IEnumerator enumerator = list.GetItems(query).GetEnumerator();
                {
                    while (enumerator.MoveNext())
                    {
                        return (SPListItem) enumerator.Current;
                    }
                }
                foreach (SPListItem item2 in TryEach<SPListItem>(list.Items))
                {
                    if (GetListItemTitle(item2, true).Equals(fileName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return item2;
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        public static string GetEdition(int key)
        {
            int num = -1;
            foreach (KeyValuePair<int, string> pair in GetEditions())
            {
                if (pair.Key > num)
                {
                    num = pair.Key;
                }
                if (pair.Key == key)
                {
                    return (((pair.Key == 0) || !IsWhiteLabel) ? pair.Value : GetProductResource("_WhiteLabelLicensed", new object[0]));
                }
            }
            if ((key < 0) && (num >= 0))
            {
                return GetEdition(num);
            }
            if (!IsWhiteLabel)
            {
                return "UNKNOWN";
            }
            return GetProductResource("_WhiteLabelLicensed", new object[0]);
        }

        private static List<KeyValuePair<int, string>> GetEditions()
        {
            int num = -1;
            if (editions.Count == 0)
            {
                lock (editions)
                {
                    List<KeyValuePair<int, string>> collection = new List<KeyValuePair<int, string>>();
                    foreach (string str in GetProductResource("_Editions", new object[0]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        collection.Add(new KeyValuePair<int, string>(num = (num < 0) ? 0 : ((num == 0) ? 2 : (num * 2)), str.Trim()));
                    }
                    if (collection.Count == 0)
                    {
                        collection.Add(new KeyValuePair<int, string>(0, GetTitle()));
                    }
                    editions.Clear();
                    editions.AddRange(collection);
                }
            }
            return editions;
        }

        public static MailAddress GetEmailAddress(string value)
        {
            int index = value.IndexOf('@');
            MailAddress address = null;
            if ((index > 0) && (value.LastIndexOf('.') > (index + 2)))
            {
                try
                {
                    address = new MailAddress(value);
                }
                catch
                {
                }
            }
            return address;
        }

        public static SPFarm GetFarm(SPContext context)
        {
            SPPersistedObject parent;
            SPFarm local = SPFarm.Local;
            SPSite site = null;
            if ((local == null) && (context == null))
            {
                context = GetContext();
            }
            if (local == null)
            {
                try
                {
                    local = GetSite(context).WebApplication.Farm;
                }
                catch
                {
                }
            }
            if ((local == null) && ((parent = (site = Elevated ? OpenSite(context) : GetSite(context)).WebApplication) != null))
            {
                while (((parent != null) && !(parent is SPFarm)) && (parent != parent.Parent))
                {
                    parent = parent.Parent;
                }
                local = parent as SPFarm;
            }
            if (Elevated && (site != null))
            {
                site.Dispose();
            }
            return local;
        }

        public static CultureInfo GetFarmCulture(SPContext context)
        {
            string str;
            SPSite site = Elevated ? OpenSite(context) : GetSite(context);
            GetFarm(context);
            CultureInfo siteCulture = null;
            if (site != null)
            {
                if (ProductPage.siteCulture == null)
                {
                    if (string.IsNullOrEmpty(str = Config(context, "_lang")))
                    {
                        ProductPage.siteCulture = Thread.CurrentThread.CurrentUICulture;
                    }
                    else
                    {
                        ProductPage.siteCulture = new CultureInfo(str);
                    }
                }
                siteCulture = ProductPage.siteCulture;
            }
            else
            {
                if (farmCulture == null)
                {
                    if (string.IsNullOrEmpty(str = Config(context, "_lang")))
                    {
                        farmCulture = Thread.CurrentThread.CurrentUICulture;
                    }
                    else
                    {
                        farmCulture = new CultureInfo(str);
                    }
                }
                siteCulture = farmCulture;
            }
            if (Elevated)
            {
                site.Dispose();
            }
            return siteCulture;
        }

        internal static SPField GetField(SPFieldCollection fields, Guid id)
        {
            foreach (SPField field in TryEach<SPField>(fields))
            {
                if (id.Equals(field.Id))
                {
                    return field;
                }
            }
            return null;
        }

        internal static SPField GetField(SPFieldCollection fields, string fieldName)
        {
            SPField fieldByInternalName = null;
            SPField field2 = null;
            if (fields != null)
            {
                try
                {
                    fieldByInternalName = fields.GetFieldByInternalName(fieldName);
                }
                catch
                {
                }
                if (fieldByInternalName == null)
                {
                    foreach (SPField field3 in TryEach<SPField>(fields))
                    {
                        if (field3.InternalName == fieldName)
                        {
                            return field3;
                        }
                        if (!string.IsNullOrEmpty(field3.InternalName) && field3.InternalName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            fieldByInternalName = field3;
                        }
                        else if (!string.IsNullOrEmpty(field3.Title) && field3.Title.Trim().Equals(fieldName.Trim(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            field2 = field3;
                        }
                    }
                }
            }
            if (fieldByInternalName == null)
            {
                return field2;
            }
            return fieldByInternalName;
        }

        public static SPField GetField(SPList list, Guid fieldID)
        {
            if (list == null)
            {
                return null;
            }
            return GetField(list.Fields, fieldID);
        }

        public static SPField GetField(SPList list, string fieldName)
        {
            if (list != null)
            {
                return GetField(list.Fields, fieldName);
            }
            return null;
        }

        public static SPField GetField(SPListItem item, string fieldName)
        {
            if (item != null)
            {
                return GetField(item.Fields, fieldName);
            }
            return null;
        }

        internal static object GetFieldProp(SPField field, string name)
        {
            LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot(field.GetType().Name + "_" + name);
            object obj2 = (namedDataSlot == null) ? null : Thread.GetData(namedDataSlot);
            if (obj2 != null)
            {
                return obj2;
            }
            return field.GetCustomProperty(name);
        }

        public static object GetFieldVal(SPListItem item, SPField field, bool returnException)
        {
            try
            {
                if (Guid.Empty.Equals(field.Id))
                {
                    return item[field.InternalName];
                }
                return item[field.Id];
            }
            catch (Exception exception)
            {
                return (returnException ? exception : null);
            }
        }

        public static Guid GetGuid(string value)
        {
            return GetGuid(value, false);
        }

        public static Guid GetGuid(string value, bool force)
        {
            if (!force)
            {
                if (!IsGuid(value))
                {
                    return Guid.Empty;
                }
                return new Guid(value);
            }
            try
            {
                return new Guid(value);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public string GetHelpTitle(string topicID)
        {
            string productResource = GetProductResource("HelpTopic_" + topicID, new object[0]);
            if (string.IsNullOrEmpty(productResource) && topicID.StartsWith("itemref_"))
            {
                productResource = GetResource("Ref", new object[] { GetProductResource("Tool_" + topicID.Substring("itemref_".Length) + "_Title", new object[0]) });
            }
            return productResource;
        }

        internal static string GetLicHtml()
        {
            string str = string.Empty;
            string str2 = string.Empty;
            LicInfo info = LicInfo.Get(null);
            if (!info.expired)
            {
                DateTime time;
                if (string.IsNullOrEmpty(info.name))
                {
                    return string.Format("<a target=\"_blank\" href=\"" + MergeUrlPaths(SPContext.Current.Web.Url, "_layouts/" + AssemblyName + ".aspx?cfg=lic") + "\" class=\"rox-licinfo\" style=\"background: green;\">{0}</a>", GetResource(DateTime.MinValue.Equals(info.expiry) ? "LicLite" : "LicTrial", new object[0]));
                }
                return string.Format("<a target=\"_blank\" href=\"" + MergeUrlPaths(SPContext.Current.Web.Url, "_layouts/" + AssemblyName + ".aspx?cfg=lic") + "\" class=\"rox-licinfo\" style=\"background: green;\">{0}</a>", GetLicStatus(info.dic, info.sd, false, out time).Replace("b>", "i>") + GetResource("LicTo", new object[] { info.name }));
            }
            SPSite site = Elevated ? OpenSite(null) : GetSite(null);
            str2 = string.Format("b: {0} ed: {1} ey: {2} is: {3} s: {8} mu: {4} n: {5} su: {6} ub: {7}", new object[] { info.broken, info.expired, info.expiry, info.installSpan, info.maxUsers, info.name, info.siteUsers, info.userBroken, (site == null) ? string.Empty : site.ID.ToString() });
            if ((site != null) && Elevated)
            {
                site.Dispose();
            }
            str = "<script type=\"text/javascript\" language=\"JavaScript\"> var roxLicError = '" + SPEncode.ScriptEncode(str2) + @"\n'; ";
            if (info.error != null)
            {
                str = str + @"roxLicError += '\n" + SPEncode.ScriptEncode(info.error.ToString()) + "'; ";
            }
            str = str + "</script>";
            if (info.userBroken)
            {
                return string.Format(str + "<a target=\"_blank\" href=\"" + MergeUrlPaths(SPContext.Current.Web.Url, "_layouts/" + AssemblyName + ".aspx?cfg=lic") + "\" class=\"rox-licinfo\" style=\"background: gold; color: #000;\">{0}</a><div style=\"text-align: right; font-size: 9px;\"><span onclick=\"alert(roxLicError);\">:</span></div>", GetResource("LicExpiryUsers", new object[0]));
            }
            return string.Format(str + "<a target=\"_blank\" href=\"" + MergeUrlPaths(SPContext.Current.Web.Url, "_layouts/" + AssemblyName + ".aspx?cfg=lic") + "\" class=\"rox-licinfo\" style=\"background: gold; color: #000;\">{0}</a><div style=\"text-align: right; font-size: 9px;\"><span onclick=\"alert(roxLicError);\">:</span></div>", GetResource("LicExpiry", new object[0]));
        }

        public static string GetLicStatus(IDictionary dic, IDictionary sd, bool detailed, out DateTime expiry)
        {
            
            object[] objArray2={};
            int result = 0;
            int num2 = 0;
            int num3 = 0;
            long ticks = 0L;
            string edition = "";
            string str2 = string.Empty;
            LicInfo info = LicInfo.Get(dic);
            dic = info.dic;
            sd = info.sd;
            expiry = info.expiry;


            return ("<b>" + GetEdition(-1) + "</b>");

            expiry = DateTime.MinValue;
            if (dic != null)
            {
                if (dic.Contains("f1"))
                {
                    int.TryParse(dic["f1"] + string.Empty, out num3);
                }
                if ((dic.Contains("f4") && long.TryParse(dic["f4"] + string.Empty, out ticks)) && (ticks > 0L))
                {
                    expiry = new DateTime(ticks);
                }
                if (dic.Contains("f2"))
                {
                    int.TryParse(dic["f2"] + string.Empty, out result);
                }
                if (dic.Contains("f3"))
                {
                    int.TryParse(dic["f3"] + string.Empty, out num2);
                }
                if ((((num3 > 1) && (num2 > 0)) && ((sd != null) && (expiry == DateTime.MinValue))) && (sd.Contains("ed") && (sd["ed"] is DateTime)))
                {
                    DateTime time2 = expiry = (DateTime) sd["ed"];
                    ticks = time2.Ticks;
                    if (expiry >= DateTime.Now)
                    {
                        num2 = 0;
                    }
                }
                if (num2 == 6)
                {
                    edition = GetEdition(4);
                }
                else
                {
                    edition = GetEdition(num2);
                }
            }
            else if (sd != null)
            {
                if (sd.Contains("ed") && (sd["ed"] is DateTime))
                {
                    DateTime time4 = expiry = (DateTime) sd["ed"];
                    ticks = time4.Ticks;
                }
                if (GetEdition(0).Equals(edition = (expiry > DateTime.Now) ? (IsWhiteLabel ? GetProductResource("_WhiteLabelUnlicensed", new object[0]) : GetEdition(-1)) : ((sd.Count > 3) ? GetEdition(0) : "")))
                {
                    str2 = GetEdition(-1);
                }
            }
            else
            {
                edition = GetResource("LicStatusBroken", new object[0]);
            }
            if (ticks <= 0L)
            {
                if ((info.siteUsers <= 10) && HasMicro)
                {
                    return ("<b>" + GetEdition(-1) + "</b>");
                }
                if (IsTheThing(dic))
                {
                    return ("<b>" + GetEdition(0) + "</b> " + (detailed ? GetResource("LicStatusDowned", new object[0]) : string.Empty));
                }
                return ("<b>" + edition + "</b>");
            }
            long num5 = ticks - DateTime.Now.Ticks;
            if (num5 > 0L)
            {
                object[] args = new object[1];
                TimeSpan span = new TimeSpan(Math.Abs(num5));
                return ("<b>" + edition + "</b> " + (detailed ? GetResource("LicStatusExpires", args) : string.Empty));
                //objArray2 = new object[2];
                //span = new TimeSpan(Math.Abs(num5));
            }
            return ("<b>" + (string.IsNullOrEmpty(edition) ? GetResource("None", new object[0]) : GetEdition(0)) + "</b> " + (detailed ? GetResource("LicStatusExpired", objArray2) : string.Empty));
        }

        public string GetLink(string cfg, params string[] queryArgs)
        {
            string str = "default.aspx?";
            if (string.IsNullOrEmpty(cfg))
            {
                cfg = base.Request["cfg"];
            }
            str = str + "cfg=" + cfg;
            if (!string.IsNullOrEmpty(cfg = base.Request["tool"]) && (Array.IndexOf<string>(queryArgs, "tool") < 0))
            {
                str = str + "&tool=" + cfg;
            }
            if ((queryArgs != null) && (queryArgs.Length > 0))
            {
                for (int i = 1; i < queryArgs.Length; i += 2)
                {
                    string str2 = str;
                    str = str2 + "&" + queryArgs[i - 1] + "=" + base.Server.UrlEncode(queryArgs[i]);
                }
            }
            return (str + "&r=" + this.Rnd.Next());
        }

        public static SPList GetList(SPWeb web, Guid id)
        {
            SPListCollection collection = null;
            try
            {
                collection = web.Lists;
            }
            catch
            {
            }
            if (collection != null)
            {
                foreach (SPList list in TryEach<SPList>(collection))
                {
                    if (id.Equals(list.ID))
                    {
                        return list;
                    }
                }
            }
            return null;
        }

        public static SPList GetList(SPWeb web, string url)
        {
            SPList listFromUrl = null;
            SPList listFromWebPartPageUrl;
            url = MergeUrlPaths(web.Url, url);
            try
            {
                listFromUrl = web.GetList(url);
                if (listFromUrl == null)
                {
                    throw new Exception();
                }
                listFromWebPartPageUrl = listFromUrl;
            }
            catch
            {
                try
                {
                    listFromUrl = web.GetListFromUrl(url);
                    if (listFromUrl == null)
                    {
                        throw new Exception();
                    }
                    listFromWebPartPageUrl = listFromUrl;
                }
                catch
                {
                    try
                    {
                        listFromWebPartPageUrl = web.GetListFromWebPartPageUrl(url);
                    }
                    catch
                    {
                        listFromWebPartPageUrl = null;
                    }
                }
            }
            return listFromWebPartPageUrl;
        }

        public static string GetListItemTitle(SPListItem item, bool forceName)
        {
            string str;
            if (!forceName)
            {
                if (!string.IsNullOrEmpty(item.Title))
                {
                    return item.Title;
                }
                foreach (string str2 in new string[] { "Title", "LinkTitle", "LinkTitleNoMenu", "LinkDiscussionTitle", "LinkDiscussionTitleNoMenu" })
                {
                    try
                    {
                        SPField field;
                        if (((field = GetField(item, str2)) != null) && !string.IsNullOrEmpty(str = item[field.Id] + string.Empty))
                        {
                            return str;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                return item.Name;
            }
            foreach (string str3 in new string[] { "LinkFilenameNoMenu", "LinkFilename", "FileLeafRef", "ParentLeafName", "BaseName" })
            {
                try
                {
                    if (!string.IsNullOrEmpty(str = item[GetField(item, str3).Id] + string.Empty))
                    {
                        return str;
                    }
                }
                catch
                {
                }
            }
            return ("#" + item.ID);
        }

        private static string GetMapping()
        {
            if (mapkey == null)
            {
                mapkey = GetProductResource("_MappingKey", new object[0]);
            }
            return mapkey;
        }

        private static string GetName()
        {
            if (pname == null)
            {
                pname = AssemblyName;
            }
            return pname;
        }

        public int GetNavCount(string item)
        {
            int num;
            bool noSave = JsonSchemaManager.noSave;
            JsonSchemaManager.noSave = true;
            try
            {
                if (item == "wss")
                {
                    return GetProductResource("_WssItems", new object[0]).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
                if (item == "cfg")
                {
                    return this.ConfigSettingsCount;
                }
                if (item == "Tool_SiteUsers")
                {
                    return GetUsersDict(GetContext()).Count;
                }
                if (item == "Tool_Localizer")
                {
                    return Loc(string.Empty, null);
                }
                if (item.StartsWith("Tool_") && (item != "Tool_Transfer"))
                {
                    foreach (string str in JsonSchemaManager.DiscoverSchemaFiles(this.Context))
                    {
                        KeyValuePair<JsonSchemaManager, JsonSchemaManager> pair = JsonSchemaManager.TryGet(this, str, this.IsAdminSite, !this.IsAdminSite, item.StartsWith("Tool_Data") ? "roxority_Shared" : null);
                        try
                        {
                            JsonSchemaManager manager;
                            JsonSchemaManager.Schema schema;
                            if (((manager = this.IsAdminSite ? pair.Key : pair.Value) != null) && manager.AllSchemas.TryGetValue(item.Substring(5), out schema))
                            {
                                return schema.InstanceCount;
                            }
                        }
                        finally
                        {
                            if ((pair.Key != null) && (pair.Key != pair.Value))
                            {
                                pair.Key.Dispose();
                            }
                            if (pair.Value != null)
                            {
                                pair.Value.Dispose();
                            }
                        }
                    }
                }
                num = -1;
            }
            finally
            {
                JsonSchemaManager.noSave = noSave;
            }
            return num;
        }

        private static string GetOtherResource(string resName, string resKey, params object[] args)
        {
            ResourceManager manager;
            if (!resMans.TryGetValue(resName, out manager))
            {
                resMans[resName] = manager = new ResourceManager(AssemblyName + ".Properties." + resName, Assembly);
            }
            return GetResource(manager, resKey, args);
        }

        public static string GetProductResource(string resKey, params object[] args)
        {
            string str = GetResource(ProductResources, resKey, args);
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            return GetResource(resKey, args);
        }

        private static T[] GetRange<T>(T[] arr, int startIndex, int length)
        {
            T[] destinationArray = new T[length];
            Array.Copy(arr, startIndex, destinationArray, 0, length);
            return destinationArray;
        }

        public static string GetResource(string resKey, params object[] args)
        {
            return GetResource(null, resKey, args);
        }

        protected internal static string GetResource(ResourceManager res, string resKey, params object[] args)
        {
            string format = string.Empty;
            string key = "roxority_Shared";
            CultureInfo lang = null;
            if (res == null)
            {
                res = Resources;
            }
            else
            {
                key = AssemblyName + "_Runtime";
            }
            try
            {
                if ((Array.IndexOf<string>(new string[] { "_HelpTopics", "_AdminHelpTopics", "_ProductID" }, resKey) < 0) && ((GetContext() != null) || (currentSite != null)))
                {
                    lang = GetFarmCulture(GetContext());
                }
                if ((lang == null) || (lang == CultureInfo.InvariantCulture))
                {
                    format = res.GetString(resKey);
                }
                else if (string.IsNullOrEmpty(format = Loc(key, resKey, lang)))
                {
                    format = res.GetString(resKey, lang);
                }
                if (format == null)
                {
                    format = string.Empty;
                }
            }
            catch
            {
            }
            format = format.Replace("{PROD}", GetTitle());
            if ((args != null) && (args.Length != 0))
            {
                return string.Format(format, args);
            }
            return format;
        }

        internal static SPWeb GetRootWeb(SPWeb web)
        {
            SPSecurity.CatchAccessDeniedException = false;
            if (web != null)
            {
                web.Site.CatchAccessDeniedException = false;
                try
                {
                    return (web.IsRootWeb ? web : GetRootWeb(web.ParentWeb));
                }
                catch
                {
                    try
                    {
                        return web.Site.RootWeb;
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }

        internal static SPSite GetSite(SPContext context)
        {
            SPSite site = ((context == null) && ((context = GetContext()) == null)) ? currentSite : context.Site;
            HttpContext current = null;
            try
            {
                current = HttpContext.Current;
            }
            catch
            {
            }
            try
            {
                if ((site == null) && (current != null))
                {
                    SPWebApplication application;
                    if (((application = SPWebApplication.Lookup(current.Request.Url)) == null) || (application.Sites.Count == 0))
                    {
                        throw new SPException(GetResource("NoSite", new object[] { AssemblyName, GetTitle() }));
                    }
                    using (SPSite site2 = application.Sites[0])
                    {
                        current.Response.Redirect(site2.Url.TrimEnd(new char[] { '/' }) + current.Request.Url.ToString().Substring(current.Request.Url.ToString().ToLowerInvariant().IndexOf("/_layouts/" + AssemblyName.ToLowerInvariant() + "/default.aspx")), true);
                    }
                }
            }
            catch
            {
            }
            if (site != null)
            {
                site.CatchAccessDeniedException = false;
            }
            return site;
        }

        public static string GetSiteTitle(SPContext context)
        {
            SPSecurity.CodeToRunElevated code = null;
            SPSite site = GetSite(context);
            SPSecurity.CatchAccessDeniedException = site.CatchAccessDeniedException = false;
            string title = string.Empty;
            try
            {
                if (code == null)
                {
                    code = delegate {
                        if (string.IsNullOrEmpty(title))
                        {
                            title = site.RootWeb.Title;
                        }
                    };
                }
                Elevate(code, true, false);
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(title))
            {
                return title;
            }
            return site.Url;
        }

        internal static string GetSrpUrl()
        {
            string sspWebUrl = string.Empty;
            if (Is14)
            {
                using (SPSite site = GetAdminSite())
                {
                    return site.Url.TrimEnd(new char[] { '/' });
                }
            }
            Elevate(delegate {
                foreach (SPWebApplication application in TryEach<SPWebApplication>(SPWebService.ContentService.WebApplications))
                {
                    if ((application.Properties != null) && application.Properties.ContainsKey("Microsoft.Office.Server.SharedResourceProvider"))
                    {
                        try
                        {
                            foreach (SPSite site in application.Sites)
                            {
                                try
                                {
                                    if (site.Url.ToLowerInvariant().TrimEnd(new char[] { '/' }).EndsWith("/ssp/admin"))
                                    {
                                        sspWebUrl = site.Url.TrimEnd(new char[] { '/' });
                                        goto Label_00D2;
                                    }
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
                Label_00D2:
                    if (!string.IsNullOrEmpty(sspWebUrl))
                    {
                        break;
                    }
                }
            }, true);
            return sspWebUrl;
        }

        internal static T GetStatus<T>(SPContext context) where T: class
        {
            List<object> list = new List<object>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            string m = GetMapping();
            object obj2 = null;
            bool flag = false;
            Guid empty = Guid.Empty;
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            SPWebApplication adminApplication = GetAdminApplication();
            SPSite adminSite = OpenSite((context == null) ? (context = GetContext()) : context);
            Converter<SPPersistedObject, byte[]> converter = delegate (SPPersistedObject input) {
                if (input.Properties.ContainsKey(input.GetType().Name.Replace("SP", string.Empty) + m))
                {
                    object obj222;
                    if ((obj222 = input.Properties[input.GetType().Name.Replace("SP", string.Empty) + m]) is byte[])
                    {
                        return obj222 as byte[];
                    }
                    if (obj222 != null)
                    {
                        return new byte[0];
                    }
                }
                return null;
            };
            if ((IsWhiteLabel && (adminApplication != null)) && (adminSite == null))
            {
                adminSite = GetAdminSite();
            }
            try
            {
                flag = HttpContext.Current.Request["cfg"] == "enable";
            }
            catch
            {
            }
            try
            {
                if (((adminApplication == null) || (adminSite == null)) || ((obj2 != null) && !(empty != adminSite.ID)))
                {
                    goto Label_03A7;
                }
                empty = adminSite.ID;
                byte[] item = converter(adminApplication);
                if (item != null)
                {
                    if (item.Length == 0)
                    {
                        return (dictionary2 as T);
                    }
                    list.Add(item);
                }
                try
                {
                    using (SymmetricAlgorithm algorithm = NewAlgo())
                    {
                        using (ICryptoTransform transform = algorithm.CreateDecryptor(GetRange<byte>(os, os.Length - 0x18, 0x18), GetRange<byte>(os, 0, 0x10)))
                        {
                            foreach (byte[] buffer2 in list)
                            {
                                using (MemoryStream stream = new MemoryStream(Trans(transform, buffer2), false))
                                {
                                    list2.Add((Dictionary<string, object>) formatter.Deserialize(stream));
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return (dictionary2 as T);
                }
                Dictionary<string, object> one = new Dictionary<string, object>();
                if (list2.Count > 0)
                {
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if ((i == 0) || (((long) list2[i][tk]) < ((long) one[tk])))
                        {
                            one[tk] = list2[i][tk];
                        }
                    }
                    dictionary2[tk] = one[tk];
                    UpdateInfo(dictionary2);
                    for (int j = 0; j < list2.Count; j++)
                    {
                        if (j == 0)
                        {
                            foreach (string str in list2[j].Keys)
                            {
                                if (str != tk)
                                {
                                    one[str] = list2[j][str];
                                }
                            }
                        }
                        else if (!Deep(one, list2[j]))
                        {
                            return (dictionary2 as T);
                        }
                    }
                }
                else
                {
                    if (list.Count > 0)
                    {
                        return (dictionary2 as T);
                    }
                    one[tk] = DateTime.Today.Ticks;
                    isEnabled = true;
                    if (adminSite.WebApplication.Id.Equals(adminApplication.Id) && (flag || IsFarmAdministrator(adminApplication.Farm)))
                    {
                        try
                        {
                            UpdateStatus(one, false);
                            goto Label_03A4;
                        }
                        catch (Exception exception)
                        {
                            errorMessage = exception.Message;
                            isEnabled = false;
                            goto Label_03A4;
                        }
                    }
                    errorMessage = "foo hoar";
                    isEnabled = false;
                }
            Label_03A4:
                obj2 = one;
            Label_03A7:
                UpdateInfo(obj2 as Dictionary<string, object>);
            }
            finally
            {
                if (adminSite != null)
                {
                    adminSite.Dispose();
                }
            }
            return (obj2 as T);
        }

        internal static string GetTitle()
        {
            if (name == null)
            {
                name = AssemblyName.Substring(AssemblyName.LastIndexOf('_') + 1);
            }
            return name;
        }

        protected internal static int GetUsers(SPContext context)
        {
            return GetUsers(context, false, null);
        }

        protected internal static int GetUsers(SPContext context, bool elevated, Dictionary<string, SPUser> list)
        {
            SPSecurity.CodeToRunElevated secureCode = null;
            int tmp;
            int count = -1;
            bool flag = Elevated;
            SPSite site = (elevated = elevated || Elevated) ? OpenSite(context) : GetSite(context);
            site.CatchAccessDeniedException = false;
            try
            {
                if ((tmp = site.RootWeb.SiteUsers.Count) > count)
                {
                    count = tmp;
                }
                if (list != null)
                {
                    foreach (SPUser user in site.RootWeb.SiteUsers)
                    {
                        if (!list.ContainsKey(user.LoginName.ToLowerInvariant()))
                        {
                            list[user.LoginName.ToLowerInvariant()] = user;
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if ((tmp = site.RootWeb.Users.Count) > count)
                {
                    count = tmp;
                }
                if (list != null)
                {
                    foreach (SPUser user2 in site.RootWeb.Users)
                    {
                        if (!list.ContainsKey(user2.LoginName.ToLowerInvariant()))
                        {
                            list[user2.LoginName.ToLowerInvariant()] = user2;
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if ((tmp = site.RootWeb.AllUsers.Count) > count)
                {
                    count = tmp;
                }
                if (list != null)
                {
                    foreach (SPUser user3 in site.RootWeb.AllUsers)
                    {
                        if (!list.ContainsKey(user3.LoginName.ToLowerInvariant()))
                        {
                            list[user3.LoginName.ToLowerInvariant()] = user3;
                        }
                    }
                }
            }
            catch
            {
            }
            if (elevated)
            {
                site.Dispose();
            }
            else
            {
                try
                {
                    Elevated = true;
                    if (secureCode == null)
                    {
                        secureCode = delegate {
                            if ((tmp = GetUsers(context, true, list)) > count)
                            {
                                count = tmp;
                            }
                        };
                    }
                    SPSecurity.RunWithElevatedPrivileges(secureCode);
                }
                catch
                {
                }
                finally
                {
                    Elevated = flag;
                }
            }
            return count;
        }

        public static Dictionary<string, SPUser> GetUsersDict(SPContext context)
        {
            Dictionary<string, SPUser> list = new Dictionary<string, SPUser>();
            GetUsers(context, false, list);
            return list;
        }

        protected internal IEnumerable<SPWebApplication> GetWebApps(SPContext context, bool contentAppsOnly)
        {
            IEnumerable<SPPersistedObject> iteratorVariable0 = EnumeratePersisteds(context, contentAppsOnly);
            if (iteratorVariable0 != null)
            {
                foreach (SPPersistedObject iteratorVariable1 in iteratorVariable0)
                {
                    if (iteratorVariable1 is SPWebApplication)
                    {
                        yield return (iteratorVariable1 as SPWebApplication);
                    }
                }
            }
        }

        internal static string GuidBracedUpper(Guid guid)
        {
            return ("{" + guid.ToString().ToUpperInvariant().Replace("{", string.Empty).Replace("}", string.Empty) + "}");
        }

        public static string GuidLower(Guid guid)
        {
            return GuidLower(guid, false);
        }

        public static string GuidLower(Guid guid, bool dashes)
        {
            return GuidBracedUpper(guid).Substring(1, 0x24).Replace("-", dashes ? "-" : string.Empty).ToLowerInvariant();
        }

        public bool HasUpdate()
        {
            try
            {
                int num;
                int num2;
                int num3;
                int num4;
                string webVersion = WebVersion;
                string displayVersion = DisplayVersion;
                if (webVersion.LastIndexOf('.') > webVersion.IndexOf('.'))
                {
                    webVersion = webVersion.Substring(0, webVersion.IndexOf('.', webVersion.IndexOf('.') + 1));
                }
                if (((!string.IsNullOrEmpty(webVersion) && !string.IsNullOrEmpty(displayVersion)) && (((num3 = webVersion.IndexOf('.')) > 0) && ((num4 = displayVersion.IndexOf('.')) > 0))) && (int.TryParse(webVersion.Substring(0, num3), out num) && int.TryParse(displayVersion.Substring(0, num4), out num2)))
                {
                    if (num > num2)
                    {
                        return true;
                    }
                    if (num == num2)
                    {
                        return (int.Parse(webVersion.Substring(num3 + 1)) > int.Parse(displayVersion.Substring(num4 + 1)));
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        protected internal object In(IDictionary value)
        {
            SPSecurity.CodeToRunElevated code = null;
            Hashtable locs;
            string key = null;
            string locKey = "roxority_Shared";
            bool flag = false;
            CultureInfo culture = null;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Guid empty = Guid.Empty;
            List<string> list = new List<string>();
            IDictionary dictionary3 = null;
            KeyValuePair<string, object> pair = new KeyValuePair<string, object>();
            if (((base.Request.RequestType.ToLowerInvariant() == "post") || !string.IsNullOrEmpty(base.Request["licremove"])) && (this.IsSiteAdmin || this.IsFarmAdmin))
            {
                try
                {
                    if ("on".Equals(base.Request["licremove"], StringComparison.InvariantCultureIgnoreCase) && ((value.Contains(key = SPContext.Current.Site.ID.ToString()) || value.Contains(key = GetFarm(SPContext.Current).Id.ToString())) || value.Contains(key = Guid.Empty.ToString())))
                    {
                        value.Remove(key);
                        UpdateStatus(value, false);
                    }
                    else if (!string.IsNullOrEmpty(base.Request["licremove"]) && value.Contains(key = base.Request["licremove"]))
                    {
                        value.Remove(key);
                        UpdateStatus(value, false);
                    }
                    else if ((base.Request.Files.Count > 0) && (base.Request["cfg"] == "lic"))
                    {
                        IDictionary dictionary2;
                        XmlDocument document = new XmlDocument();
                        document.Load(base.Request.Files[0].InputStream);
                        dictionary["l"] = document.DocumentElement.SelectSingleNode("l").FirstChild.Value.Substring(l2, (l4 * l2) - 2);
                        key = (empty = new Guid(document.DocumentElement.SelectSingleNode("i").InnerText)).ToString();
                        dictionary["c"] = document.DocumentElement.SelectSingleNode("c").FirstChild.Value;
                        for (int i = 1; i <= 4; i++)
                        {
                            XmlNode node = document.DocumentElement.SelectSingleNode("f" + i);
                            if (node != null)
                            {
                                dictionary["f" + i] = node.InnerText;
                            }
                        }
                        if (!(flag = !"0".Equals(dictionary["f1"]) && (empty.Equals(GetFarm(SPContext.Current).Id) || empty.Equals(Guid.Empty))))
                        {
                            bool flag1 = empty != SPContext.Current.Site.ID;
                        }
                        dictionary3 = In(SPContext.Current, dictionary, empty) as IDictionary;
                        if (dictionary3 == null)
                        {
                            if (dictionary3 != usersDic)
                            {
                                throw new Exception(this["LicNoUpload", new object[0]]);
                            }
                            foreach (object obj2 in usersDic)
                            {
                                if (obj2 is DictionaryEntry)
                                {
                                    DictionaryEntry entry = (DictionaryEntry) obj2;
                                    DictionaryEntry entry2 = (DictionaryEntry) obj2;
                                    pair = new KeyValuePair<string, object>(entry.Key as string, entry2.Value);
                                }
                                else if (obj2 is KeyValuePair<string, object>)
                                {
                                    pair = (KeyValuePair<string, object>) obj2;
                                }
                            }
                            throw new Exception(this["LicUsersError", new object[] { pair.Key, pair.Value }]);
                        }
                        if (flag)
                        {
                            foreach (string str2 in value.Keys)
                            {
                                if (value[str2] is IDictionary)
                                {
                                    list.Add(str2);
                                }
                            }
                            foreach (string str3 in list)
                            {
                                value.Remove(str3);
                            }
                        }
                        else
                        {
                            foreach (object obj3 in value.Values)
                            {
                                if ((((dictionary2 = obj3 as IDictionary) != null) && dictionary2.Contains("f1")) && !"0".Equals(dictionary2["f1"]))
                                {
                                    throw new Exception(this["LicNoSiteIfFarm", new object[0]]);
                                }
                            }
                        }
                        if (value.Contains(key) && ((dictionary2 = value[key] as IDictionary) != null))
                        {
                            foreach (string str4 in dictionary.Keys)
                            {
                                dictionary2[str4] = dictionary[str4];
                            }
                        }
                        value[key] = dictionary;
                        UpdateStatus(value, false);
                    }
                    else if ((base.Request["tool"] == "Tool_Localizer") && ((locs = JSON.JsonDecode(base.Request["roxLocAllVals"]) as Hashtable) != null))
                    {
                        try
                        {
                            culture = new CultureInfo(base.Request["lang"]);
                        }
                        catch
                        {
                        }
                        if (culture != null)
                        {
                            if (base.Request["tab"] != "Studio")
                            {
                                locKey = AssemblyName + "_" + base.Request["tab"];
                            }
                            if (code == null)
                            {
                                code = delegate {
                                    SPContext context = GetContext();
                                    SPFarm farm = GetFarm(context);
                                    Hashtable props = farm.Properties;
                                    bool flag111 = false;
                                    foreach (DictionaryEntry entry in locs)
                                    {
                                        flag111 |= Loc(context, farm, props, locKey, entry.Key + string.Empty, culture, entry.Value + string.Empty);
                                    }
                                    if (flag111)
                                    {
                                        try
                                        {
                                            farm.Update(true);
                                        }
                                        catch
                                        {
                                            farm.Update(false);
                                        }
                                    }
                                };
                            }
                            Elevate(code, true);
                        }
                        base.Response.Redirect(this.GetLink(base.Request["cfg"], new string[] { "tab", base.Request["tab"], "lang", base.Request["lang"] }), true);
                    }
                }
                catch (Exception exception)
                {
                    this.postEx = (exception.InnerException != null) ? exception.InnerException : exception;
                }
            }
            return In(SPContext.Current, value, empty);
        }

        private static object In(SPContext context, IDictionary value, Guid id)
        {
            Dictionary<string, object> dictionary = null;
            object obj2;
            SPSite site = Elevated ? OpenSite(context) : GetSite(context);
            string key = Guid.Empty.ToString();
            string str2 = GetFarm(context).Id.ToString();
            string str3 = site.ID.ToString();
            try
            {
                if ((((value is Dictionary<string, object>) && value.Contains("l")) && (value.Contains("f1") && value.Contains("f2"))) && (value.Contains("f3") && value.Contains("c")))
                {
                    dictionary = value as Dictionary<string, object>;
                }
                else
                {
                    if ((!value.Contains(str3) && !value.Contains(str2)) && !value.Contains(key))
                    {
                        return null;
                    }
                    if (value.Contains(str2))
                    {
                        dictionary = value[str2] as Dictionary<string, object>;
                        id = new Guid(str2);
                    }
                    if ((dictionary == null) && value.Contains(str3))
                    {
                        dictionary = value[str3] as Dictionary<string, object>;
                        id = new Guid(str3);
                    }
                    if ((dictionary == null) && value.Contains(key))
                    {
                        dictionary = value[key] as Dictionary<string, object>;
                        id = Guid.Empty;
                    }
                }
                if (dictionary != null)
                {
                    string str4;
                    string str5;
                    string str6;
                    string str7;
                    string str8;
                    if (((!dictionary.ContainsKey("l") || string.IsNullOrEmpty(str4 = dictionary["l"] as string)) || (!dictionary.ContainsKey("f1") || string.IsNullOrEmpty(str6 = dictionary["f1"] as string))) || (((!dictionary.ContainsKey("f2") || string.IsNullOrEmpty(str7 = dictionary["f2"] as string)) || (!dictionary.ContainsKey("f3") || string.IsNullOrEmpty(str8 = dictionary["f3"] as string))) || (!dictionary.ContainsKey("c") || string.IsNullOrEmpty(str5 = dictionary["c"] as string))))
                    {
                        return null;
                    }
                    using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
                    {
                        long num;
                        provider.ImportCspBlob(os);
                        if ((str6 == "0") && (id.Equals(new Guid(str2)) || id.Equals(Guid.Empty)))
                        {
                            return null;
                        }
                        if (Verify(provider, Convert.FromBase64String(str4 + "=="), id, str5, int.Parse(str6), int.Parse(str7), int.Parse(str8), (dictionary.ContainsKey("f4") && long.TryParse(Convert.ToString(dictionary["f4"]), out num)) ? num : 0L) != 0x80)
                        {
                            return null;
                        }
                        if ((int.Parse(str7) > 0) && (GetUsers(context) > int.Parse(str7)))
                        {
                            usersDic.Clear();
                            usersDic[str7] = GetUsers(context);
                            return usersDic;
                        }
                    }
                }
                obj2 = dictionary;
            }
            finally
            {
                if (Elevated && (site != null))
                {
                    site.Dispose();
                }
            }
            return obj2;
        }

        internal static void InitField(SPField field, params string[] customProperties)
        {
            InitField(field, true, customProperties);
        }

        internal static void InitField(SPField field, bool fallbackToSchema, params string[] customProperties)
        {
            XmlDocument document = fallbackToSchema ? new XmlDocument() : null;
            if (document != null)
            {
                document.LoadXml(field.SchemaXml);
            }
            foreach (string str in customProperties)
            {
                PropertyInfo property = field.GetType().GetProperty(str);
                if (property != null)
                {
                    object fieldProp = GetFieldProp(field, str);
                    if (fieldProp != null)
                    {
                        property.SetValue(field, fieldProp, null);
                    }
                    else
                    {
                        XmlAttribute attribute;
                        if ((document != null) && ((attribute = document.DocumentElement.Attributes[str]) != null))
                        {
                            if (property.PropertyType != typeof(int))
                            {
                                property.SetValue(field, (property.PropertyType == typeof(bool)) ? ((object) ParseBool(attribute.Value)) : ((object) attribute.Value), null);
                            }
                            else
                            {
                                int num;
                                if (int.TryParse(attribute.Value, out num))
                                {
                                    property.SetValue(field, num, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static DateTimeControl InitializeDateTimePicker(DateTimeControl datePicker)
        {
            SPWeb web = (GetContext() == null) ? null : SPContext.Current.Web;
            SPRegionalSettings regionalSettings = (web == null) ? null : ((web.CurrentUser == null) ? web.RegionalSettings : web.CurrentUser.RegionalSettings);
            if ((web != null) && (regionalSettings == null))
            {
                regionalSettings = web.RegionalSettings;
            }
            if (regionalSettings != null)
            {
                datePicker.LocaleId = (int) regionalSettings.LocaleId;
                datePicker.Calendar = (SPCalendarType) regionalSettings.CalendarType;
                datePicker.DateOnly = true;
                datePicker.FirstDayOfWeek = (int) regionalSettings.FirstDayOfWeek;
                datePicker.FirstWeekOfYear = regionalSettings.FirstWeekOfYear;
                datePicker.HijriAdjustment = regionalSettings.AdjustHijriDays;
                datePicker.HoursMode24 = regionalSettings.Time24;
                datePicker.TimeZoneID = regionalSettings.TimeZone.ID;
                datePicker.UseTimeZoneAdjustment = false;
            }
            return datePicker;
        }

        public static bool IsFarmAdministrator(SPFarm farm)
        {
            if ((Is14 && (farmMethod == null)) && !farmMethodTried)
            {
                try
                {
                    farmMethodTried = true;
                    farmMethod = farm.GetType().GetMethod("CurrentUserIsAdministrator", BindingFlags.Public | BindingFlags.Instance, null, new System.Type[] { typeof(bool) }, null);
                }
                catch
                {
                }
            }
            if (farmMethod != null)
            {
                try
                {
                    return (bool) farmMethod.Invoke(farm, new object[] { true });
                }
                catch
                {
                }
            }
            return farm.CurrentUserIsAdministrator();
        }

        public static bool IsFarmOnlySetting(string config)
        {
            string[] strArray;
            string productResource = GetProductResource("_SettingsFarmOnly", new object[0]);
            return ((!string.IsNullOrEmpty(productResource) && ((strArray = productResource.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) != null)) && (Array.IndexOf<string>(strArray, config) >= 0));
        }

        internal static bool IsGuid(string value)
        {
            return (!string.IsNullOrEmpty(value) && regexGuidPattern.IsMatch(value));
        }

        protected internal static bool IsTheThing(IDictionary dic)
        {
            return (dic == usersDic);
        }

        internal static bool LicEdition(SPContext context, LicInfo licInfo, int edition)
        {
            return true;
            //if (!isEnabled)
            //{
            //    return false;
            //}
            //if (licInfo == null)
            //{
            //    licInfo = LicInfo.Get(null);
            //}
            //if (licInfo.expired || licInfo.broken)
            //{
            //    return false;
            //}
            //if (licInfo.userBroken)
            //{
            //    return (edition == 0);
            //}
            //if (string.IsNullOrEmpty(licInfo.name) && ((licInfo.sd == null) || (licInfo.sd.Count <= 3)))
            //{
            //    return !licInfo.expired;
            //}
            //if (((licInfo.siteUsers > 0) && (licInfo.siteUsers <= 10)) && HasMicro)
            //{
            //    return true;
            //}
            //if (((licInfo.expiry > DateTime.MinValue) && (licInfo.expiry <= DateTime.Now)) || (((licInfo.maxUsers > 0) && (licInfo.siteUsers > 0)) && (licInfo.siteUsers > licInfo.maxUsers)))
            //{
            //    return (edition == 0);
            //}
            //return (((edition == 0) || ((licInfo.dic != null) && ((LicEditions(context, licInfo.dic) & edition) == edition))) || ((licInfo.dic == null) && (licInfo.installSpan.Days < l1)));
        }

        public static bool LicEdition(SPContext context, IDictionary dic, int edition)
        {
            return LicEdition(context, LicInfo.Get(dic), edition);
        }

        internal static int LicEditions(SPContext context)
        {
            return LicEditions(context, null);
        }

        internal static int LicEditions(SPContext context, IDictionary lic)
        {
            int def = 0;
            foreach (KeyValuePair<int, string> pair in GetEditions())
            {
                def |= pair.Key;
            }
            return LicInt(context, lic, "f3", def);
        }

        internal static int LicInt(SPContext context, string key, int def)
        {
            return LicInt(context, null, key, def);
        }

        internal static int LicInt(SPContext context, IDictionary lic, string key, int def)
        {
            if (lic == null)
            {
                lic = LicObject(context);
            }
            if (lic != null)
            {
                return int.Parse(lic[key] as string);
            }
            return def;
        }

        internal static string LicName(SPContext context)
        {
            return LicName(context, null);
        }

        internal static string LicName(SPContext context, IDictionary lic)
        {
            if (lic == null)
            {
                lic = LicObject(context);
            }
            if (lic != null)
            {
                return (lic["c"] as string);
            }
            return null;
        }

        internal static IDictionary LicObject(SPContext context)
        {
            IDictionary dictionary2;
            IDictionary status = GetStatus<IDictionary>(context);
            SPSite site = Elevated ? OpenSite(context) : GetSite(context);
            try
            {
                string str;
                if ((status != null) && ((status.Contains(str = site.ID.ToString()) || status.Contains(str = GetFarm(context).Id.ToString())) || status.Contains(str = Guid.Empty.ToString())))
                {
                    return (status[str] as IDictionary);
                }
                dictionary2 = null;
            }
            finally
            {
                if ((site != null) && Elevated)
                {
                    site.Dispose();
                }
            }
            return dictionary2;
        }

        internal static int LicTargetType(SPContext context)
        {
            return LicTargetType(context, null);
        }

        internal static int LicTargetType(SPContext context, IDictionary lic)
        {
            return LicInt(context, lic, "f1", 0);
        }

        internal static int LicUsers(SPContext context)
        {
            return LicUsers(context, null);
        }

        internal static int LicUsers(SPContext context, IDictionary lic)
        {
            return LicInt(context, lic, "f2", 0);
        }

        public static int Loc(string key, CultureInfo lang)
        {
            int c = 0;
            if (lang == null)
            {
                SPFarm farm = GetFarm(GetContext());
                SPSecurity.CatchAccessDeniedException = false;
                if (farm.Properties != null)
                {
                    foreach (DictionaryEntry entry in farm.Properties)
                    {
                        int num;
                        string str = entry.Key + string.Empty;
                        if ((str.Contains("roxority_Shared__") || str.Contains(AssemblyName + "_")) && (((num = str.IndexOf("__", StringComparison.InvariantCultureIgnoreCase)) > 0) && (str.LastIndexOf("__", StringComparison.InvariantCultureIgnoreCase) > num)))
                        {
                            c++;
                        }
                    }
                }
            }
            if (lang != null)
            {
                Elevate(delegate {
                    SPFarm farm = GetFarm(GetContext());
                    SPSecurity.CatchAccessDeniedException = false;
                    if (farm.Properties != null)
                    {
                        foreach (DictionaryEntry entry in farm.Properties)
                        {
                            string str;
                            if ((str = entry.Key + string.Empty).EndsWith("__" + lang.Name) && (string.IsNullOrEmpty(key) ? (str.StartsWith("roxority_Shared__") || str.StartsWith(AssemblyName + "_")) : str.StartsWith(key + "__")))
                            {
                                c++;
                            }
                        }
                    }
                }, true);
            }
            return c;
        }

        public static string Loc(string key, string name, CultureInfo lang)
        {
            string res = null;
            Elevate(delegate {
                int index = lang.Name.IndexOf('-');
                string str = key + "__" + name + "__" + lang.Name.Substring(0, (index > 0) ? index : lang.Name.Length);
                SPFarm farm = GetFarm(GetContext());
                SPSecurity.CatchAccessDeniedException = false;
                if (farm.Properties != null)
                {
                    res = farm.Properties[str] as string;
                }
            }, true);
            return res;
        }

        public static bool Loc(string key, string name, CultureInfo lang, string value)
        {
            SPContext context = GetContext();
            SPFarm farm = GetFarm(context);
            return Loc(context, farm, farm.Properties, key, name, lang, value);
        }

        public static bool Loc(SPContext context, SPFarm farm, Hashtable props, string key, string name, CultureInfo lang, string value)
        {
            bool flag = false;
            string str = key + ((string.IsNullOrEmpty(name) && (lang == null)) ? string.Empty : ("__" + name + "__" + lang.Name));
            SPSecurity.CatchAccessDeniedException = false;
            if (!key.StartsWith("PL_") && !LicEdition(context, (IDictionary) null, IsWhiteLabel ? 0 : 4))
            {
                throw new Exception(GetResource("NopeEd", new object[] { GetResource("Tool_Localizer_Title", new object[0]), "Ultimate" }));
            }
            if (context != null)
            {
                try
                {
                    context.Web.AllowUnsafeUpdates = true;
                }
                catch
                {
                }
            }
            if (props != null)
            {
                try
                {
                    if (string.IsNullOrEmpty(value) && props.ContainsKey(str))
                    {
                        flag = true;
                        props.Remove(str);
                        return flag;
                    }
                    if (!string.IsNullOrEmpty(value) && !value.Equals(props[str]))
                    {
                        flag = true;
                        props[str] = value;
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (SecurityException)
                {
                    throw;
                }
            }
            return flag;
        }

        public static string MergeUrlPaths(string absoluteUrl, string relativeUrl)
        {
            Uri uri;
            int startIndex = 0;
            if (string.IsNullOrEmpty(absoluteUrl))
            {
                return relativeUrl;
            }
            if (string.IsNullOrEmpty(relativeUrl))
            {
                return absoluteUrl;
            }
            if (relativeUrl.StartsWith(absoluteUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                return relativeUrl;
            }
            string str = (uri = new Uri(absoluteUrl)).GetLeftPart(UriPartial.Query).Substring(uri.GetLeftPart(UriPartial.Authority).Length);
            absoluteUrl = absoluteUrl.TrimEnd(new char[] { '/' });
            relativeUrl = relativeUrl.Trim(new char[] { '/' });
            List<string> list = new List<string>(str.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> list2 = new List<string>(relativeUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            for (int i = list.Count - 1; (i >= 0) && (startIndex < list2.Count); i--)
            {
                int num2;
                if (((startIndex + (num2 = list.Count - i)) <= list2.Count) && string.Join("/", list.ToArray(), i, num2).Equals(string.Join("/", list2.ToArray(), startIndex, num2), StringComparison.InvariantCultureIgnoreCase))
                {
                    for (int j = 0; j < num2; j++)
                    {
                        list.RemoveAt(i);
                        startIndex++;
                    }
                }
            }
            return string.Concat(new object[] { uri.GetLeftPart(UriPartial.Authority).Trim(new char[] { '/' }), '/', (list.Count == 0) ? "" : (string.Join("/", list.ToArray()) + '/'), string.Join("/", list2.ToArray()) });
        }

        private static SymmetricAlgorithm NewAlgo()
        {
            return new TripleDESCryptoServiceProvider();
        }

        public static string Normalize(string val)
        {
            bool? nullable = null;
            try
            {
                nullable = new bool?((bool) HttpContext.Current.Items["roxdonormalize"]);
            }
            catch
            {
            }
            if (!nullable.HasValue || !nullable.HasValue)
            {
                try
                {
                    bool? nullable2 = nullable = new bool?(Config<bool>(GetContext(), "DoNormalize"));
                    HttpContext.Current.Items["roxdonormalize"] = nullable2.Value;
                }
                catch
                {
                }
            }
            if (nullable.HasValue && nullable.Value)
            {
                return Regex.Replace(val.Normalize(NormalizationForm.FormD), @"[^\t\n\u001E-\u007F]", string.Empty);
            }
            return val;
        }

        protected override void OnInit(EventArgs e)
        {
            string message = null;
            base.Form.Enctype = "multipart/form-data";
            base.Form.SubmitDisabledControls = true;
            if (((base.Request.RawUrl.Contains("%00") || base.Request.RawUrl.ToLowerInvariant().Contains("%00".ToLowerInvariant())) || (base.Request.RawUrl.ToLowerInvariant().Contains(HttpUtility.UrlDecode("%00").ToLowerInvariant()) || base.Request.RawUrl.ToLowerInvariant().Contains(HttpUtility.UrlDecode("%0023c3e").ToLowerInvariant()))) || (base.Request.RawUrl.Contains(HttpUtility.UrlDecode("%00")) || base.Request.RawUrl.Contains(HttpUtility.UrlDecode("%0023c3e"))))
            {
                base.Response.Redirect("default.aspx", true);
            }
            if (((base.Request["cfg"] == "reset") || (base.Request["cfg"] == "save")) && (this.IsSiteAdmin || this.IsFarmAdmin))
            {
                SPContext.Current.Site.CatchAccessDeniedException = false;
                try
                {
                    if (base.Request["cfg"] == "reset")
                    {
                        ConfigReset(SPContext.Current, base.Request["name"], base.Request["scope"] == "site");
                    }
                    else if ((base.Request["value"] == "true") || (base.Request["value"] == "false"))
                    {
                        Config<bool>(SPContext.Current, base.Request["name"], bool.Parse(base.Request["value"]), base.Request["scope"] == "site");
                    }
                    else
                    {
                        Config<string>(SPContext.Current, base.Request["name"], base.Request["value"], base.Request["scope"] == "site");
                    }
                }
                catch (SqlException exception)
                {
                    message = exception.Message;
                }
                catch
                {
                }
                if (message == null)
                {
                    base.Response.Redirect(SPContext.Current.Site.Url + "/_layouts/" + AssemblyName + "/default.aspx?cfg=cfg&s=" + DateTime.Now.Ticks.ToString() + "#cfg_" + base.Request["name"], true);
                }
                else
                {
                    base.Response.Redirect(SPContext.Current.Site.Url + "/_layouts/" + AssemblyName + "/default.aspx?cfg=cfg&em=" + HttpContext.Current.Server.UrlEncode(message.Replace(",", "").Replace(";", "")).Replace("+", "%20").Replace("'", "%27") + "&s=" + DateTime.Now.Ticks.ToString(), true);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(base.Request["roxgosite"]))
                {
                    using (SPSite site = new SPSite(base.Request["roxgosite"]))
                    {
                        base.Response.Redirect(string.Concat(new object[] { site.Url, "/_layouts/", AssemblyName, "/default.aspx?r=", this.Rnd.Next(), "&", this.IsDocTopic ? ("doc=" + this.DocTopic) : (this.IsCfgTopic ? ("cfg=" + this.CfgTopic + "&tool=" + base.Request["tool"]) : "") }), true);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(base.Request["roxgoweb"]))
                {
                    SPWeb web = GetSite(GetContext()).OpenWeb(new Guid(base.Request["roxgoweb"]));
                    if (web == null)
                    {
                        return;
                    }
                    using (web)
                    {
                        base.Response.Redirect(web.Url.TrimEnd(new char[] { '/' }) + "/_layouts/" + AssemblyName + "/default.aspx" + base.Request.Url.Query.Replace("roxgoweb=" + base.Request["roxgoweb"], string.Empty), true);
                        return;
                    }
                }
                if ((!base.Request.RawUrl.ToLowerInvariant().Contains("default.aspx") || (!string.IsNullOrEmpty(base.Request.Headers["VTI_SCRIPT_NAME"]) && !base.Request.Headers["VTI_SCRIPT_NAME"].ToLowerInvariant().Contains("default.aspx"))) || (!string.IsNullOrEmpty(base.Request.ServerVariables["HTTP_VTI_SCRIPT_NAME"]) && !base.Request.ServerVariables["HTTP_VTI_SCRIPT_NAME"].ToLowerInvariant().Contains("default.aspx")))
                {
                    base.Response.Redirect(base.Request.RawUrl.Substring(0, base.Request.RawUrl.LastIndexOf('/')) + "/default.aspx" + base.Request.Url.Query, true);
                }
                else
                {
                    base.OnInit(e);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            JsonSchemaManager manager = null;
            JsonSchemaManager manager2 = null;
            base.OnPreRenderComplete(e);
            if (!string.IsNullOrEmpty(base.Request["file"]) && !base.Request["file"].EndsWith(".aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    base.Response.Clear();
                    base.Response.ContentType = "application/octet-stream";
                    base.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + base.Request["file"] + "\"");
                    if (base.Request["file"] == (AssemblyName + ".export.rox"))
                    {
                        try
                        {
                            Hashtable hashtable2;
                            SPContext context;
                            base.Response.ContentType = "text/plain";
                            Hashtable json = new Hashtable();
                            SPFarm farm = GetFarm(context = GetContext());
                            foreach (CultureInfo info in AllCultures)
                            {
                                if (((!string.IsNullOrEmpty(info.Name) && !info.Name.Contains("-")) && ((info.Name != "de") && (info.Name != "en"))) && (Loc(null, info) > 0))
                                {
                                    hashtable2 = new Hashtable();
                                    foreach (DictionaryEntry entry in farm.Properties)
                                    {
                                        string str;
                                        if ((str = entry.Key + string.Empty).EndsWith("__" + info.Name) && (str.StartsWith("roxority_Shared__") || str.StartsWith(AssemblyName + "_")))
                                        {
                                            hashtable2[str] = entry.Value;
                                        }
                                    }
                                    json[info.Name] = hashtable2;
                                }
                            }
                            hashtable2 = new Hashtable();
                            Hashtable hashtable3 = new Hashtable();
                            foreach (Dictionary<string, string> dictionary in this.ConfigSettings)
                            {
                                hashtable2[dictionary["name"]] = Config(context, "farm:" + dictionary["name"]);
                                hashtable3[dictionary["name"]] = Config(context, "site:" + dictionary["name"]);
                            }
                            Hashtable hashtable4 = new Hashtable();
                            Hashtable hashtable5 = new Hashtable();
                            foreach (string str4 in JsonSchemaManager.DiscoverSchemaFiles(this.Context))
                            {
                                manager = (JsonSchemaManager) (manager2 = null);
                                string asmName = str4.EndsWith("schemas.tl.json", StringComparison.InvariantCultureIgnoreCase) ? "roxority_Shared" : null;
                                try
                                {
                                    manager = new JsonSchemaManager(this, str4, false, asmName);
                                    if (!this.IsAdminSite)
                                    {
                                        manager2 = new JsonSchemaManager(this, str4, true, asmName);
                                    }
                                }
                                catch
                                {
                                }
                                if (manager != null)
                                {
                                    foreach (KeyValuePair<string, JsonSchemaManager.Schema> pair in manager.AllSchemas)
                                    {
                                        string introduced28 = Path.GetFileName(str4) + ":" + pair.Key;
                                        hashtable4[introduced28] = pair.Value.InstDict;
                                    }
                                }
                                if (manager2 != null)
                                {
                                    foreach (KeyValuePair<string, JsonSchemaManager.Schema> pair2 in manager2.AllSchemas)
                                    {
                                        string introduced29 = Path.GetFileName(str4) + ":" + pair2.Key;
                                        hashtable5[introduced29] = pair2.Value.InstDict;
                                    }
                                }
                            }
                            json["farm"] = hashtable2;
                            json["site"] = hashtable3;
                            if (hashtable4.Count > 0)
                            {
                                json["fjs"] = hashtable4;
                            }
                            if (hashtable5.Count > 0)
                            {
                                json["sjs"] = hashtable5;
                            }
                            string s = JSON.JsonEncode(json);
                            base.Response.Write(s);
                        }
                        catch (Exception exception)
                        {
                            base.Response.Write(exception.ToString());
                        }
                    }
                    else
                    {
                        base.Response.WriteFile(base.Server.MapPath(base.Request["file"]));
                    }
                    base.Response.End();
                }
                finally
                {
                    if ((manager2 != manager) && (manager2 != null))
                    {
                        manager2.Dispose();
                    }
                    if (manager != null)
                    {
                        manager.Dispose();
                    }
                }
            }
        }

        internal static SPSite OpenSite(SPContext context)
        {
            SPSite site = ((context == null) && ((context = GetContext()) == null)) ? ((currentSite == null) ? null : new SPSite(currentSite.ID)) : new SPSite(context.Site.ID);
            HttpContext current = null;
            try
            {
                current = HttpContext.Current;
            }
            catch
            {
            }
            try
            {
                if ((site == null) && (current != null))
                {
                    SPWebApplication application;
                    if (((application = SPWebApplication.Lookup(current.Request.Url)) == null) || (application.Sites.Count == 0))
                    {
                        throw new SPException(GetResource("NoSite", new object[] { AssemblyName, GetTitle() }));
                    }
                    using (SPSite site2 = application.Sites[0])
                    {
                        current.Response.Redirect(site2.Url.TrimEnd(new char[] { '/' }) + current.Request.Url.ToString().Substring(current.Request.Url.ToString().ToLowerInvariant().IndexOf("/_layouts/" + AssemblyName.ToLowerInvariant() + "/default.aspx")), true);
                    }
                }
            }
            catch
            {
            }
            if (site != null)
            {
                site.CatchAccessDeniedException = false;
            }
            return site;
        }

        public static bool ParseBool(string value)
        {
            if (!"1".Equals(value, StringComparison.InvariantCultureIgnoreCase) && !"true".Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return "yes".Equals(value, StringComparison.InvariantCultureIgnoreCase);
            }
            return true;
        }

        internal static void RemoveDuplicates<T>(List<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list.IndexOf(list[i]) < i)
                {
                    list.RemoveAt(i);
                }
            }
        }

        internal static void RemoveDuplicates<TKey, TValue>(List<KeyValuePair<TKey, TValue>> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list.FindIndex(delegate (KeyValuePair<TKey, TValue> val) {
                    KeyValuePair<TKey, TValue> pair = list[i];
                    if (object.Equals(val.Key, pair.Key))
                    {
                        KeyValuePair<TKey, TValue> pair2 = list[i];
                        return object.Equals(val.Value, pair2.Value);
                    }
                    return false;
                }) < i)
                {
                    list.RemoveAt(i);
                }
            }
        }

        public static string Serialize<T>(IEnumerable<T> values)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Formatter.Serialize(stream, new Serializables<T>(values));
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        internal static void SetFieldProp(SPField field, string name, object val)
        {
            LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot(field.GetType().Name + "_" + name);
            field.SetCustomProperty(name, val);
            if (namedDataSlot != null)
            {
                Thread.SetData(namedDataSlot, val);
            }
        }

        internal static string StripID(string value)
        {
            int index = value.IndexOf(";#");
            if (index < 0)
            {
                return value;
            }
            return value.Substring(index + ";#".Length);
        }

        private static byte[] ToByteArray(Guid id, string value, int flag1, int flag2, int flag3, long flag4)
        {
            List<byte> list = new List<byte>();
            StringBuilder builder = new StringBuilder();
            list.AddRange(id.ToByteArray());
            list.Add((byte) flag1);
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(flag2);
                    list.AddRange(stream.ToArray());
                }
            }
            list.Add((byte) flag3);
            if (flag4 <= 0L)
            {
                goto Label_00BC;
            }
            using (MemoryStream stream2 = new MemoryStream())
            {
                using (BinaryWriter writer2 = new BinaryWriter(stream2))
                {
                    writer2.Write(flag4);
                    list.AddRange(stream2.ToArray());
                }
                goto Label_00BC;
            }
        Label_00AA:
            value = value.Replace("  ", " ");
        Label_00BC:
            if (value.Contains("  "))
            {
                goto Label_00AA;
            }
            builder.Append(value.ToLowerInvariant().Trim());
            for (int i = 0; i < builder.Length; i++)
            {
                if (!char.IsLetterOrDigit(builder[i]))
                {
                    builder[i] = '_';
                }
            }
            list.AddRange(Encoding.Unicode.GetBytes(builder.ToString()));
            return list.ToArray();
        }

        private static byte[] Trans(ICryptoTransform ct, byte[] sb)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, ct, CryptoStreamMode.Write))
                {
                    stream2.Write(sb, 0, sb.Length);
                    stream2.FlushFinalBlock();
                }
                return stream.ToArray();
            }
        }

        public static string Trim(object value, params char[] trimChars)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if ((trimChars != null) && (trimChars.Length != 0))
            {
                return value.ToString().Trim(trimChars);
            }
            return value.ToString().Trim();
        }

        public static IEnumerable<T> TryEach<T>(IEnumerable collection) where T: class
        {
            return TryEach<T>(collection, false, null, false);
        }

        public static IEnumerable<T> TryEach<T>(IEnumerable collection, bool yieldNulls, Action<Exception> handler, bool elevate) where T: class
        {
            T obj;
            SPSite site;
            bool error = false;
            bool catchAccessDeniedException = SPSecurity.CatchAccessDeniedException;
            IEnumerator en = null;
            List<T> list = new List<T>();
            SPSecurity.CatchAccessDeniedException = false;
            if (SPContext.Current != null)
            {
                SPContext.Current.Site.CatchAccessDeniedException = false;
            }
            SPSecurity.CodeToRunElevated secureCode = delegate {
                try
                {
                    en = collection.GetEnumerator();
                }
                catch (Exception exception)
                {
                    if (handler != null)
                    {
                        handler(exception);
                    }
                }
                if (en != null)
                {
                    while (en.MoveNext())
                    {
                        try
                        {
                            error = false;
                            obj = en.Current as T;
                            site = obj as SPSite;
                            if (site != null)
                            {
                                site.CatchAccessDeniedException = false;
                                site.ID.ToString();
                                site.Url.ToString();
                                site.RootWeb.ToString();
                                site.RootWeb.Title.ToString();
                            }
                        }
                        catch (Exception exception2)
                        {
                            error = true;
                            obj = default(T);
                            if (handler != null)
                            {
                                handler(exception2);
                            }
                        }
                        if (!error && ((obj != null) || yieldNulls))
                        {
                            list.Add(obj);
                        }
                    }
                }
            };
            if (elevate && !Elevated)
            {
                try
                {
                    Elevated = true;
                    SPSecurity.RunWithElevatedPrivileges(secureCode);
                    goto Label_0089;
                }
                finally
                {
                    Elevated = false;
                }
            }
            secureCode();
        Label_0089:
            SPSecurity.CatchAccessDeniedException = catchAccessDeniedException;
            return list;
        }

        internal static void UpdateField(SPField field, params string[] customProperties)
        {
            foreach (string str in customProperties)
            {
                PropertyInfo property = field.GetType().GetProperty(str);
                if (property != null)
                {
                    SetFieldProp(field, str, property.GetValue(field, null));
                }
            }
        }

        private static void UpdateInfo(IDictionary value)
        {
            if (value != null)
            {
                DateTime time = new DateTime((long) value[tk]);
                value["is"] = DateTime.Today.Subtract(time).Ticks;
                value["ed"] = time.AddDays((double) l1);
            }
        }

        private static bool UpdateStatus(object d, bool ignoreErrors)
        {
            return UpdateStatus(d, ignoreErrors, false, true, GetMapping(), os);
        }

        internal static bool UpdateStatus(object d, bool ignoreErrors, bool unlessExists, bool web, string m, byte[] os)
        {
            byte[] buffer;
            string str;
            SPSecurity.CodeToRunElevated secureCode = null;
            object obj2 = null;
            bool flag = false;
            SPPersistedObject pobj = GetAdminApplication();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, d);
                buffer = stream.ToArray();
            }
            using (SymmetricAlgorithm algorithm = NewAlgo())
            {
                using (ICryptoTransform transform = algorithm.CreateEncryptor(GetRange<byte>(os, os.Length - 0x18, 0x18), GetRange<byte>(os, 0, 0x10)))
                {
                    buffer = Trans(transform, buffer);
                }
            }
            if (web)
            {
                SPContext.Current.Web.AllowUnsafeUpdates = SPContext.Current.Site.AllowUnsafeUpdates = true;
                SPContext.Current.Site.CatchAccessDeniedException = false;
            }
            SPSecurity.CatchAccessDeniedException = false;
            obj2 = pobj.Properties[str = pobj.GetType().Name.Replace("SP", string.Empty) + m];
            if ((unlessExists && (obj2 != null)) && !string.IsNullOrEmpty(obj2.ToString()))
            {
                return false;
            }
            pobj.Properties[str] = buffer;
            if (ignoreErrors)
            {
                try
                {
                    try
                    {
                        pobj.Update(true);
                    }
                    catch (SqlException)
                    {
                        flag = true;
                        throw;
                    }
                    return flag;
                }
                catch
                {
                    try
                    {
                        if (secureCode == null)
                        {
                            secureCode = delegate {
                                pobj.Update(true);
                            };
                        }
                        SPSecurity.RunWithElevatedPrivileges(secureCode);
                    }
                    catch
                    {
                    }
                    return flag;
                }
            }
            try
            {
                pobj.Update(true);
            }
            catch (SqlException exception)
            {
                try
                {
                    pobj.Properties[str] = obj2;
                    pobj.Update(true);
                }
                catch
                {
                }
                throw new Exception(GetResource("FarmAdminError", new object[] { HttpContext.Current.Server.HtmlEncode(exception.Message) }) + (exception.Message.Contains("EXECUTE") ? GetResource("FarmAdminErrorNoServer", new object[0]) : string.Empty));
            }
            catch
            {
                try
                {
                    pobj.Properties[str] = obj2;
                    pobj.Update(true);
                }
                catch
                {
                }
                throw;
            }
            return flag;
        }

        private static int Verify(RSACryptoServiceProvider rsa, byte[] license, Guid id, string value, int flag1, int flag2, int flag3, long flag4)
        {
            byte[] bytes = new byte[] { 0x6d, 0, 0x73, 0, 0x68, 0, 0x61, 0, 100, 0 };
            byte[] buffer = ToByteArray(id, value, flag1, flag2, flag3, flag4);
            List<byte> list = new List<byte>(license);
            int num = 5;
            if (rsa.VerifyData(buffer, Encoding.Unicode.GetString(bytes, 0, 2) + Encoding.Unicode.GetString(bytes, 8, 2) + num.ToString(), list.GetRange(0x80, 0x80).ToArray()))
            {
                int num2 = 1;
                if (rsa.VerifyData(buffer, Encoding.Unicode.GetString(bytes, 2, 6) + num2.ToString(), list.GetRange(0, 0x80).ToArray()))
                {
                    return 0x80;
                }
            }
            return 0x100;
        }

        public SPWebApplication AdminApp
        {
            get
            {
                if (this.adminApp == null)
                {
                    this.adminApp = GetAdminApplication();
                }
                return this.adminApp;
            }
        }

        public SPSite AdminSite
        {
            get
            {
                if (this.adminSite == null)
                {
                    this.adminSite = GetAdminSite();
                }
                return this.adminSite;
            }
        }

        public static List<CultureInfo> AllCultures
        {
            get
            {
                if (allCultures == null)
                {
                    allCultures = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures));
                    allCultures.Sort((Comparison<CultureInfo>) ((one, two) => one.DisplayName.CompareTo(two.DisplayName)));
                }
                return allCultures;
            }
        }

        public static List<CultureInfo> AllSpecificCultures
        {
            get
            {
                if (allSpecificCultures == null)
                {
                    allSpecificCultures = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
                    allSpecificCultures.Sort((Comparison<CultureInfo>) ((one, two) => one.DisplayName.CompareTo(two.DisplayName)));
                }
                return allSpecificCultures;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> Breadcrumb
        {
            get
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                SPWeb parentWeb = SPContext.Current.Web;
                Guid iD = parentWeb.ID;
                if ("tools".Equals(base.Request.QueryString["cfg"], StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(base.Request.QueryString["tool"]))
                {
                    list.Insert(0, new KeyValuePair<string, string>("?cfg=tools&r=" + this.Rnd.Next(), GetResource("ToolsFrame", new object[0])));
                }
                if (this.IsDocTopic)
                {
                    list.Insert(0, new KeyValuePair<string, string>("?doc=intro&r=" + this.Rnd.Next(), GetResource("Tab_Help", new object[0])));
                }
                else
                {
                    list.Insert(0, new KeyValuePair<string, string>("?r=" + this.Rnd.Next(), GetResource("Tab_Info", new object[0])));
                }
                list.Insert(0, new KeyValuePair<string, string>(base.Request.RawUrl.Substring(0, (base.Request.RawUrl.IndexOf('?') > 0) ? base.Request.RawUrl.IndexOf('?') : base.Request.RawUrl.Length) + "?r=" + this.Rnd.Next(), this["HelpTitle", new object[] { this.ProductName }]));
                list.Insert(0, new KeyValuePair<string, string>(base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("/_layouts/", StringComparison.InvariantCultureIgnoreCase)) + "/_layouts/settings.aspx", "Site Settings"));
                while (parentWeb != null)
                {
                    list.Insert(0, new KeyValuePair<string, string>(parentWeb.Url, parentWeb.Title));
                    try
                    {
                        parentWeb = parentWeb.ParentWeb;
                    }
                    catch
                    {
                        try
                        {
                            parentWeb = parentWeb.Site.RootWeb;
                        }
                        catch
                        {
                            parentWeb = null;
                        }
                    }
                    if (parentWeb != null)
                    {
                        if (parentWeb.ID == iD)
                        {
                            parentWeb = null;
                        }
                        else
                        {
                            iD = parentWeb.ID;
                        }
                    }
                }
                return list;
            }
        }

        public string CfgTopic
        {
            get
            {
                return base.Request["cfg"];
            }
        }

        public Dictionary<string, string> ConfigGroups
        {
            get
            {
                if (cfgGroups == null)
                {
                    string str;
                    cfgGroups = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(str = GetProductResource("_SettingGroups", new object[0])))
                    {
                        foreach (string str2 in str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            cfgGroups[str2.Substring(str2.IndexOf(':') + 1)] = str2.Substring(0, str2.IndexOf(':'));
                        }
                    }
                }
                return cfgGroups;
            }
        }

        public IEnumerable<Dictionary<string, string>> ConfigSettings
        {
            get
            {
                string[] iteratorVariable6;
                string productResource = GetProductResource("_Settings", new object[0]);
                string iteratorVariable5 = "[=" + this["Auto", new object[0]] + "|en-US=English|de-DE=Deutsch|zh-CN=中文简体";
                Dictionary<string, string> iteratorVariable7 = new Dictionary<string, string>();
                iteratorVariable7["name"] = "_lang";
                foreach (CultureInfo info in AllCultures)
                {
                    if (((!string.IsNullOrEmpty(info.Name) && !info.Name.Contains("-")) && ((info.Name != "de") && (info.Name != "en"))) && (Loc(string.Empty, info) > 0))
                    {
                        string str2 = iteratorVariable5;
                        iteratorVariable5 = str2 + "|" + info.Name + "=" + info.DisplayName;
                    }
                }
                iteratorVariable7["type"] = iteratorVariable5 + "]";
                iteratorVariable7["title"] = this["CfgSettingTitle__lang", new object[0]];
                iteratorVariable7["desc"] = this["CfgSettingDesc__lang", new object[] { this.ProductName }];
                yield return iteratorVariable7;
                if ((!string.IsNullOrEmpty(productResource) && ((iteratorVariable6 = productResource.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) != null)) && (iteratorVariable6.Length > 0))
                {
                    foreach (string iteratorVariable8 in iteratorVariable6)
                    {
                        string iteratorVariable3;
                        iteratorVariable7 = new Dictionary<string, string>();
                        iteratorVariable7["name"] = iteratorVariable8.Substring(0, iteratorVariable8.IndexOf(':'));
                        iteratorVariable7["type"] = iteratorVariable8.Substring(iteratorVariable8.IndexOf(':') + 1);
                        iteratorVariable7["title"] = GetProductResource("CfgSettingTitle_" + iteratorVariable7["name"], new object[0]);
                        iteratorVariable7["caption"] = GetProductResource("CfgSettingCaption_" + iteratorVariable7["name"], new object[0]);
                        iteratorVariable7["desc"] = GetProductResource("CfgSettingDesc_" + iteratorVariable7["name"], new object[0]);
                        iteratorVariable7["default"] = string.IsNullOrEmpty(iteratorVariable3 = GetProductResource("CfgSettingDef_" + iteratorVariable7["name"], new object[0])) ? string.Empty : iteratorVariable3;
                        if (iteratorVariable7["type"].StartsWith("[") && iteratorVariable7["type"].EndsWith("]"))
                        {
                            bool iteratorVariable0;
                            if (iteratorVariable0 = (iteratorVariable5 = iteratorVariable7["type"].Substring(1, iteratorVariable7["type"].Length - 2)).StartsWith("*", StringComparison.InvariantCultureIgnoreCase))
                            {
                                iteratorVariable5 = iteratorVariable5.Substring(1);
                            }
                            string iteratorVariable4 = string.Empty;
                            foreach (string str in iteratorVariable5.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (str.IndexOf('=') >= 0)
                                {
                                    iteratorVariable4 = iteratorVariable4 + str + '|';
                                }
                                else
                                {
                                    string iteratorVariable1 = GetProductResource("CfgSettingChoice_" + iteratorVariable7["name"] + "_" + str, new object[0]);
                                    object obj2 = iteratorVariable4;
                                    iteratorVariable4 = string.Concat(new object[] { obj2, str, '=', string.IsNullOrEmpty(iteratorVariable1) ? str : iteratorVariable1, '|' });
                                }
                            }
                            iteratorVariable7["type"] = '[' + iteratorVariable4.Substring(0, iteratorVariable4.Length - 1) + ']';
                            if (iteratorVariable0)
                            {
                                iteratorVariable7["multiSel"] = string.Empty;
                            }
                        }
                        yield return iteratorVariable7;
                    }
                }
                if (!string.IsNullOrEmpty(GetProductResource("_hasjquery", new object[0])) && (Is14 || (GetProductResource("_hasjquery", new object[0]) != "14")))
                {
                    iteratorVariable7["name"] = "_nojquery";
                    iteratorVariable7["type"] = "bool";
                    iteratorVariable7["caption"] = this["CfgSettingCaption__nojquery", new object[0]];
                    iteratorVariable7["title"] = this["CfgSettingTitle__nojquery", new object[0]];
                    iteratorVariable7["desc"] = this["CfgSettingDesc__nojquery", new object[] { this.ProductName }];
                    yield return iteratorVariable7;
                }
            }
        }

        public int ConfigSettingsCount
        {
            get
            {
                string[] strArray;
                string productResource = GetProductResource("_Settings", new object[0]);
                return ((string.IsNullOrEmpty(GetProductResource("_hasjquery", new object[0])) ? 1 : 2) + ((!string.IsNullOrEmpty(productResource) && ((strArray = productResource.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) != null)) ? strArray.Length : 0));
            }
        }

        public static string DisplayVersion
        {
            get
            {
                System.Version version = Version;
                return string.Format("{0}.{1}", version.Major, version.Minor);
            }
        }

        public string DocTopic
        {
            get
            {
                return base.Request["doc"];
            }
        }

        public List<KeyValuePair<int, string>> Editions
        {
            get
            {
                return GetEditions();
            }
        }

        public SPFarm Farm
        {
            get
            {
                if (this.farm == null)
                {
                    this.farm = GetFarm(SPContext.Current);
                }
                return this.farm;
            }
        }

        internal static CultureInfo farmCulture
        {
            get
            {
                return (HttpContext.Current.Items["farmCulture"] as CultureInfo);
            }
            set
            {
                HttpContext.Current.Items["farmCulture"] = value;
            }
        }

        public static bool HasMicro
        {
            get
            {
                return string.IsNullOrEmpty(GetProductResource("_NoMicro", new object[0]));
            }
        }

        public static string HivePath
        {
            get
            {
                string defaultValue = @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\" + (Is14 ? @"14\" : @"12\");
                HttpContext context = null;
                try
                {
                    if (((context = HttpContext.Current) != null) && (context.Server == null))
                    {
                        context = null;
                    }
                }
                catch
                {
                }
                if (context != null)
                {
                    try
                    {
                        return (new DirectoryInfo(context.Server.MapPath("/_layouts")).Parent.Parent.FullName.TrimEnd(new char[] { '\\' }) + @"\");
                    }
                    catch
                    {
                    }
                }
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\" + (Is14 ? "14" : "12") + ".0", false))
                    {
                        defaultValue = key.GetValue("Location", defaultValue, RegistryValueOptions.None) + string.Empty;
                    }
                }
                catch
                {
                }
                return defaultValue;
            }
        }

        public static bool Is14
        {
            get
            {
                if (!is14.HasValue || !is14.HasValue)
                {
                    try
                    {
                        SPFarm farm = GetFarm(GetContext());
                        if (farm != null)
                        {
                            is14 = new bool?(farm.BuildVersion.Major > 12);
                        }
                    }
                    catch
                    {
                    }
                    is14 = new bool?(typeof(SPWeb).Assembly.FullName.IndexOf("Version=14") > 0);
                }
                if (is14.HasValue)
                {
                    return is14.Value;
                }
                return false;
            }
        }

        public bool IsAdminSite
        {
            get
            {
                try
                {
                    return SPContext.Current.Site.ID.Equals(this.AdminSite.ID);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsAnyAdmin
        {
            get
            {
                if (!this.isAnyAdmin.HasValue || !this.isAnyAdmin.HasValue)
                {
                    this.isAnyAdmin = new bool?(this.IsFarmAdmin || this.IsSiteAdmin);
                }
                return this.isAnyAdmin.Value;
            }
        }

        public bool IsApplicableAdmin
        {
            get
            {
                if (!this.isAppAdmin.HasValue || !this.isAppAdmin.HasValue)
                {
                    this.isAppAdmin = new bool?(this.IsAdminSite ? this.IsFarmAdmin : this.IsSiteAdmin);
                }
                return this.isAppAdmin.Value;
            }
        }

        public bool IsCfgTopic
        {
            get
            {
                return !string.IsNullOrEmpty(this.CfgTopic);
            }
        }

        public bool IsDocTopic
        {
            get
            {
                return !string.IsNullOrEmpty(this.DocTopic);
            }
        }

        public bool IsFarmAdmin
        {
            get
            {
                if (!this.isFarmAdmin.HasValue || !this.isFarmAdmin.HasValue)
                {
                    try
                    {
                        SPContext context;
                        bool? nullable2 = this.isFarmAdmin = new bool?(IsFarmAdministrator(GetFarm(context = GetContext())));
                        if (nullable2.Value)
                        {
                            SPSite site = Elevated ? OpenSite(context) : GetSite(context);
                            this.IsFarmError = !site.WebApplication.IsAdministrationWebApplication;
                            if (Elevated)
                            {
                                site.Dispose();
                            }
                        }
                    }
                    catch
                    {
                        this.isFarmAdmin = false;
                    }
                }
                return this.isFarmAdmin.Value;
            }
        }

        public bool IsFullAdmin
        {
            get
            {
                return (this.IsFarmAdmin && this.IsSiteAdmin);
            }
        }

        public bool IsFullFarmAdmin
        {
            get
            {
                return (this.IsFarmAdmin && !this.IsFarmError);
            }
        }

        public bool IsSiteAdmin
        {
            get
            {
                SPSite site = null;
                if (!this.isSiteAdmin.HasValue || !this.isSiteAdmin.HasValue)
                {
                    this.isSiteAdmin = false;
                    SPSecurity.CatchAccessDeniedException = false;
                    try
                    {
                        SPContext context;
                        if (((context = GetContext()) != null) && ((site = Elevated ? OpenSite(context) : GetSite(context)) != null))
                        {
                            site.CatchAccessDeniedException = false;
                            this.isSiteAdmin = new bool?(site.RootWeb.UserIsSiteAdmin);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (Elevated && (site != null))
                        {
                            site.Dispose();
                        }
                    }
                }
                return this.isSiteAdmin.Value;
            }
        }

        public static bool IsWhiteLabel
        {
            get
            {
                return !"roxority_".Equals(GetProductResource("_WhiteLabel", new object[0]));
            }
        }

        public string this[int key]
        {
            get
            {
                return GetEdition(key);
            }
        }

        public string this[string resKey, params object[] args]
        {
            get
            {
                return GetResource(Resources, resKey, args);
            }
        }

        public string ProductName
        {
            get
            {
                return GetTitle();
            }
        }

        internal static CultureInfo siteCulture
        {
            get
            {
                return (HttpContext.Current.Items["siteCulture"] as CultureInfo);
            }
            set
            {
                HttpContext.Current.Items["siteCulture"] = value;
            }
        }

        protected internal IDictionary Status
        {
            get
            {
                return GetStatus<Dictionary<string, object>>(SPContext.Current);
            }
        }

        public string TopicContent
        {
            get
            {
                try
                {
                    int num = -1;
                    int num2 = 0;
                    string str2 = string.Empty;
                    string str3 = string.Empty;
                    string str4 = string.Empty;
                    CultureInfo farmCulture = GetFarmCulture(SPContext.Current);
                    StringBuilder builder = new StringBuilder();
                    if (farmCulture != null)
                    {
                        if (System.IO.File.Exists(str2 = base.Server.MapPath("help/" + farmCulture.Name + "/" + this.DocTopic + ".html")))
                        {
                            str3 = farmCulture.Name + @"\";
                        }
                        else if ((farmCulture.Parent != null) && System.IO.File.Exists(str2 = base.Server.MapPath("help/" + farmCulture.Parent.Name + "/" + this.DocTopic + ".html")))
                        {
                            str3 = farmCulture.Parent.Name + @"\";
                        }
                    }
                    if (string.IsNullOrEmpty(str2) || !System.IO.File.Exists(str2))
                    {
                        str2 = base.Server.MapPath("help/" + this.DocTopic + ".html");
                    }
                    using (StreamReader reader = System.IO.File.OpenText(str2))
                    {
                        builder.Append(reader.ReadToEnd());
                    }
                    if (builder.Length != 0)
                    {
                        goto Label_0BFC;
                    }
                    if (this.DocTopic == "farm_site_config")
                    {
                        using (StreamWriter writer = System.IO.File.CreateText(str2))
                        {
                            writer.WriteLine(GetResource("ConfigHelp_Intro", new object[] { this.ProductName, AssemblyName }));
                            foreach (Dictionary<string, string> dictionary in this.ConfigSettings)
                            {
                                string str;
                                string[] strArray;
                                writer.WriteLine("<h3>{0}</h3>", base.Server.HtmlEncode(dictionary["title"]).Replace("'", "&#39;"));
                                writer.WriteLine("<p>{0}</p>", dictionary["desc"].Replace("'", "&#39;").Replace(this["ConfigHelp_ReplOld", new object[0]], this["ConfigHelp_ReplNew", new object[] { this.ProductName }]).Replace("\x00c4", "&Auml;").Replace("\x00d6", "&Ouml;").Replace("\x00dc", "&Uuml;").Replace("\x00e4", "&auml;").Replace("\x00f6", "&ouml;").Replace("\x00fc", "&uuml;").Replace("\x00df", "&szlig;"));
                                if ((dictionary["type"].StartsWith("[") && dictionary["type"].EndsWith("]")) && (((strArray = dictionary["type"].Substring(1, dictionary["type"].Length - 2).Split(new char[] { '|' })) != null) && (strArray.Length > 0)))
                                {
                                    writer.WriteLine(GetResource("ConfigHelp_Choice", new object[] { this.ProductName }));
                                    writer.WriteLine("<ul>");
                                    foreach (string str6 in strArray)
                                    {
                                        writer.WriteLine("<li><i>{0}</i></li>", base.Server.HtmlEncode(str6.Substring(str6.IndexOf('=') + 1)).Replace("'", "&#39;"));
                                    }
                                    writer.WriteLine("</ul>");
                                }
                                if (!string.IsNullOrEmpty(str = GetProductResource("CfgSettingDef_" + dictionary["name"], new object[0])))
                                {
                                    writer.WriteLine(GetResource("ConfigHelp_Default", new object[] { GetResource("CfgReset", new object[0]), this.ProductName }));
                                    writer.WriteLine("<pre>{0}</pre>", HttpUtility.HtmlEncode(str).Replace("'", "&#39;").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<br/>"));
                                }
                            }
                        }
                        System.IO.File.Copy(str2, @"C:\Users\roxor\Documents\Visual Studio 2010\Projects\" + AssemblyName + @"\" + AssemblyName + @"\12\Template\layouts\" + AssemblyName + @"\help\" + str3 + "farm_site_config.html", true);
                        return this.TopicContent;
                    }
                    if (!this.DocTopic.StartsWith("itemref_"))
                    {
                        goto Label_0BFC;
                    }
                    using (JsonSchemaManager manager = new JsonSchemaManager(this, null, true, "itemref_DataSources".Equals(this.DocTopic) ? "roxority_Shared" : null))
                    {
                        JsonSchemaManager.Schema schema = manager.AllSchemas[this.DocTopic.Substring("itemref_".Length)];
                        using (StreamWriter writer2 = System.IO.File.CreateText(str2))
                        {
                            writer2.WriteLine(GetResource("ConfigHelp_ItemIntro", new object[] { this.ProductName, GetProductResource("Tool_" + schema.Name + "_Title", new object[0]), GetProductResource("Tool_" + schema.Name + "_TitleSingular", new object[0]) }) + "<span>");
                            foreach (JsonSchemaManager.Property property in schema.GetPropertiesNoDuplicates())
                            {
                                string str5;
                                if ((property.Tab != str4) && (schema.PropTabs.Count > 1))
                                {
                                    writer2.WriteLine("</span><h4 class=\"rox-h4 rox-doctab rox-doctab-" + (str4 = property.Tab) + "\"><a href=\"#\" onclick=\"roxTogDocTab('" + property.Tab + "');\">{0}</a></h4><span class=\"rox-doctab rox-doctab-" + property.Tab + "\">", base.Server.HtmlEncode(this["ConfigHelp_ItemTab", new object[] { schema.PropTabs[property.Tab] }]).Replace("'", "&#39;"));
                                }
                                writer2.WriteLine("<h3>{0}</h3>", base.Server.HtmlEncode(property.ToString()).Replace("'", "&#39;"));
                                if (!string.IsNullOrEmpty(str5 = this["ConfigHelp_ItemType" + property.PropertyType.GetType().Name, new object[0]]))
                                {
                                    writer2.Write("<p><b>(" + base.Server.HtmlEncode(str5) + ")</b></p>");
                                }
                                writer2.WriteLine("<p>{0}</p>", property.Description.Replace("'", "&#39;").Replace("\x00c4", "&Auml;").Replace("\x00d6", "&Ouml;").Replace("\x00dc", "&Uuml;").Replace("\x00e4", "&auml;").Replace("\x00f6", "&ouml;").Replace("\x00fc", "&uuml;").Replace("\x00df", "&szlig;"));
                                if (!string.IsNullOrEmpty(str5 = this["ConfigHelp_Item" + property.PropertyType.GetType().Name, new object[0]]) || !string.IsNullOrEmpty(str5 = this["ConfigHelp_Item" + property.PropertyType.GetType().BaseType.Name, new object[0]]))
                                {
                                    writer2.WriteLine("<p>{0}</p>", str5.Replace("'", "&#39;").Replace("\x00c4", "&Auml;").Replace("\x00d6", "&Ouml;").Replace("\x00dc", "&Uuml;").Replace("\x00e4", "&auml;").Replace("\x00f6", "&ouml;").Replace("\x00fc", "&uuml;").Replace("\x00df", "&szlig;"));
                                }
                                JsonSchemaManager.Property.Type.Choice propertyType = property.PropertyType as JsonSchemaManager.Property.Type.Choice;
                                if (propertyType != null)
                                {
                                    writer2.WriteLine("<ul>");
                                    foreach (object obj2 in propertyType.GetChoices(property.RawSchema))
                                    {
                                        if (string.IsNullOrEmpty(str5 = propertyType.GetChoiceDesc(property, obj2)))
                                        {
                                            writer2.WriteLine("<li>" + HttpUtility.HtmlEncode(propertyType.GetChoiceTitle(property, obj2)) + "</li>");
                                        }
                                        else
                                        {
                                            writer2.WriteLine("<li><b>" + HttpUtility.HtmlEncode(propertyType.GetChoiceTitle(property, obj2)) + "</b><br/>&mdash; <i>" + HttpUtility.HtmlEncode(str5) + "</i></li>");
                                        }
                                    }
                                    writer2.WriteLine("</ul>");
                                }
                                if (((!property.PropertyType.IsBool && !(property.PropertyType is JsonSchemaManager.Property.Type.Choice)) && (!(property.PropertyType is JsonSchemaManager.Property.Type.DictChoice) && !(property.PropertyType is DataFields))) && !string.IsNullOrEmpty(property.DefaultValue + string.Empty))
                                {
                                    writer2.WriteLine("<p>" + HttpUtility.HtmlEncode(GetResource("ConfigHelp_ItemDef", new object[] { GetProductResource("Tool_" + this.DocTopic.Substring(this.DocTopic.IndexOf('_') + 1) + "_Title", new object[0]) })) + "</p>");
                                    writer2.WriteLine("<pre>" + HttpUtility.HtmlEncode(property.DefaultValue + string.Empty).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") + "</pre>");
                                }
                            }
                            writer2.WriteLine("</span>");
                            writer2.Flush();
                            writer2.Close();
                        }
                    }
                    System.IO.File.Copy(str2, @"C:\Users\roxor\Documents\Visual Studio 2010\Projects\" + AssemblyName + @"\" + AssemblyName + @"\12\Template\layouts\" + AssemblyName + @"\help\" + str3 + this.DocTopic + ".html", true);
                    return this.TopicContent;
                Label_0BDD:
                    builder.Insert(num + 4, string.Format("<a name=\"s{0}\"></a>", num2++));
                Label_0BFC:
                    if ((num = builder.ToString().IndexOf("<h3>", num + 1, StringComparison.InvariantCultureIgnoreCase)) >= 0)
                    {
                        goto Label_0BDD;
                    }
                    return builder.ToString();
                }
                catch (Exception exception)
                {
                    return ("<div class=\"ms-error\">" + exception.Message + "</div>");
                }
            }
        }

        public static System.Version Version
        {
            get
            {
                return new System.Version(((AssemblyFileVersionAttribute) Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true)[0]).Version);
            }
        }

        public static string WebVersion
        {
            get
            {
                string str;
                SPSecurity.CodeToRunElevated code = null;
                XmlNode node;
                long ticks = DateTime.MinValue.Ticks;
                XmlDocument doc = new XmlDocument();
                if (string.IsNullOrEmpty(HttpContext.Current.Application[GetTitle() + "_wv"] as string))
                {
                    HttpContext.Current.Application[GetTitle() + "_wv"] = DisplayVersion;
                }
                if ((string.IsNullOrEmpty(str = HttpContext.Current.Application[GetTitle() + "_lc"] as string) || !long.TryParse(str, out ticks)) || (TimeSpan.FromDays(1.0).Ticks < (DateTime.Now.Ticks - ticks)))
                {
                    try
                    {
                        HttpContext.Current.Application[GetTitle() + "_lc"] = DateTime.Now.Ticks.ToString();
                        SPSecurity.CatchAccessDeniedException = false;
                        if (code == null)
                        {
                            code = delegate {
                                ParameterizedThreadStart start = null;
                                try
                                {
                                    if (start == null)
                                    {
                                        start = delegate (object appState) {
                                            HttpApplicationState state = appState as HttpApplicationState;
                                            if (state != null)
                                            {
                                                try
                                                {
                                                    using (WebClient client = new WebClient())
                                                    {
                                                        string str111;
                                                        if (!string.IsNullOrEmpty(str111 = client.DownloadString("http://roxority.com/storage/sharepoint/" + GetTitle().ToLowerInvariant() + "/" + GetProductResource("_WhiteLabel", new object[0]) + GetTitle() + ".xml")) && str111.Contains("<"))
                                                        {
                                                            while (!str111.StartsWith("<", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                str111 = str111.Substring(str111.IndexOf('<'));
                                                            }
                                                            doc.LoadXml(str111);
                                                        }
                                                    }
                                                    node = doc.SelectSingleNode("//Program_Version");
                                                    if (node != null)
                                                    {
                                                        state[GetTitle() + "_wv"] = node.InnerText;
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        };
                                    }
                                    new Thread(start).Start(HttpContext.Current.Application);
                                }
                                catch
                                {
                                }
                            };
                        }
                        Elevate(code, true);
                    }
                    catch
                    {
                    }
                }
                return (HttpContext.Current.Application[GetTitle() + "_wv"] as string);
            }
        }

        public string WhiteLabel
        {
            get
            {
                if (wlabel == null)
                {
                    wlabel = GetProductResource("_WhiteLabel", new object[0]);
                }
                return wlabel;
            }
        }

        public static IEnumerable<int> WssInstalledCultures
        {
            get
            {
                string[] iteratorVariable0 = new string[] { @"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\12.0\InstalledLanguages", @"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\14.0\InstalledLanguages" };
                foreach (string iteratorVariable3 in iteratorVariable0)
                {
                    RegistryKey iteratorVariable2 = null;
                    try
                    {
                        iteratorVariable2 = Registry.LocalMachine.OpenSubKey(iteratorVariable3);
                    }
                    catch
                    {
                    }
                    if (iteratorVariable2 != null)
                    {
                        RegistryKey iteratorVariable7 = iteratorVariable2;
                        foreach (string iteratorVariable4 in iteratorVariable2.GetValueNames())
                        {
                            int iteratorVariable1;
                            if (!string.IsNullOrEmpty(iteratorVariable4) && int.TryParse(iteratorVariable4, out iteratorVariable1))
                            {
                                yield return iteratorVariable1;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Dictionary<string, string>> WssItems
        {
            get
            {
                CultureInfo iteratorVariable21;
                SPFarm iteratorVariable20;
                string productResource = GetProductResource("_WssItems", new object[0]);
                string iteratorVariable2 = null;
                string[] iteratorVariable5 = productResource.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                SPContext context = GetContext();
                SPSite iteratorVariable19 = null;
                SPWeb web = null;
                bool iteratorVariable22 = false;
                Converter<SPWeb, string> createAllWebsSelect = null;
                createAllWebsSelect = delegate (SPWeb w) {
                    SPWeb web1 = w;
                    string str = string.Empty;
                    int num = 0;
                    if (w.IsRootWeb)
                    {
                        str = str + "<select style=\"font-size: 10px;\" onchange=\"roxGoWeb(this.options[this.selectedIndex].value);\">";
                    }
                    else
                    {
                        do
                        {
                            num++;
                        }
                        while (!(web1 = web1.ParentWeb).IsRootWeb);
                    }
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, "<option value=\"", w.ID, "\"", w.ID.Equals(web.ID) ? " selected=\"selected\"" : string.Empty, ">" });
                    for (int j = 0; j < num; j++)
                    {
                        str = str + ((j == (num - 1)) ? "-&nbsp;" : "&nbsp;&nbsp;");
                    }
                    str = str + this.Context.Server.HtmlEncode(w.Title) + "</option>";
                    foreach (SPWeb web2 in TryEach<SPWeb>(w.Webs))
                    {
                        using (web2)
                        {
                            str = str + createAllWebsSelect(web2);
                        }
                    }
                    if (w.IsRootWeb)
                    {
                        str = str + "</select>";
                    }
                    return str;
                };
                if ((((context != null) && ((iteratorVariable20 = GetFarm(context)) != null)) && ((iteratorVariable19 = Elevated ? OpenSite(context) : GetSite(context)) != null)) && (((web = context.Web) != null) && ((iteratorVariable21 = GetFarmCulture(context)) != null)))
                {
                    foreach (string iteratorVariable26 in iteratorVariable5)
                    {
                        SPFieldTypeDefinition iteratorVariable14;
                        int iteratorVariable6;
                        string iteratorVariable3 = string.Empty;
                        bool iteratorVariable24 = false;
                        Dictionary<string, string> iteratorVariable10 = new Dictionary<string, string>();
                        iteratorVariable10["type"] = iteratorVariable26.Substring(0, iteratorVariable6 = iteratorVariable26.IndexOf(':'));
                        iteratorVariable10["title"] = iteratorVariable10["id"] = iteratorVariable26.Substring(iteratorVariable6 + 1);
                        iteratorVariable10["desc"] = this["WssInfoNope", new object[0]];
                        iteratorVariable10["icon"] = "genericfeature.gif";
                        iteratorVariable10["info"] = string.Empty;
                        if ((iteratorVariable10["type"] == "FieldType") && ((iteratorVariable14 = web.FieldTypeDefinitionCollection[iteratorVariable10["id"]]) != null))
                        {
                            List<SPField> iteratorVariable12;
                            string iteratorVariable4;
                            iteratorVariable10["icon"] = "menuaddcolumn.gif";
                            if (string.IsNullOrEmpty(iteratorVariable4 = GetProductResource("WssField_" + iteratorVariable10["id"], new object[0])))
                            {
                                iteratorVariable10["title"] = iteratorVariable14.TypeDisplayName;
                                iteratorVariable10["desc"] = iteratorVariable14.TypeShortDescription;
                            }
                            else
                            {
                                iteratorVariable10["title"] = iteratorVariable14.TypeShortDescription;
                                iteratorVariable10["desc"] = iteratorVariable4;
                            }
                            Dictionary<SPList, List<SPField>> iteratorVariable11 = new Dictionary<SPList, List<SPField>>();
                            foreach (SPList list in TryEach<SPList>(web.Lists))
                            {
                                foreach (SPField field in TryEach<SPField>(list.Fields))
                                {
                                    if (field.TypeAsString == iteratorVariable10["id"])
                                    {
                                        if (!iteratorVariable11.TryGetValue(list, out iteratorVariable12))
                                        {
                                            iteratorVariable11[list] = iteratorVariable12 = new List<SPField>();
                                        }
                                        iteratorVariable12.Add(field);
                                    }
                                }
                            }
                            iteratorVariable12 = new List<SPField>();
                            foreach (SPField field2 in TryEach<SPField>(web.Fields))
                            {
                                if (field2.TypeAsString == iteratorVariable10["id"])
                                {
                                    iteratorVariable12.Add(field2);
                                }
                            }
                            foreach (KeyValuePair<SPList, List<SPField>> pair in iteratorVariable11)
                            {
                                string str3 = iteratorVariable3;
                                iteratorVariable3 = str3 + "<li><a target=\"_blank\" href=\"" + MergeUrlPaths(web.Url, "/_layouts/listedit.aspx?List=" + this.Context.Server.UrlEncode(pair.Key.ID.ToString())) + "#1400\">" + this.Context.Server.HtmlEncode(pair.Key.Title) + "</a>: ";
                                foreach (SPField field3 in pair.Value)
                                {
                                    string str4 = iteratorVariable3;
                                    iteratorVariable3 = str4 + "<a target=\"_blank\" href=\"" + MergeUrlPaths(web.Url, "/_layouts/FldEditEx.aspx?List=" + this.Context.Server.UrlEncode(pair.Key.ID.ToString()) + "&Field=" + this.Context.Server.UrlEncode(field3.InternalName)) + "\">" + this.Context.Server.HtmlEncode(field3.Title) + "</a>, ";
                                }
                                iteratorVariable3 = iteratorVariable3.Substring(0, iteratorVariable3.Length - 2) + "</li>";
                            }
                            if (iteratorVariable12.Count > 0)
                            {
                                iteratorVariable3 = iteratorVariable3 + "<li>" + this["WssInfoType_SiteColumns", new object[0]] + ": ";
                                foreach (SPField field4 in iteratorVariable12)
                                {
                                    string str5 = iteratorVariable3;
                                    iteratorVariable3 = str5 + "<a target=\"_blank\" href=\"" + MergeUrlPaths(web.Url, "/_layouts/FldEditEx.aspx?Field=" + this.Context.Server.UrlEncode(field4.InternalName)) + "\">" + this.Context.Server.HtmlEncode(field4.Title) + "</a>, ";
                                }
                                iteratorVariable3 = iteratorVariable3.Substring(0, iteratorVariable3.Length - 2) + "</li>";
                            }
                            if (string.IsNullOrEmpty(iteratorVariable2))
                            {
                                iteratorVariable2 = createAllWebsSelect(iteratorVariable19.RootWeb);
                            }
                            iteratorVariable10["info"] = this["WssInfoType", new object[] { iteratorVariable2 }] + "<ul style=\"font-size: 10px;\">" + (string.IsNullOrEmpty(iteratorVariable3) ? this["WssInfoNone", new object[0]] : iteratorVariable3) + "</ul>";
                        }
                        else
                        {
                            SPField iteratorVariable13;
                            if ((iteratorVariable10["type"] == "SiteColumn") && ((iteratorVariable13 = GetField(iteratorVariable19.RootWeb.Fields, iteratorVariable10["id"])) != null))
                            {
                                iteratorVariable10["link"] = string.Format(MergeUrlPaths(iteratorVariable19.RootWeb.Url, "/_layouts/FldEditEx.aspx?Field={0}"), iteratorVariable10["id"]);
                                iteratorVariable10["title"] = iteratorVariable13.Title;
                                iteratorVariable10["desc"] = iteratorVariable13.Description;
                                iteratorVariable10["icon"] = "desfield.gif";
                                foreach (SPList list2 in TryEach<SPList>(web.Lists))
                                {
                                    if (GetField(list2, iteratorVariable10["id"]) != null)
                                    {
                                        string str6 = iteratorVariable3;
                                        iteratorVariable3 = str6 + "<li><a target=\"_blank\" href=\"" + MergeUrlPaths(web.Url, "/_layouts/FldEditEx.aspx?List=" + this.Context.Server.UrlEncode(list2.ID.ToString()) + "&Field=" + this.Context.Server.UrlEncode(iteratorVariable10["id"])) + "\">" + this.Context.Server.HtmlEncode(list2.Title) + "</a></li>";
                                    }
                                }
                                if (string.IsNullOrEmpty(iteratorVariable2))
                                {
                                    iteratorVariable2 = createAllWebsSelect(iteratorVariable19.RootWeb);
                                }
                                iteratorVariable10["info"] = this["WssInfoUsed", new object[] { iteratorVariable2 }] + "<ul style=\"font-size: 10px;\">" + (string.IsNullOrEmpty(iteratorVariable3) ? this["WssInfoNone", new object[0]] : iteratorVariable3) + "</ul>";
                            }
                            else
                            {
                                SPContentTypeId iteratorVariable9;
                                SPContentType iteratorVariable7;
                                if ((iteratorVariable10["type"] == "ContentType") && ((iteratorVariable7 = iteratorVariable19.RootWeb.ContentTypes[iteratorVariable9 = new SPContentTypeId(iteratorVariable10["id"])]) != null))
                                {
                                    iteratorVariable10["title"] = iteratorVariable7.Name;
                                    iteratorVariable10["desc"] = iteratorVariable7.Description;
                                    iteratorVariable10["icon"] = "WssTeamAndCollabInfrastruct.gif";
                                    iteratorVariable10["link"] = string.Format(MergeUrlPaths(iteratorVariable19.RootWeb.Url, "/_layouts/ManageContentType.aspx?ctype={0}"), iteratorVariable10["id"]);
                                    foreach (SPList list3 in TryEach<SPList>(web.Lists))
                                    {
                                        if (list3.ContentTypesEnabled && (list3.ContentTypes != null))
                                        {
                                            foreach (SPContentType type in TryEach<SPContentType>(list3.ContentTypes))
                                            {
                                                SPContentType iteratorVariable8 = type;
                                                bool iteratorVariable25 = false;
                                                do
                                                {
                                                    iteratorVariable25 |= iteratorVariable8.Id.Equals(iteratorVariable9);
                                                }
                                                while ((!iteratorVariable25 && (iteratorVariable8.Parent != null)) && (!iteratorVariable8.Parent.Id.Equals(iteratorVariable8.Id) && ((iteratorVariable8 = iteratorVariable8.Parent) != null)));
                                                if (iteratorVariable25)
                                                {
                                                    string str7 = iteratorVariable3;
                                                    iteratorVariable3 = str7 + "<li><a target=\"_blank\" href=\"" + MergeUrlPaths(web.Url, "/_layouts/listedit.aspx?List=" + this.Context.Server.UrlEncode(list3.ID.ToString())) + "#1400\">" + this.Context.Server.HtmlEncode(list3.Title) + "</a></li>";
                                                }
                                            }
                                        }
                                    }
                                    if (string.IsNullOrEmpty(iteratorVariable2))
                                    {
                                        iteratorVariable2 = createAllWebsSelect(iteratorVariable19.RootWeb);
                                    }
                                    iteratorVariable10["info"] = this["WssInfoAttached", new object[] { iteratorVariable2 }] + "<ul style=\"font-size: 10px;\">" + (string.IsNullOrEmpty(iteratorVariable3) ? this["WssInfoNone", new object[0]] : iteratorVariable3) + "</ul>";
                                }
                                else
                                {
                                    Guid iteratorVariable15;
                                    if (!Guid.Empty.Equals(iteratorVariable15 = GetGuid(iteratorVariable10["id"], true)))
                                    {
                                        SPFeatureDefinition iteratorVariable17;
                                        SPFeature iteratorVariable16;
                                        if (((iteratorVariable17 = iteratorVariable20.FeatureDefinitions[iteratorVariable15]) != null) || (((((iteratorVariable16 = iteratorVariable19.WebApplication.Features[iteratorVariable15]) != null) || ((iteratorVariable16 = iteratorVariable19.Features[iteratorVariable15]) != null)) || ((iteratorVariable16 = web.Features[iteratorVariable15]) != null)) && ((iteratorVariable17 = iteratorVariable16.Definition) != null)))
                                        {
                                            string iteratorVariable1;
                                            iteratorVariable10["title"] = iteratorVariable17.GetTitle(iteratorVariable21);
                                            iteratorVariable10["desc"] = iteratorVariable17.GetDescription(iteratorVariable21);
                                            if (!string.IsNullOrEmpty(iteratorVariable1 = iteratorVariable17.GetImageUrl(iteratorVariable21)))
                                            {
                                                iteratorVariable10["icon"] = iteratorVariable1;
                                            }
                                        }
                                        if (iteratorVariable10["type"] == "FarmFeature")
                                        {
                                            iteratorVariable10["link"] = string.Format(MergeUrlPaths(this.AdminSite.Url, "/_admin/ManageFarmFeatures.aspx#{0}"), iteratorVariable10["id"]);
                                            iteratorVariable22 = (SPWebService.AdministrationService.Features[iteratorVariable15] != null) || (SPWebService.ContentService.Features[iteratorVariable15] != null);
                                        }
                                        else if (iteratorVariable10["type"] == "AppFeature")
                                        {
                                            iteratorVariable10["link"] = string.Format(MergeUrlPaths(this.AdminSite.Url, "/_admin/ManageWebAppFeatures.aspx?WebApplicationId={1}#{0}"), iteratorVariable10["id"], iteratorVariable19.WebApplication.Id);
                                            iteratorVariable22 = iteratorVariable19.WebApplication.Features[iteratorVariable15] != null;
                                        }
                                        else
                                        {
                                            bool iteratorVariable23;
                                            iteratorVariable10["link"] = string.Format(MergeUrlPaths(((iteratorVariable23 = !(iteratorVariable24 = iteratorVariable10["type"] == "WebFeature")) ? (iteratorVariable19.RootWeb) : (web)).Url, "/_layouts/ManageFeatures.aspx?Scope={1}#{0}"), iteratorVariable10["id"], iteratorVariable23 ? "Site" : "Web");
                                            if (iteratorVariable24 && string.IsNullOrEmpty(iteratorVariable2))
                                            {
                                                iteratorVariable2 = createAllWebsSelect(iteratorVariable19.RootWeb);
                                            }
                                            iteratorVariable22 = ((iteratorVariable23 != null) ? iteratorVariable19.Features : web.Features)[iteratorVariable15] != null;
                                        }
                                        iteratorVariable10["info"] = this["WssInfo" + (iteratorVariable24 ? "Web" : "Other") + "Feature", new object[] { iteratorVariable24 ? iteratorVariable2 : string.Empty }] + " " + this["WssInfo" + (iteratorVariable22 ? "A" : "Ina") + "ctive", new object[] { iteratorVariable10["link"] }];
                                    }
                                }
                            }
                        }
                        iteratorVariable10["title"] = iteratorVariable10["title"].Substring(iteratorVariable10["title"].IndexOf(']') + 1);
                        if ((iteratorVariable10["title"] != iteratorVariable10["id"]) || (iteratorVariable10["desc"] != this["WssInfoNope", new object[0]]))
                        {
                            yield return iteratorVariable10;
                        }
                    }
                }
                if ((iteratorVariable19 != null) && Elevated)
                {
                    iteratorVariable19.Dispose();
                }
            }
        }







        internal class LicInfo
        {
            internal readonly bool broken = false;
            internal readonly IDictionary dic;
            internal readonly bool enabled = true;
            internal Exception error;
            internal readonly bool expired = false;
            internal DateTime expiry;
            internal TimeSpan installSpan;
            internal int maxUsers;
            internal string name;
            internal readonly IDictionary sd;
            internal int siteUsers;
            internal readonly bool userBroken = false;

            private LicInfo() : this(null, null)
            {
                broken = false;
                enabled = true;
                expired = false;
                userBroken = false;
            }

            private LicInfo(SPContext context) : this(context, null)
            {
                broken = false;
                enabled = true;
                expired = false;
                userBroken = false;
            }

            private LicInfo(SPContext context, IDictionary dic)
            {
                broken = false;
                enabled = true;
                expired = false;
                userBroken = false;

                this.expiry = DateTime.MinValue;
                this.name = "";
                this.maxUsers = -1;
                this.installSpan = TimeSpan.MinValue;
                this.siteUsers = -1;
                long ev;
                IDictionary sd = null;
                if (context == null)
                {
                    context = ProductPage.GetContext();
                }
                if (context != null)
                {
                    ProductPage.Elevate(delegate {
                        bool flag = false;
                        try
                        {
                            //if (!(this.broken = ((sd = ProductPage.GetStatus<Dictionary<string, object>>(context)) != null) && (sd.Count == 0)))
                            if(!broken)
                            {
                                if (sd == null)
                                {
                                    sd = new Dictionary<string, object>();
                                    sd["is"] = 0L;
                                    sd["ed"] = DateTime.Today.AddDays((double) ProductPage.l1);
                                }
                                this.installSpan = new TimeSpan((long) sd["is"]);
                                //this.expired = (DateTime.Now >= (this.expiry = (DateTime) sd["ed"])) && (sd.Count <= 3);
                            }
                            else
                            {
                                if (sd == null)
                                {
                                    sd = new Dictionary<string, object>();
                                }
                                sd["is"] = TimeSpan.FromDays((double) (ProductPage.l1 + 1)).Ticks;
                                sd["ed"] = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
                                this.installSpan = new TimeSpan((long) sd["is"]);
                                //this.expired = true;
                            }
                            if ((sd != null) && (sd.Count > 3))
                            {
                                foreach (string str in sd.Keys)
                                {
                                    if (flag = ProductPage.IsGuid(str) && Guid.Empty.Equals(ProductPage.GetGuid(str, true)))
                                    {
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    this.expiry = DateTime.MinValue;
                                }
                            }
                            if (dic == null)
                            {
                                dic = ProductPage.LicObject(context);
                            }
                            if (dic != null)
                            {
                                //this.expired = false;
                                if ((dic.Contains("f4") && long.TryParse(Convert.ToString(dic["f4"]), out ev)) && (ev > 0L))
                                {
                                    this.expiry = new DateTime(ev);
                                }
                                if (ProductPage.IsTheThing(dic))
                                {
                                    //this.userBroken = true;
                                    foreach (object obj2 in dic)
                                    {
                                        if (obj2 is DictionaryEntry)
                                        {
                                            DictionaryEntry entry = (DictionaryEntry) obj2;
                                            this.maxUsers = int.Parse(entry.Key as string);
                                            DictionaryEntry entry2 = (DictionaryEntry) obj2;
                                            this.siteUsers = (int) entry2.Value;
                                        }
                                        else if (obj2 is KeyValuePair<string, object>)
                                        {
                                            KeyValuePair<string, object> pair = (KeyValuePair<string, object>) obj2;
                                            this.maxUsers = int.Parse(pair.Key);
                                            KeyValuePair<string, object> pair2 = (KeyValuePair<string, object>) obj2;
                                            this.siteUsers = (int) pair2.Value;
                                        }
                                    }
                                }
                                else
                                {
                                    //this.userBroken = ((this.siteUsers = ProductPage.GetUsers(context, ProductPage.Elevated, null)) > (this.maxUsers = int.Parse(dic["f2"] as string))) && (this.maxUsers > 0);
                                    this.name = ProductPage.LicName(context, dic);
                                }
                            }
                            if (!expired)
                            {
                                //this.expired = (((dic == null) || string.IsNullOrEmpty(this.name)) && ((this.expired || this.broken) || this.userBroken)) || ((dic != null) && string.IsNullOrEmpty(this.name));
                            }
                        }
                        catch (Exception exception)
                        {
                            //this.error = exception;
                            //this.broken = true;
                            //this.expired = true;
                        }
                    }, true);
                }
                this.dic = dic;
                this.sd = sd;
            }

            internal static ProductPage.LicInfo Get(IDictionary dic)
            {
                ProductPage.LicInfo info = null;
                HttpContext current = null;
                try
                {
                    current = HttpContext.Current;
                }
                catch
                {
                }
                if ((dic == null) && (current != null))
                {
                    info = current.Items["___" + ProductPage.AssemblyName + "_li"] as ProductPage.LicInfo;
                    if (info == null)
                    {
                        current.Items["___" + ProductPage.AssemblyName + "_li"] = info = new ProductPage.LicInfo();
                    }
                }
                if (info != null)
                {
                    return info;
                }
                return new ProductPage.LicInfo(null, dic);
            }
        }
    }
}