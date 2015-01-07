function getNow()
{
   var d, s = "";
   d = new Date();
   
   var week = new Array( "��", "һ", "��", "��", "��", "��", "��" );
   var weekday = week[d.getDay()];   
   
   s += "���ڣ�" + d.getYear() + "-";
   s += (d.getMonth() + 1) + "-";
   s += d.getDate();
   s += "&nbsp;&nbsp;����" + weekday;
   return(s);
}

var timerID = null; 
var timerRunning = false;

function startClock() 
{
    stopClock(); 
    showTime();
}

function stopClock()
{ 
    if(timerRunning) 
    clearTimeout(timerID); 
    timerRunning = false;
} 

function showTime() 
{
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    var seconds = now.getSeconds();
    var timevalue = " " + ((hours >12) ? hours -12 :hours) 
    timevalue += ((minutes < 10) ? ":0" : ":") + minutes 
    timevalue += ((seconds < 10) ? ":0" : ":") + seconds 
    timevalue += (hours >= 12) ? " PM" : " AM" 
    document.all("time").innerHTML = getNow() + " ";
    document.all("time").innerHTML += "&nbsp;" +  timevalue; 
    timerID = setTimeout("showTime()",1000); 
    timerRunning = true;
}
