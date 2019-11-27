<%@ Page Title="รายการองค์กร" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="OrganizationList.aspx.cs" Inherits="Nep.Project.Web.Organization.OrganizationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1"
        UpdateMode="Conditional"
        runat="server">
        <ContentTemplate>
            <div class="panel panel-default panel-search">
                <div class="panel-heading panel-heading-search"><%=Nep.Project.Resources.UI.LabelSearch %></div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.Organization_OrganizationName %></label>
                            <div  class="col-sm-4">
                                <nep:TextBox runat="server" ID="TextBoxOrganizationName" ClientIDMode="Inherit" CssClass="form-control" Text=""  MaxLength="100"></nep:TextBox>
                            </div>

                            <label class="col-sm-2 control-label" id="LabelProvince" runat="server"><%: Nep.Project.Resources.Model.Organization_Province%></label>
                            <div class="col-sm-4" id="DivComboBoxProvince" runat="server">
                                <input id="DdlProvince" runat="server" style="width:100%; " />                               
                            </div>
                            
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    OnClick="ButtonSearch_Click"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" />
                                <asp:LinkButton ID="ButtonAdd" runat="server" CssClass="btn btn-default btn-sm" Visible="<%$ code:IsDeleteRole %>"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" PostBackUrl="~/Organization/OrganizationForm"></asp:LinkButton>
                                <asp:Button ID="btnRefreshDashBoard" runat="server" Visible="false" 
                                    CssClass="btn btn-default btn-sm"  OnClick="btnRefreshDashBoard_Click" Text="Dash Board" />
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <nep:GridView runat="server" ID="OrganizationGrid" ItemType="Nep.Project.ServiceModels.OrganizationList" DataKeyNames="OrganizationID"
                SelectMethod="OrganizationGrid_GetData" AllowSorting="true" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="<%#Nep.Project.Common.Constants.PAGE_SIZE %>"
                CssClass="asp-grid" PagerStyle-CssClass="asp-pagination"
                OnRowCommand="GridOrganization_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_OrganizationName %>" ItemStyle-Width="300" SortExpression="OrganizationName">
                        <ItemTemplate>
                            <asp:HyperLink ID='lnkUserName' runat='server' Text='<%# Eval("OrganizationName") %>' NavigateUrl='<%# "~/Organization/OrganizationForm?OrganizationID=" + Eval("OrganizationID") %>' ></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_Address %>">
                        <ItemTemplate>
                           <asp:Literal ID="LiteralAddress" runat="server" Text='<%# FormatAddress(Item) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:DynamicField HeaderText="<%$ code: Nep.Project.Resources.Model.ProjectInfo_OrgUnderSupport %>" DataField="OrganizationUnder" 
                        NullDisplayText="-" ConvertEmptyStringToNull="true" ItemStyle-Width="200" />
                
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="35" Visible="<%$ code:IsDeleteRole %>">
                        <ItemTemplate>                            
                            <asp:ImageButton ID="BudgetDetailButtonDelete" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                      CommandName="del" CommandArgument='<%# Eval("OrganizationID") %>' Visible='<%# (Convert.ToBoolean(Eval("IsDeletable")))%>' OnClientClick="return ConfirmToDelete()" />
                        </ItemTemplate>                        
                    </asp:TemplateField>                    
                </Columns>
            </nep:GridView>

            <script type="text/javascript">
                function ConfirmToDelete() {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    var isConfirm = window.confirm(message);
                    return isConfirm;
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
