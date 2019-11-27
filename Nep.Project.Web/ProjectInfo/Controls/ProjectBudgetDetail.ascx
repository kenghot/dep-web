<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectBudgetDetail.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.ProjectBudgetDetail" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<style type="text/css">
    .div-budget-status{
    padding: 3px;
    font-size: 12pt;
    border-style: solid;
    border-width: 1px;
    height: 50px;
    position: fixed;
    top: 0;
    background-color: bisque;
    width: 80%;
    z-index: 1000;
}
    .secretary-detail {
        margin-top:7px;
        font-weight:bold;
    }

    .got-support-name {
        margin-left:13px;
        width:95%;
    }

    .got-support-block {
        display:block;
        padding-top:7px;
    }

    .got-support-block span, .got-support-block input {
        float:left;        
        margin-right:5px;
    }

    .got-support-block span {
       display:inline-block;
       margin-top:4px;
    }

    .got-support-block span:first-child {
         margin-left:10px;
    }
    .td-border td {
        border-style:solid;
        border-width:1px;
        padding:5px;
    } 
</style>
<%--<script type="text/javascript" src="./Scripts/projectbudget.js"></script>--%>
<asp:UpdatePanel ID="UpdatePanelProjectBudget" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
         <asp:HiddenField runat="server" ID="hdfQViewModel" />
        <asp:HiddenField runat="server" ID="hdfQContols" />
        <asp:HiddenField runat="server" ID="hdfIsDisable" />
        <asp:HiddenField runat="server" ID="HiddenFieldProjectID" />
          
        <nep:TextBox runat="server" ID="TextBoxTotalAmount" Visible="false"></nep:TextBox>
        <%--<div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleTotalProjectBudget %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group form-group-sm">
                                    <label for="DemoName" class="col-sm-4 control-label"><%:UI.LabelBudgetDetailRequestAmount %></label>        
                                    <div class="col-sm-6" style="position:relative">
                                        <nep:TextBox runat="server" ID="TextBoxTotalAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="9999999.99" />
                                        <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTotalAmount" ControlToValidate="TextBoxTotalAmount" runat="server" CssClass="error-text"
                                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>"
                                            ValidationGroup="SaveProjectBudget" />
                                    </div> 
                                </div>
                                <div class="form-group form-group-sm" runat="server" id="DivBudgetDetailRequestAmount" visible="false">
                                    <label class="col-sm-4  control-label"><%:UI.LabelBudgetDetailApprovedAmount %></label>
                                     <div class="col-sm-6" style="position:relative">
                                         <nep:TextBox runat="server" ID="TextBoxReviseAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="999999.99" ReadOnly="true" />
                                         <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                     </div> 
                                </div>
                                <div class="form-group form-group-sm">
                                    <a id="anchorValidate" name="anchorValidate"></a>
                                    <span id="spanValidate" style="display:block;color:red"></span>
                                </div>
                            </div>
                        </div>                       
                        <div class="col-sm-1"></div>   
                        <div class="col-sm-5">
                            <span class="field-desc"><%: UI.LabelFieldDescription %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.LabelTotalProjectBudgetDescription) %>                                          
                        </div>
                    </div>
                </div><!-- /form-horizontal -->

                <div class="form-horizontal">                   
                    <div class="form-group form-group-sm">
                           
                       
                    </div>
                    
                </div>   
            </div>
            <!--panel-body-->
        </div>--%><!--panel-->

        <div class="panel panel-default"><!--รายละเอียดงบประมาณ-->
                            
  
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.BudgetDetail_Detail %></h3>
            </div>
            <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">

                            <asp:UpdatePanel ID="UpdatePanelBudgetDetail" 
                                             UpdateMode="Conditional" 
                                             runat="server">
                                <ContentTemplate>  
                                    <div id="Div1" class="asp-top-button" runat="server">
                                        <asp:Button ID="ButtonAddBudgetDetail" runat="server" Text="<%$ code:UI.ButtonAdd %>" 
                                            CssClass="btn btn-default" OnClick="ButtonAddBudgetDetail_Click" Visible="false"  /> 
                                    </div>
                                    
                                    <nep:GridView runat="server" ID="GridViewBudgetDetail" AutoGenerateColumns="false" AllowPaging="false"
                                        CssClass="asp-grid budget-detail-grid" DataKeyNames="UID" 
                                        OnRowEditing="GridViewBudgetDetail_RowEditing"
                                        OnRowCancelingEdit="GridViewBudgetDetail_RowCancelingEdit"
                                        OnRowCommand="GridViewBudgetDetail_RowCommand"
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound"
                                        ShowFooter="true" Visible="false">                                                        
                                        <Columns>
                                            

                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_No%>" ItemStyle-Width="30" ItemStyle-CssClass="product-budget-no">
                                                <ItemTemplate>
                                                    <%# Eval("No") %>
                                                </ItemTemplate> 
                                                
                                                <FooterTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$ code: Model.ProjectBudgetDetail_TotalAmount%>"/>
                                                </FooterTemplate>                                             
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Detail%>" >                                        
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("Detail"), null, "") %>
                                                </ItemTemplate>                                                                                   
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxBudgetDetail" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="MultiLine" Text='<%# Eval("Detail") %>'></nep:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetDetail" ControlToValidate="TextBoxBudgetDetail" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                    
                                                   
                                                </EditItemTemplate> 
                                                
                                                         
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="<%$ code: Model.ProjectBudgetDetail_Amount%>" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Amount"), "N2", "0.00") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <nep:TextBox runat="server" ID="TextBoxBudgetAmount" CssClass="form-control" ClientIDMode="Static"
                                                        TextMode="Number" Min="1.00" Max="999999999.99" Text='<%# Eval("Amount") %>'></nep:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" ControlToValidate="TextBoxBudgetAmount" runat="server" CssClass="error-text"
                                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Amount) %>" 
                                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Amount) %>"
                                                        ValidationGroup="SaveBudgetDetail" />                                                   
                                                </EditItemTemplate>   
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalBudgetAmount" CssClass="total-budget-amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField> 
                                                                                         
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120" >
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise1Amount"), "N2", "0.00") %>                                                
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise1Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField>    
                                            
                                            
                                            <asp:TemplateField HeaderText="<%$ code: UI.LabelBudgetDetailApprovedAmount%>" ItemStyle-Width="120">
                                                <ItemTemplate>
                                                    <%# Nep.Project.Common.Web.WebUtility.DisplayInForm( Eval("Revise2Amount"), "N2", "0.00") %>                                                      
                                                </ItemTemplate>                                               
                                                <FooterTemplate>
                                                    <asp:Label ID="LabelTotalRevise2Amount" runat="server" Text=""/>
                                                </FooterTemplate>                            
                                            </asp:TemplateField>                                          

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <%# Eval("ProjectBudgetID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="HiddenProjectBudgetID" runat="server" Value='<%# Eval ("ProjectBudgetID") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                     
                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="60">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonEdit" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png" 
                                                        ValidationGroup="SaveBudgetDetail" CommandName="edit" CommandArgument='<%# Eval("UID") %>'
                                                        Visible="<%# (GridViewBudgetDetail.EditIndex < 0) %>" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonEdit %>"/>
                                                    <asp:ImageButton ID="BudgetDetailButtonDelete" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                                        CommandName="del" CommandArgument='<%# Eval("UID") %>'
                                                        Visible="<%# (GridViewBudgetDetail.EditIndex < 0)  %>" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" />
                                                </ItemTemplate>    
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="BudgetDetailButtonSave" runat="server" ImageUrl="~/Images/icon/save_icon_16.png" 
                                                        ValidationGroup="SaveBudgetDetail" CommandName="save" CommandArgument='<%# Eval("UID") %>'
                                                        ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"/>
                                                    <asp:ImageButton ID="BudgetDetailButtonCancel" runat="server" ImageUrl="~/Images/icon/cancel_icon_16.png"
                                                        CommandName="cancel" CausesValidation="false" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"/>
                                                </EditItemTemplate>                                    
                                            </asp:TemplateField>
                                        </Columns>
                                    </nep:GridView>
                                </ContentTemplate>                                
                            </asp:UpdatePanel> 
                           
                        <%--    <asp:CustomValidator ID="CustomValidatorBudgetDetail" runat="server" CssClass="error-text"
                                OnServerValidate="ProjectBudgetDetailValidate" ClientValidationFunction="validateProjectBudgetDetail"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                ValidationGroup="SaveProjectBudget"/>--%>

                            <asp:CustomValidator ID="CustomValidatorMaxAmount" runat="server" CssClass="error-text"
                                OnServerValidate="CustomValidatorMaxAmount_ServerValidate" 
                                Text="" 
                                ErrorMessage=""
                                ValidationGroup="SaveProjectBudget"/>
                                 
                        </div>
                    </div>
                </div>
            <div class="panel-body">
              
                <div class="form-horizontal" style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <div id="divRDBudgetType1" class="col-sm-6">                            
                            <input  type="radio" id="rdBudgetType1" name="BudgetType" onchange="SelectBudgetPanel();khProjBG.RadioBudgetClick();" data-bind="checked: BudgetType"    value="1"/>ค่าใช้จ่ายโครงการในการฝึกอบรม
                        </div>
                        <div id="divRDBudgetType2" class="col-sm-6">
                            <input type="radio" id="rdBudgetType2" name="BudgetType" onchange="SelectBudgetPanel();khProjBG.RadioBudgetClick();" data-bind="checked: BudgetType" value="2" />ค่าใช้จ่ายศูนย์บริการคนพิการทั่วไป

                        </div>
                    </div>
                </div>
                <hr />

                <div id="divBudgetType1" style="display:none;font-size:12pt">
                <table id="tabNormalBudget" style="width:100%">
     <%--               <tr>
                        <td colspan="4"><b>สถานที่</b></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="form-group form-group-sm">
                                <div class="col-sm-2">
                                    <input type="radio" name="B1_Location" data-bind="checked: B1_Location" value="1"/>ราชการ
                                </div>
                                <div class="col-sm-2">
                                    <input type="radio" name="B1_Location" data-bind="checked: B1_Location" value="2"/>เอกชน
                                </div>
                            </div>
                        </td>
                    </tr>--%>
                                        <tr>
                    <td colspan="4" style="padding-left:30px">
                     <div class="form-horizontal" style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <input type="radio" name="Food3Meals" onchange="SelectMealCountRadio();khProjBG.sum123();"  data-bind="checked: Food3Meals" value="1"/>จัดในสถานที่ราชการ <%--(ไม่เกิน 600 บาท/วัน/คน)--%> 
                        </div>
                        <div class="col-sm-12">
                            <input type="radio" name="Food3Meals" onchange="SelectMealCountRadio();khProjBG.sum123();"  data-bind="checked: Food3Meals" value="2"/>จัดในสถานที่เอกชน <%--(ไม่เกิน 950 บาท/วัน/คน)--%>
                        </div>
                    </div>
                    </div>
                        </td>
                   </tr>
                    <tr>
                        <td colspan="4">
                            <b>1. ค่าอาหาร </b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />1.1 ค่าอาหาร   <span id="message_error_budget1" style="color:red"></span>
                        </td>
                    </tr>
       <%--             <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br />1.1.1 จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ (ไม่เกิน 600 บาท/วัน/คน) 
                            <br />
                         <input type="text" min="0" max="600" id="B1_1_1_1" format="####" data-bind="kendoNumericTextBox: B1_1_1_1" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br /> 1.1.2 จัดในโรงแรม (ไม่เกิน 950 บาท/วัน/คน)
                        </td>
                    </tr>--%>
                    <tr>
                    <td colspan="4" style="padding-left:30px">
                     <div class="form-horizontal" style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <input type="radio" name="SelectMealCount" onchange="SelectMealCountRadio();khProjBG.sum123();"   data-bind="checked: SelectMealCount" value="1" />จัดอาหารครบ 3 มื้อ <%--(ไม่เกิน 600 บาท/วัน/คน) --%>
                        </div>
                        <div class="col-sm-12">
                            <input type="radio" name="SelectMealCount" onchange="SelectMealCountRadio();khProjBG.sum123();"    data-bind="checked: SelectMealCount" value="2"/>จัดอาหารไม่ครบ 3 มื้อ <%--(ไม่เกิน 950 บาท/วัน/คน)--%>
                        </div>
                        <hr />
                    </div>
                    </div>
                        </td>
                   </tr>
                   <tr><td colspan="4" style="padding-left:30px;"><span id='span_meal_select'>ค่าอาหาร</span></td></tr>
                    <tr class="trmealbreakfast">
                        <td colspan="4" style="padding-left:30px">
                          อาหารเช้า
                        </td>
                    </tr>
                    <tr class="trmealbreakfast">
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   id="B1_1_1_2_M_1" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_M_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_1_1_2_M_2" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_M_2" /> มื้อ x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  id="B1_1_1_2_M_3" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_M_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_1_1_2_M_total" data-bind="text: B1_1_1_2_M_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          อาหารกลางวัน
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   id="B1_1_1_2_L_1" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_L_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_1_1_2_L_2" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_L_2" /> มื้อ x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  id="B1_1_1_2_L_3" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_L_3" /> บาท  
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_1_1_2_L_total"  data-bind="text: B1_1_1_2_L_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          อาหารเย็น
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   id="B1_1_1_2_D_1" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_D_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_1_1_2_D_2" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_D_2" /> มื้อ x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  id="B1_1_1_2_D_3" onblur="khProjBG.B1_1_1_2();" data-bind="kendoNumericTextBox: B1_1_1_2_D_3" /> บาท  
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_1_1_2_D_total" data-bind="text: B1_1_1_2_D_total">0.00</span> บาท
                        </td>
                    </tr>
                    <!--1.2 ค่าอาหาร (จัดไม่ครบ 3 มื้อ) -->
      <%--              <tr>
                        <td colspan="4" style="padding-left:15px">
                           <br /> 1.2 ค่าอาหาร (จัดไม่ครบ 3 มื้อ) <span id="message_error_budget2" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                    <td colspan="4" style="padding-left:30px">
                     <div class="form-horizontal" style="font-size:12pt">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <input type="radio" name="Food2Meals"  data-bind="checked: Food2Meals" value="1"/>จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ (ไม่เกิน 400 บาท/วัน/คน)
                        </div>
                        <div class="col-sm-12">
                            <input type="radio" name="Food2Meals"   data-bind="checked: Food2Meals" value="2"/>จัดในโรงแรม (ไม่เกิน 700 บาท/วัน/คน) 
                        </div>
                    </div>
                    </div>
                        </td>
                   </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          อาหารกลางวัน
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_1_2_2();"   id="B1_1_2_2_L_1" data-bind="kendoNumericTextBox: B1_1_2_2_L_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_1_2_2();" id="B1_1_2_2_L_2" data-bind="kendoNumericTextBox: B1_1_2_2_L_2" /> มื้อ x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_1_2_2();" id="B1_1_2_2_L_3" data-bind="kendoNumericTextBox: B1_1_2_2_L_3" /> บาท  
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_1_2_2_L_total" data-bind="text: B1_1_2_2_L_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          อาหารเย็น
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_1_2_2(this);" id="B1_1_2_2_D_1" data-bind="kendoNumericTextBox: B1_1_2_2_D_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_1_2_2(this);" id="B1_1_2_2_D_2" data-bind="kendoNumericTextBox: B1_1_2_2_D_2" /> มื้อ x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_1_2_2(this);" id="B1_1_2_2_D_3" data-bind="kendoNumericTextBox: B1_1_2_2_D_3" /> บาท  
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_1_2_2_D_total" data-bind="text: B1_1_2_2_D_total">0.00</span> บาท
                        </td>
                    </tr>   --%>

                    <!-- 2. ค่าอาหารว่างและเครื่องดื่ม-->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>2. ค่าอาหารว่างและเครื่องดื่ม</b>
                        </td>
                    </tr>
                    <tr class="trshow2_1">
                        <td colspan="4" style="padding-left:15px">
                            <br />2.1 จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ (ไม่เกิน 35 บาท/คน/มื้อ)   <span id="message_error_budget3" style="color:red"></span>
                        </td>
                    </tr>
                    <tr class="trshow2_1">
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   id="B1_2_1_1" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_2_1_2" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_1_2" /> มื้อ x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_2_1_3" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_1_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_2_1_total" data-bind="text: B1_2_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr class="trshow2_2">
                        <td colspan="4" style="padding-left:15px">
                          <br />  2.2 จัดในสถานที่เอกชนหรือโรงแรม (ไม่เกิน 50 บาท/คน/มื้อ) <span id="message_error_budget4" style="color:red"></span>
                        </td>
                    </tr>
                    <tr class="trshow2_2">
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   id="B1_2_2_1" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_2_2_2" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_2_2" /> มื้อ x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" id="B1_2_2_3" onblur="khProjBG.B1_2();" data-bind="kendoNumericTextBox: B1_2_2_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_2_2_total" data-bind="text: B1_2_2_total">0.00</span> บาท
                        </td>
                    </tr>

                    <!-- 3. ค่าที่พัก -->
                    <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>3. ค่าที่พัก</b>
                        </td>
                    </tr>
                    <tr  class="trshow3_1">
                        <td colspan="4" style="padding-left:15px">
                            <br />3.1 กรณีจัดในโรงแรม
                        </td>
                    </tr>
                    <tr class="trshow3_1">
                        <td colspan="4" style="padding-left:30px">
                          <br />3.1.1 ค่าเช่าห้องพักคนเดียว (ไม่เกิน 1,450 บาท/คน/คืน) <span id="message_error_budget5" style="color:red"></span>
                        </td>
                    </tr>
                    <tr class="trshow3_1">
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_1_1" data-bind="kendoNumericTextBox: B1_3_1_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_1_2" data-bind="kendoNumericTextBox: B1_3_1_1_2" /> คืน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_1_3" data-bind="kendoNumericTextBox: B1_3_1_1_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_3_1_1_total" data-bind="text: B1_3_1_1_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr  class="trshow3_1">
                        <td colspan="4" style="padding-left:30px">
                          3.1.2 ค่าเช่าห้องพักคู่ (ไม่เกิน 900 บาท/คน/คืน)<span id="message_error_budget6" style="color:red"></span>
                        </td>
                    </tr>
                    <tr  class="trshow3_1">
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();"   id="B1_3_1_2_1" data-bind="kendoNumericTextBox: B1_3_1_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_2_2" data-bind="kendoNumericTextBox: B1_3_1_2_2" /> คืน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_2_3" data-bind="kendoNumericTextBox: B1_3_1_2_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_3_1_2_total" data-bind="text: B1_3_1_2_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr  class="trshow3_1">
                        <td colspan="4" style="padding-left:30px">
                          3.1.3 กรณีผู้จัดไม่จัดที่พัก (เหมาจ่ายไม่เกิน 500 บาท/คน/วัน)<span id="message_error_budget7" style="color:red"></span>
                        </td>
                    </tr>
                    <tr  class="trshow3_1">
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();"  id="B1_3_1_3_1" data-bind="kendoNumericTextBox: B1_3_1_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_3_2" data-bind="kendoNumericTextBox: B1_3_1_3_2" /> คืน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_1();" id="B1_3_1_3_3" data-bind="kendoNumericTextBox: B1_3_1_3_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_3_1_3_total" data-bind="text: B1_3_1_3_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr class="trshow3_2">
                        <td colspan="4" style="padding-left:15px">
                            <br />3.2 กรณีที่พักเป็นสถานที่ราชการ วัด หรือเอกชน แต่ไม่ออกใบเสร็จ
                        </td>
                    </tr>
                   <tr class="trshow3_2">
                        <td colspan="4" style="padding-left:30px">
                          <br />3.2.1 เหมาจ่ายเป็นค่าสาธารณูปโภค (ไม่เกิน 300 บาท/วัน/คน) <span id="message_error_budget8" style="color:red"></span>
                        </td>
                    </tr>
                    <tr class="trshow3_2">
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_2();"  id="B1_3_2_1_1" data-bind="kendoNumericTextBox: B1_3_2_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_3_2();" id="B1_3_2_1_2" data-bind="kendoNumericTextBox: B1_3_2_1_2" /> คืน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_3_2();" id="B1_3_2_1_3" data-bind="kendoNumericTextBox: B1_3_2_1_3" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_3_2_1_total" data-bind="text: B1_3_2_1_total" >0.00</span> บาท
                        </td>
                    </tr>

                <!-- 4. ค่าพาหนะ -->
                   <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>4. ค่าพาหนะ</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />4.1 เดินทางภายในจังหวัดหน่วยจัด (เบิกจ่ายตามจริงไม่เกิน 1,000 บาท)<span id="message_error_budget9" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_4_1();"  id="B1_4_1_1" data-bind="kendoNumericTextBox: B1_4_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_1_3" data-bind="kendoNumericTextBox: B1_4_1_3" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_1_2" data-bind="kendoNumericTextBox: B1_4_1_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_1_total" data-bind="text: B1_4_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />4.2 เดินทางจากจังหวัดที่มีพื้นที่ติดต่อกับหน่วยจัด (เบิกจ่ายตามจริงไม่เกิน 1,200 บาท) <span id="message_error_budget10" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_4_1();" id="B1_4_2_1" data-bind="kendoNumericTextBox: B1_4_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_2_3" data-bind="kendoNumericTextBox: B1_4_2_3" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_2_2" data-bind="kendoNumericTextBox: B1_4_2_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_2_total" data-bind="text: B1_4_2_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />4.3 เดินทางจากทั่วประเทศมายังหน่วยจัด (เบิกจ่ายตามจริง)<span id="message_error_budget11" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_3_1" data-bind="kendoNumericTextBox: B1_4_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_3_3" data-bind="kendoNumericTextBox: B1_4_3_3" /> วัน x
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();" id="B1_4_3_2" data-bind="kendoNumericTextBox: B1_4_3_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_3_total" data-bind="text: B1_4_3_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />4.4 เดินทางโดยเครื่องบิน 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                             4.4.1 วิทยากร เดินทางโดยเครื่องบินชั้นประหยัด (เบิกจ่ายตามจริง)<span id="message_error_budget12" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_4_1();" id="B1_4_4_1_1" data-bind="kendoNumericTextBox: B1_4_4_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_4_1_3" data-bind="kendoNumericTextBox: B1_4_4_1_3" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_4_1_2" data-bind="kendoNumericTextBox: B1_4_4_1_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_4_4_1_total" data-bind="text: B1_4_4_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                             4.4.2 เจ้าหน้าที่โครงการ ไม่เกิน 3 คน เดินทางโดยสายการบินต้นทุนต่ำ (เบิกจ่ายตามจริง)<span id="message_error_budget13" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"   id="B1_4_4_2_1" data-bind="kendoNumericTextBox: B1_4_4_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_4_2_3" data-bind="kendoNumericTextBox: B1_4_4_2_3" /> วัน x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_4_2_2" data-bind="kendoNumericTextBox: B1_4_4_2_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_4_2_total" data-bind="text: B1_4_4_2_total" >0.00</span> บาท
                        </td>
                    </tr>
                   <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />4.5 ค่าชดเชยกรณีใช้ยานพาหนะส่วนตัว <span id="message_error_budget14" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                             4.5.1 รถยนต์ กิโลเมตรละ 4 บาท (เบิกจ่ายตามจริง)<span id="message_error_budget15" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_4_1();"  id="B1_4_5_1_1" data-bind="kendoNumericTextBox: B1_4_5_1_1" /> กิโลเมตร x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_5_1_3" data-bind="kendoNumericTextBox: B1_4_5_1_3"  /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_5_1_2" data-bind="kendoNumericTextBox: B1_4_5_1_2"  /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_5_1_total" data-bind="text: B1_4_5_1_total"  >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                             4.5.2 รถจักรยานยนต์ กิโลเมตรละ 2 บาท (เบิกจ่ายตามจริง)<span id="message_error_budget16" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"   id="B1_4_5_2_1" data-bind="kendoNumericTextBox: B1_4_5_2_1" /> กิโลเมตร x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_5_2_3" data-bind="kendoNumericTextBox: B1_4_5_2_3" /> วัน x  
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_4_1();"  id="B1_4_5_2_2" data-bind="kendoNumericTextBox: B1_4_5_2_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span id="B1_4_5_2_total"  data-bind="text: B1_4_5_2_total">0.00</span> บาท
                        </td>
                    </tr>
                    <!-- 5. -->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>5. ค่าเบี้ยเลี้ยงสำหรับเจ้าหน้าที่โครงการในวันเดินทางไปและกลับ (ไม่เกิน 240 บาท/วัน/คน) <span id="message_error_budget17" style="color:red"></span></b>
                        </td>
                  </tr>
                  <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_5();"  id="B1_5_1" data-bind="kendoNumericTextBox: B1_5_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_5();"  id="B1_5_2" data-bind="kendoNumericTextBox: B1_5_2" /> วัน 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_5();" id="B1_5_3" data-bind="kendoNumericTextBox: B1_5_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_5_total" data-bind="text: B1_5_total">0.00</span> บาท
                        </td>
                    </tr>

                   <!-- 6. ค่าตอบแทนวิทยากร -->
                    <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>6. ค่าตอบแทนวิทยากร</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />6.1 ค่าตอบแทนวิทยากรบรรยาย (จะต้องไม่เกิน 1 คน/ชั่วโมง) <span id="message_error_budget18" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.1.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท) <span id="message_error_budget19" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
           <%--             <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_1_1_1" data-bind="kendoNumericTextBox: B1_6_1_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_1_2" data-bind="kendoNumericTextBox: B1_6_1_1_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_1_3" data-bind="kendoNumericTextBox: B1_6_1_1_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_1_1_total" data-bind="text: B1_6_1_1_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_1_1" data-bind="json: vue_6_1_1">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.1.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)<span id="message_error_budget20" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
<%--                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_1_2_1" data-bind="kendoNumericTextBox: B1_6_1_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_2_2" data-bind="kendoNumericTextBox: B1_6_1_2_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_2_3" data-bind="kendoNumericTextBox: B1_6_1_2_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_1_2_total" data-bind="text: B1_6_1_2_total"  >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_1_2" data-bind="json: vue_6_1_2">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.1.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ 
	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)<span id="message_error_budget20_1" style="color:red">

                        </td>
                    </tr>
                    <tr>
               <%--         <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_1_3_1" data-bind="kendoNumericTextBox: B1_6_1_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_3_2" data-bind="kendoNumericTextBox: B1_6_1_3_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_1_3_3" data-bind="kendoNumericTextBox: B1_6_1_3_3" /> บาท 
                        </td>--%>
                        <td colspan="4"  style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_1_3_total"  data-bind="text: B1_6_1_3_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_1_3" data-bind="json: vue_6_1_3">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>             
                   <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />6.2 ค่าตอบแทนวิทยากรอภิปราย (จะต้องไม่เกิน 5 คน/ชั่วโมง)<span id="message_error_budget21" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.2.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท) <span id="message_error_budget22" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
          <%--              <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_2_1_1" data-bind="kendoNumericTextBox: B1_6_2_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_1_2" data-bind="kendoNumericTextBox: B1_6_2_1_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_1_3" data-bind="kendoNumericTextBox: B1_6_2_1_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_2_1_total" data-bind="text: B1_6_2_1_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_2_1" data-bind="json: vue_6_2_1">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>  
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.2.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)<span id="message_error_budget23" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
              <%--          <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_2_1" data-bind="kendoNumericTextBox: B1_6_2_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_2_2" data-bind="kendoNumericTextBox: B1_6_2_2_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_2_3" data-bind="kendoNumericTextBox: B1_6_2_2_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_2_2_total" data-bind="text: B1_6_2_2_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_2_2" data-bind="json: vue_6_2_2">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.2.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ 
	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี) <span id="message_error_budget24" style="color:red"></span>

                        </td>
                    </tr>
                    <tr>
              <%--          <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_3_1" data-bind="kendoNumericTextBox: B1_6_2_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_3_2" data-bind="kendoNumericTextBox: B1_6_2_3_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_2_3_3" data-bind="kendoNumericTextBox: B1_6_2_3_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_2_3_total" data-bind="text: B1_6_2_3_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_2_3" data-bind="json: vue_6_2_3">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr> 
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />6.3 ค่าตอบแทนวิทยากรกลุ่ม (จะต้องไม่เกินกลุ่มละ 2 คน) <span id="message_error_budget25" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.3.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)<span id="message_error_budget26" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
          <%--              <td colspan="4">
                        <table style="width:100%;"><tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_1_4" data-bind="kendoNumericTextBox: B1_6_3_1_4" /> กลุ่ม x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_1_1" data-bind="kendoNumericTextBox: B1_6_3_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_1_2" data-bind="kendoNumericTextBox: B1_6_3_1_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_1_3" data-bind="kendoNumericTextBox: B1_6_3_1_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_3_1_total" data-bind="text: B1_6_3_1_total">0.00</span> บาท
                        </td>
                        <%--</tr></table>
                        </td>--%>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_3_1" data-bind="json: vue_6_3_1">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.3.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)<span id="message_error_budget27" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
          <%--              <td colspan="4">
                        <table style="width:100%;"><tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_2_4" data-bind="kendoNumericTextBox: B1_6_3_2_4" /> กลุ่ม x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_2_1" data-bind="kendoNumericTextBox: B1_6_3_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_2_2" data-bind="kendoNumericTextBox: B1_6_3_2_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_2_3" data-bind="kendoNumericTextBox: B1_6_3_2_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_3_2_total" data-bind="text: B1_6_3_2_total">0.00</span> บาท
                        </td>
                      <%--  </tr></table>
                        </td>--%>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_3_2" data-bind="json: vue_6_3_2">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.3.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ 
	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)<span id="message_error_budget28" style="color:red"></span>

                        </td>
                    </tr>
                    <tr>
     <%--                   <td colspan="4">
                        <table style="width:100%;"><tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();" onblur="khProjBG.B1_6();" id="B1_6_3_3_4" data-bind="kendoNumericTextBox: B1_6_3_3_4" /> กลุ่ม x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_3_1" data-bind="kendoNumericTextBox: B1_6_3_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_3_2" data-bind="kendoNumericTextBox: B1_6_3_3_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();"  onblur="khProjBG.B1_6();" id="B1_6_3_3_3" data-bind="kendoNumericTextBox: B1_6_3_3_3" /> บาท 
                        </td>--%>
                        <td colspan="4"  style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_3_3_total" data-bind="text: B1_6_3_3_total">0.00</span> บาท
                        </td>
                      <%--  </tr></table>
                        </td>--%>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_3_3" data-bind="json: vue_6_3_3">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                 <tr>

                 <td colspan="4" style="padding-left:15px">
                            <br />6.4 ค่าตอบแทนวิทยากรฝึกอาชีพ 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.4.1 วิทยากรฝึกอาชีพทั่วไป (ไม่เกินชั่วโมงละ 400 บาท)<span id="message_error_budget29" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
               <%--         <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_6();" id="B1_6_4_1_1" data-bind="kendoNumericTextBox: B1_6_4_1_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" id="B1_6_4_1_2" data-bind="kendoNumericTextBox: B1_6_4_1_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" id="B1_6_4_1_3" data-bind="kendoNumericTextBox: B1_6_4_1_3" /> บาท 
                        </td>--%>
                        <td colspan="4" style="padding-left:10px">
                            ยอดรวม <span id="B1_6_4_1_total" data-bind="text: B1_6_4_1_total" >0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_4_1" data-bind="json: vue_6_4_1">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />6.4.2 วิทยากรภาคฝึกอาชีพเชี่ยวชาญ ต้องมีวุฒิบัตรหรือเอกสารราชการรับจริงในสาขาที่อบรม 
	  (ไม่เกินชั่วโมงละ 1,200 บาท และไม่เกิน 12 ชั่วโมง/หลักสูตร)<span id="message_error_budget30" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
        <%--                <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_6();" id="B1_6_4_2_1" data-bind="kendoNumericTextBox: B1_6_4_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" id="B1_6_4_2_2" data-bind="kendoNumericTextBox: B1_6_4_2_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_6();" id="B1_6_4_2_3" data-bind="kendoNumericTextBox: B1_6_4_2_3" /> บาท 
                        </td>--%>
                        <td colspan="4"  style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_6();" id="B1_6_4_2_total" data-bind="text: B1_6_4_2_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div6_4_2" data-bind="json: vue_6_4_2">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    <!-- 7. -->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>7. ค่าตอบแทนล่ามภาษามือ (ชั่วโมงละ 600 บาท และไม่เกิน 6 ชั่วโมง/วัน)</b><span id="message_error_budget31" style="color:red"></span>
                        </td>
                  </tr>
                  <tr>
                        <td colspan="4"  style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1_7();" id="B1_7_total" data-bind="text: B1_7_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div1_7" data-bind="json: vue1_7">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
<%--                  <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_7();"  id="B1_7_1" data-bind="kendoNumericTextBox: B1_7_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_7();" id="B1_7_2" data-bind="kendoNumericTextBox: B1_7_2" /> ชั่วโมง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_7();" id="B1_7_3" data-bind="kendoNumericTextBox: B1_7_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_7_total" data-bind="text: B1_7_total">0.00</span> บาท
                        </td>
                    </tr>--%>
             <!-- 8. ค่าเช่าสถานที่ดำเนินโครงการ -->
                   <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>8. ค่าเช่าสถานที่ดำเนินโครงการ</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />8.1 กรณีจัดในโรงแรม (เบิกจ่ายตามจริง เหมาะสมและประหยัด)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_8();" id="B1_8_1_1" data-bind="kendoNumericTextBox: B1_8_1_1" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_8();" id="B1_8_1_2" data-bind="kendoNumericTextBox: B1_8_1_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_8_1_total" data-bind="text: B1_8_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />8.2 กรณีจัดในสถานที่ราชการ (เบิกจ่ายตามจริง เหมาะสมและประหยัด)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_8();" id="B1_8_2_1" data-bind="kendoNumericTextBox: B1_8_2_1" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_8();" id="B1_8_2_2" data-bind="kendoNumericTextBox: B1_8_2_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_8_2_total"  data-bind="text: B1_8_2_total">0.00</span> บาท
                        </td>
                    </tr>
                 <tr>

                 <td colspan="4" style="padding-left:15px">
                            <br />8.3 กรณีจัดในสถานที่เอกชน
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />8.3.1 ระยะเวลาการดำเนินโครงการ ไม่เกิน 5 วัน (ไม่เกินวันละ 5,000 บาท)<span id="message_error_budget32" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_8();" id="B1_8_3_1_1" data-bind="kendoNumericTextBox: B1_8_3_1_1" /> วัน x 
                        </td>

                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_8();" id="B1_8_3_1_2" data-bind="kendoNumericTextBox: B1_8_3_1_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_8();" id="B1_8_3_1_total" data-bind="text: B1_8_3_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                          <br />8.3.2 ระยะเวลาการดำเนินโครงการ มากกว่า 5 วัน (ให้เหมาจ่ายไม่เกิน 30,000 บาท/โครงการ)<span id="message_error_budget33" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_8();" id="B1_8_3_2" data-bind="kendoNumericTextBox: B1_8_3_2" /> บาท 
                        </td>
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_8();" id="B1_8_3_2_total" data-bind="text: B1_8_3_2_total" >0.00</span> บาท
                        </td>
                    </tr>
            <!-- 9. ค่าเช่ารถ -->
                   <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>9. ค่าเช่ารถ</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />9.1 ค่าเช่ารถตู้ปรับอากาศ (ไม่เกิน 1,800 บาท/คัน/วัน)<span id="message_error_budget34" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_1_1" data-bind="kendoNumericTextBox: B1_9_1_1" /> คัน x 
                        </td>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_1_2" data-bind="kendoNumericTextBox: B1_9_1_2" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_1_3" data-bind="kendoNumericTextBox: B1_9_1_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_9_1_total" data-bind="text: B1_9_1_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />9.2 ค่าเช่ารถบัส
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br />9.2.1 รถบัสแบบพัดลม (ไม่เกินวันละ 5,500 บาท/คัน/วัน)<span id="message_error_budget35" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_1_1" data-bind="kendoNumericTextBox: B1_9_2_1_1" /> คัน x 
                        </td>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_1_2" data-bind="kendoNumericTextBox: B1_9_2_1_2" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_1_3" data-bind="kendoNumericTextBox: B1_9_2_1_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_9_2_1_total" data-bind="text: B1_9_2_1_total" >0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br />9.2.2 รถบัสปรับอากาศ 30 – 32 ที่นั่ง (ไม่เกินวันละ 8,000 บาท/คัน/วัน)<span id="message_error_budget36" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_2_1" data-bind="kendoNumericTextBox: B1_9_2_2_1" /> คัน x 
                        </td>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_2_2" data-bind="kendoNumericTextBox: B1_9_2_2_2" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_2_3" data-bind="kendoNumericTextBox: B1_9_2_2_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_9_2_2_total" data-bind="text: B1_9_2_2_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br />9.2.3 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 45 ที่นั่ง (ไม่เกินวันละ 12,000 บาท/คัน/วัน)<span id="message_error_budget37" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_9();" id="B1_9_2_3_1" data-bind="kendoNumericTextBox: B1_9_2_3_1" /> คัน x 
                        </td>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_9();" id="B1_9_2_3_2" data-bind="kendoNumericTextBox: B1_9_2_3_2" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_3_3" data-bind="kendoNumericTextBox: B1_9_2_3_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_9_2_3_total" data-bind="text: B1_9_2_3_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br />9.2.4 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 50 ที่นั่ง (ไม่เกินวันละ 15,000 บาท/คัน/วัน)<span id="message_error_budget38" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_4_1" data-bind="kendoNumericTextBox: B1_9_2_4_1" /> คัน x 
                        </td>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_4_2" data-bind="kendoNumericTextBox: B1_9_2_4_2" /> วัน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_9();" id="B1_9_2_4_3" data-bind="kendoNumericTextBox: B1_9_2_4_3" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_9_2_4_total" data-bind="text: B1_9_2_4_total">0.00</span> บาท
                        </td>
                    </tr>
                    <!-- 10. ค่าน้ำมันเชื้อเพลิง (เบิกจ่ายตามจริง) -->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>10. ค่าน้ำมันเชื้อเพลิง (เบิกจ่ายตามจริง)</b>
                        </td>
                  </tr>
                  <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_10();" id="B1_10_1" data-bind="kendoNumericTextBox: B1_10_1" /> คัน x 
                        </td>

                        <td  style="padding-left:10px">
                            <input type="text" min="0"  onblur="khProjBG.B1_10();" id="B1_10_2" data-bind="kendoNumericTextBox: B1_10_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_10_total" data-bind="text: B1_10_total">0.00</span> บาท
                        </td>
                    </tr>
             <!-- 11. ค่าเช่าสถานที่ดำเนินโครงการ -->
                   <tr>
                         <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>11. ค่าเอกสารประกอบการอบรม</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />11.1 เอกสารทั่วไป (ไม่เกิน 100 บาท/คน/หลักสูตร)<span id="message_error_budget39" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_11();" id="B1_11_1_1" data-bind="kendoNumericTextBox: B1_11_1_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_11();" id="B1_11_1_2" data-bind="kendoNumericTextBox: B1_11_1_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_11_1_total" data-bind="text: B1_11_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />11.2 เอกสารอักษรเบรลล์ (ไม่เกิน 200 บาท/ชุด)<span id="message_error_budget40" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_11();" id="B1_11_2_1" data-bind="kendoNumericTextBox: B1_11_2_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_11();" id="B1_11_2_2" data-bind="kendoNumericTextBox: B1_11_2_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_11_2_total" data-bind="text: B1_11_2_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />11.3 เอกสารเสียงหรือซีดี (ไม่เกินแผ่นละ 20 บาท)<span id="message_error_budget41" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_11();" id="B1_11_3_1" data-bind="kendoNumericTextBox: B1_11_3_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_11();" id="B1_11_3_2" data-bind="kendoNumericTextBox: B1_11_3_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span id="B1_11_3_total" data-bind="text: B1_11_3_total">0.00</span> บาท
                        </td>
                    </tr>
                    <!-- 12. ค่ากระเป๋าผ้า (ไม่เกิน 80 บาท/ใบ/คน) -->
<%--                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>12. ค่ากระเป๋าผ้า (ไม่เกิน 80 บาท/ใบ/คน)</b><span id="message_error_budget42" style="color:red"></span>
                        </td>
                  </tr>
                  <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_12();" id="B1_12_1" data-bind="kendoNumericTextBox: B1_12_1" /> คน x 
                        </td>

                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_12();" id="B1_12_2" data-bind="kendoNumericTextBox: B1_12_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span  id="B1_12_total" data-bind="text: B1_12_total">0.00</span> บาท
                        </td>
                    </tr>--%>
                    <!-- 13. -->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>12. ค่าวัสดุฝึกอบรมหรือฝึกอาชีพ (ตามความจำเป็นของแต่ละโครงการ)</b><span id="message_error_budget43" style="color:red"></span>
                        </td>
                  </tr>
                  <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_13();" id="B1_13_1" data-bind="kendoNumericTextBox: B1_13_1" /> คน x 
                        </td>

                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_13();" id="B1_13_2" data-bind="kendoNumericTextBox: B1_13_2" /> บาท 
                        </td>

                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_13();" id="B1_13_total" data-bind="text: B1_13_total">0.00</span> บาท
                        </td>
                    </tr>

                   <tr>
                        <td colspan="4" style="padding-left:30px">
                            <b>อื่นๆ</b> <br /><span id="message_error_budget44" style="color:red"></span>
                             <input type="text" min="0"   onblur="khProjBG.B1_13();" id="B1_13_other" data-bind="kendoNumericTextBox: B1_13_other" /> บาท
                        </td>
                  </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            <br /><textarea cols="60" rows="5" onblur="khProjBG.B1_13();" id="B1_13_text" data-bind="value: B1_13_text"></textarea> 
                        </td>
                    </tr>
                    <!-- 13. รายการค่าใช้จ่ายอื่นๆ (B1other_expense) -->
                   <tr>
                        <td colspan="4">
                            <hr style="width:80%;position:center" />
                            <b>13. รายการค่าใช้จ่ายอื่นๆ </b><span id="message_error_B1OtherExpense" style="color:red"></span>
                        </td>
                  </tr>
                  <tr>
                        <td colspan="4"  style="padding-left:10px">
                            ยอดรวม <span onblur="khProjBG.B1OtherExpense;" id="B1OtherExpense_total" data-bind="text: B1OtherExpense_total">0.00</span> บาท
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="padding-left:10px">
                        <div id="div1_B1OtherExpense" data-bind="json: vue1_B1OtherExpense">
                            <budget-grid @on-input-blur="onDataChange" v-bind:data="$data" ></budget-grid>
                        </div>
 
                        </td>
                    </tr>
                    
                   </table> <!-- 14. -->
                   <table id="tabManageBudget" style="width:100%">
                   <tr>
                        <td colspan="4">
                    <%--        <hr style="width:80%;position:center" />--%>
                            <b>14. ค่าบริหารจัดการโครงการ (จะต้องไม่เกิน 10% ของค่าใช้จ่ายทั้งหมดของโครงการที่ได้รับการอนุมัติ)</b>
                            <p style="margin-left:24px">ค่าใช้จ่ายทั้งหมดโครงการ <span class="numberformat" id="sum1to13" data-bind="text: sum1to13">0.00</span> บาท</p>
                            <p style="margin-left:24px">ค่าบริหารโครงการไม่เกิน 10% ของค่าใช้จ่ายทั้งหมดของโครงการ = <span class="numberformat" id="cal10per" data-bind="text: cal10per">0.00</span> บาท</p>
                            <p style="margin-left:24px">ค่าบริหารโครงการที่เสนอของ <span class="numberformat" id="total14all" data-bind="text: total14all">0.00</span> บาท <span id="message_error_budget45" style="color:red"></p>
                        </td>
                  </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.1 ค่าใช้จ่ายในการติดตามหรือประเมินผลโครงการหรือถอดบทเรียน
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_1_1" data-bind="kendoNumericTextBox: B1_14_1_1" /> ครั้ง x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_1_2" data-bind="kendoNumericTextBox: B1_14_1_2" /> บาท 
                        </td2

                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_1_total" data-bind="text: B1_14_1_total">0.00</span> บาท
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:30px">
                            รายละเอียด <br />
                            <textarea cols="60" rows="5" onblur="khProjBG.B1_14();" id="B1_14_1_text" data-bind="value: B1_14_1_text"></textarea> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.2 ตอบแทนผู้ช่วยเหลือคนพิการเฉพาะกิจ (ไม่เกิน 300 บาท/คน/วัน)<span id="message_error_budget46" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_2_1" data-bind="kendoNumericTextBox: B1_14_2_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_2_2" data-bind="kendoNumericTextBox: B1_14_2_2" /> วัน x 
                        </td>
                         <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_2_3" data-bind="kendoNumericTextBox: B1_14_2_3" /> บาท
                        </td>
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_2_total" data-bind="text: B1_14_2_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.3 ตอบแทนอาสาสมัครที่ทำหน้าที่ประสานงานโครงการ (ไม่เกิน 300 บาท/คน/วัน)<span id="message_error_budget47" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_3_1" data-bind="kendoNumericTextBox: B1_14_3_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_3_2" data-bind="kendoNumericTextBox: B1_14_3_2" /> วัน x 
                        </td>
                         <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_3_3" data-bind="kendoNumericTextBox: B1_14_3_3" /> บาท
                        </td>
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_3_total" data-bind="text: B1_14_3_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.4 ค่าจ้างเจ้าหน้าที่บันทึกวีดีโอภาษามือ (ไม่เกิน 700 บาท/คน/วัน) <span id="message_error_budget48" style="color:red"></span></br>
	(สำหรับโครงการของคนพิการทางการได้ยินหรือสื่อความหมาย)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_4_1" data-bind="kendoNumericTextBox: B1_14_4_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_4_2" data-bind="kendoNumericTextBox: B1_14_4_2" /> วัน x 
                        </td>
                         <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_4_3" data-bind="kendoNumericTextBox: B1_14_4_3" /> บาท
                        </td>
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_4_total" data-bind="text: B1_14_4_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.5 ค่าจัดทำและบันทึกวีดีโอหรือซีดี (ตามความจำเป็นและเหมาะสมของแต่ละโครงการ)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_5_1" data-bind="kendoNumericTextBox: B1_14_5_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_5_2" data-bind="kendoNumericTextBox: B1_14_5_2" /> บาท 
                        </td>
  
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_5_total" data-bind="text: B1_14_5_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.6 ค่าถ่ายภาพและล้างอัดขยายภาพ (ตามความจำเป็นและเหมาะสมของแต่ละโครงการ)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_6_1" data-bind="kendoNumericTextBox: B1_14_6_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_6_2" data-bind="kendoNumericTextBox: B1_14_6_2" /> บาท 
                        </td>
  
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_6_total" data-bind="text: B1_14_6_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.7 ค่าจัดทำเอกสารรายงานผลโครงการ (ตามความจำเป็นและเหมาะสมของแต่ละโครงการ)
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_7_1" data-bind="kendoNumericTextBox: B1_14_7_1" /> ชุด x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_7_2" data-bind="kendoNumericTextBox: B1_14_7_2" /> บาท 
                        </td>
  
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_7_total" data-bind="text: B1_14_7_total">0.00</span> บาท
                        </td>
                    </tr>
                  <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.8 ค่าจดบันทึกการประชุม (1,000 บาท/คน และไม่เกิน 2 คน/โครงการ)<span id="message_error_budget49" style="color:red"></span>
                        </td>
                    </tr>

                    <tr>
                        <td  style="padding-left:30px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_8_1" data-bind="kendoNumericTextBox: B1_14_8_1" /> คน x 
                        </td>
                        <td  style="padding-left:10px">
                            <input type="text" min="0" onblur="khProjBG.B1_14();" id="B1_14_8_2" data-bind="kendoNumericTextBox: B1_14_8_2" /> บาท 
                        </td>
  
                        <td  style="padding-left:10px">
                            = <span onblur="khProjBG.B1_14();" id="B1_14_8_total" data-bind="text: B1_14_8_total">0.00</span> บาท
                        </td>
                    </tr>
                   <tr>
                        <td colspan="4" style="padding-left:15px">
                            <br />14.9 ค่าใช้จ่ายอื่นๆ ตามความจำเป็น <button onclick="addOtherClick(); return false;">เพิ่มรายการ</button><br />
                             <%--<input type="text" min="0"   onblur="khProjBG.B1_14();" id="B1_14_9_other" data-bind="kendoNumericTextBox: B1_14_9_other" /> บาท--%>
                        </td>
                  </tr>
         <%--           <tr>
                        <td colspan="4"  style="padding-left:30px">
                            <br /><textarea cols="60" rows="5" id="B1_14_9_text" data-bind="value: B1_14_9_text"></textarea> 
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4" >
                            <div id="othertmp" data-bind="json: other_expenses" ></div>
                            <div id="tableGrid">
                            <table  style="width:90%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">
                            <thead class="k-grid-header" role="rowgroup">
                            <tr role="row">
                                <th scope="col" role="columnheader" class="k-header" style="width:80px"></th>
                                <th scope="col" role="columnheader" class="k-header" style="width:80px" >รายละเอียด</th>
                                <th scope="col" role="columnheader" class="k-header" style="width:120px">บาท</th>    
                        
                            </tr>
                                <template v-for="(i,idx) in items">
                                    <tr>
                                        <td><button @click="removeRow(idx, $event)">ลบ</button> </td>
                                        <td><textarea cols="60" rows="3" v-model="items[idx].detail"   ></textarea> </td>
                                        <td><vue-numeric onblur="khProjBG.B1_14();"  separator="," v-bind:precision="2" :empty-value="0" v-model="items[idx].amount" ></vue-numeric></td>
                                    </tr>
                                </template>
                             </thead>
                            </table>
                           </div>
                        </td>
                    </tr>
                </table>
                          
 
                </div>                                             
                </div>
                <div id="divBudgetType2" style="display:block;font-size:12pt" class="td ">
                <div class="form-horizontal">
                <div class="form-group form-group-sm">
                        <div id="divSupportBudgetType1" class="col-sm-6 divSupportBudgetType1">
                            <input type="radio" id="rdSupportBudgetType1" name="SupportBudgetType" onchange="SelectSupportBudgetPanel();khProjBG.RadioBudgetClick();" data-bind="checked: SupportBudgetType" value="1"/>กรณีจัดตั้งโดยองค์กรด้านคนพิการ หรือองค์กรอื่นใดที่ให้บริการแก่คนพิการ ซึ่งได้รับการรับรองมาตรฐาน ตามมาตรา 6(10)
                        </div>
                        <div id="divSupportBudgetType2" class="col-sm-6 divSupportBudgetType2">
                            <input type="radio" id="rdSupportBudgetType2"  name="SupportBudgetType" onchange="SelectSupportBudgetPanel();khProjBG.RadioBudgetClick();" data-bind="checked: SupportBudgetType" value="2"/>กรณีจัดตั้งโดยราชการส่วนท้องถิ่นหรือหน่วยงานภาครัฐ
                        </div>
                    </div>
                </div>

                <table style="width:100%" border="1" class="td-border"> 
                        <tr>
                            <td style="width:40%">รายการค่าใช้จ่าย</td>
                            <td class="B21">กรณีจัดตั้งโดยองค์กรด้านคนพิการ หรือองค์กรอื่นใดที่ให้บริการแก่คนพิการ ซึ่งได้รับการรับรองมาตรฐาน ตามมาตรา 6(10)</td>
                            <td class="B22">กรณีจัดตั้งโดยราชการส่วนท้องถิ่นหรือหน่วยงานภาครัฐ</td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>หมวดที่ 1 สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป</b></td>
                        </tr>
                        <tr>
                            <td>1.ค่าจัดสิ่งอำนวยความสะดวกสำหรับคนพิการ</td>
                            <td class="B21"><input  type="text" min="0" id="B21_1_1" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: {value: B21_1_1, spinners: false,format: '#,###.##'}" /><span id="b21_message_error1" style="color:red"></sapn> </td>
                            <td class="B22"><input type="text" min="0" id="B22_1_1"  onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_1_1, spinners: false, format: '#,###.##' }" /><span id="b22_message_error1" style="color:red"></sapn> </td>
                        </tr>
                        <tr>
                            <td>2.ค่าจัดทำป้ายชื่อศูนย์บริการคนพิการทั่วไป</td>
                            <td class="B21"><input type="text" min="0" onblur="khProjBG.B21();" id="B21_1_2" data-bind="kendoNumericTextBox: { value: B21_1_2, spinners: false, format: '#,###.##' }" /><span id="b21_message_error2" style="color:red"></sapn></td>
                            <td class="B22"><input type="text" min="0" onblur="khProjBG.B22();" id="B22_1_2" data-bind="kendoNumericTextBox: { value: B22_1_2, spinners: false, format: '#,###.##' }" /><span id="b22_message_error2" style="color:red"></sapn></td>
                        </tr>
                        <tr>
                            <td colspan="3"><b>หมวดที่ 2 การบริหารจัดการศูนย์บริการคนพิการทั่วไป</b></td>
                        </tr>
                        <tr>
                            <td>1.ค่าสาธารณูปโภค</td>
                            <td class="B21">
                                จ่ายตามจริงแต่ไม่เกิน 2,000 บาท/วัน <span id="b21_message_error3" style="color:red"></span> <br />
                                <input type="text" style="width:100px" min="0" onblur="khProjBG.B21();" id="B21_2_1_1"  data-bind="kendoNumericTextBox: { value: B21_2_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" style="width:100px" min="0" onblur="khProjBG.B21();" id="B21_2_1_2"  data-bind="kendoNumericTextBox: { value: B21_2_1_2, spinners: false, format: '#,###.##' }" /> วัน
                                
                            </td>
                            <td class="B22"> </td>
                        </tr>
                        <tr>
                            <td>2.ค่าตอบแทนผู้ปฏิบัติงาน</td>
                            <td class="B21">
                                อัตราค่าจ้างขั้นต่ำของแต่ละพื้นที่ตามประกาศ คณะกรรมการค่าจ้าง  <span id="b21_message_error4" style="color:red"></span> <br />
                                <input type="text" min="0" style="width:70px" id="B21_2_2_1" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_2_2_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" onblur="khProjBG.B21();" style="width:60px"  id="B21_2_2_2"  data-bind="kendoNumericTextBox: { value: B21_2_2_2, spinners: false, format: '#,###.##' }" /> คน
                                <input type="text" min="0" style="width:60px" onblur="khProjBG.B21();" id="B21_2_2_3"  data-bind="kendoNumericTextBox: { value: B21_2_2_3, spinners: false, format: '#,###.##' }" /> เดือน
                            </td>
                            <td class="B22"> </td>
                        </tr>
                        <tr>
                            <td>3.ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป</td>
                            <td class="B21">
                                ค่าอาหารกลางวัน ห้ามเกิน 120 <span id="b21_message_error5" style="color:red"></span> <br />
                                <input type="text" min="0" style="width:60px" id="B21_2_3_1_1" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_2_3_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_1_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_1_3"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <br />ค่าอาหารว่างและเครื่องดื่ม <span id="b21_message_error6" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B21_2_3_2_1"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_2_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_2_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_2_3"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <br />ค่าพาหนะเดินทาง (เบิกจ่ายตามจริง) <span id="b21_message_error7" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B21_2_3_3_1"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_3_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_3_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_3_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B21_2_3_3_3"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_3_3_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                            </td>
                            <td class="B22"> 
                             ค่าอาหารกลางวัน ห้ามเกิน 120 <span id="b22_message_error3" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B22_2_3_1_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_2_3_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_1_2"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_1_3"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <br />ค่าอาหารว่างและเครื่องดื่ม <span id="b22_message_error4" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B22_2_3_2_1"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_2_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_2_2"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_2_3"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <br />ค่าพาหนะเดินทาง (เบิกจ่ายตามจริง) <span id="b22_message_error5" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B22_2_3_3_1"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_3_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_3_2"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_3_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"  id="B22_2_3_3_3"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_3_3_3, spinners: false, format: '#,###.##' }" /> ครั้ง
                            </td>
                        </tr>
                        <tr>
                            <td>4.ค่าวัสดุอุปกรณ์สำนักงาน</td>
                            <td class="B21">10,000 บาท/ปี (ขอสนับสนุนครบ 12 เดือน)<span id="b21_message_error8" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_2_4_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_4_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B21_2_4_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_2_4_2, spinners: false, format: '#,###.##' }" /> เดือน
                            </td>

                            <td class="B22">10,000 บาท/ปี (ขอสนับสนุนครบ 12 เดือน)<span id="b22_message_error6" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B22_2_4_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_2_4_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"  id="B22_2_4_2"  onblur="khProjBG.B22();"data-bind="kendoNumericTextBox: { value: B22_2_4_2, spinners: false, format: '#,###.##' }" /> เดือน
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><b>หมวดที่ 3 การจัดบริการตามภารกิจของศูนย์บริการคนพิการทั่วไป</b></td>
                        </tr>
                        <tr>
                            <td>1.การประเมินศักยภาพคนพิการ และทำแผนพัฒนาศักยภาพ คนพิการรายบุคคล ก่อนการจัดบริการ</td>
                            <td class="B21"><input type="text" min="0" style="width:60px"   id="B21_3_1_1" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_3_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"   id="B21_3_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_1_2, spinners: false, format: '#,###.##' }" /> คน x 
                                <input type="text" min="0" style="width:60px"   id="B21_3_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง 
                                <span id="b21_message_error9" style="color:red"></span></td>
                            <td class="B22"></td>
                        </tr>
                        <tr>
                            <td>2.การฝึกทักษะด้านการทำ ความคุ้นเคยกับสภาพแวดล้อม และการเคลื่อนไหว (Orientation & Mobility : O&M) สำหรับ คนพิการทางการเห็น</td>
                            <td class="B21"><input type="text" min="0" onblur="khProjBG.B21();" style="width:60px"   id="B21_3_2_1"  data-bind="kendoNumericTextBox: { value: B21_3_2_1, spinners: false, format: '#,###.##' }" /> บาท x <input type="text" min="0" style="width:60px"   id="B21_3_2_2" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_3_2_2, spinners: false, format: '#,###.##' }" /> คน <span id="b21_message_error10" style="color:red"></span></td>
                            <td class="B22"></td>
                        </tr>
                        <tr>
                            <td>3.การบริการผู้ช่วยคนพิการ</td>
                            <td class="B21">
                                <input type="text" min="0" style="width:60px"   id="B21_3_3_1"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_3_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_3_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_3_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_3_3"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_3_3, spinners: false, format: '#,###.##' }" /> วัน <span id="b21_message_error11" style="color:red"></span>
                            </td>
                            <td class="B22">
                                <input type="text" min="0"  style="width:60px"   id="B22_3_3_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_3_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B22_3_3_2"  onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_3_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B22_3_3_3" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_3_3, spinners: false, format: '#,###.##' }" /> วัน <span id="b22_message_error7" style="color:red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>4.การบริการล่ามภาษามือ</td>
                            <td class="B21"><input type="text" min="0"  style="width:60px"   id="B21_3_4_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_4_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_4_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_4_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_4_3" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_3_4_3, spinners: false, format: '#,###.##' }" /> ชม. x 
                                <input type="text" min="0" style="width:60px"   id="B21_3_4_4" onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_3_4_4, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <span id="b21_message_error12" style="color:red"></span>
                            </td>
                            <td class="B22"><input type="text" min="0"   style="width:60px"   id="B22_3_4_1" onblur="khProjBG.B22();"  data-bind="kendoNumericTextBox: { value: B22_3_4_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B22_3_4_2" onblur="khProjBG.B22();"  data-bind="kendoNumericTextBox: { value: B22_3_4_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B22_3_4_3" onblur="khProjBG.B22();"   data-bind="kendoNumericTextBox: { value: B22_3_4_3, spinners: false, format: '#,###.##' }" /> ช่ม. x 
                                <input type="text" min="0" style="width:60px"   id="B22_3_4_4" onblur="khProjBG.B22();"   data-bind="kendoNumericTextBox: { value: B22_3_4_4, spinners: false, format: '#,###.##' }" /> ครั้ง
                                <span id="b22_message_error8" style="color:red"></span>
                            </td>
                        </tr>
                         <tr>
                             <td>5.การปรับสภาพแวดล้อม ที่อยู่อาศัยให้แก่คนพิการ</td>
                              <td class="B21">อัตราตามที่อธิบดีกรมส่งเสริม และพัฒนาคุณภาพชีวิต คนพิการประกาศ
                                <input type="text" min="0" style="width:100px"  id="B21_3_5" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_5, spinners: false, format: '#,###.##' }" /> บาท <br /><br />
                                 รายละเอียด<br />
                                   <textarea cols="60" rows="5"  id="B21_3_5_text" data-bind="value: B21_3_5_text"></textarea> 
                             </td>
                            <td class="B22">อัตราตามที่อธิบดีกรมส่งเสริม และพัฒนาคุณภาพชีวิต คนพิการประกาศ
                                <input type="text" min="0" style="width:100px"   id="B22_3_5" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_5, spinners: false, format: '#,###.##' }" /> บาท <br /><br />
                                รายละเอียด<br />
                                <textarea cols="60" rows="5"  id="B22_3_5_text" data-bind="value: B22_3_5_text"></textarea> 
                            </td>
                         </tr>
                        <tr>
                            <td>6.การฟื้นฟูสมรรถภาพ ทางด้านร่างกาย</td>
                            <td class="B21"><input type="text" min="0" style="width:60px"   id="B21_3_6_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_6_1, spinners: false, format: '#,###.##' }" />  บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_6_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_6_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_6_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_6_3, spinners: false, format: '#,###.##' }" /> ครั้ง. <span id="b21_message_error13" style="color:red"></span>
                            </td>
                            <td class="B22"></td>
                        </tr>
                        <tr>
                            <td>7.การพัฒนาทักษะการช่วยเหลือตนเอง</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error14" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error15" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_7_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_7_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr>    
                        <tr>
                            <td>8.การพัฒนาทักษะทางการพูด</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error16" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B21_3_8_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_1_1, spinners: false, format: '#,###.##' }" />  บาท x  
                                <input type="text" min="0" style="width:60px" id="B21_3_8_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px" id="B21_3_8_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error17" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B21_3_8_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_8_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_8_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_8_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr>       
                        <tr>
                            <td>9.การพัฒนาสู่สุขภาวะ</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error18" style="color:red"></span><br />
                                <input type="text" min="0"  style="width:60px"   id="B21_3_9_1_1" onblur="khProjBG.B21();"data-bind="kendoNumericTextBox: { value: B21_3_9_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_9_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_9_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_9_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_9_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error19" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_9_2_1" onblur="khProjBG.B21();"data-bind="kendoNumericTextBox: { value: B21_3_9_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_9_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_9_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_9_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_9_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr>       
                        <tr>
                            <td>10.การปรับพฤติกรรม</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error20" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error21" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_10_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_10_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr>       
                        <tr>
                            <td>11.การพัฒนาทักษะทางการได้ยิน</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error22" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_11_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_11_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_11_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_11_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_11_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_11_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                               
                            </td>
                            <td class="B22"></td>
                        </tr> 
                        <tr>
                            <td>12.การพัฒนาทักษะทางการเห็น</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error23" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px" id="B21_3_12_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_12_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_12_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error24" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_12_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_12_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_12_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_12_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr> 
                        <tr>
                            <td>13.การเสริมสร้างพัฒนาการ</td>
                            <td class="B21">รายบุคคล <span id="b21_message_error25" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_13_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_13_1_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_1_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_13_1_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_1_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                                <br />รายกลุ่ม <span id="b21_message_error26" style="color:red"></span><br />
                                <input type="text" min="0"  style="width:60px"   id="B21_3_13_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_2_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_13_2_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_2_2, spinners: false, format: '#,###.##' }" /> คน x
                                <input type="text" min="0" style="width:60px"   id="B21_3_13_2_3" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_13_2_3, spinners: false, format: '#,###.##' }" /> ครั้ง.
                            </td>
                            <td class="B22"></td>
                        </tr>   
                        <tr>
                            <td>14.บริการกายอุปกรณ์ </td>
                            <td class="B21">
                                รถโยกคนพิการ <span id="b21_message_error27_1" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_14_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_14_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_14_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_14_2, spinners: false, format: '#,###.##' }" /> อัน<br />
                                ไม้เท้าขาว <span id="b21_message_error27" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_14_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_14_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_14_2_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_14_2_1, spinners: false, format: '#,###.##' }" /> อัน
                            </td>
                            <td class="B22">
                                รถโยกคนพิการ <span id="b22_message_error9_1" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B22_3_14_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_14_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B22_3_14_2" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_14_2, spinners: false, format: '#,###.##' }" /> อัน<br />
                                ไม้เท้าขาว <span id="b22_message_error9" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B22_3_14_1_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_14_1_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B22_3_14_2_1" onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_14_2_1, spinners: false, format: '#,###.##' }" /> อัน
                            </td>
                        </tr> 
                        <tr>
                            <td>15.ค่าบริการประสานส่งต่อ </td>
                            <td class="B21">
                                <input type="text" min="0" style="width:60px"   id="B21_3_15_1"  onblur="khProjBG.B21();"  data-bind="kendoNumericTextBox: { value: B21_3_15_1, spinners: false, format: '#,###.##' }" /> บาท x  
                                <input type="text" min="0" style="width:60px"   id="B21_3_15_2" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_15_2, spinners: false, format: '#,###.##' }" /> คน<span id="b21_message_error28" style="color:red"></span>
                            </td>
                            <td class="B22"></td>
                        </tr>    
                        <tr>
                            <td>16.ค่าพาหนะนำพาคนพิการ เข้ารับบริการตามสิทธิ</td>
                            <td class="B21">รถจักรยานยนต์ <span id="b21_message_error29" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_16_1_1" onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_16_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"   id="B21_3_16_1_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_16_1_2, spinners: false, format: '#,###.##' }" /> กม
                                <br />รถยนต์ <span id="b21_message_error30" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B21_3_16_2_1"  data-bind="kendoNumericTextBox: { value: B21_3_16_2_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"   id="B21_3_16_2_2"  onblur="khProjBG.B21();" data-bind="kendoNumericTextBox: { value: B21_3_16_2_2, spinners: false, format: '#,###.##' }" /> กม
                            </td>
                            <td class="B22">รถจักรยานยนต์ <span id="b22_message_error10" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B22_3_16_1_1"  onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_16_1_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"   id="B22_3_16_1_2"  onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_16_1_2, spinners: false, format: '#,###.##' }" /> กม
                                <br />รถยนต์<span id="b22_message_error11" style="color:red"></span><br />
                                <input type="text" min="0" style="width:60px"   id="B22_3_16_2_1"  data-bind="kendoNumericTextBox: { value: B22_3_16_2_1, spinners: false, format: '#,###.##' }" /> บาท x 
                                <input type="text" min="0" style="width:60px"   id="B22_3_16_2_2"  onblur="khProjBG.B22();" data-bind="kendoNumericTextBox: { value: B22_3_16_2_2, spinners: false, format: '#,###.##' }" /> กม
                            </td>
                        </tr>                                                                                                                                                                                                                 
                    </table>                    
                </div>         
            </div>
        </div><!--รายละเอียดงบประมาณ-->
        
        <%--<div class="panel panel-default"><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleIsGotSuppot %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">      
                        <div class="col-sm-7 ">
                            <div>
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportNo" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="<%$ code:UI.LabelGotSupportNo %>"></asp:RadioButton>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" ID="RadioButtonGotSupportYes" CssClass="form-control-radio-horizontal" GroupName="GotSupport" Text="<%$ code:UI.LabelGotSupportName %>"></asp:RadioButton>
                                <asp:TextBox ID="TextBoxGotSupportName" runat="server" CssClass="form-control got-support-name" />
                                <div class="got-support-block">
                                    <asp:Label ID="LabelGotSupportAmount" Text="<%$ code:UI.LabelGotSupportAmount %>" runat="server"/> 
                                    <nep:TextBox ID="TextBoxGotSupportAmount" runat="server" CssClass="form-control" Width="120" 
                                       TextMode="Number" Min="1.00" Max="999999999.99"/>
                                    <asp:Label ID="Label1" Text="<%$ code:UI.LabelBath %>" runat="server" />
                                </div>
                            </div>
                            <div style="margin-top:4px;">
                                <asp:CustomValidator ID="CustomValidatorGotSupportInfo" runat="server" CssClass="error-text" 
                                OnServerValidate="CustomValidatorGotSupportInfo_ServerValidate"
                                ValidationGroup="SaveProjectBudget" />
                            </div>                            
                        </div>        
                        <div class="col-sm-5 ">
                            <span class="field-desc"><%: UI.LabelFieldRemark %></span><%=Nep.Project.Common.Web.WebUtility.DisplayInHtml(UI.LabelProjectBudgetIsGotSupportRemark) %>                                          
                        </div>
                    </div>
                </div>
            </div>
        </div>--%><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->
        <div id="divFooter" class="div-budget-status">
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    จำนวนเงินเสนอขอ <asp:Label runat="server" ID="LabelTotalBudgetAmount" Text="#,#0.00 "  ></asp:Label> บาท
                    <asp:Button runat="server"   ID="ButtonSave" CssClass="btn btn-primary btn-sm" Visible="false" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"  
                        OnClientClick=" if (!khProjBG.CheckProjectBudgetValidate()) return false; else return  GetQNModelToServer();"  
                        OnClick="ButtonSave_Click" ValidationGroup="SaveProjectBudget" />

                <%--    <asp:Button runat="server"  ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />

                    <asp:Button runat="server"  ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click"/>

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>

                    --%>
            
                <%--    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>--%>

                    <asp:Button ID="ButtonClose" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonClose %>" CssClass="btn btn-red btn-sm"
                            Onclientclick="c2x.closeFormDialog();return false;" causesvalidation="false"/>
                   
                   <%-- <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>--%>
                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red"></asp:Label>
                 </div>
                
            </div>
            
        </div>
         </div>
          <script src="../Scripts/bootstrap.min.js"></script>
        <script src="../Scripts/Vue/Utilities/accounting.umd.js"></script>
    <script src="../Scripts/Vue/vue_v2.5.21/vue.js"></script>
    <script src="../Scripts/Vue/axios_v0.18.0/axios.min.js"></script>
     <script src="../Scripts/Vue/vue-numeric_v2.3.0/vue-numeric.min.js"></script> 
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Panel runat="server" ID="PanelScript">

</asp:Panel>