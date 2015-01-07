<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="WebManage.Web.SystemManager.UserManager" %>


<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员管理</title>

    <script type="text/javascript">
        function toolbar_Click(toolbarItem)
        {
            var width = 600;
            var height = 460;
            var closeid = "UserDetailLY";
            var OUID = document.getElementById("hidOUID").value;
            switch (toolbarItem.value)
            {	          
                case "Delete":
                   if (IsChecked() ==  false)
                   {
                       alert("请选择删除的记录！");
                       toolbarItem.needPostBack = false;
                       return;
                   } 
                   toolbarItem.needPostBack = confirm('确认要删除选择的记录吗？');
                   break;
               case "Add":                   
                    showModalWindow(closeid, "添加",width, height, "../../web/SysManager/UserDetail.aspx?Operation=1&OUID=" + OUID);
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
                        showModalWindow(closeid, "修改",width, height, "../../web/SysManager/UserDetail.aspx?Operation=2&ID=" + id + "&OUID=" + OUID);
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
                case "Moveup":
                    var selectCount = IsCheckedSingle();
                    if (selectCount == 1) {
                        return;
                    }
                    else if (selectCount == 0) {
                        alert("请选择上移记录！");
                        toolbarItem.needPostBack = false;
                    }
                    else {
                        alert("只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Movedown":
                    var selectCount = IsCheckedSingle();
                    if (selectCount == 1) {
                        return;
                    }
                    else if (selectCount == 0) {
                        alert("请选择下移记录！");
                        toolbarItem.needPostBack = false;
                    }
                    else {
                        alert("只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Setting":
                    var selectCount = IsCheckedSingle();
                    if (selectCount == 1) {
                        return;
                    }
                    else if (selectCount == 0) {
                        alert("请选择记录！");
                        toolbarItem.needPostBack = false;
                    }
                    else {
                        alert("只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Authorization":
                    var selectCount = IsCheckedSingle();
                    if(selectCount == 1)
                    {                        
                        return;
                    }
                    else if(selectCount == 0) 
                    {
                        alert("请选择记录！");
                        toolbarItem.needPostBack = false;
                    }
                    else 
                    {
                        alert("只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }                    
                    break;
            }
        }
    </script>

</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
        <table cellpadding="4" cellspacing="4" style="width: 100%;height: 80%">
            <tr>
                <td style="height: 4%">
                    <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                        <Items>
                            <asp:MenuItem Text="添加" Value="Add"></asp:MenuItem>
                            <asp:MenuItem Text="修改" Value="Update"></asp:MenuItem>
                            <asp:MenuItem Text="删除" Value="Delete"></asp:MenuItem>
                            <asp:MenuItem Text="锁定" Value="Setting"></asp:MenuItem>
                            <asp:MenuItem Text="解锁" Value="Authorization"></asp:MenuItem>
                            <asp:MenuItem Text="上移" Value="Moveup"></asp:MenuItem>
                            <asp:MenuItem Text="下移" Value="Movedown"></asp:MenuItem>  
                        </Items>
                    </ND:NDToolbar>
                </td>
            </tr>           
            <tr>
                <td style="height: 70%;" valign="Top">
                    <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                        Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                        Width="100%" DataKeyNames="UserID,OUID">
                        <Columns>
                            <asp:TemplateField>
                                <itemstyle horizontalalign="Center" width="25px" />
                                <headerstyle width="25px" />
                                <headertemplate>
                                      <asp:CheckBox id="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');"  />
                                </headertemplate>
                                <itemtemplate>
                                    <asp:CheckBox id="chkItem" runat="server" dataID='<%# Eval("UserID") %>' onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');"></asp:CheckBox> 
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OUName" HeaderText="部门">
                                <itemstyle width="35%" HorizontalAlign="Left"  />
                                <headerstyle width="35%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="姓名">
                                <itemstyle width="25%" HorizontalAlign="Center"  />
                                <headerstyle width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Account" HeaderText="帐号">
                                <itemstyle width="25%" HorizontalAlign="Center" />
                                <headerstyle width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusName" HeaderText="状态">
                                <itemstyle width="15%" HorizontalAlign="Center"  />
                                <headerstyle width="15%" />
                            </asp:BoundField>
                           </Columns>
                        <RowStyle Height="21px" />
                        <HeaderStyle Height="21px" />
                    </ND:NDGridView>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 6%;">
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
            <input id="hidOUID" type="hidden" runat="server" />
            <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" UseSubmitBehavior="False" /></div>
    </form>
</body>
</html>
