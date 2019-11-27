<%@ Page Language="C#" Title="รายงานสรุปความซ้ำซ้อนผู้รับบริการระดับจังหวัด" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
   UICulture="th-TH" Culture="th-TH"
   CodeBehind="ReportOverlapProvince.aspx.cs"  Inherits="Nep.Project.Web.Report.ReportOverlapProvince" %>
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
                                <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />                                                                                                   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ControlToValidate="DatePickerBudgetYear" runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ปีงบประมาณ") %>'
                                    ValidationGroup="Search" />
                            </div>
                            <div class="col-sm-2 control-label">ภาค</div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListRegion" AutoPostBack="true" runat="server" ClientIDMode="Inherit" CssClass="form-control" 
                                OnTextChanged="ComboBoxProvince_TextChanged" DataTextField="Text" DataValueField="Value"> </asp:DropDownList>                          
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">จังหวัด</label>
                            <div class="col-sm-4">
                                <ajaxToolkit:ComboBox runat="server" ID="ComboBoxProvince" 
                                    DropDownStyle="DropDown"                                         
                                    AutoCompleteMode="Suggest" 
                                    CaseSensitive="false"     
                                    CssClass="form-control-combobox"  AutoPostBack="true"
                                    DataTextField="Text" DataValueField="Value"   
                                    OnTextChanged="ComboBoxSection_TextChanged"
                                    >
                                <asp:ListItem Text="" Value=""/>                               
                                </ajaxToolkit:ComboBox> 
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
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerOverlapProvince" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True">
         <LocalReport DisplayName="รายงานสรุปความซ้ำซ้อนผู้รับบริการระดับจังหวัด">
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