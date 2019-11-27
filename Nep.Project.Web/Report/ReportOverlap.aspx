<%@ Page Language="C#" Title="รายงานสรุปความซ้ำซ้อนผู้รับบริการทั้งประเทศ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportOverlap.aspx.cs" Inherits="Nep.Project.Web.Report.ReportOverlap" 
    UICulture="th-TH" Culture="th-TH"%>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelSearch" 
                    UpdateMode="Conditional"
                    runat="server">
        
    <ContentTemplate>
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
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
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>'
                                    ValidationGroup="Search" />
                            </div>
                            <div class="col-sm-2 control-label">จังหวัด</div>
                            <div class="col-sm-4">
                                <input id="DdlProvince" runat="server" style="width:100%; " /> 
                                 
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ชื่อ - นามสกุล</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 control-label">เลขที่บัตรประชาชน</div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxIdCardNo" runat="server" CssClass="form-control"></asp:TextBox>                          
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="Button1" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" OnClick="ButtonSearch_Click" ValidationGroup="Search"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerOverlap" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
        <LocalReport DisplayName="รายงานสรุปความซ้ำซ้อนผู้รับบริการทั้งประเทศ">
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
         </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>