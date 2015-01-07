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
    /// <summary>
    /// 目的：实现父级下的子菜单
    /// 作者：刘娟
    /// </summary>
    public partial class leftmenu : System.Web.UI.UserControl
    {
        #region 属性
        /// <summary>
        /// 信息类型ID
        /// </summary>
        private long InfoCategoryID
        {
            get
            {
                if (ViewState["InfoCategoryID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["InfoCategoryID"].ToString());
                }
            }
            set
            {
                ViewState["InfoCategoryID"] = value;
            }
        }

        /// <summary>
        /// 类型ID
        /// </summary>
        private long CategoryID
        {
            get
            {
                if (ViewState["CategoryID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["CategoryID"].ToString());
                }
            }
            set
            {
                ViewState["CategoryID"] = value;
            }
        }

        #endregion

        StringBuilder strMenu = new StringBuilder(1000);
        StringBuilder strChildMenu = new StringBuilder(1000);

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

                ShowMenu(InfoCategoryID); //显示菜单

                if (CategoryID != 0)
                {
                    LoadParentName(CategoryID);  //显示父级菜单名
                }
                else
                {
                    LoadParentName(InfoCategoryID);  //显示父级菜单名
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
                InfoCategoryID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("InfoCategoryID"));
            }
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("CategoryID")))
            {
                CategoryID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("CategoryID"));
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

        #region 显示父级菜单名
        /// <summary>
        /// 显示父级菜单名
        /// </summary>
        /// <param name="parentNodeID"></param>
        private void LoadParentName(long parentNodeID)
        {
            DataTable dtParent = WebInfoLogic.GetCategoryName(parentNodeID);

            if (dtParent.Rows.Count > 0 && dtParent != null)
            {
                //leftParent.InnerHtml = dtParent.Rows[0]["InfoCategoryName"].ToString();
            }
        }
        #endregion

        #region 显示菜单
        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="parentNodeID"></param>
        private void ShowMenu(long parentNodeID)
        {
            if (CheckIsExistChild(parentNodeID))
            {
                DataTable dt = WebInfoLogic.GetParentName(parentNodeID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (CheckIsExistChild(ConvertHelper.ConvertLong(dt.Rows[i]["InfoCategoryID"].ToString())))
                        {
                            //strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\" onclick=\"ShowMenu('{3}')\"><a href=\"info.aspx?InfoCategoryID={0}&CategoryID={2}\">{1}",

                            //   dt.Rows[i]["InfoCategoryID"].ToString(), dt.Rows[i]["InfoCategoryName"].ToString(), dt.Rows[i]["InfoCategoryID"].ToString(), dt.Rows[i]["InfoCategoryID"].ToString()) + "</a></div>");

                            strMenu.Append(string.Format("<div class=\"list_li\" style=\"cursor:pointer\"><a href=\"info.aspx?InfoCategoryID={0}&CategoryID={2}\">{1}",

                                dt.Rows[i]["InfoCategoryID"].ToString(), dt.Rows[i]["InfoCategoryName"].ToString(), dt.Rows[i]["InfoCategoryID"].ToString()) + "</a></div>");

                        }
                        if (ConvertHelper.ConvertLong(dt.Rows[i]["InfoCategoryID"].ToString()) != 0)
                        {
                            if (CheckIsExistChild(ConvertHelper.ConvertLong(dt.Rows[i]["InfoCategoryID"].ToString())))
                            {
                                LoadChildMenu(ConvertHelper.ConvertLong(dt.Rows[i]["InfoCategoryID"].ToString()));
                            }
                            else
                            {
                                ShowMenu(ConvertHelper.ConvertLong(dt.Rows[i]["InfoCategoryID"].ToString()));
                            }
                        }
                    }
                }
            }
            else
            {
                DataTable dtName = WebInfoLogic.GetCategoryName(parentNodeID);
                strMenu.Append(string.Format("<div class=\"list_li\"><a href=\"info.aspx?InfoCategoryID={0}&CategoryID={2}\">{1}</a></div>",
                     InfoCategoryID.ToString(), dtName.Rows[0]["InfoCategoryName"].ToString(), parentNodeID.ToString()));
            }
            leftName.InnerHtml = strMenu.ToString();

        }
        #endregion

        #region 显示子菜单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParnetID"></param>
        public void LoadChildMenu(long ParnetID)
        {
            StringBuilder strChild = new StringBuilder(200);
            strChild.Append(string.Format("<span id=\"{0}\" class=\"no\"><ul class=\"list_ul00\">", ParnetID));

            DataTable dtChild = WebInfoLogic.GetCategoryName();

            DataRow[] rows = dtChild.Select("ParentInfoCategoryID=" + ParnetID);//查找当前结点的所有子结点 

            if (dtChild != null && dtChild.Rows.Count > 0)
            {
                foreach (DataRow row in rows)
                {
                    strChild.Append(string.Format("<li class=\"list_ul00_li\"><a href=\"Info.aspx?InfoCategoryID={0}&CategoryID={1}\" target=\"_self\">{2}</a></li>",
                         ParnetID.ToString(), row["InfoCategoryID"].ToString(), row["InfoCategoryName"].ToString()));

                }
            }
            strChild.Append("</ul></span>");
            strMenu.Append(strChild.ToString());
        }
        #endregion
    }
}