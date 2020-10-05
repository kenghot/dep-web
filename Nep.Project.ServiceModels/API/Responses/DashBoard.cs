using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class DashBoardResponse
    {
        public SummaryData summary { get; set; } = new SummaryData();
        public DefaultData orgTypeData { get; set; } = new DefaultData();
        public DefaultData projectTypeData { get; set; } = new DefaultData();
        public DefaultData disabilityTypeData { get; set; } = new DefaultData();
        public DefaultData missionData { get; set; } = new DefaultData();
    }
    public class LegendData
    {
        public string id { get; set; }
        public string color { get; set; }
        public string description { get; set; }
        public int projects { get; set; }
        public decimal amount { get; set; }
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
    public class DefaultData
    {
       
        public ChartData projectData { get; set; } = new ChartData() ;
        public ChartData amountData { get; set; } = new ChartData();
        public List<LegendData> legendDatas { get; set; } = new List<LegendData>();
    }
    public class ChartData
    {
        public List<string> labels { get; set; } = new List<string>();
        public List<ChartDataSet> datasets { get; set; } = new List<ChartDataSet>() { new ChartDataSet() } ;

    }
    public class ChartDataSet
    {
        public string label { get; set; }
        public List<string> backgroundColor { get; set; } = new List<string>();
        public List<decimal> data { get; set; } = new List<decimal>();
    }
}
