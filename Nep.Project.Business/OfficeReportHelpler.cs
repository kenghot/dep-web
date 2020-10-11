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

namespace Nep.Project.Business
{
    public static class OfficeReportHelpler
    {
        //public static ReturnObject<HttpResponseMessage> GetReportExcelFile(string ReportFolder, string ReportName, object data)
        //{
        //    var ret = new ReturnObject<HttpResponseMessage>();

        //    ret.IsCompleted = false;
        //    try
        //    {
        //        string mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // MIME header with default value

                
        //        var assembly = typeof(Nep.Project.Resources.Message).Assembly; // .GetTypeInfo().Assembly;
        //        var names = assembly.GetManifestResourceNames();
        //        XLTemplate template;
        //        var excel = assembly.GetManifestResourceStream($"mof.Resources.{ReportFolder}.{ReportName}.xlsx");

        //        template = new XLTemplate(excel);



        //        // var template = new XLTemplate (webRootPath + $"/{ReportFolder}/{ReportName}.xlsx");


        //        template.AddVariable(data);
        //        template.Generate();

        //        try
        //        {

        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                template.SaveAs(stream);
        //                //var wb = new XLWorkbook(stream);
        //               //var file = new FileContentResult(stream.ToArray(), mime);
        //                var result = new HttpResponseMessage(HttpStatusCode.OK)
        //                {
        //                    Content = new ByteArrayContent(stream.ToArray())
        //                };
        //                result.Content.Headers.ContentDisposition =
        //                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //                    {
        //                        FileName = $"{ReportName}_{DateTime.Now.Ticks.ToString()}.xlsx"
        //            };
        //                //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //                result.Content.Headers.ContentType = new MediaTypeHeaderValue(mime);

        //                // file.FileDownloadName = $"{ReportName}_{DateTime.Now.Ticks.ToString()}.xlsx";
        //                ret.IsCompleted = true;
        //                ret.Data = result;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ret.SetExceptionMessage(ex);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ret.SetExceptionMessage(ex);

        //    }
        //    return ret;
        //}
    
    }
}
