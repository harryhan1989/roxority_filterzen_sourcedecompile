<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ModifyPassword.aspx.cs"
    Inherits="WebManage.Web.HuiYuan.ModifyPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户账号</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    
    <script src="../../Script/PageControl.js" type="text/javascript" language="javascript"
        charset="gb2312"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <fieldset style="display: marker; width: 500px; top: 10px; border: solid 1px #253E28"
            align="center">
            <legend>修改密码</legend>
            <br />
            <table cellpadding="4" cellspacing="4" align="center">
                <tr>
                    <td>
                        原密码：</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="200px" TextMode="password" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        新密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword1" runat="server" MaxLength="50" Width="200px" TextMode="password" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        重复密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword2" runat="server" MaxLength="50" Width="200px" TextMode="password" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center" style="height: 70px">
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
        <input id="hidInfo" runat="server" type="hidden" />
    </form>
</body>
</html>
