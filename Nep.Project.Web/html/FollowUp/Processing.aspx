<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Processing.aspx.cs" Inherits="Nep.Project.Web.html.FollowUp.Processing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>แบบติดตามประเมินผลโครงการ (สำหรับผู้เข้าร่วมโครงการ)</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font@4.x/css/materialdesignicons.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css" rel="stylesheet">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">
</head>
<body>
    <form id="form1" runat="server">

        <!-- #include file="Processing.html" -->
        <script src="https://unpkg.com/accounting-js"></script>
        <script src="https://code.jquery.com/jquery-3.4.1.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/vue/2.6.11/vue.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/vuetify/2.2.22/vuetify.min.js"></script>
        <script src="https://unpkg.com/vue-numeric"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/axios/0.19.2/axios.min.js"></script>
       <script>
           <!-- #include file="Processing.js" -->
       </script>
        
    </form>
</body>
</html>
