using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportBudgetApplicant
    {
        // หมายเลข
        public String No { get; set; }

        // เลขทะเบียนโครงการ
        public String ProjectNo { get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // ภาคหน่วยงาน
        public String OrganizationType { get; set; }

        // ชื่อองค์กร
        public String Organization { get; set; }

        // หน่วยงานภายใต้การรับรอง
        public String OrganizationSupport { get; set; }

        // ชื่อโครงการ
        public String ProjectName { get; set; }

        // ประเภทโครงการ
        public String ProjectType { get; set; }

        // ภาค
        public String Section { get; set; }
        public String SectionCode { get; set; }

        // จังหวัด
        public String Province { get; set; }

        // วันที่ขอรับการสนับสนุน
        public DateTime? ProjectDate { get; set; }
        public String ProjectDateStr { get; set; }

        // จำนวนเงินที่ขอรับการสนับสนุน
        public Decimal? RequestBudget { get; set; }

        // ประเภทเงิน
        public String ApprovalBudgetType { get; set; }

        // จำนวนผู้เข้าร่วมโครงการ
        public Decimal? SumTargetGroupAmount { get; set; }

        // คณะกรรมการกลั่นกรอง(เห็นชอบ)
        public String Approval1 { get; set; }

        // คณะกรรมการกลั่นกรอง(ไม่เห็นชอบ)
        public String Approval3 { get; set; }

        // คณะกรรมการกลั่นกรอง(ปรับลดวงเงิน)
        public String Approval2 { get; set; }

        // คณะกรรมการกลั่นกรอง(ปรับปรุง)
        public String Approval_ { get; set; }

        // สถานะ(อนุมัติ)
        public String ApprovalStatus1 { get; set; }

        // สถานะ(ไม่อนุมัติ)
        public String ApprovalStatus0 { get; set; }

        // สถานะ(ปรับปรุง)
        public String ApprovalStatusNull { get; set; }
        public String ApprovalStatus { get; set; }

        // มติประชุม(ครั้งที่)
        public String ApprovalNo { get; set; }

        // มติประชุม(วัน/เดือน/ปี)
        public DateTime? ApprovalDate2 { get; set; }
        public String ApprovalDate2Str { get; set; }

        // ระดับมติ
        public String ProvinceAbbr { get; set; }

        // จำนวนเงินที่ได้รับอนุมัติ
        public Decimal? BudgetReviseValue { get; set; }

        // ระยะเวลาที่ใช้ดำเนินการ
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public String ContractDateStr { get; set; }

        // จำนวนผู้เข้าร่วมที่ขออนุมัติ(ชาย)
        public Decimal? ProjectTargetGroupMale { get; set; }

        // จำนวนผู้เข้าร่วมที่ขออนุมัติ(หญิง)
        public Decimal? ProjectTargetGroupFemale { get; set; }

        // จำนวนผู้เข้าร่วมที่ขออนุมัติ(รวม)
        public Decimal? ProjectTargetGroupAmount { get; set; }

        // จำนวนผู้เข้าร่วมโครงการจริง(ชาย)
        public Decimal? MaleParticipant { get; set; }

        // จำนวนผู้เข้าร่วมโครงการจริง(หญิง)
        public Decimal? FemaleParticipant { get; set; }

        // จำนวนผู้เข้าร่วมโครงการจริง(รวม)
        public Decimal? ParticipantAmount { get; set; }

        // กลุ่มเป้าหมาย
        public String TargetGroup { get; set; }
        public IEnumerable<String> TargetGroups { get; set; }

        // จำนวนเงินใช้จริง
        public Decimal? ActualExpense { get; set; }

        // จำนวนเงินเรียกคืน
        public Decimal? ReturnAmount { get; set; }
    }
}
