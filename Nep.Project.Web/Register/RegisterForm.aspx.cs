using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Nep.Project.Web.Register
{
    public partial class RegisterForm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IProjectInfoService projservice { get; set; }
        public Int32 CenterProvinceID
        {
            get
            {
                return Convert.ToInt32(ViewState["CenterProvinceID"]);
            }
            set
            {
                ViewState["CenterProvinceID"] = value;
            }
        }
        public String RegisPrefix
        {
            get
            {
                if (ViewState["RegisPrefix"] == null)
                {
                    string prefix = "regis/";
                    ViewState["RegisPrefix"] = prefix;
                }
                return ViewState["RegisPrefix"].ToString();
            }

            set
            {
                ViewState["RegisPrefix"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var centerProvinceResult = _provinceService.GetCenterProvinceID();
                if (centerProvinceResult.IsCompleted)
                {
                    CenterProvinceID = centerProvinceResult.Data;
                }
                else
                {
                    ShowErrorMessage(centerProvinceResult.Message);
                }

            
            }
        }
        private bool isORG()
        {
            return (this.UserInfo != null) && (this.UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }
       
        protected override void OnPreRender(EventArgs e)
        {
          
            base.OnPreRender(e);
            string orgSelected = DdlOrganization.Value;
            string provSelected = DdlOrganizationProvince.Value;
        
            String script = @" 
             $(function () {                   
                   
                   c2x.createVirtualCombobox({      
                        AutoBind:true,                 
                        ControlID: '" + DdlOrganizationProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        TextField: 'Text',
                        ValueField: 'Value',  
                        Change: function(e){onDdlOrganizationProvince('" + DdlOrganization.ClientID + @"',e);},
                        ServerFiltering:false,                                            
                        Value: '" + provSelected  + @"',  
                       // ReadUrl: './ComboboxHandler/orgregister',
                        ReadUrl: './ComboboxHandler/getprovince',
                        //VirtualUrl: './ComboboxHandler/orgregistermapper', 
                        Enable:true,
                     });                   
                   
                    c2x.createVirtualCombobox({      
                        AutoBind:false,                 
                        ControlID: '" + DdlOrganization.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlOrganizationProvince.ClientID + @"',
                       // ParentValue:'" + provSelected + @"',
                        ParentValue:'181',
                        TextField: 'Text',
                        ValueField: 'Value',                       
                        ReadUrl: './ComboboxHandler/getorg',
                        VirtualUrl: './ComboboxHandler/orgvaluemapper', 
                        Value: '" + orgSelected + @"',  
                        Param: function(){return getOrgComboboxParam('" + DdlOrganizationProvince.ClientID + @"', '" + CenterProvinceID + @"');},
                     });  
                });
              function onDdlOrganizationProvince(ddlOrgID, e){
                    var item = e.sender.dataItem();
                    var ddlOrg = $('#' + ddlOrgID).data('kendoComboBox');                   
                    
                    if (item != null) {
           
                        var provinceID = parseInt(item.Value, 10);            
                        var dataSource = ddlOrg.dataSource;            

                        ddlOrg.select(-1);
                        ddlOrg.enable(true);      
                          
                        ddlOrg.dataSource.read().then(function () {                            
                            ddlOrg.focus();
                        });

                       
                    } else {           

                        ddlOrg.select(-1);
                        ddlOrg.enable(false);
                    }
                }
                 function getOrgComboboxParam(parentId, provinceCenter){
                     var ddl = $('#' + parentId).data('kendoComboBox');
                     var selectItem = ddl.dataItem();                    
                     if (selectItem != null) {
                        var value = (selectItem.Value != provinceCenter)? selectItem.Value : '';
                        return { parentid: value};
                     }
                }       
                ";
          
                if (isORG())
                {
                    var p = (from pp in projservice.GetDB().MT_Organization where pp.OrganizationID == this.UserInfo.OrganizationID select pp).FirstOrDefault();
                    decimal prov = 0;
                    if (p != null)
                    {
                    //prov = p.ProvinceID;
                    hdfOrganization.Value = p.OrganizationID.ToString();
                         DdlOrganization.Value = p.OrganizationNameTH;
                        var pv = (from pvs in projservice.GetDB().MT_Province where pvs.ProvinceID == p.ProvinceID select pvs).FirstOrDefault();
                        if (pv != null)
                            {
                                DdlOrganizationProvince.Value = pv.ProvinceName;
                                hdfProvince.Value = pv.ProvinceID.ToString();
                            }
                        
                    }
               
                DdlOrganization.Disabled = true;
                DdlOrganizationProvince.Disabled = true; 
               //     script += @" 
               //$(function () { 
               //     var ddlOrg = $('#" + DdlOrganization.ClientID + @"').data('kendoComboBox');
               //     var ddlProv = $('#" + DdlOrganizationProvince.ClientID + @"').data('kendoComboBox');
               //     ddlOrg.enable(false);
               //     ddlProv.enable(false);
               //     ddlProv.value('" + prov.ToString() + @"');
               //    ddlOrg.value('" + this.UserInfo.OrganizationID.ToString() + @"');
                    
               //  });";

                    ButtonSelectOrganize.Visible = false;


                }
         if (!isORG())
            {
                ScriptManager.RegisterStartupScript(
                          this.Page,
                          this.GetType(),
                          "CreateComboboxScript",
                          script,
                          true);
            }


        }
        public List<ServiceModels.GenericDropDownListData> GetOrganizationProvince()
        {
            String filter = DdlOrganizationProvince.Value;


            var result = projservice.GetListProjectProvince();
            if (result.IsCompleted)
            {
                return result.Data;
            }
            else
            {
                return new List<ServiceModels.GenericDropDownListData>();
            }
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
           

            if (Page.IsValid){
                var objModel = new ServiceModels.RegisterEntry();

                string orgValue = DdlOrganization.Value;
                if (isORG())
                {
                    orgValue = hdfOrganization.Value;
                }
                int orgID = 0;
                Int32.TryParse(orgValue, out orgID);

                objModel.OrganizationID = orgID;
                objModel.TelephoneNo = TextBoxTelephoneNo.Text.Trim();
                objModel.Mobile = TextBoxMobileUser.Text.Trim();
                objModel.Email = TextBoxEmail.Text.Trim();
                objModel.Position = TextBoxPosition.Text.Trim();
                objModel.FirstName = TextBoxRegisterFirstName.Text.Trim();
                objModel.LastName = TextBoxRegisterLastName.Text.Trim();
                objModel.Position = TextBoxPosition.Text.Trim();
                objModel.PersonalID = TextBoxPersonalID.Text.Replace("-", "").Trim();

                var personIDCardAttachment = PersonalIDAttachment.AllFiles.First();
                var employeeIDCardAttachment = OrgIdentityAttachment.AllFiles != null ? OrgIdentityAttachment.AllFiles.FirstOrDefault() : null;

                var result = service.CreateRegisterEntry(objModel, personIDCardAttachment, employeeIDCardAttachment);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveUrl("~/Register/RegisterComplete"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
            
        }       

        public IQueryable ListOrganization()
        {
            return service.ListOrganization();
        }

        protected void CustomValidatorOrganization_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            String value = DdlOrganization.Value;
            if (isORG())
            {
                // value = this.UserInfo.OrganizationID.ToString();
                args.IsValid = true;
                return;
            }
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorIDCardNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String str = args.Value.Trim();
            str = str.Replace("-", "").Replace("_", "");
            args.IsValid = (str != "");
        }
    }
}