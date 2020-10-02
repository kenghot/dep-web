using Autofac.Integration.Web.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Drawing;
using Nep.Project.DBModels;
using Nep.Project.ServiceModels.ProjectInfo;

namespace Nep.Project.Web.Infra
{
    [InjectProperties]
    public class ServicesHandler : IHttpHandler
    {
        const string REPORT_PROCESS = "REPPROC";
        const string REPORT_IMAGE = "REPIMG";
        const string REPORT_DETAIL = "REPDETAIL";

        const string GETDATA_THUMBNAIL = "GETTHUMBNAIL";
        const string GETDATA_IMAGE = "GETIMAGE";
        public IServices.IAuthenticationService _authSerive { get; set; }
        
        public IServices.IProviceService __provinceService { get; set; }
        //public IServices.IRegisterService _registerService { get; set; }
        public IServices.IOrganizationService _organizationService { get; set; }
        public IServices.IProjectInfoService _projService { get; set; }
        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }
        public class InputData
        {
            public List<string> Controls { get; set; }
            public string QNGroup { get; set; }
            public decimal ProjID { get; set; }
            public object QNData { get; set; }
            public string IsReported { get; set; }
        }
        protected void ClearResponse(HttpContext context)
        {
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Clear();
        }
        protected void EnableCSRO(HttpContext context)
        {
            ClearResponse(context);
            //Disable caching
            SetNoCacheHeaders(context);
            //Set allowed origin
            SetAllowCrossSiteRequestOrigin(context);
        }
        private void SetAllowCrossSiteRequestOrigin(HttpContext context)
        {
            string origin = context.Request.Headers["Origin"];
            if (!String.IsNullOrEmpty(origin))
                //You can make some sophisticated checks here
                context.Response.AppendHeader("Access-Control-Allow-Origin", origin);
            else
                //This is necessary for Chrome/Safari actual request
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        }
        protected void SetNoCacheHeaders(HttpContext context)
        {
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            context.Response.Cache.SetValidUntilExpires(false);
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
        }
        public void ProcessRequest(HttpContext context)
        {

            //EnableCSRO(context);

            var action = Path.GetFileName(context.Request.FilePath).ToLower();
            var httpMethod = context.Request.HttpMethod.ToLower();

            if (action.Equals("login"))
            {
                Login(context);
            }
            if (action.Equals("getlistofproject"))
            {
                GetListOfProject(context);
            }

            if (action.Equals("checkdesperson"))
            {
                CheckDESPerson(context);
            }
            if (action.Equals("saveqn"))
            {
                SaveQNData(context);
            }
            if (action.Equals("getqn"))
            {
                GetQNData(context);
            }
            if (action.Equals("getdatabyact"))
            {
                GetDataByAction(context);
            }
            if (action.Equals("getprojrep"))
            {
                GetProjRep(context);
            }
            if (action.Equals("saveprojrep"))
            {
                SaveProjRep(context);
            }
            if (action.Equals("getprojects"))
            {
                GetProjects(context);
            }
        }
        private void CheckDESPerson(HttpContext context)
        {

            ServiceModels.ReturnObject<List<string>> ret = new ServiceModels.ReturnObject<List<string>>();

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
                var jsons = json["items"].ToList();
                ret.Data = new List<string>();
                foreach (var j in jsons)
                {
                    var id = j["IDCardNo"].ToString();
                    var p = API.APIHelper.GetDesPerson(id);
                    var msg = "";
                     if (!p.IsCompleted)
                    {
                        msg = p.Message[0];
                    } else
                    {
                        var pfullname = p.Data.maimad_details[0].first_name_thai.ToString().Trim() + " " + p.Data.maimad_details[0].last_name_thai.ToString().Trim();
                        var jfullname = j["FirstName"].ToString().Trim() + " " + j["LastName"].ToString().Trim();
                        if (pfullname != jfullname)
                        {
                            msg += "ชื่อไม่ตรงกับระบบ : " + pfullname + "\n";
                        }
                        if (j["DdlGender"]["Value"].ToString() != p.Data.maimad_details[0].sex_code)
                        {
                            msg += "เพศไม่ตรงกับระบบ : " + (p.Data.maimad_details[0].sex_code == "F" ? "หญิง" : "ชาย") + "\n";
                        }
                    }
                    ret.Data.Add(msg);
                }
                ret.IsCompleted = true;
               

            }
            catch (Exception ex)
            {

                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        public class QNInput
        {
            public string QNGroup { get; set; }
            public decimal ProjID { get; set; }
            public object QNData { get; set; }
            public string IsReported { get; set; }
            public string Action { get; set; }
            public decimal QNID { get; set; }
            public int Take { get; set; }

        }
        public class RepRequest
        {
            public decimal? Key { get; set; }
            public string Action { get; set; }
            public string QNGroup { get; set; }
            public Newtonsoft.Json.Linq.JObject Data { get; set; }
            public decimal UserID { get; set; }
            public decimal ProjID { get; set; }
        }
        public class RepResponse
        {
            public decimal QNID { get; set; }
            public decimal QNHDID { get; set; }

        }
        public class RepClass
        {
            public decimal ProjectID { get; set; }
            public String ProjectName { get; set; }
            public String Comment { get; set; }
            public Decimal Latitude { get; set; }
            public Decimal Longitude { get; set; }
            public String UserName { get; set; }
            public DateTime CreatedDT { get; set; }
            public decimal QNHDID { get; set; }
            public decimal QNID { get; set; }
            public byte[] UserImage { get; set; }
            public List<RepImgClass> Images { get; set; }
        }
        public class RepImgClass
        {
            public decimal QNID { get; set; }
            public DateTime CreatedDT { get; set; }
            public byte[] Image { get; set; }
        }
        public class RepImg64Class
        {
            public DateTime CreatedDT { get; set; }
            public String Image { get; set; }
        }
        /// <summary>
        /// Get data for nep mobile (activity list)
        /// </summary>
        /// <param name="context"></param>
        private void GetProjRep(HttpContext context)
        {
           

            ServiceModels.ReturnObject<List<RepClass>> ret = new ServiceModels.ReturnObject<List<RepClass>>();
            ret.IsCompleted = false;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
                if ( json == null)
                {
                    throw new Exception("Invalid json input");
                 
                }
                decimal projID;
                if (!decimal.TryParse(json["projID"].ToString(),out projID))
                {
                    throw new Exception("Invalid project id.");
                }
 
                var obj = Newtonsoft.Json.Linq.JObject.Parse(strValues).ToObject<QNInput>();

                ret.IsCompleted = true;
                var db = _projService.GetDB();
                ret.Data = new List<RepClass>();
                var data = ret.Data;
          
                //"REPIMG"
                //"REPCHECKIN"
                var q = from qs in db.PROJECTQUESTIONHDs where qs.PROJECTID == projID && qs.QUESTGROUP == REPORT_PROCESS orderby  qs.QUESTHDID descending  select qs;
                var p = db.ProjectInformations.Where(f => f.ProjectID == projID).FirstOrDefault();
              
                foreach (var qTmp in q)
                {
                    var rep = new RepClass();
                    data.Add(rep);
                    var u = db.SC_User.Where(user => user.UserID == q.FirstOrDefault().CREATEDBYID).FirstOrDefault();
                    rep.UserName = u.FirstName + " " + u.LastName;
                    rep.CreatedDT = qTmp.CREATEDDATE;
                    rep.ProjectName = p.ProjectNameTH;
                    rep.ProjectID = p.ProjectID;
                    rep.QNHDID = qTmp.QUESTHDID;
                    
                    rep.Images = new List<RepImgClass>();
                    var qn = db.PROJECTQUESTIONs.Where(w => w.QUESTHDID == qTmp.QUESTHDID).OrderByDescending(o => o.QUESTTID);
                    foreach (var qnTmp in qn)
                    {
                        if (qnTmp.QFIELD == REPORT_DETAIL)
                        {
                            var detail = Newtonsoft.Json.Linq.JObject.Parse(qnTmp.QVALUE).ToObject<RepClass>();
                            rep.Comment = detail.Comment;
                            rep.Latitude = detail.Latitude;
                            rep.Longitude = detail.Longitude;
                            rep.QNID = qnTmp.QUESTTID;
                        }
                        if (qnTmp.QFIELD == REPORT_IMAGE)
                        {
                            var img = Newtonsoft.Json.Linq.JObject.Parse(qnTmp.QVALUE).ToObject<RepImg64Class>();
                            if (img != null)
                            {
                                var objimg = new RepImgClass();
                                objimg.CreatedDT = img.CreatedDT;
                                objimg.QNID = qnTmp.QUESTTID;
 
                                byte[] bytes = Convert.FromBase64String(img.Image);

                                Image image;
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                     image = Image.FromStream(ms);
                                }
                                    using (System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 160, null, IntPtr.Zero))
                                    {
                                        using (MemoryStream memoryStream = new MemoryStream())
                                        {
                                            thumbnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                            Byte[] b = new Byte[memoryStream.Length];
                                            memoryStream.Position = 0;
                                            memoryStream.Read(b, 0, (int)b.Length);
                                        objimg.Image = b; // Convert.ToBase64String(b, 0, b.Length);
                                            
                                        }
                                    }

                              
                                //objimg.Image = System.Convert.FromBase64String(img.Image);
                                rep.Images.Add(objimg);
                            }

                        }
                        
                    }
                    
                }

            }
            catch (Exception ex)
            {

                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }

        private void GetDataByAction(HttpContext context)
        {


            ServiceModels.ReturnObject<List<string>> ret = new ServiceModels.ReturnObject<List<string>>();
            ret.IsCompleted = false;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
                if (json == null)
                {
                    throw new Exception("Invalid json input");

                }
                decimal projID;
                if (!decimal.TryParse(json["ProjID"].ToString(), out projID))
                {
                    throw new Exception("Invalid project id.");
                }

                var obj = Newtonsoft.Json.Linq.JObject.Parse(strValues).ToObject<QNInput>();

                ret.IsCompleted = true;
                var db = _projService.GetDB();
                ret.Data = new List<string>();
                var data = ret.Data;

                if (obj.Action == GETDATA_THUMBNAIL || obj.Action == GETDATA_IMAGE)
                {
                    var q = from qs in db.PROJECTQUESTIONs
                            where qs.PROJECTQUESTIONHD.PROJECTID == projID && qs.PROJECTQUESTIONHD.QUESTGROUP == REPORT_PROCESS &&
                            qs.QFIELD == REPORT_IMAGE
                            select qs;
                    if (obj.Take > 0)
                    {
                        q = q.Take(obj.Take);
                    }
                    if (obj.Action == GETDATA_THUMBNAIL)
                    {
                        var qList = q.ToList();
                        for (var i=0; i < qList.Count(); i++) 
                        {
                            var qTmp = qList[i];
                            var img = Newtonsoft.Json.Linq.JObject.Parse(qTmp.QVALUE).ToObject<RepImg64Class>();
                            if (img != null)
                            {
                                var objimg = new RepImgClass();
                                objimg.CreatedDT = img.CreatedDT;


                                byte[] bytes = Convert.FromBase64String(img.Image);
                                //byte[] bytes = File.ReadAllBytes("E:\\Documents\\Pictures\\test.jpg");
                                Image image;
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    
                                    image = Image.FromStream(ms);
                                }
                                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(60, 80, null, IntPtr.Zero))
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        thumbnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                        Byte[] b = new Byte[memoryStream.Length];
                                        memoryStream.Position = 0;
                                        memoryStream.Read(b, 0, (int)b.Length);
                                        // objimg.Image = b; 
                                        ret.Data.Add(Convert.ToBase64String(b, 0, b.Length));
                                    }
                                }

                            }
                        }
                    }

                }
     

            }
            catch (Exception ex)
            {

                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void SaveProjRep(HttpContext context)
        {

            ServiceModels.ReturnObject<RepResponse> ret = new ServiceModels.ReturnObject<RepResponse>();
            ret.IsCompleted = false;
            ret.Data = new RepResponse();
            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var obj = Newtonsoft.Json.Linq.JObject.Parse(strValues).ToObject<RepRequest>();

                ret.IsCompleted = true;
                var db = _projService.GetDB();
                                        var user = db.SC_User.Where(w => w.UserID == obj.UserID).FirstOrDefault();
                        if (user == null)
                        {
                            throw new Exception("User is not found.");
                        }
                //var q = (from qs in db.PROJECTQUESTIONHDs where qs.PROJECTID == obj.ProjID && qs.QUESTGROUP == obj.QNGroup   select qs).FirstOrDefault();
                if (obj.Action == "delete")
                {
                    if (obj.QNGroup == REPORT_PROCESS)
                    {
                        var d = db.PROJECTQUESTIONHDs.Where(w => w.QUESTHDID == obj.Key).FirstOrDefault();
                        if (d != null)
                        {
                            db.PROJECTQUESTIONHDs.Remove(d);
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        var dq = db.PROJECTQUESTIONs.Where(w => w.QUESTTID == obj.Key).FirstOrDefault();
                        if (dq != null)
                        {
                            db.PROJECTQUESTIONs.Remove(dq);
                            db.SaveChanges();
                        }
                    }

                }
                if (obj.Action == "insert")
                {
                    if (obj.QNGroup == REPORT_PROCESS)
                    {

                        var qhd = new DBModels.Model.PROJECTQUESTIONHD();
                        qhd.DATA = strValues;
                        qhd.CREATEDBYID = obj.UserID;
                        qhd.CREATEDBY = user.UserName;
                        qhd.CREATEDDATE = DateTime.Now;
                        qhd.ISREPORTED = " ";
                        qhd.PROJECTID = obj.ProjID;
                        qhd.QUESTGROUP = REPORT_PROCESS;
                        var q = new DBModels.Model.PROJECTQUESTION();
                        qhd.PROJECTQUESTIONs.Add(q);
                        q.QFIELD = REPORT_DETAIL;
                        q.QTYPE = "x";
                        // var json = Newtonsoft.Json.Linq.JObject.Parse(obj.Data);
                        q.QVALUE = obj.Data.ToString(); //json.ToString();
                        db.PROJECTQUESTIONHDs.Add(qhd);
                        db.SaveChanges();
                        ret.Data.QNHDID = qhd.QUESTHDID;
                        ret.Data.QNID = q.QUESTTID;
                    }
                    if (obj.QNGroup == REPORT_IMAGE)
                    {
                        var qhd = db.PROJECTQUESTIONHDs.Where(w => w.QUESTHDID == obj.Key).FirstOrDefault();
                        if (qhd == null)
                        {
                            throw new Exception("QUESTHDID is not found");
                        }
                        qhd.UPDATEDBYID = obj.UserID;
                        qhd.UPDATEDBY = user.UserName;
                        qhd.UPDATEDDATE = DateTime.Now;
                        qhd.ISREPORTED = " ";
                        var q = new DBModels.Model.PROJECTQUESTION();
                        qhd.PROJECTQUESTIONs.Add(q);
                        q.QFIELD = REPORT_IMAGE;
                        q.QTYPE = "x";
                        // var json = Newtonsoft.Json.Linq.JObject.Parse(obj.Data);
                        q.QVALUE = obj.Data.ToString(); //json.ToString();
                  
                        db.SaveChanges();
                        ret.Data.QNHDID = qhd.QUESTHDID;
                        ret.Data.QNID = q.QUESTTID;
                    }

                }
                if (obj.Action == "update")
                {
                    var q = db.PROJECTQUESTIONs.Where(w => w.QUESTTID == obj.Key.Value).FirstOrDefault();
                    if (q != null)
                    {
                        q.QVALUE = obj.Data.ToString();
                        db.SaveChanges();
                        ret.Data.QNHDID = q.QUESTHDID;
                        ret.Data.QNID = q.QUESTTID;
                        //ret.Data.QNID = obj.Key.Value;
                    }
                }

            }
            catch (Exception ex)
            {
                ret.Data = null;
                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void GetQNData(HttpContext context)
        {

            ServiceModels.ReturnObject<List<QNInput>> ret = new ServiceModels.ReturnObject<List<QNInput>>();
            ret.IsCompleted = false;
           
            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var obj = Newtonsoft.Json.Linq.JObject.Parse(strValues).ToObject<QNInput>();

                ret.IsCompleted = true;
                var db = _projService.GetDB();
                ret.Data = new List<QNInput>();
                List<string> w = new List<string>();
                if (string.IsNullOrEmpty(obj.QNGroup))
                {
                    w.Add("REPIMG");
                    w.Add("REPCHECKIN");
                }
                else
                {
                    w.Add(obj.QNGroup);
                }
                var q = from qs in db.PROJECTQUESTIONHDs where qs.PROJECTID == obj.ProjID && w.Contains(qs.QUESTGROUP) select qs;
                
                foreach(var tmp in  q)
                {
                    var n = new QNInput();
                    n.ProjID = tmp.PROJECTID;
                    n.QNGroup = tmp.QUESTGROUP;
                    n.QNData = tmp.DATA;
                    n.QNID = tmp.QUESTHDID;
                    ret.Data.Add(n);
                }
          
            }
            catch (Exception ex)
            {
               
                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void SaveQNData(HttpContext context)
        {

            ServiceModels.ReturnObject<bool> ret = new ServiceModels.ReturnObject<bool>();
            ret.IsCompleted = false;
            ret.Data = true;
            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var obj = Newtonsoft.Json.Linq.JObject.Parse(strValues).ToObject<QNInput>();
               
                ret.IsCompleted = true;
                var db = _projService.GetDB();
       
                //var q = (from qs in db.PROJECTQUESTIONHDs where qs.PROJECTID == obj.ProjID && qs.QUESTGROUP == obj.QNGroup   select qs).FirstOrDefault();
                if (obj.Action == "delete")
                {
                    var d = (from qs in db.PROJECTQUESTIONHDs where qs.QUESTHDID == obj.QNID select qs).FirstOrDefault();
                    if (d != null)
                    {
                        db.PROJECTQUESTIONHDs.Remove(d);
                        db.SaveChanges();
                        ret.IsCompleted = true;
                        ret.Data = true;
                         
                    }
                } 
                if (obj.Action == "insert")
                {
                    var nq = new DBModels.Model.PROJECTQUESTIONHD();
                    nq.CREATEDBY = "1";
                    nq.CREATEDBYID = 1;
                    nq.CREATEDDATE = DateTime.Now;
                    nq.DATA = obj.QNData.ToString();
                    if (obj.QNGroup == "REPIMG")
                    {
                        byte[] bytes = Convert.FromBase64String(nq.DATA);

                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                     
                        using (System.Drawing.Image thumbnail = image.GetThumbnailImage(100, 100, null , IntPtr.Zero))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                thumbnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                Byte[] b = new Byte[memoryStream.Length];
                                memoryStream.Position = 0;
                                memoryStream.Read(b, 0, (int)b.Length);
                                nq.DATA = Convert.ToBase64String(b, 0, b.Length);
                                
                            }
                        }

                    }
                    nq.ISREPORTED = " ";
                    nq.PROJECTID = obj.ProjID;
                    nq.QUESTGROUP = obj.QNGroup;
                    db.PROJECTQUESTIONHDs.Add(nq);
                    db.SaveChanges();

                }
                if (obj.QNGroup == "REPLOC")
                {
                    DBModels.Model.PROJECTQUESTIONHD loc;
                      loc = (from l in db.PROJECTQUESTIONHDs where l.PROJECTID == obj.ProjID && l.QUESTGROUP == obj.QNGroup select l).FirstOrDefault();
                    
                    if (loc == null)
                    {
                        loc = new DBModels.Model.PROJECTQUESTIONHD();
                        loc.CREATEDBY = "1";
                        loc.CREATEDBYID = 1;
                        loc.CREATEDDATE = DateTime.Now;
                       

                        loc.ISREPORTED = " ";
                        loc.PROJECTID = obj.ProjID;
                        loc.QUESTGROUP = obj.QNGroup;
                        db.PROJECTQUESTIONHDs.Add(loc);
                        
                    }
                    loc.DATA = obj.QNData.ToString();
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                ret.Data = false;
                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private class ProjectInfo
        {
            public decimal ProjectID { get; set; }
            public string ProjectTHName { get; set; }
            public string OrganizationName { get; set; }
            public byte[] Thumbnail { get; set; }
        }
        private void GetListOfProject(HttpContext context)
        {

            ServiceModels.ReturnObject<List<ProjectInfo>> ret = new ServiceModels.ReturnObject<List<ProjectInfo>>();
            ret.IsCompleted = false;
            
            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                //var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);

                //string projID = null;
                //if (json["ProjectID"] != null)
                //{
                //    projID = json["ProjectID"].ToString();
                //}
              
                var pageIndex = 0;
                var pageSize = 20;

                List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
                
                ////ชื่อองค์กร
                //if (!String.IsNullOrEmpty(projID))
                //{
                //    decimal dProj;
                //    if (decimal.TryParse(projID ,out dProj))
                //    {
                //        fields.Add(new ServiceModels.FilterDescriptor()
                //        {
                //            Field = "ProjectID",
                //            Operator = ServiceModels.FilterOperator.IsEqualTo,
                //            Value = dProj
                //        });
                //    }
                   
                //}
                //var pass = json["password"].ToString();
                var param = Nep.Project.Common.Web.NepHelper.ToQueryParameter(fields, pageIndex, pageSize, "CreatedDate", System.Web.UI.WebControls.SortDirection.Descending);
                var result = _projService.ListProjectInfoList(param,false);
                if (result.IsCompleted)
                {
                    ret.IsCompleted = true;
                    ret.Data = result.Data.Select(s => new ProjectInfo {  ProjectID = s.ProjectInfoID , ProjectTHName = s.ProjectName
                    ,OrganizationName = s.OrganizationName }).ToList();
                }

            }
            catch (Exception ex)
            {

                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            //String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);
            var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
            { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
               
            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void Login(HttpContext context)
        {

            ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo> ret = new ServiceModels.ReturnObject<ServiceModels.Security.SecurityInfo>();

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
                
                var user = json["userCode"].ToString();
                var pass = json["password"].ToString();
                ret = _authSerive.Login(user, pass);
    

            }
            catch (Exception ex)
            {
               
                ret.IsCompleted = false;
                ret.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(ret);

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
        private void GetActivityBudget(HttpContext context)
        {

            ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> act;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }
                //var st = context.Request.InputStream()
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
                // var controls = data.Controls.Select(x => x).Distinct().ToList();
                act = _projService.GetProjectBudgetInfoByProjectID(data.ProjID);
              //  qn = projService.SaveQNData(data.ProjID, data.QNGroup, data.IsReported, data.QNData.ToString());



            }
            catch (Exception ex)
            {
                act = new ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>();
                act.IsCompleted = false;
                act.Message.Add(ex.Message);
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);

            }
            context.Response.ContentType = "application/json; charset=utf-8";
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(act);

            context.Response.Write(responseText);
            //context.Response.Write(json);
            context.Response.End();
        }
      
        private void GetQuestionare(HttpContext context)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();
            ServiceModels.ReturnObject<List<ServiceModels.ProjectInfo.Questionare>> qn;
            string retjs = "";
            try
            {
                String strValues = context.Request.Form[0];
                //String strProvinceID = context.Request.Form["values[1]"];
                InputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(strValues);
                var controls = data.Controls.Select(x => x).Distinct().ToList();

                qn = _projService.GetProjectQuestionare(data.ProjID, data.QNGroup);
                retjs = Business.QuestionareHelper.ConvertQNToJson(qn.Data,controls);
                context.Response.ContentType = "application/json; charset=utf-8";
                String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(retjs);
               
                context.Response.Write(responseText);
                //context.Response.Write(json);
                context.Response.End();

            }
            catch (Exception ex)
            {
                //result.Data = new List<Int32>();
                //result.Message.Add(ex.Message);
              
            }

        
        } 
   

        private void WriteSearchOrgProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = __provinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                list = result.Data;                
                list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
            }
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteOrgProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = __provinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                list = result.Data;
                list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
            }
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteProvince(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result;
            result = __provinceService.ListProvince(null);
            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteDistrict(HttpContext context)
        {            
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            var param = context.Request.Params["parentid"];
            if(param != null){
                int id = 0;
                Int32.TryParse(param.ToString(), out id);
                result = __provinceService.ListDistrict(id, null);
               
            }

            if (result.Data == null)
            {
                result.Data = new List<ServiceModels.GenericDropDownListData>();
            }

            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteSubDistrict(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            var param = context.Request.Params["parentid"];
            if (param != null)
            {
                int id = 0;
                Int32.TryParse(param.ToString(), out id);
                result = __provinceService.ListSubDistrict(id, null);

               
            }

            if (result.Data == null)
            {
                result.Data = new List<ServiceModels.GenericDropDownListData>();
            }

            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }

        private void WriteOrg(HttpContext context)
        {
            ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData>();

            try
            {
                String parentId = context.Request.Form["parentId"];
                String strPage = context.Request.Form["page"];
                String strPageSize = context.Request.Form["pageSize"];
                String orgName = context.Request.Form["filter[filters][0][value]"];
                String orgNameField = context.Request.Form["filter[filters][0][field]"];
                Int32 pageIndex = 0;
                Int32 pageSize = 40;
                Int32 tmpPageSize = 40;

                List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
                if (!String.IsNullOrEmpty(parentId))
                {
                    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ParentID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = Convert.ToDecimal(parentId) });
                }
                if (!String.IsNullOrEmpty(orgName))
                {
                    fields.Add(new ServiceModels.FilterDescriptor() { Field = orgNameField, Operator = ServiceModels.FilterOperator.Contains, Value = orgName.Trim() });
                }
                if (!String.IsNullOrEmpty(strPage) && Int32.TryParse(strPage, out pageIndex))
                {                   
                    pageIndex = pageIndex - 1;
                }
                if (!String.IsNullOrEmpty(strPageSize) && Int32.TryParse(strPageSize, out tmpPageSize))
                {
                    pageSize = tmpPageSize;
                }


                List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
                if (fields.Count > 0)
                {
                    filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                    {
                        LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                        FilterDescriptors = fields
                    });
                }

                ServiceModels.QueryParameter param = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filterComposite, pageIndex, pageSize, "Text", System.Web.UI.WebControls.SortDirection.Ascending);

                result = _organizationService.ListDropDown(param);

                if (result.Data == null)
                {
                    result.Data = new List<ServiceModels.DecimalDropDownListData>();
                }
            }
            catch (Exception ex)
            {
                result.Data = new List<ServiceModels.DecimalDropDownListData>();
                result.Message.Add(ex.Message);
            }


            String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(result);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(responseText);
            context.Response.End();
        }


        private void GetProjects(HttpContext context)
        {

            ServiceModels.ReturnObject<List<GetProjectResponse>> rep = new ServiceModels.ReturnObject<List<GetProjectResponse>>();
            rep.IsCompleted = false;

            try
            {
                String strValues = ""; //= context.Request.Form[0];
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    strValues = reader.ReadToEnd();
                }

                var json = Newtonsoft.Json.Linq.JObject.Parse(strValues);
                string idno = null;
             
                if (json["IdCardNo"] != null)
                {
                    idno = json["IdCardNo"].ToString();
                }
                
                if (string.IsNullOrEmpty(idno))
                {
                    rep.Message.Add("IdCardNo is required.");
                    return;
                }
                var db = _projService.GetDB();
                var parts = db.ProjectParticipants.Where(w => w.IDCardNo == idno).Select(s => s.ProjectID).Distinct().ToList();
                var pns = db.ProjectPersonels.Where(w => w.IDCardNo == idno).Select(s => s.ProjectID).Distinct().ToList();
                parts.AddRange(pns);
                parts.Distinct();

                var contracts = db.ProjectContracts
                    .Join(db.MT_Province, pj => pj.ProjectGeneralInfo.ProvinceID, pv => pv.ProvinceID,(pj,pv) => new { pj, pv })
                    .Where(w => parts.Contains(w.pj.ProjectID))
                    .Select(s => new GetProjectResponse
                    {
                        BudgetAmount = s.pj.ProjectGeneralInfo.BudgetValue.HasValue ? s.pj.ProjectGeneralInfo.BudgetValue.Value : 0,
                        BudgetYear = s.pj.ProjectGeneralInfo.ProjectInformation.BudgetYear,
                        ContractStartDate = s.pj.ContractStartDate,
                        ContractNo = s.pj.ContractNo,
                        OrganizationName = s.pj.ProjectGeneralInfo.OrganizationNameTH,
                        ProjectName = s.pj.ProjectGeneralInfo.ProjectInformation.ProjectNameTH,
                        ProvinceName = s.pv.ProvinceName
                        
                    }).ToList();
                rep.Data = contracts;
                rep.IsCompleted = true;
            }
            catch (Exception ex)
            {
                rep.Message.Add(ex.Message);

            }
            finally
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                String responseText = Nep.Project.Common.Web.WebUtility.ToJSON(rep);

                context.Response.Write(responseText);
                //context.Response.Write(json);
                context.Response.End();
            }
        }
    }
}