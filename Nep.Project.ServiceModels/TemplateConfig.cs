using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.TemplateConfig
{
    public class OrgWaringReportParam
    {
        public Decimal ProjectID { get; set; }

        public String ContractNo { get; set; }
        public DateTime ContractDate { get; set; }

        public String ProjectNameTH { get; set; }
        public Decimal? BudgetReviseValue { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EnddateProjectReport 
        { 
            get 
            {
                DateTime endReportDate = EndDate.AddDays(90);
                return endReportDate;
            } 
        }

        /// <summary>
        /// PersonalPrefixName
        /// </summary>
        public String LOVName { get; set; }
     
        public String PersonalName
        {
            get {
                string name = String.Format("{0}{1} {2}", LOVName, Firstname1, Lastname1);
                return name;
            }
        }

        /// <summary>
        /// PersonalFirstName
        /// </summary>
        public String Firstname1 { get; set; }
        /// <summary>
        /// PersonalLastName
        /// </summary>
        public String Lastname1 { get; set; }
        /// <summary>
        /// PersonalMobileNumber
        /// </summary>
        public String Mobile1 { get; set; }
        /// <summary>
        /// PersonalEmail
        /// </summary>
        public String Email1 { get; set; }

        /// <summary>
        /// OrganizationName
        /// </summary>
        public String OrganizationNameTH { get; set; }
        /// <summary>
        /// OrgMobileNumber
        /// </summary>
        public String Mobile { get; set; }
        /// <summary>
        /// OrgEmail
        /// </summary>
        public String Email { get; set; }
    }
}
