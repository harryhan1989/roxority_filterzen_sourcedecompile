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
    public partial class FMenuVistor : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            InitPage();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {         
                BinddropSelect();
            }
        }

        /// <summary>
        /// 绑定单选框列表（菜单）
        /// </summary>
        private void BinddropSelect()
        {
            DataTable dt = new _12355WebChart().GetAllFMenu();
            DataRow dr = dt.NewRow();
            dr["name"] = "--请选择--";
            dr["code"] = "-1";
            dt.Rows.InsertAt(dr, 0);
            dropSelect.DataTextField = "name";
            dropSelect.DataValueField = "code";
            dropSelect.DataSource = dt;
            dropSelect.DataBind();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        public void InitPage()
        {
            //string TableName = "";

            Chart chart3DPie = new _12355WebChart().CreateChart("F", "", true, SeriesChartType.Pie, "父菜单点击率统计图");//创建3d饼图
            chart3DPie.Attributes.Add("id", "chart3DPie");
            chart3DPie.ImageUrl = "TempFiles/ChartPic_1";

            Chart chart3DColumn = new _12355WebChart().CreateChart("F", "", true, SeriesChartType.Column, "父菜单点击率统计图"); //创建3D柱形图
            chart3DColumn.Attributes.Add("id", "chart3DColumn");
            chart3DColumn.Style.Add("display", "none");//设置默认隐藏
            chart3DColumn.ImageUrl = "TempFiles/ChartPic_2";

            Chart chartPie = new _12355WebChart().CreateChart("F", "", false, SeriesChartType.Pie, "父菜单点击率统计图"); //创建饼图
            chartPie.Attributes.Add("id", "chartPie");
            chartPie.Style.Add("display", "none");//设置默认隐藏
            chartPie.ImageUrl = "TempFiles/ChartPic_3";

            Chart chartColumn = new _12355WebChart().CreateChart("F", "", false, SeriesChartType.Column, "父菜单点击率统计图"); //创建柱形图
            chartColumn.Attributes.Add("id", "chartColumn");
            chartColumn.Style.Add("display", "none");//设置默认隐藏
            chartColumn.ImageUrl = "TempFiles/ChartPic_4";

            Chart chart3DDoughnut = new _12355WebChart().CreateChart("F", "", true, SeriesChartType.Doughnut, "父菜单点击率统计图"); //创建3D圆环形
            chart3DDoughnut.Attributes.Add("id", "chart3DDoughnut");
            chart3DDoughnut.Style.Add("display", "none");//设置默认隐藏
            chart3DDoughnut.ImageUrl = "TempFiles/ChartPic_5";

            Chart chartDoughnut = new _12355WebChart().CreateChart("F", "", false, SeriesChartType.Doughnut, "父菜单点击率统计图"); //创建圆环形
            chartDoughnut.Attributes.Add("id", "chartDoughnut");
            chartDoughnut.Style.Add("display", "none");//设置默认隐藏
            chartDoughnut.ImageUrl = "TempFiles/ChartPic_6";

            Chart chart3DBar = new _12355WebChart().CreateChart("F", "", true, SeriesChartType.Bar, "父菜单点击率统计图"); //创建3D圆环形
            chart3DBar.Attributes.Add("id", "chart3DBar");
            chart3DBar.Style.Add("display", "none");//设置默认隐藏
            chart3DBar.ImageUrl = "TempFiles/ChartPic_7";

            Chart chartBar = new _12355WebChart().CreateChart("F", "", false, SeriesChartType.Bar, "父菜单点击率统计图"); //创建圆环形
            chartBar.Attributes.Add("id", "chartBar");
            chartBar.Style.Add("display", "none");//设置默认隐藏
            chartBar.ImageUrl = "TempFiles/ChartPic_8";

            chartContent.Controls.Add(chart3DPie);
            chartContent.Controls.Add(chart3DColumn);
            chartContent.Controls.Add(chart3DDoughnut);
            chartContent.Controls.Add(chart3DBar);

            chartContent.Controls.Add(chartPie);
            chartContent.Controls.Add(chartColumn);
            chartContent.Controls.Add(chartDoughnut);
            chartContent.Controls.Add(chartBar);
        }

        protected void dropSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSelect.SelectedIndex > 0 && dropSelect.SelectedValue != null)
            {
                Response.Redirect(string.Format("ZMenuVistor.aspx?Pid={0}",dropSelect.SelectedValue.ToString()));
            }
        }
    }
}
