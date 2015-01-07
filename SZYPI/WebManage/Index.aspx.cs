using System;
using System.Data;
using Aspose.Cells;
using BLL.Rule;
using Dundas.Charting.WebControl;
using Web_Survey.WebChart;
using Nandasoft;

namespace WebManage
{
    /// <summary>
    /// 问卷调查模块首页
    /// 作者：姚东
    /// 时间：20100924
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20100924
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDate();
            }
        }

        /// <summary>
        /// 首页数据绑定
        /// 作者：姚东
        /// 时间：20100924
        /// </summary>
        private void BindDate()
        {
            BLL.Query.IndexQuery query = new BLL.Query.IndexQuery();

            //绑定问卷调查部分的数据
            DataTable surveyDT = null;

            surveyDT = query.GetSurveyList();

            DataColumn dc = new DataColumn();
            dc.ColumnName = "HasAnswered";
            surveyDT.Columns.Add(dc);

            if (Session["UserGUID"] != null)
            {
                foreach (DataRow dr in surveyDT.Rows)
                {
                    if (new BLL.Query.IndexQuery().CheckHasAnswered(dr["SID"].ToString(), Session["UserIDClient"].ToString()))
                    {
                        dr["HasAnswered"] = "已参加";
                    }
                    else
                    {
                        dr["HasAnswered"] = "未参加";
                    }
                }
            }            
            
            rptSurveyMain.DataSource = surveyDT;
            rptSurveyMain.DataBind();


            //绑定热门问卷部分数据
            DataTable hotSurveyDT = query.GetHotSurveyList();

            rptHotSurvey.DataSource = hotSurveyDT;
            rptHotSurvey.DataBind();

            //绑定推荐问卷部分数据
            DataTable recommendSurveyDT = query.GetRecommendSurveyList();

            if (recommendSurveyDT.Rows.Count > 0)
            {
                lblRecommendSurveyTitle.Text = string.Format("<a href='#' onclick='window.open(\"Survey/userdata/u1/s{0}.aspx\")'>{1}</a>", recommendSurveyDT.Rows[0]["sid"].ToString(), recommendSurveyDT.Rows[0]["SurveyName"].ToString());
                lblRecommendSurveyContent.Text = recommendSurveyDT.Rows[0]["PageContent"].ToString();
                lblRecommendSurveyPoint.Text = "赠送积分：" + recommendSurveyDT.Rows[0]["Point"].ToString() + "分";                
            }

            //所有礼品
            DataTable giftDT = new BLL.Query.GiftsQuery().GetAllGifts();
            rGiftsShow.DataSource = giftDT;
            rGiftsShow.DataBind();

            //热门礼品
            DataTable hotGiftDT = new BLL.Query.GiftsQuery().GetHotGifts();
            rHotGift.DataSource = hotGiftDT;
            rHotGift.DataBind();

            //兑换记录
            DataTable hotExchangeHistoryDT = new BLL.Query.ExchangeQuery().GetNewExchangeList();
            rExchangeHistory.DataSource = hotExchangeHistoryDT;
            rExchangeHistory.DataBind();

            //合作伙伴
            DataTable partnerDT = new BLL.Query.PartnerQuery().GetOnLinePartner();
            rPartner.DataSource = partnerDT;
            rPartner.DataBind();

            //网站访问频率统计图
            Dundas.Charting.WebControl.Chart chart3DPie = new _12355WebChart().CreateChart("F", "", true, SeriesChartType.Pie, "网站访问频率统计分析图");//创建3d饼图
            chart3DPie.Width = 450;
            chart3DPie.Height = 366;
            chart3DPie.Attributes.Add("id", "chart3DPie");
            chart3DPie.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPic_1";
            chartContent1.Controls.Add(chart3DPie);

            //热线咨询统计图
            Dundas.Charting.WebControl.Chart chart3DPie2 =
                new CreateWebChart().HLACreateChart(true, SeriesChartType.Pie, "热线咨询统计图", NDConvert.ToDateTime("1900-1-1"), NDConvert.ToDateTime("2999-12-31"), "ConsultType");
            chart3DPie2.Width = 450;
            chart3DPie2.Height = 366;
            chart3DPie2.Attributes.Add("id", "chart3DPie");
            chart3DPie2.ImageUrl = "Survey/TempFiles/ChartPic_9";
            chartContent2.Controls.Add(chart3DPie2);

            //1、2010年青年最关注的十大社会现象
            Dundas.Charting.WebControl.Chart chart3DPieTop1 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "2010年青年最关注的十大社会现象","Top1");//创建3d饼图
            chart3DPieTop1.Width = 450;
            chart3DPieTop1.Height = 366;
            chart3DPieTop1.Attributes.Add("id", "chart3DPie");
            chart3DPieTop1.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_1";
            chartContent3.Controls.Add(chart3DPieTop1);

            //2、2010年青年人中十大网络流行语
            Dundas.Charting.WebControl.Chart chart3DPieTop2 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "2010年青年人中十大网络流行语", "Top2");//创建3d饼图
            chart3DPieTop2.Width = 750;
            chart3DPieTop2.Height = 366;
            chart3DPieTop2.Attributes.Add("id", "chart3DPie");
            chart3DPieTop2.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_2";
            chartContent4.Controls.Add(chart3DPieTop2);

            //3、当前青年人最关心的十大社会热点问题
            Dundas.Charting.WebControl.Chart chart3DPieTop3 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "当前青年人最关心的十大社会热点问题", "Top3");//创建3d饼图
            chart3DPieTop3.Width = 450;
            chart3DPieTop3.Height = 366;
            chart3DPieTop3.Attributes.Add("id", "chart3DPie");
            chart3DPieTop3.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_3";
            chartContent5.Controls.Add(chart3DPieTop3);

            //4、青年人最渴望获得的十大知识和技能
            Dundas.Charting.WebControl.Chart chart3DPieTop4 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人最渴望获得的十大知识和技能", "Top4");//创建3d饼图
            chart3DPieTop4.Width = 450;
            chart3DPieTop4.Height = 366;
            chart3DPieTop4.Attributes.Add("id", "chart3DPie");
            chart3DPieTop4.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_4";
            chartContent6.Controls.Add(chart3DPieTop4);

            //5、青年人十大择业标准
            Dundas.Charting.WebControl.Chart chart3DPieTop5 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大择业标准", "Top5");//创建3d饼图
            chart3DPieTop5.Width = 600;
            chart3DPieTop5.Height = 366;
            chart3DPieTop5.Attributes.Add("id", "chart3DPie");
            chart3DPieTop5.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_5";
            chartContent7.Controls.Add(chart3DPieTop5);

            //6、青年人十大择偶标准
            Dundas.Charting.WebControl.Chart chart3DPieTop6 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大择偶标准", "Top6");//创建3d饼图
            chart3DPieTop6.Width = 450;
            chart3DPieTop6.Height = 366;
            chart3DPieTop6.Attributes.Add("id", "chart3DPie");
            chart3DPieTop6.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_6";
            chartContent8.Controls.Add(chart3DPieTop6);

            //7、青年人十大消费支出项目
            Dundas.Charting.WebControl.Chart chart3DPieTop7 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大消费支出项目", "Top7");//创建3d饼图
            chart3DPieTop7.Width = 450;
            chart3DPieTop7.Height = 366;
            chart3DPieTop7.Attributes.Add("id", "chart3DPie");
            chart3DPieTop7.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_7";
            chartContent9.Controls.Add(chart3DPieTop7);

            //8、青年人十大交流工具
            Dundas.Charting.WebControl.Chart chart3DPieTop8 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大交流工具", "Top8");//创建3d饼图
            chart3DPieTop8.Width = 450;
            chart3DPieTop8.Height = 366;
            chart3DPieTop8.Attributes.Add("id", "chart3DPie");
            chart3DPieTop8.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_8";
            chartContent10.Controls.Add(chart3DPieTop8);

            //9、青年人十大休闲娱乐项目
            Dundas.Charting.WebControl.Chart chart3DPieTop9 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大休闲娱乐项目", "Top9");//创建3d饼图
            chart3DPieTop9.Width = 450;
            chart3DPieTop9.Height = 366;
            chart3DPieTop9.Attributes.Add("id", "chart3DPie");
            chart3DPieTop9.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_9";
            chartContent11.Controls.Add(chart3DPieTop9);

            //10、青年人十大幸福指数
            Dundas.Charting.WebControl.Chart chart3DPieTop10 = new _12355WebChart().CreateTop10Survey(true, SeriesChartType.Pie, "青年人十大幸福指数", "Top10");//创建3d饼图
            chart3DPieTop10.Width = 450;
            chart3DPieTop10.Height = 366;
            chart3DPieTop10.Attributes.Add("id", "chart3DPie");
            chart3DPieTop10.ImageUrl = "Web/12355WebVistor/TempFiles/ChartPicTop_10";
            chartContent12.Controls.Add(chart3DPieTop10);
        }

        private void HuiYuanLogin()
        {
 
        }        
    }
}