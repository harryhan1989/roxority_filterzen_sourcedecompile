<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_CopyItemToSurvey, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
    <script language="javascript" type="text/javascript"> 
    //会受itemlist.aspx页对blnActioned操作的影响
    window.parent.parent.blnActioned1 = "True";
	
	window.parent.parent.blnActioned = "True";
	window.parent.parent.blnActioned = "True";
	window.parent.parent.getSurvey();
    window.parent.reloadItemList("ok");   

    
    </script>
</head>
<body onload="alert('题目复制完成')">
  
</body>
</html>
