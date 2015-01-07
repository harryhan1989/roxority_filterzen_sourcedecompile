function $(thisid){
	return	document.getElementById(thisid);
}

window.onload =function (){
	setWin(45,55);
	setPageList();	
	setDataCheck();		
	showObj();		
	if(sOrderModel=="0"){$("OrderModel1").checked = true}else{$("OrderModel2").checked = true}	
	var arrOtherProperty = sOtherProperty.split("|");
	if(arrOtherProperty.length>0){
		$("MinLevelName").value = arrOtherProperty[0]
		$("MaxLevelName").value = arrOtherProperty[1]
	}	
	try{$("UseOptionIMG"+intOptionImg).checked = true;}catch(e){}
	window.parent.parent.openEdit();
	window.parent.parent.addWidth(3);
}

function setPageList(){
	var obj;
	var objPageList = $("PageNo");
	for(i=0; i<intPageAmount; i++){
		obj = document.createElement("OPTION");
		obj.text = "第"+(i+1)+"页";
		obj.value = i+1;
		objPageList.options.add(obj);
	}
	objPageList.selectedIndex = intPageNo - 1;	
}

function setDataCheck(){	
	var sCheckItem = "Empty|PostCode|IDCard|Data|Mob|Email|En|Cn|URL|MaxValue|sMaxValue|MinValue|MinSelect|MaxSelect|MinTickOff||MaxFileLen|UploadMode|FileType"
	var arrCheckItem =  sCheckItem.split("|");
	var sReg = /[0-9]{1,}/;	
	var arr; 
	var intCount = 0;
	var arrCheck = sCheckStr.split("|")
	var arrCheckValue = new Array();
	var sTemp = "";
	for(i=0; i<arrCheck.length;i++){
		arr = sReg.exec(arrCheck[i]);
		if(arr!=null){
			arrCheckValue[intCount] = arr;
			sTemp = arrCheck[i].substr(0,arrCheck[i].length-arr[0].length)	;
			try{$(sTemp).checked = true;}catch(e){}			
			try{$(sTemp).value = arr[0];}catch(e){}		
		}
		else{arrCheckValue[intCount] = -1;}		
		intCount++;	
	}
	setDataCheck_For_FileUpload(sCheckStr);
}


function setDataCheck_For_FileUpload(sInput){
	var sTemp = sInput.substring(sInput.indexOf("MaxFileLen")+"MaxFileLen".length);
	sTemp = sTemp.substring(0,sTemp.indexOf("|"));
	$("MaxFileLen").value = sTemp;
	sTemp = sInput.substring(sInput.indexOf("FileType")+"FileType".length);
	$("FileType").value = sTemp;	
	sTemp = sInput.substring(sInput.indexOf("UploadMode")+"UploadMode".length);
	sTemp = sTemp.substring(0,sTemp.indexOf("|"));
	try{
	if(sTemp!=""){$("UploadMode"+sTemp).checked = true;}
	}
	catch(e){}
}

function showObj(){
	var arrObj = ""

	switch(intItemType){
			case 1:
				_LotSizeSA("7;14");
				break;
			case 2: 
				_LotSizeSA("9;14");
				break;
			case 3: 
			case 14: 
				_LotSizeSA("");
				break;
			case 4:
			    _LotSizeSA("3;5;6;14");	
				break;
			case 5:
			    _LotSizeSA("3;6;13;14");	
				break;
			case 6:
				_LotSizeSA("3;14");	
				break;
			case 7: 
			case 15: 
				_LotSizeSA("1;3;6;14");		
				break;
			case 16:
				_LotSizeSA("1;3;6;13;14");		
				break;
			case 8: 			
			case 10: 
				_LotSizeSA("3;8;14");		
				break;			
			case 9:
			    _LotSizeSA("3;6;13;14");	
				break;
			case 11: 
				_LotSizeSA("4;6;14");	
				break;
			case 12: 
				_LotSizeSA("3");
				break;
			case 13: 
				_LotSizeSA("0;10;14");
				break;
			case 17: 			
				_LotSizeSA("11;14");
				break;
			case 18: 			
				_LotSizeSA("1;2;3;6;14");
				break;
			case 19: 			
				_LotSizeSA("1;3;6;13;14");
				break;
			case 20: 			
				_LotSizeSA("1;3;6;13;14");	
				break;
			case 21: 			
				_LotSizeSA("1;12;14");	
				break;
			case 22:				
			    $("EditArea").style.display = "none";	
			    if(blnOpenItemLib==false){
			        $("ItemLib").src = "ItemLib.aspx";
			        blnOpenItemLib = true;
			    }			    
			    $("ItemLib").style.display = "block";				
			    break;
		}
}

