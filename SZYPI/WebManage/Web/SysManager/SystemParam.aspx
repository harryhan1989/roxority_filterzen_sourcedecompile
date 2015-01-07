<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SystemParam.aspx.cs" Inherits="WebManage.Web.SystemManager.SystemParam" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; background: url(../../Images/PageTitle_back.gif) repeat-x;
            top: 0px; left: 0px;">
            <table style="width: 100%;  margin-top:15px;">
                <tr>
                    <td>
                        <span style="color: #ff0000">注意：系统运行参数不能随意修改，在了解其用途的情况下才能修改</span></td>
                </tr>
                <tr>
                    <td>
                        <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                            Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                            Width="100%" DataKeyNames="ParamID" OnRowCancelingEdit="grid_RowCancelingEdit"
                            OnRowEditing="grid_RowEditing" OnRowUpdating="grid_RowUpdating">
                            <Columns>
                                <asp:CommandField ShowEditButton="True">
                                    <headerstyle width="10%" />
                                    <itemstyle width="10%" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ParamName" HeaderText="参数名称">
                                    <itemstyle width="20%" />
                                    <headerstyle width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EnglishName" HeaderText="键" ReadOnly="True">
                                    <itemstyle width="20%" />
                                    <headerstyle width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ParamValue" HeaderText="值">
                                    <itemstyle width="20%" />
                                    <headerstyle width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="备注">
                                    <itemstyle width="30%" />
                                    <headerstyle width="30%" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle Height="21px" />
                            <HeaderStyle Height="21px" />
                        </ND:NDGridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
