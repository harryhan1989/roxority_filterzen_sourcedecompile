﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemAnalysis.aspx.cs" Inherits="WebManage.Survey.ItemAnalysis" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="Js/Share.js"></script>

    <script language="javascript" type="text/javascript" src="Js/httable.js"></script>

    <script language="javascript" type="text/javascript" src="Js/ItemAnalysis.js"></script>

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
        .BlackFont
        {
            font-size: 12px;
            color: Black;
        }
        .ShadowEffect
        {
            border: 1px solid #999999;
            filter: progid:DXImageTransform.Microsoft.Shadow (Color=#333333,Direction=120,strength=5);
        }
    </style>

    <script type="text/javascript">
        function InmageMouseOver(obj) {
            obj.style.border = "2px solid Silver";
            //            document.getElementById("").style.
        }

        function InmageMouseLeave(obj) {
            obj.style.border = "";
        }

        function setChartVisible(obj) {
            clearAllChart();

            document.getElementById(obj).style.display = "";
        }

        function setChartClick(obj) {
            parent.document.getElementById("OpenWin").style.display = 'block';
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
            parent.document.getElementById("OpenWin").style.display = 'none';
        }

        function buttonClientClick() {
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

        function CheckLength(obj, maxLength) {
            if (obj.value.length > maxLength) {
                obj.value = obj.value.substring(0, maxLength - 1);
            }
        }
        function innerDivHtml(obj) {
            document.getElementById("chartContent").innerHTML = document.getElementById("chartContent").innerHTML + obj;
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
    <div id="BgWin" style="position: absolute; top: 0px; background-color: #000000; z-index: 2;
        left: 0px; display: none" class="BgWin">
    </div>
    </form>
</body>
</html>
