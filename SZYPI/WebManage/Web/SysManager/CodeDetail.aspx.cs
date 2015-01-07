using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI;
using Nandasoft;
using BLL;

namespace WebManage.Web.SysManager
{
    public partial class CodeDetail : BasePage
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
        /// toolbar事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            this.CodeDetailID = NDConvert.ToInt64(grid.DataKeys[i]["CodeDetailID"].ToString());

                            CodeDetailEntity entity = new CodeDetailEntity(CodeDetailID);
                            entity.IsDeleted = true;
                            new CodeDetailRule().Update(entity);
                        }
                    }
                    this.CurOperation = (int)OperationEnum.INSERT;
                    this.CodeDetailID = 0;
                    txtCodeDetailName.Text = "";
                    txtCodeDetailValue.Text = "";
                    txtDetailMemo.Text = "";
                    BindGridView();
                    PageHelper.ShowMessage("删除成功！");
                    break;
            }
        }

        /// <summary>
        ///  保存操作
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
                        PageHelper.ShowMessage("保存成功！");
                        break;
                    case (int)OperationEnum.UPDATE:
                        Update();
                        this.CurOperation = (int)OperationEnum.INSERT;
                        this.CodeDetailID = 0;
                        PageHelper.ShowMessage("更新成功！");
                        break;
                }
                txtCodeDetailName.Text = "";
                txtCodeDetailValue.Text = "";
                txtDetailMemo.Text = "";
                BindGridView();
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('CodeDetailLY','btnRefresh');", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = NDConvert.ToInt32(e.CommandArgument.ToString());
            this.CodeDetailID = NDConvert.ToInt64(grid.DataKeys[RowIndex].Values["CodeDetailID"].ToString());
            CodeDetailEntity entity = new CodeDetailEntity(CodeDetailID);

            switch (e.CommandName)
            {
                case "Modify":
                    txtCodeDetailName.Text = entity.CodeDetailName;
                    txtCodeDetailValue.Text = entity.CodeDetailValue;
                    txtDetailMemo.Text = entity.DetailMemo;
                    this.CurOperation = (int)OperationEnum.UPDATE;
                    break;
                case "Del":
                    entity.IsDeleted = true;
                    new CodeDetailRule().Update(entity);
                    txtCodeDetailName.Text = "";
                    txtCodeDetailValue.Text = "";
                    txtDetailMemo.Text = "";
                    BindGridView();
                    this.CurOperation = (int)OperationEnum.INSERT;
                    this.CodeDetailID = 0;
                    PageHelper.ShowMessage("删除成功！");
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
                e.Row.Cells[4].Attributes.Add("onclick", "return confirm('确定要删除此记录吗？');");
            }
        }

        /// <summary>
        /// 获得URL参数
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["CodeID"] != null && Request.QueryString["CodeID"] != "")
            {
                this.CodeID = NDConvert.ToInt64(Request.QueryString["CodeID"].ToString());
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            LoadData();
            this.Operation = (int)OperationEnum.INSERT;
            BindGridView();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            CodeEntity entity = new CodeEntity(this.CodeID);
            txtCodeName.Text = entity.CodeName;
        }

        /// <summary>
        /// 邦定数据
        /// </summary>
        private void BindGridView()
        {
            DataTable dt = new CodeDetailQuery().GetCodeDetail(this.CodeID);
            grid.DataSource = dt;
            grid.DataBind();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtCodeDetailName.Text.Trim()))
            {
                hidInfo.Value = "请输入子代码名称！";
                txtCodeDetailName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeDetailValue.Text.Trim()))
            {
                hidInfo.Value = "请输入子代码值！";
                txtCodeDetailValue.Focus();
                return false;
            }

            if (CodeDetailQuery.CheckCodeDetailName(txtCodeDetailName.Text.Trim(), this.CodeDetailID, this.CodeID, this.CurOperation))
            {
                hidInfo.Value = "子代码名称已经存在，请重新输入名称！";
                txtCodeDetailName.Focus();
                return false;
            }

            if (CodeDetailQuery.CheckCodeDetailValue(txtCodeDetailValue.Text.Trim(), this.CodeDetailID, this.CodeID, this.CurOperation))
            {
                hidInfo.Value = "子代码值已经存在，请重新输入子代码值！";
                txtCodeDetailValue.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            CodeDetailEntity entity = new CodeDetailEntity();
            entity.CodeID = this.CodeID;
            entity.CodeDetailName = txtCodeDetailName.Text.Trim();
            entity.CodeDetailValue = txtCodeDetailValue.Text.Trim();
            entity.DetailMemo = txtDetailMemo.Text.Trim();
            entity.IsDeleted = false;
            new CodeDetailRule().Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            CodeDetailEntity entity = new CodeDetailEntity(this.CodeDetailID);
            entity.CodeDetailName = txtCodeDetailName.Text.Trim();
            entity.CodeDetailValue = txtCodeDetailValue.Text.Trim();
            entity.DetailMemo = txtDetailMemo.Text.Trim();
            new CodeDetailRule().Update(entity);
        }

        #region 属性

        /// <summary>
        /// 页面状态属性
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
        /// 子代码ID
        /// </summary>
        private long CodeDetailID
        {
            set { ViewState["CodeDetailID"] = value.ToString(); }
            get
            {
                if (ViewState["CodeDetailID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["CodeDetailID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 代码ID
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

        #endregion
    }
}
