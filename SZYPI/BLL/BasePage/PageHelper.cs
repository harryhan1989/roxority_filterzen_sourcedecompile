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
    /// 页面常用方法包装
    /// </summary>
    public class PageHelper
    {
        #region 选择组织

        /// <summary>
        /// 选择组织
        /// </summary>
        /// <param name="webChooser">响应事件调用选择页的选择控件(回写的前面两列的值写入NDChooser的Tag和Text)</param>
        public static void SetSelectOrganization(NDChooser webChooser)
        {
            SetSelectOrganization(webChooser, false);
        }
        
        /// <summary>
        /// 选择组织
        /// </summary>
        /// <param name="webChooser">响应事件调用选择页的选择控件(回写的前面两列的值写入NDChooser的Tag和Text)</param>
        /// <param name="allowMultiSelect">允许多选</param>
        public static void SetSelectOrganization(NDChooser webChooser, bool allowMultiSelect)
        {
            webChooser.ReadOnly = true;
            webChooser.ImageUrl = "../images/icon/selectTeam.gif";
            
            StringBuilder js = new StringBuilder();

            js.Append(GetShowModalWindowScript("orgSelect", "选择组织", 320, 420,
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

        #region 选择人员

        /// <summary>
        /// 选择人员
        /// </summary>
        /// <param name="webChooser">响应事件调用选择页的选择控件(回写的前面两列的值写入NDChooser的Value和Text)</param>
        public static void SetSelectEmployee(NDChooser webChooser)
        {
            SetSelectEmployee(webChooser,false);
        }

        /// <summary>
        /// 选择人员
        /// </summary>
        /// <param name="webChooser">响应事件调用选择页的选择控件(回写的前面两列的值写入NDChooser的Value和Text)</param>
        /// <param name="allowMultiSelect">允许多选</param>
        public static void SetSelectEmployee(NDChooser webChooser, bool allowMultiSelect)
        {
            webChooser.ReadOnly = true;

            StringBuilder js = new StringBuilder();
            webChooser.ImageUrl = allowMultiSelect ? "../images/icon/messageSelect.jpg" : "../images/icon/selectPeople.gif";
            webChooser.CssClass = allowMultiSelect ? "inputStyTextMulSelect" : "inputStyText";

            string url = allowMultiSelect ? "ChooseEmployee.aspx" : "ChooseEmployeeSingle.aspx";
            int width = allowMultiSelect ? 460 : 300;

            js.Append(GetShowModalWindowScript("empSelect", "选择人员", width, 420,
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

        #region 提示信息显示

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        public static void ShowMessage(string message)
        {
            ShowMessage(message,4000);
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="delayms">延迟关闭时间</param>
        private static void ShowMessage(string message, int delayms)
        {
            WriteScript(string.Format("showShortMessage('{0}'," + delayms + ");", message));
        }
        #endregion
        
        #region 消息显示

        /// <summary>
        /// 显示在线消息
        /// </summary>
        /// <param name="messages">消息标题，多个用,分割</param>
        /// <param name="links">消息链接（可选），多个用,分割</param>
        public static void ShowPopMessage(string messages,string links)
        {
            ShowPopMessage("在线消息", 180, 120, messages, links, 5000, -1);
        }

        /// <summary>
        /// 显示在线消息
        /// </summary>
        /// <param name="title">弹出框标题</param>
        /// <param name="width">弹出框宽度</param>
        /// <param name="height">弹出框高度</param>
        /// <param name="titles">消息标题，多个用,分割</param>
        /// <param name="links">消息链接（可选），多个用,分割</param>
        /// <param name="delayms">消息显示延迟</param>
        /// <param name="leftSpace">左边距</param>
        private static void ShowPopMessage(string title, int width, int height, string titles, string links, int delayms, int leftSpace)
        {
            WriteScript(string.Format("popMessage2({0},{1},'{2}','{3}','{4}',{5},{6});", width, height, title, titles, links, delayms, leftSpace == -1 ? "null" : leftSpace.ToString()));
        }

        #endregion

        #region 显示异常信息

        /// <summary>
        /// 显示异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowExceptionMessage(Exception ex)
        {
            PageHelper.ShowExceptionMessage(ex.Message);
        }

        /// <summary>
        /// 显示异常信息
        /// </summary>
        /// <param name="message"></param>
        public static void ShowExceptionMessage(string message)
        {
            WriteScript("alert('" + message + "');");
            //PageHelper.ShowExceptionMessage("错误提示", 210, 125, message);
        }

        /// <summary>
        /// 显示异常信息
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

        #region 显示模态窗口

        /// <summary>
        /// 返回把指定链接地址显示模态窗口的脚本
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
        /// 把指定链接地址显示模态窗口
        /// </summary>
        /// <param name="wid">窗口ID</param>
        /// <param name="title">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="url">链接地址</param>
        public static void ShowModalWindow(string wid, string title, int width, int height, string url)
        {
            WriteScript(GetShowModalWindowScript(wid, title, width, height, url));
        }

        /// <summary>
        /// 为指定控件绑定前台脚本：显示模态窗口
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
        /// 为指定控件绑定前台脚本：显示模态窗口
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        /// <param name="onClosedFunArg">当窗口关闭时，执行自定义函数时需要的参数</param>
        public static void ShowCilentModalWindow(string wid, WebControl control, string eventName, string title, int width, int height, string url, bool isScriptEnd, string onClosedFunArg)
        {
            string script = isScriptEnd ? "return false;" : "";
            control.Attributes[eventName] = string.Format("showModalWindow2('{0}','{1}',{2},{3},'{4}','{5}');" + script, wid, title, width, height, url, onClosedFunArg);
        }

        /// <summary>
        /// 为指定控件绑定前台脚本：显示模态窗口
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
        /// 为指定控件绑定前台脚本：显示模态窗口
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="cell"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        /// <param name="onClosedFunArg">当窗口关闭时，执行自定义函数时需要的参数</param>
        public static void ShowCilentModalWindow(string wid, TableCell cell, string eventName, string title, int width, int height, string url, bool isScriptEnd, string onClosedFunArg)
        {
            string script = isScriptEnd ? "return false;" : "";
            cell.Attributes[eventName] = string.Format("showModalWindow('{0}','{1}',{2},{3},'{4}');" + script, wid, title, width, height, url, onClosedFunArg);
        }
        #endregion

        #region 显示客户端确认窗口
        /// <summary>
        /// 显示客户端确认窗口
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string message)
        {
            ShowCilentConfirm(control, eventName, "系统提示", 210, 125, message);
        }

        /// <summary>
        /// 显示客户端确认窗口
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
        /// 显示客户端确认窗口
        /// </summary>
        /// <param name="toolbar"></param>
        /// <param name="value"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(Nandasoft.WebControls.NDToolbar toolbar, string value, string message)
        {
            ShowCilentConfirm(toolbar, value, "系统提示", 210, 125, message);
        }

        /// <summary>
        /// 显示客户端确认窗口
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

        #region 控件状态设置

        /// <summary>
        /// 锁定页面上的一些组件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">不需锁定的控件</param>
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
                    //锁定
                    LockControl(page, ctl);
                }
                else
                {
                    //解除锁定
                    UnLockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// 解除锁定页面上的一些组件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="obj">继续保持锁定的控件</param>
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
                    //解除锁定
                    UnLockControl(page, ctl);
                }
                else
                {
                    //锁定
                    LockControl(page, ctl);
                }
            }
        }

        /// <summary>
        /// 禁用控件
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
        /// 禁用控件
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

                #region 多行文本框不能禁用，应设为只读，不然滚动条不能使用

                if (ctl is TextBox)
                {
                    if (((TextBox)ctl).TextMode == TextBoxMode.MultiLine)
                    {
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = true;
                    }
                }

                #endregion

                #region 时间控件禁用时不显示图片

                //时间输入文本框禁用时不显示按钮
                if (ctl is WebDateTimeEdit)
                {
                    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.None;
                }

                //时间选择文本框禁用时不显示按钮
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
        /// 开放控件
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

                //文本框去掉只读属性
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).ReadOnly = false;
                }

                //时间输入文本框不禁用时显示按钮
                if (ctl is WebDateTimeEdit)
                {
                    ((WebDateTimeEdit)ctl).SpinButtons.Display = ButtonDisplay.OnRight;
                }

                //时间选择文本框不禁用时显示按钮
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
        /// 数组中是否包含当前控件
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

        #region 页面处理其它辅助方法
        /// <summary>
        /// 得到机器IP
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
        /// 得到机器名
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
        /// 返回客户端浏览器版本
        /// 如果是IE类型，返回版本数字
        /// 如果不是IE类型，返回-1
        /// </summary>
        /// <returns>一位数字版本号</returns>
        public static int GetClientBrowserVersion()
        {
            string USER_AGENT = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            if (USER_AGENT.IndexOf("MSIE") < 0) return -1;

            string version = USER_AGENT.Substring(USER_AGENT.IndexOf("MSIE") + 5, 1);
            if (!Nandasoft.Helper.NDHelperDataCheck.IsInt32(version)) return -1;

            return Convert.ToInt32(version);
        }
        
        /// <summary>
        /// 得到当前页对象实例
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }

        /// <summary>
        /// 从System.Web.HttpRequest的Url中获取所调用的页面名称
        /// </summary>
        /// <returns>页面名称</returns>
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
        /// 读取QueryString值
        /// </summary>
        /// <param name="queryStringName">QueryString名称</param>
        /// <returns>QueryString值</returns>
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
        /// 页面跳转
        /// </summary>
        /// <param name="url">URL地址</param>
        public void Redirect(string url)
        {
            Page page = GetCurrentPage();
            page.Response.Redirect(url);
        }

        /// <summary>
        /// 获取当前请求页面相对于根目录的层级
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
        /// 写javascript脚本
        /// </summary>
        /// <param name="script">脚本内容</param>
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

        #region 载入Toolbar控件并校验权限
        /// <summary>
        /// 加载Toolbar样式和判断权限
        /// </summary>
        /// <param name="page"></param>
        public static void LoadToolbar(Infragistics.WebUI.UltraWebToolbar.UltraWebToolbar toolBar)
        {
            if (toolBar == null) return;//没有需要校验权限的控件

            //添加按钮样式
            foreach (object obj in toolBar.Items)
            {
                if (obj.GetType().GetInterface("Infragistics.WebUI.UltraWebToolbar.ITBarButton") == null) continue;

                Infragistics.WebUI.UltraWebToolbar.ITBarButton tButton = (Infragistics.WebUI.UltraWebToolbar.ITBarButton)obj;
                AddToolBarButtonStyle(tButton);
            }

            //if (WebUI.SessionState.IsAdmin) return;//管理员不需要校验权限

            //string userID = SessionState.UserID.ToString();

            //string pageName = HttpContext.Current.Request.RawUrl;
            //int index = pageName.LastIndexOf("/");
            //pageName = pageName.Substring(index + 1, pageName.Length - index - 1);

            //long menuID = new Nandasoft.BaseModule.RightRule().GetMenuIDByPageName(pageName);

            ////得到所有权限
            //DataTable dt = new RightRule().GetAllowControlRightsItems(menuID);
            //if (dt.Rows.Count < 1) return;//没有需要校验的权限

            //List<string> rights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    rights.Add(dr["ItemKey"].ToString());
            //}

            ////得到拥有权限
            //dt = new RightRule().GetPageKeyRight(userID, menuID.ToString(), pageName, "");
            //List<string> myRights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myRights.Add(dr["ItemKey"].ToString());
            //}

            ////根据权限显示控件
            //foreach (object obj in toolBar.Items)
            //{
            //    if (obj.GetType().GetInterface("Infragistics.WebUI.UltraWebToolbar.ITBarButton") == null) continue;
            //    Infragistics.WebUI.UltraWebToolbar.ITBarButton tButton = (Infragistics.WebUI.UltraWebToolbar.ITBarButton)obj;

            //    if (!rights.Contains(tButton.Button.Key)) continue;
            //    tButton.Button.Visible = myRights.Contains(tButton.Button.Key);
            //}
        }

        /// <summary>
        /// 加载Toolbar样式和判断权限
        /// </summary>
        /// <param name="toolbar"></param>
        public static void LoadToolbar(Nandasoft.WebControls.NDToolbar toolbar)
        {
            if (toolbar == null) return;//没有需要校验权限的控件

            //添加按钮样式
            int i = 0;
            foreach (MenuItem item in toolbar.Items)
            {
                AddToolbarItemStyle(item, i != toolbar.Items.Count - 1);
                i++;
            }

            if (WebUI.SessionState.IsAdmin) return;//管理员不需要校验权限

            string userID = SessionState.UserID.ToString();

            string pageName = HttpContext.Current.Request.RawUrl;
            int index = pageName.LastIndexOf("/");
            pageName = pageName.Substring(index + 1, pageName.Length - index - 1);

            //long menuID = new RightRule().GetMenuIDByPageName(pageName);

            ////得到所有权限
            //DataTable dt = new RightRule().GetAllowControlRightsItems(menuID);
            //if (dt.Rows.Count < 1) return;//没有需要校验的权限

            //List<string> rights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    rights.Add(dr["ItemKey"].ToString());
            //}

            ////得到拥有权限
            //dt = new RightRule().GetPageKeyRight(userID, menuID.ToString(), pageName, "");
            //List<string> myRights = new List<string>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myRights.Add(dr["ItemKey"].ToString());
            //}

            ////根据权限显示控件
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

        #region Gridview导出Excel
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
                        dr[i] = "否";
                    }
                    if (dr[i].ToString().ToLower().Trim() == "true")
                    {
                        dr[i] = "是";
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

                //只有可见的邦定列和选择列才输出
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
                                        sb.Append("<td>是</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td>否</td>");
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
