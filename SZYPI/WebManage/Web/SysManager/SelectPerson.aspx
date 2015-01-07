<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPerson.aspx.cs" Inherits="WebManage.Web.SystemManager.SelectPerson" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link href="../../CSS/Form.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:10px;">
    <form id="form1" runat="server">
        <table cellpadding="4" cellspacing="4" style="width: 100%; height: 80%;">
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="width: 45px;">
                                部门：
                            </td>
                            <td style="width: 180px;">
                                <asp:DropDownList ID="drpOU" runat="server" Width="150px" />
                            </td>
                            <td style="width: 45px;">
                                姓名：
                            </td>
                            <td style="width: 130px;">
                                <asp:TextBox ID="txtUserName" runat="server" Width="100px" />
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" Text="查询" Width="88px" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 80%;padding-top:10px;">
                    <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                        Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                        Width="100%" DataKeyNames="UserID,UserName" OnRowCommand="grid_RowCommand">
                        <Columns>
                           <asp:BoundField DataField="OUName" HeaderText="部门">
                                <itemstyle width="35%" horizontalalign="Left" />
                                <headerstyle width="35%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="姓名">
                                <itemstyle width="20%" horizontalalign="Left" />
                                <headerstyle width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Duty" HeaderText="职位">
                                <itemstyle width="20%" horizontalalign="Left" />
                                <headerstyle width="20%" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="Status" HeaderText="状态">
                                <itemstyle width="15%" horizontalalign="Left" />
                                <headerstyle width="15%" />
                            </asp:BoundField>--%>
                            <asp:ButtonField CommandName="Select" Text="选择">
                                <itemstyle horizontalalign="Center" width="10%" />
                                <headerstyle horizontalalign="Center" width="10%" />
                            </asp:ButtonField>
                        </Columns>
                        <RowStyle Height="21px" />
                        <HeaderStyle Height="21px" />
                    </ND:NDGridView>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 30px; vertical-align: bottom;">
                    <ND:NDGridViewPager ID="viewpage1" runat="server" OnPageChanged="pager_PageChanged"
                        Width="100%" Height="25px" ShowCustomInfoSection="Left" CustomInfoSectionWidth="80%"
                        FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" PageSize="10"
                        PagingButtonSpacing="8px" NumericButtonCount="8" NumericButtonTextFormatString="[{0}]"
                        AlwaysShow="True" ShowInputBox="Always" ShowSelectBox="Always" InputBoxStyle="WIDTH: 60px"
                        SubmitButtonText="Go !">
                    </ND:NDGridViewPager>
                </td>
            </tr>
        </table>
        <div style="display: none">
            <input id="hidInfo" type="hidden" runat="server" />
            <asp:Button ID="btnRefresh" runat="server" Text="Refurbish" OnClick="btnRefresh_Click" UseSubmitBehavior="False" />
        </div>
    </form>
</body>
</html>
