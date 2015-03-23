namespace roxority.Data
{
    using roxority.SharePoint;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CachedRecord
    {
        internal string friendlyName = string.Empty;
        public readonly Guid ID = Guid.Empty;
        public readonly long RecordID;
        public string Url;
        public readonly Dictionary<string, List<object>> Values = new Dictionary<string, List<object>>();

        public CachedRecord(Record rec, string[] props)
        {
            this.RecordID = rec.RecordID;
            this.ID = rec.ID;
            this.Resync(rec, props);
        }

        public string Get(string key, string defaultValue, DataSource ds)
        {
            string str = string.Join(DataSourceConsumer.cfgMultiPropJoin, DataSourceConsumer.Get(ds, this, null, key, defaultValue, false).ToArray());
            if ((!ProductPage.LicEdition(ProductPage.GetContext(), DataSourceConsumer.L, 0) && (str != null)) && (str.Length > 3))
            {
                return (str.Substring(0, 3) + "...");
            }
            return str;
        }

        public void Resync(Record rec, string[] props)
        {
            Exception exception = null;
            List<string> list = new List<string>(props);
            this.Url = rec.DataSource.GetRecordUri(rec);
            props = list.ToArray();
            list.Clear();
            foreach (string str2 in props)
            {
                if (str2 != null)
                {
                    string[] strArray;
                    string str;
                    exception = null;
                    RecordPropertyValueCollection values = null;
                    if ((((strArray = str2.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (strArray.Length >= 1)) && (!string.IsNullOrEmpty(str = strArray[0].Trim()) && !list.Contains(str)))
                    {
                        list.Add(str);
                        this.Values[str] = new List<object>();
                        try
                        {
                            values = rec[str];
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                        }
                        if (exception != null)
                        {
                            this.Values[str].Add(exception.Message);
                        }
                        else if (values != null)
                        {
                            if (values.Count > 0)
                            {
                                foreach (object obj2 in values)
                                {
                                    this.Values[str].Add(obj2);
                                }
                            }
                            else if (values.Value != null)
                            {
                                this.Values[str].Add(values.Value);
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return (this.RecordID + ";#" + this.friendlyName);
        }

        public string this[string key, string defaultValue, DataSource ds]
        {
            get
            {
                return this.Get(key, defaultValue, ds);
            }
        }
    }
}

