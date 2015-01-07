using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityModel.WebEntity;
using Business.Helper;
using System.Text;
using BusinessLayer.Web.Logic;

namespace Web.Web
{
    /// <summary>
    /// 目的：查看图片
    /// 作者：刘娟
    /// 编写时间:2010-4-7
    /// </summary>
    public partial class ImgPhoto : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 浮动广告ID
        /// </summary>
        private long AdvertID
        {
            get
            {
                if (ViewState["ID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["ID"].ToString());
                }
            }
            set
            {
                ViewState["ID"] = value;
            }
        }

        private long InfoCategoryID
        {
            set { ViewState["InfoCategoryID"] = value.ToString(); }
            get
            {
                if (ViewState["InfoCategoryID"] != null)
                {
                    return ConvertHelper.ConvertLong(ViewState["InfoCategoryID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGetUrlPara();
                InitPage();
            }
        }
        #endregion

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// </summary>
        public void LoadGetUrlPara()
        {
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("ID")))
            {
                AdvertID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("ID"));
            }

            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("InfoCategoryID")))
            {
                this.InfoCategoryID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("InfoCategoryID"));
            }
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// </summary>
        public void InitPage()
        {
            try
            {
                if (this.AdvertID != 0)
                {
                    List<AdvertEntity> advertEntity = AdvertLogic.GetAdvertOnLineImage(AdvertID);

                    if (advertEntity[0].AdContent.Length > 0)
                    {
                        Response.AddHeader("Content-Disposition", "filename=img" + AdvertID);
                        Response.ContentType = "image/*";
                        Response.BinaryWrite(advertEntity[0].AdContent);
                        Response.End();
                    }
                }
                else if (this.InfoCategoryID != 0)
                {
                    DataTable dt = WebInfoLogic.WebInfoCategoryImage(InfoCategoryID);

                    if (dt.Rows[0]["MiniatureName"].ToString().Length > 0)
                    {
                        Response.AddHeader("Content-Disposition", string.Format("filename=img{0}.jpg", this.InfoCategoryID));
                        Response.ContentType = "image/*";
                        Response.BinaryWrite((byte[])dt.Rows[0]["MiniatureName"]);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().RegisterStartupScript(string.Format("<script>winow.open('{0}');", ex.Message));
            }
        }
        #endregion
    }
}
