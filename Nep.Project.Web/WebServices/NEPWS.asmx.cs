using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Nep.Project.Web.WebServices
{
    /// <summary>
    /// Summary description for NEPWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NEPWS : System.Web.Services.WebService
    {
        public IServices.IProjectInfoService _service { get; set; }
        [WebMethod]
        public WSResponse.PaymentSlip GetPaymentSlip(string paymentID)
        {
              
            var db = new DBModels.Model.NepProjectDBEntities();
            _service = new Nep.Project.Business.ProjectInfoService(db, null, null, null, null);
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
