<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BudgetGridControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.BudgetGridControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>
                                    <%--    OnRowEditing="GridViewBudgetDetail_RowEditing"
                                        OnRowCancelingEdit="GridViewBudgetDetail_RowCancelingEdit"
                                        OnRowCommand="GridViewBudgetDetail_RowCommand"
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound"--%>
                                   <nep:GridView runat="server" ID="GridViewBudgetDetail" AutoGenerateColumns="False"
                                        CssClass="asp-grid budget-detail-grid" DataKeyNames="UID" 
                                        OnRowDataBound  ="GridViewBudgetDetail_RowDataBound"
                                        ShowFooter="True" TotalRows="0">                                                        
                                        <Columns>
                                            

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_No%>" ItemStyle-Width="30" ItemStyle-CssClass="product-budget-no">
                                                <ItemTemplate>
                                                    <%# Eval("No") %>
                                                </ItemTemplate> 
                                                
                                                <FooterTemplate>
                                                    <%--<asp:Label ID="Label3" runat="server" Text="<%$ code: Model.ProjectBudgetDetail_TotalAmount%>"/>--%>
                                                     <asp:Label ID="Label1" runat="server" Text="รวม"/>
                                                </FooterTemplate>                                             

<ItemStyle CssClass="product-budget-no" Width="30px"></ItemStyle>
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

<ItemStyle Width="120px"></ItemStyle>
                                            </asp:TemplateField> 
                                                                                         
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120" >
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise1Amount"), "N2", "0.00") %>                                                
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise1Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            

<ItemStyle Width="120px"></ItemStyle>
                                            </asp:TemplateField>    
                                            
                                            
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise2Amount"), "N2", "0.00") %>                                                      
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise2Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            

<ItemStyle Width="120px"></ItemStyle>
                                            </asp:TemplateField>                                          

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("ProjectBudgetID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="HiddenProjectBudgetID" runat="server" Value='<%# Eval ("ProjectBudgetID") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                     
                                        </Columns>

<PagerStyle CssClass="asp-pagination"></PagerStyle>

<SortedAscendingHeaderStyle CssClass="sort-asc"></SortedAscendingHeaderStyle>

<SortedDescendingHeaderStyle CssClass="sort-desc"></SortedDescendingHeaderStyle>
                                    </nep:GridView>