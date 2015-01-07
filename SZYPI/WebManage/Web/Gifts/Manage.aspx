<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebManage.Web.Gifts.Manage" %>


<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>礼品管理</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
        function toolbar_Click(toolbarItem)
        {
            var width = 600;
            var height = 500;
            var closeid = "GiftEdit";
            
            switch (toolbarItem.value)
            {
               case "Add":
                    showModalWindow(closeid, "添加",width, height, "../../web/Gifts/Edit.aspx?Operation=1");                 
                    toolbarItem.needPostBack = false;
                    break;
               case "Update":
                    var selectCount = IsCheckedSingle();
                    if(selectCount == 1)
                    {
                        var id = GetCheckedItemID();
                        if (id == null)
                        {
	                       alert("请选择要修改的记录！");
	                       toolbarItem.needPostBack = false;
	                       return;
                        }
                        showModalWindow(closeid, "修改",width, height, "../../web/Gifts/Edit.aspx?Operation=2&ID=" + id);
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    else if(selectCount == 0) 
                    {
                        alert("请选择修改的记录");
                        toolbarItem.needPostBack = false;
                    }
                    else 
                    {
                        alert("修改只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }                    
                    break;              
            }
        }
    </script>
</head>
<body style="background: #FFF; height:100%;">
    <form id="form1" runat="server">
        <table cellpadding="3" cellspacing="3" style="width: 100%; height:80%;">
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
                                礼品名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                            </td>
                            
                            <td>
                                礼品状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" >
                                    <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="开放"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="下架"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                            <td align="right" width="80px">
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
                        Width="100%" DataKeyNames="ID,Status" AllowSorting="True" OnRowCommand="grid_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <itemstyle horizontalalign="Center" width="25px" />
                                <headerstyle width="25px" />
                                <itemtemplate>
                                    <asp:CheckBox id="chkItem" runat="server" dataID='<%# Eval("ID") %>'></asp:CheckBox> 
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GiftName" HeaderText="礼品名称">
                                <itemstyle width="25%" horizontalalign="Left" />
                                <headerstyle width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NeedPoint" HeaderText="所需积分">
                                <itemstyle width="10%" horizontalalign="Right" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RemainAmount" HeaderText="剩余数量">
                                <itemstyle width="10%" horizontalalign="Right" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CreateTime" HeaderText="添加时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                                <itemstyle width="15%" horizontalalign="Center" />
                                <headerstyle width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdateTime" HeaderText="修改时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                                <itemstyle width="20%" horizontalalign="Center" />
                                <headerstyle width="20%" />
                            </asp:BoundField>                            
                            <asp:ButtonField CommandName="Status" Text="下架" HeaderText="状态">
                                <itemstyle width="10%" />
                                <headerstyle width="10%" />
                            </asp:ButtonField>
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
    </form>
</body>
</html>