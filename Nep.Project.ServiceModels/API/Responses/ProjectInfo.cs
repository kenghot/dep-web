using Nep.Project.DBModels.Model;
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
        public decimal? BudgetYear { get; set; }
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
    public class BaseActivity
    {
        public string Description { get; set; }
        public DateTime? ProcessStart { get; set; }
        public DateTime? ProcessEnd { get; set; }
        public decimal? ProcessID { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DataLog LogDetail { get; set; }
    }
    public  class Activity : BaseActivity
    {
 
        public List<UploadImageResponse> ImageAttachments { get; set; }
 
    }
    #endregion
    #region Participant Survey
    public class ParticipantSurvey 
    {
        public decimal ProjectId { get; set; }
        public string ProjectTHName { get; set; }
        public List<ParticipantSurveyDetail> Surveys { get; set; }
    }
    public class ParticipantSurveyDetail
    {
        public decimal DocId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public string Activity { get; set; }
    }

    #endregion
}
