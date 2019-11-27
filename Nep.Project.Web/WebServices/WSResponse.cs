using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nep.Project.Web.WebServices
{
    public class WSResponse
    {
        public class PaymentSlip
        {
            public string projectName { get; set; }
            public string orgName { get; set; }
            public string invoiceId { get; set; }
            public DateTime invoiceDate { get; set; }
            public string invoiceUserName { get; set; }
            public decimal payAmount { get; set; }
            public decimal interestAmount { get; set; }
            public decimal totalAmount { get; set; }
        }
        public class PaymentConfirm
        {
            public decimal invoiceId { get; set; }
            public decimal payAmount { get; set; }
            public decimal interestAmount { get; set; }
            public decimal totalAmount { get; set; }
            public DateTime paymentDate { get; set; }
            public string paymentId { get; set; }
            public string receiptBookNo { get; set; }
            public string receiptDocNo { get; set; }


        }
       
    }
}