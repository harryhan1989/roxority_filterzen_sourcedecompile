<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_EditSurveyHtml, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <script language="javascript" type="text/javascript">
var sMessage;
<%=sClientJs%>
blnEditHTML=true;
    </script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/interface.js"></script>

    <script language="javascript" type="text/javascript" src="../SurveyWebEditor/WebEdior.js"></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/share.js"></script>

    <title>编辑问卷外观</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <link href="../css/EditSurveyHtml.css" rel="stylesheet" type="text/css" />
    <link href="../SurveyWebEditor/WebEditor.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Survey/js/EditSurveyHtml.js"></script>

</head>
<body onbeforeunload="event.returnValue='确定离开外观编辑页吗？';" scroll="no" style=" margin:0; padding:0; overflow:hidden">
    <div id="Box" style="padding:0; margin:0; overflow:hidden">
        <div id="ContentEditArea" style="float: left; width: 500px; padding:0; padding:0">
            <div id="EditorNode">
            </div>
        </div>
        <div class="ToolOptionAreaCSS" id="ToolOptionArea" onmouseover="setToolOptionArea(0)"
            onmouseout="setToolOptionArea(1)" onclick="switchToolOptionArea()" style="width: 7px;
            float: left; padding:0; padding:0">
            <span class="SwitchOpenDarkBT" id="SwitchBT"></span>
        </div>
        <div id="ExpandToolArea" style="vertical-align: top; background-color: #FFF; text-align: center;
            float: left; height:100%; padding:0; padding:0">
            <div id="FormArea" style="height: 25px; margin: 2px; padding: 2px; border: 1px solid #000;
                background: #FFC">
                <form style="" action="WriteSurveyFile.aspx" method="post" name="myform" target="targetWin"
                class="Myform" id="myform" onsubmit="return submitedit()">
                <input name="Save" type="button" class="SaveBT" id="Save" onclick='initFaceAfter()' value="完成编辑"
                    style="width: 80px">
                <input name="Cancel3" type="button" class="SaveBT" id="Cancel" onclick="cancel()"
                    style="width: 80px" value="返回" />
                <input name="SID" type="hidden" id="SID" value="<%=SID%>" />
                <textarea rows="0" cols="0" name="Memo" id="Memo" style="display: none"></textarea>
                <input name="Active" type="checkbox" id="Active" value="1" style="display: none" />
                </form>
            </div>
            <div id="PageList" style=" height:40px; position:relative; line-height: 20px; text-align: center">
                页列表</div>
            <div style="overflow: auto; height: 300px; text-align: center; width: 100%" id="PageStyleListWin">
                问卷样式</div>
        </div>
    </div>
    <div id="Layer1" class="ShadowEffect" style="z-index: 9; position: absolute">
        <div style="margin: 10px; vertical-align: middle; font-size: 12px">
            <span style="display: block">确认用这个模板？</span>
            <input type="button" value="OK，确认！" name="OK" class="OK" onclick="resetPageStyle()" />
            <input type="button" value="No，返回。" name="Cancel" class="Cancel" onclick="calcelEdit()" />
            <span style="display: none" id="SaveStatus">正在保存<img src="images/05043130.gif" alt="正在保存" /></span>
            <div class="PageImageBoxSelect">
                <img src="" id="DemoImg" alt="" /></div>
        </div>
    </div>
    <div id="BgWin" style="position: absolute; top: 0px; filter: alpha(opacity=30); opacity: 0.5;
        background-color: #000000; z-index: 0; left: 0px; display: none">
    </div>
    <div id="OpenWin" style="z-index: 10; position: absolute">
        <div style="background-color: #EFF3FB; margin: 5px; padding: 5px; font-weight: bold">
            &gt;&gt;&gt;最后一步</strong></div>
        <div style="margin: 5px; margin-top: 10px">
            <input name="Submit" type="button" class="SaveBT" value="生成问卷" style="width: 100px"
                id="B1" onclick="saveSurvey()" />
            <input name="OpenSurvey" type="checkbox" id="OpenSurvey" value="1" checked="checked">
            <label for="OpenSurvey">
                启用问卷</label><br />
            <label for="B1">
                确定问卷外观编辑完成，点击后将生成问卷，<strong>只有这样问卷才正式可以使用</strong>。</label>
        </div>
        <div style="margin: 5px; margin-bottom: 10px">
            <input name="Submit2" type="button" class="SaveBT" value="编辑问卷外观" style="width: 100px"
                onclick="openEdit()" />
            <br />
            点击后进行问卷外观编辑。
        </div>
        <div style="margin: 5px; margin-bottom: 10px">
            <input name="BackUpBt" type="button" class="SaveBT" id="BackUpBt" onclick="cancel()"
                value="返 回" />
        </div>
        <div style="margin: 5px;">
            <span style="font-weight: bold; color: #FF0000">警告</span>:<br />
            1、不要删除控件，否则将造成问卷错误<br />
            2、在分页问卷中，不要改变题目的页位置，否则将造成问卷错误
        </div>
    </div>
    <div id="ResultWin" style="z-index: 30; position: absolute">
        <div style="background-color: #EFF3FB; width: 98%; padding: 5px; font-weight: bold;
            text-align: center;">
            生成完成！</div>
        <div style="background-color: #FFFFFF; width: 98%; padding: 5px; text-align: center">
            <img src="images/OK.gif" alt="ok" width="50" height="50" vspace="2" /><br />
            <strong>问卷已经生成，问卷通过问卷地址调用。</strong>
        </div>
        <div>
            问卷地址：<a href='<%=sPath%>' target="_blank"><img src="images/ie.gif" width="18" height="18"
                border="0" /><%=sPath%></a>
        </div>
        <div>
            <strong>提示</strong>：<br />
            1、问卷默认状态为禁用，请在我的问卷中将问卷启用<br />
            2、在“我的问卷”中，点击“调用”按钮可以获取更多问卷调用方式
            <br />
            3、问卷外观在问卷生成后仍可重复编辑
        </div>
        <div style="text-align: center">
            <input name="ExitEditBT" type="button" class="SaveBT" id="ExitEditBT" style="width: 100px;
                height: 25px" onclick="top.location.href='../Platform/MainWeb/root.aspx?DefaultMain=../../Survey/SurveyManage.aspx  '"
                value="退出编辑" />
        </div>
    </div>
    <iframe src="" style="width: 100%; height: 330px; display: none" id="targetWin" name="targetWin">
    </iframe>
</body>
</html>
