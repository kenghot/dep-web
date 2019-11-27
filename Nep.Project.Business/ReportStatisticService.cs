using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportStatisticService : IServices.IReportStatisticService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportStatisticService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport> ListCompareDuplicatedSupport(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport> result;
            result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport>();

            try
            {
                if (_user.IsCenterOfficer)
                {
                    var data = (from participant in _db.ProjectParticipants
                                join fStatus in _db.MT_ListOfValue on participant.ProjectGeneralInfo.FollowUpStatus equals fStatus.LOVID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)

                                group participant by new
                                {
                                    participant.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                                    participant.IDCardNo
                                } into grouping
                                where grouping.Count() >= 2
                                from participant in grouping
                                let genInfo = participant.ProjectGeneralInfo
                                join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                                join prov in _db.MT_Province on genInfo.ProvinceID equals prov.ProvinceID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว
                                group participant.Gender by new
                                {
                                    genInfo.ProjectInformation.BudgetYear,
                                    prov.Section.LOVID,
                                    prov.Section.LOVName
                                } into grouping

                                select new ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport
                                {
                                    BudgetYear = grouping.Key.BudgetYear,
                                    RegionID = grouping.Key.LOVID,
                                    Region = grouping.Key.LOVName,
                                    MaleDuplicatedAmount = grouping.Count(x => x == "M"),
                                    FeMaleDuplicatedAmount = grouping.Count(x => x == "F"),
                                    CountIDCardNo = grouping.Count()
                                }
                            ).ToQueryData(p);
                    result = data;
                }
                else
                {
                    var data = (from participant in _db.ProjectParticipants
                                join fStatus in _db.MT_ListOfValue on participant.ProjectGeneralInfo.FollowUpStatus equals fStatus.LOVID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
                                group participant by new
                                {
                                    participant.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                                    participant.IDCardNo
                                } into grouping
                                where grouping.Count() >= 2
                                from participant in grouping
                                let genInfo = participant.ProjectGeneralInfo
                                join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                                join prov in _db.MT_Province on genInfo.ProvinceID equals prov.ProvinceID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว
                                group participant.Gender by new
                                {
                                    genInfo.ProjectInformation.BudgetYear,
                                    genInfo.ProvinceID,
                                    prov.Section.LOVID,
                                    prov.Section.LOVName
                                } into grouping

                                select new ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport
                                {
                                    BudgetYear = grouping.Key.BudgetYear,
                                    ProjectProvinceID = grouping.Key.ProvinceID,
                                    RegionID = grouping.Key.LOVID,
                                    Region = grouping.Key.LOVName,
                                    MaleDuplicatedAmount = grouping.Count(x => x == "M"),
                                    FeMaleDuplicatedAmount = grouping.Count(x => x == "F"),
                                    CountIDCardNo = grouping.Count()
                                }
                            ).ToQueryData(p);
                    result = data;

                }
                //if (_user.IsCenterOfficer)
                //{
                //    var data = (from genInfo in _db.ProjectGeneralInfoes
                //                join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                //                join prov in _db.MT_Province on genInfo.ProvinceID equals prov.ProvinceID
                //                from participant in genInfo.ProjectParticipants
                //                join dup in duplicatedIdCards on new {genInfo.ProjectInformation.BudgetYear, participant.IDCardNo } equals new {dup.BudgetYear, dup.IDCardNo }
                //                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว
                //                group new { genInfo, prov, participant } by new
                //                {
                //                    genInfo.ProjectInformation.BudgetYear,
                //                    prov.Section,
                //                    //participant.IDCardNo,
                //                    //participant.Gender
                //                } into grouping

                //                select new ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport
                //                {
                //                    BudgetYear = grouping.Key.BudgetYear,
                //                    RegionID = grouping.Key.Section.LOVID,
                //                    Region = grouping.Key.Section.LOVName,
                //                    //IdCardNo = grouping.Key.IDCardNo,
                //                    //Gender = grouping.Key.Gender,
                //                    MaleDuplicatedAmount = grouping.Count(x => x.participant.Gender == "M"),
                //                    FeMaleDuplicatedAmount = grouping.Count(x => x.participant.Gender == "F")
                //                }
                //            ).ToQueryData(p);
                //    result = data;
                //}
                //else
                //{
                    //var data = (from genInfo in _db.ProjectGeneralInfoes
                    //            join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                    //            join prov in _db.MT_Province on genInfo.ProvinceID equals prov.ProvinceID
                    //            join participant in _db.ProjectParticipants on genInfo.ProjectID equals participant.ProjectID
                    //            where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว
                    //            group new { genInfo, prov, participant } by new
                    //            {
                    //                genInfo.ProjectInformation.BudgetYear,
                    //                genInfo.ProvinceID,
                    //                prov.SectionID,
                    //                prov.Section.LOVName,
                    //                participant.IDCardNo,
                    //                participant.Gender
                    //            } into grouping

                    //            select new ServiceModels.Report.ReportStatisticDuplicate.CompareDuplicatedSupport
                    //            {
                    //                BudgetYear = grouping.Key.BudgetYear,
                    //                ProjectProvinceID = grouping.Key.ProvinceID,
                    //                RegionID = grouping.Key.SectionID,
                    //                Region = grouping.Key.LOVName,
                    //                IdCardNo = grouping.Key.IDCardNo,
                    //                Gender = grouping.Key.Gender,
                    //                MaleDuplicatedAmount = (grouping.Key.Gender == "M") ? grouping.Count() : 0,
                    //                FeMaleDuplicatedAmount = (grouping.Key.Gender == "F") ? grouping.Count() : 0,
                    //                CountIDCardNo = grouping.Count()
                    //            }
                    //        ).ToQueryData(p);
                //    result = data;
                //}  
                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report Statistic", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType> ListAnalyzeProjectByType(ServiceModels.QueryParameter p, Decimal? provinceID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType> result;
            result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType>();

            try
            {
                if(provinceID.HasValue){
                    decimal provinceIDFilter = (decimal)provinceID;
                    var data = (from participant in _db.ProjectParticipants
                                join fStatus in _db.MT_ListOfValue on participant.ProjectGeneralInfo.FollowUpStatus equals fStatus.LOVID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
                                group participant by new
                                {
                                    participant.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                                    participant.IDCardNo
                                } into grouping
                                where grouping.Count() >= 2
                                from participant in grouping
                                let genInfo = participant.ProjectGeneralInfo
                                join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                                join pType in _db.MT_ListOfValue on genInfo.ProjectInformation.DisabilityTypeID equals pType.LOVID
                                where (fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว) && (genInfo.ProvinceID == provinceIDFilter)
                                group participant.Gender by new
                                {
                                    genInfo.ProjectInformation.BudgetYear,
                                    genInfo.ProjectInformation.DisabilityTypeID,
                                    pType.OrderNo,
                                    pType.LOVName
                                } into grouping

                                select new ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType
                                {
                                    BudgetYear = grouping.Key.BudgetYear,
                                    TypeOrderNo = grouping.Key.OrderNo,
                                    TypeName = grouping.Key.LOVName,
                                    //IdCardNo = grouping.Key.IDCardNo,
                                    //Gender = grouping.Key.Gender,
                                    MaleAmount = grouping.Count(x=> x=="M"),
                                    FemaleAmount = grouping.Count(x => x == "F"),
                                    CountIDCardNo = grouping.Count()
                                }
                            ).ToQueryData(p);

                    _db.Database.Log = ((x) => { Common.Logging.LogInfo("->>", x); });
                    result = data;
                }else{
                    var query = (from participant in _db.ProjectParticipants
                                 join fStatus in _db.MT_ListOfValue on participant.ProjectGeneralInfo.FollowUpStatus equals fStatus.LOVID
                                 where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
                                 group participant by new
                                 {
                                     participant.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                                     participant.IDCardNo
                                 } into grouping
                                 where grouping.Count() >= 2
                                 from participant in grouping
                                 let genInfo = participant.ProjectGeneralInfo
                                 join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                                 join pType in _db.MT_ListOfValue on genInfo.ProjectInformation.DisabilityTypeID equals pType.LOVID
                                 where (fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว)
                                 group participant.Gender by new
                                 {
                                     genInfo.ProjectInformation.BudgetYear,
                                     genInfo.ProjectInformation.DisabilityTypeID,
                                     pType.OrderNo,
                                     pType.LOVName,
                                 } into grouping

                                 select new ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByType
                                 {
                                     BudgetYear = grouping.Key.BudgetYear,
                                     TypeOrderNo = grouping.Key.OrderNo,
                                     TypeName = grouping.Key.LOVName,
                                     MaleAmount = grouping.Count(x => x == "M"),
                                     FemaleAmount = grouping.Count(x => x == "F"),
                                     CountIDCardNo = grouping.Count()
                                 }
                            );
                    var data = query.ToQueryData(p);

                    //_db.Database.Log = ((x) => { Common.Logging.LogInfo("->>", x); });
                    result = data;
                }

                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report Statistic", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByStrategic> ListAnalyzeProjectByStrategic(ServiceModels.QueryParameter p, Decimal? provinceID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByStrategic> result;
            result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByStrategic>();

            try
            {
                var mainQuery = from participant in _db.ProjectParticipants
                                join fStatus in _db.MT_ListOfValue on participant.ProjectGeneralInfo.FollowUpStatus equals fStatus.LOVID
                                where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
                                group participant by new
                                {
                                    participant.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                                    participant.IDCardNo
                                } into grouping
                                where grouping.Count() >= 2
                                from participant in grouping
                                let genInfo = participant.ProjectGeneralInfo
                                join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                                where (fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว)
                                select new { participant, genInfo, genInfo.ProjectEvaluation };

                if (provinceID.HasValue)
                {
                    decimal provinceIDFilter = (decimal)provinceID;
                    mainQuery = mainQuery.Where(x => x.genInfo.ProvinceID == provinceID);
                }

                var data = (from q in mainQuery
                            group q.participant.Gender by new
                            {
                                q.genInfo.ProjectInformation.BudgetYear,
                                StrategicCode = q.ProjectEvaluation.IsPassMission1 == "1" ? "1"
                                                : (q.ProjectEvaluation.IsPassMission2 == "1" ? "2"
                                                : (q.ProjectEvaluation.IsPassMission3 == "1" ? "3"
                                                : (q.ProjectEvaluation.IsPassMission4 == "1" ? "4"
                                                : (q.ProjectEvaluation.IsPassMission5 == "1" ? "5" : "0"))))
                            } into grouping
                            where grouping.Key.StrategicCode != "0"
                            select new ServiceModels.Report.ReportStatisticDuplicate.AnalyzeProjectByStrategic
                            {
                                BudgetYear = grouping.Key.BudgetYear,
                                StrategicCode = grouping.Key.StrategicCode,
                                MaleAmount = grouping.Count(x => x == "M"),
                                FemaleAmount = grouping.Count(x => x == "F"),
                                CountIDCardNo = grouping.Count()
                            }).ToQueryData(p);
                result = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report Statistic", ex);
            }
            return result;
        }
    }
}
