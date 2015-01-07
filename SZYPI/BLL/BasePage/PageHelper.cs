using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

using Nandasoft;
using Nandasoft.BaseModule;
using Nandasoft.Helper;
using Nandasoft.WebControls;
using BLL;

using Infragistics.WebUI.WebDataInput;
using Infragistics.WebUI.WebSchedule;

namespace WebUI
{
    /// <summary>
    /// ҳ�泣�÷�����װ
    /// </summary>
    public class PageHelper
    {
        #region ѡ����֯

        /// <summary>
        /// ѡ����֯
        /// </summary>
        /// <param name="webChooser">��Ӧ�¼�����ѡ��ҳ��ѡ��ؼ�(��д��ǰ�����е�ֵд��NDChooser��Tag��Text)</param>
        public static void SetSelectOrganization(NDChooser webChooser)
        {
            SetSelectOrganization(webChooser, false);
        }
        
        /// <summary>
        /// ѡ����֯
        /// </summary>
        /// <param name="webChooser">��Ӧ�¼�����ѡ��ҳ��ѡ��ؼ�(��д��ǰ�����е�ֵд��NDChooser��Tag��Text)</param>
        /// <param name="allowMultiSelect">�����ѡ</param>
        public static void SetSelectOrganization(NDChooser webChooser, bool allowMultiSelect)
        {
            webChooser.ReadOnly = true;
            webChooser.ImageUrl = "../images/icon/selectTeam.gif";
            
            StringBuilder js = new StringBuilder();

            js.Append(GetShowModalWindowScript("orgSelect", "ѡ����֯", 320, 420,
                string.Format("../BaseModule/ChooseOrganization.aspx?winid={0}&allowMultiSelect={1}&namecid={2}&valuecid={3}&value={4}",
                //GetRelativeLevel(),
                "orgSelect",
                allowMultiSelect,
                webChooser.TextClientID,
                webChooser.ValueClientID,
                webChooser.Value)));

            webChooser.ChooseScript = js.ToString();

            //SetClearScript(webChooser);
        }

        #endregion

        #region ѡ����Ա

        /// <summary>
        /// ѡ����Ա
        /// </summary>
        /// <param name="webChooser">��Ӧ�¼�����ѡ��ҳ��ѡ��ؼ�(��д��ǰ�����е�ֵд��NDChooser��Value��Text)</param>
        public static void SetSelectEmployee(NDChooser webChooser)
        {
            SetSelectEmployee(webChooser,false);
        }

        /// <summary>
        /// ѡ����Ա
        /// </summary>
        /// <param name="webChooser">��Ӧ�¼�����ѡ��ҳ��ѡ��ؼ�(��д��ǰ�����е�ֵд��NDChooser��Value��Text)</param>
        /// <param name="allowMultiSelect">�����ѡ</param>
        public static void SetSelectEmployee(NDChooser webChooser, bool allowMultiSelect)
        {
            webChooser.ReadOnly = true;

            StringBuilder js = new StringBuilder();
            webChooser.ImageUrl = allowMultiSelect ? "../images/icon/messageSelect.jpg" : "../images/icon/selectPeople.gif";
            webChooser.CssClass = allowMultiSelect ? "inputStyTextMulSelect" : "inputStyText";

            string url = allowMultiSelect ? "ChooseEmployee.aspx" : "ChooseEmployeeSingle.aspx";
            int width = allowMultiSelect ? 460 : 300;

            js.Append(GetShowModalWindowScript("empSelect", "ѡ����Ա", width, 420,
                string.Format("../BaseModule/" + url + "?winid={0}&namecid={1}&valuecid={2}&value={3}",
                //GetRelativeLevel(),
                "empSelect",
                webChooser.TextClientID,
                webChooser.ValueClientID,
                webChooser.Value)));

            webChooser.ChooseScript = js.ToString();

            //SetClearScript(webChooser);
        }
        #endregion

        #region ��ʾ��Ϣ��ʾ

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowMessage(string message)
        {
            ShowMessage(message,4000);
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="delayms">�ӳٹر�ʱ��</param>
        private static void ShowMessage(string message, int delayms)
        {
            WriteScript(string.Format("showShortMessage('{0}'," + delayms + ");", message));
        }
        #endregion
        
        #region ��Ϣ��ʾ

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="messages">��Ϣ���⣬�����,�ָ�</param>
        /// <param name="links">��Ϣ���ӣ���ѡ���������,�ָ�</param>
        public static void ShowPopMessage(string messages,string links)
        {
            ShowPopMessage("������Ϣ", 180, 120, messages, links, 5000, -1);
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="title">���������</param>
        /// <param name="width">��������</param>
        /// <param name="height">������߶�</param>
        /// <param name="titles">��Ϣ���⣬�����,�ָ�</param>
        /// <param name="links">��Ϣ���ӣ���ѡ���������,�ָ�</param>
        /// <param name="delayms">��Ϣ��ʾ�ӳ�</param>
        /// <param name="leftSpace">��߾�</param>
        private static void ShowPopMessage(string title, int width, int height, string titles, string links, int delayms, int leftSpace)
        {
            WriteScript(string.Format("popMessage2({0},{1},'{2}','{3}','{4}',{5},{6});", width, height, title, titles, links, delayms, leftSpace == -1 ? "null" : leftSpace.ToString()));
        }

        #endregion

        #region ��ʾ�쳣��Ϣ

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowExceptionMessage(Exception ex)
        {
            PageHelper.ShowExceptionMessage(ex.Message);
        }

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="message"></param>
        public static void ShowExceptionMessage(string message)
        {
            WriteScript("alert('" + message + "');");
            //PageHelper.ShowExceptionMessage("������ʾ", 210, 125, message);
        }

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        private static void ShowExceptionMessage(string title, int width, int height, string message)
        {
            WriteScript(string.Format("setTimeout(\"showAlert('{0}',{1},{2},'{3}')\",100);", title, width, height, message));
        }
        #endregion

        #region ��ʾģ̬����

        /// <summary>
        /// ���ذ�ָ�����ӵ�ַ��ʾģ̬���ڵĽű�
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        public static string GetShowModalWindowScript(string wid, string title, int width, int height, string url)
        {
            return string.Format("setTimeout(\"showModalWindow('{0}','{1}',{2},{3},'{4}')\",100);", wid, title, width, height, url);
        }

        /// <summary>
        /// ��ָ�����ӵ�ַ��ʾģ̬����
        /// </summary>
        /// <param name="wid">����ID</param>
        /// <param name="title">����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <param name="url">���ӵ�ַ</param>
        public static void ShowModalWindow(string wid, string title, int width, int height, string url)
        {
            WriteScript(GetShowModalWindowScript(wid, title, width, height, url));
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        public static void ShowCilentModalWindow(string wid, WebControl control, string eventName, string title, int width, int height, string url, bool isScriptEnd)
        {
            ShowCilentModalWindow(wid, control, eventName, title, width, height, url, isScriptEnd, "");
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        /// <param name="onClosedFunArg">�����ڹر�ʱ��ִ���Զ��庯��ʱ��Ҫ�Ĳ���</param>
        public static void ShowCilentModalWindow(string wid, WebControl control, string eventName, string title, int width, int height, string url, bool isScriptEnd, string onClosedFunArg)
        {
            string script = isScriptEnd ? "return false;" : "";
            control.Attributes[eventName] = string.Format("showModalWindow2('{0}','{1}',{2},{3},'{4}','{5}');" + script, wid, title, width, height, url, onClosedFunArg);
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="eventName"></param>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        public static void ShowCilentModalWindow(string wid, TableCell cell, string eventName, string title, int width, int height, string url, bool isScriptEnd)
        {
            ShowCilentModalWindow(wid, cell, eventName, title, width, height, url, isScriptEnd, "");
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="cell"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        /// <param name="onClosedFunArg">�����ڹر�ʱ��ִ���Զ��庯��ʱ��Ҫ�Ĳ���</param>
        public static void ShowCilentModalWindow(string wid, TableCell cell, string eventName, string title, int width, int height, string url, bool isScriptEnd, string onClosedFunArg)
        {
            string script = isScriptEnd ? "return false;" : "";
            cell.Attributes[eventName] = string.Format("showModalWindow('{0}','{1}',{2},{3},'{4}');" + script, wid, title, width, height, url, onClosedFunArg);
        }
        #endregion

        #region ��ʾ�ͻ���ȷ�ϴ���
        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string message)
        {
            ShowCilentConfirm(control, eventName, "ϵͳ��ʾ", 210, 125, message);
        }

        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string title, int width, int height, string message)
        {
            control.Attributes[eventName] = string.Format("return showConfirm('{0}',{1},{2},'{3}','{4}');", title, width, height, message, control.ClientID);
        }

        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="toolbar"></param>
        /// <param name="value"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(Nandasoft.WebControls.NDToolbar toolbar, string value, string message)
        {
            ShowCilentConfirm(toolbar, value, "ϵͳ��ʾ", 210, 125, message);
        }

        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="toolbar"></param>
        /// <param name="value"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(Nandasoft.WebControls.NDToolbar toolbar, string value, string title, int width, int height, string message)
        {
            Page page = PageHelper.GetCurrentPage();

            System.Text.StringBuilder scripts = new StringBuilder();
            scripts.Append("function toolbar_Click(toolbarItem){");
            scripts.Append("if (toolbarItem.value == '" + value + "'){");
            scripts.Append("showToolbarConfirm('" + title + "'," + width + "," + height + ",'" + message + "','" + toolbar.ClientID + "','" + value + "');");
            scripts.Append("toolbarItem.needPostBack = false;}}");

            page.ClientScript.RegisterClientScriptBlock(typeof(Page), "confirm", scripts.ToString(),true);
        }
        #endregion

        #region �ؼ�״̬����

        /// <summary>
        /// ����ҳ���ϵ�һЩ���
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">���������Ŀؼ�</param>
        public static void LockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //����
                    LockControl(page, ctl);
                }
                else
                {
                    //�������
                    UnLockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// �������ҳ���ϵ�һЩ���
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">�������������Ŀؼ�</param>
        public static void UnLockPage(Page page, object[] obj)
        {
            Control htmlForm = null;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is HtmlForm)
                {
                    htmlForm = ctl;
                    break;
                }
            }
            //foreach (Control ctl in page.Controls[1].Controls)
            foreach (Control ctl in htmlForm.Controls)
            {
                if (IsContains(obj, ctl) == false)
                {
                    //�������
                    UnLockControl(page, ctl);
                }
                else
                {
                    //����
                    LockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// ���ÿؼ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        public static void LockControls(Page page, object[] obj)
        {
            foreach (Control ctl in obj)
            {
                LockControl(page, ctl);
            }
        }

        /// <summary>
        /// ���ÿؼ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void LockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox || ctl is WebTextEdit || ctl is WebDateChooser 
                || ctl is NDChooser || ctl is NDGridView)
            {
                ((WebControl)ctl).Enabled = false;

                #region �����ı����ܽ��ã�Ӧ��Ϊֻ������Ȼ����������ʹ��

                if (ctl is TextBox)
                {
                    if (((TextBox)ctl).TextMode == TextBoxMode.MultiLine)
                    {
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = true;
                    }
                }

                #endregion

                #region ʱ��ؼ�����ʱ����ʾͼƬ

                //ʱ�������ı������ʱ����ʾ��ť
                if (ctl is WebDateTimeEdit)
                {
                    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.None;
                }

                //ʱ��ѡ���ı������ʱ����ʾ��ť
                if (ctl is WebDateChooser)
                {
                    page.ClientScript.RegisterStartupScript(typeof(string), "Hide" + ctl.ClientID + "Image", "<script language=javascript>" +
                        "document.getElementById('" + ctl.ClientID + "_img" + "').style.display='none';</script>");
                }

                #endregion
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = true;
            }
        }

        /// <summary>
        /// ���ſؼ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctl"></param>
        private static void UnLockControl(Page page, Control ctl)
        {
            //WebControl
            if (ctl is Button || ctl is CheckBox || ctl is HyperLink || ctl is LinkButton
                || ctl is ListControl || ctl is TextBox || ctl is WebTextEdit || ctl is WebDateChooser
                || ctl is NDChooser || ctl is NDGridView)
            {
                ((WebControl)ctl).Enabled = true;

                //�ı���ȥ��ֻ������
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).ReadOnly = false;
                }

                //ʱ�������ı��򲻽���ʱ��ʾ��ť
                if (ctl is WebDateTimeEdit)
                {
                    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.OnRight;
                }

                //ʱ��ѡ���ı��򲻽���ʱ��ʾ��ť
                if (ctl is WebDateChooser)
                {
                    page.ClientScript.RegisterStartupScript(typeof(string), "Display" + ctl.ClientID + "Image", "<script language=javascript>" +
                        "document.getElementById('" + ctl.ClientID + "_img" + "').style.display='';</script>");
                }
            }

            //HtmlControl
            if (ctl is HtmlInputFile)
            {
                ((HtmlInputFile)ctl).Disabled = false;
            }
        }

        /// <summary>
        /// �������Ƿ������ǰ�ؼ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ctl"></param>
        /// <returns></returns>
        private static bool IsContains(object[] obj, Control ctl)
        {
            foreach (Control c in obj)
            {
                if (c.ID == ctl.ID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ҳ�洦��������������
        /// <summary>
        /// �õ�����IP
        /// </summary>
        /// <returns></returns>
        public static string GetMachineIP()
        {
            string userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (userIP == null || userIP == "")
            {
                userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return userIP;

        }

        /// <summary>
        /// �õ�������
        /// </summary>
        /// <returns></returns>
        public static string GetMachineName()
        {
            //string mName = HttpContext.Current.Server.MachineName.ToString();
          string mName = HttpContext.Current.Request.ServerVariables["Auth_User"];
            //string mName = HttpContext.Current.Request.UserHostName;
           

            return mName == "" ? "" : mName.Split('\\')[0];
        }

        /// <summary>
        /// ���ؿͻ���������汾
        /// �����IE���ͣ����ذ汾����
        /// �������IE���ͣ�����-1
        /// </summary>
        /// <returns>һλ���ְ汾��</returns>
        public static int GetClientBrowserVersion()
        {
            string USER_AGENT = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            if (USER_AGENT.IndexOf("MSIE") < 0) return -1;

            string version = USER_AGENT.Substring(USER_AGENT.IndexOf("MSIE") + 5, 1);
            if (!Nandasoft.Helper.NDHelperDataCheck.IsInt32(version)) return -1;

            return Convert.ToInt32(version);
        }
        
        /// <summary>
        /// �õ���ǰҳ����ʵ��
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }

        /// <summary>
        /// ��System.Web.HttpRequest��Url�л�ȡ�����õ�ҳ������
        /// </summary>
        /// <returns>ҳ������</returns>
        public static string GetPageName()
        {
            int start = 0;
            int end = 0;
            string Url = HttpContext.Current.Request.RawUrl;
            start = Url.LastIndexOf("/") + 1;
            end = Url.IndexOf("?");
            if (end <= 0)
            {
                return Url.Substring(start, Url.Length - start);
            }
            else
            {
                return Url.Substring(start, end - start);
            }
        }

        /// <summary>
        /// ��ȡQueryStringֵ
        /// </summary>
        /// <param name="queryStringName">QueryString����</param>
        /// <returns>QueryStringֵ</returns>
        public static string GetQueryString(string queryStringName)
        {
            if ((HttpContext.Current.Request.QueryString[queryStringName] != null) &&
                (HttpContext.Current.Request.QueryString[queryStringName] != "undefined"))
            {
                return HttpContext.Current.Request.QueryString[queryStringName].Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// ҳ����ת
        /// </summary>
        /// <param name="url">URL��ַ</param>
        public void Redirect(string url)
        {
            Page page = GetCurrentPage();
            page.Response.Redirect(url);
        }

        /// <summary>
        /// ��ȡ��ǰ����ҳ������ڸ�Ŀ¼�Ĳ㼶
        /// </summary>
        /// <returns></returns>
        public static string GetRelativeLevel()
        {
            string ApplicationPath = HttpContext.Current.Request.ApplicationPath;
            if (ApplicationPath.Trim() == "/")
            {
                ApplicationPath = "";
            }

            int i = ApplicationPath == "" ? 1 : 2;
            return Nandasoft.Helper.NDHelperString.Repeat("../", Nandasoft.Helper.NDHelperString.RepeatTime(HttpContext.Current.Request.Path, "/") - i);
        }
        
        /// <summary>
        /// дjavascript�ű�
        /// </summary>
        /// <param name="script">�ű�����</param>
        public static void WriteScript(string script)
        {
            Page page = GetCurrentPage();
            
            new WebUI.PageHelper().NDGridViewScriptFirst(page.Form.Controls, page);
            
            ScriptManager.RegisterStartupScript(page, page.GetType(), System.Guid.NewGuid().ToString(), script, true);
        }

        private void NDGridViewScriptFirst(ControlCollection ctls,Page page)
        {
            foreach (Control ctl in ctls)
            {
                if (ctl is NDGridView)
                {
                    NDGridView ndgv = (NDGridView)ctl;
                    if (ndgv.HeaderRow == null || ndgv.HeaderRow.Controls.Count == 0) continue;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), ndgv.ClientScriptKey, ndgv.ClientScriptName, true);
                }
                else
                {
                    NDGridViewScriptFirst(ctl.Controls, page);
                }
            }
        }

        #endregion

        #region ����Toolbar�ؼ���У��Ȩ��
        /// <summary>
        /// ����Toolbar��ʽ���ж�Ȩ��
        /// </summary>
        /// <param name="page"></param>
        public static void LoadToolbar(Infragistics.WebUI.UltraWebToolbar.UltraWebToolbar toolBar)
        {
            if (toolBar == null) return;//û����ҪУ��Ȩ�޵Ŀؼ�

            //��Ӱ�ť��ʽ
            foreach (object obj in toolBar.Items)
            {
                if (obj.GetType().GetInterface("Infragistics.WebUI.UltraWebToolbar.ITBarButton") == null) continue;

                Infragistics.WebUI.UltraWebToolbar.ITBarButton tButton = (Infragistics.WebUI.UltraWebToolbar.ITBarButton)obj;
                AddToolBarButtonStyle(tButton);
            }

            //if (WebUI.SessionState.IsAdmin) return;//����Ա����ҪУ��Ȩ��

            //string userID = SessionState.UserID.ToString();

            //string pageName = HttpContext.Current.Request.RawUrl;
            //int index = pageName.LastIndexOf("/");
            //pageName = pageName.Substring(index + 1, pageName.Length - index - 1);

            //long menuID = new Nandasoft.BaseModule.RightRule().GetMenuIDByPageName(pageName);

            ////�õ�����Ȩ��
            //DataTable dt = new RightRule().GetAllowControlRightsItems(menuID);
            //if (dt.Rows.Count < 1) return;//û����ҪУ���Ȩ��

            //List<string> rights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    rights.Add(dr["ItemKey"].ToString());
            //}

            ////�õ�ӵ��Ȩ��
            //dt = new RightRule().GetPageKeyRight(userID, menuID.ToString(), pageName, "");
            //List<string> myRights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myRights.Add(dr["ItemKey"].ToString());
            //}

            ////����Ȩ����ʾ�ؼ�
            //foreach (object obj in toolBar.Items)
            //{
            //    if (obj.GetType().GetInterface("Infragistics.WebUI.UltraWebToolbar.ITBarButton") == null) continue;
            //    Infragistics.WebUI.UltraWebToolbar.ITBarButton tButton = (Infragistics.WebUI.UltraWebToolbar.ITBarButton)obj;

            //    if (!rights.Contains(tButton.Button.Key)) continue;
            //    tButton.Button.Visible = myRights.Contains(tButton.Button.Key);
            //}
        }

        /// <summary>
        /// ����Toolbar��ʽ���ж�Ȩ��
        /// </summary>
        /// <param name="toolbar"></param>
        public static void LoadToolbar(Nandasoft.WebControls.NDToolbar toolbar)
        {
            if (toolbar == null) return;//û����ҪУ��Ȩ�޵Ŀؼ�

            //��Ӱ�ť��ʽ
            int i = 0;
            foreach (MenuItem item in toolbar.Items)
            {
                AddToolbarItemStyle(item, i != toolbar.Items.Count - 1);
                i++;
            }

            if (WebUI.SessionState.IsAdmin) return;//����Ա����ҪУ��Ȩ��

            string userID = SessionState.UserID.ToString();

            string pageName = HttpContext.Current.Request.RawUrl;
            int index = pageName.LastIndexOf("/");
            pageName = pageName.Substring(index + 1, pageName.Length - index - 1);

            //long menuID = new RightRule().GetMenuIDByPageName(pageName);

            ////�õ�����Ȩ��
            //DataTable dt = new RightRule().GetAllowControlRightsItems(menuID);
            //if (dt.Rows.Count < 1) return;//û����ҪУ���Ȩ��

            //List<string> rights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    rights.Add(dr["ItemKey"].ToString());
            //}

            ////�õ�ӵ��Ȩ��
            //dt = new RightRule().GetPageKeyRight(userID, menuID.ToString(), pageName, "");
            //List<string> myRights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myRights.Add(dr["ItemKey"].ToString());
            //}

            ////����Ȩ����ʾ�ؼ�
            //foreach (MenuItem item in toolbar.Items)
            //{
            //    if (!rights.Contains(item.Value)) continue;
            //    item.Enabled = myRights.Contains(item.Value);
            //}
        }
        
        private static void AddToolBarButtonStyle(Infragistics.WebUI.UltraWebToolbar.ITBarButton tButton)
        {
            string cssName = "menuBtn" + Nandasoft.Helper.NDHelperString.FirstToUpper(tButton.Button.Key.ToLower());
            tButton.Button.DefaultStyle.CssClass = tButton.Button.HoverStyle.CssClass = tButton.Button.SelectedStyle.CssClass = cssName;
        }

        private static void AddToolbarItemStyle(MenuItem item, bool addSeparatorImage)
        {
            string imgPath = "../images/icon/" + item.Value + ".gif";
            string realPath = System.Web.HttpContext.Current.Server.MapPath(imgPath);
            if (System.IO.File.Exists(realPath)) item.ImageUrl = imgPath;
            if (addSeparatorImage) item.SeparatorImageUrl = "../images/icon/menuSepImg.gif";
        }

        #endregion

        #region Gridview����Excel
        public static void OutputExcel(Nandasoft.WebControls.NDGridView gird,DataTable dt, string excelFileName)
        {
            Page page = (Page)HttpContext.Current.Handler;
            page.Response.Clear();

            string fileName = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(excelFileName));
            page.Response.AddHeader("Content-Disposition", "filename=" + fileName + ".xls");
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.Charset = "utf-8";

            StringBuilder s = new StringBuilder();
            s.Append("<HTML><HEAD><TITLE>" + fileName + "</TITLE><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body>");

        
            string FildContent = "";
            foreach (DataControlField field in gird.Columns)
            {
                if (field.GetType() == typeof(BoundField) || field.GetType() == typeof(CheckBoxField))
                {
                    BoundField col = (BoundField)field;
                    if (col.Visible)
                    {
                        FildContent += col.DataField.ToString() + "," + col.HeaderText + "*";
                    }
                }
            }          
            DataTable temp = new DataTable();
            string[] arr = FildContent.Split('*');
            for (int n = 0; n < arr.Length - 1; n++)
            {
                string[] data = arr[n].Split(',');
                temp.Columns.Add(data[0].ToString());
            }
            DataRow drr = temp.NewRow();
            DataRow drrr = temp.NewRow();
            for (int m = 0; m < arr.Length - 1; m++)
            {
                string[] da = arr[m].Split(',');
                drr[da[0].ToString()] = da[0].ToString();
                drrr[da[0].ToString()] = da[1].ToString();
            }
            temp.Rows.Add(drrr);
            temp.Rows.Add(drr);

            foreach (DataRow dr in dt.Rows)
            {
                DataRow drTemp = temp.NewRow();
                for (int a = 0; a < temp.Columns.Count; a++)
                {
                    drTemp[temp.Rows[1][a].ToString()] = dr[temp.Rows[1][a].ToString()].ToString();
                }
                temp.Rows.Add(drTemp);
            }

            temp.Rows.RemoveAt(1);

            foreach (DataRow dr in temp.Rows)
            {
                for (int i = 0; i < temp.Columns.Count; i++)
                {
                    if (dr[i].ToString().ToLower().Trim() == "false")
                    {
                        dr[i] = "��";
                    }
                    if (dr[i].ToString().ToLower().Trim() == "true")
                    {
                        dr[i] = "��";
                    }
                }
            }

            s.Append("<table>");          

            int count = temp.Columns.Count;

            foreach (DataRow dr in temp.Rows)
            {
                s.AppendLine("<tr>");
                for (int n = 0; n < count; n++)
                {

                    s.Append("<td>" + dr[n].ToString() + "</td>");

                }
                s.AppendLine("</tr>");
            }

            s.Append("</table>");
            s.Append("</body></html>");

            page.Response.BinaryWrite(System.Text.Encoding.GetEncoding("utf-8").GetBytes(s.ToString()));
            page.Response.End(); 
        }

        public static void OutputExcel(Nandasoft.WebControls.NDGridView grid, string excelFileName)
        {
            Page page = (Page)HttpContext.Current.Handler;

            string[] flag = new string[grid.Columns.Count];
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                DataControlField col = grid.Columns[i];

                //ֻ�пɼ��İ�к�ѡ���в����
                if (col.Visible && (col.GetType() == typeof(BoundField) || col.GetType() == typeof(CheckBoxField)))
                {
                    flag[i] = col.HeaderText;
                    
                }
                else
                {
                    flag[i] = "";
                }
            }
            page.Response.Clear();
            string fileName = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(excelFileName));
            page.Response.AddHeader("Content-Disposition", "filename=" + fileName + ".xls");
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.Charset = "utf-8";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<HTML><HEAD><TITLE>" + fileName + "</TITLE><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body>");
            sb.Append("<table border=1>");
            sb.Append("<tr><b>");
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (flag[i] != "") sb.Append("<td>" + grid.Columns[i].HeaderText + "</td>");
            }
            sb.Append("</b></tr>");


            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (((CheckBox)grid.Rows[i].Cells[1].FindControl("CheckBoxSelect")).Checked == true)
                {
                    sb.AppendLine("<tr>");
                    for (int j = 0; j < grid.Columns.Count; j++)
                    {
                        if (flag[j] != "")
                        {                          

                            if (grid.Rows[i].Cells[j].Controls.Count > 0)
                            {
                                if (grid.Rows[i].Cells[j].Controls[0].GetType().ToString().Trim() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    if (((CheckBox)grid.Rows[i].Cells[j].Controls[0]).Checked)
                                    {
                                        sb.Append("<td>��</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td>��</td>");
                                    }
                                }
                                else
                                {
                                    sb.Append("<td>" + grid.Rows[i].Cells[j].Text + "</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td>" + grid.Rows[i].Cells[j].Text + "</td>");
                            }
                        }
                    }
                    sb.AppendLine("</tr>");
                }
            }

            sb.Append("</table>");
            sb.Append("</body></html>");

            page.Response.BinaryWrite(System.Text.Encoding.GetEncoding("utf-8").GetBytes(sb.ToString()));
            page.Response.End(); 
        }
        #endregion        
    }
}
