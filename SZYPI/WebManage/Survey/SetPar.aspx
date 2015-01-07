<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_SetPar, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>参数设置</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="js/interface.js"></script>

    <script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>

    <script language="javascript" type="text/javascript" src="js/dateobj.js"></script>

    <script language="javascript" type="text/javascript" src="js/share.js"></script>

    <script language="javascript" type="text/javascript"><%=sClientJs %></script>

    <script language="javascript" type="text/javascript" src="js/SetPar.js"></script>

    <script type="text/javascript">
        function Cancel() {
            try {
                window.parent.cancelOption();
            }
            catch (e) {
                location.href = "SurveyManage.aspx";
            }
        }
    </script>

    <style>
        *
        {
            overflow-x: hidden;
            overflow-y: hidden;
        }
    </style>
</head>
<body onload="setWin(45,55);init()" class="RightOptionWin" style="margin: 5px; overflow-x: hidden;
    overflow-y: auto">
    <form name="form1" id="form1" method="post" action="SaveSetPar.aspx" target="targetWin"
    onsubmit="return checkForm()" style="margin: 0px">
    <div style="overflow: auto; overflow-x: hidden;width="97%"" id="ParTable">
        <table width="97%" border="0" cellpadding="0" cellspacing="0">
            <tr style="display: none">
                <td align="right" width="200">
                    <div align="left">
                        问卷语言</div>
                </td>
                <td>
                    <select name="Lan" id="Lan">
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" width="200">
                    <div align="left">
                        问卷积分</div>
                </td>
                <td>
                    <input name="SPoint" type="text" id="SPoint" value="0" size="10" maxlength="4" />
                    <br />
                    为正值答卷都将获取所设积分，<br />
                    负值时表示答卷需要消费积分(需要设置为会员问卷)
                </td>
            </tr>
            <tr>
                <td align="right" width="200">
                    <div align="left">
                        勾选答卷审批</div>
                </td>
                <td>
                    <label>
                        <input name="NeedConfirm" type="checkbox" id="NeedConfirm" value="1" />需审核后才能得分</label>
                    （答卷需要审核后有效）<br />
            </tr>
            <tr>
                <td align="right" width="200">
                    <div align="left">
                        分析结果的对外公开</div>
                </td>
                <td>
                    <label>
                        <input name="ResultPublish" type="radio" id="ResultPublish1" value="1" />不公开</label>
                    <label>
                        <input name="ResultPublish" type="radio" id="ResultPublish2" value="2" />公开</label>
                    <label>
                        <input name="ResultPublish" type="radio" id="ResultPublish3" value="3" />组内公开</label>
                    <%--  （答卷需要审核后有效）<br />--%>
            </tr>
            <tr>
                <td>
                    结束日期
                </td>
                <td>
                    <input name="EndDate" type="text" id="EndDate" onclick="SelectDate(this,'yyyy-MM-dd',1,1)"
                        readonly="readonly" size="12" title="选择日期，日期为空表示无限制" />
                    <img src="images/Date.gif" alt="选择日期" name="selectdatebt" width="16" height="16"
                        hspace="5" id="selectDatebt" style="cursor: pointer" onclick="SelectDate($('EndDate'),'yyyy-MM-dd',1,1)" />
                </td>
            </tr>
            <tr style="display: none">
                <td align="right" width="200" valign="top">
                    <div align="left">
                        考试功能</div>
                </td>
                <td>
                    <label>
                        <input name="TPaper" type="checkbox" id="TPaper" value="1" onclick="setTestPaper(this)" />配置为试卷</label><span
                            id="TPaper_M"></span>
                    <div style="margin-bottom: 5px; display: none" id="TestPaperBox">
                        <fieldset style="width: 300px">
                            <legend>答卷对照表</legend>
                            <label>
                                <input name="TToAll" type="radio" id="TToAll0" value="0" />关闭答卷对照表</label><br />
                            <label>
                                <input name="TToAll" type="radio" id="TToAll1" value="1" />开放答卷对照表给:所有人</label><br />
                            <label>
                                <input name="TToAll" type="radio" id="TToAll2" value="2" onclick="selectMemberLogin(this.checked)" />开放答卷对照表给:登录用户(需要设置为会员答卷)</label>
                        </fieldset>
                        <div style="line-height: 16px">
                            <span class="Lamp"></span>提示:答案、分值、描述配置：<br />
                            我的问卷->高级->答案与分值设定</div>
                    </div>
                </td>
            </tr>
            <tr style="display: none">
                <td align="right" width="200" style="display: none">
                    <div align="left">
                        不允许重复回答的题目</div>
                </td>
                <td>
                    <input type="button" name="SetNotRepeatAnswerItemBt" id="SetNotRepeatAnswerItemBt"
                        onclick="setRepeatAnswerItem()" value="设置" class="SetBT" /><span id="NotRepeatAnswerItem_M"></span>
                </td>
            </tr>
            <tr>
                <td>
                    同一IP重复答卷
                </td>
                <td>
                    <label>
                        <input name="IP" type="checkbox" id="IP" value="1" />钩住允许重复</label>
                    <span id="m1"></span>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    启用IP来源甄别
                </td>
                <td>
                    <input name="IPToScreen" type="checkbox" id="IPToScreen" value="1" checked="checked"
                        style="float: left" /><span id="m_IPToScreen" style="float: left"></span>
                    <input name="Submit3" type="button" value="  设置" onclick="setIPToScreen()" class="SetBT"
                        disabled="disabled" />
                </td>
            </tr>
            <tr>
                <td>
                    使用Cookies防重复答卷
                </td>
                <td>
                    <label>
                        <input name="Cookies" type="checkbox" id="Cookies" value="1" checked="checked" />钩住不允许重复</label><span
                            id="m2"></span>
                </td>
            </tr>
            <tr>
                <td>
                    会员登录后答卷
                </td>
                <td>
                    <label>
                        <input name="MemberLogin" type="checkbox" id="MemberLogin" value="1"><span id="m3">钩住启用</span></label>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    允许问卷显示在答卷区
                </td>
                <td>
                    <label>
                        <input name="AnswerArea" type="checkbox" id="AnswerArea" value="1" checked="CHECKED"
                            disabled="disabled" />钩住启用</label><span id="m4"></span>
                </td>
            </tr>
            <tr>
                <td>
                    问卷启用
                </td>
                <td>
                    <label>
                        <input name="Active" type="checkbox" id="Active" value="1" checked="checked" />钩住启用</label>
                </td>
            </tr>
            <tr>
                <td>
                    验证码
                </td>
                <td>
                    <label>
                        <input name="CheckCode" type="checkbox" id="CheckCode" value="1" disabled="disabled" />钩住启用</label>
                </td>
            </tr>
            <tr>
                <td>
                    Email答卷通知
                </td>
                <td>
                    <label>
                        <input name="Email" type="checkbox" id="Email" value="1" disabled="disabled" />钩住启用</label><span
                            id="m7"></span>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    设置隐藏题目
                </td>
                <td>
                    <input name="HiddenItem" type="checkbox" id="HiddenItem" value="1" style="float: left" /><span
                        id="HiddenItemText"></span>
                    <input name="HiddenItemBT" type="button" id="HiddenItemBT" value="  设置" onclick="setHiddenItem()"
                        disabled="disabled" class="SetBT" />
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    设置URL传值
                </td>
                <td>
                    <input name="URLVar" type="checkbox" id="URLVar" value="1" style="float: left" /><span
                        id="URLVarText"></span>
                    <input name="URLVarBT" type="button" id="URLVarBT" value="   设置" onclick="setURLVar()"
                        disabled="disabled" class="SetBT" />
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    启用配额
                </td>
                <td>
                    <label>
                        <input name="Quota" type="checkbox" id="Quota" value="1" style="float: left" disabled="disabled" />钩选住表示通过所设置的配额的控制答卷<span
                            id="QuotaText"></span></label>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    限制答卷人员
                </td>
                <td>
                    <label>
                        <input name="GUIDAndDep" type="checkbox" id="GUIDAndDep" value="1" style="float: left"
                            disabled="disabled" />钩选住表示只允许所设定的人员或者部门答卷<span id="GUIDAndDepText"></span></label>
                </td>
            </tr>
            <tr>
                <td>
                    开放问卷结果<br />
                    (钩住启用)
                </td>
                <td>
                    <label>
                        <input name="ReportAnswerResult" type="checkbox" id="ReportAnswerResult" value="1" />答卷结果</label><br />
                    <label>
                        <input name="ReportStat" type="checkbox" id="ReportStat" value="1" />统计分析</label><br />
                    <label>
                        <input name="Report" type="checkbox" id="Report" value="1" />问卷报告</label><br />
                    <label>
                        <input name="ReportDataList" type="checkbox" id="ReportDataList" value="1" />数据列表</label><br />
                    <label>
                        <input name="ReportPoint" type="checkbox" id="ReportPoint" value="1" />评分结果</label><br />
                    <label onclick="optionCR()" style="display: none">
                        <input name="XML" type="checkbox" id="XML" value="1" onclick="optionCR()" />
                        开启问卷报表XML输出</label><br />
                    <label style="display: none">
                        <input name="CustomizeReport" type="checkbox" id="CustomizeReport" value="1" />
                        开启自定义问卷报表匿名查看
                    </label>
                    <br />
                    <label style="display: none">
                        <input name="AnswerXML" type="checkbox" id="AnswerXML" value="1" />
                        开启答卷XML输出</label>
                    <span id="m8"></span>
                </td>
            </tr>
            <tr>
                <td>
                    问卷提交到达页
                </td>
                <td>
                    <select name="EndPage" id="EndPage" onchange="showDemo(this[this.selectedIndex].value)">
                    </select>
                    <br />
                    <span id="demo"></span>
                    <input name="ToURL" type="text" id="ToURL" size="40" maxlength="250" style="display: none" />
                    <textarea cols="5" name="ComplateMessage" style="display: none"><%=sComplateMessage%></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    问卷密码
                </td>
                <td>
                    <input name="PSW" type="checkbox" id="PSW" value="1" onclick="setPSW()" /><span id="m9"></span>
                    <input name="SurveyPSW" type="text" id="SurveyPSW" style="visibility: hidden" />
                    <span id="m10"></span>
                </td>
            </tr>
            <tr>
                <td>
                    问卷类型
                </td>
                <td>
                    <select name="ClassID" id="ClassID">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    回收数
                </td>
                <td>
                    <input name="MaxAnswerAmount" type="text" id="MaxAnswerAmount" value="0" size="8"
                        maxlength="8" />
                    <span id="m11">为0表示无限制</span>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    一次性答卷密码
                </td>
                <td>
                    <input name="AnswerPSW" type="checkbox" id="AnswerPSW" value="1" /><span id="m12"></span>
                    <input name="SetAnswerPSW" type="button" class="SaveBT" id="SetAnswerPSW" value="配置密码"
                        onclick="setAnswerPSW()" />
                </td>
            </tr>
            <tr>
                <td>
                    记录答卷人IP
                </td>
                <td>
                    <input name="RecordIP" type="checkbox" id="RecordIP" value="1" checked="checked" /><span
                        id="m14"></span>
                </td>
            </tr>
            <tr>
                <td>
                    记录提交日期
                </td>
                <td>
                    <input name="RecordTime" type="checkbox" id="RecordTime" value="1" checked="checked" /><span
                        id="m15"></span>
                </td>
            </tr>
        </table>
    </div>
    <div style="background: #EFF3FB; margin: 5px">
        <input name="Submit" type="submit" class="SaveBT" value=" 保 存 " />
        <input name="Submit2" type="button" class="SaveBT" value=" 退 出 " onclick="Cancel();" />
        <input name="SID" type="hidden" id="SID" /></div>
    </form>
    <iframe src="" frameborder="0" height="330px" width="100%" name="targetWin" id="targetWin"
        style="display: none"></iframe>
</body>
</html>
