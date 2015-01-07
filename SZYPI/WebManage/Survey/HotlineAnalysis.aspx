<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotlineAnalysis.aspx.cs"
    Inherits="WebManage.Survey.HotlineAnalysis" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .OptionTable
        {
            border: 1px double #000000;
            border-collapse: collapse;
            width: 700px;
        }
        .OptionTable th, .OptionTable td
        {
            border: 1px solid #666666;
            color: #000000;
            font-size: 12px;
            text-align: right;
        }
        .OptionTable th
        {
            background: url(images/d0_22.jpg) 0 0 repeat-x;
            height: 25px;
            line-height: 25px;
            font-weight: bold;
            text-align: center;
            font-size: 12px;
        }
        .OptionTable td
        {
            height: 21px;
            line-height: 21px;
            padding: 0 5px;
            font-size: 12px;
        }
        .OptionTable .Statistics
        {
            height: 21px;
            line-height: 21px;
            padding: 0 5px;
            font-weight: bold;
            text-align: center;
            font-size: 12px;
        }
        .TableTitle
        {
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .DivConclusion
        {
            font-size: 12px;
            font-weight: bold;
            text-align: left;
            vertical-align: text-top;
        }
        .itemConclusionText
        {
            font-size: 12px;
            text-align: left;
            float: left;
        }
        .SaveItem
        {
            font-size: 12px;
            font-weight: bold;
            float: left;
            width: 70px;
            height: 20px;
            margin-left: 20px;
            margin-top: 45px;
            cursor: pointer;
        }
        .ChartMenu tr td
        {
            text-align: center;
            cursor: pointer;
        }
        .chartMenu, .chartContent
        {
            margin-top: 10px;
            margin-left: 5px;
        }
    </style>

    <script type="text/javascript">
        function checkInput() {
            var minRecord;
            var maxRecord;

            var dateStart;
            var dataEnd;

            dateStart = document.getElementById("wdcBeginDate").value;
            dataEnd = document.getElementById("wdcEndDate").value;

            if (new Date(dateStart.split("-").join("/")) > new Date(dataEnd.split("-").join("/"))) {
                alert("输入的日期范围开始需大于结束!");
                return false;
            }

            clearAllChart()
            Anthem_InvokePageMethod('QueryClick', [], null);
            return false;
        }
        function InmageMouseOver(obj) {
            obj.style.border = "2px solid Silver";
            //            document.getElementById("").style.
        }

        function InmageMouseLeave(obj) {
            obj.style.border = "";
        }
        function setChartClick(obj) {
            clearAllChart();
            try {
                document.getElementById(obj).style.display = "";
                var movie = document.getElementById(obj)
                movie.play();
                //                return true;
            }
            catch (e) {
                Anthem_InvokePageMethod('SetChart', [obj], null);
            }
        }
        function btnQueryClientClick() {
            Anthem_InvokePageMethod('itemConclusionButtonClick', [], null);
            return false;
        }

        function clearAllChart() {
            try {
                document.getElementById("chart3DPie").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chart3DColumn").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chartPie").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chartColumn").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chartDoughnut").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chart3DDoughnut").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chart3DBar").style.display = "none";
            }
            catch (e)
            { }
            try {
                document.getElementById("chartBar").style.display = "none";
            }
            catch (e)
            { }
        }
        function innerDivHtml(obj) {
            document.getElementById("chartContent").innerHTML = document.getElementById("chartContent").innerHTML + obj;
        }
        function innerDivAllHtml(obj) {
            document.getElementById("chartContent").innerHTML =  obj;
        }
        function innerDivTableHtml(obj) {
            document.getElementById("chartTable").innerHTML = obj;
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" style="font-size: 12px;">
        <tr>
            <td>
                统计类别选择：
            </td>
            <td>
                <asp:DropDownList ID="statisticsClass" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                时间段选择：
            </td>
            <td>
                <igsch:WebDateChooser ID="wdcBeginDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                    MinDate="2000-02-19" NullDateLabel="">
                </igsch:WebDateChooser>
            </td>
            <td>
                至
            </td>
            <td>
                <igsch:WebDateChooser ID="wdcEndDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                    MinDate="2000-02-19" NullDateLabel="">
                </igsch:WebDateChooser>
            </td>
            <td align="right" width="80px" rowspan="2">
                <asp:Button ID="btnQuery"  Text="分 析" runat="server" CssClass="button70" OnClientClick="return checkInput();"
                    OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <div class="chartMenu">
        <table id="ChartMenu" class="ChartMenu" height="77" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="images/chart/chart_06.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chart3DPie')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_05.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chartPie');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_08.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chart3DDoughnut');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_07.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chartDoughnut');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_02.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chart3DBar');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_01.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chartBar');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_04.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chart3DColumn');" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_03.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="return setChartClick('chartColumn');" width="59" height="59" alt="">
                </td>
            </tr>
        </table>
    </div>
    <div id="chartContent" class="chartContent" runat="server">
    </div>
    <div id="chartTable" class="chartContent" runat="server">
    </div>
    </form>
</body>
</html>
