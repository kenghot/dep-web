<%@ Page Title="แจ้งขอลงทะเบียนองค์กร" Language="C#" MasterPageFile="~/MasterPages/Guest.Master" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs" Inherits="Nep.Project.Web.Register.RegisterCompany" 
    UICulture="th-TH" Culture="th-TH" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #page{
            height:auto;
        }

        .organization-type td:nth-child(4){
            display:block;
            clear:left;
            /*margin-top:7px;*/
        }

        .custom-width input[type='text'] {
            width:130px !important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRegister" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><%= UI.TabTitleGeneralInfo %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">    
                        <div class="form-group form-group-sm">
                            <asp:Label CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxOrganizationNameTH" runat="server"><%= Model.ProjectInfo_OrganizationNameTH %><span class="required"></span></asp:Label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="TextBoxOrganizationNameTH" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxProjectInfoNameTH" ControlToValidate="TextBoxOrganizationNameTH" 
                                    runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationNameTH) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationNameTH) %>"
                                    ValidationGroup="Save" />
                            </div>
                    
                        </div>
                        <div class="form-group form-group-sm">
                            <asp:Label CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxOrganizationNameEN" runat="server"><%= Model.ProjectInfo_OrganizationNameEN %></asp:Label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="TextBoxOrganizationNameEN" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                            </div>
                  
                        </div>

                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label">ผู้ร้องขอ (ชื่อ - นามสกุล)<span class="required"></span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBoxRequesterFirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxRequesterFirstName" 
                                    runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อผู้ร้องขอ") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อผู้ร้องขอ") %>'
                                    ValidationGroup="Save" />
                            </div>                  
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBoxRequesterLastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TextBoxRequesterLastName" 
                                    runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "นามสกุล") %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "นามสกุล") %>'
                                    ValidationGroup="Save" />
                            </div>

                            
                        </div>
                        <div class="form-group form-group-sm">
                            <asp:Label ID="Label5" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxPosition" runat="server"><%: Nep.Project.Resources.Model.UserProfile_Position %><span class="required"></span></asp:Label>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="TextBoxPosition" ClientIDMode="Inherit" CssClass="form-control" Text="" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPosition"
                                    runat="server" CssClass="error-text"
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,Nep.Project.Resources.Model.UserProfile_Position) %>'
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>'
                                    ValidationGroup="Save" />
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <asp:Label ID="Label3" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxPersonalID" runat="server"><%: Nep.Project.Resources.Model.UserProfile_PersonalID %><span class="required"></span></asp:Label>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="TextBoxPersonalID" MaxLength="13" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPersonalID"  runat="server" ID="MaskedEditExtenderPersonalID"
                                    Mask="9\-9999\-99999\-99\-9" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false" 
                                    />
                                
                                <asp:CustomValidator ID="CustomValidatorIDCardNo" ControlToValidate="TextBoxPersonalID" runat="server" CssClass="error-text"
                                    ClientValidationFunction="c2x.ClientValidateCitizenIdRequiredValidator" ValidateEmptyText="true"
                                    OnServerValidate="CustomValidatorIDCardNo_ServerValidate" 
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_PersonalID) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_PersonalID) %>"
                                    ValidationGroup="Save" SetFocusOnError="true"/>  

                                <nep:IDCardNumberValidator ID="IDCardNumberValidatorTextBoxPersonalID" ControlToValidate="TextBoxPersonalID"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>"
                                    runat="server" CssClass="error-text"
                                    ValidationGroup="Save"
                                    />
                            </div>
                            <asp:Label ID="Label4" CssClass="col-sm-2 control-label" AssociatedControlID="PersonalIDAttachment" runat="server"><%: Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment %><span class="required"></span></asp:Label>
                            <div class="col-sm-3">
                                <nep:C2XFileUpload runat="server" ID="PersonalIDAttachment" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPersonalIDAttachment" ControlToValidate="PersonalIDAttachment"
                                    runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                                    ValidationGroup="Save" />
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <asp:Label ID="Label2" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxTelephoneUser" runat="server"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %><span class="required"></span></asp:Label>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="TextBoxTelephoneUser" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control" Text="" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTelephoneUser" ControlToValidate="TextBoxTelephoneUser"
                                    runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                                    ValidationGroup="Save" />
                            </div>
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBoxMobileUser" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </div>

                            
                        </div>

                        <div class="form-group form-group-sm">
                            <asp:Label ID="Label1" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxEmail" runat="server"><%: Nep.Project.Resources.Model.UserProfile_Email %><span class="required"></span></asp:Label>
                            <div class="col-sm-3">
                                <asp:TextBox runat="server" ID="TextBoxEmail" TextMode="SingleLine" ClientIDMode="Inherit" CssClass="form-control" Text="" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmail" ControlToValidate="TextBoxEmail" runat="server"
                                    CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                    ValidationGroup="Save" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxEmail" ControlToValidate="TextBoxEmail"
                                    runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Email) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Email) %>"
                                    ValidationGroup="Save" />
                            </div>
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_OrgIdentityAttachment %></label>
                            <div class="col-sm-3">
                                <nep:C2XFileUpload runat="server" ID="OrgIdentityAttachment" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />
                            </div>
                        </div>
                    </div>
                </div>
            </div><!--General Info-->

            <div class="panel panel-default"><!--องค์กรของท่านจัดอยู่ในประเภทใด-->
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_OrganizationType %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal"> 
                        <div class="form-group form-group-sm">                        
                            <div class="col-sm-offset-2 col-sm-4 radio-button-group">
                                <div style="padding-bottom:5px; font-weight:bold;"><%= Model.ProjectInfo_OrganizationGovType %> </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType1" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" ValidationGroup="Save" Text="สังกัดกรม" />
                                    <asp:TextBox ID="TextBoxDepartmentName" runat="server" CssClass="form-control" MaxLength="1000"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartmentName" runat="server" Enabled="false" 
                                        CssClass="error-text" ControlToValidate="TextBoxDepartmentName"
                                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กรม") %>'
                                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กรม") %>'
                                        ValidationGroup="Save"  />

                                </div> 
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType2" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="กระทรวง (ให้ข้ามไปที่วัตถุประสงค์)" />
                                    <asp:TextBox ID="TextBoxMinistryName" runat="server" CssClass="form-control"  MaxLength="1000"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMinistryName" runat="server" Enabled="false" 
                                        CssClass="error-text" ControlToValidate="TextBoxMinistryName"
                                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กระทรวง") %>'
                                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กระทรวง") %>'
                                        ValidationGroup="Save"  />                                   
                                </div> 
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType3" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="องค์กรปกครองส่วนท้องถิ่น เช่นองค์การบริหารส่วนจังหวัด เทศบาล องค์การบริหารส่วนตำบล เป็นต้น" />                                 
                                </div> 
                            </div>
                        
                            <div class="col-sm-offset-1 col-sm-4 radio-button-group">
                                 <div class="required-block">
                                    <div style="padding-bottom:5px; font-weight:bold;"><%= Model.ProjectInfo_OrganizationPersonType %> </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType4" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="องค์กรด้านคนพิการ" />
                                    </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType5" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="องค์กรด้านชุมชน" />
                                    </div> 
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType6" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="องค์กรด้านธุรกิจ" />
                                    </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType7" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="อื่นๆ ระบุ" />
                                        <asp:TextBox ID="TextBoxOrganzationTypeETC" runat="server" CssClass="form-control" MaxLength="1000" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrganzationTypeETC" runat="server" Enabled="false" 
                                            CssClass="error-text" ControlToValidate="TextBoxOrganzationTypeETC"
                                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "อื่นๆ") %>'
                                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "อื่นๆ") %>'
                                            ValidationGroup="Save"  />
                                    </div>
                                    <span class="required"></span>
                                </div>
                            </div>                   
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="col-sm-offset-2 col-sm-10">
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="error-text"
                                    OnServerValidate="OrganizationTypeValidate" ClientValidationFunction="validateOrganizationType"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationType) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationType) %>"
                                    ValidationGroup="Save"/>
                            </div>
                        </div>
                    
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_OrgUnderSupport %></label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="TextBoxOrgUnderSupport" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div><!--องค์กรของท่านจัดอยู่ในประเภทใด-->

            <div class="panel panel-default"><!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_RegisterYear%></h3>
                </div>
                <div class="panel-body">   
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProjectInfo_RegisterYear %><span class="required"></span></label>
                            <div class="col-sm-2 ">                                
                                <nep:DatePicker ID="DatePickerRegisterYear" data-for="DatePickerRegisterYear" runat="server" Format="yyyy" EnabledTextBox="true" Width="155"
                                    OnClientDateSelectionChanged="onOrganizationYearChange" OnClientDateTextChanged="onOrganizationYearTextChange(event)" />                                 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegisterYear" ControlToValidate="DatePickerRegisterYear" runat="server" CssClass="error-text"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>"
                                        ValidationGroup="Save" SetFocusOnError="true"/>
                            </div>
                            <label class="col-sm-2 control-label org-register-date" style="visibility:hidden" id="OrganizationRegisterDateLabel"><%= Model.ProjectInfo_RegisterDate %><span class="required"></span></label>
                            <div class="col-sm-4 org-register-date" id="OrganizationRegisterDateControl" style="visibility:hidden">
                                <nep:DatePicker runat="server" ID="DatePickerRegisterDate" ClearTime="true" EnabledTextBox="true"  CssClass="custom-width"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                                        
                                <asp:CustomValidator ID="CustomValidatorRegisterDate" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    ControlToValidate="DatePickerRegisterDate" 
                                    OnServerValidate="CustomValidatorRegisterDate_ServerValidate" 
                                    ClientValidationFunction="validatorRegisterDate"
                                    Text="<%$ code: Nep.Project.Resources.Error.ValicateOrgnizationRegisterDate %>" 
                                    ErrorMessage="<%$ code: Nep.Project.Resources.Error.ValicateOrgnizationRegisterDate%>"
                                    ValidationGroup="Save"/>
                            </div>

                        </div>
                    </div>
                   
                </div>
            </div><!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->
    
            <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><%= Model.ProjectInfo_OfficeLocation %></h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">                   
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_AddressNo %><span class="required"></span></label>
                                <div class="col-sm-1">
                                    <asp:TextBox ID="TextBoxAddressNo" runat="server" CssClass="form-control"  MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddressNo" ControlToValidate="TextBoxAddressNo" runat="server" CssClass="error-text"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_AddressNo) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_AddressNo) %>"
                                        ValidationGroup="Save" />
                                </div>
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Moo %></label>
                                <div class="col-sm-1">
                                    <asp:TextBox ID="TextBoxMoo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxBuilding" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">                                
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxSoi" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Street %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxStreet" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>
                                <div class="col-sm-4"> 
                                    <input id="DdlProvince" runat="server" style="width:100%; " />
                                     <asp:CustomValidator ID="CustomValidatorProvince" runat="server" ControlToValidate="DdlProvince"
                                        OnServerValidate="CustomValidatorProvince_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        />
                                 
 
                                </div>
                                
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                                <div class="col-sm-4">
                                    <input id="DdlDistrict" runat="server" style="width:100%; " />
                                     <asp:CustomValidator ID="CustomValidatorDistrict" runat="server" ControlToValidate="DdlDistrict"
                                        OnServerValidate="CustomValidatorDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        />                               
                                    
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>
                                <div class="col-sm-4">
                                    <input id="DdlSubDistrict" runat="server" style="width:100%; " />
                                    <asp:CustomValidator ID="CustomValidatorSubDistrict" runat="server" ControlToValidate="DdlSubDistrict"
                                        OnServerValidate="CustomValidatorSubDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        />                                   

                                </div>

                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                                <div class="col-sm-4">
                                    <nep:TextBox ID="TextBoxPostCode" runat="server" MaxLength="5" CssClass="form-control" TextMode="Number" NumberFormat="####"></nep:TextBox>
                                    <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode"  runat="server" ID="MaskedEditExtenderPostCode"
                                        Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"  />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode" ControlToValidate="TextBoxPostCode" runat="server" CssClass="error-text"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                        ValidationGroup="Save" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostCode" ControlToValidate="TextBoxPostCode" runat="server"
                                        CssClass="error-text" ValidationExpression="\d{5}"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                        ValidationGroup="Save" />
                          
                                </div>                                
                            </div>
                            <div class="form-group form-group-sm"> 
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required"></span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxTelephoneOrganization" runat="server" CssClass="form-control"  MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephoneOrganization" ControlToValidate="TextBoxTelephoneOrganization" runat="server" CssClass="error-text"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                        ValidationGroup="Save" />
                                </div>

                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxMobileOrganization" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                 <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxFax" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                </div>
                                
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %><span class="required"></span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBoxEmailOrganization" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBoxEmailOrganization" runat="server"
                                        CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                        ValidationGroup="Save" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmailOrganization" ControlToValidate="TextBoxEmailOrganization" runat="server" CssClass="error-text"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                        ValidationGroup="Save" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!--Office Location-->

              <div class="form-horizontal">
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonSend" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                            OnClick="ButtonRegister_Click" ValidationGroup="Save" Text="<%$ code:Nep.Project.Resources.UI.ButtonSend %>" />
                        <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                            Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Register/RegisterForm.aspx"/>
                        <%--<asp:LinkButton ID="ButtonCancel" runat="server" CssClass="btn btn-default btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" PostBackUrl="~/User/UserProfileList.aspx"></asp:LinkButton>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">


        function validateOrganizationType(oSrc, args) {
            var radioChecked = $('.organization-type-radio input:checked');
            var isValid = false;
            if (radioChecked.length > 0) {
                isValid = true;
            }
            args.IsValid = isValid;
        }

        
        function validatorRegisterDate(oSrc, args) {
           
            var requiredMsg = '<%=String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterDate) %>';
            var validMsg = '<%=Nep.Project.Resources.Error.ValicateOrgnizationRegisterDate %>';

            var orgYearPicker = $find('DatePickerRegisterYear');           

            var orgRegisterYear = orgYearPicker.get_selectedDate();
            var orgYear = kendo.toString(orgRegisterYear, 'yyyy');
            orgYear = parseInt(orgYear, 10);

            var orgDatePicker = $find('DatePickerRegisterDate');
            var orgRegisterDate = orgDatePicker.get_selectedDate();

            var currentDate = kendo.parseDate(new Date());
            var currentYear = kendo.toString(currentDate, 'yyyy');
            currentYear = parseInt(currentYear, 10) - 1;

            if ((orgYear >= currentYear) && (orgRegisterDate == null)) {
                args.IsValid = false;
                $(oSrc).text(requiredMsg);
            } else if ((orgYear >= currentYear) && (orgRegisterDate != null)) {
                var diff = 0;
                
                diff = Math.floor((currentDate - orgRegisterDate) / 86400000);

                diff = diff + 1;
               
                args.IsValid = (diff >= 180);
                $(oSrc).text(validMsg);
            }
        }

        function onOrganizationYearTextChange(e) {

            showOrgRegisterDate();
        }


        function onOrganizationYearChange(sender, args) {
            showOrgRegisterDate();
        }

        

        function validateDdlProvince(oSrc, args) {
            var ddl = $("#<%=DdlProvince.ClientID%>").data("kendoComboBox");
            var item = ddl.dataItem();

            args.IsValid = (item != null);
        }

        function validateDdlDistrict(oSrc, args) {
            var ddl = $("#<%=DdlProvince.ClientID%>").data("kendoComboBox");
            var item = ddl.dataItem();

            args.IsValid = (item != null);
        }

        function validateDdlSubDistrict(oSrc, args) {
            var ddl = $("#<%=DdlProvince.ClientID%>").data("kendoComboBox");
            var item = ddl.dataItem();

            args.IsValid = (item != null);
        }

    </script>
    
</asp:Content>
