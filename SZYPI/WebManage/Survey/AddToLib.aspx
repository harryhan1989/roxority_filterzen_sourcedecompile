<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_AddToLib, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
    <script language="javascript" type="text/javascript">
    <%=sClientJs %>
	window.onload = function(){
		alert("加入完成");
		try{
			window.parent.document.getElementById("MessageLayer").style.display = "none";
			window.parent.document.getElementById("ShadowLayer").style.display = "none";
			
		}
		catch(e){
		
		}
	}
    </script>
</head>
<body>

</body>
</html>
