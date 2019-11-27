<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectBudgetControlBackup.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.ProjectBudgetControlBackup" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<style type="text/css">
    .secretary-detail {
        margin-top:7px;
        font-weight:bold;
    }

    .got-support-name {
        margin-left:13px;
        width:95%;
    }

    .got-support-block {
        display:block;
        padding-top:7px;
    }

    .got-support-block span, .got-support-block input {
        float:left;        
        margin-right:5px;
    }

    .got-support-block span {
       display:inline-block;
       margin-top:4px;
    }

    .got-support-block span:first-child {
         margin-left:10px;
    }

</style>

<asp:UpdatePanel ID="UpdatePanelProjectBudget" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="HiddenFieldProjectID" />
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleTotalProjectBudget %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm">
                                    <label for="DemoName" class="col-sm-4 control-label"><%:UI.LabelBudgetDetailRequestAmount %></label>        
                                    <div class="col-sm-6" style="position:relative">
                                        <nep:TextBox runat="server" ID="TextBoxTotalAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="9999999.99" Enabled="false" />
                                        <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTotalAmount" ControlToValidate="TextBoxTotalAmount" runat="server" CssClass="error-text"
                                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>"
                                            ValidationGroup="SaveProjectBudget" />
                                    </div> 
                                </div>
                                <div class="form-group form-group-sm" runat="server" id="DivBudgetDetailRequestAmount" visible="false">
                                    <label class="col-sm-4  control-label"><%:UI.LabelBudgetDetailApprovedAmount %></label>
                                     <div class="col-sm-6" style="position:relative">
                                         <nep:TextBox runat="server" ID="TextBoxReviseAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="999999.99" Enabled="false" />
                                         <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                     </div> 
                                </div>
                            </div>
                        </div>                       
                        <div class="col-sm-1"></div>   
                        <div class="col-sm-5">
                            <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.LabelTotalProjectBudgetDescription) %>                                          
                        </div>
                    </div>
                </div><!-- /form-horizontal -->

                <div class="form-horizontal">                   
                    <div class="form-group form-group-sm">
                           
                       
                    </div>
                    
                </div>   
            </div><!--panel-body-->
        </div><!--panel-->

        <div class="panel panel-default"><!--รายละเอียดงบประมาณ-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.BudgetDetail_Detail %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanelBudgetDetail" 
                                             UpdateMode="Conditional" 
                                             runat="server">
                                <ContentTemplate>  
                                    <div id="Div1" class="asp-top-button" runat="server">
                                        <asp:Button ID="ButtonAddBudgetDetail" runat="server" Text="<%$ code:UI.ButtonAdd %>" 
                                            CssClass="btn btn-default" OnClick="ButtonAddBudgetDetail_Click" visble="false"/> 
                                    </div>
                                    
                                    <nep:GridView runat="server" ID="GridViewBudgetDetail" AutoGenerateColumns="false" AllowPaging="false"
                                        CssClass="asp-grid budget-detail-grid" DataKeyNames="UID" 
                                        OnRowEditing="GridViewBudgetDetail_RowEditing"
                                        OnRowCancelingEdit="GridViewBudgetDetail_RowCancelingEdit"
                                        OnRowCommand="GridViewBudgetDetail_RowCommand"
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound"
                                        ShowFooter="true">                                                        
                                        <Columns>
                                            

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_No%>" ItemStyle-Width="30" ItemStyle-CssClass="product-budget-no">
                                                <ItemTemplate>
                                                    <%# Eval("No") %>
                                                </ItemTemplate> 
                                                
                                                <FooterTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ code: Model.ProjectBudgetDetail_TotalAmount%>"/>
                                                </FooterTemplate>                                             
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Detail%>" >                                        
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("Detail"), null, "") %>
                                                </ItemTemplate>                                                                                   
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxBudgetDetail" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="MultiLine" Text='<%# Eval("Detail") %>'></nep:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetDetail" ControlToValidate="TextBoxBudgetDetail" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                    
                                                   
                                                </EditItemTemplate> 
                                                
                                                         
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Amount%>" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Amount"), "N2", "0.00") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxBudgetAmount" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="Number" Min="1.00" Max="999999999.99" Text='<%# Eval("Amount") %>'></nep:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" ControlToValidate="TextBoxBudgetAmount" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Amount) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Amount) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                   
                                                </EditItemTemplate>   
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalBudgetAmount" CssClass="total-budget-amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField> 
                                                                                         
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120" >
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise1Amount"), "N2", "0.00") %>                                                
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise1Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField>    
                                            
                                            
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise2Amount"), "N2", "0.00") %>                                                      
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise2Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField>                                          

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("ProjectBudgetID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="HiddenProjectBudgetID" runat="server" Value='<%# Eval ("ProjectBudgetID") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                     
                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="60">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonEdit" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png" 
                                                        ValidationGroup="SaveBudgetDetail" CommandName="edit" CommandArgument='<%# Eval("UID") %>'
                                                        Visible="<%# (GridViewBudgetDetail.EditIndex < 0) %>" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonEdit %>"/>
                                                    <asp:ImageButton ID="BudgetDetailButtonDelete" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                                        CommandName="del" CommandArgument='<%# Eval("UID") %>'
                                                        Visible="<%# (GridViewBudgetDetail.EditIndex < 0)  %>" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" />
                                                </ItemTemplate>    
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonSave" runat="server" ImageUrl="~/Images/icon/save_icon_16.png" 
                                                        ValidationGroup="SaveBudgetDetail" CommandName="save" CommandArgument='<%# Eval("UID") %>'
                                                        ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"/>
                                                    <asp:ImageButton ID="BudgetDetailButtonCancel" runat="server" ImageUrl="~/Images/icon/cancel_icon_16.png"
                                                        CommandName="cancel" CausesValidation="false" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"/>
                                                </EditItemTemplate>                                    
                                            </asp:TemplateField>
                                        </Columns>
                                    </nep:GridView>
                                </ContentTemplate>                                
                            </asp:UpdatePanel> 
                           
                            <asp:CustomValidator ID="CustomValidatorBudgetDetail" runat="server" CssClass="error-text"
                                OnServerValidate="ProjectBudgetDetailValidate" ClientValidationFunction="validateProjectBudgetDetail"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                ValidationGroup="SaveProjectBudget"/>

                            <asp:CustomValidator ID="CustomValidatorMaxAmount" runat="server" CssClass="error-text"
                                OnServerValidate="CustomValidatorMaxAmount_ServerValidate" 
                                Text="" 
                                ErrorMessage=""
                                ValidationGroup="SaveProjectBudget"/>
                         
                                                                
                        </div>
                    </div>
                </div>
            </div>
        </div><!--รายละเอียดงบประมาณ-->
        
        <div class="panel panel-default"><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleIsGotSuppot %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">      
                        <div class="col-sm-7 ">
                            <div>
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportNo" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="<%$ code:UI.LabelGotSupportNo %>"></asp:RadioButton>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportYes" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="<%$ code:UI.LabelGotSupportName %>"></asp:RadioButton>
                                <asp:TextBox ID="TextBoxGotSupportName" runat="server" CssClass="form-control got-support-name" />
                                <div class="got-support-block">
                                    <asp:Label ID="LabelGotSupportAmount" Text="<%$ code:UI.LabelGotSupportAmount %>" runat="server"/> 
                                    <nep:TextBox ID="TextBoxGotSupportAmount" runat="server" CssClass="form-control" Width="120" 
                                       TextMode="Number" Min="1.00" Max="999999999.99"/>
                                    <asp:Label ID="Label1" Text="<%$ code:UI.LabelBath %>" runat="server" />
                                </div>
                            </div>
                            <div style="margin-top:4px;">
                                <asp:CustomValidator ID="CustomValidatorGotSupportInfo" runat="server" CssClass="error-text" 
                                OnServerValidate="CustomValidatorGotSupportInfo_ServerValidate"
                                ValidationGroup="SaveProjectBudget" />
                            </div>                            
                        </div>        
                        <div class="col-sm-5 ">
                            <span class="field-desc"><%: UI.LabelFieldRemark %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.LabelProjectBudgetIsGotSupportRemark) %>                                          
                        </div>
                    </div>
                </div>
            </div>
        </div><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSave_Click" ValidationGroup="SaveProjectBudget" />

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click"/>

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




