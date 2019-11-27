<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabProsecuteControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabProsecuteControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelPersonal" UpdateMode="Conditional" runat="server" >
    <ContentTemplate>
        <style type="text/css"> 
            .combobox-width {
                width:100%;
            }
        </style>

        <asp:HiddenField runat="server" ID="HiddenProjectID" />
        <div id="divVueQN">
        <div class="panel panel-default"> <!--ผู้รับผิดชอบโครงการ-->
            <div class="panel-heading">
                <h3 class="panel-title">การดำเนินคดีตามกฎหมาย</h3>
            </div>
            <div class="panel-body form-horizontal" style="font-weight:normal;font-size:11px">
                               
                    <div class="form-group form-group-sm">
                         <label class="col-sm-4 control-label control-label-left without-delimit">ชื่อโครงการที่จะฟ้องดำเนินคดีตามกฎหมาย :</label>
                        <asp:Label runat="server" ID="LabelProjectName" class="col-sm-8"></asp:Label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-4 control-label control-label-left without-delimit">ชื่อองค์กรที่จะฟ้องดำเนินคดีตามกฎหมาย :</label>
                        <asp:Label runat="server" ID="LabelORGName" class="col-sm-8"></asp:Label>
                    </div>              
            </div>

            <div class="panel-heading">
                <h3 class="panel-title">ข้อมูลผู้ใช้งานที่จะฟ้องดำเนินคดี</h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_IDCardNo %><span class="required" id="LabelIDCardNoSign" runat="server"></span></label>
                        <div class="col-sm-4 control-block">
                            <nep:TextBox ID="TextBoxPersonalMainIDCardNo" runat="server" CssClass="form-control" TextMode="SingleLine" v-model="items[field.tbCitizenID].v"  />                            
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPersonalMainIDCardNo"  runat="server" ID="MaskedEditExtenderPersonalMainIDCardNo"
                                Mask="9\-9999\-99999\-99\-9" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"
                                />     
                            <asp:CustomValidator ID="CustomValidatorIDCardNo" ControlToValidate="TextBoxPersonalMainIDCardNo" runat="server" CssClass="error-text"
                                ClientValidationFunction="c2x.ClientValidateCitizenIdRequiredValidator" ValidateEmptyText="true"
                                OnServerValidate="CustomValidatorIDCardNo_ServerValidate" 
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IDCardNo) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IDCardNo) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>    
                                                
                            <nep:IDCardNumberValidator ID="IDCardNumberValidatorMainIDCardNo" ControlToValidate="TextBoxPersonalMainIDCardNo" runat="server" 
                                CssClass="error-text"  SetFocusOnError="true"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                                ValidationGroup="SavePersonal"  />
                                               
                        </div>
                    </div> <!-- เลขที่บัตรประชาชน -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Firstname %><span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">
                            <asp:DropDownList ID="DropDownListPrefix1" runat="server" CssClass="form-control" Width="90"  v-model="items[field.ddlTitle].v"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrefix1" ControlToValidate="DropDownListPrefix1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>

                            <asp:TextBox ID="TextBoxFirstName1" runat="server" MaxLength="100" CssClass="form-control" Width="195" v-model="items[field.tbFirstName].v"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName1" ControlToValidate="TextBoxFirstName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Lastname %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxLastName1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbLastName].v"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName1" ControlToValidate="TextBoxLastName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                    </div> <!-- ชื่อ-สกุล -->
                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Address %><span class="required"></span></label>                        
                        <div class="col-sm-2">                            
                            <asp:TextBox ID="TextBoxAddress1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbAddNo].v"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress1" ControlToValidate="TextBoxAddress1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-1 control-label"><%= Model.ProjectInfo_Moo %></label>
                        <div class="col-sm-1">                            
                            <asp:TextBox ID="TextBoxMoo1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbMoo].v"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxBuilding1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbBuilding].v"></asp:TextBox>
                        </div>
                    </div> <!-- บ้านเลขที่ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>                        
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxSoi1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbSoi].v"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Road %></label>
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxRoad1" runat="server" MaxLength="100" CssClass="form-control" v-model="items[field.tbStreet].v"></asp:TextBox>
                        </div>                
                    </div> <!-- ซอย -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlProvince1" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorProvince1" runat="server" ControlToValidate="DdlProvince1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        />	
                                                  
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlDistrict1" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorDistrict1" runat="server" ControlToValidate="DdlDistrict1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        />
                                           
                        </div>                
                    </div> <!-- ตำบล -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlSubDistrict1" runat="server" CssClass="combobox-width" />
                             <asp:CustomValidator ID="CustomValidatorSubDistrict1" runat="server" ControlToValidate="DdlSubDistrict1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        /> 
                                                   
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                        <div class="col-sm-4">  
                            <nep:TextBox ID="TextBoxPostCode1" MaxLength="10" runat="server" CssClass="form-control"  v-model="items[field.tbZipCode].v"></nep:TextBox> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode1" ControlToValidate="TextBoxPostCode1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>        
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode1"  runat="server" ID="MaskedEditExtenderPostCode1"
                                        Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"  />                                   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostCode1" ControlToValidate="TextBoxPostCode1" runat="server"
                                CssClass="error-text" ValidationExpression="\d{5}"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ValidationGroup="SavePersonal" />                         
                        </div>                
                    </div> <!-- จังหวัด -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required" ></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxTelephone1" runat="server" MaxLength="30" CssClass="form-control" v-model="items[field.tbTel].v"></asp:TextBox>     
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone1" ControlToValidate="TextBoxTelephone1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>                       
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxMobile1" runat="server" CssClass="form-control" MaxLength="30" v-model="items[field.tbMobile].v"></asp:TextBox>
                        </div>    
                    </div> <!-- โทรศัพท์ -->

                    <div class="form-group form-group-sm">
                         <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxFax1" runat="server" MaxLength="20" CssClass="form-control" v-model="items[field.tbFax].v"></asp:TextBox>    
                        </div>          

                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %><span class="required" id="LabelEmail1Sign" runat="server"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxEmail1" runat="server" MaxLength="50" CssClass="form-control"  v-model="items[field.tbEmail].v"></asp:TextBox>         
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
                    </div> <!-- อีเมล์ -->  
                                     
                </div><!--form-horizontal-->
            </div><!--panel-body-->
        </div> <!--\ผู้รับผิดชอบโครงการ-->
       <div class="panel panel-default"><!--ถูกดำเนินคดีเรื่อง -->
            <div class="panel-heading">
                <h3 class="panel-title">ถูกดำเนินคดีเรื่อง</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">

                            <div class="form-group form-group-sm">
                                <label class="col-sm-3 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span>ถูกดำเนินคดีเรื่อง
                                </label>

                                <div class="col-sm-4">    
                                    <asp:DropDownList ID="ddlProsecute" runat="server" Width="100%" CssClass="form-control" v-model="items[field.ddlProsecute].v"></asp:DropDownList>
                                </div>   
                                <div class="col-sm-5">    
                                    <label class="control-label control-label-left without-delimit">
                                   อื่นๆ ระบุ : </label><asp:TextBox runat="server" ID="txbOther" v-model="items[field.tbOther].v"></asp:TextBox>
                                </div>                     
                            </div>
                        </div>
                    </div><!--form-group-->                    
                </div><!--form-horizontal-->
            </div>
        </div><!--ถูกดำเนินคดีเรื่อง-->
        <div class="panel panel-default"><!--เอกสารสรุปสำนวนคดี-->
            <div class="panel-heading">
                <h3 class="panel-title">เอกสารสรุปสำนวนคดี</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">

                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span>โปรดแนบเอกสาร
                                </label>
                                <div class="col-sm-8">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadProsecute" SkipScript="true" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div>
                        </div>
                    </div><!--form-group-->                    
                </div><!--form-horizontal-->
            </div>
        </div><!--เอกสารสรุปสำนวนคดี-->
        <div class="panel panel-default"><!--เอกสารประกอบการดำเนินคดีอื่นๆ-->
            <div class="panel-heading">
                <h3 class="panel-title">เอกสารประกอบการดำเนินคดีอื่นๆ</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">

                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span>โปรดแนบเอกสาร
                                </label>
                                <div class="col-sm-8">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadProsecute2" SkipScript="true"  MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div>
                        </div>
                    </div><!--form-group-->                    
                </div><!--form-horizontal-->
            </div>
        </div><!--เอกสารประกอบการดำเนินคดีอื่นๆ-->    
        <div class="col-sm-6 panel-default"><!--เอกสารประกอบการดำเนินคดีอื่นๆ-->
            <div class="panel-heading">
                <h3 class="panel-title">เจ้าหน้าที่กลุ่มกฎหมาย</h3>
            </div>
        </div>            
         <div class="col-sm-6 panel-default"><!--เอกสารประกอบการดำเนินคดีอื่นๆ-->
            <div class="panel-heading">
                <h3 class="panel-title">ชื่อเจ้าหน้าที่องค์กรที่ถูกฟ้องดำเนินคดีตามกฎหมาย</h3>
            </div>
        </div>    
                        
            <div class="panel-body">
                <div class="form-horizontal">                  
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ชื่อ - นามสกุล<span class="required"></span></label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" CssClass="form-control" v-model="items[field.tbOfficerFirst].v"></asp:TextBox>
                              
                            </div>   
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" CssClass="form-control" v-model="items[field.tbOfficerLast].v"  ></asp:TextBox>
                            </div>

                            <label class="col-sm-2 control-label">ชื่อ - นามสกุล<span class="required"></span></label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxReceiverName" runat="server" MaxLength="50" CssClass="form-control" v-model="items[field.tbOrgFirst].v" ></asp:TextBox>
                              
                            </div>   
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxReceiverSurname" runat="server" MaxLength="50" CssClass="form-control" v-model="items[field.tbOrgLast].v"  ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ตำแหน่ง</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="100" CssClass="form-control"  v-model="items[field.tbOfficerPosition].v" ></asp:TextBox>
                            </div>  
                            <label class="col-sm-2 control-label">ตำแหน่ง</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxReceiverPosition" runat="server" MaxLength="100" CssClass="form-control"  v-model="items[field.tbOrgPosition].v" ></asp:TextBox>
                            </div>  
                        </div>        
       </div>
    </div> <!-- end divVueQN -->
                <p></p>
             
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSavePersonal" CssClass="btn btn-primary btn-sm" ValidationGroup="SavePersonalx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"  OnClientClick="appVueQN.param.IsReported = '0';appVueQN.saveData();SaveAttachmentFiles(); return false;"  Visible="true" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click" Visible="false"/>

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />  

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>

                    

                    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>                 

                    

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
              

    </ContentTemplate>
</asp:UpdatePanel><!--UpdatePanelPersonal-->

