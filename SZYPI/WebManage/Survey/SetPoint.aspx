<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SetPoint, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript" src="Js/share.js"></script>
<script language="javascript" type="text/javascript" src="Js/interface.js"></script>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript">
var arrOption = new Array();
<%=sClientJs%>

function submitForm(){
	var sResult = "";
	var intPoint = 0;
	var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
	for(i=0; i<arrOption.length;i++){
	    intPoint = document.getElementById("F"+arrOption[i][0]).value;
	    if(sRegExp_Int.test(intPoint)==false){
	        document.getElementById("F"+arrOption[i][0]).focus();
	        alert("输入必须是整数");
	        return;
	    }
		sResult += arrOption[i][0]+":"+document.getElementById("F"+arrOption[i][0]).value + ";";
	}
	document.getElementById("OptionPointStr").value = sResult;
	document.getElementById("form1").submit();
	document.getElementById("Save").disabled = true;
}

window.onload = function(){
	setWin(45,55);window.parent.parent.openEdit();
	var myface = new interface();
	var P = myface._getShowSize();
	document.getElementById("ActiveArea").style.height = (P.h-65)+"px"
}
</script>
    <title>设置选项图片</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body class="RightOptionWin">
<div style="border:1px solid #333; background-color:#FFFFEE; margin:2px; padding:2px;">
    <div id="ToolArea" style="height:20px">
        	<div class="Lamp" style="float:left"></div >
        	<div  class="Switch_Open" id="Switch_" style="float:right"  onclick="share_switch(document.getElementById('HelpArea'),document.getElementById('Switch_'))"></div >
        </div>
        <div  id="HelpArea" style="clear:both; display:none">
    实现简单的计分，设置选项分值，可为正分、负分。<br />
            <span class="Share_Item" style="float:left"></span>在对应选项后输入分值保存之。<br />
            <span class="Warn"  style="float:left; margin-left:-16px"></span>如果问卷配置为计分的试卷，则得分使用所配置的试卷的计分规则计算，此处的计分无用。<br />
        </div>
</div>

<form name="form1" id="form1" method="post" action="SavePoint.aspx" target="targetWin" style="margin:0px;">
<div style=" overflow:auto" id="ActiveArea">
  <table width="100%"  border="0" cellpadding="5" cellspacing="0" class="BlackFont">
    <tr>
      <td>选项</td>
      <td>分值</td>
    
    </tr>
    <%=sOptionList%>
  </table>
 </div>
  <div style="border:1px solid #333; padding:2px; margin:2px; height:25px"><input name="Save" type="button" class="SaveBT" id="Save" onclick="submitForm()" value=" 保存" /> 
      <input name="Cancel" type="button" class="SaveBT" id="Cancel" onclick="window.parent.cancelOption()" value="取消" />
                                    <input name="IID" type="hidden" id="IID" value="<%=IID%>" />
                                    <input name="SID" type="hidden" id="SID" value="<%=SID%>" />
                                    <input name="OptionPointStr" type="hidden" id="OptionPointStr" />
                                    </div>
</form>
<iframe src="" style="display:none; width:100%; height:500px " name="targetWin" id="targetWin"></iframe>
</body>
</html>
