using System;
using System.Text;
using System.Web;
namespace Business.Import
{
    public partial class ImportErrorPage :  System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void export_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hisMsg.Value.Trim()))
            {
                string strHtml = new Business.Import.ImportErrorPageBusiness().ExportToXLS(hisMsg.Value.Trim());
                string fileNme = string.Format("错误报告{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileNme, Encoding.UTF8));
                Response.Charset = "utf-8";
                Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                Response.ContentType = "application/ms-excel";
                Response.Write(strHtml);
                Response.End();
            }

        }

    }

}
