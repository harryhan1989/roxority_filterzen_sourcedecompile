<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="WebManage.Web.HuiYuan.Info" %>
<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="button1" TagName="utlButton" Src="~/Controls/ButtonPageControl.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>在线调查</title>
<link href="../../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Platform/script/Message.js"> </script>
<script type="text/javascript" src="../../Platform/script/common.js"> </script>
</head>
<body>
<form id="Form1" runat="server">
<div id="container">
  <!--页眉-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
    <div class="surveytable">
      <div class="wdzh_top"></div>
      <div class="lpdh_mid" style="text-align:center;">
      
      <table style="text-align:center;">
        <tr style="text-align:center;">
            <td>
                登录账号：
            </td>
            <td>
                <asp:Label ID="lblHuiYuanAccount" runat="server" Width="218px" ></asp:Label>
            </td>
            <td colspan="2" align="left">
                <asp:Label ID="lblPwd" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                用户姓名：
            </td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtHuiYuanName" runat="server" Width="297px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                邮件地址：
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="225px" ></asp:TextBox>
            </td>
            <td>
                手机号码：
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" Width="200px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                会员总积分：
            </td>
            <td>
                <asp:Label ID="lblTotalPoint" runat="server"></asp:Label>
            </td>
            <td>
                剩余积分：
            </td>
            <td>
                <asp:Label ID="lblRemainPoint" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"/>
            </td>
        </tr>
      </table>
      	
        <div class="clearboth"></div>
      </div>
      <div class="lpdh_bottom"></div>
    </div>

  </div>
  <!--页脚-->
  <button1:utlButton id="utlButton" runat="server"></button1:utlButton>
</div>
</form>
</body>
</html>