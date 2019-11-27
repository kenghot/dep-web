using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportBudgetApplicantService : Nep.Project.IServices.IReportBudgetApplicantService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportBudgetApplicantService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportBudgetApplicant> ListReportBudgetApplicant(
            ServiceModels.QueryParameter p)
        {
            decimal approval1 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ApprovalStatus1 && x.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน).Select(x => x.LOVID).FirstOrDefault();
            decimal approval2 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ApprovalStatus1 && x.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ปรับลดวงเงินสนับสนุน).Select(x => x.LOVID).FirstOrDefault();
            decimal approval3 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ApprovalStatus1 && x.LOVCode == Common.LOVCode.Approvalstatus1.ไม่อนุมัติ).Select(x => x.LOVID).FirstOrDefault();
            string centerProvinceAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();

            decimal etcTargetGroup = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.TargetGroup && x.LOVCode == Common.LOVCode.Targetgroup.อื่นๆ).Select(x => x.LOVID).FirstOrDefault();

            var projectInformation = (from x in _db.ProjectInformations
                                      join Province in _db.MT_Province on x.ProjectGeneralInfo.ProvinceID equals Province.ProvinceID
                                      let targetGroups = (from t in x.ProjectGeneralInfo.ProjectParticipants
                                                          join lovTargetGroup in _db.MT_ListOfValue on t.TargetGroupID equals lovTargetGroup.LOVID
                                                          select lovTargetGroup.LOVID == etcTargetGroup ? t.TargetGroupETC : lovTargetGroup.LOVName
                                                )//.Distinct()
                                      where (_user.ProvinceID == null) || ((_user.ProvinceID != null && (_user.ProvinceAbbr == centerProvinceAbbr) || (Province.ProvinceID == _user.ProvinceID)))
                                      select new ServiceModels.Report.ReportBudgetApplicant
                                      {
                                          No = x.ProjectNo,
                                          ProjectNo = x.ProjectNo,
                                          BudgetYear = x.BudgetYear,
                                          OrganizationType = x.ProjectGeneralInfo.OrganizationTypeEtc,
                                          Organization = x.ProjectGeneralInfo.OrganizationNameTH,
                                          OrganizationSupport = x.ProjectGeneralInfo.OrgUnderSupport,
                                          ProjectName = x.ProjectNameTH,
                                          ProjectType = x.ProjectType.LOVName,
                                          Section = Province.Section.LOVName,
                                          SectionCode = Province.Section.LOVCode,
                                          Province = Province.ProvinceName,
                                          ProjectDate = x.ProjectDate,
                                          RequestBudget = x.ProjectGeneralInfo.BudgetValue,
                                          ApprovalBudgetType = x.ProjectGeneralInfo.ProjectApproval.ApprovalBudgetType.LOVName,
                                          SumTargetGroupAmount = x.ProjectGeneralInfo.ProjectTargetGroups.Sum(ptg => ptg.TargetGroupAmt),
                                          Approval1 = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus1.LOVID == approval1 ? "/" : "-",
                                          Approval3 = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus1.LOVID == approval3 ? "/" : "-",
                                          Approval2 = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus1.LOVID == approval2 ? "/" : "-",
                                          Approval_ = "-",
                                          ApprovalStatus1 = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus == "1" ? "/" : "-",
                                          ApprovalStatus0 = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus == "0" ? "/" : "-",
                                          ApprovalStatusNull = "-",
                                          ApprovalStatus = x.ProjectGeneralInfo.ProjectApproval.ApprovalStatus,
                                          ApprovalNo = x.ProjectGeneralInfo.ProjectApproval.ApprovalNo,
                                          ApprovalDate2 = x.ProjectGeneralInfo.ProjectApproval.ApprovalDate,
                                          ProvinceAbbr = Province.ProvinceAbbr,
                                          BudgetReviseValue = x.ProjectGeneralInfo.BudgetReviseValue,
                                          //ContractDate = x.ProjectGeneralInfo.ProjectContract.ContractStartDate + " - " + x.ProjectGeneralInfo.ProjectContract.ContractEndDate,
                                          ContractStartDate = x.ProjectGeneralInfo.ProjectContract.ContractStartDate,
                                          ContractEndDate = x.ProjectGeneralInfo.ProjectContract.ContractEndDate,
                                          ProjectTargetGroupMale = x.ProjectGeneralInfo.ProjectTargetGroups.Sum(ptg => ptg.Male),
                                          ProjectTargetGroupFemale = x.ProjectGeneralInfo.ProjectTargetGroups.Sum(ptg => ptg.Female),
                                          ProjectTargetGroupAmount = x.ProjectGeneralInfo.ProjectTargetGroups.Sum(ptg => ptg.TargetGroupAmt),
                                          MaleParticipant = x.ProjectGeneralInfo.ProjectReport.MaleParticipant,
                                          FemaleParticipant = x.ProjectGeneralInfo.ProjectReport.FemaleParticipant,
                                          ParticipantAmount = x.ProjectGeneralInfo.ProjectReport.MaleParticipant + x.ProjectGeneralInfo.ProjectReport.FemaleParticipant,
                                          //TargetGroup = String.Join(",", x.ProjectTargetGroups),
                                          TargetGroups = targetGroups,
                                          ActualExpense = x.ProjectGeneralInfo.ProjectReport.ActualExpense,
                                          ReturnAmount = (x.ProjectGeneralInfo.BudgetReviseValue ?? 0) - (x.ProjectGeneralInfo.ProjectReport.ActualExpense ?? 0)
                                      }).ToQueryData(p);

            //_db.Database.Log = ((x) => { Common.Logging.LogInfo("->>", x); });

            return projectInformation;
        }

        public class TempClass
        {
            public TempClass()
            {
                ProjectTargetGroups = new List<string>();
            }

            public DBModels.Model.ProjectInformation ProjectInformations { get; set; }
            public DBModels.Model.MT_Province Province { get; set; }
            public IEnumerable<String> ProjectTargetGroups { get; set; }
        }
    }
}
