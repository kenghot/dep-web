﻿//------------------------------------------------------------------------------
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
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NepProjectDBEntities : DbContext
    {
        private static String GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Common.Constants.CONNECTION_STRING_NAME];
            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder();
            builder.Metadata = @"res://*/Model.NepProjectDB.csdl|res://*/Model.NepProjectDB.ssdl|res://*/Model.NepProjectDB.msl";
            builder.Provider = connectionString.ProviderName;
            builder.ProviderConnectionString = connectionString.ConnectionString;
            return builder.ConnectionString;
        }
    
        public NepProjectDBEntities()
            : base(GetConnectionString())
        {
            this.Database.Connection.StateChange += delegate(object sender, System.Data.StateChangeEventArgs e)
            {
                if (e.CurrentState == System.Data.ConnectionState.Open)
                {
                    this.Database.ExecuteSqlCommand("ALTER SESSION SET NLS_SORT=BINARY_CI");
                    this.Database.ExecuteSqlCommand("ALTER SESSION SET NLS_COMP=LINGUISTIC");
                }
            };
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MT_Attachment> MT_Attachment { get; set; }
        public virtual DbSet<MT_District> MT_District { get; set; }
        public virtual DbSet<MT_ListOfValue> MT_ListOfValue { get; set; }
        public virtual DbSet<MT_Organization> MT_Organization { get; set; }
        public virtual DbSet<MT_OrganizationType> MT_OrganizationType { get; set; }
        public virtual DbSet<MT_Province> MT_Province { get; set; }
        public virtual DbSet<MT_SubDistrict> MT_SubDistrict { get; set; }
        public virtual DbSet<OrganizationCommittee> OrganizationCommittees { get; set; }
        public virtual DbSet<ProjectApproval> ProjectApprovals { get; set; }
        public virtual DbSet<ProjectBudget> ProjectBudgets { get; set; }
        public virtual DbSet<ProjectCommittee> ProjectCommittees { get; set; }
        public virtual DbSet<ProjectContract> ProjectContracts { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public virtual DbSet<ProjectEvaluation> ProjectEvaluations { get; set; }
        public virtual DbSet<ProjectFollowup> ProjectFollowups { get; set; }
        public virtual DbSet<ProjectGeneralInfo> ProjectGeneralInfoes { get; set; }
        public virtual DbSet<ProjectInformation> ProjectInformations { get; set; }
        public virtual DbSet<ProjectLog> ProjectLogs { get; set; }
        public virtual DbSet<ProjectOperation> ProjectOperations { get; set; }
        public virtual DbSet<ProjectPersonel> ProjectPersonels { get; set; }
        public virtual DbSet<ProjectPrintReportTracking> ProjectPrintReportTrackings { get; set; }
        public virtual DbSet<ProjectReport> ProjectReports { get; set; }
        public virtual DbSet<ProjectTargetGroup> ProjectTargetGroups { get; set; }
        public virtual DbSet<SC_Function> SC_Function { get; set; }
        public virtual DbSet<SC_Group> SC_Group { get; set; }
        public virtual DbSet<SC_User> SC_User { get; set; }
        public virtual DbSet<SC_UserAccess> SC_UserAccess { get; set; }
        public virtual DbSet<MT_OrganizationParameter> MT_OrganizationParameter { get; set; }
        public virtual DbSet<View_ProjectList> View_ProjectList { get; set; }
        public virtual DbSet<UserRegisterEntry> UserRegisterEntries { get; set; }
        public virtual DbSet<MT_RunningNumber> MT_RunningNumbers { get; set; }
        public virtual DbSet<ProjectParticipant> ProjectParticipants { get; set; }
        public virtual DbSet<MT_Template> MT_Template { get; set; }
        public virtual DbSet<OrganizationRegisterEntry> OrganizationRegisterEntries { get; set; }
        public virtual DbSet<View_BudgetSummaryByOrgType> View_BudgetSummaryByOrgType { get; set; }
        public virtual DbSet<ProjectOperationAddress> ProjectOperationAddresses { get; set; }
        public virtual DbSet<View_ParticipantProvinceDup> View_ParticipantProvinceDup { get; set; }
        public virtual DbSet<K_FILEINTABLE> K_FILEINTABLE { get; set; }
        public virtual DbSet<K_MT_POSITION> K_MT_POSITION { get; set; }
        public virtual DbSet<PROJECTFOLLOWUP2> PROJECTFOLLOWUP2 { get; set; }
        public virtual DbSet<PROJECTQUESTION> PROJECTQUESTIONs { get; set; }
        public virtual DbSet<PROJECTQUESTIONHD> PROJECTQUESTIONHDs { get; set; }
        public virtual DbSet<PROJECTHISTORY> PROJECTHISTORies { get; set; }
        public virtual DbSet<PROJECTPROCESSED> PROJECTPROCESSEDs { get; set; }
        public virtual DbSet<PROJECTBUDGETACTIVITY> PROJECTBUDGETACTIVITies { get; set; }
        public virtual DbSet<LOG_ACCESS> LOG_ACCESS { get; set; }
    
        public virtual int ClearUserAccess()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearUserAccess");
        }
    }
}
