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
    
    public partial class K_MT_POSITION
    {
        public K_MT_POSITION()
        {
            this.ORGANIZATIONCOMMITTEEs = new HashSet<OrganizationCommittee>();
            this.PROJECTCOMMITTEEs = new HashSet<ProjectCommittee>();
        }
    
        public string POSCODE { get; set; }
        public string POSNAME { get; set; }
        public string SORTORDER { get; set; }
    
        public virtual ICollection<OrganizationCommittee> ORGANIZATIONCOMMITTEEs { get; set; }
        public virtual ICollection<ProjectCommittee> PROJECTCOMMITTEEs { get; set; }
    }
}
