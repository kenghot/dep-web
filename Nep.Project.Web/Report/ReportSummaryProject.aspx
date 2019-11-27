<%@ Page Language="C#" Title="แบบสรุปโครงการ(1)" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportSummaryProject.aspx.cs" Inherits="Nep.Project.Web.Report.ReportSummaryProject" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewerSummaryProject" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
    </rsweb:ReportViewer>

    <script type="text/javascript">
        $(document).ready(function () {
            var aExport = $("a:contains('Word')");
            if (aExport != null) {
                aExport.each(function (index) {
                    $(this).hide();
                });
            }
        });
    </script>
</asp:Content>