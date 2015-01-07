using System;
using System.Data;
using WebUI;
using BLL.Entity;

namespace WebManage
{
    /// <summary>
    /// 会员登录功能
    /// 作者：姚东
    /// 时间：20100925
    /// </summary>
    public partial class HuiYuanLogin : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    LoadData();
            //}
        }

        /// <summary>
        /// 数据加载
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        private void LoadData()
        {
            BLL.Query.IndexQuery query = new BLL.Query.IndexQuery();

            //会员数量
            lblHuiYuanAmount.Text = query.GetHuiYuanAmount().ToString();

            //问卷数量
            lblSurveyAmount.Text = query.GetSurveyAmount().ToString();

            //答卷数量
            lblAnswers.Text = query.GetAnswerAmount().ToString();

            //会员登录
            if (Session["UserGUID"] != null)
            {
                //已经登录的，则自动登录
                divUser.Visible = true;
                divLogin.Visible = false;

                DataTable dt = new BLL.Query.HuiYuanInfoQuery().GetInfoHuiYuan(Session["UserGUID"].ToString());

                if (dt.Rows.Count > 0)
                {
                    lblHuiYuanName.Text = dt.Rows[0]["Name"].ToString();
                }
            }                
            else if (Request.QueryString["U"] != null && Request.QueryString["P"] != null)
            {
                //通过网站跳转过来的，则尝试登录
                string userGuid = Request.QueryString["U"].ToString();
                string passWrd = Business.Safety.SafetyBusiness.Decrypt(Request.QueryString["P"].ToString(), Business.Const.PageConst.Security.Key);

                HuiYuanEntity entity = new HuiYuanEntity(userGuid);
                if (entity.LoginPWD == passWrd)
                {
                    Session["UserGUID"] = entity.id;
                    Session["UserIDClient"] = entity.UserID;
                    Session["HuiYuanAccount"] = entity.LoginAcc;
                    Session["HuiYuanName"] = entity.Name;
                    lblHuiYuanName.Text = Session["HuiYuanName"].ToString();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect",
                                          "window.parent.location.href='Index.aspx'", true);
                }
            }
            else
            {
                divUser.Visible = false;
                divLogin.Visible = true;
            }
        }

        /// <summary>
        /// 登录按钮点击事件
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string uid = this.txtHuiYuanAccount.Text.Trim();
                if (uid == "")
                {
                    PageHelper.ShowExceptionMessage("请输入登录账号！");
                    return;
                }
                try
                {
                    DataTable dt = new BLL.Query.HuiYuanInfoQuery().GetInfoHuiYuan(txtHuiYuanAccount.Text.Trim(), txtPwd.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        Session["UserGUID"] = dt.Rows[0]["ID"].ToString();
                        Session["UserIDClient"] = dt.Rows[0]["UserID"].ToString();
                        Session["HuiYuanAccount"] = dt.Rows[0]["LoginAcc"].ToString();
                        Session["HuiYuanName"] = dt.Rows[0]["LoginPWD"].ToString();
                        lblHuiYuanName.Text = Session["HuiYuanName"].ToString();                        
                    }
                    else
                    {
                        PageHelper.ShowExceptionMessage("登录账号或者密码错误！");
                        return;
                    }
                }
                catch (Exception E)
                {
                    throw E;
                }

                this.divLogin.Visible = false;
                divUser.Visible = true;
                this.txtHuiYuanAccount.Text = "";
                this.txtPwd.Text = "";

                if (this.Page.Request.QueryString["RedirectURL"] != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect",
                        "window.parent.location.href='" + this.Page.Request.QueryString["RedirectURL"].ToString().Replace('^', '&') + "'", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect",
                                          "window.parent.location.href='Index.aspx'", true);
                }
            }
            catch (Exception E)
            {
                //FrameWeb.WebCommon.ExceptionMessage(E);
            }
        }

        /// <summary>
        /// 退出按钮点击事件
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbExit_Click(object sender, EventArgs e)
        {
            //this.Session.Abandon();
            Session["UserGUID"] = null;
            Session["UserIDClient"] = null;
            Session["HuiYuanAccount"] = null;
            Session["HuiYuanName"] = null;

            this.divLogin.Visible = true;
            this.divUser.Visible = false;
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "exit", "window.parent.location.href='Index.aspx';", true);
        }
    }
}