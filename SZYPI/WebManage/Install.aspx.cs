using System;
using System.Configuration;
using System.Web.Management;
using Nandasoft;

namespace WebManage
{
    public partial class Install : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //是否显示安装或卸载按钮
                if (ConfigurationManager.AppSettings["InstallSessionState"].ToString() == "1")
                {
                    div1.Visible = true;
                }
                else
                {
                    div1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                txtErrorInfo.Text = ex.Message;
            }
        }

        //安装
        protected void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                SqlServices.InstallSessionState(FrameworkConfig.ConnectionInfo.DataSource, FrameworkConfig.ConnectionInfo.UserID, FrameworkConfig.ConnectionInfo.Password, FrameworkConfig.ConnectionInfo.Database, SessionStateType.Custom);
            }
            catch (Exception ex)
            {
                txtErrorInfo.Text = ex.Message;
            }
        }

        //卸载
        protected void btnUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                SqlServices.UninstallSessionState(FrameworkConfig.ConnectionInfo.DataSource, FrameworkConfig.ConnectionInfo.UserID, FrameworkConfig.ConnectionInfo.Password, FrameworkConfig.ConnectionInfo.Database, SessionStateType.Custom);

            }
            catch (Exception ex)
            {
                txtErrorInfo.Text = ex.Message;
            }
        }
    }
}
