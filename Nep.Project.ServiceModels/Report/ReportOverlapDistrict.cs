using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportOverlapDistrict
    {
        // ลำดับ
        public Int32 No { get; set; }

        // ภาค
        public String Region { get; set; }

        // จังหวัด
        public String Province { get; set; }

        // อำเภอ
        public String District { get; set; }

        // จำนวนซ้ำซ้อน 
        public String DuplicateAmount { get; set; }

        // การเห็น
        public String Type1 { get; set; }

        // การได้ยิน 
        public String Type2 { get; set; }

        // การเคลื่อนไหว 
        public String Type3 { get; set; }

        // จิตใจ
        public String Type4 { get; set; }

        // สติปัญญา
        public String Type5 { get; set; }

        // การเรียนรู้
        public String Type6 { get; set; }

        // ออทิสติก 
        public String Type7 { get; set; }

        // ความพิการ
        public String Type8 { get; set; }

        // ปีงบประมาณ
        public String BudgetYear { get; set; }
    }
}
