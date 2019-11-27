using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IUserProfileService
    {
        //IQueryable<ServiceModels.UserList> List(string name, Common.UserRole? role);
        
        ServiceModels.ReturnObject<ServiceModels.UserProfile> GetUserProfile(decimal userID);
        ServiceModels.ReturnObject<ServiceModels.UserProfile> GetUserRequest(decimal userID);
        ServiceModels.ReturnObject<ServiceModels.UserProfile> CreateInternalUser(ServiceModels.UserProfile userProfile);

        ServiceModels.ReturnMessage UpdateInternalUser(ServiceModels.UserProfile userProfile);

        ServiceModels.ReturnQueryData<ServiceModels.UserList> ListWithCriteria(ServiceModels.QueryParameter p);
        ServiceModels.ReturnQueryData<ServiceModels.UserList> ListNewRequestWithCriteria(ServiceModels.QueryParameter p);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListRole();

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListInternalRole();

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvinceOrganization(decimal provinceID);

        ServiceModels.ReturnMessage UpdateExternalUser(ServiceModels.UserProfile userProfile);
        ServiceModels.ReturnMessage UpdateRequestUser(ServiceModels.UserProfile userProfile);
        ServiceModels.ReturnMessage DeleteUser(decimal userID);
        ServiceModels.ReturnMessage DeleteRequestUser(decimal userID);
        ServiceModels.ReturnObject<Int32> GetUserAdministratorRoleID();

        ServiceModels.ReturnObject<Int32> GetUserProvicnceRoleID();
    }
}
