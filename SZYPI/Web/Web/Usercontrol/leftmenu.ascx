<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="leftmenu.ascx.cs" Inherits="Web.Web.Usercontrol.leftmenu" %>

<script language="JavaScript" type="text/javascript">
    function ShowMenu(noid) {
        var block = document.getElementById(noid);

        //        var n = noid.substr(noid.length - 1);

        //        var span = document.getElementsByTagName("span");

        //        for (var i = 0; i < span.length; i++) {
        //            if (i != n) {
        //                span[i].className = "no";
        //            }
        //        }

        if (block.className == "no") {
            block.className = "";
        }
        else if (block.className == "") {
            block.className = "no";
        }
    }

    function ShowChild(noid, typeid) {
        var block = document.getElementById(noid);

        if (noid != typeid) {
            if (block.className == "no") {
                block.className = "";
            }
        }
    }


    function getArgs(noid) {
        var args = new Object();
        var query = location.search.substring(1); // 获取?后面所有参数
        var pairs = query.split("&"); // 分割

        for (var i = 0; i < pairs.length; i++) {
            var pos = pairs[i].indexOf('='); // 查找类似与"name=value"
            if (pos == -1) continue; // 没找到
            var argname = pairs[i].substring(0, pos); // 取得参数名（如name）
            var value = pairs[i].substring(pos + 1); // 取得值name参数的值
            value = decodeURIComponent(value); // 这个是反编码如果传递的是中文并且htmlencode了就要decode一下

            args[argname] = value; // 赋值

        }
        if (args[0] != args[1]) {

            var block = document.getElementById(noid);

            if (block.className == "no") {
                block.className = "";
            }
        }
    }


    
</script>

<!--存在子项的层-->
<div class="list_left" id="listLeft" runat="server">
    <div class="list_lefttop">
    </div>
    <div class="list_leftmid">
        <div class="list_ul" id="leftName" runat="server">
        </div>
    </div>
    <div class="list_leftbot">
    </div>
</div>
