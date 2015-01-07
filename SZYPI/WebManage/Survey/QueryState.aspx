<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_QueryState,Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询分析</title>

    <script language="javascript" type="text/javascript">
        <%=sClientJs %>
    </script>

    <script language="javascript" type="text/javascript" src="js/httable.js"></script>

    <script language="javascript" type="text/javascript" src="js/share.js"></script>

    <script language="javascript" type="text/javascript" src="js/interface.js"></script>

    <script language="javascript" type="text/javascript" src="js/QueryState.js"></script>
    
    <script language="javascript" type="text/javascript" charset="gbk" src="Js/x_open.js"></script>

    <script type="text/javascript">
        function anisisclick() {
            //           x_open('结果分析', 'ConditionItemAnalysis.aspx', 800, 320);
//            window.showModalDialog("ConditionItemAnalysis.aspx", "", "dialogWidth=200px;dialogHeight=100px;dialogLeft=0px;dialogTop=0px", "action");
        }
        
    </script>

    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: 12px; overflow: hidden">
    <div id="MaskLayer" style="position: absolute; display: none">
    </div>
    <div id="SubWin" style="width: 500px; height: 280px; display: none; position: absolute; 
        z-index: 200; background-color: #FFFFFF; border: #333333 1px solid" class="ShadowEffect">
        <div id="TopBar" style="height: 25px; width: 100%; cursor: pointer" class="TableHead"
            title="关闭窗口" onclick="myface._closePop($('SubWin'))">
            <span id="ActionWinName" style="font-weight: bold; float: left; width: 50%; line-height: 25px;
                margin-left: 5px">设置条件</span> <span style="float: right; margin: 5px;" class="Share_Close">
                </span>
        </div>
        <div style=" height:250px;overflow-y:auto;overflow-x:hidden;">
        <div style="margin-bottom: 5px; margin-top: 5px">
            &nbsp;当<select id="ItemList" name="ItemList" onchange="setCon(this)" style="width: 450px"></select></div>
        <div id="ConOptionArea" style="display: none; width: 470px;">
            <fieldset style="margin: 5px; padding: 5px;">
                <legend>设置条件</legend>
                <div id="ItemType1">
                    <label>
                        <input name="ItemType1_Con" type="radio" id="ItemType1_Con_0" value="0" checked="checked" />等于</label><br />
                    <label>
                        <input type="radio" value="1" id="ItemType1_Con_1" name="ItemType1_Con" />不等于</label><br />
                    <label>
                        <input type="radio" value="2" id="ItemType1_Con_2" name="ItemType1_Con" />包含</label><br />
                    <label>
                        <input type="radio" value="3" id="ItemType1_Con_3" name="ItemType1_Con" />不包含</label>
                </div>
                <div id="ItemType2">
                    <label>
                        <input name="ItemType2_Con" type="radio" id="ItemType2_Con_0" value="0" checked="checked" />等于</label><br />
                    <label>
                        <input type="radio" value="1" id="ItemType2_Con_1" name="ItemType2_Con" />不等于</label><br />
                    <label>
                        <input type="radio" value="2" id="ItemType2_Con_2" name="ItemType2_Con" />大于</label><br />
                    <label>
                        <input type="radio" value="3" id="ItemType2_Con_3" name="ItemType2_Con" />大于等于</label><br />
                    <label>
                        <input type="radio" value="4" id="ItemType2_Con_4" name="ItemType2_Con" />小于</label><br />
                    <label>
                        <input type="radio" value="5" id="ItemType2_Con_5" name="ItemType2_Con" />小于等于</label>
                </div>
                <div id="Input_ConBox">
                    值<input type="text" id="Input_Con" value="" maxlength="50" style="width: 400px" /></div>
                <div id="ItemType4">
                    <div id="ItemType44">
                        <label>
                            <input name="ItemType4_Con" type="radio" id="ItemType4_0" value="0" checked="checked" />等于以下选项</label>
                        <label>
                            <input type="radio" value="0" id="ItemType4_1" name="ItemType4_Con" />不等于以下选项</label>
                    </div>
                    <div id="ItemType48">
                        <label>
                            <input type="radio" value="0" id="ItemType8_0" name="ItemType8_Con" checked="checked" />包含以下任一所选选项</label><br />
                        <label>
                            <input type="radio" value="3" id="ItemType8_3" name="ItemType8_Con" />包含以下所选选项</label><br />
                        <label>
                            <input type="radio" value="1" id="ItemType8_1" name="ItemType8_Con" />完全等于以下所选选项,即不多不少</label><br />
                        <label>
                            <input type="radio" value="2" id="ItemType8_2" name="ItemType8_Con" />不包含以下所选选项的组合，即不同时出现所选选项</label><br />
                    </div>
                    <div>
                        <select name="OptionList" size="5" multiple="multiple" id="OptionList" style="width: 400px">
                        </select><br />
                        *注:拖住连选，按住Ctrl加选</div>
                    <div id="Raletion" style="display: none">
                        多个选项间的关系
                        <label>
                            <input name="ItemType4_relation" type="radio" id="ItemType4_relation_0" value="0" />并且</label>
                        <label>
                            <input type="radio" value="1" id="ItemType4_relation_1" name="ItemType4_relation"
                                checked="checked" />或者</label>
                    </div>
                </div>
            </fieldset>
        </div>
        <div style="padding: 5px">
            <input type="button" value="确定" style="width: 80px; height: 25px" onclick="saveItemConditionSet()" />
            <input type="button" value="取消" style="width: 80px; height: 25px" onclick="cancelCondition();myface._closePop($('SubWin'))" />
            <input type="button" value="关闭" style="width: 80px; height: 25px" onclick="myface._closePop($('SubWin'))" />

        </div>
        </div>
    </div>
    <div id="ToolBar" style="padding: 5px; border: #666 1px solid; height: 25px; background-color: #FFE;
        margin: 5px">
        <div id="AddConBox" style="float: left">
            <input type="button" id="AddCon" value="增加条件" onclick="myface._openMask();myface._switchTarget($('SubWin'),'')" />
        </div>
        <div id="FormBox" style="float: right">
            <form action="ConditionItemAnalysis.aspx" style="margin:0px;" method="post" id="form1" onsubmit="return submitStat(0);" target="_blank" >
            <select id="ResultItem" name="ResultItem" onchange="fillChartList_()">
                <option value="0">选择分析对象</option>
            </select>
            <select id="Chart" name="Chart">
                <option value="">选择图表</option>
            </select>
            <input type="submit" name="SubStat" id="SubStat" value="提交分析" onclick="return anisisclick();" />
            <input type="hidden" name="QueryCondition" id="QueryCondition" />
            <input type="hidden" name="SID" id="SID" />
            <input type="hidden" name="ResultType" id="ResultType" />
            <input type="hidden" name="ConDes" id="ConDes" />
            </form>
        </div>
    </div>
    <div id="ConContentBox" style="clear: both; padding: 5px">
    </div>
    <div id="ConShowArea" style="clear: both">
    </div>
    <div id="ResultStat" style="display: none; background-color: #FFFFFF; border: 1px #666666 solid;
        position: absolute; z-index: 300; left: 0px; top: 0px" class="ShadowEffect">
        <div id="SubBar" style="height: 25px; width: 100%; cursor: pointer" class="TableHead"
            title="关闭窗口" onclick="$('ResultStat').style.display='none'">
            <span id="" style="font-weight: bold; float: left; width: 50%; line-height: 25px;
                margin-left: 5px">结果分析</span> <span style="float: right; margin: 5px;" class="Share_Close">
                </span>
        </div>
        <iframe id="TargetWin" name="TargetWin" style="width: 100%" frameborder="0"></iframe>
    </div>
    <div id="BgWin" style="position: absolute; top: 0px; background-color: #000000; z-index: 2;
        left: 0px; display: none" class="BgWin">
    </div>
</body>
</html>
