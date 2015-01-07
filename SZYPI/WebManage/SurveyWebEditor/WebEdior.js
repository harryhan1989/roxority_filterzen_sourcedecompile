


var ie = (window.navigator.userAgent.indexOf("MSIE") >= 1) || false;
var sRegExp_Int = /^[0-9|-][0-9]{0,}$/;
var EditorWin = null;
var _e;
var sPath = "";
function $(id) {
    return document.getElementById(id);
}
_outerHTML();

function getMouseLocation(e) {
    var mouseX = 0;
    var mouseY = 0;

    if (ie) {
        mouseX = e.x + document.body.scrollLeft;
        mouseY = e.y + document.body.scrollTop;
    } else {
        mouseX = e.layerX + document.body.scrollLeft;
        mouseY = e.layerY + document.body.scrollLeft;
    }
    return { x: mouseX, y: mouseY };
}


function getEvent() {
    if (document.all) return window.event;
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}



function _outerHTML() {
    if (typeof (HTMLElement) != "undefined" && !window.opera) {
        HTMLElement.prototype.__defineGetter__("outerHTML_", function() {
            var a = this.attributes, str = "<" + this.tagName.toLowerCase(), i = 0; for (; i < a.length; i++)
                if (a[i].specified)
                str += " " + a[i].name + '="' + a[i].value + '"';
            if (!this.canHaveChildren)
                return str + " />";
            return str + ">" + this.innerHTML + "</" + this.tagName.toLowerCase() + ">";
        });
        HTMLElement.prototype.__defineSetter__("outerHTML", function(s) {
            var r = this.ownerDocument.createRange();
            r.setStartBefore(this);
            var df = r.createContextualFragment(s);
            this.parentNode.replaceChild(df, this);
            return s;
        });
        HTMLElement.prototype.__defineGetter__("canHaveChildren", function() {
            return !/^(area|base|basefont|col|frame|hr|img|br|input|isindex|link|meta|param)$/.test(this.tagName.toLowerCase());
        });
    }
}






function addEvent(obj, sEvent, fn) {

    if (obj.attachEvent) {
        obj.attachEvent("on" + sEvent, fn);
    }
    else {
        obj.addEventListener(sEvent, fn, false);
    }
}


function OQSSEditorFF(sEditorID, sColorPancel, sMaskID, sInstanceName, sWebSource, blnEnEditCode) {
    var myEditor = null;
    var myEditorDoc = null;
    var ET;
    var editMode = 0;
    var currSelect = null;
    var currexecCommand = "";
    var i = 0;
    this._className = sInstanceName;

    currEditStatus = 0;
    var arrFont = ["Arial", "Arial Black", "MS Serif", "宋体", "楷体_GB2312", "隶书", "黑体", "仿宋_GB2312"];
    var arrSize = ["8px", "9px", "12px", "14px", "16px", "18px", "24px", "36px", "48px"];
    var arrTools = [["SwapEditStatus", "bold", "italic", "fontfamily", "fontsize", "Undo", "Redo", "JustifyLeft", "JustifyCenter", "JustifyRight", "Indent", "Outdent", "createlink", "unlink", "BackColor", "ForeColor", "underline", "strikethrough", "superscript", "subscript", "InertFile", "InsertHorizontalRule", "AddTable", "removeformat"], ["编辑状态-点击转换为代码编辑", "加粗", "斜体", "字体", "字号", "撤消", "重做", "左对齐", "右对齐", "居中", "左缩进", "右缩进", "超链接", "取消链接", "背景色", "前景色", "下划线", "删除线", "上标", "下标", "加入媒体", "横线", "插入表格", "删除格式"], ["编辑状态", "加粗", "斜体", "字体", "字号", "撤消", "重做", "左对齐", "右对齐", "居中", "左缩进", "右缩进", "超链接", "取消链接", "背景色", "前景色", "下划线", "删除线", "上划线", "上标", "下标", "加入表格", "加入媒体", "横线", "插入表格", "删除格式"]];
    this._initEditor = function() {
        var divObj = document.createElement("div");
        divObj.id = sEditorID + "_Box";
        try {
            $("EditorNode").appendChild(divObj);
        }
        catch (e) {
            document.body.appendChild(divObj);
        }
        divObj = document.createElement("fieldset");
        divObj.className = "TopWin";
        divObj.style.width = "400px";
        divObj.style.height = "110px";
        divObj.id = sEditorID + "_TopWin";
        document.body.appendChild(divObj);

        divObj = document.createElement("div");
        divObj.className = "ToolsBarMaskLayer";
        divObj.id = "ToolsBarMaskLayer";
        document.body.appendChild(divObj);

        divObj = document.createElement("div");
        divObj.className = "FileManageBox";
        divObj.id = "FileManageBox";
        document.body.appendChild(divObj);

        divObj = document.createElement("fieldset");
        divObj.className = "CreateTableBox";
        divObj.id = "TableForm";
        document.body.appendChild(divObj);

        divObj = document.createElement("div");
        divObj.className = "MaskLayer";
        divObj.id = sMaskID;
        document.body.appendChild(divObj);
        this._MaskObj = $(sMaskID);
        this._MaskStyleName = "MaskLayer";
        this._initMask(this._MaskObj, "#FFF");

        $("FileManageBox").innerHTML = "<div class='TableHead' id=CloseWinBT title='关闭窗口'><div style='line-height:25px;margin-left:10px;font-weight:bold'>插入媒体</div><div class='lhan-close'></div></div><iframe hspace=0 scrolling=no src='' style='width:100%;height:375px' frameborder=0 marginheight=0 marginwidth=0 id='FileManageWin'></iframe>";
        $(sEditorID + "_TopWin").innerHTML = "<legend  style='margin:5px;margin-left:10px; backgroundColor:#EEE'>创建链接</legend><div>网址：<input type='text' id='InputLink' style='width:300px;' /></div><div><label><input type='checkbox' id='LinkTarget' />在新窗口打开</label></div><div style='margin:5px'><input type='button' value='确 定' id='DoLinkBT' style='margin-right:20px'   /><input type='button' value='取 消' id='CancelCreateLink' /></div>";

        $("TableForm").innerHTML = "<legend  style='margin:5px;margin-left:10px; backgroundColor:#EEE'>插入表格</legend><fieldset style='margin-bottom:10px'><legend>表格</legend><input type=text id='TableRow' value='2' style='width:20px' />行<input type=text id='TableColumn' value=2 style='width:20px' />列<input type=text id='TableBackColor' readonly   value=#FFF style='width:60px' />背景色 <input type=text id='TableLineColor' readonly  size=6 value=#000 style='width:60px' />线颜色 线宽<input type='text' id='TableLineWidth'  value=1  size='2' style='width:20px' />px 宽度<input type=text id=TableWidth style='width:30px' value=300 />px</fieldset> <fieldset><legend>    单元格</legend>线色<input type=text id='CellLineColor' readonly  size=6 value=#000000 style='width:60px' />    背景色<input type=text id='CellBackColor' readonly  value=#FFF style='width:60px' />    线宽<input type='text' id='CellLineWidth' value=1  style='width:20px' />px    内边距<input type='text' id='CellLineDis' maxlength='2' value=2 style='width:20px' />px</fieldset><div style='margin:5px;margin-top:20px'><input type='button' value=' 确 定 ' style='margin-right:10px' id='DoTableBT' /><input type='button' value=' 取 消 ' id='CancenTableBT' /></div>";

        var sEditor = "";
        var sToolBar = "";
        var sTemp = "";
        for (i = 0; i < arrTools[0].length; i++) {
            switch (arrTools[0][i]) {
                case "SwapEditStatus":
                    sToolBar += "<div  onmouseover='this.className=\"BT_Over\"'   onmouseout='this.className=\"BT_Normal\"'   class='BT_Normal'><div id='BT" + i + "'  class='ModeEdit' title='" + arrTools[1][i] + "'></div></div>";
                    break;
                case "fontfamily":
                    for (var j = 0; j < arrFont.length; j++) {
                        sTemp += "<option value='" + arrFont[j] + "'>" + arrFont[j] + "</option>";
                    }
                    sToolBar += "<div id=BT" + i + " style='float:left;margin-left:2px' class='lhan-" + arrTools[0][i] + "'><select id='fontfamily' style='width:85px;height:19px'>" + sTemp + "</select></div>";
                    sTemp = "";
                    break;
                case "fontsize":
                    for (var j = 0; j < arrSize.length; j++) {
                        sTemp += "<option value='" + arrSize[j] + "'>" + arrSize[j] + "</option>";
                    }
                    sToolBar += "<div id=BT" + i + " style='float:left;margin-left:2px'  class='lhan-" + arrTools[0][i] + "'><select id='fontsize'  style='width:50px;height:19px;overflow:hidden'>" + sTemp + "</select></div>";
                    sTemp = "";
                    break;
                default:
                    sToolBar += "<div  onmouseover='this.className=\"BT_Over\"'   onmouseout='this.className=\"BT_Normal\"'   class='BT_Normal'><div id='BT" + i + "' style='width:16px;height:16px'  class='lhan-" + arrTools[0][i] + "' title='" + arrTools[1][i] + "'></div></div>";
                    break;
            }
        }

        sEditor = "<iframe   id=" + sEditorID + "  class='TargetWin'  frameborder='0' marginheight='5' marginwidth='5' onload='" + sInstanceName + ".openeditor()'></iframe>";

        $(sEditorID + "_Box").innerHTML = "<div id='ToolsBar' style='background-color:#FFF;height:20px'>" + sToolBar + "</div><div style='padding:5px; background-color:#EEE;margin:0px; width:99%'>" + sEditor + "</div></div>";

        if (ie) {
            for (i = 0; i < arrTools[0].length; i++) {
                $("BT" + i).unselectable = "on";
            }
        }

        myEditor = $(sEditorID);
        myEditorDoc = myEditor.contentWindow.document;
        this.InitContainerPanel();
        this.initocolor();
        _switchToolsBarMask();
        $("MyEditor").src = sWebSource;
    }

    function _switchToolsBarMask(objToolsBarMask) {
        if ("undefined" == typeof (objToolsBarMask)) {
            objToolsBarMask = $("ToolsBarMaskLayer");
        }
        var _P = getPosi($("BT0"));
        objToolsBarMask.style.left = (_P.intPosiLeft + $("BT0").offsetWidth) + "px";
        objToolsBarMask.style.top = _P.intPosiTop + "px";
        objToolsBarMask.style.height = ($("ToolsBar").offsetHeight) + "px";
        objToolsBarMask.style.width = ($("ToolsBar").offsetWidth - $("BT0").offsetWidth - 7) + "px";
        if (objToolsBarMask.style.display == "none") {
            objToolsBarMask.style.display = "block"
            $("BT0").className = "ModeCode";
        }
        else {
            objToolsBarMask.style.display = "none";
            $("BT0").className = "ModeEdit";
        }
    }

    this.openeditor = function() {
        $(sEditorID).contentWindow.document.designMode = 'on';
    }


    this._openFileManage = function() {
        return function() {
            _openMask($(sMaskID));
            $("FileManageWin").src = "../survey/filemanage.aspx";
            $("FileManageBox").style.display = "block";
            _centerObj($("FileManageBox"), 0, 0);
            currSelect = ie ? myEditor.contentWindow.document.selection.createRange() : myEditor.contentWindow.document;
        }
    }

    this._closeFileManage = function() {
        return function() {
            _closeMask($(sMaskID));
            $("FileManageBox").style.display = "none";
        }
    }

    this._getHTMLContent = function() {
        var sResult = "";
        if (currEditStatus == 0) {
            sResult = myEditor.contentWindow.document.body.innerHTML;
        }
        else {
            sResult = ie ? myEditor.contentWindow.document.body.innerText : myEditor.contentWindow.document.body.textContent;
        }

        return sResult;
    }


    this._setEditorContentWinSize = function(h, ph, w, pw) {
        var P = this._getShowSize();
        if (h == -1) {
            if (ph == -1) {
                ph = $("ToolsBar").offsetHeight + 15;
            }
            $(sEditorID).style.height = (P.h - ph) + "px";
        }
        else if (h >= 0) {
            $(sEditorID).style.height = h + "px";
        }
        if (w == -1) {
            if (pw == -1) {
                pw = $("ToolsBar").offsetWidth + 15;
            }
            $(sEditorID).style.width = (P.w - pw) + "px";
        }
        else if (w >= 0) {
            $(sEditorID).style.width = w + "px";
        }
    }

    this._initBT = function(outerObj) {
        addEvent($("DoLinkBT"), "click", eval(this._className + "._DoLink()"));
        addEvent($("CancelCreateLink"), "click", eval(this._className + "._DoLink()"));
        addEvent($("Color_WorkArea"), "mouseover", eval(this._className + ".doOver()"));
        addEvent($("Color_WorkArea"), "click", eval(this._className + ".doclick()"));
        addEvent($("CellLineColor"), "click", outerObj._setColor('CLC'));
        addEvent($("CellBackColor"), "click", outerObj._setColor('CBC'));
        addEvent($("TableBackColor"), "click", outerObj._setColor('TBC'));
        addEvent($("TableLineColor"), "click", outerObj._setColor('TLC'));
        addEvent($("DoTableBT"), "click", outerObj._doTable('Do'));
        addEvent($("CancenTableBT"), "click", outerObj._doTable('Cancel'));
        addEvent($("CloseWinBT"), "click", outerObj._closeFileManage());
        for (i = 0; i < arrTools[0].length; i++) {
            switch (arrTools[0][i]) {
                case "BackColor":
                    addEvent($("BT" + i), "click", outerObj._setColor('BackColor'));
                    break;
                case "AddTable":
                    addEvent($("BT" + i), "click", outerObj._CreateTable());
                    break;
                case "InertFile":
                    addEvent($("BT" + i), "click", outerObj._openFileManage());
                    break;
                case "ForeColor":
                    addEvent($("BT" + i), "click", outerObj._setColor('ForeColor'));
                    break;
                case "fontsize":
                    addEvent($("fontsize"), "change", outerObj._FS($("fontsize"))); break;
                case "fontfamily":
                    addEvent($("fontfamily"), "change", outerObj._doCommand("fontname", false, eval('$(\"fontfamily\").options[$(\"fontfamily\").selectedIndex].value')));
                    break;
                case "createlink":
                    addEvent($("BT" + i), "click", outerObj._CreateLink());
                    break;
                case "SwapEditStatus":
                    addEvent($("BT" + i), "click", outerObj._switchMode());
                    break;
                default:
                    addEvent($("BT" + i), "click", outerObj._doCommand(arrTools[0][i], false, arrTools[0][i]));

                    break;

            }
        }
        addEvent(window, "resize", outerObj._resize());
    }

    this._doTable = function(v) {
        return function() {
            if (v == "Cancel") {
                $("TableForm").style.display = "none";
                _closeMask($(sMaskID));
                return;
            }

            var intRow = parseInt($("TableRow").value);
            var intColumn = parseInt($("TableColumn").value);
            var sTBC = $("TableBackColor").value;
            var sTLC = $("TableLineColor").value;
            var sTLW = parseInt($("TableLineWidth").value);
            var intTableWidth = parseInt($("TableWidth").value);

            var sCBC = $("CellBackColor").value;
            var sCLC = $("CellLineColor").value;
            var sCLW = parseInt($("CellLineWidth").value);
            var sCLD = $("CellLineDis").value;

            if (!sRegExp_Int.test(intRow)) {
                alert("行数必须是整数");
                $("TableRow").focus();
                return;
            }

            if (!sRegExp_Int.test(intTableWidth)) {
                alert("表格宽度必须是整数");
                $("TableWidth").focus();
                return;
            }

            if (intRow <= 0) {
                alert("行数必须大于0");
                $("TableRow").focus();
                return;
            }

            if (!sRegExp_Int.test(intColumn)) {
                alert("列数必须是整数");
                $("TableColumn").focus();
                return;
            }

            if (intColumn <= 0) {
                alert("列数必须大于0");
                $("TableColumn").focus();
                return;
            }

            var sTable = createTableHTML(intRow, intColumn, sTBC, sTLC, sTLW, sCBC, sCLC, sCLD, sCLW, intTableWidth);
            var _s;
            var _t;
            !ie ? currSelect.execCommand("inserthtml", false, sTable) : currSelect.pasteHTML(sTable);

            $("TableForm").style.display = "none";
            _closeMask($(sMaskID));

        }
    }

    function createTableHTML(r, c, tbc, tlc, tlw, cbc, clc, cld, clw, tw) {
        var t1, t2;
        var sResult = "";
        var tdstyle = "style='border:" + clw + "px " + clc + " solid;background-color:" + cbc + ";padding:" + cld + "px'";
        for (t1 = 0; t1 < r; t1++) {
            sResult += "<tr>";
            for (t2 = 0; t2 < c; t2++) {
                sResult += "<td " + tdstyle + "></td>";
            }
            sResult += "</tr>";
        }
        return "<table width=100 style='border:" + tlw + "px solid " + tlc + ";background-color:" + tbc + ";width:" + tw + "px' cellpadding=0 cellspacing=0>" + sResult + "</table>";
    }

    this._switchMode = function() {
        return function() {
            if (currEditStatus == 0) {
                if (!blnEnEditCode) {
                    alert("你没有:\nHTML源码编辑权限");
                    return;
                }
                ie ? (myEditor.contentWindow.document.body.innerText = myEditor.contentWindow.document.body.innerHTML) : (myEditor.contentWindow.document.body.textContent = myEditor.contentWindow.document.body.innerHTML);
                $("BT0").title = "切换为可视化编辑";
                _switchToolsBarMask($("ToolsBarMaskLayer"));
                currEditStatus = 1;
            }
            else {


                myEditor.contentWindow.document.body.innerHTML = ie ? myEditor.contentWindow.document.body.innerText : myEditor.contentWindow.document.body.textContent;
                $("BT0").title = "切换为代码编辑";
                _switchToolsBarMask($("ToolsBarMaskLayer"));
                currEditStatus = 0;
            }
        }

    }

    this._TD = function(_o) {
        var _s = myEditor.contentWindow.getSelection();
        var _t = document.createElement("span");
        _t.style.textDecoration = _o.options[_o.selectedIndex].value;
        _s.getRangeAt(0).surroundContents(_t);
    }


    this._AT = function(sText) {
        ET.execCommand('InsertHtml', '', "<table border='1'><tr><td>this is table</td><tr></table>")
    }

    this._CreateTable = function() {
        return function() {
            _openMask($(sMaskID));
            _openTableForm(ie ? arguments[0].srcElement : arguments[0].target);
            currexecCommand = "CreateTable";
            myEditor.contentWindow.focus();
            currSelect = ie ? myEditor.contentWindow.document.selection.createRange() : myEditor.contentWindow.document;
        }
    }


    this.doclick = function(e) {
        return function() {
            e = getEvent();
            var EE = ie ? e.srcElement : e.target;
            if (EE.tagName == "TD") {
                switch (currexecCommand) {
                    case "TLC":
                        $("TableLineColor").value = rgbTohex(EE._background);
                        $(sColorPancel).style.display = "none";
                        return;
                        break;
                    case "TBC":
                        $("TableBackColor").value = rgbTohex(EE._background);
                        $(sColorPancel).style.display = "none";
                        return;
                        break;
                    case "CLC":
                        $("CellLineColor").value = rgbTohex(EE._background);
                        $(sColorPancel).style.display = "none";
                        return;
                        break;
                    case "CBC":
                        $("CellBackColor").value = rgbTohex(EE._background);
                        $(sColorPancel).style.display = "none";
                        return;
                        break;
                    default:
                }



                if (ie) {
                    myEditor.contentWindow.document.selection.createRange().execCommand(currexecCommand, false, EE._background);
                    $(sColorPancel).style.display = "none";
                }
                else {
                    var _s = myEditor.contentWindow.getSelection();
                    var _t = document.createElement("span");
                    if (currexecCommand == "ForeColor") {

                        _t.style.color = EE._background;
                    }
                    else if (currexecCommand == "BackColor") {
                        _t.style.backgroundColor = EE._background;
                    }

                    _s.getRangeAt(0).surroundContents(_t);
                }
                $(sColorPancel).style.display = "none";
            }
        }
    }


    function getPosi(obj) {
        var intTop = obj.offsetTop;
        var intLeft = obj.offsetLeft;
        while (obj = obj.offsetParent) {
            intTop += obj.offsetTop;
            intLeft += obj.offsetLeft;
        }
        return { intPosiTop: intTop, intPosiLeft: intLeft }
    }


    this._doCommand = function(sCommand, bln, v) {
        return function() {
            myEditor.contentWindow.focus();
            ET = ie ? myEditor.contentWindow.document.selection.createRange() : myEditor.contentWindow.document;
            ET.execCommand(sCommand, bln, v);
        }
    }

    this._setColor = function(v) {
        return function() {
            _openColorPancel(ie ? arguments[0].srcElement : arguments[0].target);
            currexecCommand = v;
        }

    }

    function _openTableForm(os) {
        var _P = getPosi(os);
        $("TableForm").style.display = "block";
        _centerObj($("TableForm"), 0, -200);
    }

    function _openColorPancel(os) {
        var _P = getPosi(os);
        $(sColorPancel).style.display = "block";
        $(sColorPancel).style.top = (_P.intPosiTop + os.offsetHeight) + "px";
        $(sColorPancel).style.left = _P.intPosiLeft + "px";
    }


    this._FS = function(_o) {
        return function() {
            var _s = null;
            if (ie) {
                myEditor.contentWindow.focus();
                _s = myEditor.contentWindow.document.selection.createRange();
                _s.pasteHTML("<span style='font-size:" + _o.options[_o.selectedIndex].value + "'>" + _s.text + "</span>");
            }
            else {
                _s = myEditor.contentWindow.getSelection();
                var _t = document.createElement("span");
                _t.style.fontSize = _o.options[_o.selectedIndex].value;
                _s.getRangeAt(0).surroundContents(_t);
            }

        }
    }



    this._CreateLink = function() {
        return function() {
            _openMask($(sMaskID));
            $(sEditorID + "_TopWin").style.display = "block";
            _centerObj($(sEditorID + "_TopWin"), 0, -200);
            currSelect = ie ? myEditor.contentWindow.document.selection.createRange() : myEditor.contentWindow.document;
        }
    }

    this._PasteHTML = function(sHTML) {
        if (!ie) {
            myEditor.contentWindow.focus();
            _s = myEditor.contentWindow.document;
            _s.execCommand("InsertHtml", false, sHTML);


        }
        else {
            myEditor.contentWindow.focus();
            _s = myEditor.contentWindow.document.selection.createRange();
            _s.pasteHTML(_t);
        }
    }

    this._DoLink = function() {
        return function() {
            var eventSource = ie ? arguments[0].srcElement : arguments[0].target;

            if (eventSource.id == "DoLinkBT") {
                var _s;
                var _t;
                if (!ie) {
                    _s = myEditor.contentWindow.getSelection();
                    _t = document.createElement("a");
                    _t.href = $("InputLink").value;
                    _t.target = $("LinkTarget").checked ? "_blank" : "_self";
                    _s.getRangeAt(0).surroundContents(_t);
                    currSelect.execCommand("createlink", false, $("InputLink").value);
                }
                else {
                    _s = currSelect;
                    _t = ($("LinkTarget").checked) ? "<a href='" + $("InputLink").value + "'  target='_blank'>" + _s.text + "</a>" : _t = "<a href='" + $("InputLink").value + "'>" + _s.text + "</a>";
                    _s.pasteHTML(_t);
                    _s.select();
                }
            }
            $(sEditorID + "_TopWin").style.display = "none";
            _closeMask($(sMaskID));
        }
    }



    this._OpenWin = function(targetWin, par) {
        var sResult = window.showModalDialog(targetWin, "", "dialogWidth=410px;dialogHeight=160px;status=0");
        //alert(sResult);
    }





    var ColorHex = new Array('00', '33', '66', '99', 'CC', 'FF');
    var SpColorHex = new Array('ff0', '0f0', '00f', 'ff0', '0ff', 'f0f');
    var current = null;




    this.initocolor = function() {
        var colorTable = '';
        for (i = 0; i < 2; i++) {
            for (j = 0; j < 6; j++) {
                colorTable = colorTable + '<tr style="height:12px">';
                colorTable = colorTable + '<td  style="border:1px solid #000;width:11px;background-color:#000"  unselectable="on">';
                if (i == 0) {
                    colorTable = colorTable + '<td style="border:1px solid #000;width:11px;background-color:#' + ColorHex[j] + ColorHex[j] + ColorHex[j] + '"  unselectable="on">';
                }
                else {
                    colorTable = colorTable + '<td style="border:1px solid #000;width:11px;background-color:#' + SpColorHex[j] + '"  unselectable="on">';
                }
                colorTable = colorTable + '<td style="border:1px solid #000;width:11px;background-color:#000"  unselectable="on">';
                for (k = 0; k < 3; k++) {
                    for (l = 0; l < 6; l++) {
                        colorTable = colorTable + '<td style="border:1px solid #000;width:11px;background-color:#' + ColorHex[k + i * 3] + ColorHex[l] + ColorHex[j] + '"  unselectable="on">';
                    }
                }
            }
        }
        colorTable = '<table  border="0" cellspacing="0" cellpadding="0" style="border:1px #000 solid;width:253px;" id="colortable">'
					+ '<tr style="height:30px"><td colspan=21 style="background-color:#EEE">'
			   		+ '<table cellpadding="0" cellspacing="0" border="0">'
			   		+ '<tr><td width="3"><td><input type="text" name="DisColor" id="DisColor" size="6" disabled style="border:solid 1px #000;background-color:#FF0"><div class="lhan-close" onclick=document.getElementById("' + sColorPancel + '").style.display="none"></div></td>'
					+ '<td width="3"><td><input type="text" name="HexColor" id="HexColor" size="7" style="border:inset 1px;font-family:Arial;" value="#000"></td></tr></table></td></table>'
					+ '<table  cellspacing="0" cellpadding="0" style="border-collapse: collapse;cursor:pointer;" id="Color_WorkArea">'
					+ colorTable + '</table>';
        document.getElementById(sColorPancel).innerHTML = colorTable;
        //$("colortable").unselectable = "on";
    }


    function rgbTohex(sColor) {
        if (sColor.indexOf("#") >= 0) return sColor; //如果是一个hex值则直接返回
        var pattern = new RegExp("2[0-4]\\d|25[0-5]|[01]?\\d\\d?", "ig"); //这个正则是取 0 ~ 255的数字
        var va = sColor.match(pattern);
        if (va.length != 3) return "NaN"; //取出的数组长度一定得为3
        var result = "#";
        for (var i = 0; i < 3; i++) {
            var num = parseInt(va[i]);
            result += num < 16 ? "0" + num.toString(16) : num.toString(16); //如果小于F在前面补0
        }
        return result;
    }

    this.doOver = function(e) {
        return function() {
            e = getEvent();
            var EE = document.all ? e.srcElement : e.target;
            if ((EE.tagName == "TD") && (current != EE)) {
                if (current != null) { current.style.backgroundColor = current._background; }
                EE._background = EE.style.backgroundColor;
                $("DisColor").style.backgroundColor = ie ? EE.style.backgroundColor : rgbTohex(EE.style.backgroundColor);
                $("HexColor").value = ie ? EE.style.backgroundColor : rgbTohex(EE.style.backgroundColor);
                current = EE;
            }
        }
    }

    this.openColorPancel = function() {

        $(sColorPancel).style.display = "block";
    }




    this.InitContainerPanel = function() {
        var div = document.createElement("div");
        div.id = sColorPancel;
        div.className = "ColorPancel";
        document.body.appendChild(div);

    }

    this.addEvent = function(obj, sEvent, fn) {
        if (obj.attachEvent) {
            obj.attachEvent("on" + sEvent, fn);
        }
        else {
            obj.addEventListener(sEvent, fn, false);
        }
    }





    this._MaskStyleName = "";
    this._MaskObj = null;
    this._MaskLayerZIndex = 1000;
    this._arrScreenInfo = new Array();

    this._centerObj = function(obj, _X, _Y) {
        var P = this._getShowSize();

        obj.style.left = (P.w + _X - parseInt(obj.style.width.replace("px", ""))) / 2 + "px";
        obj.style.top = (P.h + _Y - parseInt(obj.style.height.replace("px", ""))) / 2 + "px";

    }

    function _centerObj(obj, _X, _Y) {

        var P = _getShowSize();
        obj.style.left = (P.w + _X - parseInt(obj.offsetWidth.toString().replace("px", ""))) / 2 + "px";
        obj.style.top = (P.h + _Y - parseInt(obj.offsetHeight.toString().replace("px", ""))) / 2 + "px";
    }

    this._getShowSize = function() {
        var H = 0;
        var W = 0;

        if (ie) {
            H = document.body.parentElement.clientHeight;
            W = document.body.parentElement.clientWidth;

        }
        else {
            H = window.innerHeight;
            W = window.innerWidth;
        }

        return { w: W, h: H };
    }

    function _getShowSize() {
        var H = 0, W = 0;
        if (ie) {
            H = document.body.parentElement.clientHeight;
            W = document.body.parentElement.clientWidth;
        }
        else {
            H = window.innerHeight;
            W = window.innerWidth;
        }
        return { w: W, h: H };
    }

    this._initMask = function(objMask, sColor) {
        if ((typeof (objMask) == "undefined") || (objMask == "")) {
            objMask = this._MaskObj;

        }
        var P = this._getShowSize();
        objMask.style.left = "0px";
        objMask.style.top = "0px";
        objMask.style.width = P.w + "px";
        objMask.style.height = P.h + "px";

        objMask.className = this._MaskStyleName;
        objMask.style.index = this._MaskLayerZIndex;
        objMask.style.position = "absolute";


    }

    this._resize = function() {
        return function() {
            var P = _getShowSize();
            objMask = $("OQSSEditorMask");
            objMask.style.left = "0px";
            objMask.style.top = "0px";
            objMask.style.width = P.w + "px";
            objMask.style.height = P.h + "px";
            var _P = getPosi($("BT0"));
            var objToolsBarMask = $("ToolsBarMaskLayer")
            objToolsBarMask.style.left = (_P.intPosiLeft + $("BT0").offsetWidth) + "px";
            objToolsBarMask.style.top = _P.intPosiTop + "px";
            objToolsBarMask.style.height = ($("ToolsBar").offsetHeight) + "px";
            objToolsBarMask.style.width = ($("ToolsBar").offsetWidth - $("BT0").offsetWidth - 7) + "px";

        }
    }



    this._openMask = function(objMask) {
        if ((typeof (objMask) == "undefined") || (objMask == "") || (objMask == null)) {
            objMask = this._MaskObj;
        }
        var P = this._getShowSize();
        objMask.style.left = "0px";
        objMask.style.top = "0px";
        objMask.style.width = P.w + "px";
        objMask.style.height = P.h + "px";
        objMask.style.display = 'block';
    }

    function _openMask(objMask) {
        var P = _getShowSize();
        objMask.style.left = "0px";
        objMask.style.top = "0px";
        objMask.style.width = P.w + "px";
        objMask.style.height = P.h + "px";
        objMask.style.display = 'block';
    }

    this._openObj = function(obj, _W, _H, _WW) {



        var P = this._getShowSize();
        if (_WW > 0) {
            obj.style.width = _WW + "px";
        }
        else {
            obj.style.width = P.w + _W + "px";
        }
        obj.style.height = P.h + _H + "px";


    }

    this._closeMask = function(objMask) {
        if ((typeof (objMask) == "undefined") || (objMask == "")) {
            objMask = this._MaskObj;

        }
        objMask.style.display = 'none';

    }

    function _closeMask(objMask) {
        if ((typeof (objMask) == "undefined") || (objMask == "") || (objMask == null)) {
            objMask = this._MaskObj;

        }
        objMask.style.display = 'none';

    }
    this.addMedia = function(f) {

        addMedia2(f);

    }
    function addMedia2(sAtt) {
        var sExpandName = "";
        var sResult = "";
        var sFilePath = "";
        var sExpandNameList = ".jpg.jpeg.gif.png.bmp.doc.ppt.avi.swf.pdf.xls.wmv.rm.rmvb.mp3.wma.txt";

        sAtt = sAtt.toLowerCase();
        sExpandName = sAtt.substr(sAtt.lastIndexOf(".") + 1)
        sFilePath = sPath + sAtt;
        switch (sExpandName) {
            case "jpg": case "gif": case "jpeg": case "png": case "bmp":

                sResult = "<img src=" + sFilePath + ">";
                break;
            case "pdf": case "doc": case "xls": case "ppt": case "txt": case "rar": case "zip":

                sResult = "<a href=" + sFilePath + " target=_blank><img src=images/File_" + sExpandName + ".gif></a>";
                break;
            case "swf":
                sResult = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0">  <param name="movie" value=' + sFilePath + '>  <param name="quality" value="high">  <embed src=' + sFilePath + ' quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash"></embed></object>'
                break;
            case "rm": case "rmvb":
                sResult = '<object width="320" height="250" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"> <param name="CONTROLS" value="ImageWindow"> <param name="CONSOLE" value="Video"> <param name="CENTER" value="TRUE"> <param name="MAINTAINSPECT" value="TRUE"> </object><object width="320" height="30" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"> <param name="CONTROLS" value="StatusBar"> <param name="CONSOLE" value="Video"> </object> <object width="320" height="30" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"> <param name="CONTROLS" value="ControlPanel"><param name="CONSOLE" value="Video"> <param name="SRC" value="' + sFilePath + '"> <param name="AUTOSTART" value="TRUE"> <param name="PREFETCH" value="0"> <param name="LOOP" value="0"> <param name="NUMLOOP" value="0"> </object> '
                break;
            case "mp3":
                sResult = '<EMBED style="WIDTH: 170px; HEIGHT: 42px" pluginspage=http://www.apple.com/quicktime/ src=' + sFilePath + ' width=178 height=42 type=audio/mpeg targetcache="false" kioskmode="false" bgcolor="#000000" scale="TOFIT" cache="false" playeveryframe="false" controller="true" loop="false" autoplay="true">';
                break;
            case "wmv":
            case "wma":
            case "mp3":
            case "avi":

                sResult = '<embed autostart="false" loop="-1" controls="ControlPanel" width="400" height="300" src="' + sAtt + '";>';
                break;

            case "mpg": case "mpeg":
                sResult = '<object classid="clsid:05589FA1-C356-11CE-BF01-00AA0055595A" id="ActiveMovie1" width="239" height="250"><param name="Appearance" value="0"><param name="AutoStart" value="-1"><param name="AllowChangeDisplayMode" value="-1"><param name="AllowHideDisplay" value="0"><param name="AllowHideControls" value="-1"><param name="AutoRewind" value="-1"><param name="Balance" value="0"><param name="CurrentPosition" value="0"><param name="DisplayBackColor" value="0"><param name="DisplayForeColor" value="16777215"><param name="DisplayMode" value="0"><param name="Enabled" value="-1"><param name="EnableContextMenu" value="-1"><param name="EnablePositionControls" value="-1"><param name="EnableSelectionControls" value="0"><param name="EnableTracker" value="-1"><param name="Filename" value="' + sFilePath + ' valuetype="ref"><param name="FullScreenMode" value="0"><param name="MovieWindowSize" value="0"><param name="PlayCount" value="1"><param name="Rate" value="1"><param name="SelectionStart" value="-1"><param name="SelectionEnd" value="-1"><param name="ShowControls" value="-1"><param name="ShowDisplay" value="-1"><param name="ShowPositionControls" value="0"><param name="ShowTracker" value="-1"><param name="Volume" value="-480"></object>';
            default:
        }
        !ie ? currSelect.execCommand("inserthtml", false, sResult) : currSelect.pasteHTML(sResult);
        _closeMask($(sMaskID));
        $("FileManageBox").style.display = "none";


    }


}

//-----------------------------------------------------------------


/*
String.prototype.hexColor = function(){
if(this.indexOf("#") >= 0) return this;//如果是一个hex值则直接返回
var pattern = new RegExp("2[0-4]\\d|25[0-5]|[01]?\\d\\d?","ig");//这个正则是取 0 ~ 255的数字
var va = this.match(pattern);
if(va.length != 3) return "NaN";//取出的数组长度一定得为3
var result = "#";
for(var i = 0; i < 3; i++) {
var num = parseInt(va[i]);
result += num < 16 ? "0" + num.toString(16) : num.toString(16);//如果小于F在前面补0
}
return result;
}*/





/*以前认为WEB的在线编辑器无非就是对输入内容的替换以及快捷的插入HTML代码，但是做的时候却发现虽然原理是那样，但是实现方法并非我想的那么死板。由于很少做UI上的东西所以到现在才知道在document中有execCommand方法可以解决插入HTML标签的问题，这个方法可以在光标所在位置插入需要的HTML标签，并且要注意的是，如果在一个限制的范围内插入标签需要先让该范围获得焦点，例如：
a.focus();
a.document.execCommand('insertButton','','btn');
这里的a对象是一个iframe对象。 execcommand中的第一个参数是需要插入的控件的命令，第2个目前还不知道，第3个是ID名，这里将插入一个ID=btn的BUTTON控件，如果想加入其他属性只需要接着后面写就行了，例如
a.document.execCommand('insertButton','','btn class=btnclass color=red');
 
第一个参数的详细列表
2D-Position 允许通过拖曳移动绝对定位的对象。
AbsolutePosition 设定元素的 position 属性为“absolute”(绝对)。
BackColor 设置或获取当前选中区的背景颜色。
BlockDirLTR 目前尚未支持。
BlockDirRTL 目前尚未支持。
Bold 切换当前选中区的粗体显示与否。
BrowseMode 目前尚未支持。
Copy 将当前选中区复制到剪贴板。
CreateBookmark 创建一个书签锚或获取当前选中区或插入点的书签锚的名称。
CreateLink 在当前选中区上插入超级链接，或显示一个对话框允许用户指定要为当前选中区插入的超级链接的 URL。
Cut 将当前选中区复制到剪贴板并删除之。
Delete 删除当前选中区。
DirLTR 目前尚未支持。
DirRTL 目前尚未支持。
EditMode 目前尚未支持。
FontName 设置或获取当前选中区的字体。
FontSize 设置或获取当前选中区的字体大小。
ForeColor 设置或获取当前选中区的前景(文本)颜色。
FormatBlock 设置当前块格式化标签。
Indent 增加选中文本的缩进。
InlineDirLTR 目前尚未支持。
InlineDirRTL 目前尚未支持。
InsertButton 用按钮控件覆盖当前选中区。
InsertFieldset 用方框覆盖当前选中区。
InsertHorizontalRule 用水平线覆盖当前选中区。
InsertIFrame 用内嵌框架覆盖当前选中区。
InsertImage 用图像覆盖当前选中区。
InsertInputButton 用按钮控件覆盖当前选中区。
InsertInputCheckbox 用复选框控件覆盖当前选中区。
InsertInputFileUpload 用文件上载控件覆盖当前选中区。
InsertInputHidden 插入隐藏控件覆盖当前选中区。
InsertInputImage 用图像控件覆盖当前选中区。
InsertInputPassword 用密码控件覆盖当前选中区。
InsertInputRadio 用单选钮控件覆盖当前选中区。
InsertInputReset 用重置控件覆盖当前选中区。
InsertInputSubmit 用提交控件覆盖当前选中区。
InsertInputText 用文本控件覆盖当前选中区。
InsertMarquee 用空字幕覆盖当前选中区。
InsertOrderedList 切换当前选中区是编号列表还是常规格式化块。
InsertParagraph 用换行覆盖当前选中区。
InsertSelectDropdown 用下拉框控件覆盖当前选中区。
InsertSelectListbox 用列表框控件覆盖当前选中区。
InsertTextArea 用多行文本输入控件覆盖当前选中区。
InsertUnorderedList 切换当前选中区是项目符号列表还是常规格式化块。
Italic 切换当前选中区斜体显示与否。
JustifyCenter 将当前选中区在所在格式化块置中。
JustifyFull 目前尚未支持。
JustifyLeft 将当前选中区所在格式化块左对齐。
JustifyNone 目前尚未支持。
JustifyRight 将当前选中区所在格式化块右对齐。
LiveResize 迫使 MSHTML 编辑器在缩放或移动过程中持续更新元素外观，而不是只在移动或缩放完成后更新。
MultipleSelection 允许当用户按住 Shift 或 Ctrl 键时一次选中多于一个站点可选元素。
Open 打开。
Outdent 减少选中区所在格式化块的缩进。
OverWrite 切换文本状态的插入和覆盖。
Paste 用剪贴板内容覆盖当前选中区。
PlayImage 目前尚未支持。
Print 打开打印对话框以便用户可以打印当前页。
Redo 重做。
Refresh 刷新当前文档。
RemoveFormat 从当前选中区中删除格式化标签。
RemoveParaFormat 目前尚未支持。
SaveAs 将当前 Web 页面保存为文件。
SelectAll 选中整个文档。
SizeToControl 目前尚未支持。
SizeToControlHeight 目前尚未支持。
SizeToControlWidth 目前尚未支持。
Stop 停止。
StopImage 目前尚未支持。
StrikeThrough 目前尚未支持。
Subscript 目前尚未支持。
Superscript 目前尚未支持。
UnBookmark 从当前选中区中删除全部书签。
Underline 切换当前选中区的下划线显示与否。
Undo 撤消。
Unlink 从当前选中区中删除全部超级链接。
Unselect 清除当前选中区的选中状态。
 
另外，在当前鼠标所点的位置插入字符可用以下方法：
<script>
function showselect() {
var oText = document.selection.createRange();
oText.text=111;
}*/