<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebManage.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>苏州市青少年公共信息分析平台</title>

    <script language="javascript" type="text/javascript">
        if (this.top.location.href != this.location.href) 
        {
            this.top.location.href = this.location.href;         
        }
    </script>

    <link href="Platform/MainWeb/css/login.css" rel="stylesheet" type="text/css" />
    <link href="CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">    
<div class="wrapper">
	<div class="logo"></div>
	<div class="banner">
		<div class="logindiv">
			<div class="itemdiv">用户名：<asp:TextBox ID="txtAccount" runat="server" CssClass="inputName"></asp:TextBox></div>
			<div class="itemdiv">密　码：<asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="inputPwd" ></asp:TextBox></div>
			<div class="itemdiv" style="display:none"><asp:TextBox ID="txtCheckCode" runat="server" CssClass="inputYZ" ></asp:TextBox><img src="images/yanzheng_03.jpg" /></div>
			<div class="loginbtns">
			    <asp:ImageButton ID="btnOK" runat="server" ImageUrl="PlatForm/images/loginbtn_03.gif" OnClick="btnOk_Click" />			    
			</div>
		</div>
	</div>
</div>
<div class="footer">版权所有：共青团苏州市委员会  技术支持：江苏瀚远科技股份有限公司</div>
    </form>
</body>
</html>
