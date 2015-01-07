<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyClassManage.aspx.cs" Inherits="WebManage.Web.Survey.SurveyClassManage" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>问卷类型管理</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 400;
            var height = 300;
            var closeid = "SurveyClassEdit";

            switch (toolbarItem.value) {
                case "Add":
                    showModalWindow(closeid, "添加", width, height, "../../Web/Survey/SurveyClassEdit.aspx?Operation=1");
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
                        showModalWindow(closeid, "修改", width, height, "../../Web/Survey/SurveyClassEdit.aspx?Operation=2&ID=" + id + "");
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
                        alert("请选择删除的记录！");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    toolbarItem.needPostBack = confirm('确认要删除选择的记录吗？');
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
                        <asp:MenuItem Text="删除" Value="Delete"></asp:MenuItem>
                        
                    </Items>
                </ND:NDToolbar>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr style="height: 80%;">
            <td>
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="CID" AllowSorting="True" OnRowDataBound="grid_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                            <HeaderStyle Width="25px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" dataID='<%# Eval("CID") %>' onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');">
                                </asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CID" HeaderText="类型编码">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SurveyClassName" HeaderText="类型名称">
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sort" HeaderText="排序">
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DefaultClassName" HeaderText="是否默认类型">
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                            <HeaderStyle Width="30%" />
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