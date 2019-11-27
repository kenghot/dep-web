using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class Report1
    {
        // ชื่อโครงการ
        public string ProjectName { get; set; }
        // เรื่อง
        public string ProjectSubject { get; set; }
        // หน่วยงานที่รับผิดชอบ
        public String Responsible { get; set; }
        // หลักการและเหตุผล
        public String Principles { get; set; }
        // วัตถุประสงค์
        public String Objective { get; set; }
        // กลุ่มเป้าหมายและผู้เข้าร่วมโครงการ
        public String Target { get; set; }
        // กิจกรรม
        public String Activity { get; set; }
        // วิทยากร
        public String Lecturer { get; set; }
        // ระยะเวลาดำเนินการ
        public String Period { get; set; }
        // สถานที่
        public String Place { get; set; }
        // ผลที่คาดว่าจะได้รับ
        public string Benefits { get; set; }
        // ตัวชี้วัด
        public string Indications { get; set; }
        // ค่าใช้จ่ายที่เสนอขอ
        public decimal? TotalExpenses { get; set; }
        // ค่าใช้จ่ายที่เสนอขอ
        public decimal? TotalRequired { get; set; }
        // ฝ่ายเลขา
        public String Secretariat { get; set; }
        // มติที่ประชุม
        public String Resolution { get; set; }
    }

    public class Report1Detail
    {
        public int GroupOrder { get; set; }

        public string GroupName { get; set; }

        public decimal? Amount { get; set; }

        public string Expenses { get; set; }

        public string Remark { get; set; }
    }
}
