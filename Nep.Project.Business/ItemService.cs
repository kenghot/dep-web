using Nep.Project.Common;
using Nep.Project.DBModels.Model;
using Nep.Project.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public class ItemService : IServices.IItemService
    {
        private readonly NepProjectDBEntities _db;

        public ItemService(NepProjectDBEntities db)
        {
            _db = db;
        }

        public ServiceModels.ReturnQueryData<ServiceModels.ItemList> ListStrategic(ServiceModels.QueryParameter p)
        {
            var result = (from item in _db.MT_ITEM.Where(l => (l.ITEMGROUP == "EVALUATIONSTRATEGIC"))
                          where (item.ISACTIVE == "1")
                          select new ServiceModels.ItemList
                          {
                              ITEMID = item.ITEMID,
                              ITEMGROUP = item.ITEMGROUP,
                              ITEMNAME = item.ITEMNAME,
                              ORDERNO = item.ORDERNO,
                          });
            return result.ToQueryData(p);
        }
        public ServiceModels.ReturnObject<ServiceModels.Item> GetListItem(string ItemGroup)
        {
            ServiceModels.ReturnObject<ServiceModels.Item> result = new ServiceModels.ReturnObject<ServiceModels.Item>();
            try
            {
                result.IsCompleted = true;
                result.Data = (from item in _db.MT_ITEM.Where(l => (l.ITEMGROUP == ItemGroup))                              
                               select new ServiceModels.Item()
                               {
                                   ITEMID = item.ITEMID,
                                   ITEMGROUP = item.ITEMGROUP,
                                   ITEMNAME = item.ITEMNAME,
                                   ORDERNO = item.ORDERNO,
                                   ISACTIVE = (item.ISACTIVE == Common.Constants.BOOLEAN_TRUE) ? true : false
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

        public ReturnObject<Item> GetListItem(string lovGroup, string lovCode)
        {
            throw new NotImplementedException();
        }
    }
}
