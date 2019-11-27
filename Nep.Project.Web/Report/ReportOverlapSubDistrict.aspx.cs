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
    public partial class ReportOverlapSubDistrict : Nep.Project.Web.Infra.BasePage
    {
        private List<string> ProvinceList = new List<string>
        {
            "เชียงราย",
            "เชียงใหม่",
            "น่าน",
            "พะเยา",
            "แพร่",
            "แม่ฮ่องสอน",
            "ลำปาง",
            "ลำพูน",
            "อุตรดิตถ์",
            "กาฬสินธุ์",
            "ขอนแก่น"
        };

        private List<string> RegionList = new List<string>
        {
            "เหนือ",
            "ตะวันออกเฉียงเหนือ",
            "กลาง",
            "ตะวันออก",
            "ตะวันตก",
            "ใต้"
        };

        private List<string> DistrictList = new List<string>
        {
        "จตุจักร",
        "ดุสิต",
        "ดอนเมือง",
        "พรเจริญ",
        "โพนพิสัย",
        "ศรีเชียงใหม่",
        "ศรีวิไล",
        "สังคม",
        "สระใคร่"
        };

        private List<string> SubDistrictList = new List<string>
        {
            "หนองโรง",
            "หนองสาหร่าย",
            "เกาะสำโรง",
            "แก่งเสี้ยน",
            "ช่องสะเดา",
            "บ้านร้อง",
            "บ้านหวด",
            "ทุ่งยั้ง",
            "นานกกก",
            "ไผ่ล้อม"
        };

        protected Random rand = new Random();

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
            data.Text = Resources.UI.DropdownAll;
            data.Value = "-1";
            list.Add(data);

            ServiceModels.GenericDropDownListData data1 = new ServiceModels.GenericDropDownListData();
            data1.Text = "2556";
            data1.Value = "2556";
            list.Add(data1);

            ServiceModels.GenericDropDownListData data2 = new ServiceModels.GenericDropDownListData();
            data2.Text = "2557";
            data2.Value = "2557";
            list.Add(data2);

            DropDownListYear.DataSource = list;
            DropDownListYear.DataTextField = "Text";
            DropDownListYear.DataValueField = "Value";
            DropDownListYear.DataBind();

            List<ServiceModels.GenericDropDownListData> listRegion = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData dataRegion = new ServiceModels.GenericDropDownListData();
            dataRegion.Text = Resources.UI.DropdownAll;
            dataRegion.Value = "-1";
            listRegion.Add(dataRegion);

            int iRegion = 0;
            foreach (var item in RegionList)
            {
                iRegion = iRegion +1;
                ServiceModels.GenericDropDownListData r = new ServiceModels.GenericDropDownListData();
                r.Text = item;
                r.Value = iRegion.ToString();
                listRegion.Add(r);
            }

            DropDownListRegion.DataSource = listRegion;
            DropDownListRegion.DataTextField = "Text";
            DropDownListRegion.DataValueField = "Value";
            DropDownListRegion.DataBind();

            List<ServiceModels.GenericDropDownListData> listProvince = new List<ServiceModels.GenericDropDownListData>();
            Array eProvince = Enum.GetValues(typeof(ServiceModels.Report.TempEnumProvince));
            ServiceModels.GenericDropDownListData pEmpty = new ServiceModels.GenericDropDownListData();
            pEmpty.Text = Resources.UI.DropdownAll;
            pEmpty.Value = "-1";
            listProvince.Add(pEmpty);

            for (int i = 0; i < eProvince.Length; i++)
            {
                ServiceModels.GenericDropDownListData p = new ServiceModels.GenericDropDownListData();
                p.Text = eProvince.GetValue(i).ToString();
                p.Value = eProvince.GetValue(i).ToString();
                listProvince.Add(p);
            }

            DropDownListProvince.DataSource = listProvince;
            DropDownListProvince.DataTextField = "Text";
            DropDownListProvince.DataValueField = "Value";
            DropDownListProvince.DataBind();

            List<ServiceModels.GenericDropDownListData> listDistrict = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData dataDistrict = new ServiceModels.GenericDropDownListData();
            dataDistrict.Text = Resources.UI.DropdownAll;
            dataDistrict.Value = "-1";
            listDistrict.Add(dataDistrict);

            int iDistrict = 0;
            foreach (var item in DistrictList)
            {
                iDistrict = iDistrict + 1;
                ServiceModels.GenericDropDownListData d = new ServiceModels.GenericDropDownListData();
                d.Text = item;
                d.Value = iDistrict.ToString();
                listDistrict.Add(d);
            }

            DropDownListDistrict.DataSource = listDistrict;
            DropDownListDistrict.DataTextField = "Text";
            DropDownListDistrict.DataValueField = "Value";
            DropDownListDistrict.DataBind();

            List<ServiceModels.GenericDropDownListData> listSubDistrict = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData dataSubDistrict = new ServiceModels.GenericDropDownListData();
            dataSubDistrict.Text = Resources.UI.DropdownAll;
            dataSubDistrict.Value = "-1";
            listSubDistrict.Add(dataSubDistrict);

            int iSubDistrict = 0;
            foreach (var item in DistrictList)
            {
                iSubDistrict = iSubDistrict + 1;
                ServiceModels.GenericDropDownListData d = new ServiceModels.GenericDropDownListData();
                d.Text = item;
                d.Value = iSubDistrict.ToString();
                listSubDistrict.Add(d);
            }

            DropDownListSubDistrict.DataSource = listSubDistrict;
            DropDownListSubDistrict.DataTextField = "Text";
            DropDownListSubDistrict.DataValueField = "Value";
            DropDownListSubDistrict.DataBind();
        }

        protected void BindReport()
        {
            string year = string.Empty;
            if (!String.IsNullOrEmpty(DropDownListYear.SelectedValue))
            {
                year = DropDownListYear.SelectedValue;
            }

            ReportViewerOverlapSubDistrict.LocalReport.DataSources.Clear();
            ReportViewerOverlapSubDistrict.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportOverlapSubDistrict.rdlc"));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");

            if (!string.IsNullOrEmpty(year) && year != "-1")
            {
                dataset1.Value = GetTemp().Where(x => x.BudgetYear == year);
            }
            else
            {
                dataset1.Value = GetTemp();
            }

            ReportViewerOverlapSubDistrict.LocalReport.DataSources.Add(dataset1);
            ReportViewerOverlapSubDistrict.DataBind();
            ReportViewerOverlapSubDistrict.Visible = true;
        }

        private List<ServiceModels.Report.ReportOverlapSubDistrict> GetTemp()
        {
            List<ServiceModels.Report.ReportOverlapSubDistrict> list = new List<ServiceModels.Report.ReportOverlapSubDistrict>();

            Random rndInt = new Random();
            for (int i = 0; i < 11; i++)
            {
                int TypeTmp1 = rndInt.Next(1, 8);
                string StrTmp1 = "Type" + TypeTmp1.ToString();
                int TypeTmp2 = rndInt.Next(1, 8);
                string StrTmp2 = "Type" + TypeTmp2.ToString();

                string d = RandomStringList("District");
                string s = RandomStringList("SubDistrict");

                ServiceModels.Report.ReportOverlapSubDistrict data = new ServiceModels.Report.ReportOverlapSubDistrict();
                data = GetTempTypeValue(StrTmp1, StrTmp2);
                data.No = (i + 1);
                data.Region = "เหนือ";
                data.Province = ProvinceList[i];
                data.District = d;
                data.SubDistrict = s;
                data.DuplicateAmount = "2";
                list.Add(data);
            }

            return list;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private String RandomStringList(string type)
        {
            string result = string.Empty;
            switch (type)
            {
                case "District":
                    {
                        int i = rand.Next(DistrictList.Count);
                        result = DistrictList[i];
                        break;
                    }
                case "SubDistrict":
                    {
                        int i = rand.Next(SubDistrictList.Count);
                        result = SubDistrictList[i];
                        break;
                    }
                default:
                    break;
            }
            return result;
        }

        private ServiceModels.Report.ReportOverlapSubDistrict GetTempTypeValue(string str1, string str2)
        {
            ServiceModels.Report.ReportOverlapSubDistrict data = new ServiceModels.Report.ReportOverlapSubDistrict();
            int cType1 = 0, cType2 = 0, cType3 = 0, cType4 = 0, cType5 = 0, cType6 = 0, cType7 = 0, cType8 = 0;

            switch (str1)
                {
                    case "Type1":
                        cType1 = 1;
                        break;
                    case "Type2":
                        cType2 = 1;
                        break;
                    case "Type3":
                        cType3 = 1;
                        break;
                    case "Type4":
                        cType4 = 1;
                        break;
                    case "Type5":
                        cType5 = 1;
                        break;
                    case "Type6":
                        cType6 = 1;
                        break;
                    case "Type7":
                        cType7 = 1;
                        break;
                    case "Type8":
                        cType8 = 1;
                        break;
                    default:
                        break;
                }

            switch (str1)
            {
                case "Type1":
                    cType1 = cType1 + 1;
                    break;
                case "Type2":
                    cType2 = cType2 + 1;
                    break;
                case "Type3":
                    cType3 = cType3 + 1;
                    break;
                case "Type4":
                    cType4 = cType4 + 1;
                    break;
                case "Type5":
                    cType5 = cType5 + 1;
                    break;
                case "Type6":
                    cType6 = cType6 + 1;
                    break;
                case "Type7":
                    cType7 = cType7 + 1;
                    break;
                case "Type8":
                    cType8 = cType8 + 1;
                    break;
                default:
                    break;
            }

            data.Type1 = (cType1 > 0) ? cType1.ToString() : "";
            data.Type2 = (cType2 > 0) ? cType2.ToString() : "";
            data.Type3 = (cType3 > 0) ? cType3.ToString() : "";
            data.Type4 = (cType4 > 0) ? cType4.ToString() : "";
            data.Type5 = (cType5 > 0) ? cType5.ToString() : "";
            data.Type6 = (cType6 > 0) ? cType6.ToString() : "";
            data.Type7 = (cType7 > 0) ? cType7.ToString() : "";
            data.Type8 = (cType8 > 0) ? cType8.ToString() : "";
            return data;
        }
    }
}