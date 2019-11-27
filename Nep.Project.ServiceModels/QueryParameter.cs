using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public class QueryParameter
    {
        public String WhereCause { get; set; }
        public Object[] WhereCauseParameters { get; set; }
        public String OrderBy { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }
    }
}
