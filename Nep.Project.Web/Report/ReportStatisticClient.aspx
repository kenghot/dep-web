<%@ Page Language="C#" Title="รายงานสถิติเปรียบเทียบผู้รับบริการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportStatisticClient.aspx.cs" Inherits="Nep.Project.Web.Report.ReportStatisticClient" UICulture="th-TH" Culture="th-TH" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div id="Div1" runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ปีงบประมาณ<span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerStartYear" runat="server" Format="yyyy" EnabledTextBox="true" />  
                                <asp:RequiredFieldValidator ID="RequiredFieldStartYear" ControlToValidate="DatePickerStartYear" runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>'
                                    ValidationGroup="Search" />
                            </div>
                            <label class="col-sm-2 control-label">ถึง<span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerEndYear" runat="server" Format="yyyy" EnabledTextBox="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldEndYear" ControlToValidate="DatePickerEndYear" runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>'
                                    ValidationGroup="Search" />
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


    <rsweb:ReportViewer runat="server" ID="ReportViewerStatisticClient" PageCountMode="Actual" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
        <LocalReport DisplayName="รายงานสถิติเปรียบเทียบผู้รับบริการ">
        </LocalReport>    
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