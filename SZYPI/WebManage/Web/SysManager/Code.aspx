<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Code.aspx.cs" Inherits="WebManage.Web.SysManager.Code" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ޱ���ҳ</title>
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
                       alert("��ѡ��ɾ���ļ�¼��");
                       toolbarItem.needPostBack = false;
                       return;
                   } 
                   toolbarItem.needPostBack = confirm('ȷ��Ҫɾ����Щ��¼��');
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
                <legend>����ά��</legend>
                <table cellpadding="2" cellspacing="2" align="center">
                    <tr>
                        <td style="width: 60px;">
                            �������ƣ�
                        </td>
                        <td style="width: 160px;">
                            <asp:TextBox ID="txtCodeName" runat="server" Width="99%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td style="width: 20px; color: Red">
                            *
                        </td>
                        <td style="width: 48px;">
                            ����ֵ��
                        </td>
                        <td style="width: 100px;">
                            <asp:TextBox ID="txtCodeValue" runat="server" Width="97%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td style="width: 20px; color: Red">
                            *
                        </td>
                        <td align="center" style="width: 90px">
                            <asp:Button ID="btnSave" runat="server" Text="����" CssClass="button70" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtMemo" runat="server" Width="100%" MaxLength="40" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset style="display: block; width: 95%; top: 10px; border: solid 1px #253E28"
                align="center">
                <legend>�����б�</legend>
                <table cellpadding="4" cellspacing="4" style="width: 100%; height: 80%">
                    <tr>
                        <td>
                            <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                                <Items>
                                    <asp:MenuItem Text="����ɾ��" Value="Delete"></asp:MenuItem>
                                </Items>
                            </ND:NDToolbar>
                        </td>
                    </tr>
                    <tr>
                        <td valign="Top">
                            <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                                Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                                Width="100%" DataKeyNames="CodeID" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <itemstyle horizontalalign="Center" width="25px" />
                                        <headerstyle width="25px" />
                                        <headertemplate>
                                              <asp:CheckBox id="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');"  />
                                        </headertemplate>
                                        <itemtemplate>
                                            <asp:CheckBox id="chkItem" runat="server" onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');" dataID='<%# Eval("CodeID") %>'></asp:CheckBox> 
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CodeName" HeaderText="��������">
                                        <itemstyle width="40%" horizontalalign="Left" />
                                        <headerstyle width="40%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodeValue" HeaderText="����ֵ">
                                        <itemstyle width="22%" horizontalalign="Left" />
                                        <headerstyle width="22%" />
                                    </asp:BoundField>
                                    <asp:ButtonField Text="�Ӵ���ά��" CommandName="Children">
                                        <itemstyle width="15%" horizontalalign="Center" verticalalign="Middle" />
                                        <headerstyle width="15%" />
                                    </asp:ButtonField>
                                    <asp:ButtonField Text="�޸�" CommandName="Modify">
                                        <itemstyle width="10%" horizontalalign="Center" verticalalign="Middle" />
                                        <headerstyle width="10%" />
                                    </asp:ButtonField>
                                    <asp:ButtonField Text="ɾ��" CommandName="Del">
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
            <asp:Button ID="btnRefresh" runat="server" Text="ˢ��" OnClick="btnRefresh_Click" />
        </div>
    </form>
</body>
</html>
