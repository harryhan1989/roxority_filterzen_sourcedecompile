using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nandasoft;

namespace BLL.Common
{
    public class RepeaterSelect
    {
        private readonly Page _objPage;
        public bool PIsRowNull;
        public String PSql;
        public String PSimpleSearch;
        public String KeyCode="SID";
        public string Type = "1";       //1:兑换记录  2:问卷内容
        //public String KeyCName;
        public String Xorder;
        //private readonly Anthem.ImageButton _btnSimpleSearch;

        private readonly Anthem.Repeater _repeaterFolder;

        private readonly Anthem.Label _tableSourceCount;

        private readonly Anthem.ImageButton _gridFirstPage;

        private readonly Anthem.ImageButton _gridBefore;

        private readonly Anthem.ImageButton _gridNext;

        private readonly Anthem.ImageButton _gridEnd;

        public readonly Anthem.DropDownList _gridJump;

        private readonly Anthem.DropDownList _ddlgridSourceCount;

        private const string CONST_PAGE_SIZE = "10";

        public Dictionary<int, String> WidthList;
        public String PSearchSql;

        public String SelectValues;
        public RepeaterSelect(Page objPage,Anthem.Repeater reperterFolder, Anthem.Label tableSourceCount, Anthem.ImageButton gridFirstPage, Anthem.ImageButton gridBefore, Anthem.ImageButton gridNext, Anthem.ImageButton gridEnd, Anthem.DropDownList gridJump, Anthem.DropDownList dDlgridSourceCount)
        {
            _objPage = objPage;
            //_btnSimpleSearch = btnSimpleSearch;
            _repeaterFolder = reperterFolder;
            _tableSourceCount = tableSourceCount;
            _gridFirstPage = gridFirstPage;
            _gridBefore = gridBefore;
            _gridNext = gridNext;
            _gridEnd = gridEnd;
            _gridJump = gridJump;
            _ddlgridSourceCount = dDlgridSourceCount;
        }

        public void LoadData(String sql)
        {
            if (!_objPage.IsPostBack)
            {
                if (!sql.Equals(string.Empty))
                {
                    PIsRowNull = false;
                    
                    AddEvent();
                    GetRepeaterPara(sql);
                }
            }
        }

        /// <summary>
        /// 分页按钮事件添加
        /// </summary>
        public void AddEvent()
        {
            _gridFirstPage.Attributes.Add("onclick", "FirstPage()");
            _gridBefore.Attributes.Add("onclick", "BeforePage()");
            _gridNext.Attributes.Add("onclick", "NextPage()");
            _gridEnd.Attributes.Add("onclick", "EndPage()");
            _gridJump.Attributes.Add("onchange", String.Format("JumpPage('{0}')", _gridJump.UniqueID));
            _ddlgridSourceCount.Attributes.Add("onchange",
                                              String.Format("PageSizePage('{0}')", _ddlgridSourceCount.UniqueID));
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sql"></param>
        public void GetRepeaterPara(String sql)
        {
            String xorder = String.Empty;
            PSql = sql;
            if (PSql.ToLower().LastIndexOf("order by") >= 0)
            {
                xorder = PSql.Substring(PSql.ToLower().LastIndexOf("order by") + 8, PSql.Length - (PSql.ToLower().LastIndexOf("order by") + 8));
            }

            if (!Xorder.Equals(string.Empty))
                xorder = Xorder;

            PSearchSql = PSql;
            GetSource(PSql, xorder, 1, String.Empty);
        }

        /// <summary>
        /// GridView预呈现
        /// </summary>
        /// <param name="e"></param>
        public void repeaterFolderPreRender(EventArgs e)
        {
            if (PIsRowNull) //隐藏空数据列
            {
                _repeaterFolder.Items[0].Visible = false;
                _repeaterFolder.UpdateAfterCallBack = true;
            }
        }

        /// <summary>
        /// 简单查询，根据标题、内容
        /// </summary>
        /// <param name="valus"></param>
        public void SimpleSearch(String valus)
        {
            String searchSql = String.Empty;
            String order = String.Empty;
            //if (!String.IsNullOrEmpty(PSimpleSearch.Trim()))
            //{
                if (!String.IsNullOrEmpty(PSql) && valus.Trim() != String.Empty)
                {
                    String str = string.Format("SurveyName like '%{0}%' or PageContent like '%{0}%'", valus);
                    String sql = String.Format("select  *   from ({0}) a ", PSql);
                    if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(sql))
                    {
                        sql += String.Format(" where ({0})", str);
                    }
                    searchSql = sql;
                    order = KeyCode.Trim();
                }
                else
                {
                    searchSql = PSql;
                    order = KeyCode.Trim();
                }
            //}
            PSearchSql = searchSql;
            GetSource(searchSql, order.Trim(), 1, String.Empty);
            //_btnSimpleSearch.UpdateAfterCallBack = true;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="currentPage"></param>
        /// <param name="pagesize"></param>
        public void GetSource(String sql, String order, int currentPage, String pagesize)
        {
            sql = String.Format("select  *   from ({0}) a ", sql);

            int maxCount = 0;
            if (String.IsNullOrEmpty(KeyCode))
            {
                DataTable dataTable = Nandasoft.NDDBAccess.Fill(sql);
                if (dataTable != null)
                {
                    maxCount = dataTable.Rows.Count;
                    if (dataTable.Columns.Count >= 2)
                    {
                        KeyCode = dataTable.Columns[0].ColumnName;
                        //KeyCName = dataTable.Columns[1].ColumnName;
                    }
                    else if (dataTable.Columns.Count == 1)
                    {
                        KeyCode = dataTable.Columns[0].ColumnName;
                        //KeyCName = dataTable.Columns[0].ColumnName;
                    }
                }
            }

            else
            {
                String sqlCount = String.Format("select count(*)  from ({0})   Table_source_Grid", sql);//计算记录数
                maxCount = NDConvert.ToInt32(NDDBAccess.Fill(sqlCount).Rows[0][0]);
            }
            String rowNumber = !String.IsNullOrEmpty(order) ? String.Format(" ROW_NUMBER() over(order by {0}) as XIDS, ", order) : String.Format(" ROW_NUMBER() over(order by {0})  as XIDS, ", KeyCode);

            if (String.IsNullOrEmpty(pagesize))
            {
                pagesize = _ddlgridSourceCount.SelectedValue.Trim();
            }
            if (!String.IsNullOrEmpty(order))
            {
                if (sql.ToLower().Contains("order") &&
                    (sql.ToLower().LastIndexOf("where") < sql.ToLower().LastIndexOf("order") ||
                     sql.ToLower().LastIndexOf("from") > sql.ToLower().LastIndexOf("order")))
                {
                    sql += String.Format(" , {0}", order);
                }
                else if (!sql.ToLower().Contains("order"))
                {
                    sql += String.Format(" order by {0}", order);
                }
            }
            _tableSourceCount.Text = String.Format("{0}", maxCount);
            _tableSourceCount.UpdateAfterCallBack = true;
            int pageCout = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(maxCount) / Convert.ToDouble(pagesize)));//总页数
            if (pageCout < currentPage)
            {
                currentPage = pageCout;//总页数
            }
            if (_gridJump.Items != null)//加载页码
            {
                _gridJump.Items.Clear();
                for (int i = 1; i <= pageCout; i++)
                {
                    var item = new ListItem(String.Format("{0}/{1}", i, pageCout), i.ToString());
                    _gridJump.Items.Add(item);
                }
                _gridJump.UpdateAfterCallBack = true;
                if (_gridJump.SelectedItem != null)
                    _gridJump.SelectedItem.Selected = false;
                if (_gridJump.Items.FindByValue(currentPage.ToString()) != null)
                    _gridJump.Items.FindByValue(currentPage.ToString()).Selected = true;

            }
            sql = sql.Insert(sql.ToLower().IndexOf("select") + 6, rowNumber);
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage >= pageCout)
            {
                currentPage = pageCout;
            }
            sql = sql.Insert(sql.ToLower().IndexOf("select") + 6, String.Format("  top {0}  ", maxCount));
            String execSql;
            if (!String.IsNullOrEmpty(order))
            {
                execSql = String.Format(
                   "select  Table_source_Grid.*  from ({0})   Table_source_Grid  where XIDS between {1} and  {2} ",
                    sql, (currentPage - 1) * int.Parse(pagesize) + 1, currentPage * int.Parse(pagesize));//分页语句

            }
            else
            {
                execSql = String.Format(
                 "select  Table_source_Grid.*  from ({0})   Table_source_Grid  where XIDS between {1} and  {2} order by XIDS",
                  sql, (currentPage - 1) * int.Parse(pagesize) + 1, currentPage * int.Parse(pagesize));//分页语句
            }



            DataTable dt = NDDBAccess.Fill(execSql);
            //空数据源处理
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add(dt.NewRow());
                PIsRowNull = true;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Trim() == "XIDS")
                {
                    dt.Columns.RemoveAt(i);
                }
            }

            //对html解析
            if (dt.Rows.Count > 0 && Type=="2")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        string str = CommonHelp.StripHTML(dr["PageContent"].ToString());

                        if (str.Length > 200)
                        {
                            dr["PageContent"] = str.Substring(0, 200) + "...";
                        }
                        else
                        {
                            dr["PageContent"] = str;
                        }
                    }
                    catch
                    { }
                    finally
                    { }
                }
            }

            if (Type == "2")
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "HasAnswered";
                dt.Columns.Add(dc);

                if (HttpContext.Current.Session["UserGUID"] != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (new BLL.Query.IndexQuery().CheckHasAnswered(dr["SID"].ToString(), HttpContext.Current.Session["UserIDClient"].ToString()))
                        {
                            dr["HasAnswered"] = "已参加";
                        }
                        else
                        {
                            dr["HasAnswered"] = "未参加";
                        }
                    }
                }  
            }

            _repeaterFolder.DataSource = dt;
            if (!_objPage.IsPostBack)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    PSimpleSearch += column.ColumnName + " like '%<#Search#>%' or ";
                }
            }
            _repeaterFolder.DataBind();
            _repeaterFolder.UpdateAfterCallBack = true;

            if (!String.IsNullOrEmpty(PSimpleSearch))
                PSimpleSearch = PSimpleSearch.Trim().Substring(0, PSimpleSearch.LastIndexOf("or"));
            SetBtnEnaled(currentPage, pageCout);
        }

        public void GetSource(String sql, int currentPage, String pagesize)
        {
            sql = String.Format("select  *   from ({0}) a ", sql);
            String rowNumber = !String.IsNullOrEmpty(Xorder) ? String.Format(" ROW_NUMBER() over(order by {0}) as XIDS, ", Xorder) : String.Format(" ROW_NUMBER() over(order by {0})  as XIDS, ", KeyCode);
            if (String.IsNullOrEmpty(pagesize))
            {
                pagesize = !String.IsNullOrEmpty(_ddlgridSourceCount.SelectedValue.Trim()) ? _ddlgridSourceCount.SelectedValue.Trim() : CONST_PAGE_SIZE;
            }
            sql = sql.Insert(sql.ToLower().IndexOf("select") + 6, rowNumber);
            int maxCurrentPage = _gridJump.Items.Count;//总页数
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage >= maxCurrentPage)
            {
                currentPage = maxCurrentPage;
            }
            String execSql =
               String.Format(
                   "select  Table_source_Grid.*  from ({0})   Table_source_Grid  where XIDS between {1} and  {2} order by XIDS",
                    sql, (currentPage - 1) * int.Parse(pagesize) + 1, currentPage * int.Parse(pagesize));//分页语句
            DataTable dt = NDDBAccess.Fill(execSql);
            //空数据源处理
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add(dt.NewRow());
                PIsRowNull = true;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Trim() == "XIDS")
                {
                    dt.Columns.RemoveAt(i);
                }
            }
            _repeaterFolder.DataSource = dt;

            _repeaterFolder.DataBind();
            _repeaterFolder.UpdateAfterCallBack = true;
            SetBtnEnaled(currentPage, maxCurrentPage);
        }

        /// <summary>
        /// 设置按钮的可用性
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="maxCurrentPage"></param>
        private void SetBtnEnaled(int currentPage, int maxCurrentPage)
        {
            if (currentPage == 1)
            {
                _gridFirstPage.Enabled = false;
                _gridBefore.Enabled = false;
                _gridBefore.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/up10.jpg");
                _gridFirstPage.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/home10.jpg");
                _gridBefore.UpdateAfterCallBack = true;
                _gridFirstPage.UpdateAfterCallBack = true;
            }
            else
            {
                _gridFirstPage.Enabled = true;
                _gridBefore.Enabled = true;
                _gridBefore.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/up.jpg");
                _gridFirstPage.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/home.jpg");
                _gridFirstPage.UpdateAfterCallBack = true;
                _gridBefore.UpdateAfterCallBack = true;
            }
            if (currentPage == maxCurrentPage)
            {
                _gridNext.Enabled = false;
                _gridEnd.Enabled = false;
                _gridNext.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/next10.jpg");
                _gridEnd.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/end10.jpg");
                _gridEnd.UpdateAfterCallBack = true;
                _gridNext.UpdateAfterCallBack = true;
            }
            else
            {
                _gridNext.Enabled = true;
                _gridEnd.Enabled = true;
                _gridNext.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/next.jpg");
                _gridEnd.ImageUrl = _objPage.ResolveUrl("../../CSS/image/Index/end.jpg");
                _gridEnd.UpdateAfterCallBack = true;
                _gridNext.UpdateAfterCallBack = true;
            }
            if (_gridJump.Items.FindByValue(currentPage.ToString()) != null)
            {
                _gridJump.Items.FindByValue(currentPage.ToString()).Selected = true;
                _gridJump.SelectedValue = currentPage.ToString();
                _gridJump.UpdateAfterCallBack = true;
            }
            if (maxCurrentPage == 0)
            {
                _gridNext.Enabled = false;
                _gridEnd.Enabled = false;
                _gridEnd.UpdateAfterCallBack = true;
                _gridNext.UpdateAfterCallBack = true;
                _gridFirstPage.Enabled = false;
                _gridBefore.Enabled = false;
                _gridBefore.UpdateAfterCallBack = true;
                _gridFirstPage.UpdateAfterCallBack = true;
            }
        }


        ///<summary>
        /// 首页
        ///</summary>
        public void FirstPageCtl()
        {
            GetSource(PSearchSql, 1, _ddlgridSourceCount.SelectedValue.Trim());
        }

        /// <summary>
        ///  上一页
        /// </summary>
        public void BeforePageCtl()
        {
            int currentPage = 0;//当前页码
            if (_gridJump.SelectedItem != null)
                currentPage = Convert.ToInt32(_gridJump.SelectedItem.Value);//当前页码
            int maxCurrentPage = _gridJump.Items.Count;//总页数
            if (currentPage - 1 < maxCurrentPage && currentPage - 1 > 0)
                currentPage--;
            else if (currentPage - 1 == 0)
                currentPage = 1;

            GetSource(PSearchSql, currentPage, _ddlgridSourceCount.SelectedValue.Trim());
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPageCtl()
        {
            int currentPage = 0;//当前页码
            if (_gridJump.SelectedItem != null)
                currentPage = Convert.ToInt32(_gridJump.SelectedItem.Value);//当前页码
            int maxCurrentPage = _gridJump.Items.Count;//总页数
            if (currentPage + 1 < maxCurrentPage)
                currentPage++;
            else if (currentPage + 1 == maxCurrentPage)
                currentPage = maxCurrentPage;
            else if (currentPage + 1 > maxCurrentPage)
                currentPage = maxCurrentPage;
            GetSource(PSearchSql, currentPage, _ddlgridSourceCount.SelectedValue.Trim());
        }

        /// <summary>
        /// 末页
        /// </summary>
        public void EndPageCtl()
        {
            int maxCurrentPage = _gridJump.Items.Count;//总页数
            GetSource(PSearchSql, maxCurrentPage, _ddlgridSourceCount.SelectedValue.Trim());
        }

        /// <summary>
        /// 跳转页
        /// </summary>
        public void JumpPageCtl(String gridPageCout)
        {
            if (!String.IsNullOrEmpty(gridPageCout))
            {
                int currentPage = int.Parse(gridPageCout);
                GetSource(PSearchSql, currentPage, _ddlgridSourceCount.SelectedValue.Trim());
            }
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        /// <param name="gridPageSize"></param>
        public void PageSizePageCtl(String gridPageSize)
        {
            if (!String.IsNullOrEmpty(gridPageSize))
            {
                int maxCount =
                    Convert.ToInt32(
                        _tableSourceCount.Text.Replace("总共", String.Empty).Replace("条", String.Empty).Trim());
                int pageCout =
                       Convert.ToInt32(Math.Ceiling(Convert.ToDouble(maxCount) / Convert.ToDouble(gridPageSize)));
                int currentPage = 0;//当前页码
                if (_gridJump.SelectedItem != null)
                    currentPage = Convert.ToInt32(_gridJump.SelectedItem.Value);//当前页码
                if (currentPage > pageCout)
                {
                    currentPage = pageCout;
                }
                //TODO:_repeaterFolder.PageSize = Convert.ToInt32(gridPageSize);
                if (_gridJump.Items != null)//加载页码
                {
                    _gridJump.Items.Clear();
                    for (int i = 1; i <= pageCout; i++)
                    {
                        var item = new ListItem(String.Format("{0}/{1}", i, pageCout), i.ToString());
                        _gridJump.Items.Add(item);
                    }
                }
                GetSource(PSearchSql, currentPage, gridPageSize);
            }
        }
    }
}