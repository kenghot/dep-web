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
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report4", ex);
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
            switch(ans)
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
    }
}
