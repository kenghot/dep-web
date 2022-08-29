<%@ Page Title="เพิ่มข้อมูลยุทธศาสตร์" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="StrategicForm.aspx.cs" Inherits="Nep.Project.Web.User.StrategicForm" 
    UICulture="th-TH" Culture="th-TH" %>

<%@ Register Src="~/ProjectInfo/Controls/OrgAssistanceControl.ascx" TagPrefix="uc1" TagName="OrgAssistanceControl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="UpdatePanelUserForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                 <div class="panel panel-default">
                 <div class="panel-heading">
                    <h3 class="panel-title">ข้อมูลยุทธศาสตร์</h3>
                </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label">ชื่อข้อมูลยุทธศาสตร์<span class="required"></span></label>
                    <div class="col-sm-6">
                        <nep:TextBox runat="server" ID="ItemName" CssClass="form-control" Text="" MaxLength="500" TextMode="MultiLine" Rows="10"></nep:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtFirstName" ControlToValidate="ItemName" 
                            runat="server" CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_FirstName) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.UserProfile_FirstName) %>"
                            ValidationGroup="Save" />
                    </div>
                   <label class="col-sm-2 control-label" for="chkIsActive">เปิดใช้งาน</label>
                    <div class="col-sm-1">
                        <asp:CheckBox ID="IsActive" runat="server" CssClass="form-control-checkbox" ClientIDMode="Inherit" />
                    </div>
                   
                </div>               

                </div> 
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonSave" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" Visible="false"
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonSave%>" ValidationGroup="Save" OnClick="ButtonSave_Click" />
                        <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                            OnClientClick="return ConfirmToDelete()" OnClick="ButtonDelete_Click"></asp:Button>
                        <asp:LinkButton ID="ButtonCancel" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>" PostBackUrl="~/User/UserProfileList.aspx"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                

                
                function ConfirmToDelete() {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    var isConfirm = window.confirm(message);
                     return isConfirm;
                 }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
