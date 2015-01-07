<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeGroup.aspx.cs" Inherits="WebManage.Web.SysManager.CodeGroup" %>


<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��֯�����б�</title>

    <script type="text/javascript">
        function toolbar_Click(toolbarItem)
        {
            var width = 750;
            var height = 220;
            var closeid = "CodeGroupEditLY";
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
                   toolbarItem.needPostBack = confirm('���棺ϵͳ������ɾ����������ϵͳ������ȷ��Ҫɾ���ü�¼��');
                   break;
               case "Add":                   
                    showModalWindow(closeid, "���",width, height, "../../web/SysManager/CodeGroupEdit.aspx?Operation=1");
                    toolbarItem.needPostBack = false;                  
                    break;
               case "Update":
                    var selectCount = IsCheckedSingleTreeview();                   
                    if(selectCount == 1)
                    {
                        return;
                    }
                    else if(selectCount == 0) 
                    {
                        alert("��ѡ���޸ĵļ�¼");
                        toolbarItem.needPostBack = false;
                    }
                    else 
                    {
                        alert("�޸�ֻ��ѡ��һ����¼��");
                        toolbarItem.needPostBack = false;
                    }                    
                    break; 
            }         
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; background: url(../../Images/PageTitle_back.gif) repeat-x;
            top: 0px; left: 0px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table cellpadding="4" cellspacing="4">
                <tr>
                    <td style="height: 37px">
                        <ND:NDToolbar ID="toolbar" runat="server" OnMenuItemClick="toolbar_MenuItemClick">
                            <Items>
                                <asp:MenuItem Text="���" Value="Add"></asp:MenuItem>
                                <asp:MenuItem Text="�޸�" Value="Update"></asp:MenuItem>
                                <asp:MenuItem Text="ɾ��" Value="Delete"></asp:MenuItem>                               
                            </Items>
                        </ND:NDToolbar>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <ignav:UltraWebTree ID="tvOU" runat="server" Cursor="Hand" AutoPostBack="True">
                                </ignav:UltraWebTree>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnRefresh" runat="server" Text="ˢ��" OnClick="btnRefresh_Click" Style="display: none;" />
    </form>
</body>
</html>
