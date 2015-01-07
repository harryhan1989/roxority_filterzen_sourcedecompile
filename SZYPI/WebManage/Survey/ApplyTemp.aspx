<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_ApplyTemp, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript">

<%=sClientJs %>
var sPath = "TempLate/SurveyStyle/smallstyle/firstpage/";
var sPagePath = "TempLate/";
function init(){
	
    var obj;
	var sn = 0;
    var targetObj = document.getElementById("TempList");
    for(i=0; i<arrPageStyle.length; i++){
        obj = document.createElement("OPTION");
        obj.text = arrPageStyle[i][0];
        obj.value = arrPageStyle[i][1];
        targetObj.options.add(obj);
        if(sCurrPageStyle==arrPageStyle[i][1]){
            sn = i;
        }
    }
    
	targetObj.selectedIndex = sn;
	document.getElementById("Demo").innerHTML = "<a href='"+sPagePath+sCurrPageStyle+"' target=_blank title='点击查看模板'><img src="+sPath+sCurrPageStyle+".gif>";
	setps();
	window.parent.parent.openEdit();
	if(document.all){
		document.getElementById("ps").style.display = "none";

	}
	else{
		document.getElementById("psbt").style.display = "none";

	}
	
}
function showDemo(v){
	document.getElementById("Demo").innerHTML = "<a href='"+sPagePath+v+"' target=_blank title='点击查看模板'><img src="+sPath+v+".gif>";		
	setps();
}

function setps(){
	document.getElementById("ps").href = "PS.aspx?SID="+SID+"&PS="+document.getElementById("TempList").options[document.getElementById("TempList").selectedIndex].value;
}

function PS(){
	var obj = document.getElementById("TempList")
	var v = obj.options[obj.selectedIndex].value
	document.getElementById("ToSurvey").href = "PS.aspx?SID="+SID+"&PS="+v;
	document.getElementById("ToSurvey").target = "_blank";
	document.getElementById("ToSurvey").click();
}

/*
HTMLElement.prototype.click = function() {
var evt = this.ownerDocument.createEvent('MouseEvents');
evt.initMouseEvent('click', true, true, this.ownerDocument.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
this.dispatchEvent(evt);
}
*/


	

function doclick(obj){
	var evt = document.createEvent("MouseEvents");
	evt.initEvent("click",true,true);
	document.getElementById(obj).dispatchEvent(evt); 	
}

function submitForm(){
	document.getElementById("Save").disabled = true;	
	document.getElementById("form1").submit();
	return true;
}
</script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body   onload="init()" class="RightOptionWin">
    <form  action="SaveTempSet.aspx" method="post" target="targetWin" id="form1" onsubmit="return submitForm()">
    <div style="margin:5px">
      <table width="100%"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td valign="top"><select name="TempList" id="TempList" onchange="showDemo(this.options[this.selectedIndex].value)" >
          </select></td>
        </tr>
        <tr>
          <td valign="top" style="height:200px "><span id="Demo"></span></td>
        </tr>
        <tr>
          <td><table width="99%"  border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC">
            <tr>
              <td bgcolor="#EFF3FB" style="height: 35px"><input name="Save" type="button" class="SaveBT" id="Save" onclick="submitForm()" value=" 保存" />
                  <input name="Submit" type="button" class="SaveBT" value="预览效果"  id="psbt" onclick="PS()" />
                  <input name="Cancel" type="button" class="SaveBT" id="Cancel" onclick="window.parent.cancelOption()" value="取消" /><a href="" id="ToSurvey" name="ToSurvey" target="_parent" style="display:none ">预览问卷</a>
                  <input name="SID" type="hidden" id="SID" value="<%=SID%>" /><a href="" target="_blank" id="ps">预览效果</a></td>
            </tr>
          </table></td>
        </tr>
      </table>
    
    </div>
</form>
	<iframe  src="empty.htm"  style="width:100%; height:330px; display:none " id="targetWin" name="targetWin" ></iframe>
</body>
</html>
