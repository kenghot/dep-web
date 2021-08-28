using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.Common;
using Nep.Project.ServiceModels;
using EntityFramework.Extensions;
using System.Data;
using System.IO;
using System.Data.Entity;

namespace Nep.Project.Business
{
    public class RegisterService : IServices.IRegisterService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly Business.MailService _mailService;
        private readonly ServiceModels.Security.SecurityInfo _loggedUser;
        private readonly Decimal _systemID;

        public RegisterService(DBModels.Model.NepProjectDBEntities db, Business.MailService mailService, ServiceModels.Security.SecurityInfo loggedUser)
        {
            _db = db;
            _mailService = mailService;
            _loggedUser = loggedUser;

            _systemID = _db.SC_User.Where(x => x.UserName == Common.Constants.SYSTEM_USERNAME).Select(y => y.UserID).FirstOrDefault();
        }

        public ServiceModels.ReturnMessage CreateRegisterEntry(ServiceModels.RegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage)
        {
            var result = new ServiceModels.ReturnMessage();
            decimal? orgID = registerEntry.OrganizationID;

            using (var tran = _db.Database.BeginTransaction())
            {
                var date = DateTime.Now.AddDays(-15);

                var chkDup = CheckDupUsername(registerEntry.Email);
                //kenghot
                //var chkOrg = (orgID.HasValue)?((from o in _db.UserRegisterEntries
                //              where ((o.OrganizationID != null) && (o.OrganizationID == orgID))  && (o.RegisterDate > date) && (o.RegisteredUserID == null)
                //              select o).Count() == 0 &&
                //            (from u in _db.SC_User
                //            where (u.OrganizationID  != null && (u.OrganizationID == orgID) && (u.IsDelete == Common.Constants.BOOLEAN_FALSE))
                //            select u).Count() == 0) : true;
                var chkOrg = true;
                //end kenghot

                if (chkDup && chkOrg)
                {
                    var isError = false;
                    var entry = new DBModels.Model.UserRegisterEntry()
                    {
                        Email = registerEntry.Email,
                        Firstname = registerEntry.FirstName,
                        Lastname = registerEntry.LastName,
                        OrganizationID = registerEntry.OrganizationID,
                        PersonalID = registerEntry.PersonalID,
                        Position = registerEntry.Position,
                        Telephone = registerEntry.TelephoneNo,
                        Mobile = registerEntry.Mobile,
                        RegisterDate = DateTime.Now,
                        ActivationCode = Common.PasswordEncrypter.GenerateActivationCode()
                    };
                    var attachmentRootPath = _db.MT_OrganizationParameter.FirstOrDefault(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;
                    if (personalIdCardImage != null)
                    {
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);
                        if (!Directory.Exists(regisPath))
                        {
                            Directory.CreateDirectory(regisPath);
                        }

                        var targetFilePath = Path.Combine(regisPath, personalIdCardImage.tempId);
                        string pathName = Path.Combine(regisFolder, personalIdCardImage.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, personalIdCardImage.tempId)));
                        if (file.Exists)
                        {
                            if(!File.Exists(targetFilePath)){

                                file.CopyTo(targetFilePath);
                            }
                            
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = personalIdCardImage.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.PERSONAL_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            entry.PersonalIDCardAttachment = fileDb;
                        }
                        else
                        {
                            ///Error
                            isError = true;
                        }
                    }

                    if (staffCardImage != null)
                    {
                        String regisFolder = "regis";
                        String regisPath = Path.Combine(attachmentRootPath, regisFolder);
                        if (!Directory.Exists(regisPath))
                        {
                            Directory.CreateDirectory(regisPath);
                        }

                        var targetFilePath = Path.Combine(regisPath, staffCardImage.tempId);
                        string pathName = Path.Combine(regisFolder, staffCardImage.tempId);
                        var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, staffCardImage.tempId)));
                        if (file.Exists)
                        {
                            if (!File.Exists(targetFilePath))
                            {
                                file.CopyTo(targetFilePath);
                            }
                           
                            var fileDb = new DBModels.Model.MT_Attachment()
                            {
                                AttachmentFilename = staffCardImage.name,
                                AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.EMPLOYEE_ID_CARD),
                                FileSize = file.Length,
                                PathName = pathName,
                                CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                CreatedByID = _systemID,
                                CreatedDate = DateTime.Now
                            };

                            _db.MT_Attachment.Add(fileDb);
                            entry.EmployeeIDCardAttachment = fileDb;
                        }
                    }

                    if (!isError)
                    {
                        _db.UserRegisterEntries.Add(entry);
                        var savedResult = _db.SaveChanges();
                        tran.Commit();
                        if (savedResult >= 1)
                        {
                            result.IsCompleted = true;
                            _mailService.SendUserRegistrationNotify(entry.EntryID);
                           
                            
                        }
                    }
                }
                else
                {
                    if (!chkDup)
                    {
                        result.Message.Add(String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.UserProfile_Email));
                    }
                    else
                    {
                        result.Message.Add(Nep.Project.Resources.Error.OrgRegistryDup);
                    }                    
                }
            }

            return result;
        }
        //public ServiceModels.ReturnMessage UpdateRegisterEntry(ServiceModels.RegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage)
        //{
        //    var result = new ServiceModels.ReturnMessage();
        //    decimal? orgID = registerEntry.OrganizationID;

        //    using (var tran = _db.Database.BeginTransaction())
        //    {
        //        var date = DateTime.Now.AddDays(-15);

        //        var chkDup = CheckDupUsername(registerEntry.Email);
        //        //kenghot
        //        //var chkOrg = (orgID.HasValue)?((from o in _db.UserRegisterEntries
        //        //              where ((o.OrganizationID != null) && (o.OrganizationID == orgID))  && (o.RegisterDate > date) && (o.RegisteredUserID == null)
        //        //              select o).Count() == 0 &&
        //        //            (from u in _db.SC_User
        //        //            where (u.OrganizationID  != null && (u.OrganizationID == orgID) && (u.IsDelete == Common.Constants.BOOLEAN_FALSE))
        //        //            select u).Count() == 0) : true;
        //        var chkOrg = true;
        //        //end kenghot

        //        if (chkDup && chkOrg)
        //        {
        //            var isError = false;
        //            var entry = new DBModels.Model.UserRegisterEntry()
        //            {
        //                Email = registerEntry.Email,
        //                Firstname = registerEntry.FirstName,
        //                Lastname = registerEntry.LastName,
        //                OrganizationID = registerEntry.OrganizationID,
        //                PersonalID = registerEntry.PersonalID,
        //                Position = registerEntry.Position,
        //                Telephone = registerEntry.TelephoneNo,
        //                Mobile = registerEntry.Mobile,
        //                RegisterDate = DateTime.Now,
        //                ActivationCode = Common.PasswordEncrypter.GenerateActivationCode()
        //            };
        //            var attachmentRootPath = _db.MT_OrganizationParameter.FirstOrDefault(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;
        //            if (personalIdCardImage != null)
        //            {
        //                String regisFolder = "regis";
        //                String regisPath = Path.Combine(attachmentRootPath, regisFolder);
        //                if (!Directory.Exists(regisPath))
        //                {
        //                    Directory.CreateDirectory(regisPath);
        //                }

        //                var targetFilePath = Path.Combine(regisPath, personalIdCardImage.tempId);
        //                string pathName = Path.Combine(regisFolder, personalIdCardImage.tempId);
        //                var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, personalIdCardImage.tempId)));
        //                if (file.Exists)
        //                {
        //                    if (!File.Exists(targetFilePath))
        //                    {

        //                        file.CopyTo(targetFilePath);
        //                    }

        //                    var fileDb = new DBModels.Model.MT_Attachment()
        //                    {
        //                        AttachmentFilename = personalIdCardImage.name,
        //                        AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.PERSONAL_ID_CARD),
        //                        FileSize = file.Length,
        //                        PathName = pathName,
        //                        CreatedBy = Common.Constants.SYSTEM_USERNAME,
        //                        CreatedByID = _systemID,
        //                        CreatedDate = DateTime.Now
        //                    };

        //                    _db.MT_Attachment.Add(fileDb);
        //                    entry.PersonalIDCardAttachment = fileDb;
        //                }
        //                else
        //                {
        //                    ///Error
        //                    isError = true;
        //                }
        //            }

        //            if (staffCardImage != null)
        //            {
        //                String regisFolder = "regis";
        //                String regisPath = Path.Combine(attachmentRootPath, regisFolder);
        //                if (!Directory.Exists(regisPath))
        //                {
        //                    Directory.CreateDirectory(regisPath);
        //                }

        //                var targetFilePath = Path.Combine(regisPath, staffCardImage.tempId);
        //                string pathName = Path.Combine(regisFolder, staffCardImage.tempId);
        //                var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, staffCardImage.tempId)));
        //                if (file.Exists)
        //                {
        //                    if (!File.Exists(targetFilePath))
        //                    {
        //                        file.CopyTo(targetFilePath);
        //                    }

        //                    var fileDb = new DBModels.Model.MT_Attachment()
        //                    {
        //                        AttachmentFilename = staffCardImage.name,
        //                        AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.EMPLOYEE_ID_CARD),
        //                        FileSize = file.Length,
        //                        PathName = pathName,
        //                        CreatedBy = Common.Constants.SYSTEM_USERNAME,
        //                        CreatedByID = _systemID,
        //                        CreatedDate = DateTime.Now
        //                    };

        //                    _db.MT_Attachment.Add(fileDb);
        //                    entry.EmployeeIDCardAttachment = fileDb;
        //                }
        //            }

        //            if (!isError)
        //            {
        //                _db.UserRegisterEntries.Add(entry);
        //                var savedResult = _db.SaveChanges();
        //                tran.Commit();
        //                if (savedResult >= 1)
        //                {
        //                    result.IsCompleted = true;
        //                    _mailService.SendUserRegistrationNotify(entry.EntryID);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (!chkDup)
        //            {
        //                result.Message.Add(String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.UserProfile_Email));
        //            }
        //            else
        //            {
        //                result.Message.Add(Nep.Project.Resources.Error.OrgRegistryDup);
        //            }
        //        }
        //    }

        //    return result;
        //}
        public IQueryable<DecimalDropDownListData> ListOrganization()
        {
            return _db.MT_Organization.Where(org => org.User.Where(u=> u.IsDelete == "0").Count() == 0).Select(x => new DecimalDropDownListData()
            {
                Text = x.OrganizationNameTH,
                Value = x.OrganizationID
            });
        }

        public ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> ListOrganizationRegister(ServiceModels.QueryParameter param)
        {
            ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> result = new ReturnQueryData<DecimalDropDownListData>();
            List<ServiceModels.DecimalDropDownListData> list = new List<DecimalDropDownListData>();
            //kenghot
            //result = (from org in _db.MT_Organization
            //          where (org.User.Where(u => u.IsDelete == "0").Count() == 0)
            //          select new ServiceModels.DecimalDropDownListData(){
            //                Value = org.OrganizationID,
            //                Text = org.OrganizationNameTH
            //            }
            //          ).ToQueryData(param);
            result = (from org in _db.MT_Organization
                      //where (org.User.Where(u => u.IsDelete == "0").Count() == 0)
                      select new ServiceModels.DecimalDropDownListData()
                      {
                          Value = org.OrganizationID,
                          Text = org.OrganizationNameTH
                      }
                    ).ToQueryData(param);

            //_db.Database.Log = ((x) => { Common.Logging.LogInfo("-->>", x); });
            return result;
        }

        public ServiceModels.ReturnQueryData<Int32> ListRegisteredOrganizationMapping(List<decimal> orgIds)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();
            try
            {
                result.IsCompleted = true;
                List<Int32> indices = new List<Int32>();
                if (orgIds != null && orgIds.Count > 0)
                {
                    var query = (from org in _db.MT_Organization
                                 //kenghot
                                 //where (org.User.Where(u => u.IsDelete == "0").Count() == 0)
                                 orderby org.OrganizationNameTH ascending
                                 select new ServiceModels.DecimalDropDownListData()
                                 {
                                     Value = org.OrganizationID,
                                     Text = org.OrganizationNameTH
                                 }
                      );

                    List<decimal> dbOrgIds = query.Select(y => y.Value).ToList();
                    Int32 index = 0;
                    foreach (decimal id in dbOrgIds)
                    {
                        if (orgIds.Contains(id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                result.Data = indices;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }

        public IQueryable<DecimalDropDownListData> ListProvince()
        {
            return _db.MT_Province.Select(x => new DecimalDropDownListData()
            {
                Text = x.ProvinceName,
                Value = x.ProvinceID
            });
        }

        public ReturnObject<RegisterEntry> GetRegistryUser(int id, String activationCode)
        {
            var result = new ReturnObject<RegisterEntry>();
            var date = DateTime.Now.AddDays(-15);
            var data = _db.UserRegisterEntries
                .Where(x => x.ActivationCode == activationCode && x.EntryID == id)
                .Select(x => new RegisterEntry()
                {
                    Email = x.Email,
                    FirstName = x.Firstname,
                    IdentityAttachmentID = x.PersonalIDCardAttachmentID,
                    IdentityAttachmentName = x.PersonalIDCardAttachment.AttachmentFilename,
                    LastName = x.Lastname,
                    OrganizationID = x.OrganizationID,
                    OrganizationName = x.Organization.OrganizationNameTH,
                    OrgIdentityAttachmentID = (x.EmployeeIDCardAttachmentID ?? 0),
                    OrgIdentityAttachmentName = x.EmployeeIDCardAttachment.AttachmentFilename,
                    PersonalID = x.PersonalID,
                    Position = x.Position,
                    RegisterEntryID = (int) x.EntryID,
                    RegisterDate = x.RegisterDate,
                    TelephoneNo = x.Telephone,
                    RegisterName = x.Firstname + " " + x.Lastname,
                    IsActive = (x.RegisteredUser != null)? x.RegisteredUser.IsActive : null,
                    RegisteredUserID = x.RegisteredUserID,
                    OrgApprovalDate = x.OrganizationRegisterEntries.Where(orgR => orgR.UserEntryID == x.EntryID).Select(y => y.ApprovedDate).FirstOrDefault()
                })
                .OrderByDescending(or => or.RegisterDate)
                .FirstOrDefault();


            if ((data == null) || ((data != null) && (data.OrganizationID.HasValue) && (data.RegisteredUserID != null)))
            {
                result.Message.Add(Nep.Project.Resources.Error.RegisterNoFound);
            }
            else
            {
                //External user ต้องยืนยันภายใน  วันหลังจากสมัคร
                if (((data.OrganizationID.HasValue) && (data.RegisterDate > date) && (data.RegisteredUserID == null)) || 
                    (data.OrgApprovalDate.HasValue && (((DateTime)data.OrgApprovalDate) > date)))
                {
                    result.IsCompleted = true;
                    result.Data = data;
                }
                else if ((data.OrganizationID.HasValue && (data.RegisteredUserID == null) && data.OrgApprovalDate == null && (data.RegisterDate <= date)) ||
                    (data.OrgApprovalDate.HasValue && (((DateTime)data.OrgApprovalDate) <= date)))
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Error.RegisterExpired);
                }
                else if ((data.OrganizationID == null) && (data.RegisteredUserID == null) && (data.OrgApprovalDate.HasValue && (((DateTime)data.OrgApprovalDate) > date)))
                {
                    result.IsCompleted = true;
                    result.Data = data;
                }
                else if ((data.OrganizationID == null) && (data.RegisteredUserID.HasValue))
                {
                    //Interanal user ไม่ต้อง validate ให้ยืนยันภายใน 15 วันหลังจากสมัคร

                    DBModels.Model.SC_User scUser = _db.SC_User.Where(x => (x.Email == data.Email) && (x.Password == null) && (x.IsDelete == "0")).FirstOrDefault();
                    if ((scUser != null) && (scUser.IsActive == Common.Constants.BOOLEAN_TRUE))
                    {
                        result.IsCompleted = true;
                        result.Data = data;
                    }
                    else if ((scUser != null))
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.UserInactiveError);
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.RegisterNoFound);
                    }                    
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Error.NotFoundUserRegistry);
                } 
            }
            return result;
        } 

        public ReturnMessage CreateExternalUser(ConfirmEmail confirmEmail)
        {
            var result = new ReturnMessage();
            var entry = _db.UserRegisterEntries
                           .Include(x=>x.Organization)
                           .Where(x => x.ActivationCode == confirmEmail.ActivationCode && x.EntryID == confirmEmail.RegisterEntryID && x.RegisteredUserID == null).FirstOrDefault();

            if (entry != null)
            {
                var salt = Common.PasswordEncrypter.GenerateSalt();

                //Validate Existing User in Org
                //kenghot
                //var userCount = _db.SC_User.Where(x =>
                //    x.OrganizationID == entry.OrganizationID
                //    && x.IsDelete == Constants.BOOLEAN_FALSE
                //).Count();
                //if (userCount >= 1)
                //{
                //    // 1 องค์กรมี user ใช้งาน 1 user
                //    result.Message.Add(Nep.Project.Resources.Error.OrgRegistryDup);
                //    result.IsCompleted = false;
                //    return result;
                //}
                //end kenghot
                //องค์กรภายนอก
                decimal userGroupID = _db.SC_Group.Where(x => x.GroupCode == Common.UserGroupCode.องค์กรภายนอก).Select(y => y.GroupID).FirstOrDefault();
                var user = new DBModels.Model.SC_User()
                {
                    Email = entry.Email,
                    EmploeeCardFileID = entry.EmployeeIDCardAttachmentID,
                    FirstName = entry.Firstname,
                    //No Group
                    GroupID = userGroupID,
                    IDCardFileID = entry.PersonalIDCardAttachmentID,
                    IDNo = entry.PersonalID,
                    IsActive = Constants.BOOLEAN_TRUE,
                    IsDelete = Constants.BOOLEAN_FALSE,
                    LastName = entry.Lastname,
                    OrganizationID = entry.OrganizationID,
                    Password = Common.PasswordEncrypter.Encrypt(confirmEmail.Password, salt),
                    Position = entry.Position,
                    ProvinceID = entry.Organization.ProvinceID,
                    RegisterDate = entry.RegisterDate,
                    Salt = salt,
                    TelephoneNo = entry.Telephone,
                    UserFlag = Constants.BOOLEAN_TRUE,
                    UserName = entry.Email,

                    CreatedBy = Constants.SYSTEM_USERNAME,
                    CreatedByID = _systemID,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = Constants.SYSTEM_USERNAME,
                    UpdatedByID = _systemID,
                    UpdatedDate = DateTime.Now
                };

                _db.SC_User.Add(user);
                entry.RegisteredUser = user;
                _db.SaveChanges();

                result.IsCompleted = true;
                result.Message.Add("ระบบได้สร้างบัญชีของท่านเรียบร้อยแล้ว กรุณากดปุ่ม กลับ เพื่อกลับไปยังหน้าเข้าสู่ระบบ");
                               
                
            }
            else
            {
                result.Message.Add(Nep.Project.Resources.Error.NotFoundUserRegistry);
            }

            return result;
        }

        public ReturnMessage CreatePasswordInternalUser(ConfirmEmail confirmEmail)
        {
            var result = new ReturnMessage();

            try
            {
                var entry = _db.UserRegisterEntries
                           .Include(x => x.Organization)
                           .Where(x => x.ActivationCode == confirmEmail.ActivationCode && x.EntryID == confirmEmail.RegisterEntryID && x.RegisteredUserID == confirmEmail.RegisteredUserID).FirstOrDefault();

                if (entry != null)
                {
                    var salt = Common.PasswordEncrypter.GenerateSalt();

                    
                    //เจ้าหน้าที่สร้าง password ใช้งาน
                    var scUser = _db.SC_User.Where(x => x.UserID == (decimal)entry.RegisteredUserID).FirstOrDefault();
                    if ((scUser != null) && (scUser.Password == null))
                    {                        
                        scUser.Password = Common.PasswordEncrypter.Encrypt(confirmEmail.Password, salt);
                        scUser.Salt = salt;
                        scUser.UpdatedBy = Common.Constants.SYSTEM_USERNAME;
                        scUser.UpdatedByID = _systemID;
                        scUser.UpdatedDate = DateTime.Now;

                        entry.RegisteredUser = scUser;

                        _db.SaveChanges();

                        
                        result.IsCompleted = true;
                        result.Message.Add("ระบบได้สร้างรหัสผ่านของท่านเรียบร้อยแล้ว กรุณากดปุ่ม กลับ เพื่อกลับไปยังหน้าเข้าสู่ระบบ");
                        return result;
                    }
                    else
                    {
                        result.Message.Add(Nep.Project.Resources.Message.NoRecord);
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
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Register", ex);
            }           

            return result;
        }

        public ReturnMessage CreateOrganizationRegisterEntry(ServiceModels.OrganizationRegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage)
        {
            var result = new ReturnMessage();
            try
            {
                string orgNameTh = registerEntry.OrganizationNameTH;
                string orgNameEn = registerEntry.OrganizationNameEN;

                using (var tran = _db.Database.BeginTransaction())
                {
                    var date = DateTime.Now.AddDays(-15);
                                       
                    var chkDup = CheckDupUsername(registerEntry.EmailUser);

                    var chkOrg = (from o in _db.OrganizationRegisterEntries
                                  where (o.OrganizationNameTH.Equals(orgNameTh) || ((o.OrganizationNameEN != null) && o.OrganizationNameEN.Equals(orgNameEn)))
                                  select o).Count() == 0 &&
                                  (from o in _db.MT_Organization
                                   where (o.OrganizationNameTH.Equals(orgNameTh) || ((o.OrganizationNameEN != null) &&  o.OrganizationNameEN.Equals(orgNameEn)))
                                   select o).Count() == 0;

                    //Validate Organization Name Thai Unique
                    //Validate Organization Name Eng Unique
                    //Validate PersonalID Unique
                    //Validate EmailUser Unique
                    //Validate EmailOrganization Unique

                    if (chkDup && chkOrg)
                    {
                        var isError = false;
                        var entry = new DBModels.Model.UserRegisterEntry()
                        {
                            Email = registerEntry.EmailUser,
                            Firstname = registerEntry.FirstName,
                            Lastname = registerEntry.LastName,
                            PersonalID = registerEntry.PersonalID,
                            Position = registerEntry.Position,
                            Telephone = registerEntry.TelephoneNoUser,        
                            Mobile = registerEntry.MobileUser,
                            RegisterDate = DateTime.Now,
                            ActivationCode = Common.PasswordEncrypter.GenerateActivationCode()
                        };
                        var attachmentRootPath = _db.MT_OrganizationParameter.FirstOrDefault(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;
                        if (personalIdCardImage != null)
                        {
                            var targetFilePath = Path.Combine(attachmentRootPath, "regis", personalIdCardImage.tempId);
                            var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, personalIdCardImage.tempId)));
                            if (file.Exists)
                            {
                                if (!File.Exists(targetFilePath))
                                {
                                    file.CopyTo(targetFilePath);
                                }
                               
                                var fileDb = new DBModels.Model.MT_Attachment()
                                {
                                    AttachmentFilename = personalIdCardImage.name,
                                    AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.PERSONAL_ID_CARD),
                                    FileSize = file.Length,
                                    PathName = targetFilePath,
                                    CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                    CreatedByID = _systemID,
                                    CreatedDate = DateTime.Now
                                };

                                _db.MT_Attachment.Add(fileDb);
                                entry.PersonalIDCardAttachment = fileDb;
                            }
                            else
                            {
                                ///Error
                                isError = true;
                            }
                        }

                        if (staffCardImage != null)
                        {
                            var targetFilePath = Path.Combine(attachmentRootPath, "regis", staffCardImage.tempId);
                            var file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(Path.Combine(Common.Constants.UPLOAD_TEMP_PATH, staffCardImage.tempId)));
                            if (file.Exists)
                            {
                                if (!File.Exists(targetFilePath))
                                {
                                    file.CopyTo(targetFilePath);
                                }
                               
                                var fileDb = new DBModels.Model.MT_Attachment()
                                {
                                    AttachmentFilename = staffCardImage.name,
                                    AttachmentType = _db.MT_ListOfValue.First(x => x.LOVGroup == Common.LOVGroup.AttachmentType && x.LOVCode == Common.LOVCode.Attachmenttype.EMPLOYEE_ID_CARD),
                                    FileSize = file.Length,
                                    PathName = targetFilePath,
                                    CreatedBy = Common.Constants.SYSTEM_USERNAME,
                                    CreatedByID = _systemID,
                                    CreatedDate = DateTime.Now
                                };

                                _db.MT_Attachment.Add(fileDb);
                                entry.EmployeeIDCardAttachment = fileDb;
                            }
                        }

                        if (!isError)
                        {
                            _db.UserRegisterEntries.Add(entry);

                            var orgEntry = new DBModels.Model.OrganizationRegisterEntry()
                            {
                                Address = registerEntry.Address,
                                Building = registerEntry.Building,
                                DistrictID = registerEntry.DistrictID,
                                Email = registerEntry.EmailOrganization,
                                Fax = registerEntry.Fax,
                                Moo = registerEntry.Moo,
                                OrganizationNameEN = registerEntry.OrganizationNameEN,
                                OrganizationNameTH = registerEntry.OrganizationNameTH,
                                OrganizationTypeID = registerEntry.OrganizationType,
                                OrganizationTypeETC = registerEntry.OrganizationTypeEtc,
                                OrganizationYear = registerEntry.OrganizationYear,
                                OrgEstablishedDate = registerEntry.OrganizationDate,
                                OrgUnderSupport = registerEntry.OrgUnderSupport,
                                PostCode = registerEntry.PostCode,
                                ProvinceID = registerEntry.ProvinceID,
                                RegisterDate = DateTime.Now,
                                Road = registerEntry.Road,
                                Soi = registerEntry.Soi,
                                SubDistrictID = registerEntry.SubDistrictID,
                                Telephone = registerEntry.TelephoneNoOrganization,
                                Mobile = registerEntry.MobileOrganization,

                                UserRegisterEntry = entry,
                            };

                            _db.OrganizationRegisterEntries.Add(orgEntry);
                            var savedResult = _db.SaveChanges();
                            tran.Commit();
                            if (savedResult >= 1)
                            {
                                result.IsCompleted = true;
                                _mailService.SendOrganizationRegistrationNotify(orgEntry.EntryID);
                                var sms = new Business.SmsService();
                                sms.Send("ระบบได้รับข้อมูลการสมัครสมาชิกของท่านแล้ว", new string[] { "registerEntry.MobileOrganization" });
                            }
                        }
                    }
                    else
                    {
                        if (!chkDup)
                        {
                            result.Message.Add(String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.UserProfile_Email));
                        }
                        else
                        {
                            result.Message.Add(Nep.Project.Resources.Error.OrgNameRegistryDup);
                        }
                        
                    }
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

        public ReturnQueryData<ServiceModels.RegisteredOrganizationList> ListRegisteredOrganization(ServiceModels.QueryParameter param)
        {
            var query = from o in _db.OrganizationRegisterEntries
                        select new RegisteredOrganizationList()
                        {
                            OrganizationEntryID = o.EntryID,
                            Address = o.Address,
                            Building = o.Building,
                            District = o.District.DistrictName,
                            Moo = o.Moo,
                            OrganizationName = o.OrganizationNameTH,
                            OrganizationType = o.OrganizationType.OrganizationType,
                            OrganizationTypeEtc = o.OrganizationTypeETC,
                            PostCode = o.PostCode,
                            Province = o.Province.ProvinceName,
                            ProvinceID = o.ProvinceID,
                            RegisterDate = o.RegisterDate,
                            RegisterName = o.UserRegisterEntry.Firstname + " " + o.UserRegisterEntry.Lastname,
                            Road = o.Road, 
                            Soi = o.Soi, 
                            SubDistrict=o.SubDistrict.SubDistrictName,
                            OrgUnderSupport = o.OrgUnderSupport,
                            ApproveDate = o.ApprovedDate,
                            IsDeletable = (o.ApprovedDate == null)? true : false
                        };

            var result = query.ToQueryData(param);
            return result;
        }
     
        public ReturnQueryData<ServiceModels.RegisteredOrganizationList> ListRegisteredOrganization(ServiceModels.QueryParameter param,Boolean isApprove)
        {
            var query = from o in _db.OrganizationRegisterEntries
                        select new RegisteredOrganizationList()
                        {
                            OrganizationEntryID = o.EntryID,
                            Address = o.Address,
                            Building = o.Building,
                            District = o.District.DistrictName,
                            Moo = o.Moo,
                            OrganizationName = o.OrganizationNameTH,
                            OrganizationType = o.OrganizationType.OrganizationType,
                            OrganizationTypeEtc = o.OrganizationTypeETC,
                            PostCode = o.PostCode,
                            Province = o.Province.ProvinceName,
                            ProvinceID = o.ProvinceID,
                            RegisterDate = o.RegisterDate,
                            RegisterName = o.UserRegisterEntry.Firstname + " " + o.UserRegisterEntry.Lastname,
                            Road = o.Road,
                            Soi = o.Soi,
                            SubDistrict = o.SubDistrict.SubDistrictName,
                            OrgUnderSupport = o.OrgUnderSupport,
                            ApproveDate = o.ApprovedDate,
                            IsDeletable = (o.ApprovedDate == null) ? true : false
                        };
            if (isApprove)
            {
               query = query.Where(w => w.ApproveDate.HasValue);
            }
            else
            {
                query = query.Where(w => !w.ApproveDate.HasValue);
            }
           
            var result = query.ToQueryData(param);
            return result;
        }
        public ServiceModels.ProjectInfo.DashBoardORG GetORGDashBoardData(decimal provinceId, int year)
        {
            ServiceModels.ProjectInfo.DashBoardORG ret = new ServiceModels.ProjectInfo.DashBoardORG();

            //ret.Add(new List<ProjectInfoList>()); //all
            //ret.Add(new List<ProjectInfoList>()); //almost expire
            //ret.Add(new List<ProjectInfoList>()); //not report
            //ret.Add(new List<ProjectInfoList>()); // follow
            //ret.Add(new List<ProjectInfoList>()); // new
            var query = (from o in _db.OrganizationRegisterEntries
                         where o.RegisterDate.Year == year  
                        select new RegisteredOrganizationList()
                        {
                            OrganizationEntryID = o.EntryID,
                            Address = o.Address,
                            Building = o.Building,
                            District = o.District.DistrictName,
                            Moo = o.Moo,
                            OrganizationName = o.OrganizationNameTH,
                            OrganizationType = o.OrganizationType.OrganizationType,
                            OrganizationTypeEtc = o.OrganizationTypeETC,
                            PostCode = o.PostCode,
                            Province = o.Province.ProvinceName,
                            ProvinceID = o.ProvinceID,
                            RegisterDate = o.RegisterDate,
                            RegisterName = o.UserRegisterEntry.Firstname + " " + o.UserRegisterEntry.Lastname,
                            Road = o.Road,
                            Soi = o.Soi,
                            SubDistrict = o.SubDistrict.SubDistrictName,
                            OrgUnderSupport = o.OrgUnderSupport,
                            ApproveDate = o.ApprovedDate,
                        });
            if (provinceId != 0)
            {
                query = query.Where(w => w.ProvinceID == provinceId);
            }
            int isApproved, notApprove ;
            decimal followAmt, expireAmt, newreqAmt, notreportAmt, allNotexpAmt, otherAmt;
            isApproved = notApprove = 0;
            followAmt = expireAmt = newreqAmt = notreportAmt = allNotexpAmt = otherAmt = 0;
            var today = DateTime.Now.Date;
            //var lov = new ListOfValueService(_db);
            //var l = lov.GetListOfValueByCode(Common.LOVGroup.FollowupStatus, Common.LOVCode.Followupstatus.รายงานผลแล้ว);
            decimal BudVal = 0;
            foreach (RegisteredOrganizationList o in query)
            {
                if (o.ApproveDate.HasValue)
                {
                    isApproved += 1;
                }else
                {
                    notApprove += 1;
                }
             
            }
            ret.OrganizationList = query.ToList();
            ret.ORGCountByStatus = new int[] { isApproved , notApprove};

            ServiceModels.KendoChart pie = new KendoChart();
            ServiceModels.KendoChartSerie s = new ServiceModels.KendoChartSerie();
            pie.series = new List<ServiceModels.KendoChartSerie>();
            pie.series.Add(s);
            s.type = "pie";
            s.data = new List<ServiceModels.KendoChartData>();
            s.data.Add(new ServiceModels.KendoChartData { category = "อนุมัติ", value = isApproved, color = "Lime", remark = "1" });
            s.data.Add(new ServiceModels.KendoChartData { category = "ยังไม่อนุมัติ", value = notApprove, color = "Yellow", remark = "2" });
            //s.data.Add(new ServiceModels.KendoChartData { category = "ไม่ส่งรายงานการประเมินผล", value = notreportAmt, color = "Orange", remark = "3" });
            //s.data.Add(new ServiceModels.KendoChartData { category = "รอการติดตามประเมินผล", value = followAmt, color = "Fuchsia", remark = "4" });
            //s.data.Add(new ServiceModels.KendoChartData { category = "ที่เสนอมาใหม่", value = newreqAmt, color = "Lime", remark = "5" });
            //s.data.Add(new ServiceModels.KendoChartData { category = "อื่นๆ", value = newreqAmt, color = "Silver", remark = "6" });
            ret.BudgetChart = pie;
            return ret;
        }
        public ReturnObject<ServiceModels.OrganizationRegisterEntry> GetRegisteredOrganization(Decimal id)
        {
            var result = new ReturnObject<ServiceModels.OrganizationRegisterEntry>();
            var query = from o in _db.OrganizationRegisterEntries
                        where o.EntryID == id
                       select new ServiceModels.OrganizationRegisterEntry
                       {
                           Address = o.Address,
                           Building = o.Building,
                           DistrictID = o.DistrictID,
                           EmailOrganization = o.Email,
                           Fax = o.Fax,
                           Moo = o.Moo,
                           OrganizationNameEN = o.OrganizationNameEN,
                           OrganizationNameTH = o.OrganizationNameTH,
                           OrganizationType = o.OrganizationTypeID,
                           OrganizationTypeEtc = o.OrganizationTypeETC,
                           OrganizationYear = o.OrganizationYear,
                           OrganizationDate = o.OrgEstablishedDate,
                           OrgUnderSupport = o.OrgUnderSupport,
                           PostCode = o.PostCode,
                           ProvinceID = o.ProvinceID,
                           RegisterDate = o.RegisterDate,
                           Road = o.Road,
                           Soi = o.Soi,
                           SubDistrictID = o.SubDistrictID,
                           TelephoneNoOrganization = o.Telephone,
                           MobileOrganization = o.Mobile,
                           EmailUser = o.UserRegisterEntry.Email,
                           FirstName = o.UserRegisterEntry.Firstname,
                           LastName = o.UserRegisterEntry.Lastname,
                           PersonalID = o.UserRegisterEntry.PersonalID,
                           IdentityAttachmentID = o.UserRegisterEntry.PersonalIDCardAttachmentID,
                           OrgIdentityAttachmentID = o.UserRegisterEntry.EmployeeIDCardAttachmentID,
                           Position = o.UserRegisterEntry.Position,
                           RegisterName = o.UserRegisterEntry.Firstname + " " + o.UserRegisterEntry.Lastname,
                           TelephoneNoUser = o.UserRegisterEntry.Telephone,
                           MobileUser = o.UserRegisterEntry.Mobile,
                           SubDistrict = o.SubDistrict.SubDistrictName,
                           District = o.District.DistrictName,
                           Province = o.Province.ProvinceName,
                           OrganizationTypeName = o.OrganizationType.OrganizationType,
                           ApprovedBy = o.ApprovedBy,
                           ApprovedDate = o.ApprovedDate
                       };
            var data = query.FirstOrDefault();

            if (data != null)
            {
                if(data.IdentityAttachmentID.HasValue){
                    var dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.IdentityAttachmentID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };
                        data.IdentityAttachment = file;
                    }
                }

                if (data.OrgIdentityAttachmentID.HasValue)
                {
                    var dbAttach = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)data.OrgIdentityAttachmentID).FirstOrDefault();
                    if (dbAttach != null)
                    {
                        ServiceModels.KendoAttachment file = new KendoAttachment()
                        {
                            id = dbAttach.AttachmentID.ToString(),
                            name = dbAttach.AttachmentFilename,
                            extension = Path.GetExtension(dbAttach.AttachmentFilename),
                            size = (int)dbAttach.FileSize
                        };
                        data.OrgIdentityAttachment = file;
                    }
                }
                result.Data = data;
                result.IsCompleted = true;
            }
            else
            {
                result.IsCompleted = false;
                result.Message.Add(Resources.Message.NoRecord);
            }

            return result;
        }

        public ReturnMessage ApproveRegisteredOrganization(Decimal entryId)
        {
            var result = new ReturnMessage();

            using (var tran = _db.Database.BeginTransaction())
            {
                var registeredOrganization = (from o in _db.OrganizationRegisterEntries.Include(x => x.UserRegisterEntry)
                                             where o.EntryID == entryId
                                             select o).FirstOrDefault();

                if (registeredOrganization != null)
                {
                    registeredOrganization.ApprovedBy = _loggedUser.UserName;
                    registeredOrganization.ApprovedByID = _loggedUser.UserID;
                    registeredOrganization.ApprovedDate = DateTime.Now;

                    var newOrg = new DBModels.Model.MT_Organization(){
                        Address = registeredOrganization.Address,
                        Building = registeredOrganization.Building,
                        CreatedBy = registeredOrganization.ApprovedBy,
                        CreatedByID = (decimal)registeredOrganization.ApprovedByID,
                        CreatedDate = registeredOrganization.ApprovedDate.Value,
                        DistrictID = registeredOrganization.DistrictID,
                        Email = registeredOrganization.Email,
                        Fax = registeredOrganization.Fax,
                        Moo = registeredOrganization.Moo,
                        OrganizationNameEN = registeredOrganization.OrganizationNameEN,
                        OrganizationNameTH = registeredOrganization.OrganizationNameTH,
                        //OrganizationNo = registeredOrganization.,
                        OrganizationTypeEtc = registeredOrganization.OrganizationTypeETC,
                        OrganizationTypeID = registeredOrganization.OrganizationTypeID,
                        OrganizationYear = registeredOrganization.OrganizationYear,
                        OrgUnderSupport = registeredOrganization.OrgUnderSupport,
                        PostCode = registeredOrganization.PostCode,
                        ProvinceID = registeredOrganization.ProvinceID,
                        RequestDate = registeredOrganization.RegisterDate,
                        RequesterName= registeredOrganization.UserRegisterEntry.Firstname,
                        RequesterLastname = registeredOrganization.UserRegisterEntry.Lastname,
                        Road = registeredOrganization.Road,
                        Soi = registeredOrganization.Soi,
                        SubDistrictID = registeredOrganization.SubDistrictID,
                        Telephone = registeredOrganization.Telephone,
                    };

                    _db.MT_Organization.Add(newOrg);
                    registeredOrganization.UserRegisterEntry.Organization = newOrg;

                    var savedResult = _db.SaveChanges();
                    tran.Commit();
                    if (savedResult >= 1)
                    {
                        result.IsCompleted = true;
                        _mailService.SendUserRegistrationNotify(registeredOrganization.UserRegisterEntry.EntryID);
                       
                       
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }
            return result;
        }

        public ServiceModels.ReturnMessage RemoveRegisteredOrganization(Decimal entryId)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            try
            {
                DBModels.Model.OrganizationRegisterEntry entry = _db.OrganizationRegisterEntries.Where(x => x.EntryID == entryId).FirstOrDefault();
                string rootDestinationFolderPath = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).Select(y => y.ParameterValue).FirstOrDefault();
                DBModels.Model.MT_Attachment idCardAttch;
                DBModels.Model.MT_Attachment empAttch;
                String destinationIdCardFilePath = "";
                String destinationEmpFilePath = "";
                decimal? personalIDCardAttachmentID;
                decimal? employeeIDCardAttachmentID;
                bool isCommit = false;
                if (entry != null)
                {
                    using (var tran = _db.Database.BeginTransaction())
                    {
                        _db.OrganizationRegisterEntries.Remove(entry);

                        DBModels.Model.UserRegisterEntry userEntry = _db.UserRegisterEntries.Where(x => x.EntryID == entry.UserEntryID).FirstOrDefault();


                        if (userEntry != null)
                        {
                            personalIDCardAttachmentID = userEntry.PersonalIDCardAttachmentID;
                            employeeIDCardAttachmentID = userEntry.EmployeeIDCardAttachmentID;
                            _db.UserRegisterEntries.Remove(userEntry);
                            if (personalIDCardAttachmentID.HasValue)
                            {
                                idCardAttch = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)personalIDCardAttachmentID).FirstOrDefault();
                                if (idCardAttch != null)
                                {
                                    destinationIdCardFilePath = Path.Combine(rootDestinationFolderPath, idCardAttch.PathName);
                                    _db.MT_Attachment.Remove(idCardAttch);
                                }
                            }

                            if (employeeIDCardAttachmentID.HasValue)
                            {
                                empAttch = _db.MT_Attachment.Where(x => x.AttachmentID == (decimal)employeeIDCardAttachmentID).FirstOrDefault();
                                if (empAttch != null)
                                {
                                    destinationEmpFilePath = Path.Combine(rootDestinationFolderPath, empAttch.PathName);
                                    _db.MT_Attachment.Remove(empAttch);
                                }
                            }
                            
                        }

                        _db.SaveChanges();
                        isCommit = true;                           
                        tran.Commit();
                    }

                    if(isCommit){
                        if (!String.IsNullOrEmpty(destinationIdCardFilePath))
                        {
                            File.Delete(destinationIdCardFilePath);
                        }
                        if (!String.IsNullOrEmpty(destinationEmpFilePath))
                        {
                            File.Delete(destinationEmpFilePath);
                        }
                    }
                }
                

                result.IsCompleted = true;
                result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Register", ex);
            }

            return result;
        }

        

        private bool CheckDupUsername(String emailUser)
        {
            var date = DateTime.Now.AddDays(-15);

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
    }

}
