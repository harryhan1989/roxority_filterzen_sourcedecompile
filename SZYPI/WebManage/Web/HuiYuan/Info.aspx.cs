using System;
using BLL.Entity;
using System.Data;
using BLL.Rule;
using WebUI;

namespace WebManage.Web.HuiYuan
{
    /// <summary>
    /// 我的账户会员基本信息管理
    /// 作者：姚东
    /// 时间：20100927
    /// </summary>
    public partial class Info : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20100927
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserGUID"] != null)
                {
                    BindInfo();
                }
                else
                {
                    Response.Redirect("../../Error/WebErrorPage.aspx?RedirectURL=" + Request.Url.ToString().Replace('&', '^'));
                }
            }
        }

        /// <summary>
        /// 信息绑定
        /// 作者：姚东
        /// 时间：20100927
        /// </summary>
        private void BindInfo()
        {
            HuiYuanEntity entity = new HuiYuanEntity(Session["UserGUID"].ToString());

            lblHuiYuanAccount.Text = entity.LoginAcc;
            lblPwd.Text = "<a href='#' onclick='window.open(\"ModifyPassword.aspx\");'>修改密码</a>";
            txtHuiYuanName.Text = entity.Name;
            txtEmail.Text = entity.Email;
            txtMobile.Text = entity.Tel;

            DataTable dt = new BLL.Query.HuiYuanInfoQuery().GetInfoHuiYuan(Session["UserGUID"].ToString());

            if (dt.Rows.Count > 0)
            {
                lblTotalPoint.Text = dt.Rows[0]["TotalPoint"].ToString();
                lblRemainPoint.Text = dt.Rows[0]["RemainPoint"].ToString();
            }
        }

        /// <summary>
        /// 信息修改
        /// 作者：姚东
        /// 时间：20100927
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            HuiYuanEntity entity = new HuiYuanEntity(Session["UserGUID"].ToString());

            entity.Name = txtHuiYuanName.Text.Trim();
            entity.Email = txtEmail.Text.Trim();
            entity.Tel = txtMobile.Text.Trim();

            new HuiYuanRule().Update(entity);

            PageHelper.ShowExceptionMessage("更新成功！");
            
        }
    }
}