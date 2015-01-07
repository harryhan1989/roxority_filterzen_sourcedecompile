/*
编写者：郑亮
时间：  2007-4-25

调用方法例子：onclick="showModalWindow('xwin1','标题',400,400,'BaseModule/MessageConfig.aspx');return false;"
*/

var x0=0,y0=0,x1=0,y1=0;
var offx =0,offy = 0;
//var offx =6,offy = 6;
var moveable = false;

var className = "";//整个层CSS名称
var headClassName = "";//头部CSS名称

//创建一个对象;
function xWin(id,w,h,l,t,tit,msg,isShowTopWindow,onClosedFunArg)
{
	this.id        = id;
	this.width   = w;
	this.height  = h;
	this.left      = l;
	this.top      = t;
	this.zIndex  = getGlobalZIndex();
	this.title     = tit;
	this.message = msg;
	this.obj      = null;
	
    this.TopWindow    = isShowTopWindow;
    
    this.OnClosedFunArg = onClosedFunArg == null ? "" : onClosedFunArg;

	this.bulid    = bulid;
	
	addGlobalZIndex(3);
	
	this.bulid();
}

//初始化;
function bulid()
{
    var twin = getWindow(this.TopWindow);
    var tdoc = twin.document;
	var str = ""
		//主体层1
		+ "<div id='xMsg" + this.id + "' "
		+ "class = '"+ className +"' " //定义样式
		+ "style='"
		+ "z-index:" + this.zIndex + ";"
		+ "width:" + this.width + "px;"
		+ "height:" + this.height + "px;"
		+ "left:" + this.left + "px;"
		+ "top:" + this.top + "px;"
		+ "position:absolute;"
		+ "cursor:default;"
		+ "' "
		+">"
			    //窗体头层1.1
			    + "<div "
		        + "class = '"+ headClassName +"' "//定义样式
			    + "style='"
			    + "width:" + (this.width - 0) + "px;"
			    + "' "
			    + "onmousedown='startDrag(this)' "
			    + "onmouseup='stopDrag(this)' "
			    + "onmousemove='drag(this)' "
			    //+ "ondblclick='min(this.childNodes[1])'"
			    + ">"
				    + "<span id='xWinTitle'>" + this.title + "</span>"
				    //关闭箭头
				+"<a href='#' onclick='closeModalWindow(\""+this.id+"\","+this.TopWindow+",\"" + this.OnClosedFunArg + "\");'></a>"
			    + "</div>"//窗体头层1.1结束
			    //内容层1.2
			    + "<div id='xMsgBody"+this.id+"'  style='"
			    //+ "padding-top:10px;"
			    + "width:100%;"
			    + "height:" + (this.height - 27) + ";"
			    + "background-color:white;"
			    + "word-break:break-all;"
			    + "'>" + this.message 
			    + "</div>"//内容层1.2结束
		+ "</div>"//主体层1结束
		
		//遮盖整页的层，透明3
		+ "<div id='xMsg" + this.id + "bg2' style='"
		+ "width:" + (tdoc.body.clientWidth) + ";"
		+ "height:" + (tdoc.body.clientHeight) + ";"
		+ "top:0;"
		+ "left:0;"
		+ "z-index:" + (this.zIndex-2) + ";"
		+ "position:absolute;"
		+ "filter:alpha(opacity=80);opacity:80;"
		+ "background-color:#FFFFFF;"
		+ "'>"
	    + createiFrame("")
		+"</div>";//遮盖整页的层，透明3结束
	//document.write(str);
	tdoc.body.insertAdjacentHTML("beforeEnd",str);
}

//-----------------------------核心打开窗口方法-----------------------------
function createxWindow(wid,title,width,height,left,top,msg,isTop,onClosedFunArg)
{
	var _isTop = isTop == null ? false : isTop;
	var a = new xWin(wid,width,height,left,top,title,msg,_isTop,onClosedFunArg);
}

//-----------------------------核心关闭窗口方法-----------------------------
function closeModalWindow(wid,isTop,onClosedFunArg)
{	
	var d = getDocument(isTop);
	
	var w1 = d.getElementById("xMsg"+wid);
	var w3 = d.getElementById("xMsg"+wid+"bg2");
	
	if (w1 != null) d.body.removeChild(w1);
	if (w3 != null) d.body.removeChild(w3);
	
	subGlobalZIndex(3);//减去记录z轴高度的全局变量
	removeContexts(wid);//移除记录子父窗口关联的全局变量
	
	OnxWinClosed(onClosedFunArg);

    return false;
}

//确认框关闭窗口
function confirmCloseModalWindow(id,isConfirm,controlID)
{
	closeModalWindow(id,false);
	if (!isConfirm) return;
	
	__doPostBack(controlID,'');
}

//确认框关闭窗口
function confirmToolbarCloseModalWindow(id,isConfirm,toolbarID,buttonKey)
{
	closeModalWindow(id,false);
	if (!isConfirm) return;
	
	var toolbar = igtbar_getToolbarById(toolbarID);
	var button = toolbar.Items.fromKey(buttonKey);
	
	toolbar.post(button.Id + ":DOWN");
}


//------------------------------------------------------------得到窗口位置------------------------------------------------------------
//得到要显示窗口的左边距
function getWindowLeft(width,isTop)
{
    var w = getWindow(isTop);
    //return (w.screen.availWidth - width) / 2;
    return (w.document.body.clientWidth - width) / 2;
}

//得到要显示窗口的顶边距
function getWindowTop(height,isTop)
{
    var w = getWindow(isTop);
    //return (w.screen.availHeight - height - 120) / 2;
    return (w.document.body.clientHeight - height) / 2;
}

//------------------------------------------------------------创建内容------------------------------------------------------------
//创建iframe
function createiFrame(url)
{
    return "<iframe style=' visibility:inherit; width:100%; border-collapse: collapse; height:100%' src='" +url+"' frameborder='no'></iframe>";
}

//创建提示框标签
function createAlertTag(msg,wid)
{
    return "<table style='width:100%;height:100%;' border='0' cellspacing='0' cellpadding='6'><tr><td align='center' valign='middle'>"+msg+"</td></tr><tr><td align='center' valign='buttom' style='height:20px;'><input type='button' name='Button' class='Button' style='width:85px;' onclick='closeModalWindow(\""+wid+"\",false);' value='确定'></td></tr><tr><td style='height:8px'></td></tr></table>"
}

//创建确认框标签
function createConfirmTag(msg,wid,controlID) 
{
    return "<table style='width:100%;height:100%;' border='0' cellspacing='0' cellpadding='6'><tr><td align='center' valign='middle' colspan='2'>"+msg+"</td></tr><tr><td align='right' style='height:20px'><input type='button' name='Button' class='Button' style='width:85px;' onclick='confirmCloseModalWindow(\""+wid+"\",true);' value='确定'></td><td align='left'><input type='button' name='Cancel' class='Button' style='width:85px;' onclick='confirmCloseModalWindow(\""+wid+"\",false);' value='取消'></td></tr></table>"
}

//创建确认框标签
function createToolBarConfirmTag(msg,wid,toolbarID,buttonKey)
{
    return "<table style='width:100%;height:100%;' border='0' cellspacing='0' cellpadding='6'><tr><td align='center' valign='middle' colspan='2'>"+msg+"</td></tr><tr><td align='right' style='height:20px'><input type='button' name='Button' class='Button' style='width:85px;' onclick='confirmToolbarCloseModalWindow(\""+wid+"\",true,\""+toolbarID+"\",\""+buttonKey+"\");' value='确定'></td><td align='left'><input type='button' name='Cancel' class='Button' style='width:85px;' onclick='confirmToolbarCloseModalWindow(\""+wid+"\",false,null,null);' value='取消'></td></tr></table>"
}

//-----------------------------------------------------获取全局zIndex-----------------------------------------------------
//得到全局zIndex
function getGlobalZIndex()
{
    var o = getObject("_zIndexControl");
    if (o == null) return 10000;
    if (o.value == "") o.value = 10000;
    
    return parseInt(o.value);
}

//累加全局zIndex
function addGlobalZIndex(val)
{
    var o = getObject("_zIndexControl");
    if (o == null) return;
    if (o.value == "") o.value = 10000;
    
    var z = parseInt(o.value);
    
    o.value = z + val;
}

//减全局zIndex
function subGlobalZIndex(val)
{
    var o = getObject("_zIndexControl");
    if (o == null) return;
    if (o.value == "") o.value = 10000;
    
    var z = parseInt(o.value);
    
    o.value = z - val;
}

//--------------------------------------拖动--------------------------------------

//开始拖动;
function startDrag(obj)
{
	if(event.button==1)
	{
		//锁定标题栏;
		obj.setCapture();
		//定义对象;
		var win = obj.parentNode;
		var sha = win.nextSibling;
		//记录鼠标和层位置;
		x0 = event.clientX;
		y0 = event.clientY;
		x1 = parseInt(win.style.left);
		y1 = parseInt(win.style.top);
		////记录颜色;
		//normal = obj.style.backgroundColor;
		////改变风格;
		//obj.style.backgroundColor = hover;
		//win.style.borderColor = hover;
		//obj.nextSibling.style.color = hover;
		//sha.style.left = x1 + offx;
		//sha.style.top  = y1 + offy;
		moveable = true;
	}
}
//拖动;
function drag(obj)
{
	if(moveable)
	{
		var win = obj.parentNode;
		var sha = win.nextSibling;
		win.style.left = x1 + event.clientX - x0;
		win.style.top  = y1 + event.clientY - y0;
		//sha.style.left = parseInt(win.style.left) + offx;
		//sha.style.top  = parseInt(win.style.top) + offy;
		
	    //窗口在可见区域内
		if (event.clientY <= 0)//上边界
		{
		    obj.releaseCapture();
		    moveable = false;
		    win.style.top = 0;
		}
		var currentHeight = getWindow(true).document.body.clientHeight;
		if (event.clientY > currentHeight)//下边界
		{
		    obj.releaseCapture();
		    moveable = false;
		    win.style.top = currentHeight - 20;
		}
	}
}
//停止拖动;
function stopDrag(obj)
{
	if(moveable)
	{
		var win = obj.parentNode;
		var sha = win.nextSibling;
		var msg = obj.nextSibling;
		//win.style.borderColor     = normal;
		//obj.style.backgroundColor = normal;
		//msg.style.color           = normal;
		//sha.style.left = obj.parentNode.style.left;
		//sha.style.top  = obj.parentNode.style.top;
		obj.releaseCapture();
		moveable = false;
	}
}

//转换为时间字串
function getUCTDateString(){
   var d, s = "";
   d = new Date();
   s += d.getHours();
   s += d.getMinutes();
   s += d.getSeconds();
   s += d.getMilliseconds();
   
   return(s);
}


//--------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------得到父文档对象----------------------------------------------------

//父对象得到打开子窗口文档对象
function parentGetDoc(wid)
{
    var url = getContexts(wid,"p");
    if (url =="") return null;
    
    return getDocumentByUrl(url);
}

//子对象得到从属的父对文档对象
function dependentGetDoc(wid)
{
    var url = getContexts(wid,"d");
    if (url =="") return null;
    
    return getDocumentByUrl(url);
}

//得到指定Url文档对象
function getDocumentByUrl(docUrl)
{
    var w = getWindow(true);
    
    return getDocumentObject(w.document.frames,docUrl);
}

//递归查询文档对象，内部使用
function getDocumentObject(frames,docUrl)
{
    var fs = frames;
    if (fs && fs.length != 0)
    {
        for (var i=0;i<fs.length;i++)
        {
            var doc = fs[i].self.window.document;
            
            var url = doc.location.href.toString();
            url = url.substring(url.lastIndexOf("/") + 1,url.length);
            var fUrl = docUrl.substring(docUrl.lastIndexOf("/") + 1,docUrl.length);
            
            if (url == fUrl) return doc;
            
            var docr = getDocumentObject(doc.frames,docUrl);
            if(docr) return docr;
        }
    }
}

//向全局变量加入文档关联
function addContexts(wid,purl,wurl)
{
    var o = getObject("_upWindowContext");
    if (o == null) return;
    
    if (o.value.indexOf(wid) > -1) removeContexts(wid);
    o.value += wid + "|" + purl + "|" + wurl + ",";
}

//从全局变量减去文档关联
function removeContexts(wid)
{
    var o = getObject("_upWindowContext");
    if (o == null) return;
    
    if (o.value.indexOf(wid) < 0) return;
    var allstr = o.value;
    var s1,s2;
    if (allstr.indexOf(wid) == 0)
    {
        s1 = "";
    }
    else
    {
        s1 = allstr.substring(0,allstr.indexOf(wid));
    }
	
    s2 = allstr.substring(allstr.indexOf(wid),allstr.length);
    s2 = s2.substring(s2.indexOf(",") + 1,s2.length);
    
    o.value = s1 + s2;
}

//得到关联，内部使用
function getContexts(wid,type)
{
    var o = getObject("_upWindowContext");
    if (o == null || o.value == "") return "";
    if (o.value.indexOf(wid) < 0) return "";
    var all = o.value;
    
    var arrAll = all.split(",");
    for (i=0;i<arrAll.length;i++)
    {
        var arr = arrAll[i].split("|");
        if (arr[0] == wid) return type == "p" ? arr[2] : arr[1];
    }
    
    return "";
}



//-----------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------外部调用方法----------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------

//显示模态窗口
function showModalWindow(wid,title,width,height,url)
{
    return showModalWindow2(wid,title,width,height,url,null);
}

//显示模态窗口,传递关闭是触发额外放的参数
function showModalWindow2(wid,title,width,height,url,onClosedFunArg)
{    
    headClassName = "ModalWindowHeadStyle";
    className = "ModalWindowStyle";
    
	var purl = this.document.location.href;
    //加入文档关联
	addContexts(wid,purl,url);

    var l = getWindowLeft(width,true);
    var t = getWindowTop(height,true);

	createxWindow(wid,title,width,height,l,t,createiFrame(url),true,onClosedFunArg);
    
    return false;
}

//显示出错信息窗口
function showAlert(title,width,height,msg)
{
    wid = getUCTDateString();

    headClassName = "ErrModalWindowHeadStyle";
    className = "ErrModalWindowStyle";
    
    var l = getWindowLeft(width,false);
    var t = getWindowTop(height,false);
   
    createxWindow(wid,title,width,height,l,t,createAlertTag(msg,wid),false);

    return false;
}

//按钮显示提示信息窗口
function showConfirm(title,width,height,msg,controlID)
{
    wid = getUCTDateString();
   
    headClassName = "ModalWindowHeadStyle";
    className = "ModalWindowStyle";

    var l = getWindowLeft(width,false);
    var t = getWindowTop(height,false);
	
	createxWindow(wid,title,width,height,l,t,createConfirmTag(msg,wid,controlID),false);

    return false;
}

//Toolbar显示提示信息窗口(内部使用)
function showToolbarConfirm(title,width,height,msg,toolbarID,buttonKey)
{
    wid = getUCTDateString();
    
    headClassName = "ModalWindowHeadStyle";
    className = "ModalWindowStyle";

    var l = getWindowLeft(width,false);
    var t = getWindowTop(height,false);

    createxWindow(wid,title,width,height,l,t,createToolBarConfirmTag(msg,wid,toolbarID,buttonKey),false);

    return false;
}

//Toolbar显示提示信息窗口(编码者调用)
function toolbarConfirm(msg, oToolbar, oButton, oEvent)
{
    showToolbarConfirm("系统提示",200,120,msg,oToolbar.Id,oButton.Key);
    
    oEvent.needPostBack = false;
}

/*
//最小化;
function min(obj)
{
	var win = obj.parentNode.parentNode;
	var sha = win.nextSibling;
	var tit = obj.parentNode;
	var msg = tit.nextSibling;
	var flg = msg.style.display=="none";
	if(flg)
	{
		win.style.height  = parseInt(msg.style.height) + parseInt(tit.style.height) + 2*2;
		//sha.style.height  = win.style.height;
		msg.style.display = "block";
		obj.innerHTML = "0";
	}
	else
	{
		win.style.height  = parseInt(tit.style.height) + 2*2;
		//sha.style.height  = win.style.height;
		obj.innerHTML = "2";
		msg.style.display = "none";
	}
}

//设置焦点
function setFocus(obj)
{
    try
    {
        var pObj = obj.parentElement;
        var a =(xWMouseX >= pObj.offsetLeft && xWMouseX <= (pObj.offsetLeft + pObj.offsetWidth)) && (xWMouseY >= pObj.offsetTop && xWMouseY <= (pObj.offsetTop + pObj.offsetHeight))
        if (a) return;
        if (obj != null) obj.focus();
    }
    catch(e)
    {
    
    }
}

//获得焦点;
function getFocus(obj)
{
	if (obj.style.zIndex != g_index)
	{
		g_index = g_index + 2;
		var idx = g_index;
		obj.style.zIndex = idx;
		obj.nextSibling.style.zIndex = idx - 1;
		obj.nextSibling.nextSibling.style.zIndex = idx - 2;
	}
}
*/

