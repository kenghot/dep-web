using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportSummaryTrackingService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking> ListReportSummaryTracking(ServiceModels.QueryParameter p);
    }
}
