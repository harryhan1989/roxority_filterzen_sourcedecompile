function checkSelect(sParStr,sCheckPar){
		if(sParStr.indexOf(sCheckPar)>=0){
			return true;
		}
		return false
}
function getSelectValue(sParStr, sCheckPar) {
    if (sParStr.indexOf(sCheckPar) >= 0) {
        return true;
    }
    return false
}
	function setAnswerPSW(){window.parent.optionActionWin("SetAnswerPSW.aspx?SID="+SID+"&"+escape(new   Date()),"配置答卷密码",200,500);}	
	function setIPToScreen(){window.parent.optionActionWin("IPToScreen.aspx?SID="+SID+"&"+escape(new   Date()),"设置IP来源",300,400);}
	function setHiddenItem(){window.parent.optionActionWin("HiddenItemSet.aspx?SID="+SID+"&"+escape(new   Date()),"设置题目隐藏",500,700);}
	function setURLVar(){window.parent.optionActionWin("URLVarSet.aspx?SID="+SID+"&"+escape(new   Date()),"设置URL传值",300,700);}
	function setRepeatAnswerItem(){window.parent.optionActionWin("RepeaAnswerItemSet.aspx?SID="+SID+"&"+escape(new   Date()),"不允许重复回答题目设置",500,700);}
	function initSelect(){
		var oOption;
		for(i=0; i<arrPageStyle.length; i++){
			oOption = document.createElement("OPTION");
			oOption.value = arrPageStyle[i][0];
			oOption.text = arrPageStyle[i][2];
			$("EndPage").options.add(oOption);
		}
		
		for(i=0; i<arrSurveyClass.length; i++){
			oOption = document.createElement("OPTION");
			oOption.value = arrSurveyClass[i][0];
			oOption.text = arrSurveyClass[i][1];
			$("ClassID").options.add(oOption);
		}
	}
	

	function init(){
		initSelect();
		for(i=0; i<$("ClassID").length; i++){
			if($("ClassID")[i].value == intClassID){
				$("ClassID").selectedIndex = i;
			}
		}
		
		for(i=0; i<$("EndPage").length; i++){
			if($("EndPage")[i].value == intEndPage){
				$("EndPage").selectedIndex = i;
			}
		}
		sPar += "|";
		$("Email").checked = checkSelect(sPar, "|Email:1|");
		$("NeedConfirm").checked = checkSelect(sPar, "|NeedConfirm:1|");
		$("CheckCode").checked = checkSelect(sPar,"|CheckCode:1|");
		$("IP").checked = checkSelect(sPar,"|IP:1|");
		$("IPToScreen").checked = checkSelect(sPar,"|IPToScreen:1|");
		$("Cookies").checked = checkSelect(sPar,"|Cookies:1|");
		$("PSW").checked = checkSelect(sPar,"|PSW:1|");
		$("AnswerPSW").checked = checkSelect(sPar,"|AnswerPSW:1|");
		$("RecordIP").checked = checkSelect(sPar,"|RecordIP:1|");
		$("RecordTime").checked = checkSelect(sPar,"|RecordTime:1|");
		$("Email").checked = checkSelect(sPar,"|Email:1|");
		$("MemberLogin").checked = checkSelect(sPar,"|MemberLogin:1|");
		$("AnswerArea").checked = checkSelect(sPar,"|AnswerArea:1|");
		$("Quota").checked = checkSelect(sPar,"|Quota:1|");
		$("GUIDAndDep").checked = checkSelect(sPar, "|GUIDAndDep:1|");
		
	    $("ResultPublish1").checked = checkSelect(sPar, "|ResultPublish:1");
	    $("ResultPublish2").checked = checkSelect(sPar, "|ResultPublish:2");
	    $("ResultPublish3").checked = checkSelect(sPar, "|ResultPublish:3");
		    
		initLan(sLan,intLan,$("Lan"));

		if(sActive=="1"){
			
			$("Active").checked = true;
		}
		else{
			$("Active").checked = false;
		}
		
		$("Report").checked = checkSelect(sReport,"|Report:1|");

		$("ReportAnswerResult").checked = checkSelect(sReport,"|ReportAnswerResult:1|");
		
		$("ReportStat").checked = checkSelect(sReport,"|ReportStat:1");
		
		$("ReportDataList").checked = checkSelect(sReport,"|ReportDataList:1|");

		$("ReportPoint").checked = checkSelect(sReport, "|ReportPoint:1|");
		
		$("XML").checked = checkSelect(sReport,"|XML:1|");
		
		$("AnswerXML").checked = checkSelect(sReport,"|AXML:1|");
		
		$("CustomizeReport").checked = checkSelect(sReport,"|CR:1|");
		
		if(sHiddenItem!=""){
			$("HiddenItem").value = sHiddenItem;
			$("HiddenItem").checked = true;
		}		
		if(sURLVar!=""){
			$("URLVar").value = sURLVar;
			$("URLVar").checked = true;
		}
		$("SID").value = SID;
		$("MaxAnswerAmount").value = intMaxAnswerAmount;
		$("SPoint").value = intSPoint;
		$("SurveyPSW").value = sSurveyPSW;
		$("EndDate").value = sEndDate;
		$("ToURL").value = sToURL;
		 if(blnSetClientReport==false){
	        $("Report").disabled = true;
		    $("ReportAnswerResult").disabled = true;
		    $("ReportStat").disabled = true;
		    $("ReportDataList").disabled = true;
		    $("ReportPoint").disabled = true;
			$("XML").disabled = true;
			$("AnswerXML").disabled = true;
			$("CustomizeReport").disabled = true;
	    }
		setPSW();
		showDemo($("EndPage")[$("EndPage").selectedIndex].value);
        try{
		      window.parent.openEdit();
		   }
		   catch (err)
		   {
		   }
		
		optionCR();
		initTestPaper(sPar);
		var myface = new interface();
		var _P = myface._getShowSize();
		document.getElementById("ParTable").style.height = (_P.h-40)+"px";	
	
	}
	
	
	
	function setPSW(){
		if($("PSW").checked==true){
			$("SurveyPSW").style.visibility = "visible";
		}
		else{
			$("SurveyPSW").style.visibility = "hidden";
		}
	}
	
	function showDemo(v){
		$("demo").innerHTML = "";
		$("ToURL").style.display = "none";
		for(i=0;i<arrPageStyle.length; i++){
			if(arrPageStyle[i][0]==v){
				switch(parseInt(arrPageStyle[i][3])){
					case 1:
						$("demo").innerHTML = "<img src=template/surveystyle/smallstyle/lastpage/"+arrPageStyle[i][1]+".gif>"				
						break;
					case 2:
						
						break;
					case 3:
						
						break;
					case 4:
						$("demo").innerHTML = "输入一个网址:";
						$("ToURL").style.display = "block"						
						break;				
				
				}
				
			}
			
		}
	}
	
	
	function checkForm(){
	
		var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
		var sURL = $("ToURL").value;
		if($("ToURL").style.display=="block"){
			if(sURL==""){
				$("ToURL").focus();
				alert("网址不能为空");
				return false;
			}
			if(sURL.toLowerCase().indexOf("http://")!=0){
				$("ToURL").focus();
				alert("网址格式错误，需要为形如http://的网址");
				return false;
			}
		}
		
		var intTemp = $("MaxAnswerAmount").value;
		if(sRegExp_Int.test(intTemp)==false){
			$("MaxAnswerAmount").focus();
			alert("最大答卷数必须是整数");
			return false;
		}
		
		intTemp = parseInt(intTemp);
		if(intTemp<0){
			$("MaxAnswerAmount").focus();
			alert("最大答卷数必须是大于等于0的整数");
			return false;
		}
		
		if(!sRegExp_Int.test($("SPoint").value)){
			$("SPoint").focus();
			alert("问卷分值必须是整数");
			return false;
		}

//		try {
//		    window.parent.initFace();
//		}
//		catch (Error) { 
//		
//		}
	}
	
	
	function optionCR(){
		if($("XML").checked){
			$("CustomizeReport").disabled = false;
		}
		else{
			$("CustomizeReport").disabled = true;	
		}
		
	}
	
	
	
		function initLan(sInput,currSelect,obj){
		var o;
		var arr = sInput.split("|");		
		for(var i=0;i<arr.length-1;i++){
			o = document.createElement("OPTION");
			o.text = arr[i].substring(arr[i].indexOf(":")+1);
			o.value = arr[i].substring(0,arr[i].indexOf(":"));
			obj.options.add(o);
			if(parseInt(arr[i].substring(0,arr[i].indexOf(":")))==parseInt(currSelect)){
				currSelect = i;
				o.selected = true;
			}
		}

	}
	
	
	function setTestPaper(obj){
		if(obj.checked){
			$("TestPaperBox").style.display = "block";
		}
		else{
			$("TestPaperBox").style.display = "none";
		}
	}
	
	function initTestPaper(sInput){
		$("TPaper").checked =checkSelect(sPar,"|TPaper:1|");
		if($("TPaper").checked){
			$("TestPaperBox").style.display = "block";
		}
		var sTTC= sInput.substring(sInput.indexOf("|TTC:")+5);
		var sTToAll = sInput.substring(sInput.indexOf("|TToAll:")+8);
		sTToAll = sTToAll.substring(0,1);
		var arrTTC = sTTC.substring(0,sTTC.indexOf("|")).split(",");	
		try{
			$("TToAll"+sTToAll).checked = true;
		}
		catch(e){
			$("TToAll0").checked = true;
		}
		
	}
	
	function selectMemberLogin(v){
		if(v){
			$("MemberLogin").checked = true;
		}
	}