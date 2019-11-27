<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommitteeControl.ascx.cs" 
    Inherits="Nep.Project.Web.ProjectInfo.Controls.CommitteeControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelCommittee" ClientIDMode="AutoID" UpdateMode="Always" RenderMode="Block" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <style type="text/css">
            .btn-hide {
               display:none;
            }

            .button-add-committee, .button-clear-committee {
                 opacity:.6;
            }

            .button-add-committee[disabled="disabled"]:hover, .button-clear-committee[disabled="disabled"]:hover {
                opacity:.6;
            }
            
            .button-add-committee:hover, .button-clear-committee:hover {
                opacity:1;
            }
        </style>

        <asp:HiddenField ID="HiddenFieldCommitteeData" runat="server" />
        <asp:HiddenField ID="HiddenFieldOfficerData" runat="server" />
        <asp:HiddenField ID="HiddenFieldOrganizationID" runat="server" />
              

         <div class="form-horizontal">               
              <div class="form-group form-group-sm" >
                  <%--<div class="col-sm-2 control-label">ประธาน/นายก<span class="required" ></span></div>--%>
                  <div class="col-sm-3">
                      <nep:TextBox ID="TextBoxHeadCommitteeFirstname" CssClass="form-control" runat="server" Visible="false" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Firstname %>' ></nep:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorHeadCommitteeFirstname" ControlToValidate="TextBoxHeadCommitteeFirstname" 
                            client-id="ValidateHeadCommitteeFirstname"
                            runat="server" CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Firstname) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Firstname) %>"
                            ValidationGroup="NotCheckValid" />
                            <%--ValidationGroup="<%$ code:ValidateGroupName %>" />--%>
                      <asp:CustomValidator ID="CheckDupCommitteeFirstname" runat="server" CssClass="error-text"
                          client-id="CheckDupCommitteeFirstname" ValidateEmptyText="true" ControlToValidate="TextBoxHeadCommitteeFirstname"
                          Text="<%$ code: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Committee_Firstname) %>" 
                          ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Committee_Firstname) %>"
                          ClientValidationFunction="validateHeadCommitteeName"  
                          ValidationGroup="NotCheckValid"  />
                          <%--ValidationGroup="<%$ code:ValidateGroupName %>"  />--%>
                  </div>
                  <div class="col-sm-3">
                      <nep:TextBox ID="TextBoxHeadCommitteeSurname" CssClass="form-control" runat="server" Visible="false" Text=''  
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Surname %>'></nep:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxHeadCommitteeSurname" 
                            client-id="ValidateHeadCommitteeLastname"
                            runat="server" CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Surname) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Surname) %>"
                            ValidationGroup="NotCheckValid" />
                            <%--ValidationGroup="<%$ code:ValidateGroupName %>" />--%>
                      <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="error-text"
                          ValidateEmptyText ="true" ControlToValidate="TextBoxHeadCommitteeSurname"
                          Text=" " 
                          ErrorMessage=""
                          ClientValidationFunction="validateHeadCommitteeName"  
                          ValidationGroup="NotCheckValid" />
                          <%--ValidationGroup="<%$ code:ValidateGroupName %>"  />--%>
                  </div>
                  <div class="col-sm-3">
                      <nep:TextBox ID="TextBoxHeadCommitteePosition" CssClass="form-control" Visible="false" runat="server" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Position %>'></nep:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxHeadCommitteePosition" 
                            client-id="ValidateHeadCommitteePosition"
                            runat="server" CssClass="error-text" SetFocusOnError="true"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Position) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Position) %>"
                            ValidationGroup="NotCheckValid" />
                            <%--ValidationGroup="<%$ code:ValidateGroupName %>" />--%>
                  </div>
              </div>
       <%--       <div class="form-group form-group-sm">
                  <div class="col-sm-12"><hr style="margin:10px 20px 5px 20px" /></div>
              </div>--%>
              <div class="form-group form-group-sm" id="CommitteeForm" runat="server">
                  <div class="col-sm-2 control-label">คณะกรรมการ<span class="required" ></span></div>
                  <div class="col-sm-2">
                      <nep:TextBox ID="TextBoxCommitteeFirstname" CssClass="form-control" runat="server" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Firstname %>' ></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateCommitteeFirstname" runat="server"
                            client-id="ValidateRequiredCommitteeFirstname"
                            data-val-validationgroup="SaveCommittee" data-val-controltovalidate="<%$ code:TextBoxCommitteeFirstname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Firstname) %>
                       </span>
                      <span class="committee-validate error-text" id="ValidateCommitteeFirstNameDup" runat="server" 
                            client-id="ValidateDupCommitteeFirstname"
                            data-val-validationgroup="SaveCommittee" data-val-controltovalidate="<%$ code:TextBoxCommitteeFirstname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Committee_Firstname) %>
                       </span>                     
                  </div>
                  <div class="col-sm-2">
                      <nep:TextBox ID="TextBoxCommitteeLastname" CssClass="form-control" runat="server" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Surname %>' ></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateCommitteeSurname" runat="server"
                            client-id="ValidateRequiredCommitteeLastname"
                            data-val-validationgroup="SaveCommittee" data-val-controltovalidate="<%$ code:TextBoxCommitteeLastname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Surname) %>
                       </span>
                     
                  </div>
                  <div class="col-sm-2">
                         <asp:DropDownList ID="ComboBoxPosition" runat="server" DataTextField="Text" DataValueField="Value" Width="100%" Height="30px">
                         </asp:DropDownList>
                         <span class="committee-validate error-text" id="ValidateCommitteePositionCode" runat="server"
                            client-id="ValidateRequiredCommitteePositionCode"
                            data-val-validationgroup="SaveCommittee" data-val-controltovalidate="<%$ code:ComboBoxPosition.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_PositionCode) %>
                       </span>
                    <%--     <ajaxToolkit:ComboBox runat="server" ID="ComboBoxPosition" 
                                DataMember="Nep.Project.ServiceModels.GenericDropDownListData"
                                DropDownStyle="DropDown"                                     
                                CaseSensitive="false"     
                                CssClass="form-control-combobox"  
                                SelectMethod="GetPosition"
                                AppendDataBoundItems="false"
                                DataTextField ="Text"
                                DataValueField="Value"                                   
                                ItemInsertLocation="OrdinalValue"
                                Enabled="false">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </ajaxToolkit:ComboBox>--%>
                  </div>
                  <div class="col-sm-2">
                      <nep:TextBox ID="TextBoxCommitteePosition" CssClass="form-control" runat="server" Text=''  
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Position %>'  ></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateCommitteePosition" runat="server"
                            client-id="ValidateRequiredCommitteePosition"
                            data-val-validationgroup="SaveCommittee" data-val-controltovalidate="<%$ code:TextBoxCommitteePosition.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Position ) %>
                       </span>
                  </div>
                 <div class="col-sm-1">
                    <asp:ImageButton ID="ImageButtonCancelCommitteeTemp" runat="server" OnClientClick="return false;" CssClass="btn-hide" />                    
                    <asp:ImageButton ID="ImageButtonSaveCommittee" runat="server" ToolTip="เพิ่ม" 
                        ImageUrl="~/Images/icon/round_plus_icon_16.png" BorderStyle="None" CssClass="button-add-committee"
                        OnClientClick="return c2xCommittee.createRowCommittee('CommitteeGrid')"/>
                    <asp:ImageButton ID="ImageButtonCancelCommittee" runat="server" ToolTip="ล้างข้อมูล" 
                        ImageUrl="~/Images/icon/brush_icon_16.png" BorderStyle="None" CssClass="button-clear-committee"
                        OnClientClick="return c2xCommittee.cancelCreateRowCommittee('SaveCommittee');"/>
                    <asp:Image ID="ImageHelpCommittee" runat="server" ToolTip="เมื่อกรอกชื่อ นามสกุล และเลือกตำแหน่งเรียบร้อยแล้วให้กดที่เครื่องหมาย + เพื่อเพิ่มข้อมูลเข้าสู่ระบบ" 
                        ImageUrl="~/Images/icon/about.png" BorderStyle="None" />
                </div>  
             </div><!-- สร้าง กรรมการ -->
             <div class="form-group form-group-sm"  id="CommitteeGridContainer" style="display:none">
                <div class="col-sm-2 control-label-left text-right"><div id="CommitteeLabelBlock" visible="false" runat="server">กรรมการ<span class="required" ></span> :</div></div>
                <div class="col-sm-10">
                    <div id="CommitteeGrid" ></div>
                </div>
            </div><!-- รายการ กรรมการ -->
            <div class="form-group form-group-sm">
                <div class="col-sm-12"><hr style="margin:10px 20px 5px 20px" /></div>
            </div>

             <div class="form-group form-group-sm" id="OfficerForm" runat="server">
                  <div class="col-sm-2 control-label">เจ้าหน้าที่<span class="required" ></span></div>
                  <div class="col-sm-4">
                      <nep:TextBox ID="TextBoxOfficerFirstname" CssClass="form-control" runat="server" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Firstname %>' ></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateRequiredOfficerFirstname" runat="server"
                            client-id="ValidateRequiredOfficerFirstname"
                            data-val-validationgroup="SaveOfficer" data-val-controltovalidate="<%$ code:TextBoxOfficerFirstname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Firstname) %>
                       </span>
                      <span class="committee-validate error-text" id="ValidateDupOfficerFirstname" runat="server" 
                            client-id="ValidateDupOfficerFirstname"
                            data-val-validationgroup="SaveOfficer" data-val-controltovalidate="<%$ code:TextBoxCommitteeFirstname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Committee_Firstname) %>
                       </span>                     
                  </div>
                 <div class="col-sm-4">
                      <nep:TextBox ID="TextBoxOfficerLastname" CssClass="form-control" runat="server" Text='' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Surname %>' ></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateRequiredOfficerLastname" runat="server"
                            client-id="ValidateRequiredOfficerLastname"
                            data-val-validationgroup="SaveOfficer" data-val-controltovalidate="<%$ code:TextBoxOfficerLastname.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Surname) %>
                       </span>
                     
                </div>
                   <%--<div class="col-sm-2">--%>
                      <nep:TextBox ID="TextBoxOfficerPosition" CssClass="form-control" runat="server" Visible="false"  Text='เจ้าหน้าที่' 
                          MaxLength="100"
                          PlaceHolder='<%$ code:Nep.Project.Resources.Model.Committee_Position %>'></nep:TextBox>
                      <span class="committee-validate error-text" id="ValidateRequiredOfficerPosition" runat="server"
                            client-id="ValidateRequiredOfficerPosition"
                            data-val-validationgroup="NotCheckValid" data-val-controltovalidate="<%$ code:TextBoxCommitteePosition.ClientID %>"
                            style="display:none;">
                            <%: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Position) %>
                       </span>
                  <%-- </div>--%>
                 <div class="col-sm-1">
                    <asp:ImageButton ID="ImageButtonSaveTemp" runat="server" OnClientClick="return false;" CssClass="btn-hide" />                    
                    <asp:ImageButton ID="ImageButtonSaveOfficer" runat="server" ToolTip="เพิ่ม" 
                        ImageUrl="~/Images/icon/round_plus_icon_16.png" BorderStyle="None" CssClass="button-add-committee"
                        OnClientClick="return c2xCommittee.createRowCommittee('OfficerGrid')"/>
                    <asp:ImageButton ID="ImageButtonCancelOfficer" runat="server" ToolTip="ล้างข้อมูล" 
                        ImageUrl="~/Images/icon/brush_icon_16.png" BorderStyle="None" CssClass="button-clear-committee"
                        OnClientClick="return c2xCommittee.cancelCreateRowCommittee('SaveOfficer');"/>
                     <asp:Image ID="ImageHelpOfficer" runat="server" ToolTip="เมื่อกรอกชื่อ นามสกุลเรียบร้อยแล้วให้กดที่เครื่องหมาย + เพื่อเพิ่มข้อมูลเข้าสู่ระบบ" 
                        ImageUrl="~/Images/icon/about.png" BorderStyle="None" />                   
                </div>  
             </div><!-- สร้าง เจ้าหน้าที่ -->
             <div class="form-group form-group-sm"  id="OfficerGridContainer" style="display:none">
                <div class="col-sm-2 control-label-left text-right"><div id="OfficerLabel" runat="server" visible="false"></div>เจ้าหน้าที่<span class="required" ></span> :</div>
                <div class="col-sm-10">
                    <div id="OfficerGrid" ></div>
                </div>
            </div><!-- รายการ เจ้าหน้าที่ -->
         </div>

         
    </ContentTemplate>
</asp:UpdatePanel>