﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.Common;
using Nep.Project.ServiceModels;
using System.IO;
namespace Nep.Project.Business
{
    public class UserProfileService : IServices.IUserProfileService
    {
        private DBModels.Model.NepProjectDBEntities _db { set; get; }
        private ServiceModels.Security.SecurityInfo _loggedUser { set; get; }
        private readonly MailService _mailService;
        private readonly Decimal _systemID;

        public UserProfileService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo loggedUser, MailService mailService)
        {
            _db = db;
            _loggedUser = loggedUser;
            _mailService = mailService;

            _systemID = _db.SC_User.Where(x => x.UserName == Common.Constants.SYSTEM_USERNAME).Select(y => y.UserID).FirstOrDefault();
        }

        public ServiceModels.ReturnQueryData<ServiceModels.UserList> ListWithCriteria(ServiceModels.QueryParameter p)
        {
            var result = (from u in _db.SC_User
                           where (u.IsDelete == "0")
                           select new ServiceModels.UserList
                          {                              
                              Email = u.Email,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              IsActive = u.IsActive,
                              IsDelete = u.IsDelete,
                              TelephoneNo = u.TelephoneNo,
                              UserID = u.UserID,
                              UserName = u.UserName,
                              RoleID = u.GroupID,
                              Role = (u.Group != null)?u.Group.GroupName : null,
                              RoleCode = (u.Group != null)? u.Group.GroupCode : null,
                              OrganizationName = u.Organization.OrganizationNameTH,
                              ProvinceID = (u.ProvinceID != null)? u.ProvinceID : (decimal?)0,
                              Province = (u.Province != null)? u.Province.ProvinceName : null                             
                          }).ToQueryData(p);
            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.UserList> ListAccessWithCriteria(ServiceModels.QueryParameter p)
        {
            var result = (
                          from l in _db.LOG_ACCESS
                          select new ServiceModels.UserList
                          {
                              Email = (l.SC_USER == null) ? "" :  l.SC_USER.Email,
                              FirstName = (l.SC_USER == null) ? "" : l.SC_USER.FirstName,
                              LastName = (l.SC_USER == null) ? "" : l.SC_USER.LastName,
                              IsActive = (l.SC_USER == null) ? "" : l.SC_USER.IsActive,
                              IsDelete = (l.SC_USER == null) ? "" : l.SC_USER.IsDelete,
                              TelephoneNo = (l.SC_USER == null) ? "" : l.SC_USER.TelephoneNo,
                              UserID = (l.SC_USER == null) ? 0 : l.SC_USER.UserID,
                              UserName = (l.SC_USER == null) ? "" : l.SC_USER.UserName,
                              RoleID = (l.SC_USER == null) ?  0 : l.SC_USER.GroupID,
                              Role = (l.SC_USER == null) ? "" : (l.SC_USER.Group != null) ? l.SC_USER.Group.GroupName : null,
                              RoleCode = (l.SC_USER == null) ? "" : (l.SC_USER.Group != null) ? l.SC_USER.Group.GroupCode : null,
                              OrganizationName = (l.SC_USER == null) ? "" : l.SC_USER.Organization.OrganizationNameTH,
                              ProvinceID = (l.SC_USER == null) ? (decimal?)0 :  (l.SC_USER.ProvinceID != null) ? l.SC_USER.ProvinceID : (decimal?)0,
                              Province = (l.SC_USER == null) ? "" : (l.SC_USER.Province != null) ? l.SC_USER.Province.ProvinceName : null,
                              AccessDateTime = l.CREATEDATETIME.Value,
                              AccessDesc = l.MT_LISTOFVALUE.LOVName,
                              AccessType = l.ACCESSTYPE,
                              IPAddress = l.IPADDRESS
                          }).ToQueryData(p);
            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.UserList> ListNewRequestWithCriteria(ServiceModels.QueryParameter p)
        {
            var result = (from u in _db.UserRegisterEntries
                          where (!u.RegisteredUserID.HasValue)
                          orderby u.RegisterDate descending
                          select new ServiceModels.UserList
                          {
                              Email = u.Email,
                              FirstName = u.Firstname,
                              LastName = u.Lastname,
                              //IsActive = u.IsActive,
                              //IsDelete = u.IsDelete,
                              TelephoneNo = u.Telephone,
                              UserID = u.EntryID,
                              UserName = u.Email,
                              //RoleID = u.GroupID,
                              //Role = (u.Group != null) ? u.Group.GroupName : null,
                              //RoleCode = (u.Group != null) ? u.Group.GroupCode : null,
                              OrganizationName = u.Organization.OrganizationNameTH,
                              ProvinceID = u.Organization.ProvinceID  ,
                              Province = u.Organization.Province.ProvinceName
                          }).ToQueryData(p);
            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.UserProfile> GetUserProfile(decimal userID)
        {
            IQueryable<DBModels.Model.SC_User> query = _db.SC_User;
            ServiceModels.ReturnObject<ServiceModels.UserProfile> result = new ReturnObject<UserProfile>();

            try
            {
                var data = (from u in query
                            where u.UserID == userID && u.IsDelete == "0"
                            select new ServiceModels.UserProfile
                            {
                                UserName = u.UserName,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                GroupID = u.GroupID,
                                GroupCode = (u.Group != null)? u.Group.GroupCode : null,
                                Email = u.Email,                               
                                TelephoneNo = u.TelephoneNo,   
                                ProvinceID = u.ProvinceID,
                                Position = u.Position,
                                IsActive = u.IsActive,

                                OrganizationID = u.OrganizationID,
                                OrganizationName = u.Organization.OrganizationNameTH,
                                IDNO = u.IDNo,
                                IDCardFileID = u.IDCardFileID,
                                EmpployeeCardFileID = u.EmploeeCardFileID,
                                RegisterDate = u.RegisterDate,
                                ExtendJSON = u.EXTENDDATA
                            }).FirstOrDefault();

                #region Add Attachment
                if ((data != null) && (data.IDCardFileID.HasValue)){
                    DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.IDCardFileID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };

                        data.IDCardAttachment = file;
                    }
                }

                if ((data != null) && (data.EmpployeeCardFileID.HasValue))
                {
                    DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.EmpployeeCardFileID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };

                        data.EmpployeeCardAttachment = file;
                    }
                }

                #endregion Add Attachment

                if (data != null)
                {
                    #region contract password

                    var qn = _db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == userID && w.QUESTGROUP == "CONTRACTPWD").FirstOrDefault();
                    if (qn != null)
                    {
                        data.ContractPWD = qn.DATA;
                    }
                    #endregion
                    try
                    {
                        data.ExtendData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserExtend>(data.ExtendJSON);
                    }
                    catch (Exception ex)
                    {

                    }
                    result.IsCompleted = true;
                    result.Data = data;
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
            }


            return result;
                 
        }
        public ServiceModels.ReturnObject<ServiceModels.UserProfile> GetUserRequest(decimal userID)
        {
            IQueryable<DBModels.Model.UserRegisterEntry> query = _db.UserRegisterEntries;
            ServiceModels.ReturnObject<ServiceModels.UserProfile> result = new ReturnObject<UserProfile>();

            try
            {
                var data = (from u in query
                            where u.EntryID == userID && !u.RegisteredUserID.HasValue
                            select new ServiceModels.UserProfile
                            {
                                UserName = u.Email,
                                FirstName = u.Firstname,
                                LastName = u.Lastname,
                                //GroupID = u.GroupID,
                                //GroupCode = (u.Group != null) ? u.Group.GroupCode : null,
                                Email = u.Email,
                                TelephoneNo = u.Telephone,
                                ProvinceID = u.Organization.ProvinceID,
                                Position = u.Position,
                                //IsActive = u.IsActive,

                                OrganizationID = u.OrganizationID,
                                OrganizationName = u.Organization.OrganizationNameTH,
                                IDNO = u.PersonalID,
                                IDCardFileID = u.PersonalIDCardAttachmentID,
                                EmpployeeCardFileID = u.EmployeeIDCardAttachmentID,
                                RegisterDate = u.RegisterDate

                            }).FirstOrDefault();
                #region Add Attachment
                if ((data != null) && (data.IDCardFileID.HasValue))
                {
                    DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.IDCardFileID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };

                        data.IDCardAttachment = file;
                    }
                }

                if ((data != null) && (data.EmpployeeCardFileID.HasValue))
                {
                    DBModels.Model.MT_Attachment dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.EmpployeeCardFileID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };

                        data.EmpployeeCardAttachment = file;
                    }
                }

                #endregion Add Attachment

                if (data != null)
                {
                    result.IsCompleted = true;
                    result.Data = data;
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
            }


            return result;

        }

        public ServiceModels.ReturnObject<Int32> GetUserAdministratorRoleID()
        {
            ServiceModels.ReturnObject<Int32> result = new ReturnObject<int>();
            try
            {
                
                var data = _db.SC_Group.Where(x => x.GroupCode == UserGroupCode.ผู้ดูแลระบบ).FirstOrDefault();
                if (data != null)
                {
                    result.IsCompleted = true;
                    result.Data = (int)data.GroupID;
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }

            return result;
        }

        public ServiceModels.ReturnObject<Int32> GetUserProvicnceRoleID()
        {
            ServiceModels.ReturnObject<Int32> result = new ReturnObject<int>();
            try
            {

                var data = _db.SC_Group.Where(x => x.GroupCode == UserGroupCode.เจ้าหน้าที่จังหวัด).FirstOrDefault();
                if (data != null)
                {
                    result.IsCompleted = true;
                    result.Data = (int)data.GroupID;
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }

            return result;
        }


        public ServiceModels.ReturnObject<ServiceModels.UserProfile> CreateInternalUser(ServiceModels.UserProfile userProfile)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.UserProfile>();
            try
            {

                IQueryable<DBModels.Model.SC_User> query = _db.SC_User;
                bool isCommit = false;
                decimal entryID = 0;
               // var date = DateTime.Now.AddDays(-3);

                var chkUserRegister = _db.UserRegisterEntries.Where(x => x.Email == userProfile.Email).FirstOrDefault();
                var chkDup = CheckDupUsername(userProfile.Email);               

                //bool isCheckAdmin = IsAdministratorValid(userProfile);
                

                if (chkDup)
                {
                    string gensalt = Common.PasswordEncrypter.GenerateSalt();
                    string password = Common.PasswordEncrypter.GeneratePassword();
                    Byte[] pass = Common.PasswordEncrypter.Encrypt(password, gensalt);
                    String fulname = String.Format("{0} {1}", userProfile.FirstName, userProfile.LastName);
                    String username = userProfile.Email;

                    var objDB = new DBModels.Model.SC_User();
                    using (var tran = _db.Database.BeginTransaction())
                    {                        

                        objDB.UserName = userProfile.Email;
                        objDB.FirstName = userProfile.FirstName;
                        objDB.LastName = userProfile.LastName;
                        objDB.GroupID = userProfile.GroupID;
                        objDB.Email = userProfile.Email;
                        objDB.TelephoneNo = userProfile.TelephoneNo;
                        objDB.ProvinceID = userProfile.ProvinceID;
                        objDB.Position = userProfile.Position;
                        objDB.IsActive = userProfile.IsActive;

                        objDB.RegisterDate = DateTime.Now;
                        objDB.CreatedBy = _loggedUser.UserName;
                        objDB.CreatedByID = (decimal)_loggedUser.UserID;
                        objDB.CreatedDate = DateTime.Now;
                        objDB.IsDelete = "0";
                        objDB.UserFlag = "1";

                        _db.SC_User.Add(objDB);
                        _db.SaveChanges();

                        var qn = _db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == userProfile.UserID && w.QUESTGROUP == "CONTRACTPWD").FirstOrDefault();
                        if (qn == null)
                        {
                            qn = new DBModels.Model.PROJECTQUESTIONHD
                            {
                                PROJECTID = userProfile.UserID,
                                CREATEDBYID = userProfile.UserID,
                                CREATEDBY = "system",
                                CREATEDDATE = DateTime.Now,
                                ISREPORTED = "1",
                                QUESTGROUP = "CONTRACTPWD"

                            };
                            _db.PROJECTQUESTIONHDs.Add(qn);   
                        }
                        qn.DATA = userProfile.ContractPWD;
                        _db.SaveChanges();

                        var entry = new DBModels.Model.UserRegisterEntry()
                        {
                            Email = userProfile.Email,
                            Firstname = userProfile.FirstName,
                            Lastname = userProfile.LastName,
                            Position = userProfile.Position,
                            Telephone = userProfile.TelephoneNo,
                            RegisterDate = DateTime.Now,
                            ActivationCode = Common.PasswordEncrypter.GenerateActivationCode(),
                            RegisteredUser = objDB
                        };

                        _db.UserRegisterEntries.Add(entry);
                        _db.SaveChanges();
                        entryID = entry.EntryID;

                        userProfile.UserID = objDB.UserID;
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.SaveSuccess);
                        result.Data = userProfile;
                        isCommit = true;
                        tran.Commit();
                    }

                    if (isCommit)
                    {
                        _mailService.SendUserInternalRegistrationNotify(entryID);
                       
                    }
                }else{
                    result.IsCompleted = false;
                    result.Message.Add(String.Format(Resources.Error.DuplicateValue, Nep.Project.Resources.Model.UserProfile_Email));

                    //if (!chkDup)
                    //{
                    //    result.IsCompleted = false;
                    //    result.Message.Add(String.Format(Resources.Error.DuplicateValue, Nep.Project.Resources.Model.UserProfile_Email));
                    //}
                    //else
                    //{
                    //    result.IsCompleted = false;
                    //    result.Message.Add(Resources.Error.ValidationAdministrator);
                    //}
                }

                
                
                return result;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }

        }

        public ServiceModels.ReturnMessage UpdateInternalUser(ServiceModels.UserProfile userProfile)
        {
            var result = new ServiceModels.ReturnMessage();
            try
            {//Beer24032021
                bool chkAdministrator = IsAdministratorValid(userProfile);
                if (chkAdministrator)
                {
                    DBModels.Model.SC_User dbUser = _db.SC_User.Where(x => x.UserID == userProfile.UserID).FirstOrDefault();
                    if (dbUser != null)
                    {
                        dbUser.FirstName = userProfile.FirstName;
                        dbUser.LastName = userProfile.LastName;
                        dbUser.GroupID = userProfile.GroupID;
                        dbUser.TelephoneNo = userProfile.TelephoneNo;
                        dbUser.ProvinceID = userProfile.ProvinceID;
                        dbUser.Position = userProfile.Position;
                        dbUser.IsActive = userProfile.IsActive;
                        if (userProfile.ExtendData != null)
                        {
                            dbUser.EXTENDDATA = Newtonsoft.Json.JsonConvert.SerializeObject(userProfile.ExtendData);
                        }
                        else
                        {
                            dbUser.EXTENDDATA = null;
                        }
                        dbUser.UpdatedBy = _loggedUser.UserName;
                        dbUser.UpdatedByID = _loggedUser.UserID;
                        dbUser.UpdatedDate = DateTime.Now;
                        var qn = _db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == userProfile.UserID && w.QUESTGROUP == "CONTRACTPWD").FirstOrDefault();
                        if (qn == null)
                        {
                            qn = new DBModels.Model.PROJECTQUESTIONHD
                            {
                                PROJECTID = userProfile.UserID,
                                CREATEDBYID = userProfile.UserID,
                                CREATEDBY = "system",
                                CREATEDDATE = DateTime.Now,
                                ISREPORTED = "1",
                                QUESTGROUP = "CONTRACTPWD"

                            };
                            _db.PROJECTQUESTIONHDs.Add(qn);
                        }
                        qn.DATA = userProfile.ContractPWD;
                         
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
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Error.ValidationAdministrator);
                }
                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);

            }

            return result;
        }

        public ServiceModels.ReturnMessage UpdateExternalUser(ServiceModels.UserProfile userProfile)
        {
            var result = new ServiceModels.ReturnMessage();
            try
            {
                DBModels.Model.SC_User dbUser = _db.SC_User.Where(x => x.UserID == userProfile.UserID).FirstOrDefault();
                ServiceModels.KendoAttachment idCardAttachment = userProfile.IDCardAttachment;
                ServiceModels.KendoAttachment empCardAttachment = userProfile.EmpployeeCardAttachment;
                var attachmentRootPath = _db.MT_OrganizationParameter.FirstOrDefault(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;

                if (dbUser != null)
                {
                    dbUser.FirstName = userProfile.FirstName;
                    dbUser.LastName = userProfile.LastName;
                    dbUser.TelephoneNo = userProfile.TelephoneNo;                    
                   
                    dbUser.Position = userProfile.Position;
                    dbUser.IDNo = userProfile.IDNO;

                    dbUser.UpdatedBy = _loggedUser.UserName;
                    dbUser.UpdatedByID = _loggedUser.UserID;
                    dbUser.UpdatedDate = DateTime.Now;
                    dbUser.IsActive = userProfile.IsActive;

                    if ((idCardAttachment != null) && (!String.IsNullOrEmpty(idCardAttachment.tempId)))
                    {
                        decimal? oldAttachID = dbUser.IDCardFileID;
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);  
                        var targetFilePath = Path.Combine(regisPath, idCardAttachment.tempId);
                        string pathName = Path.Combine(regisFolder, idCardAttachment.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, idCardAttachment.tempId)));
                        if (file.Exists)
                        {
                            file.MoveTo(targetFilePath);
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = idCardAttachment.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.PERSONAL_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            dbUser.IDCard = fileDb;
                        }

                        //if (oldAttachID.HasValue)
                        //{
                        //    DBModels.Model.MT_Attachment removeAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)oldAttachID).FirstOrDefault();
                        //    _db.MT_Attachment.Remove(removeAttach);
                        //    String filepath = removeAttach.PathName;
                        //    filepath = Path.Combine(attachmentRootPath, filepath);
                        //    File.Delete(filepath);
                        //}
                    }

                    if ((empCardAttachment != null) && (!String.IsNullOrEmpty(empCardAttachment.tempId)))
                    {
                        decimal? oldAttachID = dbUser.EmploeeCardFileID;
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);
                        var targetFilePath = Path.Combine(regisPath, empCardAttachment.tempId);
                        string pathName = Path.Combine(regisFolder, empCardAttachment.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, empCardAttachment.tempId)));
                        if (file.Exists)
                        {
                            file.MoveTo(targetFilePath);
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = empCardAttachment.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.EMPLOYEE_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            dbUser.EmployeeCard = fileDb;
                        }

                        //if(oldAttachID.HasValue){
                        //    DBModels.Model.MT_Attachment removeAttach = _db.MT_Attachment.Where(x=> x.AttachmentID == (decimal)oldAttachID).FirstOrDefault();
                        //    _db.MT_Attachment.Remove(removeAttach);

                        //    String filepath = removeAttach.PathName;
                        //    filepath = Path.Combine(attachmentRootPath, filepath);
                        //    File.Delete(filepath);
                        //}

                        
                    }
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);

            }

            return result;
        }

        public ServiceModels.ReturnMessage UpdateRequestUser(ServiceModels.UserProfile userProfile)
        {
            var result = new ServiceModels.ReturnMessage();
            try
            {
                DBModels.Model.UserRegisterEntry dbUser = _db.UserRegisterEntries.Where(x => x.EntryID == userProfile.UserID && !x.RegisteredUserID .HasValue).FirstOrDefault();
                ServiceModels.KendoAttachment idCardAttachment = userProfile.IDCardAttachment;
                ServiceModels.KendoAttachment empCardAttachment = userProfile.EmpployeeCardAttachment;
                var attachmentRootPath = _db.MT_OrganizationParameter.FirstOrDefault(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;
                DBModels.Model.SC_User user = _db.SC_User.Where(w => w.UserName == userProfile.Email).FirstOrDefault();
                if (user != null)
                {
                    result.IsCompleted = false;
                    result.Message.Add("email นี้ถูกนำมาใช้แล้ว");
                    return result;
                }
                var chkdup = _db.UserRegisterEntries.Where(w => w.Email == userProfile.Email && w.EntryID != userProfile.UserID).FirstOrDefault();
                if (chkdup != null)
                {
                    result.IsCompleted = false;
                    result.Message.Add("email นี้ถูกนำมาใช้แล้ว");
                    return result;
                }
                if (dbUser != null)
                {
                    dbUser.Firstname = userProfile.FirstName;
                    dbUser.Lastname = userProfile.LastName;
                    dbUser.Telephone = userProfile.TelephoneNo;

                    dbUser.Position = userProfile.Position;
                    dbUser.PersonalID = userProfile.IDNO;
                    dbUser.Email = userProfile.Email;
                    //dbUser.UpdatedBy = _loggedUser.UserName;
                    //dbUser.UpdatedByID = _loggedUser.UserID;
                    //dbUser.UpdatedDate = DateTime.Now;
                    //dbUser.IsActive = userProfile.IsActive;

                    if ((idCardAttachment != null) && (!String.IsNullOrEmpty(idCardAttachment.tempId)))
                    {
                        decimal? oldAttachID = dbUser.PersonalIDCardAttachmentID ;
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);
                        var targetFilePath = Path.Combine(regisPath, idCardAttachment.tempId);
                        string pathName = Path.Combine(regisFolder, idCardAttachment.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, idCardAttachment.tempId)));
                        if (file.Exists)
                        {
                            file.MoveTo(targetFilePath);
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = idCardAttachment.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.PERSONAL_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            dbUser.PersonalIDCardAttachment = fileDb;
                        }

                        //if (oldAttachID.HasValue)
                        //{
                        //    DBModels.Model.MT_Attachment removeAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)oldAttachID).FirstOrDefault();
                        //    _db.MT_Attachment.Remove(removeAttach);
                        //    String filepath = removeAttach.PathName;
                        //    filepath = Path.Combine(attachmentRootPath, filepath);
                        //    File.Delete(filepath);
                        //}
                    }

                    if ((empCardAttachment != null) && (!String.IsNullOrEmpty(empCardAttachment.tempId)))
                    {
                        decimal? oldAttachID = dbUser.EmployeeIDCardAttachmentID;
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);
                        var targetFilePath = Path.Combine(regisPath, empCardAttachment.tempId);
                        string pathName = Path.Combine(regisFolder, empCardAttachment.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, empCardAttachment.tempId)));
                        if (file.Exists)
                        {
                            file.MoveTo(targetFilePath);
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = empCardAttachment.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.EMPLOYEE_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            dbUser.EmployeeIDCardAttachment = fileDb;
                        }

                        //if(oldAttachID.HasValue){
                        //    DBModels.Model.MT_Attachment removeAttach = _db.MT_Attachment.Where(x=> x.AttachmentID == (decimal)oldAttachID).FirstOrDefault();
                        //    _db.MT_Attachment.Remove(removeAttach);

                        //    String filepath = removeAttach.PathName;
                        //    filepath = Path.Combine(attachmentRootPath, filepath);
                        //    File.Delete(filepath);
                        //}


                    }
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Register", ex);

            }

            return result;
        }
        #region Binding
        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListRole()
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            try
            {
                var q = (from e in _db.SC_Group
                        orderby e.GroupID
                     select new ServiceModels.GenericDropDownListData()
                     {
                         Text = e.GroupName,
                         Value = e.GroupID.ToString()
                     }).ToList();

                    result.Data = q;
                    result.IsCompleted = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                 return result;
            }
        }
        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvinceOrganization(decimal provinceID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            try
            {
                var q = (from e in _db.MT_Organization
                         where e.ProvinceID == provinceID
                         orderby e.OrganizationNameTH
                         select new ServiceModels.GenericDropDownListData()
                         {
                             Text = e.OrganizationNameTH,
                             Value = e.OrganizationID.ToString()
                         }).ToList();

                result.Data = q;
                result.IsCompleted = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListInternalRole()
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            try
            {
                var q = (from e in _db.SC_Group
                         where (e.GroupCode != Common.UserGroupCode.องค์กรภายนอก)
                         orderby e.GroupID
                         select new ServiceModels.GenericDropDownListData()
                         {
                             Text = e.GroupName,
                             Value = e.GroupID.ToString()
                         }).ToList();

                result.Data = q;
                result.IsCompleted = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }
        }
        #endregion Binding

        public ServiceModels.ReturnMessage DeleteUser(decimal userID)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.SC_User user = _db.SC_User.Where(x => x.UserID == userID).FirstOrDefault();
                if (user != null)
                {                   

                    user.IsDelete = "1";
                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
            }

            return result;
        }
        public ServiceModels.ReturnMessage DeleteRequestUser(decimal userID)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.UserRegisterEntry user = _db.UserRegisterEntries.Where(x => x.EntryID == userID).FirstOrDefault();
                if (user != null)
                {

                    if (user.RegisteredUserID.HasValue)
                    {
                        result.IsCompleted = false;
                        result.Message.Add("มีการยืนยันตัวตนแล้ว");
                    } else
                    {
                        _db.UserRegisterEntries.Remove(user);
                        _db.SaveChanges();

                        result.IsCompleted = true;
                        result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
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
                // result.Message.Add(ex.Message);
                result.Message.Add("ไม่สามารถลบข้อมูลได้ เพราะมีการใช้งานแล้ว");
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
            }

            return result;
        }

        private bool IsAdministratorValid(ServiceModels.UserProfile user)
        {
             bool isTrue = true;
            DBModels.Model.SC_Group administratorGroup = _db.SC_Group.Where(x => x.GroupCode == Common.UserGroupCode.ผู้ดูแลระบบ).FirstOrDefault();
            
            if ((administratorGroup != null) && (user.GroupID == administratorGroup.GroupID))
            {
                isTrue = (user.ProvinceID == null);
                //String centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                //DBModels.Model.MT_Province centerProvince = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).FirstOrDefault();
                //if(user.ProvinceID != centerProvince.ProvinceID){
                //    isTrue = false;
                //}
            }
           
            
            return isTrue;
        }

        private bool CheckDupUsername(String emailUser)
        {
            var date = DateTime.Now.AddDays(-3);

            var chkUserRegister = _db.UserRegisterEntries.Where(x => (x.Email == emailUser) && (x.RegisteredUserID == null)).OrderByDescending(or => or.RegisterDate).FirstOrDefault();
            var chkDup = (from e in _db.SC_User
                          where e.Email == emailUser && e.IsDelete == "0"
                          select e).Count() == 0;
            if (chkDup)
            {
                if ((chkUserRegister != null))
                {
                    var orgRegister = chkUserRegister.OrganizationRegisterEntries.FirstOrDefault();
                    if ((orgRegister != null) && ((orgRegister.ApprovedDate == null) || (orgRegister.ApprovedDate.HasValue) && orgRegister.ApprovedDate > date))
                    {
                        chkDup = false;
                    }                    
                    else if (chkUserRegister.RegisterDate > date)
                    {
                        chkDup = false;
                    }

                }
            }
            return chkDup;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.ItemList> ListWithItem(ServiceModels.QueryParameter p)
        {
            var result = (from item in _db.MT_ITEM.Where(l => (l.ITEMGROUP == "EVALUATIONSTRATEGIC") && (l.ISDELETE == "0"))
                          select new ServiceModels.ItemList()
                          {
                              ITEMID = item.ITEMID,
                              ITEMGROUP = item.ITEMGROUP,
                              ITEMNAME = item.ITEMNAME,
                              ORDERNO = item.ORDERNO,
                              CREATEDBY = item.CREATEDBY,
                              CREATEDDATE = item.CREATEDDATE,
                              UPDATEDBY = item.UPDATEDBY,
                              UPDATEDDATE = item.UPDATEDDATE,
                              ISACTIVESTR = (item.ISACTIVE == Common.Constants.BOOLEAN_TRUE) ? "เปิดใช้งาน" : "ปิดใช้งาน"
                          }).ToQueryData(p);
            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.Item> GetItem(decimal ItemId, string ItemGroup)
        {
            ServiceModels.ReturnObject<ServiceModels.Item> result = new ServiceModels.ReturnObject<ServiceModels.Item>();
            try
            {
                var DataResult = (from item in _db.MT_ITEM.Where(l => (l.ITEMGROUP == ItemGroup) && (l.ITEMID == ItemId) && (l.ISDELETE == "0"))
                                  select new ServiceModels.Item()
                                  {
                                      ITEMID = item.ITEMID,
                                      ITEMGROUP = item.ITEMGROUP,
                                      ITEMNAME = item.ITEMNAME,
                                      ORDERNO = item.ORDERNO,
                                      ISACTIVE = (item.ISACTIVE == Common.Constants.BOOLEAN_TRUE) ? true : false
                                  }).FirstOrDefault();

                if (DataResult != null)
                {
                    result.Data = DataResult;
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "List of Value", ex);
            }

            return result;
        }
        public ServiceModels.ReturnMessage UpdateItem(ServiceModels.UserProfile userProfile, ServiceModels.Item dataItem)
        {
            var result = new ServiceModels.ReturnMessage();
            try
            {//Beer24032021
                bool chkAdministrator = IsAdministratorValid(userProfile);
                if (chkAdministrator)
                {
                    DBModels.Model.MT_ITEM item = _db.MT_ITEM.Where(x => x.ITEMID == dataItem.ITEMID).FirstOrDefault();
                    if (item != null)
                    {
                        item.ITEMNAME = dataItem.ITEMNAME;
                        item.ISACTIVE = dataItem.ISACTIVE == true ? "1" :"0" ;
                        item.UPDATEDBY = _loggedUser.UserName;
                        item.UPDATEDDATE = DateTime.Now;
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
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Error.ValidationAdministrator);
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);

            }

            return result;
        }
        public ServiceModels.ReturnObject<ServiceModels.Item> CreateItem(ServiceModels.Item dataItem)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.Item>();
            try
            {
                IQueryable<DBModels.Model.MT_ITEM> query = _db.MT_ITEM;
                var checkItemOrder = (from getitem in _db.MT_ITEM.Where(l => (l.ITEMGROUP == dataItem.ITEMGROUP))
                                 select new ServiceModels.Item()
                                 {
                                     ORDERNO = getitem.ORDERNO
                                 }).OrderByDescending(l => l.ORDERNO).FirstOrDefault();

                if(checkItemOrder !=null)
                {
                    var itemObj = new DBModels.Model.MT_ITEM();
                    
                    using (var tran = _db.Database.BeginTransaction())
                    {
                        
                        itemObj.ITEMGROUP = dataItem.ITEMGROUP;
                        itemObj.ITEMNAME = dataItem.ITEMNAME;
                        itemObj.ORDERNO = checkItemOrder.ORDERNO+1;
                        itemObj.ISACTIVE = dataItem.ISACTIVE == true ? "1" : "0";
                        itemObj.ISDELETE = "0";
                        itemObj.CREATEDBY = _loggedUser.UserName;
                        itemObj.CREATEDDATE = DateTime.Now;
                        _db.MT_ITEM.Add(itemObj);
                        _db.SaveChanges();

                        dataItem.ITEMID = itemObj.ITEMID;
                        result.IsCompleted = true;
                        result.Message.Add(Resources.Message.SaveSuccess);
                        
                       
                        tran.Commit();
                        if(dataItem.ITEMID == 0)
                        {

                            var checkID = (from getitemId in _db.MT_ITEM.Where(l => (l.ITEMGROUP == dataItem.ITEMGROUP) && (l.ITEMNAME == dataItem.ITEMNAME) && (l.ORDERNO == itemObj.ORDERNO))
                                           select new ServiceModels.Item()
                                           {
                                               ITEMID = getitemId.ITEMID
                                           }).FirstOrDefault();
                            dataItem.ITEMID = checkID.ITEMID;
                        }
                        result.Data = dataItem;
                    }
                    return result;
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
                return result;
            }
           

        }
        public ServiceModels.ReturnMessage DeleteItem(decimal itemId)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.MT_ITEM item = _db.MT_ITEM.Where(x => x.ITEMID == itemId).FirstOrDefault();
                if (item != null)
                {

                    item.ISDELETE = "1";
                    _db.SaveChanges();

                    result.IsCompleted = true;
                    result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "User Profile", ex);
            }

            return result;
        }

    }
}
