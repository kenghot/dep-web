
using Autofac.Integration.Web.Forms;
using ClosedXML.Excel;
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
using System.Data;
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
                if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์ทำรายการ");
                    return result;
                }

                var db = projService.GetDB();
                var proj = db.ProjectGeneralInfoes.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add($"ไม่พบโครงการที่ระบุ (no. {projId})");
                    return result;
                }
                var pc = new PROJECTPROCESSED();
                pc.PROJECTID = projId;
                pc.ADDUSER = userInfo.UserID;
                pc.ADDDATETIME = DateTime.Now;
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
                    ProcessID = s.PROCESSID,
                    LogDetail = new DataLog { AddLog = new BaseDataLog { UserId = s.ADDUSER, LogDateTime = s.ADDDATETIME} }
                }).ToList();
                var path = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/UploadImages/";
                foreach (var act in acts)
                {
                    var au = act.LogDetail.AddLog;
                    if (au.UserId.HasValue)
                    {
                        var user = db.SC_User.Where(w => w.UserID == au.UserId.Value).FirstOrDefault();

                        if (user != null)
                        {
                            au.FirstName = user.FirstName;
                            au.LastName = user.LastName;
                        }
                        var uImg = db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == au.UserId.Value && w.QUESTGROUP == "USERIMG").FirstOrDefault();
                        if (uImg != null)
                        {
                            au.UserImage = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/UploadImages/{uImg.DATA}";
                        }
                    }else
                    {
                        au.FirstName = "ไม่พบ";
                        au.LastName = "ผู้ใช้งาน";
                    }
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
                var senderAcc = "1234567898";
                var senderBranch = "012";
                var status = "09";
                int totRec = 0;
                decimal totAmt = 0;
                //excel
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Receiving Bank Code รหัสธนาคาร");
                dt.Columns.Add("Receiving A/C No. เลขที่บัญชี");
                dt.Columns.Add("Receiver Name ชื่อบัญชี");
                dt.Columns.Add("Transfer Amount จำนวนเงิน");
                dt.Columns.Add("Email อีเมล์");
                dt.Columns.Add("Mobile No. เบอร์โทรศัพท์");
                foreach (var gen in gens)
                {
                    totRec++;
                    totAmt += gen.BudgetReviseValue.Value;
                    // MT_Organization
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
                    //SC_USER
                    decimal userID = (decimal)userInfo.UserID;
                    var user = db.SC_User.Where(w => w.UserID == userID).FirstOrDefault();
                    UserExtend userBank = new UserExtend();
                    try
                    {
                        userBank = Newtonsoft.Json.JsonConvert.DeserializeObject<UserExtend>(user.EXTENDDATA);
                    }
                    catch (Exception ex)
                    {


                    }
                    if ((string.IsNullOrWhiteSpace(oex.BranchNo) || string.IsNullOrWhiteSpace(oex.AccountName) || string.IsNullOrWhiteSpace(oex.AccountNo) || string.IsNullOrWhiteSpace(oex.BankNo))&&(string.IsNullOrWhiteSpace(userBank.BranchNo) || string.IsNullOrWhiteSpace(userBank.AccountName)  || string.IsNullOrWhiteSpace(userBank.AccountNo)  || string.IsNullOrWhiteSpace(userBank.BankNo) ))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"1.ไม่พบข้อมูลธนาคารของหน่วยงาน ({org.OrganizationNameTH}) 2.ไม่พบข้อมูลธนาคารของผู้ใช้งาน(บัญชีผู้โอน)"));
                    }
                    else if (string.IsNullOrWhiteSpace(oex.BranchNo) || string.IsNullOrWhiteSpace(oex.AccountName) || string.IsNullOrWhiteSpace(oex.AccountNo) || string.IsNullOrWhiteSpace(oex.BankNo))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"ไม่พบข้อมูลธนาคารของหน่วยงาน ({org.OrganizationNameTH})"));
                    }
                    else if (string.IsNullOrWhiteSpace(userBank.BranchNo) || string.IsNullOrWhiteSpace(userBank.AccountName) || string.IsNullOrWhiteSpace(userBank.AccountNo)  || string.IsNullOrWhiteSpace(userBank.BankNo) )
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError($"ไม่พบข้อมูลธนาคารของผู้ใช้งาน(บัญชีผู้โอน)"));
                    }
                    string personelMobile1 = (gen.ProjectPersonel.Mobile1 !=null) ? gen.ProjectPersonel.Mobile1 :"";
                    string personelEmail1 = (gen.ProjectPersonel.Email1 != null) ? gen.ProjectPersonel.Email1 : "";
                    string txt = "";
                    txt = $"102{totRec.ToString().PadLeft(6, '0')}{oex.BankNo.PadLeft(3, ' ').Substring(0, 3)}{oex.BranchNo.PadLeft(4, '0').Substring(0, 4)}";
                    txt += $"{oex.AccountNo.PadLeft(11, '0').Substring(0, 11)}{senderBank.PadLeft(3, '0').Substring(0, 3)}{userBank.BranchNo.PadLeft(4, '0').Substring(0, 4)}{userBank.AccountNo.PadLeft(11, '0').Substring(0, 11)}";
                    txt += $"{DateTime.Now:ddMMyyyy}1400{string.Format("{0:0.00}", gen.BudgetReviseValue).Replace(".", string.Empty).PadLeft(17, '0').Substring(0, 17)}{"".PadRight(8, ' ')}{"".PadRight(10, '0')}";
                    txt += $"{oex.AccountName.PadRight(100, ' ').Substring(0, 100)}{userBank.AccountName.PadRight(100, ' ')}{"".PadRight(40, ' ')}{" ".PadRight(18, ' ')}";
                    txt += $"  {"".PadRight(18, ' ')}  {"".PadRight(20, ' ')}{totRec.ToString().PadLeft(6, '0')}{status}{personelEmail1.PadRight(40, ' ').Substring(0, 40)}";
                    txt += $"{personelMobile1.Replace("-", string.Empty).PadLeft(20, ' ').Substring(0, 20)}0000{"".PadRight(34, ' ')}"+ Environment.NewLine;
                    dataTxt.Append(txt);
                    
                    //excel
                    DataRow dataExcel = dt.NewRow();
                    dataExcel["Receiving Bank Code รหัสธนาคาร"] = oex.BankNo;
                    dataExcel["Receiving A/C No. เลขที่บัญชี"] = oex.AccountNo;
                    dataExcel["Receiver Name ชื่อบัญชี"] = oex.AccountName;
                    dataExcel["Transfer Amount จำนวนเงิน"] = string.Format("{0:0.00}", gen.BudgetReviseValue);
                    dataExcel["Email อีเมล์"] = gen.ProjectPersonel.Email1;
                    dataExcel["Mobile No. เบอร์โทรศัพท์"] = personelMobile1.Replace("-", string.Empty);
                    dt.Rows.Add(dataExcel);
                }

                var head = $"101{"1".PadLeft(6, '0').Substring(0, 6)}{senderBank.PadRight(3, ' ').Substring(0, 3)}{totRec.ToString().PadLeft(7, '0').Substring(0, 7)}";
                   head += $"{(string.Format("{0:0.00}",totAmt).Replace(".", string.Empty)).PadLeft(19,'0').Substring(0,19)}{DateTime.Now:ddMMyyyy}C{"".PadRight(8,'0')}{"001".PadRight(16, ' ')}{"".PadRight(20, ' ')}{"".PadRight(407, ' ')}"+Environment.NewLine;
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

                //----------------txt only-------------------
                //byte[] buffer;
                //using (var memoryStream = new System.IO.MemoryStream())
                //{
                //    buffer = Encoding.Default.GetBytes(dataTxt.ToString());
                //    memoryStream.Write(buffer, 0, buffer.Length);
                //    var bytes = memoryStream.ToArray();
                //    var data = Convert.ToBase64String(bytes);
                //    // File.WriteAllText("e:\\base64-" + zipName, data);
                //    response.Content = new StringContent(data);

                //    //Set the Response Content Length.
                //    //response.Content.Headers.ContentLength = bytes.LongLength;
                //    response.Content.Headers.ContentLength = data.Length;
                //    //Set the Content Disposition Header Value and FileName.
                //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //    response.Content.Headers.ContentDisposition.FileName = "KTB" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".txt";

                //    //Set the File Content Type.
                //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                //    return response;
                //}
                //--------------------excel-------------
                MemoryStream memoryStreamExcel = new MemoryStream();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "Sheet1");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    sheet1.ColumnWidth = 30;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        wb.SaveAs(memoryStream);
                        memoryStreamExcel = memoryStream;
                    }
                }

                //----------------Zip file -----------------------
                string txtFileName = "KTB"+ DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".txt";
                string excelFileName = "KTB"+ DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".xlsx";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    var DataExcel = memoryStreamExcel.ToArray();
                    var DDR = ToTIS620(dataTxt.ToString());
                    //var SW = Encoding.UTF8.GetBytes(txtSW); 
                    zip.AddEntry(txtFileName, DDR);
                    //zip.AddEntry($"SW_DDR{strDT}.txt", SW);
                    zip.AddEntry(excelFileName, DataExcel);

                    //zip.AddFile("e:\\qr_cashcard_60060001-60060500.txt");

                    //Set the Name of Zip File.
                    string zipName = String.Format("ePayment_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    //zip.Save("e:\\" + zipName);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        //Save the Zip File to MemoryStream.
                        zip.Save(memoryStream);

                        var bytes = memoryStream.ToArray();
                        //File.WriteAllBytes("e:\\byte-" + zipName, bytes);
                        //memoryStream.Seek(0, SeekOrigin.Begin);
                        //Set the Response Content.
                        //response.Content = new ByteArrayContent(bytes);
                        var data = Convert.ToBase64String(bytes);
                        // File.WriteAllText("e:\\base64-" + zipName, data);
                        response.Content = new StringContent(data);

                        //Set the Response Content Length.
                        //response.Content.Headers.ContentLength = bytes.LongLength;
                        response.Content.Headers.ContentLength = data.Length;
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
        [HttpPost]
        [Route("UpdateSueCase")]
        public ReturnMessage UpdateSueCase([FromBody] UpdateSueCaseRequest p)
        {
            var result = new ReturnMessage();
            try
            {
                var db = projService.GetDB();
                var gen = db.ProjectGeneralInfoes.Where(w => w.SUECASEID == p.SueCaseID).FirstOrDefault();
                if (gen == null)
                {
                    result.Message.Add("ไม่พบรหัสคดีที่ระบุ");
                    return result;
                }

                var sc = new SueCaseLog
                {
                    LogBy = p.UserCode,
                    LogCode = p.Accepted ? "ACCEPTED" : "REJECTED",
                    LogDateTime = DateTime.Now,
                    LogDetail = p.Accepted ? "รับเรื่อง" : "ตีกลับ",
                    SueCaseID = p.SueCaseID
                };
                
                if (!p.Accepted)
                {
                    gen.SUECASEID = null;
                    db.SaveChanges();
                }
                var log = projService.UpdateSueCaseLog(gen.ProjectID, sc);
                result.IsCompleted = log.IsCompleted;
                result.Message.AddRange(log.Message);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        public static byte[] ToTIS620(string utf8String)
        {
            List<byte> buffer = new List<byte>();
            byte utf8Identifier = 224;
            for (var i = 0; i < utf8String.Length; i++)
            {
                string utf8Char = utf8String.Substring(i, 1);
                byte[] utf8CharBytes = Encoding.UTF8.GetBytes(utf8Char);
                if (utf8CharBytes.Length > 1 && utf8CharBytes[0] == utf8Identifier)
                {
                    var tis620Char = (utf8CharBytes[2] & 0x3F);
                    tis620Char |= ((utf8CharBytes[1] & 0x3F) << 6);
                    tis620Char |= ((utf8CharBytes[0] & 0x0F) << 12);
                    tis620Char -= (0x0E00 + 0xA0);
                    byte tis620Byte = (byte)tis620Char;
                    tis620Byte += 0xA0;
                    tis620Byte += 0xA0;
                    buffer.Add(tis620Byte);
                }
                else
                {
                    buffer.Add(utf8CharBytes[0]);
                }
            }
            return buffer.ToArray();
        }
    }

}
