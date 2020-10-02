using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;
using Nep.Project.Common;
using Nep.Project.Common.Report;
using System.Transactions;
using System.IO;
using System.Web.Hosting;
using System.Xml;
using System.Reflection;
using Nep.Project.ServiceModels.ProjectInfo;
using Nep.Project.DBModels.Model;
using Nep.Project.Business;
using Nep.Project.Common.Web;
 
namespace Nep.Project.Business
{
    public class ProjectInfoService : IServices.IProjectInfoService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;
        private readonly IServices.IRunningNumberService _runningService;
        private readonly MailService _mailService;
        private readonly SmsService _smsService;
        private const String PROJECT_FOLDER_NAME = "Project\\";
        private const int EVALUATION_PASS_SCORE = 70;
        private const String TABLE_PROJECTPERSONEL = "PROJECTPERSONEL";
        private const String TABLE_PROJECTCONTRACT = "PROJECTCONTRACT";
        private const String TABLE_PROJECTDOCUMENT = "PROJECTDOCUMENT";
        private const String TABLE_PROJECTPROCESSED = "PROJECTPROCESSED";

        private const String PROJECTPROCESSED_IMAGE = "IMAGE";
        private const String PERSON_INSTRUCTOR = "INSTRUCTOR";
        private const String PERSON_VEHICLE = "VEHICLE";
        private const String PERSON_VALUNTEER = "VALUNTEER";
        private const String TABLE_REPORT = "PROJECTREPORT";
        private const String REPORT_BUDGET = "BUDGET";
        private const String REPORT_ACTIVITY = "REPORT_ACTIVITY";
        private const String REPORT_PARTICIPANT = "REPORT_PARTICIPANT";
        private const String REPORT_RESULT = "REPORT_RESULT";
        private const String CONTRACT_SUPPORT = "CONTRACT_SUPPORT";

        public ProjectInfoService(DBModels.Model.NepProjectDBEntities db,
            ServiceModels.Security.SecurityInfo user,
            IServices.IRunningNumberService runningService,
            MailService mailService,
            SmsService smsService)
        {
            _db = db;
            _user = user;
            _runningService = runningService;
            _mailService = mailService;
            _smsService = smsService;
        }

        #region Delete Project
        public ServiceModels.ReturnMessage DeleteProject(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                #region ลบข้อมูลโครงการ
                DBModels.Model.ProjectInformation proInfo = _db.ProjectInformations.Where(x => x.ProjectID == id).FirstOrDefault();
                if (proInfo != null)
                {
                    _db.ProjectInformations.Remove(proInfo);
                }


                List<DBModels.Model.ProjectTargetGroup> proTargetGroups = _db.ProjectTargetGroups.Where(x => x.ProjectID == id).ToList();
                if ((proTargetGroups != null) && (proTargetGroups.Count > 0))
                {
                    _db.ProjectTargetGroups.RemoveRange(proTargetGroups);
                }
                #endregion ลบข้อมูลโครงการ

                #region ลบข้อมูลบุคลากร
                DBModels.Model.ProjectPersonel personel = _db.ProjectPersonels.Where(x => x.ProjectID == id).FirstOrDefault();
                if (personel != null)
                {
                    _db.ProjectPersonels.Remove(personel);
                    RemoveProjectPersonalFile(personel);
                }
                #endregion ลบข้อมูลบุคลากร

                #region ลบข้อมูลการดำเนินงาน
                DBModels.Model.ProjectOperation proOperation = _db.ProjectOperations.Where(x => x.ProjectID == id).FirstOrDefault();
                if (proOperation != null)
                {
                    _db.ProjectOperations.Remove(proOperation);
                    RemoveProjectOperationFile(proOperation.LocationMap);
                }
                #endregion ลบข้อมูลการดำเนินงาน

                #region ลบข้อมูลงบประมาณ
                List<DBModels.Model.ProjectBudget> budgets = _db.ProjectBudgets.Where(x => x.ProjectID == id).ToList();
                if ((budgets != null) && (budgets.Count > 0))
                {
                    _db.ProjectBudgets.RemoveRange(budgets);
                }
                #endregion ลบข้อมูลงบประมาณ

                #region ลบข้อมูลเอกสารแนบ
                DBModels.Model.ProjectDocument doc = _db.ProjectDocuments.Where(x => x.ProjectID == id).FirstOrDefault();
                if (doc != null)
                {
                    _db.ProjectDocuments.Remove(doc);
                    RemoveProjectDocumentFile(doc);
                }
                #endregion ลบข้อมูลเอกสารแนบ

                #region ลบข้อมูลคณะกรรมการ
                List<DBModels.Model.ProjectCommittee> committees = _db.ProjectCommittees.Where(x => x.ProjectID == id).ToList();
                if (committees != null && (committees.Count > 0))
                {
                    _db.ProjectCommittees.RemoveRange(committees);
                }
                #endregion ลบข้อมูลคณะกรรมการ

                #region ลบข้อมูลการประเมิน
                DBModels.Model.ProjectEvaluation eval = _db.ProjectEvaluations.Where(x => x.ProjectID == id).FirstOrDefault();
                if (eval != null)
                {
                    _db.ProjectEvaluations.Remove(eval);
                    //_db.SaveChanges();
                }
                #endregion ลบข้อมูลการประเมิน

                #region ลบข้อมูลสถานที่ดำเนินโครงการ
                List<DBModels.Model.ProjectOperationAddress> opAddress = _db.ProjectOperationAddresses.Where(x => x.ProjectID == id).ToList();
                if ((opAddress != null) && (opAddress.Count > 0))
                {
                    _db.ProjectOperationAddresses.RemoveRange(opAddress);
                }
                #endregion ลบข้อมูลสถานที่ดำเนินโครงการ
                var contracts = _db.ProjectContracts.Where(w => w.ProjectID == id).ToList();
                if (contracts != null && contracts.Count > 0)
                {
                    _db.ProjectContracts.RemoveRange(contracts);
                }
                var approvals = _db.ProjectApprovals.Where(w => w.ProjectID == id).ToList();
                if (approvals != null && approvals.Count > 0)
                {
                    _db.ProjectApprovals.RemoveRange(approvals);
                }
                #region ลบข้อมูลทั่วไป
                DBModels.Model.ProjectGeneralInfo proGen = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == id).FirstOrDefault();
                if (proGen != null)
                {
                    _db.ProjectGeneralInfoes.Remove(proGen);
                    _db.SaveChanges();
                }
                #endregion ลบข้อมูลทั่วไป


                result.IsCompleted = true;
                result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                if (ex.InnerException != null)
                {
                    result.Message.Add(ex.InnerException.Message);
                }
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        private void RemoveProjectPersonalFile(DBModels.Model.ProjectPersonel personel)
        {
            if (personel != null)
            {
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String destinationFilePath;

                if (personel.InstructorListFileID2.HasValue)
                {
                    DBModels.Model.MT_Attachment attach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)personel.InstructorListFileID2).FirstOrDefault();
                    if (attach != null)
                    {
                        destinationFilePath = rootDestinationFolderPath + attach.PathName;
                        _db.MT_Attachment.Remove(attach);
                        File.Delete(destinationFilePath);
                    }
                }

                if (personel.ValunteerListFileID7.HasValue)
                {
                    DBModels.Model.MT_Attachment attach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)personel.ValunteerListFileID7).FirstOrDefault();
                    if (attach != null)
                    {
                        destinationFilePath = rootDestinationFolderPath + attach.PathName;
                        _db.MT_Attachment.Remove(attach);
                        File.Delete(destinationFilePath);
                    }
                }

                if (personel.VehicleListFileID6.HasValue)
                {
                    DBModels.Model.MT_Attachment attach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)personel.VehicleListFileID6).FirstOrDefault();
                    if (attach != null)
                    {
                        destinationFilePath = rootDestinationFolderPath + attach.PathName;
                        _db.MT_Attachment.Remove(attach);
                        File.Delete(destinationFilePath);
                    }
                }
            }
        }

        private void RemoveProjectOperationFile(DBModels.Model.MT_Attachment attach)
        {
            if (attach != null)
            {
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String destinationFilePath = rootDestinationFolderPath + attach.PathName;
                _db.MT_Attachment.Remove(attach);
                File.Delete(destinationFilePath);
            }
        }

        private void RemoveProjectDocumentFile(DBModels.Model.ProjectDocument doc)
        {
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String destinationFilePath;


            //Document 1
            DBModels.Model.MT_Attachment attach1 = _db.MT_Attachment.Where(x => x.AttachmentID == doc.DocumentID1).FirstOrDefault();
            if (attach1 != null)
            {
                destinationFilePath = rootDestinationFolderPath + attach1.PathName;
                File.Delete(destinationFilePath);
                _db.MT_Attachment.Remove(attach1);
            }


            //Document 2
            if (doc.DocumentID2.HasValue)
            {
                DBModels.Model.MT_Attachment attach2 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID2).FirstOrDefault();
                if (attach2 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach2.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach2);
                }
            }

            //Document 3
            if (doc.DocumentID3.HasValue)
            {
                DBModels.Model.MT_Attachment attach3 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID3).FirstOrDefault();
                if (attach3 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach3.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach3);
                }
            }

            //Document 4
            if (doc.DocumentID4.HasValue)
            {
                DBModels.Model.MT_Attachment attach4 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID4).FirstOrDefault();
                if (attach4 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach4.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach4);
                }
            }

            //Document 5
            if (doc.DocumentID5.HasValue)
            {
                DBModels.Model.MT_Attachment attach5 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID5).FirstOrDefault();
                if (attach5 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach5.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach5);
                }
            }

            //Document 6
            if (doc.DocumentID6.HasValue)
            {
                DBModels.Model.MT_Attachment attach6 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID6).FirstOrDefault();
                if (attach6 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach6.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach6);
                }
            }

            //Document 7
            if (doc.DocumentID7.HasValue)
            {
                DBModels.Model.MT_Attachment attach7 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID7).FirstOrDefault();
                if (attach7 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach7.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach7);
                }
            }

            //Document 8
            if (doc.DocumentID8.HasValue)
            {
                DBModels.Model.MT_Attachment attach8 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID8).FirstOrDefault();
                if (attach8 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach8.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach8);
                }
            }

            //Document 9
            if (doc.DocumentID9.HasValue)
            {
                DBModels.Model.MT_Attachment attach9 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID9).FirstOrDefault();
                if (attach9 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach9.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach9);
                }
            }

            //Document 10
            if (doc.DocumentID10.HasValue)
            {
                DBModels.Model.MT_Attachment attach10 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID10).FirstOrDefault();
                if (attach10 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach10.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach10);
                }
            }

            //Document 11
            if (doc.DocumentID11.HasValue)
            {
                DBModels.Model.MT_Attachment attach11 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID11).FirstOrDefault();
                if (attach11 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach11.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach11);
                }
            }

            //Document 12
            if (doc.DocumentID12.HasValue)
            {
                DBModels.Model.MT_Attachment attach12 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID12).FirstOrDefault();
                if (attach12 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach12.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach12);
                }
            }

            //Document 13
            if (doc.DocumentID13.HasValue)
            {
                DBModels.Model.MT_Attachment attach13 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID13).FirstOrDefault();
                if (attach13 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach13.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach13);
                }
            }

            //Document 14
            if (doc.DocumentID14.HasValue)
            {
                DBModels.Model.MT_Attachment attach14 = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)doc.DocumentID14).FirstOrDefault();
                if (attach14 != null)
                {
                    destinationFilePath = rootDestinationFolderPath + attach14.PathName;
                    File.Delete(destinationFilePath);
                    _db.MT_Attachment.Remove(attach14);
                }
            }


        }
        #endregion Delete Project


        #region ProjectGeneralInfo

        #region Binding
        public List<ServiceModels.GenericDropDownListData> ListProvince()
        {
            List<ServiceModels.GenericDropDownListData> result = new List<GenericDropDownListData>();
            var q = (from e in _db.MT_Province
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.ProvinceName,
                         Value = e.ProvinceID.ToString()
                     }).ToList();

            result = q;
            return result;
        }

        public List<ServiceModels.DecimalDropDownListData> ListOrganization(decimal? provinceID)
        {
            List<ServiceModels.DecimalDropDownListData> result = new List<DecimalDropDownListData>();
            string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
            decimal? centerProvinceID = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).Select(y => y.ProvinceID).FirstOrDefault();

            if (provinceID.HasValue)
            {
                if (provinceID == centerProvinceID)
                {
                    result = (from e in _db.MT_Organization
                              orderby e.OrganizationNameTH
                              select new ServiceModels.DecimalDropDownListData()
                              {
                                  Text = e.OrganizationNameTH,
                                  Value = e.OrganizationID
                              }).ToList();
                }
                else
                {
                    result = (from e in _db.MT_Organization
                              orderby e.OrganizationNameTH
                              where e.ProvinceID == (decimal)provinceID
                              select new ServiceModels.DecimalDropDownListData()
                              {
                                  Text = e.OrganizationNameTH,
                                  Value = e.OrganizationID
                              }).ToList();
                }

            }
            else
            {
                result = (from e in _db.MT_Organization
                          select new ServiceModels.DecimalDropDownListData()
                          {
                              Text = e.OrganizationNameTH,
                              Value = e.OrganizationID
                          }).ToList();
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> GetOrganizationInfoByID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> result = new ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo>();
            try
            {
                ServiceModels.ProjectInfo.OrganizationInfo data = new ServiceModels.ProjectInfo.OrganizationInfo();
                var q = _db.MT_Organization.Where(x => x.OrganizationID == id).FirstOrDefault();

                data.OrganizationID = q.OrganizationID;
                data.OrganizationNameTH = q.OrganizationNameTH;
                data.OrganizationNameEN = q.OrganizationNameEN;
                data.OrganizationTypeID = q.OrganizationTypeID;
                data.OrganizationTypeEtc = q.OrganizationTypeEtc;
                data.OrgUnderSupport = q.OrgUnderSupport;
                data.OrganizationYear = q.OrganizationYear;
                data.OrgEstablishedDate = q.OrgEstablishedDate;
                data.Address = q.Address;
                data.Building = q.Building;
                data.Moo = q.Moo;
                data.Soi = q.Soi;
                data.Road = q.Road;
                data.SubDistrictID = q.SubDistrictID;
                data.SubDistrict = q.SubDistrict.SubDistrictName;
                data.DistrictID = q.DistrictID;
                data.District = q.District.DistrictName;
                data.AddressProvinceID = q.ProvinceID;
                data.Postcode = q.PostCode;
                data.Telephone = q.Telephone;
                data.Mobile = q.Mobile;
                data.Fax = q.Fax;
                data.Email = q.Email;

                data.Committees = listCommitteeByOrganizationID(id);

                result.Data = data;

                if (data != null)
                {
                    String purpose = _db.ProjectGeneralInfoes.Where(x => x.OrganizationID == id).OrderByDescending(or => or.CreatedDate).Select(y => y.Purpose).FirstOrDefault();
                    data.Purpose = purpose;
                }

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }


        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetOrganizationType()
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                result.Data = (from e in _db.MT_OrganizationType
                               select new ServiceModels.GenericDropDownListData()
                               {
                                   Text = e.OrganizationType,
                                   Value = e.OrganizationTypeID.ToString()
                               }).ToList();

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> GetProjectGeneralInfoByProjectID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> result = new ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo>();
            try
            {
                ServiceModels.ProjectInfo.OrganizationInfo model = new ServiceModels.ProjectInfo.OrganizationInfo();
                Decimal projectId = id;

                var q = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectId).FirstOrDefault();
                if (q != null)
                {
                    model = MappProjectGeneralInfoToOrganizationInfo(q);
                }


                result.Data = model;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        #endregion

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> Create(ServiceModels.ProjectInfo.OrganizationInfo model
            , String projectNameTH, String projectNameEN)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> result = new ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo>();
            try
            {
                DBModels.Model.ProjectGeneralInfo data = new DBModels.Model.ProjectGeneralInfo();

                using (var tran = _db.Database.BeginTransaction())
                {

                    //-- Mapp
                    data = MappOrganizationInfoToDBProjectGeneralInfo(model);

                    Decimal status = GetPrjectApproveStatusID(Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร);
                    data.ProjectApprovalStatusID = status;
                    data.CreatedBy = _user.UserName;
                    data.CreatedByID = (decimal)_user.UserID;
                    data.CreatedDate = DateTime.Now;
                    _db.ProjectGeneralInfoes.Add(data);
                    _db.SaveChanges();

                    Decimal projectID = data.ProjectID;

                    SaveOrganizationCommittee(data.OrganizationID, model.Committees);
                    SaveProjectCommittee(projectID, model.Committees);

                    DBModels.Model.ProjectInformation projectInfo = new DBModels.Model.ProjectInformation();
                    projectInfo.ProjectID = projectID;
                    projectInfo.ProjectNameTH = projectNameTH;
                    projectInfo.ProjectNameEN = projectNameEN;
                    projectInfo.ProjectDate = DateTime.Today;
                    projectInfo.BudgetYear = GetBudgetYear(projectInfo.ProjectDate);
                    projectInfo.CreatedBy = _user.UserName;
                    projectInfo.CreatedByID = (decimal)_user.UserID;
                    projectInfo.CreatedDate = DateTime.Now;
                    _db.ProjectInformations.Add(projectInfo);
                    _db.SaveChanges();

                    model.ProjectID = projectID;
                    result.Data = model;
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);

                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> Update(ServiceModels.ProjectInfo.OrganizationInfo model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> result = new ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo>();
            try
            {
                DBModels.Model.ProjectGeneralInfo data = new DBModels.Model.ProjectGeneralInfo();

                //-- Mapp
                data = MappOrganizationInfoToDBProjectGeneralInfo(model);
                data.UpdatedBy = _user.UserName;
                data.UpdatedByID = _user.UserID;
                data.UpdatedDate = DateTime.Now;
                _db.SaveChanges();

                SaveOrganizationCommittee(data.OrganizationID, model.Committees);
                SaveProjectCommittee(data.ProjectID, model.Committees);

                model.ProjectID = data.ProjectID;
                result.Data = model;
                result.IsCompleted = true;
                result.Message.Add(Resources.Message.SaveSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage DeleteProjectGeneralInfoByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var q = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == id).FirstOrDefault();
                if (q != null)
                {
                    var committees = _db.ProjectCommittees.Where(x => x.ProjectID == id).ToList();
                    if (committees != null)
                    {
                        _db.ProjectCommittees.RemoveRange(committees);
                        _db.SaveChanges();
                    }

                    _db.ProjectGeneralInfoes.Remove(q);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        #region Private method
        private List<ServiceModels.ProjectInfo.Committee> listCommitteeByOrganizationID(Decimal id)
        {
            List<ServiceModels.ProjectInfo.Committee> list = new List<ServiceModels.ProjectInfo.Committee>();
            try
            {
                list = (from e in _db.OrganizationCommittees
                        where e.OrganizationID == id
                        orderby e.OrderNo
                        select new ServiceModels.ProjectInfo.Committee
                        {
                            OrganizationCommitteeID = e.OrganizationCommitteID,
                            OrganizationID = e.OrganizationID,
                            No = e.OrderNo,
                            CommitteePosition = e.CommittePosition,
                            MemberName = e.FirstName,
                            MemberSurname = e.LastName,
                            MemberPosition = e.Position,
                            PositionCode = e.POSCODE,
                            PositionName = e.K_MT_POSITION.POSNAME
                        }).ToList();
                var cm = (from c in list where c.CommitteePosition == "1" select c).FirstOrDefault();
                if (cm != null)
                {
                    cm.CommitteePosition = "2";
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return list;
        }

        private List<ServiceModels.ProjectInfo.Committee> listCommitteeByProjectID(Decimal id)
        {
            List<ServiceModels.ProjectInfo.Committee> list = new List<ServiceModels.ProjectInfo.Committee>();
            try
            {
                list = (from e in _db.ProjectCommittees
                        where e.ProjectID == id
                        orderby e.OrderNo
                        select new ServiceModels.ProjectInfo.Committee
                        {
                            OrganizationCommitteeID = e.ProjectCommitteeID,
                            OrganizationID = e.ProjectID,
                            No = e.OrderNo,
                            CommitteePosition = e.CommitteePosition,
                            MemberName = e.Firstname,
                            MemberSurname = e.LastName,
                            MemberPosition = e.Position,
                            //kenghot  
                            PositionCode = e.POSCODE,
                            PositionName = e.K_MT_POSITION.POSNAME
                        }).ToList();
                var cm = (from c in list where c.CommitteePosition == "1" select c).FirstOrDefault();
                if (cm != null)
                {
                    cm.CommitteePosition = "2";
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return list;
        }

        private List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistanceByDBModelGeneralInfo(DBModels.Model.ProjectGeneralInfo dbModel)
        {
            List<ServiceModels.ProjectInfo.OrganizationAssistance> list = new List<ServiceModels.ProjectInfo.OrganizationAssistance>();
            DBModels.Model.ProjectGeneralInfo model = dbModel;

            ServiceModels.ProjectInfo.OrganizationAssistance obj = new ServiceModels.ProjectInfo.OrganizationAssistance();
            obj.No = "1";
            obj.OrganizationName = dbModel.SourceName1;
            obj.Amount = dbModel.MoneySupport1;
            list.Add(obj);

            obj = new ServiceModels.ProjectInfo.OrganizationAssistance();
            obj.No = "2";
            obj.OrganizationName = dbModel.SourceName2;
            obj.Amount = dbModel.MoneySupport2;
            list.Add(obj);

            obj = new ServiceModels.ProjectInfo.OrganizationAssistance();
            obj.No = "3";
            obj.OrganizationName = dbModel.SourceName3;
            obj.Amount = dbModel.MoneySupport3;
            list.Add(obj);

            obj = new ServiceModels.ProjectInfo.OrganizationAssistance();
            obj.No = "4";
            obj.OrganizationName = dbModel.SourceName4;
            obj.Amount = dbModel.MoneySupport4;
            list.Add(obj);

            return list;
        }

        private Decimal GetPrjectApproveStatusID(string code)
        {
            decimal Id = 0;
            Id = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus && x.LOVCode == code).Select(x => x.LOVID).SingleOrDefault();
            return Id;
        }

        private void SaveProjectCommittee(Decimal id, List<ServiceModels.ProjectInfo.Committee> listCommittee)
        {
            Decimal projectId = id;
            List<DBModels.Model.ProjectCommittee> listDB = new List<DBModels.Model.ProjectCommittee>();
            List<ServiceModels.ProjectInfo.Committee> list = listCommittee;


            List<DBModels.Model.ProjectCommittee> oldCommittee = _db.ProjectCommittees.Where(x => x.ProjectID == id).ToList();
            _db.ProjectCommittees.RemoveRange(oldCommittee);
            Decimal orderNo;
            DBModels.Model.ProjectCommittee dataDB;
            for (int i = 0; i < list.Count; i++)
            {

                orderNo = (list[i].No == null) ? (i + 1) : Convert.ToDecimal(list[i].No);

                dataDB = new DBModels.Model.ProjectCommittee();
                dataDB.ProjectID = id;
                dataDB.CommitteePosition = list[i].CommitteePosition;
                dataDB.OrderNo = orderNo;
                dataDB.Firstname = list[i].MemberName;
                dataDB.LastName = list[i].MemberSurname;
                dataDB.Position = list[i].MemberPosition;
                dataDB.POSCODE = list[i].PositionCode;

                if (dataDB.CommitteePosition == "1")
                {
                    dataDB.CommitteePosition = "2";
                }
                if (dataDB.CommitteePosition == "3")
                {
                    dataDB.Position = "เจ้าหน้าที่";
                }
                else
                {
                    if (string.IsNullOrEmpty(dataDB.Position))
                    {
                        dataDB.Position = " ";
                    }
                }

                listDB.Add(dataDB);


            }

            if (listDB.Count > 0)
            {
                _db.ProjectCommittees.AddRange(listDB);
                _db.SaveChanges();
            }
        }

        private void SaveOrganizationCommittee(Decimal organizationID, List<ServiceModels.ProjectInfo.Committee> listCommittee)
        {

            List<DBModels.Model.OrganizationCommittee> listDB = new List<DBModels.Model.OrganizationCommittee>();
            List<ServiceModels.ProjectInfo.Committee> list = listCommittee;

            List<DBModels.Model.OrganizationCommittee> oldCommittee = _db.OrganizationCommittees.Where(x => x.OrganizationID == organizationID).ToList();
            _db.OrganizationCommittees.RemoveRange(oldCommittee);
            Decimal orderNo;
            DBModels.Model.OrganizationCommittee dataDB;
            for (int i = 0; i < list.Count; i++)
            {

                orderNo = (list[i].No == null) ? (i + 1) : Convert.ToDecimal(list[i].No);

                dataDB = new DBModels.Model.OrganizationCommittee();
                dataDB.OrganizationID = organizationID;
                dataDB.CommittePosition = list[i].CommitteePosition;
                dataDB.OrderNo = orderNo;
                dataDB.FirstName = list[i].MemberName;
                dataDB.LastName = list[i].MemberSurname;
                dataDB.Position = list[i].MemberPosition;
                dataDB.POSCODE = list[i].PositionCode;
                if (dataDB.CommittePosition == "1")
                {
                    dataDB.CommittePosition = "2";
                }
                if (dataDB.CommittePosition == "3")
                {
                    dataDB.Position = "เจ้าหน้าที่";
                }
                else
                {
                    if (string.IsNullOrEmpty(dataDB.Position))
                    {
                        dataDB.Position = " ";
                    }
                }

                listDB.Add(dataDB);



            }

            if (listDB.Count > 0)
            {
                _db.OrganizationCommittees.AddRange(listDB);
                _db.SaveChanges();
            }
        }
        #endregion

        #endregion

        #region ProjectInformation

        public List<ServiceModels.GenericDropDownListData> ListProjectInfoType()
        {
            List<ServiceModels.GenericDropDownListData> result = new List<GenericDropDownListData>();
            var q = (from e in _db.MT_ListOfValue
                     where e.LOVGroup == Common.LOVGroup.ProjectType
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.LOVName,
                         Value = e.LOVID.ToString()
                     }).ToList();

            result = q;
            return result;
        }

        public List<ServiceModels.GenericDropDownListData> ListDisabilityType()
        {
            List<ServiceModels.GenericDropDownListData> result = new List<GenericDropDownListData>();
            var q = (from e in _db.MT_ListOfValue
                     where e.LOVGroup == Common.LOVGroup.DisabilityType
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.LOVName,
                         Value = e.LOVID.ToString(),
                         OrderNo = e.OrderNo
                     }).OrderBy(or => or.OrderNo).ToList();

            result = q;
            return result;
        }

        public List<ServiceModels.GenericDropDownListData> ListProjectTarget()
        {
            List<ServiceModels.GenericDropDownListData> result = new List<GenericDropDownListData>();
            var q = (from e in _db.MT_ListOfValue
                     where e.LOVGroup == Common.LOVGroup.TargetGroup
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.LOVName,
                         Value = e.LOVID.ToString()
                     }).ToList();

            result = q;
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> GetProjectInformationByProjecctID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> result = new ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo>();
            try
            {
                ServiceModels.ProjectInfo.TabProjectInfo model = new ServiceModels.ProjectInfo.TabProjectInfo();
                Decimal projectId = id;

                var q = _db.ProjectInformations.Where(x => x.ProjectID == projectId).FirstOrDefault();

                if (q != null)
                    model = MappDBProjectInformationToTabProjectInfo(q);

                result.Data = model;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTarget> GetProjectTargetByProjectID(Decimal id)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTarget> result = new ReturnQueryData<ServiceModels.ProjectInfo.ProjectTarget>();
            try
            {
                decimal projectId = id;
                List<ServiceModels.ProjectInfo.ProjectTarget> list = _db.ProjectTargetGroups.Where(x => x.ProjectID == projectId)
                    .Select(y => new ServiceModels.ProjectInfo.ProjectTarget
                    {
                        ProjectID = y.ProjectID,
                        TargetID = y.TargetGroupID,
                        ProjectTargetID = y.ProjectTargetGroupID,
                        Amount = y.TargetGroupAmt,
                        TargetName = _db.MT_ListOfValue.Where(t => t.LOVID == y.TargetGroupID).Select(tgn => tgn.LOVName).FirstOrDefault(),
                        TargetOtherName = y.TargetGroupEtc
                    }).ToList();

                if (list != null && (list.Count > 0))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].UID = Guid.NewGuid().ToString();
                    }
                }
                else
                {
                    list = new List<ServiceModels.ProjectInfo.ProjectTarget>();
                }

                result.IsCompleted = true;
                result.Data = list; //MappDBProjectTargetGroupToProjectTarget(listDB);


            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabAttachment> GetProjectAttachment(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabAttachment> result = new ReturnObject<ServiceModels.ProjectInfo.TabAttachment>();
            try
            {
                ServiceModels.ProjectInfo.TabAttachment tabAttch = new ServiceModels.ProjectInfo.TabAttachment();

                var committee = _db.ProjectCommittees.Where(x => (x.ProjectID == projectID) && (x.CommitteePosition == "1")).FirstOrDefault();
                if (committee != null)
                {
                    tabAttch.ProposerProject = String.Format("{0} {1}", committee.Firstname, committee.LastName);
                }

                DBModels.Model.ProjectPersonel personel = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();
                if (personel != null)
                {
                    tabAttch.ResponsibleProject = String.Format("{0}{1} {2}", personel.Prefix1Personel.LOVName, personel.Firstname1, personel.Lastname1);
                }

                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
                if (genInfo != null)
                {
                    tabAttch.ProjectID = genInfo.ProjectID;
                    tabAttch.OrganizationID = genInfo.OrganizationID;
                    tabAttch.ApprovalStatus = (genInfo.ProjectApproval != null) ? genInfo.ProjectApproval.ApprovalStatus : null;
                    tabAttch.ProjectApprovalStatusID = genInfo.ProjectApprovalStatus.LOVID;
                    tabAttch.ProjectApprovalStatusCode = genInfo.ProjectApprovalStatus.LOVCode;
                    tabAttch.ProvinceID = genInfo.ProvinceID;
                    tabAttch.CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == genInfo.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();

                }

                tabAttch.Attachments = GetProjectAttachmentProvide(projectID);

                if (tabAttch.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                {
                    tabAttch.RequiredSubmitData = GetKeyRequiredSubmitData(projectID);
                }

                result.IsCompleted = true;
                result.Data = tabAttch;

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        /// <summary>
        /// kenghot
        /// </summary>
        private void MigrateTabAttachDataToNewStructure(DBModels.Model.ProjectDocument pd)
        {
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID1", pd.DocumentID1);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID2", pd.DocumentID2);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID3", pd.DocumentID3);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID4", pd.DocumentID4);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID5", pd.DocumentID5);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID6", pd.DocumentID6);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID7", pd.DocumentID7);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID8", pd.DocumentID8);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID9", pd.DocumentID9);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID10", pd.DocumentID10);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID11", pd.DocumentID11);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID12", pd.DocumentID12);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID13", pd.DocumentID13);
            AddPojDocToFileInTable(pd.ProjectID, "DOCUMENTID14", pd.DocumentID14);
            if (_db.K_FILEINTABLE.Count() > 0)
            {
                pd.DocumentID1 = pd.DocumentID2 = pd.DocumentID3 = pd.DocumentID4 = pd.DocumentID5 = pd.DocumentID6 = pd.DocumentID7 =
                pd.DocumentID8 = pd.DocumentID9 = pd.DocumentID10 = pd.DocumentID11 = pd.DocumentID12 = pd.DocumentID13 = pd.DocumentID14 = null;
                _db.SaveChanges();
            }

        }
        private void AddPojDocToFileInTable(decimal projID, string FieldName, decimal? AttachID)
        {
            if (AttachID.HasValue)
            {
                var att = _db.MT_Attachment.Where(w => w.AttachmentID == AttachID.Value).FirstOrDefault();
                if (att != null)
                {
                    _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                    {
                        ATTACHMENTID = AttachID.Value,
                        FIELDNAME = FieldName,
                        TABLENAME = TABLE_PROJECTDOCUMENT,
                        TABLEROWID = projID,
                        MT_ATTACHMENT = att

                    });
                }



            }
        }
        /// <summary>
        /// kenghot
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        private List<ServiceModels.ProjectInfo.AttachmentProvide> GetProjectAttachmentProvide(decimal projectID)
        {
            List<ServiceModels.ProjectInfo.AttachmentProvide> list = new List<ServiceModels.ProjectInfo.AttachmentProvide>();
            ServiceModels.ProjectInfo.AttachmentProvide attach;
            int no;
            list = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectAttachment)
                .Select(x => new ServiceModels.ProjectInfo.AttachmentProvide
                {
                    ProjectID = projectID,
                    DocumentNo = x.OrderNo,
                    DocumentProvideName = x.LOVName
                }).OrderBy(or => or.DocumentNo).ToList();

            DBModels.Model.ProjectDocument projDoc = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).FirstOrDefault();

            //DBModels.Model.MT_Attachment dbAttach;
            if (projDoc != null)
            {
                //kenghot
                MigrateTabAttachDataToNewStructure(projDoc);
                var attSer = new AttachmentService(_db);
                List<ServiceModels.KendoAttachment> files = attSer.GetAttachmentOfTable(TABLE_PROJECTDOCUMENT, "", projectID);
                string colName = "";
                //end kenghot
                if (files.Count() > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        attach = list[i];
                        colName = "DOCUMENTID" + (i + 1).ToString().Trim();
                        var tmpAtt = files.Where(w => w.fieldName == colName).ToList();
                        if (tmpAtt.Count() > 0)
                        {
                            attach.AttachFiles = new List<KendoAttachment>();
                            attach.AttachFiles.AddRange(tmpAtt);
                        }

                    }
                }

            }

            return list;
        }
        //private List<ServiceModels.ProjectInfo.AttachmentProvide> GetProjectAttachmentProvide(decimal projectID)
        //{
        //    List<ServiceModels.ProjectInfo.AttachmentProvide> list = new List<ServiceModels.ProjectInfo.AttachmentProvide>();

        //    ServiceModels.ProjectInfo.AttachmentProvide attach;
        //    int no;
        //    list = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectAttachment)
        //        .Select(x => new ServiceModels.ProjectInfo.AttachmentProvide
        //        {
        //            ProjectID = projectID,
        //            DocumentNo = x.OrderNo,
        //            DocumentProvideName = x.LOVName
        //        }).OrderBy(or => or.DocumentNo).ToList();

        //    DBModels.Model.ProjectDocument projDoc = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).FirstOrDefault();
        //    DBModels.Model.MT_Attachment dbAttach;
        //    if (projDoc != null)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            attach = list[i];
        //            no = (int)attach.DocumentNo;
        //            dbAttach = null;
        //            switch (no)
        //            {
        //                case 1:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment1;
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment2;
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment3;
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment4;
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment5;
        //                        break;
        //                    }
        //                case 6:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment6;
        //                        break;
        //                    }
        //                case 7:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment7;
        //                        break;
        //                    }
        //                case 8:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment8;
        //                        break;
        //                    }
        //                case 9:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment9;
        //                        break;
        //                    }
        //                case 10:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment10;
        //                        break;
        //                    }
        //                case 11:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment11;
        //                        break;
        //                    }
        //                case 12:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment12;
        //                        break;
        //                    }
        //                case 13:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment13;
        //                        break;
        //                    }
        //                case 14:
        //                    {
        //                        dbAttach = projDoc.MT_Attachment14;
        //                        break;
        //                    }
        //            }

        //            if (dbAttach != null)
        //            {
        //                attach.AttachmentID = dbAttach.AttachmentID;
        //                attach.AttachmentFileName = dbAttach.AttachmentFilename;
        //                attach.AttachmentPathName = dbAttach.PathName;
        //                attach.AttachmentFileSize = dbAttach.FileSize;
        //            }
        //        }
        //    }

        //    return list;
        //}
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> SaveProjectInformation(ServiceModels.ProjectInfo.TabProjectInfo model
            , List<ServiceModels.ProjectInfo.ProjectTarget> targetList)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> result = new ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo>();
            try
            {
                ServiceModels.ProjectInfo.TabProjectInfo dataServiceModel = model;
                DBModels.Model.ProjectInformation dataDBModel = MappTabProjectInfoToDBProjectInformation(dataServiceModel);
                bool hasData = _db.ProjectInformations.Where(x => x.ProjectID == model.ProjectID).Count() > 0;

                using (var tran = _db.Database.BeginTransaction())
                {
                    if (hasData) // for update
                    {
                        dataDBModel.UpdatedDate = DateTime.Now;
                        dataDBModel.UpdatedBy = _user.UserName;
                        dataDBModel.UpdatedByID = _user.UserID;
                        _db.SaveChanges();
                    }
                    else // for create
                    {
                        dataDBModel.CreatedDate = DateTime.Now;
                        dataDBModel.CreatedBy = _user.UserName;
                        dataDBModel.CreatedByID = (decimal)_user.UserID;

                        _db.ProjectInformations.Add(dataDBModel);
                        _db.SaveChanges();
                    }
                    if (targetList != null)
                    {
                        SaveProjectTargetGroup(model.ProjectID, targetList);
                    }


                    result.Data = MappDBProjectInformationToTabProjectInfo(dataDBModel);
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);

                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage DeleteProjectInfoByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var q = _db.ProjectInformations.Where(x => x.ProjectID == id).FirstOrDefault();
                if (q != null)
                {
                    var targetGroup = _db.ProjectTargetGroups.Where(x => x.ProjectID == id).ToList();
                    if (targetGroup != null)
                    {
                        _db.ProjectTargetGroups.RemoveRange(targetGroup);
                        _db.SaveChanges();
                    }

                    _db.ProjectInformations.Remove(q);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private void SaveProjectTargetGroup(Decimal id, List<ServiceModels.ProjectInfo.ProjectTarget> listTarget)
        {
            Decimal projectId = id;
            List<ServiceModels.ProjectInfo.ProjectTarget> list = listTarget;
            List<DBModels.Model.ProjectTargetGroup> listTargetDb = _db.ProjectTargetGroups.Where(x => x.ProjectID == projectId).ToList();
            List<DBModels.Model.ProjectTargetGroup> listCreate = new List<DBModels.Model.ProjectTargetGroup>();

            _db.ProjectTargetGroups.RemoveRange(listTargetDb);


            for (int i = 0; i < list.Count; i++)
            {
                DBModels.Model.ProjectTargetGroup itemDB = new DBModels.Model.ProjectTargetGroup();
                itemDB.ProjectID = projectId;
                itemDB.TargetGroupID = list[i].TargetID;
                itemDB.TargetGroupEtc = list[i].TargetOtherName;
                itemDB.TargetGroupAmt = list[i].Amount.Value;
                listCreate.Add(itemDB);
            }
            _db.ProjectTargetGroups.AddRange(listCreate);
            _db.SaveChanges();

        }

        //private void SaveProjectTargetGroup(Decimal id,List<ServiceModels.ProjectInfo.ProjectTarget> listTarget)
        //{
        //    Decimal projectId = id;
        //    List<ServiceModels.ProjectInfo.ProjectTarget> list = listTarget;
        //    List<DBModels.Model.ProjectTargetGroup> listTargetDb = _db.ProjectTargetGroups.Where(x => x.ProjectID == projectId).ToList();
        //    List<DBModels.Model.ProjectTargetGroup> listCreate = new List<DBModels.Model.ProjectTargetGroup>();

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (list[i].ProjectTargetID == 0) //Create
        //        {
        //            DBModels.Model.ProjectTargetGroup itemDB = new DBModels.Model.ProjectTargetGroup();
        //            itemDB.ProjectID = projectId;
        //            itemDB.TargetGroupID = list[i].TargetID;
        //            itemDB.TargetGroupEtc = list[i].TargetOtherName;
        //            itemDB.Male = list[i].MenAmount;
        //            itemDB.Female = list[i].WomenAmount;
        //            itemDB.TargetGroupAmt = list[i].Amount.Value;                    
        //            listCreate.Add(itemDB);
        //        }
        //        else
        //        {
        //            decimal projectTargetGroupId = list[i].ProjectTargetID;
        //            DBModels.Model.ProjectTargetGroup objDb = listTargetDb.Where(x => x.ProjectTargetGroupID == projectTargetGroupId).FirstOrDefault();
        //            if (objDb != null)
        //            {
        //                objDb.TargetGroupID = list[i].TargetID;
        //                objDb.TargetGroupEtc = list[i].TargetOtherName;
        //                objDb.Male = list[i].MenAmount;
        //                objDb.Female = list[i].WomenAmount;
        //                objDb.TargetGroupAmt = list[i].Amount.Value;
        //                _db.SaveChanges();

        //                listTargetDb.Remove(objDb);
        //            }
        //        }
        //    }

        //    if (listCreate.Count > 0)
        //    {
        //        _db.ProjectTargetGroups.AddRange(listCreate);
        //        _db.SaveChanges();
        //    }

        //    if (listTargetDb.Count > 0)
        //    {
        //        _db.ProjectTargetGroups.RemoveRange(listTargetDb);
        //        _db.SaveChanges();
        //    }
        //}
        #endregion

        #region ProjectPersonal

        public List<ServiceModels.GenericDropDownListData> ListPrefix()
        {
            List<ServiceModels.GenericDropDownListData> result = new List<GenericDropDownListData>();
            var q = (from e in _db.MT_ListOfValue
                     where e.LOVGroup == "Prefix"
                     orderby e.OrderNo
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.LOVName,
                         Value = e.LOVID.ToString()
                     }).ToList();

            result = q;
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> GetProjectPersonalByProjectID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> result = new ReturnObject<ServiceModels.ProjectInfo.TabPersonal>();
            try
            {
                ServiceModels.ProjectInfo.TabPersonal model = null;
                Decimal projectId = id;

                var q = _db.ProjectPersonels.Where(x => x.ProjectID == projectId).FirstOrDefault();

                if (q == null)
                {
                    q = new DBModels.Model.ProjectPersonel();
                    q.ProjectID = id;


                }
                else
                {
                    //kengot
                    // convert attchfile to new structure
                    if (q.InstructorListFileID2.HasValue)
                    {

                        _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                        {
                            ATTACHMENTID = q.InstructorListFileID2.Value,
                            FIELDNAME = PERSON_INSTRUCTOR,
                            TABLENAME = TABLE_PROJECTPERSONEL,
                            TABLEROWID = id
                        });
                        q.InstructorListFileID2 = null;
                    }
                    if (q.ValunteerListFileID7.HasValue)
                    {

                        _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                        {
                            ATTACHMENTID = q.ValunteerListFileID7.Value,
                            FIELDNAME = PERSON_VALUNTEER,
                            TABLENAME = TABLE_PROJECTPERSONEL,
                            TABLEROWID = id
                        });
                        q.ValunteerListFileID7 = null;
                    }
                    if (q.VehicleListFileID6.HasValue)
                    {

                        _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                        {
                            ATTACHMENTID = q.VehicleListFileID6.Value,
                            FIELDNAME = PERSON_VEHICLE,
                            TABLENAME = TABLE_PROJECTPERSONEL,
                            TABLEROWID = id
                        });
                        q.VehicleListFileID6 = null;
                    }
                    _db.SaveChanges();
                    //end kenghot
                }
                model = MappDBProjectPersonalToTabPersonal(q);

                result.Data = model;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> SaveProjectPersonal(ServiceModels.ProjectInfo.TabPersonal model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> result = new ReturnObject<ServiceModels.ProjectInfo.TabPersonal>();
            try
            {
                List<decimal> districtIds = new List<decimal>();
                List<decimal> subDistrictIds = new List<decimal>();
                //save draft
                if (model.Address1.DistrictID1 != null)
                    districtIds.Add((decimal)model.Address1.DistrictID1);
                //save draft
                if (model.Address1.SubDistrictID1 != null)
                    subDistrictIds.Add((decimal)model.Address1.SubDistrictID1);
                //save draft
                if (model.Address2.DistrictID2 != null)
                    districtIds.Add((decimal)model.Address2.DistrictID2);
                //save draft
                if (model.Address2.SubDistrictID2 != null)
                    subDistrictIds.Add((decimal)model.Address2.SubDistrictID2);
                if ((model.Address3 != null) && (model.Address3.DistrictID3.HasValue))
                {
                    districtIds.Add((decimal)model.Address3.DistrictID3);
                    subDistrictIds.Add((decimal)model.Address3.SubDistrictID3);
                }

                List<DBModels.Model.MT_District> districtList = _db.MT_District.Where(x => districtIds.Contains(x.DistrictID)).ToList();
                List<DBModels.Model.MT_SubDistrict> subDistrictList = _db.MT_SubDistrict.Where(x => subDistrictIds.Contains(x.SubDistrictID)).ToList();

                ServiceModels.ProjectInfo.TabPersonal dataServiceModel = model;
                DBModels.Model.ProjectPersonel dataDBModel = new DBModels.Model.ProjectPersonel();

                using (var tran = _db.Database.BeginTransaction())
                {

                    dataDBModel = MappTabProjectPersonalToDBProjectPersonal(dataServiceModel, districtList, subDistrictList);




                    if (dataDBModel.ProjectID > 0) // for update
                    {
                        dataDBModel.UpdatedDate = DateTime.Now;
                        dataDBModel.UpdatedBy = _user.UserName;
                        dataDBModel.UpdatedByID = _user.UserID;
                        _db.SaveChanges();
                    }
                    else // for create
                    {
                        dataDBModel.ProjectID = model.ProjectID;
                        dataDBModel.CreatedDate = DateTime.Now;
                        dataDBModel.CreatedBy = _user.UserName;
                        dataDBModel.CreatedByID = (decimal)_user.UserID;

                        _db.ProjectPersonels.Add(dataDBModel);
                        _db.SaveChanges();
                    }

                    tran.Commit();
                }

                dataDBModel = _db.ProjectPersonels.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                result.Data = MappDBProjectPersonalToTabPersonal(dataDBModel);
                result.IsCompleted = true;
                result.Message.Add(Resources.Message.SaveSuccess);


            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage DeleteProjectPersonalByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var q = _db.ProjectPersonels.Where(x => x.ProjectID == id).FirstOrDefault();
                if (q != null)
                {
                    _db.ProjectPersonels.Remove(q);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        #endregion

        #region ProjectOperation

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> GetProjectOperationByProjectID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> result = new ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan>();
            try
            {

                Decimal projectId = id;

                var data = (from pro in _db.ProjectGeneralInfoes
                            where pro.ProjectID == id
                            select new ServiceModels.ProjectInfo.TabProcessingPlan
                            {
                                ProjectID = pro.ProjectID,
                                OrganizationID = pro.OrganizationID,
                                ApprovalStatus = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatus : null,
                                ProjectApprovalStatusID = pro.ProjectApprovalStatusID,
                                ProjectApprovalStatusCode = pro.ProjectApprovalStatus.LOVCode,
                                ProjectApprovalStatusName = pro.ProjectApprovalStatus.LOVName,

                                Address = (pro.ProjectOperation != null) ? pro.ProjectOperation.Address : null,
                                Building = (pro.ProjectOperation != null) ? pro.ProjectOperation.Building : null,
                                Moo = (pro.ProjectOperation != null) ? pro.ProjectOperation.Moo : null,
                                Soi = (pro.ProjectOperation != null) ? pro.ProjectOperation.Soi : null,
                                Road = (pro.ProjectOperation != null) ? pro.ProjectOperation.Road : null,
                                SubDistrictID = (pro.ProjectOperation != null) ? pro.ProjectOperation.SubDistrictID : (decimal?)null,
                                SubDistrict = (pro.ProjectOperation != null) ? pro.ProjectOperation.SubDistrict : null,
                                DistrictID = (pro.ProjectOperation != null) ? pro.ProjectOperation.DistrictID : (decimal?)null,
                                District = (pro.ProjectOperation != null) ? pro.ProjectOperation.District : null,
                                ProvinceID = (pro.ProjectOperation != null) ? pro.ProjectOperation.ProvinceID : (decimal?)null,
                                LocationMapID = (pro.ProjectOperation != null) ? pro.ProjectOperation.LocationMapID : (decimal?)null,
                                StartDate = (pro.ProjectOperation != null) ? pro.ProjectOperation.StartDate : (DateTime?)null,
                                EndDate = (pro.ProjectOperation != null) ? pro.ProjectOperation.EndDate : (DateTime?)null,
                                TotalDay = (pro.ProjectOperation != null) ? pro.ProjectOperation.TotalDay : (decimal?)null,
                                TimeDesc = (pro.ProjectOperation != null) ? pro.ProjectOperation.TimeDesc : null,
                                Method = (pro.ProjectOperation != null) ? pro.ProjectOperation.Method : null,
                                CreatorOrganizationID = _db.SC_User.Where(users => (users.UserID == pro.CreatedByID) && (users.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault(),
                                ProjectProvinceID = pro.ProvinceID,
                                OrgProjectProvinceID = _db.MT_Organization.Where(org => org.OrganizationID == pro.OrganizationID).Select(orgr => orgr.ProvinceID).FirstOrDefault()
                            }).FirstOrDefault();

                if (data != null)
                {

                    data.ProjectOperationAddresses = MappProjectOperationAddress(projectId);


                    if (data.LocationMapID.HasValue)
                    {
                        DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.LocationMapID).FirstOrDefault();
                        if (dbAttach != null)
                        {
                            ServiceModels.KendoAttachment file = new KendoAttachment()
                            {
                                id = dbAttach.AttachmentID.ToString(),
                                name = dbAttach.AttachmentFilename,
                                extension = Path.GetExtension(dbAttach.AttachmentFilename),
                                size = (int)dbAttach.FileSize
                            };

                            data.LocationMapAttachment = file;
                        }
                    }


                    if (data.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                    {
                        data.RequiredSubmitData = GetKeyRequiredSubmitData(data.ProjectID);
                    }

                }

                result.Data = data;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        private void CopyProcessed(ServiceModels.ProjectInfo.ProjectProcessed model, DBModels.Model.PROJECTPROCESSED p)
        {
            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
            decimal? locationMapID;
            decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_OPERATION);
            p.ADDRESS = model.Address;
            p.BUILDING = model.Building;
            p.DESCRIPTION = model.Description;
            p.DISTRICT = model.District;
            p.DISTRICTID = model.DistrictID;
            p.MOO = model.Moo;
            p.PROCESSEND = model.ProcessEnd.Value.Date;
            p.PROCESSSTART = model.ProcessStart.Value.Date;
            p.PROJECTID = model.ProjectID;
            p.PROVINCEID = model.ProvinceID;
            p.ROAD = model.Road;
            p.SOI = model.Soi;
            p.SUBDISTRICT = model.SubDistrict;
            p.SUBDISTRICTID = model.SubDistrictID;
            //Attachment 
            if ((model.RemovedLocationMapAttachment != null) && (!String.IsNullOrEmpty(model.RemovedLocationMapAttachment.id)))
            {
                p.LOCATIONMAPID = (decimal?)null;
                RemoveFile(model.RemovedLocationMapAttachment, rootDestinationFolderPath);
            }

            if ((model.AddedLocationMapAttachment != null) && (String.IsNullOrEmpty(model.AddedLocationMapAttachment.id)))
            {
                locationMapID = SaveFile(model.AddedLocationMapAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                p.LOCATIONMAPID = locationMapID;
            }

        }
        public ServiceModels.ReturnObject<bool> SaveProjectProcessed(decimal projID, List<ServiceModels.ProjectInfo.ProjectProcessed> data, string ipAddress)
        {
            ServiceModels.ReturnObject<bool> result = new ReturnObject<bool>();
            try
            {
                var pc = from p in _db.PROJECTPROCESSEDs where p.PROJECTID == projID select p;

                foreach (DBModels.Model.PROJECTPROCESSED ptmp in pc)
                {
                    var search = (from s in data where s.ProcessID == ptmp.PROCESSID select s).FirstOrDefault();
                    if (search != null)
                    {
                        CopyProcessed(search, ptmp);
                    }
                    else
                    {
                        _db.PROJECTPROCESSEDs.Remove(ptmp);
                    }
                }
                var add = data.Where(w => w.ProcessID < 0).ToList();
                foreach (ProjectProcessed a in add)
                {
                    var row = new DBModels.Model.PROJECTPROCESSED();
                    CopyProcessed(a, row);

                    _db.PROJECTPROCESSEDs.Add(row);
                }
                _db.PROJECTHISTORies.Add(CreateRowProjectHistory(projID, "5", _user.UserID.Value, ipAddress));
                _db.SaveChanges();
                result.Data = true;
                result.IsCompleted = true;
                result.Message.Add("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> SaveProjectOperation(ServiceModels.ProjectInfo.TabProcessingPlan model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> result = new ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan>();

            try
            {
                ServiceModels.ProjectInfo.TabProcessingPlan dataServiceModel = model;

                using (var tran = _db.Database.BeginTransaction())
                {

                    DBModels.Model.ProjectOperation dataDBModel = MappTabProcessingPlanToDBProjectOperation(dataServiceModel);

                    if (dataDBModel.ProjectID > 0) // for update
                    {

                        dataDBModel.UpdatedDate = DateTime.Now;
                        dataDBModel.UpdatedBy = _user.UserName;
                        dataDBModel.UpdatedByID = _user.UserID;
                        _db.SaveChanges();

                    }
                    else // for create
                    {
                        dataDBModel.ProjectID = model.ProjectID;
                        dataDBModel.CreatedDate = DateTime.Now;
                        dataDBModel.CreatedBy = _user.UserName;
                        dataDBModel.CreatedByID = (decimal)_user.UserID;

                        _db.ProjectOperations.Add(dataDBModel);
                        _db.SaveChanges();

                    }

                    SaveProjectOperationAddress(dataDBModel.ProjectID, dataServiceModel.ProjectOperationAddresses);

                    result.Data = MappDBProjectOperationToTabProcessingPlan(dataDBModel);
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private void SaveProjectOperationAddress(decimal projectId, List<ServiceModels.ProjectInfo.ProjectOperationAddress> addresses)
        {
            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + projectId + "\\";
            decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_OPERATION);
            ServiceModels.KendoAttachment removeAttachment;
            decimal? removeAttachmentID;
            List<DBModels.Model.ProjectOperationAddress> old = _db.ProjectOperationAddresses.Where(x => x.ProjectID == projectId).ToList();
            decimal? locationMapID;
            if (old != null)
            {
                ServiceModels.ProjectInfo.ProjectOperationAddress model;
                DBModels.Model.ProjectOperationAddress dbModel;
                decimal operationAddressID;

                foreach (DBModels.Model.ProjectOperationAddress item in old)
                {
                    operationAddressID = item.OperationAddressID;
                    model = addresses.Where(x => x.OperationAddressID == operationAddressID).FirstOrDefault();
                    if (model != null)
                    {
                        item.Address = model.Address;
                        item.Moo = model.Moo;
                        item.Building = model.Building;
                        item.Soi = model.Soi;
                        item.Road = model.Road;
                        item.SubDistrictID = model.SubDistrictID;
                        item.SubDistrict = model.SubDistrict;
                        item.District = model.District;
                        item.DistrictID = model.DistrictID;
                        item.ProvinceID = model.ProvinceID;


                        if ((model.RemovedLocationMapAttachment != null) && (!String.IsNullOrEmpty(model.RemovedLocationMapAttachment.id)))
                        {
                            item.LocationMapID = (decimal?)null;
                            RemoveFile(model.RemovedLocationMapAttachment, rootDestinationFolderPath);
                        }

                        if ((model.AddedLocationMapAttachment != null) && (String.IsNullOrEmpty(model.AddedLocationMapAttachment.id)))
                        {
                            locationMapID = SaveFile(model.AddedLocationMapAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            item.LocationMapID = locationMapID;
                        }

                        _db.SaveChanges();

                        addresses.Remove(model);
                    }
                    else
                    {
                        removeAttachmentID = item.LocationMapID;

                        _db.ProjectOperationAddresses.Remove(item);

                        if (removeAttachmentID.HasValue)
                        {
                            removeAttachment = new KendoAttachment
                            {
                                id = removeAttachmentID.ToString()
                            };
                            RemoveFile(removeAttachment, rootDestinationFolderPath);
                        }

                        _db.SaveChanges();
                    }
                }


                if (addresses != null && (addresses.Count > 0))
                {
                    List<DBModels.Model.ProjectOperationAddress> newList = new List<DBModels.Model.ProjectOperationAddress>();
                    for (int i = 0; i < addresses.Count; i++)
                    {
                        model = addresses[i];
                        locationMapID = SaveFile(model.AddedLocationMapAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);

                        dbModel = new DBModels.Model.ProjectOperationAddress
                        {
                            ProjectID = model.ProjectID,
                            Address = model.Address,
                            Moo = model.Moo,
                            Building = model.Building,
                            Soi = model.Soi,
                            Road = model.Road,
                            SubDistrict = model.SubDistrict,
                            SubDistrictID = model.SubDistrictID,
                            District = model.District,
                            DistrictID = model.DistrictID,
                            ProvinceID = model.ProvinceID,
                            LocationMapID = locationMapID
                        };
                        newList.Add(dbModel);
                    }

                    _db.ProjectOperationAddresses.AddRange(newList);
                    _db.SaveChanges();
                }
            }
        }

        public ServiceModels.ReturnMessage DeleteProjectOperationByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var q = _db.ProjectOperations.Where(x => x.ProjectID == id).FirstOrDefault();
                if (q != null)
                {
                    _db.ProjectOperations.Remove(q);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        #endregion

        #region ProjectBudget
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> GetProjectBudgetInfoByProjectID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>();
            try
            {


                ServiceModels.ProjectInfo.ProjectBudget model = null;
                decimal projectId = id;
                var q = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectId).FirstOrDefault();
                if (q != null)
                {
                    model = new ServiceModels.ProjectInfo.ProjectBudget();
                    model.ProjectID = projectId;
                    model.OrganizationID = q.OrganizationID;
                    model.TotalRequestAmount = q.BudgetValue;
                    model.TotalReviseAmount = q.BudgetReviseValue;
                    model.IsBudgetGotSupport = (q.BudgetFromOtherFlag == "1") ? true : ((q.BudgetFromOtherFlag == "0") ? false : (bool?)null);
                    model.BudgetGotSupportName = q.BudgetFromOtherName;
                    model.BudgetGotSupportAmount = q.BudgetFromOtherAmount;
                    //kenghot
                    model.BudgetDetails = MappDBProjectBudgetToListProjectBudgetDetail(q.ProjectBudgets.OrderBy(pb => pb.ProjectBudgetID).ToList());
                    // model.BudgetDetails = MappDBProjectBudgetToListProjectBudgetDetail(q.ProjectBudgets.OrderBy(pb => pb.ProjectBudgetID).OrderByDescending(o=> o.BudgetValue ).ToList());

                    model.ProjectApprovalStatusID = q.ProjectApprovalStatusID;
                    model.ProjectApprovalStatusCode = q.ProjectApprovalStatus.LOVCode;
                    model.ProjectApprovalStatusName = q.ProjectApprovalStatus.LOVName;

                    model.ApprovalStatus = (q.ProjectApproval != null) ? q.ProjectApproval.ApprovalStatus : null;

                    model.ProvinceID = q.ProvinceID;
                    model.CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == q.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();
                    model.BudgetActivities = (from a in _db.PROJECTBUDGETACTIVITies
                                              where a.PROJECTID == projectId
                                              select new Project.ServiceModels.ProjectInfo.BudgetActivity
                                              {
                                                  ActivityDESC = a.ACTIVITYDESC,
                                                  ActivityID = a.ACTIVITYID,
                                                  ActivityName = a.ACTIVITYNAME,
                                                  ProjectID = a.PROJECTID,
                                                  RunNo = a.RUNNO.Value,
                                                  TotalAmount = a.TOTALAMOUNT,
                                                  CreateDate = a.CREATEDATE
                                              }).ToList();
                    if (model.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                    {
                        model.RequiredSubmitData = GetKeyRequiredSubmitData(model.ProjectID);
                    }


                    string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                    string provAbbr = _db.MT_Province.Where(x => x.ProvinceID == model.ProvinceID).Select(y => y.ProvinceAbbr).FirstOrDefault();
                    model.IsRequestCenter = (provAbbr == centerAbbr);
                }

                result.Data = model;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> SaveProjectBudget(ServiceModels.ProjectInfo.ProjectBudget model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>();
            try
            {
                decimal projectId = Convert.ToDecimal(model.ProjectID);
                using (var tran = _db.Database.BeginTransaction())
                {
                    ServiceModels.ProjectInfo.ProjectBudget dataServiceModel = model;



                    //save activity
                    //var act = (from a in _db.PROJECTBUDGETACTIVITies where a.PROJECTID == projectId select a).ToList();
                    var act = _db.PROJECTBUDGETACTIVITies.Where(w => w.PROJECTID == projectId).ToList();
                    foreach (ServiceModels.ProjectInfo.BudgetActivity b in model.BudgetActivities)
                    {
                        DBModels.Model.PROJECTBUDGETACTIVITY actRow = null;
                        if (b.ActivityID.HasValue)
                        {
                            var edit = (from e in act where e.ACTIVITYID == b.ActivityID select e).FirstOrDefault();
                            if (edit != null)
                            {
                                actRow = edit;
                            }
                        }
                        else
                        {
                            actRow = new PROJECTBUDGETACTIVITY();
                            actRow.CREATEBYID = _user.UserID;
                            actRow.CREATEDATE = DateTime.Now;
                            _db.PROJECTBUDGETACTIVITies.Add(actRow);
                        }
                        actRow.ACTIVITYNAME = b.ActivityName;
                        actRow.ACTIVITYDESC = b.ActivityDESC;
                        actRow.PROJECTID = projectId;
                        actRow.RUNNO = b.RunNo;
                        actRow.TOTALAMOUNT = b.TotalAmount;
                        actRow.UPDATEDATE = DateTime.Now;
                        actRow.UPDATEBYID = _user.UserID;

                    }
                    //delect activity
                    var lstDel = new List<PROJECTBUDGETACTIVITY>();
                    foreach (PROJECTBUDGETACTIVITY a in act)
                    {
                        var exist = model.BudgetActivities.Where(w => w.ActivityID == a.ACTIVITYID).FirstOrDefault();
                        if (exist == null)
                        {
                            //if (a.ACTIVITYNAME != Constants.ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE)
                            //{
                            lstDel.Add(a);
                            _db.Database.ExecuteSqlCommand(string.Format("delete from PROJECTBUDGET where ACTIVITYID={0}", a.ACTIVITYID.ToString()));
                            //}

                        }

                    }
                    if (lstDel.Count > 0)
                    {
                        _db.PROJECTBUDGETACTIVITies.RemoveRange(lstDel);
                    }


                    _db.SaveChanges();


                    //SaveBudgetDetail(model.BudgetDetails.ToList(), projectId,model.ActivityID, isRevise);



                    var savedResult = GetProjectBudgetInfoByProjectID(projectId);

                    result.Data = savedResult.Data;
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);

                    tran.Commit();
                }
                var generalInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectId).FirstOrDefault();
                decimal statusDraft = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus
                                                                && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                                                                .Select(x => x.LOVID).FirstOrDefault();
                bool isRevise = false;

                if (generalInfo != null)
                {


                    generalInfo.BudgetFromOtherFlag = (model.IsBudgetGotSupport.HasValue && model.IsBudgetGotSupport == true) ? "1" : ((model.IsBudgetGotSupport.HasValue) ? "0" : null);
                    generalInfo.BudgetFromOtherName = model.BudgetGotSupportName;
                    generalInfo.BudgetFromOtherAmount = model.BudgetGotSupportAmount;

                    generalInfo.UpdatedBy = _user.UserName;
                    generalInfo.UpdatedByID = _user.UserID;
                    generalInfo.UpdatedDate = DateTime.Now;
                    generalInfo.BudgetValue = 0;
                    generalInfo.BudgetReviseValue = 0;
                    var sum = from s in _db.ProjectBudgets where s.ProjectID == model.ProjectID select s;
                    foreach (var tmp in sum)
                    {
                        if (generalInfo.ProjectApprovalStatusID == statusDraft)
                        {
                            generalInfo.BudgetValue += tmp.BudgetValue;
                            generalInfo.BudgetReviseValue += tmp.BudgetReviseValue;
                        }
                        else
                        {
                            generalInfo.BudgetValue += tmp.BudgetValue;
                            generalInfo.BudgetReviseValue += tmp.BudgetReviseValue;
                            isRevise = true;
                        }
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        private void SaveBudgetDetail(List<ServiceModels.ProjectInfo.BudgetDetail> list, Decimal projectId, Decimal? activityID, Boolean isRevise)
        {
            List<ServiceModels.ProjectInfo.BudgetDetail> result = new List<ServiceModels.ProjectInfo.BudgetDetail>();
            decimal Id = projectId;
            List<DBModels.Model.ProjectBudget> dataCreate = new List<DBModels.Model.ProjectBudget>();
            List<DBModels.Model.ProjectBudget> listProjectBudgets = _db.ProjectBudgets.Where(x => x.ProjectID == Id).ToList();
            DBModels.Model.ProjectBudget dbModel;
            DBModels.Model.ProjectBudget objDb;
            foreach (var item in list)
            {
                if (item.ProjectBudgetID == 0)
                {
                    if (!isRevise)
                    {
                        dbModel = new DBModels.Model.ProjectBudget();
                        dbModel.ProjectID = Id;
                        dbModel.BUDGETCODE = item.BudgetCode;
                        dbModel.BudgetDetail = item.Detail;
                        dbModel.BudgetDetailRevise = item.Detail;
                        dbModel.BudgetValue = item.Amount.Value;
                        dbModel.BudgetReviseValue = item.Amount.Value;
                        dbModel.BudgetReviseValue1 = item.Amount.Value;
                        dbModel.BudgetReviseValue2 = item.Amount.Value;
                        dbModel.CreatedBy = _user.UserName;
                        dbModel.CreatedByID = (decimal)_user.UserID;
                        dbModel.CreatedDate = DateTime.Now;
                        //kenghot18
                        dbModel.BUDGETCODE = item.BudgetCode;
                        if (activityID.HasValue)
                        {
                            dbModel.ACTIVITYID = activityID;
                        }
                        dataCreate.Add(dbModel);
                    }
                }
                else
                {
                    decimal projectBudgetId = item.ProjectBudgetID;
                    objDb = listProjectBudgets.Where(x => x.ProjectBudgetID == projectBudgetId).FirstOrDefault();

                    if (objDb != null)
                    {
                        if (isRevise)
                        {
                            objDb.BudgetDetailRevise = item.ReviseDetail;
                            objDb.BudgetReviseValue = item.ReviseAmount.Value;
                            objDb.BudgetReviseValue1 = item.ReviseAmount.Value;
                            objDb.BudgetReviseValue2 = item.ReviseAmount.Value;
                            objDb.RemarkRevise = item.ReviseRemark;
                        }
                        else
                        {
                            objDb.BudgetDetail = item.Detail;
                            objDb.BudgetValue = item.Amount.Value;
                            objDb.BudgetDetailRevise = item.Detail;
                            objDb.BudgetReviseValue = item.Amount.Value;
                            objDb.BudgetReviseValue1 = item.Amount.Value;
                            objDb.BudgetReviseValue2 = item.Amount.Value;
                        }

                        objDb.UpdatedBy = _user.UserName;
                        objDb.UpdatedByID = _user.UserID;
                        objDb.UpdatedDate = DateTime.Now;
                        _db.SaveChanges();

                        listProjectBudgets.Remove(objDb);
                    }
                }
            }

            if (dataCreate.Count > 0)
            {
                _db.ProjectBudgets.AddRange(dataCreate);
                _db.SaveChanges();
            }

            if (listProjectBudgets.Count > 0)
            {
                _db.ProjectBudgets.RemoveRange(listProjectBudgets);
                _db.SaveChanges();
            }
        }
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> SaveProjectBudgetActivity(ServiceModels.ProjectInfo.ProjectBudget model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>();
            try
            {
                using (var tran = _db.Database.BeginTransaction())
                {
                    ServiceModels.ProjectInfo.ProjectBudget dataServiceModel = model;
                    decimal projectId = Convert.ToDecimal(model.ProjectID);
                    var generalInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectId).FirstOrDefault();
                    decimal statusDraft = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus
                                                                    && x.LOVCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                                                                    .Select(x => x.LOVID).FirstOrDefault();
                    bool isRevise = false;
                    var sum = SaveBudgetActivityDetail(model.BudgetDetails.ToList(), projectId, model.ActivityID, isRevise, statusDraft);
                    if (generalInfo != null)
                    {
                        if (generalInfo.ProjectApprovalStatusID == statusDraft)
                        {
                            generalInfo.BudgetValue = sum.BudgetValue;
                            generalInfo.BudgetReviseValue = sum.BudgetReviseValue;
                        }
                        else
                        {
                            generalInfo.BudgetReviseValue = sum.BudgetReviseValue;
                            generalInfo.BudgetValue = sum.BudgetValue;
                            isRevise = true;
                        }

                        //generalInfo.BudgetFromOtherFlag = (model.IsBudgetGotSupport.HasValue && model.IsBudgetGotSupport == true) ? "1" : ((model.IsBudgetGotSupport.HasValue) ? "0" : null);
                        //generalInfo.BudgetFromOtherName = model.BudgetGotSupportName;
                        //generalInfo.BudgetFromOtherAmount = model.BudgetGotSupportAmount;

                        generalInfo.UpdatedBy = _user.UserName;
                        generalInfo.UpdatedByID = _user.UserID;
                        generalInfo.UpdatedDate = DateTime.Now;


                        _db.SaveChanges();
                    }




                    var savedResult = GetProjectBudgetInfoByProjectID(projectId);

                    result.Data = savedResult.Data;
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);

                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        private DBModels.Model.ProjectGeneralInfo SaveBudgetActivityDetail(List<ServiceModels.ProjectInfo.BudgetDetail> list, Decimal projectId, Decimal? activityID, Boolean isRevise, decimal statusDraft)
        {
            DBModels.Model.ProjectGeneralInfo sumdata = new ProjectGeneralInfo();
            sumdata.BudgetValue = 0;
            sumdata.BudgetReviseValue = 0;
            List<ServiceModels.ProjectInfo.BudgetDetail> result = new List<ServiceModels.ProjectInfo.BudgetDetail>();
            decimal Id = projectId;
            List<DBModels.Model.ProjectBudget> dataCreate = new List<DBModels.Model.ProjectBudget>();
            List<DBModels.Model.ProjectBudget> listProjectBudgets = _db.ProjectBudgets.Where(x => x.ProjectID == Id && x.ACTIVITYID == activityID).ToList();
            List<DBModels.Model.ProjectBudget> listOthers = _db.ProjectBudgets.Where(x => x.ProjectID == Id && x.ACTIVITYID != activityID).ToList();
            PROJECTBUDGETACTIVITY ba = _db.PROJECTBUDGETACTIVITies.Where(w => w.ACTIVITYID == activityID).FirstOrDefault();
            ba.TOTALAMOUNT = 0;
            ba.UPDATEBYID = _user.UserID;
            ba.UPDATEDATE = DateTime.Now;
            foreach (var oth in listOthers)
            {
                sumdata.BudgetValue += oth.BudgetValue;
                sumdata.BudgetReviseValue += oth.BudgetReviseValue;
            }
            DBModels.Model.ProjectBudget dbModel;
            DBModels.Model.ProjectBudget objDb;
            foreach (var item in list)
            {
                sumdata.BudgetValue += item.Amount.Value;
                ba.TOTALAMOUNT += item.Amount.Value;
                if (item.ProjectBudgetID == 0)
                {
                    if (!isRevise)
                    {
                        dbModel = new DBModels.Model.ProjectBudget();
                        dbModel.ProjectID = Id;
                        dbModel.BUDGETCODE = item.BudgetCode;
                        dbModel.BudgetDetail = item.Detail;
                        dbModel.BudgetDetailRevise = item.Detail;
                        dbModel.BudgetValue = item.Amount.Value;
                        dbModel.BudgetReviseValue = item.Amount.Value;
                        dbModel.BudgetReviseValue1 = item.Amount.Value;
                        dbModel.BudgetReviseValue2 = item.Amount.Value;
                        dbModel.CreatedBy = _user.UserName;
                        dbModel.CreatedByID = (decimal)_user.UserID;
                        dbModel.CreatedDate = DateTime.Now;
                        //kenghot18
                        dbModel.BUDGETCODE = item.BudgetCode;
                        if (activityID.HasValue)
                        {
                            dbModel.ACTIVITYID = activityID;
                        }
                        sumdata.BudgetReviseValue += item.Amount.Value;
                        dataCreate.Add(dbModel);
                    }
                }
                else
                {
                    decimal projectBudgetId = item.ProjectBudgetID;
                    objDb = listProjectBudgets.Where(x => x.ProjectBudgetID == projectBudgetId).FirstOrDefault();

                    if (objDb != null)
                    {
                        if (isRevise)
                        {
                            objDb.BudgetDetailRevise = item.ReviseDetail;
                            objDb.BudgetReviseValue = item.ReviseAmount.Value;
                            objDb.BudgetReviseValue1 = item.ReviseAmount.Value;
                            objDb.BudgetReviseValue2 = item.ReviseAmount.Value;
                            objDb.RemarkRevise = item.ReviseRemark;
                            sumdata.BudgetReviseValue += item.ReviseAmount.Value;
                        }
                        else
                        {
                            objDb.BudgetDetail = item.Detail;
                            objDb.BudgetValue = item.Amount.Value;
                            objDb.BudgetDetailRevise = item.Detail;
                            objDb.BudgetReviseValue = item.Amount.Value;
                            objDb.BudgetReviseValue1 = item.Amount.Value;
                            objDb.BudgetReviseValue2 = item.Amount.Value;
                            sumdata.BudgetReviseValue += item.Amount.Value;
                        }

                        objDb.UpdatedBy = _user.UserName;
                        objDb.UpdatedByID = _user.UserID;
                        objDb.UpdatedDate = DateTime.Now;
                        _db.SaveChanges();

                        listProjectBudgets.Remove(objDb);
                    }
                }
            }

            if (dataCreate.Count > 0)
            {
                _db.ProjectBudgets.AddRange(dataCreate);
                _db.SaveChanges();
            }

            if (listProjectBudgets.Count > 0)
            {
                _db.ProjectBudgets.RemoveRange(listProjectBudgets);
                _db.SaveChanges();
            }

            return sumdata;
        }
        public ServiceModels.ReturnMessage DeleteProjectBudgetByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var generalInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == id).FirstOrDefault();

                if (generalInfo != null)
                {
                    generalInfo.BudgetValue = null;
                    generalInfo.BudgetReviseValue = null;
                    generalInfo.BudgetFromOtherFlag = null;
                    generalInfo.BudgetFromOtherName = null;
                    _db.SaveChanges();
                }

                var budgets = _db.ProjectBudgets.Where(x => x.ProjectID == id).ToList();
                if (budgets != null)
                {
                    _db.ProjectBudgets.RemoveRange(budgets);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }


        #endregion

        #region ProjectContract
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> GetProjectContractByProjectID(Decimal id)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> result = new ReturnObject<ServiceModels.ProjectInfo.TabContract>();
            try
            {
                decimal projectId = id;
                //var test = (from ct in _db.ProjectContracts where ct.ProjectID == id select ct).FirstOrDefault();
                var data = (from pro in _db.ProjectGeneralInfoes
                            where pro.ProjectID == id
                            select new ServiceModels.ProjectInfo.TabContract
                            {
                                ProjectID = pro.ProjectID,
                                OrganizationID = pro.OrganizationID,
                                ApprovalStatus = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatus : null,

                                ProjectApprovalStatusID = pro.ProjectApprovalStatusID,
                                ProjectApprovalStatusCode = pro.ProjectApprovalStatus.LOVCode,

                                ContractNo = (pro.ProjectContract != null) ? pro.ProjectContract.ContractNo : null,
                                ApprovalYear = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalYear : null,
                                ContractYear = (pro.ProjectContract != null) ? pro.ProjectContract.ContractYear : (Decimal?)null,
                                ContractDate = (pro.ProjectContract != null) ? pro.ProjectContract.ContractDate : (DateTime?)null,
                                ContractStartDate = (pro.ProjectContract != null) ? pro.ProjectContract.ContractStartDate : (DateTime?)null,
                                ContractEndDate = (pro.ProjectContract != null) ? pro.ProjectContract.ContractEndDate : (DateTime?)null,
                                Location = (pro.ProjectContract != null) ? pro.ProjectContract.ContractLocation : null,
                                ViewerName1 = (pro.ProjectContract != null) ? pro.ProjectContract.ContractViewerName1 : null,
                                ViewerSurname1 = (pro.ProjectContract != null) ? pro.ProjectContract.ContractViewerSurname1 : null,
                                ViewerName2 = (pro.ProjectContract != null) ? pro.ProjectContract.ContractViewerName2 : null,
                                ViewerSurname2 = (pro.ProjectContract != null) ? pro.ProjectContract.ContractViewerSurname2 : null,
                                DirectorFirstName = (pro.ProjectContract != null) ? pro.ProjectContract.DirectornameName : null,
                                DirectorLastName = (pro.ProjectContract != null) ? pro.ProjectContract.DirectorLastName : null,
                                DirectorPosition = (pro.ProjectContract != null) ? pro.ProjectContract.DirectorPosition : null,
                                AttorneyNo = (pro.ProjectContract != null) ? pro.ProjectContract.AttorneyNo : null,
                                AttorneyYear = (pro.ProjectContract != null) ? pro.ProjectContract.AttorneyYear : null,
                                ContractGiverDate = (pro.ProjectContract != null) ? pro.ProjectContract.ContractGiverDate : null,
                                ProvinceContractNo = (pro.ProjectContract != null) ? pro.ProjectContract.ProvinceContractNo : null,
                                ProvinceContractYear = (pro.ProjectContract != null) ? pro.ProjectContract.ProvinceContractYear : null,
                                ProvinceContractDate = (pro.ProjectContract != null) ? (DateTime?)pro.ProjectContract.ProvinceContractDate : null,
                                Remark = (pro.ProjectContract != null) ? pro.ProjectContract.REMARK : null,
                                OrganizationNameEN = pro.OrganizationNameEN,
                                OrganizationNameTH = pro.OrganizationNameTH,
                                Address = pro.Address,
                                Building = pro.Building,
                                Moo = pro.Moo,
                                Soi = pro.Soi,
                                Road = pro.Road,
                                SubdistrictName = pro.SubDistrict,
                                DistrictName = pro.District,
                                ProvinceName = _db.MT_Province.Where(prov => prov.ProvinceID == pro.AddressProvinceID).Select(provR => provR.ProvinceName).FirstOrDefault(),
                                PostCode = pro.Postcode,
                                Telephone = pro.Telephone,
                                Fax = pro.Fax,
                                Email = pro.Email,

                                ProvinceID = pro.ProvinceID,
                                CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == pro.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault(),
                                ContractReceiveDate = (pro.ProjectContract != null) ? (DateTime?)pro.ProjectContract.ContractReceiveDate : null,
                                // มอบอำนาจ
                                AuthorizeFlag = (pro.ProjectContract != null) ? ((pro.ProjectContract.AuthorizeFlag == Common.Constants.BOOLEAN_TRUE) ? true : false) : false,
                                ReceiverName = (pro.ProjectContract != null) ? pro.ProjectContract.ReceiverName : null,
                                ReceiverSurname = (pro.ProjectContract != null) ? pro.ProjectContract.ReceiverSurname : null,
                                ReceiverPosition = (pro.ProjectContract != null) ? pro.ProjectContract.ReceiverPosition : null,
                                AuthorizeDate = (pro.ProjectContract != null) ? ((pro.ProjectContract.AuthorizeDate != null) ? (DateTime?)pro.ProjectContract.AuthorizeDate : null) : null,
                                AuthorizeDocID = (pro.ProjectContract != null) ? ((pro.ProjectContract.AuthorizeDocID != null) ? pro.ProjectContract.AuthorizeDocID : (decimal?)null) : (decimal?)null,

                                RequestBudgetAmount = pro.BudgetValue,
                                ReviseBudgetAmount = pro.BudgetReviseValue,
                                AttachPage1 = pro.ProjectContract.ATTACHPAGE1,
                                AttachPage2 = pro.ProjectContract.ATTACHPAGE2,
                                AttachPage3 = pro.ProjectContract.ATTACHPAGE3,
                                MeetingDate = pro.ProjectContract.MEETINGDATE,
                                MeetingNo = pro.ProjectContract.MEETINGNO,
                                LastApproveStatus = pro.ProjectContract.APPROVESTATUS,
                                ExtendJson = pro.ProjectContract.EXTENDDATA
                      
                            }).SingleOrDefault();

                if (data != null)
                {
                    var dues = _db.CONTRACTDUEs.Where(w => w.PROJECTID == id).Select(s => new ContractDue
                    {
                        Amount = s.AMOUNT,
                        DueId = s.DUEID,
                        EndDate = s.ENDDATE,
                        ProjectId = s.PROJECTID,
                        StartDate = s.STARTDATE
                    }).ToList();
                    data.Dues = dues;

                    data.IsCenterContract = IsCenterReviseProject((decimal)data.ProvinceID);
                    try
                    {
                        if (!string.IsNullOrEmpty(data.ExtendJson))
                        {
                            data.ExtendData = Newtonsoft.Json.JsonConvert.DeserializeObject<ContractExtend>(data.ExtendJson);
                            if (data.ExtendData.AddressAt != null)
                            {
                                var ad = data.ExtendData.AddressAt;
                                var prov = _db.MT_Province.Where(w => w.ProvinceID == ad.ProvinceId).FirstOrDefault();
                                if (prov != null)
                                {
                                    ad.ProvinceName = prov.ProvinceName;
                                }
                                var dis = _db.MT_District.Where(w => w.DistrictID == ad.DistrictId).FirstOrDefault();
                                if (dis != null)
                                {
                                    ad.DistrictName = dis.DistrictName;
                                }
                                var sdis = _db.MT_SubDistrict.Where(w => w.SubDistrictID == ad.SubDistrictId).FirstOrDefault();
                                if (sdis != null)
                                {
                                    ad.SubDistrictName = sdis.SubDistrictName;
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                    //ข้อมูลผู้ให้เงินอุดหนุนของส่วนกลาง
                    if (data.IsCenterContract)
                    {

                        if (String.IsNullOrEmpty(data.DirectorFirstName))
                        {
                            string orgDirectorGeneralNameParam = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_DIRECTOR_GENERAL_NAME).Select(y => y.ParameterValue).FirstOrDefault();
                            string orgDirectorGeneralPosParam = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_DIRECTOR_GENERAL_POS).Select(y => y.ParameterValue).FirstOrDefault();

                            int spaceIndex = orgDirectorGeneralNameParam.LastIndexOf(" ");
                            data.DirectorFirstName = orgDirectorGeneralNameParam.Substring(0, spaceIndex);
                            data.DirectorLastName = orgDirectorGeneralNameParam.Substring(spaceIndex, (orgDirectorGeneralNameParam.Length - spaceIndex));

                            data.DirectorPosition = orgDirectorGeneralPosParam;
                        }

                        if (String.IsNullOrEmpty(data.Location))
                        {
                            string orgAddressParam = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_ADDRESS).Select(y => y.ParameterValue).FirstOrDefault();
                            orgAddressParam = orgAddressParam.Replace("\n", " ");
                            data.Location = orgAddressParam;
                        }
                    }

                    if (string.IsNullOrEmpty(data.ReceiverName))
                    {
                        DBModels.Model.ProjectPersonel proContractReceive = _db.ProjectPersonels.Where(x => (x.ProjectID == id)).FirstOrDefault();
                        if (proContractReceive != null)
                        {
                            data.ContractReceiveName = string.Format("{0}{1}", proContractReceive.Prefix1Personel.LOVName, proContractReceive.Firstname1);
                            data.ContractReceiveSurname = proContractReceive.Lastname1;
                            data.ContractReceivePosition = Resources.Model.Personal_Responsibility;
                        }
                    }
                    else
                    {
                        data.ContractReceiveName = data.ReceiverName;
                        data.ContractReceiveSurname = data.ReceiverSurname;
                        data.ContractReceivePosition = data.ReceiverPosition;
                    }

                    if (data.AuthorizeDocID.HasValue)
                    {
                        DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.AuthorizeDocID).FirstOrDefault();
                        if (dbAttach != null)
                        {
                            ServiceModels.KendoAttachment file = new KendoAttachment()
                            {
                                id = dbAttach.AttachmentID.ToString(),
                                name = dbAttach.AttachmentFilename,
                                extension = Path.GetExtension(dbAttach.AttachmentFilename),
                                size = (int)dbAttach.FileSize
                            };

                            data.AuthorizeDocAttachment = file;
                        }
                    }
                }
                var att = new Business.AttachmentService(_db);
                data.SupportAttachments = att.GetAttachmentOfTable(TABLE_PROJECTCONTRACT, CONTRACT_SUPPORT, id);
                result.Data = data;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> SaveProjectContract(ServiceModels.ProjectInfo.TabContract model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> result = new ReturnObject<ServiceModels.ProjectInfo.TabContract>();
            try
            {
                ServiceModels.ProjectInfo.TabContract dataServiceModel = model;
                decimal projectId = Convert.ToDecimal(model.ProjectID);
                DBModels.Model.ProjectContract dataDBModel = MappTabProjectContractToDBProjectContract(dataServiceModel);
                bool hasData = _db.ProjectContracts.Where(x => x.ProjectID == model.ProjectID).Count() > 0;
                int contractYear = (Int32)dataDBModel.ContractYear;
                string contractNo = "";
                using (var tran = _db.Database.BeginTransaction())
                {
                    if (hasData) // for update
                    {
                        if (contractYear <= 2016)
                        {
                            contractNo = model.ContractNo;
                            if ((!String.IsNullOrEmpty(contractNo)) && !CheckContractNo(contractNo, dataDBModel.ProjectID))
                            {
                                result.IsCompleted = false;
                                result.Message.Add(String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Contract_ContractNo));
                                return result;
                            }
                            dataDBModel.ContractNo = contractNo;
                        }
                        _db.CONTRACTDUEs.RemoveRange(dataDBModel.CONTRACTDUEs);
                        short i = 1;
                        foreach (var due in dataServiceModel.Dues)
                        {
                            var newDue = new CONTRACTDUE
                            {
                                AMOUNT = due.Amount,
                                ENDDATE = due.EndDate,
                                PROJECTID = model.ProvinceID,
                                STARTDATE = due.StartDate,
                                DUENO = i
                            };
                            dataDBModel.CONTRACTDUEs.Add(newDue);
                            i++;
                        }
                        
                        
                        dataDBModel.UpdatedDate = DateTime.Now;
                        dataDBModel.UpdatedBy = _user.UserName;
                        dataDBModel.UpdatedByID = _user.UserID;
                        _db.SaveChanges();
                    }
                    else // for create
                    {
                        DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID &&
                            ((x.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
                             (x.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))).SingleOrDefault();
                        DBModels.Model.MT_ListOfValue status = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว, Common.LOVGroup.ProjectApprovalStatus);

                        if (genInfo != null)
                        {
                            dataDBModel.APPROVESTATUS = genInfo.ProjectApprovalStatusID;
                            genInfo.ProjectApprovalStatusID = status.LOVID;
                            genInfo.UpdatedBy = _user.UserName;
                            genInfo.UpdatedByID = _user.UserID;
                            genInfo.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception(Nep.Project.Resources.Error.ProjectApprovalStatusInvalid);
                        }

                        int thaiContractYear = Convert.ToInt32(dataDBModel.ContractYear) + 543;
                        contractNo = (contractYear > 2016) ? _runningService.GetProjectContractNo(model.ProjectID, thaiContractYear).Data : model.ContractNo;

                        if ((!String.IsNullOrEmpty(contractNo)) && !CheckContractNo(contractNo, dataDBModel.ProjectID))
                        {
                            result.IsCompleted = false;
                            result.Message.Add(String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Contract_ContractNo));
                            return result;
                        }

                        dataDBModel.ContractNo = contractNo;
                        dataDBModel.CreatedDate = DateTime.Now;
                        dataDBModel.CreatedBy = _user.UserName;
                        dataDBModel.CreatedByID = (decimal)_user.UserID;
                        short i = 0;
                        foreach (var due in dataServiceModel.Dues)
                        {
                            var newDue = new CONTRACTDUE
                            {
                                AMOUNT = due.Amount,
                                ENDDATE = due.EndDate,
                                PROJECTID = model.ProvinceID,
                                STARTDATE = due.StartDate,
                                DUENO = i
                            };
                            dataDBModel.CONTRACTDUEs.Add(newDue);
                            i++;
                        }
                        _db.PROJECTHISTORies.Add(CreateRowProjectHistory(model.ProjectID, "4", _user.UserID.Value, model.ipAddress));
                        _db.ProjectContracts.Add(dataDBModel);
                        _db.SaveChanges();
                    }
                    SaveAttachFile(model.ProjectID, Common.LOVCode.Attachmenttype.PROJECT_CONTRACT, model.RemovedSupportAttachments, model.AddedSupportAttachments, TABLE_PROJECTCONTRACT, CONTRACT_SUPPORT);

                    result.Data = MappDBProjectContractToTabProjectContract(dataDBModel);
                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage CancelProjectContract(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == id).SingleOrDefault();
                if (genInfo != null)
                {
                    DBModels.Model.MT_ListOfValue status = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา, Common.LOVGroup.ProjectApprovalStatus);
                    genInfo.ProjectApprovalStatusID = status.LOVID;
                    genInfo.UpdatedBy = _user.UserName;
                    genInfo.UpdatedByID = _user.UserID;
                    genInfo.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.CancelProjectContractSuccess);
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public ServiceModels.ReturnMessage UndoCancelProjectContract(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == id).SingleOrDefault();
                if (genInfo != null)
                {
                    DBModels.Model.MT_ListOfValue status = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา, Common.LOVGroup.ProjectApprovalStatus);
                    if (genInfo.ProjectApprovalStatusID != status.LOVID)
                    {
                        throw new Exception("ไม่สามารถกลับสัญญาที่ยังไม่ได้ยกเลิกได้");
                    }
                    var contract = _db.ProjectContracts.Where(w => w.ProjectID == id).FirstOrDefault();
                    if (contract == null || !contract.APPROVESTATUS.HasValue)
                    {
                        throw new Exception("ไม่พบสัญญาที่ผ่านการอนุมัติ");
                    }
                    _db.ProjectContracts.Remove(contract);
                    genInfo.ProjectApprovalStatusID = contract.APPROVESTATUS.Value;
                    genInfo.UpdatedBy = _user.UserName;
                    genInfo.UpdatedByID = _user.UserID;
                    genInfo.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.CancelProjectContractSuccess);
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public class RejectTopic
        {
            public readonly bool IsProjInfo = false;
            public readonly bool IsBudget = false;
            public readonly bool IsPerson = false;
            public readonly bool IsAttach = false;
            public readonly bool IsProcess = false;
            public RejectTopic(string chk)
            {
                try
                {
                    if (!string.IsNullOrEmpty(chk))
                    {
                        var s = chk.Split(',');
                        if (s.Contains("1")) IsProjInfo = true;
                        if (s.Contains("2")) IsBudget = true;
                        if (s.Contains("3")) IsPerson = true;
                        if (s.Contains("4")) IsAttach = true;
                        if (s.Contains("5")) IsProcess = true;
                    }
                }
                catch (Exception ex)
                {

                }


            }

        }
        public ServiceModels.ReturnMessage RejectToOrganization(decimal projectID, string comment, string checkbox)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.ProjectInformation info = _db.ProjectInformations.Where(x => x.ProjectID == projectID).SingleOrDefault();
                DBModels.Model.MT_ListOfValue status = _db.MT_ListOfValue.Where(x => (x.LOVCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) && (x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus)).FirstOrDefault();
                if (info != null)
                {
                    info.UpdatedBy = _user.UserName;
                    info.UpdatedByID = _user.UserID;
                    info.UpdatedDate = DateTime.Now;
                    info.RejectComment = comment;
                    info.ProjectGeneralInfo.ProjectApprovalStatusID = status.LOVID;
                    info.ProjectGeneralInfo.UpdatedBy = _user.UserName;
                    info.ProjectGeneralInfo.UpdatedByID = _user.UserID;
                    info.ProjectGeneralInfo.UpdatedDate = DateTime.Now;
                    //kenghot18
                    info.RejectTopic = checkbox;
                    _db.SaveChanges();

                    result.IsCompleted = true;
                    _mailService.SendRejectProject(projectID);
                    _smsService.Send("โครงการของท่าน ได้ถูกส่งให้แก้ไข", _db, projectID);
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<List<String>> GetRejectComment(decimal projectID)
        {
            ServiceModels.ReturnObject<List<string>> result = new ReturnObject<List<string>>();
            try
            {
                var data = _db.ProjectInformations.Where(x => x.ProjectID == projectID).Select(y => new { RejectComment = y.RejectComment, RejectTopic = y.RejectTopic }).FirstOrDefault();
                result.IsCompleted = true;
                result.Data = new List<string>();
                result.Data.Add(data.RejectComment);
                result.Data.Add(data.RejectTopic);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage DeleteProjectContractByID(Decimal id)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var q = _db.ProjectContracts.Where(x => x.ProjectID == id).FirstOrDefault();
                if (q != null)
                {
                    _db.ProjectContracts.Remove(q);
                    _db.SaveChanges();
                }

                result.IsCompleted = true;
                result.Message.Add(Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportFormatContract> GetReportFormatContract(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportFormatContract> result = new ReturnObject<ServiceModels.Report.ReportFormatContract>();

            try
            {
                var approvalStatus = _db.ProjectApprovals.Where(x => x.ProjectID == projectID).Select(y => y.ApprovalStatus).FirstOrDefault();
                //var projectApprovalSatatus = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus) && (x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)).FirstOrDefault();
                var genIn = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
                if (genIn != null)
                {
                    string contractBy = "";
                    string directiveNo = "";
                    string directProvinceNo = "";
                    string provinceName = _db.MT_Province.Where(x => x.ProvinceID == genIn.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                    string receiverProvince = _db.MT_Province.Where(x => x.ProvinceID == genIn.AddressProvinceID).Select(y => y.ProvinceName).FirstOrDefault();

                    var receiver = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();

                    decimal? creatorOrganizationID = _db.SC_User.Where(x => (x.UserID == genIn.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();

                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canPrint = functions.Contains(Common.ProjectFunction.PrintContract);

                    if (canPrint)
                    {
                        DBModels.Model.ProjectContract contract = genIn.ProjectContract;
                        DBModels.Model.ProjectApproval approval = genIn.ProjectApproval;

                        contractBy = contract.DirectornameName + " " + contract.DirectorLastName;
                        directiveNo = (!string.IsNullOrEmpty(contract.AttorneyNo)) ? (contract.AttorneyNo + "/" + Common.Web.WebUtility.ToBuddhaYear(contract.AttorneyYear)) : "";
                        directProvinceNo = (!string.IsNullOrEmpty(contract.ProvinceContractNo)) ? (contract.ProvinceContractNo + "/" + Common.Web.WebUtility.ToBuddhaYear(contract.ProvinceContractYear)) : "";

                        ServiceModels.Report.ReportFormatContract obj = new ServiceModels.Report.ReportFormatContract();
                        obj.ContractNo = contract.ContractNo;
                        obj.SignAt = contract.ContractLocation;
                        obj.ContractDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractDate, "d MMMM yyyy");
                        obj.ContractBy = contractBy;
                        obj.Position = contract.DirectorPosition;
                        obj.DirectiveNo = directiveNo;
                        obj.DirectiveDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractGiverDate, "d MMMM yyyy");
                        obj.DirectiveProvince = provinceName;
                        obj.DirectProvinceNo = directProvinceNo;
                        obj.DirectProvinceDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ProvinceContractDate, "d MMMM yyyy");
                        obj.ReceiverName = genIn.OrganizationNameTH;
                        obj.ReceiverAddressNo = genIn.Address;

                        obj.ReceiverSubdistrict = genIn.SubDistrict;
                        obj.ReceiverDistrict = genIn.District;
                        obj.ReceiverProvince = receiverProvince;

                        obj.AttorneyDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractReceiveDate, "d MMMM yyyy");
                        obj.Amount = Common.Web.WebUtility.DisplayInHtml(genIn.BudgetReviseValue, "N2", "");
                        obj.AmountString = Common.Web.WebUtility.ToThaiBath(genIn.BudgetReviseValue);
                        obj.ProjectName = genIn.ProjectInformation.ProjectNameTH;

                        obj.ContractViewerName1 = contract.ContractViewerName1;
                        obj.ContractViewerSurname1 = contract.ContractViewerSurname1;
                        obj.ContractViewerName2 = contract.ContractViewerName2;
                        obj.ContractViewerSurname2 = contract.ContractViewerSurname2;

                        obj.IsCenterContract = IsCenterReviseProject(genIn.ProvinceID);

                        obj.AuthorizeFlag = (contract.AuthorizeFlag == Common.Constants.BOOLEAN_TRUE) ? true : false;
                        obj.ReceiverBy = String.Format("{0} {1}", contract.ReceiverName, contract.ReceiverSurname);
                        obj.ReceivePosition = contract.ReceiverPosition;
                        obj.AttachPage1 = contract.ATTACHPAGE1?.ToString();
                        obj.AttachPage2 = contract.ATTACHPAGE2?.ToString();
                        obj.AttachPage3 = contract.ATTACHPAGE3?.ToString();
                        obj.MeetingDateText = contract.MEETINGDATE.HasValue ? Common.Web.WebUtility.ToBuddhaDateFormat(contract.MEETINGDATE, "d MMMM yyyy") : "";
                        obj.MeetingDate = contract.MEETINGDATE;
                        obj.MeetingNo = contract.MEETINGNO?.ToString();

                        if (!string.IsNullOrEmpty(contract.EXTENDDATA))
                        {
                            obj.ExtendData = Newtonsoft.Json.JsonConvert.DeserializeObject<ContractExtend>(contract.EXTENDDATA);
                            obj.ExtendData.BookDateText = Common.Web.WebUtility.ToBuddhaDateFormat(obj.ExtendData.BookDate, "d MMMM yyyy");
                            obj.ExtendData.CommandDateText = Common.Web.WebUtility.ToBuddhaDateFormat(obj.ExtendData.CommandDate, "d MMMM yyyy");
                            obj.ExtendData.MeetingDateText = Common.Web.WebUtility.ToBuddhaDateFormat(obj.ExtendData.MeetingDate, "d MMMM yyyy");
                            obj.ExtendData.BookNo = WebUtility.ParseToThaiNumber(obj.ExtendData.BookNo);
                            obj.ExtendData.BookOrder = WebUtility.ParseToThaiNumber(obj.ExtendData.BookOrder);
                            obj.ExtendData.Command = WebUtility.ParseToThaiNumber(obj.ExtendData.Command);

                            obj.ExtendData.PageCount1Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount1.ToString());
                            obj.ExtendData.PageCount2Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount2.ToString());
                            obj.ExtendData.PageCount3Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount3.ToString());
                            obj.ExtendData.PageCount4Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount4.ToString());
                            obj.ExtendData.PageCount5Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount5.ToString());
                            obj.ExtendData.PageCount6Text = WebUtility.ParseToThaiNumber(obj.ExtendData.PageCount6.ToString());
                            if (obj.ExtendData.AddressAt != null)
                            {
                                var ad = obj.ExtendData.AddressAt;
                                var prov = _db.MT_Province.Where(w => w.ProvinceID == ad.ProvinceId).FirstOrDefault();
                                if (prov != null)
                                {
                                    ad.ProvinceName = prov.ProvinceName;
                                }
                                var dis = _db.MT_District.Where(w => w.DistrictID == ad.DistrictId).FirstOrDefault();
                                if (dis != null)
                                {
                                    ad.DistrictName = dis.DistrictName;
                                }
                                var sdis = _db.MT_SubDistrict.Where(w => w.SubDistrictID == ad.SubDistrictId).FirstOrDefault();
                                if (sdis != null)
                                {
                                    ad.SubDistrictName = sdis.SubDistrictName;
                                }
                            }
                            if (obj.ExtendData.AddressAuth != null)
                            {
                                var ad = obj.ExtendData.AddressAuth;
                                var prov = _db.MT_Province.Where(w => w.ProvinceID == ad.ProvinceId).FirstOrDefault();
                                if (prov != null)
                                {
                                    ad.ProvinceName = prov.ProvinceName;
                                }
                                var dis = _db.MT_District.Where(w => w.DistrictID == ad.DistrictId).FirstOrDefault();
                                if (dis != null)
                                {
                                    ad.DistrictName = dis.DistrictName;
                                }
                                var sdis = _db.MT_SubDistrict.Where(w => w.SubDistrictID == ad.SubDistrictId).FirstOrDefault();
                                if (sdis != null)
                                {
                                    ad.SubDistrictName = sdis.SubDistrictName;
                                }
                            }
                        }
                        result.IsCompleted = true;
                        result.Data = obj;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportFormatContract> GetReportFormatContractOld(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportFormatContract> result = new ReturnObject<ServiceModels.Report.ReportFormatContract>();

            try
            {
                var approvalStatus = _db.ProjectApprovals.Where(x => x.ProjectID == projectID).Select(y => y.ApprovalStatus).FirstOrDefault();
                //var projectApprovalSatatus = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus) && (x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)).FirstOrDefault();
                var genIn = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
                if (genIn != null)
                {
                    string contractBy = "";
                    string directiveNo = "";
                    string directProvinceNo = "";
                    string provinceName = _db.MT_Province.Where(x => x.ProvinceID == genIn.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                    string receiverProvince = _db.MT_Province.Where(x => x.ProvinceID == genIn.AddressProvinceID).Select(y => y.ProvinceName).FirstOrDefault();

                    var receiver = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();

                    decimal? creatorOrganizationID = _db.SC_User.Where(x => (x.UserID == genIn.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();

                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canPrint = functions.Contains(Common.ProjectFunction.PrintContract);

                    if (canPrint)
                    {
                        DBModels.Model.ProjectContract contract = genIn.ProjectContract;
                        DBModels.Model.ProjectApproval approval = genIn.ProjectApproval;

                        contractBy = contract.DirectornameName + " " + contract.DirectorLastName;
                        directiveNo = (!string.IsNullOrEmpty(contract.AttorneyNo)) ? (contract.AttorneyNo + "/" + Common.Web.WebUtility.ToBuddhaYear(contract.AttorneyYear)) : "";
                        directProvinceNo = (!string.IsNullOrEmpty(contract.ProvinceContractNo)) ? (contract.ProvinceContractNo + "/" + Common.Web.WebUtility.ToBuddhaYear(contract.ProvinceContractYear)) : "";

                        ServiceModels.Report.ReportFormatContract obj = new ServiceModels.Report.ReportFormatContract();
                        obj.ContractNo = contract.ContractNo;
                        obj.SignAt = contract.ContractLocation;
                        obj.ContractDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractDate, "d MMMM yyyy");
                        obj.ContractBy = contractBy;
                        obj.Position = contract.DirectorPosition;
                        obj.DirectiveNo = directiveNo;
                        obj.DirectiveDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractGiverDate, "d MMMM yyyy");
                        obj.DirectiveProvince = provinceName;
                        obj.DirectProvinceNo = directProvinceNo;
                        obj.DirectProvinceDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ProvinceContractDate, "d MMMM yyyy");
                        obj.ReceiverName = genIn.OrganizationNameTH;
                        obj.ReceiverAddressNo = genIn.Address;
                        obj.ReceiverSubdistrict = genIn.SubDistrict;
                        obj.ReceiverDistrict = genIn.District;
                        obj.ReceiverProvince = receiverProvince;
                        obj.AttorneyDate = Common.Web.WebUtility.ToBuddhaDateFormat(contract.ContractReceiveDate, "d MMMM yyyy");
                        obj.Amount = Common.Web.WebUtility.DisplayInHtml(genIn.BudgetReviseValue, "N2", "");
                        obj.AmountString = Common.Web.WebUtility.ToThaiBath(genIn.BudgetReviseValue);
                        obj.ProjectName = genIn.ProjectInformation.ProjectNameTH;

                        obj.ContractViewerName1 = contract.ContractViewerName1;
                        obj.ContractViewerSurname1 = contract.ContractViewerSurname1;
                        obj.ContractViewerName2 = contract.ContractViewerName2;
                        obj.ContractViewerSurname2 = contract.ContractViewerSurname2;

                        obj.IsCenterContract = IsCenterReviseProject(genIn.ProvinceID);

                        obj.AuthorizeFlag = (contract.AuthorizeFlag == Common.Constants.BOOLEAN_TRUE) ? true : false;
                        obj.ReceiverBy = String.Format("{0} {1}", contract.ReceiverName, contract.ReceiverSurname);
                        obj.ReceivePosition = contract.ReceiverPosition;
                        obj.AttachPage1 = contract.ATTACHPAGE1?.ToString();
                        obj.AttachPage2 = contract.ATTACHPAGE2?.ToString();
                        obj.AttachPage3 = contract.ATTACHPAGE3?.ToString();
                        obj.MeetingDateText = contract.MEETINGDATE.HasValue ? Common.Web.WebUtility.ToBuddhaDateFormat(contract.MEETINGDATE, "d MMMM yyyy") : "";
                        obj.MeetingDate = contract.MEETINGDATE;
                        obj.MeetingNo = contract.MEETINGNO?.ToString();
                        result.IsCompleted = true;
                        result.Data = obj;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        #endregion

        #region ProjectApprovalResult
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectApprovalResult> GetProjectApprovalResult(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectApprovalResult> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectApprovalResult>();
            try
            {
                var data = (from pro in _db.ProjectGeneralInfoes
                            where pro.ProjectID == projectID
                            select new ServiceModels.ProjectInfo.ProjectApprovalResult
                            {
                                ProjectID = pro.ProjectID,
                                OrganizationID = pro.OrganizationID,

                                ApprovalStatus = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatus : null,

                                ProjectApprovalStatusID = pro.ProjectApprovalStatusID,
                                ProjectApprovalStatusCode = pro.ProjectApprovalStatus.LOVCode,
                                ProvinceID = pro.ProvinceID,

                                OrganizationNameTH = pro.OrganizationNameTH,
                                OrganizationNameEN = pro.OrganizationNameEN,

                                TotalBudgetRequest = pro.BudgetValue,

                                ProjectNo = (pro.ProjectInformation != null) ? pro.ProjectInformation.ProjectNo : null,
                                ProjectNameTH = (pro.ProjectInformation != null) ? pro.ProjectInformation.ProjectNameTH : null,
                                ProjectNameEN = (pro.ProjectInformation != null) ? pro.ProjectInformation.ProjectNameEN : null,

                                EvaluationIsPassAss4 = (pro.ProjectEvaluation != null) ? pro.ProjectEvaluation.IsPassAss4 : null,
                                EvaluationIsPassAss5 = (pro.ProjectEvaluation != null) ? pro.ProjectEvaluation.IsPassAss5 : null,
                                EvaluationScore = (pro.ProjectEvaluation != null) ? pro.ProjectEvaluation.EvaluationValue : 0,
                                EvaluationScoreCode = (pro.ProjectEvaluation != null) ? pro.ProjectEvaluation.EvaluationStatus.LOVCode : null,
                                EvaluationScoreDesc = (pro.ProjectEvaluation != null) ? pro.ProjectEvaluation.EvaluationStatus.LOVName : null,

                                ApprovalStatusID1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatusID1 : (decimal?)null,
                                ApprovalBudget1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalBudget1 : (decimal?)null,
                                ApprovalDesc1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalDesc1 : null,
                                ApprovalName1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalName1 : null,
                                ApprovalLastName1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalLastName1 : null,
                                ApproverPosition1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApproverPosition1 : null,
                                ApprovalDate1 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalDate1 : (DateTime?)null,

                                ApprovalStatusID2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatusID2 : (decimal?)null,
                                ApprovalBudget2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalBudget2 : (decimal?)null,
                                ApprovalDesc2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalDesc2 : null,
                                ApprovalName2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalName2 : null,
                                ApprovalLastName2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalLastName2 : null,
                                ApproverPosition2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApproverPosition2 : null,
                                ApprovalDate2 = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalDate2 : (DateTime?)null,

                                ApprovalNo = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalNo : null,
                                ApprovalYear = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalYear : null,
                                ApprovalDate = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalDate : null,

                                CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == pro.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault(),

                                BudgetTypeID = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalBudgetTypeID : (decimal?)null

                            }).FirstOrDefault();

                if (data != null)
                {
                    var budgetDetails = (from budget in _db.ProjectBudgets
                                         where budget.ProjectID == projectID
                                         select new ServiceModels.ProjectInfo.BudgetDetail
                                         {
                                             ProjectBudgetID = budget.ProjectBudgetID,
                                             ReviseDetail = budget.BudgetDetailRevise,
                                             Amount = budget.BudgetValue,
                                             ReviseAmount = budget.BudgetReviseValue,
                                             Revise1Amount = budget.BudgetReviseValue1,
                                             Revise2Amount = budget.BudgetReviseValue2,
                                             ReviseRemark = budget.RemarkRevise,
                                             ApprovalRemark = budget.RemarkApproval,
                                             ActivityID = budget.ACTIVITYID

                                             //kenghot
                                         }).OrderBy(or => or.ProjectBudgetID).ToList();
                    //}).OrderByDescending(or => or.Amount).ToList();
                    data.BudgetDetails = budgetDetails;
                    data.IsCenterReviseProject = IsCenterReviseProject(data.ProvinceID);

                    result.Data = data;
                    result.IsCompleted = true;
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        //kenghot
        private decimal? GetProjApprovalStatusByApprover(DBModels.Model.MT_ListOfValue lov)
        {
            //var status = GetListOfValueByKey(ApproveID);
            string projst = "";
            //กลั่นกรอง
            if (lov.LOVGroup == Common.LOVGroup.ApprovalStatus1)
            {
                switch (lov.LOVCode)
                {

                    case Common.LOVCode.Approvalstatus1.ชะลอการพิจารณา:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_1_ชะลอการพิจารณา;
                            break;
                        }
                    case Common.LOVCode.Approvalstatus1.ยกเลิก:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยคณะกรรมการกลั่นกรอง;
                            break;
                        }
                    case Common.LOVCode.Approvalstatus1.อื่นๆ:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยคณะกรรมการกลั่นกรอง;
                            break;
                        }
                }
            }
            //กองทุน
            if (lov.LOVGroup == Common.LOVGroup.ApprovalStatus2)
            {
                switch (lov.LOVCode)
                {
                    case Common.LOVCode.Approvalstatus2.ชะลอการพิจารณา:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_5_1_ชะลอการพิจารณา;
                            break;
                        }
                    case Common.LOVCode.Approvalstatus2.ยกเลิก:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยอนุกรรมการกองทุนหรือจังหวัด;
                            break;
                        }
                    case Common.LOVCode.Approvalstatus2.อื่นๆ:
                        {
                            projst = Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยอนุกรรมการกองทุนหรือจังหวัด;
                            break;
                        }
                }
            }
            if (projst == "")
            { return null; }

            var l = GetListOfValue(projst, Common.LOVGroup.ProjectApprovalStatus);
            return l.LOVID;
        }
        public ServiceModels.ReturnMessage SaveProjectApprovalResult(ServiceModels.ProjectInfo.ProjectApprovalResult model)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                decimal requestBudget = 0;
                decimal approvedBudget = 0;
                bool isSendApprovedMail = false;
                bool isSendNotApprovedMail = false;
                string approvalStatus = null;
                bool isTransCommit = false;
                string orgMobile = "";
                string personalMobile = "";
                string projectName = "";
                int approvalYear = 0;
                decimal projectApprovalID = 0;
                decimal oldProjectStatus = 0;
                decimal newProjectStatus = 0;
                decimal? oldBudgetRevise = (decimal?)null;
                decimal? newBudgetRevise = (decimal?)null;
                bool isCreateProjectNo = false;
                using (var tran = _db.Database.BeginTransaction())
                {
                    ServiceModels.ProjectInfo.ProjectApprovalLog oldValue = new ServiceModels.ProjectInfo.ProjectApprovalLog();
                    ServiceModels.ProjectInfo.ProjectApprovalLog newValue = new ServiceModels.ProjectInfo.ProjectApprovalLog();
                    DBModels.Model.MT_ListOfValue step4Status = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง, Common.LOVGroup.ProjectApprovalStatus);
                    DBModels.Model.MT_ListOfValue step4NotApprovedStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง, Common.LOVGroup.ProjectApprovalStatus);
                    DBModels.Model.ProjectApproval dbApproval = _db.ProjectApprovals.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                    DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).SingleOrDefault();

                    oldProjectStatus = genInfo.ProjectApprovalStatusID;
                    newProjectStatus = oldProjectStatus;
                    oldBudgetRevise = genInfo.BudgetReviseValue;
                    newBudgetRevise = oldBudgetRevise;

                    #region Assign Project Approval Value
                    if (dbApproval == null)
                    {
                        dbApproval = new DBModels.Model.ProjectApproval();
                    }
                    else
                    {
                        projectApprovalID = dbApproval.ProjectID;
                        oldProjectStatus = genInfo.ProjectApprovalStatusID;

                        newValue = GetNewValueOfProjectApproval(model);
                        oldValue = GetCurrentProjectApproval(model.ProjectID);
                    }
                    approvalYear = (!String.IsNullOrEmpty(model.ApprovalYear)) ? Int32.Parse(model.ApprovalYear) : 0;
                    dbApproval.ApprovalBudgetTypeID = (decimal)model.BudgetTypeID;
                    dbApproval.ApprovalStatusID1 = (decimal)model.ApprovalStatusID1;
                    dbApproval.ApprovalBudget1 = model.ApprovalBudget1;
                    dbApproval.ApprovalDesc1 = model.ApprovalDesc1;
                    dbApproval.ApprovalName1 = model.ApprovalName1;
                    dbApproval.ApprovalLastName1 = model.ApprovalLastName1;
                    dbApproval.ApproverPosition1 = model.ApproverPosition1;
                    dbApproval.ApprovalDate1 = model.ApprovalDate1;

                    dbApproval.ApprovalStatusID2 = model.ApprovalStatusID2;
                    dbApproval.ApprovalBudget2 = model.ApprovalBudget2;
                    dbApproval.ApprovalDesc2 = model.ApprovalDesc2;
                    dbApproval.ApprovalName2 = model.ApprovalName2;
                    dbApproval.ApprovalLastName2 = model.ApprovalLastName2;
                    dbApproval.ApproverPosition2 = model.ApproverPosition2;
                    dbApproval.ApprovalDate2 = model.ApprovalDate2;

                    dbApproval.ApprovalNo = model.ApprovalNo;
                    dbApproval.ApprovalYear = model.ApprovalYear;
                    dbApproval.ApprovalDate = model.ApprovalDate;

                    if (projectApprovalID > 0)
                    {
                        dbApproval.UpdatedBy = _user.UserName;
                        dbApproval.UpdatedDate = DateTime.Now;


                    }
                    else
                    {
                        dbApproval.ProjectID = model.ProjectID;
                        dbApproval.CreatedBy = _user.UserName;
                        dbApproval.CreatedByID = (decimal)_user.UserID;
                        dbApproval.CreatedDate = DateTime.Now;
                        _db.ProjectApprovals.Add(dbApproval);
                    }

                    #endregion Assign Project Approval Value

                    projectName = _db.ProjectInformations.Where(x => x.ProjectID == model.ProjectID).Select(y => y.ProjectNameTH).SingleOrDefault();


                    #region Update Project Approval Status
                    DBModels.Model.MT_Organization org = _db.MT_Organization.Where(x => x.OrganizationID == genInfo.OrganizationID).FirstOrDefault();
                    DBModels.Model.ProjectPersonel personal = _db.ProjectPersonels.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                    DBModels.Model.MT_ListOfValue projectApprovalStatus;

                    orgMobile = org.Mobile;
                    personalMobile = personal.Mobile1;

                    if (model.IsCenterReviseProject)
                    {
                        #region Center
                        if (dbApproval.ApprovalStatusID2.HasValue &&
                            (!String.IsNullOrEmpty(dbApproval.ApprovalNo)) &&
                            (!String.IsNullOrEmpty(dbApproval.ApprovalYear)) &&
                            (dbApproval.ApprovalDate.HasValue))
                        {

                            projectApprovalStatus = _db.MT_ListOfValue.Where(x => x.LOVID == (decimal)model.ApprovalStatusID2).FirstOrDefault();

                            if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus2.อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน)
                            {
                                approvalStatus = "1";
                                isSendApprovedMail = true;
                                requestBudget = (decimal)genInfo.BudgetValue;
                                approvedBudget = (decimal)genInfo.BudgetValue;

                                newBudgetRevise = genInfo.BudgetValue;
                                //genInfo.BudgetReviseValue = genInfo.BudgetValue;
                            }
                            else if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus2.อนุมัติ_ปรับลดวงเงินสนับสนุน)
                            {

                                approvalStatus = "1";

                                isSendApprovedMail = true;
                                requestBudget = (decimal)genInfo.BudgetValue;
                                approvedBudget = (decimal)model.ApprovalBudget2;

                                newBudgetRevise = model.ApprovalBudget2;
                                //genInfo.BudgetReviseValue = model.ApprovalBudget2;
                            }
                            else
                            {
                                isSendNotApprovedMail = true;
                                approvalStatus = "0";
                                //genInfo.BudgetReviseValue = 0;
                            }

                            if (approvalStatus == "1")
                            {
                                isCreateProjectNo = true;
                                newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน, Common.LOVGroup.ProjectApprovalStatus).LOVID;

                            }
                            else
                            {
                                //kenghot

                                //newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                                decimal? l = GetProjApprovalStatusByApprover(projectApprovalStatus);
                                if (l.HasValue)
                                    newProjectStatus = l.Value;
                                else
                                    newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                                //end kenghot
                            }


                        }
                        else
                        {
                            bool isStep4Approved = false;
                            projectApprovalStatus = _db.MT_ListOfValue.Where(x => x.LOVID == (decimal)model.ApprovalStatusID1).FirstOrDefault();

                            if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน)
                            {
                                isStep4Approved = true;
                                newBudgetRevise = genInfo.BudgetValue;
                                //genInfo.BudgetReviseValue = genInfo.BudgetValue;
                            }
                            else if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ปรับลดวงเงินสนับสนุน)
                            {
                                isStep4Approved = true;
                                newBudgetRevise = model.ApprovalBudget2;
                                //genInfo.BudgetReviseValue = model.ApprovalBudget2;
                            }
                            else if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.ไม่อนุมัติ)
                            {
                                isSendNotApprovedMail = true;
                                approvalStatus = "0";
                                //genInfo.BudgetReviseValue = 0;
                            }

                            if (isStep4Approved)
                            {
                                newProjectStatus = step4Status.LOVID;
                            }
                            //kenghot
                            //else if (approvalStatus == "0")
                            else
                            {
                                //kenghot 
                                // newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                                decimal? l = GetProjApprovalStatusByApprover(projectApprovalStatus);
                                if (l.HasValue)
                                    newProjectStatus = l.Value;
                                else
                                    newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                            }

                        }
                        #endregion Center
                    }
                    else
                    {
                        #region Province
                        if ((!String.IsNullOrEmpty(dbApproval.ApprovalNo)) &&
                            (!String.IsNullOrEmpty(dbApproval.ApprovalYear)) &&
                            (dbApproval.ApprovalDate.HasValue))
                        {

                            projectApprovalStatus = _db.MT_ListOfValue.Where(x => x.LOVID == (decimal)model.ApprovalStatusID1).FirstOrDefault();
                            if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน)
                            {

                                approvalStatus = "1";

                                isSendApprovedMail = true;
                                requestBudget = (decimal)genInfo.BudgetValue;
                                approvedBudget = (decimal)genInfo.BudgetValue;
                                newBudgetRevise = genInfo.BudgetValue;
                                //genInfo.BudgetReviseValue = genInfo.BudgetValue;
                            }
                            else if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.อนุมัติ_ปรับลดวงเงินสนับสนุน)
                            {

                                approvalStatus = "1";

                                isSendApprovedMail = true;
                                requestBudget = (decimal)genInfo.BudgetValue;
                                approvedBudget = (decimal)model.ApprovalBudget1;
                                newBudgetRevise = model.ApprovalBudget1;
                                //genInfo.BudgetReviseValue = model.ApprovalBudget1;
                            }
                            else if (projectApprovalStatus.LOVCode == Common.LOVCode.Approvalstatus1.ไม่อนุมัติ)
                            {
                                approvalStatus = "0";
                                isSendNotApprovedMail = true;
                                //genInfo.BudgetReviseValue = 0;
                            }

                            if (approvalStatus == "1")
                            {
                                isCreateProjectNo = true;
                                newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                                //genInfo.ProjectApprovalStatusID = status.LOVID;
                            }
                            //kenghot
                            // else if (approvalStatus == "0")
                            else
                            {
                                decimal? l = GetProjApprovalStatusByApprover(projectApprovalStatus);
                                if (l.HasValue)
                                    newProjectStatus = l.Value;
                                else
                                    newProjectStatus = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด, Common.LOVGroup.ProjectApprovalStatus).LOVID;
                                //genInfo.ProjectApprovalStatusID = status.LOVID;
                            }

                            #endregion Province;
                        }
                    }

                    if ((oldProjectStatus != newProjectStatus) || (oldBudgetRevise != newBudgetRevise))
                    {
                        genInfo.ProjectApprovalStatusID = newProjectStatus;
                        genInfo.BudgetReviseValue = newBudgetRevise;
                        genInfo.UpdatedBy = _user.UserName;
                        genInfo.UpdatedByID = _user.UserID;
                        genInfo.UpdatedDate = DateTime.Now;
                    }


                    #endregion Update Project Approval Status


                    #region Update ProjectNo
                    DBModels.Model.ProjectInformation proInfo = _db.ProjectInformations.Where(x => x.ProjectID == model.ProjectID).SingleOrDefault();

                    String projectNo = model.ProjectNo;
                    if (isCreateProjectNo && (newProjectStatus != oldProjectStatus) && (String.IsNullOrEmpty(projectNo)))
                    {

                        if (approvalYear > 2016)
                        {
                            var projectNoResult = _runningService.GetProjectProjectNo(model.ProjectID, (DateTime)model.ApprovalDate, Int32.Parse(model.ApprovalNo), approvalYear);
                            if (projectNoResult.IsCompleted)
                            {
                                projectNo = projectNoResult.Data;
                            }
                            else
                            {
                                throw new Exception(projectNoResult.Message[0]);
                            }
                        }
                    }

                    if (proInfo.ProjectNo != projectNo)
                    {
                        if ((!String.IsNullOrEmpty(projectNo)) && !CheckProjectNo(projectNo, proInfo.ProjectID))
                        {
                            String msg = String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectInfo_ProjectNo1);
                            result.IsCompleted = false;
                            result.Message.Add(msg);
                            return result;
                        }

                        proInfo.ProjectNo = projectNo;
                        proInfo.UpdatedBy = _user.UserName;
                        proInfo.UpdatedByID = _user.UserID;
                        proInfo.UpdatedDate = DateTime.Now;
                    }
                    #endregion Update ProjectNo               


                    dbApproval.ApprovalStatus = approvalStatus;

                    SaveApprovalBudgetDetail(model.BudgetDetails);

                    #region Inset project log
                    if (projectApprovalID > 0)
                    {
                        newValue.ProjectApprovalStatusID = newProjectStatus;
                        newValue.ProjectNo = proInfo.ProjectNo;
                        newValue.BudgetReviseValue = newBudgetRevise;

                        DBModels.Model.ProjectLog proLog = new DBModels.Model.ProjectLog();
                        proLog.ProjectID = model.ProjectID;
                        proLog.OldValue = Nep.Project.Common.Web.WebUtility.ToJSON(oldValue);
                        proLog.NewValue = Nep.Project.Common.Web.WebUtility.ToJSON(newValue);
                        proLog.UpdatedBy = dbApproval.UpdatedBy;
                        proLog.UpdatedByID = (decimal)_user.UserID;
                        proLog.UpdatedTime = dbApproval.UpdatedDate.Value;
                        _db.ProjectLogs.Add(proLog);
                    }
                    #endregion Inset project log

                    isTransCommit = true;
                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(model.ProjectID, "3", _user.UserID.Value, model.ipAddress));
                    _db.SaveChanges();
                    tran.Commit();

                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                }

                #region Send E-Mail and SMS
                try
                {
                    if ((approvalYear > 2016) && (oldProjectStatus != newProjectStatus) && isCreateProjectNo)
                    {
                        List<String> telephones = new List<String>();

                        telephones.Add(orgMobile);
                        telephones.Add(personalMobile);


                        if (isSendApprovedMail)
                        {
                            string sms = String.Format(Nep.Project.Resources.Message.ProjectApprovedSMS, projectName);
                            _mailService.SendProjectApprovalNotify(model.ProjectID, requestBudget, approvedBudget);
                            _smsService.Send(sms, telephones.ToArray());
                        }
                        else if (isSendNotApprovedMail)
                        {
                            string sms = String.Format(Nep.Project.Resources.Message.ProjectNotApprovedSMS, projectName);
                            _mailService.SendProjectNotApprovalNotify(model.ProjectID);
                            _smsService.Send(sms, telephones.ToArray());
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.IsCompleted = true;
                    result.Message.Clear();
                    result.Message.Add(Nep.Project.Resources.Message.SaveSuccessWithError);
                    result.Message.Add(ex.Message);
                }
                #endregion Send E-Mail and SMS

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private bool CheckProjectNo(String projectNo, Decimal projectId)
        {
            int count = _db.ProjectInformations.Where(x => x.ProjectID != projectId && (x.ProjectNo == projectNo)).Count();
            return (count == 0);
        }

        private bool CheckContractNo(String contractNo, Decimal projectId)
        {
            int count = _db.ProjectContracts.Where(x => x.ProjectID != projectId && (x.ContractNo == contractNo)).Count();
            return (count == 0);
        }

        private void SaveApprovalBudgetDetail(List<ServiceModels.ProjectInfo.BudgetDetail> budgetDetails)
        {
            ServiceModels.ProjectInfo.BudgetDetail budget;
            DBModels.Model.ProjectBudget dbBudget;
            for (int i = 0; i < budgetDetails.Count; i++)
            {
                budget = budgetDetails[i];
                dbBudget = _db.ProjectBudgets.Where(x => x.ProjectBudgetID == budget.ProjectBudgetID).FirstOrDefault();
                if (dbBudget != null)
                {
                    dbBudget.BudgetReviseValue1 = (budget.Revise1Amount.HasValue) ? (decimal)budget.Revise1Amount : 0;
                    dbBudget.BudgetReviseValue2 = (budget.Revise2Amount.HasValue) ? (decimal)budget.Revise2Amount : 0;
                    dbBudget.RemarkApproval = budget.ApprovalRemark;
                    dbBudget.UpdatedBy = _user.UserName;
                    dbBudget.UpdatedByID = _user.UserID;
                    dbBudget.UpdatedDate = DateTime.Now;
                }
                _db.SaveChanges();
            }
        }

        private ServiceModels.ProjectInfo.ProjectApprovalLog GetCurrentProjectApproval(decimal projectID)
        {
            ServiceModels.ProjectInfo.ProjectApprovalLog oldValue;
            oldValue = (from p in _db.ProjectApprovals
                        where p.ProjectID == projectID
                        select new ServiceModels.ProjectInfo.ProjectApprovalLog
                        {
                            ProjectApprovalStatusID = p.ProjectGeneralInfo.ProjectApprovalStatusID,
                            ProjectNo = p.ProjectGeneralInfo.ProjectInformation.ProjectNo,
                            BudgetReviseValue = p.ProjectGeneralInfo.BudgetReviseValue,

                            ApprovalStatusID1 = p.ApprovalStatusID1,
                            ApprovalBudget1 = p.ApprovalBudget1,
                            ApprovalName1 = p.ApprovalName1,
                            ApprovalLastName1 = p.ApprovalLastName1,
                            ApproverPosition1 = p.ApproverPosition1,
                            ApprovalDate1 = p.ApprovalDate1,

                            ApprovalStatusID2 = p.ApprovalStatusID2,
                            ApprovalBudget2 = p.ApprovalBudget2,
                            ApprovalName2 = p.ApprovalName2,
                            ApprovalLastName2 = p.ApprovalLastName2,
                            ApproverPosition2 = p.ApproverPosition2,
                            ApprovalDate2 = p.ApprovalDate2,

                            ApprovalNo = p.ApprovalNo,
                            ApprovalYear = p.ApprovalYear,
                            ApprovalDate = p.ApprovalDate,
                        }).FirstOrDefault();

            if (oldValue != null)
            {
                oldValue.BudgetDetails = (from pb in _db.ProjectBudgets
                                          where pb.ProjectID == projectID
                                          select new ServiceModels.ProjectInfo.BudgetDetailApprovalLog
                                          {
                                              ProjectBudgetID = pb.ProjectBudgetID,
                                              BudgetValue = pb.BudgetValue,
                                              BudgetReviseValue = pb.BudgetReviseValue,
                                              BudgetReviseValue1 = pb.BudgetReviseValue1,
                                              BudgetReviseValue2 = pb.BudgetReviseValue2
                                          }).OrderBy(or => or.ProjectBudgetID).ToList();
            }
            else
            {
                oldValue = new ServiceModels.ProjectInfo.ProjectApprovalLog();
            }

            return oldValue;
        }

        private ServiceModels.ProjectInfo.ProjectApprovalLog GetNewValueOfProjectApproval(ServiceModels.ProjectInfo.ProjectApprovalResult model)
        {

            ServiceModels.ProjectInfo.ProjectApprovalLog newValue = new ServiceModels.ProjectInfo.ProjectApprovalLog();

            newValue.ApprovalStatusID1 = model.ApprovalStatusID1.Value;
            newValue.ApprovalBudget1 = model.ApprovalBudget1;
            newValue.ApprovalName1 = model.ApprovalName1;
            newValue.ApprovalLastName1 = model.ApprovalLastName1;
            newValue.ApproverPosition1 = model.ApproverPosition1;
            newValue.ApprovalDate1 = model.ApprovalDate1;

            newValue.ApprovalStatusID2 = model.ApprovalStatusID2;
            newValue.ApprovalBudget2 = model.ApprovalBudget2;
            newValue.ApprovalName2 = model.ApprovalName2;
            newValue.ApprovalLastName2 = model.ApprovalLastName2;
            newValue.ApproverPosition2 = model.ApproverPosition2;
            newValue.ApprovalDate2 = model.ApprovalDate2;

            newValue.ApprovalNo = model.ApprovalNo;
            newValue.ApprovalYear = model.ApprovalYear;
            newValue.ApprovalDate = model.ApprovalDate;

            newValue.BudgetDetails = (from pb in model.BudgetDetails
                                      select new ServiceModels.ProjectInfo.BudgetDetailApprovalLog
                                      {
                                          ProjectBudgetID = pb.ProjectBudgetID,
                                          BudgetValue = pb.Amount.Value,
                                          BudgetReviseValue = pb.ReviseAmount.Value,
                                          BudgetReviseValue1 = pb.Revise1Amount.Value,
                                          BudgetReviseValue2 = pb.Revise2Amount.Value
                                      }).OrderBy(or => or.ProjectBudgetID).ToList();

            return newValue;
        }
        #endregion ProjectApprovalResult
        #region Questionare
        public ServiceModels.ReturnObject<string> GetQNData(decimal projectID, string qGroup)
        {
            ServiceModels.ReturnObject<string> result = new ReturnObject<string>();

            try
            {
                var data = (from pro in _db.PROJECTQUESTIONHDs
                            where pro.PROJECTID == projectID && pro.QUESTGROUP == qGroup
                            select pro.DATA).FirstOrDefault();



                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Questionare", ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<string> SaveQNData(decimal projectID, string qGroup, string isReported, string QNData)
        {
            ServiceModels.ReturnObject<string> result = new ReturnObject<string>();

            try
            {
                var data = (from pro in _db.PROJECTQUESTIONHDs
                            where pro.PROJECTID == projectID && pro.QUESTGROUP == qGroup
                            select pro).FirstOrDefault();

                if (data == null)
                {
                    data = new PROJECTQUESTIONHD();
                    _db.PROJECTQUESTIONHDs.Add(data);
                    data.PROJECTID = projectID;
                    data.CREATEDBY = _user.UserName;
                    data.CREATEDBYID = _user.UserID.HasValue ? _user.UserID.Value : 0;
                    data.CREATEDDATE = DateTime.Now;

                    data.QUESTGROUP = qGroup;


                }
                data.UPDATEDBY = _user.UserName;
                data.UPDATEDBYID = _user.UserID.HasValue ? _user.UserID.Value : 0;
                data.UPDATEDDATE = DateTime.Now;
                data.ISREPORTED = isReported;
                data.DATA = QNData;
                _db.SaveChanges();
                result.IsCompleted = true;
                result.Data = "";
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Questionare", ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.Questionare>> GetProjectQuestionare(decimal projectID, string qGroup)
        {
            ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.Questionare>> result = new ReturnObject<List<Questionare>>();

            try
            {
                var data = from pro in _db.PROJECTQUESTIONs
                           where pro.PROJECTQUESTIONHD.PROJECTID == projectID && pro.PROJECTQUESTIONHD.QUESTGROUP == qGroup
                           select new ServiceModels.ProjectInfo.Questionare
                           {
                               QField = pro.QFIELD,
                               QType = pro.QTYPE,
                               QValue = pro.QVALUE
                           };



                result.IsCompleted = true;
                result.Data = data.ToList();
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Questionare", ex);
            }
            return result;
        }

        #endregion
        #region ProjectReportResult
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectReportResult> GetProjectReportResult(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectReportResult> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectReportResult>();
            //String followupStatus = (_user.is
            var att = new Business.AttachmentService(_db);
            try
            {
                ServiceModels.ProjectInfo.ProjectReportResult data = (from pro in _db.ProjectGeneralInfoes
                                                                      where pro.ProjectID == projectID
                                                                      select new ServiceModels.ProjectInfo.ProjectReportResult
                                                                      {
                                                                          ProjectID = pro.ProjectID,
                                                                          OrganizationID = pro.OrganizationID,
                                                                          ApprovalStatus = (pro.ProjectApproval != null) ? pro.ProjectApproval.ApprovalStatus : null,
                                                                          ContractYear = (pro.ProjectContract != null) ? pro.ProjectContract.ContractYear : (decimal?)null,

                                                                          FollowupStatusID = pro.FollowUpStatus,
                                                                          FollowupStatusCode = (pro.FollowUpStatus != null) ? _db.MT_ListOfValue.Where(x => x.LOVID == pro.FollowUpStatus).Select(y => y.LOVCode).FirstOrDefault() : null,
                                                                          BudgetAmount = pro.BudgetValue,
                                                                          ReviseBudgetAmount = pro.BudgetReviseValue,
                                                                          ActivityDescription = (pro.ProjectReport != null) ? pro.ProjectReport.ActivityDescription : null,
                                                                          MaleParticipant = (pro.ProjectReport != null) ? pro.ProjectReport.MaleParticipant : 0,
                                                                          FemaleParticipant = (pro.ProjectReport != null) ? pro.ProjectReport.FemaleParticipant : 0,
                                                                          ActualExpense = (pro.ProjectReport != null) ? pro.ProjectReport.ActualExpense : (decimal?)null,
                                                                          Benefit = (pro.ProjectReport != null) ? pro.ProjectReport.Benefit : null,
                                                                          ProblemsAndObstacle = (pro.ProjectReport != null) ? pro.ProjectReport.ProblemsAndObstacle : null,
                                                                          Suggestion = (pro.ProjectReport != null) ? pro.ProjectReport.Suggestion : null,
                                                                          OperationResult = (pro.ProjectReport != null) ? pro.ProjectReport.OperationResult : null,
                                                                          OperationLevel = (pro.ProjectReport != null) ? pro.ProjectReport.OperationLevel : null,
                                                                          OperationLevelDesc = (pro.ProjectReport != null) ? pro.ProjectReport.OperationLevelDesc : null,
                                                                          ReporterName1 = (pro.ProjectReport != null) ? pro.ProjectReport.ReporterName1 : null,
                                                                          ReporterLastname1 = (pro.ProjectReport != null) ? pro.ProjectReport.ReporterLastname1 : null,
                                                                          Position1 = (pro.ProjectReport != null) ? pro.ProjectReport.Position1 : null,
                                                                          RepotDate1 = (pro.ProjectReport != null) ? pro.ProjectReport.RepotDate1 : (DateTime?)null,
                                                                          Telephone1 = (pro.ProjectReport != null) ? pro.ProjectReport.Telephone1 : null,
                                                                          SuggestionDesc = (pro.ProjectReport != null) ? pro.ProjectReport.SuggestionDesc : null,
                                                                          ReporterName2 = (pro.ProjectReport != null) ? pro.ProjectReport.ReporterName2 : null,
                                                                          ReporterLastname2 = (pro.ProjectReport != null) ? pro.ProjectReport.ReporterLastname2 : null,
                                                                          Position2 = (pro.ProjectReport != null) ? pro.ProjectReport.Position2 : null,
                                                                          RepotDate2 = (pro.ProjectReport != null) ? pro.ProjectReport.RepotDate2 : (DateTime?)null,
                                                                          Telephone2 = (pro.ProjectReport != null) ? pro.ProjectReport.Telephone2 : null,
                                                                          ReportAttachmentID = (pro.ProjectReport != null) ? pro.ProjectReport.ReportAttachmentID : null,

                                                                          ProjectApprovalStatusID = pro.ProjectApprovalStatusID,
                                                                          ProjectApprovalStatusCode = (pro.ProjectApprovalStatus != null) ? pro.ProjectApprovalStatus.LOVCode : null,
                                                                          CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == pro.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault(),
                                                                          ProvinceID = pro.ProvinceID,
                                                                          Interest = pro.ProjectReport.INTEREST

                                                                      }).FirstOrDefault();

                if (data != null)
                {
                    //Participants
                    List<ServiceModels.ProjectInfo.ProjectParticipant> participants = (from participant in _db.ProjectParticipants
                                                                                       where participant.ProjectID == projectID
                                                                                       select new ServiceModels.ProjectInfo.ProjectParticipant
                                                                                       {
                                                                                           ProjectParticipantID = participant.ProjectParticipantsID,
                                                                                           FirstName = participant.FirstName,
                                                                                           LastName = participant.LastName,
                                                                                           IDCardNo = participant.IDCardNo,
                                                                                           Gender = participant.Gender,
                                                                                           IsCripple = participant.IsCripple,
                                                                                           ProjectTargetGroupID = participant.ProjectTargetGroupID,
                                                                                           TargetGroupID = participant.TargetGroupID,
                                                                                           TargetGroupName = participant.TargetGroup.LOVName,
                                                                                           TargetGroupCode = participant.TargetGroup.LOVCode,
                                                                                           TargetGroupEtc = participant.TargetGroupETC


                                                                                       }).ToList();
                    foreach (ServiceModels.ProjectInfo.ProjectParticipant p in participants)
                    {
                        if (int.Parse(p.IsCripple) > 1)
                        {
                            p.TargetGroupID = 0;
                            p.TargetGroupName = "";
                        }
                    }
                    data.Participants = participants;

                    //Attachment
                    if (data.ReportAttachmentID.HasValue)
                    {
                        //kenghot18
                        _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                        {
                            ATTACHMENTID = data.ReportAttachmentID.Value,
                            FIELDNAME = REPORT_BUDGET,
                            TABLENAME = TABLE_REPORT,
                            TABLEROWID = projectID
                        });
                        var edit = (from e in _db.ProjectReports where e.ProjectID == projectID select e).FirstOrDefault();
                        edit.ReportAttachmentID = null;

                        _db.SaveChanges();
                        //

                        //kenghot18
                        //DBModels.Model.MT_Attachment dbAttachment = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.ReportAttachmentID).FirstOrDefault();
                        //if (dbAttachment != null)
                        //{
                        //    data.ReportAttachment = new ServiceModels.KendoAttachment()
                        //    {
                        //        id = dbAttachment.AttachmentID.ToString(),
                        //        name = dbAttachment.AttachmentFilename,
                        //        extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                        //        size = (int)dbAttachment.FileSize
                        //    };
                        //}
                    }
                    //kenghot18

                    data.ReportAttachments = att.GetAttachmentOfTable(TABLE_REPORT, REPORT_BUDGET, projectID);
                    data.ActivityAttachments = att.GetAttachmentOfTable(TABLE_REPORT, REPORT_ACTIVITY, projectID);
                    data.ParticipantAttachments = att.GetAttachmentOfTable(TABLE_REPORT, REPORT_PARTICIPANT, projectID);
                    data.ResultAttachments = att.GetAttachmentOfTable(TABLE_REPORT, REPORT_RESULT, projectID);
                    //

                }

                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<decimal?> SaveDocument(decimal projID, string QNGroup, string data)
        {
            var result = new ReturnObject<decimal?>();
            try
            {

                PROJECTQUESTIONHD doc = null;

                doc = _db.PROJECTQUESTIONHDs.Where(w => w.QUESTHDID == projID && w.QUESTGROUP == QNGroup).FirstOrDefault();
                if (doc == null)
                {
                    doc = new PROJECTQUESTIONHD
                    {
                        CREATEDBY = "system",
                        CREATEDBYID = 1,
                        CREATEDDATE = DateTime.Now,
                        ISREPORTED = "1",
                        PROJECTID = projID,
                        QUESTGROUP = QNGroup
                    };
                    _db.PROJECTQUESTIONHDs.Add(doc);
                }
                doc.UPDATEDBY = "system";
                doc.UPDATEDBYID = 1;
                doc.UPDATEDDATE = DateTime.Now;
                doc.DATA = data;
                _db.SaveChanges();
                result.Data = doc.QUESTHDID;
                result.IsCompleted = true;


            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<decimal?> InsertDocument(decimal projID, string QNGroup, string data)
        {
            var result = new ReturnObject<decimal?>();
            try
            {

                PROJECTQUESTIONHD doc = null;


                doc = new PROJECTQUESTIONHD
                {
                    CREATEDBY = "system",
                    CREATEDBYID = 1,
                    CREATEDDATE = DateTime.Now,
                    ISREPORTED = "1",
                    PROJECTID = projID,
                    QUESTGROUP = QNGroup
                };
                _db.PROJECTQUESTIONHDs.Add(doc);

                doc.UPDATEDBY = "system";
                doc.UPDATEDBYID = 1;
                doc.UPDATEDDATE = DateTime.Now;
                doc.DATA = data;
                _db.SaveChanges();
                result.Data = doc.QUESTHDID;
                result.IsCompleted = true;


            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<string> GetDocumentByKey(decimal projID, string QNGroup)
        {
            var result = new ReturnObject<string>();
            try
            {

                var doc = _db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == projID && w.QUESTGROUP == QNGroup).FirstOrDefault();
                if (doc == null)
                {
                    result.Message.Add("ไม่พบข้อมูล");
                    return result;
                }
                result.Data = doc.DATA;
                result.IsCompleted = true;


            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        public ServiceModels.ReturnObject<PROJECTQUESTIONHD> GetDocumentByDocId(decimal docID, string QNGroup)
        {
            var result = new ReturnObject<PROJECTQUESTIONHD>();
            try
            {

                var doc = _db.PROJECTQUESTIONHDs.Where(w => w.QUESTHDID == docID && w.QUESTGROUP == QNGroup).FirstOrDefault();
                if (doc == null)
                {
                    result.Message.Add("ไม่พบข้อมูล");
                    return result;
                }
                result.Data = doc;
                result.IsCompleted = true;


            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        public ServiceModels.ReturnMessage SaveProjectQuestionareResult(decimal projID, string QNGroup, string controls, bool isSaveOfficerReport, bool isSendReport, string ipAddress)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                var qn = (from qs in _db.PROJECTQUESTIONHDs where qs.PROJECTID == projID && qs.QUESTGROUP == QNGroup select qs).FirstOrDefault();
                if (qn == null)
                {
                    qn = new PROJECTQUESTIONHD();
                    _db.PROJECTQUESTIONHDs.Add(qn);
                    qn.CREATEDBY = _user.UserName;
                    qn.CREATEDBYID = _user.UserID.Value;
                    qn.CREATEDDATE = DateTime.Now;
                    qn.ISREPORTED = (isSendReport) ? "1" : "0";
                    qn.PROJECTID = projID;
                    qn.QUESTGROUP = QNGroup;

                }
                else
                {
                    if (isSendReport) qn.ISREPORTED = "1";
                    qn.UPDATEDBY = _user.UserName;
                    qn.UPDATEDBYID = _user.UserID.Value;
                    qn.UPDATEDDATE = DateTime.Now;
                }
                var q = qn.PROJECTQUESTIONs;
                Dictionary<string, string> jv = new Dictionary<string, string>();
                Newtonsoft.Json.Linq.JObject jo = new Newtonsoft.Json.Linq.JObject();
                jo = Newtonsoft.Json.Linq.JObject.Parse(controls);
                foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> j in jo)
                {
                    var tmp = q.Where(w => w.QFIELD == j.Key).FirstOrDefault();

                    if (tmp == null)
                    {
                        tmp = new PROJECTQUESTION();
                        tmp.QFIELD = j.Key;
                        tmp.QTYPE = "X";
                        qn.PROJECTQUESTIONs.Add(tmp);


                    }
                    tmp.QVALUE = j.Value.ToString();
                }
                if (QNGroup == "SATISFY")
                {
                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(projID, "6", _user.UserID.Value, ipAddress));
                }
                if (QNGroup == "SELF")
                {
                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(projID, "7", _user.UserID.Value, ipAddress));
                }
                _db.SaveChanges();
                result.IsCompleted = true;
                if (isSendReport)
                {
                    result.Message.Add(Nep.Project.Resources.Message.SendProjectReportResult);
                }
                else
                {
                    result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Questionare Satisfy", ex);
            }
            return result;



        }
        public ServiceModels.ReturnObject<bool> SaveAttachFile(decimal projectID, string attachType, List<KendoAttachment> removeFiles, List<KendoAttachment> addFiles, string tableName, string fieldName)
        {
            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + projectID + "\\";
            decimal attachmentTypeID = GetAttachmentTypeID(attachType);
            var result = new ServiceModels.ReturnObject<bool>();
            try
            {
                result.IsCompleted = true;
                if (removeFiles != null)
                {
                    foreach (KendoAttachment k in removeFiles)
                    {
                        RemoveFile(k, rootDestinationFolderPath);
                    }

                }

                if (addFiles != null)
                {
                    foreach (KendoAttachment k in addFiles)
                    {
                        var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                        _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                        {
                            ATTACHMENTID = attID.Value,
                            FIELDNAME = fieldName,
                            TABLENAME = tableName,
                            TABLEROWID = projectID
                        });
                    }
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }
        public ServiceModels.ReturnMessage SaveProjectReportResult(ServiceModels.ProjectInfo.ProjectReportResult model, bool isSaveOfficerReport, bool isSendReport)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
                decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_PERSONAL);

                using (var tran = _db.Database.BeginTransaction())
                {
                    DBModels.Model.ProjectReport dbReport = _db.ProjectReports.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                    if (dbReport == null)
                    {
                        dbReport = new DBModels.Model.ProjectReport();
                    }

                    dbReport.ActivityDescription = model.ActivityDescription;
                    dbReport.MaleParticipant = model.MaleParticipant;
                    dbReport.FemaleParticipant = model.FemaleParticipant;
                    dbReport.ActualExpense = model.ActualExpense;
                    dbReport.Benefit = model.Benefit;
                    dbReport.ProblemsAndObstacle = model.ProblemsAndObstacle;
                    dbReport.Suggestion = model.Suggestion;
                    dbReport.OperationResult = model.OperationResult;
                    dbReport.OperationLevel = model.OperationLevel;
                    dbReport.OperationLevelDesc = model.OperationLevelDesc;
                    dbReport.ReporterName1 = model.ReporterName1;
                    dbReport.ReporterLastname1 = model.ReporterLastname1;
                    dbReport.Position1 = model.Position1;
                    dbReport.RepotDate1 = model.RepotDate1;
                    dbReport.Telephone1 = model.Telephone1;
                    dbReport.INTEREST = model.Interest;
                    if (isSaveOfficerReport)
                    {
                        dbReport.SuggestionDesc = model.SuggestionDesc;
                        dbReport.ReporterName2 = model.ReporterName2;
                        dbReport.ReporterLastname2 = model.ReporterLastname2;
                        dbReport.Position2 = model.Position2;
                        dbReport.RepotDate2 = model.RepotDate2;
                        dbReport.Telephone2 = model.Telephone2;
                    }


                    //Attachment 

                    if (model.RemovedReportAttachment != null)
                    {
                        //kenghot18
                        //RemoveFile(model.RemovedReportAttachment, rootDestinationFolderPath);
                        //dbReport.ReportAttachmentID = (decimal?)null;
                        foreach (KendoAttachment k in model.RemovedReportAttachments)
                        {
                            RemoveFile(k, rootDestinationFolderPath);
                        }
                    }

                    if (model.AddedReportAttachment != null)
                    {
                        //kenghot18
                        //dbReport.ReportAttachmentID = SaveFile(model.AddedReportAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                        foreach (KendoAttachment k in model.AddedReportAttachments)
                        {
                            var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                            {
                                ATTACHMENTID = attID.Value,
                                FIELDNAME = REPORT_BUDGET,
                                TABLENAME = TABLE_REPORT,
                                TABLEROWID = model.ProjectID
                            });
                        }
                    }



                    if (model.RemovedActivityAttachments != null)
                    {
                        foreach (KendoAttachment k in model.RemovedActivityAttachments)
                        {
                            RemoveFile(k, rootDestinationFolderPath);
                        }

                    }

                    if (model.AddedActivityAttachments != null)
                    {
                        foreach (KendoAttachment k in model.AddedActivityAttachments)
                        {
                            var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                            {
                                ATTACHMENTID = attID.Value,
                                FIELDNAME = REPORT_ACTIVITY,
                                TABLENAME = TABLE_REPORT,
                                TABLEROWID = model.ProjectID
                            });
                        }
                    }

                    if (model.RemovedParticipantAttachments != null)
                    {
                        foreach (KendoAttachment k in model.RemovedParticipantAttachments)
                        {
                            RemoveFile(k, rootDestinationFolderPath);
                        }

                    }

                    if (model.AddedParticipantAttachments != null)
                    {
                        foreach (KendoAttachment k in model.AddedParticipantAttachments)
                        {
                            var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                            {
                                ATTACHMENTID = attID.Value,
                                FIELDNAME = REPORT_PARTICIPANT,
                                TABLENAME = TABLE_REPORT,
                                TABLEROWID = model.ProjectID
                            });
                        }
                    }

                    if (model.RemovedResultAttachments != null)
                    {
                        foreach (KendoAttachment k in model.RemovedResultAttachments)
                        {
                            RemoveFile(k, rootDestinationFolderPath);
                        }

                    }

                    if (model.AddedResultAttachments != null)
                    {
                        foreach (KendoAttachment k in model.AddedResultAttachments)
                        {
                            var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                            {
                                ATTACHMENTID = attID.Value,
                                FIELDNAME = REPORT_RESULT,
                                TABLENAME = TABLE_REPORT,
                                TABLEROWID = model.ProjectID
                            });
                        }
                    }

                    //Participant
                    SaveProjectParticipant(model.Participants, model.ProjectID);

                    if (dbReport.ProjectID > 0)
                    {
                        dbReport.UpdatedBy = _user.UserName;
                        dbReport.UpdatedByID = _user.UserID;
                        dbReport.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbReport.ProjectID = model.ProjectID;
                        dbReport.CreatedBy = _user.UserName;
                        dbReport.CreatedByID = (decimal)_user.UserID;
                        dbReport.CreatedDate = DateTime.Now;
                        _db.ProjectReports.Add(dbReport);
                    }

                    if (isSendReport)
                    {
                        DBModels.Model.MT_ListOfValue followupStatus = GetListOfValue(Common.LOVCode.Followupstatus.รายงานผลแล้ว, Common.LOVGroup.FollowupStatus);
                        DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                        genInfo.FollowUpStatus = followupStatus.LOVID;
                        genInfo.LastedFollowupDate = DateTime.Today;
                        genInfo.UpdatedBy = _user.UserName;
                        genInfo.UpdatedByID = _user.UserID;
                        genInfo.UpdatedDate = DateTime.Now;
                        dbReport.REVISECOMMENT = "";
                    }

                    foreach (var bg in model.BudgetDetails)
                    {
                        var b = (from bs in _db.ProjectBudgets where bs.ProjectBudgetID == bg.ProjectBudgetID select bs).FirstOrDefault();
                        if (b != null)
                        {
                            b.ACTUALEXPENSE = bg.ActualExpense.Value;
                        }
                    }

                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(model.ProjectID, "8", _user.UserID.Value, model.IPAddress));
                    _db.SaveChanges();
                    tran.Commit();

                    result.IsCompleted = true;
                    if (isSendReport)
                    {
                        _mailService.SendProjectConfirmNotify(model.ProjectID);
                        _smsService.Send("มีการยื่นคำร้องโครงการเข้ามาใหม่", _db, model.ProjectID);
                        result.Message.Add(Nep.Project.Resources.Message.SendProjectReportResult);
                    }
                    else
                    {
                        result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                    }

                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnMessage SaveOfficerProjectReport(ServiceModels.ProjectInfo.ProjectReportResult model)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.ProjectReport dbReport = _db.ProjectReports.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                if (dbReport != null)
                {
                    dbReport.SuggestionDesc = model.SuggestionDesc;
                    dbReport.ReporterName2 = model.ReporterName2;
                    dbReport.ReporterLastname2 = model.ReporterLastname2;
                    dbReport.Position2 = model.Position2;
                    dbReport.RepotDate2 = model.RepotDate2;
                    dbReport.Telephone2 = model.Telephone2;
                    dbReport.UpdatedBy = _user.UserName;
                    dbReport.UpdatedByID = _user.UserID;
                    dbReport.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();
                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTargetNameList> GetProjectTargetForParticipant(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTargetNameList> result = new ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTargetNameList>();
            try
            {
                List<ServiceModels.ProjectInfo.ProjectTargetNameList> data = (
                        (from targetGroup in _db.MT_ListOfValue
                         join pTargetGroup in _db.ProjectTargetGroups.Where(pt => pt.ProjectID == projectID) on targetGroup.LOVID equals pTargetGroup.TargetGroupID into pTargetGroupTmp
                         from ptg in pTargetGroupTmp.DefaultIfEmpty()
                         where (targetGroup.LOVGroup == Common.LOVGroup.TargetGroup) && ((targetGroup.IsActive == Common.Constants.BOOLEAN_TRUE) || ((targetGroup.IsActive == Common.Constants.BOOLEAN_FALSE) && (ptg != null)))
                         select new ServiceModels.ProjectInfo.ProjectTargetNameList
                         {
                             ProjectParticipantsID = (decimal?)null,
                             ProjectTargetID = ptg.ProjectTargetGroupID,
                             TargetID = targetGroup.LOVID,
                             TargetName = targetGroup.LOVName,
                             TargetEtc = ptg.TargetGroupEtc,
                             LovIsActive = targetGroup.IsActive
                         }).Union(
                            from participant in _db.ProjectParticipants
                            join targetGroup in _db.MT_ListOfValue on participant.TargetGroupID equals targetGroup.LOVID
                            where (participant.ProjectID == projectID) && (participant.ProjectTargetGroupID == null) && (targetGroup.LOVCode == Common.LOVCode.Targetgroup.อื่นๆ)
                            select new ServiceModels.ProjectInfo.ProjectTargetNameList
                            {
                                ProjectParticipantsID = participant.ProjectParticipantsID,
                                ProjectTargetID = (decimal?)null,
                                TargetID = targetGroup.LOVID,
                                TargetName = targetGroup.LOVName,
                                TargetEtc = participant.TargetGroupETC,
                                LovIsActive = targetGroup.IsActive
                            }
                        )

                        ).Distinct().OrderBy(or => or.TargetID).ToList();




                DBModels.Model.MT_ListOfValue lovOther = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.TargetGroup) && (x.LOVCode == Common.LOVCode.Targetgroup.อื่นๆ) && (x.IsActive == Common.Constants.BOOLEAN_TRUE)).FirstOrDefault();
                if (lovOther != null)
                {
                    int otherCount = data.Where(x => x.TargetID == lovOther.LOVID && ((x.ProjectTargetID == null) && (x.ProjectParticipantsID == null))).Count();
                    if (otherCount == 0)
                    {
                        data.Add(new ServiceModels.ProjectInfo.ProjectTargetNameList
                        {
                            ProjectTargetID = (decimal?)null,
                            TargetID = lovOther.LOVID,
                            TargetName = lovOther.LOVName,
                            TargetEtc = "",
                            LovIsActive = Common.Constants.BOOLEAN_TRUE
                        });
                    }
                }

                //from proTarget in _db.ProjectTargetGroups
                //                                                          join targetGroup in _db.MT_ListOfValue on proTarget.TargetGroupID equals targetGroup.LOVID
                //                                                          where proTarget.ProjectID == projectID
                //                                                          select new ServiceModels.ProjectInfo.ProjectTargetNameList
                //                                                          {
                //                                                              ProjectTargetID = proTarget.ProjectTargetGroupID,
                //                                                              TargetID = proTarget.TargetGroupID,
                //                                                              TargetName = targetGroup.LOVName,
                //                                                              TargetEtc = proTarget.TargetGroupEtc
                //                                                          }).Distinct().OrderBy(or => or.ProjectTargetID).ToList();


                //List<ServiceModels.ProjectInfo.ProjectTargetNameList> data = (from proTarget in _db.ProjectTargetGroups
                //           join targetGroup in _db.MT_ListOfValue on proTarget.TargetGroupID equals targetGroup.LOVID
                //           where proTarget.ProjectID == projectID
                //           select new ServiceModels.ProjectInfo.ProjectTargetNameList
                //            {
                //                ProjectTargetID = proTarget.ProjectTargetGroupID,
                //                TargetID = proTarget.TargetGroupID,
                //                TargetName = targetGroup.LOVName,
                //                TargetEtc = proTarget.TargetGroupEtc
                //            }).Distinct().OrderBy(or => or.ProjectTargetID).ToList();


                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetProjectParticipantTargetEtc(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ReturnQueryData<GenericDropDownListData>();
            try
            {
                var data = (from proTarget in _db.ProjectTargetGroups
                            join targetGroup in _db.MT_ListOfValue on proTarget.TargetGroupID equals targetGroup.LOVID
                            where (proTarget.ProjectID == projectID) &&
                                (targetGroup.LOVGroup == Common.LOVGroup.TargetGroup) &&
                                (targetGroup.LOVCode == Common.LOVCode.Targetgroup.อื่นๆ)
                            orderby proTarget.ProjectTargetGroupID ascending
                            select new ServiceModels.GenericDropDownListData
                            {
                                Value = proTarget.TargetGroupEtc,
                                Text = proTarget.TargetGroupEtc
                            }).Distinct().ToList();

                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public String GetProjectTargetGroupEtc(decimal projectID)
        {

            String etc = (from proTarget in _db.ProjectTargetGroups
                          join targetGroup in _db.MT_ListOfValue on proTarget.TargetGroupID equals targetGroup.LOVID
                          where (proTarget.ProjectID == projectID) &&
                              (targetGroup.LOVGroup == Common.LOVGroup.TargetGroup) &&
                              (targetGroup.LOVCode == Common.LOVCode.Targetgroup.อื่นๆ)
                          select proTarget.TargetGroupEtc
                        ).FirstOrDefault();


            return etc;
        }

        private void SaveProjectParticipant(List<ServiceModels.ProjectInfo.ProjectParticipant> participants, decimal projectID)
        {
            if (participants != null)
            {
                List<DBModels.Model.ProjectParticipant> oldParticipants = _db.ProjectParticipants.Where(x => x.ProjectID == projectID).ToList();
                _db.ProjectParticipants.RemoveRange(oldParticipants);

                List<DBModels.Model.ProjectParticipant> newParticipants = new List<DBModels.Model.ProjectParticipant>();
                ServiceModels.ProjectInfo.ProjectParticipant newItem;
                for (int i = 0; i < participants.Count; i++)
                {
                    newItem = participants[i];
                    newParticipants.Add(new DBModels.Model.ProjectParticipant
                    {
                        ProjectID = projectID,
                        FirstName = newItem.FirstName,
                        LastName = newItem.LastName,
                        IDCardNo = newItem.IDCardNo,
                        Gender = newItem.Gender,
                        IsCripple = newItem.IsCripple,
                        ProjectTargetGroupID = (newItem.ProjectTargetGroupID.HasValue && newItem.ProjectTargetGroupID > 0) ? newItem.ProjectTargetGroupID : null,
                        TargetGroupID = (decimal)newItem.TargetGroupID,
                        TargetGroupETC = newItem.TargetGroupEtc,
                    });
                }
                _db.ProjectParticipants.AddRange(newParticipants);
            }
        }
        #endregion ProjectReportResult

        /**ปีงบประมาณ เริ่มที่ปี 2557*/
        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ApprovalYear()
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                result.IsCompleted = true;
                int startYear = 2014;
                int currentYear = DateTime.Today.Year;
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                String year;
                for (int i = (startYear - 1); i < currentYear; i++)
                {
                    year = (startYear + 543).ToString();
                    list.Add(new ServiceModels.GenericDropDownListData() { Value = year, Text = year });
                    startYear++;
                }

                result.Data = list;

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        private DBModels.Model.MT_ListOfValue GetApprovalBudgetByProjecID(Decimal ProjectID)
        {
            var ret = (from a in _db.ProjectApprovals where a.ProjectID == ProjectID select a).FirstOrDefault();
            if (ret != null)
            {

                return GetListOfValueByKey(ret.ApprovalStatusID1);


            }
            else
                return null;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectInfoList> ListProjectInfoList(ServiceModels.QueryParameter p, bool isCountOnly)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectInfoList> result = new ReturnQueryData<ServiceModels.ProjectInfo.ProjectInfoList>();
            try
            {
                decimal? searcherOganizationID = _user.OrganizationID;
                decimal? searcherGroupID = _user.UserGroupID;
                string searcherGroupCode = _user.UserGroupCode;
                string searcherName = _user.UserName;

                result = (from proj in _db.View_ProjectList
                          select new ServiceModels.ProjectInfo.ProjectInfoList
                          {
                              ProjectInfoID = proj.ProjectID,
                              ProjectName = proj.ProjectName,
                              ProjectApprovalStatusID = proj.ProjectApprovalStatusID,
                              ProjectApprovalStatusCode = proj.ProjectApprovalStatusCode,

                              ProjectNo = proj.ProjectNo,

                              OrganizationID = proj.OrganizationID,
                              OrganizationTypeID = proj.OrganizationTypeID,
                              OrganizationName = proj.OrganizationName,
                              OrganizationToBeUnder = proj.OrganizationToBeUnder,

                              ProjectTypeID = proj.ProjectTypeID,
                              BudgetYear = proj.BudgetYear,

                              ProvinceID = proj.ProvinceID,
                              ProvinceName = proj.ProvinceName,
                              ProvinceAbbr = proj.ProvinceAbbr,

                              BudgetValue = proj.BudgetValue,
                              BudgetReviseValue = proj.BudgetReviseValue,

                              IsFollowup = (proj.IsFollowup.HasValue && proj.IsFollowup > 0) ? true : false,

                              IsPassMission1 = (proj.IsPassMission1 == "1") ? true : false,
                              IsPassMission2 = (proj.IsPassMission2 == "1") ? true : false,
                              IsPassMission3 = (proj.IsPassMission3 == "1") ? true : false,
                              IsPassMission4 = (proj.IsPassMission4 == "1") ? true : false,
                              IsPassMission5 = (proj.IsPassMission5 == "1") ? true : false,

                              IsStep1Approved = (proj.IsStep1Approved.HasValue && proj.IsStep1Approved > 0) ? true : false,
                              IsStep2Approved = (proj.IsStep2Approved.HasValue && proj.IsStep2Approved > 0) ? true : false,
                              IsStep3Approved = (proj.IsStep3Approved.HasValue && proj.IsStep3Approved == 1) ? true : ((proj.IsStep3Approved.HasValue && proj.IsStep3Approved == 0) ? false : (bool?)null),
                              IsStep4Approved = (proj.IsStep4Approved.HasValue && proj.IsStep4Approved == 1) ? true : ((proj.IsStep4Approved.HasValue && proj.IsStep4Approved == 0) ? false : (bool?)null),
                              IsStep5Approved = (proj.IsStep5Approved.HasValue && proj.IsStep5Approved == 1) ? true : ((proj.IsStep5Approved.HasValue && proj.IsStep5Approved == 0) ? false : (bool?)null),
                              IsStep6Approved = (proj.IsStep6Approved.HasValue && proj.IsStep6Approved == 1) ? true : ((proj.IsStep6Approved.HasValue && proj.IsStep6Approved == 0) ? false : (bool?)null),

                              ApprovalStatus = proj.ApprovalStatus,

                              DisabilityTypeID = proj.DisabilityTypeID,

                              SearcherName = searcherName,
                              SearcherOrganizationID = searcherOganizationID,
                              SearcherGroupID = searcherGroupID,
                              SearcherGroupCode = searcherGroupCode,

                              CreatedBy = proj.CreatedBy,
                              CreatedByID = proj.CreatedByID,

                              CreatorOrganizationID = proj.CreatorOrganizationID,
                              CreatorProvinceID = proj.CreatorProvinceID,

                              IsCreateByOfficer = (proj.IsCreateByOfficer.HasValue && proj.IsCreateByOfficer == 1) ? true : false,
                              SubmitedDate = proj.SubmitedDate,
                              IsReject = (proj.IsReject == 1) ? true : false,
                              CreatedDate = proj.CreatedDate,
                              //kenghot
                              ProjectEndDate = proj.ProjectEndDate,
                              FollowupStatusName = proj.FollowupStatusName,
                              FollowupStatusID = proj.FollowupStatusID,
                              //ApprovalBudget1 = GetApprovalBudgetByProjecID(proj.ProjectID)
                              RejectComment = proj.REJECTCOMMENT

                          }).ToQueryData(p);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            if (result.IsCompleted)
            {
                if (!isCountOnly)
                {
                    foreach (ServiceModels.ProjectInfo.ProjectInfoList pl in result.Data)
                    {
                        pl.ApprovalStatus1 = GetApprovalBudgetByProjecID(pl.ProjectInfoID);
                    }
                }

            }

            return result;
        }

        public Decimal SaveMTAttachment(ServiceModels.Attachment attach, String lovCode)
        {
            decimal result = 0;
            if (attach != null)
            {
                decimal typeId = _db.MT_ListOfValue.Where(x => x.LOVGroup == "AttachmentType" && x.LOVCode == lovCode).Select(x => x.LOVID).FirstOrDefault();
                if (attach.AttachmentID == 0)
                {
                    DBModels.Model.MT_Attachment attachment = new DBModels.Model.MT_Attachment();
                    attachment.AttachmentFilename = attach.AttachmentFileName;
                    attachment.PathName = attach.PathName;
                    attachment.FileSize = attach.FileSize;
                    attachment.AttachmentTypeID = typeId;
                    attachment.CreatedBy = _user.UserName;
                    attachment.CreatedByID = (decimal)_user.UserID;
                    attachment.CreatedDate = DateTime.Now;
                    _db.MT_Attachment.Add(attachment);
                    _db.SaveChanges();

                    attach.AttachmentID = attachment.AttachmentID;
                }


                result = attach.AttachmentID;
            }

            return result;
        }

        public ServiceModels.ReturnObject<bool> ValidateSubmitData(decimal projectID)
        {
            List<string> errorMessage = new List<string>();
            ServiceModels.ReturnObject<bool> result = new ReturnObject<bool>();
            Dictionary<String, String> required = GetRequiredSubmitData(projectID);
            foreach (String key in required.Keys)
            {
                errorMessage.Add(required[key]);
            }


            bool isValid = (errorMessage.Count == 0) ? true : false;

            result.IsCompleted = isValid;
            result.Data = isValid;
            result.Message = errorMessage;

            return result;
        }

        public ServiceModels.ReturnMessage SendDataToReview(decimal projectID, string ipAddress)
        {
            ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
            var validateResult = ValidateSubmitData(projectID);
            if ((validateResult.IsCompleted) && (validateResult.Data == true))
            {
                var steptOneStatus = _db.MT_ListOfValue.Where(x => x.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ &&
                x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus).FirstOrDefault();
                if (steptOneStatus != null)
                {
                    var projectGenaralInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();


                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(projectID, "1", _user.UserID.Value, ipAddress));
                    if (projectGenaralInfo != null)
                    {

                        projectGenaralInfo.ProjectApprovalStatusID = steptOneStatus.LOVID;
                        projectGenaralInfo.UpdatedDate = DateTime.Now;
                        projectGenaralInfo.SubmitedDate = DateTime.Now;
                        projectGenaralInfo.UpdatedBy = _user.UserName;
                        projectGenaralInfo.UpdatedByID = _user.UserID;

                        _db.SaveChanges();

                        result.IsCompleted = true;
                        result.Message.Add(Nep.Project.Resources.Message.SendDataToReviewSuccess);

                        _mailService.SendProjectConfirmNotify(projectID);
                        _smsService.Send("มีการยื่นคำร้องโครงการเข้ามาใหม่", _db, projectID);
                    }
                }

                if (!result.IsCompleted)
                {
                    result.Message.Add(Nep.Project.Resources.Error.SendDataToReviewUnsuccess);
                }
            }
            else
            {
                result.IsCompleted = false;
                result.Message = validateResult.Message;
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ListOfValue> GetProjectApprovalStatus(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ListOfValue> result = new ReturnObject<ListOfValue>();
            try
            {
                result.IsCompleted = true;
                var data = (from p in _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID)
                            join status in _db.MT_ListOfValue on p.ProjectApprovalStatusID equals status.LOVID
                            select new ServiceModels.ListOfValue()
                            {
                                LovID = p.ProjectApprovalStatusID,
                                LovCode = status.LOVCode,
                                LovName = status.LOVName
                            }).FirstOrDefault();

                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnMessage SaveProjectDocument(ServiceModels.ProjectInfo.ProjectDocument model)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
                decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_ATTACHMENT);


                DBModels.Model.ProjectDocument projDoc = _db.ProjectDocuments.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                projDoc = (projDoc == null) ? new DBModels.Model.ProjectDocument() : projDoc;

                using (var tran = _db.Database.BeginTransaction())
                {
                    #region kenghot Add Document and Remove Document
                    // kenghot
                    decimal i = 1;
                    string colName = "";
                    if (model.AddedDocuments.Count() > 0)
                    {
                        foreach (List<KendoAttachment> tmp in model.AddedDocuments)
                        {
                            colName = "DOCUMENTID" + i.ToString().Trim();
                            i++;
                            if (tmp.Count() > 0)
                            {
                                foreach (KendoAttachment k in tmp)
                                {
                                    var attKey = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                                    _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                                    {
                                        ATTACHMENTID = attKey.Value,
                                        FIELDNAME = colName,
                                        TABLENAME = TABLE_PROJECTDOCUMENT,
                                        TABLEROWID = model.ProjectID
                                    });
                                }
                            }

                        }
                    }
                    if (model.RemovedDocuments.Count() > 0)
                    {
                        foreach (List<KendoAttachment> tmp in model.RemovedDocuments)
                        {
                            colName = "DOCUMENTID" + i.ToString().Trim();
                            i++;
                            if (tmp.Count() > 0)
                            {
                                foreach (KendoAttachment k in tmp)
                                {
                                    RemoveFile(k, rootDestinationFolderPath);
                                }
                            }

                        }
                    }

                    #endregion
                    #region Add Document and Remove Document
                    // kenghot
                    // remark this region
                    //if (model.AddedDocument1 != null)
                    //{
                    //    projDoc.DocumentID1 = SaveFile(model.AddedDocument1, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument2 != null)
                    //{
                    //    projDoc.DocumentID2 = SaveFile(model.AddedDocument2, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if(model.AddedDocument3 != null){
                    //    projDoc.DocumentID3 = SaveFile(model.AddedDocument3, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if(model.AddedDocument4 != null){
                    //    projDoc.DocumentID4 = SaveFile(model.AddedDocument4, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if(model.AddedDocument5 != null){
                    //    projDoc.DocumentID5 = SaveFile(model.AddedDocument5, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument6 != null)
                    //{
                    //    projDoc.DocumentID6 = SaveFile(model.AddedDocument6, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument7 != null)
                    //{
                    //    projDoc.DocumentID7 = SaveFile(model.AddedDocument7, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument8 != null)
                    //{
                    //    projDoc.DocumentID8 = SaveFile(model.AddedDocument8, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument9 != null)
                    //{
                    //    projDoc.DocumentID9 = SaveFile(model.AddedDocument9, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument10 != null)
                    //{
                    //    projDoc.DocumentID10 = SaveFile(model.AddedDocument10, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument11 != null)
                    //{
                    //    projDoc.DocumentID11 = SaveFile(model.AddedDocument11, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument12 != null)
                    //{
                    //    projDoc.DocumentID12 = SaveFile(model.AddedDocument12, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument13 != null)
                    //{
                    //    projDoc.DocumentID13 = SaveFile(model.AddedDocument13, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //if (model.AddedDocument14 != null)
                    //{
                    //    projDoc.DocumentID14 = SaveFile(model.AddedDocument14, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    //}

                    //RemoveFile(model.RemovedDocument1, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument2, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument3, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument4, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument5, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument6, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument7, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument8, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument9, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument10, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument11, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument12, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument13, rootDestinationFolderPath);
                    //RemoveFile(model.RemovedDocument14, rootDestinationFolderPath);

                    #endregion Add Document and Remove Document

                    if (projDoc.ProjectID > 0)
                    {
                        projDoc.UpdatedBy = _user.UserName;
                        projDoc.UpdatedByID = _user.UserID;
                        projDoc.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        projDoc.ProjectID = model.ProjectID;
                        projDoc.CreatedBy = _user.UserName;
                        projDoc.CreatedByID = (decimal)_user.UserID;
                        projDoc.CreatedDate = DateTime.Now;

                        _db.ProjectDocuments.Add(projDoc);
                    }

                    _db.SaveChanges();


                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        #region Project Followup
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectFollowup> GetProjectFollowup(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectFollowup> result = new ReturnObject<ServiceModels.ProjectInfo.ProjectFollowup>();
            try
            {

                ServiceModels.ProjectInfo.ProjectFollowup data = (from p in _db.ProjectGeneralInfoes
                                                                  where p.ProjectID == projectID
                                                                  select new ServiceModels.ProjectInfo.ProjectFollowup
                                                                  {
                                                                      ProjectID = p.ProjectID,
                                                                      OrganizationID = p.OrganizationID,
                                                                      ApprovalStatus = (p.ProjectApproval != null) ? p.ProjectApproval.ApprovalStatus : null,

                                                                      ApprovalBudget = p.BudgetReviseValue,

                                                                      ProjectName = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.ProjectName : null,
                                                                      ProjectFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.ProjectFollowUpValue : null,
                                                                      Assessment61 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment61 : (Decimal?)null,

                                                                      Reason = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Reason : null,
                                                                      ReasonFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.ReasonFollowUpValue : (Decimal?)null,
                                                                      Assessment62 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment62 : (Decimal?)null,

                                                                      Objective = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Objective : null,
                                                                      ObjectiveFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.ObjectiveFollowUpValue : (Decimal?)null,
                                                                      Assessment63 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment63 : (Decimal?)null,

                                                                      TargetGroup = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.TargetGroup : null,
                                                                      TargetGroupFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.TargetGroupFollowUpValue : (Decimal?)null,
                                                                      Assessment64 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment64 : (Decimal?)null,

                                                                      Location = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Location : null,
                                                                      LocationFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.LocationFollowUpValue : (Decimal?)null,
                                                                      Assessment65 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment65 : (Decimal?)null,

                                                                      Timing = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Timing : null,
                                                                      TimingFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.TimingFollowUpValue : (Decimal?)null,
                                                                      Assessment66 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment66 : (Decimal?)null,

                                                                      OperationMethod = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.OperationMethod : null,
                                                                      OperationMethodFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.OperationFollowUpValue : (Decimal?)null,
                                                                      Assessment67 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment67 : (Decimal?)null,

                                                                      Budget = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Budget : null,
                                                                      BudgetFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.BudgetFollowUpValue : (Decimal?)null,
                                                                      Assessment68 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment68 : (Decimal?)null,

                                                                      Expection = (p.ProjectFollowUp != null) ? p.ProjectFollowUp.Expectation : null,
                                                                      ExpectionFollowupValue = (p.ProjectFollowUp != null) ? (Decimal?)p.ProjectFollowUp.ExpectationFollowUpValue : (Decimal?)null,
                                                                      Assessment69 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment69 : (Decimal?)null,

                                                                      ProjectApprovalStatusID = p.ProjectApprovalStatusID,
                                                                      ProjectApprovalStatusCode = (p.ProjectApprovalStatus != null) ? p.ProjectApprovalStatus.LOVCode : null,
                                                                      FollowupStatusCode = (p.FollowUpStatus != null) ? _db.MT_ListOfValue.Where(f => f.LOVID == (decimal)p.FollowUpStatus).Select(fObj => fObj.LOVCode).FirstOrDefault() : null,
                                                                      ProvinceID = p.ProvinceID,
                                                                      ProvinceAbbr = _db.MT_Province.Where(prov => prov.ProvinceID == p.ProjectID).Select(provr => provr.ProvinceAbbr).FirstOrDefault()

                                                                  }).SingleOrDefault();
                List<DBModels.Model.MT_Attachment> dbAttachments = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).SelectMany(y => y.MT_Attachment).ToList();
                List<ServiceModels.KendoAttachment> attachments = new List<KendoAttachment>();
                DBModels.Model.MT_Attachment dbAttachment;
                if ((data != null) && (dbAttachments != null) && (dbAttachments.Count > 0))
                {
                    for (int i = 0; i < dbAttachments.Count; i++)
                    {
                        dbAttachment = dbAttachments[i];
                        attachments.Add(new ServiceModels.KendoAttachment
                        {
                            id = dbAttachment.AttachmentID.ToString(),
                            name = dbAttachment.AttachmentFilename,
                            extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                            size = (int)dbAttachment.FileSize
                        });
                    }

                    data.Attachments = attachments;


                }

                data.ProjectFollowup2 = (from f in _db.PROJECTFOLLOWUP2 where f.PROJECTID == projectID select f).FirstOrDefault();
                var tg = _db.ProjectTargetGroups.Where(w => w.ProjectID == projectID);
                if (tg.Count() > 0)
                {
                    data.TotalTargetGroup = (int)tg.Sum(s => s.TargetGroupAmt);
                }
                else
                {
                    data.TotalTargetGroup = 0;
                }

                data.TotalParticipant = (int)_db.ProjectParticipants.Where(w => w.ProjectID == projectID).Count();
                var period = _db.ProjectOperations.Where(w => w.ProjectID == projectID).FirstOrDefault();
                data.Period1 = string.Format("{0} ถึง {1}", Utility.ToThaiDateFormat(period.StartDate, "dd MMMM yyyy", ""), Utility.ToThaiDateFormat(period.EndDate, "dd MMMM yyyy", ""));
                result.Data = data;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnMessage SaveProjectFollowup(ServiceModels.ProjectInfo.ProjectFollowup model)
        {
            ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
            try
            {
                String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
                decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_FOLLOWUP);
                ServiceModels.KendoAttachment attachment;
                List<DBModels.Model.MT_Attachment> addedDBAttachment = new List<DBModels.Model.MT_Attachment>();
                DBModels.Model.MT_Attachment dbAttachment;

                DBModels.Model.ProjectFollowup dbProjectFollowup = _db.ProjectFollowups.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                if (dbProjectFollowup == null)
                {
                    dbProjectFollowup = new DBModels.Model.ProjectFollowup();
                }

                DBModels.Model.PROJECTFOLLOWUP2 f2 = _db.PROJECTFOLLOWUP2.Where(x => x.PROJECTID == model.ProjectFollowup2.PROJECTID).FirstOrDefault();
                if (f2 == null)
                {
                    f2 = new DBModels.Model.PROJECTFOLLOWUP2();
                }
                using (var tran = _db.Database.BeginTransaction())
                {
                    #region Add Attachment and Remove Attachment
                    if ((model.AddedAttachments != null) && (model.AddedAttachments.Count > 0))
                    {
                        for (int i = 0; i < model.AddedAttachments.Count; i++)
                        {
                            attachment = model.AddedAttachments[i];
                            dbAttachment = CopyFile(attachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                            if (dbAttachment != null)
                            {
                                addedDBAttachment.Add(dbAttachment);
                            }
                        }

                        DBModels.Model.ProjectGeneralInfo proGenInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).SingleOrDefault();
                        proGenInfo.MT_Attachment = addedDBAttachment;
                        proGenInfo.UpdatedBy = _user.UserName;
                        proGenInfo.UpdatedByID = _user.UserID;
                        proGenInfo.UpdatedDate = DateTime.Now;
                    }


                    List<decimal> removedAttachmentIDs = new List<decimal>();
                    if ((model.RemovedAttachments != null) && (model.RemovedAttachments.Count > 0))
                    {
                        for (int i = 0; i < model.RemovedAttachments.Count; i++)
                        {
                            attachment = model.RemovedAttachments[i];
                            removedAttachmentIDs.Add(Decimal.Parse(attachment.id));
                            RemoveFile(attachment, rootDestinationFolderPath, false);
                        }

                        _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).SelectMany(atts => atts.MT_Attachment.Where(att => removedAttachmentIDs.Contains(att.AttachmentID))).ToList()
                            .ForEach(removeAtt => _db.MT_Attachment.Remove(removeAtt));

                        //_db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).SelectMany(y => y.MT_Attachment);
                        //_db.Database.ExecuteSqlCommand("DELETE FROM PROJECTFOLLOWUPATT WHERE ATTACHMENTID in(@p0)", String.Join(",", removedAttachmentIDs)); 
                        //List<DBModels.Model.MT_Attachment> removedDbAttach =  _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID)
                        //    .SelectMany(y => y.MT_Attachment).ToList<DBModels.Model.MT_Attachment>();                        

                        //removedDbAttach.ForEach(att => _db.MT_Attachment.Remove(att));


                    }
                    #endregion Add Attachment and Remove Attachment

                    #region Update Project Followup Value
                    dbProjectFollowup.ProjectName = model.ProjectName;
                    dbProjectFollowup.ProjectFollowUpValue = (decimal)model.ProjectFollowupValue;

                    dbProjectFollowup.Reason = model.Reason;
                    dbProjectFollowup.ReasonFollowUpValue = (decimal)model.ReasonFollowupValue;

                    dbProjectFollowup.Objective = model.Objective;
                    dbProjectFollowup.ObjectiveFollowUpValue = (decimal)model.ObjectiveFollowupValue;

                    dbProjectFollowup.TargetGroup = model.TargetGroup;
                    dbProjectFollowup.TargetGroupFollowUpValue = (decimal)model.TargetGroupFollowupValue;

                    dbProjectFollowup.Location = model.Location;
                    dbProjectFollowup.LocationFollowUpValue = (decimal)model.LocationFollowupValue;

                    dbProjectFollowup.Timing = model.Timing;
                    dbProjectFollowup.TimingFollowUpValue = (decimal)model.TimingFollowupValue;

                    dbProjectFollowup.OperationMethod = model.OperationMethod;
                    dbProjectFollowup.OperationFollowUpValue = (decimal)model.OperationMethodFollowupValue;

                    dbProjectFollowup.Budget = model.Budget;
                    dbProjectFollowup.BudgetFollowUpValue = (decimal)model.BudgetFollowupValue;

                    dbProjectFollowup.Expectation = model.Expection;
                    dbProjectFollowup.ExpectationFollowUpValue = (decimal)model.ExpectionFollowupValue;


                    if (dbProjectFollowup.ProjectID > 0)
                    {
                        dbProjectFollowup.UpdatedBy = _user.UserName;
                        dbProjectFollowup.UpdatedByID = _user.UserID;
                        dbProjectFollowup.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbProjectFollowup.ProjectID = model.ProjectID;
                        dbProjectFollowup.CreatedBy = _user.UserName;
                        dbProjectFollowup.CreatedByID = (decimal)_user.UserID;
                        dbProjectFollowup.CreatedDate = DateTime.Now;
                        _db.ProjectFollowups.Add(dbProjectFollowup);
                    }
                    #endregion Update Project Followup Value
                    var f = model.ProjectFollowup2;
                    #region Update Project Followup 2
                    f2.ACTIVITY1 = f.ACTIVITY1;
                    f2.ACTIVITY2 = f.ACTIVITY2;
                    f2.ACTIVITYSCORE1 = f.ACTIVITYSCORE1;
                    f2.OBJECTIVE1 = f.OBJECTIVE1;
                    f2.OBJECTIVE2 = f.OBJECTIVE2;
                    f2.OBJECTIVESCORE = f.OBJECTIVESCORE;
                    f2.PARTICIPANTSCORE1 = f.PARTICIPANTSCORE1;
                    f2.PERIODSCORE1 = f.PERIODSCORE1;

                    f2.RESULT1 = f.RESULT1;
                    f2.RESULT2 = f.RESULT2;
                    f2.RESULTSCORE = f.RESULTSCORE;
                    f2.TARGET1 = f.TARGET1;
                    f2.TARGET2 = f.TARGET2;
                    f2.TARGETSCORE = f.TARGETSCORE;
                    f2.TOTALPERCENT = f.TOTALPERCENT;
                    f2.TOTALPERCENT1 = f.TOTALPERCENT1;
                    f2.TOTALPERCENT2 = f.TOTALPERCENT2;
                    f2.TOTALTALSCORE = f.TOTALTALSCORE;
                    f2.TOTALSCORE1 = f.TOTALSCORE1;
                    f2.TOTALSCORE2 = f.TOTALSCORE2;
                    f2.PERIODFROM2 = f.PERIODFROM2;
                    f2.PERIODTO2 = f.PERIODTO2;

                    if (f2.PROJECTID > 0)
                    {
                        f2.UPDATEDBY = _user.UserName;
                        f2.UPDATEDBYID = _user.UserID;
                        f2.UPDATEDDATE = DateTime.Now;
                    }
                    else
                    {
                        f2.PROJECTID = model.ProjectID;
                        f2.CREATEDBY = _user.UserName;
                        f2.CREATEDBYID = (decimal)_user.UserID;
                        f2.CREATEDDATE = DateTime.Now;
                        _db.PROJECTFOLLOWUP2.Add(f2);
                    }
                    #endregion Update Project Followup 2
                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        #endregion Project Followup

        #region ReportTracking
        public ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> GetListFollowupTrackingDocumentList(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> result = new ReturnQueryData<ServiceModels.ProjectInfo.FollowupTrackingDocumentList>();
            try
            {
                result = (from tracking in _db.ProjectPrintReportTrackings
                          select new ServiceModels.ProjectInfo.FollowupTrackingDocumentList
                          {
                              ProjectID = tracking.ProjectID,
                              ReportTrackingID = tracking.ReportTrackingID,
                              ReportTrackingTypeName = tracking.ReportTrackingType.LOVName,
                              ReportNo = tracking.ReportNo,
                              ReportDate = tracking.ReportDate,
                              LetterAttchmentID = tracking.LetterAttchmentID,
                              LetterAttchmentName = (tracking.LetterAttchment != null) ? tracking.LetterAttchment.AttachmentFilename : null,
                              LetterSize = (tracking.LetterAttchment != null) ? (decimal?)tracking.LetterAttchment.FileSize : (decimal?)null,
                              LastedReportTrackingID = _db.ProjectPrintReportTrackings.Where(t => (t.ReportTrackingTypeID == tracking.ReportTrackingTypeID) && (t.ProjectID == tracking.ProjectID)).Max(x => x.ReportTrackingID)
                          }).ToQueryData(p);

                //if((result != null) && (result.Data != null)){
                //    List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> list = result.Data;
                //    ServiceModels.ProjectInfo.FollowupTrackingDocumentList item;
                //    ServiceModels.KendoAttachment atta;
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        item = list[i];
                //        if (item.LetterAttchmentID.HasValue)
                //        {
                //            atta = new KendoAttachment();
                //            atta.id = item.LetterAttchmentID.ToString();
                //            atta.name = item.LetterAttchmentName;
                //            atta.size = (int)item.LetterSize;
                //            atta.extension = Path.GetExtension(item.LetterAttchmentName);
                //            list[i].LetterAttchment = atta;
                //        }
                //    }

                //    result.Data = list;
                //}
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> IsReportTrackingCreateNew(decimal projectID, decimal reportTrackingTypeID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> result = new ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm>();
            try
            {
                var data = _db.ProjectPrintReportTrackings.Where(x => (x.ProjectID == projectID) && (x.ReportTrackingTypeID == reportTrackingTypeID))
                    .OrderByDescending(or => or.ReportTrackingID)
                    .Select(y => new ServiceModels.ProjectInfo.FollowupTrackingDocumentForm()
                    {
                        ProjectID = y.ProjectID,
                        ReportTrackingID = y.ReportTrackingID,
                        ReportTrackingTypeID = y.ReportTrackingTypeID,
                        ReportNo = y.ReportNo,
                        ReportDate = y.ReportDate,
                        DeadlineResponseDate = y.DeadlineResponseDate,
                        ReferenceInfo = y.ReferenceInfo,
                        ReferenceInfo1 = y.ReferenceInfo1
                    })
                    .FirstOrDefault();
                result.Data = data;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> GetFollowupTrackingDocumentForm(decimal reportTrackingID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> result = new ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm>();
            try
            {
                result.Data = GetFollowupTrackingDocumentFormFromDb(reportTrackingID);
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage DeleteFollowupTrackingDocumentForm(decimal reportTrackingID)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                DBModels.Model.ProjectPrintReportTracking tracking = _db.ProjectPrintReportTrackings.Where(x => x.ReportTrackingID == reportTrackingID).FirstOrDefault();

                if (tracking != null)
                {
                    DBModels.Model.MT_Attachment attach = tracking.LetterAttchment;
                    using (var tran = _db.Database.BeginTransaction())
                    {

                        _db.ProjectPrintReportTrackings.Remove(tracking);
                        if (attach != null)
                        {
                            String destinationFilePath = rootDestinationFolderPath + attach.PathName;
                            File.Delete(destinationFilePath);
                            _db.MT_Attachment.Remove(attach);
                        }

                        _db.SaveChanges();
                        tran.Commit();

                        result.IsCompleted = true;
                        result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
                    }
                }
                else
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> SaveFollowupTrackingDocumentForm(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> result =
                new ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm>();
            try
            {
                bool isDup = IsFollowupTrackingDocumentDuplicate(model.ProjectID, model.ReportTrackingID, model.ReportNo);
                decimal reportTrackingNo = 0;
                DBModels.Model.MT_ListOfValue orgTrackingType = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.ReportTrackingType) && (x.LOVCode == Common.LOVCode.Reporttrackingtype.หนังสือติดตามถึงองค์กร)).FirstOrDefault();
                if (!isDup)
                {
                    String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
                    String rootDestinationFolderPath = GetAttachmentRootFolder();
                    String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
                    decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PRING_REPORT_TRACKING);

                    Decimal followupTrakingID = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.FollowupStatus) && (x.LOVCode == Common.LOVCode.Followupstatus.กำลังติดตาม)).Select(y => y.LOVID).FirstOrDefault();
                    var genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

                    DBModels.Model.ProjectPrintReportTracking dbTracking = new DBModels.Model.ProjectPrintReportTracking();
                    ServiceModels.KendoAttachment newAttachment = model.LetterAttchment;
                    DBModels.Model.MT_Attachment oldAttachment = null;
                    using (var tran = _db.Database.BeginTransaction())
                    {
                        if ((model.ReportTrackingTypeID == orgTrackingType.LOVID) && (model.ReportTrackingID == null))
                        {
                            reportTrackingNo = (genInfo.ReportTrackingNo.HasValue) ? (decimal)genInfo.ReportTrackingNo : 0;
                            genInfo.ReportTrackingNo = (reportTrackingNo + 1);
                        }

                        if (model.ReportTrackingID.HasValue)
                        {
                            decimal trackingID = (decimal)model.ReportTrackingID;

                            dbTracking = _db.ProjectPrintReportTrackings.Where(x => x.ReportTrackingID == trackingID).FirstOrDefault();
                            oldAttachment = dbTracking.LetterAttchment;
                        }

                        dbTracking.ProjectID = model.ProjectID;
                        dbTracking.ReportDate = (DateTime)model.ReportDate;
                        dbTracking.DeadlineResponseDate = model.DeadlineResponseDate;
                        dbTracking.ReportNo = model.ReportNo;
                        dbTracking.ReportTrackingTypeID = model.ReportTrackingTypeID;
                        dbTracking.ReferenceInfo = model.ReferenceInfo;
                        dbTracking.ReferenceInfo1 = model.ReferenceInfo1;
                        dbTracking.LetterAttchmentID = SaveFile(newAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);

                        if (dbTracking.ReportTrackingID > 0)
                        {
                            dbTracking.UpdatedBy = _user.UserName;
                            dbTracking.UpdatedByID = _user.UserID;
                            dbTracking.UpdatedDate = DateTime.Now;
                        }
                        else
                        {
                            dbTracking.CreatedBy = _user.UserName;
                            dbTracking.CreatedByID = (decimal)_user.UserID;
                            dbTracking.CreatedDate = DateTime.Now;
                            _db.ProjectPrintReportTrackings.Add(dbTracking);
                        }

                        if (genInfo != null)
                        {
                            genInfo.FollowUpStatus = followupTrakingID;
                            genInfo.LastedFollowupDate = DateTime.Today;
                            genInfo.UpdatedBy = _user.UserName;
                            genInfo.UpdatedByID = _user.UserID;
                            genInfo.UpdatedDate = DateTime.Now;
                        }

                        if (oldAttachment != null)
                        {
                            ServiceModels.KendoAttachment removeAttachment = new KendoAttachment
                            {
                                id = oldAttachment.AttachmentID.ToString()
                            };
                            RemoveFile(removeAttachment, rootDestinationFolderPath);
                        }

                        _db.SaveChanges();

                        tran.Commit();

                        result.IsCompleted = true;
                        result.Data = GetFollowupTrackingDocumentFormFromDb(dbTracking.ReportTrackingID);
                        result.Message.Add(Resources.Message.SaveSuccess);
                    }
                }
                else
                {
                    String error = String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.FollowupTrackingDocumentForm_ReportNo);
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }



        public ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> GetReportOrgTrackingContext(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> result = new ReturnObject<ServiceModels.Report.ReportOrgTracking>();
            try
            {
                String nepAddress = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_ADDRESS).Select(y => y.ParameterValue).FirstOrDefault();
                String followupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

                if (genInfo != null)
                {
                    nepAddress = Common.Web.WebUtility.ParseToThaiNumber(nepAddress);

                    String projectName = genInfo.ProjectInformation.ProjectNameTH;
                    String orgName = genInfo.OrganizationNameTH;
                    String orgUnderSupportName = (!String.IsNullOrEmpty(genInfo.OrgUnderSupport)) ? String.Format(" ภายใต้การรับรองของ{0}", genInfo.OrgUnderSupport) : "";
                    String budgetAmount = Common.Web.WebUtility.ToThaiAmountNumber(genInfo.BudgetReviseValue, "N2");
                    String budgetAmountWord = Common.Web.WebUtility.ToThaiBath(genInfo.BudgetReviseValue);
                    String deadlineResponseDate = (model.DeadlineResponseDate.HasValue) ? Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.DeadlineResponseDate) : "";
                    String provinceName = _db.MT_Province.Where(x => x.ProvinceID == genInfo.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                    String approvalDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)genInfo.ProjectApproval.ApprovalDate);
                    String approvalNo = Common.Web.WebUtility.ToThaiNumber(Convert.ToDecimal(genInfo.ProjectApproval.ApprovalNo), "####");
                    approvalNo = approvalNo + "/" + Common.Web.WebUtility.ToThaiNumber(Convert.ToDecimal(genInfo.ProjectApproval.ApprovalYear), "####");
                    String reportDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.ReportDate);

                    String orgTemplateCode = (model.IsFirstTracking) ? Common.TemplateCode.ORG_TRACKING : Common.TemplateCode.ORG_RETRACKING;
                    var data = _db.MT_Template.Where(x => x.TemplateCode == orgTemplateCode).FirstOrDefault();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(data.TemplateDetail);
                    XmlNode config = xmlDoc.SelectSingleNode("config");

                    //header
                    String subject = config["subject"].InnerText;

                    String to = config["to"].InnerText;
                    to = to.Replace("##LetterTo##", orgName);

                    String attachment = (!String.IsNullOrEmpty(model.ReferenceInfo1)) ? model.ReferenceInfo1 : null;

                    //detail
                    XmlNode detail = config.SelectSingleNode("detail");
                    String reason = detail["reason"].InnerText;
                    reason = reason.Replace("##LetterReference##", model.ReferenceInfo);
                    reason = reason.Replace("##ApprovalNo##", approvalNo);
                    reason = reason.Replace("##ApprovalDate##", approvalDate);
                    reason = reason.Replace("##ProjectName##", projectName);
                    reason = reason.Replace("##ProvinceName##", provinceName);
                    reason = reason.Replace("##OrganizationName##", orgName);
                    reason = reason.Replace("##OrganizationUnderSupport##", orgUnderSupportName);
                    reason = reason.Replace("##BudgetAmount##", budgetAmount);
                    reason = reason.Replace("##BudgetAmountText##", budgetAmountWord);
                    reason = reason.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                    reason = "<div style='text-indent:2.5cm; text-align:justify;'>" + reason + "</div>";

                    String purpose = detail["purpose"].InnerText;
                    purpose = purpose.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                    purpose = "<div style='text-indent:2.5cm; text-align:justify;'>" + purpose + "</div>";

                    String summary = detail["summary"].InnerText;
                    summary = "<div style='text-indent:2.5cm;'>" + summary + "</div>";

                    //ending
                    String complimentary = config["complimentary"].InnerText;

                    ServiceModels.Report.ReportOrgTracking traking = new ServiceModels.Report.ReportOrgTracking();
                    traking.NepAddress = nepAddress;
                    traking.ReportNo = Common.Web.WebUtility.ParseToThaiNumber(model.ReportNo);
                    traking.ReportDate = reportDate;
                    traking.Subject = subject;
                    traking.To = to;
                    traking.Attachment = attachment;
                    traking.Reason = reason;
                    traking.Purpose = purpose;
                    traking.Summary = summary;
                    traking.Complementary = complimentary;
                    traking.DepartmentContact = followupContact;

                    result.Data = traking;
                    result.IsCompleted = true;
                }
                else
                {
                    throw new Exception(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> GetReportOrgTracking(decimal reportTrackingID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> result = new ReturnObject<ServiceModels.Report.ReportOrgTracking>();
            try
            {
                var trackingFormResult = GetFollowupTrackingDocumentForm(reportTrackingID);
                if (trackingFormResult.IsCompleted)
                {
                    ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model = trackingFormResult.Data;

                    String nepAddress = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.NEP_ADDRESS).Select(y => y.ParameterValue).FirstOrDefault();
                    String followupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
                    DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

                    if (genInfo != null)
                    {
                        nepAddress = Common.Web.WebUtility.ParseToThaiNumber(nepAddress);

                        String projectName = genInfo.ProjectInformation.ProjectNameTH;
                        String orgName = genInfo.OrganizationNameTH;
                        String orgUnderSupportName = (!String.IsNullOrEmpty(genInfo.OrgUnderSupport)) ? String.Format(" ภายใต้การรับรองของ{0}", genInfo.OrgUnderSupport) : "";
                        String budgetAmount = Common.Web.WebUtility.ToThaiAmountNumber(genInfo.BudgetReviseValue, "N2");
                        String budgetAmountWord = Common.Web.WebUtility.ToThaiBath(genInfo.BudgetReviseValue);
                        String deadlineResponseDate = (model.DeadlineResponseDate.HasValue) ? Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.DeadlineResponseDate) : "";
                        String provinceName = _db.MT_Province.Where(x => x.ProvinceID == genInfo.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                        String approvalDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)genInfo.ProjectApproval.ApprovalDate);
                        String approvalNo = Common.Web.WebUtility.ToThaiNumber(Convert.ToDecimal(genInfo.ProjectApproval.ApprovalNo), "####");
                        approvalNo = approvalNo + "/" + Common.Web.WebUtility.ToThaiNumber(Convert.ToDecimal(genInfo.ProjectApproval.ApprovalYear), "####");
                        String reportDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.ReportDate);

                        String orgTemplateCode = (model.IsFirstTracking) ? Common.TemplateCode.ORG_TRACKING : Common.TemplateCode.ORG_RETRACKING;
                        var data = _db.MT_Template.Where(x => x.TemplateCode == orgTemplateCode).FirstOrDefault();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(data.TemplateDetail);
                        XmlNode config = xmlDoc.SelectSingleNode("config");

                        //header
                        String subject = config["subject"].InnerText;

                        String to = config["to"].InnerText;
                        to = to.Replace("##LetterTo##", orgName);

                        String attachment = (!String.IsNullOrEmpty(model.ReferenceInfo1)) ? model.ReferenceInfo1 : null;

                        //detail
                        XmlNode detail = config.SelectSingleNode("detail");
                        String reason = detail["reason"].InnerText;
                        reason = reason.Replace("##LetterReference##", model.ReferenceInfo);
                        reason = reason.Replace("##ApprovalNo##", approvalNo);
                        reason = reason.Replace("##ApprovalDate##", approvalDate);
                        reason = reason.Replace("##ProjectName##", projectName);
                        reason = reason.Replace("##ProvinceName##", provinceName);
                        reason = reason.Replace("##OrganizationName##", orgName);
                        reason = reason.Replace("##OrganizationUnderSupport##", orgUnderSupportName);
                        reason = reason.Replace("##BudgetAmount##", budgetAmount);
                        reason = reason.Replace("##BudgetAmountText##", budgetAmountWord);
                        reason = reason.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                        reason = "<div style='text-indent:2.5cm; text-align:justify;'>" + reason + "</div>";

                        String purpose = detail["purpose"].InnerText;
                        purpose = purpose.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                        purpose = "<div style='text-indent:2.5cm; text-align:justify;'>" + purpose + "</div>";

                        String summary = detail["summary"].InnerText;
                        summary = "<div style='text-indent:2.5cm;'>" + summary + "</div>";

                        //ending
                        String complimentary = config["complimentary"].InnerText;

                        ServiceModels.Report.ReportOrgTracking traking = new ServiceModels.Report.ReportOrgTracking();
                        traking.NepAddress = nepAddress;
                        traking.ReportNo = Common.Web.WebUtility.ParseToThaiNumber(model.ReportNo);
                        traking.ReportDate = reportDate;
                        traking.Subject = subject;
                        traking.To = to;
                        traking.Attachment = attachment;
                        traking.Reason = reason;
                        traking.Purpose = purpose;
                        traking.Summary = summary;
                        traking.Complementary = complimentary;
                        traking.DepartmentContact = followupContact;

                        result.Data = traking;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        throw new Exception(Nep.Project.Resources.Message.NoRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> GetReportProvinceTrackingContext(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> result = new ReturnObject<ServiceModels.Report.ReportProvinceTracking>();
            try
            {
                String followupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
                String nepOrgName = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.OGR_NAME).Select(y => y.ParameterValue).FirstOrDefault();
                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

                if (genInfo != null)
                {
                    String projectName = genInfo.ProjectInformation.ProjectNameTH;
                    String orgName = genInfo.OrganizationNameTH;
                    String orgUnderSupportName = (!String.IsNullOrEmpty(genInfo.OrgUnderSupport)) ? String.Format(" ภายใต้การรับรองของ{0}", genInfo.OrgUnderSupport) : "";
                    String budgetAmount = Common.Web.WebUtility.ToThaiAmountNumber(genInfo.BudgetReviseValue, "N2");
                    String budgetAmountWord = Common.Web.WebUtility.ToThaiBath(genInfo.BudgetReviseValue);
                    String deadlineResponseDate = (model.DeadlineResponseDate.HasValue) ? Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.DeadlineResponseDate) : "";
                    String provinceName = _db.MT_Province.Where(x => x.ProvinceID == genInfo.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                    String reportDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.ReportDate);

                    String orgTemplateCode = (model.IsFirstTracking) ? Common.TemplateCode.PROVINCE_TRACKING : Common.TemplateCode.PROVINCE_RETRACKING;
                    var data = _db.MT_Template.Where(x => x.TemplateCode == orgTemplateCode).FirstOrDefault();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(data.TemplateDetail);
                    XmlNode config = xmlDoc.SelectSingleNode("config");

                    String to = config["to"].InnerText;
                    to = to.Replace("##LetterTo##", provinceName);

                    //detail
                    XmlNode detail = config.SelectSingleNode("detail");
                    String reason = detail["reason"].InnerText;
                    reason = reason.Replace("##LetterReference##", model.ReferenceInfo);
                    reason = reason.Replace("##ProjectName##", projectName);
                    reason = reason.Replace("##OrganizationName##", orgName);
                    reason = reason.Replace("##OrganizationUnderSupport##", orgUnderSupportName);
                    reason = reason.Replace("##BudgetAmount##", budgetAmount);
                    reason = reason.Replace("##BudgetAmountText##", budgetAmountWord);
                    reason = reason.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                    reason = "<div style='text-indent:1in; text-align:justify;'>" + reason + "</div>";

                    String purpose = detail["purpose"].InnerText;
                    purpose = purpose.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                    purpose = purpose.Replace("##ProvinceName##", provinceName);
                    purpose = "<div style='text-indent:1in; text-align:justify;'>" + purpose + "</div>";

                    String summary = (detail["summary"] != null) ? detail["summary"].InnerText : "";
                    summary = (!String.IsNullOrEmpty(summary)) ? ("<div style='text-indent:1in;'>" + summary + "</div>") : null;

                    ServiceModels.Report.ReportProvinceTracking traking = new ServiceModels.Report.ReportProvinceTracking();
                    traking.ReportNo = model.ReportNo;
                    traking.ReportDate = reportDate;
                    traking.To = to;
                    traking.Reason = reason;
                    traking.Purpose = purpose;
                    traking.Summary = summary;
                    traking.DepartmentContact = followupContact;
                    traking.OrganizationName = nepOrgName;

                    result.Data = traking;
                    result.IsCompleted = true;
                }
                else
                {
                    throw new Exception(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> GetReportProvinceTracking(decimal reportTrackingID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> result = new ReturnObject<ServiceModels.Report.ReportProvinceTracking>();
            try
            {
                var trackingFormResult = GetFollowupTrackingDocumentForm(reportTrackingID);
                if (trackingFormResult.IsCompleted)
                {
                    ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model = trackingFormResult.Data;
                    String followupContact = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.FOLLOWUP_CONTACT).Select(y => y.ParameterValue).FirstOrDefault();
                    String nepOrgName = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.OGR_NAME).Select(y => y.ParameterValue).FirstOrDefault();
                    DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();

                    if (genInfo != null)
                    {
                        String projectName = genInfo.ProjectInformation.ProjectNameTH;
                        String orgName = genInfo.OrganizationNameTH;
                        String orgUnderSupportName = (!String.IsNullOrEmpty(genInfo.OrgUnderSupport)) ? String.Format(" ภายใต้การรับรองของ{0}", genInfo.OrgUnderSupport) : "";
                        String budgetAmount = Common.Web.WebUtility.ToThaiAmountNumber(genInfo.BudgetReviseValue, "N2");
                        String budgetAmountWord = Common.Web.WebUtility.ToThaiBath(genInfo.BudgetReviseValue);
                        String deadlineResponseDate = (model.DeadlineResponseDate.HasValue) ? Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.DeadlineResponseDate) : "";
                        String provinceName = _db.MT_Province.Where(x => x.ProvinceID == genInfo.ProvinceID).Select(y => y.ProvinceName).FirstOrDefault();
                        String reportDate = Common.Web.WebUtility.ToThaiDateDDMMMMYYYY((DateTime)model.ReportDate);

                        String orgTemplateCode = (model.IsFirstTracking) ? Common.TemplateCode.PROVINCE_TRACKING : Common.TemplateCode.PROVINCE_RETRACKING;
                        var data = _db.MT_Template.Where(x => x.TemplateCode == orgTemplateCode).FirstOrDefault();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(data.TemplateDetail);
                        XmlNode config = xmlDoc.SelectSingleNode("config");

                        String to = config["to"].InnerText;
                        to = to.Replace("##LetterTo##", provinceName);

                        //detail
                        XmlNode detail = config.SelectSingleNode("detail");
                        String reason = detail["reason"].InnerText;
                        reason = reason.Replace("##LetterReference##", model.ReferenceInfo);
                        reason = reason.Replace("##ProjectName##", projectName);
                        reason = reason.Replace("##OrganizationName##", orgName);
                        reason = reason.Replace("##OrganizationUnderSupport##", orgUnderSupportName);
                        reason = reason.Replace("##BudgetAmount##", budgetAmount);
                        reason = reason.Replace("##BudgetAmountText##", budgetAmountWord);
                        reason = reason.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                        reason = "<div style='text-indent:1in; text-align:justify;'>" + reason + "</div>";

                        String purpose = detail["purpose"].InnerText;
                        purpose = purpose.Replace("##DeadlineResponseDate##", deadlineResponseDate);
                        purpose = purpose.Replace("##ProvinceName##", provinceName);
                        purpose = "<div style='text-indent:1in; text-align:justify;'>" + purpose + "</div>";

                        String summary = (detail["summary"] != null) ? detail["summary"].InnerText : "";
                        summary = (!String.IsNullOrEmpty(summary)) ? ("<div style='text-indent:1in;'>" + summary + "</div>") : null;

                        ServiceModels.Report.ReportProvinceTracking traking = new ServiceModels.Report.ReportProvinceTracking();
                        traking.ReportNo = model.ReportNo;
                        traking.ReportDate = reportDate;
                        traking.To = to;
                        traking.Reason = reason;
                        traking.Purpose = purpose;
                        traking.Summary = summary;
                        traking.DepartmentContact = followupContact;
                        traking.OrganizationName = nepOrgName;

                        result.Data = traking;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        throw new Exception(Nep.Project.Resources.Message.NoRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private bool IsFollowupTrackingDocumentDuplicate(decimal projectID, decimal? reportTrackingID, string reportNo)
        {
            decimal trackingID = (reportTrackingID.HasValue) ? (decimal)reportTrackingID : 0;
            var obj = _db.ProjectPrintReportTrackings.Where(x => (x.ReportNo.Equals(reportNo)) && (x.ReportTrackingID != reportTrackingID)).FirstOrDefault();
            bool isDup = (obj != null);
            return isDup;
        }

        private ServiceModels.ProjectInfo.FollowupTrackingDocumentForm GetFollowupTrackingDocumentFormFromDb(decimal reportTrackingID)
        {
            ServiceModels.ProjectInfo.FollowupTrackingDocumentForm obj;
            obj = _db.ProjectPrintReportTrackings.Where(x => x.ReportTrackingID == reportTrackingID)
                .Select(y => new ServiceModels.ProjectInfo.FollowupTrackingDocumentForm()
                {
                    ProjectID = y.ProjectID,
                    ReportTrackingID = y.ReportTrackingID,
                    ReportTrackingTypeID = y.ReportTrackingTypeID,
                    ReportTrackingTypeCode = y.ReportTrackingType.LOVCode,
                    ReportNo = y.ReportNo,
                    ReportDate = y.ReportDate,
                    DeadlineResponseDate = y.DeadlineResponseDate,
                    ReferenceInfo = y.ReferenceInfo,
                    ReferenceInfo1 = y.ReferenceInfo1,
                    LetterAttchmentID = y.LetterAttchmentID,
                    LetterAttchmentName = (y.LetterAttchment != null) ? y.LetterAttchment.AttachmentFilename : null,
                    LetterAttchmentSize = (y.LetterAttchment != null) ? y.LetterAttchment.FileSize : (decimal?)null

                }).FirstOrDefault();

            if (obj != null)
            {
                bool isEditable = true;
                List<Common.ProjectFunction> fList = GetProjectFunction(obj.ProjectID).Data;
                isEditable = fList.Contains(Common.ProjectFunction.PrintTrackingDocument);

                if (obj.LetterAttchmentID.HasValue)
                {
                    obj.LetterAttchment = new ServiceModels.KendoAttachment
                    {
                        id = obj.LetterAttchmentID.ToString(),
                        name = obj.LetterAttchmentName,
                        extension = Path.GetExtension(obj.LetterAttchmentName),
                        size = (int)obj.LetterAttchmentSize
                    };
                }
                obj.IsEditable = isEditable;

                IEnumerable<DBModels.Model.ProjectPrintReportTracking> lastedTrackings = _db.ProjectPrintReportTrackings.Where(x => (x.ProjectID == obj.ProjectID) && (x.ReportTrackingTypeID == obj.ReportTrackingTypeID)).OrderByDescending(or => or.ReportTrackingID);


                if (lastedTrackings.Count() > 1)
                {
                    DBModels.Model.ProjectPrintReportTracking lastedTracking = lastedTrackings.FirstOrDefault();
                    obj.IsEditable = (lastedTracking.ReportTrackingID == obj.ReportTrackingID) && isEditable;

                    lastedTracking = lastedTrackings.LastOrDefault();
                    obj.IsCreateFirstTime = (lastedTracking.ReportTrackingID == obj.ReportTrackingID);
                }
                else
                {
                    obj.IsCreateFirstTime = true;
                }
            }

            return obj;
        }

        #endregion ReportTracking

        #region Assessment
        public ServiceModels.ReturnObject<ServiceModels.ProjectInfo.AssessmentProject> GetAssessmentProject(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.AssessmentProject> result = new ReturnObject<ServiceModels.ProjectInfo.AssessmentProject>();
            try
            {
                ServiceModels.ProjectInfo.AssessmentProject data = (from p in _db.ProjectGeneralInfoes
                                                                    where p.ProjectID == projectID
                                                                    select new ServiceModels.ProjectInfo.AssessmentProject
                                                                    {
                                                                        ProjectID = p.ProjectID,
                                                                        OrganizationID = p.OrganizationID,
                                                                        ProjectApprovalStatusID = p.ProjectApprovalStatusID,
                                                                        ProjectApprovalStatusCode = p.ProjectApprovalStatus.LOVCode,

                                                                        AssessmentProvinceID = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.ProvinceID : p.ProvinceID,
                                                                        AssessmentProvinceName = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Province.ProvinceName : _db.MT_Province.Where(gProv => (gProv.ProvinceID == p.ProvinceID)).Select(rProv => rProv.ProvinceName).FirstOrDefault(),

                                                                        OrganizationNameTH = p.OrganizationNameTH,
                                                                        OrganizationNameEN = p.OrganizationNameEN,

                                                                        ProjectNameTH = (p.ProjectInformation != null) ? p.ProjectInformation.ProjectNameTH : null,
                                                                        ProjectNameEN = (p.ProjectInformation != null) ? p.ProjectInformation.ProjectNameEN : null,

                                                                        BudgetRequest = p.BudgetReviseValue,

                                                                        IsPassAss4 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassAss4 == "1" ? true : false) : (bool?)null,
                                                                        IsPassAss5 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassAss5 == "1" ? true : false) : (bool?)null,

                                                                        Assessment61 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment61 : (decimal?)null,
                                                                        Assessment62 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment62 : (decimal?)null,
                                                                        Assessment63 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment63 : (decimal?)null,
                                                                        Assessment64 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment64 : (decimal?)null,
                                                                        Assessment65 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment65 : (decimal?)null,
                                                                        Assessment66 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment66 : (decimal?)null,
                                                                        Assessment67 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment67 : (decimal?)null,
                                                                        Assessment68 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment68 : (decimal?)null,
                                                                        Assessment69 = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.Assessment69 : (decimal?)null,

                                                                        IsPassMission1 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassMission1 == "1" ? true : false) : (bool?)null,
                                                                        IsPassMission2 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassMission2 == "1" ? true : false) : (bool?)null,
                                                                        IsPassMission3 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassMission3 == "1" ? true : false) : (bool?)null,
                                                                        IsPassMission4 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassMission4 == "1" ? true : false) : (bool?)null,
                                                                        IsPassMission5 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.IsPassMission5 == "1" ? true : false) : (bool?)null,
                                                                        IsPassMission6 = (p.ProjectEvaluation != null) ? (p.ProjectEvaluation.ISPASSMISSION6 == "1" ? true : false) : (bool?)null,

                                                                        TotalScore = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.EvaluationValue : (decimal?)null,

                                                                        EvaluationStatusID = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.EvaluationValue : (decimal?)null,
                                                                        EvaluationScoreDesc = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.EvaluationStatus.LOVName : null,
                                                                        AssessmentDesc = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.AssessmentDesc : null,

                                                                        ProvinceMissionDesc = (p.ProjectEvaluation != null) ? p.ProjectEvaluation.ProvinceMissionDesc : null,

                                                                        ProvinceID = p.ProvinceID,

                                                                        CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == p.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault()

                                                                    }).SingleOrDefault();

                result.Data = data;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public DBModels.Model.PROJECTHISTORY CreateRowProjectHistory(decimal projectID, string histType, decimal userID, string ipAddress)
        {
            DBModels.Model.PROJECTHISTORY ph = new PROJECTHISTORY()
            {
                ENTRYDT = DateTime.Now,
                HISTORYTYPE = histType,
                IPADDRESS = ipAddress,
                PROJECTID = projectID,
                USERID = userID
            };
            return ph;
        }
        public ServiceModels.ReturnMessage SaveProjectEvaluation(ServiceModels.ProjectInfo.AssessmentProject model)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                decimal evaluationID = 0;

                using (var tran = _db.Database.BeginTransaction())
                {
                    DBModels.Model.ProjectEvaluation dbEval = _db.ProjectEvaluations.Where(x => x.ProjectID == model.ProjectID).SingleOrDefault();

                    if (dbEval == null)
                    {
                        dbEval = new DBModels.Model.ProjectEvaluation();

                    }
                    else
                    {
                        evaluationID = dbEval.ProjectID;
                    }

                    dbEval.ProvinceID = (decimal)model.AssessmentProvinceID;

                    dbEval.IsPassAss4 = (model.IsPassAss4 == true) ? "1" : "0";
                    dbEval.IsPassAss5 = (model.IsPassAss5 == true) ? "1" : "0";

                    if ((model.IsPassAss4 == true) && (model.IsPassAss5 == true))
                    {
                        dbEval.Assessment61 = (decimal)model.Assessment61;
                        dbEval.Assessment62 = (decimal)model.Assessment62;
                        dbEval.Assessment63 = (decimal)model.Assessment63;
                        dbEval.Assessment64 = (decimal)model.Assessment64;
                        dbEval.Assessment65 = (decimal)model.Assessment65;
                        dbEval.Assessment66 = (decimal)model.Assessment66;
                        dbEval.Assessment67 = (decimal)model.Assessment67;
                        dbEval.Assessment68 = (decimal)model.Assessment68;
                        dbEval.Assessment69 = (decimal)model.Assessment69;
                        dbEval.EvaluationValue = (decimal)model.TotalScore;
                    }
                    else
                    {
                        dbEval.Assessment61 = 0;
                        dbEval.Assessment62 = 0;
                        dbEval.Assessment63 = 0;
                        dbEval.Assessment64 = 0;
                        dbEval.Assessment65 = 0;
                        dbEval.Assessment66 = 0;
                        dbEval.Assessment67 = 0;
                        dbEval.Assessment68 = 0;
                        dbEval.Assessment69 = 0;
                        dbEval.EvaluationValue = 0;
                    }

                    dbEval.IsPassMission1 = (model.IsPassMission1 == true) ? "1" : "0";
                    dbEval.IsPassMission2 = (model.IsPassMission2 == true) ? "1" : "0";
                    dbEval.IsPassMission3 = (model.IsPassMission3 == true) ? "1" : "0";
                    dbEval.IsPassMission4 = (model.IsPassMission4 == true) ? "1" : "0";
                    dbEval.IsPassMission5 = (model.IsPassMission5 == true) ? "1" : "0";
                    dbEval.ISPASSMISSION6 = (model.IsPassMission6 == true) ? "1" : "0";

                    dbEval.AssessmentDesc = model.AssessmentDesc;
                    dbEval.ProvinceMissionDesc = model.ProvinceMissionDesc;

                    dbEval.EvaluationStatusID = GetEvaluationStatusID(model.IsPassAss4, model.IsPassAss5, model.TotalScore);


                    DBModels.Model.MT_ListOfValue status = GetListOfValue(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน, Common.LOVGroup.ProjectApprovalStatus);
                    DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => (x.ProjectID == model.ProjectID) && (
                           x.ProjectApprovalStatus.LOVCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ)).SingleOrDefault();

                    if (evaluationID == 0)
                    {

                        dbEval.ProjectID = model.ProjectID;
                        dbEval.CreatedBy = _user.UserName;
                        dbEval.CreatedByID = (decimal)_user.UserID;
                        dbEval.CreatedDate = DateTime.Now;
                        _db.ProjectEvaluations.Add(dbEval);
                    }
                    else
                    {
                        dbEval.UpdatedBy = _user.UserName;
                        dbEval.UpdatedByID = _user.UserID;
                        dbEval.UpdatedDate = DateTime.Now;
                    }

                    if ((genInfo != null) && (evaluationID == 0))
                    {
                        genInfo.ProjectApprovalStatusID = status.LOVID;
                        genInfo.UpdatedBy = _user.UserName;
                        genInfo.UpdatedByID = _user.UserID;
                        genInfo.UpdatedDate = DateTime.Now;

                        var u = (from ur in _db.SC_User where ur.UserID == _user.UserID select ur).FirstOrDefault();
                        if (u != null)
                        {
                            genInfo.RESPONSEEMAIL = _user.UserName;
                            genInfo.RESPONSEFIRSTNAME = _user.FirstName;
                            genInfo.RESPONSELASTNAME = _user.LastName;
                            genInfo.RESPONSEPOSITION = u.Position;
                            genInfo.RESPONSETEL = u.TelephoneNo;
                        }

                    }

                    _db.PROJECTHISTORies.Add(CreateRowProjectHistory(model.ProjectID, "2", _user.UserID.Value, model.IPAddress));
                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Resources.Message.SaveSuccess);

                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        #endregion Assessment

        #region Attachment
        //public ReturnObject<bool> SaveFileUpload(List<KendoAttachment> addFiles, List<KendoAttachment> removeFiles, decimal projID, string tabName, string tabField,decimal attachTypeID )
        //{
        //    ReturnObject<bool> ret = new ReturnObject<bool>();
        //    ret.Data = false;
        //    String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
        //    String rootDestinationFolderPath = GetAttachmentRootFolder();
        //    String folder = PROJECT_FOLDER_NAME + projID.ToString() + "\\";
        //    try
        //    {
        //        // Instructor File
        //        if (addFiles != null)
        //        {

        //            foreach (KendoAttachment k in addFiles)
        //            {
        //                var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachTypeID);
        //                _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
        //                {
        //                    ATTACHMENTID = attID.Value,
        //                    FIELDNAME = tabField,
        //                    TABLENAME = tabName,
        //                    TABLEROWID = projID
        //                });
        //            }
        //            _db.SaveChanges();
        //            //End kenghot
        //        }
        //        if (removeFiles != null)
        //        {


        //            foreach (KendoAttachment k in removeFiles)
        //            {
        //                RemoveFile(k, rootDestinationFolderPath);
        //            }
        //            //end kenghot
        //        }
        //        ret.Data = true;
        //        ret.IsCompleted = true;
        //        _db.SaveChanges();
        //        return ret;
        //    }
        //    catch(Exception e)
        //    {
        //        ret.IsCompleted = false;
        //        ret.Message.Add(e.Message);
        //        return ret;
        //    }


        //}
        private decimal? SaveFile(KendoAttachment kendoAttachment, String rootFolderPath, String rootDestinationFolderPath, String projectFolder, decimal attachmentTypeID)
        {
            decimal? fileID = null;
            if ((kendoAttachment != null) && (String.IsNullOrEmpty(kendoAttachment.id)))
            {
                String destFolderPath = rootDestinationFolderPath + projectFolder;
                String sourceFilePath = HostingEnvironment.MapPath(rootFolderPath + kendoAttachment.tempId);
                String destinationFilePath = destFolderPath + kendoAttachment.tempId;

                if (!Directory.Exists(destFolderPath))
                {
                    Directory.CreateDirectory(destFolderPath);
                }

                if (!File.Exists(destinationFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }


                DBModels.Model.MT_Attachment attac = new DBModels.Model.MT_Attachment();
                attac.AttachmentFilename = kendoAttachment.name;
                attac.FileSize = kendoAttachment.size;
                attac.PathName = projectFolder + kendoAttachment.tempId;
                attac.AttachmentTypeID = attachmentTypeID;
                attac.CreatedBy = _user.UserName;
                attac.CreatedByID = (decimal)_user.UserID;
                attac.CreatedDate = DateTime.Now;

                _db.MT_Attachment.Add(attac);
                _db.SaveChanges();

                fileID = attac.AttachmentID;

            }
            return fileID;
        }

        private DBModels.Model.MT_Attachment CopyFile(KendoAttachment kendoAttachment, String rootFolderPath, String rootDestinationFolderPath, String projectFolder, decimal attachmentTypeID)
        {
            DBModels.Model.MT_Attachment attac = null;

            if ((kendoAttachment != null) && (String.IsNullOrEmpty(kendoAttachment.id)))
            {
                String destFolderPath = rootDestinationFolderPath + projectFolder;
                String sourceFilePath = HostingEnvironment.MapPath(rootFolderPath + kendoAttachment.tempId);
                String destinationFilePath = destFolderPath + kendoAttachment.tempId;

                if (!Directory.Exists(destFolderPath))
                {
                    Directory.CreateDirectory(destFolderPath);
                }

                if (!File.Exists(destinationFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }


                attac = new DBModels.Model.MT_Attachment();
                attac.AttachmentFilename = kendoAttachment.name;
                attac.FileSize = kendoAttachment.size;
                attac.PathName = projectFolder + kendoAttachment.tempId;
                attac.AttachmentTypeID = attachmentTypeID;
                attac.CreatedBy = _user.UserName;
                attac.CreatedByID = (decimal)_user.UserID;
                attac.CreatedDate = DateTime.Now;

            }
            return attac;
        }

        private void RemoveFile(KendoAttachment kendoAttachment, String rootDestinationFolderPath, bool isRemoveAttachment = true)
        {
            if (kendoAttachment != null)
            {


                if (!String.IsNullOrEmpty(kendoAttachment.id))
                {
                    decimal oldAttachmentID = Decimal.Parse(kendoAttachment.id);

                    var oldAttachment = _db.MT_Attachment.Where(x => x.AttachmentID == oldAttachmentID).FirstOrDefault();
                    if (oldAttachment != null)
                    {
                        String destinationFilePath = rootDestinationFolderPath + oldAttachment.PathName;
                        File.Delete(destinationFilePath);

                        if (isRemoveAttachment)
                        {
                            _db.MT_Attachment.Remove(oldAttachment);
                        }
                    }
                }
            }
        }

        private String GetAttachmentRootFolder()
        {
            String folder = "";
            folder = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath)
                .Select(y => y.ParameterValue).FirstOrDefault();


            return folder;
        }
        #endregion Attachment

        public ServiceModels.ReturnObject<decimal> GetTotalIsFollowup(ServiceModels.QueryParameter p)
        {
            ServiceModels.ReturnObject<decimal> result = new ReturnObject<decimal>();
            try
            {
                var searchResult = ListProjectInfoList(p, true);

                result.IsCompleted = searchResult.IsCompleted;
                result.Message = searchResult.Message;

                if (searchResult.IsCompleted)
                {
                    result.Data = searchResult.TotalRow;
                }
                else
                {
                    result.Data = 0;
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<Common.ProjectFunction> GetProjectFunction(decimal projectID)
        {
            ServiceModels.ReturnQueryData<Common.ProjectFunction> result = new ReturnQueryData<ProjectFunction>();
            ServiceModels.ProjectFunctionParam param = (from p in _db.ProjectGeneralInfoes
                                                        where p.ProjectID == projectID
                                                        select new ServiceModels.ProjectFunctionParam
                                                        {
                                                            ProjectApprovalStatus = p.ProjectApprovalStatus.LOVCode,
                                                            CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == p.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault(),
                                                            ProjectProvinceID = p.ProvinceID,
                                                            FolloupStatusCode = (p.FollowUpStatus != null) ? _db.MT_ListOfValue.Where(fs => fs.LOVID == p.FollowUpStatus).Select(fsr => fsr.LOVCode).FirstOrDefault() : null,
                                                            ApprovalStatus = (p.ProjectApproval != null) ? p.ProjectApproval.ApprovalStatus : null,
                                                            ProjectOrgID = p.OrganizationID,
                                                            HasReportedResult = (p.ProjectReport != null)
                                                        }).FirstOrDefault();

            List<Common.ProjectFunction> projectFunctions = new List<Common.ProjectFunction>();
            IEnumerable<string> userRoles = _user.Roles;
            decimal? userProvinceId = _user.ProvinceID;
            decimal? userOrganizationID = _user.OrganizationID;

            if (param != null)
            {
                if (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                {
                    if ((userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (param.CreatorOrganizationID == null) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)) ||
                        ((param.CreatorOrganizationID == null) && (_user.IsAdministrator)) ||
                        (userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) && (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue) && (userOrganizationID == param.CreatorOrganizationID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.SaveDarft);
                        projectFunctions.Add(Common.ProjectFunction.Submit);
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm); /*for training*/
                        projectFunctions.Add(Common.ProjectFunction.Delete);
                    }
                }
                else if ((param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ) ||
                    (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน))
                {
                    if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)))
                    {
                        if (param.CreatorOrganizationID == null)
                        {
                            projectFunctions.Add(Common.ProjectFunction.ReviseData);
                            projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
                        }
                        else
                        {
                            projectFunctions.Add(Common.ProjectFunction.Reject); /* reject เพื่อให้ องค์กรแก้ไขข้อมูลบางอย่าง */
                            projectFunctions.Add(Common.ProjectFunction.ReviseAttachment);
                        }

                        projectFunctions.Add(Common.ProjectFunction.ReviseBudget);
                        projectFunctions.Add(Common.ProjectFunction.SaveEvaluation);
                        projectFunctions.Add(Common.ProjectFunction.SaveApproval);
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);


                    }
                    else if (_user.IsCenterOfficer ||
                       (_user.IsProvinceOfficer && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))) ||
                       (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue && (userOrganizationID == param.CreatorOrganizationID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }
                }
                else if (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง)
                {
                    if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.SaveEvaluation);
                        projectFunctions.Add(Common.ProjectFunction.SaveApproval);
                        projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }
                    else if (_user.IsCenterOfficer ||
                       (_user.IsProvinceOfficer && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))) ||
                       (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue && (userOrganizationID == param.CreatorOrganizationID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }

                }
                else if ((param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
                    (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))
                {
                    if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.SaveApproval);
                        projectFunctions.Add(Common.ProjectFunction.SaveContract);
                        projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }
                    else if (_user.IsCenterOfficer ||
                       (_user.IsProvinceOfficer && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))) ||
                       (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue && (userOrganizationID == param.CreatorOrganizationID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }

                }
                else if (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_1_ชะลอการพิจารณา ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_1_ชะลอการพิจารณา ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยคณะกรรมการกลั่นกรอง ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยอนุกรรมการกองทุนหรือจังหวัด ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยคณะกรรมการกลั่นกรอง ||
                   param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยอนุกรรมการกองทุนหรือจังหวัด
                   )
                {
                    if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.SaveApproval);
                        projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }
                }
                else if (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
                {
                    if (param.IsReportedResult)
                    {
                        if ((userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID) || (_user.IsAdministrator))))
                        {
                            projectFunctions.Add(Common.ProjectFunction.SaveReportResult);

                        }

                        if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)))
                        {
                            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                            projectFunctions.Add(Common.ProjectFunction.PrintBudget);

                        }

                        if (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (_user.IsCenterOfficer))
                        {
                            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                            projectFunctions.Add(Common.ProjectFunction.PrintBudget);

                        }

                        if (_user.IsAdministrator || _user.IsCenterOfficer ||
                               (_user.IsProvinceOfficer && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))) ||
                               (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue && (userOrganizationID == param.CreatorOrganizationID))
                            )
                        {
                            projectFunctions.Add(Common.ProjectFunction.PrintReport);
                        }
                    }
                    else
                    {
                        if ((userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) && (param.CreatorOrganizationID == null) && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID) || _user.IsAdministrator)) ||
                        ((userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) && (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue) && (userOrganizationID == param.CreatorOrganizationID))) || _user.IsAdministrator)
                        {
                            projectFunctions.Add(Common.ProjectFunction.SaveDraftReportResult);
                            projectFunctions.Add(Common.ProjectFunction.SubmitReportResult);

                            if (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT))
                            {
                                projectFunctions.Add(Common.ProjectFunction.SaveReportResult);
                            }

                        }

                        if ((!param.HasReportedResult) && String.IsNullOrEmpty(param.FolloupStatusCode) && (_user.IsAdministrator || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))))
                        {
                            projectFunctions.Add(Common.ProjectFunction.SaveContract);
                            projectFunctions.Add(Common.ProjectFunction.CancelContract);
                        }


                        if ((_user.IsAdministrator) || (_user.IsProvinceOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) &&
                       userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)) ||
                       (_user.IsCenterOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)))
                        {
                            projectFunctions.Add(Common.ProjectFunction.PrintTrackingDocument);
                        }

                    }

                    if ((_user.IsAdministrator) || (_user.IsProvinceOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) &&
                       userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)) ||
                       (_user.IsCenterOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.SaveTrackingResult);
                    }

                    if (_user.IsAdministrator || _user.IsCenterOfficer ||
                       (_user.IsProvinceOfficer && (userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID))) ||
                       (userOrganizationID.HasValue && param.CreatorOrganizationID.HasValue && (userOrganizationID == param.CreatorOrganizationID)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
                        projectFunctions.Add(Common.ProjectFunction.PrintBudget);
                    }

                    if (_user.IsAdministrator || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && userProvinceId.HasValue && ((userProvinceId == param.ProjectProvinceID) || _user.IsCenterOfficer)))
                    {
                        projectFunctions.Add(Common.ProjectFunction.PrintContract);
                    }

                }

                //View Summary Info Report
                if (userRoles.Contains(Common.FunctionCode.VIEW_PROJECT) ||
                    userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) ||
                    userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) ||
                    userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)
                    )
                {
                    if (param.ProjectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                    {
                        if (((param.CreatorOrganizationID == null) && ((userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)) || _user.IsAdministrator) ||
                            (param.CreatorOrganizationID.HasValue && userOrganizationID.HasValue && (param.CreatorOrganizationID == userOrganizationID))))
                        {
                            projectFunctions.Add(Common.ProjectFunction.CanViewProjectInfo);
                        }
                    }
                    else
                    {
                        if (_user.IsAdministrator || _user.IsCenterOfficer ||
                        (_user.IsProvinceOfficer && userProvinceId.HasValue && (userProvinceId == param.ProjectProvinceID)) ||
                        (userOrganizationID.HasValue && (userOrganizationID == param.ProjectOrgID)))
                        {
                            projectFunctions.Add(Common.ProjectFunction.CanViewProjectInfo);
                        }
                    }
                }

                result.IsCompleted = true;
                result.Data = projectFunctions;
            }
            else
            {
                result.IsCompleted = false;
                result.Message.Add(Nep.Project.Resources.Message.NoRecord);
            }

            return result;
        }

        //public ServiceModels.ReturnQueryData<Common.ProjectFunction> GetProjectFunction(String projectApprovalStatus, decimal? creatorOrganizationID, decimal? projectProvinceID, bool isReportedResult, String approvalStatus, decimal projectOrgID, bool hasReportedResult = false)
        //{
        //    ServiceModels.ReturnQueryData<Common.ProjectFunction> result = new ReturnQueryData<ProjectFunction>();

        //    List<Common.ProjectFunction> projectFunctions = new List<Common.ProjectFunction>();
        //    IEnumerable<string> userRoles = _user.Roles;
        //    decimal? userProvinceId = _user.ProvinceID;
        //    decimal? userOrganizationID = _user.OrganizationID;

        //    if (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
        //    {
        //        if ((userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (creatorOrganizationID == null) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)) ||
        //            ((creatorOrganizationID == null) && (_user.IsAdministrator)) ||
        //            (userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) && (userOrganizationID.HasValue && creatorOrganizationID.HasValue) && (userOrganizationID == creatorOrganizationID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.SaveDarft);
        //            projectFunctions.Add(Common.ProjectFunction.Submit);
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm); /*for training*/
        //            projectFunctions.Add(Common.ProjectFunction.Delete);                    
        //        }
        //    }
        //    else if ((projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ) ||
        //        (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน))
        //    {
        //        if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)))
        //        {
        //            if (creatorOrganizationID == null)
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.ReviseData);
        //                projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
        //            }
        //            else
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.Reject); /* reject เพื่อให้ องค์กรแก้ไขข้อมูลบางอย่าง */
        //                projectFunctions.Add(Common.ProjectFunction.ReviseAttachment);
        //            }

        //            projectFunctions.Add(Common.ProjectFunction.ReviseBudget);
        //            projectFunctions.Add(Common.ProjectFunction.SaveEvaluation);
        //            projectFunctions.Add(Common.ProjectFunction.SaveApproval);
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);


        //        }else if(_user.IsCenterOfficer ||
        //            (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID))) ||
        //            (userOrganizationID.HasValue && creatorOrganizationID.HasValue && (userOrganizationID == creatorOrganizationID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }
        //    }
        //    else if (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง)
        //    {
        //        if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.SaveEvaluation);
        //            projectFunctions.Add(Common.ProjectFunction.SaveApproval);
        //            projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }
        //        else if (_user.IsCenterOfficer ||
        //           (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID))) ||
        //           (userOrganizationID.HasValue && creatorOrganizationID.HasValue && (userOrganizationID == creatorOrganizationID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }

        //    }
        //    else if ((projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
        //        (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))
        //    {
        //        if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.SaveApproval);
        //            projectFunctions.Add(Common.ProjectFunction.SaveContract);
        //            projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }
        //        else if (_user.IsCenterOfficer ||
        //           (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID))) ||
        //           (userOrganizationID.HasValue && creatorOrganizationID.HasValue && (userOrganizationID == creatorOrganizationID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }

        //    }else if (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง ||
        //        projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด)
        //    {
        //        if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)))
        //        {                    
        //            projectFunctions.Add(Common.ProjectFunction.SaveApproval);
        //            projectFunctions.Add(Common.ProjectFunction.CancelledProjectRequest);
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }
        //    }
        //    else if (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
        //    {
        //        if (isReportedResult)
        //        {
        //            if ((userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) && ((userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID) || (_user.IsAdministrator))))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.SaveReportResult);

        //            }

        //            if ((_user.IsAdministrator) || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)))
        //            {                       
        //                projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //                projectFunctions.Add(Common.ProjectFunction.PrintBudget);

        //            }

        //            if (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (_user.IsCenterOfficer))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //                projectFunctions.Add(Common.ProjectFunction.PrintBudget);

        //            }

        //            if (_user.IsAdministrator || _user.IsCenterOfficer ||
        //                   (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID))) ||
        //                   (userOrganizationID.HasValue && creatorOrganizationID.HasValue && (userOrganizationID == creatorOrganizationID))
        //                )
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.PrintReport);
        //            }                    
        //        }
        //        else
        //        {
        //            if ((userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) && (creatorOrganizationID == null) && ((userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID) || _user.IsAdministrator)) ||
        //            (userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) && (userOrganizationID.HasValue && creatorOrganizationID.HasValue) && (userOrganizationID == creatorOrganizationID)))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.SaveDraftReportResult);
        //                projectFunctions.Add(Common.ProjectFunction.SubmitReportResult);                       

        //                if (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT))
        //                {
        //                    projectFunctions.Add(Common.ProjectFunction.SaveReportResult);
        //                }

        //            }

        //            if ((!hasReportedResult) && ( _user.IsAdministrator ||( userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID))))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.SaveContract);
        //                projectFunctions.Add(Common.ProjectFunction.CancelContract);                       
        //            }


        //            if ((_user.IsAdministrator)|| (_user.IsProvinceOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) &&
        //           (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)) ||
        //           (_user.IsCenterOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)))
        //            {                        
        //                projectFunctions.Add(Common.ProjectFunction.PrintTrackingDocument);
        //            }

        //        }

        //        if ((_user.IsAdministrator) || (_user.IsProvinceOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT) &&
        //           (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)) ||
        //           (_user.IsCenterOfficer && userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.SaveTrackingResult);                    
        //        }

        //        if (_user.IsAdministrator || _user.IsCenterOfficer ||
        //           (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID))) ||
        //           (userOrganizationID.HasValue && creatorOrganizationID.HasValue && (userOrganizationID == creatorOrganizationID)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.PrintDataForm);
        //            projectFunctions.Add(Common.ProjectFunction.PrintBudget);
        //        }

        //        if (_user.IsAdministrator || (userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) && (userProvinceId.HasValue && projectProvinceID.HasValue) && ((userProvinceId == projectProvinceID) || _user.IsCenterOfficer)))
        //        {
        //            projectFunctions.Add(Common.ProjectFunction.PrintContract);
        //        }               

        //    }

        //    //View Summary Info Report
        //    if (userRoles.Contains(Common.FunctionCode.VIEW_PROJECT) || 
        //        userRoles.Contains(Common.FunctionCode.REQUEST_PROJECT) ||
        //        userRoles.Contains(Common.FunctionCode.MANAGE_PROJECT) ||
        //        userRoles.Contains(Common.FunctionCode.TRACK_PROJECT)
        //        )
        //    {
        //        if (projectApprovalStatus == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
        //        {
        //            if(((creatorOrganizationID == null) && ((userProvinceId.HasValue && projectProvinceID.HasValue && (userProvinceId == projectProvinceID)) || _user.IsAdministrator) || 
        //                (creatorOrganizationID.HasValue && userOrganizationID.HasValue && (creatorOrganizationID == userOrganizationID))))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.CanViewProjectInfo);
        //            }
        //        }
        //        else
        //        {
        //            if (_user.IsAdministrator || _user.IsCenterOfficer ||
        //            (_user.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID)) ||
        //            (userOrganizationID.HasValue && (userOrganizationID == projectOrgID)))
        //            {
        //                projectFunctions.Add(Common.ProjectFunction.CanViewProjectInfo);
        //            }
        //        }                
        //    }


        //    result.IsCompleted = true;
        //    result.Data = projectFunctions;

        //    return result;
        //}
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportPaymentSlip> GetPaymentSlip(decimal projectID)
        {
            var ret = new ServiceModels.ReturnObject<ServiceModels.Report.ReportPaymentSlip>();
            ret.IsCompleted = false;
            try
            {
                var p = (from ps in _db.ProjectGeneralInfoes where ps.ProjectID == projectID select ps).FirstOrDefault();
                if (p == null)
                {
                    ret.Message.Add("ไม่พบโครงการที่ระบุ");
                    return ret;
                }



                decimal expense = 0;
                var result = GetProjectReportResult(projectID);
                if (!result.IsCompleted)
                {
                    ret.Message.Add(result.Message[0]);
                    return ret;
                }
                var act = GetProjectBudgetInfoByProjectID(projectID);
                if (!act.IsCompleted)
                {
                    ret.Message.Add(act.Message[0]);
                    return ret;
                }
                foreach (var b in act.Data.BudgetDetails)
                {
                    if (b.ActualExpense.HasValue)
                    {
                        expense += b.ActualExpense.Value;
                    }

                }
                if (expense == 0)
                {
                    ret.Message.Add("ยังไม่มีการระบุค่าใช้จ่ายจริง");
                    return ret;
                }
                if (!result.Data.Interest.HasValue)
                {
                    ret.Message.Add("ยังไม่มีการระบุดอกเบี้ย");
                    return ret;
                }
                var barcode = new KeepAutomation.Barcode.RDLC.BarCode();
                barcode.Symbology = KeepAutomation.Barcode.Symbology.Code39;
                var data = new ServiceModels.Report.ReportPaymentSlip();
                data.OrganizationName = p.OrganizationNameTH;
                data.OrganizationCode = p.OrganizationID.ToString();
                data.ProjectName = p.ProjectInformation.ProjectNameTH;
                data.BudgetAmount = result.Data.BudgetAmount.Value;
                data.ReviseBudgetAmount = result.Data.ReviseBudgetAmount.Value;
                data.Interest = result.Data.Interest.Value;
                data.ActualExpense = expense;
                data.Balance = data.ReviseBudgetAmount - data.ActualExpense;

                data.TotalBalance = data.Balance + data.Interest;
                data.TransactionCode = projectID.ToString().PadLeft(10, '0');
                barcode.CodeToEncode = data.TransactionCode;
                barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                data.Barcode = barcode.generateBarcodeToByteArray();
                data.TransactionDate = DateTime.Now;
                data.ThaiBaht = Nep.Project.Common.Report.Utility.ToThaiBath(data.TotalBalance);
                data.User = "test";

                ret.Data = data;
                ret.IsCompleted = true;
                return ret;
            }
            catch (Exception ex)
            {
                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);

                return ret;
            }
        }
        #region Project Request Report
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo> GetReportProjectRequest(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo> result = new ReturnObject<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo>();
            try
            {
                var data = (from g in _db.ProjectGeneralInfoes
                            where g.ProjectID == projectID
                            select new ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo
                            {
                                ProjectID = g.ProjectID,

                                ProjectProvinceID = g.ProvinceID,

                                ApprovalStatus = (g.ProjectApproval != null) ? g.ProjectApproval.ApprovalStatus : null,
                                ProjectApprovalStatus = g.ProjectApprovalStatus.LOVCode,
                                BudgetYear = (g.ProjectInformation != null) ? g.ProjectInformation.BudgetYear : 0,
                                CreatorOrganizationID = _db.SC_User.Where(user => user.UserID == g.CreatedByID).Select(userObj => userObj.OrganizationID).FirstOrDefault(),

                                OganizationID = g.OrganizationID,
                                OganizationNameTH = g.OrganizationNameTH,
                                OganizationNameEN = g.OrganizationNameEN,

                                OganizationTypeID = g.OrganizationTypeID,
                                OrganizationTypeEtc = g.OrganizationTypeEtc,
                                OrganizationYear = g.OrganizationYear,

                                Address = g.Address,
                                Building = g.Building,
                                Moo = g.Moo,
                                Soi = g.Soi,
                                Road = g.Road,
                                SubDistrict = g.SubDistrict,
                                District = g.District,
                                AddressProvinceName = _db.MT_Province.Where(prov => prov.ProvinceID == g.AddressProvinceID).Select(provObj => provObj.ProvinceName).FirstOrDefault(),
                                Postcode = g.Postcode,
                                Telephone = g.Telephone,
                                Fax = g.Fax,
                                Email = g.Email,

                                Purpose = g.Purpose,

                                CurrentProject = g.CurrentProject,

                                CurrentProjectResult = g.CurrentProjectResult,

                                GotSupportFlag = g.GotSupportFlag,
                                GotSupportYear = g.GotSupportYear,
                                GotSupportTimes = g.GotSupportTimes,
                                GotSupportLastProject = g.GotSupportLastProject,
                                GotSupportLastResult = g.GotSupportLastResult,
                                GotSupportLastProblems = g.GotSupportLastProblems,

                                ProjectNameTH = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameTH : null,
                                ProjectNameEN = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameEN : null,

                                DisabilityTypeID = (g.ProjectInformation != null) ? g.ProjectInformation.DisabilityTypeID : null,
                                DisabilityTypeCode = ((g.ProjectInformation != null) && (g.ProjectInformation.DisabilityTypeID != null)) ?
                                    _db.MT_ListOfValue.Where(dist => dist.LOVID == (decimal)g.ProjectInformation.DisabilityTypeID).Select(distObj => distObj.LOVCode).FirstOrDefault() : null,

                                PrefixName1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Prefix1Personel.LOVName : null,
                                Firstname1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Firstname1 : null,
                                Lastname1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Lastname1 : null,
                                Address1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Address1 : null,
                                Building1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Building1 : null,
                                Moo1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Moo1 : null,
                                Soi1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Soi1 : null,
                                Road1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Road1 : null,
                                SubDistrict1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.SubDistrict1 : null,
                                District1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.District1 : null,
                                ProvinceName1 = (g.ProjectPersonel != null) ? _db.MT_Province.Where(prov1 => prov1.ProvinceID == g.ProjectPersonel.ProvinceID1)
                                    .Select(prov1Obj => prov1Obj.ProvinceName).FirstOrDefault() : null,
                                PostCode1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.PostCode1 : null,
                                Telephone1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Telephone1 : null,
                                Fax1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Fax1 : null,
                                Email1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Email1 : null,

                                PrefixName2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Prefix2Personel.LOVName : null,
                                Firstname2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Firstname2 : null,
                                Lastname2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Lastname2 : null,
                                Address2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Address2 : null,
                                Building2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Building2 : null,
                                Moo2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Moo2 : null,
                                Soi2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Soi2 : null,
                                Road2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Road2 : null,
                                SubDistrict2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.SubDistrict2 : null,
                                District2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.District2 : null,
                                ProvinceName2 = (g.ProjectPersonel != null) ? _db.MT_Province.Where(prov2 => prov2.ProvinceID == g.ProjectPersonel.ProvinceID2)
                                    .Select(prov2Obj => prov2Obj.ProvinceName).FirstOrDefault() : null,
                                PostCode2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.PostCode2 : null,
                                Telephone2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Telephone2 : null,
                                Fax2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Fax2 : null,
                                Email2 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Email2 : null,

                                PrefixName3 = ((g.ProjectPersonel != null) && (g.ProjectPersonel.Prefix3 != null)) ? g.ProjectPersonel.Prefix3Personel.LOVName : null,
                                Firstname3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Firstname3 : null,
                                Lastname3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Lastname3 : null,
                                Address3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Address3 : null,
                                Building3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Building3 : null,
                                Moo3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Moo3 : null,
                                Soi3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Soi3 : null,
                                Road3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Road3 : null,
                                SubDistrict3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.SubDistrict3 : null,
                                District3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.District3 : null,
                                ProvinceName3 = ((g.ProjectPersonel != null) && (g.ProjectPersonel.ProvinceID3 != null)) ?
                                    _db.MT_Province.Where(prov3 => prov3.ProvinceID == g.ProjectPersonel.ProvinceID3)
                                    .Select(prov3Obj => prov3Obj.ProvinceName).FirstOrDefault() : null,
                                PostCode3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.PostCode3 : null,
                                Telephone3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Telephone3 : null,
                                Fax3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Fax3 : null,
                                Email3 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Email3 : null,

                                ProjectReason = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectReason : null,

                                ProjectPurpose = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectPurpose : null,

                                OperationAddress = (g.ProjectOperation != null) ? g.ProjectOperation.Address : null,
                                OperationBuilding = (g.ProjectOperation != null) ? g.ProjectOperation.Building : null,
                                OperationMoo = (g.ProjectOperation != null) ? g.ProjectOperation.Moo : null,
                                OperationSoi = (g.ProjectOperation != null) ? g.ProjectOperation.Soi : null,
                                OperationRoad = (g.ProjectOperation != null) ? g.ProjectOperation.Road : null,
                                OperationSubDistrict = (g.ProjectOperation != null) ? g.ProjectOperation.SubDistrict : null,
                                OperationDistrict = (g.ProjectOperation != null) ? g.ProjectOperation.District : null,
                                OperationProvince = (g.ProjectOperation != null) ? g.ProjectOperation.MT_Province.ProvinceName : null,

                                StartDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.StartDate : (DateTime?)null,
                                EndDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.EndDate : (DateTime?)null,
                                TotalDay = (g.ProjectOperation != null) ? (Decimal?)g.ProjectOperation.TotalDay : (Decimal?)null,
                                TimeDesc = (g.ProjectOperation != null) ? g.ProjectOperation.TimeDesc : null,

                                Method = (g.ProjectOperation != null) ? g.ProjectOperation.Method : null,

                                BudgetValue = g.BudgetValue,

                                BudgetFromOtherFlag = (g.BudgetFromOtherFlag == "1") ? true : false,
                                BudgetFromOtherName = g.BudgetFromOtherName,
                                BudgetFromOtherAmount = g.BudgetFromOtherAmount,

                                ProjectKPI = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectKPI : null,

                                ProjectResult = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectResult : null,

                                AssessmentDesc = (g.ProjectEvaluation != null) ? g.ProjectEvaluation.AssessmentDesc : null

                            }).FirstOrDefault();

                if (data != null)
                {

                    data = ReplaceArabicNumber(data);
                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canPrint = functions.Contains(Common.ProjectFunction.PrintDataForm);



                    if (canPrint)
                    {
                        result.IsCompleted = true;
                        result.Data = data;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }


            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo ReplaceArabicNumber(ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo model)
        {
            //1.1
            model.OganizationNameTH = Common.Web.WebUtility.ParseToThaiNumber(model.OganizationNameTH);
            model.OrganizationTypeEtc = Common.Web.WebUtility.ParseToThaiNumber(model.OrganizationTypeEtc);

            //1.5
            model.Address = Common.Web.WebUtility.ParseToThaiNumber(model.Address);
            model.Building = Common.Web.WebUtility.ParseToThaiNumber(model.Building);
            model.Moo = Common.Web.WebUtility.ParseToThaiNumber(model.Moo);
            model.Soi = Common.Web.WebUtility.ParseToThaiNumber(model.Soi);
            model.Road = Common.Web.WebUtility.ParseToThaiNumber(model.Road);
            //model.SubDistrict = Common.Web.WebUtility.ParseToThaiNumber(model.SubDistrict);
            //model.District = Common.Web.WebUtility.ParseToThaiNumber(model.District);
            model.AddressProvinceName = Common.Web.WebUtility.ParseToThaiNumber(model.AddressProvinceName);
            model.Postcode = Common.Web.WebUtility.ParseToThaiNumber(model.Postcode);
            model.Telephone = Common.Web.WebUtility.ParseToThaiNumber(model.Telephone);
            model.Fax = Common.Web.WebUtility.ParseToThaiNumber(model.Fax);
            //model.Email = Common.Web.WebUtility.ParseToThaiNumber(model.Email);

            //1.6
            model.Purpose = Common.Web.WebUtility.ParseToThaiNumber(model.Purpose);

            //1.7
            model.CurrentProject = Common.Web.WebUtility.ParseToThaiNumber(model.CurrentProject);

            //1.8
            model.CurrentProjectResult = Common.Web.WebUtility.ParseToThaiNumber(model.CurrentProjectResult);

            //1.9
            model.GotSupportLastProject = Common.Web.WebUtility.ParseToThaiNumber(model.GotSupportLastProject);
            model.GotSupportLastResult = Common.Web.WebUtility.ParseToThaiNumber(model.GotSupportLastResult);
            model.GotSupportLastProblems = Common.Web.WebUtility.ParseToThaiNumber(model.GotSupportLastProblems);

            //2.1
            model.ProjectNameTH = Common.Web.WebUtility.ParseToThaiNumber(model.ProjectNameTH);

            //2.3 ผู้รับผิดชอบโครงการ 
            model.Address1 = Common.Web.WebUtility.ParseToThaiNumber(model.Address1);
            model.Building1 = Common.Web.WebUtility.ParseToThaiNumber(model.Building1);
            model.Moo1 = Common.Web.WebUtility.ParseToThaiNumber(model.Moo1);
            model.Soi1 = Common.Web.WebUtility.ParseToThaiNumber(model.Soi1);
            model.Road1 = Common.Web.WebUtility.ParseToThaiNumber(model.Road1);
            //model.SubDistrict1 = Common.Web.WebUtility.ParseToThaiNumber(model.SubDistrict1);
            //model.District1 = Common.Web.WebUtility.ParseToThaiNumber(model.District1);
            //model.ProvinceName1 = Common.Web.WebUtility.ParseToThaiNumber(model.ProvinceName1);
            model.PostCode1 = Common.Web.WebUtility.ParseToThaiNumber(model.PostCode1);
            model.Telephone1 = Common.Web.WebUtility.ParseToThaiNumber(model.Telephone1);
            model.Fax1 = Common.Web.WebUtility.ParseToThaiNumber(model.Fax1);
            //model.Email1 = Common.Web.WebUtility.ParseToThaiNumber(model.Email1);

            //2.4 ผู้ประสานงานโครงการ 
            model.Firstname2 = Common.Web.WebUtility.ParseToThaiNumber(model.Firstname2);
            model.Lastname2 = Common.Web.WebUtility.ParseToThaiNumber(model.Lastname2);
            model.Address2 = Common.Web.WebUtility.ParseToThaiNumber(model.Address2);
            model.Building2 = Common.Web.WebUtility.ParseToThaiNumber(model.Building2);
            model.Moo2 = Common.Web.WebUtility.ParseToThaiNumber(model.Moo2);
            model.Soi2 = Common.Web.WebUtility.ParseToThaiNumber(model.Soi2);
            model.Road2 = Common.Web.WebUtility.ParseToThaiNumber(model.Road2);
            //model.SubDistrict2 = Common.Web.WebUtility.ParseToThaiNumber(model.SubDistrict2);
            //model.District2 = Common.Web.WebUtility.ParseToThaiNumber(model.District2);
            //model.ProvinceName2 = Common.Web.WebUtility.ParseToThaiNumber(model.ProvinceName2);
            model.PostCode2 = Common.Web.WebUtility.ParseToThaiNumber(model.PostCode2);
            model.Telephone2 = Common.Web.WebUtility.ParseToThaiNumber(model.Telephone2);
            model.Fax2 = Common.Web.WebUtility.ParseToThaiNumber(model.Fax2);
            //model.Email2 = Common.Web.WebUtility.ParseToThaiNumber(model.Email2);

            model.Firstname3 = Common.Web.WebUtility.ParseToThaiNumber(model.Firstname3);
            model.Lastname3 = Common.Web.WebUtility.ParseToThaiNumber(model.Lastname3);
            model.Address3 = Common.Web.WebUtility.ParseToThaiNumber(model.Address3);
            model.Building3 = Common.Web.WebUtility.ParseToThaiNumber(model.Building3);
            model.Moo3 = Common.Web.WebUtility.ParseToThaiNumber(model.Moo3);
            model.Soi3 = Common.Web.WebUtility.ParseToThaiNumber(model.Soi3);
            model.Road3 = Common.Web.WebUtility.ParseToThaiNumber(model.Road3);
            //model.SubDistrict3 = Common.Web.WebUtility.ParseToThaiNumber(model.SubDistrict3);
            //model.District3 = Common.Web.WebUtility.ParseToThaiNumber(model.District3);
            //model.ProvinceName3 = Common.Web.WebUtility.ParseToThaiNumber(model.ProvinceName3);
            model.PostCode3 = Common.Web.WebUtility.ParseToThaiNumber(model.PostCode3);
            model.Telephone3 = Common.Web.WebUtility.ParseToThaiNumber(model.Telephone3);
            model.Fax3 = Common.Web.WebUtility.ParseToThaiNumber(model.Fax3);
            //model.Email3 = Common.Web.WebUtility.ParseToThaiNumber(model.Email3);

            //2.5
            model.ProjectReason = Common.Web.WebUtility.ParseToThaiNumber(model.ProjectReason);

            //2.6
            model.ProjectPurpose = Common.Web.WebUtility.ParseToThaiNumber(model.ProjectPurpose);

            //2.8
            model.OperationAddress = Common.Web.WebUtility.ParseToThaiNumber(model.OperationAddress);
            model.OperationBuilding = Common.Web.WebUtility.ParseToThaiNumber(model.OperationBuilding);
            model.OperationMoo = Common.Web.WebUtility.ParseToThaiNumber(model.OperationMoo);
            model.OperationSoi = Common.Web.WebUtility.ParseToThaiNumber(model.OperationSoi);
            model.OperationRoad = Common.Web.WebUtility.ParseToThaiNumber(model.OperationRoad);
            //model.OperationSubDistrict = Common.Web.WebUtility.ParseToThaiNumber(model.OperationSubDistrict);
            //model.OperationDistrict = Common.Web.WebUtility.ParseToThaiNumber(model.OperationDistrict);
            //model.OperationProvince = Common.Web.WebUtility.ParseToThaiNumber(model.OperationProvince);

            //2.9
            model.TimeDesc = Common.Web.WebUtility.ParseToThaiNumber(model.TimeDesc);

            //2.10
            model.Method = Common.Web.WebUtility.ParseToThaiNumber(model.Method);

            //2.11
            model.BudgetFromOtherName = Common.Web.WebUtility.ParseToThaiNumber(model.BudgetFromOtherName);

            //2.12
            model.ProjectKPI = Common.Web.WebUtility.ParseToThaiNumber(model.ProjectKPI);

            //2.13
            model.ProjectResult = Common.Web.WebUtility.ParseToThaiNumber(model.ProjectResult);

            //ความเห็นประกอบการพิจารณา
            model.AssessmentDesc = Common.Web.WebUtility.ParseToThaiNumber(model.AssessmentDesc);


            return model;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance> GetListOrganizationAssistance(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance>();
            try
            {
                var dbGenInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
                if (dbGenInfo != null)
                {
                    List<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance> list = new List<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance>();
                    int no = 0;
                    no++;
                    if (!String.IsNullOrEmpty(dbGenInfo.SourceName1))
                    {
                        list.Add(new ServiceModels.Report.ReportProjectRequest.OrganizationAssistance
                        {
                            No = no,
                            OrganizationName = Common.Web.WebUtility.ParseToThaiNumber(dbGenInfo.SourceName1),
                            Amount = dbGenInfo.MoneySupport1,
                        });
                    }

                    no++;
                    if (!String.IsNullOrEmpty(dbGenInfo.SourceName2))
                    {
                        list.Add(new ServiceModels.Report.ReportProjectRequest.OrganizationAssistance
                        {
                            No = no,
                            OrganizationName = Common.Web.WebUtility.ParseToThaiNumber(dbGenInfo.SourceName2),
                            Amount = dbGenInfo.MoneySupport2,
                        });
                    }

                    no++;
                    if (!String.IsNullOrEmpty(dbGenInfo.SourceName3))
                    {
                        list.Add(new ServiceModels.Report.ReportProjectRequest.OrganizationAssistance
                        {
                            No = no,
                            OrganizationName = Common.Web.WebUtility.ParseToThaiNumber(dbGenInfo.SourceName3),
                            Amount = dbGenInfo.MoneySupport3,
                        });
                    }

                    no++;
                    if (!String.IsNullOrEmpty(dbGenInfo.SourceName4))
                    {
                        list.Add(new ServiceModels.Report.ReportProjectRequest.OrganizationAssistance
                        {
                            No = no,
                            OrganizationName = Common.Web.WebUtility.ParseToThaiNumber(dbGenInfo.SourceName4),
                            Amount = dbGenInfo.MoneySupport4,
                        });
                    }

                    result.IsCompleted = true;
                    result.Data = list;

                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> GetListProjectAttachment(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment>();
            try
            {
                List<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> attList = new List<ServiceModels.Report.ReportProjectRequest.ProjectAttachment>();
                var att = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).FirstOrDefault();
                var lovAtts = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectAttachment).OrderBy(or => or.OrderNo).ToList();
                var attnew = _db.K_FILEINTABLE.Where(w => w.TABLENAME == "PROJECTDOCUMENT" && w.TABLEROWID == projectID).ToList();
                bool hasProjectAttachment = (attnew.Count > 0);
                if (lovAtts != null)
                {
                    DBModels.Model.MT_ListOfValue lov;
                    String lovName;
                    String lovCode;
                    // bool hasAttachment = false;
                    int no;

                    for (int i = 0; i < lovAtts.Count; i++)
                    {
                        lov = lovAtts[i];
                        lovCode = lov.LOVCode;
                        lovName = lov.LOVName;
                        no = (int)lov.OrderNo;
                        var fieldname = string.Format("DOCUMENTID{0}", no.ToString());
                        //hasAttachment = false;
                        var found = attnew.Where(w => w.FIELDNAME == fieldname).FirstOrDefault();

                        if (hasProjectAttachment)
                        {
                            attList.Add(new ServiceModels.Report.ReportProjectRequest.ProjectAttachment
                            {
                                HasAttachment = found != null,
                                ProjectAttachmentName = Common.Web.WebUtility.ParseToThaiNumber(lov.LOVName)
                            });
                        }



                    }

                    result.IsCompleted = true;
                    result.Data = attList;
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        private ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> GetListProjectAttachmentOld(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment>();
            try
            {
                List<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> attList = new List<ServiceModels.Report.ReportProjectRequest.ProjectAttachment>();
                var att = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).FirstOrDefault();
                var lovAtts = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectAttachment).OrderBy(or => or.OrderNo).ToList();
                bool hasProjectAttachment = (att != null);
                if (lovAtts != null)
                {
                    DBModels.Model.MT_ListOfValue lov;
                    String lovName;
                    String lovCode;
                    bool hasAttachment = false;
                    int no;

                    for (int i = 0; i < lovAtts.Count; i++)
                    {
                        lov = lovAtts[i];
                        lovCode = lov.LOVCode;
                        lovName = lov.LOVName;
                        no = (int)lov.OrderNo;
                        hasAttachment = false;
                        if (hasProjectAttachment)
                        {
                            switch (no)
                            {
                                case 1:
                                    {
                                        hasAttachment = true;
                                        break;
                                    }
                                case 2:
                                    {
                                        hasAttachment = att.DocumentID2.HasValue;
                                        break;
                                    }
                                case 3:
                                    {
                                        hasAttachment = att.DocumentID3.HasValue;
                                        break;
                                    }
                                case 4:
                                    {
                                        hasAttachment = att.DocumentID4.HasValue;
                                        break;
                                    }
                                case 5:
                                    {
                                        hasAttachment = att.DocumentID5.HasValue;
                                        break;
                                    }
                                case 6:
                                    {
                                        hasAttachment = att.DocumentID6.HasValue;
                                        break;
                                    }
                                case 7:
                                    {
                                        hasAttachment = att.DocumentID7.HasValue;
                                        break;
                                    }
                                case 8:
                                    {
                                        hasAttachment = att.DocumentID8.HasValue;
                                        break;
                                    }
                                case 9:
                                    {
                                        hasAttachment = att.DocumentID9.HasValue;
                                        break;
                                    }
                                case 10:
                                    {
                                        hasAttachment = att.DocumentID10.HasValue;
                                        break;
                                    }
                                case 11:
                                    {
                                        hasAttachment = att.DocumentID11.HasValue;
                                        break;
                                    }
                                case 12:
                                    {
                                        hasAttachment = att.DocumentID12.HasValue;
                                        break;
                                    }
                                case 13:
                                    {
                                        hasAttachment = att.DocumentID13.HasValue;
                                        break;
                                    }
                                case 14:
                                    {
                                        hasAttachment = att.DocumentID14.HasValue;
                                        break;
                                    }
                            }
                        }


                        attList.Add(new ServiceModels.Report.ReportProjectRequest.ProjectAttachment
                        {
                            HasAttachment = hasAttachment,
                            ProjectAttachmentName = Common.Web.WebUtility.ParseToThaiNumber(lov.LOVName)
                        });
                    }

                    result.IsCompleted = true;
                    result.Data = attList;
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> GetListProjectBudget(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget>();
            try
            {
                var data = (from b in _db.ProjectBudgets
                            where b.ProjectID == projectID
                            select new ServiceModels.Report.ReportProjectRequest.ProjectBudget
                            {
                                ProjectBudgetID = b.ProjectBudgetID,
                                BudgetDetail = b.BudgetDetail,
                                BudgetValue = b.BudgetValue
                            }).OrderBy(or => or.ProjectBudgetID).ToList();

                if (data != null)
                {
                    ServiceModels.Report.ReportProjectRequest.ProjectBudget item;
                    for (int i = 0; i < data.Count; i++)
                    {
                        item = data[i];
                        item.BudgetDetail = Common.Web.WebUtility.ParseToThaiNumber(item.BudgetDetail);
                    }
                }

                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> GetListProjectBudgetNew(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget>();
            try
            {
                var data = (from b in _db.ProjectBudgets
                            where b.ProjectID == projectID
                            orderby b.ACTIVITYID, b.ProjectBudgetID
                            select new ServiceModels.Report.ReportProjectRequest.ProjectBudget
                            {
                                ProjectBudgetID = b.ProjectBudgetID,
                                BudgetDetail = b.BudgetDetail,
                                BudgetValue = b.BudgetValue
                            }).ToList();

                if (data != null)
                {
                    ServiceModels.Report.ReportProjectRequest.ProjectBudget item;
                    for (int i = 0; i < data.Count; i++)
                    {
                        item = data[i];
                        item.BudgetDetail = Common.Web.WebUtility.ParseToThaiNumber(item.BudgetDetail);
                    }
                }

                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        //public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> GetListProjectBudgetNew(decimal projectID)
        //{
        //    ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget>();
        //    try
        //    {

        //        List<Nep.Project.DBModels.Model.PROJECTQUESTION> qn = new List<DBModels.Model.PROJECTQUESTION>();
        //        var data = new List<ServiceModels.Report.ReportProjectRequest.ProjectBudget>();


        //        var qnh = (from h in _db.PROJECTQUESTIONHDs where h.PROJECTID == projectID && h.QUESTGROUP == "BUDGET" orderby h.QUESTHDID descending select h).FirstOrDefault();
        //        if (qnh != null)
        //        {
        //            qn = (from q in _db.PROJECTQUESTIONs where q.QUESTHDID == qnh.QUESTHDID select q).ToList();
        //        }

        //        var bgType = qn.Where(w => w.QFIELD == "BudgetType").FirstOrDefault();
        //        if (bgType != null)
        //        {
        //            if (bgType.QVALUE == "1")
        //            {
        //                var bg = GenerateBudgetType1(qn);
        //                if (bg.IsCompleted)
        //                {
        //                    result.Data = bg.Data;
        //                }
        //                else
        //                {
        //                    result.IsCompleted = false;
        //                    result.Message.Add(bg.Message[0]);
        //                    return result;
        //                }
        //            }
        //            else
        //            {

        //            }

        //        }
        //        else
        //        {
        //            result.IsCompleted = false;
        //            result.Message.Add("Budget type is not selected.");
        //            return result;
        //        }


        //        result.IsCompleted = true;
        //        //  result.Data = data; // data;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsCompleted = false;
        //        result.Message.Add(ex.Message);
        //        Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
        //    }
        //    return result;
        //}
        //private ReturnObject<List<ServiceModels.Report.ReportProjectRequest.ProjectBudget>> GenerateBudgetType1(List<Nep.Project.DBModels.Model.PROJECTQUESTION> qn)
        //{
        //    var result = new ReturnObject<List<ServiceModels.Report.ReportProjectRequest.ProjectBudget>>();
        //    List<ServiceModels.Report.ReportProjectRequest.ProjectBudget> data = new List<ServiceModels.Report.ReportProjectRequest.ProjectBudget>();
        //    decimal sum;
        //    sum = QuestionareHelper.SumQNValue(qn, new string[] { "B1_1_1_2_M_total", "B1_1_1_2_L_total", "B1_1_1_2_D_total" });
        //    if (sum > 0)
        //    {
        //        data.Add(new ServiceModels.Report.ReportProjectRequest.ProjectBudget()
        //        {
        //            BudgetDetail = Nep.Project.Common.Web.WebUtility.ParseToThaiNumber("ค่าอาหาร (จัดอาหารครบ 3 มื้อ)"),
        //            GroupNO = "1"
        //        });
        //    }
        //    result.Data = data;
        //    result.IsCompleted = true;
        //    return result;
        //}
        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectCommittee> GetListProjectCommitteet(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectCommittee> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectCommittee>();
            try
            {
                var dbComm = _db.ProjectCommittees.Where(x => x.ProjectID == projectID).OrderBy(od => od.OrderNo).ToList();
                List<ServiceModels.Report.ReportProjectRequest.ProjectCommittee> list = new List<ServiceModels.Report.ReportProjectRequest.ProjectCommittee>();
                ServiceModels.Report.ReportProjectRequest.ProjectCommittee obj;
                DBModels.Model.ProjectCommittee dbObj;
                String commPos;
                int commPos3Count = 0;
                int commPos3No = 0;
                String firstName = "";
                for (int i = 0; i < dbComm.Count; i++)
                {
                    obj = new ServiceModels.Report.ReportProjectRequest.ProjectCommittee();
                    dbObj = dbComm[i];
                    firstName = Common.Web.WebUtility.ParseToThaiNumber(dbObj.Firstname);
                    commPos = dbObj.CommitteePosition;
                    if (commPos == "1")
                    {
                        obj.No = (i + 1);
                        firstName = String.Format("ประธาน/นายก {0}", firstName);
                    }
                    else if (commPos == "2")
                    {
                        obj.No = (i + 1);
                        firstName = String.Format("กรรมการ {0}", firstName);
                    }
                    else if (commPos == "3")
                    {
                        commPos3Count++;
                        commPos3No++;

                        if (commPos3Count == 1)
                        {
                            obj.No = (i + 1);
                            firstName = String.Format("เจ้าหน้าที่  {0}. {1}", Common.Web.WebUtility.ToThaiNumber(commPos3No, "####"), firstName);
                        }
                        else
                        {
                            firstName = String.Format("              {0}. {1}", Common.Web.WebUtility.ToThaiNumber(commPos3No, "####"), firstName);
                        }
                    }
                    else
                    {
                        obj.No = (i + 1);
                    }

                    obj.FirstName = firstName;
                    obj.LastName = Common.Web.WebUtility.ParseToThaiNumber(dbObj.LastName);
                    if (string.IsNullOrEmpty(dbObj.Position.Trim()) && dbObj.POSCODE != null)
                    {
                        obj.Position = Common.Web.WebUtility.ParseToThaiNumber(dbObj.K_MT_POSITION.POSNAME);
                    }
                    else
                    {
                        obj.Position = Common.Web.WebUtility.ParseToThaiNumber(dbObj.Position);
                    }


                    list.Add(obj);
                }

                result.Data = list;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup> GetListProjectTargetGroup(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup>();
            try
            {
                var data = (from tg in _db.ProjectTargetGroups
                            where tg.ProjectID == projectID
                            select new ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup
                            {
                                TargetGroupName = _db.MT_ListOfValue.Where(x => x.LOVID == tg.TargetGroupID).Select(obj => obj.LOVName).FirstOrDefault(),
                                TargetGroupEtc = tg.TargetGroupEtc,
                                Male = tg.Male,
                                Female = tg.Female,
                                TargetGroupAmt = tg.TargetGroupAmt
                            }).ToList();

                if (data != null)
                {
                    ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup item;
                    for (int i = 0; i < data.Count; i++)
                    {
                        item = data[i];
                        item.TargetGroupName = (!String.IsNullOrEmpty(item.TargetGroupEtc)) ? Common.Web.WebUtility.ParseToThaiNumber(item.TargetGroupEtc) : Common.Web.WebUtility.ParseToThaiNumber(item.TargetGroupName);
                        item.TargetGroupEtc = Common.Web.WebUtility.ParseToThaiNumber(item.TargetGroupEtc);
                    }
                }


                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }


        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress> GetListProjectOperationAddres(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress> result = new ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress>();
            try
            {
                List<ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress> list;
                list = (from p in _db.ProjectOperationAddresses
                        where p.ProjectID == projectID
                        select new ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress
                        {
                            ProjectID = p.ProjectID,
                            Address = p.Address,
                            Building = p.Building,
                            Moo = p.Moo,
                            Soi = p.Soi,
                            SubDistrict = p.SubDistrict,
                            District = p.District,
                            Province = p.MT_Province.ProvinceName
                        }
                       ).ToList();

                if (list != null)
                {
                    ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress item;
                    for (int i = 0; i < list.Count; i++)
                    {
                        item = list[i];
                        item.Address = Common.Web.WebUtility.ParseToThaiNumber(item.Address);
                        item.Building = Common.Web.WebUtility.ParseToThaiNumber(item.Building);
                        item.Moo = Common.Web.WebUtility.ParseToThaiNumber(item.Moo);
                        item.Soi = Common.Web.WebUtility.ParseToThaiNumber(item.Soi);
                        item.Road = Common.Web.WebUtility.ParseToThaiNumber(item.Road);
                    }
                }

                result.IsCompleted = true;
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }
        #endregion Project Request Report

        #region Project Report Summary Info Report1
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> GetReportSummaryProjectInfo1(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> result = new ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo>();
            try
            {
                var data = (from g in _db.ProjectGeneralInfoes
                            where (g.ProjectID == projectID)
                            select new ServiceModels.Report.ReportSummaryProjectInfo
                            {
                                ProjectID = g.ProjectID,

                                ApprovalStatus = (g.ProjectApproval != null) ? g.ProjectApproval.ApprovalStatus : null,

                                ProjectApprovalStatus = g.ProjectApprovalStatus.LOVCode,
                                CreatorOrganizationID = _db.SC_User.Where(x => x.UserID == g.CreatedByID).Select(y => y.OrganizationID).FirstOrDefault(),
                                ProjectProvinceID = g.ProvinceID,

                                ProjectNameTH = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameTH : null,
                                ProjectNameEN = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameEN : null,

                                OrganizationID = g.OrganizationID,
                                OrganizationNameTH = g.OrganizationNameTH,
                                OrganizationNameEN = g.OrganizationNameEN,

                                ProjectReason = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectReason : null,

                                ProjectPurpose = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectPurpose : null,

                                Method = (g.ProjectOperation != null) ? g.ProjectOperation.Method : null,

                                StartDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.StartDate : null,

                                EndDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.EndDate : null,

                                ProjectResult = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectResult : null,

                                ProjectKPI = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectKPI : null,

                                BudgetValue = g.BudgetValue,

                                BudgetReviseValue = g.BudgetReviseValue,

                                DisabilityTypeCode = (g.ProjectInformation != null) ? _db.MT_ListOfValue.Where(dis => dis.LOVID == g.ProjectInformation.DisabilityTypeID).Select(disObj => disObj.LOVCode).FirstOrDefault() : null,

                                Strategy = ((g.ProjectEvaluation != null) && (g.ProjectEvaluation.IsPassMission1 == "1")) ? Resources.UI.LabelStandardStrategic1 :
                                            ((g.ProjectEvaluation != null) && (g.ProjectEvaluation.IsPassMission1 == "2")) ? Resources.UI.LabelStandardStrategic2 :
                                            ((g.ProjectEvaluation != null) && (g.ProjectEvaluation.IsPassMission1 == "3")) ? Resources.UI.LabelStandardStrategic3 :
                                            ((g.ProjectEvaluation != null) && (g.ProjectEvaluation.IsPassMission1 == "4")) ? Resources.UI.LabelStandardStrategic4 :
                                            ((g.ProjectEvaluation != null) && (g.ProjectEvaluation.IsPassMission1 == "5")) ? Resources.UI.LabelStandardStrategic5 : null

                            }).FirstOrDefault();

                if (data != null)
                {
                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canView = functions.Contains(Common.ProjectFunction.CanViewProjectInfo);
                    if (canView)
                    {
                        data.OperationAddress = GetOperationAddressText(projectID);
                        data.ProjectTargetGroup = GetProjectTargetGroupText(projectID);
                        data.CommitteeApprovalResult = GetCommitteeApprovalResultText(projectID, data.ProjectProvinceID);
                        data.DisabledCommitteePrositionName = GetDisabledCommitteePrositionName(data.DisabilityTypeCode, data.ProjectProvinceID);

                        result.IsCompleted = true;
                        result.Data = data;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }


        #endregion Project Report Summary Info Report1

        #region Project Report Summary Info Report
        public ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> GetReportSummaryProjectInfo(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> result = new ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo>();
            try
            {
                var data = (from g in _db.ProjectGeneralInfoes
                            where g.ProjectID == projectID
                            select new ServiceModels.Report.ReportSummaryProjectInfo
                            {
                                ProjectID = g.ProjectID,

                                ApprovalStatus = (g.ProjectApproval != null) ? g.ProjectApproval.ApprovalStatus : null,

                                ProjectApprovalStatus = g.ProjectApprovalStatus.LOVCode,
                                CreatorOrganizationID = _db.SC_User.Where(x => x.UserID == g.CreatedByID).Select(y => y.OrganizationID).FirstOrDefault(),
                                ProjectProvinceID = g.ProvinceID,

                                ProjectNameTH = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameTH : null,
                                ProjectNameEN = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameEN : null,

                                OrganizationID = g.OrganizationID,
                                OrganizationNameTH = g.OrganizationNameTH,
                                OrganizationNameEN = g.OrganizationNameEN,

                                ProjectReason = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectReason : null,

                                ProjectPurpose = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectPurpose : null,

                                Method = (g.ProjectOperation != null) ? g.ProjectOperation.Method : null,

                                StartDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.StartDate : null,

                                EndDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.EndDate : null,


                                ProjectResult = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectResult : null,

                                ProjectKPI = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectKPI : null,

                                BudgetValue = g.BudgetValue,

                                BudgetReviseValue = g.BudgetReviseValue,

                                DisabilityTypeCode = (g.ProjectInformation != null) ? _db.MT_ListOfValue.Where(dis => dis.LOVID == g.ProjectInformation.DisabilityTypeID).Select(disObj => disObj.LOVCode).FirstOrDefault() : null

                            }).FirstOrDefault();

                if (data != null)
                {
                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canView = functions.Contains(Common.ProjectFunction.CanViewProjectInfo);
                    if (canView)
                    {
                        data.OperationAddress = GetOperationAddressText(projectID);
                        data.ProjectTargetGroup = GetProjectTargetGroupText(projectID);
                        data.CommitteeApprovalResult = GetCommitteeApprovalResultText(projectID, data.ProjectProvinceID);
                        data.DisabledCommitteePrositionName = GetDisabledCommitteePrositionName(data.DisabilityTypeCode, data.ProjectProvinceID);
                        result.IsCompleted = true;
                        result.Data = data;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private string GetOperationAddressText(decimal projectID)
        {
            StringBuilder address = new StringBuilder();
            List<ServiceModels.ProjectInfo.ProjectOperationAddress> list = MappProjectOperationAddress(projectID);
            ServiceModels.ProjectInfo.ProjectOperationAddress item;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    item = list[i];
                    address.AppendFormat("เลขที่/สถานที่ดำเนินการ {0}", item.Address);

                    if (!String.IsNullOrEmpty(item.Building))
                    {
                        address.AppendFormat(" อาคาร{0}", item.Building);
                    }

                    if (!String.IsNullOrEmpty(item.Moo))
                    {
                        address.AppendFormat(" หมู่ {0}", item.Moo);
                    }

                    if (!String.IsNullOrEmpty(item.Soi))
                    {
                        address.AppendFormat(" ซอย{0}", item.Soi);
                    }

                    if (!String.IsNullOrEmpty(item.Road))
                    {
                        address.AppendFormat(" ถนน{0}", item.Road);
                    }

                    if (!String.IsNullOrEmpty(item.SubDistrict))
                    {
                        address.AppendFormat(" แขวง/ตำบล {0}", item.SubDistrict);
                    }

                    if (!String.IsNullOrEmpty(item.District))
                    {
                        address.AppendFormat(" เขต/อำเภอ {0}", item.District);
                    }

                    if (!String.IsNullOrEmpty(item.Province))
                    {
                        address.AppendFormat(" จังหวัด{0}", item.Province);
                    }

                    address.Append("\n");
                }
            }

            return address.ToString();
        }

        private String GetProjectTargetGroupText(decimal projectID)
        {
            StringBuilder text = new StringBuilder();
            List<DBModels.Model.ProjectTargetGroup> list = _db.ProjectTargetGroups.Where(x => x.ProjectID == projectID).ToList();


            var data = (from tg in _db.ProjectTargetGroups
                        join tgName in _db.MT_ListOfValue on tg.TargetGroupID equals tgName.LOVID
                        where tg.ProjectID == projectID
                        select new ServiceModels.ProjectInfo.ProjectTarget
                        {
                            TargetName = tgName.LOVName,
                            TargetOtherName = tg.TargetGroupEtc,
                            Amount = tg.TargetGroupAmt,
                            ProjectTargetID = tg.ProjectTargetGroupID
                        }
                        ).OrderBy(or => or.ProjectTargetID).ToList();



            ServiceModels.ProjectInfo.ProjectTarget obj;
            string etc = "";
            string label = Nep.Project.Resources.UI.LabelPerson;
            string amountFormate;
            decimal totalTargetGroup = 0;
            string tgDesc = "";
            if ((data != null) && (data.Count > 0))
            {
                obj = data[0];
                //etc = (!String.IsNullOrEmpty(obj.TargetOtherName))? String.Concat(" (", obj.TargetOtherName, ") ") : "";
                //etc = (!String.IsNullOrEmpty(obj.TargetOtherName)) ? obj.TargetOtherName : "";
                tgDesc = (!String.IsNullOrEmpty(obj.TargetOtherName)) ? obj.TargetOtherName : obj.TargetName;
                amountFormate = Nep.Project.Common.Web.WebUtility.DisplayInHtml(obj.Amount, "#,###", "-");
                text.AppendFormat("{0} {1} {2}", tgDesc, amountFormate, label);

                totalTargetGroup += (obj.Amount.HasValue) ? (decimal)obj.Amount : 0;

                for (int i = 1; i < data.Count; i++)
                {
                    obj = data[i];
                    //etc = (!String.IsNullOrEmpty(obj.TargetOtherName)) ? String.Concat(" (", obj.TargetOtherName, ") ") : "";                   
                    tgDesc = (!String.IsNullOrEmpty(obj.TargetOtherName)) ? obj.TargetOtherName : obj.TargetName;
                    amountFormate = Nep.Project.Common.Web.WebUtility.DisplayInHtml(obj.Amount, "#,###", "-");
                    text.AppendFormat(",\n{0} {1} {2}", tgDesc, amountFormate, label);
                    totalTargetGroup += (obj.Amount.HasValue) ? (decimal)obj.Amount : 0;
                }

                amountFormate = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalTargetGroup, "#,###", "-");
                text.AppendFormat("\n{0} {1} {2}", Nep.Project.Resources.UI.LabelTotalProjectTargetGroup, amountFormate, label);
            }
            return text.ToString();
        }

        private String GetCommitteeApprovalResultText(decimal projectID, decimal projectProvinceID)
        {
            StringBuilder text = new StringBuilder();
            bool isCenterReviseProject = IsCenterReviseProject(projectProvinceID);
            var data = _db.ProjectApprovals.Where(x => x.ProjectID == projectID).FirstOrDefault();
            if (data != null)
            {
                if (isCenterReviseProject)
                {
                    text.Append(data.ApprovalStatus2.LOVName);
                    if (!String.IsNullOrEmpty(data.ApprovalDesc2))
                    {
                        text.Append("\n");
                        text.Append(data.ApprovalDesc2);
                    }
                }
                else
                {
                    text.Append(data.ApprovalStatus1.LOVName);
                    if (!String.IsNullOrEmpty(data.ApprovalDesc1))
                    {
                        text.Append("\n");
                        text.Append(data.ApprovalDesc1);
                    }
                }
            }
            return text.ToString();
        }

        private String GetDisabledCommitteePrositionName(string disabledTypeCode, decimal projectProvinceID)
        {
            bool isCenterReviseProject = IsCenterReviseProject(projectProvinceID);
            string text = "";
            string disabilityCommitteeCode = "";

            if (isCenterReviseProject)
            {
                if (disabledTypeCode == Common.LOVCode.Disabilitytype.ทุกประเภทความพิการ)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทุกประเภท;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางการเคลื่อนไหวหรือร่างกาย)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางกายหรือการเคลื่อนไหว;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางการเรียนรู้)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางการเรียนรู้;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางการเห็น)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางการเห็น;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางการได้ยินหรือสื่อความหมาย)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางการได้ยินหรือสื่อความหมาย;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางจิตใจหรือพฤติกรรม)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางจิตใจหรือพฤติกรรม;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางสติปัญญา)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลพิการทางสติปัญญา;
                }
                else if (disabledTypeCode == Common.LOVCode.Disabilitytype.ประเภททางออทิสติก)
                {
                    disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะอนุกรรมการบุคคลออทิสติ;
                }
            }
            else
            {
                disabilityCommitteeCode = Common.LOVCode.Disabilitycommittee.คณะกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด;
            }



            if (!String.IsNullOrEmpty(disabilityCommitteeCode))
            {
                var data = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.DisabilityCommittee) && (x.LOVCode == disabilityCommitteeCode))
                        .Select(y => y.LOVName).FirstOrDefault();
                if (data != null)
                {
                    text = data;
                }
            }
            else
            {
                text = Nep.Project.Resources.UI.LabelDisabilityCommitteeTemp;
            }

            return text;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail> GetListReportSummaryProjectInfoBudgetDetail(decimal projectID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail>();
            try
            {
                var data = _db.ProjectBudgets.Where(x => x.ProjectID == projectID).Select(y => new ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail
                {
                    ProjectBudgetID = y.ProjectBudgetID,
                    Detail = y.BudgetDetail,
                    RevisedDetail = y.BudgetDetailRevise,
                    RequestValue = y.BudgetValue,
                    ReviseValue = y.BudgetReviseValue,
                    ReviseValue1 = y.BudgetReviseValue1,
                    ReviseValue2 = y.BudgetReviseValue2,
                    RemarkApproval = y.RemarkApproval
                }).OrderBy(or => or.ProjectBudgetID).ToList();

                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        #endregion Project Report Summary Info Report



        #region Project Report Result
        public ReturnObject<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo> GetReportProjectResult(decimal projectID)
        {
            ServiceModels.ReturnObject<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo> result = new ReturnObject<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo>();
            try
            {
                var data = (from g in _db.ProjectGeneralInfoes
                            where g.ProjectID == projectID
                            select new ServiceModels.Report.ReportProjectResult.GeneralProjectInfo
                            {
                                ProjectID = g.ProjectID,

                                ApprovalStatus = (g.ProjectApproval != null) ? g.ProjectApproval.ApprovalStatus : null,

                                ProjectApprovalStatus = g.ProjectApprovalStatus.LOVCode,
                                FollowupStatusCode = (g.FollowUpStatus != null) ? _db.MT_ListOfValue.Where(followup => followup.LOVID == (decimal)g.FollowUpStatus).Select(fObj => fObj.LOVCode).FirstOrDefault() : null,
                                CreatorOrganizationID = _db.SC_User.Where(user => user.UserID == g.CreatedByID).Select(userObj => userObj.OrganizationID).FirstOrDefault(),
                                ProjectProvinceID = g.ProvinceID,

                                BudgetYear = (g.ProjectInformation != null) ? g.ProjectInformation.BudgetYear : 0,
                                OrganizationID = g.OrganizationID,
                                OganizationNameTH = g.OrganizationNameTH,
                                OganizationNameEN = g.OrganizationNameEN,
                                Address = g.Address,
                                Building = g.Building,
                                Moo = g.Moo,
                                Soi = g.Soi,
                                Road = g.Road,
                                SubDistrict = g.SubDistrict,
                                District = g.District,
                                AddressProvinceName = _db.MT_Province.Where(prov => prov.ProvinceID == g.AddressProvinceID).Select(provObj => provObj.ProvinceName).FirstOrDefault(),
                                Postcode = g.Postcode,
                                Telephone = g.Telephone,
                                Fax = g.Fax,
                                Email = g.Email,

                                PrefixName1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Prefix1Personel.LOVName : null,
                                Firstname1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Firstname1 : null,
                                Lastname1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Lastname1 : null,
                                Address1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Address1 : null,
                                Building1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Building1 : null,
                                Moo1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Moo1 : null,
                                Soi1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Soi1 : null,
                                Road1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Road1 : null,
                                SubDistrict1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.SubDistrict1 : null,
                                District1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.District1 : null,
                                ProvinceName1 = (g.ProjectPersonel != null) ? _db.MT_Province.Where(prov1 => prov1.ProvinceID == g.ProjectPersonel.ProvinceID1)
                                    .Select(prov1Obj => prov1Obj.ProvinceName).FirstOrDefault() : null,
                                PostCode1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.PostCode1 : null,
                                Telephone1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Telephone1 : null,
                                Fax1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Fax1 : null,
                                Email1 = (g.ProjectPersonel != null) ? g.ProjectPersonel.Email1 : null,

                                ProjectNameTH = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameTH : null,
                                ProjectNameEN = (g.ProjectInformation != null) ? g.ProjectInformation.ProjectNameEN : null,

                                OperationAddress = (g.ProjectOperation != null) ? g.ProjectOperation.Address : null,
                                OperationBuilding = (g.ProjectOperation != null) ? g.ProjectOperation.Building : null,
                                OperationMoo = (g.ProjectOperation != null) ? g.ProjectOperation.Moo : null,
                                OperationSoi = (g.ProjectOperation != null) ? g.ProjectOperation.Soi : null,
                                OperationRoad = (g.ProjectOperation != null) ? g.ProjectOperation.Road : null,
                                OperationSubDistrict = (g.ProjectOperation != null) ? g.ProjectOperation.SubDistrict : null,
                                OperationDistrict = (g.ProjectOperation != null) ? g.ProjectOperation.District : null,
                                OperationProvince = (g.ProjectOperation != null) ? g.ProjectOperation.MT_Province.ProvinceName : null,

                                StartDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.StartDate : (DateTime?)null,
                                EndDate = (g.ProjectOperation != null) ? (DateTime?)g.ProjectOperation.EndDate : (DateTime?)null,
                                TotalDay = (g.ProjectOperation != null) ? (Decimal?)g.ProjectOperation.TotalDay : (Decimal?)null,

                                ActivityDescription = (g.ProjectReport != null) ? g.ProjectReport.ActivityDescription : null,

                                BudgetRequest = (g.BudgetValue != null) ? (Decimal)g.BudgetValue : 0,
                                BudgetRevised = (g.BudgetReviseValue != null) ? (Decimal)g.BudgetReviseValue : 0,
                                ActualExpense = ((g.ProjectReport != null) && (g.ProjectReport.ActualExpense != null)) ? (Decimal)g.ProjectReport.ActualExpense : 0,

                                Benefit = (g.ProjectReport != null) ? g.ProjectReport.Benefit : null,

                                MaleParticipant = (g.ProjectReport != null) ? g.ProjectReport.MaleParticipant : (Decimal?)null,
                                FemaleParticipant = (g.ProjectReport != null) ? g.ProjectReport.FemaleParticipant : (Decimal?)null,

                                ProblemsAndObstacle = (g.ProjectReport != null) ? g.ProjectReport.ProblemsAndObstacle : null,

                                Suggestion = (g.ProjectReport != null) ? g.ProjectReport.Suggestion : null,

                                ReporterName1 = (g.ProjectReport != null) ? g.ProjectReport.ReporterName1 : null,
                                ReporterLastname1 = (g.ProjectReport != null) ? g.ProjectReport.ReporterLastname1 : null,
                                Position1 = (g.ProjectReport != null) ? g.ProjectReport.Position1 : null,
                                ReportDate1 = (g.ProjectReport != null) ? g.ProjectReport.RepotDate1 : null,
                                ReporterTelephone1 = (g.ProjectReport != null) ? g.ProjectReport.Telephone1 : null,

                                SuggestionDesc = (g.ProjectReport != null) ? g.ProjectReport.SuggestionDesc : null,
                                ReporterName2 = (g.ProjectReport != null) ? g.ProjectReport.ReporterName2 : null,
                                ReporterLastname2 = (g.ProjectReport != null) ? g.ProjectReport.ReporterLastname2 : null,
                                Position2 = (g.ProjectReport != null) ? g.ProjectReport.Position2 : null,
                                ReportDate2 = (g.ProjectReport != null) ? g.ProjectReport.RepotDate2 : null,

                            }).FirstOrDefault();

                if (data != null)
                {
                    bool isReport = ((data.FollowupStatusCode != null) && (data.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว)) ? true : false;
                    List<Common.ProjectFunction> functions = this.GetProjectFunction(projectID).Data;
                    bool canPrint = functions.Contains(Common.ProjectFunction.PrintReport);

                    if (canPrint)
                    {
                        data.OganizationHeadFullName = GetOrganizationHeadName(projectID);

                        result.IsCompleted = true;
                        result.Data = data;
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.CanotViewProjectData);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }


            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        private String GetOrganizationHeadName(decimal projectID)
        {
            string name = "-";
            var data = _db.ProjectCommittees.Where(x => (x.ProjectID == projectID) && (x.CommitteePosition == "1")).FirstOrDefault();
            if (data != null)
            {
                name = String.Format("{0} {1}        ตำแหน่ง {2}", data.Firstname, data.LastName, data.Position);
            }
            return name;
        }

        public ReturnQueryData<ServiceModels.Report.ReportProjectResult.ProjectType> GetListReportProjectResultProjectType(decimal projectID)
        {
            ReturnQueryData<ServiceModels.Report.ReportProjectResult.ProjectType> result =
                new ReturnQueryData<ServiceModels.Report.ReportProjectResult.ProjectType>();
            try
            {
                List<ServiceModels.Report.ReportProjectResult.ProjectType> list = new List<ServiceModels.Report.ReportProjectResult.ProjectType>();

                var tmpProjectTypeID = _db.ProjectInformations.Where(x => x.ProjectID == projectID).Select(y => y.ProjectTypeID).FirstOrDefault();
                var projectTypes = _db.MT_ListOfValue.Where(x => x.LOVGroup == Common.LOVGroup.ProjectType).OrderBy(or => or.OrderNo).ToList();

                decimal projectTypeId = (tmpProjectTypeID != null) ? (decimal)tmpProjectTypeID : -1;
                bool isSelected = false;
                DBModels.Model.MT_ListOfValue dbType;
                ServiceModels.Report.ReportProjectResult.ProjectType pType;
                if (projectTypes != null)
                {
                    for (int i = 0; i < projectTypes.Count; i++)
                    {
                        dbType = projectTypes[i];
                        isSelected = (dbType.LOVID == projectTypeId);
                        pType = new ServiceModels.Report.ReportProjectResult.ProjectType
                        {
                            No = (Int32)dbType.OrderNo,
                            IsSelected = isSelected,
                            ProjectTypeCode = dbType.LOVCode,
                            ProjectTypeName = dbType.LOVName
                        };
                        list.Add(pType);
                    }
                }

                result.Data = list;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }

        public ReturnQueryData<ServiceModels.Report.ReportProjectResult.SummaryProjectResult> GetListReportProjectResultSummaryProjectResult(decimal projectID, string summaryResultCode)
        {
            ReturnQueryData<ServiceModels.Report.ReportProjectResult.SummaryProjectResult> result =
                new ReturnQueryData<ServiceModels.Report.ReportProjectResult.SummaryProjectResult>();
            try
            {
                List<ServiceModels.Report.ReportProjectResult.SummaryProjectResult> summaryList = new List<ServiceModels.Report.ReportProjectResult.SummaryProjectResult>();
                List<DBModels.Model.MT_ListOfValue> dbSummaryResults = _db.MT_ListOfValue.Where(x => x.LOVGroup == summaryResultCode).OrderBy(or => or.OrderNo).ToList();

                DBModels.Model.ProjectReport pReport = _db.ProjectReports.Where(x => x.ProjectID == projectID).FirstOrDefault();
                decimal? tmpOperationResultID = (pReport != null) ? ((summaryResultCode == Common.LOVGroup.OperationResult) ? pReport.OperationResult : pReport.OperationLevel) : (decimal?)null;
                decimal operationResultID = (tmpOperationResultID.HasValue) ? (decimal)tmpOperationResultID : 0;

                DBModels.Model.MT_ListOfValue dbObj;
                ServiceModels.Report.ReportProjectResult.SummaryProjectResult summary;
                bool isSelect = false;
                string summaryDesc;
                if (dbSummaryResults != null)
                {
                    for (int i = 0; i < dbSummaryResults.Count; i++)
                    {
                        dbObj = dbSummaryResults[i];
                        isSelect = (dbObj.LOVID == operationResultID);
                        summaryDesc = null;
                        if (isSelect && (dbObj.LOVCode == Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ))
                        {
                            summaryDesc = (pReport != null) ? pReport.OperationLevelDesc : null;
                        }

                        summary = new ServiceModels.Report.ReportProjectResult.SummaryProjectResult
                        {
                            IsSelected = isSelect,
                            Code = dbObj.LOVCode,
                            Name = dbObj.LOVName,
                            Description = summaryDesc
                        };

                        summaryList.Add(summary);
                    }
                }

                result.Data = summaryList;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }
            return result;
        }
        #endregion Project Report Result

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetListProjectProvince()
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ReturnQueryData<GenericDropDownListData>();
            try
            {

                string centerProvinceAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                DBModels.Model.MT_Province centerProvince = _db.MT_Province.Where(x => x.ProvinceAbbr == centerProvinceAbbr).FirstOrDefault();

                List<ServiceModels.GenericDropDownListData> list = new List<GenericDropDownListData>();
                if (_user.IsCenterOfficer)
                {


                    list = _db.MT_Province.Where(x => x.ProvinceAbbr != centerProvince.ProvinceAbbr)
                        .Select(y => new ServiceModels.GenericDropDownListData
                        {
                            Value = y.ProvinceID.ToString(),
                            Text = y.ProvinceName
                        }).OrderBy(or => or.Text).ToList();
                    list.Insert(0, new GenericDropDownListData { Text = centerProvince.ProvinceName, Value = centerProvince.ProvinceID.ToString() });
                }
                else
                {

                    DBModels.Model.MT_Province userProvince = _db.MT_Province.Where(x => x.ProvinceID == _user.ProvinceID).FirstOrDefault();

                    if (!_user.IsProvinceOfficer)
                    {
                        list.Add(new ServiceModels.GenericDropDownListData { Text = centerProvince.ProvinceName, Value = centerProvince.ProvinceID.ToString() });
                    }

                    list.Add(new ServiceModels.GenericDropDownListData { Text = userProvince.ProvinceName, Value = userProvince.ProvinceID.ToString() });
                }

                result.IsCompleted = true;
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }


        public ServiceModels.ReturnObject<ServiceModels.GenericDropDownListData> GetProjectProvince(decimal projectid)
        {
            ServiceModels.ReturnObject<ServiceModels.GenericDropDownListData> result = new ReturnObject<GenericDropDownListData>();
            try
            {
                result.Data = (from g in _db.ProjectGeneralInfoes
                               join prov in _db.MT_Province on g.AddressProvinceID equals prov.ProvinceID
                               where g.ProjectID == projectid
                               select new ServiceModels.GenericDropDownListData
                               {
                                   Value = g.AddressProvinceID.ToString(),
                                   Text = prov.ProvinceName
                               }).FirstOrDefault();

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        #region Manage Cancelled Project Request
        public ServiceModels.ReturnObject<ServiceModels.KendoAttachment> GetCancelledProjectRequest(decimal attachmentID)
        {
            ServiceModels.ReturnObject<ServiceModels.KendoAttachment> result = new ReturnObject<KendoAttachment>();
            try
            {
                var data = _db.MT_Attachment.Where(x => x.AttachmentID == attachmentID).FirstOrDefault();
                if (data != null)
                {
                    ServiceModels.KendoAttachment attach = new KendoAttachment
                    {
                        id = data.AttachmentID.ToString(),
                        name = data.AttachmentFilename,
                        extension = Path.GetExtension(data.AttachmentFilename),
                        size = (int)data.FileSize
                    };
                    result.IsCompleted = true;
                    result.Data = attach;
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.KendoAttachment> SaveCancelledProjectRequest(decimal projectID, ServiceModels.KendoAttachment attachment)
        {
            ServiceModels.ReturnObject<ServiceModels.KendoAttachment> result = new ReturnObject<KendoAttachment>();
            try
            {
                String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
                String rootDestinationFolderPath = GetAttachmentRootFolder();
                String folder = PROJECT_FOLDER_NAME + projectID + "\\";
                decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_INFORMATION);

                DBModels.Model.MT_Attachment oldAttachment = null;
                DBModels.Model.ProjectInformation dbInfo = _db.ProjectInformations.Where(x => x.ProjectID == projectID).FirstOrDefault();
                DBModels.Model.ProjectGeneralInfo dbGenInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
                decimal projectApprovalStatusID = _db.MT_ListOfValue.Where(x => (x.LOVGroup == Common.LOVGroup.ProjectApprovalStatus) && (x.LOVCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกคำร้อง)).Select(y => y.LOVID).FirstOrDefault();
                decimal? currentAttachmentID = ((attachment != null) && (!String.IsNullOrEmpty(attachment.id))) ? Convert.ToDecimal(attachment.id) : (decimal?)null;
                decimal? cancelledAttachmentID = (decimal?)null;
                if (dbInfo != null)
                {
                    oldAttachment = (dbInfo.CancelledDocumentID.HasValue) ? dbInfo.CancelledDocument : null;
                    if (currentAttachmentID.HasValue && (oldAttachment != null) && (currentAttachmentID == oldAttachment.AttachmentID))
                    {
                        result.IsCompleted = true;
                        result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                        result.Data = attachment;
                        return result;
                    }
                    else
                    {
                        using (var tran = _db.Database.BeginTransaction())
                        {
                            if (attachment != null)
                            {
                                cancelledAttachmentID = (decimal)SaveFile(attachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                                dbInfo.CancelledDocumentID = cancelledAttachmentID;
                            }

                            dbGenInfo.ProjectApprovalStatusID = projectApprovalStatusID;

                            if (oldAttachment != null)
                            {
                                ServiceModels.KendoAttachment removeAttachment = new KendoAttachment
                                {
                                    id = oldAttachment.AttachmentID.ToString()
                                };
                                RemoveFile(removeAttachment, rootDestinationFolderPath);
                            }
                            _db.SaveChanges();

                            if (attachment != null)
                            {
                                ServiceModels.KendoAttachment newAttach = attachment;
                                newAttach.tempId = null;
                                newAttach.id = (cancelledAttachmentID.HasValue) ? cancelledAttachmentID.ToString() : null;
                                result.Data = newAttach;
                            }

                            result.IsCompleted = true;
                            result.Message.Add(Nep.Project.Resources.Message.SaveSuccess);
                            tran.Commit();
                        }
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Project Info", ex);
            }

            return result;
        }

        #endregion Manage Cancelled Project Request

        private decimal GetAttachmentTypeID(String attachmentTypeCode)
        {
            decimal id = _db.MT_ListOfValue.Where(x => (x.LOVCode == attachmentTypeCode) && (x.LOVGroup == Common.LOVGroup.AttachmentType))
                .Select(y => y.LOVID).FirstOrDefault();
            return id;
        }

        #region Mapping
        private DBModels.Model.ProjectGeneralInfo MappOrganizationInfoToDBProjectGeneralInfo(ServiceModels.ProjectInfo.OrganizationInfo model)
        {
            DBModels.Model.ProjectGeneralInfo dataDB = new DBModels.Model.ProjectGeneralInfo();
            ServiceModels.ProjectInfo.OrganizationInfo dataModel = model;
            Decimal projectId = model.ProjectID;

            if (projectId > 0)
            {
                dataDB = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectId).FirstOrDefault();
            }

            dataDB.ProvinceID = dataModel.ProvinceID;
            dataDB.OrganizationID = dataModel.OrganizationID;
            dataDB.OrganizationNameTH = dataModel.OrganizationNameTH;
            dataDB.OrganizationNameEN = dataModel.OrganizationNameEN;
            dataDB.OrganizationTypeID = dataModel.OrganizationTypeID;
            dataDB.OrganizationTypeEtc = dataModel.OrganizationTypeEtc;
            dataDB.OrgUnderSupport = dataModel.OrgUnderSupport;
            dataDB.OrganizationYear = dataModel.OrganizationYear;
            dataDB.OrgEstablishedDate = dataModel.OrgEstablishedDate;
            dataDB.Address = dataModel.Address;
            dataDB.Building = dataModel.Building;
            dataDB.Moo = dataModel.Moo;
            dataDB.Soi = dataModel.Soi;
            dataDB.Road = dataModel.Road;
            dataDB.SubDistrictID = dataModel.SubDistrictID;
            dataDB.SubDistrict = dataModel.SubDistrict;
            dataDB.DistrictID = dataModel.DistrictID;
            dataDB.District = dataModel.District;
            dataDB.AddressProvinceID = dataModel.AddressProvinceID;
            dataDB.Postcode = dataModel.Postcode;
            dataDB.Telephone = dataModel.Telephone;
            dataDB.Mobile = dataModel.Mobile;
            dataDB.Fax = dataModel.Fax;
            dataDB.Email = dataModel.Email;
            dataDB.Purpose = dataModel.Purpose;
            dataDB.CurrentProject = dataModel.CurrentProject;
            dataDB.CurrentProjectResult = dataModel.CurrentProjectResult;
            dataDB.GotSupportFlag = (dataModel.GotSupportFlag == true) ? "1" : "0";
            dataDB.GotSupportYear = dataModel.GotSupportYear;
            dataDB.ToGotSupportYear = (!String.IsNullOrEmpty(dataModel.TogotSupportYear)) ? dataModel.TogotSupportYear : null;
            dataDB.GotSupportTimes = dataModel.GotSupportTimes;
            dataDB.GotSupportLastProject = dataModel.GotSupportLastProject;
            dataDB.GotSupportLastResult = dataModel.GotSupportLastResult;
            dataDB.GotSupportLastProblems = dataModel.GotSupportLastProblems;

            dataDB.SourceName1 = model.Assistances[0].OrganizationName;
            dataDB.MoneySupport1 = model.Assistances[0].Amount;
            dataDB.SourceName2 = model.Assistances[1].OrganizationName;
            dataDB.MoneySupport2 = model.Assistances[1].Amount;
            dataDB.SourceName3 = model.Assistances[2].OrganizationName;
            dataDB.MoneySupport3 = model.Assistances[2].Amount;
            dataDB.SourceName4 = model.Assistances[3].OrganizationName;
            dataDB.MoneySupport4 = model.Assistances[3].Amount;

            return dataDB;
        }

        private ServiceModels.ProjectInfo.OrganizationInfo MappProjectGeneralInfoToOrganizationInfo(DBModels.Model.ProjectGeneralInfo dbModel)
        {
            ServiceModels.ProjectInfo.OrganizationInfo result = new ServiceModels.ProjectInfo.OrganizationInfo();
            DBModels.Model.ProjectGeneralInfo DataDB = dbModel;

            result.ProjectID = dbModel.ProjectID;
            result.ProvinceID = DataDB.ProvinceID;
            result.OrganizationID = DataDB.OrganizationID;
            result.OrganizationNameTH = DataDB.OrganizationNameTH;
            result.OrganizationNameEN = DataDB.OrganizationNameEN;
            result.OrganizationTypeID = DataDB.OrganizationTypeID;
            result.OrganizationTypeEtc = DataDB.OrganizationTypeEtc;
            result.OrgUnderSupport = DataDB.OrgUnderSupport;
            result.OrganizationYear = DataDB.OrganizationYear;
            result.OrgEstablishedDate = DataDB.OrgEstablishedDate;
            result.Address = DataDB.Address;
            result.Building = DataDB.Building;
            result.Moo = DataDB.Moo;
            result.Soi = DataDB.Soi;
            result.Road = DataDB.Road;
            result.SubDistrictID = DataDB.SubDistrictID;
            result.SubDistrict = DataDB.SubDistrict;
            result.DistrictID = DataDB.DistrictID;
            result.District = DataDB.District;
            result.AddressProvinceID = DataDB.AddressProvinceID;
            result.Postcode = DataDB.Postcode;
            result.Telephone = DataDB.Telephone;
            result.Mobile = DataDB.Mobile;
            result.Fax = DataDB.Fax;
            result.Email = DataDB.Email;
            result.Purpose = DataDB.Purpose;
            result.CurrentProject = DataDB.CurrentProject;
            result.CurrentProjectResult = DataDB.CurrentProjectResult;
            result.GotSupportFlag = (DataDB.GotSupportFlag == "1") ? true : false;
            result.GotSupportYear = DataDB.GotSupportYear;
            result.TogotSupportYear = DataDB.ToGotSupportYear;
            result.GotSupportTimes = DataDB.GotSupportTimes;
            result.GotSupportLastProject = DataDB.GotSupportLastProject;
            result.GotSupportLastResult = DataDB.GotSupportLastResult;
            result.GotSupportLastProblems = DataDB.GotSupportLastProblems;

            result.Committees = listCommitteeByProjectID(DataDB.ProjectID);
            result.Assistances = listAssistanceByDBModelGeneralInfo(DataDB);

            result.ProjectApprovalStatusID = dbModel.ProjectApprovalStatusID;
            result.ProjectApprovalStatusCode = dbModel.ProjectApprovalStatus.LOVCode;
            result.ProjectApprovalStatusName = dbModel.ProjectApprovalStatus.LOVName;

            result.ApprovalStatus = (dbModel.ProjectApproval != null) ? dbModel.ProjectApproval.ApprovalStatus : null;

            result.CreatorOrganizationID = _db.SC_User.Where(users => (users.UserID == dbModel.CreatedByID) && (users.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();

            if (result.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
            {
                result.RequiredSubmitData = GetKeyRequiredSubmitData(result.ProjectID);
            }

            return result;
        }

        private DBModels.Model.ProjectInformation MappTabProjectInfoToDBProjectInformation(ServiceModels.ProjectInfo.TabProjectInfo model)
        {
            DBModels.Model.ProjectInformation result = null;
            decimal id = model.ProjectID;

            result = _db.ProjectInformations.Where(x => x.ProjectID == id).FirstOrDefault();
            if (result == null)
            {
                result = new DBModels.Model.ProjectInformation();
            }

            result.ProjectID = id;

            result.ProjectNameTH = model.ProjectInfoNameTH;
            result.ProjectNameEN = model.ProjectInfoNameEN;
            result.ProjectTypeID = model.ProjectInfoType;
            result.ProjectDate = model.ProjectInfoStartDate;
            result.BudgetYear = GetBudgetYear(result.ProjectDate);
            result.DisabilityTypeID = model.TypeDisabilitys;
            result.ProjectReason = model.Principles;
            result.ProjectPurpose = model.ProjectInfoObjective;
            result.ProjectKPI = model.ProjectInfoindicator;
            result.ProjectResult = model.ProjectInfoAnticipation;

            return result;
        }

        private ServiceModels.ProjectInfo.TabProjectInfo MappDBProjectInformationToTabProjectInfo(DBModels.Model.ProjectInformation dbModel)
        {
            ServiceModels.ProjectInfo.TabProjectInfo result = new ServiceModels.ProjectInfo.TabProjectInfo();
            result.ProjectID = dbModel.ProjectID;

            result.ProjectNo = dbModel.ProjectNo;
            result.ProjectInfoNameTH = dbModel.ProjectNameTH;
            result.ProjectInfoNameEN = dbModel.ProjectNameEN;
            result.ProjectInfoType = dbModel.ProjectTypeID;
            result.ProjectInfoStartDate = dbModel.ProjectDate;
            result.BudgetYear = dbModel.BudgetYear;
            result.TypeDisabilitys = dbModel.DisabilityTypeID;
            result.Principles = dbModel.ProjectReason;
            result.ProjectInfoObjective = dbModel.ProjectPurpose;
            result.ProjectInfoindicator = dbModel.ProjectKPI;
            result.ProjectInfoAnticipation = dbModel.ProjectResult;
            result.ProjectApprovalStatusID = dbModel.ProjectGeneralInfo.ProjectApprovalStatusID;
            result.ProjectApprovalStatusCode = dbModel.ProjectGeneralInfo.ProjectApprovalStatus.LOVCode;
            result.ProjectApprovalStatusName = dbModel.ProjectGeneralInfo.ProjectApprovalStatus.LOVName;
            result.CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == dbModel.ProjectGeneralInfo.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();
            result.ProvinceID = dbModel.ProjectGeneralInfo.ProvinceID;
            result.RejectComment = dbModel.RejectComment;
            result.RejectTopic = dbModel.RejectTopic;
            //kenghot18
            result.BudgetValue = dbModel.ProjectGeneralInfo.BudgetValue;
            result.ApprovalStatus = _db.ProjectApprovals.Where(x => x.ProjectID == dbModel.ProjectID).Select(y => y.ApprovalStatus).FirstOrDefault();

            var folloupstatus = (from g in _db.ProjectGeneralInfoes.Where(x => x.ProjectID == dbModel.ProjectID)
                                 join fStaus in _db.MT_ListOfValue on g.FollowUpStatus equals fStaus.LOVID
                                 select fStaus
                                ).FirstOrDefault();
            result.FollowupStatusCode = (folloupstatus != null) ? folloupstatus.LOVCode : null;

            result.CancelledAttachmentID = dbModel.CancelledDocumentID;
            if (result.CancelledAttachmentID.HasValue)
            {
                DBModels.Model.MT_Attachment dbAttachment = dbModel.CancelledDocument;
                result.CancelledAttachment = new KendoAttachment
                {
                    id = dbAttachment.AttachmentID.ToString(),
                    name = dbAttachment.AttachmentFilename,
                    extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                    size = (int)dbAttachment.FileSize
                };
            }

            if (result.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
            {
                result.RequiredSubmitData = GetKeyRequiredSubmitData(result.ProjectID);
            }

            var projectOrgResult = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == dbModel.ProjectID).FirstOrDefault();
            result.ProjectOrganizationID = projectOrgResult.OrganizationID;
            result.SubmitedDate = projectOrgResult.SubmitedDate;

            bool isReported = (!String.IsNullOrEmpty(result.FollowupStatusCode) && (result.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว));
            result.ProjectRole = GetProjectFunction(result.ProjectID).Data;

            result.HasEvaluationInfo = (_db.ProjectEvaluations.Where(x => x.ProjectID == dbModel.ProjectID).Count() > 0);
            result.HasApprovalInfo = (_db.ProjectApprovals.Where(x => x.ProjectID == dbModel.ProjectID).Count() > 0);
            return result;
        }

        //private List<ServiceModels.ProjectInfo.ProjectTarget> MappDBProjectTargetGroupToProjectTarget(List<DBModels.Model.ProjectTargetGroup> targetGroups)
        //{
        //    List<ServiceModels.ProjectInfo.ProjectTarget> result = new List<ServiceModels.ProjectInfo.ProjectTarget>();
        //    List<DBModels.Model.ProjectTargetGroup> list = targetGroups;
        //    List<ServiceModels.GenericDropDownListData> listLOVTarget = ListProjectTarget();

        //    foreach (var item in list)
        //    {
        //        string targetName = listLOVTarget.Where(x => x.Value == item.TargetGroupID.ToString()).Select(x => x.Text).FirstOrDefault();

        //        ServiceModels.ProjectInfo.ProjectTarget target = new ServiceModels.ProjectInfo.ProjectTarget();
        //        target.UID = Guid.NewGuid().ToString();
        //        target.ProjectID = item.ProjectID;
        //        target.ProjectTargetID = item.ProjectTargetGroupID;
        //        target.TargetID = item.TargetGroupID;
        //        target.TargetName = targetName; //Not Reference
        //        target.TargetOtherName = item.TargetGroupEtc;               
        //        target.Amount = item.TargetGroupAmt;
        //        result.Add(target);
        //    }

        //    return result;
        //}
        private string SaveDraftText(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return " ";
            }
            else
            {
                return s;
            }
        }
        private DBModels.Model.ProjectPersonel MappTabProjectPersonalToDBProjectPersonal(ServiceModels.ProjectInfo.TabPersonal model, List<DBModels.Model.MT_District> districtList, List<DBModels.Model.MT_SubDistrict> subDistrictList)
        {
            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
            decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_PERSONAL);

            DBModels.Model.ProjectPersonel result = null;
            decimal id = model.ProjectID;

            result = _db.ProjectPersonels.Where(x => x.ProjectID == id).FirstOrDefault();
            if (result == null)
            {
                result = new DBModels.Model.ProjectPersonel();
            }

            result.IDCardNo = model.IDCardNo;

            if (model.Address1 != null)
            {
                result.Prefix1 = model.Address1.Prefix1;
                result.Firstname1 = SaveDraftText(model.Address1.Firstname1);
                result.Lastname1 = SaveDraftText(model.Address1.Lastname1);
                result.Address1 = SaveDraftText(model.Address1.Address1);
                result.Moo1 = model.Address1.Moo1;
                result.Building1 = model.Address1.Building1;
                result.Soi1 = model.Address1.Soi1;
                result.Road1 = model.Address1.Road1;
                result.SubDistrictID1 = model.Address1.SubDistrictID1;
                //save draft
                if (result.SubDistrictID1 != null)
                    result.SubDistrict1 = subDistrictList.Where(x => x.SubDistrictID == (decimal)model.Address1.SubDistrictID1).Select(y => y.SubDistrictName).FirstOrDefault();
                else
                    result.SubDistrict1 = " ";
                result.DistrictID1 = model.Address1.DistrictID1;
                //save draft
                if (result.DistrictID1 != null)
                    result.District1 = districtList.Where(x => x.DistrictID == (decimal)model.Address1.DistrictID1).Select(y => y.DistrictName).FirstOrDefault();
                else
                    result.District1 = " ";
                result.ProvinceID1 = model.Address1.ProvinceID1;
                result.PostCode1 = model.Address1.PostCode1;
                result.Telephone1 = SaveDraftText(model.Address1.Telephone1);
                result.Mobile1 = model.Address1.Mobile1;
                result.Fax1 = model.Address1.Fax1;
                result.Email1 = model.Address1.Email1;
            }

            if (model.Address2 != null)
            {
                result.Prefix2 = model.Address2.Prefix2;
                result.Firstname2 = SaveDraftText(model.Address2.Firstname2);
                result.Lastname2 = SaveDraftText(model.Address2.Lastname2);
                result.Address2 = SaveDraftText(model.Address2.Address2);
                result.Moo2 = model.Address2.Moo2;
                result.Building2 = model.Address2.Building2;
                result.Soi2 = model.Address2.Soi2;
                result.Road2 = model.Address2.Road2;
                result.SubDistrictID2 = model.Address2.SubDistrictID2;
                //save draft
                if (result.SubDistrictID2 != null)
                    result.SubDistrict2 = subDistrictList.Where(x => x.SubDistrictID == (decimal)model.Address2.SubDistrictID2).Select(y => y.SubDistrictName).FirstOrDefault();
                else
                    result.SubDistrict2 = " ";
                result.DistrictID2 = model.Address2.DistrictID2;
                if (result.DistrictID2 != null)
                    result.District2 = districtList.Where(x => x.DistrictID == (decimal)model.Address2.DistrictID2).Select(y => y.DistrictName).FirstOrDefault();
                else
                    result.District2 = " ";
                result.ProvinceID2 = model.Address2.ProvinceID2;
                result.PostCode2 = model.Address2.PostCode2;
                result.Telephone2 = SaveDraftText(model.Address2.Telephone2);
                result.Fax2 = model.Address2.Fax2;
                result.Email2 = model.Address2.Email2;
            }


            if ((model.Address3 != null) && (!String.IsNullOrEmpty(model.Address3.Firstname3)))
            {
                result.Prefix3 = model.Address3.Prefix3;
                result.Firstname3 = model.Address3.Firstname3;
                result.Lastname3 = model.Address3.Lastname3;
                result.Address3 = model.Address3.Address3;
                result.Moo3 = model.Address3.Moo3;
                result.Building3 = model.Address3.Building3;
                result.Soi3 = model.Address3.Soi3;
                result.Road3 = model.Address3.Road3;
                result.SubDistrictID3 = model.Address3.SubDistrictID3;
                result.SubDistrict3 = subDistrictList.Where(x => x.SubDistrictID == (decimal)model.Address3.SubDistrictID3).Select(y => y.SubDistrictName).FirstOrDefault();
                result.DistrictID3 = model.Address3.DistrictID3;
                result.District3 = districtList.Where(x => x.DistrictID == (decimal)model.Address3.DistrictID3).Select(y => y.DistrictName).FirstOrDefault();
                result.ProvinceID3 = model.Address3.ProvinceID3;
                result.PostCode3 = model.Address3.PostCode3;
                result.Telephone3 = model.Address3.Telephone3;
                result.Fax3 = model.Address3.Fax3;
                result.Email3 = model.Address3.Email3;
            }
            else
            {
                result.Prefix3 = (decimal?)null;
                result.Firstname3 = string.Empty;
                result.Lastname3 = string.Empty;
                result.Address3 = string.Empty;
                result.Moo3 = string.Empty;
                result.Building3 = string.Empty;
                result.Soi3 = string.Empty;
                result.Road3 = string.Empty;
                result.SubDistrictID3 = (decimal?)null;
                result.SubDistrict3 = string.Empty;
                result.DistrictID3 = (decimal?)null;
                result.District3 = string.Empty;
                result.ProvinceID3 = (decimal?)null;
                result.PostCode3 = string.Empty;
                result.Telephone3 = string.Empty;
                result.Fax3 = string.Empty;
                result.Email3 = string.Empty;
            }

            result.SupportPlace1 = model.SupportPlace1;
            result.SupportOrgName1 = model.SupportOrgName1;
            result.InstructorAmt2 = model.InstructorAmt2;
            result.SupportOrgName2 = model.SupportOrgName2;
            result.InstructorListFileID2 = model.InstructorListFileID2;
            result.SupportBudgetAmt3 = model.SupportBudgetAmt3;
            result.SupportOrgName3 = model.SupportOrgName3;
            result.SupportEquipment4 = model.SupportEquipment4;
            result.SupportOrgName4 = model.SupportOrgName4;
            result.SupportDrinkFoodAmt5 = model.SupportDrinkFoodAmt5;
            result.SupportOrgName5 = model.SupportOrgName5;
            result.SupportOrgName6 = model.SupportOrgName6;
            result.VehicleListFileID6 = model.VehicleListFile6;
            result.SupportValunteerAmt7 = model.SupportValunteerAmt7;
            result.SupportOrgName7 = model.SupportOrgName7;
            result.ValunteerListFileID7 = model.ValunteerListFileID7;
            result.SupportOther8 = model.SupportOther8;
            result.SupportOrgName8 = model.SupportOrgName8;


            // Instructor File
            if (model.AddedInstructorAttachment != null)
            {
                //kenghot

                //result.InstructorListFileID2 = SaveFile(model.AddedInstructorAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                foreach (KendoAttachment k in model.AddedInstructorAttachments)
                {
                    var attID = SaveFile(k, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
                    _db.K_FILEINTABLE.Add(new DBModels.Model.K_FILEINTABLE
                    {
                        ATTACHMENTID = attID.Value,
                        FIELDNAME = PERSON_INSTRUCTOR,
                        TABLENAME = TABLE_PROJECTPERSONEL,
                        TABLEROWID = model.ProjectID
                    });
                }

                //End kenghot
            }
            if (model.RemovedInstructorAttachment != null)
            {
                //kenghot
                //RemoveFile(model.RemovedInstructorAttachment, rootDestinationFolderPath);
                result.InstructorListFileID2 = (decimal?)null;
                foreach (KendoAttachment k in model.RemovedInstructorAttachments)
                {
                    RemoveFile(k, rootDestinationFolderPath);
                }
                //end kenghot
            }

            // VehicleAttachment File

            if (model.RemovedVehicleAttachment != null)
            {
                RemoveFile(model.RemovedVehicleAttachment, rootDestinationFolderPath);
                result.VehicleListFileID6 = (decimal?)null;
            }
            if (model.AddedVehicleAttachment != null)
            {
                result.VehicleListFileID6 = SaveFile(model.AddedVehicleAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
            }

            // ValunteerAttachment File           
            if (model.RemovedValunteerAttachment != null)
            {
                RemoveFile(model.RemovedValunteerAttachment, rootDestinationFolderPath);
                result.ValunteerListFileID7 = (decimal?)null;
            }
            if (model.AddedValunteerAttachment != null)
            {
                result.ValunteerListFileID7 = SaveFile(model.AddedValunteerAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
            }

            return result;
        }

        private ServiceModels.ProjectInfo.TabPersonal MappDBProjectPersonalToTabPersonal(DBModels.Model.ProjectPersonel dbModel)
        {
            ServiceModels.ProjectInfo.TabPersonal result = new ServiceModels.ProjectInfo.TabPersonal();
            DBModels.Model.ProjectPersonel model = dbModel;
            ServiceModels.KendoAttachment attch;
            var att = new Business.AttachmentService(_db);
            result.ProjectID = model.ProjectID;
            //Kenghot 
            //2017-1-17
            //remove remark this line
            result.IDCardNo = model.IDCardNo;

            ServiceModels.ProjectInfo.AddressTabPersonal1 address1 = null;
            if (!String.IsNullOrEmpty(model.Firstname1))
            {
                address1 = new ServiceModels.ProjectInfo.AddressTabPersonal1();
                address1.Prefix1 = model.Prefix1;
                address1.Firstname1 = model.Firstname1;
                address1.Lastname1 = model.Lastname1;
                address1.Address1 = model.Address1;
                address1.Moo1 = model.Moo1;
                address1.Building1 = model.Building1;
                address1.Soi1 = model.Soi1;
                address1.Road1 = model.Road1;
                address1.SubDistrictID1 = model.SubDistrictID1;
                address1.SubDistrict1 = model.SubDistrict1;
                address1.DistrictID1 = model.DistrictID1;
                address1.District1 = model.District1;
                address1.ProvinceID1 = model.ProvinceID1;
                address1.PostCode1 = model.PostCode1;
                address1.Telephone1 = model.Telephone1;
                address1.Mobile1 = model.Mobile1;
                address1.Fax1 = model.Fax1;
                address1.Email1 = model.Email1;
                result.Address1 = address1;
            }

            ServiceModels.ProjectInfo.AddressTabPersonal2 address2 = null;
            if (!String.IsNullOrEmpty(model.Firstname2))
            {
                address2 = new ServiceModels.ProjectInfo.AddressTabPersonal2();
                address2.Prefix2 = model.Prefix2;
                address2.Firstname2 = model.Firstname2;
                address2.Lastname2 = model.Lastname2;
                address2.Address2 = model.Address2;
                address2.Moo2 = model.Moo2;
                address2.Building2 = model.Building2;
                address2.Soi2 = model.Soi2;
                address2.Road2 = model.Road2;
                address2.SubDistrictID2 = model.SubDistrictID2;
                address2.SubDistrict2 = model.SubDistrict2;
                address2.DistrictID2 = model.DistrictID2;
                address2.District2 = model.District2;
                address2.ProvinceID2 = model.ProvinceID2;
                address2.PostCode2 = model.PostCode2;
                address2.Telephone2 = model.Telephone2;
                address2.Fax2 = model.Fax2;
                address2.Email2 = model.Email2;
                result.Address2 = address2;
            }


            ServiceModels.ProjectInfo.AddressTabPersonal3 address3 = null;
            if (model.Prefix3 != null)
            {
                address3 = new ServiceModels.ProjectInfo.AddressTabPersonal3();
                address3.Prefix3 = model.Prefix3;
                address3.Firstname3 = model.Firstname3;
                address3.Lastname3 = model.Lastname3;
                address3.Address3 = model.Address3;
                address3.Moo3 = model.Moo3;
                address3.Building3 = model.Building3;
                address3.Soi3 = model.Soi3;
                address3.Road3 = model.Road3;
                address3.SubDistrictID3 = model.SubDistrictID3;
                address3.SubDistrict3 = model.SubDistrict3;
                address3.DistrictID3 = model.DistrictID3;
                address3.District3 = model.District3;
                address3.ProvinceID3 = model.ProvinceID3;
                address3.PostCode3 = model.PostCode3;
                address3.Telephone3 = model.Telephone3;
                address3.Fax3 = model.Fax3;
                address3.Email3 = model.Email3;
                result.Address3 = address3;
            }

            result.SupportPlace1 = model.SupportPlace1;
            result.SupportOrgName1 = model.SupportOrgName1;

            result.InstructorAmt2 = model.InstructorAmt2;
            result.SupportOrgName2 = model.SupportOrgName2;
            result.InstructorListFileID2 = model.InstructorListFileID2;

            ///kenghot
            result.InstructorAttachments = att.GetAttachmentOfTable(TABLE_PROJECTPERSONEL, PERSON_INSTRUCTOR, result.ProjectID);
            result.VehicleAttachments = att.GetAttachmentOfTable(TABLE_PROJECTPERSONEL, PERSON_VEHICLE, result.ProjectID);
            result.ValunteerAttachments = att.GetAttachmentOfTable(TABLE_PROJECTPERSONEL, PERSON_VALUNTEER, result.ProjectID);
            ///end kenghot
            if (model.InstructorListFileID2.HasValue)
            {
                var dbAttachment = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)model.InstructorListFileID2).FirstOrDefault();
                if (dbAttachment != null)
                {
                    attch = new KendoAttachment()
                    {
                        id = dbAttachment.AttachmentID.ToString(),
                        name = dbAttachment.AttachmentFilename,
                        extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                        size = (int)dbAttachment.FileSize
                    };

                    result.InstructorAttachment = attch;
                }
            }

            result.SupportBudgetAmt3 = model.SupportBudgetAmt3;
            result.SupportOrgName3 = model.SupportOrgName3;

            result.SupportEquipment4 = model.SupportEquipment4;
            result.SupportOrgName4 = model.SupportOrgName4;

            result.SupportDrinkFoodAmt5 = model.SupportDrinkFoodAmt5;
            result.SupportOrgName5 = model.SupportOrgName5;

            result.SupportOrgName6 = model.SupportOrgName6;
            result.VehicleListFile6 = model.VehicleListFileID6;
            if (model.VehicleListFileID6.HasValue)
            {
                var dbAttachment = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)model.VehicleListFileID6).FirstOrDefault();
                if (dbAttachment != null)
                {
                    attch = new KendoAttachment()
                    {
                        id = dbAttachment.AttachmentID.ToString(),
                        name = dbAttachment.AttachmentFilename,
                        extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                        size = (int)dbAttachment.FileSize
                    };

                    result.VehicleAttachment = attch;
                }
            }

            result.SupportValunteerAmt7 = model.SupportValunteerAmt7;
            result.SupportOrgName7 = model.SupportOrgName7;
            result.ValunteerListFileID7 = model.ValunteerListFileID7;
            if (model.ValunteerListFileID7.HasValue)
            {
                var dbAttachment = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)model.ValunteerListFileID7).FirstOrDefault();
                if (dbAttachment != null)
                {
                    attch = new KendoAttachment()
                    {
                        id = dbAttachment.AttachmentID.ToString(),
                        name = dbAttachment.AttachmentFilename,
                        extension = Path.GetExtension(dbAttachment.AttachmentFilename),
                        size = (int)dbAttachment.FileSize
                    };

                    result.ValunteerAttachment = attch;
                }
            }

            result.SupportOther8 = model.SupportOther8;
            result.SupportOrgName8 = model.SupportOrgName8;


            if (model.ProjectGeneralInfo == null)
            {
                DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
                result.OrganizationID = genInfo.OrganizationID;
                result.ProjectApprovalStatusID = genInfo.ProjectApprovalStatusID;
                result.ProjectApprovalStatusCode = genInfo.ProjectApprovalStatus.LOVCode;
                result.ProjectApprovalStatusName = genInfo.ProjectApprovalStatus.LOVName;

                result.ApprovalStatus = (genInfo.ProjectApproval != null) ? genInfo.ProjectApproval.ApprovalStatus : null;

                result.CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == genInfo.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();
                result.ProvinceID = genInfo.ProvinceID;
            }
            else
            {
                result.OrganizationID = model.ProjectGeneralInfo.OrganizationID;
                result.ProjectApprovalStatusID = model.ProjectGeneralInfo.ProjectApprovalStatusID;
                result.ProjectApprovalStatusCode = model.ProjectGeneralInfo.ProjectApprovalStatus.LOVCode;
                result.ProjectApprovalStatusName = model.ProjectGeneralInfo.ProjectApprovalStatus.LOVName;
                result.CreatorOrganizationID = _db.SC_User.Where(x => (x.UserID == model.ProjectGeneralInfo.CreatedByID) && (x.IsDelete == "0")).Select(y => y.OrganizationID).FirstOrDefault();
                result.ProvinceID = model.ProjectGeneralInfo.ProvinceID;
            }

            DBModels.Model.ProjectInformation proInfo = _db.ProjectInformations.Where(x => x.ProjectID == model.ProjectID).FirstOrDefault();
            result.BudgetYear = 0;
            if (proInfo != null)
            {
                result.BudgetYear = proInfo.BudgetYear;
            }

            if (result.ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
            {
                result.RequiredSubmitData = GetKeyRequiredSubmitData(result.ProjectID);
            }


            return result;
        }

        private DBModels.Model.ProjectOperation MappTabProcessingPlanToDBProjectOperation(ServiceModels.ProjectInfo.TabProcessingPlan model)
        {

            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
            decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_OPERATION);

            DBModels.Model.ProjectOperation result = null;
            decimal id = model.ProjectID;

            result = _db.ProjectOperations.Where(x => x.ProjectID == id).FirstOrDefault();
            if (result == null)
            {
                result = new DBModels.Model.ProjectOperation();
            }

            result.Address = model.Address;
            result.Building = model.Building;
            result.Moo = model.Moo;
            result.Soi = model.Soi;
            result.Road = model.Road;
            result.SubDistrictID = model.SubDistrictID;
            result.SubDistrict = model.SubDistrict;
            result.DistrictID = model.DistrictID;
            result.District = model.District;
            result.ProvinceID = model.ProvinceID;
            result.StartDate = (DateTime)model.StartDate;
            result.EndDate = (DateTime)model.EndDate;
            result.TotalDay = (decimal)model.TotalDay;
            result.TimeDesc = model.TimeDesc;
            result.Method = model.Method;

            //Attachment 
            if (model.RemovedLocationMapAttachment != null)
            {
                RemoveFile(model.RemovedLocationMapAttachment, rootDestinationFolderPath);
                result.LocationMapID = (decimal?)null;
            }

            if (model.AddedLocationMapAttachment != null)
            {
                result.LocationMapID = SaveFile(model.AddedLocationMapAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
            }



            return result;
        }

        private ServiceModels.ProjectInfo.TabProcessingPlan MappDBProjectOperationToTabProcessingPlan(DBModels.Model.ProjectOperation dbModel)
        {


            ServiceModels.ProjectInfo.TabProcessingPlan result = new ServiceModels.ProjectInfo.TabProcessingPlan();
            DBModels.Model.ProjectOperation model = dbModel;

            result.ProjectID = model.ProjectID;
            result.Address = model.Address;
            result.Building = model.Building;
            result.Moo = model.Moo;
            result.Soi = model.Soi;
            result.Road = model.Road;
            result.SubDistrictID = model.SubDistrictID;
            result.SubDistrict = model.SubDistrict;
            result.DistrictID = model.DistrictID;
            result.District = model.District;
            result.ProvinceID = model.ProvinceID;
            result.LocationMapID = model.LocationMapID;
            result.StartDate = model.StartDate;
            result.EndDate = model.EndDate;
            result.TotalDay = model.TotalDay;
            result.TimeDesc = model.TimeDesc;
            result.Method = model.Method;

            DBModels.Model.MT_ListOfValue status = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == dbModel.ProjectID).Select(y => y.ProjectApprovalStatus).SingleOrDefault();
            result.ProjectApprovalStatusID = status.LOVID;
            result.ProjectApprovalStatusCode = status.LOVCode;
            result.ProjectApprovalStatusName = status.LOVName;

            if (result.LocationMapID.HasValue)
            {
                DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)result.LocationMapID).FirstOrDefault();
                if (dbAttach != null)
                {
                    ServiceModels.KendoAttachment file = new KendoAttachment()
                    {
                        id = dbAttach.AttachmentID.ToString(),
                        name = dbAttach.AttachmentFilename,
                        extension = Path.GetExtension(dbAttach.AttachmentFilename),
                        size = (int)dbAttach.FileSize
                    };

                    result.LocationMapAttachment = file;
                }
            }

            //result.ProjectOperationAddresses = MappProjectOperationAddress(model.ProjectID);

            return result;
        }
        public List<ServiceModels.ProjectInfo.ProjectProcessed> MapProjectProcessed(decimal projectID)
        {
            List<ServiceModels.ProjectInfo.ProjectProcessed> addresses = null;

            addresses = (from d in _db.PROJECTPROCESSEDs
                         where d.PROJECTID == projectID
                         select new ServiceModels.ProjectInfo.ProjectProcessed
                         {
                             ProjectID = d.PROJECTID,
                             ProcessID = d.PROCESSID,
                             Address = d.ADDRESS,
                             Moo = d.MOO,
                             Building = d.BUILDING,
                             Soi = d.SOI,
                             Road = d.ROAD,
                             SubDistrictID = d.SUBDISTRICTID,
                             SubDistrict = d.SUBDISTRICT,
                             DistrictID = d.DISTRICTID,
                             District = d.DISTRICT,
                             ProvinceID = d.PROVINCEID,
                             Description = d.DESCRIPTION,
                             ProcessEnd = d.PROCESSEND,
                             ProcessStart = d.PROCESSSTART,
                             LocationMapID = d.LOCATIONMAPID,
                             Latitude = d.LATITUDE,
                             Longitude = d.LONGITUDE

                             //FileName = (d. != null) ? d.MapAttachment.AttachmentFilename : null,
                             // FileSize = (d.MapAttachment != null) ? d.MapAttachment.FileSize : (decimal?)null,
                         }
                    ).ToList();
            if ((addresses != null) && (addresses.Count > 0))
            {
                ServiceModels.ProjectInfo.ProjectOperationAddress item;
                for (int i = 0; i < addresses.Count; i++)
                {
                    var pid = addresses[i].ProvinceID;
                    var pv = (from p in _db.MT_Province where p.ProvinceID == pid select p).FirstOrDefault();
                    if (pv != null)
                    {
                        addresses[i].Province = pv.ProvinceName;
                    }
                    if (addresses[i].LocationMapID.HasValue)
                    {
                        var lID = addresses[i].LocationMapID.Value;
                        var file = _db.MT_Attachment.Where(w => w.AttachmentID == lID).FirstOrDefault();
                        if (file != null)
                        {
                            addresses[i].FileName = file.AttachmentFilename;
                            addresses[i].FileSize = file.FileSize;
                        }
                    }
                    item = addresses[i];
                    item.Runno = i + 1;
                    if (item.LocationMapID.HasValue)
                    {
                        item.LocationMapAttachment = new KendoAttachment
                        {
                            id = item.LocationMapID.ToString(),
                            name = item.FileName,
                            extension = Path.GetExtension(item.FileName),
                            size = (int)item.FileSize
                        };
                    }
                    var att = new Business.AttachmentService(_db);
                    //addresses[i].ImageAttachments = att.GetAttachmentOfTable(TABLE_PROJECTPROCESSED, PROJECTPROCESSED_IMAGE, addresses[i].ProcessID);
                    //addresses[i].ImageAttachments.Add(new KendoAttachment
                    //{
                    //    extension = "jpg",
                    //    fieldName = "f1",
                    //    id = "1",
                    //    name = "n1",
                    //    size = 111,
                    //    tempId = "1"
                    //});
                    //addresses[i].ImageAttachments.Add(new KendoAttachment
                    //{
                    //    extension = "jpg",
                    //    fieldName = "f2",
                    //    id = "2",
                    //    name = "n2",
                    //    size = 222,
                    //    tempId = "2"
                    //});
                    addresses[i].ImageAttachments = _db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == addresses[i].ProcessID && w.QUESTGROUP == "ACTIVITYIMG")
                        .Select(s => new KendoAttachment
                        {
                            id = s.PROJECTID.ToString(),
                            name = s.DATA
                        })
                        .ToList();

                }
            }


            return addresses;
        }
        private List<ServiceModels.ProjectInfo.ProjectOperationAddress> MappProjectOperationAddress(decimal projectID)
        {
            List<ServiceModels.ProjectInfo.ProjectOperationAddress> addresses = null;

            addresses = (from d in _db.ProjectOperationAddresses
                         where d.ProjectID == projectID
                         select new ServiceModels.ProjectInfo.ProjectOperationAddress
                         {
                             ProjectID = d.ProjectID,
                             OperationAddressID = d.OperationAddressID,
                             Address = d.Address,
                             Moo = d.Moo,
                             Building = d.Building,
                             Soi = d.Soi,
                             Road = d.Road,
                             SubDistrictID = d.SubDistrictID,
                             SubDistrict = d.SubDistrict,
                             DistrictID = d.DistrictID,
                             District = d.District,
                             ProvinceID = d.ProvinceID,
                             Province = d.MT_Province.ProvinceName,
                             LocationMapID = d.LocationMapID,
                             FileName = (d.MapAttachment != null) ? d.MapAttachment.AttachmentFilename : null,
                             FileSize = (d.MapAttachment != null) ? d.MapAttachment.FileSize : (decimal?)null,
                         }
                    ).ToList();
            if ((addresses != null) && (addresses.Count > 0))
            {
                ServiceModels.ProjectInfo.ProjectOperationAddress item;
                for (int i = 0; i < addresses.Count; i++)
                {
                    item = addresses[i];
                    item.Runno = i + 1;
                    if (item.LocationMapID.HasValue)
                    {
                        item.LocationMapAttachment = new KendoAttachment
                        {
                            id = item.LocationMapID.ToString(),
                            name = item.FileName,
                            extension = Path.GetExtension(item.FileName),
                            size = (int)item.FileSize
                        };
                    }
                }
            }


            return addresses;
        }

        private List<ServiceModels.ProjectInfo.BudgetDetail> MappDBProjectBudgetToListProjectBudgetDetail(List<DBModels.Model.ProjectBudget> list)
        {
            //decimal id = projectId;
            List<ServiceModels.ProjectInfo.BudgetDetail> result = new List<ServiceModels.ProjectInfo.BudgetDetail>();
            //List<DBModels.Model.ProjectBudget> list = _db.ProjectBudgets.Where(x => x.ProjectID == id).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                ServiceModels.ProjectInfo.BudgetDetail model = new ServiceModels.ProjectInfo.BudgetDetail();
                model.ProjectBudgetID = list[i].ProjectBudgetID;
                model.No = (i + 1);
                model.Detail = list[i].BudgetDetail;
                model.Amount = list[i].BudgetValue;
                model.BudgetCode = list[i].BUDGETCODE;
                model.ActivityID = list[i].ACTIVITYID;
                model.ActualExpense = list[i].ACTUALEXPENSE;
                if (!string.IsNullOrEmpty(list[i].BudgetDetailRevise))
                    model.ReviseDetail = list[i].BudgetDetailRevise;

                if (list[i].BudgetReviseValue >= 0)
                    model.ReviseAmount = list[i].BudgetReviseValue;

                if (!string.IsNullOrEmpty(list[i].RemarkRevise))
                    model.ReviseRemark = list[i].RemarkRevise;

                if (list[i].BudgetReviseValue1 >= 0)
                    model.Revise1Amount = list[i].BudgetReviseValue1;

                if (list[i].BudgetReviseValue2 >= 0)
                    model.Revise2Amount = list[i].BudgetReviseValue2;

                result.Add(model);
            }

            return result;
        }

        private DBModels.Model.ProjectContract MappTabProjectContractToDBProjectContract(ServiceModels.ProjectInfo.TabContract model)
        {
            DBModels.Model.ProjectContract result = null;
            decimal id = model.ProjectID;

            result = _db.ProjectContracts.Where(x => x.ProjectID == id).FirstOrDefault();
            if (result == null)
            {
                result = new DBModels.Model.ProjectContract();
            }

            result.ProjectID = id;
            result.ContractYear = (Decimal)model.ContractYear;
            result.ContractDate = (DateTime)model.ContractDate;
            result.ContractStartDate = (DateTime)model.ContractStartDate;
            result.ContractEndDate = (DateTime)model.ContractEndDate;
            result.ContractLocation = model.Location;
            result.ContractViewerName1 = model.ViewerName1;
            result.ContractViewerSurname1 = model.ViewerSurname1;
            result.ContractViewerName2 = model.ViewerName2;
            result.ContractViewerSurname2 = model.ViewerSurname2;
            result.DirectornameName = model.DirectorFirstName;
            result.DirectorLastName = model.DirectorLastName;
            result.DirectorPosition = model.DirectorPosition;
            result.AttorneyNo = model.AttorneyNo;
            result.AttorneyYear = model.AttorneyYear;
            result.ContractGiverDate = model.ContractGiverDate;
            result.ProvinceContractNo = model.ProvinceContractNo;
            result.ProvinceContractYear = model.ProvinceContractYear;
            result.ProvinceContractDate = model.ProvinceContractDate;
            result.ContractReceiveDate = (DateTime)model.ContractReceiveDate;
            result.AuthorizeFlag = model.AuthorizeFlag == true ? Common.Constants.BOOLEAN_TRUE : Common.Constants.BOOLEAN_FALSE;
            result.ATTACHPAGE1 = model.AttachPage1;
            result.ATTACHPAGE2 = model.AttachPage2;
            result.ATTACHPAGE3 = model.AttachPage3;
            result.MEETINGNO = model.MeetingNo;
            result.MEETINGDATE = model.MeetingDate;
            result.REMARK = model.Remark;
            try
            {
                result.EXTENDDATA = Newtonsoft.Json.JsonConvert.SerializeObject(model.ExtendData);
            }
            catch
            {

            }
            if (model.AuthorizeFlag)
            {
                result.ReceiverName = model.ReceiverName;
                result.ReceiverSurname = model.ReceiverSurname;
                result.ReceiverPosition = model.ReceiverPosition;
                result.AuthorizeDate = model.AuthorizeDate;
            }
            else
            {
                result.ReceiverName = model.ContractReceiveName;
                result.ReceiverSurname = model.ContractReceiveSurname;
                result.ReceiverPosition = model.ContractReceivePosition;
            }

            String rootFolderPath = Common.Constants.UPLOAD_TEMP_PATH;
            String rootDestinationFolderPath = GetAttachmentRootFolder();
            String folder = PROJECT_FOLDER_NAME + model.ProjectID + "\\";
            decimal attachmentTypeID = GetAttachmentTypeID(Common.LOVCode.Attachmenttype.PROJECT_CONTRACT);

            //Attachment 

            if (model.RemovedAuthorizeDocAttachment != null)
            {
                RemoveFile(model.RemovedAuthorizeDocAttachment, rootDestinationFolderPath);
                result.AuthorizeDocID = (decimal?)null;
            }
            if (model.AddedAuthorizeDocAttachment != null)
            {
                result.AuthorizeDocID = SaveFile(model.AddedAuthorizeDocAttachment, rootFolderPath, rootDestinationFolderPath, folder, attachmentTypeID);
            }

            return result;
        }

        private ServiceModels.ProjectInfo.TabContract MappDBProjectContractToTabProjectContract(DBModels.Model.ProjectContract dbModel)
        {
            ServiceModels.ProjectInfo.TabContract result = new ServiceModels.ProjectInfo.TabContract();
            DBModels.Model.ProjectContract model = dbModel;

            result.ProjectID = model.ProjectID;
            result.ContractNo = model.ContractNo;
            result.ContractYear = model.ContractYear;
            result.ContractDate = model.ContractDate;
            result.ContractStartDate = model.ContractStartDate;
            result.ContractEndDate = model.ContractEndDate;
            result.Location = model.ContractLocation;
            result.ViewerName1 = model.ContractViewerName1;
            result.ViewerSurname1 = model.ContractViewerSurname1;
            result.ViewerName2 = model.ContractViewerName2;
            result.ViewerSurname2 = model.ContractViewerSurname2;
            result.DirectorFirstName = model.DirectornameName;
            result.DirectorLastName = model.DirectorLastName;
            result.AttorneyNo = model.AttorneyNo;
            result.AttorneyYear = model.AttorneyYear;
            result.ContractGiverDate = model.ContractGiverDate;
            result.ProvinceContractNo = model.ProvinceContractNo;
            result.ProvinceContractYear = model.ProvinceContractYear;
            result.AuthorizeFlag = model.AuthorizeFlag == Common.Constants.BOOLEAN_TRUE ? true : false;

            if (result.AuthorizeFlag)
            {
                result.ReceiverName = model.ReceiverName;
                result.ReceiverSurname = model.ReceiverSurname;
                result.ReceiverPosition = model.ReceiverPosition;
                result.AuthorizeDate = model.AuthorizeDate;
            }
            else
            {
                result.ContractReceiveName = model.ReceiverName;
                result.ContractReceiveSurname = model.ReceiverSurname;
                result.ContractReceivePosition = model.ReceiverPosition;
            }

            if (result.AuthorizeDocID.HasValue)
            {
                DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)result.AuthorizeDocID).FirstOrDefault();
                if (dbAttach != null)
                {
                    ServiceModels.KendoAttachment file = new KendoAttachment()
                    {
                        id = dbAttach.AttachmentID.ToString(),
                        name = dbAttach.AttachmentFilename,
                        extension = Path.GetExtension(dbAttach.AttachmentFilename),
                        size = (int)dbAttach.FileSize
                    };

                    result.AuthorizeDocAttachment = file;
                }
            }

            return result;
        }
        #endregion


        public DBModels.Model.MT_ListOfValue GetListOfValue(string code, string group)
        {
            DBModels.Model.MT_ListOfValue obj = _db.MT_ListOfValue.Where(x => x.LOVGroup == group && (x.LOVCode == code)).SingleOrDefault();
            return obj;
        }
        //kenghot
        public DBModels.Model.MT_ListOfValue GetListOfValueByKey(decimal LOVID)
        {
            DBModels.Model.MT_ListOfValue obj = _db.MT_ListOfValue.Where(x => x.LOVID == LOVID).SingleOrDefault();
            return obj;
        }
        private Decimal GetEvaluationStatusID(bool? isPassAss4, bool? isPassAss5, decimal? totalScore)
        {
            decimal id = 0;
            if ((isPassAss4 == true) && (isPassAss5 == true) && (totalScore >= EVALUATION_PASS_SCORE))
            {
                id = _db.MT_ListOfValue.Where(x => (x.LOVCode == Common.LOVCode.Evaluationstatus.ผ่าน) && (x.LOVGroup == Common.LOVGroup.EvaluationStatus))
                    .Select(y => y.LOVID).SingleOrDefault();
            }
            else
            {
                id = _db.MT_ListOfValue.Where(x => (x.LOVCode == Common.LOVCode.Evaluationstatus.ไม่ผ่าน) && (x.LOVGroup == Common.LOVGroup.EvaluationStatus))
                    .Select(y => y.LOVID).SingleOrDefault();
            }
            return id;
        }

        private bool IsCenterReviseProject(decimal projectProvinceID)
        {
            bool isCenter = false;

            string centerProvinceAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
            string provinceAbbr = _db.MT_Province.Where(x => x.ProvinceID == projectProvinceID).Select(y => y.ProvinceAbbr).FirstOrDefault();
            if ((!String.IsNullOrEmpty(provinceAbbr)) && (!String.IsNullOrEmpty(centerProvinceAbbr)) && (provinceAbbr == centerProvinceAbbr))
            {
                isCenter = true;
            }

            return isCenter;
        }

        /// <summary>
        /// คำนวณหาปีงบประมาณจากวันที่ส่งเข้ามา
        /// </summary>
        /// <param name="date"></param>
        /// <returns>ปีงบประมาณ</returns>
        private int GetBudgetYear(DateTime? date)
        {
            int budgetYear = 0;
            if (date.HasValue)
            {
                DateTime tempRequestDate = (DateTime)date;
                DateTime requestDate = new DateTime(tempRequestDate.Year, tempRequestDate.Month, tempRequestDate.Day, 0, 0, 0);
                DateTime endBudgetDate = new DateTime(tempRequestDate.Year, 9, 30, 0, 0, 0);
                int requestYear = requestDate.Year;
                int endBudgetYear = endBudgetDate.Year;

                if (requestDate.CompareTo(endBudgetDate) > 0)
                {
                    budgetYear = (requestYear + 1);
                }
                else
                {
                    budgetYear = requestYear;
                }
            }
            return budgetYear;
        }

        //private bool IsSameTelephoneNumber(String num1, String num2)
        //{
        //    num1 = num1.Replace(" ", "").Replace("-", "");
        //    num2 = num2.Replace(" ", "").Replace("-", "");

        //    if (num1.IndexOf("+") == 0)
        //    {
        //        num1 = num1.Substring(2);
        //    }
        //    else
        //    {
        //        num1 = num1.Substring(1);
        //    }

        //    if (num2.IndexOf("+") == 0)
        //    {
        //        num2 = num2.Substring(2);
        //    }
        //    else
        //    {
        //        num2 = num2.Substring(1);
        //    }

        //    bool isSame = (num1 == num2);
        //    return isSame;
        //}



        private Dictionary<String, String> GetRequiredSubmitData(decimal projectID)
        {
            Dictionary<String, String> required = new Dictionary<string, string>();
            DBModels.Model.ProjectInformation projectInfo = _db.ProjectInformations.Where(x => x.ProjectID == projectID).FirstOrDefault();
            bool isRequiredAttachment = true;
            int budgetYear = 0;

            /*--Validate Project Info--*/
            #region Validate Project Info
            bool projectInfoIsValid = true;
            if (projectInfo != null)
            {
                budgetYear = (int)projectInfo.BudgetYear;
                isRequiredAttachment = (projectInfo.BudgetYear > 2016);

                int projectTargetGroupCount = _db.ProjectTargetGroups.Where(x => x.ProjectID == projectID).Count();

                if (!projectInfo.ProjectTypeID.HasValue || !projectInfo.DisabilityTypeID.HasValue ||
                    String.IsNullOrEmpty(projectInfo.ProjectReason) || String.IsNullOrEmpty(projectInfo.ProjectPurpose) ||
                    String.IsNullOrEmpty(projectInfo.ProjectKPI) || String.IsNullOrEmpty(projectInfo.ProjectResult) ||
                    (projectTargetGroupCount == 0))
                {
                    projectInfoIsValid = false;
                }
            }
            else
            {
                projectInfoIsValid = false;
            }

            if (!projectInfoIsValid)
            {
                required.Add("ProjectInformation", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitleProjectInfo));
            }
            #endregion Validate Project Info

            /*--Validate Project Personal--*/
            DBModels.Model.ProjectPersonel personel = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();
            if (personel == null)
            {
                required.Add("ProjectPersonel", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitlePersonal));
            }
            else if ((personel != null) && (budgetYear > 2016))
            {
                if (String.IsNullOrEmpty(personel.IDCardNo) || String.IsNullOrEmpty(personel.Email1) || String.IsNullOrEmpty(personel.Email2))
                {
                    required.Add("ProjectPersonel", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitlePersonal));
                }
            }


            /*--Validate Processing Plan--*/
            int processingPlanCount = _db.ProjectOperations.Where(x => x.ProjectID == projectID).Count();
            if (processingPlanCount == 0)
            {
                required.Add("ProjectOperation", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitleProcessingPlan));
            }

            /*--Validate Project Budject--*/
            int projectBudjectCount = _db.ProjectBudgets.Where(x => x.ProjectID == projectID).Count();
            if (projectBudjectCount == 0)
            {
                required.Add("ProjectBudget", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitleProjectBudget));
            }

            /*--Validate Document--*/
            if (isRequiredAttachment)
            {
                int projectAttachmentCount = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).Count();
                if (projectAttachmentCount == 0)
                {
                    required.Add("ProjectDocument", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitleAttachment));
                }
                else
                {
                    //kenghot
                    // DBModels.Model.ProjectDocument doc = _db.ProjectDocuments.Where(x => x.ProjectID == projectID).FirstOrDefault();

                    //if (doc.DocumentID1.HasValue || doc.DocumentID2.HasValue || doc.DocumentID3.HasValue || doc.DocumentID4.HasValue || doc.DocumentID5.HasValue ||
                    //    doc.DocumentID6.HasValue || doc.DocumentID7.HasValue || doc.DocumentID8.HasValue || doc.DocumentID9.HasValue || doc.DocumentID10.HasValue ||
                    //    doc.DocumentID11.HasValue || doc.DocumentID12.HasValue || doc.DocumentID13.HasValue || doc.DocumentID14.HasValue)
                    //{
                    //    hasFile = true;
                    //}
                    bool hasFile = false;
                    var doc = _db.K_FILEINTABLE.Where(w => w.TABLENAME == TABLE_PROJECTDOCUMENT && w.TABLEROWID == projectID).FirstOrDefault();
                    if (doc != null)
                    {
                        hasFile = true;
                    }
                    // end kenghot
                    if (!hasFile)
                    {
                        required.Add("ProjectDocument", String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TabTitleAttachment));
                    }
                }
            }

            return required;
        }

        private List<String> GetKeyRequiredSubmitData(decimal projectID)
        {

            Dictionary<string, string> required = GetRequiredSubmitData(projectID);
            List<String> keys = new List<string>();
            foreach (string key in required.Keys)
            {
                keys.Add(key);
            }

            return (keys.Count > 0) ? keys : null;
        }

        public ReturnQueryData<GenericDropDownListData> ListPosition()

        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {

                result.IsCompleted = true;
                result.Data = (from p in _db.K_MT_POSITION
                               orderby p.SORTORDER
                               select new GenericDropDownListData
                               {
                                   Value = p.POSCODE,
                                   Text = p.POSNAME
                               }).ToList();

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }

        public KendoChart GetDashBoardData()
        {
            ServiceModels.KendoChart pie = new KendoChart();
            ServiceModels.KendoChartSerie s = new ServiceModels.KendoChartSerie();
            pie.series = new List<ServiceModels.KendoChartSerie>();
            pie.series.Add(s);
            s.type = "pie";
            s.data = new List<ServiceModels.KendoChartData>();
            var all = ListProjectInfoList(new QueryParameter() { PageSize = 9999999 },false);  //(from a in  _db.View_ProjectList select a ).ToList();
            int follow, expire, newreq, notreport;
            follow = expire = newreq = notreport = 0;
            var today = DateTime.Now.Date;
            var lov = new ListOfValueService(_db);
            var l = lov.GetListOfValueByCode(Common.LOVGroup.FollowupStatus, Common.LOVCode.Followupstatus.รายงานผลแล้ว);

            foreach (ServiceModels.ProjectInfo.ProjectInfoList p in all.Data)
            {
                if (p.SubmitedDate.HasValue && p.ApprovalStatus == null)
                {
                    newreq++;
                }
                if (p.IsFollowup.HasValue && p.IsFollowup.Value)
                {
                    follow++;
                }
                if (p.ProjectEndDate != null && p.ProjectEndDate >= today && (today - p.ProjectEndDate.Value.Date).TotalDays < 30)
                {
                    expire++;
                }
                if (p.FollowupStatusID.HasValue && p.FollowupStatusID != l.Data.LovID)
                {
                    notreport++;
                }
            }

            s.data.Add(new ServiceModels.KendoChartData { category = "ที่เสนอมาใหม่", value = newreq });
            s.data.Add(new ServiceModels.KendoChartData { category = "ใกล้หมดเวลา", value = expire });
            s.data.Add(new ServiceModels.KendoChartData { category = "ไม่ส่งรายงานการประเมินผล", value = notreport });
            s.data.Add(new ServiceModels.KendoChartData { category = "รอการติดตามประเมินผล", value = follow });
            return pie;
        }

        public ServiceModels.ProjectInfo.DashBoard GetDashBoardData(QueryParameter q)
        {
            ServiceModels.ProjectInfo.DashBoard ret = new ServiceModels.ProjectInfo.DashBoard();

            //ret.Add(new List<ProjectInfoList>()); //all
            //ret.Add(new List<ProjectInfoList>()); //almost expire
            //ret.Add(new List<ProjectInfoList>()); //not report
            //ret.Add(new List<ProjectInfoList>()); // follow
            //ret.Add(new List<ProjectInfoList>()); // new
            var all = ListProjectInfoList(q,false);  //(from a in  _db.View_ProjectList select a ).ToList();

            int follow, expire, newreq, notreport, allNotexp, other;
            decimal followAmt, expireAmt, newreqAmt, notreportAmt, allNotexpAmt, otherAmt;
            follow = expire = newreq = notreport = allNotexp = other = 0;
            followAmt = expireAmt = newreqAmt = notreportAmt = allNotexpAmt = otherAmt = 0;
            var today = DateTime.Now.Date;
            var lov = new ListOfValueService(_db);
            var l = lov.GetListOfValueByCode(Common.LOVGroup.FollowupStatus, Common.LOVCode.Followupstatus.รายงานผลแล้ว);
            decimal BudVal = 0;
            foreach (ServiceModels.ProjectInfo.ProjectInfoList p in all.Data)
            {
                if (p.BudgetValue.HasValue)
                { BudVal = p.BudgetValue.Value; }
                else
                { BudVal = 0; }
                if (p.SubmitedDate.HasValue && p.ApprovalStatus == null)
                {
                    p.RecStatus = "5";
                    newreq++;
                    newreqAmt += BudVal;
                    continue;
                }
                if (p.IsFollowup.HasValue && p.IsFollowup.Value)
                {
                    p.RecStatus = "4";
                    follow++;
                    followAmt += BudVal;
                    continue;
                }
                if (p.ProjectEndDate != null && p.ProjectEndDate >= today && (today - p.ProjectEndDate.Value.Date).TotalDays < 30)
                {
                    p.RecStatus = "2";
                    expire++;
                    expireAmt += BudVal;
                    continue;
                }
                if (p.FollowupStatusID.HasValue && p.FollowupStatusID != l.Data.LovID)
                {
                    if (p.ProjectEndDate < today)
                    {
                        p.RecStatus = "3";
                        notreport++;
                        notreportAmt += BudVal;
                        continue;
                    }
                    else
                    {
                        p.RecStatus = "1";
                        allNotexp++;
                        allNotexpAmt += BudVal;
                        continue;
                    }

                }
                p.RecStatus = "6";
                otherAmt += BudVal;
                other++;
            }
            ret.ProjectInfoList = all.Data;
            ret.ProjectCountByStatus = new int[] { allNotexp, expire, notreport, follow, newreq, other };

            ServiceModels.KendoChart pie = new KendoChart();
            ServiceModels.KendoChartSerie s = new ServiceModels.KendoChartSerie();
            pie.series = new List<ServiceModels.KendoChartSerie>();
            pie.series.Add(s);
            s.type = "pie";
            s.data = new List<ServiceModels.KendoChartData>();
            s.data.Add(new ServiceModels.KendoChartData { category = "ทั้งหมด", value = allNotexpAmt, color = "LightBlue", remark = "1" });
            s.data.Add(new ServiceModels.KendoChartData { category = "ใกล้หมดเวลา", value = expireAmt, color = "Yellow", remark = "2" });
            s.data.Add(new ServiceModels.KendoChartData { category = "ไม่ส่งรายงานการประเมินผล", value = notreportAmt, color = "Orange", remark = "3" });
            s.data.Add(new ServiceModels.KendoChartData { category = "รอการติดตามประเมินผล", value = followAmt, color = "Fuchsia", remark = "4" });
            s.data.Add(new ServiceModels.KendoChartData { category = "ที่เสนอมาใหม่", value = newreqAmt, color = "Lime", remark = "5" });
            s.data.Add(new ServiceModels.KendoChartData { category = "อื่นๆ", value = newreqAmt, color = "Silver", remark = "6" });
            ret.BudgetChart = pie;
            return ret;
        }

        public NepProjectDBEntities GetDB()
        {
            return _db;
        }

        public ReturnObject<List<ServiceModels.ProjectInfo.ProjectHistory>> GetProjectHistoryList(decimal projID)
        {
            ReturnObject<List<ServiceModels.ProjectInfo.ProjectHistory>> ph = new ReturnObject<List<ServiceModels.ProjectInfo.ProjectHistory>>();
            try
            {
                var hist = (from h in _db.PROJECTHISTORies
                            where h.PROJECTID == projID
                            orderby h.HISTORYTYPE, h.ENTRYDT descending
                            select h).ToList();
                var hlist = new List<ServiceModels.ProjectInfo.ProjectHistory>();
                string hname = "";
                for (int i = 1; i <= 8; i++)
                {
                    var last = hist.Where(w => w.HISTORYTYPE == i.ToString()).FirstOrDefault();
                    ph.Data = hlist;
                    if (last != null)
                    {
                        var user = (from u in _db.SC_User where u.UserID == last.USERID select u).FirstOrDefault();
                        if (user != null)
                        {
                            switch (i)
                            {
                                case 1: { hname = "ยื่นโครงการ"; break; }
                                case 2: { hname = "บันทึกข้อมูลการประเมินโครงการ"; break; }
                                case 3: { hname = "บันทึกผลการอนุมัติ"; break; }
                                case 4: { hname = "บันทึกข้อมูลสัญญา"; break; }
                                case 5: { hname = "บันทึกการดำเนินการ"; break; }
                                case 6: { hname = "บันทึกแบบประเมินความพึงพอใจ"; break; }
                                case 7: { hname = "บันทึกแบบประเมินตนเอง"; break; }
                                case 8: { hname = "บันทึกแบบรายงานผลปฎิบัติงาน"; break; }
                            }
                            hlist.Add(new ProjectHistory()
                            {
                                History = last,
                                HistoryName = hname,
                                userName = string.Format("{0} {1}", user.FirstName, user.LastName)
                            });

                        }

                    }
                }
                //// var qg  = new string[] { "SELF", "SATISFY"};
                // var qs = (from q in GetDB().PROJECTQUESTIONHDs where q.PROJECTID == projID && q.QUESTGROUP == "SATISFY" orderby q.QUESTHDID descending select q).FirstOrDefault() ;
                // if (qs != null)
                // {
                //     hlist.Add(new ProjectHistory()
                //     {
                //         History = last,
                //         HistoryName = hname,
                //         userName = string.Format("{0} {1}", user.FirstName, user.LastName)
                //     });
                // }
                // qs = (from q in GetDB().PROJECTQUESTIONHDs where q.PROJECTID == projID && q.QUESTGROUP == "SELF" orderby q.QUESTHDID descending select q).FirstOrDefault();
                // if (qs != null)
                // {

                // }

                ph.IsCompleted = true;


            }
            catch (Exception ex)
            {
                ph.IsCompleted = false;
                ph.Message.Add(ex.Message);
            }
            return ph;
        }

        public ReturnObject<bool> SendReportToRevise(decimal projecID, string comment)
        {
            var result = new ReturnObject<bool>();
            try
            {
                var pj = (from p in _db.ProjectGeneralInfoes where p.ProjectID == projecID select p).FirstOrDefault();
                if (pj != null)
                {
                    result.IsCompleted = true;
                    DBModels.Model.MT_ListOfValue followupStatus = GetListOfValue(Common.LOVCode.Followupstatus.รายงานผลแล้ว, Common.LOVGroup.FollowupStatus);
                    if (pj.FollowUpStatus == followupStatus.LOVID)
                    {
                        pj.FollowUpStatus = null;
                        var rep = (from r in _db.ProjectReports where r.ProjectID == projecID select r).FirstOrDefault();
                        if (rep != null)
                        {
                            rep.REVISECOMMENT = comment;
                        }
                        var qn = from q in _db.PROJECTQUESTIONHDs where q.PROJECTID == projecID && q.ISREPORTED == "1" select q;
                        foreach (PROJECTQUESTIONHD qtmp in qn)
                        {
                            qtmp.ISREPORTED = "0";
                            qtmp.UPDATEDBY = _user.UserName;
                            qtmp.UPDATEDBYID = _user.UserID;
                            qtmp.UPDATEDDATE = DateTime.Now;
                        }


                        _db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }
        public ServiceModels.ReturnMessage SaveLogAccess(decimal? userId, string accessCode, string accessType, string ipAddress)
        {
            var result = new ReturnMessage();
            result.IsCompleted = false;
            try
            {
                var lov = _db.MT_ListOfValue.Where(w => w.LOVCode == accessCode && w.LOVGroup == "LogAccess").FirstOrDefault();
                var log = new LOG_ACCESS
                {
                    ACCESSID = (lov == null) ? (decimal?)null : lov.LOVID,
                    CREATEDATETIME = DateTime.Now,
                    ACCESSTYPE = accessType,
                    IPADDRESS = ipAddress,
                    USERID = userId
                };
                _db.LOG_ACCESS.Add(log);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Message.Add(ex.Message);
            }
            return result;
        }
    }

}
