<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="CancelProjectRequestPopup.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.CancelProjectRequestPopup" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelCancelProjectRequestForm" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
      
        <ContentTemplate>  
            <div class="form-horizontal" style="margin-top:20px;">
                <div class="form-group form-group-sm" id="PrintOptionContainer" runat="server">                   
                    <div class="col-sm-12">                            
                        <nep:C2XFileUpload runat="server" ID="C2XFileUploadCancelledProjectRequest" ViewAttachmentPrefix="<%$ code:CancelledProjectRequestPrefix %>"  />                       
                       
                    </div>
                   
                </div>
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="ButtonSave" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" CssClass="btn btn-default btn-sm" 
                            OnClick="ButtonSave_Click" OnClientClick="c2x.clearResultMsg()" ValidationGroup="SaveCancelledForm"/>
                        
                        <asp:Button ID="ButtonClose" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonClose %>" CssClass="btn btn-red btn-sm"
                            Onclientclick="closeCancelledProjectRequestDialog();return false;" causesvalidation="false"/>

                    </div>
                </div>                       
            </div> 
            <script>
                function closeCancelledProjectRequestDialog() {

                    c2x.closeFormDialog();
                }


            </script>
               
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
