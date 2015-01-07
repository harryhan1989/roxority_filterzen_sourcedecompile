
var face = new interface();
face._MaskStyleName = "BgWin";
var sState = "False";
var m1 = "问卷已经生成，此操作需要在经过页面编辑后生效。";
var m2 = "问卷已经生成，不能进行此操作\n如需进行此操作，可进行问卷重构!";
var sOpenPage = "block";
var sOpenItem = "block";
var blnActioned = "True";



function applyTemp() {
    setRightMenu("block");
    var objTargetWin = $("targetWin")
    if (window.frames.targetWin.sState == "True") {
        alert(m1);
    }
    initFace();
    window.parent.$("TitleName").innerHTML = " 应用模板";
    window.parent.$("SetWin").src = "applyTemp.aspx?SID=" + SID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}


function editItem1(SID1, PageNo) {
    if (window.frames.targetWin.sState == "True") {
        alert(m2);
        return;
    }
    optionActionWin('EditItem.aspx?SID=' + SID1 + '&PageNo=' + PageNo , '编辑题目', 571, 840, "No");

}

function getSurvey() {
    if (blnActioned == "True") {
        $("targetWin").src = "getSurvey.aspx?SID=" + SID + "&" + escape(new Date());
    }
    else {
        blnActioned = "False";
    }

}



function setRightMenu(blnSwitch) {
    var RightMenu = $("RightMenu");
    if (blnSwitch != null) {
        RightMenu.style.display = blnSwitch;
        return;
    }



    if ((RightMenu.style.display == "") || (RightMenu.style.display == "block")) {
        RightMenu.style.width = "0px";
        $("LeftWin").style.width = "100%";
        RightMenu.style.display = "none";

    }
    else {
        RightMenu.style.width = "25%";
        $("LeftWin").style.width = "75%";
        RightMenu.style.display = "block";
    }
}

function addPage(intFrontPage) {
    if (window.frames.targetWin.sState == "True") {
        alert(m2);
        return;
    }
    initFace();
    $("ActionPage").src = "AddPage.aspx?SID=" + SID + "&FrontPage=" + intFrontPage + "&" + escape(new Date());
}



function addWidth(d) {
    var objRigthMenu = $("RightMenu");
    var objLeftWin = $("LeftWin");
    var sRigthMenuWidth = objRigthMenu.style.width;
    var sLeftMenuWidth = objLeftWin.style.width;
    var intRigthMenuWidth = parseInt(sRigthMenuWidth.substring(0, sRigthMenuWidth.indexOf("%")))
    var intLeftMenuWidth = parseInt(sLeftMenuWidth.substring(0, sLeftMenuWidth.indexOf("%")))
    if (d == 0) {
        intRigthMenuWidth += 10;
        intLeftMenuWidth -= 10;
    }
    else {
        intRigthMenuWidth -= d * 10;
        intLeftMenuWidth += d * 10;
    }
    if ((intRigthMenuWidth >= 85) || (intRigthMenuWidth <= 15)) {
        return;
    }
    sRigthMenuWidth = intRigthMenuWidth + "%";
    sLeftMenuWidth = intLeftMenuWidth + "%";
    objRigthMenu.style.width = sRigthMenuWidth;
    objLeftWin.style.width = sLeftMenuWidth;
    $("SetWin").style.width = "100%";
}

function cancelOption() {
    window.$("RightMenuContent").style.display = "none";
    addWidth(4)
    window.parent.$("SetWin").src = "empty.htm";
}

function setSurveyStyle() {
    if (window.frames.targetWin.sState == "True") {
        alert(m2);
        return;
    }
    var _P = face._getShowSize();
    //optionActionWin('ps.aspx?SID='+SID+'&'+escape(new   Date()),'预览问卷[<a href="ps.aspx?sid='+SID+'" target="_blank" style="color:#FF0000">在新窗口打开</a>]',_P.h,_P.w,"Yes",0,0,"auto",30);
    optionActionWin('setstyle.aspx?SID=' + SID + '&' + escape(new Date()), '编辑样式-CSS', _P.h, _P.w, "Yes", 0, 0, "auto", 30);

}

function ComplateSurveyEdit() {
    var objTargetWin = $("targetWin");
    if (window.frames.targetWin.arrItem.length <= 0) {
        alert("问卷中没有题目，不能完成编辑");
        return;
    }
    if (confirm("确定完成问卷编辑？\n点击确定将进入问卷整体外观编辑!") == true) {
        initFace();
        try {
            top.location.href = "EditSurveyHtml.aspx?SID=" + SID;
        }
        catch (e) {
        }

    }
    else {
        return;
    }

}

function showHelp() {
    setRightMenu("block");
    initFace();
    window.parent.$("TitleName").innerHTML = " 帮助";
    //window.parent.$("SetWin").src = "help/index.htm";
    window.parent.$("SetWin").src = "../help/h0.htm";
    window.parent.$("RightMenuContent").style.display = "block";
}


function HiddenItemSet(v) {
    setRightMenu("block");
    if (v != "") {
        window.frames["SetWin"].$("HiddenItem").checked = true;
    }
    else {
        window.frames["SetWin"].$("HiddenItem").checked = false;
    }
    window.frames["SetWin"].$("HiddenItem").value = v;

}

function URLVarSet(v) {
    setRightMenu("block");
    if (v != "") {
        window.frames["SetWin"].$("URLVar").checked = true;
    }
    else {
        window.frames["SetWin"].$("URLVar").checked = false;
    }
    window.frames["SetWin"].$("URLVar").value = v;

}

function surveyInfo() {
    setRightMenu("block");
    initFace();
    window.parent.$("TitleName").innerHTML = " 问卷概况";
    window.parent.$("SetWin").src = "SurveyInfo.aspx?SID=" + SID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function PointStat() {
    setRightMenu("block");
    initFace();
    window.parent.$("TitleName").innerHTML = " 评分表";
    window.parent.$("SetWin").src = "SetPointStat.aspx?SID=" + SID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}



function showAll(d) {
    var objTargetWin = $("targetWin")
    if (d == 0) {

        if (sOpenPage == "block") {
            sOpenPage = "none";
        }
        else {
            sOpenPage = "block";
        }
        for (i = 0; i < window.frames.targetWin.arrPageNo.length; i++) {

            window.frames.targetWin.showPage(window.frames.targetWin.arrPageNo[i])

        }

    }
    else {
        if (sOpenItem == "block") {
            sOpenItem = "none";
        }
        else {
            sOpenItem = "block";
        }
        for (i = 0; i < window.frames.targetWin.arrItem.length; i++) {

            window.frames.targetWin.showItem(window.frames.targetWin.arrItem[i][0])

        }
    }
}



function initFace() {
    face._initMask($("BgWin"), "#666666");
    face._openMask($("BgWin"));
    $("OpenWin").style.display = 'block';
}





function initOpenSurveyListWin() {
    optionActionWin('OpenSurveyList.aspx?&' + escape(new Date()), '打开问卷', 550, 840, "");
}




function openEdit() {
    $("OpenWin").style.display = 'none';
    face._closeMask($("BgWin"));
    return true;
}

//document.oncontextmenu=new Function("event.returnValue=false;"); 
//document.onselectstart=new Function("event.returnValue=false;"); 

function setDiv() {
    face._centerObj($("OpenWin"), 0, 0);
}


function setPar() {
    setRightMenu("block");
    initFace();
    window.parent.$("TitleName").innerHTML = " 参数设置";
    window.parent.$("SetWin").style.width = "100%";
    window.parent.$("SetWin").src = "SetPar.aspx?SID=" + SID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}


function openStyleLib() {
    if (window.frames.targetWin.sState == "True") {
        alert(m1);
    }
    optionActionWin('StyleLibList.aspx?SID=' + SID + '&' + escape(new Date()), '选择预定义样式', 500, 840, "Yes");
}


function init() {
    $("targetWin").src = "getSurvey.aspx?SID=" + SID;
    intPublicHeight = 34;
}

function optionActionWin(targetURL, sWinName, h, w, ShowTopBar, leftPosi, topPosi, TargetWinOverFlow, TargetWinOverFlowHeight) {
    face._openMask($("BgWin"));
//    try {
//        document.getElementById("ActionTarget").src = targetURL;
//    }
//    catch (e) {
    //        document.getElementById("ActionTarget").location.href = targetURL;
//    document.frames["ActionTarget"].location.href = targetURL;
    
//    }
//    if (TargetWinOverFlow != null) {
//        $("ActionTarget").style.overflow = TargetWinOverFlow;
//    }
//    else {
//        $("ActionTarget").style.overflow = "hidden";
//    }
//    $("ActionWinName").innerHTML = "<strong>" + sWinName + "</strong>";
    $("ActionWin").style.height = h + "px";
    $("ActionWin").style.width = w + "px";
//    if (TargetWinOverFlowHeight == null) {
//        $("ActionTarget").style.height = (h) + "px";
//    }
//    else {
//        $("ActionTarget").style.height = (h - TargetWinOverFlowHeight) + "px";
//    }
//    $("ActionTarget").style.width = w + "px";
//    face._centerObj($("ActionWin"), 0, 0);
//    $("OpenWin").style.display = 'block';
    x_open('<strong>' + sWinName + '</strong>', targetURL, w, h-25, "closefuncation:closeit();refresh();closeActionWin();$")
//    if (ShowTopBar == "No") {
//        $("TopBar").style.display = "none";
//    }
//    else {
//        $("TopBar").style.display = "block";
//    }
}

function complateActionWin() {
//    $("ActionWin").style.display = "block";
    $("OpenWin").style.display = 'none';
}


function closeActionWin() {
    $("OpenWin").style.display = 'none';
    $("ActionWin").style.display = "none";
    $("BgWin").style.display = 'none';
    $("ActionTarget").src = "";
    getSurvey();
}

function PageExpandOption() {
    var PagePosi = getPos($("PageOptionBT"));
    $("Layer3").style.display = "block";
    $("Layer1").style.left = PagePosi.x + "px";
    $("Layer1").style.top = (PagePosi.y + 20) + "px";
    $("Layer1").style.display = "block";


}


window.document.onmousemove = function() {
    try {
        if ($("Layer1").style.display == "none") {
            return;
        }
        var e = arguments[0] || window.event;
        var currID = (e.srcElement) ? e.srcElement.id : e.target.id;
        $("PageOptionBT").className = "EditSurveyToolsBarBT_Move";
        if ("$Layer1$PageOptionBT$PageOptionBT1$PageOptionBT2$PageOptionBT3$PageExpandOptionMenu$Bar1$Bar2$Bar3$DropMenuBT$".indexOf("$" + currID + "$") < 0) {
            $("Layer3").style.display = "none";
            $("Layer1").style.display = "none";
            $("PageOptionBT").className = "EditSurveyToolsBarBT_Normal";
        }
    }
    catch (e)
	{ }
}


function getPos(obj) {
    var P_Top = obj.offsetTop;
    var P_Left = obj.offsetLeft;
    var x = 0;
    var y = 0;
    while (obj = obj.offsetParent) {
        P_Top += obj.offsetTop;
        P_Left += obj.offsetLeft;
    }

    return { x: P_Left, y: P_Top }
}

function volumeAddPage() {
    $("Layer3").style.display = "none";
    $("Layer1").style.display = "none";
    $("PageOptionBT").className = "EditSurveyToolsBarBT_Normal";
    if (window.frames.targetWin.sState == "True") {
        alert(m2);
        return;
    }
    optionActionWin('volumeAddPage.aspx?SID=' + SID + '&' + escape(new Date()), '批量加页', 250, 400, "Yes");
}


function sortOutPage() {
    $("Layer3").style.display = "none";
    $("Layer1").style.display = "none";
    $("PageOptionBT").className = "EditSurveyToolsBarBT_Normal";
    if (window.frames.targetWin.sState == "True") {
        alert(m2);
        return;
    }
    optionActionWin('sortOutPage.aspx?SID=' + SID + '&' + escape(new Date()), '整理分页', 400, 600, "Yes");
}

function ps() {
    var _P = face._getShowSize();
    optionActionWin('ps.aspx?SID=' + SID + '&' + escape(new Date()), '预览问卷[<a href="ps.aspx?sid=' + SID + '" target="_blank" style="color:#FF0000">在新窗口打开</a>]', _P.h, _P.w, "Yes", 0, 0, "auto", 30);
}

window.onload = function() {
    openEdit();
    setDiv();
    init();
    var myface = new interface();
    var arr = myface._getShowSize();
    $("targetWin").style.height = (arr.h - 28) + "px";
    $("SetWin").style.height = (arr.h - 32 - 22) + "px";
    hiddenScrollBar();
}

window.onresize = function() {
    face._resizeMask($("BgWin"));
}

