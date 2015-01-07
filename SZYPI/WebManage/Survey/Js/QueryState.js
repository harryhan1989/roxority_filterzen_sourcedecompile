var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
var htItem = new Hashtable();
var htOption = new Hashtable();
var htOption_ToIndex = new Hashtable();
var htItemExt = new Hashtable();
var arrItemExt = new Array();
var arrChart = new Array();
//arrItem[i][5] 存放字段;3D矩阵则存放新数组
//6　存放条件，如果为空，表示未设置，反之已设
//
var myface = new interface();

window.onload = function(){
	initLevelItem();
	extItem();
	initChartArr();
	arrChart = initChartArr();
	$("SID").value = SID;
	initHashTable();
	try{
	setParentWinHeight();	}
	catch(e){};
	initItemList($("ItemList"));
	initResultItemList($("ResultItem"));
	
	myface._MaskStyleName = "Mask";
	myface._MaskObj = $("MaskLayer");
	myface._initMask("","");
	myface._centerObj($("SubWin"),0,-100);

	if(sSource=="QS"){
		myface._openObj($("ResultStat"),-200,-100,800);
		myface._openObj($("TargetWin"),-196,-125,798);
		myface._centerObj($("ResultStat"),0,-100);
		
	}
	else{
		myface._openObj($("ResultStat"),0,0);
		myface._openObj($("TargetWin"),-0,-25);
		myface._centerObj($("ResultStat"),0,0);
	}
	
}

function initChartArr(){
	var arrResult = new Array();
	for(var i=0;i<17;i++){
		arrResult[i] = new Array();
		arrResult[i][0] = "";
		arrResult[i][1] = "";
	
	}

	arrResult[1][0] = "chart3DPie";
	arrResult[1][2] = "3D饼图";
	arrResult[1][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[2][0] = "chartPie";
	arrResult[2][2] = "2D饼图";
	arrResult[2][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[3][0] = "chart3DDoughnut";
	arrResult[3][2] = "3D圆环图";
	arrResult[3][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[4][0] = "chartDoughnut";
	arrResult[4][2] = "2D圆环图";
	arrResult[4][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[5][0] = "chart3DBar";
	arrResult[5][2] = "3D横向柱形图";
	arrResult[5][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[6][0] = "chartBar";
	arrResult[6][2] = "2D横向柱形图";
	arrResult[6][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[7][0] = "chart3DColumn";
	arrResult[7][2] = "3D纵向柱形图";
	arrResult[7][1] = ",1,2,3,4,5,6,7,8,9,10,";

	arrResult[8][0] = "chartColumn";
	arrResult[8][2] = "2D纵向柱形图";
	arrResult[8][1] = ",1,2,3,4,5,6,7,8,9,10,";
	
	
	arrResult[0][0] = "";	
	arrResult[0][1] = ",1,2,3,4,5,6,7,8,9,10,";
	arrResult[0][2] = "选择图表";
	return arrResult;
	
}


function initResultItemList(objTarget){
	for(var i=0;i<arrItem.length;i++){
		if(arrItem[i][3]==0){
			if(",2,4,5,6,7,8,9,10,11,12,15,17,18,19,20,21,".indexOf(","+arrItem[i][2]+",")>=0){
				addSelectOption(objTarget,arrItem[i][0], arrItem[i][1]);
			}
			
		}
	}
}

function extItem(){
	var SN = 0;
	for(var i=0;i<arrItem.length;i++){
		
		if(arrItem[i][3]==0){
			
			switch(arrItem[i][2]){
				case 1:		
				case 13:
					arrItemExt[SN] = new Array();
					arrItemExt[SN][0] = arrItem[i][0];
					arrItemExt[SN][1] = arrItem[i][1]+"[文本型]";
					arrItemExt[SN][2] = 1;
					arrItemExt[SN][3] = arrItem[i][3];
					arrItemExt[SN][4] = arrItem[i][4];
					arrItemExt[SN][5] = arrItem[i][0];//原形
					arrItemExt[SN][6] = "";//Condition
					SN++;
					break;
				case 2:			
					arrItemExt[SN] = new Array();
					arrItemExt[SN][0] = arrItem[i][0];
					arrItemExt[SN][1] = arrItem[i][1]+"[数值输入]";
					arrItemExt[SN][2] = 2;
					arrItemExt[SN][3] = arrItem[i][3];
					arrItemExt[SN][4] = arrItem[i][4];
					arrItemExt[SN][5] = arrItem[i][0];//原形
					arrItemExt[SN][6] = "";//Condition
					SN++;
					break;
				case 4:
				case 5:
				case 6:	
				
					arrItemExt[SN] = new Array();
					arrItemExt[SN][0] = arrItem[i][0];
					arrItemExt[SN][1] = arrItem[i][1]+"[选择类型]";
					arrItemExt[SN][2] = 4;
					arrItemExt[SN][3] = arrItem[i][3];
					arrItemExt[SN][4] = arrItem[i][4];
					arrItemExt[SN][5] = arrItem[i][0];//原形
					arrItemExt[SN][6] = "";//Condition
					SN++;
					
					break;
				case 11:
					arrItemExt[SN] = new Array();
					arrItemExt[SN][0] = arrItem[i][0];
					arrItemExt[SN][1] = arrItem[i][1]+"[选择类型]";
					arrItemExt[SN][2] = 11;
					arrItemExt[SN][3] = arrItem[i][3];
					arrItemExt[SN][4] = arrItem[i][4];
					arrItemExt[SN][5] = arrItem[i][0];//原形
					arrItemExt[SN][6] = "";//Condition
					SN++;
					break;
				case 8:
				case 9:
				case 10:
				
					arrItemExt[SN] = new Array();
					arrItemExt[SN][0] = arrItem[i][0];
					arrItemExt[SN][1] = arrItem[i][1]+"[选择类型]";
					arrItemExt[SN][2] = 8;
					arrItemExt[SN][3] = arrItem[i][3];
					arrItemExt[SN][4] = arrItem[i][4];
					arrItemExt[SN][5] = arrItem[i][0];//原形
					arrItemExt[SN][6] = "";//Condition
					SN++;
					break;
				case 7:
				case 19:				
				
					for(var j=0;j<arrItem.length;j++){
						if(arrItem[i][0]==arrItem[j][3]){							
							arrItemExt[SN] = new Array();
							arrItemExt[SN][0] = arrItem[j][0];
							arrItemExt[SN][1] = arrItem[j][1]+"[矩阵'"+arrItem[i][1]+"'中的子题目(单选题)]";
							arrItemExt[SN][2] = 4;
							arrItemExt[SN][3] = arrItem[i][0];
							arrItemExt[SN][4] = arrItem[i][4];
							arrItemExt[SN][5] = arrItem[j][0];//原形
							arrItemExt[SN][6] = "";//Condition
							SN++;
						}
					}
					break;
				case 15:
				case 20:
					for(var j=0;j<arrItem.length;j++){
						if(arrItem[i][0]==arrItem[j][3]){							
							arrItemExt[SN] = new Array();
							arrItemExt[SN][0] = arrItem[j][0];
							arrItemExt[SN][1] = arrItem[j][1]+"[矩阵'"+arrItem[i][1]+"'中的子题目(多选题)]";
							arrItemExt[SN][2] = 8;
							arrItemExt[SN][3] = arrItem[i][0];
							arrItemExt[SN][4] = arrItem[i][4];
							arrItemExt[SN][5] = arrItem[j][0];//原形
							arrItemExt[SN][6] = "";//Condition
							SN++;
						}
					}
					break;
				case 16:
					var intTemp = 0;
					for(var j=0;j<arrItem.length;j++){
						if(arrItem[i][0]==arrItem[j][3]){
							arrItem[j][5] = new Array();
							arrItem[j][6] = new Array();
							for(var n=0;n<arrOption.length;n++){
								if(arrItem[i][0]==arrOption[n][2]){									
									arrItemExt[SN] = new Array();
									arrItemExt[SN][0] = arrItem[j][0]+"_"+arrOption[n][0];
									arrItemExt[SN][1] = arrItem[j][1]+"-"+arrOption[n][1]+"[矩阵'"+arrItem[i][1]+"'中的输入单元]";
									arrItemExt[SN][2] = 1;
									arrItemExt[SN][3] = arrItem[i][0];
									arrItemExt[SN][4] = arrItem[i][4];
									arrItemExt[SN][5] = arrItem[j][0];//原形
									arrItemExt[SN][6] = "";//Condition
									SN++;
									
								}
							}
						}
					}
					break;
				
				
				
				case 21:
					for(var j=0;j<arrItem.length;j++){
						if(arrItem[i][0]==arrItem[j][3]){
							//addSelectOption(objTarget,arrItem[j][0],sn+":"+arrItem[j][1]+"[百分比'"+arrItem[i][1]+"'中的子题目(输入题)]");
							arrItemExt[SN] = new Array();
							arrItemExt[SN][0] = arrItem[j][0];
							arrItemExt[SN][1] = arrItem[j][1]+"[百分比'"+arrItem[i][1]+"'中的子题目(输入题)]";
							arrItemExt[SN][2] = 2;
							arrItemExt[SN][3] = arrItem[i][0];
							arrItemExt[SN][4] = arrItem[i][4];
							arrItemExt[SN][5] = arrItem[j][0];//原形
							arrItemExt[SN][6] = "";//Condition
							SN++;
							
						}
					}
					break;
			}
			
		}
	}
	
	
	for(var i=0;i<arrItemExt.length;i++){
		htItemExt.add(arrItemExt[i][0],i);		
	}
}


function initItemList(objTarget){
	var objNew = "";
	var intParentType = 0;
	var intParentIndex = 0;
	var sn = 1;
	var sConContentBox = "";
	addSelectOption(objTarget,0,"选择题目设置条件");		
	for(var i=0;i<arrItemExt.length;i++){		
		sConContentBox += "<div id=ConContentBox"+arrItemExt[i][0]+"></div>";
		switch(arrItemExt[i][2]){
			case 1:	
			case 2:				
			case 4:
			case 5:
			case 6:				
			case 8:
			case 9:
			case 10:				
			case 11:				
			case 12:				
			case 13:
				addSelectOption(objTarget,arrItemExt[i][0],sn+":"+arrItemExt[i][1]);
				break;
			}
			sn++;
		
	}
	$("ConContentBox").innerHTML = sConContentBox;
}


function addSelectOption(objTarget,sValue,sOption){
	var objNew = document.createElement("OPTION");
	objNew.value = sValue;
	objNew.text = sOption;
	objTarget.options.add(objNew);
}

function addCondition(){
	
}

function clearCondition(IID){
	$("ConContentBox"+IID).innerHTML = "";
	$("ConContentBox"+IID).style.display = "block";
	arrItemExt[htItemExt.items(IID)][6] = "";
}

function setCon(obj){
	var IID = obj.value;
	if(IID==0){
		$("ConOptionArea").style.display = "none";
		return;	
	}
	
	$("ConOptionArea").style.display = "block";

	var intIndex = htItemExt.items(IID);
	$("ItemType1").style.display = "none";
	$("ItemType2").style.display = "none";
	$("ItemType4").style.display = "none";
	$("ItemType44").style.display = "none";
	$("ItemType48").style.display = "none";
	$("Input_ConBox").style.display = "none";
	//$("Raletion").style.display = "none";
	
	var sTemp = "";
	for(var i=0;i<arrItemExt.length;i++){
		sTemp += arrItemExt[i][0]+":"+htItemExt.items(arrItemExt[i][0])+"\n";
	}
	
	switch(arrItemExt[intIndex][2]){
		case 1:		
			$("ItemType1").style.display = "block";
			$("Input_ConBox").style.display = "block";
			break;
		case 2:		
			$("ItemType2").style.display = "block";
			$("Input_ConBox").style.display = "block";
			break;
		case 4 :
			$("ItemType4").style.display = "block";
			$("ItemType44").style.display = "block";
			//$("Raletion").style.display = "block";
			fillOptionList(IID);
			break;
		case 11 :
			$("ItemType4").style.display = "block";
			$("ItemType44").style.display = "block";
			//$("Raletion").style.display = "block";
			fillOptionList(IID);
			break;
		case 8:
			$("ItemType4").style.display = "block";
			$("ItemType48").style.display = "block";
			fillOptionList(IID);
			break;
	}
	
}

function fillOptionList(IID){	
	$("OptionList").length = 0;	
	var intParentIndex = htItem.items(arrItemExt[htItemExt.items(IID)][3]);	
	if((intParentIndex>=0)&&(intParentIndex!="")){
		IID = 	arrItem[intParentIndex][0];
	}
	
	for(var i=0;i<arrOption.length;i++){
		if(parseInt(arrOption[i][2])==IID){			
			addSelectOption($("OptionList"),arrOption[i][0],arrOption[i][1]);
		}
		
	}
	if($("OptionList").length>5){
		$("OptionList").size =5;	
	}
	else{
		$("OptionList").size = $("OptionList").length;	
	}
}

function cancelCondition(){
	var IID = $("ItemList").options[$("ItemList").selectedIndex].value;
	
	if(IID==0){
		return;	
	}
	else{
	
		arrItemExt[htItemExt.items(IID)][6] = "";
		$("ConContentBox"+IID).innerHTML = "";
		$("ConContentBox"+IID).style.display = "none";		
	}
}

function saveItemConditionSet(){
	var IID = $("ItemList").options[$("ItemList").selectedIndex].value;
	if(IID==0){
		$("ConOptionArea").style.display = "none";
		alert("请选择题目");
		 $("ItemList").focus();
		return;
	}
	var intIndex = htItemExt.items(IID);
	
	var sResult = "";
	var sCon = "";
	var sValue = "";
	var sRaletion = "";
	
	switch(arrItemExt[intIndex][2]){
		case 1:	
			for(var i=0;i<$n("ItemType1_Con").length;i++){
				if($("ItemType1_Con_"+i).checked){
					sCon = $("ItemType1_Con_"+i).value;	
				}
			}
			sValue = $("Input_Con").value;
			break;
		case 2:
		
			for(var i=0;i<$n("ItemType2_Con").length;i++){
				
				if($("ItemType2_Con_"+i).checked){
					
					sCon = $("ItemType2_Con_"+i).value;	
					break;
				}
			}
			sValue = $("Input_Con").value;		
			
			if(!sRegExp_Int.test(sValue)||sValue==""){
				alert("本题目为数值型输入题，条件中的查询内容需要为数字");
				$("Input_Con").focus();
				return "";	
			}
			
			break;
		case 4 :
		case 11:
			sCon  = $("ItemType4_0").checked?0:1;
			sRaletion = $("ItemType4_relation_0").checked?0:1;
			
			for(var i=0;i<$("OptionList").length;i++){
				if($("OptionList").options[i].selected)	{
					sValue += $("OptionList").options[i].value+";";
				}
			}
			if(sValue==""){
				alert("请选择查询内容");
				$("OptionList").focus();
				return "";	
			}
			sValue = sValue.substring(0,sValue.length-1);
			
			break;
		case 111 :
			sCon  = $("ItemType4_0").checked?0:1;
			sRaletion = $("ItemType4_relation_0").checked?0:1;
			
			for(var i=0;i<$("OptionList").length;i++){
				if($("OptionList").options[i].selected)	{
					sValue += $("OptionList").options[i].text+";";
				}
			}
			if(sValue==""){
				alert("请选择查询内容");
				$("OptionList").focus();
				return "";	
			}
			sValue = sValue.substring(0,sValue.length-1);
			
			break;
		case 8 :
			if($("ItemType8_0").checked){
				sCon  = 0;	
			}
			else if($("ItemType8_1").checked){
				sCon  = 1;	
			}
			else if($("ItemType8_2").checked){
				sCon = 2;	
			}
			else{
				sCon = 3;	
			}
			
			
			
			for(var i=0;i<$("OptionList").length;i++){
				if($("OptionList").options[i].selected)	{
					sValue += $("OptionList").options[i].value+";";
				}
			}
			if(sValue==""){
				alert("请选择查询内容");
				$("OptionList").focus();
				return "";	
			}
			sValue = sValue.substring(0,sValue.length-1);
			
			break;
	}
	
	sResult = IID+"$"+sCon+"$"+sRaletion+"$"+sValue+"$"+arrItemExt[intIndex][2];
	arrItemExt[intIndex][6] = sResult;

	arrItemExt[intIndex][7] =showConContent(IID,parseInt(arrItemExt[intIndex][2]),sValue,parseInt(sCon),sRaletion);
	sRaletion = $("ItemType4_relation_0").checked?0:1;
	$("ConContentBox"+IID).innerHTML = "<input type='button' id='ClearConBt"+IID+"' value='清除条件'  style='display:none1' onclick='clearCondition(\""+IID+"\")' />"+arrItemExt[intIndex][7];
	$("ConContentBox"+IID).style.display = "block";
	return sResult ;
	
}

function showConContent(IID,intItemType,sValue,intConType,sRaletion){
	var sResult = "";
	var sConType = getCon(intConType,intItemType);
	
	var intIndex = htItemExt.items(IID);
	var sStyle_Con = "<span style='color:#FF0000'>";
	var sStyle_Raletion = "<span style='color:#0000FF'>";
	var sStyle_Value = "<span style='color:#00FF00;font-weight:bold'>";
	var sStyle_Item = "<span style='color:#000000'>";
	
	sResult = "";
	switch(intItemType){
		case 1:
			if(sValue!=""){
				sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>" + sStyle_Con+ sConType+"</span>"+sStyle_Value + sValue+"</span>";
			}
			else{
				sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>" + sStyle_Con+ sConType+"</span>"+sStyle_Value + "<空></span>";
			}
			break;
		case 2:
			sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>" + sStyle_Con+ sConType+"</span>"+sStyle_Value + sValue+"</span>";
			break;
		case 4 :
		case 11:
			if(sValue.lastIndexOf(";")==sValue.length-1){
				sValue = sValue.substring(0,sValue.length-1);	
			}
			var arrTemp = sValue.split(";");
			
			for(var i=0;i<arrTemp.length;i++){
				if(i<arrTemp.length-1){
					sResult += sStyle_Con+sConType+"</span>"+sStyle_Value+ htOption.items(arrTemp[i])+"</span>"+sStyle_Raletion + getCon(parseInt(sRaletion),41)+"</span>";
				}
				else{
					sResult += sStyle_Con+sConType+"</span>"+sStyle_Value+ htOption.items(arrTemp[i])+"</span>";	
				}
				
			}
			sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>" +sResult;
			break;
		case 111:
			if(sValue.lastIndexOf(";")==sValue.length-1){
				sValue = sValue.substring(0,sValue.length-1);	
			}
			var arrTemp = sValue.split(";");

			for(var i=0;i<arrTemp.length;i++){
				if(i<arrTemp.length-1){
					sResult += sStyle_Con+sConType+"</span>"+sStyle_Value+ arrTemp[i]+"</span>"+sStyle_Raletion + getCon(parseInt(sRaletion),41)+"</span>";
					
				}
				else{
					sResult += sStyle_Con+sConType+"</span>"+sStyle_Value+ arrTemp[i]+"</span>";	
				}
				
			}
			sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>" +sResult;
		
			break;
	
		case 8:
			if(sValue.lastIndexOf(";")==sValue.length-1){
				sValue = sValue.substring(0,sValue.length-1);	
			}
			var arrTemp = sValue.split(";");
			var sTempResult = "";
			for(var i=0;i<arrTemp.length;i++){
				
				sTempResult += htOption.items(arrTemp[i])+",";
				
			}
			sTempResult = sTempResult.substring(0,sTempResult.length-1);
			var sConType1 = "";
			switch(intConType){
				case 0:
					sConType = "包含" ;
					sConType1 = "中的任一选项";
					break;
				case 1:
					sConType = "完全等于" ;
					break
				case 2:
					sConType = "不包含";
					break;
				case 3:
					sConType = "完全包含";
					sConType1 = "中的所有选项";
					break;
			}
			
			
			
			sResult = sStyle_Item+arrItemExt[intIndex][1]+"</span>"+sStyle_Con+sConType +"</span>"+sStyle_Value+sTempResult+"</span>"+sConType1;
			break;
	}
	
	return sResult;
	
	function getCon (v,t){
		var r = "";
		switch(t){
			case 1:
				r = getCon1(v);
				break;
			case 2:
				r = getCon2(v);
				break;
			case 4:
			case 8:
			case 11:
				r = getCon4(v);
				break;
			case 41:
				r = getCon41(v);
				break;
		}
		return r;
	}
	
	function getCon1(v){	
		var r = "";
		switch(v){
			case 0:
				r = "等于";
				break;
			case 1:
				r = "不等于";
				break;
			case 2:
				r = "包含";
				break;
			case 3:
				r = "不包含";
				break;
		}
		return r;
	}
	
	function getCon2(v){	
		var r = "";
		switch(v){
			case 0:
				r = "等于";
				break;
			case 1:
				r = "不等于";
				break;
			case 2:
				r = "大于";
				break;
			case 3:
				r = "大于等于";
				break;
			case 4:
				r = "小于";
				break;
			default :
				r = "小于等于";
				break;
		}
		return r;
	}
	
	function getCon4(v){
		var r = "";
		switch(v){
			case 0:
				r = "等于";
				break;
			case 1:
				r = "不等于";
				break;		
		}
		return r;
	}
	
	function getCon41(v){
		var r = "";
		switch(parseInt(v)){
			case 0:
				r = "并且";
				break;
			case 1:
				r = "或者";
				break;		
		}
		return r;
	}
	
}

function fillChartList(sItemType,arr,obj){
	while(obj.length>0){
		obj.remove(obj.length-1);
	} 
	for(var n=0;n<arr.length;n++){
		//alert(arr[n][1]+"\n"+sItemType);
		if(arr[n][1].indexOf(","+sItemType+",")>=0)	{
			addSelectOption(obj,arr[n][0],arr[n][2]);
		}
	}
}

function fillChartList_(){
	try{
		fillChartList(arrItem[htItem.items($("ResultItem").options[$("ResultItem").selectedIndex].value)][2],arrChart,$("Chart"))
	}
	catch(e){
	}
}

function submitStat(checkResult){
	if(checkResult==0){
		if(($("ResultItem").selectedIndex==0)){
			alert("请选择分析对象");
			$("ResultItem").focus();
			
			return false;	
		}
		$("ResultType").value = arrItem[htItem.items($("ResultItem").options[$("ResultItem").selectedIndex].value)][2];
	}
	
	var sResult = "";
	var sConDes = "";
	for(var i=0;i<arrItemExt.length;i++){
		if(arrItemExt[i][6]!=""){
			sResult += arrItemExt[i][6]+"*";
			sConDes +=  "("+arrItemExt[i][7]+") 并且 <br/>";
		}
	}
	if(sResult==""){
		alert("请设置条件");
		$("AddCon").focus();
		return false;		
	}
	
	$("QueryCondition").value = sResult.substring(0,sResult.length-1);
	$("ConDes").value = sConDes.substring(0,sConDes.length-8);
//	$("ResultStat").style.display = "block";
	return true;
	
}


function initLevelItem(){
	var i,j;
	var arrOptionIndex = arrOption.length;
	for(i=0;i<arrItem.length;i++){

		if(arrItem[i][2]==11){
			for(j=1;j<=arrItem[i][4];j++){
				arrOption[arrOptionIndex] = new Array();
				arrOption[arrOptionIndex][0] = j;
				arrOption[arrOptionIndex][1] = "等级"+j;
				arrOption[arrOptionIndex][2] = arrItem[i][0];
				arrOption[arrOptionIndex][3] = "";
				arrOption[arrOptionIndex][4] = 0;
				
				arrOptionIndex++;
	
			}
			
		}
	}
}