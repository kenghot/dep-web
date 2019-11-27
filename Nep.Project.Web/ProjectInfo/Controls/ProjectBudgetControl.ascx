<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectBudgetControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.ProjectBudgetControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>
<%@ Register Src="~/ProjectInfo/Controls/BudgetGridControl.ascx" TagPrefix="uc1" TagName="BudgetGridControl" %>


<style type="text/css">
    .div-budget-status{
    padding: 3px;
    font-size: 12pt;
    border-style: solid;
    border-width: 1px;
    height: 25px;
    position: fixed;
    bottom: 0;
    background-color: bisque;
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

        
        <div class="panel panel-default"><!--รายละเอียดงบประมาณ-->
                            <asp:UpdatePanel ID="UpdatePanelBudgetActivity" 
                                             runat="server">
                                <ContentTemplate> 
<%--                                    <asp:GridView runat="server" ID="GridViewActivity" AutoGenerateColumns="False"
                                        ItemType="Nep.Project.DBModels.Model.PROJECTBUDGETACTIVITY"
                                        CssClass="asp-grid project-approval-grid" DataKeyNames="ACTIVITYID" 
                                        OnRowEditing="GridViewActivity_RowEditing"
                                        OnRowCancelingEdit="GridViewActivity_RowCancelingEdit"
                                        OnRowCommand="GridViewActivity_RowCommand"                                       
                                        OnRowDataBound="GridViewActivity_RowDataBound"
                                        ShowFooter="True" Width="100%" 
                                        >    

                                        <Columns>
                                            <asp:BoundField DataField="RUNNO" HeaderText="กิจกรรมที่">
                                            <HeaderStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ACTIVITYNAME" HeaderText="ชื่อกิจกรรม" />
                                            <asp:BoundField DataField="ACTIVITYDESC" HeaderText="รายละเอียดกิจกรรม" />
                                            <asp:BoundField DataField="TOTALAMOUNT"  HeaderText="งบประมาณที่ขอ" />
                                            <asp:ButtonField ButtonType="Button" CommandName="EditBudgetButton" Text="รายละเอียดงบประมาณ" />
                                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ShowInsertButton="True" />
                                        </Columns>

                                    </asp:GridView>--%>
            <div class="panel-heading">
                <h3 class="panel-title"><%= Model.BudgetDetail_Detail %></h3>
            </div>
               <div class="form-horizontal">
                    <div class="col-sm-12">                       

                        <div class="form-horizontal custom-padd">                            
                            <div class="form-group form-group-sm" id="CreateProjectGroupForm" runat="server">
                                <div class="col-sm-5">
                                   <label style="font-weight:normal; float:left;margin-top:4px;padding-left:6px; padding-right:10px;">ชื่อกิจกรรม</label>
                                        <%--<nep:TextBox ID="TextBoxProjectTarget" runat="server"  Width="100%" PlaceHolder="กลุ่มเป้าหมาย"  ClientIDMode="Static" AutoPostBack="false"/>--%> 
                                        <nep:TextBox runat="server" ID="TextBoxAddActivityName" CssClass="form-control" ClientIDMode="Static"
                                                            TextMode="MultiLine" MaxLength="1000" ></nep:TextBox>      
                                        <asp:RequiredFieldValidator ID="RequiredAddActivityName" runat="server" ErrorMessage="กรุณาระบุชื่อกิจกรรม"
                                             ControlToValidate="TextBoxAddActivityName" ValidationGroup="AddActivity" ForeColor="Red"></asp:RequiredFieldValidator>                
                                 <%--       <span class="project-target-group-validate error-text" id="ValidateAddActivityName" runat="server"
                                            data-val-validationgroup="AddActivit" data-val-controltovalidate="<%$ code:TextBoxAddActivityName.ClientID %>"
                                            style="display:none;">
                                            กรุณาระบุ
                                        </span>--%>
                                    
                                    <%--<div id="ProjectTargetEtcBlock" style="display:none; margin-top:7px;">
                                        <nep:TextBox ID="TextBoxProjectTargetEtc" MaxLength="1333" runat="server" PlaceHolder="ชื่อกลุ่มเป้าหมาย" CssClass="form-control" Width="360px"
                                                />
                                        <span class="project-target-group-validate error-text"  id="ValidateProjectTargetGroupEtc" runat="server"
                                            data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxProjectTargetEtc.ClientID %>"
                                            style="display:none;">
                                            <%: String.Format(Nep.Project.Resources.Error.RequiredField,  "ชื่อกลุ่มเป้าหมายอื่นๆ") %>
                                        </span>
                                    </div>--%>
                                <%--    <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroupDupCreate" 
                                            data-val-validationgroup="SaveProjectTargetGroup" 
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectTarget_TargetName) %>
                                    </span>--%>
                                </div>
                                
                                <div class="col-sm-5">
                                    <label style="font-weight:normal; float:left;margin-top:4px;padding-left:6px; padding-right:10px;">รายละเอียด</label>
                                       <nep:TextBox runat="server" ID="TextBoxAddActivityDESC" CssClass="form-control" ClientIDMode="Static"
                                                            TextMode="MultiLine" MaxLength="1000" ></nep:TextBox>      
                                        <asp:RequiredFieldValidator ID="RequiredAddActivityDESC" runat="server" ErrorMessage="กรุณาระบุรายละเอียดกิจกรรม" 
                                            ControlToValidate="TextBoxAddActivityDESC" ValidationGroup="AddActivity" ForeColor="Red"></asp:RequiredFieldValidator>                
<%--                                    <nep:TextBox ID="TextBoxProjectTargetAmount" runat="server" Width="100px" PlaceHolder="รวม" TextMode="Number" 
                                        NumberFormat="N0"  Min="1" Max="99999999" CssClass="form-control" />
                                    <span class="project-target-group-validate error-text" id="ValidateProjectTargetGroupAmount" runat="server"
                                        data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="<%$ code:TextBoxProjectTargetAmount.ClientID %>"
                                        style="display:none;">
                                        <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_Amount) %>
                                    </span>--%>
                                </div>
                                <div class="col-sm-2" style="padding-left:15px;">
                                    <%--<asp:ImageButton ID="ImageButtonCancelProductTargetGroupTemp" runat="server" OnClientClick="return false;" CssClass="btn-hide" />--%>
                    
                                    <asp:ImageButton ID="ImageButtonSaveProductTargetGroup" runat="server" ToolTip="เพิ่ม" 
                                        ImageUrl="~/Images/icon/round_plus_icon_16.png" BorderStyle="None" CssClass="button-add-targetgroup"
                                         ValidationGroup="AddActivity" OnClick="ImageButtonSaveProductTargetGroup_Click"   />
                                    <asp:ImageButton ID="ImageButtonRefreshActivity" runat="server" ToolTip="รีเฟรช" 
                                        ImageUrl="~/Images/icon/reload_icon_16.png" BorderStyle="None" CssClass="button-add-targetgroup"
                                           OnClick="ImageButtonRefreshActivity_Click"     />
                                    <asp:Image ID="ImageHelp2" runat="server" ToolTip="เมื่อกรอกชื่อกิจกรรมและรายละเอียดกิจกรรมแล้วให้กดเครื่องหมาย + เพื่อเพิ่มข้อมูลเข้าสู่ระบบ" ImageUrl="~/Images/icon/about.png" BorderStyle="None" /> 
                                        <%--OnClientClick="return c2xProjectInfo.createRowProjectTargetGroup(event)"/>--%>
                                  <%--  <asp:ImageButton ID="ImageButtonCancelProductTargetGroup" runat="server" ToolTip="ล้างข้อมูล" 
                                        ImageUrl="~/Images/icon/brush_icon_16.png" BorderStyle="None" CssClass="button-clear-targetgroup"
                                        OnClientClick="return c2xProjectInfo.cancelCreateRowProjectTargetGroup();"/>
                    --%>
                                </div>  
                            </div>
                       </div>
                      </div>
                   <div style="text-align:center;padding-bottom:5px;font-size:14px">
                       <asp:Button runat="server" ID="btnMangaeExpense" OnClick="btnMangaeExpense_Click" Text="กดเพื่อเพิ่ม ค่าบริหารจัดการโครงการ *โดยสามารถเพิ่มได้เพียง 1 ครั้งต่อ 1 โครงการเท่านั้น" /><br />
                   </div>
                   
                   </div>

                                    <asp:GridView runat="server" ID="GridViewActivity" AutoGenerateColumns="false" AllowPaging="false"
                                        ItemType="Nep.Project.ServiceModels.ProjectInfo.BudgetActivity"
                                        CssClass="asp-grid project-approval-grid" DataKeyNames="ActivityID" 
                                        OnRowEditing="GridViewActivity_RowEditing"
                                        OnRowCancelingEdit="GridViewActivity_RowCancelingEdit"
                                        OnRowCommand="GridViewActivity_RowCommand"                                       
                                        OnRowDataBound="GridViewActivity_RowDataBound"
                                        ShowFooter="false" OnRowDeleting="GridViewActivity_RowDeleting" 
                                        >                                                        
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate> 
                                                    <th style="width:30px"> 
                                                        กิจกรรมที่
                                                                                                                                
                                                    </th>
                                                    <th  style="width:200px"> 
                                                       ชื่อกิจกรรม                                                                             
                                                    </th>
                                                    <th> 
                                                       รายละเอียดกิจกรรม                                                                              
                                                    </th>
                                                    <th  style="width:120px">
                                                        งบประมาณที่ขอ
                                                    </th>                                                                                                      
                                                    <th style="width:60px"></th>
                                                    <th style="width:60px"></th>
                                                </HeaderTemplate>

                                                <ItemTemplate>                                                  
                                                     
                                                    <td>
                                                         <%# Eval("RUNNO") %> 
                                                    </td>
                                                    <td colspan="2">
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ACTIVITYNAME"), null, "") %> : 
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("ACTIVITYDESC"), null,"") %>
                                                        <uc1:BudgetGridControl runat="server" id="BudgetGridControl" />
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("TOTALAMOUNT"), "N2", "0.00")%>
                                                    </td>

                                                    <td>  
                                                        <asp:HiddenField runat="server" ID="HiddenActivityID" Value='<%# Eval("ACTIVITYID")%>' />
                                                        <asp:Button runat="server" BackColor="Yellow" ID="ButtonPleaseSave" Text="กรุณากดบันทึกด้านล่างเพื่อทำงานต่อ" Enabled="false"></asp:Button>
                                                       <asp:Button runat="server" ID="BudgetButtonEdit" Text="กรอกรายละเอียดงบประมาณ" CommandName="budget" 
                                                            OnClientClick = '<%# PopupActivityScreen(Eval("ACTIVITYID")) %>'
                                                           CommandArgument='<%# Eval("ACTIVITYID")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ActivityButtonEdit" runat="server" ImageUrl="~/Images/icon/doc_edit_icon_16.png" 
                                                        CommandName="edit" CommandArgument='<%# Eval("Runno")%>' CausesValidation="false"
                                                        Visible="<%# GridViewActivity.EditIndex < 0 %>"/>    
                                                        <asp:ImageButton ID="ActivityButtonDelete" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                                        CommandName="delete" CommandArgument='<%# Eval("Runno")%>' CausesValidation="false"
                                                        Visible="<%# GridViewActivity.EditIndex < 0 %>" OnClientClick="return confirm('การลบกิจกรรมจะเป็นการลบข้อมูลของการของบประมาณทั้งหมดที่ได้กรอกไว้ในกิจกรรมนี้แล้วด้วย ท่านยืนยันที่จะต้องการลบกิจกรรมนี้หรือไม่')" />
                                                    </td>
                                       
                                                </ItemTemplate>
                                           
                                                <EditItemTemplate>                                                    
                                                   
                                                    <td>
                                                       <asp:Label runat="server" ID="LabelRunno" Text='<%# Eval("RUNNO") %>' ></asp:Label>
                                                    </td>

                                                    <td>
                                                        <nep:TextBox runat="server" ID="TextBoxActivityName" CssClass="form-control" ClientIDMode="Static"
                                                            TextMode="MultiLine" MaxLength="1000" Text='<%# Eval("ACTIVITYNAME") %>'></nep:TextBox>
                                                    </td>
                                                    <td>
                                                        <nep:TextBox runat="server" ID="TextBoxActivityDesc" CssClass="form-control" ClientIDMode="Static"
                                                            TextMode="MultiLine" MaxLength="1000" Text='<%# Eval("ACTIVITYDESC") %>'></nep:TextBox>
                                                    </td>
                                                    <td>
                                                        <%# Nep.Project.Common.Web.WebUtility.DisplayInHtml( Eval("TOTALAMOUNT"), "N2", "0.00")%>
                                                    </td>
                                                    <td></td>
                                              <%--       <%
                                                        if (true)
                                                        {%>
                                                            <td>
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetDiscriminationTeamAmount" CssClass="form-control" ClientIDMode="Static"
                                                                   TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise1Amount") %>'></nep:TextBox>  
                                                                <asp:CustomValidator ID="CustomValidatorRevise1Amount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorRevise1Amount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" /> 
                                                            </td>
                                                            <td>
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetSubcommitteeAmount" CssClass="form-control" ClientIDMode="Static"
                                                                   TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise2Amount") %>'></nep:TextBox> 
                                                                <asp:CustomValidator ID="CustomValidatorRevise2Amount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorRevise2Amount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false"/>   
                                                            </td>
                                                        <%}else{%>
                                                            <td>                                                                
                                                                <nep:TextBox runat="server" ID="TextBoxBudgetProvinceCommitteeAmount" CssClass="form-control" ClientIDMode="Static"
                                                                    TextMode="Number" Min="0" Max="999999999.99" Text='<%# Eval("Revise1Amount") %>'></nep:TextBox>  
                                                                
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Model.BudgetDetail_Amount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" />
                                                                
                                                                <asp:CustomValidator ID="CustomValidatorProvinceReviseAmount" ControlToValidate="TextBoxBudgetProvinceCommitteeAmount" runat="server" CssClass="error-text"
                                                                    OnServerValidate="CustomValidatorProvinceReviseAmount_ServerValidate" ValidateEmptyText="true"
                                                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailRequestAmount) %>" 
                                                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.LessThanOREqual, Nep.Project.Resources.UI.LabelBudgetDetailApprovedAmount,  Nep.Project.Resources.UI.LabelBudgetDetailReviseAmount) %>"
                                                                    ValidationGroup="SaveBudgetApprovalDetail" Enabled="false" /> 
                                                            </td>
                                                        <%}
                                                     %>                                                    
                                                    --%>

                                                    <td>
                                                        <asp:ImageButton ID="ActivityDetailButtonSave" runat="server" ImageUrl="~/Images/icon/save_icon_16.png" 
                                                            ValidationGroup="SaveActivityDetail" CommandName="save" CommandArgument='<%# Eval("Runno") %>'/>
                                                        <asp:ImageButton ID="ActivitytDetailButtonCancel" runat="server" ImageUrl="~/Images/icon/cancel_icon_16.png"
                                                            CommandName="cancel" CausesValidation="false"/>
                                                    </td>
                                                </EditItemTemplate>
<%--                                                <FooterTemplate>
                                                    
                                                    <td colspan="3">
                                                        ยอดรวม
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelRequestAmount" runat="server"/>
                                                    </td>
  
                                                 
                                                    
                                                    <td></td>
                                                    <td></td>
                                                </FooterTemplate>--%>
                                                
                                             </asp:TemplateField>                                                            
                                        </Columns> 
                                    </asp:GridView>

 <%--                                   <asp:CustomValidator ID="CustomValidatorApprovalBudgetDetail" runat="server" CssClass="error-text"
                                        OnServerValidate="CustomValidatorApprovalBudgetDetail_ServerValidate"  ValidationGroup="SaveApproval"/>--%>
                            
                                </ContentTemplate>
                            </asp:UpdatePanel>
<!--UpdatePanelApprovalBudgetDetail-->    

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
                                        OnRowDataBound="GridViewBudgetDetail_RowDataBound" Visible="false"
                                        ShowFooter="true">                                                        
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
                           
                          <%--  <asp:CustomValidator ID="CustomValidatorBudgetDetail" runat="server" CssClass="error-text"
                                OnServerValidate="ProjectBudgetDetailValidate" ClientValidationFunction="validateProjectBudgetDetail"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.BudgetDetail_Detail) %>"
                                ValidationGroup="SaveProjectBudget"/>

                            <asp:CustomValidator ID="CustomValidatorMaxAmount" runat="server" CssClass="error-text"
                                OnServerValidate="CustomValidatorMaxAmount_ServerValidate" 
                                Text="" 
                                ErrorMessage=""
                                ValidationGroup="SaveProjectBudget"/>
                         --%>
                                                                
                        </div>
                    </div>
                </div>
            <div class="panel-body">
              
        
                
            </div>
        </div><!--รายละเอียดงบประมาณ-->
        <div class="panel panel-default">
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
                                        <nep:TextBox runat="server" ID="TextBoxTotalAmount" TextMode="Number" CssClass="form-control" Min="1.00" Max="9999999.99" Enabled="false"  Text="0.00"/>
                                        <span class="form-control-desc" style="right:-22px;"><%:UI.LabelBath %></span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTotalAmount" ControlToValidate="TextBoxTotalAmount" runat="server" CssClass="error-text"
                                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>" ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudget_TotalAmount) %>"
                                            ValidationGroup="SaveProjectBudget" />--%>
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
            </div><!--panel-body-->
        </div><!--panel-->        
        <div class="panel panel-default"><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->
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
        </div><!--ได้เสนอโครงการเดียวกันนี้เพื่อรับการสนับสนุนจากแหล่งทุนอื่นหรือไม่-->

        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server"   ID="ButtonSave" CssClass="btn btn-primary btn-sm" Visible="false" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>"  
                  
                        OnClick="ButtonSave_Click" ValidationGroup="SaveProjectBudget" />

                    <asp:Button runat="server"  ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />

                    <asp:Button runat="server"  ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm" Visible="false"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click"/>

                    <asp:HyperLink ID="HyperLinkPrint" runat="server" CssClass="btn btn-default btn-sm"  Visible="false"                      
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>" Target="_blank"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportProjectRequest?projectID={0}", ProjectID ) %>'></asp:HyperLink>

                    
            
                    <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-red btn-sm" Text="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>"
                        OnClientClick="return ConfirmToDeleteProject()" OnClick="ButtonDelete_Click" Visible="false"></asp:Button>

                    
                   
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>
                </div>
                
            </div>
            
        </div>
 
    
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Panel runat="server" ID="PanelScript">

</asp:Panel>




