using System;
using BLL.Rule;
using BLL.Entity;

namespace WebManage.Web.Gifts
{
    public partial class ImagePhoto : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUrlParameter();
                InitPage();
            }
        }

        /// <summary>
        /// 获得URL参数
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.GiftID = Request.QueryString["ID"].ToString();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            try
            {
                if (this.GiftID != string.Empty)
                {
                    GiftsEntity entity = new GiftsEntity(this.GiftID);

                    if (entity.Picture.Length > 0)
                    {
                        Response.AddHeader("Content-Disposition", "filename=img" + this.GiftID + ".jpg");
                        Response.ContentType = "image/*";
                        Response.BinaryWrite(entity.Picture);
                        Response.End();
                    }
                }
            }
            catch
            {

            }
        }

        #region 属性

        /// <summary>
        /// 接收URL参数传入的ID
        /// </summary>
        private string GiftID
        {
            set { ViewState["GiftID"] = value.ToString(); }
            get
            {
                if (ViewState["GiftID"] != null)
                {
                    return ViewState["GiftID"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
    }
}
