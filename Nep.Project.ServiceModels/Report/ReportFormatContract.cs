using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nep.Project.ServiceModels.Report
{
    public class ReportFormatContract
    {
        public String ContractNo {get;set;}
        public String SignAt {get;set;}
        public String SignDistrict {get;set;}
        public String SignSubdistrict {get;set;}
        public String SignProvince {get;set;}
        public String ContractDate {get;set;}
        public String ContractBy {get;set;}
        public String Position {get;set;}
        public String DirectiveNo {get;set;}
        public String DirectiveDate {get;set;}
        public String DirectiveProvince {get;set;}
        public String DirectProvinceNo {get;set;}
        public String DirectProvinceDate {get;set;}
        public String ReceiverName {get;set;}
        public String ReceiverAddressNo {get;set;}
        public String ReceiverDistrict {get;set;}
        public String ReceiverSubdistrict {get;set;}
        public String ReceiverProvince {get;set;}
        public String ReceiverBy {get;set;}
        public String AttorneyDate {get;set;}
        public String Amount {get;set;}
        public String AmountString {get;set;}
        public String ProjectName { get; set; }

        public String ContractViewerName1 { get; set; }
        public String ContractViewerSurname1 { get; set; }
        public String ContractViewer1Fullname {
            get
            {
                string name = this.ContractViewerName1;
                if(!String.IsNullOrEmpty(this.ContractViewerSurname1)){
                    name = String.Format("{0} {1}", name, this.ContractViewerSurname1);
                }
                return name;
            }
        }

        public String ContractViewerName2 { get; set; }
        public String ContractViewerSurname2 { get; set; }
        public String ContractViewer2Fullname
        {
            get
            {
                string name = this.ContractViewerName2;
                if (!String.IsNullOrEmpty(this.ContractViewerSurname2))
                {
                    name = String.Format("{0} {1}", name, this.ContractViewerSurname2);
                }
                return name;
            }
        }

        public Boolean IsCenterContract { get; set; }

        public String ReceivePosition { get; set; }

        public Boolean AuthorizeFlag { get; set; }

        public string AttachPage1 { get; set; }
        public string AttachPage2 { get; set; }
        public string AttachPage3 { get; set; }
        public string MeetingNo { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string MeetingDateText { get; set; }
        public string MeetingText { get; set; }
    }
}
