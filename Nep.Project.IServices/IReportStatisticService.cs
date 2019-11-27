using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportStatisticService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport> ListCompareDuplicatedSupport(ServiceModels.QueryParameter p);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType> ListAnalyzeProjectByType(ServiceModels.QueryParameter p, Decimal? provinceID);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByStrategic> ListAnalyzeProjectByStrategic(ServiceModels.QueryParameter p, Decimal? provinceID);
    }
}
