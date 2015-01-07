<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_FileToServer, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <script language="javascript" type="text/javascript">
    function init(){
        if(document.getElementById("Label1").innerText!=""){
            window.parent.document.getElementById("FileWin").src = "FileExplorer.aspx?"+escape(new   Date());
        }
    }
    
    </script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:#EFF3FB; margin:2px" class="BlackFont" onload="init()">
    <form id="form1" runat="server">
    <div  style="text-align:center; "> 
        <table width="100%" border="0" cellpadding="0" cellspacing="2">
            <tr>
                <td style="width: 100%; height: 20px; text-align:left"><strong>
                  文件上传:</strong>从本地上传文件到服务器</td>
            </tr>
      </table>
     
<asp:FileUpload ID="f0" runat="server" Width="200px" /><br /><asp:FileUpload ID="f1" runat="server" Width="292px" /><br /><asp:FileUpload ID="f2" runat="server" Width="292px" /><br /><asp:FileUpload ID="f3" runat="server" Width="292px" /><br /><asp:FileUpload ID="f4" runat="server" Width="292px" /><br />
<div style="margin:5px"><asp:Button ID="BT" runat="server" OnClick="Button1_Click" Text=" 上 传 " /></div>
</div>
<div style="text-align:left">  <asp:Label ID="Label1" runat="server" Text=""></asp:Label></div>
    </form>
</body>
</html>
