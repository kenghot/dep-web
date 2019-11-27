<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="ReviseReportPopup.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.ReviseReportPopup" %>
<%@ Import Namespace="Nep.Project.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRejectCommentForm" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group form-group-sm form-group-title">
                    <div class="col-sm-12"><%=Nep.Project.Resources.Model.ProjectInfo_RejectComment %></div>
                </div>
                <div class="form-group form-group-sm">
                    <div class="col-sm-12">
                        <nep:TextBox TextMode="MultiLine" ID="TextBoxRejectComment" runat="server" CssClass="form-control textarea-height" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxRejectComment" ControlToValidate="TextBoxRejectComment" runat="server" CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectInfo_RejectComment) %>' 
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectInfo_RejectComment) %>'
                            ValidationGroup="SaveRejectComment" /> 

                    </div>
                </div>
                    <div class="form-group form-group-sm">
                                <label class="col-sm-6 control-label control-label-left without-delimit">
                                    เอกสารแนบ
                                </label>
                    </div>
                    <div class="form-group form-group-sm">
                                <div class="col-sm-6">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadAttach" MultipleFileMode="true" ViewAttachmentPrefix="ReviseFile" />  
                    </div>   
                    </div>                   
                </div>
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="ButtonSaveComment" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonReject%>" CssClass="btn btn-default btn-sm" 
                                    OnClick="ButtonSaveComment_Click" OnClientClick="c2x.clearResultMsg()" ValidationGroup="SaveRejectComment"/>
                        
                        <asp:Button ID="ButtonClose" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonClose %>" CssClass="btn btn-red btn-sm"
                            Onclientclick="closeRejectCommentFormDialog();return false;" causesvalidation="false"/>
                    </div>
                </div>
            </div>

            <script>
                function closeRejectCommentFormDialog() {

                    c2x.closeFormDialog();
                }


             </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
