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
    public partial class MenuDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUrlParameter();
                InitPage();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }
            try
            {
                switch (this.CurOperation)
                { 
                    case (int)OperationEnum.INSERT:
                        Save();
                        PageHelper.ShowMessage("�����ɹ���");
                        break;
                    case (int)OperationEnum.UPDATE:
                        Update();
                        PageHelper.ShowMessage("���³ɹ���");
                        break;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('MenuDetailLY','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('MenuDetailLY','btnRefresh');", true);
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            switch (this.CurOperation)
            { 
                case (int)OperationEnum.INSERT:
                    if (this.MenuID == 0)
                    {
                        txtNavigateURL.Enabled = false;
                        txtImageURL.Enabled = false;
                    }
                    break;
                case (int)OperationEnum.UPDATE:
                    LoadData();
                    break;
            }
        }

        /// <summary>
        /// ���URL����
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] != "")
            {
                this.CurOperation = NDConvert.ToInt32(Request.QueryString["Operation"].ToString());
            }

            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.MenuID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void LoadData()
        {
            MenuEntity entity = new MenuEntity(this.MenuID);
            txtMenuName.Text = entity.MenuName;
            for (int i = 0; i < rdoIsDisplay.Items.Count; i++)
            {
                if (entity.IsDisplay && rdoIsDisplay.Items[i].Value == "1")
                {
                    rdoIsDisplay.Items[i].Selected = true;
                }
                else if (!entity.IsDisplay && rdoIsDisplay.Items[i].Value == "0")
                {
                    rdoIsDisplay.Items[i].Selected = true;
                }
            }
            CommonHelp.BinddrpCurValue(drpType,entity.Type.ToString());
            txtNavigateURL.Text = entity.NavigateURL;
            txtImageURL.Text = entity.ImageURL;
            if (entity.ParentMenuID == 0)
            {
                txtNavigateURL.Enabled = false;
                txtImageURL.Enabled = false;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            MenuEntity entity = new MenuEntity();
            entity.ImageURL = txtImageURL.Text;
            if (rdoIsDisplay.SelectedValue == "0")
            {
                entity.IsDisplay = false;
            }
            else
            {
                entity.IsDisplay = true; 
            }
            entity.IsLink = false;
            entity.MenuName = txtMenuName.Text.Trim();
            entity.NavigateURL = txtNavigateURL.Text;
            entity.ParentMenuID = this.MenuID;
            entity.Type = NDConvert.ToInt32(drpType.SelectedValue);
            entity.IsDeleted = false;
            entity.SortOrder = MenuQuery.MaxSortIndex(this.MenuID);
            new MenuRule().Add(entity);
        }

        /// <summary>
        /// �޸�
        /// </summary>
        private void Update()
        {
            MenuEntity entity = new MenuEntity(this.MenuID);
            entity.ImageURL = txtImageURL.Text;
            if (rdoIsDisplay.SelectedValue == "0")
            {
                entity.IsDisplay = false;
            }
            else
            {
                entity.IsDisplay = true;
            }
            entity.Type = NDConvert.ToInt32(drpType.SelectedValue);
            entity.MenuName = txtMenuName.Text.Trim();
            entity.NavigateURL = txtNavigateURL.Text;
            new MenuRule().Update(entity);
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (!string.IsNullOrEmpty(txtMenuName.Text.Trim()))
            {
                if (CommonHelp.CheckString(txtMenuName.Text.Trim()))
                {
                    hidInfo.Value = "�˵������в��ܰ��������ַ���";
                    txtMenuName.Focus();
                    return false;
                }
            }
            else
            {
                hidInfo.Value = "�˵����Ʋ���Ϊ�գ�";
                txtMenuName.Focus();
                return false;
            }
            return true;
        }

        #region ����
        /// <summary>
        /// ҳ��״̬����
        /// </summary>
        private int CurOperation
        {
            set { ViewState["Operation"] = value.ToString(); }
            get
            {
                if (ViewState["Operation"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Operation"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// �˵�ID
        /// </summary>
        private long MenuID
        {
            set { ViewState["MenuID"] = value.ToString(); }
            get
            {
                if (ViewState["MenuID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["MenuID"].ToString());
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
