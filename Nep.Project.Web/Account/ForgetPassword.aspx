<%@ Page Title="ลืมรหัสผ่าน" Language="C#" MasterPageFile="~/MasterPages/Guest.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="Nep.Project.Web.Account.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-horizontal">

        <asp:Panel runat="server" ID="PanelForgetPassword">
            <div class="form-group form-group-sm">
                <label for="DemoName" class="col-sm-offset-3 col-sm-1 control-label"><%=Nep.Project.Resources.Model.UserProfile_UserName%><span class="required"></span></label>
                <div class="col-sm-5">                    
                    <nep:TextBox ID="TxtUsername" runat="server" MaxLength="50" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server"
                        CssClass="error-text" ValidationGroup="Submit" ControlToValidate="TxtUsername"
                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_UserName) %>" 
                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_UserName) %>" />
                </div>
            </div>
        </asp:Panel>
         
        <asp:Panel runat="server" CssClass="alert alert-info" ID="PanelMessage" Visible="false">
                ระบบได้ทำการจัดส่งอีเมลเพื่อยืนยันตัวตน กรุณาตรวจสอบเมลบ็อกซ์ของท่านและดำเนินการตามขั้นตอนที่ได้แจ้งไว้ในอีเมล
        </asp:Panel>
        
        <div class="form-group form-group-sm">
            <div class="col-sm-12 text-center">
                <asp:Button runat="server" ID="ButtonSend" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSend %>" ValidationGroup="Submit" OnClick="ButtonSend_Click"/>

                <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Account/Login" />
            </div>
        </div>
    </div>
    
</asp:Content>
