using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportOverlapProvinceService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince> ListReportOverlapProvince(ServiceModels.QueryParameter p);
    }
}
