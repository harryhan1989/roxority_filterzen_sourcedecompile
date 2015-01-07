<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SaveOptionImage,Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript">
var IID =<%=IID%>
function init(){
    window.parent.document.getElementById("Save").disabled = false;
}

  function updateInfo(){
//        var sContent = document.getElementById("Content").value;
        var subWin = window.parent.parent.frames["targetWin"];
        window.parent.document.getElementById("Save").disabled = false;
//        subWin.document.getElementById("I"+IID).innerHTML = sContent;
        
    }
</script>
    <title>无标题页</title>
</head>
<body onload="updateInfo()">
</body>
</html>
