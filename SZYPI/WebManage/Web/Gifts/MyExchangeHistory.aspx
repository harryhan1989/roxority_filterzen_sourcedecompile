<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyExchangeHistory.aspx.cs" Inherits="WebManage.Web.Gifts.MyExchangeHistory" %>


<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="botton1" TagName="utlBotton" Src="~/Controls/ButtonPageControl.ascx"  %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�ҵĶһ���¼</title>
<link href="../../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Platform/script/Message.js"> </script>
<script type="text/javascript" src="../../Platform/script/common.js"> </script>
<style type="text/css">
.award_fs_user dt
{
   margin-top:15px;
   
   line-height:20px;
}

.gridview
{
	border-top: 0px solid #C0D1E3;
}

.gridview-header0
{
	display: block;
	font-weight: bold;
	text-decoration: none;
	color: #000;
	width: 100%;
	height: 100%;
	padding: 0px;
	margin: 0px;
}
.gridview-header th
{
	text-align: center;
	font-family: "font-family:Verdana, Arial, Helvetica, sans-serif";
	font-size: 12px;
	border-top: 0px;
	border-right: 1px solid #C0D1E3;
	border-bottom: 1px solid #C0D1E3;
	border-left: 1px solid #C0D1E3;
	border-top: 1px solid #C0D1E3;
	background: url(../../css/image/index/bgbb.jpg) repeat-x;
	height: 27px;
	padding-left: 10px;	
}

.gridview-header th a:link, .gridview-header th a:visited
{
	padding-top: 7px;
	display: block;
	font-weight: bold;
	text-decoration: none;
	color: #000;
	width: 100%;
	height: 100%;
}
.gridview-header th a:hover
{
	display: block;
	background: url(images/engine/girdview-title-bg-hover.gif) repeat-x;
	width: 100%;
	height: 100%;
}
.gridviewRow
{
	background: #FFFFFF;
	color: #474747;
}
.gridviewRow td
{
	padding-left: 5px;
	height: 28px; /*text-align: left;*/
	border: 1px solid #BCCEE2;
}
.gridviewRowSelected td
{
	background: #FFCCFF;
}

.alternatingrow
{
	background-color: #F7F5F6;
	color: #474747;
}
.alternatingrow td
{
	padding-left: 5px; /*text-align: left;*/
	height: 28px;
	border: 1px solid #BCCEE2;
}
.alternatingrow td a:link
{
	color: blue;
	text-decoration: underline;
}

.alternatingrow td a:visited
{
	color: purple;
	text-decoration: underline;
}

.alternatingrow td a:hover
{
	color: red;
	text-decoration: underline;
}

.alternatingrow td a:active
{
	color: gray;
	text-decoration: underline;
}

.gridviewRow td a:link
{
	color: blue;
	text-decoration: underline;
}

.gridviewRow td a:visited
{
	color: purple;
	text-decoration: underline;
}

.gridviewRow td a:hover
{
	color: red;
	text-decoration: underline;
}

.gridviewRow td a:active
{
	color: gray;
	text-decoration: underline;
}
</style>
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
      <div class="wddhjl_top" runat="server" id="divTop"></div>
      
        <!--���ݿؼ�-->
       <anthem:Repeater  ID="rptExchangeList" runat="server" EnableCallBack="true" 
            EnabledDuringCallBack="true"  AutoUpdateAfterCallBack="false" 
            TextDuringCallBack="���ݼ�����..." onprerender="rptSurveyMain_PreRender"  >       
       <HeaderTemplate>
       <div class="left_mid">
       <table class="gridview" style="width: 820px; border-collapse: collapse;" border="0" cellSpacing="0" cellPadding="0">
       <tbody>
        <tr class="gridview-header">
            <th align="center" vAlign="middle" style="width: 240px;" scope="col">
                ����һ�����
            </th>
            <th align="center" vAlign="middle"  style="width: 210px;" scope="col">
                ��Ʒ����
            </th>
            <th align="center" vAlign="middle"  style="width: 120px;" scope="col">
                ���û���
            </th>
            <th align="center" vAlign="middle"  style="width: 150px;" scope="col">
                ״̬
            </th>
            <th align="center" vAlign="middle"  style="width: 100px;" scope="col">
                ����
            </th>
        </tr>
       </HeaderTemplate>
       
        <ItemTemplate>
            <tr class="gridviewRow">
                <td align="center" vAlign="middle" style="width: 240px;">    
                    <%#Eval("ApplyTime")%>
                </td>
                <td align="center" vAlign="middle" style="width: 210px;">
                    <%#Eval("GiftName") %>
                </td>
                <td align="center" vAlign="middle" style="width: 120px;">
                    <%#Eval("UsedPoint") %>
                </td>
                <td align="center" vAlign="middle" style="width: 150px;">
                    <%#Eval("StatusName")%>
                </td>
                <td align="center" vAlign="middle" style="width: 100px;">
                    <%# Eval("StatusName").ToString() == "δ�һ�" ? "<a href='#' onclick='ExchangeCancel(" + Eval("ID") + ")'>ȡ��</a>" : "<a href='ExchangeApply.aspx?GiftGuid=" + Eval("GiftID") + "'>�鿴</a>"%>
                </td>
             </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="alternatingrow">
                <td align="center" vAlign="middle" style="width: 240px;">    
                    <%#Eval("ApplyTime")%>
                </td>
                <td align="center" vAlign="middle" style="width: 210px;">
                    <%#Eval("GiftName") %>
                </td>
                <td align="center" vAlign="middle" style="width: 120px;">
                    <%#Eval("UsedPoint") %>
                </td>
                <td align="center" vAlign="middle" style="width: 150px;">
                    <%#Eval("StatusName")%>
                </td>
                <td align="center" vAlign="middle" style="width: 100px;">
                    <%# Eval("StatusName").ToString() == "δ�һ�" ? "<a href='#' onclick='ExchangeCancel(" + Eval("ID") + ");'>ȡ��</a>" : "<a href='ExchangeApply.aspx?GiftGuid=" + Eval("GiftID") + "'>�鿴</a>"%>
                </td>
             </tr>
        </AlternatingItemTemplate>   
        <FooterTemplate>
        </tbody>
        </table>
        </div>
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
        
        window.location.href="../../Web/Survey/AllSurveysPage.aspx?KeyCode=" + keyCode;  
    }
    
    //ȡ���һ�����
    function ExchangeCancel(id)
    {
        if(!confirm("ȷ��Ҫȡ�����ζһ�����ô��"))
        {
            return false;
        }
        else
        {
            return Anthem_InvokePageMethod('ExchangeCancel', [id], null);
            return true;
        }
    }
</script>