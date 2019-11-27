using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportBudgetSummaryService : IServices.IReportBudgetSummaryService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportBudgetSummaryService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportBudgetSummary> ListReportBudgetSummary(ServiceModels.QueryParameter p, decimal? provinceID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportBudgetSummary> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportBudgetSummary>();
            try
            {
                if (provinceID.HasValue)
                {
                    decimal pid = (decimal)provinceID;
                    result = (from summay in _db.View_BudgetSummaryByOrgType
                                where summay.ProvinceID == pid
                                group summay by new {
                                    summay.BudgetYear 
                                }
                                into grouping
                                select new ServiceModels.Report.ReportBudgetSummary
                                {
                                    BudgetYear = grouping.Key.BudgetYear,

                                    Project = grouping.Sum(pAll => pAll.Project),
                                    Budget = grouping.Sum(bAll => bAll.Budget),

                                    Project1 = grouping.Sum(p1 => p1.Project1),
                                    Budget1 = grouping.Sum(b1 => b1.Budget1),

                                    Project2 = grouping.Sum(p2 => p2.Project2),
                                    Budget2 = grouping.Sum(b2 => b2.Budget2),

                                    Project3 = grouping.Sum(p3 => p3.Project3),
                                    Budget3 = grouping.Sum(b3 => b3.Budget3),
                                }
                               ).ToQueryData(p);
                    
                }
                else
                {
                    result = (from summay in _db.View_BudgetSummaryByOrgType                              
                              group summay by new
                              {
                                  summay.BudgetYear
                              }
                                  into grouping
                                  select new ServiceModels.Report.ReportBudgetSummary
                                  {
                                      BudgetYear = grouping.Key.BudgetYear,

                                      Project = grouping.Sum(pAll => pAll.Project),
                                      Budget = grouping.Sum(bAll => bAll.Budget),

                                      Project1 = grouping.Sum(p1 => p1.Project1),
                                      Budget1 = grouping.Sum(b1 => b1.Budget1),

                                      Project2 = grouping.Sum(p2 => p2.Project2),
                                      Budget2 = grouping.Sum(b2 => b2.Budget2),

                                      Project3 = grouping.Sum(p3 => p3.Project3),
                                      Budget3 = grouping.Sum(b3 => b3.Budget3),
                                  }
                               ).ToQueryData(p); ;
                }
                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report Budget Summary", ex);
            }
            return result;
        }
    }
}
