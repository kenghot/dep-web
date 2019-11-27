using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportStatisticSupportService
    {
        List<ServiceModels.Report.ReportStatisticSupport.CompareSupport> GetCompareSupports(int startYear, int endYear);

        List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType> GetAnalyzeProjectByTypes(int startYear, int endYear);

        List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic> GetAnalyzeProjectByStrategics(int startYear, int endYear);

        List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType> GetAnalyzeProjectByProjectTypes(int startYear, int endYear);
    }
}
