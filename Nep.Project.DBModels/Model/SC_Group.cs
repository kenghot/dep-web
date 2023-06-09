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
    
    public partial class SC_Group
    {
        public SC_Group()
        {
            this.Function = new HashSet<SC_Function>();
            this.User = new HashSet<SC_User>();
        }
    
        public decimal GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string GroupCode { get; set; }
    
        public virtual ICollection<SC_Function> Function { get; set; }
        public virtual ICollection<SC_User> User { get; set; }
    }
}
