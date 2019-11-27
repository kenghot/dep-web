using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public class Province
    {
        public decimal ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceAbbr { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
    }
}
