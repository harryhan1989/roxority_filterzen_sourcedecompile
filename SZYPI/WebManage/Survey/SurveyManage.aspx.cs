using System;
using System.Data;
using System.Web.UI.WebControls;
using BLL;
using BLL.Rule;
using BLL.Query;
using Nandasoft;
using WebUI;
using BLL.Entity;
using System.Drawing;
using BusinessLayer.Survey;

namespace WebManage.Survey
{

    /// <summary>
    /// 调查管理GRID页面
    /// 作者：姚东
    /// 时间：20100921
    /// </summary>
    public partial class SurveyManage : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            initToolBar();
            base.OnInit(e);
        }

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
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }
            viewpage1.CurrentPageIndex = 1;

            this.SurveyName = txtSurveyName.Text.Trim();
            this.State = NDConvert.ToInt32(ddlState.SelectedValue);
            this.Active = NDConvert.ToInt32(ddlActive.SelectedValue);
            this.ClassID = NDConvert.ToInt32(ddlSurveyClass.SelectedValue);
            this.BeginDate = NDConvert.ToDateTime(wdcBeginDate.Value);
            if (NDConvert.ToDateTime(wdcEndDate.Value) == NDConvert.ToDateTime("1900-1-1"))
            {
                this.EndDate = NDConvert.ToDateTime("2999-12-31");
            }
            else
            {
                this.EndDate = NDConvert.ToDateTime(wdcEndDate.Value);
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

        #region 初始化操作按钮
        public void initToolBar()
        {
            if (!IsPostBack)
            {
                long UserID = NDConvert.ToInt64(Session["UserID"]); //当前管理操作的中
                if (UserID > 0)
                {
                    int userType = ConvertInt(new SurveyManage_Layer().GetUserType(ConvertString(UserID)));
                    if (userType == (int)CommonEnum.QSUserType.AdminUserType)
                    {
                        MenuItem Pass = new MenuItem();
                        Pass.Text = "审批通过";
                        Pass.Value = "Pass";
                        Pass.ImageUrl = "../web/Images/icon/accept.png";

                        MenuItem Invalid = new MenuItem();
                        Invalid.Text = "审批退回";
                        Invalid.Value = "Invalid";
                        Invalid.ImageUrl = "../web/Images/icon/arrow_undo.png";

                        this.toolbar.Items.Add(Pass);
                        this.toolbar.Items.Add(Invalid);
                    }
                    else if (userType == (int)CommonEnum.QSUserType.CommonUserType)
                    {

                    }
                    MenuItem ReInput = new MenuItem();
                    ReInput.Text = "重新提交审批";
                    ReInput.Value = "ReInput";
                    ReInput.ImageUrl = "../web/Images/icon/arrow_redo.jpg";

                    this.toolbar.Items.Add(ReInput);
                }
            }
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
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        long SID = Business.Helper.ConvertHelper.ConvertLong(grid.DataKeys[i]["SID"]);
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            SurveyTableEntity entity = new SurveyTableEntity(SID);
                            if (entity.State == (int)CommonEnum.SurveyState.HasCreated)
                            {
                                entity.State = (int)CommonEnum.SurveyState.NotCreate;

                                new SurveyTableRule().Update(entity);
                                num++;
                            }

                            if (num > 0)
                            {
                                PageHelper.ShowMessage("所选问卷成功撤销发布！");
                            }
                            else
                            {
                                PageHelper.ShowMessage("选中问卷中都无需撤销发布问卷！");
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    break;
                case "Pass":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        long SID = Business.Helper.ConvertHelper.ConvertLong(grid.DataKeys[i]["SID"]);
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            if (Business.Helper.ConvertHelper.ConvertInt(grid.DataKeys[i]["ApprovalStaus"]) == (int)CommonEnum.SurveyApprovalStaus.Waiting)
                            {
                                new SurveyManage_Layer().GetUserType(SID.ToString(),"1");
                                num++;
                            }

                            if (num > 0)
                            {
                                PageHelper.ShowMessage("所选问卷成功审批通过！");
                            }
                            else
                            {
                                PageHelper.ShowMessage("选中问卷中都无需审批！");
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    break;
                case "Invalid":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        long SID = Business.Helper.ConvertHelper.ConvertLong(grid.DataKeys[i]["SID"]);
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            if (Business.Helper.ConvertHelper.ConvertInt(grid.DataKeys[i]["ApprovalStaus"]) == (int)CommonEnum.SurveyApprovalStaus.Waiting)
                            {
                                new SurveyManage_Layer().GetUserType(SID.ToString(), "2");
                                num++;
                            }

                            if (num > 0)
                            {
                                PageHelper.ShowMessage("所选问卷成功审批退回！");
                            }
                            else
                            {
                                PageHelper.ShowMessage("选中问卷中都无需审批！");
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    break;
                case "ReInput":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        long SID = Business.Helper.ConvertHelper.ConvertLong(grid.DataKeys[i]["SID"]);
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            if (Business.Helper.ConvertHelper.ConvertInt(grid.DataKeys[i]["ApprovalStaus"]) == (int)CommonEnum.SurveyApprovalStaus.Back)
                            {
                                new SurveyManage_Layer().GetUserType(SID.ToString(), "0");
                                num++;
                            }

                            if (num > 0)
                            {
                                PageHelper.ShowMessage("所选问卷成功重新提交审批！");
                            }
                            else
                            {
                                PageHelper.ShowMessage("选中问卷中都无需重新提交审批！");
                            }
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
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
                case "State":   //编辑状态
                    if (entity.State == (int)CommonEnum.SurveyState.NotCreate)
                    {
                        PageHelper.ShowMessage("页面还未生成，无法更改状态！");
                        BindGridView();
                    }
                    else
                    {
                        string url = string.Format("userdata/u1/s{0}.aspx", ID);
                        PageHelper.WriteScript(string.Format("window.open('{0}');", url));
                        BindGridView();
                    }
                    break;
                case "Active":  //活动状态
                    if ((entity.Active == (int)CommonEnum.SurveyActive.InActive))
                    {
                        if (entity.State == (int)CommonEnum.SurveyState.HasCreated)
                        {
                            entity.Active = (int)CommonEnum.SurveyActive.Active;

                            new SurveyTableRule().Update(entity);

                            PageHelper.ShowMessage("活动状态更改成功！");
                            BindGridView();
                        }
                        else
                        {
                            PageHelper.ShowMessage("问卷未生成，不能修改活动状态！");
                            BindGridView();
                        }
                    }
                    else
                    {
                        entity.Active = (int)CommonEnum.SurveyActive.InActive;

                        new SurveyTableRule().Update(entity);

                        PageHelper.ShowMessage("活动状态更改成功！");
                        BindGridView();
                    }
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
                case "DeleteSurvey":
                    try
                    {
                        new SurveyManage_Layer().UpdateSurveyTable(ID.ToString());
                        PageHelper.ShowMessage("问卷删除成功！");
                    }
                    catch
                    {
                        PageHelper.ShowMessage("问卷删除出错！");
                    }
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
            if (UserID > 0)
            {
                DataSet ds = GetData(UserID.ToString());
                DataTable dt = ds.Tables[1];

                grid.DataSource = dt;
                grid.DataBind();

                BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));

                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    long ID = NDConvert.ToInt64(grid.DataKeys[i]["SID"].ToString());

                    DataTable GetSurveyInfo = new SurveyManage_Layer().GetSurveyTable(ID.ToString(), UserID.ToString());

                    //问卷名称
                    LinkButton objName = (LinkButton)grid.Rows[i].Cells[1].Controls[0];
                    objName.ToolTip = grid.DataKeys[i]["SurveyName"].ToString();
                    if (grid.DataKeys[i]["SurveyName"].ToString().Length > 7)
                    {
                        objName.Text = grid.DataKeys[i]["SurveyName"].ToString().Substring(0, 6);
                    }
                    else
                    {
                        objName.Text = grid.DataKeys[i]["SurveyName"].ToString();
                    }
                    if (NDConvert.ToInt32(grid.DataKeys[i]["State"].ToString()) == (int)CommonEnum.SurveyState.NotCreate)
                    {
                        objName.ForeColor = Color.Red;
                    }
                    else
                    {
                        objName.ForeColor = Color.Green;
                    }

                    //回收数
                    LinkButton objAnswerAmount = (LinkButton)grid.Rows[i].Cells[2].Controls[0];

                    if (GetSurveyInfo.Rows.Count > 0)
                    {
                        if (long.Parse(GetSurveyInfo.Rows[0]["MaxAnswerAmount"].ToString()) > 0)
                        {
                            objAnswerAmount.Text = GetSurveyInfo.Rows[0]["AnswerAmount"].ToString() + "/" + GetSurveyInfo.Rows[0]["MaxAnswerAmount"].ToString();
                        }
                        else
                        {
                            objAnswerAmount.Text = GetSurveyInfo.Rows[0]["AnswerAmount"].ToString() + "/" + "不限";
                        }
                        objAnswerAmount.Attributes.CssStyle.Add("text-decoration", "underline");
                    }


                    //编辑状态
                    LinkButton objState = (LinkButton)grid.Rows[i].Cells[3].Controls[0];

                    System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                    image.Style.Add("margin", "0");
                    if (NDConvert.ToInt32(grid.DataKeys[i]["State"].ToString()) == (int)CommonEnum.SurveyState.NotCreate)
                    {
                        image.ImageUrl = "images/SurveyNotComplate.gif";
                        grid.Rows[i].Cells[3].Controls.AddAt(0, image);
                        objState.Text = "未生成";
                    }
                    else
                    {
                        image.ImageUrl = "images/SurveyComplate.gif";
                        grid.Rows[i].Cells[3].Controls.AddAt(0, image);
                        objState.Text = "已生成";
                    }

                    //活动状态
                    LinkButton objActive = (LinkButton)grid.Rows[i].Cells[4].Controls[0];

                    System.Web.UI.WebControls.Image image1 = new System.Web.UI.WebControls.Image();
                    image1.Style.Add("margin", "0");
                    if (NDConvert.ToInt32(grid.DataKeys[i]["Active"].ToString()) == (int)CommonEnum.SurveyActive.InActive)
                    {
                        image1.ImageUrl = "images/stop.gif";
                        grid.Rows[i].Cells[4].Controls.AddAt(0, image1);
                        objActive.Text = "禁用";
                    }
                    else
                    {
                        image1.ImageUrl = "images/run.gif";
                        grid.Rows[i].Cells[4].Controls.AddAt(0, image1);
                        objActive.Text = "启用";
                    }

                    //审核状态
                    LinkButton objApproval = (LinkButton)grid.Rows[i].Cells[5].Controls[0];
                    if (ConvertString(grid.DataKeys[i]["ApprovalStaus"]) == ConvertString((int)CommonEnum.SurveyApprovalStaus.Waiting))
                    {
                        grid.Rows[i].Cells[5].Text = "待审批";
                        grid.Rows[i].Cells[5].ForeColor = Color.Red;
                    }
                    else if (ConvertString(grid.DataKeys[i]["ApprovalStaus"]) == ConvertString((int)CommonEnum.SurveyApprovalStaus.Pass))
                    {
                        grid.Rows[i].Cells[5].Text = "已通过";
                        grid.Rows[i].Cells[5].ForeColor = Color.Green;
                    }
                    else if (ConvertString(grid.DataKeys[i]["ApprovalStaus"]) == ConvertString((int)CommonEnum.SurveyApprovalStaus.Back))
                    {
                        grid.Rows[i].Cells[5].Text = "被退回";
                    }

                    //分析
                    LinkButton objStatistics = (LinkButton)grid.Rows[i].Cells[9].Controls[0];

                    System.Web.UI.WebControls.Image image5 = new System.Web.UI.WebControls.Image();
                    image5.Style.Add("margin", "0");
                    if (NDConvert.ToInt32(grid.DataKeys[i]["State"].ToString()) == (int)CommonEnum.SurveyState.NotCreate)
                    {
                        image5.ImageUrl = "images/statDisable.gif";
                        objStatistics.Enabled = false;
                    }
                    else
                    {
                        image5.ImageUrl = "images/stat.gif";
                        objStatistics.Enabled = true;
                    }
                    objStatistics.Text = "分析";
                    objStatistics.OnClientClick = "showMenuAll();";
                    grid.Rows[i].Cells[9].Controls.AddAt(0, image5);

                    //选项
                    LinkButton objOption = (LinkButton)grid.Rows[i].Cells[10].Controls[0];

                    System.Web.UI.WebControls.Image image4 = new System.Web.UI.WebControls.Image();
                    image4.Style.Add("margin", "0");
                    image4.ImageUrl = "images/modifyoption.gif";
                    objOption.Text = "选项";
                    grid.Rows[i].Cells[10].Controls.AddAt(0, image4);


                    //编辑
                    LinkButton objEdit = (LinkButton)grid.Rows[i].Cells[11].Controls[0];

                    System.Web.UI.WebControls.Image image2 = new System.Web.UI.WebControls.Image();
                    image2.Style.Add("margin", "0");
                    image2.ImageUrl = "images/edit2.gif";
                    objEdit.Text = "编辑";
                    grid.Rows[i].Cells[11].Controls.AddAt(0, image2);

                    //删除
                    LinkButton objDel = (LinkButton)grid.Rows[i].Cells[12].Controls[0];
                    objDel.OnClientClick = "if(!window.confirm('确定要删除该数据吗？'))return false;";

                    System.Web.UI.WebControls.Image image3 = new System.Web.UI.WebControls.Image();
                    image3.Style.Add("margin", "0");
                    image3.ImageUrl = "images/del.gif";
                    objDel.Text = "删除";
                    grid.Rows[i].Cells[12].Controls.AddAt(0, image3);

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

        #region 获得调查问卷信息
        /// <summary>
        /// 获得调查问卷信息
        /// </summary>
        private DataSet GetData(string UserID)
        {
            DataSet ds = new SurveyTableQuery().GetSurveyInfo(SurveyName, State, Active, ClassID, BeginDate, EndDate, viewpage1.CurrentPageIndex, viewpage1.PageSize, UserID);
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
        private string SurveyName
        {
            set { ViewState["SurveyName"] = value; }
            get
            {
                if (ViewState["SurveyName"] != null)
                {
                    return ViewState["SurveyName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        #region 类型判断
        public String ConvertString(object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;
            return obj.ToString().Trim();
        }
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ConvertInt(object obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return 0;
            if (Equals(obj.ToString().Trim(), String.Empty))
                return 0;
            int i;
            if (Int32.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }
        #endregion

        /// <summary>
        /// 查询条件：编辑状态
        /// </summary>
        private int State
        {
            set { ViewState["State"] = value; }
            get
            {
                if (ViewState["State"] != null)
                {
                    return NDConvert.ToInt32(ViewState["State"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：活动状态
        /// </summary>
        private int Active
        {
            set { ViewState["Active"] = value; }
            get
            {
                if (ViewState["Active"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Active"].ToString());
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
        private int ClassID
        {
            set { ViewState["ClassID"] = value; }
            get
            {
                if (ViewState["ClassID"] != null)
                {
                    return NDConvert.ToInt32(ViewState["ClassID"]);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：开始时间
        /// </summary>
        private DateTime BeginDate
        {
            set { ViewState["BeginDate"] = value; }
            get
            {
                if (ViewState["BeginDate"] != null)
                {
                    return NDConvert.ToDateTime(ViewState["BeginDate"]);
                }
                else
                {
                    return NDConvert.ToDateTime("1900-1-1");
                }
            }
        }

        /// <summary>
        /// 查询条件：结束时间
        /// </summary>
        private DateTime EndDate
        {
            set { ViewState["EndDate"] = value; }
            get
            {
                if (ViewState["EndDate"] != null)
                {
                    return NDConvert.ToDateTime(ViewState["EndDate"]);
                }
                else
                {
                    return NDConvert.ToDateTime("2999-12-31");
                }
            }
        }

        #endregion
    }
}