<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="WebManage.Web.SystemManager.UserDetail" %>


<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人信息维护</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <fieldset style="display: marker; width: 500px; top: 10px; border: solid 1px #253E28"
            align="center">
            <legend>人员信息</legend>
            <table cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td>
                        姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
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
                        <asp:DropDownList ID="drpOUList" runat="server" Width="204px">
                        </asp:DropDownList></td>
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
                        &nbsp;</td>
                </tr>
                            
                <tr>
                    <td>
                        电子邮件：</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                            
                <tr>
                    <td>
                        办公电话：</td>
                    <td>
                        <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                            
                <tr>
                    <td>
                        手机号码：</td>
                    <td>
                        <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="12" Width="200px" onfocus="TextPanl(this,true)" onblur="TextPanl(this,false)"></asp:TextBox></td>
                    <td>
                        &nbsp;</td>
                </tr>
                            
            </table>
        </fieldset>
        <table>
            <tr>
                <td style="height: 10px; width: 3px;">
                </td>
            </tr>
        </table>
        <fieldset style="display: marker; width: 500px; border: solid 1px #253E28" align="center">
            <legend>人员帐号</legend>
            <table cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td>
                        帐号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccount" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: #ff0000">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword1" runat="server" MaxLength="20" Width="200px" TextMode="password"></asp:TextBox>
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
                        <asp:TextBox ID="txtPassword2" runat="server" MaxLength="20" Width="200px" TextMode="password"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: Red">*</span>
                    </td>
                </tr>               
            </table>
        </fieldset>
        <table align="center" style="margin-top: 10px;">
            <tr>
                <td align="center" style="height: 40px">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />&nbsp;
                    <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <input id="hidInfo" type="hidden" runat="server" />
    </form>
</body>
</html>
