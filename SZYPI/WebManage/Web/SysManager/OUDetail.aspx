<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OUDetail.aspx.cs" Inherits="WebManage.Web.SystemManager.OUDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门设置</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 15px;">
    </div>
    <fieldset style="display: marker; width: 526px; border: solid 1px #253E28; height: 325px;"
        align="center">
        <legend>部门设置</legend>
        <table cellpadding="2" cellspacing="2" align="center" style="margin-top: 10px; height: 266px;">
            <tr>
                <td>
                    父级名称：
                </td>
                <td>
                    <asp:DropDownList ID="drpOUList" runat="server">
                    </asp:DropDownList>
                    <%-- <asp:TextBox ID="txtParentName" runat="server" Width="220px" Enabled="false"></asp:TextBox>--%>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    部门名称：
                </td>
                <td>
                    <asp:TextBox ID="txtOUName" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                </td>
                <td>
                    <span style="color: Red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    部门描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescriptipn" runat="server" Width="300px" Height="128px" MaxLength="10000"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" style="height: 50px">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />
                    &nbsp;<asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <input id="hidInfo" type="hidden" runat="server" /><br />
    </form>
</body>
</html>
