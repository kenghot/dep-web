using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nep.Project.Business
{
    public class SecurityService : IServices.ISecurityService
    {
        private readonly DBModels.Model.NepProjectDBEntities _context = null;

        public SecurityService(DBModels.Model.NepProjectDBEntities context)
        {
            _context = context;
        }

        public ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo> UpdateUserAccess(string ticketId)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo>();
            try
            {
                var userAccess = _context.SC_UserAccess.Where(u => u.User.IsDelete == "0").FirstOrDefault(x => x.TicketID == ticketId);
                if (userAccess != null)
                {
                    userAccess.LastAccessTime = DateTime.Now;
                    _context.SaveChanges();


                    var info = _context.SC_User.Select(x => new ServiceModels.Security.SecurityInfo()
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
                        OrganizationName = (x.Organization != null)? x.Organization.OrganizationNameTH : null,                       
                        UserGroupID = x.GroupID,
                        UserGroupCode = (x.Group != null)? x.Group.GroupCode : null,
                        SectionID = x.Province.SectionID,
                        SectionName = x.Province.Section.LOVName
                    }).Single(x => x.UserID == userAccess.UserID);

                    info.IsAdministrator = (info.UserGroupID.HasValue && (info.UserGroupCode == Common.UserGroupCode.ผู้ดูแลระบบ));
                    info.LoginTime = DateTime.Now;
                    #region Check Officer User
                    var centerAbbrConfig = _context.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                    if (centerAbbrConfig != null)
                    {

                        if ((!String.IsNullOrEmpty(info.ProvinceAbbr)) && info.UserGroupID.HasValue && (info.UserGroupCode != Common.UserGroupCode.องค์กรภายนอก))
                        {
                            if (info.ProvinceAbbr == centerAbbrConfig)
                            {
                                info.IsCenterOfficer = true;
                            }
                            else
                            {
                                info.IsProvinceOfficer = true;
                            }
                            
                        }

                        if(info.IsAdministrator){
                            info.IsCenterOfficer = true;
                        }                        
                    }
                    #endregion Check Officer User


                    result.Data = info;
                }
                else
                {
                    result.Data = CreateAnonymousInfo();
                }

                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.Data = CreateAnonymousInfo();
                result.IsCompleted = false;
            }

            return result;
        }

        public static ServiceModels.Security.SecurityInfo CreateAnonymousInfo()
        {
            return new ServiceModels.Security.SecurityInfo()
            {
                TicketID = null,
                UserID = null,
                UserName = "Anonymous",
                Roles = new String[0],
                FirstName = "Anonymous",
                LastName = "User",
                IsAuthenticated = false,
                ProvinceID = null,
                ProvinceName = "-",
                ProvinceAbbr = "-",
                OrganizationID = null,
                OrganizationName = "-"
            };
        }
    }
}
