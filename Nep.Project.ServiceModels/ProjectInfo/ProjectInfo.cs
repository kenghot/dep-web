using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Nep.Project.Resources;
using Nep.Project.DBModels;
using Nep.Project.DBModels.Model;
using Nep.Project.ServiceModels.API.Responses;

namespace Nep.Project.ServiceModels.ProjectInfo
{
    public class OrganizationInfo
    {
        public Decimal ProjectID { get; set; }

        //-- ข้อมูลทั่วไป --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal ProvinceID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal OrganizationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(1000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrganizationNameTH", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationNameTH { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrganizationNameEN", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationNameEN { get; set; }

        //-- องค์กรของท่านจัดอยู่ในประเภทใด --//
        [Display(Name = "ProjectInfo_OrganizationGovType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? OrganizationTypeID { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrganizationPersonType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationTypeEtc { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrgUnderSupport", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrgUnderSupport { get; set; }        

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(4, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_RegisterYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationYear { get; set; }

        [Display(Name = "ProjectInfo_RegisterDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? OrgEstablishedDate { get; set; }
        

        //-- ที่ตั้งสำนักงาน --//
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_AddressNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building { get; set; }

        [StringLength(10, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Street", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SubDistrictID { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String SubDistrict { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? DistrictID { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String District { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal AddressProvinceID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(10, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Postcode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Postcode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Telephone { get; set; }

        [Display(Name = "ProjectInfo_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Mobile { get; set; }

        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Fax", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Fax { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Email { get; set; }

        //-- วัตถุประสงค์ขององค์กร --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrganizationObjective", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Purpose { get; set; }

        //-- กิจกรรมหรือโครงการที่องค์กรดำเนินอยู่ในปัจจุบัน --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ActivityCurrent", ResourceType = typeof(Nep.Project.Resources.Model))]       
        public String CurrentProject { get; set; }

        //-- ผลงานในรอบ ๑ ปี ที่ผ่านมา (โดยสรุป) --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_WorkingYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String CurrentProjectResult { get; set; }

        //-- องค์กรของท่านเคยเสนอโครงการขอรับการสนับสนุน --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_IsfirstFlag", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Boolean GotSupportFlag { get; set; }

        //-- (ถ้าเคย) ปีขอรับการสนับสนุน --//
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(4, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_PromoteYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String GotSupportYear { get; set; }


        //-- ถึง ปีขอรับการสนับสนุน --//
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(4, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_PromoteYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String TogotSupportYear { get; set; }
          

        //-- จำนวนครั้งที่ขอรับการสนับสนุน --//
        [Display(Name = "ProjectInfo_PromoteAmount", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? GotSupportTimes { get; set; }

        //-- โครงการล่าสุดที่เคยขอรับการสนันสนุนชื่อโครงการ --//
        //[StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectLasted", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String GotSupportLastProject { get; set; }

        //-- ผลของโครงการ --//
        [Display(Name = "ProjectInfo_ProjectLastedResult", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String GotSupportLastResult { get; set; }

        //-- ปัญหาและอุปสรรค --//
        [Display(Name = "ProjectInfo_Problem", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String GotSupportLastProblems { get; set; }

        //-- แหล่งขอความช่วยเหลือที่องค์กรได้รับ --//
        [Display(Name = "ProjectInfo_Assistances", ResourceType = typeof(Nep.Project.Resources.Model))]
        public List<ServiceModels.ProjectInfo.OrganizationAssistance> Assistances { get; set; }

        //-- รายชื่อคณะกรรมการ --//
        [Display(Name = "ProjectInfo_Committees", ResourceType = typeof(Nep.Project.Resources.Model))]
        public List<ServiceModels.ProjectInfo.Committee> Committees { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }

        //-- สถานะ Project --//
        public Decimal? ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String ProjectApprovalStatusName { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }
       
        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }
    }

    public class OrganizationAssistance
    {
        public String No { get; set; }

        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_OrganizationNameHelp", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String OrganizationName { get; set; }

        [Display(Name = "ProjectInfo_AssistanceAmount", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? Amount { get; set; }
    }

    [Serializable]
    public class Committee
    {
        public String UID { get; set; }

        public Decimal OrganizationCommitteeID { get; set; }

        public Decimal OrganizationID { get; set; }

        public Decimal? No { get; set; }

        public String CommitteePosition { get; set; }

        [Display(Name = "ProjectInfo_MemberName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String MemberName { get; set; }

        [Display(Name = "ProjectInfo_MemberSurname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String MemberSurname { get; set; }

        [Display(Name = "ProjectInfo_MemberPosition", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String MemberPosition { get; set; }
        //kenghot
        public string PositionCode { get; set; }
        public String PositionName { get; set; }
               
    }

    public class Position
    {
        public Int32 PositionID { get; set; }

        public String PositionName { get; set; }
    }

    public class TabProjectInfo
    {
        public Decimal ProjectID {get;set;}

        //-- ชื่อโครงการ --//
        //[Display(Name = "ProjectInfo_Name", ResourceType = typeof(Nep.Project.Resources.Model))]
        //public Int32 ProjectInfoID { get; set; }

        public Decimal? ProvinceID { get; set; }

        //-- หมายเลขทะเบียนโครงการ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(11, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectNo1", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectNo { get; set; }

        [Display(Name = "ProjectInfo_BudgetYear", ResourceType = typeof(Nep.Project.Resources.Model))]
        public decimal BudgetYear { get; set; }

        //-- ชื่อโครงการภาษาไทย --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoNameTH", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectInfoNameTH { get; set; }

        //-- ชื่อโครงการภาษาอังกฤษ (ถ้ามี) --//
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoNameEN", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectInfoNameEN { get; set; }

        //-- ประเภทโครงการ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoType", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? ProjectInfoType { get; set; }

        //-- เมื่อวันที่ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoStartDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime ProjectInfoStartDate { get; set; }

        //-- ประเภทความพิการที่ขอรับการสนับสนุน --//
        [Display(Name = "ProjectInfo_TypeDisabilitys", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? TypeDisabilitys { get; set; }

        //-- หลักการและเหตุผล --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Principles", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Principles { get; set; }

        //-- วัตถุประสงค์ของโครงการ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoObjective", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectInfoObjective { get; set; }

        //-- กลุ่มเป้าหมายของโครงการ --//
        //[Display(Name = "ProjectInfo_ProjectInfoTarget", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[StringLength(4000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        //public String ProjectInfoTarget { get; set; }

        //-- ตัวชี้วัดโครงการ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoindicator", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectInfoindicator { get; set; }

        //-- ผลที่คาดว่าจะได้รับ --//
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_ProjectInfoAnticipation", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectInfoAnticipation { get; set; }


        //-- สถานะ Project --//
        public Decimal? ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String ProjectApprovalStatusName { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }
        public String FollowupStatusCode { get; set; }


        public Decimal? CreatorOrganizationID { get; set; }

        public Decimal? CancelledAttachmentID { get; set; }
        public KendoAttachment CancelledAttachment { get; set; }
        
        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }

        public Decimal ProjectOrganizationID { get; set; }
        /// <summary>
        /// วันที่ส่งคำร้อง
        /// </summary>
        public DateTime? SubmitedDate { get; set; }
        /// <summary>
        /// ข้อแนะนำให้องค์ปรับแก้
        /// </summary>
        public String RejectComment { get; set; }
      
        public string RejectTopic { get; set; }
        public List<Common.ProjectFunction> ProjectRole { get; set; }

        public bool HasEvaluationInfo { get; set; }
        public bool HasApprovalInfo { get; set; }
        //kenghot18
        public decimal? BudgetValue { get; set; }
    }

    public class TypeDisability
    {
        public Int32 TypeDisabilityID { get; set; }

        public String TypeDisabilityName { get; set; }
    }
   
    public class TabAttachment
    {
        public decimal ProjectID {get;set;}
                
        public decimal OrganizationID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }

        //-- เอกสารแนบ --//       
        public List<AttachmentProvide> Attachments { get; set; }

        //-- ผู้รับผิดชอบโครงการ --//
        [Display(Name = "ProjectInfo_ResponsibleProject", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ResponsibleProject { get; set; }

        //-- ผู้เสนอโครงการ --//
        [Display(Name = "ProjectInfo_ProposerProject", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProposerProject { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }

        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }
    }

    public class ProjectDocument 
    {
        public decimal ProjectID { get; set; }

        public decimal DocumentID1 { get; set; }
        public KendoAttachment AddedDocument1 { get; set; }
        public KendoAttachment RemovedDocument1 { get; set; }

        public decimal? DocumentID2 { get; set; }
        public KendoAttachment AddedDocument2 { get; set; }
        public KendoAttachment RemovedDocument2 { get; set; }

        public decimal? DocumentID3 { get; set; }
        public KendoAttachment AddedDocument3 { get; set; }
        public KendoAttachment RemovedDocument3 { get; set; }

        public decimal? DocumentID4 { get; set; }
        public KendoAttachment AddedDocument4 { get; set; }
        public KendoAttachment RemovedDocument4 { get; set; }

        public decimal? DocumentID5 { get; set; }
        public KendoAttachment AddedDocument5 { get; set; }
        public KendoAttachment RemovedDocument5 { get; set; }

        public decimal? DocumentID6 { get; set; }
        public KendoAttachment AddedDocument6 { get; set; }
        public KendoAttachment RemovedDocument6 { get; set; }

        public decimal? DocumentID7 { get; set; }
        public KendoAttachment AddedDocument7 { get; set; }
        public KendoAttachment RemovedDocument7 { get; set; }

        public decimal? DocumentID8 { get; set; }
        public KendoAttachment AddedDocument8 { get; set; }
        public KendoAttachment RemovedDocument8 { get; set; }

        public decimal? DocumentID9 { get; set; }
        public KendoAttachment AddedDocument9 { get; set; }
        public KendoAttachment RemovedDocument9 { get; set; }

        public decimal? DocumentID10 { get; set; }
        public KendoAttachment AddedDocument10 { get; set; }
        public KendoAttachment RemovedDocument10 { get; set; }

        public decimal? DocumentID11 { get; set; }
        public KendoAttachment AddedDocument11 { get; set; }
        public KendoAttachment RemovedDocument11 { get; set; }

        public decimal? DocumentID12 { get; set; }
        public KendoAttachment AddedDocument12 { get; set; }
        public KendoAttachment RemovedDocument12 { get; set; }

        public decimal? DocumentID13 { get; set; }
        public KendoAttachment AddedDocument13 { get; set; }
        public KendoAttachment RemovedDocument13 { get; set; }

        public decimal? DocumentID14 { get; set; }
        public KendoAttachment AddedDocument14 { get; set; }
        public KendoAttachment RemovedDocument14 { get; set; }
        //kenghot
        public List<List<KendoAttachment>> AddedDocuments { get; set; }
        public List<List<KendoAttachment>> RemovedDocuments { get; set; }
        //end kenghot
    }

    [Serializable]
    public class AttachmentProvide
    {
        public Decimal ProjectID { get; set; }
        public Decimal DocumentNo { get; set; }
        public String DocumentProvideName { get; set; }
        public Decimal? AttachmentID { get; set; }
        public String AttachmentFileName { get; set; }
        public String AttachmentPathName { get; set; }
        public Decimal? AttachmentFileSize { get; set; }
        /// <summary>
        /// kenghot
        /// </summary>
        public List<KendoAttachment> AttachFiles { get; set; }
    }

    public class AssessmentProject
    {
        public Decimal ProjectID { get; set; }
        public decimal OrganizationID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }

        [Display(Name = "ProjectInfo_AssessmentProvince", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? AssessmentProvinceID { get; set; }
        public String AssessmentProvinceName { get; set; }

        public String OrganizationNameTH { get; set; }
        public String OrganizationNameEN { get; set; }
        public String OrganizationName { 
            get {
                String name = OrganizationNameTH;
                if(!String.IsNullOrEmpty(OrganizationNameEN)){
                    name = String.Format("{0} ({1})", name, OrganizationNameEN);
                }
                return name;
            } 
        }

        [Display(Name = "ProjectInfo_RegistrationNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProjectNo { get; set; }

        public String ProjectNameTH { get; set; }
        public String ProjectNameEN { get; set; }
        public String ProjectName {
            get
            {
                String name = ProjectNameTH;
                if (!String.IsNullOrEmpty(ProjectNameEN))
                {
                    name = String.Format("{0} ({1})", name, ProjectNameEN);
                }
                return name;
            }
        }

        [Display(Name = "ProjectInfo_BudgetRequest", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? BudgetRequest { get; set; }

        [Display(Name = "ProjectInfo_CriterionNo4", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Boolean? IsPassAss4 { get; set; }

        [Display(Name = "ProjectInfo_CriterionNo5", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Boolean? IsPassAss5 { get; set; }

        public Decimal? Assessment61 { get; set; }
        public Decimal? Assessment62 { get; set; }
        public Decimal? Assessment63 { get; set; }
        public Decimal? Assessment64 { get; set; }
        public Decimal? Assessment65 { get; set; }
        public Decimal? Assessment66 { get; set; }
        public Decimal? Assessment67 { get; set; }
        public Decimal? Assessment68 { get; set; }
        public Decimal? Assessment69 { get; set; }
        public Decimal? Assessment610 { get; set; }
        public Decimal? Assessment611 { get; set; }

        public Decimal? TotalScore { get; set; }
        public String TotalScoreDesc { get; set; }

        [Display(Name = "ProjectInfo_CommentOther", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String AssessmentDesc { get; set; }

        //-- ยุทธศาสตร์ --//
        public Boolean? IsPassMission1 { get; set; }
        public Boolean? IsPassMission2 { get; set; }
        public Boolean? IsPassMission3 { get; set; }
        public Boolean? IsPassMission4 { get; set; }
        public Boolean? IsPassMission5 { get; set; }
        public Boolean? IsPassMission6 { get; set; }

        [Display(Name = "ProjectInfo_StrategicProvice", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProvinceMissionDesc { get; set; }

        public Decimal? EvaluationStatusID { get; set; }
        public String EvaluationScoreDesc { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }
        public string IPAddress { get; set; }
        public string ExtendJSON { get; set; }
        public AssessmentExtend ExtendData { get; set; }
    }
    public class AssessmentExtend
    {
        public string rd6_1 { get; set; }
        public string txt6_1 { get; set; }
        public string rd6_2_1 { get; set; }
        public string txt6_2_1 { get; set; }
        public string rd6_2_2 { get; set; }
        public string txt6_2_2 { get; set; }
        public string rd6_3 { get; set; }
        public string txt6_3 { get; set; }
        public string rd6_4 { get; set; }
        public string txt6_4 { get; set; }
        public string rd6_5 { get; set; }
        public string txt6_5 { get; set; }
        public string rd6_6 { get; set; }
        public string txt6_6 { get; set; }
        public string rd6_7_1 { get; set; }
        public string txt6_7_1 { get; set; }
        public string rd6_7_2 { get; set; }
        public string txt6_7_2 { get; set; }
        public string rd6_7_3 { get; set; }
        public string txt6_7_3 { get; set; }
        public string rd6_8 { get; set; }
        public string txt6_8 { get; set; }
        public string rd6_9_1 { get; set; }
        public string txt6_9_1 { get; set; }
        public string rd6_9_2 { get; set; }
        public string txt6_9_2 { get; set; }
        public string rd6_10_1 { get; set; }
        public string txt6_10_1 { get; set; }
        public string rd6_10_2 { get; set; }
        public string txt6_10_2 { get; set; }
        public string rd6_11 { get; set; }
        public string txt6_11 { get; set; }

    }

    public class StandardCriterionNo6
    {
        public Int32 AssessmentProjectID { get; set; }

        public Int16 StandardID { get; set; }

        public String OrderNo { get; set; }

        public String StandardName { get; set; }

        public Int32 Score { get; set; }

        public ICollection<GenericDropDownListData> ScoreList { get; set; }
    }

    public class StandardStrategic
    {
        public Int32 StractegicID { get; set; }

        public String StractegicName { get; set; }
    }

    public class ProjectBudget
    {
        public Decimal ProjectID { get; set; }
        public Decimal OrganizationID { get; set; }

        [Display(Name = "ProjectBudget_TotalAmount", ResourceType = typeof(Nep.Project.Resources.Model))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal? TotalRequestAmount { get; set; }

        public Decimal? TotalReviseAmount { get; set; }

        public Boolean? IsBudgetGotSupport { get; set; }

        private string _budgetGotSupportName = null;
        public String BudgetGotSupportName
        {
            get { return _budgetGotSupportName; }
            set
            {
                if (IsBudgetGotSupport.HasValue && (IsBudgetGotSupport == true))
                {
                    _budgetGotSupportName = value;
                }
                else
                {
                    _budgetGotSupportName = null;
                }
            }
        }
        public decimal? BudgetGotSupportAmount { get; set; }
        
        public ICollection<BudgetDetail> BudgetDetails { get; set; }

        //-- สถานะ Project --//
        public Decimal? ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String ProjectApprovalStatusName { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }

        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }

        /// <summary>
        /// ขอโครงการจากส่วนกลางใช่หรือไม่
        /// </summary>
        public bool IsRequestCenter { get; set; }
       
        //kenghot18
        public List<ServiceModels.ProjectInfo.BudgetActivity> BudgetActivities { get; set; }
        public decimal? ActivityID { get; set; }
        public decimal? ReviseBudgetAmount { get; set; }
        public decimal? Interest { get; set; }
    }
    [Serializable]
    public class ProjectReportScreen
    {
        public ProjectBudgetAmount GrandTotal { get; set; }
        public decimal? Interest { get; set; }
        public decimal? TotalBalance { get; set; }
        public decimal? Balance { get; set; }
        public ProjectBudgetAmount[] Summary { get; set; }
        
    }
    [Serializable]
    public class ProjectBudgetAmount
    {
        public decimal? Amount { get; set; }
        public decimal? ReviseAmount { get; set; }
        public decimal? Revise1Amount { get; set; }
        public decimal? Revise2Amount { get; set; }
        public decimal? ActualExpense { get; set; }
    }


[Serializable]
    public class BudgetDetail
    {
        public Decimal ProjectBudgetID { get; set; }

        public string UID { get; set; }

        public Int32? No { get; set; }

        [Display(Name = "BudgetDetail_Detail", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        //[StringLength(1000, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String Detail { get; set; }

        [Display(Name = "BudgetDetail_Amount", ResourceType = typeof(Nep.Project.Resources.Model))]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        //[Range(1, 999999, ErrorMessageResourceName = "ValidationRangeTypeNumber", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal? Amount { get; set; }

        public String ReviseDetail { get; set; }
        public Decimal? ReviseAmount { get; set; }
        public String ReviseRemark { get; set; }

        public Decimal? Revise1Amount { get; set; }
        public Decimal? Revise2Amount { get; set; }
        public Decimal? ActualExpense { get; set; }
        public String ApprovalRemark { get; set; }
        public string BudgetCode { get; set; }
        public decimal? ActivityID { get; set; }
    }
    [Serializable]
    public class BudgetActivity
    {
        public Decimal? ActivityID { get; set; }
        public Decimal? ProjectID { get; set; }
        public long RunNo { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDESC { get; set; }
        public Decimal? TotalAmount { get; set; }
        public DateTime? CreateDate { get; set; }

    }

    [Serializable]
    public class ProcessingServiceProvider
    {
        public string UID { get; set; }

        public Int32 BudgetDetialID { get; set; }
        public Int32 ProjectBudgetID { get; set; }

        public Int32? No { get; set; }

        public String ServiceProviderName { get; set; }

        public String ServiceProviderSurName { get; set; }

        public String ServiceProviderIDNo { get; set; }

        public ICollection<GenericDropDownListData> SexList { get; set; }

        //public String SecretaryDetail { get; set; }
        //public Decimal? SecretaryAmount { get; set; }
        //public String SecretaryRemark { get; set; }

        //public Decimal? ProvinceCommitteeAmount { get; set; }

        //public Decimal? DiscriminationTeamAmount { get; set; }
        //public Decimal? SubcommitteeAmount { get; set; }

        //public Decimal? CommittreeAmount { get; set; }
    }

    [Serializable]
    public class ProjectInfoList
    {
        public Decimal ProjectInfoID { get; set; }
        public String ProjectNo { get; set; }
        public String ProjectName { get; set; }
        public String FollowupStatusName { get; set; }
        public Decimal? FollowupStatusID { get; set; }
        public String ProjectNameDesc
        {
            get
            {
                String desc = this.ProjectName;
                if ((this.IsFollowup.HasValue) && (this.IsFollowup == true))
                {
                    desc += String.Format("<span class='alert-folloup-desc'> ({0}) </span>", FollowupStatusName);
                }

                if ((this.IsCancelContract.HasValue) && (this.IsCancelContract == true))
                {
                    desc += String.Format("<span class='alert-cancelcontract-desc'> ({0}) </span>", Nep.Project.Resources.UI.LabelCancelContractStatus);
                }

                if (!String.IsNullOrEmpty(this.ApprovalStatus) && (this.ApprovalStatus == "0"))
                {
                    desc += String.Format("<span class='alert-cancelcontract-desc'> ({0}) </span>", Nep.Project.Resources.UI.LabelNotApprovalStatus);
                }

                if (this.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกคำร้อง)
                {
                    desc += String.Format("<span class='alert-cancelcontract-desc'> ({0}) </span>", Nep.Project.Resources.UI.LabelCancelledProjectRequest);
                }

                if (this.IsReject)
                {
                    desc += String.Format("<span class='alert-reject-desc'> ({0}) </span>", Nep.Project.Resources.UI.LabelRejectedProject);
                }
                if (this.IsReportRevise)
                {
                    desc += String.Format("<span class='alert-folloup-desc'> ({0}) </span>", "ส่งแก้ไขผลปฎิบัติงาน");
                }
                if (this.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอน_6_1_รอโอนเงิน)
                {
                    desc += String.Format("<span class='alert-cancelcontract-desc'> ({0}) </span>", "รอโอนเงิน");
                }
                return desc;
            }
        }


        public Decimal? ProjectTypeID { get; set; }        

        public Decimal OrganizationID { get; set; }
        public Decimal? OrganizationTypeID { get; set; }
        public String OrganizationToBeUnder { get; set; }
        public String OrganizationName { get; set; }

        public Decimal ProvinceID { get; set; }
        public String ProvinceName { get; set; }
        public String ProvinceAbbr { get; set; }

        public Decimal? BudgetYear { get; set; }
        public Int32 BudgetYearThai
        {
            get
            {
                if (this.BudgetYear.HasValue)
                {
                    DateTime date = new DateTime((Int32)BudgetYear, 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                    return Convert.ToInt32(date.ToString("yyyy", Common.Constants.UI_CULTUREINFO));
                }else
                {
                    return 0;
                }
                
            }
        }
        public Decimal? BudgetValue { get; set; }
        public Decimal? BudgetReviseValue { get; set; }


        public Decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }

        public Boolean? IsPassMission1 { get; set; }
        public Boolean? IsPassMission2 { get; set; }
        public Boolean? IsPassMission3 { get; set; }
        public Boolean? IsPassMission4 { get; set; }
        public Boolean? IsPassMission5 { get; set; }

        public Boolean? IsStep1Approved { get; set; }
        public Boolean? IsStep2Approved { get; set; }
        public Boolean? IsStep3Approved { get; set; }
        public Boolean? IsStep4Approved { get; set; }
        public Boolean? IsStep5Approved { get; set; }
        public Boolean? IsStep6Approved { get; set; }

        /// <summary>
        /// 1 = approved
        /// 0 = not approved
        /// null = nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Boolean? IsFollowup { get; set; }

        public Boolean? IsCancelContract
        {
            get
            {
                bool isAble = (this.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา) ? true : false;

                return isAble;
            }
        }

        public Boolean IsDeletable { 
            get {
                bool isAble = (this.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) ? true : false ;
                if (isAble)
                {
                    isAble = ((CreatorOrganizationID.HasValue && (CreatorOrganizationID == SearcherOrganizationID)) || (CreatorOrganizationID == null && (IsCreateByOfficer)) );
                }               
               
                return isAble;
            } 
        }


        public Decimal? DisabilityTypeID { get; set; }

        public String SearcherName { get; set; }
        public Decimal? SearcherOrganizationID { get; set; }        

        public Decimal? SearcherGroupID { get; set; }
        public String SearcherGroupCode { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? CreatorProvinceID { get; set; }
        public String CreatedBy { get; set; }
        public Decimal? CreatedByID { get; set; }

        public Boolean IsCreateByOfficer { get; set; }

        public DateTime? SubmitedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsReject { get; set; }
        //kenghot
        public DateTime? ProjectEndDate { get; set; }
        public DBModels.Model.MT_ListOfValue  ApprovalStatus1 { get; set; }
        public string RecStatus { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public bool IsReportRevise { get; set; }
        public string RejectComment { get; set; }
        public string Acknowledged { get; set; }
        //end kenghot
    }

    [Serializable]
    public class ProjectTarget
    {
        public string UID { get; set; }
        public Decimal ProjectID { get; set; }
        public Decimal ProjectTargetID { get; set; }
        public Decimal TargetID { get; set; }
        public String TargetName { get; set; }
        public String TargetOtherName { get; set; }
        public String TargetDesc
        {
            get
            {
                string desc = "";
                if (!String.IsNullOrEmpty(TargetOtherName))
                {
                    desc = TargetOtherName;
                }
                else
                {
                    desc = TargetName;
                }
                return desc;
            }
        }
        public Decimal? Amount { get; set; }      
        public ListOfValue Target
        {
            get
            {
                ListOfValue item = new ListOfValue { LovID = TargetID, LovName = TargetName};
                return item;
            }
        }
    }

    [Serializable]
    public class ProjectTargetNameList
    {        
        public Decimal? ProjectParticipantsID { get; set; }
        public String TempProjectTargetID { get; set; }
        public Decimal? ProjectTargetID { get; set; }
        public Decimal? TargetID { get; set; }
        public string TargetCode { get; set; }
        public String TargetName { get; set; }
        public String TargetEtc { get; set; }
        public String TargetDesc
        {
            get
            {
                string desc = "";
                if (!String.IsNullOrEmpty(TargetEtc))
                {
                    desc = TargetEtc;
                }
                else
                {
                    desc = TargetName;
                }
                return desc;
            }
        }
        public String LovIsActive { get; set; }
    }
       
    public class ProjectFollowup
    {
        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal ProjectID { get; set; }
        public Decimal OrganizationID { get; set; }
        public Decimal? ApprovalBudget { get; set; }

        public String ProjectName { get; set; }
        public Decimal? ProjectFollowupValue {get; set;}
        public Decimal? Assessment61 {get; set;}       

        public String Reason { get; set; }
        public Decimal? ReasonFollowupValue { get; set; }
        public Decimal? Assessment62 { get; set; }

        public String Objective { get; set; }
        public Decimal? ObjectiveFollowupValue { get; set; }
        public Decimal? Assessment63 { get; set; }

        public String TargetGroup { get; set; }
        public Decimal? TargetGroupFollowupValue { get; set; }
        public Decimal? Assessment64 { get; set; }

        public String Location { get; set; }
        public Decimal? LocationFollowupValue { get; set; }
        public Decimal? Assessment65 { get; set; }

        public String Timing { get; set; }
        public Decimal? TimingFollowupValue { get; set; }
        public Decimal? Assessment66 { get; set; }

        public String OperationMethod { get; set; }
        public Decimal? OperationMethodFollowupValue { get; set; }
        public Decimal? Assessment67 { get; set; }

        public String Budget { get; set; }
        public Decimal? BudgetFollowupValue { get; set; }
        public Decimal? Assessment68 { get; set; }

        public String Expection { get; set; }
        public Decimal? ExpectionFollowupValue { get; set; }
        public Decimal? Assessment69 { get; set; }


        public List<ServiceModels.KendoAttachment> Attachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedAttachments { get; set; }

       
        public Decimal? ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String FollowupStatusCode { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }
        public String ProvinceAbbr { get; set; }
        
       public DBModels.Model.PROJECTFOLLOWUP2 ProjectFollowup2 { get; set; }
        public int? TotalTargetGroup { get; set; }
        public int? TotalParticipant { get; set; }
        public string Period1 { get; set; }
        public string Period2 { get; set; }
    }

    [Serializable]
    public class FollowupTrackingDocumentList
    {
        public int No { get; set; }
        public decimal ProjectID { get; set; }
        public decimal? ReportTrackingID { get; set; }
        public decimal? LastedReportTrackingID { get; set; }
        public decimal ReportTrackingTypeID { get; set; }
        public String ReportTrackingTypeName { get; set; }
        public DateTime? ReportDate { get; set; }       

        public string ReportNo { get; set; }
       
        public decimal? LetterAttchmentID { get; set; }
        public String LetterAttchmentName { get; set; }
        public decimal? LetterSize { get; set; }  
     
        public bool IsPdf{
            get
            {
                bool isPdf = false;
                if(!String.IsNullOrEmpty(LetterAttchmentName)){
                    string ext = System.IO.Path.GetExtension(LetterAttchmentName);
                    isPdf = (ext.ToLower() == ".pdf");
                }

                return isPdf;
            }
        }

        public bool IsDeletable
        {
            get
            {
                bool isDel = true;
                if((ReportTrackingID.HasValue && LastedReportTrackingID.HasValue) && 
                    (ReportTrackingID != LastedReportTrackingID)){
                        isDel = false;
                }
                return isDel;
            }
        }
    }
    
    public class FollowupTrackingDocumentForm
    {
        public int No { get; set; }
        public decimal ProjectID { get; set; }
        public decimal? ReportTrackingID { get; set; }
        public decimal ReportTrackingTypeID { get; set; }
        public String ReportTrackingTypeCode { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? DeadlineResponseDate { get; set; }

        public string ReportNo { get; set; }
        public string ReferenceInfo { get; set; }
        public string ReferenceInfo1 { get; set; }
        public decimal? LetterAttchmentID { get; set; }
        public string LetterAttchmentName { get; set; }
        public decimal? LetterAttchmentSize { get; set; }
        public ServiceModels.KendoAttachment LetterAttchment { get; set; }

        public bool IsFirstTracking { get; set; }
        public bool IsCreateFirstTime { get; set; }
        public bool IsEditable { get; set; }
    }

    public class TabPersonal
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public Decimal ProjectID { get; set; }
        public Decimal OrganizationID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_IDCardNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String IDCardNo { get; set; }

        //----------- ผู้รับผิดชอบโครงการ ------------
        public AddressTabPersonal1 Address1 { get; set; }

        //----------- ผู้รับผิดชอบโครงการ (1) ------------
        public AddressTabPersonal2 Address2 { get; set; }

        //----------- ผู้รับผิดชอบโครงการ (2) ------------
        public AddressTabPersonal3 Address3 { get; set; }

        // สถานที่
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportPlace", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportPlace1 { get; set; }

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName1 { get; set; }

        // วิทยาการ จำนวน 
        [Display(Name = "ProjectInfo_InstructorAmt", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? InstructorAmt2 { get; set; }

        // (โปรดแนบรายชื่อ) 
        [Display(Name = "ProjectInfo_InstructorListFileID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? InstructorListFileID2 { get; set; }
        public ServiceModels.KendoAttachment InstructorAttachment { get; set; }

        public ServiceModels.KendoAttachment AddedInstructorAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedInstructorAttachment { get; set; }
        /// <summary>
        /// kenghot
        /// </summary>
        public List<ServiceModels.KendoAttachment> InstructorAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedInstructorAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedInstructorAttachments { get; set; }
        // end kenghot 

        // ชื่อหน่วยงานที่ให้การสนับสนุน
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName2 { get; set; }
        

        // งบประมาณจำนวน
        [Display(Name = "ProjectInfo_SupportBudgetAmt", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SupportBudgetAmt3 { get; set; }

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName3 { get; set; }

        // อุปกรณ์
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportEquipment", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportEquipment4 { get; set; }

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName4 { get; set; }

        // อาหาร - เครื่องดื่ม จำนวน 
        [Display(Name = "ProjectInfo_SupportDrinkFoodAmt", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SupportDrinkFoodAmt5 { get; set; }

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName5 { get; set; }

        // ยานพาหนะ (โปรดแนบรายชื่อ) 
        [Display(Name = "ProjectInfo_VehicleListFile", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? VehicleListFile6 { get; set; }
        public ServiceModels.KendoAttachment VehicleAttachment { get; set; }

        public ServiceModels.KendoAttachment AddedVehicleAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedVehicleAttachment { get; set; }
        /// <summary>
        /// kenghot
        /// </summary>
        public List<ServiceModels.KendoAttachment> VehicleAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedVehicleAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedVehicleAttachments { get; set; }
        // end kenghot

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName6 { get; set; }        

        // อาสาสมัคร จำนวน
        [Display(Name = "ProjectInfo_SupportValunteerAmt", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SupportValunteerAmt7 { get; set; }

        // (โปรดแนบรายชื่อ)
        [Display(Name = "ProjectInfo_ValunteerListFileID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? ValunteerListFileID7 { get; set; }
        public ServiceModels.KendoAttachment ValunteerAttachment { get; set; }
       
        public ServiceModels.KendoAttachment AddedValunteerAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedValunteerAttachment { get; set; }
        /// <summary>
        /// kenghot
        /// </summary>
        public List<ServiceModels.KendoAttachment> ValunteerAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedValunteerAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedValunteerAttachments { get; set; }
        // end kenghot
        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName7 { get; set; }

        // อื่นๆ
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOther", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOther8 { get; set; }

        // ชื่อหน่วยงานที่ให้การสนับสนุน 
        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SupportOrgName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SupportOrgName8 { get; set; }

        //-- สถานะ Project --//
        public Decimal? ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String ProjectApprovalStatusName { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }

        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }

        public Decimal BudgetYear { get; set; }
    }

    public class AddressTabPersonal1
    {
        // คำนำหน้า
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Prefix", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal Prefix1 { get; set; }
        public string PrefixOther { get; set; }
        // ชื่อ
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Firstname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Firstname1 { get; set; }

        // สกุล
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Lastname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Lastname1 { get; set; }

        // บ้านเลขที่
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address1 { get; set; }

        // อาคาร
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building1 { get; set; }

        // หมู่
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo1 { get; set; }

        // ซอย
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi1 { get; set; }

        // ถนน
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road1 { get; set; }

        // ตำบล
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SubDistrictID1 { get; set; }

        // ตำบล
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict1 { get; set; }

        // อำเภอ
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? DistrictID1 { get; set; }

        // อำเภอ
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District1 { get; set; }

        // จังหวัด
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal ProvinceID1 { get; set; }

        // รหัสไปรษณีย์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(10, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String PostCode1 { get; set; }

        // โทรศัพท์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Telephone1 { get; set; }

        [StringLength(30, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Mobile", ResourceType = typeof(Nep.Project.Resources.Model))]        
        public String Mobile1 { get; set; }

        // โทรสาร
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Fax", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Fax1 { get; set; }

        // อีเมล์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Email1 { get; set; }
    }

    public class AddressTabPersonal2
    {
        // คำนำหน้า
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Prefix", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal Prefix2 { get; set; }
        public string PrefixOther { get; set; }
        // ชื่อ
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Firstname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Firstname2 { get; set; }

        // สกุล
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Lastname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Lastname2 { get; set; }

        // บ้านเลขที่
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address2 { get; set; }

        // อาคาร
        [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building2 { get; set; }

        // หมู่
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo2 { get; set; }

        // ซอย
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi2 { get; set; }

        // ถนน
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road2 { get; set; }

        // ตำบล
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SubDistrictID2 { get; set; }

        // ตำบล
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict2 { get; set; }

        // อำเภอ
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? DistrictID2 { get; set; }

        // อำเภอ
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District2 { get; set; }

        // จังหวัด
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal ProvinceID2 { get; set; }

        // รหัสไปรษณีย์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(10, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String PostCode2 { get; set; }

        // โทรศัพท์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(30, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Telephone2 { get; set; }

        // แฟกซ์
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Fax", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Fax2 { get; set; }

        // อีเมล์
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Email2 { get; set; }
    }

    public class AddressTabPersonal3
    {
        // คำนำหน้า
        [Display(Name = "ProjectInfo_Prefix", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? Prefix3 { get; set; }
        public string PrefixOther { get; set; }
        // ชื่อ
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Firstname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Firstname3 { get; set; }

        // สกุล
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Lastname", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Lastname3 { get; set; }

        // บ้านเลขที่
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address3 { get; set; }

        // อาคาร
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building3 { get; set; }

        // หมู่
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo3 { get; set; }

        // ซอย
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi3 { get; set; }

        // ถนน
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road3 { get; set; }

        // ตำบล
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SubDistrictID3 { get; set; }

        // ตำบล
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict3 { get; set; }

        // อำเภอ
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? DistrictID3 { get; set; }

        // อำเภอ
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District3 { get; set; }

        // จังหวัด
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? ProvinceID3 { get; set; }

        // รหัสไปรษณีย์
        [StringLength(10, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_PostCode", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String PostCode3 { get; set; }

        // โทรศัพท์
        [StringLength(30, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Telephone", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Telephone3 { get; set; }

        // แฟกซ์
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Fax", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Fax3 { get; set; }

        // อีเมล์
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Email", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Email3 { get; set; }
    }

    public class TabProcessingPlan
    {
        public Decimal ProjectID { get; set; }
        public Decimal OrganizationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Address { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Building { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Moo { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Soi { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Road { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? SubDistrictID { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String SubDistrict { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? DistrictID { get; set; }

        [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String District { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? ProvinceID { get; set; }

        [Display(Name = "ProcessingPlan_Map", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? LocationMapID { get; set; }
        public ServiceModels.KendoAttachment LocationMapAttachment { get; set; }
        public ServiceModels.KendoAttachment AddedLocationMapAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedLocationMapAttachment { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProcessingPlan_StartDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? StartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProcessingPlan_EndDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? EndDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProcessingPlan_TotalPeriod", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? TotalDay { get; set; }

        public String TimeDesc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "ProcessingPlan_Method", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Method { get; set; }

        //-- สถานะ Project --//
        public Decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public String ProjectApprovalStatusName { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProjectProvinceID { get; set; }
        public Decimal OrgProjectProvinceID { get; set; }

        /// <summary>
        /// 
        /// รายชื่อแท็บที่ยังใส่ข้อมูลไม่ครบ
        /// </summary>
        public List<string> RequiredSubmitData { get; set; }

        public List<ProjectOperationAddress> ProjectOperationAddresses { get; set; }

    }

    [Serializable]
    public partial class ProjectOperationAddress
    {
        public string UID { get; set; }
        public decimal? OperationAddressID { get; set; }
        public decimal ProjectID { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public decimal? SubDistrictID { get; set; }
        public string SubDistrict { get; set; }
        public decimal? DistrictID { get; set; }
        public string District { get; set; }
        public decimal ProvinceID { get; set; }
        public string Province { get; set; }
        public decimal? LocationMapID { get; set; }
        public KendoAttachment LocationMapAttachment { get; set; }
        public KendoAttachment AddedLocationMapAttachment { get; set; }
        public KendoAttachment RemovedLocationMapAttachment { get; set; }

        public string FileName { get; set; }      
        public decimal? FileSize { get; set; }
        public int? Runno { get; set; }
    }
    [Serializable]
    public partial class ProjectProcessed : ProjectOperationAddress
    { 
        public string Description { get; set; }
        public DateTime? ProcessStart { get; set; }
        public DateTime? ProcessEnd { get; set; }
        public decimal ProcessID { get; set; }

        /// <summary>
        /// kenghot
        /// </summary>
        public List<UploadImageResponse> ImageAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedImageAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedImageAttachments { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class TabContract
    {
        public Decimal ProjectID { get; set; }
        public decimal OrganizationID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ContractNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(4, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? ContractYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? ContractDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_StartDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? ContractStartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_EndDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? ContractEndDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_Location", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String Location { get; set; }

        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ViewerName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ViewerName1 { get; set; }

        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ViewerName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ViewerSurname1 { get; set; }

        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ViewerName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ViewerName2 { get; set; }

        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ViewerName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ViewerSurname2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractRefName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String DirectorFirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractRefName", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String DirectorLastName { get; set; }

        [StringLength(500, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String DirectorPosition { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractRefNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String AttorneyNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractRefNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String AttorneyYear { get; set; }

        [Display(Name = "Contract_ContractRefDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? ContractGiverDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ProvinceNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProvinceContractNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [StringLength(20, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ProvinceNo", ResourceType = typeof(Nep.Project.Resources.Model))]
        public String ProvinceContractYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        [Display(Name = "Contract_ContractRefDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? ProvinceContractDate { get; set; }
        
        public DateTime? ContractReceiveDate { get; set; }

        public String ContractReceiveName { get; set; }

        public String ContractReceiveSurname { get; set; }

        public String ContractReceivePosition { get; set; }

        #region Address
        public String OrganizationNameTH { get; set; }
        public String OrganizationNameEN { get; set; }
        public String OrganizationName { 
            get {
                string name = this.OrganizationNameTH;
                if(!String.IsNullOrEmpty(this.OrganizationNameEN)){
                    name = String.Format("{0} ({1})", name, this.OrganizationNameEN);
                }
                return name;
            } 
        }

        public String OraganizationAddress
        {
            get
            {
                StringBuilder text = new StringBuilder();
                if(!String.IsNullOrEmpty(this.Address)){
                    text.AppendFormat("{0} {1}", Model.ProjectInfo_AddressNo, this.Address);
                }

                if(!String.IsNullOrEmpty(this.Building)){
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_Building, this.Building);                     
                }

                if (!String.IsNullOrEmpty(this.Moo))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_Moo, this.Moo);
                }

                if (!String.IsNullOrEmpty(this.Soi))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_Soi, this.Soi);
                }

                if (!String.IsNullOrEmpty(this.Road))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_Building, this.Road);
                }

                if (!String.IsNullOrEmpty(this.SubdistrictName))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_SubDistrict, this.SubdistrictName);
                }

                if (!String.IsNullOrEmpty(this.DistrictName))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_District, this.DistrictName);
                }

                if (!String.IsNullOrEmpty(this.ProvinceName))
                {
                    text.AppendFormat(" {0} {1}", Model.ProjectInfo_Province, this.ProvinceName);
                }

                if (!String.IsNullOrEmpty(this.PostCode))
                {
                    text.AppendFormat(" {0}", this.PostCode);
                }
                return text.ToString();
            }
        }

        public String Address { get; set; }
        public String Building { get; set; }
        public String Moo { get; set; }
        public String Soi { get; set; }
        public String Road { get; set; }
        public String SubdistrictName { get; set; }
        public String DistrictName { get; set; }
        public String ProvinceName { get; set; }
        public String PostCode { get; set; }
        public String Telephone { get; set; }
        public String Fax { get; set; }
        public String Email { get; set; }
       
        #endregion Address

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }
        public Boolean IsCenterContract { get; set; }

        // มอบอำนาจหรือไม่
        [Display(Name = "Contract_AuthorizeFlag", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Boolean AuthorizeFlag { get; set; }

        // ชื่อผู้รับเงิน
        [Display(Name = "Contract_ReceiverName", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String ReceiverName { get; set; }

        // นามสกุลผู้รับเงิน
        [Display(Name = "Contract_ReceiverSurname", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String ReceiverSurname { get; set; }

        // ตำแหน่งผู้รับเงิน
        [Display(Name = "Contract_ReceiverPosition", ResourceType = typeof(Nep.Project.Resources.Model))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
        public String ReceiverPosition { get; set; }

        // วันที่มอบอำนาจ
        [Display(Name = "Contract_AuthorizeDate", ResourceType = typeof(Nep.Project.Resources.Model))]
        public DateTime? AuthorizeDate { get; set; }

        // เอกสารการมอบอำนาจ
        [Display(Name = "Contract_AuthorizeDocID", ResourceType = typeof(Nep.Project.Resources.Model))]
        public Decimal? AuthorizeDocID { get; set; }
        public ServiceModels.KendoAttachment AuthorizeDocAttachment { get; set; }
        public ServiceModels.KendoAttachment AddedAuthorizeDocAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedAuthorizeDocAttachment { get; set; }

        public List<ServiceModels.KendoAttachment> AuthorizeDocAttachmentMulti { get; set; }
        public List<ServiceModels.KendoAttachment> AddedAuthorizeDocAttachmentMulti { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedAuthorizeDocAttachmentMulti { get; set; }

        //Budget
        public Decimal? RequestBudgetAmount { get; set; }
        public Decimal? ReviseBudgetAmount { get; set; }
        public String ApprovalYear { get; set; }
        public string ipAddress { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// kenghot
        /// </summary>
        public List<ServiceModels.KendoAttachment> SupportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedSupportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedSupportAttachments { get; set; }

        public decimal? AttachPage1 { get; set; }
        public decimal? AttachPage2 { get; set; }
        public decimal? AttachPage3 { get; set; }
        public decimal? MeetingNo { get; set; }
        public DateTime? MeetingDate { get; set; }
        public decimal? LastApproveStatus { get; set; }
        public ContractExtend ExtendData { get; set; }
        public string ExtendJson { get; set; }

        public List<ContractDue> Dues { get; set; }
        public List<ServiceModels.KendoAttachment> KTBAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedKTBAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedKTBAttachments { get; set; }
    }
    public class ContractDue
    {
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ProjectId { get; set; }
        public decimal DueId { get; set; }
    }
    public class ContractExtend
    {
        public int PageCount1 { get; set; }
        public int PageCount2 { get; set; }
        public int PageCount3 { get; set; }
        public int PageCount4 { get; set; }
        public int PageCount5 { get; set; }
        public int PageCount6 { get; set; }
        public string PageCount1Text { get; set; }
        public string PageCount2Text { get; set; }
        public string PageCount3Text { get; set; }
        public string PageCount4Text { get; set; }
        public string PageCount5Text { get; set; }
        public string PageCount6Text { get; set; }
        public string Command { get; set; }
        public DateTime CommandDate { get; set; }
        public string CommandDateText { get; set; }
        public string BookNo { get; set; }
        public DateTime BookDate { get; set; }
        public string BookDateText { get; set; }
        public string BookOrder { get; set; }

        public int MeetingOrder { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingDateText { get; set; }
        public Address AddressAt { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจ
        /// </summary>
        public Address AddressAuth { get; set; }
        public string ReferenceNo { get; set; }
    }
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public DateTime? SignDate { get; set; }
        public string SignDateText { get; set; }

    }
    public class Address
    {
        public string AddressNo { get; set; }
        public string Moo { get; set; }
        public string Building { get; set; }
        public string Soi { get; set; }
        public string Street { get; set; }
        public decimal ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public decimal DistrictId { get; set; }
        public string DistrictName { get; set; }
        public decimal SubDistrictId { get; set; }
        public string SubDistrictName { get; set; }
        public string ZipCode { get; set; }
    }
    public class ProjectApprovalResult
    {
        public Decimal ProjectID { get; set; }
        public Decimal OrganizationID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public Decimal ProvinceID{ get; set; }
        public Boolean IsCenterReviseProject { get; set; }
               
        public String ProjectNo { get; set; }

        public String OrganizationNameTH { get; set; }
        public String OrganizationNameEN { get; set; }
        public String OrganizationName
        {
            get
            {
                String name = OrganizationNameTH;
                if (!String.IsNullOrEmpty(OrganizationNameEN))
                {
                    name = String.Format("{0} ({1})", name, OrganizationNameEN);
                }
                return name;
            }
        }
          
        public String ProjectNameTH { get; set; }
        public String ProjectNameEN { get; set; }
        public String ProjectName
        {
            get
            {
                String name = ProjectNameTH;
                if (!String.IsNullOrEmpty(ProjectNameEN))
                {
                    name = String.Format("{0} ({1})", name, ProjectNameEN);
                }
                return name;
            }
        }
        public string ProjectTypeCode { get; set; }
        public Decimal? TotalBudgetRequest { get; set; }

        public String EvaluationIsPassAss4 { get; set; }
        public String EvaluationIsPassAss5 { get; set; }
        public Decimal EvaluationScore { get; set; }
        public String EvaluationScoreCode { get; set; }
        public String EvaluationScoreDesc { get; set; }       
        public String EvaluationStatusName
        {
            get {
                String status = String.Empty;
                if (!String.IsNullOrEmpty(EvaluationIsPassAss4) && (!String.IsNullOrEmpty(EvaluationIsPassAss5)))                    
                {
                    if ((EvaluationIsPassAss4 == "1") && (EvaluationIsPassAss5 == "1"))
                    {
                        status = Nep.Project.Resources.UI.LabelPass;
                    }else{
                        status = Nep.Project.Resources.UI.LabelNotPass;
                    }
                }
                return status;
            }
        }

        public Decimal? ApprovalBudgetRequest { get; set; }

        public Decimal? ApprovalStatusID1 { get; set; }
        public Decimal? ApprovalBudget1 { get; set; }
        public String ApprovalDesc1 { get; set; }
        public String ApprovalName1 { get; set; }
        public String ApprovalLastName1 { get; set; }
        public String ApproverPosition1 { get; set; }
        public DateTime? ApprovalDate1 { get; set; }

        public Decimal? ApprovalStatusID2 { get; set; }
        public Decimal? ApprovalBudget2 { get; set; }
        public String ApprovalDesc2 { get; set; }
        public String ApprovalName2 { get; set; }
        public String ApprovalLastName2 { get; set; }
        public String ApproverPosition2 { get; set; }
        public DateTime? ApprovalDate2 { get; set; }
        
        public string ApprovalNo { get; set; }
        public string ApprovalYear { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public List<ServiceModels.ProjectInfo.BudgetDetail> BudgetDetails;

        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? BudgetTypeID { get; set; }
        public string ipAddress { get; set; }
    }

    public class ProjectApprovalLog{
        public Decimal ProjectApprovalStatusID { get; set; }
        public String ProjectNo { get; set; }
        public Decimal? BudgetReviseValue { get; set; }
        public Decimal ApprovalStatusID1 { get; set; }
        public Decimal? ApprovalBudget1 { get; set; }        
        public String ApprovalName1 { get; set; }
        public String ApprovalLastName1 { get; set; }
        public String ApproverPosition1 { get; set; }
        public DateTime? ApprovalDate1 { get; set; }

        public Decimal? ApprovalStatusID2 { get; set; }
        public Decimal? ApprovalBudget2 { get; set; }
        public String ApprovalName2 { get; set; }
        public String ApprovalLastName2 { get; set; }
        public String ApproverPosition2 { get; set; }
        public DateTime? ApprovalDate2 { get; set; }

        public string ApprovalNo { get; set; }
        public string ApprovalYear { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public List<ServiceModels.ProjectInfo.BudgetDetailApprovalLog> BudgetDetails;
    }

    public class BudgetDetailApprovalLog
    {
        public Decimal ProjectBudgetID { get; set; }
        public Decimal BudgetValue { get; set; }
        public Decimal BudgetReviseValue { get; set; }
        public Decimal BudgetReviseValue1 { get; set; }
        public Decimal BudgetReviseValue2 { get; set; }
    }

    public class AttachFiles
    {
        public List<ServiceModels.KendoAttachment> ReportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedReportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedReportAttachments { get; set; }
    }
    public class ProjectReportResult
    {
        public decimal ProjectID { get; set; }
        public decimal OrganizationID { get; set; }

        public decimal? ContractYear { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public Decimal? FollowupStatusID { get; set; }
        public string FollowupStatusCode { get; set; }
        public Decimal? BudgetAmount { get; set; }
        public Decimal? ReviseBudgetAmount { get; set; }

        public string ActivityDescription { get; set; }
        public decimal MaleParticipant { get; set; }
        public decimal FemaleParticipant { get; set; }
        public decimal? ActualExpense { get; set; }
        public string Benefit { get; set; }
        public string ProblemsAndObstacle { get; set; }
        public string Suggestion { get; set; }
        public decimal? OperationResult { get; set; }
        public decimal? OperationLevel { get; set; }
        public string OperationLevelDesc { get; set; }
        public string ReporterName1 { get; set; }
        public string ReporterLastname1 { get; set; }
        public string Position1 { get; set; }
        public DateTime? RepotDate1 { get; set; }
        public string Telephone1 { get; set; }
        public string SuggestionDesc { get; set; }
        public string ReporterName2 { get; set; }
        public string ReporterLastname2 { get; set; }
        public string Position2 { get; set; }
        public DateTime? RepotDate2 { get; set; }
        public string Telephone2 { get; set; }

        public Decimal? ReportAttachmentID { get; set; }
        public ServiceModels.KendoAttachment ReportAttachment { get; set; }
        public ServiceModels.KendoAttachment AddedReportAttachment { get; set; }
        public ServiceModels.KendoAttachment RemovedReportAttachment { get; set; }

        /// <summary>
        /// kenghot18
        /// </summary>
        public List<ServiceModels.KendoAttachment> ReportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedReportAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedReportAttachments { get; set; }

        public List<ServiceModels.KendoAttachment> ActivityAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedActivityAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedActivityAttachments { get; set; }

        public List<ServiceModels.KendoAttachment> ParticipantAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedParticipantAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedParticipantAttachments { get; set; }

        public List<ServiceModels.KendoAttachment> ResultAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> AddedResultAttachments { get; set; }
        public List<ServiceModels.KendoAttachment> RemovedResultAttachments { get; set; }
        // end kenghot 
        public List<ProjectParticipant> Participants { get; set; }

        public decimal ProjectApprovalStatusID { get; set; }
        public String ProjectApprovalStatusCode { get; set; }
        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal? ProvinceID { get; set; }
        public string IPAddress { get; set; }
        public List<BudgetDetail> BudgetDetails { get; set; }
        public decimal? Interest { get; set; }
        public decimal? SueCaseId { get; set; }
        public MultipleAttachFile SueDocument1 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument2 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument3 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument4 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument5 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument6 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument7 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument8 { get; set; } = new MultipleAttachFile();
        public MultipleAttachFile SueDocument9 { get; set; } = new MultipleAttachFile();
    }

    [Serializable]
    public class ProjectParticipant
    {
        public string UID { get; set; }
        public int? No { get; set; }
       
        public Decimal? ProjectParticipantID { get; set; }       
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String IDCardNo { get; set; }
        public String Gender { get; set; }
        public String GenderName { 
            get 
            {
                string name = (Gender == Common.Constants.GENDER_MALE) ? Nep.Project.Resources.UI.LabelMale : Nep.Project.Resources.UI.LabelFemale;
                return name;
            } 
        }

        public String TempProjectTargetGroupID { get; set; } 
        public Decimal? ProjectTargetGroupID { get; set; }
        public Decimal? TargetGroupID { get; set; }
        public String TargetGroupName { get; set; }
        public String TargetGroupNameDesc
        {
            get
            {
                String text = this.TargetGroupName;
                if(!String.IsNullOrEmpty(this.TargetGroupEtc)){
                    text = this.TargetGroupEtc;
                }             

                return text;
            }
        }
        public String TargetGroupCode { get; set; }
        public String TargetGroupEtc { get; set; }
        public GenericDropDownListData DdlGender { 
            get {
                GenericDropDownListData item = new GenericDropDownListData {
                    Value = Gender,
                    Text = (Gender == "M")? Nep.Project.Resources.UI.LabelMale : Nep.Project.Resources.UI.LabelFemale
                };
                return item;
            } 
        }
        private List<ServiceModels.GenericDropDownListData> _isCrippleList;
        public List<ServiceModels.GenericDropDownListData> isCrippleList
        {
            get
            {
                if (_isCrippleList == null)
                {
                    _isCrippleList = new List<ServiceModels.GenericDropDownListData>();
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCripple, Value = "1" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCrippleSupporter, Value = "0" });
                    //kenghot
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "วิทยากร", Value = "2" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "อาสาสมัครประชุมหน้าที่ประสานงาน", Value = "3" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "เจ้าหน้าที่โครงการ", Value = "4" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "กลุ่มเป้าหมายอื่นๆ ", Value = "5" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "ล่ามภาษามือ", Value = "6" });
                    _isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "ผู้ช่วยเหลือคนพิการเฉพาะกิจ", Value = "7" });
                }
                return  _isCrippleList;
            }
            
        }
        public String IsCripple { get; set; }
        public String IsCrippleDesc { 
            get {
                //kenghot
               
                    var desc = isCrippleList.Where(w => w.Value ==  IsCripple).FirstOrDefault();
                    if (desc != null)
                    {
                      return desc.Text;
                    }
                    else
                {
                    return (IsCripple == "1") ? Nep.Project.Resources.UI.LabelCripple : Nep.Project.Resources.UI.LabelCrippleSupporter;
                }
               
              
            }  
           // set { }
        }
        public GenericDropDownListData DdlIsCripple
        {
            get
            {
                GenericDropDownListData item = new GenericDropDownListData
                {
                    Value = IsCripple,
                    Text = (IsCripple == "1") ? Nep.Project.Resources.UI.LabelCripple : Nep.Project.Resources.UI.LabelCrippleSupporter
                };
                return item;
            }
        }

        public ProjectTargetNameList DdlTargetGroup
        {
            get
            {
                ProjectTargetNameList item = new ProjectTargetNameList
                {
                    ProjectParticipantsID = ProjectParticipantID,
                    ProjectTargetID = ProjectTargetGroupID,
                    TargetID = TargetGroupID,
                    TargetName = TargetGroupName,
                    TargetEtc = TargetGroupEtc,                    
                };
                return item;
            }
        }


        [Serializable]
        public class ProjectOperationAddress
        {
            public string UID { get; set; }
            public Decimal? OperationAddressID { get; set; } 
            public Decimal ProjectID { get; set; }         

            [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Address", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String Address { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Building", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String Building { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Moo", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String Moo { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Soi", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String Soi { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Road", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String Road { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
            public Decimal? SubDistrictID { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_SubDistrict", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String SubDistrict { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
            public Decimal? DistrictID { get; set; }

            [StringLength(100, ErrorMessageResourceName = "StringLengthField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_District", ResourceType = typeof(Nep.Project.Resources.Model))]
            public String District { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Nep.Project.Resources.Error))]
            [Display(Name = "ProjectInfo_Province", ResourceType = typeof(Nep.Project.Resources.Model))]
            public Decimal? ProvinceID { get; set; }
            public String Province { get; set; }

            [Display(Name = "ProcessingPlan_Map", ResourceType = typeof(Nep.Project.Resources.Model))]
            public Decimal? LocationMapID { get; set; }
            public ServiceModels.KendoAttachment LocationMapAttachment { get; set; }
            public ServiceModels.KendoAttachment AddedLocationMapAttachment { get; set; }
            public ServiceModels.KendoAttachment RemovedLocationMapAttachment { get; set; }
        }
    }

    public class DashBoard
    {
        public List<ProjectInfoList> ProjectInfoList { get; set; }
        public KendoChart BudgetChart { get; set; }
        public int[] ProjectCountByStatus { get; set; }
     }
    public class DashBoardORG
    {
        public List<RegisteredOrganizationList> OrganizationList { get; set; }
        public KendoChart BudgetChart { get; set; }
        public int[] ORGCountByStatus { get; set; }
    }
    public class Questionare
    {
        public string QType { get; set; }
        public string QField { get; set; }
        public string QValue { get; set; }
    }
    public class ProjectHistory
    {
        public DBModels.Model.PROJECTHISTORY History { get; set; }
        public string userName { get; set; }
        public int DayCount { get; set; }
        public string HistoryName { get; set; }
    }
    public class GetProjectResponse
    {
        public string ContractNo { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public string OrganizationName { get; set; }
        public string ProvinceName { get; set; }
        public decimal BudgetYear { get; set; }
        public decimal BudgetAmount { get; set; }
    }
    public class SueCaseLog
    {
        public decimal SueCaseID { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogBy { get; set; }
        public string LogCode { get; set; }
        public string LogDetail { get; set; }
        public string HostAddress { get; set; }
    }

}
