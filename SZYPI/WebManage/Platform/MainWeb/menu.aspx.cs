using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using Nandasoft.Helper;
using Nandasoft;
using BLL;


namespace WebUI
{
    public partial class menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //根据用户类型加载用户菜单
                InitPage();
            }
        }

        private void InitPage()
        {
            LoadLeftMenu();
        }

        /// <summary>
        /// 加载最顶级菜单
        /// </summary>
        /// <param name="dtMenu"></param>
        private void LoadLeftMenu()
        {
            try
            {
                CommonEnum.MenuType MenuType = CommonEnum.MenuType.Inner;
                switch (SessionState.OUType)
                {
                    case (int)CommonEnum.OUType.Inner:
                    case (int)CommonEnum.OUType.Outer:  
                        MenuType = CommonEnum.MenuType.Inner;
                        break;
                }
                HtmlTable tb = new HtmlTable();
                tb.CellPadding = 0;
                tb.CellSpacing = 0;
                HtmlTableRow tr1 = null;
                HtmlTableRow tr2 = null;
                HtmlTableCell tcMenuTitle1 = null;
                HtmlTableCell tcMenuTitle2 = null;
                HtmlTableCell tcMenuList = null;

                DataTable Dt = null;
                DataTable DtLayer1 = null;

                string Url = "";
                string MenuName = "";
                int Count = 0;  //判断加载菜单的个数

                long menuID = 0;
                long UserID = SessionState.UserID;

                //一级菜单
                Dt = new MenuQuery().GetMenuAll(0, MenuType);
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    menuID = NDConvert.ToInt64(Dt.Rows[i]["MenuID"].ToString());
                    if ((RightQuery.GetUserRightQuery(UserID, menuID) || SessionState.IsAdmin) ||
                        MenuType == CommonEnum.MenuType.Outer)
                    {
                        tr1 = new HtmlTableRow();
                        tr2 = new HtmlTableRow();

                        tcMenuTitle1 = new HtmlTableCell();
                        tcMenuTitle1.Height = "24px";
                        tcMenuTitle1.Width = "167px";
                        tcMenuTitle1.Style["background"] = "url(../../images/bg_menu2_bg.jpg)";
                        tcMenuTitle1.Style["cursor"] = "hard";
                        tcMenuTitle1.InnerHtml = "&nbsp;&nbsp;&nbsp;" + Dt.Rows[i]["MenuName"].ToString();
                        tcMenuTitle1.Attributes["class"] = "Font4";

                        tcMenuTitle2 = new HtmlTableCell();
                        tcMenuTitle2.ID = "tdTitle" + i;
                        tcMenuTitle2.Height = "24px";
                        tcMenuTitle2.Width = "13px";
                        tcMenuTitle2.Attributes["background"] = "../../images/bg_menu2_down.gif";

                        //二级菜单
                        DtLayer1 = new MenuQuery().GetMenuAll(menuID, MenuType);
                        tcMenuList = new HtmlTableCell();
                        tcMenuList.ColSpan = 2;
                        tcMenuList.ID = "MenuList" + Count;
                        tcMenuList.Style["display"] = "none";

                        tr1.Attributes["onclick"] = "ControlLeftMenuItem('" + tcMenuTitle2.ID + "','" + tcMenuList.ID + "')";
                        tr1.Style["cursor"] = "hand";

                        Count++;

                        if (DtLayer1.Rows.Count > 0)
                        {
                            HtmlTable tb1 = new HtmlTable();
                            tb1.CellPadding = 0;
                            tb1.CellSpacing = 0;
                            tb1.Style["margin-left"] = "12px";
                            HtmlTableRow tr11 = null;
                            HtmlTableRow tr12 = null;
                            HtmlTableCell td11 = null;
                            HtmlTableCell td11img = null;

                            for (int j = 0; j < DtLayer1.Rows.Count; j++)
                            {
                                menuID = NDConvert.ToInt64(DtLayer1.Rows[j]["MenuID"].ToString());
                                if ((RightQuery.GetUserRightQuery(UserID, menuID) || SessionState.IsAdmin) ||
                                    MenuType == CommonEnum.MenuType.Outer)
                                {
                                    Url = DtLayer1.Rows[j]["NavigateURL"].ToString();
                                    MenuName = DtLayer1.Rows[j]["MenuName"].ToString().Trim();

                                    tr11 = new HtmlTableRow();
                                    tr12 = new HtmlTableRow();

                                    td11img = new HtmlTableCell();
                                    td11img.Width = "25px";
                                    td11img.Align = "right";
                                    td11img.VAlign = "top";
                                    td11img.InnerHtml = "<img src = '" + DtLayer1.Rows[j]["ImageURL"].ToString().Trim() + "'  />";

                                    td11 = new HtmlTableCell();
                                    td11.ID = "menuItem" + i.ToString() + j.ToString();
                                    td11.Width = "140px";
                                    td11.Height = "20px";
                                    td11.Attributes["onclick"] = "mainFrameUrl('" + Url + "');SelectedItemColor(this);";
                                    td11.Attributes["onmousemove"] = "linkMouseOver(this)";
                                    td11.Attributes["onmouseout"] = "linkMouseOut(this)";
                                    td11.Style["font-size"] = "12px";
                                    td11.Style["cursor"] = "hand";

                                    td11.InnerHtml = "<div class='hideText' style='height:22px; width:140px; margin-left:5px;'>" + MenuName + "</div>";

                                    tcMenuTitle2.Attributes["background"] = "../../images/bg_menu2_up.gif";
                                    if (Count == 1)
                                    {
                                        tcMenuList.Style["display"] = "block";
                                    }
                                    else
                                    {
                                        tcMenuList.Style["display"] = "none";
                                    }

                                    //组合二级菜单
                                    tr11.Cells.Add(td11img);
                                    tr11.Cells.Add(td11);
                                    tb1.Rows.Add(tr11);
                                    tb1.Rows.Add(tr12);
                                    tb1.Rows.Add(Line());

                                    tcMenuList.Controls.Add(tb1);
                                }
                            }
                            if (tb1.Rows.Count > 0)
                            {
                                tb1.Rows.Insert(0, Line());
                            }
                        }
                        //组合一级菜单
                        tr1.Cells.Add(tcMenuTitle1);
                        tr1.Cells.Add(tcMenuTitle2);
                        tr2.Cells.Add(tcMenuList);
                        tb.Rows.Add(tr1);
                        tb.Rows.Add(tr2);
                    }
                }
                this.Controls.Add(tb);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 单元格空格
        /// </summary>
        /// <returns></returns>
        private HtmlTableRow Line()
        {
            HtmlTableRow trLine = new HtmlTableRow();
            HtmlTableCell tcLine = new HtmlTableCell();
            tcLine.Height = "5px";
            tcLine.ColSpan = 2;
            trLine.Cells.Add(tcLine);
            return trLine;
        }
        
    }
}
