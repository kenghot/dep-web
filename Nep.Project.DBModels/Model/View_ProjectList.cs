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
    
    public partial class View_ProjectList
    {
        public decimal ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string OrganizationName { get; set; }
        public decimal ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public decimal ProjectApprovalStatusID { get; set; }
        public Nullable<decimal> FollowupStatusID { get; set; }
        public string FollowupStatusName { get; set; }
        public Nullable<decimal> IsFollowup { get; set; }
        public Nullable<decimal> IsStep1Approved { get; set; }
        public Nullable<decimal> IsStep2Approved { get; set; }
        public Nullable<decimal> IsStep3Approved { get; set; }
        public Nullable<decimal> IsStep4Approved { get; set; }
        public Nullable<decimal> IsStep5Approved { get; set; }
        public Nullable<decimal> IsStep6Approved { get; set; }
        public string IsPassMission1 { get; set; }
        public string IsPassMission2 { get; set; }
        public string IsPassMission3 { get; set; }
        public string IsPassMission4 { get; set; }
        public string IsPassMission5 { get; set; }
        public Nullable<decimal> ProjectTypeID { get; set; }
        public Nullable<decimal> OrganizationTypeID { get; set; }
        public string OrganizationToBeUnder { get; set; }
        public string ProjectApprovalStatusCode { get; set; }
        public string ProjectNo { get; set; }
        public Nullable<decimal> BudgetValue { get; set; }
        public Nullable<decimal> BudgetReviseValue { get; set; }
        public Nullable<decimal> DisabilityTypeID { get; set; }
        public decimal OrganizationID { get; set; }
        public Nullable<decimal> CreatorOrganizationID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<decimal> CreatorProvinceID { get; set; }
        public Nullable<decimal> IsCreateByOfficer { get; set; }
        public Nullable<decimal> BudgetYear { get; set; }
        public string ProvinceAbbr { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<System.DateTime> SubmitedDate { get; set; }
        public Nullable<decimal> IsReject { get; set; }
        public decimal CreatedByID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ProjectEndDate { get; set; }
        public string REJECTCOMMENT { get; set; }
    }
}
