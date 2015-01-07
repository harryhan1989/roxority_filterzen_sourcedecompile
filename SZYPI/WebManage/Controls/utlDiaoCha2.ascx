<%@ Control Language="c#" AutoEventWireup="false" Codebehind="utlDiaoCha2.ascx.cs" Inherits="WebManage.Controls.utlDiaoCha2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div style="LINE-HEIGHT: 18pt">
<%if(true)
{
   System.Data.DataTable dt = Nandasoft.NDDBAccess.Fill("select * from ut_diaocha where ishome=1 order by edittime asc");
   int i=0;
			foreach(System.Data.DataRow row in dt.Rows)
			{
				Response.Write("&nbsp;&nbsp;" + (i+1).ToString() + "、" +row["title"].ToString());
				Response.Write("<BR>");
				bool single = row["diaochatype"].ToString() == "1";
				string[] XHList = new string[]{"","A","B","C","D","E","F"};

				for(int j=1;j<=6;j++)
				{
					string content = row["a" + j.ToString()].ToString();
					if(content == "")
					{
					   continue;
					}
					Response.Write("&nbsp;&nbsp;<input type=" + (single?"radio":"checkbox") + " name=d_" + i.ToString()  + " value='" + j.ToString() + "'>");
					Response.Write(XHList[j] + "、" + content);
					Response.Write("<BR>");
					
				}
				Response.Write("<input type=hidden name=t_" + i.ToString() + " value='" + row["id"].ToString() + "'>");					
				i++;
			}
}
%>
</div>
<br>
<div align="center">
	<asp:ImageButton id="btnTiJiao" Text="提交" runat="server" ImageUrl="~/CSS/image/Index/toupiao.jpg"></asp:ImageButton><FONT face="宋体">&nbsp;</FONT>
	<a href="#" onclick="OpenViewResult();return false;" target="ck"><IMG alt="查看结果" src="../../CSS/image/Index/chakan.jpg" border=0></a>
</div>

<script type="text/javascript">
function OpenViewResult()
{
    window.open("ViewResult.aspx");
    
    window.close();
}
</script>

