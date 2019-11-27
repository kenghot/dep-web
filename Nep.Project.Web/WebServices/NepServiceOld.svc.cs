using Nep.Project.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Nep.Project.Web.WebServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NepService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select NepService.svc or NepService.svc.cs at the Solution Explorer and start debugging.
	public class NepServiceOld : INepService
    {
        public IServices.IProjectInfoService _service { get; set; }

        public WSResponse.PaymentSlip GetPaymentSlip(string paymentID)
        {
            WSResponse.PaymentSlip ret = new WSResponse.PaymentSlip();
            ret.IsSuccess = false;
            try
            {
                decimal id = 0;
                if (!decimal.TryParse(paymentID, out id))
                {
                    ret.ErrorMessage = "ระบุเลขที่ใบรับเงินไม่ถูกต้อง";
                    return ret;

                }
                var p = _service.GetPaymentSlip(id);
                if (!p.IsCompleted)
                {
                    ret.ErrorMessage = p.Message[0];
                    return ret;
                }
                ret.BranchCode = "00000";
                ret.CompanyCode = p.Data.OrganizationCode;
                ret.CompanyName = p.Data.OrganizationName;
                ret.ErrorMessage = "";
                ret.InterestAmount = p.Data.Interest;
                ret.InvoiceDate = p.Data.TransactionDate;
                ret.InvoiceId = paymentID;
                ret.InvoicePayDate = p.Data.TransactionDate;
                ret.InvoiceRemark = "";
                ret.InvoiceUserName = p.Data.User;
                ret.PaidInterestAmount = p.Data.Interest;
                ret.PaidPrincipleAmount = p.Data.Balance;
                ret.PaidTotalAmount = p.Data.TotalBalance;
                ret.PrincipleAmount = p.Data.Balance;
                ret.TotalAmount = p.Data.TotalBalance;
                ret.TransactionID = paymentID;
                ret.Year = p.Data.TransactionDate.Year;
                ret.IsSuccess = true;
                return ret;
            }
            catch (Exception ex)
            {
                ret.ErrorMessage = ex.Message;
                return ret;
            }

        }
    }
}
