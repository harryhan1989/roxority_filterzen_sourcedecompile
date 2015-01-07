//得到指定名称的对新
function docEle (obj) 
{
	var doc = getDocument(true);
	return doc.getElementById(obj) || false;
}

//弹出层显示信息
function showShortMessage(message,delayms) 
{
	var _id = "__messageDiv$0";
	var winW = 250;
	var winH = 32;
	if (message.length > 18) winW = 440;
	
	var doc = getDocument(true);
	
	var divLeft = doc.body.clientWidth - winW + (-2);
	var divTop = doc.body.clientHeight - winH + (-2);
	
	if (getBrowserVersion() == 6) divTop = doc.body.style.pixelHeight - winH + (-2);
	
	//alert(doc.body.clientHeight)
	var count = 0;
	while (docEle(_id))
	{
		count = parseInt(_id.split("$")[1]) + 1;
		_id = _id.split("$")[0] + "$" + count;
	}
	
	divTop = divTop- (winH + 1) * count;

	// 新激活图层
	var newDiv = doc.createElement("div");

	newDiv.id = _id;
	newDiv.className = "messageBox";
	newDiv.style.position = "absolute";
	newDiv.style.zIndex = 9999;
	newDiv.style.width = winW + "px";
	newDiv.style.height = winH + "px";
	newDiv.style.left = divLeft;
	newDiv.style.top = divTop;
	newDiv.style.overflow= "auto";
	//newDiv.innerHTML = getHtmlText("[系统消息] <b>" + message + "</b>");
	newDiv.innerHTML = getHtmlText(message);
	doc.body.appendChild(newDiv);
	
	var newDivBg = doc.createElement("div");
    newDivBg.id = _id + "bg";
	newDivBg.style.position = "absolute";
	newDivBg.style.zIndex = 9998;
	newDivBg.style.width = newDiv.style.width;
	newDivBg.style.height = winH + "px";
	newDivBg.style.left = divLeft;
	newDivBg.style.top = divTop;
	newDivBg.innerHTML = "<iframe style=' visibility:inherit; width:100%; height:"+winH+"px;' src='' frameborder='no'></iframe>"
	doc.body.appendChild(newDivBg);
	
	getWindow(true).setTimeout("removeMessageDiv('"+_id+"')",delayms);
}

//移除指定ID的对象
function removeMessageDiv(id)
{
	getDocument(true).body.removeChild(docEle(id));
	getDocument(true).body.removeChild(docEle(id+"bg"));
}

//得到内容
function getHtmlText(msg)
{
	return "<div style='overflow:hidden;'>" + msg + "</div>";
}