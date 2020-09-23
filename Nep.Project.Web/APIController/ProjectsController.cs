using Autofac.Integration.Web.Forms;
using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Responses;
using Nep.Project.ServiceModels.Report.ReportProjectRequest;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private IServices.IProjectInfoService projService { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        private SecurityInfo userInfo { get; set; }
        public ProjectsController(IServices.IProjectInfoService ps, SecurityInfo UserInfo, IServices.IAttachmentService attach)
        {
            projService = ps;
            userInfo = UserInfo;
            attachService = attach;
        }

        [Route("GetProjectListScreen/{id}")]
        [HttpPost]
        public ReturnObject<ProjectProcessedScreen> GetProjectListScreen([FromUri]decimal id)
        {
            var result = new ReturnObject<ProjectProcessedScreen>();
            result.IsCompleted = false;
            try
            {
               if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์เข้าถึงข้อมูล");
                    return result;
                }
                result.Data = new ProjectProcessedScreen();
                var db = projService.GetDB();
                var acts = db.PROJECTPROCESSEDs.Where(w => w.PROJECTID == id).Select(s => new Activity
                {
                    Description = s.DESCRIPTION,
                    Latitude = s.LATITUDE,
                    Longitude = s.LONGITUDE,
                    ProcessEnd = s.PROCESSEND,
                    ProcessStart = s.PROCESSSTART,
                    ProcessID = s.PROCESSID
                }).ToList();
                var path = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/UploadImages/";
                foreach (var act in acts)
                {
                    act.ImageAttachments = db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == act.ProcessID && w.QUESTGROUP == "ACTIVITYIMG")
                        .Select(s => new UploadImageResponse
                        {
                           
                            ImageName = s.DATA,
                            ImageId = s.QUESTHDID
                            })
                        .ToList();
                    foreach (var i in act.ImageAttachments)
                    {
                        i.ImageFullPath = path + i.ImageName;
                    }
                }
                result.Data.Activities = acts;
                
                result.IsCompleted = true;
            }
            catch (Exception ex) {
                result.Message.Add(ex.Message);
                result.Message.Add(ex.InnerException.Message);
            }
            return result;   
        }
    }

}
