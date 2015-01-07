<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="root.aspx.cs" Inherits="WebUI.root" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../CSS/Menu.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="js/showArrow.js"></script>

    <link href="../../CSS/ChildMenu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        window.onload = function() {    //����
            var hid = document.getElementById("hidInfo");
            if (hid.value == "1") {
                SmallWindow();
            }
        }

        //���½�С����
        function SmallWindow() {
            document.getElementById('winpop').style.height = '0px';
            document.getElementById('DivShim').style.height = '0px';
            setTimeout("tips_pop()", 800);     //����tips_pop()�������

        }

        //��ҳ���ܲ˵�
        function MenuPending() {
            var url = "../../Web/ReceptionManage/PendingRequest.aspx";      //��������

            window.frames[0].frames["mainFrame"].location.href = url;

            //            window.frames[0].frames["topFrame"].document.getElementById("td1").className = "RCPMenu";
            //            window.frames[0].frames["topFrame"].document.getElementById("td2").className = "RCPMenuCur";
        }

    </script>

    <script type="text/javascript">
        //        ����URL��ȡ����
        function request(paras, url) {
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
    </script>

</head>
<body style="margin: 0 0 0 0; height: 100%; background: #FFF; height: 100%" scroll="no">
    <form id="form1" runat="server">
    <div align="center" style="vertical-align: top;">
        <iframe style="width: 1024px; height: 100%;" src="index.html"></iframe>
    </div>
    <input id="_upWindowContext" type="hidden" /><!--�Ӹ����ڹ����������-->
    <input id="_zIndexControl" type="hidden" /><!--�������ڸ߶ȱ������-->
    <div id="winpop">
        <div class="title">
            ������������<span class="close" onclick="tips_pop()">X</span></div>
        <div class="con1" id="divContent" runat="server">
        </div>
        <div class="bottom">
            <span style="margin-right: 10px;"><a href="javascript:void();" onclick="MenuPending()">
                ���࡭</a></span>
        </div>
    </div>
    <iframe id="DivShim" src="" scrolling="no" frameborder="0"></iframe>
    <input id="hidInfo" type="hidden" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">
    //����ʱMainframe��Ĭ����ʾ
    window.onload = function LoadDefaultMain() {
        //        var url = "../../Survey/SurveyManage.aspx";
        var urlLcal = location.href;
        var DefaultMainValue = request('DefaultMain', urlLcal)
        if (DefaultMainValue != null && DefaultMainValue != "") {
            window.frames[0].frames["mainFrame"].location.href = DefaultMainValue;
        }
    }
</script>

