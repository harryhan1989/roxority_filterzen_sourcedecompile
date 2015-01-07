<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebManage.Index" %>

<%@ Register TagPrefix="uc1" TagName="utlDiaoCha" Src="~/Controls/utlDiaoCha.ascx" %>
<%@ Register TagPrefix="top1" TagName="utlTop" Src="~/Controls/TopPageControl.ascx" %>
<%@ Register TagPrefix="button1" TagName="utlButton" Src="~/Controls/ButtonPageControl.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>在线调查</title>
<link href="CSS/szypistyle.css" type="text/css" rel="stylesheet" />

</head>
<body>
<form runat="server">
<div id="container">
 <!--页眉-->
<top1:utlTop id="utlTop1" runat="server"></top1:utlTop>

  <div class="main">
    <!-- 问卷列表 -->
    <div class="left">
      <div class="left_top"><a href="Web/Survey/AllSurveysPage.aspx?SurveyType=-1">更多..</a></div>
      <div class="left_mid" style="height:606px;">
       <asp:Repeater ID="rptSurveyMain" runat="server" >
        <ItemTemplate>
            <div class="flagleft"><a href="#" onclick='window.open("Survey/userdata/u1/s" + <%# DataBinder.Eval(Container.DataItem,"sid") %> + ".aspx")' > <%# Eval("SurveyName")%></a></div>
            <div class="wj_left"><%# Eval("PageContent")%></div>
            <div class="admin_uer">
                <%# Eval("UserName")%>发布于<%# Eval("CreateDate") %>
                <span class="widthspace0"></span>
                <span class="flag0"></span>
               <%-- <a href="#">填写人数(<%# Eval("AnswerAmount")%>)</a>--%>
               <span >填写人数(<%# Eval("AnswerAmount")%>)</span>
                <span style="color:Red; font-weight:bold;">赠送积分<%# Eval("Point") %>分</span>
                <span style="color:Blue; font-weight:bold;"><%# Eval("HasAnswered")%></span>
            </div>
            
            <hr>
        </ItemTemplate>
        </asp:Repeater>
        
      </div>
      <div class="left_bottom"></div>
    </div>
    <div class="right">
      <!--热门问卷-->
      <div class="hot_wj">
        <div class="hot_wjtop">热门问卷</div>
        <div class="hot_wjmid">
          <ul>
          <asp:Repeater ID="rptHotSurvey" runat="server" >
              <ItemTemplate>
                <li>·<a href="#" onclick='window.open("Survey/userdata/u1/s" + <%# DataBinder.Eval(Container.DataItem,"sid") %> + ".aspx")'><%# Eval("SurveyName")%></a></li>
              </ItemTemplate>
          </asp:Repeater>
          </ul>
        </div>
        <div class="hot_wjbottom"></div>
      </div>
      
      <!--推荐问卷-->
      <div class="tj_wj">
        <div class="tj_wjtop">推荐问卷</div>
        <div class="tj_wjmid">
          <div class="tj_title"><asp:Label ID="lblRecommendSurveyTitle" runat="server" ></asp:Label></div>
          <div class="wz"><asp:Label ID="lblRecommendSurveyContent" runat="server"></asp:Label></div>
          <!--<div class="tt"><img src="CSS/image/Index/arrow.jpg" width="90" height="70" /></div>-->
          <%--<div class="clearboth"></div>--%>
          <div class="zsjf"><asp:Label ID="lblRecommendSurveyPoint" runat="server"></asp:Label></div>
          <div class="detial_but"></div>
        </div>
        <div class="tj_wjbottom"></div>
      </div>
      <div class="xdc">
        <div class="xdc_top">小调查</div>
       <div class="xdc_mid" style="height:140px">
        <uc1:utldiaocha id="utldiaocha1" runat="server"></uc1:utldiaocha>
        <%--正在建设中，敬请期待。。。--%>
       </div>
        
         <div class="xdc_but">
          <input type="image" src="CSS/image/Index/detail.jpg" onclick="return OpenXiaoDiaoCha();" width="101px" height="31px" />
        </div>
        <div class="tj_wjbottom"></div>
      </div>
    </div>
    <div class="clearboth"></div>
    <div class="lpdh">
      <div class="lpdh_top"><a href="Web/Gifts/Show.aspx">更多..</a></div>
      <div class="lpdh_mid">
      
      <!--正在建设中，敬请期待。。。-->
      <!--礼品兑换区-->
        <div class="lpdh_left">
          <div class="lpdh_left_top"></div>
          <div class="lpdh_left_mid">
            <ul style="text-align:left">
            <asp:Repeater ID="rGiftsShow" runat="server" >
            <ItemTemplate>
              <li style="width:140px; white-space:normal ; "><img src='Web/Gifts/ImagePhoto.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID") %>' onclick="window.location.href='web/Gifts/exchangeapply.aspx?giftguid=<%# DataBinder.Eval(Container.DataItem,"ID") %>';" width="140" height="100" style="cursor:pointer" />
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
		        <asp:Repeater ID="rExchangeHistory" runat="server" >
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
			          <div class="jft"><img src="Web/Gifts/ImagePhoto.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID") %>" onclick="window.location.href='web/Gifts/exchangeapply.aspx?giftguid=<%# DataBinder.Eval(Container.DataItem,"ID") %>';" width="100" height="78" style="cursor:pointer"/></div>
                        <div class="wp0"><span class="jif"><%# DataBinder.Eval(Container.DataItem, "NeedPoint")%></span>积分<br />
                          <%# DataBinder.Eval(Container.DataItem, "GiftName")%></div>
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
	
    <div class="tjfq">
        <div class="tjfq_top"><a href="#">更多..</a></div>
        <div class="tjfq_mid">
	    <ul>
	        <!--
	        <li><img src="CSS/image/Index/linkt.jpg" width="180" height="166" /></li>
	        <li><img src="CSS/image/Index/linkt.jpg" width="180" height="166" /></li>
	        -->
	        <li>
	        <div class="linkcontent" id="chart0" style="overflow: hidden; height: 366px; width: 99%">
	            <div id="chartIn" style="float: left; width: 1600%">
	                <div id="chart1" runat="server" style="float: left">
	                    <div id="chartContent1" width="180" height="166" style="float:left;" runat="server">
                        </div>
                    
	                    <div id="chartContent2" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent3" width="180" height="166" style="float:left" runat="server">
                        </div>
                        
                        <div id="chartContent4" width="500" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent5" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent6" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent7" width="500" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent8" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent9" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent10" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent11" width="180" height="166" style="float:left;" runat="server">
                        </div>
                        
                        <div id="chartContent12" width="180" height="166" style="float:left" runat="server">
                        </div>
                    </div>
                    <div id="chart2" style="float: left">
                    </div>
                </div>
            </div>
            </li>
	    <!--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;正在建设中，敬请期待。。。-->
	    </ul>
	<div class="clearboth"></div>
	</div>
      <div class="tjfq_bottom"></div>
    </div>
	<div class="tjfq">
      <div class="link_top"></div>
      <div class="link_mid">
	  <ul>
	      <asp:Repeater ID="rPartner" runat="server">
	          <ItemTemplate>
	            <li>
	                <img src="Web/SysManager/ImagePhoto.aspx?ID=<%# DataBinder.Eval(Container.DataItem,"ID") %>" 
	                onclick='window.open(" <%# DataBinder.Eval(Container.DataItem,"URL") %>  ")' 
	                alt="<%# DataBinder.Eval(Container.DataItem,"Name") %>"
	                width="150px" height="40px" />
	            </li>
	          </ItemTemplate>
	      </asp:Repeater>	  
	  </ul>
        <div class="clearboth">
		
		</div>
	</div>
      <div class="tjfq_bottom"></div>
    </div>
	
  </div>
    <!--页脚-->
    <button1:utlButton id="utlButton" runat="server"></button1:utlButton>
</div>
</form>
</body>
</html>

<script type="text/javascript">
    //搜索按钮点击事件
    function SearchClick()
    {
        var keyCode = document.getElementById("utlTop1$txtSearchCondition").value;
        
        //return Anthem_InvokePageMethod('SearchClick', [keyCode], null);        
        
        window.location.href="Web/Survey/AllSurveysPage.aspx?KeyCode=" + keyCode;
    }
    
    function OpenXiaoDiaoCha()
    {
        var width=300;
        var height=400;
        var left=(window.screen.width-width)/2;
        var top=(window.screen.height-height)/2;
        
        window.showModalDialog('Web/Diaocha/XiaoDiaoCha.aspx',null,
        "dialogWidth="+width.toString()+"px;dialogHeight="+height.toString()+"px;dialogTop="+top.toString()+"px;dialogLeft="+left.toString()+"px;");
        
        return false;
    }
    
    /*兑换记录滚动效果*/
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


    /*统计图滚动效果*/
    var ChartSpeed = 10;  //调整滚动速度
    var Charttab = document.getElementById("chart0");
    var Charttab1 = document.getElementById("chart1");
    var Charttab2 = document.getElementById("chart2");
    Charttab2.innerHTML = Charttab1.innerHTML;
    function ChartMarquee() {
        if (Charttab2.offsetWidth - Charttab.scrollLeft <= 0)
            Charttab.scrollLeft -= Charttab1.offsetWidth;
        else {
            Charttab.scrollLeft++;
        }
    }
    var ChartMyMar = setInterval(ChartMarquee, ChartSpeed);
    Charttab.onmouseover = function() { clearInterval(ChartMyMar) };
    Charttab.onmouseout = function() { ChartMyMar = setInterval(ChartMarquee, ChartSpeed) };
</script>