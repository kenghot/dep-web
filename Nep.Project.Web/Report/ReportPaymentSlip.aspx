<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Print.Master" AutoEventWireup="true" CodeBehind="ReportPaymentSlip.aspx.cs" Inherits="Nep.Project.Web.Report.ReportPaymentSlip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewerPaymentSlip" CssClass="report-viewer" Width="100%" Height="100%" 
        ProcessingMode="Local" SizeToReportContent="True" PageCountMode="Actual">
        <LocalReport DisplayName="ใบชำระเงิน" ></LocalReport>
    </rsweb:ReportViewer>

    <script type="text/javascript">
        $(document).ready(function () {
            

            var xExport = $("a:contains('Excel')");
            if (xExport != null) {
                xExport.each(function (index) {
                    $(this).hide();
                });
            }
        });
    </script>

</asp:Content>
