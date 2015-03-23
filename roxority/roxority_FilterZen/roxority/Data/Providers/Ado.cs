namespace roxority.Data.Providers
{
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Reflection;

    public class Ado : DataSource
    {
        private string adoProvider;
        private IDbCommand cmd;
        private IDbConnection conn;
        private Guid? contextID = null;
        private RecordPropertyCollection props;
        private IDataReader reader;
        private IEnumerator recEnum;
        private List<Record> recs = new List<Record>();
        private Reflector refl;

        public Ado()
        {
            this.props = new RecordPropertyCollection(this);
        }

        public override void Dispose()
        {
            if (!base.noDispose)
            {
                try
                {
                    if (this.reader != null)
                    {
                        this.reader.Dispose();
                        this.reader = null;
                    }
                    if (this.cmd != null)
                    {
                        this.cmd.Dispose();
                        this.cmd = null;
                    }
                    if (this.conn != null)
                    {
                        this.conn.Dispose();
                        this.conn = null;
                    }
                }
                catch
                {
                }
                finally
                {
                    base.Dispose();
                }
            }
        }

        public override RecordPropertyValueCollection GetPropertyValues(Record rec, RecordProperty prop)
        {
            IDictionary mainItem = rec.MainItem as IDictionary;
            return new RecordPropertyValueCollection(this, rec, prop, (mainItem == null) ? new object[0] : new object[] { mainItem[prop.Name] }, null, null, null);
        }

        public override Record GetRecord(long recID)
        {
            if (this.recs.Count == 0)
            {
                foreach (Record record in this)
                {
                    if (record.RecordID == recID)
                    {
                        return record;
                    }
                }
            }
            if (this.recs.Count < recID)
            {
                return null;
            }
            return this.recs[((int) recID) - 1];
        }

        public override void InitSchema(JsonSchemaManager.Schema owner)
        {
            IDictionary pmore = new OrderedDictionary();
            base.InitSchema(owner);
            pmore["config"] = "DataProviders";
            pmore["always_show_help"] = true;
            base.AddSchemaProp(owner, "ac", "ConfigChoice", pmore);
            pmore = new OrderedDictionary();
            pmore["lines"] = 4;
            pmore["always_show_help"] = true;
            base.AddSchemaProp(owner, "cs", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["always_show_help"] = true;
            pmore["is_password"] = true;
            base.AddSchemaProp(owner, "pw", "String", pmore);
            pmore = new OrderedDictionary();
            pmore["always_show_help"] = true;
            pmore["lines"] = 8;
            base.AddSchemaProp(owner, "sq", "String", pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pd", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pt", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pp", "DataFields", "w", true, pmore);
            pmore = new OrderedDictionary();
            base.AddSchemaProp(owner, "pu", "DataFields", "w", true, pmore);
            this.InitSchemaForCaching(owner, false, true);
        }

        public override bool MoveNext()
        {
            if (this.recEnum == null)
            {
                return this.AdoReader.Read();
            }
            return this.recEnum.MoveNext();
        }

        public override void Reset()
        {
            if ((this.recEnum == null) && (this.recs.Count > 0))
            {
                this.recEnum = this.recs.GetEnumerator();
            }
            if (this.recEnum != null)
            {
                this.recEnum.Reset();
            }
        }

        public IDbCommand AdoCommand
        {
            get
            {
                if (this.cmd == null)
                {
                    this.cmd = this.AdoConnection.CreateCommand();
                    this.cmd.CommandText = this.AdoQuery;
                }
                return this.cmd;
            }
        }

        public IDbConnection AdoConnection
        {
            get
            {
                if (this.conn == null)
                {
                    this.conn = this.AdoReflector.New(this.AdoProviderType, new object[] { this.AdoConnectionString }) as IDbConnection;
                }
                if (this.conn == null)
                {
                    throw new Exception(ProductPage.GetResource("Data_AdoConnError", new object[] { this.AdoProviderName }));
                }
                return this.conn;
            }
        }

        public string AdoConnectionString
        {
            get
            {
                return base["cs", string.Empty].Replace("{$ROXPWD$}", base["pw", string.Empty]);
            }
        }

        public string AdoProvider
        {
            get
            {
                if (this.adoProvider == null)
                {
                    this.adoProvider = base["ac", string.Empty];
                }
                return this.adoProvider;
            }
        }

        public string AdoProviderAssembly
        {
            get
            {
                return this.AdoProvider.Substring(this.AdoProvider.IndexOf(',') + 1).Trim();
            }
        }

        public string AdoProviderName
        {
            get
            {
                JsonSchemaManager.Property.Type.ConfigChoice propertyType = base.JsonSchema[this.SchemaPropNamePrefix + "_ac"].PropertyType as JsonSchemaManager.Property.Type.ConfigChoice;
                return (propertyType.Choices[this.AdoProvider] + string.Empty);
            }
        }

        public string AdoProviderType
        {
            get
            {
                if (this.AdoProvider.IndexOf(',') <= 0)
                {
                    return this.AdoProvider;
                }
                return this.AdoProvider.Substring(0, this.AdoProvider.IndexOf(','));
            }
        }

        public string AdoQuery
        {
            get
            {
                return base["sq", string.Empty];
            }
        }

        public IDataReader AdoReader
        {
            get
            {
                if (this.reader == null)
                {
                    this.recs.Clear();
                    this.AdoConnection.Open();
                    this.reader = this.AdoCommand.ExecuteReader();
                }
                return this.reader;
            }
        }

        public Reflector AdoReflector
        {
            get
            {
                if (this.refl == null)
                {
                    this.refl = new Reflector(Assembly.LoadWithPartialName(this.AdoProviderAssembly));
                }
                return this.refl;
            }
        }

        public override Guid ContextID
        {
            get
            {
                if ((!this.contextID.HasValue || !this.contextID.HasValue) && ((base.JsonInstance != null) && Guid.Empty.Equals(this.contextID = new Guid?(ProductPage.GetGuid(base.JsonInstance["id"] + string.Empty, true)))))
                {
                    this.contextID = new Guid("ef0bd2b1-2c73-597f-b86c-1783eb216994");
                }
                if (!this.contextID.HasValue)
                {
                    return Guid.Empty;
                }
                return this.contextID.Value;
            }
        }

        public override long Count
        {
            get
            {
                return ((this.RequireCaching && (this.recs.Count < 1)) ? ((long) (-1)) : ((long) this.recs.Count));
            }
        }

        public override object Current
        {
            get
            {
                Record record;
                if (this.recEnum != null)
                {
                    return this.recEnum.Current;
                }
                IDictionary item = new OrderedDictionary();
                for (int i = 0; i < this.AdoReader.FieldCount; i++)
                {
                    object obj2;
                    string str;
                    try
                    {
                        obj2 = this.AdoReader.GetValue(i);
                    }
                    catch (Exception exception)
                    {
                        obj2 = exception;
                    }
                    if (this.props.GetPropertyByName(str = this.AdoReader.GetName(i)) == null)
                    {
                        this.props.Props.Add(new RecordProperty(this, str, this.AdoReader.GetFieldType(i)));
                    }
                    item.Add(str, obj2);
                }
                this.recs.Add(record = new Record(this, item, null, Guid.Empty, (long) (this.recs.Count + 1)));
                return record;
            }
        }

        public override RecordPropertyCollection Properties
        {
            get
            {
                if (this.props.Props.Count == 0)
                {
                    IEnumerator enumerator = base.GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            object current = enumerator.Current;
                            goto Label_003F;
                        }
                    }
                }
            Label_003F:
                return this.props;
            }
        }

        public override string SchemaPropNamePrefix
        {
            get
            {
                return "db";
            }
        }
    }
}

