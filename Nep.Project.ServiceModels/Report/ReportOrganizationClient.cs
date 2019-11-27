using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportOrganizationClient
    {

        // รหัสจังหวัด
        public Decimal ProvinceID { get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // โครงการ
        public String ProjectName { get; set; }

        // อชื่อ นามสกุล ผู้รับบริการ
        public String Name {get;set;}

        // เพศ
        public String Gender { get; set; }

        // เลขประจำตัวประชาชน
        public String IdCardNo {get;set;}

        // กลุ่มเป้าหมาย
        public String Target {get;set;}

        // รหัสจังหวัด
        public Decimal DuplicatedAmt { get; set; }

    }
}
