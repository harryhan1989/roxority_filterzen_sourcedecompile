var	sRegExp_Email = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
var	sRegExp_Real = /^[+-]?\d+(\.\d+)?$/;
var	sRegExp_IDCard15 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
var	sRegExp_IDCard18 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/;
var	sRegExp_IDCard17X = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(x|X)$/;
var	sRegExp_PostCode = /^[1-9]\d{5}$/;
var	sRegExp_Mob = /(130|131|132|133|134|135|136|137|138|139|150|151|153|156|157|158|159|185|188|189)\d{8}/;
var	sRegExp_Date = /^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$/;
var sRegExp_En=  /[u00-uFF]/ ;     
var sRegExp_Cn  = /\W/;
sRegExp_Cn  = /^[u4E00-u9FA5]+$/;
var intTargetPage=intTargetSID = 0;
var intpageamount = 1;
var intCurrPage = 1;
var sOverPage=sEliminate = "";
var htItem=htOption=sMessage = null;
var sURLVar=sHiddenItem=GUID=sProgressiveAsk='';
var _I=new Array();
var _O= new Array();
var blnCheckCode = false;
var blnAnswerPSW = false;
function getcheckcode(){
	$("CheckCodeImg").src = "../../checkcode.aspx";	
}

function setObj(){
	var sObj = "<table><tr>";
	var blnHasObj = false;
	if(blnCheckCode){
		sObj += '<td>'+_L[0]+'<input name="CheckCode" type="text" id="CheckCode" size="5" maxlength="5" class="CheckCode"><img src="../../checkcode.aspx" id="CheckCodeImg" onclick="getcheckcode()"></td>';
		blnHasObj = true;
	}
	else{sObj += '<td></td>';}
	if(blnAnswerPSW){
		sObj += '<td>'+_L[1]+':<input name="AnswerPSW" type="text" id="AnswerPSW" size="10" maxlength="30" class="OneTimePSW"></td>';
		blnHasObj = true;
	}
	else{sObj += '<td></td>';}	
	
	if(blnHasObj){
		sObj += "</tr></table>";
		sObj = "<BR>"+sObj
		$("obj").innerHTML = sObj;
		
	}else{sObj = "";}	
}

window.onload = function() {
if (typeof (sLanguage) != "undefined") {
        _L = sLanguage.replace("<|>", '"').split("|");
     }  
    htItem = initHT(_I);
    htOption = initHT(_O);
    try { $("page_1").style.display = "block"; } catch (e) { alert(_L[2]); return; }
    try { $("GUID").value = GUID; } catch (e) { }
    setObj();
    setBT();
    try { initHiddenItem(sHiddenItem); } catch (e) { };
    try { bindURLVar(sURLVar); } catch (e) { };
    initObj();
    try { initIntroductionAnswer(_I, htItem, htOption) } catch (e) { };
    try { initProgressiveAsk_0(sProgressiveAsk) } catch (e) { };
}

function $(o$){try{return 	document.getElementById(o$)}catch(e){alert(_L[3])}}
function $n(o$){try{return 	document.getElementsByName(o$)}catch(e){alert(_L[3])}}

function setBT(){
	if(intpageamount==1){
		$("submitbt").disabled = false;
		$("submitbt").style.visibility = "visible";
		$("nextpagebt").style.display = "none";
		try {
		    $("beforepagebt").style.display = "none";
		}
		catch (e) { 		
		}
		return;
	}

	if (intCurrPage < intpageamount) {
	    $("submitbt").style.visibility = "visible";
	    $("submitbt").disabled = true;
	    $("nextpagebt").disabled = false;
	    $("nextpagebt").style.visibility = "visible";
	    $("nextpagebt").style.display = "";	    
	}
	else {
	    $("submitbt").disabled = false;
	    $("submitbt").style.visibility = "visible";
	    $("nextpagebt").style.display = "none";
	}

	if (intCurrPage > 1) {
	    try {
	        $("beforepagebt").disabled = false;
	        $("beforepagebt").style.visibility = "visible";
	        $("beforepagebt").style.display = "";
	    }
	    catch (e) {
	    }
	}
	else if (intCurrPage == 1) {
	    try {
	        $("beforepagebt").style.display = "none";
	    }
	    catch (e) {
	    }
	}
}

function nextpage(){
	var i;
	if(intCurrPage>=intpageamount){	return;}
	intTargetPage = 0;
	
	for(i=0;i<_I.length; i++){
		if((_I[i][2]==intCurrPage)&&(_I[i][6]==0)){			
			if(!setDataCheck(i)){
				try{$n("f"+_I[i][0])[0].focus();}catch(e){}
			    alert(sMessage);						
				return;
			}
		}
	}
	for(i=0;i<_I.length;i++){		
		if(_I[i][2]==intCurrPage){			
			doLogic(i);
		}
	}

	$("page_"+intCurrPage).style.display = "none";	
	switch(parseInt(intTargetPage)){
		case 0:
			intCurrPage++;
			$n("submitbt").disabled = true;			
			break;
		case -1:
			top.location.href = "../../toFinish.htm"			
			break;
		case -2:
			$("submitbt").disabled = false;
			$("SurveyForm").submit();
			return;		
			break;
		default:
			intCurrPage = intTargetPage;
			break;			
	}	
	if(intTargetPage==-1){$("SurveyForm").submit();init();return;}

	if(sOverPage!=""){		
		if(sOverPage.lastIndexOf(",")==0){sOverPage = sOverPage.substr(0,sOverPage.length-1)}

		var arrOverPage = sOverPage.split(",");
		arrOverPage = intSort(arrOverPage);
		var ok= new Array();
		var result = arrOverPage;		
		for(i=0; i<arrOverPage.length; i++){
			for(j=i+1; j<arrOverPage.length;j++){
				if(arrOverPage[i]==arrOverPage[j]){
					result[i] = null
				}
			}
		}
		var m = 0
		for(i=0;i<result.length;i++){
			if(result[i]!=null){
				ok[m] = result[i]
				m++
			}
		}		
		arrOverPage = ok;		
		for(i=0; i<arrOverPage.length; i++){
			if(intCurrPage<=arrOverPage[i]){
				if(intCurrPage==arrOverPage[i]){
					intCurrPage++;
				}
				else{
					break;
				}
			}
		}
	}
	$("page_"+intCurrPage).style.display = "block";	
	initSlide();
	setBT();	
	scroll(0,0);
	if(intCurrPage>=intpageamount){return;}
}


function beforepage() {
    var i;
    if (intCurrPage <=1) { return; }
    intTargetPage = 0;

//    for (i = 0; i < _I.length; i++) {
//        if ((_I[i][2] == intCurrPage) && (_I[i][6] == 0)) {
//            if (!setDataCheck(i)) {
//                try { $n("f" + _I[i][0])[0].focus(); } catch (e) { }
//                alert(sMessage);
//                return;
//            }
//        }
//    }
    for (i = 0; i < _I.length; i++) {
        if (_I[i][2] == intCurrPage) {
            doLogic(i);
        }
    }

    $("page_" + intCurrPage).style.display = "none";
    switch (parseInt(intTargetPage)) {
        case 0:
            intCurrPage--;
            $n("submitbt").disabled = true;
            break;
        case -1:
            top.location.href = "../../toFinish.htm"
            break;
        case -2:
            $("submitbt").disabled = false;
            $("SurveyForm").submit();
            return;
            break;
        default:
            intCurrPage = intTargetPage;
            break;
    }
    if (intTargetPage == -1) { $("SurveyForm").submit(); init(); return; }

    if (sOverPage != "") {
        if (sOverPage.lastIndexOf(",") == 0) { sOverPage = sOverPage.substr(0, sOverPage.length - 1) }

        var arrOverPage = sOverPage.split(",");
        arrOverPage = intSort(arrOverPage);
        var ok = new Array();
        var result = arrOverPage;
        for (i = 0; i < arrOverPage.length; i++) {
            for (j = i + 1; j < arrOverPage.length; j++) {
                if (arrOverPage[i] == arrOverPage[j]) {
                    result[i] = null
                }
            }
        }
        var m = 0
        for (i = 0; i < result.length; i++) {
            if (result[i] != null) {
                ok[m] = result[i]
                m++
            }
        }
        arrOverPage = ok;
        for (i = 0; i < arrOverPage.length; i++) {
            if (intCurrPage >= arrOverPage[i]) {
                if (intCurrPage == arrOverPage[i]) {
                    intCurrPage--;
                }
                else {
                    break;
                }
            }
        }
    }
    $("page_" + intCurrPage).style.display = "block";
    initSlide();
    setBT();
    scroll(0, 0);
    if (intCurrPage <= 1) { return; }
}


function doLogic(sn){
	if((_I[sn][4]>=7)||(_I[sn][4]<=3)){return;}
	if(_I[sn][5]==""){return;}		
	var intSelectValue = 0;
	var m;
	var obj = $n("f"+_I[sn][0]);		
	for(m=0; m<obj.length; m++){		
		if(obj[m].checked){
			intSelectValue =obj[m].value;
			break;
		}
	}			
	if(intSelectValue==0){return;}	
	var sLogic = "";
	var arrLogic = _I[sn][5].split("|");	
	for(m=0;m<arrLogic.length;m++){		
		if(arrLogic[m].indexOf("Select:"+intSelectValue+":")>=0){			
			sLogic = arrLogic[m];
			break;
		}		
	}
	if(sLogic==""){return;}	
	arrLogic = sLogic.split(":");	
	intTargetPage = arrLogic[2].substr("ToPage>".length);
	sOverPage += arrLogic[3].substr("OverPage>".length)+";";	
}


function checkform(){
	if(intCurrPage>intpageamount){return false;}		
	intTargetPage = 0;
	var i,j,m;
	for(i=0;i<_I.length; i++){
		if((_I[i][2]==intCurrPage)&&(_I[i][6]==0)){
			if(!setDataCheck(i)){
				try{$n("f"+_I[i][0])[0].focus();}catch(e){}
				alert(sMessage);
				return false;
			}			
		}
	}
	
	for(i=0;i<_I.length;i++){if(_I[i][2]==intCurrPage){doLogic(i);}}	
	$("page_"+intCurrPage).style.display = "none";
	switch(intTargetPage){
		case 0:		
			if(blnCheckCode){
				if($("CheckCode").value==""){
					alert(_L[5]);
					$("CheckCode").focus();
					return false;
				}
			}
			if(blnAnswerPSW){
				if($("AnswerPSW").value==""){
					alert(_L[6]);
					$("AnswerPSW").focus();
					return false;
				}
			}
			statPoint();
			$("submitbt").disabled = true;	
			return true;	
			break;
		case -1:
			CloseWin();
			break;
		case -2:
			if(blnCheckCode){
				if($("CheckCode").value==""){
					alert(_L[5]);
					$("CheckCode").focus();
					return false;
				}
			}
			if(blnAnswerPSW){
				if($("AnswerPSW").value==""){
					alert(_L[5]);
					$("AnswerPSW").focus();
					return false;
				}
			}
			$("submitbt").disabled = true;	
			statPoint();
			break;
		default:intCurrPage = intTargetPage;break;
			
	}
	if(intTargetPage==-1){$("SurveyForm").submit();init();return false;}
	if(sOverPage!=""){		
		if(sOverPage.lastIndexOf(",")==0){
			sOverPage = sOverPage.substr(0,sOverPage.length-1)
		}
		var arrOverPage = sOverPage.split(",");
		arrOverPage = intSort(arrOverPage);
		var ok= new Array();
		var result = arrOverPage;		
		for(i=0; i<arrOverPage.length; i++){
			for(j=i+1; j<arrOverPage.length;j++){
				if(arrOverPage[i]==arrOverPage[j]){
					result[i] = null;
				}
			}
		}
		m  = 0;
		for(i=0;i<result.length;i++){
			if(result[i]!=null){
				ok[m] = result[i];
				m++;
			}
		}
		arrOverPage = ok;
		for(i=0; i<arrOverPage.length; i++){
			if(intCurrPage<=arrOverPage[i]){
				if(intCurrPage==arrOverPage[i]){
					intCurrPage++;
				}
				else{break;}
			}
		}
	}	
	$("page_"+intCurrPage).style.display = "block";	
	return false;
}

function filterHiddenCheck(sInput,sCheckTarget){
	if(sInput.indexOf(";"+sCheckTarget+";")>=0){return true;}else{return false;}
}

function setDataCheck(sn){	
	var sCheckItem = "Empty|PostCode|IDCard|Data|Mob|Email|En|Cn|URL|MaxValue|MaxValue|MinValue|MinSelect|MaxSelect|MinTickOff";
	var arrCheckItem =  sCheckItem.split("|");
	var sCheckStr = _I[sn][3];
	var sReg = /[0-9]{1,}/;	
	var arr; 
	var intCount = 0;
	var arrCheck = sCheckStr.split("|")
	var arrCheckValue = new Array();
	var sTemp = "";
	var intMinLen = 0;
	var intMaxLen = 50;
	for(j=0; j<arrCheck.length;j++){
		arr = sReg.exec(arrCheck[j])
		if(arr!=null){
			arrCheckValue[intCount] = arr;
			sTemp = arrCheck[j].substr(0,arrCheck[j].length-arr[0].length)
		}
		else{
			arrCheckValue[intCount] = -1;
		}
		intCount++;			
	}
	var obj;
	var value;
	var blnSelected = false;	
	try{if(filterHiddenCheck(";"+sHiddenItem+";",_I[sn][0])){return true;}}catch(e){}

	switch(_I[sn][4]){
		case 1:
			if(_I[sn][6]> 0){return true;}
			obj = $("f"+_I[sn][0]);
			value = obj.value;
			if(sCheckStr.indexOf("Empty1")>=0){				
				if(value==""){
					sMessage = _I[sn][1]+"\n"+_L[6];//输入不能为空
					return false;
				}
			}			
			if(sCheckStr.indexOf("PostCode1")>=0){				
				if(!sRegExp_PostCode.test(value)){//不是合法的邮编格式
					sMessage = _I[sn][1]+"\n"+_L[7];
					return false;
				}
			}
			if(sCheckStr.indexOf("Data1")>=0){if(!sRegExp_Date.test(value)){sMessage = _I[sn][1]+"\n"+_L[8];	return false;}}			
			if(sCheckStr.indexOf("IDCard1")>=0){
				if(!sRegExp_IDCard15.test(value)){
					if(!sRegExp_IDCard18.test(value)){
						if(!sRegExp_IDCard17X.test(value)){
							sMessage = _I[sn][1]+"\n"+_L[9];//不是合法的身份证地址
							return false;
						}
					}
				}
			}			
			if(sCheckStr.indexOf("Mob1")>=0){				
				if(!sRegExp_Mob.test(value)){
					sMessage = _I[sn][1]+"\n"+_L[10];//不是合法的手机号
					return false;
				}
			}			
			if(sCheckStr.indexOf("En1")>=0){				
				if(!sRegExp_En.test(value)){
					sMessage = _I[sn][1]+"\n"+_L[11];//必须为英文
					return false;
				}
			}			
			if(sCheckStr.indexOf("Cn1")>=0){				
				if(sRegExp_Cn.test(value)){
					sMessage = _I[sn][1]+"\n"+_L[12];//必须为汉字
					return false;
				}
			}			
			if(sCheckStr.indexOf("Email1")>=0){				
				if(!sRegExp_Email.test(value)){
					sMessage = _I[sn][1]+"\n"+_L[13];//不是合法的Email地址
					return false;
				}
			}			
			return true;
			break;
		case 2:			
			obj = $("f"+_I[sn][0]);
			value = obj.value;
			if(sCheckStr.indexOf("Empty1")>=0){				
				if(value==""){sMessage = _I[sn][1]+"\n"+_L[6];return false;}
			}
			var vMaxValue = getCheckValue(sCheckStr,"MaxValue")
			var vMinValue = getCheckValue(sCheckStr,"MinValue")
			if(value!=""){
				if(!sRegExp_Int.test(value)){
					sMessage = _I[sn][1]+"\n"+_L[14];//必须为整数
					return false;
				}
			}
			if(vMinValue!=""){
				vMinValue = parseFloat(vMinValue);
				if(value!=""){
					if(parseInt(value)<vMinValue){
						sMessage = _I[sn][1]+"\n"+_L[15].replace("<$MaxValue$>",vMinValue);//不能小于vMinValue
						return false;
					}
				}
			}
			if(vMaxValue!=""){
				vMaxValue = parseFloat(vMaxValue);
				if(value!=""){
					if(parseInt(value)>vMaxValue){
						sMessage = _I[sn][1]+"\n"+_L[16].replace("<$MinValue$>",vMaxValue);//不能大于vMaxValue
						return false;
					}
				}
			}
			break;
		case 3:
			if(_I[sn][6]>0){return true;}
			if(sCheckStr.indexOf("Empty1")>=0){				
				if($("f"+_I[sn][0]).value==""){
					sMessage = _I[sn][1]+"\n"+_L[6];//不能为空
					return false;
				}
			}
			break;
		case 4:case 5:case 11:
			if(sCheckStr.indexOf("Empty1")>=0){	
				for(n=0; n<$n("f"+_I[sn][0]).length;n++){
					if($n("f"+_I[sn][0])[n].checked){
						return true;
					}
				}
				sMessage = _I[sn][1]+"\n"+_L[17];//请选择
				return false;
			}
			break;
		case 6:			
			if(sCheckStr.indexOf("Empty1")>=0){	
				if($("f"+_I[sn][0]).selectedIndex==0){
					sMessage = _I[sn][1]+"\n"+_L[17]; //请选择
					return false;
				}
			}			
			return true;
			break;
		case 7:
		case 19:
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			var intTemp = 0;
			var m;
			for(m=0; m<_I.length; m++){
				blnSelected = false;
				if(_I[m][6]==_I[sn][0]){
					for(n=0; n<$n("f"+_I[m][0]).length;n++){						
						if($n("f"+_I[m][0])[n].checked){
							blnSelected = true;
							break;
						}
					}
					intTemp++;
					if(!blnSelected){						
						sMessage = _I[m][1]+"\n"+_L[17];		//请选择			
						return false;
					}
				}
			}
			break;			
		case 15:
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			var intTemp = 0;
			var m;
			for(m=0; m<_I.length; m++){
				blnSelected = false;
				if(_I[m][6]==_I[sn][0]){
					
					for(n=0; n<$n("f"+_I[m][0]).length;n++){						
						if($n("f"+_I[m][0])[n].checked){
							blnSelected = true;
							break;
						}
					}
					intTemp++;
					if(!blnSelected){						
						sMessage = _I[m][1]+"\n"+_L[17];	//请选择				
						return false;
					}
					
				}
			}
			break;	
		case 16:
		case 20:
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			var m;
			for(m=0; m<_I.length; m++){				
				if(_I[m][6]==_I[sn][0]){					
					for(n=0; n<$n("f"+_I[m][0]).length;n++){						
						if($n("f"+_I[m][0])[n].value==""){
							sMessage = _I[m][1]+"\n"+_L[17];	//请选择					
							return false;
						}
					}
				}
			}
			break;	
		case 8:case 9:
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			var intMaxSelect = getCheckValue(sCheckStr,"MaxSelect");
			var intMinSelect = getCheckValue(sCheckStr,"MinSelect");
			var intSelectAmount = 0;				
			for(m=0;m<$n("f"+_I[sn][0]).length;m++){if($n("f"+_I[sn][0])[m].checked){intSelectAmount++;}}if(intSelectAmount==0){sMessage = _I[sn][1]+"\n"+_L[17];return false;}

			if(intMaxSelect!=""){if(intMaxSelect<intSelectAmount){sMessage = _I[sn][1]+"\n"+_L[19].replace("<$MaxSelectNum$>",intMaxSelect);return false;}}						
			if(intMinSelect!=""){if(intMinSelect>intSelectAmount){sMessage = _I[sn][1]+"\n"+_L[20].replace("<$MinSelectNum$>",intMinSelect);return false;}}			
			break;			
		case 10:			
			var intMaxSelect = getCheckValue(sCheckStr,"MaxSelect");
			var intMinSelect = getCheckValue(sCheckStr,"MinSelect");
			var intSelectAmount = 0;
			for(m=0; m<$("f"+_I[sn][0]).length; m++){					
				if($("f"+_I[sn][0])[m].selected){
					intSelectAmount++;
				}
			}			
			if(sCheckStr.indexOf("Empty1")>=0){	
				if(intSelectAmount==0){
					sMessage = _I[sn][1]+"\n"+_L[17];//请选择
					return false;
				}
			}			
			if(intMaxSelect!=""){
				if(intMaxSelect<intSelectAmount){
					sMessage = _I[sn][1]+"\n"+_L[19].replace("<$MaxSelectNum$>",intMaxSelect);//最多选择
					return false;
				}
			}			
			if(intMinSelect!=""){
				if(intMinSelect>intSelectAmount){
					sMessage = _I[sn][1]+"\n"+_L[20].replace("<$MinSelectNum$>",intMinSelect);//最少选择
					return false;
				}
			}			
			return true;break;
		case 12:			
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			var intInputValue;
			var sInputValue = "";
			for(m=0; m<_I[sn][7];m++){
				intInputValue = $("f"+_I[sn][0]+"_"+m).value;				
				if(!sRegExp_Int.test(intInputValue)){
					sMessage = _L[24].replace("<$StartNum$>","1");
					sMessage = _I[sn][1]+"\n"+sMessage.replace("<$EndNum$>",_I[sn][7])//必须输入<$StartNum$>到<$EndNum$>之间的整数
					return false;
				}
				if(intInputValue>_I[sn][7]){
					sMessage = _I[sn][1]+"\n"+_L[22].replace("<$MaxValue$>",_I[sn][7]);//输入不能大于<$MaxValue$>
					return false;
				}
				if(intInputValue<1){
					sMessage = _I[sn][1]+"\n"+_L[23].replace("<$MinValue$>","1");	//输入不能小于<$MinValue$>		
					return false;
				}
				sInputValue += intInputValue+";";				
			}			
			sInputValue = sInputValue.substr(0,sInputValue.length-1);			
			var arrInputValue = sInputValue.split(";");
			arrInputValue = intSort(arrInputValue);			
			for(m=0;m<(arrInputValue.length-1); m++){
				if(parseInt(arrInputValue[m])+1!=parseInt(arrInputValue[m+1])){				
				    sMessage = _L[24].replace("<$StartNum$>","1");//必须输入<$StartNum$>到<$EndNum$>之间的整数,且输入值不能相同
					sMessage = _I[sn][1]+"\n"+sMessage.replace("<$EndNum$>",_I[sn][7])	
					return false;
				}
			}
			break;		
		case 13:		
			var intMinTickOff = getCheckValue(sCheckStr,"MinTickOff");
			var intMaxTickOff = getCheckValue(sCheckStr,"MaxTickOff");
			var intInputAmount = 0;
			for(m=0; m<intMaxTickOff;m++){
				if($("f"+_I[sn][0]+"_"+m).value!=""){
					intInputAmount++;
				}
			}
			if(sCheckStr.indexOf("Empty1")>=0){	
				if(intInputAmount==0){
					sMessage = _I[sn][1]+"\n"+_L[18];//请输入
					return false;
				}
			}			
			if(intMinSelect!=""){
				if(intMinTickOff>intInputAmount){
					sMessage = _I[sn][1]+"\n"+_L[25].replace("5",intMinTickOff);//至少需要列举5项
					return false;
				}
			}			
			return true;break;
		case 17:
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			value=$("f"+_I[sn][0]).value;
			if(value==""){
				sMessage = _I[sn][1]+"\n"+_L[26].replace("<$BrowseButton$>",_L[27]);	//请点击'<$BrowseButton$>'按钮进行上传!			
				return false;	
			}
			if(sCheckStr.indexOf("|UploadMode|")>0){}
			else{
				var sFileType = ("."+sCheckStr.substring(sCheckStr.indexOf("|FileType")+"|FileType".length)+".").toLowerCase();	
				value = value.substring(value.lastIndexOf(".")).toLowerCase();
				if(sCheckStr.indexOf("|UploadMode0|")>=0){//白名单
					if(sFileType.indexOf(value+".")<0){
						sMessage = _I[sn][1]+"\n"+_L[28].replace("<$FileType$>",value);	//不允许上传'<$FileType$>'类型的文件";传! _L[28]
						return false;
					}
				}
				else{
					if(sFileType.indexOf(value+".")>0){
						sMessage = _I[sn][1]+"\n"+_L[28].replace("<$FileType$>",value);						
						return false;
					}				
				}
			}	
			break;		
		case 18://矩阵下拉
			if(sCheckStr.indexOf("Empty1")<0){return true;}
			for(var i1=0;i1<_I.length;i1++){
				if(_I[i1][6]==_I[sn][0]){
					for(var i2=0;i2<_O.length;i2++)	{
						if((_O[i2][2]==_I[sn][0])&&(_O[i2][5]=="True")){	
							if($("f"+_I[i1][0]+"_"+_O[i2][0]).options[$("f"+_I[i1][0]+"_"+_O[i2][0]).selectedIndex].value==""){
								sMessage = _I[sn][1]+":"+_I[i1][1]+">"+_O[i2][4]+"\n"+_L[17];//请选择
								$("f"+_I[i1][0]+"_"+_O[i2][0]).focus();
								return false;	
							}
						}
					}
				}
			}
	
			break;
		case 21://百分比题
			var intMinPercent =  parseInt( getCheckValue(sCheckStr,"MinPercent"));
			var intMaxPercent =  parseInt( getCheckValue(sCheckStr,"MaxPercent"));
			var intTotalPerent =  parseInt( getCheckValue(sCheckStr,"TotalPerent"));
			var intTotal = 0;
			for(var i1=0;i1<_I.length;i1++){
				if(_I[i1][6]==_I[sn][0]){
					value = $("f"+_I[i1][0]).value;
					if(!sRegExp_Int.test(value)){
						sMessage = _I[sn][1]+":"+_I[i1][1]+"\n"+_L[14];
						$("f"+_I[i1][0]).focus();
						return false;
					}
					else{
						value = parseInt(value);
						intTotal+=value;
						if(value<intMinPercent){				
							sMessage = _I[sn][1]+":"+_I[i1][1]+"\n"+_L[15].replace("<$MaxValue$>",intMinPercent);
							$("f"+_I[i1][0]).focus();
							return false;	
						}
						else{
							if(value>intMaxPercent){				
								sMessage = _I[sn][1]+":"+_I[i1][1]+"\n"+_L[16].replace("<$MinValue$>",intMaxPercent);
								$("f"+_I[i1][0]).focus();
								return false;	
							}
							
						}
					}
				}
			}
		
			if(parseInt(intTotal)!=intTotalPerent){
				sMessage = _I[sn][1]+"\n"+_L[29].replace("<$TotalPerent$>",intTotalPerent);
				return false;
			}
			break;
		case 14:return true;break;
		default :return false;break;
	}
	return true;
}


function getCheckValue(sInput,sCheckItem){
	var s = sInput.substr(sInput.indexOf(sCheckItem))
	var intPosi = s.indexOf("|");
	if(intPosi>=0){
		s = s.substring(0,intPosi);
	}	
	s = s.substr(sCheckItem.length);	
	return s;	
}


function statPoint(){
	var intAllPoint = 0;
	var bln = false;
	for(i=0; i<_I.length; i++){		
		switch(_I[i][4]){
			case 4:case 5:intAllPoint += getPoint(getSingleValue($n("f"+_I[i][0])));break;
			case 6:intAllPoint += getMulitPoint($("f"+_I[i][0]),"Select");break;
			case 8:intAllPoint += getMulitPoint($n("f"+_I[i][0]),"CheckBox");break;
			case 9:intAllPoint += getMulitPoint($n("f"+_I[i][0]),"CheckBox");break;
			case 10:intAllPoint += getMulitPoint($n("f"+_I[i][0]),"Select");break;
			case 11:intAllPoint += getPoint(getSingleValue($n("f"+_I[i][0])));break;			
		}	
	}
	$("Point").value = intAllPoint;	
}

function getSingleValue(inputObj){	
	for(var m=0;m<inputObj.length;m++){if(inputObj[m].checked)	{return inputObj[m].value;}}	
	return 0;
}

function getMulitPoint(inputObj,objType){
	var intResult = 0;
	var m=0;
	if(objType=="Select"){
		for(mm=0;mm<inputObj.selected;mm++){
			if(inputObj.options[mm].selected)	{
				intResult += getPoint(inputObj.options[mm].value);
			}
		}
	}
	else{
		for(mm=0;mm<inputObj.length;mm++){
			if(inputObj[mm].checked)	{
				intResult += getPoint(inputObj[mm].value);				
			}			
		}	
	}
	return intResult;
}


function getPoint(inputOID){
	inputOID = parseInt(inputOID);
	for(var m=0; m<_O.length; m++){if(_O[m][0]==inputOID){return _O[m][1];	}}
	return 0;
}

function advDrop(ev){
	ev = ev || window.event;
	var target = ev.target || ev.srcElement;
	if(target.id==""){return;}
	var i = 0;
	var ID = parseInt(target.id.substring(1));	
	var intSelectedIndex = target.selectedIndex;
	var intSelectValue = parseInt(target.options[intSelectedIndex].value);
	var oOption = null;
	
	var intChildID = 0;
	for(i=0; i<_I.length;i++){
		if(_I[i][0]==ID){
			if(_I[i][8]>0){
				intChildID = _I[i][8]; 	
			}
			break;	
		}
	}

	if(intChildID==0){return;}	
	$("f"+intChildID).length=0;
	oOption = document.createElement("OPTION");
	oOption.text = _L[17];
	oOption.value = 0;
	$("f"+intChildID).options.add(oOption);
	for(i=0; i<_O.length;i++){
		if(_O[i][3]==intSelectValue){
			oOption = document.createElement("OPTION");
			oOption.text = _O[i][4];
			oOption.value = _O[i][0];
			$("f"+intChildID).options.add(oOption);
		}
	}
}


function MultiReject(o,SN,sOptionStr){
	var getIID = parseInt(o.id.substring(1,o.id.indexOf("_")));
	var	arrOptionStr = sOptionStr.split("|");
	var sReject = arrOptionStr[SN-1].substring(arrOptionStr[SN-1].indexOf(":")+1);
	var i,j;
	if(sReject!=""){
		var arrReject = sReject.split(",");	
		if(o.checked){
			for(i=0;i<arrReject.length;i++){
				$("f"+getIID+"_"+(arrReject[i]-1)).checked = false;
				$("f"+getIID+"_"+(arrReject[i]-1)).disabled = true;
			}
		}
		else{			
			for(i=0;i<arrReject.length;i++){$("f"+getIID+"_"+(arrReject[i]-1)).disabled = false;}
		}
	}
	for(i=0;i<arrOptionStr.length;i++){		
		for(j=0;j<arrOptionStr.length;j++){		
			if(   (arrOptionStr[i]+":").indexOf(":"+(j+1)+":") >0){
				if($("f"+getIID+"_"+i).checked){$("f"+getIID+"_"+j).disabled = true;}
			}
		}
	}
}


function ImgOption(o,i1,i2,SN){
	for(var i=i1;i<i2;i++){
		$("f"+o+"_"+i).checked = false;
 	  	$("Img_f"+o+"_"+i).className = $("Img_f"+o+"_"+i).className.replace("Color","Gray");
	}
	$("Img_f"+o+"_"+SN).className = $("Img_f"+o+"_"+SN).className.replace("Gray","Color");	
	$("f"+o+"_"+SN).checked = true;	
}

function ImgOptionMouse(o,intOptionAmount,SN,d){
	if(!$("f"+o+"_"+SN).checked){
		o = "Img_f"+o+"_"+SN;	
		$(o).className = (d==0)?$(o).className.replace("Gray","Color"): $(o).className.replace("Color","Gray");
	}

}

function initHiddenItem(sInput){
	if(sInput==""){return;}
	var arrInput = sInput.split(";");
	for(var i=0;i<arrInput.length;i++){try{$("Item"+arrInput[i]).style.display = "none";}catch(e){}}
}

function bindURLVar(){
	if(sURLVar==""){return;	
		
	}
	if(window.location.toString().split("?")<=1){
		return;	
	}
	var pagevar = window.location.toString().split("?")[1];
	var s = "";
	var arrV = pagevar.split("&");
	var arr = sURLVar.split(";");
	var IID = 0;
	var sValue = "";
	var i,j,m;
	for(i=0;i<arr.length;i++){
		for(j=0;j<arrV.length;j++){
			if(arr[i].split(":")[0]==arrV[j].split("=")[0]){
				switch(_I[htItem.items(arr[i].split(":")[1])][4]){
					case 1:
					case 2:
					case 3:
						$("f"+_I[htItem.items(arr[i].split(":")[1])][0]).value = arrV[j].split("=")[1];
						break;
					case 4:
					case 5:
						for(var m=0;m<$n("f"+_I[htItem.items(arr[i].split(":")[1])][0]).length;m++){
							if($n("f"+_I[htItem.items(arr[i].split(":")[1])][0])[m].value==arrV[j].split("=")[1])	{
								$n("f"+_I[htItem.items(arr[i].split(":")[1])][0])[m].checked=true;
								break;
							}
						}
						break;
				}
			}
		}
	}
}


function EliminateItem(obj,sInput){
	var currOID = $(obj).value;
	var currStr  = "";
	var arrInput = sInput.split("-");
	var sShow,sHidden = "";	
	var n;
	for(n=0;n<arrInput.length;n++){
		if((":"+arrInput[n]).indexOf(":"+currOID+":")==0){
			currStr = arrInput[n].substring(arrInput[n].indexOf(":")+1);
			break;
		}
	}	
	if(currStr==""){return;	}	
	if(currStr.indexOf("|")==0){//只有显示
		sShow = currStr.substring(1);
		var arrShow = sShow.split(";");
		for(n=0;n<arrShow.length;n++){
			$("Item"+arrShow[n]).style.display = "block";	
		}
	}
	else{
		var	arrShow = currStr.substring(currStr.indexOf("|")+1).split(";");
		var arrHidden = currStr.substring(0,currStr.indexOf("|")).split(";");
		for(n=0;n<arrShow.length;n++){						
			if(arrShow[n]!=""){$("Item"+arrShow[n]).style.display = "block";}
		}
		for(n=0;n<arrHidden.length;n++){
			
			if(arrHidden[n]!=""){$("Item"+arrHidden[n]).style.display = "none";}
		}
	}	
}



function addEvent(obj,sEvent,fn){if(obj.attachEvent){obj.attachEvent("on"+sEvent,fn)}else{obj.addEventListener(sEvent,fn,false);}}
function selectToInput(SelectObj,InputObj,ObjName){return function(){if($n(ObjName)[$n(ObjName).length-1].checked){$(InputObj).disabled=false;}else{$(InputObj).disabled=true;	}}}
function round(num1,num2){return Math.round (num1*Math.pow(10,num2))/Math.pow(10,num2);}//四舍五入
function CloseWin(){window.opener=null;window.open("","_self");window.close();}  //不提示关闭浏览器,只对IE有效

function intSort(arrInput){
    var intTemp = 0;
	var ii;
    for(ii=0;ii<arrInput.length;ii++){
        arrInput[ii] = parseInt(arrInput[ii]);
    }
    for(ii=0;ii<arrInput.length;ii++){
        for(jj=0;jj<arrInput.length;jj++){
            if(arrInput[ii]<arrInput[jj]){
                intTemp = arrInput[ii];
                arrInput[ii] = arrInput[jj];
                arrInput[jj] = intTemp;
            }
        }
    }
    return arrInput;
}

function drag(moveObj,refObj,targetObj){
	moveObj.style.top = (refObj.offsetTop+moveObj.offsetHeight)+"px";
	var oCenterWidth = Math.floor(moveObj.offsetWidth/2);
	var intScale = refObj.offsetWidth/(targetObj.length-1)
	var refObjoPosi = getPosition(refObj);
	moveObj.style.position = "absolute";
	moveObj.style.left = (refObjoPosi.left-oCenterWidth)+"px" ;
	moveObj.style.top = (refObjoPosi.top+moveObj.offsetHeight-12)+"px";
	
	moveObj.style.cursor = "w-resize";
	var intPosiLeft = refObjoPosi.left-oCenterWidth;
	var intPosiRight = refObjoPosi.left+refObj.offsetWidth-oCenterWidth;
	refObj.onmousedown = function (a){return false};
	moveObj.onmousedown=function(){
		var d=document;
		var a = arguments[0]||window.event;
		var x=a.layerX?a.layerX:a.offsetX,y=a.layerY?a.layerY:a.offsetY;
		if(moveObj.setCapture){moveObj.setCapture();}else if(window.addEventListener){window.addEventListener};
		d.onmousemove=function(a){
			a = arguments[0]||window.event;
			if(!a.pageX)a.pageX=a.clientX;
			if(!a.pageY)a.pageY=a.clientY;
			var tx=a.pageX-x,ty=a.pageY-y;
			tx = tx>intPosiRight?intPosiRight:tx;
			tx = tx<intPosiLeft?intPosiLeft:tx;
			moveObj.style.left=tx+"px";
		}
		d.onmouseup=function(){
			if(moveObj.releaseCapture)moveObj.releaseCapture();
			else if(window.addEventListener)window.addEventListener;
			d.onmousemove=null;
			d.onmouseup=null;
			var intMovePosi = moveObj.offsetLeft-intPosiLeft;
			var intTemp = round(intMovePosi/intScale,0);
			moveObj.style.left = (intPosiLeft+intTemp*intScale)+"px";	
			targetObj[intTemp].checked = true;
		}
	}
}

function initSlide(){
	for(var i=0;i<_I.length;i++){
		if(_I[i][4]==11){
			if(_I[i][9]=="6"){
					drag($("AdjustBt"+_I[i][0]),$("Rule"+_I[i][0]),$n("f"+_I[i][0]));
			}	
		}
	}
}

function initObj(){
	var j =0;
	for(var i=0;i<_I.length;i++){
		switch(_I[i][4]){
			case 4:
			case 5:			
				if((",1,2,3,4,5,").indexOf(","+_I[i][9]+",")>=0){
					for(j=0;j<_I[i][7];j++){
						$('Img_f'+_I[i][0]+'_'+j).onclick= new Function ('ImgOption('+_I[i][0]+',0,'+_I[i][7]+','+j+')');						
						$('Img_f'+_I[i][0]+'_'+j).onmouseover= new Function ('ImgOptionMouse('+_I[i][0]+','+_I[i][7]+','+j+',0)');
						$('Img_f'+_I[i][0]+'_'+j).onmouseout= new Function ('ImgOptionMouse('+_I[i][0]+','+_I[i][7]+','+j+',1)');
					}
				}
				else if(_I[i][9]=="6"){
					drag($("AdjustBt"+_I[i][0]),$("Rule"+_I[i][0]),$n("f"+_I[i][0]));
				}				
				if(_I[i][4]==5){
					for(j=0;j<_I[i][7];j++){
						addEvent($("f"+_I[i][0]+"_"+j),"click",selectToInput("f"+_I[i][0]+"_"+j,"f"+_I[i][0]+"_Input","f"+_I[i][0]));
					}
				}				
				if(_I[i][10]!=""){
					for(j=0;j<_I[i][7];j++){
						$("f"+_I[i][0]+"_"+j).onclick= new Function ('EliminateItem(this.id,"' + _I[i][10] + '")');
					}
				}				
				break;
			case 6:
				$("f"+_I[i][0]).onchange = advDrop;
				break;
			case 11:
				if((",1,2,3,4,5,").indexOf(","+_I[i][9]+",")>=0){
					for(j=1;j<=_I[i][7];j++){
						$('Img_f'+_I[i][0]+'_'+j).onclick= new Function ('ImgOption('+_I[i][0]+',1,'+(_I[i][7]+1)+','+j+')');						
						$('Img_f'+_I[i][0]+'_'+j).onmouseover= new Function ('ImgOptionMouse('+_I[i][0]+','+_I[i][7]+','+j+',0)');
						$('Img_f'+_I[i][0]+'_'+j).onmouseout= new Function ('ImgOptionMouse('+_I[i][0]+','+_I[i][7]+','+j+',1)');
					}
				}
				else if(_I[i][9]=="6"){
					drag($("AdjustBt"+_I[i][0]),$("Rule"+_I[i][0]),$n("f"+_I[i][0]));
				}
				break;
			case 8:
			case 9:
				if(_I[i][10]!=""){
					for(j=0;j<_I[i][7];j++){
						$("f"+_I[i][0]+"_"+j).onclick= new Function ('MultiReject(this,'+(j+1)+',"' + _I[i][10] + '")');
					}
				}
				if(_I[i][4]==9){
					for(j=0;j<_I[i][7];j++){
						addEvent($("f"+_I[i][0]+"_"+j),"click",selectToInput("f"+_I[i][0]+"_"+j,"f"+_I[i][0]+"_Input","f"+_I[i][0]));
					}
				}
				break;			
		}
	}
}

function getPosition(obj) {
    var top=0;
    var left=0;
    var width=obj.offsetWidth;
    var height=obj.offsetHeight;
    while (obj.offsetParent) {
        top += obj.offsetTop;
        left += obj.offsetLeft;
        obj = obj.offsetParent;
    }
    return {"top":top,"left":left,"width":width,"height":height};
 }
 
 function Hashtable(){
    this._hash = new Object(); 
    this.add = function(key,value){
		if(typeof(key)!="undefined"){
			if(!this.contains(key)){
				this._hash[key]=typeof(value)=="undefined"?null:value;
				return true;
			} else {
				return false;
            }
        } else {
			return false;
		}
	}
    this.remove = function(key){delete this._hash[key];}
    this.count = function(){var i=0;for(var k in this._hash){i++;} return i;}
    this.items = function(key){if(typeof(this._hash[key])=="undefined"){return "";}else{return this._hash[key]}}
    this.contains = function(key){ return typeof(this._hash[key])!="undefined";}
    this.clear = function(){for(var k in this._hash){delete this._hash[k];}}
}
 
function initHT(arr){
	var ht = new Hashtable();
	for(var i=0;i<arr.length;i++){
		ht.add(arr[i][0],i);
	} 
	return ht;
}

function clearSelect(id){
	switch(_I[htItem.items(id)][4]){
		case 4:case 5:case 11:
			for(var i=0;i<$n("f"+id).length;i++){$n("f"+id)[i].checked = false;}
			break;
	}
}

function LoadJS(src){
	var headObj = document.getElementsByTagName("head")[0];
	srciptObj = document.createElement("script");
	srciptObj.language = "javaScript";
	srciptObj.type = "text/JavaScript";
	srciptObj.src = src;
	headObj.appendChild(srciptObj);
}
