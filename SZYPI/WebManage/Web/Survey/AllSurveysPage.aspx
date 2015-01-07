<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllSurveysPage.aspx.cs" Inherits="WebManage.Web.Survey.AllSurveysPage" %>

<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="botton1" TagName="utlBotton" Src="~/Controls/ButtonPageControl.ascx"  %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>���ߵ���</title>
<link href="../../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Platform/script/Message.js"> </script>
<script type="text/javascript" src="../../Platform/script/common.js"> </script>
</head>
<body>
<form id="Form1" runat="server">
<div id="container">
  <!--ҳü-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
   <!-- �ʾ��б� -->
    <div class="surveytable">
      <div class="wdwj_top" runat="server" id="divTop"></div>
      
        <!--���ݿؼ�-->
       <anthem:Repeater  ID="rptSurveyMain" runat="server" EnableCallBack="true" 
            EnabledDuringCallBack="true"  AutoUpdateAfterCallBack="false" 
            TextDuringCallBack="���ݼ�����..." onprerender="rptSurveyMain_PreRender"  >       
       <HeaderTemplate>
       <table>
       </HeaderTemplate>
        <ItemTemplate>
        <tr>
            <td>
                <div class="left_mid">
                <div class="flagleft"><a href="#" onclick='window.open("../../Survey/userdata/u1/s" + <%# DataBinder.Eval(Container.DataItem,"sid") %> + ".aspx")' > <%# Eval("SurveyName")%></a></div>
                <div class="wj_left"><%# Eval("PageContent")%></div>
                <div class="admin_uer">
                    <%# Eval("UserName")%>������<%# Eval("CreateDate")%>
                    <span class="widthspace0"></span>
                    <span class="flag0"></span>
                    <%--<a href="#">��д����(<%# Eval("AnswerAmount")%>)</a>--%>
                    <span >��д����(<%# Eval("AnswerAmount")%>)</span>
                    <span style="color:Red; font-weight:bold;">���ͻ���<%# Eval("Point")%>��</span>
                    <span style="color:Blue; font-weight:bold;"><%# Eval("HasAnswered")%></span>
                </div>
                
                <hr>
                 </div>
                </td>
             </tr>
        </ItemTemplate>   
        <FooterTemplate>
        </table>
        </FooterTemplate>     
        </anthem:Repeater>
        
        <!--��ҳ-->
    <div id="gridPageCtl" style="width: 100%">
        <anthem:Panel ID="PaginationDiv" runat="server">
            <div class="repeaterPaginator">
                <table border="0" cellpadding="0" cellspacing="0" align="right" valign="bottom">
                    <tr style="height: 40px">
                        <td style="" valign="bottom" align="center">
                            �ܹ�&nbsp;
                            <anthem:Label ID="TableSourceCount" runat="server" Text="0" ForeColor="#C91D1D" Font-Bold="true">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </anthem:Label>&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridFirstPage" Enabled="false" runat="server" ImageAlign="Bottom"
                                ImageUrl="~/CSS/image/Index/home.jpg" ToolTip="��ҳ" CommandName="0"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridBefore" Enabled="false" runat="server" ImageAlign="Bottom"
                                ImageUrl="~/CSS/image/Index/up.jpg" CommandName="1" ToolTip="��һҳ"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridNext" runat="server" ImageAlign="Bottom" ImageUrl="~/CSS/image/Index/next.jpg"
                                CommandName="2" ToolTip="��һҳ"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridEnd" runat="server" ImageAlign="Bottom" ImageUrl="~/CSS/image/Index/end.jpg"
                                CommandName="3" ToolTip="ĩҳ"></anthem:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom" align="center">
                            ��ת��
                            <anthem:DropDownList ID="GridJump" runat="server" ForeColor="#C91D1D" Font-Bold="true">
                            </anthem:DropDownList>
                            ҳ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom">
                            ��ǰҳ��ʾ
                            <anthem:DropDownList ID="DdlgridSourceCount" runat="server" ForeColor="#C91D1D" Font-Bold="true">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>60</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                            </anthem:DropDownList>
                            ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 7px;" colspan="3">
                        </td>
                    </tr>
                </table>
            </div>
        </anthem:Panel>
    </div>
        
        
     
      <div class="lpdh_bottom"></div>
    </div>

  </div>
  <!--ҳ��-->
  <botton1:utlBotton id="utlBotton" runat="server"></botton1:utlBotton>
</div>
</form>
</body>
</html>

<script type="text/javascript">
//��ҳ
    function FirstPage()//��ҳ
    {
        return Anthem_InvokePageMethod('FirstPage_Ctl', [''], null);
    }


    function BeforePage()//��һҳ
    {
        return Anthem_InvokePageMethod('BeforePage_Ctl', [''], null);
    }

    function NextPage()//��һҳ
    {

        return Anthem_InvokePageMethod('NextPage_Ctl', [''], null);
    }

    function EndPage()//ĩҳ
    {
        return Anthem_InvokePageMethod('EndPage_Ctl', [''], null);
    }

    function JumpPage(ddlID)//��תҳ
    {
        return Anthem_InvokePageMethod('JumpPage_Ctl', [document.getElementById(ddlID).value], null);
    }

    function PageSizePage(ddlID)//��ǰҳ��¼
    {
        Anthem_InvokePageMethod('PageSizePage_Ctl', [document.getElementById(ddlID).value], null);

    }

    //������ť����¼�
    function SearchClick()
    {
        var keyCode = document.getElementById("utlTop1$txtSearchCondition").value;
        
        return Anthem_InvokePageMethod('SearchClick', [keyCode], null);        
    }
</script>