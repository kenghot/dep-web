<%@ Page Title="สร้างผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="UserProfileForm.aspx.cs" Inherits="Nep.Project.Web.User.UserProfileForm" 
    UICulture="th-TH" Culture="th-TH" %>

<%@ Register Src="~/ProjectInfo/Controls/OrgAssistanceControl.ascx" TagPrefix="uc1" TagName="OrgAssistanceControl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="UpdatePanelUserForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="DdlRole"><%: Nep.Project.Resources.Model.UserProfile_Role %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="DdlRoleID"  AutoPostBack="true" SelectMethod ="DdlRole_GetData" OnSelectedIndexChanged="DdlRoleID_SelectedIndexChanged"
                            DataTextField ="Text" DataValueField ="Value"  runat="server" CssClass="form-control" >
                        </asp:DropDownList>
                        <asp:CustomValidator ID="CustomValidatorRole" runat="server" 
                            OnServerValidate="CustomValidatorRole_ServerValidate"  ValidateEmptyText="true"
                            ClientValidationFunction="validateRole"
                            CssClass="error-text" ValidationGroup="Save" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Role) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Role) %>"
                            />
                    </div>
                    <label class="col-sm-2 control-label" for="TextBoxContractPwd">รหัสแก้เลขที่สัญญา</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="TextBoxContractPwd" TextMode="Password" CssClass="form-control" Text="" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtTelephoneNo" 
                            runat="server" CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                            ValidationGroup="Save" />
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
                    
                    <label class="col-sm-2 control-label" id="LabelProvince" runat="server"><%= Nep.Project.Resources.Model.ProjectInfo_Province %><span class="required"></span></label>
                    <div class="col-sm-4" id="DivComboBoxProvince" runat="server">
                        <input id="DdlProvince" runat="server" style="width:100%; " />
                        <asp:CustomValidator ID="CustomValidatorProvince" runat="server" ControlToValidate="DdlProvince"
                            OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                            CssClass="error-text" ValidationGroup="Save" ValidateEmptyText="true" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                            />	
                      

                       
                    </div> 
                    <label class="col-sm-2 control-label" for="TextBoxConfirmPwd">ยืนยันรหัส</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="TextBoxConfirmPwd" TextMode="Password" CssClass="form-control" Text="" MaxLength="20"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="Comp1" ControlToValidate="TextBoxConfirmPwd" ControlToCompare="TextBoxContractPwd" 
                            Text="ยืนยันรหัสผ่านไม่ถูกต้อง" Font-Size="11px" ForeColor="Red"  ValidationGroup="Save"  />
                    </div>
                </div>               

                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label" for="txtEmail"><%: Nep.Project.Resources.Model.UserProfile_UserName %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <nep:TextBox runat="server" ID="TxtEmail" TextMode="Email" ClientIDMode="Inherit" CssClass="form-control" Text=""
                            PlaceHolder="<%$ code:Nep.Project.Resources.UI.LabelUsernamePlaceHolder %>"></nep:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" ControlToValidate="TxtEmail" 
                            runat="server" CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Email) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Email) %>"
                            ValidationGroup="Save" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorDemoEmail" runat="server" ControlToValidate="TxtEmail"  CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.Model.UserProfile_Email) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.Model.UserProfile_Email) %>"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Save"/>
                    </div>

                    <label class="col-sm-2 control-label" for="txtTelephoneNo"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="TxtTelephoneNo" TextMode="Phone" CssClass="form-control" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephoneNo" ControlToValidate="TxtTelephoneNo" 
                            runat="server" CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_TelephoneNo) %>"
                            ValidationGroup="Save" />
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    
                    <label class="col-sm-2 control-label" for="txtPosition"><%: Nep.Project.Resources.Model.UserProfile_Position%><span class="required"></span></label>
                    <div class="col-sm-4">
                        <nep:TextBox runat="server" ID="TxtPosition" ClientIDMode="Inherit" CssClass="form-control" Text="" MaxLength="100"></nep:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPosition" ControlToValidate="TxtPosition" 
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Position) %>"
                            ValidationGroup="Save" />
                    </div>

                    <label class="col-sm-2 control-label" for="chkIsActive"><%: Nep.Project.Resources.Model.UserProfile_IsActive %></label>
                    <div class="col-sm-4">
                        <asp:CheckBox ID="IsActive" runat="server" CssClass="form-control-checkbox" ClientIDMode="Inherit" />
                    </div>
                </div>
                   
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonSave" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" Visible="false"
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonSave%>" ValidationGroup="Save" OnClick="ButtonSave_Click" />
                        <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                            OnClientClick="return ConfirmToDelete()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>
                        <asp:LinkButton ID="ButtonCancel" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" PostBackUrl="~/User/UserProfileList.aspx"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                

                function validateRole(oSrc, args) {
                    var selectedValue = $("#<%=DdlRoleID.ClientID%>").val();
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
