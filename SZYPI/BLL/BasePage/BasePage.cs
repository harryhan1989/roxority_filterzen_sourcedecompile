using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL;

namespace WebUI
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class BasePage : _BasePage
    {
        /// <summary>
        /// 验证用户是否有权限操作表单功能
        /// </summary>
        /// <param name="EmployeID"></param>
        /// <param name="MenuID"></param>
        /// <param name="toolbar"></param>
        public void CheckUserRight(long UserID, long MenuID, Nandasoft.WebControls.NDToolbar toolbar, Button btnQuery)
        {
            if (SessionState.IsAdmin == false)
            {
                if (toolbar != null)
                {
                    for (int i = 0; i < toolbar.Items.Count; i++)
                    {                       
                        if (MenuFunctionRightQuery.CheckRightFunction1(UserID, MenuID, toolbar.Items[i].Value))
                        {
                            toolbar.Items[i].Enabled = true;
                        }
                        else
                        {
                            toolbar.Items[i].Enabled = false;
                        }                        
                    }
                }
                if (btnQuery != null)
                {
                    if (MenuFunctionRightQuery.CheckRightFunction1(UserID, MenuID, "Query"))
                    {
                        btnQuery.Enabled = true;
                    }
                    else
                    {
                        btnQuery.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 验证用户是否有权限操作表单功能
        /// </summary>
        /// <param name="EmployeID"></param>
        /// <param name="MenuID"></param>
        /// <param name="toolbar"></param>
        public void CheckUserRight(long UserID, long MenuID, Nandasoft.WebControls.NDToolbar toolbar)
        {
            if (SessionState.IsAdmin == false)
            {
                for (int i = 0; i < toolbar.Items.Count; i++)
                {
                    if (MenuFunctionRightQuery.CheckRightFunction1(UserID, MenuID, toolbar.Items[i].Value))
                    {
                        toolbar.Items[i].Enabled = true;
                    }
                    else
                    {
                        toolbar.Items[i].Enabled = false;
                    }
                }                
            }
        }

       
    }
}
