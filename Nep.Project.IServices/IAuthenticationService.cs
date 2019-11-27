using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IAuthenticationService
    {
        ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo> Login(String username, String password);
        ServiceModels.ReturnMessage Logout();
        ServiceModels.ReturnMessage SubmitForgetPasswordRequest(String username);
        ServiceModels.ReturnObject<ServiceModels.Security.ForgetPasswordInfo> GetForgetPasswordInfo(String username, String token);
        ServiceModels.ReturnMessage ConfirmForgetPassword(ServiceModels.Security.ConfirmForgetPassword model);
        ServiceModels.ReturnMessage ChangePassword(String oldPassword, String newPassword);
    }
}
