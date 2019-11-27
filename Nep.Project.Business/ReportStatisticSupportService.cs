using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Business
{
    public class ReportStatisticSupportService : IServices.IReportStatisticSupportService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;
        private decimal _provincebkkId;

        public ReportStatisticSupportService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
            _provincebkkId = _db.MT_Province.Where(x => x.ProvinceName == "กรุงเทพมหานคร").Select(x => x.ProvinceID).FirstOrDefault();
        }

        public List<ServiceModels.Report.ReportStatisticSupport.CompareSupport> GetCompareSupports(int startYear, int endYear)
        {
            List<ServiceModels.Report.ReportStatisticSupport.CompareSupport> result = new List<ServiceModels.Report.ReportStatisticSupport.CompareSupport>();
            decimal status6 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว).Select(x => x.LOVID).FirstOrDefault();   
            decimal? provinceId = _user.ProvinceID;
            int countYear = startYear;

            do
            {
                ServiceModels.Report.ReportStatisticSupport.CompareSupport item = new ServiceModels.Report.ReportStatisticSupport.CompareSupport();
                List<DBModels.Model.ProjectGeneralInfo> q = new List<DBModels.Model.ProjectGeneralInfo>();
                if (_provincebkkId == provinceId)
                {
                    q = _db.ProjectGeneralInfoes.Where(x => (x.ProjectApprovalStatusID == status6 && x.ProjectInformation.BudgetYear == countYear)).ToList();
                }
                else
                {
                    q = _db.ProjectGeneralInfoes.Where(x => (x.ProjectApprovalStatusID == status6)
                    && (x.ProjectInformation.BudgetYear == countYear) && (x.ProvinceID == provinceId)).ToList();
                }

                if (q.Count > 0)
                {
                    foreach (var gInfo in q)
                    {
                        item.BudgetYear = (countYear + 543).ToString();
                        item.ApproveAmount += (gInfo.BudgetValue.HasValue) ? gInfo.BudgetValue.Value : 0;
                        item.AllowcateAmount += (gInfo.BudgetReviseValue.HasValue) ? gInfo.BudgetReviseValue.Value : 0;
                    }
                }
                else
                {
                    item.BudgetYear = (countYear + 543).ToString();
                    item.ApproveAmount = 0;
                    item.AllowcateAmount = 0;
                }

                result.Add(item);
                countYear = countYear + 1;
            } while (countYear <= endYear);

            int countNoAmount = 0;
            foreach (var i in result)
            {
                if (i.AllowcateAmount == 0 && i.ApproveAmount == 0)
                {
                    countNoAmount++;
                }
            }

            if (countNoAmount == result.Count)
            {
                result = new List<ServiceModels.Report.ReportStatisticSupport.CompareSupport>();
            }

            return result;
        }

        public List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType> GetAnalyzeProjectByTypes(int startYear, int endYear)
        {
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType> result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType>();
            var listType = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.DisabilityType).ToList();
            decimal status6 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว).Select(x => x.LOVID).FirstOrDefault();
            decimal? provinceId = _user.ProvinceID;
            int countYear = startYear;

            do
            {
                foreach (var item in listType)
                {
                    int amountAnalyze = 0;
                    if (_provincebkkId == provinceId)
                    {
                        amountAnalyze = _db.ProjectInformations.Where(x => x.DisabilityTypeID == item.LOVID && x.BudgetYear == countYear
                        && x.ProjectGeneralInfo.ProjectApprovalStatusID == status6).Count();
                    }
                    else
                    {
                        amountAnalyze = _db.ProjectInformations.Where(x => x.DisabilityTypeID == item.LOVID && x.BudgetYear == countYear
                        && x.ProjectGeneralInfo.ProjectApprovalStatusID == status6 && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();
                    }

                    ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType newitem = new ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType();
                    newitem.No = item.OrderNo.ToString();
                    newitem.TypeName = item.LOVName;
                    newitem.Year = (countYear + 543).ToString();
                    newitem.Amount = amountAnalyze;
                    result.Add(newitem);
                }

                countYear = countYear + 1;
            }while(countYear <= endYear);

            int countNoAmount = 0;
            foreach (var i in result)
            {
                if (i.Amount == 0)
                {
                    countNoAmount++;
                }
            }

            if (countNoAmount == result.Count)
            {
                result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType>();
            }

            return result;
        }

        public List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic> GetAnalyzeProjectByStrategics(int startYear, int endYear)
        {
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic> result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic>();
            List<string> listStrategic = new List<string>();
            listStrategic.Add(Resources.UI.LabelStandardStrategic1);
            listStrategic.Add(Resources.UI.LabelStandardStrategic2);
            listStrategic.Add(Resources.UI.LabelStandardStrategic3);
            listStrategic.Add(Resources.UI.LabelStandardStrategic4);
            listStrategic.Add(Resources.UI.LabelStandardStrategic5);
            decimal status6 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว).Select(x => x.LOVID).FirstOrDefault();
            decimal? provinceId = _user.ProvinceID;
            int countYear = startYear;
            int i = 0;

            do
            {
                int q1 = 0, q2 = 0 , q3 = 0, q4 = 0, q5 = 0;
                if (_provincebkkId == provinceId)
                {
                    q1 = _db.ProjectEvaluations.Where(x => x.IsPassMission1 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)).Count();
                    q2 = _db.ProjectEvaluations.Where(x => x.IsPassMission2 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)).Count();
                    q3 = _db.ProjectEvaluations.Where(x => x.IsPassMission3 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)).Count();
                    q4 = _db.ProjectEvaluations.Where(x => x.IsPassMission4 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)).Count();
                    q5 = _db.ProjectEvaluations.Where(x => x.IsPassMission5 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)).Count();
                }
                else
                {
                    q1 = _db.ProjectEvaluations.Where(x => x.IsPassMission1 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)
                                                            && x.ProvinceID == provinceId).Count();
                    q2 = _db.ProjectEvaluations.Where(x => x.IsPassMission2 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)
                                                            && x.ProvinceID == provinceId).Count();
                    q3 = _db.ProjectEvaluations.Where(x => x.IsPassMission3 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)
                                                            && x.ProvinceID == provinceId).Count();
                    q4 = _db.ProjectEvaluations.Where(x => x.IsPassMission4 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)
                                                            && x.ProvinceID == provinceId).Count();
                    q5 = _db.ProjectEvaluations.Where(x => x.IsPassMission5 == "1" && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                                            && (x.ProjectGeneralInfo.ProjectApprovalStatusID == status6)
                                                            && x.ProvinceID == provinceId).Count();
                }

                foreach (var item in listStrategic)
                {
                    i = i + 1;
                    decimal amount = 0;
                    ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic newitem = new ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic();
                    newitem.No = i.ToString();
                    newitem.StrategicName = item;
                    newitem.Year = (countYear + 543).ToString();

                    switch (i)
                    {
                        case 1: amount = q1;
                            break;
                        case 2: amount = q2;
                            break;
                        case 3: amount = q3;
                            break;
                        case 4: amount = q4;
                            break;
                        case 5: amount = q5;
                            break;
                    }

                    newitem.Amount = amount;
                    result.Add(newitem);
                }

                countYear = countYear + 1;
            } while (countYear <= endYear);

            int countNoAmount = 0;
            foreach (var icount in result)
            {
                if (icount.Amount == 0)
                {
                    countNoAmount++;
                }
            }

            if (countNoAmount == result.Count)
            {
                result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic>();
            }

            return result;
        }

        public List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType> GetAnalyzeProjectByProjectTypes(int startYear, int endYear)
        {
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType> result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType>();
            var listType = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectType).ToList();
            decimal status6 = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว).Select(x => x.LOVID).FirstOrDefault();
            decimal? provinceId = _user.ProvinceID;
            int countYear = startYear;

            do
            {
                foreach (var item in listType)
                {
                    int amountAnalyze = 0;

                    if (_provincebkkId == provinceId)
                    {
                        amountAnalyze = _db.ProjectInformations.Where(x => x.ProjectTypeID == item.LOVID && x.BudgetYear == countYear
                        && x.ProjectGeneralInfo.ProjectApprovalStatusID == status6).Count();
                    }
                    else
                    {
                        amountAnalyze = _db.ProjectInformations.Where(x => x.ProjectTypeID == item.LOVID && x.BudgetYear == countYear
                        && x.ProjectGeneralInfo.ProjectApprovalStatusID == status6 && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();
                    }

                    ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType newitem = new ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType();
                    newitem.No = item.OrderNo.ToString();
                    newitem.ProjectTypeName = item.LOVName;
                    newitem.Year = (countYear + 543).ToString();
                    newitem.Amount = amountAnalyze;
                    result.Add(newitem);
                }

                countYear = countYear + 1;
            } while (countYear <= endYear);

            int countNoAmount = 0;
            foreach (var i in result)
            {
                if (i.Amount == 0)
                {
                    countNoAmount++;
                }
            }

            if (countNoAmount == result.Count)
            {
                result = new List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType>();
            }

            return result;
        }
    }
}
