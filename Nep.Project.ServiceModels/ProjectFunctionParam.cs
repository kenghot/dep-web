using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public class ProjectFunctionParam
    {
        public String ProjectApprovalStatus { get; set; }
        public Decimal? CreatorOrganizationID { get; set; }
        public Decimal ProjectProvinceID { get; set; }
        public String FolloupStatusCode { get; set; }
        public bool IsReportedResult {
            get{
                bool isTrue = false;
                isTrue = (!String.IsNullOrEmpty(FolloupStatusCode) && (FolloupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว));
                return isTrue;
            }
        }
        public String ApprovalStatus { get; set; }
        public Decimal ProjectOrgID { get; set; }
        public bool HasReportedResult { get; set; }
    }
}
