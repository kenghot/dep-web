<%@ Page Language="C#" Title="รายงานผู้ขอรับเงินสนับสนุนโครงการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportReceiverFunding.aspx.cs" Inherits="Nep.Project.Web.Report.ReportReceiverFunding" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">หมายเลข</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">ปีงบประมาณ</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxYear" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ชื่อหน่วยงาน</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxOrganizationName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">ชื่อโครงการ</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">สถานะ</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                OnClick="ButtonSearch_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerReceiverFunding" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
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
