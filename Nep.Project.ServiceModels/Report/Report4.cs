using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class Report4
    {
        // อนุมัติครั้งที่
        public String ApproveNo {get;set;}

        // อนุมัติปี
        public String ApproveYear {get;set;}

        // ลำดับที่
        public String No {get;set;}

        // ชื่อโครงการ
        public String ProjectName {get;set;}

        // ชื่อองค์กร
        public String OrganizationName {get;set;}

        // จำนวนเงินอนุมัติ
        public Decimal? Amount {get;set;}

        // สัญญาเมื่อวันที่
        public DateTime? ContractDate { get; set; }

        public String ContractDateStr {get;set;}

        // ปีงบประมาณ
        public Decimal BudgetYear {get;set;}

        // ระยะเวลาดำเนินการ
        public DateTime? StartDate { get; set; }

        public String StartDateStr { get; set; }

        // ระยะเวลาดำเนินการ
        public DateTime? EndDate { get; set; }

        public String EndDateStr { get; set; }

        // อยู่ระหว่างดำเนินการ , ดำเนินการเสร็จแล้วรายงานแล้ว , ดำเนินการเสร็จแล้วแต่ยังไม่ได้รายงาน
        public String Status {get;set;}

        // จังหวัด
        public Decimal ProvinceId { get; set; }

        // รหัสโครงการ
        public Decimal ProjectId { get; set; }
    }
}
