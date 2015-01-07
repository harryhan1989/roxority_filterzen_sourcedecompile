<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SaveSetPar, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript">
function init(){
    try{
        window.parent.parent.openEdit()
    }
    catch(e){
//    	alert("保存完成")
    }
}
</script>
    <title>无标题页</title>
</head>
<body onload="init()">

</body>
</html>
