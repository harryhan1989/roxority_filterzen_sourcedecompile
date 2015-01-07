<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_volumeAddPage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
<%=sClientJs %>
window.onload = function(){
    if(sMessage=="ok"){
        alert("保存完成");
        window.parent.blnActioned = "True";
        window.parent.closeActionWin();
        
    }
    else{
        window.parent.complateActionWin();
    }
}
</script>
</head>

<body class="BlackFont">
    <form id="form1" runat="server">
    <div style="margin:10px">
        <span>选择增加页数<br />
        </span>
        <asp:ListBox ID="ListBox1" runat="server" Rows="10">
            <asp:ListItem Value="1" Selected="True">1页</asp:ListItem>
            <asp:ListItem Value="2">2页</asp:ListItem>
            <asp:ListItem Value="3">3页</asp:ListItem>
            <asp:ListItem Value="4">4页</asp:ListItem>
            <asp:ListItem Value="5">5页</asp:ListItem>
            <asp:ListItem Value="6">6页</asp:ListItem>
            <asp:ListItem Value="7">7页</asp:ListItem>
            <asp:ListItem Value="8">8页</asp:ListItem>
            <asp:ListItem Value="9">9页</asp:ListItem>
            <asp:ListItem Value="10">10页</asp:ListItem>
        </asp:ListBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存" /></div>
    </form>
</body>
</html>
