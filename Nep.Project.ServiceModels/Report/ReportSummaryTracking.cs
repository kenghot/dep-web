using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportSummaryTracking
    {
        // หมายเลข
        public String No { get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // เลขทะเบียน
        public String RegisterNumber { get; set; }

        // ชื่อโครงการ
        public String ProjectName { get; set; }

        // ชื่อองค์กร
        public String OrganizationName { get; set; }

        // จังหวัด
        public String Province { get; set; }

        // รหัสจังหวัด
        public Decimal ProvinceID { get; set; }

        // สถานะ
        public String Status { get; set; }

        // รหัสสถานะ
        public Decimal StatusID { get; set; }

        // วันสิ้นสุดโครงการ
        public DateTime? FinishedDate { get; set; }

        // เดือน
        public Decimal MonthTracing { get; set; }

        // ปี
        public Decimal YearTracing { get; set; }
    }
}
