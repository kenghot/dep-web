using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Register
{
    public partial class ConfirmEmail2 : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public Nep.Project.Business.RegisterService sv;
        private static string AcitvatationCode_QueryString = "code";
        private static string ID_QueryString = "entryId";

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }

        int entryid = 0;
        String code = null;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(Request.QueryString[ID_QueryString]) && !string.IsNullOrEmpty(Request.QueryString[AcitvatationCode_QueryString]))
            {
                entryid = int.Parse(Request.QueryString[ID_QueryString].ToString());
                code = Request.QueryString[AcitvatationCode_QueryString];
            }

            if (!IsPostBack)
            {
                var start = 1;
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["NepProjectDBEntities"].ConnectionString;
                var dbm = new DBModels.Model.NepProjectDBEntities();
                var db = dbm.Database;
                var u = dbm.SC_User.Where(w => w.UserID >= 664).ToList();
                for (int i = 0; i < 50; i++)
                {
                    var tmp = u[i];
                    tmp.OrganizationID = 582 + i;
                }
                    //var org = dbm.MT_Organization.Where(w => w.OrganizationID == 563).FirstOrDefault();
                    //for (int i = 0; i <= 50;i++)
                    //{
                    //    DBModels.Model.MT_Organization o = new DBModels.Model.MT_Organization { Address = org.Address ,
                    //     Building = org.Building , CreatedBy = org.CreatedBy , CreatedByID = org.CreatedByID , CreatedDate = org.CreatedDate ,
                    //     District = org.District , DistrictID = org.DistrictID , Email = org.Email , Fax = org.Fax , Mobile = org.Mobile ,
                    //     Moo = org.Moo , OrganizationCommittees = org.OrganizationCommittees , OrganizationID = org.OrganizationID ,
                    //     OrganizationNo = org.OrganizationNo , OrganizationNameEN = "" ,  OrganizationNameTH = "" ,
                    //     OrganizationType = org.OrganizationType , OrganizationTypeEtc = org.OrganizationTypeEtc,
                    //     OrganizationTypeID = org.OrganizationTypeID , OrganizationYear = org.OrganizationYear ,
                    //     OrgEstablishedDate = org.OrgEstablishedDate , OrgUnderSupport = org.OrgUnderSupport,
                    //      PostCode = org.PostCode , Province = org.Province , ProvinceID = org.ProvinceID ,
                    //     RequestDate = org.RequestDate , RequesterLastname = org.RequesterLastname , RequesterName = org.RequesterName ,
                    //     Road = org.Road , Soi = org.Soi    , SubDistrict = org.SubDistrict , SubDistrictID = org.SubDistrictID ,
                    //     Telephone = org.Telephone, UpdatedBy = org.UpdatedBy , UpdatedByID = org.UpdatedByID , UpdatedDate = org.UpdatedDate,
                    //     User = org.User , UserRegisterEntries = org.UserRegisterEntries};
                    //    o.OrganizationNameTH = String.Format("องค์กรทดสอบ {0}", i.ToString().PadLeft(2, '0'));
                    //    o.OrganizationNameEN  = String.Format("Test ORG {0}", i.ToString().PadLeft(2, '0'));
                    //    dbm.MT_Organization.Add (o);


                    //}
                    var x = dbm.SaveChanges();
              
                //var result = service.GetRegistryUserList();
                return;
                var result = dbm.UserRegisterEntries.Where(w => w.EntryID >= 823).ToList();
                foreach ( DBModels.Model.UserRegisterEntry  r in result)
                {
                    txtEmail.Text = r.Email;
                    txtRegisterName.Text = string.Format("test",start.ToString().PadLeft(2,'0'));
                    txtTelephoneNo.Text = "01234567890";
                    HiddenFieldOrgID.Value = (r.OrganizationID.HasValue) ? r.OrganizationID.ToString() : "";
                    HiddenFieldUserID.Value = (r.RegisteredUserID.HasValue) ? r.RegisteredUserID.ToString() : "";

                    decimal? orgID = (!String.IsNullOrEmpty(HiddenFieldOrgID.Value)) ? Convert.ToDecimal(HiddenFieldOrgID.Value) : (decimal?)null;
                    decimal? registeredUserID = (!String.IsNullOrEmpty(HiddenFieldUserID.Value)) ? Convert.ToDecimal(HiddenFieldUserID.Value) : (decimal?)null;
                    ServiceModels.ConfirmEmail data = new ServiceModels.ConfirmEmail();
                    data.ActivationCode = r.ActivationCode ;
                    data.RegisterEntryID = Convert.ToInt16(r.EntryID)  ;
                    data.Password = "test1234";
                    data.ConfirmPassword = "test1234";
                    data.RegisteredUserID = registeredUserID;

               
                        var ct = service.CreateExternalUser(data);
                        if (ct.IsCompleted)
                        {
                            ShowResultMessage(ct.Message);
                            EnableForm(false);
                        }
                        else
                        {
                            ShowErrorMessage(ct.Message);
                        }
                   
                  


                    start += 1;
                }
                   
                    
                
                
            }

            
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            decimal? orgID = (!String.IsNullOrEmpty(HiddenFieldOrgID.Value)) ? Convert.ToDecimal(HiddenFieldOrgID.Value) : (decimal?)null;
            decimal? registeredUserID = (!String.IsNullOrEmpty(HiddenFieldUserID.Value)) ? Convert.ToDecimal(HiddenFieldUserID.Value) : (decimal?)null;
            ServiceModels.ConfirmEmail data = new ServiceModels.ConfirmEmail();
            data.ActivationCode = code;
            data.RegisterEntryID = entryid;
            data.Password = txtPassword.Text.Trim();
            data.ConfirmPassword = txtConfirmPassword.Text.Trim();
            data.RegisteredUserID = registeredUserID;

            if (orgID.HasValue)
            {
                var result = service.CreateExternalUser(data);
                if (result.IsCompleted)
                {
                    ShowResultMessage(result.Message);
                    EnableForm(false);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
            else
            {
                var result = service.CreatePasswordInternalUser(data);
                if (result.IsCompleted)
                {
                    ShowResultMessage(result.Message);                   
                    EnableForm(false);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }

           
        }

        private void EnableForm(Boolean isEnable)
        {
            txtPassword.Enabled = isEnable;
            txtConfirmPassword.Enabled = isEnable;
            ButtonSubmit.Enabled = isEnable;
            ButtonSubmit.Visible = isEnable;
        }

        protected void CustomValidatorPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string text = txtPassword.Text.Trim();
            bool lengthValid = (text.Length >= 8);    
            bool hasLeter =  Regex.IsMatch(text, @"[a-zA-Z]+");
            bool hasNumber = Regex.IsMatch(text, @"[0-9]+");
            args.IsValid = (lengthValid && hasLeter && hasNumber);
        }
    }
}