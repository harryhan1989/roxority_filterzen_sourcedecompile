function setWin(intRw, intLw) {
    try{
		if((intRw=="")||(intLw=="")){
			intRw = 45
			intLw = 55
		}
        var intWinHeight = window.document.body.scrollHeight+200; 
		//intWinHeight = window.parent.document.body.scrollHeight-100;

		var sRigthMenuWidth = window.parent.document.getElementById("RightMenu").style.width;
		var intRightMenuWidth =  parseInt(sRigthMenuWidth.substring(0,sRigthMenuWidth.indexOf("%")));
		
		window.parent.document.getElementById("RightMenu").style.width = intRw.toString()+"%";
		window.parent.document.getElementById("LeftWin").style.width = intLw.toString()+"%";
		//window.parent.document.getElementById("SetWin").style.height = intWinHeight+'px';  
		
		if(intRightMenuWidth<=50){
			//window.parent.document.getElementById("Add").click();
			//window.parent.document.getElementById("Add").click();
			
		}
		}
		catch (err)
		{
		
		}
		//window.parent.document.getElementById("RightMenuContent").style.display = "block";
    }