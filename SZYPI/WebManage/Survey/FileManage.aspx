<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_FileManage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript">
var sSelectFile;

var sBTOverImage = "images/ShowLeft2.gif";
var sBTNormalImage = "images/ShowLeft1.gif";
function ShowUploadWin(){
	var objUp = document.getElementById("Up");
	var objFile = document.getElementById("File");
	var objBT = document.getElementById("BT");
	if(objUp.style.width == "200px"){
		objUp.style.width = "0px";
		objFile.style.width = "733px";
		BT.src = "images/ShowRight1.gif";
		sBTOverImage = "images/ShowRight2.gif";
		sBTNormalImage = "images/ShowRight1.gif";
	}
	else{
		objUp.style.width = "200px";
		objFile.style.width = "533px";
		BT.src = "images/ShowLeft1.gif";
		sBTOverImage = "images/ShowLeft2.gif";
		sBTNormalImage = "images/ShowLeft1.gif";
	}
	
}

function setMouseMove(d){
	var objBT = document.getElementById("BT");
	if(d==0){//如果鼠标移入
		objBT.src = sBTOverImage
	
	}
	else{
		objBT.src = sBTNormalImage
	}
}

function applyFile(){
	try{
		window.parent._e.addMedia(sSelectFile);
	}
	catch(e){
		window.returnValue = sSelectFile;
		window.close();
	}
}
</script>
    <title>文件管理</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body>
<table style="width:100%; height:100% "  border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top" style="width:200px;" id="Up"><iframe src="FileToServer.aspx" style="width:100%; height:375px;" frameborder="0"  id="UpWin" scrolling="no"></iframe></td>
  <td background="images/Line.gif" style="width:7px;cursor:pointer "  onclick="ShowUploadWin()" onmousemove="setMouseMove(0)" onmouseout="setMouseMove(1)"><img src="images/ShowLeft1.gif" width="7" height="50" id="BT" alt="" /></td>
    <td valign="top" style="width:533px" id="File"><iframe src="FileExplorer.aspx" style="width:100%; height:375px " frameborder="0" id="FileWin"></iframe></td>
  </tr>
</table>
  
</body>
</html>
