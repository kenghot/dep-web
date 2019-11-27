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
    
    public partial class ProjectGeneralInfo
    {
        public ProjectGeneralInfo()
        {
            this.ProjectBudgets = new HashSet<ProjectBudget>();
            this.ProjectCommittees = new HashSet<ProjectCommittee>();
            this.ProjectLogs = new HashSet<ProjectLog>();
            this.ProjectPrintReportTrackings = new HashSet<ProjectPrintReportTracking>();
            this.ProjectTargetGroups = new HashSet<ProjectTargetGroup>();
            this.MT_Attachment = new HashSet<MT_Attachment>();
            this.ProjectParticipants = new HashSet<ProjectParticipant>();
            this.PROJECTOPERATIONADDRESSes = new HashSet<ProjectOperationAddress>();
            this.PROJECTDOCUMENTLISTs = new HashSet<PROJECTDOCUMENTLIST>();
        }
    
        public decimal ProjectID { get; set; }
        public decimal ProjectApprovalStatusID { get; set; }
        public Nullable<decimal> FollowUpStatus { get; set; }
        public Nullable<decimal> ReportTrackingNo { get; set; }
        public Nullable<decimal> AlertNo { get; set; }
        public decimal ProvinceID { get; set; }
        public decimal OrganizationID { get; set; }
        public string OrganizationNameTH { get; set; }
        public string OrganizationNameEN { get; set; }
        public Nullable<decimal> OrganizationTypeID { get; set; }
        public string OrganizationTypeEtc { get; set; }
        public string OrganizationYear { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public Nullable<decimal> SubDistrictID { get; set; }
        public string SubDistrict { get; set; }
        public Nullable<decimal> DistrictID { get; set; }
        public string District { get; set; }
        public decimal AddressProvinceID { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
        public string CurrentProject { get; set; }
        public string CurrentProjectResult { get; set; }
        public string GotSupportFlag { get; set; }
        public string GotSupportYear { get; set; }
        public Nullable<decimal> GotSupportTimes { get; set; }
        public string GotSupportLastProject { get; set; }
        public string GotSupportLastResult { get; set; }
        public string GotSupportLastProblems { get; set; }
        public string SourceName1 { get; set; }
        public Nullable<decimal> MoneySupport1 { get; set; }
        public string SourceName2 { get; set; }
        public Nullable<decimal> MoneySupport2 { get; set; }
        public string SourceName3 { get; set; }
        public Nullable<decimal> MoneySupport3 { get; set; }
        public string SourceName4 { get; set; }
        public Nullable<decimal> MoneySupport4 { get; set; }
        public Nullable<decimal> BudgetValue { get; set; }
        public string BudgetFromOtherFlag { get; set; }
        public string BudgetFromOtherName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> BudgetReviseValue { get; set; }
        public string OrgUnderSupport { get; set; }
        public Nullable<decimal> BudgetFromOtherAmount { get; set; }
        public Nullable<System.DateTime> OrgEstablishedDate { get; set; }
        public string Mobile { get; set; }
        public string ToGotSupportYear { get; set; }
        public Nullable<System.DateTime> SubmitedDate { get; set; }
        public Nullable<System.DateTime> LastedFollowupDate { get; set; }
        public decimal CreatedByID { get; set; }
        public Nullable<decimal> UpdatedByID { get; set; }
        public string RESPONSEFIRSTNAME { get; set; }
        public string RESPONSETEL { get; set; }
        public string RESPONSEEMAIL { get; set; }
        public string RESPONSELASTNAME { get; set; }
        public string RESPONSEPOSITION { get; set; }
    
        public virtual ProjectApproval ProjectApproval { get; set; }
        public virtual ICollection<ProjectBudget> ProjectBudgets { get; set; }
        public virtual ICollection<ProjectCommittee> ProjectCommittees { get; set; }
        public virtual ProjectContract ProjectContract { get; set; }
        public virtual ProjectDocument ProjectDocument { get; set; }
        public virtual ProjectEvaluation ProjectEvaluation { get; set; }
        public virtual ProjectFollowup ProjectFollowUp { get; set; }
        public virtual ICollection<ProjectLog> ProjectLogs { get; set; }
        public virtual ProjectOperation ProjectOperation { get; set; }
        public virtual ProjectPersonel ProjectPersonel { get; set; }
        public virtual ProjectReport ProjectReport { get; set; }
        public virtual ICollection<ProjectPrintReportTracking> ProjectPrintReportTrackings { get; set; }
        public virtual ICollection<ProjectTargetGroup> ProjectTargetGroups { get; set; }
        public virtual ICollection<MT_Attachment> MT_Attachment { get; set; }
        public virtual MT_ListOfValue ProjectApprovalStatus { get; set; }
        public virtual ProjectInformation ProjectInformation { get; set; }
        public virtual ICollection<ProjectParticipant> ProjectParticipants { get; set; }
        public virtual ICollection<ProjectOperationAddress> PROJECTOPERATIONADDRESSes { get; set; }
        public virtual ICollection<PROJECTDOCUMENTLIST> PROJECTDOCUMENTLISTs { get; set; }
        public virtual PROJECTFOLLOWUP2 PROJECTFOLLOWUP2 { get; set; }
    }
}
