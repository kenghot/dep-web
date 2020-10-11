<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabAssessmentControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabAssessmentControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>




<asp:UpdatePanel ID="UpdatePanelAssessment" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>  
        <style>
            .style-label {
                display:inline-block;
                padding-top:4px;
            }

            .label-no {
                width:25px;
            }
        </style>
        
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TabTitleAssessmentProject %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title" style="text-align:center"><%= UI.TitleAssessmentForm %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-6 form-group-title"><%= Model.ProjectInfo_AssessmentProvince %>:</label>
                        <div class="col-sm-4">
                            <asp:Label ID="LabelOfficerProvince" runat="server" />                           
                            <asp:HiddenField ID="HiddenFieldOfficerProvince" runat="server" />
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_ProjectNo1 %></label>                
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelProjectNo" runat="server"></asp:Label>                            
                        </div>                             
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationName %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="OrganizationName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_Name %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelProjectName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_BudgetRequest %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelBudget" runat="server" />
                            <%= UI.LabelBath %>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo4 %></div>               
                        <div class="col-sm-9">
                            <asp:RadioButtonList ID="RadioButtonListIsPassAss4" runat="server" CssClass="form-control-radio criterion-no-4" RepeatDirection="Horizontal">
                                 <asp:ListItem Text="<%$ code:UI.LabelPass %>" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="<%$ code:UI.LabelNotPass %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo5 %></div>                
                        <div class="col-sm-9">
                            <asp:RadioButtonList ID="RadioButtonListIsPassAss5" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$ code:UI.LabelPass %>" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="<%$ code:UI.LabelNotPass %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo6 %></div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ก)</span><%=Nep.Project.Resources.UI.LabelAssessmentProjectName %>                                        
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentProjectName" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div> 
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ข)</span><%=Nep.Project.Resources.UI.LabelAssessmentReason %>                                        
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentReason" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div>  
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ค)</span><%=Nep.Project.Resources.UI.LabelAssessmentObjective %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentObjective" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div>    
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ง)</span><%=Nep.Project.Resources.UI.LabelAssessmentTargetGroup %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentTargetGroup" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div> 
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(จ)</span><%=Nep.Project.Resources.UI.LabelAssessmentLocation %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentLocation" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div>  
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ฉ)</span><%=Nep.Project.Resources.UI.LabelAssessmentTiming %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentTiming" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div>  
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ช)</span><%=Nep.Project.Resources.UI.LabelAssessmentOperationMethod %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentOperationMethod" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div> 
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ซ)</span><%=Nep.Project.Resources.UI.LabelAssessmentBudget %>
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentBudget" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div> 
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">(ณ)</span><%=Nep.Project.Resources.UI.LabelAssessmentExpection %>
                                        <asp:HiddenField ID="HiddenField7" runat="server" Value='<%# Eval("StandardID") %>' />
                                    </label>                   
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="DropDownListAssessmentExpection" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
                                    </div>                                        
                                </div> 
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <span class="form-control-left-desc"><%=UI.LabelTotal %></span>
                            <asp:Label id="TotalScore"  runat="server" Text="-" CssClass="style-label total-score"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="TotalScoreDesc" runat="server" Text="" CssClass="style-label total-score-desc"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12"><%= Model.ProjectInfo_CommentOther %></label>
                    </div>                
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssessmentDesc" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProjectInfo_Strategic %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectInfo_StandardStrategic %><span class="required"></span></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">                            
                            <asp:RadioButtonList ID="RadioButtonListStandardStrategics" runat="server" 
                                CssClass="form-control-checkbox-horizontal standard-strategic-checkbox" 
                                RepeatDirection="Vertical" ></asp:RadioButtonList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorStandardStrategics" runat="server" ControlToValidate="RadioButtonListStandardStrategics" 
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_StandardStrategic) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_StandardStrategic) %>"
                                ValidationGroup="SaveAssessment"/>
                           
                        </div>                  
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12"><%= Model.ProjectInfo_StrategicProvice %></label>
                    </div>                
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxProvinceMissionDesc" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" OnClick="ButtonSave_Click"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" ValidationGroup="SaveAssessment" Visible="false" />

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>

       
    </ContentTemplate>
</asp:UpdatePanel>



