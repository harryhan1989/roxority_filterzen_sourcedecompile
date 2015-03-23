namespace roxority.Data
{
    using roxority.SharePoint;
    using System;
    using System.Collections.Generic;

    public class RecordProperty
    {
        private bool? allowDateParsing = null;
        public readonly roxority.Data.DataSource DataSource;
        public readonly string Name;
        public readonly object Property;

        public RecordProperty(roxority.Data.DataSource dataSource, string name, object property)
        {
            this.DataSource = dataSource;
            this.Name = name;
            this.Property = property;
        }

        public static IEnumerable<string> ExtractNames(string name, bool isVal, roxority.Data.DataSource ds)
        {
            string str;
            string[] strArray;
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(str = isVal ? name : ds[name.Substring(name.StartsWith("rox___") ? "rox___".Length : 0), string.Empty]) && ((strArray = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length > 0))
            {
                foreach (string str2 in strArray)
                {
                    int num2;
                    int num = -1;
                    while ((num < (num = str2.IndexOf('[', num + 1))) && ((num2 = str2.IndexOf(']', num + 1)) > num))
                    {
                        if (!list.Contains(str = str2.Substring(num + 1, (num2 - num) - 1)))
                        {
                            list.Add(str);
                        }
                    }
                }
            }
            return list;
        }

        public bool AllowDateParsing
        {
            get
            {
                if (!this.allowDateParsing.HasValue || !this.allowDateParsing.HasValue)
                {
                    this.allowDateParsing = new bool?(this.DataSource.AllowDateParsing(this));
                }
                return this.allowDateParsing.Value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.DataSource.GetPropertyDisplayName(this);
            }
        }

        internal class Comparer : IComparer<string>
        {
            public readonly roxority.Data.DataSource DataSource;

            public Comparer(roxority.Data.DataSource dataSource)
            {
                this.DataSource = dataSource;
            }

            public int Compare(string x, string y)
            {
                string str;
                if (x.Equals(y))
                {
                    return 0;
                }
                if (!string.IsNullOrEmpty(str = ProductPage.GetResource("Disp_" + x, new object[0])))
                {
                    x = str;
                }
                if (!string.IsNullOrEmpty(str = ProductPage.GetResource("Disp_" + y, new object[0])))
                {
                    y = str;
                }
                if (this.DataSource != null)
                {
                    RecordProperty property;
                    if (((property = DataSourceConsumer.dataProps.GetPropertyByName(x)) != null) && !string.IsNullOrEmpty(str = property.DisplayName))
                    {
                        x = str;
                    }
                    if (((property = DataSourceConsumer.dataProps.GetPropertyByName(y)) != null) && !string.IsNullOrEmpty(str = property.DisplayName))
                    {
                        y = str;
                    }
                }
                return x.CompareTo(y);
            }
        }
    }
}

