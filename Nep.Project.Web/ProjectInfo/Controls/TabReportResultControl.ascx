<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabReportResultControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabReportResultControl"  %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelReportResult" 
                    UpdateMode="Conditional" 
                    runat="server" >

    <ContentTemplate>
        <style type="text/css">
            #RadioButtonListComparePurpose > tbody > tr:first-child > td > input[type="radio"]:first-child {
                position: absolute;
            }

            #RadioButtonListComparePurpose > tbody > tr:first-child > td .control-label-radio.radio-block {
                margin-left: 18px;
            }

            div.combobox-block {
                position: relative !important;
            }

            .ajax__combobox_itemlist {
                top: 27px !important;
                left: 7px !important;
            }



            .btn-hide {
                display: none;
            }

            .button-add-participant, .button-clear-participant {
                margin-top: 6px;
                margin-right: 5px;
                opacity: .6;
            }

                .button-add-participant[disabled="disabled"]:hover, .button-clear-participant[disabled="disabled"]:hover {
                    opacity: .6;
                }

                .button-add-participant:hover, .button-clear-participant:hover {
                    opacity: 1;
                }
            /*.k-grid-with-pager .k-grid-pager {
                 width:965px;
            }

            .k-grid-with-pager .k-grid-pager.k-grid-pager-width {
                 width:900px !important;
            }*/

            .maskedtextbox {
                width: 99%;
            }

            .k-grid .k-dropdown-wrap {
                padding-right: 18px;
            }
        </style>
        
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%: UI.TitleProjectReport %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%: Model.ProjectReportResult_ActivityDescription %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <div class="required-block">
                                <nep:TextBox ID="TextBoxActivityDescription" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorActivityDescription" ControlToValidate="TextBoxActivityDescription" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                />                   
                        </div>
                    </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label control-label-left without-delimit">
                                     แนบไฟล์
                                </label>
                                <div class="col-sm-8">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadActivityAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div>
                    <!--ผู้เข้าร่วมโครงการ/กิจกรรม-->
                    <div class="col-sm-12 form-group-title-line" style="padding-left:0px">  <%--class="form-group form-group-sm">--%>

                        <label><%:Model.ProjectReportResult_Participants %></label>
                            <asp:Button runat="server" ID="btnDownloadForm" Text="ดาวน์โหลดแบบฟอร์มเพื่อกรอกข้อมูล ผู้เข้าร่วมโครงการ/กิจกรรม" 
                             OnClientClick="window.open('../Content/Files/ProjectParticipantForm.xlsx','_blank');return false;" />
                       
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                        <nep:TextBox runat="server" CssClass="form-control" ID="TextBoxCheckId" MaxLength="13" 
                                            PlaceHolder="ตรวจสอบบัตรประชาชน" ToolTip="ตรวจสอบบัตรประชาชน"/>

                            </div>
                            <div class="col-sm-3">

                            <button type="button" class="btn btn-default btn-sm" onclick="objIMP.CheckIDFromDisability()">ตรวจสอบจากข้อมูลจากทะเบียนกลางคนพิการ</button>
                            </div>
                        </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <div class="form-horizontal custom-padd">                            
                                <div class="form-group form-group-sm" id="CreateParticipantForm" runat="server">
                                    <div class="col-sm-2">
                                        <nep:TextBox runat="server" CssClass="form-control" ID="TextBoxParticipantFirstName" MaxLength="50" 
                                            PlaceHolder="<%$ code:Model.ProjectParticipant_FirstName %>" ToolTip="<%$ code:Model.ProjectParticipant_FirstName %>"/>
                                        <span class="participant-validate error-text"  id="ValidateParticipantFirstName" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantFirstName.ClientID %>" style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  Model.ProjectParticipant_FirstName) %>
                                        </span>
                                    </div>
                                    <div class="col-sm-2">
                                        <nep:TextBox runat="server" ID="TextBoxParticipantLastName" CssClass="form-control" MaxLength="50" 
                                            PlaceHolder="<%$ code:Model.ProjectParticipant_LastName %>"  ToolTip="<%$ code:Model.ProjectParticipant_FirstName %>"/>
                                        <span class="participant-validate error-text"  id="ValidateParticipantLastName" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantLastName.ClientID %>" style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  Model.ProjectParticipant_LastName) %>
                                        </span>
                                    </div>
                                    <div class="col-sm-2">
                                        <nep:TextBox runat="server" ID="TextBoxParticipantIDCardNo" CssClass="form-control maskedtextbox" MaxLength="50" 
                                            PlaceHolder="<%$ code:Model.ProjectParticipant_IDCardNo %>" ToolTip="<%$ code:Model.ProjectParticipant_IDCardNo %>"/>
                                        <span class="participant-validate error-text"  id="ValidateRequiredParticipantIDCardNo" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantIDCardNo.ClientID %>" style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  Model.ProjectParticipant_IDCardNo) %>
                                        </span>
                                        <span class="participant-validate error-text"  id="ValidateParticipantIDCardNo" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantIDCardNo.ClientID %>" style="display:none;">
                                            <%: Nep.Project.Resources.Error.InvalidIDCardNo %>
                                        </span>
                                    </div>
                                    <div class="col-sm-1">
                                        <nep:TextBox runat="server" ID="TextBoxParticipantGender"  Width="100%"
                                            PlaceHolder="<%$ code:Model.ProjectParticipant_Gender %>" ToolTip="<%$ code:Model.ProjectParticipant_Gender %>"/>
                                        <span class="participant-validate error-text"  id="ValidateParticipantGender" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantGender.ClientID %>" style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  Model.ProjectParticipant_Gender) %>
                                        </span>
                                    </div>
                                    <div class="col-sm-2">
                                        <nep:TextBox runat="server" ID="TextBoxIsCripple"  Width="100%"
                                            PlaceHolder="<%$ code:Model.ProjectParticipant_IsCripple %>" ToolTip="<%$ code:Model.ProjectParticipant_IsCripple %>"/>
                                        <span class="participant-validate error-text"  id="ValidateParticipantGenderIsCripple" runat="server"
                                            data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxIsCripple.ClientID %>" style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  Model.ProjectParticipant_IsCripple) %>
                                        </span>
                                    </div>
                                    <div class="col-sm-2" >
                                        <div>
                                            <nep:TextBox ID="TextBoxParticipantTargetGroup" runat="server"  PlaceHolder="กลุ่มเป้าหมาย"
                                                 AutoPostBack="false" Width="100%" ToolTip="กลุ่มเป้าหมาย"/>                       
                                            <span class="participant-validate error-text" id="ValidateParticipantTargetGroup" runat="server"
                                                data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantTargetGroup.ClientID %>"
                                                style="display:none;">
                                                <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectParticipant_TargetGroup) %>
                                            </span>
                                        </div>
                                        <div id="ParticipantTargetGroupEtcBlock" style="display:none; margin-top:7px;">
                                            <nep:TextBox ID="TextBoxParticipantTargetGroupEtc" MaxLength="1333" runat="server" PlaceHolder="ชื่อกลุ่มเป้าหมาย" CssClass="form-control"  
                                                    />
                                            <span class="participant-validate error-text"  id="ValidateParticipantTargetGroupEtc" runat="server"
                                                data-val-validationgroup="SaveParticipant" data-val-controltovalidate="<%$ code:TextBoxParticipantTargetGroupEtc.ClientID %>"
                                                style="display:none;">
                                                <%: String.Format(Nep.Project.Resources.Error.RequiredField,  "ชื่อกลุ่มเป้าหมายอื่นๆ") %>
                                            </span>
                                        </div>
                                        <span class="participant-validate error-text" id="ValidateParticipantTargetGroupEtcDupCreate" 
                                                data-val-validationgroup="SaveParticipant" 
                                            style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>
                                        </span>
                                    </div>                                   
                                    <div class="col-sm-1" style="padding-left:15px;">                                       
                                        <asp:ImageButton ID="ImageButtonTemp" runat="server" OnClientClick="return false;" CssClass="btn-hide" />

                                        <asp:ImageButton ID="ImageButtonSaveParticipant" runat="server" ToolTip="เพิ่ม"
                                            ImageUrl="~/Images/icon/round_plus_icon_16.png" BorderStyle="None" CssClass="button-add-participant"
                                            OnClientClick="return c2xProjectReport.createRowParticipant(event)"/>
                                        <asp:ImageButton ID="ImageButtonCancelParticipant" runat="server" ToolTip="ล้างข้อมูล" 
                                            ImageUrl="~/Images/icon/brush_icon_16.png" BorderStyle="None" CssClass="button-clear-participant"
                                            OnClientClick="return c2xProjectReport.cancelCreateRowParticipant();"/>
                    
                                    </div>  
                                </div>
                                 <div class="form-group form-group-sm">

                                <div class="col-sm-6">    
                                    <%--<nep:C2XFileUpload runat="server" ID="C2XFileUploadExcel" MultipleFileMode="false" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />--%>  
                                <%--    <span id="C2XFileUploadExcel3" data-control-type="C2XFileUpload">
                                    <div class="input-file-container single-file">
					                <div class="k-widget k-upload k-header"><div class="k-dropzone">--%>
                                    <div class="k-button k-upload-button">
                                         <%--onchange="objIMP.importFile($('#excelFile')); return false;"--%>
                                    <input  type="file" id="excelFile" name="excelFile" /><span>อัพโหลดไฟล์ผู้เข้าร่วมโครงการ/กิจกรรม</span>
                                        </div>
                                       <%-- </div>
                                        </div>
                                        </div>
                                        </span>--%>
                                </div> 

                                 </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-12">
                                        <asp:HiddenField ID="HiddenFieldParticipant" runat="server" />
                                        <div id="ParticipantGrid" runat="server" class="k-grid-with-pager"></div>
                                    </div>
                                </div>
                                
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-1 control-label">
                                        <%=UI.LabelMale %>
                                    </label>
                                    <div class="col-sm-1 control-value">
                                        <asp:HiddenField ID="HiddTotalMaleParticipant" runat="server" />
                                        <asp:Label ID="LabelTotalMaleParticipant" CssClass="total-male-participant" runat="server" Text="0"></asp:Label> <%=UI.LabelPerson %>
                                    </div>
                                    <label class="col-sm-1 control-label">
                                        <%=UI.LabelFemale %>
                                    </label>
                                    <div class="col-sm-1 control-value">
                                        <asp:HiddenField ID="HiddTotalFemaleParticipant" runat="server" />
                                        <asp:Label ID="LabelTotalFemaleParticipant" CssClass="total-female-participant" runat="server" Text="0"></asp:Label> <%=UI.LabelPerson %>
                                    </div>
                                    <label class="col-sm-1 control-label">
                                        <%=UI.LabelTotal %>
                                    </label>
                                    <div class="col-sm-1 control-value">
                                        <asp:Label ID="LabelTotalParticipant" CssClass="total-total-participant" runat="server" Text="0"></asp:Label> <%=UI.LabelPerson %>
                                    </div>
                                    <div class="col-sm-4">
                                         <asp:Button OnClientClick="objIMP.CheckDESData(); return false;" runat="server" ID="Button1" Text="ตรวจสอบผู้เข้าร่วมโครงการ" />
                                    </div>
                                </div>
                               
                            </div><!-- CreateParticipantForm -->    
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">

                            <asp:CustomValidator ID="CustomValidatorParticipant" runat="server" CssClass="error-text"
                                OnServerValidate="CustomValidatorParticipant_ServerValidate" ClientValidationFunction="validateParticipant"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_Participants) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_Participants) %>"
                                ValidationGroup="SaveProjectReport" SetFocusOnError="true" />
                            
                        </div>
                    </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label control-label-left without-delimit">
                                     แนบไฟล์
                                </label>
                                <div class="col-sm-8">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadParticipantAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div> 

  <div class="panel-group">
    <div class="panel panel-default">
      <div class="panel-heading" style="padding-top: 10px;padding-bottom: 10px;">
        <h4 class="panel-title">
          <a data-toggle="collapse" href="#collapse1">บันทึกรายละเอียดค่าใช้จ่าย</a>
        </h4>
      </div>
      <div id="collapse1" class="panel-collapse">
<div id="divActivityBudget">
    <div style="font-size:14px">
    <div class="form-group form-group-sm">
        <div class="col-sm-3">
            งบประมาณที่เสนอขอ
        </div>
     
         <div class="col-sm-3 control-value  ">
                            <asp:Label ID="LabelBudgetAmount" runat="server" /> 
         </div>
            <%--<vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="GrandTotal.Revise1Amount" ></vue-numeric>--%>
       

    </div>
    <div class="form-group form-group-sm">
        <div class="col-sm-3">
            จำนวนเงินที่ได้รับอนุมัติ
        </div>
        <div class="col-sm-3">
             <asp:Label ID="LabelReviseBudgetAmount" runat="server" CssClass="revise-budget-amount" />
            <%--<vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="GrandTotal.Revise1Amount" ></vue-numeric>--%>
        </div>
        <div class="col-sm-3">
            จำนวนเงินที่ใช้จ่ายจริง
        </div>
        <div class="col-sm-3">
            <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="GrandTotal.ActualExpense" ></vue-numeric>
        </div>
    </div>
    <div class="form-group form-group-sm" >
        <div class="col-sm-3">
            จำนวนเงินคืน
        </div>
        <div class="col-sm-3">
            <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="Balance" ></vue-numeric>
        </div>
        <div class="col-sm-3">
            จำนวนดอกเบื้ย
        </div>
        <div class="col-sm-3">
            <vue-numeric  onblur="appVueAB.sumData()" separator="," v-bind:precision="2" :empty-value="0" v-model="data.Data.Interest" ></vue-numeric>
        </div>
    </div>
        <div class="form-group form-group-sm">
        <div class="col-sm-6" style="text-align:right">
                <b>รวมจำนวนเงินคงเหลือที่ต้องส่งคืนกองทุนฯ</b>
        </div>
        <div class="col-sm-6" style="text-align:left">
           <b><vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="TotalBalance" ></vue-numeric></b>
        </div>
 
    </div>
    </div>
       <table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" id="ApprovalControl_GridViewActivity" style="border-collapse:collapse;">
							<tbody>
                                <tr>
								<th scope="col" style="width:60px;">กิจกรรมที่</th><th scope="col">รายละเอียด</th>
							    </tr>
                           <template v-for="(act,actidx) in data.Data.BudgetActivities">
                                <tr>
								<td>
                                                    <span >{{act.RunNo}}</span>
                                                </td>
                                    <td>
                                                    <span >{{act.ActivityName}}:{{act.ActivityDESC}}</span>
                                                    
                               
									<table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" :id="act.ActivityID" style="border-collapse:collapse;">
										<tbody>
                                            <tr>

                                                <th rowspan="2" style="width:30px"> 
                                                        ลำดับ      
                                                                                                                                
                                                    </th>
                                                    <th rowspan="2" style="width:300px"> 
                                                        ค่าใช้จ่ายที่เสนอขอ   
                                                                                                                                    
                                                    </th>
                                                    <th colspan="3"> 
                                                       จำนวนเงิน    
                                                                                                                                      
                                                    </th>
                                                    <th rowspan="2" style="width:100px">งบประมาณที่ใช้จ่ายจริง</th>                                                                                                      

                                                    </tr>
                                            <tr>
                                                      
                                                        <th>
                                                            เสนอขอ
                                                            
                                                        </th>
                                                        <th>ฝ่ายเลขานุการ</th>
                                                        
                                                            <th>คณะกรรมการจังหวัด</th>
                                                                                                               
                                                                                                        
                                                    </tr>
                              <template v-for="(bud,budidx) in  data.Data.BudgetDetails" v-if="bud.ActivityID == act.ActivityID">                 
										<tr>
										<td>
                                                        {{bud.No}}
                                                    </td>
                                                    <td>
                                                      {{bud.Detail}} 
                                                    </td>
                                                    <td>
                                                        <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Amount" ></vue-numeric>   
                                                    </td>
                                                    <td>
                                                       <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Revise2Amount" ></vue-numeric>   
                                                    </td>
                                                    
                                                        <td>
                                                         <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Revise1Amount" ></vue-numeric>    
                                                        </td>

                                                    <td>
                                                        <vue-numeric onblur="appVueAB.sumData()"  separator="," v-bind:precision="2" :empty-value="0" v-model="data.Data.BudgetDetails[budidx].ActualExpense" ></vue-numeric>
                                                    </td>
                                               
										</tr>

                                        </template> 
                                       <tr>
										<td colspan="2">
                                                      ยอดรวม
                                                    </td>
                                                    <td>
                                                        <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="Summary[actidx].Amount" ></vue-numeric>   
                                                    </td>
                                                    <td>
                                                       <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="Summary[actidx].Revise2Amount" ></vue-numeric>   
                                                    </td>
                                                    
                                                        <td>
                                                         <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="Summary[actidx].Revise1Amount" ></vue-numeric>    
                                                        </td>

                                                    <td>
                                                        <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="Summary[actidx].ActualExpense" ></vue-numeric>
                                                    </td>
                                               
										</tr>
                                       <tr>
										<td colspan="2">
                                                      ค่าใช้จ่ายคงเหลือจากการดำเนินโครงการฯ
                                                    </td>
                                                    <td colspan="4" style="text-align: center">
                                                        <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" :value="Summary[actidx].Revise1Amount - Summary[actidx].ActualExpense" ></vue-numeric>
                                                    </td>
                                               
										</tr>
									</tbody></table>
								
                                                </td>
							</tr>

                            </template>    
						</tbody></table>

       <br />
       <div style="text-align:center">
           <%--<svg id="barcode"></svg>--%>
                               <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-default btn-sm"  Visible="true"                      
                        Text="พิมพ์ใบชำระเงิน" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportPaymentSlip?projectID={0}", ProjectID ) %>'></asp:HyperLink>
       </div>   
   </div>
       
      </div>
    </div>
  </div>


   
                                      
                    <!--งบประมาณที่เสนอ-->
<%--                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title form-group-title-line"><%: UI.TitleProjectReportBudget %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">
                            <%: Model.ProjectReportResult_BudgetAmount %>
                        </label>
                        <div class="col-sm-2 control-value text-right">
                            <asp:Label ID="LabelBudgetAmount" runat="server" /> 
                        </div>
                        <div class="col-sm-2 control-value"><%: UI.LabelBath %></div>
                    </div><!--งบประมาณที่เสนอขอ-->--%>

          <%--          <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">
                            <%= Model.ProjectReportResult_ReviseBudgetAmount %>
                        </label>
                        <div class="col-sm-2 control-value text-right">
                            <asp:Label ID="LabelReviseBudgetAmount" runat="server" CssClass="revise-budget-amount" />
                        </div>
                        <div class="col-sm-2 control-value"><%: UI.LabelBath %></div>
                    </div><!--งบประมาณที่ได้รับอนุมัติ-->--%>

<%--                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">
                            <%= Model.ProjectReportResult_ActualExpense %><span class="required"></span>
                        </label>
                        <div class="col-sm-2 control-value">
                            <nep:TextBox ID="TextBoxActualExpense" runat="server" NumberFormat="N2" TextMode="Number" CssClass="form-control text-right textbox-actual-expense"
                                Min="1" Max="9999999.99" OnClientTextChanged="onActualExpenseChage(this)"></nep:TextBox> 

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorActualExpense" ControlToValidate="TextBoxActualExpense" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActualExpense) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActualExpense) %>"
                                /> 
                        </div>
                        <div class="col-sm-2 control-value"><%: UI.LabelBath %></div>
                    </div><!--งบประมาณที่ใช้จ่ายจริง-->--%>

          <%--          <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">
                            <%= Model.ProjectReportResult_BalanceAmount %>
                        </label>
                        <div class="col-sm-2 control-value text-right">
                            <nep:TextBox ID="TextBoxBalanceAmount" runat="server" CssClass="form-control text-right project-report-balance-amount" Text="0.00" Enabled="false"/>                           
                        </div>
                        <div class="col-sm-2 control-value"><%: UI.LabelBath %></div>
                    </div><!--เหลืองบประมาณ-->--%>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">
                            <%= Model.ProjectReportResult_ReportAttachment %>
                        </label>
                        <div class="col-sm-5 control-value text-right">
                            <%--<nep:C2XFileUpload runat="server" ID="FileUploadReportAttachment" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>"/>--%>
                            <nep:C2XFileUpload runat="server" ID="FileUploadReportAttachment"  MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>"/>
                            <div style="clear:both; text-align:left">
                                <asp:CustomValidator ID="CustomValidatorReportAttachment" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomValidatorReportAttachment_ServerValidate" SetFocusOnError="true" ControlToValidate="FileUploadReportAttachment"
                                    Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ReportAttachment) %>" 
                                    ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ReportAttachment) %>"
                                    ValidationGroup="SaveProjectReport"/>
                            </div>
                             
                        </div>
                        <div class="col-sm-5 control-value" style="font-size:10pt">
                            <span class="field-desc"><%: UI.LabelFieldRemark %></span>
                            <%= Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectReportAttachmentDesc) %>                          
                        </div>
                    </div><!--แนบไฟล์-->
                    
                    
                    <!--ผลการดำเนินงาน/ประโยชน์ที่ได้รับจากการดำเนินงาน-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectReportResult_Benefit %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <div class="required-block">
                                <nep:TextBox ID="TextBoxBenefit" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBenefit" ControlToValidate="TextBoxBenefit" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_Benefit) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_Benefit) %>"
                                />  
                        </div>
                    </div>
                      <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label control-label-left without-delimit">
                                     แนบไฟล์
                                </label>
                                <div class="col-sm-10">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadResultAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div>
                    <!--ปัญหาอุปสรรค์และวิธีการแก้ปัญหาจากการดำเนินการ-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectReportResult_ProblemsAndObstacle %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">                            
                          <nep:TextBox ID="TextBoxProblemsAndObstacle" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                        </div>
                    </div>


                    <!--ข้อคิดเห็นและข้อเสนอแนะ-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectReportResult_Suggestion %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <nep:TextBox ID="TextBoxSuggestion" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                        </div>
                    </div>
                    
                </div><!--form-horizontal-->
            </div>
         </div><!--panel แบบรายงานผลการปฏิบัติงาน-->
       
        <!--สรุปผลการดำเนินงาน-->
        <div class="panel panel-default" runat="server" id="PanelSummaryReportResult">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProjectReportResult_OperationResult %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-5">                            
                            <div class="required-block">
                                <asp:RadioButtonList ID="RadioButtonListOperationResult" runat="server" 
                                    CssClass="form-control-radio-horizontal" DataTextField="LovName" DataValueField="LovID">                                    
                                </asp:RadioButtonList>
                                <span class="required"></span>
                            </div>  
                            <asp:CustomValidator ID="CustomValidatorOperationResult" ClientValidationFunction="validateOperationResult" 
                                ValidateEmptyText="true"
                                CssClass="error-text" runat="server" OnServerValidate="CustomValidatorOperationResult_ServerValidate"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_OperationResult) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_OperationResult) %>"
                                ValidationGroup="SaveProjectReport" SetFocusOnError="true" ControlToValidate="RadioButtonListOperationResult"/>                          
                        </div>
                        <div class="col-sm-7">
                            <span class="field-desc"><%: UI.LabelFieldRemark %></span>
                            <%= Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.ProjectReportOperationResultDesc) %>
                        </div>
                    </div>
                </div>
            </div>
        </div><!--สรุปผลการดำเนินงาน-->
    
        <!--เปรียบเทียบกับวัตถุประสงค์-->
        <div class="panel panel-default" runat="server" id="PanelOperationLevel"> 
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProjectReportResult_OperationLevel %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">
                            <div class="radio-button-group required-block" id="RadioButtonOperationLevelContainer" style="width:540px">
                                <div>
                                    <asp:RadioButton ID="RadioButtonOperationLevel_1" runat="server" 
                                        GroupName="RadioButtonOperationLevel" CssClass="radio-button-operation-level"/>
                                    <nep:TextBox ID="TextBoxOperationLevelDesc" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                    <asp:CustomValidator ID="CustomValidatorOperationLevelDesc" runat="server" CssClass="error-text"
                                        OnServerValidate="CustomValidatorOperationLevelDesc_ServerValidate"
                                        ValidationGroup="SaveProjectReport" SetFocusOnError="true" ControlToValidate="TextBoxOperationLevelDesc"/>
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOperationLevel_2" runat="server" 
                                        GroupName="RadioButtonOperationLevel" CssClass="radio-button-operation-level"/>
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOperationLevel_3" runat="server" 
                                        GroupName="RadioButtonOperationLevel" CssClass="radio-button-operation-level"/>
                                </div>
                                <div>
                                    <asp:RadioButton ID="RadioButtonOperationLevel_4" runat="server" 
                                        GroupName="RadioButtonOperationLevel" CssClass="radio-button-operation-level"/>
                                </div>
                                <span class="required"></span>
                            </div>
                            <div>
                                <asp:CustomValidator ID="CustomValidatorOperationLevel" ClientValidationFunction="validateOperationLevel" 
                                    ValidateEmptyText="true"
                                    CssClass="error-text" runat="server" OnServerValidate="CustomValidatorOperationLevel_ServerValidate"
                                    Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_OperationLevel) %>" 
                                    ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_OperationLevel) %>"
                                    ValidationGroup="SaveProjectReport" /> 
                            </div>                                     
                        </div>

                        <!--ลงชื่อผู้รายงาน-->
                        <div class="col-sm-4">  
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm">
                                     <div class="col-sm-12 control-label control-label-left without-delimit text-nowrap">
                                        (ลงชื่อ).....................................................ผู้รายงาน
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-6 text-right">
                                        <span class="form-control-left-desc" style="left:3px;">(</span>
                                        <nep:TextBox ID="TextBoxReporter1FirstName" runat="server" CssClass="form-control" MaxLength="50"  Width="93%" />
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <nep:TextBox ID="TextBoxReporter1LastName" runat="server" CssClass="form-control" Width="93%" MaxLength="50"></nep:TextBox>
                                        <span class="form-control-right-desc" style="right:6px; position:absolute;top: 4px;">)</span>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                     <div class="col-sm-12 control-label control-label-left without-delimit text-nowrap">
                                        ตำแหน่ง.....................................................
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-12 text-right">
                                        <span class="form-control-left-desc" style="left:3px;">(</span>
                                        <nep:TextBox ID="TextBoxReporter1Position" runat="server" CssClass="form-control" MaxLength="100" Width="93%"></nep:TextBox>
                                        <span class="form-control-right-desc" style="right:6px; position:absolute;top: 4px;">)</span>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-2 control-label control-label-left without-delimit">
                                        <%: Model.ProjectReportResult_Date %>
                                    </div>
                                    <div class="col-sm-10">                                        
                                        <nep:DatePicker ID="DatePickerReporter1" runat="server" ClearTime="true" EnabledTextBox="true"  Width="94%"/>                                    
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-2 control-label control-label-left without-delimit">
                                        <%: Model.ProjectReportResult_Telephone %>
                                    </div>
                                    <div class="col-sm-10">                                        
                                        <nep:TextBox ID="TextBoxReporter1Telephone" runat="server" CssClass="form-control" Width="94%" MaxLength="30"></nep:TextBox>
                                    </div>
                                </div>  
                            </div>   <!-- form-horizontal -->                   
                        </div><!--ลงชื่อผู้รายงาน-->
                    </div>
                </div>
            </div>
        </div><!--เปรียบเทียบกับวัตถุประสงค์-->

        <!--ข้อคิดเห็นและข้อเสนอแนะ-->
        <div class="panel panel-default" runat="server" id="PanelSuggestionDesc" visible="false">
            <div class="panel-heading">
                <h3 class="panel-title"><%: Model.ProjectReportResult_SuggestionDesc %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">
                            <div>
                                <%: UI.ProjectReportOfficerSuggestionDesc %>                                
                            </div>
                            <nep:TextBox ID="TextBoxSuggestionDesc" runat="server" TextMode="MultiLine" CssClass="form-control  textarea-height"></nep:TextBox>
                        </div>

                        <!--ลงชื่อเจ้าหน้าที่-->
                        <div class="col-sm-4">                            
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm">
                                     <div class="col-sm-12 control-label control-label-left without-delimit text-nowrap">
                                        (ลงชื่อ).....................................................
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-6 text-right">
                                        <span class="form-control-left-desc" style="left:3px;">(</span>
                                        <nep:TextBox ID="TextBoxReporter2FirstName" runat="server" CssClass="form-control"  Width="93%" MaxLength="50"></nep:TextBox>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <nep:TextBox ID="TextBoxReporter2LastName" runat="server" CssClass="form-control" Width="93%" MaxLength="50"></nep:TextBox>
                                        <span class="form-control-right-desc" style="right:6px; position:absolute;top: 4px;">)</span>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                     <div class="col-sm-12 control-label control-label-left without-delimit text-nowrap">
                                        ตำแหน่ง.....................................................
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-12 text-right">
                                        <span class="form-control-left-desc" style="left:3px;">(</span>
                                        <nep:TextBox ID="TextBoxReporter2Position" runat="server" CssClass="form-control" Width="93%" MaxLength="100"></nep:TextBox>
                                        <span class="form-control-right-desc" style="right:6px; position:absolute;top: 4px;">)</span>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-2 control-label control-label-left without-delimit">
                                        <%: Model.ProjectReportResult_Date %>
                                    </div>
                                    <div class="col-sm-10">                                        
                                        <nep:DatePicker ID="DatePickerReporter2" runat="server" ClearTime="true" EnabledTextBox="true"  Width="94%"/>                                    
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <div class="col-sm-2 control-label control-label-left without-delimit">
                                        <%: Model.ProjectReportResult_Telephone %>
                                    </div>
                                    <div class="col-sm-10">                                        
                                        <nep:TextBox ID="TextBoxReporter2Telephone" runat="server" CssClass="form-control"  Width="94%" MaxLength="30"></nep:TextBox>
                                    </div>
                                </div>  
                            </div>   <!-- form-horizontal -->                            
                        </div> <!--ลงชื่อเจ้าหน้าที่-->
                    </div>
                </div>
            </div>
        </div><!--ข้อคิดเห็นและข้อเสนอแนะ-->
          <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Label runat="server" ID="LabelNeedQN" ForeColor="Red" Visible="false">กรุณากรอกข้อมูล การดำเนินการ แบบประเมินความพึงพอใจ และ แบบประเมินตนเอง ให้ครบก่อนทำการส่งผลการปฏิบัติงาน</asp:Label>
                </div>
            </div>
          </div>

        <asp:HiddenField runat="server" ID="hdfActOBJ" />
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                      OnClientClick="GetActOBJJSON() ;"   Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSaveReportResult_Click" Visible="false"/>

                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" ValidationGroup="SaveProjectReport"
                        OnClientClick="GetActOBJJSON() ; return confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?');"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectReport%>"
                        OnClick="ButtonSaveAndSendProjectReport_Click" />

                     <asp:Button runat="server" ID="ButtonRevise" CssClass="btn btn-primary btn-sm" Visible="true" 
                        Text="ส่งแก้ไขข้อมูลผลการปฏิบัติงาน" OnClientClick="c2x.clearResultMsg(); return openReviseReportForm();" 
                        />

                     <asp:Button runat="server" ID="ButtonOfficerSave" CssClass="btn btn-primary btn-sm" ValidationGroup="OfficerSaveReportResult"
                       OnClientClick="GetActOBJJSON();"  Text="<%$ code:Nep.Project.Resources.UI.ButtonOfficerSaveSuggestion %>" OnClick="ButtonOfficerSave_Click" Visible="false" />

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectResult?projectID={0}", ProjectID ) %>'></asp:HyperLink>                

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>
     <%--           <asp:UpdatePanel ID="panelTest" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn" EventName="Click" />
            </Triggers>
            <ContentTemplate>        
                </ContentTemplate>
                </asp:UpdatePanel>
        <asp:Button OnClick="btn_Click" runat="server" ID="btn" Text="test" />--%>
 
    </ContentTemplate>
</asp:UpdatePanel>
