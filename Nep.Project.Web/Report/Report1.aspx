<%@ Page Language="C#" Title="Report1" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="Report1.aspx.cs" Inherits="Nep.Project.Web.Report.Report1" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewer1" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
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