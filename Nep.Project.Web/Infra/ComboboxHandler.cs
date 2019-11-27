using Autofac.Integration.Web.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Nep.Project.Web.Infra
{
    [InjectProperties]
    public class ComboboxHandler : IHttpHandler
    {
        public IServices.IProviceService ProvinceService { get; set; }
        public IServices.IRegisterService RegisterService { get; set; }
        public IServices.IOrganizationService OrganizationService { get; set; }

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var action = Path.GetFileName(context.Request.FilePath).ToLower();
            var httpMethod = context.Request.HttpMethod.ToLower();

            if (action.Equals("getprovince"))
            {
                WriteProvince(context);
            }
            else if (action.Equals("getdistrict"))
            {
                WriteDistrict(context);
            }
            else if (action.Equals("getsubdistrict"))
            {
                WriteSubDistrict(context);
            }
            else if (action.Equals("getorg"))
            {
                WriteOrg(context);
            }
            else if (action.Equals("orgregister"))
            {
                WriteOrgregister(context);
            }
            else if (action.Equals("getsearchorgprovince"))
            {
                WriteSearchOrgProvince(context);
            }
            else if (action.Equals("getorgprovince"))
            {
                WriteOrgProvince(context);
            }
            else if (action.Equals("orgvaluemapper"))
            {
                OrgValueMapper(context);
            }
            else if (action.Equals("orgregistermapper"))
            {
                OrgRegisterValueMapper(context);
            }
        }

        private void OrgValueMapper(HttpContext context)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();
            
            try
            {
                String strValues = context.Request.Form["values[0]"];
                String strProvinceID = context.Request.Form["values[1]"];

                result.IsCompleted = true;
                result.Data = new List<int>();
                if (!String.IsNullOrEmpty(strValues))
                {                   
                    decimal? provinceID = (!String.IsNullOrEmpty(strProvinceID))? Convert.ToDecimal(strProvinceID) : (decimal?)null;


                    List<decimal> values = new List<decimal>();
                    values.Add(Convert.ToDecimal(strValues));               
                    result = OrganizationService.ListValueMapping(values, provinceID);
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