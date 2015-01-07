<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="WebUI.help" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
    *,td,tr { font:9pt ;margin:0; padding:0; }
    body,html{height:100%;}
    </style>
</head>
<body style=" font:9pt ">
    <form id="form1" runat="server">
        &nbsp;
        <asp:GridView ID="GridView1" runat="server">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:GridView>
    </form>
</body>
</html>
