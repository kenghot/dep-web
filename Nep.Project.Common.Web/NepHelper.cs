using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Common.Web
{
    public static class NepHelper
    {
        private static void BuildFilterString(StringBuilder s, List<Object> filterValues, ServiceModels.IFilterDescriptor filter)
        {
            if (filter is ServiceModels.CompositeFilterDescriptor)
            {
                BuildFilterString(s, filterValues, filter as ServiceModels.CompositeFilterDescriptor);
            }
            if (filter is ServiceModels.FilterDescriptor)
            {
                BuildFilterString(s, filterValues, filter as ServiceModels.FilterDescriptor);
            }
        }

        private static void BuildFilterString(StringBuilder s, List<Object> filterValues, ServiceModels.CompositeFilterDescriptor filter)
        {

            //s.Append("AND (");
            s.Append("(");
            for (int i = 0; i < filter.FilterDescriptors.Count; i++)
            {
                if (i >= 1)
                {
                    if (filter.LogicalOperator == ServiceModels.FilterCompositionLogicalOperator.And)
                    {
                        s.Append("&&");
                    }
                    else if (filter.LogicalOperator == ServiceModels.FilterCompositionLogicalOperator.Or)
                    {
                        s.Append("||");
                    }
                }
                BuildFilterString(s, filterValues, filter.FilterDescriptors[i]);
            }
            s.Append(")");

        }

        private static void BuildFilterString(StringBuilder s, List<Object> filterValues, ServiceModels.FilterDescriptor filter)
        {
            String filterName = filter.Field.ToUpper();

            switch (filter.Operator)
            {
                case ServiceModels.FilterOperator.Contains:
                    {
                        s.AppendFormat("{0}.Contains(@{1})", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.DoesNotContain:
                    {
                        s.AppendFormat("!{0}.Contains(@{1})", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.StartsWith:
                    {
                        s.AppendFormat("{0}.StartsWith(@{1})", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.EndsWith:
                    {
                        s.AppendFormat("{0}.EndsWith(@{1})", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsEqualTo:
                    {
                        s.AppendFormat("{0}==@{1}", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsNotEqualTo:
                    {
                        s.AppendFormat("{0}!=@{1}", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsGreaterThan:
                    {
                        s.AppendFormat("{0}>@{1}", filter.Field.ToUpper(), filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsGreaterThanOrEqualTo:
                    {
                        s.AppendFormat("{0}>=@{1}", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsLessThan:
                    {
                        s.AppendFormat("{0}<@{1}", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsLessThanOrEqualTo:
                    {
                        s.AppendFormat("{0}<=@{1}", filterName, filterValues.Count);
                        break;
                    }
                case ServiceModels.FilterOperator.IsContainedIn:
                    {
                        s.AppendFormat("@{1}.Contains({0})", filterName, filterValues.Count);
                        break;
                    }
            }

            filterValues.Add(filter.Value);

        }

        public static ServiceModels.QueryParameter ToQueryParameter(List<ServiceModels.IFilterDescriptor> filters, 
            int pageIndex, int pageSize, String  sortExpression, System.Web.UI.WebControls.SortDirection? sortDirection)
        {
            ServiceModels.QueryParameter obj = new ServiceModels.QueryParameter() ;
                
            StringBuilder filterStringBuilder = new StringBuilder();
            StringBuilder sortStringBuilder = new StringBuilder();
            List<Object> filterValues = new List<Object>();
            String whereCause = "";

            if (filters != null)
            {
                foreach (ServiceModels.IFilterDescriptor filter in filters)
                {
                    BuildFilterString(filterStringBuilder, filterValues, filter);
                }

                whereCause = filterStringBuilder.ToString();
                //int compositOperator = whereCause.IndexOf("AND");
                //if (compositOperator == 0)
                //{
                //    whereCause = whereCause.Substring(3);
                //}
            }

            if (!String.IsNullOrEmpty(sortExpression))
            {
                sortStringBuilder.Append(sortExpression);
                if ((sortDirection != null) && (sortDirection == System.Web.UI.WebControls.SortDirection.Ascending))
                {
                    sortStringBuilder.Append(" ASC");
                }
                else
                {
                    sortStringBuilder.Append(" DESC");
                }
            }          

            obj.WhereCause = whereCause;
            obj.WhereCauseParameters = filterValues.ToArray();
            obj.OrderBy = sortStringBuilder.ToString();
            obj.PageIndex = pageIndex;
            obj.PageSize = pageSize;
                  
            return obj;
        }
        public static void WriteLog(string msg)
        {
            var appLog = new EventLog("Application");
            appLog.Source = "NEP";
            appLog.WriteEntry(msg);
        }
    }
}
