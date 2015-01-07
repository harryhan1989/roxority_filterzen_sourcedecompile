<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="WebManage.Install" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>InstallSessionState</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div1" runat="server">
        <br />
        <asp:Button ID="btnInstall" runat="server" Height="34px" OnClick="btnInstall_Click"
            Text="安装" Width="90px" />
        &nbsp;&nbsp;
        <asp:Button ID="btnUninstall" runat="server" Height="34px" OnClick="btnUninstall_Click"
            Text="卸载" Width="90px" />
        <br />
        <br />
        <asp:TextBox ID="txtErrorInfo" runat="server" Height="266px" TextMode="MultiLine"
            Width="521px" BorderStyle="Dotted"></asp:TextBox></div>
    </form>
</body>
</html>
