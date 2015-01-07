using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI;
using Nandasoft;
using BLL;

namespace WebManage.Web.SysManager
{
    public partial class Code: BasePage
    {
        /// <summary>
        /// 
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
        /// ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitPage();
        }

        /// <summary>
        /// toolbar�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    int succeed = 0;
                    string CodeNames = "";
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            this.CodeID = NDConvert.ToInt64(grid.DataKeys[i]["CodeID"].ToString());

                            CodeEntity entity = new CodeEntity(CodeID);
                            if (!CodeDetailQuery.IsExistCodeDetail(CodeID))
                            {
                                entity.IsDeleted = true;
                                new CodeRule().Update(entity);
                                succeed++;
                            }
                            else
                            {
                                CodeNames += "[" + entity.CodeName + "]";
                            }
                        }
                    }
                    this.CurOperation = (int)OperationEnum.INSERT;
                    this.CodeID = 0;
                    txtCodeName.Text = "";
                    txtCodeValue.Text = "";
                    txtMemo.Text = "";

                    if (succeed > 0)
                    {
                        BindGridView();
                        PageHelper.ShowMessage("ɾ���ɹ���");
                    }
                    if (CodeNames != "")
                    {
                        string msgErr = "��������Ϊ" + CodeNames + "�ļ�¼�����Ӽ�¼������ɾ����\\n\\n" +
                                        "����ɾ�������Ӽ�¼����ɾ����Ӧ���룡";
                        PageHelper.ShowExceptionMessage(msgErr);
                    }
                    break;
            }
        }

        /// <summary>
        ///  �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.CheckData())
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
                        PageHelper.ShowMessage("����ɹ���");
                        break;
                    case (int)OperationEnum.UPDATE:
                        Update();
                        this.CurOperation = (int)OperationEnum.INSERT;
                        this.CodeID = 0;
                        PageHelper.ShowMessage("���³ɹ���");
                        break;

                }
                txtCodeName.Text = "";
                txtCodeValue.Text = "";
                txtMemo.Text = "";
                BindGridView();
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = NDConvert.ToInt32(e.CommandArgument.ToString());
            this.CodeID = NDConvert.ToInt64(grid.DataKeys[RowIndex].Values["CodeID"].ToString());
            CodeEntity entity = new CodeEntity(CodeID);

            switch(e.CommandName)
            {
                case "Children":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CodeDetail", "showModalWindow('CodeDetailLY','�Ӵ�����Ϣ',600, 560,'../../Web/SysManager/CodeDetail.aspx?CodeID=" + this.CodeID + "');", true);
                    break;
                case "Modify":
                    txtCodeName.Text = entity.CodeName;
                    txtCodeValue.Text = entity.CodeValue;
                    txtMemo.Text = entity.Memo;
                    this.CurOperation = (int)OperationEnum.UPDATE;
                    break;
                case "Del":
                    if (!CodeDetailQuery.IsExistCodeDetail(this.CodeID))
                    {
                        entity.IsDeleted = true;
                        new CodeRule().Update(entity);
                        PageHelper.ShowMessage("ɾ���ɹ���");
                        BindGridView();
                    }
                    else
                    {
                        string msgErr = "�ô�������Ӽ�¼������ɾ����\\n\\n" +
                                        "����ɾ�������Ӽ�¼����ɾ���ô��룡";
                        PageHelper.ShowExceptionMessage(msgErr);
                    }
                    txtCodeName.Text = "";
                    txtCodeValue.Text = "";
                    txtMemo.Text = "";
                    this.CurOperation = (int)OperationEnum.INSERT;
                    this.CodeID = 0;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long ThisCodeID = NDConvert.ToInt64(grid.DataKeys[e.Row.RowIndex].Values["CodeID"].ToString());
                if (CodeDetailQuery.IsExistCodeDetail(ThisCodeID))
                {
                    e.Row.Cells[0].Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                }
                else
                {
                    e.Row.Cells[5].Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ���˼�¼��');");
                }
            }
        }

        /// <summary>
        /// ���URL����
        /// </summary>
        private void GetUrlParameter()
        {          
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
            this.Operation = (int)OperationEnum.INSERT;
            BindGridView();
        }

        /// <summary>
        /// �Ⱥ������
        /// </summary>
        private void BindGridView()
        {
            DataTable dt = new CodeQuery().GetAllCode(this.CodeGroupID);
            grid.DataSource = dt;
            grid.DataBind();
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtCodeName.Text))
            {
                hidInfo.Value = "������������ƣ�";
                txtCodeName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeValue.Text))
            {
                hidInfo.Value = "���������ֵ��";
                txtCodeValue.Focus();
                return false;
            }

            if (CodeQuery.CheckCodeName(txtCodeName.Text.Trim(),this.CodeID ,this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "���������Ѿ����ڣ��������������ƣ�";
                txtCodeName.Focus();
                return false;
            }

            if (CodeQuery.CheckCodeValue(txtCodeValue.Text.Trim(), this.CodeID, this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "����ֵ�Ѿ����ڣ��������������ֵ��";
                txtCodeValue.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            CodeEntity entity = new CodeEntity();
            entity.CodeGroupID = this.CodeGroupID;
            entity.CodeName = txtCodeName.Text.Trim();
            entity.CodeValue = txtCodeValue.Text.Trim();
            entity.Memo = txtMemo.Text.Trim();
            entity.IsDeleted = false;
            new CodeRule().Add(entity);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Update()
        {
            CodeEntity entity = new CodeEntity(this.CodeID);
            entity.CodeName = txtCodeName.Text.Trim();
            entity.CodeValue = txtCodeValue.Text.Trim();
            entity.Memo = txtMemo.Text.Trim();
            new CodeRule().Update(entity);
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
                    return 0;
                }
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        private long CodeID
        {
            set { ViewState["CodeID"] = value.ToString(); }
            get
            {
                if (ViewState["CodeID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["CodeID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// ������ID
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
