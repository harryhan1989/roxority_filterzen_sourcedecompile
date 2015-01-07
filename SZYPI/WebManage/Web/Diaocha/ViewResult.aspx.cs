using System;
using Nandasoft;

namespace WebManage.Web.Diaocha
{
	/// <summary>
	/// ViewResult 的摘要说明。
	/// </summary>
    public class ViewResult : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Repeater Repeater1;
		protected System.Web.UI.WebControls.Label lblTitle;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Repeater1.DataSource = NDDBAccess.Fill("select * from uv_diaocha where ishome=1");
			this.Repeater1.DataBind();
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
