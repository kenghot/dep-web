using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Report
{
    public partial class ReportProjectTracking : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerProjectTracking, "EXCELOPENXML");
            //SuppressExportButton(ReportViewerProjectTracking, "WORDOPENXML");
        }

        private void BindReport()
        {

            string tmpTrackingID = Request.QueryString["trackingid"];
            string trackingTypecode = Request.QueryString["trackingtypecode"];

            decimal reportTrackingID = 0;

            Decimal.TryParse(tmpTrackingID, out reportTrackingID);
            String reportName = "";
            
            if (trackingTypecode == Common.LOVCode.Reporttrackingtype.หนังสือติดตามถึงจังหวัด)
            {
                var result = _service.GetReportOrgTracking(reportTrackingID);
                if (result.IsCompleted)
                {
                    // Create Report DataSource
                    List<ServiceModels.Report.ReportOrgTracking> dataSet = new List<ServiceModels.Report.ReportOrgTracking>();


                    dataSet.Add(result.Data);

                    var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                    dataset1.Value = dataSet;
                    ReportViewerProjectTracking.LocalReport.DataSources.Add(dataset1);

                    reportName = "ReportOrgTracking.rdlc";
                    ReportViewerProjectTracking.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile(reportName));
                    ReportViewerProjectTracking.DataBind();
                    ReportViewerProjectTracking.LocalReport.Refresh();
                    ReportViewerProjectTracking.Visible = true;
                }
                else
                {
                    ShowErrorMessage(result.Message);
                    ReportViewerProjectTracking.Visible = false;
                }


                

            }else{
                var result = _service.GetReportProvinceTracking(reportTrackingID);
                if (result.IsCompleted)
                {
                    // Create Report DataSource
                    List<ServiceModels.Report.ReportProvinceTracking> dataSet = new List<ServiceModels.Report.ReportProvinceTracking>();


                    dataSet.Add(result.Data);

                    var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                    dataset1.Value = dataSet;
                    ReportViewerProjectTracking.LocalReport.DataSources.Add(dataset1);

                    reportName = "ReportProvinceTracking.rdlc";
                    ReportViewerProjectTracking.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile(reportName));
                    ReportViewerProjectTracking.DataBind();
                    ReportViewerProjectTracking.LocalReport.Refresh();
                    ReportViewerProjectTracking.Visible = true;
                }
                else
                {
                    ShowErrorMessage(result.Message);
                    ReportViewerProjectTracking.Visible = false;
                }

            }
        }
    }
}