using Nep.Project.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Report
{
    public partial class ShowReportService : System.Web.UI.Page
    {

        public ServiceModels.Security.SecurityInfo UserInfo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string err = "";

            var repUrl = ConfigurationManager.AppSettings["REPORT_URL"];

            try
            {
                HttpClient client = new HttpClient();
                long projid = -1;
                var id = long.TryParse(Request.QueryString["projectId"], out projid);
                var extension = string.IsNullOrEmpty(Request.QueryString["fileextension"]) ? "pdf" : Request.QueryString["fileextension"];
                var repName = Request.QueryString["report"];
                var obj = new { project_id = projid };
                var url = string.Format("{0}/nep/generate/document/report/{1}", repUrl, repName);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("sso_tid", UserInfo.TicketID);
                //client.DefaultRequestHeaders.Add("sso_tid", "e2b11999f84a4c6fa5af32f52129cbe2");
                var task = Task.Run(async () => await client.PostAsync(url, content));
                var tres = Task.Run(async () => await task.Result.Content.ReadAsStringAsync());
                var res = tres.Result;

                JObject j = JObject.Parse(res);
                if (j["error"] != null && !string.IsNullOrEmpty(j["error"].ToString()))
                {
                    err = j["error"].ToString();
                }
                else
                {
                    if (j["filename"] != null && !string.IsNullOrEmpty(j["filename"].ToString()))
                    {
                        Response.Redirect(string.Format("https://docs.google.com/gview?url={0}/file/nep/{1}.{2}", repUrl, j["filename"].ToString(), extension),false);
                        //Response.End();
                    }
                    else
                    {
                        err = "ไม่สามารถสร้างรายงานได้";
                    }
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;
                Common.Logging.LogError(Logging.ErrorType.WebError, "Show Report", ex);
            }
            if (err != "")
            {
                Label1.Text = string.Format("พบข้อผิดพลาด : {0}", err);
            }



        }

    }
}