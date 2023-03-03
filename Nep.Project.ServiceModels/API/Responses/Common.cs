using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class UploadImageResponse
    {
        public decimal ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageFullPath { get; set; }
    }
    public class BaseDataLog
    {
        public decimal? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string UserImage { get; set; }

    }
    public class DataLog
    {
        public BaseDataLog AddLog { get; set; }
        public BaseDataLog UpdateLog { get; set; }
    }
    public class Media
    {
        public decimal? ID { get; set; }
        /// <summary>
        /// youtube , facebook, instagram
        /// </summary>
        public string MediaType { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
    }
    public class GenericDropDownListData
    {
        public String Value { get; set; }
        public String Text { get; set; }
        public decimal? OrderNo { get; set; }
    }
}
