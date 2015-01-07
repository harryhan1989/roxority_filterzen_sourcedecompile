var sSelectFile;
var sBTOverImage = "../Survey/images/ShowLeft2.gif";
var sBtNormal1Image = "../Survey/images/ShowLeft1.gif";



window.onload = function(){
	//document.getElementById("Main").style.height = "540px";
	//setWorkAreaHeight("OptionArea",102+25);
	document.getElementById("ItemList").innerHTML = initItemList();
	//setParentWinHeight();
	var myface = new interface();
	var winSize = myface._getShowSize();
	document.getElementById("OptionArea").style.height = (winSize.h - 105)+"px";
	document.getElementById("ItemList").style.height = (winSize.h - 105)+"px";
	
}


function ShowUploadWin(){
	var objUp = document.getElementById("LeftMenu");
	var objFile = document.getElementById("Main");
	var objBT = document.getElementById("BT");
	if(objUp.style.display=="block"){
		objUp.style.display = "none";
		objBT.src = "../Survey/images/ShowRight1.gif";
		sBTOverImage = "../Survey/images/ShowRight2.gif";
		sBtNormal1Image = "../Survey/images/ShowRight1.gif";
	}
	else{
		objUp.style.display = "block";
		objBT.src = "../Survey/images/ShowLeft1.gif";
		sBTOverImage = "../Survey/images/ShowLeft2.gif";
		sBtNormal1Image = "../Survey/images/ShowLeft1.gif";
	}
	
}

function setMouseMove(d){
	var objBT = document.getElementById("BT");
	if(d==0){//如果鼠标移入
		objBT.src = sBTOverImage	
	}
	else{
		objBT.src = sBtNormal1Image
	}
}



function printTarget(){	
	if(document.getElementById("targetWin").src==""){
		alert("没有打印对象");
		return;
	}
	window.frames.targetWin.focus();
	window.frames.targetWin.print();
}

function formatShowShortStr(sInput,intShowLen,sEndStr){
	var sResult = sInput;
	if(sInput.length>intShowLen){
		sResult = sInput.substring(0,intShowLen)+sEndStr;
	}
	return sResult;
}
var currIndex = -1;
function initItemList(){
	var sResult = "";
	for(var i=0;i<arrItem.length;i++){
		switch(arrItem[i][2]){
			case 1:
			case 3:
			case 13:
			case 14:
			case 16:
			case 17:
				sResult += "<div class=ItemGray id=Item"+i+">"+(i+1)+":"+formatShowShortStr(arrItem[i][1],10,"...")+"</div>";
				break;
			case 2:
			case 21:
			    sResult += "<div  clearMenuStyle() style=\"cursor:pointer\" id=Item" + arrItem[i][0] + " class=ItemNormal onmouseover = SetBt(" + i + ",0) onmouseout= SetBt(" + i + ",1) onclick = SetBt(" + i + ",2);viewStat(" + SID + "," + arrItem[i][0] + ")>" + (i + 1) + ":" + formatShowShortStr(arrItem[i][1], 10, "...") + "</div>";
				break;
			default :
			    sResult += "<div  clearMenuStyle() style=\"cursor:pointer\" id=Item" + arrItem[i][0] + " class=ItemICONormal  onmouseover = SetBt(" + i + ",0) onmouseout= SetBt(" + i + ",1) onclick = SetBt(" + i + ",2);viewStat(" + SID + "," + arrItem[i][0] + ")>" + (i + 1) + ":" + formatShowShortStr(arrItem[i][1], 10, "...") + "</div>";
				break;
		}
	}
	return sResult;
}

function SetBt(ItemIndex,State){
	switch(State){
		case 0://Over
			if((arrItem[ItemIndex][2]!=2)&&(arrItem[ItemIndex][2]!=21)){
				if(document.getElementById("Item"+arrItem[ItemIndex][0]).className != "ItemICONormalSelect"){
				document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemICONormalOver";
				}
			}
			else{
				if(document.getElementById("Item"+arrItem[ItemIndex][0]).className != "ItemNormalSelect"){
				document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemNormalOver";
				}
			}
			break;
		case 1://Out

			if((arrItem[ItemIndex][2]!=2)&&(arrItem[ItemIndex][2]!=21)){
				if(document.getElementById("Item"+arrItem[ItemIndex][0]).className != "ItemICONormalSelect"){
					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemICONormal";
				}
			}
			else{
				if(document.getElementById("Item"+arrItem[ItemIndex][0]).className != "ItemNormalSelect"){
					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemNormal";
				}
			}
			break;
		case 2://Click
			if(currIndex>=0){
				if((arrItem[currIndex][2]!=2)&&(arrItem[currIndex][2]!=21)){
					document.getElementById("Item"+arrItem[currIndex][0]).className = "ItemICONormal";
					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemICONormalSelect";
				}
				else{
					document.getElementById("Item"+arrItem[currIndex][0]).className = "ItemNormal";
					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemNormalSelect";
				}
			}
			else{
				if((arrItem[ItemIndex][2]!=2)&&(arrItem[ItemIndex][2]!=21)){
					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemICONormalSelect";
				}
				else{

					document.getElementById("Item"+arrItem[ItemIndex][0]).className = "ItemNormalSelect";
				}	
			}
			
			
			currIndex  = ItemIndex;
			
			break;
	}
}

function clearMenuStyle() {
    document.getElementById("B1").className = btOut;
}

function viewStat(SID,IID){
    document.getElementById("targetWin").src = "ItemAnalysis.aspx?SID=" + SID + "&IID=" + IID;	
}