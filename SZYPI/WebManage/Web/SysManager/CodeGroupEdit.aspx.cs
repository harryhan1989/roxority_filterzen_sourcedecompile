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
using WebUI;
using Nandasoft;
using BLL;
using Nandasoft.Helper;

namespace WebManage.Web.SysManager
{
    public partial class CodeGroupEdit : BasePage
    {
        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
        protected void btnSave_Click(object sender, EventArgs e)
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
                    case (int)OperationEnum.UPDATE:
                        Update();
                        PageHelper.ShowMessage("���³ɹ���");
                        break;
                    case (int)OperationEnum.INSERT:
                        Save();
                        PageHelper.ShowMessage("����ɹ���");
                        break;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('CodeGroupEditLY','btnRefresh')", true);
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('CodeGroupEditLY','btnRefresh')", true);
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
                this.CodeGroupID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());
            }
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                LoadData();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void LoadData()
        {
            CodeGroupEntity entity = new CodeGroupEntity(this.CodeGroupID);
            txtCodeGroupName.Text = entity.CodeGroupName;
            txtCodeGroupKey.Text = entity.CodeGroupKey;
            txtMemo.Text = entity.Memo;               
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            CodeGroupEntity entity = new CodeGroupEntity();
            entity.CodeGroupName = txtCodeGroupName.Text;
            entity.CodeGroupKey = txtCodeGroupKey.Text;
            entity.Memo = txtMemo.Text;    
            entity.IsDeleted = false;
            new CodeGroupRule().Add(entity);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Update()
        {
            CodeGroupEntity entity = new CodeGroupEntity(this.CodeGroupID);
            entity.CodeGroupName = txtCodeGroupName.Text;
            entity.CodeGroupKey = txtCodeGroupKey.Text;
            entity.Memo = txtMemo.Text;
            entity.IsDeleted = false;
            new CodeGroupRule().Update(entity);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtCodeGroupName.Text.Trim()))
            {
                hidInfo.Value = "��������������ƣ�";
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeGroupKey.Text.Trim()))
            {
                hidInfo.Value = "��������룡";
                return false;
            }

            if (CodeGroupQuery.CheckCodeGroupName(txtCodeGroupName.Text.Trim(), this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "�����������Ѿ����ڣ��������������ƣ�";
                return false;
            }

            if (CodeGroupQuery.CheckCodeGroupKey(txtCodeGroupKey.Text.Trim(), this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "�����Ѿ����ڣ�������������룡";
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
        /// ����ID
        /// </summary>
        private long CodeGroupID
        {
            set { ViewState["CodeGroupID"] = value.ToString(); }
            get
            {
                if (ViewState["CodeGroupID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["CodeGroupID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        #endregion
    }
}
