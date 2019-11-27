using Nep.Project.DBModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class OrganizationParameterService : IServices.IOrganizationParameterService
    {
        private readonly NepProjectDBEntities _db;
        public OrganizationParameterService(NepProjectDBEntities db)
        {
            _db = db;        
        }
        public ServiceModels.ReturnObject<ServiceModels.OrganizationParameter> GetOrganizationParameter()
        {
            ServiceModels.ReturnObject<ServiceModels.OrganizationParameter> result = new ServiceModels.ReturnObject<ServiceModels.OrganizationParameter>();
            try
            {
                var data = new ServiceModels.OrganizationParameter();
                var parameters = _db.MT_OrganizationParameter.Where(x => x.IsActive == "1").ToDictionary(x => x.ParameterCode, x => x.ParameterValue);
                data.AttachFilePath = parameters[Common.OrganizationParameterCode.AttachFilePath];
                //data.BangkokProvinceID = Convert.ToInt32(parameters[Common.OrganizationParameterCode.BANGKOK_PROVINCE_ID]);
                data.PrefixDocNo0702 = parameters[Common.OrganizationParameterCode.PREFIX_DOC_NO_0702];
                data.NepAddress = parameters[Common.OrganizationParameterCode.NEP_ADDRESS];
                data.FollowupContact = parameters[Common.OrganizationParameterCode.FOLLOWUP_CONTACT];
                data.NepProjectDirectorName = parameters[Common.OrganizationParameterCode.NEP_PROJECT_DIRECTOR];
                data.NepProjectDirectorPosition = parameters[Common.OrganizationParameterCode.NEP_PROJECT_DIRECTOR_POSITION];
                data.BangkokProvinceAbbr = parameters[Common.OrganizationParameterCode.BANGKOK_PROVINCE_ABBR];
                data.CenterProvinceAbbr = parameters[Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR];
                result.IsCompleted = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Organization Parameter Service", ex);
            }

            
            return result;            
        }
    }
}
