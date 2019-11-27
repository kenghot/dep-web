using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class ProcessedPopup : Infra.BasePage
    {
        public IServices.IProviceService _provService { get; set; }
        public IServices.IProjectInfoService _projectService { get; set; }

        public Decimal ProjectID
        {
            get
            {
                if (ViewState["ProjectID"] == null)
                {
                    string stID = Request.QueryString["projectid"];
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

        public String OperationAddressAttachmentPrefix
        {
            get
            {
                if (ViewState["OperationAddressAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["OperationAddressAttachmentPrefix"] = prefix;
                }
                return ViewState["OperationAddressAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["OperationAddressAttachmentPrefix"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            String getProvinceUrl = ResolveUrl("~/ProjectInfo/ComboboxHandler/GetProvince");
            String getDistrictUrl = ResolveUrl("~/ProjectInfo/ComboboxHandler/GetDistrict");
            String getSubdistrictUrl = ResolveUrl("~/ProjectInfo/ComboboxHandler/GetSubDistrict");
            List<ServiceModels.GenericDropDownListData> provinceList = GetProvince();
            //Int32 provinceID = Convert.ToInt32(provinceList[0].Value);

                String scriptUrl = ResolveUrl("~/Scripts/manage.processedaddress.js?v=" + DateTime.Now.Ticks.ToString());
                var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
                ScriptManager.RegisterClientScriptBlock(
                           this.Page,
                           this.GetType(),
                           "RefOperationAddressScript",
                           refScript,
                           false);

                String script = @" 
            $(function () {                   
                    
                    c2xProcAddress.config({                       
                        AttachmentViewAttachmentPrefix: '" + OperationAddressAttachmentPrefix + @"',                       
                        TxtAddressID: '"+ TextBoxAddress.ClientID + @"',
                        TxtMooID: '" + TextBoxMoo.ClientID + @"',
                        TxtBuildingID: '" + TextBoxBuilding.ClientID + @"',
                        TxtSoiID: '" + TextBoxSoi.ClientID + @"',
                        TxtRoadID: '" + TextBoxRoad.ClientID + @"',
                        TxtDescription: '" + TextBoxActivity.ClientID + @"',
                        TxtProcessStart: '" + ProcessStart.ClientID + @"',
                        TxtProcessEnd: '" + ProcessEnd.ClientID + @"',
                        DdlSubdistrictID : '" + DdlSubDistrict.ClientID + @"',
                        DdlDistrictID: '" + DdlDistrict.ClientID + @"',
                        DdlProvinceID: '" + DdlProvince.ClientID + @"',

                        GridOperationAddressID: 'OperationAddressGrid',
                        UrlGetProvince: '" + getProvinceUrl + @"',
                        UrlGetDistrict: '" + getDistrictUrl + @"',
                        UrlGetSubDistrict: '" + getSubdistrictUrl + @"',
                        AddressFormTitle: '" + Nep.Project.Resources.Model.ProjectInfo_LocationPropose + @"',
                        ColumnTitle:{
                                     Address: '" + Nep.Project.Resources.UI.LabelAddress + @"', SubDistrictID:'" + Nep.Project.Resources.Model.ProjectInfo_SubDistrict + @"',
                                     DistrictID: '" + Nep.Project.Resources.Model.ProjectInfo_District + @"', ProvinceID:'" + Nep.Project.Resources.Model.ProjectInfo_Province + @"',
                                     LocationMapID: '" + Nep.Project.Resources.Model.ProcessingPlan_Map + @"',
                                    },
                        IsView: false,
                        ProjectID: " + ProjectID + @",
                        RequredFieldMsg : '" + Nep.Project.Resources.Error.RequiredField + @"',
                        DdlPleaseSelect : '"+ Nep.Project.Resources.UI.DropdownPleaseSelect+ @"',
                        ProvinceData:{Data:"+ Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince()) + @"},                        
                        });
                        
                    c2xProcAddress.createValidator();
                });

         function updateOperationAddress(model){
            window.parent.updateOperationAddress(model);    
            c2x.closeFormDialog();           
         }
       
";

                ScriptManager.RegisterStartupScript(
                          this.Page,
                          this.GetType(),
                          "UpdateProcessingAddressScript",
                          script,
                          true);
          
        }

        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();

            //var result = _projectService.GetProjectProvince(ProjectID);
            //if (result.IsCompleted)
            //{
            //    list.Add(result.Data);
            //}
            //else
            //{
            //    ShowErrorMessage(result.Message);
            //}
            var result = _provService.ListProvince(null);
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

        private  List<ServiceModels.GenericDropDownListData> GetListDistrict(Int32 provinceID){
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provService.ListDistrict(provinceID, null);
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

        protected void CustomValidatorCombobox_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = args.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        } 

    }
}