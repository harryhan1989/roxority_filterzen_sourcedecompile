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
    public partial class UserDetail : BasePage
    {
        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetUrlParameter();
                InitPage();
            }
        }

        /// <summary>
        /// ����
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
                switch (this.CurOperation)
                {
                    case (int)OperationEnum.UPDATE:
                        Update();
                        PageHelper.ShowMessage("���³ɹ���");
                        break;
                    case (int)OperationEnum.INSERT:
                        Save();
                        Session["OperationEnum"] = (int)OperationEnum.INSERT;
                        PageHelper.ShowMessage("����ɹ���");
                        break;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('UserDetailLY','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('UserDetailLY','btnRefresh');", true);
        }

        /// <summary>
        /// ���URL����
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] != "")
            {
                this.CurOperation = NDConvert.ToInt32(Request.QueryString["Operation"].ToString());
            }

            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.UserID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());
            }

            if (Request.QueryString["OUID"] != null && Request.QueryString["OUID"] != "")
            {
                this.OUID = NDConvert.ToInt64(Request.QueryString["OUID"].ToString());
            }
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            BindOUList();
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                LoadData();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void LoadData()
        {
            UserEntity entity = new UserEntity(this.UserID);
            txtUserName.Text = entity.UserName;
            CommonHelp.BinddrpCurValue(drpOUList, this.OUID.ToString());
            txtAccount.Text = entity.Account;                     
        }

        /// <summary>
        /// ���������
        /// </summary>
        private void BindOUList()
        {
            DataTable dt = new OUQuery().GetOU();
            NDHelperWebControl.LoadDropDownList(drpOUList, dt, "OUName", "OUID", new ListItem("��ѡ��", "0"));

            CommonHelp.BinddrpCurValue(drpOUList, this.OUID.ToString());
            drpOUList.Enabled = false;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            UserEntity entity = new UserEntity();
            entity.OUID = this.OUID;
            entity.UserName = txtUserName.Text;
            entity.MobilePhone = txtMobilePhone.Text;
            entity.OfficePhone = txtOfficePhone.Text;
            entity.Email = txtEmail.Text;
            entity.Sex = NDConvert.ToInt16(rdolstSex.SelectedValue);

            entity.Account = txtAccount.Text.Trim();
            entity.Password = Security.EncryptQueryString(txtPassword1.Text.Trim());
            entity.AddDate = DateTime.Now;
            entity.Status = 1;
            entity.UserType = (int)CommonEnum.UserType.InnerUser;
            entity.SortIndex = UserQuery.GetMaxSortIndex();
            new UserRule().Add(entity);          
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Update()
        {
            UserEntity entity = new UserEntity(this.UserID);
            entity.UserName = txtUserName.Text;
            
            entity.Account = txtAccount.Text.Trim();
            if (!string.IsNullOrEmpty(txtPassword1.Text.Trim()))
            {
                entity.Password = Security.EncryptQueryString(txtPassword1.Text.Trim());
            }
            entity.UpdateDate = DateTime.Now;
            new UserRule().Update(entity);           
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                hidInfo.Value = "������������";
                return false;
            }

            if (drpOUList.SelectedIndex == 0)
            {
                hidInfo.Value = "��ѡ���ţ�";
                return false;
            }
            if (string.IsNullOrEmpty(txtAccount.Text.Trim()))
            {
                hidInfo.Value = "�������û��ʺţ�";
                return false;
            }

            if (!UserQuery.CheckAccount(txtAccount.Text.Trim(), this.UserID, this.CurOperation))
            {
                hidInfo.Value = "���ʺ��Ѿ�ʹ�ã������������ʺţ�";
                return false;
            }

            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                if (!string.IsNullOrEmpty(txtPassword1.Text.Trim()) || !string.IsNullOrEmpty(txtPassword2.Text.Trim()))
                {
                    if(!string.IsNullOrEmpty(txtPassword1.Text.Trim()))
                    {
                        string[] str = txtPassword1.Text.Split(' ');
                        if (str.Length > 1)
                        {
                            hidInfo.Value = "�����в��ܴ��ո�";
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtPassword2.Text.Trim()))
                    {
                        string[] str = txtPassword2.Text.Split(' ');
                        if (str.Length > 1)
                        {
                            hidInfo.Value = "�ظ������в��ܴ��ո�";
                            return false;
                        }
                    }

                    if (txtPassword1.Text.Trim() != txtPassword2.Text.Trim())
                    {
                        hidInfo.Value = "�������벻һ�£����������룡";
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword1.Text.Trim()))
                {
                    hidInfo.Value = "���������룡";
                    return false;
                }
                else
                {
                    string[] str = txtPassword1.Text.Split(' ');
                    if (str.Length > 1)
                    {
                        hidInfo.Value = "�����в��ܴ��ո�";
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtPassword2.Text.Trim()))
                {
                    hidInfo.Value = "�������ظ�ȷ�����룡";
                    return false;
                }
                else
                {
                    string[] str = txtPassword2.Text.Split(' ');
                    if (str.Length > 1)
                    {
                        hidInfo.Value = "�ظ������в��ܴ��ո�";
                        return false;
                    }
                }

                if (txtPassword1.Text.Trim() != txtPassword2.Text.Trim())
                {
                    hidInfo.Value = "�������벻һ�£����������룡";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!NDHelperDataCheck.IsEmail(txtEmail.Text.Trim()))
                {
                    hidInfo.Value = "�����ʼ���ʽ����";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtMobilePhone.Text.Trim()))
            {
                if (!CommonHelp.PhoneNumberCheck(txtMobilePhone.Text.Trim()))
                {
                    hidInfo.Value = "�ֻ������ʽ����";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtMobilePhone.Text.Trim()))
            {
                if (!UserQuery.CheckMobilePhone(txtMobilePhone.Text.Trim(), SessionState.UserID, (int)CommonEnum.OperationEnum.UPDATE))
                {
                    hidInfo.Value = "���ֻ������Ѿ�ʹ�ã������������ֻ����룡";
                    return false;
                }
            }
            return true;
        }

        #region ����
        /// <summary>
        /// ҳ��״̬����
        /// </summary>
        private int CurOperation
        {
            set { ViewState["Operation"] = value.ToString(); }
            get
            {
                if (ViewState["Operation"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Operation"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        private long UserID
        {
            set { ViewState["EmployeeID"] = value.ToString(); }
            get
            {
                if (ViewState["EmployeeID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["EmployeeID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        private long OUID
        {
            set { ViewState["OUID"] = value.ToString(); }
            get
            {
                if (ViewState["OUID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["OUID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }
        #endregion
    }
}
