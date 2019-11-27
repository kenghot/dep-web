using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using Nep.Project.Common;
using System.IO;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabProcessedControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProviceService _provinceService { get; set; }
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

        public Decimal OrgProjectProvinceID
        {
            get
            {
                decimal id = 0;
                if (ViewState["OrgProjectProvinceID"] != null)
                {
                    id = Convert.ToDecimal(ViewState["OrgProjectProvinceID"]);
                }
                return id;
            }

            set
            {
                ViewState["OrgProjectProvinceID"] = value;
            }
        }

        public List<String> RequiredSubmitData
        {
            get
            {
                List<string> dataName = null;
                if (ViewState["RequiredSubmitData"] != null)
                {
                    dataName = (List<string>)ViewState["RequiredSubmitData"];
                }

                return dataName;
            }

            set
            {
                ViewState["RequiredSubmitData"] = value;
            }
        }

        public String ProcessingViewAttachmentPrefix
        {
            get
            {
                if (ViewState["ProcessingViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["ProcessingViewAttachmentPrefix"] = prefix;
                }
                return ViewState["ProcessingViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ProcessingViewAttachmentPrefix"] = value;
            }
        }

        public Boolean IsEditable
        {
            get {
                bool isTrue = false;
                if (ViewState["IsEditable"] != null)
                {
                    isTrue = (Boolean)ViewState["IsEditable"];
                }

                return isTrue;
            }
            set { ViewState["IsEditable"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string compare = "TabProcessingPlan_ProcessingPlanControl";
            string target = Request["__EVENTTARGET"];
            if ((IsPostBack) && (!String.IsNullOrEmpty(target) && (target.Contains(compare))))
            {
                //RegisterRequiredData();
            }
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            RegisterClientScript();
        }


        public void BindData()
        {
            try
            {

                decimal projectID = ProjectID;
              //  ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

                if (projectID > 0)
                {
                    HiddenProjectID.Value = projectID.ToString();

                    var result = _service.GetProjectOperationByProjectID(projectID);
                    if (result.IsCompleted)
                    {
                        ServiceModels.ProjectInfo.TabProcessingPlan model = result.Data;                        
                        DisplayDataProcessingPlan(model);
                       
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

          
        }

 
        //public List<ServiceModels.GenericDropDownListData> GetProvince()
        //{
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    try
        //    {
        //        string filter = OrgProjectProvinceName;
                
        //        var result = _provinceService.ListProvince(filter);
        //        if (result.IsCompleted)
        //        {
        //            list = result.Data;                    
        //        }
                    
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "ProcessingPlan", ex);
        //        ShowErrorMessage(ex.Message);
        //    }

        //    return list;
        //}

        protected void ProcessingPlanDate_TextChanged(object sender, EventArgs e)
        {
            DateTime? startDate = ProcessingPlanStartDate.SelectedDate;
            DateTime? endDate = ProcessingPlanEndDate.SelectedDate;
            double diffDay = 0;

            if(startDate.HasValue && endDate.HasValue)
            {
                DateTime startSelectedDate = (DateTime)startDate;
                DateTime endSelectedDate = (DateTime)endDate;

                diffDay = (endSelectedDate - startSelectedDate).TotalDays + 1;
            }

            TextBoxTotalPeriod.Text = diffDay.ToString();
        }

        //protected void ComboBoxProvince_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxProvince.SelectedIndex;
        //        string value = ComboBoxProvince.SelectedValue;
        //        int provinceID = 0;
        //        bool tryParseId = Int32.TryParse(value, out provinceID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            var result = _provinceService.ListDistrict(provinceID, string.Empty);
        //            if (result.IsCompleted)
        //            {
        //                ComboBoxDistrict.Enabled = true;
        //                ComboBoxDistrict.DataSource = result.Data;
        //                ComboBoxDistrict.DataBind();

        //                ComboBoxSubDistrict.SelectedValue = "";
        //                ComboBoxSubDistrict.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            ComboBoxDistrict.SelectedIndex = -1;
        //            ComboBoxDistrict.Enabled = false;

        //            ComboBoxSubDistrict.SelectedIndex = -1;
        //            ComboBoxSubDistrict.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "ProcessingPlan", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxDistrict_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxDistrict.SelectedIndex;
        //        string value = ComboBoxDistrict.SelectedValue;
        //        int districtID = 0;
        //        bool tryParseId = Int32.TryParse(value, out districtID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            ComboBoxDistrict.Enabled = true;
        //            var result = _provinceService.ListSubDistrict(districtID, string.Empty);
        //            if (result.IsCompleted)
        //            {

        //                ComboBoxSubDistrict.Enabled = true;
        //                ComboBoxSubDistrict.DataSource = result.Data;
        //                ComboBoxSubDistrict.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            ComboBoxSubDistrict.SelectedIndex = -1;
        //            ComboBoxSubDistrict.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "ProcessingPlan", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.TabProcessingPlan model = new ServiceModels.ProjectInfo.TabProcessingPlan();
            try
            {
                if(Page.IsValid){
                   // model = MapControlToTabProcessingPlan();
                     
                    String tmpAddress = HiddOperationAddress.Value;
                    if (tmpAddress != "")
                    {
                        List<ServiceModels.ProjectInfo.ProjectProcessed> list =
                               Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.ProjectProcessed>>(tmpAddress);
                        var result = _service.SaveProjectProcessed(ProjectID, list,Request.UserHostAddress);
                        if (result.IsCompleted)
                        {
                            // RequiredSubmitData = result.Data.RequiredSubmitData;
                            //HiddOperationAddress.Value = (result.Data.ProjectOperationAddresses != null) ? Nep.Project.Common.Web.WebUtility.ToJSON(result.Data.ProjectOperationAddresses) : "";
                            Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                            page.RebindData("TabPanelProcessed");
                            ShowResultMessage(result.Message);
                        }
                        else
                        {
                            ShowErrorMessage(result.Message);
                        }

                    }else
                    {
                        ShowErrorMessage("ไม่พบข้อมูล");
                    }

                //    var result = _service.SaveProjectProcessed(ProjectID,li);
             
                }
                
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "ProjectProcessed", ex);
                ShowErrorMessage(ex.Message);

                //RegisterRequiredData();
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal projectId = 0;
                decimal.TryParse(HiddenProjectID.Value, out projectId);
                if (projectId > 0)
                {
                    var result = _service.DeleteProject(projectId);
                    if (result.IsCompleted)
                        Response.Redirect(Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoList.aspx?isDeleteSuccess=true"));
                    else
                        ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ButtonSendProjectInfo_Click(object sender, EventArgs e)
        {
            
            decimal projectID = ProjectID;
            if (projectID > 0)
            {
                //var result = _service.ValidateSubmitData(projectID);
                //if (result.IsCompleted)
                //{
                    var sendDataToReviewResult = _service.SendDataToReview(projectID,Request.UserHostAddress);
                    if (sendDataToReviewResult.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabProcessingPlan");
                        string message = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Message.SubmitDataToReviewSuccess : Message.SendDataToReviewSuccess;
                        ShowResultMessage(message);
                    }
                    else
                    {
                        ShowErrorMessage(sendDataToReviewResult.Message[0]);
                    }
                //}
                //else
                //{
                //    ShowErrorMessage(result.Message);
                //}
            }
        }

        private ServiceModels.ProjectInfo.TabProcessingPlan MapControlToTabProcessingPlan()
        {
            ServiceModels.ProjectInfo.TabProcessingPlan result = new ServiceModels.ProjectInfo.TabProcessingPlan();
            decimal projectId = Convert.ToDecimal(HiddenProjectID.Value);
            //string address = TextBoxAddress.Text.Trim();
            //string moo = TextBoxMoo.Text.Trim();
            //string building = TextBoxBuilding.Text.Trim();
            //string soi = TextBoxSoi.Text.Trim();
            //string road = TextBoxRoad.Text.Trim();

            //decimal subdistrictId = 0;
            //Decimal.TryParse(ComboBoxSubDistrict.SelectedValue,out subdistrictId);
            //string strSubDistrict = ComboBoxSubDistrict.SelectedItem.Text;

            //decimal districtId = 0;
            //Decimal.TryParse(ComboBoxDistrict.SelectedValue, out districtId);
            //string strDistrict = ComboBoxDistrict.SelectedItem.Text;

            //decimal provinceId = 0;
            //Decimal.TryParse(ComboBoxProvince.SelectedValue, out provinceId);

            DateTime startDate = Convert.ToDateTime(ProcessingPlanStartDate.SelectedDate);
            DateTime endDate = Convert.ToDateTime(ProcessingPlanEndDate.SelectedDate);
            decimal totalDay = 0;
            decimal.TryParse(TextBoxTotalPeriod.Text.Trim(),out totalDay);

            string timeDesc = TextBoxTimeDesc.Text.TrimEnd();
            string method = TextBoxMethod.Text.TrimEnd();

            result.ProjectID = projectId;
            //result.Address = address;
            //result.Moo = moo;
            //result.Building = building;
            //result.Soi = soi;
            //result.Road = road;
            //result.SubDistrictID = (decimal?)null;//subdistrictId;
            //result.SubDistrict = ""; //strSubDistrict;
            //result.DistrictID = (decimal?)null;//districtId;
            //result.District = ""; // strDistrict;
            //result.ProvinceID = (decimal?)null;//provinceId;
            result.StartDate = startDate;
            result.EndDate = endDate;
            result.TotalDay = totalDay;
            result.TimeDesc = timeDesc;
            result.Method = method;

            //Attachment
            //IEnumerable<ServiceModels.KendoAttachment> addedFiles = FileUploadProcessingPlanMap.AddedFiles;
            //IEnumerable<ServiceModels.KendoAttachment> removedFiles = FileUploadProcessingPlanMap.RemovedFiles;
            //result.AddedLocationMapAttachment = (addedFiles.Count() > 0)? addedFiles.First() : null;
            //result.RemovedLocationMapAttachment = (removedFiles.Count() > 0) ? addedFiles.First() : null;

            //ProjectOperationAddresses
            String tmpAddress = HiddOperationAddress.Value;
            if (tmpAddress != "")
            {
                List<ServiceModels.ProjectInfo.ProjectOperationAddress> list =
                       Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.ProjectOperationAddress>>(tmpAddress);
                result.ProjectOperationAddresses = list;
            }

            return result;
        }
               

        private void DisplayDataProcessingPlan(ServiceModels.ProjectInfo.TabProcessingPlan model)
        {
            if(model != null){
                OrgProjectProvinceID = model.OrgProjectProvinceID;

                bool canSendProjectInfo = (model.RequiredSubmitData == null);
                RequiredSubmitData = model.RequiredSubmitData;

                //Check Function
                List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
                //kenghot
                //bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData));
                var master = (this.Page.Master as MasterPages.SiteMaster);
                bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData) || functions.Contains(Common.ProjectFunction.SaveDraftReportResult)
                    || master.UserInfo.IsAdministrator);

                //end kenghot
                IsEditable = isEditable;
                ButtonAddPlanAddress.Enabled = isEditable;
                ButtonSave.Visible = isEditable;
                //ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                //ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                //HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                //AddPlanAddress.Visible = isEditable;               
                //ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);
                               

                //List<ServiceModels.KendoAttachment> attach = new List<KendoAttachment>();
                //if (model.LocationMapID.HasValue)
                //{
                //    attach.Add(model.LocationMapAttachment);
                //}
                //FileUploadProcessingPlanMap.ClearChanges();
                //FileUploadProcessingPlanMap.ExistingFiles = attach;
                //FileUploadProcessingPlanMap.DataBind();
                
                ProcessingPlanStartDate.SelectedDate = model.StartDate;
                ProcessingPlanEndDate.SelectedDate = model.EndDate;
                TextBoxTotalPeriod.Text = (model.TotalDay.HasValue)? model.TotalDay.ToString() : "";
                TextBoxTimeDesc.Text = model.TimeDesc;
                TextBoxMethod.Text = model.Method;

                // Address
                var proc = _service.MapProjectProcessed(model.ProjectID);
                if (proc.Count >= 0)
                {
                    // HiddOperationAddress.Value = Nep.Project.Common.Web.WebUtility.ToJSON(proc);
                    HiddOperationAddress.Value = Newtonsoft.Json.JsonConvert.SerializeObject(proc);
                }
         
                
            }              
            
        }

        protected void CustomValidatorPlanDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ProcessingPlanStartDate.SelectedDate.HasValue && ProcessingPlanEndDate.SelectedDate.HasValue)
            {
                int diff = GetDiffStartAndEndProcessingPlanate();
                args.IsValid = (diff > 0);
            }            
        }

        protected void CustomValidatorTotalPeriod_ServerValidate(object source, ServerValidateEventArgs args)
        {
            decimal totalPeriod = 0;
            CustomValidator validator = (CustomValidator)source;
            string textPeriod = TextBoxTotalPeriod.Text;
            Decimal.TryParse(textPeriod, out totalPeriod);

            DateTime? startDate = ProcessingPlanStartDate.SelectedDate;
            DateTime? endDate = ProcessingPlanEndDate.SelectedDate;

            String msg;

            int diffDate = GetDiffStartAndEndProcessingPlanate();
            if((diffDate > 0) && (totalPeriod > 0)){
                args.IsValid = (totalPeriod <= diffDate);
                msg = String.Format(Nep.Project.Resources.Error.LessThanOREqual, Model.ProcessingPlan_TotalPeriod, UI.LabelValidateProcessingTotalPeriod);
                validator.ErrorMessage = msg;
                validator.Text = msg;
            }
            else if (totalPeriod == 0 && (startDate != null) && (endDate != null))
            {
                args.IsValid = false;
                msg = String.Format(Nep.Project.Resources.Error.OverThan, Model.ProcessingPlan_TotalPeriod, "0");
                validator.ErrorMessage = msg;
                validator.Text = msg;
            }
        }

        private int GetDiffStartAndEndProcessingPlanate()
        {
            int diff = 0;
            DateTime? startDate = ProcessingPlanStartDate.SelectedDate;
            DateTime? endDate = ProcessingPlanEndDate.SelectedDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                diff = ((DateTime)endDate).Subtract((DateTime)startDate).Days + 1;
            }

            return diff;
        }

        private void RegisterClientScript()
        {
            String popupUrl = ResolveUrl("~/ProjectInfo/ProcessedPopup");
            String scriptUrl = ResolveUrl("~/Scripts/manage.processed.js?v=" + DateTime.Now.Ticks.ToString());
            //String scriptUrl = ResolveUrl("~/Scripts/manage.processingplan.js?v=" + DateTime.Now.Ticks.ToString());
            var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelProcessingPlan,
                       this.GetType(),
                       "RefUpdatePanelProcessingPlanScript",
                       refScript,
                       false);

            String participantScript = @" 
            $(function () {                   
                    
                    c2xProc.config({
                        ViewAttachmentUrl : " + Nep.Project.Common.Web.WebUtility.ToJSON(ProcessingViewAttachmentPrefix) + @",
                        HiddOperationAddressID: '" + HiddOperationAddress.ClientID + @"',
                        GridOperationAddressID: '" + OperationAddressGrid.ClientID + @"',
                        PopupUrl: '" + popupUrl + @"',
                        AddressFormTitle: '" + Nep.Project.Resources.Model.ProcessingPlan_Location + @"',

                        AddressLabel: '" + Nep.Project.Resources.Model.ProcessingPlan_Address + @"',
                        MooLabel: '" + Nep.Project.Resources.Model.ProjectInfo_Moo + @"',
                        BuildingLabel: '" + Nep.Project.Resources.Model.ProjectInfo_Building + @"',
                        SoiLabel: '" + Nep.Project.Resources.Model.ProjectInfo_Soi + @"',
                        RoadLabel: '" + Nep.Project.Resources.Model.ProjectInfo_Road + @"',

                        MapLabel: '" + Nep.Project.Resources.Model.ProcessingPlan_Map + @"',

                        ColumnTitle:{
                                     Address: '" + Nep.Project.Resources.UI.LabelAddress + @"', SubDistrictID:'" + Nep.Project.Resources.Model.ProjectInfo_SubDistrict + @"',
                                     DistrictID: '" + Nep.Project.Resources.Model.ProjectInfo_District + @"', ProvinceID:'" + Nep.Project.Resources.Model.ProjectInfo_Province + @"',
                                     LocationMapID: '" + Nep.Project.Resources.Model.ProcessingPlan_Map + @"',
                                    },
                        IsView: " + Nep.Project.Common.Web.WebUtility.ToJSON(!IsEditable) + @",
                        ProjectID: " + ProjectID + @"
                        });

                    c2xProc.createOperationAddressGrid();
                });

             function updateOperationAddress(model){
                var obj = c2x.clone(model);
                c2xProc.updateAddress(obj);
                            
             }

            function getCurrentEditItem(){
                return c2xProc.getEditItem();
            }

            function validatorOperationAddress(sender, args) {
               var address = $('#" + HiddOperationAddress.ClientID +@"').val();
               args.IsValid = (address != '');
           }
           
    ";

            ScriptManager.RegisterStartupScript(
                      UpdatePanelProcessingPlan,
                      this.GetType(),
                      "UpdatePanelProcessingPlanScript",
                      participantScript,
                      true);

            String script = @"
                function onProcessingPlanDateSelectionChanged(sender, args) {
                    var planStartDatePicker = $find('ProcessingPlanStartDate');
                    var planEndDatePicker = $find('ProcessingPlanEndDate');

                    var planStartDate = planStartDatePicker.get_selectedDate();
                    

                    var planEndDate = planEndDatePicker.get_selectedDate();  
                    

                   
                    var diff = 0;                   
                    if ((planStartDate != null) && (planEndDate != null)) {
                        planStartDate = new Date(planStartDate.getFullYear(), planStartDate.getMonth(), planStartDate.getDate(),0,0,0,0);
                        planEndDate = new Date(planEndDate.getFullYear(), planEndDate.getMonth(), planEndDate.getDate(),0,0,0,0);

                        diff = Math.floor((planEndDate - planStartDate) / 86400000);
                        diff = diff + 1;
                    }

                    $('.total-processing-period').val((diff > 0) ? diff : '');
                
                }

                
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelProcessingPlan,
                       this.GetType(),
                       "ProcessingPlanDateSelectionChangedScript",
                       script,
                       true);

            RegisterRequiredData();

        }

        private void RegisterRequiredData()
        {
            
            String onloadScript = @"
                $(function () {      
                SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                });";
            ScriptManager.RegisterStartupScript(
                        UpdatePanelProcessingPlan,
                        this.GetType(),
                        "UpdatePanelProcessingPlancript",
                        onloadScript,
                        true);
                     
        }

        protected void CustomValidatorOperationAddress_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string address = HiddOperationAddress.Value;
            args.IsValid = (address != "");
        }

    }
}