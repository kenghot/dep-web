using Nep.Project.ServiceModels;
using Nep.Project.ServiceModels.API.Responses;
using Nep.Project.ServiceModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nep.Project.Web.APIController
{
    [RoutePrefix("api/dashboard")]
    public class DashBoardController : ApiController
    {
        private IServices.IProjectInfoService projService { get; set; }
        private IServices.IAttachmentService attachService { get; set; }
        private SecurityInfo userInfo { get; set; }
        public DashBoardController(IServices.IProjectInfoService ps, SecurityInfo UserInfo, IServices.IAttachmentService attach)
        {
            projService = ps;
            userInfo = UserInfo;
            attachService = attach;
        }
        [Route("Get/{year}")]
        [HttpGet]
        public ReturnObject<DashBoardResponse> Get([FromUri] int year)
        {
            var result = new ReturnObject<DashBoardResponse>();
            result.IsCompleted = false;
            try
            {
                result.Data = new DashBoardResponse();
                var pjtypeAmt = new ChartData();
                result.Data.projectTypeAmount = pjtypeAmt;
                pjtypeAmt.datasets = new List<ChartDataSet>
                {
                    new ChartDataSet
                    {
                        backgroundColor = new List<string>() { "red", "blue", "green", "yellow" },
                        data = new List<decimal>() { 10, 50, 35, 24 },
                        label = "test"
                    }

                };
                pjtypeAmt.labels = new List<string>() { "ทดสอบ1", "ทดสอบ2", "ทดสอบ3", "ทดสอบ4" };
                result.IsCompleted = true;
            }
            catch (Exception ex)
            {
                result.SetExceptionMessage(ex);
            }
            return result;
        }
    }
}
