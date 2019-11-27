using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportEvaluationSummary
    {
        // หมายเลข
        public String No { get; set; }

        // ชื่อองค์กร
        public String Organization { get; set; }

        // ชื่อโครงการ
        public String ProjectName { get; set; }

        // จำนวนเงินที่ขอรับการสนับสนุน
        public Decimal? RequestBudget { get; set; }

        // เกณฑ์ชี้วัดข้อ 4
        public String IsPassAss4 { get; set; }

        // เกณฑ์ชี้วัดข้อ 5
        public String IsPassAss5 { get; set; }

        // ก
        public Decimal? Assessment61 { get; set; }

        // ข
        public Decimal? Assessment62 { get; set; }

        // ค
        public Decimal? Assessment63 { get; set; }

        // ง
        public Decimal? Assessment64 { get; set; }

        // จ
        public Decimal? Assessment65 { get; set; }

        // ฉ
        public Decimal? Assessment66 { get; set; }

        // ช
        public Decimal?  Assessment67 { get; set; }

        // ซ
        public Decimal? Assessment68 { get; set; }

        // ฌ
        public Decimal? Assessment69 { get; set; }

        // จำนวนเงินที่ขอรับการสนับสนุน
        public Decimal? BugetReviseValue { get; set; }

        // หมายเหตุ
        public String AssessmentDesc{ get; set; }

        // ปีงบประมาณ
        public Decimal BudgetYear { get; set; }

        // เลขทะเบียนโครงการ
        public String ProjectNo { get; set; }

        // จังหวัด
        public Decimal? ProvinceID { get; set; }
    }
}
