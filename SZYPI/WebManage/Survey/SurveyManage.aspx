<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyManage.aspx.cs" Inherits="WebManage.Survey.SurveyManage" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调查管理</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Js/surveylist.js"></script>

    <script type="text/javascript">

        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 400;
            var height = 300;
            var closeid = "HuiYuanEdit";
            var selectCount = IsCheckedSingle();
            switch (toolbarItem.value) {
                case "Repeal":
                    if (IsChecked() == false) {
                        alert("请选择需撤销生成的问卷！");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    toolbarItem.needPostBack = confirm('确认要撤销发布所选问卷吗？');
                    break;
                case "Pass":
                    if (selectCount == 0) {
                        alert("请选择审批问卷");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Invalid":
                    if (selectCount == 0) {
                        alert("请选择审批问卷");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "ReInput":
                    if (selectCount == 0) {
                        alert("请选择问卷");
                        toolbarItem.needPostBack = false;
                    }
                    break;

            }
        }
        function checkInput() {
            var dateStart;
            var dataEnd;
            dateStart = document.getElementById("wdcBeginDate").value
            dataEnd = document.getElementById("wdcEndDate").value
            if (new Date(dateStart.split("-").join("/")) > new Date(dataEnd.split("-").join("/"))) {
                alert("输入的创建日期范围开始需大于结束!");
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        function showMenuAll() {
            var cols = this.top.frames[0].frames['mainFrameset'].cols;
            //		        if (cols == "0,0,*") {
            //		            cols = "180,5,*";
            //		            this.top.frames[0].frames['topFrame'].document.getElementById("divMenuControl").innerText = "隐藏菜单";
            //		            showMinindex();
            //		        }
            //		        else {
            cols = "0,0,*";
            this.top.frames[0].frames['topFrame'].document.getElementById("divMenuControl").innerText = "显示菜单";
            showMaxindex();
            //		        }
            this.top.frames[0].frames['mainFrameset'].cols = cols;
        }

        //显示菜单后首页
        function showMinindex() {
            if (window.parent.frames["mainFrame"].document.getElementById("tbAll") != null) {
                window.parent.frames["mainFrame"].document.getElementById("tbAll").style.width = '836px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg1").style.width = '368px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg2").style.width = '368px';
                window.parent.frames["mainFrame"].document.getElementById("tdTime").style.width = '178px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn1").style.width = '245px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn2").style.width = '270px';
                window.parent.frames["mainFrame"].document.getElementById("tdsize").style.width = '98px';

                window.parent.frames["mainFrame"].document.getElementById("hidAll").value = '836px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg1").value = '368px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg2").value = '368px';
                window.parent.frames["mainFrame"].document.getElementById("hidTime").value = '178px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn1").value = '245px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn2").value = '270px';
                window.parent.frames["mainFrame"].document.getElementById("hidsize").value = '98px';
            }
        }

        //隐藏菜单后首页
        function showMaxindex() {
            if (window.parent.frames["mainFrame"].document.getElementById("tbAll") != null) {
                window.parent.frames["mainFrame"].document.getElementById("tbAll").style.width = '1020px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg1").style.width = '485px';
                window.parent.frames["mainFrame"].document.getElementById("tbtoplinebg2").style.width = '482px';
                window.parent.frames["mainFrame"].document.getElementById("tdTime").style.width = '180px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn1").style.width = '260px';
                window.parent.frames["mainFrame"].document.getElementById("tdbtn2").style.width = '240px';
                window.parent.frames["mainFrame"].document.getElementById("tdsize").style.width = '301px';

                window.parent.frames["mainFrame"].document.getElementById("hidAll").value = '1020px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg1").value = '485px';
                window.parent.frames["mainFrame"].document.getElementById("hidtoplinebg2").value = '482px';
                window.parent.frames["mainFrame"].document.getElementById("hidTime").value = '180px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn1").value = '260px';
                window.parent.frames["mainFrame"].document.getElementById("hidbtn2").value = '240px';
                window.parent.frames["mainFrame"].document.getElementById("hidsize").value = '301px';
            }
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
                        <asp:MenuItem Text="撤销生成问卷" Value="Repeal" ImageUrl="../web/Images/icon/Delete.gif">
                        </asp:MenuItem>
<%--                        <asp:MenuItem Text="审批通过" Value="Pass" ImageUrl="../web/Images/icon/accept.png">
                        </asp:MenuItem>
                        <asp:MenuItem Text="审批退回" Value="Invalid" ImageUrl="../web/Images/icon/arrow_undo.png">
                        </asp:MenuItem>
                        <asp:MenuItem Text="重新提交审批" Value="ReInput" ImageUrl="../web/Images/icon/arrow_undo.png">
                        </asp:MenuItem>--%>
                    </Items>
                </ND:NDToolbar>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            问卷名称：
                        </td>
                        <td>
                            <asp:TextBox ID="txtSurveyName" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            编辑状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server">
                                <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="未生成"></asp:ListItem>
                                <asp:ListItem Value="1" Text="已生成"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            活动状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlActive" runat="server">
                                <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="80px" rowspan="2">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="button70" OnClientClick="return checkInput();"
                                OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            问卷类型：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSurveyClass" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            创建时间：
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="wdcBeginDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                                MinDate="2000-02-19" NullDateLabel="">
                            </igsch:WebDateChooser>
                        </td>
                        <td>
                            至
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="wdcEndDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                                MinDate="2000-02-19" NullDateLabel="">
                            </igsch:WebDateChooser>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 80%;">
            <td>
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="SID,State,Active,SurveyName,AnswerAmount,ApprovalStaus"
                    AllowSorting="True" OnRowCommand="grid_RowCommand">
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
                        <asp:ButtonField CommandName="SurveyName" HeaderText="问卷名称">
                            <ItemStyle Width="11%" HorizontalAlign="Left" />
                            <HeaderStyle Width="11%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="AnswerAmount" HeaderText="回收数">
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle Width="8%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="State" HeaderText="编辑状态">
                            <ItemStyle Width="9%" />
                            <HeaderStyle Width="9%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="Active" HeaderText="活动状态">
                            <ItemStyle Width="9%" />
                            <HeaderStyle Width="9%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="ApprovalStaus" HeaderText="审核状态">
                            <ItemStyle Width="8%" />
                            <HeaderStyle Width="8%" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="SurveyClassName" HeaderText="问卷类型">
                            <ItemStyle Width="9%" HorizontalAlign="Center" />
                            <HeaderStyle Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreateDate" HeaderText="创建时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Width="9%" HorizontalAlign="Center" />
                            <HeaderStyle Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Point" HeaderText="积分值">
                            <ItemStyle Width="7%" />
                            <HeaderStyle Width="7%" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="Statistics" HeaderText="分析">
                            <ItemStyle Width="7%" />
                            <HeaderStyle Width="7%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="Options" HeaderText="选项">
                            <ItemStyle Width="7%" />
                            <HeaderStyle Width="7%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="Editer" HeaderText="编辑">
                            <ItemStyle Width="7%" />
                            <HeaderStyle Width="7%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="DeleteSurvey" HeaderText="删除">
                            <ItemStyle Width="7%" />
                            <HeaderStyle Width="7%" />
                        </asp:ButtonField>
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
</body>
</html>
