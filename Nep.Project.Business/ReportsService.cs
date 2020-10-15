using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nep.Project.Business.QueryHelper;

namespace Nep.Project.Business
{
    public class ReportsService : IServices.IReportsService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportsService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        #region SatisfyReport
        public ReturnObject<SatisfyReportModel> ListSatisfyReport(QueryParameter p)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.SatisfyReportModel> result = new ServiceModels.ReturnObject<ServiceModels.Report.SatisfyReportModel>();
            decimal statusReport = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.FollowupStatus && x.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว).Select(x => x.LOVID).FirstOrDefault();
            result.IsCompleted = false;
            try
            {
                var data = (from gen in _db.ProjectGeneralInfoes
                            join pInfo in _db.ProjectInformations on gen.ProjectID equals pInfo.ProjectID
                            join pContract in _db.ProjectContracts on gen.ProjectID equals pContract.ProjectID 
                            where gen.ProjectApprovalStatus.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && ((gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว))
                            select new ServiceModels.Report.SatisfyReportDetail
                            {
                                ProjectName = pInfo.ProjectNameTH,
                                BudgetYear = pInfo.BudgetYear,
                                ProvinceId = gen.ProvinceID,
                                ProjectId = gen.ProjectID
                            }).ToQueryData(p);
                var items = new List<ServiceModels.Report.SatisfyReportDetail>();
                result.Data = new SatisfyReportModel();
                result.Data.Items = items;

                if (data.IsCompleted)
                {
                    int i = 1;
                    foreach (var d in data.Data)
                    {
                        var qn = _db.PROJECTQUESTIONHDs.Where(q => q.PROJECTID == d.ProjectId && q.QUESTGROUP == "SATISFY").FirstOrDefault();
                        if (qn != null)
                        {
                            var newSat = getSatisfyDetail(qn.DATA);
                            if (newSat != null)
                            {
                                newSat.No = i;
                                newSat.ProjectName = d.ProjectName;
                                items.Add(newSat);
                                i++;
                            }
                        }
       
                    }
                    if (items.Count == 0)
                    {
                        result.Message.Add("ไม่พบข้อมูล");
                        return result;
                    }
                    //result.Data.Items = data.Data;
                    result.IsCompleted = true;
                }else
                {
                    result.Message.AddRange(data.Message);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ListSatisfyReport", ex);
            }
            return result;
        }
        
        private SatisfyReportDetail getSatisfyDetail(string data)
        {
            SatisfyReportDetail ret = null;
            try
            {
                var obj = JsonConvert.DeserializeObject<List<QNData>>(data);
                ret = new SatisfyReportDetail();
                ret.QN1_0 = getSatisfyLevel(obj, "QN1_0");
                ret.QN1_1  = getSatisfyLevel(obj, "QN1_1");
                ret.QN1_2 = getSatisfyLevel(obj, "QN1_2");
                ret.QN1_3 = getSatisfyLevel(obj, "QN1_3");
                ret.QN1_4 = getSatisfyLevel(obj, "QN1_4");
                ret.QN1_5 = getSatisfyLevel(obj, "QN1_5");
                ret.QN2_0 = getSatisfyLevel(obj, "QN2_0");
                ret.QN2_1 = getSatisfyLevel(obj, "QN2_1");
                ret.QN2_2_1 = getSatisfyLevel(obj, "QN2_2_1");
                ret.QN2_2_2 = getSatisfyLevel(obj, "QN2_2_2");
                ret.QN2_2_3  = getSatisfyLevel(obj, "QN2_2_3");
                ret.QN2_2_4  = getSatisfyLevel(obj, "QN2_2_4");
                ret.QN2_3_1  = getSatisfyLevel(obj, "QN2_3_1");
                ret.QN2_3_2 = getSatisfyLevel(obj, "QN2_3_2");
                ret.QN2_3_3 = getSatisfyLevel(obj, "QN2_3_3");
                ret.QN2_3_4 = getSatisfyLevel(obj, "QN2_3_4");
                ret.QN3_1 = getSatisfyLevel(obj, "QN3_1");
                ret.QN4_1 = getSatisfyLevel(obj, "QN4_1");
                ret.QN4_2 = getSatisfyLevel(obj, "QN4_2");
                ret.QN4_3 = getSatisfyLevel(obj, "QN4_3");

            }
            catch(Exception ex )
            {
                ret = null;
            }
            
            return ret;
        }
        private string getSatisfyLevel(List<QNData> qn , string topic)
        {
            var ans = qn.Where(w => w.n == topic).Select(s=>s.v).FirstOrDefault();
            if (ans != null)
            {
                switch (ans.ToString())
                {
                    case "5":
                        return "มากที่สุด";
                    case "4":
                        return "มาก";
                    case "3":
                        return "ปานกลาง";
                    case "2":
                        return "น้อย";
                    case "1":
                        return "น้อยที่สุด";
                    default:
                        return "";


                }
            }
            else
            {
                return "";
            }


        }
        #endregion
        private string getQNValue(List<QNData> qn, string name)
        {
            var ans = qn.Where(w => w.n == name).Select(s => s.v).FirstOrDefault();
            if (ans == null)
            {
                return "";
            }else
            {
                return ans.ToString();
            }
           

        }
        #region FollowUP
        public ReturnObject<FollowUpReportModel> ListFollowUpReport(QueryParameter p)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.FollowUpReportModel> result = new ServiceModels.ReturnObject<ServiceModels.Report.FollowUpReportModel>();
            decimal statusReport = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.FollowupStatus && x.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว).Select(x => x.LOVID).FirstOrDefault();
            result.IsCompleted = false;
            try
            {
                var data = (from gen in _db.ProjectGeneralInfoes
                            join pInfo in _db.ProjectInformations on gen.ProjectID equals pInfo.ProjectID
                            join pContract in _db.ProjectContracts on gen.ProjectID equals pContract.ProjectID
                            where gen.ProjectApprovalStatus.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && ((gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว))
                            select new ServiceModels.Report.FollowUpReportDetail
                            {
                                ProjectName = pInfo.ProjectNameTH,
                                BudgetYear = pInfo.BudgetYear,
                                ProvinceId = gen.ProvinceID,
                                ProjectId = gen.ProjectID,
                                BudgetAmount = gen.BudgetReviseValue
                            }).ToQueryData(p);
                var items = new List<ServiceModels.Report.FollowUpReportDetail>();
                result.Data = new FollowUpReportModel();
                result.Data.Items = items;

                if (data.IsCompleted)
                {
                    int i = 1;
                    foreach (var d in data.Data)
                    {
                        d.BudgetValueType = (d.BudgetAmount.HasValue && d.BudgetAmount.Value > 5000000) ? "มากกว่า 5 ล้าน" : "ต่ำกว่า 5 ล้าน";
                        
                        var qn = _db.PROJECTQUESTIONHDs.Where(q => q.PROJECTID == d.ProjectId && q.QUESTGROUP == "FLUN5M").FirstOrDefault();
                        if (qn != null)
                        {
                            var newRow = getFollowUpReportDetail(qn.DATA);
                            if (newRow != null)
                            {
                                newRow.No = i;
                                newRow.ProjectName = d.ProjectName;
                                items.Add(newRow);
                                i++;
                            }
                        }

                    }
                    if (items.Count == 0)
                    {
                        result.Message.Add("ไม่พบข้อมูล");
                        return result;
                    }
                    //result.Data.Items = data.Data;
                    result.IsCompleted = true;
                }
                else
                {
                    result.Message.AddRange(data.Message);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ListFollowUpReport", ex);
            }
            return result;
        }
        private FollowUpReportDetail getFollowUpReportDetail(string data)
        {
            FollowUpReportDetail ret = null;
            try
            {
                var obj = JsonConvert.DeserializeObject<List<QNData>>(data);
                ret = new FollowUpReportDetail();
                ret.rd0_1 = getQNValue(obj, "rd0_1");
                ret.tb0_1 = getQNValue(obj, "tb0_1");
                ret.rd1_1_1 = getQNValue(obj, "rd1_1_1");
                ret.rd1_2_1_1 = getQNValue(obj, "rd1_2_1_1");
                ret.rd1_2_2_1 = getQNValue(obj, "rd1_2_2_1");
                ret.rd1_2_3_1 = getQNValue(obj,"rd1_2_3_1");
                ret.rd1_3_1_1 = getQNValue(obj,"rd1_3_1_1");
                ret.rd1_3_2_1 = getQNValue(obj,"rd1_3_2_1");
                ret.rd1_4_1_1 = getQNValue(obj,"rd1_4_1_1");
                ret.rd2_1_1_1 = getQNValue(obj,"rd2_1_1_1");
                ret.rd2_1_2_1 = getQNValue(obj,"rd2_1_2_1");
                ret.rd2_2_1_1 = getQNValue(obj,"rd2_2_1_1");
                ret.lbTotal1 = getQNValue(obj,"lbTotal1");
                ret.lbTotal2 = getQNValue(obj,"lbTotal2");
                ret.lbGrandTotal = getQNValue(obj,"lbGrandTotal");
                ret.tbSignName1 = getQNValue(obj,"tbSignName1");
                ret.tbSignName2 = getQNValue(obj,"tbSignName2");
                ret.tbSignName3 = getQNValue(obj,"tbSignName3");
                ret.lbResultText = getQNValue(obj,"lbResultText");
                ret.tbProblem = getQNValue(obj,"tbProblem");
                ret.tbSolution = getQNValue(obj,"tbSolution");
                ret.tbSuccess = getQNValue(obj,"tbSuccess");
                ret.tbImplimentor = getQNValue(obj,"tbImplimentor");
                ret.tbEvaluator = getQNValue(obj,"tbEvaluator");
                ret.tbSuggestion = getQNValue(obj,"tbSuggestion");

            }
            catch (Exception ex)
            {
                ret = null;
            }

            
            return ret;
        }
        #endregion
        #region approved report 
        public ReturnQueryData<ApprovedReportModel> ListApprovedReport(QueryParameter p)
        {
            ReturnQueryData<ApprovedReportModel> result = new ReturnQueryData<ApprovedReportModel>();
            decimal statusReport = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.FollowupStatus && x.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว).Select(x => x.LOVID).FirstOrDefault();
            result.IsCompleted = false;
            try
            {
                var data = (from gen in _db.ProjectGeneralInfoes
                            join pInfo in _db.ProjectInformations on gen.ProjectID equals pInfo.ProjectID
                            join pContract in _db.ProjectContracts on gen.ProjectID equals pContract.ProjectID
                            join prov in _db.MT_Province on gen.ProvinceID equals prov.ProvinceID
                            join ot in _db.MT_OrganizationType on gen.OrganizationTypeID equals ot.OrganizationTypeID
                            join dis in _db.MT_ListOfValue on pInfo.DisabilityTypeID equals dis.LOVID
                            where gen.ProjectApprovalStatus.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && ((gen.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว))
                            select new ServiceModels.Report.ApprovedReportModel
                            {
                                ProjectName = pInfo.ProjectNameTH,
                                ProvinceId = gen.ProvinceID,
                                OrganizationId = gen.OrganizationID,
                                OrganizationName = gen.OrganizationNameTH,
                                OrganizationType = ot.OrganizationType,
                                OrganizationTypeEtc = gen.OrganizationTypeEtc,
                                OrganizationTypeCode = ot.OrganizationTypeCode,
                                ApprovedAmount = gen.BudgetReviseValue.Value,
                                DisabilityType = dis.LOVName,
                                ProvinceName = prov.ProvinceName,
                                Mission1 = gen.ProjectEvaluation.IsPassMission1,
                                Mission2 = gen.ProjectEvaluation.IsPassMission2,
                                Mission3 = gen.ProjectEvaluation.IsPassMission3,
                                Mission4 = gen.ProjectEvaluation.IsPassMission4,
                                Mission5 = gen.ProjectEvaluation.IsPassMission5,
                                Mission6 = gen.ProjectEvaluation.ISPASSMISSION6,
                                ProjectId = gen.ProjectID,
                                ProjectTypeName = pInfo.ProjectType.LOVName,
                                TotalTarget = gen.ProjectTargetGroups.Sum(sm => sm.TargetGroupAmt),
                                BudgetYear = pInfo.BudgetYear,
                                BudgetTypeId = gen.ProjectApproval.ApprovalBudgetTypeID,
                                BudgetTypeName = gen.ProjectApproval.ApprovalBudgetType.LOVName
                            }).ToQueryData(p);
             

              
                    result = data;
            
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ListApprovedReport", ex);
            }
            return result;
        }
        #endregion
    }
}
