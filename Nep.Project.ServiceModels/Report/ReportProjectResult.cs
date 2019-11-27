using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report.ReportProjectResult
{
    public class GeneralProjectInfo
    {
        public decimal ProjectID { get; set; }
        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus { get; set; }
        public string ProjectApprovalStatus { get; set; }
        public string FollowupStatusCode { get; set; }
        public decimal? CreatorOrganizationID { get; set; }
        public decimal ProjectProvinceID { get; set; }

        public decimal BudgetYear { get; set; }
        public int BudgetYearThai
        {
            get
            {
                //string thaiYear = BudgetYear;
                //int year = 0;
                //Int32.TryParse(BudgetYear, out year);
                //thaiYear = (year > 0) ? (year + 543).ToString() : thaiYear;
                DateTime date = new DateTime((int)BudgetYear, 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                int thaiYear = Convert.ToInt32(date.ToString("yyyy", Common.Constants.UI_CULTUREINFO));
                return thaiYear;
            }
        }

        //1. ชื่อองค์กร
        public decimal OrganizationID { get; set; }
        public string OganizationNameTH { get; set; }
        private string _oganizationNameEN;
        public string OganizationNameEN
        {
            get
            {
                string text = (String.IsNullOrEmpty(_oganizationNameEN)) ? "-" : _oganizationNameEN;
                return text;
            }
            set
            {
                _oganizationNameEN = value;
            }
        }
        public string OganizationName
        {
            get
            {
                string fullname = (!String.IsNullOrEmpty(_oganizationNameEN))? String.Format("{0} ({1})", OganizationNameTH, _oganizationNameEN) : OganizationNameTH;
                return fullname;
            }
        }

        //ที่ตั้งองค์กร
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string AddressProvinceName { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string FullAddress
        {
            get
            {
                StringBuilder address = new StringBuilder();
                address.AppendFormat("เลขที่ {0}", Address);

                if (!String.IsNullOrEmpty(Building))
                {
                    address.AppendFormat(" อาคาร{0}", Building);
                }

                if (!String.IsNullOrEmpty(Moo))
                {
                    address.AppendFormat(" หมู่ {0}", Moo);
                }

                if (!String.IsNullOrEmpty(Soi))
                {
                    address.AppendFormat(" ซอย{0}", Soi);
                }

                if (!String.IsNullOrEmpty(Road))
                {
                    address.AppendFormat(" ถนน{0}", Road);
                }

                if (!String.IsNullOrEmpty(SubDistrict))
                {
                    address.AppendFormat(" แขวง/ตำบล {0}", SubDistrict);
                }

                if (!String.IsNullOrEmpty(District))
                {
                    address.AppendFormat(" เขต/อำเภอ {0}", District);
                }

                if (!String.IsNullOrEmpty(AddressProvinceName))
                {
                    address.AppendFormat(" จังหวัด{0}", AddressProvinceName);
                }

                if (!String.IsNullOrEmpty(Postcode))
                {
                    address.AppendFormat(" {0}", Postcode);
                }

                if (!String.IsNullOrEmpty(Telephone))
                {
                    address.AppendFormat(" โทรศัพท์/โทรศัพท์มือถือ {0}", Telephone);
                }

                if (!String.IsNullOrEmpty(Fax))
                {
                    address.AppendFormat(" โทรสาร {0}", Fax);
                }

                if (!String.IsNullOrEmpty(Email))
                {
                    address.AppendFormat(" อีเมล์ {0}", Email);
                }
                return address.ToString();
            }
        }
        public string ShowAddress
        {
            get
            {
                string address = "ที่ตั้ง " + FullAddress; 
                return address;
            }
        }
        //2.ชื่อหัวหน้าองค์กร       
        public string OganizationHeadFullName { get; set; }
        public string OganizationHeadAddress
        {
            get
            {
                string address = "ที่อยู่ " + FullAddress;
                return address;
            }
        }

        //3.ผู้รับผิดชอบโครงการ
        public string PrefixName1 { get; set; }
        public string Firstname1 { get; set; }
        public string Lastname1 { get; set; }
        public string Fullname1
        {
            get
            {
                String name = String.Format("{0}{1} {2}", PrefixName1, Firstname1, Lastname1);
                return name;
            }
        }
        
        //ที่อยู่
        public string Address1 { get; set; }
        public string Building1 { get; set; }
        public string Moo1 { get; set; }
        public string Soi1 { get; set; }
        public string Road1 { get; set; }
        public string SubDistrict1 { get; set; }
        public string District1 { get; set; }
        public string ProvinceName1 { get; set; }
        public string PostCode1 { get; set; }
        public string Telephone1 { get; set; }
        public string Fax1 { get; set; }
        public string Email1 { get; set; }
        public string FullAddrees1
        {
            get
            {
                StringBuilder address = new StringBuilder();
                address.AppendFormat("ที่อยู่ เลขที่ {0}", Address1);

                if (!String.IsNullOrEmpty(Building1))
                {
                    address.AppendFormat(" อาคาร{0}", Building1);
                }

                if (!String.IsNullOrEmpty(Moo1))
                {
                    address.AppendFormat(" หมู่ {0}", Moo1);
                }

                if (!String.IsNullOrEmpty(Soi1))
                {
                    address.AppendFormat(" ซอย{0}", Soi1);
                }

                if (!String.IsNullOrEmpty(Road1))
                {
                    address.AppendFormat(" ถนน{0}", Road1);
                }

                if (!String.IsNullOrEmpty(SubDistrict1))
                {
                    address.AppendFormat(" แขวง/ตำบล {0}", SubDistrict1);
                }

                if (!String.IsNullOrEmpty(District1))
                {
                    address.AppendFormat(" เขต/อำเภอ {0}", District1);
                }

                address.AppendFormat(" จังหวัด{0}", ProvinceName1);


                if (!String.IsNullOrEmpty(PostCode1))
                {
                    address.AppendFormat(" {0}", PostCode1);
                }

                if (!String.IsNullOrEmpty(Telephone1))
                {
                    address.AppendFormat(" โทรศัพท์/โทรศัพท์มือถือ {0}", Telephone1);
                }

                if (!String.IsNullOrEmpty(Fax1))
                {
                    address.AppendFormat(" โทรสาร {0}", Fax1);
                }

                if (!String.IsNullOrEmpty(Email1))
                {
                    address.AppendFormat(" อีเมล์ {0}", Email1);
                }
                return address.ToString();
            }
        }
    
        //4.ชื่อโครงการ      
        public string ProjectNameTH { get; set; }       
        public string ProjectNameEN { get; set; } 
        public string ProjectName
        {
            get
            {
                string name = ProjectNameTH;
                if(!String.IsNullOrEmpty(ProjectNameEN)){
                    name = String.Format("{0} ({1})", name, ProjectNameEN);
                }
                return name;
            }
        }

        //พื้นที่ดำเนินกิจกรรม
        public string OperationAddress { get; set; }
        public string OperationBuilding { get; set; }
        public string OperationMoo { get; set; }
        public string OperationSoi { get; set; }
        public string OperationRoad { get; set; }
        public string OperationSubDistrict { get; set; }
        public string OperationDistrict { get; set; }
        public string OperationProvince { get; set; }
        public string FullOperationAddress
        {
            get
            {
                StringBuilder address = new StringBuilder();
                address.AppendFormat("พื้นที่ดำเนินกิจกรรมตามโครงการ ที่อยู่ เลขที่ {0}", OperationAddress);

                if (!String.IsNullOrEmpty(OperationBuilding))
                {
                    address.AppendFormat(" อาคาร{0}", OperationBuilding);
                }

                if (!String.IsNullOrEmpty(OperationMoo))
                {
                    address.AppendFormat(" หมู่{0}", OperationMoo);
                }

                if (!String.IsNullOrEmpty(OperationSoi))
                {
                    address.AppendFormat(" ซอย{0}", OperationSoi);
                }

                if (!String.IsNullOrEmpty(OperationRoad))
                {
                    address.AppendFormat(" ถนน{0}", OperationRoad);
                }

                if (!String.IsNullOrEmpty(OperationSubDistrict))
                {
                    address.AppendFormat(" แขวง/ตำบล {0}", OperationSubDistrict);
                }

                if (!String.IsNullOrEmpty(OperationDistrict))
                {
                    address.AppendFormat(" เขต/อำเภอ {0}", OperationDistrict);
                }

                if (!String.IsNullOrEmpty(OperationProvince))
                {
                    address.AppendFormat(" {0}", OperationProvince);
                }

                return address.ToString();
            }
        }

        //5.ระยะเวลาการดำเนินโครงการ
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalDay { get; set; }

        //6.กิจกรรมโครงการ
        public string ActivityDescription { get; set; }

        //7.งบประมาณ
        public Decimal BudgetRequest { get; set; }
        public Decimal BudgetRevised { get; set; }
        public Decimal ActualExpense { get; set; }

        //8.ผลการดำเนินงาน
        public string Benefit { get; set; }

        //9.จำนวนผู้เข้าร่วมโครงการ
        public decimal? MaleParticipant { get; set; }
        public decimal? FemaleParticipant { get; set; }
        public decimal TotalParticipant 
        {
            get
            {
                decimal male = (MaleParticipant.HasValue)? (decimal)MaleParticipant : 0;
                decimal female = (FemaleParticipant.HasValue) ? (decimal)FemaleParticipant : 0;
                return (male + female);
            }
        }
    
        //10.ปัญหาอุปสรรคและวิธีการแก้ไขปัญหา
        public string ProblemsAndObstacle { get; set; }

        //11.ข้อคิดเห็นและข้อเสนอแนะ
        public string Suggestion { get; set; }

        

        //Reportor Info (องค์กร)
        public string ReporterName1 { get; set; }
        public string ReporterLastname1 { get; set; }
        public string Position1 { get; set; }
        public DateTime? ReportDate1 { get; set; }
        public string ReporterTelephone1 { get; set; }

        //Reportor Info (เจ้าหน้าที่)
        public string SuggestionDesc { get; set; }
        public string ReporterName2 { get; set; }
        public string ReporterLastname2 { get; set; }
        public string Position2 { get; set; }
        public DateTime? ReportDate2 { get; set; }
        
    }

    //4.ประเภทโครงการ
    public class ProjectType
    {
        public Int32 No { get; set; }
        public bool IsSelected { get; set; }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get; set; }
    }


    //12.สรุปผลการดำเนินงาน
    //12.1 เปรียบเทียบกับวัตถุประสงค์
    //12.2 เปรียบเทียบกับเป้าหมาย
    public class SummaryProjectResult
    {
        public bool IsSelected { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
