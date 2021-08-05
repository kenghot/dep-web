<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentProvideControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.AttachmentProvideControl" %>
<style type="text/css">
    #attachment-form span.error-text {
        display:block;
    }
</style>
<asp:UpdatePanel ID="UpdatePanelAttachment" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <div class="form-horizontal" id="attachment-form">
        <asp:Repeater ID="RepeaterAttachemnt" runat="server" 
            OnDataBinding="RepeaterAttachemnt_DataBinding"             
            OnItemDataBound="RepeaterAttachemnt_ItemDataBound">
            <ItemTemplate>
                <div class="form-group form-group-sm" attachment-row="<%# Eval("DocumentNo") %>">
                    <div class="col-sm-1">
                        <asp:CheckBox ID="CheckBoxSelect" runat="server" Enabled="false" CssClass="form-control form-control-checkbox" />  
                        <asp:HiddenField ID="DocumentNo" Value='<%# Eval("DocumentNo") %>' runat="server" />                     
                    </div>
                    <div class="col-sm-7">
                        <span style="margin-left:-50px; padding-top:6px;" class="label-no"><%# Eval("DocumentNo") %></span>
                        <span style="margin-left:-30px; padding-top:6px; display:inline-block;"><%# Eval("DocumentProvideName") %><span class="required"></span></span>  
                         
                                             
                    </div>                                    
                    <div class="col-sm-4">
                        <nep:C2XFileUpload runat="server" ID="C2XFileUploadProjectAttachment" MultipleFileMode="true" Enabled="<%$ code:IsEditabled %> "
                             ViewAttachmentPrefix="<%$ code:ViewAttachmentPrefix %>"  /> 
                         <asp:CustomValidator ID="CustomValidatorProjectAttachment" runat="server" 
                            ValidateEmptyText="true"                            
                            OnServerValidate="CustomValidatorProjectAttachment_ServerValidate_New" 
                            CssClass="error-text" ValidationGroup="SaveProjectAttachment" 
                            Text='<%$ code: Nep.Project.Resources.Error.RequiredAttachmentOne %>' 
                            ErrorMessage='<%$ code: Nep.Project.Resources.Error.RequiredAttachmentOne%>'></asp:CustomValidator> 
                        
                    </div>      
                </div>                
            </ItemTemplate>
        </asp:Repeater>
        </div>
        <%--<div>
            <asp:CustomValidator ID="CustomValidatorProjectAttachment" runat="server" 
                            ValidateEmptyText="true"                            
                            OnServerValidate="CustomValidatorProjectAttachment_ServerValidate" 
                            CssClass="error-text" ValidationGroup="SaveProjectAttachment" 
                            Text='<%$ code: Nep.Project.Resources.Error.RequiredAttachmentOne %>' 
                            ErrorMessage='<%$ code: Nep.Project.Resources.Error.RequiredAttachmentOne%>'></asp:CustomValidator> 
        </div>--%>
        
    </ContentTemplate>
</asp:UpdatePanel>