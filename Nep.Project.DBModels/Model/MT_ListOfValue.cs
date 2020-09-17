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
    
    public partial class MT_ListOfValue
    {
        public MT_ListOfValue()
        {
            this.Attachment = new HashSet<MT_Attachment>();
            this.Province = new HashSet<MT_Province>();
            this.ProjectApprovals = new HashSet<ProjectApproval>();
            this.ProjectApprovals1 = new HashSet<ProjectApproval>();
            this.ProjectInformations = new HashSet<ProjectInformation>();
            this.ProjectPersonelPrefix1 = new HashSet<ProjectPersonel>();
            this.ProjectPersonelPrefix2 = new HashSet<ProjectPersonel>();
            this.ProjectPersonelPrefix3 = new HashSet<ProjectPersonel>();
            this.ProjectGeneralInfoes = new HashSet<ProjectGeneralInfo>();
            this.ProjectEvaluations = new HashSet<ProjectEvaluation>();
            this.ProjectParticipants = new HashSet<ProjectParticipant>();
            this.ProjectPrintReportTrackings = new HashSet<ProjectPrintReportTracking>();
            this.ProjectApprovalBudgetTypes = new HashSet<ProjectApproval>();
            this.LOG_ACCESS = new HashSet<LOG_ACCESS>();
        }
    
        public decimal LOVID { get; set; }
        public string LOVCode { get; set; }
        public string LOVName { get; set; }
        public string LOVGroup { get; set; }
        public decimal OrderNo { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ICollection<MT_Attachment> Attachment { get; set; }
        public virtual ICollection<MT_Province> Province { get; set; }
        public virtual ICollection<ProjectApproval> ProjectApprovals { get; set; }
        public virtual ICollection<ProjectApproval> ProjectApprovals1 { get; set; }
        public virtual ICollection<ProjectInformation> ProjectInformations { get; set; }
        public virtual ICollection<ProjectPersonel> ProjectPersonelPrefix1 { get; set; }
        public virtual ICollection<ProjectPersonel> ProjectPersonelPrefix2 { get; set; }
        public virtual ICollection<ProjectPersonel> ProjectPersonelPrefix3 { get; set; }
        public virtual ICollection<ProjectGeneralInfo> ProjectGeneralInfoes { get; set; }
        public virtual ICollection<ProjectEvaluation> ProjectEvaluations { get; set; }
        public virtual ICollection<ProjectParticipant> ProjectParticipants { get; set; }
        public virtual ICollection<ProjectPrintReportTracking> ProjectPrintReportTrackings { get; set; }
        public virtual ICollection<ProjectApproval> ProjectApprovalBudgetTypes { get; set; }
        public virtual ICollection<LOG_ACCESS> LOG_ACCESS { get; set; }
    }
}
