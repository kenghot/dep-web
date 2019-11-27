<%@ Page Title="แก้ไขข้อมูลผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="UserRequestForm.aspx.cs" Inherits="Nep.Project.Web.User.UserRequestForm" 
    UICulture="th-TH" Culture="th-TH" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <style type="text/css">
        span.aspNetDisabled.form-control {
            padding:0px;
            border:0px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanelUserForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group form-group-sm" >
                    <label class="col-sm-2 control-label" for="txtFirstName"><%: Nep.Project.Resources.Model.UserProfile_OrganizationName %><span class="required"></span></label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="TextBoxOrganizationName" TextMode="SingleLine" ClientIDMode="Inherit" ReadOnly="true" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Nep.Project.Resources.Model.ProjectInfo_Province %></label>
                    <div class="col-sm-4">
                        <ajaxToolkit:ComboBox runat="server" ID="ComboBoxProvince"                    
                            DropDownStyle="DropDown"                                         
                            AutoCompleteMode= "Suggest" 
                            CaseSensitive="false"     
                            CssClass="form-control-combobox"                    
                            DataTextField ="Text"
                            DataValueField="Value"
                            AppendDataBoundItems="false"
                            Enabled="false">                    
                        </ajaxToolkit:ComboBox>
                               
                        <asp:CustomValidator ID="CustomValidatorProvince" runat="server" 
                            OnServerValidate="CustomValidatorProvince_ServerValidate" 
                            ClientValidationFunction="validateProvince"
                            CssClass="error-text" ValidationGroup="Save" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                            />
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="txtFirstName"><%: Nep.Project.Resources.Model.UserProfile_FirstName %><span class="required"></span></label>
                    <div class="col-sm-2">
                        <nep:TextBox runat="server" ID="TxtFirstName" CssClass="form-control" Text="" MaxLength="100"></nep:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtFirstName" ControlToValidate="TxtFirstName" 
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_FirstName) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_FirstName) %>"
                            ValidationGroup="Save" />
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox runat="server" ID="TxtLastName" ClientIDMode="Static" CssClass="form-control" Text="" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtLastName" ControlToValidate="TxtLastName" 
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_LastName) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_LastName) %>"
                            ValidationGroup="Save" />
                    </div>

                    <label class="col-sm-2 control-label" for="txtPosition"><%: Nep.Project.Resources.Model.UserProfile_Position%><span class="required"></span></label>
                    <div class="col-sm-4">
                        <nep:TextBox runat="server" ID="TxtPosition" ClientIDMode="Inherit" CssClass="form-control" Text="" MaxLength="100"></nep:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPosition" ControlToValidate="TxtPosition" 
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>"
                            ValidationGroup="Save" />
                    </div>
                    
                </div>

                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="DdlRole"><%: Nep.Project.Resources.Model.UserProfile_PersonalID %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <nep:TextBox ID="TextBoxIDCardNo" runat="server" CssClass="form-control" TextMode="SingleLine" />                            
                        <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxIDCardNo"  runat="server" ID="MaskedEditExtenderPersonalMainIDCardNo"
                            Mask="9\-9999\-99999\-99\-9" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"
                            />                             
                        <nep:IDCardNumberValidator ID="IDCardNumberValidator" ControlToValidate="TextBoxIDCardNo" runat="server" CssClass="error-text"  
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                            ValidationGroup="Save" Enabled="false"  />
                    </div>

                    <asp:Label ID="Label1" CssClass="col-sm-2 control-label" AssociatedControlID="PersonalIDAttachment" runat="server"><%: Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment %><span class="required"></span></asp:Label>
                    <div class="col-sm-4">
                        <nep:C2XFileUpload runat="server" ID="PersonalIDAttachment" MultipleFileMode="false"  ViewAttachmentPrefix="<%$ code:RegisPrefix %>"  />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPersonalIDAttachment" ControlToValidate="PersonalIDAttachment"
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                            ValidationGroup="Save" />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="txtEmail"><%: Nep.Project.Resources.Model.UserProfile_UserName %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="TxtEmail" TextMode="Email" ClientIDMode="Inherit" CssClass="form-control" Text="" ></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label" for="txtTelephoneNo"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="TxtTelephoneNo" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephoneNo" ControlToValidate="TxtTelephoneNo" 
                            runat="server" CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                            ValidationGroup="Save" />
                    </div>
                </div>
                
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_OrgIdentityAttachment %></label>
                    <div class="col-sm-4">
                        <nep:C2XFileUpload runat="server" ID="OrgIdentityAttachment" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />
                    </div>
                    <label class="col-sm-2 control-label"><%= Nep.Project.Resources.Model.UserProfile_RegisterDate %></label>
                    <div class="col-sm-4">
                        <nep:DatePicker runat="server" CssClass="form-control" ID="DatePickerRegisterDate" Enabled="false"/>                        
                    </div>
                </div> 
                    
                
           <%--     <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="chkIsActive"><%: Nep.Project.Resources.Model.UserProfile_IsActive %></label>
                    <div class="col-sm-4">
                        <asp:CheckBox ID="CheckBoxIsActive" runat="server" CssClass="form-control-checkbox" ClientIDMode="Inherit" Checked="true" />
                    </div>
                </div>   --%>   
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonSave" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonSave%>" ValidationGroup="Save" OnClick="ButtonSave_Click" />
                         <asp:Button runat="server" ID="ButtonSendMail" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                Text="ส่ง email เพื่อยืนยัน" ValidationGroup="Save" OnClick="ButtonSendMail_Click" />
                        <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                            OnClientClick="return ConfirmToDelete()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>
                        <asp:LinkButton ID="ButtonCancel" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" PostBackUrl="~/User/UserNewProfileList.aspx"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function validateProvince(oSrc, args) {
                    var selectedValue = $find("<%=ComboBoxProvince.ClientID%>").get_hiddenFieldControl().value;
                    var isValid = false, selectedIndex;
                    if ((selectedValue != "") && (!isNaN(selectedValue))) {
                        selectedIndex = parseInt(selectedValue, 10);
                        isValid = (selectedIndex < 0) ? false : true;
                    }
                    args.IsValid = isValid;
                }

                function ConfirmToDelete() {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    var isConfirm = window.confirm(message);
                      return isConfirm;
                  }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

