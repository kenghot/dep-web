using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class OrganizationRegisterEntry
    {
        //User Info
        public int OrganzationEntryID { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string EmailUser { get; set; }

        [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string EmailOrganization { get; set; }

        [Display(Name = "UserProfile_FirstName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string RegisterName { get; set; }

        [Display(Name = "UserProfile_RegisterDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "UserProfile_ExpireDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        [DataType(DataType.Date)]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "UserProfile_TelephoneNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string TelephoneNoUser { get; set; }

        [Display(Name = "UserProfile_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string MobileUser { get; set; }

        [Display(Name = "UserProfile_TelephoneNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string TelephoneNoOrganization { get; set; }

        [Display(Name = "UserProfile_Position", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Position { get; set; }

        public Decimal? IdentityAttachmentID { get; set; }

        public string IdentityAttachmentName { get; set; }

        public KendoAttachment IdentityAttachment { get; set; }

        public Decimal? OrgIdentityAttachmentID { get; set; }

        public string OrgIdentityAttachmentName { get; set; }

        public KendoAttachment OrgIdentityAttachment { get; set; }

        [Display(Name = "UserProfile_PersonalID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String PersonalID { get; set; }

        //Org Info

        [Display(Name = "Organization_OrganizationNameTH", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(1000, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationNameTH { get; set; }

        [Display(Name = "Organization_OrganizationNameEN", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(1000, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationNameEN { get; set; }

        [Display(Name = "Organization_OrganizationType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal OrganizationType { get; set; }

        [Display(Name = "Organization_OrganizationType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationTypeName { get; set; }

        [Display(Name = "Organization_OrganizationTypeEtc", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationTypeEtc { get; set; }

        public String OrgUnderSupport { get; set; }

        [Display(Name = "Organization_OrganizationYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(4, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string OrganizationYear { get; set; }

        [Display(Name = "Organization_OrganizationDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? OrganizationDate { get; set; }

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

        [Display(Name = "Organization_Fax", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Fax { get; set; }

        [Display(Name = "Organization_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string MobileOrganization { get; set; }

        public String ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
    public class OrganizationRegisterEntryAPI
    {
        //User Info
        public int OrganzationEntryID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string EmailUser { get; set; }
        public string EmailOrganization { get; set; }
        public string RegisterName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TelephoneNoUser { get; set; }
        public string MobileUser { get; set; }
        public string TelephoneNoOrganization { get; set; }
        public string Position { get; set; }
        public Decimal? IdentityAttachmentID { get; set; }
        public string IdentityAttachmentName { get; set; }
        public KendoAttachmentAPI IdentityAttachment { get; set; }
        public Decimal? OrgIdentityAttachmentID { get; set; }
        public string OrgIdentityAttachmentName { get; set; }
        public KendoAttachmentAPI OrgIdentityAttachment { get; set; }
        public String PersonalID { get; set; }

        //Org Info
        public string OrganizationNameTH { get; set; }
        public string OrganizationNameEN { get; set; }
        public decimal OrganizationType { get; set; }
        public String OrganizationTypeName { get; set; }
        public string OrganizationTypeEtc { get; set; }
        public String OrgUnderSupport { get; set; }
        public string OrganizationYear { get; set; }
        public DateTime? OrganizationDate { get; set; }
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
        public string Fax { get; set; }
        public string MobileOrganization { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
