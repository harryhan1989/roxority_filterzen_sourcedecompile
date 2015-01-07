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
using BLL.Rule;

namespace WebManage.Web._12355WebVistor
{
    public partial class NewsVistor : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 父菜单ID
        /// </summary>
        private string PID
        {
            set { ViewState["PID"] = value; }
            get
            {
                if (ViewState["PID"] != null)
                {
                    return ViewState["PID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 上一菜单ID
        /// </summary>
        private string LAID
        {
            set { ViewState["LAID"] = value; }
            get
            {
                if (ViewState["LAID"] != null)
                {
                    return ViewState["LAID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            InitPage();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        public void InitPage()
        {
            if (Request.QueryString["pid"] != null)
            {
                PID = Request.QueryString["pid"].ToString();
            }
            if (Request.QueryString["laid"] != null)
            {
                LAID = Request.QueryString["laid"].ToString();
            }
            long countNum = new _12355WebChart().GetCountNum("N", PID);

            DataTable dtItemOption = null;
            dtItemOption = new _12355WebChart().GetMenuDT(PID);

            HtmlGenericControl divItemTable = new HtmlGenericControl("div");
            //divItemTable.Style.Add("width", "700px");
            divItemTable.Style.Add("margin-top", "10px");
            divItemTable.Style.Add("margin-left", "5px");
            divItemTable.Attributes.Add("align", "Left");

            divItemTable.InnerHtml = "<div style=\"width:700px\" class=\"TableTitle\" >统计列表</div>";
            divItemTable = ShowItemTable(divItemTable, dtItemOption, countNum);

            Chart chart3DColumn = new _12355WebChart().CreateChart("N", PID, true, SeriesChartType.Column, "新闻访问率统计图"); //创建3D柱形图
            chart3DColumn.Attributes.Add("id", "chart3DColumn");
            chart3DColumn.ImageUrl = "TempFiles/ChartPic_2";

            Chart chartColumn = new _12355WebChart().CreateChart("N", PID, false, SeriesChartType.Column, "新闻访问率统计图"); //创建柱形图
            chartColumn.Attributes.Add("id", "chartColumn");
            chartColumn.Style.Add("display", "none");//设置默认隐藏
            chartColumn.ImageUrl = "TempFiles/ChartPic_4";

            Chart chart3DBar = new _12355WebChart().CreateChart("N", PID, true, SeriesChartType.Bar, "新闻访问率统计图"); //创建3D圆环形
            chart3DBar.Attributes.Add("id", "chart3DBar");
            chart3DBar.Style.Add("display", "none");//设置默认隐藏
            chart3DBar.ImageUrl = "TempFiles/ChartPic_7";

            Chart chartBar = new _12355WebChart().CreateChart("N", PID, false, SeriesChartType.Bar, "新闻访问率统计图"); //创建圆环形
            chartBar.Attributes.Add("id", "chartBar");
            chartBar.Style.Add("display", "none");//设置默认隐藏
            chartBar.ImageUrl = "TempFiles/ChartPic_8";

            chartContent.Controls.Add(chart3DColumn);
            chartContent.Controls.Add(chart3DBar);

            chartContent.Controls.Add(chartColumn);
            chartContent.Controls.Add(chartBar);

            Page.Form.Controls.Add(divItemTable);  //添加到页面控件
        }

        public HtmlGenericControl ShowItemTable(HtmlGenericControl div, DataTable dtItemOption, long ItemToTalChoosed)
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
                    long frequence = ConvertHelper.ConvertLong(dtItemOption.Rows[i]["viewcount"]);
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    tc1.Style.Add("text-align", "left");
                    tc1.Text = ConvertHelper.ConvertString(dtItemOption.Rows[i]["name"]);
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ZMenuVistor.aspx?Pid={0}", LAID));
        }
    }
}
