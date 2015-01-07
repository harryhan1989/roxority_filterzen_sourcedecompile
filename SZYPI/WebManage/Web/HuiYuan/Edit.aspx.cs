using System;
using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;
using System.Configuration;
using BLL.Rule;
using BLL.Entity;

namespace WebManage.Web.HuiYuan
{
    /// <summary>
    /// 会员编辑页面
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    public partial class Edit : BasePage
    {
        HuiYuanEntity entity = new HuiYuanEntity();

        #region 页面加载
        /// <summary>
        /// 加载页面
        /// 作者：姚东
        /// 时间：20100919
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
        #endregion

        #region 保存信息
        /// <summary>
        /// 保存
        /// 作者：姚东
        /// 时间：20100919
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
                        Save(1);
                        PageHelper.ShowMessage("保存成功！");
                        break;
                    case (int)OperationEnum.UPDATE:
                        {
                            Update();
                            PageHelper.ShowMessage("更新成功！");
                        }
                        break;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('HuiYuanEdit','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }
        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('HuiYuanEdit','btnRefresh');", true);
        }
        #endregion

        #region 获得Url参数
        /// <summary>
        /// 获得URL参数
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] != "")
            {
                this.CurOperation = NDConvert.ToInt32(Request.QueryString["Operation"].ToString());
            }
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.HuiYuanID = Request.QueryString["ID"].ToString();
            }
        }
        #endregion

        #region 初始化页面操作
        /// <summary>
        /// 初始化页面操作
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
               LoadData();
            }
        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void LoadData()
        {
            HuiYuanEntity hyEntity = new HuiYuanEntity(this.HuiYuanID);

            txtName.Text = hyEntity.Name;
            txtAccount.Text = hyEntity.LoginAcc;
            txtPassWD.Text = hyEntity.LoginPWD;
            txtConfirmPassWD.Text = hyEntity.LoginPWD;
            txtEmail.Text = hyEntity.Email;
            txtMobile.Text = hyEntity.Tel;

            txtAccount.Enabled = false;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void Save(int Status)
        {
            entity.id = Guid.NewGuid().ToString();
            entity.Name = txtName.Text;
            entity.LoginAcc = txtAccount.Text;
            entity.LoginPWD = txtPassWD.Text;
            entity.Email = txtEmail.Text;
            entity.Tel = txtMobile.Text;
            entity.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            new HuiYuanRule().Add(entity);
            this.HuiYuanID = entity.id;
        }
        
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private void Update()
        {
            HuiYuanEntity entity = new HuiYuanEntity(this.HuiYuanID);

             #region 更新内容
            entity.id = this.HuiYuanID;
            entity.Name = txtName.Text.Trim();
            entity.LoginAcc = txtAccount.Text.Trim();

            if (!txtPassWD.Text.Trim().Equals(string.Empty))
            {
                entity.LoginPWD = txtPassWD.Text.Trim();
            }

            entity.Email = txtEmail.Text.Trim();
            entity.Tel = txtMobile.Text.Trim();
         
            #endregion

            new HuiYuanRule().Update(entity);
        }
        #endregion

        #region 验证页面数据
        /// <summary>
        /// 验证页面数据
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private bool CheckData()
        {
            entity.LoginAcc = txtAccount.Text.Trim();            

            if (this.CurOperation == (int)OperationEnum.INSERT && entity.IsExistByPropertys())
            {
                hidInfo.Value = "该账号已存在，请更换后重新注册！";
                txtAccount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                hidInfo.Value = "请输入会员姓名！";
                txtName.Focus();
                return false;
            }

            if ((!string.IsNullOrEmpty(txtPassWD.Text.Trim()) || 
                !string.IsNullOrEmpty(txtConfirmPassWD.Text.Trim())) && 
                txtPassWD.Text != txtConfirmPassWD.Text)
            {
                hidInfo.Value = "密码与确认密码必须一致！";
                txtConfirmPassWD.Focus();
                return false;
            }           

            return true;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 页面状态属性
        /// 作者：姚东
        /// 时间：20100919
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
        /// 会员ID
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        private string HuiYuanID
        {
            set { ViewState["HuiYuanID"] = value.ToString(); }
            get
            {
                if (ViewState["HuiYuanID"] != null)
                {
                    return ViewState["HuiYuanID"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion
    }
}