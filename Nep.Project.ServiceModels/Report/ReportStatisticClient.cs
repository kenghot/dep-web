using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportStatisticClient
    {
        public class CompareClientSupport
        {
            public String BudgetYear {get;set;}
            public String  Region { get; set; }
            public Decimal MaleClientAmount {get;set;}
            public Decimal FemaleClientAmount { get; set; }
            public Decimal MaleDuplicatedAmount { get; set; }
            public Decimal FeMaleDuplicatedAmount { get; set; }
        }
        
        public class AnalyzeProjectByType
        {
            public String No { get; set; }
            public String TypeName { get; set; }
            public String Year { get; set; }
            public Decimal MaleAmount { get; set; }
            public Decimal FemaleAmount { get; set; }
        }

        public class AnalyzeProjectByStrategic
        {
            public String No { get; set; }
            public String StrategicName { get; set; }
            public String Year { get; set; }
            public Decimal MaleAmount { get; set; }
            public Decimal FemaleAmount { get; set; }
        }

        public class AnalyzeProjectByTargetGroup
        {
            public String No { get; set; }
            public String Year { get; set; }
            public String TargetGroup { get; set; }
            public Decimal MaleAmount { get; set; }
            public Decimal FemaleAmount { get; set; }
        }
    }
}
