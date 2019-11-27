using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportPaymentSlip
    {

            public string ProjectName { get; set; }
            public string OrganizationCode { get; set; }
            public string OrganizationName { get; set; }
            public string TransactionCode { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal Interest { get; set; }
            public decimal BudgetAmount { get; set; }
            public decimal ReviseBudgetAmount { get; set; }
            public decimal ActualExpense { get; set; }
            public decimal Balance { get; set; }
            public decimal TotalBalance { get; set; }
            public string User { get; set; }
        public string ThaiBaht { get; set; }
        public Byte[] Barcode { get; set; }
        
    }
}
