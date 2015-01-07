<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_getSurvey, Web_Survey" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.01 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/loose.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>获取问卷源</title>

     <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
	  <link href="../css/getSurvey.css" rel="stylesheet" type="text/css" />
	   <link href="../css/AdvObj.css" rel="stylesheet" type="text/css" />
	    <script language="javascript" type="text/javascript"> <%=sClientJs %></script>
	 <script language="javascript" type="text/javascript" charset="UTF-8" src="js/getSurvey.js"></script>
     <script language="javascript" type="text/javascript" src="js/share.js"></script>
   
  
     <style type="text/css">
<!--
<%=sStyle %>
-->
     
     html,body{
         overflow-x:hidden; 
         overflow-y:auto;
     }
     </style>
</head>
<body  style=" padding:0; margin:5px 0 5px 5px;" >
<div class="PageHeadLine">
	<span style="margin:5px; width:100px; font-size:12px; float:left">页眉</span>	
	<span class="EditBT" onclick="editHeadAndFoot(SID,0)" title="编辑页眉" style="margin:5px"></span>
	<span class="PageHead" style="clear:both; display:block; margin:0px" id="PageHead"><%=sPageHead %></span>
</div>

<%=sSurveySrc%>


<div class="PageFootLine">
	<span style="float:left; margin:5px; font-size:12px">页脚</span>
	<span class="EditBT" onclick="editHeadAndFoot(SID,1)" title="编辑页脚"  style="margin:5px; float:right"></span>
	<span  id="PageFoot" style=" display:block;clear:both; margin:5px"><%=sPageFoot %></span>
</div>
<iframe src=""  id="targetWin"  width="100%" height="330px" style="display:none;overflow:hidden;"></iframe>

</body>
</html>