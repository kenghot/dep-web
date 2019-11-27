using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Nep.Project.Web.WebServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INepService" in both code and config file together.
	[ServiceContract]
	public interface INepService
	{
        [OperationContract]
        WSResponse.PaymentSlip GetPaymentSlip(string paymentID);
        [OperationContract]
        WSResponse.SavePaymentSlipResponse SavePaymentSlip(WSResponse.SavePaymentSlip p);
    }
}
