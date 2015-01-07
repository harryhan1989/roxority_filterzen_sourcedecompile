<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CodeGroupEdit.aspx.cs" Inherits="WebManage.Web.SysManager.CodeGroupEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <fieldset style="display: block; width: 700px; top: 10px; border: solid 1px #253E28"
            align="center">
            <legend>代码组维护</legend>
            <table cellpadding="2" cellspacing="4" align="center" style="margin-top: 10px;">
                <tr>
                    <td style="width: 80px;">
                        代码组名称：
                    </td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="txtCodeGroupName" runat="server" Width="99%" MaxLength="40"></asp:TextBox>
                    </td>
                    <td style="width: 20px; color: Red">
                        *
                    </td>
                    <td style="width: 40px;">
                        代码：
                    </td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="txtCodeGroupKey" runat="server" Width="97%" MaxLength="40"></asp:TextBox>
                    </td>
                     <td style="width: 20px; color: Red">
                        *
                    </td>
                </tr>
                <tr>
                    <td>
                        备注：</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtMemo" runat="server" Width="99%" MaxLength="80"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 45px">
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <input id="hidInfo" type="hidden" runat="server" />
    </form>
</body>
</html>
