<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="BudgetCopyDetailPopUp.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.BudgetCopyDetailPopUp" %>
<%@ Register src="Controls/ProjectBudgetCopyDetail.ascx" tagname="ProjectBudgeCopytDetail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label runat="server" ID="LabelProjectName" Font-Bold="True" Font-Size="Larger"></asp:Label><br />
    <asp:Label runat="server" ID="LabelActivity" Font-Bold="True" Font-Size="Larger"></asp:Label>
    <asp:Button runat="server" ID="ButtonClose" Visible="false" Text="ปิด" />
 <uc1:ProjectBudgeCopytDetail ID="ProjectBudgetDetail1" runat="server" Visible="False" />
   <script type="text/javascript" src="../Scripts/projectbudget.js?v=<%= DateTime.Now.Ticks.ToString() %>">
   </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
