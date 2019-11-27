using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportReceiverFunding
    {
        // หมายเลข
        public String No { get; set; }

        // ปีงบประมาณ
        public String BudgetYear { get; set; }

        // ภาคหน่วยงาน
        public String Agencies { get; set; }

        // ชื่อองค์กร
        public String Organization { get; set; }

        // หน่วยงานภาคใต้การรับรอง
        public String AgencyUnder { get; set; }

        // ชื่อโครงการ
        public String ProjectName { get; set; }

        // ประเภทโครงการ
        public String ProjectType { get; set; }

        // ภาค
        public String Region { get; set; }

        // จังหวัด
        public String Province { get; set; }

        // วันที่ขอรับการสนับสนุน
        public String SupportRequestDate { get; set; }

        // จำนวนเงินที่ขอรับการสนับสนุน
        public Decimal? SupportRequestAmount { get; set; }

        // ประเภทเงิน
        public String FundingType { get; set; }

        // จำนวนผู้เข้าร่วมโครงการ
        public Int32? TotalMembers { get; set; }

        // คณะทำงานกลั่นกรองฯ
        public Int32? ResultConsider { get; set; }

        // สถานะ
        public Int32? Status { get; set; }

        // มติประชุม - ครั้งที่
        public String ResolutionTime { get; set; }

        // มติประชุม - วัน/เดือน/ปี
        public String ResolutionDate { get; set; }

        // มติประชุม - ระดับมติ
        public String ResolutionLevel { get; set; }

        // จำนวนเงินที่ได้รับอนุมัติ
        public Decimal? ApproveAmount { get; set; }

        // ระยะเวลาที่ใช้ดำเนินการ
        public String Period { get; set; }

        //จำนวนผู้เข้าร่วมที่ขออนุมัติ
        public Int32? AmountMaleRequest { get; set; }
        public Int32? AmountFemaleRequest { get; set; }
        public Int32? TotalAmountRequest { get; set; }

        //จำนวนผู้เข้าร่วมโครงการจริง
        public Int32? AmountMaleReal { get; set; }
        public Int32? AmountFemaleReal { get; set; }
        public Int32? TotalAmountReal { get; set; }

        //กลุ่มเป้าหมาย
        public String Target { get; set; }

        //จำนวนเงินใช้จ่ายจริง
        public Decimal? AmountReal { get; set; }

        //จำนวนเงินเรียกคืน
        public Decimal? AmountReFunding { get; set; }
    }
}
