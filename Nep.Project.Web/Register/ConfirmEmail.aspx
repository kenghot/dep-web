<%@ Page Title="ยืนยันการลงทะเบียน" Language="C#" MasterPageFile="~/MasterPages/Guest.Master" AutoEventWireup="true" CodeBehind="ConfirmEmail.aspx.cs" Inherits="Nep.Project.Web.Register.ConfirmEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="HiddenFieldOrgID" runat="server" />
    <asp:HiddenField ID="HiddenFieldUserID" runat="server" />
    <div class="form-horizontal">
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" for="txtEmail"><%: Nep.Project.Resources.Model.UserProfile_UserName %></label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" ClientIDMode="Inherit" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" for="txtRegisterName"><%: Nep.Project.Resources.Model.UserProfile_FirstName %></label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtRegisterName" ClientIDMode="Inherit" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" for="txtTelephoneNo"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %></label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtTelephoneNo" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control" Text="" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" for="txtTelephoneNo"><%: Nep.Project.Resources.Model.UserProfile_Password %></label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="server"
                     CssClass="error-text"
                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_Password) %>'
                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_Password) %>'
                        ValidationGroup="Save"  />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" ControlToValidate="txtPassword" runat="server"
                    CssClass="error-text" ValidationExpression="^(?=.*\d)(?=.*[a-zA-Z]).{8,}$"
                    Text="<%$ code: String.Format(Nep.Project.Resources.UI.PasswordDesc) %>"
                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.UI.PasswordDesc) %>"
                    ValidationGroup="Save" />
                <asp:CustomValidator ID="CustomValidatorPassword" runat="server" ValidationGroup="Save" CssClass="error-text"
                    OnServerValidate="CustomValidatorPassword_ServerValidate"
                    Text='<%$ code: Nep.Project.Resources.UI.PasswordDesc %>'
                    ErrorMessage='<%$ code: Nep.Project.Resources.UI.PasswordDesc %>'
                />
            </div>
            <div class="col-sm-6">
                <span class="field-desc"><%: Nep.Project.Resources.UI.LabelFieldRemark %></span><%:Nep.Project.Resources.UI.PasswordDesc %>               
            </div>
        </div>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 control-label" for="txtTelephoneNo"><%: Nep.Project.Resources.Model.UserProfile_ConfirmPassword %></label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" ClientIDMode="Inherit" CssClass="form-control" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword" runat="server"
                     CssClass="error-text"
                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField,  Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                        ValidationGroup="Save"  />
                <asp:CompareValidator ID="cpvConfirmPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" runat="server"
                     CssClass="error-text"
                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.CompareEqual, Nep.Project.Resources.Model.UserProfile_Password, Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.CompareEqual, Nep.Project.Resources.Model.UserProfile_Password, Nep.Project.Resources.Model.UserProfile_ConfirmPassword) %>'
                        ValidationGroup="Save"  />
            </div>
        </div>

        <div class="form-group form-group-sm">
            <div class="col-sm-12  text-center">
                <asp:Button runat="server" ID="ButtonSubmit" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" ValidationGroup="Save"
                    OnClick="ButtonSubmit_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSubmit %>" />
                <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Account/Login"/>
                <%--<asp:LinkButton ID="ButtonCancel" runat="server" CssClass="btn btn-default btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" PostBackUrl="~/User/UserProfileList.aspx"></asp:LinkButton>--%>
            </div>
        </div>
    </div>
</asp:Content>
