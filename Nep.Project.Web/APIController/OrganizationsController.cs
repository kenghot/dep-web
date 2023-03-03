using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/Organizations")]
    public class OrganizationsController : ApiController
    {
        private IServices.IProjectInfoService projService { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        private IServices.IOrganizationService orgService { get; set; }
        private SecurityInfo userInfo { get; set; }
        public OrganizationsController(IServices.IProjectInfoService ps, SecurityInfo UserInfo, IServices.IAttachmentService attach, IServices.IOrganizationService ORGService)
        {
            projService = ps;
            userInfo = UserInfo;
            attachService = attach;
            orgService = ORGService;
        }
        /// <summary>
        /// ค้นหา องค์กร
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("Search")]
        [HttpPost]
        public ServiceModels.ReturnQueryData<ServiceModels.OrganizationListAPI> List(ServiceModels.QueryParameter param)
        {
            //(PROVINCEID ==@0)
            var orgs = orgService.List(param);
            var orgsjson = Newtonsoft.Json.JsonConvert.SerializeObject(orgs);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceModels.ReturnQueryData<ServiceModels.OrganizationListAPI>>(orgsjson);

        }
    }
}
