using System;
using BLL.Rule;
using BLL.Entity;

namespace WebManage.Web.SysManager
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
                this.PartnerID = Request.QueryString["ID"].ToString();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            try
            {
                if (this.PartnerID != string.Empty)
                {
                    PartnerEntity entity = new PartnerEntity(this.PartnerID);

                    if (entity.Image.Length > 0)
                    {
                        Response.AddHeader("Content-Disposition", "filename=img" + this.PartnerID + ".jpg");
                        Response.ContentType = "image/*";
                        Response.BinaryWrite(entity.Image);
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
        private string PartnerID
        {
            set { ViewState["PartnerID"] = value.ToString(); }
            get
            {
                if (ViewState["PartnerID"] != null)
                {
                    return ViewState["PartnerID"].ToString();
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
