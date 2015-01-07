using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nandasoft;
using System.Data;
using BLL;
using BusinessLayer.Survey;
using WebManage.Common;
using BLL.Entity;
using System.Drawing;
using BLL.Query;
using Business.Helper;

namespace WebManage.Survey
{
    public partial class SelectGrid : System.Web.UI.Page
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
                BindData();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定问卷类型数据源
        /// 作者：姚东
        /// 时间：20100922
        /// </summary>
        private void BindData()
        {
            DataTable dt = new SurveyTableQuery().GetClassID();

            ddlSurveyClass.DataSource = dt;
            ddlSurveyClass.DataValueField = "CID";
            ddlSurveyClass.DataTextField = "SurveyClassName";
            ddlSurveyClass.DataBind();
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
            viewpage1.CurrentPageIndex = 1;

            this.TxtSurveyName =txtSurveyName.Text.Trim();
            if (!string.IsNullOrEmpty(getBackNum.Text.Trim()))
            {
                this.GetBackNum = NDConvert.ToInt32(getBackNum.Text.Trim());
            }
            else
            {
                this.GetBackNum = -1;
            }
            if (!string.IsNullOrEmpty(ddlSurveyClass.Text.Trim()))
            {
                this.DdlSurveyClass = NDConvert.ToInt32(ddlSurveyClass.Text.Trim());
            }
            else
            {
                this.DdlSurveyClass = -1;
            }
            this.WdcBeginDate = NDConvert.ToDateTime(wdcBeginDate.Value);
            if (NDConvert.ToDateTime(wdcEndDate.Value) == NDConvert.ToDateTime("1900-1-1"))
            {
                this.WdcEndDate = NDConvert.ToDateTime("2999-12-31");
            }
            else
            {
                this.WdcEndDate = NDConvert.ToDateTime(wdcEndDate.Value);
            }

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
            int count = 0;
            int num = 0;
            switch (e.Item.Value)
            {
                case "Repeal":
                    BindGridView();
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
            long ID = NDConvert.ToInt64(grid.DataKeys[rowIndex]["SID"].ToString());
            SurveyTableEntity entity = new SurveyTableEntity(ID);

            switch (e.CommandName)
            {
                //case "State":   //编辑状态
                //    if (entity.State == (int)CommonEnum.SurveyState.NotCreate)
                //    {
                //        entity.State = (int)CommonEnum.SurveyState.HasCreated;

                //        new SurveyTableRule().Update(entity);

                //        PageHelper.ShowMessage("编辑状态更改成功！");
                //        BindGridView();
                //    }
                //    else
                //    {
                //        entity.State = (int)CommonEnum.SurveyState.NotCreate;

                //        new SurveyTableRule().Update(entity);

                //        PageHelper.ShowMessage("编辑状态更改成功！");
                //        BindGridView();
                //    }
                //    break;
                case "SurveyName":
                    base.Response.Write(string.Format("<script language='javascript'>top.location.href = \"editsurvey.aspx?SID={0}\"</script>", ID));// ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("<script language='javascript'>window.parent.parent.alert(\"{0}\")</script>", "保存成功！"));
                    BindGridView();
                    break;
                case "AnswerAmount":
                    //string urlAnswerAmount = string.Format("AnswerManager.aspx?SID={0}", ID);
                    base.Response.Write(string.Format("<script language='javascript'>location.href = \"AnswerManager.aspx?SID={0}\"</script>", ID));
                    BindGridView();
                    break;

                case "TempPage":
                    //TODO:PageHelper.WriteScript("window.open('../../Web/Gifts/ImagePhoto.aspx?ID=" + entity.GiftGuid + "');");
                    break;
                case "Statistics":
                    base.Response.Write(string.Format("<script language='javascript'>location.href = \"StatIndex.aspx?SID={0}\"</script>", ID));
                    BindGridView();
                    break;
                case "Options":
                    string urlOptions = string.Format("SetPar.aspx?SID={0}", ID);
                    //PageHelper.WriteScript(string.Format("window.open('{0}');", urlOptions));
                    Response.Redirect(urlOptions);
                    BindGridView();
                    break;
                case "Editer":
                    base.Response.Write(string.Format("<script language='javascript'>top.location.href = \"editsurvey.aspx?SID={0}\"</script>", ID));// ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("<script language='javascript'>window.parent.parent.alert(\"{0}\")</script>", "保存成功！"));
                    BindGridView();
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
            long UserID = NDConvert.ToInt64(Session["UserID"]); //当前管理操作的中
            DataSet ds = GetData(UserID.ToString());
            DataTable dt = ds.Tables[1];

            grid.DataSource = dt;
            grid.DataBind();

            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                long ID = NDConvert.ToInt64(grid.DataKeys[i]["SID"].ToString());

                DataTable GetSurveyInfo = new SurveyManage_Layer().GetSurveyTable(ID.ToString(), UserID.ToString());


                //编辑
                //LinkButton objEdit = (LinkButton)grid.Rows[i].Cells[10].Controls[0];

                //System.Web.UI.WebControls.Image image2 = new System.Web.UI.WebControls.Image();
                //image2.Style.Add("margin", "0");
                //image2.ImageUrl = "images/edit2.gif";
                //objEdit.Text = "编辑";
                //grid.Rows[i].Cells[10].Controls.AddAt(0, image2);
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

        #region 获得调查问卷信息
        /// <summary>
        /// 获得调查问卷信息
        /// </summary>
        private DataSet GetData(string UserID)
        {
            DataSet ds = new Survey_StatIndex_Layer().GetSurveyTableSelectGrid(UserID, TxtSurveyName, GetBackNum, DdlSurveyClass, WdcBeginDate, WdcEndDate, viewpage1.CurrentPageIndex, viewpage1.PageSize);
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
        /// 查询条件：问卷名称
        /// </summary>
        private string TxtSurveyName
        {
            set { ViewState["TxtSurveyName"] = value; }
            get
            {
                if (ViewState["TxtSurveyName"] != null)
                {
                    return ViewState["TxtSurveyName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 查询条件：回收数
        /// </summary>
        private int GetBackNum
        {
            set { ViewState["GetBackNum"] = value; }
            get
            {
                if (ViewState["GetBackNum"] != null)
                {
                    return NDConvert.ToInt32(ViewState["GetBackNum"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：问卷类型
        /// </summary>
        private int DdlSurveyClass
        {
            set { ViewState["DdlSurveyClass"] = value; }
            get
            {
                if (ViewState["DdlSurveyClass"] != null)
                {
                    return NDConvert.ToInt32(ViewState["DdlSurveyClass"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：创建时间（开始）
        /// </summary>
        private DateTime WdcBeginDate
        {
            set { ViewState["WdcBeginDate"] = value; }
            get
            {
                if (ViewState["WdcBeginDate"] != null)
                {
                    return NDConvert.ToDateTime(ViewState["WdcBeginDate"]);
                }
                else
                {
                    return NDConvert.ToDateTime("1900-1-1");
                }
            }
        }

        /// <summary>
        /// 查询条件：创建时间（结束）
        /// </summary>
        private DateTime WdcEndDate
        {
            set { ViewState["WdcEndDate"] = value; }
            get
            {
                if (ViewState["WdcEndDate"] != null)
                {
                    return NDConvert.ToDateTime(ViewState["WdcEndDate"]);
                }
                else
                {
                    return NDConvert.ToDateTime("2999-12-31");
                }
            }
        }

        #endregion

        protected void Choosed_Click(object sender, EventArgs e)
        {
            string sid="";
            sid = ConvertHelper.ConvertString(Request.Form["choose"]);
            if (sid == "")
            {
                Response.Write(string.Format("<script language='javascript'> alert('{0}');</script>","请选择问卷！"));
            }
            else
            {
                Response.Write(string.Format("<script language='javascript'> parent.location.href = 'statindex.aspx?sid={0}';</script>", sid));
            }
        }
    }
}
