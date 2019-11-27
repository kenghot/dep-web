<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabResponseControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabResponseControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelPersonal" UpdateMode="Conditional" runat="server" >
    <ContentTemplate>
        <style type="text/css"> 
            .combobox-width {
                width:100%;
            }
        </style>
        <asp:HiddenField runat="server" ID="HiddenProjectID" />

        <div class="panel panel-default"> <!--ผู้รับผิดชอบโครงการ-->
            <div class="panel-heading">
                <h3 class="panel-title">เจ้าหน้าที่ผู้รับผิดชอบโครงการ</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">                    

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Firstname %><span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">

                            <asp:TextBox ID="TextBoxFirstName1" runat="server" MaxLength="50" CssClass="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName1" ControlToValidate="TextBoxFirstName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Lastname %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxLastName1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName1" ControlToValidate="TextBoxLastName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                    </div> <!-- ชื่อ-สกุล -->

                     <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">ตำแหน่ง<span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">

                            <asp:TextBox ID="TextBoxPosition1" runat="server" MaxLength="50" CssClass="form-control"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxPosition1" runat="server" CssClass="error-text"
                                Text="กรุณาระบุตำแหน่ง" 
                                ErrorMessage="กรุณาระบุตำแหน่ง"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
  
                    </div> <!-- ตำแหน่ง -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">
                            <asp:TextBox ID="TextBoxTelephone1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>     
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone1" ControlToValidate="TextBoxTelephone1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>                       
                        </div>
                    
                            <label class="col-sm-2 control-label">อีเมลล์<span class="required"></span></label> 
                            <div class="col-sm-4 control-block">  
                            <asp:TextBox ID="TextBoxEmail1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>         
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail1" ControlToValidate="TextBoxEmail1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmail" ControlToValidate="TextBoxEmail1" runat="server"
                                CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ValidationGroup="SavePersonal" />   
                            </div>              
                            
                    </div> <!-- โทรศัพท์ -->
 
                </div><!--form-horizontal-->
            </div><!--panel-body-->
        </div> <!--\ผู้รับผิดชอบโครงการ-->
    
      
 

        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveResponse" CssClass="btn btn-primary btn-sm" ValidationGroup="SavePersonal"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSaveResponse_Click" Visible="true" />
   
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
              
    
    </ContentTemplate>
</asp:UpdatePanel><!--UpdatePanelPersonal-->

