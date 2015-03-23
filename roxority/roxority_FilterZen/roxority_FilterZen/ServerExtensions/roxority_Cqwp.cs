namespace roxority_FilterZen.ServerExtensions
{
    using Microsoft.SharePoint.Publishing.WebControls;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using roxority_FilterZen;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Web.UI.WebControls.WebParts;

    [Guid("40da97dd-4f0c-4c54-9bd8-08c799ee0e56")]
    public class roxority_Cqwp : ContentByQueryWebPart, IFilterConsumer
    {
        public event FilterConsumerInitEventHandler FilterConsumerInit;

        public roxority_Cqwp()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        public override ConnectionRunAt CanRunAt()
        {
            return ConnectionRunAt.Server;
        }

        public void ClearFilter(object sender, EventArgs e)
        {
        }

        protected override void CreateChildControls()
        {
            if (!string.IsNullOrEmpty(this.CustomQuery))
            {
                base.QueryOverride = this.CustomQuery;
            }
            base.CreateChildControls();
            base.QueryOverride = string.Empty;
            base.CommonViewFields = string.Empty;
        }

        public override void EnsureInterfaces()
        {
            base.EnsureInterfaces();
            base.RegisterInterface("roxContentQuery", "IFilterConsumer", 1, ConnectionRunAt.Server, this, "", "menu label", "menu desc", true);
        }

        public void NoFilter(object sender, EventArgs e)
        {
        }

        public override void PartCommunicationConnect(string interfaceName, Microsoft.SharePoint.WebPartPages.WebPart connectedPart, string connectedInterfaceName, ConnectionRunAt runAt)
        {
            roxority_FilterWebPart part = connectedPart as roxority_FilterWebPart;
            if ((part != null) && !part.CamlFilters)
            {
                part.additionalWarningsErrors.Add(part["CqwpCaml", new object[0]]);
            }
        }

        public override void PartCommunicationInit()
        {
        }

        public override void PartCommunicationMain()
        {
        }

        public void SetFilter(object sender, SetFilterEventArgs e)
        {
        }

        public string CustomQuery { get; set; }
    }
}

