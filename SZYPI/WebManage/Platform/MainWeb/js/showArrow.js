function InitMenu()
{
    this.top.frames[0].frames['mainFrameset'].cols = "0,0,*";
}

function showMenuAll()
{
   var cols = this.top.frames[0].frames['mainFrameset'].cols;  
   if(cols == "0,0,*")
   {          
       cols = "180,5,*";
       
       document.getElementById("divMenuControl").innerText = "隐藏菜单";
       showMinindex();
   }
   else
   {
       cols = "0,0,*";      
       document.getElementById("divMenuControl").innerText = "显示菜单";  
       showMaxindex();     
   }           
   this.top.frames[0].frames['mainFrameset'].cols = cols;
}


function showMenu()
{
    var cols = this.top.frames[0].frames['mainFrameset'].cols;
    
    var menuWidth = cols.split(",")[0];
    if (menuWidth != "0")
    {
        cols = "0,5,*";
        window.document.getElementById("divback").style.background = "url(../../images/leftshow.gif)";
        window.document.getElementById("separator").className = "cursorR";
        showMaxindex1();  
    }
    else
    {
        cols = "180,5,*";
        window.document.getElementById("divback").style.background = "url(../../images/lefthide.gif)";
        window.document.getElementById("separator").className = "cursorL";
        showMinindex1();
    }
    this.top.frames[0].frames['mainFrameset'].cols = cols;
}


function tips_pop()
{
    var MsgPop=document.getElementById("winpop");         
    var popH=parseInt(MsgPop.style.height);//将对象的高度转化为数字
    if (popH==0)
    {
        //MsgPop.style.display="block";//显示隐藏的窗口
        show=setInterval("changeH('up')",2);
    }
    else
    { 
        hide=setInterval("changeH('down')",2);
    }
}
function changeH(str)
{
    var MsgPop=document.getElementById("winpop");
    var IfrRef = document.getElementById('DivShim'); 
    var popH=parseInt(MsgPop.style.height);
    if(str=="up")
    {
        if (popH<=160)
        {
            MsgPop.style.height=(popH+4).toString()+"px";
            DivSetVisible();
        }
        else
        {  
            clearInterval(show);
        }
    }
    if(str=="down")
    { 
        if (popH>=4)
        {  
            MsgPop.style.height=(popH-4).toString()+"px";
            DivSetVisible();
        }
        else
        { 
            clearInterval(hide);   
            MsgPop.style.display="none";  //隐藏DIV
            IfrRef.style.display="none";
        }
    }
    setTimeout("MsgDis()",180000);
}

function MsgDis()
{
    var MsgPop = document.getElementById('winpop');
    var IfrRef = document.getElementById('DivShim');
    var popH=parseInt(MsgPop.style.height);
    if(popH >= 4)
    {
        MsgPop.style.height=(popH-4).toString()+"px";
        DivSetVisible();
    }
    else
    {
        MsgPop.style.display="none";  //隐藏DIV
        IfrRef.style.display="none";
    }
}

function DivSetVisible()
{
    var MsgPop = document.getElementById('winpop');
    var IfrRef = document.getElementById('DivShim');
    MsgPop.style.display = "block";
    IfrRef.style.display = "block";
    IfrRef.style.width = MsgPop.offsetWidth;
    IfrRef.style.height = MsgPop.offsetHeight;
    IfrRef.style.left = MsgPop.offsetLeft;
    IfrRef.style.top = MsgPop.offsetTop;
}

