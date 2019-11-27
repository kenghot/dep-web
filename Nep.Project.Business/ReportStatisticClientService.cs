using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Business
{
    public class ReportStatisticClientService : IServices.IReportStatisticClientService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportStatisticClientService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.CompareClientSupport> GetCompareClientSupports(int startYear, int endYear)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.CompareClientSupport> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.CompareClientSupport>();
            List<ServiceModels.Report.ReportStatisticClient.CompareClientSupport> listResult = new List<ServiceModels.Report.ReportStatisticClient.CompareClientSupport>();
            var listSection = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.Section).ToList();
            decimal? provinceId = _user.ProvinceID;
            decimal countYear = startYear;

            try
            {
                do
                {
                    foreach (var item in listSection)
                    {
                        List<decimal> existingId = _db.MT_Province.Where(x => x.SectionID == item.LOVID).Select(x => x.ProvinceID).ToList();
                        decimal maleAmount = 0, femaleAmount = 0, maleDupAmount = 0, femaleDupAmount = 0;
                        List<DBModels.Model.ProjectReport> listProjectReport;
                        if (!_user.IsCenterOfficer)
                        {
                            listProjectReport = _db.ProjectReports.Where(x => existingId.Contains(x.ProjectGeneralInfo.ProvinceID) && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.ProjectGeneralInfo.ProvinceID == provinceId).ToList();
                        }
                        else
                        {
                            listProjectReport = _db.ProjectReports.Where(x => existingId.Contains(x.ProjectGeneralInfo.ProvinceID) && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();
                        }

                        ServiceModels.Report.ReportStatisticClient.CompareClientSupport newitem = new ServiceModels.Report.ReportStatisticClient.CompareClientSupport();
                        if (listProjectReport != null && listProjectReport.Count > 0)
                        {
                            List<string> mIDCardNo = new List<string>();
                            List<string> fIDCardNo = new List<string>();

                            foreach (var iReport in listProjectReport)
                            {
                                maleAmount += iReport.MaleParticipant;
                                femaleAmount += iReport.FemaleParticipant;

                                //MaleDuplicateAmount
                                if (mIDCardNo.Count > 0)
                                {
                                    var mPart = iReport.ProjectGeneralInfo.ProjectParticipants.Where(x => (x.Gender == "M") && (x.IDCardNo != null));
                                    maleDupAmount += mPart.Where(x => mIDCardNo.Contains(x.IDCardNo)).Count();
                                    mIDCardNo.AddRange(mPart.Where(x => !mIDCardNo.Contains(x.IDCardNo)).Select(x => x.IDCardNo));
                                }
                                else
                                {
                                    mIDCardNo.AddRange(iReport.ProjectGeneralInfo.ProjectParticipants.Where(x => (x.Gender == "M") && (x.IDCardNo != null)).Select(x => x.IDCardNo));
                                }

                                //FemaleDuplicateAmount
                                if (fIDCardNo.Count > 0)
                                {
                                    var fPart = iReport.ProjectGeneralInfo.ProjectParticipants.Where(x => (x.Gender == "F") && (x.IDCardNo != null));
                                    femaleDupAmount += fPart.Where(x => fIDCardNo.Contains(x.IDCardNo)).Count();
                                    fIDCardNo.AddRange(fPart.Where(x => !fIDCardNo.Contains(x.IDCardNo)).Select(x => x.IDCardNo));
                                }
                                else
                                {
                                    fIDCardNo.AddRange(iReport.ProjectGeneralInfo.ProjectParticipants.Where(x => (x.Gender == "F") && (x.IDCardNo != null)).Select(x => x.IDCardNo));
                                }
                            }
                        }

                        newitem.Region = item.LOVName;
                        newitem.BudgetYear = (countYear + 543).ToString();
                        newitem.MaleClientAmount = maleAmount;
                        newitem.MaleDuplicatedAmount = maleDupAmount;
                        newitem.FemaleClientAmount = femaleAmount;
                        newitem.FeMaleDuplicatedAmount = femaleDupAmount;
                        listResult.Add(newitem);
                    }

                    countYear = countYear + 1;
                } while (countYear <= endYear);

                if (listResult.Count > 0)
                {
                    bool hasValue = listResult.Where(x => x.MaleClientAmount > 0 || x.FemaleClientAmount > 0).Count() > 0;
                    if (hasValue)
                    {
                        result.Data = listResult;
                        result.TotalRow = listResult.Count;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.NoRecord);
                    }
                }
                else
                {
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ReportStatisticClient", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType> GetAnalyzeProjectByTypes(int startYear, int endYear)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType> result = new ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType>();
            List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType> listResult = new List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType>();
            var listType = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.DisabilityType).ToList();
            decimal? provinceId = _user.ProvinceID;
            decimal countYear = startYear;

            try
            {
                do
                {
                    foreach (var item in listType)
                    {
                        decimal m, f;
                        if (!_user.IsCenterOfficer)
                        {
                            m = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectInformation.DisabilityTypeID == item.LOVID
                                    && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "M" && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();

                            f = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectInformation.DisabilityTypeID == item.LOVID
                                    && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "F" && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();
                        }
                        else
                        {
                            m = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectInformation.DisabilityTypeID == item.LOVID
                                    && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "M").Count();

                            f = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectInformation.DisabilityTypeID == item.LOVID
                                    && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "F").Count();
                        }

                        ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType newitem = new ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByType();
                        newitem.No = item.OrderNo.ToString();
                        newitem.TypeName = item.LOVName;
                        newitem.Year = (countYear + 543).ToString();
                        newitem.MaleAmount = m;
                        newitem.FemaleAmount = f;
                        listResult.Add(newitem);
                    }

                    countYear = countYear + 1;
                } while (countYear <= endYear);

                if (listResult.Count > 0)
                {
                    bool hasValue = listResult.Where(x => x.MaleAmount > 0 || x.FemaleAmount > 0).Count() > 0;
                    if (hasValue)
                    {
                        result.Data = listResult;
                        result.TotalRow = listResult.Count;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.NoRecord);
                    }
                }
                else
                {
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ReportStatisticClient", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic> GetAnalyzeProjectByStrategics(int startYear, int endYear)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic> result = new ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic>();
            List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic> listResult = new List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic>();
            List<string> listStrategic = new List<string>();
            listStrategic.Add(Resources.UI.LabelStandardStrategic1);
            listStrategic.Add(Resources.UI.LabelStandardStrategic2);
            listStrategic.Add(Resources.UI.LabelStandardStrategic3);
            listStrategic.Add(Resources.UI.LabelStandardStrategic4);
            listStrategic.Add(Resources.UI.LabelStandardStrategic5);
            decimal? providerId = _user.ProvinceID;
            decimal countYear = startYear;

            try
            {
                do
                {
                    int i = 0;
                    List<DBModels.Model.ProjectParticipant> q1, q2, q3, q4, q5;
                    q1 = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectEvaluation.IsPassMission1 == "1"
                                && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();
                    q2 = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectEvaluation.IsPassMission2 == "1"
                                && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();
                    q3 = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectEvaluation.IsPassMission3 == "1"
                                && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();
                    q4 = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectEvaluation.IsPassMission4 == "1"
                                && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();
                    q5 = _db.ProjectParticipants.Where(x => x.ProjectGeneralInfo.ProjectEvaluation.IsPassMission5 == "1"
                                && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear).ToList();

                    if (!_user.IsCenterOfficer)
                    {
                        q1 = q1.Where(x => x.ProjectGeneralInfo.ProvinceID == providerId).ToList();
                        q2 = q2.Where(x => x.ProjectGeneralInfo.ProvinceID == providerId).ToList();
                        q3 = q3.Where(x => x.ProjectGeneralInfo.ProvinceID == providerId).ToList();
                        q4 = q4.Where(x => x.ProjectGeneralInfo.ProvinceID == providerId).ToList();
                        q5 = q5.Where(x => x.ProjectGeneralInfo.ProvinceID == providerId).ToList();
                    }

                    foreach (var item in listStrategic)
                    {
                        i = i + 1;
                        decimal maleAmount = 0, femaleAmount = 0;
                        ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic newitem = new ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByStrategic();
                        newitem.No = i.ToString();
                        newitem.StrategicName = item;
                        newitem.Year = (countYear + 543).ToString();

                        switch (i)
                        {
                            case 1:
                                if (q1.Count > 0)
                                {
                                    maleAmount = q1.Where(x => x.Gender == "M").Count();
                                    femaleAmount = q1.Where(x => x.Gender == "F").Count();
                                }
                                break;
                            case 2:
                                if (q2.Count > 0)
                                {
                                    maleAmount = q2.Where(x => x.Gender == "M").Count();
                                    femaleAmount = q2.Where(x => x.Gender == "F").Count();
                                }
                                break;
                            case 3:
                                if (q3.Count > 0)
                                {
                                    maleAmount = q3.Where(x => x.Gender == "M").Count();
                                    femaleAmount = q3.Where(x => x.Gender == "F").Count();
                                }
                                break;
                            case 4:
                                if (q4.Count > 0)
                                {
                                    maleAmount = q4.Where(x => x.Gender == "M").Count();
                                    femaleAmount = q4.Where(x => x.Gender == "F").Count();
                                }
                                break;
                            case 5:
                                if (q5.Count > 0)
                                {
                                    maleAmount = q5.Where(x => x.Gender == "M").Count();
                                    femaleAmount = q5.Where(x => x.Gender == "F").Count();
                                }
                                break;
                        }

                        newitem.MaleAmount = maleAmount;
                        newitem.FemaleAmount = femaleAmount;
                        listResult.Add(newitem);
                    }

                    countYear = countYear + 1;
                } while (countYear <= endYear);

                if (listResult.Count > 0)
                {
                    bool hasValue = listResult.Where(x => x.MaleAmount > 0 || x.FemaleAmount > 0).Count() > 0;
                    if (hasValue)
                    {
                        result.Data = listResult;
                        result.TotalRow = listResult.Count;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.NoRecord);
                    }
                }
                else
                {
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ReportStatisticClient", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup> GetAnalyzeProjectByTargetGroups(int startYear, int endYear)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup> result = new ReturnQueryData<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup>();
            List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup> listResult = new List<ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup>();
            var listTargetGroup = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.TargetGroup).ToList();
            decimal? provinceId = _user.ProvinceID;
            decimal countYear = startYear;

            try
            {
                do
                {
                    foreach (var item in listTargetGroup)
                    {
                        decimal m, f;
                        if (!_user.IsCenterOfficer)  // where provinceId case userlogin.provinceId != "กรุงเทพมหานคร"
                        {
                            m = _db.ProjectParticipants.Where(x => x.TargetGroupID == item.LOVID && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "M" && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();

                            f = _db.ProjectParticipants.Where(x => x.TargetGroupID == item.LOVID && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                    && x.Gender == "F" && x.ProjectGeneralInfo.ProvinceID == provinceId).Count();
                        }
                        else
                        {
                            m = _db.ProjectParticipants.Where(x => x.TargetGroupID == item.LOVID && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                        && x.Gender == "M").Count();

                            f = _db.ProjectParticipants.Where(x => x.TargetGroupID == item.LOVID && x.ProjectGeneralInfo.ProjectInformation.BudgetYear == countYear
                                        && x.Gender == "F").Count();
                        }

                        ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup newitem = new ServiceModels.Report.ReportStatisticClient.AnalyzeProjectByTargetGroup();
                        newitem.No = item.OrderNo.ToString();
                        newitem.TargetGroup = item.LOVName;
                        newitem.Year = (countYear + 543).ToString();
                        newitem.MaleAmount = m;
                        newitem.FemaleAmount = f;
                        listResult.Add(newitem);
                    }

                    countYear = countYear + 1;
                } while (countYear <= endYear);

                if (listResult.Count > 0)
                {
                    bool hasValue = listResult.Where(x => x.MaleAmount > 0 || x.FemaleAmount > 0).Count() > 0;
                    if (hasValue)
                    {
                        result.Data = listResult;
                        result.TotalRow = listResult.Count;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.NoRecord);
                    }
                }
                else
                {
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "ReportStatisticClient", ex);
            }

            return result;
        }
    }
}
