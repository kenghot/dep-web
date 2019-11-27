using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class FollowupPrintingPopup : Infra.BasePage
    {
        public IServices.IProjectInfoService _projectService { get; set; }
        public IServices.IOrganizationParameterService _paramService { get; set; }
        public IServices.IListOfValueService _lovService { get; set; }

        #region Properties
        protected Decimal? ProjectID
        {
            get
            {
                decimal? projectid = (decimal?)null;
                if(ViewState["ProjectID"] == null){
                    string tmpProjectID = Request.QueryString["projectid"];
                    decimal id = 0;
                    if (Decimal.TryParse(tmpProjectID, out id))
                    {
                        ViewState["ProjectID"] = id;                        
                    }                    
                }

                projectid = Convert.ToDecimal(ViewState["ProjectID"]);
                return projectid;
            }

            set
            {
                ViewState["ProjectID"] = value;
            }
        }

        protected Decimal? ProjectReportTrackingID
        {
            get
            {
                decimal? id = (decimal?)null;
                if (ViewState["ProjectReportTrackingID"] != null)
                {
                    id = Convert.ToDecimal(ViewState["ProjectReportTrackingID"]);
                }
                return id;
            }

            set
            {
                ViewState["ProjectReportTrackingID"] = value;
            }
        }

        protected Decimal OrgReportTrackingTypeID
        {
            get
            {
                if (ViewState["OrgReportTrackingTypeID"] == null)
                {
                    var result = _lovService.GetListOfValueByCode(Common.LOVGroup.ReportTrackingType, Common.LOVCode.Reporttrackingtype.หนังสือติดตามถึงองค์กร);
                    if (result.IsCompleted)
                    {
                        ViewState["OrgReportTrackingTypeID"] = result.Data.LovID;
                    }
                }

                return Convert.ToDecimal(ViewState["OrgReportTrackingTypeID"]);
            }

            set
            {
                ViewState["OrgReportTrackingTypeID"] = value;
            }
        }

        protected String PrefixDocNo0702
        {
            get
            {
                string prefix = "";
                if (ViewState["PrefixDocNo0702"] == null)
                {
                    var paramResult = _paramService.GetOrganizationParameter();
                    if (paramResult.IsCompleted)
                    {
                        ViewState["PrefixDocNo0702"] = paramResult.Data.PrefixDocNo0702;
                    }
                    else
                    {
                        ShowErrorMessage(paramResult.Message[0]);
                    }

                }
                prefix = ViewState["PrefixDocNo0702"].ToString();

                return prefix;
            }

            set
            {
                ViewState["PrefixDocNo0702"] = value;
            }
        }

        protected Boolean IsShowPrintingOpt
        {
            get
            {
                bool isShow = false;
                if (ViewState["IsShowPrintingOpt"] != null)
                {
                    isShow = Convert.ToBoolean(ViewState["IsShowPrintingOpt"]);
                }
                return isShow;
            }

            set
            {
                ViewState["IsShowPrintingOpt"] = value;
            }
        }

        protected Boolean CanPrinting
        {
            get
            {
                bool isTrue = true;
                if (ViewState["CanPrinting"] != null)
                {
                    isTrue = Convert.ToBoolean(ViewState["CanPrinting"]);
                }
                return isTrue;
            }

            set
            {
                ViewState["CanPrinting"] = value;
            }
        }

        protected DateTime? FirstDeadlineDate
        {
            get
            {
                DateTime? date = (DateTime?)null;
                if (ViewState["FirstDeadlineDate"] != null)
                {
                    date = Convert.ToDateTime(ViewState["FirstDeadlineDate"]);
                }
                return date;
            }

            set
            {
                ViewState["FirstDeadlineDate"] = value;
            }
        }

        protected Boolean IsCreateFirstTime
        {
            get
            {
                bool isTrue = false;
                if (ViewState["IsCreateFirstTime"] != null)
                {
                    isTrue = Convert.ToBoolean(ViewState["IsCreateFirstTime"]);
                }
                return isTrue;
            }

            set
            {
                ViewState["IsCreateFirstTime"] = value;
            }
        }

        public String ReportTrackingViewAttachmentPrefix
        {
            get
            {
                if (ViewState["ReportTrackingViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["ReportTrackingViewAttachmentPrefix"] = prefix;
                }
                return ViewState["ReportTrackingViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ReportTrackingViewAttachmentPrefix"] = value;
            }
        }
        #endregion Properties

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
                string tmpProjectID = Request.QueryString["projectid"];
                string tmpTrackingID = Request.QueryString["trackingid"];
                string tmpIsShowOpt = Request.QueryString["isshowopt"];
                string tmpCanPrinting = Request.QueryString["canprinting"];

                decimal projectID = 0;
                decimal reportTrackingID = 0;
                bool isShowOpt = false;
                bool canPrinting = true;

                Decimal.TryParse(tmpProjectID, out projectID);
                Decimal.TryParse(tmpTrackingID, out reportTrackingID);
                Boolean.TryParse(tmpIsShowOpt, out isShowOpt);
                Boolean.TryParse(tmpCanPrinting, out canPrinting);
                CanPrinting = canPrinting;

                ProjectID = (projectID > 0) ? (decimal?)projectID : (decimal?)null;
                ProjectReportTrackingID = (reportTrackingID > 0) ? (decimal?)reportTrackingID : (decimal?)null;
                IsShowPrintingOpt = isShowOpt;

                
                DisplayContent(OrgReportTrackingTypeID);
            }
        }

        

        protected void DropDownListPrintOption_TextChanged(object sender, EventArgs e)
        {
            decimal trackingTypeID = Convert.ToDecimal(DropDownListPrintOption.SelectedValue);
            ServiceModels.ProjectInfo.FollowupTrackingDocumentForm firstItem = null;
            var isCreateNewResult = _projectService.IsReportTrackingCreateNew((decimal)ProjectID, trackingTypeID);
            if (isCreateNewResult.IsCompleted)
            {
                firstItem = isCreateNewResult.Data;
                if (firstItem != null)
                {
                    FirstDeadlineDate = firstItem.DeadlineResponseDate;
                    IsCreateFirstTime = (firstItem.ReportTrackingTypeID != trackingTypeID);
                }
                else
                {
                    IsCreateFirstTime = true;
                }
            }
            else
            {
                ShowErrorMessage(isCreateNewResult.Message[0]);
            }
            DisplayFirstTimeForm();
        }

        private void DisplayFirstTimeForm()
        {
            

            if (!IsCreateFirstTime)
            {
                LabelDeadlineDate.Visible = false;
                DivDeadlineDate.Visible = false;
                RequiredFieldValidatorDeadlineDate.Enabled = false;
                ContainerOrgRefInfo1.Visible = false;
                CustomValidatorReportDate.Enabled = false;
            }
            else
            {
                LabelDeadlineDate.Visible = true;
                DivDeadlineDate.Visible = true;
                RequiredFieldValidatorDeadlineDate.Enabled = true;
                ContainerOrgRefInfo1.Visible = true;
                CustomValidatorReportDate.Enabled = true;
            }
           
        }

        private void BindDropDownListPrintOption(decimal? selectedID)
        {
            var printOptionResult = _lovService.ListActive(Common.LOVGroup.ReportTrackingType, selectedID);
            if (printOptionResult.IsCompleted)
            {
                DropDownListPrintOption.DataSource = printOptionResult.Data;
                DropDownListPrintOption.DataBind();

                if(selectedID.HasValue){
                    DropDownListPrintOption.SelectedValue = selectedID.ToString();
                }
                else
                {
                    DropDownListPrintOption.SelectedIndex = 0;
                }
                
            }
            else
            {
                ShowErrorMessage(printOptionResult.Message);
            }
        }

        private void DisplayContent(decimal reportTrackingTypeID)
        {
            ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model = new ServiceModels.ProjectInfo.FollowupTrackingDocumentForm();
            decimal projectID = (ProjectID.HasValue) ? (decimal)ProjectID : 0;
            bool isShowOpt = IsShowPrintingOpt;
            decimal reportTrackingID = (ProjectReportTrackingID.HasValue) ? (decimal)ProjectReportTrackingID : 0;
            decimal? selectedPrintOptionID = null;
            if (projectID > 0)
            {
                if (!isShowOpt)
                {
                    PrintOptionContainer.Visible = false;
                }
               
                ContainerOrgRefInfo1.Visible = (reportTrackingTypeID == OrgReportTrackingTypeID)? true : false;                
                               

                #region Check Create New             
                
                
                #endregion Check Create New


                if (reportTrackingID > 0)
                {
                    DropDownListPrintOption.Enabled = false;
                    var trackingResult = _projectService.GetFollowupTrackingDocumentForm(reportTrackingID);
                    if (trackingResult.IsCompleted)
                    {
                        ServiceModels.ProjectInfo.FollowupTrackingDocumentForm trackingForm = trackingResult.Data;

                        IsCreateFirstTime = trackingForm.IsCreateFirstTime;

                        selectedPrintOptionID = trackingForm.ReportTrackingTypeID;


                        DatePickerSendDate.SelectedDate = trackingForm.ReportDate;
                        DatePickerDeadlineDate.SelectedDate = trackingForm.DeadlineResponseDate;

                        TextBoxReportNo.Text = trackingForm.ReportNo;
                        TextBoxRefInfo1.Text = trackingForm.ReferenceInfo1;
                        TextBoxRefInfo.Text = trackingForm.ReferenceInfo;

                        ButtonPrint.Visible = (CanPrinting && trackingForm.IsEditable);
                        ButtonPrintWord.Visible = (CanPrinting && trackingForm.IsEditable);

                        ButtonDelete.Visible = (CanPrinting && trackingForm.IsEditable);

                    }
                    else
                    {
                        ShowErrorMessage(trackingResult.Message);
                    }
                }
                else
                {
                    
                    selectedPrintOptionID = reportTrackingTypeID;
                    ServiceModels.ProjectInfo.FollowupTrackingDocumentForm firstItem = null;
                    var isCreateNewResult = _projectService.IsReportTrackingCreateNew((decimal)ProjectID, reportTrackingTypeID);
                    if (isCreateNewResult.IsCompleted)
                    {
                        firstItem = isCreateNewResult.Data;
                        if (firstItem != null)
                        {
                            FirstDeadlineDate = firstItem.DeadlineResponseDate;
                            IsCreateFirstTime = (firstItem.ReportTrackingTypeID != reportTrackingTypeID);
                        }
                        else
                        {
                            IsCreateFirstTime = true;
                        }
                    }
                    else
                    {
                        ShowErrorMessage(isCreateNewResult.Message[0]);
                    }
                         
                }

                DisplayFirstTimeForm();

            }
            else
            {
                UpdatePanelFolloupPrePrintingForm.Visible = false;
                ShowErrorMessage(Nep.Project.Resources.Message.NoRecord);
            }

            BindDropDownListPrintOption(selectedPrintOptionID);
        }

        private ServiceModels.ProjectInfo.FollowupTrackingDocumentForm GetData()
        {
            ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model = new ServiceModels.ProjectInfo.FollowupTrackingDocumentForm();
            decimal reportTrackingTypeID = Convert.ToDecimal(DropDownListPrintOption.SelectedValue);

            model.ProjectID = (ProjectID.HasValue)? (decimal)ProjectID : 0;
            model.ReportTrackingID = ProjectReportTrackingID;

            model.ReportDate = DatePickerSendDate.SelectedDate;

            model.ReportTrackingTypeID = reportTrackingTypeID;
            model.ReportNo = TextBoxReportNo.Text.Trim();
            model.ReferenceInfo = TextBoxRefInfo.Text.TrimEnd();

            
            if (IsCreateFirstTime)
            {
                model.DeadlineResponseDate = DatePickerDeadlineDate.SelectedDate;
                model.ReferenceInfo1 = (reportTrackingTypeID == OrgReportTrackingTypeID) ? TextBoxRefInfo1.Text.TrimEnd() : null;
            }
            else
            {
                model.DeadlineResponseDate = FirstDeadlineDate;
            }
            model.IsFirstTracking = IsCreateFirstTime;
                       

            return model;
        }

        private ServiceModels.KendoAttachment CreateOrgLetter(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model, String printType)
        {
            ServiceModels.KendoAttachment file = new ServiceModels.KendoAttachment();
            var letterContextResult = _projectService.GetReportOrgTrackingContext(model);
            if (letterContextResult.IsCompleted)
            {
                // Create Report DataSource
                List<ServiceModels.Report.ReportOrgTracking> dataSet = new List<ServiceModels.Report.ReportOrgTracking>();               
                dataSet.Add(letterContextResult.Data);

                ReportDataSource dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                dataset1.Value = dataSet;

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = "utf-8";
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportOrgTracking.rdlc"));
                viewer.LocalReport.DataSources.Add(dataset1); // Add datasource here

                string format = (printType == "PDF") ? "PDF" : "WORDOPENXML";
                string fileExtension = (printType == "PDF") ? ".pdf" : ".doc";
                byte[] bytes = viewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                var fileName = Guid.NewGuid().ToString("N");
                var filePath = HttpContext.Current.Server.MapPath(Common.Constants.UPLOAD_TEMP_PATH + fileName);

                // Create a file stream and write the report to it
                FileStream stream = File.Create(filePath, bytes.Length);
                
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();


                file.name = String.Format("หนังสือติดตามถึงองค์กร({0})" + fileExtension, GetPostFixFileName());
                file.tempId = fileName;
                file.extension = fileExtension;
                file.size = bytes.Length;
                
            }
            else
            {
                ShowErrorMessage(letterContextResult.Message);
            }

            return file;
        }

        private ServiceModels.KendoAttachment CreateProvinceLetter(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model, string printType)
        {
            ServiceModels.KendoAttachment file = new ServiceModels.KendoAttachment();
            var letterContextResult = _projectService.GetReportProvinceTrackingContext(model);
            if (letterContextResult.IsCompleted)
            {
                // Create Report DataSource
                List<ServiceModels.Report.ReportProvinceTracking> dataSet = new List<ServiceModels.Report.ReportProvinceTracking>();
                dataSet.Add(letterContextResult.Data);

                ReportDataSource dataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportProvinceTracking");
                dataset.Value = dataSet;

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = "utf-8";
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportProvinceTracking.rdlc"));
                viewer.LocalReport.DataSources.Add(dataset); // Add datasource here

                string format = (printType == "PDF") ? "PDF" : "WORDOPENXML";
                string fileExtension = (printType == "PDF") ? ".pdf" : ".doc";
                byte[] bytes = viewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                var fileName = Guid.NewGuid().ToString("N");
                var filePath = HttpContext.Current.Server.MapPath(Common.Constants.UPLOAD_TEMP_PATH + fileName);

                // Create a file stream and write the report to it
                FileStream stream = File.Create(filePath, bytes.Length);

                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                //String fileName = String

                file.name = String.Format("หนังสือติดตามถึงจังหวัด({0})"+fileExtension, GetPostFixFileName()); 
                file.tempId = fileName;
                file.extension = fileExtension;
                file.size = bytes.Length;

            }
            else
            {
                ShowErrorMessage(letterContextResult.Message);
            }

            return file;
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            Print("PDF");                                  
        }

        protected void ButtonPrintWord_Click(object sender, EventArgs e)
        {
            Print("WORDOPENXML");   
        }

        private void Print(string printType)
        {
            if (Page.IsValid)
            {
                ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model = GetData();
                ServiceModels.KendoAttachment letterFile = (model.ReportTrackingTypeID == OrgReportTrackingTypeID) ? CreateOrgLetter(model, printType) : CreateProvinceLetter(model, printType);
                model.LetterAttchment = letterFile;

                var saveResult = _projectService.SaveFollowupTrackingDocumentForm(model);
                if (saveResult.IsCompleted)
                {
                    model = saveResult.Data;
                    ButtonDelete.Visible = true;
                    DropDownListPrintOption.Enabled = false;
                    ProjectReportTrackingID = model.ReportTrackingID;
                    var url = ResolveUrl("~/AttachmentHandler/View/Project/" + model.ProjectID + "/" + model.LetterAttchmentID + "/" + model.LetterAttchment.name);

                    String scriptTag = @"
                    $(function () {
                        var openTrackingLetterUrl = '" + url + @"'
                        window.open(openTrackingLetterUrl, '_blank');
                        window.parent.reloadGridViewPrintedDocument('updated');

                        c2x.closeFormDialog();  
                    });";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openTrackingLetter", scriptTag, true);
                }
                else
                {
                    ShowErrorMessage(saveResult.Message);
                }
            }  
        }

        private String GetPostFixFileName()
        {
            String text = DateTime.Today.ToString("yyyyMMdd");
            int year = DateTime.Today.Year;
            int buddhaYear = year + 543;
            text = text.Replace(year.ToString(), buddhaYear.ToString());
            return text;
        }

        protected void CustomValidatorReportDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int diff = 0;
            DateTime? startDate = DatePickerSendDate.SelectedDate;
            DateTime? endDate = DatePickerDeadlineDate.SelectedDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                diff = ((DateTime)endDate).Subtract((DateTime)startDate).Days + 1;
                args.IsValid = (diff > 0);
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (ProjectReportTrackingID.HasValue)
            {
                var result = _projectService.DeleteFollowupTrackingDocumentForm((decimal)ProjectReportTrackingID);
                if(result.IsCompleted){
                    String scriptTag = @"
                    $(function () {
                         
                        window.parent.reloadGridViewPrintedDocument('deleted');
                        c2x.closeFormDialog();  
                    });";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "reloadGridView", scriptTag, true);
                }
            }            
        }
    }
}