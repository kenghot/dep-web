using Nep.Project.DBModels.Model;
using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Requests;
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
        #region contract
        [Route("EditContractNo/{projId}")]
        [HttpGet]
        public ReturnObject<string> EditContractNo([FromUri]decimal projId)
        {
            var result = new ReturnObject<string>();
            result.IsCompleted = false;
            try
            {
                if (userInfo.UserGroupCode != "G6")
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
                var proj = db.ProjectInformations.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add("ไม่พบโครงการที่ระบุ");
                    return result;
                }
                if (proj.BudgetYear >= DateTime.Now.Year)
                {
                    result.Message.Add("อนุญาตให้แก้ไขได้เฉพาะปีงบประมาณก่อนหน้าเท่านั้น");
                    return result;
                }
                var contract = db.ProjectContracts.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add("ไม่พบสัญญาโครงการที่ระบุ");
                    return result;
                }
                result.Data = contract.ContractNo;
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        [Route("SaveContractNo/{projId}")]
        [HttpPost]
        public ReturnObject<string> SaveContractNo([FromUri] decimal projId,[FromBody] SaveContractNoRequest p)
        {
            var result = new ReturnObject<string>();
            result.IsCompleted = false;
            try
            {
                if (userInfo.UserGroupCode != "G6")
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
                if ( pass != p.Password)
                {
                    result.Message.Add("รหัสไม่ถูกต้อง");
                    return result;
                }
                var proj = db.ProjectInformations.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add("ไม่พบโครงการที่ระบุ");
                    return result;
                }
                if (proj.BudgetYear >= DateTime.Now.Year)
                {
                    result.Message.Add("อนุญาตให้แก้ไขได้เฉพาะปีงบประมาณก่อนหน้าเท่านั้น");
                    return result;
                }
                var contract = db.ProjectContracts.Where(w => w.ProjectID == projId).FirstOrDefault();
                if (proj == null)
                {
                    result.Message.Add("ไม่พบสัญญาโครงการที่ระบุ");
                    return result;
                }
                contract.ContractNo = p.ContractNo;

                db.SaveChanges();
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
        #endregion
        #region Document
        /// <summary>
        /// Always Insert (project id may be duplicated)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("InsertDocument")]
        [HttpPost]
        public ReturnObject<decimal?> InsertDocument([FromBody]SaveDocRequest p)
        {
            var result = new ReturnObject<decimal?>();
            result.IsCompleted = false;
            try
            {
                result = projService.InsertDocument(p.KeyId, p.DocGroup, p.Data);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;

        }
        /// <summary>
        /// Insert when not exist, update when existed
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("SaveDocument")]
        [HttpPost]
        public ReturnObject<decimal?> SaveDocument([FromBody] SaveDocRequest p)
        {
            var result = new ReturnObject<decimal?>();
            result.IsCompleted = false;
            try
            {
                result = projService.SaveDocument(p.KeyId, p.DocGroup, p.Data);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;

        }
        [Route("GetDocumentByDocId/{id}/{docGroup}")]
        [HttpGet]
        public ReturnObject<PROJECTQUESTIONHD> GetDocumentByDocId([FromUri]decimal id,[FromUri]string docGroup)
        {
            var result = new ReturnObject<PROJECTQUESTIONHD>();
            result.IsCompleted = false;
            try
            {
                result = projService.GetDocumentByDocId(id , docGroup);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;

        }
        [Route("GetDocumentByKey/{id}/{docGroup}")]
        [HttpGet]
        public ReturnObject<PROJECTQUESTIONHD> GetDocumentByKey([FromUri] decimal id, [FromUri] string docGroup)
        {
            var result = new ReturnObject<PROJECTQUESTIONHD>();
            result.IsCompleted = false;
            try
            {
                result = projService.GetDocumentByKey(id, docGroup);
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;

        }
        #endregion
    }
}
