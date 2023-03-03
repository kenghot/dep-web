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
        public string ProjectName { get; set; }
        public string OrganizationName { get; set; }
        public string Description { get; set; }
        public DateTime? ProcessStart { get; set; }
        public DateTime? ProcessEnd { get; set; }
        public decimal? ProcessID { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DataLog LogDetail { get; set; }
        public decimal? LocationMapID { get; set; }
        public ApiAttachment AttachmentFile { get; set; }
        public string Addresss { get; set; }
        public string Building { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public decimal? SubDistrictID { get; set; }
        //public string SubDistrict { get; set; }
        public decimal? DistrictID { get; set; }
        //public string District { get; set; }
        public decimal ProvinceID { get; set; }
        //public string Province { get; set; }

    }
    public class ActivityExtend
    {
        public bool PublishActivity { get; set; }
        public List<Media> Medias { get; set; } = new List<Media>();
    }
    public  class Activity : BaseActivity
    {
 
        public List<UploadImageResponse> ImageAttachments { get; set; }
        public ActivityExtend ExtendData { get; set; } = new ActivityExtend();
        public string ExtendJson { get; set; }

    }
    public class ApiAttachment
    {
        public String id { get; set; }
        public String tempId { get; set; }
        public string fileUrl { get; set; }
        public String name { get; set; }
        public String extension { get; set; }
        public int size { get; set; }
        //kenghot
        public String fieldName { get; set; }
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
