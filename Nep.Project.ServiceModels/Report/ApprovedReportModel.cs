using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
 
    public class ApprovedReportModel
    {
        public int No { get; set; }
        public decimal BudgetYear { get; set; }
        public decimal ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string ProjectTypeName { get; set; }
        public decimal OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string OrganizationType { get; set; }
        public string OrganizationTypeCode { get; set; }
        public string OrganizationTypeEtc { get; set; }
        public string DisabilityType { get; set; }
        public string Mission1 { get; set; }
        public string Mission2 { get; set; }
        public string Mission3 { get; set; }
        public string Mission4 { get; set; }
        public string Mission5 { get; set; }
        public string Mission6 { get; set; }
        public decimal TotalTarget { get; set; }
        public decimal BudgetTypeId { get; set; }
        public string BudgetTypeName { get; set; }
        public string MissionText { get {
                var m = "";
                if (this.Mission1 == "1")
                {
                    m = "1";
                }
                if (this.Mission2 == "1")
                {
                    m = "2";
                }
                if (this.Mission3 == "2")
                {
                    m = "3";
                }
                if (this.Mission4 == "1")
                {
                    m = "4";
                }
                if (this.Mission5 == "1")
                {
                    m = "5";
                }
                if (this.Mission6 == "1")
                {
                    m = "6";
                }
                if (!string.IsNullOrEmpty(m))
                {
                    return $"ยุทธศาตร์ที่ {m}";
                }else
                {
                    return "";
                }
            } }

    }
 
    
    }
