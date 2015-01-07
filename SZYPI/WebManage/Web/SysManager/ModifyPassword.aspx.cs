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
using WebUI;
using Nandasoft;
using BLL;
using Nandasoft.Helper;

namespace WebManage.Web.SystemManager
{
    public partial class ModifyPassword : BasePage
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }

            try
            {
                UserEntity entity = new UserEntity(SessionState.UserID);
                entity.Password = Security.EncryptQueryString(txtPassword1.Text.Trim());
                new UserRule().Update(entity);
                txtPassword.Text = "";          
                txtPassword1.Text = "";
                txtPassword2.Text = "";

                PageHelper.ShowMessage("修改成功！");
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        private bool CheckData()
        {
            UserEntity entity = new UserEntity(SessionState.UserID);

            if (Security.EncryptQueryString(txtPassword.Text.Trim()) != entity.Password)
            {
                hidInfo.Value = "原密码错误请重新输入！";
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword1.Text.Trim()) || string.IsNullOrEmpty(txtPassword2.Text.Trim()))
            {
                hidInfo.Value = "新密码或重复密码不能为空！";
                return false;
            }

            if (txtPassword1.Text != txtPassword2.Text)
            {
                hidInfo.Value = "密码输入不一致，请重新输入！";
                return false;
            }

            return true;
        }
    }
}
