<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="FollowupPrintingPopup.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.FollowupPrintingPopup" 
    UICulture="th-TH" Culture="th-TH" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .datepicker-tracking > span>  input.form-control.form-control-datepicker{
            width:90% !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelFolloupPrePrintingForm" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="ButtonFollowupPrePrintingPrint" />
        </Triggers>--%>
        <ContentTemplate>  
            <div class="form-horizontal">
                <div class="form-group form-group-sm" id="PrintOptionContainer" runat="server">
                    <label class="col-sm-2 control-label "><%=UI.ButtonPrint %></label>
                    <div class="col-sm-4 text-center" id="">                            
                        <asp:DropDownList runat="server" ID="DropDownListPrintOption"  
                            DataTextField="LovName" DataValueField="LovID" CssClass="form-control"
                            OnTextChanged="DropDownListPrintOption_TextChanged" AutoPostBack="true"/>                       
                    </div>
                   
                </div>  
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label "><%: Model.FollowupTrackingDocumentForm_SendDate %><span class="required"></span></label> 
                    <div class="col-sm-4">
                        <nep:DatePicker CssClass="datepicker-tracking" runat="server" ID="DatePickerSendDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="Save"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDemoDate" ControlToValidate="DatePickerSendDate" runat="server" CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_SendDate) %>' 
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_SendDate) %>'
                            ValidationGroup="PrintingForm" /> 
                    </div> 
                    <label class="col-sm-2 control-label " id="LabelDeadlineDate" runat="server"><%: Model.FollowupTrackingDocumentForm_DeadlineDate %><span class="required"></span></label> 
                    <div class="col-sm-4" id="DivDeadlineDate" runat="server">
                        <nep:DatePicker CssClass="datepicker-tracking" runat="server" ID="DatePickerDeadlineDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="Save"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeadlineDate" ControlToValidate="DatePickerDeadlineDate" runat="server" CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_DeadlineDate) %>' 
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_DeadlineDate) %>'
                            ValidationGroup="PrintingForm" /> 
                        <asp:CustomValidator ID="CustomValidatorReportDate" CssClass="error-text" runat="server" 
                                    OnServerValidate="CustomValidatorReportDate_ServerValidate"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.FollowupTrackingDocumentForm_DeadlineDate, Model.FollowupTrackingDocumentForm_SendDate) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.FollowupTrackingDocumentForm_DeadlineDate, Model.FollowupTrackingDocumentForm_SendDate) %>"
                                    ValidationGroup="PrintingForm" />
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label "><%: Model.FollowupTrackingDocumentForm_ReportNo %><span class="required"></span></label> 
                    <div class="col-sm-4">                        
                        <asp:TextBox ID="TextBoxReportNo" runat="server" CssClass="form-control" MaxLength="20"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorReportNo" ControlToValidate="TextBoxReportNo" runat="server" CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_ReportNo) %>' 
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_ReportNo) %>'
                            ValidationGroup="PrintingForm" /> 
                    </div>   

                            
                </div>   
                <div class="form-group form-group-sm" id="ContainerOrgRefInfo1" runat="server">
                    <label class="col-sm-2 control-label "><%: Model.FollowupTrackingDocumentForm_RefInfo1 %></label> 
                    <div class="col-sm-10">
                        <nep:TextBox ID="TextBoxRefInfo1" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div> 
                </div>                     
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label "><%: Model.FollowupTrackingDocumentForm_RefInfo %><span class="required"></span></label> 
                    <div class="col-sm-10">
                        <nep:TextBox ID="TextBoxRefInfo" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRefInfo" ControlToValidate="TextBoxRefInfo" runat="server" CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_RefInfo) %>' 
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.FollowupTrackingDocumentForm_RefInfo) %>'
                            ValidationGroup="PrintingForm" /> 
                    </div> 
                </div> 
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="ButtonPrint" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonSavePdf %>" CssClass="btn btn-default btn-sm" 
                            OnClick="ButtonPrint_Click" OnClientClick="c2x.clearResultMsg()" ValidationGroup="PrintingForm"/>

                        <asp:Button ID="ButtonPrintWord" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonSaveWord %>" CssClass="btn btn-default btn-sm" 
                            OnClick="ButtonPrintWord_Click" OnClientClick="c2x.clearResultMsg()" ValidationGroup="PrintingForm"/>

                         <asp:Button ID="ButtonDelete" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" CssClass="btn btn-red btn-sm" 
                            OnClick="ButtonDelete_Click" OnClientClick="return ConfirmToTrackingDoc()" Visible="false"/>
                        
                        <asp:Button ID="ButtonClose" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonClose %>" CssClass="btn btn-red btn-sm"
                            Onclientclick="closePrintingFormDialog();return false;" causesvalidation="false"/>

                    </div>
                </div>                       
            </div> 
            <script>
                function closePrintingFormDialog() {                    
                   
                    c2x.closeFormDialog();                    
                }


                function ConfirmToTrackingDoc() {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    var isConfirm = window.confirm(message);
                    return isConfirm;
                }
                
            </script>
               
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>

<asp:Content ID="Footer" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
