<%@ Page Title="เปลี่ยนรหัสผ่าน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Nep.Project.Web.Account.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-horizontal">
        <asp:Panel runat="server" ID="PanelForgetPassword">
            <div class="form-group form-group-sm">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxOrganizationName"><%: Nep.Project.Resources.Model.UserProfile_OrganizationName %></asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="TextBoxOrganizationName" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxRegisterName"><%: Nep.Project.Resources.Model.UserProfile_FirstName %></asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="TextBoxRegisterName" ClientIDMode="Inherit" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="form-group form-group-sm">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxOldPassword"><%: Nep.Project.Resources.Model.UserProfile_OldPassword %></asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="TextBoxOldPassword" TextMode="Password" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" ControlToValidate="TextBoxOldPassword" runat="server"
                         CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_OldPassword) %>'
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_OldPassword) %>'
                            ValidationGroup="Save"  />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxPassword"><%: Nep.Project.Resources.Model.UserProfile_NewPassword %></asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="TextBoxPassword" TextMode="Password" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword" runat="server"
                         CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_NewPassword) %>'
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_NewPassword) %>'
                            ValidationGroup="Save"  />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" ControlToValidate="TextBoxPassword" runat="server"
                        CssClass="error-text" ValidationExpression="^(?=.*\d)(?=.*[a-zA-Z]).{8,}$"
                        Text="<%$ code: String.Format(Nep.Project.Resources.UI.PasswordDesc) %>"
                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.UI.PasswordDesc) %>"
                        ValidationGroup="Save" />
                </div>
                <div class="col-sm-6">
                    <span class="field-desc"><%: Nep.Project.Resources.UI.LabelFieldRemark %></span><%:Nep.Project.Resources.UI.PasswordDesc %>   
                </div>
            </div>
            <div class="form-group form-group-sm">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="TextBoxConfirmPassword"><%: Nep.Project.Resources.Model.UserProfile_ConfirmPassword %></asp:Label>
                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="TextBoxConfirmPassword" TextMode="Password" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPassword" ControlToValidate="TextBoxConfirmPassword" runat="server"
                         CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                            ValidationGroup="Save"  />
                    <asp:CompareValidator ID="CompareValidatorConfirmPassword" ControlToValidate="TextBoxConfirmPassword" ControlToCompare="TextBoxPassword" runat="server"
                         CssClass="error-text"
                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.CompareEqual, Nep.Project.Resources.Model.UserProfile_Password, Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.CompareEqual, Nep.Project.Resources.Model.UserProfile_Password, Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                            ValidationGroup="Save"  />
                </div>
            </div>
        </asp:Panel>
        <div class="form-group form-group-sm">
            <div class="col-sm-12 text-center">
                <asp:Button runat="server" ID="ButtonSubmit" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" ValidationGroup="Save"
                    OnClick="ButtonSubmit_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSubmit %>" />
                <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                            Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Account/Login"/>
            </div>
        </div>
    </div>
</asp:Content>
