using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IOrganizationService
    {
        ServiceModels.ReturnQueryData<ServiceModels.OrganizationList> List(ServiceModels.QueryParameter param);
        ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Get(Decimal id);
        ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Create(ServiceModels.OrganizationProfile model);
        ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Update(ServiceModels.OrganizationProfile model);
        ServiceModels.ReturnMessage Remove(decimal organizationID);

        ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> ListDropDown(ServiceModels.QueryParameter param);

        ServiceModels.ReturnQueryData<Int32> ListValueMapping(List<decimal> orgIds, decimal? provinceID);
        //kenghot
        ServiceModels.ReturnObject<bool> IsBlackList(decimal? orgId , decimal? provinceId);
        //beer
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListBank();
    }
}
