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
    
    public partial class ProjectInformation
    {
        public decimal ProjectID { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectNameEN { get; set; }
        public Nullable<decimal> ProjectTypeID { get; set; }
        public System.DateTime ProjectDate { get; set; }
        public string ProjectReason { get; set; }
        public string ProjectPurpose { get; set; }
        public string ProjectTarget { get; set; }
        public string ProjectKPI { get; set; }
        public string ProjectResult { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ProjectNo { get; set; }
        public Nullable<decimal> DisabilityTypeID { get; set; }
        public decimal BudgetYear { get; set; }
        public Nullable<decimal> CancelledDocumentID { get; set; }
        public string RejectComment { get; set; }
        public decimal CreatedByID { get; set; }
        public Nullable<decimal> UpdatedByID { get; set; }
        public string RejectTopic { get; set; }
    
        public virtual MT_ListOfValue ProjectType { get; set; }
        public virtual ProjectGeneralInfo ProjectGeneralInfo { get; set; }
        public virtual MT_Attachment CancelledDocument { get; set; }
    }
}
