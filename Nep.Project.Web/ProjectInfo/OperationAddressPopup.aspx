<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="OperationAddressPopup.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.OperationAddressPopup" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .invalid {
            color: red;
        }

        .k-invalid-msg {
            display:none;
        }

        span.k-widget.k-tooltip-validation {
            display: inline-block;           
            text-align: left;
            border: 0;
            padding: 0;
            margin: 0;
            background: none;
            box-shadow: none;
            color: red;
        }

        .k-tooltip-validation .k-warning {
            display: none;
        }

        body {
            padding:0px;
        }

        #main {
            width:750px;
        }
    </style>
    <div class="form-horizontal" id="OperationAddressForm">      
       
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" data-label="TxtAddress"><%= Model.ProcessingPlan_Address %><span class="required"></span></label>                        
            <div class="col-sm-10">  
                <asp:TextBox ID="TextBoxAddress" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" ControlToValidate="TextBoxAddress" runat="server" CssClass="error-text"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>" 
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>"
                    ValidationGroup="SaveAddress" SetFocusOnError="true"/>                            
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Moo %></label>
            <div class="col-sm-1">     
                <asp:TextBox ID="TextBoxMoo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
            </div>
                        
            <label class="col-sm-offset-3 col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
            <div class="col-sm-4">
                 <asp:TextBox ID="TextBoxBuilding" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>                         
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>                        
            <div class="col-sm-4">     
                <asp:TextBox ID="TextBoxSoi" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
            </div>
                        
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Road %></label>
            <div class="col-sm-4">    
                <asp:TextBox ID="TextBoxRoad" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox> 
            </div>                
        </div>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>                        
            <div class="col-sm-4">
                 <input id="DdlProvince" runat="server" style="width:100%; " />
                <asp:CustomValidator ID="CustomValidatorProvince" runat="server" ControlToValidate="DdlProvince"
                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                        CssClass="error-text" ValidationGroup="SaveAddress" ValidateEmptyText="true" SetFocusOnError="true"
                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                        />
                          
            </div>
                        
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %></label>
            <div class="col-sm-4">  
                <input id="DdlDistrict" runat="server" style="width:100%; " />
               
            </div>  
        </div>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %></label>                        
            <div class="col-sm-4">
                <input id="DdlSubDistrict" runat="server" style="width:100%; " />
                   
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label"><%=Model.ProcessingPlan_Map %></label>
            <div class="col-sm-10">
                <div class="input-file-container single-file">
                    <input type="hidden" id="HiddLocationAddedFiles" />
                    <input type="hidden" id="HiddLocationRemoved" />
                    <input type="file" id="FileOperationAddress" name="FileOperationAddress" accept=".gif, .jpg, .jpeg, .png, .pdf"/>               
                    <span class="extension-validate" ></span>  
                </div>
                                 
            </div> 
        </div>
        
        
        <div class="form-group form-group-sm">
            <div class="col-sm-12 text-center">
                <asp:Button ID="ButtonSaveComment" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonSave%>" CssClass="btn btn-default btn-sm" 
                            OnClientClick="return c2xOprAddress.save()" />
                        
                <asp:Button ID="ButtonClose" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonClose %>" CssClass="btn btn-red btn-sm"
                    Onclientclick="closeAddressFormDialog();return false;" causesvalidation="false"/>
            </div>
        </div>                           
           
    </div>

    <script>
        function closeAddressFormDialog() {

            c2x.closeFormDialog();
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
