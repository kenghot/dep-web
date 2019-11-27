using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;
using Nep.Project.Business;
using Nep.Project.Common;
using Nep.Project.DBModels;

namespace Nep.Project.Business
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _user;
        private readonly Business.MailService _mailService;

        public AuthenticationService(DBModels.Model.NepProjectDBEntities db, ServiceModels.Security.SecurityInfo user, Business.MailService mailService)
        {
            _db = db;
            _user = user;
            _mailService = mailService;
        }

        public ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo> Login(String username, String password)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo>();
            try
            {
                if (!_user.IsAuthenticated)
                {
                    var user = (from u in _db.SC_User
                                where u.UserName == username && u.IsDelete == "0"
                                select u).SingleOrDefault();

                    if ((user != null) && (user.IsActive == "1"))
                    {
                        if (user.Password.SequenceEqual(Common.PasswordEncrypter.Encrypt(password, user.Salt)))
                        {
                            result.IsCompleted = true;
                            var userAccess = new Nep.Project.DBModels.Model.SC_UserAccess()
                            {
                                TicketID = Guid.NewGuid().ToString("N"),
                                UserID = user.UserID,
                                LastAccessTime = DateTime.Now
                            };

                            _db.SC_UserAccess.Add(userAccess);
                            _db.SaveChanges();

                            userAccess.User = user;

                            var info = _db.SC_User.Select(x => new ServiceModels.Security.SecurityInfo()
                            {
                                UserID = x.UserID,
                                UserName = x.UserName,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                ProvinceID = x.ProvinceID,
                                ProvinceName = (x.Province != null)? x.Province.ProvinceName : null,
                                ProvinceAbbr = (x.Province != null)? x.Province.ProvinceAbbr : null,
                                TicketID = userAccess.TicketID,
                                IsAuthenticated = true,
                                Roles = x.Group.Function.Select(f => f.FunctionCode),
                                OrganizationID = x.OrganizationID,
                                OrganizationName = x.Organization.OrganizationNameTH,
                                SectionID = x.Province.SectionID,
                                SectionName = x.Province.Section.LOVName
                            }).Single(x => x.UserID == user.UserID);
                            info.LoginTime = DateTime.Now;
                            if (System.Web.HttpContext.Current.Session != null)
                            {
                                System.Web.HttpContext.Current.Session[Constants.SESSION_LOGIN_DATETIME] = DateTime.Now;
                                System.Web.HttpContext.Current.Response.Cookies[Constants.SESSION_LOGIN_DATETIME].Value = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);
                            }
                           
                            result.Data = info;
                        }
                        else
                        {
                            result.IsCompleted = false;
                            result.Message.Add(Nep.Project.Resources.Error.UsernameOrPasswordMismatch);
                        }
                    }
                    else if ((user != null) && (user.IsActive == Common.Constants.BOOLEAN_FALSE))
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.UserInactiveError);
                    }
                    else
                    {
                        result.IsCompleted = false;
                        result.Message.Add(Nep.Project.Resources.Error.UsernameOrPasswordMismatch);
                    }
                }
                else
                {
                    result.IsCompleted = false;
                    result.Message.Add(Nep.Project.Resources.Error.UnexpectedError);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add(Nep.Project.Resources.Error.UnexpectedError);
                result.IsCompleted = false;
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Security", ex);
            }

            return result;
        }

        public ServiceModels.ReturnMessage Logout()
        {
            var result = new ServiceModels.ReturnMessage();
            try
            {
                if (_user.IsAuthenticated)
                {
                    var userAccess = _db.SC_UserAccess.FirstOrDefault(x => x.TicketID == _user.TicketID);
                    if (userAccess != null)
                    {
                        _db.SC_UserAccess.Remove(userAccess);
                        _db.SaveChanges();
                    }
                }

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Security", ex);
            }
            return result;
        }

        private ServiceModels.Security.SecurityInfo TestLogin(String username, String password)
        {
            ServiceModels.Security.SecurityInfo info = null;
            username = username.Trim().ToLower();

            if (username == "center")
            {
                info = new ServiceModels.Security.SecurityInfo()
                {
                    UserID = 2,
                    UserName = username,
                    FirstName = "Center",
                    Roles = new String[] { "company", "province", "center" }
                };
            }
            else if (username == "company")
            {
                info = new ServiceModels.Security.SecurityInfo()
                {
                    UserID = 1,
                    UserName = username,
                    FirstName = "Company",
                    Roles = new String[] { "company" }
                };
            }
            else if (username == "province")
            {
                info = new ServiceModels.Security.SecurityInfo()
                {
                    UserID = 3,
                    UserName = username,
                    FirstName = "Province",
                    Roles = new String[] { "company", "province" }
                };
            }
            else if (username == "admin")
            {
                info = new ServiceModels.Security.SecurityInfo()
                {
                    UserID = 3,
                    UserName = username,
                    FirstName = "Admin",
                    Roles = new String[] { "admin", "center" }
                };
            }

            return info;
        }

        //public ServiceModels.ReturnMessage ChangePassword(Int32 UserID, String OldPassword, String NewPassword)
        //{
        //    ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
        //    try
        //    {
        //        model.UserID = 32;//wait for function GetUserLogin
        //        var obj = _db.Users.Where(x => x.UserID == model.UserID).SingleOrDefault();
        //        if (VerifyCurrentPassword(obj, model))
        //        {
        //            obj.UpdatedBy = "System";//Fix for test
        //            obj.UpdatedDate = DateTime.Now;
        //            //obj.Password = Common.PasswordEncrypter.Encrypt(model.NewPassword, obj.Salt);
        //            _db.SaveChanges();

        //            result.IsCompleted = true;
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Message.ChangePasswordSuccess);
        //        }
        //        else
        //        {
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Error.CurrentPassword);
        //            result.IsCompleted = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message.Add(ex.Message);
        //        result.IsCompleted = false;
        //    }
        //    return result;
        //}

        //public ServiceModels.ReturnMessage ResetPassword(String UserName, Decimal OrganizationID)
        //{
        //    ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
        //    try
        //    {
        //        model.UserID = 32;//wait for function GetUserLogin
        //        var obj = _db.Users.Where(x => x.UserID == model.UserID).SingleOrDefault();
        //        if (VerifyCurrentPassword(obj, model))
        //        {
        //            obj.UpdatedBy = "System";//Fix for test
        //            obj.UpdatedDate = DateTime.Now;
        //            //obj.Password = Common.PasswordEncrypter.Encrypt(model.NewPassword, obj.Salt);
        //            _db.SaveChanges();

        //            result.IsCompleted = true;
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Message.ChangePasswordSuccess);
        //        }
        //        else
        //        {
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Error.CurrentPassword);
        //            result.IsCompleted = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message.Add(ex.Message);
        //        result.IsCompleted = false;
        //    }
        //    return result;
        //}

        //public ServiceModels.ReturnMessage ResetPasswordByAdmin(Decimal UserID)
        //{
        //    ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
        //    try
        //    {
        //        model.UserID = 32;//wait for function GetUserLogin
        //        var obj = _db.Users.Where(x => x.UserID == model.UserID).SingleOrDefault();
        //        if (VerifyCurrentPassword(obj, model))
        //        {
        //            obj.UpdatedBy = "System";//Fix for test
        //            obj.UpdatedDate = DateTime.Now;
        //            //obj.Password = Common.PasswordEncrypter.Encrypt(model.NewPassword, obj.Salt);
        //            _db.SaveChanges();

        //            result.IsCompleted = true;
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Message.ChangePasswordSuccess);
        //        }
        //        else
        //        {
        //            result.Message.Add(C2X.DrLife.FrontOffice.Resources.Error.CurrentPassword);
        //            result.IsCompleted = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message.Add(ex.Message);
        //        result.IsCompleted = false;
        //    }
        //    return result;
        //}

        public ServiceModels.ReturnMessage SubmitForgetPasswordRequest(String username)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();

            var user = (from u in _db.SC_User
                        where u.UserName == username
                            && u.IsActive == Common.Constants.BOOLEAN_TRUE
                            && u.IsDelete == Common.Constants.BOOLEAN_FALSE
                       select u).FirstOrDefault();

            if (user != null)
            {
                user.ForgetPasswordRequestDate = DateTime.Now;
                user.ForgetPasswordToken = Common.PasswordEncrypter.GenerateActivationCode();
                _db.SaveChanges();

                _mailService.SendForgetPasswordConfirmation(user.UserID);
                result.IsCompleted = true;
            }
            else
            {
                result.Message.Add("ไม่พบชื่อผู้ใช้ในองค์กร");
            }

            return result;
        }


        public ServiceModels.ReturnObject<ServiceModels.Security.ForgetPasswordInfo> GetForgetPasswordInfo(String username, String token)
        {
            ServiceModels.ReturnObject<ServiceModels.Security.ForgetPasswordInfo> result = new ReturnObject<ServiceModels.Security.ForgetPasswordInfo>();
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(token))
            {
                result.Message.Add(Resources.Message.NoRecord);
            }
            else
            {
                var validDate = DateTime.Today.AddDays(-1);
                var user = (from u in _db.SC_User
                            where u.UserName == username
                                 && u.ForgetPasswordToken == token
                                 && u.ForgetPasswordRequestDate >= validDate
                            select new ServiceModels.Security.ForgetPasswordInfo
                            {
                                Email = u.Email,
                                UserName = u.UserName,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                OrganizationName = (u.Organization != null)? u.Organization.OrganizationNameTH : null,
                                Token = u.ForgetPasswordToken
                            }).FirstOrDefault();

                if (user != null)
                {
                    result.Data = user;
                    result.IsCompleted = true;
                }
                else
                {
                    result.Message.Add(Resources.Message.NoRecord);
                }
            }

            return result;
        }

        public ServiceModels.ReturnMessage ConfirmForgetPassword(ServiceModels.Security.ConfirmForgetPassword model)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();
            if (model == null || String.IsNullOrWhiteSpace(model.Token) || String.IsNullOrWhiteSpace(model.UserName))
            {
                result.Message.Add(Resources.Message.NoRecord);
            }

            var validDate = DateTime.Today.AddDays(-1);
            var user = (from u in _db.SC_User
                        where u.UserName == model.UserName
                             && u.ForgetPasswordToken == model.Token
                        select u).FirstOrDefault();

            if (user != null)
            {
                if (user.ForgetPasswordRequestDate >= validDate)
                {
                    var salt = Common.PasswordEncrypter.GenerateSalt();
                    var password = Common.PasswordEncrypter.Encrypt(model.Password, salt);

                    user.Salt = salt;
                    user.Password = password;
                    user.ForgetPasswordToken = null;
                    user.ForgetPasswordRequestDate = null;

                    _db.SaveChanges();
                    result.IsCompleted = true;
                }
                else
                {
                    result.Message.Add("ลิงค์ยืนยันในอีเมลหมดอายุแล้ว กรุณาเริ่มต้นใหม่");
                }
            }
            else
            {
                result.Message.Add(Resources.Message.NoRecord);
            }

            return result;
        }

        public ServiceModels.ReturnMessage ChangePassword(String oldPassword, String newPassword)
        {
            ServiceModels.ReturnMessage result = new ReturnMessage();

            var user = (from u in _db.SC_User
                        where u.UserID == _user.UserID
                        select u).FirstOrDefault();

            if (user != null)
            {
                var oldHashedPassword = Common.PasswordEncrypter.Encrypt(oldPassword, user.Salt);
                if (oldHashedPassword.SequenceEqual(user.Password))
                {
                    var salt = Common.PasswordEncrypter.GenerateSalt();
                    var newHashedPassword = Common.PasswordEncrypter.Encrypt(newPassword, salt);

                    user.Salt = salt;
                    user.Password = newHashedPassword;
                    user.ForgetPasswordToken = null;
                    user.ForgetPasswordRequestDate = null;

                    _db.SaveChanges();
                    result.IsCompleted = true;
                }
                else
                {
                    result.Message.Add(Resources.Error.InvalidOldPassword);
                }
            }
            else
            {
                result.Message.Add(Resources.Message.NoRecord);
            }

            return result;
        }
    }
}
