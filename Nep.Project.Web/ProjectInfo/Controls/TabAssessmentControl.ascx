<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabAssessmentControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabAssessmentControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>




<asp:UpdatePanel ID="UpdatePanelAssessment"
    UpdateMode="Conditional"
    runat="server">
    <ContentTemplate>
        <style>
            .style-label {
                display: inline-block;
                padding-top: 4px;
            }

            .label-no {
                width: 30px;
            }
        </style>

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TabTitleAssessmentProject %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title" style="text-align: center"><%= UI.TitleAssessmentForm %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-6 form-group-title"><%= Model.ProjectInfo_AssessmentProvince %>:</label>
                        <div class="col-sm-4">
                            <asp:Label ID="LabelOfficerProvince" runat="server" />
                            <asp:HiddenField ID="HiddenFieldOfficerProvince" runat="server" />
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3 control-label"><%= Model.ProjectInfo_ProjectNo1 %></label>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelProjectNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_OrganizationName %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="OrganizationName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_Name %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelProjectName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_BudgetRequest %></div>
                        <div class="col-sm-9 control-value">
                            <asp:Label ID="LabelBudget" runat="server" />
                            <%= UI.LabelBath %>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo4 %></div>
                        <div class="col-sm-9">
                            <asp:RadioButtonList ID="RadioButtonListIsPassAss4" runat="server" CssClass="form-control-radio criterion-no-4" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$ code:UI.LabelPass %>" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="<%$ code:UI.LabelNotPass %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo5 %></div>
                        <div class="col-sm-9">
                            <asp:RadioButtonList ID="RadioButtonListIsPassAss5" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$ code:UI.LabelPass %>" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="<%$ code:UI.LabelNotPass %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3 control-label"><%= Model.ProjectInfo_CriterionNo6 %></div>
                        <div class="col-sm-9">
                            <span class="form-control-left-desc"><%=UI.LabelTotal %></span>
                            <asp:Label ID="TotalScore" runat="server" Text="-" CssClass="style-label total-score"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="TotalScoreDesc" runat="server" Text="" CssClass="style-label total-score-desc"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-3"></div>
                        <div class="col-sm-9">
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                        <span class="label-no">(1)</span>ชื่อแผนงานหรือโครงการ (ค่าน้ำหนัก = 5 คะแนน)

                                    </label>
                                    <div class="col-sm-2" style="background-color: yellow">
                                        <%--<asp:Label runat="server" ID="lblScore61" Text="888"></asp:Label>--%>
                                        
                                            
                                        <%--<label class="col-sm-10 control-label control-label-right without-delimit">--%>
                                            <asp:TextBox runat="server" ID="txtScore61" Enabled="false" Width="40px"></asp:TextBox> คะแนน 

                                        <%--</label>--%>
                                        
                                    </div>
                                    
                                    <div class="col-sm-2" style="display: none">
                                        <asp:DropDownList ID="DropDownListAssessmentProjectName" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">1.1</span>การเขียนชื่อแผนงานหรือโครงการมีความชัดเจน เหมาะสม และเฉพาะเจาะจง
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rd6_1"
                                        CssClass="error-text" SetFocusOnError="true"
                                        Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                </div>
                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_1" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="ดีมาก = 5 คะแนน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ปานกลาง ค่อนข้างดี = 2 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_1" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>
                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                        <span class="label-no">(2)</span>หลักการและเหตุผล (ค่าน้ำหนัก = 20 คะแนน)
                                    </label>
                                    <div class="col-sm-2" style="background-color: yellow">
                                <%--        <label class="col-sm-10 control-label control-label-right without-delimit">

                                            <asp:Label runat="server" ID="lblScore62" Text="0"></asp:Label>
                                            คะแนน
                                        </label>--%>
                                        <asp:TextBox runat="server" ID="txtScore62" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                    </div>

                                    <div class="col-sm-2" style="display: none">
                                        <asp:DropDownList ID="DropDownListAssessmentReason" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">2.1</span>แสดงถึงปัญหา สะท้อนสถานการณ์ปัญหา และความต้องการของกลุ่มเป้าหมายอย่างชัดเจน
                  
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rd6_2_1"
                                        CssClass="error-text" SetFocusOnError="true"
                                        Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                </div>
                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_2_1" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="ดีมาก = 15 คะแนน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ปานกลาง ค่อนข้างดี = 5 คะแนน" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_2_1" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-10 control-label control-label-left without-delimit">
                                        <span class="label-no">2.2</span>สอดคล้องกับนโยบาย และยุทธศาสตร์ที่เกียวข้องกับคนพิการ
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rd6_2_2"
                                        CssClass="error-text" SetFocusOnError="true"
                                        Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                </div>
                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_2_2" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="สอดคล้อง = 5 คะแนน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ไม่สอดคล้อง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_2_2" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>

                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <div class="col-sm-10">
                                        <label class="control-label control-label-left without-delimit" style="font-weight: bold">
                                            <span class="label-no">(3)</span>วัตถุประสงค์ของโครงการ (ค่าน้ำหนัก = 15 คะแนน)
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rd6_3"
                                            CssClass="error-text" SetFocusOnError="true"
                                            Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                    </div>
                                    <div class="col-sm-2" style="background-color: yellow">
                                     <%--   <label class="col-sm-10 control-label control-label-right without-delimit">

                                            <asp:Label runat="server" ID="lblScore63" Text="0"></asp:Label>
                                            คะแนน
                                        </label>--%>
                                         <asp:TextBox runat="server" ID="txtScore63" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                    </div>
                                    <div class="col-sm-2" style="display: none">
                                        <asp:DropDownList ID="DropDownListAssessmentObjective" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_3" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="1. มีความชัดเจน มีความเป็นไปได้ และสามารถวัดผลและประเมินผลได้" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2. สอดคล้องกับชื่อโครงการที่เสนอ และสะท้อนการป้องกันแก้ไขปัญหาหรือสนองตอบกลุ่มเป้าหมาย เกี่ยวกับการคุ้มครองและพัฒนาคุณภาพชีวิตคนพิการ" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_3" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>

                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <div class="col-sm-10">
                                        <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                            <span class="label-no">(4)</span>การกำหนดกลุ่มเป้าหมาย (ค่าน้ำหนัก = 10 คะแนน)
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rd6_4"
                                            CssClass="error-text" SetFocusOnError="true"
                                            Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                    </div>
                                    <div class="col-sm-2" style="background-color: yellow">
                                     <%--   <label class="col-sm-10 control-label control-label-right without-delimit">

                                            <asp:Label runat="server" ID="lblScore64" Text="0"></asp:Label>
                                            คะแนน
                                        </label>--%>
                                         <asp:TextBox runat="server" ID="txtScore64" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                    </div>
                                    <div class="col-sm-2" style="display: none">
                                        <asp:DropDownList ID="DropDownListAssessmentTargetGroup" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_4" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="1. สอดคล้องกับวัตถุประสงค์ของโครงการ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2. ระบุกลุ้มเป้าหมายชัดเจน" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3. มีจำนวนกลุ่มเป้าหมายที่เหมาะสม" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4. ไม่ซ้ำซ้อนกับโครงการลักษณะเดียวกัน และดำเนินการในพื้นที่เดียวกันที่ได้รับอนุมัติแล้ว" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5. มีวิธิการคัดเลือกกลุ่มเป้าหมายที่ชัดเจน" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="6"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_4" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>
                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <div class="col-sm-10">
                                        <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                            <span class="label-no">(5)</span>สถานที่ดำเนินการ (ค่าน้ำหนัก = 10 คะแนน)
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rd6_5"
                                            CssClass="error-text" SetFocusOnError="true"
                                            Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                    </div>
                                    <div class="col-sm-2" style="background-color: yellow">
             <%--                           <label class="col-sm-10 control-label control-label-right without-delimit">

                                            <asp:Label runat="server" ID="lblScore65" Text="0"></asp:Label>
                                            คะแนน
                                        </label>--%>
                                         <asp:TextBox runat="server" ID="txtScore65" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                    </div>
                                    <div class="col-sm-2" style="display: none">
                                        <asp:DropDownList ID="DropDownListAssessmentLocation" runat="server" CssClass="form-control assessment-dropdownlist"
                                            DataTextField="Text" DataValueField="Value">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm" style="padding-left: 20px">

                                    <asp:RadioButtonList ID="rd6_5" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                        <asp:ListItem Text="1. เหมาะสมกับการดำเนินกิจกรรม" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2. ให้ระบุสถานที่ตั้งของโครงการที่ชัดเจน" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3. มีความเหมาะสมกับสภาพความพิการ" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4. มีความสะดวก และประหยัด" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txt6_5" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                                </div>
                                <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                    <div class="col-sm-10">
                                        <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                            <span class="label-no">(6)</span>ระยะเวลาการดำเนินการ (ค่าน้ำหนัก = 10 คะแนน)
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rd6_6"
                                            CssClass="error-text" SetFocusOnError="true"
                                            Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                                    </div>
                     
                                <div class="col-sm-2" style="background-color: yellow">
                                 <%--   <label class="col-sm-10 control-label control-label-right without-delimit">

                                        <asp:Label runat="server" ID="lblScore66" Text="0"></asp:Label>
                                        คะแนน
                                    </label>--%>
                                     <asp:TextBox runat="server" ID="txtScore66" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                </div>
                                <div class="col-sm-2" style="display: none">
                                    <asp:DropDownList ID="DropDownListAssessmentTiming" runat="server" CssClass="form-control assessment-dropdownlist"
                                        DataTextField="Text" DataValueField="Value">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group form-group-sm" style="padding-left: 20px">

                                <asp:RadioButtonList ID="rd6_6" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                    <asp:ListItem Text="1. มีการระบุระยะเวลาเริ่มต้นและสิ้นสุดของโครงการภายในปีงบประมาณ ในกรณีมีแผนงานเกินกว่าหนึ่งปีให้เสนอภาพรวมมาประกอบพิจารณา" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2. มีการกำหนดระยะเวลาในการดำเนินกิจกรรมแต่ละกิจกรรมอย่างชัดเจน" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3. มีความสอดคล้องกับสภาพความพิการ" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txt6_6" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                            </div>
                            <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                                <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                    <span class="label-no">(7)</span>วิธีการดำเนินการ/กิจกรรม (ค่าน้ำหนัก = 20 คะแนน)
                                </label>
                                <div class="col-sm-2" style="background-color: yellow">
              <%--                      <label class="col-sm-10 control-label control-label-right without-delimit">

                                        <asp:Label runat="server" ID="lblScore67" Text="0"></asp:Label>
                                        คะแนน
                                    </label>--%>
                                     <asp:TextBox runat="server" ID="txtScore67" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                                </div>
                                <div class="col-sm-2" style="display: none">
                                    <asp:DropDownList ID="DropDownListAssessmentOperationMethod" runat="server" CssClass="form-control assessment-dropdownlist"
                                        DataTextField="Text" DataValueField="Value">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-10 control-label control-label-left without-delimit">
                                    <span class="label-no">7.1</span>วิธีการดำเนินการ/กิจกรรม
                  
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rd6_7_1"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_7_1" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="1. แสดงถึงรายละเอียดกิจกรรมที่เกี่ยวกับการคุ้มครองและพัฒนาคุณภาพชีวิตคนพิการอย่าชัดเจน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2. กิจกรรมที่ดำเนินงานต้องสอดคล้องกับวัตถุประสงค์โคงการ" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_7_1" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">7.2</span>มีการจัดลำดับชั้นตอนของกิจกรรมได้อย่างเหมาะสม
                  
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="rd6_7_2"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_7_2" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="ดีมาก = 5 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ปานกลาง ค่อนข้างดี = 3 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_7_2" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">7.3</span>มีแผนการปฎิบัติงานที่ชัดเจน (Action Plan)
                  
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="rd6_7_3"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_7_3" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="ดีมาก = 5 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ปานกลาง ค่อนข้างดี = 3 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_7_3" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                            <div class="col-sm-10 ">
                                <label class="control-label control-label-left without-delimit" style="font-weight: bold">
                                    <span class="label-no">(8)</span>งบประมาณ (ค่าน้ำหนัก = 10 คะแนน)
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rd6_8"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                            </div>
                        <div class="col-sm-2" style="background-color: yellow">
<%--                            <label class="col-sm-10 control-label control-label-right without-delimit">
                                <asp:Label runat="server" ID="lblScore68" Text="0"></asp:Label>
                                คะแนน
                            </label>--%>
                             <asp:TextBox runat="server" ID="txtScore68" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                        </div>
                        <div class="col-sm-2" style="display: none">
                            <asp:DropDownList ID="DropDownListAssessmentBudget" runat="server" CssClass="form-control assessment-dropdownlist"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_8" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="1. มีความสอดคล้องกับโครงการที่ขอรับการสนับสนุน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2. มีความสมเหตุสมผล ประหยัด คุ้มค่า เป็นไปตามเกณฑ์ที่กำหนดไว้" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_8" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                            <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                <span class="label-no">(9)</span>ผลที่คาดว่าจะได้รับ (ค่าน้ำหนัก = 20 คะแนน)

                                        <asp:HiddenField ID="HiddenField7" runat="server" Value='<%# Eval("StandardID") %>' />
                            </label>
                            <div class="col-sm-2" style="background-color: yellow">
  <%--                              <label class="col-sm-10 control-label control-label-right without-delimit">

                                    <asp:Label runat="server" ID="lblScore69" Text="0"></asp:Label>
                                    คะแนน
                                </label>--%>
                                 <asp:TextBox runat="server" ID="txtScore69" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                            </div>
                            <div class="col-sm-2" style="display: none">
                                <asp:DropDownList ID="DropDownListAssessmentExpection" runat="server" CssClass="form-control assessment-dropdownlist"
                                    DataTextField="Text" DataValueField="Value">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">9.1</span>ผลที่คาดว่าจะได้รับ
                  
                            </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rd6_9_1"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_9_1" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="1. สามารถกำหนดผลที่เกิดขึ้นโดยตรงและโดยอ้อมจากการดำเนินงานตามโครงการ" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2. สามารถระบุกลุ่มเป้าหมายที่จะได้รับผลประโยชน์และผลกระทบทั้งในเชิงปริมาณและเชิงคุณภาพ" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_9_1" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">9.2</span>มีความเชื่อมโยงกับปัญหา และความต้องการในการแก้ไขปัญหาอย่างชัดเจน
                  
                            </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rd6_9_2"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_9_2" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="มี = 5 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ไม่มี = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_9_2" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                            <label class="col-sm-10 control-label control-label-left without-delimit" style="font-weight: bold">
                                <span class="label-no">(10)</span> ตัวชี้วัดความสำเร็จ (ค่าน้ำหนัก = 10 คะแนน)
                            </label>
                            <div class="col-sm-2" style="background-color: yellow">
<%--                                <label class="col-sm-10 control-label control-label-right without-delimit">

                                    <asp:Label runat="server" ID="lblScore610" Text="0"></asp:Label>
                                    คะแนน
                                </label>--%>
                                 <asp:TextBox runat="server" ID="txtScore610" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                            </div>
                            <div class="col-sm-2" style="display: none">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control assessment-dropdownlist"
                                    DataTextField="Text" DataValueField="Value">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">10.1</span> มีการกำหนดตัวชี้วัดที่ชัดเจน และสามารถวัดได้ทุกกิจกรรม
                  
                            </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="rd6_10_1"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_10_1" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="ดีมาก = 5 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ปานกลางค่อนข้างดี = 3 คะแนน" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_10_1" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-10 control-label control-label-left without-delimit">
                                <span class="label-no">10.2</span> สามารถวัดได้ในแต่ละวัตถุประสงค์
                  
                            </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rd6_10_2"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_10_2" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical" onclick="calRadio()">
                                <asp:ListItem Text="ดีมาก = 5 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ปานกลางค่อนข้างดี = 3 คะแนน" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = 0 คะแนน (โปรดให้ข้อเสนอแนะ)" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_10_2" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                    <div class="form-group form-group-sm" style="border-bottom: solid; border-width: 1px; border-color: gray;">
                            <div class="col-sm-10 ">
                                <label class="control-label control-label-left without-delimit" style="font-weight: bold">
                                    <span class="label-no">(11)</span>ความคิดเห็นเบื้องต้นต่อโครงการของผู้ประเมิน (ค่าน้ำหนัก = 10 คะแนน)
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="rd6_11"
                                    CssClass="error-text" SetFocusOnError="true"
                                    Text="กรุณาเลือกคำตอบ" ErrorMessage="กรุณาเลือกคำตอบ" ValidationGroup="SaveAssessment" />
                            </div>
                        <div class="col-sm-2" style="background-color: yellow;display:none">
<%--                            <label class="col-sm-10 control-label control-label-right without-delimit">
                                <asp:Label runat="server" ID="lblScore611" Text="0"></asp:Label>
                                คะแนน
                            </label>--%>
                             <asp:TextBox runat="server" ID="txtScore611" Enabled="false" Width="40px"></asp:TextBox> คะแนน 
                        </div>
                        <div class="col-sm-2" style="display: none">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control assessment-dropdownlist"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div class="form-group form-group-sm" style="padding-left: 20px">

                            <asp:RadioButtonList ID="rd6_11" runat="server" CssClass="form-control-radio criterion-no-5" RepeatDirection="Vertical">
                                <asp:ListItem Text="ดีมาก = 80 - 100 คะแนน" Value="1"></asp:ListItem>
                                <asp:ListItem Text="ดี = 60 - 79 คะแนน" Value="2"></asp:ListItem>
                                <asp:ListItem Text="ปานกลาง = 40 - 59 คะแนน" Value="3"></asp:ListItem>
                                <asp:ListItem Text="พอใช้ = 30 - 39 คะแนน" Value="4" ></asp:ListItem>
                                <asp:ListItem Text="ควรปรับปรุง = น้อยกว่า 20 คะแนน" Value="5"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txt6_11" Rows="3" placeholder="ข้อเสนอแนะเพื่อปรับปรุง" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>

                    </div>
                </div>

            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-12"><%= Model.ProjectInfo_CommentOther %></label>
            </div>
            <div class="form-group form-group-sm">
                <div class="col-sm-12">
                    <asp:TextBox ID="TextBoxAssessmentDesc" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                </div>
            </div>
        </div>
        </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.ProjectInfo_Strategic %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= Model.ProjectInfo_StandardStrategic %><span class="required"></span></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:RadioButtonList ID="RadioButtonListStandardStrategics" runat="server"
                                CssClass="form-control-checkbox-horizontal standard-strategic-checkbox"
                                RepeatDirection="Vertical">
                            </asp:RadioButtonList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorStandardStrategics" runat="server" ControlToValidate="RadioButtonListStandardStrategics"
                                CssClass="error-text" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_StandardStrategic) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_StandardStrategic) %>"
                                ValidationGroup="SaveAssessment" />

                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12"><%= Model.ProjectInfo_StrategicProvice %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBoxProvinceMissionDesc" runat="server" TextMode="MultiLine" CssClass="form-control textarea-height"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" OnClick="ButtonSave_Click"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" ValidationGroup="SaveAssessment" Visible="false" />

                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm"
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>



