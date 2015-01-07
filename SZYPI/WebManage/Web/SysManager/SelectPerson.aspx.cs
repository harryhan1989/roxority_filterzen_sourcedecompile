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
using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;

namespace WebManage.Web.SystemManager
{
    public partial class SelectPerson : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }

            this.OUID = NDConvert.ToInt64(drpOU.SelectedValue);

            BindGridView();
        }

        /// <summary>
        /// ��ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        /// <summary>
        /// ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int RowIndex = NDConvert.ToInt32(e.CommandArgument);
                    Session["SelectUserName"] = grid.DataKeys[RowIndex].Values["UserName"].ToString();
                    Session["SelectUserID"] = grid.DataKeys[RowIndex].Values["UserID"].ToString();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('SelectPersonLY','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            BindOU();
            BindGridView();
        }

        /// <summary>
        /// �󶨲���
        /// </summary>
        private void BindOU()
        {
            DataTable dt = new OUQuery().GetOU();
            NDHelperWebControl.LoadDropDownList(drpOU, dt, "OUName", "OUID", new ListItem("��ѡ��", "0"));
        }

        /// <summary>
        /// ��
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = GetData();
            DataTable dt = ds.Tables[1];
            grid.DataSource = dt;
            grid.DataBind();
            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new UserQuery().GetUsers(this.OUID, txtUserName.Text, viewpage1.CurrentPageIndex, viewpage1.PageSize);
            return ds;
        }

        /// <summary>
        /// ���ҳ�ؼ�
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            viewpage1.RecordCount = RecordCount;
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                if (CommonHelp.CheckString(txtUserName.Text.Trim()))
                {
                    hidInfo.Value = "�����������������ַ���";
                    txtUserName.Focus();
                    return false;
                }
            }
            return true;
        }

        #region ����
        /// <summary>
        /// ����ID
        /// </summary>
        private long OUID
        {
            set { ViewState["OUID"] = value; }
            get
            {
                if (ViewState["OUID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["OUID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

      

    }
}
