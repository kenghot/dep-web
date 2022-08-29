<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabSatisfyControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabSatisfyControl"  %>
<%@ Import Namespace="Nep.Project.Resources" %>


<asp:UpdatePanel ID="UpdatePanelReportResult" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <style type="text/css">
            div.input-count input[type="number"]{ 
                 width: 40px;
                 text-align:right;
            }
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
   input {
           border-style: solid;
    border-width: thin;
   }
        </style>

        <asp:HiddenField runat="server" ID="hdfQViewModel" />
        <asp:HiddenField runat="server" ID="hdfQContols" />
        <asp:HiddenField runat="server" ID="hdfIsDisable" />

        <div  id="divVueQN" class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">แบบประเมินความพึงพอใจ</h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                    <span class="field-desc">
                    <div class="form-group form-group-sm">
                        <button onclick="window.open('../Content/files/SatisfyForm.pdf', '_blank'); return false;">ดาวน์โหลดแบบประเมิน</button>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ส่วนที่ 1: ข้อมูลทั่วไป</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-2">
                             1. ชื่อองค์กร / หน่วยงาน
                        </div>
                        <div class="col-sm-10">
                            <input type="text" style="width:100%"  v-model="items[field.txtProjectName].v" />
                        </div>
                    </div>
                     <div class="form-group form-group-sm">
                        <div class="col-sm-2">
                             สถานที่ติดต่อ
                        </div>
                        <div class="col-sm-10">
                            <nep:TextBox ID="TextBox1" v-model="items[field.txtAddress].v" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                          
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-2">
                             โทรศัพท์/โทรสาร
                        </div>
                        <div class="col-sm-4">
                            <input type="text"  v-model="items[field.txtTel].v" />
                        </div>
                        <div class="col-sm-2">
                             E-mail
                        </div>
                        <div class="col-sm-4">
                            <input type="text"  v-model="items[field.txtEmail].v" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                             <%--2. อายุ <asp:TextBox runat="server" ID="T_1_2" data-bind="kendoNumericTextBox: T_1_2" >0</asp:TextBox> ปี--%>
                             2. จำนวนโครงการที่เคยได้รับการสนับสนุนจากกองทุนฯ จำนวน <input type="number"  v-model.number="items[field.txtProjectCount].v" /> โครงการ
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-4">
                             3. กลุ่มเป้าหมายในโครงการ
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                              <input type="radio" name="R_1_3" value="1" v-model="items[field.rd1_3].v"  />1. พิการทางการเคลื่อนไหวหรือทางร่างกาย
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="2" v-model="items[field.rd1_3].v"  />2. พิการทางการมองเห็น
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="3" v-model="items[field.rd1_3].v"  />3. พิการทางการได้ยินหรือสื่อความหมาย 
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="4" v-model="items[field.rd1_3].v" />4. พิการทางสติปัญญาหรือการเรียนรู้
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="5" v-model="items[field.rd1_3].v"  />5. พิการทางจิตใจหรือพฤติกรรม
                        </div>
                        <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="6" v-model="items[field.rd1_3].v"  />6. พิการทางออทิสติก
                        </div>
                                                <div class="col-sm-6">
                             <input type="radio" name="R_1_3" value="7" v-model="items[field.rd1_3].v"  />7. พิการมากกว่า 1 ประเภท
                        </div>
                        <div class="col-sm-6"> 
                             <input type="radio" name="R_1_3" value="8" v-model="items[field.rd1_3].v"  />8. อื่นๆ ระบุ <asp:TextBox runat="server" v-model="items[field.txt1_3_other].v" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <p></p>

              
                    <p></p>
<%--                    <div class="form-group form-group-sm">
                         <div class="col-sm-12">
                        จำนวนที่สำรวจ <input type="number"  v-model.number="items[field.fAll].v" onblur="CalAllSatisfy()" /> ชุด
                             </div>
                    </div>--%>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title">ส่วนที่ 2 : ระดับความพึงพอใจของผู้รับบริการ</label>
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
                                 <%--<th scope="col" role="columnheader" data-field="No" rowspan="1" data-title="ลำดับ" data-index="0" id="efdc983f-8432-4625-9261-e515c41db2c6" class="k-header">ลำดับ</th>--%>
                                <th rowspan="2" scope="col" role="columnheader" class="k-header" style="vertical-align : middle;text-align:center;" >ประเด็นการสารวจความพึงพอใจ</th>
                                <th colspan="5" scope="col" role="columnheader" class="k-header" >ระดับคะแนน</th>

                        </tr>
                        <tr>
                                 <th scope="col" role="columnheader" class="k-header" style="width:50px">5</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">4</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">3</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">2</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:50px">1</th>
                        </tr>
  <%--                      <tr role="row">
                               
                                <th scope="col" role="columnheader" class="k-header" >ราย</th>
                                <th scope="col" role="columnheader" class="k-header" >%</th>
                                <th scope="col" role="columnheader" class="k-header" >ราย</th>
                                <th scope="col" role="columnheader" class="k-header" >%</th>
                                <th scope="col" role="columnheader" class="k-header" >ราย</th>
                                <th scope="col" role="columnheader" class="k-header" >%</th>
                                <th scope="col" role="columnheader" class="k-header" >ราย</th>
                                <th scope="col" role="columnheader" class="k-header" >%</th>
                                <th scope="col" role="columnheader" class="k-header" >ราย</th>
                                <th scope="col" role="columnheader" class="k-header" >%</th>
                     
                        </tr>--%>


                         </thead>

<tr  role="row">
<td colspan="5" role="gridcell">
<b>1. ความพึงพอใจเกี่ยวกับการให้บริการของเจ้าหน้าที่<b>
</td>
<%--<td  style="text-align:center;vertical-align:central" role="gridcell">--%>
<%--<div class="input-count">--%>
    <%--<input type="radio" name="QN1_0" value="1" v-model="items[field.QN1_0].v"  />--%>
<%--<input type="number" :id="items[field.QN1_0_0].n" title="QN1_0_0" v-model.number="items[field.QN1_0_0].v" onblur="CalSastisfy(this)" />--%>
<%--</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_0" value="2" v-model="items[field.QN1_0].v"  />--%>
<%--<input type="number" :id="items[field.QN1_0_1].n" title="QN1_0_1" v-model.number="items[field.QN1_0_1].v" onblur="CalSastisfy(this)" />--%>
<%--</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_0" value="3" v-model="items[field.QN1_0].v"  />--%>
<%--<input type="number" :id="items[field.QN1_0_2].n" title="QN1_0_2" v-model.number="items[field.QN1_0_2].v" onblur="CalSastisfy(this)" />--%>
<%--</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_0" value="4" v-model="items[field.QN1_0].v"  />--%>
<%--<input type="number" :id="items[field.QN1_0_3].n" title="QN1_0_3" v-model.number="items[field.QN1_0_3].v" onblur="CalSastisfy(this)" />--%>
<%--</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_0" value="5" v-model="items[field.QN1_0].v"  />--%>
<%--<input type="number" :id="items[field.QN1_0_4].n" title="QN1_0_4" v-model.number="items[field.QN1_0_4].v" onblur="CalSastisfy(this)" />--%>
<%--</div>
</td>--%>

</tr>
<tr  role="row">
<td role="gridcell">
1.1 ให้คำแนะนำและตอบข้อซักถามอย่างชัดเจน
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_1" value="1" v-model="items[field.QN1_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_1" value="2" v-model="items[field.QN1_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_1" value="3" v-model="items[field.QN1_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_1" value="4" v-model="items[field.QN1_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_1" value="5" v-model="items[field.QN1_1].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td role="gridcell">
1.2 ให้บริการด้วยความสะดวกรวดเร็ว
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_2" value="1" v-model="items[field.QN1_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_2" value="2" v-model="items[field.QN1_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_2" value="3" v-model="items[field.QN1_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_2" value="4" v-model="items[field.QN1_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_2" value="5" v-model="items[field.QN1_2].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td role="gridcell">
1.3 ความพร้อม ความตั้งใจ ความกระตือรือร้นในการให้บริการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_3" value="1" v-model="items[field.QN1_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_3" value="2" v-model="items[field.QN1_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_3" value="3" v-model="items[field.QN1_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_3" value="4" v-model="items[field.QN1_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_3" value="5" v-model="items[field.QN1_3].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
1.4 การพูดจา และอัธยาศัยไมตรีของเจ้าหน้าที่
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_4" value="1" v-model="items[field.QN1_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_4" value="2" v-model="items[field.QN1_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_4" value="3" v-model="items[field.QN1_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_4" value="4" v-model="items[field.QN1_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_4" value="5" v-model="items[field.QN1_4].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td role="gridcell">
1.5 การนิเทศ/ติดตามประเมินผล
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_5" value="1" v-model="items[field.QN1_5].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_5" value="2" v-model="items[field.QN1_5].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_5" value="3" v-model="items[field.QN1_5].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_5" value="4" v-model="items[field.QN1_5].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN1_5" value="5" v-model="items[field.QN1_5].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td colspan="5" role="gridcell">
<b>2. ความพึงพอใจด้านกระบวนการหรือขั้นตอนการให้บริการ</b>
</td>
<%--<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_0" value="1" v-model="items[field.QN2_0].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_0" value="2" v-model="items[field.QN2_0].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_0" value="3" v-model="items[field.QN2_0].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_0" value="4" v-model="items[field.QN2_0].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_0" value="5" v-model="items[field.QN2_0].v"  />
</div>
</td>--%>

</tr>
<tr  role="row">
<td role="gridcell">
2.1 ระเบียบ หลักเกณฑ์ ขั้นตอน วิธีการให้เงินอุดหนุนโครงการของกองทุนฯ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_1" value="1" v-model="items[field.QN2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_1" value="2" v-model="items[field.QN2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_1" value="3" v-model="items[field.QN2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_1" value="4" v-model="items[field.QN2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_1" value="5" v-model="items[field.QN2_1].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td colspan="5" role="gridcell">
2.2 ขั้นตอนการขอรับเงินสนับสนุนโครงการ
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การยื่นเสนอโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_1" value="1" v-model="items[field.QN2_2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_1" value="2" v-model="items[field.QN2_2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_1" value="3" v-model="items[field.QN2_2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_1" value="4" v-model="items[field.QN2_2_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_1" value="5" v-model="items[field.QN2_2_1].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การพิจารณาโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_2" value="1" v-model="items[field.QN2_2_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_2" value="2" v-model="items[field.QN2_2_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_2" value="3" v-model="items[field.QN2_2_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_2" value="4" v-model="items[field.QN2_2_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_2" value="5" v-model="items[field.QN2_2_2].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การแจ้งผลการพิจารณาโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_3" value="1" v-model="items[field.QN2_2_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_3" value="2" v-model="items[field.QN2_2_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_3" value="3" v-model="items[field.QN2_2_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_3" value="4" v-model="items[field.QN2_2_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_3" value="5" v-model="items[field.QN2_2_3].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td role="gridcell">
 - การเบิกจ่ายเงิน
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_4" value="1" v-model="items[field.QN2_2_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_4" value="2" v-model="items[field.QN2_2_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_4" value="3" v-model="items[field.QN2_2_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_4" value="4" v-model="items[field.QN2_2_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_2_4" value="5" v-model="items[field.QN2_2_4].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td colspan="5" role="gridcell">
2.3 ระยะเวลาในการขอรับการสนับสนุนโครงการ 
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การยื่นเสนอโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_1" value="1" v-model="items[field.QN2_3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_1" value="2" v-model="items[field.QN2_3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_1" value="3" v-model="items[field.QN2_3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_1" value="4" v-model="items[field.QN2_3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_1" value="5" v-model="items[field.QN2_3_1].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การพิจารณาโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_2" value="1" v-model="items[field.QN2_3_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_2" value="2" v-model="items[field.QN2_3_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_2" value="3" v-model="items[field.QN2_3_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_2" value="4" v-model="items[field.QN2_3_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_2" value="5" v-model="items[field.QN2_3_2].v"  />
</div>
</td>


</tr>
<tr  role="row">
<td role="gridcell">
 - การแจ้งผลการพิจารณาโครงการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_3" value="1" v-model="items[field.QN2_3_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_3" value="2" v-model="items[field.QN2_3_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_3" value="3" v-model="items[field.QN2_3_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_3" value="4" v-model="items[field.QN2_3_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_3" value="5" v-model="items[field.QN2_3_3].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การเบิกจ่ายเงิน
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_4" value="1" v-model="items[field.QN2_3_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_4" value="2" v-model="items[field.QN2_3_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_4" value="3" v-model="items[field.QN2_3_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_4" value="4" v-model="items[field.QN2_3_4].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN2_3_4" value="5" v-model="items[field.QN2_3_4].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td colspan="5" role="gridcell">
<b>3. ความพึงพอใจด้านสิ่งอานวยความสะดวก</b> 
</td>

</tr>
<tr  role="row">
<td role="gridcell">
 - การจัดสิ่งอานวยความสะดวกสาหรับผู้รับบริการ เช่น ห้องสุขา ทางลาด เก้าอี้ น้าดื่ม ที่จอดรถ ฯลฯ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN3_1" value="1" v-model="items[field.QN3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN3_1" value="2" v-model="items[field.QN3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN3_1" value="3" v-model="items[field.QN3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN3_1" value="4" v-model="items[field.QN3_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN3_1" value="5" v-model="items[field.QN3_1].v"  />
</div>
</td>

</tr>
<tr  role="row">
<td colspan="5" role="gridcell">
<b>4. ความพึงพอใจช่องทางการรับรู้ข้อมูลข่าวสาร และการติดต่อสื่อสาร</b>
</td>

</tr>
<tr  role="row">
<td role="gridcell">
4.1 ศูนย์บริการคนพิการ
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_1" value="1" v-model="items[field.QN4_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_1" value="2" v-model="items[field.QN4_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_1" value="3" v-model="items[field.QN4_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_1" value="4" v-model="items[field.QN4_1].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_1" value="5" v-model="items[field.QN4_1].v"  />
</div>
</td>
</tr>
<tr  role="row">
<td role="gridcell">
4.2 ข้อมูลทางอินเตอร์เน็ต
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_2" value="1" v-model="items[field.QN4_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_2" value="2" v-model="items[field.QN4_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_2" value="3" v-model="items[field.QN4_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_2" value="4" v-model="items[field.QN4_2].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_2" value="5" v-model="items[field.QN4_2].v"  />
</div>
</td>

</tr>

<tr  role="row">
<td role="gridcell">
4.3 ข้อมูลทางสื่อประชาสัมพันธ์ เช่น ทีวี , แผ่นพับประชาสัมพันธ์
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_3" value="1" v-model="items[field.QN4_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_3" value="2" v-model="items[field.QN4_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_3" value="3" v-model="items[field.QN4_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_3" value="4" v-model="items[field.QN4_3].v"  />
</div>
</td>
<td  style="text-align:center;vertical-align:central" role="gridcell">
<div class="input-count">
    <input type="radio" name="QN4_3" value="5" v-model="items[field.QN4_3].v"  />
</div>
</td>

</tr>


                                </table>
                    
                    <div class="form-group form-group-sm">
                       <br> <label class="col-sm-12 form-group-title">ส่วนที่ 3 ข้อคิดเห็น และข้อเสนอแนะเพิ่มเติม</label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm_12">
                            <div class="required-block">
                                <nep:TextBox ID="T_4" v-model="items[field.CM3].v" runat="server" CssClass="form-control  textarea-height" TextMode="MultiLine"></nep:TextBox>
                                <span class="required"></span>
                            </div>  
                            <asp:RequiredFieldValidator ID="RequiredT_4" ControlToValidate="T_4" 
                                runat="server" CssClass="error-text" ValidationGroup="SaveProjectReport" SetFocusOnError="true"
                                Text="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>" 
                                ErrorMessage="<%$ code:String.Format(Nep.Project.Resources.Error.RequiredField, Model.ProjectReportResult_ActivityDescription) %>"
                                />                   
                        </div>
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
       <%--<script src="../../Scripts/VueQN.js?v=<%= DateTime.Now.Ticks.ToString() %>"></script>--%> 
<%--               <script type="text/javascript" src="../../Scripts/Vue/Satisfy.js?v="<%= DateTime.Now.Ticks.ToString()  %>></script>
                <script type="text/javascript" src="../../Scripts/Vue/VueQN.js?v="<%= DateTime.Now.Ticks.ToString()  %>></script>--%>
        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
<%--                    <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>--%>
                     <asp:Button runat="server" ID="ButtonSaveReportResult" CssClass="btn btn-primary btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="appVueQN.param.IsReported = '0';appVueQN.saveData(); return false;" Visible="false"/>
                    <%--<asp:Button runat="server" ID="Button1" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveProjectReport"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClientClick="return GetQNModelToServer()" OnClick="ButtonSaveReportResult_Click" Visible="false"/>--%>
                    <!-- OnClientClick="return ConfirmToSubmitRportResult()"-->
                    <asp:Button runat="server" ID="ButtonSaveAndSendProjectReport" CssClass="btn btn-primary btn-sm" Visible="false" 
                        Text="บันทึกและส่งแบบประเมินความพึงพอใจ"
                        OnClientClick="if (confirm('เมื่อท่านทำการส่งข้อมูลให้เจ้าหน้าที่แล้วจะไม่สามารถแก้ไขข้อมูลในส่วนนี้ได้อีก - ในกรณีที่ต้องการบันทึกข้อมูลโดยยังไม่ส่งข้อมูล ให้กดที่ปุ่ม \'บันทึก\' - ต้องการยืนยันการส่งข้อมูล?')) {appVueQN.param.IsReported = '1';appVueQN.saveData(); return false; } else return false;"
                         />
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
        <div style="display:none">
       <!-- ko text: $(document).ready( function() { $('input[type=radio]').change(function() {
                SastisfySumScore(this);

               });
            }); -->
        <!-- /ko -->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

