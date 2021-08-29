using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using Nep.Project.Common;
using Nep.Project.DBModels.Model;
using Nep.Project.ServiceModels.ProjectInfo;
using System.Web.Mvc;

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
                            myDivUploadFileKTB.Visible = false;//hide div upload file ktb
                            ButtonUndoCancelContract.Visible = false;
                            if (UserInfo.IsAdministrator || UserInfo.IsCenterOfficer)
                            {
                                DBModels.Model.MT_ListOfValue status = _service.GetListOfValue(Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา, Common.LOVGroup.ProjectApprovalStatus);
                                var gen = _service.GetDB().ProjectGeneralInfoes.Where(w => w.ProjectID == model.ProjectID).FirstOrDefault();
                                if (gen != null && gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา && result.Data.LastApproveStatus.HasValue)
                                {
                                    ButtonUndoCancelContract.Visible = true;
                                }
                            }
                            if (UserInfo.IsAdministrator || UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)
                            {
                                var gen = _service.GetDB().ProjectGeneralInfoes.Where(w => w.ProjectID == model.ProjectID).FirstOrDefault();
                                if (gen != null && (gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอน_6_1_รอโอนเงิน || gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว) && result.Data.LastApproveStatus.HasValue)
                                {
                                    myDivUploadFileKTB.Visible = true; //open div upload file ktb
                                }
                                ButtonEditStartEndContractDate.Visible = functions.Contains(Common.ProjectFunction.PrintContract);
                            }
                            
                            
                            HyperLinkPrint.Visible = functions.Contains(Common.ProjectFunction.PrintContract);
                            ButtonCancelContract.Visible = functions.Contains(Common.ProjectFunction.CancelContract);
                            ButtonEditContractNo.Visible = functions.Contains(Common.ProjectFunction.PrintContract);
                            if (ButtonEditContractNo.Visible)
                            {
                                ButtonEditContractNo.OnClientClick = $"VueContract.EditContractNo({ProjectID});return false;";
                            }
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
                            StoerDue(0, model.Dues, DatePickerStartDue1, DatePickerEndDue1, TextBoxDueAmount1);
                            StoerDue(1, model.Dues, DatePickerStartDue2, DatePickerEndDue2, TextBoxDueAmount2);
                            StoerDue(2, model.Dues, DatePickerStartDue3, DatePickerEndDue3, TextBoxDueAmount3);
                            List<ServiceModels.KendoAttachment> SupportFiles = model.SupportAttachments;
                            if (model.ExtendData != null)
                            {
                                //TextBoxAttachPage1.Text = model.ExtendData.PageCount1.ToString();
                                //TextBoxAttachPage2.Text = model.ExtendData.PageCount2.ToString();
                                //TextBoxAttachPage3.Text = model.ExtendData.PageCount3.ToString();
                                //TextBoxAttachPage4.Text = model.ExtendData.PageCount4.ToString();
                                //TextBoxAttachPage5.Text = model.ExtendData.PageCount5.ToString();
                                //TextBoxAttachPage6.Text = model.ExtendData.PageCount6.ToString();
                                //DatePickerBook.SelectedDate = model.ExtendData.BookDate;
                                //DatePickerCommand.SelectedDate = model.ExtendData.CommandDate;
                                //TextBoxBookOrder.Text = model.ExtendData.BookOrder;
                                //TextBoxBooNo.Text = model.ExtendData.BookNo;
                                //TextBoxCommand.Text = model.ExtendData.Command;
                                //DatePickerMeeting.SelectedDate = model.ExtendData.MeetingDate;
                                //TextBoxMeetingOrder.Text = model.ExtendData.MeetingOrder.ToString();
                                TextBoxReferenceNo.Text = model.ExtendData.ReferenceNo;
                                TextBoxRefPositionLine2.Text= model.ExtendData.DirectorPositionLine2;
                                TextBoxRefPositionLine3.Text= model.ExtendData.DirectorPositionLine3;
                                if (model.ExtendData.AddressAt != null)
                                {
                                    var data = model.ExtendData.AddressAt;
                                    DdlProvince.Value = ((int)data.ProvinceId).ToString();
                                    DdlDistrict.Value = ((int)data.DistrictId).ToString();
                                    DdlSubDistrict.Value = ((int)data.SubDistrictId).ToString();
                                    TextBoxAddressNo.Text = data.AddressNo;
                                    TextBoxBuilding.Text = data.Building;
                                    TextBoxSoi.Text = data.Soi;
                                    TextBoxStreet.Text = data.Street;
                                    TextBoxMoo.Text = data.Moo;
                                    TextBoxPostCode.Text = data.ZipCode;
                                }

                                //Beer29082021
                                //LabelHistoryEditStartEndDate
                                if(model.ExtendData.ContractStartEndDateByName != null)
                                {
                                    divHistoryEditStartEndDate.Visible = true;
                                    LabelHistoryEditStartEndDate.Text = "วันที่เริ่มสัญญาเดิม: " + model.ExtendData.ContractStartDateOld.ToString("dd/MM/yyyy");
                                    LabelHistoryEditStartEndDate.Text+= " ,วันที่สิ้นสุดสัญญาเดิม : " + model.ExtendData.ContractEndDateOld.ToString("dd/MM/yyyy");
                                    LabelHistoryEditStartEndDate.Text += " ,แก้ไขโดย : " + model.ExtendData.ContractStartEndDateByName;
                                }
                            }

                            //end kenghot
                            FileUploadSupport.ClearChanges();
                            FileUploadSupport.ExistingFiles = SupportFiles;
                            FileUploadSupport.DataBind();

                            //Beer28082021
                            List<ServiceModels.KendoAttachment> SignedContractFiles = model.SignedContractAttachments;
                            C2XFileUploadSignedContract.ClearChanges();
                            C2XFileUploadSignedContract.ExistingFiles = SignedContractFiles;
                            C2XFileUploadSignedContract.DataBind();

                            List<ServiceModels.KendoAttachment> KTBFiles = model.KTBAttachments;
                            FileUploadKTB.ClearChanges();
                            FileUploadKTB.ExistingFiles = KTBFiles;
                            FileUploadKTB.DataBind();

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
                                ContractRefNoBlock.Visible = !isShowProvinceSupportGiven;
                                CustomValidatorRefNo1.Enabled = !isShowProvinceSupportGiven;
                                CustomValidatorRefNo2.Enabled = !isShowProvinceSupportGiven;
                                //RequiredFieldTextBoxRefNo1.Enabled = isShowProvinceSupportGiven;
                                //RequiredFieldRefNo2.Enabled = isShowProvinceSupportGiven;

                                ContractRefDateBlock.Visible = !isShowProvinceSupportGiven;
                                CustomValidatorRefDate.Enabled = !isShowProvinceSupportGiven;
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
                            //สัญญาวิจัย
                            if (obj.Data.ProjectTypeCode == LOVCode.Projecttype.การวิจัย_นวัตกรรม)
                            {
                                ContractProvinceNoBlock.Visible = false;
                                CustomValidatorProvinceNo1.Enabled = false;
                                CustomValidatorProvinceNo2.Enabled = false;
                                
                                ContractProvinceRefDateBlock.Visible = false;
                                CustomValidatorProvinceDate.Enabled = false;
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
                                    //C2XFileUploadAuthorizeDoc.Visible = true;
                                    attach.Add(model.AuthorizeDocAttachment);
                                }
                                C2XFileUploadAuthorizeDoc.ClearChanges();
                                C2XFileUploadAuthorizeDoc.ExistingFiles = attach;
                                C2XFileUploadAuthorizeDoc.DataBind();
                                //beer28082021
                                List<ServiceModels.KendoAttachment> AuthorizeDocMultis = model.AuthorizeDocAttachmentMulti;
                                if (model.AuthorizeDocID.HasValue)
                                {
                                    AuthorizeDocMultis.Add(model.AuthorizeDocAttachment);
                                }
                                C2XFileUploadAuthorizeDocMulti.ClearChanges();
                                C2XFileUploadAuthorizeDocMulti.ExistingFiles = AuthorizeDocMultis;
                                C2XFileUploadAuthorizeDocMulti.DataBind();

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
        private void StoerDue(int no, List<ContractDue> dues, Nep.Project.Web.UserControls.DatePicker start, Nep.Project.Web.UserControls.DatePicker end , Nep.Project.Web.UserControls.TextBox amt  )
        {
            start.SelectedDate = null;
            end.SelectedDate = null;
            amt.Text = "";
            if (no < dues.Count)
            {
                var due = dues[no];
                start.SelectedDate = due.StartDate;
                end.SelectedDate = due.EndDate;
                amt.Text = due.Amount.ToString();
            }
        }
        private void LoadDue( List<ContractDue> dues, Nep.Project.Web.UserControls.DatePicker start, Nep.Project.Web.UserControls.DatePicker end, Nep.Project.Web.UserControls.TextBox amt)
        {
            if (start.SelectedDate.HasValue && end.SelectedDate.HasValue && decimal.Parse(amt.Text) > 0)
            {
                var due = new ContractDue
                {
                    StartDate = start.SelectedDate,
                    EndDate = end.SelectedDate,
                    Amount = decimal.Parse(amt.Text)
                };
                dues.Add(due);
            }
             
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
            //Beer28082021 multi file
            addedFiles = C2XFileUploadAuthorizeDocMulti.AddedFiles;
            removedFiles = C2XFileUploadAuthorizeDocMulti.RemovedFiles;

            result.AddedAuthorizeDocAttachmentMulti = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            result.RemovedAuthorizeDocAttachmentMulti = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
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

            //Beer28082021
            addedFiles = C2XFileUploadSignedContract.AddedFiles;
            removedFiles = C2XFileUploadSignedContract.RemovedFiles;

            result.AddedSignedContractAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            result.RemovedSignedContractAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

            addedFiles = FileUploadKTB.AddedFiles;
            removedFiles = FileUploadKTB.RemovedFiles;

            result.AddedKTBAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            result.RemovedKTBAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

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
            if (result.ExtendData == null)
            {
                result.ExtendData = new ServiceModels.ProjectInfo.ContractExtend();
            }
            int i;
            //result.ExtendData.PageCount1 = int.TryParse(TextBoxAttachPage1.Text, out i) ? i : 0;
            //result.ExtendData.PageCount2 = int.TryParse(TextBoxAttachPage2.Text, out i) ? i : 0;
            //result.ExtendData.PageCount3 = int.TryParse(TextBoxAttachPage3.Text, out i) ? i : 0;
            //result.ExtendData.PageCount4 = int.TryParse(TextBoxAttachPage4.Text, out i) ? i : 0;
            //result.ExtendData.PageCount5 = int.TryParse(TextBoxAttachPage5.Text, out i) ? i : 0;
            //result.ExtendData.PageCount6 = int.TryParse(TextBoxAttachPage6.Text, out i) ? i : 0;

            //result.ExtendData.BookDate = DatePickerBook.SelectedDate.Value;
            //result.ExtendData.CommandDate = DatePickerCommand.SelectedDate.Value;
            //result.ExtendData.BookOrder = TextBoxBookOrder.Text;
            //result.ExtendData.BookNo = TextBoxBooNo.Text;
            //result.ExtendData.Command = TextBoxCommand.Text;
            //result.ExtendData.MeetingDate = DatePickerMeeting.SelectedDate.Value;
            //result.ExtendData.MeetingOrder = int.Parse(TextBoxMeetingOrder.Text);
            result.ExtendData.ReferenceNo = TextBoxReferenceNo.Text.Trim() ;
            //Beer28082021
            result.ExtendData.DirectorPositionLine2 = TextBoxRefPositionLine2.Text.Trim();
            result.ExtendData.DirectorPositionLine3 = TextBoxRefPositionLine3.Text.Trim();

            var ad = new ServiceModels.ProjectInfo.Address();
            result.ExtendData.AddressAt = ad;
            ad.AddressNo = TextBoxAddressNo.Text.Trim();
            ad.Building = TextBoxBuilding.Text.Trim();
            ad.Moo = TextBoxMoo.Text.Trim();
            ad.Soi = TextBoxSoi.Text.Trim();
            ad.Street = TextBoxStreet.Text.Trim();
            ad.ZipCode = TextBoxPostCode.Text.Trim();


            string provValue = DdlProvince.Value;
            int provID = 0;
            Int32.TryParse(provValue, out provID);

            string distValue = DdlDistrict.Value;
            int distID = 0;
            Int32.TryParse(distValue, out distID);

            string subDistValue = DdlSubDistrict.Value;
            int subDistID = 0;
            Int32.TryParse(subDistValue, out subDistID);
            ad.ProvinceId = provID;
            ad.DistrictId = distID;
            ad.SubDistrictId = subDistID;
            result.Dues = new List<ContractDue>();
            LoadDue(result.Dues, DatePickerStartDue1, DatePickerEndDue1, TextBoxDueAmount1);
            LoadDue(result.Dues, DatePickerStartDue2, DatePickerEndDue2, TextBoxDueAmount2);
            LoadDue(result.Dues, DatePickerStartDue3, DatePickerEndDue3, TextBoxDueAmount3);
            //ad = new ServiceModels.ProjectInfo.Address();
            //result.ExtendData.AddressAuth = ad;
            //ad.AddressNo = TextBoxAddressNo2.Text.Trim();
            //ad.Building = TextBoxBuilding2.Text.Trim();
            //ad.Moo = TextBoxMoo2.Text.Trim();
            //ad.Soi = TextBoxSoi2.Text.Trim();
            //ad.Street = TextBoxStreet2.Text.Trim();
            //ad.ZipCode = TextBoxPostCode2.Text.Trim();
            //provValue = DdlProvince2.Value;
            //provID = 0;
            //Int32.TryParse(provValue, out provID);d

            //distValue = DdlDistrict2.Value;
            //distID = 0;
            //Int32.TryParse(distValue, out distID);

            //subDistValue = DdlSubDistrict2.Value;
            //subDistID = 0;
            //Int32.TryParse(subDistValue, out subDistID);
            //ad.ProvinceId = provID;
            //ad.DistrictId = distID;
            //ad.SubDistrictId = subDistID;
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
        protected void ButtonUndoCancelContract_Click(object sender, EventArgs e)
        {
            var result = _service.UndoCancelProjectContract(ProjectID);
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
        protected void CustomRequiredFileKTB_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if(myDivUploadFileKTB.Visible == true && (FileUploadKTB !=null ) && (string.IsNullOrEmpty(args.Value)))
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
            var vue = System.IO.File.ReadAllText($"{Server.MapPath("\\html\\contract\\contractvue.js")}");
            script += vue + $"\nVueContract.refreshButton = '{ImageButtonRefresh.ClientID}'";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelContract,
                       this.GetType(),
                       "TabContractScript",
                       script,
                       true);
            RegisterProvince();
        }
        private void RegisterProvince()
        {
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Value)) ? DdlProvince.Value : "null";
            string districtSelected = (!String.IsNullOrEmpty(DdlDistrict.Value)) ? DdlDistrict.Value : "null";
            string subDistrictSelected = (!String.IsNullOrEmpty(DdlSubDistrict.Value)) ? DdlSubDistrict.Value : "null";
            //string provinceSelected2 = (!String.IsNullOrEmpty(DdlProvince2.Value)) ? DdlProvince2.Value : "null";
            //string districtSelected2 = (!String.IsNullOrEmpty(DdlDistrict2.Value)) ? DdlDistrict2.Value : "null";
            //string subDistrictSelected2 = (!String.IsNullOrEmpty(DdlSubDistrict2.Value)) ? DdlSubDistrict2.Value : "null";
            String script = @"
                $(function () {                 
                    
                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable: true" + @",
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetProvince',
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict.ClientID + @"', '" + DdlSubDistrict.ClientID + @"',e);},
                        Value: " + provinceSelected + @",                     
                     });  

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlDistrict.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlProvince.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetDistrict',
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict.ClientID + @"',e);},
                        Value: " + districtSelected + @",
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince.ClientID + @"');},
                     });    

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlSubDistrict.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlDistrict.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                        Value: " + subDistrictSelected + @",     
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict.ClientID + @"');},           
                     });                   

                });

                function showOrgRegisterDate() {
                    var orgYearPicker = $find('DatePickerRegisterYear');
                    if (orgYearPicker != null) {
                        stopShowOrgDateInterval();
                        var orgSelectedDate = orgYearPicker.get_selectedDate();
                        var orgYear = kendo.toString(orgSelectedDate, 'yyyy');
                        orgYear = parseInt(orgYear, 10);

                        var currentYear = kendo.toString(kendo.parseDate(new Date()), 'yyyy');
                        currentYear = parseInt(currentYear, 10) - 1;
                        if (orgYear >= currentYear) {
                            $('.org-register-date').each(function (item) {
                                $(this).css('visibility', 'visible');
                            });

                        } else {
                            $('.org-register-date').each(function (item) {
                                $(this).css('visibility', 'hidden');
                            });
                        }
                    }            
                }

                function stopShowOrgDateInterval() {
                    clearInterval(showOrgDateInterval);
                }

                var showOrgDateInterval = setInterval(function(){ showOrgRegisterDate() }, 1000);
                
                

            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelContract,
                       this.GetType(),
                       "RegisterProvince",
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
        protected void CustomValidatorProvince_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlProvince.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorDistrict_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlDistrict.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorSubDistrict_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlSubDistrict.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }
        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            //GridViewActivity.DataSource = GridViewActivityData;
            //GridViewActivity.DataBind();
            Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
            page.RebindData("TabPanelContract");
        }

        protected void ButtonEditStartEndContractDate_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.TabContract model = new ServiceModels.ProjectInfo.TabContract();
            try
            {
                if (Page.IsValid)
                {
                    var resultGetData = _service.GetProjectContractByProjectID(ProjectID);
                    if (resultGetData.IsCompleted)
                    {
                        model = resultGetData.Data;
                        //Beer29082021 update
                        DateTime startDate = Convert.ToDateTime(DatePickerContractStartDate.SelectedDate);
                        DateTime endDate = Convert.ToDateTime(DatePickerContractEndDate.SelectedDate);
                        model.ProjectID = ProjectID;
                        model.ContractStartDate = startDate;
                        model.ContractEndDate = endDate;

                        var result = _service.UpdateProjectContractStartEndDate(model);
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
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Contract", ex);
                ShowErrorMessage(ex.Message);
            }
        }
    }
}