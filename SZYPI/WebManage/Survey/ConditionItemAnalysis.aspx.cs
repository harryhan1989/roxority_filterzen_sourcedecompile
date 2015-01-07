using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Helper;
using System.Data;
using BusinessLayer.Chart;
using System.Text;
using System.Web.UI.HtmlControls;
using Dundas.Charting.WebControl;
using Web_Survey.WebChart;
using Business.Safety;

namespace WebManage.Survey
{
    public partial class ConditionItemAnalysis : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            Anthem.Manager.Register(this);
            InitPage();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {            

        }

        public void InitPage()
        {
            string QueryCondition = ConvertHelper.ConvertString(Request.Form["QueryCondition"]);
            string chartType=ConvertHelper.ConvertString(Request.Form["chart"]);
            
            this.SID = ConvertHelper.ConvertString(Request.Form["SID"]);
            this.IID = ConvertHelper.ConvertString(Request.Form["ResultItem"]);
            SIDRecord.Value = SID;
            IIDRecord.Value = IID;

            string ConditionSql = ConvertHelper.ConvertString(Request.QueryString["Condition"]);
            string TableName = "";
            long ItemToTalChoosed = 0;
            if (SID == "" || IID == "")
            {
                return;
            }

            this.sql = Getsql(QueryCondition);

            DataTable dtItemOption = new WebChart_Layer().GetTtemOption(SID, IID);

            dtTableName = new WebChart_Layer().GetSurveyTableName(SID);
            dtChooseOption = new WebChart_Layer().GetChoosedOption(SID, sql);

            if (dtTableName.Rows.Count > 0)
            {
                TableName = ConvertHelper.ConvertString(dtTableName.Rows[0]["SurveyName"]);
            }

            //获取所有选择次数 
            DataRow[] dtItemChoosedRows = dtChooseOption.Select(string.Format("IID={0}", IID));
            if (dtItemChoosedRows != null)
            {
                for (int i = 0; i < dtItemChoosedRows.Length; i++)
                {
                    ItemToTalChoosed += ConvertHelper.ConvertLong(dtItemChoosedRows[i]["CountItemValue"]);
                }
            }

            HtmlGenericControl divItemTable = new HtmlGenericControl("div");
            //divItemTable.Style.Add("width", "700px");
            divItemTable.Style.Add("margin-top", "10px");
            divItemTable.Style.Add("margin-left", "5px");
            divItemTable.Attributes.Add("align", "Left");

            HtmlGenericControl divItemTableValue = new HtmlGenericControl("div");
            //divItemTableValue.Style.Add("width", "700px");
            divItemTableValue.Style.Add("margin-top", "10px");
            divItemTableValue.Style.Add("margin-left", "5px");
            divItemTableValue.Attributes.Add("align", "Left");


            divItemTable.InnerHtml = "<div style=\"width:700px\" class=\"TableTitle\" >" + TableName + "</div>";
            divItemTable = ShowItemTable(divItemTable, dtItemOption, dtChooseOption, ItemToTalChoosed);

            divItemTableValue.InnerHtml = "<div style=\"width:700px\" class=\"TableTitle\" >" + "分值分析" + "</div>";
            divItemTableValue = ShowItemTableValue(divItemTableValue, dtItemOption, dtChooseOption, ItemToTalChoosed);

            HtmlGenericControl divItemConclusion = new HtmlGenericControl("div");
            divItemConclusion.Style.Add("height", "100px");
            divItemConclusion.Style.Add("margin-top", "10px");
            divItemConclusion.Style.Add("margin-left", "5px");
            divItemConclusion.Attributes.Add("align", "Left");


            divItemConclusion.InnerHtml = "<div style=\"width:700px\" class=\"DivConclusion\" >" + "结论评语：" + "</div>";
            divItemConclusion = ShowItemConclusion(divItemConclusion);

            //if (!IsPostBack)
            //{
            //    Chart chart3DPie = new CreateWebChart().YFICreateChart(SID, IID, true, SeriesChartType.Pie, dtChooseOption, dtTableName); //创建3d饼图
            //    chart3DPie.Attributes.Add("id", "chart3DPie");
            //    chart3DPie.ImageUrl = "TempFiles/ChartPic_1";

            //    chartContent.Controls.Add(chart3DPie);
            //}
            if (!string.IsNullOrEmpty(chartType) && chartType != "")
            {
                SetChart(chartType);
            }

            Page.Form.Controls.Add(divItemTable);  //添加到页面控件
            //Page.Form.Controls.Add(divItemTableValue);  //添加到页面控件
            Page.Form.Controls.Add(divItemConclusion);  //添加到页面控件

        }

        [Anthem.Method]
        protected void SetChart(object chart)
        {
            string strChart = ConvertHelper.ConvertString(chart);
            if (strChart == "chart3DPie")
            {
                Chart chart3DPie = new CreateWebChart().YFICreateChart(SID, IID, true, SeriesChartType.Pie, dtChooseOption, dtTableName); //创建3d饼图
                chart3DPie.Attributes.Add("id", "chart3DPie");
                chart3DPie.ImageUrl = "TempFiles/ChartPic_1";
                chartContent.Controls.Add(chart3DPie);
                string chart3DPieHtml = "\"" + RenderHTML(chart3DPie).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "").Replace("</OBJECT>", "<object><PARAM NAME=wmode VALUE=transparent /></OBJECT>") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DPieHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DColumn")
            {
                Chart chart3DColumn = new CreateWebChart().YFICreateChart(SID, IID, true, SeriesChartType.Column, dtChooseOption, dtTableName); //创建3D柱形图
                chart3DColumn.Attributes.Add("id", "chart3DColumn");
                chart3DColumn.ImageUrl = "TempFiles/ChartPic_2";
                chartContent.Controls.Add(chart3DColumn);
                string chart3DColumnHtml = "\"" + RenderHTML(chart3DColumn).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DColumnHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartPie")
            {
                Chart chartPie = new CreateWebChart().YFICreateChart(SID, IID, false, SeriesChartType.Pie, dtChooseOption, dtTableName); //创建饼图
                chartPie.Attributes.Add("id", "chartPie");
                chartPie.ImageUrl = "TempFiles/ChartPic_3";
                chartContent.Controls.Add(chartPie);
                string chartPieHtml = "\"" + RenderHTML(chartPie).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartPieHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartColumn")
            {
                Chart chartColumn = new CreateWebChart().YFICreateChart(SID, IID, false, SeriesChartType.Column, dtChooseOption, dtTableName); //创建柱形图
                chartColumn.Attributes.Add("id", "chartColumn");
                chartColumn.ImageUrl = "TempFiles/ChartPic_4";
                chartContent.Controls.Add(chartColumn);
                string chartColumnHtml = "\"" + RenderHTML(chartColumn).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartColumnHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DDoughnut")
            {
                Chart chart3DDoughnut = new CreateWebChart().YFICreateChart(SID, IID, true, SeriesChartType.Doughnut, dtChooseOption, dtTableName); //创建3D圆环形
                chart3DDoughnut.Attributes.Add("id", "chart3DDoughnut");
                chart3DDoughnut.ImageUrl = "TempFiles/ChartPic_5";
                chartContent.Controls.Add(chart3DDoughnut);
                string chart3DDoughnutHtml = "\"" + RenderHTML(chart3DDoughnut).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DDoughnutHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartDoughnut")
            {
                Chart chartDoughnut = new CreateWebChart().YFICreateChart(SID, IID, false, SeriesChartType.Doughnut, dtChooseOption, dtTableName); //创建圆环形
                chartDoughnut.Attributes.Add("id", "chartDoughnut");
                chartDoughnut.ImageUrl = "TempFiles/ChartPic_6";
                chartContent.Controls.Add(chartDoughnut);
                string chartDoughnutHtml = "\"" + RenderHTML(chartDoughnut).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartDoughnutHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DBar")
            {
                Chart chart3DBar = new CreateWebChart().YFICreateChart(SID, IID, true, SeriesChartType.Bar, dtChooseOption, dtTableName); //创建3D横向形
                chart3DBar.Attributes.Add("id", "chart3DBar");
                chart3DBar.ImageUrl = "TempFiles/ChartPic_7";
                chartContent.Controls.Add(chart3DBar);
                string chart3DBarHtml = "\"" + RenderHTML(chart3DBar).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DBarHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartBar")
            {
                Chart chartBar = new CreateWebChart().YFICreateChart(SID, IID, false, SeriesChartType.Bar, dtChooseOption, dtTableName); //创建横向形
                chartBar.Attributes.Add("id", "chartBar");
                chartBar.ImageUrl = "TempFiles/ChartPic_8";
                chartContent.Controls.Add(chartBar);
                string chartBarHtml = "\"" + RenderHTML(chartBar).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartBarHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
        }

        //将控件转换成html
        public string RenderHTML(WebControl objWebCtrl)
        {
            try
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter html = new HtmlTextWriter(sw);

                objWebCtrl.RenderControl(html);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        //设置sql条件
        public string Getsql(string condition)
        {
            //condition格式类似 "3$0$1$16;17$4*7$0$1$40;41$4"   
            //[1]---IID
            //[2]---符号 0-等于 ，1-不等于
            //[3]---正，否
            //[4]---选项，分号相隔
            //[5]---题目类型4-单选，8--多选
            string setSql = "";
            int i = 0;
            int j = 0;

            string[] itemCondition = condition.Split('*');
            
            j=0;


            foreach (string item in itemCondition)
            {

                string[] itemElements = item.Split('$');
                if (itemElements[4] == "4")
                {
                    string[] values = itemElements[3].Split(';');
                    string sign = "";
                    setSql += "and  EXISTS ( SELECT AnswerGUID FROM UT_QS_AnswerDetail t" + (j + 1) + " WHERE t" + (j + 1) + ".AnswerGUID =t0.AnswerGUID and ";
                    setSql += "  IID=" + itemElements[0] + " AND ( ";
                    //setSql += "  IID=" + itemElements[0] + " AND ( ";
                    i = 0;
                    foreach (string value in values)
                    {
                        sign = Getsign(itemElements[1], itemElements[4], value);

                        if (i < values.Length - 1)
                        {
                            setSql += " Answer " + sign ;
                            if (itemElements[1] == "0")
                            {
                                setSql += " or ";
                            }
                            else
                            {
                                setSql += " and ";
                            }
                        }
                        else
                        {
                            setSql += " Answer " + sign ;
                        }
                        i++;
                    }
                    setSql += " ) )";
                    j++;

                }
                else if(itemElements[4] == "8")
                {
                    string[] values = itemElements[3].Split(';');
                    string sign = "";
                    setSql += "and  EXISTS ( SELECT AnswerGUID FROM UT_QS_AnswerDetail t" + (j + 1) + " WHERE t" + (j + 1) + ".AnswerGUID =t0.AnswerGUID and ";
                    setSql += "  IID=" + itemElements[0] + " AND ( ";
                    //setSql += "  IID=" + itemElements[0] + " AND ( ";
                    i = 0;

                    if (itemElements[1] == "1" || itemElements[1] == "2")
                    {
                        sign = Getsign(itemElements[1], itemElements[4], itemElements[3].Replace(";",","));
                        setSql += " Answer " + sign;
                    }
                    else
                    {
                        foreach (string value in values)
                        {
                            sign = Getsign(itemElements[1], itemElements[4], value);

                            if (i < values.Length - 1)
                            {
                                setSql += " Answer " + sign;
                                if (itemElements[1] == "0")
                                {
                                    setSql += " or ";
                                }
                                else
                                {
                                    setSql += " and ";
                                }
                            }
                            else
                            {
                                setSql += " Answer " + sign;
                            }
                            i++;
                        }
                    }


                    setSql += " ) )";
                    j++;
                }

            }

            return setSql;
        }

        public string Getsign(string signType,string itemType,string value)
        {
            string sign="";
            if (itemType == "4")
            {
                switch (signType)
                {
                    case "0":
                        sign = " like '" + value+"' ";
                        break;
                    case "1":
                        sign = " not like '" + value + "' ";
                        break;
                }
            }
            else if (itemType == "8")
            {
                switch (signType)
                {
                    case "0":
                        sign = " like '%" + value + "%' ";
                        break;
                    case "1":
                        sign = " like '" + value + "%' and Answer not like '" + value + ",%'  ";
                        break;
                    case "2":
                        sign = " not like '%" + value + "%' ";
                        break;
                    case "3":
                        sign = " like '%" + value + "%' ";
                        break;
                }
            }
            return sign;
        }

        public HtmlGenericControl ShowItemTable(HtmlGenericControl div, DataTable dtItemOption, DataTable dtChooseOption, long ItemToTalChoosed)
        {
            Table tableItemOption = new Table();
            tableItemOption.CssClass = "OptionTable";
            TableRow tr1 = new TableRow();
            //TableCell tr1tc1 = new TableCell();
            TableHeaderCell tr1tc1 = new TableHeaderCell();
            tr1tc1.Text = "选项";
            tr1tc1.Width = 300;
            tr1.Cells.Add(tr1tc1);

            TableHeaderCell tr1tc2 = new TableHeaderCell();
            tr1tc2.Text = "频数";
            tr1tc2.Width = 200;
            tr1.Cells.Add(tr1tc2);

            TableHeaderCell tr1tc3 = new TableHeaderCell();
            tr1tc3.Text = "频率";
            tr1tc3.Width = 200;
            tr1.Cells.Add(tr1tc3);
            tableItemOption.Rows.Add(tr1);

            if (dtItemOption.Rows.Count > 0)
            {
                for (int i = 0; i < dtItemOption.Rows.Count; i++)
                {
                    long frequence = GetFrequence(dtChooseOption, ConvertHelper.ConvertString(dtItemOption.Rows[i]["OID"]));
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    tc1.Style.Add("text-align", "left");
                    tc1.Text = ConvertHelper.ConvertString(dtItemOption.Rows[i]["OptionName"]);
                    tr.Cells.Add(tc1);

                    TableCell tc2 = new TableCell();
                    tc2.Text = ConvertHelper.ConvertString(frequence);
                    tr.Cells.Add(tc2);

                    TableCell tc3 = new TableCell();
                    tc3.Text = String.Format("{0:F}", (double)frequence * 100 / ItemToTalChoosed) + "%";
                    tr.Cells.Add(tc3);

                    tableItemOption.Rows.Add(tr);
                }
            }


            div.Controls.Add(tableItemOption);
            return div;
        }

        public HtmlGenericControl ShowItemTableValue(HtmlGenericControl div, DataTable dtItemOption, DataTable dtChooseOption, long ItemToTalChoosed)
        {

            Table tableItemOption = new Table();
            tableItemOption.CssClass = "OptionTable";
            TableRow tr1 = new TableRow();
            //TableCell tr1tc1 = new TableCell();
            TableHeaderCell tr1tc1 = new TableHeaderCell();
            tr1tc1.Text = "选项";
            tr1tc1.Width = 140;
            tr1.Cells.Add(tr1tc1);

            TableHeaderCell tr1tc2 = new TableHeaderCell();
            tr1tc2.Text = "选项分值";
            tr1tc2.Width = 280;
            tr1.Cells.Add(tr1tc2);

            TableHeaderCell tr1tc3 = new TableHeaderCell();
            tr1tc3.Text = "频数";
            tr1tc3.Width = 140;

            tr1.Cells.Add(tr1tc3);
            tableItemOption.Rows.Add(tr1);

            TableHeaderCell tr1tc4 = new TableHeaderCell();
            tr1tc4.Text = "小计";
            tr1tc4.Width = 140;
            tr1.Cells.Add(tr1tc4);
            tableItemOption.Rows.Add(tr1);


            div.Controls.Add(tableItemOption);
            return div;
        }

        public HtmlGenericControl ShowItemConclusion(HtmlGenericControl div)
        {
            DataTable dtTtem = new WebChart_Layer().GetTtem(SID, IID);
            string itemConclusionText = "";
            if (dtTtem.Rows.Count > 0)
            {
                itemConclusionText = ConvertHelper.ConvertString(dtTtem.Rows[0]["ItemConclusion"]);
            }

            Anthem.TextBox itemConclusion = new Anthem.TextBox();
            itemConclusion.TextMode = TextBoxMode.MultiLine;
            itemConclusion.ID = "itemConclusion";
            itemConclusion.Height = 61;
            itemConclusion.Width = 600;
            itemConclusion.ToolTip = "最大允许输入长度为500个字,超过将被截断！";
            itemConclusion.CssClass = "itemConclusionText";

            itemConclusion.Text = itemConclusionText;
            itemConclusion.Attributes.Add("onKeyUp", "CheckLength(this,500)");


            Anthem.Button itemConclusionButton = new Anthem.Button();
            itemConclusionButton.OnClientClick = "return buttonClientClick();";
            itemConclusionButton.Text = "保存";
            itemConclusionButton.CssClass = "SaveItem";


            div.Controls.Add(itemConclusion);
            div.Controls.Add(itemConclusionButton);
            return div;
        }

        [Anthem.Method]
        protected void itemConclusionButtonClick()
        {
            if (ControlSafeCheck() == "True")
            {
                this.SID = ConvertHelper.ConvertString(Request.Form["SIDRecord"]);
                this.IID = ConvertHelper.ConvertString(Request.Form["IIDRecord"]);
                string ItemConclusion = ConvertHelper.ConvertString(Request.Form["itemConclusion"]);
                new WebChart_Layer().SetItemConclusion(SID, IID, ItemConclusion);
                //new MessageBoxHelper().Show(Page,"保存成功！");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("alert('{0}')", "保存成功！"));
            }
            else
            {
                //new MessageBoxHelper().Show(Page, ControlSafeCheck());
                Anthem.Manager.AddScriptForClientSideEval(string.Format("alert('{0}')", ControlSafeCheck()));
            }
        }

        /// <summary>
        /// 获取频数
        /// </summary>
        /// <returns></returns>
        public long GetFrequence(DataTable dtChooseOption, string ItemValue)
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

        #region 输入控件安全校验
        /// <summary>
        /// 控件安全校验,对每个输入控件进行安全校验
        /// </summary>
        private string ControlSafeCheck()
        {
            if (SafetyBusiness.CheckBadWord(ConvertHelper.ConvertString(Request.Form["itemConclusion"])))
            {
                return "评价结论输入有误！";
            }
            return "True";
        }
        #endregion

        #region 属性
        /// <summary>
        /// 选项ID
        /// </summary>
        private string IID
        {
            set { ViewState["IID"] = value; }
            get
            {
                if (ViewState["IID"] != null)
                {
                    return ViewState["IID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

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

        /// <summary>
        /// 问卷ID
        /// </summary>
        private string sql
        {
            set { ViewState["sql"] = value; }
            get
            {
                if (ViewState["sql"] != null)
                {
                    return ViewState["sql"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 所有已选择项
        /// </summary>
        private DataTable dtChooseOption
        {
            set { ViewState["dtChooseOption"] = value; }
            get
            {
                if (ViewState["dtChooseOption"] != null)
                {
                    return (DataTable)ViewState["dtChooseOption"];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 问卷表
        /// </summary>
        private DataTable dtTableName
        {
            set { ViewState["dtTableName"] = value; }
            get
            {
                if (ViewState["dtTableName"] != null)
                {
                    return (DataTable)ViewState["dtTableName"];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }
}
