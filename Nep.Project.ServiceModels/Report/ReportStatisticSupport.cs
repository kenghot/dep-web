using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportStatisticSupport
    {
        public class CompareSupport
        {
            public String BudgetYear {get;set;}
            public Decimal AllowcateAmount {get;set;}
            public Decimal ApproveAmount { get; set; }
        }

        public class AnalyzeProjectByType
        {
            public String No { get; set; }
            public String TypeName { get; set; }
            public String Year { get; set; }
            public Decimal Amount { get; set; }
        }

        public class AnalyzeProjectByStrategic
        {
            public String No { get; set; }
            public String StrategicName { get; set; }
            public String Year { get; set; }
            public Decimal Amount { get; set; }
        }

        public class AnalyzeProjectByProjectType
        {
            public String No { get; set; }
            public String ProjectTypeName { get; set; }
            public String Year { get; set; }
            public Decimal Amount { get; set; }
        }
    }
}
