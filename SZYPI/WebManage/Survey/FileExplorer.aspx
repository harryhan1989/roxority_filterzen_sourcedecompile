<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_FileExplorer, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript">
var sFileStr = "";
var arrFiles;
<%=sClientJs %>
function init(){
    arrFiles = sFileStr.split(";");
	var intArrLen = arrFiles.length;
	var intRowAmount1 = 0;
	var intRowAmount2 = 4;
	var s = "";
	intRowAmount1 = parseInt(intArrLen/intRowAmount2);
	if(intRowAmount1<intArrLen/intRowAmount2){
		intRowAmount1 += 1;
	}
	
	var sTop = '<table width="100%"  border="0" cellpadding="5" cellspacing="0">';
	var sn = 0;
	var ico = "";
	for(i=0; i<intRowAmount1; i++){
		s += "<tr>";
		for(j=0; j<intRowAmount2; j++){
			//sn = i*intRowAmount1+j
			if(typeof(arrFiles[sn])!="undefined"){
				ico = "File_"+arrFiles[sn].substring(arrFiles[sn].lastIndexOf(".")+1)+".gif"				
				s += "<td onmouseover=setStyle("+sn+",0)  onmouseout=setStyle("+sn+",1)  onclick=setStyle("+sn+",2)  class='FileNormal' title='"+arrFiles[sn]+"，右键点击查看详情' id='b"+sn+"' oncontextmenu='showImage("+sn+")'><img src='images/"+ico+"' class='ICOBG'><BR><input type='checkbox' name='checkbox' id='f"+sn+"' value='"+arrFiles[sn]+"' onclick=setStyle("+sn+",2) />"+arrFiles[sn].substring(0,10)+"</td>";
			}
			else{
				s += "<td></td>";
			}
			sn++;
		}
		s+= "</tr>"
	}	
	s = sTop + s +"</table>";
	document.getElementById("FileList").innerHTML = s ;
}



function setStyle(id,d){
	var obj = document.getElementById("b"+id)
	var objCheckBox = document.getElementById("f"+id);
	
	
	if(objCheckBox.checked==true){
		if(d==2){
			obj.className = "FileNormal";
			objCheckBox.checked=false
			return;
		}
		else{
			obj.className = "FileSelected";
			return;
		}
	}
	if(d==0)	//over
	{
		obj.className = "MouseOver";
		
	}
	else
	{
		if(d==1)	//out
		{			
			obj.className = "FileNormal";
			
		}
		else{//select
			objCheckBox.checked=true
			obj.className = "FileSelected";
		}
	
	}
}

function DelFile(){
	var sResult = "";
	for(i=0; i<arrFiles.length; i++){
		if(document.getElementById("f"+i).checked==true){
			sResult += document.getElementById("f"+i).value+";"
		}
	}
	if(sResult==""){
		alert("没有选中文件")
		return;
	}
	if(confirm("确定删除？")==true){
		document.getElementById("SelectResult").value = sResult;
		document.getElementById("form1").submit();
	}
	
	
	
}

function selectAll(){
	for(i=0; i<arrFiles.length;i++){
		if(document.getElementById("f"+i).checked==false){
			document.getElementById("b"+i).click();
		}
	}
}

function applyFile(){
	var intSelectAmount = 0;
	var sSelectFile = "";
	for(i=0; i<arrFiles.length;i++){
		if(document.getElementById("f"+i).checked==true){
			intSelectAmount++;
			sSelectFile = arrFiles[i];
		}
	}
	if(intSelectAmount==0){
		alert("请选择一个文件")
		return;
	}
	if(intSelectAmount>=2){
		alert("一次只能选择一个文件")
		return;
	}
	window.parent.sSelectFile = sSelectFile;
	window.parent.applyFile();
	
}

function selectOther(){
	for(i=0; i<arrFiles.length;i++){
		document.getElementById("b"+i).click();
	}
}


function showImage(id){
	var objImageArea = document.getElementById("Layer2");
	var objImage = document.getElementById("img");
    objImageArea.style.visibility = "visible";
	objImage.src = "userdata/"+Root+"/"+arrFiles[id];
}
</script>
<title>文件浏览</title>
<link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.FileNormal {
	font-size: 12px;
	color: #000000;
	text-decoration: none;
	border: 1px solid #FFFFFF;
}
.FileSelected {
	font-size: 12px;
	color: #FFFFFF;
	text-decoration: none;
	background-color: #0000FF;
	border: 1px dashed #FFFFCC;
}
.MouseOver {
	font-size: 12px;
	background-color: #FFFFCC;
	text-decoration: underline;
	border: 1px dashed #FFFF00;
}
.ICOBG {
	background-color: #FFFFFF;
}
-->
    </style>

</head>
<body onload="init()">
 <div id="Layer2" style=" visibility:hidden; position:absolute; left:5px; top:36px; width:109px; height:11px; z-index:2" class="BlackFont">
    <table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#EFF3FB"><table width="100%"  border="0" cellpadding="2" cellspacing="0" bgcolor="#EEEEEE">
          <tr>
            <td width="82%" ><strong>预览图片</strong></td>
            <td width="18%" onclick='document.getElementById("Layer2").style.visibility="hidden"' style="cursor:pointer "><img src="images/Close.gif" width="16" height="14" alt="关闭" /></td>
          </tr>
          <tr>
            <td colspan="2"><img src=""  id="img" alt="" /></td>
          </tr>
        </table></td>
      </tr>
    </table>
</div>
<form action="" method="post" name="form1" class="Myform" id="form1">
      <table width="99%"  border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC">
        <tr>
          <td bgcolor="#EEEEEE">            <table width="100%"  border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td><input type="button" name="Submit" value="应用选中文件" onclick="applyFile()" /></td>
              <td align="right"><input name="SelectAll" type="button" id="SelectAll" onclick="selectAll()" value="全选" />
                <input name="SelectOther" type="button" id="SelectOther" onclick="selectOther()" value="反选" />
                <input type="button" name="Submit3" value="删除选中的文件" onclick="DelFile()" />
                <input type="hidden" name="SelectResult" id="SelectResult" /></td>
            </tr>
            </table></td>
        </tr>
  </table>     
</form>
<span id="FileList"></span>
</body>
</html>
