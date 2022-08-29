using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nep.Project.Business
{
    public class SetFollowupStatusJobService : IServices.ISetFollowupStatusJobService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly MailService _mailService;
        private readonly SmsService _smsService;
        private const Decimal MAX_EXCLUDED_YEAR = 2016;

        public SetFollowupStatusJobService(DBModels.Model.NepProjectDBEntities db, MailService mailService,  SmsService smsService)
        {
            _db = db;
            _mailService = mailService;
            _smsService = smsService;           
        }

        public void SetFollowupStatus()
        {
            SendTrackingWarning30Days();
            SendTrackingWarning45Days();
            ResendTrackingWarning30Days();
            SendTrackingWarningContract();
        }

        private void SendTrackingWarningContract()
        {
            try
            {

                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CurrentCulture;
                DateTime toDay = DateTime.Today;
                DateTime compareDate = toDay.AddDays(30);
                DateTime tmpDate = new DateTime(compareDate.Year, compareDate.Month, compareDate.Day, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                int cYear = compareDate.Year;
                int cMonth = compareDate.Month;
                int cDay = compareDate.Day;

                var dues = _db.CONTRACTDUEs.Where(w => w.STARTDATE == compareDate && w.MAILSENT != "1").ToList();
                var data = (from d in _db.CONTRACTDUEs
                             join i in _db.ProjectInformations on d.PROJECTID equals i.ProjectID
                             join g in _db.ProjectGeneralInfoes on i.ProjectID equals g.ProjectID
                             join person in _db.ProjectPersonels on i.ProjectID equals person.ProjectID
                             join c in _db.ProjectContracts on i.ProjectID equals c.ProjectID
                             join org in _db.MT_Organization on g.OrganizationID equals org.OrganizationID
                             where d.STARTDATE == compareDate && d.MAILSENT != "1"
                             select new ServiceModels.TemplateConfig.ContractDueWarning
                             {
                                 Amount = d.AMOUNT,
                                 DueId = d.DUEID,
                                 EndDate = d.ENDDATE,
                                 OrganizationTHName = org.OrganizationNameTH,
                                 ProjectTHName = i.ProjectNameTH,
                                 StartDate = d.STARTDATE,
                                 Email1 = person.Email1,
                                 Email = org.Email,
                                 DueNo = d.DUENO,

                             }).ToList();
     


                SendContractWarning(data);
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Contract Due", ex);
            }
        }
        private void SendContractWarning(List<ServiceModels.TemplateConfig.ContractDueWarning> datas)
        {
            var folloupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
            var nepProjectDirectorPosition = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_PROJECT_DIRECTOR_POSITION).Select(y => y.ParameterValue).FirstOrDefault();

            ServiceModels.TemplateConfig.ContractDueWarning param;
            String smsFormat = Nep.Project.Resources.Message.WarningProjectReportResultSMS;
            //String sms;
            //String endDateProjectReport;
            //String errorSendTracking = Nep.Project.Resources.Error.TrackingProjectReportError;
            //String orgName = "";
 


            if (datas != null)
            {
                for (int i = 0; i < datas.Count; i++)
                {
                    try
                    {
                        param = datas[i];

                        _mailService.SendWarningContractDue(folloupContact, param ,param.Email);

                        if (param.Email.ToLower() == param.Email1.ToLower())
                        {
                            _mailService.SendWarningContractDue(folloupContact, param, param.Email1);
                            //_smsService.Send("แจ้งเตือนการส่งรายงานผลโครงการ", _db, param.ProjectID);
                        }
                      


                        var due = _db.CONTRACTDUEs.Where(w => w.DUEID == param.DueId).FirstOrDefault();
                        if (due != null)
                        {
                            due.MAILSENT = "1";
                            _db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", String.Format(errorSendTracking, orgName));
                        Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", ex);
                    }
                }
            }
        }
        private void SendTrackingWarning30Days()
        {
            try
            {                

                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CurrentCulture;
                DateTime toDay = DateTime.Today;
                DateTime compareDate = toDay.AddDays(-30);
                DateTime tmpDate = new DateTime(compareDate.Year, compareDate.Month, compareDate.Day, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                int cYear = compareDate.Year;
                int cMonth = compareDate.Month;
                int cDay = compareDate.Day;
              

                var isFolloupStatus = _db.MT_ListOfValue.Where(x => x.LOVCode == Common.LOVCode.Followupstatus.กำลังติดตาม && (x.LOVGroup == Common.LOVGroup.FollowupStatus)).FirstOrDefault();
                decimal? isFolloupStatusID = (isFolloupStatus != null)? isFolloupStatus.LOVID : 0;
                List<ServiceModels.TemplateConfig.OrgWaringReportParam> projectList;
                var query = (from g in _db.ProjectGeneralInfoes
                             join person in _db.ProjectPersonels on g.ProjectID equals person.ProjectID
                             join c in _db.ProjectContracts on g.ProjectID equals c.ProjectID
                             join org in _db.MT_Organization on g.OrganizationID equals org.OrganizationID
                             join tel in _db.MT_TELEPHONE on org.ProvinceID equals tel.PROVINCEID
                             where (c.ContractYear > MAX_EXCLUDED_YEAR) && (g.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว) &&
                                 ((g.FollowUpStatus == null) && (c.ContractEndDate <= DbFunctions.CreateDateTime(cYear, cMonth, cDay, 0, 0, 0)))
                                 
                             select new ServiceModels.TemplateConfig.OrgWaringReportParam
                             {
                                 ProjectID = g.ProjectID,
                                 LOVName = person.Prefix1Personel.LOVName,
                                 Firstname1 = person.Firstname1,
                                 Lastname1 = person.Lastname1,
                                 Mobile1 = person.Mobile1,
                                 Email1 = person.Email1,

                                 ContractNo = g.ProjectContract.ContractNo,
                                 ContractDate = g.ProjectContract.ContractDate,

                                 OrganizationNameTH = org.OrganizationNameTH,

                                 ProjectNameTH = g.ProjectInformation.ProjectNameTH,
                                 EndDate = g.ProjectOperation.EndDate,
                                 BudgetReviseValue = g.BudgetReviseValue,

                                 Mobile = org.Mobile,
                                 Email = org.Email,

                                 ORGANIZATIONNAME = tel.ORGANIZATIONNAME,
                                 TELEPHONE1 = tel.TELEPHONE1,
                                 EXTENSION1 = tel.EXTENSION1,
                                 TELEPHONE2 = tel.TELEPHONE2,
                                 EXTENSION2 = tel.EXTENSION2,
                                 FAXNUMBER1 = tel.FAXNUMBER1,
                                 FAXEXTENSION1 = tel.FAXEXTENSION1,
                                 FAXNUMBER2 = tel.FAXNUMBER2,
                                 FAXEXTENSION2 = tel.FAXEXTENSION2
                             }
                               );

                
                //dataContext.GetCommand(query).CommandText;

                //Common.Logging.LogInfo("30 Date->>", String.Format("{0}-{1}-{2}", cYear, cMonth, cDay));
                //Common.Logging.LogInfo("->> ", query.ToString());
                string sql = query.ToString();
                //sql.Replace(":p__linq__0 ", isFolloupStatusID.ToString());
                sql = sql.Replace(":p__linq__0", cYear.ToString());
                sql = sql.Replace(":p__linq__1", cMonth.ToString());
                sql = sql.Replace(":p__linq__2", cDay.ToString());

                //_db.Database.ExecuteSqlCommand(sql, null);
                //Common.Logging.LogInfo("->>>> ", sql);
                var sqlResult = _db.Database.SqlQuery<ServiceModels.TemplateConfig.OrgWaringReportParam>(sql);
                //_db.Database.Log = ((x) => { Common.Logging.LogInfo("->>", x); });

                projectList = (sqlResult != null) ? sqlResult.ToList() : null;
                

                _db.Database.Log = null;

                DBModels.Model.MT_ListOfValue followupStatus = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.ถึงกำหนดติดตาม_30_วัน)).FirstOrDefault();
                
                SendTrackingWarning(projectList, followupStatus.LOVID);
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", ex);
            }
        }

        private void SendTrackingWarning45Days()
        {
            try
            {
                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CurrentCulture;
                DateTime toDay = DateTime.Today;
                DateTime compareDate = toDay.AddDays(-45);
                Decimal tracking30DaysID = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.ถึงกำหนดติดตาม_30_วัน)).Select(y => y.LOVID).FirstOrDefault();
                Decimal tracking45DaysID = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.ถึงกำหนดติดตาม_45_วัน)).Select(y => y.LOVID).FirstOrDefault();
                int cYear = compareDate.Year;
                int cMonth = compareDate.Month;
                int cDay = compareDate.Day;

                List<ServiceModels.TemplateConfig.OrgWaringReportParam> projectList;
                var query = (from g in _db.ProjectGeneralInfoes
                               join person in _db.ProjectPersonels on g.ProjectID equals person.ProjectID
                               join c in _db.ProjectContracts on g.ProjectID equals c.ProjectID
                               join org in _db.MT_Organization on g.OrganizationID equals org.OrganizationID
                                join tel in _db.MT_TELEPHONE on org.ProvinceID equals tel.PROVINCEID
                                 where (c.ContractYear > MAX_EXCLUDED_YEAR) && (g.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว) &&
                                   ((g.FollowUpStatus != null) && (g.FollowUpStatus == tracking30DaysID) && (c.ContractEndDate <= DbFunctions.CreateDateTime(cYear, cMonth, cDay,0,0,0)))
                               select new ServiceModels.TemplateConfig.OrgWaringReportParam
                               {
                                   ProjectID = g.ProjectID,
                                   LOVName = person.Prefix1Personel.LOVName,
                                   Firstname1 = person.Firstname1,
                                   Lastname1 = person.Lastname1,
                                   Mobile1 = person.Mobile1,
                                   Email1 = person.Email1,

                                   ContractNo = g.ProjectContract.ContractNo,
                                   ContractDate = g.ProjectContract.ContractDate,

                                   OrganizationNameTH = org.OrganizationNameTH,

                                   ProjectNameTH = g.ProjectInformation.ProjectNameTH,
                                   EndDate = g.ProjectOperation.EndDate,
                                   BudgetReviseValue = g.BudgetReviseValue,

                                   Mobile = org.Mobile,
                                   Email = org.Email,

                                   ORGANIZATIONNAME = tel.ORGANIZATIONNAME,
                                   TELEPHONE1 = tel.TELEPHONE1,
                                   EXTENSION1 = tel.EXTENSION1,
                                   TELEPHONE2 = tel.TELEPHONE2,
                                   EXTENSION2 = tel.EXTENSION2,
                                   FAXNUMBER1 = tel.FAXNUMBER1,
                                   FAXEXTENSION1 = tel.FAXEXTENSION1,
                                   FAXNUMBER2 = tel.FAXNUMBER2,
                                   FAXEXTENSION2 = tel.FAXEXTENSION2
                               });

                string sql = query.ToString();
                sql = sql.Replace(":p__linq__0", tracking30DaysID.ToString());
                sql = sql.Replace(":p__linq__1", cYear.ToString());
                sql = sql.Replace(":p__linq__2", cMonth.ToString());
                sql = sql.Replace(":p__linq__3", cDay.ToString());

                //Common.Logging.LogInfo("45 Date->>", String.Format("{0}-{1}-{2}", cYear, cMonth, cDay));
                //Common.Logging.LogInfo("Send 45->>", sql);
                var sqlResult = _db.Database.SqlQuery<ServiceModels.TemplateConfig.OrgWaringReportParam>(sql);
                //_db.Database.Log = ((x) => { Common.Logging.LogInfo("45 ->>", x); });
                projectList = (sqlResult != null)? sqlResult.ToList() : null;
                SendTrackingWarning(projectList, tracking45DaysID);
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", ex);
            }
        }

        private void ResendTrackingWarning30Days()
        {
            try
            {
              
                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CurrentCulture;
                DateTime toDay = DateTime.Today;
                DateTime compareDate = toDay.AddDays(-30);
                DateTime tmpDate = new DateTime(compareDate.Year, compareDate.Month, compareDate.Day, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                int cYear = compareDate.Year;
                int cMonth = compareDate.Month;
                int cDay = compareDate.Day;


                var isFolloupStatus = _db.MT_ListOfValue.Where(x => x.LOVCode == Common.LOVCode.Followupstatus.กำลังติดตาม && (x.LOVGroup == Common.LOVGroup.FollowupStatus)).FirstOrDefault();
                Decimal tracking45DaysID = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.ถึงกำหนดติดตาม_45_วัน)).Select(y => y.LOVID).FirstOrDefault();

                Decimal folloupStatusID = (isFolloupStatus != null) ? isFolloupStatus.LOVID : 0;
                List<ServiceModels.TemplateConfig.OrgWaringReportParam> projectList;
                var query = (from g in _db.ProjectGeneralInfoes
                             join person in _db.ProjectPersonels on g.ProjectID equals person.ProjectID
                             join c in _db.ProjectContracts on g.ProjectID equals c.ProjectID
                             join org in _db.MT_Organization on g.OrganizationID equals org.OrganizationID
                             join tel in _db.MT_TELEPHONE on org.ProvinceID equals tel.PROVINCEID
                             where (c.ContractYear > MAX_EXCLUDED_YEAR) && (g.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว) &&
                                 ((g.FollowUpStatus != null) && (g.FollowUpStatus == tracking45DaysID || g.FollowUpStatus == folloupStatusID)) && ( (g.LastedFollowupDate != null) && ( g.LastedFollowupDate < DbFunctions.CreateDateTime(cYear, cMonth, cDay, 0, 0, 0)))

                             select new ServiceModels.TemplateConfig.OrgWaringReportParam
                             {
                                 ProjectID = g.ProjectID,
                                 LOVName = person.Prefix1Personel.LOVName,
                                 Firstname1 = person.Firstname1,
                                 Lastname1 = person.Lastname1,
                                 Mobile1 = person.Mobile1,
                                 Email1 = person.Email1,

                                 ContractNo = g.ProjectContract.ContractNo,
                                 ContractDate = g.ProjectContract.ContractDate,

                                 OrganizationNameTH = org.OrganizationNameTH,

                                 ProjectNameTH = g.ProjectInformation.ProjectNameTH,
                                 EndDate = g.ProjectOperation.EndDate,
                                 BudgetReviseValue = g.BudgetReviseValue,

                                 Mobile = org.Mobile,
                                 Email = org.Email,

                                 ORGANIZATIONNAME = tel.ORGANIZATIONNAME,
                                 TELEPHONE1 = tel.TELEPHONE1,
                                 EXTENSION1 = tel.EXTENSION1,
                                 TELEPHONE2 = tel.TELEPHONE2,
                                 EXTENSION2 = tel.EXTENSION2,
                                 FAXNUMBER1 = tel.FAXNUMBER1,
                                 FAXEXTENSION1 = tel.FAXEXTENSION1,
                                 FAXNUMBER2 = tel.FAXNUMBER2,
                                 FAXEXTENSION2 = tel.FAXEXTENSION2
                             }
                               );
                              
                string sql = query.ToString();
                //sql.Replace(":p__linq__0 ", isFolloupStatusID.ToString());
                sql = sql.Replace(":p__linq__0", tracking45DaysID.ToString());
                sql = sql.Replace(":p__linq__1", folloupStatusID.ToString());
                sql = sql.Replace(":p__linq__2", cYear.ToString());
                sql = sql.Replace(":p__linq__3", cMonth.ToString());
                sql = sql.Replace(":p__linq__4", cDay.ToString());

                
                //Common.Logging.LogInfo("Resend Date->>", String.Format("{0}-{1}-{2}", cYear, cMonth, cDay));
                var sqlResult = _db.Database.SqlQuery<ServiceModels.TemplateConfig.OrgWaringReportParam>(sql);
                //_db.Database.Log = ((x) => { Common.Logging.LogInfo("Resend->>", x); });

                projectList = (sqlResult != null) ? sqlResult.ToList() : null;


                _db.Database.Log = null;

                DBModels.Model.MT_ListOfValue followupStatus = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.ถึงกำหนดติดตาม_30_วัน)).FirstOrDefault();

                SendTrackingWarning(projectList, (decimal?)null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", ex);
            }
        }

        private void SendTrackingWarning(List<ServiceModels.TemplateConfig.OrgWaringReportParam> projectList, decimal? followupStatusID)
        {
            var folloupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
            var nepProjectDirectorPosition = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_PROJECT_DIRECTOR_POSITION).Select(y => y.ParameterValue).FirstOrDefault();

            ServiceModels.TemplateConfig.OrgWaringReportParam param;
            String smsFormat = Nep.Project.Resources.Message.WarningProjectReportResultSMS;
            String sms;
            String endDateProjectReport;
            String errorSendTracking = Nep.Project.Resources.Error.TrackingProjectReportError;
            String orgName = "";
            String orgMobile = "";
            String personMobile = "";
          
            
            if (projectList != null)
            {
                for (int i = 0; i < projectList.Count; i++)
                {
                    try
                    {
                        param = projectList[i];
                        param.ContractNo = Common.Web.WebUtility.ParseToThaiNumber(param.ContractNo);
                        orgName = param.OrganizationNameTH;
                        orgMobile = param.Mobile;
                        personMobile = param.Mobile1;
                        endDateProjectReport = Common.Web.WebUtility.ToBuddhaDateFormat(param.EnddateProjectReport, "dMMMyy", "");
                        endDateProjectReport = endDateProjectReport.Replace(".", "");
                        param.TELEPHONE1 = Common.Web.WebUtility.ParseToThaiNumber(param.TELEPHONE1);
                        param.EXTENSION1 = Common.Web.WebUtility.ParseToThaiNumber(param.EXTENSION1);
                        param.TELEPHONE2 = Common.Web.WebUtility.ParseToThaiNumber(param.TELEPHONE2);
                        param.EXTENSION2 = Common.Web.WebUtility.ParseToThaiNumber(param.EXTENSION2);
                        param.FAXNUMBER1 = Common.Web.WebUtility.ParseToThaiNumber(param.FAXNUMBER1);
                        param.FAXEXTENSION1 = Common.Web.WebUtility.ParseToThaiNumber(param.FAXEXTENSION1);
                        param.FAXNUMBER2 = Common.Web.WebUtility.ParseToThaiNumber(param.FAXNUMBER2);
                        param.FAXEXTENSION2 = Common.Web.WebUtility.ParseToThaiNumber(param.FAXEXTENSION2);
                        sms = String.Format(smsFormat, endDateProjectReport);

                        if (param.Email.ToLower() == param.Email1.ToLower())
                        {
                            _mailService.SendWarningProjectReportResultToOrg(folloupContact, nepProjectDirectorPosition, param);
                            _smsService.Send("แจ้งเตือนการส่งรายงานผลโครงการ", _db, param.ProjectID);
                        }
                        else
                        {
                            _mailService.SendWarningProjectReportResultToOrg(folloupContact, nepProjectDirectorPosition, param);
                            _mailService.SendWarningProjectReportResultToPerson(folloupContact, nepProjectDirectorPosition, param);
                            _smsService.Send("แจ้งเตือนการส่งรายงานผลโครงการ", _db, param.ProjectID);
                        }

                        _smsService.Send(sms, param.Mobile, param.Mobile1);
                        UpdateFollowupStatus(param.ProjectID, followupStatusID);
                    }
                    catch (Exception ex)
                    {
                        Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", String.Format(errorSendTracking, orgName));
                        Common.Logging.LogError(Logging.ErrorType.ServiceError, "Set Followup Status Job", ex);
                    }                   
                }
            }
        }

   
        private void UpdateFollowupStatus(decimal projectID, decimal? followupStautsID)
        {           
            DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
            DateTime toDay = DateTime.Today;
            if(genInfo != null){
                if (followupStautsID.HasValue)
                {
                    genInfo.FollowUpStatus = followupStautsID;
                }
                genInfo.LastedFollowupDate = toDay;
                _db.SaveChanges();
            }
        }
        
    }
}
