<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollow5MControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollow5MControl"  %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelReportResult" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <style type="text/css">
            #RadioButtonListComparePurpose > tbody > tr:first-child > td > input[type="radio"]:first-child {
                position:absolute;
            }

            #RadioButtonListComparePurpose > tbody > tr:first-child > td .control-label-radio.radio-block {
                margin-left:18px;
            }

            div.combobox-block{
                position:relative !important;
            }

            .ajax__combobox_itemlist{
                top:27px  !important;
                left:7px !important;
            }

            

             .btn-hide {
                display:none;
            }

            .button-add-participant, .button-clear-participant {
                margin-top:6px;
                margin-right:5px;
                opacity:.6;

            }

             .button-add-participant[disabled="disabled"]:hover, .button-clear-participant[disabled="disabled"]:hover {
                opacity:.6;
            }
            
            .button-add-participant:hover, .button-clear-participant:hover {
                opacity:1;
            }
            /*.k-grid-with-pager .k-grid-pager {
                 width:965px;
            }

            .k-grid-with-pager .k-grid-pager.k-grid-pager-width {
                 width:900px !important;
            }*/

            .maskedtextbox{
                width:99%;
            }

            .k-grid .k-dropdown-wrap{
               
                padding-right:18px;
            }

        </style>
        <asp:HiddenField runat="server" ID="hdfQViewModel" />
        <asp:HiddenField runat="server" ID="hdfQContols" />
        <asp:HiddenField runat="server" ID="hdfIsDisable" />
        <div id="divVueQN" class="panel panel-default">
            <div class="panel-heading" style="text-align:center">
                <h3 class="panel-title">แบบประเมินผลการติดตามประเมินผลโครงการ (สำหรับเจ้าหน้าที่กองทุน)<br />
การประเมินผลโครงการที่ได้รับการสนับสนุนจากกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ<br />
(งบประมาณมากกว่า 5 ล้านบาท)
</h3>
            </div>
            <div class="container" style="font-size:14px">
            <div class="row" style="padding-top:5px">
                <div class="col-sm-3">
                    ชื่อโครงการ
                </div>
                 <div class="col-sm-4">
                      {{extend.projectName}}  
                 </div>
            </div>
            <div class="row" style="padding-top:5px">
                <div class="col-sm-3">
                    หน่วยงานที่รับผิดชอบ
                </div>
                 <div class="col-sm-4">
                      {{extend.organization}}  
                 </div>
            </div>
            <div class="row" style="padding-top:5px">
                <div class="col-sm-3">
                    ระยะเวลาดำเนินการ
                </div>
                 <div class="col-sm-4">
                     <vue-numeric    separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.numTotalDays].v" ></vue-numeric>
                 </div>
            </div>
            <div class="row" style="padding-top:5px">
                <div class="col-sm-3">
                    วันที่เริ่มดำเนินการทำสัญญา
                </div>
                 <div class="col-sm-4">
                      {{extend.startDate}}  
                 </div>
                  <div class="col-sm-2">
                    วันสิ้นสุดสัญญา 
                </div>
                 <div class="col-sm-3">
                      {{extend.contractEndDate}}  
                 </div>
            </div>
            </div>
             <br />
                  <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                        <tr role="row">
                                <th scope="col" role="columnheader" class="k-header">เกณฑ์</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:80px">1 <br />คะแนนดิบ<br />(เต็ม) </th>
                                <th scope="col" role="columnheader" class="k-header" style="width:80px">2 <br />คะแนนดิบ<br />(ที่ได้) </th>    
                                <th scope="col" role="columnheader" class="k-header" style="width:80px">หารด้วย</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:80px" >4<br />คะแนนที่นำมาประมวล</th>

                        </tr>

                   </thead>
                                <tr>
                                    <td>
                                        1. ความสมบูรณ์ของข้อเสนอ โครงการ (25 คะแนน)
                                    </td>
                                    <td style="text-align:center">
                                        125
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="125"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num1_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        5    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num1_1_2].v" >   
                                    </td>
                                        
                                </tr>
                      <tr>
                          <td>2. ผลการด าเนินโครงการ (30 คะแนน)</td>
                          <td colspan="4"></td>
                      </tr>
                                <tr>
                                    <td>
                                        <span> </span>2.1 การบริหารเวลา
                                    </td>
                                    <td style="text-align:center">
                                        30
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="30"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num2_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        3    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num2_1_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        <span> </span>2.2 ความครบถ้วนของกิจกรรม
                                    </td>
                                    <td style="text-align:center">
                                        30
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="30"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num2_2_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        3    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num2_2_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        <span> </span>2.3 ความครบถ้วนของกลุ่มเป้าหมาย
                                    </td>
                                    <td style="text-align:center">
                                        30
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()"  v-bind:min="0" v-bind:max="30"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num2_3_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        3    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num2_3_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        3. ประสิทธิผลในการดำเนินโครงการ  (20 คะแนน)
                                    </td>
                                    <td style="text-align:center">
                                        20
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="20"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num3_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        1    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num3_1_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        4. ผลกระทบของโครงการ (10 คะแนน)
                                    </td>
                                    <td style="text-align:center">
                                        100
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="100"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num4_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        10    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num4_1_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        5. ความยั่งยืนของโครงการ (10 คะแนน)
                                    </td>
                                    <td style="text-align:center">
                                        100
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="100"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num5_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        10    
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num5_1_2].v" >   
                                    </td>
                                        
                                </tr>
                                <tr>
                                    <td>
                                        6. การถ่ายทอดส่งต่อ
                                    </td>
                                    <td style="text-align:center">
                                        10
                                    </td>
   
                                    <td>
                                       <vue-numeric onblur="CalulateFollowUp5M()" v-bind:min="0" v-bind:max="10"  separator="," v-bind:precision="0" :empty-value="0" v-model="items[field.num6_1_1].v" >
                                    </td>
                                  <td style="text-align:center">
                                        2   
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.num6_1_2].v" >   
                                    </td>
                                        
                                </tr>
                                 <tr>
                                    <td  style="text-align:center">
                                       รวม
                                    </td>
                                    <td style="text-align:center">
                                        445
                                    </td>
   
                                    <td>
                                      <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.sumGrid1].v" >  
                                    </td>
                                  <td style="text-align:center">
                                        100  
                                    </td>
                                    <td style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.sumGrid2].v" >   
                                    </td>
                                        
                                </tr>
                                 <tr>
                                    <td  style="text-align:center">
                                       เกรดที่ได้รับ
                                    </td>
  
                                    <td colspan="5" style="text-align:center">
                                        <vue-numeric :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="items[field.sumAll].v" > คะแนน
                                    </td>
                                        
                                </tr>
                                 <tr>
                                    <td  style="text-align:center">
                                       โครงการอยู่ในระดับ/ความหมาย
                                    </td>
  
                                    <td colspan="5" style="text-align:center">
                                        {{items[field.gradeText].v}}
                                    </td>
                                        
                                </tr>
                      </table>       
            <br />  
            <div class="container" style="font-size:14px;padding-top">
              <div class="row">

                <div class="col-sm-1">
                    ลงชื่อ  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbSignName1].v" />
               </div>
                </div>  
                <div class="row" style="padding-top: 5px">

                <div class="col-sm-1">
                    ตำแหน่ง  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbPosition1].v" />
               </div>
                  
 
              </div>
                <div class="row" style="padding-top: 5px">
                  <div class="col-sm-4" style="text-align:center">
                    เจ้าหน้าที่ประเมินผล
                   </div>
                </div>
            </div>
         </div><!--panel แบบรายงานผลการปฏิบัติงาน-->
       
        <br />
             <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label control-label-left without-delimit">
                        แนบไฟล์
                </label>
                <div class="col-sm-8">    
                    <nep:C2XFileUpload  runat="server" ID="FileUploadFollowAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                </div>                      
            </div>          
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="appVueQN.param.IsReported = '0';appVueQN.saveData();SaveAttachmentFiles(); return false;"  Visible="false"/>

                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectReport%>"
                        OnClientClick="if (confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?')) {appVueQN.param.IsReported = '1';appVueQN.saveData();SaveAttachmentFiles(); return false; } else return false;"   />
                        <%--OnClick="ButtonSaveAndSendProjectReport_Click" />--%>

              <%--       <asp:Button runat="server" ID="ButtonOfficerSave" CssClass="btn btn-primary btn-sm" ValidationGroup="OfficerSaveReportResult"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonOfficerSaveSuggestion %>" OnClick="ButtonOfficerSave_Click" Visible="false" />--%>

              <%--      <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectResult?projectID={0}", ProjectID ) %>'></asp:HyperLink>      --%>          
                      <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary btn-sm"  
                        Text="พิมพ์" OnClientClick="printDiv('divVueQN'); return false;"   />
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>

