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
 
    public class ProjectQuery
    {

        public string ProjectApprovalCode { get; set; }
        public decimal? BudgetValue { get; set; }
        public decimal? BudgetReviseValue { get; set; }
        public string OrganizationTypeCode { get; set; }
        public string OrganizationType { get; set; }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get; set; }
        public string DisabilityCode { get; set; }
        public string DisabilityName { get; set; }
        public string RejectComment { get; set; }
        public string FollowCode { get; set; }
        public string ACKNOWLEDGED { get; set; }
        public bool IsApproved { get; set; }
        public string IsPassMission1 { get; set; }
        public string IsPassMission2 { get; set; }
        public string IsPassMission3 { get; set; }
        public string IsPassMission4 { get; set; }
        public string IsPassMission5 { get; set; }
        public string IsPassMission6 { get; set; }


    }
    public class ProjectDetail
    {
        public string ProjectName { get; set; }
        public string Organization { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string ProvinceName { get; set; }
        public string ApproveStatus { get; set; }
        public string FollowStatus { get; set; }
    }
    public class LegendData
    {
        public string id { get; set; }
        public string color { get; set; }
        public string description { get; set; }
        public int projects { get; set; }
        public decimal amount { get; set; }
        public string amountString { get; set; }

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
