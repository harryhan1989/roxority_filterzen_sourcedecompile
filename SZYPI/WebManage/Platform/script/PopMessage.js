/*
  消息弹出窗方法     
  作者：金利峰
  日期：2004-11-11
  修改：郑亮 2007-4-25 
  
  参数说明：
  width		        窗口宽度
  height		        窗口高度
  title			        窗口标题
  contentArray	消息内容数组
  linkArray		    链接地址数组
  outTime		    延时关闭时间（单位：豪秒）
  left                  消息窗口起始左边距 为null则从最右端弹出 

  调用的例子：
    popMessage(200,140,'消息提醒',new Array('dddd'),new Array(''),10000)
    popMessage2(180,120,'消息提醒','dddd',5000,null) 
  */

var oPopup;

var popTop = 0;

var upLoopID;
var downLoopID;

var flag = 0;

//得到消息内容
function innerHTML(width,height,title,contentArray,linkArray)
{
	var TempArray = "";
	var i = 0;
	var number = 0;
	var zindex = 9999;
	var html = "";
	var width2 = width-6;
	var height2 = height-25;
	var popupWinId = "PopupWin";
	var popupWinHeaderId = "PopupWin_Header";
	var popupWinTitleId = "PopupWin_Title";
	var popupWinContentId ="PopupWin_Content";
	var popupWinMessageId ="PopupWin_Message";
	html = html+"<div id=\""+popupWinId+"\" style=\"background:#E0E9F8; border-right:1px solid #455690; border-bottom:1px solid #455690;border-left:1px solid #B9C9EF; border-top:1px solid #B9C9EF; position:absolute; z-index:"+zindex+";  width:"+width+"px; height:"+height+"px; left:0px; right:0px;\" onselectstart=\"return false;\" >\r\n"+
				"<div id=\""+popupWinHeaderId+"\" style=\"cursor:default;position:absolute; left:2px; width:"+width2+"px; top:2px;height:15px;filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF');font:12px Tahoma; color:#1F336B; text-decoration:none;\">\r\n"+
					"<span id=\""+popupWinTitleId+"\" style=\"padding: 2px;\">"+title+"</span>\r\n"+
					"<span style=\"padding: 2px;vertical-align: middle;position:absolute; right:0px; top:0px; cursor:pointer; color:#728EB8; font:bold 12px Tahoma;\" onclick=\"parent.ClosePopupWin()\" onmousedown=\"event.cancelBubble=true;\" onmouseover=\"style.color='#455690';\" onmouseout=\"style.color='#728EB8';\">X</span>\r\n"+
				"</div>\r\n"+
				"<div id=\""+popupWinContentId+"\" onmousedown=\"event.cancelBubble=true;\" style=\"border-left:1px solid #728EB8; border-top:1px solid #728EB8;border-bottom:1px solid #B9C9EF; border-right:1px solid #B9C9EF;background:#E0E9F8;padding:5px;pacing=4px; overflow:hidden; text-align:left; filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF');position:absolute;OVERFLOW: auto; left:2px; width:"+width2+"px; top:20px; height:"+height2+"px;\">\r\n";
	
	if (linkArray == null)
	{
	    html = html+"<span  style=\"cursor:hand; font:12px Tahoma; color:#1F336B; text-decoration:none;\" id=\""+popupWinMessageId+"\">"+contentArray+"</span><br>";
	}
	else
	{
	    contentArray = contentArray.split(",");
	    linkArray = linkArray.split(",");
	    
	    for(i=0;i<contentArray.length;i++)
	    {
		    number = number+1;
		    var link = linkArray[i] != "" ? "onclick=\"parent.OpenWin('"+linkArray[i]+"','"+i+"')\"" : ""; 
		    html = html+"<span  style=\"cursor:hand; font:12px Tahoma; color:#1F336B; text-decoration:none;\" id=\""+popupWinMessageId+"\""+link+ ">"+number+"."+contentArray[i]+"</span><br>";
	    }
	}
	html = html + "</div>\r\n";
	html = html + "</div>\r\n";
	oPopup = window.createPopup();
	oPopup.document.body.innerHTML = html;
}

//外部调用方法
function popMessage(width,height,title,content,timeOut,left)
{
	flag = 0;
	innerHTML(width,height,title,content,null);//构造内容
	popupWin(width,height,left);
	window.setTimeout("popdownWin(" + width + "," + height + "," + left + ")",timeOut);//延时执行popdownWin
}

//外部调用方法,包含链接
function popMessage2(width,height,title,contentArray,linkArray,timeOut,left)
{
	flag = 0;
	innerHTML(width,height,title,contentArray,linkArray);//构造内容
	popupWin(width,height,left);
	window.setTimeout("popdownWin(" + width + "," + height + "," + left + ")",timeOut);//延时执行popdownWin
}

//延时循环执行oPopup.show--窗口向上移动
function popupWin(width,height,left,top)
{
	if (left == null) left = screen.width - width;
	if (top == null) top = screen.height - popTop;
	
	try//解决弹出窗口被屏蔽问题
	{
	    if ( popTop <= height)
	    {
		    oPopup.show(left,top,width, popTop);
	    }
	    else if (popTop <= height+30)
	    {
		    oPopup.show(left,top,width, height);
	    }
	    else
	    {
		    oPopup.show(left,top-30,width, height);
	    }
	    popTop=popTop + 1;
	    upLoopID = setTimeout("popupWin(" + width + "," + height + "," + left + "," + top +");",5);
    }catch(e){}
}

//延时循环执行oPopup.show--窗口向下移动并停止
function popdownWin(width,height,left,top)
{
	var _top;
	if (left == null) left = screen.width - width;
	_top = top == null ? screen.height - height : top;

	try//解决弹出窗口被屏蔽问题
	{
	    if (flag == 0)
	    {
		    window.clearInterval(upLoopID);
		    if( height > 0 && null != oPopup)
		    {
			    oPopup.show(left,_top - 30, width, height);
		    }
		    else
		    {
			    window.clearInterval(downLoopID);
			    if(null != oPopup)
			    {
				    oPopup.show(0,0,0,0);
				    flag=1;
				    popTop = 0;
			    }
			    return;
		    }
		    height = height - 1;
		    downLoopID = window.setTimeout("popdownWin(" + width + "," + height + "," + left + "," + top + ")",5);
	    }
    }catch(e){}
}

//立即终止弹出窗口
function ClosePopupWin()
{
	oPopup.show(0,0,0,0);
	flag = 1;
	popTop = 0;
	window.clearInterval(upLoopID);
	window.clearInterval(downLoopID);
}

function OpenWin(strUrl,winName)
{
	var intHeight = 400;
	var intWidth = 600;
	var mytop=(screen.height- intHeight)/2;
	var myleft=(screen.width- intWidth)/2;
	var strType='resizable=no,scrollbars=no,width=' + intWidth + ',height=' + intHeight + ',left=' + myleft + ',top=' + mytop;
	var win=window.open(strUrl,winName,strType);
	win.focus();
}