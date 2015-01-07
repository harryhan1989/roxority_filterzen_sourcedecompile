<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SetOptionImage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript" src="Js/interface.js"></script>

<script language="javascript" type="text/javascript">
var arrOption = new Array();
var sRoot = "u"+<%=UID.ToString()%>;
<%=sClientJs%>
function applyImage(OID){	
	 var sAtt = showModalDialog("FileManage.aspx?"+escape(new   Date()), "文件浏览", "dialogWidth:740px; dialogHeight:430px; status:0;help:0;scroll:1");		 
	 if ((sAtt != '')&&(typeof(sAtt)!="undefined")){	
		 document.getElementById("image"+OID).innerHTML = sAtt;
		 document.getElementById("image"+OID+"_").value = sAtt;
	 }
}

function viewImage(sn){
	var sImage = document.getElementById("image"+sn+"_").value;
	var objImageArea = document.getElementById("Layer2");
	var objImage = document.getElementById("img");
	if(sImage==""){
		return;
	}
	objImageArea.style.visibility = "visible";
	objImage.src = "userdata/"+sRoot+"/"+sImage;
}

function clearImage(sn){
	document.getElementById("image"+sn).innerHTML = "";
	document.getElementById("image"+sn+"_").value = "";
}

function submitForm(){
	var sResult = "";
	for(i=0; i<arrOption.length;i++){
		sResult += arrOption[i][0]+":"+document.getElementById("image"+arrOption[i][0]+"_").value + ";";
	}
	document.getElementById("OptionImageStr").value = sResult;
	document.getElementById("form1").submit();
	document.getElementById("Save").disabled = true;
}

window.onload = function(){
	setWin(45,55);window.parent.parent.openEdit();
	var myface = new interface();
	var P = myface._getShowSize();
	document.getElementById("form1").style.height = (P.h-38)+"px"
	
}
</script>
    <title>设置选项图片</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body class="RightOptionWin">
<form name="form1" id="form1" method="post" action="SaveOptionImage.aspx" target="targetWin" style="margin:0px; padding:2px; overflow:auto">
  <table width="100%"  border="0" cellpadding="5" cellspacing="0" class="BlackFont">
    <tr>
      <td style="width:177px">选项</td>
      <td  style="width:238px">图片名</td>
      <td  style="width:301px">应用</td>
      <td  style="width:301px">清除</td>
      <td  style="width:301px">查看</td>
    </tr>
    <%=sOptionList%>
  </table>
  
  <div id="Layer2" style=" visibility:hidden; position:absolute; left:14px; top:17px; width:109px; height:11px; z-index:2" class="BlackFont">
    <table  border="0" cellpadding="0" cellspacing="1" style="background:#CCCCCC; width:100px">
      <tr>
        <td style="background:#EFF3FB"><table   border="0" cellpadding="2" cellspacing="0"  style="background:#EEE; width:100%">
          <tr   style="cursor:pointer" onclick='document.getElementById("Layer2").style.visibility="hidden"'>
            <td style="width:82%"><strong>预览图片</strong></td>
            <td  style="width:18%;cursor:pointer" onclick='document.getElementById("Layer2").style.visibility="hidden"'><img src="images/Close.gif" width="16" height="14" alt="关闭" /></td>
          </tr>
          <tr>
            <td colspan="2"><img src=""  id="img" alt="" /></td>
          </tr>
        </table></td>
      </tr>
    </table>
  </div> <input name="IID" type="hidden" id="IID" value="<%=IID%>" />
                                    <input name="SID" type="hidden" id="SID" value="<%=SID%>" />
                                    <input name="OptionImageStr" type="hidden" id="OptionImageStr" />
                                    <%=sImgHidden %>
</form>

  <div style="border:1px solid #333; background:#EEE; padding:2px; margin:2px; height:25px"><input name="Save" type="button" class="SaveBT" id="Save" onclick="submitForm()" value=" 保存" /> 
      <input name="Cancel" type="button" class="SaveBT" id="Cancel" onclick="window.parent.cancelOption()" value="取消" /></div>
<iframe src="" style="display:none; width:100%; height:500px " name="targetWin" id="targetWin"></iframe>
</body>
</html>
