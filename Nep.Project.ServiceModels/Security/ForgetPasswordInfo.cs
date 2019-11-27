using System;

namespace Nep.Project.ServiceModels.Security
{
    public class ForgetPasswordInfo
    {
        public String UserName { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
        public String OrganizationName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
