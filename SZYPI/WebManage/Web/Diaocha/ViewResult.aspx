
<%@ Page language="c#" Codebehind="ViewResult.aspx.cs" AutoEventWireup="false" Inherits="WebManage.Web.Diaocha.ViewResult" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language="javascript">        
		function openwin(url)
		{
		   var win1 = window.open(url,"_blank");
		   win1.focus();
		}
		function refresh()
		{
		  document.getElementById("btnRefresh").click();
		}
		function RefreshParent()
		{
		   var parentwin = window.opener;
		   if(parentwin != null)
		   {
		      //parentwin.location.reload(true);		      
		      //parentwin.document.getElementById("btnRefresh").click();
		      parentwin.refresh();
		      window.close();
		   }
		}
		
		function refresh()
		{
		  document.getElementById("btnRefresh").click();
		}		
		function SelelctAll(flag)
		{		  
		   var obj = document.all("selectid");		   
		   if(obj.length == null)
		   {
		      obj.checked = flag;
		      return;
		   }		   
		   for(i=0;i<obj.length;i++)
		   {
		      obj[i].checked  = flag;
		   }
		}
		
		  function edit(id)
        {
           var url = "DiaoChaEdit.aspx?id=" + id;
           document.location = url;     
        }      
        
        function ViewPL(newsid)
        {
          var url = "PingLunManage.aspx?newsid=" + newsid;
           document.location = url;
            //var pop = window.open(url,"SSProduct");
            //pop.focus();     
            var result = (SelectInfo(url)); 
               
          // var result = true;
            //alert(result);  
            if(result)
            {            
               //refresh();              
            }
        }		 
		</script>
		
		<style type="text/css">
		/*UltraWebGrid Style-begin*/
        ..FrameStyle
        {
	        border-color:#009999;
	        border-width:1px;
	        border-style:solid;
	        border-collapse:collapse;	
        }        
        
        /*RowStyleDefault*/
        ..RowStyleDefault
        {
	        background-color:#FFFFFF;
	        border-style:solid;
	        border-color:#0083B9;
	        border-width:1px;
	        padding-left:5px;
        }
        
        /*HeaderStyleDefault*/
        ..HeaderStyleDefault
        {
	        background-color:#BFE0F8;
	        border-style:solid;
	        border-width:1px;
	        border-color:#0083B9;
	        font-size:9pt;
        }
        
        .Title
        {
            font-size:medium;
            font-weight:bold;
            width:100%;
            text-align:center;
            line-height:150%;
        }
        
        .form {
	    FONT-SIZE: 9pt;
	    COLOR: #000000;
	    HEIGHT: 20px;
	    border: 1px solid #37747B;
        }
        
        BODY {
	    FONT-FAMILY: "宋体"; FONT-SIZE: 9pt; 
	    SCROLLBAR-FACE-COLOR: #f6f6f6;
        }
        
        TD {
	        FONT-FAMILY: "宋体"; FONT-SIZE: 9pt; 
        }

		</style>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%" cellSpacing="1" cellPadding="1" border="0">
				<caption class="Title">
					<asp:Label id="lblTitle" runat="server">12355小调查结果查看</asp:Label></caption>
				<TBODY>
					<TR>
						<TD vAlign="top" align="left" width="100%"><FONT face="宋体"></FONT><FONT face="宋体"></FONT></TD>
					</TR>
				</TBODY>
			</TABLE>
			<asp:Repeater id="Repeater1" runat="server">
				<ItemTemplate>
					<table border="0" class="FrameStyle" align=center>
						<tr>
							<td colspan="2" class="RowStyleDefault"><%# DataBinder.Eval(Container, "DataItem.title") %></td>
						</tr>
						<tr align="center">
							<td width=250 class="HeaderStyleDefault">选项</td>
							<td width=50 class="HeaderStyleDefault">投票数</td>
						</tr>
						<%# DataBinder.Eval(Container, "DataItem.a1") == ""?"": "<tr><td class=RowStyleDefault>A、" + DataBinder.Eval(Container, "DataItem.a1") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a1").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a1")) + "</font></td></tr>"  %>
						<%# DataBinder.Eval(Container, "DataItem.a2") == ""?"": "<tr><td class=RowStyleDefault>B、" + DataBinder.Eval(Container, "DataItem.a2") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a2").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a2")) + "</font></td></tr>"  %>
						<%# DataBinder.Eval(Container, "DataItem.a3") == ""?"": "<tr><td class=RowStyleDefault>C、" + DataBinder.Eval(Container, "DataItem.a3") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a3").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a3")) + "</font></td></tr>"  %>
						<%# DataBinder.Eval(Container, "DataItem.a4") == ""?"": "<tr><td class=RowStyleDefault>D、" + DataBinder.Eval(Container, "DataItem.a4") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a4").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a4")) + "</font></td></tr>"  %>
						<%# DataBinder.Eval(Container, "DataItem.a5") == ""?"": "<tr><td class=RowStyleDefault>E、" + DataBinder.Eval(Container, "DataItem.a5") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a5").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a5")) + "</font></td></tr>"  %>
						<%# DataBinder.Eval(Container, "DataItem.a6") == ""?"": "<tr><td class=RowStyleDefault>F、" + DataBinder.Eval(Container, "DataItem.a6") + "</td><td class=RowStyleDefault align=center><font color=red>" + (DataBinder.Eval(Container, "DataItem.hd_a6").ToString()==""?"0":DataBinder.Eval(Container, "DataItem.hd_a6")) + "</font></td></tr>"  %>
					</table>
					<br>
				</ItemTemplate>
			</asp:Repeater>
		</form>
	</body>
</HTML>
