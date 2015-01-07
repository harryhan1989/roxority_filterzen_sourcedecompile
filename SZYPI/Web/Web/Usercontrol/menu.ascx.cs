using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityModel.WebEntity;
using Business.Helper;
using System.Data;
using BusinessLayer.Web.Logic;
using System.Text;

namespace Web.Web.Usercontrol
{
    public partial class menu : System.Web.UI.UserControl
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

        /// <summary>
        /// 类型
        /// </summary>
        private string type
        {
            get
            {
                if (ViewState["type"] == null)
                {
                    return "";
                }
                else
                {
                    return ViewState["type"].ToString();
                }
            }
            set
            {
                ViewState["type"] = value;
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
                    LoadChildContent(ConvertHelper.ConvertLong(InfoCategoryID));
                }
                else
                {
                    DataTable dtParent = WebInfoLogic.GetParentCategoryName(InfoCategoryID);

                    if (dtParent.Rows.Count>0)
                    {
                        LoadChildContent(ConvertHelper.ConvertLong(dtParent.Rows[0]["ParentInfoCategoryID"]));
                    }
                    else
                    {
                        LoadParent(ConvertHelper.ConvertLong(InfoCategoryID));
                    }
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
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("type")))
            {
                type = ConvertHelper.ConvertString(RequestQueryString.GetQueryString("type"));
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
                strMenu.Append(string.Format("<div class=\"list_li\">{0}",
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
                        if (type == "1")
                        {
                            if (CheckIsExistChild(ConvertHelper.ConvertLong(row["InfoCategoryID"])))
                            {
                                if (ParentID == 1)
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Jggk_Detail.aspx?InfoCategoryID={1}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                                }
                                else
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={1}&type=1\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                                }
                            }
                            else
                            {
                                if (ParentID == 1)
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Jggk_Detail.aspx?CategoryID={1}&InfoCategoryID={2}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                                }
                                else
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?CategoryID={1}&InfoCategoryID={2}&type=1\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                                }
                            }
                        }
                        else
                        {
                            if (CheckIsExistChild(ConvertHelper.ConvertLong(row["InfoCategoryID"])))
                            {
                                if (ParentID == 1)
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Jggk_Detail.aspx?InfoCategoryID={1}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                                }
                                else
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={1}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString()));
                                }
                            }
                            else
                            {
                                if (ParentID == 1)
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Jggk_Detail.aspx?CategoryID={1}&InfoCategoryID={2}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                                }
                                else
                                {
                                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?CategoryID={1}&InfoCategoryID={2}\" target=\"_self\">{0}</a></li>", row["InfoCategoryName"].ToString(), row["InfoCategoryID"].ToString(), ParentID));
                                }
                            }
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
                    strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\">{0}",
                                    dtname.Rows[0][0].ToString()) + "</div>");
                    //if (type == "1")
                    //{
                    //    strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={0}&type=1\">{1}",
                    //                 dt.Rows[0][0].ToString(), dtname.Rows[0][0].ToString()) + "</a></div>");
                    //}
                    //else
                    //{
                    //    strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={0}\">{1}",
                    //                    dt.Rows[0][0].ToString(), dtname.Rows[0][0].ToString()) + "</a></div>");
 
                    //}
                    DataTable dtChild = WebInfoLogic.ParentNameByID(ParentID);
                    if (dtChild.Rows.Count > 0)
                    {
                        strChild.Append(string.Format("<ul class=\"list_ul00\">"));

                        if (ParentID == 1)
                        {
                            strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Jggk_Detail.aspx?InfoCategoryID={0}\" target=\"_self\">{1}</a></li>", ParentID.ToString(), dtChild.Rows[0][0].ToString()));
                        }
                        else
                        {
                            if (type == "1")
                            {
                                strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={0}&type=1\" target=\"_self\">{1}</a></li>", ParentID.ToString(), dtChild.Rows[0][0].ToString()));
                            }
                            else
                            {
                                strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Zwgk_Info.aspx?InfoCategoryID={0}\" target=\"_self\">{1}</a></li>", ParentID.ToString(), dtChild.Rows[0][0].ToString()));
                            }
                        }
                        strChild.Append("</ul>");
                    }
                }
            }
            leftName.InnerHtml = strMenu.Append(strChild.ToString()).ToString();
        }
        #endregion
    }
}