namespace roxority.Data.Providers
{
    using roxority.Data;
    using roxority.Shared;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class Dummy : DataSource
    {
        private IEnumerator enumerator;
        private RecordPropertyCollection propCol;
        private List<Record> recs;
        private ArrayList staticRecs;

        public override string GetPropertyDisplayName(RecordProperty prop)
        {
            string str = prop.Property + string.Empty;
            string[] strArray = str.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length <= 1)
            {
                return str;
            }
            return string.Join(":", strArray, 1, strArray.Length - 1);
        }

        public override RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            string str = rec.Tags[prop.Name] + string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                if ((this.StaticRecs != null) && (this.StaticRecs.Count >= rec.RecordID))
                {
                    IDictionary dictionary = this.StaticRecs[((int) rec.RecordID) - 1] as IDictionary;
                    rec.Tags[prop.Name] = str = (dictionary == null) ? null : (dictionary[prop.Name] as string);
                }
                else
                {
                    int num=0;
                    if (!"Title".Equals(prop.Name))
                    {
                        num = 1;
                    }
                    rec.Tags[prop.Name] = str = num.Equals(DataSource.rnd.Next(0, 2)) ? (ProductPage.GetResource("PC_DataSources_t_" + base.GetType().Name, new object[0]) + " #" + rec.RecordID) : DataSource.rnd.Next(0x3e8, 0xf4240).ToString();
                }
            }
            return new RecordPropertyValueCollection(this, rec, prop, new object[] { str }, null, null, null);
        }

        public override Record GetRecord(long recID)
        {
            return this.Records[((int) recID) - 1];
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            IDictionary pmore = new OrderedDictionary();
            pmore["lines"] = 4;
            pmore["default"] = "ID\r\nTitle\r\nSampleColumn";
            pmore["validator"] = "roxValidateNonEmpty";
            base.AddSchemaProp(owner, "f", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["validator"] = "roxValidateNumeric";
            pmore["default"] = "85";
            base.AddSchemaProp(owner, "c", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["lines"] = "4";
            pmore["validator"] = "roxValidateJson";
            base.AddSchemaProp(owner, "r", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = "SampleColumn:" + ProductPage.GetResource("PC_DataSources_t_" + base.GetType().Name, new object[0]);
            base.AddSchemaProp(owner, "pd", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            pmore["default"] = "[Title]";
            base.AddSchemaProp(owner, "pt", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pp", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pu", "DataFields", "w", true, pmore);
            this.InitSchemaForCaching(owner, false, false);
        }

        public override bool MoveNext()
        {
            return this.Enumerator.MoveNext();
        }

        public override void Reset()
        {
            this.Enumerator.Reset();
        }

        public override long Count
        {
            get
            {
                return (long) this.Records.Count;
            }
        }

        public override object Current
        {
            get
            {
                return this.Enumerator.Current;
            }
        }

        public IEnumerator Enumerator
        {
            get
            {
                if (this.enumerator == null)
                {
                    this.enumerator = this.Records.GetEnumerator();
                }
                return this.enumerator;
            }
        }

        public override RecordPropertyCollection Properties
        {
            get
            {
                if (this.propCol == null)
                {
                    this.propCol = new RecordPropertyCollection(this);
                    if (base.JsonInstance != null)
                    {
                        string str;
                        List<string> list;
                        if (string.IsNullOrEmpty(str = base["f"] + string.Empty))
                        {
                            for (int i = 0; i < DataSource.rnd.Next(3, 8); i++)
                            {
                                object obj2 = str;
                                str = string.Concat(new object[] { obj2, "\r\nField", i, ":Field #", i + 1 });
                            }
                        }
                        if ((list = new List<string>(str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))).Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                list[j] = list[j].Trim();
                            }
                            ProductPage.RemoveDuplicates<string>(list);
                            list.Sort(StringComparer.InvariantCultureIgnoreCase);
                            foreach (string str2 in list)
                            {
                                string[] strArray = str2.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                this.propCol.Props.Add(new RecordProperty(this, (strArray.Length > 1) ? strArray[0] : str2, str2));
                            }
                        }
                    }
                }
                return this.propCol;
            }
        }

        public List<Record> Records
        {
            get
            {
                if (this.recs == null)
                {
                    this.recs = new List<Record>();
                    if (base.JsonInstance != null)
                    {
                        if (this.StaticRecs != null)
                        {
                            foreach (IDictionary dictionary2 in this.staticRecs)
                            {
                                this.recs.Add(new Record(this, dictionary2, null, Guid.Empty, (long) (this.recs.Count + 1)));
                            }
                        }
                        else
                        {
                            int num;
                            if (!int.TryParse(base["c", string.Empty], out num) || (num <= 1))
                            {
                                num = DataSource.rnd.Next(2, 100);
                            }
                            for (int i = 0; i < num; i++)
                            {
                                IDictionary item = new OrderedDictionary();
                                foreach (RecordProperty property in this.Properties)
                                {
                                    object[] objArray = new object[5];
                                    objArray[0] = property.Name;
                                    objArray[1] = " #";
                                    objArray[2] = i + 1;
                                    objArray[3] = ": ";
                                    int num3 = 1;
                                    objArray[4] = num3.Equals(DataSource.rnd.Next(0, 2)) ? DataSource.rnd.Next(2, 0x7fffffff).ToString() : ProductPage.GuidLower(Guid.NewGuid());
                                    item[property.Name] = string.Concat(objArray);
                                }
                                this.recs.Add(new Record(this, item, null, Guid.Empty, (long) (this.recs.Count + 1)));
                            }
                        }
                    }
                }
                return this.recs;
            }
        }

        public override bool RequireCaching
        {
            get
            {
                return false;
            }
        }

        public override string SchemaPropNamePrefix
        {
            get
            {
                return "du";
            }
        }

        public ArrayList StaticRecs
        {
            get
            {
                if (this.staticRecs == null)
                {
                    string str;
                    this.staticRecs = new ArrayList();
                    if (((base.JsonInstance != null) && !string.IsNullOrEmpty(str = base["r", string.Empty])) && ((this.staticRecs = JSON.JsonDecode(str) as ArrayList) == null))
                    {
                        throw new FormatException("Bad JSON syntax: " + ProductPage.GetResource("PC_DataSources_" + this.SchemaPropNamePrefix + "_r", new object[0]));
                    }
                }
                if (this.staticRecs.Count != 0)
                {
                    return this.staticRecs;
                }
                return null;
            }
        }
    }
}

