<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuanLogin.aspx.cs" Inherits="WebManage.HuiYuanLogin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/szypistyle.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
		function Reg()
		{		 
		    var today = new Date();
		    var val = window.showModalDialog("http://www.sz12355.com/home/reg.aspx?a=" + today,null,"dialogWidth:300px;dialogHeight:300px;resizable:yes;center:yes");
		}
		
		function ExitClick()
		{
		    window.parent.location.href="Index.aspx";
		    
		    
		    return true;
		}
	</script>
</head>
<body>
    <form id="form1" runat="server" style="height:34px;">
    <div class="user_mid"> 
    <div class="displayinfo">
        目前共有会员
        <span class="huiy"></span>
        <span class="font12">
            <asp:Label ID="lblHuiYuanAmount" runat="server" ></asp:Label>
        </span>
        人，共有问卷
        <span class="wenj"></span>
        <span class="font12">
            <asp:Label ID="lblSurveyAmount" runat="server" ></asp:Label>
        </span>
        份，共有
        <span class="huiy"></span>
        <span class="font12">
            <asp:Label ID="lblAnswers" runat="server" ></asp:Label>
        </span>
        人填写了问卷
        <span class="widthspace">
        </span>
      </div>  
        <div id="divLogin" runat="server" class="displayinfo">
            登录账号：
            <asp:TextBox ID="txtHuiYuanAccount" runat="server" class="textbg_user"></asp:TextBox>
            <span class="widthspace0"></span>
            密码：
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" class="textbg_pwd" onkeypress="OnKeyPress(event.keyCode);"></asp:TextBox>
            <span class="widthspace0"></span>        
            <asp:LinkButton ID="lbLogin" runat="server"  
                style="background:url('CSS/image/Index/login.jpg') no-repeat; vertical-align:middle"  
                Width="50px" Height="22px" onclick="lbLogin_Click"></asp:LinkButton>
            <span class="widthspace0"></span>
            <a href="Javascript:Reg();" >注册</a>
            
            <span class="widthspace0"></span>
            <a href="#">帮助？</a>
        </div>
        <div id="divUser" runat="server" class="displayinfo">
            <asp:Label id="lblHuiYuanName" runat="server" ForeColor="Blue"></asp:Label>
				&nbsp; 欢迎您回来！&nbsp;
			<asp:LinkButton id="lbExit" runat="server" onclick="lbExit_Click">【退出登陆】</asp:LinkButton>
			
			<span class="widthspace0"></span>
            <a href="#">帮助？</a>
        </div>   
    
      </div>
    </form>
</body>
</html>

<script type="text/javascript">
function OnKeyPress(keycode)
{
    if(keycode == "13")
    {
        document.getElementById("lbLogin").click();
    }
}
</script>