function SetCwinHeight(o){

	if (document.getElementById){
   		if (o && !window.opera){
    		if (o.contentDocument && o.contentDocument.body.offsetHeight){
				o.height = parseInt(o.contentDocument.body.offsetHeight)+"px";
			}
			else if(o.Document && o.Document.body.scrollHeight){
				o.height = o.Document.body.scrollHeight+"px";
			}
		}
	}
}


function showIE(){
//alert(getClientHeight())
var  s = ""; 
s += "\r\n网页可见区域宽："+ document.body.clientWidth; 
s += "\r\n网页可见区域高："+ document.body.clientHeight; 
s += "\r\n网页可见区域宽："+ document.body.offsetWidth  +" (包括边线和滚动条的宽)"; 
s += "\r\n网页可见区域高："+ document.body.offsetHeight +" (包括边线的宽)"; 
s += "\r\n网页正文全文宽："+ document.body.scrollWidth; 
s += "\r\n网页正文全文高："+ document.body.scrollHeight; 
s += "\r\n网页被卷去的高："+ document.body.scrollTop; 
s += "\r\n网页被卷去的左："+ document.body.scrollLeft; 
s += "\r\n网页正文部分上："+ window.screenTop; 
s += "\r\n网页正文部分左："+ window.screenLeft; 
s += "\r\n屏幕分辨率的高："+ window.screen.height; 
s += "\r\n屏幕分辨率的宽："+ window.screen.width; 
s += "\r\n屏幕可用工作区高度："+ window.screen.availHeight; 
s += "\r\n屏幕可用工作区宽度："+ window.screen.availWidth; 
s += "\r\n你的屏幕设置是 "+ window.screen.colorDepth +" 位彩色"; 
s += "\r\n你的屏幕设置 "+ window.screen.deviceXDPI +" 像素/英寸"; 
//alert(s); 
}

var intPublicHeight = 104;

function setWorkAreaHeight(objTarget,v){
	var intTaskBarHeight = 54;
	if(window.screenTop){
		document.getElementById(objTarget).style.height = 	(window.screen.height-window.screenTop - intTaskBarHeight-v)+"px";
	}
	else{
		document.getElementById(objTarget).style.height = 	(window.screen.height-screenY - intTaskBarHeight-v)+"px";
	}
	

}

function setParentWinHeight(){	
	//if(parseInt(document.body.offsetHeight)<parseInt(window.parent.document.getElementById("Main").offsetHeight)){		
		//window.parent.document.getElementById("targetWin").style.height = window.parent.document.getElementById("Main").offsetHeight+"px";
		//alert("setParentWinHeight")
	//}
	try{
	    window.parent.document.getElementById("targetWin").style.height = window.parent.document.getElementById("Main").offsetHeight+"px";
	}
	catch (e) {
//	    document.getElementById("targetWin").style.height = window.parent.document.getElementById("Main").offsetHeight + "px";

	}

}

function $(sInput){
	return document.getElementById(sInput);
}
function $n(sInput){
	return document.getElementsByName(sInput);
}

function addEvent(obj,sEvent,fn){

	if(obj.attachEvent){
		obj.attachEvent("on"+sEvent,fn);
	}
	else{
		obj.addEventListener(sEvent,fn,false);
	}
}

function round(num1,num2){return Math.round (num1*Math.pow(10,num2))/Math.pow(10,num2);}//四舍五入
function formatPercent(num1,num2){
	var temp = round(num1,num2)*100;	
	return  round(temp,num2)+"%";
}

function ClientSetParentHeight(obj) {
	window.parent.document.getElementById("targetWin").style.height = document.body.offsetHeight + "px";

}

function getCurrDate(){
   var d, s = "";          
   d = new Date();                          
   s += (d.getMonth() + 1) + "/";            
   s += d.getDate() + "/";                   
   s += d.getYear();                         
   return(s);                               
}

function checknavigator(){
	var Sys = {};
	var sResult = "";
        var ua = navigator.userAgent.toLowerCase();
        var s;
        (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
        (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
        (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
        (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
        (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

        //以下进行测试
        if (Sys.ie) sResult = 'IE: ' + Sys.ie;
        if (Sys.firefox) sResult = 'Firefox: ' + Sys.firefox;
        if (Sys.chrome) sResult = 'Chrome: ' + Sys.chrome;
        if (Sys.opera) sResult = 'Opera: ' + Sys.opera;
        if (Sys.safari) sResult = 'Safari: ' + Sys.safari;
		return sResult;
}
function hiddenScrollBar(){
	document.documentElement.style.overflow="hidden";	
}

function share_switch(objTarget,objBT){
	if(objBT.className == "Switch_Open"){
		objBT.className = "Switch_Close";
		objTarget.style.display = "block";
	}
	else{
		objBT.className = "Switch_Open";
		objTarget.style.display = "none";
	}
	
}

function share_initWinHeight(obj){
		var myface = new interface();
		var _P = myface._getShowSize();
		obj.style.height = (_P.h-70)+"px";	
	}
	
function $(o$){try{return 	document.getElementById(o$)}catch(e){alert('获取对象错误')}}
function $n(o$){try{return 	document.getElementsByName(o$)}catch(e){alert('获取对象错误')}}

function getPosi(obj){
	var intTop = obj.offsetTop; 
	var intLeft = obj.offsetLeft; 
	while(obj = obj.offsetParent) { 
		intTop += obj.offsetTop;
		intLeft += obj.offsetLeft;
	} 
	return{intPosiTop:intTop,intPosiLeft:intLeft}
}
function getPosTop(obj) { 
	var l = obj.offsetTop; 
	while(obj = obj.offsetParent) { 
		l += obj.offsetTop; 
	} 
	return l; 
}

function getPosLeft(obj) { 
	var l = obj.offsetLeft; 
	while(obj = obj.offsetParent) { 
		l += obj.offsetLeft; 
	} 
	return l; 
}