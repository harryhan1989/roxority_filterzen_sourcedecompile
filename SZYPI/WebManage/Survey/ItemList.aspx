<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_ItemList, Web_Survey" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
<base target="_self" />
<script language="javascript" type="text/javascript">
var intItemType = 0;
var blnActioned = "False";
var intPageNo = 0;

<%=sClientJs %>

function setParentWin(){
    
    if(window.parent.blnActioned1=="True"){
        blnActioned = "True";
    }

    if(intItemType>0){
        window.parent.document.forms.namedItem("form1").reset();
        window.parent.document.getElementById("flag").value = intItemType;
    }
    window.parent.blnActioned = blnActioned;
    
	if(intPageNo<=0){
		intPageNo = 1;
	}
	
    window.parent.document.getElementById("PageNo").selectedIndex = intPageNo-1;
	window.parent.createdNum++;
	
    try{
        window.parent.document.getElementById("ItemName").focus();
    }
    catch(e){
    
    }
	window.parent.checkItemNum();
}


</script>
</head>

<body style="background-color:#FFFFFF" onload="setParentWin()" >
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  PageSize="20"
            AutoGenerateColumns="False" DataKeyNames="IID,ItemName,ItemType,PageNo"  OnRowDataBound="GridView1_RowDataBound" CssClass="BlackFont" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" HorizontalAlign="Center">
            <Columns>
            <asp:TemplateField HeaderText="序">
                <ItemTemplate>
                     <%#Container.DataItemIndex+1%>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="ItemName" HeaderText="题目名" SortExpression="ItemName" />
                <asp:BoundField DataField="ItemType" HeaderText="题型" SortExpression="ItemType" />
                <asp:BoundField DataField="PageNo" HeaderText="页" SortExpression="PageNo" />
            <asp:TemplateField HeaderImageUrl="images/del.gif">
                <ItemTemplate>
                    
                </ItemTemplate>            
            </asp:TemplateField>   
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            
        </asp:GridView>
<%--        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AccessDB %>"
            SelectCommand="SELECT * FROM [ItemTable]"   ProviderName="<%$ ConnectionStrings:AccessDB.ProviderName %>"></asp:SqlDataSource>--%>
    
    </div>
    </form>
</body>
</html>
