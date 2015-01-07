<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeDetail.aspx.cs" Inherits="WebManage.Web.SysManager.CodeDetail" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function toolbar_Click(toolbarItem)
        {
            switch (toolbarItem.value)
            {	          
                case "Delete":
                   var selectCount = IsCheckedSingleTreeview();
                   if (selectCount == 0)
                   {
                       alert("请选择删除的记录！");
                       toolbarItem.needPostBack = false;
                       return;
                   } 
                   toolbarItem.needPostBack = confirm('确定要删除这些记录吗？');
                   break;
            }
        }
    </script>

</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
        <div style="width: 100%; background: url(../../Images/PageTitle_back.gif) repeat-x;
            top: 0px; left: 0px;">
            <br />
            <fieldset style="display: block; width: 95%; top: 10px; border: solid 1px #253E28"
                align="center">
                <legend>父代码信息</legend>
                <table cellpadding="2" cellspacing="2" align="center">
                    <tr>
                        <td style="width: 40px;">
                            名称：
                        </td>
                        <td style="width: 160px;">
                            <asp:TextBox ID="txtCodeName" runat="server" Width="99%" MaxLength="40" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width:280px;"></td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="display: block; width: 95%; top: 10px; border: solid 1px #253E28"
                align="center">
                <legend>子代码维护</legend>
                <table cellpadding="2" cellspacing="2" align="center">
                    <tr>
                        <td style="width: 40px;">
                            名称：
                        </td>
                        <td style="width: 160px;">
                            <asp:TextBox ID="txtCodeDetailName" runat="server" Width="99%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td style="width: 20px; color: Red">
                            *
                        </td>
                        <td style="width: 30px;">
                            值：
                        </td>
                        <td style="width: 100px;">
                            <asp:TextBox ID="txtCodeDetailValue" runat="server" Width="97%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td style="width: 20px; color: Red">
                            *
                        </td>
                        <td align="center" style="width: 90px">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button70" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注：</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDetailMemo" runat="server" Width="100%" MaxLength="40" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td></td>
                        <td align="center" valign="top">
                            <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="button70" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="display: block; width: 95%; top: 10px; border: solid 1px #253E28"
                align="center">
                <legend>子代码列表</legend>
                <table cellpadding="4" cellspacing="4" style="width: 100%; height: 80%">
                    <tr>
                        <td>
                            <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                                <Items>
                                    <asp:MenuItem Text="批量删除" Value="Delete"></asp:MenuItem>
                                </Items>
                            </ND:NDToolbar>
                        </td>
                    </tr>
                    <tr>
                        <td valign="Top">
                            <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                                Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                                Width="100%" DataKeyNames="CodeDetailID" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <itemstyle horizontalalign="Center" width="25px" />
                                        <headerstyle width="25px" />
                                        <headertemplate>
                                              <asp:CheckBox id="chkItemAll" runat="Server"  onclick="SetAllChecked(this, 'chkItem');"  />
                                              </headertemplate>
                                        <itemtemplate>
                                            <asp:CheckBox id="chkItem" runat="server" onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');" dataID='<%# Eval("CodeDetailID") %>'></asp:CheckBox> 
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CodeDetailName" HeaderText="子代码名称">
                                        <itemstyle width="45%" horizontalalign="Left" />
                                        <headerstyle width="45%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodeDetailValue" HeaderText="子代码值">
                                        <itemstyle width="32%" horizontalalign="Left" />
                                        <headerstyle width="32%" />
                                    </asp:BoundField>
                                    <asp:ButtonField Text="修改" CommandName="Modify">
                                        <itemstyle width="10%" horizontalalign="Center" verticalalign="Middle" />
                                        <headerstyle width="10%" />
                                    </asp:ButtonField>
                                    <asp:ButtonField Text="删除" CommandName="Del">
                                        <itemstyle width="10%" horizontalalign="Center" verticalalign="Middle" />
                                        <headerstyle width="10%" />
                                    </asp:ButtonField>
                                </Columns>
                                <RowStyle Height="21px" />
                                <HeaderStyle Height="21px" />
                            </ND:NDGridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="display: none">
            <input id="hidInfo" type="hidden" runat="server" />
        </div>
    </form>
</body>
</html>
