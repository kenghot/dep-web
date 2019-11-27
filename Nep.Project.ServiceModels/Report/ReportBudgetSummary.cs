using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportBudgetSummary
    {
        /// <summary>
        /// ปีงบประมาณ
        /// </summary>
        public Decimal BudgetYear { get; set; }        

        /// <summary>
        /// งบประมาณ
        /// </summary>        
        public Decimal? Budget { get; set; }
        public Decimal? Budget1 { get; set; }
        public Decimal? Budget2 { get; set; }
        public Decimal? Budget3 { get; set; }

        /// <summary>
        /// จำนวนโครงการ
        /// </summary>
        public Decimal? Project { get; set; }
        public Decimal? Project1 { get; set; }
        public Decimal? Project2 { get; set; }
        public Decimal? Project3 { get; set; }       

    }
}
