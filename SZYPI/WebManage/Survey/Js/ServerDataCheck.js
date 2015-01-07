// JScript 文件
var blnOpenItemLib = false;
var arrOptionArea = new Array();
arrOptionArea[0] = "MaxTickoffBox";
arrOptionArea[1] = "SubItemBox";
arrOptionArea[2] = "MatrixRowColumnBox";
arrOptionArea[3] = "BackSelectBox";
arrOptionArea[4] = "LevelBox";
arrOptionArea[5] = "OptionImgArea";
arrOptionArea[6] = "BackSelectOptionModeAreaBox";
arrOptionArea[7] = "CheckBox_TextInput";
arrOptionArea[8] = "MinMaxSelectBox";
arrOptionArea[9] = "MinMaxValueBox";
arrOptionArea[10] = "MinMaxTickoffBox";
arrOptionArea[11] = "UploadOption";
arrOptionArea[12] = "PercentBox";
arrOptionArea[13] = "InputLenghBox";
arrOptionArea[14] = "CheckBox";



function $(thisid){
	return	document.getElementById(thisid);
}

function _LotSizeSA(sShowTarget){
	for(var arrIndex=0;arrIndex<arrOptionArea.length;arrIndex++){
		_sa(arrOptionArea[arrIndex],0);
	}
	if(sShowTarget==""){
		return;	
	}
	var arrShowTarget = sShowTarget.split(";");
	for(var arrIndex=0;arrIndex<arrShowTarget.length;arrIndex++){
		_sa(arrOptionArea[arrShowTarget[arrIndex]],1);
	}	
}

function _sa(sID,switchValue){//开关显示操作区,_sa switch area
	if(switchValue==0){$(sID).style.display = "none";}else{$(sID).style.display = "block";}
}

function setStyle(id,intItemType){
	 
	 	var obj = $(id);
		for(i=1; i<23; i++){
			$("T"+i).className = "ItemNormal";
		}
		$(id).className = "ItemDown";
		$("EditArea").style.display = "block";
		$("flag").value = intItemType;
		$("ItemLib").style.display = "none";	
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
			    _LotSizeSA("3;6;8;14");
			    break;			
			case 10: 
				_LotSizeSA("3;8;14");		
				break;			
			case 9: 
				_LotSizeSA("3;6;13;14");	
				break;
			case 11: 
				_LotSizeSA("4;5;6;14");	
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
		if(intItemType!=22){		
			$("ItemName").focus();			
		}
	 }
	 
	 function showForm(intItemType){
	 	var objItemName = $("ItemName");
	 	var objItemType = $("ItemType");
	 	var objMemo = $("Memo");
		var objSubItem = $("SubItem");
		var objBackSelect = $("BackSelect");
		var objEmpty = $("Empty");
		var objEmail = $("Email");
		var objPostCode = $("PostCode");
		var objMob = $("Mob");
		var objIDCard = $("IDCard");
		var objData = $("Data");
		var objURL = $("URL");
		var objCn = $("Cn");
		var objEn = $("En");		
		var objMaxSelect = $("MaxSelect");
		var objMinSelect = $("MinSelect");
		var objMaxTickoff = $("MaxTickoff");
		var objMinTickoff = $("MinTickoff");
	 }

	 
	 
	 function checkForm(){		
	 	var intItemType = parseInt($("flag").value);				
		var	sRegExp_Email = /^[_a-z0-9][0-9a-zA-Z\_\-\.]{0,}@([_a-z0-9]+\.)+[a-z0-9]{2,4}$/;
		var	sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
		var sRegExp_EmptyLine = /\n[\s| ]*\r/;
		var objItemName = $("ItemName");
	 	var objItemType = $("ItemType");
	 	var objMemo = $("Memo");
		var objSubItem = $("SubItem");
		var objBackSelect = $("BackSelect");
		var objEmpty = $("Empty");
		var objEmail = $("Email");
		var objPostCode = $("PostCode");
		var objMob = $("Mob");
		var objIDCard = $("IDCard");
		var objData = $("Data");
		var objURL = $("URL");
		var objCn = $("Cn");
		var objEn = $("En");		
		var objMaxSelect = $("MaxSelect");
		var objMinSelect = $("MinSelect");
		var objMaxLen = $("MaxLen");
		var objMinLen = $("MinLen");
		var objMaxTickoff = $("MaxTickoff");
		var objMinTickoff = $("MinTickoff");	
		var objLevelAmount = $("LevelAmount");		
		var objMinValue = $("MinValue");	
		var objMaxValue = $("MaxValue");		
		var objMaxFileLen = $("MaxFileLen");
		var objTotalPercent = $("TotalPercent");
		var objMinPercent = $("MinPercent");
		var objMaxPercent = $("MinPercent");
		var objMatrixRowColumn = $("MatrixRowColumn");
		var objInputLength = $("InputLength");	
		
		
		if(objItemName.value==""){
			$("ItemName_Message").innerHTML = "题目名不能为空";
			objItemName.focus();
			return false;			
		}
		$("ItemName_Message").innerHTML = "";
		if(objItemName.value.indexOf("%")>=0){
			$("ItemName_Message").innerHTML = "题目名有特殊字符";
			objItemName.focus();
			return false;			
		}
		$("ItemName_Message").innerHTML = "";
		if(objMemo.value!=""){
			if(objMemo.value.indexOf("%")>=0){
				$("MemoMessage").innerHTML = "题目说明有特殊字符";
				objMemo.focus();
				return false;	
			}
		}
		
		$("MemoMessage").innerHTML = "";
		
		//-------------------------验证文字输入题--------------------------------
	
		if(intItemType==1){
			
			var intMinLen = objMinLen.value
			var intMaxLen = objMaxLen.value
			if(sRegExp_Int.test(intMinLen)==false){
				$("MinMaxLen_Message").innerHTML = "最小字符应该为整数";
				objMinLen.focus();
				return false
			}
			$("MinMaxLen_Message").innerHTML = "";		
			
			if(sRegExp_Int.test(intMaxLen)==false){
				$("MinMaxLen_Message").innerHTML = "最大字符应该为整数";
				objMaxLen.focus();
				return false
			}				
			$("MinMaxLen_Message").innerHTML = "";		
			intMinLen = parseInt(intMinLen);
			intMaxLen = parseInt(intMaxLen);
			if(intMinLen>intMaxLen){
				$("MinMaxLen_Message").innerHTML = "最小值不能大于最大值";
				objMinLen.focus();
				return false;
			}
			$("MinMaxLen_Message").innerHTML = "";
			
			if(intMaxLen>200){
				$("MinMaxLen_Message").innerHTML = "最大字符数不能超过200";
				objMaxLen.focus();
				return false
			}
			$("MinMaxLen_Message").innerHTML = "";
		}

		//---------------------------------------------------------------------
		
		//--------------------------验证数值输题--------------------------
		if(intItemType==2){		
			var intMinValue = objMinValue.value
			var intMaxValue = objMaxValue.value
			if(sRegExp_Int.test(intMinValue)==false){
				$("MinMaxValueBox_Message").innerHTML = "最小值应该为数字";
				objMinValue.focus();
				return false
			}
			$("MinMaxValueBox_Message").innerHTML = "";		
			
			if(sRegExp_Int.test(intMaxValue)==false){
				$("MinMaxValueBox_Message").innerHTML = "最大值应该为数字";
				objMaxValue.focus();
				return false
			}				
			$("MinMaxValueBox_Message").innerHTML = "";		
			intMinValue = parseInt(intMinValue);
			intMaxValue = parseInt(intMaxValue);
			if(intMinValue>intMaxValue){
				$("MinMaxValueBox_Message").innerHTML = "最小值不能大于最大值";
				objMinValue.focus();
				return false;
			}
			$("MinMaxValueBox_Message").innerHTML = "";
			
			if(intMaxValue>999999999){
				$("MinMaxValueBox_Message").innerHTML = "最大字符数不能超过999999999";
				objMaxValue.focus();
				return false
			}
			$("MinMaxValueBox_Message").innerHTML = "";
		}
		
		
		if(",5,9,16,19,20,".indexOf(","+intItemType+",")>=0){
			var intInputLength = objInputLength.value;
			try{
				intInputLength = parseInt(intInputLength);
			}
			catch(e){
				$("InputLenghBox_Message").innerHTML = "需要输入1到50的整数";
				objInputLengh.focus();
				return false
			}
			if(sRegExp_Int.test(intInputLength)==false){
				$("InputLenghBox_Message").innerHTML = "需要输入1到50的整";
				objMaxValue.focus();
				return false
			}

			$("InputLenghBox_Message").innerHTML = "";
			if((intInputLength<=0)||(intInputLength>=51)){
				$("InputLenghBox_Message").innerHTML = "需要输入1到50的整数";
				objInputLengh.focus();
				return false
			}
			$("InputLenghBox_Message").innerHTML = "";
		}
		
		
		//验证矩阵题
		if(",7,15,16,18,19,20,21,".indexOf(","+intItemType+",")>=0){
			if(objSubItem.value==""){
				$("SubItemBox_Message").innerHTML = "子题目不能为空";
				objSubItem.focus();
				return false;
			}
			$("SubItemBox_Message").innerHTML = "";
			
			if(objSubItem.value.indexOf("%")>=0){
				$("SubItemBox_Message").innerHTML = "子题目不能有特殊字符";
				objSubItem.focus();
				return false;
			}
			$("SubItemBox_Message").innerHTML = "";
		}

		//矩阵验证完
		
		//备选项验证
		if(",4,5,6,7,8,9,10,12,15,16,18,19,20,".indexOf(","+intItemType+",")>=0){
			
			if(intItemType==18){
				if(objMatrixRowColumn.value==""){
					$("MatrixRowColumn_Message").innerHTML = "备选项不能为空";
					objMatrixRowColumn.focus();
					return false;
				}
				$("MatrixRowColumn_Message").innerHTML = "";
				if(objMatrixRowColumn.value.indexOf("%")>=0){
					$("MatrixRowColumn_Message").innerHTML = "备选项不能有特殊字符";
					objMatrixRowColumn.focus();
					return false;
				}
				$("MatrixRowColumn_Message").innerHTML = "";				
			}
			
			
			if(objBackSelect.value==""){
				$("BackSelectBox_Message").innerHTML = "备选项不能为空";
				objBackSelect.focus();
				return false;
			}
			$("BackSelectBox_Message").innerHTML = "";
			if(objBackSelect.value.indexOf("%")>=0){
				$("BackSelectBox_Message").innerHTML = "备选项不能有特殊字符";
				objBackSelect.focus();
				return false;
			}
			$("BackSelectBox_Message").innerHTML = "";
			
			
			
			//最多最小选择验证
			if(",8,9,10,".indexOf(","+intItemType+",")>=0){
				var intMaxSelect = objMaxSelect.value;
				var intMinSelect = objMinSelect.value;
				
				if(sRegExp_Int.test(intMinSelect)==false){
					$("MinMaxSelectBox_Message").innerHTML = "最少选择应该为数字";
					objMinSelect.focus();
					return false
				}
				$("MinMaxSelectBox_Message").innerHTML = "";		
				
				if(sRegExp_Int.test(intMaxSelect)==false){
					$("MinMaxSelectBox_Message").innerHTML = "最多选择应该为数字";
					objMaxSelect.focus();
					return false
				}				
				$("MinMaxSelectBox_Message").innerHTML = "";		
				intMinSelect = parseInt(intMinSelect);
				intMaxSelect = parseInt(intMaxSelect);
				if(intMinSelect>intMaxSelect){
					$("MinMaxSelectBox_Message").innerHTML = "最小值不能大于最大值";
					objMinSelect.focus();
					return false;
				}
				$("MinMaxSelectBox_Message").innerHTML = "";
			}
			//最多最小选择验证完
			
			
			
		}		
		
		
		if(intItemType==17){
			if(!sRegExp_Int.test($("MaxFileLen").value)){
				$("UploadFile_M_MaxFileLen").innerHTML="值应该为整数";	
				$("MaxFileLen").focus();
				return false
			}
			$("UploadFile_M_MaxFileLen").innerHTML="";
		}
		
		
		
		
		if(intItemType==11){
			intLevelAmount = objLevelAmount.value;			
			if(sRegExp_Int.test(intLevelAmount)==false){
				$("LevelBox_Message").innerHTML = "等级数必须为整数";
				objLevelAmount.focus();
				return false
			}	
			$("LevelBox_Message").innerHTML = "";
		}
		
	
	
		
		
		//最多最少列举验证
			if(intItemType==13){
				var intMaxTickoff = objMaxTickoff.value;
				var intMinTickoff = objMinTickoff.value;
				
				if(sRegExp_Int.test(intMinTickoff)==false){
					$("MinMaxTickoffBox_Message").innerHTML = "最少列举应该为数字";
					objMinTickoff.focus();
					return false
				}
				$("MinMaxTickoffBox_Message").innerHTML = "";		
				
				if(sRegExp_Int.test(intMaxTickoff)==false){
					$("MinMaxTickoffBox_Message").innerHTML = "列举项数应该为数字";
					objMaxTickoff.focus();
					return false
				}
				$("MinMaxTickoffBox_Message").innerHTML = "";				
				if(intMinTickoff>intMaxTickoff){
					$("MinMaxTickoffBox_Message").innerHTML = "最少列举项数不能大于列举项数";
					objMinTickoff.focus();
					return false;
				}
				$("MinMaxTickoffBox_Message").innerHTML = "";
	
			}
			//最多最小选择验证完
			
			
			
			if(intItemType==21){
				var intTotalPercent = objTotalPercent.value;
				var intMinPercent = objMinPercent.value;
				var intMaxPercent = objMaxPercent.value;
				if(sRegExp_Int.test(intTotalPercent)==false){
					$("PercentBox_Message").innerHTML = "百分比题总值应该为整数";
					objTotalPercent.focus();
					return false
				}
				
				if(sRegExp_Int.test(intMinPercent)==false){
					$("PercentBox_Message").innerHTML = "百分比题最小值应该为整数";
					objMinPercent.focus();
					return false
				}
				
				if(sRegExp_Int.test(intMaxPercent)==false){
					$("PercentBox_Message").innerHTML = "百分比题最大值应该为整数";
					objMaxPercent.focus();
					return false
				}				
				$("PercentBox_Message").innerHTML = "";
			}
		
		
	 }
	 
	 
	 function checkTest(id){
	 	var objEmail = $("Email");
		var objPostCode = $("PostCode");
		var objMob = $("Mob");
		var objIDCard = $("IDCard");
		var objData = $("Data");
		var objURL = $("URL");
		var objCn = $("Cn");
		var objEn = $("En");		
	 
	 	objEmail.checked = false
	 	objPostCode.checked = false
		objMob.checked = false
		objData.checked = false
		objCn.checked = false
		objURL.checked = false
		objCn.checked = false
		objEn.checked = false
		objIDCard.checked = false
		$(id).checked = true
		var	intMaxLen = 50;
		if($("PostCode").checked){
			intMaxLen = 6;
		}
		else{
			if($("Mob").checked){
				intMaxLen = 11;
			}
			else{
				if($("IDCard").checked){
					intMaxLen = 18;
				}
				else{
					if($("Data").checked){
						intMaxLen = 11;
					}
					else{
						intMaxLen = 50;
					}
					
				}
			}
		}
		$("MaxLen").value = intMaxLen;
		
	 }


function mouseMoveBT(v,d){
    var obj = $("T"+v);
	if(obj.className=="ItemDown"){
		return;	
	}
	if(d==0){//如果是onmousemove事件
        obj.className = "ItemOver";           
    }
    else{//如果是onmouseout事件
        obj.className = "ItemNormal";
    }
}

var blnActioned = 'False';
var blnActioned1 = 'False';

function CloseWin(){  

	if(blnActioned1=="True"){
	    blnActioned = 'True';
	    blnActioned = 'False';
	}
	
	window.parent.blnActioned = blnActioned;	
	window.parent.closeActionWin();
	
}

function openItemLib(){
	$("ItemLib").src = "ItemLib.aspx?SID="+SID
}

function insertNormal(){
	$('Layer1').style.display='block'		
}