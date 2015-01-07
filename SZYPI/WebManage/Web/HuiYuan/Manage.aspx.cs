using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;

namespace WebManage.Web.HuiYuan
{
    /// <summary>
    /// 会员管理功能
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    public partial class Manage : BasePage
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUrlParameter();
                InitPage();
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// 作者：姚东
        /// 时间：20100919
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
            viewpage1.CurrentPageIndex = 1;
            this.Name = txtName.Text.Trim();
            this.Account = txtAccount.Text.Trim();
            this.Status = NDConvert.ToInt32(drpStatus.SelectedValue);
            BindGridView();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region toolbar事件
        /// <summary>
        /// toolbar事件
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    string HuiYuanID = "";
                    int count = 0;
                    int num = 0;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            HuiYuanID = grid.DataKeys[i]["ID"].ToString();
                            DataTable dt = new BLL.Query.HuiYuanInfoQuery().GetInfoHuiYuan(HuiYuanID);

                            if (dt.Rows.Count>0 && dt.Rows[0]["Status"].ToString() == "1")
                            {
                                new BLL.Rule.HuiYuanRule().DelInfoHuiYuan(HuiYuanID);
                            }
                            else
                            {
                                num++;
                                continue;
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();

                    PageHelper.ShowMessage("停用成功！");
                    if (num > 0)
                    {
                        PageHelper.ShowExceptionMessage("部分(已审核、禁用)信息无法停用！");
                    }
                    break;
                case "Authorization":
                    HuiYuanID = "";
                    count = 0;
                    num = 0;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            HuiYuanID = grid.DataKeys[i]["ID"].ToString();
                            DataTable dt = new BLL.Query.HuiYuanInfoQuery().GetInfoHuiYuan(HuiYuanID);

                            if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "0")
                            {
                                new BLL.Rule.HuiYuanRule().StartInfoHuiYuan(HuiYuanID);
                            }
                            else
                            {
                                num++;
                                continue;
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();

                    PageHelper.ShowMessage("启用成功！");
                    if (num > 0)
                    {
                        PageHelper.ShowExceptionMessage("部分(已审核、禁用)信息无法停用！");
                    }
                    break;
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region 行绑定
        /// <summary>
        /// 行绑定
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!this.toolbar.Items[1].Enabled)
            {
                return;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string HuiYuanID = grid.DataKeys[e.Row.RowIndex]["ID"].ToString();
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 0) continue;

                    cell.Attributes["onclick"] = "showModalWindow('HuiYuanEdit','修改',400, 300,'../../Web/HuiYuan/Edit.aspx?Operation=2&ID=" + HuiYuanID + "')";
                    cell.Style["cursor"] = "hand";
                }
            }
        }
        #endregion

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["HuiYuanID"] != null && Request.QueryString["HuiYuanID"] != "")
            {
                this.HuiYuanID = Request.QueryString["HuiYuanID"].ToString();
            }
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void InitPage()
        {
            BindGridView();
        }
        #endregion

        #region 邦定GridView
        /// <summary>
        /// 邦定GridView
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = GetData();
            DataTable dt = ds.Tables[1];
            grid.DataSource = dt;
            grid.DataBind();

            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));
        }
        #endregion

        #region 邦定分页控件
        /// <summary>
        /// 邦定分页控件
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            
            viewpage1.RecordCount = RecordCount;
        }
        #endregion

        #region 获得网站信息数据
        /// <summary>
        /// 获得网站信息数据
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new DataSet();

            ds = new BLL.Query.HuiYuanInfoQuery().GetInfo(this.Name,this.Account,this.Status, viewpage1.CurrentPageIndex, viewpage1.PageSize);

            return ds;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证数据
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                if (CommonHelp.CheckString(txtName.Text.Trim()))
                {
                    hidInfo.Value = "姓名中不能包含特殊字符！";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtAccount.Text.Trim()))
            {
                if (CommonHelp.CheckString(txtAccount.Text.Trim()))
                {
                    hidInfo.Value = "账号中不能包含特殊字符！";
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 接收URL参数传入的信息类型ID
        /// 作者：姚东
        /// 时间：20100919        /// </summary>
        
        private string HuiYuanID
        {
            set { ViewState["HuiYuanID"] = value.ToString(); }
            get
            {
                if (ViewState["HuiYuanID"] != null)
                {
                    return ViewState["HuiYuanID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
       

        /// <summary>
        /// 姓名
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private string Name
        {
            set { ViewState["Name"] = value; }
            get
            {
                if (ViewState["Name"] != null)
                {
                    return ViewState["Name"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 账号
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private string Account
        {
            set { ViewState["Account"] = value; }
            get
            {
                if (ViewState["Account"] != null)
                {
                    return ViewState["Account"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 状态
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private int Status
        {
            set { ViewState["Status"] = value; }
            get
            {
                if (ViewState["Status"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Status"].ToString());
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