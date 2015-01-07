using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Survey;
using Nandasoft;
using Web_Survey.WebChart;
using Dundas.Charting.WebControl;
using System.Web.UI.HtmlControls;
using Business.Helper;
using WebUI;

namespace WebManage.Survey
{
    public partial class HotlineAnalysis :BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(this);
            if (!IsPostBack)
            {
                BindData();
                InitPage();
            }
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            CreateChart();
        }
        #endregion

        #region 初始化页
        /// <summary>
        /// 初始化页
        /// </summary>
        private void InitPage()
        {
            statisticsClass.SelectedValue = "ConsultType";
            CreateChart();
            CreateDiv(statisticsClass.SelectedItem.Text);
        }

        //分析按钮
        [Anthem.Method]
        protected void QueryClick()
        {
            CreateChart();
            CreateDiv(statisticsClass.SelectedItem.Text);
        }
        #endregion

        #region 创建图表
        /// <summary>
        /// 创建图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Anthem.Method]
        private void CreateChart()
        {
            this.StatisticsClass = statisticsClass.SelectedValue; //分析类型
            this.BeginDate = NDConvert.ToDateTime(wdcBeginDate.Value);
            if (NDConvert.ToDateTime(wdcEndDate.Value) == NDConvert.ToDateTime("1900-1-1"))
            {
                this.EndDate = NDConvert.ToDateTime("2999-12-31");
            }
            else
            {
                this.EndDate = NDConvert.ToDateTime(wdcEndDate.Value);
            }

            Chart chart3DPie = new CreateWebChart().HLACreateChart(true, SeriesChartType.Pie, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass);
            chart3DPie.Attributes.Add("id", "chart3DPie");
            chart3DPie.ImageUrl = "TempFiles/ChartPic_9";
            chartContent.Controls.Add(chart3DPie);
            if (IsPostBack)
            {
                string chart3DPieHtml = "\"" + RenderHTML(chart3DPie).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivAllHtml({0});", chart3DPieHtml));
            }
        }

        [Anthem.Method]
        protected void SetChart(object chart)
        {
            string strChart = ConvertHelper.ConvertString(chart);
            if (strChart == "chart3DPie")
            {
                Chart chart3DPie = new CreateWebChart().HLACreateChart(true, SeriesChartType.Pie, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建3d饼图
                chart3DPie.Attributes.Add("id", "chart3DPie");
                chart3DPie.ImageUrl = "TempFiles/ChartPic_9";
                chartContent.Controls.Add(chart3DPie);
                string chart3DPieHtml = "\"" + RenderHTML(chart3DPie).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DPieHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DColumn")
            {
                Chart chart3DColumn = new CreateWebChart().HLACreateChart(true, SeriesChartType.Column, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建3D柱形图
                chart3DColumn.Attributes.Add("id", "chart3DColumn");
                chart3DColumn.ImageUrl = "TempFiles/ChartPic_10";
                chartContent.Controls.Add(chart3DColumn);
                string chart3DColumnHtml = "\"" + RenderHTML(chart3DColumn).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DColumnHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartPie")
            {
                Chart chartPie = new CreateWebChart().HLACreateChart(false, SeriesChartType.Pie, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建饼图
                chartPie.Attributes.Add("id", "chartPie");
                chartPie.ImageUrl = "TempFiles/ChartPic_11";
                chartContent.Controls.Add(chartPie);
                string chartPieHtml = "\"" + RenderHTML(chartPie).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartPieHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartColumn")
            {
                Chart chartColumn = new CreateWebChart().HLACreateChart(false, SeriesChartType.Column, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建柱形图
                chartColumn.Attributes.Add("id", "chartColumn");
                chartColumn.ImageUrl = "TempFiles/ChartPic_12";
                chartContent.Controls.Add(chartColumn);
                string chartColumnHtml = "\"" + RenderHTML(chartColumn).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartColumnHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DDoughnut")
            {
                Chart chart3DDoughnut = new CreateWebChart().HLACreateChart(true, SeriesChartType.Doughnut, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建3D圆环形
                chart3DDoughnut.Attributes.Add("id", "chart3DDoughnut");
                chart3DDoughnut.ImageUrl = "TempFiles/ChartPic_13";
                chartContent.Controls.Add(chart3DDoughnut);
                string chart3DDoughnutHtml = "\"" + RenderHTML(chart3DDoughnut).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DDoughnutHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartDoughnut")
            {
                Chart chartDoughnut = new CreateWebChart().HLACreateChart(false, SeriesChartType.Doughnut, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建圆环形
                chartDoughnut.Attributes.Add("id", "chartDoughnut");
                chartDoughnut.ImageUrl = "TempFiles/ChartPic_14";
                chartContent.Controls.Add(chartDoughnut);
                string chartDoughnutHtml = "\"" + RenderHTML(chartDoughnut).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartDoughnutHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chart3DBar")
            {
                Chart chart3DBar = new CreateWebChart().HLACreateChart(true, SeriesChartType.Bar, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建3D圆环形
                chart3DBar.Attributes.Add("id", "chart3DBar");
                chart3DBar.ImageUrl = "TempFiles/ChartPic_15";
                chartContent.Controls.Add(chart3DBar);
                string chart3DBarHtml = "\"" + RenderHTML(chart3DBar).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chart3DBarHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
            else if (strChart == "chartBar")
            {
                Chart chartBar = new CreateWebChart().HLACreateChart(false, SeriesChartType.Bar, statisticsClass.SelectedItem.Text, BeginDate, EndDate, StatisticsClass); //创建圆环形
                chartBar.Attributes.Add("id", "chartBar");
                chartBar.ImageUrl = "TempFiles/ChartPic_16";
                chartContent.Controls.Add(chartBar);
                string chartBarHtml = "\"" + RenderHTML(chartBar).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                //Anthem.Manager.RegisterStartupScript(typeof(Page), chartContent.ID, "document.getElementById(\"chartContent\").innerHTML="+"123");
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivHtml({0});", chartBarHtml));
                //Anthem.Manager.AddCallBacks(chartContent, true, "", "", "", "");

            }
        }
        //将控件转换成html
        public string RenderHTML(Control objWebCtrl)
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

        //创建显示的表
        [Anthem.Method]
        private void CreateDiv(string StatisticsClassName)
        {
            long nowRentCount = 0;  //题目选项总选择数
            string title = ""; //图表的标题，题目标题

            DataTable StatisticsDetail = new HotlineAnalysis_Layer().GetStatisticsDetail(BeginDate, EndDate, StatisticsClass);  //获取需要热线分析的数据

            if (StatisticsDetail != null)
            {
                for (int i = 0; i < StatisticsDetail.Rows.Count; i++)
                {
                    nowRentCount = nowRentCount + ConvertHelper.ConvertLong(StatisticsDetail.Rows[i]["CountStatisticsClass"]);
                }
            }
            title = StatisticsClassName;

            HtmlGenericControl divItemTable = new HtmlGenericControl("div"); 
            //divItemTable.Style.Add("width", "700px");
            divItemTable.Style.Add("margin-top", "10px");
            divItemTable.Style.Add("margin-left", "5px");
            divItemTable.Attributes.Add("align", "Left");

            divItemTable.InnerHtml = "<div style=\"width:700px\" class=\"TableTitle\" >" + title + "</div>";
            divItemTable = ShowItemTable(divItemTable, StatisticsDetail, nowRentCount);

            chartTable.Controls.Add(divItemTable);  //添加到页面控件

            if (IsPostBack)
            {
                string chart3DPieHtml = "\"" + RenderHTML(divItemTable).Replace("\"", "'").Replace("\n", "").Replace("\r", "").Replace("   ", "") + "\"";
                Anthem.Manager.AddScriptForClientSideEval(string.Format("innerDivTableHtml({0});", chart3DPieHtml));
            }

        }

        //创建表详细
        public HtmlGenericControl ShowItemTable(HtmlGenericControl div, DataTable StatisticsDetail, long nowRentCount)
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

            if (StatisticsDetail.Rows.Count > 0)
            {
                for (int i = 0; i < StatisticsDetail.Rows.Count; i++)
                {
                    long frequence = ConvertHelper.ConvertLong(StatisticsDetail.Rows[i]["CountStatisticsClass"]);
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    tc1.Style.Add("text-align", "left");
                    tc1.Text = ConvertHelper.ConvertString(StatisticsDetail.Rows[i]["StatisticsClass"]);
                    tr.Cells.Add(tc1);

                    TableCell tc2 = new TableCell();
                    tc2.Text = ConvertHelper.ConvertString(frequence);
                    tr.Cells.Add(tc2);

                    TableCell tc3 = new TableCell();
                    tc3.Text = String.Format("{0:F}", (double)frequence * 100 / nowRentCount) + "%";
                    tr.Cells.Add(tc3);

                    tableItemOption.Rows.Add(tr);
                }
            }


            div.Controls.Add(tableItemOption);
            return div;
        }

        #endregion

        #region 绑定下拉列表数据
        /// <summary>
        ///  绑定下拉列表数据
        /// 作者：韩亮
        /// 时间：20101008
        /// </summary>
        private void BindData()
        {
            DataTable dt = new HotlineAnalysis_Layer().SelectCodeType();

            statisticsClass.DataSource = dt;
            statisticsClass.DataValueField = "CodeType";
            statisticsClass.DataTextField = "CName";
            statisticsClass.DataBind();
        }
        #endregion

        #region 属性

        /// <summary>
        /// 查询条件：统计类别
        /// </summary>
        private string StatisticsClass
        {
            set { ViewState["StatisticsClass"] = value; }
            get
            {
                if (ViewState["StatisticsClass"] != null)
                {
                    return ViewState["StatisticsClass"].ToString();
                }
                else
                {
                    return "";
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
