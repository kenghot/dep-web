using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IReport4Service
    {
        ServiceModels.ReturnQueryData<ServiceModels.Report.Report4> ListReport4(ServiceModels.QueryParameter p);
    }
}
