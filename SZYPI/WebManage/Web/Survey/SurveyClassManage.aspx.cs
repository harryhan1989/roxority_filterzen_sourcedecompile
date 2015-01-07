using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;
using BLL.Entity;
using BLL.Rule;

namespace WebManage.Web.Survey
{
    public partial class SurveyClassManage : BasePage
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// 作者：姚东
        /// 时间：20101025
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

        #region 刷新
        /// <summary>
        /// 刷新
        /// 作者：姚东
        /// 时间：20101025
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
        /// 时间：20101025
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    string cid = "";
                    int count = 0;
                    int num = 0;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            cid = grid.DataKeys[i]["CID"].ToString();

                            SurveyClassEntity entity = new SurveyClassEntity(NDConvert.ToInt64(cid));

                            new SurveyClassRule().Delete(entity);
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
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// 作者：姚东
        /// 时间：20101025
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
        /// 时间：20101025
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
                string CID = grid.DataKeys[e.Row.RowIndex]["CID"].ToString();
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 0) continue;

                    cell.Attributes["onclick"] = "showModalWindow('SurveyClassEdit','修改',400, 300,'../../Web/Survey/SurveyClassEdit.aspx?Operation=2&ID=" + CID + "')";
                    cell.Style["cursor"] = "hand";
                }
            }
        }
        #endregion

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// 作者：姚东
        /// 时间：20101025
        /// </summary>
        private void GetUrlParameter()
        {
            //if (Request.QueryString["HuiYuanID"] != null && Request.QueryString["HuiYuanID"] != "")
            //{
            //    this.HuiYuanID = Request.QueryString["HuiYuanID"].ToString();
            //}
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// 作者：姚东
        /// 时间：20101025
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
        /// 时间：20101025
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
        /// 时间：20101025
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
        /// 时间：20101025
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new DataSet();

            ds = new BLL.Query.SurveyClassQuery().GetInfo(viewpage1.CurrentPageIndex, viewpage1.PageSize);

            return ds;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 类型ID
        /// 作者：姚东
        /// 时间：20101025        
        /// </summary>
        private long CID
        {
            set { ViewState["CID"] = value; }
            get
            {
                if (ViewState["CID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["CID"]);
                }
                else
                {
                    return -1;
                }
            }
        }


        /// <summary>
        /// 类型名称
        /// 作者：姚东
        /// 时间：20101025
        /// </summary>
        private string SurveyClassName
        {
            set { ViewState["SurveyClassName"] = value; }
            get
            {
                if (ViewState["SurveyClassName"] != null)
                {
                    return ViewState["SurveyClassName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 排序
        /// 作者：姚东
        /// 时间：20101025
        /// </summary>
        private int Sort
        {
            set { ViewState["Sort"] = value; }
            get
            {
                if (ViewState["Sort"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Sort"]);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 是否默认类型
        /// 作者：姚东
        /// 时间：20101025
        /// </summary>
        private bool DefaultClass
        {
            set { ViewState["DefaultClass"] = value; }
            get
            {
                if (ViewState["DefaultClass"] != null)
                {
                    return NDConvert.ToBoolean(ViewState["DefaultClass"]);
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }
}