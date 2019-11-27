using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Nep.Project.Common;

namespace Nep.Project.Business
{
    public class JobService : IServices.IJobService
    {   
        private readonly DBModels.Model.NepProjectDBEntities _db;

        public JobService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;           
        }

        public void ClearUserAccess()
        {
            using (var tran = _db.Database.BeginTransaction())
            {
                _db.ClearUserAccess();
                tran.Commit();
            }
        }
    }
}
