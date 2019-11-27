using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportOverlapProvince
    {

        // ภาค
        public String Region { get; set; }

        // รหัสภาค
        public Decimal RegionID { get; set; }

        // จังหวัด
        public String Province { get; set; }

        // รหัสจังหวัด
        public Decimal ProvinceID { get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // ประเภทความพิการ
        public String Disablility { get; set; }

        // ประเภทความพิการ
        public Decimal DisablilityAmt { get; set; }
    }
}
