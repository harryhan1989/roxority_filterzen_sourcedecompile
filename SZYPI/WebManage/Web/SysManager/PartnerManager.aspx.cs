using System;
using System.Data;
using System.Web.UI.WebControls;
using BLL;
using BLL.Rule;
using BLL.Query;
using Nandasoft;
using WebUI;
using BLL.Entity;

namespace WebManage.Web.SysManager
{
    public partial class PartnerManager : BasePage
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #endregion

        #region 查询
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
            viewpage1.CurrentPageIndex = 1;
            this.Name = txtName.Text.Trim();
            this.Status = NDConvert.ToInt32(ddlStatus.SelectedValue);
            BindGridView();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region 数据行操作
        /// <summary>
        /// 数据行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = NDConvert.ToInt32(e.CommandArgument);
            string partnerID = grid.DataKeys[rowIndex]["ID"].ToString();
            switch (e.CommandName)
            {
                case "Status":
                    PartnerEntity entity = new PartnerEntity(partnerID);
                    if (entity.Status == (int)CommonEnum.PartnerStatus.Offline)
                    {
                        entity.Status = (int)CommonEnum.PartnerStatus.Online;
                    }
                    else
                    {
                        entity.Status = (int)CommonEnum.PartnerStatus.Offline;

                    }
                    
                    new PartnerRule().Update(entity);
                    PageHelper.ShowMessage("状态更改成功！");
                    BindGridView();
                    break;
                case "View":
                    PageHelper.WriteScript("window.open('ImagePhoto.aspx?ID=" + partnerID + "');");
                    break;
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindGridView();
        }
        #endregion

        #region 邦定GridView
        /// <summary>
        /// 邦定GridView
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = GetData();
            DataTable dt = ds.Tables[1];
            grid.DataSource = dt;
            grid.DataBind();

            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                LinkButton obj = (LinkButton)grid.Rows[i].Cells[3].Controls[0];
                if (NDConvert.ToInt32(grid.DataKeys[i]["Status"].ToString()) == (int)CommonEnum.PartnerStatus.Online)
                {
                    obj.Text = "发布";
                }
                else
                {
                    obj.Text = "下架";
                }
            }
        }
        #endregion

        #region 邦定分页控件
        /// <summary>
        /// 邦定分页控件
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            //viewpage1.CurrentPageIndex = 1;
            viewpage1.RecordCount = RecordCount;
        }
        #endregion

        #region 获得列表数据
        /// <summary>
        /// 获得列表数据
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new PartnerQuery().GetPartnerInfo(this.Name, this.Status, viewpage1.CurrentPageIndex, viewpage1.PageSize);
            return ds;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            return true;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 查询条件：合作伙伴名称
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
        /// 查询条件：礼品状态
        /// </summary>
        private int Status
        {
            set { ViewState["Status"] = value; }
            get
            {
                if (ViewState["Status"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Status"]);
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