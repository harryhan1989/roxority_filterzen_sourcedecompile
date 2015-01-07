<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="WebManage.Web.Gifts.Show" %>

<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="botton1" TagName="utlBotton" Src="~/Controls/ButtonPageControl.ascx"  %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>礼品展示</title>
<link href="../../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Platform/script/Message.js"> </script>
<script type="text/javascript" src="../../Platform/script/common.js"> </script>
</head>
<body>
<form id="Form1" runat="server">
<div id="container">
  <!--页眉-->
  <top1:utlTop id="utlTop1" runat="server"></top1:utlTop>
  <div class="main">
    <div class="clearboth"></div>
    <div class="surveytable">
      <div class="lpdh_top"></div>
      <div class="lpdh_mid">
      
      
      <!--礼品兑换区-->
        <div class="lpdh_left">
          <div class="lpdh_left_top"></div>
          <div class="lpdh_left_mid">
            <ul style="text-align:left">
            <asp:Repeater ID="rGiftsShow" runat="server" >
            <ItemTemplate>
              <li style="width:140px; white-space:normal ; "><img src='ImagePhoto.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID") %>' onclick="window.location.href='exchangeapply.aspx?giftguid=<%# DataBinder.Eval(Container.DataItem,"ID") %>';" width="140" height="100" style="cursor:pointer" />
                <div class="wp"><span class="jif"><%# DataBinder.Eval(Container.DataItem, "NeedPoint")%></span>积分<br />
                 <%# DataBinder.Eval(Container.DataItem, "GiftName")%></div>
              </li>
              </ItemTemplate>
              </asp:Repeater>              
            </ul>
          </div>
		 <div class="clearboth"></div>		  
		  <div class="dhjl"></div>		  
		  <div class="dhjf_detail" id="demo" style="overflow:hidden;height:100px;">
		    <div id="demo1">
		        <ul >
		        <asp:Repeater ID="rExchangeHistory" runat="server"  >
		        <ItemTemplate>
			      <li>·<%# DataBinder.Eval(Container.DataItem,"date") %> 
			      <span class="font12blue"><%# DataBinder.Eval(Container.DataItem,"huiyuanname") %></span> 
			      兑换了<span class="font12blue"><%# DataBinder.Eval(Container.DataItem,"giftname") %> 
			      (<%# DataBinder.Eval(Container.DataItem,"usedpoint") %>积分)</span></li>			  
			      </ItemTemplate>
			      </asp:Repeater>
			    </ul>	
			</div>
			<div id="demo2"></div> 
		  </div>
		   		  
        </div >	
        
       
        <div class="lpdh_right">
		  <div class="lpdh_right_top"></div>
		  <div style="height:10px; overflow:hidden; text-align:left; background:#eff9ff;"></div>

		  <div class="lpdh_right_mid">
		    <ul>
		        <asp:Repeater ID="rHotGift" runat="server">
		            <ItemTemplate>
                      <li style="white-space:normal ;">			  
			          <div class="jft"><img src="ImagePhoto.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID") %>" onclick="window.location.href='exchangeapply.aspx?giftguid=<%# DataBinder.Eval(Container.DataItem,"ID") %>';" width="100px" height="78px" style="cursor:pointer"/></div>
                        <div class="wp0">
                            <span class="jif">
                                <%# DataBinder.Eval(Container.DataItem, "NeedPoint")%>
                            </span>
                            <span>积分<br />
                                <%# DataBinder.Eval(Container.DataItem, "GiftName")%>
                            </span>
                        </div>
				          <div class="clearboth"></div>				
                      </li>
			          <hr class="hr0" />			  
			        </ItemTemplate>
			   	</asp:Repeater>
			</ul>		   
		   </div>	    

		  <div class="lpdh_right_bottom"></div>		
		</div>
			
        <div class="clearboth"></div>
      </div>
      <div class="lpdh_bottom"></div>
    </div>

  </div>
  <!--页脚-->
  <botton1:utlBotton id="utlBotton" runat="server"></botton1:utlBotton>
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
    
    
    var speed=50;  //调整滚动速度
    var tab=document.getElementById("demo");
    var tab1=document.getElementById("demo1");
    var tab2=document.getElementById("demo2");
    tab2.innerHTML=tab1.innerHTML;
    function Marquee(){
    if(tab2.offsetTop-tab.scrollTop <=0)
        tab.scrollTop-=tab1.offsetHeight;
    else{
        tab.scrollTop++;
      }
    }
    var MyMar=setInterval(Marquee,speed)
    tab.onmouseover=function() {clearInterval(MyMar)}
    tab.onmouseout=function() {MyMar=setInterval(Marquee,speed)}

    
    if(tab1.offsetHeight<=90)
    {
        tab2.style.display="none";
    }
    else
    {
        tab2.style.display="block";
    }
    
</script>