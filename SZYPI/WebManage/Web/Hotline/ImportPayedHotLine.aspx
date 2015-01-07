<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportPayedHotLine.aspx.cs" Inherits="Business.Import.ImportPayedHotLine" %>

<%--<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>公积金扣款情况导入</title>
    <base target="_self">
    <script src="../Js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        //导入时,有错误数据时触发
        function OpenImportErrorPage()
        {
            var width = 800;
            var height = 600;
            var msgList = [];
            var L = (screen.width - width) / 2;
            var T = (screen.height - height) / 2;
            var vs = document.getElementById("hidMsg").value;
            msgList[0] = vs;
            window.showModalDialog('ImportErrorPage.aspx', msgList, 'dialogWidth:' + width + 'px;dialogHeight:' + height + 'px; dialogLeft:' + L + 'px; dialogTop:' + T + 'px;  status=no;help=no;resizable=no')
        }
        function confirmClose() {
            if (confirm('确定关闭?')) {
                top.window.close();
            }
            else {
               
            }
        }
    </script>
    <style type="text/css">
        /*导入*/
        .loadajj_container
        {
            margin: 0 auto;
            text-align: center;
            margin-top: 2px;
        }

        /*导入数据头部*/
        .loadajj_top
        {
            width: 447px;
            height: 21px;
            background: url(images/loaddatabg_03.jpg) repeat-x;
            border: 1px solid #79A2C2;
            color: #000;
            font-weight: bold;
            font-size: 14px;
            padding-top: 5px;
            padding-left: 5px;
            text-align: left;
        }
        /*导入数据主题*/
        .loadajj_main
        {
            margin-top: 2px;
            width: 346px;
            height: 124px;
            border: 1px solid #79A2C2;
            background: #F5FAFE;
            text-align: left;
            padding-top: 26px;
            padding-left: 39px;
            font-size: 12px;
        }

        .loadajj_load
        {
            width: 350px;
        }

        .loadajj_load_text
        {
            height: 25px;
            width: 407px;
            background: #fff;
            border: 1px solid #79A2C2;
        }
        
        .load_anniu
        {
            width: 372px;
            margin-top: 20px;
            margin-bottom: 20px;
            margin-right: 10px;
            text-align: right;
        }
    </style>
</head>
<body  style=" background:#CFE5F9;">
    <form id="form1" runat="server">
    <div class="loadajj_container">
        <div class="loadajj_top">热线数据导入</div>
            <div class="loadajj_main">
                <div class="loadajj_load">
                    <asp:FileUpload ID="fuFileUpload" runat="server" Width="350px" />
                </div>
                 
                <div class="load_anniu">
                <asp:Button ID="btnOK" SkinID="importBtn"  runat="server" Text="确  定" OnClientClick="this.value='导入中...';" onclick="btnOK_Click"/>                    
                    <asp:Button ID="btnClose" SkinID="importBtn" runat="server" Text="关  闭" OnClientClick="parent.closeit();return false;" />
                    
                    
                    
                </div>
               
        </div>
    </div>
<asp:HiddenField ID="hidMsg" runat="server" />
    </form>
</body>
</html>