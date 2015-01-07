<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SetPointStat,Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>评分表</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
	
	<script language="javascript" type="text/javascript">
	<%=sClientJs%>
	function checkForm(){
		var blnRelation1 = document.getElementById("Relation1")[document.getElementById("Relation1").selectedIndex].value;
		var blnRelation2 = document.getElementById("Relation2")[document.getElementById("Relation2").selectedIndex].value;
		var intMaxValue = document.getElementById("MaxValue").value;
		var intMinValue = document.getElementById("MinValue").value;
		var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
		
		if(sRegExp_Int.test(intMaxValue)==false){
			alert("输入值必须是整数");
			document.getElementById("MaxValue").focus;
			return false;
		}
		if(sRegExp_Int.test(intMinValue)==false){
			alert("输入值必须是整数");
			document.getElementById("MinValue").focus;
			return false;
		}
		intMaxValue = parseInt(intMaxValue);
		intMinValue = parseInt(intMinValue);
		
		if(intMinValue > intMaxValue){
			alert("第二个值必须大于或等于第一个值");
			document.getElementById("MaxValue").focus;
			return false;
		}
		
		
		
		document.getElementById("form1").action = "SetPointStat.aspx?SID="+SID;
		window.parent.initFace();
		return true;
	}
	
	function del(ID){
		window.parent.initFace();
		self.location.href = "SetPointStat.aspx?SID="+SID+"&ID="+ID+"&A=del";
	}
	
	function modify(ID){
		window.parent.initFace();
		self.location.href = "SetPointStat.aspx?SID="+SID+"&ID="+ID+"&A=modify";
	}
	
	
	function setModify(){
		document.getElementById("ModifyID").value = intModifyID;
		document.getElementById("CancelModifyBT").style.visibility = "hidden";
		window.parent.parent.openEdit();
		if(intModifyID==0){
			return;
		}
		for(i=0; i<2;i++){
			if(document.getElementById("Relation1")[i].value==intRelation1){
				document.getElementById("Relation1").selectedIndex = i;
			}
			
			if(document.getElementById("Relation2")[i].value==intRelation2){
				document.getElementById("Relation2").selectedIndex = i;
			}
		}
		document.getElementById("MaxValue").value = intMaxValue;
		document.getElementById("MinValue").value = intMinValue;
		document.getElementById("Flag").value = "update";
		document.getElementById("CreateRuleBT").value = "保存规则";
		document.getElementById("CancelModifyBT").style.visibility = "visible";
		
		
	}
	
	function cancelModify(){
		document.getElementById("Flag").value = "Yes";
		document.getElementById("CreateRuleBT").value = "创建规则";
		document.getElementById("Relation1").selectedIndex = 0;
		document.getElementById("Relation2").selectedIndex = 1;
		document.getElementById("MaxValue").value = "";
		document.getElementById("MinValue").value = "";
		document.getElementById("Result").value = "";
		document.getElementById("CancelModifyBT").style.visibility = "hidden";
	}
	</script>
    <style type="text/css">
<!--
.BTNormal {
	background-color: #FFFFFF;
	background-position: right center;
	cursor: pointer;
	text-align: center;
	vertical-align: middle;
}
.BTMove {
	background-color: #0066FF;
	background-position: right center;
	cursor: pointer;
	text-align: center;
	vertical-align: middle;
}
-->
    </style>
</head>
<body  onload="setWin(45,55);setModify()" class="RightOptionWin">
<table  border="0" cellpadding="2" cellspacing="1" style="background:#CCC; width:100%">
  <tr style="background:#EEE">
    <td width="8%" nowrap="nowrap">当得分</td>
    <td width="29%" nowrap="nowrap">关系</td>
    <td width="9%" nowrap="nowrap">值</td>
    <td width="15%" nowrap="nowrap">并且</td>
    <td width="15%" nowrap="nowrap">关系</td>
    <td width="9%" nowrap="nowrap">值</td>
    <td width="15%" nowrap="nowrap">结论</td>
    <td width="15%" nowrap="nowrap">修改</td>
    <td width="15%" nowrap="nowrap">删除</td>
  </tr>
  
 	<%=sList%>
</table>
<form name="form1" id="form1" method="post" action="SetPointStat.aspx" onsubmit="return checkForm()" style="margin:0px">
<table style="width:100%; margin:2px">
        <tr>
          <td style="width:60px">当得分</td>
          <td ><select name="Relation1" id="Relation1">
              <option value="0" selected="selected">大于或等于</option>
              <option value="1">大于</option>
            </select>
              <input name="MinValue" type="text" id="MinValue" size="5" maxlength="5" /></td>
        </tr>
        <tr>
          <td>并且</td>
          <td><select name="Relation2" id="Relation2">
            <option value="2">小于或等于</option>
            <option value="3"  selected="selected">小于</option>
                                </select>
              <input name="MaxValue" type="text" id="MaxValue" size="5" maxlength="5" /></td>
        </tr>
        <tr>
          <td>结论为</td>
          <td><textarea name="Result" cols="40" rows="5" id="Result" style="100%"><%=sModifyResult%></textarea></td>
        </tr>
        <tr>
          <td colspan="2">
           <div style="border:1px solid #ccc; padding:2px; margin:2px; height:25px">
          <input name="CreateRuleBT" id="CreateRuleBT" type="submit" class="SaveBT" value="创建规则"   />
              <input name="Submit22" type="button" class="SaveBT" value="取消修改" id="CancelModifyBT" style="visibility:hidden " onclick="cancelModify()" />
              <input name="Submit" type="button" class="SaveBT" value=" 取 消 " onclick="window.parent.cancelOption()" />
            <input name="Flag" type="hidden" id="Flag" value="Yes" />
            <input name="ModifyID" type="hidden" id="ModifyID" />
            </div>
            
            </td>
        </tr>
      </table>
</form>
</body>
</html>
