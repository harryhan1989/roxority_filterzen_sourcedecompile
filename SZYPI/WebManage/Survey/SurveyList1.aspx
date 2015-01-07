<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SurveyList1, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
<meta http-equiv="pragma" content="no-cache" />
<title>问卷列表</title>
<link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="js/surveylist.js"></script>
<script language="javascript" type="text/javascript"><%=sClientJs %></script>

<style type="text/css">
<!--
.editstatus1 {
	padding: 1px;
	height: 18px;
	width: 200px;
	text-align:left;
	border: 1px solid #333333;
	background-color:#FFFFFF;
}
.message1 {
	font-size: 12px;
	color: #333333;
}
-->
</style>
</head>

<body onload="initSurveyList();setPageList()" class="BlackFont">
<div id="Layer1" style="position:absolute; left:175px; top:5px; width:400px; z-index:1; display:none; height:425px; border:1px solid #666;" class="ShadowEffect">
<div class="TableHead" style=" cursor:pointer"  title="关闭窗口" onclick="document.getElementById('Layer1').style.display='none'" ><span class="Share_Close" style="float:right; margin-top:5px; margin-right:5px"></span></div>
<iframe src=""  id="InfoWin"   style="width:100%;height:400px; display:block" marginheight="5" marginwidth="5" frameborder="0"></iframe> 
</div>
<table width="100%"  border="0" cellpadding="2" cellspacing="0" style="height:0px" >
  <tr>
    <td align="left"><span id="pagelist1"></span></td>
    <td align="right"><span id="pagelist2"></span></td>
  </tr>
</table>
<div style="height:450px">
<span id="SurveyList"></span>
<div id="Message"></div>

</div>
<a href="" id="hl"></a>




</body>
</html>
