<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabFollowUnder5MControlOld.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabFollowUnder5MControlOld"  %>
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
                <h3 class="panel-title">การติดตามประเมินผลโครงการ (งบประมาณต่ำกว่า 5 ล้านบาท)<e/h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                   <%--<span class="field-desc">--%>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ส่วนที่ 1 ประเมิณก่อนการดำเนินโครงการ</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-2">
                             1. เพศ
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                         
                        <div class="col-sm-2">
                            <%--<asp:RadioButton ID="R_1_1_1" runat="server" GroupName="R_1_1" value="1" data-bind="checked: R_1_1" />1. ชาย--%>
                            <input type="radio" name="R_1_1" value="1" data-bind="checked: R_1_1" />1. ชาย
                        </div>
                        <div class="col-sm-2">
                            <%--<asp:RadioButton ID="R_1_1_2" runat="server" GroupName="R_1_1" value="2"  data-bind="checked: R_1_1" />2. หญิง--%>
                            <input type="radio" name="R_1_1" value="2" data-bind="checked: R_1_1" />2. หญิง
                        </div>

                    </div>

                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                             2. อายุ 
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                         
                        <div class="col-sm-2">
                            <input type="radio" name="R_1_2" value="1" data-bind="checked: R_1_2" />1. ต่ำกว่า 10 ปี
                        </div>
                        <div class="col-sm-2">
                            <input type="radio" name="R_1_2" value="2" data-bind="checked: R_1_2" />2. 11-20 ปี
                        </div>
                        <div class="col-sm-2">
                            <input type="radio" name="R_1_2" value="3" data-bind="checked: R_1_2" />3. 21-30 ปี
                        </div>
                        <div class="col-sm-2">
                            <input type="radio" name="R_1_2" value="4" data-bind="checked: R_1_2" />4. 31-50 ปี
                        </div>
                        <div class="col-sm-2">
                            <input type="radio" name="R_1_2" value="5" data-bind="checked: R_1_2" />5. มากกว่า 50 ปี
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                             3. หน้าที่รับผิดชอบ
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                              <input type="radio" name="R_1_3" value="1" data-bind="checked: R_1_3"  />1. หัวหน้าโครงการคนพิการ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="2" data-bind="checked: R_1_3"  />2. เจ้าหน้าที่โครงการ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="3" data-bind="checked: R_1_3"  />3. อาสาสมัคร
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="4" data-bind="checked: R_1_3"  />4. วิทยากร
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="5" data-bind="checked: R_1_3"  />5. คณะกรรมการโครงการ
                        </div>
                        <div class="col-sm-6"> 
                             <input type="radio" name="R_1_3" value="6" data-bind="checked: R_1_3"  />6. เจ้าหน้าที่ พน. หรือ ข้าราชการในหน่วยงานที่เกี่ยวข้อง
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                             4. ภูมิภาค
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                              <input type="radio" name="R_1_4" value="1" data-bind="checked: R_1_4"  />1. ภาคกลาง
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_4" value="2" data-bind="checked: R_1_4"  />2. ภาคใต้
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_4" value="3" data-bind="checked: R_1_4"  />3. ภาคตะวันออกเฉียงเหนือ / ภาคอีสาน
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_4" value="4" data-bind="checked: R_1_4"  />4. ภาคเหนือ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_4" value="5" data-bind="checked: R_1_4"  />5. กรุงเทพมหานคร
                        </div>
 
                    </div>
                    <p></p>

    
            <%--        <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่ </label>
                    </div>--%>
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
                             <td colspan="6" style="background-color:lightslategray;font-weight:bold">ประเด็นทั่วไป</td>
                         </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">1. ความสอดคล้อง</td>
                         </tr>
                                <tr  role="row">
                                    <td role="gridcell">
                                        1.1 โครงการมีความสอดคล้องกับนโยบายและแผนการพัฒนาในระดับท้องถินและระดับประเทศ
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_1" value="1" data-bind="checked: R_2_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_1" value="2" data-bind="checked: R_2_1_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_1" value="3" data-bind="checked: R_2_1_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_1" value="4" data-bind="checked: R_2_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_1" value="5" data-bind="checked: R_2_1_1"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.2 โครงการสามารถตอบสนองความต้องการ และ แก้ไขปัญหาของกลุ่มเป้าหมายได้    
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_2" value="1" data-bind="checked: R_2_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_2" value="2" data-bind="checked: R_2_1_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_2" value="3" data-bind="checked: R_2_1_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_2" value="4" data-bind="checked: R_2_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_2" value="5" data-bind="checked: R_2_1_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.3 กลุ่มเป้าหมายตระหนักถึงความสำคัญของโครงการในมิติด้านการพัฒนาและแก้ไขปัญหาที่กำลังเผชิญอยู่ 

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_3" value="1" data-bind="checked: R_2_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_3" value="2" data-bind="checked: R_2_1_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_3" value="3" data-bind="checked: R_2_1_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_3" value="4" data-bind="checked: R_2_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_1_3" value="5" data-bind="checked: R_2_1_3"  />
                                    </td> 
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">2. ประสิทธิภาพ</td>
                         </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.1 วัตถุประสงค์ของโครงการมีแนวโน้มที่จะประสบความสำเร็จ
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_1" value="1" data-bind="checked: R_2_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_1" value="2" data-bind="checked: R_2_2_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_1" value="3" data-bind="checked: R_2_2_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_1" value="4" data-bind="checked: R_2_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_1" value="5" data-bind="checked: R_2_2_1"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.2 โครงการก่อให้เกิดการพัฒนาอย่างยั่งยืนต่อกลุ่มเป้าหมาย

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_2" value="1" data-bind="checked: R_2_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_2" value="2" data-bind="checked: R_2_2_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_2" value="3" data-bind="checked: R_2_2_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_2" value="4" data-bind="checked: R_2_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_2" value="5" data-bind="checked: R_2_2_2"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.3 โครงการก่อให้เกิดการพัฒนาที่นำไปสู่การเปลี่ยนแปลงนโยบายทั้งในระดับท้องถิ่นและระดับประเทศ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_3" value="1" data-bind="checked: R_2_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_3" value="2" data-bind="checked: R_2_2_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_3" value="3" data-bind="checked: R_2_2_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_3" value="4" data-bind="checked: R_2_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_2_3" value="5" data-bind="checked: R_2_2_3"  />
                                    </td>   
                                </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">3. ประสิทธิผล</td>
                         </tr>                          
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       3.1 ผลลัพธ์ของโครงการที่มีความเหมาะสมกับทรัพยากรที่ใช้ไป
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_1" value="1" data-bind="checked: R_2_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_1" value="2" data-bind="checked: R_2_3_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_1" value="3" data-bind="checked: R_2_3_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_1" value="4" data-bind="checked: R_2_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_1" value="5" data-bind="checked: R_2_3_1"  />
                                    </td>   
                     
 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.2 โครงการมีการจัดสรรทรัพยากรอย่างเป็นระบบ

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_2" value="1" data-bind="checked: R_2_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_2" value="2" data-bind="checked: R_2_3_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_2" value="3" data-bind="checked: R_2_3_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_2" value="4" data-bind="checked: R_2_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_2" value="5" data-bind="checked: R_2_3_2"  />
                                    </td>   
                                </tr>                           
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.3 โครงการมีวางแผนการดำเนินงานโดยใช้ทรัพยากรที่มีอยู่อย่างจำกัดให้ก่อเกิดประโยชน์สูงสุด
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_3" value="1" data-bind="checked: R_2_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_3" value="2" data-bind="checked: R_2_3_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_3" value="3" data-bind="checked: R_2_3_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_3" value="4" data-bind="checked: R_2_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_3_3" value="5" data-bind="checked: R_2_3_3"  />
                                    </td>    
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">4. ผลกระทบ</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        4.1 ผลลัพธืของโครงการมีผลกระทบต่อบริบททางสังคม เศรษฐกิจ การเมือง และ วัฒนธรรม
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
                                       4.2 ผลลัพธ์ของโครงการเป็นที่ประจักษ์ในระดับสากล

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
                                        4.3 การดำเนินโครงการนำไปสู่ผลกระทบในวงกว้าง โดยมีการปรับเปลี่ยนให้เหมาะสมกับกลุ่มผู้มีส่วนได้ส่วนเสีย

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
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">5. ความยั่งยืน</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.1 กิจกรรม ผลลัพธ์ และ ผลกระทบ จากการดำเนินโครงการยังคงดำเนินต่อไปแม้โครงการสิ้นสุดลงแล้ว
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_1" value="1" data-bind="checked: R_2_5_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_1" value="2" data-bind="checked: R_2_5_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_1" value="3" data-bind="checked: R_2_5_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_1" value="4" data-bind="checked: R_2_5_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_1" value="5" data-bind="checked: R_2_5_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       5.2 การดำเนินโครงการคำนึงถึงปัจจัยด้านความยั่งยืนทั้งทางเศรษฐกิจ สิ่งแวดล้อม สังคม และ วัฒนธรรม

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_2" value="1" data-bind="checked: R_2_5_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_2" value="2" data-bind="checked: R_2_5_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_2" value="3" data-bind="checked: R_2_5_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_2" value="4" data-bind="checked: R_2_5_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_2" value="5" data-bind="checked: R_2_5_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        5.3 โครงการมีส่วนสนับสนุให้กลุ่มเป้าหมายสามารถพึ่งตนเองได้

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_3" value="1" data-bind="checked: R_2_5_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_3" value="2" data-bind="checked: R_2_5_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_3" value="3" data-bind="checked: R_2_5_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_3" value="4" data-bind="checked: R_2_5_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_5_3" value="5" data-bind="checked: R_2_5_3"  />
                                    </td> 
                                </tr>    
                         <!-- เกณฑ์เฉพาะ -->
                         <tr>
                             <td colspan="6" style="background-color:lightslategray;font-weight:bold">เกณฑ์เฉพาะ</td>
                         </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">6. ความเหมาะสม</td>
                         </tr>
                                <tr  role="row">
                                    <td role="gridcell">
                                        6.1 โครงการให้ความสำคัญถึงความแตกต่างของกลุ่มเป้าหมาย เช่น เพศ ความพิการ ฯลฯ เพื่อประโยชน์ในการจัดกิจกรรมต่างๆ ได้อย่างเหมาะสม
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_1" value="1" data-bind="checked: R_2_6_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_1" value="2" data-bind="checked: R_2_6_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_1" value="3" data-bind="checked: R_2_6_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_1" value="4" data-bind="checked: R_2_6_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_1" value="5" data-bind="checked: R_2_6_1"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.2 การดำเนินโครงการพิจารณาถึงความสามารถและศักยภาพของกลุ่มเป้าหมาย
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_2" value="1" data-bind="checked: R_2_6_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_2" value="2" data-bind="checked: R_2_6_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_2" value="3" data-bind="checked: R_2_6_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_2" value="4" data-bind="checked: R_2_6_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_2" value="5" data-bind="checked: R_2_6_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        6.3 การดำเนินโครงการ และ กิจจกรรมของโครงการมีความเหมาะสม และ สอดคล้องกับความต้องการของกลุ่มเป้าหมาย

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_3" value="1" data-bind="checked: R_2_6_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_3" value="2" data-bind="checked: R_2_6_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_3" value="3" data-bind="checked: R_2_6_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_3" value="4" data-bind="checked: R_2_6_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_6_3" value="5" data-bind="checked: R_2_6_3"  />
                                    </td> 
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">7. ความสมเหตุสมผล</td>
                         </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.1 การดำเนินโครงการสามารถตอบสนองต่อนโยบายทางสังคมได้อย่างเหมาะสม
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_1" value="1" data-bind="checked: R_2_7_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_1" value="2" data-bind="checked: R_2_7_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_1" value="3" data-bind="checked: R_2_7_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_1" value="4" data-bind="checked: R_2_7_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_1" value="5" data-bind="checked: R_2_7_1"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.2 การดำเนินโครงการในทุกขึ้นตอนสมเหตุสมผลตามสถานการณ์ในปัจจุบัน  

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_2" value="1" data-bind="checked: R_2_7_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_2" value="2" data-bind="checked: R_2_7_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_2" value="3" data-bind="checked: R_2_7_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_2" value="4" data-bind="checked: R_2_7_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_2" value="5" data-bind="checked: R_2_7_2"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        7.3 การดำเนินโครงการคำนึงถึงประเด็นด้านสิทธิมนุษยชน และ การพัฒนาในระดับดับสากล

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_3" value="1" data-bind="checked: R_2_7_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_3" value="2" data-bind="checked: R_2_7_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_3" value="3" data-bind="checked: R_2_7_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_3" value="4" data-bind="checked: R_2_7_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_7_3" value="5" data-bind="checked: R_2_7_3"  />
                                    </td>   
                                </tr>
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">8. การประสานงาน</td>
                         </tr>                          
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       8.1 โครงการมีระบบการประสานงานที่ดี เหมาะสมกับกลุ่มเปห้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสีย
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_1" value="1" data-bind="checked: R_2_8_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_1" value="2" data-bind="checked: R_2_8_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_1" value="3" data-bind="checked: R_2_8_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_1" value="4" data-bind="checked: R_2_8_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_1" value="5" data-bind="checked: R_2_8_1"  />
                                    </td>   
                     
 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        8.2 โครงการมีระบบตรวจสอบเมื่อเกิดปัญหาในการประสานงาน

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_2" value="1" data-bind="checked: R_2_8_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_2" value="2" data-bind="checked: R_2_8_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_2" value="3" data-bind="checked: R_2_8_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_2" value="4" data-bind="checked: R_2_8_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_2" value="5" data-bind="checked: R_2_8_2"  />
                                    </td>   
                                </tr>                           
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        8.3 โครงการผนวกกลุ่มผู้มีส่วนได้ส่วนเสียในทุกระดับและประสานงานกับทุกภาคส่วนอย่างทั่วถึง และ เท่าเทียม
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_3" value="1" data-bind="checked: R_2_8_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_3" value="2" data-bind="checked: R_2_8_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_3" value="3" data-bind="checked: R_2_8_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_3" value="4" data-bind="checked: R_2_8_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_8_3" value="5" data-bind="checked: R_2_8_3"  />
                                    </td>    
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">9. ความเชื่อมโยง</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        9.1 โครงการมีวัตถุประสงค์สอดคล้องกับการทำงานของภาครัฐ ทำให้เมื่อสิ้นสุดโครงการ หน่วยงานรัฐสามารถนำกิจกรรไปดำเนินงานต่อได้
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_1" value="1" data-bind="checked: R_2_9_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_1" value="2" data-bind="checked: R_2_9_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_1" value="3" data-bind="checked: R_2_9_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_1" value="4" data-bind="checked: R_2_9_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_1" value="5" data-bind="checked: R_2_9_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       9.2 โครงการมีการดำเนินงานที่สนับสนุนการมีส่วนร่วมกับภาคีเครือข่าย

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_2" value="1" data-bind="checked: R_2_9_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_2" value="2" data-bind="checked: R_2_9_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_2" value="3" data-bind="checked: R_2_9_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_2" value="4" data-bind="checked: R_2_9_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_2" value="5" data-bind="checked: R_2_9_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        9.3 โครงการสามารถต่อยอดการพัฒนา และ เสริมศักยภาพให้กับกลุ่มเป้าหมายได้

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_3" value="1" data-bind="checked: R_2_9_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_3" value="2" data-bind="checked: R_2_9_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_3" value="3" data-bind="checked: R_2_9_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_3" value="4" data-bind="checked: R_2_9_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_9_3" value="5" data-bind="checked: R_2_9_3"  />
                                    </td> 
                                </tr> 
                         <tr>
                             <td colspan="6" style="background-color:lightyellow;font-weight:bold">10. ความคลอบคลุม</td>
                         </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        10.1 โครงการได้รับการสนับสนุนจากผู้มีส่วนเกี่ยวข้องในทุกระดับ
                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_1" value="1" data-bind="checked: R_2_10_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_1" value="2" data-bind="checked: R_2_10_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_1" value="3" data-bind="checked: R_2_10_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_1" value="4" data-bind="checked: R_2_10_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_1" value="5" data-bind="checked: R_2_10_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       10.2 การดำเนินโครงการไม่ขัดต่อความต้องการของกลุ่มเป้าหมายกลุ่มใดกลุ่มหนึ่งโดยเฉพาะ

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_2" value="1" data-bind="checked: R_2_10_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_2" value="2" data-bind="checked: R_2_10_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_2" value="3" data-bind="checked: R_2_10_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_2" value="4" data-bind="checked: R_2_10_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_2" value="5" data-bind="checked: R_2_10_2"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        10.3 โครงการได้เปิดโอกาสให้กลุ่มเป้าหมายได้แสดงความคิดเห็นและตอบข้อสงสัยได้อย่างชัดเจน

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_3" value="1" data-bind="checked: R_2_10_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_3" value="2" data-bind="checked: R_2_10_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_3" value="3" data-bind="checked: R_2_10_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_3" value="4" data-bind="checked: R_2_10_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_2_10_3" value="5" data-bind="checked: R_2_10_3"  />
                                    </td> 
                                </tr>  
                                </table>
                 <%--   <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่  4  ภายหลังจบโครงการท่านจะสามารถนำไปดำเนินการต่อยอด หรือนำประโยชน์ไปใช้ได้อย่างไร (โปรดระบุ)</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4" data-bind="value: T_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
                            <asp:RequiredFieldValidator ID="RequiredT_4" ControlToValidate="T_4" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                />                   
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่  5  ข้อคิดเห็นและข้อเสนอแนะ</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <div class="required-block">
                                <nep:TextBox ID="T_5" data-bind="value: T_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
                        <asp:RequiredFieldValidator ID="RequiredT_5" ControlToValidate="T_5" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                />               
                        </div>
                    </div>--%>

         <%--           <div class="form-group form-group-sm">
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


                    <!--ข้อคิดเห็นและข้อเสนอแนะ-->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectReportResult_Suggestion %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <nep:TextBox ID="TextBoxSuggestion" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                        </div>
                    </div>--%>
             <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label control-label-left without-delimit">
                        แนบไฟล์
                </label>
                <div class="col-sm-8">    
                    <nep:C2XFileUpload runat="server" ID="FileUploadAttachment" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
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

