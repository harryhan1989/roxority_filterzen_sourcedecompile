function SaveOption(){
		$("Memo").value = Editor.document.body.innerHTML		
		$("Save").disabled = true		
		$("myform").submit();
	}
	
	
	function saveSurvey(){
		$("Memo").value = _e._getHTMLContent();
		$("Save").disabled = true;		
		$("Active").checked = $("OpenSurvey").checked;	
		$("myform").submit();
		
	}
	
	
	function initForm(){
		$("SID").value = SID;				
	}

function cancel(){
	top.location.href='EditSurvey.aspx?SID='+SID;
}
	



  function   initFace(){
	var myface = new interface();
	var _P = myface._getShowSize();
	$("BgWin").style.display='block';
  	$("BgWin").style.width=_P.w+"px";   
  	$("BgWin").style.height=_P.h+"px";   
	
	
	$("OpenWin").style.display='block';
	$("OpenWin").style.left = (_P.w-540)/2+"px";
	$("OpenWin").style.top = (_P.h-220)/2+"px";   

	$("Layer1").style.left = (_P.w-400)/2+"px";   
	$("Layer1").style.top = (_P.h-230)/2+"px";   
	
	$("ResultWin").style.display='none';
	$("ResultWin").style.left = (_P.w-540)/2+"px";   
	$("ResultWin").style.top = (_P.h-220)/2+"px";   
	
	$("ContentEditArea").style.width =  _P.w*0.75+"px";
	$("ToolOptionArea").style.width =  "7px";
	$("ToolOptionArea").style.height = _P.h+"px";
	$("ExpandToolArea").style.width =  (_P.w - _P.w*0.75 - 7)-12+"px";
	$("ExpandToolArea").style.height = (_P.h - 2) + "px";
	$("PageStyleListWin").style.height = (_P.h)-20 - parseInt(document.getElementById("FormArea").style.height.replace("px", "")) - parseInt(document.getElementById("PageList").style.height.replace("px", "")) + "px";

  }

  function initFaceAfter() {
      var myface = new interface();
      var _P = myface._getShowSize();
      $("BgWin").style.display = 'block';
      $("BgWin").style.width = _P.w + "px";
      $("BgWin").style.height = _P.h + "px";


      $("OpenWin").style.display = 'block';
      $("OpenWin").style.left = (_P.w - 540) / 2 + "px";
      $("OpenWin").style.top = (_P.h - 220) / 2 + "px";

      $("Layer1").style.left = (_P.w - 400) / 2 + "px";
      $("Layer1").style.top = (_P.h - 230) / 2 + "px";

      $("ResultWin").style.display = 'none';
      $("ResultWin").style.left = (_P.w - 540) / 2 + "px";
      $("ResultWin").style.top = (_P.h - 220) / 2 + "px";

//      $("ContentEditArea").style.width = _P.w * 0.75 + "px";
//      $("ToolOptionArea").style.width = "7px";
//      $("ToolOptionArea").style.height = _P.h + "px";
//      $("ExpandToolArea").style.width = (_P.w - _P.w * 0.75 - 7) + "px";
//      $("ExpandToolArea").style.height = (_P.h - 2) + "px";
//      $("PageStyleListWin").style.height = (_P.h - 60) + "px";


  }  
  
  function   openEdit(){  
	$("OpenWin").style.display='none';
	$("BgWin").style.display='none';
	$("BgWin").style.width=document.body.clientWidth+"px";   
	$("BgWin").style.height=document.body.clientHeight+"px";   
	
	return   true;   
  }   
  
  function showResult(){
  	$("OpenWin").style.display='none';
	$("BgWin").style.width=document.body.clientWidth+"px";    
  	$("BgWin").style.height=document.body.clientHeight+"px";   
	$("ResultWin").style.display='block';
	$("BgWin").style.display='block';	
  }
  
  function calcelEdit(){
  	$("Layer1").style.display='none';
	$("BgWin").style.display='none';
  }
  
function pageNavigation(intCurrPage,intPageSize,intRecordCount){
	var sResult = "";
	var	intCurrPageAmount = Math.ceil(intRecordCount/intPageSize);
	for(var i=1;i<=intCurrPageAmount;i++){
		if(i!=intCurrPage){
			sResult += "<span class='NormalPageCSS' onclick='toPage("+i+",5,"+intRecordCount+")'>"+i+"</span>";
		}
		else{
			sResult += "<span class='CurrPageCSS' onclick='toPage("+i+",5,"+intRecordCount+")'>"+i+"</span>";	
		}
	}
	$("PageList").innerHTML  = sResult;
	
}

function toPage(intPageNo,intPageSize,intRecordCount){
	var sResult = "";
	var intStart = (intPageNo-1)*intPageSize;
	var intEnd = intStart+intPageSize;

	if(intEnd>intRecordCount){
		intEnd = intRecordCount;	
	}
	for(var i=intStart;i<intEnd;i++){
		
		sResult += "<div class='SingleTemplateBox' onclick=selectPageStyle("+arrPage[i][0]+",'Template/surveystyle/smallstyle/firstpage/"+arrPage[i][1]+".gif')>"+arrPage[i][2]+"<br /><img src='Template/surveystyle/smallstyle/firstpage/"+arrPage[i][1]+".gif'></div>";
	}

	$("PageStyleListWin").innerHTML = sResult;
	
	
	pageNavigation(intPageNo,5,intRecordCount);
}

function applyPage(sPageFileName){
	if(confirm("更换模板后需要重新定义报表!\n确定吗？")){
		window.parent.reloadEditor(sPageFileName);		
	}
}


var currPage = 0;
var currPageImg = "";

function resetPageStyle(){
	if(currPage==0){
		return;
	}
	var targetPage = "";
	for(i=0;i<arrPage.length;i++){
		if(arrPage[i][0]==currPage){
			targetPage = arrPage[i][1];
		}
	}	
	$("targetWin").src = "PageStyleChange.aspx?SID="+SID+"&P="+targetPage+"&"+escape(new   Date());
	$("Cancel").disabled="disabled";
	$("SaveStatus").style.display = "block";
}

function selectPageStyle(selectedPage,selectedPageImg){
	currPage = selectedPage;
	currPageImg = selectedPageImg;
	$("Layer1").style.display='block';
	$("BgWin").style.display='block';
	$("DemoImg").src = selectedPageImg;
}

var switchstatus = 0;

function setToolOptionArea(d){
	if(switchstatus==0){
		//SwitchBT
		if(d==0){//移入
			$("SwitchBT").className = "SwitchOpenHighBT";
		}
		else{
			$("SwitchBT").className = "SwitchOpenDarkBT";
		}
	}
	else{
		if(d==0){//移入
			$("SwitchBT").className = "SwitchCloseHighBT";
		}
		else{
			$("SwitchBT").className = "SwitchCloseDarkBT";
		}
	}
}

function switchToolOptionArea(){
	var myface = new interface();
	var _P = myface._getShowSize();
	if(switchstatus==0){//在打开状态
		switchstatus = 1;
		$("SwitchBT").className = "SwitchCloseDarkBT";
		$("ContentEditArea").style.width =  (_P.w-7)+"px";   
		$("ExpandToolArea").style.width = "0px";
		$("ToolOptionArea").style.width =  "7px";
		$("ExpandToolArea").style.display = "none";

	}
	else{
		switchstatus = 0;
		$("SwitchBT").className = "SwitchOpenDarkBT";
		$("ToolOptionArea").style.width =  "7px";
		$("ContentEditArea").style.width =  (_P.w*.75)+"px";  
		$("ExpandToolArea").style.width =  (_P.w*.25- 7)+"px";   
		$("ExpandToolArea").style.display = "block";
	}
}

window.onload = function() {
    toPage(1, 5, arrPage.length);
    _e = new OQSSEditorFF("MyEditor", "colorbox", "OQSSEditorMask", "_e", "getSurveyHTML.aspx?sid=" + SID, blnEditHTML);
    _e._initEditor();
    _e._initBT(_e);
    _e._setEditorContentWinSize(-1, -1, -2, -1);
    $("SID").value = SID;
    initFace();
//    hiddenScrollBar();
    document.getElementById("Box").style.height = document.activeElement.offsetHeight+"px";
}


