//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nep.Project.DBModels.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MT_SubDistrict
    {
        public MT_SubDistrict()
        {
            this.Organization = new HashSet<MT_Organization>();
            this.OrganizationRegisterEntries = new HashSet<OrganizationRegisterEntry>();
        }
    
        public decimal SubDistrictID { get; set; }
        public string SubDistrictName { get; set; }
        public decimal DistrictID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual MT_District District { get; set; }
        public virtual ICollection<MT_Organization> Organization { get; set; }
        public virtual ICollection<OrganizationRegisterEntry> OrganizationRegisterEntries { get; set; }
    }
}
