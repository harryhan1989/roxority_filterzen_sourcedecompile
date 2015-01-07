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
        /// 加载页面
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
        /// 保存
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
                        PageHelper.ShowMessage("更新成功！");
                        break;
                    case (int)OperationEnum.INSERT:
                        Save();
                        PageHelper.ShowMessage("保存成功！");
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('CodeGroupEditLY','btnRefresh')", true);
        }

        /// <summary>
        /// 获得URL参数
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
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                LoadData();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            CodeGroupEntity entity = new CodeGroupEntity(this.CodeGroupID);
            txtCodeGroupName.Text = entity.CodeGroupName;
            txtCodeGroupKey.Text = entity.CodeGroupKey;
            txtMemo.Text = entity.Memo;               
        }

        /// <summary>
        /// 保存
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
        /// 更新
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
        /// 检测数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtCodeGroupName.Text.Trim()))
            {
                hidInfo.Value = "请输入代码组名称！";
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeGroupKey.Text.Trim()))
            {
                hidInfo.Value = "请输入代码！";
                return false;
            }

            if (CodeGroupQuery.CheckCodeGroupName(txtCodeGroupName.Text.Trim(), this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "代码组名称已经存在，请重新输入名称！";
                return false;
            }

            if (CodeGroupQuery.CheckCodeGroupKey(txtCodeGroupKey.Text.Trim(), this.CodeGroupID, this.CurOperation))
            {
                hidInfo.Value = "代码已经存在，请重新输入代码！";
                return false;
            }
           
            return true;
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
                    return -1;
                }
            }
        }

        /// <summary>
        /// 部门ID
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
