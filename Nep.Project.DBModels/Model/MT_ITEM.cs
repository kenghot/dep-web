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
    
    public partial class MT_ITEM
    {
        public MT_ITEM()
        {
            this.PROJECTEVALUATIONs = new HashSet<ProjectEvaluation>();
        }
    
        public decimal ITEMID { get; set; }
        public string ITEMGROUP { get; set; }
        public string ITEMNAME { get; set; }
        public decimal ORDERNO { get; set; }
        public string ISACTIVE { get; set; }
        public string CREATEDBY { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public string ISDELETE { get; set; }
    
        public virtual ICollection<ProjectEvaluation> PROJECTEVALUATIONs { get; set; }
    }
}
