
window.onload = function(){
	
	try{setParentWinHeight();}	catch(e){}

	
}

function setParentWinHeightForObserver(){	
	if(document.body.offsetHeight<window.parent.document.getElementById("Main").offsetHeight){		
		try{window.parent.document.getElementById("targetWin").style.height = (window.parent.document.getElementById("Main").offsetHeight+10)+"px";}
		catch(e){
			
		}
	}
}

