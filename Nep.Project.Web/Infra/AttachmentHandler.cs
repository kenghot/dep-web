using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Forms;
using Nep.Project.Common;

namespace Nep.Project.Web.Infra
{
    [InjectProperties]
    public class AttachmentHandler : IHttpHandler
    {
        public IServices.IAttachmentService Service { get; set; }
        public IServices.IProjectInfoService pService { get; set; }
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var action = Path.GetFileName(context.Request.FilePath).ToLower();
            var httpMethod = context.Request.HttpMethod.ToLower();

            if (action.Equals("upload") && httpMethod.Equals("post"))
            {
                SaveAttachment(context);
            }
            else if (action.Equals("remove") && httpMethod.Equals("post"))
            {
                RemoveAttachment(context);
            }
            else if (action.Equals("view") && httpMethod.Equals("get"))
            {
                ViewAttachment(context);
            }
            else if (action.Equals("savetodb") && httpMethod.Equals("post"))
            {
                SaveToDB(context);
            }
        }
        private void SaveToDB(HttpContext context)
        {
            String strValues = ""; //= context.Request.Form[0];
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                strValues = reader.ReadToEnd();
            }

            var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
            decimal projID;
            decimal.TryParse(json["projID"].ToString(),out projID);
            List<ServiceModels.KendoAttachment> removeFiles = json["removeFiles"].ToObject < List<ServiceModels.KendoAttachment>>();
            List <ServiceModels.KendoAttachment> addFiles = json["addFiles"].ToObject<List<ServiceModels.KendoAttachment>>();
            string fname = json["fieldName"].ToString();
            string tname = json["tableName"].ToString();
       
      

            pService.SaveAttachFile(projID, Common.LOVCode.Attachmenttype.PROJECT_INFORMATION, removeFiles, addFiles, tname, fname);
        }
        private void ViewAttachment(HttpContext context)
        {
            var path = context.Request.PathInfo.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (path.Length < 1)
            {
                return;
            }

            var filename = path[path.Length - 1];
            var filepath = String.Empty;
            var etag = String.Empty;

            var mode = path[0].ToLower();
            //var fileId = path[1];
            if (mode == "temp")
            {
                var fileTempId = path[1];
                Guid guid;
                if (Guid.TryParseExact(fileTempId, "N", out guid))
                {
                    filepath = context.Server.MapPath(Constants.UPLOAD_TEMP_PATH + fileTempId);
                    etag = fileTempId;
                }
            }else if (mode == "project")
            {
                if (path.Length < 4)
                {
                    return;
                }
                String parentId = path[1];
                Decimal attachment_id;
                if (Decimal.TryParse(path[2], out attachment_id))
                {
                    etag = path[2];
                    var data = Service.GetAttachmentInfoForDownload(mode, parentId, attachment_id);
                    if (data.IsCompleted)
                    {
                        filepath = data.Data.PathName;
                        filename = data.Data.AttachmentFileName;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (path.Length < 3)
                {
                    return;
                }
                
                Decimal attachment_id;
                if (Decimal.TryParse(path[1], out attachment_id))
                {
                    etag = path[1];
                    var data = Service.GetAttachmentInfoForDownload(mode, String.Empty, attachment_id);
                    if (data.IsCompleted)
                    {
                        filepath = data.Data.PathName;
                        filename = data.Data.AttachmentFileName;
                    }
                }
                else
                {
                    return;
                }

            }
            if (!String.IsNullOrWhiteSpace(filepath) && !String.IsNullOrWhiteSpace(filename) && !String.IsNullOrWhiteSpace(etag))
            {
                TransmitFile(context.Request, context.Response, filepath, filename, etag);
            }
        }

        private Boolean IsValidFileName(String filename)
        {
            var result = true;


            return result;
        }

        private void FileNotFound(HttpResponse response)
        {
            response.StatusCode = (Int32)HttpStatusCode.NotFound;
            response.End();
        }

        private void PreconditionFailed(HttpResponse response)
        {
            response.StatusCode = (Int32)HttpStatusCode.PreconditionFailed;
            response.End();
        }

        private void FileNotModified(HttpResponse response)
        {
            response.StatusCode = (Int32)HttpStatusCode.NotModified;
            response.End();
        }

        /// <summary>
        /// Writes the file stored in the filesystem to the response stream without buffering in memory, ideal for large files. Supports resumable downloads.
        /// </summary>
        private void TransmitFile(HttpRequest request, HttpResponse response, String filepath, String filename, String etag)
        {
            response.Clear();
            response.Buffer = false; 

            FileInfo fileInfo = new FileInfo(filepath);

            if (String.IsNullOrWhiteSpace(filepath) || !fileInfo.Exists) {
                FileNotFound(response);
            }

            Int32 fileSize = (Int32)fileInfo.Length;

            Int32 responseLength = fileSize;
            Int32 startIndex = 0;

            //if the "If-Match" exists and is different to etag (or is equal to any "*" with no resource) then return 412 precondition failed
            if (request.Headers["If-Match"] != null && request.Headers["If-Match"] != "*" && request.Headers["If-Match"] != etag) {
                PreconditionFailed(response);
            }

            if (request.Headers["If-None-Match"] == etag) {
                FileNotModified(response);
            }

            if (request.Headers["Range"] != null && (request.Headers["If-Range"] == null || request.Headers["IF-Range"] == etag)) {
                var match = Regex.Match(request.Headers["Range"], "bytes=(\\d*)-(\\d*)");
                startIndex = Int32.Parse(match.Groups[1].Value);
                responseLength = (!String.IsNullOrWhiteSpace(match.Groups[2].Value)? Int32.Parse(match.Groups[2].Value) + 1 : responseLength) - startIndex;
                response.StatusCode = (Int32)(HttpStatusCode.PartialContent);
                response.Headers["Content-Range"] = string.Format(" bytes {0}-{1}/{2}", startIndex, (startIndex + responseLength - 1), fileSize);
            }

            response.AddHeader("content-disposition", "filename=" + HttpUtility.UrlEncode(filename, Encoding.UTF8).Replace("+", "%20"));

            response.ContentType = System.Web.MimeMapping.GetMimeMapping(filename);
            response.AppendHeader("Connection", "Keep-Alive"); 
            response.Headers["Accept-Ranges"] = "bytes";
            response.Headers["Content-Length"] = responseLength.ToString();
            response.Cache.SetCacheability(HttpCacheability.Public);
            //required for etag output
            response.Cache.SetETag(etag);
            response.Cache.SetLastModified(fileInfo.LastWriteTimeUtc);
            //required for IE9 resumable downloads

            response.TransmitFile(fileInfo.FullName, startIndex, responseLength);
        }

        private void SaveAttachment(HttpContext context)
        {
            var uploadedFiles = context.Request.Files;
            var kendoFiles = new List<ServiceModels.KendoAttachment>();
            var isError = false;
            try
            {
                foreach (String key in uploadedFiles)
                {
                    var file = uploadedFiles[key];
                    var kendoFile = new ServiceModels.KendoAttachment();
                    var tempFileName = Guid.NewGuid().ToString("N");

                    kendoFile.extension = Path.GetExtension(file.FileName);
                    kendoFile.name = Path.GetFileName(file.FileName);
                    kendoFile.size = file.ContentLength;
                    kendoFile.tempId = tempFileName;

                    file.SaveAs(context.Server.MapPath(Constants.UPLOAD_TEMP_PATH + tempFileName));
                    kendoFiles.Add(kendoFile);
                }
            }
            catch (Exception ex)
            {
                //Delete saved file
                foreach (var file in kendoFiles)
                {
                    File.Delete(context.Server.MapPath(Constants.UPLOAD_TEMP_PATH + file.tempId));
                }
                kendoFiles.Clear();
                isError = true;
                Common.Logging.LogError(Common.Logging.ErrorType.WebError, "Upload", ex);
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (!isError)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(Common.Web.WebUtility.ToJSON(kendoFiles));
            }
            else
            {
                context.Response.Write("การอัพโหลดไฟล์มีปัญหา");
            }
        }

        private void RemoveAttachment(HttpContext context)
        {
            var isError = false;
            try
            {
                var tempId = context.Request.Form["tempId"];
                if (!String.IsNullOrWhiteSpace(tempId))
                {
                    Guid result;
                    if (Guid.TryParseExact(tempId, "N", out result))
                    {
                        File.Delete(context.Server.MapPath(Constants.UPLOAD_TEMP_PATH + tempId));
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                Common.Logging.LogError(Common.Logging.ErrorType.WebError, "Upload", ex);
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (!isError)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("");
            }
            else
            {
                context.Response.Write("การลบไฟล์มีปัญหา");
            }
        }

        #endregion
    }
}
