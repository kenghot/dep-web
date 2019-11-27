using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace Nep.Project.Web.Report
{
    public partial class Report3 : Nep.Project.Web.Infra.BasePrintPage
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
            ReportViewer3.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("Report3.rdlc"));

            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            dataset1.Value = GetTemp();
            ReportViewer3.LocalReport.DataSources.Add(dataset1);

            ReportViewer3.DataBind();
            ReportViewer3.LocalReport.Refresh();
            ReportViewer3.Visible = true;
        }

        private List<ServiceModels.Report.Report3> GetTemp()
        {
            List<ServiceModels.Report.Report3> list = new List<ServiceModels.Report.Report3>();
            ServiceModels.Report.Report3 data = new ServiceModels.Report.Report3();
            data.No = "พม ๐๗๐๒/๓๕๙๕";
            data.DocumentDate = "๑๓ กรกฏาคม ๒๕๕๘";
            data.To = "ประธานชมรมคนพิการตำบลสมอโคน";
            data.ReferenceNo = "พม ๐๖๐๔/๕๔๕๙";
            data.RefDocumentDate = "๖ ตุลาคม ๒๕๕๗";
            data.ResultTimeNo = "๑๑/๒๕๕๗";
            data.ResultDate = "๑๖ กันยายน ๒๕๕๗";
            data.ProjectName = "โครงการส่งเสริมความเข้มแข็งให้กับชมรมคนพิการตำบลสมอโคน";
            data.ProjectBy = "ชมรมคนพิการตำบลสมอโคน";
            data.Amount = "๔๔,๘๐๐";
            data.StringAmount = "(สี่หมื่นสี่พันแปดร้อยบาทถ้วน)";
            data.ProjectProvince = "ตาก";
            data.SendDocumentDate = "๒๐ กรกฏาคม ๒๕๕๘";
            list.Add(data);
            return list;
        }

        private String SetDateToString()
        {
            string dateToday = "";
            string strDay = DateTime.Now.Day.ToString();
            string strYear = DateTime.Now.ToString("yyyy", new CultureInfo("th-TH"));
            string strMonth = "";
            int month = DateTime.Now.Month;
            switch (month)
            {
                case 1:
                    strMonth = "มกราคม";
                    break;
                case 2:
                    strMonth = "กุมภาพันธ์";
                    break;
                case 3:
                    strMonth = "มีนาคม";
                    break;
                case 4:
                    strMonth = "เมษายน";
                    break;
                case 5:
                    strMonth = "พฤษภาคม";
                    break;
                case 6:
                    strMonth = "มิถุนายน";
                    break;
                case 7:
                    strMonth = "กรกฏาคม";
                    break;
                case 8:
                    strMonth = "สิงหาคม";
                    break;
                case 9:
                    strMonth = "กันยายน";
                    break;
                case 10:
                    strMonth = "ตุลาคม";
                    break;
                case 11:
                    strMonth = "พฤศจิกายน";
                    break;
                case 12:
                    strMonth = "ธันวาคม";
                    break;
                default:
                    break;
            }

            dateToday = strDay + " " + strMonth + " " + strYear;
            return dateToday;
        }
    }
}