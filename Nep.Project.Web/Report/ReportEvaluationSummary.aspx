<%@ Page Language="C#" Title="รายงานสรุปการพิจารณาโครงการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportEvaluationSummary.aspx.cs" Inherits="Nep.Project.Web.Report.ReportEvaluationSummary" 
    UICulture="th-TH" Culture="th-TH"%>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search"><%=UI.LabelSearch %></div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%=UI.LabelProjectNo%></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker ID="DatePickerStartBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ControlToValidate="DatePickerStartBudgetYear" 
                                    runat="server" CssClass="error-text" SetFocusOnError="true"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>'
                                    ValidationGroup="Search" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= UI.LabelOrgName %></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxContractOrgName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label"><%=Model.Processing_ProjectName%></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectName" runat="server" CssClass="form-control"></asp:TextBox>
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
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerEvaluationSummary" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
        <LocalReport DisplayName="รายงานสรุปการพิจารณาโครงการ">
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
