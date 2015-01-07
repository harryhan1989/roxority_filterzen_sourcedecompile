<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SaveStyle, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
<script language="javascript" type="text/javascript">
window.onload = function(){
//window.parent.document.getElementById("Save").disabled = false
var sMessage = '<%=sMessage %>';
alert("样式保存完成\n"+sMessage);
}
//window.parent.parent.document.getElementById("targetWin").src = "getSurvey.aspx?SID="+ window.parent.document.getElementById("SID").value+"&"+escape(new   Date());
</script>
    <title>无标题页</title>
</head>
<body>

</body>
</html>
