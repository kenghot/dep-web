using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using Nep.Project.Resources;

namespace Nep.Project.Web.ManageItem
{
    public partial class StrategicList : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IOrganizationService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }


        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = (UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_ORGANIZATION));
                return isTrue;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_ORGANIZATION };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isDel = Request.QueryString["isDeleteSuccess"];
                if(!String.IsNullOrEmpty(isDel) && (isDel == "true")){
                    ShowResultMessage(Nep.Project.Resources.Message.DeleteSuccess);
                }   
             
                if(UserInfo.IsProvinceOfficer){
                    SearchData();
                }
            }

            //if (UserInfo.IsProvinceOfficer)
            //{
            //    LabelProvince.Visible = false;
            //    DivComboBoxProvince.Visible = false;
            //}
            //else
            //{
            //    LabelProvince.Visible = true;
            //    DivComboBoxProvince.Visible = true;
            //}

            //if (base.UserInfo.IsAdministrator || base.UserInfo.IsCenterOfficer)
            //{
            //    btnRefreshDashBoard.Visible = true;
            //} else
            //{
            //    btnRefreshDashBoard.Visible = false;
            //}
        }

        //protected override void OnPreRender(EventArgs e)
        //{
            //base.OnPreRender(e);
            //string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Value)) ? DdlProvince.Value : "null";
            //String script = @"
            //    $(function () {                 
                    
            //        c2x.createLocalCombobox({                       
            //            ControlID: '" + DdlProvince.ClientID + @"',
            //            Placeholder: '" + Nep.Project.Resources.UI.DropdownAll + @"',                        
            //            TextField: 'Text',
            //            ValueField: 'Value',
            //            ServerFiltering: false,
            //            Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince()) + @", TotalRow:0, IsCompleted:true},                   
            //            Value: " + provinceSelected + @",                     
            //         });  
            //    });";

            //ScriptManager.RegisterStartupScript(
            //         this.Page,
            //         this.GetType(),
            //         "ManageUpdatePanelRegister",
            //         script,
            //         true);
        //}

        //public List<ServiceModels.ItemList> StrategicGrid_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        //{
        //    //var result = _service.List(OrganizationGrid.QueryParameter);
        //    var result = _itemService.ListStrategic(StrategicGrid.QueryParameter);
        //    List<ServiceModels.ItemList> data = new List<ServiceModels.ItemList>();
        //    totalRowCount = 0;

        //    if (result.IsCompleted)
        //    {
        //        data = result.Data;
        //        totalRowCount = result.TotalRow;
        //        StrategicGrid.TotalRows = result.TotalRow;

        //        if (totalRowCount == 0)
        //        {
        //            ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
        //        }
        //        else
        //        {
        //            // @TODO Clear Message
        //        }
        //    }
        //    else
        //    {
        //        ShowErrorMessage(result.Message);
        //    }

        //    return data;
        //}

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }        
        
        #region Private Method

        private void SearchData()
        {
            //List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            //if (!String.IsNullOrEmpty(TextBoxOrganizationName.Text))
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "OrganizationName", Operator = ServiceModels.FilterOperator.Contains, Value = TextBoxOrganizationName.Text.Trim() });
            //}

            //if (UserInfo.IsProvinceOfficer)
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = UserInfo.ProvinceID });
            //}
            //else if (!String.IsNullOrEmpty(DdlProvince.Value))
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = Decimal.Parse(DdlProvince.Value) });
            //}




            //List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            //if (fields.Count > 0)
            //{
            //    filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
            //    {
            //        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
            //        FilterDescriptors = fields
            //    });
            //}


            //this.StrategicGrid.FilterDescriptors = filterComposite;
            //this.StrategicGrid.DataBind();
        }
        #endregion Private Method

        protected String FormatAddress(ServiceModels.OrganizationList item)
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

        public List<ServiceModels.GenericDropDownListData> DropDownListProvince_SelectMethod()
        {
            var data = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListProvince(null);
            if (result.IsCompleted)
            {
                data = result.Data;              
               
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return data;
        }

        protected void GridStrategic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //String key = e.CommandArgument.ToString();
            //decimal id = 0;
            //Decimal.TryParse(key, out id);

            //if ((e.CommandName == "del") && (id > 0))
            //{
            //    var result = _service.Remove(id);
            //    if (result.IsCompleted)
            //    {
            //        StrategicGrid.DataBind();
            //        ShowResultMessage(result.Message);
            //    }
            //    else
            //    {
            //        ShowErrorMessage(result.Message);
            //    }
            //}
        }

        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListProvince(null);
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
            //Response.Redirect("~/ProjectInfo/OrganizationDashboard.aspx");
            //lblDashBoard.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);

            //ServiceModels.KendoChart pie = ProjectService.GetDashBoardData();  

            //hdfDashBoard.Value = Newtonsoft.Json.JsonConvert.SerializeObject(pie.series);
            //ScriptManager.RegisterClientScriptBlock( UpdatePanel1 , UpdatePanel1.GetType(),"ScriptDashBoard", "createChart()", true);
            //  lblDashBoard.Text += "<script>createChart();</script>";
        }
    }
}