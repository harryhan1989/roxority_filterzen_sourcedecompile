using System;
using System.Data;
using System.Web.UI.WebControls;
using BLL;
using BLL.Rule;
using BLL.Query;
using Nandasoft;
using WebUI;
using BLL.Entity;

namespace WebManage.Web.Gifts
{
    /// <summary>
    /// 礼品兑换确认功能
    /// 作者：姚东
    /// 时间：20100920
    /// </summary>
    public partial class Exchange : BasePage
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

            this.HuiYuanAccount = txtHuiYuanAccount.Text.Trim();
            this.HuiYuanName = txtHuiYuanName.Text.Trim();
            this.GiftName = txtGiftName.Text.Trim();            
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

        #region toolbar事件
        /// <summary>
        /// toolbar事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    long ID = 0;
                    int count = 0;
                    bool hasValidData = false;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            ID = NDConvert.ToInt64(grid.DataKeys[i]["ID"].ToString());
                            ExchangeEntity entity = new ExchangeEntity(ID);

                            if (entity.Status == (int)CommonEnum.GiftExchangeStatus.ForExchange)
                            {
                                hasValidData = true;

                                //更新礼品兑换状态
                                entity.Status = (int)CommonEnum.GiftExchangeStatus.HasDrop;
                                new ExchangeRule().Update(entity);

                                //更新会员积分
                                PointEntity pointEntity = new PointEntity(entity.HuiYuanGuid);
                                pointEntity.RemainPoint += entity.UsedPoint;
                                new PointRule().Update(pointEntity);

                                //更新礼品余量
                                GiftsEntity giftsEntity = new GiftsEntity(entity.GiftGuid);
                                giftsEntity.RemainAmount += 1;
                                new GiftsRule().Update(giftsEntity);
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();

                    if (hasValidData)
                    {
                        PageHelper.ShowMessage("操作成功！");
                    }
                    else
                    {
                        PageHelper.ShowExceptionMessage("不能对该记录进行取消操作！");
                    }
                    break;
            }
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
            long ID = NDConvert.ToInt64(grid.DataKeys[rowIndex]["ID"].ToString());
            ExchangeEntity entity = new ExchangeEntity(ID);

            switch (e.CommandName)
            {
                case "Status":
                    //GiftsEntity entity = new GiftsEntity(GiftID);
                    if (entity.Status == (int)CommonEnum.GiftExchangeStatus.ForExchange)
                    {
                        entity.Status = (int)CommonEnum.GiftExchangeStatus.HasExchange;

                        entity.ExchangeTime = DateTime.Now;
                        new ExchangeRule().Update(entity);
                        new HuiYuanRule().UpdateRemainPoint(entity.HuiYuanGuid, entity.UsedPoint);

                        PageHelper.ShowMessage("状态更改成功！");
                        BindGridView();
                    }
                    break;
                case "View":
                    PageHelper.WriteScript("window.open('../../Web/Gifts/ImagePhoto.aspx?ID=" + entity.GiftGuid + "');");
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
                LinkButton obj = (LinkButton)grid.Rows[i].Cells[8].Controls[0];
                if (NDConvert.ToInt32(grid.DataKeys[i]["Status"].ToString()) == (int)CommonEnum.GiftExchangeStatus.ForExchange)
                {
                    obj.Text = "同意兑换";
                    obj.Enabled = true;
                    //obj.Attributes.Add("onclick", "if(confirm('确认要进行此次礼品兑换么？')==true）return true; else return false;");
                    obj.Attributes.Add("onclick", "return ConfirmExchange();");
                }
                else if (NDConvert.ToInt32(grid.DataKeys[i]["Status"].ToString()) == (int)CommonEnum.GiftExchangeStatus.HasExchange)
                {
                    obj.Text = "已兑换";
                    obj.Enabled = false;
                }
                else 
                {
                    obj.Text = "已放弃";
                    obj.Enabled = false;
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

        #region 获得广告信息
        /// <summary>
        /// 获得广告信息
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new ExchangeQuery().GetExchangeInfo(this.HuiYuanAccount, this.HuiYuanName, this.GiftName, this.Status, viewpage1.CurrentPageIndex, viewpage1.PageSize);
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
        /// 查询条件：会员账号
        /// </summary>
        private string HuiYuanAccount
        {
            set { ViewState["HuiYuanAccount"] = value; }
            get
            {
                if (ViewState["HuiYuanAccount"] != null)
                {
                    return ViewState["HuiYuanAccount"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 查询条件：会员名称
        /// </summary>
        private string HuiYuanName
        {
            set { ViewState["HuiYuanName"] = value; }
            get
            {
                if (ViewState["HuiYuanName"] != null)
                {
                    return ViewState["HuiYuanName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 查询条件：礼品名称
        /// </summary>
        private string GiftName
        {
            set { ViewState["GiftName"] = value; }
            get
            {
                if (ViewState["GiftName"] != null)
                {
                    return ViewState["GiftName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 查询条件：兑换状态
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
