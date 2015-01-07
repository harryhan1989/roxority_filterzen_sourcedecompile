using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
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
	/// <summary>
	/// Ŀ��: WEBҳ�����.
	/// ��д����: 2007.4.11.
	/// </summary>
    public class _BasePage : System.Web.UI.Page
    {
        #region Web Form Designer generated code
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Ԥ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            this.BrowserVersion = PageHelper.GetClientBrowserVersion();
            //����������汾��������
            switch (this.BrowserVersion)
            { 
                case 6:
                    this.Theme = "IE6Default";
                    break;
                case 7:
                    this.Theme = "IE7Default";
                    break;
                default:
                    this.Theme = "IE6Default";
                    break;
            }
            
            base.OnPreInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ǰ������汾
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
        /// �Ƿ���������Ĭ�Ͻ��㣬Ĭ�Ͻ���Ϊ��һ���ɲ����ı��ؼ�
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

        /// <summary>
        /// ҳ��������������ʵ���ID�����ڿ�����Ա����Ĵ��ʵ��ID����ViewStateʵ�֣�ViewState���ƣ�EntityID��
        /// </summary>
        public long EntityID
        {
            get
            {
                return NDConvert.ToInt64(this.ViewState["EntityID"]);
            }
            set
            {
                this.ViewState["EntityID"] = value;
            }
        }

        /// <summary>
        /// ҳ�����״̬�����ڿ�����Ա����Ĵ�ŵ�ǰҳ��Ĳ���״̬����ViewStateʵ�֣�ViewState���ƣ�Operation��
        /// </summary>
        public int Operation
        {
            get
            {
                return NDConvert.ToInt32(this.ViewState["Operation"]);
            }
            set
            {
                this.ViewState["Operation"] = value;
            }
        }

        /// <summary>
        /// ��ʾ�ô��ڵ�ˮƽ������
        /// </summary>
        public bool ShowHorizontalScrollbar
        {
            get
            {
                if (this.ViewState["ShowHorizontalScrollbar"] == null) return false;
                return NDConvert.ToBoolean(this.ViewState["ShowHorizontalScrollbar"]);
            }
            set
            {
                this.ViewState["ShowHorizontalScrollbar"] = value;
            }
        }
        #endregion

        private void Page_Load(object sender, System.EventArgs e)
        {
            //��IE6�����У�����ҳ��ʱ���Ƿ���ʾˮƽ������
            this.RemoveHorizontalScrollbar(this.ShowHorizontalScrollbar);
            
            string path = PageHelper.GetRelativeLevel();
            string scriptPath = path + "Platform/script";

            ClientScript.RegisterClientScriptInclude("common", scriptPath + "/common.js");
            ClientScript.RegisterClientScriptInclude("popMessage", scriptPath + "/PopMessage.js");
            ClientScript.RegisterClientScriptInclude("message", scriptPath + "/Message.js");

            StringBuilder scri = new StringBuilder();
            scri.AppendLine("setPageFirstFocus();");
            if (this.AllowSetPageFirstFocus) ClientScript.RegisterStartupScript(this.GetType(), "setFocus", scri.ToString(), true);

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
            ClientScript.RegisterClientScriptInclude("modalWindow", scriptPath + sc);
            
            ClientScript.RegisterHiddenField("_lockDropdownlist", "");

            CheckLogin();
            
            //Toolbar������ʽ��У��Ȩ��
            RecursionLoadToolbar(this.Form.Controls);
            //UltraWebToolbar������ʽ��У��Ȩ�� Ϊ����Ŀ���������ԣ�����Ŀ����UltraWebToolbar�ؼ�
            RecursionLoadUltraWebToolbar(this.Form.Controls);
            
            if (!this.IsPostBack)
            {

            }
        }

        /// <summary>
        /// ��IE6�����У�����ҳ��ʱ(�༭ҳ���̳�IPageEdit�ӿ�)���Ƿ���ʾˮƽ������
        /// </summary>
        /// <param name="showHorizontalScrollbar">�Ƿ���ʾˮƽ������</param>
        private void RemoveHorizontalScrollbar(bool showHorizontalScrollbar)
        {
            bool isEditPage = this.GetType().GetInterface("Nandasoft.IPageEdit") != null;
            if (this.BrowserVersion == 6 && isEditPage && !showHorizontalScrollbar)
            {
                LiteralControl lic = new LiteralControl();
                lic.Text = "\r\n<style>html{overflow-x: hidden; overflow-y: auto; }</style>\r\n";
                this.Header.Controls.Add(lic);
            }
        }

        private void RecursionLoadToolbar(ControlCollection controls)
        {
            if (controls.Count == 0) return;
            foreach (Control ctl in controls)
            {
                if (ctl is Nandasoft.WebControls.NDToolbar)
                {
                    Nandasoft.WebControls.NDToolbar toolbar = (Nandasoft.WebControls.NDToolbar)ctl;
                    PageHelper.LoadToolbar(toolbar);
                }
                
                RecursionLoadToolbar(ctl.Controls);
            }
        }

        private void RecursionLoadUltraWebToolbar(ControlCollection controls)
        {
            if (controls.Count == 0) return;
            foreach (Control ctl in controls)
            {
                if (ctl is Infragistics.WebUI.UltraWebToolbar.UltraWebToolbar)
                {
                    Infragistics.WebUI.UltraWebToolbar.UltraWebToolbar toolbar = (Infragistics.WebUI.UltraWebToolbar.UltraWebToolbar)ctl;
                    PageHelper.LoadToolbar(toolbar);
                }

                RecursionLoadUltraWebToolbar(ctl.Controls);
            }
        }

        /// <summary>
        /// ����Ƿ��¼
        /// </summary>
        public void CheckLogin()
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
    }
}