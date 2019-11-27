using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public interface IFilterDescriptor
    {
    }
      
    [Serializable]
    public class FilterDescriptor : IFilterDescriptor
    {      
        public String Field { get; set; }
        public Object Value { get; set; }
        public FilterOperator Operator { get; set; }
    }

    [Serializable]
    public class CompositeFilterDescriptor : IFilterDescriptor
    {
        public FilterCompositionLogicalOperator LogicalOperator{get; set;}
        public List<IFilterDescriptor> FilterDescriptors { get; set; }
    }

    [Serializable]
    public class GroupCompositeFilterDescriptor : IFilterDescriptor
    {
        public FilterCompositionLogicalOperator LogicalOperator { get; set; }
        public List<CompositeFilterDescriptor> CompositeFilterDescriptors { get; set; }
    }

    public enum FilterOperator
    {
        IsLessThan = 0,       
        IsLessThanOrEqualTo = 1,       
        IsEqualTo = 2,      
        IsNotEqualTo = 3,
        IsGreaterThanOrEqualTo = 4,        
        IsGreaterThan = 5,        
        StartsWith = 6,
        EndsWith = 7,
        Contains = 8,
        IsContainedIn = 9,
        DoesNotContain = 10,
    }

    public enum FilterCompositionLogicalOperator
    {
        And,
        Or
    }

}
