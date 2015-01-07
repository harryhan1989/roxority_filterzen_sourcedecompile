<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectGrid.aspx.cs" Inherits="WebManage.Survey.SelectGrid" %>

<%@ Register Assembly="Nandasoft.WebControls" Namespace="Nandasoft.WebControls" TagPrefix="ND" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Menu.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Platform/MainWeb/js/showArrow.js"></script>

    <link href="../CSS/ChildMenu.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Js/surveylist.js"></script>
    <script type="text/javascript">
        function SetSelectValue() {
            document.getElementById("selectSurvey").options[0].label = "123";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="3" style="width: 100%; height: 80%;">
        <tr>
            <td> 
                <table border="0" cellpadding="2" cellspacing="2" style="font-size: 12px">
                    <tr> 
                        <td>
                            问卷名称： 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSurveyName" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            回收数：
                        </td>
                        <td>
                            <asp:TextBox ID="getBackNum" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            问卷类型：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSurveyClass" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="80px" rowspan="2">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="button70" OnClientClick="return checkInput();"
                                OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
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
                <input id="hiddenRecord" type="hidden" value="" />
                <ND:NDGridView ID="grid" runat="server" AllowSetRowStyle="True" AutoGenerateColumns="False"
                    Height="100%" RowHoverStyleClassName="gridviewRowHover" RowStyleClassName="gridviewRow"
                    Width="100%" DataKeyNames="SID,SurveyName,AnswerAmount" AllowSorting="True" OnRowCommand="grid_RowCommand"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="12px">
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                            <HeaderStyle Width="25px" />
                            <%--                            <HeaderTemplate>
                                <asp:RadioButton ID="chkItemAll" runat="Server" onclick="SetAllChecked(this, 'chkItem');" />
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <input type="radio" id="chkItem" name="choose" value='<%# Eval("ID") %>' onclick="" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SurveyName" HeaderText="问卷名称">
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AnswerAmount" HeaderText="答卷回收数">
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SurveyClassName" HeaderText="问卷类型">
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreateDate" HeaderText="创建时间" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Width="23%" HorizontalAlign="Center" />
                            <HeaderStyle Width="23%" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle Height="21px" BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle Height="21px" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                </ND:NDGridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <ND:NDGridViewPager ID="viewpage1" runat="server" OnPageChanged="pager_PageChanged"
                    Font-Size="12px" Width="100%" Height="25px" ShowCustomInfoSection="Left" CustomInfoSectionWidth="80%"
                    FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" PageSize="5"
                    PagingButtonSpacing="8px" NumericButtonCount="8" NumericButtonTextFormatString="[{0}]"
                    AlwaysShow="True" ShowInputBox="Always" ShowSelectBox="Always" InputBoxStyle="WIDTH: 60px"
                    SubmitButtonText="Go !">
                </ND:NDGridViewPager>
            </td>
        </tr>
    </table>
    <div style="width: 100%; text-align: center">
        <asp:Button ID="Choosed" runat="server" Width="70" Text="选择" OnClick="Choosed_Click" />
    </div>
    </form>
</body>
</html>
