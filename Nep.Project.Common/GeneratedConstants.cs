using System;

namespace Nep.Project.Common
{
	public static class TemplateCode
    {
       
        public const String ORG_RETRACKING = "ORG_RETRACKING";
       
        public const String PROVINCE_RETRACKING = "PROVINCE_RETRACKING";
       
        public const String ORG_TRACKING = "ORG_TRACKING";
       
        public const String PROVINCE_TRACKING = "PROVINCE_TRACKING";
    }

	public static class OrganizationTypeID
    {
        /// <summary>
        /// function name : สังกัดกรม      
        /// </summary>
        public const Decimal สังกัดกรม = 1;
        /// <summary>
        /// function name : กระทรวง      
        /// </summary>
        public const Decimal กระทรวง = 2;
        /// <summary>
        /// function name : องค์กรปกครองส่วนท้องถิ่น      
        /// </summary>
        public const Decimal องค์กรปกครองส่วนท้องถิ่น = 3;
        /// <summary>
        /// function name : องค์กรด้านคนพิการ      
        /// </summary>
        public const Decimal องค์กรด้านคนพิการ = 4;
        /// <summary>
        /// function name : องค์กรชุมชน      
        /// </summary>
        public const Decimal องค์กรชุมชน = 5;
        /// <summary>
        /// function name : องค์กรธุรกิจ      
        /// </summary>
        public const Decimal องค์กรธุรกิจ = 6;
        /// <summary>
        /// function name : อื่นๆ      
        /// </summary>
        public const Decimal อื่นๆ = 7;
    }

	public static class UserGroupCode
    {
        
        public const String เจ้าหน้าที่จังหวัด = "G1";
        
        public const String องค์กรภายนอก = "G2";
        
        public const String กลุ่มอนุมัติโครงการ = "G3";
        
        public const String กลุ่มติดตามโครงการ = "G4";
        
        public const String กลุ่มผู้บริหาร = "G5";
        
        public const String ผู้ดูแลระบบ = "G6";
    }

    public static class FunctionCode
    {
        /// <summary>
        /// function name : ViewProject
        /// description : ดูข้อมูลโครงการทั้งหมดได้ แต่ไม่สามารถเพิ่ม ลบ แก้ไข ได้
        /// </summary>
        public const String VIEW_PROJECT = "11";
        /// <summary>
        /// function name : ManageProject
        /// description : เพิ่ม ลบ แก้ไข ข้อมูลโครงการได้ทั้งหมด ยกเว้น การติดตามโครงการ
        /// </summary>
        public const String MANAGE_PROJECT = "12";
        /// <summary>
        /// function name : RequestProject
        /// description : เพิ่ม ลบ แก้ไข ข้อมูลโครงการได้เฉพาะ ข้อมูลทั่วไป ข้อมูลโครงการ บุคลากร งบประมาณ เอกสารแนบ
        /// </summary>
        public const String REQUEST_PROJECT = "13";
        /// <summary>
        /// function name : TrackProject
        /// description : เพิ่ม ลบ แก้ไข ข้อมูลการติดตามโครงการ
        /// </summary>
        public const String TRACK_PROJECT = "14";
        /// <summary>
        /// function name : ViewApprovingReport
        /// description : ดูข้อมูลรายงานที่เกี่ยวกับการอนุมัติ
        /// </summary>
        public const String VIEW_APPROVING_REPORT = "21";
        /// <summary>
        /// function name : ViewTrackingingReport
        /// description : ดูข้อมูลรายงานที่เกี่ยวกับการติดตามโครงการ
        /// </summary>
        public const String VIEW_TRACKINGING_REPORT = "22";
        /// <summary>
        /// function name : ViewRedundancyReport
        /// description : ดูข้อมูลรายงานที่เกี่ยวกับซ้ำซ้อนผู้รับบริการ
        /// </summary>
        public const String VIEW_REDUNDANCY_REPORT = "23";
        /// <summary>
        /// function name : ManageUser
        /// description : เพิ่ม ลบ แก้ไขข้อมูลผู้ใช้งาน
        /// </summary>
        public const String MANAGE_USER = "31";
        /// <summary>
        /// function name : ManageOrganization
        /// description : จัดการข้อมูลองค์กร
        /// </summary>
        public const String MANAGE_ORGANIZATION = "41";
    }

    public static class OrganizationParameterCode
    {
        public const String AttachFilePath = "AttachFilePath";
        public const String BANGKOK_PROVINCE_ABBR = "BANGKOK_PROVINCE_ABBR";
        public const String CENTER_PROVINCE_ABBR = "CENTER_PROVINCE_ABBR";
        public const String FOLLOWUP_CONTACT = "FOLLOWUP_CONTACT";
        public const String MAIL_AUTH = "MAIL_AUTH";
        public const String MAIL_PASS = "MAIL_PASS";
        public const String MAIL_PORT = "MAIL_PORT";
        public const String MAIL_SERVER = "MAIL_SERVER";
        public const String MAIL_SSL = "MAIL_SSL";
        public const String MAIL_USER = "MAIL_USER";
        public const String NEP_ADDRESS = "NEP_ADDRESS";
        public const String NEP_DIRECTOR_GENERAL_NAME = "NEP_DIRECTOR_GENERAL_NAME";
        public const String NEP_DIRECTOR_GENERAL_POS = "NEP_DIRECTOR_GENERAL_POS";
        public const String NEP_PROJECT_DIRECTOR = "NEP_PROJECT_DIRECTOR";
        public const String NEP_PROJECT_DIRECTOR_POSITION = "NEP_PROJECT_DIRECTOR_POSITION";
        public const String OGR_NAME = "OGR_NAME";
        public const String PREFIX_DOC_NO_0702 = "PREFIX_DOC_NO_0702";
    }

    public static class LOVGroup
    {
        public const String ApprovalStatus1 = "ApprovalStatus1";
        public const String ApprovalStatus2 = "ApprovalStatus2";
        public const String AttachmentType = "AttachmentType";
        public const String Bank = "Bank";
        public const String BudgetType = "BudgetType";
        public const String DisabilityCommittee = "DisabilityCommittee";
        public const String DisabilityType = "DisabilityType";
        public const String EvaluationStatus = "EvaluationStatus";
        public const String FollowupStatus = "FollowupStatus";
        public const String LogAccess = "LogAccess";
        public const String OperationLevel = "OperationLevel";
        public const String OperationResult = "OperationResult";
        public const String Prefix = "Prefix";
        public const String ProjectApprovalStatus = "ProjectApprovalStatus";
        public const String ProjectAttachment = "ProjectAttachment";
        public const String ProjectType = "ProjectType";
        public const String ReportTrackingType = "ReportTrackingType";
        public const String Section = "Section";
        public const String TargetGroup = "TargetGroup";
    }

    public static class LOVCode
    {

        public static class Approvalstatus1
        {
            public const String อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน = "1";
            public const String อนุมัติ_ปรับลดวงเงินสนับสนุน = "2";
            public const String ไม่อนุมัติ = "3";
            public const String ชะลอการพิจารณา = "4";
            public const String ยกเลิก = "5";
            public const String อื่นๆ = "6";
        }

        public static class Approvalstatus2
        {
            public const String อนุมัติ_ตามวงเงินที่โครงการขอสนับสนุน = "1";
            public const String อนุมัติ_ปรับลดวงเงินสนับสนุน = "2";
            public const String ไม่อนุมัติ = "3";
            public const String ชะลอการพิจารณา = "4";
            public const String ยกเลิก = "5";
            public const String อื่นๆ = "6";
        }

        public static class Attachmenttype
        {
            public const String PROJECT_PERSONAL = "1";
            public const String PROJECT_CONTRACT = "10";
            public const String PROJECT_OPERATION = "2";
            public const String PROJECT_ATTACHMENT = "3";
            public const String PERSONAL_ID_CARD = "4";
            public const String EMPLOYEE_ID_CARD = "5";
            public const String PROJECT_FOLLOWUP = "6";
            public const String PROJECT_REPORT = "7";
            public const String PRING_REPORT_TRACKING = "8";
            public const String PROJECT_INFORMATION = "9";
        }

        public static class Bank
        {
            public const String ธนาคารแห่งประเทศไทย = "001";
            public const String ธนาคารกรุงเทพ_จำกัด__มหาชน = "002";
            public const String ธนาคารกสิกรไทย_จำกัด__มหาชน = "004";
            public const String ธนาคารเอบีเอ็น_แอมโร_เอ็น_วี = "005";
            public const String ธนาคารกรุงไทย_จำกัด__มหาชน = "006";
            public const String ธนาคารเจพีมอร์แกน_เชส = "008";
        }

        public static class Budgettype
        {
            public const String คณะสติปัญญาและการเรียนรู้ = "1";
            public const String บริหารจัดการองค์การคนพิการแต่ละประเภท_ = "10";
            public const String วันคนพิการสากล = "11";
            public const String แผนพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด = "12";
            public const String การดำเนินงานของ_อปท___MATCHING_FUND = "13";
            public const String การเข้าถึงสิทธิของคนพิการ_ = "14";
            public const String เทคโนโลยีสารสนเทศ_การวิจัย_และนวัตกรรม = "15";
            public const String สมัชชาคนพิการจังหวัด = "16";
            public const String เสริมสร้างเจตคติ = "17";
            public const String การจัดสิ่งอำนวยความสะดวกในสถานประกอบการ = "18";
            public const String โครงการบูรณาระบบการส่งเสริมอาชีพและการมีงานทำ = "19";
            public const String คณะเคลื่อนไหวหรือทางร่างกาย = "2";
            public const String สนับสนุนศูนย์บริการคนพิการ = "20";
            public const String การขับเคลื่อนการพัฒนาสตรีพิการ = "21";
            public const String การจัดประชุมคณะกรรมการจังหวัด = "22";
            public const String การบริหารจัดการภัยพิบัติ = "23";
            public const String การรณรงค์หาเสียงเลือกตั้งผู้แทนไทย = "24";
            public const String กรอบวงเงินสนับสนุนโครงการตามแผนพัฒนาคุณภาพชีวิตคนพิการคนพิการประจำจังหวัด = "25";
            public const String กรอบวงเงินสนับการดำเนินงานของ_อปท_ = "26";
            public const String กรอบวงเงินวันคนพิการสากลปี_2559_ส่วนภูมิภาค = "27";
            public const String กรอบวงเงินสนับสนุนการขับเคลื่อนยุทธศาสตร์การพัฒนาสตรีพิการ_ = "28";
            public const String กรอบวงเงินการจัดทำแผนพัฒนาคุณภาพชีวิตคนพิการจังหวัด_ฉบับที่_3__พ_ศ__2560___2564_จำนวน = "29";
            public const String คณะการได้ยินหรือสื่อความหมาย = "3";
            public const String กรอบวงเงินสนับสนุนจังหวัดในการประชุมเตรียมความพร้อมและซักซ้อมคนพิการในสถานการณ์ภัยพิบัติ = "30";
            public const String กรอบวงเงินสนับสนุนศูนย์บริการคนพิการ = "31";
            public const String กรอบวงเงินปรับปรุงเช่าหรือก่อสร้างสำนักงานศูนย์บริการคนพิการระดับจังหวัด = "32";
            public const String กรอบสนับสนุนโครงการตามแผนยุทธศาสตร์มุ่งเป้าภายใต้คณะอนุกรรมการนโยบาย_แผนงาน_และมาตรการกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ = "33";
            public const String คณะออทิสติก = "4";
            public const String คณะจิตใจหรือพฤติกรรม = "5";
            public const String คณะการเห็น = "6";
            public const String คณะทำงานส่วนกลาง = "7";
            public const String คณะทำงาน_พก_ = "8";
            public const String บริหารจัดการสมาคมสภาฯ = "9";
        }

        public static class Disabilitycommittee
        {
            public const String คณะอนุกรรมการบุคคลพิการทางการเห็น = "1";
            public const String คณะอนุกรรมการบุคคลพิการทางการได้ยินหรือสื่อความหมาย = "2";
            public const String คณะอนุกรรมการบุคคลพิการทางกายหรือการเคลื่อนไหว = "3";
            public const String คณะอนุกรรมการบุคคลพิการทางจิตใจหรือพฤติกรรม = "4";
            public const String คณะอนุกรรมการบุคคลพิการทางสติปัญญา = "5";
            public const String คณะอนุกรรมการบุคคลพิการทางการเรียนรู้ = "6";
            public const String คณะอนุกรรมการบุคคลออทิสติ = "7";
            public const String คณะอนุกรรมการบุคคลพิการทุกประเภท = "8";
            public const String คณะกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด = "9";
        }

        public static class Disabilitytype
        {
            public const String ประเภททางการเห็น = "1";
            public const String ประเภททางการได้ยินหรือสื่อความหมาย = "2";
            public const String ประเภททางการเคลื่อนไหวหรือร่างกาย = "3";
            public const String ประเภททางจิตใจหรือพฤติกรรม = "4";
            public const String ประเภททางสติปัญญา = "5";
            public const String ประเภททางการเรียนรู้ = "6";
            public const String ประเภททางออทิสติก = "7";
            public const String ทุกประเภทความพิการ = "8";
        }

        public static class Evaluationstatus
        {
            public const String ผ่าน = "1";
            public const String ไม่ผ่าน = "2";
        }

        public static class Followupstatus
        {
            public const String ถึงกำหนดติดตาม_30_วัน = "1";
            public const String ถึงกำหนดติดตาม_45_วัน = "2";
            public const String รายงานผลแล้ว = "3";
            public const String กำลังติดตาม = "4";
        }

        public static class Logaccess
        {
            public const String LOGIN = "1";
            public const String รายการโครงการ = "2";
            public const String ลงทะเบียนผู้ใช้งาน = "3";
            public const String ลงทะเบียนหน่วยงาน = "4";
            public const String เปลี่ยนรหัสผ่าน = "5";
            public const String ยืนยันการลงทะเบียน = "6";
            public const String รายละเอียดโครงการ = "7";
        }

        public static class Operationlevel
        {
            public const String สูงกว่าเป้าหมาย_เพราะ = "1";
            public const String ตามเป้าหมาย__ร้อยละ_๑๐๐ = "2";
            public const String ร้อยละ_๖๐___ร้อยละ_๙๙_ของเป้าหมาย = "3";
            public const String ต่ำกว่าร้อยละ_๖๐_ของเป้าหมาย = "4";
        }

        public static class Operationresult
        {
            public const String ผลการดำเนินงานเป็นไปตามวัตถุประสงค์__ร้อยละ_๑๐๐ = "1";
            public const String ผลการดำเนินงานเป็นไปตามวัตถุประสงค์_ร้อยละ_๖๐___ร้อยละ_๙๙ = "2";
            public const String ผลการดำเนินงานเป็นไปตามวัตถุประสงค์_ต่ำกว่าร้อยละ_๖๐ = "3";
        }

        public static class Prefix
        {
            public const String นาย = "1";
            public const String นาง = "2";
            public const String นางสาว = "3";
            public const String อื่นๆ = "4";
        }

        public static class Projectapprovalstatus
        {
            public const String ร่างเอกสาร = "0";
            public const String ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ = "1";
            public const String ยกเลิกคำร้อง = "10";
            public const String ขั้นตอนที่_3_1_ปรับปรุง = "11";
            public const String ขั้นตอนที่_4_1_ปรับปรุง = "12";
            public const String ขั้นตอนที่_5_1_ปรับปรุง = "13";
            public const String ขั้นตอนที่_3_1_ชะลอการพิจารณา = "14";
            public const String ขั้นตอนที่_4_1_ชะลอการพิจารณา = "15";
            public const String ขั้นตอนที่_5_1_ชะลอการพิจารณา = "16";
            public const String ยืนยันการปรับปรุง = "17";
            public const String ขั้นตอน_6_1_รอโอนเงิน = "18";
            public const String ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน = "2";
            public const String ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด = "3";
            public const String ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง = "4";
            public const String ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน = "5";
            public const String ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว = "6";
            public const String ยกเลิกสัญญา = "7";
            public const String ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง = "8";
            public const String ยกเลิกโดยคณะกรรมการกลั่นกรอง = "81";
            public const String อื่นๆ_โดยคณะกรรมการกลั่นกรอง = "82";
            public const String ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด = "9";
            public const String ยกเลิกโดยอนุกรรมการกองทุนหรือจังหวัด = "91";
            public const String อื่นๆ_โดยอนุกรรมการกองทุนหรือจังหวัด = "92";
        }

        public static class Projectattachment
        {
            public const String โครงการตามแบบฟอร์มเสนอโครงการ_จำนวน_๑_ชุด = "1";
            public const String แผนผังของพื้นที่ดำเนินการ = "10";
            public const String หนังสือรับรององค์กร__กรณีไม่เป็นองค์กรนิติบุคคล = "11";
            public const String หนังสื่อรับรองการมีส่วนร่วม__กรณีเป็นโครงการตามแผนพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด = "12";
            public const String รายการการประชุมคณะอนุกรรมการส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด___กรณีเป็นโครงการตามแผนพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด = "13";
            public const String อื่นๆ__ที่เป็นประโยชน์ต่อการพิจารณาโครงการ = "14";
            public const String รายชื่อคณะกรรมการบริหารองค์กรชุดปัจจุบัน = "2";
            public const String สำเนาใบอนุญาติจัดตั้ง_และระเบียบหรือข้อบังคับองค์กร = "3";
            public const String รายงานผลการดำเนินงานในรอบปีที่ผ่านมาอย่างคร่าวๆ = "4";
            public const String งบดุล_งบแสดงรายรับ___รายจ่ายขององค์กร = "5";
            public const String โครงการทีมงานในการบริการจัดการโครงการ = "6";
            public const String รายชื่อผู้เข้าร่วมโครงการ_หรือรายชื่อกลุ่มเป้าหมาย = "7";
            public const String ร่างกำหนดการโครงการ = "8";
            public const String แผนผังที่ตั้งองค์กร = "9";
        }

        public static class Projecttype
        {
            public const String การอบรมให้ความรู้ = "1";
            public const String การติดตามประเมินผล = "10";
            public const String ส่งเสริมมาตราฐานองค์กร = "11";
            public const String ค่าตอบแทนล่ามภาษามือ = "12";
            public const String ผลิตสื่อโฆษณา = "13";
            public const String ประชุมอนุกรรมการ_ฯ_ประจำจังหวัด = "14";
            public const String การเข้าสิทธิคนพิการ = "15";
            public const String เงินอุดหนุนเพื่อช่วยเหลือคนพิการ = "16";
            public const String การฝึกอาชีพ = "2";
            public const String การส่งเสริมการมีงานทำ = "3";
            public const String การประชาสัมพันธ์ = "4";
            public const String ด้านสตรีพิการ = "5";
            public const String สิ่งอำนวยความสะดวก___ปรับสภาพแวดล้อมฯ = "6";
            public const String การวิจัย_นวัตกรรม = "7";
            public const String ศุนย์บริการคนพิการ = "8";
            public const String ค่าตอบแทนผู้ช่วยคนพิการ = "9";
        }

        public static class Reporttrackingtype
        {
            public const String หนังสือติดตามถึงองค์กร = "1";
            public const String หนังสือติดตามถึงจังหวัด = "2";
        }

        public static class Section
        {
            public const String ส่วนกลาง = "1";
            public const String ภาคกลางและตะวันออก = "2";
            public const String ภาคตะวันออกเฉียงเหนือ = "3";
            public const String ภาคใต้ = "4";
            public const String ภาคเหนือ = "5";
        }

        public static class Targetgroup
        {
            public const String คนพิการทางการเห็น = "1";
            public const String คนพิการทางการได้ยินหรือสื่อความหมาย = "2";
            public const String คนพิการทางการเคลื่อนไหวหรือร่างกาย = "3";
            public const String คนพิการทางจิตใจหรือพฤติกรรม = "4";
            public const String คนพิการทางสติปัญญา = "5";
            public const String คนพิการทางการเรียนรู้ = "6";
            public const String ออทิสติก = "7";
            public const String ทุกประเภทความพิการ = "8";
            public const String อื่นๆ = "9";
        }
    }
}
