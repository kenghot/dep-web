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
    [RoutePrefix("api/Systems")]
    public class SystemsController : ApiController
    {
        public IServices.IAuthenticationService authSerivce { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        public IServices.IProviceService provinceService { get; set; }
        //public IServices.IRegisterService _registerService { get; set; }
        public IServices.IOrganizationService organizationService { get; set; }
        public IServices.IProjectInfoService projService { get; set; }
        private SecurityInfo userInfo;
        public SystemsController( IServices.IAuthenticationService auth, IServices.IAttachmentService attach, IServices.IProjectInfoService proj, SecurityInfo UserInfo)
        {
            authSerivce = auth;
            attachService = attach;
            projService = proj;
            userInfo = UserInfo;
        }
        [Route("EditContractNo/{projId}")]
        public ReturnObject<string> EditContractNo([FromUri]decimal projId)
        {
            var result = new ReturnObject<string>();
            result.IsCompleted = false;
            try
            {
                if (userInfo.UserGroupCode != "41")
                {
                    result.Message.Add("ขั้นตอนนี้สำหรับผู้ดูแลระบบเท่านั้น");
                    return result;
                }
                var db = projService.GetDB();
                var pass = db.PROJECTQUESTIONHDs.Where(w => w.QUESTGROUP == "CONTRACTPWD" && w.PROJECTID == userInfo.UserID).Select(s => s.DATA).FirstOrDefault();
                if (string.IsNullOrEmpty(pass))
                {
                    result.Message.Add("ยังไม่มีการตั้งรหัสสำหรับแก้ไขเลขที่สัญญา");
                    return result;
                }
                var contract = db.ProjectContracts.Where(w => w.ProjectID == projId).FirstOrDefault();
            }catch (Exception ex)
            {
                result.Message.Add(ex.Message);
            }
            return result;
        }
     
    }
}
