<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyClassEdit.aspx.cs" Inherits="WebManage.Web.Survey.SurveyClassEdit" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>问卷类型维护</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #eWebEditor1
        {
            width: 648px;
            height: 400px;
        }
        .style2
        {
            text-align: right;
            padding-right: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" align="center" style="margin-top: 10px;
        width: 390px;">
        <tr id="trCID" runat="server">
            <td class="style2">
                类型编码：
            </td>
            <td>
                <asp:TextBox ID="txtCID" runat="server" Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                类型名称：
            </td>
            <td>
                <asp:TextBox ID="txtSurveyClassName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                排序：
            </td>
            <td>
                <asp:TextBox ID="txtSort" runat="server" MaxLength="100" Width="200px" ></asp:TextBox>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                是否默认值：
            </td>
            <td>
                <asp:DropDownList ID="ddlDefaultClass" runat="server" Width="200px">
                    <asp:ListItem Text="--请选择--" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        
        
        <tr>
            <td colspan="2" align="center" style="height: 40px">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <input id="hidInfo" runat="server" type="hidden" />
    </form>
</body>
</html>