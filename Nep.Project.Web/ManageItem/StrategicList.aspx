<%@ Page Title="ยุทธศาสตร์" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="StrategicList.aspx.cs" Inherits="Nep.Project.Web.ManageItem.StrategicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1"
        UpdateMode="Conditional"
        runat="server">
        <ContentTemplate>
<%--            <div class="panel panel-default panel-search">
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
            </div>--%>
            <asp:GridView ID="GridViewStrategicList" runat="server" AutoGenerateColumns="False" DataKeyNames="ITEMID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ITEMID" HeaderText="ITEMID" ReadOnly="True" SortExpression="ITEMID" />
                    <asp:BoundField DataField="ITEMNAME" HeaderText="ITEMNAME" SortExpression="ITEMNAME" />
                    <asp:BoundField DataField="ISACTIVE" HeaderText="ISACTIVE" SortExpression="ISACTIVE" />
                    <asp:BoundField DataField="CREATEDBY" HeaderText="CREATEDBY" SortExpression="CREATEDBY" />
                    <asp:BoundField DataField="CREATEDDATE" HeaderText="CREATEDDATE" SortExpression="CREATEDDATE" />
                    <asp:BoundField DataField="UPDATEDBY" HeaderText="UPDATEDBY" SortExpression="UPDATEDBY" />
                    <asp:BoundField DataField="UPDATEDDATE" HeaderText="UPDATEDDATE" SortExpression="UPDATEDDATE" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NepProjectDBEntities %>" ProviderName="<%$ ConnectionStrings:NepProjectDBEntities.ProviderName %>" SelectCommand="SELECT ITEMID, ITEMNAME, ISACTIVE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE FROM NEP_APP02.MT_ITEM WHERE (ITEMGROUP = 'EVALUATIONSTRATEGIC') ORDER BY ORDERNO"></asp:SqlDataSource>

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
