namespace roxority.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class RecordPropertyCollection : IEnumerable
    {
        public readonly roxority.Data.DataSource DataSource;
        private SortedDictionary<string, string> dict;
        public readonly List<RecordProperty> Props = new List<RecordProperty>();

        public RecordPropertyCollection(roxority.Data.DataSource dataSource)
        {
            this.DataSource = dataSource;
        }

        public IEnumerator GetEnumerator()
        {
            return this.Props.GetEnumerator();
        }

        public RecordProperty GetPropertyByName(string name)
        {
            string altName = this.DataSource.RewritePropertyName(name);
            RecordProperty property = this.Props.Find(prop => prop.Name == name);
            if (property == null)
            {
                property = this.Props.Find(prop => prop.Name == altName);
            }
            return property;
        }

        public SortedDictionary<string, string> Dict
        {
            get
            {
                if (this.dict == null)
                {
                    List<string> list = new List<string>();
                    this.dict = new SortedDictionary<string, string>();
                    foreach (RecordProperty property in this.Props)
                    {
                        string str;
                        if (!list.Contains(str = property.DisplayName))
                        {
                            list.Add(property.DisplayName);
                        }
                        else if (property.Name != str)
                        {
                            str = str + " [" + property.Name + "]";
                        }
                        this.dict[property.Name] = str;
                    }
                }
                return this.dict;
            }
        }

        public OrderedDictionary SortedByName
        {
            get
            {
                OrderedDictionary dictionary = new OrderedDictionary();
                List<RecordProperty> list = new List<RecordProperty>(this.Props);
                list.Sort(delegate (RecordProperty one, RecordProperty two) {
                    int num = one.Name.CompareTo(two.Name);
                    if (num != 0)
                    {
                        return num;
                    }
                    return one.DisplayName.CompareTo(two.DisplayName);
                });
                foreach (RecordProperty property in list)
                {
                    dictionary.Add(property.Name, property.DisplayName);
                }
                return dictionary;
            }
        }

        public OrderedDictionary SortedByTitle
        {
            get
            {
                OrderedDictionary dictionary = new OrderedDictionary();
                List<RecordProperty> list = new List<RecordProperty>(this.Props);
                list.Sort(delegate (RecordProperty one, RecordProperty two) {
                    int num = one.DisplayName.CompareTo(two.DisplayName);
                    if (num != 0)
                    {
                        return num;
                    }
                    return one.Name.CompareTo(two.Name);
                });
                foreach (RecordProperty property in list)
                {
                    dictionary.Add(property.Name, property.DisplayName);
                }
                return dictionary;
            }
        }
    }
}

