using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nep.Project.ServiceModels;
using System.Web.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Nep.Project.ServiceModels.API.Requests;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Nep.Project.Web.API
{
    public class APIHelper
    {
        static HttpClient client = new HttpClient();
        public static ReturnObject<DesPersonWS.getDesPersonResponse> GetDesPerson(string citizenID)
        {

            ReturnObject<DesPersonWS.getDesPersonResponse> ret = new ReturnObject<DesPersonWS.getDesPersonResponse>();
            ret.IsCompleted = false;
            try
            {
                var ws = new DesPersonWS.WebService();



                var p = new DesPersonWS.getDesPersonRequest();
                string url = WebConfigurationManager.AppSettings["DES_PERSON_WS_URL"];
                p.username = WebConfigurationManager.AppSettings["DES_PERSON_WS_USER"];
                p.password = WebConfigurationManager.AppSettings["DES_PERSON_WS_PASSWORD"];
                ws.Url = url;
                if (url == null || p.username == null || p.password == null)
                {
                    ret.Message.Add("DES_PERSON_WS is not set.");
                    return ret;

                }
                p.person_code = citizenID;
                var res = ws.getDesPerson(p);
                if (res.return_code != "0")
                {
                    ret.Message.Add(res.return_message);
                    return ret;
                }
                else
                {
                    ret.IsCompleted = true;
                    ret.Data = res;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                ret.Message.Add(ex.Message);
                return ret;
            }

            return ret;
        }
        public static string CheckIDFromDisability(string citizenID)
        {

            string ret = "";

            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("the_id", citizenID)
                    });
                var post =  client.PostAsync("http://job.dep.go.th/ajax_get_des_person.php", formContent).Result;
        
                var stringContent =   post.Content.ReadAsStringAsync().Result;

                ret = stringContent;

              
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }

            return ret;
        }
        public static ReturnObject<decimal?>   SendSueCase(SueCase sc)
        {

            var  ret = new ReturnObject<decimal?>();
            ret.IsCompleted = true;
            try
            {
                var content = new StringContent( JsonConvert.SerializeObject(sc), Encoding.UTF8,   "application/json");
                var post = client.PostAsync("http://203.154.94.105/law_system/rest_save_case.php", content).Result;

                var stringContent = post.Content.ReadAsStringAsync().Result;

                JObject o = JObject.Parse(stringContent);
                var  status = o["status"].ToString();
                ret.IsCompleted = (status == "1") ? true : false;
                if (ret.IsCompleted)
                {
                    decimal id;
                    if (decimal.TryParse(o["case_id"].ToString(),out id))
                    {
                        ret.Data = id;
                    }else
                    {
                        ret.IsCompleted = false;
                        ret.Message.Add("ไม่พบรหัสคดี จากระบบดำเนินคดี");
                    }
                }else
                {
                    ret.Message.Add(o["status_message"].ToString());
                }



            }
            catch (Exception ex)
            {
                ret.SetExceptionMessage(ex);
            }

            return ret;
        }
    }
}