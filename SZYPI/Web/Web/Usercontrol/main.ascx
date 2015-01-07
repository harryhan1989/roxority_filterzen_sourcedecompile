<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="main.ascx.cs" Inherits="Web.Web.Usercontrol.main" %>

<script type="text/javascript" language="javascript">

    //图片新闻 （多采活动）
    function getFlashNews(sPicUrl, sLinks, sTexts) {
        var focus_width = 256;     //图片框的宽度
        var focus_height = 180;    //图片框的高度 
        var text_height = 18;      //文字的高度 
        var swf_height = focus_height + text_height;   //flash的高度 

        var pics = sPicUrl;    //图片文件的的地址，以“|”为分界 
        var links = sLinks;    //文字连接的地址，以“|”分界，注意，这个变量是空的时候，就是没有连接哦 
        var texts = sTexts;    //连接文字

        document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
        document.write('<param name="allowscriptAccess" value="sameDomain"><param name="movie" value="pixviewer.swf"><param name="quality" value="high"><param name="bgcolor" value="#F0F0F0">');
        document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
        document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
        document.write('<embed src="pixviewer.swf" wmode="opaque" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#F0F0F0" quality="high" width="' + focus_width + '" height="' + focus_height + '" allowscriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>');
    }
</script>

<!--文化动态-->
<div class="whdt">
    <div class="whdt_top">
        文化动态</div>
    <div class="whdt_content">
        <div class="whdt_left" id="divPicNews" runat="server">
            <%--<img width="256" height="180" /><span class="newstitle">我局召开文广新系统</span>--%></div>
        <div class="whdt_right" id="divWhdtRight" runat="server">
        </div>
    </div>
</div>
<div class="whcy">
    <div class="whcy_top">
        <span class="whcy_title">文化产业</span><span class="whcy_more"><a href="InfoList.aspx?InfoCategoryID=7">>>更多</a></span></div>
    <div class="whcy_content">
        <div class="whcy_blank">
            <span class="whdt_left" id="imgWhcy" runat="server"></span><span class="whcy_title0"
                id="topWhcyTitle" runat="server"></span>
            <div class="whcy_tcontent" id="topWhcyContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhcyNews" runat="server">
        </div>
    </div>
</div>
<div class="whyc">
    <div class="whyc_top">
        <span class="whyc_title">文化遗产</span><span class="whyc_more"><a href="InfoList.aspx?InfoCategoryID=8">>>更多</a></span></div>
    <div class="whyc_content">
        <div class="whyc_blank">
            <span class="whdt_left" id="imgWhyc" runat="server"></span><span class="whyc_title0"
                id="topWhycTitle" runat="server"></span>
            <div class="whyc_tcontent" id="topWhycContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhycNews" runat="server">
        </div>
    </div>
</div>
<!--广播影视-->
<div class="whcy">
    <div class="whcy_top">
        <span class="whcy_title">广播影视</span><span class="whcy_more"><a href="InfoList.aspx?InfoCategoryID=9">>>更多</a></span></div>
    <div class="whcy_content">
        <div class="whcy_blank">
            <span class="whdt_left" id="imgGbys" runat="server"></span><span class="whcy_title0"
                id="topGbysTitle" runat="server"></span>
            <div class="whcy_tcontent" id="topGbysContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divGbysNews" runat="server">
        </div>
    </div>
</div>
<!--新闻出版-->
<div class="whyc">
    <div class="whyc_top">
        <span class="whyc_title">新闻出版</span><span class="whyc_more"><a href="InfoList.aspx?InfoCategoryID=10">>>更多</a></span></div>
    <div class="whyc_content">
        <div class="whyc_blank">
            <span class="whdt_left" id="imgXwcb" runat="server"></span><span class="whyc_title0"
                id="topXwcbTitle" runat="server"></span>
            <div class="whyc_tcontent" id="topXwcbContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divXwcbNews" runat="server">
        </div>
    </div>
</div>
<!--文化专题-->
<div class="whzt">
    <div class="whzt_top">
        文化专题</div>
    <div class="whzt_content" id="divTopic" runat="server">
    </div>
</div>
<!---->
<!--文化创作-->
<div class="whcy">
    <div class="whcy_top">
        <span class="whcy_title">文化创作</span><span class="whcy_more"><a href="InfoList.aspx?InfoCategoryID=11">>>更多</a></span></div>
    <div class="whcy_content">
        <div class="whcy_blank">
            <span class="whdt_left" id="imgWhcz" runat="server"></span><span class="whcy_title0"
                id="topWhczTitle" runat="server"></span>
            <div class="whcy_tcontent" id="topWhczContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhczNews" runat="server">
        </div>
    </div>
</div>
<!--文化市场-->
<div class="whyc">
    <div class="whyc_top">
        <span class="whyc_title">文化市场</span><span class="whyc_more"><a href="InfoList.aspx?InfoCategoryID=12">>>更多</a></span></div>
    <div class="whyc_content">
        <div class="whyc_blank">
            <span class="whdt_left" id="imgWhsc" runat="server"></span><span class="whyc_title0"
                id="topWhscTitle" runat="server"></span>
            <div class="whyc_tcontent" id="topWhscContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhscNews" runat="server">
        </div>
    </div>
</div>
<!--文化交流-->
<div class="whcy">
    <div class="whcy_top">
        <span class="whcy_title">文化交流</span><span class="whcy_more"><a href="InfoList.aspx?InfoCategoryID=13">>>更多</a></span></div>
    <div class="whcy_content">
        <div class="whcy_blank">
            <span class="whdt_left" id="imgWhjl" runat="server"></span><span class="whcy_title0"
                id="topWhjlTitle" runat="server"></span>
            <div class="whcy_tcontent" id="topWhjlContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhjlNews" runat="server">
        </div>
    </div>
</div>
<!--文化人才-->
<div class="whyc">
    <div class="whyc_top">
        <span class="whyc_title">文化人才</span><span class="whyc_more"><a href="InfoList.aspx?InfoCategoryID=14">>>更多</a></span></div>
    <div class="whyc_content">
        <div class="whyc_blank">
            <span class="whdt_left" id="imgWhrc" runat="server"></span><span class="whyc_title0"
                id="topWhrcTitle" runat="server"></span>
            <div class="whyc_tcontent" id="topWhrcContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhrcNews" runat="server">
        </div>
    </div>
</div>
<!--文化研究-->
<div class="whcy">
    <div class="whcy_top">
        <span class="whcy_title">文化研究</span><span class="whcy_more"><a href="InfoList.aspx?InfoCategoryID=15">>>更多</a></span></div>
    <div class="whcy_content">
        <div class="whcy_blank">
            <span class="whdt_left" id="imgWhyj" runat="server"></span><span class="whcy_title0"
                id="topWhyjTitle" runat="server"></span>
            <div class="whcy_tcontent" id="topWhyjContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhyjNews" runat="server">
        </div>
    </div>
</div>
<!--文化民生-->
<div class="whyc">
    <div class="whyc_top">
        <span class="whyc_title">文化民生</span><span class="whyc_more"><a href="InfoList.aspx?InfoCategoryID=6">>>更多</a></span></div>
    <div class="whyc_content">
        <div class="whyc_blank">
            <span class="whdt_left" id="imgWhms" runat="server"></span><span class="whyc_title0"
                id="topWhmsTitle" runat="server"></span>
            <div class="whyc_tcontent" id="topWhmsContent" runat="server">
            </div>
        </div>
        <div class="whcy_news" id="divWhmsNews" runat="server">
        </div>
    </div>
</div>
