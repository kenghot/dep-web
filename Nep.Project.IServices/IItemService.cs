using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IItemService
    {
        ServiceModels.ReturnQueryData<ServiceModels.ItemList> ListStrategic(ServiceModels.QueryParameter p);
        ServiceModels.ReturnObject<ServiceModels.Item> GetListItem(String lovGroup, String lovCode);
    }
}
