using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nandasoft;
using BLL.Entity;
using System.Data;
using BusinessLayer.Survey;
using WebUI;
using System.Text;

namespace WebManage.Survey
{
    public partial class HotlineData : BasePage
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #endregion

        #region toolbar事件
        /// <summary>
        /// toolbar事件
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    break;
            }
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region 数据行操作
        /// <summary>
        /// 数据行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = NDConvert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SurveyName":
                    break;
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindGridView();
        }
        #endregion

        #region 邦定GridView
        /// <summary>
        /// 邦定GridView
        /// </summary>
        private void BindGridView()
        {
            long UserID = NDConvert.ToInt64(Session["UserID"]); //当前管理操作的中

            DataSet ds = GetData();
            DataTable dt = ds.Tables[1];

            grid.DataSource = dt;
            grid.DataBind();

            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                string name = grid.Rows[i].Cells[1].Text;

                if (name.Length > 8)
                {
                    grid.Rows[i].Cells[1].Text = Truncate(name, 13, "...");
                    grid.Rows[i].Cells[1].ToolTip = name;
                }

                string ageRange = grid.Rows[i].Cells[3].Text;
                if (ageRange.Length > 8)
                {
                    grid.Rows[i].Cells[3].Text = Truncate(ageRange, 13, "...");
                    grid.Rows[i].Cells[3].ToolTip = ageRange;
                }

                string consultContent=grid.Rows[i].Cells[9].Text;
                if(consultContent.Length>9)
                {
                    grid.Rows[i].Cells[9].Text = Truncate(consultContent, 15, "...");
                    grid.Rows[i].Cells[9].ToolTip = consultContent;
                }

                string opinionContent = grid.Rows[i].Cells[10].Text;
                if (opinionContent.Length > 9)
                {
                    grid.Rows[i].Cells[10].Text = Truncate(opinionContent, 15, "...");
                    grid.Rows[i].Cells[10].ToolTip = opinionContent;
                }
            }

        }

        #endregion

        #region 类型判断
        public String ConvertString(object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;
            return obj.ToString().Trim();
        }
        #endregion

        #region 邦定分页控件
        /// <summary>
        /// 邦定分页控件
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            //viewpage1.CurrentPageIndex = 1;
            viewpage1.RecordCount = RecordCount;
        }
        #endregion

        #region 获得调查答卷信息
        /// <summary>
        /// 获得调查问卷信息
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new HotlineData_Layer().GetHotline_Analyse(viewpage1.CurrentPageIndex, viewpage1.PageSize);
            return ds;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            return true;
        }
        #endregion

        #region 截取字符串方法（中英区分）
        /// <summary>   
        /// 截取字符串长度   
        /// </summary>   
        /// <param name="input">要截取的字符串对象</param>   
        /// <param name="length">要保留的字符个数</param>   
        /// <param name="suffix">后缀(用以替换超出长度部分)</param>   
        /// <returns></returns>   
        public static string MySubString(string input, int length, string suffix)
        {
            Encoding encode = Encoding.GetEncoding("gb2312");
            byte[] byteArr = encode.GetBytes(input);
            if (byteArr.Length <= length) return input;

            int m = 0, n = 0;
            foreach (byte b in byteArr)
            {
                if (n >= length) break;
                if (b > 127) m++; //重要一步：对前p个字节中的值大于127的字符进行统计   
                n++;
            }
            if (m % 2 != 0) n = length + 1; //如果非偶：则说明末尾为双字节字符，截取位数加1   

            return encode.GetString(byteArr, 0, n) + suffix;
        }

        /// <summary>   
        /// 截取字符串长度(高效)   
        /// </summary>   
        /// <param name="original">要截取的字符串对象</param>   
        /// <param name="length">要保留的字符个数</param>   
        /// <param name="suffix">后缀(用以替换超出长度部分)</param>   
        /// <returns></returns>   
        public static string Truncate(string original, int length, string suffix)
        {
            int len = original.Length;
            int i = 0;
            for (; i < length && i < len; ++i)
            {
                if ((int)(original[i]) > 0xFF)
                    --length;
            }
            if (length < i)
                length = i;
            else if (length > len)
                length = len;
            return original.Substring(0, length) + suffix;
        }
        #endregion

    }
}
