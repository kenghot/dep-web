<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabProcessingPlanControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabProcessingPlanControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>
<%@ Register TagPrefix="nep" TagName="AttachmentProvideControl1" Src="~/ProjectInfo/Controls/AttachmentProvideControl.ascx" %>

<asp:UpdatePanel ID="UpdatePanelProcessingPlan" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="HiddenProjectID" />
       
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProcessingPlan_Location %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm" id="AddPlanAddress" runat="server">
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="ButtonAddPlanAddress" CssClass="btn btn-default btn-sm" 
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" 
                                OnClientClick="return c2xPlan.openAddressForm();" /> 
                            <asp:Image ID="ImageHelp2" runat="server" ToolTip="เมื่อต้องการเพิ่มสถานที่ดำเนินโครงการให้กดที่คำว่าเพิ่ม" ImageUrl="~/Images/icon/about.png" BorderStyle="None" />          
                        </div>
                        <div class="col-sm-10">
                            <span class="field-desc"><%: UI.LabelFieldDescription %></span><%= Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.LocationDesc)%>   
                        </div>
                    </div> 
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:HiddenField ID="HiddOperationAddress" runat="server" />
                           
                            <div id="OperationAddressGrid" runat="server" ></div>
                           
                            
                            <asp:CustomValidator ID="CustomValidatorOperationAddress" CssClass="error-text operation-address-validator" runat="server" 
                                 ClientValidationFunction="validatorOperationAddress" ValidateEmptyText="true"
                                 OnServerValidate="CustomValidatorOperationAddress_ServerValidate"
                                 Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProcessingPlan_Location) %>" 
                                 ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProcessingPlan_Location) %>"
                                 ValidationGroup="SaveProcessingPlan" />
                        </div>
                    </div>           
                      <div class="form-group form-group-sm"  id="divHistoryAddress" style="color:gray;" runat="server" visible="false">
                        <label class="col-sm-12" style="color:gray;">สถานที่ดำเนินโครงการเดิม</label>
                        <div class="col-sm-12">
                            <asp:Label ID="LabelOperationAddressOld" runat="server" Text=""></asp:Label>
                        </div>
                    </div>       
                    
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProcessingPlan_Period%></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProcessingPlan_StartDate %><span class="required"></span></label>
                            <div class="col-sm-8">
                                <nep:DatePicker runat="server" ID="ProcessingPlanStartDate" ClearTime="true" EnabledTextBox="true" 
                                    ValidationGroup="SaveProcessingPlan" 
                                    OnClientDateSelectionChanged="onProcessingPlanDateSelectionChanged" 
                                    OnClientDateTextChanged="onProcessingPlanDateSelectionChanged(null, null)"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlanStartDate" ControlToValidate="ProcessingPlanStartDate" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_StartDate) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_StartDate) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>
                            </div>                  
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProcessingPlan_EndDate %><span class="required"></span></label>
                            <div class="col-sm-8">
                                <nep:DatePicker runat="server" ID="ProcessingPlanEndDate" ClearTime="true" EnabledTextBox="true"
                                     ValidationGroup="SaveProcessingPlan" 
                                    OnClientDateSelectionChanged="onProcessingPlanDateSelectionChanged" 
                                    OnClientDateTextChanged="onProcessingPlanDateSelectionChanged(null, null)"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlanEndDate" ControlToValidate="ProcessingPlanEndDate" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_EndDate) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_EndDate) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>

                                <asp:CustomValidator ID="CustomValidatorPlanDate" CssClass="error-text" runat="server" 
                                    OnServerValidate="CustomValidatorPlanDate_ServerValidate"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProcessingPlan_EndDate, Model.ProcessingPlan_StartDate) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProcessingPlan_EndDate, Model.ProcessingPlan_StartDate) %>"
                                    ValidationGroup="SaveProcessingPlan" />
                            </div>                  
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-4 control-label"><%= Model.ProcessingPlan_TotalPeriod %><span class="required"></span></label>
                            <div class="col-sm-8">
                                <nep:TextBox ID="TextBoxTotalPeriod" runat="server" CssClass="form-control total-processing-period" 
                                    Min="1" NumberFormat="N0"></nep:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTotalPeriod" ControlToValidate="TextBoxTotalPeriod" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_TotalPeriod) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_TotalPeriod) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>

                                <asp:CustomValidator ID="CustomValidatorTotalPeriod" CssClass="error-text" runat="server" OnServerValidate="CustomValidatorTotalPeriod_ServerValidate"
                                 Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Model.ProcessingPlan_TotalPeriod, UI.LabelValidateProcessingTotalPeriod) %>" 
                                 ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Model.ProcessingPlan_TotalPeriod, UI.LabelValidateProcessingTotalPeriod) %>"
                                 ValidationGroup="SaveProcessingPlan" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="col-sm-12">
                                <asp:TextBox ID="TextBoxTimeDesc" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.PeriodDesc) %>                
                    </div>
                </div>
                 <%--ประวัติการแก้ไข Contract_StartDate Contract_EndDate --%> 
               <div class="form-horizontal" id="divHistoryEditStartEndDate" style="color:gray;" runat="server" visible="false" >
                        <label class="col-sm-12" style="color:gray;">ระยะเวลาเดิม</label>
                        <div class="col-sm-12">
                            <asp:Label ID="LabelHistoryEditStartEndDate" runat="server" Text=""></asp:Label>
                 </div>
            </div>
                 
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProcessingPlan_Method%></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="col-sm-8">
                        <div  class="required-block">
                            <asp:TextBox ID="TextBoxMethod" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            <span class="required"></span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMethod" ControlToValidate="TextBoxMethod" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_Method) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_Method) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>
                    </div>
                    <div class="col-sm-4">
                        <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProcessingMethodDesc) %>                
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                     <asp:Button runat="server" ID="ButtonDraft" CssClass="btn btn-primary btn-sm"  
                        Text="บันทึกร่าง"  OnClick="ButtonSave_Click" Visible="false"/>
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProcessingPlan"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"  OnClick="ButtonSave_Click" Visible="false"/>

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click" Visible="false"/>

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
</asp:UpdatePanel>

