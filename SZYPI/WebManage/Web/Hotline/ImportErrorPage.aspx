<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportErrorPage.aspx.cs"
    Inherits="Business.Import.ImportErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入数据错误摘要报告</title>
    <base target="downloadErr">

    <script type="text/javascript">
        function getValue() {
            if (parent) {
                var obj = parent.dialogArguments[0];
                document.getElementById("hisMsg").value = obj;
                var th = "<table id=\"messageTbl\" class=\"gridview\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"border-collapse:collapse;\">" +
                    "<tr class=\"gridview-header\" align=\"center\" valign=\"middle\"><th scope=\"col\">错误数据的行号</th><th>错误类型</th><th>错误提示消息</th><th>错误数据</th></tr>";
                var objList = eval(obj);
                if (objList && objList.length > 0) {

                    var iCount = objList.length;
                    if (iCount > 100) {
                        iCount = 100;
                        setTimeout(function() { createMessage() }, 500);
                    }
                    for (var i = 0; i < iCount; i++) {
                        var err = objList[i];
                        var tr;

                        if (i % 2 == 0) {
                            tr = "<tr class=\"gridviewRow\" onmouseover=\"if(this.style.backgroundColor.toUpperCase()=='#F7F5F6'||" +
                        "this.style.backgroundColor.toUpperCase()=='') this.style.backgroundColor='#D5E7F2'\" " +
                        "onmouseout=\"if(this.style.backgroundColor.toUpperCase()=='#D5E7F2') this.style.backgroundColor='';\"><td>" + err["ErrRowNo"] + "</td><td>" + err["ErrType"] + "</td><td>" + err["ErrData"] + "</td><td>" + err["ErrMsg"] + "</td></tr>";
                        }
                        else {
                            tr = "<tr class=\"gridviewRow\" onmouseover=\"if(this.style.backgroundColor.toUpperCase()=='#F7F5F6'||" +
                        "this.style.backgroundColor.toUpperCase()=='') this.style.backgroundColor='#D5E7F2'\" " +
                        "onmouseout=\"if(this.style.backgroundColor.toUpperCase()=='#D5E7F2') this.style.backgroundColor='#F7F5F6';\"><td>" + err["ErrRowNo"] + "</td><td>" + err["ErrType"] + "</td><td>" + err["ErrData"] + "</td><td>" + err["ErrMsg"] + "</td></tr>";
                        }
                        th += tr;
                    }
                }
                th += "</table>";

                document.getElementById("msgDiv").innerHTML = th;
            }

        }

        function createMessage() {
            var tbl = document.getElementById("messageTbl");
            var obj = parent.dialogArguments[0];
            var objList = eval(obj);
            if (objList && objList.length > 0) {

                var iCount = objList.length;
                for (var i = 100; i < iCount; i++) {
                    var err = objList[i];

                    var row = tbl.insertRow();
                    var cell = row.insertCell(0);
                    cell.innerHTML = err["ErrRowNo"];

                    var cell = row.insertCell(1);
                    cell.innerHTML = err["ErrType"];

                    var cell = row.insertCell(2);
                    cell.innerHTML = err["ErrData"];

                    var cell = row.insertCell(3);
                    cell.innerHTML = err["ErrMsg"];

                }
            }
        }
    </script>

</head>
<body onload="getValue();">
    <form id="form1" runat="server">
    <table align="center">
        <tr>
            <td colspan="2" align="center">
                <div style="text-align: center; font-size: x-large; color: Red; font-weight: bolder;
                    padding-top: 10px; padding-bottom: 10px">
                    导入数据错误摘要报告
                </div>
                <iframe id="downloadErr" name="downloadErr" height="0px" width="0px"></iframe>
            </td>
        </tr>        
        <tr>
            <td align="center">
                <div id="msgDiv" style="height: 500px; width: 500px; overflow: auto">
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="export" Text="导出" CssClass="toolBarExport" runat="server" 
                    OnClick="export_Click" Width="62px" />                
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                
                &nbsp;<input type="button" value="关闭" onclick="window.close();" class="styButton" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hisMsg" runat="server" />
    </form>
</body>
</html>
