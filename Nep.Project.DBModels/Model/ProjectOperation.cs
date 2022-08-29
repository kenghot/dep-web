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
    
    public partial class ProjectOperation
    {
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
        public Nullable<decimal> ProvinceID { get; set; }
        public Nullable<decimal> LocationMapID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string TimeDesc { get; set; }
        public string Method { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public decimal TotalDay { get; set; }
        public decimal CreatedByID { get; set; }
        public Nullable<decimal> UpdatedByID { get; set; }
        public string EXTENDDATA { get; set; }
    
        public virtual MT_Attachment LocationMap { get; set; }
        public virtual MT_District MT_District { get; set; }
        public virtual MT_Province MT_Province { get; set; }
        public virtual ProjectGeneralInfo ProjectGanerakInfo { get; set; }
    }
}
