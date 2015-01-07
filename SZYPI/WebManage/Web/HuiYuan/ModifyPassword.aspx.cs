using System;
using BLL;
using BLL.Entity;
using BLL.Rule;
using WebUI;

namespace WebManage.Web.HuiYuan
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
                HuiYuanEntity entity = new HuiYuanEntity(Session["UserGuid"].ToString());
                entity.LoginPWD = txtPassword1.Text.Trim();
                new HuiYuanRule().Update(entity);
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
            HuiYuanEntity entity = new HuiYuanEntity(Session["UserGuid"].ToString());

            if (txtPassword.Text.Trim() != entity.LoginPWD)
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
