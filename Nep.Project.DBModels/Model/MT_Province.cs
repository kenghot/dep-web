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
    
    public partial class MT_Province
    {
        public MT_Province()
        {
            this.District = new HashSet<MT_District>();
            this.Organization = new HashSet<MT_Organization>();
            this.ProjectEvaluations = new HashSet<ProjectEvaluation>();
            this.ProjectOperations = new HashSet<ProjectOperation>();
            this.User = new HashSet<SC_User>();
            this.OrganizationRegisterEntries = new HashSet<OrganizationRegisterEntry>();
            this.ProjectOperationAddresses = new HashSet<ProjectOperationAddress>();
        }
    
        public decimal ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public decimal SectionID { get; set; }
        public string ProvinceAbbr { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ICollection<MT_District> District { get; set; }
        public virtual MT_ListOfValue Section { get; set; }
        public virtual ICollection<MT_Organization> Organization { get; set; }
        public virtual ICollection<ProjectEvaluation> ProjectEvaluations { get; set; }
        public virtual ICollection<ProjectOperation> ProjectOperations { get; set; }
        public virtual ICollection<SC_User> User { get; set; }
        public virtual ICollection<OrganizationRegisterEntry> OrganizationRegisterEntries { get; set; }
        public virtual ICollection<ProjectOperationAddress> ProjectOperationAddresses { get; set; }
    }
}
