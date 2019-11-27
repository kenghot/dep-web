using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
namespace Nep.Project.Web.WebServices
{
    /// <summary>
    /// Summary description for NepService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NepService : System.Web.Services.WebService
    {
        
        public IServices.IProjectInfoService _service { get; set; }
        public NepService()
        {
            var db = new DBModels.Model.NepProjectDBEntities();
            _service = new Nep.Project.Business.ProjectInfoService(db, null, null, null, null);
        }
        [WebMethod]
        public ServiceModels.ReturnObject< WSResponse.PaymentConfirm> ConfirmPayment(string user, string password ,WSResponse.PaymentConfirm p)
        {

            ServiceModels.ReturnObject<WSResponse.PaymentConfirm> ret = new ServiceModels.ReturnObject<WSResponse.PaymentConfirm>();
            ret.IsCompleted = false;
            try
            {
                isAuthen(user, password);
                ret.IsCompleted = true;
                ret.Data = p;

                return ret;
            }
            catch(Exception ex)
            {
                ret.Message.Add(ex.Message);
                return ret;
            }
        }

        [WebMethod]
        public ServiceModels.ReturnObject< WSResponse.PaymentSlip> GetPaymentSlip(string user, string password, string paymentID)
        {

            ServiceModels.ReturnObject<WSResponse.PaymentSlip> ret = new ServiceModels.ReturnObject<WSResponse.PaymentSlip>();
            ret.IsCompleted = false;
            try
            {
                isAuthen(user,password);
                decimal id = 0;
                if (!decimal.TryParse(paymentID,out id))
                {
                    ret.Message.Add("ระบุเลขที่ใบรับเงินไม่ถูกต้อง");
                    return ret;

                }
                var p = _service.GetPaymentSlip(id);
                if (!p.IsCompleted)
                {
                    ret.Message.Add(p.Message[0]);
                    return ret;
                }
                ret.Data = new WSResponse.PaymentSlip()
                {
                    interestAmount = p.Data.Interest,
                    invoiceDate = p.Data.TransactionDate,
                    invoiceId = paymentID.PadLeft(10, '0'),
                    invoiceUserName = "Test",
                    orgName = p.Data.OrganizationName,
                    payAmount = p.Data.Balance,
                    projectName = p.Data.ProjectName,
                    totalAmount = p.Data.TotalBalance

                };
                ret.IsCompleted = true;
                return ret;
            }
            catch (Exception ex)
            {
                ret.Message.Add(  ex.Message);
                return ret;
            }
 
        }
        private void isAuthen(string user,string password)
        {
            //Basic bmVwd3M6bmVwd3MxMjM0
            //var auth = Context.Request.Headers["Authorization"];
            //if (string.IsNullOrEmpty(auth) || auth != "Basic bmVwd3M6bmVwd3MxMjM0")
            //{

            //    throw new ApplicationException("invalid authorization");
            //}
            if (user != "nepws" && password != "nepws1234")
            {
                throw new ApplicationException("invalid authorization");
            }

        }

    }
}
