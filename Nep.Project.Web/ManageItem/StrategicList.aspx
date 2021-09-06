<%@ Page Title="ข้อมูลยุทธศาสตร์" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="StrategicList.aspx.cs" Inherits="Nep.Project.Web.ManageItem.StrategicList" %>

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
                            <label class="col-sm-2 control-label">ชื่อยุทธศาสตร์</label>
                            <div  class="col-sm-10">
                                <nep:TextBox runat="server" ID="TextBoxOrganizationName" ClientIDMode="Inherit" CssClass="form-control" Text=""  MaxLength="500"></nep:TextBox>
                            </div>

                            
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    OnClick="ButtonSearch_Click"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" />
                                <asp:LinkButton ID="ButtonAdd" runat="server" CssClass="btn btn-default btn-sm" Visible="<%$ code:IsDeleteRole %>"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" PostBackUrl="~/ManageItem/StrategicForm.aspx"></asp:LinkButton>
                                <asp:Button ID="btnRefreshDashBoard" runat="server" Visible="false" 
                                    CssClass="btn btn-default btn-sm"  OnClick="btnRefreshDashBoard_Click" Text="Dash Board" />
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <%--  <Nep:GridView runat="server" ID="StrategicGrid" ItemType="Nep.Project.ServiceModels.ItemList" DataKeyNames="ITEMID"
                SelectMethod="StrategicGrid_GetData" AllowSorting="true" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="<%#Nep.Project.Common.Constants.PAGE_SIZE %>"
                OnRowDataBound="StrategicGrid_RowDataBound"
                OnRowCommand="StrategicGrid_RowCommand"
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
            </Nep:GridView>--%>
            <asp:GridView ID="GridViewStrategicList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="asp-grid" PagerStyle-CssClass="asp-pagination" DataKeyNames="ITEMNAME">
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="ลำดับ" ReadOnly="True" SortExpression="NO" />
                    <%--<asp:BoundField DataField="ITEMNAME" HeaderText="ชื่อยุทธศาสตร์" SortExpression="ITEMNAME" />--%>
                 
                      <asp:BoundField DataField="ITEMID" HeaderText="ITEMID" SortExpression="ITEMID" ReadOnly="True" visible="false"/>
                     <asp:HyperLinkField
                    DataNavigateUrlFields="ITEMID"
                    DataNavigateUrlFormatString="StrategicForm.aspx?ItemID={0}"
                    DataTextField="ITEMNAME"
                    HeaderText="ชื่อยุทธศาสตร์"
                    SortExpression="ITEMID" />
                    <asp:BoundField DataField="CASEISACTIVEWHEN'1'THEN'เปิดใช้งาน'ELSE'ปิดใช้งาน'END" HeaderText="สถานะเปิดใช้งาน" SortExpression="CASEISACTIVEWHEN'1'THEN'เปิดใช้งาน'ELSE'ปิดใช้งาน'END" />
                    <asp:BoundField DataField="CREATEDBY" HeaderText="ผู้เพิ่มข้อมูล" SortExpression="CREATEDBY" />
                    <asp:BoundField DataField="CREATEDDATE" HeaderText="วันที่เพิ่มข้อมูล" SortExpression="CREATEDDATE" />
                    <asp:BoundField DataField="UPDATEDBY" HeaderText="ผู้อัพเดตข้อมูล" SortExpression="UPDATEDBY" />
                    <asp:BoundField DataField="UPDATEDDATE" HeaderText="วันที่อัพเดตข้อมูล" SortExpression="UPDATEDDATE" ReadOnly="True" />
                </Columns>
                <PagerStyle CssClass="asp-pagination" />
            </asp:GridView>
            <%--   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ITEMID" DataSourceID="SqlDataSource1" CssClass="asp-grid" PagerStyle-CssClass="asp-pagination">
                <Columns>
                    <asp:BoundField DataField="ITEMID" HeaderText="ลำดับ" ReadOnly="True" SortExpression="ITEMID" />
                   
                 
                   <asp:HyperLinkField
                    DataNavigateUrlFields="ITEMID"
                    DataNavigateUrlFormatString="StrategicForm.aspx?ItemID={0}"
                    DataTextField="ITEMNAME"
                    HeaderText="ชื่อยุทธศาสตร์"
                    SortExpression="ITEMID" />
                    <asp:BoundField DataField="CASEISACTIVEWHEN'1'THEN'เปิดใช้งาน'ELSE'ปิดใช้งาน'END" HeaderText="สถานะเปิดใช้งาน" SortExpression="CASEISACTIVEWHEN'1'THEN'เปิดใช้งาน'ELSE'ปิดใช้งาน'END" />
                    <asp:BoundField DataField="CREATEDBY" HeaderText="ผู้เพิ่มข้อมูล" SortExpression="CREATEDBY" />
                    <asp:BoundField DataField="CREATEDDATE" HeaderText="วันที่เพิ่มข้อมูล" SortExpression="CREATEDDATE" />
                    <asp:BoundField DataField="UPDATEDBY" HeaderText="ผู้อัพเดตข้อมูล" SortExpression="UPDATEDBY" />
                    <asp:BoundField DataField="UPDATEDDATE" HeaderText="วันที่อัพเดตข้อมูล" SortExpression="UPDATEDDATE" ReadOnly="True" />
                </Columns>
                <PagerStyle CssClass="asp-pagination" />
            </asp:GridView>--%>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NepProjectDBEntities %>" ProviderName="<%$ ConnectionStrings:NepProjectDBEntities.ProviderName %>" SelectCommand="SELECT row_number() over(ORDER BY ORDERNO) AS NO,ITEMID, ITEMNAME, CASE ISACTIVE WHEN '1' THEN 'เปิดใช้งาน' ELSE 'ปิดใช้งาน' END, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE FROM NEP_APP02.MT_ITEM WHERE (ITEMGROUP = 'EVALUATIONSTRATEGIC') AND ISDELETE = '0' ORDER BY ORDERNO"></asp:SqlDataSource>

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
