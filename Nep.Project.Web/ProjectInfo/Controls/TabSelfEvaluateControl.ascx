<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabSelfEvaluateControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabSelfEvaluateControl"  %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelReportResult" UpdateMode="Conditional" runat="server">
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

            } .button-add-participant[disabled="disabled"]:hover, .button-clear-participant[disabled="disabled"]:hover {
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
                <h3 class="panel-title">การประเมินตนเอง
ของโครงการที่ได้รับทุนสนับสนุนจากกองทุนส่งเสริม และพัฒนาคุณภาพชีวิตคนพิการ
                </h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal" style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">สถานภาพของโครงการ</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3"><%= Model.ProjectInfo_OrganizationName %>/องค์กร  :</div>
                        <div class="col-sm-9">
                            <asp:Label ID="LabelOrganizationName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3"><%= Model.ProjectInfo_Name %>  :</div>
                        <div class="col-sm-9">
                            <asp:Label ID="LabelProjectName" runat="server"></asp:Label>
                        </div>
                    </div>
                     <div class="form-group form-group-sm">
                        <div class="col-sm-3">ชื่อผู้รับผิดชอบการประเมินตนเอง</div>
                       <div class="col-sm-4">
                            <nep:TextBox ID="T_0_4" data-bind="value: T_0_4" runat="server" Width="100%" CssClass="form-control"></nep:TextBox>
                        </div>
                         <div class="col-sm-2">ตำแหน่งในโครงการ</div>
                        <div class="col-sm-3">
                            <nep:TextBox ID="T_0_5" data-bind="value: T_0_5" runat="server" Width="100%" CssClass="form-control"></nep:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-2">ชื่อผู้ให้ข้อมูล</div>
                        <div class="col-sm-5">
                            <nep:TextBox ID="T_0_1" data-bind="value: T_0_1" runat="server" Width="100%" CssClass="form-control"></nep:TextBox>
                        </div>
                         <div class="col-sm-2">ความเกี่ยวข้องกับโครงการ</div>
                        <div class="col-sm-3">
                            <nep:TextBox ID="T_0_2" data-bind="value: T_0_2" runat="server" Width="100%" CssClass="form-control"></nep:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">ประเภทของความพิการ</div>
                        <div class="col-sm-8">
                            <nep:TextBox ID="T_0_3" data-bind="value: T_0_3" runat="server" Width="100%" CssClass="form-control"></nep:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 1.ลักษณะโครงการ </div>
                    </div>
                    <div class="form-group form-group-sm">
                            <div class="col-sm-4">
                            <input type="radio" name="R_0_4" value="1" data-bind="checked: R_0_4" />
 โครงการที่อยู่ระหว่างดำเนินการ 
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_0_4" value="2" checked="checked"/>
 โครงการที่ดำเนินการเสร็จสิ้นแล้ว  
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="col-sm-12"> 2.สถานภาพโครงการ</div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                            <input type="radio" name="R_0_5" value="1" data-bind="checked: R_0_5" />
 อยู่ระหว่างดำเนินโครงการยังไม่มีผลผลิตเกิดขึ้น 
                        </div>
                        <div class="col-sm-6">
                            <input type="radio" name="R_0_5" value="2" data-bind="checked: R_0_5" />
 อยู่ระหว่างดำเนินโครงการและมีผลผลิตเกิดขึ้นแล้วเป็นระยะ  
                        </div>
                        <div class="col-sm-6">
                            <input type="radio" name="R_0_5" value="2" data-bind="checked: R_0_5" />
 ดำเนินโครงการแล้วเสร็จและมีผลผลิตนำไปใช้งานได้  
                        </div>
                    </div>
                    <p></p>
                    <hr style="width:80%;align-content:center" />
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">องค์ประกอบที่ 1 ด้านการบริหารโครงการ</label>
                    </div>
                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:25%">ประเด็น</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:30%">เกณฑ์การประเมิน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:25%">คะแนน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">คำอธิบาย</th>
                            </tr>
                        </thead>
                        <tr >
                            <td style="vertical-align:top">1.1 หัวหน้าโครงการ/ผู้บริหารโครงการ</td>
                            <td style="vertical-align:top">
                            จำนวน                                <input type="text" style="width:150px" min="0" id="T_1_1_1" data-bind="kendoNumericTextBox: { value: T_1_1_1, spinners: false, format: '##,#0' }" />
 คน                                <br />
                                <input type="checkbox" name="C_1_1_2_1" onchange="khProjBG.C_1_1();" data-bind="checked: C_1_1_2_1" />
มีคุณวุฒิหรือประสบการณ์ในการบริหารโครงการ                                <br />
                                <input type="checkbox" name="C_1_1_2_2" onchange="khProjBG.C_1_1();" data-bind="checked: C_1_1_2_2" />
มีความสามารถในการบริหารจัดการโครงการจนบรรลุวัตถุประสงค์ของโครงการ
                            </td>
                            <td style="vertical-align:top">
                                  <input type="radio" name="T_1_1_3" value="6" onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_1_3" />
เหมาะสมและสอดคล้อง (6 คะแนน)                        <br />
                                <input type="radio" name="T_1_1_3" value="3.1"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_1_3" />
เหมาะสมข้อเดียว (3 คะแนน)   <br />
                                   <input type="radio" name="T_1_1_3" value="3.2"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_1_3" />
สอดคล้องข้อเดียว (3 คะแนน)                        <br />
                                <input type="radio" name="T_1_1_3" value="0" onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_1_3" />
ไม่เหมาะสมและไม่สอดคล้อง (0 คะแนน)    <br />
                                <%--<span id="T_1_1_3" data-bind="text: T_1_1_3">0.00</span>--%>
                                <!-- <input style="width:80px" type="text" min="0" id="T_1_1_3" data-bind="kendoNumericTextBox: { value: T_1_1_3, spinners: false, format: '##,#0' }" /> -->
                            </td >
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_1_1_4" data-bind="value: T_1_1_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">1.2 จำนวนเจ้าหน้าที่ในการดำเนินโครงการ</td>
                            <td style="vertical-align:top">
                            จำนวน                                <input type="text" style="width:150px" min="0" id="T_1_2_1" data-bind="kendoNumericTextBox: { value: T_1_2_1, spinners: false, format: '##,#0' }" />
 คน                                <br />
                                <input type="checkbox" name="C_1_2_2_1" onchange="khProjBG.C_1_2();" data-bind="checked: C_1_2_2_1" />
มีเจ้าหน้าที่ที่เพียงพอกับหน้าที่ในการดำเนินโครงการ                                <br />
                                <input type="checkbox" name="C_1_2_2_2" onchange="khProjBG.C_1_2();" data-bind="checked: C_1_2_2_2" />
มีคุณวุฒิ/ประสบการณ์ในการดำเนินโครงการ
                            </td>
                            <td style="vertical-align:top">
                                <%--<span id="T_1_2_3" data-bind="text: T_1_2_3">0.00</span>--%>
                                <!-- <input style="width:80px" type="text" min="0" id="T_1_2_3" data-bind="kendoNumericTextBox: { value: T_1_2_3, spinners: false, format: '##,#0' }" /> -->
                              <input type="radio" name="T_1_2_3" value="6"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_2_3" />
เพียงพอและเหมาะสม (6 คะแนน)                   <br />
                                <input type="radio" name="T_1_2_3" value="3.1"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_2_3" />
เพียงพอข้อเดียว (3 คะแนน)  <br />
                                   <input type="radio" name="T_1_2_3" value="3.2"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_2_3" />
เหมาะสมข้อเดียว (3 คะแนน)                      <br />
                                <input type="radio" name="T_1_2_3" value="0"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_2_3" />
ไม่เพียงพอและไม่เหมาะสม (0 คะแนน)    <br />
                            </td >
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_1_2_4" data-bind="value: T_1_2_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">1.3	การจัดทำรายงานการประชุม</td>
                            <td style="vertical-align:top">
                            จน. ครั้งที่จัดประชุม                                <input type="text" style="width:100px" onblur="khProjBG.C_1_3();" min="0" id="T_1_3_2_1" data-bind="kendoNumericTextBox: { value: T_1_3_2_1, spinners: false, format: '##,#0' }" />
 ครั้ง                                <br />
                                <%--<input type="checkbox" name="C_1_3_2" data-bind="checked: C_1_3_2" />
มีการดำเนินการจัดประชุมภายในโครงการและจัดทำรายงานการประชุมทุกครั้ง--%>  
                              มีการดำเนินการจัดประชุมภายในโครงการและจัดทำรายงานการประชุม                                <input type="text" style="width:100px" min="0" id="T_1_3_2_2" onblur="khProjBG.C_1_3();" data-bind="kendoNumericTextBox: { value: T_1_3_2_2, spinners: false, format: '##,#0' }" />
  ครั้ง 
                            </td>
                            <td style="vertical-align:top">
                                <%--<span id="T_1_3_3" data-bind="text: T_1_3_3">0.00</span>--%>
                                <%--<input style="width:80px" type="text" min="0" id="T_1_3_3" data-bind="kendoNumericTextBox: { value: T_1_3_3, spinners: false, format: '##,#0' }" />
--%>
                                 <input type="radio" name="T_1_3_3" value="6"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_3_3" />
ดําเนินการประชุมทุกครั้ง (6 คะแนน)                 <br />
                                <input type="radio" name="T_1_3_3" value="3" onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_3_3" />
ดําเนินการประชุมบางครั้ง (3 คะแนน)  <br />
                                   <input type="radio" name="T_1_3_3" value="0" onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_3_3" />
ไม่มีการประชุม (0 คะแนน)                   <br />
                            </td >
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_1_3_4" data-bind="value: T_1_3_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">1.4 เครื่องมือ อุปกรณ์ สิ่งอำนวยความสะดวก.</td>
                            <td style="vertical-align:top">

                                <input type="checkbox" onchange="khProjBG.C_1_4();" name="C_1_4_2" data-bind="checked: C_1_4_2" />
มีเครื่องมือ อุปกรณ์ สิ่งอำนวยความสะดวกต่อการบริหารจัดการโครงการฯตามความจำเป็น  
                              
                            </td>
                            <td style="vertical-align:top">
                                <%--<span id="T_1_4_3" data-bind="text: T_1_4_3">0.00</span>--%>
                                <!-- <input style="width:80px" type="text" min="0" id="T_1_4_3" data-bind="kendoNumericTextBox: { value: T_1_4_3, spinners: false, format: '##,#0' }" /> -->
                                <input type="radio" name="T_1_4_3" value="6"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_4_3" />
เหมาะสม มีเครื่องมือครบ (6 คะแนน)               <br />
                                <input type="radio" name="T_1_4_3" value="3"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_4_3" />
มีเครื่องมือไม่ครบแต่สามารถดําเนินการได้ ( 3 คะแนน)<br />
                                   <input type="radio" name="T_1_4_3" value="0"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_4_3" />
ไม่เหมาะสม และไม่มีเครื่องมือ (0 คะแนน)               <br />
                                </td >
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_1_4_4" data-bind="value: T_1_4_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">1.5 งบประมาณ</td>
                            <td style="vertical-align:top">
                            จำนวนงบประมาณของโครงการ                                <input type="text" style="width:150px" min="0" max="999999999" id="T_1_5_1" data-bind="kendoNumericTextBox: { value: T_1_5_1, spinners: false, format: '###,###,###' }" />
 บาท                                <br />
                                <input type="checkbox" onchange="khProjBG.C_1_5();" name="C_1_5_2_1" data-bind="checked: C_1_5_2_1" />
กำหนดให้มีบุคคลผู้รับผิดชอบทางการเงินและบัญชีชัดเจน                                <br />
                                <input type="checkbox" onchange="khProjBG.C_1_5();" name="C_1_5_2_2" data-bind="checked: C_1_5_2_2" />
มีการดำเนินงานด้านงบประมาณ อย่างมีประสิทธิภาพโดยมีการ จัดทำรายงานงบประมาณค่าใช้จ่าย                                <br />
                                <input type="checkbox" onchange="khProjBG.C_1_5();" name="C_1_5_2_3" data-bind="checked: C_1_5_2_3" />
งบประมาณพอเพียงกับการดำเนินโครงการ
                            </td>
                            <td style="vertical-align:top">
                                <%--<span id="T_1_5_3" data-bind="text: T_1_5_3">0.00</span>--%>
                                <!-- <input style="width:80px" type="text" min="0" id="T_1_5_3" data-bind="kendoNumericTextBox: { value: T_1_5_3, spinners: false, format: '##,#0' }" /> -->
                                <input type="radio" name="T_1_5_3" value="6"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_5_3" />
มีทั้ง 3 ข้อ (6 คะแนน)               <br />
                                <input type="radio" name="T_1_5_3" value="4"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_5_3" />
มี 2 ใน 3 ข้อ (4 คะแนน) <br />
                                 <input type="radio" name="T_1_5_3" value="2"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_5_3" />
มี 1 ใน 3 ข้อ (2 คะแนน) <br />
                                 <input type="radio" name="T_1_5_3" value="0"  onchange="khProjBG.SECT_1_total();" data-bind="checked: T_1_5_3" />
ไม่มี (0 คะแนน) <br />
                            </td >
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_1_5_4" data-bind="value: T_1_5_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:right">รวมคะแนน</td>
                            <td colspan="2">
                                เต็ม 30 คะแนน ได้คะแนน   <span id="SECT_1_total" data-bind="text: SECT_1_total">0.00</span>
                            </td>
                        </tr>
                    </table>
                    <hr style="width:80%;align-content:center" />
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">องค์ประกอบที่ 2 ด้านการดำเนินโครงการ</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">2.1 วัตถุประสงค์การดําเนินโครงการ</label>
                    </div>
                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th rowspan="1" scope="col" role="columnheader" class="k-header" style="width:20%">ประเด็น</th>
                                <th rowspan="1" scope="col" role="columnheader" class="k-header" style="width:20%">เกณฑ์การประเมิน</th>
                                <th rowspan="1" scope="col" role="columnheader" class="k-header" style="width:10%">ผลการดำเนินงาน</th>
                                <th rowspan="1" scope="col" role="columnheader" class="k-header" style="width:30%">เกณฑ์การให้คะแนน</th>
                                <%--<th rowspan="2" scope="col" role="columnheader" class="k-header">กลุ่มเป้าหมาย</th>--%>
                                <%--<th colspan="3" scope="col" role="columnheader" class="k-header">จน.ระยะเวลา</th>--%>
                                <th rowspan="1" scope="col" role="columnheader" class="k-header" style="width:20%">เอกสารหลักฐาน/คำอธิบาย</th>
                            </tr>
                         <%--   <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:30px">วัน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:30px">เดือน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:30px">ปี</th>
                            </tr>--%>
                        </thead>
                        <tr >
                            <td rowspan="6" style="vertical-align:top">2.1 วัตถุประสงค์การดําเนินโครงการ 
                                <%--<nep:TextBox ID="T_2_1_1" data-bind="value: T_2_1_1" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>--%>
                            </td>
                            <td style="vertical-align:top">

                                <input type="checkbox" name="C_2_1_2" onchange="khProjBG.C_2_1();" data-bind="checked: C_2_1_2" />
มีความสอดคล้องกับทุก วัตถุประสงค์ที่ได้กำหนดตาม แผนงานหรือโครงการฯที่ ได้ยื่นขอรับทุน สนับสนุนจากกองทุน                                <br />

                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_1_3" data-bind="value: T_2_1_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <td rowspan="6" style="vertical-align:top">
                                  <input type="radio" name="S_2_1_4" onchange="khProjBG.SECT_2_total();" value="30" data-bind="checked: S_2_1_4" />
มีความสอดคล้องกับวัตถุประสงค์และคนกลุ่มเป้าหมายได้รับความรู้ความเข้าใจ พร้อมทั้ง
สามารถพัฒนาศักยภาพทางร่างกาย (30 คะแนน)  <br />
                                <input type="radio" name="S_2_1_4" onchange="khProjBG.SECT_2_total();" value="15.00001" data-bind="checked: S_2_1_4" />
มีความสอดคล้องกับวัตถุประสงค์ข้อเดียว(15 คะแนน)<br />
                                 <input type="radio" name="S_2_1_4" onchange="khProjBG.SECT_2_total();" value="15.00002" data-bind="checked: S_2_1_4" />
คนกลุ่มเป้าหมายได้รับความรู้ความเข้าใจพร้อมทั้งสามารถพัฒนาศักยภาพทางร่างกาย อย่างเดียว (15 คะแนน)<br />
                                 <input type="radio" name="S_2_1_4" onchange="khProjBG.SECT_2_total();" value="0" data-bind="checked: S_2_1_4" />
ไม่มี (0 คะแนน) <br />

                                <%--<select data-bind="value: S_2_1_4" onclick="khProjBG.C_2_1();" style="width:100%;">
                                    <option value="">กรุณาระบุคะแนน </option>
                                    <option value="5">5 คะแนน - สอดคล้องครบ</option>
                                    <option value="4">4 คะแนน - สอดคล้อง 3 ใน 4 ข้อ</option>
                                    <option value="3">3 คะแนน - สอดคล้อง 1 ใน 2 ข้อ</option>
                                    <option value="2">2 คะแนน - สอดคล้อง 1 ใน 3 ข้อ</option>
                                    <option value="1">1 คะแนน - สอดคล้อง 1 ข้อ</option>
                                    <option value="0">0 คะแนน - ไม่มีความสอดคล้อง </option>
                                        <option value="30">มีความสอดคล้องกับวัตถุประสงค์และคนกลุ่มเป้าหมายได้รับความรู้ความเข้าใจ พร้อมทั้ง
สามารถพัฒนาศักยภาพทางร่างกาย (30 คะแนน)</option>
                                    <option value="15">มีความสอดคล้องกับวัตถุประสงค์ข้อเดียว(15 คะแนน)</option>
                                    <option value="15">คนกลุ่มเป้าหมายได้รับความรู้ความเข้าใจพร้อมทั้งสามารถพัฒนาศักยภาพทางร่างกาย อย่างเดียว (15 คะแนน)</option>
                                   <option value="0">ไม่มี (0 คะแนน)</option>
                                </select>    --%>   
                                <br>                         
                                <%--<span style="display:none;" id="T_2_1_4_1" data-bind="text: T_2_1_4_1">0.00</span>--%>
                            </td>
                           <%-- <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_1_5" data-bind="value: T_2_1_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                           <%-- <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_1_6" data-bind="kendoNumericTextBox: { value: T_1_1_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_1_7" data-bind="kendoNumericTextBox: { value: T_2_1_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_1_8" data-bind="kendoNumericTextBox: { value: T_2_1_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_1_9" data-bind="value: T_2_1_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >

                            <td style="vertical-align:top">
                                <input type="checkbox" onchange="khProjBG.C_2_2();" name="C_2_2_2" data-bind="checked: C_2_2_2" />
ส่งเสริมและให้ความรู้แก่คนพิการ <br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_2_3" data-bind="value: T_2_2_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <%--<td style="vertical-align:top">
                                <span id="S_2_2_4" data-bind="text: S_2_2_4">0.00</span>
                                 <select data-bind="value: S_2_2_4" style="width:120px">
                                 <option value="">กรุณาระบุคะแนน </option>  
                                 <option value="5">5 คะแนน - สอดคล้องครบ</option>
                                 <option value="4">4 คะแนน - สอดคล้อง 3 ใน 4 ข้อ</option>     
                                 <option value="3">3 คะแนน - สอดคล้อง 1 ใน 2 ข้อ</option>     
                                 <option value="2">2 คะแนน - สอดคล้อง 1 ใน 3 ข้อ</option>     
                                 <option value="1">1 คะแนน - สอดคล้อง 1 ข้อ</option>     
                                 <option value="0">0 คะแนน - ไม่มีความสอดคล้อง </option>            
                             </select> 
                            </td>--%>
                            <%--<td style="vertical-align:top">
                                <nep:TextBox ID="T_2_2_5" data-bind="value: T_2_2_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                          <%--  <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_2_6" data-bind="kendoNumericTextBox: { value: T_2_2_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_2_7" data-bind="kendoNumericTextBox: { value: T_2_2_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_2_8" data-bind="kendoNumericTextBox: { value: T_2_2_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_2_9" data-bind="value: T_2_2_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">
                                <input type="checkbox" onchange="khProjBG.C_2_3();" name="C_2_3_2" data-bind="checked: C_2_3_2" />
ส่งเสริมและพัฒนาศักยภาพด้านร่างกายแก่คนพิการ                                <br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_3_3" data-bind="value: T_2_3_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                           <%-- <td style="vertical-align:top">
                                <span id="S_2_3_4" data-bind="text: S_2_3_4">0.00</span>
                            </td>--%>
                            <%--<td style="vertical-align:top">
                                <nep:TextBox ID="T_2_3_5" data-bind="value: T_2_3_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                           <%-- <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_3_6" data-bind="kendoNumericTextBox: { value: T_2_3_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_3_7" data-bind="kendoNumericTextBox: { value: T_2_3_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_3_8" data-bind="kendoNumericTextBox: { value: T_2_3_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_3_9" data-bind="value: T_2_3_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top">
                                <input type="checkbox" name="C_2_4_2" onchange="khProjBG.C_2_4();" data-bind="checked: C_2_4_2" />
ส่งเสริมและสร้างความเข้มแข็งด้านจิตใจแก่คนพิการ <br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_4_3" data-bind="value: T_2_4_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        <%--    <td style="vertical-align:top">
                                <span id="S_2_4_4" data-bind="text: S_2_4_4">0.00</span>
                            </td>--%>
                          <%--  <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_4_5" data-bind="value: T_2_4_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                           <%-- <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_4_6" data-bind="kendoNumericTextBox: { value: T_2_4_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_4_7" data-bind="kendoNumericTextBox: { value: T_2_4_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_4_8" data-bind="kendoNumericTextBox: { value: T_2_4_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_4_9" data-bind="value: T_2_4_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >

                            <td style="vertical-align:top">
                            จำนวนคนพิการ ที่มีอาชีพ <input type="text" style="width:150px" min="0" id="T_2_5_2_1" data-bind="kendoNumericTextBox: { value: T_2_5_2_1, spinners: false, format: '##,#0' }" /> คน <br /><input type="checkbox" name="C_2_5_2_2" onchange="khProjBG.C_2_5();"  data-bind="checked: C_2_5_2_2" />
ส่งเสริมและพัฒนาด้านการประกอบอาชีพ 
                             
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_5_3" data-bind="value: T_2_5_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                           <%-- <td style="vertical-align:top">
                                <span id="S_2_5_4" data-bind="text: S_2_5_4">0.00</span>
                            </td>--%>
                            <%--<td style="vertical-align:top">
                                <nep:TextBox ID="T_2_5_5" data-bind="value: T_2_5_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                          <%--  <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_5_6" data-bind="kendoNumericTextBox: { value: T_2_5_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_5_7" data-bind="kendoNumericTextBox: { value: T_2_5_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_5_8" data-bind="kendoNumericTextBox: { value: T_2_5_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_5_9" data-bind="value: T_2_5_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr >

                            <td style="vertical-align:top">
                                <input type="checkbox" name="C_2_6_2_1" onchange="khProjBG.C_2_6();"  data-bind="checked: C_2_6_2_1" />ส่งเสริมด้าน <br />
                                <nep:TextBox ID="T_2_6_2_2" data-bind="value: T_2_6_2_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_6_3" data-bind="value: T_2_6_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <%--<td style="vertical-align:top">
                                <span id="S_2_6_4" data-bind="text: S_2_6_4">0.00</span>
                                 <select data-bind="value: S_2_6_4" style="width:120px">
                                 <option value="">กรุณาระบุคะแนน </option>  
                                 <option value="5">5 คะแนน - สอดคล้องครบ</option>
                                 <option value="4">4 คะแนน - สอดคล้อง 3 ใน 4 ข้อ</option>     
                                 <option value="3">3 คะแนน - สอดคล้อง 1 ใน 2 ข้อ</option>     
                                 <option value="2">2 คะแนน - สอดคล้อง 1 ใน 3 ข้อ</option>     
                                 <option value="1">1 คะแนน - สอดคล้อง 1 ข้อ</option>     
                                 <option value="0">0 คะแนน - ไม่มีความสอดคล้อง </option>            
                             </select> 
                            </td>--%>
                            <%--<td style="vertical-align:top">
                                <nep:TextBox ID="T_2_6_5" data-bind="value: T_2_6_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>--%>
                           <%-- <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_6_6" data-bind="kendoNumericTextBox: { value: T_2_6_6, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_6_7" data-bind="kendoNumericTextBox: { value: T_2_6_7, spinners: false, format: '##,#0' }" />
                            </td >
                            <td style="vertical-align:top">
                                <input style="width:25px" type="text" min="0" id="T_2_6_8" data-bind="kendoNumericTextBox: { value: T_2_6_8, spinners: false, format: '##,#0' }" />
                            </td >--%>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_2_6_9" data-bind="value: T_2_6_9" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align:right">รวมคะแนน</td>
                            <td colspan="1">
                                เต็ม 30 คะแนน ได้คะแนน   <span id="SECT_2_total" data-bind="text: SECT_2_total">0.00</span> 
                            </td>
                            <!-- <td colspan="5"></td>
                        <td colspan="4"></td> -->
                        </tr>
                    </table>
                    <p></p>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">2.2  ผลการดําเนินกิจกรรมโครงการ</label>
                    </div>
                       <div class="form-group form-group-sm">
                        <div class="col-sm-12">ผลการดําเนินงานกิจกรรมตามระยะเวลาที่กําหนดในโครงการ</div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                            <input type="radio" name="R_2_CC" value="0"  onchange="khProjBG.SECT_3_total();" data-bind="checked: R_2_CC" />
 ล่าช้ากว่าโครงการ 
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_2_CC" value="1" onchange="khProjBG.SECT_3_total();"data-bind="checked: R_2_CC" />
 ตามโครงการ  
                        </div>
                        <div class="col-sm-4">
                            <input type="radio" name="R_2_CC" value="2" onchange="khProjBG.SECT_3_total();"data-bind="checked: R_2_CC" />
 เร็วกว่าโครงการ  
                        </div>
                    </div>
                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width:25px"></th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="width:300px">กิจกรรม/โครงการ (ระบุกิจกรรมดําเนินการตามที่ได้กําหนดไว้ในรายละเอียดเสนอโครงการ)</th>
                                <th colspan="2" scope="col" role="columnheader" class="k-header">งบประมาณ</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header">อุปสรรคที่เกิดขึ้น</th>
                                <th colspan="2" scope="col" role="columnheader" class="k-header">การบรรลุวัตถุประสงค์</th>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header">แนวทางที่ดำเนินงานต่อไป</th>
                            </tr>
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:10px">พอ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:10px">ไม่พอ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:10px">บรรลุ</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:10px">ไม่บรรลุ</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>
                                    <nep:TextBox ID="T_2_PJ_1_2" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_1_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_1_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_1_3"  />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_1_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_1_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="T_2_PJ_1_5" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_1_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_1_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_1_6"/>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_1_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_1_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="T_2_PJ_1_7" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_1_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>
                                    <nep:TextBox ID="TextBox1" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_2_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_2_3" onchange="khProjBG.SECT_2_1();"  value="1" data-bind="checked: R_2_PJ_2_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_2_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_2_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox2" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_2_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_2_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_2_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_2_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_2_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox3" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_2_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>
                                    <nep:TextBox ID="TextBox4" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_3_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_3_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_3_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_3_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_3_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox5" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_3_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_3_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_3_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_3_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_3_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox6" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_3_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>
                                    <nep:TextBox ID="TextBox7" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_4_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_4_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_4_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_4_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_4_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox8" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_4_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_4_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_4_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_4_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_4_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox9" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_4_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>
                                    <nep:TextBox ID="TextBox10" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_5_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_5_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_5_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_5_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_5_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox11" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_5_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_5_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_5_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_5_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_5_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox12" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_5_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>6</td>
                                <td>
                                    <nep:TextBox ID="TextBox13" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_6_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_6_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_6_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_6_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_6_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox14" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_6_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_6_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_6_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_6_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_6_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox15" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_6_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>7</td>
                                <td>
                                    <nep:TextBox ID="TextBox16" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_7_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_7_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_7_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_7_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_7_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox17" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_7_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_7_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_7_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_7_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_7_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox18" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_7_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>8</td>
                                <td>
                                    <nep:TextBox ID="TextBox19" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_8_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_8_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_8_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_8_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_8_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox20" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_8_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_8_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_8_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_8_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_8_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox21" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_8_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>9</td>
                                <td>
                                    <nep:TextBox ID="TextBox22" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_9_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_9_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_9_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_9_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_9_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox23" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_9_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_9_6" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_9_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_9_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_9_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox24" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_9_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>10</td>
                                <td>
                                    <nep:TextBox ID="TextBox25" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_10_2" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_10_3" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_10_3" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_10_3" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_10_3" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox26" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_10_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_10_6" onchange="khProjBG.SECT_2_1();" onchange="khProjBG.SECT_2_1();" value="1" data-bind="checked: R_2_PJ_10_6" />
                                </td>
                                <td>
                                    <input type="radio" name="R_2_PJ_10_6" onchange="khProjBG.SECT_2_1();" value="2" data-bind="checked: R_2_PJ_10_6" />
                                </td>
                                <td>
                                    <nep:TextBox ID="TextBox27" onblur="khProjBG.SECT_2_1();" data-bind="value: T_2_PJ_10_7" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                </td>

                            </tr>
                           <%-- <tr>
                                <td colspan="6" style="text-align:right;border: 1px solid #ccc;">รวมคะแนน</td>
                                <td colspan="4" style="border: 1px solid #ccc;">
                                    <span id="SECT_2_1_total" data-bind="text: SECT_2_1_total"></span> คะแนน
                                </td>
                            </tr>--%>
                          <%--  <tr style="border:1px">
                                <td colspan="3" style="text-align:right;">กิจกรรม/โครงการ</td>
                                <td colspan="5" style="border: 1px solid #ccc;">
                                    <select data-bind="value: S_2_2" onclick="khProjBG.SECT_3_total();">
                                        <option value="">กรุณาระบุคะแนน </option>
                                        <option value="3">3 คะแนน - ดำเนินกิจกรรม/โครงการครบถ้วน</option>
                                        <option value="2">2 คะแนน - ดำเนินกิจกรรม/โครงการ 1 ใน 3</option>
                                        <option value="1">1 คะแนน - ดำเนินกิจกรรม/โครงการอย่างน้อย 1 กิจกรรม</option>
                                        <option value="0">0 คะแนน – ไม่ได้ดำเนินกิจกรรม</option>
                                    </select>
                                </td>
                            </tr>--%>
                        </thead>
                    </table>
                    <br /> <br />
                    <div class="form-group form-group-sm">
                              <input type="radio" name="S_2_2" onchange="khProjBG.SECT_3_total();" value="10" data-bind="checked: S_2_2" />
กิจกรรมการดําเนินการมีงบประมาณเพียงพอและมีการดําเนินการบรรลุวัตถุประสงค์ (10 คะแนน) <br />
                                <input type="radio" name="S_2_2" onchange="khProjBG.SECT_3_total();" value="5.00001" data-bind="checked: S_2_2" />
กิจกรรมการดําเนินการมีงบประมาณเพียงพอ แต่มีการดําเนินการบรรลุไม่วัตถุประสงค์ (5 คะแนน)<br />
                                 <input type="radio" name="S_2_2" onchange="khProjBG.SECT_3_total();" value="5.00002" data-bind="checked: S_2_2" />
กิจกรรมการดําเนินกามีการดําเนินการบรรลุวัตถุประสงค์ แต่งบประมาณไม่เพียงพอ (5 คะแนน)<br />
                                 <input type="radio" name="S_2_2" onchange="khProjBG.SECT_3_total();" value="0" data-bind="checked: S_2_2" />
กิจกรรมการดําเนินการมีงบประมาณไม่เพียงพอและดําเนินการไม่บรรลุวัตถุประสงค์ (0 คะแนน) <br />
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12 text-left">
                            เต็ม 10 คะแนน ได้คะแนน   <span id="SECT_3_total" data-bind="text: SECT_3_total"></span>
                        </div>
                    </div>

                    <hr style="width:80%;align-content:center" />
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">องค์ประกอบที่  3 ด้านผลลัพธ์การดำเนินโครงการ</label>
                    </div>
                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">ประเด็น</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">เกณฑ์การประเมิน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">ผลการดำเนินงาน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">เกณฑ์การให้คะแนน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">คำอธิบาย</th>
                            </tr>
                        </thead>
                        <tr >
                            <td style="vertical-align:top">3.1 การติดตาม/ประเมินผลกิจกรรม</td>
                            <td style="vertical-align:top">
                                <input type="checkbox" name="C_3_1_2" onchange="khProjBG.SECT_4_total();" data-bind="checked: C_3_1_2" />
มีการดำเนินการติดตาม/ประเมินผลการจัดกิจกรรม 
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_1_3" data-bind="value: T_3_1_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <td style="vertical-align:top">
                                <%--<nep:TextBox ID="T_3_1_4" data-bind="value: T_3_1_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>--%>
                                            <input type="radio" name="T_3_1_4" onchange="khProjBG.SECT_4_total();" value="10" data-bind="checked: T_3_1_4" />
มีการดําเนินการติดตาม และประเมินผล 
 การจัดกิจกรรม (10 คะแนน)<br />
                                <input type="radio" name="T_3_1_4" onchange="khProjBG.SECT_4_total();" value="5" data-bind="checked: T_3_1_4" />
มีการดําเนินการติดตาม แต่ไม่มี 
 การประเมินผลการจัดกิจกรรม (5 คะแนน)<br />
                                 <input type="radio" name="T_3_1_4" onchange="khProjBG.SECT_4_total();" value="0" data-bind="checked: T_3_1_4" />
ไม่มีการดําเนินการติดตามและประเมินผล
การจัดกิจกรรม (0 คะแนน)<br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_1_5" data-bind="value: T_3_1_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>

                        </tr>
                        <tr >
                            <td style="vertical-align:top">3.2 ผลลัพธ์ที่ได้จากกิจกรรม</td>
                            <td style="vertical-align:top">
                                <input type="checkbox" name="C_3_2_2_1" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_2_2_1" />
ได้รับความรู้                                <br />
                                <input type="checkbox" name="C_3_2_2_2" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_2_2_2" />
มีความเข้มแข็งทางด้านจิตใจ                                <br />
                                <input type="checkbox" name="C_3_2_2_3" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_2_2_3" />
มีความเข้มแข็งทางด้านร่างกาย                                <br />
                                <input type="checkbox" name="C_3_2_2_4" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_2_2_4" />
มีอาชีพ                                <br />
                                <input type="checkbox" name="C_3_2_2_5" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_2_2_5" />
ด้านอื่นๆ                                <br />
                                <asp:TextBox Width="100%" runat="server" ID="T_3_2_2_6" data-bind="value: T_3_2_2_6"></asp:TextBox>
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_2_3" data-bind="value: T_3_2_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <td style="vertical-align:top">
                                <%--<nep:TextBox ID="T_3_2_4" data-bind="value: T_3_2_4" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>--%>
                                  <input type="radio" name="T_3_2_4" onchange="khProjBG.SECT_4_total();" value="10" data-bind="checked: T_3_2_4" />
มี 3 ข้อขึ้นไป (10 คะแนน)<br />
                                <input type="radio" name="T_3_2_4" onchange="khProjBG.SECT_4_total();" value="5" data-bind="checked: T_3_2_4" />
มี 1 – 2 ข้อ (5 คะแนน) <br />
                                 <input type="radio" name="T_3_2_4" onchange="khProjBG.SECT_4_total();" value="0" data-bind="checked: T_3_2_4" />
ไม่มี (0 คะแนน)<br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_2_5" data-bind="value: T_3_2_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>

                        </tr>
                        <tr >
                            <td style="vertical-align:top">3.3 กลุ่มเป้าหมาย</td>
                            <td style="vertical-align:top">
                                <input type="checkbox" name="C_3_3_2_1" onchange="khProjBG.SECT_4_total();"  data-bind="checked: C_3_3_2_1" />
กลุ่มเป้าหมายที่ เข้าร่วมกิจกรรม ลักษณะตรงตามเป้าหมาย ของโครงการ <br /> จำนวน <input type="text" style="width:150px" min="0" id="T_3_3_2_2" data-bind="kendoNumericTextBox: { value: T_3_3_2_2, spinners: false, format: '##,#0' }" />คน <br />  ร้อยละ <input type="text" style="width:150px" min="0" id="T_3_3_2_3" data-bind="kendoNumericTextBox: { value: T_3_3_2_3, spinners: false, format: '##,#0' }" />
 คน 
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_3_3" data-bind="value: T_3_3_3" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>
                            <td style="vertical-align:top">
                               <%-- <select data-bind="value: S_3_3_4" onclick="khProjBG.SECT_4_total();">
                                    <option value="">กรุณาระบุคะแนน </option>
                                    <option value="3">3 คะแนน – เข้าร่วมครบ</option>
                                    <option value="2">2 คะแนน - เข้าร่วมมากกว่า 1 ใน 3</option>
                                    <option value="1">1 คะแนน – เข้าร่วมน้อยกว่า 1 ใน 3</option>
                                    <option value="0">0 คะแนน – ไม่มีกลุ่มเป้าหมายเข้าร่วม</option>
                                </select>--%>
                                   <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="10" data-bind="checked: S_3_3_4" />
เข้าร่วมครบ ร้อยละ 100 (10 คะแนน)<br />
                                <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="8" data-bind="checked: S_3_3_4" />
เข้าร่วมร้อยละ 80 - 99 (8 คะแนน)<br />
                                 <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="6" data-bind="checked: S_3_3_4" />
เข้าร่วมร้อยละ 60 – 79 (6 คะแนน)<br />
                                 <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="4" data-bind="checked: S_3_3_4" />
เข้าร่วมร้อยละ 50 – 57 (4 คะแนน)<br />
                                  <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="2" data-bind="checked: S_3_3_4" />
เข้าร่วมร้อยละ 40 – 47 (2 คะแนน)<br />
                                 <input type="radio" name="S_3_3_4" onchange="khProjBG.SECT_4_total();" value="0" data-bind="checked: S_3_3_4" />
เข้าร่วมน้อยกว่าร้อยละ 40 (0 คะแนน) <br />
                            </td>
                            <td style="vertical-align:top">
                                <nep:TextBox ID="T_3_3_5" data-bind="value: T_3_3_5" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td colspan="3" style="text-align:right">รวมคะแนน</td>
                            <td colspan="2">
                                เต็ม 30 คะแนน ได้คะแนน   <span id="SECT_4_total" data-bind="text: SECT_4_total">0.00</span>
                            </td>
                        </tr>
                    </table>
                    <br />
                      <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">สรุปผลการประเมินตนเอง</label>
                    </div>
                <%--รวมคะแนนทั้งสิ้น <span id="total_score" data-bind="text: Total_Score">0.00</span> คะแนนเต็ม 100 คะแนน คิดเป็นร้อยละ <span id="Percent_Score" data-bind="text: Percent_Score">0.00</span>--%>

                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:60%">เกณฑ์คะแนน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">คะแนนเต็ม</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:20%">คะแนนที่ได้</th>
                            </tr>
                        </thead>
                        <tr >
                            <td style="vertical-align:top">องค์ประกอบที่ 1 ด้านการบริหารโครงการ</td>
                            <td style="text-align:center">30 คะแนน</td>
                              <td style="text-align:center"><span id="SECT_1_total_sum" data-bind="text: SECT_1_total">0.00</span> คะแนน</td>
                        </tr>
                       <tr >
                            <td style="vertical-align:top">องค์ประกอบที่ 2 ด้านการดําเนินโครงการ</td>
                            <td style="vertical-align:top"></td>
                              <td style="vertical-align:top"></td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">2.1 วัตถุประสงค์การดําเนินโครงการ</td>
                            <td style="text-align:center">30 คะแนน</td>
                              <td style="text-align:center"><span id="SECT_2_total_sum" data-bind="text: SECT_2_total">0.00</span> คะแนน</td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">2.2 ผลการดําเนินกิจกรรมโครงการ</td>
                            <td style="text-align:center">10 คะแนน</td>
                              <td style="text-align:center"><span id="SECT_3_total_sum" data-bind="text: SECT_3_total">0.00</span> คะแนน</td>
                        </tr>
                        <tr >
                            <td style="vertical-align:top">องค์ประกอบที่ 3 ด้านผลลัพธ์การดําเนินโครงการ</td>
                            <td style="text-align:center">30 คะแนน</td>
                              <td style="text-align:center"><span id="SECT_4_total_sum" data-bind="text: SECT_4_total">0.00</span> คะแนน</td>
                        </tr>
                        <tr>
                            <td  style="vertical-align:top">รวมคะแนน</td>
                           <td  style="text-align:center">100 คะแนน</td>
                             <td  style="text-align:center"><span id="total_score_sum" data-bind="text: Total_Score">0.00</span> คะแนน</td>
                        </tr>
                    </table>
                    <br /><br />
                    <table style="width:100%;border-style: solid;border-color: #ccc;border-width: 1px;" class="k-grid">

                        <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:30%">เกณฑ์คะแนน</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:70%">การแปรผล</th>
                            </tr>
                        </thead>
                        <tr >
                            <td style="vertical-align:top">ได้เกณฑ์การประเมิน 80 คะแนน ขึ้นไป</td>
                            <td style="text-align:left">โครงการประสบผลสําเร็จ วัตถุประสงค์โครงการเหมาะสม สอดคล้อง ตรวจตามความต้องการของ
กลุ่มเป้าหมาย สมควรได้รับการขยายผลดําเนินการต่อ/ดําเนินการต่อ</td>
                              </tr>
                       <tr >
                           <td style="vertical-align:top">ได้เกณฑ์การประเมิน 60-79 คะแนน </td>
                            <td style="text-align:left">โครงการประสบผลสําเร็จ แต่ยังสามารถปรับปรุงประสิทธิภาพและประสิทธิผลในการดําเนินโครงการ
เนื่องจากอาจยังไม่สามารถบรรลุวัตถุประสงค์ หรือสอดคล้องเพียงบางส่วน </td>
                        </tr>
                        <tr >
                          <td style="vertical-align:top">ได้เกณฑ์การประเมิน 40-49 คะแนน</td>
                            <td style="text-align:left">โครงการประสบผลสําเร็จ แต่ไม่อยู่ในระดับที่สมควรขยายผล/ดําเนินต่อ หากไม่มีการปรับเปลี่ยน เช่น 
ตรงตามความต้องการของกลุ่มเป้าหมาย ตรงตามวัตถุประสงค์โครงการเป็นต้น</td>        </tr>
                           <tr >
                          <td style="vertical-align:top">ได้เกณฑ์การประเมินต่ํากว่า 50 คะแนน</td>
                            <td style="text-align:left">โครงการไม่ประสบผลสําเร็จ เห็นควรจัดทําโครงการอื่น</td>        </tr>
                       
                    </table>


                </div>                <!--form-horizontal-->
            </div>            <!--panel แบบรายงานผลการปฏิบัติงาน-->



            <div class="form-horizontal">
                <div class="form-group form-group-sm">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport" Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>

                        <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                                                <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" ValidationGroup="SaveProjectReport" 
                            Text="บันทึกและส่งแบบประเมินตนเอง" 
                            OnClientClick="if (confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?')) return GetQNModelToServer(); else return false;"
                            OnClick="ButtonSaveReportResult_Click" />
                        <%--OnClick="ButtonSaveAndSendProjectReport_Click" />
--%>

                        <%--       <asp:Button runat="server" ID="ButtonOfficerSave" CssClass="btn btn-primary btn-sm" ValidationGroup="OfficerSaveReportResult" Text="<%$ code:Nep.Project.Resources.UI.ButtonOfficerSaveSuggestion %>" OnClick="ButtonOfficerSave_Click" Visible="false" />
--%>

                        <%--      <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm" Visible="false" Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank" NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectResult?projectID={0}", ProjectID ) %>'></asp:HyperLink>      --%>

                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx" Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

