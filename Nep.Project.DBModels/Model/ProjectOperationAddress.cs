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
    
    public partial class ProjectOperationAddress
    {
        public decimal OperationAddressID { get; set; }
        public decimal ProjectID { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public Nullable<decimal> SubDistrictID { get; set; }
        public string SubDistrict { get; set; }
        public Nullable<decimal> DistrictID { get; set; }
        public string District { get; set; }
        public decimal ProvinceID { get; set; }
        public Nullable<decimal> LocationMapID { get; set; }
    
        public virtual ProjectGeneralInfo PROJECTGENERALINFO { get; set; }
        public virtual MT_Attachment MapAttachment { get; set; }
        public virtual MT_Province MT_Province { get; set; }
    }
}
