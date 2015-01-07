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
    public partial class UserInfo : BasePage
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
                InitPage();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            LoadData();
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
                Update();
                PageHelper.ShowMessage("更新成功");

            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            UserEntity entity = new UserEntity(SessionState.UserID);
            entity.UserName = txtUserName.Text.Trim();
            entity.Sex = NDConvert.ToInt32(rdolstSex.SelectedValue);
            entity.Email = txtEmail.Text.Trim();
            entity.OfficePhone = txtOfficePhone.Text.Trim();
            entity.MobilePhone = txtMobilePhone.Text.Trim();
            entity.UpdateDate = DateTime.Now;
            new UserRule().Update(entity);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {           
            UserEntity entity = new UserEntity(SessionState.UserID);
            txtUserName.Text = entity.UserName;
            txtAccount.Text = entity.Account;
            txtOUName.Text = new OUEntity(entity.OUID).OUName;
            CommonHelp.BindrdoCurValue(rdolstSex, entity.Sex.ToString());
            txtEmail.Text = entity.Email;
            txtOfficePhone.Text = entity.OfficePhone;
            txtMobilePhone.Text = entity.MobilePhone;
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
    }
}
