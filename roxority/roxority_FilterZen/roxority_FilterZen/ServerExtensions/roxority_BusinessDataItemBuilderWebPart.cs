namespace roxority_FilterZen.ServerExtensions
{
    using Microsoft.Office.Server.ApplicationRegistry.Infrastructure;
    using Microsoft.Office.Server.ApplicationRegistry.MetadataModel;
    using Microsoft.Office.Server.ApplicationRegistry.Runtime;
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Portal.WebControls;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using roxority.SharePoint;
    using roxority_FilterZen;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls.WebParts;

    [Guid("abd6282a-2ac6-4664-adce-49e93b3b6e2a")]
    public class roxority_BusinessDataItemBuilderWebPart : WebPartBase, ICellConsumer, IEntityInstanceProvider, IRowProvider
    {
        private bool _cellConnected;
        private bool _rowConnected;
        private string[] cell;
        private string cellFieldName = "ID";
        private object cellValue;
        private Entity entity;
        private IEntityInstance entityInstance;
        private Exception regError;
        private RowProviderInitEventArgs rowArgs;
        private DataTable rowTable;
        private LobSystemInstance systemInstance;

        public event CellConsumerInitEventHandler CellConsumerInit;

        public event RowProviderInitEventHandler RowProviderInit;

        public event RowReadyEventHandler RowReady;

        public roxority_BusinessDataItemBuilderWebPart()
        {
            this.ChromeType = PartChromeType.None;
            this.Description = base["BdcWebPart_Desc", new object[0]];
        }

        public override ConnectionRunAt CanRunAt()
        {
            if (!this.CanRun)
            {
                return ConnectionRunAt.None;
            }
            return ConnectionRunAt.Server;
        }

        public void CellProviderInit(object sender, CellProviderInitEventArgs cellProviderInitArgs)
        {
            this.cell = new string[] { cellProviderInitArgs.FieldName, cellProviderInitArgs.FieldDisplayName };
        }

        public void CellReady(object sender, CellReadyEventArgs cellReadyArgs)
        {
            Converter<string, object> converter = null;
            RowReadyEventArgs e = new RowReadyEventArgs();
            Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View view = null;
            this.cellValue = cellReadyArgs.Cell;
            bool doit = base.LicEd(4) || !this.Exed;
            if (this._cellConnected && this._rowConnected)
            {
                if (this.rowTable != null)
                {
                    this.rowTable.Dispose();
                }
                this.rowTable = new DataTable();
                if (this.RowArgs.FieldList.Length > 0)
                {
                    foreach (string str in this.RowArgs.FieldList)
                    {
                        this.rowTable.Columns.Add(str);
                    }
                    if (doit)
                    {
                        try
                        {
                            view = GetView(this.entity);
                            if (this.entityInstance == null)
                            {
                                this.entityInstance = this.GetEntityInstance(view);
                            }
                        }
                        catch
                        {
                        }
                    }
                    DataRow[] rowArray = new DataRow[1];
                    if (converter == null)
                    {
                        converter = delegate(string fieldName)
                        {
                            if (!doit)
                            {
                                return this.CellValue;
                            }
                            if ((this.entityInstance != null) && (view != null))
                            {
                                try
                                {
                                    foreach (Field field in view.Fields)
                                    {
                                        if (field.Name == fieldName)
                                        {
                                            object obj2;
                                            return ((obj2 = this.entityInstance[field]) == null) ? string.Empty : obj2.ToString();
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                            return string.Empty;
                        };
                    }
                    rowArray[0] = this.rowTable.Rows.Add(new List<string>(this.RowArgs.FieldList).ConvertAll<object>(converter).ToArray());
                    e.Rows = rowArray;
                    this.OnRowReady(e);
                }
            }
        }

        public override void Dispose()
        {
            if (this.rowTable != null)
            {
                this.rowTable.Dispose();
                this.rowTable = null;
            }
            base.Dispose();
        }

        public override void EnsureInterfaces()
        {
            try
            {
                base.RegisterInterface("roxorityConsumeCell", "ICellConsumer", 1, this.CanRun ? ConnectionRunAt.Server : ConnectionRunAt.None, this, string.Empty, base["GetIDFrom", new object[0]], base["GetIDFrom", new object[0]], true);
                base.RegisterInterface("roxorityProvideRow", "IRowProvider", -1, ConnectionRunAt.Server, this, "", base["SendRowTo", new object[0]], base["SendsRowDesc", new object[0]], true);
            }
            catch (Exception exception)
            {
                this.regError = exception;
            }
        }

        public IEntityInstance GetEntityInstance(Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View desiredView)
        {
            if (!BdcClientUtilAvailable)
            {
                throw new Exception(base["BdcNotAvailable", new object[0]]);
            }
            if ((desiredView != null) && !string.Equals(desiredView.Name, GetView(this.entity).Name))
            {
                throw new NotSupportedException();
            }
            if (this.entityInstance == null)
            {
                this.entityInstance = this.entity.FindSpecific(this.IdentifierValues, this.systemInstance);
            }
            return this.entityInstance;
        }

        public string GetEntityInstanceId()
        {
            return EntityInstanceIdEncoder.EncodeEntityInstanceId(this.IdentifierValues);
        }

        public override InitEventArgs GetInitEventArgs(string interfaceName)
        {
            CellConsumerInitEventArgs args = (interfaceName == "roxorityConsumeCell") ? new CellConsumerInitEventArgs() : null;
            if (args != null)
            {
                args.FieldDisplayName = args.FieldName = this.EffectiveCellFieldName;
                return args;
            }
            if (interfaceName == "roxorityProvideRow")
            {
                return this.RowArgs;
            }
            return args;
        }

        [ConnectionProvider("BdcItem", "roxorityEntityProviderInterface", typeof(BdcConnectionProvider), AllowsMultipleConnections = true)]
        public IEntityInstanceProvider GetProvider()
        {
            return this;
        }

        public static Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View GetView(Entity entity)
        {
            return entity.GetSpecificFinderView();
        }

        protected virtual void OnCellConsumerInit(CellConsumerInitEventArgs e)
        {
            if (this.CellConsumerInit != null)
            {
                this.CellConsumerInit(this, e);
            }
        }

        protected virtual void OnRowProviderInit(RowProviderInitEventArgs e)
        {
            if (this.RowProviderInit != null)
            {
                this.RowProviderInit(this, e);
            }
        }

        protected virtual void OnRowReady(RowReadyEventArgs e)
        {
            e.SelectionStatus = "Standard";
            if (this.RowReady != null)
            {
                this.RowReady(this, e);
            }
        }

        public override void PartCommunicationConnect(string interfaceName, Microsoft.SharePoint.WebPartPages.WebPart connectedPart, string connectedInterfaceName, ConnectionRunAt runAt)
        {
            if (interfaceName == "roxorityConsumeCell")
            {
                this._cellConnected = true;
            }
            if (interfaceName == "roxorityProvideRow")
            {
                this._rowConnected = true;
            }
        }

        public override void PartCommunicationInit()
        {
            CellConsumerInitEventArgs e = new CellConsumerInitEventArgs();
            if (this.IsConnected)
            {
                e.FieldDisplayName = e.FieldName = this.EffectiveCellFieldName;
                this.OnCellConsumerInit(e);
                if (this._rowConnected)
                {
                    this.OnRowProviderInit(this.RowArgs);
                }
            }
        }

        public override void PartCommunicationMain()
        {
        }

        protected override void RenderWebPart(HtmlTextWriter output)
        {
            try
            {
                if (!ProductPage.isEnabled)
                {
                    using (SPSite site = ProductPage.GetAdminSite())
                    {
                        output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", ProductPage.GetResource("NotEnabled", new object[] { ProductPage.MergeUrlPaths(site.Url, "/_layouts/roxority_FilterZen/default.aspx?cfg=enable"), "FilterZen" }), "servicenotinstalled.gif", "noid");
                        goto Label_00D0;
                    }
                }
                if (this.CanRun && ((base.DesignMode || base.WebPartManager.DisplayMode.AllowPageDesign) || (this.Exed || !base.LicEd(4))))
                {
                    output.Write("<div><b>{0}</b> = <i>{1}</i></div>", this.Context.Server.HtmlEncode(this.EffectiveCellFieldName), this.Context.Server.HtmlEncode(ProductPage.Trim(this.CellValue, new char[0])));
                }
            Label_00D0:
                base.RenderWebPart(output);
            }
            catch
            {
            }
        }

        public void SetConsumerEntities(IEnumerator<Entity> enumerator)
        {
            using (enumerator)
            {
                if (enumerator.MoveNext())
                {
                    this.entity = enumerator.Current;
                }
            }
        }

        public void SetConsumerEntities(NamedEntityDictionary entities)
        {
            if (entities != null)
            {
                this.SetConsumerEntities(entities.Values.GetEnumerator());
            }
        }

        public void SetConsumerSystemInstance(LobSystemInstance lobSystemInstance)
        {
            this.systemInstance = lobSystemInstance;
        }

        public static bool BdcClientUtilAvailable
        {
            get
            {
                try
                {
                    roxority_FilterWebPart.BdcClientUtilType.ToString();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        protected internal override bool CanRun
        {
            get
            {
                return ((base.CanRun && BdcClientUtilAvailable) && !base.IsPreview);
            }
        }

        [Personalizable(true), WebBrowsable(false), WebPartStorage(Storage.Shared), FriendlyName("Optionally specify the cell, column, field or filter name that the connected Web Part uses to provide the BCS External Content Type / BDC Entity Instance ID to this Web Part over a non-configurable single column 'cell' connection. (Ignored if the connected Web Part provides the BCS/BDC Entity Instance ID to this Web Part via a configurable multi-column 'row' connection.)"), Category("FilterZen"), DefaultValue("ID"), Browsable(false)]
        public string CellFieldName
        {
            get
            {
                return this.cellFieldName;
            }
            set
            {
                this.cellFieldName = string.IsNullOrEmpty(value = ProductPage.Trim(value, new char[0])) ? "ID" : value;
            }
        }

        public object CellValue
        {
            get
            {
                if (!base.LicEd(4))
                {
                    return ProductPage.GetResource("Nope" + (this.Exed ? "Expired" : "Ed"), new object[] { "Cell connections", "Ultimate" });
                }
                return this.cellValue;
            }
        }

        public string EffectiveCellFieldName
        {
            get
            {
                if (((this.cell != null) && (this.cell.Length != 0)) && !string.IsNullOrEmpty(this.cell[0]))
                {
                    return this.cell[0];
                }
                return this.CellFieldName;
            }
        }

        protected internal object[] IdentifierValues
        {
            get
            {
                return new object[] { this.CellValue };
            }
        }

        public override bool IsConnected
        {
            get
            {
                if (!this._cellConnected)
                {
                    return base.IsConnected;
                }
                return true;
            }
        }

        protected internal RowProviderInitEventArgs RowArgs
        {
            get
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (this.rowArgs == null)
                {
                    this.rowArgs = new RowProviderInitEventArgs();
                    try
                    {
                        Microsoft.Office.Server.ApplicationRegistry.MetadataModel.View view;
                        if ((this.entity != null) && ((view = GetView(this.entity)) != null))
                        {
                            foreach (Field field in view.Fields)
                            {
                                list.Add(new KeyValuePair<string, string>(field.Name, field.ContainsLocalizedDisplayName ? field.LocalizedDisplayName : field.DefaultDisplayName));
                            }
                        }
                    }
                    catch
                    {
                    }
                    this.rowArgs.FieldList = list.ConvertAll<string>(value => value.Key).ToArray();
                    this.rowArgs.FieldDisplayList = list.ConvertAll<string>(value => value.Value).ToArray();
                }
                return this.rowArgs;
            }
        }

        public Entity SelectedConsumerEntity
        {
            get
            {
                return this.entity;
            }
        }

        public class BdcConnectionProvider : ConnectionProvider
        {
            public BdcConnectionProvider(MethodInfo callbackMethod, System.Type interfaceType, System.Type controlType, string displayName, string id, bool allowsMultipleConnections)
                : base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
            {
            }

            public override bool GetEnabled(Control control)
            {
                return true;
            }
        }

        public Microsoft.BusinessData.Runtime.IEntityInstance GetEntityInstance(Microsoft.BusinessData.MetadataModel.IView desiredView)
        {
            throw new NotImplementedException();
        }

        Microsoft.BusinessData.MetadataModel.IEntity IEntityInstanceProvider.SelectedConsumerEntity
        {
            get { throw new NotImplementedException(); }
        }

        public void SetConsumerEntities(IList<Microsoft.BusinessData.MetadataModel.IEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void SetConsumerSystemInstance(Microsoft.BusinessData.MetadataModel.ILobSystemInstance systemInstance)
        {
            throw new NotImplementedException();
        }
    }
}