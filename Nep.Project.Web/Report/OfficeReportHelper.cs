using Nep.Project.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Extensions;
using ClosedXML.Report;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Web;

namespace Nep.Project.Web.Report
{
    public static class OfficeReportHelpler
    {
       
        public static ReturnMessage GetReportExcelFile(HttpResponse Response, string ReportFolder, string ReportName, object data)
        {
            var result = new ReturnMessage();
            result.IsCompleted = false;
            try
            {
                var rep = GetReportExcelFile(ReportFolder, ReportName, data);
                if (rep.IsCompleted)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + $"{ReportName}_{DateTime.Now.Ticks.ToString()}.xlsx");
                    Response.AddHeader("Content-Length", rep.Data.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.BufferOutput = true;
                    Response.OutputStream.Write(rep.Data, 0, rep.Data.Length);
                    Response.End();
                    rep.IsCompleted = true;
                }
                else
                {
                    result.Message = rep.Message;
                }
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;


        }
        private static ReturnObject<byte[]> GetReportExcelFile(string ReportFolder, string ReportName, object data)
        {
            var ret = new ReturnObject<byte[]>();
            ret.IsCompleted = false;
            try
            {
                string mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // MIME header with default value


                var assembly = typeof(Nep.Project.Report.Class1).Assembly; // .GetTypeInfo().Assembly;
                var names = assembly.GetManifestResourceNames();
                XLTemplate template;
                var excel = assembly.GetManifestResourceStream($"Nep.Project.Report.{ReportFolder}.{ReportName}.xlsx");

                template = new XLTemplate(excel);



                // var template = new XLTemplate (webRootPath + $"/{ReportFolder}/{ReportName}.xlsx");


                template.AddVariable(data);
                template.Generate();

                try
                {

                    using (MemoryStream stream = new MemoryStream())
                    {
                        template.SaveAs(stream);
                        //var wb = new XLWorkbook(stream);
                        //var file = new FileContentResult(stream.ToArray(), mime);
                        ret.Data = stream.ToArray();

                        ret.IsCompleted = true;

                    }
                }
                catch (Exception ex)
                {
                    ret.SetExceptionMessage(ex);

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
