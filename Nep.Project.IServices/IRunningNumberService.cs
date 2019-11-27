using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IRunningNumberService
    {
        ServiceModels.ReturnObject<String> GetProjectContractNo(Decimal projectID, int contractYear);
        ServiceModels.ReturnObject<String> GetProjectProjectNo(Decimal projectID, DateTime approvalDate, int committeeNo, int committeeYear);
    }
}
