using System;

namespace WebManage.Web.HuiYuan
{
    /// <summary>
    /// 会员注册页面
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.bLoadCSS = false;
            //this.bLoadJS = false;
        }

        private void Image2_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //DBFactory db = this.DBAcc;

            //BLL. entity = new SZ12355.DataEntity.HuiYuanEntity(db);
            //db.BeginTran();
           
            //try
            //{
            //    this.DataID = Guid.NewGuid().ToString();
            //    NewEase.WebUI.GeneralData.GeneralDataUtil.SetEntityFromForm(this, this.DataID, entity);

            //    errListOutput errmsg = new errListOutput();
            //    errmsg.Start();
            //    if (entity.CheckData(errmsg))
            //    {

            //        entity.SetFieldValue("CreateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //        entity.Insert(db);
            //        this.RegisterStartupScript("msg", "<script language=javascript>alert('注册成功');dosu();</script>");
            //    }
            //    else
            //    {
            //        errmsg.End();
            //        //this.lblMsg.Text = errmsg.ToHTML();
            //        this.RegisterStartupScript("msg", "<script language=javascript>alert('用户名已存在');</script>");
            //    }
            //    db.Commit();

            //}
            //catch (Exception E)
            //{
            //    db.Rollback();
            //    FrameWeb.WebCommon.ExceptionMessage(E);
            //}
        }
    }
}