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
    public partial class MenuFunction : BasePage
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
                    case (int)OperationEnum.INSERT:
                        Save();
                        PageHelper.ShowMessage("�����ɹ���");
                        break;
                    case (int)OperationEnum.UPDATE:
                        Update();
                        PageHelper.ShowMessage("���³ɹ���");
                        break;
                }
                txtFName.Text = "";
                txtFCode.Text = "";
                for (int i = 0; i < rdoIsShow.Items.Count; i++)
                {
                    if (rdoIsShow.Items[i].Value == "1")
                    {
                        rdoIsShow.Items[i].Selected = true;
                    }
                }
                this.CurOperation = (int)OperationEnum.INSERT;
                BindGridView();
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
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
        /// toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    int succeed = 0;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            this.FID = NDConvert.ToInt64(grid.DataKeys[i]["FID"].ToString());

                            MenuFunctionEntity entity = new MenuFunctionEntity(FID);
                            
                            entity.IsDeleted = true;
                            new MenuFunctionRule().Update(entity);
                            succeed++;
                            
                        }
                    }
                    if (succeed > 0)
                    {
                        BindGridView();
                        PageHelper.ShowMessage("ɾ���ɹ���");
                    }
                    break;
            }
        }

        /// <summary>
        /// grid��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = NDConvert.ToInt32(e.CommandArgument.ToString());
            this.FID = NDConvert.ToInt64(grid.DataKeys[RowIndex].Values["FID"].ToString());
            MenuFunctionEntity entity = new MenuFunctionEntity(this.FID);

            switch (e.CommandName)
            {
                case "Modify":
                    this.CurOperation = (int)OperationEnum.UPDATE;
                    txtFName.Text = entity.FName;
                    txtFCode.Text = entity.FCode;

                    for (int i = 0; i < rdoIsShow.Items.Count; i++)
                    {
                        if (entity.IsShow && rdoIsShow.Items[i].Value == "1")
                        {
                            rdoIsShow.Items[i + 1].Selected = false;
                            rdoIsShow.Items[i].Selected = true;
                        }
                        else if (!entity.IsShow && rdoIsShow.Items[i].Value == "0")
                        {
                            rdoIsShow.Items[i].Selected = true;
                        }
                    }
                    break;
                case "Del":
                   
                    entity.IsDeleted = true;
                    new MenuFunctionRule().Update(entity);
                    PageHelper.ShowMessage("ɾ���ɹ���");
                    BindGridView();
                    break;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitPage()
        {
            this.CurOperation = (int)OperationEnum.INSERT;
            BindGridView();
        }

        /// <summary>
        /// ���URL����
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.MenuID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());
            }
        }

        /// <summary>
        /// ��grid
        /// </summary>
        private void BindGridView()
        {
            DataTable dt = GetData();
            grid.DataSource = dt;
            grid.DataBind();
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[5].Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ���˼�¼��');");
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        private DataTable GetData()
        {
            DataTable dt = new MenuFunctionQuery().GetMenuFunctionNow(this.MenuID);
            return dt;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            MenuFunctionEntity entity = new MenuFunctionEntity();
            entity.FCode = txtFCode.Text.Trim();
            entity.FName = txtFName.Text.Trim();
            entity.IsDeleted = false;
            entity.MenuID = this.MenuID;
            if(rdoIsShow.SelectedValue == "0")
            {
                entity.IsShow = false;
            }
            else
            {
                entity.IsShow = true;
            }
            entity.SortIndex = MenuFunctionQuery.MaxSortIndex(this.MenuID);
            new MenuFunctionRule().Add(entity);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Update()
        {
            MenuFunctionEntity entity = new MenuFunctionEntity(this.FID);
            entity.FCode = txtFCode.Text.Trim();
            entity.FName = txtFName.Text.Trim();
            if (rdoIsShow.SelectedValue == "0")
            {
                entity.IsShow = false;
            }
            else
            {
                entity.IsShow = true;
            }
            new MenuFunctionRule().Update(entity);
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        private bool CheckData()
        {
            if (!string.IsNullOrEmpty(txtFName.Text))
            {
                if (CommonHelp.CheckString(txtFName.Text.Trim()))
                {
                    hidInfo.Value = "���������в��ܰ��������ַ���";
                    txtFName.Focus();
                    return false;
                }
            }
            else
            {
                hidInfo.Value = "�������Ʋ���Ϊ�գ�";
                txtFName.Focus();
                return false;
            }
            if (MenuFunctionQuery.IsExisitName(this.MenuID, txtFName.Text.Trim()))
            {
                hidInfo.Value = "�˹��������Ѿ����ڣ�";
                txtFName.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtFCode.Text))
            {
                if (CommonHelp.CheckString(txtFCode.Text.Trim()))
                {
                    hidInfo.Value = "���������в��ܰ��������ַ���";
                    txtFCode.Focus();
                    return false;
                }
            }
            else
            {
                hidInfo.Value = "�������Ʋ���Ϊ�գ�";
                txtFCode.Focus();
                return false;
            }

           
            if (MenuFunctionQuery.IsExisitCode(this.MenuID, txtFCode.Text.Trim()))
            {
                hidInfo.Value = "�˴��������Ѿ����ڣ�";
                txtFCode.Focus();
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
            set { ViewState["CurOperation"] = value.ToString(); }
            get
            {
                if (ViewState["CurOperation"] != null)
                {
                    return NDConvert.ToInt32(ViewState["CurOperation"].ToString());
                }
                else
                {
                    return 0;
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

        /// <summary>
        /// ��ťID
        /// </summary>
        private long FID
        {
            set { ViewState["FID"] = value.ToString(); }
            get
            {
                if (ViewState["FID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["FID"].ToString());
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
