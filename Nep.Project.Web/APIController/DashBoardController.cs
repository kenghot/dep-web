using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Responses;
using Nep.Project.ServiceModels.ProjectInfo;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/dashboard")]
    public class DashBoardController : ApiController
    {
        private IServices.IProjectInfoService projService { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        private SecurityInfo userInfo { get; set; }
        public DashBoardController(IServices.IProjectInfoService ps, SecurityInfo UserInfo, IServices.IAttachmentService attach)
        {
            projService = ps;
            userInfo = UserInfo;
            attachService = attach;
        }
        [Route("Get/{year}")]
        [HttpGet]
        public ReturnObject<DashBoardResponse> Get([FromUri] int year)
        {
            var result = new ReturnObject<DashBoardResponse>();
            result.IsCompleted = false;
            try
            {
                result.Data = new DashBoardResponse();
                var summary = new SummaryData();
                result.Data.summary = summary;
                var db = projService.GetDB();
                var newOrg = db.OrganizationRegisterEntries.Where(w => w.RegisterDate.Year == year && w.ApprovedByID == null).Count();
                summary.newOrganization = newOrg;

                var query = from gen in db.ProjectGeneralInfoes
                            join inf in db.ProjectInformations on gen.ProjectID equals inf.ProjectID
                            join subapp in db.ProjectApprovals on gen.ProjectID equals subapp.ProjectID into lapp from app in lapp.DefaultIfEmpty()
                            join suborg in db.MT_Organization on gen.OrganizationID equals suborg.OrganizationID into lorg from org in lorg.DefaultIfEmpty()
                            join subdis in db.MT_ListOfValue on inf.DisabilityTypeID equals subdis.LOVID into ldis from dis in ldis.DefaultIfEmpty()
                            join subfol in db.MT_ListOfValue on gen.FollowUpStatus equals subfol.LOVID into lfol from fol in lfol.DefaultIfEmpty()
                            join subev in db.ProjectEvaluations on inf.ProjectID equals subev.ProjectID into lev from subev in lev.DefaultIfEmpty()
                            where inf.BudgetYear == year
                            select new
                            {
                                ProjectApprovalCode = gen.ProjectApprovalStatus.LOVCode,
                                gen.BudgetValue,
                                gen.BudgetReviseValue,
                                org.OrganizationType.OrganizationTypeCode,
                                org.OrganizationType.OrganizationType,
                                ProjectTypeCode = inf.ProjectType.LOVCode,
                                ProjectTypeName = inf.ProjectType.LOVName,
                                DisabilityCode = dis.LOVCode,
                                DisabilityName = dis.LOVName,
                                inf.RejectComment,
                                FollowCode = fol.LOVCode,
                                gen.ACKNOWLEDGED,
                                IsApproved = app != null

                            };
                var projs = query.ToList();
                foreach (var proj in projs)
                {
                    if (proj.ProjectApprovalCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                    {
                        summary.newProject++;
                    }
                    if (proj.IsApproved && proj.FollowCode != Common.LOVCode.Followupstatus.รายงานผลแล้ว)
                    {
                        summary.notReported++;
                    }
                    if (proj.ACKNOWLEDGED != "1")
                    {
                        summary.noProcess++;
                    }
                    if (proj.FollowCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว)
                    {
                        summary.reported++;
                    }
                    if (!string.IsNullOrWhiteSpace(proj.RejectComment) && proj.ProjectApprovalCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ)
                    {
                        summary.rePurpose++;
                    }
                }
                
                var pjtypeAmt = new ChartData();
                result.Data.projectTypeAmount = pjtypeAmt;
                pjtypeAmt.datasets = new List<ChartDataSet>
                {
                    new ChartDataSet
                    {
                        backgroundColor = new List<string>() { "red", "blue", "green", "yellow" },
                        data = new List<decimal>() { 10, 50, 35, 24 },
                        label = "test"
                    }

                };
                pjtypeAmt.labels = new List<string>() { "ทดสอบ1", "ทดสอบ2", "ทดสอบ3", "ทดสอบ4" };
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
    }
}
