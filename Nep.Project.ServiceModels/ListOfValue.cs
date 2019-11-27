using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    [Serializable]
    public class ListOfValue
    {
        public Decimal LovID { get; set; }
        public String LovCode { get; set; }
        public String LovName { get; set; }
        public Decimal OrderNo { get; set; }
        public Boolean IsActive { get; set; }
    }
}
