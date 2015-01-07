<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoDiaoCha.aspx.cs" Inherits="WebManage.Controls.XiaoDiaoCha" %>
<%@ Register TagPrefix="uc1" TagName="utlDiaoCha" Src="~/Controls/utlDiaoCha2.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>小调查</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc1:utlDiaoCha id="utldiaocha2" runat="server"></uc1:utlDiaoCha>
    </div>
    </form>
</body>
</html>
