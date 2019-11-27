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
    
    public partial class OrganizationRegisterEntry
    {
        public decimal EntryID { get; set; }
        public string OrganizationNameTH { get; set; }
        public string OrganizationNameEN { get; set; }
        public decimal OrganizationTypeID { get; set; }
        public string OrganizationTypeETC { get; set; }
        public string OrganizationYear { get; set; }
        public string OrgUnderSupport { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public decimal SubDistrictID { get; set; }
        public decimal DistrictID { get; set; }
        public decimal ProvinceID { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public decimal UserEntryID { get; set; }
        public Nullable<System.DateTime> OrgEstablishedDate { get; set; }
        public string Mobile { get; set; }
        public Nullable<decimal> ApprovedByID { get; set; }
    
        public virtual MT_District District { get; set; }
        public virtual MT_OrganizationType OrganizationType { get; set; }
        public virtual MT_Province Province { get; set; }
        public virtual MT_SubDistrict SubDistrict { get; set; }
        public virtual UserRegisterEntry UserRegisterEntry { get; set; }
    }
}
