using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportOrganizationClientService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient> ListReportOrganizationClient(ServiceModels.QueryParameter p);
    }
}
