<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelerikRepContractProvince.aspx.cs" Inherits="Nep.Project.Web.TelerikReport.TelerikRepContractProvince" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:100%">
    
    <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"  ViewMode="PrintPreview"></telerik:ReportViewer>

    </div>
    </form>
</body>
</html>
