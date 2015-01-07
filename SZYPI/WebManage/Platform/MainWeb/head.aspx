<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="head.aspx.cs" Inherits="WebUI.head" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../Script/Menu.js" type="text/javascript" language="javascript" charset="gb2312"></script>

    <script language="javascript" type="text/javascript" src="js/showArrow.js" charset="gb2312"></script>

    <script type="text/javascript" src="js/head.js" charset="gb2312"></script>

    <link href="css/head.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/ChildMenu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .td
        {
            cursor: hand;
            color: #106699;
        }
    </style>

    <script type="text/javascript">
        function onmouse(this_id, mouse_op) {
            if (mouse_op == "over") {
                this_id.style.color = "#FF0000";
            }
            else if (mouse_op == "out") {
                this_id.style.color = "";
            }
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
        
        //返回网站首页
        function returnSite() {
            var webAddress = document.getElementById("Hidden1").value;
            if (webAddress) {   
                window.parent.parent.self.location = webAddress;
            }
            window.document.getElementById('lnkbtnHomePage').click();
        }
    </script>

</head>
<body onload="startClock(); timerONE=window.setTimeout">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 1024px; height: 85px; background: url(../../Images/ManagerTitle.jpg) no-repeat"
        id="TableheadImg" runat="server">
        <tr>
            <td align="right" valign="top">
                <table cellpadding="2" cellspacing="2" style="margin-right: 20px; margin-top: 5px;">
                    <tr>
                        <%--<td style="background: url(../../Images/1.jpg) no-repeat left bottom; width: 30px;
                                height: 40px;">
                            </td>
                            <td class="td" id="td1" onclick="MenuMainLink(this);" onmouseover="onmouse(this,'over');" onmouseout="onmouse(this,'out');">
                                首页
                            </td>
                            <td style="width: 5px;">
                            </td>--%>
                        <td style="background: url(../../Images/6.jpg) no-repeat left bottom; width: 30px;
                            height: 40px;">
                        </td>
                        <td class="td" id="td6" onclick="MenuMainLink(this);" onmouseover="onmouse(this,'over');"
                            onmouseout="onmouse(this,'out');">
                            注销
                        </td>
                        <td style="background: url(../../Images/1.jpg) no-repeat left bottom; width: 30px;
                            height: 40px;">
                        </td>
                        <td class="td" id="td1" onclick="returnSite();" onmouseover="onmouse(this,'over');"
                            onmouseout="onmouse(this,'out');">
                            网站首页
                        </td>
                        <input id="Hidden1" runat="server" type="hidden" />
                        <%-- <td style="background: url(../../Images/5.jpg) no-repeat left bottom; width: 30px;
                                height: 40px;">
                            </td>
                            <td class="td" id="td5" onclick="MenuMainLink(this);" onmouseover="onmouse(this,'over');" onmouseout="onmouse(this,'out');">
                                帮助
                            </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table cellpadding="0" cellspacing="0" style="width: 100%;" align="center">
                    <tr>
                        <td style="width: 180px; vertical-align: bottom; background: url(../../Images/qh.gif) no-repeat left bottom;">
                            <div id="divMenuControl" style="height: 18px; width: 100px; margin-left: 28px; cursor: hand;
                                font-size: 12px; color: #416AB7;" onclick="showMenuAll();">
                                隐藏菜单
                            </div>
                        </td>
                        <td style="color: #416AB7; padding-left: 400px; width: 40px;">
                            您好：
                        </td>
                        <td style="color: #416AB7;">
                            <asp:Label ID="lblPersonName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="right" style="padding-right: 25px;">
                            <div id="time">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" style="width: 1024px; height: 5px; background: url(../../Images/head_line.gif)">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnLogout" runat="server" Style="display: none;" OnClick="btnLogout_Click" />
    <asp:Button ID="lnkbtnHomePage" runat="server" Style="display: none;" OnClick="lnkbtnHomePage_Click" />
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    <div style="display: none">
        <asp:TextBox ID="txtPageID" runat="server" Text="td1"></asp:TextBox>
    </div>
    </form>
</body>
</html>
