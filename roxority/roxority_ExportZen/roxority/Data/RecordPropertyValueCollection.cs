namespace roxority.Data
{
    using roxority.SharePoint;
    using System;
    using System.Collections;

    public class RecordPropertyValueCollection : IEnumerable
    {
        public readonly roxority.Data.DataSource DataSource;
        public IEnumerator Enum;
        public roxority.SharePoint.Func<int> GetCountDelegate;
        public roxority.SharePoint.Func<object> GetValueDelegate;
        public readonly roxority.Data.Record Record;
        public readonly roxority.Data.RecordProperty RecordProperty;
        public readonly object[] Values;

        public RecordPropertyValueCollection(roxority.Data.DataSource dataSource, roxority.Data.Record record, roxority.Data.RecordProperty prop, object[] values, IEnumerator enumerator, roxority.SharePoint.Func<int> getCount, roxority.SharePoint.Func<object> getValue)
        {
            this.DataSource = dataSource;
            this.Record = record;
            this.RecordProperty = prop;
            this.GetCountDelegate = getCount;
            this.GetValueDelegate = getValue;
            this.Values = values;
            if (this.Values != null)
            {
                this.Enum = this.Values.GetEnumerator();
            }
            else if (enumerator != null)
            {
                this.Enum = enumerator;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Enum;
        }

        public int Count
        {
            get
            {
                if (this.GetCountDelegate != null)
                {
                    return this.GetCountDelegate();
                }
                if (this.Values != null)
                {
                    return this.Values.Length;
                }
                return 0;
            }
        }

        public object Value
        {
            get
            {
                if (this.GetValueDelegate != null)
                {
                    return this.GetValueDelegate();
                }
                if (this.Values != null)
                {
                    if (this.Values.Length == 1)
                    {
                        return this.Values[0];
                    }
                    if (this.Values.Length != 0)
                    {
                        return this.Values;
                    }
                }
                return null;
            }
        }
    }
}

