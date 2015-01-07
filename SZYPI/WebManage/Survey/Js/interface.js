// JavaScript Document

function interface(){
	this._MaskStyleName = "";
	this._MaskObj = null;
	this._MaskLayerZIndex = 100;
	this._arrScreenInfo = new Array();
	this._IE = "";
	function getIE(){
		if (window.navigator.userAgent.indexOf("MSIE")>=1){
			this._IE = "IE";
		}
		else if (window.navigator.userAgent.indexOf("Firefox")>=1){
			this._IE="FF";
		}
		else{
			this._IE = "Other";	
		}
		return this._IE;
	}
	
	

	this._getScreenInfo = function(){			
		this._arrScreenInfo[0] = ["网页可见区域宽",document.body.clientWidth];
		this._arrScreenInfo[1] = ["网页可见区域高",document.body.clientHeight];
		this._arrScreenInfo[2] = ["网页可见区域宽 (包括边线和滚动条的宽)",document.body.offsetWidth];
		this._arrScreenInfo[3] = ["网页可见区域高 (包括边线的宽)",document.body.offsetHeight];
		this._arrScreenInfo[4] = ["网页正文全文宽",document.body.scrollWidth]; 
		this._arrScreenInfo[5] = ["网页正文全文高",document.body.scrollHeight]; 
		this._arrScreenInfo[6] = ["网页被卷去的高",document.body.scrollTop]; 
		this._arrScreenInfo[7] = ["网页被卷去的左",document.body.scrollLeft]; 
		this._arrScreenInfo[8] = ["网页正文部分上",window.screenTop]; 		
		this._arrScreenInfo[9] = ["网页正文部分左",window.screenLeft]; 
		this._arrScreenInfo[10] = ["屏幕分辨率的高",window.screen.height]; 
		this._arrScreenInfo[11] = ["屏幕分辨率的宽",window.screen.width]; 
		this._arrScreenInfo[12] = ["屏幕可用工作区高度",window.screen.availHeight]; 
		this._arrScreenInfo[13] = ["屏幕可用工作区宽度",window.screen.availWidth]; 
		this._arrScreenInfo[14] = ["你的屏幕设置是(位彩色)",window.screen.colorDepth];
		this._arrScreenInfo[15] = ["你的屏幕设置(像素/英寸)",window.screen.deviceXDPI];	
		return this._arrScreenInfo;
	}
	
	this._centerObj = function(obj,_X,_Y){
		var P = this._getShowSize();
		obj.style.left = (P.w+_X-parseInt(obj.style.width.replace("px","")))/2+"px";
		obj.style.top = (P.h+_Y-parseInt(obj.style.height.replace("px","")))/2+"px";
		
	}
	
	this._getShowSize = function(){
		//得到网页可见部分的高度
	
		var H = 0;
		var W = 0;
		if(getIE()=="IE"){
			H = document.body.parentElement.clientHeight;
			W = document.body.parentElement.clientWidth;
		}
		else{
			H = window.innerHeight;
			W = window.innerWidth;
		}
		return {w:W,h:H};
	}
	
	this._initMask = function(obj,sColor){
		if((typeof(obj)=="undefined")||(obj=="")){
			obj = this._MaskObj;
			
		}
		obj.className = this._MaskStyleName;
		obj.style.index = this._MaskLayerZIndex;
		obj.style.position = "absolute";		
		if(sColor!=""){
			obj.style.backgroundColor = sColor;	
		}
	}
	
	this._resizeMask = function(obj){
		var _P = this._getShowSize();
		obj.style.height = _P.h+"px";
		obj.style.width = _P.w + "px";
	}
	
	
	this._openMask = function(objMask){
		if((typeof(objMask)=="undefined")||(objMask=="")){
			objMask = this._MaskObj;			
		}	
		
	
		var P = this._getShowSize();
		objMask.style.left = "0px";
		objMask.style.top = "0px";
		objMask.style.width= P.w+"px";   
  		objMask.style.height= P.h+"px"; 
		objMask.style.display='block';
		
	}
	
	this._openObj = function(obj,_W,_H,_WW){
		
		
	
		var P = this._getShowSize();
		if(_WW>0){
			obj.style.width= _WW+"px";	
		}
		else{
			obj.style.width= P.w+_W+"px";   
		}
  		obj.style.height= P.h+_H+"px"; 
		
		
	}
	
	this._closeMask = function(objMask){
		if((typeof(objMask)=="undefined")||(objMask=="")){
			objMask = this._MaskObj;
			
		}
		objMask.style.display='none';
		
	}
	
	this._switchTarget = function(obj){
		if((obj.style.display=="")||(obj.style.display=="block")){
			obj.style.display = "none";
		}
		else{
			obj.style.display = "block";	
		}
	}
	
	this._setObjShow = function(obj,_W,_H,display,ZIndex){
		obj.style.width = _W+"px";
		obj.style.height = _H+"px";
		if(ZIndex!=""){
			obj.style.index = ZIndex;
		}
		if(display!=""){
			obj.style.display = display;
		}
	}
	
	this._closePop = function(obj,objMask){
		if((typeof(objMask)=="undefined")||(objMask=="")){
			objMask = this._MaskObj;			
		}	
		this._closeMask();
		obj.style.display = "none";
	}
	
	
}