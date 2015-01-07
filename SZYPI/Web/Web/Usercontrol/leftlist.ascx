<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="leftlist.ascx.cs" Inherits="Web.Web.Usercontrol.leftlist" %>
<!--存在子项的层-->
<div class="list_left" id="listLeft" runat="server">
     <div class="list_lefttop">
        <div class="list_parent" id="leftParent" runat="server">
        </div>
    </div>
    <div class="list_leftmid">
        <ul class="list_ul" id="leftName" runat="server">
        </ul>
    </div>
    <div class="list_leftbot">
    </div>
</div>

<div id="noticeLeft" runat="server">
    <!--公告公示-->
    <div class="zfxxgk">
        <div class="zfxxgk_top">
             公告公示
            <a href="Zwgk_Left.aspx?InfoCategoryID=50" class="morelink">&gt;&gt;更多</a></div>
        <div class="zfxxgk_mid" id="divNotice" runat="server">
        </div>
        <div class="zfxxgk_bottom">
        </div>
    </div>
    <!--便民服务-->
    <div class="gggs">
        <div class="gggs_top">
            便民服务<a href="InfoList.aspx?InfoCategoryID=3" class="morelink">&gt;&gt;更多</a>
            </div>
        <div class="gggs_mid" id="divService" runat="server">
        </div>
        <div class="gggs_bottom">
        </div>
    </div>
</div>
