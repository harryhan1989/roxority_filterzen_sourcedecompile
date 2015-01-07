var blnAction = false
var arrStyle = new Array();
var currTargetObj,currSourceObj;
var ID = 0;
var intTimeIndex;

var arrObj = ["FrontColor","BackGroundColor","FontFamily","FontWeight","FontSize","FontStyle","TextAlign","LineHeight","underline","overline","line-through","Width","Height","Border","BorderWidth","BorderColor","BorderTop","BorderTopWidth","BorderTopColor","BorderBottom","BorderBottomWidth","BorderBottomColor","BorderLeft","BorderLeftWidth","BorderLeftColor","BorderRight","BorderRightWidth","BorderRightColor","Margin","MarginTop","MarginBottom","MarginLeft","MarginRight","Padding","PaddingTop","PaddingBottom","PaddingLeft","PaddingRight","Float","ClearFloat","OverFlow","BackGroundImage","BackGroundImageRepeat"];
		
var arrStyle = ["color","background-color","font-family","font-weight","font-size","font-style","text-align","line-height","underline","overline","line-through","width","height","border-style","border-width","border-color","border-top","border-top-width","border-top-color","border-bottom","border-bottom-width","border-bottom-color","border-left","border-left-width","border-left-color","border-right","border-right-width","border-right-color","margin","margin-top","margin-bottom","margin-left","margin-right","padding","padding-top","padding-bottom","padding-left","padding-right","float","clear","over-flow","background-image","background-repeat"];

var arrType = [1,1,2,2,0,2,2,0,3,3,3,0,0,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,4,2];

var htObj;
var htStyle;

window.onload = function(){
	htObj = new Hashtable();
	htStyle = new Hashtable();
	for(var i=0;i<arrObj.length;i++){
		htObj.add(arrObj[i],i);
		htStyle.add(arrStyle[i],i);
	}
	
	window.dhx_globalImgPath = "../survey/colorselect/codebase/imgs/";
	var z = new dhtmlXColorPicker("CPcont", false, true);
	z.init();
	z.setCustomColors("#ff00ff,#00aabb,#6633ff");
	z.setOnSelectHandler(fillColor);
	z.hideOnSelect(false);
	applyStyleToSurvey($("Result").value);
	window.parent.complateActionWin();
	var myface = new interface();
	var _P = myface._getShowSize();
	$("DemoArea").style.height = (_P.h-20)+"px";
	$("StyleLibWin").src = "StyleLibList.aspx?SID="+$("sid").value;
	$("StyleLibWin").style.height = (_P.h-48)+"px";
	if(ID==1){
		updateParentStyle($("Result").value);	
	}
}

function fillColor(color) {
   if(currTargetObj!=null){
		currTargetObj.value = color;   
		currTargetObj.style.backgroundColor = color;
		currSourceObj.style.backgroundColor = color;
   }
}


function updateParentStyle(sInput){
	if(sInput==""){
			return;	
		}
		var arr = sInput.split("\r\n");
		var sClassName = "";
		var sStyleValue = "";
		var j=0;

		for(var i=0;i<arr.length;i++){
			if(arr[i]!=""){
				sClassName = arr[i].split("{")[0];
				sStyleValue = arr[i].substring(arr[i].indexOf("{")+1,arr[i].indexOf("}")-1);
				setObjStyle(sClassName,sStyleValue,window.parent.frames["targetWin"].document);	
				
				
			}
		}
}


function setObjStyle(sClassName,v,targetObj){
		if(targetObj==null){
			targetObj = window.frames["DemoArea"].document;
		}
		if(document.all){
			if(sClassName=="body"){
				sClassName = "BODY";	
			}
			for(var i=0;i<targetObj.styleSheets.length;i++){
				for(var j=0;j<targetObj.styleSheets[i].rules.length;j++)	{
					if(targetObj.styleSheets[i].rules[j].selectorText==sClassName){
						targetObj.styleSheets[i].rules[j].style.cssText = v;
					}
				}
			}
		}
		else{
			for(var i=0;i<targetObj.styleSheets.length;i++){
				for(var j=0;j<targetObj.styleSheets[i].cssRules.length;j++)	{
					if(targetObj.styleSheets[i].cssRules[j].selectorText==sClassName){
						targetObj.styleSheets[i].cssRules[j].style.cssText = v;
						
					}
				}
			}	
		}
	}
	
	function applyStyleToSurvey(sInput){
		if(sInput==""){
			return;	
		}
		var sTest = "";
		var arr = sInput.split("\r\n");
		var sClassName = "";
		var sStyleValue = "";
		var j=0;
		var obj = $("CSSObj");
		for(var i=0;i<arr.length;i++){
			if(arr[i]!=""){
				sClassName = arr[i].split("{")[0];
				sStyleValue = arr[i].substring(arr[i].indexOf("{")+1,arr[i].indexOf("}"));
				setObjStyle(sClassName,sStyleValue);	
				$(sClassName.replace(".","")+"_").value = sStyleValue;
				for(j=0;j<obj.length;j++){
					if(obj[j].value==sClassName.replace(".","")){
						obj[j].text = obj[j].text+"[已配置]";
						break;	
					}
				}
				
			}
		}
	}
	
	function checkForm(){
		var obj = $("CSSObj");
		var sResult = "";
		for(var i=1;i<obj.length;i++){
			if($(obj[i].value+"_").value!=""){
				if(obj[i].value!="body"){
					sResult +=  "."+obj[i].value+"{"+$(obj[i].value+"_").value+"}\r\n";
				}
				else{
					sResult +=  obj[i].value+"{"+$(obj[i].value+"_").value+"}\r\n";
				}
			}
			
		}
		$("Result").value = sResult;
		if($("SaveToLib").checked){
			if($("StyleLibName").value==""){
				alert("请输入样式名");
				$("StyleLibName").focus();
				return false;	
			}
		}
		
		
		if(sResult==""){
			return confirm("样式为空，这意味着清除所有样式，确定吗？");
		}
		updateParentStyle(sResult);
		
		return true;
		
	}
	
	
	function createStyle(){
		if($("CSSObj").selectedIndex<=0){
			$("Message").style.display = "block";
			$("Message").innerHTML = "<ul><li>需要先选择一个操作对象</li></ul>";
			setTimeout('clearMessage()',4000);
			$("CSSObj").focus();
			return;	
		}
		var sNumObj = "";
		var sMessage = "";
		var sStyle = "";
		if($("FrontColor").value!=""){
			sStyle += "color:"+$("FrontColor").value+";";
		}
		if($("BackGroundColor").value!=""){
			sStyle += "background-color:"+$("BackGroundColor").value+";";
		}
		if($("FontFamily").selectedIndex>0){
			sStyle += "font-family:"+$("FontFamily")[$("FontFamily").selectedIndex].value+";";
		}
		
		if($("FontWeight").selectedIndex>0){
			sStyle += "font-weight:"+$("FontWeight")[$("FontWeight").selectedIndex].value+";";
		}
		
		
		if($("FontSize").value!=""){
			sStyle += "font-size:"+$("FontSize").value+"px;";
			sNumObj += "FontSize;";
			sMessage += "大小;";
		}
		if($("FontStyle").selectedIndex>0){
			sStyle += "font-style:"+$("FontStyle")[$("FontStyle").selectedIndex].value+";";
		}
		if($("TextAlign").selectedIndex>0){
			sStyle += "text-align:"+$("TextAlign")[$("TextAlign").selectedIndex].value+";";
		}
		if($("LineHeight").value!=""){
			sStyle += "line-height:"+$("LineHeight").value+"px;";
			sNumObj += "LineHeight;";
			sMessage += "行高;";
		}
		
		if($("underline").checked){
			sStyle += "text-decoration:underline;";
		}
		if($("overline").checked){
			sStyle += "text-decoration:overline;";
		}
		if($("line-through").checked){
			sStyle += "text-decoration:line-through;";
		}
		
		
		if($("BackGroundImage").value!=""){
			sStyle += "background-image:url("+$("BackGroundImage").value+");";
			if($("BackGroundImageRepeat").selectedIndex>0){
				sStyle += "background-repeat:"+$("BackGroundImageRepeat")[$("BackGroundImageRepeat").selectedIndex].value+";";
			}
		}
		
		if($("Width").value!=""){
			sStyle += "width:"+$("Width").value+$("WidthUnit")[$("WidthUnit").selectedIndex].value+";";
			sNumObj += "Width;";sMessage += "宽度;";
		}
		if($("Height").value!=""){
			sStyle += "height:"+$("Height").value+"px;";
			sNumObj += "Height;";sMessage += "高度;";
		}
		
		if($("Border").selectedIndex>0){
			sStyle += "border-style:"+$("Border")[$("Border").selectedIndex].value+";";
		}
		if($("BorderWidth").value!=""){
			sStyle += "border-width:"+$("BorderWidth").value+"px;";
			sNumObj += "BorderWidth;";sMessage += "边框粗细;";
		}
		if($("BorderColor").value!=""){
			sStyle += "border-color:"+$("BorderColor").value+";";
		}
		
		if($("BorderTop").selectedIndex>0){
			sStyle += "border-top-style:"+$("BorderTop")[$("BorderTop").selectedIndex].value+";";
		}
		if($("BorderTopWidth").value!=""){
			sStyle += "border-top-width:"+$("BorderTopWidth").value+";";
			sNumObj += "BorderTopWidth;";sMessage += "上边框粗细;";
		}
		if($("BorderTopColor").value!=""){
			sStyle += "border-top-color:"+$("BorderTopColor").value+";";
		}
		
		if($("BorderBottom").selectedIndex>0){
			sStyle += "border-bottom-style:"+$("BorderBottom")[$("BorderBottom").selectedIndex].value+";";
		}
		if($("BorderBottomWidth").value!=""){
			sStyle += "border-bottom-width:"+$("BorderBottomWidth").value+";";
			sNumObj += "BorderBottomWidth;";sMessage += "下边框粗细;";
		}
		if($("BorderBottomColor").value!=""){
			sStyle += "border-bottom-color:"+$("BorderBottomColor").value+";";
		}
		
		if($("BorderLeft").selectedIndex>0){
			sStyle += "border-left-style:"+$("BorderLeft")[$("BorderLeft").selectedIndex].value+";";
		}
		if($("BorderLeftWidth").value!=""){
			sStyle += "border-left-width:"+$("BorderLeftWidth").value+";";
			sNumObj += "BorderLeftWidth;";sMessage += "左边框粗细;";
		}
		if($("BorderLeftColor").value!=""){
			sStyle += "border-left-color:"+$("BorderLeftColor").value+";";
		}
		
		if($("BorderRight").selectedIndex>0){
			sStyle += "border-right-style:"+$("BorderRight")[$("BorderRight").selectedIndex].value+";";
		}
		if($("BorderRightWidth").value!=""){
			sStyle += "border-right-width:"+$("BorderRightWidth").value+";";
			sNumObj += "BorderRightWidth;";
			sMessage += "右边框粗细;";
		}
		if($("BorderRightColor").value!=""){
			sStyle += "border-right-color:"+$("BorderRightColor").value+";";
		}
		
		if($("Margin").value!=""){
			sStyle += "margin:"+$("Margin").value+"px;";	
			sNumObj += "Margin;";sMessage += "外边距;";
		}
		if($("MarginTop").value!=""){
			sStyle += "margin-top:"+$("MarginTop").value+"px;";	
			sNumObj += "BorderRightWidth;";sMessage += "外上边距;";
		}
		if($("MarginBottom").value!=""){
			sStyle += "margin-bottom:"+$("MarginBottom").value+"px;";	
			sNumObj += "MarginBottom;";sMessage += "外下边距;";
		}
		if($("MarginLeft").value!=""){
			sStyle += "margin-left:"+$("MarginLeft").value+"px;";	
			sNumObj += "MarginLeft;";sMessage += "外左边距;";
		}
		
		if($("MarginRight").value!=""){
			sStyle += "margin-right:"+$("MarginRight").value+"px;";	
			sNumObj += "MarginRight;";sMessage += "外右边距;";
		}
		
		if($("Padding").value!=""){
			sStyle += "padding:"+$("Padding").value+"px;";	
			sNumObj += "Padding;";sMessage += "内边距;";
		}
		if($("PaddingTop").value!=""){
			sStyle += "padding-top:"+$("PaddingTop").value+"px;";	
			sNumObj += "PaddingTop;";sMessage += "内上边距;";
		}
		if($("PaddingBottom").value!=""){
			sStyle += "padding-bottom:"+$("PaddingBottom").value+"px;";	
			sNumObj += "PaddingBottom;";sMessage += "内下边距;";
		}
		if($("PaddingLeft").value!=""){
			sStyle += "padding-left:"+$("PaddingLeft").value+"px;";	
			sNumObj += "PaddingLeft;";sMessage += "内左边距;";
		}
		if($("PaddingRight").value!=""){
			sStyle += "padding-right:"+$("PaddingRight").value+"px;";	
			sNumObj += "PaddingRight;";sMessage += "内右边距;";
		}
		
		if($("Float").selectedIndex>0){
			sStyle += "float:"+$("Float")[$("Float").selectedIndex].value+";";
			
		}
		if($("ClearFloat").selectedIndex>0){
			sStyle += "clear:"+$("ClearFloat")[$("ClearFloat").selectedIndex].value+";";
		}
		if($("OverFlow").selectedIndex>0){
			sStyle += "over-flow:"+$("OverFlow")[$("OverFlow").selectedIndex].value+";";
		}
		
		var sMessage = checkInputError(sNumObj,sMessage);
		if(sMessage!=""){
			$("Message").style.display = "block";
			$("Message").innerHTML = sMessage;
			setTimeout('clearMessage()',4000);
			return;	
		}
		
		
		
		var selectTarget = $("CSSObj")[$("CSSObj").selectedIndex].value;
		if(selectTarget!="body"){
			selectTarget = "."+selectTarget;	
		}
		$(selectTarget.replace(".","")+"_").value = sStyle;		
		setObjStyle(selectTarget,sStyle)
		if($("CSSObj")[$("CSSObj").selectedIndex].text.indexOf("[已设置")<0){
			$("CSSObj")[$("CSSObj").selectedIndex].text =  $("CSSObj")[$("CSSObj").selectedIndex].text+"[已设置]";
		}
	}
	
	function checkInputError(sNumObj,sMessage){
		if(sNumObj==""){
			return "";	
		}
		var sResult = "";
		var arrNumObj = sNumObj.split(";");
		var arrMessage = sMessage.split(";");
		var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
		for(var i=0;i<arrNumObj.length-1;i++){
			if(!sRegExp_Int.test(  $(arrNumObj[i]).value  )){
				sResult += "<li>"+arrMessage[i]+"必须为整数</li>";	
			}
		}
		if(sResult!=""){
			sResult = "<ul>"+sResult+"</ul>";	
		}
		return sResult;
	}
	
	
	function testvar(){
		var obj = $("CSSObj");
		var sresult = "";
		for(var i=0;i<obj.length;i++){
			try{
			sresult += obj[i].value+"------"+$(obj[i].value+"_").value+"<HR color=red />"
			}
			catch(e){}
		}
		$("Message").innerHTML = sresult;
	}
	
	
function openColorSelectWin(objSource,objTarget){
	var _P = getPosi(objSource);
	currTargetObj = objTarget;
	currSourceObj = objSource;
	$("CPcont").style.left = (_P.intPosiLeft-objSource.offsetWidth-80)+"px";
	$("CPcont").style.top = (_P.intPosiTop+objSource.offsetHeight)+"px";
	if($("CPcont").style.display=="none")	{
		$("CPcont").style.display="block"
	}
	else{
		$("CPcont").style.display="none"
		
	}
}


function clearStyle(){
	$("Result").value = "";
	resetform();
	$("form2").submit();
}

function ExcludeOption(obj,obj1,obj2){
	obj1.checked = false;
	obj2.checked = false;
}

function fillStyleToForm(CSSV){
	resetform();
	if(CSSV==""){
		return;	
	}
	
	var arr = CSSV.split(";");
	var sCSSName = "";
	var sCSSValue = "";
	var intIndex = 0;
	for(var i=0;i<arr.length;i++){
		if(arr[i]!=""){
			
			sCSSName = arr[i].split(":")[0];
			sCSSValue = arr[i].split(":")[1];
			if(sCSSName!="text-decoration"){
				intIndex = htStyle.items(sCSSName);
			}
			else{
				intIndex = htStyle.items(sCSSValue);
			}
			switch(arrType[intIndex]){
				case 0:
					if(sCSSValue.indexOf("%")<0){
						$(arrObj[intIndex]).value = sCSSValue.replace("px","");
					}
					else{
						$(arrObj[intIndex]).value = sCSSValue.substring(0,sCSSValue.length-1);
						$("WidthUnit").selectedIndex=1;
					}
					break;
				case 1:
					$(arrObj[intIndex]).value = sCSSValue;
					$(arrObj[intIndex]).style.backgroundColor = sCSSValue;
					break;
				case 2:
					fillSelect($(arrObj[intIndex]),sCSSValue);
					break;
				case 3:
					$(sCSSValue).checked = true;
					break;
				case 4:
					$(arrObj[intIndex]).value = arr[i].substring(arr[i].indexOf("(")+1,arr[i].indexOf(")"));
					
					break;
			}
		}
	}
}

function fillSelect(obj,v){
	for(var i=0;i<obj.length;i++){
		if(obj[i].value==v)	{
			obj.selectedIndex = i;
			return;
		}
	}
}

function resetform(){
	$("form1").reset();
	var textobj = document.getElementsByTagName('input');
	for(var i=0; i<textobj.length; i++){
		if(textobj[i].type=="text"){
			textobj[i].style.backgroundColor = "#FFF";
		}
		
	}	
}

function clearMessage(){
	$("Message").innerHTML = "";
	$("Message").style.display = "none";
}

function saveToLib(){
		if($("SaveToLib").checked){
			$("StyleLibName").disabled = "";
			$("StyleLibName").value = "";
			$("StyleLibName").focus();
		}
		else{
			$("StyleLibName").disabled = "disabled";
			$("StyleLibName").value = "样式名";	
		}
		
	}
	
	
	function switchBT(obj){
		
		if(obj.id=="BT0"){
			$("DefineCSS").style.display = "block";
			$("StyleLib").style.display = "none";
			$("BT0").className = "ActiveBT";
			$("BT1").className = "NormalBT";
		}
		else{
			$("DefineCSS").style.display = "none";
			$("StyleLib").style.display = "block";	
			$("BT0").className = "NormalBT";
			$("BT1").className = "ActiveBT";
		}
	}
	
	


