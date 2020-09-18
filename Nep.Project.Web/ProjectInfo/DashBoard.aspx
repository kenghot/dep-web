<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/dashboard.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- #include file="~/Html/DashBoard/DashBoard.html" -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
     <asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ContentPlaceHolderFooterScript">
         <script>
    <!-- #include file="~/Html/DashBoard/DashBoard.js" -->
         </script>
     </asp:Content>
