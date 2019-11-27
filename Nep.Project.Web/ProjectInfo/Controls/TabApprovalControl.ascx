<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabApprovalControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabApprovalControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>
<%@ Register Src="~/ProjectInfo/Controls/ApproveBudgetGridControl.ascx" TagPrefix="uc1" TagName="ApproveBudgetGridControl" %>


<style type="text/css">
    .radio-button-group.radio-button-group-approval input, 
    .radio-button-group.radio-button-group-approval label{
        float:left;
        margin-right:5px;
    }

    .radio-button-group.radio-button-group-approval div {
        clear:left;
    }

    .radio-button-group.radio-button-group-approval div:nth-child(2) {
        padding-top:15px;
        padding-bottom:12px;
    }

    .radio-button-group.radio-button-group-approval div:nth-child(2) > input.form-control {
        width:158px;
    }

    .radio-button-group.radio-button-group-approval div:nth-child(3) > input.form-control {
        width:425px;
    }

    .radio-button-group.radio-button-group-approval input[type="text"] {
        margin-top:-4px;
    }
    .project-approval-grid th:first-child, .project-approval-grid td:first-child {
        display:none;
        border:none;
    }

    .project-approval-grid tr:first-child  th:nth-child(2), 
    .project-approval-grid td:nth-child(2),
    .project-approval-grid.hide-command-column tr:first-child  th:last-child 
    .project-approval-grid.hide-command-column td:last-child   {
        border-left:none;       
    }

    .project-approval-grid.hide-command-column tr:first-child > th:last-child , 
    .project-approval-grid.hide-command-column td:last-child  {
        display:none;
    }
</style>

<asp:UpdatePanel ID="UpdatePanelApprovalProjectBudget" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>       
        <asp:HiddenField runat="server" ID="HiddenFieldProjectBudgetID" />
        <asp:HiddenField runat="server" ID="DemoSaveApprovalStep" />
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleApproval %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">                   
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%:UI.TitleTotalProjectBudget %></label>
                    </div>

                     <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectApproval_BudgetType %><span class="required" ></span></label>                
                        <div class="col-sm-8 control-value">
                            <asp:DropDownList ID="DropDownListBudgetType" runat="server" DataTextField="LovName" DataValueField="LovID" CssClass="form-control"/>
                            
                            <asp:CompareValidator ID="CompareValidatorBudgetType" runat="server" ControlToValidate="DropDownListBudgetType" 
                                ValueToCompare="0" Type="Integer" Operator="GreaterThan"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_BudgetType) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_BudgetType) %>"
                                ValidationGroup="SaveApproval" />

                            
                        </div> 
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_ProjectNo1 %></label>                
                        <div class="col-sm-3 control-value">
                            <nep:TextBox ID="TextBoxProjectNo"  runat="server" MaxLength="11" CssClass="form-control" />
                            <asp:CustomValidator ID="CustomValidatorProjectNo" ControlToValidate="TextBoxProjectNo" 
                                runat="server" CssClass="error-text" SetFocusOnError="true" ValidateEmptyText="true"
                                ClientValidationFunction="validateProjectNo"
                                OnServerValidate ="CustomValidatorProjectNo_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectNo1) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectNo1) %>"
                                ValidationGroup="SaveApproval" />
                        </div>  
                        
                                                   
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationName %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelOrganizationName" runat="server"></asp:Label>
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
                            <asp:Label ID="LabelBudgetRequest" runat="server" />
                            <%= UI.LabelBath %>
                        </div>
                    </div>
            
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanelApprovalBudgetDetail" 
                                             UpdateMode="Conditional" 
                                             runat="server">
                                <ContentTemplate> 
                                    <asp:GridView ID="GridViewActivity" runat="server" AutoGenerateColumns="False"
                                        ItemType="Nep.Project.ServiceModels.ProjectInfo.BudgetActivity"
                                        CssClass="asp-grid project-approval-grid" 
                                        OnRowDataBound="GridViewActivity_RowDataBound"
                                        >

                                        <Columns>
                                            <asp:BoundField />
                                            <asp:TemplateField HeaderText="กิจกรรมที่">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RUNNO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("RUNNO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายละเอียด">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelActivityName" runat="server" Text='<%# Eval("ACTIVITYNAME") %>'></asp:Label>
                                                    <asp:HiddenField ID="HiddenActivitID" runat="server" Value='<%# Eval("ACTIVITYID") %>' />
                                                    <uc1:ApproveBudgetGridControl ID="ApproveBudgetGridControl" runat="server" />
                                                </ItemTemplate>
                                                <%--<HeaderStyle Width="100%" />--%></asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>                    
                                    <asp:GridView runat="server" ID="GridViewApprovalBudgetDetail" AutoGenerateColumns="false" AllowPaging="false"
                                        ItemType="Nep.Project.ServiceModels.ProjectInfo.BudgetDetail"
                                        CssClass="asp-grid project-approval-grid" DataKeyNames="ProjectBudgetID" 
                                        OnRowEditing="GridViewApprovalBudgetDetail_RowEditing"
                                        OnRowCancelingEdit="GridViewApprovalBudgetDetail_RowCancelingEdit"
                                        OnRowCommand="GridViewApprovalBudgetDetail_RowCommand"                                       
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound"
                                        ShowFooter="true"  Visible="false"
                                        >                                                        
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate> 
                                                    <th rowspan="2" style="width:30px"> 
                                                        <%: Model.ProjectBudgetDetail_No %>      
                                                                                                                                
                                                    </th>
                                                    <th rowspan="2" style="width:300px"> 
                                                        <%: Model.ProjectBudgetDetail_ReviseDetail %>   
                                                     <%--    <asp:LinkButton ID="LinkButtonBudgetDetail_ReviseDetail" runat="server" 
                                                        Text="<%$ code:Model.ProjectBudgetDetail_ReviseDetail %>" CommandName="Sort" CommandArgument="ReviseDetail"/>      --%>                                                                               
                                                    </th>
                                                    <th <%=(IsCenterReviseProject.Value == true)? " colspan='4'" : " colspan='3'" %>> 
                                                       <%: Model.ProjectBudgetDetail_Amount%>    
                                                                                                                                      
                                                    </th>
                                                    <th rowspan="2" style="width:200px"><%: Model.ProjectBudgetDetail_Remark %></th>                                                                                                      
                                                    <th rowspan="2" style="width:60px"></th>
                                                    <tr>
                                                        <th></th>
                                                        <th>
                                                            <%: Model.ProjectBudgetDetail_RequesetAmount %>
                                                           <%-- <asp:LinkButton ID="LinkButtonBudgetDetail_Amount" runat="server" 
                                                            Text="<%$ code:Model.ProjectBudgetDetail_Amount %>" CommandName="Sort" CommandArgument="Amount"/> --%> 
                                                        </th>
                                                        <th><%: Model.ProjectBudgetDetail_ReviseAmount %></th>
                                                        <%
                                                        if (IsCenterReviseProject.Value == true)
                                                        {%>
                                                            <th><%: Model.ProjectBudgetDetail_Revise1CenterAmount %></th>
                                                            <th><%: Model.ProjectBudgetDetail_Revise2Amount %></th> 
                                                        <%}else{%>
                                                            <th><%: Model.ProjectBudgetDetail_Revise1ProvinceAmount %></th>
                                                        <%}
                                                        %>                                                       
                                                                                                        
                                                    </tr>
                                                </HeaderTemplate>

                                                <ItemTemplate>                                                  
                                                    
                                                    <td>
                                                         <%# Convert.ToInt32(DataBinder.Eval(Container, "RowIndex"))+1 %> 
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ReviseDetail"), null, "") %>
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("Amount"), "N2", "0.00") %>
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ReviseAmount"), "N2", "0.00")%>
                                                    </td>
                                                    <%
                                                    if (IsCenterReviseProject.Value == true)
                                                    {%>
                                                        <td>
                                                            <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("Revise1Amount"), "N2", "0.00")%>
                                                        </td>
                                                        <td>
                                                            <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("Revise2Amount"), "N2", "0.00")%>
                                                        </td> 
                                                    <%}else{%>
                                                        <td>
                                                            <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("Revise1Amount"), "N2", "0.00")%>
                                                        </td>
                                                    <%}
                                                    %>
                                                    
                                                    
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ApprovalRemark"), null, "")%>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ApprovalBudgetDetailButtonEdit" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png" 
                                                        CommandName="edit" CommandArgument='<%# Eval("ProjectBudgetID")%>' CausesValidation="false"
                                                        Visible="<%# GridViewApprovalBudgetDetail.EditIndex < 0 %>"/>    
                                                    </td>
                                                </ItemTemplate>
                                           
                                                <EditItemTemplate>                                                    
                                                   
                                                    <td>
                                                         <%# Convert.ToInt32(DataBinder.Eval(Container, "RowIndex"))+1 %> 
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ReviseDetail"), null, "") %>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField runat="server" ID="HiddenFieldRequestAmount" Value='<%# Eval("Amount") %>'/>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("Amount"), "N2", "0.00") %>
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ReviseAmount"), "N2", "0.00")%>
                                                    </td>

                                                     <%
                                                        if (IsCenterReviseProject.Value == true)
                                                        {%>
                                                            <td>
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetDiscriminationTeamAmount" CssClass="form-control" ClientIDMode="Static"
                                                                   TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise1Amount") %>'></nep:TextBox>  
                                                                <asp:CustomValidator ID="CustomValidatorRevise1Amount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorRevise1Amount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" /> 
                                                            </td>
                                                            <td>
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetSubcommitteeAmount" CssClass="form-control" ClientIDMode="Static"
                                                                   TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise2Amount") %>'></nep:TextBox> 
                                                                <asp:CustomValidator ID="CustomValidatorRevise2Amount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorRevise2Amount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false"/>   
                                                            </td>
                                                        <%}else{%>
                                                            <td>                                                                
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetProvinceCommitteeAmount" CssClass="form-control" ClientIDMode="Static"
                                                                    TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise1Amount") %>'></nep:TextBox>  
                                                                
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" />
                                                                
                                                                <asp:CustomValidator ID="CustomValidatorProvinceReviseAmount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorProvinceReviseAmount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" /> 
                                                            </td>
                                                        <%}
                                                     %>                                                    
                                                    
                                                    <td>
                                                        <nep:TextBox runat="server" ID="TextBoxApprovalRemark" CssClass="form-control" ClientIDMode="Static"
                                                            TextMode="MultiLine" MaxLength="1000" Text='<%# Eval("ApprovalRemark") %>'></nep:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ApprovalBudgetDetailButtonSave" runat="server" ImageUrl="~/Images/icon/save_icon_16.png" 
                                                            ValidationGroup="SaveBudgetApprovalDetail" CommandName="save" CommandArgument='<%# Eval("ProjectBudgetID") %>'/>
                                                        <asp:ImageButton ID="ApprovalBudgetDetailButtonCancel" runat="server" ImageUrl="~/Images/icon/cancel_icon_16.png"
                                                            CommandName="cancel" CausesValidation="false"/>
                                                    </td>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    
                                                    <td colspan="2">
                                                        <asp:Label ID="LabelTotalAmountDesc" runat="server" Text="<%$ code:Model.ProjectBudgetDetail_TotalAmount %>"/>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelRequestAmount" runat="server"/>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelReviseAmount" runat="server" />
                                                    </td>
                                                    <%
                                                        if (IsCenterReviseProject.Value == true)
                                                        {%>
                                                            <td>
                                                                <asp:Label ID="LabelRevise1CenterAmount" runat="server" Text=""/>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelRevise2CenterAmount" runat="server" Text=""/>
                                                            </td>
                                                        <%}else{%>
                                                            <td>
                                                                <asp:Label ID="LabelRevise1ProvinceAmount" runat="server" Text=""/>
                                                            </td>
                                                        <%}
                                                    %>
                                                    
                                                    
                                                    <td></td>
                                                    <td></td>
                                                </FooterTemplate>
                                                
                                             </asp:TemplateField>                                                            
                                        </Columns> 
                                    </asp:GridView>

                                    <asp:CustomValidator ID="CustomValidatorApprovalBudgetDetail" runat="server" CssClass="error-text"
                                        OnServerValidate="CustomValidatorApprovalBudgetDetail_ServerValidate"  ValidationGroup="SaveApproval"/>
                            
                                </ContentTemplate>
                            </asp:UpdatePanel><!--UpdatePanelApprovalBudgetDetail-->                                  
                        </div>
                    </div>            
                </div>   
            </div><!--panel-body-->
        </div><!--panel-->

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%=UI.TitleProjectApprovalResult %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=UI.LabelProperty %></label>
                        <div class="col-sm-2 control-value"><asp:Label ID="LabelAssessmentPropertyResult" runat="server" /></div>
                        <label class="col-sm-4 control-label"><asp:Label ID="LabelAssessmentScoreDesc" runat="server" /></label>
                        <div class="col-sm-2 control-value"><asp:Label ID="LabelAssessmentScoreResult" runat="server" /></div>
                    </div>
                </div>
            </div><!--panel-body-->
        </div><!--panel ผลการพิจารณาอนุมัติ-->

        <div class="panel panel-default" runat="server" id="PanelRevise1" visible="false">
            <div class="panel-heading">
                <h3 class="panel-title title-revise1" runat="server" id="TitleRevise1"></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <div class=" radio-button-group radio-button-group-approval required-block">
                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_1" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult"  CssClass="revise1-approval-result"/>
                                </div>

                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_2" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult" CssClass="revise1-approval-result"/>
                                    <nep:TextBox ID="TextBoxRevise1Amount" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"
                                        Min="1" Max="999999999.99" /> <%=UI.LabelBath %>
                                    <asp:CustomValidator ID="CustomValidatorRevise1Amount" runat="server" CssClass="error-text"
                                        OnServerValidate="ValidateRevise1Amount" 
                                        ClientValidationFunction="validateReviseAmount"
                                   
                                        Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Budget) %>' 
                                        ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Budget) %>'
                                        ValidationGroup="SaveApproval"/>
                                </div>

                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_3" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult" CssClass="revise1-approval-result" />                                
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_4" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult" CssClass="revise1-approval-result" />                                
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_5" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult" CssClass="revise1-approval-result" />                                
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonRevise1_6" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise1CheckedChanged"
                                        GroupName="Revise1ApprovalResult" CssClass="revise1-approval-result" />                                
                                </div>
                                <span class="required"></span>
                            </div><!--required-block-->                            
                              
                            <div>
                                <asp:CustomValidator ID="CustomValidatorRevise1Approval" runat="server" CssClass="error-text"
                                    OnServerValidate="ValidateRvise1Approved"                                                                   
                                    ValidationGroup="SaveApproval"/>
                            </div>                          
                        </div>               
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <nep:TextBox ID="TextBoxRevise1Desc" runat="server" TextMode="MultiLine" 
                                MaxLength="4000" CssClass="form-control textarea-height" ></nep:TextBox>
                            <asp:CustomValidator ID="CustomValidatorRevise1Desc" runat="server" CssClass="error-text"
                                    OnServerValidate="CustomValidatorRevise1Desc_ServerValidate" 
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Revise1Desc) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Revise1Desc) %>'
                                    ValidationGroup="SaveApproval" Enabled="false" />
                        </div>
                    </div>
                    <%--<div class="form-group form-group-sm">
                        <span class="col-sm-2 control-label"><%= Model.ProjectApproval_ApproverName %></span>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise1FirstName" runat="server" CssClass="form-control" ></asp:TextBox>                            
                        </div>                        
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise1LastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <span class="col-sm-1 control-label"><%= Model.ProjectApproval_Position %></span>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise1Position" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>

                        <span class="col-sm-1 control-label"><%= Model.ProjectApproval_Date %></span>
                        <div class="col-sm-2">
                            <nep:DatePicker runat="server" ID="DatePickerRevise1ApprovalDate" ClearTime="true" EnabledTextBox="true"  
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                        </div>
                    </div>--%>
                </div>
            </div><!--panel-body-->
        </div><!--panel ผลการพิจารณาคณะกรรมการจังหวัด หรือ มติคณะทำงานกลั่นกรอง/ คณะอนุกรรมการตามประเภทความพิการ-->

        <div class="panel panel-default" runat="server" id="PanelRevise2" visible="false">
            <div class="panel-heading">
                <h3 class="panel-title"><%=UI.TitleCommittreeApprovalResult %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12 radio-button-group radio-button-group-approval">
                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_1" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult"  CssClass="revise2-approval-result"/>
                            </div>

                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_2" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult" CssClass="revise2-approval-result"/>
                                <nep:TextBox ID="TextBoxRevise2Amount" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"
                                    Min="1" Max="999999999.99" /> <%=UI.LabelBath %>
                                <asp:CustomValidator ID="CustomValidatorRevise2Amount" runat="server" CssClass="error-text"
                                    OnServerValidate="ValidateRevise2Amount" 
                                    ClientValidationFunction="validateReviseAmount"
                                   
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Budget) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Budget) %>'
                                    ValidationGroup="SaveApproval" Enabled="false"/>
                            </div>

                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_3" runat="server"  AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult"  CssClass="revise2-approval-result" />                                
                            </div>
                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_4" runat="server"  AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult"  CssClass="revise2-approval-result" />                                
                            </div>
                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_5" runat="server"  AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult"  CssClass="revise2-approval-result" />                                
                            </div>
                            <div>
                                <asp:RadioButton ID="RadioButtonRevise2_6" runat="server"  AutoPostBack="true" OnCheckedChanged="RadioButtonRevise2CheckedChanged"
                                    GroupName="Revise2ApprovalResult"  CssClass="revise2-approval-result" />                                
                            </div>
                           <div>
                                <asp:CustomValidator ID="CustomValidatorRevise2Approval" runat="server" CssClass="error-text"
                                    OnServerValidate="ValidateRvise2Approved" 
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.TitleCommittreeApprovalResult) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.TitleCommittreeApprovalResult) %>'
                                    ValidationGroup="SaveApproval" Enabled="false" />
                            </div>
                        </div>              
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <nep:TextBox ID="TextBoxRevise2Desc" runat="server" TextMode="MultiLine" 
                                MaxLength="4000" CssClass="form-control textarea-height" ></nep:TextBox>
                            <asp:CustomValidator ID="CustomValidatorRevise2Desc" runat="server" CssClass="error-text"
                                    OnServerValidate="CustomValidatorRevise2Desc_ServerValidate" 
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Revise2Desc) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Revise2Desc) %>'
                                    ValidationGroup="SaveApproval" Enabled="false" />
                        </div>
                    </div>
                    <%--<div class="form-group form-group-sm">
                        <span class="col-sm-2 control-label"><%= Model.ProjectApproval_ApproverName %></span>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise2FirstName" runat="server" CssClass="form-control" ></asp:TextBox>                            
                        </div>                        
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise2LastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <span class="col-sm-1 control-label"><%= Model.ProjectApproval_Position %></span>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRevise2Position" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>

                        <span class="col-sm-1 control-label"><%= Model.ProjectApproval_Date %></span>
                        <div class="col-sm-2">
                            <nep:DatePicker runat="server" ID="DatePickerRevise2ApprovalDate" ClearTime="true" EnabledTextBox="true"  
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                        </div>
                    </div>--%>
                </div>
            </div><!--panel-body-->
        </div><!--panel มติคณะอนุกรรมการบริหารกองทุน-->
       
        

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%=UI.TitleNo %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">            
                    <div class="form-group form-group-sm">
                        <span class="col-sm-2 control-label"><%=Model.ProjectApproval_No %></span>
                        <div class="col-sm-2">
                            <nep:TextBox ID="TextBoxApprovalNo" runat="server" CssClass="form-control" TextMode="Number" NumberFormat="N0"
                                Min="1" Max="99"></nep:TextBox>
                            <asp:CustomValidator ID="CustomValidatorApprovalNo" runat="server" CssClass="error-text"
                                    OnServerValidate="CustomValidatorApprovalNo_ServerValidate" 
                                                                      
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_No) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_No) %>'
                                    ValidationGroup="SaveApproval" ValidateEmptyText="true"/>
                        </div>
                        <div class="col-sm-2" style="position:relative">
                            <span style="position:absolute;top:7px;left:-3px"> / </span>
                            <nep:DatePicker ID="DatePickerApprovalYear" runat="server" Format="yyyy" EnabledTextBox="true" 
                                 OnClientDateSelectionChanged="onApprovalYearChanged" 
                                 OnClientDateTextChanged="onApprovalYearChanged(null, null)"/> 
                            <asp:CustomValidator ID="CustomValidatorDatePickerApprovalYear" runat="server" CssClass="error-text"
                                    OnServerValidate="CustomValidatorDatePickerApprovalYear_ServerValidate"                                                                        
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Year) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Year) %>'
                                    ValidationGroup="SaveApproval" ValidateEmptyText="true"
                                   />
                        </div> 

                        <span class="col-sm-1 control-label"><%=Model.ProjectApproval_Date %></span>
                        <div class="col-sm-2">
                            <nep:DatePicker runat="server" ID="DatePickerApprovalDate" ClearTime="true" EnabledTextBox="true" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/>
                            <asp:CustomValidator ID="CustomValidatorDatePickerApprovalDate" runat="server" CssClass="error-text"
                                    OnServerValidate="CustomValidatorDatePickerApprovalDate_ServerValidate"                                     
                                   
                                    Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Date) %>' 
                                    ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectApproval_Date) %>'
                                    ValidationGroup="SaveApproval" ValidateEmptyText="true" /> 
                        </div>                
                    </div>
                </div>
            </div><!--panel-body-->
        </div><!--panel ครั้งที่-->

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveApprovalProjectBudget" CssClass="btn btn-primary btn-sm"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSaveApprovalProjectBudget_Click"
                        ValidationGroup="SaveApproval" Visible="false" />                   

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
        
        

    </ContentTemplate>
</asp:UpdatePanel><!--UpdatePanelApprovalProjectBudget-->
 
