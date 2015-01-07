    function editSurvey(SID1){
        top.location.href = "editsurvey.aspx?SID="+SID1+"&PageNo="+intCurrPageNo;
    }
	
	function advOption(SID1){
        self.location.href = "advOption.aspx?SID="+SID1+"&PageNo="+intCurrPageNo;
    }
	
	function viewInfo(SID1){
		document.getElementById("InfoWin").src = "ShowSurveyInfo.aspx?SID="+SID1;
		document.getElementById("Layer1").style.display = "block";
	}
	
	function delSurvey(SN){
		if(confirm("确定删除？")){
			
		}
		else{
			return;
		}

		
		if(arrSurvey[SN][2]=="0"){
	        self.location.href = "SurveyList1.aspx?SID="+arrSurvey[SN][0]+"&A=d&PageNo="+intCurrPageNo;
		}
		else{
	        self.location.href = "DeleteSurvey.aspx?SID="+arrSurvey[SN][0]+"&PageNo="+intCurrPageNo;
		}
    }
    
    function statIndex(SID1){
        //top.location.href = "StatIndex.aspx?SID="+SID1;
        top.location.href = "StatIndex.aspx?SID="+SID1;
    
    }
    
    function getCode(SID1){
        //top.location.href = "StatIndex.aspx?SID="+SID1;
        self.location.href = "GetCode.aspx?SID="+SID1+"&PageNo="+intCurrPageNo;
    
    }
    
    function setupActive(SID1){

        self.location.href = "SurveyList1.aspx?SID="+SID1+"&A=a&PageNo="+intCurrPageNo;     
    }
	
	function setOption(SID1){
        self.location.href = "SetupOption.aspx?SID="+SID1+"&PageNo="+intCurrPageNo;     
    }
    
	
	function formatStr(sInput,intShowLen){
		
		if(sInput.length <= intShowLen){			
			return sInput;
		}
		
		
		
		return  sInput.substr(0,intShowLen)+"...";
		
	}
    
    function initSurveyList(){

        var sCell1 = "";
        var sCell2 = "";
        var sCell3 = "";
        var sCell4 = "";
        var sCell5 = "";
        var sCell6 = "";
        var sCell7 = "";
        var sCell8 = "";
        var sCell9 = "";
		var sCell10 = "";
		var sCell11 = "";
		var sCR = "";//自定义报表
        var sSurveyList = '';
        var sHead = "";
        sHead = "<tr bgcolor='#ACCFF5' align='center' style='height:37px; width:100%; background:url(../survey/images/f2.gif) repeat-x 0px -147px'>";
        sHead += "<td class='verticalLine' ></td><td style='width:200px;overflow:hidden' class='verticalLine' title='问卷名\n问卷创建日期\n最新答卷时间'>问卷名/创建/更新日期</td>";
        sHead += "<td class='verticalLine'>任务进度<BR>完成/任务</td>";
        sHead += "<td class='verticalLine' title='问卷编辑完成的状态'>编辑状态</td>";
        sHead += "<td class='verticalLine' title='问卷启用禁用状态'>活动状态</td>";
        sHead += "<td class='verticalLine' title='问卷调用的代码'>问卷代码</td>";
		//sHead += "<td class='verticalLine' title='自定义报表'>定义报表</td>";
        sHead += "<td class='verticalLine' title='数据分析\n答卷查看\n数据导出'>分析</td>";
        sHead += "<td class='verticalLine' title='问卷参数的设置'>选项</td>";
		//sHead += "<td class='verticalLine' title='问卷参数的设置'>观察员</td>";
        sHead += "<td class='verticalLine' title='清空答卷\n反生成问卷\n导出答卷密码\n自定义问卷报表\n问卷观察员设置\n问卷配额管理'>高级</td>";
		sHead += "<td class='verticalLine' title='进行问卷编辑'>编辑</td>";
        sHead += "<td class='verticalLine' title='删除问卷'>删除</td>";
        sHead += "</tr>";
        for(i=0; i<arrSurvey.length; i++){
            
            
        
            if(arrSurvey[i][5]==0){
             
                sCell2 = "<td  class='t'> "+arrSurvey[i][4]+"/不限</td>";
            }
            else{
   
                sCell2 =  "<td  class='t'>"+arrSurvey[i][4]+"/"+arrSurvey[i][5]+"</td>";
            }
            
            if(arrSurvey[i][2]=="0"){
                sCell1 = "<td  class='t'   title='"+arrSurvey[i][1]+"'><img src='images/info.gif' alt='查看问卷概况' onclick='viewInfo("+arrSurvey[i][0]+")' style='cursor:pointer;color:#FF0000' hspace='5'></td><td  class='t' onclick='reName("+i+")' align='left'  title='"+arrSurvey[i][1]+"'><strong  style='color:#FF0000'><span id=SName"+arrSurvey[i][0]+" contenteditable=true class=none onKeyDown=getEvent(event,this.innerText,"+arrSurvey[i][0]+")>"+formatStr(arrSurvey[i][1],15)+"</span></strong><BR><span class='GrayFont'>"+arrSurvey[i][7]+"</span></td>";
            
                sCell3 = "<td  class='t' title='问卷未生成，不能使用此功能'><img src='images/SurveyNotComplate.gif' alt=''>未生成</td>";
                sCell5 = "<td  class='t' title='问卷未生成，不能使用此功能'><img src='images/ie.gif' style='filter:gray' alt=''>调用</td>";
				//sCR = "<td  class='t' title='问卷未生成，不能使用此功能'><img src='images/cr.png' style='filter:gray' alt=''>定义</td>";
                sCell6 = "<td  class='t' title='问卷未生成，不能使用此功能'><img src='images/stat.gif' style='filter:gray' alt=''>分析</td>";
            }
            else{
 
                sCell1 = "<td  class='t' title='"+arrSurvey[i][1]+"'><img src='images/info.gif' alt='查看问卷概况' onclick='viewInfo("+arrSurvey[i][0]+")' style='cursor:pointer' hspace='5'></td><td  class='t' onclick='reName("+i+")' align='left'  title='"+arrSurvey[i][1]+"'><strong><a href='UserData/U"+UID+"/S"+arrSurvey[i][0]+".aspx' target='_blank' style='cursor:pointer;color:#006600'><span id=SName"+arrSurvey[i][0]+" contenteditable=true class=none onKeyDown=getEvent(event,this.innerText,"+arrSurvey[i][0]+")>"+formatStr(arrSurvey[i][1],14)+"</span></a></strong><BR><span class='GrayFont'>"+arrSurvey[i][7]+" / "+arrSurvey[i][6]+"</span></td>";
                
                sCell3 = "<td  class='t'  style='cursor:pointer'  title='已生成'><a href='userdata/u"+UID+"/s"+arrSurvey[i][0]+".aspx' target='_blank'><img  border='0' src='images/SurveyComplate.gif' alt=''>已生成</a></td>";
                sCell5 = "<td  class='t'  style='cursor:pointer'  title='问卷调用代码' onclick='getCode("+arrSurvey[i][0]+")'><img src='images/ie.gif' style='cursor:pointer'>调用</td>";
				//sCR = "<td  class='t'  style='cursor:pointer'  title='点击进入定义报表' onclick='customizeReprot("+arrSurvey[i][0]+")'><img src='images/cr.png' style='cursor:pointer'>定义</td>";
                sCell6 = "<td  class='t'  style='cursor:pointer'  title='统计分析\n答卷明细\n问卷报告\n数据导出\n查询分析\n交叉分析\n数据查询\n数据修改\n随机答卷' onclick='statIndex("+arrSurvey[i][0]+")' ><img src='images/stat.gif'>分析</td>";
            }
            
            if(arrSurvey[i][3]=="0"){
                sCell4 = "<td  class='t' onclick='setupActive("+arrSurvey[i][0]+")'   style='cursor:pointer'  title='当前禁用，点击启用'><img src='images/stop.gif'>禁用</td>";
            }
            else{
                sCell4 = "<td  class='t' onclick='setupActive("+arrSurvey[i][0]+")'  style='cursor:pointer'  title='当前启用，点击禁用'><img src='images/run.gif'>启用</td>";
            }
            
            sCell7 = "<td  class='t'  style='cursor:pointer' onclick='setOption("+arrSurvey[i][0]+")' title='设置选项'><img src='images/modifyoption.gif'>选项</td>"
            sCell8 = "<td  class='t'  onclick='advOption("+arrSurvey[i][0]+")' style='cursor:pointer' title='高级设置'><img src='images/advBT.gif'></td>"
			sCell9 = "<td  class='t'  onclick='editSurvey("+arrSurvey[i][0]+")' style='cursor:pointer' title='编辑问卷'><img src='images/edit2.gif'>编辑</td>"
            sCell10 = "<td  class='t'   onclick='delSurvey("+i+")'  style='cursor:pointer' title='删除问卷'><img src='images/del.gif'>删除</td>"
			//sCell11 = "<td  class='t' onclick='issue("+arrSurvey[i][0]+")'   style='cursor:pointer' title='设置问卷观察员'><img src='images/issue.png'>设置</td>"
     
            sSurveyList += "<tr style='height:40px' onmousemove=this.style.backgroundColor='#E2F2FF'  onmouseout=this.style.backgroundColor='#FFFFFF' align='center'>"+sCell1+sCell2+sCell3+sCell4+sCell5+sCR+sCell6+sCell7+sCell11+sCell8+sCell9+sCell10+"</tr>";
        }
        
document.getElementById("SurveyList").innerHTML = '<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="BlackFont">'+sHead+sSurveyList+'</table>';    
    }
	
function issue(id){
	//location.href = "submitorder.aspx?sid="+id;
	location.href = "observerset.aspx?sid="+id;
}
	
function setPageList(){
	var sTemp = ""		
	var intBigPage = 10;
	var intStart = parseInt(intCurrPageNo/intBigPage)*intBigPage
	var intEnd = intStart + intBigPage
	var intTempPage
	var sURL = ""	
	//sURL = "&SID="+SID
	var intTotalPageNo =  Math.abs(Math.floor(-intRecordAmount/intPageSize));	
	if (intEnd >= intTotalPageNo){
		intEnd = intTotalPageNo  		
	}
	if (intStart==0){
		intStart = 1
	}
	
	for (i=intStart; i<(intEnd+1); i++){
		intTempPage = i 
		if (intTempPage!=intCurrPageNo){
			sTemp += "<td style='width:15px' onclick=toURL('SurveyList1.aspx?PageNo="+intTempPage + sURL +"') id='Page"+i+"' onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)>"+intTempPage +"</td>"
		}
		else{
			sTemp += "<td style='width:15px' class='RedFont'><strong>"+intTempPage+"</strong></td>"
		}
	}
	document.getElementById("pagelist1").innerHTML = "记录数:"+intRecordAmount+"&nbsp;"+intPageSize+"记录/页"+intCurrPageNo+"/"+intTotalPageNo
	var s1,s2,s11,s21,s13,s23
	if (intCurrPageNo==1){
		s1 = "<td><span class=GranFont>第一页</span></td>"
		s11 = "<td><span class=GranFont>上一页</span></td>"
	}
	else{
		s1 = "<td onclick=toURL('SurveyList1.aspx?PageNo=1"+sURL+"') id='FirstPage'  onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)>第一页</td>"
		s11 = "<td  onclick=toURL('SurveyList1.aspx?PageNo="+(intCurrPageNo-1)+sURL+"') id='UpPage' onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)>上一页</td>"
	}
	
	if (intCurrPageNo<intBigPage){
		s13 = "<td><span class=GranFont>前十页</span></td>"
	}
	else{
		s13 = "<td  onclick=toURL('SurveyList1.aspx?PageNo="+(intCurrPageNo-intBigPage)+sURL+"') id='UpPage10' onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)><span class=blackfont>前十页</span></td>"	
	}
	
	if (intCurrPageNo>=parseInt(intTotalPageNo/intBigPage)*intBigPage){
		s23 = "<td  ><span class=GranFont>后十页</span></td>"
	}
	else{
		s23 = "<td  onclick=toURL('SurveyList1.aspx?PageNo="+(intCurrPageNo+intBigPage)+sURL+"')  id='LastPage10' onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)><span class=blackfont>后十页</span></td>"	
	}
	
	if (intCurrPageNo==intTotalPageNo){
		s2 = "<td ><span class='GranFont'>最后一页</span></td>"
		s21 = "<td <span class='GranFont'>下一页</span></td>"
	}
	else{
		s2 = "<td onclick=toURL('SurveyList1.aspx?PageNo="+intTotalPageNo+sURL+"') id='LastPage'  onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)>最后一页</td>"
		s21 = "<td onclick=toURL('SurveyList1.aspx?PageNo="+(intCurrPageNo+1)+sURL+"') id='NextPage' onmouseover=setBG(0,this.id) onmouseout=setBG(1,this.id)>下一页</td>"
	}
	document.getElementById("pagelist2").innerHTML = '<table  border="0" cellpadding="5" cellspacing="0" bgcolor="#EEEEEE"><tr bgcolor="#FFFFFF" style="cursor:default;text-align:center">'+s1+s13+s11+sTemp+s21+s23+s2+"</tr></table>";
	

}


function setBG(d,ID){
	if(d==0){
		document.getElementById(ID).style.backgroundColor  = "#0000FF";
		document.getElementById(ID).style.color  = "#FFFFFF";
	}
	else{
		document.getElementById(ID).style.backgroundColor  = "#FFFFFF";
		document.getElementById(ID).style.color  = "#000000";
	}
}

function toURL(ToURL){
	self.location.href = ToURL;	
}


function reName(SN){
	for(j=0; j<arrSurvey.length;j++){
		if(SN==j){
			document.getElementById("SName"+arrSurvey[j][0]).className = "editstatus1";	
		}
		else{
			document.getElementById("SName"+arrSurvey[j][0]).className = "none";	
		}
	}
	
	document.getElementById("Message").innerHTML = "<BR>消息:<span class=message1>在输入框中输入新问卷名，回车保存操作</span>";
}

function getEvent(event,v,SID1){
	
	
	if (event.keyCode==13) {


	v = trim(v);
	if(v==""){
		alert("输入不能为空");			
		this.focus();
		return false;
	}
	
	if(v.length>=50){
		alert("输入最多为50字符");	
		this.focus();
		return false;
	}
	
		this.target = "_blank";
		this.location.href = "SurveyList1.aspx?SID="+SID1+"&NewName="+v+"";

		this.focus();
		return false;
	}   	
}







function trim(s){ 
    s=removebeforechar(s); 
    s=removeafterchar(s); 
    return s; 
} 
//去前空格 
function removebeforechar(t) 
{ 
  if(t.charCodeAt(0)==32 || t.charCodeAt(0)==12288) 
  { 
    t=t.slice(1,t.length); 
    return   removebeforechar(t); 
  } 
  else 
   { 
     return t; 
   } 
} 
//去后空格 
function removeafterchar(t) 
{ 
  if(t.charCodeAt(t.length-1)==32 || t.charCodeAt(t.length-1)==12288) 
  { 
    t=t.slice(0,t.length-1); 
    return   removeafterchar(t); 
  } 
  else 
   { 
     return t; 
   } 
}


function customizeReprot(id){
	document.getElementById("hl").href = "CustomizeReport.aspx?sid="+id;
	document.getElementById("hl").target = "_blank";
	document.getElementById("hl").click();
	
}