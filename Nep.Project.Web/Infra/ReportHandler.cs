using Autofac.Integration.Web.Forms;
using Nep.Project.Common.Web;
using Nep.Project.ServiceModels.ProjectInfo;
using Nep.Project.ServiceModels.Report;
using Nep.Project.ServiceModels.Report.ReportProjectRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Nep.Project.Web.Infra
{
    [InjectProperties]
    public class ReportHandler : IHttpHandler
    {
        public IServices.IProviceService ProvinceService { get; set; }
        public IServices.IRegisterService RegisterService { get; set; }
        public IServices.IOrganizationService OrganizationService { get; set; }
        public IServices.IProjectInfoService projService { get; set; }
        public IServices.IAuthenticationService _authSerive { get; set; }

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }
        public class InputData
        {
            public List<string> Controls { get; set; }
            public string QNGroup { get; set; }
            public decimal ProjID { get; set; }
            public object QNData { get; set; }
            public string IsReported { get; set; }
            public string ExtendData { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var action = Path.GetFileName(context.Request.FilePath).ToLower();
            var httpMethod = context.Request.HttpMethod.ToLower();


            //if (action.Equals("reportprojectrequest")) // return object
            //{
            //    GetReportProjectRequest(context);
            //}

            if (action.Equals("reportprojectcontract")) // return object
            {
                GetReportProjectContract(context);
            }
            //if (action.Equals("reportprojectresult")) // return object
            //{
            //    GetReportProjectResult(context);
            //}
        }

        //private void GetReportProjectRequest(HttpContext context)
        //{

        //    ServiceModels.ReturnObject<ProjectRequestReport> rep = new ServiceModels.ReturnObject<ProjectRequestReport>();
        //    rep.IsCompleted = false;

        //    try
        //    {
        //        String strValues = ""; //= context.Request.Form[0];
        //        using (StreamReader reader = new StreamReader(context.Request.InputStream))
        //        {
        //            strValues = reader.ReadToEnd();
        //        }
        //        var projectID = decimal.Parse(context.Request.QueryString["projectId"].ToString());


        //        rep.Data = new ProjectRequestReport();
        //        var genInfoResult = projService.GetReportProjectRequest(projectID);
        //        if (genInfoResult.IsCompleted)
        //        {
        //            rep.Data.Info = genInfoResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.AddRange(genInfoResult.Message);
        //            return;
        //        }

        //        var orgAssResult = projService.GetListOrganizationAssistance(projectID);
        //        if (orgAssResult.IsCompleted)
        //        {
        //            rep.Data.OrgAssistances = orgAssResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลแหล่งความช่วยเหลือที่องค์กรได้รับในปัจจุบัน : " + orgAssResult.Message[0]);
        //            return;
        //        }
        //        var attListResult = projService.GetListProjectAttachment(projectID);
        //        if (attListResult.IsCompleted)
        //        {
        //            rep.Data.Files = attListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลเอกสารแนบ : " + attListResult.Message[0]);
        //            return;
        //        }

        //        var budgetListResult = projService.GetListProjectBudget(projectID);
        //        if (budgetListResult.IsCompleted)
        //        {
        //            rep.Data.Budgets = budgetListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลงบประมาณ : " + budgetListResult.Message[0]);
        //            return;
        //        }
        //        var commListResult = projService.GetListProjectCommitteet(projectID);
        //        if (commListResult.IsCompleted)
        //        {
        //            rep.Data.Commitees = commListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลรายชื่อคณะกรรมการ : " + commListResult.Message[0]);
        //            return;
        //        }

        //        var targetListResult = projService.GetListProjectTargetGroup(projectID);
        //        if (targetListResult.IsCompleted)
        //        {
        //            rep.Data.Targets = targetListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลกลุ่มเป้าหมายโครงการ : " + targetListResult.Message[0]);
        //            return;
        //        }

        //        var operationAddressListResult = projService.GetListProjectOperationAddres(projectID);
        //        if (operationAddressListResult.IsCompleted)
        //        {
        //            rep.Data.OperationAddresses = operationAddressListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.Add("ข้อมูลสถานที่ดำเนินโครงการ : " + operationAddressListResult.Message[0]);
        //            return;
        //        }
        //        var acts = projService.GetDB().PROJECTBUDGETACTIVITies.Where(w => w.PROJECTID == projectID).Select(s => s.ACTIVITYNAME).ToList();
        //        rep.Data.Activities = acts;
        //        rep.IsCompleted = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        rep.Message.Add(ex.Message);

        //    }
        //    finally
        //    {
        //        context.Response.ContentType = "application/json; charset=utf-8";
        //        String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(rep);

        //        context.Response.Write(responseText);
        //        //context.Response.Write(json);
        //        context.Response.End();
        //    }
        //}
        private String CreateSupportGivenDesc(ServiceModels.Report.ReportFormatContract contract)
        {
            StringBuilder desc = new StringBuilder();
            if (!String.IsNullOrEmpty(contract.DirectiveNo) && (!String.IsNullOrEmpty(contract.DirectProvinceNo)))
            {
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่ง กรมส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการที่ <u>{0}</u> ลงวันที่ <u>{1}</u>", contract.DirectiveNo, contract.DirectiveDate);
                desc.AppendFormat(" และคำสั่งจังหวัด <u>{0}</u> ที่ <u>{1}</u>  ลงวันที่ <u>{2}</u> ", contract.DirectiveProvince, contract.DirectProvinceNo, contract.DirectProvinceDate);

            }
            else if (!String.IsNullOrEmpty(contract.DirectiveNo))
            {
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่ง กรมส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการที่ <u>{0}</u> ลงวันที่ <u>{1}</u> ", contract.DirectiveNo, contract.DirectiveDate);
            }
            else if (!String.IsNullOrEmpty(contract.DirectProvinceNo))
            {
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่งจังหวัด <u>{0}</u> ที่ <u>{1}</u>  ลงวันที่ <u>{2}</u> ", contract.DirectiveProvince, contract.DirectProvinceNo, contract.DirectProvinceDate);
            }


            return WebUtility.ParseToThaiNumber(desc.ToString());
        }
        private void GetReportProjectContract(HttpContext context)
        {

            ServiceModels.ReturnObject<ReportFormatContract> rep = new ServiceModels.ReturnObject<ReportFormatContract>();
            rep.IsCompleted = false;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var projectID = decimal.Parse(context.Request.QueryString["projectId"].ToString());
                rep = projService.GetReportFormatContract(projectID);

                if (rep.IsCompleted)
                {
                  
                    ServiceModels.Report.ReportFormatContract contract = rep.Data;
                    
                    contract.ContractNo = WebUtility.ParseToThaiNumber(contract.ContractNo);
                    contract.ReceiverAddressNo = WebUtility.ParseToThaiNumber(contract.ReceiverAddressNo);
                    contract.ContractDate = WebUtility.ParseToThaiNumber(contract.ContractDate);
                    contract.Amount = WebUtility.ParseToThaiNumber(contract.Amount);
                    contract.ProjectName = WebUtility.ParseToThaiNumber(contract.ProjectName);
                    contract.AttachPage1 = string.IsNullOrEmpty(contract.AttachPage1) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage1)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage1)).Replace("บาทถ้วน", "")}) ";
                    contract.AttachPage2 = string.IsNullOrEmpty(contract.AttachPage2) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage2)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage2)).Replace("บาทถ้วน", "")}) ";
                    contract.AttachPage3 = string.IsNullOrEmpty(contract.AttachPage3) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage3)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage3)).Replace("บาทถ้วน", "")}) ";
                    contract.MeetingText = $" {contract.MeetingNo} ";
                    var obj = projService.GetProjectApprovalResult(projectID);
                    if (obj.IsCompleted)
                    {
                        contract.MeetingText = $"<u>{obj.Data.ApprovalNo} / " +
                        $"{int.Parse(obj.Data.ApprovalYear) + 543}</u> เมื่อวันที่ <u>{Common.Web.WebUtility.ToBuddhaDateFormat(obj.Data.ApprovalDate, "d MMMM yyyy")}</u>";
                        contract.MeetingText = WebUtility.ParseToThaiNumber(contract.MeetingText);

                        if (contract.AuthorizeFlag)
                        {
                            contract.MsgAuthorizeFlag = string.Format("ผู้มีอำนาจลงนามผูกพันปรากฏตามหนังสือมอบอำนาจ ฉบับลงวันที่ {0}", contract.AttorneyDate);
                        }




                        if (!contract.IsCenterContract)
                        {
                            contract.SupportGivenDesc = CreateSupportGivenDesc(contract);
                        }


                    }
                    else
                    {
                        rep.Message.AddRange(rep.Message);
                        return;
                    }


                }
                else
                {
                    rep.Message.AddRange(rep.Message);
                    return;
                }
                var genInfoResult = projService.GetReportProjectRequest(projectID);
                if (genInfoResult.IsCompleted)
                {
                    rep.Data.Info = genInfoResult.Data;

                }
                else
                {
                    rep.Message.AddRange(genInfoResult.Message);
                    return;
                }
                rep.IsCompleted = true;
            }
            catch (Exception ex)
            {
                rep.Message.Add(ex.Message);

            }
            finally
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(rep);

                context.Response.Write(responseText);
                //context.Response.Write(json);
                context.Response.End();
            }
        }

        //private void GetReportProjectResult(HttpContext context)
        //{

        //    ServiceModels.ReturnObject<ProjectResultReport> rep = new ServiceModels.ReturnObject<ProjectResultReport>();
        //    rep.IsCompleted = false;

        //    try
        //    {
        //        String strValues = ""; //= context.Request.Form[0];
        //        using (StreamReader reader = new StreamReader(context.Request.InputStream))
        //        {
        //            strValues = reader.ReadToEnd();
        //        }
        //        var projectID = decimal.Parse(context.Request.QueryString["projectId"].ToString());
        //        var result = projService.GetProjectReportResult(projectID);
        //        rep.Data = new ProjectResultReport();
        //        if (result.IsCompleted)
        //        {

        //            rep.Data.Result = result.Data;



        //        }
        //        else
        //        {
        //            rep.Message.AddRange(result.Message);
        //            return;
        //        }
        //        var genInfoResult = projService.GetReportProjectRequest(projectID);
        //        if (genInfoResult.IsCompleted)
        //        {
        //            rep.Data.Info = genInfoResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.AddRange(genInfoResult.Message);
        //            return;
        //        }
        //        var budgetListResult = projService.GetListProjectBudget(projectID);
        //        if (budgetListResult.IsCompleted)
        //        {
        //            rep.Data.Budgets = budgetListResult.Data;

        //        }
        //        else
        //        {
        //            rep.Message.AddRange(genInfoResult.Message);
        //            return;
        //        }
        //        rep.IsCompleted = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        rep.Message.Add(ex.Message);

        //    }
        //    finally
        //    {
        //        context.Response.ContentType = "application/json; charset=utf-8";
        //        String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(rep);

        //        context.Response.Write(responseText);
        //        //context.Response.Write(json);
        //        context.Response.End();
        //    }
        //}
    }
}