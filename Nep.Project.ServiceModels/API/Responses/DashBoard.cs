using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class DashBoardResponse
    {
        public SummaryData summary { get; set; }

        public ChartData projectTypeAmount { get; set; }
    }
    public class SummaryData
    {
        public int newProject { get; set; }
        public int newOrganization { get; set; }
        public int notReported { get; set; }
        public int noProcess { get; set; }
        public int reported { get; set; }
        public int rePurpose { get; set; }
    }
    public class ChartData
    {
        public List<string> labels { get; set; }
        public List<ChartDataSet> datasets { get; set; }
    }
    public class ChartDataSet
    {
        public string label { get; set; }
        public List<string> backgroundColor { get; set; }
        public List<decimal> data { get; set; }
    }
}
