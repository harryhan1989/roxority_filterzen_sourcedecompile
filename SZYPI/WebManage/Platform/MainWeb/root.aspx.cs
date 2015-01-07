using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Nandasoft;
using BLL;

namespace WebUI
{
    public partial class root : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = System.Configuration.ConfigurationManager.AppSettings["systemName"];
            InitPage();
        }


        private void InitPage()
        {
            //hidInfo.Value = "";
            //string str = "<ul>";
            //DataTable dt = new RCPAuditQuery().RCPAuditOpen(SessionState.UserID);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        long RCPForecastID = NDConvert.ToInt64(dt.Rows[i]["RCPForecastID"].ToString());
            //        int AuditType = NDConvert.ToInt32(dt.Rows[i]["AuditType"].ToString());
            //        str += "<li>";
            //        if (AuditType == 1)
            //        {
            //            str += "<a style='cursor:hand; color=#FF8282' onclick='showModalWindow(\"ReceptionForecastEditLY\",\"客情报告\",900,600,\"../../Web/ReceptionManage/ReceptionForecastEdit.aspx?Operation=2&ID=" + RCPForecastID + "\")'>" + dt.Rows[i]["AuditTypeName"].ToString() + ":" + dt.Rows[i]["RCPName"].ToString() + "</a>";
            //        }
            //        else if (AuditType == 2)
            //        {
            //            str += "<a style='cursor:hand; color=#EE82EE' onclick='showModalWindow(\"ReceptionForecastEditLY\",\"接待计划\",900,600,\"../../Web/ReceptionManage/ReceptionForecastReception.aspx?Operation=2&ID=" + RCPForecastID + "\")'>" + dt.Rows[i]["AuditTypeName"].ToString() + ":" + dt.Rows[i]["RCPName"].ToString() + "</a>";
            //        }

            //        str += "</li>";

            //    }
            //    str += "</ul>";
            //    hidInfo.Value = "1";
            //}
            //divContent.InnerHtml = str;
        }
    }
}
