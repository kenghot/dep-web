using Nep.Project.DBModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.IServices
{
    public interface IAttachmentService
    {
        ServiceModels.ReturnObject<String> GetFileUrl(decimal attachmentTypeID);
        //ServiceModels.ReturnQueryData<ServiceModels.Attachment> UploadProjectFile(List<Http> attachmentList, decimal projectID);
        //ServiceModels.ReturnQueryData<ServiceModels.Attachment> UploadFile(List<HttpPostedFile> attachmentList, string folderName);
        ServiceModels.ReturnObject<ServiceModels.Attachment> GetAttachmentInfoForDownload(String sourcePage, String sourceId, decimal attachmentId);
        /// <summary>
        /// kenghot
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        List<ServiceModels.KendoAttachment> GetAttachmentOfTable(String TableName, String FieldName, decimal TableRowID);
        ServiceModels.ReturnObject<PROJECTQUESTIONHD> UploadImage(string imgGroupName, decimal? imgId, decimal dataKey, string base64);
        ServiceModels.ReturnObject<string> DeleteImage(decimal imageId,string groupCode);
    }
}
