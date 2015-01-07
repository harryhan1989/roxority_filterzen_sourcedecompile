using System;
using System.Web.UI;
using WebUI;
using Nandasoft;
using BLL;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Nandasoft.Helper;

namespace WebManage.Web.SystemManager
{
    public partial class OUDetail : BasePage
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('OUDetailLY','btnRefresh')", true);
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('OUDetailLY','btnRefresh')", true);
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
                this.OUID = NDConvert.ToInt64(Request.QueryString["ID"].ToString());

                if (this.CurOperation == (int)OperationEnum.INSERT)
                {
                    if (this.OUID != 0)
                    {
                        OUEntity entity = new OUEntity(this.OUID);
                        drpOUList.SelectedValue = NDConvert.ToString(entity.OUID);
                    }
                    //else
                    //{
                    //    drpOUList.Text = "无";
                    //}
                }
                else
                {
                    OUEntity entity = new OUEntity(this.OUID);
                    if (entity.OUParentID != 0)
                    {
                        OUEntity entityP = new OUEntity(entity.OUParentID);
                        drpOUList.SelectedValue = NDConvert.ToString(entityP.OUID);
                    }
                    //else
                    //{
                    //    drpOUList.Text = "无";
                    //}
                }
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.INSERT)
            {
                BindOUList();

                if (this.OUID != -1)
                {
                    drpOUList.SelectedValue = NDConvert.ToString(this.OUID);
                    drpOUList.Enabled = false;
                }
                else
                {
                    drpOUList.Enabled = false;
                }
            }

            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                BindOUList();
                LoadData();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            OUEntity entity = new OUEntity(this.OUID);
            OUEntity parentEntity = new OUEntity(entity.OUParentID);
            txtOUName.Text = entity.OUName;
            if (this.OUID != -1)
            {
                drpOUList.SelectedValue = NDConvert.ToString(parentEntity.OUID);
            }
            else if (entity.OUParentID == 0)
            {
                drpOUList.Enabled = false;
            }
            txtDescriptipn.Text = entity.Description;
        }

        /// <summary>
        /// 邦定父级部门
        /// </summary>
        private void BindOUList()
        {
            DataTable dt = new OUQuery().GetParentOU(0);
            NDHelperWebControl.LoadDropDownList(drpOUList, dt, "OUName", "OUID", new ListItem("请选择", "0"));
           
            //drpOUList.SelectedIndex = 1;
            //drpOUList.Enabled = false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            OUEntity entity = new OUEntity();
            entity.OUName = txtOUName.Text.Trim();
            entity.OUType = 1;
            if (this.OUID != -1)
            {
                entity.OUParentID = this.OUID;
            }
            else
            {
                entity.OUParentID = 0;
            }
            entity.Description = txtDescriptipn.Text.Trim();
            entity.SortIndex = OUQuery.GetMaxSortIndex();
            entity.AddDate = DateTime.Now;
            entity.IsDeleted = false;
            new OURule().Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            OUEntity entity = new OUEntity(this.OUID);
            entity.OUParentID = NDConvert.ToInt64(drpOUList.SelectedValue);
            entity.OUType = 1;
            entity.OUName = txtOUName.Text.Trim();
            entity.Description = txtDescriptipn.Text.Trim();
            if (entity.SortIndex == 0)
            {
                entity.SortIndex = OUQuery.GetMaxSortIndex();
            }
            entity.UpdateDate = DateTime.Now;
            new OURule().Update(entity);
        }

        /// <summary>
        /// 检测数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtOUName.Text.Trim()))
            {
                hidInfo.Value = "请输入部门名称！";
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
        private long OUID
        {
            set { ViewState["OUID"] = value.ToString(); }
            get
            {
                if (ViewState["OUID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["OUID"].ToString());
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
