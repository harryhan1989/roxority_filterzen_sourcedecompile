using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebUI
{
    public partial class refreshMessage : WebUI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int interval = new Nandasoft.BaseModule.MessageRule().GetMessageAwokeInterval(SessionState.EmployeeID);

                timerRefresh.Interval = interval == 0 ? 300000 : interval * 60000;
                btnRefresh_Click(null, null);
            }
        }

        /// <summary>
        /// 刷新新的在线消息数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                long employeeID = WebUI.SessionState.EmployeeID;
                int newMsgConut = new Nandasoft.BaseModule.MessageRule().GetMyReceivedMessageList(employeeID, 1, 100).Tables[1].Rows.Count;
                PageHelper.ShowMessage("Message count=" + newMsgConut);
            }
            catch //(Exception ex)
            { 
            
            }
        }

        /// <summary>
        /// 触发定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session == null ||
              System.Web.HttpContext.Current.Session.Count < 1)
            {
                timerRefresh.Enabled = false;
            }
            else
            {
                btnRefresh_Click(btnRefresh, new EventArgs());
            }
        }
    }
}
