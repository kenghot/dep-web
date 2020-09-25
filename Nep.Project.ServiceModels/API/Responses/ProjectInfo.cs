using Nep.Project.ServiceModels.ProjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.API.Responses
{
    #region Project
    public class ProjectScreen
    {
        public ProjectDashboard Dashboard { get; set; }
        public List<Project> Projects { get; set; }
    }
    public class ProjectDashboard
    {
        public int TotalProject { get; set; }
    }
    public class Project
    {
        public decimal ProjectId { get; set; }
        public string ProjectNameTH { get; set; }
        public string OrganizationNameTH { get; set; }
        public string ProvinceName { get; set; }
        public decimal BudgetYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FollowUp { get; set; }
        public decimal? BudgetAmount { get; set; }
        public int TotalActivity { get; set; }
        

        public List<string> Thumbnail { get; set; }
    }
    #endregion
    #region Activity
    public class ActivityScreen
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
    #endregion
}
