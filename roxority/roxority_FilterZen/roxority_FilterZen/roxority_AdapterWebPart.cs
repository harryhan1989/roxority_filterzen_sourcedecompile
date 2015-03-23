namespace roxority_FilterZen
{
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using roxority.Shared;
    using roxority.Shared.ComponentModel;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Web.UI.WebControls.WebParts;

    [Guid("cdbec42f-2b0c-4c1c-8984-1a38f425e8f6")]
    public class roxority_AdapterWebPart : WebPartBase, ICellConsumer, IFilterConsumer, IListConsumer, IParametersInConsumer, IParametersOutConsumer, IRowConsumer, ICellProvider, IFilterProvider, IListProvider, IParametersInProvider, IParametersOutProvider, IRowProvider
    {
        internal string cellFieldName;
        internal string cellFieldTitle;
        internal object cellValue;
        internal int consumersWaiting;
        internal Dictionary<IWebPartField, object> fieldProviders = new Dictionary<IWebPartField, object>();
        internal SortedDictionary<string, string> fields = new SortedDictionary<string, string>();
        internal bool filterClear;
        internal bool filterNone;
        internal Dictionary<ITransformableFilterValues, Duo<string, ReadOnlyCollection<string>>> filterProviders = new Dictionary<ITransformableFilterValues, Duo<string, ReadOnlyCollection<string>>>();
        internal SortedDictionary<string, string> filterValues = new SortedDictionary<string, string>();
        internal DataTable list;
        internal bool listPartial;
        internal bool paramInNone;
        internal ParameterInProperty[] paramInProps;
        internal string[] paramInValues;
        internal bool paramOutNone;
        internal ParameterOutProperty[] paramOutProps;
        internal string[] paramOutValues;
        internal Dictionary<IWebPartParameters, IDictionary> paramProviders = new Dictionary<IWebPartParameters, IDictionary>();
        internal Dictionary<IWebPartRow, object> rowProviders = new Dictionary<IWebPartRow, object>();
        internal DataRow[] rows;
        internal string rowSel = "Standard";
        internal Dictionary<IWebPartTable, ICollection> tableProviders = new Dictionary<IWebPartTable, ICollection>();

        public event CellConsumerInitEventHandler CellConsumerInit;

        public event CellProviderInitEventHandler CellProviderInit;

        public event CellReadyEventHandler CellReady;

        public event ClearFilterEventHandler ClearFilter;

        public event FilterConsumerInitEventHandler FilterConsumerInit;

        public event ListProviderInitEventHandler ListProviderInit;

        public event ListReadyEventHandler ListReady;

        public event NoFilterEventHandler NoFilter;

        public event NoParametersInEventHandler NoParametersIn;

        public event NoParametersOutEventHandler NoParametersOut;

        public event ParametersInConsumerInitEventHandler ParametersInConsumerInit;

        public event ParametersInReadyEventHandler ParametersInReady;

        public event ParametersOutProviderInitEventHandler ParametersOutProviderInit;

        public event ParametersOutReadyEventHandler ParametersOutReady;

        public event PartialListReadyEventHandler PartialListReady;

        public event RowProviderInitEventHandler RowProviderInit;

        public event RowReadyEventHandler RowReady;

        public event SetFilterEventHandler SetFilter;

        public roxority_AdapterWebPart()
        {
            this.ChromeType = PartChromeType.None;
        }

        public override ConnectionRunAt CanRunAt()
        {
            return ConnectionRunAt.Server;
        }

        public override void EnsureInterfaces()
        {
            base.RegisterInterface("ICellConsumer", "ICellConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "ICellConsumer (Get Cell From)", "ICellConsumer", true);
            base.RegisterInterface("IFilterConsumer", "IFilterConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IFilterConsumer (Get Filters From)", "IFilterConsumer", true);
            base.RegisterInterface("IListConsumer", "IListConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IListConsumer (Get List From)", "IListConsumer", true);
            base.RegisterInterface("IParametersInConsumer", "IParametersInConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IParametersInConsumer (Get Input From)", "IParametersInConsumer", true);
            base.RegisterInterface("IParametersOutConsumer", "IParametersOutConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IParametersOutConsumer (Get Output From)", "IParametersOutConsumer", true);
            base.RegisterInterface("IRowConsumer", "IRowConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IRowConsumer (Get Row From)", "IRowConsumer", true);
            base.RegisterInterface("ICellProvider", "ICellProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "ICellProvider (Send Cell To)", "ICellProvider", true);
            base.RegisterInterface("IFilterProvider", "IFilterProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IFilterProvider (Send Filters To)", "IFilterProvider", true);
            base.RegisterInterface("IListProvider", "IListProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IListProvider (Send List To)", "IListProvider", true);
            base.RegisterInterface("IParametersInProvider", "IParametersInProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IParametersInProvider (Send Input To)", "IParametersInProvider", true);
            base.RegisterInterface("IParametersOutProvider", "IParametersOutProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IParametersOutProvider (Send Output To)", "IParametersOutProvider", true);
            base.RegisterInterface("IRowProvider", "IRowProvider", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, "IRowProvider (Send Row To)", "IRowProvider", true);
        }

        public override InitEventArgs GetInitEventArgs(string interfaceName)
        {
            Duo<string[], string[]> fields = this.Fields;
            if (((fields == null) || (fields.Value1 == null)) || (fields.Value1.Length == 0))
            {
                fields = new Duo<string[], string[]>(new string[] { interfaceName }, new string[] { interfaceName });
            }
            if (interfaceName == "ICellConsumer")
            {
                return new CellConsumerInitEventArgs { FieldDisplayName = "ICellConsumer", FieldName = "ICellConsumer" };
            }
            if (interfaceName == "IFilterConsumer")
            {
                return new FilterConsumerInitEventArgs { FieldList = fields.Value1, FieldDisplayList = fields.Value2 };
            }
            if (interfaceName == "IParametersInConsumer")
            {
                ParameterInProperty[] propertyArray = new ParameterInProperty[fields.Value1.Length];
                for (int i = 0; i < propertyArray.Length; i++)
                {
                    propertyArray[i] = new ParameterInProperty { Description = (fields.Value2.Length > i) ? ((string) fields.Value2[i]) : ((string) fields.Value1[i]), ParameterDisplayName = (fields.Value2.Length > i) ? ((string) fields.Value2[i]) : ((string) fields.Value1[i]), ParameterName = fields.Value1[i], Required = false };
                }
                return new ParametersInConsumerInitEventArgs { ParameterInProperties = propertyArray };
            }
            if (interfaceName == "ICellProvider")
            {
                return new CellProviderInitEventArgs { FieldName = "ICellProvider", FieldDisplayName = "ICellProvider" };
            }
            if (interfaceName == "IListProvider")
            {
                return new ListProviderInitEventArgs { FieldList = fields.Value1, FieldDisplayList = fields.Value2 };
            }
            if (interfaceName == "IParametersOutProvider")
            {
                ParameterOutProperty[] propertyArray2 = new ParameterOutProperty[fields.Value1.Length];
                for (int j = 0; j < propertyArray2.Length; j++)
                {
                    propertyArray2[j] = new ParameterOutProperty { Description = (fields.Value2.Length > j) ? ((string) fields.Value2[j]) : ((string) fields.Value1[j]), ParameterDisplayName = (fields.Value2.Length > j) ? ((string) fields.Value2[j]) : ((string) fields.Value1[j]), ParameterName = fields.Value1[j] };
                }
                return new ParametersOutProviderInitEventArgs { ParameterOutProperties = propertyArray2 };
            }
            if (interfaceName == "IRowProvider")
            {
                return new RowProviderInitEventArgs { FieldList = fields.Value1, FieldDisplayList = fields.Value2 };
            }
            return base.GetInitEventArgs(interfaceName);
        }

        void ICellConsumer.CellProviderInit(object sender, CellProviderInitEventArgs e)
        {
            if (this.cellFieldName == null)
            {
                this.cellFieldName = e.FieldName;
            }
            else
            {
                e.FieldName = this.cellFieldName;
            }
            if (this.cellFieldTitle == null)
            {
                this.cellFieldTitle = e.FieldDisplayName;
            }
            else
            {
                e.FieldDisplayName = this.cellFieldTitle;
            }
        }

        void ICellConsumer.CellReady(object sender, CellReadyEventArgs e)
        {
            this.cellValue = e.Cell;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void ICellProvider.CellConsumerInit(object sender, CellConsumerInitEventArgs e)
        {
            if (this.cellFieldName == null)
            {
                this.cellFieldName = e.FieldName;
            }
            else
            {
                e.FieldName = this.cellFieldName;
            }
            if (this.cellFieldTitle == null)
            {
                this.cellFieldTitle = e.FieldDisplayName;
            }
            else
            {
                e.FieldDisplayName = this.cellFieldTitle;
            }
        }

        void IFilterConsumer.ClearFilter(object sender, EventArgs e)
        {
            this.filterClear = true;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IFilterConsumer.NoFilter(object sender, EventArgs e)
        {
            this.filterNone = true;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IFilterConsumer.SetFilter(object sender, SetFilterEventArgs e)
        {
            string str = null;
            List<string> list = new List<string>();
            foreach (string str2 in e.FilterExpression.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray;
                if ((strArray = str2.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)).Length > 1)
                {
                    if (str == null)
                    {
                        list.Add(str = string.Join("=", strArray, 1, strArray.Length - 1));
                    }
                    else
                    {
                        this.filterValues[str] = string.Join("=", strArray, 1, strArray.Length - 1);
                        str = null;
                    }
                }
            }
            this.Fields = new Duo<string[], string[]>(list.ToArray(), list.ToArray());
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IFilterProvider.FilterConsumerInit(object sender, FilterConsumerInitEventArgs e)
        {
            this.Fields = new Duo<string[], string[]>(e.FieldList, e.FieldDisplayList);
        }

        void IListConsumer.ListProviderInit(object sender, ListProviderInitEventArgs e)
        {
            this.Fields = new Duo<string[], string[]>(e.FieldList, e.FieldDisplayList);
        }

        void IListConsumer.ListReady(object sender, ListReadyEventArgs e)
        {
            this.listPartial = false;
            this.list = e.List;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IListConsumer.PartialListReady(object sender, PartialListReadyEventArgs e)
        {
            if (this.listPartial = (this.list == null) && (e.List != null))
            {
                this.list = e.List;
            }
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IParametersInConsumer.NoParametersIn(object sender, EventArgs e)
        {
            this.paramInNone = true;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IParametersInConsumer.ParametersInReady(object sender, ParametersInReadyEventArgs e)
        {
            this.paramInNone = false;
            this.paramInValues = e.ParameterValues;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IParametersInProvider.ParametersInConsumerInit(object sender, ParametersInConsumerInitEventArgs e)
        {
            this.paramInProps = e.ParameterInProperties;
            if (this.paramInProps != null)
            {
                this.Fields = new Duo<string[], string[]>(Array.ConvertAll<ParameterInProperty, string>(this.paramInProps, pip => pip.ParameterName), Array.ConvertAll<ParameterInProperty, string>(this.paramInProps, pip => pip.ParameterDisplayName));
            }
        }

        void IParametersOutConsumer.NoParametersOut(object sender, EventArgs e)
        {
            this.paramOutNone = true;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IParametersOutConsumer.ParametersOutProviderInit(object sender, ParametersOutProviderInitEventArgs e)
        {
            this.paramOutProps = e.ParameterOutProperties;
            if (this.paramOutProps != null)
            {
                this.Fields = new Duo<string[], string[]>(Array.ConvertAll<ParameterOutProperty, string>(this.paramOutProps, pop => pop.ParameterName), Array.ConvertAll<ParameterOutProperty, string>(this.paramOutProps, pop => pop.ParameterDisplayName));
            }
        }

        void IParametersOutConsumer.ParametersOutReady(object sender, ParametersOutReadyEventArgs e)
        {
            this.paramOutNone = false;
            this.paramOutValues = e.ParameterValues;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        void IRowConsumer.RowProviderInit(object sender, RowProviderInitEventArgs e)
        {
            this.Fields = new Duo<string[], string[]>(e.FieldList, e.FieldDisplayList);
        }

        void IRowConsumer.RowReady(object sender, RowReadyEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.SelectionStatus))
            {
                this.rowSel = e.SelectionStatus;
            }
            this.rows = e.Rows;
            if (--this.consumersWaiting == 0)
            {
                this.PartCommunicationMain();
            }
        }

        public override void PartCommunicationConnect(string interfaceName, Microsoft.SharePoint.WebPartPages.WebPart connectedPart, string connectedInterfaceName, ConnectionRunAt runAt)
        {
        }

        public override void PartCommunicationInit()
        {
            if (this.CellConsumerInit != null)
            {
                this.consumersWaiting++;
                this.CellConsumerInit(this, this.GetInitEventArgs("ICellConsumer") as CellConsumerInitEventArgs);
            }
            if (this.FilterConsumerInit != null)
            {
                this.consumersWaiting++;
                this.FilterConsumerInit(this, this.GetInitEventArgs("IFilterConsumer") as FilterConsumerInitEventArgs);
            }
            if (this.ParametersInConsumerInit != null)
            {
                this.consumersWaiting++;
                this.ParametersInConsumerInit(this, this.GetInitEventArgs("IParametersInConsumer") as ParametersInConsumerInitEventArgs);
            }
            if (this.CellProviderInit != null)
            {
                this.CellProviderInit(this, this.GetInitEventArgs("ICellProvider") as CellProviderInitEventArgs);
            }
            if (this.ListProviderInit != null)
            {
                this.ListProviderInit(this, this.GetInitEventArgs("IListProvider") as ListProviderInitEventArgs);
            }
            if (this.RowProviderInit != null)
            {
                this.RowProviderInit(this, this.GetInitEventArgs("IRowProvider") as RowProviderInitEventArgs);
            }
        }

        public override void PartCommunicationMain()
        {
            if (this.consumersWaiting == 0)
            {
                if (this.CellReady != null)
                {
                    CellReadyEventArgs e = new CellReadyEventArgs {
                        Cell = this.Provide(ProviderPreference.SingleValue)
                    };
                    this.CellReady(this, e);
                }
                if (this.filterClear && (this.ClearFilter != null))
                {
                    this.ClearFilter(this, EventArgs.Empty);
                }
                else if (this.filterNone && (this.NoFilter != null))
                {
                    this.NoFilter(this, EventArgs.Empty);
                }
                else if (this.SetFilter != null)
                {
                    SetFilterEventArgs args2 = new SetFilterEventArgs {
                        FilterExpression = this.Provide(ProviderPreference.FilterString) as string
                    };
                    this.SetFilter(this, args2);
                }
                if (this.listPartial && (this.PartialListReady != null))
                {
                    PartialListReadyEventArgs args3 = new PartialListReadyEventArgs {
                        List = this.Provide(ProviderPreference.Table) as DataTable
                    };
                    this.PartialListReady(this, args3);
                }
                else if (this.ListReady != null)
                {
                    ListReadyEventArgs args4 = new ListReadyEventArgs {
                        List = this.Provide(ProviderPreference.Table) as DataTable
                    };
                    this.ListReady(this, args4);
                }
                if (this.paramInNone && (this.NoParametersIn != null))
                {
                    this.NoParametersIn(this, EventArgs.Empty);
                }
                else if (this.ParametersInReady != null)
                {
                    ParametersInReadyEventArgs args5 = new ParametersInReadyEventArgs {
                        ParameterValues = this.Provide(ProviderPreference.Values) as string[]
                    };
                    this.ParametersInReady(this, args5);
                }
                if (this.paramOutNone && (this.NoParametersOut != null))
                {
                    this.NoParametersOut(this, EventArgs.Empty);
                }
                else if (this.ParametersOutReady != null)
                {
                    ParametersOutReadyEventArgs args6 = new ParametersOutReadyEventArgs {
                        ParameterValues = this.Provide(ProviderPreference.Values) as string[]
                    };
                    this.ParametersOutReady(this, args6);
                }
                if (this.RowReady != null)
                {
                    RowReadyEventArgs args7 = new RowReadyEventArgs {
                        SelectionStatus = this.rowSel,
                        Rows = this.Provide(ProviderPreference.Rows) as DataRow[]
                    };
                    this.RowReady(this, args7);
                }
            }
        }

        internal object Provide(ProviderPreference pref)
        {
            int num = 1;
            List<DataRow> list2 = new List<DataRow>();
            DataTable table = null;
            if (this.rows != null)
            {
                foreach (DataRow row2 in this.rows)
                {
                    list2.Add(row2);
                }
            }
            if (this.list != null)
            {
                foreach (DataRow row3 in this.list.Rows)
                {
                    list2.Add(row3);
                }
            }
            if (pref == ProviderPreference.FilterString)
            {
                string str = string.Empty;
                SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
                if (this.filterValues != null)
                {
                    foreach (KeyValuePair<string, string> pair in this.filterValues)
                    {
                        dictionary[pair.Key] = pair.Value;
                    }
                }
                if ((this.cellValue != null) && (this.cellFieldName != null))
                {
                    dictionary[this.cellFieldName] = this.cellValue.ToString();
                }
                if ((this.paramInProps != null) && (this.paramInValues != null))
                {
                    for (int i = 0; i < Math.Min(this.paramInProps.Length, this.paramInValues.Length); i++)
                    {
                        dictionary[this.paramInProps[i].ParameterName] = this.paramInValues[i];
                    }
                }
                if ((this.paramOutProps != null) && (this.paramOutValues != null))
                {
                    for (int j = 0; j < Math.Min(this.paramOutProps.Length, this.paramOutValues.Length); j++)
                    {
                        dictionary[this.paramOutProps[j].ParameterName] = this.paramOutValues[j];
                    }
                }
                if (list2.Count > 0)
                {
                    foreach (DataColumn column in list2[0].Table.Columns)
                    {
                        dictionary[column.ColumnName] = list2[0][column] + string.Empty;
                    }
                }
                foreach (KeyValuePair<string, string> pair2 in dictionary)
                {
                    string introduced48 = pair2.Key.Replace("&", "%26").Replace("=", "%3D");
                    str = str + string.Format("FilterField{0}={1}&FilterValue{0}={2}&", num++, introduced48, pair2.Value.Replace("&", "%26").Replace("=", "%3D"));
                }
                if (!string.IsNullOrEmpty(str))
                {
                    return str.Substring(0, str.Length - 1);
                }
                return string.Empty;
            }
            if ((pref == ProviderPreference.Rows) || (pref == ProviderPreference.Table))
            {
                table = new DataTable();
                foreach (KeyValuePair<string, string> pair3 in this.fields)
                {
                    if (!table.Columns.Contains(pair3.Key))
                    {
                        table.Columns.Add(pair3.Key);
                    }
                }
                if ((this.cellFieldName != null) && !table.Columns.Contains(this.cellFieldName))
                {
                    table.Columns.Add(this.cellFieldName);
                }
                foreach (DataRow row4 in list2)
                {
                    table.Rows.Add(row4);
                }
                DataRow row = table.NewRow();
                if ((this.cellValue != null) && (this.cellFieldName != null))
                {
                    row[this.cellFieldName] = this.cellValue;
                }
                if (this.filterValues != null)
                {
                    foreach (KeyValuePair<string, string> pair4 in this.filterValues)
                    {
                        row[pair4.Key] = pair4.Value;
                    }
                }
                if ((this.paramInProps != null) && (this.paramInValues != null))
                {
                    for (int k = 0; k < Math.Min(this.paramInProps.Length, this.paramInValues.Length); k++)
                    {
                        row[this.paramInProps[k].ParameterName] = this.paramInValues[k];
                    }
                }
                if ((this.paramOutProps != null) && (this.paramOutValues != null))
                {
                    for (int m = 0; m < Math.Min(this.paramOutProps.Length, this.paramOutValues.Length); m++)
                    {
                        row[this.paramOutProps[m].ParameterName] = this.paramOutValues[m];
                    }
                }
                table.Rows.Add(row);
                if (pref == ProviderPreference.Rows)
                {
                    DataRow[] array = new DataRow[table.Rows.Count];
                    table.Rows.CopyTo(array, 0);
                    return array;
                }
                return table;
            }
            if (pref == ProviderPreference.SingleValue)
            {
                if (this.cellValue != null)
                {
                    return this.cellValue;
                }
                if (this.paramInValues != null)
                {
                    string[] paramInValues = this.paramInValues;
                    int index = 0;
                    while (index < paramInValues.Length)
                    {
                        return paramInValues[index];
                    }
                }
                if (this.paramOutValues != null)
                {
                    string[] paramOutValues = this.paramOutValues;
                    int num8 = 0;
                    while (num8 < paramOutValues.Length)
                    {
                        return paramOutValues[num8];
                    }
                }
                if (this.filterValues.Count > 0)
                {
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator enumerator8 = this.filterValues.Values.GetEnumerator())
                    {
                        while (enumerator8.MoveNext())
                        {
                            return enumerator8.Current;
                        }
                    }
                }
                foreach (DataRow row5 in list2)
                {
                    foreach (DataColumn column2 in row5.Table.Columns)
                    {
                        return row5[column2];
                    }
                }
            }
            else if (pref == ProviderPreference.Values)
            {
                List<string> list = new List<string>();
                if (this.paramInValues != null)
                {
                    list.AddRange(this.paramInValues);
                }
                if (this.paramOutValues != null)
                {
                    list.AddRange(this.paramOutValues);
                }
                if (this.cellValue != null)
                {
                    list.Add(this.cellValue.ToString());
                }
                if (this.filterValues.Count > 0)
                {
                    list.AddRange(this.filterValues.Values);
                }
                if (list2.Count > 0)
                {
                    foreach (DataColumn column3 in list2[0].Table.Columns)
                    {
                        list.Add(list2[0][column3] + string.Empty);
                    }
                }
                return list.ToArray();
            }
            return null;
        }

        internal Duo<string[], string[]> Fields
        {
            get
            {
                string[] array = new string[this.fields.Count];
                string[] strArray2 = new string[this.fields.Count];
                this.fields.Keys.CopyTo(array, 0);
                this.fields.Values.CopyTo(strArray2, 0);
                return new Duo<string[], string[]>(array, strArray2);
            }
            set
            {
                if (((value != null) && (value.Value1 != null)) && (value.Value2 != null))
                {
                    for (int i = 0; i < value.Value1.Length; i++)
                    {
                        this.fields[value.Value1[i]] = (value.Value2.Length > i) ? value.Value2[i] : ((string) value.Value1[i]);
                    }
                }
            }
        }

        public class ConnPoint : ITransformableFilterValues, IWebPartField, IWebPartParameters, IWebPartRow, IWebPartTable
        {
            public readonly roxority_AdapterWebPart Part;

            public ConnPoint(roxority_AdapterWebPart part)
            {
                this.Part = part;
            }

            internal PropertyDescriptorCollection GetSchema(string name)
            {
                PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(new PropertyDescriptor[0], false);
                if (this.Part.cellFieldName != null)
                {
                    descriptors.Add(new CustomPropertyDescriptor(this.Part.cellFieldName, new Attribute[0], null, null));
                }
                foreach (KeyValuePair<string, string> pair in this.Part.fields)
                {
                    descriptors.Add(new CustomPropertyDescriptor(pair.Key, new Attribute[0], null, null));
                }
                if (descriptors.Count == 0)
                {
                    descriptors.Add(new CustomPropertyDescriptor(name, new Attribute[0], null, null));
                }
                return descriptors;
            }

            void IWebPartField.GetFieldValue(FieldCallback callback)
            {
                callback(this.Part.Provide(roxority_AdapterWebPart.ProviderPreference.SingleValue));
            }

            void IWebPartParameters.GetParametersData(ParametersCallback callback)
            {
                DataTable table = this.Part.Provide(roxority_AdapterWebPart.ProviderPreference.Table) as DataTable;
                Hashtable parametersData = new Hashtable();
                if (((table != null) && (table.Rows.Count > 0)) && (table.Columns.Count > 0))
                {
                    parametersData = new Hashtable();
                    foreach (DataColumn column in table.Columns)
                    {
                        parametersData[column.ColumnName] = table.Rows[0][column];
                    }
                    callback(parametersData);
                }
            }

            void IWebPartParameters.SetConsumerSchema(PropertyDescriptorCollection schema)
            {
            }

            void IWebPartRow.GetRowData(RowCallback callback)
            {
                DataTable table = this.Part.Provide(roxority_AdapterWebPart.ProviderPreference.Table) as DataTable;
                if ((table != null) && (table.Rows.Count > 0))
                {
                    callback(table.Rows[0]);
                }
            }

            void IWebPartTable.GetTableData(TableCallback callback)
            {
                DataTable table = this.Part.Provide(roxority_AdapterWebPart.ProviderPreference.Table) as DataTable;
                if ((table != null) && (table.Rows.Count > 0))
                {
                    callback(table.Rows);
                }
            }

            bool ITransformableFilterValues.AllowAllValue
            {
                get
                {
                    return true;
                }
            }

            bool ITransformableFilterValues.AllowEmptyValue
            {
                get
                {
                    return true;
                }
            }

            bool ITransformableFilterValues.AllowMultipleValues
            {
                get
                {
                    return true;
                }
            }

            string ITransformableFilterValues.ParameterName
            {
                get
                {
                    if (this.Part.cellFieldName != null)
                    {
                        return this.Part.cellFieldName;
                    }
                    foreach (KeyValuePair<string, string> pair in this.Part.fields)
                    {
                        return pair.Key;
                    }
                    return "ITransformableFilterValues";
                }
            }

            ReadOnlyCollection<string> ITransformableFilterValues.ParameterValues
            {
                get
                {
                    string[] strArray = this.Part.Provide(roxority_AdapterWebPart.ProviderPreference.Values) as string[];
                    return new ReadOnlyCollection<string>(new List<string>((strArray == null) ? ((IEnumerable<string>) new string[0]) : ((IEnumerable<string>) strArray)));
                }
            }

            PropertyDescriptor IWebPartField.Schema
            {
                get
                {
                    if (this.Part.cellFieldName != null)
                    {
                        return new CustomPropertyDescriptor(this.Part.cellFieldName, new Attribute[0], null, null);
                    }
                    foreach (KeyValuePair<string, string> pair in this.Part.fields)
                    {
                        return new CustomPropertyDescriptor(pair.Key, new Attribute[0], null, null);
                    }
                    return new CustomPropertyDescriptor("IWebPartField", new Attribute[0], null, null);
                }
            }

            PropertyDescriptorCollection IWebPartParameters.Schema
            {
                get
                {
                    return this.GetSchema("IWebPartParameters");
                }
            }

            PropertyDescriptorCollection IWebPartRow.Schema
            {
                get
                {
                    return this.GetSchema("IWebPartRow");
                }
            }

            PropertyDescriptorCollection IWebPartTable.Schema
            {
                get
                {
                    return this.GetSchema("IWebPartTable");
                }
            }
        }

        public enum ProviderPreference
        {
            FilterString,
            Rows,
            SingleValue,
            Table,
            Values
        }
    }
}

