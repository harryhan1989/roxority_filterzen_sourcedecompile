function toTarget(ID, sURL, BTAmount) {
    document.getElementById("targetWin").src = sURL
    for (i = 1; i <= BTAmount; i++) {
        document.getElementById("B" + i).className = "BtNormal";
    }
    document.getElementById(ID).className = "BtDown";
}

function setBT(ID, d, sBtDown, sBtMove, sBtNormal) {
    if (document.getElementById(ID).className != sBtDown) {
        if (d == 0) {
            document.getElementById(ID).className = sBtNormal;
        }
        else {
            document.getElementById(ID).className = sBtMove;
        }
    }

}

function btAction(d, obj, btNormal, btOver, btOut, btDown, btAmount) {
    var btAmountStart = 0;
    switch (d) {
        case 0: //btNormal
            document.getElementById(obj).className = btNormal;
            break;
        case 1: //btOver
            if (document.getElementById(obj).className != btDown) {
                document.getElementById(obj).className = btOver;
            }
            break;
        case 2: //btOut
            if (document.getElementById(obj).className != btDown) {
                document.getElementById(obj).className = btOut;
            }
            break;
        case 3: //btDown

            for (btAmountStart = 0; btAmountStart < btAmount; btAmountStart++) {
                document.getElementById("B" + btAmountStart).className = btNormal;

            }
            document.getElementById(obj).className = btDown;
            break;
    }
}



function clickBT(ID, URL, sBtDown, sBtNormal, BTAmount) {

    for (i = 1; i <= BTAmount; i++) {
        try {
            document.getElementById("B" + i).className = sBtNormal;
        }
        catch (e) {

        }
    }
    document.getElementById("targetWin").style.height = document.getElementById("OptionArea").style.height;
    document.getElementById(ID).className = sBtDown;
    document.getElementById("targetWin").src = URL + ".aspx?SID=" + SID;
    document.getElementById("PrintBT").style.filter = "";
    document.getElementById("LeftMenu").style.display = "none";

}