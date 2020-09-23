using Nep.Project.ServiceModels.ProjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    public class ProjectListScreen
    {
        public List<ProjectList> Projects { get; set; }
    }
    public class ProjectList 
    {
        public decimal ProjectId { get; set; }
        public string ProjectNameTH { get; set; }
    }

    public class ProjectProcessedScreen
    {
        public List<Activity> Activities { get; set; }
    }
    public  class Activity
    {
        public string Description { get; set; }
        public DateTime? ProcessStart { get; set; }
        public DateTime? ProcessEnd { get; set; }
        public decimal ProcessID { get; set; }

        /// <summary>
        /// kenghot
        /// </summary>
        public List<UploadImageResponse> ImageAttachments { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
   
}
