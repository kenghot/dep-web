<%@ Page Language="C#" Title="หนังสือทวงถาม จังหวัด" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportInterrogate.aspx.cs" Inherits="Nep.Project.Web.Report.ReportInterrogate" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer runat="server" ID="ReportViewerInterrogate" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
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