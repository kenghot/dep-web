using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportBudgetSummaryService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportBudgetSummary> ListReportBudgetSummary(ServiceModels.QueryParameter p, decimal? provinceID);
    }
}
