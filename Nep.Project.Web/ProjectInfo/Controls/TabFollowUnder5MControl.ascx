<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollowUnder5MControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollowUnder5MControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelReportResult"
    UpdateMode="Conditional"
    runat="server">
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

            .tdborder {
                border-left-width: 1px;
                border-left-style: solid;
                border-left-color: rgb(204, 204, 204);
            }

            input, textarea {
                border: solid;
                border-width: 1px;
            }
        </style>
        <asp:HiddenField runat="server" ID="hdfQViewModel" />
        <asp:HiddenField runat="server" ID="hdfQContols" />
        <asp:HiddenField runat="server" ID="hdfIsDisable" />

        <div id="divVueQN" class="panel panel-default">
            <div class="panel-heading" style="text-align: center">
                <h3 class="panel-title">แบบติดตามประเมินผลโครงการ (สิ้นสุดการดำเนินโครงการ) (สำหรับเจ้าหน้าที่ผู้ประเมิน)<br />
                    โครงการที่ได้รับการสนับสนุนจากกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ</h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                    <div style="font-size: 14px">
                        <div>
                            <div class="col-sm-3">
                                <b>ชื่อโครงการ</b>
                            </div>
                            <div class="col-sm-9">
                                {{extend.projectTHName}}
                            </div>
                        </div>

                        <div>
                            <div class="col-sm-3">
                                <b>โครงการที่ได้รับการสนับสนุน</b>
                            </div>
                            <div class="col-sm-9">
                                <input type="radio" name="budgetRate" value="1" v-model="extend.budgetRate" disabled />มากกว่า 5 ล้าน
                                 <input type="radio" name="budgetRate" value="2" v-model="extend.budgetRate" disabled />น้อยกว่าหรือเท่ากับ 5 ล้าน
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>หน่วยงานที่รับผิดชอบ</b>
                            </div>
                            <div class="col-sm-9">
                                {{extend.organization}}
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>ผู้รับผิดชอบโครงการ</b>
                            </div>
                            <div class="col-sm-5">
                                {{extend.name1}}
                            </div>
                            <div class="col-sm-2">
                                <b>เบอร์โทรศัพท์</b>
                            </div>
                            <div class="col-sm-2">
                                {{extend.tel1}}
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>ผู้ประสานงาน</b>
                            </div>
                            <div class="col-sm-5">
                                {{extend.name2}}
                            </div>
                            <div class="col-sm-2">
                                <b>เบอร์โทรศัพท์</b>
                            </div>
                            <div class="col-sm-2">
                                {{extend.tel2}}
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>งบประมาณ (ที่อนุมัติ)</b>
                            </div>
                            <div class="col-sm-9">
                                {{extend.approvedBudget}}
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>วันที่เริ่มดำเนินการทำสัญญา</b>
                            </div>
                            <div class="col-sm-9">
                                {{extend.contractStartDate}}
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>ระยะเวลาดำเนินการ (เดือน)</b>
                            </div>
                            <div class="col-sm-9">
                                <input type="number" width="60px" v-model="items[field.processMonth].v" />
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <b>วันที่สิ้นสุดการดำเนินโครงการ</b>
                            </div>
                            <div class="col-sm-3">
                                <%--<nep:DatePicker runat="server" ID="DatePickerContractDate" ClearTime="true" EnabledTextBox="true"  />--%>
                                <input type="date" v-model="items[field.endDate].v" />
                                
                            </div>
                            <div class="col-sm-3">
                                <b>วันที่ประเมินผลโครงการ</b>
                            </div>
                            <div class="col-sm-3">
                               <input type="date" v-model="items[field.evaluateDate].v" />
                            </div>

                        </div>
                    </div>
                    <%--<span class="field-desc">--%>
                    <div>
                        <br /> 
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่ 1 การส่งมอบรายงาน</label>
                    </div>


                    <div style="font-size: 14px">
                        <div>
                            1. รายงานผลการดำเนินงาน/รายงานทางการเงิน
                        </div>

                        <div>
                            <div class="col-sm-12">
                                <input type="radio" name="R_0_1" value="1" v-model="items[field.rd0_1].v" />ส่งแล้ว<br />

                            </div>

                        </div>
                        <div>
                            <div class="col-sm-12">
                                <input type="radio" name="R_0_1" value="2" v-model="items[field.rd0_1].v" />ล่าช้ากว่ากำหนด<br />
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-12">
                                <input type="radio" name="R_0_1" value="3" v-model="items[field.rd0_1].v" />ล่าช้ากว่ากำหนด<br />
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-3">
                                <input type="radio" name="R_0_1" value="4" v-model="items[field.rd0_1].v" />ยังไม่ส่ง
                            </div>
                            <div class="col-sm-9">
                                เนื่องจาก
                            <textarea style="width: 100%" rows="6" v-model="items[field.tb0_1].v"></textarea>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ส่วนที่ 2 การประเมินประสิทธิภาพของโครงการ</label>
                    </div>

                    <table style="width: 100%; border-style: solid; border-color: #ccc; border-width: 1px;" class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width: 20px">ลำดับที่</th>
                                <th scope="col" role="columnheader" class="k-header">รายการประเมิน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 20px">น้ำหนัก</th>
                                <th colspan="4" scope="col" role="columnheader" class="k-header">เกณฑ์</th>
                                <th scope="col" role="columnheader" class="k-header">หลักฐาน</th>
                                <th scope="col" role="columnheader" class="k-header">คะแนน</th>

                            </tr>

                        </thead>
                        <tr>
                            <td rowspan="3">1
                            </td>
                            <td rowspan="3">ด้านจำนวนผู้เข้าร่วมโครงการ
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">10    
                            </td>
                            <td style="width: 80px; font-size: 10px">100-80%</td>
                            <td style="width: 80px; font-size: 10px">79-60%</td>
                            <td style="width: 80px; font-size: 10px">59-40%</td>
                            <td style="width: 80px; font-size: 10px">น้อยกว่า 40%</td>
                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_1_2].v" />รูปถ่าย<br />
                                <input type="checkbox" value="2" v-model="items[field.mcb1_1_2].v" />ใบลงทะเบียน<br />
                                <input type="checkbox" value="3" v-model="items[field.mcb1_1_2].v" />สำเนาบัตรประจำตัวประชาชน<br />
                                <input type="checkbox" value="4" v-model="items[field.mcb1_1_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_1_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_1_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">8 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="10" v-model="items[field.rd1_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="8" v-model="items[field.rd1_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="6" v-model="items[field.rd1_1_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="4" v-model="items[field.rd1_1_1].v" />
                            </td>


                        </tr>

                        <tr role="row">
                            <td>2</td>
                            <td colspan="8">ด้านกิจกรรม</td>
                        </tr>

                        <tr>
                            <td rowspan="3"></td>
                            <td rowspan="3">2.1 การดำเนินงานครบถ้วน
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">10    
                            </td>
                            <td style="width: 80px; font-size: 10px">100%</td>
                            <td style="width: 80px; font-size: 10px">99-80%</td>
                            <td style="width: 80px; font-size: 10px">79-60%</td>
                            <td style="width: 80px; font-size: 10px">น้อยกว่า 60%</td>
                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_2_1_2].v" />รูปถ่าย<br />
                                <input type="checkbox" value="2" v-model="items[field.mcb1_2_1_2].v" />รายงานผล<br />
                                <input type="checkbox" value="3" v-model="items[field.mcb1_2_1_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_1_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_2_1_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">8 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="10" v-model="items[field.rd1_2_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="8" v-model="items[field.rd1_2_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="6" v-model="items[field.rd1_2_1_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="4" v-model="items[field.rd1_2_1_1].v" />
                            </td>

                        </tr>
                        <tr>
                            <td rowspan="3"></td>
                            <td rowspan="3">2.2 ระดับความพึงพอใจ
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">15    
                            </td>
                            <td style="width: 80px; font-size: 10px">4.01-5.00</td>
                            <td style="width: 80px; font-size: 10px">3.01-4.00</td>
                            <td style="width: 80px; font-size: 10px">2.01-3.00</td>
                            <td style="width: 80px; font-size: 10px">1.01-2.00 </td>
                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_2_2_2].v" />ผลการสำรวจความพึงพอใจ<br />
                                <input type="checkbox" value="3" v-model="items[field.mcb1_2_2_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_2_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_2_2_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; font-size: 10px">15 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">12 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">9 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="15" v-model="items[field.rd1_2_2_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="12" v-model="items[field.rd1_2_2_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="9" v-model="items[field.rd1_2_2_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="6" v-model="items[field.rd1_2_2_1].v" />
                            </td>

                        </tr>
                        <td rowspan="3"></td>
                        <td rowspan="3">2.3 การประเมินตนเอง
                        </td>

                        <td rowspan="3" style="text-align: center; vertical-align: central">15   
                        </td>
                        <td colspan="2" style="width: 80px; font-size: 10px">มี </td>
                        <td colspan="2" style="width: 80px; font-size: 10px">ไม่มี</td>

                        <td rowspan="3">
                            <input type="checkbox" value="1" v-model="items[field.mcb1_2_3_2].v" />แบบฟอร์มการประเมินตนเอง<br />
                            <input type="checkbox" value="2" v-model="items[field.mcb1_2_3_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_2_3_3].v" />
                        </td>
                        <td rowspan="3">{{items[field.rd1_2_3_1].v}}
                        </td>
                        </tr>
                         <tr>
                             <td colspan="2" style="width: 80px; font-size: 10px">15 คะแนน</td>
                             <td colspan="2" style="width: 80px; font-size: 10px">0 คะแนน</td>

                         </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="15" v-model="items[field.rd1_2_3_1].v" />
                            </td>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="0" v-model="items[field.rd1_2_3_1].v" />
                            </td>

                        </tr>

                        <tr role="row">
                            <td>3</td>
                            <td colspan="8">ด้านระยะเวลาในการดำเนินโครงการ</td>
                        </tr>

                        <tr>
                            <td rowspan="3"></td>
                            <td rowspan="3">3.1 ระยะเวลาในการดำเนินโครงการ
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">5    
                            </td>
                            <td style="width: 80px; font-size: 10px">100%</td>
                            <td style="width: 80px; font-size: 10px">99-80%</td>
                            <td style="width: 80px; font-size: 10px">79-60%</td>
                            <td style="width: 80px; font-size: 10px">น้อยกว่า 60%</td>
                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_3_1_2].v" />ใบลงทะเบียน<br />
                                <input type="checkbox" value="2" v-model="items[field.mcb1_3_1_2].v" />กำหนดการ<br />
                                <input type="checkbox" value="3" v-model="items[field.mcb1_3_1_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_3_1_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_3_1_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; font-size: 10px">5 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">3 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">2 คะแนน</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="5" v-model="items[field.rd1_3_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="4" v-model="items[field.rd1_3_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="3" v-model="items[field.rd1_3_1_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="2" v-model="items[field.rd1_3_1_1].v" />
                            </td>

                        </tr>
                        <tr>
                            <td rowspan="3"></td>
                            <td rowspan="3">3.2 ระยะเวลาในการจัดส่งรายงานผล
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">10    
                            </td>
                            <td colspan="2" style="width: 80px; font-size: 10px">30 วัน</td>
                            <td colspan="2" style="width: 80px; font-size: 10px">มากกว่า 30 วัน</td>
                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_3_2_2].v" />เล่มรายงานผลการปฏิบัติงานและค่าใช้จ่าย<br />
                                <input type="checkbox" value="2" v-model="items[field.mcb1_3_2_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_3_2_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_3_2_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td colspan="2" style="width: 80px; font-size: 10px">0 คะแนน</td>

                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="10" v-model="items[field.rd1_3_2_1].v" />
                            </td>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="0" v-model="items[field.rd1_3_2_1].v" />
                            </td>
                            <%--                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="3" v-model="items[field.rd1_3_2_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="2" v-model="items[field.rd1_3_2_1].v" />
                            </td>--%>
                        </tr>

                        <tr>
                            <td rowspan="3">4
                            </td>
                            <td rowspan="3">ด้านงบประมาณ
                            </td>

                            <td rowspan="3" style="text-align: center; vertical-align: central">10   
                            </td>
                            <td colspan="2" style="width: 80px; font-size: 10px">มีครบถ้วน</td>
                            <td colspan="2" style="width: 80px; font-size: 10px">ไม่ครบถ้วน</td>

                            <td rowspan="3">
                                <input type="checkbox" value="1" v-model="items[field.mcb1_4_1_2].v" />แบบรายงานผล<br />
                                <input type="checkbox" value="2" v-model="items[field.mcb1_4_1_2].v" />แบบสรุปค่าใช้จ่าย<br />
                                <input type="checkbox" value="3" v-model="items[field.mcb1_4_1_2].v" />เอกสารการเบิกจ่าย/ใบเสร็จรับเงิน<br />
                                <input type="checkbox" value="4" v-model="items[field.mcb1_4_1_2].v" />อื่นๆ 
                                        <input type="text" v-model="items[field.tb1_4_1_3].v" />
                            </td>
                            <td rowspan="3">{{items[field.rd1_4_1_1].v}}
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 80px; font-size: 10px">5 คะแนน</td>
                            <td colspan="2" style="width: 80px; font-size: 10px">0 คะแนน</td>

                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="5" v-model="items[field.rd1_4_1_1].v" />
                            </td>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="0" v-model="items[field.rd1_4_1_1].v" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="8" style="text-align: right; vertical-align: central" role="gridcell">ผลรวมการประเมินประสิทธิผลโครงการ ส่วนที่ 2
                            </td>
                            <td colspan="8" style="text-align: right; vertical-align: central" role="gridcell">{{items[field.lbTotal1].v}}
                            </td>
                        </tr>
                    </table>


                    <div class="form-group form-group-sm">
                        <br />
                        <label class="col-sm-12 form-group-title">ส่วนที่ 3 การประเมินประสิทธิผลของโครงการ</label>
                    </div>

                    <table style="width: 100%; border-style: solid; border-color: #ccc; border-width: 1px;" class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width: 20px">ลำดับที่</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header">รายการประเมิน</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width: 20px">น้ำหนัก</th>
                                <th colspan="5" scope="col" role="columnheader" class="k-header">เกณฑ์</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header">คะแนน</th>

                            </tr>
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width: 75px">100%</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 75px">99-80%</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 75px">79-60%</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 75px">69-40%</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 75px">น้อยกว่า 40%</th>
                            </tr>
                        </thead>
                        <tr role="row">
                            <td>1</td>
                            <td colspan="8">ด้านการเปรียบเทียบวัตถุประสงค์</td>
                        </tr>

                        <tr>
                            <td rowspan="2" colspan="2">1.1 วัตถุประสงค์สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และกองทุนฯ
                            </td>

                            <td rowspan="2" style="text-align: center; vertical-align: central">10    
                            </td>
                            <td style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">8 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">2 คะแนน</td>

                            <td rowspan="2">{{items[field.rd2_1_1_1].v}}
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="10" v-model="items[field.rd2_1_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="8" v-model="items[field.rd2_1_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="6" v-model="items[field.rd2_1_1_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="4" v-model="items[field.rd2_1_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="2" v-model="items[field.rd2_1_1_1].v" />
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td rowspan="2" colspan="2">1.2 ผลการดำเนินงานบรรลุวัตถุประสงค์ที่กำหนด
                            </td>

                            <td rowspan="2" style="text-align: center; vertical-align: central">10    
                            </td>
                            <td style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">8 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">2 คะแนน</td>

                            <td rowspan="2">{{items[field.rd2_1_2_1].v}}
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="10" v-model="items[field.rd2_1_2_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="8" v-model="items[field.rd2_1_2_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="6" v-model="items[field.rd2_1_2_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="4" v-model="items[field.rd2_1_2_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="2" v-model="items[field.rd2_1_2_1].v" />
                            </td>
                        </tr>
                        <tr>

                        <tr role="row">
                            <td>2</td>
                            <td colspan="8">ด้านผลลัพธ์หรือความสำเร็จ</td>
                        </tr>

                        <tr>
                            <td rowspan="2" colspan="2">"2.1 ผลการดำเนินงานบรรลุตัวชี้วัดที่กำหนดไว้
                            </td>

                            <td rowspan="2" style="text-align: center; vertical-align: central">10 
                            </td>
                            <td style="width: 80px; font-size: 10px">10 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">8 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">6 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">4 คะแนน</td>
                            <td style="width: 80px; font-size: 10px">2 คะแนน</td>

                            <td rowspan="2">{{items[field.rd2_2_1_1].v}}
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <%--<input @click="$CalulateFollowUnder5M()" type="radio" name="R_1_3" value="20" v-model="items[field.rd1_1_1].v"  />--%>
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="10" v-model="items[field.rd2_2_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="8" v-model="items[field.rd2_2_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="6" v-model="items[field.rd2_2_1_1].v" />

                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="4" v-model="items[field.rd2_2_1_1].v" />
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">
                                <input v-on:change="$CalulateFollowUnder5M()" type="radio" value="2" v-model="items[field.rd2_2_1_1].v" />
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td colspan="8" style="text-align: right; vertical-align: central" role="gridcell">ผลรวมการประเมินประสิทธิผลโครงการ ส่วนที่ 3
                            </td>
                            <td colspan="8" style="text-align: right; vertical-align: central" role="gridcell">{{items[field.lbTotal2].v}}
                            </td>
                        </tr>
                    </table>

                    <div class="form-group form-group-sm">
                        <br />
                        <label class="col-sm-12 form-group-title">รวมคะแนน </label>
                    </div>

                    <table style="width: 100%; border-style: solid; border-color: #ccc; border-width: 1px;" class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width: 20px">ส่วนที่</th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 120px">คะแนนเต็ม</th>
                                <th scope="col" role="columnheader" class="k-header">คะแนนที่ได้</th>
                            </tr>

                        </thead>

                        <tr role="row">
                            <td>2
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">70
                            </td>

                            <td style="text-align: center; vertical-align: central" role="gridcell">{{items[field.lbTotal1].v}}
                            </td>
                        </tr>
                        <tr role="row">
                            <td>3
                            </td>
                            <td style="text-align: center; vertical-align: central" role="gridcell">30
                            </td>

                            <td style="text-align: center; vertical-align: central" role="gridcell">{{items[field.lbTotal2].v}}
                            </td>
                        </tr>
                        <tr role="row">

                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">คะแนนรวม
                            </td>

                            <td style="text-align: center; vertical-align: central" role="gridcell">{{items[field.lbGrandTotal].v}}
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; vertical-align: central" role="gridcell">โครงการอยู่ในระดับ/ความหมาย
                            </td>

                            <td style="text-align: left; vertical-align: central" role="gridcell">

                                <span v-html="items[field.lbResultText].v"></span>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; border-style: solid; border-color: #ccc; border-width: 1px;" class="k-grid">
                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width: 20%; font-size: 11px">ถ้าคะแนนรวม ได้น้อยกว่า 20 คะแนน<br />
                                    โครงการระดับ E (ปรับปรุง)	
                                </th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 20%; font-size: 11px">ถ้าคะแนนรวมได้ 20 - 39 คะแนน<br />
                                    โครงการระดับ D (พอใช้)				
                                </th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 20%; font-size: 11px">ถ้าคะแนนรวมได้ 40 - 59 คะแนน<br />
                                    โครงการระดับ C (ปานกลาง)		
                                </th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 20%; font-size: 11px">ถ้าคะแนนรวมได้ 60 - 79 คะแนน<br />
                                    โครงการระดับ B (ดี)		
                                </th>
                                <th scope="col" role="columnheader" class="k-header" style="width: 20%; font-size: 11px">ถ้าคะแนนรวมได้ 80 - 100 คะแนน<br />
                                    โครงการระดับ A (ดีมาก)					
                                </th>
                            </tr>
                            <tr>
                                <td style="font-size: 11px">
                                โครงการไม่ประสบความสำเร็จมีสิ่งที่ควรปรับปรุงแก้ไขโครงการไม่มีความสอดคล้องกับคนพิการไม่สามารถแก้ไขปัญหาและไม่ก่อให้เกิดประโยชน์ต่อคนพิการอย่างแท้จริงวัตถุประสงค์ไม่สอดคล้องกับการดำเนินกิจกรรรมหรือตัวชี้วัดทำให้โครงการไม่บรรลุผลตามเป้าหมายที่วางไว้	
                               <td style="font-size: 11px">โครงการประสบความสำเร็จประมาณ 20 - 39% โครงการมีความสอดคล้องแต่ยังไม่สามารถแก้ไขปัญหาของคนพิการที่เข้าร่วมโครงการได้รวมทั้งไม่สามารถทำให้คนพิการเพิ่มประสิทธิภาพและประสิทธิผลในตนเองได้อีกทั้งโครงการสามารถเข้าถึงคนพิการได้เพียงเล็กน้อยวัตถุประสงค์ไม่ตรงกับกลุ่มเป้าหมายที่ตั้งไว้ 				
                               </td>
                                <td style="font-size: 11px">โครงการประสบความสำเร็จประมาณ 40 - 59%  โครงการมีความสอดคล้องกับคนพิการสามารถแก้ไขปัญหาเบื้องต้นให้กับคนพิการได้ไม่ก่อให้เกิดประโยชน์ต่อคนพิการวัตถุประสงค์บางประการไม่สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และ กองทุน อีกทั้งระยะเวลาในการดำเนินโครงการยังไม่มีความเหมาะสม 		
                                </td>
                                <td style="font-size: 11px">โครงการประสบความสำเร็จประมาณ 60 - 79% สอดคล้องกับเกณฑ์ทั่วไป เกณฑ์เฉพาะประสิทธิภาพแล ประสิทธิผลของโครงการ แต่ยังต้องมีการพัฒนาและปรับปรุงในบางประเด็น เป็นโครงการที่ก่อให้เกิดการพัฒนาต่อกลุ่มคนพิการมีการจัดสรรทรัพยากรตามแผนงานก่อให้เกิด ประโยชน์ต่อกลุ่มคนพิการและสามารถตอบสนองต่อปัญหาของคนพิการไปใช้ให้เกิดประโยชน์ได้
                                </td>
                                <td style="font-size: 11px">โครงการประสบความสำเร็จสอดคล้องกับเกณฑ์ทั่วไปเกณฑ์เฉพาะประสิทธิภาพและประสิทธิผลของโครงการวัตถุประสงค์สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และกองทุน มีการจัดสรรทรัพยากร ที่ได้รับอย่างคุ้มค่าและเหมาะสมสามารถตอบสนองต่อปัญหาของคนพิการได้อย่างเป็นรูปธรรมและยั่งยืนคนพิการสามารถพึ่งตนเองได้ในระยะยาวอีกทั้งโครงการทำให้เกิดการเปลี่ยนแปลงในระดับท้องถิ่งและระดับประเทศ 					

                                </td>
                            </tr>

                        </thead>
                    </table>
                    <br />
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                            <label class="form-group-title">ข้อคิดเห็น</label>
                            <textarea style="width: 100%" rows="6" v-model="items[field.tbComment].v"></textarea>
                        </div>
                        <div class="col-sm-6">
                            <label class="form-group-title">ข้อควรพัฒนา</label><br />
                            <textarea style="width: 100%" rows="6" v-model="items[field.tbSuggestion].v"></textarea>
                        </div>
                    </div>
                    <!--form-horizontal-->

                </div>
                <br />
                <div class="form-group form-group-sm">
                    <div class="container" style="font-size: 14px; padding-top">
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
                                <input type="text" v-model="items[field.tbPosition2].v" />
                            </div>

                            <div class="col-sm-1">
                                ตำแหน่ง  
                            </div>
                            <div class="col-sm-3">
                                <input type="text" v-model="items[field.tbPosition3].v" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 5px">
                            <div class="col-sm-4" style="text-align: center">
                                เจ้าหน้าที่ประเมินผล
                            </div>
                            <div class="col-sm-4" style="text-align: center">
                                เจ้าหน้าที่ประเมินผล
                            </div>
                            <div class="col-sm-4" style="text-align: center">
                                เจ้าหน้าที่ประเมินผล
                            </div>
                        </div>
                    </div>


                </div>
                <!--panel แบบรายงานผลการปฏิบัติงาน-->
            </div>
        </div>
        <div class="form-group form-group-sm">
            <br />
            <label class="col-sm-2 control-label control-label-left without-delimit">
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
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="appVueQN.param.IsReported = '0';appVueQN.saveData();SaveAttachmentFiles(); return false;" Visible="false" />

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
                        Text="พิมพ์" OnClientClick="printDiv('divVueQN'); return false;" />
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm"
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>

