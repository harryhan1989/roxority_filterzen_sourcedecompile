<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZMenuVistor.aspx.cs" Inherits="WebManage.Web._12355WebVistor.ZMenuVistor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        function clearAllChart() {
            document.getElementById("chart3DPie").style.display = "none";
            document.getElementById("chart3DColumn").style.display = "none";
            document.getElementById("chartPie").style.display = "none";
            document.getElementById("chartColumn").style.display = "none";
            document.getElementById("chartDoughnut").style.display = "none";
            document.getElementById("chart3DDoughnut").style.display = "none";
            document.getElementById("chart3DBar").style.display = "none";
            document.getElementById("chartBar").style.display = "none";
        }

        function CheckLength(obj, maxLength) {
            if (obj.value.length > maxLength) {
                obj.value = obj.value.substring(0, maxLength - 1);
            }
        }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="chartMenu">
    <table id="Table1" class="ChartMenu" height="77" border="0" cellpadding="0" cellspacing="0">
            <tr>
               
                <td style="padding-left:140px">
                
                    <asp:Label ID="Label1" runat="server" Text="查看功能点下新闻点击率"></asp:Label>
                <asp:DropDownList ID="dropSelect" runat="server" 
                        onselectedindexchanged="dropSelect_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />
                    </td>
            </tr>
            </table>
        <table id="ChartMenu" class="ChartMenu" height="77" border="0" cellpadding="0" cellspacing="0">
            
            <tr>
                <td>
                    <img src="images/chart/chart_06.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chart3DPie')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_05.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chartPie')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_08.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chart3DDoughnut')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_07.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chartDoughnut')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_02.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chart3DBar')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_01.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chartBar')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_04.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chart3DColumn')" width="59" height="59" alt="">
                </td>
                <td>
                    <img src="images/chart/chart_03.gif" onmouseover="InmageMouseOver(this);" onmouseleave="InmageMouseLeave(this)"
                        onclick="setChartVisible('chartColumn')" width="59" height="59" alt="">
                </td>
            </tr>
        </table>
    </div>
    <div id="chartContent" class="chartContent" runat="server">
    </div>
    </form>
</body>
</html>
