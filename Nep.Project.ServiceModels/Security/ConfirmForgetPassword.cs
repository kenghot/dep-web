using System;

namespace Nep.Project.ServiceModels.Security
{

    public class ConfirmForgetPassword
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public String Token { get; set; }
    }
}
