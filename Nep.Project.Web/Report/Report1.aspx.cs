using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace Nep.Project.Web.Report
{
    public partial class Report1 : Nep.Project.Web.Infra.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }

        protected void BindReport()
        {
            ReportViewer1.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("Report1.rdlc"));

            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            dataset1.Value = GetTemp1();
            var dataset2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2");
            dataset2.Value = GetTemp2();
            ReportViewer1.LocalReport.DataSources.Add(dataset1);
            ReportViewer1.LocalReport.DataSources.Add(dataset2);

            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;
        }

        private List<ServiceModels.Report.Report1> GetTemp1()
        {
            List<ServiceModels.Report.Report1> list = new List<ServiceModels.Report.Report1>();
            ServiceModels.Report.Report1 data = new ServiceModels.Report.Report1();
            string strBenefits = "1. สตรีพิการและผู้ดูแล สามารถประกอบอาชีพได้จริง" +
                                 "<br />2. สตรีพิการและผู้ดูแล สามารถพัฒนาทักษะด้านอาชีพ เพื่อหารายได้และพึ่งพาตนเองได้" +
                                 "<br />3. สตรีพิการและผู้ดูแล มีรายได้เพิ่มขึ้น";

            string strIndicators = "1. ร้อยละของสตรีพิการและผู้ดูแล สามารถนำความรู้ไปประกอบอาชีพได้จริง" +
                                   "<br />2. ความสำเร็จในการส่งเสริมและพัฒนาทักษะการประกอบอาชีพ" +
                                   "<br />3. ร้อยละของสตรีพิการและผู้ดูแลคนพิการ มีรายได้เพิ่มขึ้น";

            data.ProjectName = "โครงการสมัชชาคนพิการแห่งชาติ  สมาคมคนพิการแห่งประเทศไทย  ครั้งที่ 1";
            data.ProjectSubject = "เรื่อง "+'"'+"การสร้างเสริมพลังอำนาจให้แก่คนพิการในการเข้าถึงสิทธิและใช้ประโยชน์ได้อย่างมีคุณภาพ" +'"';
            data.Responsible = "สมาคมคนพิการแห่งประเทศไทย";
            data.Principles = "จากพระราชบัญญัติส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ  พ.ศ.2550  มาตรา 23  กำหนดให้มีการจัดตั้งกองทุน"
                              + "<br />ส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ เพื่อเป็นทุนสำหรับการใช้จ่ายเกี่ยวกับการคุ้มครองและพัฒนาคุณภาพชีวิต"
                              + "<br />คนพิการ การส่งเสริมและการดำเนินงานด้านการสงเคราะห์ช่วยเหลือคนพิการ การฟื้นฟูสมรรถภาพคนพิการ การ"
                              + "<br />ศึกษาและการประกอบอาชีพของคนพิการ  รวมทั้งการส่งเสริมและสนับสนุนการดำเนินงานขององค์กรที่เกี่ยวข้องกับ"
                              + "<br />คนพิการ  ซึ่งการพัฒนาคุณภาพชีวิตคนพิการก็ได้มีการทำอย่างต่อเนื่องมาหลายปี  หลายรูปแบบทั้งภาครัฐ  เอกชน"
                              + "<br />องค์กรคนพิการ";
            data.Objective = "1. เพื่อให้คนพิการได้รับความรู้ได้เข้าถึงและใช้ประโยชน์ได้จากระบบบริการด้านการแพทย์ ด้านการศึกษา  และด้านสวัสดิการสังคม"
                             + "<br />2. เพื่อเสริมพลังคนพิการให้มีความพร้อม ความมั่นคง ความก้าวหน้าบนพื้นฐานของความเท่าเทียมในสังคม"
                             + "<br />3. เพื่อให้หน่วยงานที่เกี่ยวข้องได้พัฒนาและเพิ่มประสิทธิภาพในการบริการช่วยเหลือคนพิการและผู้เกี่ยวข้องโดยมี"
                             +"คนพิการ  ครอบครัว  ชุมชน  องค์กรและภาคีเครือข่ายเป็นศูนย์กลาง";
            data.Target = " - ผู้แทนคนพิการประจำจังหวัด  76  จังหวัด         จำนวน  400  คน"
                          +"<br /> - ผู้ช่วยเหลือคนพิการ (PA)                    จำนวน   40  คน"
                          + "<br /> - ที่ปรึกษาสมาคมคนพิการแห่งประเทศไทย           จำนวน    15  คน"
                          + "<br /> - กรรมการบริหารสมาคมคนพิการแห่งประเทศไทย      จำนวน    15  คน"
                          + "<br /> - เจ้าหน้าที่สมาคมคนพิการแห่งประเทศไทย          จำนวน    10  คน"
                          + "<br /> - อาสาสมัครเฉพาะกิจ                        จำนวน    20  คน"
                          + "<br />   รวมทั้งสิ้น  500  คน";
            data.Activity = "จัดสมัชชาคนพิการแห่งชาติ  สมาคมคนพิการแห่งประเทศไทย  โดยมีสาระสำคัญ  ดังนี้"
                           + "<br /> - ชี้แจงวัตถุประสงค์ของโครงการ"
                           + "<br /> - ปาฐกถาพิเศษเรื่อง" + '"' + " นโยบายของรัฐบาลเรื่องสิทธิของคนพิการตามกฎหมาย" + '"'
                           + "<br /> - บรรยายเรื่อง" + '"' + " การเข้าถึงสิทธิ และสิ่งอำนวยความสะดวกของคนพิการ" + '"'
                           + "<br /> - การสร้างเสริมพลังอำนาจให้แก่คนพิการในการเข้าถึงสิทธิและใช้ประโยชน์ได้อย่างมีคุณภาพ";
            data.Lecturer = "1. นายกและคณะกรรมการบริหารสมาคมคนพิการแห่งประเทศไทย"
                            + "<br />2. วิทยากรจาก สนง.ส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการแห่งชาติ";
            data.Period = "ระหว่างวันที่  27 - 29  มีนาคม  2555";
            data.Place = "โรงแรมเดอะริช  จังหวัดนนทบุรี";
            data.Benefits = strBenefits;
            data.Indications = strIndicators;
            data.TotalExpenses = 268500;
            data.TotalRequired = 268500;
            data.Resolution = ".......";
            list.Add(data);

            return list;
        }

        private List<ServiceModels.Report.Report1Detail> GetTemp2()
        {
            List<ServiceModels.Report.Report1Detail> list = new List<ServiceModels.Report.Report1Detail>();
            List<ServiceModels.GenericDropDownListData> listGroup = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData g1 = new ServiceModels.GenericDropDownListData();
            g1.Text = "เสนอขอ";
            g1.Value = "1";
            listGroup.Add(g1);

            ServiceModels.GenericDropDownListData g2 = new ServiceModels.GenericDropDownListData();
            g2.Text = "ฝ่ายเลขานุการ";
            g2.Value = "2";
            listGroup.Add(g2);

            ServiceModels.GenericDropDownListData g3 = new ServiceModels.GenericDropDownListData();
            g3.Text = "คณะทำงานพิจารณากลั่นกรองโครงการ";
            g3.Value = "3";
            listGroup.Add(g3);

            Random rand = new Random();
            foreach (var item in listGroup)
            {
                for (int i = 1; i < 7; i++)
                {
                    ServiceModels.Report.Report1Detail data = new ServiceModels.Report.Report1Detail();
                    data.GroupOrder = Convert.ToInt32(item.Value);
                    data.GroupName = item.Text;
                    data.Expenses = GetExpensesName(i);
                    if (item.Text != "คณะทำงานพิจารณากลั่นกรองโครงการ")
                        data.Amount = rand.Next(10000, 90000);

                    if(i == 1 && item.Text == "เสนอขอ")
                        data.Remark = "วิทยากรบรรยาย <br />(1 ชม x 600 บาท x 7 รุ่น = 4,200.- บาท)";

                    list.Add(data);
                }
            }

            return list;
        }

        private string GetExpensesName(int no)
        {
            string str = "";
            switch (no)
            {
                case 1: 
                    str = "1) ค่าตอบแทนวิทยากร <br />(600 บาท x 4.5 ชั่วโมง x 7 รุ่น)";
                    break;
                case 2:
                    str = "2) ค่าพาหนะเดินทาง (ไป-กลับ) <br />(7 รุ่น x 50 คน x 200 บาท)";
                    break;
                case 3:
                    str = "3) ค่าอาหารกลางวัน (7 รุ่น x 50 คน x 120 บาท)";
                    break;
                case 4:
                    str = "4) ค่าอาหารว่างและเครื่องดื่ม (7 รุ่น x 50 คน x 35 บาท x 2 มื้อ)";
                    break;
                case 5:
                    str = "5) ค่าวัสดุประกอบการประชุม <br /> - กลุ่มอาชีพจักสาน/งานฝีมือ<br /> - กลุ่มอาชีพซ่อมมอเตอร์ไซค์/อุปกรณ์อิเล็กทรอนิคส์<br /> - กลุ่มอาชีพเกษตรกรรม<br /> - ค่าป้ายไวนิล";
                    break;
                case 6:
                    str = "6) ค่าถ่ายเอกสาร (7 รุ่น x 50 คน x 50 บาท)";
                    break;
                default:
                    break;
            }

            return str;
        }
    }
}