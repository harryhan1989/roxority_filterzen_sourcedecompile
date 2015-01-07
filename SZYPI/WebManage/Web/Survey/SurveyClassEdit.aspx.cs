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

namespace WebManage.Web.Survey
{
    public partial class SurveyClassEdit : BasePage
    {
        SurveyClassEntity entity = new SurveyClassEntity();

        #region 页面加载
        /// <summary>
        /// 加载页面
        /// 作者：姚东
        /// 时间：20101027
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
        /// 时间：20101027
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('SurveyClassEdit','btnRefresh');", true);
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
        /// 时间：20101027
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('SurveyClassEdit','btnRefresh');", true);
        }
        #endregion

        #region 获得Url参数
        /// <summary>
        /// 获得URL参数
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] != "")
            {
                this.CurOperation = NDConvert.ToInt32(Request.QueryString["Operation"].ToString());
            }
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.CID = NDConvert.ToInt64(Request.QueryString["ID"]);
            }
        }
        #endregion

        #region 初始化页面操作
        /// <summary>
        /// 初始化页面操作
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                txtCID.Enabled = false;
                LoadData();
            }
            else
            {
                trCID.Visible = false;
            }
        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private void LoadData()
        {
            SurveyClassEntity entity = new SurveyClassEntity(this.CID);

            txtCID.Text = entity.CID.ToString();
            txtSurveyClassName.Text = entity.SurveyClassName;
            txtSort.Text = entity.Sort.ToString();

            if (entity.DefaultClass == true)
            {
                ddlDefaultClass.SelectedValue = "1";
            }
            else
            {
                ddlDefaultClass.SelectedValue = "0";
            }

            txtCID.Enabled = false;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private void Save(int Status)
        {
            entity.SurveyClassName = txtSurveyClassName.Text;
            entity.Sort = NDConvert.ToInt32(txtSort.Text);

            if (ddlDefaultClass.SelectedValue == "1")
            {
                entity.DefaultClass = true;
            }
            else
            {
                entity.DefaultClass = false;
            }

            entity.ParentID = -1;

            new SurveyClassRule().Add(entity);
            this.CID = entity.CID;
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private void Update()
        {
            SurveyClassEntity entity = new SurveyClassEntity(this.CID);

            #region 更新内容
            entity.SurveyClassName = txtSurveyClassName.Text;
            entity.Sort = NDConvert.ToInt32(txtSort.Text);

            if (ddlDefaultClass.SelectedValue == "1")
            {
                entity.DefaultClass = true;
            }
            else
            {
                entity.DefaultClass = false;
            }

            entity.ParentID = -1;

            #endregion

            new SurveyClassRule().Update(entity);
        }
        #endregion

        #region 验证页面数据
        /// <summary>
        /// 验证页面数据
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private bool CheckData()
        {
            entity.SurveyClassName = txtSurveyClassName.Text.Trim();            

            if (string.IsNullOrEmpty(txtSurveyClassName.Text.Trim()))
            {
                hidInfo.Value = "请输入会员类型名称！";
                txtSurveyClassName.Focus();
                return false;
            }

            if (ddlDefaultClass.SelectedValue.Trim() == "-1")
            {
                hidInfo.Value = "请选择是否为默认类型！";
                ddlDefaultClass.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 页面状态属性
        /// 作者：姚东
        /// 时间：20101027
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
        /// 问卷类型ID
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        private long CID
        {
            set { ViewState["CID"] = value; }
            get
            {
                if (ViewState["CID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["CID"]);
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