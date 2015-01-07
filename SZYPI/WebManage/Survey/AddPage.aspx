<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_AddPage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
    <script language="javascript" type="text/javascript">
    function init(){

		var sMessage = '<%=sMessage%>';
		if(sMessage=="Error"){
			alert("问卷已经完成，不可增加新页，要作此操作，请将问卷反生成。")
		}
		else{
			window.parent.targetWin.location.reload();	
		}
		window.parent.closeActionWin();
    }
    
    
    </script>
</head>
<body onload="init()">
<label>
<input name="Set" type="button" id="Set" value="按钮" />
</label>
</body>
</html>
