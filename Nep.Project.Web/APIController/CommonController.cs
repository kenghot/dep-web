using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nep.Project.ServiceModels.API.Requests;
using Nep.Project.ServiceModels.API.Responses;

namespace Nep.Project.Web.APIController
{

    public class CommonController : ApiController
    {
        public IServices.IAuthenticationService authSerivce { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        public IServices.IProviceService provinceService { get; set; }
        //public IServices.IRegisterService _registerService { get; set; }
        public IServices.IOrganizationService organizationService { get; set; }
        public IServices.IProjectInfoService projService { get; set; }
        public CommonController(
              IServices.IAuthenticationService auth , IServices.IAttachmentService attach
            )
        {
            authSerivce = auth;
            attachService = attach;
        }
        [Route("Login")]
        [HttpPost]
        public ReturnObject<SecurityInfo> Login([FromBody]LoginRequest p)
        {
            var result = new ReturnObject<SecurityInfo>();
            result.IsCompleted = false;
            try
            {
                result = authSerivce.Login(p.UserCode, p.Password);

            }catch(Exception ex)
            {
                result.Message.Add(ex.Message);
            }
            return result;
        }
        [Route("UploadImage")]
        [HttpPost]
        public ReturnObject<UploadImageResponse> UploadImage([FromBody]UploadImageRequest p)
        {
            var result = new ReturnObject<UploadImageResponse>();
            result.IsCompleted = false;
            try
            {
                var img = attachService.UploadImage(p.GroupCode, p.ImgId, p.DataKey, p.Base64Data);
                if (!img.IsCompleted)
                {
                    result.Message.Add(img.Message[0]);
                    return result;
                }
                result.Data = new UploadImageResponse
                {
                    ImageFullPath = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/UploadImages/{img.Data.DATA}" ,
                    ImageId = img.Data.QUESTHDID,
                    ImageName = img.Data.DATA

                };
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
            }
            return result;
        }
    }
}
