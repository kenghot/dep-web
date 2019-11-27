using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportOrgTracking
    {
        public String NepAddress { get; set; }
        public String ReportNo { get; set; }
        public String ReportDate { get; set; }
        public String Subject { get; set; }
        public String To { get; set; }
        public String Attachment { get; set; }
        public String Reason { get; set; }
        public String Purpose { get; set; }
        public String Summary { get; set; }
        public String Complementary { get; set; }        
        public String DepartmentContact { get; set; }        
    }
}
