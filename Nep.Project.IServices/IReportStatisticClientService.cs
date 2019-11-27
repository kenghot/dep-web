using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReportStatisticClientService
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.CompareClientSupport> GetCompareClientSupports(int startYear, int endYear);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType> GetAnalyzeProjectByTypes(int startYear, int endYear);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic> GetAnalyzeProjectByStrategics(int startYear, int endYear);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup> GetAnalyzeProjectByTargetGroups(int startYear, int endYear);
    }
}
