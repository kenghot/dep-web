<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveBudgetGridControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.ApproveBudgetGridControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:GridView runat="server" ID="GridViewApprovalBudgetDetail" AutoGenerateColumns="false" AllowPaging="false"
                                        ItemType="Nep.Project.ServiceModels.ProjectInfo.BudgetDetail"
                                        CssClass="asp-grid project-approval-grid" DataKeyNames="ProjectBudgetID" 
                                        OnRowEditing="GridViewApprovalBudgetDetail_RowEditing"
                                        OnRowCancelingEdit="GridViewApprovalBudgetDetail_RowCancelingEdit"
                                        OnRowCommand="GridViewApprovalBudgetDetail_RowCommand"                                       
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound"
                                        OnDataBound ="GridViewBudgetDetail_DataBound"
                                        ShowFooter="true" 
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
                                                    <th <%=( IsCenterReviseProject.Value == true)? " colspan='4'" : " colspan='3'" %>> 
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