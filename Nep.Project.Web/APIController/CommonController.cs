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
using GenericDropDownListData = Nep.Project.ServiceModels.API.Responses.GenericDropDownListData;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        public IServices.IAuthenticationService authSerivce { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        public IServices.IProviceService provinceService { get; set; }
        //public IServices.IRegisterService _registerService { get; set; }
        public IServices.IOrganizationService organizationService { get; set; }
        public IServices.IProjectInfoService projService { get; set; }
        public CommonController(
              IServices.IAuthenticationService auth , IServices.IAttachmentService attach,
              IServices.IProjectInfoService proj, IServices.IProviceService province
            )
        {
            authSerivce = auth;
            attachService = attach;
            projService = proj;
            provinceService = province;
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
        /// <summary>
        /// Always insert record
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Romove existing one and then insert the new one
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("UpdateImage")]
        [HttpPost]
        public ReturnObject<UploadImageResponse> UpdateImage([FromBody] UploadImageRequest p)
        {
            var result = new ReturnObject<UploadImageResponse>();
            result.IsCompleted = false;
            try
            {
                var db = projService.GetDB();
                var exImg = db.PROJECTQUESTIONHDs.Where(w => w.PROJECTID == p.DataKey && w.QUESTGROUP == p.GroupCode).FirstOrDefault();
                if (exImg != null)
                {
                    var del = attachService.DeleteImage(exImg.QUESTHDID, exImg.QUESTGROUP);
                }
                var img = attachService.UploadImage(p.GroupCode, p.ImgId, p.DataKey, p.Base64Data);
                if (!img.IsCompleted)
                {
                    result.Message.Add(img.Message[0]);
                    return result;
                }
                result.Data = new UploadImageResponse
                {
                    ImageFullPath = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/UploadImages/{img.Data.DATA}",
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
        [Route("DeleteImage/{imageId}/{groupCode}")]
        [HttpDelete]
        public ReturnObject<string> DeleteImage([FromUri]decimal imageId,[FromUri]string groupCode)
        {
            var result = new ReturnObject<string>();
            result.IsCompleted = false;
            try
            {
                 result = attachService.DeleteImage(imageId, groupCode);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        [Route("ListProvince")]
        [HttpGet]
        public ServiceModels.ReturnQueryData<GenericDropDownListData> ListProvince(String filter = "")
        {
            var result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            result.IsCompleted = false;
            try
            {
                var prov = provinceService.ListProvince(filter);
                if (prov.IsCompleted)
                {
                    result.Data = prov.Data.Select(s => new GenericDropDownListData
                    {
                        OrderNo = s.OrderNo,
                        Text = s.Text,
                        Value = s.Value
                    }).ToList();
                    result.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        [Route("ListDistrict")]
        [HttpGet]
        public ServiceModels.ReturnQueryData<GenericDropDownListData> ListDistrict(String provinceId,String filter = "")
        {
            var result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            result.IsCompleted = false;
            try
            {
                var prov = provinceService.ListDistrict(int.Parse(provinceId), filter);
                if (prov.IsCompleted)
                {
                    result.Data = prov.Data.Select(s => new GenericDropDownListData
                    {
                        OrderNo = s.OrderNo,
                        Text = s.Text,
                        Value = s.Value
                    }).ToList();
                    result.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        [Route("ListSubDistrict")]
        [HttpGet]
        public ServiceModels.ReturnQueryData<GenericDropDownListData> ListSubDistrict(String districtId,  string filter = "")
        {
            var result = new ServiceModels.ReturnQueryData<GenericDropDownListData>();
            result.IsCompleted = false;
            try
            {
                var prov = provinceService.ListSubDistrict(int.Parse(districtId), filter);
                if (prov.IsCompleted)
                {
                    result.Data = prov.Data.Select(s => new GenericDropDownListData
                    {
                        OrderNo = s.OrderNo,
                        Text = s.Text,
                        Value = s.Value
                    }).ToList();
                    result.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
    }
}
