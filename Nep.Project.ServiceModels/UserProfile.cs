using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class UserProfile
    {
        public decimal UserID { get; set; }

        [Display(Name = "UserProfile_Organization", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? OrganizationID { get; set; }

        [Display(Name = "UserProfile_Organization", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationName { get; set; }       

        [Display(Name = "UserProfile_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal? ProvinceID { get; set; }

        [Display(Name = "UserProfile_Role", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal? GroupID { get; set; }
        public string GroupCode { get; set; }

        [Display(Name = "UserProfile_UserFlag", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string UserFlag{ get; set; }

        public string UserName { get; set; }
        
        public Byte[] Password { get; set; }

        [Display(Name = "UserProfile_FirstName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string FirstName { get; set; }

        [Display(Name = "UserProfile_LastName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "UserProfile_Position", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Position { get; set; }

        [StringLength(13, ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "UserProfile_PersonalID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string IDNO { get; set; }

        [Display(Name = "UserProfile_OrgIdentityAttach", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal? IDCardFileID { get; set; }
        public ServiceModels.KendoAttachment IDCardAttachment { get; set; }

        [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(30, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string Email { get; set; }

        [StringLength(13, ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "UserProfile_TelephoneNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string TelephoneNo { get; set; }

        [Display(Name = "UserProfile_IdentityAttachment", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal? EmpployeeCardFileID { get; set; }
        public ServiceModels.KendoAttachment EmpployeeCardAttachment { get; set; }

        [Display(Name = "UserProfile_IsActive", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string IsActive { get; set; }

        [Display(Name = "UserProfile_RegisterDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public DateTime? RegisterDate { get; set; }

        public string IsDelete { get; set; }

        public string Salt { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate  { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

      
        
    }

    //[Serializable]
    //public class ExternalUserProfile
    //{
    //    public decimal UserID { get; set; }

    //    [Display(Name = "UserProfile_UserName", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    public string UserName { get; set; }

    //    [Display(Name = "UserProfile_TelephoneNo", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    public string TelephoneNo { get; set; }

    //    [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    public string Email { get; set; }

    //    [Display(Name = "UserProfile_FirstName", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    [StringLength(100, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    public string FirstName { get; set; }

    //    [Display(Name = "UserProfile_LastName", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    [StringLength(100, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
    //    public string LastName { get; set; }

    //    [Display(Name = "UserProfile_Password", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Display(Name = "UserProfile_ConfirmPassword", ResourceType = typeof(Nep.Project.Resources.Model))]
    //    [DataType(DataType.Password)]
    //    public string ConfirmPassword { get; set; }

    //}

    [Serializable]
    public class UserList
    {
        public decimal UserID { get; set; }

       

        [Display(Name = "UserProfile_UserName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string UserName { get; set; }

        [Display(Name = "UserProfile_TelephoneNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string TelephoneNo { get; set; }

        [Display(Name = "UserProfile_IsActive", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string IsActive { get; set; }

        public string IsDelete { get; set; }

        [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string Email { get; set; }

        [Display(Name = "UserProfile_FirstName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "UserProfile_Role", ResourceType = typeof(Nep.Project.Resources.Model))]
       // public Common.UserRole Role { get; set; }
        public String Role { get; set; }
        
        public Decimal? RoleID { get; set; }
        public String RoleCode { get; set; }

        [Display(Name = "UserProfile_OrganizationName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string OrganizationName { get; set; }


        public Decimal? ProvinceID { get; set; }
        [Display(Name = "UserProfile_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Province { get; set; }

        
    }
}
