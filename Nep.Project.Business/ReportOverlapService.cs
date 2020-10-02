using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportOverlapService : IServices.IReportOverlapService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;

        public ReportOverlapService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }

        //public ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>> ListReportOverlap(Decimal? Year, Decimal? ProvinceID, List<String> Names, List<String> IDCardNos)
        //{
        //    ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>> result = new ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>>();
        //    try
        //    {
        //        var query = (from genInfo in _db.ProjectGeneralInfoes
        //                     join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
        //                     join proInfo in _db.ProjectInformations on genInfo.ProjectID equals proInfo.ProjectID
        //                     join province in _db.MT_Province on genInfo.ProvinceID equals province.ProvinceID
        //                     join participant in _db.ProjectParticipants on genInfo.ProjectID equals participant.ProjectID
        //                     where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
        //                     select new ServiceModels.Report.ReportOverlap()
        //                     {
        //                         Name = participant.FirstName + " " + participant.LastName,
        //                         IdCardNo = participant.IDCardNo,
        //                         ProjectName = proInfo.ProjectNameTH,
        //                         BudgetYear = proInfo.BudgetYear,
        //                         ProvinceID = genInfo.ProvinceID,
        //                         Province = province.ProvinceName
        //                     });
        //        var duplicatedIds = query.GroupBy(y => y.IdCardNo).Where(z => z.Count() >= 2).Select(z => z.Key);
        //        var filteredDuplicatedIds = query.Where(x => duplicatedIds.Contains(x.IdCardNo));
        //        if (Year.HasValue)
        //        {
        //            filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.BudgetYear == Year);
        //        }
                
        //        if (ProvinceID.HasValue)
        //        {
        //            filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.ProvinceID == ProvinceID);
        //        }
        //        Func<ServiceModels.Report.ReportOverlap, bool> predicate = p => false;
        //        if (Names.Count() > 0)
        //        {
        //            foreach (var Name in Names)
        //            {
        //                //filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.Name.Contains(Name));
        //                var oldPredicate = predicate;
        //                predicate = p => oldPredicate(p) || p.IdCardNo.Contains(Name);
        //            }
                   
        //        }

        //        if (IDCardNos.Count() > 0)
        //        {
        //            foreach (var IDCardNo in IDCardNos)
        //            {
        //                // filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.IdCardNo.Contains(IDCardNo));
        //                var oldPredicate = predicate;
        //                predicate = p => oldPredicate(p) || p.IdCardNo.Contains(IDCardNo) ;
        //            }
                    
        //        }
        //        var w = filteredDuplicatedIds.Where(predicate);
        //        //result.Data = query.Where(x => filteredDuplicatedIds.Select(f => f.IdCardNo).Contains(x.IdCardNo)).ToList();
        //        result.Data = query.Where(x => w.Select(f => f.IdCardNo).Contains(x.IdCardNo)).ToList();
        //        result.IsCompleted = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsCompleted = false;
        //        result.Message.Add(ex.Message);
        //        Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportOverlapService", ex);
        //    }

        //    return result;
        //}

        public ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>> ListReportOverlap(Decimal? Year, Decimal? ProvinceID, List<String> Names, List<String> IDCardNos)
        {
            ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>> result = new ServiceModels.ReturnObject<List<ServiceModels.Report.ReportOverlap>>();
            try
            {
                var query = (from genInfo in _db.ProjectGeneralInfoes
                             join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                             join proInfo in _db.ProjectInformations on genInfo.ProjectID equals proInfo.ProjectID
                             join province in _db.MT_Province on genInfo.ProvinceID equals province.ProvinceID
                             join participant in _db.ProjectParticipants on genInfo.ProjectID equals participant.ProjectID
                             where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว && (participant.IDCardNo != null)
                             select new ServiceModels.Report.ReportOverlap()
                             {
                                 Name = participant.FirstName + " " + participant.LastName,
                                 IdCardNo = participant.IDCardNo,
                                 ProjectName = proInfo.ProjectNameTH,
                                 BudgetYear = proInfo.BudgetYear,
                                 ProvinceID = genInfo.ProvinceID,
                                 Province = province.ProvinceName
                             });
                var duplicatedIds = query.GroupBy(y => y.IdCardNo).Where(z => z.Count() >= 2).Select(z => z.Key);
                var filteredDuplicatedIds = query.Where(x => duplicatedIds.Contains(x.IdCardNo));
                if (Year.HasValue)
                {
                    //filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.BudgetYear == Year);
                    query = query.Where(x => x.BudgetYear == Year);
                }

                if (ProvinceID.HasValue)
                {
                    //filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.ProvinceID == ProvinceID);
                    query = query.Where(x => x.ProvinceID == ProvinceID);
                }
                Func<ServiceModels.Report.ReportOverlap, bool> predicate = p => false;
               
                if (Names.Count() > 0)
                {
                    foreach (var Name in Names)
                    {
                        //filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.Name.Contains(Name));
                        var namePredicate = predicate;
                        predicate = p => namePredicate(p) || p.Name.Contains(Name.Trim());
                    }

                }

                if (IDCardNos.Count() > 0)
                {
                    foreach (var IDCardNo in IDCardNos)
                    {
                        // filteredDuplicatedIds = filteredDuplicatedIds.Where(x => x.IdCardNo.Contains(IDCardNo));
                        var oldPredicate = predicate;
                        predicate = p => oldPredicate(p) || p.IdCardNo.Contains(IDCardNo.Trim());
                    }

                }
                //var w = filteredDuplicatedIds.Where(predicate);
                //result.Data = query.Where(x => filteredDuplicatedIds.Select(f => f.IdCardNo).Contains(x.IdCardNo)).ToList();
                if (Names.Count() > 0  ||  IDCardNos.Count() > 0) {
                    query = query.Where(predicate).AsQueryable();
                }
                 
                var all = query.ToList();
                var nondups = from a in all group a by a.IdCardNo into grp where grp.Count() <= 1 select grp.Key;
                foreach (var non in nondups)
                {
                    all.RemoveAll(r => r.IdCardNo == non);
                }
                //result.Data = query.Where(x => w.Select(f => f.IdCardNo).Contains(x.IdCardNo)).ToList();
                result.Data = all;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportOverlapService", ex);
            }

            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlap> ListReportOverlap(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlap> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlap>();
            try
            {
                var query = (from genInfo in _db.ProjectGeneralInfoes
                             join fStatus in _db.MT_ListOfValue on genInfo.FollowUpStatus equals fStatus.LOVID
                             join proInfo in _db.ProjectInformations on genInfo.ProjectID equals proInfo.ProjectID
                             join province in _db.MT_Province on genInfo.ProvinceID equals province.ProvinceID
                             join participant in _db.ProjectParticipants on genInfo.ProjectID equals participant.ProjectID
                             where fStatus.LOVCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว
                             select new ServiceModels.Report.ReportOverlap()
                            {
                                Name = participant.FirstName + " " + participant.LastName,
                                IdCardNo = participant.IDCardNo,
                                ProjectName = proInfo.ProjectNameTH,
                                BudgetYear = proInfo.BudgetYear,
                                ProvinceID = genInfo.ProvinceID,
                                Province = province.ProvinceName
                            });
                var duplicatedIds = query.GroupBy(y => y.IdCardNo).Where(z => z.Count() >= 2).Select(z => z.Key);

                result = query.Where(x => duplicatedIds.Contains(x.IdCardNo)).ToQueryData(p);

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportOverlapService", ex);
            }

            return result;
        }
    }
}
