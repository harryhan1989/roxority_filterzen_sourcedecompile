﻿<%@ page language="C#" autoeventwireup="true" validaterequest="false" inherits="Web_Survey.Survey.Survey_SaveItemStyle, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript">
var sError = '<%=sError%>';
var IID =<%=IID%>
if(sError=="True"){
    alert("有非法输入")
}

function updateInfo(){
        var sContent = document.getElementById("Content").value;
        var subWin = window.parent.parent.frames["targetWin"];
        window.parent.document.getElementById("Save").disabled = false;       
        subWin.document.getElementById("I"+IID).innerHTML = sContent;

    }
</script>
    <title>无标题页</title>
</head>
<body onload="updateInfo()">
 <textarea id="Content" rows="5" cols="5" style="width: 345px; height: 203px"><%=sItemHTML %></textarea>

</body>
</html>
