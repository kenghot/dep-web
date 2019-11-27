<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabGeneralInfoControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabGeneralInfoControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>
<%@ Register TagPrefix="nep" TagName="CommitteeControl" Src="~/ProjectInfo/Controls/CommitteeControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="OrgAssistanceControl" Src="~/ProjectInfo/Controls/OrgAssistanceControl.ascx" %>

<asp:UpdatePanel ID="UpdatePanelGeneralInfoControl" 
                    UpdateMode="Conditional" 
                    runat="server" >
    
    <ContentTemplate>  

        
     

 
        <div class="panel panel-default"><!--ข้อมูลทั่วไป-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TabTitleGeneralInfo %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">               
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>
                        <div class="col-sm-8">
                            <ajaxToolkit:ComboBox runat="server" ID="ComboBoxOrganizationProvince" 
                                DataMember="Nep.Project.ServiceModels.GenericDropDownListData"
                                DropDownStyle="DropDown"                                         
                                AutoCompleteMode= "Suggest" 
                                CaseSensitive="false"     
                                CssClass="form-control-combobox"  
                                SelectMethod="GetOrganizationProvince"
                                AppendDataBoundItems="false"
                                DataTextField ="Text"
                                DataValueField="Value"                                   
                                ItemInsertLocation="OrdinalValue"
                                Enabled="false">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </ajaxToolkit:ComboBox>   
                                
                            <asp:CustomValidator ID="CustomValidatorOrganizationProvince" runat="server" 
                                OnServerValidate="CustomValidatorOrganizationProvince_ServerValidate" 
                                ClientValidationFunction="validateOrganizationProvince"
                                CssClass="error-text" ValidationGroup="SaveGeneralInfo" 
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                />                                
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationNameTH %><span class="required"></span></label>
                        <div class="col-sm-8">
                            <ajaxToolkit:ComboBox runat="server" ID="ComboBoxOrganizationNameTH" 
                                DropDownStyle="DropDown"                                         
                                AutoCompleteMode="Suggest" 
                                CaseSensitive="false"     
                                CssClass="form-control-combobox"   
                                DataTextField ="Text"
                                DataValueField="Value"                              
                                Enabled ="false">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </ajaxToolkit:ComboBox>
                        </div>
                   
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationNameEN %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="TextBoxOrganizationNameEN" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div> 
                </div>
        </div>
    </div><!--ข้อมูลทั่วไป-->

    <div class="panel panel-default"><!--องค์กรของท่านจัดอยู่ในประเภทใด-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_OrganizationType %></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">               
                <div class="form-group form-group-sm">   
                    <label class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationGovType %> (ให้ข้ามไปที่วัตถุประสงค์)</label>
                    <div class="col-sm-3 radio-button-group">
                        <div>
                            <!--สังกัดกรม-->
                            <asp:RadioButton ID="RadioButtonOrganizationType1" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" 
                                Text="" />
                            <asp:TextBox ID="TextBoxOrganizationType1" runat="server" CssClass="form-control" ReadOnly="true" />                                   
                        </div> 
                        <div>
                            <!--กระทรวง-->
                            <asp:RadioButton ID="RadioButtonOrganizationType2" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" 
                                Text="" />
                            <asp:TextBox ID="TextBoxOrganizationType2" runat="server" CssClass="form-control" ReadOnly="true" />                                   
                        </div> 
                        <div>
                            <!--องค์กรปกครองส่วนท้องถิ่น-->
                            <asp:RadioButton ID="RadioButtonOrganizationType3" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" 
                                Text="" />                                 
                        </div> 
                    </div>
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_OrganizationPersonType %></label>
                    <div class="col-sm-3  radio-button-group">
                        <div class="required-block">
                            <div>
                                <!--องค์กรด้านคนพิการ-->
                                <asp:RadioButton ID="RadioButtonOrganizationType4" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" Text="" />
                            </div>
                            <div>
                                <!--องค์กรชุมชน-->
                                <asp:RadioButton ID="RadioButtonOrganizationType5" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" Text="" />
                            </div> 
                            <div>
                                <!--องค์กรธุรกิจ-->
                                <asp:RadioButton ID="RadioButtonOrganizationType6" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" Text="" />
                            </div>
                            <div>
                                <!--อื่นๆ-->
                                <asp:RadioButton ID="RadioButtonOrganizationType7" runat="server" Enabled="false" CssClass="organization-type-radio" GroupName="OrganizationType" Text="" />
                                <asp:TextBox ID="TextBoxOrganizationType7" runat="server" CssClass="form-control"  ReadOnly="true" />
                            </div>
                            <%-- <span class="required"></span>--%>
                        </div>                        
                    </div>                  
                </div>
                 <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_OrgUnderSupport %></label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="TextBoxOrgUnderSupport" runat="server" MaxLength="1000" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div><!--องค์กรของท่านจัดอยู่ในประเภทใด-->

    <div class="panel panel-default"><!--รายชื่อคณะกรรมการ/ตำแหน่ง-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_Committees %></h3>
        </div>
        <div class="panel-body">          
            <div class="form-group form-group-sm">
                <div class="col-sm-12">
                    <nep:CommitteeControl runat="server" ID="CommitteeControl" ValidateGroupName="SaveGeneralInfo" />
                    <asp:CustomValidator ID="CustomValidatorCommittee" runat="server" OnServerValidate="CustomValidatorCommittee_ServerValidate" 
                        client-id="CustomValidatorCommittee"
                                    CssClass="error-text" ValidationGroup="SaveGeneralInfo"                                     
                                    />  
                </div>
            </div>
        </div>
    </div><!--รายชื่อคณะกรรมการ/ตำแหน่ง-->

    <div class="panel panel-default"><!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_RegisterYear%></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm">
                    <label class="col-sm-4 control-label"><%= Model.ProjectInfo_RegisterYear %><span class="required"></span></label>
                    <div class="col-sm-2 ">
                        <asp:TextBox ID="TextBoxRegisterYear" runat="server" CssClass="form-control" Width="160" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegisterYear" ControlToValidate="TextBoxRegisterYear" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_RegisterYear) %>"
                                ValidationGroup="SaveGeneralInfo" SetFocusOnError="true"/>
                    </div>

                    <label class="col-sm-2 control-label org-register-date" runat="server" id="OrganizationRegisterDateLabel" visible="false"><%= Model.ProjectInfo_RegisterDate %></label>
                    <div class="col-sm-4 org-register-date" runat="server" id="OrganizationRegisterDateControl" visible="false">
                        <asp:TextBox ID="TextBoxRegisterDate" runat="server" CssClass="form-control" Width="160" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div><!--ปีที่จดทะเบียนก่อตั้งองค์กรหรือปีที่เริ่มดำเนินการ-->

    <div class="panel panel-default"><!--ที่ตั้งสำนักงาน-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_OfficeLocation %></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">               
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_AddressNo %><span class="required"></span></label>
                    <div class="col-sm-1">
                        <asp:TextBox ID="TextBoxAddressNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                     <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Moo %></label>
                    <div class="col-sm-1">
                        <asp:TextBox ID="TextBoxMoo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxBuilding" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-sm">
                   
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxSoi" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Street %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxStreet" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>
                    <div class="col-sm-4"> 
                        <ajaxToolkit:ComboBox runat="server" ID="ComboBoxProvince" 
                            DropDownStyle="DropDown"                                         
                            AutoCompleteMode="Suggest" 
                            CaseSensitive="false"     
                            CssClass="form-control-combobox"    
                            Enabled="false"
                            DataTextField ="Text"
                            DataValueField="Value" 
                            SelectMethod="GetProvince">
                        </ajaxToolkit:ComboBox> 
                    </div>
                    
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <ajaxToolkit:ComboBox runat="server" ID="ComboBoxDistrict" 
                            DropDownStyle="DropDown"                                         
                            AutoCompleteMode="Suggest" 
                            CaseSensitive="false"     
                            CssClass="form-control-combobox"    
                            Enabled="false"
                            DataTextField ="Text"
                            DataValueField="Value" 
                            SelectMethod="GetDistrict">
                        </ajaxToolkit:ComboBox>
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <ajaxToolkit:ComboBox runat="server" ID="ComboBoxSubDistrict" 
                            DropDownStyle="DropDown"                                         
                            AutoCompleteMode="Suggest" 
                            CaseSensitive="false"     
                            CssClass="form-control-combobox"    
                            Enabled="false"
                            DataTextField ="Text"
                            DataValueField="Value" 
                            SelectMethod="GetSubDistrict">
                        </ajaxToolkit:ComboBox>
                    </div>
                    
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxPostCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxFax" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-sm">
                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxMobile" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %><span class="required"></span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div><!--ที่ตั้งสำนักงาน-->

    <div class="panel panel-default"><!--วัตถุประสงค์ขององค์กร-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_OrganizationObjective %></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">                               
                <div class="form-group form-group-sm">
                    <div class="col-sm-12">
                        <div class="required-block">
                            <asp:TextBox ID="TextBoxOrganizationObjective" runat="server" TextMode="MultiLine" CssClass="form-control  textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrganizationObjective" ControlToValidate="TextBoxOrganizationObjective" runat="server" 
                            CssClass="error-text"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationObjective) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_OrganizationObjective) %>"
                            ValidationGroup="SaveGeneralInfo" />
                    </div>
                </div>
            </div>
        </div>
    </div><!--วัตถุประสงค์ขององค์กร-->

    <div class="row">
        <div class="col-sm-6"><!--กิจกรรมหรือโครงการที่องค์กรดำเนินอยู่ในปัจจุบัน (โดยสรุป)-->
             <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_ActivityCurrent %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">                               
                        <div class="form-group form-group-sm">
                            <div class="col-sm-12">
                                <nep:TextBox ID="TextBoxActivityCurrent" runat="server" TextMode="MultiLine"  CssClass="form-control textarea-height"></nep:TextBox>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div><!--panel-->
        </div><!--กิจกรรมหรือโครงการที่องค์กรดำเนินอยู่ในปัจจุบัน (โดยสรุป)-->

        <div class="col-sm-6"><!--ผลงานในรอบ ๑ ปี ที่ผ่านมา (โดยสรุป)-->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><%= Model.ProjectInfo_WorkingYear %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">                               
                        <div class="form-group form-group-sm">
                            <div class="col-sm-12">
                                <asp:TextBox ID="TextBoxWorkingYear" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div><!--panel-->
        </div><!--ผลงานในรอบ ๑ ปี ที่ผ่านมา (โดยสรุป)-->
    </div>

    <div class="panel panel-default"><!--องค์กรของท่านเคยเสนอโครงการขอรับการสนับสนุน...-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_IsfirstFlag %></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">               
                <div class="form-group form-group-sm">
                    <div class="col-sm-2">
                         <div class="required-block">
                             <asp:RadioButtonList ID="rbLstIsFirstFlag" runat="server" CssClass="form-control-radio" RepeatDirection="Horizontal">
                                <asp:ListItem Text="ไม่เคย" Value="No"></asp:ListItem>
                                <asp:ListItem Text="เคย" Value="Yes"></asp:ListItem>
                            </asp:RadioButtonList>
                            <span class="required"></span>
                         </div>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIsFirstFlag" ControlToValidate="rbLstIsFirstFlag" runat="server" 
                                    CssClass="error-text"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IsfirstFlag) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IsfirstFlag) %>"
                                    ValidationGroup="SaveGeneralInfo" />
                    </div>
                    <div class="col-sm-2 control-label" id="LabelPromoteYear"><%= Model.ProjectInfo_PromoteYear %></div>
                    <div class="col-sm-1" id="ContainerPromoteYearForm">
                        <nep:DatePicker ID="DatePickerPromoteYear" runat="server" Format="yyyy" EnabledTextBox="true" />       
                        <asp:CustomValidator ID="CustomValidator4" runat="server" CssClass="error-text" ValidateEmptyText="true"
                            OnServerValidate="CustomValidatorSupport_ServerValidate" ControlToValidate="DatePickerPromoteYear"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_PromoteYear) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_PromoteYear) %>"
                            ValidationGroup="SaveGeneralInfo" />                 
                    </div>
                    <div class="col-sm-1" id="ContainerPromoteYearTo">    
                        <span class="form-control-left-desc" style="margin-left:-10px;">-</span>                            
                        <nep:DatePicker ID="DatePickerTogotSupportYear" runat="server" Format="yyyy" EnabledTextBox="true" />    
                        <asp:CustomValidator ID="CustomValidatorTogotSupportYear" CssClass="error-text" runat="server" 
                            OnServerValidate="CustomValidatorSupportDate_ServerValidate"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProjectInfo_ToSupportYear, Model.ProjectInfo_StartSupportYear) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProjectInfo_ToSupportYear, Model.ProjectInfo_StartSupportYear) %>"
                            ValidationGroup="SaveGeneralInfo" />                         
                    </div>
                    <div class="col-sm-3 control-label" id="LabelPromoteAmount"><%= Model.ProjectInfo_PromoteAmount %></div>
                    <div class="col-sm-2" id="ContainerPromoteAmount" style="position:relative">
                        <asp:TextBox ID="TextBoxPromoteAmount" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="form-control-desc"><%= UI.LabelOnce %></span>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" CssClass="error-text" ValidateEmptyText="true"
                            OnServerValidate="CustomValidatorSupport_ServerValidate" ControlToValidate="TextBoxPromoteAmount"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_PromoteAmount) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_PromoteAmount) %>"
                            ValidationGroup="SaveGeneralInfo" />
                    </div>                    
                </div>
                <div class="form-group form-group-sm"  id="LabelProjectLasted">
                    <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectLasted %></label>
                    <div class="col-sm-8">
                        <nep:TextBox ID="TextBoxProjectLasted" runat="server" CssClass="form-control"></nep:TextBox>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" CssClass="error-text" ValidateEmptyText="true"
                            OnServerValidate="CustomValidatorSupport_ServerValidate" ControlToValidate="TextBoxProjectLasted"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectLasted) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectLasted) %>"
                            ValidationGroup="SaveGeneralInfo" />
                    </div>
                    <div class="col-sm-4"></div>
                </div>
                <div class="form-group form-group-sm" id="LabelProjectLastedResultAndProblem">
                    <label class="col-sm-6"><%= Model.ProjectInfo_ProjectLastedResult %></label>
                    <label class="col-sm-6"><%= Model.ProjectInfo_Problem %></label>
                </div>
                <div class="form-group form-group-sm" id="ContainerProjectLastedResultAndProblem">
                    <div class="col-sm-6">
                        <nep:TextBox ID="TextBoxProjectLastedResult" runat="server" TextMode="MultiLine" CssClass="form-control  textarea-height"></nep:TextBox>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="error-text" ValidateEmptyText="true"
                            OnServerValidate="CustomValidatorSupport_ServerValidate" ControlToValidate="TextBoxProjectLastedResult"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectLastedResult) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectLastedResult) %>"
                            ValidationGroup="SaveGeneralInfo" />
                    </div>
                    <div class="col-sm-6">
                        <asp:TextBox ID="TextBoxProblem" runat="server" TextMode="MultiLine" CssClass="form-control  textarea-height"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidatorProblem" runat="server" CssClass="error-text" ValidateEmptyText="true"
                            OnServerValidate="CustomValidatorSupport_ServerValidate" ControlToValidate="TextBoxProblem"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Problem) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Problem) %>"
                            ValidationGroup="SaveGeneralInfo" />
                    </div>
                </div>                
            </div>
        </div>
    </div><!--<!--องค์กรของท่านเคยเสนอโครงการขอรับการสนับสนุน...-->

    <div class="panel panel-default"><!--แหล่งขอความช่วยเหลือที่องค์กรได้รับในปัจจุบัน (ทั้งในและต่างประเทศ)-->
        <div class="panel-heading">
            <h3 class="panel-title"><%= Model.ProjectInfo_Assistances %></h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">                
                <div class="form-group form-group-sm">
                    <div class="col-sm-12">
                        <nep:OrgAssistanceControl ID="OrgAssistanceControl" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div><!--แหล่งขอความช่วยเหลือที่องค์กรได้รับในปัจจุบัน (ทั้งในและต่างประเทศ)-->

      
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                     <asp:Button runat="server" ID="ButtonDraft" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="บันทึกร่าง"  OnClick="ButtonSave_Click" />
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave%>" ValidationGroup="SaveGeneralInfo" OnClick="ButtonSave_Click" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm"
                        Text="" 
                        OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click" Visible="false"/>   

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />  

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>

                    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>

                    

                    <asp:HyperLink ID="ButtonCancel" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
        
        <script type="text/javascript">
            $(function () {               
                c2x.handleTextAreaMaxlength(['TextBoxActivityCurrent', 'TextBoxWorkingYear', 'TextBoxOrganizationObjective', 'TextBoxProjectLastedResult']);
                
            });

           

            function validateOrganizationType(oSrc, args) {
                var radioChecked = $('.organization-type-radio input:checked');
                var isValid = false;
                if (radioChecked.length > 0) {
                    isValid = true;
                }
                args.IsValid = isValid;
            }
            
            function validateOrganizationProvince(oSrc, args) {
                var selectedValue = $find("<%=ComboBoxOrganizationProvince.ClientID%>").get_hiddenFieldControl().value;
                var isValid = false, selectedIndex;
                if ((selectedValue != "") && (!isNaN(selectedValue))) {
                    selectedIndex = parseInt(selectedValue, 10);
                    isValid = (selectedIndex < 0) ? false : true;
                }
                args.IsValid = isValid;
            }

            function handelProjectIsGotSupport() {
                var radio = $('#<%=rbLstIsFirstFlag.ClientID%>').find('input');
               $.each(radio, function (index, item) {                   
                   $(item).change(function () {
                       showOrHideProjectIsGotSupport(item);
                   });

                   showOrHideProjectIsGotSupport(item);
               });

           }

           function showOrHideProjectIsGotSupport(el) {
               var value = (el.value).toLowerCase();
               if (el.checked && (value == 'yes')) {
                   $('#LabelPromoteYear').show();
                   $('#ContainerPromoteYearForm').show();
                   $('#ContainerPromoteYearTo').show();
                   $('#LabelPromoteAmount').show();
                   $('#ContainerPromoteAmount').show();
                   $('#LabelProjectLasted').show();
                   $('#LabelProjectLastedResultAndProblem').show();
                   $('#ContainerProjectLastedResultAndProblem').show();
               } else {
                   $('#LabelPromoteYear').hide();
                   $('#ContainerPromoteYearForm').hide();
                   $('#ContainerPromoteYearTo').hide();
                   $('#LabelPromoteAmount').hide();
                   $('#ContainerPromoteAmount').hide();
                   $('#LabelProjectLasted').hide();
                   $('#LabelProjectLastedResultAndProblem').hide();
                   $('#ContainerProjectLastedResultAndProblem').hide();
               }
           }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>


