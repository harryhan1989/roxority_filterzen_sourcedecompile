using System;
using BLL.Common;

namespace WebManage.Web.Gifts
{
    public partial class GiftExchangeList : System.Web.UI.Page
    {
        private RepeaterSelect _repeaterSelect;
        ///<summary>sql
        ///</summary>
        public string PSql
        {
            set { ViewState["sql"] = value; }
            get
            {
                if (ViewState["sql"] == null)
                    return string.Empty;
                return ViewState["sql"].ToString();
            }
        }
        /// <summary>
        /// PSimpleSearch
        /// </summary>
        public string PSearchSql
        {
            set { ViewState["PSearchSql"] = value; }
            get
            {
                if (ViewState["PSearchSql"] == null)
                    return string.Empty;
                return ViewState["PSearchSql"].ToString();
            }
        }
        ///<summary>sql
        ///</summary>
        public string Xorder
        {
            set { ViewState["Xorder"] = value; }
            get
            {
                if (ViewState["Xorder"] == null)
                    return string.Empty;
                return ViewState["Xorder"].ToString();
            }
        }
        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20100927
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(this);
            _repeaterSelect = new RepeaterSelect(Page, rptSurveyMain, TableSourceCount, GridFirstPage, GridBefore, GridNext, GridEnd, GridJump, DdlgridSourceCount);

            BLL.Query.ExchangeQuery query = new BLL.Query.ExchangeQuery();
            string sql = query.GetExchangeListSql();
            Xorder = "exchangetime desc";
            _repeaterSelect.Xorder = "exchangetime desc";
            _repeaterSelect.KeyCode = "ID";
            _repeaterSelect.LoadData(sql);

            SetParas();
        }

        private void SetParas()
        {
            if (!IsPostBack)
            {
                PSql = _repeaterSelect.PSql;
                Xorder = _repeaterSelect.Xorder;
                _repeaterSelect.KeyCode = "ID";
                PSearchSql = _repeaterSelect.PSearchSql;

            }
            else
            {
                _repeaterSelect.PSql = PSql;
                _repeaterSelect.Xorder = Xorder;
                _repeaterSelect.KeyCode = "ID";
                _repeaterSelect.PSearchSql = PSearchSql;

            }
        }

        #region 自定义分页
        ///<summary>
        /// 首页
        ///</summary>
        [Anthem.Method]
        public void FirstPage_Ctl()
        {
            _repeaterSelect.FirstPageCtl();

        }
        /// <summary>
        ///  上一页
        /// </summary>
        [Anthem.Method]
        public void BeforePage_Ctl()
        {
            _repeaterSelect.BeforePageCtl();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        [Anthem.Method]
        public void NextPage_Ctl()
        {
            _repeaterSelect.NextPageCtl();
        }
        /// <summary>
        /// 末页
        /// </summary>
        [Anthem.Method]
        public void EndPage_Ctl()
        {
            _repeaterSelect.EndPageCtl();
        }
        /// <summary>
        /// 跳转页
        /// </summary>
        [Anthem.Method]
        public void JumpPage_Ctl(string gridPageCout)
        {
            _repeaterSelect.JumpPageCtl(gridPageCout);
        }
        /// <summary>
        /// 当前页记录数
        /// </summary>
        /// <param name="gridPageSize"></param>
        [Anthem.Method]
        public void PageSizePage_Ctl(string gridPageSize)
        {
            _repeaterSelect.PageSizePageCtl(gridPageSize);
            SetParas();
        }
        #endregion

        protected void rptSurveyMain_PreRender(object sender, EventArgs e)
        {
            _repeaterSelect.repeaterFolderPreRender(e);
        }
    }
}
