<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refreshMessage.aspx.cs" Inherits="WebUI.refreshMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="timerRefresh" runat="server" OnTick="timerRefresh_Tick" EnableViewState="False">
                </asp:Timer>
                <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />
                &nbsp;
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
