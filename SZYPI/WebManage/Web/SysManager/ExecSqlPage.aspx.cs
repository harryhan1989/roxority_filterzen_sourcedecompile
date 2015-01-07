using System;
using System.Data;
using WebUI;

namespace WebManage.Web.SysManager
{
    public partial class ExecSqlPage : BasePage//System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = txtSqlArea.Text;

            DataTable dt = DBAccess.DbHelperSQL.Fill(sql);

            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void btnExec_Click(object sender, EventArgs e)
        {
            string sql = txtSqlArea.Text;

            int execResult = DBAccess.DbHelperSQL.ExecuteSql(sql);

            if (execResult > 0)
            {
                PageHelper.ShowExceptionMessage("执行成功，成功执行了" + execResult.ToString() + "行数据！");
            }
            else
            {
                PageHelper.ShowExceptionMessage("执行成功，成功执行了" + execResult.ToString() + "行数据！");
            }
        }
    }
}