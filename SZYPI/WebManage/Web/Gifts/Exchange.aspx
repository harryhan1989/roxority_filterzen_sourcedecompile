<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exchange.aspx.cs" Inherits="WebManage.Web.Gifts.Exchange" %>


<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>礼品兑换</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
        function toolbar_Click(toolbarItem)
        {
            switch (toolbarItem.value)
            {               
              case "Delete":
                   if (IsChecked() ==  false)
                   {
                       alert("请选择要放弃兑换的记录！");
                       toolbarItem.needPostBack = false;
                       return;
                   } 
                   toolbarItem.needPostBack = confirm('确认要放弃该次礼品兑换申请么？');
                   break;
            }
        }
        
        function ConfirmExchange()
        {
            if(confirm('确认要进行此次礼品兑换么？') == true)
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
    </script>
</head>
<body style="background: #FFF; height:100%;">
    <form id="form1" runat="server">
        <table cellpadding="3" cellspacing="3" style="width: 100%; height:80%;">
            <tr>
                <td>
                    <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                        <Items>
                            <asp:MenuItem Text="放弃兑换" Value="Delete"></asp:MenuItem>
                        </Items>
                    </ND:NDToolbar>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                会员账号：
                            </td>
                            <td>
                                <asp:TextBox ID="txtHuiYuanAccount" runat="server" Width="80px" ></asp:TextBox>
                            </td>
                            <td>
                                会员名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtHuiYuanName" runat="server" Width="80px" ></asp:TextBox>
                            </td>
                            <td>
                                礼品名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtGiftName" runat="server" Width="80px" ></asp:TextBox>
                            </td>                           
                            <td>
                                礼品状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" >
                                    <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="未兑换"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="已兑换"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="已放弃"></asp:ListItem>
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
                            <asp:BoundField DataField="HuiYuanAccount" HeaderText="会员账号">
                                <itemstyle width="10%" horizontalalign="Left" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HuiYuanName" HeaderText="会员名称">
                                <itemstyle width="10%" horizontalalign="Left" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RemainPoint" HeaderText="剩余积分">
                                <itemstyle width="10%" horizontalalign="Right" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GiftName" HeaderText="礼品名称">
                                <itemstyle width="20%" horizontalalign="Left" />
                                <headerstyle width="20%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="NeedPoint" HeaderText="所需积分">
                                <itemstyle width="10%" horizontalalign="Right" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApplyTime" HeaderText="申请时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                                <itemstyle width="10%" horizontalalign="Center" />
                                <headerstyle width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ExchangeTime" HeaderText="兑换时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                                <itemstyle width="10%" horizontalalign="Center" />
                                <headerstyle width="10%" />
                            </asp:BoundField>                           
                            <asp:ButtonField CommandName="Status" Text="同意兑换" HeaderText="操作">
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