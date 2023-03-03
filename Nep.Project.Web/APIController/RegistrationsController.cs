using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/Registrations")]
    public class RegistrationsController : ApiController
    {
        private IServices.IProjectInfoService projService { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        private IServices.IRegisterService regService { get; set; }
        private SecurityInfo userInfo { get; set; }
        public RegistrationsController(IServices.IProjectInfoService ps, SecurityInfo UserInfo, IServices.IAttachmentService attach, IServices.IRegisterService RegService)
        {
            projService = ps;
            userInfo = UserInfo;
            attachService = attach;
            regService = RegService;
        }
        /// <summary>
        /// เพิ่ม องค์กร
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("Organizations")]
        [HttpPost]
        public ReturnMessage RegistOrganization([FromBody] OrganizationRegisterEntryAPI p)
        {
            var result = new ReturnMessage();
            result.IsCompleted = false;
            try
            {
                if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์เข้าถึงข้อมูล");
                }
                var regJson = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                var reg = Newtonsoft.Json.JsonConvert.DeserializeObject<OrganizationRegisterEntry>(regJson);
                result = regService.CreateOrganizationRegisterEntry(reg, null, null);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        /// <summary>
        /// สมัคร
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("Users")]
        [HttpPost]
        public ReturnMessage RegistUsers([FromBody] RegisterEntryAPI p)
        {
            var result = new ReturnMessage();
            result.IsCompleted = false;
            try
            {
                if (!userInfo.IsAuthenticated)
                {
                    result.Message.Add("ไม่มีสิทธิ์เข้าถึงข้อมูล");
                }
                var regJson = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                var reg = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterEntry>(regJson);
                result = regService.CreateRegisterEntry(reg, null, null);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
    }
}
