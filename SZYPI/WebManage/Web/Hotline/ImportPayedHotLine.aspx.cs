using System;
namespace Business.Import
{
    public partial class ImportPayedHotLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string msgList = string.Empty;
            ImportBusiness1 logic = new ImportBusiness1();
            bool success;

            success = logic.ImportXls2Db(fuFileUpload, "UT_QS_Hotline_Analyse", ref msgList, ref msg);
            hidMsg.Value = msgList;
            //hidMsg.UpdateAfterCallBack = true;
            if (!success)
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    //Anthem.Manager.AddScriptForClientSideEval(string.Format("setTimeout(function () {{{0}}},100);</script>", msg));
                    ClientScript.RegisterStartupScript(Page.GetType(), "errMessage",
                                                       string.Format(
                                                           "<script type=\"text/javascript\"> setTimeout(function () {{{0}}},100);</script>",
                                                           msg));
                }
                else
                {
                    //Anthem.Manager.AddScriptForClientSideEval("OpenImportErrorPage();");
                    ClientScript.RegisterStartupScript(Page.GetType(), "errMessage",
                                                       "<script type=\"text/javascript\"> OpenImportErrorPage();</script>");
                }
            }
            else
            {
                //导入成功
                //Anthem.Manager.AddScriptForClientSideEval("window.opener.window.DialogboxShow('数据导入成功！',true,true);window.close();");
                ClientScript.RegisterStartupScript(Page.GetType(), "RefreshParentData",
                                                          "<script type=\"text/javascript\"> alert('数据导入成功！');</script>");
            }
            //btnOK.UpdateAfterCallBack = true;
        }

    }
}