﻿<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_DelItem, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
</head>
<script language="javascript">
var SID = 0;
<%=sClientJs%>
window.onload = function(){
	window.parent.location.href = 'getsurvey.aspx?sid='+SID;
}
</script>
<body>

</body>
</html>
