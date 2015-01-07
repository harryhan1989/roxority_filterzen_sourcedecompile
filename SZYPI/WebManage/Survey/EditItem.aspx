<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_EditItem, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>编辑题目</title>
<script language="javascript" type="text/javascript" src="Js/interface.js"></script>
<script language="javascript" type="text/javascript">
var intPageNo,SID;
var enCreateNum = 0;
var createdNum = 0;
<%=sClientJs %>
function setPageNo(){    
	if(intPageNo>0){
		intPageNo--;
	}
//	document.getElementsByTagName("option")(intPageNo).selected=true;
    document.getElementById("PageNo").selectedIndex = intPageNo;
}

function setParentWin(){
	 window.parent.complateActionWin();
	 var myface = new interface();
	 var _P = myface._getShowSize();
	 document.getElementById("targetWin").style.height = (_P.h-20)+"px";
}
function reloadItemList(f){
    document.getElementById("targetWin").src = "ItemList.aspx?SID="+SID+"&f="+f;
}



function checkItemNum(){
	if(enCreateNum==0){
		return;	
	}
	if(createdNum>=enCreateNum){
		$("Submit").disabled="disabled";
		CloseWin();
		alert("问卷超出了所允许的最大题目数");
		
	}
}
</script>
<script language="javascript" type="text/javascript" src="Js/ServerDataCheck.js"></script>

<link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
<link href="../css/edititem.css" rel="stylesheet" type="text/css" />

</head>
<body onunload="window.returnValue=blnActioned">
<%--<div id="Layer1" style="display:none; height:300px" class="ShadowEffect">
	<div style="height:25px; width:100%; cursor:pointer"  class="TableHead"  title="关闭窗口" onclick="document.getElementById('Layer1').style.display='none'">
	<span style="font-weight:bold; width:100%; line-height:25px; margin-left:5px">插入常用备选项</span>
	<span style=" float:right; margin:5px"><img src="images/close1.gif" alt="关闭窗口" width="15" height="15" /></span>
	<iframe src="InCommonUseOption.htm" style="width:100%; height:300px" frameborder="0"></iframe>
	</div>

</div>

<div style="height:25px; width:100%; cursor:pointer"  class="TableHead"  title="关闭窗口" onclick="CloseWin()">
	<span style="font-weight:bold; width:100%; float:left; line-height:25px; margin-left:5px">编辑题目<span style="color:#666666">[点击关闭窗口]</span></span>
	<span style=" float:right; margin:5px; margin-top:-20px"><img src="images/close1.gif" alt="关闭窗口" width="15" height="15" /></span>
</div>--%>

<table  border="0" cellpadding="0" cellspacing="0" class="BlackFont" style="width:100%;">
  <tr>
    <td valign="top">
		<table  border="0" cellpadding="0" cellspacing="0"  style="height:546px">
   		  <tr>

            <td class="ItemNormal"  id="T4"  onclick="setStyle(this.id,4)"  onmouseover="mouseMoveBT(4,0)"  onmouseout="mouseMoveBT(4,1)"  style=" background-image:url(images/forms/4.gif)"> 单选题[点选式]</td>
		   
      		<td rowspan="20" valign="top" style="background-color:#FFFFFF; width:397px; ">
	  
	  			<form action="" method="post" name="form1" target="targetWin" id="form1" onsubmit="return checkForm()">
                	<table   border="0" cellpadding="5" cellspacing="0" style="height: 100%;width:360px; display:none"  id="EditArea">
  						<tr>
							<td valign="top">

                                <span class="style1">*</span>
                                题目名<span id="ItemName_Message" class="style1"></span><br />
                                <input type="text" maxlength="200" name="ItemName" id="ItemName" size="50" /><br />
                                题目说明<span class="style1" id="MemoMessage"></span><br />
                                <textarea name="Memo" cols="49" rows="2" id="Memo"></textarea>
                                
                                
                                <span id="MaxTickoffBox">
                                    <span class="style1">*</span>列举数量
                                    <input name="MaxTickoff" type="text" id="MaxTickoff" value="5" size="5" maxlength="2" />
                                    <span id="MaxTickoffItemBox_Message" class="style1"></span>
                                </span>
                                
                                
                                <span id="SubItemBox">
                                    <span class="style1">*</span>子题目(每行[回车]表示一个子题目)
                                    <span id="SubItemBox_Message" class="style1"></span>
                                    <textarea name="SubItem" cols="49" rows="4" id="SubItem"></textarea>
                                </span>
                                
                                
                                <span id="MatrixRowColumnBox">
                                    <span class="style1">*</span>矩阵选择中的行或列(每行[回车]表示一个子题目)
                                    <span id="MatrixRowColumn_Message" class="style1"></span>
                                    <textarea name="MatrixRowColumn" cols="49" rows="4" id="MatrixRowColumn"></textarea>
                                </span>
                                
                                <span id="BackSelectBox">
                                    <span class="style1">*</span>备选项(每行[回车]表示一个备选项)
                                    <span id="BackSelectBox_Message" class="style1"></span> ,<span onclick="insertNormal()" style="cursor:pointer; color:#0033CC">点击插入常用项目</span>
                                    <textarea name="BackSelect" cols="49" rows="5" id="BackSelect"></textarea>
                                </span>
                                
                                <span id="LevelBox">
                                    等级数<input name="LevelAmount" type="text" id="LevelAmount" value="5" size="10" maxlength="2" /><span id="LevelBox_Message" class="style1"></span><br />
                                    最小值名称<input name="MinLevelName" type="text" id="MinLevelName" value="最小" size="10" maxlength="30" /><br />
                                    最大值名称<input name="MaxLevelName" type="text" id="MaxLevelName" value="最大" size="10" maxlength="30" />
                                </span>
                                
                                <fieldset id="OptionImgArea"  style="padding:5px">
                                    <legend>使用图形表示备选项[<a href="example.htm" target="_blank">示例</a>]</legend>
                                    <table width="90%" border="0" cellpadding="0">
                                      <tr>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG0" value="0"  checked="checked" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG1"  value="1" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG2"  value="2" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG3"  value="3" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG4"  value="4" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG5"  value="5" /></td>
                                        <td align="center"><input type="radio" name="UseOptionIMG" id="UseOptionIMG6"  value="6" /></td>
                                      </tr>
                                      <tr>
                                        <td onclick="document.getElementById('UseOptionIMG0').checked=true" align="center"> 无</td>
                                        <td onclick="document.getElementById('UseOptionIMG1').checked=true" style="background-image:url(images/OptionICo01.gif); background-repeat:no-repeat; background-position:center; height:24px"></td>
                                        <td onclick="document.getElementById('UseOptionIMG2').checked=true"  style="background-image:url(images/OptionICo02.gif); background-repeat:no-repeat; background-position:center"></td>
                                        <td onclick="document.getElementById('UseOptionIMG3').checked=true"  style="background-image:url(images/OptionICo03.gif); background-repeat:no-repeat; background-position:center"></td>
                                        <td onclick="document.getElementById('UseOptionIMG4').checked=true"  style="background-image:url(images/OptionICo04.gif); background-repeat:no-repeat; background-position:center"></td>
                                        <td onclick="document.getElementById('UseOptionIMG5').checked=true"  style="background-image:url(images/OptionICo05.gif); background-repeat:no-repeat; background-position:center"></td>
                                         <td onclick="document.getElementById('UseOptionIMG6').checked=true"  style="background-image:url(images/OptionICo06.gif); background-repeat:no-repeat; background-position:center; text-align:center">滑杆</td>
                                      </tr>
                                    </table>
                                </fieldset>
                                
                                
                                <fieldset id="BackSelectOptionModeAreaBox" style="padding:3px">
                                    <legend>备选项排列</legend>
                                    <span id="BackSelectOptionModeArea_Message">
                                    <label><input name="OrderModel" id="OrderModel1" type="radio" value="0"/> 纵向排列</label>
                                    <label><input name="OrderModel" id="OrderModel2" type="radio" value="1"  checked="checked" /> 横向排列</label>
                                    </span>
                                </fieldset>
                                
                                <fieldset style="padding:3px" id="CheckBox"><legend>题目验证</legend>
                                    <label id="EmptyBox"><input name="Empty" type="checkbox" id="Empty" value="1" checked="checked" />必答</label>
                                    <span id="CheckBox_TextInput">
                                        <label><input name="PostCode" type="checkbox" id="PostCode" value="1" onclick="checkTest(this.id)" />邮编</label>
                                        <label><input name="Mob" type="checkbox" id="Mob" value="1" onclick="checkTest(this.id)" />手机号</label>
                                        <label><input name="Email" type="checkbox" id="Email" value="1" onclick="checkTest(this.id)" />Email</label>
                                        <label><input name="URL" type="checkbox" id="URL" value="1" onclick="checkTest(this.id)" />网址</label>    
                                        <label><input name="Cn" type="checkbox" id="Cn" value="1" onclick="checkTest(this.id)" />中文</label>
                                        <label><input name="En" type="checkbox" id="En" value="1" onclick="checkTest(this.id)" />英文</label>
                                        <label><input name="IDCard" type="checkbox" id="IDCard" value="1" onclick="checkTest(this.id)" />身份证号</label>
                                        <label><input name="Data" type="checkbox" id="Data" value="1" onclick="checkTest(this.id)" />日期</label>    
                                        最少字符数<input name="MinLen" type="text" id="MinLen" value="0" size="2" maxlength="3" />
                                        最多字符数<input name="MaxLen" type="text" id="MaxLen" value="50" size="2" maxlength="3" />
                                        <span id="MinMaxLen_Message" class="style1"></span>
                                    </span>
                                    
                                    <span id="InputLenghBox">
                                        <input name="InputLength" id="InputLength" value="20" size="3" maxlength="2"  />输入项的最多字符数，最大50，最小1
                                        <span id="InputLenghBox_Message" class="style1"></span>
                                    </span>
                                    
                                    <span id="MinMaxSelectBox">
                                        最少选择<input name="MinSelect" type="text" id="MinSelect" value="1" size="5" maxlength="2" />个，
                                        最多选择<input name="MaxSelect" type="text" id="MaxSelect" value="10" size="5" maxlength="2" />个
                                        <span id="MinMaxSelectBox_Message" class="style1"></span>
                                    </span>
                                    
                                    <span id="MinMaxValueBox">
                                        最小值<input name="MinValue" type="text" id="MinValue" value="0" size="5" maxlength="10" />，
                                        最大值<input name="MaxValue" type="text" id="MaxValue" value="999999999" size="10" maxlength="10" />
                                        <span id="MinMaxValueBox_Message" class="style1"></span><br /> 
                                    </span>
                                    
                                     <span id="PercentBox">
                                        百分比总数<input name="TotalPercent" type="text" id="TotalPercent" value="100" size="5" maxlength="10" /><br />
                                        最小百分比<input name="MinPercent" type="text" id="MinPercent" value="0" size="5" maxlength="10" />
                                        最大百分比<input name="MaxPercent" type="text" id="MaxPercent" value="100" size="5" maxlength="10" />
                                        <span id="PercentBox_Message" class="style1"></span>
                                    </span>
                                    
                                    <span id="MinMaxTickoffBox">
                                    最少列举多少项<input name="MinTickoff" type="text" id="MinTickoff" value="1" size="5" maxlength="2" />
                                    <span id="MinMaxTickoffBox_Message" class="style1"></span></span>
                                </fieldset>
                                
                                
                                <fieldset style="padding:5px; display:none" id="UploadOption">
                                    <legend>上传选项</legend>
                                    附件上限<input name="MaxFileLen" type="text" id="MaxFileLen" value="100" size="3" maxlength="3" />k <span id="UploadFile_M_MaxFileLen" class="style1"></span><br />
                                    上传类型<br /><input type="radio" name="UploadMode" id="UploadMode0" value="0" /> 白名单模式，只允许上传下面指定的类型<br />
                                    <input type="radio" name="UploadMode" id="UploadMode1" value="1" /> 黑名单模式，不允许上传下面指定的类型 <br />
                                    文件类型,为空表示无限制,使用点作为分隔<br />
                                    <input name="FileType" type="text" id="FileType" value="" size="50" maxlength="150" /><br />
                                    <span style="color:#999999">例:选择白名单，填写.jpg.gif.png表示只允许上传“jpg”、“gif”、“png”类型的文件。选择黑名单，填写，.exe.dll表示不允许上传“exe”、“dll”类型的文件</span><br />
                                </fieldset>
                                
                                
                                        
                                    <div style="border:#999 1px solid; padding:3px;background-color:#EEE; width:380px; float:left; margin-top:5px; cursor:pointer;" onmouseover="this.style.backgroundColor='#FFC'"  onmouseout="this.style.backgroundColor='#EEE'"><input name="Submit" id="Submit" type="submit" class="SaveBT" value="保存" />
                                    <input name="Flag" type="hidden" id="flag" value="0" /><span class="PageNoBox"></span>
                                
                                    <span class="style1">*</span>号为必填 题目放在<select id="PageNo" name="PageNo" ><%=sPageList%> </select></div>
        
    						</td>
  					</tr>
				</table>
				<iframe id="ItemLib" style="height:520px; display:none" frameborder="0" marginheight="1" marginwidth="1"></iframe>
  		</form>
  
  		</td>
      </tr>
      <tr>
        <td class="ItemNormal"  id="T5"  onclick="setStyle(this.id,5)"  onmouseover="mouseMoveBT(5,0)"  onmouseout="mouseMoveBT(5,1)"  style=" background-image:url(images/forms/5.gif)"> 单选题+文字输入</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T6"  onclick="setStyle(this.id,6)"  onmouseover="mouseMoveBT(6,0)"  onmouseout="mouseMoveBT(6,1)"  style=" background-image:url(images/forms/6.gif)"> 单选题[下拉列表]</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T8"  onclick="setStyle(this.id,8)"  onmouseover="mouseMoveBT(8,0)"  onmouseout="mouseMoveBT(8,1)"  style=" background-image:url(images/forms/8.gif)"> 多选题[点选式]</td>
    
      </tr>
      <tr>
        <td class="ItemNormal"  id="T9"  onclick="setStyle(this.id,9)" onmouseover="mouseMoveBT(9,0)"  onmouseout="mouseMoveBT(9,1)"  style=" background-image:url(images/forms/9.gif)"> 多选+文字输入</td>
  
      </tr>
      <tr>
        <td class="ItemNormal"  id="T10"  onclick="setStyle(this.id,10)"  onmouseover="mouseMoveBT(10,0)"  onmouseout="mouseMoveBT(10,1)"  style=" background-image:url(images/forms/10.gif)"> 多选题[列表式]</td>
    
      </tr>
      <tr>
      	<td class="ItemNormal"  id="T22"  onclick="setStyle(this.id,22);openItemLib()"  onmouseover="mouseMoveBT(22,0)"  onmouseout="mouseMoveBT(22,1)"  style=" background-image:url(images/lib.gif)">题目库</td>
   
      </tr>	
      
      <%-- 分隔线--%>
            <tr>
      	<td class="ItemNormal"  id="T1"    >&nbsp;</td>
      </tr>
      <tr>
        <td class="ItemNormal"  id="T2"    > &nbsp;</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T3"    > &nbsp;</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T7"    >&nbsp; </td>
   
      </tr>

      <tr>
        <td class="ItemNormal"  id="T11"   > &nbsp;</td>
     
      </tr>
      <tr>
        <td class="ItemNormal"  id="T12"   > &nbsp;</td>
        
      </tr>
      <tr>
        <td class="ItemNormal"  id="T13"   >&nbsp; </td>
     
      </tr>
      <tr>
        <td class="ItemNormal"  id="T14"   >&nbsp;</td>
       
      </tr>
	  
	  
	  <tr>
        <td class="ItemNormal"  id="T15"   >&nbsp;</td>
     
      </tr>
	  
      <tr>
        <td class="ItemNormal"  id="T16"   >&nbsp;</td>       
	  </tr>
       <tr>
        <td class="ItemNormal"  id="T17"   >&nbsp;</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T18"   >&nbsp;</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T19"   >&nbsp;</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T20"   >&nbsp;</td>   
	  </tr>
      
      <tr>
        <td class="ItemNormal"  id="T21"   >&nbsp;</td>   
	  </tr>
	  
<%--      <tr>
      	<td class="ItemNormal"  id="T1"  onclick="setStyle(this.id,1)" onmouseover="mouseMoveBT(1,0)"  onmouseout="mouseMoveBT(1,1)"  style=" background-image:url(images/forms/TextInputObj.gif); visibility: hidden ;">文字输入题</td>
      </tr>
      <tr>
        <td class="ItemNormal"  id="T2"  onclick="setStyle(this.id,2)"  onmouseover="mouseMoveBT(2,0)"  onmouseout="mouseMoveBT(2,1)"  style=" background-image:url(images/forms/2.gif);visibility: hidden ;"> 数值输入题</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T3"  onclick="setStyle(this.id,3)"  onmouseover="mouseMoveBT(3,0)"  onmouseout="mouseMoveBT(3,1)"  style=" background-image:url(images/forms/3.gif);visibility: hidden ; "> 简答题</td>

      </tr>
      <tr>
        <td class="ItemNormal"  id="T7"  onclick="setStyle(this.id,7)"  onmouseover="mouseMoveBT(7,0)"  onmouseout="mouseMoveBT(7,1)"  style=" background-image:url(images/forms/7.gif);visibility: hidden ; "> 单选矩阵题[点选]</td>
   
      </tr>

      <tr>
        <td class="ItemNormal"  id="T11"  onclick="setStyle(this.id,11)"  onmousemove="mouseMoveBT(11,0)"  onmouseout="mouseMoveBT(11,1)"  style=" background-image:url(images/forms/11.gif);visibility: hidden ; "> 等级题</td>
     
      </tr>
      <tr>
        <td class="ItemNormal"  id="T12"  onclick="setStyle(this.id,12)"  onmousemove="mouseMoveBT(12,0)"  onmouseout="mouseMoveBT(12,1)"  style=" background-image:url(images/forms/12.gif);visibility: hidden ;"> 排序题</td>
        
      </tr>
      <tr>
        <td class="ItemNormal"  id="T13"  onclick="setStyle(this.id,13)" onmousemove="mouseMoveBT(13,0)"  onmouseout="mouseMoveBT(13,1)"  style=" background-image:url(images/forms/13.gif);visibility: hidden ; "> 列举题</td>
     
      </tr>
      <tr>
        <td class="ItemNormal"  id="T14"  onclick="setStyle(this.id,14)"  onmousemove="mouseMoveBT(14,0)"  onmouseout="mouseMoveBT(14,1)"  style=" background-image:url(images/forms/14.gif);visibility: hidden ; ">插入文字</td>
       
      </tr>
	  
	  
	  <tr>
        <td class="ItemNormal"  id="T15" onclick="setStyle(this.id,15)"   onmousemove="mouseMoveBT(15,0)"  onmouseout="mouseMoveBT(15,1)"  style=" background-image:url(images/forms/15.gif);visibility: hidden ;">多选矩阵</td>
     
      </tr>
	  
      <tr>
        <td class="ItemNormal"  id="T16"     onclick="setStyle(this.id,16)"   onmousemove="mouseMoveBT(16,0)"  onmouseout="mouseMoveBT(16,1)"  style=" background-image:url(images/forms/16.gif);visibility: hidden ; ">矩阵输入题</td>       
	  </tr>
       <tr>
        <td class="ItemNormal"  id="T17"      onclick="setStyle(this.id,17)"   onmousemove="mouseMoveBT(17,0)"  onmouseout="mouseMoveBT(17,1)"  style=" background-image:url(images/forms/17.gif);visibility: hidden ;">文件上传</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T18"   onclick="setStyle(this.id,18)"  onmousemove="mouseMoveBT(18,0)"  onmouseout="mouseMoveBT(18,1)"  style=" background-image:url(images/forms/18.gif);visibility: hidden ; ">3D矩阵</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T19"      onclick="setStyle(this.id,19)"   onmousemove="mouseMoveBT(19,0)"  onmouseout="mouseMoveBT(19,1)"  style=" background-image:url(images/forms/19.gif);visibility: hidden ;">矩阵单选+输入</td>   
	  </tr>
      
       <tr>
        <td class="ItemNormal"  id="T20"      onclick="setStyle(this.id,20)"  onmousemove="mouseMoveBT(20,0)"  onmouseout="mouseMoveBT(20,1)"  style=" background-image:url(images/forms/20.gif);visibility: hidden ; ">矩阵多选+输入</td>   
	  </tr>
      
      <tr>
        <td class="ItemNormal"  id="T21"   onclick="setStyle(this.id,21)"  onmousemove="mouseMoveBT(21,0)"  onmouseout="mouseMoveBT(21,1)"  style=" background-image:url(images/forms/21.gif);visibility: hidden ; ">百分比题</td>   
	  </tr>--%>
      
    </table>
    <td valign="top" style="width:1px; background-color:#333333"></td>
    <td valign="top"  style="width:250px; background-color:#FFFFFF ">
		 <iframe id="targetWin" name="targetWin"  frameborder="0" marginheight="0" marginwidth="0" style="overflow:hidden"></iframe>
	</td>
  </tr>
</table>
</body>
</html>
<script language="javascript">
    window.onload = function() {
        setPageNo();
        setParentWin();
        $("form1").action = "ItemList.aspx?SID=" + SID;
        $("targetWin").src = "ItemList.aspx?SID=" + SID;
        createdNum = window.parent.frames.targetWin.arrItem.length;
        checkItemNum();

    }
</script>
