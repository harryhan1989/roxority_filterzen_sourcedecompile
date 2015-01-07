<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="left.ascx.cs" Inherits="Web.Web.Usercontrol.left" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<div class="zfxxgk">
    <div class="zfxxgk_top">
        政府信息公开 <a href="Zwgk_Left.aspx?InfoCategoryID=2" class="morelink">&gt;&gt;更多</a></div>
    <div class="zfxxgk_mid" id="divGovInfo" runat="server">
    </div>
    <div class="zfxxgk_bottom">
    </div>
</div>
<!--政策法规-->
<div class="gggs">
    <div class="gggs_top">
        政策法规 <a href="Zwgk_Left.aspx?InfoCategoryID=23" class="morelink">&gt;&gt;更多</a></div>
    <div class="zfxxgk_mid" id="divPolicy" runat="server">
    </div>
    <div class="zfxxgk_bottom">
    </div>
</div>
<!--公告公示-->
<div class="gggs">
    <div class="gggs_top">
        公告公示 <a href="Zwgk_Info.aspx?InfoCategoryID=50&type=1" class="morelink">&gt;&gt;更多</a>
    </div>
    <div class="gggs_mid" id="divNotice" runat="server">
    </div>
    <div class="gggs_bottom">
    </div>
</div>
<!--便民服务-->
<div class="bmfw">
    <div class="bmfw_top">
        便民服务 <a href="InfoList.aspx?InfoCategoryID=3" class="morelink">&gt;&gt;更多</a>
    </div>
    <div class="bmfw_mid" id="divService" runat="server">
    </div>
    <div class="bmfw_bottom">
    </div>
</div>
<!--公众监督-->
<div class="gzjd">
    <div class="gzjd_top">
        公众监督</div>
    <div class="gzjd_mid">
        <div class="mid_bg">
            <a href="SuggestionConsults.aspx?MessageType=2" target="_self">在线咨询 </a>| <a target="_self"
                href="SuggestionConsults.aspx?MessageType=4">举报投诉 </a>| <a target="_self" href="SuggestionConsults.aspx?MessageType=3">
                    意见建议</a></div>
    </div>
    <div class="gzjd_bottom">
    </div>
</div>
<!--局长信箱-->
<div class="jzxx">
    <a href="SuggestionConsults.aspx?MessageType=1" target="_self">
        <img src="images/newbg_1_11.jpg" width="257" height="52" border="0" /></a></div>
<!--印刷行业-->

<!--光盘复制业-->

<!--在线调查-->
<div class="zxdc">
    <div class="zxdc_top">
        在线调查</div>
    <div class="zxdc_mid">
        <div class="mid_bg">
            <div class="zxdc_mid_title">
                <strong>您在本站中最关注什么版块？</strong>
            </div>
            <div class="zxdc_mid_content">
                <span class="dc">
                    <label>
                        <input id="cbNews" type="checkbox" runat="server" />
                    </label>
                    文化动态<label>
                        <input id="cbLive" type="checkbox" runat="server" />
                    </label>
                    文化民生</span> <span class="dc">
                        <label>
                            <input id="cbIndustry" type="checkbox" runat="server" />
                        </label>
                        文化产业
                        <label>
                            <input id="cbHeritage" type="checkbox" runat="server" />
                        </label>
                        文化遗产</span> <span class="dc">
                            <label>
                                <input id="cbRadioTV" type="checkbox" runat="server" />
                            </label>
                            广播影视
                            <label>
                                <input id="cbPublic" type="checkbox" runat="server" />
                            </label>
                            新闻出版</span> <span class="dc">
                                <label>
                                    <input id="cbCreate" type="checkbox" runat="server" />
                                    文化艺术</label><label>
                                        <input id="cbMarket" type="checkbox" runat="server" />
                                        文化市场</label>
                            </span><span class="dc">
                                <label>
                                    <input id="cbExchange" type="checkbox" runat="server" />
                                </label>
                                文化交流
                                <label>
                                    <input id="cbTalent" type="checkbox" runat="server" />
                                </label>
                                文化人才</span> <span class="dc">
                                    <label>
                                        <input id="cbStudy" type="checkbox" runat="server" />
                                    </label>
                                    文化研究<label>
                                        <input id="cbTopic" type="checkbox" runat="server" />
                                    </label>
                                    文化专题 </span><span class="dc0">
                                        <anthem:Button ID="bntSubmit" Text="提交" runat="server" OnClick="bntSubmit_Click"
                                            CssClass="btn1" />
                                        &nbsp;
                                        <anthem:Button ID="btnSearch" Text="查看投票结果" runat="server" CssClass="btn2"></anthem:Button>
                                    </span>
            </div>
            <div class="zxdc_mid_button">
            </div>
        </div>
    </div>
    <div class="zxdc_bottom">
    </div>
</div>
<!--流量统计-->
<div class="lltj">
    <div class="lltj_top">
        流量统计</div>
    <div class="lltj_mid">
        <div class="mid_bg">
            开站日期：<asp:Label ID="lblStartDate" runat="server" Text="" /><br />
            <br />
            总访问量：<anthem:Label ID="lblCount" runat="server" CssClass="redtxt"></anthem:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            今日访问：<anthem:Label ID="lblDayCount" runat="server" CssClass="redtxt"></anthem:Label><br />
            <%--<br />
            <br />
            <br />--%>
            <%--日均访问：<anthem:Label ID="lblAveCount" runat="server" CssClass="redtxt"></anthem:Label><br />
            <br />
            文 章 数：<anthem:Label ID="lblAticle" runat="server" Text="" CssClass="redtxt" />--%></div>
    </div>
  
    <div class="lltj_bottom">
    </div>
</div>

<!--友情连接-->
<div class="gggs">
    <div class="gggs_top">
        友情链接 </div>
    <div class="friend_mid" id="div1" runat="server">
    <span class="link">
            <asp:DropDownList ID="ddlDept" Width="190px" runat="server" style="height:21px;">
            </asp:DropDownList>
        </span><span class="link">
            <asp:DropDownList ID="ddlCulture" Width="190px" runat="server" style="height:21px;">
            </asp:DropDownList>
        </span><span class="link">
            <asp:DropDownList ID="ddlFriend" Width="190px" runat="server" style="height:21px;">
            </asp:DropDownList>
        </span><span class="link">
            <asp:DropDownList ID="ddlBasic" Width="190px" runat="server" style="height:21px;">
            </asp:DropDownList>
        </span><span class="link">
            <asp:DropDownList ID="ddlMedia" Width="190px" runat="server" style="height:21px;">
            </asp:DropDownList>
        </span>
      
    </div>
    <div class="zfxxgk_bottom">
    </div>
</div>



<script type="text/javascript">
    function OpenFriendshipUrl(ddlFriendship) {
        if (ddlFriendship) {
            if (document.all[ddlFriendship]) {
                var url = document.all[ddlFriendship].value;

                if (url != "0") {
                    window.open(url);
                }
            }
        }
    }

    function OpenUrl(strUrl) {
        if (strUrl) {
            var url = "OnlineResearch.aspx";
            if (url) {

                var awidth = 650; //窗口宽度,需要设置

                var aheight = 520; //窗口高度,需要设置 

                var atop = (screen.availHeight - aheight) / 2; //窗口顶部位置,一般不需要改

                var aleft = (screen.availWidth - awidth) / 2; //窗口放中央,一般不需要改

                var param0 = "scrollbars=0,status=0,menubar=0,resizable=2,location=0"; //新窗口的参数

                var params = "top=" + atop + ",left=" + aleft + ",width=" + awidth + ",height=" + aheight + "," + param0;


                win = window.open(url, "投票结果", params); //打开新窗口

                win.focus(); //新窗口获得焦点

            }
        }
    }
</script>

