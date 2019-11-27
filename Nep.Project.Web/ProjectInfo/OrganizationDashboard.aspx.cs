using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using System.Web.ModelBinding;

namespace Nep.Project.Web.ProjectInfo
{
    
    public partial class OrganizationDashboard : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public IServices.IListOfValueService LovService {get; set;}
        public IServices.IOrganizationParameterService OrgParamService { get; set; }
        public IServices.IProviceService ProvinceService { get; set; }
        public IServices.IProjectInfoService ProjectService {get; set;}
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
            get {
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
            get {
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
           // lblCannotAdd.Visible = false;
            if (!Page.IsPostBack)
            {
                //DatePickerBudgetYear.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
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

                //GridProjectInfo.Sort("SubmitedDate", SortDirection.Descending);
                //GridProjectInfo.Sort("CreatedDate", SortDirection.Descending);

                bool isCompany = (IsProvinceOfficer || IsCenterOfficer) ? false : true;
                bool isProvince = IsProvinceOfficer;
                //string gridViewCss = GridProjectInfo.CssClass;
                if (isCompany)
                {
                  //  FormGroupOrgName.Visible = false;
                  //  FormGroupOrgType.Visible = false;
                    //FormGroupProvince.Visible = false;
                    //gridViewCss += " view-data-org"; 
                }
                else if (isProvince)
                {
                    //FormGroupProvince.Visible = false;
                    decimal provinceID = (UserInfo.ProvinceID.HasValue) ? (decimal)UserInfo.ProvinceID : 0;
                    DdlProvince.Enabled = false;
                    DdlProvince.Text = provinceID.ToString();
                    //gridViewCss += " view-data-province"; 
                }

               // GridProjectInfo.CssClass = gridViewCss;

               // PrepareData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Text)) ? DdlProvince.Text : "null";
            var all = new ServiceModels.GenericDropDownListData();
            all.OrderNo = 0;
            all.Text = "ทั้งหมด";
            all.Value = "0";
            var prov = GetOrgProvince();
            prov.Insert(0, all);
            String script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownAll + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(prov) + @", TotalRow:0, IsCompleted:true},                   
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
        protected String FormatAddress(ServiceModels.RegisteredOrganizationList item)
        {
            var sb = new System.Text.StringBuilder();
            if (!String.IsNullOrWhiteSpace(item.Address))
            {
                sb.AppendFormat(" บ้านเลขที่ {0}", item.Address);
            }

            if (!String.IsNullOrWhiteSpace(item.Building))
            {
                sb.AppendFormat(" อาคาร {0}", item.Building);
            }

            if (!String.IsNullOrWhiteSpace(item.Moo))
            {
                sb.AppendFormat(" หมู่ที่ {0}", item.Moo);
            }

            if (!String.IsNullOrWhiteSpace(item.Soi))
            {
                sb.AppendFormat(" ซอย{0}", item.Soi);
            }

            if (!String.IsNullOrWhiteSpace(item.Road))
            {
                sb.AppendFormat(" ถนน{0}", item.Road);
            }

            if (!String.IsNullOrWhiteSpace(item.SubDistrict))
            {
                sb.AppendFormat(" {0}", item.SubDistrict);
            }

            if (!String.IsNullOrWhiteSpace(item.District))
            {
                sb.AppendFormat(" {0}", item.District);
            }

            if (!String.IsNullOrWhiteSpace(item.Province))
            {
                sb.AppendFormat(" {0}", item.Province);
            }

            if (!String.IsNullOrWhiteSpace(item.PostCode))
            {
                sb.AppendFormat(" {0}", item.PostCode);
            }

            if (sb.Length >= 1)
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
        }

        public List<ServiceModels.RegisteredOrganizationList> OrganizationGrid_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            if (string.IsNullOrEmpty(DdlProvince.Text ) && !DatePickerBudgetYear.SelectedDate.HasValue)
            {
                totalRowCount = 0;
                return null;
            }
            //ServiceModels.ReturnQueryData<ServiceModels.RegisteredOrganizationList> result;
            var result = service.GetORGDashBoardData(decimal.Parse(DdlProvince.Text), DatePickerBudgetYear.SelectedDate.Value.Year);
            //if (rdAllApprove.Checked)
            //TODO : check
            //if (true)
            //{
            //    result = service.ListRegisteredOrganization(OrganizationGrid.QueryParameter);
            //}
            //else
            //{
            //   // result = service.ListRegisteredOrganization(OrganizationGrid.QueryParameter, rdApproved.Checked);
            //}

            List<ServiceModels.RegisteredOrganizationList> data = new List<ServiceModels.RegisteredOrganizationList>();
            totalRowCount = 0;

            //if (result.IsCompleted)
            //{
                data = result.OrganizationList;
                totalRowCount = data.Count();
                OrganizationGrid.TotalRows = totalRowCount;

                if (totalRowCount == 0)
                {
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    // @TODO Clear Message
                }
            //}
            //else
            //{
            //    ShowErrorMessage(result.Message);
            //}
            hdfDashBoardGrahp.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ORGCountByStatus);
            hdfDashBoardPie.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.BudgetChart.series);
            ScriptManager.RegisterClientScriptBlock(UpdatePanelSearch, UpdatePanelSearch.GetType(), "ScriptDashBoard", "createGraph();createChart()", true);
            return data;
           
        }

        private bool _isAutoBinding = true;
        //public List<ServiceModels.ProjectInfo.ProjectInfoList> GridProjectInfo_GetData()
        //{
        //    if (_isAutoBinding)
        //        return null;
        //   // List<ServiceModels.IFilterDescriptor> filters = CreateFilter(false);
             
        //    var q = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filters, 0, 9999999, null, null);
        //    var result = this.ProjectService.GetDashBoardData(q);
        //    //var result = this.ProjectService.ListProjectInfoList(GridProjectInfo.QueryParameter);
        //    List<ServiceModels.ProjectInfo.ProjectInfoList> data = new List<ServiceModels.ProjectInfo.ProjectInfoList>();
        //    //totalRowCount = 0;
        //    if (true) //result.IsCompleted)
        //    {
        //        data = result.ProjectInfoList ;
        //        //totalRowCount = result.TotalRow;
        //       // GridProjectInfo.TotalRows = result.TotalRow;
        //        //if (result.TotalRow == 0)
        //        //{
        //        //    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
        //        //}
        //    }
        //    else
        //    {
        //        //ShowErrorMessage(result.Message[0]);
        //    }
        //    hdfDashBoardGrahp.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ProjectCountByStatus);
        //    hdfDashBoardPie.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.BudgetChart.series);
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanelSearch, UpdatePanelSearch.GetType(), "ScriptDashBoard", "createGraph();createChart()", true);
        //    return data;
        //}
        public void ButtonSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (string.IsNullOrEmpty(DdlProvince.Text) || !DatePickerBudgetYear.SelectedDate.HasValue)
            {
                lblMessage.Text = "กรุณาระบุข้อมูล";
                return;
            }
            _isAutoBinding = false;

            //  this.GridProjectInfo.DataBind();
            SearchData();

         

        }

        private void SearchData()
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
      
            //if (UserInfo.IsProvinceOfficer)
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = UserInfo.ProvinceID });
            //}
            if (!String.IsNullOrEmpty(DdlProvince.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = Decimal.Parse(DdlProvince.Text) });
            }
            //if (chbApproved.Checked && !chbNotApproved.Checked){
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ApproveDate", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = new DateTime?()});
            //}
            //if (!chbApproved.Checked && chbNotApproved.Checked)
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ApproveDate", Operator = ServiceModels.FilterOperator.IsGreaterThan, Value = new DateTime?() });
            //}
            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }


            this.OrganizationGrid.FilterDescriptors = filterComposite;
            this.OrganizationGrid.DataBind();
        }
        #region Grid Project Info
        //public List<ServiceModels.ProjectInfo.ProjectInfoList> GridProjectInfo_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)


        protected void OrganizationGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
        { 
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                int colSpan = 13;
                e.Row.Cells[0].ColumnSpan = colSpan;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.CssClass = "rowHeader";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ServiceModels.RegisteredOrganizationList r = (ServiceModels.RegisteredOrganizationList) e.Row.DataItem;
                e.Row.CssClass = (r.ApproveDate.HasValue ? "1" : "2");
                e.Row.BackColor = r.ApproveDate.HasValue ? System.Drawing.Color.Lime : System.Drawing.Color.Yellow;
                //switch (r.RecStatus)
                //{
                //    case "1":
                //        e.Row.BackColor = System.Drawing.Color.Fuchsia;

                //        break;
                //    case "2":
                //        e.Row.BackColor = System.Drawing.Color.Yellow;

                //        break;
                //    case "3":
                //        e.Row.BackColor = System.Drawing.Color.Orange;
                //        break;
                //    case "4":
                //        e.Row.BackColor = System.Drawing.Color.Fuchsia;
                //        break;
                //    case "5":
                //        e.Row.BackColor = System.Drawing.Color.Lime;
                //        break;
                //    case "6":
                //        e.Row.BackColor = System.Drawing.Color.Silver;
                //        break;
                //}
                //Image img = (Image)e.Row.FindControl("imgApprovalStatus");
                //if (img != null)
                //{
                //    if (r.ApprovalStatus1 != null)
                //    {
                //        img.Visible = true;
                //        img.ImageUrl = string.Format("~/Images/Approval/ApprovalStatus1_{0}.png", r.ApprovalStatus1.LOVCode.Trim());
                //        img.AlternateText = r.ApprovalStatus1.LOVName;
                //        img.ToolTip = r.ApprovalStatus1.LOVName;
                //    }
                //}
            }
        }
       
        #endregion Grid Project Info

        #region Private Method

        private void PrepareData()
        {
            List<string> errors = new List<string>();

            #region Bind data to ประเภทโครงการ
            // KengDashBoard
            //List<ServiceModels.ListOfValue> projectTypeList = new List<ServiceModels.ListOfValue>();
            //var projectTypeListResult = this.LovService.ListAll(Common.LOVGroup.ProjectType);
            //if (projectTypeListResult.IsCompleted)
            //{
            //    projectTypeList = projectTypeListResult.Data;
            //    projectTypeList.Insert(0, new ServiceModels.ListOfValue() { LovID = -1, LovName = UI.DropdownAll });
            //    DropDownListProjectInfoType.DataSource = projectTypeList;
            //    DropDownListProjectInfoType.DataBind();
            //}
            //else {
            //    errors.Add(projectTypeListResult.Message[0]);
            //}
            #endregion Bind data to ประเภทโครงการ

            #region Bind data to ประเภทความพิการ
            // KengDashBoard
            //var disabilityTypeListResult = this.LovService.ListAll(Common.LOVGroup.DisabilityType);
            //if (disabilityTypeListResult.IsCompleted)
            //{
            //    CheckBoxListTypeDisabilitys.DataSource = disabilityTypeListResult.Data;
            //    CheckBoxListTypeDisabilitys.DataBind();
            //}
            //else
            //{
            //    errors.Add(disabilityTypeListResult.Message[0]);
            //}
            #endregion Bind data to ประเภทความพิการ

            #region Bind data to ยุทธศาสตร์ที่สอดคล้องกับโครงการ
            // KengDashBoard
            //BindChcekBoxStandardStrategics();
            #endregion Bind data to ยุทธศาสตร์ที่สอดคล้องกับโครงการ

            #region Bind data to ขั้นตอนการอนุมัติ
            // KengDashBoard
            //List<ServiceModels.ListOfValue> approvalProcessList = new List<ServiceModels.ListOfValue>();
            //var approvalProcessResult = this.LovService.ListAll(Common.LOVGroup.ProjectApprovalStatus);
            //if (approvalProcessResult.IsCompleted)
            //{
            //    approvalProcessList = approvalProcessResult.Data;
            //    ServiceModels.ListOfValue approvalStatusCancel = approvalProcessList.Where(x => x.LovCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา).FirstOrDefault();
            //    ServiceModels.ListOfValue cancelledProjectRequestStatus = approvalProcessList.Where(x => x.LovCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกคำร้อง).FirstOrDefault();

            //    approvalProcessList = approvalProcessList.Where(x => 
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ) ||
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน) || 
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง) ||
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน) ||
            //        (x.LovCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)).ToList();


            //    CheckBoxListApprovalProcess.DataSource = approvalProcessList;
            //    CheckBoxListApprovalProcess.DataBind();
            //    CheckBoxCancelContractStatus.Attributes.Add("Value", approvalStatusCancel.LovID.ToString());
            //    CheckBoxCancelledProjectRequest.Attributes.Add("Value", cancelledProjectRequestStatus.LovID.ToString());


            //    for (int i = 0; i < approvalProcessList.Count; i++)
            //    {
            //        ViewState["ApprovalNameStep" + (i + 1)] = approvalProcessList[i].LovName;
            //    }

            //    ViewState["NotApprovalName"] = Nep.Project.Resources.UI.LabelNotApprovalStatus;
            //}
            //else
            //{
            //    errors.Add(approvalProcessResult.Message[0]);
            //}
            #endregion Bind data to ขั้นตอนการอนุมัติ

 

            // KengDashBoard
            // UpdateTotalIsFollowup();

           // List<ServiceModels.IFilterDescriptor> filters = CreateFilter(false);
            //GridProjectInfo.FilterDescriptors = filters;

            //Create Button
            //if (UserInfo.Roles.Contains(Common.FunctionCode.REQUEST_PROJECT) ||
            //    UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_PROJECT))
            //{
            //    ButtonAdd.Visible = true;
            //}
            
            if (errors.Count > 0)
            {
                ShowErrorMessage(errors);
            }
        }

        //private void UpdateTotalIsFollowup()
        //{
        //    List<ServiceModels.IFilterDescriptor> filters = CreateFilter(true);

        //    ServiceModels.QueryParameter p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filters, 0, Common.Constants.PAGE_SIZE, null, null);
        //    var result = ProjectService.GetTotalIsFollowup(p);
        //    string totalIsFollowup = "0";
        //    if (result.IsCompleted)
        //    {
        //        totalIsFollowup = Nep.Project.Common.Web.WebUtility.DisplayInHtml(result.Data, "N0", "0");                
        //    }else{
        //        ShowErrorMessage(result.Message);
        //    }

        //    //LabelTotalIsFollowup.Text = String.Format(Nep.Project.Resources.Message.TotalFollowup, totalIsFollowup);
        //}

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
          
   
        #endregion Private Method

        

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

      

        protected void btnRefreshDashBoard_Click(object sender, EventArgs e)
        {
            //lblDashBoard.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);

            ServiceModels.KendoChart pie = ProjectService.GetDashBoardData();  
        
            //hdfDashBoard.Value = Newtonsoft.Json.JsonConvert.SerializeObject(pie.series);
            //ScriptManager.RegisterClientScriptBlock( UpdatePanel1 , UpdatePanel1.GetType(),"ScriptDashBoard", "createChart()", true);
          
        }
        
    }
}