<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeApply.aspx.cs" Inherits="WebManage.Web.Gifts.ExchangeApply" %>
<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="botton1" TagName="utlBotton" Src="~/Controls/ButtonPageControl.ascx"  %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>礼品兑换</title>
    <link href="../../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Platform/script/Message.js"> </script>
<script type="text/javascript" src="../../Platform/script/common.js"> </script>
     <style type="text/css">
    .divBgBtn{background:url(../../css/Image/index/btn.gif) no-repeat;border:none; width:68px;height:23px;line-height:23px; margin-top:15px; text-align:center;}
    .panel{zoom:1;}
    .panel-nobody .panel-hc{padding-bottom:0;border-bottom:0;}
    .panel-tl,.panel-tr{background:url(../../css/Image/index/panel_corners.gif) no-repeat 0 0;height:4px;}
    .panel-tl,.panel-tr,.panel-tc{line-height:1px;overflow:hidden;}
    .panel-tl{padding-left:4px;background-position:0 0;}
    .panel-tr{padding-right:4px;background-position:right 0;}
    .panel-tc{height:3px;border-top:1px solid #dedede;background:#fff;}
    .panel-hc{border:1px solid #dedede;border-width:0 1px 1px 1px;padding:1px;background:#fff;}
    .panel-hc .panel-header{padding:1px 3px 4px 10px;*padding:2px 3px 3px 10px;border:0;background:url(../../css/Image/index/panel_bg_x.gif) repeat-x left 0;}
    .panel-border .panel-header button{margin:0;}
    .f14{font-size:14px;}
    .b{font-weight:bold;}
    .gray{color:#666;}
    .red{color:#c00;}
    .panel-bl,.panel-br{background:url(../../css/Image/index/panel_corners.gif) no-repeat 0 0;height:6px;}
    .panel-bl,.panel-br,.panel-bc{line-height:1px;overflow:hidden;}
    .panel-bl{padding-left:6px;background-position:0 bottom;}
    .panel-br{padding-right:6px;background-position:right bottom;}
    .panel-bc{height:5px;border-bottom:1px solid #dedede;background:#fff;}
    .panel-bc div{height:4px;background:#ececec;overflow:hidden;}
    .panel-spacer{height:2px;border:1px solid #dedede;border-width:0 1px;background:url(../../css/Image/index/panel_bg_x.gif) left -30px;overflow:hidden;}

    div.content{padding:10px 10px 20px 10px;line-height:22px;color:#666;}  
    div,ul,ol,li,p,form,dl,dt,dd,fieldset{margin:0;padding:0;}
    ol,ul,li{list-style:none;}     
    </style> 
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
  <!--页眉-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
    <div class="surveytable">
      <div class="lpdh_top"></div>
      <div class="lpdh_mid">
                    <div style="border:solid 1px #ccc; float:left; width:30%;">
                    <asp:Image ID="img" runat="server" style="border-width:0px; width:270px; height:120px;" />
                     
                    </div>
                    <div style="float:left; margin-left:20px;">
                      <b><span id="giftName" class="f14">
                      <asp:Label ID="lblGiftName" runat="server" ></asp:Label>
                                            
                      </span></b><br /><br />
                      <b>兑换积分：</b><span id="giftNeedPoint" class="red" style="font-weight:bold;">
                      <asp:Label ID="lblNeedPoint" runat="server"></asp:Label>
                      积分</span><br />
                       <div class="divBgBtn">
                            <asp:LinkButton ID="btnExchange" runat="server" Text="立即兑换" class="f14 b" 
                                style="color:#333333;" OnClientClick="return CheckExchange();" onclick="btnExchange_Click"></asp:LinkButton>
                        </div>
                        <div id="address" style="padding-top:10px; margin-top:10px;">
                            <span class="red" style="font-weight:bold; ">请到下面地址领取兑换礼品:沧浪区觅渡桥西南侧(南门路8号)，青少年活动中心1楼东侧，12355热线台</span>
                        </div>
                    </div>
                    <div style="clear:both; height:20px;"></div>
                    <div class="panel panel-nobody" style="width:850px; height:">
		                <div class="panel-tl"><div class="panel-tr"><div class="panel-tc"></div></div></div>
		                <div class="panel-hc">
			                <div class="panel-header" style="padding:2px 3px 2px 8px;">
				                <p class="f14 b gray">礼品描述</p>
			                </div>
		                </div>
		                <div class="panel-bl"><div class="panel-br"><div class="panel-bc"><div></div></div></div></div>
	                </div>
                    <div>
                    <div class="content" id="divDescription" runat="server">
		                
	                </div></div>
	    <div class="clearboth"></div>
      </div>
      <div class="lpdh_bottom"></div>
    </div>

  </div>
	<div id="divBotton">
	<botton1:utlBotton id="utlBotton" runat="server"></botton1:utlBotton>
	<anthem:HiddenField ID="hdfid" runat="server" />
	</div>
    </form>
</body>
</html>

<script type="text/javascript">
    //搜索按钮点击事件
    function SearchClick()
    {
        var keyCode = document.getElementById("utlTop1$txtSearchCondition").value;
        
        window.location.href="../../Web/Survey/AllSurveysPage.aspx?KeyCode=" + keyCode;        
    }
    
    //确认兑换礼品
    function CheckExchange()
    {
        if(confirm("确认要兑换该礼品吗？"))
        {        
            var result = Anthem_InvokePageMethod('CheckHasLogin', [""], null);
            
            if(result.value==false)
            {
                alert('登录系统后才能进行礼品兑换!');
                return false
            }
            
             return true;
        }
         
        return false;
    }
</script>
