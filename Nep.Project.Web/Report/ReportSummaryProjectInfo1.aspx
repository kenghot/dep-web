<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Print.Master" AutoEventWireup="true" CodeBehind="ReportSummaryProjectInfo1.aspx.cs" Inherits="Nep.Project.Web.Report.ReportSummaryProjectInfo1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewerProjectSummaryInfo1" CssClass="report-viewer" Width="100%" Height="100%" 
        ProcessingMode="Local" SizeToReportContent="True" PageCountMode="Actual">
        <LocalReport DisplayName="รายงานสรุปโครงการ" ></LocalReport>
    </rsweb:ReportViewer>

    <script type="text/javascript">
        $(document).ready(function () {
            var xWord = $("a:contains('Word')");
            if (xWord != null) {
                xWord.each(function (index) {
                    $(this).hide();
                });
            }
        });
    </script>
</asp:Content>