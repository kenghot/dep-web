using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Security
{
    public class SecurityInfo
    {
        public Decimal? UserID { get; set; }
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName {
            get
            {
                return (String.Format("{0} {1}", this.FirstName, ((!String.IsNullOrEmpty(this.LastName)) ? this.LastName : "")));
            }
        }
        public Decimal? ProvinceID { get; set; }
        public String ProvinceName { get; set; }
        public String ProvinceAbbr { get; set; }
        public Decimal? OrganizationID { get; set; }
        public String OrganizationName { get; set; }
        public IEnumerable<String> Roles { get; set; }
        public String TicketID { get; set; }
        public Boolean IsAuthenticated { get; set; }

        public Decimal? UserGroupID { get; set; }
        public String UserGroupCode { get; set; }
        public Boolean IsProvinceOfficer { get; set; }
        public Boolean IsCenterOfficer { get; set; }

        public Decimal? SectionID { get; set; }
        public String SectionName { get; set; }
        public Boolean IsAdministrator { get; set; }
        public DateTime? LoginTime { get; set; }
    }
}
