using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    #region FollowUpReportModel
    public class FollowUpReportModel
    {
        public string Year { get; set; }
        public List<FollowUpReportDetail> Items { get; set; }

    }
    public class FollowUpReportDetail
    {
        public int No { get; set; }
        public decimal ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProvinceId { get; set; }
        public decimal BudgetYear { get; set; }
        public string QNData { get; set; }
        public string BudgetValueType { get; set; }
        public string Reported { get {
                var ret = "";
                switch(this.rd0_1)
                {
                    case "1":
                        {
                            ret = "ส่งแล้ว";
                            break;
                        }
                    case "2":
                        {
                            ret = "ภายในกำหนด";
                            break;
                        }
                    case "3":
                        {
                            ret = "ล่าช้ากว่ากำหนด";
                            break;
                        }
                    case "4":
                        {
                            ret = $"ยังไม่ส่ง เนื่องจาก {this.tb0_1}";
                            break;
                        }
                }
                return ret;
            } }
        public decimal? BudgetAmount { get; set; }
        public string rd0_1 { get; set; }
        public string tb0_1 { get; set; }
        public string rd1_1_1 { get; set; }
        public string rd1_2_1_1 { get; set; }
        public string rd1_2_2_1 { get; set; }
        public string rd1_2_3_1 { get; set; }
        public string rd1_3_1_1 { get; set; }
        public string rd1_3_2_1 { get; set; }
        public string rd1_4_1_1 { get; set; }
        public string rd2_1_1_1 { get; set; }
        public string rd2_1_2_1 { get; set; }
        public string rd2_2_1_1 { get; set; }
        public string lbTotal1 { get; set; }
        public string lbTotal2 { get; set; }
        public string lbGrandTotal { get; set; }
        public string tbSignName1 { get; set; }
        public string tbSignName2 { get; set; }
        public string tbSignName3 { get; set; }
        public string lbResultText { get; set; }
        public string tbProblem { get; set; }
        public string tbSolution { get; set; }
        public string tbSuccess { get; set; }
        public string tbImplimentor { get; set; }
        public string tbEvaluator { get; set; }
        public string tbSuggestion { get; set; }
        public string NoProblem { get {
                
                return (string.IsNullOrEmpty(this.tbProblem)) ? "ไม่พบปัญหา" : "";
            } }
    }
        #endregion
}
