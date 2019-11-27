<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSendMail.aspx.cs" Inherits="Nep.Project.Web.TestSendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Send to:"></asp:Label> <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <hr />
        <asp:Button ID="btnGenAct" runat="server" Text="Generate Budget Activity" OnClick="btnGenAct_Click"  />
    </div>
    </form>
</body>
</html>
