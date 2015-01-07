//去掉字串两端空格
function trim(str) 
{ 
    return str.replace(/^[ \t\n\r]+/g, "").replace(/[ \t\n\r]+$/g, "");
}

//得到当前浏览器版本，如果不是IE则返回-1
function getBrowserVersion()
{
	var version = navigator.appVersion;
	var p1 = version.indexOf("MSIE ") + 5;
	if (p1 == 4) return -1;
	return version.substring(p1,p1 + 1);
}

//得到根窗口对象
function getWindow(isTop)
{
    if (!isTop) return window;
    var w = window;
    while (w.parent.location.href != w.location.href)
    {
        w = w.parent;
    }
    
    return w;
}

//得到文档对象
function getDocument(isTop)
{
    var w = getWindow(isTop);
    return w.document;
}
 
//Gridview CheckBox全选
function selectAll(chkall)
{
	var chkother= document.getElementsByTagName("input");
	for (var i=0;i<chkother.length;i++)
	{
		if( chkother[i].type=='checkbox')
		{
			if(chkother[i].id.indexOf('CheckBoxSelect')>-1)
			{
				if(chkall.checked==true)
				{
					chkother[i].checked=true;
				}
				else
				{
                    chkother[i].checked=false;
				}
			}
		}
	}
}

//UltraWebTree CheckBox全选
function tv_NodeChecked(treeId, nodeId, bChecked)
{
    setChildNodesChecked(igtree_getNodeById(nodeId),bChecked);
}

function setChildNodesChecked(node,bChecked)
{
    var childNodes = node.getChildNodes();
    if (childNodes.length > 0)
    {
        for(var i=0;i<childNodes.length;i++)
        {
            setChildNodesChecked(childNodes[i],bChecked)
            childNodes[i].setChecked(bChecked);
        }
    }
}

//绑定TreeView的CheckBox事件 
function BindCheckBoxEventHanlder(tvID)
{
    var  inputs = document.getElementsByTagName("input"); 
    var j = 0; 
    for (var i=0;i<inputs.length;i++)
    {
        var input = inputs[i];
        if (input.type == "checkbox" && input.id.indexOf(tvID) != -1)
        {
            input.onclick = function(){onCheck();checkFatherCheckbox(event.srcElement);};
            j ++;
        }
    }
}

//子节点全选的话，父节点自动选中；或子节点都未选，父节点取消选中
function checkFatherCheckbox(objChk)
{
    if (objChk.checked) //选中
    {
        var objDiv = getParentDiv(objChk);//整个DIV
        if (objDiv == null) return;
        
        if (!isAllChildChecked(objDiv)) return;//所有子节点都选中了
        
        var objChk = getFirstCheckbox(objDiv);//得到DIV中第一个checkbox
        
        if (objChk == null) return;
        
        var id = objChk.id;
        
        var p = id.substr(0,id.lastIndexOf("n") + 1);
        
        var sn = id.substr(id.lastIndexOf("n") + 1);
        sn = sn.substr(0,sn.indexOf("CheckBox"));
        
        var n = parseInt(sn) - 1;
        
        var pCheckBox = document.getElementById(p + n.toString() + "CheckBox");
        if (pCheckBox) pCheckBox.checked = true;
    }
    else//未选中
    {
        var objDiv = getParentDiv(objChk);//整个DIV
        if (objDiv == null) return;
                
        var objChk = getFirstCheckbox(objDiv);//得到DIV中第一个checkbox
        
        if (objChk == null) return;
        
        var id = objChk.id;
        
        var p = id.substr(0,id.lastIndexOf("n") + 1);
        
        var sn = id.substr(id.lastIndexOf("n") + 1);
        sn = sn.substr(0,sn.indexOf("CheckBox"));
        
        var n = parseInt(sn) - 1;
        
        var pCheckBox = document.getElementById(p + n.toString() + "CheckBox");
        if (pCheckBox) pCheckBox.checked = false;
    }
}

//得到父级DIV
function getParentDiv(objElement)
{
    var div;
    if (objElement.parentElement.tagName.toLowerCase() != "div")
    {
        div = getParentDiv(objElement.parentElement);
    }
    else
    {
        return objElement.parentElement;
    }
    
    return div;
}

//CheckBox全选事件
function onCheck()
{
    var div = event.srcElement.parentElement.parentElement.parentElement.parentElement.nextSibling;
    if (div)
    {
        if (div.tagName == "DIV")
        {
            CheckChild(div,event.srcElement.checked);
        }
    }
}


//递归设置CheckBox Checked
function CheckChild(objdiv,checked)
{
    try
    {
        if (objdiv.childNodes)
        {
            var childNodes = objdiv.childNodes;
            if (childNodes.length > 0)
            {   
                for(var i = 0; i < childNodes.length ; i ++)
                {
                    var child = childNodes[i];
                    if (child.tagName == "DIV")
                    {
                        CheckChild(child,checked);
                    }
                    else if (child.tagName == "TABLE")
                    {
                        var childChecks = child.getElementsByTagName("input");
                        if (childChecks.length > 0)
                        {
                            childChecks[0].checked = checked;
                        }
                    }
                }
            }
        }
    }
    catch (e)
    {
        return;
    }
}

//递归查找指定DIV中是否所有的 Checkbox 都 Checked
function isAllChildChecked(objdiv)
{
    try
    {
        if (objdiv.childNodes)
        {
            var childNodes = objdiv.childNodes;
            if (childNodes.length > 0)
            {   
                for(var i = 0; i < childNodes.length ; i ++)
                {
                    var child = childNodes[i];
                    if (child.tagName == "DIV")
                    {
                        CheckChild(child,checked);
                    }
                    else if (child.tagName == "TABLE")
                    {
                        var childChecks = child.getElementsByTagName("input");
                        if (childChecks.length > 0)
                        {
                            if (!childChecks[0].checked) return false;
                        }
                    }
                }
            }
        }
        
        return true;
    }
    catch (e)
    {
        return false;
    }
}

//返回指定DIV中第一个checkbox
function getFirstCheckbox(objdiv)
{
    try
    {
        if (objdiv.childNodes)
        {
            var childNodes = objdiv.childNodes;
            if (childNodes.length > 0)
            {   
                for(var i = 0; i < childNodes.length ; i ++)
                {
                    var child = childNodes[i];
                    if (child.tagName == "DIV")
                    {
                        CheckChild(child,checked);
                    }
                    else if (child.tagName == "TABLE")
                    {
                        var childChecks = child.getElementsByTagName("input");
                        if (childChecks.length > 0)
                        {
                            return childChecks[0];
                        }
                    }
                }
            }
        }
        
        return null;
    }
    catch (e)
    {
        return null;
    }
}

//关闭弹出的Div层窗口，并重新载入父页，如果指定按钮的，模拟点击按钮
function modalWindowReloadParentPage(wid,buttons)
{
    if (buttons != null && buttons != "")
    {
        var arrButtons = buttons.split(",");
        
        var doc = dependentGetDoc(wid);
        if(doc != null)
        {
            for (var i=0;i<arrButtons.length;i++)
            {
                var button = doc.getElementById(arrButtons[i]);
                if (button != null)
                {
                    button.click();
                    break; 
                }
            }
        }        
    }
    else
    {
        var doc = dependentGetDoc(wid);
        if (doc) doc.location.reload();
    }
    
    return closeModalWindow(wid,true);
}

//解决TreeView控件IE7界面断线问题
function setTreeViewDivHeight(treeViewID)
{
	var treeview = document.body.all(treeViewID);
	var startIndex = treeview.sourceIndex;
	var begin = false;
	
	for(var i = startIndex;i < document.body.all.length - startIndex;i++)
    {
		var e = document.body.all(i);
		if (e.tagName == "DIV" 
		&& e.id == "" 
		&& e.style.width == "20px" 
		&& e.style.height == "1px")
		{
			e.style.height = "20px";
			begin = true;
		}
		if (begin && e.tagName == "DIV" && e.id != "" && e.id.indexOf(treeViewID) == -1)
		{
			break;
		}
	}
}

//得到指定名称的对象
function getObject(objName)
{
    var o = null;
    o = window.document.getElementById(objName);//当前文档对象
    if (o != null) return o;

	var pd = getDocument(true);//得到根
    
    o = pd.getElementById(objName);//根文档对象
    if (o != null) return o;
    
    //2
    if (pd.frames.length != 0)
    {
        for (i=0;i<pd.frames.length;i++)
        {
            o = pd.frames[i].self.window.document.getElementById(objName);//第二级
            if (o != null) return o;
            
            //3
            if (pd.frames[i].frames.length != 0)
            {
                for (j=0;j<pd.frames[i].frames.length;j++)
                {
                    o = pd.frames[i].frames[j].self.window.document.getElementById(objName);//第三级
                    if (o != null) return o;
                    
                    //4
                    if (pd.frames[i].frames[j].frames.length != 0)
                    {
                        for (q=0;q<pd.frames[i].frames[j].frames.length;q++)
                        {
                            o = pd.frames[i].frames[j].frames[q].self.window.document.getElementById(objName);//第四级
                            if (o != null) return o;
                        } 
                    } 
                } 
            } 
        }
    }
    
    return null;
}

//设置页默认焦点
function setPageFirstFocus()
{
	var e;
	
	for(var i = 0;i < window.document.forms[0].elements.length;i++)
	{
		e = window.document.forms[0].elements[i];
		if (e.readOnly != true && e.disabled != true && e.readOnly != "readonly" && e.disabled != "disabled" && e.style.display != "none" &&
		(e.type == "text" ||
		e.type == "password" ||
		e.type == "select-multiple" ||
		e.type == "textarea" || 
		e.type == "radio" || 
		e.type == "checkbox" || 
		e.type == "select-one"))
		{
			try
			{
				e.focus();
			}
			catch(e)
			{
			
			}
			break;
		}
	}
}

//得到GridView里选中行的ID
function findGridViewCheckedDataID(checkboxID)
{
	for(var i = 0;i < window.document.forms[0].elements.length;i++)
    {
	    e = window.document.forms[0].elements[i];
	    if (e.type == "checkbox" && e.id.indexOf(checkboxID) > -1 && e.checked)
	    {
	        return e.parentNode.dataID;
	    }
	}
	
	return null;
}

//用户自定义函数
function OnxWinClosed(onClosedFunArg)
{
    //alert(onClosedFunArg);
}

//控制文本框输入最大长度
function TextBoxMaxLength(maxLength)
{
	if (event.srcElement.value.length > maxLength) 
	{
		event.srcElement.value = event.srcElement.value.substring(0,maxLength);
	}
}

//判断页面的复选框是否选中
function IsChecked()
{  
    var CheckFlag = false;            
    var Check = document.getElementsByTagName("input");
    for(var i=0; i<Check.length; i++)
    {
        var e= Check[i];
        if((e.type=='checkbox') && e.id.indexOf("chkItem")!=-1 && e.checked)
        {
            CheckFlag = true;
            break;
        }
    }              
    return CheckFlag;
}

//判断页面的复选框是否选中
function IsCheckedSingle()
{           
    var Check = document.getElementsByTagName("input");    
    var count = 0;
    for(var i=0; i<Check.length; i++)
    {        
        var e= Check[i];
        if((e.type=="checkbox") && e.id.indexOf("chkItem")!=-1 && e.checked)
        {            
            count++;         
        }
    }         
    return count;    
}

function IsCheckedSingleTreeview()
{           
    var Check = document.getElementsByTagName("input");    
    var count = 0;
    for(var i=0; i<Check.length; i++)
    {        
        var e= Check[i];
        if((e.type=="checkbox") && e.checked)
        {            
            count++;         
        }
    }         
    return count;    
}


function GetCheckedItemID()
{
    for(var i = 0;i < window.document.forms[0].elements.length;i++)
    {
	    e = window.document.forms[0].elements[i];
	    if (e.type == "checkbox" && e.id.indexOf("chkItem") > -1 && e.checked)
	    {
	        return e.parentNode.dataID;
	    }
	}
	
	return null;
	
//    var Check = document.getElementsByTagName("input");    
//    var ID;
//    for(var i=0; i<Check.length; i++)
//    {        
//        var e= Check[i];
//        if((e.type=="checkbox") && e.id.indexOf("chkItem")!=-1 && e.checked)
//        {            
//            ID = e.parentNode;
//            alert(e.parentNode.dataID)
//        }
//    }         
//    return ID;
}



















//Gridview CheckBox全选
function SetAllChecked(CheckAll, chkItem)
{
    var CheckFlag = CheckAll.checked;            
    var Check = document.getElementsByTagName("input");
    for(var i=0; i<Check.length; i++)
    {
        var e = Check[i];
        if((e.type=='checkbox') && e.id.indexOf(chkItem)!=-1)
        {
            if (e.disabled == false)
            {
                e.checked = CheckFlag;
            }
        }
    }
}

//判断页面的复选框是否选中,如果有一个未选中，则全选框不打勾
function SetIsAllChecked(grid_ctl01_chkAll, eItem, chkItem)
{
    var CheckAll = document.getElementById(grid_ctl01_chkAll);
    if(!eItem.checked)
    {
        CheckAll.checked = false;
    }
    else
    {
        var rowCount = 0;
        var rowAmount = 0;
        var Check = document.getElementsByTagName("input");
        for(var i=0; i<Check.length; i++)
        {
            var e = Check[i];
            if((e.type=='checkbox') && e.id.indexOf(chkItem)!=-1)
            {
                rowAmount++;
                if (e.checked)
                {
                    rowCount++;
                }
            }
        }
        if (rowCount == rowAmount-1 && rowCount > 1)
        {
            CheckAll.checked = true;
        }
    }
}

//取得根目录
function getRootPath() {

    var strFullPath = window.document.location.href;

    var strPath = window.document.location.pathname;

    var pos = strFullPath.indexOf(strPath);

    var prePath = strFullPath.substring(0, pos);

    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);

    return (prePath + postPath);

}

