<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebManage.Web.HuiYuan.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title>����12355���������̨</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<LINK href="main.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<style type="text/css">.STYLE1 { FONT-WEIGHT: bold; COLOR: #998103 }
		</style>
		<script language="javascript" src="../Script/Nandasoft.js"></script>
		<script language="javascript">
		function dosu()
		{
		  //document.location.replace('login.aspx');
		  window.close();
		}

		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		<br>
			<table cellSpacing="0" cellPadding="0" width="180" border="0" runat="server" id="TBLogin"
				align="center">
				<tr>
					<td><IMG height="35" src="../../images/reg_top1.jpg" width="180"></td>
				</tr>
				<tr>
					<td background="../../images/login_bg.jpg">
						<table cellSpacing="3" cellPadding="0" width="90%" align="center" border="0">
							<TBODY>
								<tr>
									<td>�û�����<input class="form1" type="text" size="12" name="f_08LoginAcc" id="f_08LoginAcc" runat="server"
											contextLabel="�û���" alt="required">
									</td>
								</tr>
								<tr>
									<td>��&nbsp; �룺<INPUT class="form1" id="f_08LoginPWD" type="text" size="12" name="f_08LoginPWD" runat="server"
											DESIGNTIMEDRAGDROP="316"></td>
								</tr>
								<tr>
									<td>��&nbsp; ����<INPUT class="form1" id="f_04Name" type="text" size="12" name="f_04Name" runat="server"
											contextLabel="����" alt="required" DESIGNTIMEDRAGDROP="336"></td>
								</tr>
								<tr>
									<td>��&nbsp; ����<INPUT class="form1" id="f_03tel" type="text" size="12" name="f_03tel" runat="server" DESIGNTIMEDRAGDROP="337"></td>
								</tr>
								<tr>
									<td>EMail ��<input class="form1" type="text" size="12" name="f_05Email" id="f_05Email" runat="server"></td>
								</tr>
								<tr>
									<td align="center"><input type="image" src="../../images/zc_button.jpg" value="�ύ" name="Submit2" id="Image2" runat="server"
											onclientclick="return CheckValue();" onclick="Image2_ServerClick" ></td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
				<tr>
					<td><IMG height="9" src="../../images/login_2.jpg" width="180"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>