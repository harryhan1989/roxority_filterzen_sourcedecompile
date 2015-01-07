<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebManage.Web.Gifts.Edit" %>


<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> 礼品维护</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="3" cellspacing="3" align="center" style="margin-top: 25px;">
            <tr>
                <td>
                    礼品名称：
                </td>
                <td>
                    <asp:TextBox ID="txtGiftName" runat="server" MaxLength="200" Width="360px"></asp:TextBox>
                </td>
                <td>
                    <span style="color: #ff0000">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    所需兑换积分：
                </td>
                <td>
                    <asp:TextBox ID="txtNeedPoints" runat="server" MaxLength="200" Width="360px"></asp:TextBox>
                </td>
                <td>
                    <span style="color: #ff0000">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    剩余礼品数量：
                </td>
                <td>
                    <asp:TextBox ID="txtRemainAmount" runat="server" MaxLength="200" Width="360px"></asp:TextBox>
                </td>
                <td>
                    <span style="color: #ff0000">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    礼品图片：
                </td>
                <td>
                    <asp:FileUpload ID="fuPicture" runat="server" Width="364px" />
                </td>
                <td id="tdSpan1" runat="server">
                    <span style="color: #ff0000">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    礼品描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="200" Width="360px" Height="280px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <span style="color: #ff0000">*</span>
                </td>
            </tr>
            
            <tr>
                <td colspan="3" align="center" valign="bottom" style="height: 40px">                    
                    <asp:Button ID="btnUpdate" runat="server" Text="保存" CssClass="button70" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <input id="hidInfo" runat="server" type="hidden" />
    </form>
</body>
</html>