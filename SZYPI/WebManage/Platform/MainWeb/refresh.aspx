<%@ Page Language="C#" AutoEventWireup="true" Codebehind="refresh.aspx.cs" Inherits="WebUI.Refresh" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Timer ID="timerRefreshOnline" runat="server" Interval="60000" OnTick="timerRefreshOnline_Tick">
            </asp:Timer>
            <asp:Timer ID="timerRefresh" runat="server" OnTick="timerRefresh_Tick" EnableViewState="False">
            </asp:Timer>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    &nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />&nbsp;
                    <asp:Button ID="btnRefreshOnline" runat="server" Text="Button" OnClick="btnRefreshOnline_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
