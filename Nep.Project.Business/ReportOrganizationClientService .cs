using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportOrganizationClientService : IServices.IReportOrganizationClientService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportOrganizationClientService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }


        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient> ListReportOrganizationClient(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient>();
            try
            {

                result = (from genInfo in _db.ProjectGeneralInfoes
                          join proInfo in _db.ProjectInformations on genInfo.ProjectID equals proInfo.ProjectID
                          join province in _db.MT_Province on genInfo.ProvinceID equals province.ProvinceID
                          /*join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID*/
                          join participant in _db.ProjectParticipants on genInfo.ProjectID equals participant.ProjectID
                          join targetgroup in _db.MT_ListOfValue on participant.TargetGroupID equals targetgroup.LOVID
                          /*where (fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว) &&
                                (genInfo.ProjectEvaluation.IsPassMission1 == "1")*/
                          where participant.IDCardNo != null
                          select new ServiceModels.Report.ReportOrganizationClient()
                          {
                              Name = participant.FirstName + " " + participant.LastName,
                              IdCardNo = participant.IDCardNo,
                              ProjectName = proInfo.ProjectNameTH,
                              BudgetYear = proInfo.BudgetYear,
                              ProvinceID = genInfo.ProvinceID,
                              Gender = participant.Gender,
                              Target = targetgroup.LOVName ,
                              DuplicatedAmt = (from inner in _db.ProjectParticipants
                                             /*  join innerFStatus in _db.MT_ListOfValue on inner.ProjectGeneralInfo.FollowUpStatus equals innerFStatus.LOVID*/
                                               where /*(innerFStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว) 
                                                    && inner.ProjectGeneralInfo.ProjectEvaluation.IsPassMission1 == "1"
                                                    &&*/ inner.IDCardNo == participant.IDCardNo
                                                select inner
                                                ).Count()
                          }).ToQueryData(p);

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportOrganizationClient", ex);
            }

            return result;
        }
    }
}
