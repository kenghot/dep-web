using Nep.Project.Common;
using Nep.Project.DBModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ProviceService : IServices.IProviceService
    {
        private readonly NepProjectDBEntities _db;
        //private readonly ServiceModels.Security.SecurityInfo _loggedUser;

        public ProviceService(NepProjectDBEntities db)
        {
            _db = db;
            //_loggedUser = loggedUser;
        }

        public ServiceModels.ReturnObject<Int32> GetCenterProvinceID()
        {
            ServiceModels.ReturnObject<Int32> result = new ServiceModels.ReturnObject<Int32>();
            try
            {
                string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                DBModels.Model.MT_Province centerProv = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).FirstOrDefault();
                result.IsCompleted = true;
                result.Data = (Int32)centerProv.ProvinceID;

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListOrgProvince(String filter)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                DBModels.Model.MT_Province centerProv = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).FirstOrDefault();
                result.IsCompleted = true;
                if (!String.IsNullOrEmpty(filter))
                {

                    list = (from province in _db.MT_Province.Where(p => p.ProvinceName.Contains(filter) && (p.ProvinceAbbr != centerAbbr))
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName,
                                   }).ToList();


                    list.Insert(0, new ServiceModels.GenericDropDownListData {Text = centerProv.ProvinceName, Value = centerProv.ProvinceID.ToString()});

                }
                else
                {
                    list = (from province in _db.MT_Province.Where(x => x.ProvinceAbbr != centerAbbr)
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName
                                   }).ToList();
                    list.Insert(0, new ServiceModels.GenericDropDownListData { Text = centerProv.ProvinceName, Value = centerProv.ProvinceID.ToString() });
                }

                result.Data = list;
                if(result.Data.Count == 0){
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }
                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }
            
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvince(String filter)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                result.IsCompleted = true;
                if (!String.IsNullOrEmpty(filter))
                {
                    result.Data = (from province in _db.MT_Province.Where(p =>(p.ProvinceName.Contains(filter) && (p.ProvinceAbbr != centerAbbr)))
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName
                                   }).ToList();
                }
                else
                {
                    result.Data = (from province in _db.MT_Province.Where(x => x.ProvinceAbbr != centerAbbr)
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName
                                   }).ToList();
                    
                }

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListProvinceByID(decimal? filterID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                result.IsCompleted = true;
                if (filterID.HasValue)
                {
                    result.Data = (from province in _db.MT_Province.Where(p => (p.ProvinceID == filterID))
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName
                                   }).ToList();
                }
                else
                {
                    result.Data = (from province in _db.MT_Province.Where(x => x.ProvinceAbbr != centerAbbr)
                                   orderby province.ProvinceName
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = province.ProvinceID.ToString(),
                                       Text = province.ProvinceName
                                   }).ToList();

                }

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }
            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListDistrict(Int32? provinceID, String filter)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                result.IsCompleted = true;
                if (!String.IsNullOrEmpty(filter))
                {
                    result.Data = (from district in _db.MT_District.Where(d => (d.ProvinceID == provinceID) && (d.DistrictName.Contains(filter)))
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = district.DistrictID.ToString(),
                                       Text = district.DistrictName
                                   }).ToList();
                }
                else
                {
                    if (provinceID != null)
                    {
                        result.Data = (from district in _db.MT_District.Where(d => d.ProvinceID == provinceID)
                                       select new ServiceModels.GenericDropDownListData()
                                       {
                                           Value = district.DistrictID.ToString(),
                                           Text = district.DistrictName
                                       }).ToList();
                    }
                    else
                    {
                        // For do not select province
                        result.Data = (from district in _db.MT_District
                                       select new ServiceModels.GenericDropDownListData()
                                       {
                                           Value = district.DistrictID.ToString(),
                                           Text = district.DistrictName
                                       }).ToList();
                    }
                }

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListSubDistrict(Int32? districtID, String filter)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                result.IsCompleted = true;
                if (!String.IsNullOrEmpty(filter))
                {
                    result.Data = (from subdistrict in _db.MT_SubDistrict.Where(sd => (sd.DistrictID == districtID) && (sd.SubDistrictName.Contains(filter)))
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = subdistrict.SubDistrictID.ToString(),
                                       Text = subdistrict.SubDistrictName
                                   }).ToList();
                }
                else
                {
                    if (districtID != null)
                    {
                        result.Data = (from subdistrict in _db.MT_SubDistrict.Where(sd => sd.DistrictID == districtID)
                                       select new ServiceModels.GenericDropDownListData()
                                       {
                                           Value = subdistrict.SubDistrictID.ToString(),
                                           Text = subdistrict.SubDistrictName
                                       }).ToList();
                    }
                    else
                    {
                        // For do not select district
                        result.Data = (from subdistrict in _db.MT_SubDistrict
                                       select new ServiceModels.GenericDropDownListData()
                                       {
                                           Value = subdistrict.SubDistrictID.ToString(),
                                           Text = subdistrict.SubDistrictName
                                       }).ToList();
                    }
                }

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListOrgProvinceSection(Int32? sectionID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            try
            {
                string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                DBModels.Model.MT_Province centerProv = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).FirstOrDefault();
                result.IsCompleted = true;
                if (sectionID != null)
                {
                    list = (from province in _db.MT_Province.Where(p => (p.SectionID == sectionID))
                            orderby province.ProvinceName
                            select new ServiceModels.GenericDropDownListData()
                            {
                                Value = province.ProvinceID.ToString(),
                                Text = province.ProvinceName,
                            }).ToList();
 
                }
                else
                {
                    list = (from province in _db.MT_Province.Where(x => x.ProvinceAbbr != centerAbbr)
                            orderby province.ProvinceName
                            select new ServiceModels.GenericDropDownListData()
                            {
                                Value = province.ProvinceID.ToString(),
                                Text = province.ProvinceName
                            }).ToList();
                    list.Insert(0, new ServiceModels.GenericDropDownListData { Text = centerProv.ProvinceName, Value = centerProv.ProvinceID.ToString() });
                }

                result.Data = list;

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }
        public ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> ListSection(Int32? provinceID)
        {
            string centerAbbr = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).Select(y => y.ParameterValue).FirstOrDefault();
            ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData> result = new ServiceModels.ReturnQueryData<ServiceModels.GenericDropDownListData>();
            DBModels.Model.MT_Province centerProv = _db.MT_Province.Where(x => x.ProvinceAbbr == centerAbbr).FirstOrDefault();
            try
            {
                result.IsCompleted = true;
                if (provinceID != null)
                {
                    result.Data = (from section in _db.MT_Province.Where(d => (d.ProvinceID == provinceID))
                                   group section by new { section.SectionID, section.Section.LOVName } into sectionGrouping
                                   select new ServiceModels.GenericDropDownListData()
                                   {
                                       Value = sectionGrouping.Key.SectionID.ToString(),
                                       Text = sectionGrouping.Key.LOVName
                                   }).ToList();
                }
                else
                {
                    result.Data = (from section in _db.MT_Province
                                    group section by new { section.SectionID, section.Section.LOVName } into sectionGrouping
                                    select new ServiceModels.GenericDropDownListData()
                                    {
                                        Value = sectionGrouping.Key.SectionID.ToString(),
                                        Text = sectionGrouping.Key.LOVName
                                    }).ToList();
                }

                if (result.Data.Count == 0)
                {
                    result.Message.Add(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    result.TotalRow = result.Data.Count;
                }

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Provice", ex);
            }

            return result;
        }

    }
}
