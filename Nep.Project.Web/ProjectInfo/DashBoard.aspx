<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/dashboard.css" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.min.js"></script>
<script src="https://unpkg.com/vue-chartjs/dist/vue-chartjs.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href='<%= System.Configuration.ConfigurationManager.AppSettings["MIS_DASHBOARD_URL"] %>' class="btn btn-default btn-sm" target="_blank">DashBoard ภาพรวม</a>
    <!-- #include file="~/Html/DashBoard/DashBoard.html" -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
     <asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ContentPlaceHolderFooterScript">
         <script>
    <!-- #include file="~/Html/DashBoard/DashBoard.js" -->
         </script>
     </asp:Content>
