<%@ Page Language="C#" Title="รายงานผู้ขอรับเงินสนับสนุนโครงการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportBudgetApplicant.aspx.cs" Inherits="Nep.Project.Web.Report.ReportBudgetApplicant" UICulture="th-TH" Culture="th-TH" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">เลขทะเบียนโครงการ</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <!-- <asp:DropDownList ID="DropDownListYear" AutoPostBack="false" runat="server" CssClass="form-control" ></asp:DropDownList> -->
                                <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetYear" ControlToValidate="DatePickerBudgetYear" 
                                    runat="server" CssClass="error-text" SetFocusOnError="true"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>'
                                    ValidationGroup="Search" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ชื่อหน่วยงาน</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxContractOrgName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label"><%=Model.Processing_ProjectName%></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">สถานะ</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="DdlApprovalStatus"
                                    CssClass="form-control"
                                    DataTextField="Text" DataValueField="Value"
                                    SelectMethod="ComboBoxApprovalStatus_GetData">
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    OnClick="ButtonSearch_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" ValidationGroup="Search" />
                                <asp:Button runat="server" ID="ButtonPrint" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    OnClick="ButtonPrint_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" ValidationGroup="Search" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <ajaxToolkit:TabContainer runat="server" ID="TabContainerReport">
        <ajaxToolkit:TabPanel runat='server' ID='TabPanelCentral' HeaderText='' Visible='False'>
            <ContentTemplate>
                <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetApplicant_Central" AsyncRendering="false" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
                    <LocalReport DisplayName="รายงานผู้ขอรับเงินสนับสนุนโครงการ">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat='server' ID='TabPanelCentralAndWestern' HeaderText='' Visible='False'>
            <ContentTemplate>
                <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetApplicant_CentralAndWestern" AsyncRendering="false" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
                    <LocalReport DisplayName="รายงานผู้ขอรับเงินสนับสนุนโครงการ">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="TabPanelNorthern" HeaderText="ภาคเหนือ" Visible="False">
            <ContentTemplate>
                <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetApplicant_Northern" AsyncRendering="false" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
                    <LocalReport DisplayName="รายงานผู้ขอรับเงินสนับสนุนโครงการ">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="TabPanelNortheast" HeaderText="ภาคตะวันออกเฉียงเหนือ" Visible="False">
            <ContentTemplate>
                <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetApplicant_Northeast" AsyncRendering="false" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
                    <LocalReport DisplayName="รายงานผู้ขอรับเงินสนับสนุนโครงการ">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="TabPanelSouthern" HeaderText="ภาคใต้" Visible="False">
            <ContentTemplate>
                <rsweb:ReportViewer runat="server" ID="ReportViewerBudgetApplicant_Southern" AsyncRendering="false" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
                    <LocalReport DisplayName="รายงานผู้ขอรับเงินสนับสนุนโครงการ">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

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
