using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabContractControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
       
        public Decimal ProjectID
        {
            get
            {
                if (ViewState["ProjectID"] == null)
                {
                    string stID = Request.QueryString["id"];
                    decimal id = 0;
                    Decimal.TryParse(stID, out id);
                    ViewState["ProjectID"] = id;
                }


                return (decimal)ViewState["ProjectID"];
            }

            set
            {
                ViewState["ProjectID"] = value;
            }
        }

        public String ApprovalYear
        {
            get
            {
                return ViewState["ApprovalYear"].ToString();
            }

            set
            {
                ViewState["ApprovalYear"] = value;
            }
        }

        public String FollowupViewAttachmentPrefix
        {
            get
            {
                if (ViewState["FollowupViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["FollowupViewAttachmentPrefix"] = prefix;
                }
                return ViewState["FollowupViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ViewAttachmentPrefix"] = value;
            }
        }

        public bool CanSaveContract
        {
            get
            {
                bool isCan = false;
                if (ViewState["CanSaveContract"] != null)
                {
                    isCan = Convert.ToBoolean(ViewState["CanSaveContract"]);
                }
                return isCan;
            }
            set
            {
                ViewState["CanSaveContract"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //DateTime? contractDate = DatePickerContractDate.SelectedDate;

            //if (IsGenerateContractNo(contractDate))
            //{
            //    TextBoxContractNo.Enabled = false;
            //    TextBoxContractNo.Attributes.Add("placeholder", Nep.Project.Resources.UI.LabelGeneratebySystem);
            //}
            //else
            //{
            //    TextBoxContractNo.Enabled = true;
            //    TextBoxContractNo.Attributes.Remove("placeholder");
            //}

            if (IsGenerateContractNo())
            {
                TextBoxContractNo.Enabled = false;
                TextBoxContractNo.Attributes.Add("placeholder", Nep.Project.Resources.UI.LabelGeneratebySystem);
                CustomValidatorContractNo.Enabled = false;
            }
            else
            {
                TextBoxContractNo.Enabled = true;
                TextBoxContractNo.Attributes.Remove("placeholder");
                CustomValidatorContractNo.Enabled = true;
            }

            if (!CanSaveContract)
            {
                TextBoxContractNo.Enabled = false;
                CustomValidatorContractNo.Enabled = false;
            }

        }

        public void BindData()
        {
            try
            {
                if (ProjectID > 0)
                {
                    var result = _service.GetProjectContractByProjectID(ProjectID);
                    if (result.IsCompleted)
                    {
                        ServiceModels.ProjectInfo.TabContract model = result.Data;
                        if (model != null)
                        {
                            var obj = _service.GetProjectApprovalResult(ProjectID);
                            if (obj.IsCompleted)
                            {
                                LabelMeetingText.Text = $"ผนวก 3 แจ้งผลการพิจารณาโครงการของคณะอนุกรรมการบริหารกองทุนส่งเสริม และพัฒนาคุณภาพชีวิตคนพิการ ในการประชุมครั้งที่ {obj.Data.ApprovalNo} / " + 
                                $"{int.Parse(obj.Data.ApprovalYear) + 543} เมื่อวันที่ {obj.Data.ApprovalDate:dd/MM/yyyy}";

                            }

                            ApprovalYear = model.ApprovalYear;
                            List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
                            bool canSave = functions.Contains(Common.ProjectFunction.SaveContract);
                            CanSaveContract = canSave;
                            ButtonSave.Visible = canSave;
                            CheckBoxAuthorizeFlag.Enabled = canSave;

                            HyperLinkPrint.Visible = functions.Contains(Common.ProjectFunction.PrintContract);
                            ButtonCancelContract.Visible = functions.Contains(Common.ProjectFunction.CancelContract);
                            C2XFileUploadAuthorizeDoc.Enabled = functions.Contains(Common.ProjectFunction.SaveContract);

                            //LabelContractNo.Text = Common.Web.WebUtility.DisplayInHtml(model.ContractNo, null, "-");
                            TextBoxContractNo.Text = model.ContractNo;
                            DatePickerContractDate.SelectedDate = model.ContractDate;
                            DatePickerContractStartDate.SelectedDate = model.ContractStartDate;
                            DatePickerContractEndDate.SelectedDate = model.ContractEndDate;
                            LabelBudgetRequestAmount.Text = Common.Web.WebUtility.DisplayInHtml(model.RequestBudgetAmount, "N2", "0.00");
                            LabelApprovedAmount.Text = Common.Web.WebUtility.DisplayInHtml(model.ReviseBudgetAmount, "N2", "0.00");
                            TextBoxContractLocation.Text = model.Location;
                            TextBoxWitnessFirstName1.Text = model.ViewerName1;
                            TextBoxWitnessLastname1.Text = model.ViewerSurname1;
                            TextBoxWitnessFirstName2.Text = model.ViewerName2;
                            TextBoxWitnessLastName2.Text = model.ViewerSurname2;
                            TextBoxRefFirstName.Text = model.DirectorFirstName;
                            TextBoxRefLastName.Text = model.DirectorLastName;
                            TextBoxRefPosition.Text = model.DirectorPosition;
                            TextBoxRefNo1.Text = model.AttorneyNo;
                            TextBoxRemark.Text = model.Remark;
                            DatePickerRefDate.SelectedDate = model.ContractGiverDate;
                            TextBoxProvinceNo1.Text = model.ProvinceContractNo;

                            DatePickerProvinceDate.SelectedDate = model.ProvinceContractDate;

                            DatePickerReceiveDate.SelectedDate = model.ContractReceiveDate;
                            LabelContractOrgName.Text = model.OrganizationName;
                            LabelContractOrgAddress.Text = model.OraganizationAddress;
                            LabelContractOrgTelephone.Text = Common.Web.WebUtility.DisplayInHtml(model.Telephone, null, "-");
                            LabelContractOrgFax.Text = Common.Web.WebUtility.DisplayInHtml(model.Fax, null, "-");
                            LabelContractOrgEmail.Text = Common.Web.WebUtility.DisplayInHtml(model.Email, null, "-");
                            //DatePickerMeetingDate.SelectedDate = model.MeetingDate;
                            TextBoxAttachPage1.Text = model.AttachPage1?.ToString();
                            TextBoxAttachPage2.Text = model.AttachPage2?.ToString();
                            TextBoxAttachPage3.Text = model.AttachPage3?.ToString();
                            //TextboxMeetingNo.Text = model.MeetingNo?.ToString();
                            List<ServiceModels.KendoAttachment> SupportFiles = model.SupportAttachments;
                  
                            //end kenghot
                            FileUploadSupport.ClearChanges();
                            FileUploadSupport.ExistingFiles = SupportFiles;
                            FileUploadSupport.DataBind();

                            int refNo2 = 0;
                            if (!String.IsNullOrEmpty(model.AttorneyYear) && Int32.TryParse(model.AttorneyYear, out refNo2))
                            {

                                DateTime refNo2Year = new DateTime(refNo2, 1, 1, 0, 0, 0);
                                DatePickerRefNo2.SelectedDate = refNo2Year;
                            }

                            int provContractYear = 0;
                            if (!String.IsNullOrEmpty(model.ProvinceContractYear) && Int32.TryParse(model.ProvinceContractYear, out provContractYear))
                            {
                                DateTime provContractYearDate = new DateTime(provContractYear, 1, 1, 0, 0, 0);
                                DatePickerProvinceNo2.SelectedDate = provContractYearDate;
                            }

                            //ผู้ให้เงินอุดหนุน
                            bool isShowProvinceSupportGiven = (!model.IsCenterContract);
                            if (model.IsCenterContract)
                            {
                                LabelContractRefName.Text = Model.Contract_CenterContractRefName;
                                ContractRefNoBlock.Visible = isShowProvinceSupportGiven;
                                CustomValidatorRefNo1.Enabled = isShowProvinceSupportGiven;
                                CustomValidatorRefNo2.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldTextBoxRefNo1.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldRefNo2.Enabled = isShowProvinceSupportGiven;

                                ContractRefDateBlock.Visible = isShowProvinceSupportGiven;
                                CustomValidatorRefDate.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldValidatorRefDate.Enabled = isShowProvinceSupportGiven;

                                ContractProvinceNoBlock.Visible = isShowProvinceSupportGiven;
                                CustomValidatorProvinceNo1.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldTextBoxProvinceNo1.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldProvinceNo2.Enabled = isShowProvinceSupportGiven;

                                ContractProvinceRefDateBlock.Visible = isShowProvinceSupportGiven;
                                CustomValidatorProvinceDate.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldValidatorProvinceDate.Enabled = isShowProvinceSupportGiven;
                            }
                            else
                            {
                                LabelContractRefName.Text = Model.Contract_ContractRefName;
                            }

                            //มอบอำนาจหรือไม่
                            CheckBoxAuthorizeFlag.Checked = model.AuthorizeFlag;
                            if (model.AuthorizeFlag)
                            {
                                TextBoxReceiverName.Text = model.ReceiverName;
                                TextBoxReceiverSurname.Text = model.ReceiverSurname;
                                TextBoxReceiverPosition.Text = model.ReceiverPosition;
                                DatePickerAuthorizeDate.SelectedDate = model.AuthorizeDate;

                                List<ServiceModels.KendoAttachment> attach = new List<KendoAttachment>();
                                if (model.AuthorizeDocID.HasValue)
                                {
                                    attach.Add(model.AuthorizeDocAttachment);
                                }
                                C2XFileUploadAuthorizeDoc.ClearChanges();
                                C2XFileUploadAuthorizeDoc.ExistingFiles = attach;
                                C2XFileUploadAuthorizeDoc.DataBind();
                            }
                            else
                            {
                                TextBoxContractReceiveName.Text = model.ContractReceiveName;
                                TextBoxContractReceiveSurname.Text = model.ContractReceiveSurname;
                                TextBoxContractReceivePosition.Text = model.ContractReceivePosition;
                            }
                        }
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }                
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "ProcessingPlan", ex);
                ShowErrorMessage(ex.Message);
            }

            RegisterClientScript();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.TabContract model = new ServiceModels.ProjectInfo.TabContract();
            try
            {
                if(Page.IsValid){
                    model = MapControlToTabContract();

                    var result = _service.SaveProjectContract(model);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelContract");
                        ShowResultMessage(result.Message);
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Contract", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        private ServiceModels.ProjectInfo.TabContract MapControlToTabContract()
        {
            ServiceModels.ProjectInfo.TabContract result = new ServiceModels.ProjectInfo.TabContract();

            DateTime contractDate = Convert.ToDateTime(DatePickerContractDate.SelectedDate);
            DateTime startDate = Convert.ToDateTime(DatePickerContractStartDate.SelectedDate);
            DateTime endDate = Convert.ToDateTime(DatePickerContractEndDate.SelectedDate);
            string location = TextBoxContractLocation.Text.Trim();
            string viewerName1 = TextBoxWitnessFirstName1.Text.Trim();
            string viewerSurName1 = TextBoxWitnessLastname1.Text.Trim();
            string viewerName2 = TextBoxWitnessFirstName2.Text.Trim();
            string viewerSurName2 = TextBoxWitnessLastName2.Text.Trim();
            string directorName = TextBoxRefFirstName.Text.Trim();
            string directorSurName = TextBoxRefLastName.Text.Trim();
            string directorPosition = TextBoxRefPosition.Text.Trim();
            string attorneyNo = TextBoxRefNo1.Text.Trim();
                        
            string attorneyYear = (DatePickerRefNo2.SelectedDate.HasValue) ? ((DateTime)DatePickerRefNo2.SelectedDate).Year.ToString() : "";
            DateTime? giveDate = DatePickerRefDate.SelectedDate;
            string provinceNo = TextBoxProvinceNo1.Text.Trim();
            string provinceYear = (DatePickerProvinceNo2.SelectedDate.HasValue) ? ((DateTime)DatePickerProvinceNo2.SelectedDate).Year.ToString() : "";
            DateTime? provinceDate = DatePickerProvinceDate.SelectedDate;
            DateTime? receiveDate = DatePickerReceiveDate.SelectedDate;
            bool authorizeFlag = CheckBoxAuthorizeFlag.Checked;
            string receiverName = TextBoxReceiverName.Text.Trim();
            string receiverSurname = TextBoxReceiverSurname.Text.Trim();
            string receiverPosition = TextBoxReceiverPosition.Text.Trim();
            DateTime? authorizeDate = DatePickerAuthorizeDate.SelectedDate;
            string contractReceiveName = TextBoxContractReceiveName.Text.Trim();
            string contractReceiveSurname = TextBoxContractReceiveSurname.Text.Trim();
            string contractReceivePosition = TextBoxContractReceivePosition.Text.Trim();

            result.ProjectID = ProjectID;
            result.ContractNo = TextBoxContractNo.Text.Trim();
            //result.ContractYear = Convert.ToDecimal(ApprovalYear);
            result.ContractYear = startDate.Year;
            if (startDate.Month >= 10)
            {
                result.ContractYear += 1;
            }
            result.ContractDate = contractDate;
            result.ContractStartDate = startDate;
            result.ContractEndDate = endDate;
            result.Location = location;
            result.ViewerName1 = viewerName1;
            result.ViewerSurname1 = viewerSurName1;
            result.ViewerName2 = viewerName2;
            result.ViewerSurname2 = viewerSurName2;
            result.DirectorFirstName = directorName;
            result.DirectorLastName = directorSurName;
            result.DirectorPosition = directorPosition;
            result.AttorneyNo = attorneyNo;
            result.AttorneyYear = attorneyYear;
            result.ContractGiverDate = giveDate;
            result.ProvinceContractNo = provinceNo;
            result.ProvinceContractYear = provinceYear;
            result.ProvinceContractDate = provinceDate;
            result.ContractReceiveDate = receiveDate;
            result.AuthorizeFlag = authorizeFlag;
            //--------- มอบอำนาจ -----------//
            result.ReceiverName = receiverName;
            result.ReceiverSurname = receiverSurname;
            result.ReceiverPosition = receiverPosition;
            result.AuthorizeDate = authorizeDate;

            //Attachment
            IEnumerable<ServiceModels.KendoAttachment> addedFiles = C2XFileUploadAuthorizeDoc.AddedFiles;
            IEnumerable<ServiceModels.KendoAttachment> removedFiles = C2XFileUploadAuthorizeDoc.RemovedFiles;
            result.AddedAuthorizeDocAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            result.RemovedAuthorizeDocAttachment = (removedFiles.Count() > 0) ? addedFiles.First() : null;
            //-----------------------------//
            result.Remark = TextBoxRemark.Text.Trim();
            result.ContractReceiveName = contractReceiveName;
            result.ContractReceiveSurname = contractReceiveSurname;
            result.ContractReceivePosition = contractReceivePosition;
            result.ipAddress = Request.UserHostAddress;
            addedFiles = FileUploadSupport.AddedFiles;
            removedFiles = FileUploadSupport.RemovedFiles;

            result.AddedSupportAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            result.RemovedSupportAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

            //result.MeetingNo = decimal.Parse(TextboxMeetingNo.Text);
            //result.AttachPage1 = 
            //result.AttachPage2 = decimal.Parse(TextBoxAttachPage2.Text);
            //result.AttachPage3 = decimal.Parse(TextBoxAttachPage3.Text);
   
            decimal d;
            result.AttachPage1 = decimal.TryParse(TextBoxAttachPage1.Text, out d) ? d : 0;
            result.AttachPage2 = decimal.TryParse(TextBoxAttachPage2.Text, out d) ? d : 0;
            result.AttachPage3 = decimal.TryParse(TextBoxAttachPage3.Text, out d) ? d : 0;
            //result.MeetingNo = decimal.TryParse(TextboxMeetingNo.Text, out d) ? d : 0;
            //result.MeetingDate = (DatePickerMeetingDate.SelectedDate.HasValue) ? (DateTime?)Convert.ToDateTime(DatePickerMeetingDate.SelectedDate) : null;
            return result;
        }

        protected void ButtonCancelContract_Click(object sender, EventArgs e)
        {
            var result = _service.CancelProjectContract(ProjectID);
            if (result.IsCompleted)
            {
                Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                page.RebindData("TabPanelContract");
                ShowResultMessage(result.Message);
            }
            else
            {
                ShowErrorMessage(result.Message);
            }
        }

        protected void CustomValidatorContractDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int diff = 0;
            DateTime? startDate = DatePickerContractStartDate.SelectedDate;
            DateTime? endDate = DatePickerContractEndDate.SelectedDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                diff = ((DateTime)endDate).Subtract((DateTime)startDate).Days + 1;
                args.IsValid = (diff > 0);
            }
        }

        protected void CustomRequiredAuthorize_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool chkValue = CheckBoxAuthorizeFlag.Checked;
            bool isValid = true;

            if ((chkValue == true) && (string.IsNullOrEmpty(args.Value)))
            {
                isValid = false;
            }

            args.IsValid = isValid;
        }

        protected void CustomRequiredSupportReceive_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool chkValue = CheckBoxAuthorizeFlag.Checked;
            bool isValid = true;

            if ((chkValue == false) && (string.IsNullOrEmpty(args.Value)))
            {
                isValid = false;
            }

            args.IsValid = isValid;
        }

        private void RegisterClientScript()
        {
            String script = @"
                $(document).ready(function () {
                    var checkBoxId = '"+CheckBoxAuthorizeFlag.ClientID+ @"';
                    var chkFlag = '"+ CheckBoxAuthorizeFlag.Checked + @"';
                    
                    if (chkFlag == 'True'){
		                $('#divAuthorize').show();
                        $('#divSupportReceiver').hide();
	                }
	                else{
		                $('#divAuthorize').hide();
                        $('#divSupportReceiver').show();
	                }

                    $('#'+ checkBoxId).change(function () {
	                    if (!this.checked){
		                    $('#divAuthorize').hide();
                            $('#divSupportReceiver').show();
	                    }else{
		                    $('#divAuthorize').show();
                            $('#divSupportReceiver').hide();
                        }
                    });
                });

                function ConfirmToCancelContract() {
                    var isConfirm = window.confirm('" + Nep.Project.Resources.Message.CancelContractConfirmation + @"');
                    return isConfirm;
                }

                function validateRefData(oSrc, args) {                  
                        
                    var refNo1 = $('#" + TextBoxRefNo1.ClientID + @"').val();
                    refNo1 = $.trim(refNo1);

                    var refNo2DatePicker = $find('DatePickerRefNo2');
                    var refNo2 = refNo2DatePicker.get_selectedDate();

                    var refDateDatePicker = $find('DatePickerRefDate');
                    var refDate =  refDateDatePicker.get_selectedDate();

                    var isValid = true;
                    if(args.Value == ''){
                        isValid = ((refNo1 == '') && (refNo2 == null) && (refDate == null)); 

                        var vRefNo1 = '" + CustomValidatorRefNo1.ClientID + @"';
                        var vRefNo2 = '" + CustomValidatorRefNo2.ClientID + @"';
                        var vRefDate = '" + CustomValidatorRefDate.ClientID + @"';
                        
                        if(isValid){
                            $('#'+ vRefNo1).css('visibility','hidden');
                            $('#'+ vRefNo2).css('visibility','hidden');
                            $('#'+ vRefDate).css('visibility','hidden');
                        }
                    }       
                    
                    args.IsValid = isValid;

                }

                function validateProvinceRefData(oSrc, args) {                  
                        
                    var refNo1 = $('#" + TextBoxProvinceNo1.ClientID + @"').val();

                    var refNo2DatePicker = $find('DatePickerProvinceNo2');
                    var refNo2 = refNo2DatePicker.get_selectedDate();

                    var refDateDatePicker = $find('DatePickerProvinceDate');
                    var refDate =  refDateDatePicker.get_selectedDate();

                    var isValid = true;
                    if(args.Value == ''){
                        isValid = ((refNo1 == '') && (refNo2 == null) && (refDate == null));
                        var vRefNo1 = '" + CustomValidatorProvinceNo1.ClientID + @"';
                        var vRefNo2 = '" + CustomValidatorProvinceNo2.ClientID + @"';
                        var vRefDate = '" + CustomValidatorProvinceDate.ClientID + @"';
                        
                        if(isValid){
                            $('#'+ vRefNo1).css('visibility','hidden');
                            $('#'+ vRefNo2).css('visibility','hidden');
                            $('#'+ vRefDate).css('visibility','hidden');
                        }
                       
                    } 
                    args.IsValid = isValid;                    
                    
                }

                function onContractDateChanged(sender, args) {
                    var contractDatePicker = $find('DatePickerContractDate');  
                    var contractDate = contractDatePicker.get_selectedDate();
                             
                    var isGen = false;    
                    var budgetYear = 0, year = 0, month;    
                    if(contractDate != null){
                        month = contractDate.getMonth() + 1; 
                        year = contractDate.getFullYear();
                        //budgetYear = (month > 9)? (year + 1) : year;
                    }
                    var txtId = '" + TextBoxContractNo.ClientID + @"';                    
                    if(year > 2016){
                        $('#'+ txtId).val('');     
                        $('#'+ txtId).attr('disabled', 'disabled');
                        $('#'+ txtId).attr('placeholder', '" + Nep.Project.Resources.UI.LabelGeneratebySystem + @"');                      
                    }else{
                        $('#'+ txtId).removeAttr('disabled');
                        $('#'+ txtId).removeAttr('placeholder');                       
                    } 
                }

                function validateContractNo(oSrc, args){
                    var isValid = false;
                    //var contractDatePicker = $find('DatePickerContractDate');  
                    //var contractDate = contractDatePicker.get_selectedDate();
                    //var budgetYear = 0, month;    
                    var year = "+ ApprovalYear +@";
                    //if(contractDate != null){
                    //    month = contractDate.getMonth() + 1; 
                    //    year = contractDate.getFullYear();
                    //    //budgetYear = (month > 9)? (year + 1) : year;
                    //}
                   
                    if((args.Value != '') || ((args.Value == '') && (year > 2016)) ){
                        isValid = true;
                    }
                     args.IsValid = isValid;  
                }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelContract,
                       this.GetType(),
                       "TabContractScript",
                       script,
                       true);
            
        }

        protected void CustomValidatorRefData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string refNo1 = TextBoxRefNo1.Text.Trim();
            DateTime? refNo2 = DatePickerRefNo2.SelectedDate;
            DateTime? refDate = DatePickerRefDate.SelectedDate;
            bool isValid = true;
            if(args.Value == ""){
                isValid = (refNo1 == "") && (!refNo2.HasValue) && (!refDate.HasValue);
            }
            args.IsValid = isValid;
        }
      
        protected void CustomValidatorProvinceRefData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string refNo1 = TextBoxProvinceNo1.Text.Trim();
            DateTime? refNo2 = DatePickerProvinceNo2.SelectedDate;
            DateTime? refDate = DatePickerProvinceDate.SelectedDate;
            bool isValid = true;
            if (args.Value == "")
            {
                isValid = (refNo1 == "") && (!refNo2.HasValue) && (!refDate.HasValue);
            }
            args.IsValid = isValid;
        }


        //private bool IsGenerateContractNo(DateTime? contractDate)
        //{
        //    bool isGen = false;
        //    if (contractDate != null)
        //    {
        //        DateTime date = (DateTime)contractDate;
        //        date = date.Date;

        //        //DateTime compareDate = new DateTime(date.Year, 9, 30, 0, 0, 0);
        //        //compareDate = compareDate.Date;               
        //        //int budgetYear = (date.CompareTo(compareDate) > 0) ? (date.Year + 1) : date.Year;

        //        isGen = (date.Year > 2016);
        //    }

        //    return isGen;
        //}

        private bool IsGenerateContractNo()
        {           
            int approvalYear = 0;
            Int32.TryParse(ApprovalYear, out approvalYear);

            return (approvalYear > 2016);
        }

        protected void CustomValidatorContractNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isGen = false;
            DateTime? tmpContractDate = DatePickerContractDate.SelectedDate;

            if (tmpContractDate != null)
            {
                DateTime contractDate = (DateTime)tmpContractDate;
                contractDate = contractDate.Date;

                DateTime compareDate = new DateTime(2016, 9, 30, 0, 0, 0);
                compareDate = compareDate.Date;

                isGen = (contractDate.CompareTo(compareDate) > 0);
            }

            args.IsValid = ((args.Value != "") || (isGen && (args.Value == "")));
           
        }
    }
}