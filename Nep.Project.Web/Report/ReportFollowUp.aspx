<%@ Page Language="C#" Title="รายงานแบบติดตามประเมินผลโครงการ" MasterPageFile="~/MasterPages/Site.Master"  AutoEventWireup="true" 
    CodeBehind="ReportFollowUp.aspx.cs" Inherits="Nep.Project.Web.Report.ReportFollowUp" UICulture="th-TH" Culture="th-TH" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label">จังหวัด</label>
                            <div class="col-sm-4">
                                <input id="DdlProvince" runat="server" style="width:100%; " /> 
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label">ปีงบประมาณ</label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />                               
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                OnClick="ButtonSearch_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" ValidationGroup="Search"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

<%--    <rsweb:ReportViewer runat="server" ID="ReportViewer4" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
        <LocalReport DisplayName="รายงานสถิติเปรียบเทียบความซ้ำซ้อนของผู้รับบริการ" >                    
        </LocalReport>
    </rsweb:ReportViewer>--%>

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