<%@ Page Language="C#" MasterPageFile="~/MasterPages/Guest.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Nep.Project.Web.Account.Login" Title="เข้าสู่ระบบ"  %>
<%@ MasterType TypeName="Nep.Project.Web.MasterPages.Guest"%>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        html, body {
            overflow:hidden;
        }

        #page {
            width:570px;
            height:auto;

            margin-top:5%;            
        }

        .form-horizontal .form-group-sm .control-label {
            font-size:16px;
            padding-top:10px;
            text-align:left;
            color:#797676;
        }

        .form-group-sm .form-control, input.form-control{
            font-size:16px;
            padding-top:7px;
            padding-bottom:7px;
            padding-left:7px;

            border-color:rgb(210, 207, 207);
            border-radius:4px;
        }

        .form-group-sm .form-control {
            height:36px;
        }

        #primary, .site-content-custom {
            padding-top:0px !important;
            padding-left:0px !important;
        }

        .wrapper {
            box-shadow:none;
            border:0px;
        }

        .btn.btn-login:hover {
            color: #fff;
            background-color: #286090;
            border-color: #204d74;
        }

        .btn.btn-login {
            margin-top:4px;
            width:100%;

            color: #fff;
            font-size:15px;
            font-weight: 400;

            background-color: #337ab7;
            border-color: #2e6da4;            
            border: 1px solid transparent;
            border-radius: 4px;
            padding-top:6px;
            padding-bottom:6px;

            /*background: rgba(226,226,226,1);
            background: -moz-linear-gradient(top, rgba(226,226,226,1) 0%, rgba(219,219,219,1) 50%, rgba(209,209,209,1) 51%, rgba(254,254,254,1) 100%);
            background: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(226,226,226,1)), color-stop(50%, rgba(219,219,219,1)), color-stop(51%, rgba(209,209,209,1)), color-stop(100%, rgba(254,254,254,1)));
            background: -webkit-linear-gradient(top, rgba(226,226,226,1) 0%, rgba(219,219,219,1) 50%, rgba(209,209,209,1) 51%, rgba(254,254,254,1) 100%);
            background: -o-linear-gradient(top, rgba(226,226,226,1) 0%, rgba(219,219,219,1) 50%, rgba(209,209,209,1) 51%, rgba(254,254,254,1) 100%);
            background: -ms-linear-gradient(top, rgba(226,226,226,1) 0%, rgba(219,219,219,1) 50%, rgba(209,209,209,1) 51%, rgba(254,254,254,1) 100%);
            background: linear-gradient(to bottom, rgba(226,226,226,1) 0%, rgba(219,219,219,1) 50%, rgba(209,209,209,1) 51%, rgba(254,254,254,1) 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#e2e2e2', endColorstr='#fefefe', GradientType=0 );*/
        }

        .link {
            font-size:14px;
            font-weight:100;
        }
        .forget-password-link {
            color:#929191;
        }

        #resultMessage-block {
            margin-left:30px;
        }

        hgroup a  {
            font-size:30px;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="form-horizontal">
        <div class="form-group form-group-sm">
            <label for="<%= TextBoxUsername.ClientID %>"   class="col-sm-offset-3 col-sm-3 control-label"><%=Nep.Project.Resources.Model.UserProfile_UserName%></label>
        </div>
        <div class="form-group form-group-sm">            
            <div class="col-sm-offset-3 col-sm-7">
                
                <nep:TextBox runat="server" CssClass="form-control" ID="TextBoxUsername" AutoCompleteType="None" PlaceHolder="<%$ code: Nep.Project.Resources.UI.LabelUsernamePlaceHolder %>" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" ControlToValidate="TextBoxUsername" runat="server" CssClass="error-text"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_UserName) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_UserName) %>"
                    ValidationGroup="Login" />
            </div>
        </div>
        <div class="form-group form-group-sm">
            <label for="<%= TextBoxPassword.ClientID %>" class="col-sm-offset-3 col-sm-3 control-label text-left"><%=Nep.Project.Resources.Model.UserProfile_Password%></label>
        </div>
        <div class="form-group form-group-sm">           
            <div class="col-sm-offset-3 col-sm-7">
                <asp:TextBox runat="server"  TextMode="Password" CssClass="form-control" ID="TextBoxPassword" AutoCompleteType="None"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxPassword" runat="server" CssClass="error-text"
                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Password) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_Password) %>"
                    ValidationGroup="Login" />
                <asp:CustomValidator ID="CustomValidatorLogin" ControlToValidate="TextBoxPassword" runat="server" CssClass="error-text"
                    Text='<%$ code:Nep.Project.Resources.Error.InvalidPassword %>' ErrorMessage='<%$ code:Nep.Project.Resources.Error.InvalidPassword %>'
                    OnServerValidate="CustomValidatorLogin_ServerValidate"
                    ValidationGroup="Login" />
            </div>
           
        </div>
        <div class="form-group form-group-sm">
            <div class="col-sm-offset-3 col-sm-7">
                <asp:Button runat="server" ID="ButtonLogin" ClientIDMode="Inherit" CssClass="btn btn-login btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonLogin %>" ValidationGroup="Login" OnClick="ButtonLogin_Click" />
            </div>

           
        </div>
        <div class="form-group form-group-sm">
            <div class="col-sm-offset-3 col-sm-7 text-center">
                <asp:HyperLink runat="server" ID="ButtonForgetPassword" ClientIDMode="Inherit" CssClass="link forget-password-link"
                    Text='<%$ code:Nep.Project.Resources.UI.ButtonForgetPassword + "?" %> ' NavigateUrl="~/Account/ForgetPassword"  />
                &nbsp;&nbsp;
                 <asp:HyperLink runat="server" ID="ButtonRegistry" ClientIDMode="Inherit" CssClass="link"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonRegistry %>" NavigateUrl="~/Register/RegisterForm"  />
            </div>
        </div>
              
    </div>           

   
</asp:Content>
