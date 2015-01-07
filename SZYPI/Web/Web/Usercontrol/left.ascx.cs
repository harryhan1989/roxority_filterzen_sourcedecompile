using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Helper;
using EntityModel.WebEntity;
using BusinessLayer.Web.Logic;
using System.Collections.Generic;

namespace Web.Web.Usercontrol
{
    public partial class left : UserControl
    {
        private string _linkName = string.Empty;

        #region 关注模块类型

        private int _typeChecked = -1;
        private bool _flag = false;
        private string _userIP = string.Empty;
        private string _ip = string.Empty;
        private long _totalCount = 0;
        private DateTime _datetimea = new DateTime();
        private DateTime _datetimeb = new DateTime();

        #endregion

        #region 在线投票实体类
        OnlineResearchEntity reserachEntity = new OnlineResearchEntity();
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCount(); //流量统计
                LoadDropDownList(); //友情链接
                LoadGovPublic();//政府信息公开
                LoadPublicNotice();//公告公示
                LoadPeopleService();//便民服务
                LoadPolicy();//政策法规
                //_flag = ConvertHelper.ConvertBoolean(Session["flage"]);
            }
            else
            {
                //_flag = ConvertHelper.ConvertBoolean(Session["flage"]);

                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    _userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    _userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            btnSearch.Attributes.Add("onclick", "OpenUrl('" + btnSearch.UniqueID + "');return false;");
            #region 注册下拉框脚本事件

            ddlDept.Attributes.Add("onchange", "OpenFriendshipUrl('" + ddlDept.UniqueID + "');return false;");
            ddlCulture.Attributes.Add("onchange", "OpenFriendshipUrl('" + ddlCulture.UniqueID + "');return false;");
            ddlFriend.Attributes.Add("onchange", "OpenFriendshipUrl('" + ddlFriend.UniqueID + "');return false;");
            ddlMedia.Attributes.Add("onchange", "OpenFriendshipUrl('" + ddlMedia.UniqueID + "');return false;");
            ddlBasic.Attributes.Add("onchange", "OpenFriendshipUrl('" + ddlBasic.UniqueID + "');return false;");

            #endregion
        }
        #endregion

        #region 政府信息公开
        /// <summary>
        /// 获取政府信息公开
        /// </summary>
        public void LoadGovPublic()
        {
            DataTable dtGov = WebInfoLogic.GetGovPublic();
            StringBuilder strRoll = new StringBuilder(100);

            if (dtGov.Rows.Count > 0)
            {
                strRoll.Append("<ul>");
                for (int i = 0; i < dtGov.Rows.Count; i++)
                {
                    strRoll.Append(string.Format("<li><a href=\"Zwgk_Left.aspx?InfoCategoryID={0}", ConvertHelper.ConvertLong(dtGov.Rows[i]["InfoCategoryID"].ToString())));
                    strRoll.Append("\">" + dtGov.Rows[i]["InfoCategoryName"].ToString() + "</a>");
                }
                strRoll.Append("</ul>");
                divGovInfo.InnerHtml = strRoll.ToString();
            }
        }
        #endregion

        #region 政策法规
        /// <summary>
        /// 获取政策法规
        /// </summary>
        public void LoadPolicy()
        {
            int infoCategory = WebInfoLogic.GetCategoryID("政策法规");
            DataTable dtNotice = WebInfoLogic.GetPolicy(7, infoCategory);
            StringBuilder strNotice = new StringBuilder(1000);

            if (dtNotice.Rows.Count > 0)
            {
                strNotice.Append("<ul>");
                for (int i = 0; i < dtNotice.Rows.Count; i++)
                {
                    strNotice.Append("<li><a style=\"display: block;overflow: hidden;white-space: pre-wrap;text-overflow: clip;width:180px;height:22px;\" href=\"Zfgk_Detail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtNotice.Rows[i]["InfoID"].ToString()));
                    strNotice.Append("&InfoCategoryID=" + ConvertHelper.ConvertLong(dtNotice.Rows[i]["InfoCategoryID"].ToString()));
                    strNotice.Append("\" target=\"_blank\" title=\"标    题：" + dtNotice.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                    strNotice.Append(ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                    strNotice.Append(dtNotice.Rows[i]["ClickRate"].ToString() + "\">" + dtNotice.Rows[i]["Title"].ToString() + "</a>");
                    strNotice.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    strNotice.Append("[" + ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).Month.ToString() + "/");
                    strNotice.Append(ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).Day.ToString() + "]");
                }
                strNotice.Append("</ul>");
                divPolicy.InnerHtml = strNotice.ToString();
            }
        }
        #endregion

        #region 公告公示信息
        /// <summary>
        /// 获取公告公示信息
        /// </summary>
        public void LoadPublicNotice()
        {
            int infoCategory = WebInfoLogic.GetCategoryID("通知公告");
            DataTable dtNotice = WebInfoLogic.GetPublicNotice(7,infoCategory);
            StringBuilder strNotice = new StringBuilder(1000);

            if (dtNotice.Rows.Count > 0)
            {
                strNotice.Append("<ul>");
                for (int i = 0; i < dtNotice.Rows.Count; i++)
                {
                    strNotice.Append("<li><a style=\"display: block;overflow: hidden;white-space: pre-wrap;text-overflow: clip;width:180px;height:22px;\" href=\"Zfgk_Detail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtNotice.Rows[i]["InfoID"].ToString()));
                    strNotice.Append("&InfoCategoryID=" + ConvertHelper.ConvertLong(dtNotice.Rows[i]["InfoCategoryID"].ToString()));
                    strNotice.Append("\" target=\"_blank\" title=\"标    题：" + dtNotice.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                    strNotice.Append(ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                    strNotice.Append(dtNotice.Rows[i]["ClickRate"].ToString() + "\">" + dtNotice.Rows[i]["Title"].ToString() + "</a>");
                    strNotice.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    strNotice.Append("[" + ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).Month.ToString() + "/");
                    strNotice.Append(ConvertHelper.ConvertDateTime(dtNotice.Rows[i]["PubDate"].ToString()).Day.ToString() + "]");
                }
                strNotice.Append("</ul>");
                divNotice.InnerHtml = strNotice.ToString();
            }
        }
        #endregion

        #region 便民服务信息
        /// <summary>
        /// 获取便民服务信息
        /// </summary>
        public void LoadPeopleService()
        {
            DataTable dtService = WebInfoLogic.GetPeopleService(7);
            StringBuilder strService = new StringBuilder(200);

            if (dtService.Rows.Count > 0)
            {
                strService.Append("<ul>");
                for (int i = 0; i < dtService.Rows.Count; i++)
                {
                    strService.Append("<li><a style=\"display: block;overflow: hidden;white-space: pre-wrap;text-overflow: clip;width:200px;height:22px;\" href=\"InfoDetail.aspx?InfoID=" + ConvertHelper.ConvertLong(dtService.Rows[i]["InfoID"].ToString()));
                    strService.Append("&InfoCategoryID=" + dtService.Rows[i]["InfoCategoryID"].ToString());
                    strService.Append("\" target=\"_blank\" title=\"标    题：" + dtService.Rows[i]["Title"].ToString() + "&#10;发布日期：");
                    strService.Append(ConvertHelper.ConvertDateTime(dtService.Rows[i]["PubDate"].ToString()).ToShortDateString() + "&#10;访问次数：");
                    strService.Append(dtService.Rows[i]["ClickRate"].ToString() + "\">" + dtService.Rows[i]["Title"].ToString() + "</a>");
                    strService.Append("&nbsp;&nbsp;");
                }
                strService.Append("</ul>");
                divService.InnerHtml = strService.ToString();
            }
        }
        #endregion

        #region 投票事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bntSubmit_Click(object sender, EventArgs e)
        {
            bool voter = false;

            if (cbChecked() == false)
            {
                new MessageBoxHelper().ClientScriptEval("alert(\"请选择所关注的模块！\");");
                bntSubmit.UpdateAfterCallBack = true;
            }
            else
            {
                if ( DateTime.Now.AddMinutes(-5) > ConvertHelper.ConvertDateTime(Session["VoteTime"]))
                {//_userIP == Session["ip"].ToString() &&ConvertHelper.ConvertBoolean(Session["flage"]) == false &&
                    #region 判断被选中的复选框
                    
                    if (cbNews.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureNews;
                        voter = VoterCount();
                    }
                    if (cbIndustry.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureIndustry;
                        voter = VoterCount();
                    }
                    if (cbLive.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureLive;
                        voter = VoterCount();
                    }
                    if (cbHeritage.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureHeritage;
                        voter = VoterCount();
                    }
                    if (cbRadioTV.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.RadioTV;
                        voter = VoterCount();
                    }
                    if (cbPublic.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.NewsPublic;
                        voter = VoterCount();
                    }
                    if (cbCreate.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureCreation;
                        voter = VoterCount();
                    }
                    if (cbMarket.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CulturalMarket;
                        voter = VoterCount();
                    }
                    if (cbExchange.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CulturalExchange;
                        voter = VoterCount();
                    }
                    if (cbTalent.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CultureTalent;
                        voter = VoterCount();
                    }
                    if (cbStudy.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CulturalStudies;
                        voter = VoterCount();
                    }
                    if (cbTopic.Checked)
                    {
                        _typeChecked = (int)Common.PageEnum.ResearchType.CulturalTopics;
                        voter = VoterCount();
                    }
                    #endregion
                    if (voter == true)
                    {
                        //Session["flage"] = true;
                        new MessageBoxHelper().ClientScriptEval("alert(\"投票成功,感谢你的参与！\");");
                        bntSubmit.UpdateAfterCallBack = true;
                    }
                    else
                    {
                        //Session["flage"] = true;
                        new MessageBoxHelper().ClientScriptEval("alert(\"投票失败,感谢你的参与！\");");
                        bntSubmit.UpdateAfterCallBack = true;
                    }
                    Session["VoteTime"] = DateTime.Now;
                }
                else
                {
                   // _flag = ConvertHelper.ConvertBoolean(Session["flage"]);
                    new MessageBoxHelper().ClientScriptEval("alert(\"你已投票，请5分钟后再次投票，感谢你的参与！\");");
                    bntSubmit.UpdateAfterCallBack = true;
                }
            }
        }
        #endregion

        #region 流量统计
        /// <summary>
        /// 流量统计
        /// </summary>
        public void LoadCount()
        {
            lblCount.Text = ConvertHelper.ConvertString(Application["count"]);//总访问量
            lblDayCount.Text = ConvertHelper.ConvertString(Application["daycount"]);//日访问量

            List<TrafficStatisticsEntity> traEntity = TrafficStatisticsLogic.GetTrafficCount();

            lblStartDate.Text = ConvertHelper.ConvertDateTime(traEntity[0].StartDate).ToShortDateString();

            //_totalCount = ConvertHelper.ConvertLong(Application["count"]) + ConvertHelper.ConvertLong(Application["daycount"]);

            _totalCount = ConvertHelper.ConvertLong(Application["count"]);

            //更新总访问量
            TrafficStatisticsLogic.UpdateTrafficCount(traEntity[0].ID, _totalCount);

            //lblAticle.Text = ConvertHelper.ConvertString(TrafficStatisticsLogic.GetTrafficArticleCount());

            //天数
            //_datetimea = ConvertHelper.ConvertDateTime(DateTime.Now.ToShortDateString());
            //_datetimeb = ConvertHelper.ConvertDateTime(traEntity[0].StartDate.ToShortDateString());

            TimeSpan ts = (TimeSpan)_datetimea.Subtract(_datetimeb);

            //日均访问
            //if (Application["daycount"].ToString() == "0")
            //{
            //    lblAveCount.Text = "0";
            //}
            //else
            //{
            //    lblAveCount.Text = ConvertHelper.ConvertString(_totalCount/ConvertHelper.ConvertLong(ts.Days));
            //}
            lblCount.UpdateAfterCallBack = true;
            lblDayCount.UpdateAfterCallBack = true;

            //lblAticle.UpdateAfterCallBack = true;
            //lblAveCount.UpdateAfterCallBack = true;
        }
        #endregion

        #region 投票方法
        /// <summary>
        /// 投票方法
        /// </summary>
        public bool VoterCount()
        {
            reserachEntity.InfoCategory = _typeChecked;

            if (OnlineResearchLogic.IsExists(reserachEntity.InfoCategory))
            {
                OnlineResearchLogic.UpdateResearch(reserachEntity);
                return true;
            }
            else
            {
                reserachEntity.VoterCount = 1;
                if (OnlineResearchLogic.InsertResearch(reserachEntity) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 判断是否选中类型
        /// <summary>
        /// 判断是否选中类型
        /// </summary>
        /// <returns></returns>
        public bool cbChecked()
        {
            bool flage = false;

            if (cbNews.Checked || cbIndustry.Checked || cbLive.Checked || cbHeritage.Checked || cbRadioTV.Checked || cbPublic.Checked
                || cbCreate.Checked || cbMarket.Checked || cbExchange.Checked || cbTalent.Checked || cbStudy.Checked || cbTopic.Checked)
            {
                flage = true;
            }
            else
            {
                flage = false;
            }

            return flage;
        }
        #endregion

        #region 加载友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        public void LoadDropDownList()
        {
            //各级部门网站
            DataTable dtDept = WebInfoLogic.GetDropDownInfo((long)Common.PageEnum.DownLinkType.Dept);
            _linkName = WebInfoLogic.GetDownLinkName((long)Common.PageEnum.DownLinkType.Dept);

            ddlDept.Items.Insert(0, new ListItem(string.Format("---{0}---", _linkName), "0"));
            foreach (DataRow row in dtDept.Rows)
            {
                ListItem li = new ListItem();
                li.Text = row["DownLinkName"].ToString();
                li.Value = row["DownLinkHref"].ToString();
                ddlDept.Items.Add(li);
            }

            //文化部门网站
            DataTable dtCulture = WebInfoLogic.GetDropDownInfo((long)Common.PageEnum.DownLinkType.Culture);
            _linkName = WebInfoLogic.GetDownLinkName((long)Common.PageEnum.DownLinkType.Culture);

            ddlCulture.Items.Insert(0, new ListItem(string.Format("---{0}---", _linkName), "0"));
            foreach (DataRow row in dtCulture.Rows)
            {
                ListItem li = new ListItem();
                li.Text = row["DownLinkName"].ToString();
                li.Value = row["DownLinkHref"].ToString();
                ddlCulture.Items.Add(li);
            }

            //友好文化网站
            DataTable dtFriend = WebInfoLogic.GetDropDownInfo((long)Common.PageEnum.DownLinkType.Friend);
            _linkName = WebInfoLogic.GetDownLinkName((long)Common.PageEnum.DownLinkType.Friend);

            ddlFriend.Items.Insert(0, new ListItem(string.Format("---{0}---", _linkName), "0"));
            foreach (DataRow row in dtFriend.Rows)
            {
                ListItem li = new ListItem();
                li.Text = row["DownLinkName"].ToString();
                li.Value = row["DownLinkHref"].ToString();
                ddlFriend.Items.Add(li);
            }

            //推荐媒体网站
            DataTable dtMedia = WebInfoLogic.GetDropDownInfo((long)Common.PageEnum.DownLinkType.Media);
            _linkName = WebInfoLogic.GetDownLinkName((long)Common.PageEnum.DownLinkType.Media);

            ddlMedia.Items.Insert(0, new ListItem(string.Format("---{0}---", _linkName), "0"));
            foreach (DataRow row in dtMedia.Rows)
            {
                ListItem li = new ListItem();
                li.Text = row["DownLinkName"].ToString();
                li.Value = row["DownLinkHref"].ToString();
                ddlMedia.Items.Add(li);
            }

            //基层单位网站
            DataTable dtBasic = WebInfoLogic.GetDropDownInfo((long)Common.PageEnum.DownLinkType.Basic);
            _linkName = WebInfoLogic.GetDownLinkName((long)Common.PageEnum.DownLinkType.Basic);

            ddlBasic.Items.Insert(0, new ListItem(string.Format("---{0}---", _linkName), "0"));
            foreach (DataRow row in dtBasic.Rows)
            {
                ListItem li = new ListItem();
                li.Text = row["DownLinkName"].ToString();
                li.Value = row["DownLinkHref"].ToString();
                ddlBasic.Items.Add(li);
            }
        }
        /// <summary>
        /// 选中某一项打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUrl = ddlDept.SelectedValue.ToString();
            if (selectedUrl != "0")
            {
                new MessageBoxHelper().RegisterStartupScript(ResolveClientUrl("window.open('" + selectedUrl + "');"));
            }
        }
        #endregion
    }
}