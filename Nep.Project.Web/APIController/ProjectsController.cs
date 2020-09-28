using Autofac.Integration.Web.Forms;
using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Requests;
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

        [Route("GetActivityListScreen/{id}")]
        [HttpPost]
        public ReturnObject<ActivityScreen> GetActivityListScreen([FromUri] decimal id)
        {
            var result = new ReturnObject<ActivityScreen>();
            result.IsCompleted = false;
            try
            {
                if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์เข้าถึงข้อมูล");
                    return result;
                }
                result.Data = new ActivityScreen();
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
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                result.Message.Add(ex.InnerException.Message);
            }
            return result;
        }
        [Route("GetProjectListScreen")]
        [HttpPost]
        public ReturnObject<ProjectScreen> GetProjectListScreen([FromBody]Paging p)
        {
            var result = new ReturnObject<ProjectScreen>();
            result.IsCompleted = false;
            try
            {
                if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์เข้าถึงข้อมูล");
                }
                var filter = CreateFilter(false);

                System.Web.UI.WebControls.SortDirection sort = (System.Web.UI.WebControls.SortDirection)p.SortDirection;
                ServiceModels.QueryParameter QueryParameter =  Nep.Project.Common.Web.NepHelper.ToQueryParameter(filter, p.PageIndex, p.PageSize, "ProjectInfoID", System.Web.UI.WebControls.SortDirection.Descending);
                var pjs = projService.ListProjectInfoList(QueryParameter);
                if (pjs.IsCompleted)
                {
                    result.Data = new  ProjectScreen ();
                    result.Data.Dashboard = new ProjectDashboard
                    {
                        TotalProject = pjs.TotalRow
                    };
                    var projects = new List<ServiceModels.API.Responses.Project>();
                    result.Data.Projects = projects;
                    var db = projService.GetDB();
                    foreach (var pj in pjs.Data)
                    {
                        var item = new ServiceModels.API.Responses.Project
                        {
                            BudgetAmount = pj.BudgetValue,
                            BudgetYear = pj.BudgetYear,
                            EndDate = pj.ProjectEndDate,
                            StartDate = pj.CreatedDate,
                            FollowUp = pj.FollowupStatusName,
                            OrganizationNameTH = pj.OrganizationName,
                            ProjectId = pj.ProjectInfoID,
                            ProjectNameTH = pj.ProjectName,
                            ProvinceName = pj.ProvinceName,
                        };
                        projects.Add(item);
                        var acts = db.PROJECTPROCESSEDs.Where(w => w.PROJECTID == item.ProjectId)
                            .Select(s => db.PROJECTQUESTIONHDs.Where(h => h.QUESTGROUP == "ACTIVITYIMG" && h.PROJECTID == s.PROCESSID).Select(f => f.DATA).ToList()).ToList() ;

                        item.Thumbnail = acts.SelectMany(s => s).Select(t => $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/UploadImages/{t}").ToList();
                        item.TotalActivity = acts.Count();
                    }
                   
                }else
                {
                    result.Message = pjs.Message;
                    return result;
                }
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        [Route("GetProjectInformation/{id}")]
        [HttpGet]
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> GetProjectInformation([FromUri]Decimal id)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo>();
            result.IsCompleted = false;
            try
            {
                result = projService.GetProjectInformationByProjecctID(id);
            }catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        private List<ServiceModels.IFilterDescriptor> CreateFilter(bool isFilterFollowup)
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
          


            //ปีงบประมาณ

            fields.Add(new ServiceModels.FilterDescriptor()
            {
                Field = "BudgetYear",
                Operator = ServiceModels.FilterOperator.IsEqualTo,
                Value = DateTime.Now.Year
            });


            fields.Add(new ServiceModels.FilterDescriptor()
            {
                Field = "ApprovalStatus",
                Operator = ServiceModels.FilterOperator.IsNotEqualTo,
                Value = "0"
            });


            fields.Add(new ServiceModels.FilterDescriptor()
            {
                Field = "ProjectApprovalStatusCode",
                Operator = ServiceModels.FilterOperator.IsEqualTo,
                Value = Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว
            });


            //filter by user login 
            List<ServiceModels.IFilterDescriptor> productFilters = new List<ServiceModels.IFilterDescriptor>();
            if ((!userInfo.IsProvinceOfficer) && (!userInfo.IsCenterOfficer))
            {
                #region Organization
                decimal userOrgId = (userInfo.OrganizationID.HasValue) ? (decimal)userInfo.OrganizationID : 0;
                List<ServiceModels.IFilterDescriptor> productOrgFilter = new List<ServiceModels.IFilterDescriptor>();
                List<ServiceModels.IFilterDescriptor> productOrgCreateByOfficerFilter = new List<ServiceModels.IFilterDescriptor>();

                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = userOrgId
                });
                #endregion Organization
            }
            else if (userInfo.IsProvinceOfficer)
            {
                #region IsProvinceOfficer
                decimal provinceID = (userInfo.ProvinceID.HasValue) ? (decimal)userInfo.ProvinceID : 0;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = provinceID
                });

          
                #endregion IsProvinceOfficer
            }
            else if (userInfo.IsAdministrator)
            {
                #region IsAdministrator
                
                #endregion IsAdministrator
            }
            else if (userInfo.IsCenterOfficer)
            {
                #region IsCenterOfficer
                decimal provinceID = (userInfo.ProvinceID.HasValue) ? (decimal)userInfo.ProvinceID : 0;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = provinceID
                });
                #endregion IsCenterOfficer
            }

            List<ServiceModels.IFilterDescriptor> filters = new List<IFilterDescriptor>();

            filters.Add(new ServiceModels.CompositeFilterDescriptor
            {
                LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                FilterDescriptors = fields
            });

            return filters;

        }
    }

}
