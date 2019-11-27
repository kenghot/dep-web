using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ReportOverlapProvinceService : IServices.IReportOverlapProvinceService
    {
          private readonly DBModels.Model.NepProjectDBEntities _db;
          private readonly ServiceModels.Security.SecurityInfo _user;

          public ReportOverlapProvinceService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user)
        {
            _db = db;
            _user = user;
        }


          public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince> ListReportOverlapProvince(ServiceModels.QueryParameter p)
          {
              ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince>();

            try
            {
                result = _db.View_ParticipantProvinceDup.Select(x => new ServiceModels.Report.ReportOverlapProvince
                {                   
                    RegionID = x.SectionID,
                    Region = x.SectionName,
                    ProvinceID = x.ProvinceID,
                    Province = x.ProvinceName,                    
                    BudgetYear = x.BudgetYear,
                    Disablility = x.DisabilityTypeName,
                    DisablilityAmt = (x.CountParticipant != null)? (decimal)x.CountParticipant : 0
                }).ToQueryData(p);
                  
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Report Statistic", ex);
            }
            return result;


            //try
            //{
            //    result = (from genInfo in _db.ProjectGeneralInfoes
            //              join proInfo in _db.ProjectInformations on genInfo.ProjectID equals proInfo.ProjectID 
            //              join province in _db.MT_Province on genInfo.ProvinceID equals province.ProvinceID
            //              join Region in _db.MT_ListOfValue on province.SectionID equals Region.LOVID
            //              join Disablilitys in _db.MT_ListOfValue on proInfo.DisabilityTypeID equals Disablilitys.LOVID
            //              join Report in _db.ProjectReports on proInfo.ProjectID equals Report.ProjectID

            //              select new ServiceModels.Report.ReportOverlapProvince()
            //          {
            //              Region = Region.LOVName,
            //              RegionID = Region.LOVID,
            //              Province = province.ProvinceName,
            //              ProvinceID = genInfo.ProvinceID,
            //              BudgetYear = proInfo.BudgetYear,
            //              Disablility = Disablilitys.LOVName ,
            //              DisablilityAmt = Report.MaleParticipant + Report.FemaleParticipant
            //               }).ToQueryData(p);

            //}
            //catch (Exception ex)
            //{
            //    result.IsCompleted = false;
            //    result.Message.Add(ex.Message);
            //    Common.Logging.LogError(Logging.ErrorType.ServiceError, "ReportOverlapService", ex);
            //}

            //return result;
          }
    }
}
