<%@ Page Title="ข้อมูลองค์กร" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="OrganizationForm.aspx.cs" Inherits="Nep.Project.Web.Organization.OrganizationForm"
    UICulture="th-TH" Culture="th-TH" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #page {
            height: auto;
        }

        .organization-type td:nth-child(4) {
            display: block;
            clear: left;
            /*margin-top:7px;*/
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
                            <asp:Label ID="Label1" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxOrganizationNameTH" runat="server"><%= Model.ProjectInfo_OrganizationNameTH %><span class="required"></span></asp:Label>
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
                            <asp:Label ID="Label2" CssClass="col-sm-3 control-label" AssociatedControlID="TextBoxOrganizationNameEN" runat="server"><%= Model.ProjectInfo_OrganizationNameEN %></asp:Label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="TextBoxOrganizationNameEN" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--General Info-->

            <div class="panel panel-default">
                <!--องค์กรของท่านจัดอยู่ในประเภทใด-->
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_OrganizationType %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <div class="col-sm-offset-2 col-sm-4 radio-button-group">
                                <div style="padding-bottom: 5px; font-weight: bold;"><%= Model.ProjectInfo_OrganizationGovType %> </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType1" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" ValidationGroup="Save" Text="สังกัดกรม" />
                                    <asp:TextBox ID="TextBoxDepartmentName" runat="server" CssClass="form-control" MaxLength="1000" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartmentName" runat="server" Enabled="false"
                                        CssClass="error-text" ControlToValidate="TextBoxDepartmentName"
                                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กรม") %>'
                                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กรม") %>'
                                        ValidationGroup="Save" />

                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType2" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="กระทรวง" />
                                    <asp:TextBox ID="TextBoxMinistryName" runat="server" CssClass="form-control" MaxLength="1000" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMinistryName" runat="server" Enabled="false"
                                        CssClass="error-text" ControlToValidate="TextBoxMinistryName"
                                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กระทรวง") %>'
                                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "กระทรวง") %>'
                                        ValidationGroup="Save" />
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType3" runat="server" CssClass="organization-type-radio" AutoPostBack="true" OnCheckedChanged="RadioButtonOrganizationType_CheckedChanged" GroupName="OrganizationType" Text="องค์กรปกครองส่วนท้องถิ่น เช่นองค์การบริหารส่วนจังหวัด เทศบาล องค์การบริหารส่วนตำบล เป็นต้น" />
                                </div>
                            </div>

                            <div class="col-sm-offset-1 col-sm-4 radio-button-group">
                                <div class="required-block">
                                    <div style="padding-bottom: 5px; font-weight: bold;"><%= Model.ProjectInfo_OrganizationPersonType %> </div>
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
                                            ValidationGroup="Save" />
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
                                    ValidationGroup="Save" />
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
            </div>
            <!--องค์กรของท่านจัดอยู่ในประเภทใด-->

            <div class="panel panel-default">
                <!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_RegisterYear%></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProjectInfo_RegisterYear %><span class="required"></span></label>
                            <div class="col-sm-2 ">
                                <nep:DatePicker ID="DatePickerRegisterYear" runat="server" Format="yyyy" EnabledTextBox="true" Width="155"
                                    OnClientDateSelectionChanged="onOrganizationYearChange" OnClientDateTextChanged="onOrganizationYearTextChange(event)" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegisterYear" ControlToValidate="DatePickerRegisterYear" runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>"
                                    ValidationGroup="Save" SetFocusOnError="true" />
                            </div>

                            <label class="col-sm-2 control-label org-register-date" style="visibility: hidden" id="OrganizationRegisterDateLabel"><%= Model.ProjectInfo_RegisterDate %><span class="required"></span></label>
                            <div class="col-sm-4 org-register-date" id="OrganizationRegisterDateControl" style="visibility: hidden">
                                <nep:DatePicker runat="server" ID="DatePickerRegisterDate" ClearTime="true" EnabledTextBox="true" Width="155"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />

                                <asp:CustomValidator ID="CustomValidatorRegisterDate" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    ControlToValidate="DatePickerRegisterDate"
                                    OnServerValidate="CustomValidatorRegisterDate_ServerValidate"
                                    ClientValidationFunction="validatorRegisterDate"
                                    Text="<%$ code: Nep.Project.Resources.Error.ValicateOrgnizationRegisterDate %>"
                                    ErrorMessage="<%$ code: Nep.Project.Resources.Error.ValicateOrgnizationRegisterDate%>"
                                    ValidationGroup="Save" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_OfficeLocation %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-12 form-group-title"><%= Model.ProjectInfo_OfficeLocation %></label>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_AddressNo %><span class="required"></span></label>
                            <div class="col-sm-1">
                                <asp:TextBox ID="TextBoxAddressNo" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
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
                                <input id="DdlProvince" runat="server" style="width: 100%;" />
                                <asp:CustomValidator ID="CustomValidatorProvince" runat="server" ControlToValidate="DdlProvince"
                                    OnServerValidate="CustomValidatorProvince_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                    CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>" />


                            </div>


                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <input id="DdlDistrict" runat="server" style="width: 100%;" />
                                <asp:CustomValidator ID="CustomValidatorDistrict" runat="server" ControlToValidate="DdlDistrict"
                                    OnServerValidate="CustomValidatorDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                    CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>" />

                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <input id="DdlSubDistrict" runat="server" style="width: 100%;" />
                                <asp:CustomValidator ID="CustomValidatorSubDistrict" runat="server" ControlToValidate="DdlSubDistrict"
                                    OnServerValidate="CustomValidatorSubDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                    CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>" />
                            </div>
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:TextBox ID="TextBoxPostCode" runat="server" MaxLength="5" CssClass="form-control" TextMode="Number" NumberFormat="####"></nep:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode" ControlToValidate="TextBoxPostCode" runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                    ValidationGroup="Save" />
                                <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode" runat="server" ID="MaskedEditExtenderPostCode"
                                    Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false" />
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
                                <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone" ControlToValidate="TextBoxTelephone" runat="server" CssClass="error-text"
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
                                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" ControlToValidate="TextBoxEmail" runat="server" CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                    ValidationGroup="Save" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmail" ControlToValidate="TextBoxEmail" runat="server"
                                    CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                    ValidationGroup="Save" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Office Location-->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">บัญชีธนาคาร</h3>
                </div>
                <div class="panel-body">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">รหัสธนาคาร</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxBankNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">รหัสสาขา</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxBranchNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                </div>
                <div class="panel-body">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">เลขที่บัญชี</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxAccountNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">ชื่อบัญชี</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxAccountName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                </div>
            </div>
            <!-bank  -->

              <div class="form-horizontal">
                  <div class="form-group form-group-sm">
                      <div class="col-sm-12  text-center">
                          <asp:Button runat="server" ID="ButtonSave" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                              OnClick="ButtonSave_Click" ValidationGroup="Save" Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" />

                          <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                              OnClientClick="return ConfirmToDeleteOrg()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>

                          <asp:HyperLink runat="server" ID="ButtonCancel" ClientIDMode="Inherit" CssClass="btn btn-red btn-sm"
                              Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" NavigateUrl='~/Organization/OrganizationList' />
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

        function ConfirmToDeleteOrg() {
            var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
            var isConfirm = window.confirm(message);
            return isConfirm;
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

    </script>
</asp:Content>
