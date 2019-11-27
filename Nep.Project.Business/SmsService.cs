using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Nep.Project.Business
{
    public class SmsService
    {
        private static String EncodeQueryString(Dictionary<String, String> queryString)
        {
            var sb = new System.Text.StringBuilder();

            foreach (KeyValuePair<String, String> pair in queryString)
            {
                sb.AppendFormat(
                    "&{0}={1}"
                    , pair.Key
                    , System.Net.WebUtility.UrlEncode(pair.Value));
            }

            sb.Remove(0, 1);

            return sb.ToString();
        }

        private static Boolean IsValidMobileNo(String mobileNo)
        {
            Boolean result = true;

            if (mobileNo != null)
            {
                mobileNo = mobileNo.Replace("-", "").Replace(" ", "");
            }

            Func<Boolean>[] conditions = new Func<bool>[]
            {
                () => mobileNo != null,
                () => mobileNo.All(x=> (x >= '0' && x <= '9')|| x == '+'),
                () => mobileNo.StartsWith("06") || mobileNo.StartsWith("08") || mobileNo.StartsWith("09") 
                    || mobileNo.StartsWith("666") || mobileNo.StartsWith("668") || mobileNo.StartsWith("669")
                    || mobileNo.StartsWith("+666") || mobileNo.StartsWith("+668") || mobileNo.StartsWith("+669")
            };

            foreach (var c in conditions)
            {
                if (!c())
                {
                    result = false;
                    break;
                }
            }

            return result;
        }


        private static async Task SendCAT4SMS(String message, params String[] mobileNo)
        {
            try
            {
                StringBuilder mobile = new StringBuilder();

                if (mobileNo != null)
                {
                    foreach (var no in mobileNo)
                    {
                        if (no != null)
                        {
                            var splitNo = no.Split(new Char[]{',', '/'}, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var number in splitNo)
                            {
                                if (IsValidMobileNo(number))
                                {
                                    mobile.Append(number);
                                    mobile.Append(",");
                                }
                            }
                        }
                    }
                }
                if (mobile.Length > 0)
                {
                    mobile.Remove(mobile.Length - 1, 1);
                }

                Dictionary<String, String> query = new Dictionary<String, String>()
                {
                    {"username", Common.Constants.SMS_SERVICE_USERNAME},
                    {"password", Common.Constants.SMS_SERVICE_PASSWORD},
                    {"msisdn", mobile.ToString()},
                    {"message", message},
                    {"sender",  Common.Constants.SMS_SENDER},
                    {"ScheduledDelivery",""}
                };

                using (HttpClient client = new HttpClient())
                {
                    UriBuilder uri = new UriBuilder(Common.Constants.SMS_SERVICE_URL);
                    uri.Query = EncodeQueryString(query);
                    using (var result = await client.GetAsync(uri.Uri))
                    {
                        using (var stream = await result.Content.ReadAsStreamAsync())
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(stream);

                            var statusNode = xml.SelectSingleNode("/SMS/Status");
                            if (statusNode != null && statusNode.InnerText == "0")
                            {
                                var detailNode = xml.SelectSingleNode("/SMS/Detail");
                                if (detailNode != null)
                                {
                                    Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "sms", detailNode.InnerText);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "sms", ex);
            }
        }

        public void Send(String message, params String[] mobileNo)
        {
            if (mobileNo == null || mobileNo.Length == 0)// && mobileNo.Any(x => !IsValidMobileNo(x)))
            {
                return;
            }

            System.Threading.ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                var task = SendCAT4SMS(message, mobileNo);
            task.Wait();
        });
        }
        public void Send(String message,Nep.Project.DBModels.Model.NepProjectDBEntities db, decimal projID)
        {
            var p = db.ProjectGeneralInfoes.Where(w => w.ProjectID == projID).FirstOrDefault();
            if (p == null)
            {
                return;
            }
        
            var org = db.MT_Organization.Where(w => w.OrganizationID == p.OrganizationID).FirstOrDefault();
            List<string> mobileNo = new List<string>();
            if (org == null)
            {
                return;
            }
            //if (string.IsNullOrEmpty(org.Mobile))
            //{
            //    mobileNo.Add(org.Mobile);
            //}
            var users = db.SC_User.Where(su => su.OrganizationID == org.OrganizationID && su.Group.GroupCode == Common.UserGroupCode.องค์กรภายนอก);
            foreach (var m in users)
            {
                mobileNo.Add(m.TelephoneNo);
            }
            if ( mobileNo.Count == 0)// && mobileNo.Any(x => !IsValidMobileNo(x)))
            {
                return;
            }
            var pi = db.ProjectInformations.Where(w => w.ProjectID == projID).FirstOrDefault();
            if (pi == null)
            {
                return;
            }
            System.Threading.ThreadPool.QueueUserWorkItem(delegate (object state)
            {

                message = string.Format("{0} ({1})", message, pi.ProjectNameTH);
                var task = SendCAT4SMS(message, mobileNo.ToArray());
                task.Wait();
            });
        }
    }
}
