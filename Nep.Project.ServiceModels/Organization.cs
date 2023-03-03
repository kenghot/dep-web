using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class OrganizationProfile
    {
        public decimal OrganizationID { get; set; }

        [Display(Name = "Organization_OrganizationNameTH", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(1000, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationNameTH { get; set; }

        [Display(Name = "Organization_OrganizationNameEN", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(1000, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationNameEN { get; set; }

        [Display(Name = "Organization_OrganizationType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal OrganizationType { get; set; }
        
        [Display(Name = "Organization_OrganizationTypeEtc", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationTypeEtc { get; set; }

        [Display(Name = "Organization_OrganizationYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(4, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationYear { get; set; }

        [Display(Name = "Organization_OrganizationDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? OrgEstablishedDate { get; set; }

        [Display(Name = "Organization_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Address { get; set; }

        [Display(Name = "Organization_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Building { get; set; }

        [Display(Name = "Organization_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Moo { get; set; }

        [Display(Name = "Organization_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Soi { get; set; }

        [Display(Name = "Organization_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Road { get; set; }

        [Display(Name = "Organization_SubDistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal SubDistrictID { get; set; }

        [Display(Name = "Organization_DistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal DistrictID { get; set; }

        [Display(Name = "Organization_ProvinceID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal ProvinceID { get; set; }

        [Display(Name = "Organization_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string PostCode { get; set; }

        [Display(Name = "Organization_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Telephone { get; set; }

        [Display(Name = "Organization_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Mobile { get; set; }

        [Display(Name = "Organization_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Fax { get; set; }

        [Display(Name = "Organization_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string Email { get; set; }

        [Display(Name = "UserProfile_IsActive", ResourceType = typeof(Nep.Project.Resources.Model))]
        public bool IsActive { get; set; }     

        [Display(Name = "Organization_Under", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationUnder { get; set; }

        //[Display(Name = "UserProfile_FirstName", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        //[StringLength(100, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        //public string ProfileName { get; set; }

        //[Display(Name = "UserProfile_Role", ResourceType = typeof(Nep.Project.Resources.Model))]
        //public Common.UserRole Role { get; set; }

        //[Display(Name = "UserProfile_Password", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Display(Name = "UserProfile_ConfirmPassword", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        public bool IsDeleteable { get; set; }
        public string ExtendJSON { get; set; }
        public OrganizationExtend ExtendData { get; set; }
    }
    public class OrganizationExtend
    {
        public string BankNo { get; set; }
        public string BranchNo { get; set; }
        public string AccountNo { get; set; }
        public  string AccountName { get; set; }
    }

    [Serializable]
    public class OrganizationList
    {
        public Decimal OrganizationID { get; set; }

        //[Display(Name = "Organization_RequestName", ResourceType = typeof(Nep.Project.Resources.Model))]
        //public string RequestName { get; set; }

        //[Display(Name = "Organization_RequestDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        //public string RequestDate { get; set; }

        //[Display(Name = "Organization_OrganizationNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        //public string OrganizationNo { get; set; }

        [Display(Name = "Organization_OrganizationName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationName { get; set; }

        [Display(Name = "Organization_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Address { get; set; }

        [Display(Name = "Organization_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Building { get; set; }

        [Display(Name = "Organization_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Moo { get; set; }

        [Display(Name = "Organization_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Soi { get; set; }

        [Display(Name = "Organization_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Road { get; set; }

        [Display(Name = "Organization_SubDistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal SubDistrictID { get; set; }

        [Display(Name = "Organization_SubDistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict { get; set; }

        [Display(Name = "Organization_DistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal DistrictID { get; set; }

        [Display(Name = "Organization_DistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District { get; set; }

        [Display(Name = "Organization_ProvinceID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal ProvinceID { get; set; }

        [Display(Name = "Organization_ProvinceID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Province { get; set; }

        [Display(Name = "Organization_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string PostCode { get; set; }


        [Display(Name = "Organization_Under", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationUnder { get; set; }

        public bool IsDeletable { get; set; }
    }
    public class OrganizationListAPI
    {
        public Decimal OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public string Address { get; set; }

        public string Building { get; set; }

        public string Moo { get; set; }

        public string Soi { get; set; }

        public string Road { get; set; }

        public decimal SubDistrictID { get; set; }

        public String SubDistrict { get; set; }

        public decimal DistrictID { get; set; }

        public String District { get; set; }

        public decimal ProvinceID { get; set; }

        public String Province { get; set; }

        public string PostCode { get; set; }


        public string OrganizationUnder { get; set; }

        public bool IsDeletable { get; set; }
    }
}
