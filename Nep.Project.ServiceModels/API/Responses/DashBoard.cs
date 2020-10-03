using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class DashBoardResponse
    {
        public ChartData projectTypeAmount { get; set; }
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
