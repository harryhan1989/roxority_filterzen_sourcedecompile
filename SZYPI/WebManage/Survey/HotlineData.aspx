<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotlineData.aspx.cs" Inherits="WebManage.Survey.HotlineData" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>热线数据管理</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" charset="gbk" src="Js/x_open.js"></script>

    <script language="javascript" type="text/javascript" src="../Survey/Js/InterFace.js"></script>

    <style>
        .StaticMenuStyle
        {
            float: left;
        }
        .cut
        {
            display: block;
            width: 100px;
            overflow: hidden;
            white-space: nowrap;
            -o-text-overflow: ellipsis;
            text-overflow: ellipsis;
        }
    </style>

    <script type="text/javascript">
        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 400;
            var height = 300;
            var closeid = "HuiYuanEdit";

            switch (toolbarItem.value) {
                case "Import":
                    //                    location.href = "../Web/Hotline/ImportPayedHotLine.aspx";
                    initFace();
                    x_open('导入数据', '../Web/Hotline/ImportPayedHotLine.aspx', 500, 200);
                    toolbarItem.needPostBack = false;
                    break;
            }
        }

        function $(thisid) {
            return document.getElementById(thisid);
        }

        function initFace() {
//            var face = new interface();
//            face._MaskStyleName = "BgWin";
//            face._initMask($("BgWin"), "#666666");
//            face._openMask($("BgWin"));
            //                       document.getElementById("1").style.filter="Alpha(opacity=30)"
//            $("BgWin").style.filter = "Alpha(opacity=30)";
        }
        function refresh() {
//            $("BgWin").style.display = 'none';
        }
    </script>

</head>
<body style="background: #FFF; height: 100%;">
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="3" style="width: 100%; height: 80%;">
        <tr>
            <td>
                <ND:NDToolbar StaticMenuStyle-CssClass="StaticMenuStyle" ID="toolbar" runat="server"
                    OnMenuItemClick="toolbar_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="导入热线数据" Value="Import" ImageUrl="../web/Images/icon/page_white_excel.png">
                        </asp:MenuItem>
                    </Items>
                </ND:NDToolbar>
            </td>
        </tr>
        <tr style="height: 80%;">
            <td>
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="Name" AllowSorting="True" OnRowCommand="grid_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                            <HeaderStyle Width="25px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" dataID='<%# Eval("ID") %>' onclick="SetIsAllChecked('grid_ctl01_chkItemAll', this, 'chkItem');">
                                </asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="来电人姓名">
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="cut" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sex" HeaderText="性别">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AgeRange" HeaderText="年龄段">
                            <ItemStyle Width="100px" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MarriageStatus" HeaderText="婚否">
                            <ItemStyle Width="100px" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="District" HeaderText="所属地区">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VisitorType" HeaderText="来电人类型">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ConsultType" HeaderText="咨询类别">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OpinionType" HeaderText="处理意见">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ConsultContent" HeaderText="来电内容">
                            <ItemStyle Width="120px" HorizontalAlign="Left" CssClass="cut" />
                            <HeaderStyle Width="100px" CssClass="cut" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OpinionContent" HeaderText="处理内容">
                            <ItemStyle Width="120px" HorizontalAlign="Left" CssClass="cut" />
                            <HeaderStyle Width="100px" CssClass="cut" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle Height="21px" />
                    <HeaderStyle Height="21px" />
                </ND:NDGridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <ND:NDGridViewPager ID="viewpage1" runat="server" OnPageChanged="pager_PageChanged"
                    Width="100%" Height="25px" ShowCustomInfoSection="Left" CustomInfoSectionWidth="80%"
                    FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" PageSize="10"
                    PagingButtonSpacing="8px" NumericButtonCount="8" NumericButtonTextFormatString="[{0}]"
                    AlwaysShow="True" ShowInputBox="Always" ShowSelectBox="Always" InputBoxStyle="WIDTH: 60px"
                    SubmitButtonText="Go !">
                </ND:NDGridViewPager>
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Width="0px" />
        <input id="hidInfo" runat="server" type="hidden" />
    </div>
    </form>
    <div id="BgWin" style="position: absolute; top: 0px; background-color: #000000; z-index: 2;
        left: 0px; display: none" class="BgWin">
    </div>
</body>
</html>
