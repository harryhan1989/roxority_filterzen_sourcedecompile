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
        /// 加载页面
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
        /// 保存
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
                        PageHelper.ShowMessage("更新成功！");
                        break;
                    case (int)OperationEnum.INSERT:
                        Save();
                        Session["OperationEnum"] = (int)OperationEnum.INSERT;
                        PageHelper.ShowMessage("保存成功！");
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('UserDetailLY','btnRefresh');", true);
        }

        /// <summary>
        /// 获得URL参数
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
        /// 初始化页面
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
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            UserEntity entity = new UserEntity(this.UserID);
            txtUserName.Text = entity.UserName;
            CommonHelp.BinddrpCurValue(drpOUList, this.OUID.ToString());
            txtAccount.Text = entity.Account;                     
        }

        /// <summary>
        /// 邦定父级部门
        /// </summary>
        private void BindOUList()
        {
            DataTable dt = new OUQuery().GetOU();
            NDHelperWebControl.LoadDropDownList(drpOUList, dt, "OUName", "OUID", new ListItem("请选择", "0"));

            CommonHelp.BinddrpCurValue(drpOUList, this.OUID.ToString());
            drpOUList.Enabled = false;
        }

        /// <summary>
        /// 保存
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
        /// 更新
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
        /// 检测数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                hidInfo.Value = "请输入姓名！";
                return false;
            }

            if (drpOUList.SelectedIndex == 0)
            {
                hidInfo.Value = "请选择部门！";
                return false;
            }
            if (string.IsNullOrEmpty(txtAccount.Text.Trim()))
            {
                hidInfo.Value = "请输入用户帐号！";
                return false;
            }

            if (!UserQuery.CheckAccount(txtAccount.Text.Trim(), this.UserID, this.CurOperation))
            {
                hidInfo.Value = "该帐号已经使用，请重新输入帐号！";
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
                            hidInfo.Value = "密码中不能带空格！";
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtPassword2.Text.Trim()))
                    {
                        string[] str = txtPassword2.Text.Split(' ');
                        if (str.Length > 1)
                        {
                            hidInfo.Value = "重复密码中不能带空格！";
                            return false;
                        }
                    }

                    if (txtPassword1.Text.Trim() != txtPassword2.Text.Trim())
                    {
                        hidInfo.Value = "密码输入不一致，请重新输入！";
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword1.Text.Trim()))
                {
                    hidInfo.Value = "请输入密码！";
                    return false;
                }
                else
                {
                    string[] str = txtPassword1.Text.Split(' ');
                    if (str.Length > 1)
                    {
                        hidInfo.Value = "密码中不能带空格！";
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtPassword2.Text.Trim()))
                {
                    hidInfo.Value = "请输入重复确认密码！";
                    return false;
                }
                else
                {
                    string[] str = txtPassword2.Text.Split(' ');
                    if (str.Length > 1)
                    {
                        hidInfo.Value = "重复密码中不能带空格！";
                        return false;
                    }
                }

                if (txtPassword1.Text.Trim() != txtPassword2.Text.Trim())
                {
                    hidInfo.Value = "密码输入不一致，请重新输入！";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!NDHelperDataCheck.IsEmail(txtEmail.Text.Trim()))
                {
                    hidInfo.Value = "电子邮件格式错误！";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtMobilePhone.Text.Trim()))
            {
                if (!CommonHelp.PhoneNumberCheck(txtMobilePhone.Text.Trim()))
                {
                    hidInfo.Value = "手机号码格式错误！";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtMobilePhone.Text.Trim()))
            {
                if (!UserQuery.CheckMobilePhone(txtMobilePhone.Text.Trim(), SessionState.UserID, (int)CommonEnum.OperationEnum.UPDATE))
                {
                    hidInfo.Value = "该手机号码已经使用，请重新输入手机号码！";
                    return false;
                }
            }
            return true;
        }

        #region 属性
        /// <summary>
        /// 页面状态属性
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
        /// 部门ID
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
        /// 部门ID
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
