<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgAssistanceControl.ascx.cs" 
    Inherits="Nep.Project.Web.ProjectInfo.Controls.OrgAssistanceControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelOrgAssistance" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <div class="form-horizontal">
            <asp:Repeater ID="RepeaterOrgAssistance" runat="server" OnDataBinding="RepeaterOrgAssistance_DataBinding">
                <ItemTemplate>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-7" style="padding-left:30px">
                            <span style="margin-left:-25px; padding-top:6px;" class="label-no"><%# Eval("No") %></span>
                            <nep:TextBox ID="TextBoxOrganizationName" CssClass="form-control" runat="server" MaxLength="500" Text='<%# Eval("OrganizationName") %>'></nep:TextBox>
                        </div>
                        <label class="col-sm-1 control-label without-delimit"><%= UI.LabelAmount %></label>
                        <div class="col-sm-3" style="position:relative">
                            <nep:TextBox ID="TextBoxAmount" runat="server" Text='<%# Eval("Amount") %>'  CssClass="form-control form-control-desc" TextMode="Number"
                                Min="1" Max="999999999.99" ></nep:TextBox> 
                            <span class="form-control-desc  desc-bath"><%= UI.LabelBath %></span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
       </div>
    </ContentTemplate>
</asp:UpdatePanel>