using System;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BLL.Entity;

namespace WebManage.Controls
{
    public partial class TopPageControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 12355网站菜单连接
            string WebSite12355 = WebConfigurationManager.AppSettings["WebSite12355"];
            string BBSWebSite = WebConfigurationManager.AppSettings["BBSWebSite"];  

            if (Session["UserGUID"] == null)
            {
                //网站首页
                a12355Index.HRef = WebSite12355;

                //学习教育
                a12355Education.HRef = WebSite12355 + "xxjy/index.aspx";

                //身心健康
                a12355Healthy.HRef = WebSite12355 + "sxjk/index.aspx";

                //困难救助
                a12355Difficulty.HRef = WebSite12355 + "knjz/index.aspx";

                //青少年维权
                a12355Youth.HRef = WebSite12355 + "gzdt/newslist.aspx?fl=QSNWQ";

                //就业创业
                a12355Job.HRef = WebSite12355 + "jycy/index.aspx";

                //婚恋交友
                a12355Love.HRef = WebSite12355 + "hljy/index.aspx?fl=jypd";

                //工作动态
                a12355Work.HRef = WebSite12355 + "gzdt/newslist.aspx?fl=zcfg";

                //青年志愿者
                a12355Volunteer.HRef = WebSite12355 + "zyz/index.aspx";

                //旅游休闲
                a12355Tour.HRef = WebSite12355 + "lyxx/index.aspx";

                //手机游戏
                a12355Game.HRef = WebSite12355 + "game/index.aspx";

                //12355论坛
                a12355BBS.HRef = BBSWebSite;
            }
            else
            {
                string userGuid = Session["UserGUID"].ToString();
                HuiYuanEntity entity = new HuiYuanEntity(userGuid);
                string loginPWD = entity.LoginPWD;
                string encrptPWD = Business.Safety.SafetyBusiness.Encrypt(loginPWD);

                //网站首页
                a12355Index.HRef = WebSite12355 + "home/homeIndex.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //学习教育
                a12355Education.HRef = WebSite12355 + "xxjy/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //身心健康
                a12355Healthy.HRef = WebSite12355 + "sxjk/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //困难救助
                a12355Difficulty.HRef = WebSite12355 + "knjz/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //青少年维权
                a12355Youth.HRef = WebSite12355 + "gzdt/newslist.aspx?fl=QSNWQ&U=" + userGuid + "&P=" + encrptPWD;

                //就业创业
                a12355Job.HRef = WebSite12355 + "jycy/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //婚恋交友
                a12355Love.HRef = WebSite12355 + "hljy/index.aspx?fl=jypd&U=" + userGuid + "&P=" + encrptPWD;

                //工作动态
                a12355Work.HRef = WebSite12355 + "gzdt/newslist.aspx?fl=zcfg&U=" + userGuid + "&P=" + encrptPWD;

                //青年志愿者
                a12355Volunteer.HRef = WebSite12355 + "zyz/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //旅游休闲
                a12355Tour.HRef = WebSite12355 + "lyxx/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //手机游戏
                a12355Game.HRef = WebSite12355 + "game/index.aspx?U=" + userGuid + "&P=" + encrptPWD;

                //12355论坛
                a12355BBS.HRef = BBSWebSite;
            }
            #endregion

            #region 分析平台菜单栏
            //首页
            aIndex.HRef = ResolveUrl("../") + "Index.aspx";

            //调查问卷
            aAllSurveys.HRef = ResolveUrl("../") + "Web/Survey/AllSurveysPage.aspx?SurveyType=0";     //问卷展示
            aMySurveys.HRef = ResolveUrl("../") + "Web/HuiYuan/MySurveys.aspx?SurveyType=0";      //我的问卷

            //测评问卷
            aAllTestSurveys.HRef = ResolveUrl("../") + "Web/Survey/AllSurveysPage.aspx?SurveyType=2";     //测评展示
            aMyTestSurveys.HRef = ResolveUrl("../") + "Web/HuiYuan/MySurveys.aspx?SurveyType=2";      //我的测评

            //投票问卷
            aAllVoteSurveys.HRef = ResolveUrl("../") + "Web/Survey/AllSurveysPage.aspx?SurveyType=1";     //投票展示
            aMyVoteSurveys.HRef = ResolveUrl("../") + "Web/HuiYuan/MySurveys.aspx?SurveyType=1";      //我的投票

            //礼品兑换
            aAllGifts.HRef = ResolveUrl("../") + "Web/Gifts/Show.aspx";           //礼品展示
            aGiftExchangeList.HRef = ResolveUrl("../") + "Web/Gifts/GiftExchangeList.aspx";   //兑换记录

            //会员管理
            aHuiYuanInfo.HRef = ResolveUrl("../") + "Web/HuiYuan/Info.aspx";        //我的账户
            aGiftExchange.HRef = ResolveUrl("../") + "Web/Gifts/Show.aspx";           //礼品兑换
            aMyGiftExchangeList.HRef = ResolveUrl("../") + "Web/Gifts/MyExchangeHistory.aspx";     //兑换记录
            aMyAllSurveys.HRef = ResolveUrl("../") + "Web/HuiYuan/MySurveys.aspx?SurveyType=-1";  //我的问卷
            #endregion

            //会员登录的连接
            string queryString = string.Empty;
            if (Page.Request.QueryString.Count > 0)
            {
                queryString = Page.Request.Url.ToString().Substring(Page.Request.Url.ToString().IndexOf('?'));
            }

            hddSrc.Value = ResolveUrl("../") + "HuiYuanLogin.aspx"+queryString;            

            //搜索按钮的样式
            lbSearch.Style.Add("background", "url('" + ResolveUrl("../") + "CSS/image/Index/search.jpg') no-repeat; vertical-align:middle");

            //首页flash的资源连接
            para1.Attributes.Add("value", ResolveUrl("../") + "css/image/index/swf.swf");
        }        
    }
}