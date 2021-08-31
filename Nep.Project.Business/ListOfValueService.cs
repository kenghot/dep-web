using Nep.Project.Common;
using Nep.Project.DBModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ListOfValueService : IServices.IListOfValueService
    {
        private readonly NepProjectDBEntities _db;

        public ListOfValueService(NepProjectDBEntities db)
        {
            _db = db;
            //_loggedUser = loggedUser;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListAll(string lovGroup)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> result = new ServiceModels.ReturnQueryData<ServiceModels.ListOfValue>();
            try
            {
                result.IsCompleted = true;
                result.Data = (from lov in _db.MT_ListOfValue.Where(l => l.LOVGroup == lovGroup)                              
                               select new ServiceModels.ListOfValue()
                               {
                                   LovID = lov.LOVID,
                                   LovCode = lov.LOVCode,
                                   LovName = lov.LOVName,
                                   OrderNo = lov.OrderNo,
                                   IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE)? true : false
                               }).OrderBy(or => or.OrderNo).ToList();

                

            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "List of Value", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListActive(string lovGroup, decimal? selectedID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> result = new ServiceModels.ReturnQueryData<ServiceModels.ListOfValue>();
            try
            {
                result.IsCompleted = true;

                if (selectedID.HasValue)
                {
                    decimal id = (decimal)selectedID;

                    result.Data = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == lovGroup) && (l.IsActive == "1"))
                                   select new ServiceModels.ListOfValue()
                                   {
                                       LovID = lov.LOVID,
                                       LovCode = lov.LOVCode,
                                       LovName = lov.LOVName,
                                       OrderNo = lov.OrderNo,
                                       IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                   })
                                   .Union(
                                    from lov in _db.MT_ListOfValue.Where(l => l.LOVID == id)
                                    select new ServiceModels.ListOfValue()
                                    {
                                        LovID = lov.LOVID,
                                        LovCode = lov.LOVCode,
                                        LovName = lov.LOVName,
                                        OrderNo = lov.OrderNo,
                                        IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                    }
                                   ).OrderBy(or => or.OrderNo).ToList();
                }
                else
                {
                    result.Data = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == lovGroup) && (l.IsActive == "1"))
                                   select new ServiceModels.ListOfValue()
                                   {
                                       LovID = lov.LOVID,
                                       LovCode = lov.LOVCode,
                                       LovName = lov.LOVName,
                                       OrderNo = lov.OrderNo,
                                       IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                   }).OrderBy(or => or.OrderNo).ToList();
                }
                
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "List of Value", ex);
            }

            return result;
        }

        public ServiceModels.ReturnObject<ServiceModels.ListOfValue> GetListOfValueByCode(string lovGroup, string lovCode)
        {
            ServiceModels.ReturnObject<ServiceModels.ListOfValue> result = new ServiceModels.ReturnObject<ServiceModels.ListOfValue>();
            try
            {
                result.IsCompleted = true;
                result.Data = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == lovGroup) && (l.LOVCode == lovCode))                              
                               select new ServiceModels.ListOfValue()
                               {
                                   LovID = lov.LOVID,
                                   LovCode = lov.LOVCode,
                                   LovName = lov.LOVName,
                                   OrderNo = lov.OrderNo,
                                   IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                               }).FirstOrDefault();



            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "List of Value", ex);
            }

            return result;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> ListBudgetType(decimal provinceID, decimal? selectedID)
        {
            ServiceModels.ReturnQueryData<ServiceModels.ListOfValue> result = new ServiceModels.ReturnQueryData<ServiceModels.ListOfValue>();
            DBModels.Model.MT_Province prov = _db.MT_Province.Where(x => x.ProvinceID == provinceID).FirstOrDefault();
            DBModels.Model.MT_OrganizationParameter param = _db.MT_OrganizationParameter.Where(x => x.ParameterCode == Common.OrganizationParameterCode.CENTER_PROVINCE_ABBR).FirstOrDefault();

            bool isCenter = (prov.ProvinceAbbr == param.ParameterValue);
            try
            {
                result.IsCompleted = true;
                //String[] provCodes = new String[] { Common.LOVCode.Budgettype.แผนพัฒนาคุณภาพชีวิตคนพิการประจำจังหวัด, 
                //    Common.LOVCode.Budgettype.การดำเนินงานของ_อปท___MATCHING_FUND,
                //    Common.LOVCode.Budgettype.สนับสนุนศูนย์บริการคนพิการ,
                //    Common.LOVCode.Budgettype.การขับเคลื่อนการพัฒนาสตรีพิการ,
                //    Common.LOVCode.Budgettype.กรอบวงเงินการจัดทำแผนพัฒนาคุณภาพชีวิตคนพิการจังหวัด_ฉบับที่_3__พ_ศ__2560___2564_จำนวน,
                //    Common.LOVCode.Budgettype.กรอบวงเงินวันคนพิการสากลปี_2559_ส่วนภูมิภาค ,
                //    Common.LOVCode.Budgettype.กรอบวงเงินสนับการดำเนินงานของ_อปท_ ,
                //    Common.LOVCode.Budgettype.กรอบวงเงินสนับสนุนการขับเคลื่อนยุทธศาสตร์การพัฒนาสตรีพิการ_,
                //    Common.LOVCode.Budgettype.กรอบวงเงินสนับสนุนจังหวัดในการประชุมเตรียมความพร้อมและซักซ้อมคนพิการในสถานการณ์ภัยพิบัติ,
                //    Common.LOVCode.Budgettype.กรอบวงเงินสนับสนุนศูนย์บริการคนพิการ,
                //    Common.LOVCode.Budgettype.กรอบวงเงินสนับสนุนโครงการตามแผนพัฒนาคุณภาพชีวิตคนพิการคนพิการประจำจังหวัด,
                //    Common.LOVCode.Budgettype.กรอบวงเงินปรับปรุงเช่าหรือก่อสร้างสำนักงานศูนย์บริการคนพิการระดับจังหวัด
                //};
                List<ServiceModels.ListOfValue> list = new List<ServiceModels.ListOfValue>();

                if (selectedID.HasValue)
                {
                    decimal id = (decimal)selectedID;

                    //list = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == Common.LOVGroup.BudgetType) && (l.IsActive == "1"))
                    //               select new ServiceModels.ListOfValue()
                    //               {
                    //                   LovID = lov.LOVID,
                    //                   LovCode = lov.LOVCode,
                    //                   LovName = lov.LOVName ,
                    //                   OrderNo = lov.OrderNo,
                    //                   IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                    //               })
                    //               .Union(
                    //                from lov in _db.MT_ListOfValue.Where(l => l.LOVID == id)
                    //                select new ServiceModels.ListOfValue()
                    //                {
                    //                    LovID = lov.LOVID,
                    //                    LovCode = lov.LOVCode,
                    //                    LovName = lov.LOVName,
                    //                    OrderNo = lov.OrderNo,
                    //                    IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                    //                }
                    // 
                    //Beer312021
                    list = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == Common.LOVGroup.BudgetType) && (l.IsActive == "1") && (l.LOVID == id))
                                select new ServiceModels.ListOfValue()
                                {
                                    LovID = lov.LOVID,
                                    LovCode = lov.LOVCode,
                                    LovName = lov.LOVName,
                                    OrderNo = lov.OrderNo,
                                    IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                })
                                 .Union(
                                        from lov in _db.MT_ListOfValue.Where(l => !isCenter ? (l.LOVGroup == Common.LOVGroup.BudgetTypeProvince): (l.LOVGroup == Common.LOVGroup.BudgetTypeCenter))
                                        select new ServiceModels.ListOfValue()
                                        {
                                            LovID = lov.LOVID,
                                            LovCode = lov.LOVCode,
                                            LovName = lov.LOVName,
                                            OrderNo = lov.OrderNo,
                                            IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                        }
                                   ).OrderBy(or => or.OrderNo).ToList();
                }
                else if(isCenter)
                {
                    //Beer312021
                    list = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == Common.LOVGroup.BudgetTypeCenter) && (l.IsActive == "1"))
                            select new ServiceModels.ListOfValue()
                            {
                                LovID = lov.LOVID,
                                LovCode = lov.LOVCode,
                                LovName = lov.LOVName,
                                OrderNo = lov.OrderNo,
                                IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                            }).OrderBy(or => or.OrderNo).ToList();
                }
                else if (!isCenter)
                {
                    //Beer312021
                    list = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == Common.LOVGroup.BudgetTypeProvince) && (l.IsActive == "1"))
                            select new ServiceModels.ListOfValue()
                            {
                                LovID = lov.LOVID,
                                LovCode = lov.LOVCode,
                                LovName = lov.LOVName,
                                OrderNo = lov.OrderNo,
                                IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                            }).OrderBy(or => or.OrderNo).ToList();
                }
                else
                {
                    //Beer312021
                    list = (from lov in _db.MT_ListOfValue.Where(l => (l.LOVGroup == Common.LOVGroup.BudgetTypeCenter || l.LOVGroup == Common.LOVGroup.BudgetTypeProvince) && (l.IsActive == "1"))
                                   select new ServiceModels.ListOfValue()
                                   {
                                       LovID = lov.LOVID,
                                       LovCode = lov.LOVCode,
                                       LovName = lov.LOVName ,
                                       OrderNo = lov.OrderNo,
                                       IsActive = (lov.IsActive == Common.Constants.BOOLEAN_TRUE) ? true : false
                                   }).OrderBy(or => or.OrderNo).ToList();
                }
                // list.Add(new ServiceModels.ListOfValue { IsActive = true, LovCode = "0", LovID = 0, LovName = list.Count().ToString(), OrderNo = 0 });
                //if (!isCenter)
                //{
                //    list = list.Where(x => provCodes.Contains(x.LovCode)).ToList();
                //}

                result.Data = list;
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "List of Value", ex);
            }
            return result;
        }
    }
}
