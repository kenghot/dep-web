using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class Report4Service : IServices.IReport4Service
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public Report4Service(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.Report4> ListReport4(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.Report4> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.Report4>();
            decimal statusReport = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.FollowupStatus && x.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว).Select(x => x.LOVID).FirstOrDefault();

            try
            {
                var data = (from gen in _db.ProjectGeneralInfoes
                            join pApprove in _db.ProjectApprovals on gen.ProjectID equals pApprove.ProjectID
                            join pInfo in _db.ProjectInformations on gen.ProjectID equals pInfo.ProjectID
                            join pContract in _db.ProjectContracts on gen.ProjectID equals pContract.ProjectID into contractTemp
                                from contract in contractTemp.DefaultIfEmpty()
                            join s in _db.MT_ListOfValue on gen.ProjectApprovalStatusID equals s.LOVID
                            join fs in _db.MT_ListOfValue on gen.FollowUpStatus equals fs.LOVID into fsTemp 
                                from fs in fsTemp.DefaultIfEmpty()
                            where gen.ProjectApprovalStatus.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && ((gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
                                   || (gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) || (gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))
                            select new ServiceModels.Report.Report4
                            {
                                ApproveNo = pApprove.ApprovalNo,
                                ApproveYear = pApprove.ApprovalYear,
                                No = "1",
                                ProjectName = pInfo.ProjectNameTH,
                                OrganizationName = gen.OrganizationNameTH,
                                Amount = gen.BudgetReviseValue.Value,
                                ContractDate = contract.ContractDate,
                                BudgetYear = pInfo.BudgetYear,
                                StartDate = contract.ContractStartDate,
                                EndDate = contract.ContractEndDate,
                                Status = (fs == null)? "อยู่ระหว่างดำเนินการ" : fs.LOVName,
                                ProjectId = gen.ProjectID,
                                ProvinceId = gen.ProvinceID
                            }).ToQueryData(p);

                result = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report4", ex);
            }
            return result;
        }
    }
}
