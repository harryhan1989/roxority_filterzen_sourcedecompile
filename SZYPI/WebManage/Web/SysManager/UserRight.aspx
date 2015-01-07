<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRight.aspx.cs" Inherits="WebManage.Web.SysManager.UserRight" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员权限</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; background: url(../../Images/PageTitle_back.gif) repeat-x;
        top: 0px; left: 0px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="2" cellspacing="2" style="margin-left: 20px; margin-top: 10px">
                    <tr>
                        <td style="width: 70px;">
                            部门名称：
                        </td>
                        <td style="width: 400px;">
                            <asp:DropDownList ID="drpOUList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpOUList_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="drpChildList" Visible="false" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="drpChildList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            人员姓名：
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPersonList" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="drpPersonList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 85px">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 133px;" valign="top" colspan="3">
                            <ignav:UltraWebTree ID="tvRight" runat="server" Cursor="hand" AutoPostBack="True"
                                OnNodeChecked="tvRight_NodeChecked">
                            </ignav:UltraWebTree>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <input id="hidInfo" type="hidden" runat="server" />
    </form>
</body>
</html>
