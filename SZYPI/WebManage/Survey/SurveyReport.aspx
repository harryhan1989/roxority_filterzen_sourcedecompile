<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyReport.aspx.cs" Inherits="WebManage.Survey.SurveyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .OptionTable
        {
            border: 1px double #EEEEEE;
            border-collapse: collapse;
            width: 630px;
        }
        .OptionTable th, .OptionTable td
        {
            border: 1px solid #EEEEEE;
            color: #000000;
            font-size: 12px;
            text-align: right;
        }
        .OptionTable th
        {
            height: 25px;
            line-height: 25px;
            font-weight: bold;
            text-align: center;
            font-size: 12px;
        }
        .OptionTable td
        {
            height: 21px;
            line-height: 21px;
            padding: 0 5px;
            font-size: 12px;
            font-weight: normal;
        }
        .OptionTable .Statistics
        {
            height: 21px;
            line-height: 21px;
            padding: 0 5px;
            font-weight: bold;
            text-align: center;
            font-size: 12px;
        }
        .TableTitle
        {
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .DivConclusion
        {
            font-size: 12px;
            font-weight: bold;
            text-align: left;
            vertical-align: text-top;
        }
        .surveyReport
        {
            background: url(images/d0_22.jpg) 0 0 repeat-x;
            line-height: 25px;
            font-weight: bold;
            text-align: left;
            font-size: 12px;
            border: 1px double #000000;
            border-collapse: collapse;
            width: 800px;
            height: auto;
        }
        .surveyReportTable
        {
            text-align: left;
            font-size: 12px;
            font-weight: normal;
        }
        .TopDivTitle
        {
            text-align: left;
            height: 25px;
            text-indent:10px;
        }
        .AnalysisTitle
        {
            text-align: left;
            height: 25px;
            width: 630px;
            background-color: #EEEEEE;
            font-weight: normal;
            text-indent:8px;
        }
        .OutTitle
        {
            text-align: left;
            height: 25px;
            font-weight: bold;
            font-size: 13px;
            vertical-align: bottom;
            padding-top: 15px;
            text-indent:6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
