<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebManage.Web.HuiYuan.Manage" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员信息维护</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 400;
            var height = 300;
            var closeid = "HuiYuanEdit";

            switch (toolbarItem.value) {
                case "Add":
                    showModalWindow(closeid, "添加", width, height, "../../Web/HuiYuan/Edit.aspx?Operation=1");
                    toolbarItem.needPostBack = false;
                    break;
                case "Update":
                    var selectCount = IsCheckedSingle();
                    if (selectCount == 1) {
                        var id = GetCheckedItemID();
                        if (id == null) {
                            alert("请选择要修改的记录！");
                            toolbarItem.needPostBack = false;
                            return;
                        }
                        showModalWindow(closeid, "修改", width, height, "../../Web/HuiYuan/Edit.aspx?Operation=2&ID=" + id + "");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    else if (selectCount == 0) {
                        alert("请选择修改的记录");
                        toolbarItem.needPostBack = false;
                    }
                    else {
                        alert("修改只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Delete":
                    if (IsChecked() == false) {
                        alert("请选择停用的记录！");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    toolbarItem.needPostBack = confirm('确认要停用选择的记录吗？');
                    break;
                case "Authorization":
                    if (IsChecked() == false) {
                        alert("请选择启用的记录！");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    toolbarItem.needPostBack = confirm('确认要启用选择的记录吗？');
                    break;
            }
        }
    </script>

</head>
<body style="background: #FFF; height: 100%;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="3" cellspacing="3" style="width: 100%; height: 80%;">
        <tr>
            <td>
                <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="添加" Value="Add"></asp:MenuItem>
                        <asp:MenuItem Text="修改" Value="Update"></asp:MenuItem>
                        <asp:MenuItem Text="停用" Value="Delete"></asp:MenuItem>
                        <asp:MenuItem Text="启用" Value="Authorization"></asp:MenuItem>
                    </Items>
                </ND:NDToolbar>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                           会员姓名：
                        </td>
                        <td >
                            <asp:TextBox ID="txtName" runat="server" Width="120px" MaxLength="50"></asp:TextBox>                            
                        </td>
                        <td>
                           会员账号：
                        </td>
                        <td >
                            <asp:TextBox ID="txtAccount" runat="server" Width="120px" MaxLength="50"></asp:TextBox>                            
                        </td>
                        <td align="right">
                            会员状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="drpStatus" runat="server" Width="120px">
                                <asp:ListItem Value="-1" Selected="True">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1" >启用</asp:ListItem>
                                <asp:ListItem Value="0">停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="88px" align="right">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="button70" OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr style="height: 80%;">
            <td>
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="ID" AllowSorting="True" OnRowDataBound="grid_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                            <HeaderStyle Width="25px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" dataID='<%# Eval("ID") %>' onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');">
                                </asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="会员姓名">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LoginAcc" HeaderText="账号">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="邮箱地址">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tel" HeaderText="手机号码">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalPoint" HeaderText="总积分">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RemainPoint" HeaderText="剩余积分">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusName" HeaderText="状态">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreateTime" HeaderText="注册时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>                        
                    </Columns>
                    <RowStyle Height="21px" />
                    <HeaderStyle Height="21px" />
                </ND:NDGridView>
            </td>
        </tr>
        <tr>
            <td align="right">
            
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
        <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Width="0px" />
        <input id="hidInfo" runat="server" type="hidden" />
    </div>
    <input id="Hidden1" runat="server" type="hidden" />
    </form>
</body>
</html>
