using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;
using Nep.Project.Common.Web;
using Nep.Project.DBModels.Model;
using System.Drawing;
using System.IO;

namespace Nep.Project.Business
{
    public class AttachmentService : IServices.IAttachmentService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;

        public AttachmentService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;           
        }

        public ServiceModels.ReturnObject<string> GetFileUrl(decimal attachmentTypeID)
        {            
            //throw new NotImplementedException();
            return null;
        }
        /// <summary>
        /// For uploading file to physical path. Physical format path is {root}/{parenID}/{attachmentTypeID}/{filename}
        /// </summary>
        /// <param name="attachmentList"></param>
        /// <param name="attachmentTypeID"></param>
        /// <param name="parentID">It's project id.</param>
        /// <returns></returns>
        public ServiceModels.ReturnQueryData<ServiceModels.Attachment> UploadFile(List<ServiceModels.Attachment> attachmentList, decimal attachmentTypeID, decimal parentID)
        {
            //throw new NotImplementedException();
            return null;
        }

        public ServiceModels.ReturnObject<ServiceModels.Attachment> GetAttachmentInfoForDownload(String sourcePage, String sourceId, decimal attachmentId)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.Attachment>();
            var attachment = _db.MT_Attachment.Select(x => new ServiceModels.Attachment()
            {
                AttachmentID = x.AttachmentID, PathName = x.PathName, FileSize = x.FileSize, AttachmentFileName = x.AttachmentFilename
            }).SingleOrDefault(x => x.AttachmentID == attachmentId);

            if (attachment != null) {
                var path = _db.MT_OrganizationParameter.Single(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).ParameterValue;

                attachment.PathName = System.IO.Path.Combine(path, attachment.PathName);
            }

            result.Data = attachment;
            result.IsCompleted = true;
            return result;
        }

        public List<ServiceModels.KendoAttachment> GetAttachmentOfTable(string TableName, string FieldName, decimal TableRowID)
        {
            //string cmd = "SELECT A.* FROM K_FILEINTABLE F INNER JOIN MT_ATTACHMENT A ON A.ATTACHMENTID = F.ATTACHMENTID " + 
            //             string.Format("WHERE F.TABLENAME='{0}' AND F.TABLEROWID={1} AND F.FIELDNAME='{2}'", TableName, TableRowID.ToString(),FieldName);

            //var q = _db.Database.SqlQuery<DBModels.Model.MT_Attachment>(cmd);
            var att = _db.K_FILEINTABLE.Where(w => w.TABLENAME == TableName && w.TABLEROWID == TableRowID);
            if (!string.IsNullOrEmpty(FieldName))
            {
                att = att.Where(w => w.FIELDNAME == FieldName);
            }
            
            var ret = new List<ServiceModels.KendoAttachment>();
            foreach (DBModels.Model.K_FILEINTABLE tmp in att)
            {
                if (tmp.ATTACHMENTID > 0)
                {
                    var q = (from qs in _db.MT_Attachment where qs.AttachmentID == tmp.ATTACHMENTID select qs).FirstOrDefault();
                    if (q != null)
                    {
                        //NepHelper.WriteLog(q.AttachmentFilename);
                        ret.Add(new ServiceModels.KendoAttachment
                        {
                     
                            id = q.AttachmentID.ToString(),
                            fieldName = tmp.FIELDNAME ,
                            name = q.AttachmentFilename,
                            extension = System.IO.Path.GetExtension(q.AttachmentFilename),
                            size = (int)q.FileSize
                        });
                    }
                }
          
            }
            return ret ;
      
        }
        public ServiceModels.ReturnObject<PROJECTQUESTIONHD> UploadImage(string imgGroupName, decimal? imgId, decimal dataKey, string base64)
        {
            var result = new ServiceModels.ReturnObject<PROJECTQUESTIONHD>();
            result.IsCompleted = false;
            try
            {
                var bytes = Convert.FromBase64String(base64);
 

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                var path = System.Web.HttpContext.Current.Server.MapPath("/UploadImages/");
                var fname = Guid.NewGuid() + ".png";
                image.Save(path + fname, System.Drawing.Imaging.ImageFormat.Png);

                PROJECTQUESTIONHD img = null;
                if (imgId.HasValue)
                {
                    img = _db.PROJECTQUESTIONHDs.Where(w => w.QUESTHDID == imgId.Value && w.QUESTGROUP == imgGroupName).FirstOrDefault();
                }
                if (img == null)
                {
                    img = new PROJECTQUESTIONHD
                    {
                        ISREPORTED = "1",
                        CREATEDBY = "system",
                        CREATEDBYID = 1,
                        CREATEDDATE = DateTime.Now,
                        PROJECTID = dataKey,
                        QUESTGROUP = imgGroupName,
                        
                    };
                    _db.PROJECTQUESTIONHDs.Add(img);
                }
                img.DATA = fname;
                img.UPDATEDBY = "system";
                img.UPDATEDBYID = 1;
                img.UPDATEDDATE = DateTime.Now;

                _db.SaveChanges();
                result.Data = img;
                result.IsCompleted = true;
            }catch (Exception ex)
            {
                result.Message.Add(ex.Message + " | " + ex.InnerException?.Message);
            }
            return result;
        }
    }
}
