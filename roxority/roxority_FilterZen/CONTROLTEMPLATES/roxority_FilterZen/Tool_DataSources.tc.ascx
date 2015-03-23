<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>
<%@ Assembly Name="roxority_FilterZen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a2103dd0c3e898e1" %>
<%@ Import Namespace="roxority.Shared" %>
<%@ Import Namespace="roxority.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.Administration" %>
<%@ Import Namespace="Microsoft.SharePoint.Utilities" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Resources" %>
<%@ Register TagPrefix="rox" TagName="ItemEditor" Src="~/_controltemplates/roxority_FilterZen/ItemEditor.tc.ascx" %>
<rox:ItemEditor runat="server" ID="ItemEditor" AsmName="roxority_Shared" FilePath="/_layouts/roxority_FilterZen/schemas.tl.json" SchemaName="DataSources" />
