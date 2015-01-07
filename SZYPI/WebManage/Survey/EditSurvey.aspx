<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_Default3, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑问卷</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <link href="../css/EditSurvey.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Js/share.js"></script>

    <script language="javascript" type="text/javascript" charset="gbk" src="Js/x_open.js"></script>

    <script language="javascript" type="text/javascript" src="Js/Interface.js"></script>

    <script language="javascript" type="text/javascript" src="Js/EditSurvey.js"></script>

</head>

<script language="javascript" type="text/javascript">
    <%=sClientJs%>
    function refresh()
    {
       $("OpenWin").style.display='none';
       $("BgWin").style.display='none';   
    }
 
</script>

<body class="BlackFont" style="background-color: #EFF3FB; overflow: hidden;" onbeforeunload="event.returnValue='确定离开题目编辑吗？';refresh();">
    <form id="form1" runat="server">
    <div id="Layer3">
    </div>
    <div id="Layer1" class="ShadowEffect">
        <span onclick="addPage(-1)" class="PageOptionMenuNormal" id="PageOptionBT1" onmousemove="this.className='PageOptionMenuMove'"
            onmouseout="this.className='PageOptionMenuNormal'"><span id="Bar1" class="MenuBarWord">
                增加分页</span></span> <span onclick="volumeAddPage()" class="PageOptionMenuNormal" id="PageOptionBT2"
                    onmousemove="this.className='PageOptionMenuMove'" onmouseout="this.className='PageOptionMenuNormal'">
                    <span id="Bar2" class="MenuBarWord">批量加页</span></span> <span onclick="sortOutPage()"
                        class="PageOptionMenuNormal" id="PageOptionBT3" onmousemove="this.className='PageOptionMenuMove'"
                        onmouseout="this.className='PageOptionMenuNormal'"><span id="Bar3" class="MenuBarWord">
                            整理分页</span></span>
    </div>
    <input type="hidden" name="DoAction" id="DoAction" value="False" />
    <div class="TableHead" id="MenuBar">
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="optionActionWin('CreateNewSurvey.aspx?date1='+new Date().getTime(),'新建问卷',200,500)">
            <span class='Menu_Bt_00'></span>新建问卷</div>
        <%--<div class="EditSurveyToolsBarBT_Normal" onmousemove="this.className='EditSurveyToolsBarBT_Move'" onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="initOpenSurveyListWin()"><span class='Menu_Bt_01'></span>打开问卷</div>--%>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="editItem1(SID,0)">
            <span class='Menu_Bt_02'></span><span style="font-weight: bold; color: #C00; float: left;
                margin-left: -5px">加入题目</span></div>
        <div id="PageOptionBT" class="EditSurveyToolsBarBT_Normal" onmousemove="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="PageExpandOption()">
            分页操作</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="setPar(SID)">
            <span class='Menu_Bt_04'></span>选项</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="PointStat()">
            <span class='Menu_Bt_05'></span>评分表</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="setSurveyStyle()">
            <span class='Menu_Bt_06'></span>定义样式</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="applyTemp(SID)">
            <span class='Menu_Bt_07'></span>应用模板</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="setRightMenu()">
            <span class='Menu_Bt_08'></span>侧边栏</div>
        <div class="EditSurveyToolsBarBT_Normal" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="top.location.href='../Platform/MainWeb/root.aspx?DefaultMain=../../Survey/SurveyManage.aspx'+SurveyListPageNo">
            <span class='Menu_Bt_09'></span>退出编辑</div>
        <div class="EditSurveyToolsBarBT_Normal" onclick="ComplateSurveyEdit()" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'">
            <span class='Menu_Bt_10'></span><span style="color: #060; font-weight: bold;">完成编辑</span></div>
        <div class="EditSurveyToolsBarBT_Normal" onclick="ps()" onmouseover="this.className='EditSurveyToolsBarBT_Move'"
            onmouseout="this.className='EditSurveyToolsBarBT_Normal'" style="width: 40px"
            title="预览问卷">
            <span class='Menu_Bt_11'></span>
        </div>
        <%--    <div class="EditSurveyToolsBarBT_Normal" onmousemove="this.className='EditSurveyToolsBarBT_Move'" onmouseout="this.className='EditSurveyToolsBarBT_Normal'" onclick="showHelp()"  style="width:40px" title="帮助"><span class='Menu_Bt_12'></span></div>--%>
    </div>
    <div style="width: 75%; vertical-align: top; float: left" align="center" id="LeftWin">
        <iframe style="width: 100%; overflow-y: auto;" src="" marginheight="0" frameborder="0"
            marginwidth="0" vspace="0" hspace="0" id="targetWin" name="targetWin"></iframe>
    </div>
    <div style="width: 25%; vertical-align: top; background-color: #EFF3FB; font-size: 12px;
        float: left" id="RightMenu">
        <div id="RightMenuContent">
            <div class="TableHead" id="TableHead" style="line-height: 25px; height: 25px">
                <div id="TitleName" style="font-weight: bolder; padding-left: 5px; float: left">
                </div>
                <%-- <div   class="Menu_Bt_16" onclick="surveyInfo(0)" id="Info" title="问卷概况"></div>--%>
                <div class="Menu_Bt_15" onclick="showAll(0)" id="Page" title="折叠所有页">
                </div>
                <div class="Menu_Bt_15" onclick="showAll(1)" id="Item" title="折叠所有题目">
                </div>
                <div class="Menu_Bt_14" onclick="addWidth(0)" id="Add" title="加宽">
                </div>
                <div onclick="addWidth(1)" id="Sub" title="减宽" class="Menu_Bt_13">
                </div>
            </div>
            <div>
                <iframe src="" id="SetWin" name="SetWin" style="width: 100%; height: 100%; overflow: hidden"
                    frameborder="0" marginheight="0" marginwidth="0" onload="Javascript:SetCwinHeight(this)">
                </iframe>
            </div>
        </div>
        <iframe src="" id="ActionPage" style="width: 100%; height: 300px; display: none">
        </iframe>
    </div>
    <div id="OpenWin" style="position: absolute; z-index: 4; width: 540px; height: 150px;
        background-color: #fff; display: block; text-align: center; vertical-align: middle;
        left: 242px; top: 307px; line-height: " class="ShadowEffect">
        <div class="BlackFont">
            <br />
            <br />
            <br />
            <img src="images/loading.gif" alt="" /><br />
            载入中</div>
    </div>
    <div id="ActionWin" style="position: absolute; z-index: 3; width: 600px; height: 400px;
        background-color: #FFFFFF; display: none; left: 5px; top: 163px;" class="ShadowEffect">
        <div id="TopBar" style="height: 25px; width: 100%; cursor: pointer" class="TableHead"
            title="关闭窗口" onclick="closeActionWin()">
            <span id="ActionWinName" style="font-weight: bold; float: left; width: 90%; line-height: 25px;
                margin-left: 5px"></span><span style="float: right; width: 4%; padding-top: 4px">
                    <img src="images/close1.gif" alt="关闭窗口" width="15" height="15" /></span>
        </div>
        <iframe id="ActionTarget" src="" name="ActionTarget" style="height: 375px; width: 600px;
            margin: 0; padding: 0; border: 0; display:none" frameborder="0" marginheight="0" marginwidth="0"
            scrolling="no"></iframe>
    </div>
    <div id="BgWin" style="position: absolute; top: 0px; background-color: #000000; z-index: 2;
        left: 0px; display: none" class="BgWin">
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    setPar(SID);
    document.getElementById("SetWin").style.height = document.documentElement.clientHeight - document.getElementById("MenuBar").clientHeight - document.getElementById("TableHead").offsetHeight - 5 + "px";
</script>

