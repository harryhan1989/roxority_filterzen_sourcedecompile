var blnActioned = 'False';
window.onload = function(){
	try{
			window.parent.complateActionWin();
		}
		catch(e){
		
		}
		initItem(arrItem);
}


function addOption(v,t,obj){
	var o = document.createElement("option");
	o.value = v;
	o.text = t;
	obj.options.add(o);
}

function initItem(arr){
	var obj = document.getElementById("ItemList");
	for(var i=0;i<arr.length;i++){
		addOption(arr[i][0],arr[i][1],obj)	
	}
}

function ttt(){
	return;
	var obj = document.getElementById("ItemList");
	var selectindex = obj.selectedIndex;
	var selectValue = obj[selectindex].value;
	var selectText = obj[selectindex].text;
	var switchValue = obj[selectindex+1].value
	var switchText = obj[selectindex+1].text;
	obj[selectindex].value = switchValue;
	obj[selectindex].text = switchText;
	obj[selectindex+1].value = selectValue;
	obj[selectindex+1].text = selectText;

}

function switchSort(d,obj){
	
	
	var selectindex = obj.selectedIndex;
	if(selectindex<0){
		alert("请选择一个题目来操作");
		return;	
	}
	
	if(d==-1){
		if(selectindex==0){
			return;	
		}
	}
	else{
		if(selectindex==(obj.length-1)){
			return;	
		}	
	}


	var selectValue = obj[selectindex].value;
	var selectText = obj[selectindex].text;
	
	var switchValue = obj[selectindex+d].value
	var switchText = obj[selectindex+d].text;
	
	
	obj[selectindex].value = switchValue;
	obj[selectindex].text = switchText;
	obj[selectindex+d].value = selectValue;
	obj[selectindex+d].text = selectText;
	obj.selectedIndex = selectindex+d;	
}

function checkForm(){
	var sResult = "";
	var obj = document.getElementById("ItemList");
	for(var i=0;i<obj.length;i++){
		sResult += obj.options[i].value + ":"+(i+1)+","
	}
	if(sResult!=""){
		sResult = sResult.substring(0,sResult.length-1);	
	}
	if(sResult==""){
		alert("没有操作");
		return false;	
	}
	window.parent.blnActioned = 'True';
	document.getElementById("Result").value = sResult;
}