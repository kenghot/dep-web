using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace Nep.Project.Web.Report
{
    public partial class ReportReceiverFunding : Nep.Project.Web.Infra.BasePage
    {
        private List<string> AgencyList = new List<string>
        {
            "หน่วยงานภาครัฐ",
            "องค์กรไม่หวังผลกำไร"
        };

        private List<string> OrganizationList = new List<string>
        {
            "มูลนิธิพัฒนาคนพิการไทย",
            "สำนักงานแรงงานจังหวัดระยอง",
            "สมาคมสภาคนพิการทุกประเภทแห่งประเทศไทย",
            "สมาคมศิษย์เก่าศูนย์ฝึกอาชีพและพัฒนาสมรรถภาพคนตาบอด",
            "สมาคมคนพิการนนทบุรี",
            "สมาคมเพื่อคนพิการทางสติปัญญาแห่งประเทศไทย",
            "สมาคมพร้อมใจคนพิการและผู้ด้อยโอกาส",
            "กรมส่งเสริมการปกครองท้องถิ่น"
        };

        private List<string> ProjectNameList = new List<string>
        {
            "โครงการประชุมสมัชชาคนพิการจังหวัด",
            "โครงการพัฒนาศักยภาพอาสาสมัคร/ผู้นำ  เพื่อการฟื้นฟูและพัฒนาคนพิการในชุมชน",
            "โครงการฝึกอาชีพนวดแผนไทย",
            "โครงการจัดตั้งศูนย์เรียนรู้ในชุมชนเพื่อการฟื้นฟูและพัฒนาคนพิการโดยชุมชน",
            "โครงการฝึกอบรมวิชาชีพการนวดแผนไทยให้แก่คนตาบอดจังหวัดพิจิตร",
            "โครงการฝึกวิชาชีพโดยการนวดแผนไทยให้แก่คนตาบอดจังหวัดพิจิตรและการฝึกการเรียนรู้เรื่องใกล้ตัวที่จำเป็นในชีวิตประจำวันของคนตาบอด",
            "โครงการฝึกอบรมอาชีพอิสระการทำพวงหรีดด้วยช้อน - ส้อม",
            "โครงการอบรมอาชีพการเพาะเห็ดนางฟ้าและเห็ดนางรม",
            "โครงการประชุมเชิงปฏิบัติการบุคลากรขององค์กรปกครองส่วนท้องถิ่นในการเสริมสร้างระบบการดูแลสุขภาพผู้พิการระดับตำบลและจังหวัด",
            "โครงการฝึกอบรมภาษาอังกฤษเบื้องต้นเพื่อการประกอบอาชีพและการสื่อสารสำหรับคนพิการทางการเห็น",
            "โครงการฝึกอบรมคอมพิวเตอร์เบื้องต้นสำหรับผู้พิการทางสายตา"
        };

        private List<string> FundingTypeList = new List<string>
        {
            "พ.ก.",
            "จังหวัด",
        };

        protected Random rnd = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
                BindReport();
            }
        }

        protected void BindDropDownList()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData data = new ServiceModels.GenericDropDownListData();
            data.Text = Resources.UI.DropdownPleaseSelect;
            data.Value = "-1";
            list.Add(data);

            ServiceModels.GenericDropDownListData data1 = new ServiceModels.GenericDropDownListData();
            data1.Text = "อนุมัติ";
            data1.Value = "1";
            list.Add(data1);

            ServiceModels.GenericDropDownListData data2 = new ServiceModels.GenericDropDownListData();
            data2.Text = "ไม่อนุมัติ";
            data2.Value = "2";
            list.Add(data2);

            ServiceModels.GenericDropDownListData data3 = new ServiceModels.GenericDropDownListData();
            data3.Text = "ปรับปรุง";
            data3.Value = "3";
            list.Add(data3);

            ddlStatus.DataSource = list;
            ddlStatus.DataTextField = "Text";
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataBind();
        }

        protected void BindReport()
        {
            ReportViewerReceiverFunding.LocalReport.DataSources.Clear();
            
            List<ServiceModels.Report.ReportReceiverFunding> list = GetTempCriteria();
            ReportViewerReceiverFunding.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportReceiverFunding.rdlc"));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            dataset1.Value = list;

            if (list.Count > 0)
            {
                ReportViewerReceiverFunding.LocalReport.DataSources.Add(dataset1);
                ReportViewerReceiverFunding.DataBind();
                ReportViewerReceiverFunding.Visible = true;
            }
            else
            {
                ReportViewerReceiverFunding.Visible = false;
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private List<ServiceModels.Report.ReportReceiverFunding> GetTemp()
        {
            List<ServiceModels.Report.ReportReceiverFunding> list = new List<ServiceModels.Report.ReportReceiverFunding>();
            
            int initNo = 570957250;

            Random rndAmount = new Random();
            for (int i = 0; i < 11; i++)
            {
                string rAgencies = RandomStringList("Agency");
                string rOranizationName = RandomStringList("Organize");
                string agencyUnder = string.Empty;

                if (rOranizationName == "สมาคมเพื่อคนพิการทางสติปัญญาแห่งประเทศไทย")
                    agencyUnder = "ชมรมส่งเสริมและพัฒนาปัญญาจังหวัดพิจิตร";
                else if (rOranizationName == "สำนักงานแรงงานจังหวัดระยอง")
                    agencyUnder = "ชมรมกลุ่มสร้างงานสร้างอาชีพคนพิการและผู้สูงอายุ";

                string rProvince = RandomStringList("Province");
                decimal? rRequestAmount = rndAmount.Next(50000, 1000000);

                string rFundingType = RandomStringList("FundingType");
                string resolutionLevel = string.Empty;
                if (rFundingType == "จังหวัด")
                    resolutionLevel = "จังหวัด";
                else
                    resolutionLevel = "ส่วนกลาง";

                int rAmountMale = rndAmount.Next(1, 10);
                int rAmountFemale = rndAmount.Next(1, 10);
                int totalAmount = rAmountMale + rAmountFemale;

                ServiceModels.Report.ReportReceiverFunding data = new ServiceModels.Report.ReportReceiverFunding();
                data.No = "C" + (initNo + i).ToString();
                data.BudgetYear = "2557";
                data.Agencies = rAgencies;
                data.Organization = rOranizationName;
                data.AgencyUnder = agencyUnder;
                data.ProjectName = ProjectNameList[i];
                data.ProjectType = string.Empty;
                data.Region = "กลาง";
                data.Province = rProvince;
                data.SupportRequestDate = "";
                data.SupportRequestAmount = rRequestAmount;
                data.FundingType = rFundingType;
                data.TotalMembers = null;
                data.ResultConsider = 1;
                data.Status = 1;
                data.ResolutionTime = "8/56";
                data.ResolutionDate = "23 ก.ค. 56";
                data.ResolutionLevel = resolutionLevel;
                data.ApproveAmount = rRequestAmount;
                data.AmountMaleRequest = rAmountMale;
                data.AmountFemaleRequest = rAmountFemale;
                data.TotalAmountRequest = totalAmount;
                data.AmountMaleReal = rAmountMale;
                data.AmountFemaleReal = rAmountFemale;
                data.TotalAmountReal = totalAmount;
                data.Target = "";
                data.AmountReal = rRequestAmount;
                data.AmountReFunding = 0;

                if(i == 4 || i == 7)
                    data.Period = "ต.ค.56 - ก.ย.57";

                list.Add(data);
            }

            return list;
        }

        private String RandomStringList(string type)
        {
            string result = string.Empty;

            switch (type)
            {
                case "Agency":
                                {
                                    int i = rnd.Next(AgencyList.Count);
                                    result = AgencyList[i];
                                    break;
                                }
                case "Organize":
                                {
                                    int i = rnd.Next(OrganizationList.Count);
                                    result = OrganizationList[i];
                                    break;
                                }
                case "Province":
                                {
                                    Array values = Enum.GetValues(typeof(ServiceModels.Report.TempEnumProvince));
                                    int i = rnd.Next(values.Length);
                                    result = values.GetValue(i).ToString();
                                    break;
                                }
                case "FundingType":
                                {
                                    int i = rnd.Next(FundingTypeList.Count);
                                    result = FundingTypeList[i];
                                    break;
                                }
                default:
                    break;
            }

            return result;
        }

        private List<ServiceModels.Report.ReportReceiverFunding> GetTempCriteria()
        {
            List<ServiceModels.Report.ReportReceiverFunding> result = new List<ServiceModels.Report.ReportReceiverFunding>();
            string no = TextBoxNumber.Text.Trim();
            string year = TextBoxYear.Text.Trim();
            string organize = TextBoxOrganizationName.Text.Trim();
            string project = TextBoxProjectName.Text.Trim();
            int? status = Convert.ToInt32(ddlStatus.SelectedValue);

            result = GetTemp();
            if (!string.IsNullOrEmpty(no))
                result = result.Where(x => x.No.Contains(no)).ToList();

            if (!string.IsNullOrEmpty(year))
                result = result.Where(x => x.BudgetYear == year).ToList();

            if (!string.IsNullOrEmpty(organize))
                result = result.Where(x => x.Organization.Contains(organize)).ToList();

            if (!string.IsNullOrEmpty(project))
                result = result.Where(x => x.ProjectName.Contains(project)).ToList();

            if (status > 0)
                result = result.Where(x => x.Status == status).ToList();

            return result;
        }

    }
}