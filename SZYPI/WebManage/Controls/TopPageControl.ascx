<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPageControl.ascx.cs" Inherits="WebManage.Controls.TopPageControl" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<link href="../CSS/szypistyle.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    //设置延时
    function sb_setlayer(name, vis)
    { 
        var d = document.getElementById(name);
        d.style.display= vis?"":"none";
    }
    
    //时间参数
    var timeoutArray=new Object();

    //二级下拉菜单
    function sb_setmenu(name, vis)
    {
        if (vis)
        {
            if (timeoutArray[name])
            {
                window.clearTimeout (timeoutArray[name]);
                timeoutArray[name]=0;
            }
            sb_setlayer(name, vis);
        }
        else
        {
            timeoutArray[name] = window.setTimeout ("sb_setlayer('"+name+"', false)", 50);
        }
    } 
</script>

  <div class="top">
    <div class="top_menu">
      <ul>
        <li><a id="a12355Index" runat="server" href="http://www.sz12355.com/">网站首页</a></li>
        <li><a id="a12355Education" runat="server" href="http://www.sz12355.com/xxjy/index.aspx">学习教育</a></li>
        <li><a id="a12355Healthy" runat="server" href="http://www.sz12355.com/sxjk/index.aspx">身心健康</a></li>
        <li><a id="a12355Difficulty" runat="server" href="http://www.sz12355.com/knjz/index.aspx">困难救助</a></li>
        <li><a id="a12355Youth" runat="server" href="http://www.sz12355.com/gzdt/newslist.aspx?fl=QSNWQ">青少年维权</a></li>
        <li><a id="a12355Job" runat="server" href="http://www.sz12355.com/jycy/index.aspx">就业创业</a></li>
        <li><a id="a12355Love" runat="server" href="http://www.sz12355.com/hljy/index.aspx?fl=jypd">婚恋交友</a></li>
        <li><a id="a12355Work" runat="server" href="http://www.sz12355.com/gzdt/newslist.aspx?fl=zcfg">工作动态</a></li>
        <li><a id="a12355Volunteer" runat="server" href="http://www.sz12355.com/zyz/index.aspx">青年志愿者</a></li>
        <li><a id="a12355Tour" runat="server" href="http://www.sz12355.com/lyxx/index.aspx">旅游休闲</a></li>
        <li><a id="a12355Game" runat="server" href="http://www.sz12355.com/game/index.aspx">手机游戏</a></li>
        <li><a id="a12355BBS" runat="server" href="http://www.sz12355.com/bbs/frame.aspx">12355论坛</a></li>
      </ul>
    </div>
    <div class="banner">
      <div class="logo">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="698" height="112">
          <param name="movie" id="para1" runat="server"  value="../css/image/index/swf.swf" />
          <param name="quality" value="high" />
          <embed id="embed1" runat="server" src="../css/image/index/swf.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="698" height="112"></embed>
        </object>
      </div>
      <div class="clear"></div>
    </div>
    
    <!-- 菜单功能 -->
    <div class="menu">
      <div class="menu_left"></div>
      <div class="menu_search">
        <asp:TextBox ID="txtSearchCondition" runat="server" class="textbg" Text="请输入查询条件" onfocus="if(this.value=='请输入查询条件') this.value='';"
                                onblur="if(this.value=='') this.value='请输入查询条件';" ></asp:TextBox>
        <anthem:LinkButton ID="lbSearch" runat="server"  
              style="background:url('../CSS/image/Index/search.jpg') no-repeat; vertical-align:middle"  
              Width="72px" Height="25px" OnClientClick="SearchClick();"></anthem:LinkButton>
      </div>
      <ul class="menu_mid">
        <li>               
            <a id="aIndex" runat="server">首页</a>
        </li>
        
        <li>
            <div class="divM">
                <a href="#" id="A2" onmouseout="sb_setmenu('diaocha', false, this)" onmouseover="sb_setmenu('diaocha', true, this)">
                    调查问卷</a>
                <div class="downMenu" id="diaocha" style=" display:none;width:220px;left:-30px; position: absolute; top:20px; ">
                    <ul onmouseout="sb_setmenu('diaocha', false, this)" onmouseover="sb_setmenu('diaocha', true, this)">
                        <li  class="none"><a id="aAllSurveys" runat="server" title="查找所有问卷信息。">问卷展示</a> </li>
                        <li  class="none"><a id="aMySurveys" runat="server" title="会员自身回答过的问卷。">我的问卷</a> </li> 
                    </ul>
                </div>
            </div>
        </li>
        <li>
            <div class="divM">
                <a href="#" id="A3" onmouseout="sb_setmenu('ceshi', false, this)" onmouseover="sb_setmenu('ceshi', true, this)">
                    测评问卷</a>
                <div class="downMenu" id="ceshi" style=" display:none;width:220px;left: -30px; position: absolute; top: 20px; ">
                    <ul onmouseout="sb_setmenu('ceshi', false, this)" onmouseover="sb_setmenu('ceshi', true, this)">
                        <li  class="none"><a id="aAllTestSurveys" runat="server" title="查找所有测评信息。">测评展示</a> </li>
                        <li class="none"><a id="aMyTestSurveys" runat="server" title="会员自身参与过的测评。">我的测评</a> </li> 
                    </ul>
                </div>
          </div>
        </li>
        <li>
            <div class="divM">
                <a href="#" id="A4" onmouseout="sb_setmenu('toupiao', false, this)" onmouseover="sb_setmenu('toupiao', true, this)">
                    投票问卷</a>
                <div class="downMenu" id="toupiao" style=" display:none;width:220px;left: -30px; position: absolute; top: 20px; ">
                    <ul onmouseout="sb_setmenu('toupiao', false, this)" onmouseover="sb_setmenu('toupiao', true, this)">
                        <li class="none"><a id="aAllVoteSurveys" runat="server" title="查找所有投票信息。">投票展示</a> </li>
                        <li class="none"><a id="aMyVoteSurveys" runat="server" title="会员自身参与过的投票。">我的投票</a> </li> </ul>
                </div>
          </div>
        </li>
        <li>
            <div class="divM">
                <a href="#" id="A5" onmouseout="sb_setmenu('gifts', false, this)" onmouseover="sb_setmenu('gifts', true, this)">
                    礼品兑换</a>
                <div class="downMenu" id="gifts" style=" display:none;width:220px;left: -30px; position: absolute; top: 20px; ">
                    <ul onmouseout="sb_setmenu('gifts', false, this)" onmouseover="sb_setmenu('gifts', true, this)">
                        <li class="none"><a id="aAllGifts" runat="server" title="查看所有的礼品信息。">礼品展示</a> </li>
                        <li class="none"><a id="aGiftExchangeList" runat="server" title="查看所有会员的兑换记录。">兑换记录</a> </li> 
                    </ul>
                </div>
          </div>
        </li>
        <li>
            <div class="divM">
                <a href="#" id="ctl00_aTiyan" onmouseout="sb_setmenu('huiyuan', false, this)" onmouseover="sb_setmenu('huiyuan', true, this)">
                    会员管理</a>
                <div class="downMenu" id="huiyuan" style=" display:none;width:220px;left: -30px; position: absolute; top: 20px; ">
                    <ul onmouseout="sb_setmenu('huiyuan', false, this)" onmouseover="sb_setmenu('huiyuan', true, this)">
                        <li class="none"><a id="aHuiYuanInfo" runat="server" title="会员可维护自身的基本信息，并且查看账户余额。">我的账户</a> </li>
                        <li class="none"><a id="aGiftExchange" runat="server" title="会员可兑换礼品。">礼品兑换</a> </li> 
                        <li class="none" ><a id="aMyGiftExchangeList" runat="server" title="会员可查看自己的礼品兑换记录。">兑换记录</a> </li> 
                        <li class="none" ><a id="aMyAllSurveys" runat="server" title="会员可查看已回答过的调查问卷记录。">所有问卷</a> </li> 
                    </ul>
                </div>
          </div>
        </li>
      </ul>
      <div class="menu_right"></div>
    </div>
    <div class="user">
      <div class="user_left"></div>
      <!-- 会员登录信息 -->
      <div class="user_iframe" class="displayinfo"> 
      <input type="hidden" id="hddSrc"  runat="server" />
        <iframe id="iUserInfo" runat="server" frameBorder="0" width="100%" scrolling="no" height="34"></iframe>
      </div>     
      <div class="user_right"></div>
    </div>
    <div class="clearboth"></div>
    
  </div>
  
  <script language="javascript" type="text/javascript">
    var hid =  document.getElementById('utlTop1_hddSrc');

    if (hid.value != '')
    {
        document.getElementById('utlTop1_iUserInfo').src= hid.value;
    }
    
    
</script>