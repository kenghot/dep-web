using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nep.Project.ServiceModels;

namespace Nep.Project.Web.Report
{
    public partial class ReportInterrogate : Nep.Project.Web.Infra.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }

        private void BindReport()
        {
            ReportViewerInterrogate.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportInterrogate.rdlc"));

            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            dataset1.Value = GetTemp();
            ReportViewerInterrogate.LocalReport.DataSources.Add(dataset1);

            ReportViewerInterrogate.DataBind();
            ReportViewerInterrogate.LocalReport.Refresh();
            ReportViewerInterrogate.Visible = true;
        }

        private List<ServiceModels.Report.ReportInterrogate> GetTemp()
        {
            List<ServiceModels.Report.ReportInterrogate> list = new List<ServiceModels.Report.ReportInterrogate>();
            ServiceModels.Report.ReportInterrogate data = new ServiceModels.Report.ReportInterrogate();
            data.No = "พม ๐๗๐๒/";
            data.SendDate = "มิถุนายน ๒๕๕๘";
            data.SendTo = "นายกสมาคมคนตาบอดแห่งประเทศไทย";
            data.ReferenceDocument = "๑.หนังสือกองบริหารกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ ที่ พม ๐๖๐๔/๑๓๒๔ ลงวันที่ ๖ มีนาคม ๒๕๕๗"
                                    + "<br />๒.หนังสือกองบริหารกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ ที่ พม ๐๖๐๔/๑๗๘๘ ลงวันที่ ๙ มิถุนายน ๒๕๕๗"
                                    + "<br />๓.หนังสือกองบริหารกองทุนส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการ ที่ พม ๐๖๐๔/๖๑๒๔ ลงวันที่ ๔ ธันวาคม ๒๕๕๗"
                                    + "<br />๔.แบบฟอร์มรายงานผลการปฏิบัติการงานโครงการ จำนวน ๑ ฉบับ";
            data.ResultRefDoc = "๑ - ๓";
            data.AmountProject = "๕";
            data.ProjectName = "๑.โครงการวิจัย เรื่อง การศึกษาปัจจัยที่สร้างภาวะผู้นำที่พึงประสงค์ของผู้พิการทางสายตาในชุมชน "
                                +"ของสมาคมคนตาบอดแห่งประเทศไทย พร้อมโอนเงินอุดหนุนเป็นเงินจำนวน ๒,๑๖๗,๐๐๐.- บาท (สองล้านหนึ่งแสนหกหมื่นเจ็ดพันบาทถ้วน)"
                                + "<br />๒.โครงการวิจัย เรื่อง ปัจจยที่ส่งผลต่อการเลือกใช้การบริการหมอนวดผู้พิการทางสายตาของสมาคมคนตาบอดแห่งประเทศไทย "
                                +"พร้อมโอนเงินอุดหนุนเป็นเงินจำนวน ๒,๓๑๐,๐๐๐.- บาท (สองล้านสามแสนหนึ่งหมื่นบาทถ้วน)"
                                + "<br />๓.โครงการฝึกอบรมนวดแผนไทยเพื่อการสร้างงานสร้างรายได้ให้แก่คนตาบอดในท้องถิ่นภาคใต้ตอนล่าง ของสมาคมคนตาบอดแห่งประเทศไทย"
                                +"ดำเนินการโดยสมาคมคนตาบอดแห่งประเทศไทยสาขาภาคใต้ตอนล่าง พร้อมโอนเงินอุดหนุนเป็นเงินจำนวน ๒๓๗,๓๐๐.- บาท (สองแสนสามหมื่นเจ็ดพันสามร้อยบาทถ้วน)"
                                + "<br />๔.โครงการสัมมนาเรื่อง " + '"' + "การรู้หนังสือกับการปฏิรูปประเทศไทย : โอกาสการพัฒนาคุณภาพชีวิตคนตาบอดอย่างยั่งยืน" + '"'
                                +"ของสมาคมคนตาบอดแห่งประเทศไทย พร้อมโอนเงินอุดหนุนเป็นเงินจำนวน ๓๗๘,๒๓๐ (สามแสนเจ็ดหมื่นแปดพันสองร้อยสามสิบบาทถ้วน)"
                                + "<br />๕.โครงการ " + '"' + "๕ ธันวา คนตาบอดนวดผู้สูงอายุ ทั่วประเทศถวายเป็นพระราชกุศลแด่สมเด็จพระเจ้าอยู่หัว" + '"'
                                +" ของสมาคมคนตาบอดแห่งประเทศไทย พร้อมโอนเงินอุดหนุนเป็นเงินจำนวน ๕๔๖,๓๐๐.- บาท (ห้าแสนสี่หมื่นหกพันสามร้อยบาทถ้วน)";
            data.Sign = "(นางณฐอร  อินทร์ดีศรี)";
            data.Position = "ผู้อำนวนการกองทุนและส่งเสริมความเสมอภาคคนพิการ";
            data.Address = "กองกองทุนและส่งเสริมความเสมอภาคคนพิการ"
                            +"<br />โทร. ๐ ๒๑๐๖ ๙๓๔๘"
                            +"<br />โทรสาร. ๐ ๒๑๐๖ ๘๓๕๑";
            list.Add(data);
            return list;
        }
    }
}