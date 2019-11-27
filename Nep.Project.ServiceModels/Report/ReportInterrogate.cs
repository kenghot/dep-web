using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportInterrogate
    {
        public String No {get;set;}
        public String SendDate {get;set;}
        public String SendTo {get;set;}
        public String ReferenceDocument {get;set;}
        public String ResultRefDoc {get;set;}
        public String AmountProject {get;set;}
        public String ProjectName {get;set;}
        public String Sign {get;set;}
        public String Position {get;set;}
        public String Address { get; set; }
    }
}
