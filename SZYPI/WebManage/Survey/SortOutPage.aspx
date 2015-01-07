<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SortOutPage, Web_Survey" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
      <link href="../css/SortOutPage.css" rel="stylesheet" type="text/css" />
	<script language="javascript">
	var arrPage = new Array();
	var arrItem = new Array();
	var action = "Card2";

	<%=sClientJs%>
	window.onload = function(){
		try{
			window.parent.complateActionWin();
		}
		catch(e){
		
		}
		initFace();
		document.getElementById(action).click();
		if(sMessage!=""){
			window.parent.blnActioned = "True";
			document.getElementById("Message").value = sMessage;
		}
		
	}
	
	
	function selectBT(obj,optionWin){
		document.getElementById("Card1").className = "NormalBT";
		document.getElementById("Card2").className = "NormalBT";
		document.getElementById("Card3").className = "NormalBT";
		
		
		
		document.getElementById("ItemToPageWin").style.display = "none";
		document.getElementById("DelPageWin").style.display = "none";
		document.getElementById("AddPageWin").style.display = "none";
		
		
		document.getElementById(obj).className = "MoveBT";
		document.getElementById(optionWin).style.display  = "block";
	}
	
	
	function initFace(){
//		document.getElementById("Card0").style.width = 8;
	//	document.getElementById("Card4").style.width = document.body.clientWidth-310;
		var oOption = null;
		for(i=0;i<arrItem.length;i++){
			oOption = document.createElement("OPTION");
			oOption.value = arrItem[i][0];
			oOption.text = "页"+arrItem[i][2]+":"+arrItem[i][1];
			document.getElementById("ItemList").options.add(oOption);
		}
		
		for(i=0;i<arrPage.length;i++){
			oOption = document.createElement("OPTION");
			oOption.value = arrPage[i][1];
			oOption.text = "第"+arrPage[i][1]+"页";
			document.getElementById("PageList").options.add(oOption);
		}
		
		for(i=1;i<arrPage.length;i++){
			oOption = document.createElement("OPTION");
			oOption.value = arrPage[i][0];
			oOption.text = "第"+arrPage[i][1]+"页";
			document.getElementById("DelPageList").options.add(oOption);
		}
	}
	
	
	function checkForm1(){
		if(document.getElementById("ItemList").selectedIndex<0){
			alert("需选择移动的题目");
			document.getElementById("ItemList").focus();
			return false;
		}
		
		if(document.getElementById("PageList").selectedIndex<0){
			alert("需选择移动到的页");
			document.getElementById("PageList").focus();
			return false;
		}
	}
	
	function checkForm2(){
	
		var obj = document.getElementById("DelPageList");
		if(obj.selectedIndex<0){
			alert("需选择需要删除的页");
			obj.focus();
			return false;
		}
		
		var sDelPageID = "";
		var sDelPageNo = "";
		
		for(i=0;i<obj.length;i++){
			if(obj[i].selected){			
				sDelPageID = obj[i].value;
				sDelPageNo = getPageNo(obj[i].value);
				break;
			}			
		}
		
		if(sDelPageID!=""){
		
			document.getElementById("DelPageID").value = sDelPageID;
			document.getElementById("DelPageNo").value = sDelPageNo;
		}

		if((document.getElementById("DelPageID").value=="")||(document.getElementById("DelPageNo").value=="")){
			alert("有错误发生！");
			obj.focus();
			return false;
		}
		
	}
	
	function getPageNo(PID){
		for(j=0;j<arrPage.length;j++){
			if(parseInt(arrPage[j][0])==parseInt(PID)){
				return arrPage[j][1]
			}
		}
	}
	
	function checkForm3(){
		
	}
	

	</script>
</head>
<body class="BlackFont">
<div id="BG">
<div class="BTExpand" id="Card00"><span class="BTText"  id="BT0"></span></div>
<div class="MoveBT" id="Card1" onclick="selectBT(this.id,'ItemToPageWin')"><span class="BTText"  id="BT1">题目改页</span></div>
<div class="NormalBT" id="Card2" onclick="selectBT(this.id,'DelPageWin')"><span class="BTText"  id="BT2">删除页</span></div>
<div class="NormalBT" id="Card3" onclick="selectBT(this.id,'AddPageWin')"><span class="BTText"  id="BT3">批量加页</span></div>
<div class="BTExpand" id="Card41"><span class="BTText"  id="BT4"></span></div>
</div>
<div style=" background-color:#FFFFEE">
	<div>
		<div id="ItemToPageWin" style="display:block">
			<form action="" method="post" name="form1" id="form1" onsubmit="return checkForm1()">
				<div style="float:left">
					<span style="display:block; margin:5px">选择题目(按住鼠标拖动或按住Shift+进行连选，按住Ctrl键点击选项进行加选)</span>
					<select name="ItemList" id="ItemList" multiple="multiple" size="15" style="width:400px"></select>
				</div>
				<div>
					<span style="display:block; margin:5px">移动到</span>
					<select name="PageList" id="PageList"  size="15" style="width:100px"></select>

				</div>
			  <div style="clear:both; margin-top:10px"><input type="submit" value="保存" />
			    <input name="Flag" type="hidden" id="Flag1" value="Form1" />
			  </div>
			</form>
			
		</div>
		<div id="DelPageWin" style="display:none">
			<form action="" method="post" name="form2" id="form2" onsubmit="return checkForm2()">
			<span style="display:block;height:25px; margin-top:10px">选择需要删除的页面</span>
				<select name="DelPageList" id="DelPageList"  size="12" style="width:100px"></select>
				<div style="clear:both; margin-top:10px">
				<input type="submit" value="保存" />
			    <input name="Flag" type="hidden" id="Flag2" value="Form2" />	
				<input type="hidden" name="DelPageNo" id="DelPageNo" />
				<input type="hidden" name="DelPageID" id="DelPageID" />		
				<span style="display:block; margin:5px">提示：第一页不能删除，被删除页内的题目被移动到上一页，逻辑设置将被清除。</span>
				</div>
			</form>
		
		</div>
		<div id="AddPageWin" style="display:none">
	
		<form action="" method="post" name="form3" id="form3" onsubmit="return checkForm3()">

				<span style="display:block;height:25px; margin-top:10px">选择需要增加的页数</span>
				<select name="NewPage" id="NewPage"  size="10" style="width:100px">
				<option value="1" selected="selected">1页</option>
				<option value="2">2页</option>
				<option value="3">3页</option>
				<option value="4">4页</option>
				<option value="5">5页</option>
				<option value="6">6页</option>
				<option value="7">7页</option>
				<option value="8">8页</option>
				<option value="9">9页</option>
				<option value="10">10页</option>
				
				</select>
				<div style="clear:both; margin-top:10px"><input type="submit" value="保存" />
			    <input name="Flag" type="hidden" id="Flag3" value="Form3" />
				</div>
			</form>
		
		
		</div>
		<div style=" display:block" id="Message"></div>
	</div>
</div>
</body>
</html>

