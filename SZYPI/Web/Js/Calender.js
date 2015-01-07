	// 一个简单的测试是否IE浏览器的表达式
	isIE = (document.all ? true : false);

	// 得到IE中各元素真正的位移量，即使这个元素在一个表格中
	function getIEPosX(elt) { return getIEPos(elt,"Left"); }
	function getIEPosY(elt) { return getIEPos(elt,"Top"); }
	
	function getIEPos(elt,which)
	{
		iPos = 0;
		
		while (elt!=null) 
		{
			iPos += elt["offset" + which];
			elt = elt.offsetParent;
		}
		
		return iPos;
	}

	function getXBrowserRef(eltname)
	{
		return (isIE ? document.all[eltname].style : document.layers[eltname]);
	}

	function hideElement(eltname)
	{
		getXBrowserRef(eltname).visibility = 'hidden';
	}

	// 按不同的浏览器进行处理元件的位置
	function moveBy(elt,deltaX,deltaY)
	{
		if (isIE)
		{
			elt.left = elt.pixelLeft + deltaX;
			elt.top = elt.pixelTop + deltaY;
		}
		else
		{
			elt.left += deltaX;
			elt.top += deltaY;
		}
	}

	function toggleVisible(eltname, pos)
	{
		elt = getXBrowserRef(eltname);
		if (elt.visibility == 'visible' || elt.visibility == 'show')
		{
			elt.visibility = 'hidden';
		}
		else
		{
			fixPosition(eltname, pos);
			elt.visibility = 'visible';
		}
	}

	function setPosition(elt,positionername,isPlacedUnder)
	{
		positioner = null;
		
		if (isIE)
		{
			positioner = document.all[positionername];
			elt.left = getIEPosX(positioner) ;
			elt.top = getIEPosY(positioner) ;
		}
		else
		{
			positioner = document.images[positionername];
			elt.left = positioner.x;
			elt.top = positioner.y;
		}
		
		if (isPlacedUnder)
		{
			moveBy(elt,0,positioner.height);
		}
		
		//由于日历控件有可能被下拉框遮盖
		document.all["calenderMaskFrame"].left = elt.left;
		document.all["calenderMaskFrame"].top = elt.top;
		document.all["calenderMaskFrame"].style.zIndex =-1;
		//elt.zIndex = 999;
	}



	// 初始月份及各月份天数数组
    var months = new Array("1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12");
    var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
	var displayMonth = new Date().getMonth();
	var displayYear = new Date().getFullYear();
	var displayDivName;
	var displayElement;

	//测试选择的年份是否是润年？
    function getDays(month, year)
    {
        if (1 == month)
			return ((0 == year % 4) && (0 != (year % 100))) || (0 == year % 400) ? 29 : 28;
        else
			return daysInMonth[month];
    }

    
    // 得到今天的日期
    function getToday()
    {
	    this.now = new Date();
        this.year = this.now.getFullYear();
        this.month = this.now.getMonth();
        this.day = this.now.getDate();
    }

    function newCalendar(eltName,attachedElement)
    {
	    if (attachedElement)
	    {
	   
	       if (displayDivName && displayDivName != eltName) hideElement(displayDivName);
	       displayElement = attachedElement;
	    }
	    
	    displayDivName = eltName;
        today = new getToday();
        var parseYear = parseInt(displayYear + '');
        var newCal = new Date(parseYear,displayMonth,1);
        var day = -1;
        var startDayOfWeek = newCal.getDay();
        
        if( (today.year == newCal.getFullYear()) && (today.month == newCal.getMonth()) )
	    {
			day = today.day;
        }
        
        var intDaysInMonth = getDays(newCal.getMonth(), newCal.getFullYear());
        var daysGrid = makeDaysGrid(startDayOfWeek,day,intDaysInMonth,newCal,eltName);
       
	    if( isIE )
	    {
	 
	       var elt = document.all[eltName];
	     
	       elt.innerHTML = daysGrid;
	       
	       
	       
	    }
	    else
	    {
	       var elt = document.layers[eltName].document;
	       elt.open();
	       elt.write(daysGrid);
	       elt.close();
	    }
	 }

	 function incMonth(delta,eltName)
	 {
		displayMonth += delta;
		if (displayMonth >= 12)
		{
			displayMonth = 0;
			incYear(1,eltName);
		}
		else if ( displayMonth <= -1 )
		{
			displayMonth = 11;
			incYear(-1,eltName);
		}
		else
		{
			newCalendar(eltName);
		}
		//由于日历控件有可能被下拉框遮盖
		document.all["calenderMaskFrame"].style.zIndex =-1;
		//elt.zIndex = 999;
	 }

	 function incYear(delta,eltName)
	 {
		displayYear = parseInt(displayYear + '') + delta;
		newCalendar(eltName);
		
		//由于日历控件有可能被下拉框遮盖
		document.all["calenderMaskFrame"].style.zIndex =-1;
		//elt.zIndex = 999;
	 }

	 function makeDaysGrid( startDay,day,intDaysInMonth,newCal,eltName )
	 {
	    var daysGrid;
	    var month = newCal.getMonth();
	    var year = newCal.getFullYear();
	    var isThisYear = (year == new Date().getFullYear());
	    var isThisMonth = (day > -1);
	    
	    daysGrid = '<table border=0 cellspacing=1 cellpadding=0 width=150 height=118 bgcolor=#808080 style="font-size:12px"><tr height=20><td bgcolor=#f5f5f5 nowrap align=center>';
	    daysGrid += '<font face="courier new, courier" style="font-size:12px;cursor: hand">';
	    daysGrid += '<a href="javascript:incYear(-1,\'' + eltName + '\')">&laquo; </a>';
	    daysGrid += '<b>';
	    if (isThisYear) { daysGrid += '<font color=red>' + year + '</font>'; }
	    else { daysGrid += '<font color=blue>'+ year + '</font>'; }
	    daysGrid += '</b>年';


	    daysGrid += '<a href="javascript:incYear(1,\'' + eltName + '\')"> &raquo;</a>&nbsp;&nbsp;';

	    daysGrid += '<a href="javascript:incMonth(-1,\'' + eltName + '\')">&laquo; </a>';
	    daysGrid += '<b>';
	    if (isThisMonth) { daysGrid += '<font color=red>' + months[month] + '</font>'; }
	    else { daysGrid += '<font color=blue>' + months[month] + '</font>'; }
	    daysGrid += '</b>月';

	    daysGrid += '<a href="javascript:incMonth(1,\'' + eltName + '\')"> &raquo;</a>';
	    daysGrid += '</font>';
	    daysGrid += '</td><td bgcolor=#f5f5f5 nowrap align=center onclick=hideElement(\'' + eltName + '\')>';
	    daysGrid += '<font face="courier new, courier" style="font-size:13px;cursor: hand">';
	    daysGrid += '<b>×</b>';



	    daysGrid += '</font></td></tr><tr><td bgcolor=ffffff colspan=2><table border=0 cellspacing=1 cellpadding=0 width=150 height=100   style="font-size:12px"><tr bgcolor=#999999 height=18>';

	    daysGrid += '<td align=center><font color=ffffff>日</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>一</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>二</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>三</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>四</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>五</font></td>';
	    daysGrid += '<td align=center><font color=ffffff>六</font></td>';

	    daysGrid += '</tr>';

	    
	    var dayOfMonthOfFirstSunday = (7 - startDay + 1);
	    
	    for (var intWeek = 0; intWeek < 6; intWeek++)
	    {
			var dayOfMonth;
			daysGrid += '<tr bgcolor=#ffffff height=20>';
			for (var intDay = 0; intDay < 7; intDay++)
			{
				
				dayOfMonth = (intWeek * 7) + intDay + dayOfMonthOfFirstSunday - 7;
				if (dayOfMonth <= 0)
				{
					daysGrid += '<td>&nbsp;</td>';
				}
				else if( dayOfMonth <= intDaysInMonth )
				{
				    var color = "#236900";
					if (day > 0 && day == dayOfMonth) color="red";

					daysGrid += '<td align=center bgcolor=#E6F3DA><a href="javascript:setDay(';
					daysGrid += dayOfMonth + ',\'' + eltName + '\')" '
					daysGrid += 'style="color:' + color + '">';
					var dayString = dayOfMonth + "</a></td>";
					if (dayString.length == 6) dayString = '0' + dayString;
					daysGrid += dayString;
				}
			}
			
			if (dayOfMonth < intDaysInMonth) daysGrid += '</tr>';
		}
	    //由于日历控件有可能被下拉框遮盖
	    return daysGrid + "</table></td></tr></table><IFRAME id=\"calenderMaskFrame\" name=\"calenderMaskFrame\" width=170 height=138 style=\"zIndex:-1;filter: alpha(opacity=0);display:block;LEFT: 0px; POSITION: absolute; TOP: 0px\" src=\"javascript:false;\" frameBorder=\"0\" scrolling=\"no\"></IFRAME>";
	    //return daysGrid + "</table></td></tr></table>";
	 }

	 function setDay(day, eltName) {
	     var month = displayMonth < 9 ? '0' + String(displayMonth + 1) : String(displayMonth + 1);
	     var displayday = day < 10 ? '0' + day : day;

	     displayElement.value = displayYear + "-" + month + "-" + displayday;
		
		
		hideElement(eltName);
	 }


	function fixPosition( eltname, pos )
	{
		elt = getXBrowserRef(eltname);
		positionerName = pos;
		// hint: try setting isPlacedUnder to false
		isPlacedUnder = false;
		if( isPlacedUnder )
		{
			setPosition(elt,positionerName,true);
		}
		else
		{
			setPosition(elt,positionerName)
		}
	}


	function toggleDatePicker(eltName,formElt, pos)
	{
	  
	   
		var x = formElt.indexOf('.');
		var formName = formElt.substring(0,x);
	   
		var formEltName = formElt.substring(x+1);
		
		
		if( formName.length > 0 )
		{
		    
			newCalendar(eltName,document.forms[formName].elements[formEltName]);
		}
		else
		{
		 
			newCalendar(eltName,document.all[formEltName]);
		}
		
		toggleVisible(eltName, pos);
	}