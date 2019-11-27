using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportEvaluationService : IServices.IReportEvaluationService
    {
          private readonly DBModels.Model.NepProjectDBEntities _db;

          public ReportEvaluationService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;
        }


       public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary> ListReportEvaluationSummary(ServiceModels.QueryParameter p)
          {
              ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary>();
             try
             {

                 result = (from proGeneral in _db.ProjectGeneralInfoes
                          where proGeneral.ProjectApprovalStatus.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && ((proGeneral.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
                                   || (proGeneral.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) || (proGeneral.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))
                       select new ServiceModels.Report.ReportEvaluationSummary()
                       {
                           No = null,
                           Organization = proGeneral.OrganizationNameTH,
                           ProjectName = (proGeneral.ProjectInformation != null)?proGeneral.ProjectInformation.ProjectNameTH:null,
                           ProjectNo = (proGeneral.ProjectInformation != null) ? proGeneral.ProjectInformation.ProjectNo : null,
                           RequestBudget = proGeneral.BudgetValue,
                           IsPassAss4 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.IsPassAss4:null,
                           IsPassAss5 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.IsPassAss5:null,
                           Assessment61 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment61:(decimal?)null,
                           Assessment62 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment62:(decimal?)null,
                           Assessment63 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment63:(decimal?)null,
                           Assessment64 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment64:(decimal?)null,
                           Assessment65 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment65:(decimal?)null,
                           Assessment66 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment66:(decimal?)null,
                           Assessment67 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment67:(decimal?)null,
                           Assessment68 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment68:(decimal?)null,
                           Assessment69 = (proGeneral.ProjectEvaluation != null)?proGeneral.ProjectEvaluation.Assessment69:(decimal?)null,
                           BudgetYear = (proGeneral.ProjectInformation != null) ? proGeneral.ProjectInformation.BudgetYear : 0,
                           ProvinceID = proGeneral.ProvinceID ,
                           BugetReviseValue = proGeneral.BudgetReviseValue,
                           AssessmentDesc = (proGeneral.ProjectEvaluation != null) ? proGeneral.ProjectEvaluation.AssessmentDesc :null
                            }).ToQueryData(p);
          
             }
             catch (Exception ex)
             {
                 result.IsCompleted = false;
                 result.Message.Add(ex.Message);
                 Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportEvaluationSummary", ex);
             }

             return result;
          }
    }
}
