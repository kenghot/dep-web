<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="TabContractControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabContractControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>
<!-- #include file="~/Html/Contract/Contract.html" -->
<asp:UpdatePanel ID="UpdatePanelContract"
    UpdateMode="Conditional"
    runat="server">
    <ContentTemplate>

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TabTitleContract %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                   <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label">เลขที่อ้างอิงสัญญา</label>
                        <div class="col-sm-4 control-value">
                            <nep:TextBox ID="TextBoxReferenceNo" runat="server" MaxLength="20" CssClass="form-control" />
<%--                            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="TextBoxContractNo"
                                runat="server" CssClass="error-text" SetFocusOnError="true" ValidateEmptyText="true"
                                ClientValidationFunction="validateContractNo"
                                OnServerValidate="CustomValidatorContractNo_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractNo) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractNo) %>"
                                ValidationGroup="SaveContract" />--%>
                        </div>
 
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_ContractNo %></label>
                        <div class="col-sm-4 control-value">
                            <nep:TextBox ID="TextBoxContractNo" runat="server" MaxLength="20" CssClass="form-control" />
                            <asp:CustomValidator ID="CustomValidatorContractNo" ControlToValidate="TextBoxContractNo"
                                runat="server" CssClass="error-text" SetFocusOnError="true" ValidateEmptyText="true"
                                ClientValidationFunction="validateContractNo"
                                OnServerValidate="CustomValidatorContractNo_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractNo) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractNo) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <label class="col-sm-2 control-label"><%= Model.Contract_ContractDate %><span class="required"></span></label>
                        <div class="col-sm-4 control-value">
                            <nep:DatePicker runat="server" ID="DatePickerContractDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:RequiredFieldValidator ID="RequiredFieldDatePickerContractDate" ControlToValidate="DatePickerContractDate"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_StartDate %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <nep:DatePicker runat="server" ID="DatePickerContractStartDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:RequiredFieldValidator ID="RequiredFieldDatePickerStartDate" ControlToValidate="DatePickerContractStartDate"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_StartDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_StartDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <label class="col-sm-2 control-label"><%= Model.Contract_EndDate %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <nep:DatePicker runat="server" ID="DatePickerContractEndDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:RequiredFieldValidator ID="RequiredFieldDatePickerEndDate" ControlToValidate="DatePickerContractEndDate"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_EndDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_EndDate) %>"
                                ValidationGroup="SaveContract" />

                            <asp:CustomValidator ID="CustomValidatorContractDate" CssClass="error-text" runat="server"
                                OnServerValidate="CustomValidatorContractDate_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.Contract_EndDate, Model.Contract_StartDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.Contract_EndDate, Model.Contract_StartDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                     <%--ประวัติการแก้ไข Contract_StartDate Contract_EndDate --%> 
                     <div class="form-group form-group-sm" id="divHistoryEditStartEndDate" style="color:gray;" runat="server" visible="false" >
                        <label class="col-sm-2 control-label" style="color:gray;">ระยะเวลาเดิม</label>
                        <div class="col-sm-10">
                            <asp:Label ID="LabelHistoryEditStartEndDate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= UI.LabelBudgetDetailRequestAmount %></label>
                        <div class="col-sm-4 control-value">
                            <asp:Label runat="server" ID="LabelBudgetRequestAmount" />&nbsp;&nbsp;<%=UI.LabelBath %></div>
                        <label class="col-sm-2 control-label"><%= UI.LabelBudgetDetailApprovedAmount %></label>
                        <div class="col-sm-4 control-value">
                            <asp:Label runat="server" ID="LabelApprovedAmount" />&nbsp;&nbsp;<%=UI.LabelBath %></div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_Location %><span class="required"></span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBoxContractLocation" runat="server" MaxLength="70" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldTextBoxContractLocation" ControlToValidate="TextBoxContractLocation"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                    <!-- ที่อยู่ -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_AddressNo %><span class="required"></span></label>
                        <div class="col-sm-1">
                            <asp:TextBox ID="TextBoxAddressNo" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddressNo" ControlToValidate="TextBoxAddressNo" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_AddressNo) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_AddressNo) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Moo %></label>
                        <div class="col-sm-1">
                            <asp:TextBox ID="TextBoxMoo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>

                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxBuilding" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxSoi" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>

                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Street %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxStreet" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <input id="DdlProvince" runat="server" style="width: 100%;" />
                            <asp:CustomValidator ID="CustomValidatorProvince" runat="server" ControlToValidate="DdlProvince"
                                OnServerValidate="CustomValidatorProvince_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                CssClass="error-text" ValidationGroup="SaveContract" ValidateEmptyText="true" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>" />


                        </div>


                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <input id="DdlDistrict" runat="server" style="width: 100%;" />
                            <asp:CustomValidator ID="CustomValidatorDistrict" runat="server" ControlToValidate="DdlDistrict"
                                OnServerValidate="CustomValidatorDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                CssClass="error-text" ValidationGroup="SaveContract" ValidateEmptyText="true" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>" />

                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <input id="DdlSubDistrict" runat="server" style="width: 100%;" />
                            <asp:CustomValidator ID="CustomValidatorSubDistrict" runat="server" ControlToValidate="DdlSubDistrict"
                                OnServerValidate="CustomValidatorSubDistrict_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                CssClass="error-text" ValidationGroup="SaveContract" ValidateEmptyText="true" SetFocusOnError="true"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>" />
                        </div>

                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <nep:TextBox ID="TextBoxPostCode" runat="server" MaxLength="5" CssClass="form-control" TextMode="Number" NumberFormat="####"></nep:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode" ControlToValidate="TextBoxPostCode" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ValidationGroup="SaveContract" />
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode" runat="server" ID="MaskedEditExtenderPostCode"
                                Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostCode" ControlToValidate="TextBoxPostCode" runat="server"
                                CssClass="error-text" ValidationExpression="\d{5}"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>

                    <!-- ที่อยู่ -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-offset-6 col-sm-2 control-label"><%= Model.Contract_ViewerName %></label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxWitnessFirstName1" runat="server" MaxLength="50" CssClass="form-control" />
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxWitnessLastname1" runat="server" MaxLength="50" CssClass="form-control" />
                        </div>

                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-offset-6 col-sm-2 control-label"><%= Model.Contract_ViewerName %></label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxWitnessFirstName2" runat="server" MaxLength="50" CssClass="form-control" />
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxWitnessLastName2" runat="server" MaxLength="50" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleSupportGiven %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= UI.LabelDepartmentName %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-8 control-label">
                            <asp:Label ID="LabelContractRefName" runat="server" /><span class="required"></span></label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRefFirstName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldTextBoxRefFirstName" ControlToValidate="TextBoxRefFirstName"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="TextBoxRefLastName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldTextBoxRefLastName" ControlToValidate="TextBoxRefLastName"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefProsition %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxRefPosition" runat="server" MaxLength="40" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRefPosition" ControlToValidate="TextBoxRefPosition"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ValidationGroup="SaveContract" />
                        </div>

                    </div
                       <div class="form-group form-group-sm">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefPositionLine2 %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxRefPositionLine2" runat="server" MaxLength="40" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxRefPosition"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ValidationGroup="SaveContract" />--%>
                        </div>

                    </div>
                    <br />
                       <div class="form-group form-group-sm">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefPositionLine3 %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxRefPositionLine3" runat="server" MaxLength="40" CssClass="form-control"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxRefPosition"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefProsition) %>"
                                ValidationGroup="SaveContract" />--%>
                        </div>

                    </div>
                    <div class="form-group form-group-sm" id="ContractRefNoBlock" runat="server">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefNo %></label>
                        <div class="col-sm-2">
                            <nep:TextBox ID="TextBoxRefNo1" runat="server" MaxLength="20" CssClass="form-control"></nep:TextBox>
                            <asp:CustomValidator ID="CustomValidatorRefNo1" ControlToValidate="TextBoxRefNo1" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateRefData" OnServerValidate="CustomValidatorRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.LabelDirective) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.LabelDirective) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <div class="col-sm-2" style="position: relative">
                            <span style="position: absolute; top: 7px; left: -3px">/ </span>
                            <nep:DatePicker ID="DatePickerRefNo2" runat="server" Format="yyyy" EnabledTextBox="true" />
                            <asp:CustomValidator ID="CustomValidatorRefNo2" ControlToValidate="DatePickerRefNo2" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateRefData" OnServerValidate="CustomValidatorRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.LabelDirective) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.LabelDirective) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm" id="ContractRefDateBlock" runat="server">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefDate %></label>
                        <div class="col-sm-4">
                            <nep:DatePicker runat="server" ID="DatePickerRefDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:CustomValidator ID="CustomValidatorRefDate" ControlToValidate="DatePickerRefDate" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateRefData" OnServerValidate="CustomValidatorRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm" id="ContractProvinceNoBlock" runat="server">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ProvinceNo %></label>
                        <div class="col-sm-2">
                            <nep:TextBox ID="TextBoxProvinceNo1" runat="server" MaxLength="20" CssClass="form-control"></nep:TextBox>
                            <asp:CustomValidator ID="CustomValidatorProvinceNo1" ControlToValidate="TextBoxProvinceNo1" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateProvinceRefData" OnServerValidate="CustomValidatorProvinceRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ProvinceNo) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ProvinceNo) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                        <div class="col-sm-2" style="position: relative">
                            <span style="position: absolute; top: 7px; left: -3px">/ </span>
                            <nep:DatePicker ID="DatePickerProvinceNo2" runat="server" Format="yyyy" EnabledTextBox="true" />
                            <asp:CustomValidator ID="CustomValidatorProvinceNo2" ControlToValidate="DatePickerProvinceNo2" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateProvinceRefData" OnServerValidate="CustomValidatorProvinceRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ProvinceNo) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ProvinceNo) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>

                    <div class="form-group form-group-sm" id="ContractProvinceRefDateBlock" runat="server">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractRefDate %></label>
                        <div class="col-sm-4">
                            <nep:DatePicker runat="server" ID="DatePickerProvinceDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:CustomValidator ID="CustomValidatorProvinceDate" ControlToValidate="DatePickerProvinceDate" ValidateEmptyText="true"
                                runat="server" CssClass="error-text" ClientValidationFunction="validateProvinceRefData" OnServerValidate="CustomValidatorProvinceRefData_ServerValidate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractRefDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleSupportReciever %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <!-- มอบอำนาจหรือไม่ -->
                        <label class="col-sm-8 control-label"><%= Model.Contract_AuthorizeFlag %></label>
                        <div class="col-sm-4 control-value">
                            <asp:CheckBox ID="CheckBoxAuthorizeFlag" runat="server" />
                        </div>
                    </div>

                    <div id="divAuthorize">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_NameSurnameAuthorize %><span class="required"></span></label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxReceiverName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomRequiredReceiverName" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredAuthorize_ServerValidate" ControlToValidate="TextBoxReceiverName"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverName) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverName) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxReceiverSurname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomRequiredReceiverSurname" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredAuthorize_ServerValidate" ControlToValidate="TextBoxReceiverSurname"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverSurname) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverSurname) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_ReceiverPosition %></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxReceiverPosition" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_AuthorizeDocID %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:C2XFileUpload runat="server" ID="C2XFileUploadAuthorizeDoc" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" visible="false" />
                               <%-- <asp:CustomValidator ID="CustomRequiredAuthorizeDoc" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredAuthorize_ServerValidate" ControlToValidate="C2XFileUploadAuthorizeDoc"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDocID) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDocID) %>"
                                    ValidationGroup="SaveContract" />--%>
                                  <nep:C2XFileUpload runat="server" ID="C2XFileUploadAuthorizeDocMulti" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />
                                 <asp:CustomValidator ID="CustomRequiredAuthorizeDoc" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredAuthorize_ServerValidate" ControlToValidate="C2XFileUploadAuthorizeDocMulti"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDocID) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDocID) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_AuthorizeDate %><span class="required"></span></label>
                            <div class="col-sm-4">
                                <nep:DatePicker runat="server" ID="DatePickerAuthorizeDate" ClearTime="true" EnabledTextBox="true"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                                <asp:CustomValidator ID="CustomRequiredAuthorizeDate" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredAuthorize_ServerValidate" ControlToValidate="DatePickerAuthorizeDate"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDate) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_AuthorizeDate) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                        </div>
                    </div>

                    <div id="divSupportReceiver">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_ContractReceiveName %><span class="required"></span></label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxContractReceiveName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomContractReceiveName" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredSupportReceive_ServerValidate" ControlToValidate="TextBoxContractReceiveName"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverName) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverName) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBoxContractReceiveSurname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomContractReceiveSurname" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredSupportReceive_ServerValidate" ControlToValidate="TextBoxContractReceiveSurname"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverSurname) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ReceiverSurname) %>"
                                    ValidationGroup="SaveContract" />
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-8 control-label"><%= Model.Contract_ReceiverPosition %></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxContractReceivePosition" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-8 control-label"><%= Model.Contract_ContractReceiveDate %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <nep:DatePicker runat="server" ID="DatePickerReceiveDate" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceiveDate" ControlToValidate="DatePickerReceiveDate"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractReceiveDate) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_ContractReceiveDate) %>"
                                ValidationGroup="SaveContract" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_OrgName %></label>
                        <div class="col-sm-10 control-value">
                            <asp:Label ID="LabelContractOrgName" runat="server" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_OrgAddress%></label>
                        <div class="col-sm-10 control-value">
                            <asp:Label ID="LabelContractOrgAddress" runat="server" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_OrgTelephone%></label>
                        <div class="col-sm-4 control-value">
                            <asp:Label ID="LabelContractOrgTelephone" runat="server" />
                        </div>
                        <label class="col-sm-2 control-label"><%= Model.Contract_OrgFax%></label>
                        <div class="col-sm-4 control-value">
                            <asp:Label ID="LabelContractOrgFax" runat="server" />
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Contract_OrgEmail%></label>
                        <div class="col-sm-4 control-value">
                            <asp:Label ID="LabelContractOrgEmail" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading">
                <h3 class="panel-title">ข้อมูลการจ่ายเงินสนับสนุน</h3>
            </div>
            <span class="form-control-desc"><b>เอกสารแนบท้ายสัญญาต่อไปนี้ ให้ถือว่าเป็นส่วนหนึ่งของสัญญานี้</b></span>

            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-9 control-label control-label-left without-delimit">
                            ผนวก 1 ระเบียบคณะกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการแห่งชาติ ว่าด้วยการพิจารณาอนุมัติ การจ่ายเงินเพื่อการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการและการจัดทำรายงานสถานะการเงินและการบริหารกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ พ.ศ. ๒๕๕๒ และที่แก้ไขเพิ่มเติม จำนวน 
                        </label>
                        <div class="col-sm-2" style="position: relative">
                            <nep:TextBox ID="TextBoxAttachPage1" TextMode="Number" NumberFormat="N0" runat="server"
                                Min="1" CssClass="form-control"></nep:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TextBoxAttachPage1"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                            <span class="form-control-desc">หน้า</span>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-9 control-label control-label-left without-delimit">
                            ผนวก 2 ประกาศคณะอนุกรรมการบริหารกองทุนส่งเสริมและพัฒนาคุณภาพ ชีวิตคนพิการ ว่าด้วยเรื่องกำหนดอัตราวงเงินและรายการค่าใช้จ่ายที่กองทุนให้การสนับสนุนแผนงาน หรือการเกี่ยวกับการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ พ.ศ. ๒๕๖๐ จำนวน
                        </label>
                        <div class="col-sm-2" style="position: relative">
                            <nep:TextBox ID="TextBoxAttachPage2" TextMode="Number" NumberFormat="N0" runat="server"
                                Min="1" CssClass="form-control"></nep:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="TextBoxAttachPage2"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                            <span class="form-control-desc">หน้า</span>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <asp:Label ID="LabelMeetingText" runat="server" class="col-sm-9 control-label control-label-left without-delimit">
                                    ผนวก 3 แจ้งผลการพิจารณาโครงการของคณะอนุกรรมการบริหารกองทุนส่งเสริม   และพัฒนาคุณภาพชีวิตคนพิการ ในการประชุมครั้งที่    
                        </asp:Label>
                        <div class="col-sm-2" style="position: relative">
                            <nep:TextBox ID="TextBoxAttachPage3" TextMode="Number" NumberFormat="N0" runat="server"
                                Min="1" CssClass="form-control"></nep:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TextBoxAttachPage3"
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                            <span class="form-control-desc">หน้า</span>
                        </div>
                    </div>
                    <%--<div class="form-group form-group-sm">
                         <div class="form-group form-group-sm">
                                <label class="col-sm-10 control-label control-label-left without-delimit" style="position:relative">
                                     ผนวก 1 ระเบียบคณะกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการแห่งชาติ ว่าด้วยการพิจารณาอนุมัติ การจ่ายเงินเพื่อการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการและการจัดทำรายงานสถานะการเงินและการบริหารกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ พ.ศ. ๒๕๕๒ และที่แก้ไขเพิ่มเติม จำนวน 
                                </label>
                                <div class="col-sm-2" style="position:relative">
                                    <nep:TextBox ID="TextBoxAttachPage1x" width="80px" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" CssClass="form-control"></nep:TextBox> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TextBoxAttachPage1x" 
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                                        <span style="position:relative"> หน้า </span>
                                </div>
                                
                             
                             <label class="col-sm-10 control-label control-label-left without-delimit" style="position:relative">    
                                     ผนวก 2 ประกาศคณะอนุกรรมการบริหารกองทุนส่งเสริมและพัฒนาคุณภาพ ชีวิตคนพิการ ว่าด้วยเรื่องกำหนดอัตราวงเงินและรายการค่าใช้จ่ายที่กองทุนให้การสนับสนุนแผนงาน หรือการเกี่ยวกับการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ พ.ศ. ๒๕๖๐ จำนวน
                             </label>   
                             <div class="col-sm-2" style="position:relative">    
                                    <nep:TextBox  ID="TextBoxAttachPage2" width="80px" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" CssClass="form-control"></nep:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxAttachPage2" 
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                                 <span class="form-control-desc">  หน้า</span>
                                </div>
                                         
                              
                                                             <div class="col-sm-12"  >
                                     
                                     <span style='display:flex;'> ผนวก 3 แจ้งผลการพิจารณาโครงการของคณะอนุกรรมการบริหารกองทุนส่งเสริม   และพัฒนาคุณภาพชีวิตคนพิการ ในการประชุมครั้งที่    
                                         <nep:TextBox  ID="TextboxMeetingNo" width="100px" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" CssClass="form-control"></nep:TextBox> 
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextboxMeetingNo" 
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                                         เมื่อวันที่ 
                                        <nep:DatePicker runat="server" ID="DatePickerMeetingDate" width="300px" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate) %>"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DatePickerMeetingDate" 
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>"
                                ValidationGroup="SaveContract" /> 
                                         จำนวน  <nep:TextBox  ID="TextBoxAttachPage3" width="100px" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" CssClass="form-control"></nep:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TextBoxAttachPage3" 
                                runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, '.') %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_Location) %>"
                                ValidationGroup="SaveContract" />
                                         หน้า 
                                     </span>
                                   
                                           
                                </div>
                         </div>
                    </div>--%>
                </div>

                <div class="form-horizontal">
                    <div class="form-group form-group-sm">

                        <div class="col-sm-6">
                            <nep:TextBox ID="TextBoxRemark" runat="server" TextMode="MultiLine"
                                MaxLength="4000" CssClass="form-control textarea-height"></nep:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <nep:C2XFileUpload runat="server" ID="FileUploadSupport" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading">
                <h3 class="panel-title">งวดสัญญา</h3>
            </div>
            <style>
                .form-group-sm .form-control.form-control-datepicker {
                    width: 90%;
                    float: left;
                }
            </style>

            <%--                        <asp:CustomValidator ID="CustomValidator1" CssClass="error-text" runat="server"
                            OnServerValidate="CustomValidatorContractDate_ServerValidate"
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.Contract_EndDate, Model.Contract_StartDate) %>"
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.OverThanOrEqual, Model.Contract_EndDate, Model.Contract_StartDate) %>"
                            ValidationGroup="SaveContract" />--%>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-1">งวดที่ 1</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerStartDue1" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />

                        </div>
                        <label class="col-sm-1">ถึง</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerEndDue1" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />


                        </div>
                        <label class="col-sm-1">จำนวน</span></label>
                        <div class="col-sm-2">
                            <nep:TextBox ID="TextBoxDueAmount1" TextMode="Number" NumberFormat="##.##" runat="server"
                                CssClass="form-control"></nep:TextBox>
                        </div>
                        <label class="col-sm-1">บาท</span></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-1">งวดที่ 2</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerStartDue2" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />

                        </div>
                        <label class="col-sm-1">ถึง</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerEndDue2" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />


                        </div>
                        <label class="col-sm-1">จำนวน</span></label>
                        <div class="col-sm-2">
                            
                            <nep:TextBox ID="TextBoxDueAmount2" TextMode="Number" NumberFormat="##.##" runat="server"
                                CssClass="form-control"></nep:TextBox>
                        </div>
                        <label class="col-sm-1">บาท</span></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-1">งวดที่ 3</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerStartDue3" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />

                        </div>
                        <label class="col-sm-1">ถึง</span></label>
                        <div class="col-sm-3">
                            <nep:DatePicker runat="server" ID="DatePickerEndDue3" ClearTime="true" EnabledTextBox="true" ValidationGroup="SaveContract" />


                        </div>
                        <label class="col-sm-1">จำนวน</span></label>
                        <div class="col-sm-2">
                            <nep:TextBox ID="TextBoxDueAmount3" TextMode="Number" NumberFormat="##.##" runat="server"
                                CssClass="form-control"></nep:TextBox>
                        </div>
                        <label class="col-sm-1">บาท</span></label>
                    </div>
                </div>
            </div>
            <div class="panel panel-default"  id="Div2" runat="server">
                 <div class="panel-heading">
                <h3 class="panel-title">เอกสารแนบคู่ฉบับสัญญาที่ลงนามแล้ว</h3>
                </div>
                <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3">เอกสารแนบคู่ฉบับสัญญาที่ลงนามแล้ว</span></label>
                        <div class="col-sm-9">
                            <nep:C2XFileUpload runat="server" ID="C2XFileUploadSignedContract" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>"  />
                          <%--  <asp:CustomValidator ID="CustomValidator2" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredFileKTB_ServerValidate" ControlToValidate="C2XFileUploadSignedContract"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ValidationGroup="SaveContract" />--%>
                        </div>
                    </div>
                   </div>
                </div>
            </div>
            <div class="panel panel-default"  id="myDivUploadFileKTB" runat="server">
                 <div class="panel-heading">
                <h3 class="panel-title">ผลการโอนเงินผ่านระบบ KTB Corporate Online </h3>
                </div>
                <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3">อัพโหลดไฟล์ที่ได้จากระบบ KTB </span></label>
                        <div class="col-sm-9">
                            <nep:C2XFileUpload runat="server" ID="FileUploadKTB" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>"  />
                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredFileKTB_ServerValidate" ControlToValidate="FileUploadKTB"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ValidationGroup="SaveContract" />--%>
                        </div>
                    </div>
                   </div>
                </div>
            </div>
             <div class="panel panel-default"  id="DivRefund" runat="server">
                 <div class="panel-heading">
                <h3 class="panel-title">การคืนเงิน(ในกรณีทำสัญญาแล้ว)</h3>
                </div>
                <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-3">แนบเอกสารการคืนเงิน</span></label>
                        <div class="col-sm-9">
                            <nep:C2XFileUpload runat="server" ID="C2XFileUploadRefund" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>"  />
                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="error-text" ValidateEmptyText="true"
                                    OnServerValidate="CustomRequiredFileKTB_ServerValidate" ControlToValidate="FileUploadKTB"
                                    Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Contract_FileKTB) %>"
                                    ValidationGroup="SaveContract" />--%>
                        </div>
                    </div>
                     <div class="form-group form-group-sm">
                        <div class="col-sm-6">
                            <nep:TextBox ID="TextBoxRefund" runat="server" TextMode="MultiLine"
                                MaxLength="4000" CssClass="form-control textarea-height"></nep:TextBox>
                        </div>
                        </div>
                    </div>
                   </div>
                </div>
            </div>
        </div>



        <div id="Div1" class="form-horizontal" runat="server">
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm" ValidationGroup="SaveContract"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSave_Click" Visible="false" />
                    <asp:HyperLink ID="HyperLinkPrint" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>"
                        CssClass="btn btn-default btn-sm" Visible="false"
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportContractControl?projectID={0}&report=contract", ProjectID ) %>' Target="_blank" />
                    <%--                     <asp:HyperLink ID="HyperLinkPrint" runat="server" Text="<%$ code:Nep.Project.Resources.UI.ButtonPrint %>"
                        CssClass="btn btn-default btn-sm" Visible="false" 
                        NavigateUrl='<%$ code:String.Format("~/Report/ReportFormatContract?projectID={0}", ProjectID ) %>' Target="_blank" />   --%>
                    <asp:Button runat="server" ID="ButtonUndoCancelContract" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('ต้องการทำสัญญาใหม่หรือไม่ ?')"
                        Text="ทำสัญญาใหม่" OnClick="ButtonUndoCancelContract_Click" Visible="false" />
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-default btn-sm"
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="กลับ"></asp:HyperLink>
                    <asp:Button runat="server" ID="ButtonCancelContract" CssClass="btn btn-default btn-sm" OnClientClick="return ConfirmToCancelContract()"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancelContract%>" OnClick="ButtonCancelContract_Click" Visible="false" />
                    <asp:Button runat="server" ID="ButtonEditContractNo" CssClass="btn btn-default btn-sm" OnClientClick="VueContract.EditContractNo(1);return false;"
                        Text="แก้ไขเลขที่สัญญา" Visible="false" />
                    <asp:Button runat="server" ID="ButtonEditStartEndContractDate" CssClass="btn btn-default btn-sm" ValidationGroup="SaveStartEndContractDate"
                        Text="บันทึกวันที่เริ่มสัญญาและสิ้นสุดสัญญา" Visible="false" OnClick="ButtonEditStartEndContractDate_Click" />
                      <asp:Button runat="server" ID="ButtonRefund" CssClass="btn btn-default btn-sm" ValidationGroup="SaveRefund"
                        Text="บันทึกการคืนเงิน(ทำสัญญาแล้ว)" Visible="false" OnClick="ButtonRefund_Click" />
                    <asp:ImageButton ID="ImageButtonRefresh" runat="server" ToolTip="รีเฟรช"
                        ImageUrl="~/Images/icon/reload_icon_16.png" BorderStyle="None" CssClass="button-add-targetgroup"
                        OnClick="ImageButtonRefresh_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
