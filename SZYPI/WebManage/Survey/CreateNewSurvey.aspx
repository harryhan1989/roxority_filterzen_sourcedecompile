<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_CreateNewSurvey, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script language="javascript" type="text/javascript">
var SID = 0;
<%=sClientJs%>
function init(){
	
	if(SID==0){
		//window.parent.openEdit();
		//window.parent.openNewSurvey();
		//document.getElementById("SurveyName").focus();
		window.parent.complateActionWin();
		document.getElementById("SurveyName").focus();
		return;
	}
	else{
		top.location.href = "EditSurvey.aspx?SID="+SID
	}
}

</script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body class="BlackFont" style="background-color:#FFFFFF" onload="init()">

    <form id="form1" runat="server" class="Myform">   
        <table width="100%" border="0" cellpadding="0" cellspacing="5" bgcolor="#FFFFFF"  style="height: 145px; text-align: left">
           
            <tr>
                <td>
                    <table bgcolor="#FFFFFF" style="width:100% ">
                    
                        <tr>
                            <td>
                                <strong>
                                问卷名<br />
                                </strong>
                                <asp:TextBox ID="SurveyName" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SurveyName"
                                    ErrorMessage="请输入问卷名" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="问卷名不能有特殊字符" ControlToValidate="SurveyName" Display="Dynamic" ValidationExpression="^((?![~\!@#%\^&\*\+=\[\]{}\|<>\?,.]).)*$"></asp:RegularExpressionValidator><br />
                                <br />
                                <strong>
                                问卷类型<br />
                                </strong>
                                <asp:DropDownList ID="SurveyClass" runat="server"  >
                                </asp:DropDownList>
                                <br />
<%-- <strong class="BlackFont">问卷语言</strong><br />

                                    <asp:DropDownList ID="Lan" runat="server" Visible="false">
                                        <asp:ListItem Selected="True" Value="1">简体中文</asp:ListItem>                                    
                                    </asp:DropDownList>
                                <br />--%>
                                 <strong class="BlackFont">问卷类别</strong><br />

                                    <asp:DropDownList ID="SurveyType" runat="server" >
                                        <asp:ListItem Selected="True" Value="0">调查问卷</asp:ListItem>    
                                        <asp:ListItem  Value="1">投票问卷</asp:ListItem>     
                                        <asp:ListItem  Value="2">测评问卷</asp:ListItem>                                  
                                    </asp:DropDownList>
                                <br />
                          </td>
                        </tr>
                      
                  </table>
                </td>
            </tr>
            <tr>
                <td style="margin-left:10px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="创建" CssClass="SaveBT" /><span id="Message" runat="server"></span></td>
            </tr>
      </table>
    
   
    </form>
</body>
</html>
