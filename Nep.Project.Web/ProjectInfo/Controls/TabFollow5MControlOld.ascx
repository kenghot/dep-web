<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollow5MControlOld.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollow5MControlOld"  %>
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
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">การติดตามประเมินผลโครงการ (งบประมาณมากกว่า 5 ล้านบาท)</h3>
            </div>
                  <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label control-label-left without-delimit">
                        แนบไฟล์
                </label>
                <div class="col-sm-8">    
                    <nep:C2XFileUpload  runat="server" ID="FileUploadFollowAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                </div>                      
            </div>  
            <div class="panel-body">
                <div class="form-horizontal">
                   
             <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                     <%--       <colgroup>
                                <col style="width:50px">
                                <col style="width:135px"><col style="width:135px">
                                <col style="width:150px"><col style="width:90px">
                                <col style="width:90px"><col style="width:250px">

                            </colgroup>--%>
                                                     <thead class="k-grid-header" role="rowgroup">
                        <tr role="row">
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" >รายการประเมิน</th>
                                <th colspan="5" scope="col" role="columnheader" class="k-header" >ระดับความคิดเห็น</th>
                                <%--<th scope="col" role="columnheader" class="k-header" style="width:150px">ไม่ตามแผน</th>--%>
                        </tr>
                        <tr role="row">
                                 <%--<th scope="col" role="columnheader" data-field="No" rowspan="1" data-title="ลำดับ" data-index="0" id="efdc983f-8432-4625-9261-e515c41db2c6" class="k-header">ลำดับ</th>--%>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">มากที่สุด</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">มาก</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">ปานกลาง</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">น้อย</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">น้อยที่สุด</th>
                               </tr>

                         </thead>
                         <tr>
                             <td colspan="6" style="background-color:lightslategray;font-weight:bold">ส่วนที่ 1 ประเมินก่อนการดำเนินโครงการ</td>
                         </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">1. การจัดทำข้อตกลงระหว่างผู้ประเมินกับผู้มีส่วนได้ส่วนเสีย (Contractual Agreement) เกี่ยวกับระเบียบวิธี งบประมาณ การวางแผนกิจกรรมร่วมกัน</td>
                         </tr>
                                <tr  role="row">
                                    <td role="gridcell">
                                        1.1  ข้อตกลงโครงการได้จัดทำถูกต้องตามหลักเกณฑ์ของกองทุน
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_1" value="1" data-bind="checked: R_1_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_1" value="2" data-bind="checked: R_1_1_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_1" value="3" data-bind="checked: R_1_1_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_1" value="4" data-bind="checked: R_1_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_1" value="5" data-bind="checked: R_1_1_1"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.2 ข้อตกลงของโครงการได้รับการพิจารณาตามหลักเกณฑ์ ขั้นตอน และ ระยะเวลาที่กำหนดไว้     
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_2" value="1" data-bind="checked: R_1_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_2" value="2" data-bind="checked: R_1_1_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_2" value="3" data-bind="checked: R_1_1_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_2" value="4" data-bind="checked: R_1_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_2" value="5" data-bind="checked: R_1_1_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.3 ข้อตกลงของโครงการมีข้อมูล รายละเอียด ครบถ้วนตามหลักเกณฑ์ที่กองทุนกำหนด  

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_3" value="1" data-bind="checked: R_1_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_3" value="2" data-bind="checked: R_1_1_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_3" value="3" data-bind="checked: R_1_1_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_3" value="4" data-bind="checked: R_1_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_3" value="5" data-bind="checked: R_1_1_3"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.4 ข้อตกลงของโครงการเกิดขึ้นบนหลักการพิจารณาอย่างมีเหตุผล รอบคอบระมัดระวัง  

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_4" value="1" data-bind="checked: R_1_1_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_4" value="2" data-bind="checked: R_1_1_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_4" value="3" data-bind="checked: R_1_1_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_4" value="4" data-bind="checked: R_1_1_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_4" value="5" data-bind="checked: R_1_1_4"  />
                                    </td> 
                                </tr> 
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.5 ข้อตกลงของโครงการกำหนดกลุ่มเป้าหมายอย่างเหมาะสม เป็นไปได้จริงทั้งมิติ ประมาณ และ คุณภาพ เมื่อวิเคราะห์ร่วมกับงบประมาณ และ ระยะเวลา  

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_5" value="1" data-bind="checked: R_1_1_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_5" value="2" data-bind="checked: R_1_1_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_5" value="3" data-bind="checked: R_1_1_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_5" value="4" data-bind="checked: R_1_1_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_1_5" value="5" data-bind="checked: R_1_1_5"  />
                                    </td> 
                                </tr> 
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">2. โครงการได้แสดงหลักการและเหตุผลด้วยข้อมุลที่ผ่านการประเมินสภาพแวดล้อม (ด้านการต่างประเทศ เศรษฐกิจ สังคม เทคโนโลยีสารสนเทศ การเมือง นโยบาย กฏหมาย และ ระเบียบต่างๆ) และพบว่าเหมาะสมและจำเป็นที่จะต้องดำเนินการ</td>
                         </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.1 โครงการได้แสดงหลักการและเหตุผลด้วยข้อมูลที่ผ่านการเปิมนสภาพแวดล้อม (ด้านการต่างประเทศ เศรษฐิจ สังคม เทคโนโลยีสารสนเทศ การเมือง นโยบาย กฏหมาย และ ระเบียบต่างๆ) และ พบว่าเหมาะสม และ จำเป็นที่จะต้องดำเนินการ
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_1" value="1" data-bind="checked: R_1_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_1" value="2" data-bind="checked: R_1_2_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_1" value="3" data-bind="checked: R_1_2_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_1" value="4" data-bind="checked: R_1_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_1" value="5" data-bind="checked: R_1_2_1"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.2 โครงการกำหนดประเด็นปัญหาหรือประเด็นพัฒนาตามความจำเป็นสำคัญที่จำเป็นต้องดำเนินการโดยมิอาจหลีกเลี่ยง

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_2" value="1" data-bind="checked: R_1_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_2" value="2" data-bind="checked: R_1_2_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_2" value="3" data-bind="checked: R_1_2_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_2" value="4" data-bind="checked: R_1_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_2" value="5" data-bind="checked: R_1_2_2"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.3 โครงการได้แสดงหลักการและเหตุผลที่วิเคราะห์ตามหลักวิชาการ (อ้างอิงตัวเลข สถิติ นโยบาย กฎหมาย)

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_3" value="1" data-bind="checked: R_1_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_3" value="2" data-bind="checked: R_1_2_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_3" value="3" data-bind="checked: R_1_2_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_3" value="4" data-bind="checked: R_1_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_3" value="5" data-bind="checked: R_1_2_3"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.4 โครงการการกำหนดวัตถุประสงค์ไว้เพื่อการเตรียมความหร้อม การป้องกัน และ การบรรเทาปัญหาที่จะเกิดขึ้นหรืออาจจะเกิดขึ้นในอนาคต (หลักการมีภูมิคุ้มกันที่ดี)

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_4" value="1" data-bind="checked: R_1_2_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_4" value="2" data-bind="checked: R_1_2_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_4" value="3" data-bind="checked: R_1_2_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_4" value="4" data-bind="checked: R_1_2_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_4" value="5" data-bind="checked: R_1_2_4"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.5 โครงการการมีวัตุประสงค์ที่สอดคล้องกับนโยบาย วัตถุประสงค์ของกองทุน 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_5" value="1" data-bind="checked: R_1_2_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_5" value="2" data-bind="checked: R_1_2_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_5" value="3" data-bind="checked: R_1_2_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_5" value="4" data-bind="checked: R_1_2_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_5" value="5" data-bind="checked: R_1_2_5"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.6 โครงการกำหนดวัตถุประสงค์ กลุ่มเป้าหมาย และ ผลผลติที่ชัดเจน และ วัดได้ (Measurable)  

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_6" value="1" data-bind="checked: R_1_2_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_6" value="2" data-bind="checked: R_1_2_6"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_6" value="3" data-bind="checked: R_1_2_6"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_6" value="4" data-bind="checked: R_1_2_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_6" value="5" data-bind="checked: R_1_2_6"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.7 โครงการกำหนดวัตถุประสงค์ กลุ่มเป้าหมาย และ ผลผลิตที่เป็นไปได้ (Realistic) และ ทำได้ (Achievable) ในกรอบเวลาและงบประมาณ  

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_7" value="1" data-bind="checked: R_1_2_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_7" value="2" data-bind="checked: R_1_2_7"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_7" value="3" data-bind="checked: R_1_2_7"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_7" value="4" data-bind="checked: R_1_2_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_7" value="5" data-bind="checked: R_1_2_7"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.8 โครงการกำหนดวัตถุประสงค์ กลุ่มเป้าหมาย และ ผลผลิตโดยได้รับความเห็นชอบ  

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_8" value="1" data-bind="checked: R_1_2_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_8" value="2" data-bind="checked: R_1_2_8"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_8" value="3" data-bind="checked: R_1_2_8"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_8" value="4" data-bind="checked: R_1_2_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_8" value="5" data-bind="checked: R_1_2_8"  />
                                    </td>   
                                </tr>
                         <tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.9 โครงการกำหนดวัตถุประสงค์ กลุ่มเป้าหมาย และ ผลผลิตโดยได้สอบถามความเห็นจากชุมชน / พื้นที่ที่จะดำเนินโครงการแล้ว 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_9" value="1" data-bind="checked: R_1_2_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_9" value="2" data-bind="checked: R_1_2_9"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_9" value="3" data-bind="checked: R_1_2_9"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_9" value="4" data-bind="checked: R_1_2_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_2_9" value="5" data-bind="checked: R_1_2_9"  />
                                    </td>   
                                </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">3. การประเมินผลปัจจัยนำเข้า (Input Evaluation) เกี่ยวกับการวางแผนกิจกรรมและงบประมาณ</td>
                         </tr>    
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">งบประมาณ</td>
                         </tr>                                               
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       3.1 โครงการขอรับการสนับสนุนงบประมาณอย่างเหมาะสม สมเหตุผล สำหรับดำเนินโครงการได้อย่างมีประสิทธิภาพ
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_1" value="1" data-bind="checked: R_1_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_1" value="2" data-bind="checked: R_1_3_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_1" value="3" data-bind="checked: R_1_3_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_1" value="4" data-bind="checked: R_1_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_1" value="5" data-bind="checked: R_1_3_1"  />
                                    </td>   
                     
 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.2 โครงการขอรับงบประมาณสนับสนุนในหมวด / รายการต่างๆ (ค่าจ้าง ค่าใช้สอย) หลังจากที่ได้ทำการวิเคราะห์ เปรียบเทียบกับราคากลาง / ราคาตลาด / มาตรฐานสากลแล้ว 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_2" value="1" data-bind="checked: R_1_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_2" value="2" data-bind="checked: R_1_3_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_2" value="3" data-bind="checked: R_1_3_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_2" value="4" data-bind="checked: R_1_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_2" value="5" data-bind="checked: R_1_3_2"  />
                                    </td>   
                                </tr>   
                          <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">บุคลากร</td>
                         </tr>                                        
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.3 โครงการมีจำนวนบุคลากรหลัก (ผู้บริหารโครงการและเจ้าหน้าที่ประจำโครงการ) เหมาะสมพอเพียงที่จะทำให้โครงการดำเนินการได้อย่างมีประสิทธิภาพ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_3" value="1" data-bind="checked: R_1_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_3" value="2" data-bind="checked: R_1_3_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_3" value="3" data-bind="checked: R_1_3_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_3" value="4" data-bind="checked: R_1_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_3" value="5" data-bind="checked: R_1_3_3"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.4 โครงการได้แสดงหลักการและเหตุผลด้วยข้อมูลที่ผ่านการเปิมนสภาพแวดล้อม (ด้านการต่างประเทศ เศรษฐิจ สังคม เทคโนโลยีสารสนเทศ การเมือง นโยบาย กฏหมาย และ ระเบียบต่างๆ) และ พบว่าเหมาะสม และ จำเป็นที่จะต้องดำเนินการ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_4" value="1" data-bind="checked: R_1_3_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_4" value="2" data-bind="checked: R_1_3_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_4" value="3" data-bind="checked: R_1_3_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_4" value="4" data-bind="checked: R_1_3_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_4" value="5" data-bind="checked: R_1_3_4"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.5 โครงการมีจำนวนบุคลากรสนับสนุนตามช่วงเวลา (วิทยากร ที่ปรึกษา บุคลากรไม่เต็มเวลา) เหมาะสมพอเพียงที่จะทำให้โครงการดำเนินการได้อย่างมีประสิทธิภาพ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_5" value="1" data-bind="checked: R_1_3_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_5" value="2" data-bind="checked: R_1_3_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_5" value="3" data-bind="checked: R_1_3_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_5" value="4" data-bind="checked: R_1_3_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_5" value="5" data-bind="checked: R_1_3_5"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.6 โครงการมีจำนวนบุคลากรสนับสนุนตามช่วงเวลา (วิทยากร ที่ปรึกษา บุคลากรไม่เต็มเวลา) ที่มีคุณสมบัติ (เพศ อายุ การศึกษา ทัศนคติ ประสบการณ์) เหมาะสม กับภารกิจที่ต้องรับผิดชอบได้อย่างมีประสิทธิภาพ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_6" value="1" data-bind="checked: R_1_3_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_6" value="2" data-bind="checked: R_1_3_6"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_6" value="3" data-bind="checked: R_1_3_6"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_6" value="4" data-bind="checked: R_1_3_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_6" value="5" data-bind="checked: R_1_3_6"  />
                                    </td>    
                                </tr> 
                          <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">วัสดุอุปกรณ์ เทคโนโลยี ระยะเวลา</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.7 โครงการจัดหาวัสดุอุปกรณ์ที่สอดคล้องกับกิจกรรม กลุ่มเป้าหมายและนำไปสู่ผลผลิตที่ต้องการได้
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_7" value="1" data-bind="checked: R_1_3_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_7" value="2" data-bind="checked: R_1_3_7"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_7" value="3" data-bind="checked: R_1_3_7"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_7" value="4" data-bind="checked: R_1_3_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_7" value="5" data-bind="checked: R_1_3_7"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.8 โครงการจัดหาวัสดุอุปกรณ์มีคุณภาพ สภาพการใช้งานเหมาะสมกับกลุ่มเป้าหมาย
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_8" value="1" data-bind="checked: R_1_3_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_8" value="2" data-bind="checked: R_1_3_8"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_8" value="3" data-bind="checked: R_1_3_8"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_8" value="4" data-bind="checked: R_1_3_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_8" value="5" data-bind="checked: R_1_3_8"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.9 โครงการได้นำเทคโนโลยีสารสนเทศ และ นวัตกรรมมาช่วยในการบริหารจัดการเอย่างเหมาะสม เช่น จัดเก็บข้อมูลสำคัญเพื่อการบริหารพัฒนา
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_9" value="1" data-bind="checked: R_1_3_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_9" value="2" data-bind="checked: R_1_3_9"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_9" value="3" data-bind="checked: R_1_3_9"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_9" value="4" data-bind="checked: R_1_3_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_9" value="5" data-bind="checked: R_1_3_9"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.10 โครงการการำหนดระยะเวลาดำเนินงานตั้งแต่ต้นจนจบโครงการได้อย่างเหมาะสม
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_10" value="1" data-bind="checked: R_1_3_10"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_10" value="2" data-bind="checked: R_1_3_10"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_10" value="3" data-bind="checked: R_1_3_10"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_10" value="4" data-bind="checked: R_1_3_10"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_10" value="5" data-bind="checked: R_1_3_10"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.11 โครงการกำหนดระยะเวลาในแต่ละช่วงกิจกรรมได้อย่าเหมาะสม และ สามารถเกิดผลผลิตตามที่ต้องการได้อย่างมีประสิทธิภาพ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_11" value="1" data-bind="checked: R_1_3_11"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_11" value="2" data-bind="checked: R_1_3_11"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_11" value="3" data-bind="checked: R_1_3_11"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_11" value="4" data-bind="checked: R_1_3_11"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_11" value="5" data-bind="checked: R_1_3_11"  />
                                    </td>    
                                </tr> 
                          <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">การบริหารจัดการ</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.12 โครงการมีแผนปฏิบัติงาน และ กิจกรรมอย่างชัดเจนตลอดดครงการ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_12" value="1" data-bind="checked: R_1_3_12"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_12" value="2" data-bind="checked: R_1_3_12"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_12" value="3" data-bind="checked: R_1_3_12"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_12" value="4" data-bind="checked: R_1_3_12"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_12" value="5" data-bind="checked: R_1_3_12"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.13 โครงการวงางแผนความเสี่ยง และ กำหนดแนวทางการแก้ไข / แผนสำรองไว้
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_13" value="1" data-bind="checked: R_1_3_13"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_13" value="2" data-bind="checked: R_1_3_13"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_13" value="3" data-bind="checked: R_1_3_13"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_13" value="4" data-bind="checked: R_1_3_13"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_13" value="5" data-bind="checked: R_1_3_13"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.14 โครงการมีงวดงานและงวดเงินที่สัมพันธ์สอดคล้องกัน
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_14" value="1" data-bind="checked: R_1_3_14"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_14" value="2" data-bind="checked: R_1_3_14"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_14" value="3" data-bind="checked: R_1_3_14"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_14" value="4" data-bind="checked: R_1_3_14"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_14" value="5" data-bind="checked: R_1_3_14"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.15 โครงการได้จัดวางบุคลการหลัก และ บคุลากรสนับสนุนได้สอดคล้องกับกิจกรรม และ กลุ่มเป้าหมาย
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_15" value="1" data-bind="checked: R_1_3_15"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_15" value="2" data-bind="checked: R_1_3_15"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_15" value="3" data-bind="checked: R_1_3_15"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_15" value="4" data-bind="checked: R_1_3_15"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_1_3_15" value="5" data-bind="checked: R_1_3_15"  />
                                    </td>    
                                </tr> 
                           <tr>
                             <td colspan="6" style="background-color:lightslategray;font-weight:bold">ช่วงที่ 2 ประเมินระหว่างการดำเนินโครงการ</td>
                         </tr>                    

                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">4. การประเมินผลกระบวนการ (Process Evaluation) เกี่ยวกับการดำเนินกิจกรรม การบันทึกผลการปฎิบัติงาน และการติดตามผล</td>
                         </tr>


                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.1 โครงการได้เริ่มดำเนินกิจกรรมตามแผนงานในเวลาที่กำหนดไว้
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_1" value="1" data-bind="checked: R_2_4_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_1" value="2" data-bind="checked: R_2_4_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_1" value="3" data-bind="checked: R_2_4_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_1" value="4" data-bind="checked: R_2_4_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_1" value="5" data-bind="checked: R_2_4_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       4.2 โครงการได้ดำเนินกิจกรรมต่างๆ ตามแผนงานที่กำหนดไว้

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_2" value="1" data-bind="checked: R_2_4_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_2" value="2" data-bind="checked: R_2_4_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_2" value="3" data-bind="checked: R_2_4_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_2" value="4" data-bind="checked: R_2_4_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_2" value="5" data-bind="checked: R_2_4_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.3 โครงการดำเนินกิจกรรมต่างๆ อย่างโปร่งใส ตรวจสอบได้ และ เปิดโอกาสให้ผู้เกี่ยวข้องมีส่วนร่วม 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_3" value="1" data-bind="checked: R_2_4_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_3" value="2" data-bind="checked: R_2_4_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_3" value="3" data-bind="checked: R_2_4_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_3" value="4" data-bind="checked: R_2_4_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_3" value="5" data-bind="checked: R_2_4_3"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.4 โครงการได้สรุป หารือ ประเมินผลการดำเนินในแต่ละขั้นตอน / กิจกรรม 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_4" value="1" data-bind="checked: R_2_4_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_4" value="2" data-bind="checked: R_2_4_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_4" value="3" data-bind="checked: R_2_4_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_4" value="4" data-bind="checked: R_2_4_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_4" value="5" data-bind="checked: R_2_4_4"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.5 โครงการมีการบันทึกรายละเอียดของการดำเนินงานในแต่ละขั้นตอนไว้

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_5" value="1" data-bind="checked: R_2_4_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_5" value="2" data-bind="checked: R_2_4_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_5" value="3" data-bind="checked: R_2_4_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_5" value="4" data-bind="checked: R_2_4_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_5" value="5" data-bind="checked: R_2_4_5"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.6 โครงการได้จัดส่งรายงานการดำเนินในขั้นตอนสำคัญให้กองทุนรับทราบอย่างเป็นระบบ (1/3 ของแผนงาน ½ ของแผนงาน และ เมื่อสิ้นสุดโครงการ) และ ต่อเนื่องตลอดโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_6" value="1" data-bind="checked: R_2_4_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_6" value="2" data-bind="checked: R_2_4_6"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_6" value="3" data-bind="checked: R_2_4_6"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_6" value="4" data-bind="checked: R_2_4_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_6" value="5" data-bind="checked: R_2_4_6"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.7 โครงการขอคำแนะนำ ปรึกษากองทุนฯ เมื่อเกิดปัญหาระหว่างการดำเนินโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_7" value="1" data-bind="checked: R_2_4_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_7" value="2" data-bind="checked: R_2_4_7"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_7" value="3" data-bind="checked: R_2_4_7"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_7" value="4" data-bind="checked: R_2_4_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_7" value="5" data-bind="checked: R_2_4_7"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.8 โครงการแจ้งให้กองทุนทราบในกรณีที่ไม่สามารถดำเนินการให้เป็นไปตามแผลงานที่วางไว้ได้ อาทิ เปลี่ยนชื่อดครงการ / สถานที่จัดดครงการ / ระยะเวลาดำเนินการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_8" value="1" data-bind="checked: R_2_4_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_8" value="2" data-bind="checked: R_2_4_8"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_8" value="3" data-bind="checked: R_2_4_8"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_8" value="4" data-bind="checked: R_2_4_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_8" value="5" data-bind="checked: R_2_4_8"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.9 โครงการมีการรายงานผลการใช้จ่ายระหว่างการดำเนินการตลอดโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_9" value="1" data-bind="checked: R_2_4_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_9" value="2" data-bind="checked: R_2_4_9"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_9" value="3" data-bind="checked: R_2_4_9"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_9" value="4" data-bind="checked: R_2_4_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_9" value="5" data-bind="checked: R_2_4_9"  />
                                    </td> 
                                </tr> 
                                 <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.10 โครงการสามารถปรับเปลี่ยนบุคลากรได้อย่างอิสระ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_10" value="1" data-bind="checked: R_2_4_10"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_10" value="2" data-bind="checked: R_2_4_10"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_10" value="3" data-bind="checked: R_2_4_10"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_10" value="4" data-bind="checked: R_2_4_10"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_10" value="5" data-bind="checked: R_2_4_10"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.11 โครงการสามารถปรับเปลี่ยนค่าใช้จ่ายแต่ละรายการได้อย่างอิสระ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_11" value="1" data-bind="checked: R_2_4_11"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_11" value="2" data-bind="checked: R_2_4_11"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_11" value="3" data-bind="checked: R_2_4_11"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_11" value="4" data-bind="checked: R_2_4_11"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_11" value="5" data-bind="checked: R_2_4_11"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.12 โครงการสามารถปรับเปลี่ยนเทคโนโลยีสารสนเทศ และ นวัตกรรมได้อย่างอิสระ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_12" value="1" data-bind="checked: R_2_4_12"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_12" value="2" data-bind="checked: R_2_4_12"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_12" value="3" data-bind="checked: R_2_4_12"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_12" value="4" data-bind="checked: R_2_4_12"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_12" value="5" data-bind="checked: R_2_4_12"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.13 โครงการสามารถปรับเปีล่ยนกิจกรรม / รูปแบบกิจกรรมได้อย่างอิสระ 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_13" value="1" data-bind="checked: R_2_4_13"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_13" value="2" data-bind="checked: R_2_4_13"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_13" value="3" data-bind="checked: R_2_4_13"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_13" value="4" data-bind="checked: R_2_4_13"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_13" value="5" data-bind="checked: R_2_4_13"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.14 โครงการได้ทำการเปิมนความก้าวหน้าในการดำเนินงานเปรียบเทียบกับการใช้จ่ายงบประมาณเป็นระยะตลอดการดำเนินโครงการ 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_14" value="1" data-bind="checked: R_2_4_14"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_14" value="2" data-bind="checked: R_2_4_14"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_14" value="3" data-bind="checked: R_2_4_14"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_14" value="4" data-bind="checked: R_2_4_14"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_14" value="5" data-bind="checked: R_2_4_14"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.15 กองทุนมีข้อสังเกต / ข้อแนะนำตอลกลับเมื่อได้รับรายงานผลการปฏิบัติงาน และ การใช้จ่ายงบประมาณของโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_15" value="1" data-bind="checked: R_2_4_15"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_15" value="2" data-bind="checked: R_2_4_15"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_15" value="3" data-bind="checked: R_2_4_15"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_15" value="4" data-bind="checked: R_2_4_15"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_15" value="5" data-bind="checked: R_2_4_15"  />
                                    </td>
				</tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.16 กองทุนได้ส่งตัวแทนเข้าร่วมสังเกตการณ์ในกิจกรรมสำคัญของดครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_16" value="1" data-bind="checked: R_2_4_16"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_16" value="2" data-bind="checked: R_2_4_16"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_16" value="3" data-bind="checked: R_2_4_16"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_16" value="4" data-bind="checked: R_2_4_16"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_16" value="5" data-bind="checked: R_2_4_16"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.17 กองทุนได้ทำการประเมินความเสี่ยง และ สรุปเป็นข้อเสนอและสำหรับการปรับปรุงในอนาคตไว้

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_17" value="1" data-bind="checked: R_2_4_17"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_17" value="2" data-bind="checked: R_2_4_17"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_17" value="3" data-bind="checked: R_2_4_17"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_17" value="4" data-bind="checked: R_2_4_17"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_17" value="5" data-bind="checked: R_2_4_17"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.18 กองทุนได้มีข้อแนะนำให้กับโครงการฯ เป็นระยะตลอดการดำเนินงาน

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_18" value="1" data-bind="checked: R_2_4_18"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_18" value="2" data-bind="checked: R_2_4_18"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_18" value="3" data-bind="checked: R_2_4_18"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_18" value="4" data-bind="checked: R_2_4_18"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_18" value="5" data-bind="checked: R_2_4_18"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.19 กองทุนพร้อมที่จะให้คำแนะนำและ / หรือเข้าร่วมแก้ไขปัญหาร่วมกับโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_19" value="1" data-bind="checked: R_2_4_19"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_19" value="2" data-bind="checked: R_2_4_19"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_19" value="3" data-bind="checked: R_2_4_19"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_19" value="4" data-bind="checked: R_2_4_19"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_4_19" value="5" data-bind="checked: R_2_4_19"  />
                                    </td> 
                                </tr> 
                                				
                           <tr>
                             <td colspan="6" style="background-color:lightslategray;font-weight:bold">ช่วงที่ 3 ประเมินภายหลังการดำเนินการ</td>
                         </tr> 			 
			 <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">5. การประเมินผลกระทบ (Impact Evaluation) เกี่ยวกับผลกระทบต่อโครงการ / ต่อผุ้รับผลประโยชน์ / ต่อชุมชน</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.1 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อ กลุ่มเป้าหมายของโครงการโดยตรง
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_1" value="1" data-bind="checked: R_3_5_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_1" value="2" data-bind="checked: R_3_5_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_1" value="3" data-bind="checked: R_3_5_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_1" value="4" data-bind="checked: R_3_5_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_1" value="5" data-bind="checked: R_3_5_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       5.2 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อครอบครัวของกลุ่มเป้าหมายของโครงการ 

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_2" value="1" data-bind="checked: R_3_5_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_2" value="2" data-bind="checked: R_3_5_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_2" value="3" data-bind="checked: R_3_5_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_2" value="4" data-bind="checked: R_3_5_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_2" value="5" data-bind="checked: R_3_5_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.3 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อองค์กรของกลุ่มเป้าหมายของโครงการ 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_3" value="1" data-bind="checked: R_3_5_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_3" value="2" data-bind="checked: R_3_5_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_3" value="3" data-bind="checked: R_3_5_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_3" value="4" data-bind="checked: R_3_5_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_3" value="5" data-bind="checked: R_3_5_3"  />
                                    </td> 
                                </tr>    
                                  <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.4 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อชุมชนของกลุ่มเป้าหมายของโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_4" value="1" data-bind="checked: R_3_5_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_4" value="2" data-bind="checked: R_3_5_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_4" value="3" data-bind="checked: R_3_5_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_4" value="4" data-bind="checked: R_3_5_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_4" value="5" data-bind="checked: R_3_5_4"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.5 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อพื้นที่ดำเนินดครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_5" value="1" data-bind="checked: R_3_5_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_5" value="2" data-bind="checked: R_3_5_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_5" value="3" data-bind="checked: R_3_5_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_5" value="4" data-bind="checked: R_3_5_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_5" value="5" data-bind="checked: R_3_5_5"  />
                                    </td> 
                                </tr> 				
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.6 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อสังคมโดยรวม

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_6" value="1" data-bind="checked: R_3_5_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_6" value="2" data-bind="checked: R_3_5_6"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_6" value="3" data-bind="checked: R_3_5_6"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_6" value="4" data-bind="checked: R_3_5_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_5_6" value="5" data-bind="checked: R_3_5_6"  />
                                    </td> 
                                </tr> 
				
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">6. การประเมินประสิทธิผล (Effectiveness Evaluation)</td>
                         </tr>
                                <tr  role="row">
                                    <td role="gridcell">
                                        6.1 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วได้ผลตามที่คาดหวังไว้ 
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_1" value="1" data-bind="checked: R_3_6_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_1" value="2" data-bind="checked: R_3_6_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_1" value="3" data-bind="checked: R_3_6_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_1" value="4" data-bind="checked: R_3_6_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_1" value="5" data-bind="checked: R_3_6_1"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.2 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วบรรลุวัตถุประสงค์โครงการที่กำหนดไว้     
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_2" value="1" data-bind="checked: R_3_6_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_2" value="2" data-bind="checked: R_3_6_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_2" value="3" data-bind="checked: R_3_6_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_2" value="4" data-bind="checked: R_3_6_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_2" value="5" data-bind="checked: R_3_6_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.3 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วบรรลุวัตถุประสงค์ตัวชี้วัดที่กำหนดไว้ในเชิงปริมาณ 

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_3" value="1" data-bind="checked: R_3_6_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_3" value="2" data-bind="checked: R_3_6_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_3" value="3" data-bind="checked: R_3_6_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_3" value="4" data-bind="checked: R_3_6_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_3" value="5" data-bind="checked: R_3_6_3"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.4 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วบรรลุตามตัวชี้วัดที่กำหนดไว้ในเชิงคุณภาพ

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_4" value="1" data-bind="checked: R_3_6_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_4" value="2" data-bind="checked: R_3_6_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_4" value="3" data-bind="checked: R_3_6_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_4" value="4" data-bind="checked: R_3_6_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_4" value="5" data-bind="checked: R_3_6_4"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.5 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วบรรลุตามตัวชี้วัดทีกำหนดไว้ในเชิงคุณภาพ ตามหลัวิชาการ / เกณฑ์สากล 

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_5" value="1" data-bind="checked: R_3_6_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_5" value="2" data-bind="checked: R_3_6_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_5" value="3" data-bind="checked: R_3_6_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_5" value="4" data-bind="checked: R_3_6_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_6_5" value="5" data-bind="checked: R_3_6_5"  />
                                    </td> 
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">7. การประเมินความยังยืน (Sustainability Evaluation) เกี่ยวกับความต่อเนิ่องในอนาคต</td>
                         </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.1 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อกลุ่มเป้าหมายของโครงการโดยตรงเป็นระยะเวลาอย่างน้อย 5 ปี
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_1" value="1" data-bind="checked: R_3_7_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_1" value="2" data-bind="checked: R_3_7_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_1" value="3" data-bind="checked: R_3_7_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_1" value="4" data-bind="checked: R_3_7_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_1" value="5" data-bind="checked: R_3_7_1"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.2 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อครอบครัวของกลุ่มเป้าหมายของโครงการเป็นระยะเวลาอย่างน้อย 5 ปี

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_2" value="1" data-bind="checked: R_3_7_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_2" value="2" data-bind="checked: R_3_7_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_2" value="3" data-bind="checked: R_3_7_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_2" value="4" data-bind="checked: R_3_7_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_2" value="5" data-bind="checked: R_3_7_2"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.3 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อต่อองค์กรของกลุ่มเป้าหมายของโครงการเป็นระยะเวลาอย่างน้อย 5 ปี

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_3" value="1" data-bind="checked: R_3_7_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_3" value="2" data-bind="checked: R_3_7_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_3" value="3" data-bind="checked: R_3_7_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_3" value="4" data-bind="checked: R_3_7_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_3" value="5" data-bind="checked: R_3_7_3"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.4 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อต่อชุมชนของกลุ่มเป้าหมายของโครงการเป้ฯระยะเวลาอย่างน้อย 5 ปี

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_4" value="1" data-bind="checked: R_3_7_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_4" value="2" data-bind="checked: R_3_7_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_4" value="3" data-bind="checked: R_3_7_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_4" value="4" data-bind="checked: R_3_7_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_4" value="5" data-bind="checked: R_3_7_4"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.5 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อต่อพื้นที่ดำเนินงานของโครงการเป็นระยะเวลาอย่างน้อย 5 ปี

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_5" value="1" data-bind="checked: R_3_7_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_5" value="2" data-bind="checked: R_3_7_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_5" value="3" data-bind="checked: R_3_7_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_5" value="4" data-bind="checked: R_3_7_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_5" value="5" data-bind="checked: R_3_7_5"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.6 โครงการฯ ที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อต่อสังคมในภาพรวมเป็นระยะเวลาอย่างน้อย 5 ปี

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_6" value="1" data-bind="checked: R_3_7_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_6" value="2" data-bind="checked: R_3_7_6"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_6" value="3" data-bind="checked: R_3_7_6"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_6" value="4" data-bind="checked: R_3_7_6"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_6" value="5" data-bind="checked: R_3_7_6"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.7 โครงการได้ชี้แจงทำความเข้าใจเกี่ยวกับการดำเนินโครงการและเปิดให้สมาชิกหน่วยงาน / องค์กร และ ชมุชน (ที่ไม่ใช้กลุ่มเป้าหมาย) เข้ามามีส่วนร่วมอย่างเหมาะสมในระหว่างทีดำเนินโครงการ และ เมื่อสิ้นสุดโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_7" value="1" data-bind="checked: R_3_7_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_7" value="2" data-bind="checked: R_3_7_7"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_7" value="3" data-bind="checked: R_3_7_7"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_7" value="4" data-bind="checked: R_3_7_7"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_7" value="5" data-bind="checked: R_3_7_7"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.8 โครงการได้ชี้แจงทำความเข้าใจถึงประโยชน์ที่เกิดขึ้นจากการดำเนินโครงการต่อสมาชิกหน่วยงาน / องค์กร และ ชุมชน (ที่ไม่ใช้กลุ่มเป้าหมาย) อย่างเหมาะสมในระหว่างที่ดำเนินโครงการและเมื่อสิ้นสุดโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_8" value="1" data-bind="checked: R_3_7_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_8" value="2" data-bind="checked: R_3_7_8"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_8" value="3" data-bind="checked: R_3_7_8"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_8" value="4" data-bind="checked: R_3_7_8"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_8" value="5" data-bind="checked: R_3_7_8"  />
                                    </td>   
                                </tr>
		                    <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.9 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วก่อให้เกิดการเปลี่ยนแปลงในทางที่ดีขึ้นต่อกลุ่มเป้าหมาย ครอบครัวของกลุ่มเปห้าหมาย ชุมชนของกลุ่มเป้าหมาย ครบทุกมิติ ทั้งทางด้านชีวิตสังคม เศรษฐกิจ สิ่งแวดลอม

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_9" value="1" data-bind="checked: R_3_7_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_9" value="2" data-bind="checked: R_3_7_9"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_9" value="3" data-bind="checked: R_3_7_9"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_9" value="4" data-bind="checked: R_3_7_9"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_7_9" value="5" data-bind="checked: R_3_7_9"  />
                                    </td>   
                                </tr>

                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">8. การประเมินการถ่ายทอดส่งต่อ (Transportability Evaluation) เกี่ยวกับการถ่ายทอดส่งต่อไปยังโครงการอื่น หน่วยงานอื่น ชุมชนอื่น</td>
                         </tr>                          
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       8.1 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วได้จัดทำรายละเอียดปัจจัยความสำเร็จ (KSFs) และ ปัจจัยเสี่ยง / ข้อควรระวังในการดำเนินโครงการ
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_1" value="1" data-bind="checked: R_3_8_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_1" value="2" data-bind="checked: R_3_8_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_1" value="3" data-bind="checked: R_3_8_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_1" value="4" data-bind="checked: R_3_8_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_1" value="5" data-bind="checked: R_3_8_1"  />
                                    </td>   
                     
 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        8.2 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วได้นำ (KSFs) และ ปัจจัยเสี่ยง / ข้อควรระวังในการดำนเนินโครงการส่งต่อไปให้กับโครงการอื่นๆ หรือ หน่วยงานอื่นชุมชนอื่น

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_2" value="1" data-bind="checked: R_3_8_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_2" value="2" data-bind="checked: R_3_8_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_2" value="3" data-bind="checked: R_3_8_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_2" value="4" data-bind="checked: R_3_8_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_2" value="5" data-bind="checked: R_3_8_2"  />
                                    </td>   
                                </tr>                           
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        8.3 กองทุนได้นำ (KSFs) และ ปัจจัยเสี่ยง / ข้อควรระวังในการดำเนินโครงการไปปรับใช้ในการพัฒนาโครงการอื่นๆ ที่ขอรับการสนับสนุน
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_3" value="1" data-bind="checked: R_3_8_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_3" value="2" data-bind="checked: R_3_8_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_3" value="3" data-bind="checked: R_3_8_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_3" value="4" data-bind="checked: R_3_8_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_8_3" value="5" data-bind="checked: R_3_8_3"  />
                                    </td>    
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">9. การประเมินผลสรุปรวบยอด (Meta Evaluation) เป็นการสรุปผลรวบยอดจากรายงานการประเมินผลที่ใช้ระเบียบวิธีที่มีความแตกต่างกัน</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        9.1 กองทุนได้นำรายงานความก้าวหน้าตามระยะเวลาของทุกโครงการที่ดำเนินการอยู่ในขณนั้นมาพิจารณาเอเป็นข้อมูลในการให้คำแนะนำ ให้ข้อเสนอแนะ ให้ข้อควรระวังหรือเพื่อปรับปรุง และ พัฒนาข้ามโครงการ
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_1" value="1" data-bind="checked: R_3_9_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_1" value="2" data-bind="checked: R_3_9_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_1" value="3" data-bind="checked: R_3_9_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_1" value="4" data-bind="checked: R_3_9_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_1" value="5" data-bind="checked: R_3_9_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       9.2 กองทุนได้นำรายงานการใช้จ่ายงบประมาณของทุกโครงการที่ดำเนินการอยู่ในแต่ละปีมาพิจารณาเพื่อเป็นฐานข้อมูลในการพิจารณาสนับสนุนโครงการในปีต่อไปใกล้เคียงกันตามหลักพอประมาณและมีเหตุผลในปีต่อๆ ไป

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_2" value="1" data-bind="checked: R_3_9_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_2" value="2" data-bind="checked: R_3_9_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_2" value="3" data-bind="checked: R_3_9_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_2" value="4" data-bind="checked: R_3_9_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_2" value="5" data-bind="checked: R_3_9_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        9.3 กองทุนได้นำรายงานใช้จ่ายงบประมาณของทุกโครงการที่ดำเนินการอยู่ในแต่ะปีมาพิจารณาเพื่อเป็นฐานข้อมูลในการพิจารณาการให้งบประมาณสนับสนุนโครงการที่มีลักษณะใกล้เคียงกันตามหลักพอประมาณและมีเหตุผลในปีต่อๆ ไป

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_3" value="1" data-bind="checked: R_3_9_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_3" value="2" data-bind="checked: R_3_9_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_3" value="3" data-bind="checked: R_3_9_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_3" value="4" data-bind="checked: R_3_9_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_3" value="5" data-bind="checked: R_3_9_3"  />
                                    </td> 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        9.4 กองทุนได้นำรายงานของทุกโครงการที่ดำเนินการอยู่ในแต่ละปีมาสรุปเพื่อเป็นฐานข้อมูลประกอบการพิจารณาการกำหนดหลักการและเหตุผล เป้าหมาย วัตถุประสงค์ กลุ่มเป้าหมาย และ ผลผลิตของโครงการที่มีลักษณะใกล้เคียงกันตามหลักพอประมาณและมีเหตุผลในปีต่อๆ ไปเพื่อเป็นการยกมาตรฐานโครงการ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_4" value="1" data-bind="checked: R_3_9_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_4" value="2" data-bind="checked: R_3_9_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_4" value="3" data-bind="checked: R_3_9_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_4" value="4" data-bind="checked: R_3_9_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_9_4" value="5" data-bind="checked: R_3_9_4"  />
                                    </td> 
                                </tr> 				
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">10. การนำเสนอบทเรียนสุดท้าย (Final Synthesis Report เกี่ยวกับสิ่งที่จัดทำ ผลสำเร็จและบทเรียนจากประสบการณ์</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        10.1 โครงการที่ดำเนินการเสร็นสิ้นไปแล้วได้มีการนำเสนอบทเรียนสุดท้าย (Final Synthesis Report) เกี่ยวกับสิ่งที่จัดทำ ผลสำเร็จ และ บทเรียนจากประสบการณ์ นอกเนือไปจากการรายงานด้วยเอกสารให้กับกองทุน
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_1" value="1" data-bind="checked: R_3_10_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_1" value="2" data-bind="checked: R_3_10_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_1" value="3" data-bind="checked: R_3_10_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_1" value="4" data-bind="checked: R_3_10_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_1" value="5" data-bind="checked: R_3_10_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       10.2 โครงการที่ดำเนินการเสร็นสิ้นไปแล้วได้มีการนำเสนอบทเยนสุดท้าย (Final Synthesis Report) เกี่ยวกับสิ่งที่จัดทำ ผลสำเร็จ และ บทเรียนจากประสบการณ์ ให้กับกลุ่มเป้าหมาย

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_2" value="1" data-bind="checked: R_3_10_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_2" value="2" data-bind="checked: R_3_10_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_2" value="3" data-bind="checked: R_3_10_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_2" value="4" data-bind="checked: R_3_10_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_2" value="5" data-bind="checked: R_3_10_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        10.3 โครงการที่ดำเนินการเสร็จสิ้นไปแล้วได้มีการนำเสนอ บทเรียนสุดท้าย (Final Synthesis Report) เกี่ยวกับสิ่งที่จัดทำ ผลสำเร็จ และ บทเรียนจากประสบการณ์ให้กับสมาชิกหน่วยงาน / องค์กร และชุมชน (ที่ไม่ใช้กลุ่มเป้าหมาย)

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_3" value="1" data-bind="checked: R_3_10_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_3" value="2" data-bind="checked: R_3_10_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_3" value="3" data-bind="checked: R_3_10_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_3" value="4" data-bind="checked: R_3_10_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_3" value="5" data-bind="checked: R_3_10_3"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        10.4 กองทุนมีเวที / มีช่องทางให้โครงการที่ดำเนินการเสร็นสิ้นไปแล้วได้มีการนำเสนอ บทเรียนสุดท้าย (Final Synthesis Report) เกี่ยวกับสิ่งที่จัดทำ ผลสำเร็จและบทเรียนจากประสบการณ์

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_4" value="1" data-bind="checked: R_3_10_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_4" value="2" data-bind="checked: R_3_10_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_4" value="3" data-bind="checked: R_3_10_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_4" value="4" data-bind="checked: R_3_10_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_10_4" value="5" data-bind="checked: R_3_10_4"  />
                                    </td> 
                                </tr>  				
                                </table>
      
                    <div style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ช่วงที่ 4 ความคิดเห็นในเรื่องของปัญหา อุปสรรค และ ข้อเสนอแนะในการดำเนินโครงการในภาพรวม<br />
                        คำชี้แจง โปรดแสดงความคิดเห็นในเรื่องของปัญหา อุปสรรคและข้อสเนอแนะลงในช่องว่างให้ตรงตามความคิดเห็นของท่านมากที่สุด
                    </label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 11.1 สิ่งที่ท่านเห็นว่าเป็นปัญหา อุปสรรค สิ่งที่ควรต้องแก้ไขปรับปรุงที่เกิดขึ้นก่อนดำเนินโครงการ
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_11_1" data-bind="value: T_4_11_1" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
            <%--                <asp:RequiredFieldValidator ID="RequiredT_4" ControlToValidate="T_4" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                />   --%>                
                        </div>
                        <div class="col-sm-12"> 11.2 สิ่งที่ท่านเห็นว่าเป็นปัญหา อุปสรรค สิ่งที่ควรต้องแก้ไขปรับปรุงที่เกิดขึ้นระหว่างดำเนินโครงการ
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_11_2" data-bind="value: T_4_11_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>
                        <div class="col-sm-12"> 11.3 สิ่งที่ท่านเห็นว่าเป็นปัญหา อุปสรรค สิ่งที่ควรต้องแก้ไขปรับปรุงที่เกิดขึ้นเมื่อโครงการสิ้นสุดแล้ว
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_11_3" data-bind="value: T_4_11_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>

                        <div class="col-sm-12"> 12.1 สิ่งที่ท่านต้องการเสนอแนะ ให้ข้อคิดเห็นเพื่อปรับปรุง พัฒนาในช่วงก่อนดำเนินโครงการ
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_12_1" data-bind="value: T_4_12_1" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>
                        <div class="col-sm-12"> 12.2 สิ่งที่ท่านต้องการเสนอแนะ ให้ข้อคิดเห็นเพื่อปรับปรุง พัฒนาในช่วงระหว่างดำเนินโครงการ
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_12_2" data-bind="value: T_4_12_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>
                        <div class="col-sm-12"> 12.3 สิ่งที่ท่านต้องการเสนอแนะ ให้ข้อคิดเห็นเพื่อปรับปรุง พัฒนาในช่วงก่อนเมื่อโครงการสิ้นสุดลงแล้ว
                            </div>
                        <div class="col-sm-12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4_12_3" data-bind="value: T_4_12_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>
                        <div class="col-sm-12"> 12.4 ท่านคิดว่า โครงการนี้ควรดำเนินการต่อ / ขยายต่อหรือไม่
                            </div>
                        <div class="col-sm-12">
                        <div class="col-sm-4">
                            <input type="radio" name="R_4_12_4" value="1" data-bind="checked: R_4_12_4"  /> 1. ควร
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_4_12_4" value="2" data-bind="checked: R_4_12_4"  /> 2. ไม่ควร
                        </div>
                            <div class="required-block">
                                เหตุผล
                                <nep:TextBox ID="T_4_12_4" data-bind="value: T_4_12_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
             
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ช่วงที่ 5 ข้อมูลส่วนบุคคล (ข้อ 15-17 ตอบเฉพาะกลุ่มผู้ได้รับทุน)<br />
                         คำชี้แจง โปรดตอบตามข้อเท็จจริง ข้อมูลที่ได้จะเก็บเป็นความลับ และ ใช้ในมิติทางวิชาการเท่านั้น
                        </label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 13. บทบาทของท่านในโครงการ
                            </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_13" value="1" data-bind="checked: R_5_13"  /> 1. ผู้บริหารกองทุน
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_13" value="2" data-bind="checked: R_5_13"  /> 2. เจ้าหน้าที่กองทุน
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_13" value="3" data-bind="checked: R_5_13"  /> 3. ผู้บริหารโครงการ
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_13" value="4" data-bind="checked: R_5_13"  /> 4. เจ้าหน้าที่โครงการ
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_13" value="5" data-bind="checked: R_5_13"  /> 5. บุคลากรสนับสนุนของโครงการ
                        </div>

                    </div>     
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 14. ท่านได้มีส่วนร่วมในการดำเนินโครงการนี้ตั้งแต่ต้นจนจบ
                            </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_14" value="1" data-bind="checked: R_5_14"  /> 1. ใช่  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_14" value="2" data-bind="checked: R_5_14"  /> 2. ไม่ใช้ แต่มีส่วนร่วมมากกว่าครึ่ง  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_14" value="3" data-bind="checked: R_5_14"  /> 3. ไม่ใช้ และ มีส่วนร่วมน้อยกว่าครึ่งโครงการ เมื่อนับจากระยะเวลา   
                        </div>
            
                    </div>       
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 15. ท่านเคยรับผิดชอบโครงการที่มีลักษณะใกล้เคียงกับโครงการนี้มาก่อน
                            </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_15" value="1" data-bind="checked: R_5_15"  /> 1. ใช่  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_15" value="2" data-bind="checked: R_5_15"  /> 2. ไม่ใช้  
                        </div>
                    </div>  
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 16. ท่านเคยได้รับทุนสนับสนุนจากกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการมาก่อนหรือไม่ 
                            </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_16" value="1" data-bind="checked: R_5_16"  /> 1. เคย ตัวอย่างโครงการสำคัญ  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_16" value="2" data-bind="checked: R_5_16"  /> 2. ไม่เคย  
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 1 <nep:TextBox ID="T_5_16_1" data-bind="value: T_5_16_1" runat="server" width="100%"></nep:TextBox>              
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 2 <nep:TextBox ID="T_5_16_2" data-bind="value: T_5_16_2" runat="server" width="100%" ></nep:TextBox>              
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 3 <nep:TextBox ID="T_5_16_3" data-bind="value: T_5_16_3" runat="server" width="100%" ></nep:TextBox>              
                        </div>
                    </div>   
                     <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 17. ท่านเคยได้รับทุนสนับสนุนจากองทุนอื่น / แหล่งทุนอื่นมาก่อนหรือไม่ 
                            </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_17" value="1" data-bind="checked: R_5_17"  /> 1. เคย ตัวอย่างโครงการสำคัญ  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_5_17" value="2" data-bind="checked: R_5_17"  /> 2. ไม่เคย  
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 1 <nep:TextBox ID="T_5_17_1" data-bind="value: T_5_17_1" runat="server" width="100%"></nep:TextBox>              
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 2 <nep:TextBox ID="T_5_17_2" data-bind="value: T_5_17_2" runat="server" width="100%" ></nep:TextBox>              
                        </div>
                        <div class="col-sm-12">        
                                โครงการที่ 3 <nep:TextBox ID="T_5_17_3" data-bind="value: T_5_17_3" runat="server" width="100%" ></nep:TextBox>              
                        </div>
                    </div>     
                    </div> 
                                                                                                       
                </div><!--form-horizontal-->
            </div>
 
         </div><!--panel แบบรายงานผลการปฏิบัติงาน-->
       

        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>

                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectReport%>"
                        OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" />
                        <%--OnClick="ButtonSaveAndSendProjectReport_Click" />--%>

              <%--       <asp:Button runat="server" ID="ButtonOfficerSave" CssClass="btn btn-primary btn-sm" ValidationGroup="OfficerSaveReportResult"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonOfficerSaveSuggestion %>" OnClick="ButtonOfficerSave_Click" Visible="false" />--%>

              <%--      <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectResult?projectID={0}", ProjectID ) %>'></asp:HyperLink>      --%>          

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>

