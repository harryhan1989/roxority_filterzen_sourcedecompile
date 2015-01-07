<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExecSqlPage.aspx.cs" Inherits="WebManage.Web.SysManager.ExecSqlPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtSqlArea" runat="server" TextMode="MultiLine" Height="200px" 
                        Width="865px"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="btnExec" runat="server" Text="执行" onclick="btnExec_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="grid" runat="server" Height="527px" Width="868px" 
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                        CellPadding="2" ForeColor="Black" GridLines="None" PagerSettings-Mode="Numeric" PagerSettings-Visible="true" pa PageSize="20" >
                        <FooterStyle BackColor="Tan" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                            HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                     
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
