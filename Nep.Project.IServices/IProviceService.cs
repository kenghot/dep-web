using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IProviceService
    {
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListOrgProvince(String filter);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvince(String filter);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListDistrict(Int32? provinceID, String filter);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListSubDistrict(Int32? districtID, String filter);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListOrgProvinceSection(Int32? sectionID);
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListSection(Int32? provinceID);
        ServiceModels.ReturnObject<Int32> GetCenterProvinceID();
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvinceByID(decimal? filterID);
    }
}
