<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabProjectInfoControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabProjectInfoControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelProjectInfo" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>         
        <style type="text/css" >
            .attachment-block input{
                float:left;
            }
            
            .button-add-targetgroup, .button-clear-targetgroup {
                margin-top:6px;
                margin-right:5px;
                opacity:.6;

            }

             .button-add-targetgroup[disabled="disabled"]:hover, .button-clear-targetgroup[disabled="disabled"]:hover {
                opacity:.6;
            }
            
            .button-add-targetgroup:hover, .button-clear-targetgroup:hover {
                opacity:1;
            }


            .ddl-project-target-group .k-dropdown-wrap{
                width:95%;
            }

            .project-target-group-etc {
                width:100%;
            }

            .btn-hide {
                display:none;
            }

            .form-horizontal.custom-padd .col-sm-7,
            .form-horizontal.custom-padd .col-sm-1,
            .form-horizontal.custom-padd .col-sm-2{
                padding-right:3px;
                padding-left:3px;
            }
        </style>
        
        <div class="row">
            <div class="col-sm-8"><!--ข้อมูลโครงการ-->
                <div class="panel panel-default"  >
                    <div class="panel-heading">
                        <h3 class="panel-title"><%= Model.ProjectInfo_Name %></h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal"> 
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectNo1 %></label>
                                <div class="col-sm-2 control-value">
                                    <asp:Label ID="LabelProjectNo" runat="server" ></asp:Label>
                                </div>   
                                
                                <label class="col-sm-3 control-label"><%= Model.ProjectInfo_BudgetYear %></label>
                                <div class="col-sm-2 control-value">                                   
                                    <asp:Label ID="LabelBudgetYear" runat="server" Text="-"></asp:Label>
                                </div>        
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_SubmitedDate %></label>
                                <div class="col-sm-8 control-value">
                                    <asp:Label ID="LabelSubmitedDate" runat="server" Text="-"></asp:Label>                                    
                                </div> 
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectInfoNameTH %><span class="required"></span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxProjectInfoNameTH" runat="server" CssClass="form-control" MaxLength="300"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProjectInfoNameTH" ControlToValidate="TextBoxProjectInfoNameTH" runat="server" 
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoNameTH) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoNameTH) %>"
                                    ValidationGroup="SaveProjectInfo" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectInfoNameEN %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxProjectInfoNameEN" runat="server" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectInfoType %><span class="required"></span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownListProjectInfoType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProjectInfoType" ControlToValidate="DropDownListProjectInfoType" 
                                        runat="server" CssClass="error-text" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoType) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoType) %>"
                                        ValidationGroup="SaveProjectInfo" />
                                </div>                  
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label"><%= Model.ProjectInfo_ProjectInfoStartDate %><span class="required"></span></label>
                                <div class="col-sm-8">
                                    <nep:DatePicker runat="server" ID="ProjectInfoStartDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveProject"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"
                                        OnClientDateSelectionChanged="onProjectInfoStartDateChanged" 
                                        OnClientDateTextChanged="onProjectInfoStartDateChanged(null, null)"/> 

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" ControlToValidate="ProjectInfoStartDate" 
                                        runat="server" CssClass="error-text" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoStartDate) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoStartDate) %>"
                                        ValidationGroup="SaveProjectInfo" />
                                </div>                  
                            </div> 
                        </div>
                    </div>
                </div>
            </div><!--ข้อมูลโครงการ-->

            <div class="col-sm-4"><!--ประเภทความพิการที่ขอรับการสนับสนุน-->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><%= Model.ProjectInfo_TypeDisabilitys %></h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">  
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <div class="required-block">
                                        <asp:RadioButtonList ID="RadioButtonListTypeDisabilitys" runat="server" CssClass="form-control-checkbox" RepeatDirection="Vertical"></asp:RadioButtonList>
                                        <span class="required"></span>
                                    </div>
                                    <asp:CustomValidator ID="CustomValidatorTypeDisabilitys" ClientValidationFunction="ValidateRadioButtonList" 
                                        CssClass="error-text" runat="server" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_TypeDisabilitys) %>" 
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_TypeDisabilitys) %>"
                                        ValidationGroup="SaveProjectInfo" />
                                </div>                  
                            </div>
                        </div>
                    </div>
                </div>
            </div><!--ประเภทความพิการที่ขอรับการสนับสนุน-->          
        </div>
               
        <div class="panel panel-default">
            <div class="panel-heading"><!-- หลักการและเหตุผล -->
                <h3 class="panel-title"><%= Model.ProjectInfo_Principles %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div class="required-block">
                            <asp:TextBox ID="TextBoxPrinciples" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrinciples" ControlToValidate="TextBoxPrinciples" runat="server" 
                            CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>"
                            ValidationGroup="SaveProjectInfo" />
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.PrinciplesDesc) %>                
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading"><!-- วัตถุประสงค์ของโครงการ -->
                <h3 class="panel-title"><%= Model.ProjectInfo_ProjectInfoObjective %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div class="required-block">
                            <asp:TextBox ID="TextBoxProjectInfoObjective" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorProjectInfoObjective" ControlToValidate="TextBoxProjectInfoObjective" runat="server" 
                            CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>"
                            ValidationGroup="SaveProjectInfo" />                        
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectInfoObjectiveDesc) %>                               
                    </div>
                </div>
            </div>
        </div>       
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProjectInfo_ProjectInfoTarget %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">                       

                        <div class="form-horizontal custom-padd">                            
                            <div class="form-group form-group-sm" id="CreateProjectGroupForm" runat="server">
                                <div class="col-sm-7">
                                    <div>
                                        <nep:TextBox ID="TextBoxProjectTarget" runat="server"  Width="100%" PlaceHolder="กลุ่มเป้าหมาย"  ClientIDMode="Static" AutoPostBack="false"/>                       
                                        <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroup" runat="server"
                                            data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxProjectTarget.ClientID %>"
                                            style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>
                                        </span>
                                    </div>
                                    <div id="ProjectTargetEtcBlock" style="display:none; margin-top:7px;">
                                        <nep:TextBox ID="TextBoxProjectTargetEtc" MaxLength="1333" runat="server" PlaceHolder="ชื่อกลุ่มเป้าหมาย" CssClass="form-control" Width="360px"
                                                />
                                        <span class="project-target-group-validate error-text"  id="ValidateProjectTargetGroupEtc" runat="server"
                                            data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxProjectTargetEtc.ClientID %>"
                                            style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  "ชื่อกลุ่มเป้าหมายอื่นๆ") %>
                                        </span>
                                    </div>
                                    <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroupDupCreate" 
                                            data-val-validationgroup="SaveProjectTargetGroup" 
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>
                                    </span>
                                </div>
                                
                                <div class="col-sm-3">
                                    <label style="font-weight:normal; float:left;margin-top:4px;padding-left:6px; padding-right:10px;">จำนวน</label>
                                    <nep:TextBox ID="TextBoxProjectTargetAmount" runat="server" Width="100px" PlaceHolder="รวม" TextMode="Number" 
                                        NumberFormat="N0"  Min="1" Max="99999999" CssClass="form-control" />
                                    <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroupAmount" runat="server"
                                        data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxProjectTargetAmount.ClientID %>"
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_Amount) %>
                                    </span>
                                </div>
                                <div class="col-sm-2" style="padding-left:15px;">
                                    <asp:ImageButton ID="ImageButtonCancelProductTargetGroupTemp" runat="server" OnClientClick="return false;" CssClass="btn-hide" />
                    
                                    <asp:ImageButton ID="ImageButtonSaveProductTargetGroup" runat="server" ToolTip="เพิ่ม" 
                                        ImageUrl="~/Images/icon/round_plus_icon_16.png" BorderStyle="None" CssClass="button-add-targetgroup"
                                        OnClientClick="return c2xProjectInfo.createRowProjectTargetGroup(event)"/>
                                    <asp:ImageButton ID="ImageButtonCancelProductTargetGroup" runat="server" ToolTip="ล้างข้อมูล" 
                                        ImageUrl="~/Images/icon/brush_icon_16.png" BorderStyle="None" CssClass="button-clear-targetgroup"
                                        OnClientClick="return c2xProjectInfo.cancelCreateRowProjectTargetGroup();"/>
                                   <asp:Image ID="ImageHelp2" runat="server" ToolTip="เมื่อเลือกกลุ่มเป้าหมายและระบุจำนวนผู้เข้าร่วมแล้ว ให้กดที่เครื่องหมาย + เพือเพิ่มข้อมูลเข้าสู่ระบบ" ImageUrl="~/Images/icon/about.png" BorderStyle="None" /> 
                                </div>  
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <asp:HiddenField ID="HiddenFieldProjectTargetGroup" runat="server" />
                                    <div id="ProductTargetGroupGrid" runat="server"></div>
                                </div>                               
                                <div class="form-group form-group-sm">                                    
                                    <label class="col-sm-1 control-label">
                                        <%=UI.LabelTotal %>
                                    </label>
                                    <div class="col-sm-11 control-value">
                                        <asp:Label ID="LabelTotalParticipant" CssClass="total-targetgroup" runat="server" Text="0"></asp:Label> <%=UI.LabelPerson %>
                                    </div>
                                </div>
                                
                            </div>
                        </div><!-- CreateProjectGroupForm -->                                              

                        <asp:CustomValidator ID="CustomValidatorTargetGroup" CssClass="error-text" runat="server" 
                             ClientValidationFunction="projectTargetGroupValidate"
                             OnServerValidate="CustomValidatorTargetGroup_ServerValidate"
                             Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectInfo_ProjectInfoTarget) %>" 
                             ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectInfo_ProjectInfoTarget) %>"
                             ValidationGroup="SaveProjectInfo" />
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectInfoTargetDesc) %>                               
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading"><!-- ตัวชี้วัดโครงการ -->
                <h3 class="panel-title"><%= Model.ProjectInfo_ProjectInfoindicator %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div class="required-block">
                            <asp:TextBox ID="TextBoxProjectInfoindicator" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIndicator" ControlToValidate="TextBoxProjectInfoindicator" runat="server" 
                            CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoindicator) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoindicator) %>"
                            ValidationGroup="SaveProjectInfo" />                          
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectInfoindicatorDesc) %>              
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading"><!-- ผลที่คาดว่าจะได้รับ -->
                <h3 class="panel-title"><%= Model.ProjectInfo_ProjectInfoAnticipation %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div class="required-block">
                            <asp:TextBox ID="TextBoxProjectInfoAnticipation" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>                 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAnticipation" ControlToValidate="TextBoxProjectInfoAnticipation" runat="server" 
                            CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>"
                            ValidationGroup="SaveProjectInfo" />       
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectInfoAnticipationDesc) %>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default" id="RejectCommentBlock" runat="server" visible="false">
            <div class="panel-heading"><!-- ข้อแนะนำสำหรับการปรับปรุง -->
                <h3 class="panel-title"><%= Model.ProjectInfo_RejectComment %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-12">
                        <nep:TextBox TextMode="MultiLine" ID="TextBoxRejectComment" runat="server" CssClass="form-control textarea-height" />
                    </div>                   
                </div>
            </div>
        </div>

        <div class="panel panel-default" id="CancelledProjectRequestBlock" runat="server" visible="false">
            <div class="panel-heading"><!-- หนังสือรับรองการขอยกเลิกคำร้อง -->
                <h3 class="panel-title"><%= Model.ProjectInfo_CancelledProjectRequestAttachment %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-12 attachment-block">
                        <asp:ImageButton ID="ImageButtonEditCancelledAttachment" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png"                             
                            OnClientClick="c2x.clearResultMsg(); return openCancelProjectRequestForm();"/>
                        <nep:C2XFileUpload runat="server" ID="C2XFileUploadCancelledProjectRequest" ViewAttachmentPrefix="<%$ code:CancelledProjectRequestPrefix %>" Enabled="false" />  
                    </div>                   
                </div>
            </div>
        </div>

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonDraft" CssClass="btn btn-primary btn-sm"  
                        Text="บันทึกร่าง"  OnClick="ButtonSave_Click" Visible="false"/>
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectInfo"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"  OnClick="ButtonSave_Click" Visible="false"/>

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                        OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click" />

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />  

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>                    

                    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>

                    <asp:Button ID="ButtonCancelProjectRequest" runat="server" CssClass="btn btn-default btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancelProjectRequest %>" 
                        Visible="false" OnClientClick="c2x.clearResultMsg(); return openCancelProjectRequestForm();" />

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
        
    </ContentTemplate>
</asp:UpdatePanel>
