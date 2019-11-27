<%@ Page Language="C#" Title="รายงานสนับสนุนโครงการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportBudgetSummary.aspx.cs" Inherits="Nep.Project.Web.Report.ReportBudgetSummary" UICulture="th-TH" Culture="th-TH"%>
<%@ Import Namespace="Nep.Project.Resources" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search"><%=UI.LabelSearch %></div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerStartBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />                               
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ControlToValidate="DatePickerStartBudgetYear" 
                                    runat="server" CssClass="error-text" SetFocusOnError="true"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelStartBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelStartBudgetYear) %>'
                                    ValidationGroup="Search" />
                            </div>
                            <label class="col-sm-2 control-label"><%= UI.LabelTo %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerEndBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />     
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DatePickerEndBudgetYear" 
                                    runat="server" CssClass="error-text" SetFocusOnError="true"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelEndBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelEndBudgetYear) %>'
                                    ValidationGroup="Search" />                            
                                <asp:CustomValidator ID="CustomValidatorEndBudgetYear" ControlToValidate="DatePickerEndBudgetYear" 
                                    runat="server" CssClass="error-text" SetFocusOnError="true"
                                    OnServerValidate="CustomValidatorEndBudgetYear_ServerValidate"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, UI.LabelEndBudgetYear, UI.LabelStartBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, UI.LabelEndBudgetYear, UI.LabelStartBudgetYear) %>'
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
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetSummary" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
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
