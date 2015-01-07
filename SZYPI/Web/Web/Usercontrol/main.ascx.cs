using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Business.Helper;
using EntityModel.WebEntity;
using BusinessLayer.Web.Logic;

namespace Web.Web.Usercontrol
{
    /// <summary>
    /// 目的：网站的主体内容
    /// 作者：刘娟
    /// 编写时间：2010-4-7
    /// </summary>
    public partial class main : System.Web.UI.UserControl
    {
        StringBuilder strTitle;//标题
        StringBuilder strContent;//内容 
        StringBuilder strNews;//信息
        StringBuilder strImg;//图片

        #region 页面加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCultureTrends();//文化动态
                LoadCultureIndustry();//文化产业
                LoadCultureHeritage();//文化遗产
                LoadBroadcastTele();//广播影视
                LoadNewsMedia();//新闻出版
                LoadCultureTopic();//文化专题 
                LoadCultureExchange();//文化交流
                LoadCultureWrite();//文化创作
                LoadCultureMarket();//文化市场
                LoadCultureTalents();//文化人才
                LoadCultureResearch();//文化研究
                LoadCulturePeople();//文化民生
            }
        }
        #endregion

        #region 绑定文化动态新闻
        /// <summary>
        /// 文化动态
        /// </summary>
        public void LoadCultureTrends()
        {
            DataTable dtTrends = WebInfoLogic.GetWebInfoRoll(5);

            string sPicUrls = "";
            string sLinks = "";
            string sTexts = "";
            string url = Request.Url.ToString().ToLower().Replace("index.aspx", "InfoDetail.aspx?InfoID=");

            foreach (DataRow dr in dtTrends.Rows)
            {
                sPicUrls += SystemParaLogic.GetSystemParam("MiniatureDummyPath") + dr["MiniatureFileName"].ToString() + "|";
                sLinks += url + dr["InfoID"].ToString() + "|";
                sTexts += dr["Title"].ToString() + "|";
            }

            if (dtTrends.Rows.Count > 0)
            {
                sPicUrls = sPicUrls.Substring(0, sPicUrls.Length - 1);
                sLinks = sLinks.Substring(0, sLinks.Length - 1);
                sTexts = sTexts.Substring(0, sTexts.Length - 1);
            }
            divPicNews.InnerHtml = "<script type=\"text/javascript\">getFlashNews(\"" + sPicUrls + "\",\"" + sLinks + "\",\"" + sTexts + "\");</script>";

            LoadCultureTrendsRight();

        }
        #endregion

        #region 绑定文化产业
        /// <summary>
        /// 文化产业
        /// </summary>
        public void LoadCultureIndustry()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureIndustry);

            imgWhcy.InnerHtml = strImg.ToString();
            topWhcyTitle.InnerHtml = strTitle.ToString();
            topWhcyContent.InnerHtml = strContent.ToString();
            divWhcyNews.InnerHtml = strNews.ToString();

        }
        #endregion

        #region 绑定文化遗产
        /// <summary>
        /// 文化遗产
        /// </summary>
        public void LoadCultureHeritage()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureHeritage);

            imgWhyc.InnerHtml = strImg.ToString();
            topWhycTitle.InnerHtml = strTitle.ToString();
            topWhycContent.InnerHtml = strContent.ToString();
            divWhycNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定广播影视
        /// <summary>
        /// 广播影视
        /// </summary>
        public void LoadBroadcastTele()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.BroadcastTele);

            imgGbys.InnerHtml = strImg.ToString();
            topGbysTitle.InnerHtml = strTitle.ToString();
            topGbysContent.InnerHtml = strContent.ToString();
            divGbysNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定新闻出版
        /// <summary>
        /// 新闻出版
        /// </summary>
        public void LoadNewsMedia()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.NewsMedia);

            imgXwcb.InnerHtml = strImg.ToString();
            topXwcbTitle.InnerHtml = strTitle.ToString();
            topXwcbContent.InnerHtml = strContent.ToString();
            divXwcbNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化专题
        /// <summary>
        /// 文化专题
        /// </summary>
        public void LoadCultureTopic()
        {
            DataTable dtTopic = WebInfoLogic.GetCulumnInfo((long)Common.PageEnum.ColumnCategory.CultureTopic, 5);
            StringBuilder strTopic = new StringBuilder(500);

            if (dtTopic.Rows.Count > 0)
            {
                strTopic.Append("<marquee style=\"padding: 1px;\" scrollamount=\"1\" scrolldelay=\"5\" behavior=\"alternate\"");
                strTopic.Append(" height=\"155\" width=\"670\" loop=\"-1\" onmouseover=\"javascript:this.stop();\" onmouseout=\"javascript:this.start();\">");
                for (int i = 0; i < dtTopic.Rows.Count; i++)
                {
                    strTopic.Append("<div class=\"pp\"><img width=\"184\" height=\"129\" src=\"" + dtTopic.Rows[i]["MiniatureFileName"].ToString());
                    strTopic.Append("\" /><a href=\"InfoDetail.aspx?InfoID=" + dtTopic.Rows[i]["InfoID"].ToString() + "\">");
                    strTopic.Append("<div class=\"name\">" + dtTopic.Rows[i]["Title"] + "</div></a></div>");
                }
                strTopic.Append("</marquee>");
                divTopic.InnerHtml = strTopic.ToString();
            }

        }
        #endregion

        #region 绑定文化交流
        /// <summary>
        /// 文化交流
        /// </summary>
        public void LoadCultureExchange()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureExchange);

            imgWhjl.InnerHtml = strImg.ToString();
            topWhjlTitle.InnerHtml = strTitle.ToString();
            topWhjlContent.InnerHtml = strContent.ToString();
            divWhjlNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化创作
        /// <summary>
        /// 文化创作
        /// </summary>
        public void LoadCultureWrite()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureWrite);

            imgWhcz.InnerHtml = strImg.ToString();
            topWhczTitle.InnerHtml = strTitle.ToString();
            topWhczContent.InnerHtml = strContent.ToString();
            divWhczNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化市场
        /// <summary>
        /// 文化市场
        /// </summary>
        public void LoadCultureMarket()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureMarket);

            imgWhsc.InnerHtml = strImg.ToString();
            topWhscTitle.InnerHtml = strTitle.ToString();
            topWhscContent.InnerHtml = strContent.ToString();
            divWhscNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化人才
        /// <summary>
        /// 文化人才
        /// </summary>
        public void LoadCultureTalents()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureTalents);

            imgWhrc.InnerHtml = strImg.ToString();
            topWhrcTitle.InnerHtml = strTitle.ToString();
            topWhrcContent.InnerHtml = strContent.ToString();
            divWhrcNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化研究
        /// <summary>
        /// 文化研究
        /// </summary>
        public void LoadCultureResearch()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CultureResearch);

            imgWhyj.InnerHtml = strImg.ToString();
            topWhyjTitle.InnerHtml = strTitle.ToString();
            topWhyjContent.InnerHtml = strContent.ToString();
            divWhyjNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定文化民生
        /// <summary>
        /// 文化民生
        /// </summary>
        public void LoadCulturePeople()
        {
            LoadData((long)Common.PageEnum.ColumnCategory.CulturePeople);

            imgWhms.InnerHtml = strImg.ToString();
            topWhmsTitle.InnerHtml = strTitle.ToString();
            topWhmsContent.InnerHtml = strContent.ToString();
            divWhmsNews.InnerHtml = strNews.ToString();
        }
        #endregion

        #region 绑定数据信息
        /// <summary>
        /// 各类型信息
        /// </summary>
        public void LoadData(long InfoCategory)
        {
            strTitle = new StringBuilder(500);
            strContent = new StringBuilder(500);
            strImg = new StringBuilder(200);
            strNews = new StringBuilder(2000);

            DataTable dtNews = WebInfoLogic.GetCulumnInfo(InfoCategory, 5);

            if (dtNews.Rows.Count > 0)
            {
                strNews.Append("<table border=\"0\" style=\"width: 100%;\">");
                for (int i = 0; i < dtNews.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strTitle.Append("<a href=\"InfoDetail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtNews.Rows[i]["InfoID"].ToString()));
                        strTitle.Append("\" target=\"_blank\" title=\"标    题：" + dtNews.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                        strTitle.Append(ConvertHelper.ConvertDateTime(dtNews.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                        strTitle.Append(dtNews.Rows[i]["ClickRate"].ToString() + "\">");
                        strTitle.Append(SelectInfo(dtNews.Rows[0]["Title"].ToString().Trim(), 13) + "</a>");

                        strContent.Append("<span class=\"hidetext\">");
                        strContent.Append(Server.HtmlDecode(dtNews.Rows[0]["InfoContent"].ToString()));
                        strContent.Append("</span>");

                        if (CheckImageUrl(dtNews.Rows[0]["MiniatureFileName"].ToString()))
                        {
                            strImg.Append("<img width=\"105\" height=\"72\" src=\"" + SelectImage(InfoCategory));
                            strImg.Append("\" />");
                        }
                        else
                        {
                            strImg.Append("<img width=\"105\" height=\"72\" src=\"" + SystemParaLogic.GetSystemParam("MiniatureDummyPath") + dtNews.Rows[0]["MiniatureFileName"].ToString() + "\"/>");
                        }
                    }
                    else
                    {
                        strNews.Append("<tr><td style=\"width:60%;\"><ul>");
                        strNews.Append("<li><a class=\"hideText\" style=\"width:200px;\" href=\"InfoDetail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtNews.Rows[i]["InfoID"].ToString()));
                        strNews.Append("\" target=\"_blank\" title=\"标    题：" + dtNews.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                        strNews.Append(ConvertHelper.ConvertDateTime(dtNews.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                        strNews.Append(dtNews.Rows[i]["ClickRate"].ToString() + "\">");
                        strNews.Append(dtNews.Rows[i]["Title"].ToString().Trim() + "</a></li></ul>");
                        strNews.Append("</td><td style=\"width:40%;\">");
                        strNews.Append("[" + dtNews.Rows[i]["PubDate"].ToString() + "]</td></tr>");
                    }
                }
                strNews.Append("</table>");
            }
            else
            {
                strTitle.Remove(0, strTitle.Length);
                strContent.Remove(0, strContent.Length);
                strNews.Remove(0, strNews.Length);
            }
        }
        #endregion

        #region 文化动态右边信息
        /// <summary>
        /// 右边信息
        /// </summary>
        public void LoadCultureTrendsRight()
        {
            DataTable dtRight = WebInfoLogic.GetWebInfoRoll(8);
            StringBuilder strRight = new StringBuilder(1000);

            if (dtRight.Rows.Count > 0)
            {
                strRight.Append("<ul>");
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    strRight.Append("<li><a href=\"InfoDetail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtRight.Rows[i]["InfoID"].ToString()));
                    strRight.Append("\" target=\"_blank\" title=\"标    题：" + dtRight.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                    strRight.Append(ConvertHelper.ConvertDateTime(dtRight.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                    strRight.Append(dtRight.Rows[i]["ClickRate"].ToString() + "\">" + dtRight.Rows[i]["Title"].ToString() + "</a>");
                    strRight.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    strRight.Append("[" + ConvertHelper.ConvertDateTime(dtRight.Rows[i]["PubDate"].ToString()).Month.ToString() + "/");
                    strRight.Append(ConvertHelper.ConvertDateTime(dtRight.Rows[i]["PubDate"].ToString()).Day.ToString() + "]");
                }
                strRight.Append("</ul>");
            }
            divWhdtRight.InnerHtml = strRight.ToString();
        }
        #endregion

        #region 截取标题长度
        /// <summary>
        /// 截取标题长度
        /// </summary>
        /// <param name="strInfo"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string SelectInfo(string strInfo, int length)
        {
            string selectInfo = string.Empty;
            if (strInfo.Length > length)
            {
                selectInfo = strInfo.Substring(0, length).ToString().Trim();
            }
            else
            {
                selectInfo = strInfo.ToString().Trim();
            }
            return selectInfo;
        }

        #endregion

        #region 获取默认图片
        /// <summary>
        /// 默认图片
        /// </summary>
        /// <param name="InfoCategory"></param>
        /// <returns></returns>
        public string SelectImage(long InfoCategory)
        {
            string strUrl = string.Empty;
            switch (InfoCategory)
            {
                case (long)Common.PageEnum.ColumnCategory.CultureIndustry: strUrl = "images/文化产业.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureHeritage: strUrl = "images/文化遗产.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.BroadcastTele: strUrl = "images/广播影视.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.NewsMedia: strUrl = "images/新闻出版.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureWrite: strUrl = "images/文艺创作.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureMarket: strUrl = "images/文化市场.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureExchange: strUrl = "images/文化交流.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureTalents: strUrl = "images/文化人才.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CultureResearch: strUrl = "images/文化研究.jpg";
                    break;
                case (long)Common.PageEnum.ColumnCategory.CulturePeople: strUrl = "images/文化民生.jpg";
                    break;
                default:
                    break;
            }
            return strUrl;
        }
        #endregion

        #region 判断图片地址是否存在
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public bool CheckImageUrl(string imgUrl)
        {
            if (string.IsNullOrEmpty(imgUrl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
