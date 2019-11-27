<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabPersonalControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.TabPersonalControl" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelPersonal" UpdateMode="Conditional" runat="server" >
    <ContentTemplate>
        <style type="text/css"> 
            .combobox-width {
                width:100%;
            }
        </style>
        <asp:HiddenField runat="server" ID="HiddenProjectID" />

        <div class="panel panel-default"> <!--ผู้รับผิดชอบโครงการ-->
            <div class="panel-heading">
                <h3 class="panel-title"><%: Model.Personal_Responsibility %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_IDCardNo %><span class="required" id="LabelIDCardNoSign" runat="server"></span></label>
                        <div class="col-sm-4 control-block">
                            <nep:TextBox ID="TextBoxPersonalMainIDCardNo" runat="server" CssClass="form-control" TextMode="SingleLine" />                            
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPersonalMainIDCardNo"  runat="server" ID="MaskedEditExtenderPersonalMainIDCardNo"
                                Mask="9\-9999\-99999\-99\-9" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"
                                />     
                            <asp:CustomValidator ID="CustomValidatorIDCardNo" ControlToValidate="TextBoxPersonalMainIDCardNo" runat="server" CssClass="error-text"
                                ClientValidationFunction="c2x.ClientValidateCitizenIdRequiredValidator" ValidateEmptyText="true"
                                OnServerValidate="CustomValidatorIDCardNo_ServerValidate" 
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IDCardNo) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_IDCardNo) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>    
                                                
                            <nep:IDCardNumberValidator ID="IDCardNumberValidatorMainIDCardNo" ControlToValidate="TextBoxPersonalMainIDCardNo" runat="server" 
                                CssClass="error-text"  SetFocusOnError="true"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.InvalidIDCardNo) %>" 
                                ValidationGroup="SavePersonal"  />
                                               
                        </div>
                    </div> <!-- เลขที่บัตรประชาชน -->
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Firstname %><span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">
                            <asp:DropDownList ID="DropDownListPrefix1" runat="server" CssClass="form-control" Width="90"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrefix1" ControlToValidate="DropDownListPrefix1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>

                            <asp:TextBox ID="TextBoxFirstName1" runat="server" MaxLength="100" CssClass="form-control" Width="195"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName1" ControlToValidate="TextBoxFirstName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Lastname %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxLastName1" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName1" ControlToValidate="TextBoxLastName1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                    </div> <!-- ชื่อ-สกุล -->
                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Address %><span class="required"></span></label>                        
                        <div class="col-sm-2">                            
                            <asp:TextBox ID="TextBoxAddress1" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress1" ControlToValidate="TextBoxAddress1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-1 control-label"><%= Model.ProjectInfo_Moo %></label>
                        <div class="col-sm-1">                            
                            <asp:TextBox ID="TextBoxMoo1" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxBuilding1" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div> <!-- บ้านเลขที่ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>                        
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxSoi1" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Road %></label>
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxRoad1" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>                
                    </div> <!-- ซอย -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlProvince1" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorProvince1" runat="server" ControlToValidate="DdlProvince1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        />	
                                                  
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlDistrict1" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorDistrict1" runat="server" ControlToValidate="DdlDistrict1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        />
                                           
                        </div>                
                    </div> <!-- ตำบล -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlSubDistrict1" runat="server" CssClass="combobox-width" />
                             <asp:CustomValidator ID="CustomValidatorSubDistrict1" runat="server" ControlToValidate="DdlSubDistrict1"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        /> 
                                                   
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                        <div class="col-sm-4">  
                            <nep:TextBox ID="TextBoxPostCode1" MaxLength="10" runat="server" CssClass="form-control"></nep:TextBox> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode1" ControlToValidate="TextBoxPostCode1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>        
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode1"  runat="server" ID="MaskedEditExtenderPostCode1"
                                        Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"  />                                   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostCode1" ControlToValidate="TextBoxPostCode1" runat="server"
                                CssClass="error-text" ValidationExpression="\d{5}"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.PostCodeField) %>"
                                ValidationGroup="SavePersonal" />                         
                        </div>                
                    </div> <!-- จังหวัด -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxTelephone1" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>     
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone1" ControlToValidate="TextBoxTelephone1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>                       
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Mobile %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxMobile1" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                        </div>    
                    </div> <!-- โทรศัพท์ -->

                    <div class="form-group form-group-sm">
                         <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxFax1" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>    
                        </div>          

                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %><span class="required" id="LabelEmail1Sign" runat="server"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxEmail1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>         
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail1" ControlToValidate="TextBoxEmail1" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmail" ControlToValidate="TextBoxEmail1" runat="server"
                                CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ValidationGroup="SavePersonal" />                 
                        </div>
                    </div> <!-- อีเมล์ -->  
                                     
                </div><!--form-horizontal-->
            </div><!--panel-body-->
        </div> <!--\ผู้รับผิดชอบโครงการ-->
    
        <div class="panel panel-default"><!--ผู้ประสานงานโครงการ (1)-->
            <div class="panel-heading">
                <h3 class="panel-title"><%: Model.Personal_Contract1 %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">                    
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <div>
                                <asp:CheckBox ID="CheckBoxDupData1" CssClass="form-control-checkbox" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="CheckBoxDupData1_CheckedChanged" Text="ที่อยู่เดียวกับผู้รับผิดชอบโครงการ"/>
                                 <asp:Image ID="ImageHelp1" runat="server" ToolTip="ถ้าผู้ประสานงานโครงการ เป็นข้อมูลเดียวกัน ให้เลือกที่ช่องสี่เหลี่ยม เพื่อให้ระบบดึงข้อมูลผู้ประสานงานให้" 
                                   ImageUrl="~/Images/icon/about.png" BorderStyle="None" /> 
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Firstname %><span class="required"></span></label>                        
                        <div class="col-sm-4 control-block">
                            <asp:DropDownList ID="DropDownListPrefix2" runat="server" CssClass="form-control" Width="90"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrefix2" ControlToValidate="DropDownListPrefix2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Prefix) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>

                            <asp:TextBox ID="TextBoxFirstName2" runat="server" MaxLength="100" CssClass="form-control" Width="195"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName2" ControlToValidate="TextBoxFirstName2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Lastname %><span class="required"></span></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxLastName2" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName2" ControlToValidate="TextBoxLastName2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                    </div> <!-- ชื่อ-สกุล -->
                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Address %><span class="required"></span></label>                        
                        <div class="col-sm-2">                            
                            <asp:TextBox ID="TextBoxAddress2" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress2" ControlToValidate="TextBoxAddress2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>
                        </div>
                        
                        <label class="col-sm-1 control-label"><%= Model.ProjectInfo_Moo %></label>
                        <div class="col-sm-1">                            
                            <asp:TextBox ID="TextBoxMoo2" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxBuilding2" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div> <!-- บ้านเลขที่ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>                        
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxSoi2" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Road %></label>
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxRoad2" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>                
                    </div> <!-- ซอย -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlProvince2" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorProvince2" runat="server" ControlToValidate="DdlProvince2"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                        />	 

                                                   
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %><span class="required"></span></label>
                        <div class="col-sm-4">  
                            <asp:TextBox ID="DdlDistrict2" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator ID="CustomValidatorDistrict2" runat="server" ControlToValidate="DdlDistrict2"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                        />
                        </div>                
                    </div> <!-- ตำบล -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                           <asp:TextBox ID="DdlSubDistrict2" runat="server" CssClass="combobox-width" />
                           <asp:CustomValidator ID="CustomValidatorSubDistrict2" runat="server" ControlToValidate="DdlSubDistrict2"
                                        OnServerValidate="CustomValidatorCombobox_ServerValidate" ClientValidationFunction="c2x.validateComboBoxRequired"
                                        CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" SetFocusOnError="true"
                                        Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                        /> 
                                                   
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %><span class="required"></span></label>
                        <div class="col-sm-4">  
                            <nep:TextBox ID="TextBoxPostCode2" MaxLength="10" runat="server" CssClass="form-control"></nep:TextBox> 
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode2"  runat="server" ID="MaskedEditExtender2"
                                        Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"  />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode2" ControlToValidate="TextBoxPostCode2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>                           
                        </div>                
                    </div> <!-- จังหวัด -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %><span class="required"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxTelephone2" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>     
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone2" ControlToValidate="TextBoxTelephone2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>                        
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxFax2" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>    
                        </div>                
                    </div> <!-- โทรศัพท์ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %><span class="required" id="LabelEmail2Sign" runat="server"></span></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxEmail2" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail2" ControlToValidate="TextBoxEmail2" runat="server" CssClass="error-text"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                ValidationGroup="SavePersonal" SetFocusOnError="true"/>    
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBoxEmail2" runat="server"
                                CssClass="error-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.EmailField) %>"
                                ValidationGroup="SavePersonal" />                           
                        </div>
                    </div> <!-- อีเมล์ -->                   
                </div>
            </div>
        </div><!--\ผู้ประสานงานโครงการ (1) -->
        
        <div class="panel panel-default"><!--ผู้ประสานงานโครงการ (2) -->
            <div class="panel-heading">
                <h3 class="panel-title"><%: Model.Personal_Contract2 %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12">
                            <div>
                                <asp:CheckBox ID="CheckBoxDupData2" runat="server" CssClass="form-control-checkbox" AutoPostBack="true"
                                    OnCheckedChanged="CheckBoxDupData2_CheckedChanged" Text="ที่อยู่เดียวกับผู้รับผิดชอบโครงการ"/>
                                <asp:Image ID="ImageHelp2" runat="server" ToolTip="ถ้าผู้ประสานงานโครงการ เป็นข้อมูลเดียวกัน ให้เลือกที่ช่องสี่เหลี่ยม เพื่อให้ระบบดึงข้อมูลผู้ประสานงานให้" ImageUrl="~/Images/icon/about.png" BorderStyle="None" /> 
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Firstname %></label>                        
                        <div class="col-sm-4 control-block">
                            <asp:DropDownList ID="DropDownListPrefix3" runat="server" CssClass="form-control" Width="90"></asp:DropDownList>
                            

                            <asp:TextBox ID="TextBoxFirstName3" runat="server" MaxLength="100" CssClass="form-control" Width="195"></asp:TextBox>
                            <asp:CustomValidator id="CustomValidatorFirstName3" runat="server" ControlToValidate = "TextBoxFirstName3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true"
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Firstname) %>">
                            </asp:CustomValidator>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Lastname %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxLastName3" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:CustomValidator id="CustomValidatorLastName3" runat="server" ControlToValidate = "TextBoxLastName3" 
                                CssClass="error-text" ValidationGroup="SavePersonal"  ValidateEmptyText="true"
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Lastname) %>">
                            </asp:CustomValidator>
                        </div>
                    </div> <!-- ชื่อ-สกุล -->
                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Address %></label>                        
                        <div class="col-sm-2">                            
                            <asp:TextBox ID="TextBoxAddress3" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:CustomValidator id="CustomValidatorAddress3" runat="server" ControlToValidate = "TextBoxAddress3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" 
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Address) %>">
                            </asp:CustomValidator>
                        </div>
                        
                        <label class="col-sm-1 control-label"><%= Model.ProjectInfo_Moo %></label>
                        <div class="col-sm-1">                            
                            <asp:TextBox ID="TextBoxMoo3" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Building %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxBuilding3" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div> <!-- บ้านเลขที่ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Soi %></label>                        
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxSoi3" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Road %></label>
                        <div class="col-sm-4">                            
                            <asp:TextBox ID="TextBoxRoad3" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                        </div>                
                    </div> <!-- ซอย -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="DdlProvince3" runat="server" CssClass="combobox-width" />
                           
                            <asp:CustomValidator id="CustomValidatorProvince3" runat="server" ControlToValidate = "DdlProvince3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" 
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Province) %>">
                            </asp:CustomValidator>            
                        </div>

                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_District %></label>
                        <div class="col-sm-4">  
                            <asp:TextBox ID="DdlDistrict3" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator id="CustomValidatorDistrict3" runat="server" ControlToValidate = "DdlDistrict3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" 
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_District) %>">
                            </asp:CustomValidator>                 
                        </div>                
                    </div> <!-- ตำบล -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_SubDistrict %></label>                        
                        <div class="col-sm-4">
                          <asp:TextBox ID="DdlSubDistrict3" runat="server" CssClass="combobox-width" />
                            <asp:CustomValidator id="CustomValidatorSubDistrict3" runat="server" ControlToValidate = "DdlSubDistrict3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" 
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_SubDistrict) %>">
                            </asp:CustomValidator>                  
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Postcode %></label>
                        <div class="col-sm-4">  
                            <nep:TextBox ID="TextBoxPostCode3" MaxLength="10" runat="server" CssClass="form-control"></nep:TextBox>   
                            <ajaxToolkit:MaskedEditExtender TargetControlID="TextBoxPostCode3"  runat="server" ID="MaskedEditExtender3"
                                        Mask="99999" MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" ClearMaskOnLostFocus="false"  />  
                            <asp:CustomValidator id="CustomValidatorPostCode3" runat="server" ControlToValidate = "TextBoxPostCode3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" Enabled="true" Display="Dynamic"
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Postcode) %>">
                            </asp:CustomValidator>                       
                        </div>                
                    </div> <!-- จังหวัด -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Telephone %></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxTelephone3" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>  
                            <asp:CustomValidator id="CustomValidatorTelephone3" runat="server" ControlToValidate = "TextBoxTelephone3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true"
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Telephone) %>">
                            </asp:CustomValidator>                             
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Fax %></label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxFax3" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>    
                        </div>                
                    </div> <!-- โทรศัพท์ -->

                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Email %></label>                        
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBoxEmail3" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox> 
                            <asp:CustomValidator id="CustomValidatorEmail3" runat="server" ControlToValidate = "TextBoxEmail3"
                                CssClass="error-text" ValidationGroup="SavePersonal" ValidateEmptyText="true" 
                                OnServerValidate="ProjectPersonalAddress3Validate" ClientValidationFunction="projectPersonalAddress3Validate"
                                Text="<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                                ErrorMessage = "<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectInfo_Email) %>">
                            </asp:CustomValidator>    
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail3" runat="server" ControlToValidate="TextBoxEmail3"  CssClass="error-text" 
                            Text="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.Model.ProjectInfo_Email) %>" 
                            ErrorMessage="<%$ code: String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.Model.ProjectInfo_Email) %>"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SavePersonal"/>                           
                        </div>
                    </div> <!-- อีเมล์ -->                  
                </div>
            </div>
        </div><!--\ผู้ประสานงานโครงการ (2) -->

        <div class="panel panel-default"><!--การมีส่วนร่วมขององค์การปกครองส่วนท้องถิ่น-->
            <div class="panel-heading">
                <h3 class="panel-title"><%: UI.TitleInvolvementOfLocalGovernments %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <div class="col-sm-8">
                            <!--1. สถานที่-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">1.</span><%= Model.ProjectInfo_SupportPlace %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportPlace1" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName1" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--2. วิทยากร-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">2.</span><%= Model.ProjectInfo_InstructorAmt %>
                                </label>
                                <div class="col-sm-4" style="position:relative">
                                    <nep:TextBox ID="TextBoxInstructorAmt2" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" CssClass="form-control"></nep:TextBox>
                                    <span class="form-control-desc"><%= UI.LabelPerson %></span>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_InstructorListFileID %>
                                </label>
                                <div class="col-sm-8">    
                                    <nep:C2XFileUpload runat="server" ID="FileUploadInstructor" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>                      
                            </div>
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName2" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--3. งบประมาณ-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">3.</span><%= Model.ProjectInfo_SupportBudgetAmt %>
                                </label>
                                <div class="col-sm-4" style="position:relative">
                                    <nep:TextBox ID="TextBoxSupportBudgetAmt3" TextMode="Number" runat="server" 
                                        Min="1" Max="999999999.99" CssClass="form-control"></nep:TextBox>
                                    <span class="form-control-desc desc-bath"><%= UI.LabelBath %></span>
                                </div>
                            </div>                            
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName3" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--4. อุปกรณ์-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">4.</span><%= Model.ProjectInfo_SupportEquipment %>
                                </label>
                                <div class="col-sm-8">
                                    <nep:TextBox ID="TextBoxSupportEquipment4" runat="server" MaxLength="500" CssClass="form-control"></nep:TextBox>
                                </div>
                            </div>                            
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName4" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--5. อาหาร - เครื่องดื่ม จำนวน-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">5.</span><%= Model.ProjectInfo_SupportDrinkFoodAmt %>
                                </label>
                                <div class="col-sm-4" style="position:relative">
                                    <nep:TextBox ID="TextBoxSupportDrinkFoodAmt5" TextMode="Number" runat="server" 
                                        Min="1" Max="999999999.99" CssClass="form-control"></nep:TextBox>
                                    <span class="form-control-desc desc-bath"><%= UI.LabelBath %></span>
                                </div>
                            </div>                            
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName5" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--6. ยานพาหนะ (โปรดแนบรายชื่อ)-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">6.</span><%= Model.ProjectInfo_VehicleListFile %>
                                </label>
                                <div class="col-sm-8">
                                    <nep:C2XFileUpload runat="server" ID="FileUploadVehicle" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" /> 
                                </div>
                            </div>                            
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName6" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--7. อาสาสมัคร จำนวน-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">7.</span><%= Model.ProjectInfo_SupportValunteerAmt %>
                                </label>
                                <div class="col-sm-4" style="position:relative">
                                    <nep:TextBox ID="TextBoxSupportValunteerAmt7" TextMode="Number" NumberFormat="N0" runat="server" 
                                        Min="1" Max="999999999" CssClass="form-control"></nep:TextBox>
                                    <span class="form-control-desc"><%= UI.LabelPerson %></span>
                                </div>
                            </div>  
                            <div class="form-group form-group-sm">
                                 <label class="col-sm-4 control-label control-label-left without-delimit">
                                     <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_VehicleListFile %>
                                 </label>
                                <div class="col-sm-8">
                                    <nep:C2XFileUpload runat="server" ID="FileUploadValunteer" MultipleFileMode="true" ViewAttachmentPrefix="<%$ code:FollowupViewAttachmentPrefix %>" />  
                                </div>  
                            </div>                          
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName7" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                            <!--8. อื่นๆ (โปรดระบุให้ชัดเจน)-->
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">8.</span><%= Model.ProjectInfo_SupportOther %>
                                </label>
                                <div class="col-sm-8">
                                    <nep:TextBox ID="TextBoxSupportOther8" runat="server" MaxLength="500" CssClass="form-control"></nep:TextBox>
                                </div>
                            </div>                            
                            <div class="form-group form-group-sm">
                                <label class="col-sm-4 control-label control-label-left without-delimit">
                                    <span class="label-no">&nbsp;</span><%= Model.ProjectInfo_SupportOrgName %>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBoxSupportOrgName8" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                </div>                      
                            </div>

                        </div><!--ข้อมูล-->
                        <div class="col-sm-4">
                            <span class="field-desc"><%: UI.LabelFieldDescription %></span>
                                <%= UI.InvolvementOfLocalGovernmentsDesc %>
                        </div>
                    </div><!--form-group-->                    
                </div><!--form-horizontal-->
            </div>
        </div><!--การมีส่วนร่วมขององค์การปกครองส่วนท้องถิ่น-->
        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                     <asp:Button runat="server" ID="ButtonDraft" CssClass="btn btn-primary btn-sm"  
                        Text="บันทึกร่าง" OnClick="ButtonSavePersonal_Click" Visible="false" />
                    <asp:Button runat="server" ID="ButtonSavePersonal" CssClass="btn btn-primary btn-sm" ValidationGroup="SavePersonal"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" OnClick="ButtonSavePersonal_Click" Visible="false" />

                    <asp:Button runat="server" ID="ButtonSendProjectInfo" CssClass="btn btn-primary btn-sm"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSendProjectInfo%>" 
                         OnClientClick="return ConfirmToSubmitProject()"
                        OnClick="ButtonSendProjectInfo_Click" Visible="false"/>

                    <asp:Button runat="server" ID="ButtonReject" CssClass="btn btn-default btn-sm" 
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonReject %>" Visible="false" OnClientClick="c2x.clearResultMsg(); return openRejectCommentForm();" />  

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
</asp:UpdatePanel><!--UpdatePanelPersonal-->

