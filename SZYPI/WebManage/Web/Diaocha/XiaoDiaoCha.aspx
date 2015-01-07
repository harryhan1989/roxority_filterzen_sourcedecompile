<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoDiaoCha.aspx.cs" Inherits="WebManage.Web.Diaocha.XiaoDiaoCha" %>
<%@ Register TagPrefix="uc1" TagName="utlDiaoCha" Src="~/Controls/utlDiaoCha2.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>小调查</title>
    <style type="text/css">
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
    </style>
     <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <!--小调查-->
		<table cellSpacing="0" cellPadding="0" width="204" border="0">
			<tr>
				<td vAlign="top" align="center" height="35" colspan="3">
					<div align="center"><IMG height="35" src="../../CSS/image/Index/diaocha.jpg" width="204" border="0"></div>
				</td>
			</tr>
			<%--<tr>
				<td vAlign="bottom" align="center" background="image/hljy_10.gif" colSpan="3" height="7"></td>
			</tr>--%>
			<tr>
				<%--<td width="5" rowspan="1" background="image/hljy_07.gif"></td>--%>
				<td align="left" width="194" height="100">
					<uc1:utlDiaoCha id="utldiaocha2" runat="server"></uc1:utlDiaoCha>
				</td>
				<%--<td align="right" width="5" background="image/hljy_08.gif"></td>--%>
			</tr>
			<tr>
				<%--<td vAlign="bottom" align="center" background="image/hljy_09.gif" colSpan="3" height="7"></td>--%>
			</tr>
		</table>
    

    </form>
</body>
</html>
