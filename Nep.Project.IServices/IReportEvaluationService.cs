using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportEvaluationService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary> ListReportEvaluationSummary(ServiceModels.QueryParameter p);
    }
}
