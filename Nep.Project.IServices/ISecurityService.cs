using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;

namespace Nep.Project.IServices
{
    public interface ISecurityService
    {
        ReturnObject<ServiceModels.Security.SecurityInfo> UpdateUserAccess(string ticketId);
    }
}
