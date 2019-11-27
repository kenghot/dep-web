<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollowupControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollowupControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>


<style >
    #ButtonAttach {
        padding-left:10px;
        padding-right:10px;
    }


    .auto-style1 {
        width: 1312px;
    }
    .auto-style2 {
        width: 100%;
    }


</style>

<asp:UpdatePanel ID="UpdatePanelFollowup" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <div class="panel panel-default"><!--ประวัติการส่งหนังสือติดตาม-->
            <div class="panel-heading">
                <h3 class="panel-title"><%=Nep.Project.Resources.UI.TitleFollowupHistory %></h3>
            </div> 
            <div class="panel-body">
                <div class="form-horizontal">  
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:Button runat="server" ID="ButtonFollowupPrint" CssClass="btn btn-default btn-sm" 
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonCreateTrackingDoc %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openPrintingForm();" />          
                        </div>
                    </div>                  
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanelPrintedDocument" runat="server" UpdateMode="Conditional" OnLoad="UpdatePanelPrintedDocument_Load" >
                                <ContentTemplate>
                                    <nep:GridView runat="server" ID="GridViewPrintedDocument" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="<%#Nep.Project.Common.Constants.PAGE_SIZE %>"
                                    CssClass="asp-grid" PagerStyle-CssClass="asp-pagination" DataKeyNames="ReportTrackingID" 
                                    SelectMethod="GridViewPrintedDocument_GetData"
                                    OnRowCommand="GridViewPrintedDocument_RowCommand">                                                        
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ code:Model.FollowupTrackingDocumentForm_No %>" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <%#Eval("No") %>
                                                    </ItemTemplate> 
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="<%$ code:Model.FollowupTrackingDocumentForm_SendDate %>">
                                                    <ItemTemplate>                                                  
                                                        <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Convert.ToDateTime(Eval("ReportDate")), Nep.Project.Common.Constants.UI_FORMAT_DATE, "") %>
                                                    </ItemTemplate> 
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="<%$ code:Model.FollowupTrackingDocumentForm_ReportNo %>">
                                                    <ItemTemplate>
                                                        <a href="javascript:void(0)" onclick='return openPrintingForm(<%#Eval("ReportTrackingID") %>, <%= Nep.Project.Common.Web.WebUtility.ToJSON(IsEditableFolloup) %>);'><%#Eval("ReportNo") %></a>                                                   
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ code:Model.FollowupTrackingDocumentForm_TrackingType %>">
                                                    <ItemTemplate>  
                                                        <%#Eval("ReportTrackingTypeName") %>                                                                                                            
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                                                    <ItemTemplate>  
                                                        <a href="javascript:void(0)" onclick='return openPrintingAttachment(<%#Eval("LetterAttchmentID") %>, "<%#Eval("LetterAttchmentName") %>");' class='icon <%#(Convert.ToBoolean(Eval("IsPdf")) == true)? "icon-pdf" : "icon-word" %>'></a>                                                   
                                                    
                                                        <asp:ImageButton ID="TrackingDocButtonDelete" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                                            CommandName="del" CommandArgument='<%# Eval("ReportTrackingID") %>' Visible='<%# (Convert.ToBoolean(Eval("IsDeletable")))%>' OnClientClick="return ConfirmDeleteTrackingDoc()" 
                                                            CausesValidation="true" />
                                                    </ItemTemplate> 
                                                </asp:TemplateField> 
                                            </Columns>
                                    </nep:GridView>
                                </ContentTemplate>                                
                            </asp:UpdatePanel>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>   
       
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%=Nep.Project.Resources.UI.TitleFollowup %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                       <asp:Label runat="server" ID="Label4" Text="ส่วนที่ 1 (50 คะแนน)" />          
                                  
                    </div> 
                    <div>
                        <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                     <%--       <colgroup>
                                <col style="width:50px">
                                <col style="width:135px"><col style="width:135px">
                                <col style="width:150px"><col style="width:90px">
                                <col style="width:90px"><col style="width:250px">

                            </colgroup>--%>
                            <thead class="k-grid-header" role="rowgroup">
                                <tr role="row">
                                 <%--<th scope="col" role="columnheader" data-field="No" rowspan="1" data-title="ลำดับ" data-index="0" id="efdc983f-8432-4625-9261-e515c41db2c6" class="k-header">ลำดับ</th>--%>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ลำดับที่</th>
                                <th scope="col" role="columnheader" class="k-header" >รายการ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:160px">ระบุ ในโครงการ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:160px">ผลการดำเนินการ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:160px">คะแนน</th>
                             <%--   <th scope="col" role="columnheader" class="k-header" style="width:150px">ไม่ตามแผน</th>--%>
</tr></thead>
<%--                                <th scope="col" role="columnheader" data-field="FirstName" rowspan="1" data-title="ชื่อ" data-index="1" id="2a530df2-9ca6-4b5b-8f4a-62219e593971" class="k-header" data-role="columnsorter">
                                    <a class="k-link" href="#">ชื่อ</a></th>
                                <th scope="col" role="columnheader" data-field="LastName" rowspan="1" data-title="นามสกุล" data-index="2" id="ffb683f7-3316-4e83-86ec-7ba87e7d368e" class="k-header" data-role="columnsorter">
                                    <a class="k-link" href="#">นามสกุล</a></th>
                                <th scope="col" role="columnheader" data-field="IDCardNo" rowspan="1" data-title="เลขประจำตัวประชาชน" data-index="3" id="c6ab05b9-e329-4aef-88e3-5c155c260668" class="k-header" data-role="columnsorter">
                                        <a class="k-link" href="#">เลขประจำตัวประชาชน</a></th>
                                    <th scope="col" role="columnheader" data-field="DdlGender" rowspan="1" data-title="เพศ" data-index="4" id="fde575ab-70cb-4693-858a-9030b090f546" class="k-header">เพศ</th><th scope="col" role="columnheader" data-field="DdlIsCripple" rowspan="1" data-title="ประเภทผู้เข้าร่วม" data-index="5" id="a5d10e57-6d86-4d2e-9791-9faccfd6b83c" class="k-header">ประเภทผู้เข้าร่วม</th><th scope="col" role="columnheader" data-field="DdlTargetGroup" rowspan="1" data-title="กลุ่มเป้าหมาย" data-index="6" id="622e982b-4a43-4f7c-b273-bc65446fd781" class="k-header" data-role="columnsorter"><a class="k-link" href="#">กลุ่มเป้าหมาย</a></th></tr></thead><tbody role="rowgroup"> --%>
                                
                                <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">1</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>จำนวนผู้เข้ารวม</b><br />
                                        (20 คะแนน)<br />
                                        100%      = 20 คะแนน <br />
                                        99-80%    = 18 คะแนน <br />
                                        79-60%    = 16 คะแนน <br />
                                        น้อยกว่า 60% = 14 คะแนน  
                                    </td>
                                    <td  style="text-align:center;" role="gridcell">
                                        <asp:Label  runat="server" ID="lblParticipant1" Text="5"></asp:Label> คน

                                    </td>
                                    <td style="text-align:center;" >
                                         <asp:Label  runat="server" ID="lblParticipant2" Text="5"></asp:Label> คน
                                                    <%--<div class="col-sm-3">--%>
                                  <%--  <label style="font-weight:normal; float:left;margin-top:4px;padding-left:6px; padding-right:10px;">จำนวน</label>--%>
                             <%--       <nep:TextBox ID="TextBoxParticipant" runat="server" Width="80px" PlaceHolder="คน" TextMode="Number" 
                                        NumberFormat="N0"  Min="0" Max="99999" CssClass="form-control" /> คน

                                    <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroupAmount" runat="server"
                                        data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxParticipant.ClientID %>"
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.RequiredField, "จำนวนคน") %>
                                    </span>--%>
                                <%--</div>--%>
                                    </td>
                                    <td class="text-center" role="gridcell">
                                        <asp:TextBox runat="server" ID="txbParticipantScore" Width="80px" Text="0"></asp:TextBox> คะแนน
                                        <%--<asp:Label  runat="server" ID="lblParticipantScore1" Text="0 คะแนน"></asp:Label>--%>
                                    </td>
                               <%--     <td class="text-center" role="gridcell">
                                        <asp:Label  runat="server" ID="lblParticipantScore2" Text="0 คะแนน"></asp:Label>

                                    </td>--%>
   
                                </tr>
                                <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">2</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>จำนวนกิจกรรม</b><br />
                                        (20 คะแนน)<br />
                                        100%      = 20 คะแนน <br />
                                        99-80%    = 18 คะแนน <br />
                                        79-60%    = 16 คะแนน <br />
                                        น้อยกว่า 60% = 14 คะแนน 
                                    </td>
                                 <td  style="text-align:center;" role="gridcell">
                                     <%--    <nep:TextBox ID="txbActivity1" runat="server" Width="80px" PlaceHolder="กิจกรรม" TextMode="Number" 
                                        NumberFormat="N0"  Min="0" Max="99999" CssClass="form-control" /> กิจกรรม
                                    <span class="project-target-group-validate error-text" id="ValidatetxbActivity1" runat="server"
                                        data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:txbActivity1.ClientID %>"
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.RequiredField, "จำนวนกิจกรรม") %>                                       
                                    </span>--%>
                                     <asp:TextBox runat="server" ID="txbActivity1" Width="80px" Text="0"></asp:TextBox> กิจกรรม 
                                    </td>
                                        <td style="text-align:center;"><%--<div class="col-sm-3">--%><%--  <label style="font-weight:normal; float:left;margin-top:4px;padding-left:6px; padding-right:10px;">จำนวน</label>--%><%--       <nep:TextBox ID="txbActivity2" runat="server" Width="80px" PlaceHolder="กิจกรรม" TextMode="Number" 
                                        NumberFormat="N0"  Min="0" Max="99999" CssClass="form-control" /> กิจกรรม
                                    <span class="project-target-group-validate error-text" id="ValidatetxbActivity2" runat="server"
                                        data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:txbActivity2.ClientID %>"
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.RequiredField, "จำนวนกิจกรรม") </span>
                                 %>--%>
                                            <asp:TextBox ID="txbActivity2" runat="server" Width="80px" Text="0"></asp:TextBox>
                                            กิจกรรม <%--</div>--%></td>
                                        <td class="text-center" role="gridcell">
                                            <asp:TextBox ID="txbActivityScore" runat="server" Width="80px" Text="0"></asp:TextBox> คะแนน 

                                        </td>
                                  <%--      <td class="text-center" role="gridcell">
                                            <asp:Label ID="lblActivityScore2" runat="server" Text="0 คะแนน"></asp:Label>
                                        </td>--%>
                                    </caption>
   
                                </tr>
                                <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">3</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>ระยะเวลาดำเนินโครงการ</b><br />
                                        (10 คะแนน)<br />
                                        100%      = 10 คะแนน <br />
                                        99-80%    = 8 คะแนน <br />
                                        79-60%    = 6 คะแนน <br />
                                        น้อยกว่า 60% = 4 คะแนน  
                                    </td>
                                    <td  style="text-align:center;" role="gridcell">
                                        <asp:Label  runat="server" ID="lblPeriod1" Text="กุมภาพันธ์ ถึง เมษายน 2559"></asp:Label>

                                    </td>
                                    <td style="text-align:center;" >
                                        <%--<asp:Label  runat="server" ID="lblPeriod2" Text="กุมภาพันธ์ ถึง เมษายน 2559"></asp:Label>--%>
                                        <nep:DatePicker runat="server" ID="ProjectInfoStartDate" ClearTime="true" EnabledTextBox="true" />  ถึง <br />
                                        <nep:DatePicker runat="server" ID="ProjectInfoEndDate" ClearTime="true" EnabledTextBox="true" /> 
                               <%--<label class="col-sm-4 control-label"><%= Model.ProcessingPlan_StartDate %><span class="required"></span></label>--%>
                           
                              <%--  <nep:DatePicker runat="server" ID="ProcessingPlanStartDate" ClearTime="true" EnabledTextBox="true" 
                                    ValidationGroup="SaveProcessingPlan" 
                                    OnClientDateSelectionChanged="onProcessingPlanDateSelectionChanged" 
                                    OnClientDateTextChanged="onProcessingPlanDateSelectionChanged(null, null)"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlanStartDate" ControlToValidate="ProcessingPlanStartDate" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_StartDate) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_StartDate) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>
                          <label class="col-sm-4 control-label"><%= Model.ProcessingPlan_EndDate %><span class="required"></span></label>
                            <div class="col-sm-8">
                                <nep:DatePicker runat="server" ID="ProcessingPlanEndDate" ClearTime="true" EnabledTextBox="true"
                                     ValidationGroup="SaveProcessingPlan" 
                                    OnClientDateSelectionChanged="onProcessingPlanDateSelectionChanged" 
                                    OnClientDateTextChanged="onProcessingPlanDateSelectionChanged(null, null)"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlanEndDate" ControlToValidate="ProcessingPlanEndDate" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_EndDate) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProcessingPlan_EndDate) %>"
                                ValidationGroup="SaveProcessingPlan" SetFocusOnError="true"/>

                                <asp:CustomValidator ID="CustomValidatorPlanDate" CssClass="error-text" runat="server" 
                                    OnServerValidate="CustomValidatorPlanDate_ServerValidate"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProcessingPlan_EndDate, Model.ProcessingPlan_StartDate) %>" 
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.ProcessingPlan_EndDate, Model.ProcessingPlan_StartDate) %>"
                                    ValidationGroup="SaveProcessingPlan" />--%>
                                <%--</div>--%>
                                    </td>
                                    <td class="text-center" role="gridcell">
                                        <asp:TextBox ID="txbPeriodScore" runat="server" Width="80px" Text="0"></asp:TextBox> คะแนน 
                                    </td>
                                   <%-- <td class="text-center" role="gridcell">
                                        <asp:Label  runat="server" ID="Label3" Text="0 คะแนน"></asp:Label>

                                    </td>--%>
   
                                </tr>
                            <tr>
                                <td colspan="3"></td>
                                <td style="text-align:right">รวมคะแนน</td>
                                <td style="text-align:center" > 
                                    <asp:Label runat="server" Text="0" ID="lblTotalScore1"></asp:Label> คะแนน

                                </td>
                            </tr>
                                  <tr>
                                <td colspan="3"></td>
                                <td style="text-align:right">คิดเป็นร้อยละ</td>
                                <td style="text-align:center" > 
                                    <asp:Label runat="server" Text="0" ID="lblTotalPercent1"></asp:Label>  

                                </td>
                            </tr>
                                <%--<tr class="k-alt" data-uid="616b79ae-5ae2-4186-b8d3-0ad832977853" role="row"><td class="participant-no" role="gridcell">2</td><td role="gridcell">dd</td><td role="gridcell">dd</td><td class="text-center" role="gridcell">3-1006-03026-46-3</td><td class="text-center" role="gridcell">ชาย</td><td class="text-center" role="gridcell">วิทยากร</td><td role="gridcell">-</td></tr>--%>
                           </table>
                    </div>
                    <div class="form-group form-group-sm" ><asp:Label runat="server" ID="Label5" Text="ส่วนที่ 2 (50 คะแนน)" /></div>           
                    <div>

                        <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                     <%--       <colgroup>
                                <col style="width:50px">
                                <col style="width:135px"><col style="width:135px">
                                <col style="width:150px"><col style="width:90px">
                                <col style="width:90px"><col style="width:250px">

                            </colgroup>--%>
                                                     <thead class="k-grid-header" role="rowgroup">
                                <tr role="row">
                                 <%--<th scope="col" role="columnheader" data-field="No" rowspan="1" data-title="ลำดับ" data-index="0" id="efdc983f-8432-4625-9261-e515c41db2c6" class="k-header">ลำดับ</th>--%>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width:50px">ลำดับที่</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" >รายการ</th>
                                <th colspan="3" scope="col" role="columnheader" class="k-header" >ระบุ ในโครงการ</th>
                                <th colspan="3" scope="col" role="columnheader" class="k-header" >ผลการดำเนินการ</th>
                                <th rowspan="2" role="columnheader" class="k-header" style="width:150px">คะแนนรวม</th>
                                <%--<th scope="col" role="columnheader" class="k-header" style="width:150px">ไม่ตามแผน</th>--%>
                               </tr>
                        <tr role="row">
                                 <%--<th scope="col" role="columnheader" data-field="No" rowspan="1" data-title="ลำดับ" data-index="0" id="efdc983f-8432-4625-9261-e515c41db2c6" class="k-header">ลำดับ</th>--%>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ร้อยละ<br />100</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ร้อยละ<br />99-60</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ต่ำกว่าร้อยละ<br />60</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ร้อยละ<br />100</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ร้อยละ<br />99-60</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ต่ำกว่าร้อยละ<br />60</th>
                               </tr>

                                                     </thead>
                                <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">1</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>เปรียบเทียบกับวัตถุประสงค์</b><br />
                                        (15 คะแนน)<br />
                                        100%      = 15 คะแนน <br />
                                        99-60%    = 13 คะแนน <br />
                                        น้อยกว่า 60% = 11 คะแนน 
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective1_1" runat="server" GroupName="Objective1" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective1_2" runat="server" GroupName="Objective1" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective1_3" runat="server" GroupName="Objective1" />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective2_1" runat="server" GroupName="Objective2" Value="15" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective2_2" runat="server" GroupName="Objective2" Value="13" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbObjective2_3" runat="server" GroupName="Objective2" Value="11" />
                                    </td>                                                                                        
                                    <td style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:Label  runat="server" ID="lblObjectiveScore" Text="0"></asp:Label> คะแนน

                                    </td>
   
                                </tr>
                            <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">2</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>เปรียบเทียบกับเป้าหมาย</b><br />
                                        (15 คะแนน)<br />
                                        100%      = 15 คะแนน <br />
                                        99-60%    = 13 คะแนน <br />
                                        น้อยกว่า 60% = 11 คะแนน  
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget1_1" runat="server" GroupName="Target1" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget1_2" runat="server" GroupName="Target1" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget1_3" runat="server" GroupName="Target1" />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget2_1" runat="server" GroupName="Target2" Value="15" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget2_2" runat="server" GroupName="Target2" Value="13" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbTarget2_3" runat="server" GroupName="Target2" Value="11" />
                                    </td>                                                                                        
                                    <td style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:Label  runat="server" ID="lblTargetScore" Text="0"></asp:Label> คะแนน

                                    </td>
   
                                </tr>
                            <tr  role="row">
                                    <td style="text-align:center;" role="gridcell">3</td>
                                    <td class="text-center"  role="gridcell">
                                        <b>ผลลัพธ์หรือความสำเร็จ</b><br />
                                        (20 คะแนน)<br />
                                        100%      = 20 คะแนน <br />
                                        99-60%    = 18 คะแนน <br />
                                        น้อยกว่า 60% = 16 คะแนน  
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult1_1" runat="server" GroupName="Result1" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult1_2" runat="server" GroupName="Result1" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult1_3" runat="server" GroupName="Result1" />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult2_1" runat="server" GroupName="Result2" Value="20" />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult2_2" runat="server" GroupName="Result2"  Value="18" />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:RadioButton ID="rdbResult2_3" runat="server" GroupName="Result2"  Value="16" />
                                    </td>                                                                                        
                                    <td style="text-align:center;vertical-align:central" role="gridcell">
                                        <asp:Label  runat="server" ID="lblResultScore" Text="0"></asp:Label> คะแนน

                                    </td>
   
                                </tr>
                             <tr>
                                <td colspan="7"></td>
                                <td style="text-align:right">รวมคะแนน</td>
                                <td style="text-align:center" > 
                                    <asp:Label runat="server" Text="0" ID="lblTotalScore2"></asp:Label> คะแนน

                                </td>
                            </tr>
                                  <tr>
                                <td colspan="7"></td>
                                <td style="text-align:right">คิดเป็นร้อยละ</td>
                                <td style="text-align:center" > 
                                    <asp:Label runat="server" Text="0" ID="lblTotalPercent2"></asp:Label>  

                                </td>
                            </tr>                               
                                
                                <%--<tr class="k-alt" data-uid="616b79ae-5ae2-4186-b8d3-0ad832977853" role="row"><td class="participant-no" role="gridcell">2</td><td role="gridcell">dd</td><td role="gridcell">dd</td><td class="text-center" role="gridcell">3-1006-03026-46-3</td><td class="text-center" role="gridcell">ชาย</td><td class="text-center" role="gridcell">วิทยากร</td><td role="gridcell">-</td></tr>--%>
                                </table>
                    </div>
                 </div>  
                 <asp:Panel ID="pnlOld" runat="server" Visible="false"> 
                     <label class="col-sm-10 control-label control-label-left without-delimit" ><asp:Label runat="server" ID="LabelApprovalBudget" /></label>
               
                    <div class="form-group form-group-sm"><!-- 1.ชื่อโครงการ -->
                        <label class="col-sm-12 form-group-title">1.<%=Nep.Project.Resources.Model.ProjectInfo_Name %><span class="required"></span></label>

                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssessmentProjectName" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentProjectName" ControlToValidate="TextBoxAssessmentProjectName" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_Name) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_Name) %>"
                                ValidationGroup="SaveFollowupx" />
                            
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssessmentProjectNameScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssessmentProjectNameScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                               
                            </asp:DropDownList>   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentProjectNameScore" ControlToValidate="DropDownListAssessmentProjectNameScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_Name) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_Name) %>"
                                ValidationGroup="SaveFollowupx" />                         
                        </div>                       
                     </div>
                
                <div class="form-horizontal"><!-- 2.หลักการและเหตุผล -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            2.<%=Nep.Project.Resources.Model.ProjectInfo_Principles %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssessmentPrinciples" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentPrinciples" ControlToValidate="TextBoxAssessmentPrinciples" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssessmentPrinciplesScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssessmentPrinciplesScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                      
                            </asp:DropDownList> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentPrinciplesScore" ControlToValidate="DropDownListAssessmentPrinciplesScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_Principles) %>"
                                ValidationGroup="SaveFollowupx" />                            
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!-- 3.วัตถุประสงค์ของโครงการ -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            3.<%=Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssessmentObjective" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxAssessmentObjective" ControlToValidate="TextBoxAssessmentObjective" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssessmentObjectiveScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssessmentObjectiveScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                              
                            </asp:DropDownList> 
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentObjectiveScore" ControlToValidate="DropDownListAssessmentObjectiveScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoObjective) %>"
                                ValidationGroup="SaveFollowupx" />                          
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!-- 4.กลุ่มเป้าหมาย -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            4.<%=Nep.Project.Resources.Model.ProjectTarget_TargetName %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssementTargetName" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTargetName" ControlToValidate="TextBoxAssementTargetName" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssementTargetNameScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssementTargetNameScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                                           
                            </asp:DropDownList>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementTargetNameScore" ControlToValidate="DropDownListAssementTargetNameScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>"
                                ValidationGroup="SaveFollowupx" />                           
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!-- 5.สถานที่ -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            5.<%=Nep.Project.Resources.Model.ProjectInfo_SupportPlace %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssementSupportPlace" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementSupportPlace" ControlToValidate="TextBoxAssementSupportPlace" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_SupportPlace) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_SupportPlace) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssementSupportPlaceScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssementSupportPlaceScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                         
                            </asp:DropDownList> 
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementSupportPlaceScore" ControlToValidate="DropDownListAssementSupportPlaceScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_SupportPlace) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_SupportPlace) %>"
                                ValidationGroup="SaveFollowupx" />                            
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!--6.ระยะเวลา-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            6.<%=Nep.Project.Resources.Model.ProjectFollowup_Period %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssementPeriod" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementPeriod" ControlToValidate="TextBoxAssementPeriod" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Period) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Period) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssementPeriodScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssementPeriodScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                       
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementPeriodScore" ControlToValidate="DropDownListAssementPeriodScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Period) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Period) %>"
                                ValidationGroup="SaveFollowupx" />                             
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!--7.วิธีการดำเนินงาน-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            7.<%=Nep.Project.Resources.Model.ProjectFollowup_Processing %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssementProcessing" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementProcessing" ControlToValidate="TextBoxAssementProcessing" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Processing) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Processing) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssementProcessingScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssementProcessingScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                   
                            </asp:DropDownList>   
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementProcessingScore" ControlToValidate="DropDownListAssementProcessingScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Processing) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Processing) %>"
                                ValidationGroup="SaveFollowupx" />                          
                        </div>                       
                     </div>
                </div>
                <div class="form-horizontal"><!--8.ข้อบ่งชี้ด้านงบประมาณ-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line">
                            8.<%=Nep.Project.Resources.Model.ProjectFollowup_Bubget %><span class="required"></span></label></div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssemetBubget" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssemetBubget" ControlToValidate="TextBoxAssemetBubget" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Bubget) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Bubget) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssemetBubgetScore" /></label>  
                        <label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>                                                      
                        <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssemetBubgetScore" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                           
                            </asp:DropDownList>   
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssemetBubgetScore" ControlToValidate="DropDownListAssemetBubgetScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Bubget) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectFollowup_Bubget) %>"
                                ValidationGroup="SaveFollowupx" />                          
                        </div>                       
                     </div>
                </div>
               </asp:Panel>
                <div class="form-horizontal"><!--9.ผลที่คาดว่าจะได้รับ-->
                    <div class="form-group form-group-sm">
                    <%--    <label class="col-sm-12 form-group-title form-group-title-line">
                            9.<%=Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation%></label>--%>
                          <label class="col-sm-12 form-group-title form-group-title-line">
                            ข้อเสนอแนะ</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxAssementAnticipation" runat="server" TextMode="MultiLine" CssClass="form-control "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementAnticipation" ControlToValidate="TextBoxAssementAnticipation" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>"
                                ValidationGroup="SaveFollowupx" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                  <%--<label class="col-sm-3 control-label"><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>--%>     
                        <label class="col-sm-2 control-label control-label-left without-delimit"><asp:Label runat="server" ID="LabelAssementAnticipationScore" Visible="false" /></label>  
                        <%--<label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>--%>                                               
                     <div class="col-sm-2">
                            <asp:DropDownList ID="DropDownListAssementAnticipationScore" Visible="false" runat="server" CssClass="form-control tracking-dropdownlist"
                                DataTextField="Text" DataValueField="Value">                                                        
                            </asp:DropDownList>   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssementAnticipationScore" ControlToValidate="DropDownListAssementAnticipationScore" runat="server"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.AssessmentScoreRequiredField, Nep.Project.Resources.Model.ProjectInfo_ProjectInfoAnticipation) %>"
                                ValidationGroup="SaveFollowupx" />                         
                        </div>                     
                     </div>
                </div>
            </div>
        </div><!--การมีส่วนร่วมขององค์การปกครองส่วนท้องถิ่น--> 
 

        <div class="panel panel-default"><!--สรุปคะแนนทั้ง 9 ข้อ-->
            <div class="panel-heading">
                <%--<h3 class="panel-title"><%=Nep.Project.Resources.UI.TitleFollowupSummaryEvaluate %></h3>--%>
                <h3 class="panel-title">รวมคะแนน</h3>
            </div> 
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                       <%-- <label class="col-sm-3 control-label "><%=Nep.Project.Resources.UI.ProjectApprovalGroup %></label>     
                        <label class="col-sm-2 control-label control-label-left without-delimit" runat="server" id="LabelSummayAssementScore"></label>  --%>
                        <table>
                            <tr>
                                <td style="width:200px">ส่วนที่ 1</td>
                                <td style="text-align:right">
                                    <asp:Label runat="server" Text="0" ID="lblToatalScorePart1"></asp:Label> คะแนน

                                </td>
                                <%--<td style="text-align:right"><span id="lblToatalScorePart1">0</span> คะแนน</td>--%>
                            </tr>
                            <tr>
                                <td>ส่วนที่ 2</td>
                                <td style="text-align:right">
                                    <asp:Label runat="server" Text="0" ID="lblToatalScorePart2"></asp:Label> คะแนน
                                 </td>
                                <%--<td style="text-align:right"><span id="lblToatalScorePart2">0</span> คะแนน</td>--%>
                            </tr>
                            <tr>
                                <td>คะแนนรวม</td>
                                <td style="text-align:right"><asp:Label runat="server" Text="0" ID="lblTotalScore"></asp:Label> คะแนน</td>
                            </tr>
                            <tr>
                                <td>คิดเป็นร้อยละ</td>
                                <td style="text-align:right"><asp:Label runat="server" Text="0" ID="lblTotalPercent"></asp:Label>  </td>
                            </tr>
                        </table>
                      
                        <%--<label class="col-sm-4 control-label"><%=Nep.Project.Resources.UI.ProjectFollowupGroup %></label>--%>              
                    <%--    <div class="col-sm-3">                                      
                            <label class="control-label control-label-left without-delimit followup-score" runat="server" id="LabelSummayFollowupScore"></label>   
                            <label class="control-label control-label-left without-delimit text-bold followup-score-desc" runat="server" id="LabelSummayFollowupScoreDesc"></label>      
                        </div> --%>                                    
                        </div>
                </div> 
           <%--     <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.EvalutionDescription) %>                
                        </div>                                    
                    </div>
                </div> --%>
            </div>                           
        </div><!--สรุปคะแนนทั้ง 9 ข้อ--> 

        <div class="panel panel-default"><!--แนบเอกสาร-->
            <div class="panel-heading">
                <h3 class="panel-title"><%=Nep.Project.Resources.UI.TitleFollowupAttachment %></h3>
              
            </div> 
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <nep:C2XFileUpload runat="server" ID="C2XFileUploadProjectFollowupAttachment" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" MultipleFileMode="true" /> 
                        </div>
                    </div>
                </div>
            </div>
        </div><!--แนบเอกสาร-->
        
  
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                   
                    <asp:HiddenField ID="HiddenValue" runat="server" />
                    <asp:Button runat="server" ID="ButtonSaveFollowup" CssClass="btn btn-primary btn-sm" OnClick="ButtonSaveFollowup_Click"  
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" ValidationGroup="SaveFollowup" Visible="false"/>

                                              

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                                NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                                Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>        

        <script type="text/javascript">
         
          
        </script>

    </ContentTemplate>
</asp:UpdatePanel>



