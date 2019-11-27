<%@ Page Language="C#" Title="แบบฟอร์มสัญญา" MasterPageFile="~/MasterPages/Print.Master" AutoEventWireup="true" 
    CodeBehind="ReportFormatContract.aspx.cs" Inherits="Nep.Project.Web.Report.ReportFormatContract" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewerContract" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
    </rsweb:ReportViewer>

</asp:Content>