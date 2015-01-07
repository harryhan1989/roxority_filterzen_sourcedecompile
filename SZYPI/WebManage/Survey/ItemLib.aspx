<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_ItemLib, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>题目库</title>

    <script language="javascript" type="text/javascript">

        function copyItem(SID, LIID) {
            document.getElementById("targetWin").src = "CopyItemToSurvey.aspx?SID=" + SID + "&LIID=" + LIID + "&" + escape(new Date());
        }

        function delItem(LIID) {
            if (confirm('删除此条记录?') == true) {
                self.location.href = "ItemLib.aspx?LIID=" + LIID + "&A=Del";
            }
        }
    </script>

    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body class="BlackFont">
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="False"
            AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound"
            DataKeyNames="LIID,Active" BackColor="#FFFFFF" BorderWidth="0px" CellPadding="5">
            <Columns>
                <asp:BoundField DataField="ItemName" ItemStyle-HorizontalAlign="Left" HeaderText="题目名"
                    />
                <asp:BoundField DataField="ItemType" ItemStyle-HorizontalAlign="Center" HeaderText="题目类型"
                     />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除"></asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="复制此题"></asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    </form>
    <iframe id="targetWin" style="width: 100%; height: 330px; display: none"></iframe>
</body>
</html>
