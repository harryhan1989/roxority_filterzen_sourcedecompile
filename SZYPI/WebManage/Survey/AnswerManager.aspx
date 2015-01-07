<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnswerManager.aspx.cs"
    Inherits="WebManage.Survey.AnswerManager" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>答卷管理</title>
    <link href="../../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Js/surveylist.js"></script>

    <script type="text/javascript">
        //工具栏按钮点击事件
        function toolbar_Click(toolbarItem) {
            var width = 400;
            var height = 300;
            var closeid = "HuiYuanEdit";

            switch (toolbarItem.value) {
                case "Add":
                    showModalWindow(closeid, "添加", width, height, "../../Web/HuiYuan/Edit.aspx?Operation=1");
                    toolbarItem.needPostBack = false;
                    break;
                case "Update":
                    var selectCount = IsCheckedSingle();
                    if (selectCount == 1) {
                        var id = GetCheckedItemID();
                        if (id == null) {
                            alert("请选择要修改的记录！");
                            toolbarItem.needPostBack = false;
                            return;
                        }
                        showModalWindow(closeid, "修改", width, height, "../../Web/HuiYuan/Edit.aspx?Operation=2&ID=" + id + "");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    else if (selectCount == 0) {
                        alert("请选择修改的记录");
                        toolbarItem.needPostBack = false;
                    }
                    else {
                        alert("修改只能选择一条记录！");
                        toolbarItem.needPostBack = false;
                    }
                    break;
                case "Delete":
                    if (IsChecked() == false) {
                        alert("请选择删除的记录！");
                        toolbarItem.needPostBack = false;
                        return;
                    }
                    toolbarItem.needPostBack = confirm('确认要删除选择的记录吗？');
                    break;
                case "Pass":
                    if (selectCount == 0) {
                        alert("请选择记录");
                        toolbarItem.needPostBack = false;
                    }
                case "Invalid":
                    if (selectCount == 0) {
                        alert("请选择记录");
                        toolbarItem.needPostBack = false;
                    }

            }
        }

        function redirect() {
            location.href = 'SurveyManage.aspx';
            return false;
        }

        function checkInput() {
            var minTime;
            var maxTime;

            var minRecord;
            var maxRecord;

            var dateStart;
            var dataEnd;

            minTime =parseInt(document.getElementById("minTime").value);
            maxTime = parseInt(document.getElementById("maxTime").value);

            minRecord = parseInt(document.getElementById("minRecord").value);
            maxRecord = parseInt(document.getElementById("maxRecord").value);

            dateStart = document.getElementById("startDate").value;
            dataEnd = document.getElementById("endDate").value;
            
            if (minTime > maxTime) {
                alert("最小答题时间需小于最大答题时间!");
                return false;
            }
            if (minRecord > maxRecord) {
                alert("最小得分需小于最大得分!");
                return false;
            }
            if (new Date(dateStart.split("-").join("/")) > new Date(dataEnd.split("-").join("/"))) {
                alert("输入的创建日期范围开始需大于结束!");
                return false;
            }

            return true;
        }
    </script>

    <style>
        .StaticMenuStyle
        {
            float: left;
        }
    </style>
</head>
<body style="background: #FFF; height: 100%;">
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="3" style="width: 100%; height: 80%;">
        <tr>
            <td>
                <ND:NDToolbar StaticMenuStyle-CssClass="StaticMenuStyle" ID="toolbar" runat="server"
                    OnMenuItemClick="toolbar_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="审批通过" Value="Pass" ImageUrl="../web/Images/icon/accept.png">
                        </asp:MenuItem>
                        <asp:MenuItem Text="审批作废" Value="Invalid" ImageUrl="../web/Images/icon/arrow_undo.png">
                        </asp:MenuItem>
                        <asp:MenuItem Text="删除答卷" Value="Delete" ImageUrl="../web/Images/icon/Delete.gif">
                        </asp:MenuItem>
                    </Items>
                </ND:NDToolbar>
                <a href="SurveyManage.aspx" style="float: right; padding: 0; margin: 0">返回</a><img
                    src="../web/Images/icon/toolBarBack.gif" style="float: right; padding: 0; margin: 0" />
                <%--<input type="image" src="../web/Images/icon/Delete.gif" style=" float:right"  Value="返回"    onclick="return redirect();"  />--%>
                <%-- <asp:ImageButton runat="server"   ImageUrl="../web/Images/icon/Delete.gif"  OnClientClick="return redirect();" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            答卷者：
                        </td>
                        <td>
                            <asp:TextBox ID="answerName" runat="server" Width="90px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            审核状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="approvalStaus" runat="server" Width="95px" MaxLength="50">
                            </asp:DropDownList>
                        </td>
                        <td>
                            答卷用时(秒)：
                        </td>
                        <td>
                            <asp:TextBox ID="minTime" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            至：
                        </td>
                        <td>
                            <asp:TextBox ID="maxTime" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="right" width="80px" rowspan="2">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="button70" OnClientClick="return checkInput();"
                                OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            得分：
                        </td>
                        <td>
                            <asp:TextBox ID="minRecord" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            至：
                        </td>
                        <td>
                            <asp:TextBox ID="maxRecord" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            提交日期：
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="startDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                                MinDate="2000-02-19" NullDateLabel="">
                            </igsch:WebDateChooser>
                        </td>
                        <td>
                            至：
                        </td>
                        <td>
                            <igsch:WebDateChooser ID="endDate" runat="server" Width="88px" Value="" MaxDate="2020-02-19"
                                MinDate="2000-02-19" NullDateLabel="">
                            </igsch:WebDateChooser>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 80%;">
            <td colspan="1" rowspan="1">
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="SID,UID,SurveyName,Anonymity,UserName,ApprovalStaus,AnswerGUID,AnswerUserKind"
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
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                            <HeaderStyle Width="20%" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="UserName" HeaderText="答题者">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IP" HeaderText="答题IP">
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Point" HeaderText="得分">
                            <ItemStyle Width="10%" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SecondTime" HeaderText="答卷用时(秒)">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubmittIime" HeaderText="提交时间">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="Editer" HeaderText="审核状态">
                            <ItemStyle Width="10%" />
                            <HeaderStyle Width="10%" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="DeleteAnswer" HeaderText="删除">
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
