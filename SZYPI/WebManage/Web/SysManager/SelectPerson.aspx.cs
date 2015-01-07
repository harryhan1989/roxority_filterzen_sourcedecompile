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
using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;

namespace WebManage.Web.SystemManager
{
    public partial class SelectPerson : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }

            this.OUID = NDConvert.ToInt64(drpOU.SelectedValue);

            BindGridView();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int RowIndex = NDConvert.ToInt32(e.CommandArgument);
                    Session["SelectUserName"] = grid.DataKeys[RowIndex].Values["UserName"].ToString();
                    Session["SelectUserID"] = grid.DataKeys[RowIndex].Values["UserID"].ToString();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('SelectPersonLY','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindOU();
            BindGridView();
        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindOU()
        {
            DataTable dt = new OUQuery().GetOU();
            NDHelperWebControl.LoadDropDownList(drpOU, dt, "OUName", "OUID", new ListItem("请选择", "0"));
        }

        /// <summary>
        /// 绑定
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = GetData();
            DataTable dt = ds.Tables[1];
            grid.DataSource = dt;
            grid.DataBind();
            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new UserQuery().GetUsers(this.OUID, txtUserName.Text, viewpage1.CurrentPageIndex, viewpage1.PageSize);
            return ds;
        }

        /// <summary>
        /// 邦定分页控件
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            viewpage1.RecordCount = RecordCount;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                if (CommonHelp.CheckString(txtUserName.Text.Trim()))
                {
                    hidInfo.Value = "姓名不能输入特殊字符！";
                    txtUserName.Focus();
                    return false;
                }
            }
            return true;
        }

        #region 属性
        /// <summary>
        /// 部门ID
        /// </summary>
        private long OUID
        {
            set { ViewState["OUID"] = value; }
            get
            {
                if (ViewState["OUID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["OUID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

      

    }
}
