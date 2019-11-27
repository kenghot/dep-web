using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IRegisterService
    {
        // @todoist
        ServiceModels.ReturnMessage CreateRegisterEntry(ServiceModels.RegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage);
       // ServiceModels.ReturnMessage UpdateRegisterEntry(ServiceModels.RegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage);

        ServiceModels.ReturnObject<ServiceModels.RegisterEntry> GetRegistryUser(int id, String activationCode);

        ServiceModels.ReturnMessage CreateExternalUser(ServiceModels.ConfirmEmail confirmEmail);

        IQueryable<ServiceModels.DecimalDropDownListData> ListOrganization();

        ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> ListOrganizationRegister(ServiceModels.QueryParameter param);

        IQueryable<ServiceModels.DecimalDropDownListData> ListProvince();

        ServiceModels.ReturnMessage CreateOrganizationRegisterEntry(ServiceModels.OrganizationRegisterEntry registerEntry, ServiceModels.KendoAttachment personalIdCardImage, ServiceModels.KendoAttachment staffCardImage);

        ServiceModels.ReturnQueryData<ServiceModels.RegisteredOrganizationList> ListRegisteredOrganization(ServiceModels.QueryParameter param);
        ServiceModels.ReturnQueryData<ServiceModels.RegisteredOrganizationList> ListRegisteredOrganization(ServiceModels.QueryParameter param,Boolean isApprove);
        ServiceModels.ReturnObject<ServiceModels.OrganizationRegisterEntry> GetRegisteredOrganization(Decimal entryId);

        ServiceModels.ReturnMessage ApproveRegisteredOrganization(Decimal entryId);

        ServiceModels.ReturnMessage RemoveRegisteredOrganization(Decimal entryId);

        ServiceModels.ReturnMessage CreatePasswordInternalUser(ServiceModels.ConfirmEmail confirmEmail);

        ServiceModels.ReturnQueryData<Int32> ListRegisteredOrganizationMapping(List<decimal> orgIds);
        ServiceModels.ProjectInfo.DashBoardORG GetORGDashBoardData(decimal provinceId, int year);
    };
}
