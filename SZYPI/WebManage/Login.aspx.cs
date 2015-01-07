using System;
using System.Data;
using BLL;
using Nandasoft;
using WebUI;

namespace WebManage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAccount.Focus();
        }
        /// <summary>
        /// 登陆验证事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            //帐号
            if (txtAccount.Text.Trim() == "")
            {
                PageHelper.ShowExceptionMessage("请输入用户名！");                
                return;
            }

            //密码
            if (txtPwd.Text.Trim() == "")
            {
                PageHelper.ShowExceptionMessage("请输入密码！");
                return;
            }

            //验证码
            //if (txtCheckCode.Text.Trim() == "")
            //{
            //    PageHelper.ShowExceptionMessage("请输入验证码！");
            //    return;
            //}

            string CheckCode = "";
            //if (Session["CheckCode"] != null)
            //{
            //    CheckCode = Session["CheckCode"].ToString();
            //    Session.Remove("CheckCode");
            //}
            //else
            //{
            //    PageHelper.ShowExceptionMessage("验证码错误！");
            //    return;
            //}

            //if (CheckCode != "")
            //{
            //    if (CheckCode.ToLower() != txtCheckCode.Text.Trim().ToLower())
            //    {
            //        PageHelper.ShowExceptionMessage("验证码错误！");
            //        return;
            //    }
            //}
            //else
            //{
            //    PageHelper.ShowExceptionMessage("验证码错误！");
            //    return;
            //}


            try
            {
                string dd = Security.EncryptQueryString("123@abcd");
                DataTable dt = new UserQuery().GetUserByAccount(txtAccount.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    UserEntity entity = new UserEntity(NDConvert.ToInt64(dt.Rows[0]["UserID"].ToString()));
                    if (entity.Status == 2) //锁定
                    {
                        PageHelper.ShowExceptionMessage("此用户已经被锁定，无法登陆，请与管理员联系！");
                        return;
                    }


                    if (Security.EncryptQueryString(txtPwd.Text.Trim()).ToLower() == entity.Password.ToLower())
                    {
                        switch (entity.UserType)
                        {
                            case (int)CommonEnum.UserType.Admin:
                                SessionState.UserType = CommonEnum.UserType.Admin;
                                SessionState.IsAdmin = true;
                                break;
                            case (int)CommonEnum.UserType.InnerUser:
                                SessionState.UserType = CommonEnum.UserType.InnerUser;
                                SessionState.IsAdmin = false;
                                break;
                            default:
                                SessionState.UserType = CommonEnum.UserType.InnerUser;
                                SessionState.IsAdmin = false;
                                break;
                        }
                        SessionState.UserID = entity.UserID;
                        SessionState.UserName = entity.UserName;
                        SessionState.Account = entity.Account;
                        SessionState.OUID = entity.OUID;
                        OUEntity OUEntity = new OUEntity(entity.OUID);
                        SessionState.OUType = OUEntity.OUType;
                        SessionState.OUName = OUEntity.OUName;
                        Response.Redirect("Platform/MainWeb/root.aspx");
                        return;
                    }
                }

                PageHelper.ShowExceptionMessage("帐号或密码错误！");
                txtAccount.Focus();
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex);
            }
        }
    }
}
