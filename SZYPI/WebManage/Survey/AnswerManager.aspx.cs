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
using System.Data.SqlClient;
using DBAccess.Entity;
using System.Text;

namespace WebManage.Survey
{
    public partial class AnswerManager : BasePage
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
            int count = 0;
            int num = 0;
            string AnswerGuid = "";
            string ApprovalStaus = "0";
            string par = "";
            string SID;
            string UID = "";
            string AnswerUserKind = "";
            SID = (Request.QueryString["SID"]).ToString();
            switch (e.Item.Value)
            {
                case "Delete":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            count++;
                            AnswerGuid = ConvertString(grid.DataKeys[i]["AnswerGUID"]);
                            //new AnswerManager_Layer().DeleteAnswer(AnswerGuid);
                            int ReturnNum = new AnswerManager_Layer().SetApprovalStaus("3", AnswerGuid);
                            new AnswerManager_Layer().UpdateAnswerNum(ReturnNum, ID.ToString());

                            num++;
                        }
                    }
                    new AnswerManager_Layer().UpdateAnswerNum(num, SID);
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    PageHelper.ShowMessage("删除成功！");
                    break;
                case "Pass":
                    num = 0;
                    try
                    {
                        par = ConvertString(new AnswerManager_Layer().GetSparBySID(SID).Rows[0]["Par"]);
                    }
                    catch (Exception ex)
                    {
                    }

                    if (par.IndexOf("|NeedConfirm:1") > 0)
                    {
                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            UID = ConvertString(grid.DataKeys[i]["UID"]);
                            AnswerUserKind = ConvertString(grid.DataKeys[i]["AnswerUserKind"]);
                            CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                            if (chkItem != null && chkItem.Checked == true)
                            {
                                count++;
                                AnswerGuid = ConvertString(grid.DataKeys[i]["AnswerGUID"]);
                                ApprovalStaus = ConvertString(grid.DataKeys[i]["ApprovalStaus"]);
                                if (ApprovalStaus == "0")
                                {
                                    new AnswerManager_Layer().SetApprovalStaus("1", AnswerGuid);
                                    if (AnswerUserKind != "1" && AnswerUserKind != "2")
                                    {
                                        string TabelName = null;
                                        string UserGuid = null; //会员GUID
                                        string prefix = new Survey_ss_Layer().prefix;
                                        int integral = 0; //积分
                                        UserGuid = ConvertString(new Survey_ss_Layer().GetUserGUID(UID.ToString()).Rows[0]["id"]);
                                        integral = int.Parse(ConvertString(new Survey_ss_Layer().GetSIntegral(SID).Rows[0]["Point"]));
                                        string sqlUpdatePoint = "";
                                        SqlDataReader dr = new Survey_ss_Layer().GetHuiYuan_Point(UserGuid);


                                        TabelName = " " + prefix + "HuiYuan_Point ";

                                        if (!dr.Read())
                                        {
                                            sqlUpdatePoint = "Insert into" + TabelName + "(HuiYuanGuid,TotalPoint,RemainPoint,Status) values(@HuiYuanGuid,@integral,@integral,1)";
                                        }
                                        else
                                        {
                                            sqlUpdatePoint = "Update" + TabelName + "set TotalPoint=TotalPoint+@integral,RemainPoint=RemainPoint+@integral where HuiYuanGuid=@HuiYuanGuid";
                                        }

                                        SqlParameter[] parameters = new SqlParameter[2];
                                        parameters[0] = new SqlParameter("@integral", integral);
                                        parameters[1] = new SqlParameter("@HuiYuanGuid", UserGuid);

                                        new AnswerManager_Layer().ExcuteSql(sqlUpdatePoint, parameters);

                                    }
                                    num++;

                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        if (count == this.grid.Rows.Count)
                        {
                            viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                        }
                        BindGridView();
                        if (num == 0)
                        {
                            PageHelper.ShowMessage("所选项目不可审核！");
                        }
                        else
                        {
                            PageHelper.ShowMessage("审批通过成功！");
                        }
                    }
                    else
                    {
                        if (count == this.grid.Rows.Count)
                        {
                            viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                        }
                        BindGridView();

                        PageHelper.ShowMessage("此份问卷答卷无需审批！");
                    }

                    break;

                case "Invalid":
                    num = 0;
                    AnswerGuid = "";
                    try
                    {
                        par = ConvertString(new AnswerManager_Layer().GetSparBySID(SID).Rows[0]["Par"]);
                    }
                    catch (Exception ex)
                    {
                    }

                    if (par.IndexOf("|NeedConfirm:1") > 0)
                    {
                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                            if (chkItem != null && chkItem.Checked == true)
                            {
                                count++;
                                AnswerGuid = ConvertString(grid.DataKeys[i]["AnswerGUID"]);
                                ApprovalStaus = ConvertString(grid.DataKeys[i]["ApprovalStaus"]);

                                if (ApprovalStaus == "0")
                                {
                                    int ReturnNum=new AnswerManager_Layer().SetApprovalStaus("2", AnswerGuid);
                                    new AnswerManager_Layer().UpdateAnswerNum(ReturnNum, ID.ToString());
                                    num++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        if (count == this.grid.Rows.Count)
                        {
                            viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                        }
                        BindGridView();

                        if (num == 0)
                        {
                            PageHelper.ShowMessage("所选项目不可审核！");
                        }
                        else
                        {
                            PageHelper.ShowMessage("审批作废成功！");
                        }
                    }
                    else
                    {
                        if (count == this.grid.Rows.Count)
                        {
                            viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                        }
                        BindGridView();

                        PageHelper.ShowMessage("此份问卷答卷无需审批！");
                    }
                    break;


            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定答卷类型数据源
        /// 作者：韩亮
        /// 时间：20100928
        /// </summary>
        //private void BindData()
        //{
        //    DataTable dt = new SurveyTableQuery().GetClassID();

        //    ddlSurveyClass.DataSource = dt;
        //    ddlSurveyClass.DataValueField = "CID";
        //    ddlSurveyClass.DataTextField = "SurveyClassName";
        //    ddlSurveyClass.DataBind();
        //}
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

            this.AnswerName = answerName.Text.Trim();
            if (!string.IsNullOrEmpty(minRecord.Text.Trim()))
            {
                this.MinAnswerRecord = NDConvert.ToInt32(minRecord.Text.Trim());
            }
            else
            {
                this.MinAnswerRecord = -1;
            }
            if (!string.IsNullOrEmpty(maxRecord.Text.Trim()))
            {
                this.MaxAnswerRecord = NDConvert.ToInt32(maxRecord.Text.Trim());
            }
            else
            {
                this.MaxAnswerRecord = -1;
            }
            if (!string.IsNullOrEmpty(minTime.Text.Trim()))
            {
                this.MinUseTime = NDConvert.ToInt32(minTime.Text.Trim());
            }
            else
            {
                this.MinUseTime = -1;
            }
            if (!string.IsNullOrEmpty(maxTime.Text.Trim()))
            {
                this.MaxUseTime = NDConvert.ToInt32(maxTime.Text.Trim());
            }
            else
            {
                this.MaxUseTime = -1;
            }
            if (!string.IsNullOrEmpty(approvalStaus.SelectedValue))
            {
                this.ApprovalStaus = NDConvert.ToInt32(approvalStaus.SelectedValue);
            }
            else
            {
                this.ApprovalStaus = -1;
            }
            this.BeginDate = NDConvert.ToDateTime(startDate.Value);
            if (NDConvert.ToDateTime(endDate.Value) == NDConvert.ToDateTime("1900-1-1"))
            {
                this.EndDate = NDConvert.ToDateTime("2999-12-31");
            }
            else
            {
                this.EndDate = NDConvert.ToDateTime(endDate.Value);
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
            int count = 0;
            switch (e.CommandName)
            {
                case "SurveyName":
                    string urlAnswer = string.Format("ClientPs.aspx?SID={0}&AnswerGUID={1}", ID, ConvertString(grid.DataKeys[rowIndex]["AnswerGUID"]));
                    PageHelper.WriteScript(string.Format("window.open('{0}');", urlAnswer));
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    break;
                case "TempPage":

                    BindGridView();
                    break;
                case "Statistics":

                    BindGridView();
                    break;
                case "Options":

                    BindGridView();
                    break;
                case "Editer":

                    BindGridView();
                    break;
                case "DeleteAnswer":
                    //int count = 0;
                    string AnswerGuid = ConvertString(grid.DataKeys[rowIndex]["AnswerGUID"]);
                    //new AnswerManager_Layer().DeleteAnswer(AnswerGuid); //彻底删除答卷
                    int ReturnNum = new AnswerManager_Layer().SetApprovalStaus("3", AnswerGuid);
                    new AnswerManager_Layer().UpdateAnswerNum(ReturnNum, ID.ToString());
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
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
            BindApprovalStaus();
            BindGridView();
        }
        #endregion

        #region 初始设置下来控件值
        private void BindApprovalStaus()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Name");
            DataColumn dc1 = new DataColumn("Value");
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr0 = dt.NewRow();
            dr0["Name"] = "-请选择-";

            DataRow dr = dt.NewRow();
            dr["Name"] = "待审核";
            dr["Value"] = dr["Value"] = (int)CommonEnum.ApprovalStaus.Pending;

            DataRow dr1 = dt.NewRow();
            dr1["Name"] = "通过";
            dr1["Value"] = (int)CommonEnum.ApprovalStaus.Pass;

            dt.Rows.Add(dr0);
            dt.Rows.Add(dr);
            dt.Rows.Add(dr1);

            approvalStaus.DataSource = dt;
            approvalStaus.DataValueField = "Value";
            approvalStaus.DataTextField = "Name";
            approvalStaus.DataBind();
        }
        #endregion

        #region 邦定GridView
        /// <summary>
        /// 邦定GridView
        /// </summary>
        private void BindGridView()
        {
            if (Request.QueryString["SID"] != null)
            {
                string SID = (Request.QueryString["SID"]).ToString();
                long UserID = NDConvert.ToInt64(Session["UserID"]); //当前管理操作的中

                DataSet ds = GetData(SID);
                DataTable dt = ds.Tables[1];

                grid.DataSource = dt;
                grid.DataBind();

                BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));

                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    long ID = NDConvert.ToInt64(grid.DataKeys[i]["SID"].ToString());
                    DataTable GetSurveyInfo = new SurveyManage_Layer().GetSurveyTable(ID.ToString(), UserID.ToString());

                    //问卷名称
                    LinkButton surveyName = (LinkButton)grid.Rows[i].Cells[1].Controls[0];
                    if (ConvertString(grid.DataKeys[i]["SurveyName"]).Length > 8)
                    {
                        surveyName.Text = ConvertString(grid.DataKeys[i]["SurveyName"]).Substring(0, 7);
                    }
                    else
                    {
                        surveyName.Text = ConvertString(grid.DataKeys[i]["SurveyName"]);
                    }
                    surveyName.ToolTip = ConvertString(grid.DataKeys[i]["SurveyName"]);


                    //答题者
                    //LinkButton objName = (LinkButton)grid.Rows[i].Cells[2].Controls[0];

                    //if (grid.DataKeys[i]["AnswerUserKind"] == null || ConvertString(grid.DataKeys[i]["AnswerUserKind"])=="0")
                    //{
                    grid.Rows[i].Cells[2].Text = ConvertString(grid.DataKeys[i]["UserName"]);
                    //}
                    //else
                    //{
                    //    if (ConvertString(grid.DataKeys[i]["AnswerUserKind"]) == "2") 
                    //    {
                    //        grid.Rows[i].Cells[2].Text = "匿名";
                    //    }
                    //    else
                    //    {
                    //        grid.Rows[i].Cells[2].Text = "后台(" + new AnswerManager_Layer().GetManager(ConvertString(grid.DataKeys[i]["UID"]))+")";
                    //    }

                    //}


                    //审批状态
                    LinkButton objEdit = (LinkButton)grid.Rows[i].Cells[7].Controls[0];
                    if (dt.Rows[0]["SPar"].ToString().IndexOf("|NeedConfirm:1") < 0)
                    {
                        grid.Rows[i].Cells[7].Text = "无需审核";
                    }
                    else if (ConvertString(grid.DataKeys[i]["ApprovalStaus"]) == ConvertString((int)CommonEnum.ApprovalStaus.Pending))
                    {
                        grid.Rows[i].Cells[7].Text = "待审批";
                        grid.Rows[i].Cells[7].ForeColor = Color.Red;
                    }
                    else if (this.ConvertString(grid.DataKeys[i]["ApprovalStaus"]) == ConvertString((int)CommonEnum.ApprovalStaus.Pass))
                    {
                        grid.Rows[i].Cells[7].Text = "已通过";
                        grid.Rows[i].Cells[7].ForeColor = Color.Green;

                    }

                    //删除
                    LinkButton objDel = (LinkButton)grid.Rows[i].Cells[8].Controls[0];
                    objDel.OnClientClick = "if(!window.confirm('确定要删除该数据吗？'))return false;";

                    System.Web.UI.WebControls.Image image3 = new System.Web.UI.WebControls.Image();
                    image3.Style.Add("margin", "0");
                    image3.ImageUrl = "images/del.gif";
                    objDel.Text = "删除";
                    grid.Rows[i].Cells[8].Controls.AddAt(0, image3);

                }
            }
            else
            {

            }

        }

        #endregion

        #region 类型判断
        public String ConvertString(object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;
            return obj.ToString().Trim();
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

        #region 获得调查答卷信息
        /// <summary>
        /// 获得调查问卷信息
        /// </summary>
        private DataSet GetData(string SID)
        {
            DataSet ds = new AnswerManager_Layer().GetAnswerInfoBySID(SID, AnswerName, MinAnswerRecord, MaxAnswerRecord, MinUseTime, MaxUseTime, ApprovalStaus, BeginDate, viewpage1.CurrentPageIndex, viewpage1.PageSize, EndDate);
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
        private string AnswerName
        {
            set { ViewState["AnswerName"] = value; }
            get
            {
                if (ViewState["AnswerName"] != null)
                {
                    return ViewState["AnswerName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 查询条件：答卷得分（最小得分）
        /// </summary>
        private int MinAnswerRecord
        {
            set { ViewState["MinAnswerRecord"] = value; }
            get
            {
                if (ViewState["MinAnswerRecord"] != null)
                {
                    return NDConvert.ToInt32(ViewState["MinAnswerRecord"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：答卷得分（最da得分）
        /// </summary>
        private int MaxAnswerRecord
        {
            set { ViewState["MaxAnswerRecord"] = value; }
            get
            {
                if (ViewState["MaxAnswerRecord"] != null)
                {
                    return NDConvert.ToInt32(ViewState["MaxAnswerRecord"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：答卷用时（选择最小用时）
        /// </summary>
        private int MinUseTime
        {
            set { ViewState["MinUseTime"] = value; }
            get
            {
                if (ViewState["MinUseTime"] != null)
                {
                    return NDConvert.ToInt32(ViewState["MinUseTime"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：答卷用时（选择最大用时）
        /// </summary>
        private int MaxUseTime
        {
            set { ViewState["MaxUseTime"] = value; }
            get
            {
                if (ViewState["MaxUseTime"] != null)
                {
                    return NDConvert.ToInt32(ViewState["MaxUseTime"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 查询条件：答卷审核状态
        /// </summary>
        private int ApprovalStaus
        {
            set { ViewState["ApprovalStaus"] = value; }
            get
            {
                if (ViewState["ApprovalStaus"] != null)
                {
                    return NDConvert.ToInt32(ViewState["ApprovalStaus"]);
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

        #region 截取字符串方法（中英区分）
        /// <summary>   
        /// 截取字符串长度   
        /// </summary>   
        /// <param name="input">要截取的字符串对象</param>   
        /// <param name="length">要保留的字符个数</param>   
        /// <param name="suffix">后缀(用以替换超出长度部分)</param>   
        /// <returns></returns>   
        public static string MySubString(string input, int length, string suffix)
        {
            Encoding encode = Encoding.GetEncoding("gb2312");
            byte[] byteArr = encode.GetBytes(input);
            if (byteArr.Length <= length) return input;

            int m = 0, n = 0;
            foreach (byte b in byteArr)
            {
                if (n >= length) break;
                if (b > 127) m++; //重要一步：对前p个字节中的值大于127的字符进行统计   
                n++;
            }
            if (m % 2 != 0) n = length + 1; //如果非偶：则说明末尾为双字节字符，截取位数加1   

            return encode.GetString(byteArr, 0, n) + suffix;
        }  
        #endregion
    }
}
