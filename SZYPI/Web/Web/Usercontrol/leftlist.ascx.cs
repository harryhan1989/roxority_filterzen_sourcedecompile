using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Helper;
using BusinessLayer.Web.Logic;

namespace Web.Web.Usercontrol
{
    public partial class leftlist : System.Web.UI.UserControl
    {
        #region 属性
        /// <summary>
        /// 栏目类别
        /// </summary>
        private string InfoCategoryID
        {
            get
            {
                if (ViewState["InfoCategoryID"] == null)
                {
                    return "";
                }
                else
                {
                    return ViewState["InfoCategoryID"].ToString();
                }
            }
            set
            {
                ViewState["InfoCategoryID"] = value;
            }
        }
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
                LoadGetUrlPara(); //获取url参数

                if (CheckIsExistChild(ConvertHelper.ConvertLong(InfoCategoryID)))//检查是否存在子项
                {
                    noticeLeft.Attributes.Add("style", "display:none"); //隐藏公告公示、便民服务

                    LoadChildContent(ConvertHelper.ConvertLong(InfoCategoryID));
                }
                else
                {
                    listLeft.Attributes.Add("style", "display:none"); //栏目导航

                    LoadParent(ConvertHelper.ConvertLong(InfoCategoryID));
                    LoadPublicNotice(); //加载公告公示
                    LoadPeopleService();//加载便民服务
                }
                if (InfoCategoryID == "0")
                {
                    noticeLeft.Attributes.Add("style", "display:"); //公告公示、便民服务
                    listLeft.Attributes.Add("style", "display:none"); //栏目导航

                    LoadPublicNotice(); //加载公告公示
                    LoadPeopleService();//加载便民服务
                }
            }
        }
        #endregion

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// </summary>
        public void LoadGetUrlPara()
        {
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("InfoCategoryID")))
            {
                InfoCategoryID = RequestQueryString.GetQueryString("InfoCategoryID").ToString();
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
            DataTable dtNotice = WebInfoLogic.GetPublicNotice(7, infoCategory);
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

        #region 检查是否存在子项
        /// <summary>
        /// 检查信息是否存在子项
        /// </summary>
        public bool CheckIsExistChild(long ParentID)
        {
            if (WebInfoLogic.IsExistChild(ParentID))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region

        public void ChildContent(long ParentID)
        {
            //if (ParentID == 0)
            //{
            //    if (CheckIsExistChild(ParentID))
            //    {
            //        LoadChildInfo();
            //    }
            //}
            //else
            //{
            //    LoadChildInfo();
            //}

            StringBuilder strMenu = new StringBuilder(200);

            DataTable dt = WebInfoLogic.GetCategoryName();

            DataTable dtParent = WebInfoLogic.ParentNameByID(ParentID);

            DataRow[] rows = dt.Select("ParentInfoCategoryID=" + ParentID);//查找当前结点的所有子结点 

            if (dtParent.Rows.Count > 0 && dtParent != null)
            {
                leftParent.InnerHtml = dtParent.Rows[0]["InfoCategoryName"].ToString();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        if (CheckIsExistChild(ConvertHelper.ConvertLong(row["InfoCategoryID"])))
                        {
                            strMenu.Append(string.Format("<li class=\"list_li\"><a href=\"Info.aspx?InfoCategoryID={1}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                        }
                        else
                        {
                            strMenu.Append(string.Format("<li class=\"list_li\"><a href=\"InfoList.aspx?CategoryID={1}&InfoCategoryID={2}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                        }
                    }
                    leftName.InnerHtml = strMenu.ToString();
                }
                else
                {
                    leftName.InnerHtml = "暂无相关信息...";
                }
            }
        }
        #endregion

        #region 存在子栏目
        /// <summary>
        /// 存在子栏目
        /// </summary>
        /// <param name="ParentID"></param>
        public void LoadChildContent(long ParentID)
        {
            StringBuilder strMenu = new StringBuilder(200);
            StringBuilder strChild = new StringBuilder(2000);

            DataTable dtname = WebInfoLogic.ParentNameByID(ParentID);
            if (dtname.Rows.Count > 0)
            {
                //strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\"><a href=\"InfoList.aspx?InfoCategoryID={0}\">{1}",

                //                  ParentID.ToString(), dtname.Rows[0][0].ToString()) + "</a></div>");
                strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\">{0}",

                                  dtname.Rows[0][0].ToString()) + "</div>");
            }

            DataTable dt = WebInfoLogic.GetCategoryName();

            DataTable dtParent = WebInfoLogic.ParentNameByID(ParentID);

            DataRow[] rows = dt.Select("ParentInfoCategoryID=" + ParentID);//查找当前结点的所有子结点 

            if (dtParent.Rows.Count > 0 && dtParent != null)
            {
                leftParent.InnerHtml = dtParent.Rows[0]["InfoCategoryName"].ToString();

                if (dt != null && dt.Rows.Count > 0)
                {
                    strChild.Append(string.Format("<ul class=\"list_ul00\">"));
                    foreach (DataRow row in rows)
                    {
                        if (CheckIsExistChild(ConvertHelper.ConvertLong(row["InfoCategoryID"])))
                        {
                            strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"InfoList.aspx?InfoCategoryID={1}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                        }
                        else
                        {
                            strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Info.aspx?CategoryID={1}&InfoCategoryID={2}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                        }
                    }
                    strChild.Append("</ul>");
                    leftName.InnerHtml = strMenu.Append(strChild.ToString()).ToString();
                }
                else
                {
                    leftName.InnerHtml = "暂无相关信息...";
                }
            }
        }
        #endregion

        #region 没有子栏目
        /// <summary>
        /// 没有子栏目
        /// </summary>
        /// <param name="ParentID"></param>
        public void LoadParent(long ParentID)
        {
            StringBuilder strMenu = new StringBuilder(1000);
            StringBuilder strChild = new StringBuilder(200);

            DataTable dt = WebInfoLogic.ParentCategoryID(ParentID);

            if (dt.Rows.Count > 0)
            {
                DataTable dtname = WebInfoLogic.ParentNameByID(ConvertHelper.ConvertLong(dt.Rows[0][0].ToString()));
                if (dtname.Rows.Count > 0)
                {
                    //strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\"><a href=\"InfoList.aspx?InfoCategoryID={0}\">{1}",

                    //                  dt.Rows[0][0].ToString(), dtname.Rows[0][0].ToString()) + "</a></div>");

                    strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\">{0}",

                                      dtname.Rows[0][0].ToString()) + "</div>");

                    DataTable dtChild = WebInfoLogic.ParentNameByID(ParentID);
                    if (dtChild.Rows.Count > 0)
                    {
                        strChild.Append(string.Format("<ul class=\"list_ul00\">"));
                        strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Info.aspx?InfoCategoryID={0}\" target=\"_self\">{1}</a></li>", ParentID.ToString(), dtChild.Rows[0][0].ToString()));
                        strChild.Append("</ul>");
                    }
                }
            }
            leftName.InnerHtml = strMenu.Append(strChild.ToString()).ToString();
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
                selectInfo = Common.PageHelper.StripHTML(strInfo).Substring(0, length).ToString().Trim();
            }
            else
            {
                selectInfo = Common.PageHelper.StripHTML(strInfo).ToString().Trim();
            }
            return selectInfo;
        }
        #endregion
    }
}