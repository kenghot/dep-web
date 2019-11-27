using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Nep.Project.Web.Report
{
    public partial class ReportBudgetApplicant : Nep.Project.Web.Infra.BasePage
    {
        private const String FileName = "รายงานผู้ขอรับเงินสนับสนุนโครงการ.xls";
        private const int FirstIndexOfRow = 3;
        private const string FormulaString = "SUM({0})";

        public IServices.IReportBudgetApplicantService ReportBudgetApplicantService { get; set; }
        public IServices.IProjectInfoService ProjectService { get; set; }
        public IServices.IListOfValueService LovService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            List<ServiceModels.ListOfValue> sectionList = LovService.ListAll(Common.LOVGroup.Section).Data;
            ViewState["SectionN"] = sectionList.Where(x => x.LovCode == Common.LOVCode.Section.ภาคเหนือ).Select(y => y.LovName).FirstOrDefault();
            ViewState["SectionE"] = sectionList.Where(x => x.LovCode == Common.LOVCode.Section.ภาคตะวันออกเฉียงเหนือ).Select(y => y.LovName).FirstOrDefault();
            ViewState["SectionS"] = sectionList.Where(x => x.LovCode == Common.LOVCode.Section.ภาคใต้).Select(y => y.LovName).FirstOrDefault();
            ViewState["SectionCW"] = sectionList.Where(x => x.LovCode == Common.LOVCode.Section.ภาคกลางและตะวันออก).Select(y => y.LovName).FirstOrDefault();
            ViewState["SectionC"] = sectionList.Where(x => x.LovCode == Common.LOVCode.Section.ส่วนกลาง).Select(y => y.LovName).FirstOrDefault();
            
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TabPanelCentral.HeaderText = ViewState["SectionC"].ToString();
            TabPanelCentralAndWestern.HeaderText = ViewState["SectionCW"].ToString();
            TabPanelNorthern.HeaderText = ViewState["SectionN"].ToString();
            TabPanelNortheast.HeaderText = ViewState["SectionE"].ToString();
            TabPanelSouthern.HeaderText = ViewState["SectionS"].ToString();


            if (!IsPostBack)
            {
                ReportViewerBudgetApplicant_Central.ShowExportControls = false;
                ReportViewerBudgetApplicant_CentralAndWestern.ShowExportControls = false;
                ReportViewerBudgetApplicant_Northern.ShowExportControls = false;
                ReportViewerBudgetApplicant_Northeast.ShowExportControls = false;
                ReportViewerBudgetApplicant_Southern.ShowExportControls = false;
            }
        }

        public List<ServiceModels.GenericDropDownListData> ComboBoxApprovalStatus_GetData()
        {
            var list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownSelect });
            list.Add(new ServiceModels.GenericDropDownListData { Value = "1", Text = "อนุมัติ" });
            list.Add(new ServiceModels.GenericDropDownListData { Value = "0", Text = "ไม่อนุมัติ" });
            list.Add(new ServiceModels.GenericDropDownListData { Value = "null", Text = "ปรับปรุง" });
            return list;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindReport();
            }
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var reportValue = GetReportValue();

                ExportExcel(Page.Response, reportValue);
            }
        }

        protected void BindReport()
        {
            ReportViewerBudgetApplicant_Central.LocalReport.DataSources.Clear();
            ReportViewerBudgetApplicant_CentralAndWestern.LocalReport.DataSources.Clear();
            ReportViewerBudgetApplicant_Northern.LocalReport.DataSources.Clear();
            ReportViewerBudgetApplicant_Northeast.LocalReport.DataSources.Clear();
            ReportViewerBudgetApplicant_Southern.LocalReport.DataSources.Clear();

            var reportValue = GetReportValue();


            //var central = reportValue["1"].Union(reportValue["2"]).ToList();
            var central = reportValue["1"].ToList();
            var centralAndWwestern = reportValue["2"].ToList();
            var northern = reportValue["5"].ToList();
            var northeast = reportValue["3"].ToList();
            var southern = reportValue["4"].ToList();

            ReportViewerBudgetApplicant_Central.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetApplicant.rdlc"));
            ReportViewerBudgetApplicant_CentralAndWestern.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetApplicant.rdlc"));
            ReportViewerBudgetApplicant_Northern.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetApplicant.rdlc"));
            ReportViewerBudgetApplicant_Northeast.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetApplicant.rdlc"));
            ReportViewerBudgetApplicant_Southern.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetApplicant.rdlc"));

            if (central.Count > 0)
            {
                ReportViewerBudgetApplicant_Central.LocalReport.DataSources.Add(
                    new Microsoft.Reporting.WebForms.ReportDataSource("ReportBudgetApplicantDataSet") { Value = central });
                ReportViewerBudgetApplicant_Central.DataBind();
                ReportViewerBudgetApplicant_Central.Visible = true;
                TabPanelCentral.Visible = true;
            }
            else
            {
                ReportViewerBudgetApplicant_Central.Visible = false;
                TabPanelCentral.Visible = false;
            }

            if (centralAndWwestern.Count > 0)
            {
                ReportViewerBudgetApplicant_CentralAndWestern.LocalReport.DataSources.Add(
                    new Microsoft.Reporting.WebForms.ReportDataSource("ReportBudgetApplicantDataSet") { Value = centralAndWwestern });
                ReportViewerBudgetApplicant_CentralAndWestern.DataBind();
                ReportViewerBudgetApplicant_CentralAndWestern.Visible = true;
                TabPanelCentralAndWestern.Visible = true;
            }
            else
            {
                ReportViewerBudgetApplicant_CentralAndWestern.Visible = false;
                TabPanelCentralAndWestern.Visible = false;
            }

            //
            if (northern.Count > 0)
            {
                ReportViewerBudgetApplicant_Northern.LocalReport.DataSources.Add(
                    new Microsoft.Reporting.WebForms.ReportDataSource("ReportBudgetApplicantDataSet") { Value = northern });
                ReportViewerBudgetApplicant_Northern.DataBind();
                ReportViewerBudgetApplicant_Northern.Visible = true;
                TabPanelNorthern.Visible = true;
            }
            else
            {
                ReportViewerBudgetApplicant_Northern.Visible = false;
                TabPanelNorthern.Visible = false;
            }

            //
            if (northeast.Count > 0)
            {
                ReportViewerBudgetApplicant_Northeast.LocalReport.DataSources.Add(
                    new Microsoft.Reporting.WebForms.ReportDataSource("ReportBudgetApplicantDataSet") { Value = northeast });
                ReportViewerBudgetApplicant_Northeast.DataBind();
                ReportViewerBudgetApplicant_Northeast.Visible = true;
                TabPanelNortheast.Visible = true;
            }
            else
            {
                ReportViewerBudgetApplicant_Northeast.Visible = false;
                TabPanelNortheast.Visible = false;
            }

            //
            if (southern.Count > 0)
            {
                ReportViewerBudgetApplicant_Southern.LocalReport.DataSources.Add(
                    new Microsoft.Reporting.WebForms.ReportDataSource("ReportBudgetApplicantDataSet") { Value = southern });
                ReportViewerBudgetApplicant_Southern.DataBind();
                ReportViewerBudgetApplicant_Southern.Visible = true;
                TabPanelSouthern.Visible = true;
            }
            else
            {
                ReportViewerBudgetApplicant_Southern.Visible = false;
                TabPanelSouthern.Visible = false;
            }
        }

        private ILookup<string, ServiceModels.Report.ReportBudgetApplicant> GetReportValue()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
            List<ServiceModels.Report.ReportBudgetApplicant> list = GetTemp();

            list.All(x =>
            {
                x.No = x.No ?? "-";
                x.ProjectNo = x.ProjectNo ?? "-";
                x.BudgetYear = x.BudgetYear + 543;
                x.OrganizationType = x.OrganizationType ?? "-";
                x.Organization = x.Organization ?? "-";
                x.OrganizationSupport = x.OrganizationSupport ?? "-";
                x.ProjectName = x.ProjectName ?? "-";
                x.ProjectType = x.ProjectType ?? "-";
                x.Section = x.Section ?? "-";
                x.Province = x.Province ?? "-";
                x.ProjectDateStr = x.ProjectDate.HasValue ? x.ProjectDate.Value.ToString("d MMM yy", format) : "-";
                //RequestBudget
                x.ApprovalBudgetType = x.ApprovalBudgetType ?? "-";
                //SumTargetGroupAmount
                //Approval1
                //Approval3
                //Approval2
                //Approval_
                //ApprovalStatus0
                //ApprovalStatus1
                //ApprovalStatus_ 
                x.ApprovalNo = x.ApprovalNo ?? "-";
                x.ApprovalDate2Str = x.ApprovalDate2.HasValue ? x.ApprovalDate2.Value.ToString("d MMM yy", format) : "-";
                x.ProvinceAbbr = x.ProvinceAbbr ?? "-";
                //BudgetReviseValue
                x.ContractDateStr = (x.ContractStartDate.HasValue
                    ? x.ContractStartDate.Value.ToString("d MMM yy", format)
                    : string.Empty) + " - "
                                    +
                                    (x.ContractEndDate.HasValue
                                        ? x.ContractEndDate.Value.ToString("d MMM yy", format)
                                        : string.Empty);
                //ContractStartDate
                //ContractEndDate
                //ProjectTargetGroupMale
                //ProjectTargetGroupFemale
                //ProjectTargetGroupAmount
                //MaleParticipant
                //FemaleParticipant
                //ParticipantAmount
                x.TargetGroup = x.TargetGroups.Any() ? string.Join(",", x.TargetGroups.Distinct()) : "-";
                //ActualExpense
                //ReturnAmount
                return true;
            });

            var all = list.ToLookup(key => key.SectionCode);
            return all;
        }

        private List<ServiceModels.Report.ReportBudgetApplicant> GetTemp()
        {
            var data = new List<ServiceModels.Report.ReportBudgetApplicant>();
            var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "BudgetYear", SortDirection.Ascending);
            var result = this.ReportBudgetApplicantService.ListReportBudgetApplicant(p);
            if (result.IsCompleted)
            {
                data = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message[0]);
            }

            return data;
        }

        private List<ServiceModels.IFilterDescriptor> CreateFilter()
        {
            var fields = new List<ServiceModels.IFilterDescriptor>();

            // เลขทะเบียนโครงการ
            if (!String.IsNullOrEmpty(TextBoxProjectNo.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectNo",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectNo.Text.Trim()
                });
            }

            // ปีงบประมาณ
            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                int approvalYear = DatePickerBudgetYear.SelectedDate.Value.Year;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = approvalYear
                });
            }

            // ชื่อองค์กร
            if (!String.IsNullOrEmpty(TextBoxContractOrgName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "Organization",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxContractOrgName.Text.Trim()
                });
            }

            // ชื่อหน่วยงาน
            if (!String.IsNullOrEmpty(TextBoxProjectName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectName.Text.Trim()
                });
            }

            // สถานะ
            if (DdlApprovalStatus.SelectedIndex > 0)
            {
                var approvalStatus = DdlApprovalStatus.SelectedValue;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ApprovalStatus",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = approvalStatus == "null" ? null : approvalStatus
                });
            }

            // Create CompositeFilterDescriptor
            var filters = new List<ServiceModels.IFilterDescriptor>
            {
                new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                }
            };

            return filters;
        }

        private void ExportExcel(HttpResponse response, ILookup<string, ServiceModels.Report.ReportBudgetApplicant> reportValue)
        {

            System.Reflection.Assembly current = System.Reflection.Assembly.Load("Nep.Project.Report");

            HSSFWorkbook templateWorkbook;
            // Opening the Excel template...
            using (System.IO.Stream stream = current.GetManifestResourceStream("Nep.Project.Report.Template-ReportBudgetApplicant.xls"))
            {
                // Getting the complete workbook...
                templateWorkbook = new HSSFWorkbook(stream, true);
                var summaryInformation = templateWorkbook.SummaryInformation;
                summaryInformation.Author = UserInfo.FullName;
                summaryInformation.CreateDateTime = DateTime.Now;
                summaryInformation.RemoveLastAuthor();
                summaryInformation.RemoveLastSaveDateTime();
            }

            ISheet worksheet;

            var keys = reportValue.Select(g => g.Key).OrderBy(g => g).ToList();
            var sheetIndex = 1;
            foreach (var key in keys)
            {
                var budgetAppModels = reportValue[key].ToList();

                if (budgetAppModels.Any())
                {
                    worksheet = templateWorkbook.CloneSheet(0);
                    var sheetName = WorkbookUtil.CreateSafeSheetName(GetSheetName(key));
                    templateWorkbook.SetSheetName(sheetIndex++, sheetName);
                    for (var i = 0; i < budgetAppModels.Count(); i++)
                    {
                        var budgetAppModel = budgetAppModels[i];
                        int sourceIndex = i + FirstIndexOfRow - 1;
                        if (i < budgetAppModels.Count() - 1)
                        {
                            int destinationIndex = i + FirstIndexOfRow;
                            CopyRow(templateWorkbook, worksheet, sourceIndex, destinationIndex, false);
                        }
                        IRow dataRow = worksheet.GetRow(sourceIndex);

                        SetCellValue(dataRow.GetCell(0), budgetAppModel.ProjectNo);
                        SetCellValue(dataRow.GetCell(1), budgetAppModel.BudgetYear);
                        SetCellValue(dataRow.GetCell(2), budgetAppModel.OrganizationType);
                        SetCellValue(dataRow.GetCell(3), budgetAppModel.Organization);
                        SetCellValue(dataRow.GetCell(4), budgetAppModel.OrganizationSupport);
                        SetCellValue(dataRow.GetCell(5), budgetAppModel.ProjectName);
                        SetCellValue(dataRow.GetCell(6), budgetAppModel.ProjectType);
                        SetCellValue(dataRow.GetCell(7), budgetAppModel.Section);
                        SetCellValue(dataRow.GetCell(8), budgetAppModel.Province);
                        SetCellValue(dataRow.GetCell(9), budgetAppModel.ProjectDateStr);
                        SetCellValue(dataRow.GetCell(10), budgetAppModel.RequestBudget);
                        SetCellValue(dataRow.GetCell(11), budgetAppModel.ApprovalBudgetType);
                        SetCellValue(dataRow.GetCell(12), budgetAppModel.SumTargetGroupAmount);
                        SetCellValue(dataRow.GetCell(13), budgetAppModel.Approval1);
                        SetCellValue(dataRow.GetCell(14), budgetAppModel.Approval3);
                        SetCellValue(dataRow.GetCell(15), budgetAppModel.Approval2);
                        SetCellValue(dataRow.GetCell(16), budgetAppModel.Approval_);
                        SetCellValue(dataRow.GetCell(17), budgetAppModel.ApprovalStatus1);
                        SetCellValue(dataRow.GetCell(18), budgetAppModel.ApprovalStatus0);
                        SetCellValue(dataRow.GetCell(19), budgetAppModel.ApprovalStatusNull);
                        SetCellValue(dataRow.GetCell(20), budgetAppModel.ApprovalNo);
                        SetCellValue(dataRow.GetCell(21), budgetAppModel.ApprovalDate2Str);
                        SetCellValue(dataRow.GetCell(22), budgetAppModel.ProvinceAbbr);
                        SetCellValue(dataRow.GetCell(23), budgetAppModel.BudgetReviseValue);
                        SetCellValue(dataRow.GetCell(24), budgetAppModel.ContractDateStr);
                        SetCellValue(dataRow.GetCell(25), budgetAppModel.ProjectTargetGroupMale);
                        SetCellValue(dataRow.GetCell(26), budgetAppModel.ProjectTargetGroupFemale);
                        SetCellValue(dataRow.GetCell(27), budgetAppModel.ProjectTargetGroupAmount);
                        SetCellValue(dataRow.GetCell(28), budgetAppModel.MaleParticipant);
                        SetCellValue(dataRow.GetCell(29), budgetAppModel.FemaleParticipant);
                        SetCellValue(dataRow.GetCell(30), budgetAppModel.ParticipantAmount);
                        SetCellValue(dataRow.GetCell(31), budgetAppModel.TargetGroup);
                        SetCellValue(dataRow.GetCell(32), budgetAppModel.ActualExpense);
                        SetCellValue(dataRow.GetCell(33), budgetAppModel.ReturnAmount);
                    }

                    CreateTotalRow(budgetAppModels, templateWorkbook, worksheet,
                        worksheet.GetRow(FirstIndexOfRow - 1 + budgetAppModels.Count));

                    worksheet.ForceFormulaRecalculation = true;
                    worksheet.IsSelected = false;
                    worksheet.IsActive = false;
                    worksheet.DisplayGridlines = false;
                }
            }

            // Remove template worksheet
            templateWorkbook.RemoveSheetAt(0);

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                templateWorkbook.Write(exportData);
                response.Clear();
                response.Buffer = true;
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}", FileName));

                response.BinaryWrite(exportData.GetBuffer());
                response.Flush();
                response.End();
            }
        }

        private String GetSheetName(String sectionCode)
        {
            switch (sectionCode)
            {
                case "1": 
                    return ViewState["SectionC"].ToString();
                case "2":
                    return ViewState["SectionCW"].ToString();
                case "3":
                    return ViewState["SectionE"].ToString();
                case "4":
                    return ViewState["SectionS"].ToString();
                case "5":
                    return ViewState["SectionN"].ToString();
                default:
                    return "ไม่ระบุ";
            }
        }

        private void CopyRow(IWorkbook workbook, ISheet worksheet, int sourceRowNum, int destinationRowNum, bool isCopyValue)
        {
            // Get the source / new row
            IRow newRow = worksheet.GetRow(destinationRowNum);
            IRow sourceRow = worksheet.GetRow(sourceRowNum);

            // If the row exist in destination, push down all rows by 1 else create a new row
            if (newRow != null)
            {
                worksheet.ShiftRows(destinationRowNum, worksheet.LastRowNum, 1);
            }
            else
            {
                newRow = worksheet.CreateRow(destinationRowNum);
            }

            // Loop through source columns to add to new row
            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                // Grab a copy of the old/new cell
                ICell oldCell = sourceRow.GetCell(i);
                ICell newCell = newRow.CreateCell(i);

                // If the old cell is null jump to next cell
                if (oldCell == null)
                {
                    newCell = null;
                    continue;
                }

                // Copy style from old cell and apply to new cell
                newCell.CellStyle = oldCell.CellStyle;

                // If there is a cell comment, copy
                if (newCell.CellComment != null) newCell.CellComment = oldCell.CellComment;

                // If there is a cell hyperlink, copy
                if (oldCell.Hyperlink != null) newCell.Hyperlink = oldCell.Hyperlink;

                // Set the cell data type
                newCell.SetCellType(oldCell.CellType);

                if (isCopyValue)
                {
                    // Set the cell data value
                    switch (oldCell.CellType)
                    {
                        case CellType.Blank:
                            newCell.SetCellValue(oldCell.StringCellValue);
                            break;
                        case CellType.Boolean:
                            newCell.SetCellValue(oldCell.BooleanCellValue);
                            break;
                        case CellType.Error:
                            newCell.SetCellErrorValue(oldCell.ErrorCellValue);
                            break;
                        case CellType.Formula:
                            newCell.SetCellFormula(oldCell.CellFormula);
                            break;
                        case CellType.Numeric:
                            newCell.SetCellValue(oldCell.NumericCellValue);
                            break;
                        case CellType.String:
                            newCell.SetCellValue(oldCell.RichStringCellValue);
                            break;
                        case CellType.Unknown:
                            newCell.SetCellValue(oldCell.StringCellValue);
                            break;
                    }
                }
            }

            // If there are are any merged regions in the source row, copy to new row
            for (int i = 0; i < worksheet.NumMergedRegions; i++)
            {
                CellRangeAddress cellRangeAddress = worksheet.GetMergedRegion(i);
                if (cellRangeAddress.FirstRow == sourceRow.RowNum)
                {
                    CellRangeAddress newCellRangeAddress =
                        new CellRangeAddress(
                            newRow.RowNum
                            , (newRow.RowNum + (cellRangeAddress.FirstRow - cellRangeAddress.LastRow))
                            , cellRangeAddress.FirstColumn
                            , cellRangeAddress.LastColumn
                        );
                    worksheet.AddMergedRegion(newCellRangeAddress);
                }
            }

        }

        private void SetCellValue(ICell cell, object value)
        {
            if (value is DBNull)
                cell.SetCellValue("-");
            else if (value == null)
                cell.SetCellValue("-");
            else if (value is decimal)
                cell.SetCellValue(decimal.ToDouble((decimal)value));
            else if (value is DateTime)
                cell.SetCellValue(value.ToString());
            else
                cell.SetCellValue(!String.IsNullOrEmpty((String)value) ? (String)value : "-");
        }

        private IRow CreateTotalRow(IList<ServiceModels.Report.ReportBudgetApplicant> list, IWorkbook workbook, ISheet worksheet, IRow dataRow)
        {
            var count = list.Count;
            SetTotalCell(dataRow.Cells[23], count, workbook);
            SetTotalCell(dataRow.Cells[25], count, workbook);
            SetTotalCell(dataRow.Cells[26], count, workbook);
            SetTotalCell(dataRow.Cells[27], count, workbook);
            SetTotalCell(dataRow.Cells[28], count, workbook);
            SetTotalCell(dataRow.Cells[29], count, workbook);
            SetTotalCell(dataRow.Cells[30], count, workbook);
            SetTotalCell(dataRow.Cells[32], count, workbook);
            SetTotalCell(dataRow.Cells[33], count, workbook);
            return dataRow;
        }

        private void SetTotalCell(ICell cell, int count, IWorkbook workbook)
        {
            var formula = string.Format(FormulaString, cell.StringCellValue);
            // clear value
            cell.SetCellValue(0);
            cell.SetCellType(CellType.Formula);
            cell.SetCellFormula(string.Format(formula, FirstIndexOfRow, FirstIndexOfRow + count - 1));
            IFormulaEvaluator evaluator = new HSSFFormulaEvaluator(workbook);
            evaluator.EvaluateFormulaCell(cell);
        }
    }
}