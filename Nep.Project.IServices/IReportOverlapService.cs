using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportOverlapService
    {
        ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>> ListReportOverlap(Decimal? Year, Decimal? ProvinceID, String Name, String IDCardNo);
    }
}
