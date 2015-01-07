<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MenuDetail.aspx.cs" Inherits="WebManage.Web.SystemManager.MenuDetail" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" align="center" style="padding: 10px 10px 0px 10px">
            <tr>
                <td width="70px">
                    菜单名称：
                </td>
                <td width="180px">
                    <asp:TextBox ID="txtMenuName" runat="server" Width="160px" MaxLength="50"></asp:TextBox><span
                        style="color: Red;">*</span>
                </td>
                <td width="70px">
                </td>
                <td width="180px">
                </td>
            </tr>
            <tr>
                <td>
                    类型：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpType" Width="165px">
                        <asp:ListItem Value="0">请选择</asp:ListItem>
                        <asp:ListItem Value="1">类型一</asp:ListItem>
                        <asp:ListItem Value="2">类型二</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td>
                    是否显示：
                </td>
                <td width="180px">
                    <asp:RadioButtonList ID="rdoIsDisplay" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    页面路径：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNavigateURL" runat="server" Width="380px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    图片路径：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtImageURL" runat="server" Width="380px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnOK" CssClass="button70" runat="server" Text="保存" OnClick="btnOK_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" Style="margin-left: 15px;"
                        OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <div style="display: none">
            <input id="hidInfo" type="hidden" runat="server" />
        </div>
    </form>
</body>
</html>
