using System;
using System.Collections.Generic;
using System.Data;
using EntityModel.WebEntity;
using Business.Helper;
using BusinessLayer.Web.Logic;

namespace Web.Web.Usercontrol
{
    public partial class InfoPosition1 : System.Web.UI.UserControl
    {
        #region 属性
        /// <summary>
        /// 信息ID
        /// </summary>
        private long InfoID
        {
            get
            {
                if (ViewState["InfoID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["InfoID"].ToString());
                }
            }
            set
            {
                ViewState["InfoID"] = value;
            }
        }

        /// <summary>
        /// 类别ID
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

        private string _str = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGetUrlPara(); //获取页面传递参数 

                //LoadPage(87);

                LoadPosition(); //加载数据位置
                LoadDetail();
            }
        }

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// </summary>
        public void LoadGetUrlPara()
        {
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("InfoID")))
            {
                InfoID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("InfoID"));
            }

            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("InfoCategoryID")))
            {
                if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("CategoryID")))
                {
                    InfoCategoryID = ConvertHelper.ConvertString(RequestQueryString.GetQueryString("CategoryID"));
                    return;
                }
                InfoCategoryID = ConvertHelper.ConvertString(RequestQueryString.GetQueryString("InfoCategoryID"));
            }
        }
        #endregion

        #region
        public string LoadPage(long ParentInfoID)
        {
            string Result = string.Empty;

            DataTable dt = WebInfoLogic.GetChildCategoryName(ParentInfoID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int mno = Convert.ToInt32(dt.Rows[i][0].ToString());
                if ("".Equals(Result))
                    Result += mno;
                else
                    Result += "," + mno;

                string sRes = menuChild(mno);

                if (!"".Equals(sRes))
                    Result += "," + sRes;
            }

            return Result;
        }

        private string menuChild(int ParentId)
        {
            string Result = String.Empty;
            DataTable dt = WebInfoLogic.GetChildCategoryName(ParentId);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int mid = Convert.ToInt32(dt.Rows[i][0].ToString());
                if ("".Equals(Result))
                    Result += mid;
                else
                    Result += "," + mid;
                Result += menuChild(mid);
            }
            return Result;

        }
        #endregion

        #region 绑定位置
        /// <summary>
        /// 绑定位置 
        /// </summary>
        public void LoadPosition()
        {
            if (!string.IsNullOrEmpty(InfoCategoryID) && (InfoCategoryID.ToString() != "0" || InfoCategoryID.ToString() == "0"))
            {
                LoadInfo(InfoCategoryID);
            }
            else if (string.IsNullOrEmpty(InfoCategoryID) && InfoID.ToString() != "0")
            {
                List<WebInfoEntity> infoEntity = WebInfoLogic.GetWebInfoEntityByInfoID(InfoID);

                InfoCategoryID = ConvertHelper.ConvertString(infoEntity[0].InfoCategoryID);

                LoadInfo(InfoCategoryID);
            }
        }
        #endregion

        #region 位置信息
        /// <summary>
        /// 位置信息
        /// </summary>
        /// <param name="webInfoID"></param>
        public void LoadInfo(string webInfoID)
        {
            if (ConvertHelper.ConvertString(webInfoID) != "0")
            {
                DataTable dt = WebInfoLogic.GetParentCategoryName(webInfoID);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        DataTable dtname = WebInfoLogic.GetCategoryName(webInfoID);

                        if (WebInfoLogic.IsExistChild(ConvertHelper.ConvertLong(webInfoID)))
                        {
                            _str = string.Format(">><a href=InfoList.aspx?InfoCategoryID={0} target=\"_self\">{1}</a>", webInfoID, dtname.Rows[0]["InfoCategoryName"].ToString()) + _str;
                        }
                        else
                        {
                            _str = string.Format(">><a href=Info.aspx?InfoCategoryID={0} target=\"_self\">{1}</a>", webInfoID, dtname.Rows[0]["InfoCategoryName"].ToString()) + _str;
                        }

                        LoadInfo(dtname.Rows[0]["ParentInfoCategoryID"].ToString());
                    }
                    else
                    {
                        DataTable dtname = WebInfoLogic.GetCategoryName(webInfoID);

                        if (WebInfoLogic.IsExistChild(ConvertHelper.ConvertLong(webInfoID)))
                        {
                            _str = string.Format(">><a href=InfoList.aspx?InfoCategoryID={0} target=\"_self\">{1}</a>", webInfoID, dtname.Rows[0]["InfoCategoryName"].ToString()) + _str;
                        }
                        else
                        {
                            _str = string.Format(">><a href=Info.aspx?InfoCategoryID={0} target=\"_self\">{1}</a>", webInfoID, dtname.Rows[0]["InfoCategoryName"].ToString()) + _str;
                        }
                    }
                }
            }
            else
            {
                if (ConvertHelper.ConvertString(webInfoID) == "0")
                {
                    _str = string.Format(">><a href=InfoList.aspx?InfoCategoryID={0} target=\"_self\">{1}</a>", "0", "文化动态");
                }
            }
        }

        private void LoadDetail()
        {
            if (InfoID != 0)
            {
                _str = _str + ">>正文";
            }
            divPosition.InnerHtml = "您当前位置：<a href=\"Index.aspx\" target=\"_self\">首页</a>" + _str;
        }
        #endregion
    }
}

