﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartnerManager.aspx.cs" Inherits="WebManage.Web.SysManager.PartnerManager" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合作伙伴维护</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 500;
            var height = 400;
            var closeid = "PartnerEdit";

            switch (toolbarItem.value) {
                case "Add":
                    showModalWindow(closeid, "添加", width, height, "../../Web/SysManager/PartnerEdit.aspx?Operation=1");
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
                        showModalWindow(closeid, "修改", width, height, "../../Web/SysManager/PartnerEdit.aspx?Operation=2&ID=" + id + "");
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
                <ND:NDToolbar ID="toolbar" runat="server" >
                    <Items>
                        <asp:MenuItem Text="添加" Value="Add"></asp:MenuItem>
                        <asp:MenuItem Text="修改" Value="Update"></asp:MenuItem>                        
                    </Items>
                </ND:NDToolbar>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                           合作伙伴姓名：
                        </td>
                        <td >
                            <asp:TextBox ID="txtName" runat="server" Width="120px" MaxLength="50"></asp:TextBox>                            
                        </td>                        
                        <td align="right">
                            状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="120px">
                                <asp:ListItem Value="-1" Selected="True">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1" >发布</asp:ListItem>
                                <asp:ListItem Value="0">下架</asp:ListItem>
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
                    Width="100%" DataKeyNames="ID,Status" AllowSorting="True" 
                    onrowcommand="grid_RowCommand">
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
                        <asp:BoundField DataField="Name" HeaderText="合作伙伴名称">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="URL" HeaderText="超连接">
                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                            <HeaderStyle Width="40%" />
                        </asp:BoundField>
                        
                        <asp:ButtonField CommandName="Status" Text="状态" HeaderText="状态">
                            </asp:ButtonField>  
                            
                        <%--<asp:BoundField DataField="StatusName" HeaderText="状态">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="sort" HeaderText="排序">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="View" Text="图片预览" HeaderText="图片预览">
                            </asp:ButtonField>         
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