<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollowUnder5MControl_2019.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollowUnder5MControl_2019"  %>
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
            .tdborder {
                border-left-width: 1px;
    border-left-style: solid;
    border-left-color: rgb(204, 204, 204);
            }
               input ,textarea {
       border:solid;
       border-width:1px;
   }
               
        </style>
        <asp:HiddenField runat="server" ID="hdfQViewModel" />
        <asp:HiddenField runat="server" ID="hdfQContols" />
        <asp:HiddenField runat="server" ID="hdfIsDisable" />

        <div id="divVueQN" class="panel panel-default">
            <div class="panel-heading" style="text-align:center">
                <h3 class="panel-title">แบบประเมินผลการติดตามประเมินผลโครงการ (สำหรับเจ้าหน้าที่กองทุน)<br />
การประเมินผลโครงการที่ได้รับการสนับสนุนจากกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ<br />
(งบประมาณต่ำกว่า 5 ล้านบาท)</h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                   <%--<span class="field-desc">--%>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ส่วนที่ 1 การประเมินประสิทธิภาพของโครงการ</label>
                    </div>

                     <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                        <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:20px">ลำดับที่</th>
                                <th scope="col" role="columnheader" class="k-header" >รายการประเมิน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20px">น้ำหนัก</th>    
                                <th colspan="4" scope="col" role="columnheader" class="k-header" >เกณฑ์</th>
                                <th scope="col" role="columnheader" class="k-header" >หลักฐาน</th>
                                <th  scope="col" role="columnheader" class="k-header" >คะแนน</th>

                        </tr>

                   </thead>
                                <tr>
                                    <td rowspan="3">
                                        1
                                    </td>
                                    <td rowspan="3">
                                        ด้านจำนวนผู้เข้าร่วมโครงการ
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        20    
                                    </td>
                                     <td style="width:80px;font-size:10px">100-80%</td>
                                    <td  style="width:80px;font-size:10px">79-60%</td>
                                    <td style="width:80px;font-size:10px">59-40%</td>
                                    <td style="width:80px;font-size:10px">น้อยกว่า 40%</td>
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_1_2].v"  />รูปถ่าย<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_1_2].v"  />ใบลงทะเบียน<br />
                                        <input type="checkbox" value="3" v-model="items[field.mcb1_1_2].v"  />สำเนาบัตรประจำตัวประชาชน<br />
                                        <input type="checkbox" value="4" v-model="items[field.mcb1_1_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_1_3].v" />
                                    </td>
                                    <td rowspan="3">
                                        {{items[field.rd1_1_1].v}}
                                    </td>
                                </tr>
                         <tr>
                                <td  style="width:80px;font-size:10px">20 คะแนน</td>
                                <td  style="width:80px;font-size:10px">18 คะแนน</td>
                                <td  style="width:80px;font-size:10px">16 คะแนน</td>
                                <td  style="width:80px;font-size:10px">14 คะแนน</td>
                         </tr>
                         <tr>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="18" v-model="items[field.rd1_1_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="16" v-model="items[field.rd1_1_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="14" v-model="items[field.rd1_1_1].v"  />
                                    </td>   


                         </tr>

                         <tr  role="row">
                             <td>2</td>
                             <td colspan="8">ด้านกิจกรรม</td>
                        </tr>

                        <tr>
                                    <td rowspan="3">
                                        
                                    </td>
                                    <td rowspan="3">
                                        2.1 การดำเนินงานครบถ้วน
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        10    
                                    </td>
                                    <td style="width:80px;font-size:10px">100%</td>
                                    <td  style="width:80px;font-size:10px">99-80%</td>
                                    <td style="width:80px;font-size:10px">79-60%</td>
                                    <td style="width:80px;font-size:10px">น้อยกว่า 60%</td>
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_2_1_2].v"  />รูปถ่าย<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_2_1_2].v"  />รายงานผล<br />
                                        <input type="checkbox" value="3" v-model="items[field.mcb1_2_1_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_1_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                        {{items[field.rd1_2_1_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td  style="width:80px;font-size:10px">10 คะแนน</td>
                                <td  style="width:80px;font-size:10px">8 คะแนน</td>
                                <td  style="width:80px;font-size:10px">6 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                         </tr>                                
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="10" v-model="items[field.rd1_2_1_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="8" v-model="items[field.rd1_2_1_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="6" v-model="items[field.rd1_2_1_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="4" v-model="items[field.rd1_2_1_1].v"  />
                                    </td>                                   

                                </tr>
                        <tr>
                                    <td rowspan="3">
                                        
                                    </td>
                                    <td rowspan="3">
                                        2.2 ระดับความพึงพอใจ
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        5    
                                    </td>
                                    <td style="width:80px;font-size:10px">4.01-5.00</td>
                                    <td  style="width:80px;font-size:10px">3.01-4.00</td>
                                    <td style="width:80px;font-size:10px">2.01-3.00</td>
                                    <td style="width:80px;font-size:10px">1.01-2.00 </td>
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_2_2_2].v"  />ผลการสำรวจความพึงพอใจ<br />
                                        <input type="checkbox" value="3" v-model="items[field.mcb1_2_2_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_2_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                        {{items[field.rd1_2_2_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td  style="width:80px;font-size:10px">5 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                                <td  style="width:80px;font-size:10px">3 คะแนน</td>
                                <td  style="width:80px;font-size:10px">2 คะแนน</td>
                         </tr>                                
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="5" v-model="items[field.rd1_2_2_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="4" v-model="items[field.rd1_2_2_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="3" v-model="items[field.rd1_2_2_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="2" v-model="items[field.rd1_2_2_1].v"  />
                                    </td>                                   

                                </tr>
                                  <td rowspan="3">
                                        
                                    </td>
                                    <td rowspan="3">
                                       2.3 การประเมินตนเอง
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        5   
                                    </td>
                                    <td colspan="2" style="width:80px;font-size:10px">มี</td>
                                    <td colspan="2"  style="width:80px;font-size:10px">ไม่มี</td>
                           
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_2_3_2].v"  />แบบฟอร์มการประเมินตนเอง<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_2_3_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_3_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                         {{items[field.rd1_2_3_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td colspan="2" style="width:80px;font-size:10px">5 คะแนน</td>
                                <td colspan="2" style="width:80px;font-size:10px">0 คะแนน</td>

                         </tr>                                
                                <tr>
                                     <td colspan="2"  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="5" v-model="items[field.rd1_2_3_1].v"  />
                                    </td>
                                    <td colspan="2"  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="0" v-model="items[field.rd1_2_3_1].v"  />
                                    </td>           
  
                                </tr>

                        <tr  role="row">
                             <td>3</td>
                             <td colspan="8">ด้านระยะเวลาในการดำเนินโครงการ</td>
                        </tr>

                        <tr>
                                    <td rowspan="3">
                                        
                                    </td>
                                    <td rowspan="3">
                                        3.1 ระยะเวลาในการดำเนินโครงการ
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        5    
                                    </td>
                                    <td style="width:80px;font-size:10px">100%</td>
                                    <td  style="width:80px;font-size:10px">99-80%</td>
                                    <td style="width:80px;font-size:10px">79-60%</td>
                                    <td style="width:80px;font-size:10px">น้อยกว่า 60%</td>
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_3_1_2].v"  />ใบลงทะเบียน<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_3_1_2].v"  />กำหนดการ<br />
                                        <input type="checkbox" value="3" v-model="items[field.mcb1_3_1_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_3_1_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                        {{items[field.rd1_3_1_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td  style="width:80px;font-size:10px">5 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                                <td  style="width:80px;font-size:10px">3 คะแนน</td>
                                <td  style="width:80px;font-size:10px">2 คะแนน</td>
                         </tr>                                
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="5" v-model="items[field.rd1_3_1_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="4" v-model="items[field.rd1_3_1_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="3" v-model="items[field.rd1_3_1_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="2" v-model="items[field.rd1_3_1_1].v"  />
                                    </td>                                   

                                </tr>
                    <tr>
                                    <td rowspan="3">
                                        
                                    </td>
                                    <td rowspan="3">
                                        3.2 ระยะเวลาในการจัดส่งรายงานผล
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        5    
                                    </td>
                                    <td style="width:80px;font-size:10px">30วัน</td>
                                    <td  style="width:80px;font-size:10px">31-60วัน</td>
                                    <td style="width:80px;font-size:10px">61-90วัน</td>
                                    <td style="width:80px;font-size:10px">มากกว่า 90</td>
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_3_2_2].v"  />เล่มรายงานผลการปฏิบัติงานและค่าใช้จ่าย<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_3_2_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_3_2_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                        {{items[field.rd1_3_2_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td  style="width:80px;font-size:10px">5 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                                <td  style="width:80px;font-size:10px">3 คะแนน</td>
                                <td  style="width:80px;font-size:10px">2 คะแนน</td>
                         </tr>                                
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="5" v-model="items[field.rd1_3_2_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="4" v-model="items[field.rd1_3_2_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="3" v-model="items[field.rd1_3_2_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="2" v-model="items[field.rd1_3_2_1].v"  />
                                    </td>                                   

                                </tr>

                    <tr>
                                    <td rowspan="3">
                                        4
                                    </td>
                                    <td rowspan="3">
                                       ด้านงบประมาณ
                                    </td>

                                    <td rowspan="3"  style="text-align:center;vertical-align:central">
                                        10   
                                    </td>
                                    <td colspan="2" style="width:80px;font-size:10px">มีครบถ้วน</td>
                                    <td colspan="2"  style="width:80px;font-size:10px">ไม่ครบถ้วน</td>
                           
                                    <td rowspan="3">
                                        <input type="checkbox" value="1" v-model="items[field.mcb1_4_1_2].v"  />แบบรายงานผล<br />
                                        <input type="checkbox" value="2" v-model="items[field.mcb1_4_1_2].v"  />แบบสรุปค่าใช้จ่าย<br />
                                        <input type="checkbox" value="3" v-model="items[field.mcb1_4_1_2].v"  />เอกสารการเบิกจ่าย/ใบเสร็จรับเงิน<br />
                                        <input type="checkbox" value="4" v-model="items[field.mcb1_4_1_2].v"  />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_4_1_3].v" />
                                    </td> 
                                    <td rowspan="3">
                                         {{items[field.rd1_4_1_1].v}}
                                    </td>                                                                       
                                </tr>
                         <tr>
                                <td colspan="2" style="width:80px;font-size:10px">10 คะแนน</td>
                                <td colspan="2" style="width:80px;font-size:10px">0 คะแนน</td>

                         </tr>                                
                                <tr>
                                     <td colspan="2"  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="10" v-model="items[field.rd1_4_1_1].v"  />
                                    </td>
                                    <td colspan="2"  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="0" v-model="items[field.rd1_4_1_1].v"  />
                                    </td>           
  
                                </tr>
                        <tr>
                                    <td colspan="8"  style="text-align:right;vertical-align:central" role="gridcell">
                                        ผลรวมการประเมินประสิทธิผลโครงการ ส่วนที่ 1
                                    </td>  
                                    <td colspan="8"  style="text-align:right;vertical-align:central" role="gridcell">
                                        {{items[field.lbTotal1].v}}
                                    </td>
                        </tr>
                                </table>


                    <div class="form-group form-group-sm">
                        <br /><label class="col-sm-12 form-group-title">ส่วนที่ 2 การประเมินประสิทธิผลของโครงการ</label>
                    </div>

                     <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                        <tr role="row">
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width:20px">ลำดับที่</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" >รายการประเมิน</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width:20px">น้ำหนัก</th>    
                                <th colspan="5" scope="col" role="columnheader" class="k-header" >เกณฑ์</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" >คะแนน</th>

                        </tr>
                        <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:75px">100%</th>      
                            <th scope="col" role="columnheader" class="k-header" style="width:75px">99-80%</th>   
                            <th scope="col" role="columnheader" class="k-header" style="width:75px">79-60%</th>   
                            <th scope="col" role="columnheader" class="k-header" style="width:75px">69-40%</th>   
                             <th scope="col" role="columnheader" class="k-header" style="width:75px">น้อยกว่า 40%</th>  
                        </tr>
                   </thead>
                        <tr  role="row">
                             <td>1</td>
                             <td colspan="8">ด้านการเปรียบเทียบวัตถุประสงค์</td>
                        </tr>

                        <tr>
                                    <td rowspan="2" colspan="2">
                                        1.1 วัตถุประสงค์สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และกองทุนฯ
                                    </td>

                                    <td rowspan="2"  style="text-align:center;vertical-align:central">
                                        10    
                                    </td>
                                <td  style="width:80px;font-size:10px">10 คะแนน</td>
                                <td  style="width:80px;font-size:10px">8 คะแนน</td>
                                <td  style="width:80px;font-size:10px">6 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                                <td  style="width:80px;font-size:10px">2 คะแนน</td>

                                    <td rowspan="2">
                                        {{items[field.rd2_1_1_1].v}}
                                    </td>                                                                       
                                </tr>
                            
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="10" v-model="items[field.rd2_1_1_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="8" v-model="items[field.rd2_1_1_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="6" v-model="items[field.rd2_1_1_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="4" v-model="items[field.rd2_1_1_1].v"  />
                                    </td>                                   
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="2" v-model="items[field.rd2_1_1_1].v"  />
                                    </td>    
                                </tr>
                        <tr>
                       <tr>
                                    <td rowspan="2" colspan="2">
                                        1.2 ผลการดำเนินงานบรรลุวัตถุประสงค์ที่กำหนดไว้
                                    </td>

                                    <td rowspan="2"  style="text-align:center;vertical-align:central">
                                        10    
                                    </td>
                                <td  style="width:80px;font-size:10px">10 คะแนน</td>
                                <td  style="width:80px;font-size:10px">8 คะแนน</td>
                                <td  style="width:80px;font-size:10px">6 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>
                                <td  style="width:80px;font-size:10px">2 คะแนน</td>

                                    <td rowspan="2">
                                        {{items[field.rd2_1_2_1].v}}
                                    </td>                                                                       
                                </tr>
                            
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="10" v-model="items[field.rd2_1_2_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="8" v-model="items[field.rd2_1_2_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="6" v-model="items[field.rd2_1_2_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="4" v-model="items[field.rd2_1_2_1].v"  />
                                    </td>                                   
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="2" v-model="items[field.rd2_1_2_1].v"  />
                                    </td>    
                                </tr>
                        <tr>

                      <tr  role="row">
                             <td>2</td>
                             <td colspan="8">ด้านผลลัพธ์หรือความสำเร็จ</td>
                        </tr>

                        <tr>
                                    <td rowspan="2" colspan="2">
                                       2.1 ผลการดำเนินงานบรรลุตัวชี้วัด
                                    </td>

                                    <td rowspan="2"  style="text-align:center;vertical-align:central">
                                        20    
                                    </td>
                                <td  style="width:80px;font-size:10px">20 คะแนน</td>
                                <td  style="width:80px;font-size:10px">16 คะแนน</td>
                                <td  style="width:80px;font-size:10px">12 คะแนน</td>
                                <td  style="width:80px;font-size:10px">8 คะแนน</td>
                                <td  style="width:80px;font-size:10px">4 คะแนน</td>

                                    <td rowspan="2">
                                        {{items[field.rd2_2_1_1].v}}
                                    </td>                                                                       
                                </tr>
                            
                                <tr>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="20" v-model="items[field.rd2_2_1_1].v"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="16" v-model="items[field.rd2_2_1_1].v"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                       <input v-on:change="$CalulateFollowUnder5M()" type="radio"   value="12" v-model="items[field.rd2_2_1_1].v"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="8" v-model="items[field.rd2_2_1_1].v"  />
                                    </td>                                   
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input v-on:change="$CalulateFollowUnder5M()" type="radio"  value="4" v-model="items[field.rd2_2_1_1].v"  />
                                    </td>    
                                </tr>
                        <tr>
                        <tr>
                                    <td colspan="8"  style="text-align:right;vertical-align:central" role="gridcell">
                                        ผลรวมการประเมินประสิทธิผลโครงการ ส่วนที่ 2
                                    </td>  
                                    <td colspan="8"  style="text-align:right;vertical-align:central" role="gridcell">
                                        {{items[field.lbTotal2].v}}
                                    </td>
                        </tr>
                         </table>

                  <div class="form-group form-group-sm">
                       <br /> <label class="col-sm-12 form-group-title">รวมคะแนน </label>
                    </div>

                     <table  style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                        <tr role="row">
                                <th  scope="col" role="columnheader" class="k-header" style="width:20px">ส่วนที่</th>
                                <th  scope="col" role="columnheader" class="k-header" style="width:120px">คะแนนเต็ม</th>
                                <th  scope="col" role="columnheader" class="k-header" >คะแนนที่ได้</th>    
                        </tr>

                   </thead>

                                <tr  role="row">
                                    <td>
                                        1
                                    </td>
                                    <td style="text-align:center;vertical-align:central" role="gridcell">
                                        60
                                    </td>

                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        {{items[field.lbTotal1].v}}
                                    </td>
                                </tr>
                                <tr  role="row">
                                    <td>
                                        2
                                    </td>
                                    <td style="text-align:center;vertical-align:central" role="gridcell">
                                        40
                                    </td>

                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        {{items[field.lbTotal2].v}}
                                    </td>
                                </tr>
                                <tr  role="row">
                                     
                                    <td colspan="2" style="text-align:center;vertical-align:central" role="gridcell">
                                        คะแนนรวม
                                    </td>

                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        {{items[field.lbGrandTotal].v}}
                                    </td>
                                 </tr>
                         <tr>
                                     <td colspan="2" style="text-align:center;vertical-align:central" role="gridcell">
                                        โครงการอยู่ในระดับ/ความหมาย
                                    </td>

                                    <td  style="text-align:left;vertical-align:central" role="gridcell">

                                       <span v-html="items[field.lbResultText].v"></span>
                                    </td>
                                </tr>
                                </table>
                <br />
                <div class="form-group form-group-sm">
                   <div class="col-sm-6">
                      <label class="form-group-title">ข้อคิดเห็น</label> 
                      <textarea style="width:100%" rows="6" v-model="items[field.tbComment].v"></textarea>
                    </div>
                    <div class="col-sm-6">
                        <label class="form-group-title">ข้อควรพัฒนา</label><br />
                       <textarea style="width:100%" rows="6" v-model="items[field.tbSuggestion].v"></textarea>
                    </div>
                </div><!--form-horizontal-->
                                      
            </div>
                <br />
            <div class="form-group form-group-sm">
            <div class="container" style="font-size:14px;padding-top">
              <div class="row">

                <div class="col-sm-1">
                    1) ลงชื่อ  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbSignName1].v" />
               </div>
                  
                <div class="col-sm-1">
                   2) ลงชื่อ  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbSignName2].v" />
               </div>
                  
                <div class="col-sm-1">
                   3) ลงชื่อ  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbSignName3].v" />
               </div>
              </div>
                 <div class="row" style="padding-top: 5px">

                <div class="col-sm-1">
                    ตำแหน่ง  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbPosition1].v" />
               </div>
                  
                <div class="col-sm-1">
                    ตำแหน่ง  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbPosition2].v"  />
               </div>
                  
                <div class="col-sm-1">
                    ตำแหน่ง  
                </div>
               <div class="col-sm-3">
                 <input type="text" v-model="items[field.tbPosition3].v"  />
               </div>
              </div>
                <div class="row" style="padding-top: 5px">
                  <div class="col-sm-4" style="text-align:center">
                    เจ้าหน้าที่ประเมินผล
                   </div>
                    <div class="col-sm-4" style="text-align:center">
                    เจ้าหน้าที่ประเมินผล
                   </div>
                  <div class="col-sm-4" style="text-align:center">
                    เจ้าหน้าที่ประเมินผล
                   </div>
                </div>
            </div>
              
          
         </div><!--panel แบบรายงานผลการปฏิบัติงาน-->
           </div>
        </div>
         <div class="form-group form-group-sm">
               <br /> <label class="col-sm-2 control-label control-label-left without-delimit">
                        แนบไฟล์
                </label>
                <div class="col-sm-8">    
                    <nep:C2XFileUpload runat="server" ID="FileUploadAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                </div>                      
            </div>   
        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="appVueQN.param.IsReported = '0';appVueQN.saveData();SaveAttachmentFiles(); return false;"   Visible="false"/>

                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectReport%>"
                        OnClientClick="if (confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?')) {appVueQN.param.IsReported = '1';appVueQN.saveData();SaveAttachmentFiles(); return false; } else return false;" />
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

