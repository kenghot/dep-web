﻿using Autofac.Integration.Web.Forms;
using Nep.Project.DBModels.Model;
using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Requests;
using Nep.Project.ServiceModels.API.Responses;
using Nep.Project.ServiceModels.ProjectInfo;
using Nep.Project.ServiceModels.Report.ReportProjectRequest;
using Nep.Project.ServiceModels.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// update กิจกรรม
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("UpdateProcessActivity/{projId}/{processId}")]
        [HttpPost]
        public ReturnObject<decimal?> UpdateProcessActivity([Required][FromUri] decimal projId, [Required][FromUri] decimal processId, [FromBody] BaseActivity p)
        {
            var result = new ReturnObject<decimal?>();
            result.IsCompleted = false;
            try
            {

                var db = projService.GetDB();
                var pc = db.PROJECTPROCESSEDs.Where(w => w.PROCESSID == processId && w.PROJECTID == projId).FirstOrDefault();
                if (pc == null)
                {
                    result.Message.Add($"ไม่พบโครงการที่ระบุ (no. {projId})");
                    return result;
                }

                copyProjectProcessed(p, pc);
                //db.PROJECTPROCESSEDs.Add(pc);
                db.SaveChanges();
                result.Data = pc.PROCESSID;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        /// <summary>
        /// เพิ่ม กิจกรรม
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("AddProcessActivity/{projId}")]
        [HttpPost]
        public ReturnObject<decimal?> AddProcessActivity([FromUri]decimal projId, [FromBody]BaseActivity p)
        {
            var result = new ReturnObject<decimal?>();
            result.IsCompleted = false;
            try
            {

                var db = projService.GetDB();
                var proj = db.ProjectGeneralInfoes.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add($"ไม่พบโครงการที่ระบุ (no. {projId})");
                    return result;
                }
                var pc = new PROJECTPROCESSED();
                pc.PROJECTID = projId;
                copyProjectProcessed(p, pc);
                db.PROJECTPROCESSEDs.Add(pc);
                db.SaveChanges();
                result.Data = pc.PROCESSID;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        private void copyProjectProcessed(BaseActivity source, PROJECTPROCESSED dest)

        {
            var db = projService.GetDB();
            var p = db.MT_Province.Where(w => w.SectionID == 1).FirstOrDefault();
            
            dest.PROVINCEID = p.ProvinceID;
            dest.DESCRIPTION = source.Description;
            dest.LATITUDE = source.Latitude;
            dest.LONGITUDE = source.Longitude;
            dest.PROCESSSTART = source.ProcessStart;
            dest.PROCESSEND = source.ProcessEnd;
           // dest.PROCESSID = source.ProcessID.Value;

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
                result.SetExceptionMessage(ex);
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
                var pjs = projService.ListProjectInfoList(QueryParameter,false);
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

        #region Participant Survey 
        [Route("GetParticipantSurvey/{projId}")]
        [HttpGet]
        public ReturnObject<ParticipantSurvey> GetParticipantSurvey([FromUri]decimal projId)
        {
            var result = new ServiceModels.ReturnObject<ParticipantSurvey>();
            result.IsCompleted = false;
            try
            {
                result.Data = new ParticipantSurvey();
                var db = projService.GetDB();
                var svs = db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == projId && w.QUESTGROUP == "PARTICIPANTSV").ToList();
                var details = new List<ParticipantSurveyDetail>();
                result.Data.Surveys = details;
                foreach (var sv in svs)
                {
                    var detail = new ParticipantSurveyDetail
                    {
                        DocId = sv.QUESTHDID,
                        CreateDatetime = sv.CREATEDDATE
                    };
                    JObject json = JObject.Parse(sv.DATA);
                    JToken activity;
                     if (json.TryGetValue("activity",out activity))
                    {
                        detail.Activity = json.GetValue( "activity").ToString();
                    }
                    details.Add(detail);
                }
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        #endregion
    }

}
