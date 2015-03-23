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
    using System.Runtime.InteropServices;
    using System.Web;

    public class DataSourceConsumer : IDisposable
    {
        internal List<string> andFilters;
        internal string[] cfgExcludePatterns;
        internal static string cfgMultiPropJoin = ProductPage.Config(ProductPage.GetContext(), "MultiPropJoin");
        private StringComparison comparison;
        internal static RecordPropertyCollection dataProps = null;
        public readonly roxority.Data.DataSource DataSource;
        public readonly bool DateIgnoreDay;
        public readonly bool DateThisYear;
        internal DataSourceCache dsCache;
        internal static Dictionary<string, DataSourceCache> dsCaches = new Dictionary<string, DataSourceCache>();
        internal List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>> effectiveFilters;
        internal List<object[]> filters;
        internal readonly Dictionary<string, int> groupCounts;
        [ThreadStatic]
        internal static ProductPage.LicInfo lic = null;
        public readonly List<CachedRecord> List;
        internal List<string> nameProps;
        public readonly int PageSize;
        public readonly int PageStart;
        public readonly string Properties;
        internal int recCount;
        internal List<string> tabs;
        internal bool tabsReduced;
        internal bool tabsReverse;
        internal bool tabsShowAll;
        internal string tabValue;
        internal long totalCount;

        public DataSourceConsumer(int pageSize, int pageStart, bool dateThisYear, bool dateIgnoreDay, string properties, object sSortProp, object bSortDesc, string tabProp, string tv, object sGroupProp, object bGroupDesc, bool groupByCounts, bool groupShowCounts, SPWeb contextSite, string dsid, IDictionary dynInst, Hashtable fht, object l, List<Exception> sortErrors)
        {
            string str;
            this.List = new List<CachedRecord>();
            this.groupCounts = new Dictionary<string, int>();
            this.andFilters = new List<string>();
            this.comparison = StringComparison.InvariantCulture;
            SPContext context = ProductPage.GetContext();
            this.tabsReverse = ProductPage.Config<bool>(context, "FilterTabReverse");
            cfgMultiPropJoin = ProductPage.Config(context, "MultiPropJoin");
            this.DateThisYear = dateThisYear;
            this.DateIgnoreDay = dateIgnoreDay;
            this.PageSize = pageSize;
            this.PageStart = pageStart;
            this.Properties = properties;
            this.tabValue = tv;
            this.tabsShowAll = ProductPage.Config<bool>(context, "FilterTabShowAll");
            lic = l as ProductPage.LicInfo;
            if (fht != null)
            {
                if (((this.filters = fht["f"] as List<object[]>) == null) && (fht["f"] is ICollection))
                {
                    this.filters = new List<object[]>();
                    foreach (ArrayList list in (ICollection) fht["f"])
                    {
                        this.filters.Add(list.ToArray(typeof(object)) as object[]);
                    }
                }
                if (fht["fa"] is List<string>)
                {
                    this.andFilters = fht["fa"] as List<string>;
                }
                else if (fht["fa"] is ICollection)
                {
                    this.andFilters = new List<string>();
                    foreach (string str2 in (ICollection) fht["fa"])
                    {
                        this.andFilters.Add(str2);
                    }
                }
            }
            UserDataSource source = (this.DataSource = roxority.Data.DataSource.FromID(dsid, true, true, (dynInst == null) ? null : (dynInst["t"] + string.Empty))) as UserDataSource;
            if (source != null)
            {
                this.cfgExcludePatterns = source.ExcludePatterns;
            }
            if (this.DataSource == null)
            {
                throw new Exception(ProductPage.GetResource("DataSourceNotFound", new object[] { dsid }));
            }
            if ((dynInst != null) && (dynInst.Count > 0))
            {
                if (this.DataSource.inst == null)
                {
                    this.DataSource.inst = new OrderedDictionary();
                }
                foreach (DictionaryEntry entry in dynInst)
                {
                    this.DataSource.inst[entry.Key] = entry.Value;
                }
            }
            if ((!Guid.Empty.Equals(this.DataSource.ContextID) && this.DataSource.RequireCaching) && !dsCaches.TryGetValue(str = ProductPage.GuidLower(this.DataSource.ContextID, false), out this.dsCache))
            {
                dsCaches[str] = this.dsCache = new DataSourceCache();
            }
            this.PopulateList(sSortProp, bSortDesc, sGroupProp, tabProp, bGroupDesc, groupByCounts, groupShowCounts, sortErrors);
        }

        public virtual void Dispose()
        {
            this.DataSource.DoDispose();
        }

        internal static List<string> Get(roxority.Data.DataSource ds, CachedRecord record, List<object> values, string propertyName, string defaultValue, bool longDateTime)
        {
            string str;
            Record record2;
            RecordProperty propertyByName = null;
            if (ds != null)
            {
                propertyByName = ds.Properties.GetPropertyByName(propertyName);
            }
            if (values == null)
            {
                record.Values.TryGetValue(propertyName, out values);
            }
            if ((!propertyName.StartsWith("rox___") && (values == null)) && ((record.RecordID > 0L) && ((record2 = ds.GetRecord(record.RecordID)) != null)))
            {
                record.Resync(record2, new string[] { propertyName });
                record.Values.TryGetValue(propertyName, out values);
            }
            List<string> list = (values == null) ? new List<string>() : new List<string>(values.Count);
            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    SPFieldUrlValue value2;
                    list.Add(((value2 = values[i] as SPFieldUrlValue) != null) ? string.Format("<a href=\"{0}\">{1}</a>", value2.Url, HttpUtility.HtmlEncode(value2.Description)) : (values[i] + string.Empty));
                }
            }
            if ((!propertyName.StartsWith("{", StringComparison.InvariantCultureIgnoreCase) || !propertyName.EndsWith("}", StringComparison.InvariantCultureIgnoreCase)) || !propertyName.ToLowerInvariant().Contains("birthday"))
            {
                DateTime time;
                if (longDateTime)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (DateTime.TryParse(list[j], out time))
                        {
                            list[j] = time.ToLongDateString();
                        }
                    }
                }
                else if ((propertyByName == null) || propertyByName.AllowDateParsing)
                {
                    for (int k = 0; k < list.Count; k++)
                    {
                        if (DateTime.TryParse(list[k], out time))
                        {
                            list[k] = time.ToShortDateString();
                        }
                    }
                }
            }
            if (((ds != null) && (propertyByName == null)) && (propertyName.StartsWith("rox___") && !string.IsNullOrEmpty(str = Record.GetSpecialFieldValue(ds, propertyName, n => record[n, string.Empty, ds]))))
            {
                list.Add(str);
            }
            if ((list.Count == 0) && !string.IsNullOrEmpty(defaultValue))
            {
                list.Add(defaultValue);
            }
            return list;
        }

        internal DateTime GetDate(DateTime value)
        {
            if (this.DateThisYear && this.DateIgnoreDay)
            {
                value = new DateTime(DateTime.Now.Year, value.Month, DateTime.Now.Day);
                return value;
            }
            if (this.DateThisYear)
            {
                value = new DateTime(DateTime.Now.Year, value.Month, value.Day);
                return value;
            }
            if (this.DateIgnoreDay)
            {
                value = new DateTime(value.Year, value.Month, DateTime.Now.Day);
                return value;
            }
            value = new DateTime(value.Year, value.Month, value.Day);
            return value;
        }

        internal bool GetDate(string dt, out DateTime dtVal)
        {
            bool flag;
            if (flag = DateTime.TryParse(dt, out dtVal))
            {
                dtVal = this.GetDate(dtVal);
            }
            return flag;
        }

        internal static string GetTitle(DataSourceConsumer consumer, CachedRecord record)
        {
            if (string.IsNullOrEmpty(record.friendlyName) || consumer.DataSource.RequireCaching)
            {
                record.friendlyName = record["rox___pt", string.Empty, consumer.DataSource];
            }
            if (!string.IsNullOrEmpty(record.friendlyName))
            {
                return consumer.DataSource.FixupTitle(record.friendlyName);
            }
            if (record.RecordID <= 0L)
            {
                return record.ID.ToString();
            }
            return ("#" + record.RecordID);
        }

        internal bool InFilter(roxority.Data.DataSource ds, CachedRecord rec)
        {
            string str2 = string.Empty;
            if (ds != null)
            {
                if (((rec != null) && (this.cfgExcludePatterns != null)) && (this.cfgExcludePatterns.Length > 0))
                {
                    foreach (string str3 in Get(ds, rec, null, "rox___pl", string.Empty, false))
                    {
                        str2 = (string.Empty + str3).ToLowerInvariant();
                        break;
                    }
                    if (string.IsNullOrEmpty(str2))
                    {
                        foreach (string str4 in Get(ds, rec, null, ds.RewritePropertyName("AccountName"), string.Empty, false))
                        {
                            str2 = (string.Empty + str4).ToLowerInvariant();
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        foreach (string str5 in this.cfgExcludePatterns)
                        {
                            if (!string.IsNullOrEmpty(str5))
                            {
                                string str = str5.Trim().ToLowerInvariant();
                                if ((str.StartsWith("*") && str.EndsWith("*")) && (str2.IndexOf(str.Substring(1, str.Length - 2)) >= 0))
                                {
                                    return false;
                                }
                                if (str.StartsWith("*") && str2.EndsWith(str.Substring(1)))
                                {
                                    return false;
                                }
                                if (str.EndsWith("*") && str2.StartsWith(str.Substring(0, str.Length - 1)))
                                {
                                    return false;
                                }
                                if (str2.Equals(str))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                if ((this.filters != null) && (this.filters.Count > 0))
                {
                    if (this.effectiveFilters == null)
                    {
                        this.effectiveFilters = new List<KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>>();
                        foreach (object[] objArray in this.filters)
                        {
                            int num2;
                            if ((objArray == null) || (objArray.Length < 3))
                            {
                                continue;
                            }
                            KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> item = new KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>>(objArray[0] + string.Empty, new KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>(new List<KeyValuePair<string, CamlOperator>>(), this.andFilters.Contains(objArray[0] + string.Empty)));
                            int num = num2 = -1;
                            foreach (KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> pair2 in this.effectiveFilters)
                            {
                                num2++;
                                if (string.Equals(pair2.Key, item.Key))
                                {
                                    num = num2;
                                    item = pair2;
                                    break;
                                }
                            }
                            item.Value.Key.Add(new KeyValuePair<string, CamlOperator>(objArray[1] + string.Empty, (objArray[2] is string) ? ((CamlOperator) Enum.Parse(typeof(CamlOperator), objArray[2] + string.Empty, true)) : ((CamlOperator) objArray[2])));
                            if (num >= 0)
                            {
                                this.effectiveFilters[num] = item;
                            }
                            else
                            {
                                this.effectiveFilters.Add(item);
                            }
                        }
                    }
                    if (rec != null)
                    {
                        foreach (KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> pair3 in this.effectiveFilters)
                        {
                            if ((pair3.Value.Key != null) && !string.IsNullOrEmpty(pair3.Key))
                            {
                                List<string> pvals = Get(ds, rec, null, pair3.Key, null, true);
                                if (!this.IsMatch(pvals, pair3.Value.Key, pair3.Value.Value))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        internal bool InTab(CachedRecord rec, string tabProp, CamlOperator op, bool asDate)
        {
            int num = 0;
            string tabValue = this.tabValue;
            DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
            if (this.tabs == null)
            {
                return true;
            }
            if (tabValue == null)
            {
                if (this.tabsShowAll)
                {
                    return true;
                }
                tabValue = this.tabs[0];
                if ((asDate && this.DateThisYear) && this.tabs.Contains(currentInfo.MonthNames[DateTime.Now.Month - 1]))
                {
                    tabValue = currentInfo.MonthNames[DateTime.Now.Month - 1];
                }
                this.tabValue = tabValue;
            }
            if (asDate && (tabValue.Length > 0))
            {
                if (!this.DateThisYear)
                {
                    num = int.Parse(tabValue);
                }
                else
                {
                    foreach (string str4 in currentInfo.MonthNames)
                    {
                        if (str4.Equals(tabValue, StringComparison.InvariantCultureIgnoreCase))
                        {
                            break;
                        }
                        num++;
                    }
                }
            }
            if (rec.Values.ContainsKey(tabProp))
            {
                if ((tabValue.Length == 0) && ((rec.Values[tabProp] == null) || (rec.Values[tabProp].Count == 0)))
                {
                    return true;
                }
                if (tabValue.Length > 0)
                {
                    foreach (object obj2 in rec.Values[tabProp])
                    {
                        string s = ProductPage.Normalize(obj2 + string.Empty);
                        if (asDate)
                        {
                            DateTime time;
                            if (!DateTime.TryParse(s, out time))
                            {
                                time = DateTime.Parse(s, CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces);
                            }
                            if (!this.DateThisYear && (time.Year == num))
                            {
                                return true;
                            }
                            if (this.DateThisYear && (time.Month == (num + 1)))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            string str3 = ProductPage.Normalize(tabValue);
                            if ((op == CamlOperator.Eq) && str3.Equals(s, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return true;
                            }
                            if ((op == CamlOperator.BeginsWith) && s.StartsWith(str3, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return true;
                            }
                            if ((op == CamlOperator.Contains) && s.ToLowerInvariant().Contains(str3.ToLowerInvariant()))
                            {
                                return true;
                            }
                            if ((op == CamlOperator.Neq) && !str3.Equals(s, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        internal bool IsMatch(List<string> pvals, List<KeyValuePair<string, CamlOperator>> vals, bool and)
        {
            List<bool> list;
            if (this.DateThisYear && (vals.Count == 2))
            {
                KeyValuePair<string, CamlOperator> pair2 = vals[0];
                if (((CamlOperator) pair2.Value) != CamlOperator.Geq)
                {
                    KeyValuePair<string, CamlOperator> pair3 = vals[0];
                    if (((CamlOperator) pair3.Value) != CamlOperator.Gt)
                    {
                        goto Label_005B;
                    }
                }
                KeyValuePair<string, CamlOperator> pair4 = vals[1];
                if (((CamlOperator) pair4.Value) != CamlOperator.Leq)
                {
                    KeyValuePair<string, CamlOperator> pair5 = vals[1];
                    CamlOperator local1 = pair5.Value;
                }
            }
        Label_005B:
            list = new List<bool>();
            int num3 = 0;
            if (pvals.Count == 0)
            {
                pvals.Add(string.Empty);
            }
            foreach (string str in pvals)
            {
                if (str != null)
                {
                    bool flag2 = false;
                    foreach (KeyValuePair<string, CamlOperator> pair in vals)
                    {
                        bool flag;
                        DateTime time;
                        DateTime time2;
                        if ((DateTime.TryParse(str, out time) || DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time)) && (DateTime.TryParse(pair.Key, out time2) || DateTime.TryParse(pair.Key, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time2)))
                        {
                            if (this.DateThisYear || this.DateIgnoreDay)
                            {
                                time = new DateTime(this.DateThisYear ? DateTime.Now.Year : time.Year, time.Month, this.DateIgnoreDay ? DateTime.Now.Day : time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
                                time2 = new DateTime(this.DateThisYear ? DateTime.Now.Year : time2.Year, time2.Month, this.DateIgnoreDay ? DateTime.Now.Day : time2.Day, time2.Hour, time2.Minute, time2.Second, time2.Millisecond);
                            }
                            if (((CamlOperator) pair.Value) == CamlOperator.Geq)
                            {
                                flag = time >= time2;
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Gt)
                            {
                                flag = time > time2;
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Leq)
                            {
                                flag = time <= time2;
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Lt)
                            {
                                flag = time < time2;
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Neq)
                            {
                                flag = time != time2;
                            }
                            else
                            {
                                flag = time == time2;
                            }
                        }
                        else
                        {
                            decimal num;
                            decimal num2;
                            if ((decimal.TryParse(str, out num) || decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out num)) && (decimal.TryParse(pair.Key, out num2) || decimal.TryParse(pair.Key, NumberStyles.Any, CultureInfo.InvariantCulture, out num2)))
                            {
                                if (((CamlOperator) pair.Value) == CamlOperator.Geq)
                                {
                                    flag = num >= num2;
                                }
                                else if (((CamlOperator) pair.Value) == CamlOperator.Gt)
                                {
                                    flag = num > num2;
                                }
                                else if (((CamlOperator) pair.Value) == CamlOperator.Leq)
                                {
                                    flag = num <= num2;
                                }
                                else if (((CamlOperator) pair.Value) == CamlOperator.Lt)
                                {
                                    flag = num < num2;
                                }
                                else if (((CamlOperator) pair.Value) == CamlOperator.Neq)
                                {
                                    flag = num != num2;
                                }
                                else
                                {
                                    flag = num == num2;
                                }
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.BeginsWith)
                            {
                                flag = str.StartsWith(pair.Key, this.Comparison);
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Contains)
                            {
                                flag = str.IndexOf(pair.Key, this.Comparison) >= 0;
                            }
                            else if (((CamlOperator) pair.Value) == CamlOperator.Neq)
                            {
                                flag = !str.Equals(pair.Key, this.Comparison);
                            }
                            else
                            {
                                flag = str.Equals(pair.Key, this.Comparison);
                            }
                        }
                        if (flag)
                        {
                            flag2 = true;
                        }
                        list.Add(flag);
                        num3++;
                    }
                    if (flag2)
                    {
                        break;
                    }
                }
            }
            if ((list.Count <= 0) || !list.Contains(true))
            {
                return false;
            }
            if (and)
            {
                return !list.Contains(false);
            }
            return true;
        }

        internal void PopulateList(object sSortProp, object bSortDesc, object sGroupProp, string tabProp, object bGroupDesc, bool groupByCounts, bool groupShowCounts, List<Exception> sortErrors)
        {
            string[] strArray3;
            int num4;
            Record record;
            string groupProp = null;
            string sortProp = null;
            string tmp = null;
            string sort2Prop = this.DataSource["s2", ""];
            bool groupDesc = false;
            bool sortDesc = false;
            bool asDate = false;
            bool flag2 = false;
            bool sort2Desc = !string.IsNullOrEmpty(sort2Prop) && sort2Prop.StartsWith("-");
            long recID = 0L;
            int cacheRate = this.DataSource.CacheRate;
            int numTabs = 0;
            int dtTabs = 0;
            int result = 0x10;
            decimal decTmp = new decimal(-1);
            bool? isDt = null;
            bool? isDec = null;
            bool? isGrDt = null;
            bool? isGrDec = null;
            List<string> collection = new List<string>();
            List<string> tabsDone = null;
            List<string> list4 = new List<string>();
            List<CachedRecord> recordCache = null;
            CamlOperator eq = CamlOperator.Eq;
            DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
            this.recCount = 0;
            this.totalCount = 0L;
            if ((!int.TryParse(ProductPage.Config(null, "FilterTabThreshold"), out result) || (result < 2)) || (result > 50))
            {
                result = 0x10;
            }
            if (sSortProp is string)
            {
                sortProp = sSortProp + string.Empty;
            }
            if (sGroupProp is string)
            {
                groupProp = sGroupProp + string.Empty;
            }
            if (bSortDesc is bool)
            {
                sortDesc = (bool) bSortDesc;
            }
            if (bGroupDesc is bool)
            {
                groupDesc = (bool) bGroupDesc;
            }
            if (string.Equals(sortProp, groupProp, StringComparison.InvariantCultureIgnoreCase))
            {
                sortProp = null;
            }
            this.InFilter(this.DataSource, null);
            if (this.effectiveFilters != null)
            {
                foreach (KeyValuePair<string, KeyValuePair<List<KeyValuePair<string, CamlOperator>>, bool>> pair in this.effectiveFilters)
                {
                    list4.Add(pair.Key);
                }
            }
            if (!string.IsNullOrEmpty(sortProp) && !"___roxRandomizedSort".Equals(sortProp, StringComparison.InvariantCultureIgnoreCase))
            {
                list4.Add(sortProp);
            }
            if (!string.IsNullOrEmpty(groupProp))
            {
                list4.Add(groupProp);
            }
            if (!string.IsNullOrEmpty(tabProp))
            {
                list4.Add(tabProp);
            }
            if ((this.DataSource is UserDataSource) && !string.IsNullOrEmpty(this.DataSource["pl", string.Empty]))
            {
                list4.Add("roxSiteGroups");
            }
            if (this.DataSource is UserDataSource)
            {
                list4.Add(this.DataSource.RewritePropertyName("FirstName"));
            }
            if (this.DataSource is UserDataSource)
            {
                list4.Add(this.DataSource.RewritePropertyName("LastName"));
            }
            foreach (string str3 in roxority.Data.DataSource.SchemaProps)
            {
                foreach (string str4 in RecordProperty.ExtractNames(str3, false, this.DataSource))
                {
                    list4.Add(str4);
                }
            }
            if (this.DataSource is UserDataSource)
            {
                list4.Add(this.DataSource.RewritePropertyName("UserName"));
            }
            foreach (string str5 in this.Properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list4.Add(((num4 = str5.IndexOf(':')) > 0) ? str5.Substring(0, num4) : str5);
            }
            tmp = string.Empty;
            ProductPage.RemoveDuplicates<string>(list4);
            string[] props = list4.ToArray();
            try
            {
                this.totalCount = this.DataSource.Count;
            }
            catch (UnauthorizedAccessException)
            {
                this.totalCount = -1L;
            }
            if (!this.DataSource.RequireCaching || (this.dsCache == null))
            {
                recordCache = new List<CachedRecord>();
                foreach (Record record3 in this.DataSource)
                {
                    if (record3 != null)
                    {
                        recordCache.Add(new CachedRecord(record3, props));
                    }
                }
            }
            else if (!this.dsCache.caching)
            {
                try
                {
                    string[] strArray;
                    string str;
                    int num2;
                    this.dsCache.caching = true;
                    if ((((num2 = (int) this.totalCount) < 0) && ((this.dsCache.recordCache.Count == 0) || ((this.dsCache.recacheHere == 0) && this.DataSource.Recache))) || ((num2 >= 0) && (this.dsCache.recordCache.Count != num2)))
                    {
                        Directory.cacheHelpers.Remove(this.DataSource.ContextID);
                        this.dsCache.recordCache.Clear();
                        this.dsCache.recacheHere = 0;
                        foreach (string str6 in props)
                        {
                            if ((((strArray = str6.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (strArray.Length >= 1)) && !this.dsCache.cachedProperties.Contains(str = strArray[0].Trim()))
                            {
                                this.dsCache.cachedProperties.Add(str);
                            }
                        }
                        if (((this.DataSource is UserProfiles) && ProductPage.Config<bool>(null, "AltEnum")) && (num2 >= 0))
                        {
                            while (this.dsCache.recordCache.Count < num2)
                            {
                                record = null;
                                try
                                {
                                    record = this.DataSource.GetRecord(recID);
                                }
                                catch (Exception exception)
                                {
                                    if ("UserNotFoundException".Equals(exception.GetType().Name) && flag2)
                                    {
                                        this.dsCache.recordCache.Add(null);
                                    }
                                }
                                if (record != null)
                                {
                                    this.dsCache.recordCache.Add(new CachedRecord(record, props));
                                }
                                if (recID >= 0x7fffffffL)
                                {
                                    goto Label_0955;
                                }
                                recID += 1L;
                            }
                        }
                        else
                        {
                            List<Guid> list = new List<Guid>();
                            foreach (Record record4 in this.DataSource)
                            {
                                Guid guid;
                                if ((record4 != null) && !list.Contains(guid = record4.ID))
                                {
                                    list.Add(guid);
                                    this.dsCache.recordCache.Add(new CachedRecord(record4, props));
                                }
                            }
                            this.totalCount = this.dsCache.recordCache.Count;
                            num2 = (int)this.totalCount;
                        }
                    }
                    else
                    {
                        foreach (string str7 in props)
                        {
                            if ((((strArray = str7.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (strArray.Length >= 1)) && (!this.dsCache.cachedProperties.Contains(str = strArray[0].Trim()) && !collection.Contains(str)))
                            {
                                collection.Add(str);
                            }
                        }
                        if (this.filters != null)
                        {
                            foreach (object[] objArray in this.filters)
                            {
                                if (!this.dsCache.cachedProperties.Contains(str = (objArray[0] + string.Empty).Trim()) && !collection.Contains(str))
                                {
                                    collection.Add(str);
                                }
                            }
                        }
                        if (collection.Count > 0)
                        {
                            for (int j = 0; j < this.dsCache.recordCache.Count; j++)
                            {
                                if (this.dsCache.recordCache[j] != null)
                                {
                                    record = null;
                                    try
                                    {
                                        record = this.DataSource.GetRecord(this.dsCache.recordCache[j].RecordID);
                                    }
                                    catch
                                    {
                                    }
                                    if (record != null)
                                    {
                                        this.dsCache.recordCache[j].Resync(record, collection.ToArray());
                                    }
                                }
                            }
                            this.dsCache.cachedProperties.AddRange(collection);
                        }
                        else if (cacheRate > 0)
                        {
                            strArray3 = this.dsCache.cachedProperties.ToArray();
                            for (int k = this.dsCache.recacheHere; (k < (this.dsCache.recacheHere + cacheRate)) && (k < this.dsCache.recordCache.Count); k++)
                            {
                                CachedRecord record2 = this.dsCache.recordCache[k];
                                if (record2 != null)
                                {
                                    record = null;
                                    try
                                    {
                                        record = this.DataSource.GetRecord(record2.RecordID);
                                    }
                                    catch
                                    {
                                    }
                                    if (record != null)
                                    {
                                        record2.Resync(record, strArray3);
                                    }
                                }
                            }
                            this.dsCache.recacheHere += cacheRate;
                            if (this.dsCache.recacheHere >= this.dsCache.recordCache.Count)
                            {
                                this.dsCache.recacheHere = 0;
                            }
                        }
                    }
                }
                finally
                {
                    this.dsCache.caching = false;
                }
            }
        Label_0955:
            if (!string.IsNullOrEmpty(tabProp))
            {
                this.tabs = new List<string>();
                tabsDone = new List<string>();
                if (!this.DataSource.RequireCaching || (this.dsCache == null))
                {
                    for (int m = 0; m < recordCache.Count; m++)
                    {
                        if (!this.InFilter(this.DataSource, recordCache[m]))
                        {
                            recordCache.RemoveAt(m);
                            m--;
                        }
                        else
                        {
                            this.PrepareTab(recordCache[m], tabProp, tabsDone, ref dtTabs, ref numTabs, ref tmp, ref decTmp);
                        }
                    }
                }
                else
                {
                    recordCache = new List<CachedRecord>();
                    for (int n = 0; n < this.dsCache.recordCache.Count; n++)
                    {
                        if (this.dsCache.recordCache[n] != null)
                        {
                            try
                            {
                                if (this.InFilter(this.DataSource, this.dsCache.recordCache[n]))
                                {
                                    recordCache.Add(this.dsCache.recordCache[n]);
                                    this.PrepareTab(this.dsCache.recordCache[n], tabProp, tabsDone, ref dtTabs, ref numTabs, ref tmp, ref decTmp);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                asDate = (dtTabs == this.tabs.Count) || ((dtTabs == (this.tabs.Count - 1)) && this.tabs.Contains(string.Empty));
                if ((numTabs != this.tabs.Count) && (numTabs == (this.tabs.Count - 1)))
                {
                    this.tabs.Contains(string.Empty);
                }
                if (this.tabs.Count > result)
                {
                    if (asDate)
                    {
                        for (int num14 = 0; num14 < this.tabs.Count; num14++)
                        {
                            if (this.tabs[num14].Length > 0)
                            {
                                if (this.DateThisYear)
                                {
                                    this.tabs[num14] = DateTime.Parse(this.tabs[num14], CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces).Month + string.Empty;
                                }
                                else
                                {
                                    this.tabs[num14] = DateTime.Parse(this.tabs[num14], CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces).Year + string.Empty;
                                }
                            }
                        }
                        for (int num15 = 0; num15 < this.tabs.Count; num15++)
                        {
                            if (this.tabs.IndexOf(this.tabs[num15]) < num15)
                            {
                                this.tabs.RemoveAt(num15);
                                num15--;
                            }
                        }
                        this.tabs.Sort(delegate (string one, string two) {
                            if (one.Length == 0)
                            {
                                return two.CompareTo(one);
                            }
                            if (two.Length != 0)
                            {
                                return long.Parse(one).CompareTo(long.Parse(two));
                            }
                            return one.CompareTo(two);
                        });
                        if (this.tabsReverse)
                        {
                            this.tabs.Reverse();
                        }
                        if (((num4 = this.tabs.IndexOf(string.Empty)) >= 0) && (num4 < (this.tabs.Count - 1)))
                        {
                            for (int num16 = num4; num16 < (this.tabs.Count - 1); num16++)
                            {
                                this.tabs[num16] = this.tabs[num16 + 1];
                            }
                            this.tabs[this.tabs.Count - 1] = string.Empty;
                        }
                        if (this.DateThisYear)
                        {
                            for (int num17 = 0; num17 < this.tabs.Count; num17++)
                            {
                                if (this.tabs[num17].Length > 0)
                                {
                                    this.tabs[num17] = currentInfo.MonthNames[int.Parse(this.tabs[num17]) - 1];
                                }
                            }
                        }
                    }
                    else
                    {
                        this.tabsReduced = true;
                        eq = CamlOperator.BeginsWith;
                        for (int num18 = 0; num18 < this.tabs.Count; num18++)
                        {
                            if (this.tabs[num18].Length > 0)
                            {
                                this.tabs[num18] = this.tabs[num18].Trim().Substring(0, 1).ToUpperInvariant();
                            }
                            if (this.tabs.IndexOf(this.tabs[num18]) < num18)
                            {
                                this.tabs.RemoveAt(num18);
                                num18--;
                            }
                        }
                    }
                }
            }
            if (((this.tabs != null) && this.tabs.Contains(string.Empty)) && ProductPage.Config<bool>(null, "FilterTabHideEmpty"))
            {
                this.tabs.Remove(string.Empty);
            }
            if ((this.tabs != null) && (this.tabs.Count == 0))
            {
                this.tabs = null;
            }
            if ((this.DataSource.RequireCaching && (this.dsCache != null)) && ((this.tabs == null) || (recordCache == null)))
            {
                recordCache = this.dsCache.recordCache;
            }
            if ((this.tabs != null) && !asDate)
            {
                this.tabs.Sort();
                if (this.tabsReverse)
                {
                    this.tabs.Reverse();
                }
                if (((num4 = this.tabs.IndexOf(string.Empty)) >= 0) && (num4 < (this.tabs.Count - 1)))
                {
                    for (int num19 = num4; num19 < (this.tabs.Count - 1); num19++)
                    {
                        this.tabs[num19] = this.tabs[num19 + 1];
                    }
                    this.tabs[this.tabs.Count - 1] = string.Empty;
                }
            }
            for (int i = 0; i < recordCache.Count; i++)
            {
                if (recordCache[i] != null)
                {
                    try
                    {
                        if ((this.dsCache == null) ? (string.IsNullOrEmpty(tabProp) ? this.InFilter(this.DataSource, recordCache[i]) : this.InTab(recordCache[i], tabProp, eq, asDate)) : ((recordCache != this.dsCache.recordCache) ? this.InTab(recordCache[i], tabProp, eq, asDate) : this.InFilter(this.DataSource, recordCache[i])))
                        {
                            if ((!string.IsNullOrEmpty(sortProp) || !string.IsNullOrEmpty(groupProp)) || ((this.PageSize <= 0) || ((this.List.Count < this.PageSize) && (this.recCount >= this.PageStart))))
                            {
                                this.List.Add(recordCache[i]);
                                if (!string.IsNullOrEmpty(groupProp) && (groupByCounts || groupShowCounts))
                                {
                                    int num3;
                                    this.groupCounts[recordCache[i][groupProp, string.Empty, this.DataSource]] = this.groupCounts.TryGetValue(recordCache[i][groupProp, string.Empty, this.DataSource], out num3) ? (num3 + 1) : 1;
                                }
                            }
                            this.recCount++;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (sort2Desc)
            {
                sort2Prop = sort2Prop.Substring(1);
            }
            while ((num4 = this.List.IndexOf(null)) >= 0)
            {
                this.List.RemoveAt(num4);
            }
            if ("___roxRandomizedSort".Equals(sortProp))
            {
                this.List.Sort(delegate (CachedRecord one, CachedRecord two) {
                    if ((one == null) && (two == null))
                    {
                        return 0;
                    }
                    if (one == null)
                    {
                        return -2147483648;
                    }
                    if (two == null)
                    {
                        return 0x7fffffff;
                    }
                    if (((one.RecordID > 0L) && (two.RecordID > 0L)) && one.RecordID.Equals(two.RecordID))
                    {
                        return 0;
                    }
                    if ((!Guid.Empty.Equals(one.ID) && !Guid.Empty.Equals(two.ID)) && one.ID.Equals(two.ID))
                    {
                        return 0;
                    }
                    return roxority.Data.DataSource.rnd.Next(-2147483648, 0x7fffffff);
                });
            }
            else
            {
                this.List.Sort(delegate (CachedRecord one, CachedRecord two) {
                    int num6;
                    try
                    {
                        decimal num;
                        decimal num2;
                        int num444;
                        DateTime time;
                        DateTime time2;
                        int num3 = 0;
                        if ((one == null) && (two == null))
                        {
                            return 0;
                        }
                        if ((one != null) && (two == null))
                        {
                            return 1;
                        }
                        if ((one == null) && (two != null))
                        {
                            return -1;
                        }
                        if (this.DataSource == null)
                        {
                            throw new ArgumentNullException("Manager");
                        }
                        string strB = ((string.IsNullOrEmpty(sortProp = sortProp + string.Empty) || (sortProp == "_rox_Name")) ? GetTitle(this, one) : one[sortProp, string.Empty, this.DataSource]) + string.Empty;
                        string str2 = ((string.IsNullOrEmpty(sortProp) || (sortProp == "_rox_Name")) ? GetTitle(this, two) : two[sortProp, string.Empty, this.DataSource]) + string.Empty;
                        string str3 = string.IsNullOrEmpty(groupProp = groupProp + string.Empty) ? string.Empty : (one[groupProp, string.Empty, this.DataSource] + string.Empty);
                        string str4 = string.IsNullOrEmpty(groupProp) ? string.Empty : (two[groupProp, string.Empty, this.DataSource] + string.Empty);
                        if (strB == str2)
                        {
                            if (string.IsNullOrEmpty(sort2Prop))
                            {
                                num444 = 0;
                            }
                            else
                            {
                                strB = one[sort2Prop, string.Empty, this.DataSource] + string.Empty;
                                str2 = two[sort2Prop, string.Empty, this.DataSource] + string.Empty;
                                num444 = sort2Desc ? str2.CompareTo(strB) : strB.CompareTo(str2);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(strB) && !string.IsNullOrEmpty(str2))
                            {
                                if (!isDt.HasValue || !isDt.HasValue)
                                {
                                    isDt = new bool?((DateTime.TryParse(strB, out time) || DateTime.TryParse(strB, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time)) && (DateTime.TryParse(str2, out time2) || DateTime.TryParse(str2, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time2)));
                                }
                                if (!isDec.HasValue || !isDec.HasValue)
                                {
                                    isDec = new bool?((decimal.TryParse(strB, out num) || decimal.TryParse(strB, NumberStyles.Any, CultureInfo.InvariantCulture, out num)) && (decimal.TryParse(str2, out num2) || decimal.TryParse(str2, NumberStyles.Any, CultureInfo.InvariantCulture, out num2)));
                                }
                            }
                            if ((((!isDt.HasValue || !isDt.HasValue) || isDt.Value) && (DateTime.TryParse(strB, out time) || DateTime.TryParse(strB, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time))) && (DateTime.TryParse(str2, out time2) || DateTime.TryParse(str2, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time2)))
                            {
                                isDt = true;
                                num444 = sortDesc ? DateTime.Compare(this.GetDate(time2), this.GetDate(time)) : DateTime.Compare(this.GetDate(time), this.GetDate(time2));
                            }
                            else if ((((!isDec.HasValue || !isDec.HasValue) || isDec.Value) && (decimal.TryParse(strB, out num) || decimal.TryParse(strB, NumberStyles.Any, CultureInfo.InvariantCulture, out num))) && (decimal.TryParse(str2, out num2) || decimal.TryParse(str2, NumberStyles.Any, CultureInfo.InvariantCulture, out num2)))
                            {
                                isDec = true;
                                num444 = sortDesc ? decimal.Compare(num2, num) : decimal.Compare(num, num2);
                            }
                            else
                            {
                                if (isDt.HasValue && isDt.Value)
                                {
                                    isDt = new bool?(string.IsNullOrEmpty(strB) || string.IsNullOrEmpty(str2));
                                }
                                if (isDec.HasValue && isDec.Value)
                                {
                                    isDec = new bool?(string.IsNullOrEmpty(strB) || string.IsNullOrEmpty(str2));
                                }
                                num444 = sortDesc ? string.Compare(str2, strB) : string.Compare(strB, str2);
                            }
                        }
                        if (!string.IsNullOrEmpty(groupProp))
                        {
                            if (str4 == str3)
                            {
                                num3 = 0;
                            }
                            else
                            {
                                if ((!groupByCounts && !string.IsNullOrEmpty(str3)) && !string.IsNullOrEmpty(str4))
                                {
                                    if (!isGrDt.HasValue || !isGrDt.HasValue)
                                    {
                                        isGrDt = new bool?((DateTime.TryParse(str3, out time) || DateTime.TryParse(str3, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time)) && (DateTime.TryParse(str4, out time2) || DateTime.TryParse(str4, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time2)));
                                    }
                                    if (!isGrDec.HasValue || !isGrDec.HasValue)
                                    {
                                        isGrDec = new bool?((decimal.TryParse(str3, out num) || decimal.TryParse(str3, NumberStyles.Any, CultureInfo.InvariantCulture, out num)) && (decimal.TryParse(str4, out num2) || decimal.TryParse(str4, NumberStyles.Any, CultureInfo.InvariantCulture, out num2)));
                                    }
                                }
                                if (groupByCounts)
                                {
                                    int num5;
                                    num3 = (this.groupCounts.TryGetValue(groupDesc ? str4 : str3, out num5) ? num5 : 0).CompareTo(this.groupCounts.TryGetValue(groupDesc ? str3 : str4, out num5) ? num5 : 0);
                                }
                                else if ((((!isGrDt.HasValue || !isGrDt.HasValue) || isGrDt.Value) && (DateTime.TryParse(str3, out time) || DateTime.TryParse(str3, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time))) && (DateTime.TryParse(str4, out time2) || DateTime.TryParse(str4, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time2)))
                                {
                                    isGrDt = true;
                                    num3 = groupDesc ? DateTime.Compare(this.GetDate(time2), this.GetDate(time)) : DateTime.Compare(this.GetDate(time), this.GetDate(time2));
                                }
                                else if ((((!isGrDec.HasValue || !isGrDec.HasValue) || isGrDec.Value) && (decimal.TryParse(str3, out num) || decimal.TryParse(str3, NumberStyles.Any, CultureInfo.InvariantCulture, out num))) && (decimal.TryParse(str4, out num2) || decimal.TryParse(str4, NumberStyles.Any, CultureInfo.InvariantCulture, out num2)))
                                {
                                    isGrDec = true;
                                    num3 = groupDesc ? decimal.Compare(num2, num) : decimal.Compare(num, num2);
                                }
                                else
                                {
                                    if (isGrDt.HasValue && isGrDt.Value)
                                    {
                                        isGrDt = new bool?(string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(str4));
                                    }
                                    if (isGrDec.HasValue && isGrDec.Value)
                                    {
                                        isGrDec = new bool?(string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(str4));
                                    }
                                    num3 = groupDesc ? string.Compare(str4, str3) : string.Compare(str3, str4);
                                }
                            }
                        }
                        num6 = (num3 == 0) ? num444 : num3;
                    }
                    catch (Exception exception)
                    {
                        sortErrors.Add(exception);
                        throw;
                    }
                    return num6;
                });
            }
            if ((this.PageSize > 0) && (this.List.Count > this.PageSize))
            {
                if (this.PageStart > 0)
                {
                    this.List.RemoveRange(0, this.PageStart);
                }
                if (this.List.Count > this.PageSize)
                {
                    this.List.RemoveRange(this.PageSize, this.List.Count - this.PageSize);
                }
            }
            if (this.DataSource.CacheRefresh)
            {
                strArray3 = this.dsCache.cachedProperties.ToArray();
                foreach (CachedRecord record5 in this.List)
                {
                    try
                    {
                        record = this.DataSource.GetRecord(record5.RecordID);
                    }
                    catch
                    {
                        record = null;
                    }
                    if (record != null)
                    {
                        record5.Resync(record, strArray3);
                    }
                }
            }
        }

        internal void PrepareTab(CachedRecord rec, string tabProp, List<string> tabsDone, ref int dtTabs, ref int numTabs, ref string tmp, ref decimal decTmp)
        {
            if (rec.Values.ContainsKey(tabProp))
            {
                if ((rec.Values[tabProp] == null) || (rec.Values[tabProp].Count == 0))
                {
                    if (!tabsDone.Contains(string.Empty))
                    {
                        tabsDone.Add(string.Empty);
                        this.tabs.Add(string.Empty);
                    }
                }
                else
                {
                    for (int i = 0; i < rec.Values[tabProp].Count; i++)
                    {
                        bool flag;
                        DateTime time;
                        tmp = ProductPage.Normalize(rec.Values[tabProp][i] + string.Empty);
                        if (flag = DateTime.TryParse(tmp, out time) || DateTime.TryParse(tmp, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out time))
                        {
                            if (!this.DateIgnoreDay && !this.DateThisYear)
                            {
                                tmp = time.ToShortDateString();
                            }
                            else
                            {
                                tmp = time.ToString((this.DateIgnoreDay && this.DateThisYear) ? "MMMM" : (this.DateIgnoreDay ? "MMMM yyyy" : "dd MMMM"));
                            }
                        }
                        if (!tabsDone.Contains(tmp.ToLowerInvariant()))
                        {
                            tabsDone.Add(tmp.ToLowerInvariant());
                            this.tabs.Add(tmp);
                            if (flag)
                            {
                                dtTabs++;
                            }
                            if (decimal.TryParse(tmp, out decTmp) || decimal.TryParse(tmp, NumberStyles.Any, CultureInfo.InvariantCulture, out decTmp))
                            {
                                numTabs++;
                            }
                        }
                    }
                }
            }
        }

        public StringComparison Comparison
        {
            get
            {
                if (this.comparison == StringComparison.InvariantCulture)
                {
                    string str;
                    this.comparison = StringComparison.InvariantCultureIgnoreCase;
                    if (!string.IsNullOrEmpty(str = ProductPage.Config(ProductPage.GetContext(), "Compare")))
                    {
                        try
                        {
                            this.comparison = (StringComparison) Enum.Parse(typeof(StringComparison), str, true);
                        }
                        catch
                        {
                        }
                    }
                }
                return this.comparison;
            }
        }

        internal static ProductPage.LicInfo L
        {
            get
            {
                if (lic == null)
                {
                    lic = ProductPage.LicInfo.Get(null);
                }
                return lic;
            }
        }
    }
}

