<%@ Page Language="C#" Title="<%$ code:Nep.Project.Resources.UI.PageTitleProjectForm %>" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master"
    CodeBehind="ProjectInfoForm.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.ProjectInfoForm" 
    UICulture="th-TH" Culture="th-TH"  %>

<%@ Import Namespace="Nep.Project.Resources" %>


<%@ Register TagPrefix="nep" TagName="GeneralInfoControl" Src="~/ProjectInfo/Controls/TabGeneralInfoControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProcessingControl" Src="~/ProjectInfo/Controls/ProcessingControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProjectInfoControl" Src="~/ProjectInfo/Controls/TabProjectInfoControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProjectBudgetControl" Src="~/ProjectInfo/Controls/ProjectBudgetControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="AttachmentControl" Src="~/ProjectInfo/Controls/TabAttachmentControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ResponseControl" Src="~/ProjectInfo/Controls/TabResponseControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="AssessmentControl" Src="~/ProjectInfo/Controls/TabAssessmentControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="PersonalControl" Src="~/ProjectInfo/Controls/TabPersonalControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProcessingPlanControl" Src="~/ProjectInfo/Controls/TabProcessingPlanControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ApprovalControl" Src="~/ProjectInfo/Controls/TabApprovalControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProcessedControl" Src="~/ProjectInfo/Controls/TabProcessedControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ContractControl" Src="~/ProjectInfo/Controls/TabContractControl.ascx"%>
<%@ Register TagPrefix="nep" TagName="ReportResultControl" Src="~/ProjectInfo/Controls/TabReportResultControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="SatisfyControl" Src="~/ProjectInfo/Controls/TabSatisfyControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="Follow5MControl" Src="~/ProjectInfo/Controls/TabFollow5MControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="FollowUnder5MControl" Src="~/ProjectInfo/Controls/TabFollowUnder5MControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="FollowProcessing" Src="~/ProjectInfo/Controls/TabFollowProcessing.ascx" %>
<%@ Register TagPrefix="nep" TagName="SelfEvaluateControl" Src="~/ProjectInfo/Controls/TabSelfEvaluateControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="SurveyParticipant" Src="~/ProjectInfo/Controls/TabSurveyParticipant.ascx" %>
<%@ Register TagPrefix="nep" TagName="FollowupControl" Src="~/ProjectInfo/Controls/TabFollowupControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProjectBudgetForSecretaryControl" Src="~/ProjectInfo/Controls/TabProjectBudgetForSecretaryControl.ascx" %>
<%@ Register TagPrefix="nep" TagName="ProsecuteControl" Src="~/ProjectInfo/Controls/TabProsecuteControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css"> 
        .organization-type td:nth-child(4){
            display:block;
            clear:left;           
        }

        .entry-title {
            display:inline;
        }
        .project-name {
            padding-left:20px;
            font-size: 22px;
            font-weight: normal;
            color: #808080;
            line-height:1.2;
        }

        .project-status {
            text-align: right;
            font-size: 14px;
            font-weight: bold;
            padding-bottom: 5px;
            color: #16A1E7;
        }

        .project-status span {
            font-weight: normal;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript" src="../Scripts/kendo.all.min.js" ></script>

   <script>
   function ShowSuggestion() {
       $( "#divSuggestion" ).dialog({width:500,height:400});
  }  
  </script>
    <div id="DataContainer" runat="server" visible="false"> 
        <asp:UpdatePanel ID="UpdatePanelProjectInfoForm" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">            
            <ContentTemplate>
                <asp:HiddenField ID="HiddenFieldProjectID" Value="" runat="server" />
                  <%--<div class="form-group form-group-sm" style="font-size:12px">
                        <div class="col-sm-4 control-block">
                        <label class="control-label">ชื่อผู้รับผิดชอบ<span class="required"></span></label>                          
                            <asp:TextBox ID="TextBoxResponseName" runat="server" MaxLength="100" CssClass="form-control" Width="220" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName1" ControlToValidate="TextBoxResponseName" runat="server" CssClass="error-text"
                                Text='<%= "กรุณาระบุชื่อผู้รับผิดชอบ" %>'
                                ErrorMessage='<%= "กรุณาระบุชื่อผู้รับผิดชอบ" %>'
                                ValidationGroup="SaveResponse" SetFocusOnError="true"/>
                        </div>

                        <div class="col-sm-3">
                        <label class="control-label"><%= Model.ProjectInfo_Telephone %><span class="required" ></span></label>                        
                        
                            <asp:TextBox ID="TextBoxResponseTel" runat="server" MaxLength="50" Width="180" Enabled="false" CssClass="form-control"></asp:TextBox>     
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone1" ControlToValidate="TextBoxResponseTel" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ValidationGroup="SaveResponse" SetFocusOnError="true"/>                       
                        </div>
                        <div class="col-sm-3">
                        <label class="control-label"><%= Model.ProjectInfo_Email %><span class="required" id="LabelEmail1Sign" runat="server"></span></label>                        
                        
                            <asp:TextBox ID="TextBoxResponseEmail" runat="server" MaxLength="50"   CssClass="form-control"></asp:TextBox>         
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail1" ControlToValidate="TextBoxResponseEmail" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmail" ControlToValidate="TextBoxResponseEmail" runat="server"
                                CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ValidationGroup="SaveResponse" />                 
                        </div>
                      <div class="col-sm-2">
                       <asp:Button runat="server" ID="ButtonEditResponse" CssClass="btn btn-primary btn-sm" OnClientClick="ButtonResponseEditClick(); return false;"  
                        Text="แก้ไข"  Visible="true" />
                       <asp:Button runat="server" ID="ButtonSaveResponse" CssClass="btn btn-primary btn-sm" OnClientClick="ButtonResponseSaveClick(); return false"  ValidationGroup="SaveResponse" 
                        Text="บันทึก"  Visible="true" />
                      
                       <asp:Button runat="server" ID="ButtonBackResponse" CssClass="btn btn-primary btn-sm"   
                        Text="กลับ"  Visible="true"  OnClientClick="ButtonResponseBackClick(); return false"  />
                      </div>
                     
                   <script type="text/javascript">
                       ResponseSwitchMode(false);
                       var ResponseMode = 0;
                          function ButtonResponseEditClick()
                          {
                               ResponseSwitchMode(true);
                          }
                          function ButtonSaveBackClick()
                          {
                              ResponseSwitchMode(false);
                          }
                           function ButtonResponseBackClick()
                          {
                               ResponseSwitchMode(false);
                          }
                          function ResponseSwitchMode(isEditMode)
                           {
                                var saveButton = <%= ButtonSaveResponse.ClientID  %>;
                               var editButton = <%= ButtonEditResponse.ClientID  %>;
                               var backButton = <%= ButtonBackResponse.ClientID  %>;
                                if (!isEditMode) {
                                    
                                    saveButton.style.display = "none";
                                    backButton.style.display = "none";
                                    editButton.style.display = "block";
                                   } else
                                {                                      
                                    saveButton.style.display = "block";
                                    backButton.style.display = "block";
                                    editButton.style.display = "none";
                                    }
                                <%= TextBoxResponseName.ClientID  %>.disabled = !isEditMode;   
                                <%= TextBoxResponseEmail.ClientID  %>.disabled = !isEditMode;  
                                <%= TextBoxResponseTel.ClientID  %>.disabled = !isEditMode;  
                          }
                      </script>
                  
                    
               </div>--%>
               <div id="divSuggestion" title="ข้อเสนอแนะสำหรับการปรับปรุง" style="display:none">
                    <p><asp:Label runat="server" ID="LableSuggestion" Text="asdfasfasfasfaf"></asp:Label></p>
                </div>
                <div class="form-group form-group-sm">
                    <div class="col-sm-6">
                        <asp:Button runat="server" ID="btnShowSuggestion" Visible="false" OnClientClick="ShowSuggestion();return false;" Text="แสดงข้อแนะนำสำหรับการปรับปรุง" />
                    </div>
                    <div class="project-status">
                        สถานะขั้นตอนการอนุมัติ : <asp:Label ID="LabelApprovalStatus" runat="server" />
                    </div>
                </div>
                
                <ajaxToolkit:TabContainer ID="TabContainerProjectInfoForm" runat="server" ActiveTabIndex="0" 
                    OnActiveTabChanged="TabContainerProjectInfoForm_ActiveTabChanged" 
                    OnClientActiveTabChanged="clientActiveTabChanged" AutoPostBack="true" >
        
                    <ajaxToolkit:TabPanel  ID="TabPanelGeneralInfo" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleOrganizeInfo %>">
                        <ContentTemplate>
                            <!--ข้อมูลองค์กร-->
                            <nep:GeneralInfoControl ID="DisplayGeneralInfoControl" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="TabPanelProjectInfo" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleProjectInfo %>">
                        <ContentTemplate>
                            <!--ข้อมูลโครงการ-->   
                            <nep:ProjectInfoControl ID="DisplayProjectInfoControl" runat="server" Visible="false"/>                                  
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="TabPersonal" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitlePersonal %>">            
                        <ContentTemplate>
                            <!--บุคคลากร-->  
                            <nep:PersonalControl ID="PersonalControl" runat="server"  Visible="false" />                               
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="TabProcessingPlan" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleProcessingPlan %>" >
                        <ContentTemplate>  
                            <!--การดำเนินงาน--> 
                            <nep:ProcessingPlanControl ID="ProcessingPlanControl"   runat="server"  Visible="false"/>                                           
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                   <ajaxToolkit:TabPanel ID="TabPanelProjectBudget" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleProjectBudget %>">
                        <ContentTemplate>
                            <!--งบประมาณ--> 
                            <nep:ProjectBudgetControl ID="ProjectBudgetControl" runat="server"  Visible="false"/>
                            <nep:ProjectBudgetForSecretaryControl ID="ProjectBudgetForSecretaryControl" runat="server"   Visible="false" />

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="TabPanelAttachment" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleAttachment %>">
                        <ContentTemplate>  
                            <!--เอกสารแนบ-->
                            <nep:AttachmentControl ID="DisplayAttachmentControl"  runat="server"  Visible="false"/>   
                        </ContentTemplate>            
                    </ajaxToolkit:TabPanel>
                    
                    <ajaxToolkit:TabPanel ID="TabPanelResponse" runat="server" HeaderText="เจ้าหน้าที่รับผิดชอบโครงการ">            
                        <ContentTemplate>
                            <!--เจ้าหน้าที่รับผิดชอบโครงการ-->  
                            <nep:ResponseControl ID="ResponseControl" runat="server"  Visible="false" />                               
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
        
                    <ajaxToolkit:TabPanel ID='TabPanelAssessment' runat='server' HeaderText='<%$ code:Nep.Project.Resources.UI.TabTitleAssessmentProject %>'  Visible="false">
                        <ContentTemplate>    
                            <!--ประเมินโครงการ-->  
                            <nep:AssessmentControl ID="DisplayAssessmentControl" runat="server"  Visible="false"/>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
        
        
                    <ajaxToolkit:TabPanel ID="TabPanelProjectApproval" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleProjectApproval %> "  Visible="false">
                        <ContentTemplate> 
                            <!--ผลการอนุมัติ-->
                            <nep:ApprovalControl ID="ApprovalControl" runat="server"  Visible="false" />                              
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelContract" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TabTitleContract %>"  Visible="false">
                        <ContentTemplate> 
                            <!--ข้อมูลสัญญา-->
                            <nep:ContractControl ID="ContractControl" runat="server"   Visible="false"/>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel> 
                    <ajaxToolkit:TabPanel ID="TabPanelFollowUpProcess" runat="server" HeaderText="ประเมินผลโครงการ (ระหว่างดำเนินโครงการ)"  Visible="false">
                        <ContentTemplate> 
                            <!--ประเมินผลโครงการ (ระหว่างดำเนินโครงการ)-->
                            <nep:FollowProcessing ID="FollowProcessingControl" runat="server"   Visible="false"/>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel> 
                    <ajaxToolkit:TabPanel ID="TabPanelProcessed" runat="server" HeaderText="การดำเนินการ"  Visible="false">
                        <ContentTemplate> 
                            <!--การดำเนินการ-->
                            <nep:ProcessedControl ID="ProcessedControl" runat="server"  Visible="false" />                              
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>                             

                    <ajaxToolkit:TabPanel ID="TabPanelSatisfy" runat="server" HeaderText="แบบประเมินความพึงพอใจ"  Visible="false">
                        <ContentTemplate>   
                            <!--รายงานความพึงพอใจ--> 
                            <nep:SatisfyControl ID="SatisfyControl" runat="server"  Visible="false" />            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelSelfEvaluate" runat="server" HeaderText="แบบประเมินตนเอง"  Visible="false">
                        <ContentTemplate>   
                            <!--ประเมินตนเอง--> 
                            <nep:SelfEvaluateControl ID="SelfEvaluateControl" runat="server"  Visible="false" />            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabSurveyParticipant" runat="server" HeaderText="แบบประเมิน(ผู้เข้าร่วม)"  Visible="false">
                        <ContentTemplate>   
                            <!--survey--> 
                            <nep:SurveyParticipant ID="SurveyParticipant" runat="server"  Visible="false" />            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelFollowup" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TitleFollowup %>"   Visible="true">
                        <ContentTemplate>  
                            <!--ติดตามประเมินผลโครงการ--> 
                           <nep:FollowupControl ID="FollowupControl" runat="server"  Visible="false" />  
                            <!-- kenghot18 -->
                           <nep:Follow5MControl ID="Follow5MControl" runat="server"  Visible="false" />  
                           <nep:FollowUnder5MControl ID="FollowUnder5MControl" runat="server"  Visible="false" />  
                                      
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelReportResult" runat="server" HeaderText="<%$ code:Nep.Project.Resources.UI.TitleProjectReport %>"  Visible="false">
                        <ContentTemplate>   
                            <!--รายงานผลการปฏิบัติงาน--> 
                            <nep:ReportResultControl ID="ReportResultControl" runat="server"  Visible="false" />            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabProsecute" runat="server" HeaderText="การดำเนินคดีตามกฎหมาย">            
                        <ContentTemplate>
                            <!--การดำเนินคดีตามกฎหมาย-->  
                            <nep:ProsecuteControl ID="ProsecuteControl" runat="server"  Visible="false" />                               
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <asp:Panel runat="server" ID="PanelHistory">
                 
                    <table style="width:100%;">
                        <tr>
                        <td style="border-style:solid;border-color:gray;border-width:1px;text-align:center;padding:2px">
                                <asp:Label ID="LabelProjectHistory" runat="server"></asp:Label>
                         </td>

                         </tr>

                    </table>
                </asp:Panel>
            </ContentTemplate>           
        </asp:UpdatePanel>     
       
        

        <script type="text/javascript">
            $(function () {
                //ReHightHeaderTab();                
            });

            function ReHightHeaderTab() {
                var tabHeader = $(".ajax__tab_header > span").get();
                var child, header, headerH, childH;
                for (var i = 0; i < tabHeader.length; i++) {
                    headerH = $(".ajax__tab_header > span").get(i).offsetHeight;
                    childH = $(".ajax__tab_header > span").get(i).firstChild.offsetHeight;

                    if (childH < (headerH - 1)) {
                        var outer = $(".ajax__tab_header > span").get(i).firstChild;


                        $(outer).attr("style", "height: " + (headerH + 1) + "px !important");

                    }
                }
            }

            function ConfirmToDeleteProject() {
                var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                var isConfirm = window.confirm(message);
                return isConfirm;
            }

            function ConfirmToSubmitProject() {
                var message  = <%= (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)?Nep.Project.Common.Web.WebUtility.ToJSON(Message.OfficerConfirmSubmitProjectInfo) :  Nep.Project.Common.Web.WebUtility.ToJSON(Message.ConfirmSubmitProjectInfo) %>;
                var isConfirm = window.confirm(message);
                return isConfirm;
            }

            function ConfirmToSubmitRportResult() {
                var isConfirm = window.confirm('<%= (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)? Message.ConfirmSendProjectReport :  Message.ConfirmSubmitProjectReport %>');
                return isConfirm;
            }

            function clientActiveTabChanged(sender, args) {

                //c2x.clearResultMsg();
                //var tabPanels = $('.ajax__tab_body').find(".ajax__tab_panel");
                //var tabPanel;
                //var visibility;
                //for (var i = 0; i < tabPanels.length; i++) {
                //    tabPanel = tabPanels[i];
                //    visibility = $(tabPanel).css("visibility");
                //    if (visibility == "visible") {
                //        $(tabPanel).empty();
                //    }
                //}
            }

            function SetTabHeader(tabNames) {   
                 //console.log(tabNames);
                var header = $('#<%=TabContainerProjectInfoForm.ClientID %>').find(".ajax__tab_header");
                var headerTab = (typeof(header) != "undefined")? ((header).find(".ajax__tab_outer")) : null;

                if((headerTab != null) && (tabNames != null)){
                    var tabName = "";
                    for(var i = 0; i < tabNames.length; i++){
                        tabName = (tabNames[i]).toLowerCase();
                        
                        if(tabName == "projectinformation"){
                            $(headerTab[1]).addClass("tab-required");
                        }else if(tabName == "projectpersonel"){
                            $(headerTab[2]).addClass("tab-required");
                        }else if(tabName == "projectoperation"){
                            $(headerTab[3]).addClass("tab-required");
                        }else if(tabName == "projectbudget"){
                            $(headerTab[4]).addClass("tab-required");
                        }else if(tabName == "projectdocument"){
                            $(headerTab[5]).addClass("tab-required");
                        }else if(tabName == "selfevaluate"){
                            $(headerTab[8]).addClass("tab-required");
                        }else if(tabName == "satisfy"){
                            $(headerTab[9]).addClass("tab-required");
                        }

                    }
                }
            }
            function RequiredTabByName(tabname){
                var tab = $("span.ajax__tab_outer:contains(" +  tabname + ")");
                if (tab){
                    tab.addClass("tab-required");
                }
            }
            function redirectoProjectList(param) {
                var url = '<%= ResolveUrl("~/ProjectInfo/ProjectInfoList")%>?'+ param;
                window.location.href = url;
            }

            function openRejectCommentForm() {
                var hiddProjectID = <%= Nep.Project.Common.Web.WebUtility.ToJSON(HiddenFieldProjectID.ClientID) %>;
                var id = $("#"+hiddProjectID).val();
                var pageUrl = '<%= ResolveUrl("~/ProjectInfo/RejectCommentPopup")%>';
                pageUrl = pageUrl + "?projectid=" + id;

                c2x.openFormDialog(pageUrl, '<%= Model.ProjectInfo_RejectComment%>', { width: 600, height: 500 }, null);
                return false;
            }
            function openReviseReportForm() {
                var hiddProjectID = <%= Nep.Project.Common.Web.WebUtility.ToJSON(HiddenFieldProjectID.ClientID) %>;
                var id = $("#"+hiddProjectID).val();
                var pageUrl = '<%= ResolveUrl("~/ProjectInfo/ReviseReportPopup")%>';
                pageUrl = pageUrl + "?projectid=" + id;

                c2x.openFormDialog(pageUrl, '<%= Model.ProjectInfo_RejectComment%>', { width: 600, height: 450 }, null);
                return false;
            }
            Sys.Extended.UI.TabPanel.prototype._header_onclick = function(e) {
                //Here we are using onclick event of header..
                
                var yn =  ConfirmChangeTab();
                if (yn) {
                    this.raise_click();
                    // add this additional code line to do validation
                    this.get_owner().set_activeTab(this);
                    // If validation is true sets the tab 
                }
              
            };

            function ConfirmChangeTab(){
                var divSaved = $("div.alert.alert-info" ) 
                if (divSaved.length > 0 && divSaved[0].innerText.search("บันทึกสำเร็จ") > 0){
                    return true;
                }
                return confirm('ข้อมูลยังไม่บันทึก ต้องการเปลี่ยน Tab หรือไม่?');
            }
        </script>
    </div>
    
</asp:Content>

<asp:Content ID="FooterContent" runat="server" ContentPlaceHolderID="FooterScript">
    <script type="text/javascript">
        //$(function () {
        //    if (typeof (bindEventToProjectDocumentAttachment) != 'undefined') {
        //        bindEventToProjectDocumentAttachment();
        //    }                  
        //});
    </script>    
</asp:Content>