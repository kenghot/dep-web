
using Autofac.Integration.Web.Forms;
using Ionic.Zip;
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
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public ReturnObject<decimal?> AddProcessActivity([FromUri] decimal projId, [FromBody] BaseActivity p)
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
        public ReturnObject<ProjectScreen> GetProjectListScreen([FromBody] Paging p)
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
                ServiceModels.QueryParameter QueryParameter = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filter, p.PageIndex, p.PageSize, "ProjectInfoID", System.Web.UI.WebControls.SortDirection.Descending);
                var pjs = projService.ListProjectInfoList(QueryParameter, false);
                if (pjs.IsCompleted)
                {
                    result.Data = new ProjectScreen();
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
                            .Select(s => db.PROJECTQUESTIONHDs.Where(h => h.QUESTGROUP == "ACTIVITYIMG" && h.PROJECTID == s.PROCESSID).Select(f => f.DATA).ToList()).ToList();

                        item.Thumbnail = acts.SelectMany(s => s).Select(t => $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/UploadImages/{t}").ToList();
                        item.TotalActivity = acts.Count();
                    }

                }
                else
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
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> GetProjectInformation([FromUri] Decimal id)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo>();
            result.IsCompleted = false;
            try
            {
                result = projService.GetProjectInformationByProjecctID(id);
            }
            catch (Exception ex)
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
        public ReturnObject<ParticipantSurvey> GetParticipantSurvey([FromUri] decimal projId)
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
                    if (json.TryGetValue("activity", out activity))
                    {
                        detail.Activity = json.GetValue("activity").ToString();
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
        [HttpPost]
        [Route("CreateEPayment")]
        public HttpResponseMessage CreateEPayment([FromBody] List<string> projs)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var projIds = projs.Select(s => decimal.Parse(s));
                var db = projService.GetDB();

                var gens = db.ProjectGeneralInfoes.Where(w => projIds.Contains(w.ProjectID)).ToList();
                var dataTxt = new StringBuilder();
                var senderBank = "006";
                var senderAcc = "1234567890";
                var senderBranch = "012";
                var status = "00";
                int totRec = 0;
                decimal totAmt = 0;
                foreach (var gen in gens)
                {
                    totRec++;
                    totAmt += gen.BudgetReviseValue.Value;
                    var org = db.MT_Organization.Where(w => w.OrganizationID == gen.OrganizationID).FirstOrDefault();
                    if (gen.ProjectContract == null)
                    {
                        //response = Request.CreateResponse(HttpStatusCode.NoContent);
                        //response.Content = new StringContent($"ไม่พบข้อมูลสัญญาของโครงการ ({gen.ProjectInformation.ProjectNameTH})");
                        return Request.CreateResponse(HttpStatusCode.NotFound,new HttpError($"ไม่พบข้อมูลสัญญาของโครงการ ({gen.ProjectInformation.ProjectNameTH})")); 
                    }
                    if (org == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"ไม่พบหน่วยงานที่ระบุ (project no {gen.ProjectID})"));
                    }
                    OrganizationExtend oex = new OrganizationExtend();
                    try
                    {
                        oex = Newtonsoft.Json.JsonConvert.DeserializeObject<OrganizationExtend>(org.EXTENDDATA);
                    }
                    catch (Exception ex)
                    {
                      
                
                    }
                    if (string.IsNullOrWhiteSpace(oex.BranchNo) || string.IsNullOrWhiteSpace(oex.AccountName) || string.IsNullOrWhiteSpace(oex.AccountNo) || string.IsNullOrWhiteSpace(oex.BankNo))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"ไม่พบข้อมูลธนาคารของหน่วยงาน ({org.OrganizationNameTH})"));
                    }
                    string txt = "";
                    txt = $"102{totRec.ToString().PadLeft(6, '0')}{oex.BankNo.PadRight(10, ' ').Substring(0, 3)}{oex.BranchNo.PadRight(10, ' ').Substring(0, 4)}";
                    txt += $"{oex.AccountNo.PadLeft(11, '0').Substring(0, 11)}{senderBank.PadRight(10, ' ').Substring(0, 3)}{senderBranch.PadRight(10, ' ').Substring(0, 4)}{senderAcc.PadLeft(15, '0').Substring(0, 11)}";
                    txt += $"{DateTime.Now:ddMMyyyy}0600{string.Format("{0:0.00}",gen.BudgetReviseValue).PadRight(20, ' ').Substring(0, 17)}{"".PadRight(8, ' ')}{"".PadRight(10, ' ')}";
                    txt += $"{oex.AccountName.PadRight(100,' ').Substring(0,100)}{"ชื่อทดสอบ ผู้โอน".PadRight(100, ' ')}{"other info 1".PadRight(40, ' ')}{"DDA Ref 1 ".PadRight(18, ' ')}";
                    txt += $"  {"DDA Ref 2".PadRight(18, ' ')}  {"other inf 2".PadRight(20, ' ')}000000{status}{gen.ProjectPersonel.Email1.PadRight(40,' ').Substring(0,40)}";
                    txt += $"{"".PadRight(20, ' ').Substring(0, 20)}{"".PadRight(38,' ')}\r\n";
                    dataTxt.Append(txt);
                    
                }

                var head = $"101{DateTime.Now.Ticks.ToString().PadLeft(6, '0').Substring(0, 6)}{senderBank.PadRight(3, ' ').Substring(0, 3)}{totRec.ToString().PadRight(7, ' ').Substring(0, 7)}";
                   head += $"{string.Format("{0:0.00}",totAmt).PadRight(19,' ').Substring(0,19)}{DateTime.Now:ddMMyyyy}C{"".PadRight(8,' ')}{"".PadRight(16, ' ')}{"".PadRight(20, ' ')}{"".PadRight(407, ' ')}\r\n";
                dataTxt.Insert(0, head);
                string strDT = $"{DateTime.Now:ddMMyyyyhhmmss}";
                var txtSW = $"UPLD_SERVICE_NAME=KTB iPay Direct 04\r\n";
                txtSW += $"UPLD_COMP_ID=SSBT027805\r\n";
                txtSW += $"UPLD_USER_NAME=SYSTEM\r\n";
                txtSW += $"UPLD_NTFD_EMAIL=Programmer.isc@siamsmile.co.th\r\n";
                txtSW += $"UPLD_NTFD_SMS=0847774204\r\n";
                txtSW += $"UPLD_ENCRYPT=Y\r\n";
                txtSW += $"FILE_NAME=DDR{strDT}.txt";
                var compressedFileStream = new MemoryStream();

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zip.AddDirectoryByName("Files");
                    //foreach (FileModel file in files)
                    //{
                    //    if (file.IsSelected)
                    //    {
                    //        zip.Ad file.FilePath, "Files");
                    //    }
                    //}
                   
                    var DDR = Encoding.UTF8.GetBytes(dataTxt.ToString());
                    var SW = Encoding.UTF8.GetBytes(txtSW); ;
                    zip.AddEntry($"DDR{strDT}.txt", DDR);
                    zip.AddEntry($"SW_DDR{strDT}.txt", SW);
                    //zip.AddFile("e:\\qr_cashcard_60060001-60060500.txt");

                    //Set the Name of Zip File.
                    string zipName = String.Format("ePayment_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    zip.Save("e:\\" + zipName);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        //Save the Zip File to MemoryStream.
                        zip.Save(memoryStream);
                        //memoryStream.Seek(0, SeekOrigin.Begin);
                        //Set the Response Content.
                        response.Content = new ByteArrayContent(memoryStream.ToArray());

                        //Set the Response Content Length.
                        response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

                        //Set the Content Disposition Header Value and FileName.
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        response.Content.Headers.ContentDisposition.FileName = zipName;

                        //Set the File Content Type.
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                        return response;
                    }
                }
                // create a working memory stream
                //using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                //{
                //    // create a zip
                //    using (System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                //    {

                //        // add the item name to the zip
                //        System.IO.Compression.ZipArchiveEntry zipDDR = zip.CreateEntry("DDR" + DateTime.Now.Ticks.ToString() + ".txt");
                       
                //        var DDR = Encoding.UTF8.GetBytes("DDR ทดสอบ");
                      
                //        // add the item bytes to the zip entry by opening the original file and copying the bytes
                //        using (System.IO.MemoryStream originalFileMemoryStream = new System.IO.MemoryStream(DDR))
                //        {
                //            using (System.IO.Stream entryStream = zipDDR.Open())
                //            {
                //                originalFileMemoryStream.CopyTo(entryStream);
                //            }
                //        }
                //        System.IO.Compression.ZipArchiveEntry zipSW = zip.CreateEntry("SW" + DateTime.Now.Ticks.ToString() + ".txt");
                //        var SW = Encoding.UTF8.GetBytes("SW ทดสอบ");
                //        using (System.IO.MemoryStream originalFileMemoryStream = new System.IO.MemoryStream(SW))
                //        {
                //            using (System.IO.Stream entryStream = zipSW.Open())
                //            {
                //                originalFileMemoryStream.CopyTo(entryStream);
                //            }
                //        }
                //    }
                //    fileBytes = memoryStream.ToArray();
                //    //Set the Response Content.
                //    response.Content = new ByteArrayContent(fileBytes);

                //    //Set the Response Content Length.
                //    response.Content.Headers.ContentLength = fileBytes.LongLength;

                //    //Set the Content Disposition Header Value and FileName.
                //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //    response.Content.Headers.ContentDisposition.FileName = "zip" + DateTime.Now.Ticks.ToString() + ".zip";

                //    //Set the File Content Type.
                //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                //}

                ////// download the constructed zip
                ////Response.AddHeader("Content-Disposition", "attachment; filename=download.zip");
                ////return File(fileBytes, "application/zip");
                //return response;
            }
            catch (Exception ex)
            {
       
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }

        }
    }

}
