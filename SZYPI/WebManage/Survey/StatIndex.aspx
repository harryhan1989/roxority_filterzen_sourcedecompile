<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web_Survey.Survey.Survey_StatIndex, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>分析报表</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <link href="../css/StatIndex.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript"><%=sClientJs %></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/InterFace.js"></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/StatIndex.js"></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/BT.js"></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/share.js"></script>

    <%--<script language="javascript" type="text/javascript" src="Js/EditSurvey.js"></script>--%>

    <script language="javascript" type="text/javascript" charset="gbk" src="Js/x_open.js"></script>

    <script type="text/javascript">
        function showMenuAll() {
            var cols = this.top.frames[0].frames['mainFrameset'].cols;
            //		        if (cols == "0,0,*") {
            cols = "180,5,*";
            this.top.frames[0].frames['topFrame'].document.getElementById("divMenuControl").innerText = "隐藏菜单";
            showMinindex();
            //		        }
            //		        else {
            //		            cols = "0,0,*";
            //		            this.top.frames[0].frames['topFrame'].document.getElementById("divMenuControl").innerText = "显示菜单";
            //		            showMaxindex();
            //		        }
            this.top.frames[0].frames['mainFrameset'].cols = cols;
        }

        //显示菜单后首页
        function showMinindex() {
            if (window.parent.frames["mainFrame"].document.getElementById("tbAll") != null) {
                window.parent.frames["mainFrame"].document.getElementById("tbAll").style.width = '836px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg1").style.width = '368px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg2").style.width = '368px';
                window.parent.frames["mainFrame"].document.getElementById("tdTime").style.width = '178px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn1").style.width = '245px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn2").style.width = '270px';
                window.parent.frames["mainFrame"].document.getElementById("tdsize").style.width = '98px';

                window.parent.frames["mainFrame"].document.getElementById("hidAll").value = '836px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg1").value = '368px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg2").value = '368px';
                window.parent.frames["mainFrame"].document.getElementById("hidTime").value = '178px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn1").value = '245px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn2").value = '270px';
                window.parent.frames["mainFrame"].document.getElementById("hidsize").value = '98px';
            }
        }

        //隐藏菜单后首页
        function showMaxindex() {
            if (window.parent.frames["mainFrame"].document.getElementById("tbAll") != null) {
                window.parent.frames["mainFrame"].document.getElementById("tbAll").style.width = '1020px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg1").style.width = '485px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg2").style.width = '482px';
                window.parent.frames["mainFrame"].document.getElementById("tdTime").style.width = '180px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn1").style.width = '260px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn2").style.width = '240px';
                window.parent.frames["mainFrame"].document.getElementById("tdsize").style.width = '301px';

                window.parent.frames["mainFrame"].document.getElementById("hidAll").value = '1020px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg1").value = '485px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg2").value = '482px';
                window.parent.frames["mainFrame"].document.getElementById("hidTime").value = '180px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn1").value = '260px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn2").value = '240px';
                window.parent.frames["mainFrame"].document.getElementById("hidsize").value = '301px';
            }
        }

        function initFace() {
            var face = new interface();
            face._MaskStyleName = "BgWin";
            face._initMask($("BgWin"), "#666666");
            face._openMask($("BgWin"));
            //            document.getElementById("1").style.filter="Alpha(opacity=30)"
            $("BgWin").style.filter = "Alpha(opacity=30)";
        }
        function refresh() {
            $("BgWin").style.display = 'none';
        }
        
    </script>

</head>
<body class="BlackFont" scroll="no">
    <div class="Top" style="padding: 5px; background-color: #FFFFFF; height: 25px">
        <div style="float: left; color: #000000">
            问卷:<span id="SurveyName" runat="server"></span>
        </div>
        <div style="float: right">
            <img src="images/Back.gif" alt="返回" width="28" height="29" hspace="10" style="cursor: pointer"
                onclick="showMenuAll();location.href='SurveyManage.aspx'" /></div>
        <div style="float: right; margin-top: 5px">
            <span id="SL" runat="server"></span>
        </div>
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="5" cellspacing="0" class="BlackFont">
                    <tr>
                        <td align="center" bgcolor="#FFFFFF" onmouseover="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" id="B1" class="BtNormal1"
                            onclick="clickBT(this.id,'SurveyReport','BtDown1','BtNormal1',9)" >
                            <div align="center">
                                <img src="images/report.gif" alt="问卷报告" width="32" height="32" border="0" /><br />
                                问卷报告</div>
                        </td>
                        <td align="center" bgcolor="#FFFFFF" onmouseover="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" id="B2" class="BtNormal1"
                            onclick="clickBT(this.id,'QueryState','BtDown1','BtNormal1',9)" >
                            <div align="center">
                                <img src="images/querystat.gif" alt="查询分析" width="32" height="32" /><br />
                                查询分析</div>
                        </td>
                        <%--                 <td align="center" bgcolor="#FFFFFF" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" id="B3" class="BtNormal1"
                            onclick="clickBT(this.id,'IntersectStat','BtDown1','BtNormal1',9)">
                            <div align="center">
                                <img src="images/intersectTable.gif" alt="交叉分析" width="27" height="28" /><br />
                                交叉分析</div>
                        </td>--%>
                        <%--                                            <td align="center" bgcolor="#FFFFFF" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" id="B4" class="BtNormal1"
                            onclick="clickBT(this.id,'AnswerCardView','BtDown1','BtNormal1',9)">
                            <img src="images/Card.gif" alt="单张答卷卡片式查阅" width="32" height="32" /><br />
                            卡片阅卷
                        </td>
                        <td align="center" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" id="B5" class="BtNormal1"
                            onclick="clickBT(this.id,'AnswerListView','BtDown1','BtNormal1',9)">
                            <img src="images/datalist.gif" alt="多行答卷明细列表查看" width="32" height="32" /><br />
                            数据列表
                        </td>--%>
                        <%--                        <td align="center" class="BtNormal1" id="B6" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" onclick="clickBT(this.id,'FindAllAnswer','BtDown1','BtNormal1',9)">
                            <img src="images/exportdata.gif" alt="导出excel格式答卷数据" width="32" height="32" /><br />
                            数据导出
                        </td>--%>
                        <%--                        <td align="center" class="BtNormal1" id="B7" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" onclick="clickBT(this.id,'GetRandomAnswer','BtDown1','BtNormal1',9)">
                            <img alt="随机抽卷" src="images/tz.gif" width="32" height="32" /><br />
                            随机抽卷
                        </td>
                        <td align="center" class="BtNormal1" id="B8" onmousemove="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" onclick="clickBT(this.id,'DataQuery','BtDown1','BtNormal1',9)">
                            <img src="images/DataQuery.gif" width="28" height="29" alt="数据查询" /><br />
                            数据查询
                        </td>--%>
                    </tr>
                </table>
            </td>
            <td align="right">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="BtNormal1">
                    <tr>
                        <td class="BtNormal1" id="B10" onmouseover="setBT(this.id,1,'BtDown1','BtMove1','BtNormal1')"
                            onmouseout="setBT(this.id,0,'BtDown1','BtMove1','BtNormal1')" onclick="printTarget()">
                            <img alt="" src="images/Print.gif" name="PrintBT" width="32" height="32" id="PrintBT"
                                style='filter: gray'>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#C7DCF7">
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td style="height: 2px; background-color: #999999">
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 100%" border="0" cellpadding="0" cellspacing="0"
        id="OptionArea">
        <tr>
            <td valign="top" style="width: 200px; display: block" id="LeftMenu">
                <div id="ItemList" style="overflow-x: hidden; overflow-y: auto">
                </div>
            </td>
            <td background="images/Line.gif" style="width: 7px; cursor: pointer" onclick="ShowUploadWin()"
                onmousemove="setMouseMove(0)" onmouseout="setMouseMove(1)">
                <img src="images/ShowLeft1.gif" width="7" height="50" id="BT" alt="" />
            </td>
            <td valign="top" class="mainBG" width="100%" style="height: 1px" id="Main">
                <iframe name="targetWin" id="targetWin" style="width: 100%" marginheight="0" marginwidth="0"
                    frameborder="0" height="0"></iframe>
            </td>
        </tr>
    </table>
    <table width="100%" style="height: 25px; display: none">
        <tr>
            <td align="center" bgcolor="#D5ECFA">
                CopyRight 2010 sz12355.COM
            </td>
        </tr>
    </table>
    <div id="BgWin" style="position: absolute; top: 0px; background-color: #000000; z-index: 2;
        left: 0px; display: none" class="BgWin">
    </div>
    <div id="OpenWin" style="position: absolute; z-index: 4; width: 540px; height: 150px;
        background-color: #fff; display: none; text-align: center; vertical-align: middle;
        left: 242px; top: 307px; line-height: " class="ShadowEffect">
        <div class="BlackFont">
            <br />
            <br />
            <br />
            <img src="images/loading.gif" alt="" /><br />
            载入中</div>
    </div>
</body>
</html>

<script type="text/javascript">

</script>

