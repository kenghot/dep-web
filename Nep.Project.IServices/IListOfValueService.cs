using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IListOfValueService
    {
        ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListAll(String lovGroup);
        ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListActive(String lovGroup, decimal? selectedID);
        ServiceModels.ReturnObject<ServiceModels.ListOfValue> GetListOfValueByCode(String lovGroup, String lovCode);
        ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListBudgetType(decimal provinceID, decimal? selectedID);
    }
}
