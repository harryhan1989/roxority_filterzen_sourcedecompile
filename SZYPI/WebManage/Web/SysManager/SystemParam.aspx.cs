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

namespace WebManage.Web.SystemManager
{
    public partial class SystemParam : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            this.grid.DataSource = new SystemParamQuery().GetGardenSystemParam();
            this.grid.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grid.EditIndex = e.NewEditIndex;
            this.grid.DataSource = new SystemParamQuery().GetGardenSystemParam(); 
            this.grid.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grid.EditIndex = -1;
            this.grid.DataSource = new SystemParamQuery().GetGardenSystemParam(); 
            this.grid.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            long id = (long)grid.DataKeys[e.RowIndex].Values[0];
            string ParamName = ((TextBox)grid.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string ParamValue = ((TextBox)grid.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Replace("<", "&lt;").Replace(">", "&gt;");
            string Memo = ((TextBox)grid.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            if (string.IsNullOrEmpty(ParamName))
            {
                PageHelper.ShowExceptionMessage("�������Ʋ���Ϊ��");
                return;
            }
            else
            {
                if (ParamName.Length > 50)
                {
                    PageHelper.ShowExceptionMessage("�������Ƴ��Ȳ��ܳ���50���ַ�");
                    return;
                }

                //if (AuthCommon.SpecialStringAuth(ParamName))
                //{
                //    PageHelper.ShowExceptionMessage("�������Ʋ��ܰ��������ַ�");
                //    return;
                //}
            }

            if (string.IsNullOrEmpty(ParamValue))
            {
                PageHelper.ShowExceptionMessage("����ֵ����Ϊ��");
                return;
            }
            else
            {
                //if (AuthCommon.SpecialStringAuth(ParamValue))
                //{
                //    PageHelper.ShowExceptionMessage("����ֵ���ܰ��������ַ�");
                //    return;
                //}
            }

            //if (!string.IsNullOrEmpty(Memo))
            //{
            //    if (AuthCommon.SpecialStringAuth(Memo))
            //    {
            //        PageHelper.ShowExceptionMessage("��ע�в��ܰ��������ַ�");
            //        return;
            //    }
            //}

            SystemParamEntity eneity = new SystemParamEntity(id);
            eneity.ParamName = ParamName;
            eneity.ParamValue = ParamValue;
            eneity.Description = Memo;
            new SystemParamRule().Update(eneity);

            this.grid.EditIndex = -1;
            this.grid.DataSource = new SystemParamQuery().GetGardenSystemParam(); 
            this.grid.DataBind();
        }     
    }
}
