using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Responses;
using Nep.Project.ServiceModels.ProjectInfo;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                            join subev in db.ProjectEvaluations on inf.ProjectID equals subev.ProjectID into lev from ev in lev.DefaultIfEmpty()
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
                                IsApproved = app != null,
                                ev.IsPassMission1,
                                ev.IsPassMission2,
                                ev.IsPassMission3,
                                ev.IsPassMission4,
                                ev.IsPassMission5,
                                ev.ISPASSMISSION6,

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

                    if (proj.IsApproved)
                    {
                        collectLegned(result.Data.orgTypeData.legendDatas, new LegendData
                        {
                            amount = proj.BudgetReviseValue.HasValue ? proj.BudgetReviseValue.Value : 0,
                            description = proj.OrganizationType,
                            id = proj.OrganizationTypeCode
                        });
                        collectLegned(result.Data.projectTypeData.legendDatas, new LegendData
                        {
                            amount = proj.BudgetReviseValue.HasValue ? proj.BudgetReviseValue.Value : 0,
                            description = proj.ProjectTypeName,
                            id = proj.ProjectTypeCode
                        });
                        collectLegned(result.Data.disabilityTypeData.legendDatas, new LegendData
                        {
                            amount = proj.BudgetReviseValue.HasValue ? proj.BudgetReviseValue.Value : 0,
                            description = proj.DisabilityName,
                            id = proj.DisabilityCode
                        });
                        string mission = (proj.IsPassMission1 == "1") ? "1" : (proj.IsPassMission2 == "1") ? "2" : (proj.IsPassMission3 == "1") ? "3" :
                            (proj.IsPassMission4 == "1") ? "4" : (proj.IsPassMission5 == "1") ? "5" : (proj.ISPASSMISSION6 == "1") ? "6" : "";
                        collectLegned(result.Data.missionData.legendDatas, new LegendData
                        {
                            amount = proj.BudgetReviseValue.HasValue ? proj.BudgetReviseValue.Value : 0,
                            description = $"ยุทธศาสตร์ที่ {mission}",
                            id = mission
                        });
                    }
                }

                storeChartData(result.Data.projectTypeData);

                storeChartData(result.Data.orgTypeData);
                storeChartData(result.Data.disabilityTypeData);
                storeChartData(result.Data.missionData);

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        private void storeChartData(DefaultData data)
        {
            data.legendDatas = data.legendDatas.OrderBy(o => o.id).ToList();
            foreach (var l in data.legendDatas)
            {
                data.amountData.datasets[0].data.Add(l.amount);
                data.amountData.datasets[0].label = "จำนวนเงิน";
                data.amountData.datasets[0].backgroundColor.Add(l.color);
                data.amountData.labels.Add(l.description);

                data.projectData.datasets[0].data.Add(l.projects);
                data.projectData.datasets[0].label = "จำนวนโครงการ";
                data.projectData.datasets[0].backgroundColor.Add(l.color);
                data.projectData.labels.Add(l.description);
            }
        }
        private void collectLegned(List<LegendData> datas, LegendData data)
        {
            var s = datas.Where(w => w.id == data.id).FirstOrDefault();
            if (s == null)
            {
                s = new LegendData
                {
                    id = data.id,
                    color = randomColor(),
                    description = data.description
                };
                datas.Add(s);

            }
            s.amount += data.amount;
            s.projects++;
        }
        private Random r = new Random();
        private string randomColor()
        {
            
           
            var c = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            var color = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
            return color;
        }
    }
    
}
