<%@ Page Title="ลงทะเบียนผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Guest.Master" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="Nep.Project.Web.Register.RegisterForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">

        <div class="form-group form-group-sm">
            <asp:Label CssClass="col-sm-2 control-label" runat="server"><%= Nep.Project.Resources.Model.UserProfile_Province %></asp:Label>
            <div class="col-sm-10">    
                <asp:HiddenField ID="hdfProvince" runat="server" />
                <input ID="DdlOrganizationProvince" runat="server" style="width:30%" />
            <%--    <asp:CustomValidator ID="CustomValidatorProjectProvince" runat="server" ControlToValidate="DdlOrganizationProvince"
                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                        CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                        /> --%>                            

            </div>  
        </div>
        <div class="form-group form-group-sm">
            <asp:Label CssClass="col-sm-2 control-label" runat="server"><%: Nep.Project.Resources.Model.UserProfile_OrganizationName %><span class="required"></span></asp:Label>
            <div class="col-sm-8">            
                    <asp:HiddenField ID="hdfOrganization" runat="server" />
                <input id="DdlOrganization" runat="server" style="width:100%"/>        
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="DdlOrganization"
                    OnServerValidate="CustomValidatorOrganization_ServerValidate" ClientValidationFunction="validateComboBoxRequiredWithAlert"
                    CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_OrganizationName) %>"
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_OrganizationName) %>"
                    />                      
          <%--      <asp:CustomValidator ID="CustomValidatorOrganization" runat="server" ControlToValidate="DdlOrganization"
                    OnServerValidate="CustomValidatorOrganization_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                    CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_OrganizationName) %>"
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_OrganizationName) %>"
                    /> --%>
            </div>
            <div class="col-sm-2">
                <asp:HyperLink ID="ButtonSelectOrganize" runat="server" Text="ไม่พบชื่อองค์กร" CssClass="btn btn-default btn-sm btn-block"
                    NavigateUrl="~/Register/RegisterCompany" />
            </div>
        </div>

        <div class="form-group form-group-sm">
            <asp:Label CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxRegisterFirstName" runat="server">ชื่อ - นามสกุล<span class="required"></span></asp:Label>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="TextBoxRegisterFirstName" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxFirstName" ControlToValidate="TextBoxRegisterFirstName"
                    runat="server" CssClass="error-text"
                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อ") %>'
                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อ") %>'
                    ValidationGroup="Save" />
            </div>

            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="TextBoxRegisterLastName" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxLastName" ControlToValidate="TextBoxRegisterLastName"
                    runat="server" CssClass="error-text"
                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "นามสกุล") %>'
                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, "นามสกุล") %>'
                    ValidationGroup="Save" />
            </div>

            <asp:Label CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxPosition" runat="server"><%: Nep.Project.Resources.Model.UserProfile_Position %><span class="required"></span></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="TextBoxPosition" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxPosition"
                    runat="server" CssClass="error-text"
                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,Nep.Project.Resources.Model.UserProfile_Position) %>'
                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>'
                    ValidationGroup="Save" />
            </div>
        </div>

        <div class="form-group form-group-sm">
            <asp:Label CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxPersonalID" runat="server"><%: Nep.Project.Resources.Model.UserProfile_PersonalID %><span class="required"></span></asp:Label>
            <div class="col-sm-4">
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
            <asp:Label CssClass="col-sm-2 control-label" AssociatedControlID="PersonalIDAttachment" runat="server"><%: Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment %><span class="required"></span></asp:Label>
            <div class="col-sm-4">
                <nep:C2XFileUpload runat="server" ID="PersonalIDAttachment" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPersonalIDAttachment" ControlToValidate="PersonalIDAttachment"
                    runat="server" CssClass="error-text"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredAttachment, Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment) %>"
                    ValidationGroup="Save" />
            </div>
        </div>

        <div class="form-group form-group-sm">
           

            <asp:Label ID="Label1" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxTelephoneNo" runat="server"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %><span class="required"></span></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="TextBoxTelephoneNo" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTelephoneNo" ControlToValidate="TextBoxTelephoneNo"
                    runat="server" CssClass="error-text"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                    ValidationGroup="Save" />
            </div>

            <label class="col-sm-2 control-label"><%= Nep.Project.Resources.Model.ProjectInfo_Mobile %></label>
            <div class="col-sm-4">
                <asp:TextBox ID="TextBoxMobileUser" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
            </div>
            
        </div>

        <div class="form-group form-group-sm">
            <asp:Label ID="Label2" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxEmail" runat="server"><%: Nep.Project.Resources.Model.UserProfile_Email %><span class="required"></span></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="TextBoxEmail" TextMode="SingleLine" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
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
            <div class="col-sm-4">
                <nep:C2XFileUpload runat="server" ID="OrgIdentityAttachment" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />
            </div>
        </div>
        <div class="form-group form-group-sm">
            <div class="col-sm-12 text-center">
                <asp:Button runat="server" ID="ButtonRegister" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                    OnClick="ButtonRegister_Click" ValidationGroup="Save" Text="<%$ code:Nep.Project.Resources.UI.ButtonRegister %>" />
                <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Account/Login"/>
                <%--<Label><%: Nep.Project.Common.Constants.WEBSITE_URL %>"></Label>--%>
            </div>
        </div>

    </div>
   <script type="text/javascript">
        validateComboBoxRequiredWithAlert = function (oSrc, args) {
           var ddlID = $(oSrc).attr("data-val-controltovalidate");
           var ddl = $("#" + ddlID).data("kendoComboBox");
           var item = ddl.dataItem();
           args.IsValid = (item != null);
           if (!args.IsValid) {
               alert("ไม่พบชื่อองค์กรของท่านในฐานข้อมูลกรุณากดที่ปุ่ม 'ไม่พบชื่อองค์กร' เพื่อสมัครใช้งานระบบ");
           }
       }
   </script>

</asp:Content>
