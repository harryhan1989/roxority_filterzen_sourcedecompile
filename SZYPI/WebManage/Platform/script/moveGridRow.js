///////////////////////////////////////////////////////////////// 
var Main_Tab	= null;
var cur_row;
var cur_bgc	= "#B1CBE0";
var cur_fc	= "black";
//var color1 = "#F7F7F7";
var color1 = "#ffffff";
var color2 = "#ffffff";
var checkbox_state = "";
var cur_cell = null;
var act_bgc	= "#F7F7F7";
var act_fc	= "black";
var checkbox_state = "";
var sequence = new Array();

function window.onload()
{
	var objGrid = dgvSet;
	init(objGrid);
}

//初始化
function init(objGrid)
{
    Main_Tab = objGrid;
    Main_Tab.onclick	= clickItem; 
	
	for(var j=1;j<Main_Tab.rows.length;j++)
	{
	    var jk= j-1;
	    sequence[jk] = j;
	}
	var temp="";
	for(var n=0;n<sequence.length;n++)
	{
	    temp += sequence[n]+"|";
	}
	if(document.getElementById("seq")!=null)
	{
	    document.getElementById("seq").value = temp;
	}
}

//上移
 function Move_up(the_table)
{
	event.cancelBubble = true;
	if(cur_row==null || cur_row<=1) return;
	setSeq(1);
	change_row(the_table,cur_row,--cur_row);
}

//下移
function Move_down(the_table){
	event.cancelBubble = true;
	if(cur_row==null || cur_row==the_table.rows.length-1 || cur_row==0) return;
	setSeq(0);
	change_row(the_table,cur_row,++cur_row);
}

function setSeq(i)
{
    var te = "";
    var temp="";
    if(i==1)
    {
        te = sequence[cur_row-1];
        sequence[cur_row-1] = sequence[cur_row-2];
        sequence[cur_row-2] = te;  
    }
    else if(i==0)
    {
        te = sequence[cur_row-1];
        sequence[cur_row-1] = sequence[cur_row];
        sequence[cur_row] = te;
    }
    for(var n=0;n<sequence.length;n++)
    {
        temp += sequence[n]+"|";
    }   
    document.getElementById("seq").value = temp;
}

function setState(obj)
{
    var temp="";
    var rep_str="";
    var res_str="";
    
    if(obj.checked == true)
    {
        rep_str = obj.id +"|"+"false";
        res_str = obj.id +"|"+"true";
        temp = checkbox_state.replace (rep_str,res_str);
        checkbox_state = temp;
    }
    else
    {
        rep_str = obj.id +"|"+"true";
        res_str = obj.id +"|"+"false";
        temp = checkbox_state.replace (rep_str,res_str);
        checkbox_state = temp;
    }
    
}

function get_Element(the_ele,the_tag){
	the_tag = the_tag.toLowerCase();
	if(the_ele.tagName.toLowerCase()==the_tag)return the_ele;
	while(the_ele=the_ele.offsetParent){
		if(the_ele.tagName.toLowerCase()==the_tag)return the_ele;
	}
	return(null);
}

function change_row(the_tab,line1,line2)
{
	the_tab.rows[line1].swapNode(the_tab.rows[line2])
}

function clickItem()
{
	event.cancelBubble=true;
	var the_obj = event.srcElement;
	var i = 0 ,j = 0;
	
	if (the_obj.tagName.toLowerCase() != "table" && the_obj.tagName.toLowerCase() != "tbody" && the_obj.tagName.toLowerCase() != "tr")
	{
	   	var the_td	= get_Element(the_obj,"td");
		if(the_td==null) return;
		var the_tr	= the_td.parentElement;
		var the_table	= get_Element(the_td,"table");
		initcolor(the_table,the_tr.cells.length);
		var i = 0;
		cur_row = the_tr.rowIndex;
		if(cur_row!=0)
		{
			for(i=0;i<the_tr.cells.length;i++)
			{
				with(the_tr.cells[i])
				{
					style.backgroundColor=cur_bgc;
					style.color=cur_fc;
				}
			}
		}
	}
}

function initcolor(tab,len)
{
   var trig=0;
   for(var i=len;i<tab.cells.length;i++)
   {
        if(i%len ==0)
        {
            trig++;
        }
        if(trig%2==0)
        {
            tab.cells[i].style.backgroundColor = color2;
        }
        else
        {
            tab.cells[i].style.backgroundColor = color1;
        }
    }
}
