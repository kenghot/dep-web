<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabSurveyParticipant.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabSurveyParticipant"
    %>
<%@ Import Namespace="Nep.Project.Resources"  %>

<asp:UpdatePanel ID="UpdatePanelContract" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>      
        <!-- #include file="~/Html/ParticipantSurvey/Form.html" -->
 
    </ContentTemplate>
</asp:UpdatePanel>
