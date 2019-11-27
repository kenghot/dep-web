using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.Common;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportSummaryProjectInfo
    {
        public decimal ProjectID { get; set; }

        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }

        public string ProjectApprovalStatus { get; set; }
        public decimal? CreatorOrganizationID { get; set; }
        public decimal ProjectProvinceID { get; set; }

        public string ProjectNameTH { get; set; }
        public string ProjectNameEN { get; set; }
        public string ProjectName 
        {
            get
            {
                string text = ProjectNameTH;
                if(!String.IsNullOrEmpty(ProjectNameEN)){
                    text = string.Format("{0}({1})", text, ProjectNameEN);
                }
                return text;
            }
        }

        /// <summary>
        /// หน่วยงานที่รับผิดชอบ
        /// </summary>
        public decimal OrganizationID { get; set; }
        public string OrganizationNameTH { get; set; }
        public string OrganizationNameEN { get; set; }
        public string OrganizationName
        {
            get
            {
                string text = OrganizationNameTH;
                if (!String.IsNullOrEmpty(OrganizationNameEN))
                {
                    text = string.Format("{0}({1})", text, OrganizationNameEN);
                }
                return text;
            }
        }

        /// <summary>
        /// หลักการและเหตุผล
        /// </summary>
        public string ProjectReason { get; set; }

        /// <summary>
        /// วัตถุประสงค์
        /// </summary>
        public string ProjectPurpose { get; set; }

        /// <summary>
        /// กลุ่มเป้าหมาย และผู้เข้าร่วมโครงการ
        /// </summary>
        public string ProjectTargetGroup { get; set; }

        /// <summary>
        /// กิจกรรม
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// วิทยากร
        /// </summary>
        public string ProjectLecturer { get; set; }
               
        /// <summary>
        /// ระยะเวลาดำเนินการ
        /// </summary>
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// ระยะเวลาดำเนินการ
        /// </summary>
        public System.DateTime? EndDate { get; set; }

        //public string Address { get; set; }
        //public string Building { get; set; }
        //public string Moo { get; set; }
        //public string Soi { get; set; }
        //public string Road { get; set; }
        //public string SubDistrict { get; set; }
        //public string District { get; set; }
        //public string AddressProvinceName { get; set; }   
        /// <summary>
        /// สถานที่
        /// </summary>
        public string OperationAddress { get; set; }
        //public string FullAddress 
        //{
        //    get
        //    {
        //        StringBuilder address = new StringBuilder();
        //        address.AppendFormat("เลขที่ {0}", Address);

        //        if (!String.IsNullOrEmpty(Building))
        //        {
        //            address.AppendFormat(" อาคาร{0}", Building);
        //        }

        //        if (!String.IsNullOrEmpty(Moo))
        //        {
        //            address.AppendFormat(" หมู่ {0}", Moo);
        //        }

        //        if (!String.IsNullOrEmpty(Soi))
        //        {
        //            address.AppendFormat(" ซอย{0}", Soi);
        //        }

        //        if (!String.IsNullOrEmpty(Road))
        //        {
        //            address.AppendFormat(" ถนน{0}", Road);
        //        }

        //        if (!String.IsNullOrEmpty(SubDistrict))
        //        {
        //            address.AppendFormat(" แขวง/ตำบล {0}", SubDistrict);
        //        }

        //        if (!String.IsNullOrEmpty(District))
        //        {
        //            address.AppendFormat(" เขต/อำเภอ {0}", District);
        //        }

        //        if (!String.IsNullOrEmpty(AddressProvinceName))
        //        {
        //            address.AppendFormat(" จังหวัด{0}", AddressProvinceName);
        //        }               
        //        return address.ToString();
        //    }
        //}

        /// <summary>
        /// ผลที่คาดว่าจะได้รับ
        /// </summary>
        public string ProjectResult { get; set; }

        /// <summary>
        /// ตัวชี้วัด
        /// </summary>
        public string ProjectKPI { get; set; }

        /// <summary>
        /// ค่าใช้จ่ายที่เสนอขอ
        /// </summary>
        public decimal? BudgetValue { get; set; }

        /// <summary>
        /// ฝ่ายเลขา
        /// </summary>
        public decimal? BudgetReviseValue { get; set; }

        /// <summary>
        /// มติที่ประชุมคณะอนุกรรมการบริหารกองทุนฯ
        /// </summary>
        public decimal? BudgetApprovalValue { get; set; }

        /// <summary>
        /// ฝ่ายเลขา
        /// </summary>
        public string SecretaryComment { get; set; }

        /// <summary>
        /// มติที่ประชุมคณะอนุกรรมการบริหารกองทุนฯ
        /// </summary>
        public string CommitteeApprovalResult { get; set; }

        /// <summary>
        /// รหัสประเภทความพิการ
        /// </summary>
        public string DisabilityTypeCode{get;set;} 

        /// <summary>
        /// ชื่อคณะกรรมการกลั่นกรอง กรณีส่วนกลางแยกตามประเภทความพิการ
        /// </summary>
        public string DisabledCommitteePrositionName { get; set; }

        /// <summary>
        /// ชื่อคณะกรรมการกลั่นกรอง กรณีส่วนกลางแยกตามประเภทความพิการ
        /// </summary>
        public string Strategy { get; set; }

        /// <summary>
        /// จำนวนเงินที่อนุมัติไทย
        /// </summary>
        public string BudgetReviseValueThai
        {
            get
            {
                string value = "-";
                if (BudgetReviseValue != 0)
                {
                    value = Common.Report.Utility.ToThaiBath(BudgetReviseValue);
                }
                return value;
            }
        }
    }

    public class ReportSummaryProjectInfoBudgetDetail
    {
        public decimal ProjectBudgetID { get; set; }
        public string Detail { get; set; }
        /// <summary>
        /// รายละเอียดที่แก้ไข
        /// </summary>
        public string RevisedDetail { get; set; }

        /// <summary>
        /// เสนอขอ
        /// </summary>
        public decimal? RequestValue { get; set; }
        /// <summary>
        /// ฝ่ายเลขานุการ
        /// </summary>
        public decimal? ReviseValue { get; set; }

        /// <summary>
        /// คณะกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด หรือ คณะอนุกรรมตามประเภทคนพิการ
        /// </summary>
        public decimal? ReviseValue1 { get; set; }
        /// <summary>
        /// คณะอนุกรรมการบริหารกองทุน
        /// </summary>
        public decimal? ReviseValue2 { get; set; }
        public string RemarkApproval { get; set; }
    }
}
