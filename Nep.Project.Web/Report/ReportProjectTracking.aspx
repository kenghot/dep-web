<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Print.Master" AutoEventWireup="true" CodeBehind="ReportProjectTracking.aspx.cs" Inherits="Nep.Project.Web.Report.ReportProjectTracking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <rsweb:ReportViewer runat="server" ID="ReportViewerProjectTracking" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
    </rsweb:ReportViewer>
</asp:Content>
