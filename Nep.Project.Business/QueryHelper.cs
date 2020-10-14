using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Linq.Dynamic;
using Nep.Project;

namespace Nep.Project.Business
{
    public class QNData
    {
        public string n { get; set; }
        public object v { get; set; }
    }
    public static class QueryHelper
    {
        private static IQueryable<T> InnerApplyDefaultSort<T>(IQueryable<T> query)
        {
            if (query.Provider != null)
            {
                var type = query.Provider.GetType();
                if (!type.FullName.StartsWith("System.Linq.EnumerableQuery"))
                {
                    query = query.OrderBy(x => x);
                }
            }
            return query;
        }

        private static IQueryable<T> InnerApplyParameter<T>(IQueryable<T> query, ServiceModels.QueryParameter param)
        {
            var newQuery = query;
            if (param != null)
            {
                if (!String.IsNullOrWhiteSpace(param.WhereCause))
                {
                    newQuery = newQuery.Where(param.WhereCause, param.WhereCauseParameters);
                }

                if (!String.IsNullOrWhiteSpace(param.OrderBy))
                {
                    newQuery = newQuery.OrderBy(param.OrderBy);
                }
                else
                {
                    newQuery = InnerApplyDefaultSort(newQuery);
                }
            }
            else
            {
                newQuery = InnerApplyDefaultSort(newQuery);
            }

            return newQuery;
        }

        private static Int32 GetStartRow(ServiceModels.QueryParameter param)
        {
            return param != null ? ((param.PageIndex > 0 ? param.PageIndex : 0) * (param.PageSize > 0 ? param.PageSize : 1)) : 0;
        }


        private static Int32 GetPageSize(ServiceModels.QueryParameter param)
        {
            return param == null ? Int32.MaxValue : (param.PageSize > 0 ? param.PageSize : Common.Constants.PAGE_SIZE);
        }

        public static IQueryable<T> ApplyParameter<T>(this IQueryable<T> query, ServiceModels.QueryParameter param)
        {
            return InnerApplyParameter(query, param);
        }

        public static ServiceModels.ReturnQueryData<T> ToQueryData<T>(this IQueryable<T> query, ServiceModels.QueryParameter param)
        {
            ServiceModels.ReturnQueryData<T> output = new ServiceModels.ReturnQueryData<T>();
            try
            {
                var newQuery = InnerApplyParameter(query, param);
                output.TotalRow = newQuery.Count();
                output.Data = newQuery.Skip(GetStartRow(param)).Take(GetPageSize(param)).ToList();
                output.IsCompleted = true;
            }
            catch (Exception ex)
            {
                output.IsCompleted = false;

                if (ex is System.Data.DataException)
                {
                    output.Message.Add(ex.Message);
                }
                else
                {
                    output.Message.Add(Resources.Error.UnexpectedError);
                }
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Query", ex);
            }
            return output;
        }
 
    }
}
