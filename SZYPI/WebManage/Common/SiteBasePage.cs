using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Nandasoft;
using System.Reflection;
using System.Data.SqlClient;
using Nandasoft.BaseModule;
using Nandasoft.Helper;

namespace WebUI
{
    public class SiteBasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
            string path = PageHelper.GetRelativeLevel();
            string scriptPath = path + "Platform/script";

            ClientScript.RegisterClientScriptInclude("common", scriptPath + "/common.js");
            ClientScript.RegisterClientScriptInclude("popMessage", scriptPath + "/PopMessage.js");
            ClientScript.RegisterClientScriptInclude("message", scriptPath + "/Message.js");

            string sc = string.Empty;
            switch (this.BrowserVersion)
            {
                case 6:
                    sc = "/ModalWindow_ie6.js";
                    break;
                case 7:
                    sc = "/ModalWindow_ie7.js";
                    break;
                default:
                    sc = "/ModalWindow_ie6.js";
                    break;
            }
            CheckLogin();
            ClientScript.RegisterClientScriptInclude("modalWindow", scriptPath + sc);
           
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        private void CheckLogin()
        {
            string checkLogin = System.Configuration.ConfigurationManager.AppSettings["checkLogin"];
            if (checkLogin != null &&
                (checkLogin == "0" ||
                checkLogin.ToLower() == "false")) return;

            string path = PageHelper.GetRelativeLevel();
            if (System.Web.HttpContext.Current.Session == null ||
                System.Web.HttpContext.Current.Session.Count < 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "LoseSession", "<script>getDocument(true).location = \"" + path + "Default.aspx\";</script>");
            }
        }

        #region Web Form Designer generated code
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 预初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            this.Theme = "Site";
            base.OnPreInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 当前浏览器版本
        /// </summary>
        public int BrowserVersion
        {
            get
            {
                if (this.ViewState["BrowserVersion"] == null) this.ViewState["BrowserVersion"] = 6;
                return NDConvert.ToInt32(this.ViewState["BrowserVersion"]); ;
            }
            set
            {
                this.ViewState["BrowserVersion"] = value;
            }
        }

        /// <summary>
        /// 是否允许设置默认焦点，默认焦点为第一个可操作的表单控件
        /// </summary>
        public bool AllowSetPageFirstFocus
        {
            get
            {
                if (this.ViewState["AllowSetPageFirstFocus"] == null) return true;
                return NDConvert.ToBoolean(this.ViewState["AllowSetPageFirstFocus"]);
            }
            set
            {
                this.ViewState["AllowSetPageFirstFocus"] = value;
            }
        }
        #endregion

    }
}
