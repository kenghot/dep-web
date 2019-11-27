<%@ Page Language="C#" Title="Report3" MasterPageFile="~/MasterPages/Print.Master"  AutoEventWireup="true" 
    CodeBehind="Report3.aspx.cs" Inherits="Nep.Project.Web.Report.Report3" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewer3" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
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

            var xExport = $("a:contains('Excel')");
            if (xExport != null) {
                xExport.each(function (index) {
                    $(this).hide();
                });
            }
        });
    </script>
</asp:Content>