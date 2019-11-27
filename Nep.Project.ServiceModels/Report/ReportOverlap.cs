using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportOverlap
    {
        // ลำดับ
        public String No { get; set; }

        // ชื่อ - สกุล
        public String Name { get; set; }

        // เลขที่บัตรประชาชน
        public String IdCardNo { get; set; }

        // ซ้ำซ้อน (โครงการ)
        public Decimal DuplicateAmount { get; set; }

        // โครงการ
        public String ProjectName { get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // รหัสจังหวัด
        public Decimal ProvinceID { get; set; }

        // เลขที่บัตรประชาชน
        public String Province { get; set; }

    }
}
