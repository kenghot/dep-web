<%@ Page Language="C#" Title="รายงานทะเบียนผู้รับบริการแยกตามโครงการ" MasterPageFile="~/MasterPages/Site.Master"  AutoEventWireup="true" 
    UICulture="th-TH" Culture="th-TH"
    CodeBehind="ReportOrganizationClient.aspx.cs" Inherits="Nep.Project.Web.Report.ReportOrganizationClient" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">จังหวัด</label>
                            <div class="col-sm-4">
                                <input id="DdlProvince" runat="server" style="width:100%; " />                                 
                            </div>   
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />                                                                                                   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ControlToValidate="DatePickerBudgetYear" runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>'
                                    ValidationGroup="Search" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" ValidationGroup="Search"
                                OnClick="ButtonSearch_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer runat="server" ID="ReportViewerOrganizationClient" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
        <LocalReport DisplayName="รายงานทะเบียนผู้รับบริการแยกตามโครงการ" >                    
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