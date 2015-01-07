<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartnerEdit.aspx.cs" Inherits="WebManage.Web.SysManager.PartnerEdit" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合作伙伴维护</title>
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
        <tr>
            <td class="style2">
                合作伙伴：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                超链接：
            </td>
            <td>
                <asp:TextBox ID="txtURL" runat="server" Width="200px" TextMode="MultiLine" Height="100px" MaxLength="50"></asp:TextBox>
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                图片：
            </td>
            <td>
                <asp:FileUpload ID="fuPicture" runat="server" Width="300px" />
                <span style="color: #ff0000;">*</span>
            </td>
        </tr>
        <tr>
            <td class="style2">
                排序：
            </td>
            <td>
                <asp:TextBox ID="txtSort" runat="server"  MaxLength="50" Width="200px"></asp:TextBox>               
            </td>
        </tr>        
          
        <tr>
            <td colspan="2" align="center" style="height: 40px">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" 
                    OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <input id="hidInfo" runat="server" type="hidden" />
    </form>
</body>
</html>