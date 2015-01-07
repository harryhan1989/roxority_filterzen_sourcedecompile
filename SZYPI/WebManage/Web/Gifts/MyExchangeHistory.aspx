<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyExchangeHistory.aspx.cs" Inherits="WebManage.Web.Gifts.MyExchangeHistory" %>


<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="botton1" TagName="utlBotton" Src="~/Controls/ButtonPageControl.ascx"  %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>我的兑换记录</title>
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
  <!--页眉-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
   <!-- 问卷列表 -->
    <div class="surveytable">
      <div class="wddhjl_top" runat="server" id="divTop"></div>
      
        <!--数据控件-->
       <anthem:Repeater  ID="rptExchangeList" runat="server" EnableCallBack="true" 
            EnabledDuringCallBack="true"  AutoUpdateAfterCallBack="false" 
            TextDuringCallBack="数据加载中..." onprerender="rptSurveyMain_PreRender"  >       
       <HeaderTemplate>
       <div class="left_mid">
       <table class="gridview" style="width: 820px; border-collapse: collapse;" border="0" cellSpacing="0" cellPadding="0">
       <tbody>
        <tr class="gridview-header">
            <th align="center" vAlign="middle" style="width: 240px;" scope="col">
                申请兑换日期
            </th>
            <th align="center" vAlign="middle"  style="width: 210px;" scope="col">
                礼品名称
            </th>
            <th align="center" vAlign="middle"  style="width: 120px;" scope="col">
                所用积分
            </th>
            <th align="center" vAlign="middle"  style="width: 150px;" scope="col">
                状态
            </th>
            <th align="center" vAlign="middle"  style="width: 100px;" scope="col">
                操作
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
                    <%# Eval("StatusName").ToString() == "未兑换" ? "<a href='#' onclick='ExchangeCancel(" + Eval("ID") + ")'>取消</a>" : "<a href='ExchangeApply.aspx?GiftGuid=" + Eval("GiftID") + "'>查看</a>"%>
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
                    <%# Eval("StatusName").ToString() == "未兑换" ? "<a href='#' onclick='ExchangeCancel(" + Eval("ID") + ");'>取消</a>" : "<a href='ExchangeApply.aspx?GiftGuid=" + Eval("GiftID") + "'>查看</a>"%>
                </td>
             </tr>
        </AlternatingItemTemplate>   
        <FooterTemplate>
        </tbody>
        </table>
        </div>
        </FooterTemplate>     
        </anthem:Repeater>
        
        <!--分页-->
    <div id="gridPageCtl" style="width: 100%">
        <anthem:Panel ID="PaginationDiv" runat="server">
            <div class="repeaterPaginator">
                <table border="0" cellpadding="0" cellspacing="0" align="right" valign="bottom">
                    <tr style="height: 40px">
                        <td style="" valign="bottom" align="center">
                            总共&nbsp;
                            <anthem:Label ID="TableSourceCount" runat="server" Text="0" ForeColor="#C91D1D" Font-Bold="true">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </anthem:Label>&nbsp;&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridFirstPage" Enabled="false" runat="server" ImageAlign="Bottom"
                                ImageUrl="~/CSS/image/Index/home.jpg" ToolTip="首页" CommandName="0"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridBefore" Enabled="false" runat="server" ImageAlign="Bottom"
                                ImageUrl="~/CSS/image/Index/up.jpg" CommandName="1" ToolTip="上一页"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridNext" runat="server" ImageAlign="Bottom" ImageUrl="~/CSS/image/Index/next.jpg"
                                CommandName="2" ToolTip="下一页"></anthem:ImageButton>
                        </td>
                        <td style="width: 4px">
                        </td>
                        <td valign="bottom">
                            <anthem:ImageButton ID="GridEnd" runat="server" ImageAlign="Bottom" ImageUrl="~/CSS/image/Index/end.jpg"
                                CommandName="3" ToolTip="末页"></anthem:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom" align="center">
                            跳转至
                            <anthem:DropDownList ID="GridJump" runat="server" ForeColor="#C91D1D" Font-Bold="true">
                            </anthem:DropDownList>
                            页&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="bottom">
                            当前页显示
                            <anthem:DropDownList ID="DdlgridSourceCount" runat="server" ForeColor="#C91D1D" Font-Bold="true">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>60</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                            </anthem:DropDownList>
                            条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
  <!--页脚-->
  <botton1:utlBotton id="utlBotton" runat="server"></botton1:utlBotton>
</div>
</form>
</body>
</html>

<script type="text/javascript">
//翻页
    function FirstPage()//首页
    {
        return Anthem_InvokePageMethod('FirstPage_Ctl', [''], null);
    }


    function BeforePage()//上一页
    {
        return Anthem_InvokePageMethod('BeforePage_Ctl', [''], null);
    }

    function NextPage()//下一页
    {

        return Anthem_InvokePageMethod('NextPage_Ctl', [''], null);
    }

    function EndPage()//末页
    {
        return Anthem_InvokePageMethod('EndPage_Ctl', [''], null);
    }

    function JumpPage(ddlID)//跳转页
    {
        return Anthem_InvokePageMethod('JumpPage_Ctl', [document.getElementById(ddlID).value], null);
    }

    function PageSizePage(ddlID)//当前页记录
    {
        Anthem_InvokePageMethod('PageSizePage_Ctl', [document.getElementById(ddlID).value], null);

    }
    
    //搜索按钮点击事件
    function SearchClick()
    {
        var keyCode = document.getElementById("utlTop1$txtSearchCondition").value;
        
        window.location.href="../../Web/Survey/AllSurveysPage.aspx?KeyCode=" + keyCode;  
    }
    
    //取消兑换申请
    function ExchangeCancel(id)
    {
        if(!confirm("确认要取消本次兑换申请么？"))
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