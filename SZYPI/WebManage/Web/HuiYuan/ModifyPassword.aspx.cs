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
        /// ����ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// �������
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

                PageHelper.ShowMessage("�޸ĳɹ���");
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        private bool CheckData()
        {
            HuiYuanEntity entity = new HuiYuanEntity(Session["UserGuid"].ToString());

            if (txtPassword.Text.Trim() != entity.LoginPWD)
            {
                hidInfo.Value = "ԭ����������������룡";
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword1.Text.Trim()) || string.IsNullOrEmpty(txtPassword2.Text.Trim()))
            {
                hidInfo.Value = "��������ظ����벻��Ϊ�գ�";
                return false;
            }

            if (txtPassword1.Text != txtPassword2.Text)
            {
                hidInfo.Value = "�������벻һ�£����������룡";
                return false;
            }

            return true;
        }
    }
}
