using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportsService
    {
        ServiceModels.ReturnObject<ServiceModels.Report.SatisfyReportModel> ListSatisfyReport(ServiceModels.QueryParameter p);
        ServiceModels.ReturnObject<ServiceModels.Report.FollowUpReportModel> ListFollowUpReport(ServiceModels.QueryParameter p);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ApprovedReportModel> ListApprovedReport(ServiceModels.QueryParameter p);
    }
}

