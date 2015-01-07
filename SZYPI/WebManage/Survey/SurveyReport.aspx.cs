using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BusinessLayer.Survey;
using Business.Helper;
using BLL.Entity;
using Web_Survey.PublicMethod;
using BusinessLayer.Chart;

namespace WebManage.Survey
{
    public partial class SurveyReport : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            InitPage();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitPage()
        {
            //SID = "1";
            string SurveryName = "";
            long ItemToTalChoosed = 0;
            long nowRentCount = 0;
            SID = ConvertHelper.ConvertString(Request.QueryString["SID"]);

            DataTable dtChooseOption = new WebChart_Layer().GetChoosedOption(SID, ""); ; //获取选择项
            DataTable SurveyItems = new WebChart_Layer().GetAllSurveyTtem(ConvertHelper.ConvertString(SID)); //获取问卷的所有题目
            DataTable dtSurveyTable = new WebChart_Layer().GetSurveyTableName(ConvertHelper.ConvertString(SID)); //获取问卷

            if (dtSurveyTable.Rows.Count == 1)
            {
                SurveryName = ConvertHelper.ConvertString(dtSurveyTable.Rows[0]["SurveyName"]);
            }

            //创建问卷报告表
            HtmlGenericControl divSurveyReportOut = new HtmlGenericControl("div");
            HtmlGenericControl divSurveyReport = new HtmlGenericControl("div");
            divSurveyReport.Attributes.Add("class", "surveyReport");
            divSurveyReport.Style.Add("margin-top", "10px");
            divSurveyReport.Style.Add("margin-left", "5px");
            divSurveyReport.Attributes.Add("align", "Left");

            divSurveyReportOut.InnerHtml += "<div  class=\"OutTitle\" >" + SurveryName+" -- 综合分析报告" + "</div>";
            divSurveyReport.InnerHtml += "<div style=\"width:800px\" class=\"TopDivTitle\" >" + SurveryName + "</div>";
            divSurveyReport = ShowItemTableValue(divSurveyReport, SID.ToString());

            divSurveyReportOut.Controls.Add(divSurveyReport);
            Page.Form.Controls.Add(divSurveyReportOut);  //添加到页面控件

            HtmlGenericControl divItemsOut = new HtmlGenericControl("div");
            divItemsOut.InnerHtml += "<div  class=\"OutTitle\" >" +"结果分析" + "</div>";
            //创建问卷选项表

            if (SurveyItems.Rows.Count > 0)
            {
                foreach (DataRow drSurveyItem in SurveyItems.Rows)
                {
                    string IID = ConvertHelper.ConvertString(drSurveyItem["IID"]);  //获取选项ID
                    //获取所有选择次数 
                    if (!string.IsNullOrEmpty(IID))
                    {
                        HtmlGenericControl divTableTitle = new HtmlGenericControl("div");
                        divTableTitle.Attributes.Add("class", "surveyReport");
                        divTableTitle.Style.Add("margin-top", "10px");
                        divTableTitle.Style.Add("margin-left", "5px");
                        divTableTitle.Attributes.Add("align", "Left");

                        divTableTitle.InnerHtml += "<div style=\"width:800px\" class=\"TopDivTitle\" >" + ConvertHelper.ConvertString(drSurveyItem["ItemName"]) + "</div>";
                        divTableTitle.InnerHtml += "<div style=\"width:630px\" class=\"AnalysisTitle\" >" + "频率分析"+ "</div>";

                        HtmlGenericControl divItems = new HtmlGenericControl("div");
                        DataRow[] dtItemChoosedRows = dtChooseOption.Select(string.Format("IID={0}", IID));
                        if (dtItemChoosedRows != null)
                        {
                            for (int i = 0; i < dtItemChoosedRows.Length; i++)
                            {
                                ItemToTalChoosed += ConvertHelper.ConvertLong(dtItemChoosedRows[i]["CountItemValue"]);
                            }
                        }

                        DataTable dtItemOption = new WebChart_Layer().GetTtemOption(SID, IID);
                        divItems = ShowItemTable(divItems, dtItemOption, dtChooseOption, ItemToTalChoosed,IID);

                        divTableTitle.Controls.Add(divItems);
                        divItemsOut.Controls.Add(divTableTitle);
                        ItemToTalChoosed = 0;
                    }

                }
            }
            Page.Form.Controls.Add(divItemsOut);  //添加到页面控件
        }

        /// <summary>
        /// 创建问卷选项表
        /// </summary>
        /// <param name="div"></param>
        /// <param name="dtItemOption"></param>
        /// <param name="dtChooseOption"></param>
        /// <param name="ItemToTalChoosed"></param>
        /// <returns></returns>
        public HtmlGenericControl ShowItemTable(HtmlGenericControl div, DataTable dtItemOption, DataTable dtChooseOption, long ItemToTalChoosed,string IID)
        {
            Table tableItemOption = new Table();
            tableItemOption.CssClass = "OptionTable";
            TableRow tr1 = new TableRow();
            //TableCell tr1tc1 = new TableCell();
            TableHeaderCell tr1tc0 = new TableHeaderCell();
            tr1tc0.Text = " ";
            tr1tc0.Width = 50;
            tr1.Cells.Add(tr1tc0);

            TableHeaderCell tr1tc1 = new TableHeaderCell();
            tr1tc1.Text = "备选项";
            tr1tc1.Width = 200;
            tr1.Cells.Add(tr1tc1);

            TableHeaderCell tr1tc2 = new TableHeaderCell();
            tr1tc2.Text = "频数";
            tr1tc2.Width = 50;
            tr1.Cells.Add(tr1tc2);

            TableHeaderCell tr1tc3 = new TableHeaderCell();
            tr1tc3.Text = "频率";
            tr1tc3.Width = 120;
            tr1.Cells.Add(tr1tc3);
            tableItemOption.Rows.Add(tr1);

            TableHeaderCell tr1tc4 = new TableHeaderCell();
            tr1tc4.Text = " ";
            tr1tc4.Width = 210;
            tr1.Cells.Add(tr1tc4);
            tableItemOption.Rows.Add(tr1);

            if (dtItemOption.Rows.Count > 0)
            {
                for (int i = 0; i < dtItemOption.Rows.Count; i++)
                {
                    long frequence = GetFrequence(dtChooseOption, ConvertHelper.ConvertString(dtItemOption.Rows[i]["OID"]),IID);
                    TableRow tr = new TableRow();

                    TableCell tc0 = new TableCell();
                    tc0.Style.Add("text-align", "right");
                    tc0.Text =ConvertHelper.ConvertString( i + 1);
                    tr.Cells.Add(tc0);

                    TableCell tc1 = new TableCell();
                    tc1.Style.Add("text-align", "left");
                    tc1.Text = ConvertHelper.ConvertString(dtItemOption.Rows[i]["OptionName"]);
                    tr.Cells.Add(tc1);

                    TableCell tc2 = new TableCell();
                    tc2.Text = ConvertHelper.ConvertString(frequence);
                    tr.Cells.Add(tc2);

                    TableCell tc3 = new TableCell();
                    tc3.Text = String.Format("{0:F}", (double)frequence * 100 / ItemToTalChoosed)+"%";
                    tr.Cells.Add(tc3);

                    tableItemOption.Rows.Add(tr);
                }
            }


            div.Controls.Add(tableItemOption);
            return div;
        }

        /// <summary>
        /// 获取频数
        /// </summary>
        /// <returns></returns>
        public long GetFrequence(DataTable dtChooseOption, string ItemValue, string IID)
        {
            long frequence = 0;

            DataRow[] dtItemChoosedRows = dtChooseOption.Select(string.Format("IID={0} and ItemValue={1}", IID, ItemValue));
            if (dtItemChoosedRows.Length == 1)
            {
                frequence = ConvertHelper.ConvertLong(dtItemChoosedRows[0]["CountItemValue"]);
            }
            else
            {

            }
            return frequence;
        }

        /// <summary>
        /// 创建问卷报告表
        /// </summary>
        /// <param name="div"></param>
        /// <param name="SID">问卷ID</param>
        /// <returns></returns>
        public HtmlGenericControl ShowItemTableValue(HtmlGenericControl div, string SID)
        {
            DataTable dtGetSurveyTableInfo = new SurveyReport_Layer().GetSurveyTableInfo(SID);  //获取SurveyTable表的信息
            DataTable dtGetAnswerInfoStatistics = new SurveyReport_Layer().GetAnswerInfoStatistics(SID); //获取AnswerInfoStatistics的统计信息 
            DataTable dtGetPageTable = new SurveyReport_Layer().GetPageTable(SID);  //获取PageTable统计信息
            DataTable dtGetItemTable = new SurveyReport_Layer().GetItemTable(SID);  //获取ItemTable统计信息

            string startDate = null;
            string endDate = null;
            string surveyPeriod = null;  //调研周期
            string surveyProgress = null;  //调研进度
            long maxAnswerAmount = 0;
            int itemNum = 0;
            int pageNum = 0;
            long maxTime = 0;
            long minTime = 0;
            long sumTime = 0;
            long avgTime = 0;

            if (dtGetSurveyTableInfo.Rows.Count > 0)
            {
                startDate = ConvertHelper.ConvertString(dtGetSurveyTableInfo.Rows[0]["CreateDate"]);
                endDate = ConvertHelper.ConvertString(dtGetSurveyTableInfo.Rows[0]["EndDate"]);
                maxAnswerAmount = ConvertHelper.ConvertLong(dtGetSurveyTableInfo.Rows[0]["MaxAnswerAmount"]);
                if (!string.IsNullOrEmpty(endDate))
                {
                    surveyPeriod = (ConvertHelper.ConvertDateTime(dtGetSurveyTableInfo.Rows[0]["CreateDate"]) - ConvertHelper.ConvertDateTime(dtGetSurveyTableInfo.Rows[0]["EndDate"])).Days + " 天";
                }
                else
                {
                    endDate = "未设置";
                    surveyPeriod = "天";
                }
                if (maxAnswerAmount <= 0)
                {
                    surveyProgress = ConvertHelper.ConvertString(dtGetSurveyTableInfo.Rows[0]["AnswerAmount"]) + "/不限制";
                }
                else
                {
                    surveyProgress = ConvertHelper.ConvertString(dtGetSurveyTableInfo.Rows[0]["AnswerAmount"]) + "/" + maxAnswerAmount;
                }
            }
            itemNum = dtGetItemTable.Rows.Count;
            pageNum = dtGetPageTable.Rows.Count;
            if (dtGetAnswerInfoStatistics.Rows.Count > 0)
            {
                maxTime = ConvertHelper.ConvertLong(dtGetAnswerInfoStatistics.Rows[0]["MaxSecondTime"]);
                minTime = ConvertHelper.ConvertLong(dtGetAnswerInfoStatistics.Rows[0]["MinSecondTime"]);
                sumTime = ConvertHelper.ConvertLong(dtGetAnswerInfoStatistics.Rows[0]["SumSecondTime"]);
                avgTime = ConvertHelper.ConvertLong(dtGetAnswerInfoStatistics.Rows[0]["AvgSecondTime"]);
            }


            #region 创建表

            TableAddEntity TableAdd = new TableAddEntity();
            TableAdd.TableCssCalss = "surveyReportTable";
            List<TableAddRowEntity> TableAddRows = new List<TableAddRowEntity>();

            //创建开始日期
            TableAddRowEntity TableAddRowStartDate = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsStartDate = new List<TableAddCellEntity>();
            TableAddCellEntity TableAddCellStartDate = new TableAddCellEntity();
            TableAddCellStartDate.TableCellWidth = Unit.Point(280);
            TableAddCellStartDate.TableCellHorizontalAlign = HorizontalAlign.Left;
            TableAddCellStartDate.TableCellContent = "开始日期：";
            TableAddCellEntity TableAddCellStartDateValue = new TableAddCellEntity();
            TableAddCellStartDateValue.TableCellWidth = Unit.Point(420);
            TableAddCellStartDateValue.TableCellHorizontalAlign = HorizontalAlign.Left;
            TableAddCellStartDateValue.TableCellContent = startDate;
            TableAddCellsStartDate.Add(TableAddCellStartDate);
            TableAddCellsStartDate.Add(TableAddCellStartDateValue);
            TableAddRowStartDate.TableAddCells = TableAddCellsStartDate;

            //创建结束日期
            TableAddRowEntity TableAddRowEndDate = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsEndDate = new List<TableAddCellEntity>();
            TableAddCellEntity TableAddCellEndDate = new TableAddCellEntity();
            TableAddCellEndDate.TableCellContent = "结束日期：";
            TableAddCellEntity TableAddCellEndDateValue = new TableAddCellEntity();
            TableAddCellEndDateValue.TableCellContent = endDate;
            TableAddCellsEndDate.Add(TableAddCellEndDate);
            TableAddCellsEndDate.Add(TableAddCellEndDateValue);
            TableAddRowEndDate.TableAddCells = TableAddCellsEndDate;

            //创建调研周期
            TableAddRowEntity TableAddRowSurveyPeriod = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsSurveyPeriod = new List<TableAddCellEntity>();
            TableAddCellEntity TableAddCellSurveyPeriod = new TableAddCellEntity();
            TableAddCellSurveyPeriod.TableCellContent = "调研周期：";
            TableAddCellEntity TableAddCellSurveyPeriodValue = new TableAddCellEntity();
            TableAddCellSurveyPeriodValue.TableCellContent = surveyPeriod;
            TableAddCellsSurveyPeriod.Add(TableAddCellSurveyPeriod);
            TableAddCellsSurveyPeriod.Add(TableAddCellSurveyPeriodValue);
            TableAddRowSurveyPeriod.TableAddCells = TableAddCellsSurveyPeriod;

            //创建调研进度
            TableAddRowEntity TableAddRowSurveyProgress = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsSurveyProgress = new List<TableAddCellEntity>();

            TableAddCellEntity TableAddCellSurveyProgress = new TableAddCellEntity();
            TableAddCellSurveyProgress.TableCellContent = "调研进度：";

            TableAddCellEntity TableAddCellSurveyProgressValue = new TableAddCellEntity();
            TableAddCellSurveyProgressValue.TableCellContent = surveyProgress;
            TableAddCellsSurveyProgress.Add(TableAddCellSurveyProgress);
            TableAddCellsSurveyProgress.Add(TableAddCellSurveyProgressValue);
            TableAddRowSurveyProgress.TableAddCells = TableAddCellsSurveyProgress;

            //创建题目数
            TableAddRowEntity TableAddRowItemNum = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsItemNum = new List<TableAddCellEntity>();

            TableAddCellEntity TableAddCellItemNum = new TableAddCellEntity();
            TableAddCellItemNum.TableCellContent = "题目数/页数：";

            TableAddCellEntity TableAddCellItemNumValue = new TableAddCellEntity();
            TableAddCellItemNumValue.TableCellContent = itemNum + "/" + pageNum;
            TableAddCellsItemNum.Add(TableAddCellItemNum);
            TableAddCellsItemNum.Add(TableAddCellItemNumValue);
            TableAddRowItemNum.TableAddCells = TableAddCellsItemNum;

            //创建答卷用时
            TableAddRowEntity TableAddRowAnswerTime = new TableAddRowEntity();
            List<TableAddCellEntity> TableAddCellsAnswerTime = new List<TableAddCellEntity>();

            TableAddCellEntity TableAddCellAnswerTime = new TableAddCellEntity();
            TableAddCellAnswerTime.TableCellContent = "答卷用时（秒）：";

            TableAddCellEntity TableAddCellAnswerTimeValue = new TableAddCellEntity();
            TableAddCellAnswerTimeValue.TableCellContent = "最多用时:" + maxTime + " 最少用时:" + minTime + " 用时总计:" + sumTime + " 平均用时:" + avgTime;
            TableAddCellsAnswerTime.Add(TableAddCellAnswerTime);
            TableAddCellsAnswerTime.Add(TableAddCellAnswerTimeValue);
            TableAddRowAnswerTime.TableAddCells = TableAddCellsAnswerTime;



            TableAddRows.Add(TableAddRowStartDate);
            TableAddRows.Add(TableAddRowEndDate);
            TableAddRows.Add(TableAddRowSurveyPeriod);
            TableAddRows.Add(TableAddRowSurveyProgress);
            TableAddRows.Add(TableAddRowItemNum);
            TableAddRows.Add(TableAddRowAnswerTime);

            TableAdd.TableAddRows = TableAddRows;

            Table table1 = new PublicClass().AddTable(TableAdd);

            div.Controls.Add(table1);

            #endregion

            return div;
        }
        #region 属性

        /// <summary>
        /// 问卷ID
        /// </summary>
        private string SID
        {
            set { ViewState["SID"] = value; }
            get
            {
                if (ViewState["SID"] != null)
                {
                    return ViewState["SID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion
    }
}
