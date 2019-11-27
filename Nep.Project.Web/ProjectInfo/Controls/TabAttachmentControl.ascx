<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabAttachmentControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabAttachmentControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>
<%@ Register TagPrefix="nep" TagName="AttachmentProvideControl" Src="~/ProjectInfo/Controls/AttachmentProvideControl.ascx" %>

<asp:UpdatePanel ID="UpdatePanelAttachment" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        
        <%--<script type="text/javascript" src="../../Scripts/kendo.all.min.js" ></script>   --%>     

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TabTitleAttachment %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <asp:Image ID="ImageHelp1" runat="server" ToolTip="กรุณาแนบไฟล์ตามรายละเอียดด้านล่างอย่างน้อย 1 รายการ" ImageUrl="~/Images/icon/about.png" BorderStyle="None" /> 
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectInfo_AttachmentInfo %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <nep:AttachmentProvideControl ID="AttachmentProvideControl" runat="server" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">
                            <div class="form-group form-group-sm">
                                <label class="col-sm-12 form-group-title"><%= Model.ProjectInfo_LocationPropose %></label>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= UI.ProjectInfoLocationProposeDesc1 %></span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= UI.ProjectInfoLocationProposeDesc2 %></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= UI.LabelSignature %></span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-1">
                                    <span class="form-control-left-desc">( </span>
                                </div>
                                <div class="col-sm-10">
                                    <div class="required-block">
                                        <asp:TextBox ID="TextBoxResponsibleProject" runat="server" CssClass="form-control" ReadOnly="true" Text=""></asp:TextBox>
                                       <%-- <span class="required"></span>--%>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <span class="form-control-left-desc"> )</span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= Model.ProjectInfo_ResponsibleProject %></span>
                                </div>
                            </div>

                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= UI.LabelSignature %></span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-1">
                                    <span class="form-control-left-desc">( </span>
                                </div>
                                <div class="col-sm-10">
                                    <div class="required-block">
                                        <asp:TextBox ID="TextBoxProposerProject" runat="server" CssClass="form-control" ReadOnly="true" Text=""></asp:TextBox>
                                        <%--<span class="required"></span>--%>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <span class="form-control-left-desc"> )</span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= Model.ProjectInfo_ProposerProject %></span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-12">
                                    <span><%= Model.ProjectInfo_LeaderOrganize %></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
             <div><asp:Literal runat="server" ID="ltrEdoc"></asp:Literal><br /></div>
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                           <asp:Button runat="server" ID="ButtonDraft" CssClass="btn btn-primary btn-sm" Visible="false"  
                        Text="บันทึกร่าง" OnClick="ButtonSave_Click" />
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" Visible="false"
                        ValidationGroup="SaveProjectAttachment"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSave_Click" />

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click"/>

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>
                    
                    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>                   

                    

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
    

    </ContentTemplate>
</asp:UpdatePanel>

