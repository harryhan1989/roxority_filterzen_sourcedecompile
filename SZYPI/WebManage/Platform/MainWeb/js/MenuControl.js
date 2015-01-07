var bg1 = "../../images/dhh.jpg";
var bg2 = "../../images/dh.jpg";
var bg3 = "../../images/dh1.jpg";
var bg4 = "../../images/bg_menu1_bg2.gif";
var bg5 = "#FFF";
var bg6 = "../../images/bg_menu2_down.gif";
var bg7 = "../../images/bg_menu2_up.gif";


function ControlLeftMenuItem(title,item)
{
    var Title = document.getElementById(title);
    var Item = document.getElementById(item);            
    hidLeftMenuItem(item);
    changeLeftMenuTitle_bg(title);
   
    if(Item.style.display == "block")
    {        
        Item.style.display = "none";
        document.getElementById(title).background = bg6;  
    }
    else
    {
        Item.style.display = "block";
        document.getElementById(title).background = bg7;
    }
}

function changeLeftMenuTitle_bg(title)
{
    var controlID = "";	    
    var Flag = true;
    var i = -1;    
    var Ctl;
    while(Flag)
    {  
        i++;    
        controlID ="tdTitle" + i;    
        Ctl = document.getElementById(controlID);
        if(Ctl == null)
        { 
            Flag = false;
            break;
        } 
        if(controlID != title)
        {            
            Ctl.background = bg6;            
        }        
    }
}

function hidLeftMenuItem(item)
{
    var controlID = "";	    
    var Flag = true;
    var i = -1;    
    var Ctl;
    while(Flag)
    {  
        i++;    
        controlID ="MenuList" + i;    
        Ctl = document.getElementById(controlID);
        if(Ctl == null)
        { 
            Flag = false;
            break;
        } 
        if(controlID != item)
        {
            Ctl.style.display = "none";
        }
    }  
}


function linkMouseOver(aa)
{
    if(aa.id != document.getElementById("hidValue").value)
    {
        aa.style.color = "#FF9966";
    }
}

function linkMouseOut(aa)
{
    if(aa.id != document.getElementById("hidValue").value)
    {
        aa.style.color = "#000";
    }
    else
    {
        aa.style.color = "#FA7E3F";  
    }
}


function SelectedItemColor(obj)
{
    document.getElementById("hidValue").innerText = obj.id;  
    var controls = document.getElementsByTagName("td");   
    for(var i=0; i< controls.length; i++)
    {
       var e= controls[i];
       if(e.id.indexOf("menuItem") != -1)
       {
            if(e.id != obj.id)
            {
                e.style.color = "#000";
            }
            else
            {
                e.style.color = "#FA7E3F";
            }
       }     
    }          
}


function mainFrameUrl(Url)
{
   window.top.frames[0].frames["mainFrame"].location.href = Url;
}
