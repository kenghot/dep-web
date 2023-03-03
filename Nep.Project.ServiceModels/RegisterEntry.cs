using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class RegisterEntry
    {
        public int RegisterEntryID { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        [Display(Name = "UserProfile_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public string Email { get; set; }

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
        public string TelephoneNo { get; set; }

        [Display(Name = "UserProfile_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Mobile { get; set; }

        [Display(Name = "UserProfile_Position", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string Position { get; set; }

        public Decimal? OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public Decimal? IdentityAttachmentID { get; set; }

        public string IdentityAttachmentName { get; set; }

        public Decimal? OrgIdentityAttachmentID { get; set; }

        public string OrgIdentityAttachmentName { get; set; }

        [Display(Name = "UserProfile_PersonalID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String PersonalID { get; set; }

        public String IsActive { get; set; }

        public Decimal? RegisteredUserID { get; set; }

        public DateTime? OrgApprovalDate { get; set; }
    }
    public class RegisterEntryAPI
    {
        public int RegisterEntryID { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string Email { get; set; }
        public string RegisterName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TelephoneNo { get; set; }
        public string Mobile { get; set; }
        public string Position { get; set; }

        public Decimal? OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public Decimal? IdentityAttachmentID { get; set; }

        public string IdentityAttachmentName { get; set; }

        public Decimal? OrgIdentityAttachmentID { get; set; }

        public string OrgIdentityAttachmentName { get; set; }
        public String PersonalID { get; set; }

        public String IsActive { get; set; }

        public Decimal? RegisteredUserID { get; set; }

        public DateTime? OrgApprovalDate { get; set; }
    }
    [Serializable]
    public class ConfirmEmail
    {
        public int RegisterEntryID { get; set; }

        public String ActivationCode { get; set; }

        [Display(Name = "UserProfile_Password", ResourceType = typeof(Nep.Project.Resources.Model))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "UserProfile_ConfirmPassword", ResourceType = typeof(Nep.Project.Resources.Model))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public Decimal? RegisteredUserID { get; set; }
    }
    
}
