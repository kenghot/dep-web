using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportSummaryTrackingService : IServices.IReportSummaryTrackingService
    {
          private readonly DBModels.Model.NepProjectDBEntities _db;

       public ReportSummaryTrackingService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;
        }


       public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking> ListReportSummaryTracking(ServiceModels.QueryParameter p)
          {
              ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking>();
             try
             {
                 result = (from proGeneral in _db.ProjectGeneralInfoes
                           join province in _db.MT_Province on proGeneral.ProvinceID equals province.ProvinceID
                           join appStatus in _db.MT_ListOfValue.Where(apps => (apps.LOVGroup == Common.LOVGroup.ProjectApprovalStatus) && (apps.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)) on proGeneral.ProjectApprovalStatusID equals appStatus.LOVID 
                           join status in _db.MT_ListOfValue on proGeneral.FollowUpStatus  equals status.LOVID into tmpStatus
                           from fs in tmpStatus.DefaultIfEmpty()
                          
                           select new ServiceModels.Report.ReportSummaryTracking()
                       {
                           No = null,
                           BudgetYear = (proGeneral.ProjectInformation != null) ? proGeneral.ProjectInformation.BudgetYear : 0,
                           RegisterNumber = (proGeneral.ProjectInformation != null) ? proGeneral.ProjectInformation.ProjectNo : null,
                           ProjectName = (proGeneral.ProjectInformation != null) ? proGeneral.ProjectInformation.ProjectNameTH : null,
                           OrganizationName = proGeneral.OrganizationNameTH,
                           Province = (province.ProvinceName != null) ? province.ProvinceName : null,
                           ProvinceID = (province.ProvinceID != null) ? province.ProvinceID : 0,
                           Status = (fs != null) ? fs.LOVName : "ยังไม่ติดตาม",
                           StatusID = (fs != null)? fs.LOVID : 0,
                           FinishedDate = (proGeneral.ProjectContract != null) ? proGeneral.ProjectContract.ContractEndDate : (DateTime?)null,
                           MonthTracing = (proGeneral.ProjectContract != null) ? proGeneral.ProjectContract.ContractEndDate.Month : 0,
                           YearTracing = (proGeneral.ProjectContract != null) ? proGeneral.ProjectContract.ContractEndDate.Year : 0

                            }).ToQueryData(p);

                 //_db.Database.Log = ((x) => { Common.Logging.LogInfo("->>", x); });
          
             }
             catch (Exception ex)
             {
                 result.IsCompleted = false;
                 result.Message.Add(ex.Message);
                 Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportSummaryTrackingService", ex);
             }

             return result;
          }
    }
}
