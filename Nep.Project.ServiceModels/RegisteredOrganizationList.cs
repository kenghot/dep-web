using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class RegisteredOrganizationList
    {
        //User Info
        public Decimal OrganizationEntryID { get; set; }

        public String RegisterName { get; set; }
        public DateTime RegisterDate { get; set; }

        public DateTime? ApproveDate { get; set; }
        public String OrganizationName { get; set; }

        public String OrganizationType { get; set; }

        public String OrganizationTypeEtc { get; set; }

        public String OrgUnderSupport { get; set; }

        [Display(Name = "Organization_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address { get; set; }

        [Display(Name = "Organization_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building { get; set; }

        [Display(Name = "Organization_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo { get; set; }

        [Display(Name = "Organization_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi { get; set; }

        [Display(Name = "Organization_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road { get; set; }

        [Display(Name = "Organization_SubDistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict { get; set; }

        [Display(Name = "Organization_DistrictID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District { get; set; }

        [Display(Name = "Organization_ProvinceID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Province { get; set; }
        public Decimal ProvinceID { get; set; }

        [Display(Name = "Organization_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public string PostCode { get; set; }
        public string Status {  get { return (ApproveDate.HasValue) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ"; }   }
        public bool IsDeletable { get; set; }
        
    }
}
