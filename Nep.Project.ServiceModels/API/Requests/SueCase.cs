using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Requests
{
    public class SueCase
    {
        public string case_type { get; set; }
        public string case_code { get; set; }
        public string case_name { get; set; }
        public SueCaseAddress case_contact_address { get; set; }
        public string case_submitted_by_name { get; set; }
        public string case_remarks { get; set; }
        public List<SueCaseDetail> case_details { get; set; }
        public List<SueCaseDocument> case_documents { get; set; }

    }
    public class SueCaseAddress
    {
        public string address_no { get; set; }
        public string soi { get; set; }
        public string moo { get; set; }
        public string road { get; set; }
        public string province_code { get; set; }
        public string province_name { get; set; }
        public string district { get; set; }
        public string district_name { get; set; }
        public string sub_district { get; set; }
        public string sub_district_name { get; set; }
        public string postcode { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
    }
    public class SueCaseDetail
    {
        public string contract_no { get; set; }
        public string contract_start_date { get; set; }
        public  string contract_end_date { get; set; }
        public  decimal contract_amount { get; set; }
        public decimal contract_paid { get; set; }
        public  decimal contract_outstanding { get; set; }
        public string contract_last_payment_date { get; set; }
        public  decimal case_owned_principal { get; set; }
        public decimal case_owned_interests { get; set; }
    }
    public class SueCaseDocument
    {
        public string label { get; set; }
        public string url { get; set; }
    }
}
