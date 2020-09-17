<%@ Page Title="รายการผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" Inherits="Nep.Project.Web.User.UserAccessList" Codebehind="UserAccessList.aspx.cs"  UICulture="th-TH" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <p>
    </p>
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
                            <label class="col-sm-3 control-label">ระหว่างวันที่</label>
                            <div class="col-sm-2">
                                <nep:DatePicker runat="server" ID="DatePickerStart" ClearTime="true" EnabledTextBox="true" ValidationGroup="Search"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/>                               
                            </div>
                            <div class="col-sm-1">
                                <span style="position:absolute;padding-left:30px;padding-top:7px;">ถึง</span>
                            </div>
                            <div class="col-sm-2">
                                <nep:DatePicker runat="server" ID="DatePickerEnd" ClearTime="true" EnabledTextBox="true" ValidationGroup="Search"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/>   
                            </div> 
                        </div> 
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
                  <asp:TemplateField HeaderText="วัน เวลา" ItemStyle-Width="150" SortExpression="AccessDateTime" >
                        <ItemTemplate>
                           <%# Nep.Project.Common.Web.WebUtility.ToBuddhaDateFormat(Convert.ToDateTime(Eval("AccessDateTime")), Nep.Project.Common.Constants.UI_FORMAT_DATE_TIME, "-")  %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="การใช้งาน" SortExpression="AccessDesc">
                        <ItemTemplate>  
                            <%# Eval("AccessDesc") %> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IP Address" SortExpression="IPAddress">
                        <ItemTemplate>  
                            <%# Eval("IPAddress") %> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.UI.LabelUsernameAndDesc %>" SortExpression="Email">
                        <ItemTemplate>  
                            <%# Eval("Email") %> 
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
