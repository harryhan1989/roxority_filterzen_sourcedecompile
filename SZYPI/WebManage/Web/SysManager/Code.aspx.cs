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
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitPage();
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
                        PageHelper.ShowMessage("删除成功！");
                    }
                    if (CodeNames != "")
                    {
                        string msgErr = "代码名称为" + CodeNames + "的记录存在子记录，不可删除！\\n\\n" +
                                        "可先删除代码子记录后再删除相应代码！";
                        PageHelper.ShowExceptionMessage(msgErr);
                    }
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
                        this.CodeID = 0;
                        PageHelper.ShowMessage("更新成功！");
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CodeDetail", "showModalWindow('CodeDetailLY','子代码信息',600, 560,'../../Web/SysManager/CodeDetail.aspx?CodeID=" + this.CodeID + "');", true);
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
                        PageHelper.ShowMessage("删除成功！");
                        BindGridView();
                    }
                    else
                    {
                        string msgErr = "该代码存在子记录，不可删除！\\n\\n" +
                                        "可先删除代码子记录后再删除该代码！";
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
                    e.Row.Cells[5].Attributes.Add("onclick", "return confirm('确定要删除此记录吗？');");
                }
            }
        }

        /// <summary>
        /// 获得URL参数
        /// </summary>
        private void GetUrlParameter()
        {          
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.CodeGroupID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            this.Operation = (int)OperationEnum.INSERT;
            BindGridView();
        }

        /// <summary>
        /// 邦定群组数据
        /// </summary>
        private void BindGridView()
        {
            DataTable dt = new CodeQuery().GetAllCode(this.CodeGroupID);
            grid.DataSource = dt;
            grid.DataBind();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtCodeName.Text))
            {
                hidInfo.Value = "请输入代码名称！";
                txtCodeName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeValue.Text))
            {
                hidInfo.Value = "请输入代码值！";
                txtCodeValue.Focus();
                return false;
            }

            if (CodeQuery.CheckCodeName(txtCodeName.Text.Trim(),this.CodeID ,this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "代码名称已经存在，请重新输入名称！";
                txtCodeName.Focus();
                return false;
            }

            if (CodeQuery.CheckCodeValue(txtCodeValue.Text.Trim(), this.CodeID, this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "代码值已经存在，请重新输入代码值！";
                txtCodeValue.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存
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
        /// 更新
        /// </summary>
        private void Update()
        {
            CodeEntity entity = new CodeEntity(this.CodeID);
            entity.CodeName = txtCodeName.Text.Trim();
            entity.CodeValue = txtCodeValue.Text.Trim();
            entity.Memo = txtMemo.Text.Trim();
            new CodeRule().Update(entity);
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

        /// <summary>
        /// 代码组ID
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
