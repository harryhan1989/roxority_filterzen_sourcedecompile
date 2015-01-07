var m1 = "问卷已经生成，此操作需要在经过页面编辑后生效。";
var m2 = "问卷已经生成，不能进行此操作\n如需进行此操作，可进行问卷重构!";
function addPage(SID1, PID) {
    if (sState == "True") { alert(m2); return false; }
    window.parent.initFace();

    for (i = 0; i < arrPageNo.length; i++) {
        if (PID == arrPageNo[i]) {
            window.parent.addPage((i + 1))
            return;
        }
    }
}

function showMessage() {
    var arrMessage = new Array();
    arrMessage[0] = "此题型不支持这个操作";
    arrMessage[1] = "问卷已经生成，不能进行此操作\n如需进行此操作，可进行问卷重构!";
    arrMessage[2] = "问卷已经生成，此操作需要在经过页面编辑后生效。";

}
window.document.ondblclick = function() {
    window.parent.setRightMenu();
}

window.onload = function() {
    sState = sState == "0" ? "False" : "True";
    arrItem2 = arrItem;
    _outerHTML();
    var intWinHeight = window.document.body.scrollHeight;
    var sTemp = "";
    var sItemStyleBT = "<span class='ItemStyleBT' onclick='setStyle($SID$,$IID$,1)' title='编辑题目外观'></span>";
    var sEliminateBT = "<span class='EliminateBT'style=\" display:none;\"  onclick='setEliminate($SID$,$IID$,1)' title='排除题目'></span>";
    var sDropToDrop = "<span class='DropListToDropListBT'  onclick='DropToDrop($SID$,$IID$)' style=\" display:none;\" title='编辑下拉联动'></span>";
    var sRejectBT = "<span class='RejectBT'  onclick='rejectOption($SID$,$IID$)' style=\" display:none;\" title='设置互斥'></span>";
    var sImageBT = "<span class='ImageBT'   onclick='setImage($SID$,$IID$,1)' style=\" display:none;\" title='设置备选项媒体'></span>";
    var sPointBT = "<span class='PointBT'   onclick='setPoint($SID$,$IID$)' title='设置备选项分值'></span>";
    var sLogicBT = "<span class='LogicBT'  onclick='setLogic($SID$,$IID$)' style=\" display:none;\" title='设置逻辑跳题'></span>";
    var sMoveItemUpBT = "<span class='MoveItemUpBT'  onclick='moveItem($SID$,$IID$,0);' title='上移题目'></span>";
    var sMoveItemDownBT = "<span class='MoveItemDownBT'  onclick='moveItem($SID$,$IID$,1);' title='下移题目'></span>";
    var sEditBT = "<span class='EditBT'  onclick='editItem($SID$,$IID$,1)' title='编辑题目'></span>";
    var sShowBT = "<span class='ShowBT'   onclick='showItem($IID$)' title='折叠' id=SH$IID$></span>";
    var sDelBT = "<span class='DelBT'    onclick='delItem($SID$,$IID$)' title='删除此题'></span>";
    var sLibBT = "<span class='LibBT'    onclick='addLib($SID$,$IID$)' title='加入到题目库'></span>";

    var sEliminateBT_Gray = "<span class='EliminateBT_Gray' style=\" display:none;\"  title='排除题目'></span>";
    var sDropToDrop_Gray = "<span class='DropListToDropListBT_Gray' style=\" display:none;\"  title='编辑下拉联动'></span>";
    var sRejectBT_Gray = "<span class='RejectBT_Gray' style=\" display:none;\"  title='设置互斥'></span>";
    var sImageBT_Gray = "<span class='ImageBT_Gray' style=\" display:none;\"   title='设置备选项媒体'></span>";
    var sPointBT_Gray = "<span class='PointBT_Gray'   title='设置备选项分值'></span>";
    var sLogicBT_Gray = "<span class='LogicBT_Gray' style=\" display:none;\"  title='设置逻辑跳题'></span>";
    var sEditBT_Gray = "<span class='EditBT_Gray' title='编辑题目'></span>";
    var sDelBT_Gray = "<span class='DelBT_Gray'    title='删除此题'></span>";
    var sItemBatchSortBT = "<span class='ItemBatchSortBT'  onclick='ItemBatchSort($SID$,$PID$)' title='题目批量排序'></span>";
    var sItemBatchSortBT_Gray = "<span class='ItemBatchSortBT_Gray'   title='题目批量排序'></span>";


    var sMovePageUpBT = "<span class='MoveItemUpBT'  onclick='movePage($PID$,0);' title='上移页'></span>";
    var sMovePageDownBT = "<span class='MoveItemDownBT'  onclick='movePage($PID$,1);' title='下移页'></span>";
    var sInsertItem = "<span class='InsertItemBT'   title='在本页插入题目' onclick='window.parent.editItem1($SID$,$PageNo$)'></span>";
    var sInsertItem_Gray = "<span class='InsertItemBT_Gray'   title='在本页插入题目'></span>";
    var sDelPageBT = "<span class='DelBT'  onclick='delPage($SID$,$PID$)'    title='删除此页'></span>";
    var sDelPageBT_Gray = "<span class='DelBT_Gray'    title='删除此页'></span>";
    var sEditPageBT = "<span class='EditBT'  onclick='editPage($SID$,$PID$)' title='编辑页'></span>";
    var sInsertPage = "<span class='PageBT'   title='在此页前插入分页' onclick='addPage($SID$,$PID$)'></span>";
    var sInsertPage_Gray = "<span class='PageBT_Gray'   title='在此页前插入分页'></span>";
    var sShowPageBT = "<span class='ShowBT'   onclick='showPage($PID$)' id=SHP$PID$ title='折叠'></span>";

    var sIntroductionAnswerBT = "<span class='IntroductionAnswer'  onclick='IntroductionAnswer($SID$,$IID$)' style=\" display:none;\" title='引入答题结果'></span>";
    var sIntroductionAnswerBT_Gray = "<span class='IntroductionAnswer_Gray' style=\" display:none;\"  title='引入答题结果'></span>";

    var sProgressiveAskBT = "<span class='ProgressiveAskBT'  onclick='ProgressiveAsk($SID$,$IID$)' style=\" display:none;\" title='排除其它题选项'></span>";
    var sProgressiveAskBT_Gray = "<span class='ProgressiveAskBT_Gray' style=\" display:none;\"   title='排除其它题选项'></span>";

    for (i = 0; i < arrItem2.length; i++) {
        switch (arrItem2[i][2]) {
            case 1:
            case 2:
            case 3:
            case 17:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 4:
            case 5:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT + sLogicBT + sPointBT + sImageBT + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT + sItemStyleBT + sEditBT; }
                break;
            case 6:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT + sLogicBT_Gray + sPointBT + sImageBT_Gray + sRejectBT_Gray + sDropToDrop + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 7:
            case 15:
            case 18:
            case 19:
            case 20:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 16:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 8:
            case 9:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT + sLogicBT_Gray + sPointBT + sImageBT + sRejectBT + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 10:
            case 11:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 12:
            case 13:
            case 21:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;
            case 14:
                if (sState == "True") { sTemp = sShowBT + sDelBT_Gray + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT_Gray + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT_Gray; }
                else { sTemp = sShowBT + sDelBT + sLibBT + sMoveItemDownBT + sMoveItemUpBT + sIntroductionAnswerBT + sProgressiveAskBT_Gray + sLogicBT_Gray + sPointBT_Gray + sImageBT_Gray + sRejectBT_Gray + sDropToDrop_Gray + sEliminateBT_Gray + sItemStyleBT + sEditBT; }
                break;

        }


        sTemp = sTemp.replace(/\$SID\$/g, SID);
        sTemp = sTemp.replace(/\$IID\$/g, arrItem2[i][0]);
        sTemp = "<div>" + sTemp + "</div>";
        $("I_Option" + arrItem[i][0]).innerHTML = sTemp;
        sTemp = "";
    }


    var sItemContent = "";
    var intTempPID = 0;
    for (i = 0; i < arrPage2.length; i++) {
        if (sState == "True") {
            sTemp = sShowPageBT + sDelPageBT + sMovePageDownBT + sMovePageUpBT + sItemBatchSortBT_Gray + sInsertPage_Gray + sInsertItem + sEditPageBT;
        }
        else {
            sTemp = sShowPageBT + sDelPageBT + sMovePageDownBT + sMovePageUpBT + sItemBatchSortBT + sInsertPage + sInsertItem + sEditPageBT;
        }

        sTemp = sTemp.replace(/\$SID\$/g, SID);
        sTemp = sTemp.replace(/\$PID\$/g, arrPage2[i]);
        sTemp = sTemp.replace(/\$PageNo\$/g, (i + 1));
        sTemp = "<div style='margin-top:5px;'>" + sTemp + "</div>";
        $("P_Option" + arrPage2[i]).innerHTML = sTemp;
        $("PageNo" + arrPage2[i]).innerHTML = (i + 1) + "/" + (arrPage2.length) + "页";
        for (j = 0; j < arrItem2.length; j++) {
            if ((i + 1) == arrItem[j][1]) {
                intTempPID = arrPage2[i];
                sItemContent += $("Posi_I_" + j).outerHTML;

                $("Posi_I_" + j).outerHTML = "";
            }
        }
        if (intTempPID > 0) {
            $("PageItem" + intTempPID).innerHTML += sItemContent;
        }
        sItemContent = "";
    }
}

function getPageIndex(inputPID) {
    for (m = 0; m < arrPageNo.length; m++) {
        if (arrPageNo[m] == inputPID) {
            return m;
        }
    }
}

function movePage(PID, d) {
    //d=1 下移 d=0 上移

    if (sState == "True") {
        alert(m2);
        return false;
    }

    var sCurrPageContent = "";
    var intCurrIndex = getPageIndex(PID)
    if (d == 1) {
        if (intCurrIndex == arrPageNo.length - 1) {
            alert("已是最后一页");
            return;
        }
        window.parent.initFace();
        sCurrPageContent = $("Posi_P_" + intCurrIndex).innerHTML;
        $("Posi_P_" + intCurrIndex).innerHTML = $("Posi_P_" + (intCurrIndex + 1)).innerHTML;
        $("Posi_P_" + (intCurrIndex + 1)).innerHTML = sCurrPageContent;
        $("PageNo" + PID).innerHTML = (intCurrIndex + 2) + "/" + arrPageNo.length;
        $("PageNo" + arrPageNo[intCurrIndex + 1]).innerHTML = (intCurrIndex + 1) + "/" + arrPageNo.length + "页";
        arrPageNo[intCurrIndex] = arrPageNo[intCurrIndex + 1]
        arrPageNo[intCurrIndex + 1] = PID
        $("targetWin").src = "";
        _sort();
        $("targetWin").src = "MovePage.aspx?d=1&SID=" + SID + "&PID=" + PID + "&" + escape(new Date());
    }
    else {
        if (intCurrIndex == 0) {
            alert("已是第一页");
            return;
        }
        window.parent.initFace();
        sCurrPageContent = $("Posi_P_" + intCurrIndex).innerHTML;
        $("Posi_P_" + intCurrIndex).innerHTML = $("Posi_P_" + (intCurrIndex - 1)).innerHTML;
        $("Posi_P_" + (intCurrIndex - 1)).innerHTML = sCurrPageContent;
        $("PageNo" + PID).innerHTML = intCurrIndex + "/" + arrPageNo.length;
        $("PageNo" + arrPageNo[intCurrIndex - 1]).innerHTML = (intCurrIndex + 1) + "/" + arrPageNo.length + "页";
        arrPageNo[intCurrIndex] = arrPageNo[intCurrIndex - 1];
        arrPageNo[intCurrIndex - 1] = PID;
        $("targetWin").src = "";
        _sort();
        $("targetWin").src = "MovePage.aspx?d=0&SID=" + SID + "&PID=" + PID + "&" + escape(new Date());
    }
}


function delPage(SID, PID) {

    if (sState == "True") {
        alert(m2);
        return false;
    }

    $("targetWin").src = "DelPage.aspx?SID=" + SID + "&PID=" + PID + "&" + escape(new Date());
}


function delItem(SID, IID) {
    if (sState == "True") {
        alert(m2);
        return false;
    }
    $("targetWin").src = "DelItem.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
}


function showItem(IID) {
    if (($("I" + IID).style.display == "block") || ($("I" + IID).style.display == "")) {
        $("SH" + IID).className = "HiddenBT";
        $("I" + IID).style.display = "none";
        $("ItemNameTitle" + IID).className = "ItemNameTitleShow";

    }
    else {
        $("SH" + IID).className = "ShowBT";
        $("I" + IID).style.display = "block";
        $("ItemNameTitle" + IID).className = "ItemNameTitleHidden";
    }

}

function showPage(PID) {
    if (($("PageBox" + PID).style.display == "block") || ($("PageBox" + PID).style.display == "")) {
        $("SHP" + PID).className = "HiddenBT";
        $("PageBox" + PID).style.display = "none";


    }
    else {
        $("SHP" + PID).className = "ShowBT";
        $("PageBox" + PID).style.display = "block";

    }
}


function moveItem(SID, IID, d) {
    if (sState == "True") {
        alert(m2);
        return false;
    }
    var intPageNo = 0;
    var intCurrPosi = 0;
    var intMaxPosi = 0;
    var intMinPosi = 65536;
    var i;

    var sCurrItemContent = "";

    for (i = 0; i < arrItem.length; i++) {
        if (arrItem[i][0] == IID) {
            intPageNo = parseInt(arrItem[i][1]);
            intCurrPosi = i;
            break;
        }
    }


    for (i = 0; i < arrItem.length; i++) {

        if (parseInt(arrItem[i][1]) == intPageNo) {

            if (i > intMaxPosi) {
                intMaxPosi = i; //得到最大位置
            }

            if (i < intMinPosi) {
                intMinPosi = i; //得到最小位置
            }
        }
    }

    if (d == 1) {   //下移

        if (intCurrPosi == intMaxPosi) {
            alert("题目已处于本页最后一位");
            return;
        }


        for (i = 0; i < arrItem.length; i++) {
            if (arrItem[i][0] == IID) {
                sCurrItemContent = $("Posi_I_" + i).innerHTML;
                $("Posi_I_" + i).innerHTML = $("Posi_I_" + (i + 1)).innerHTML;
                $("Posi_I_" + (i + 1)).innerHTML = sCurrItemContent;
                arrItem[i][0] = arrItem[i + 1][0]
                arrItem[i + 1][0] = IID
                break;
            }
        }
        $("targetWin").src = "";
        _sort();
        $("targetWin").src = "MoveItem.aspx?d=1&SID=" + SID + "&IID=" + IID + "&" + escape(new Date());

    }
    else {
        if (intCurrPosi == intMinPosi) {
            alert("题目已处于本页第一位");
            return;
        }

        for (i = 0; i < arrItem.length; i++) {
            if (arrItem[i][0] == IID) {
                sCurrItemContent = $("Posi_I_" + i).innerHTML;
                $("Posi_I_" + i).innerHTML = $("Posi_I_" + (i - 1)).innerHTML;
                $("Posi_I_" + (i - 1)).innerHTML = sCurrItemContent;
                arrItem[i][0] = arrItem[i - 1][0]
                arrItem[i - 1][0] = IID
                break;
            }
        }
        $("targetWin").src = "";
        _sort();
        $("targetWin").src = "MoveItem.aspx?d=0&SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    }
}

function editItem(SID, IID) {
    if (sState == "True") {
        alert(m2);
        return false;
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 修改题目";
    window.parent.$("SetWin").src = "ModifyItem.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function editHeadAndFoot(SID, t) {
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    if (t == 0) {
        window.parent.$("TitleName").innerHTML = " 编辑页眉";
        window.parent.$("SetWin").src = "EditHeadAndFoot.aspx?SID=" + SID + "&t=0&" + escape(new Date());
    }
    else {
        window.parent.$("TitleName").innerHTML = " 编辑页脚";
        window.parent.$("SetWin").src = "EditHeadAndFoot.aspx?SID=" + SID + "&t=1&" + escape(new Date());
    }

    window.parent.$("RightMenuContent").style.display = "block";
}

function editPage(SID, PID) {
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 编辑页内容";
    window.parent.$("SetWin").src = "EditPage.aspx?SID=" + SID + "&PID=" + PID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}



function setPoint(SID, IID) {
    for (i = 0; i < arrItem.length; i++) {
        if (arrItem[i][0] == IID) {
            if ((arrItem[i][2] >= 11) || (arrItem[i][2] <= 3)) {
                alert("此题型不支持此功能");
                return;
            }
            break;
        }
    }
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 设置选项分值";
    window.parent.$("SetWin").src = "SetPoint.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function setImage(SID, IID) {
    for (i = 0; i < arrItem.length; i++) {
        if (arrItem[i][0] == IID) {
            if ((arrItem[i][2] >= 10) || (arrItem[i][2] <= 3) || (arrItem[i][2] == 6) || (arrItem[i][2] == 7)) {
                alert("此题型不支持此功能");
                return;
            }
            break;
        }
    }
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 设置选项图片";
    window.parent.$("SetWin").src = "SetOptionImage.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function setStyle(SID, IID) {
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 编辑题目外观";
    window.parent.$("SetWin").src = "SetItemStyle.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function setLogic(SID, IID) {
    for (i = 0; i < arrItem.length; i++) {
        if (arrItem[i][0] == IID) {
            if ((arrItem[i][2] >= 10) || (arrItem[i][2] <= 3)) {
                alert("此题型不支持此功能");
                return;
            }
            break;
        }
    }
    if (sState == "True") {
        alert(m1);
    }
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 设置逻辑跳题";
    window.parent.$("SetWin").src = "SetLogic.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}



function addLib(SID, IID) {
    $("targetWin").src = "AddToLib.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
}



function doMoveItem() {
    if (sState == "True") {
        alert(m2);
        return false;
    }
    window.parent.initFace();
    var sCurrItemContent = "";
    if (d == 1) {   //下移
        for (i = 0; i < arrItem.length; i++) {
            if (arrItem[i][0] == IID) {
                sCurrItemContent = $("Posi_I_" + i).innerHTML;
                $("Posi_I_" + i).innerHTML = $("Posi_I_" + (i + 1)).innerHTML;
                $("Posi_I_" + (i + 1)).innerHTML = sCurrItemContent;
                arrItem[i][0] = arrItem[i + 1][0]
                arrItem[i + 1][0] = IID
                return;
            }
        }
    }
    else {
        for (i = 0; i < arrItem.length; i++) {
            if (arrItem[i][0] == IID) {
                sCurrItemContent = $("Posi_I_" + i).innerHTML;
                $("Posi_I_" + i).innerHTML = $("Posi_I_" + (i - 1)).innerHTML;
                $("Posi_I_" + (i - 1)).innerHTML = sCurrItemContent;
                arrItem[i][0] = arrItem[i - 1][0]
                arrItem[i - 1][0] = IID
                return;
            }
        }

    }
}



function DropToDrop(SID, IID) {
    window.parent.optionActionWin("AdvDropList1.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date()), "编辑下拉框联动效果-绑定对象", 430, 800)
}

function ItemBatchSort(SID, PID) {
    window.parent.optionActionWin("ItemBatchSort.aspx?SID=" + SID + "&PID=" + PID + "&" + escape(new Date()), "对本页题目进行批量排序", 400, 500)
}


function rejectOption(SID, IID) {
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 设置多选互斥";
    window.parent.$("SetWin").src = "rejectOption.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}


function setEliminate(SID, IID) {
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 设置排除题目";
    window.parent.$("SetWin").src = "EliminateItem.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function IntroductionAnswer(SID, IID) {
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 引入答题结果";
    window.parent.$("SetWin").src = "IntroductionAnswer.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function ProgressiveAsk(SID, IID) {
    window.parent.initFace();
    window.parent.$("TitleName").innerHTML = " 排除其它题选项";
    window.parent.$("SetWin").src = "ProgressiveAsk.aspx?SID=" + SID + "&IID=" + IID + "&" + escape(new Date());
    window.parent.$("RightMenuContent").style.display = "block";
}

function _outerHTML() {
    if (typeof (HTMLElement) != "undefined" && !window.opera) {
        HTMLElement.prototype.__defineGetter__("outerHTML", function() {
            var a = this.attributes, str = "<" + this.tagName, i = 0; for (; i < a.length; i++)
                if (a[i].specified)
                str += " " + a[i].name + '="' + a[i].value + '"';
            if (!this.canHaveChildren)
                return str + " />";
            return str + ">" + this.innerHTML + "</" + this.tagName + ">";
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

//设置题目排序
function _sort() {
    var docs = document.body.innerHTML;
    var regex = new RegExp("<SPAN class=SerialNumberSortStyle>[^<]*</SPAN>", "g");
    regex.global = true;
    var arrMactches = docs.match(regex)

    var i = 1;
    if (arrMactches != null) {
        for (var j = 0; j < arrMactches.length; j++) {
            document.body.innerHTML = document.body.innerHTML.replace(arrMactches[j], "SerialNumberSortStyleNum" + i);
            i++;
        }
    }

    var i = 1;
    if (arrMactches != null) {
        for (var j = 0; j < arrMactches.length; j++) {
            document.body.innerHTML = document.body.innerHTML.replace("SerialNumberSortStyleNum" + i, "<span class=SerialNumberSortStyle>" + i + "、" + "</span>");
            i++;
        }
    }

    //    window.location.reload(); 
}