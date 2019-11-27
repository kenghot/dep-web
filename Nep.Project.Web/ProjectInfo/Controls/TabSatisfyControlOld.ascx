<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabSatisfyControlOld.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabSatisfyControlOld"  %>
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
                <h3 class="panel-title">แบบประเมินความพึงพอใจ</h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                    <span class="field-desc">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่ 1 ข้อมูลทั่วไป</label>
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
                             <%--2. อายุ <asp:TextBox runat="server" ID="T_1_2" data-bind="kendoNumericTextBox: T_1_2" >0</asp:TextBox> ปี--%>
                            2. อายุ <input  id="T_1_2"   data-bind="kendoNumericTextBox: T_1_2" / > 
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                             3. สถานะผู้เข้าร่วมการประชุม
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                              <input type="radio" name="R_1_3" value="1" data-bind="checked: R_1_3"  />1. คนพิการ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="2" data-bind="checked: R_1_3"  />2. ผู้ปกครอง/ผู้ดูแลคนพิการ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="3" data-bind="checked: R_1_3"  />3. ข้าราชการ/พนักงาน/เจ้าหน้าที่รัฐ
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="4" data-bind="checked: R_1_3"  />4. บุคลากรภาคเอกชน
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="5" data-bind="checked: R_1_3"  />5. ประชาชนทั่วไป
                        </div>
                        <div class="col-sm-6"> 
                             <input type="radio" name="R_1_3" value="6" data-bind="checked: R_1_3"  />6. อื่นๆ ระบุ <asp:TextBox runat="server" ID="T_1_3_6" data-bind="value: T_1_3_6" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <p></p>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่ 2 โครงการนี้ได้รับการสนับสนุนเงินจากแหล่งใด</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                             <input type="radio" name="R_2" value="1" data-bind="checked: R_2"  />1. กองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                             <input type="radio" name="R_2" value="2" data-bind="checked: R_2"  />2. อื่นๆ ระบุ <asp:TextBox runat="server" ID="T_2_2" MaxLength="50" Width="150" data-bind="value: T_2_2"></asp:TextBox>
                        </div>
                    </div>
              
                    <p></p>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ตอนที่ 3 ความคิดเห็นของผู้เข้าร่วมโครงการ</label>
                    </div>
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
                                <th colspan="5" scope="col" role="columnheader" class="k-header" >ระบุ ในโครงการ</th>
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
                                <tr  role="row">
                                     
                                    <td role="gridcell">
                                        <b>1.  ด้านวิทยากร</b><br />
                                        1.1  ความรอบรู้ในเนื้อหาวิชา และสามารถถ่ายทอดให้มีความเข้าใจในเรื่องดังกล่าว

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_1" value="5" data-bind="checked: R_3_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_1" value="4" data-bind="checked: R_3_1_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_1" value="3" data-bind="checked: R_3_1_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_1" value="2" data-bind="checked: R_3_1_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_1" value="1" data-bind="checked: R_3_1_1"  />
                                    </td>   
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.2  ความสามารถในการจัดลำดับความสัมพันธ์ของเนื้อหาที่สอดคล้องต่อเนื่อง   
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_2" value="5" data-bind="checked: R_3_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_2" value="4" data-bind="checked: R_3_1_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_2" value="3" data-bind="checked: R_3_1_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_2" value="2" data-bind="checked: R_3_1_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_2" value="1" data-bind="checked: R_3_1_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.3  การบรรยายเสียงดังฟังชัด ได้สาระใจความ

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_3" value="5" data-bind="checked: R_3_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_3" value="4" data-bind="checked: R_3_1_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_3" value="3" data-bind="checked: R_3_1_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_3" value="2" data-bind="checked: R_3_1_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_3" value="1" data-bind="checked: R_3_1_3"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.4  การเปิดโอกาสให้ซักถาม/ ตอบคำถาม

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_4" value="5" data-bind="checked: R_3_1_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_4" value="4" data-bind="checked: R_3_1_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_4" value="3" data-bind="checked: R_3_1_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_4" value="2" data-bind="checked: R_3_1_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_4" value="1" data-bind="checked: R_3_1_4"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        1.5  การเตรียมความพร้อมของวิทยากร

                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_5" value="5" data-bind="checked: R_3_1_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_5" value="4" data-bind="checked: R_3_1_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_5" value="3" data-bind="checked: R_3_1_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_5" value="2" data-bind="checked: R_3_1_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_1_5" value="1" data-bind="checked: R_3_1_5"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        <b>2.  ด้านเนื้อหาสาระ และการนำไปใช้ประโยชน์</b><br />
                                        2.1 ได้รับความรู้ความเข้าใจเกี่ยวกับการจัดทำโครงการ  

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_1" value="5" data-bind="checked: R_3_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_1" value="4" data-bind="checked: R_3_2_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_1" value="3" data-bind="checked: R_3_2_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_1" value="2" data-bind="checked: R_3_2_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_1" value="1" data-bind="checked: R_3_2_1"  />
                                    </td>   
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       2.2  สามารถนำความรู้ที่ได้รับนำไปปรับใช้ได้อย่างมีประสิทธิภาพและเกิดประสิทธิผลต่อตนเองชุมชน สังคมหรือองค์กร

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_2" value="5" data-bind="checked: R_3_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_2" value="4" data-bind="checked: R_3_2_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_2" value="3" data-bind="checked: R_3_2_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_2" value="2" data-bind="checked: R_3_2_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_2" value="1" data-bind="checked: R_3_2_2"  />
                                    </td>   
                     
 
                                </tr>
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        2.3  ท่านเห็นความสำคัญของการจัดทำโครงการ 

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_3" value="5" data-bind="checked: R_3_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_3" value="4" data-bind="checked: R_3_2_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_3" value="3" data-bind="checked: R_3_2_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_3" value="2" data-bind="checked: R_3_2_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_2_3" value="1" data-bind="checked: R_3_2_3"  />
                                    </td>   
                                </tr>                           
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        <b>3.  ด้านการบริหารจัดการ</b><br />
                                        3.1  การต้อนรับ การอำนวยความสะดวก และการให้บริการ
                                     </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_1" value="5" data-bind="checked: R_3_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_1" value="4" data-bind="checked: R_3_3_1"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_1" value="3" data-bind="checked: R_3_3_1"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_1" value="2" data-bind="checked: R_3_3_1"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_1" value="1" data-bind="checked: R_3_3_1"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.2  ความเหมาะสมของสื่อ เครื่องมือโสตทัศนูปกรณ์ และเอกสาร

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_2" value="5" data-bind="checked: R_3_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_2" value="4" data-bind="checked: R_3_3_2"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_2" value="3" data-bind="checked: R_3_3_2"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_2" value="2" data-bind="checked: R_3_3_2"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_2" value="1" data-bind="checked: R_3_3_2"  />
                                    </td>    
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                       3.3  ความเหมาะสมของระยะเวลาในการประชุม

                                    </td>

                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_3" value="5" data-bind="checked: R_3_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_3" value="4" data-bind="checked: R_3_3_3"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_3" value="3" data-bind="checked: R_3_3_3"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_3" value="2" data-bind="checked: R_3_3_3"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_3" value="1" data-bind="checked: R_3_3_3"  />
                                    </td>  
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.4  ความเหมาะสมของสถานที่ / อาหาร / อาหารว่างและเครื่องดื่ม

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_4" value="5" data-bind="checked: R_3_3_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_4" value="4" data-bind="checked: R_3_3_4"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_4" value="3" data-bind="checked: R_3_3_4"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_4" value="2" data-bind="checked: R_3_3_4"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_4" value="1" data-bind="checked: R_3_3_4"  />
                                    </td> 
                                </tr> 
                                <tr  role="row">
                                     
                                    <td  role="gridcell">
                                        3.5  โดยภาพรวมท่านพึงพอใจต่อการจัดทำโครงการครั้งนี้

                                    </td>
                                      <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_5" value="5" data-bind="checked: R_3_3_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_5" value="4" data-bind="checked: R_3_3_5"  />
                                    </td>
                                    <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_5" value="3" data-bind="checked: R_3_3_5"  />
                                    </td>           
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_5" value="2" data-bind="checked: R_3_3_5"  />
                                     
                                    </td>
                                     <td  style="text-align:center;vertical-align:central" role="gridcell">
                                        <input type="radio" name="R_3_3_5" value="1" data-bind="checked: R_3_3_5"  />
                                    </td>   
                                </tr> 
                                </table>
                    <div class="form-group form-group-sm">
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
 <%--                           <asp:RequiredFieldValidator ID="RequiredT_5" ControlToValidate="T_5" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                /> --%>                  
                        </div>
                    </div>
                            <div class="form-group form-group-lg">
                <div class="col-sm-12 text-center" style="font-size:larger">
                    <hr />
                    รวมคะแนน :  <span id="T_TotalScore" data-bind="text: T_TotalScore" >0.00</span> คะแนน   <br />
                    คะแนนเฉลี่ย :  <span id="T_TotalAverage" data-bind="text: T_TotalAverage" >0.00</span> คะแนน <br />
                </div>
        </div>
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
                    
                </div><!--form-horizontal-->
            </div>
         </div><!--panel แบบรายงานผลการปฏิบัติงาน-->
       

        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>
                    <%--<asp:Button runat="server" ID="Button1" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>--%>
                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" 
                        Text="บันทึกและส่งแบบประเมินความพึงพอใจ"
                        OnClientClick="if (confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?')) return GetQNModelToServer(); else return false;"
                        OnClick="ButtonSaveReportResult_Click" />
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
        <div style="display:none">
       <!-- ko text: $(document).ready( function() { $('input[type=radio]').change(function() {
                SastisfySumScore(this);

               });
            }); -->
        <!-- /ko -->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

