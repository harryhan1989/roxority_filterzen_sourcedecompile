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
using Nandasoft;

namespace WebUI
{
    public partial class Refresh : WebUI.BasePage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int interval = new Nandasoft.BaseModule.MessageRule().GetMessageAwokeInterval(SessionState.EmployeeID);

                timerRefresh.Interval = interval == 0 ? 300000 : interval * 60000;

                btnRefreshOnline_Click(null, null);//标记为在线
                btnRefresh_Click(null, null);//得到新消息
            }
        }

        /// <summary>
        /// 得到新消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                List<Nandasoft.BaseModule.OnLineMessage> content = new Nandasoft.BaseModule.MessageRule().GetOnlineMessage(WebUI.SessionState.EmployeeID, 5);

                string titles = string.Empty;
                string links = string.Empty;

                foreach (Nandasoft.BaseModule.OnLineMessage message in content)
                {
                    titles += message.Title + ",";
                    links += message.Link + ",";
                }
                if (titles.Length > 0) titles = titles.Remove(titles.Length - 1, 1);
                if (links.Length > 0) links = links.Remove(links.Length - 1, 1);

                if (!string.IsNullOrEmpty(titles))
                {
                    PageHelper.ShowPopMessage(titles, links);
                }
            }
            catch //(Exception ex)
            {
                ////写入日志
                //new Nandasoft.BaseModule.LogRule().WriteLog(
                //    SessionState.UserAccount,
                //    SessionState.UserName,
                //    "得到用户最新消息时发生错误：" + ex.Message,
                //    PageHelper.GetMachineIP(),
                //    PageHelper.GetMachineName(),
                //    (int)Nandasoft.BaseModule.LogTypeEnum.错误日志);
            }
        }

        /// <summary>
        /// 刷新在线用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefreshOnline_Click(object sender, EventArgs e)
        {
            try
            {
                new Nandasoft.BaseModule.UserRule().SetUserOnline(SessionState.UserID);
            }
            catch// (Exception ex)
            {
                ////写入日志
                //new Nandasoft.BaseModule.LogRule().WriteLog(
                //    SessionState.UserAccount,
                //    SessionState.UserName,
                //    "刷新在线用户时发生错误：" + ex.Message,
                //    PageHelper.GetMachineIP(),
                //    PageHelper.GetMachineName(),
                //    (int)Nandasoft.BaseModule.LogTypeEnum.错误日志);
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
                btnRefresh_Click(null, null);
            }
        }

        /// <summary>
        /// 触发定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void timerRefreshOnline_Tick(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session == null ||
               System.Web.HttpContext.Current.Session.Count < 1)
            {
                timerRefreshOnline.Enabled = false; 
            }
            else
            {
                btnRefreshOnline_Click(null, null);
            }
        }
    }
}
