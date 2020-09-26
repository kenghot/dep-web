using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Requests
{
    public class SaveContractNoRequest
    {
        public string ContractNo { get; set; }
        public string Password { get; set; }
    }
    public class SaveDocRequest
    {
        /// <summary>
        /// PK of ProjectQuestionHD
        /// </summary>
        public decimal? docId { get; set; }
        /// <summary>
        /// this is ProjectID in ProjectQuestionHD
        /// </summary>
        public decimal keyId { get; set; }
        public string docGroup { get; set; }
        /// <summary>
        /// Json string
        /// </summary>
        public string Data { get; set; }
    }
}
