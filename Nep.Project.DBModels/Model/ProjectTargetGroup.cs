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
    
    public partial class ProjectTargetGroup
    {
        public ProjectTargetGroup()
        {
            this.ProjectParticiPants = new HashSet<ProjectParticipant>();
        }
    
        public decimal ProjectTargetGroupID { get; set; }
        public decimal ProjectID { get; set; }
        public decimal TargetGroupID { get; set; }
        public string TargetGroupEtc { get; set; }
        public decimal TargetGroupAmt { get; set; }
        public Nullable<decimal> Male { get; set; }
        public Nullable<decimal> Female { get; set; }
    
        public virtual ProjectGeneralInfo ProjectGeneralInfo { get; set; }
        public virtual ICollection<ProjectParticipant> ProjectParticiPants { get; set; }
    }
}
