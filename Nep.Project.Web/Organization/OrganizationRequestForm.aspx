<%@ Page Title="ข้อมูลองค์กรขอแจ้งเพิ่ม" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="OrganizationRequestForm.aspx.cs" Inherits="Nep.Project.Web.Organization.OrganizationRequestForm" 
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

        .org-empty-attachment {
            padding-top:4px;
            padding-left:10px;
            display:inline-block;
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
                            <label class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationNameTH %></label>
                            <div class="col-sm-9 control-value">
                                <asp:Label ID="LabelOrganizationNameTH" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                    
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationNameEN %></label>
                            <div class="col-sm-9 control-value">
                                <asp:Label ID="LabelOrganizationNameEN" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                  
                        </div>

                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label">ผู้ร้องขอ (ชื่อ - นามสกุล)</label>
                            <div class="col-sm-4 control-value">
                                <asp:Label ID="LabelRequesterFirstName" runat="server" CssClass="form-control-static"></asp:Label>&nbsp;
                                <asp:Label ID="LabelRequesterLastName" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>                  
                            
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_Position %></label>
                            <div class="col-sm-3 control-value">
                                <asp:Label runat="server" ID="LabelPosition" CssClass="form-control-static" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label"><%: Nep.Project.Resources.Model.UserProfile_PersonalID %></label>
                            <div class="col-sm-4 control-value">
                                <asp:Label runat="server" ID="LabelPersonalID" CssClass="form-control-static" Text=""></asp:Label>
                            </div>
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_PersonalIDAttachment %></label>
                            <div class="col-sm-3 control-value">
                                <nep:C2XFileUpload runat="server" ID="PersonalIDAttachment" MultipleFileMode="false" Enabled="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                           
                            <label class="col-sm-3 control-label"><%: Nep.Project.Resources.Model.UserProfile_TelephoneNo %></label>
                            <div class="col-sm-4 control-value">
                                <asp:Label runat="server" ID="LabelTelephoneUser" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control-static" Text=""></asp:Label>
                            </div>

                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_Mobile %></label>
                            <div class="col-sm-3 control-value">
                                <asp:Label runat="server" ID="LabelMobileUser" TextMode="Phone" ClientIDMode="Inherit" CssClass="form-control-static" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <label class="col-sm-3 control-label"><%: Nep.Project.Resources.Model.UserProfile_Email %></label>
                            <div class="col-sm-4 control-value">
                                <asp:Label runat="server" ID="LabelEmail" TextMode="SingleLine" ClientIDMode="Inherit" CssClass="form-control-static" Text=""></asp:Label>
                            </div>

                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_OrgIdentityAttachment %></label>
                            <div class="col-sm-3">
                                <asp:Label ID="LabelOrgIdentityAttachment" runat="server" Text="-" CssClass="org-empty-attachment"/>
                                <nep:C2XFileUpload runat="server" ID="OrgIdentityAttachment" MultipleFileMode="false" Enabled="false" ViewAttachmentPrefix="<%$ code:RegisPrefix %>" />                                
                            </div>
                        </div>
                    </div>
                </div>
            </div><!--General Info-->

            <!--องค์กรของท่านจัดอยู่ในประเภทใด-->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_OrganizationType %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal"> 
                        <div class="form-group form-group-sm">                        
                            <div class="col-sm-offset-2 col-sm-4 radio-button-group">
                                <div style="padding-bottom:5px; font-weight:bold;"><%= Model.ProjectInfo_OrganizationGovType %> </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType1" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="สังกัดกรม" />
                                    <asp:TextBox ID="TextBoxDepartmentName" runat="server" ReadOnly="true" CssClass="form-control"/>
                                </div> 
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType2" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="กระทรวง" />
                                    <asp:TextBox ID="TextBoxMinistryName" runat="server" ReadOnly="true" CssClass="form-control" />                               
                                </div> 
                                <div>
                                    <asp:RadioButton ID="RadioButtonOrganizationType3" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="องค์กรปกครองส่วนท้องถิ่น เช่นองค์การบริหารส่วนจังหวัด เทศบาล องค์การบริหารส่วนตำบล เป็นต้น" />                                 
                                </div> 
                            </div>
                        
                            <div class="col-sm-offset-1 col-sm-4 radio-button-group">
                                 <div class="required-block">
                                    <div style="padding-bottom:5px; font-weight:bold;"><%= Model.ProjectInfo_OrganizationPersonType %> </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType4" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="องค์กรด้านคนพิการ" />
                                    </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType5" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="องค์กรด้านชุมชน" />
                                    </div> 
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType6" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="องค์กรด้านธุรกิจ" />
                                    </div>
                                    <div>
                                        <asp:RadioButton ID="RadioButtonOrganizationType7" runat="server" CssClass="organization-type-radio" Enabled="false" GroupName="OrganizationType" Text="อื่นๆ ระบุ" />
                                        <asp:TextBox ID="TextBoxOrganzationTypeETC" runat="server" ReadOnly="true" CssClass="form-control" />
                                    </div>
                                   
                                </div>
                            </div>                   
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_OrgUnderSupport %></label>
                            <div class="col-sm-10 control-value">
                                <asp:Label ID="LabelOrgUnderSupport" runat="server" CssClass="form-control-static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--องค์กรของท่านจัดอยู่ในประเภทใด-->

            <div class="panel panel-default"><!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_RegisterYear%></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProjectInfo_RegisterYear %></label>
                            <div class="col-sm-2 control-value">
                                <asp:Label ID="LabelRegisterYear" runat="server" CssClass="form-control-static"/>
                            </div>

                            <label class="col-sm-2 control-label org-register-date" runat="server" id="OrganizationRegisterDateLabel" visible="false"><%= Model.ProjectInfo_RegisterDate %></label>
                            <div class="col-sm-4  control-value org-register-date" runat="server" id="OrganizationRegisterDateControl" visible="false">
                                <asp:Label ID="LabelRegisterDate" runat="server" CssClass="form-control-static"/>
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
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_AddressNo %></label>
                                <div class="col-sm-1 control-value">
                                    <asp:Label ID="LabelAddressNo" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                 <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Moo %></label>
                                <div class="col-sm-1 control-value">
                                    <asp:Label ID="LabelMoo" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelBuilding" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                               
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelSoi" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>

                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Street %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelStreet" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelSubDistrict" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                
                                 <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelDistrict" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelProvince" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelPostCode" runat="server" CssClass="form-control-static"></asp:Label>
                          
                                </div>
                            </div>
                            <div class="form-group form-group-sm">  
                                                     
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelTelephoneOrganization" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>

                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                                <div class="col-sm-4  control-value">
                                    <asp:Label ID="LabelMobileOrganization" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelFax" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %></label>
                                <div class="col-sm-4 control-value">
                                    <asp:Label ID="LabelEmailOrganization" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!--Office Location-->

              <div class="form-horizontal">
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonApprove" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                            OnClick="ButtonApprove_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonApprove %>" />
                        <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                            OnClientClick="return ConfirmToDeleteOrg()" OnClick="ButtonDelete_Click" ></asp:Button>
                        <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                            Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Organization/OrganizationRequestList"/>
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                function ConfirmToDeleteOrg() {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    var isConfirm = window.confirm(message);
                     return isConfirm;
                 }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
