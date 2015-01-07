using CreateItem;
using LoginClass;
using System;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SaveItemModify : Page, IRequiresSessionState
    {
        public long IID;
        public long  SID;
        public string sItemHTML = "";
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.SID = Convert.ToInt64(base.Request.Form["SID"]);
            this.IID = Convert.ToInt64(base.Request.Form["IID"]);
            CreateItem.CreateItem item = new CreateItem.CreateItem();
            string[] arrFormsData = item.getFormsData();
            item.createItem(arrFormsData, this.SID, this.UID, this.IID);
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
    }
}
