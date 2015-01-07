<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_CreateSurvey, Web_Survey" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>创建问卷</title>


    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body onload="init()" class="mainBG">
    <script language="javascript" type="text/javascript">
    <%=sClientJs %>
    function init(){
        if(blnCreated==false){
            document.getElementById("SurveyName").focus();
        }
        else{
            top.location.href = "EditSurvey.aspx?SID="+SID;
        }
    }
	
	
    function docreate(){
		if(document.getElementById("SurveyName").value!=""){
		document.getElementById("Button1").value = "正在创建问卷...";	
		document.getElementById("Button1").disabled = "disabled";
		}
	}
    </script>
    <form id="form1" runat="server">
    <div>
        <table width="100%"   style="height:350px ">
            <tr>
                <td valign="top">
                    <table width="100%" height="" border="0" cellpadding="20" cellspacing="0">
                     
                        <tr>
                            <td valign="top" >
                                <strong class="BlackFont">问卷名</strong><br />
                                <asp:TextBox ID="SurveyName" runat="server" MaxLength="50" Columns="50" CausesValidation="True" Font-Names="宋体" Font-Size="18px" Height="20px" Width="400px" Font-Bold="True"></asp:TextBox><input type="button" value=">>" title="扩展选项"  onclick="document.getElementById('adv').style.display='block';this.style.display='none'"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SurveyName"
                                    ErrorMessage="请输入问卷名" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <div id="adv" style="display:none">
                                <br />

                                <strong class="BlackFont">问卷类型</strong><br />
                                <asp:DropDownList ID="SurveyClass" runat="server">                                    
                                </asp:DropDownList><br />
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
 <strong class="BlackFont">问卷说明</strong>
                                <br />
                                <asp:TextBox ID="SurveyContent" runat="server" Height="92px" TextMode="MultiLine"
                                    Width="389px"></asp:TextBox>
                             
							</div>
                             <br />
                                <asp:HiddenField ID="StyleLibID" runat="server" Value="1" />
<br />


                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="下一步" CssClass="SaveBT"  Width="150px" Height="30px" Font-Bold="true" />                          
                          </td>
                        </tr>
                        <tr>
                            <td align="right" >                          </td>
                        </tr>
                  </table>
                </td>
            </tr>
      </table>
    
    </div>
    </form>
</body>
</html>
