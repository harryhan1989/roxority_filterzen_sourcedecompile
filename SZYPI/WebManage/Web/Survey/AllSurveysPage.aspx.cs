using System;
using System.Data;
using BLL.Common;
using System.Web.UI.WebControls;

namespace WebManage.Web.Survey
{
    public partial class AllSurveysPage : System.Web.UI.Page
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
        /// 查询关键字
        /// </summary>
        public string KeyCode
        {
            set { ViewState["KeyCode"] = value; }
            get
            {
                if (ViewState["KeyCode"] == null)
                    return string.Empty;
                return ViewState["KeyCode"].ToString();
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
            //if (!IsPostBack)
            //{

            

            //获取类型
            if (Request.QueryString["SurveyType"] != null)
            {
                SurveyType = Request.QueryString["SurveyType"].ToString();
            }

            //根据类型改变背景图片
            switch (SurveyType)
            {
                case "-1":
                case "0":
                    divTop.Style.Add("background", "url(../../css/image/Index/wjzs.jpg) no-repeat");
                    break;
                case "1":
                    divTop.Style.Add("background", "url(../../css/image/Index/cpzs.jpg) no-repeat");
                    break;
                case "2":
                    divTop.Style.Add("background", "url(../../css/image/Index/tpzs.jpg) no-repeat");
                    break;
                default:
                    divTop.Style.Add("background", "url(../../css/image/Index/wjzs.jpg) no-repeat");
                    break;
            }

            Anthem.Manager.Register(this);
            _repeaterSelect = new RepeaterSelect(Page, rptSurveyMain, TableSourceCount, GridFirstPage, GridBefore, GridNext, GridEnd, GridJump, DdlgridSourceCount);
            //if (Request.QueryString["sql"] != null)
            //{
            BLL.Query.SurveyTableQuery query = new BLL.Query.SurveyTableQuery();
            string sql = query.GetSurveySql(SurveyType);
            Xorder = "createdate desc";
            _repeaterSelect.Xorder = "createdate desc";
            _repeaterSelect.Type = "2";
            _repeaterSelect.LoadData(sql);
            if (!IsPostBack)
            {
                if (Request.QueryString["KeyCode"] != null)
                {
                    ((TextBox)Page.FindControl("utlTop1$txtSearchCondition")).Text = Request.QueryString["KeyCode"].ToString();
                    _repeaterSelect.SimpleSearch(Request.QueryString["KeyCode"].ToString());
                }
            }
            SetParas();
        }

        private void SetParas()
        {
            if (!IsPostBack)
            {
                PSql = _repeaterSelect.PSql;
                Xorder = _repeaterSelect.Xorder;
                KeyCode =  _repeaterSelect.KeyCode;
                PSearchSql = _repeaterSelect.PSearchSql;

                
            }
            else
            {
                _repeaterSelect.PSql = PSql;
                _repeaterSelect.Xorder = Xorder;
                _repeaterSelect.PSearchSql = PSearchSql;
                _repeaterSelect.KeyCode = KeyCode;
            }
        }

        /// <summary>
        /// 问卷方式
        /// 作者：姚东
        /// 时间：20101009    
        /// </summary>
        private string SurveyType
        {
            set { ViewState["SurveyType"] = value.ToString(); }
            get
            {
                if (ViewState["SurveyType"] != null)
                {
                    return ViewState["SurveyType"].ToString();
                }
                else
                {
                    return "-1";
                }
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

        ///<summary>
        /// 查询按钮点击事件
        ///</summary>
        [Anthem.Method]
        public void SearchClick(string keyCode)
        {
            _repeaterSelect.SimpleSearch(keyCode);

        }
    }
}
