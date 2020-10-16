using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using System.Web.ModelBinding;
using Nep.Project.Common.Web;
using System.ComponentModel;

namespace Nep.Project.Web.ProjectInfo
{

    public partial class ProjectInfoList : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IListOfValueService LovService { get; set; }
        public IServices.IOrganizationParameterService OrgParamService { get; set; }
        public IServices.IProviceService ProvinceService { get; set; }
        public IServices.IProjectInfoService ProjectService { get; set; }
        public IServices.IOrganizationService _orgService { get; set; }
        #region Properties
        public Boolean IsDeleteRole
        {
            get
            {
                bool isRole = false;
                if (ViewState["IsDeleteRole"] != null)
                {
                    isRole = Convert.ToBoolean(ViewState["IsDeleteRole"]);
                }
                return isRole;
            }

            set { ViewState["IsDeleteRole"] = value; }
        }

        public Boolean IsCenterOfficer
        {
            get
            {
                bool isCenter = false;
                if (ViewState["IsCenterOfficer"] != null)
                {
                    isCenter = Convert.ToBoolean(ViewState["IsCenterOfficer"]);
                }
                return isCenter;
            }
            set { ViewState["IsCenterOfficer"] = value; }
        }

        public Boolean IsProvinceOfficer
        {
            get
            {
                bool isCompany = false;
                if (ViewState["IsProvinceOfficer"] != null)
                {
                    isCompany = Convert.ToBoolean(ViewState["IsProvinceOfficer"]);
                }
                return isCompany;
            }
            set { ViewState["IsProvinceOfficer"] = value; }
        }

        public String CenterProvinceAbbr
        {
            get
            {
                string abbr = "";
                if (ViewState["CenterProvinceAbbr"] != null)
                {
                    abbr = ViewState["CenterProvinceAbbr"].ToString();
                }
                return abbr;
            }
            set { ViewState["CenterProvinceAbbr"] = value; }
        }

        public Decimal DisabilityALLTypeID
        {
            get
            {
                decimal id = 0;
                if (ViewState["DisabilityALLTypeID"] != null)
                {
                    var result = LovService.GetListOfValueByCode(Common.LOVGroup.DisabilityType, Common.LOVCode.Disabilitytype.ทุกประเภทความพิการ);
                    if (result.IsCompleted)
                    {
                        id = result.Data.LovID;
                        ViewState["DisabilityALLTypeID"] = id;
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }

                return id;
            }
            set { ViewState["DisabilityALLTypeID"] = value; }
        }
        #endregion Properties
        private DBModels.Model.MT_ListOfValue reportedLOV;
        protected void Page_Init(object sender, EventArgs e)
        {
            ServiceModels.Security.SecurityInfo userInfo = UserInfo;
            IsProvinceOfficer = userInfo.IsProvinceOfficer;
            IsCenterOfficer = userInfo.IsCenterOfficer;
            IsDeleteRole = (userInfo.Roles.Contains(Common.FunctionCode.MANAGE_PROJECT) || userInfo.Roles.Contains(Common.FunctionCode.REQUEST_PROJECT));

            CenterProvinceAbbr = OrgParamService.GetOrganizationParameter().Data.CenterProvinceAbbr;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //NepHelper.WriteLog("list");
            //Business.SetFollowupStatusJobService f = new Business.SetFollowupStatusJobService(ProjectService.GetDB(), null, null);
            //SetFollowupStatus();
            lblCannotAdd.Visible = false;
            if (!Page.IsPostBack)
            {
                bool isDeleteSuccess;
                bool isRejectSuccess;
                if (Boolean.TryParse(Request.QueryString["isDeleteSuccess"], out isDeleteSuccess))
                {
                    ShowResultMessage(Message.DeleteSuccess);
                }
                else if (Boolean.TryParse(Request.QueryString["rejectsuccess"], out isRejectSuccess))
                {
                    ShowResultMessage(Message.RejectSuccess);
                }
                else if (Boolean.TryParse(Request.QueryString["sendreporttoreview"], out isRejectSuccess))
                {
                    ShowResultMessage("ส่งแก้ไขข้อมูลผลปฎิบัติงานเรียบร้อย");
                }
                GridProjectInfo.Sort("SubmitedDate", SortDirection.Descending);
                GridProjectInfo.Sort("CreatedDate", SortDirection.Descending);

                bool isCompany = (IsProvinceOfficer || IsCenterOfficer) ? false : true;
                bool isProvince = IsProvinceOfficer;
                string gridViewCss = GridProjectInfo.CssClass;
                if (isCompany)
                {
                    FormGroupOrgName.Visible = false;
                    FormGroupOrgType.Visible = false;
                    //FormGroupProvince.Visible = false;
                    gridViewCss += " view-data-org";
                }
                else if (isProvince)
                {
                    FormGroupProvince.Visible = false;
                    gridViewCss += " view-data-province";
                }
                if (base.UserInfo.IsAdministrator || base.UserInfo.IsCenterOfficer)
                {
                    btnRefreshDashBoard.Visible = true;
                }
                GridProjectInfo.CssClass = gridViewCss;
          
                PrepareData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Text)) ? DdlProvince.Text : "null";
            String script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownAll + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetOrgProvince()) + @", TotalRow:0, IsCompleted:true},                   
                        Value: " + provinceSelected + @",                     
                     });  
                });";

            ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "ManageComboboxScript",
                     script,
                     true);
        }

        public void ButtonSearch_Click(object sender, EventArgs e)
        {
            List<ServiceModels.IFilterDescriptor> filters = CreateFilter(false);
            this.GridProjectInfo.FilterDescriptors = filters;
            this.GridProjectInfo.DataBind();

            UpdateTotalIsFollowup();
        }

        private List<ServiceModels.IFilterDescriptor> CreateFilter(bool isFilterFollowup)
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            decimal valueId;
            //ชื่อองค์กร
            if (!String.IsNullOrEmpty(TextBoxContractOrgName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxContractOrgName.Text.Trim()
                });
            }

            //ประเภทองค์กร
            if (DropDownListOrgType.SelectedIndex > 0)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationToBeUnder",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = DropDownListOrgType.SelectedItem.Value,
                });
            }

            //ทะเบียนโครงการ
            if (!String.IsNullOrEmpty(TextBoxProjectNo.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectNo",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectNo.Text.Trim()
                });
            }

            //ชื่อโครงการ
            if (!String.IsNullOrEmpty(TextBoxProjectName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectName.Text.Trim()
                });
            }

            //ประเภทโครงการ
            if (DropDownListProjectInfoType.SelectedIndex > 0)
            {
                Decimal.TryParse(DropDownListProjectInfoType.SelectedItem.Value, out valueId);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectTypeID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }

            //ปีงบประมาณ
            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                int approvalYear = ((DateTime)DatePickerBudgetYear.SelectedDate).Year;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = approvalYear
                });
            }

            //จังหวัด
            if (DdlProvince.Text != "")
            {
                Decimal.TryParse(DdlProvince.Text, out valueId);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }

            //วันที่ส่งคำร้อง
            if (DatePickerSubmitedDateStart.SelectedDate.HasValue || DatePickerSubmitedDateEnd.SelectedDate.HasValue)
            {
                DateTime endDate; DateTime endDateTemp;
                if (DatePickerSubmitedDateStart.SelectedDate.HasValue && DatePickerSubmitedDateEnd.SelectedDate.HasValue)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "SubmitedDate",
                        Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                        Value = DatePickerSubmitedDateStart.SelectedDate
                    });

                    endDateTemp = (DateTime)DatePickerSubmitedDateEnd.SelectedDate;
                    endDate = new DateTime(endDateTemp.Year, endDateTemp.Month, endDateTemp.Day, 23, 59, 59, 999);
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "SubmitedDate",
                        Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                        Value = endDate
                    });
                }
                else if (DatePickerSubmitedDateStart.SelectedDate.HasValue)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "SubmitedDate",
                        Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                        Value = DatePickerSubmitedDateStart.SelectedDate
                    });
                }
                else if (DatePickerSubmitedDateEnd.SelectedDate.HasValue)
                {
                    endDateTemp = (DateTime)DatePickerSubmitedDateEnd.SelectedDate;
                    endDate = new DateTime(endDateTemp.Year, endDateTemp.Month, endDateTemp.Day, 23, 59, 59, 999);
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "SubmitedDate",
                        Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                        Value = endDate
                    });
                }
            }
            // kenghot
            //วันหมดอายุ
            if (DatePickerEndDateStart.SelectedDate.HasValue || DatePickerEndDateEnd.SelectedDate.HasValue)
            {
                DateTime endDate; DateTime endDateTemp;
                if (DatePickerEndDateStart.SelectedDate.HasValue && DatePickerEndDateEnd.SelectedDate.HasValue)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectEndDate",
                        Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                        Value = DatePickerEndDateStart.SelectedDate
                    });

                    endDateTemp = (DateTime)DatePickerEndDateEnd.SelectedDate;
                    endDate = new DateTime(endDateTemp.Year, endDateTemp.Month, endDateTemp.Day, 23, 59, 59, 999);
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectEndDate",
                        Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                        Value = endDate
                    });
                }
                else if (DatePickerEndDateStart.SelectedDate.HasValue)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectEndDate",
                        Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                        Value = DatePickerEndDateStart.SelectedDate
                    });
                }
                else if (DatePickerEndDateEnd.SelectedDate.HasValue)
                {
                    endDateTemp = (DateTime)DatePickerEndDateEnd.SelectedDate;
                    endDate = new DateTime(endDateTemp.Year, endDateTemp.Month, endDateTemp.Day, 23, 59, 59, 999);
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectEndDate",
                        Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                        Value = endDate
                    });
                }
            }
            //end kenghot

            //ติดตาม
            if (isFilterFollowup || IsAlertFollowup.Checked)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "IsFollowup",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = true
                });
            }

            //ประเภทความพิการ
            List<ServiceModels.IFilterDescriptor> projDisabilityTypeFields = new List<ServiceModels.IFilterDescriptor>();
            if (CheckBoxListTypeDisabilitys.SelectedIndex >= 0)
            {
                ListItemCollection items = CheckBoxListTypeDisabilitys.Items;
                foreach (ListItem item in CheckBoxListTypeDisabilitys.Items)
                {
                    if (item.Selected)
                    {
                        Decimal.TryParse(item.Value, out valueId);

                        projDisabilityTypeFields.Add(new ServiceModels.FilterDescriptor()
                        {
                            Field = "DisabilityTypeID",
                            Operator = ServiceModels.FilterOperator.IsEqualTo,
                            Value = valueId
                        });
                    }
                }
            }

            List<ServiceModels.IFilterDescriptor> standardStrategicsFields = new List<ServiceModels.IFilterDescriptor>();
            //ยุทธศาสตร์ที่สอดคล้องกับโครงการ
            if (CheckBoxListStandardStrategics.SelectedIndex >= 0)
            {
                foreach (ListItem item in CheckBoxListStandardStrategics.Items)
                {
                    if (item.Selected == true)
                    {
                        standardStrategicsFields.Add(new ServiceModels.FilterDescriptor()
                        {
                            Field = CheckBoxListStandardStrategics.Text,
                            Operator = ServiceModels.FilterOperator.IsEqualTo,
                            Value = true
                        });
                    }
                }

            }

            //ไม่อนุมัติ
            if (CheckBoxNotApprovalStatus.Checked)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ApprovalStatus",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = "0"
                });
            }

            List<ServiceModels.IFilterDescriptor> projAppStatusFields = new List<ServiceModels.IFilterDescriptor>();
            //ยกเลิกสัญญา
            if (CheckBoxCancelContractStatus.Checked)
            {
                Decimal.TryParse(CheckBoxCancelContractStatus.Attributes["Value"], out valueId);
                projAppStatusFields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectApprovalStatusID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }

            //ยกเลิกคำร้อง
            if (CheckBoxCancelledProjectRequest.Checked)
            {
                Decimal.TryParse(CheckBoxCancelledProjectRequest.Attributes["Value"], out valueId);
                projAppStatusFields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectApprovalStatusID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }
            if (CheckConsiderCancel.Checked)
            {
                Decimal.TryParse(CheckBoxCancelledProjectRequest.Attributes["Value"], out valueId);

                List<ServiceModels.IFilterDescriptor> f = new List<ServiceModels.IFilterDescriptor>();
                f.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "IsStep2Approved",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = true
                });
                f.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "IsStep3Approved",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = false
                });
                f.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectApprovalStatusID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                }
                    );
                projAppStatusFields.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = f
                }
                );
            }
            if (CheckApproveCancel.Checked)
            {
                Decimal.TryParse(CheckBoxCancelledProjectRequest.Attributes["Value"], out valueId);

                List<ServiceModels.IFilterDescriptor> f = new List<ServiceModels.IFilterDescriptor>();
                f.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "IsStep3Approved",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = true
                });
                f.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectApprovalStatusID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                }
                    );
                projAppStatusFields.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = f
                }
                );
            }
            //ขั้นตอนการอนุมัติ
            if (CheckBoxListApprovalProcess.SelectedIndex >= 0)
            {
                foreach (ListItem item in CheckBoxListApprovalProcess.Items)
                {
                    if (item.Selected == true)
                    {
                        Decimal.TryParse(item.Value, out valueId);
                        projAppStatusFields.Add(new ServiceModels.FilterDescriptor()
                        {
                            Field = "ProjectApprovalStatusID",
                            Operator = ServiceModels.FilterOperator.IsEqualTo,
                            Value = valueId
                        });
                    }
                }
            }

            //filter by user login 
            List<ServiceModels.IFilterDescriptor> productFilters = new List<ServiceModels.IFilterDescriptor>();
            if ((!IsProvinceOfficer) && (!IsCenterOfficer))
            {
                #region Organization
                decimal userOrgId = (UserInfo.OrganizationID.HasValue) ? (decimal)UserInfo.OrganizationID : 0;
                List<ServiceModels.IFilterDescriptor> productOrgFilter = new List<ServiceModels.IFilterDescriptor>();
                List<ServiceModels.IFilterDescriptor> productOrgCreateByOfficerFilter = new List<ServiceModels.IFilterDescriptor>();

                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = userOrgId
                });

                productOrgFilter.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "CreatorOrganizationID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = UserInfo.OrganizationID
                });

                //------ Officer Created ----
                productOrgCreateByOfficerFilter.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "IsCreateByOfficer",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = true
                });

                productOrgCreateByOfficerFilter.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectApprovalStatusCode",
                    Operator = ServiceModels.FilterOperator.IsNotEqualTo,
                    Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                });

                //-----------------
                productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = productOrgFilter
                });
                productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = productOrgCreateByOfficerFilter
                });

                #endregion Organization
            }
            else if (IsProvinceOfficer)
            {
                #region IsProvinceOfficer
                decimal provinceID = (UserInfo.ProvinceID.HasValue) ? (decimal)UserInfo.ProvinceID : 0;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = provinceID
                });

                if (projAppStatusFields.Count == 0)
                {
                    List<ServiceModels.IFilterDescriptor> productDraftCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.IFilterDescriptor> productCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productDraftCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();

                    List<ServiceModels.GroupCompositeFilterDescriptor> productCanViewFilters = new List<ServiceModels.GroupCompositeFilterDescriptor>();

                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });
                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "IsCreateByOfficer",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = true
                    });
                    productCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsNotEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });


                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productDraftCanViewFilter
                    });
                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productCanViewFilter
                    });
                }
                #endregion IsProvinceOfficer
            }
            else if (UserInfo.IsAdministrator)
            {
                #region IsAdministrator
                if (projAppStatusFields.Count == 0)
                {
                    decimal provinceID = (UserInfo.ProvinceID.HasValue) ? (decimal)UserInfo.ProvinceID : 0;
                    List<ServiceModels.IFilterDescriptor> productDraftCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.IFilterDescriptor> productCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productDraftCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();

                    List<ServiceModels.GroupCompositeFilterDescriptor> productCanViewFilters = new List<ServiceModels.GroupCompositeFilterDescriptor>();

                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });
                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "IsCreateByOfficer",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = true
                    });

                    productCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsNotEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });

                    List<ServiceModels.CompositeFilterDescriptor> productComposit = new List<ServiceModels.CompositeFilterDescriptor>();
                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productDraftCanViewFilter
                    });
                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productCanViewFilter
                    });

                }
                #endregion IsAdministrator
            }
            else if (IsCenterOfficer)
            {
                #region IsCenterOfficer
                if (projAppStatusFields.Count == 0)
                {
                    decimal provinceID = (UserInfo.ProvinceID.HasValue) ? (decimal)UserInfo.ProvinceID : 0;
                    List<ServiceModels.IFilterDescriptor> productDraftCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.IFilterDescriptor> productCanViewFilter = new List<ServiceModels.IFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productDraftCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();
                    List<ServiceModels.CompositeFilterDescriptor> productCanViewComposite = new List<ServiceModels.CompositeFilterDescriptor>();

                    List<ServiceModels.GroupCompositeFilterDescriptor> productCanViewFilters = new List<ServiceModels.GroupCompositeFilterDescriptor>();

                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });
                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "IsCreateByOfficer",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = true
                    });
                    productDraftCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProvinceID",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = provinceID
                    });

                    productCanViewFilter.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProjectApprovalStatusCode",
                        Operator = ServiceModels.FilterOperator.IsNotEqualTo,
                        Value = Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร
                    });

                    List<ServiceModels.CompositeFilterDescriptor> productComposit = new List<ServiceModels.CompositeFilterDescriptor>();
                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productDraftCanViewFilter
                    });
                    productFilters.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = productCanViewFilter
                    });

                }
                #endregion IsCenterOfficer
            }

            // Create CompositeFilterDescriptor
            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }

            if (projAppStatusFields.Count > 0)
            {

                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = projAppStatusFields

                });
            }

            if (standardStrategicsFields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = standardStrategicsFields
                });
            }

            if (projDisabilityTypeFields.Count > 0)
            {

                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = projDisabilityTypeFields
                });
            }

            if (productFilters.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = productFilters
                });
            }

            List<ServiceModels.IFilterDescriptor> filters = null;
            if (filterComposite.Count > 0)
            {
                filters = new List<ServiceModels.IFilterDescriptor>();

                filters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = filterComposite
                });
            }



            return filters;

        }
        private List<ServiceModels.IFilterDescriptor> CreateFilterForCannotAdd(decimal? userOrgId, decimal? provinceId)
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();




            fields.Add(new ServiceModels.FilterDescriptor()
            {
                Field = "IsFollowup",
                Operator = ServiceModels.FilterOperator.IsEqualTo,
                Value = true
            });
            if (userOrgId.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = userOrgId
                });

            }
            if (provinceId.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = provinceId.Value
                });

            }


            List<ServiceModels.IFilterDescriptor> filters = new List<ServiceModels.IFilterDescriptor>();

            filters = new List<ServiceModels.IFilterDescriptor>();

            filters.Add(new ServiceModels.CompositeFilterDescriptor()
            {
                LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                FilterDescriptors = fields
            });




            return filters;

        }
        public void ButtonClear_Click(object sender, EventArgs e)
        {
            TextBoxContractOrgName.Text = String.Empty;
            DropDownListOrgType.ClearSelection();
            TextBoxProjectNo.Text = String.Empty;
            TextBoxProjectName.Text = String.Empty;
            DropDownListProjectInfoType.ClearSelection();
            DatePickerBudgetYear.Clear();
            DdlProvince.Text = "";
            IsAlertFollowup.Checked = false;
            CheckBoxCancelContractStatus.Checked = false;
            CheckBoxNotApprovalStatus.Checked = false;
            CheckBoxCancelledProjectRequest.Checked = false;

            CheckBoxListTypeDisabilitys.ClearSelection();
            CheckBoxListStandardStrategics.ClearSelection();
            CheckBoxListApprovalProcess.ClearSelection();

            DatePickerSubmitedDateStart.Clear();
            DatePickerSubmitedDateEnd.Clear();

            //kenghot
            DatePickerEndDateStart.Clear();
            DatePickerEndDateEnd.Clear();
            //end kenghot

            /*
            UpdateTotalIsFollowup();

            GridProjectInfo.FilterDescriptors = null;
            GridProjectInfo.PageIndex = 0;
            GridProjectInfo.DataBind();
             * */
        }

        public void LinkButtonExpandAdvanceSearch_Click(object sender, EventArgs e)
        {
            LinkButtonExpandAdvanceSearch.Visible = false;
            LinkButtonCollapseAdvanceSearch.Visible = true;

            ColumnLeft.Attributes["class"] = "col-sm-8";
            ColumnRight.Visible = true;
            AdvanceSerchBlock2.Visible = true;

            GridProjectInfo.DataBind();

        }

        public void LinkButtonCollapseAdvanceSearch_Click(object sender, EventArgs e)
        {
            LinkButtonCollapseAdvanceSearch.Visible = false;
            LinkButtonExpandAdvanceSearch.Visible = true;

            ColumnLeft.Attributes["class"] = "col-sm-12";
            ColumnRight.Visible = false;
            AdvanceSerchBlock2.Visible = false;
            GridProjectInfo.DataBind();
        }

        #region Grid Project Info
        public List<ServiceModels.ProjectInfo.ProjectInfoList> GridProjectInfo_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            reportedLOV = ProjectService.GetListOfValue(Common.LOVCode.Followupstatus.รายงานผลแล้ว, Common.LOVGroup.FollowupStatus);
            var result = this.ProjectService.ListProjectInfoList(GridProjectInfo.QueryParameter, false);
            List<ServiceModels.ProjectInfo.ProjectInfoList> data = new List<ServiceModels.ProjectInfo.ProjectInfoList>();
            totalRowCount = 0;
            if (result.IsCompleted)
            {
                data = result.Data;
                foreach (ServiceModels.ProjectInfo.ProjectInfoList r in data)
                {
                    if ((r.IsFollowup.HasValue) && (r.IsFollowup == false))
                    {
                        var rep = (from rp in ProjectService.GetDB().ProjectReports where rp.ProjectID == r.ProjectInfoID select rp).FirstOrDefault();
                        if (rep != null && !string.IsNullOrEmpty(rep.REVISECOMMENT))
                        {
                            r.IsReportRevise = true;
                        }
                    }
                }
                totalRowCount = result.TotalRow;
                GridProjectInfo.TotalRows = result.TotalRow;
                if (result.TotalRow == 0)
                {
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
            }
            else
            {
                ShowErrorMessage(result.Message[0]);
            }

            return data;
        }

        protected void GridProjectInfo_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                int colSpan = 13;
                e.Row.Cells[0].ColumnSpan = colSpan;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ServiceModels.ProjectInfo.ProjectInfoList r = (ServiceModels.ProjectInfo.ProjectInfoList)e.Row.DataItem;
                Image img = (Image)e.Row.FindControl("imgApprovalStatus");
                Image imgReported = (Image)e.Row.FindControl("imgReported");
                Button btnAcknowledged = (Button)e.Row.FindControl("ButtonAcknowledged");
                if (img != null)
                {
                    if (r.ApprovalStatus1 != null)
                    {

                        img.Visible = true;
                        img.ImageUrl = string.Format("~/Images/Approval/ApprovalStatus1_{0}.png", r.ApprovalStatus1.LOVCode.Trim());
                        img.AlternateText = r.ApprovalStatus1.LOVName;
                        img.ToolTip = r.ApprovalStatus1.LOVName;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(r.RejectComment) && r.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ)
                        {
                            img.Visible = true;
                            img.ImageUrl = "~/Images/Approval/revise.jpg";
                            img.AlternateText = "ถูกแก้ไขและส่งกลับมาเข้าระบบใหม่";
                            img.ToolTip = "ถูกแก้ไขและส่งกลับมาเข้าระบบใหม่";
                        }
                    }

                }
                if (imgReported != null)
                {
                    imgReported.Visible = r.FollowupStatusID.HasValue && r.FollowupStatusID.Value == reportedLOV.LOVID;
                }
                if (btnAcknowledged != null && UserInfo.IsAdministrator)
                {
                    btnAcknowledged.Visible = r.Acknowledged != "1";
                }
                //kenghot18
                var hist = ProjectService.GetProjectHistoryList(r.ProjectInfoID);
                if (hist.IsCompleted && hist.Data.Count() > 0)
                {
                    getHistDay(hist.Data, 2, (Label)e.Row.FindControl("LabelDay2"));
                    getHistDay(hist.Data, 3, (Label)e.Row.FindControl("LabelDay3"));
                    getHistDay(hist.Data, 4, (Label)e.Row.FindControl("LabelDay4"));
                }
                //if ((r.IsFollowup.HasValue) && (r.IsFollowup == false))
                //{
                //    var rep = (from rp in ProjectService.GetDB().ProjectReports where rp.ProjectID == r.ProjectInfoID select rp ).FirstOrDefault();
                //     if (rep != null && !string.IsNullOrEmpty(rep.REVISECOMMENT))
                //     {
                //        r.ProjectNameDesc += String.Format("<span class='alert-folloup-desc'> ({0}) </span>", "ส่งแก้ไขผลปฎิบัติงาน");
                //    }
                //}

            }
        }
        private void getHistDay(List<ServiceModels.ProjectInfo.ProjectHistory> h, int iNo, Label lb)
        {
            lb.Visible = false;
            var cur = h.Where(w => w.History.HISTORYTYPE == iNo.ToString()).FirstOrDefault();
            var prev = h.Where(w => w.History.HISTORYTYPE == (iNo - 1).ToString()).FirstOrDefault();

            var dayText = "";
            if (cur != null && prev != null)
            {
                dayText = string.Format("{0:#0}วัน", Math.Floor((cur.History.ENTRYDT.Value - prev.History.ENTRYDT.Value).TotalDays));
                lb.ToolTip = string.Format("จำนวนวันตั้งแต่ {0:dd/MM/yyyy} ถึง {1:dd/MM/yyyy}: {2} วัน",
                    prev.History.ENTRYDT, cur.History.ENTRYDT, dayText);
                lb.Text = dayText;

                lb.Visible = true;
            }


        }
        #endregion Grid Project Info

        #region Private Method

        private void PrepareData()
        {
            List<string> errors = new List<string>();

            #region Bind data to ประเภทโครงการ
            List<ServiceModels.ListOfValue> projectTypeList = new List<ServiceModels.ListOfValue>();
            var projectTypeListResult = this.LovService.ListAll(Common.LOVGroup.ProjectType);
            if (projectTypeListResult.IsCompleted)
            {
                projectTypeList = projectTypeListResult.Data;
                projectTypeList.Insert(0, new ServiceModels.ListOfValue() { LovID = -1, LovName = UI.DropdownAll });
                DropDownListProjectInfoType.DataSource = projectTypeList;
                DropDownListProjectInfoType.DataBind();
            }
            else
            {
                errors.Add(projectTypeListResult.Message[0]);
            }
            #endregion Bind data to ประเภทโครงการ

            #region Bind data to ประเภทความพิการ
            var disabilityTypeListResult = this.LovService.ListAll(Common.LOVGroup.DisabilityType);
            if (disabilityTypeListResult.IsCompleted)
            {
                CheckBoxListTypeDisabilitys.DataSource = disabilityTypeListResult.Data;
                CheckBoxListTypeDisabilitys.DataBind();
            }
            else
            {
                errors.Add(disabilityTypeListResult.Message[0]);
            }
            #endregion Bind data to ประเภทความพิการ

            #region Bind data to ยุทธศาสตร์ที่สอดคล้องกับโครงการ
            BindChcekBoxStandardStrategics();
            #endregion Bind data to ยุทธศาสตร์ที่สอดคล้องกับโครงการ

            #region Bind data to ขั้นตอนการอนุมัติ
            List<ServiceModels.ListOfValue> approvalProcessList = new List<ServiceModels.ListOfValue>();
            var approvalProcessResult = this.LovService.ListAll(Common.LOVGroup.ProjectApprovalStatus);
            if (approvalProcessResult.IsCompleted)
            {
                approvalProcessList = approvalProcessResult.Data;
                ServiceModels.ListOfValue approvalStatusCancel = approvalProcessList.Where(x => x.LovCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา).FirstOrDefault();
                ServiceModels.ListOfValue cancelledProjectRequestStatus = approvalProcessList.Where(x => x.LovCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกคำร้อง).FirstOrDefault();

                approvalProcessList = approvalProcessList.Where(x =>
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ) ||
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน) ||
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง) ||
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน) ||
                    (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)).ToList();


                CheckBoxListApprovalProcess.DataSource = approvalProcessList;
                CheckBoxListApprovalProcess.DataBind();
                CheckBoxCancelContractStatus.Attributes.Add("Value", approvalStatusCancel.LovID.ToString());
                CheckBoxCancelledProjectRequest.Attributes.Add("Value", cancelledProjectRequestStatus.LovID.ToString());


                for (int i = 0; i < approvalProcessList.Count; i++)
                {
                    ViewState["ApprovalNameStep" + (i + 1)] = approvalProcessList[i].LovName;
                }

                ViewState["NotApprovalName"] = Nep.Project.Resources.UI.LabelNotApprovalStatus;
            }
            else
            {
                errors.Add(approvalProcessResult.Message[0]);
            }
            #endregion Bind data to ขั้นตอนการอนุมัติ

            //#region Bind data to จังหวัด
            //ComboBoxProvince.DataSource = GetProvinceData();
            //ComboBoxProvince.DataBind();
            //ComboBoxProvince.SelectedIndex = 0;
            //#endregion Bind data to จังหวัด

            UpdateTotalIsFollowup();

            List<ServiceModels.IFilterDescriptor> filters = CreateFilter(false);
            GridProjectInfo.FilterDescriptors = filters;

            //Create Button
            if (UserInfo.Roles.Contains(Common.FunctionCode.REQUEST_PROJECT) ||
                UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_PROJECT))
            {
                ButtonAdd.Visible = true;
            }

            if (errors.Count > 0)
            {
                ShowErrorMessage(errors);
            }
        }

        private void UpdateTotalIsFollowup()
        {
            List<ServiceModels.IFilterDescriptor> filters = CreateFilter(true);

            ServiceModels.QueryParameter p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filters, 0, Common.Constants.PAGE_SIZE, null, null);
            var result = ProjectService.GetTotalIsFollowup(p);
            string totalIsFollowup = "0";
            if (result.IsCompleted)
            {
                totalIsFollowup = Nep.Project.Common.Web.WebUtility.DisplayInHtml(result.Data, "N0", "0");
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            LabelTotalIsFollowup.Text = String.Format(Nep.Project.Resources.Message.TotalFollowup, totalIsFollowup);
        }

        public List<ServiceModels.GenericDropDownListData> ComboBoxBudgetYear_GetData()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = this.ProjectService.ApprovalYear();
            if (result.IsCompleted)
            {
                list = result.Data;
                //list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownAll, Value = "" });
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        public List<ServiceModels.GenericDropDownListData> GetProvinceData()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = this.ProjectService.GetListProjectProvince();
            if (result.IsCompleted)
            {
                list = result.Data;
                list.Insert(0, new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownAll });
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        private void BindChcekBoxStandardStrategics()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData data1 = new ServiceModels.GenericDropDownListData();
            data1.Text = UI.LabelStandardStrategic1;
            data1.Value = "IsPassMission1";
            list.Add(data1);

            ServiceModels.GenericDropDownListData data2 = new ServiceModels.GenericDropDownListData();
            data2.Text = UI.LabelStandardStrategic2;
            data2.Value = "IsPassMission2";
            list.Add(data2);

            ServiceModels.GenericDropDownListData data3 = new ServiceModels.GenericDropDownListData();
            data3.Text = UI.LabelStandardStrategic3;
            data3.Value = "IsPassMission3";
            list.Add(data3);

            ServiceModels.GenericDropDownListData data4 = new ServiceModels.GenericDropDownListData();
            data4.Text = UI.LabelStandardStrategic4;
            data4.Value = "IsPassMission4";
            list.Add(data4);

            ServiceModels.GenericDropDownListData data5 = new ServiceModels.GenericDropDownListData();
            data5.Text = UI.LabelStandardStrategic5;
            data5.Value = "IsPassMission5";
            list.Add(data5);

            CheckBoxListStandardStrategics.DataSource = list;
            CheckBoxListStandardStrategics.DataTextField = "Text";
            CheckBoxListStandardStrategics.DataValueField = "Value";
            CheckBoxListStandardStrategics.DataBind();
        }
        #endregion Private Method

        protected void GridProjectInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String key = e.CommandArgument.ToString();
            decimal projectInfoID = 0;
            Decimal.TryParse(key, out projectInfoID);

            if ((e.CommandName == "del") && (projectInfoID > 0))
            {
                var result = ProjectService.DeleteProject(projectInfoID);
                if (result.IsCompleted)
                {
                    GridProjectInfo.DataBind();
                    ShowResultMessage(result.Message);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
            if ((e.CommandName == "Acknowledged") && (projectInfoID > 0))
            {
                var db = ProjectService.GetDB();
                var gen = db.ProjectGeneralInfoes.Where(w => w.ProjectID == projectInfoID).FirstOrDefault();
                if (gen != null)
                {
                    gen.ACKNOWLEDGED = "1";
                    db.SaveChanges();
                    GridProjectInfo.DataBind();
                }
            }
        }

        private List<ServiceModels.GenericDropDownListData> GetOrgProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = ProvinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            lblCannotAdd.Visible = false;
            var canAdd = true;
            if (!UserInfo.IsAdministrator && (UserInfo.UserGroupCode != Common.UserGroupCode.เจ้าหน้าที่จังหวัด))
            {
                //kenghot
                var isBlackList = _orgService.IsBlackList(UserInfo.OrganizationID, UserInfo.ProvinceID);
                if (isBlackList.IsCompleted && isBlackList.Data)
                {
                    //var err = "องค์กรของท่านไม่สามารถเพิ่มโครงการได้ เนื่องจากองค์กรถูกจัดไว้ในสถานะ Blacklist";
                    //ShowErrorMessage(err);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" +err+
                    //    "');window.location.href='/ProjectInfo/ProjectInfoList.aspx' ", true);
                    //ButtonSave.Visible = false;
                    lblCannotAdd.Visible = true;
                    canAdd = false;
                    List<ServiceModels.IFilterDescriptor> filters = CreateFilterForCannotAdd(UserInfo.OrganizationID, UserInfo.ProvinceID);
                    this.GridProjectInfo.FilterDescriptors = filters;
                    this.GridProjectInfo.DataBind();

                    UpdateTotalIsFollowup();
                }
                //end kenghot

            }
            if (canAdd)
            {
                Response.Redirect("~/ProjectInfo/Create.aspx");
            }

        }

        protected void btnRefreshDashBoard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProjectInfo/ProjectDashboard.aspx");
            //lblDashBoard.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);

            //ServiceModels.KendoChart pie = ProjectService.GetDashBoardData();  

            //hdfDashBoard.Value = Newtonsoft.Json.JsonConvert.SerializeObject(pie.series);
            //ScriptManager.RegisterClientScriptBlock( UpdatePanel1 , UpdatePanel1.GetType(),"ScriptDashBoard", "createChart()", true);
            //  lblDashBoard.Text += "<script>createChart();</script>";
        }

    }
}