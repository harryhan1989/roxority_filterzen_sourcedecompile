<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintainPage.aspx.cs" Inherits="WebManage.MaintainPage" %>
<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="button1" TagName="utlButton" Src="~/Controls/ButtonPageControl.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>���ߵ���</title>
<link href="CSS/szypistyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="Form1" runat="server">
<div id="container">
  <!--ҳü-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
    <div class="surveytable">
      <div class="maintain_top"></div>
      <div class="lpdh_mid">
      
      ���ڽ����У������ڴ�������
      	
        <div class="clearboth"></div>
      </div>
      <div class="lpdh_bottom"></div>
    </div>

  </div>
  <!--ҳ��-->
  <button1:utlButton id="utlButton" runat="server"></button1:utlButton>
</div>
</form>
</body>
</html>

<script type="text/javascript">
    //������ť����¼�
    function SearchClick()
    {
        var keyCode = document.getElementById("utlTop1$txtSearchCondition").value;
        
        window.location.href="Web/Survey/AllSurveysPage.aspx?KeyCode=" + keyCode;        
    }
</script>