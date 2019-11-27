<%@ Page Title="รายการผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="UserProfileList.aspx.cs" Inherits="Nep.Project.Web.User.UserProfileList" %>

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
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.UI.LabelUsernameAndDesc %></label>
                            <div class="col-sm-4">
                                <nep:TextBox runat="server" ID="txtEmail" ClientIDMode="Inherit" CssClass="form-control" Text=""  MaxLength="50"></nep:TextBox>
                            </div>

                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_FirstName %></label>
                            <div  class="col-sm-4">
                                <nep:TextBox runat="server" ID="txtProfileName" ClientIDMode="Inherit" CssClass="form-control" Text=""  MaxLength="200"></nep:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">                            
                            
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_Role %></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlRole" 
                                    AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control"
                                    DataTextField="Text" DataValueField="Value">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_OrganizationName %></label>
                            <div  class="col-sm-4">
                                <nep:TextBox runat="server" ID="txtOrgName" ClientIDMode="Inherit" CssClass="form-control" Text=""  MaxLength="200"></nep:TextBox>
                            </div>                            
                        </div>
                        <div class="form-group form-group-sm" runat="server" >  
                            <label class="col-sm-2 control-label"><%: Nep.Project.Resources.Model.UserProfile_Province %></label>
                            <div class="col-sm-4">
                                <input id="DdlProvince" runat="server" style="width:100%; " />
                            </div>  
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    OnClick="ButtonSearch_Click"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" />
                                <asp:LinkButton ID="LinkButtonAdd" Visible="false" runat="server" CssClass="btn btn-default btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" PostBackUrl="~/User/UserProfileForm.aspx"></asp:LinkButton>
                                <asp:Button runat="server" ID="ButtonClear" CssClass="btn btn-green btn-sm"
                                    OnClick="ButtonClear_Click"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonClear %>" />
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>



            <div class="form-inline form-search">
                

                
            </div>


            <Nep:GridView runat="server" ID="UserProfileGrid" ItemType="Nep.Project.ServiceModels.UserList" DataKeyNames="UserID"
                SelectMethod="UserProfileGrid_GetData" AllowSorting="true" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="<%#Nep.Project.Common.Constants.PAGE_SIZE %>"
                OnRowDataBound="UserProfileGrid_RowDataBound"
                OnRowCommand="UserProfileGrid_RowCommand"
                CssClass="asp-grid" PagerStyle-CssClass="asp-pagination">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.UI.LabelUsernameAndDesc %>" SortExpression="Email">
                        <ItemTemplate>  
                            <asp:HyperLink ID='lnkUserName' runat='server' Text='<%# Eval("Email") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.UserProfile_FirstName %>" SortExpression="FirstName">
                        <ItemTemplate>
                            <%#Eval("FirstName") %> <%#Eval("LastName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.UserProfile_TelephoneNo %>" SortExpression="TelephoneNo">
                        <ItemTemplate>
                            <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("TelephoneNo"), "", "-") %>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:DynamicField DataField="Role" />
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.UserProfile_OrganizationName %>" SortExpression="OrganizationName">
                        <ItemTemplate>
                            <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("OrganizationName"), "", "-") %>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.UserProfile_Province %>" SortExpression="Province">
                        <ItemTemplate>
                            <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("Province"), "", Nep.Project.Resources.UI.LabelNotProvinceName) %>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="35" Visible="<%$ code:IsDeleteRole %>">
                        <ItemTemplate>                            
                            <asp:ImageButton ID="UserDetailButtonDelete" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                CommandName="del" CommandArgument='<%# Eval("UserID") %>' Visible='<%# (Eval("UserName").ToString().ToLower() != Nep.Project.Common.Constants.SYSTEM_USERNAME)%>' OnClientClick="return ConfirmToDelete()" 
                                />
                        </ItemTemplate>                        
                    </asp:TemplateField>   
                </Columns>
            </Nep:GridView>
           

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
