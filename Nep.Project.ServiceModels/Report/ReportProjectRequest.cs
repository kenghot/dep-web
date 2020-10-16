using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report.ReportProjectRequest
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
        public decimal? CreatorOrganizationID { get; set; }
        public decimal? ProjectProvinceID { get; set; }

        public decimal BudgetYear { get; set; }
        public String BudgetYearThai { 
            get{
                //string thaiYear = BudgetYear;
                //int year = 0;
                //Int32.TryParse(BudgetYear, out year);
                //thaiYear = (year > 0) ? (year + 543).ToString() : thaiYear;
                DateTime date = new DateTime((int)BudgetYear, 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                return date.ToString("yyyy", Common.Constants.UI_CULTUREINFO);
            }
        }

        //1.1
        public decimal OganizationID { get; set; }
        public string OganizationNameTH { get; set; }
        private string _oganizationNameEN;
        public string OganizationNameEN
        {
            get {
                string text = (String.IsNullOrEmpty(_oganizationNameEN)) ? "-" : _oganizationNameEN;
                return text;
            }
            set
            {
                _oganizationNameEN = value;
            }
        }

        //1.2
        public decimal? OganizationTypeID { get; set; }
        public string OganizationTypeIDText { 
            get 
            {
                string text = "";
                if (OganizationTypeID.HasValue)
                {
                    text = OganizationTypeID.ToString();
                }
                return text;
            } 
        }
        public string OrganizationTypeEtc { get; set; }
        public string OganizationType1Name
        {
            get
            {
                String name = (OganizationTypeID.HasValue && (OganizationTypeID == Common.OrganizationTypeID.สังกัดกรม)) ? OrganizationTypeEtc : "";
                
                return name;
            }
        }
        public string OganizationType2Name
        {
            get
            {
                String name = (OganizationTypeID.HasValue && (OganizationTypeID == Common.OrganizationTypeID.กระทรวง)) ? OrganizationTypeEtc : "";
                
                return name;
            }
        }
        public string OganizationType7Name
        {
            get
            {
                String name = (OganizationTypeID.HasValue && (OganizationTypeID == Common.OrganizationTypeID.อื่นๆ)) ? OrganizationTypeEtc : "";
                
                return name;
            }
        }

        //1.4
        public string OrganizationYear { get; set; }
        public string OrganizationYearThai { 
            get 
            {
                string thaiYear = OrganizationYear;
                int year = 0;
                Int32.TryParse(OrganizationYear, out year);
                thaiYear = (year > 0) ? (year + 543).ToString() : thaiYear;
                return thaiYear;
            } 
        }

        //1.5
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

                if(!String.IsNullOrEmpty(Moo)){
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

        //1.6
        public string Purpose { get; set; }

        //1.7
        public string CurrentProject { get; set; }

        //1.8
        public string CurrentProjectResult { get; set; }

        //1.9
        public string GotSupportFlag { get; set; } 
        public string GotSupportYear { get; set; }
        public string GotSupportYearThai 
        {
            get 
            {
                string thaiYear = GotSupportYear;
                int year = 0;
                Int32.TryParse(GotSupportYear, out year);
                thaiYear = (year > 0) ? (year + 543).ToString() : thaiYear;
                return thaiYear;
            } 
        }
        public decimal? GotSupportTimes { get; set; }
        
        private string _gotSupportLastProject;
        public string GotSupportLastProject {
            get
            {
                string text = (String.IsNullOrEmpty(_gotSupportLastProject)) ? "-" :_gotSupportLastProject;
                return text;
            }
            set
            {
                _gotSupportLastProject = value;
            }
        }
        public string GotSupportLastProjectHtml
        {
            get
            {
                string text = (String.IsNullOrEmpty(_gotSupportLastProject)) ? "<div style='text-align:justify; text-indent: 0.33in;'>โครงการล่าสุดที่เคยขอรับการสนับสนุนชื่อโครงการ - </div>" :
                    ("<div style='text-align:justify; text-indent: 0.33in;'>โครงการล่าสุดที่เคยขอรับการสนับสนุนชื่อโครงการ " + _gotSupportLastProject + "</div>");
                return text;
            }
        }

        public string GotSupportLastResult { get; set; }            
        public string GotSupportLastResultHtml
        {
            get
            {
                string text = "";
                if (!String.IsNullOrEmpty(GotSupportLastResult))
                {
                    text = "<div style='text-align:justify; text-indent: 0.33in;'>" + GotSupportLastResult + "</div>";
                }
                return text;
            }
        }

        public string GotSupportLastProblems { get; set; }
        public string GotSupportLastProblemsHtml
        {
            get
            {
                string text = "";
                if (!String.IsNullOrEmpty(GotSupportLastProblems))
                {
                    text = "<div style='text-align:justify; text-indent: 0.33in;'>" + GotSupportLastProblems + "</div>";
                }
                return text;
            }
        }


        //2.1
        public string ProjectNameTH { get; set; }
        //private 
        public string ProjectNameEN 
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

        //2.2
        public decimal? DisabilityTypeID { get; set; }
        public string DisabilityTypeCode { get; set; }

        public string PrefixOther1 { get; set; }
        public string PrefixOther2 { get; set; }
        public string PrefixOther3 { get; set; }
        public string PrefixCode1 { get; set; }
        public string PrefixCode2 { get; set; }
        public string PrefixCode3 { get; set; }
        //2.3 ผู้รับผิดชอบโครงการ  
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
                address.AppendFormat("เลขที่ {0}", Address1);

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

        //2.4 ผู้ประสานงานโครงการ        
        public string PrefixName2 { get; set; }
        public string Firstname2 { get; set; }
        public string Lastname2 { get; set; }
        public string Address2 { get; set; }
        public string Building2 { get; set; }
        public string Moo2 { get; set; }
        public string Soi2 { get; set; }
        public string Road2 { get; set; }        
        public string SubDistrict2 { get; set; }        
        public string District2 { get; set; }
        public string ProvinceName2 { get; set; }
        public string PostCode2 { get; set; }
        public string Telephone2 { get; set; }
        public string Fax2 { get; set; }
        public string Email2 { get; set; }
        public string FullAddrees2
        {
            get
            {
                StringBuilder address = new StringBuilder();
                address.AppendFormat("เลขที่ {0}", Address2);

                if (!String.IsNullOrEmpty(Building2))
                {
                    address.AppendFormat(" อาคาร{0}", Building2);
                }

                if (!String.IsNullOrEmpty(Moo2))
                {
                    address.AppendFormat(" หมู่ {0}", Moo2);
                }

                if (!String.IsNullOrEmpty(Soi2))
                {
                    address.AppendFormat(" ซอย{0}", Soi2);
                }

                if (!String.IsNullOrEmpty(Road2))
                {
                    address.AppendFormat(" ถนน{0}", Road2);
                }

                if (!String.IsNullOrEmpty(SubDistrict2))
                {
                    address.AppendFormat(" แขวง/ตำบล {0}", SubDistrict2);
                }

                if (!String.IsNullOrEmpty(District2))
                {
                    address.AppendFormat(" เขต/อำเภอ {0}", District2);
                }

                if (!String.IsNullOrEmpty(ProvinceName1))
                {
                    address.AppendFormat(" จังหวัด{0}", ProvinceName1);
                }               

                if (!String.IsNullOrEmpty(PostCode2))
                {
                    address.AppendFormat(" {0}", PostCode2);
                }

                if (!String.IsNullOrEmpty(Telephone2))
                {
                    address.AppendFormat(" โทรศัพท์/โทรศัพท์มือถือ {0}", Telephone2);
                }

                if (!String.IsNullOrEmpty(Fax2))
                {
                    address.AppendFormat(" โทรสาร {0}", Fax2);
                }

                if (!String.IsNullOrEmpty(Email2))
                {
                    address.AppendFormat(" อีเมล์ {0}", Email2);
                }
                return address.ToString();
            }
        }

      
        public string PrefixName3 { get; set; }
        public string Firstname3 { get; set; }
        public string Lastname3 { get; set; }
        public string Address3 { get; set; }
        public string Building3 { get; set; }
        public string Moo3 { get; set; }
        public string Soi3 { get; set; }
        public string Road3 { get; set; }        
        public string SubDistrict3 { get; set; }        
        public string District3 { get; set; }        
        public string ProvinceName3 { get; set; }
        public string PostCode3 { get; set; }
        public string Telephone3 { get; set; }
        public string Fax3 { get; set; }
        public string Email3 { get; set; }
        public string FullAddrees3
        {
            get
            {
                StringBuilder address = new StringBuilder();              
                address.AppendFormat("เลขที่ {0}", Address3);

                if (!String.IsNullOrEmpty(Building3))
                {
                    address.AppendFormat(" อาคาร{0}", Building3);
                }

                if (!String.IsNullOrEmpty(Moo3))
                {
                    address.AppendFormat(" หมู่ {0}", Moo3);
                }

                if (!String.IsNullOrEmpty(Soi3))
                {
                    address.AppendFormat(" ซอย{0}", Soi3);
                }

                if (!String.IsNullOrEmpty(Road3))
                {
                    address.AppendFormat(" ถนน{0}", Road3);
                }

                if (!String.IsNullOrEmpty(SubDistrict3))
                {
                    address.AppendFormat(" แขวง/ตำบล {0}", SubDistrict3);
                }

                if (!String.IsNullOrEmpty(District3))
                {
                    address.AppendFormat(" เขต/อำเภอ {0}", District3);
                }

                if (!String.IsNullOrEmpty(ProvinceName3))
                {
                    address.AppendFormat(" จังหวัด{0}", ProvinceName3);
                }

                if (!String.IsNullOrEmpty(PostCode3))
                {
                    address.AppendFormat(" {0}", PostCode3);
                }

                if (!String.IsNullOrEmpty(Telephone3))
                {
                    address.AppendFormat(" โทรศัพท์/โทรศัพท์มือถือ {0}", Telephone3);
                }

                if (!String.IsNullOrEmpty(Fax3))
                {
                    address.AppendFormat(" โทรสาร {0}", Fax3);
                }

                if (!String.IsNullOrEmpty(Email3))
                {
                    address.AppendFormat(" อีเมล์ {0}", Email3);
                }
                return address.ToString();
            }
        }
    
        //2.5
        public string ProjectReason { get; set; }

        //2.6
        public string ProjectPurpose { get; set; }

        //2.8
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
                address.AppendFormat("เลขที่ {0}", OperationAddress);

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
    
        //2.9
        public DateTime? StartDate { get; set; }
        public string StartDateThaiFormat { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndDateThaiFormat { get; set; }
        public decimal? TotalDay { get; set; }
        public string TotalDayThaiFormat { get; set; }
        public string TimeDesc { get; set; }

        //2.10
        public string Method { get; set; }

        //2.11
        public decimal? BudgetValue { get; set; }
        public string BudgetValueThaiFormat { get; set; }
        public string BudgetValueText { get; set; }

        public bool BudgetFromOtherFlag { get; set; }

        private string _budgetFromOtherName;
        public string BudgetFromOtherName { 
            get {
                string text = (String.IsNullOrEmpty(_budgetFromOtherName))? "-" : _budgetFromOtherName;
                
                return text;
            }
            set { _budgetFromOtherName = value; }
        }
        public decimal? BudgetFromOtherAmount { get; set; }        

        //2.12
        public string ProjectKPI { get; set; }

        //2.13
        public string ProjectResult { get; set; }

        //ความเห็นประกอบการพิจารณา
        private string _assessmentDesc;
        public string AssessmentDesc
        {
            get
            {
                string text = (String.IsNullOrEmpty(_assessmentDesc)) ? "-" : _assessmentDesc;
               
                return text;
            }
            set { _assessmentDesc = value; }
        }

        public string ProjectTypeCode { get; set; }
        
    }

    public class ProjectCommittee
    {
        public int? No { get; set; }
          
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName
        {
            get
            {
                string name = FirstName;
                if (!String.IsNullOrEmpty(LastName))
                {
                    name = String.Format("{0} {1}", name, LastName);
                }
                return name;
            }
        }

        public String Position { get; set; }
    }

    public class OrganizationAssistance
    {
        public int No { get; set; }
        public string OrganizationName { get; set; }
        public decimal? Amount { get; set; }
        public string AmountThaiFormat { get; set; }
    }

    public class ProjectBudget
    {
 
        public decimal ProjectBudgetID { get; set; }
        public int No { get; set; }
        public string ActivityName { get; set; }
        public string BudgetDetail { get; set; }
        public decimal BudgetValue { get; set; }
        public string Equal { get; set; }
        public string Baht { get; set; }
    }

    public class ProjectAttachment
    {        
        public bool HasAttachment { get; set; }
        public string ProjectAttachmentName { get; set; }
    }

    public class ProjectTargetGroup
    {        
        public decimal ProjectTargetGroupID { get; set; }
        public string TargetGroupName { get; set; }
        public string TargetGroupEtc { get; set; }
        public decimal TargetGroupAmt { get; set; }
        public decimal? Male { get; set; }
        public decimal? Female { get; set; }
    }

    public class ProjectOperationAddress
    {   
        public decimal ProjectID { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }       
        public string SubDistrict { get; set; }        
        public string District { get; set; }       
        public string Province { get; set; }
        public string FullAddress
        {
            get
            {
                StringBuilder address = new StringBuilder();
                address.AppendFormat("เลขที่/สถานที่ดำเนินการ {0}", Address);

                if (!String.IsNullOrEmpty(Building))
                {
                    address.AppendFormat(" อาคาร{0}", Building);
                }

                if (!String.IsNullOrEmpty(Moo))
                {
                    address.AppendFormat(" หมู่{0}", Moo);
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

                if (!String.IsNullOrEmpty(Province))
                {
                    address.AppendFormat(" {0}", Province);
                }

                return address.ToString();
            }
        }
    }
}
