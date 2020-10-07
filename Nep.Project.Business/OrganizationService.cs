using Nep.Project.Common;
using Nep.Project.DBModels.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;

namespace Nep.Project.Business
{
    public class OrganizationService : IServices.IOrganizationService
    {
        private readonly NepProjectDBEntities _db;
        private readonly ServiceModels.Security.SecurityInfo _loggedUser;
        private readonly IServices.IProviceService _provinceService;

        public OrganizationService(NepProjectDBEntities db, ServiceModels.Security.SecurityInfo loggedUser, IServices.IProviceService provinceService)
        {
            _db = db;
            _loggedUser = loggedUser;
            _provinceService = provinceService;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.OrganizationList> List(ServiceModels.QueryParameter param)
        {

            var query = (from o in _db.MT_Organization
                         join project in
                             (from p in _db.ProjectGeneralInfoes
                              group p by p.OrganizationID into pg
                              select new { OrganizationID = pg.Key })
                                 on new { o.OrganizationID } equals new { project.OrganizationID } into tmpP
                         from orgP in tmpP.DefaultIfEmpty()
                         select new ServiceModels.OrganizationList
                         {
                             OrganizationID = o.OrganizationID,
                             OrganizationName = o.OrganizationNameTH,
                             //OrganizationNo = o.OrganizationNo,
                             OrganizationUnder = o.OrgUnderSupport,
                             Address = o.Address,
                             Building = o.Building,
                             District = o.District.DistrictName,
                             DistrictID = o.DistrictID,
                             Moo = o.Moo,
                             PostCode = o.PostCode,
                             Province = o.Province.ProvinceName,
                             ProvinceID = o.ProvinceID,
                             Road = o.Road,
                             Soi = o.Soi,
                             SubDistrict = o.SubDistrict.SubDistrictName,
                             SubDistrictID = o.SubDistrictID,

                             IsDeletable = (orgP == null)
                         });
            return query.ToQueryData(param);
        }

        public ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Get(Decimal id)
        {

            var result = new ServiceModels.ReturnObject<ServiceModels.OrganizationProfile>();
            var data = (from o in _db.MT_Organization
                        join project in
                            (from p in _db.ProjectGeneralInfoes
                             group p by p.OrganizationID into pg
                             select new { OrganizationID = pg.Key })
                                  on new { o.OrganizationID } equals new { project.OrganizationID } into tmpP
                        from orgP in tmpP.DefaultIfEmpty()
                        where o.OrganizationID == id
                        select new ServiceModels.OrganizationProfile
                        {
                            Address = o.Address,
                            Building = o.Building,
                            DistrictID = o.DistrictID,
                            Email = o.Email,
                            Fax = o.Fax,
                            Moo = o.Moo,
                            OrganizationID = o.OrganizationID,
                            OrganizationNameEN = o.OrganizationNameEN,
                            OrganizationNameTH = o.OrganizationNameTH,
                            OrganizationType = o.OrganizationTypeID,
                            OrganizationTypeEtc = o.OrganizationTypeEtc,
                            OrganizationYear = o.OrganizationYear,
                            OrgEstablishedDate = o.OrgEstablishedDate,
                            OrganizationUnder = o.OrgUnderSupport,
                            PostCode = o.PostCode,
                            ProvinceID = o.ProvinceID,
                            Road = o.Road,
                            Soi = o.Soi,
                            SubDistrictID = o.SubDistrictID,
                            Telephone = o.Telephone,
                            Mobile = o.Mobile,
                            IsDeleteable = (orgP == null),
                            ExtendJSON = o.EXTENDDATA
                        })
                            .FirstOrDefault();

            if (data != null)
            {

                try
                {
                    data.ExtendData = Newtonsoft.Json.JsonConvert.DeserializeObject<OrganizationExtend>(data.ExtendJSON);
                }
                catch (Exception ex)
                {

                }

                result.Data = data;
                result.IsCompleted = true;
            }
            else
            {
                result.IsCompleted = false;
                result.Message.Add(Resources.Message.NoRecord);
            }
            return result;
        }

        private Boolean CheckOrganizationProfile(ServiceModels.OrganizationProfile model, Boolean isCreate)
        {
            return true;
        }

        public ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Create(ServiceModels.OrganizationProfile model)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.OrganizationProfile>();
            result.Data = model;
            string orgNameTh = model.OrganizationNameTH;
            string orgNameEn = model.OrganizationNameEN;
            var date = DateTime.Now.AddDays(-3);


            var chkOrg = (from o in _db.OrganizationRegisterEntries
                          where (o.OrganizationNameTH.Equals(orgNameTh) || (o.OrganizationNameEN != null) && o.OrganizationNameEN.Equals(orgNameEn)) && (o.RegisterDate < date)
                          select o).Count() == 0 &&
                                  (from o in _db.MT_Organization
                                   where (o.OrganizationNameTH.Equals(orgNameTh) || ((o.OrganizationNameEN != null) && o.OrganizationNameEN.Equals(orgNameEn))) && (o.OrganizationID != model.OrganizationID)
                                   select o).Count() == 0;

            if (chkOrg)
            {
                using (var tran = _db.Database.BeginTransaction())
                {
                    var newOrg = new DBModels.Model.MT_Organization()
                    {
                        Address = model.Address,
                        Building = model.Building,
                        CreatedBy = _loggedUser.UserName,
                        CreatedByID = (decimal)_loggedUser.UserID,
                        CreatedDate = DateTime.Now,
                        DistrictID = model.DistrictID,
                        Email = model.Email,
                        Fax = model.Fax,
                        Moo = model.Moo,
                        OrganizationNameEN = model.OrganizationNameEN,
                        OrganizationNameTH = model.OrganizationNameTH,
                        //OrganizationNo = model.,
                        OrganizationTypeEtc = model.OrganizationTypeEtc,
                        OrganizationTypeID = model.OrganizationType,
                        OrganizationYear = model.OrganizationYear,
                        OrgEstablishedDate = model.OrgEstablishedDate,
                        OrgUnderSupport = model.OrganizationUnder,
                        PostCode = model.PostCode,
                        ProvinceID = model.ProvinceID,
                        RequestDate = null,
                        RequesterName = null,
                        RequesterLastname = null,
                        Road = model.Road,
                        Soi = model.Soi,
                        SubDistrictID = model.SubDistrictID,
                        Telephone = model.Telephone,
                        Mobile = model.Mobile
                    };

                    _db.MT_Organization.Add(newOrg);
                    var saveResult = _db.SaveChanges();

                    if (saveResult >= 1)
                    {
                        tran.Commit();
                        model.OrganizationID = newOrg.OrganizationID;
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.Message.Add(Resources.Message.SaveFail);
                    }
                }
            }
            else
            {
                result.Message.Add(Nep.Project.Resources.Error.OrgNameRegistryDup);
            }




            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> Update(ServiceModels.OrganizationProfile model)
        {
            var result = new ServiceModels.ReturnObject<ServiceModels.OrganizationProfile>();
            result.Data = model;

            string orgNameTh = model.OrganizationNameTH;
            string orgNameEn = model.OrganizationNameEN;
            var date = DateTime.Now.AddDays(-3);
            var chkOrg = (from o in _db.OrganizationRegisterEntries
                          where (o.OrganizationNameTH.Equals(orgNameTh) || ((o.OrganizationNameEN != null) && o.OrganizationNameEN.Equals(orgNameEn))) && (o.RegisterDate < date)
                          select o).Count() == 0 &&
                                  (from o in _db.MT_Organization
                                   where (o.OrganizationNameTH.Equals(orgNameTh) || ((o.OrganizationNameEN != null) && o.OrganizationNameEN.Equals(orgNameEn))) && (o.OrganizationID != model.OrganizationID)
                                   select o).Count() == 0;
            if (chkOrg)
            {
                using (var tran = _db.Database.BeginTransaction())
                {
                    var newOrg = _db.MT_Organization
                                .Where(x => x.OrganizationID == model.OrganizationID)
                                .First();

                    newOrg.Address = model.Address;
                    newOrg.Building = model.Building;
                    newOrg.UpdatedBy = _loggedUser.UserName;
                    newOrg.UpdatedByID = _loggedUser.UserID;
                    newOrg.UpdatedDate = DateTime.Now;
                    newOrg.DistrictID = model.DistrictID;
                    newOrg.Email = model.Email;
                    newOrg.Fax = model.Fax;
                    newOrg.Moo = model.Moo;
                    newOrg.OrganizationNameEN = model.OrganizationNameEN;
                    newOrg.OrganizationNameTH = model.OrganizationNameTH;
                    newOrg.OrganizationTypeEtc = model.OrganizationTypeEtc;
                    newOrg.OrganizationTypeID = model.OrganizationType;
                    newOrg.OrganizationYear = model.OrganizationYear;
                    newOrg.OrgEstablishedDate = model.OrgEstablishedDate;
                    newOrg.OrgUnderSupport = model.OrganizationUnder;
                    newOrg.PostCode = model.PostCode;
                    newOrg.ProvinceID = model.ProvinceID; ;
                    newOrg.Road = model.Road;
                    newOrg.Soi = model.Soi;
                    newOrg.SubDistrictID = model.SubDistrictID;
                    newOrg.Telephone = model.Telephone;
                    newOrg.Mobile = model.Mobile;
                    if (model.ExtendData != null)
                    {
                        newOrg.EXTENDDATA = Newtonsoft.Json.JsonConvert.SerializeObject(model.ExtendData);
                    }
                    var saveResult = _db.SaveChanges();

                    if (saveResult >= 1)
                    {
                        tran.Commit();
                        result.IsCompleted = true;
                    }
                    else
                    {
                        result.Message.Add(Resources.Message.SaveFail);
                    }
                }
            }
            else
            {
                result.Message.Add(Nep.Project.Resources.Error.OrgNameRegistryDup);
            }




            return result;
        }

        public ServiceModels.ReturnMessage Remove(decimal organizationID)
        {
            ServiceModels.ReturnMessage result = new ServiceModels.ReturnMessage();
            try
            {
                DBModels.Model.MT_Organization org = _db.MT_Organization.Where(x => x.OrganizationID == organizationID).FirstOrDefault();
                List<DBModels.Model.UserRegisterEntry> userRegisterEntryList = _db.UserRegisterEntries.Where(x => x.OrganizationID == organizationID).ToList();
                List<DBModels.Model.SC_User> userList = _db.SC_User.Where(x => x.OrganizationID == organizationID).ToList();
                List<DBModels.Model.OrganizationRegisterEntry> orgRegisterEntryList = (from r in _db.OrganizationRegisterEntries
                                                                                       join ur in _db.UserRegisterEntries on r.UserEntryID equals ur.EntryID
                                                                                       where ur.OrganizationID == organizationID
                                                                                       select r
                                                                                      ).Distinct().ToList();



                List<DBModels.Model.MT_Attachment> idCardAttchList = _db.UserRegisterEntries.Where(x => x.OrganizationID == organizationID).Select(y => y.PersonalIDCardAttachment).ToList();
                List<DBModels.Model.MT_Attachment> empAttchList = _db.UserRegisterEntries.Where(x => (x.OrganizationID == organizationID) && (x.EmployeeIDCardAttachmentID != null)).Select(y => y.EmployeeIDCardAttachment).ToList();
                string rootDestinationFolderPath = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.AttachFilePath).Select(y => y.ParameterValue).FirstOrDefault();
                DBModels.Model.MT_Attachment attch;
                List<String> destFiles = new List<string>();
                String destinationFilePath;

                if (idCardAttchList != null)
                {
                    for (int i = 0; i < idCardAttchList.Count; i++)
                    {
                        attch = idCardAttchList[i];
                        destinationFilePath = rootDestinationFolderPath + attch.PathName;
                        destFiles.Add(destinationFilePath);

                    }
                }

                if (empAttchList != null)
                {
                    for (int i = 0; i < empAttchList.Count; i++)
                    {
                        attch = empAttchList[i];
                        destinationFilePath = rootDestinationFolderPath + attch.PathName;
                        destFiles.Add(destinationFilePath);

                    }
                }

                bool isCommit = false;
                using (var tran = _db.Database.BeginTransaction())
                {
                    if (orgRegisterEntryList != null)
                    {
                        _db.OrganizationRegisterEntries.RemoveRange(orgRegisterEntryList);
                    }
                    if (userRegisterEntryList != null)
                    {
                        _db.UserRegisterEntries.RemoveRange(userRegisterEntryList);
                    }

                    if (userList != null)
                    {
                        _db.SC_User.RemoveRange(userList);
                    }

                    if (idCardAttchList != null)
                    {
                        _db.MT_Attachment.RemoveRange(idCardAttchList);
                    }

                    if (empAttchList != null)
                    {
                        _db.MT_Attachment.RemoveRange(empAttchList);
                    }

                    if (org != null)
                    {
                        _db.MT_Organization.Remove(org);
                    }

                    _db.SaveChanges();
                    tran.Commit();
                    isCommit = true;
                }

                if (isCommit)
                {
                    for (int i = 0; i < destFiles.Count; i++)
                    {
                        destinationFilePath = destFiles[i];
                        if (File.Exists(destinationFilePath))
                        {
                            File.Delete(destinationFilePath);
                        }
                    }
                }
                result.IsCompleted = true;
                result.Message.Add(Nep.Project.Resources.Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Organization", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.DecimalDropDownListData> ListDropDown(ServiceModels.QueryParameter param)
        {
            var query = (from o in _db.MT_Organization
                         select new ServiceModels.DecimalDropDownListData
                         {
                             Value = o.OrganizationID,
                             Text = o.OrganizationNameTH,
                             ParentID = o.ProvinceID
                         });

            return query.ToQueryData(param);
        }

        public ServiceModels.ReturnQueryData<Int32> ListValueMapping(List<decimal> orgIds, decimal? provinceID)
        {
            ServiceModels.ReturnQueryData<Int32> result = new ServiceModels.ReturnQueryData<Int32>();
            try
            {
                result.IsCompleted = true;
                List<Int32> indices = new List<Int32>();
                int centerProvinceID = _provinceService.GetCenterProvinceID().Data;
                if (orgIds != null && orgIds.Count > 0)
                {
                    List<decimal> dbOrgIds = new List<decimal>();

                    if (provinceID.HasValue && (provinceID != centerProvinceID))
                    {
                        dbOrgIds = (from org in _db.MT_Organization
                                    where org.ProvinceID == (decimal)provinceID
                                    orderby org.OrganizationNameTH ascending
                                    select org.OrganizationID).ToList();
                    }
                    else
                    {
                        dbOrgIds = (from org in _db.MT_Organization
                                    orderby org.OrganizationNameTH ascending
                                    select org.OrganizationID).ToList();
                    }

                    Int32 index = 0;
                    if (dbOrgIds != null)
                    {
                        foreach (decimal id in dbOrgIds)
                        {
                            if (orgIds.Contains(id))
                            {
                                indices.Add(index);
                            }

                            index += 1;
                        }
                    }

                }

                result.Data = indices;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }
        //kenghot
        public ReturnObject<bool> IsBlackList(decimal? orgId, decimal? provinceId)
        {
            ServiceModels.ReturnObject<bool> result = new ServiceModels.ReturnObject<bool>();
            result.Data = false;
            try
            {
                result.IsCompleted = true;
                var lov = new ListOfValueService(_db);
                var l = lov.GetListOfValueByCode(Common.LOVGroup.FollowupStatus, Common.LOVCode.Followupstatus.รายงานผลแล้ว);
                if (l.IsCompleted)
                {
                    var i = from p in _db.View_ProjectList where p.IsFollowup.HasValue && p.IsFollowup.Value == 1 select p;
                    if (orgId.HasValue) i = i.Where(w => w.OrganizationID == orgId.Value);
                    if (provinceId.HasValue) i = i.Where(w => w.ProvinceID == provinceId.Value);

                    var q = i.FirstOrDefault();

                    result.Data = (q != null);
                }


            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
            }
            return result;
        }
    }
}
