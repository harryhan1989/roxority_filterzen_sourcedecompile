using System;
using System.Data;
using BLL;
using Nandasoft;
using WebUI;

namespace WebManage
{
    public partial class TransferWeb : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        private long UserID
        {
            get
            {
                if (ViewState["userid"] == null)
                {
                    return 0;
                }
                else
                {
                    return NDConvert.ToInt64(ViewState["userid"].ToString());
                }
            }
            set
            {
                ViewState["userid"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGetUrlPara(); //获取url参数

                TransferPage();
            }
        }

        #region 获取url参数
        /// <summary>
        /// 获取url参数
        /// </summary>
        public void LoadGetUrlPara()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                UserID = NDConvert.ToInt64(Request.QueryString["userid"]);
            }
        }
        #endregion

        public void TransferPage()
        {
            try
            {
                DataTable dt = new UserQuery().GetUserByUserID(NDConvert.ToString(UserID));

                if (dt.Rows.Count > 0)
                {
                    UserEntity entity = new UserEntity(NDConvert.ToInt64(dt.Rows[0]["UserID"].ToString()));

                    if (entity.Status == 2) //锁定
                    {
                        PageHelper.ShowExceptionMessage("此用户已经被锁定，无法登陆，请与管理员联系！");
                        return;
                    }

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
                }
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex);
            }
        }
    }
}
