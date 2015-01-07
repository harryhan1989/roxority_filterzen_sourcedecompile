<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UserInfo.aspx.cs" Inherits="WebManage.Web.SystemManager.UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Script/PageControl.js" type="text/javascript" language="javascript" charset="gb2312"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <fieldset style="display: marker; width: 500px; top: 10px; border: solid 1px #253E28"
            align="center">
            <legend>个人信息</legend>
            <table cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td>
                        姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="50" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        帐号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccount" runat="server" Width="200px" MaxLength="50" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        部门：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOUName" runat="server" MaxLength="50" Width="200px" Enabled="False"></asp:TextBox></td>
                    <td>
                        <span style="color: #ff0000">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdolstSex" runat="server" RepeatDirection="Horizontal" Width="117px">
                            <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                            <asp:ListItem Value="2">女</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        电子邮件：
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        办公电话：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        手机号码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="12" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox></td>
                    <td>
                        <span id="spanMobileMark" runat="server" visible="false" style="color: #ff0000">*</span>
                    </td>
                </tr>                
                <tr>
                    <td colspan="3" align="center" style="height: 50px">
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <input id="hidInfo" runat="server" type="hidden" />
    </form>
</body>
</html>
