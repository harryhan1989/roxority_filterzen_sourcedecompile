<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_EnterPSW, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>输入密码</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
	<link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body class="BlackFont">
    <form id="form1" runat="server" method="post">
    <div>
        <table style="width:400px;height:300px"  border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td align="right"><table width="100%"  border="0" cellpadding="0" cellspacing="5" bgcolor="#009900">
              <tr>
                <td bgcolor="#FFFFFF"><table width="100%"  border="0" align="center" cellpadding="5" cellspacing="5" class="BlackFont">
                    <tr>
                      <td width="75%" align="center">            </td>
                    <td width="13%" align="center" title="问卷被锁，需要问卷密码"><img alt="" src="images/112.png" width="32" height="32" /></td>
                    <td width="12%" align="center" title="输入正确问卷密码后进入问卷"><img alt="" src="images/115.png" width="32" height="32" /></td>
                    </tr>
                    <tr>
                      <td colspan="3" align="center">            <table width="100%"  border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td><div align="center"><strong>输入密码</strong></div></td>
                            <td><div align="center">
                              <asp:TextBox ID="PSW" runat="server" CssClass="" TextMode="Password"></asp:TextBox>
                            </div></td>
                            <td><div align="center">
                              <asp:Button ID="Button1" runat="server" Text=" 确 定 " OnClick="Button1_Click" postbackurl="" CssClass="SaveBT" />                            
                            </div></td>
                          </tr>
                        </table></td>
                    </tr>
                    <tr>
                      <td colspan="3" align="center"><asp:Label ID="Label1" runat="server"></asp:Label></td>
                    </tr>
                </table></td>
              </tr>
            </table><span style="background-color:#666666"><a href="http://www.12355.com" style="color:#FFFFFF; text-decoration: none " title="访问12355.COM"> <br />
            12355.COM </a></span></td>
          </tr>
        </table>
      </div>
    </form>
</body>
</html>
