using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportStatisticDuplicate
    {
        public class CompareDuplicatedSupport
        {
            public decimal BudgetYear { get; set; }
            public decimal BudgetYearThai
            {
                get
                {
                    int year = (int)BudgetYear;
                    DateTime date = new DateTime(year, 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                    string strYear = date.ToString("yyyy", Common.Constants.UI_CULTUREINFO);
                    return Convert.ToDecimal(strYear);
                }
            }
            public String Region { get; set; }
            public Decimal RegionID { get; set; }
            //public string IdCardNo { get; set; }
            public String Gender { get; set; }
            public Int32 CountIDCardNo { get; set; }
            public Decimal ProjectProvinceID { get; set; }
            public Decimal MaleDuplicatedAmount { get; set; }
            public Decimal FeMaleDuplicatedAmount { get; set; }
            
        }

        public class AnalyzeProjectByType
        {
            public decimal TypeOrderNo { get; set; }
            public Decimal TypeID { get; set; }
            public String TypeName { get; set; }
            public Decimal BudgetYear { get; set; }
            public Int32 BudgetYearThai
            {
                get
                {
                    int year = (int)BudgetYear;
                    return (year + 543);
                }
            }
            //public string IdCardNo { get; set; }
            //public String Gender { get; set; }
            public Int32 CountIDCardNo { get; set; }
            public Decimal MaleAmount { get; set; }
            public Decimal FemaleAmount { get; set; }
        }

        public class AnalyzeProjectByStrategic
        {            
            public String StrategicCode { get; set; }
            public String StrategicName
            {
                get {
                    string name = "";
                    string code = StrategicCode;
                   
                    switch(code){
                        case "1":
                            name = Nep.Project.Resources.UI.LabelStandardStrategic1;
                            break;
                        case "2":
                            name = Nep.Project.Resources.UI.LabelStandardStrategic2;
                            break;
                        case "3":
                            name = Nep.Project.Resources.UI.LabelStandardStrategic3;
                            break;
                        case "4":
                            name = Nep.Project.Resources.UI.LabelStandardStrategic4;
                            break;
                        case "5":
                            name = Nep.Project.Resources.UI.LabelStandardStrategic5;
                            break;                        
                    }
                    return name;
                }
            }
            public Decimal BudgetYear { get; set; }
            public Int32 BudgetYearThai
            {
                get
                {
                    int year = (int)BudgetYear;
                    return (year + 543);
                }
            }
            public Decimal MaleAmount { get; set; }
            public Decimal FemaleAmount { get; set; }
            //public string IdCardNo { get; set; }
            //public String Gender { get; set; }
            public Int32 CountIDCardNo { get; set; }
           
        }
    }
}
