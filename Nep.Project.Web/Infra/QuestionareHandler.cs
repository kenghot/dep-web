using Autofac.Integration.Web.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Nep.Project.Web.Infra
{
    [InjectProperties]
    public class QuestionareHandler : IHttpHandler
    {
        public IServices.IProviceService ProvinceService { get; set; }
        public IServices.IRegisterService RegisterService { get; set; }
        public IServices.IOrganizationService OrganizationService { get; set; }
        public IServices.IProjectInfoService projService { get; set; }
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
        }

        public void ProcessRequest(HttpContext context)
        {
            var action = Path.GetFileName(context.Request.FilePath).ToLower();
            var httpMethod = context.Request.HttpMethod.ToLower();

            if (action.Equals("getquestionare"))
            {
                GetQuestionare(context);
            }
            if (action.Equals("getdata"))
            {
                GetQNData(context);
            }
            if (action.Equals("savedata"))
            {
                SaveQNData(context);
            }
            if (action.Equals("getactivitybudget"))
            {
                GetActivityBudget(context);
            }
            //else if (action.Equals("getdistrict"))
            //{
            //    WriteDistrict(context);
            //}
            //else if (action.Equals("getsubdistrict"))
            //{
            //    WriteSubDistrict(context);
            //}
            //else if (action.Equals("getorg"))
            //{
            //    WriteOrg(context);
            //}
            //else if (action.Equals("orgregister"))
            //{
            //    WriteOrgregister(context);
            //}
            //else if (action.Equals("getsearchorgprovince"))
            //{
            //    WriteSearchOrgProvince(context);
            //}
            //else if (action.Equals("getorgprovince"))
            //{
            //    WriteOrgProvince(context);
            //}
            //else if (action.Equals("orgvaluemapper"))
            //{
            //    OrgValueMapper(context);
            //}
            //else if (action.Equals("orgregistermapper"))
            //{
            //    OrgRegisterValueMapper(context);
            //}
        }
        private void GetActivityBudget(HttpContext context)
        {

            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> act;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                //var st = context.Request.InputStream()
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
                // var controls = data.Controls.Select(x => x).Distinct().ToList();
                act = projService.GetProjectBudgetInfoByProjectID(data.ProjID);
              //  qn = projService.SaveQNData(data.ProjID, data.QNGroup, data.IsReported, data.QNData.ToString());



            }
            catch (Exception ex)
            {
                act = new ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>();
                act.IsCompleted = false;
                act.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(act);

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void GetQNData(HttpContext context)
        {
         
            ServiceModels.ReturnObject<string> qn;
     
            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                //var st = context.Request.InputStream()
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
               // var controls = data.Controls.Select(x => x).Distinct().ToList();

                qn = projService.GetQNData(data.ProjID, data.QNGroup);
              
                context.Response.ContentType = "application/json; charset=utf-8";
         

                context.Response.Write(qn.Data);
                //context.Response.Write(json);
                context.Response.End();

            }
            catch (Exception ex)
            {
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            //context.Response.ContentType = "application/javascript";
            //context.Response.Write(@"{""items"":[{""id"":1,""text"":""test1""},{""id"":2,""text"":""test2""}],  ""id"":1,  ""text"":""text"" }");
        
            //context.Response.End();
        }
        private void SaveQNData(HttpContext context)
        {

            ServiceModels.ReturnObject<string> qn;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                //var st = context.Request.InputStream()
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
                // var controls = data.Controls.Select(x => x).Distinct().ToList();

                qn = projService.SaveQNData(data.ProjID, data.QNGroup, data.IsReported,data.QNData.ToString());
         
          

            }
            catch (Exception ex)
            {
                qn = new ServiceModels.ReturnObject<string>();
                qn.IsCompleted = false;
                qn.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(qn);

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void GetQuestionare(HttpContext context)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();
            ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.Questionare>> qn;
            string retjs = "";
            try
            {
                String strValues = context.Request.Form[0];
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
                var controls = data.Controls.Select(x => x).Distinct().ToList();

                qn = projService.GetProjectQuestionare(data.ProjID, data.QNGroup);
                retjs = Business.QuestionareHelper.ConvertQNToJson(qn.Data,controls);
                context.Response.ContentType = "application/json; charset=utf-8";
                String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(retjs);
               
                context.Response.Write(responseText);
                //context.Response.Write(json);
                context.Response.End();

            }
            catch (Exception ex)
            {
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);
              
            }

        
        }

        private void OrgRegisterValueMapper(HttpContext context)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();

            try
            {
                String strValues = context.Request.Form["values[0]"];
                result.IsCompleted = true;
                result.Data = new List<int>();
                if (!String.IsNullOrEmpty(strValues))
                {
                    List<decimal> values = new List<decimal>();
                    values.Add(Convert.ToDecimal(strValues));
                    result = RegisterService.ListRegisteredOrganizationMapping(values);
                }

            }
            catch (Exception ex)
            {
                result.Data = new List<Int32>();
                result.Message.Add(ex.Message);
            }


            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result.Data);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteSearchOrgProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = ProvinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                list = result.Data;                
                list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
            }
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteOrgProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = ProvinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                list = result.Data;
                list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
            }
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = ProvinceService.ListProvince(null);
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteDistrict(HttpContext context)
        {            
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            var param = context.Request.Params["parentid"];
            if(param != null){
                int id = 0;
                Int32.TryParse(param.ToString(), out id);
                result = ProvinceService.ListDistrict(id, null);
               
            }

            if (result.Data == null)
            {
                result.Data = new List<ServiceModels.GenericDropDownListData>();
            }

            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteSubDistrict(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            var param = context.Request.Params["parentid"];
            if (param != null)
            {
                int id = 0;
                Int32.TryParse(param.ToString(), out id);
                result = ProvinceService.ListSubDistrict(id, null);

               
            }

            if (result.Data == null)
            {
                result.Data = new List<ServiceModels.GenericDropDownListData>();
            }

            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteOrg(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData>();

            try
            {
                String parentId = context.Request.Form["parentId"];
                String strPage = context.Request.Form["page"];
                String strPageSize = context.Request.Form["pageSize"];
                String orgName = context.Request.Form["filter[filters][0][value]"];
                String orgNameField = context.Request.Form["filter[filters][0][field]"];
                Int32 pageIndex = 0;
                Int32 pageSize = 40;
                Int32 tmpPageSize = 40;

                List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
                if (!String.IsNullOrEmpty(parentId))
                {
                    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ParentID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = Convert.ToDecimal(parentId) });
                }
                if (!String.IsNullOrEmpty(orgName))
                {
                    fields.Add(new ServiceModels.FilterDescriptor() { Field = orgNameField, Operator = ServiceModels.FilterOperator.Contains, Value = orgName.Trim() });
                }
                if (!String.IsNullOrEmpty(strPage) && Int32.TryParse(strPage, out pageIndex))
                {                   
                    pageIndex = pageIndex - 1;
                }
                if (!String.IsNullOrEmpty(strPageSize) && Int32.TryParse(strPageSize, out tmpPageSize))
                {
                    pageSize = tmpPageSize;
                }


                List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
                if (fields.Count > 0)
                {
                    filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = fields
                    });
                }

                ServiceModels.QueryParameter param = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filterComposite, pageIndex, pageSize, "Text", System.Web.UI.WebControls.SortDirection.Ascending);

                result = OrganizationService.ListDropDown(param);

                if (result.Data == null)
                {
                    result.Data = new List<ServiceModels.DecimalDropDownListData>();
                }
            }
            catch (Exception ex)
            {
                result.Data = new List<ServiceModels.DecimalDropDownListData>();
                result.Message.Add(ex.Message);
            }


            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        

        private void WriteOrgregister(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData>();

            try
            {
                String strPage = context.Request.Form["page"];
                String strPageSize = context.Request.Form["pageSize"];
                String value = context.Request.Form["filter[filters][0][value]"];
                String field = context.Request.Form["filter[filters][0][field]"];
                Int32 pageIndex = 0;
                Int32 pageSize = 40;
                Int32 tmpPageSize = 40;

                List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
                if(!String.IsNullOrEmpty(value)){
                    fields.Add(new ServiceModels.FilterDescriptor() { Field = field, Operator = ServiceModels.FilterOperator.Contains, Value = value.Trim() });
                }

                List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
                if (fields.Count > 0)
                {
                    filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = fields
                    });
                }

                if (!String.IsNullOrEmpty(strPage) && Int32.TryParse(strPage, out pageIndex))
                {
                    pageIndex = pageIndex - 1;
                }

                if (!String.IsNullOrEmpty(strPageSize) && Int32.TryParse(strPageSize, out tmpPageSize))
                {
                    pageSize = tmpPageSize;
                }

                ServiceModels.QueryParameter param = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filterComposite, pageIndex, pageSize, "Text", System.Web.UI.WebControls.SortDirection.Ascending);

                result = RegisterService.ListOrganizationRegister(param);

                if(result.Data == null){
                    result.Data = new List<ServiceModels.DecimalDropDownListData>();
                }
            }
            catch (Exception ex)
            {
                result.Data = new List<ServiceModels.DecimalDropDownListData>();
                result.Message.Add(ex.Message);
            }
            

            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

    }
}