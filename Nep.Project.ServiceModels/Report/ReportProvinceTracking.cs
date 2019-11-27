using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportProvinceTracking
    {        
        public String ReportNo { get; set; }
        public String ReportDate { get; set; }        
        public String To { get; set; }        
        public String Reason { get; set; }
        public String Purpose { get; set; }
        public String Summary { get; set; }
        public String OrganizationName { get; set; }
        public String DepartmentContact { get; set; }    
    }
}
