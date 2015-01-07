<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_SetStyle, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
 <link rel="STYLESHEET" type="text/css" href="../survey/colorselect/codebase/dhtmlxcolorpicker.css" />
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
 <style type="text/css">

fieldset{
	margin-bottom:5px;
	padding:2px;
}
legend{
	font-weight:bold;
}

form{
	margin:0px;
}



body{
}

.PageHead{
}

.SurveyName{
}

.PageFoot{
}

.MatrixInputCSS{
}

.ExpandContentStyle{
}

.PageBox{
}

.PageContent{
}

.ItemBox{
}

.ItemBar{
}

.ItemName{
}

.SubItemName{
	
}

.ItemContent{
	
}

.OptionName{
}

.InputCSS{
}

.OtherInputCSS{
}

.CheckBoxCSS{
}

.RadioCSS{
}

.ListMulitCSS{
}

.TextAreaCSS{
}

.SelectCSS{
}

.MatrixSelectCSS{
}

.PercentInputCSS{
}

.NumInputCSS{
}

.MatrixInputCSS{
}

.NextPage{
	
}

.BeforePage{

}

.SubmitBT{
	
}

.CheckCode{
	
}

.OneTimePSW{
	
}
.ItemPitch{
	
}


.NormalBT{
	height:25px;
	background-color:#EFF0FF;
	border:1px solid #EFF0FF;
	border-bottom:0px;
	font-weight:normal;
	width:100px;
	float:left;
			cursor:pointer;
			line-height:25px;
}


.ActiveBT{
	width:100px;
	height:25px;
	background-color:#FFC;
	border:1px solid #CCC;
	border-bottom:0px;
	font-weight:bold;
		float:left;
		cursor:pointer;
		line-height:25px;
}
</style>
<title>设置全局样式</title>
</head>



<script  src="colorselect/codebase/dhtmlxcommon.js" type="text/javascript"></script>
<script  src="colorselect/codebase/dhtmlxcolorpicker.js"  type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="js/share.js"></script>
<script language="javascript" type="text/javascript" src="Js/httable.js"></script>
<script language="javascript" type="text/javascript" src="Js/styleoption.js"></script>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
<script language="javascript" type="text/javascript" src="js/interface.js"></script>
<script language="javascript" type="text/javascript" ><%=sClientJs %></script>
<body style="margin:0px"  class="RightOptionWin">
<div id="CPcont" style="display:none; position:absolute"></div>
<div style="width:580px; height:280px;float:left; padding:5px">
	<iframe src="SurveyDemo.htm" style="width:100%; "   id="DemoArea" name="DemoArea"></iframe>
</div>

<div style="width:400px; float:left; margin:2px;">
	<div style="width:100%;text-align:center; margin:3px">
        <div class="ActiveBT" onclick="switchBT(this)" id="BT0">自定义样式</div>
        <%--<div class="NormalBT" onclick="switchBT(this)" id="BT1">样式库</div>--%>
	</div>
	
    
    <div style="float:left; width:100%; margin:3px; margin-top:0px; padding:3px; background-color:#EEE; border:1px solid #ccc" id="DefineCSS">
        <select id="CSSObj" onchange="fillStyleToForm($(this[this.selectedIndex].value+'_').value)">
        <option value="">------选择一个对象定义其样式CSS------</option>
        <optgroup title="整个网页" label="整个网页外观">
        <option value="body">Body(主体)</option>
        <option value="PageHead">页眉</option>
        <option value="SurveyName">问卷名</option>
        <option value="PageFoot">页脚</option>
        <option value="ItemPitch">题目间隔</option>
        <option value="ExpandContentStyle">引入的答案的显示</option>
        </optgroup>
        
        <optgroup label="问卷分页外观">
        <option value="PageBox">页容器</option>
        <option value="PageContent">页内文本</option>
        </optgroup>
        
        <optgroup label="题目外观">
            <option value="ItemBox">题目容器</option>
            <option value="ItemBar">题目名栏</option>
            <option value="ItemName">题目名</option>
            <option value="SubItemName">子题目名</option>
            <option value="ItemContent">题目说明</option>
            <option value="OptionName">选项文字</option>
        </optgroup>
        
        <optgroup label="控件外观">
        <option value="InputCSS">单行文本输入框</option>
        <option value="OtherInputCSS">其它输入框</option>
        <option value="CheckBoxCSS">多选选择按钮</option>
        <option value="RadioCSS">单选选择按钮</option>
        <option value="ListMulitCSS">多选列表</option>
        <option value="TextAreaCSS">多行文本输入框</option>
        <option value="SelectCSS">下拉列表框</option>
        <option value="MatrixSelectCSS">矩阵下拉列表中的下拉框</option>
        <option value="PercentInputCSS">比例题中的输入框</option>
        <option value="NumInputCSS">数值输入题中的输入框</option>
        <option value="MatrixInputCSS">矩阵输入题中的输入框</option>
        <option value="NextPage">下一页按钮</option>
        <option value="SubmitBT">提交按钮</option>
        <option value="BeforePage">上一页按钮</option>        
        <option value="CheckCode">验证码输入框</option>
        <option value="OneTimePSW">一次性密码输入框</option>
        </optgroup>
        </select>
        <input type="button" value="预览并生成" onclick="createStyle()" />
        
        <div id="Message" style="border:1px #F00  solid; margin:2px; color:#F30; background-color:#FFC; height:80px; overflow:auto; display:none"></div>
        
        <form action="" id="form1">
            <fieldset>
            <legend>常规</legend>
            <div>
            文字颜色<input type="text"  id="FrontColor" maxlength="7" size="7"  onclick="openColorSelectWin(this,this)" onchange="this.style.backgroundColor=this.value"  />
            背景色<input type="text"  id="BackGroundColor" maxlength="7" size="7"   onclick="openColorSelectWin(this,this)"  onchange="this.style.backgroundColor=this.value"   />
            </div>
    
            <div>
            字体<select name="FontFamily" id="FontFamily" style="width:60px " >
            <option value="">默认</option>
            <option value="宋体">宋体</option>
            <option value="黑体">黑体</option>
            <option value="隶书">隶书</option>
            <option value="楷体_GB2312">楷体_GB2312</option>
            <option value="Arial, Helvetica, sans-serif">Arial,Helvetica,sans-serif</option>
            </select>
            大小<input type="text" size="3" maxlength="2" id="FontSize" />px
            粗细<select name="FontWeight" id="FontWeight" >
            <option value="">默认</option>
            <option value="normal">正常</option>
            <option value="bold">粗体</option>
            <option value="bolder">特粗</option>
            <option value="lighter">细体</option>
            <option value="100">100磅</option>
            <option value="200">200磅</option>
            <option value="300">300磅</option>
            <option value="400">400磅</option>
            <option value="500">500磅</option>
            <option value="600">600磅</option>
            <option value="700">700磅</option>
            <option value="800">800磅</option>
            <option value="900">900磅</option>
            </select>
            样式<select name="FontStyle" id="FontStyle">
            <option value="">默认</option>
            <option value="normal">正常</option>
            <option value="italic">斜体</option>
            <option value="oblique">偏斜体</option>
            </select>
        </div>
        <div>
            对齐<select name="TextAlign" id="TextAlign" >
            <option value="">默认</option>
            <option value="left">左对齐</option>
            <option value="right">右对齐</option>
            <option value="center">居中</option>
            <option value="justify">平均</option>
            </select>
            行高<input type="text"  id="LineHeight" maxlength="3" size="3"  />px
            <label>上划线<input name="decoration" type="checkbox" id="underline" value="underline" onclick="ExcludeOption($('underline'),$('overline'),$('line-through'))" /></label>
            <label>下划线<input name="decoration" type="checkbox" id="overline" value="overline"  onclick="ExcludeOption($('overline'),$('underline'),$('line-through'))"  /></label>
            <label>删除线<input name="decoration" type="checkbox" id="line-through" value="line-through"   onclick="ExcludeOption($('line-through'),$('overline'),$('underline'))" /></label>
        </div>           
                    
        背景图<input type="text" id="BackGroundImage"  size="35"    /><select id="BackGroundImageRepeat"><option>默认</option>
        <option value="repeat">重复</option>
        <option value="no-repeat">不重复</option>
        <option value="repeat-x">横向重复</option>
        <option value="repeat-y">纵向重复</option>
        </select>
        </fieldset>
    
    <fieldset>
    <legend>显示尺寸</legend>
    宽度<input type="text"  id="Width" maxlength="7" size="4" />
    <span id="UnitType">px</span> 
    <select id="WidthUnit"  onchange="$('UnitType').innerHTML=this[this.selectedIndex].value">
      <option value="px" selected="selected">值</option>
      <option value="%">%</option>
    </select>
     高度<input type="text"  id="Height" maxlength="4" size="4" />px
    </fieldset>
    
    <fieldset>
    <legend>边框</legend>
    
    <div>边&nbsp;&nbsp;&nbsp;&nbsp;线
      <select name="Border" id="Border">
             <option value="">默认</option>
          <option value="none">无</option>
          <option value="dotted">点划线</option>
          <option value="dashed">虚线</option>
          <option value="solid">实线</option>
          <option value="double">双线</option>
          <option value="groove">槽状</option>
          <option value="ridge">脊状</option>
          <option value="inset">凹陷</option>
          <option value="outse ">凸出</option>
          </select>
          粗细<input type="text" id="BorderWidth" size="5" maxlength="2" />px 
          颜色<input type="text" id="BorderColor" size="7" maxlength="7"   onclick="openColorSelectWin(this,this)"   />
    </div>
    
    
    <div>上边线
      <select name="BorderTop" id="BorderTop">
       <option value="">默认</option>
          <option value="none">无</option>
          <option value="dotted">点划线</option>
          <option value="dashed">虚线</option>
          <option value="solid">实线</option>
          <option value="double">双线</option>
          <option value="groove">槽状</option>
          <option value="ridge">脊状</option>
          <option value="inset">凹陷</option>
          <option value="outse ">凸出</option>
            </select>
          粗细<input type="text" id="BorderTopWidth" size="5" maxlength="2" />px 
          颜色<input type="text" id="BorderTopColor" size="7" maxlength="7"   onclick="openColorSelectWin(this,this)"   />
    </div>
    
    <div>下边线
      <select name="BorderBottom" id="BorderBottom">
       <option value="">默认</option>
          <option value="none">无</option>
          <option value="dotted">点划线</option>
          <option value="dashed">虚线</option>
          <option value="solid">实线</option>
          <option value="double">双线</option>
          <option value="groove">槽状</option>
          <option value="ridge">脊状</option>
          <option value="inset">凹陷</option>
          <option value="outse ">凸出</option>
            </select>
          粗细<input type="text" id="BorderBottomWidth" size="5" maxlength="2" />px 
          颜色<input type="text" id="BorderBottomColor" size="7" maxlength="7"   onclick="openColorSelectWin(this,this)"   />
    </div>
    
    <div>左边线
      <select name="BorderLeft" id="BorderLeft">
       <option value="">默认</option>
          <option value="none">无</option>
          <option value="dotted">点划线</option>
          <option value="dashed">虚线</option>
          <option value="solid">实线</option>
          <option value="double">双线</option>
          <option value="groove">槽状</option>
          <option value="ridge">脊状</option>
          <option value="inset">凹陷</option>
          <option value="outse ">凸出</option>
            </select>
          粗细<input type="text" id="BorderLeftWidth" size="5" maxlength="2" />px 
          颜色<input type="text" id="BorderLeftColor" size="7" maxlength="7"   onclick="openColorSelectWin(this,this)"   />
    </div>
    
    <div>右边线
      <select name="BorderRight" id="BorderRight">
       <option value="">默认</option>
          <option value="none">无</option>
          <option value="dotted">点划线</option>
          <option value="dashed">虚线</option>
          <option value="solid">实线</option>
          <option value="double">双线</option>
          <option value="groove">槽状</option>
          <option value="ridge">脊状</option>
          <option value="inset">凹陷</option>
          <option value="outse ">凸出</option>
            </select>
          粗细<input type="text" id="BorderRightWidth" size="5" maxlength="2" />px 
          颜色<input type="text" id="BorderRightColor" size="7" maxlength="7"   onclick="openColorSelectWin(this,this)"   />
    </div>
    </fieldset>
    
    <fieldset>
        <legend>边距</legend>
        <div>
        外边距<input type="text" id="Margin" size="2" maxlength="3" style="width:20px" />px
        上边距<input type="text" id="MarginTop" size="2" maxlength="3" style="width:20px"  />px
        下边距<input type="text" id="MarginBottom" size="2" maxlength="3" style="width:20px"  />px
        左边距<input type="text" id="MarginLeft" size="2" maxlength="3" style="width:20px"  />px
        右边距<input type="text" id="MarginRight" size="2" maxlength="3" style="width:20px"  />px
        </div>
        <div>
        内边距<input type="text" id="Padding" size="2" maxlength="3" style="width:20px"  />px
        上边距<input type="text" id="PaddingTop" size="2" maxlength="3" style="width:20px"  />px
        下边距<input type="text" id="PaddingBottom" size="2" maxlength="3" style="width:20px"  />px
        左边距<input type="text" id="PaddingLeft" size="2" maxlength="3" style="width:20px"  />px
        右边距<input type="text" id="PaddingRight" size="2" maxlength="3" style="width:20px"  />px
        </div>
    </fieldset>
    
    <fieldset>
        <legend>其它</legend>
        浮动<select id="Float">
         <option value="">默认</option>
        <option value="none">无</option>
        <option value="left">左</option>
        <option value="right">右</option>
        </select>
        清除浮动<select id="ClearFloat">
        <option value="">默认</option>
        <option value="none">无</option>
        <option value="both">两者</option>
        <option value="left">左</option>
        <option value="right">右</option>
        </select>
        溢出<select id="OverFlow">
        <option value="">默认</option>
        <option value="visible">可见</option>
        <option value="scroll">滚动</option>
        <option value="hidden">隐藏</option>
        <option value="auto">自动</option>
        </select>
    </fieldset>
    
    </form>
    
    <form action="savestyle.aspx" id="form2" onsubmit="return checkForm()" target="TargetWin" method="post">
    <input type="hidden" id="body_" runat="server"  />
    <input type="hidden" id="PageHead_" runat="server"  />
    <input type="hidden" id="SurveyName_" runat="server"  />
    <input type="hidden" id="PageFoot_" runat="server"  />
    <input type="hidden" id="ItemPitch_" runat="server"  />
    <input type="hidden" id="ExpandContentStyle_" runat="server"  />
    
    <input type="hidden" id="PageBox_" runat="server"  />
    <input type="hidden" id="PageContent_" runat="server"  />
    
    <input type="hidden" id="ItemBox_" runat="server"  />
    <input type="hidden" id="ItemBar_" runat="server"  />
    <input type="hidden" id="ItemName_" runat="server"  />
    <input type="hidden" id="SubItemName_" />
    <input type="hidden" id="ItemContent_"  runat="server" />
    <input type="hidden" id="OptionName_" runat="server"  />
    
    <input type="hidden" id="InputCSS_" runat="server"  />
    <input type="hidden" id="OtherInputCSS_" runat="server"  />
    <input type="hidden" id="CheckBoxCSS_" runat="server"  />
    <input type="hidden" id="RadioCSS_" runat="server"  />
    <input type="hidden" id="ListMulitCSS_" runat="server"  />
    <input type="hidden" id="TextAreaCSS_" runat="server"  />
    <input type="hidden" id="SelectCSS_" runat="server"  />
    <input type="hidden" id="MatrixSelectCSS_" runat="server"  />
    <input type="hidden" id="PercentInputCSS_" runat="server"  />
    <input type="hidden" id="NumInputCSS_" runat="server"  />
    <input type="hidden" id="MatrixInputCSS_" runat="server"  />
    <input type="hidden" id="NextPage_" runat="server"  />
    <input type="hidden" id="SubmitBT_" runat="server"  />
    <input type="hidden" id="BeforePage_" runat="server"  />
    <input type="hidden" id="CheckCode_" runat="server"  />
    <input type="hidden" id="OneTimePSW_" runat="server"  />
    <input type="hidden" id="Result" name="Result"  runat="server"/>
    <input type="hidden" id="sid" name="sid" runat="server" />
        <div style="border:1px #666 solid; padding:2px; margin:2px">
            <input type="submit" value="保存到问卷" style="width:100px" /><span style="color:#666; margin-left:10px">在逐步生成样式后，保存样式到问卷中</span>
            <input type="button" value="从问卷中清除" onclick="clearStyle()"  style="width:100px"  />
            <div ><label><input type="checkbox" id="SaveToLib" name="SaveToLib" onclick="saveToLib()" />同时保存到样式库中</label>
            <input type="text" id="StyleLibName" value="样式名" name="StyleLibName" disabled="disabled"/>
            </div>
        </div>
     
	</form>
</div>

	<div id="StyleLib" style="display:none; width:100%">
    	<iframe   id="StyleLibWin" style="width:100%; border:1px solid #ccc" frameborder="0"></iframe>
     </div>

</div>
<iframe src="" style="height:150px;width:100%; display:none" id="TargetWin" name="TargetWin"></iframe>

</body>
</html>

