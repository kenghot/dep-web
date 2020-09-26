using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IProjectInfoService
    {
        Nep.Project.DBModels.Model.NepProjectDBEntities GetDB();
        List<ServiceModels.GenericDropDownListData> ListProvince();

        List<ServiceModels.DecimalDropDownListData> ListOrganization(decimal? provinceID);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> GetOrganizationInfoByID(Decimal id);

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetOrganizationType();

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> Create(ServiceModels.ProjectInfo.OrganizationInfo model
            , String projectNameTH, String projectNameEN);

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ApprovalYear();

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> GetProjectGeneralInfoByProjectID(Decimal id);

        ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectInfoList> ListProjectInfoList(ServiceModels.QueryParameter p);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.OrganizationInfo> Update(ServiceModels.ProjectInfo.OrganizationInfo model);

        List<ServiceModels.GenericDropDownListData> ListProjectInfoType();

        List<ServiceModels.GenericDropDownListData> ListProjectTarget();

        List<ServiceModels.GenericDropDownListData> ListDisabilityType();

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> SaveProjectInformation(ServiceModels.ProjectInfo.TabProjectInfo model
            , List<ServiceModels.ProjectInfo.ProjectTarget> targetList);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProjectInfo> GetProjectInformationByProjecctID(Decimal id);

        ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTarget> GetProjectTargetByProjectID(Decimal id);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> SaveProjectPersonal(ServiceModels.ProjectInfo.TabPersonal model);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabPersonal> GetProjectPersonalByProjectID(Decimal id);

        List<ServiceModels.GenericDropDownListData> ListPrefix();

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> SaveProjectOperation(ServiceModels.ProjectInfo.TabProcessingPlan model);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabProcessingPlan> GetProjectOperationByProjectID(Decimal id);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> GetProjectBudgetInfoByProjectID(Decimal id);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> SaveProjectBudget(ServiceModels.ProjectInfo.ProjectBudget model);
        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> SaveProjectBudgetActivity(ServiceModels.ProjectInfo.ProjectBudget model);
        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> GetProjectContractByProjectID(Decimal id);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabContract> SaveProjectContract(ServiceModels.ProjectInfo.TabContract model);

        ServiceModels.ReturnMessage CancelProjectContract(Decimal id);
        ServiceModels.ReturnMessage UndoCancelProjectContract(Decimal id);
        ServiceModels.ReturnMessage DeleteProject(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectGeneralInfoByID(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectInfoByID(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectPersonalByID(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectOperationByID(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectBudgetByID(Decimal id);

        ServiceModels.ReturnMessage DeleteProjectContractByID(Decimal id);

        ServiceModels.ReturnObject<bool> ValidateSubmitData(decimal projectID);

        ServiceModels.ReturnMessage SendDataToReview(decimal projectID,string ipAddress);

        ServiceModels.ReturnObject<ServiceModels.ListOfValue> GetProjectApprovalStatus(decimal projectID);        

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.TabAttachment> GetProjectAttachment(decimal projectID);

        ServiceModels.ReturnMessage SaveProjectDocument(ServiceModels.ProjectInfo.ProjectDocument model);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectFollowup> GetProjectFollowup(decimal projectID);

        ServiceModels.ReturnMessage SaveProjectFollowup(ServiceModels.ProjectInfo.ProjectFollowup model);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.AssessmentProject> GetAssessmentProject(decimal projectID);

        ServiceModels.ReturnMessage SaveProjectEvaluation(ServiceModels.ProjectInfo.AssessmentProject model);
        DBModels.Model.PROJECTHISTORY CreateRowProjectHistory(decimal projectID, string histType, decimal userID, string ipAddress);
        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectApprovalResult> GetProjectApprovalResult(decimal projectID);

        ServiceModels.ReturnMessage SaveProjectApprovalResult(ServiceModels.ProjectInfo.ProjectApprovalResult model);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectReportResult> GetProjectReportResult(decimal projectID);
        ServiceModels.ReturnObject<string> GetQNData(decimal projectID, string qGroup);
        ServiceModels.ReturnObject<string> SaveQNData(decimal projectID, string qGroup,string isReported, string QNData);
        ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.Questionare>> GetProjectQuestionare(decimal projectID,string qGroup);
       
        ServiceModels.ReturnMessage SaveProjectReportResult(ServiceModels.ProjectInfo.ProjectReportResult model, bool isSaveOfficerReport, bool isSendReport);
        ServiceModels.ReturnMessage SaveProjectQuestionareResult(decimal projID,string QNGroup,string controls, bool isSaveOfficerReport, bool isSendReport,string ipAddress);
 
        ServiceModels.ReturnObject<decimal?> SaveDocument(decimal projID, string QNGroup, string data);
        ServiceModels.ReturnObject<string> GetDocument(decimal projID, string QNGroup);
        ServiceModels.ReturnObject<bool> SaveAttachFile(decimal projectID, string attachType, List<ServiceModels.KendoAttachment> removeFiles, List<ServiceModels.KendoAttachment> addFiles, string tableName, string fieldName);
        ServiceModels.ReturnMessage SaveOfficerProjectReport(ServiceModels.ProjectInfo.ProjectReportResult model);

        ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.ProjectTargetNameList> GetProjectTargetForParticipant(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetProjectParticipantTargetEtc(decimal projectID);

        ServiceModels.ReturnObject<decimal> GetTotalIsFollowup(ServiceModels.QueryParameter p);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> IsReportTrackingCreateNew(decimal projectID, decimal reportTrackingTypeID);

        ServiceModels.ReturnQueryData<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> GetListFollowupTrackingDocumentList(ServiceModels.QueryParameter p);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> GetFollowupTrackingDocumentForm(decimal reportTrackingID);

        ServiceModels.ReturnMessage DeleteFollowupTrackingDocumentForm(decimal reportTrackingID);

        ServiceModels.ReturnObject<ServiceModels.ProjectInfo.FollowupTrackingDocumentForm> SaveFollowupTrackingDocumentForm(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> GetReportOrgTrackingContext(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> GetReportProvinceTrackingContext(ServiceModels.ProjectInfo.FollowupTrackingDocumentForm model);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportFormatContract> GetReportFormatContract(decimal projectID);

        //ServiceModels.ReturnQueryData<Common.ProjectFunction> GetProjectFunction(String projectApprovalStatus, decimal? creatorOrganizationID, decimal? projectProvinceID, bool isReportedResult, String approvalStatus, decimal projectOrganizationID);
        ServiceModels.ReturnQueryData<Common.ProjectFunction> GetProjectFunction(decimal projectID);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo> GetReportProjectRequest(decimal projectID);
        ServiceModels.ReturnObject<ServiceModels.Report.ReportPaymentSlip> GetPaymentSlip(decimal projectID);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.OrganizationAssistance> GetListOrganizationAssistance(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectAttachment> GetListProjectAttachment(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> GetListProjectBudget(decimal projectID);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectBudget> GetListProjectBudgetNew(decimal projectID);
        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectCommittee> GetListProjectCommitteet(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectTargetGroup> GetListProjectTargetGroup(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectRequest.ProjectOperationAddress> GetListProjectOperationAddres(decimal projectID);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> GetReportSummaryProjectInfo(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail> GetListReportSummaryProjectInfoBudgetDetail(decimal projectID);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo> GetReportProjectResult(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectResult.ProjectType> GetListReportProjectResultProjectType(decimal projectID);

        ServiceModels.ReturnQueryData<ServiceModels.Report.ReportProjectResult.SummaryProjectResult> GetListReportProjectResultSummaryProjectResult(decimal projectID, string summaryResultCode);

        ServiceModels.ReturnObject<ServiceModels.KendoAttachment> SaveCancelledProjectRequest(decimal projectID, ServiceModels.KendoAttachment attachment);

        ServiceModels.ReturnObject<ServiceModels.KendoAttachment> GetCancelledProjectRequest(decimal attachmentID);

        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> GetListProjectProvince();

        ServiceModels.ReturnObject<ServiceModels.Report.ReportSummaryProjectInfo> GetReportSummaryProjectInfo1(decimal projectID);

        ServiceModels.ReturnMessage RejectToOrganization(decimal projectID, string comment,string checkbox);

        ServiceModels.ReturnObject<List<String>> GetRejectComment(decimal projectID);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportOrgTracking> GetReportOrgTracking(decimal reportTrackingID);

        ServiceModels.ReturnObject<ServiceModels.Report.ReportProvinceTracking> GetReportProvinceTracking(decimal reportTrackingID);

        ServiceModels.ReturnObject<ServiceModels.GenericDropDownListData> GetProjectProvince(decimal projectid);
        //kenghot
        ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListPosition();
        ServiceModels.KendoChart GetDashBoardData();
        ServiceModels .ProjectInfo.DashBoard  GetDashBoardData(ServiceModels.QueryParameter q);
        ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.ProjectHistory>> GetProjectHistoryList(decimal projID);
        ServiceModels.ReturnObject<bool> SendReportToRevise(decimal projecID, string comment);
        List<ServiceModels.ProjectInfo.ProjectProcessed> MapProjectProcessed(decimal projectID);
        DBModels.Model.MT_ListOfValue GetListOfValue(string code, string group);
        DBModels.Model.MT_ListOfValue GetListOfValueByKey(decimal LOVID);
        ServiceModels.ReturnObject<bool> SaveProjectProcessed(decimal projID, List<ServiceModels.ProjectInfo.ProjectProcessed> data,string ipAddress);
        ServiceModels.ReturnMessage SaveLogAccess(decimal? userId,string accessCode, string accessType, string ipAddress);
        //ServiceModels.ReturnObject<bool> SaveFileUpload(List<ServiceModels.KendoAttachment> addFiles, List<ServiceModels.KendoAttachment> removeFiles, decimal projID, string tabName, string tabField, decimal attachTypeID);
    }
}
