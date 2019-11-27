<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabProjectBudgetForSecretaryControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabProjectBudgetForSecretaryControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<style type="text/css">
    .secretary-detail {
        margin-top:7px;
        font-weight:bold;
    }

    .budget-doc-ref {
        display: inline-block;
        vertical-align: bottom;
        margin-left: 20px;
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

<asp:UpdatePanel ID="UpdatePanelProjectBudgetForSecretary" UpdateMode="Conditional" runat="server" >
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
                                    <label class="col-sm-4 control-label"><%:UI.LabelBudgetDetailRequestAmount %></label>
                                    <div class="col-sm-8" style="position:relative">
                                        <asp:HiddenField runat="server" ID="HiddenFieldReviseAmount" />
                                        <nep:TextBox runat="server" ID="TextBoxTotalAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="999999999.99" Enabled="false" />
                                        <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTotalAmount" ControlToValidate="TextBoxTotalAmount" runat="server" CssClass="error-text"
                                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>"
                                            ValidationGroup="SaveProjectBudget" />
                                    </div>                                   
                                </div>
                                <div class="form-group form-group-sm" runat="server" id="DivBudgetDetailRequestAmount" visible="false">
                                     <label class="col-sm-4 control-label"><%:UI.LabelBudgetDetailApprovedAmount %></label>
                                     <div class="col-sm-8" style="position:relative">
                                         <nep:TextBox runat="server" ID="TextBoxTotalReviseAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="999999999.99" Enabled="false" />
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
                </div>   
            </div><!--panel-body-->
        </div><!--panel-->

        <div class="panel panel-default"><!--รายละเอียดงบประมาณ-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.BudgetDetail_Detail  %></h3>
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
                                        <asp:HyperLink ID="HyperLinkBudgetDocumentRef" runat="server" NavigateUrl="~/Download/Document/อัตราวงเงินและรายการค่าใช้จ่าย.pdf" 
                                            Target="_blank" CssClass="budget-doc-ref" Text="เอกสารประกอบการกรอกรายการค่าใช้จ่าย"  />
                                    </div>
                                    
                                    <nep:GridView runat="server" ID="GridViewBudgetDetail" AutoGenerateColumns="false" AllowPaging="false"
                                        ItemType="Nep.Project.ServiceModels.ProjectInfo.BudgetDetail"
                                        CssClass="asp-grid" DataKeyNames="UID" 
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

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Detail%>" ItemStyle-Width="200">                                        
                                                <ItemTemplate>
                                                    <%# Eval("Detail") %>                                               
                                                </ItemTemplate>  
                                                                                                                        
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Edit%>" ItemStyle-Width="200">                                        
                                                <ItemTemplate>
                                                    <%# Eval("ReviseDetail") %>                                               
                                                </ItemTemplate>                                                                                   
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxReviseDetail" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="MultiLine" Text='<%# Eval("ReviseDetail") %>'></nep:TextBox>   
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetDetail" ControlToValidate="TextBoxReviseDetail" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                   
                                                </EditItemTemplate>              
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Amount%>" ItemStyle-Width="80">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Amount"), "N2", "") %>
                                                </ItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalBudgetAmount" runat="server" Text=""/>
                                                </FooterTemplate>                                                                                        
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Edit%>" ItemStyle-Width="80">
                                                <ItemTemplate>
                                                     <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("ReviseAmount"), "N2", "") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField runat="server" ID="HiddenFieldRequestAmount" Value='<%# Eval("Amount") %>'/>
                                                    <nep:TextBox runat="server" ID="TextBoxReviseAmount" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("ReviseAmount") %>'></nep:TextBox>  
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" ControlToValidate="TextBoxReviseAmount" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>"
                                                        ValidationGroup="SaveBudgetDetail" />  
                                                    <asp:CustomValidator ID="CustomValidatorReviseAmount" ControlToValidate="TextBoxReviseAmount" runat="server" CssClass="error-text"
                                                        OnServerValidate="CustomValidatorReviseAmount_ServerValidate"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                  
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalBudgetAmountSecretary" runat="server" Text=""/>
                                                </FooterTemplate>                                        
                                            </asp:TemplateField>   
                                            
                                            <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-Width="200">                                        
                                                <ItemTemplate>
                                                    <%# Eval("ReviseRemark") %>                                               
                                                </ItemTemplate>                                                                                   
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxReviseRemark" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="MultiLine" Text='<%# Eval("ReviseRemark") %>'></nep:TextBox>
                                                      
                                                </EditItemTemplate>              
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("ProjectBudgetID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="HiddenProjectBudgetID" runat="server" Value='<%# Eval ("ProjectBudgetID") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>  
                                            
                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="60" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonEdit" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png" 
                                                         CommandName="edit" CommandArgument='<%# Eval("UID") %>'
                                                        Visible="<%# GridViewBudgetDetail.EditIndex < 0 %>" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonEdit %>"/>
                                                </ItemTemplate>    
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonSave" runat="server" ImageUrl="~/Images/icon/save_icon_16.png" 
                                                        ValidationGroup="SaveBudgetDetail" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"
                                                        CommandName="save" CommandArgument='<%# Eval("UID") %>'/>
                                                    <asp:ImageButton ID="BudgetDetailButtonCancel" runat="server" ImageUrl="~/Images/icon/cancel_icon_16.png"
                                                        CommandName="cancel" CausesValidation="false" 
                                                        ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"/>
                                                </EditItemTemplate>  
                                                <FooterTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text=""/>
                                                </FooterTemplate>                                    
                                            </asp:TemplateField>                                                           
                                        </Columns>
                                    </nep:GridView>
                                </ContentTemplate>                       
                            </asp:UpdatePanel>                            
                                                                
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
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportNo" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="ไม่"></asp:RadioButton>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportYes" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="เสนอแหล่งทุนอื่นด้วย คือ"></asp:RadioButton>
                                <asp:TextBox ID="TextBoxGotSupportName" runat="server" CssClass="form-control" />
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

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>       
    </ContentTemplate>
</asp:UpdatePanel>