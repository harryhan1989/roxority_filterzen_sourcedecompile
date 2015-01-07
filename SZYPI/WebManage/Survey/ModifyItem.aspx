<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_ModifyItems, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<style type="text/css">
.style1{
color:#990000;
}</style>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript" src="Js/ServerDataCheck.js"></script>
<script language="javascript" type="text/javascript"><%=sClientJs%></script>
<script language="javascript" type="text/javascript" src="Js/ModifyItem.js"></script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body   style="background-color:#EFF3FB ">
<form action="SaveModifyItem.aspx" method="post" name="form1" target="targetWin" id="form1" onsubmit="return checkForm()">
  <table   border="0" cellpadding="5" cellspacing="0" class="BlackFont"  id="EditArea" style="height: 100%; display:block; width:100%;">
  <tr>
<td valign="top"  style="height: 100%">
<div id="ItemNameBox">
    <span class="style1">*</span>
    题目名<span id="ItemName_Message" class="style1"></span><br /> 
    <input name="ItemName" type="text" id="ItemName" value="" size="48" runat="server" maxlength="200" />
</div>

<div>
    题目说明<br />
    <span class="style1" id="MemoMessage"></span>
    <textarea name="Memo" cols="40" rows="3" id="Memo" runat="server" ></textarea>
</div>

<div id="MaxTickoffBox">
    <span class="style1">*</span>列举数量
    <input name="MaxTickoff" type="text" id="MaxTickoff" value="" size="5" runat="server"  maxlength="2" />
    <span id="MaxTickoffItemBox_Message" class="style1"></span>
</div>



<div id="SubItemBox">
<span class="style1">*</span>子题目(每行[回车]表示一个子题目)
<span id="SubItemBox_Message" class="style1"></span><br />

<textarea name="SubItem" cols="40" rows="4" id="SubItem" runat="server"></textarea><br />

</div>

<div id="MatrixRowColumnBox">
	<span class="style1">*</span>矩阵选择中的行或列(每行[回车]表示一个子题目)
    <span id="MatrixRowColumn_Message" class="style1"></span><br />

	<textarea name="MatrixRowColumn" cols="40" rows="4" id="MatrixRowColumn" runat="server" ></textarea>
</div>

<div id="BackSelectBox">
	<span class="style1">*</span>备选项(每行[回车]表示一个备选项)
    <span id="BackSelectBox_Message" class="style1"></span><br />

	<textarea name="BackSelect" cols="40" rows="4" id="BackSelect" runat="server" ></textarea>
</div>


<div id="LevelBox">
	等级数<input name="LevelAmount" type="text" id="LevelAmount" value="5" size="10" maxlength="2"  runat="server"  />
    <span id="LevelBox_Message" class="style1"></span><br />
	最小值名称<input name="MinLevelName" type="text" id="MinLevelName" value="最小" size="10" maxlength="30"  runat="server"  /><br />
	最大值名称<input name="MaxLevelName" type="text" id="MaxLevelName" value="最大" size="10" maxlength="30"  runat="server"  />
</div>


<fieldset id="OptionImgArea" style="width:320px; display:none">
<legend>使用图形表示备选项[<a href="example.htm" target="_blank">示例</a>]</legend>

	<table border="0" cellpadding="0">
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
		<td onclick="document.getElementById('UseOptionIMG0').checked=true" align="center" style="width:40px"> 无</td>
		<td onclick="document.getElementById('UseOptionIMG1').checked=true" style="background-image:url(images/OptionICo01.gif); background-repeat:no-repeat; background-position:center; height:24px; width:40px"></td>
		<td onclick="document.getElementById('UseOptionIMG2').checked=true"  style="background-image:url(images/OptionICo02.gif); background-repeat:no-repeat; background-position:center; width:40px"></td>
		<td onclick="document.getElementById('UseOptionIMG3').checked=true"  style="background-image:url(images/OptionICo03.gif); background-repeat:no-repeat; background-position:center; width:40px"></td>
		<td onclick="document.getElementById('UseOptionIMG4').checked=true"  style="background-image:url(images/OptionICo04.gif); background-repeat:no-repeat; background-position:center; width:40px"></td>
		<td onclick="document.getElementById('UseOptionIMG5').checked=true"  style="background-image:url(images/OptionICo05.gif); background-repeat:no-repeat; background-position:center; width:40px"></td>
		<td onclick="document.getElementById('UseOptionIMG6').checked=true"  style="background-image:url(images/OptionICo06.gif); background-repeat:no-repeat; background-position:center; width:40px"></td>
	  </tr>
	</table>
</fieldset>



<fieldset id="BackSelectOptionModeAreaBox" style="padding:3px">
	<legend>备选项排列</legend>
    <span id="BackSelectOptionModeArea_Message">
    <label><input name="OrderModel" id="OrderModel1" type="radio" value="0" checked="checked" /> 纵向排列</label>
    <label><input name="OrderModel" id="OrderModel2" type="radio" value="1" /> 横向排列</label>
    </span>
</fieldset>




<fieldset style="padding:3px" id="CheckBox"><legend>题目验证</legend>
	<label id="EmptyBox"><input name="Empty" type="checkbox" id="Empty" value="1" checked="checked" />必答</label>
    <div id="CheckBox_TextInput">
        <input name="PostCode" type="checkbox" id="PostCode" value="1" onclick="checkTest(this.id)" />邮编
        <input name="Mob" type="checkbox" id="Mob" value="1" onclick="checkTest(this.id)" />手机号
        <input name="Email" type="checkbox" id="Email" value="1" onclick="checkTest(this.id)" />Email
        <input name="URL" type="checkbox" id="URL" value="1" onclick="checkTest(this.id)" />网址
        <br />
        <input name="Cn" type="checkbox" id="Cn" value="1" onclick="checkTest(this.id)" />中文
        <input name="En" type="checkbox" id="En" value="1" onclick="checkTest(this.id)" />英文
        <input name="IDCard" type="checkbox" id="IDCard" value="1" onclick="checkTest(this.id)" />身份证号
        <input name="Data" type="checkbox" id="Data" value="1" onclick="checkTest(this.id)" />日期
        <br /> 
        最少字符数<input name="MinLen" type="text" id="MinLen" size="2" maxlength="3" />
        最多字符数<input name="MaxLen" type="text" id="MaxLen" size="2" maxlength="3" />
        <span id="MinMaxLen_Message" class="style1"></span>
    </div>


    <div id="InputLenghBox">
            <input name="InputLength" id="InputLength" value="20" size="3" maxlength="2"  />输入项的最多字符数，最大50，最小1
            <span id="InputLenghBox_Message" class="style1"></span>
    </div>

    <div id="MinMaxSelectBox">
        最少选择<input name="MinSelect" type="text" id="MinSelect" size="5" maxlength="2" runat="server" />个，
        最多选择<input name="MaxSelect" type="text" id="MaxSelect" size="5" maxlength="2" runat="server"  />个
        <span id="MinMaxSelectBox_Message" class="style1"></span>
    </div>


    <div id="MinMaxValueBox">
    	最小值<input name="MinValue" type="text" id="MinValue" value="" size="5" maxlength="10" runat="server"  />，
   		最大值<input name="MaxValue" type="text" id="MaxValue" value="" size="10" maxlength="10"  runat="server" />
    	<span id="MinMaxValueBox_Message" class="style1"></span>
    </div>


	<div id="PercentBox">
        百分比总数<input name="TotalPercent" type="text" id="TotalPercent" value="100" size="5" maxlength="10"  runat="server"  /><br />
		最小百分比<input name="MinPercent" type="text" id="MinPercent" value="0" size="5" maxlength="10"  runat="server"  />
        最大百分比<input name="MaxPercent" type="text" id="MaxPercent" value="100" size="5" maxlength="10"  runat="server"  />
        <span id="PercentBox_Message" class="style1"></span>
    </div>

    <div id="MinMaxTickoffBox">
        最少列举多少项<input name="MinTickoff" type="text" id="MinTickoff" size="5" maxlength="2"  runat="server"  />
        <span id="MinMaxTickoffBox_Message" class="style1"></span>
    </div>
</fieldset>


<fieldset style="padding:5px; display:none" id="UploadOption">
    <legend>上传选项</legend>
    附件上限<input name="MaxFileLen" type="text" id="MaxFileLen" value="100" size="3" maxlength="3" />
    k<span id="UploadFile_M_MaxFileLen"></span><br />
    上传类型<br />
    <input type="radio" name="UploadMode" id="UploadMode0" value="0" />
    白名单模式，只允许上传下面指定的类型)<br />
    <input type="radio" name="UploadMode" id="UploadMode1" value="1" />
    黑名单模式，不允许上传下面指定的类型
    <br />
    文件类型,为空表示无限制,使用点作为分隔<br />
    <input name="FileType" type="text" id="FileType" value="" size="50" maxlength="150" />
    <br />
    <span style="color:#999999">例:选择白名单，填写.jpg.gif.png表示只允许上传“jpg”、“gif”、“png”类型的文件。选择黑名单，填写，.exe.dll表示不允许上传“exe”、“dll”类型的文件</span><br />
</fieldset>



    <span class="style1">*</span>题目放在
    <select id="PageNo" name="PageNo"></select>
    <table width="99%"  border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#EFF3FB"><input name="Save" id="Save" type="submit" class="SaveBT" value="保存" />
          <input type="button" name="Cancel" id="Cancel" value="取消" onclick="window.parent.cancelOption()" class="SaveBT" />
            <input name="SID" type="hidden" id="sid" value="" runat="server" />
            <span class="style1">*</span>号为必填
            <input name="IID" type="hidden" id="iid" value="" runat="server" />
            <input name="ItemType" type="hidden" id="ItemType" value="" runat="server"  />
            <input name="flag" type="hidden" id="flag" value="" runat="server"  />
            <input name="OldPageNo" type="hidden" id="OldPageNo" value="" runat="server"  />
            </td>
      </tr>
     
    </table></td>
  </tr>
</table>

</form>
 <iframe id="targetWin" name="targetWin" style="width:100%; display:none; height:300px"></iframe>
 	
</body>
</html>


