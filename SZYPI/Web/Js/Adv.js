//浮动广告-------------------start-----------------------
var objCenter=document.getElementById("AdCenterLayer");
var closeCenter=false;
function closeCenterBanner()
 {
   closeCenter=true;
   return;
 }
var isStop=false;
function stopMove()
{
   isStop = true;
   return;
}
function Move()
{
   isStop = false;
   return;
}

var xPos = 20;
var yPos = document.documentElement.clientHeight;
var step = 1;
var delay = 30; 
var height = 0;
var Hoffset = 0;
var Woffset = 0;
var yon = 0;
var xon = 0;
var pause = true;
var interval;
if(objCenter!=null)
{
    objCenter.style.top = yPos;
    start();
}

function changePos() 
{
   if(!closeCenter&&!isStop)
    {
        width = document.documentElement.clientWidth;
        height = document.documentElement.clientHeight;
        Hoffset = objCenter.offsetHeight;
        Woffset = objCenter.offsetWidth;
        objCenter.style.left = xPos + document.documentElement.scrollLeft;
        objCenter.style.top = yPos + document.documentElement.scrollTop;
        if (yon) 
        {
            yPos = yPos + step;
        }
        else 
        {
            yPos = yPos - step;
        }
        if (yPos < 0) 
        {
            yon = 1;
            yPos = 0;
        }
        if (yPos >= (height - Hoffset)) 
        {
            yon = 0;
            yPos = (height - Hoffset);
        }
        if (xon) 
        {
            xPos = xPos + step;
        }
        else 
        {
            xPos = xPos - step;
        }
        if (xPos < 0) 
        {
            xon = 1;
            xPos = 0;
        }
        if (xPos >= (width - Woffset)) 
        {
            xon = 0;
            xPos = (width - Woffset);
        }
   }
   else if(closeCenter)
   {
        objCenter.style.visibility = "hidden";
   }
   else if(isStop)
   {
        objCenter.style.left = xPos + document.documentElement.scrollLeft;
        objCenter.style.top = yPos + document.documentElement.scrollTop;
   }
}
function start() 
{
      objCenter.style.visibility = "visible";
      interval = setInterval('changePos()', delay);
}
        
//浮动广告--------------------------------end-----------------

//-----------------------------浮动广告2(Start)-------------------------------------------
var objCenter2 = document.getElementById("AdCenterLayer2");

var xPos2 = document.documentElement.clientWidth;
var yPos2 = document.documentElement.clientHeight;
var step2 = 1;
var delay2 = 30; 
var height2 = 0;
var Hoffset2 = 0;
var Woffset2 = 0;
var yon2 = 0;
var xon2 = 0;
var pause2 = true;
var interval2;
//控制停止，浮动
var isStop2 = false;
//控制隐藏和显示
var closeCenter2 = false;

function closeCenterBanner2()
{
   closeCenter2 = true;
   return;
}


function stopMove2()
{
   isStop2 = true;
   return;
}

function Move2()
{
   isStop2 = false;
   return;
}


if(objCenter2!=null)
{
    objCenter2.style.top = yPos2;
    start2();
}

function start2() 
{
      objCenter2.style.visibility = "visible";
      interval = setInterval("changePos2()", delay);
}

function changePos2() 
{  
   if(!closeCenter2 &&!isStop2)
    {
        width2 = document.documentElement.clientWidth;
        height2 = document.documentElement.clientHeight;
        Hoffset2 = objCenter2.offsetHeight;
        Woffset2 = objCenter2.offsetWidth;
        objCenter2.style.left = xPos2 + document.documentElement.scrollLeft;
        objCenter2.style.top = yPos2 + document.documentElement.scrollTop;
        if (yon2) 
        {
            yPos2 = yPos2 + step2;
        }
        else 
        {
            yPos2 = yPos2 - step2;
        }
        if (yPos2 < 0) 
        {
            yon2 = 1;
            yPos2 = 0;
        }
        if (yPos2 >= (height2 - Hoffset2)) 
        {
            yon2 = 0;
            yPos2 = (height2 - Hoffset2);
        }
        if (xon2) 
        {
            xPos2 = xPos2 + step2;
        }
        else 
        {
            xPos2 = xPos2 - step2;
        }
        if (xPos2 < 0) 
        {
            xon2 = 1;
            xPos2 = 0;
        }
        if (xPos2 >= (width2 - Woffset2)) 
        {
            xon2 = 0;
            xPos2 = (width2 - Woffset2);
        }
   }
   else if(closeCenter2)
   {
        objCenter2.style.visibility = "hidden";
   }
   else if(isStop2)
   {
        objCenter2.style.left = xPos2 + document.documentElement.scrollLeft;
        objCenter2.style.top = yPos2 + document.documentElement.scrollTop;
   }
}

//-----------------------------浮动广告2(end)---------------------------------------------
     
//对联广告-----------------------start------------------
var obj3=document.getElementById("AdLayer3") 
var obj2=document.getElementById("AdLayer2") 
var obj4=document.getElementById("AdLayer4") 
var obj6=document.getElementById("AdLayer6") 
var obj5=document.getElementById("AdLayer5") 
var obj7=document.getElementById("AdLayer7") 
var closeB3=false;
var closeB2=false;
var closeB4=false;
var closeB6=false;
var closeB5=false;
var closeB7=false;


function closeBanner3()
 {
 closeB3=true;
 return;
 }
 
 function closeBanner2()
 {
 closeB2=true;
 return;
 }
 
 function closeBanner4()
 {
 closeB4=true;
 return;
 }
 
 function closeBanner6()
 {
 closeB6=true;
 return;
 }
 
 function closeBanner5()
 {
 closeB5=true;
 return;
 }
 
 function closeBanner7()
 {
 closeB7=true;
 return;
 }

function initEcAd()
 {
  if(obj3!=null )
	{
        document.all.AdLayer3.style.posTop = - 200;
        document.all.AdLayer3.style.visibility = 'visible'
        MoveLeftLayer('AdLayer3');
    }
	if(obj5!=null )
	{
        document.all.AdLayer5.style.posTop = -200;
        document.all.AdLayer5.style.visibility = 'visible'
        MoveLeftLayer5('AdLayer5');
    }
    if(obj7!=null )
  {
 
        document.all.AdLayer7.style.posTop = -200;
        document.all.AdLayer7.style.visibility = 'visible'
        MoveLeftLayer7('AdLayer7');
    }   
    if(obj2!=null )
    {
        document.all.AdLayer2.style.posTop = -200;
        document.all.AdLayer2.style.visibility = 'visible'
        
        MoveRightLayer('AdLayer2');
    }
    if(obj4!=null )
    {
        document.all.AdLayer4.style.posTop = -200;
        document.all.AdLayer4.style.visibility = 'visible'
        
        MoveRightLayer4('AdLayer4');
    } 
    if(obj6!=null )
    {
        document.all.AdLayer6.style.posTop = -200;
        document.all.AdLayer6.style.visibility = 'visible'
        
        MoveRightLayer6('AdLayer6');
    } 
    

}
function MoveLeftLayer(layerName) {
if(!closeB3)
    {
        var x = 5;
        var y = 50;
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer3.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posLeft = x");
        //alert(y);
        setTimeout("MoveLeftLayer('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer3.style.visibility = 'hidden'
    }
}

  function MoveLeftLayer5(layerName) {
if(!closeB5)
    {
        var x = 5;
        var y = 50+20+document.all.h3.height+19;
       
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer5.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posLeft = x");
        //alert(document.documentElement.scrollTop);
        setTimeout("MoveLeftLayer5('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer5.style.visibility = 'hidden'
    }
}
  function MoveLeftLayer7(layerName) {
if(!closeB7)
    {
        var x = 5;
        var y = 50+20+20+document.all.h3.height+document.all.h5.height+19*2;
       
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer7.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posLeft = x");
        //alert(document.documentElement.scrollTop);
        setTimeout("MoveLeftLayer7('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer7.style.visibility = 'hidden'
    }
}
function MoveRightLayer(layerName) {
if(!closeB2)
    {
        var x = 5;
        var y = 50;
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer2.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posRight = x");
        setTimeout("MoveRightLayer('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer2.style.visibility = 'hidden'
    }
}
function MoveRightLayer4(layerName) {
if(!closeB4)
    {
        var x = 5;
        var y = 50+20+document.all.h2.height+19;
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer4.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posRight = x");
        setTimeout("MoveRightLayer4('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer4.style.visibility = 'hidden'
    }
}
function MoveRightLayer6(layerName) {
if(!closeB6)
    {
        var x = 5;
        var y = 50+20+20+document.all.h2.height+document.all.h4.height+19*2;
        var diff = (document.documentElement.scrollTop + y - document.all.AdLayer6.style.posTop)*.60;
        var y = document.documentElement.scrollTop + y - diff;
        eval("document.all." + layerName + ".style.posTop = y");
        eval("document.all." + layerName + ".style.posRight = x");
        setTimeout("MoveRightLayer6('"+layerName+"');", 60);
    }
    else
    {
        document.all.AdLayer6.style.visibility = 'hidden'
    }
}
initEcAd();
//对联广告-----------------------end------------------
function setClickRate2(ADID)
{
    var oHttpReq = new ActiveXObject("MSXML2.XMLHTTP");
    oHttpReq.open("POST","../../../ClickRate.aspx?ID="+ADID,false);
    oHttpReq.send();
}


