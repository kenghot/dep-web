using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class Report3
    {
        public String No { get; set; }

        public String DocumentDate { get; set; }

        public String To { get; set; }

        public String ReferenceNo { get; set; }

        public String RefDocumentDate { get; set; }

        public String ResultTimeNo { get; set; }

        public String ResultDate { get; set; }

        public String ProjectName { get; set; }

        public String ProjectBy { get; set; }

        public String Amount { get; set; }

        public String StringAmount { get; set; }

        public String ProjectProvince { get; set; }

        public String SendDocumentDate { get; set; }
    }
}
