using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class SatisfyReportModel
    {
        public string Year { get; set; }
        public List<SatisfyReportDetail> Items { get; set; }
      
    }
    public class SatisfyReportDetail
    {
        public int No { get; set; }
        public decimal ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProvinceId { get; set; }
        public decimal BudgetYear { get; set; }
        public string QNData { get; set; }
        public string QN1_0 { get; set; } 
        public string QN1_1 { get; set; }  
        public string QN1_2 { get; set; }
        public string QN1_3 { get; set; }
        public string QN1_4 { get; set; }
        public string QN1_5 { get; set; }
        public string QN2_0 { get; set; }
        public string QN2_1 { get; set; }
        public string QN2_2_1 { get; set; }
        public string QN2_2_2 { get; set; }
        public string QN2_2_3 { get; set; }
        public string QN2_2_4 { get; set; }

        public string QN2_3_1 { get; set; }
        public string QN2_3_2 { get; set; }
        public string QN2_3_3 { get; set; }
        public string QN2_3_4 { get; set; }
        public string QN3_1 { get; set; }
        public string QN4_1 { get; set; }
        public string QN4_2 { get; set; }
        public string QN4_3 { get; set; }



    }

}
